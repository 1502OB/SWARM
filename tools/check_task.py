"""Check task targets against the current document's scoped index or a targeted scout.

    python tools/check_task.py <task.json> [<task.json> ...] [--scout <task.scout.json>]

Exit 0 = checked, 1 = task/evidence conflict, 2 = tool/input problem, 3 = fresh targeted scout needed.
"""
import argparse
import re
from pathlib import Path

from model_index import APPS, SWARM, identity, last_invalidation, load_json, lookup_scopes, read_index, scope_key


def target_key(value):
    return str(value).strip().casefold()


def indexed_targets(scopes):
    found = {}
    for name, scope in scopes.items():
        for row in scope["entries"]:
            value = row.get("id") if isinstance(row, dict) else row
            if isinstance(value, (int, str)):
                found.setdefault(target_key(value), name)
    return found


def scout_targets(scout, task, document_id, project):
    if not scout:
        return set(), False
    if any(scout.get(key) != task.get(key) for key in ("run_id", "task_id", "app")):
        raise ValueError("scout run_id, task_id, or app differs from the task")
    key = identity(scout.get("document_id"))
    if not key or (document_id and key != document_id):
        raise ValueError("scout live document identity differs from the task's document")
    if not document_id:
        expected_title = project.get(f"{task['app']}_document")
        if not expected_title or target_key(scout.get("document")) != target_key(expected_title):
            raise ValueError("supply a stable document_id or confirm the exact project document with a fresh scout")
    confirmed = scout.get("confirmed_target_ids")
    if not isinstance(confirmed, list):
        raise ValueError("scout must explicitly list confirmed_target_ids, even when empty")
    return {target_key(value) for value in confirmed}, True


def check(task_path, scout_path=None):
    errors, warnings, found = [], [], []
    task = load_json(task_path)
    try:
        project = load_json(SWARM / "manifests" / "project.json")
    except (OSError, ValueError):
        project = {}
    prefix = project.get("task_prefix")
    task_id = task.get("task_id", "")
    if prefix and not task_id.startswith(prefix + "-"):
        errors.append(f"task_id '{task_id}' must start with '{prefix}-' for the active project")
    elif not prefix:
        warnings.append("active project has no task_prefix; set it at Step 0")

    app = task.get("app")
    if app not in APPS:
        return errors + [f"unsupported task app: {app!r}"], warnings, found, []
    document_id = identity(task.get("document_id") or project.get(f"{app}_document_id"))
    run_id = task.get("run_id")
    if not run_id:
        return errors + ["task requires run_id"], warnings, found, []

    # Only use scope snapshots from this run and the exact intended document.
    index = read_index(app, document_id, run_id) if document_id else {"scopes": {}}
    requested = [scope_key(scope) for scope in task.get("lookup_scopes", [])]
    scopes = lookup_scopes(index["scopes"], requested)
    known = indexed_targets(scopes)
    if scout_path is None:
        candidate = Path(task_path).with_name(f"{task_id}.scout.json")
        if candidate.is_file():
            candidate_data = load_json(candidate)
            if "confirmed_target_ids" in candidate_data and candidate_data.get("run_id") == run_id:
                scout_path = candidate
    if scout_path and Path(scout_path).stat().st_mtime_ns <= last_invalidation(index, requested):
        warnings.append("targeted scout predates a write or uncertain model change")
        scout_path = None
    try:
        confirmed, identity_checked = scout_targets(load_json(scout_path) if scout_path else None,
                                                     task, document_id, project)
    except ValueError as error:
        return errors + [str(error)], warnings, found, []

    needs_scout = []
    targets = list(dict.fromkeys(target_key(t) for t in task.get("target_ids", [])))
    for target in targets:
        if target in confirmed:
            found.append(f"target {target}: current scout")
        elif target in known:
            found.append(f"target {target}: cached {known[target]}")
        elif requested and all(scopes.get(name, {}).get("complete") for name in requested):
            errors.append(f"target {target} absent from complete scopes {', '.join(requested)}")
        else:
            needs_scout.append(f"target {target} has no current evidence")
    if not scopes and not identity_checked and not needs_scout:
        needs_scout.append("no current document evidence; have the scout confirm the live document")
    for value in dict.fromkeys(re.findall(r"\b(\d{6,9})\b", task.get("change", ""))):
        if value in known and value not in targets:
            found.append(f"mentioned {value}: cached {known[value]}")
    return errors, warnings, found, needs_scout


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("tasks", nargs="+")
    parser.add_argument("--scout")
    args = parser.parse_args()
    if args.scout and len(args.tasks) != 1:
        parser.error("--scout applies to one task")

    failures = set()
    for path in args.tasks:
        try:
            errors, warnings, found, needs_scout = check(path, args.scout)
        except (OSError, ValueError, TypeError) as error:
            print(f"ERROR {path}: {error}")
            failures.add(2)
            continue
        status = "FAIL" if errors else "NEEDS_SCOUT" if needs_scout else "WARN" if warnings else "OK"
        print(f"{status} {Path(path).name}: {len(found)} found")
        for label, items in (("ERROR", errors), ("WARN", warnings), ("SCOUT", needs_scout), ("ok", found)):
            for item in items:
                print(f"  {label}  {item}")
        if errors:
            failures.add(1)
        elif needs_scout:
            failures.add(3)
    return next((code for code in (2, 1, 3) if code in failures), 0)


if __name__ == "__main__":
    raise SystemExit(main())
