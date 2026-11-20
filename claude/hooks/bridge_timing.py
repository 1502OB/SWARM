"""Record CAD MCP duration metadata without storing prompts, source code or tool responses."""
import hashlib
import json
import math
import os
import re
import sys
import time
from pathlib import Path

from paths import SWARM

TOOLS = {"mcp__revit-mcp__": "revit", "mcp__autocad-mcp__": "acad"}
EVENTS = {"PostToolUse": "ok", "PostToolUseFailure": "error"}


def record(payload, now):
    tool = payload.get("tool_name", "")
    app = next((name for prefix, name in TOOLS.items() if tool.startswith(prefix)), None)
    status = EVENTS.get(payload.get("hook_event_name"))
    duration = payload.get("duration_ms")
    call_id = payload.get("tool_use_id")
    if not app or not status or not call_id or not isinstance(duration, (float, int)) or duration < 0:
        return
    if not math.isfinite(duration):
        return
    manifests = Path(os.environ.get("SWARM_MANIFESTS_DIR", SWARM / "manifests"))
    project = json.loads((manifests / "project.json").read_text(encoding="utf-8-sig"))
    run_id = project.get("run_id", "")
    if not re.fullmatch(r"[0-9]{8}-[A-Za-z0-9_-]+", run_id):
        return
    digest = hashlib.sha256(str(call_id).encode("utf-8")).hexdigest()
    folder = SWARM / "logs" / f"{run_id}.bridge"
    folder.mkdir(parents=True, exist_ok=True)
    row = {"app": app, "tool": tool, "status": status, "duration_ms": duration, "finished_at": now}
    path = folder / (digest + ".json")
    try:
        with path.open("x", encoding="utf-8") as stream:
            json.dump(row, stream)
    except FileExistsError:
        pass


if __name__ == "__main__":
    finished_at = time.time()
    try:
        record(json.load(sys.stdin), finished_at)
    except (OSError, ValueError, TypeError, AttributeError):
        pass  # Timing is best effort and never blocks a model operation.
