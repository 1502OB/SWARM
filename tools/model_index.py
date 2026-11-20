"""Store model index scopes by document identity and run.

    python tools/model_index.py show <app> <document_id> <run_id> [scope ...]
    python tools/model_index.py merge <app> <document_id> <run_id> <scout.json>
    python tools/model_index.py invalidate <app> <document_id> <run_id> [scope ...]

The orchestrator is the sole cache writer, while it holds the app's queue slot.
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
    return {"app": app, "document_id": key, "run_id": run_id, "scopes": {}, "invalidated_ns": {}}


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
    save_json(cache_path(app, document_id), data)
    return data


def invalidate(app, document_id, run_id, scopes=()):
    data = read_index(app, document_id, run_id)
    affected = [scope_key(scope) for scope in scopes]
    remove = [name for name in data["scopes"]
              if not affected or any(overlapping(name, scope) for scope in affected)]
    for name in remove:
        del data["scopes"][name]
    now = time.time_ns()
    invalidated = data.setdefault("invalidated_ns", {})
    if not affected:
        invalidated.clear()
    for name in affected or ["*"]:
        invalidated[name] = now
    save_json(cache_path(app, document_id), data)
    return remove


def last_invalidation(data, requested):
    return max((stamp for name, stamp in data.get("invalidated_ns", {}).items()
                if name == "*" or not requested or any(overlapping(name, scope) for scope in requested)), default=0)


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("command", choices=("show", "merge", "invalidate"))
    parser.add_argument("app", choices=APPS)
    parser.add_argument("document_id")
    parser.add_argument("run_id")
    parser.add_argument("args", nargs="*")
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
            data = {"invalidated": invalidate(*key, args.args)}
        print(json.dumps({"path": path.as_posix(), **data}))
    except (OSError, ValueError) as error:
        parser.exit(2, f"{error}\n")


if __name__ == "__main__":
    main()
