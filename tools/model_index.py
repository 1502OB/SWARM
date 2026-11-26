"""Store model index scopes by document identity and run.

    python tools/model_index.py show <app> <document_id> <run_id> [scope ...]
    python tools/model_index.py merge <app> <document_id> <run_id> <scout.json>
    python tools/model_index.py invalidate <app> <document_id> <run_id> [scope ...] [--reason write|uncertain|outside|reopen]

The orchestrator is the sole cache writer, while it holds the app's queue slot (run_queue.py finish does the
merge/invalidate for it with --scout / --invalidate / --invalidate-all).

Invalidation reasons: "write" = a verified in-run write by this swarm with known impact scopes (the default when
scopes are given); "uncertain" / "outside" / "reopen" = anything else (the default when no scope is given).
invalidated_ns keeps the latest invalidation of any reason per scope; invalidated_hard_ns keeps only the
non-write ones; stale_scopes keeps the entries a "write" removed, until a non-write invalidation or a fresh merge.
check_task.py uses these to let a "live_reresolve" task pass with a WARN when only in-run writes made it stale.
"""
import argparse
import hashlib
import json
import os
import sys
import time
from pathlib import Path
from tempfile import NamedTemporaryFile

sys.path.insert(0, str(Path(__file__).absolute().parent.parent))
from swarm_paths import SWARM  # noqa: E402

APPS = ("revit", "acad")
REASONS = ("write", "uncertain", "outside", "reopen")


def identity(value):
    return str(value or "").strip().replace("\\", "/").casefold()


def cache_path(app, document_id):
    key = identity(document_id)
    if app not in APPS or not key:
        raise ValueError("app and stable document identity are required")
    digest = hashlib.sha256(key.encode("utf-8")).hexdigest()
    return SWARM / "cache" / "model_indexes" / app / f"{digest}.json"


def load_json(path):
    with Path(path).open(encoding="utf-8-sig") as source:
        return json.load(source)


def save_json(path, data):
    path.parent.mkdir(parents=True, exist_ok=True)
    with NamedTemporaryFile("w", encoding="utf-8", dir=path.parent, delete=False) as out:
        tmp = Path(out.name)
        try:
            json.dump(data, out, indent=2)
            out.write("\n")
            out.close()
            os.replace(tmp, path)
        finally:
            tmp.unlink(missing_ok=True)


def read_index(app, document_id, run_id):
    key = identity(document_id)
    try:
        data = load_json(cache_path(app, key))
        if isinstance(data, dict) and (data.get("app"), data.get("document_id"), data.get("run_id")) == (
                app, key, run_id) and isinstance(data.get("scopes"), dict):
            return data
    except (OSError, ValueError):
        pass
    return {"app": app, "document_id": key, "run_id": run_id, "scopes": {}, "invalidated_ns": {},
            "invalidated_hard_ns": {}, "invalidated_reason": {}, "stale_scopes": {}}


def scope_key(value):
    key = str(value).strip().casefold()
    if not key or any(not part.strip() for part in key.split(":")):
        raise ValueError("scope names and components must be nonempty")
    return key


def overlapping(left, right):
    return left == right or left.startswith(right + ":") or right.startswith(left + ":")


def lookup_scopes(scopes, requested):
    wanted = [scope_key(scope) for scope in requested]
    return {name: row for name, row in scopes.items()
            if not wanted or any(name == scope or name.startswith(scope + ":") for scope in wanted)}


def merge_index(app, document_id, run_id, scout, observed_ns=None):
    if (scout.get("app"), identity(scout.get("document_id")), scout.get("run_id")) != (
            app, identity(document_id), run_id):
        raise ValueError("scout app, live document identity, or run does not match the cache")
    scopes = scout.get("index_scopes")
    if not isinstance(scopes, dict) or not scopes:
        raise ValueError("scout has no index_scopes to merge")
    data = read_index(app, document_id, run_id)
    for raw_name, record in scopes.items():
        name = scope_key(raw_name)
        if observed_ns is not None and observed_ns <= last_invalidation(data, [name]):
            raise ValueError(f"{name}: scout predates the latest write affecting the scope")
        if not isinstance(record, dict) or not isinstance(record.get("entries"), list) or not isinstance(
                record.get("complete"), bool):
            raise ValueError(f"{name}: expected entries array and complete boolean")
        data["scopes"][name] = {"entries": record["entries"], "complete": record["complete"]}
        data.get("stale_scopes", {}).pop(name, None)
    save_json(cache_path(app, document_id), data)
    return data


def invalidate(app, document_id, run_id, scopes=(), reason=None):
    affected = [scope_key(scope) for scope in scopes]
    reason = reason or ("write" if affected else "uncertain")
    if reason not in REASONS:
        raise ValueError(f"invalidation reason must be one of {', '.join(REASONS)}")
    data = read_index(app, document_id, run_id)
    invalidated = data.setdefault("invalidated_ns", {})
    # Files written before reasons existed: treat every earlier invalidation as non-write (conservative).
    hard = data.setdefault("invalidated_hard_ns", dict(invalidated))
    reasons = data.setdefault("invalidated_reason", {})
    stale = data.setdefault("stale_scopes", {})
    remove = [name for name in data["scopes"]
              if not affected or any(overlapping(name, scope) for scope in affected)]
    now = time.time_ns()
    for name in remove:
        row = data["scopes"].pop(name)
        if reason == "write":
            stale[name] = {**row, "invalidated_ns": now}
    if reason != "write":
        for name in [n for n in stale if not affected or any(overlapping(n, scope) for scope in affected)]:
            del stale[name]
    if not affected:
        invalidated.clear()
        reasons.clear()
        if reason != "write":
            hard.clear()
    for name in affected or ["*"]:
        invalidated[name] = now
        reasons[name] = reason
        if reason != "write":
            hard[name] = now
    save_json(cache_path(app, document_id), data)
    return remove


def last_invalidation(data, requested, hard=False):
    """Latest invalidation touching the requested scopes; hard=True counts only non-write reasons."""
    stamps = data.get("invalidated_ns", {})
    if hard:
        stamps = data.get("invalidated_hard_ns", stamps)
    return max((stamp for name, stamp in stamps.items()
                if name == "*" or not requested or any(overlapping(name, scope) for scope in requested)), default=0)


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("command", choices=("show", "merge", "invalidate"))
    parser.add_argument("app", choices=APPS)
    parser.add_argument("document_id")
    parser.add_argument("run_id")
    parser.add_argument("args", nargs="*")
    parser.add_argument("--reason", choices=REASONS,
                        help="invalidate only; default write with scopes, uncertain without")
    args = parser.parse_args()
    key = (args.app, args.document_id, args.run_id)
    try:
        path = cache_path(*key[:2])
        if args.command == "show":
            data = read_index(*key)
            data["scopes"] = lookup_scopes(data["scopes"], args.args)
        elif args.command == "merge":
            if len(args.args) != 1:
                parser.error("merge takes one scout JSON path")
            scout = Path(args.args[0])
            data = {"scopes": list(merge_index(*key, load_json(scout), scout.stat().st_mtime_ns)["scopes"])}
        else:
            data = {"invalidated": invalidate(*key, args.args, args.reason)}
        print(json.dumps({"path": path.as_posix(), **data}))
    except (OSError, ValueError) as error:
        parser.exit(2, f"{error}\n")


if __name__ == "__main__":
    main()
