"""Write the task.json of a derived task (visual export, AutoCAD -PUB publish, read-only probe) from its base task.

    python tools/make_task.py derive <base.task.json> --task-id <id> --operation <export-sheet|publish-sheet|read-only>
                                     [--sheets S1 S2 ...] [--out <path>] [--script <path>] [--note "<text>"] [--force]

run_id, document_id and source_requirement_id come from the base task; app and assigned_agent follow the operation:
  export-sheet   Revit visual export (revit-writer, default script library/revit/export-visual-sheet.cs); needs --sheets
  publish-sheet  AutoCAD -PUB publish (acad-writer, default script tools/swarm_publish.lsp); needs --sheets
  read-only      probe on the base task's app and targets (revit-writer / acad-writer); --note says what to read
The new id must keep the base id's project prefix. Output goes next to the base (<id>.task.json) unless --out.
The result is validated against schemas/task.schema.json before it is written; an existing file is never
overwritten without --force. Prints JSON {path, task_id, app, operation, assigned_agent, sheets}.
Exit 0 = written, 1 = invalid task (nothing written), 2 = usage/input problem.
"""
import argparse
import json
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).absolute().parent.parent))
import validate  # noqa: E402
from swarm_paths import fwd  # noqa: E402

OPERATIONS = {
    "export-sheet": {"app": "revit", "agent": "revit-writer", "script": "library/revit/export-visual-sheet.cs"},
    "publish-sheet": {"app": "acad", "agent": "acad-writer", "script": "tools/swarm_publish.lsp"},
    "read-only": {"app": None, "agent": None, "script": None},
}
WRITERS = {"revit": "revit-writer", "acad": "acad-writer"}


def change_text(operation, base, task_id, sheets, script, note):
    run_id, base_id = base["run_id"], base["task_id"]
    listed = ", ".join(sheets)
    if operation == "export-sheet":
        text = (f"VISUAL EXPORT ONLY for {base_id}: export sheet(s) {listed} to PDF with {script} (transactionMode "
                f"none). Output: the exact absent PDF path per sheet that the orchestrator gives, in a fresh "
                f"visual/{run_id}/{task_id}/<attempt>/ folder; echo the request id and path. Never overwrite, "
                f"no model change, never save or sync. Return tail 'done=<n> skipped=<n> failed=<n>'.")
    elif operation == "publish-sheet":
        dsd = f"visual/{run_id}/{base_id}/{base_id}"
        text = (f"PUBLISH ONLY for {base_id}: sheet(s)/layout(s) {listed}. Load {script} and call "
                f"(swarm:publish \"{dsd}.dsd\" \"{dsd}.pdf\") (DSD from tools/acad_dsd.py). No drawing change, "
                f"never save. A publish outlasts one bridge call: a TIMEOUT is expected; the orchestrator runs "
                f"the dialog check and reads PlotandPublishLog.CSV.")
    else:
        text = (f"READ-ONLY PROBE for {base_id}: no transaction, no model or drawing change, never save or sync. "
                + (f"Sheets: {listed}. " if sheets else "")
                + (f"Script: {script}. " if script else "")
                + "Return what was read as JSON.")
    return text + (f" {note.strip()}" if note and note.strip() else "")


def derive(base, task_id, operation, sheets=(), script=None, note=None):
    spec = OPERATIONS[operation]
    for key in ("run_id", "task_id", "app"):
        if not base.get(key):
            raise ValueError(f"base task has no {key}")
    if base["task_id"].split("-")[0] != task_id.split("-")[0]:
        raise ValueError(f"task id {task_id} must keep the base prefix {base['task_id'].split('-')[0]}-")
    if task_id == base["task_id"]:
        raise ValueError("a derived task needs its own task id")
    app = spec["app"] or base["app"]
    if spec["app"] and base["app"] != spec["app"]:
        raise ValueError(f"{operation} is for {spec['app']} tasks; the base task is {base['app']}"
                         + (" (use publish-sheet)" if base["app"] == "acad" else " (use export-sheet)"))
    sheets = list(dict.fromkeys(s.strip() for s in sheets if s.strip()))
    if operation != "read-only" and not sheets:
        raise ValueError(f"{operation} needs --sheets")
    script = fwd(script) if script else spec["script"]
    task = {"run_id": base["run_id"], "task_id": task_id, "app": app, "operation": operation}
    if base.get("document_id"):
        task["document_id"] = base["document_id"]
    task["target_ids"] = list(base.get("target_ids", [])) if operation == "read-only" else []
    task["change"] = change_text(operation, base, task_id, sheets, script, note)
    task["source_requirement_id"] = f"derived from {base['task_id']}: {base.get('source_requirement_id') or 'n/a'}"
    task["assigned_agent"] = WRITERS[app] if operation == "read-only" else spec["agent"]
    return task


def schema_errors(task):
    if not validate.TASK_SCHEMA.is_file():
        return []
    errors = []
    validate.check(task, validate.load(validate.TASK_SCHEMA), "$", errors)
    return errors


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    commands = parser.add_subparsers(dest="command", required=True)
    sub = commands.add_parser("derive")
    sub.add_argument("base", type=Path)
    sub.add_argument("--task-id", required=True)
    sub.add_argument("--operation", required=True, choices=tuple(OPERATIONS))
    sub.add_argument("--sheets", nargs="+", default=[])
    sub.add_argument("--out", type=Path)
    sub.add_argument("--script")
    sub.add_argument("--note")
    sub.add_argument("--force", action="store_true")
    args = parser.parse_args(argv)
    try:
        base = validate.load(args.base)
        task = derive(base, args.task_id, args.operation, args.sheets, args.script, args.note)
        errors = schema_errors(task)
        if errors:
            print(json.dumps({"task_id": args.task_id, "valid": False, "errors": errors}))
            return 1
        out = args.out or args.base.with_name(f"{args.task_id}.task.json")
        if out.exists() and not args.force:
            raise ValueError(f"{fwd(out)} exists; pass --force to replace it")
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(json.dumps(task, indent=1) + "\n", encoding="utf-8")
    except (OSError, ValueError, KeyError) as error:
        parser.exit(2, f"{error}\n")
    print(json.dumps({"path": fwd(out), "task_id": task["task_id"], "app": task["app"],
                      "operation": task["operation"], "assigned_agent": task["assigned_agent"],
                      "sheets": list(dict.fromkeys(s.strip() for s in args.sheets if s.strip())), "valid": True}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
