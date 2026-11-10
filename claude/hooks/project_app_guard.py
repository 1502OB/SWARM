"""PreToolUse guard: only call the apps this project uses.

Reads <SWARM>/manifests/project.json (SWARM from hooks/paths.py: $SWARM_HOME, else ~/swarm) ({"apps": ["revit"|"autocad", ...], ...}), written by the
orchestrator at Step 0. A Revit or AutoCAD MCP call to an app not in "apps" is blocked (exit 2). With no
project.json, every call is allowed, so older runs keep working.
"""
import json
import os
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
from paths import SWARM, fwd  # noqa: E402  (2026-11-09: no hard-coded user folder)

PROJECT = Path(os.environ.get("SWARM_MANIFESTS_DIR", fwd(SWARM / "manifests"))) / "project.json"
APPS = {"mcp__revit-mcp__": "revit", "mcp__autocad-mcp__": "autocad"}


def main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        sys.stderr.write("BLOCKED by project_app_guard: could not parse hook input\n")
        sys.exit(2)
    tool = payload.get("tool_name", "")
    app = next((a for p, a in APPS.items() if tool.startswith(p)), None)
    if app is None or not PROJECT.exists():
        sys.exit(0)
    try:
        apps = [str(a).lower() for a in json.loads(PROJECT.read_text(encoding="utf-8")).get("apps", [])]
    except Exception as e:  # noqa: BLE001
        sys.stderr.write(f"BLOCKED by project_app_guard: {PROJECT.as_posix()} is not valid JSON ({e})\n")
        sys.exit(2)
    if app not in apps:
        sys.stderr.write(f"BLOCKED by project_app_guard: this project uses {apps or 'no apps'} "
                         f"({PROJECT.as_posix()}), not {app}. Do not query {app}; report it as not applicable.\n")
        sys.exit(2)
    sys.exit(0)


if __name__ == "__main__":
    main()
