"""Plan and time the ready stages of a swarm run. The main session owns the queue and dispatches agents.

    python tools/run_queue.py <queue.json> init <plan.json> [--project-folder F] [--project-number N]
                                              [--revit-document-id ID] [--acad-document-id ID]
    python tools/run_queue.py <queue.json> project [same four overrides]   (show / correct the stored project)
    python tools/run_queue.py <queue.json> add <plan.json>
    python tools/run_queue.py <queue.json> ready
    python tools/run_queue.py <queue.json> start <stage-id>
    python tools/run_queue.py <queue.json> finish <stage-id> <pass|fail|uncertain> [--scout <scout.json>]
                                              [--invalidate <scope> ...] [--invalidate-all]
    python tools/run_queue.py <queue.json> resolve <stage-id> <pass|fail>
    python tools/run_queue.py <queue.json> retry <stage-id> [--after <stage-id> ...]
    python tools/run_queue.py <queue.json> fail-cycle <task-id> --cause "<one line>" [--landed yes|no|maybe]
                                              [--approved-extra "<Jeremy's words>"]
    python tools/run_queue.py <queue.json> cycles
    python tools/run_queue.py <queue.json> report

init stores project_folder, project_number, revit_document_id and acad_document_id from swarm/manifests/project.json
(flags override) and writes <project_folder>/manifests/project.json (the project's own copy) if it is missing.
start re-checks the shared pointer swarm/manifests/project.json: if its project_number, project_folder or run_id
is not this queue's, it backs the foreign pointer up as project.json.<their number>.bak and restores the project's
own copy with this run_id ("pointer_restored" in the output); with no own copy it refuses (exit 2).
finish does the model-index work (app = the stage lane, revit/acad only): --scout merges the scout's index_scopes,
--invalidate invalidates the named scopes (reason write), --invalidate-all and every `uncertain` outcome invalidate
the whole document (reason uncertain). Merge runs before invalidate. The attempt records it as "index".
fail-cycle counts failures per task (dry-run-driven redesigns too) and prints the next step from DEBUG_LADDER.
Exit 0 = ok, 2 = input/state error (nothing changed), 4 = fail-cycle says stop and ask Jeremy.
"""
import argparse
from collections import Counter
from graphlib import TopologicalSorter
import json
import re
import shutil
import time
from pathlib import Path

import model_index
from model_index import SWARM, identity, load_json, save_json

BRIDGES = {"revit", "acad"}
LANES = BRIDGES | {"offline", "tracker", "human"}
AGENT_LIMIT = 20
PROJECT_FIELDS = ("project_folder", "project_number", "revit_document_id", "acad_document_id")

DEBUG_LADDER = (
    {"failure": 1, "next": "debugger-c1", "model": "sonnet",
     "note": "script-debugger (sonnet), cycle 1: writes scripts/{task}.v<n>.* and manifests/{task}.debug.json"},
    {"failure": 2, "next": "debugger-c2", "model": "opus",
     "note": "script-debugger with model override opus, ChatGPT second opinion (mcp__chatgpt__chatgpt, logged in "
             "manifests/{task}.chatgpt.json), writes manifests/{task}.debug.c2.json"},
    {"failure": 3, "next": "stop-ask-jeremy", "model": None,
     "note": "ladder exhausted: stop and show Jeremy the causes; mark {task} FAIL unless he approves one more "
             "cycle (then re-run fail-cycle with --approved-extra \"<his words>\")"},
)
DEBUG_EXTRA = {"next": "debugger-extra", "model": "opus",
               "note": "Jeremy approved an extra cycle: script-debugger with model override opus, writes "
                       "manifests/{task}.debug.c{n}.json"}
DEBUG_LADDER_DOC = """Debug ladder: the single source of truth is DEBUG_LADDER, applied by `run_queue.py fail-cycle`.

Call fail-cycle on EVERY failure of a task: compile error, dry run failing its GO conditions or forcing a redesign,
writer failed/rolled_back, verifier FAIL. Failure 1 -> debugger-c1 (script-debugger, sonnet). Failure 2 ->
debugger-c2 (script-debugger with model override opus, ChatGPT second opinion, writes <task>.debug.c2.json).
Failure 3 and later -> stop-ask-jeremy (exit 4), unless --approved-extra records Jeremy's words for this failure
(then debugger-extra, opus, writes <task>.debug.c<n>.json). Given right after a stop, --approved-extra answers that
stop (same failure number, no new count). --landed yes|maybe adds "needs_jeremy_go": true:
Jeremy's go before any writer runs the next version.
"""


def new_queue(plan, now):
    stages = {}
    for entry in plan["stages"]:
        name, task, lane = (entry[key] for key in ("id", "task", "lane"))
        after = entry.get("after", [])
        if not all(isinstance(v, str) and v for v in (name, task, lane)):
            raise ValueError("stage id, task and lane must be nonempty strings")
        if name in stages or lane not in LANES:
            raise ValueError(f"duplicate stage or unknown lane: {name} ({lane})")
        if not isinstance(after, list) or any(not isinstance(d, str) for d in after):
            raise ValueError(f"{name}: after must be a list of stage IDs")
        stages[name] = {"task": task, "lane": lane, "after": after, "attempts": [], "status": "pending",
                        "ready_at": None}
    if not stages or not isinstance(plan["run_id"], str) or not plan["run_id"].strip():
        raise ValueError("plan needs a run ID and stages")
    missing = {d for stage in stages.values() for d in stage["after"] if d not in stages}
    if missing:
        raise ValueError(f"unknown dependencies: {', '.join(sorted(missing))}")
    TopologicalSorter({name: stage["after"] for name, stage in stages.items()}).prepare()
    queue = {"run_id": plan["run_id"], "stages": stages}
    mark_ready(queue, now)
    return queue


def mark_ready(queue, now):
    stages = queue["stages"]
    for stage in stages.values():
        if stage["status"] == "pending" and stage["ready_at"] is None and all(
                stages[dep]["status"] == "pass" for dep in stage["after"]):
            stage["ready_at"] = now


def available(queue):
    stages = queue["stages"]
    busy = {stage["lane"] for stage in stages.values()
            if stage["status"] in ("running", "uncertain") and stage["lane"] != "offline"}
    slots = AGENT_LIMIT - sum(stage["lane"] in BRIDGES | {"offline"} and stage["status"] == "running"
                              for stage in stages.values())
    active_apps = {stage["lane"] for stage in stages.values()
                   if stage["lane"] in BRIDGES and stage["status"] in ("pending", "running", "uncertain")}
    for name, stage in sorted(stages.items(), key=lambda pair: pair[1]["lane"] not in BRIDGES):
        lane = stage["lane"]
        reserved = len(active_apps - busy) if lane == "offline" else 0
        if stage["status"] == "pending" and stage["ready_at"] is not None and lane not in busy and (
                slots > reserved or lane in {"human", "tracker"}):
            yield name, stage
            if lane != "offline":
                busy.add(lane)
            if lane in BRIDGES | {"offline"}:
                slots -= 1


def transition(queue, command, name, outcome, now, after=()):
    stages = queue["stages"]
    stage = stages[name]
    state = stage["status"]
    if command == "start" and name in dict(available(queue)):
        stage["status"] = "running"
        stage["attempts"].append({"ready_at": stage["ready_at"], "started_at": now})
    elif command == "finish" and state == "running":
        if outcome == "uncertain" and stage["lane"] not in BRIDGES:
            raise ValueError("only bridge stages can hold a lane for reconciliation")
        stage["status"] = outcome
        stage["attempts"][-1].update(finished_at=now, outcome=outcome)
    elif command == "resolve" and state == "uncertain":
        stage["status"] = outcome
        stage["attempts"][-1].update(resolved_at=now, resolution=outcome)
    elif command == "retry" and state == "fail":
        dependencies = list(dict.fromkeys([*stage["after"], *after]))
        if any(dep not in stages for dep in dependencies):
            raise ValueError("retry dependency is missing")
        graph = {item: row["after"] for item, row in stages.items()}
        graph[name] = dependencies
        TopologicalSorter(graph).prepare()
        stage.update(status="pending", ready_at=None, after=dependencies)
    else:
        raise ValueError(f"{name}: cannot {command} from {state}")
    mark_ready(queue, now)


# ---- project pointer (swarm/manifests/project.json is shared by every session)
def pointer_path():
    return SWARM / "manifests" / "project.json"


def project_copy_path(project):
    """<project_folder>/manifests/project.json; project_folder ("swarm/<name>") is relative to SWARM's parent."""
    folder = (project or {}).get("project_folder")
    return SWARM.parent / folder / "manifests" / "project.json" if folder else None


def same_folder(left, right):
    return bool(left) and identity(left).rstrip("/") == identity(right).rstrip("/")


def read_pointer():
    try:
        data = load_json(pointer_path())
        return data if isinstance(data, dict) else {}
    except (OSError, ValueError):
        return {}


def project_from_pointer(overrides):
    pointer = read_pointer()
    project = {key: pointer.get(key) for key in PROJECT_FIELDS}
    project.update({key: value for key, value in overrides.items() if value is not None})
    return project, pointer


def init_project_copy(project, pointer, run_id):
    """Write the project's own pointer copy if missing (only from a pointer that is this project's)."""
    path = project_copy_path(project)
    if path is None:
        return "no project_folder: start cannot restore the pointer"
    if path.is_file():
        return None
    if not path.parent.parent.is_dir():
        return f"project folder {path.parent.parent.as_posix()} not found: start cannot restore the pointer"
    if pointer.get("project_number") != project.get("project_number") or not same_folder(
            pointer.get("project_folder"), project.get("project_folder")):
        return "swarm/manifests/project.json belongs to another project: own copy not written"
    copy = dict(pointer, run_id=run_id)
    for key in ("revit_document_id", "acad_document_id"):
        if project.get(key) is not None:
            copy[key] = project[key]
    save_json(path, copy)
    return None


def check_pointer(queue):
    """("unchecked"|"ok", None) or ("restore", (foreign pointer, our pointer)); raises when it cannot restore."""
    project = queue.get("project")
    if not project or not project.get("project_number"):
        return "unchecked", None
    pointer = read_pointer()
    if (pointer.get("project_number") == project["project_number"] and pointer.get("run_id") == queue["run_id"]
            and same_folder(pointer.get("project_folder"), project.get("project_folder"))):
        return "ok", None
    copy_path = project_copy_path(project)
    if copy_path is None or not copy_path.is_file():
        where = copy_path.as_posix() if copy_path else "<no project_folder>"
        raise ValueError(f"swarm/manifests/project.json is not this run's (project {pointer.get('project_number')}, "
                         f"run {pointer.get('run_id')}) and no project copy exists at {where}; restore it by hand")
    own = load_json(copy_path)
    if own.get("project_number") != project["project_number"]:
        raise ValueError(f"{copy_path.as_posix()} is for project {own.get('project_number')}, not "
                         f"{project['project_number']}; restore the pointer by hand")
    return "restore", (pointer, dict(own, run_id=queue["run_id"]))


def restore_pointer(pointer, own):
    path = pointer_path()
    backup = None
    if path.exists():
        tag = re.sub(r"[^A-Za-z0-9_.-]", "_", str(pointer.get("project_number") or "unknown"))
        backup = path.with_name(f"project.json.{tag}.bak")
        shutil.copyfile(path, backup)
    save_json(path, own)  # Python json, UTF-8 without BOM
    return backup


# ---- model index work folded into finish
def index_work(queue, stage, outcome, scout=None, scopes=(), invalidate_all=False):
    """Merge/invalidate the scoped model index for a finished stage; returns the record for the attempt or None."""
    app = stage["lane"]
    wanted = bool(scout or scopes or invalidate_all)
    if app not in BRIDGES:
        if wanted:
            raise ValueError("--scout/--invalidate apply only to revit/acad lane stages")
        return None
    if not (wanted or outcome == "uncertain"):
        return None
    document_id = (queue.get("project") or {}).get(f"{app}_document_id")
    if not document_id:
        if wanted:
            raise ValueError(f"queue has no {app}_document_id; set it with `project --{app}-document-id <id>`")
        return {"skipped": f"no {app}_document_id in the queue; nothing cached for it, nothing invalidated"}
    record = {"app": app, "document_id": document_id}
    if scout:
        path = Path(scout)
        merged = model_index.merge_index(app, document_id, queue["run_id"], load_json(path), path.stat().st_mtime_ns)
        record.update(scout=path.as_posix(), merged=sorted(merged["scopes"]))
    if invalidate_all or outcome == "uncertain":
        record.update(invalidate="document", reason="uncertain",
                      invalidated=model_index.invalidate(app, document_id, queue["run_id"], (), "uncertain"))
    elif scopes:
        record.update(invalidate=list(scopes), reason="write",
                      invalidated=model_index.invalidate(app, document_id, queue["run_id"], scopes, "write"))
    return record


def precheck_finish(queue, name, scout, scopes, invalidate_all):
    """Reject bad index arguments before anything is written."""
    stage = queue["stages"][name]
    if (scout or scopes or invalidate_all) and stage["lane"] not in BRIDGES:
        raise ValueError("--scout/--invalidate apply only to revit/acad lane stages")
    if (scout or scopes or invalidate_all) and not (queue.get("project") or {}).get(f"{stage['lane']}_document_id"):
        raise ValueError(f"queue has no {stage['lane']}_document_id; set it with "
                         f"`project --{stage['lane']}-document-id <id>`")
    for value in scopes:
        model_index.scope_key(value)
    if scout:
        data = load_json(scout)
        if data.get("run_id") != queue["run_id"] or data.get("app") != stage["lane"]:
            raise ValueError("scout run_id or app does not match this queue stage")


# ---- debug ladder
def task_ids(queue):
    return list(dict.fromkeys(stage["task"] for stage in queue["stages"].values()))


def fail_cycle(queue, task, cause, landed=None, approved_extra=None, now=0.0):
    """Record one failure of a task and return the next ladder step (DEBUG_LADDER_DOC)."""
    if task not in task_ids(queue):
        raise ValueError(f"task {task} is not in this queue")
    if not cause or not cause.strip():
        raise ValueError("--cause needs one line")
    history = queue.setdefault("debug", {}).setdefault(task, [])
    approved = bool(approved_extra and approved_extra.strip())
    if approved and history and history[-1]["next"] == "stop-ask-jeremy":
        # Jeremy's answer to the stop just recorded: upgrade that failure, do not count a new one.
        entry = history[-1]
        entry.update(next=DEBUG_EXTRA["next"], model=DEBUG_EXTRA["model"], approved_extra=approved_extra.strip(),
                     approved_at=now)
        result = {"task": task, "failures": entry["failure"], "next": entry["next"], "model": entry["model"],
                  "note": DEBUG_EXTRA["note"].format(task=task, n=entry["failure"])}
        if (landed or entry.get("landed")) in ("yes", "maybe"):
            result["needs_jeremy_go"] = True
        return result
    failures = len(history) + 1
    step = DEBUG_LADDER[min(failures, len(DEBUG_LADDER)) - 1]
    extra = step["next"] == "stop-ask-jeremy" and approved
    if extra:
        step = DEBUG_EXTRA
    entry = {"failure": failures, "at": now, "cause": cause.strip().splitlines()[0], "landed": landed,
             "next": step["next"], "model": step["model"]}
    if extra:
        entry["approved_extra"] = approved_extra.strip()
    history.append(entry)
    result = {"task": task, "failures": failures, "next": step["next"], "model": step["model"],
              "note": step["note"].format(task=task, n=failures)}
    if landed in ("yes", "maybe"):
        result["needs_jeremy_go"] = True
    return result


def cycles(queue):
    debug = queue.get("debug", {})
    rows = {}
    for task in dict.fromkeys([*task_ids(queue), *debug]):
        history = debug.get(task, [])
        rows[task] = {"failures": len(history), "next": history[-1]["next"] if history else None,
                      "last_cause": history[-1]["cause"] if history else None}
    return {"run_id": queue["run_id"], "cycles": rows}


def bridge_calls(run_id):
    if not re.fullmatch(r"[0-9]{8}-[A-Za-z0-9_-]+", run_id):
        return []
    calls = []
    for path in (SWARM / "logs" / f"{run_id}.bridge").glob("*.json"):
        try:
            row = load_json(path)
            calls.append((row["app"], row["finished_at"] - row["duration_ms"] / 1000, row["duration_ms"] / 1000))
        except (OSError, ValueError, KeyError, TypeError):
            pass  # An async hook may still be writing this entry.
    return calls


def report(queue, calls=()):
    by_task = {}
    attempts_by_app = {app: [] for app in BRIDGES}
    for stage in queue["stages"].values():
        totals = by_task.setdefault(stage["task"], Counter())
        lane = stage["lane"]
        for attempt in stage["attempts"]:
            totals["queue_wait_s"] += max(0, attempt["started_at"] - attempt["ready_at"])
            if "finished_at" in attempt:
                duration = max(0, attempt["finished_at"] - attempt["started_at"])
                metric = {"offline": "offline_s", "tracker": "tracker_s", "human": "human_wait_s"}
                totals[metric.get(lane, "bridge_lane_s")] += duration
                if "resolved_at" in attempt:
                    totals["reconciliation_s"] += max(0, attempt["resolved_at"] - attempt["finished_at"])
            if lane in BRIDGES:
                attempts_by_app[lane].append((attempt["started_at"], attempt.get(
                    "resolved_at", attempt.get("finished_at", float("inf"))), stage["task"]))
    unmatched = 0
    for lane, start, seconds in calls:
        matches = [task for first, last, task in attempts_by_app.get(lane, []) if first <= start <= last]
        if len(matches) == 1:
            by_task[matches[0]]["bridge_tool_s"] += seconds
        else:
            unmatched += 1
    return {"run_id": queue["run_id"], "statuses": dict(Counter(s["status"] for s in queue["stages"].values())),
            "bridge_calls": len(calls), "unassigned_bridge_calls": unmatched,
            "seconds_by_task": {task: {key: round(value, 2) for key, value in totals.items()}
                                for task, totals in by_task.items()}}


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("queue", type=Path)
    commands = parser.add_subparsers(dest="command", required=True)
    for command in ("init", "project"):
        sub = commands.add_parser(command)
        if command == "init":
            sub.add_argument("plan", type=Path)
        for field in PROJECT_FIELDS:
            sub.add_argument("--" + field.replace("_", "-"), dest=field)
    commands.add_parser("add").add_argument("plan", type=Path)
    commands.add_parser("ready")
    commands.add_parser("report")
    commands.add_parser("cycles")
    sub = commands.add_parser("fail-cycle", description=DEBUG_LADDER_DOC,
                              formatter_class=argparse.RawDescriptionHelpFormatter)
    sub.add_argument("task")
    sub.add_argument("--cause", required=True)
    sub.add_argument("--landed", choices=("yes", "no", "maybe"))
    sub.add_argument("--approved-extra")
    for command in ("start", "finish", "resolve", "retry"):
        sub = commands.add_parser(command)
        sub.add_argument("stage")
        if command in ("finish", "resolve"):
            outcomes = ("pass", "fail", "uncertain") if command == "finish" else ("pass", "fail")
            sub.add_argument("outcome", choices=outcomes)
        if command == "finish":
            sub.add_argument("--scout", type=Path)
            sub.add_argument("--invalidate", nargs="+", action="extend", default=[], metavar="SCOPE")
            sub.add_argument("--invalidate-all", action="store_true")
        if command == "retry":
            sub.add_argument("--after", action="append", default=[])
    args = parser.parse_args(argv)

    code = 0
    try:
        now = time.time()
        if args.command == "init":
            if args.queue.exists():
                raise ValueError("queue already exists; use the existing state to resume")
            queue = new_queue(load_json(args.plan), now)
            project, pointer = project_from_pointer({field: getattr(args, field) for field in PROJECT_FIELDS})
            queue["project"] = project
            warnings = []
            if pointer.get("run_id") != queue["run_id"]:
                warnings.append(f"swarm/manifests/project.json run_id is {pointer.get('run_id')}, not "
                                f"{queue['run_id']}; start restores it from the project copy")
            warning = init_project_copy(project, pointer, queue["run_id"])
            if warning:
                warnings.append(warning)
            save_json(args.queue, queue)
            result = {"run_id": queue["run_id"], "stages": len(queue["stages"]), "project": project}
            if warnings:
                result["warnings"] = warnings
        else:
            queue = load_json(args.queue)
            if args.command == "ready":
                result = {"run_id": queue["run_id"],
                          "ready": [{"id": name, "task": stage["task"], "lane": stage["lane"]}
                                    for name, stage in available(queue)]}
            elif args.command == "report":
                result = report(queue, bridge_calls(queue["run_id"]))
            elif args.command == "cycles":
                result = cycles(queue)
            elif args.command == "project":
                changes = {field: getattr(args, field) for field in PROJECT_FIELDS
                           if getattr(args, field) is not None}
                if changes:
                    queue.setdefault("project", dict.fromkeys(PROJECT_FIELDS)).update(changes)
                    save_json(args.queue, queue)
                result = {"run_id": queue["run_id"], "project": queue.get("project")}
            elif args.command == "fail-cycle":
                result = fail_cycle(queue, args.task, args.cause, args.landed, args.approved_extra, now)
                save_json(args.queue, queue)
                code = 4 if result["next"] == "stop-ask-jeremy" else 0
            elif args.command == "add":
                plan = load_json(args.plan)
                if plan["run_id"] != queue["run_id"]:
                    raise ValueError("new stages belong to a different run")
                current = queue["stages"]
                specs = [{"id": name, "task": stage["task"], "lane": stage["lane"], "after": stage["after"]}
                         for name, stage in current.items()]
                expanded = new_queue({"run_id": plan["run_id"], "stages": specs + plan["stages"]}, now)
                for name in current:
                    expanded["stages"][name] = current[name]
                for key, value in queue.items():  # keep project and debug history
                    expanded.setdefault(key, value)
                mark_ready(expanded, now)
                save_json(args.queue, expanded)
                result = {"run_id": queue["run_id"], "added": len(plan["stages"])}
            else:
                pointer = check_pointer(queue) if args.command == "start" else ("unchecked", None)
                if args.command == "finish" and args.stage in queue["stages"]:
                    precheck_finish(queue, args.stage, args.scout, args.invalidate, args.invalidate_all)
                transition(queue, args.command, args.stage, getattr(args, "outcome", None), now,
                           getattr(args, "after", ()))
                stage = queue["stages"][args.stage]
                result = {"id": args.stage, "status": stage["status"]}
                if args.command == "finish":
                    record = index_work(queue, stage, args.outcome, args.scout, args.invalidate, args.invalidate_all)
                    if record is not None:
                        stage["attempts"][-1]["index"] = record
                        result["index"] = record
                elif args.command == "start":
                    if pointer[0] == "restore":
                        backup = restore_pointer(*pointer[1])
                        result.update(pointer_restored=True, pointer_backup=backup.as_posix() if backup else None)
                    else:
                        result["pointer"] = pointer[0]
                save_json(args.queue, queue)
        print(json.dumps(result))
    except (OSError, KeyError, ValueError, TypeError) as error:
        parser.exit(2, f"{error}\n")
    return code


if __name__ == "__main__":
    raise SystemExit(main())
