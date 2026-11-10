"""Move files the orchestrator downloaded with Chrome (ACC, Procore, a download link) into a run's inputs folder.

    python ~/swarm/tools/intake_downloads.py list [--since 30] [--from <folder>]
    python ~/swarm/tools/intake_downloads.py move <dest folder> --source "<url or 'ACC: <folder path>'>"
           --file "<name>" [--file "<name>" ...] [--from <folder>] [--version "<v3 / 2026-11-09>"]

list: files in the download folder modified in the last --since minutes (default 30), newest first, with size and
  whether the download is still in progress (.crdownload / .tmp / zero bytes). Use it to match what Jeremy approved.
move: moves each named file from the download folder (default ~/Downloads, Chrome's default) to <dest folder>
  (the folder doc-scout scans: pdf_intake.py reads only the top level, so no sub-folders). Never overwrites: a name
  that exists gets _2, _3, ... before the extension (reported). Refuses partial downloads. Appends one record per file
  to <dest folder>/SOURCES.json: {"file","original_name","source","version","bytes","sha256","downloaded_at",
  "moved_at"} so the letter and retro can say where every input came from.
Prints JSON. Exit 0 = everything moved, 1 = something refused or missing, 2 = usage.
"""
import hashlib
import json
import os
import shutil
import sys
import time
from datetime import datetime
from pathlib import Path

PARTIAL = (".crdownload", ".tmp", ".part", ".download")


def opt(args, name, default=None, multi=False):
    vals = []
    while name in args:
        i = args.index(name)
        if i + 1 >= len(args):
            usage()
        vals.append(args[i + 1])
        del args[i:i + 2]
    if multi:
        return vals
    return vals[-1] if vals else default


def usage():
    lines = __doc__.strip().splitlines()
    print("\n".join(l.strip() for l in lines[2:5]))
    sys.exit(2)


def downloads_dir(arg):
    return Path(os.path.abspath(os.path.expanduser(arg or "~/Downloads")))


def is_partial(p):
    return p.suffix.lower() in PARTIAL or p.stat().st_size == 0


def free_name(dest, name):
    target = dest / name
    stem, suf, k = target.stem, target.suffix, 2
    while target.exists():
        target = dest / f"{stem}_{k}{suf}"
        k += 1
    return target


def sha256(p):
    h = hashlib.sha256()
    with open(p, "rb") as f:
        for chunk in iter(lambda: f.read(1 << 20), b""):
            h.update(chunk)
    return h.hexdigest()


def stamp(t):
    return datetime.fromtimestamp(t).isoformat(timespec="seconds")


def cmd_list(args):
    since = float(opt(args, "--since", "30"))
    src = downloads_dir(opt(args, "--from"))
    if args:
        usage()
    if not src.is_dir():
        print(json.dumps({"error": f"no folder {src.as_posix()}"}))
        sys.exit(1)
    cutoff = time.time() - since * 60
    rows = [p for p in src.iterdir() if p.is_file() and p.stat().st_mtime >= cutoff]
    rows.sort(key=lambda p: p.stat().st_mtime, reverse=True)
    print(json.dumps({"folder": src.as_posix(), "since_min": since, "files": [
        {"name": p.name, "bytes": p.stat().st_size, "modified": stamp(p.stat().st_mtime),
         "in_progress": is_partial(p)} for p in rows]}, indent=1))


def cmd_move(args):
    source = opt(args, "--source")
    version = opt(args, "--version")
    names = opt(args, "--file", multi=True)
    src = downloads_dir(opt(args, "--from"))
    if len(args) != 1 or not source or not names:
        usage()
    dest = Path(os.path.abspath(os.path.expanduser(args[0])))
    dest.mkdir(parents=True, exist_ok=True)
    ledger = dest / "SOURCES.json"
    records = []
    if ledger.is_file():
        try:
            records = json.loads(ledger.read_text(encoding="utf-8-sig"))
        except ValueError:
            print(json.dumps({"error": f"{ledger.as_posix()} is not valid JSON; fix it before moving more files"}))
            sys.exit(1)
    out, ok = [], True
    for name in names:
        p = src / name
        if not p.is_file():
            out.append({"file": name, "status": "missing"}); ok = False; continue
        if is_partial(p):
            out.append({"file": name, "status": "in_progress"}); ok = False; continue
        downloaded_at = stamp(p.stat().st_mtime)
        target = free_name(dest, p.name)
        shutil.move(str(p), str(target))
        rec = {"file": target.name, "original_name": name, "source": source, "version": version,
               "bytes": target.stat().st_size, "sha256": sha256(target), "downloaded_at": downloaded_at,
               "moved_at": datetime.now().isoformat(timespec="seconds")}
        records.append(rec)
        out.append({**rec, "status": "moved", "renamed": target.name != name, "path": target.as_posix()})
    ledger.write_text(json.dumps(records, indent=1), encoding="utf-8")
    print(json.dumps({"dest": dest.as_posix(), "ledger": ledger.as_posix(), "files": out}, indent=1))
    sys.exit(0 if ok else 1)


def main():
    args = sys.argv[1:]
    if not args or args[0] not in ("list", "move"):
        usage()
    (cmd_list if args[0] == "list" else cmd_move)(args[1:])


if __name__ == "__main__":
    main()
