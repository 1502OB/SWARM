"""PreToolUse guard: restrict autocad-mcp tools to read-only operations.

Used by the acad-scout and verifier subagents (wired in their frontmatter).
The autocad-mcp server exposes multi-operation tools (entity list AND entity
erase live in the same tool), so a frontmatter tool list alone cannot make an
agent read-only. Exit code 2 blocks the call and feeds stderr back to the agent.
"""
import json
import re
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
from guardlib import strip_lisp_comments  # noqa: E402

READ_OPS = {
    "drawing": {"info", "get_variables"},
    "entity": {"list", "count", "get"},
    "layer": {"list"},
    "block": {"list", "get_attributes"},
    "system": {"status", "health", "get_backend", "runtime", "execute_lisp"},
    "view": {"get_screenshot"},
    "pid": {"list_symbols"},
    "annotation": set(),
}

# AutoLISP calls that can change the drawing, the session, or the filesystem.
# 2026-11-06: vl-file-size / vl-file-directory-p / vl-file-systime are read-only and allowed (only delete, rename,
# copy are denied); ActiveX methods that modify entities and command queueing (vla-SendCommand / PostCommand) are
# denied (they were missing). ; comments are removed before matching. Tests: hooks/tests/test_guards.py.
LISP_DENY = re.compile(
    r"\(\s*("
    r"entmake|entmakex|entmod|entdel|entupd|command|command-s|vl-cmdf|setvar|"
    r"load|startapp|open|write-line|write-char|eval|read|"
    r"vl-file-(?:delete|rename|copy)|vl-mkdir|vl-registry-write|vl-registry-delete|"
    r"vla-put-\S+|vla-add\S*|vla-delete|vla-erase|vla-save\S*|vla-purge\S*|"
    r"vla-sendcommand|vla-postcommand|vla-move|vla-copy|vla-rotate\S*|vla-mirror\S*|vla-scaleentity|"
    r"vla-explode|vla-offset|vla-array\S*|vla-update|vla-regen|vla-import|vla-wblock|vla-export|vla-activate|"
    r"vlax-put\S*|vlax-invoke\S*|vlax-release-object|ssadd|ssdel|"
    r"vl-load-reactors|vlr-\S+|defun|defun-q"
    r")[\s()]",
    re.IGNORECASE,
)


# Tab switching is allowed for every AutoCAD agent (user decision 2026-10-16): exactly one setvar CTAB call.
CTAB_ONLY = re.compile(r'^\(setvar "CTAB" "[^"\()]{1,255}"\)$', re.IGNORECASE)
OLEOPEN_ONLY = re.compile(r'^\(command "_\.OLEOPEN" \(handent "[0-9A-Fa-f]{1,16}"\)\)$')


def block(msg):
    sys.stderr.write("BLOCKED by acad_readonly_guard: " + msg + "\n")
    sys.exit(2)


def main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        block("could not parse hook input")
    tool = payload.get("tool_name", "")
    if not tool.startswith("mcp__autocad-mcp__"):
        sys.exit(0)
    short = tool[len("mcp__autocad-mcp__"):]
    ti = payload.get("tool_input") or {}
    op = str(ti.get("operation", "")).strip()
    allowed = READ_OPS.get(short)
    if allowed is None:
        block(f"unknown autocad tool '{short}' is not on the read allowlist")
    if op not in allowed:
        block(f"{short}.{op} is a write/unsafe operation; this agent is read-only")
    if short == "system" and op == "execute_lisp":
        code = str((ti.get("data") or {}).get("code", ""))
        # --allow-oleopen (excel-writer only): the single permitted write is opening one OLE object by handle.
        if CTAB_ONLY.match(code.strip()):
            sys.exit(0)
        if "--allow-oleopen" in sys.argv and OLEOPEN_ONLY.match(code.strip()):
            sys.exit(0)
        m = LISP_DENY.search(strip_lisp_comments(code) + " ")
        if m:
            block(f"execute_lisp contains '{m.group(1)}', which can modify the drawing; use pure query LISP only")
    sys.exit(0)


if __name__ == "__main__":
    main()
