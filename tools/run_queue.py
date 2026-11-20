"""Plan and time the ready stages of a swarm run. The main session owns the queue and dispatches agents.

    python tools/run_queue.py <queue.json> init <plan.json>
    python tools/run_queue.py <queue.json> add <plan.json>
    python tools/run_queue.py <queue.json> ready
    python tools/run_queue.py <queue.json> start <stage-id>
    python tools/run_queue.py <queue.json> finish <stage-id> <pass|fail|uncertain>
    python tools/run_queue.py <queue.json> resolve <stage-id> <pass|fail>
    python tools/run_queue.py <queue.json> retry <stage-id> [--after <stage-id> ...]
    python tools/run_queue.py <queue.json> report
"""
import argparse
from collections import Counter
from graphlib import TopologicalSorter
import json
import re
import time
from pathlib import Path

from model_index import SWARM, load_json, save_json

BRIDGES = {"revit", "acad"}
LANES = BRIDGES | {"offline", "tracker", "human"}
AGENT_LIMIT = 20


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


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("queue", type=Path)
    commands = parser.add_subparsers(dest="command", required=True)
    commands.add_parser("init").add_argument("plan", type=Path)
    commands.add_parser("add").add_argument("plan", type=Path)
    commands.add_parser("ready")
    commands.add_parser("report")
    for command in ("start", "finish", "resolve", "retry"):
        sub = commands.add_parser(command)
        sub.add_argument("stage")
        if command in ("finish", "resolve"):
            outcomes = ("pass", "fail", "uncertain") if command == "finish" else ("pass", "fail")
            sub.add_argument("outcome", choices=outcomes)
        if command == "retry":
            sub.add_argument("--after", action="append", default=[])
    args = parser.parse_args()

    try:
        now = time.time()
        if args.command == "init":
            if args.queue.exists():
                raise ValueError("queue already exists; use the existing state to resume")
            queue = new_queue(load_json(args.plan), now)
            save_json(args.queue, queue)
            result = {"run_id": queue["run_id"], "stages": len(queue["stages"])}
        else:
            queue = load_json(args.queue)
            if args.command == "ready":
                result = {"run_id": queue["run_id"],
                          "ready": [{"id": name, "task": stage["task"], "lane": stage["lane"]}
                                    for name, stage in available(queue)]}
            elif args.command == "report":
                result = report(queue, bridge_calls(queue["run_id"]))
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
                mark_ready(expanded, now)
                save_json(args.queue, expanded)
                result = {"run_id": queue["run_id"], "added": len(plan["stages"])}
            else:
                transition(queue, args.command, args.stage, getattr(args, "outcome", None), now,
                           getattr(args, "after", ()))
                save_json(args.queue, queue)
                result = {"id": args.stage, "status": queue["stages"][args.stage]["status"]}
        print(json.dumps(result))
    except (OSError, KeyError, ValueError, TypeError) as error:
        parser.exit(2, f"{error}\n")


if __name__ == "__main__":
    main()
