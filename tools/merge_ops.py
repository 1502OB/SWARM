"""Merge per-agent attempt logs into the shared ops log of a project (LESSONS A48).

    python ~/swarm/tools/merge_ops.py <project-folder> [--base M18-SAVE] [--dry-run]

Agents have Write but not Edit, so none of them may rewrite a shared JSON log. Each one writes its own
manifests/<base>.attempt<N>.ops.json (a JSON list of entries, or one JSON object = one entry); the orchestrator runs
this tool, which:
  - reads <project-folder>/manifests/<base>.ops.json (a JSON list; created if missing),
  - appends every entry of every <base>.attempt*.ops.json in file-mtime order (oldest first), skipping an entry that
    is already in the log (full JSON equality), so a second run adds nothing,
  - writes the log back (UTF-8, indent 1; the previous file is kept as <base>.ops.json.bak), then
  - moves each merged attempt file to manifests/merged/ (never deletes; an existing name there gets a _2, _3 suffix).
Without --base, every base that has attempt files is merged. An attempt file that is not valid JSON is left in place
and reported. Prints one JSON line: merged files, entries added, entries skipped, errors. Exit 1 if any file failed.
"""
import json
import os
import re
import shutil
import sys
from pathlib import Path

ATTEMPT = re.compile(r"^(?P<base>.+?)\.attempt\d+[a-z]?\.ops\.json$", re.IGNORECASE)


def load(p: Path):
    with open(p, encoding="utf-8-sig") as f:  # tolerate a BOM (PowerShell-written files)
        return json.load(f)


def canon(x) -> str:
    return json.dumps(x, sort_keys=True, ensure_ascii=False)


def free_name(folder: Path, name: str) -> Path:
    dst, k = folder / name, 1
    while dst.exists():
        k += 1
        stem, _, ext = name.partition(".")
        dst = folder / f"{stem}_{k}.{ext}"
    return dst


def merge(project: Path, base: str | None = None, dry_run: bool = False) -> dict:
    man = project / "manifests"
    if not man.is_dir():
        raise FileNotFoundError(f"no manifests folder in {project}")
    groups: dict[str, list[Path]] = {}
    for p in man.iterdir():
        m = ATTEMPT.match(p.name)
        if p.is_file() and m and (base is None or m.group("base").lower() == base.lower()):
            groups.setdefault(m.group("base"), []).append(p)
    report = {"project": project.as_posix(), "bases": {}, "errors": []}
    for b, files in sorted(groups.items()):
        files.sort(key=lambda p: (p.stat().st_mtime, p.name))
        log_path = man / f"{b}.ops.json"
        log = []
        if log_path.exists():
            log = load(log_path)
            if not isinstance(log, list):
                report["errors"].append(f"{log_path.name} is not a JSON list; not merged")
                continue
        seen = {canon(e) for e in log}
        added = skipped = 0
        done = []
        for p in files:
            try:
                data = load(p)
            except (OSError, ValueError) as e:
                report["errors"].append(f"{p.name}: {e}")
                continue
            for e in data if isinstance(data, list) else [data]:
                c = canon(e)
                if c in seen:
                    skipped += 1
                    continue
                seen.add(c)
                log.append(e)
                added += 1
            done.append(p)
        if not dry_run and done:
            if log_path.exists():
                shutil.copyfile(log_path, log_path.with_name(log_path.name + ".bak"))
            tmp = log_path.with_name(log_path.name + ".tmp")
            tmp.write_text(json.dumps(log, indent=1, ensure_ascii=False) + "\n", encoding="utf-8")
            os.replace(tmp, log_path)
            dest = man / "merged"
            dest.mkdir(exist_ok=True)
            for p in done:
                shutil.move(str(p), str(free_name(dest, p.name)))
        report["bases"][b] = {"log": log_path.name, "files": [p.name for p in done], "added": added,
                              "skipped_duplicates": skipped, "entries": len(log)}
    return report


def main():
    args = sys.argv[1:]
    base, dry = None, "--dry-run" in args
    args = [a for a in args if a != "--dry-run"]
    if "--base" in args:
        i = args.index("--base")
        if i + 1 >= len(args):
            print(__doc__.strip().splitlines()[2].strip())
            sys.exit(2)
        base = args[i + 1]
        del args[i:i + 2]
    if len(args) != 1:
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    try:
        rep = merge(Path(os.path.abspath(args[0])), base, dry)
    except (OSError, ValueError) as e:
        print(json.dumps({"error": str(e)}))
        sys.exit(1)
    rep["dry_run"] = dry
    print(json.dumps(rep))
    sys.exit(1 if rep["errors"] else 0)


if __name__ == "__main__":
    main()
