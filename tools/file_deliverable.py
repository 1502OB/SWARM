"""Copy one approved deliverable to its project folder for filer. Never overwrites.

    python C:/Users/Jeremy/swarm/tools/file_deliverable.py "<source file>" "<destination folder>" "<new file name>" <project>

The source must be under C:/Users/Jeremy/swarm/output/. If <destination>/<new name> exists, a suffix _2, _3, ... is
added before the extension (and reported). Appends {"file","source","destination","renamed_from_conflict",
"timestamp"} to C:/Users/Jeremy/swarm/ledger/<project>.deliverables.json (a JSON list; created if missing).
Prints the ledger entry as JSON. Exit 0 = copied, 1 = refused / failed.
"""
import json
import re
import shutil
import sys
from datetime import datetime
from pathlib import Path

OUTPUT = Path("C:/Users/Jeremy/swarm/output").resolve()
LEDGER = Path("C:/Users/Jeremy/swarm/ledger")


def fail(msg):
    print(json.dumps({"error": msg}))
    sys.exit(1)


def main():
    if len(sys.argv) != 5:
        print('usage: file_deliverable.py "<source>" "<destination folder>" "<new name>" <project>')
        sys.exit(2)
    src, dest_dir, name, project = Path(sys.argv[1]).resolve(), Path(sys.argv[2]), sys.argv[3], sys.argv[4]
    if OUTPUT not in src.parents:
        fail(f"source must be under {OUTPUT.as_posix()}")
    if not src.is_file():
        fail(f"source not found: {src.as_posix()}")
    if not re.fullmatch(r"[\w .,()&'-]+", project) or any(c in name for c in '\\/:*?"<>|'):
        fail("bad project or file name")
    if not dest_dir.is_dir():
        fail(f"destination folder not found: {dest_dir.as_posix()}")
    target = dest_dir / name
    stem, ext, n = target.stem, target.suffix, 2
    while target.exists():
        target = dest_dir / f"{stem}_{n}{ext}"
        n += 1
    try:
        shutil.copy2(src, target)
    except OSError as e:
        fail(f"copy failed: {e}")
    entry = {"file": target.name, "source": src.as_posix(), "destination": target.as_posix(),
             "renamed_from_conflict": target.name != name, "timestamp": datetime.now().isoformat(timespec="seconds")}
    LEDGER.mkdir(parents=True, exist_ok=True)
    ledger = LEDGER / f"{project}.deliverables.json"
    data = json.loads(ledger.read_text(encoding="utf-8")) if ledger.exists() else []
    data.append(entry)
    ledger.write_text(json.dumps(data, indent=1), encoding="utf-8")
    print(json.dumps(entry))


if __name__ == "__main__":
    main()
