"""PostToolUse hook (Write|Edit): lint every AutoLISP file written under swarm/scripts or swarm/library (2026-11-06).

Runs swarm/tools/lsp_lint.py on the file just written. Errors (unbalanced parens, non-AutoLISP functions, duplicate
or library-clashing defuns, attach commands sent with (command)) are fed back to the agent that wrote the file as a
blocking PostToolUse decision, so the scripter fixes them before anyone loads the script. Warnings are passed back as
context. Any other file: no output. A crash here never blocks work (exit 0).
"""
import json
import re
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
from paths import SWARM, fwd, path_re  # noqa: E402  (2026-11-09: no hard-coded user folder)

sys.path.insert(0, fwd(SWARM / "tools"))
WATCH = re.compile(r"(?:^|[\\/])swarm[\\/](scripts|library)[\\/].+\.lsp$|^" + path_re(SWARM) +
                   r"[\\/](scripts|library)[\\/].+\.lsp$", re.IGNORECASE)


def main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        return
    ti = payload.get("tool_input") or {}
    path = str(ti.get("file_path") or (payload.get("tool_response") or {}).get("filePath") or "")
    if not WATCH.search(path) or not Path(path).is_file():
        return
    try:
        from lsp_lint import lint
        errors, warnings = lint(path)
    except Exception as e:  # never block work because the linter itself failed
        print(json.dumps({"hookSpecificOutput": {"hookEventName": "PostToolUse",
                                                 "additionalContext": f"lsp_lint could not run on {path}: {e}"}}))
        return
    name = Path(path).name
    if errors:
        lines = [f"{c} line {l}: {m}" for c, l, m in errors] + [f"{c} line {l}: {m}" for c, l, m in warnings[:10]]
        print(json.dumps({"decision": "block", "reason":
                          f"lsp_lint found {len(errors)} error(s) in {name}; fix them before the script is loaded:\n"
                          + "\n".join(lines)}))
    elif warnings:
        lines = [f"{c} line {l}: {m}" for c, l, m in warnings[:15]]
        print(json.dumps({"hookSpecificOutput": {"hookEventName": "PostToolUse", "additionalContext":
                          f"lsp_lint: {name} has no errors, {len(warnings)} warning(s):\n" + "\n".join(lines)}}))


if __name__ == "__main__":
    main()
