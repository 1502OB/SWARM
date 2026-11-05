<#
dialog_check.ps1 - READ-ONLY check for dialogs blocking Revit / AutoCAD (or any named process).

Never clicks, closes, focuses or types. It only enumerates windows, reads their text, and (with -CaptureDir)
saves an image of each DIALOG window alone (PrintWindow), never the desktop.

Run it when a bridge call hangs or times out, a Revit sync takes over ~2 min, or before retrying an app.

  pwsh -NoProfile -File swarm/tools/dialog_check.ps1                       # Revit + AutoCAD, readable report
  pwsh -NoProfile -File swarm/tools/dialog_check.ps1 -Json                 # same, as JSON for the orchestrator
  pwsh -NoProfile -File swarm/tools/dialog_check.ps1 -CaptureDir <run>/dialogs   # also PNG of each dialog
  pwsh -NoProfile -File swarm/tools/dialog_check.ps1 -ProcessName Revu

Exit codes: 0 = no dialog and every app responding, 1 = dialog found or app blocked/not responding,
            2 = none of the named apps is running.

How a dialog is recognised (per visible top-level window of the process):
  - class "#32770" (standard Windows dialogs, message boxes, TaskDialogs), or
  - an owned window whose owner is disabled (any modal: WPF/WinForms dialogs in Revit, AutoCAD alerts).
  Docked/floating palettes (AutoCAD Sheet Set Manager, Blocks, ...) leave the main window enabled and are skipped.
  A main window that is disabled with no dialog found is reported as "blocked (modal not found)".
Text: child-window text via WM_GETTEXT with a 500 ms timeout; if that finds no message text (TaskDialogs draw
their own), UI Automation names, but only when the process is responding (UIA can hang on a hung app).
Swarm: LESSONS.md O23. Built 2026-11-04.
#>
[CmdletBinding()]
param(
    [string[]]$ProcessName = @('Revit', 'acad'),
    [int[]]$ProcessId,
    [string]$CaptureDir,
    [switch]$Json
)
$ErrorActionPreference = 'Stop'

if (-not ('DlgCheck.Native' -as [type])) {
Add-Type -TypeDefinition @'
using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace DlgCheck {
public static class Native {
    public delegate bool EnumProc(IntPtr h, IntPtr l);
    [DllImport("user32.dll")] public static extern bool EnumWindows(EnumProc p, IntPtr l);
    [DllImport("user32.dll")] public static extern bool EnumChildWindows(IntPtr parent, EnumProc p, IntPtr l);
    [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
    [DllImport("user32.dll")] public static extern bool IsWindowEnabled(IntPtr h);
    [DllImport("user32.dll")] public static extern IntPtr GetWindow(IntPtr h, uint cmd);
    [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr h, out uint pid);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetClassName(IntPtr h, StringBuilder s, int n);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetWindowText(IntPtr h, StringBuilder s, int n);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessageTimeout(IntPtr h, uint msg, IntPtr w, StringBuilder l, uint flags, uint ms, out IntPtr res);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr h, IntPtr hdc, uint flags);
    [StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left, Top, Right, Bottom; }

    public static string ClassOf(IntPtr h) { var s = new StringBuilder(256); GetClassName(h, s, 256); return s.ToString(); }
    public static string TitleOf(IntPtr h) { var s = new StringBuilder(512); GetWindowText(h, s, 512); return s.ToString(); }
    // WM_GETTEXT (0x000D) with SMTO_ABORTIFHUNG (0x2): works across processes, never blocks on a hung app.
    public static string TextOf(IntPtr h) {
        var s = new StringBuilder(4096); IntPtr r;
        IntPtr ok = SendMessageTimeout(h, 0x000D, (IntPtr)4096, s, 0x2, 500, out r);
        return ok == IntPtr.Zero ? "" : s.ToString();
    }
    public static List<IntPtr> TopWindows(uint pid) {
        var list = new List<IntPtr>();
        EnumWindows((h, l) => { uint p; GetWindowThreadProcessId(h, out p);
            if (p == pid && IsWindowVisible(h)) list.Add(h); return true; }, IntPtr.Zero);
        return list;
    }
    public static List<string[]> Children(IntPtr parent) {
        var list = new List<string[]>();
        EnumChildWindows(parent, (h, l) => { if (IsWindowVisible(h)) list.Add(new string[] { ClassOf(h), TextOf(h) }); return true; }, IntPtr.Zero);
        return list;
    }
}
}
'@
}

function Get-UiaText([IntPtr]$hwnd) {
    # Message text and button names from UI Automation (TaskDialogs, WPF). Only call on a responding process.
    try {
        Add-Type -AssemblyName UIAutomationClient, UIAutomationTypes -ErrorAction Stop
        $root = [System.Windows.Automation.AutomationElement]::FromHandle($hwnd)
        $all = $root.FindAll([System.Windows.Automation.TreeScope]::Descendants, [System.Windows.Automation.Condition]::TrueCondition)
        $text = New-Object System.Collections.Generic.List[string]
        $buttons = New-Object System.Collections.Generic.List[string]
        foreach ($e in $all) {
            $name = $e.Current.Name
            if ([string]::IsNullOrWhiteSpace($name)) { continue }
            $type = $e.Current.ControlType.ProgrammaticName
            if ($type -eq 'ControlType.Button' -or $type -eq 'ControlType.RadioButton' -or $type -eq 'ControlType.CheckBox') {
                if (-not $buttons.Contains($name)) { $buttons.Add($name) }
            } elseif ($type -eq 'ControlType.Text' -or $type -eq 'ControlType.Edit' -or $type -eq 'ControlType.Document') {
                if (-not $text.Contains($name)) { $text.Add($name) }
            }
        }
        return @{ text = @($text); buttons = @($buttons) }
    } catch {
        return @{ text = @(); buttons = @(); error = "uia: $($_.Exception.Message)" }
    }
}

function Save-WindowImage([IntPtr]$hwnd, [string]$path) {
    Add-Type -AssemblyName System.Drawing
    $r = New-Object DlgCheck.Native+RECT
    [void][DlgCheck.Native]::GetWindowRect($hwnd, [ref]$r)
    $w = $r.Right - $r.Left; $h = $r.Bottom - $r.Top
    if ($w -le 0 -or $h -le 0) { return $null }
    $bmp = New-Object System.Drawing.Bitmap $w, $h
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $hdc = $g.GetHdc()
    # PW_RENDERFULLCONTENT (2): draws this window only, even if covered by other windows.
    $ok = [DlgCheck.Native]::PrintWindow($hwnd, $hdc, 2)
    $g.ReleaseHdc($hdc); $g.Dispose()
    if ($ok) { $bmp.Save($path, [System.Drawing.Imaging.ImageFormat]::Png) }
    $bmp.Dispose()
    if ($ok) { return $path } else { return $null }
}

# ---- processes ----
$procs = @()
if ($ProcessId) { $procs = @(Get-Process -Id $ProcessId -ErrorAction SilentlyContinue) }
else { foreach ($n in $ProcessName) { $procs += @(Get-Process -Name $n -ErrorAction SilentlyContinue) } }

$report = [ordered]@{ checked_at = (Get-Date).ToString('s'); apps = @() }
$blocked = $false
$stamp = (Get-Date).ToString('HHmmss')
if ($CaptureDir) { New-Item -ItemType Directory -Force -Path $CaptureDir | Out-Null }

foreach ($p in $procs) {
    $main = $p.MainWindowHandle
    $mainEnabled = if ($main -ne [IntPtr]::Zero) { [DlgCheck.Native]::IsWindowEnabled($main) } else { $null }
    $app = [ordered]@{
        process = $p.ProcessName; pid = $p.Id
        main_title = $p.MainWindowTitle
        responding = $p.Responding
        main_window_enabled = $mainEnabled
        dialogs = @()
    }
    $i = 0
    foreach ($h in [DlgCheck.Native]::TopWindows([uint32]$p.Id)) {
        $cls = [DlgCheck.Native]::ClassOf($h)
        if ($h -eq $main -and $cls -ne '#32770') { continue }   # a process whose only window is a dialog reports it as main
        $owner = [DlgCheck.Native]::GetWindow($h, 4)   # GW_OWNER
        $ownerDisabled = ($owner -ne [IntPtr]::Zero) -and (-not [DlgCheck.Native]::IsWindowEnabled($owner))
        if (-not ($cls -eq '#32770' -or $ownerDisabled)) { continue }
        $i++
        $title = [DlgCheck.Native]::TitleOf($h)
        $text = New-Object System.Collections.Generic.List[string]
        $buttons = New-Object System.Collections.Generic.List[string]
        foreach ($c in [DlgCheck.Native]::Children($h)) {
            $t = ($c[1] -replace '&(?=\S)', '').Trim()
            if (-not $t) { continue }
            if ($c[0] -eq 'Button') { if (-not $buttons.Contains($t)) { $buttons.Add($t) } }
            elseif ($c[0] -in @('Static', 'Edit', 'RichEdit20W', 'RICHEDIT50W')) { if (-not $text.Contains($t)) { $text.Add($t) } }
        }
        $source = 'win32'
        $uiaErr = $null
        if ($text.Count -eq 0) {
            if ($p.Responding) {
                $u = Get-UiaText $h
                foreach ($t in $u.text) { if ($t -ne $title -and -not $text.Contains($t)) { $text.Add($t) } }
                foreach ($b in $u.buttons) { if (-not $buttons.Contains($b)) { $buttons.Add($b) } }
                $source = 'uia'; $uiaErr = $u.error
            } else { $source = 'none (process not responding; use -CaptureDir for an image)' }
        }
        $d = [ordered]@{
            title = $title; class = $cls
            modal = $ownerDisabled
            text = @($text); buttons = @($buttons); text_source = $source
        }
        if ($uiaErr) { $d.error = $uiaErr }
        if ($CaptureDir) {
            $file = Join-Path $CaptureDir ("{0}_{1}_{2}_dialog{3}.png" -f $p.ProcessName, $p.Id, $stamp, $i)
            $d.image = Save-WindowImage $h $file
        }
        $app.dialogs += $d
    }
    $state = 'clear'
    if (-not $p.Responding) { $state = 'not responding' }
    if ($app.dialogs.Count -gt 0) { $state = 'dialog open' }
    elseif ($mainEnabled -eq $false) { $state = 'blocked (main window disabled, modal not found)' }
    $app.state = $state
    if ($state -ne 'clear') { $blocked = $true }
    $report.apps += $app
}

if ($Json) {
    $report | ConvertTo-Json -Depth 6
} else {
    if ($procs.Count -eq 0) { "No running process named: $($ProcessName -join ', ')" }
    foreach ($a in $report.apps) {
        "{0} (pid {1}): {2}  [{3}]" -f $a.process, $a.pid, $a.state.ToUpper(), $a.main_title
        foreach ($d in $a.dialogs) {
            "  DIALOG: '{0}'{1}" -f $d.title, $(if ($d.modal) { ' (modal)' } else { '' })
            foreach ($t in $d.text) { "    | " + (($t -split "`r?`n") -join "`n    | ") }
            if ($d.buttons.Count) { "    buttons: [" + ($d.buttons -join '] [') + "]" }
            if ($d.image) { "    image: $($d.image)" }
            if ($d.error) { "    note: $($d.error)" }
            if ($d.text.Count -eq 0) { "    (no readable text: $($d.text_source))" }
        }
    }
}

if ($procs.Count -eq 0) { exit 2 }
if ($blocked) { exit 1 }
exit 0
