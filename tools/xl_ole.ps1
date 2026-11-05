<#
xl_ole.ps1 - read / edit the Excel workbook of an embedded OLE object that AutoCAD has opened (OLEOPEN).

Usage (Windows PowerShell 5.1):
  powershell -NoProfile -ExecutionPolicy Bypass -File xl_ole.ps1 status
  powershell ... -File xl_ole.ps1 read  <out.json>      full dump to out.json + compact <out>.summary.json (read this one)
  powershell ... -File xl_ole.ps1 range <"Sheet!A1:D20"> <out.json>   only those cells (max 2000)
  powershell ... -File xl_ole.ps1 summary <dump.json> <out.json>     offline: summarize an existing read dump
  powershell ... -File xl_ole.ps1 set   <edits.json> <out.json>     changed cells only; full after-dump in <out>.after_full.json
  powershell ... -File xl_ole.ps1 copyfmt <spec.json> <out.json>   formats only (Format Painter), values kept
  powershell ... -File xl_ole.ps1 rowfmt <spec.json> <out.json>    row height / font size of a block (values kept)
  powershell ... -File xl_ole.ps1 close <keep|discard>

rowfmt spec: {"sheet":"Sheet1","range":"A3:B119","row_height":12.75,"font_size":9}  (either property optional)
  Sets RowHeight on every row of the range and Font.Size on its cells; values are checked unchanged afterwards
  and everything (heights, sizes, values) is restored if anything changed. Reports before/after per row.

edits.json: {"edits":[{"sheet":"Sheet1","cell":"C5","from":77,"to":67}]}
  Every edit is checked BEFORE any change: the cell must exist, must not contain a formula, and its current
  value must equal "from" (numbers within 1e-9, text exact). If any check fails, nothing is changed.
"close keep" returns the edited workbook to AutoCAD (the OLE object updates); "close discard" abandons changes.
Never saves anything to disk.
#>
param([Parameter(Mandatory = $true)][string]$Mode, [string]$Arg1, [string]$Arg2)
$ErrorActionPreference = 'Stop'

function Out-Json($obj, $path, [switch]$Compress) {
    $json = $obj | ConvertTo-Json -Depth 8 -Compress:$Compress
    if ($path) { [IO.File]::WriteAllText($path, $json, (New-Object Text.UTF8Encoding($false))) }
    else { $json }
}

function Get-Xl {
    try { return [Runtime.InteropServices.Marshal]::GetActiveObject('Excel.Application') }
    catch { throw 'No running Excel found. Run OLEOPEN on the object in AutoCAD first.' }
}

function Get-EmbeddedWb($xl) {
    $wbs = @($xl.Workbooks)
    if ($wbs.Count -eq 0) { throw 'Excel is running but has no open workbook.' }
    # An OLE-embedded workbook has no file path; a normal file has one.
    $emb = @($wbs | Where-Object { [string]::IsNullOrEmpty($_.Path) })
    if ($emb.Count -ne 1) {
        $names = ($wbs | ForEach-Object { $_.Name + ' [path=' + $_.Path + ']' }) -join '; '
        throw "Expected exactly one embedded (path-less) workbook, found $($emb.Count). Open: $names"
    }
    return $emb[0]
}

function Dump-Wb($wb) {
    $sheets = @()
    foreach ($ws in @($wb.Worksheets)) {
        $cells = @()
        $ur = $ws.UsedRange
        foreach ($c in @($ur.Cells)) {
            $f = [string]$c.Formula
            if ($f -ne '') {
                $cells += [ordered]@{
                    cell = $c.Address($false, $false); value = $c.Value2; text = [string]$c.Text; fmt = [string]$c.NumberFormat
                    formula = $(if ($c.HasFormula) { $f } else { $null })
                }
            }
        }
        $sheets += [ordered]@{ name = $ws.Name; used_range = $ur.Address($false, $false); cells = $cells }
    }
    return [ordered]@{ workbook = $wb.Name; sheet_count = $sheets.Count; sheets = $sheets }
}

function Split-Addr([string]$a) {
    # "AB12" -> @(28, 12)
    if ($a -match '^([A-Z]+)(\d+)$') {
        $n = 0; foreach ($ch in $Matches[1].ToCharArray()) { $n = $n * 26 + ([int]$ch - 64) }
        return @($n, [int]$Matches[2])
    }
    return $null
}

function Is-Num($v) { return ($v -is [double] -or $v -is [int] -or $v -is [long] -or $v -is [decimal]) }

function Summarize-Wb($dump) {
    # Every numeric cell with the nearest text label to its left (up to 6 columns), so an agent can find calc
    # inputs and outputs without reading the full dump. Capped at 400 numeric cells per sheet.
    $sheets = @()
    foreach ($s in @($dump.sheets)) {
        $grid = @{}
        foreach ($c in @($s.cells)) { $rc = Split-Addr $c.cell; if ($rc) { $grid["$($rc[0]),$($rc[1])"] = $c } }
        $entries = @(); $nIn = 0; $nF = 0; $nNum = 0
        foreach ($c in @($s.cells)) {
            if ($null -ne $c.formula) { $nF++ }
            if (-not (Is-Num $c.value)) { continue }
            $nNum++
            $isIn = ($null -eq $c.formula); if ($isIn) { $nIn++ }
            $rc = Split-Addr $c.cell; $label = $null
            if ($rc) {
                for ($k = 1; $k -le 6 -and $null -eq $label; $k++) {
                    $l = $grid["$($rc[0] - $k),$($rc[1])"]
                    if ($l -and $l.value -is [string] -and $l.value.Trim() -ne '') { $label = $l.value.Trim() }
                }
            }
            if ($entries.Count -lt 400) {
                # text only when it differs from the raw value (e.g. formatted dates, rounding, units)
                $e = [ordered]@{ cell = $c.cell; label = $label; value = $c.value; in = $isIn }
                if ([string]$c.text -ne [string]$c.value) { $e.text = $c.text }
                $entries += $e
            }
        }
        $sheets += [ordered]@{ name = $s.name; used_range = $s.used_range; cells = @($s.cells).Count; formulas = $nF
            numeric_cells = $nNum; numeric_inputs = $nIn; truncated = ($nNum -gt 400); numeric = $entries }
    }
    return [ordered]@{ workbook = $dump.workbook
        note = 'numeric cells with the nearest text label to the left; use range mode for anything else'; sheets = $sheets }
}

function Same($a, $b) {
    $na = 0.0; $nb = 0.0
    if ([double]::TryParse([string]$a, [ref]$na) -and [double]::TryParse([string]$b, [ref]$nb)) { return [math]::Abs($na - $nb) -lt 1e-9 }
    return ([string]$a) -ceq ([string]$b)
}

$StateFile = 'C:\Users\Jeremy\swarm\locks\xl_ole_tab.txt'

function Get-Acad {
    try { return [Runtime.InteropServices.Marshal]::GetActiveObject('AutoCAD.Application') }
    catch { throw 'No running AutoCAD found.' }
}

switch ($Mode) {
    'oleopen' {
        # Arg1 = handle (hex). Switch to the object's layout (paper space), then OLEOPEN via the command line.
        if ($Arg1 -notmatch '^[0-9A-Fa-f]{1,16}$') { throw 'oleopen needs a hex handle' }
        $doc = (Get-Acad).ActiveDocument
        if ([int]$doc.GetVariable('CMDACTIVE') -ne 0) { throw "AutoCAD is busy (CMDACTIVE=$($doc.GetVariable('CMDACTIVE'))); not sending OLEOPEN." }
        $obj = $doc.HandleToObject($Arg1)
        if ($obj.ObjectName -ne 'AcDbOle2Frame') { throw "handle $Arg1 is $($obj.ObjectName), not an OLE frame" }
        $owner = $doc.ObjectIdToObject($obj.OwnerID)
        if (-not $owner.IsLayout) { throw "handle $Arg1 is not owned by a layout block" }
        $target = $owner.Layout.Name
        $orig = $doc.ActiveLayout.Name
        if (-not (Test-Path $StateFile)) { [IO.File]::WriteAllText($StateFile, $orig) }  # keep the FIRST original tab
        if ($orig -ne $target) { $doc.ActiveLayout = $doc.Layouts.Item($target) }
        if ($target -ne 'Model' -and $doc.MSpace) { $doc.MSpace = $false }
        # OLEOPEN only acts on a PRE-selected object ("Object must be selected before entering..."), so set the
        # pickfirst selection to just this object first, then run OLEOPEN with no arguments.
        if ([int]$doc.GetVariable('PICKFIRST') -ne 1) { throw 'PICKFIRST is 0; OLEOPEN needs pre-selection. Not changing it automatically.' }
        $doc.SendCommand("(sssetfirst nil (ssadd (handent `"$Arg1`")))`n")
        Start-Sleep -Milliseconds 500
        $doc.SendCommand("_.OLEOPEN`n")
        $deadline = (Get-Date).AddSeconds(30); $found = $false
        while ((Get-Date) -lt $deadline -and -not $found) {
            Start-Sleep -Milliseconds 750
            try {
                $xl = [Runtime.InteropServices.Marshal]::GetActiveObject('Excel.Application')
                if (@(@($xl.Workbooks) | Where-Object { [string]::IsNullOrEmpty($_.Path) }).Count -ge 1) { $found = $true }
            } catch { }
        }
        Out-Json ([ordered]@{ handle = $Arg1; original_tab = $orig; switched_to = $target; workbook_found = $found }) $null
        if (-not $found) { exit 4 }
    }
    'restore' {
        # Return to the tab saved by the first oleopen, then forget it.
        if (-not (Test-Path $StateFile)) { 'nothing to restore'; break }
        $orig = [IO.File]::ReadAllText($StateFile).Trim()
        $doc = (Get-Acad).ActiveDocument
        if ($doc.ActiveLayout.Name -ne $orig) { $doc.ActiveLayout = $doc.Layouts.Item($orig) }
        Remove-Item $StateFile
        "restored tab $orig"
    }
    'geom' {
        # Arg1 = out.json. Geometry (points) of every non-empty cell on the ACTIVE sheet's used range, relative to
        # the used range's top-left. Used to map changed cells onto the OLE frame drawn in AutoCAD.
        $wb = Get-EmbeddedWb (Get-Xl); $ws = $wb.ActiveSheet; $ur = $ws.UsedRange
        $ox = [double]$ur.Left; $oy = [double]$ur.Top
        $cells = @()
        foreach ($c in @($ur.Cells)) {
            if ([string]$c.Formula -eq '') { continue }
            $m = $c.MergeArea
            $cells += [ordered]@{ cell = $c.Address($false, $false); text = [string]$c.Text
                x = [double]$m.Left - $ox; y = [double]$m.Top - $oy; w = [double]$m.Width; h = [double]$m.Height }
        }
        Out-Json ([ordered]@{ sheet = $ws.Name; used_range = $ur.Address($false, $false)
            width = [double]$ur.Width; height = [double]$ur.Height; cells = $cells }) $Arg1
        "geom ok -> $Arg1 ($($cells.Count) cells, sheet '$($ws.Name)')"
    }
    'status' {
        $xl = Get-Xl
        $list = @($xl.Workbooks) | ForEach-Object { [ordered]@{ name = $_.Name; path = $_.Path } }
        Out-Json ([ordered]@{ excel = $xl.Version; workbooks = @($list) }) $null
    }
    'read' {
        $wb = Get-EmbeddedWb (Get-Xl)
        $dump = Dump-Wb $wb
        Out-Json $dump $Arg1
        $sum = $Arg1 -replace '\.json$', '.summary.json'
        Out-Json (Summarize-Wb $dump) $sum -Compress
        "read ok -> $Arg1 (full dump, do not Read it whole) and $sum (Read this)"
    }
    'summary' {
        # Offline: Arg1 = an existing read dump, Arg2 = out.json.
        $dump = Get-Content -Raw -Path $Arg1 | ConvertFrom-Json
        Out-Json (Summarize-Wb $dump) $Arg2 -Compress
        "summary ok -> $Arg2"
    }
    'range' {
        # Arg1 = "Sheet!A1:D20" (or "A1:D20" for the active sheet), Arg2 = out.json. Only those cells.
        $wb = Get-EmbeddedWb (Get-Xl)
        if ($Arg1 -match '^(.+)!([A-Za-z]+\d+(:[A-Za-z]+\d+)?)$') { $ws = $wb.Worksheets.Item($Matches[1].Trim("'")); $addr = $Matches[2] }
        else { $ws = $wb.ActiveSheet; $addr = $Arg1 }
        $rng = $ws.Range($addr)
        if ($rng.Cells.Count -gt 2000) { throw "range $addr has $($rng.Cells.Count) cells; ask for at most 2000" }
        $cells = @()
        foreach ($c in @($rng.Cells)) {
            $cells += [ordered]@{ cell = $c.Address($false, $false); value = $c.Value2; text = [string]$c.Text
                fmt = [string]$c.NumberFormat; formula = $(if ($c.HasFormula) { [string]$c.Formula } else { $null }) }
        }
        Out-Json ([ordered]@{ sheet = $ws.Name; range = $addr; cells = $cells }) $Arg2
        "range ok -> $Arg2 ($($cells.Count) cells, sheet '$($ws.Name)')"
    }
    'set' {
        $xl = Get-Xl; $wb = Get-EmbeddedWb $xl
        $spec = Get-Content -Raw -Path $Arg1 | ConvertFrom-Json
        $problems = @(); $targets = @()
        foreach ($e in @($spec.edits)) {
            try { $c = $wb.Worksheets.Item($e.sheet).Range($e.cell) } catch { $problems += "$($e.sheet)!$($e.cell): not found"; continue }
            if ($c.Cells.Count -ne 1) { $problems += "$($e.sheet)!$($e.cell): not a single cell"; continue }
            if ($null -ne $e.format) {
                # display-format edit: {sheet, cell, from_format, format}; value and formula are left untouched
                if ([string]$e.from_format -ne '*' -and [string]$c.NumberFormat -cne [string]$e.from_format) { $problems += "$($e.sheet)!$($e.cell): format '$($c.NumberFormat)' != expected '$($e.from_format)'"; continue }
                $targets += , @($c, $e); continue
            }
            if ($c.HasFormula) { $problems += "$($e.sheet)!$($e.cell): has formula $($c.Formula); refusing" ; continue }
            if (-not (Same $c.Value2 $e.from)) { $problems += "$($e.sheet)!$($e.cell): current '$($c.Value2)' != expected '$($e.from)'"; continue }
            $targets += , @($c, $e)
        }
        if ($problems.Count -gt 0) {
            Out-Json ([ordered]@{ applied = 0; problems = $problems }) $Arg2
            "REFUSED: no cells changed. " + ($problems -join ' | '); exit 3
        }
        $before = Dump-Wb $wb
        # PowerShell's COM binder caches the value type of the first `.Value2 = x` at a call site, so mixing
        # Int32/Decimal/String in one loop throws InvalidCastException. Set through InvokeMember instead,
        # with all numbers normalized to Double.
        # NOTE: `close discard` does NOT undo writes to an OLE-embedded workbook, so a mid-loop failure must be
        # rolled back here: every cell already written is restored to its original value before rethrowing.
        $done = @()
        try {
            foreach ($t in $targets) {
                if ($null -ne $t[1].format) {
                    $origf = [string]$t[0].NumberFormat
                    [void][System.__ComObject].InvokeMember('NumberFormat', [Reflection.BindingFlags]::SetProperty, $null, $t[0], @([string]$t[1].format))
                    $done += , @($t[0], $origf, 'fmt'); continue
                }
                $orig = $t[0].Value2
                $v = $t[1].to
                if ($v -is [decimal] -or $v -is [int] -or $v -is [long]) { $v = [double]$v }
                [void][System.__ComObject].InvokeMember('Value2', [Reflection.BindingFlags]::SetProperty, $null, $t[0], @($v))
                $done += , @($t[0], $orig)
            }
        } catch {
            $err = $_.Exception.Message
            [array]::Reverse($done)
            foreach ($d in $done) { $p = $(if ($d.Count -gt 2) { 'NumberFormat' } else { 'Value2' }); [void][System.__ComObject].InvokeMember($p, [Reflection.BindingFlags]::SetProperty, $null, $d[0], @($d[1])) }
            $xl.Calculate()
            Out-Json ([ordered]@{ applied = 0; rolled_back = $done.Count; error = $err }) $Arg2
            "FAILED and rolled back $($done.Count) cell(s): $err"; exit 5
        }
        $xl.Calculate()
        $after = Dump-Wb $wb
        # diff every cell whose value changed (inputs and recalculated outputs)
        $changes = @()
        for ($i = 0; $i -lt $after.sheets.Count; $i++) {
            $bmap = @{}; foreach ($c in $before.sheets[$i].cells) { $bmap[$c.cell] = $c }
            foreach ($c in $after.sheets[$i].cells) {
                $b = $bmap[$c.cell]
                $bv = $(if ($b) { $b.value } else { $null })
                if (-not (Same $bv $c.value)) {
                    $changes += [ordered]@{ sheet = $after.sheets[$i].name; cell = $c.cell; before = $bv; after = $c.value
                        text_after = $c.text; is_input = [bool]($null -eq $c.formula) }
                }
            }
        }
        $full = $Arg2 -replace '\.json$', '.after_full.json'
        Out-Json $after $full
        Out-Json ([ordered]@{ applied = $targets.Count; changed_cells = $changes; after_full = $full }) $Arg2
        "set ok: $($targets.Count) input(s) changed, $($changes.Count) cell value(s) changed -> $Arg2"
    }
    'copyfmt' {
        # Arg1 = spec.json {"sheet":"S","from":"A37:K37","to":"A38:K38"}, Arg2 = out.json.
        # Copies cell formats (font, borders, fill, alignment, number format) and row height like Format Painter.
        # Values are left untouched and checked unchanged afterwards; any value change is rolled back.
        $xl = Get-Xl; $wb = Get-EmbeddedWb $xl
        $spec = Get-Content -Raw -Path $Arg1 | ConvertFrom-Json
        $ws = $wb.Worksheets.Item([string]$spec.sheet)
        $src = $ws.Range([string]$spec.from); $dst = $ws.Range([string]$spec.to)
        if ($src.Rows.Count -ne 1 -or $dst.Rows.Count -ne 1 -or $src.Columns.Count -ne $dst.Columns.Count) {
            Out-Json ([ordered]@{ applied = 0; problem = 'from/to must be single rows of equal width' }) $Arg2
            'REFUSED: from/to must be single rows of equal width'; exit 3
        }
        $vals = @(); foreach ($c in @($dst.Cells)) { $vals += , @($c, $c.Value2) }
        $origh = $dst.RowHeight
        [void]$src.Copy()
        [void]$dst.PasteSpecial(-4122)   # xlPasteFormats
        $xl.CutCopyMode = $false
        $dst.RowHeight = $src.RowHeight
        $bad = @(); foreach ($v in $vals) { if (-not (Same $v[0].Value2 $v[1])) { $bad += $v[0].Address($false, $false) } }
        if ($bad.Count -gt 0) {
            foreach ($v in $vals) { [void][System.__ComObject].InvokeMember('Value2', [Reflection.BindingFlags]::SetProperty, $null, $v[0], @($v[1])) }
            Out-Json ([ordered]@{ applied = 0; values_changed = $bad; restored = $true }) $Arg2
            "FAILED: values changed in $($bad -join ',') (restored)"; exit 5
        }
        $cells = @()
        for ($i = 1; $i -le $dst.Columns.Count; $i++) {
            $s = $src.Cells.Item(1, $i); $d = $dst.Cells.Item(1, $i)
            $cells += [ordered]@{ cell = $d.Address($false, $false); font = [string]$d.Font.Name; size = $d.Font.Size; bold = $d.Font.Bold
                halign = $d.HorizontalAlignment; border_bottom = $d.Borders.Item(9).LineStyle
                matches_src = ([string]$d.Font.Name -eq [string]$s.Font.Name -and $d.Font.Size -eq $s.Font.Size -and $d.Font.Bold -eq $s.Font.Bold -and $d.HorizontalAlignment -eq $s.HorizontalAlignment -and $d.Borders.Item(9).LineStyle -eq $s.Borders.Item(9).LineStyle) }
        }
        Out-Json ([ordered]@{ applied = 1; sheet = $ws.Name; from = $spec.from; to = $spec.to; row_height_before = $origh; row_height_after = $dst.RowHeight; cells = $cells }) $Arg2
        "copyfmt ok: $($spec.from) -> $($spec.to) on '$($ws.Name)', values unchanged -> $Arg2"
    }
    'rowfmt' {
        $xl = Get-Xl; $wb = Get-EmbeddedWb $xl
        $spec = Get-Content -Raw -Path $Arg1 | ConvertFrom-Json
        $ws = $wb.Worksheets.Item([string]$spec.sheet)
        $rng = $ws.Range([string]$spec.range)
        if ($rng.Cells.Count -gt 4000) { 'REFUSED: range larger than 4000 cells'; exit 3 }
        $vals = @(); foreach ($c in @($rng.Cells)) { $vals += , @($c, $c.Value2, $c.Font.Size) }
        $rows = @(); foreach ($r in @($rng.Rows)) { $rows += , @($r, $r.RowHeight) }
        try {
            if ($null -ne $spec.row_height) { foreach ($r in $rows) { $r[0].RowHeight = [double]$spec.row_height } }
            if ($null -ne $spec.font_size) { $rng.Font.Size = [double]$spec.font_size }
        } catch {
            foreach ($r in $rows) { $r[0].RowHeight = $r[1] }; foreach ($v in $vals) { $v[0].Font.Size = $v[2] }
            Out-Json ([ordered]@{ applied = 0; error = $_.Exception.Message; restored = $true }) $Arg2
            "FAILED (restored): $($_.Exception.Message)"; exit 5
        }
        $bad = @(); foreach ($v in $vals) { if (-not (Same $v[0].Value2 $v[1])) { $bad += $v[0].Address($false, $false) } }
        if ($bad.Count -gt 0) {
            foreach ($v in $vals) { [void][System.__ComObject].InvokeMember('Value2', [Reflection.BindingFlags]::SetProperty, $null, $v[0], @($v[1])); $v[0].Font.Size = $v[2] }
            foreach ($r in $rows) { $r[0].RowHeight = $r[1] }
            Out-Json ([ordered]@{ applied = 0; values_changed = $bad; restored = $true }) $Arg2
            "FAILED: values changed in $($bad -join ',') (all restored)"; exit 5
        }
        $per = @(); foreach ($r in $rows) { $per += [ordered]@{ row = $r[0].Row; height_before = $r[1]; height_after = $r[0].RowHeight } }
        Out-Json ([ordered]@{ applied = 1; sheet = $ws.Name; range = $spec.range; rows = $per; font_size_after = $rng.Cells.Item(1, 1).Font.Size }) $Arg2
        "rowfmt ok: $($spec.range) height=$($spec.row_height) font=$($spec.font_size), values unchanged -> $Arg2"
    }
    'close' {
        $xl = Get-Xl; $wb = Get-EmbeddedWb $xl
        if ($Arg1 -eq 'keep') { $wb.Close($true) } elseif ($Arg1 -eq 'discard') { $wb.Close($false) }
        else { throw 'close needs keep or discard' }
        [void][Runtime.InteropServices.Marshal]::ReleaseComObject($wb); $wb = $null
        # Leave nothing behind: quit Excel once no workbook is open (user preference, 2026-10-16).
        Start-Sleep -Seconds 2
        $left = $xl.Workbooks.Count
        if ($left -eq 0) { $xl.Quit() }
        [void][Runtime.InteropServices.Marshal]::ReleaseComObject($xl); $xl = $null
        [GC]::Collect(); [GC]::WaitForPendingFinalizers()
        if ($left -eq 0) { "closed ($Arg1); Excel quit" } else { "closed ($Arg1); Excel left running with $left other workbook(s)" }
    }
    default { throw "unknown mode $Mode" }
}
