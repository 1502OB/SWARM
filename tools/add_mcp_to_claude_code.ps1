# add_mcp_to_claude_code.ps1
# Copies the Revit, AutoCAD and Bluebeam MCP servers from the Claude desktop app
# into Claude Code (CLI) at user scope, so they work in every project (incl. ~/swarm).
# Names match the swarm PreToolUse hook: revit-mcp, autocad-mcp, Bluebeam.
#
# Run in a normal PowerShell window:
#   powershell -ExecutionPolicy Bypass -File "$HOME\swarm\tools\add_mcp_to_claude_code.ps1"
# Add -DryRun to only print what it would do.

param([switch]$DryRun)

$ErrorActionPreference = 'Stop'

# Target name in Claude Code  <-  regex that finds it in the desktop app
$wanted = [ordered]@{
    'revit-mcp'   = 'revit'
    'autocad-mcp' = 'autocad|acad'
    'Bluebeam'    = 'bluebeam|revu'
}

if (-not (Get-Command claude -ErrorAction SilentlyContinue)) {
    throw "The 'claude' CLI isn't on PATH. Open a new terminal after installing Claude Code."
}

# Desktop app data folder (normal install or Microsoft Store/MSIX install)
$bases = @("$env:APPDATA\Claude")
$bases += Get-ChildItem "$env:LOCALAPPDATA\Packages" -Directory -Filter 'Claude_*' -ErrorAction SilentlyContinue |
          ForEach-Object { Join-Path $_.FullName 'LocalCache\Roaming\Claude' }
$bases = $bases | Where-Object { Test-Path $_ }
if (-not $bases) { throw "Couldn't find the Claude desktop app data folder." }

$found = @{}   # target name -> @{command; args; env; url; source}

function Expand-Vars([string]$s, [string]$dir, $userCfg) {
    if ($null -eq $s) { return $s }
    $s = $s.Replace('${__dirname}', $dir).Replace('${HOME}', $HOME).Replace('${pathSeparator}', '\').Replace('${/}', '\')
    if ($userCfg) {
        foreach ($p in $userCfg.PSObject.Properties) {
            $v = $p.Value
            if ($v -is [array]) { $v = ($v -join ',') }
            $s = $s.Replace('${user_config.' + $p.Name + '}', [string]$v)
        }
    }
    return $s
}

foreach ($base in $bases) {
    # 1) Servers in claude_desktop_config.json
    $cfgFile = Join-Path $base 'claude_desktop_config.json'
    if (Test-Path $cfgFile) {
        $cfg = Get-Content $cfgFile -Raw | ConvertFrom-Json
        if ($cfg.mcpServers) {
            foreach ($p in $cfg.mcpServers.PSObject.Properties) {
                foreach ($t in $wanted.Keys) {
                    if (-not $found[$t] -and $p.Name -match $wanted[$t]) {
                        $s = $p.Value
                        $found[$t] = @{ command = $s.command; args = @($s.args); env = $s.env; url = $s.url; source = "config: $($p.Name)" }
                    }
                }
            }
        }
    }

    # 2) Desktop extensions (.mcpb / .dxt) installed through Settings > Extensions
    $extRoot = Join-Path $base 'Claude Extensions'
    $setRoot = Join-Path $base 'Claude Extensions Settings'
    if (Test-Path $extRoot) {
        foreach ($dir in Get-ChildItem $extRoot -Directory) {
            $mf = Join-Path $dir.FullName 'manifest.json'
            if (-not (Test-Path $mf)) { continue }
            $m = Get-Content $mf -Raw | ConvertFrom-Json
            $label = "$($dir.Name) $($m.name) $($m.display_name)"
            foreach ($t in $wanted.Keys) {
                if ($found[$t] -or $label -notmatch $wanted[$t]) { continue }
                $mc = $m.server.mcp_config
                if (-not $mc) { continue }
                if ($mc.platform_overrides -and $mc.platform_overrides.win32) {
                    $o = $mc.platform_overrides.win32
                    foreach ($k in 'command','args','env') { if ($o.$k) { $mc.$k = $o.$k } }
                }
                # Saved user_config values (defaults first, then what you entered in the app)
                $uc = New-Object psobject
                if ($m.user_config) {
                    foreach ($p in $m.user_config.PSObject.Properties) {
                        if ($null -ne $p.Value.default) { $uc | Add-Member NoteProperty $p.Name $p.Value.default -Force }
                    }
                }
                $sf = Join-Path $setRoot ($dir.Name + '.json')
                if (Test-Path $sf) {
                    $saved = (Get-Content $sf -Raw | ConvertFrom-Json).userConfig
                    if ($saved) { foreach ($p in $saved.PSObject.Properties) { $uc | Add-Member NoteProperty $p.Name $p.Value -Force } }
                }
                $cmd  = Expand-Vars $mc.command $dir.FullName $uc
                $args = @($mc.args | ForEach-Object { Expand-Vars ([string]$_) $dir.FullName $uc })
                $env  = @{}
                if ($mc.env) { foreach ($p in $mc.env.PSObject.Properties) { $env[$p.Name] = Expand-Vars ([string]$p.Value) $dir.FullName $uc } }
                $found[$t] = @{ command = $cmd; args = $args; env = $env; url = $null; source = "extension: $($dir.Name)" }
            }
        }
    }
}

foreach ($t in $wanted.Keys) {
    $s = $found[$t]
    if (-not $s) {
        Write-Host "[$t] NOT FOUND in the desktop app. Add it by hand (see notes in chat)." -ForegroundColor Yellow
        continue
    }

    # Replace any existing user-scope entry with the same name
    $null = & claude mcp get $t 2>$null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "[$t] already in Claude Code, replacing it"
        if (-not $DryRun) { & claude mcp remove $t -s user 2>$null | Out-Null }
    }

    if ($s.url) {
        $cli = @('mcp','add',$t,'-s','user','--transport','http',$s.url)
    } else {
        $cmd  = $s.command
        $args = @($s.args | Where-Object { $_ -ne $null -and $_ -ne '' })
        # npx/npm/uvx are .cmd shims on Windows; Claude Code needs them wrapped in cmd /c
        if ($cmd -match '^(npx|npm|pnpm|yarn|uvx)(\.cmd)?$') { $args = @('/c', $cmd) + $args; $cmd = 'cmd' }
        $cli = @('mcp','add',$t,'-s','user')
        if ($s.env) {
            $pairs = if ($s.env -is [hashtable]) { $s.env.GetEnumerator() } else { $s.env.PSObject.Properties }
            foreach ($e in $pairs) { $cli += @('-e', "$($e.Name)=$($e.Value)") }
        }
        $cli += @('--', $cmd) + $args
    }

    Write-Host "[$t] from $($s.source)" -ForegroundColor Cyan
    Write-Host ("  claude " + ($cli -join ' '))
    if (-not $DryRun) {
        & claude @cli
        if ($LASTEXITCODE -ne 0) { Write-Host "  FAILED (exit $LASTEXITCODE)" -ForegroundColor Red }
    }
}

Write-Host ""
Write-Host "Done. Check with:  claude mcp list" -ForegroundColor Green
