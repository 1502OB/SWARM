# mcp_diag.ps1 - finds where the desktop app keeps its MCP servers and writes a report
# to $HOME\swarm\tools\mcp_diag.txt (secret-looking values are masked).
$out = "$HOME\swarm\tools\mcp_diag.txt"
$log = New-Object System.Collections.Generic.List[string]
function L($s) { $log.Add([string]$s) }
function Mask($o) {
    if ($null -eq $o) { return $o }
    $j = $o | ConvertTo-Json -Depth 8 -Compress
    return ($j -replace '("(?i)[^"]*(key|token|secret|password|pass|auth)[^"]*"\s*:\s*")[^"]*"', '$1***"')
}

L "=== claude CLI ==="
$c = Get-Command claude -ErrorAction SilentlyContinue
L ("claude on PATH: " + $(if ($c) { $c.Source } else { 'NO' }))
if ($c) { L ((& claude --version 2>&1) -join ' '); L "--- claude mcp list ---"; (& claude mcp list 2>&1) | ForEach-Object { L $_ } }

L ""; L "=== candidate app folders ==="
$roots = @("$env:APPDATA\Claude", "$env:LOCALAPPDATA\Claude", "$env:LOCALAPPDATA\AnthropicClaude", "$env:APPDATA\Claude-3p")
$roots += Get-ChildItem "$env:LOCALAPPDATA\Packages" -Directory -ErrorAction SilentlyContinue |
          Where-Object { $_.Name -match 'claude|anthropic' } | ForEach-Object { $_.FullName }
foreach ($r in $roots) { L ("{0}  exists={1}" -f $r, (Test-Path $r)) }

L ""; L "=== config files (claude_desktop_config.json, *.mcp*.json) ==="
foreach ($r in $roots | Where-Object { Test-Path $_ }) {
    Get-ChildItem $r -Recurse -Depth 5 -File -ErrorAction SilentlyContinue |
      Where-Object { $_.Name -match '^claude_desktop_config.*\.json$|mcp.*\.json$|extensions-installations\.json$' } |
      ForEach-Object {
        L "--- $($_.FullName)"
        try { $j = Get-Content $_.FullName -Raw | ConvertFrom-Json
              if ($j.mcpServers) { foreach ($p in $j.mcpServers.PSObject.Properties) { L ("  server {0}: {1}" -f $p.Name, (Mask $p.Value)) } }
              else { L ("  keys: " + (($j.PSObject.Properties.Name) -join ', ')) } } catch { L "  (not json)" }
      }
}

L ""; L "=== extension manifests ==="
foreach ($r in $roots | Where-Object { Test-Path $_ }) {
    Get-ChildItem $r -Recurse -Depth 6 -File -Filter manifest.json -ErrorAction SilentlyContinue | ForEach-Object {
        try { $m = Get-Content $_.FullName -Raw | ConvertFrom-Json } catch { return }
        if (-not $m.server) { return }
        L "--- $($_.FullName)"
        L ("  name={0} display={1} type={2}" -f $m.name, $m.display_name, $m.server.type)
        L ("  mcp_config=" + (Mask $m.server.mcp_config))
        if ($m.user_config) { L ("  user_config keys: " + (($m.user_config.PSObject.Properties.Name) -join ', ')) }
    }
}

L ""; L "=== anything named revit/autocad/bluebeam + mcp elsewhere ==="
$search = @("$HOME", "$env:ProgramData", "$env:ProgramFiles", "C:\temp")
foreach ($s in $search) {
    Get-ChildItem $s -Recurse -Depth 3 -Directory -ErrorAction SilentlyContinue |
      Where-Object { $_.Name -match '(revit|autocad|acad|bluebeam|revu).*mcp|mcp.*(revit|autocad|acad|bluebeam|revu)' } |
      Select-Object -First 20 | ForEach-Object { L $_.FullName }
}
Get-Process -ErrorAction SilentlyContinue | Where-Object { $_.Path -match 'mcp' } |
  ForEach-Object { L ("process: {0}  {1}" -f $_.Name, $_.Path) }

$log | Set-Content -Path $out -Encoding UTF8
Write-Host "Wrote $out"
