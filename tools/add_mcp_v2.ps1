# add_mcp_v2.ps1 - registers Revit and Bluebeam MCP servers with Claude Code (user scope)
# by copying the exact command lines the desktop app is running right now.
# Also looks for the autocad-mcp bridge. Writes a log to C:\Users\j\swarm\tools\mcp_add_log.txt
#
#   powershell -ExecutionPolicy Bypass -File "C:\Users\j\swarm\tools\add_mcp_v2.ps1"

$log = New-Object System.Collections.Generic.List[string]
function L($s) { $log.Add([string]$s); Write-Host $s }
$logFile = 'C:\Users\j\swarm\tools\mcp_add_log.txt'

Add-Type @"
using System; using System.Runtime.InteropServices;
public static class ArgvSplit {
  [DllImport("shell32.dll", SetLastError=true)] static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmd, out int n);
  [DllImport("kernel32.dll")] static extern IntPtr LocalFree(IntPtr h);
  public static string[] Split(string c) { int n; IntPtr p = CommandLineToArgvW(c, out n); var r = new string[n];
    for (int i = 0; i < n; i++) r[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(p, i * IntPtr.Size)); LocalFree(p); return r; }
}
"@

# Name in Claude Code -> how to spot it among running processes, and a fallback if not running
$targets = [ordered]@{
  'revit-mcp' = @{
    match    = 'revit_mcp_plugin.*build\\index\.js'
    fallback = @('C:\Users\j\AppData\Roaming\Autodesk\Revit\Addins\2025\revit_mcp_plugin\Commands\RevitMCPCommandSet\server\runtime\node.exe',
                 'C:\Users\j\AppData\Roaming\Autodesk\Revit\Addins\2025\revit_mcp_plugin\Commands\RevitMCPCommandSet\server\build\index.js')
  }
  'Bluebeam' = @{
    match    = 'Bluebeam MCP Server\.exe'
    fallback = @('C:\Program Files\Bluebeam Software\Bluebeam Revu\21\Revu\mcp\Bluebeam MCP Server.exe')
  }
  'autocad-mcp' = @{
    match    = 'autocad'
    fallback = $null
  }
}

$claude = (Get-Command claude -ErrorAction SilentlyContinue).Source
if (-not $claude) { L "claude CLI not on PATH"; $log | Set-Content $logFile; exit 1 }

$procs = Get-CimInstance Win32_Process -ErrorAction SilentlyContinue | Where-Object { $_.CommandLine }
L "=== running MCP-looking processes ==="
$procs | Where-Object { $_.CommandLine -match 'mcp|autocad|bluebeam' -and $_.Name -notmatch '^(claude|Claude)\.exe$' } |
  ForEach-Object { L ("{0} (pid {1}, parent {2}): {3}" -f $_.Name, $_.ProcessId, $_.ParentProcessId, $_.CommandLine) }

foreach ($name in $targets.Keys) {
  $t = $targets[$name]
  L ""; L "=== $name ==="
  $p = $procs | Where-Object { $_.CommandLine -match $t.match -and $_.Name -notmatch '^(acad|claude|Claude)\.exe$' } | Select-Object -First 1
  if ($p) { $argv = [ArgvSplit]::Split($p.CommandLine); L "using running process: $($p.CommandLine)" }
  elseif ($t.fallback -and (Test-Path $t.fallback[0])) { $argv = $t.fallback; L "not running, using known path" }
  else { L "NOT FOUND (no running process, no known path)"; continue }

  $cmd = $argv[0]; $rest = @(); if ($argv.Count -gt 1) { $rest = $argv[1..($argv.Count - 1)] }
  if ($cmd -match '(^|\\)(npx|npm|uvx|uv)(\.cmd|\.exe)?$' -and $cmd -notmatch '\.exe$') { $rest = @('/c', $cmd) + $rest; $cmd = 'cmd' }

  & $claude mcp remove $name -s user 2>&1 | Out-Null
  $cli = @('mcp', 'add', $name, '-s', 'user', '--', $cmd) + $rest
  L ("claude " + (($cli | ForEach-Object { if ($_ -match ' ') { '"' + $_ + '"' } else { $_ } }) -join ' '))
  & $claude @cli 2>&1 | ForEach-Object { L "  $_" }
}

L ""; L "=== folders that look like an autocad-mcp bridge ==="
$roots = @(@{p='C:\Users\j'; d=4}, @{p='C:\'; d=2}, @{p='C:\temp'; d=1})
foreach ($r in $roots) {
  try {
    Get-ChildItem $r.p -Recurse -Depth $r.d -Directory -ErrorAction SilentlyContinue |
      Where-Object { $_.Name -match 'autocad|acad' -and $_.FullName -notmatch '\\AppData\\Local\\(Temp|Packages)|\\Program Files' } |
      Select-Object -First 25 | ForEach-Object {
        $hint = (Get-ChildItem $_.FullName -File -ErrorAction SilentlyContinue | Where-Object { $_.Name -match '\.py$|pyproject|package\.json|server' } | Select-Object -First 5 | ForEach-Object Name) -join ', '
        L ("{0}   [{1}]" -f $_.FullName, $hint)
      }
  } catch { }
}
if (Test-Path 'C:\temp') { L "C:\temp files: " + ((Get-ChildItem 'C:\temp' -File -ErrorAction SilentlyContinue | Select-Object -First 30 | ForEach-Object Name) -join ', ') }

L ""; L "=== claude mcp list ==="
& $claude mcp list 2>&1 | ForEach-Object { L $_ }
$log | Set-Content -Path $logFile -Encoding UTF8
Write-Host "`nLog written to $logFile" -ForegroundColor Green
