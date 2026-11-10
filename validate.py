"""Validate a JSON file against a swarm schema (standard library only; no jsonschema dependency).

    python ~/swarm/validate.py <schema.json> <file.json>

When <file.json> is a <task-id>.expected.json and the sibling <task-id>.task.json exists, that task.json is also
validated against schemas/task.schema.json and the two task_id values must be equal (LESSONS A40).
Exit 0 = valid, 1 = invalid (one line per error on stdout), 2 = usage error or unreadable JSON.
Supports the subset the swarm schemas use: type (incl. lists like ["integer","null"]), enum, required,
properties, additionalProperties: false, items, minItems, minLength, pattern. "{}" accepts any value.
"""
import json
import os
import re
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from swarm_paths import SWARM  # noqa: E402

TASK_SCHEMA = SWARM / "schemas" / "task.schema.json"

TYPES = {
    "object": lambda v: isinstance(v, dict),
    "array": lambda v: isinstance(v, list),
    "string": lambda v: isinstance(v, str),
    "integer": lambda v: isinstance(v, int) and not isinstance(v, bool),
    "number": lambda v: isinstance(v, (int, float)) and not isinstance(v, bool),
    "boolean": lambda v: isinstance(v, bool),
    "null": lambda v: v is None,
}


def check(value, schema, path, errors):
    t = schema.get("type")
    if t is not None:
        allowed = t if isinstance(t, list) else [t]
        if not any(TYPES[x](value) for x in allowed):
            errors.append(f"{path}: expected {'|'.join(allowed)}, got {type(value).__name__}")
            return
    if "enum" in schema and value not in schema["enum"]:
        errors.append(f"{path}: {value!r} not one of {schema['enum']}")
    if isinstance(value, str) and len(value) < schema.get("minLength", 0):
        errors.append(f"{path}: shorter than {schema['minLength']} character(s)")
    if isinstance(value, str) and "pattern" in schema and not re.search(schema["pattern"], value):
        errors.append(f"{path}: {value!r} does not match {schema['pattern']}"
                      + (f" ({schema['description']})" if schema.get("description") else ""))
    if isinstance(value, dict):
        props = schema.get("properties", {})
        for key in schema.get("required", []):
            if key not in value:
                errors.append(f"{path}: missing required '{key}'")
        if schema.get("additionalProperties") is False:
            for key in value:
                if key not in props:
                    errors.append(f"{path}: unexpected property '{key}'")
        for key, sub in props.items():
            if key in value:
                check(value[key], sub, f"{path}.{key}", errors)
    if isinstance(value, list):
        if len(value) < schema.get("minItems", 0):
            errors.append(f"{path}: fewer than {schema['minItems']} item(s)")
        if "items" in schema:
            for i, item in enumerate(value):
                check(item, schema["items"], f"{path}[{i}]", errors)


def load(p):
    with open(p, encoding="utf-8-sig") as f:  # tolerate a BOM (PowerShell-written files)
        return json.load(f)


def sibling_task_errors(path, data):
    """<id>.expected.json -> validate <id>.task.json (if present) and compare task_id."""
    name = os.path.basename(path)
    if not name.lower().endswith(".expected.json"):
        return []
    task_path = os.path.join(os.path.dirname(os.path.abspath(path)), name[:-len(".expected.json")] + ".task.json")
    if not os.path.isfile(task_path):
        return []
    try:
        task = load(task_path)
    except (OSError, ValueError) as e:
        return [f"sibling {os.path.basename(task_path)}: unreadable ({e})"]
    errs = []
    if TASK_SCHEMA.is_file():
        sub = []
        check(task, load(TASK_SCHEMA), "$", sub)
        errs += [f"sibling {os.path.basename(task_path)}: {e}" for e in sub]
    a = data.get("task_id") if isinstance(data, dict) else None
    b = task.get("task_id") if isinstance(task, dict) else None
    if a != b:
        errs.append(f"$.task_id: {a!r} differs from {b!r} in sibling {os.path.basename(task_path)}")
    return errs


def main():
    if len(sys.argv) != 3:
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    try:
        schema, data = load(sys.argv[1]), load(sys.argv[2])
    except (OSError, ValueError) as e:
        print(f"ERROR: {e}")
        sys.exit(2)
    errors = []
    check(data, schema, "$", errors)
    errors += sibling_task_errors(sys.argv[2], data)
    if errors:
        print(f"INVALID {sys.argv[2]} ({len(errors)} error(s)):")
        for e in errors:
            print("  " + e)
        sys.exit(1)
    print(f"VALID {sys.argv[2]}")


if __name__ == "__main__":
    main()
