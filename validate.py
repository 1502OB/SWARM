"""Validate a JSON file against a swarm schema (standard library only; no jsonschema dependency).

    python C:/Users/Jeremy/swarm/validate.py <schema.json> <file.json>

Exit 0 = valid, 1 = invalid (one line per error on stdout), 2 = usage error or unreadable JSON.
Supports the subset the swarm schemas use: type (incl. lists like ["integer","null"]), enum, required,
properties, additionalProperties: false, items, minItems, minLength. "{}" accepts any value.
"""
import json
import sys

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
    if errors:
        print(f"INVALID {sys.argv[2]} ({len(errors)} error(s)):")
        for e in errors:
            print("  " + e)
        sys.exit(1)
    print(f"VALID {sys.argv[2]}")


if __name__ == "__main__":
    main()
