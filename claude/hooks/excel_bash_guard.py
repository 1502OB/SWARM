"""PreToolUse guard for excel-writer's Bash: only the xl_ole.ps1 helper, one command, no chaining.

Allowed: powershell -NoProfile -ExecutionPolicy Bypass -File <...>/swarm/tools/xl_ole.ps1 <status|read|set|close> [args]
Exit code 2 blocks the call.
"""
import json
import re
import sys

ALLOW = re.compile(
    r'^powershell(\.exe)? -NoProfile -ExecutionPolicy Bypass -File "?C:/Users/Jeremy/swarm/tools/xl_ole\.ps1"? '
    r'(status|read|range|summary|set|copyfmt|close|oleopen|restore)( ("[^"$`;&|<>]*"|[^\s"$`;&|<>]+)){0,2}\s*$',
    re.IGNORECASE,
)


def main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        sys.stderr.write("BLOCKED by excel_bash_guard: could not parse hook input\n")
        sys.exit(2)
    if payload.get("tool_name") != "Bash":
        sys.exit(0)
    cmd = str((payload.get("tool_input") or {}).get("command", "")).strip()
    if ALLOW.match(cmd):
        sys.exit(0)
    sys.stderr.write(
        "BLOCKED by excel_bash_guard: excel-writer may only run "
        'powershell -NoProfile -ExecutionPolicy Bypass -File "C:/Users/Jeremy/swarm/tools/xl_ole.ps1" <mode> [args] '
        "(single command, no chaining or substitution). Read lock files and JSON with the Read tool, not cat/head/grep; "
        "for specific cells use the helper's range mode.\n"
    )
    sys.exit(2)


if __name__ == "__main__":
    main()
