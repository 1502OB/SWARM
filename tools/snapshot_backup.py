"""Copy the open model / drawing files into swarm/backups/<run-id>/ for snapshot-taker. Never overwrites.

    python C:/Users/Jeremy/swarm/tools/snapshot_backup.py <run-id> "<file>" ["<file>" ...]

Prints one JSON object: {"run_id", "dest", "files": [{"source", "backup": "copied|locked|cloud|missing|exists|error",
"path"?, "bytes"?, "error"?}]}. Exit 0 if every file was copied, 1 otherwise (the agent must report "locked"/"cloud"
to the orchestrator so Jeremy can save-as by hand).
Cloud models (ACC / BIM 360 paths) cannot be copied from disk: reported as "cloud".
"""
import json
import re
import shutil
import sys
from pathlib import Path

BACKUPS = Path("C:/Users/Jeremy/swarm/backups")
CLOUD = re.compile(r"^(BIM ?360|Autodesk Docs|cloud)[:/]", re.IGNORECASE)


def main():
    if len(sys.argv) < 3 or not re.fullmatch(r"\d{8}-\d{4}", sys.argv[1]):
        print('usage: snapshot_backup.py <run-id YYYYMMDD-HHMM> "<file>" ["<file>" ...]')
        sys.exit(2)
    run_id, sources = sys.argv[1], sys.argv[2:]
    dest = BACKUPS / run_id
    dest.mkdir(parents=True, exist_ok=True)
    out = []
    for s in sources:
        rec = {"source": s}
        src = Path(s)
        if CLOUD.match(s):
            rec["backup"] = "cloud"
        elif not src.is_file():
            rec["backup"] = "missing"
        else:
            target = dest / src.name
            if target.exists():
                rec["backup"], rec["path"] = "exists", target.as_posix()
            else:
                try:
                    shutil.copy2(src, target)
                    rec.update(backup="copied", path=target.as_posix(), bytes=target.stat().st_size)
                except PermissionError as e:
                    rec.update(backup="locked", error=str(e))
                except OSError as e:
                    rec.update(backup="locked" if getattr(e, "winerror", None) in (32, 33) else "error", error=str(e))
        out.append(rec)
    print(json.dumps({"run_id": run_id, "dest": dest.as_posix(), "files": out}, indent=1))
    sys.exit(0 if all(r["backup"] == "copied" for r in out) else 1)


if __name__ == "__main__":
    main()
