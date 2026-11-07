"""Content check of a task.json against the model index cache, before any writer runs (plan -> VALIDATE -> execute).

    python C:/Users/Jeremy/swarm/tools/check_task.py <task.json> [<task.json> ...]

validate.py checks the SHAPE (schemas/task.schema.json); this checks the CONTENT against cache/<app>-index.json:
  - the index is for the task's document and is not invalidated;
  - every target id, and every 6+ digit id mentioned in "change", is in the index; a sheet/level/grid/revision id that
    is not there is an ERROR listing valid alternatives of that category; an id in a category the index does not
    list (partial index: text notes, plan views, MEP elements) is UNVERIFIABLE (warning: the scout must confirm it).
Exit 0 = no errors (warnings allowed), 1 = errors, 2 = usage / unreadable file.
"""
import json
import re
import sys
from pathlib import Path

SWARM = Path("C:/Users/Jeremy/swarm")
APP_INDEX = {"revit": "revit-index.json", "acad": "acad-index.json"}
# which index lists an operation's targets should be in (by operation-name keywords)
OP_CATEGORY = [("sheet", "sheets"), ("revision-cloud", "revision_clouds"), ("revision", "revisions"),
               ("level", "levels"), ("grid", "grids"), ("annotation", "generic_annotations")]


def load(p):
    with open(p, encoding="utf-8-sig") as f:
        return json.load(f)


def flatten(index):
    """id -> (category, label) from every list in the index."""
    out = {}
    for cat, val in index.items():
        if not isinstance(val, list):
            continue
        for row in val:
            if isinstance(row, int):
                out[row] = (cat, "")
            elif isinstance(row, list):
                ids = [x for x in row if isinstance(x, int)]
                label = " / ".join(str(x) for x in row if isinstance(x, str) and x)
                for i in ids:
                    out[i] = (cat, label)
            elif isinstance(row, dict):
                i = row.get("id")
                if isinstance(i, int):
                    out[i] = (cat, row.get("name") or row.get("number") or "")
    return out


def check(task_path):
    errors, warnings, found = [], [], []
    task = load(task_path)
    # item 10 (2026-11-06): task ids carry the project's prefix, so two projects never share a manifest name
    try:
        proj = load(SWARM / "manifests" / "project.json")
    except (OSError, ValueError):
        proj = {}
    want = proj.get("task_prefix")
    tid = str(task.get("task_id", ""))
    if want and not tid.startswith(want + "-"):
        errors.append(f"task_id '{tid}' must start with '{want}-' (task_prefix of {proj.get('project_number', '?')} "
                      "in swarm/manifests/project.json)")
    elif not want:
        warnings.append("project.json has no task_prefix: set it at Step 0 (e.g. C22) so task ids stay unique")
    app = task.get("app")
    idx_path = SWARM / "cache" / APP_INDEX.get(app, "?")
    if not idx_path.exists():
        return [f"no index {idx_path.name} for app {app!r}: run the {app} scout first"], [], []
    index = load(idx_path)
    doc = index.get("document", "")
    if index.get("invalidated"):
        warnings.append(f"index {idx_path.name} is invalidated (a writer ran since it was built): ids may have changed")
    if doc and doc not in task.get("change", ""):
        warnings.append(f"index is for document '{doc}', which the task's change text does not name")
    ids = flatten(index)
    op = task.get("operation", "")
    expect_cat = next((c for k, c in OP_CATEGORY if k in op), None)
    targets = [t for t in task.get("target_ids", []) if isinstance(t, int) or str(t).isdigit()]
    mentioned = [int(x) for x in re.findall(r"\b(\d{6,9})\b", task.get("change", ""))]
    partial = bool(index.get("partial"))
    for tid in dict.fromkeys([int(t) for t in targets] + mentioned):
        role = "target" if tid in [int(t) for t in targets] else "mentioned"
        if tid in ids:
            cat, label = ids[tid]
            found.append(f"{role} {tid}: {cat}{' ' + label if label else ''}")
            if role == "target" and expect_cat and cat != expect_cat:
                warnings.append(f"target {tid} is a {cat}, but operation '{op}' implies {expect_cat}")
        elif expect_cat and role == "target" and isinstance(index.get(expect_cat), list):
            alts = [f"{i} ({lbl})" if lbl else str(i) for i, (c, lbl) in ids.items() if c == expect_cat][:8]
            errors.append(f"target {tid} not found in {expect_cat} of '{doc}'; valid {expect_cat}: {', '.join(alts)}"
                          + (" ..." if sum(1 for c, _ in ids.values() if c == expect_cat) > 8 else ""))
        else:
            why = "index is partial (" + index.get("partial_note", "")[:80] + "...)" if partial else "category not indexed"
            warnings.append(f"{role} {tid} UNVERIFIABLE from the index ({why}): the scout must confirm it exists")
    return errors, warnings, found


def main():
    if len(sys.argv) < 2:
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    bad = 0
    for p in sys.argv[1:]:
        try:
            errors, warnings, found = check(p)
        except (OSError, ValueError) as e:
            print(f"ERROR {p}: {e}")
            sys.exit(2)
        status = "FAIL" if errors else ("WARN" if warnings else "OK")
        print(f"{status} {Path(p).name}: {len(found)} found, {len(warnings)} warning(s), {len(errors)} error(s)")
        for e in errors:
            print("  ERROR " + e)
        for w in warnings:
            print("  WARN  " + w)
        for f in found:
            print("  ok    " + f)
        bad += bool(errors)
    sys.exit(1 if bad else 0)


if __name__ == "__main__":
    main()
