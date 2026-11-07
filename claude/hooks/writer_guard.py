"""PreToolUse guard for revit-writer and acad-writer. Usage: writer_guard.py revit|acad

1. Lock held: every Revit / AutoCAD MCP call is blocked unless swarm/locks/<app>.lock holds a task id
   (not missing, empty, or "released"). The writer takes the lock with Write before its first MCP call.
2. AutoCAD: blocks code that would save-as, close or quit, including code inside a .lsp passed to
   (load "..."); an in-place save of a DWG (vla-Save, or drawing.save without a path) is allowed.
3. Revit split (Jeremy, 2026-11-02). `writer_guard.py revit` (revit-writer): model edits in transactions,
   plus switching to a model already open; save / sync / relinquish / close / detach / audit / open blocked.
   `writer_guard.py revit-ops` (revit-ops agent): those file operations, plus opening files listed in
   swarm/locks/revit_open_approved.txt (Jeremy approves each); transactions and model edits blocked.
   Both: PostCommand, Process.Start and UIApplication members other than OpenAndActivateDocument /
   ActiveUIDocument stay blocked.
4. `writer_guard.py acad-ops` (acad-ops agent, Jeremy 2026-11-03): AutoCAD file operations only — drawing open
   for DWGs listed in swarm/locks/acad_open_approved.txt (exact file, or folder entry ending in '/'), switching the
   active drawing (one vla-activate call), in-place save, info/status/init, query LISP. Entity edits, save-as,
   close, quit and purge blocked. Same acad.lock as the writer.
Exit code 2 blocks the call and feeds stderr back to the agent.
"""
import json
import os
import re
import sys
from pathlib import Path

LOCKS = Path(os.environ.get("SWARM_LOCKS_DIR", "C:/Users/Jeremy/swarm/locks"))  # override for hook tests only

# Jeremy, 2026-11-02 (split): revit-writer = model edits in transactions + switching to an already-open model.
# File operations (save / save-as / sync / relinquish / close / detach / audit / open) belong to revit-ops only.
REVIT_DENY = re.compile(
    r"\b(Save|SaveAs|SynchronizeWithCentral|RelinquishOwnership|Close|PostCommand|"
    r"Process\.Start|OpenDocumentFile|DetachFromCentral\w*|Audit|SetAudit)\b"
)
# revit-ops: file operations allowed, model edits blocked.
OPS_DENY = re.compile(
    r"\b(Transaction|SubTransaction|TransactionGroup|ElementTransformUtils|PostCommand|Process\.Start|"
    r"Delete|NewFamilyInstance|Pipe\.Create|Duct\.Create|TextNote\.Create|IndependentTag\.Create|"
    r"RevisionCloud\.Create|EditFamily|LoadFamily|Regenerate)\b"
    r"|(\.\s*Set\s*\()"
)
# Opening a Revit file that is NOT already open needs Jeremy's per-file approval: the orchestrator adds the
# approved path (one per line, as written in the code) to swarm/locks/revit_open_approved.txt after Jeremy
# approves it in chat. Switching to a model already open (target from Application.Documents) needs no approval.
OPEN_APPROVALS = LOCKS / "revit_open_approved.txt"
UIAPP_RE = re.compile(r"\bUIApplication\b")
UIAPP_ALLOWED_MEMBERS = {"OpenAndActivateDocument", "ActiveUIDocument", "Application"}
UIAPP_VAR_RE = re.compile(r"\b(?:var|UIApplication)\s+(\w+)\s*=\s*new\s+UIApplication\s*\(")
OPEN_CALL_RE = re.compile(r"\b(OpenDocumentFile|OpenAndActivateDocument)\b")


def _approved_paths():
    try:
        lines = OPEN_APPROVALS.read_text(encoding="utf-8").splitlines()
    except FileNotFoundError:
        return []
    return [ln.strip() for ln in lines if ln.strip() and not ln.strip().startswith("#")]


def revit_open_problem(code):
    """None if the code opens/activates only already-open models or Jeremy-approved files, else the reason."""
    for var in UIAPP_VAR_RE.findall(code):
        for member in re.findall(r"\b" + re.escape(var) + r"\s*\.\s*(\w+)", code):
            if member not in UIAPP_ALLOWED_MEMBERS:
                return f"UIApplication member '{member}' is not allowed; only OpenAndActivateDocument / ActiveUIDocument"
    if not OPEN_CALL_RE.search(code):
        return None
    if re.search(r"Application\s*\.\s*Documents\b", code) and "OpenDocumentFile" not in code \
            and not re.search(r"OpenAndActivateDocument\s*\(\s*@?\"", code):
        return None  # switching to a model already open in the session (target taken from Application.Documents)
    approved = _approved_paths()
    if any(p in code for p in approved):
        return None
    return ("opening a Revit file that is not already open needs Jeremy's approval for that file; the orchestrator "
            f"adds the approved path to {OPEN_APPROVALS.as_posix()} after he approves it")
# In-place saves are allowed (Jeremy, 2026-10-21): vla-Save in LISP, and drawing.save with no path.
# Save-as, close, quit, open and purge stay blocked.
ACAD_DENY = re.compile(
    r'"_?\.?(QSAVE|SAVE|SAVEAS|SAVEALL|CLOSE|CLOSEALL|QUIT|EXIT|WBLOCK|ETRANSMIT)"'
    r"|\bvla-(SaveAs|Close)\b|\bvlax-invoke[^)]*'(SaveAs|Close)\b",
    re.IGNORECASE,
)
ACAD_DRAWING_DENY = re.compile(r"save|close|open|new|quit|purge", re.IGNORECASE)
LOAD_RE = re.compile(r'\(\s*load\s+"([^"]+)"', re.IGNORECASE)


def block(msg):
    sys.stderr.write("BLOCKED by writer_guard: " + msg + "\n")
    sys.exit(2)


def check_lock(app):
    path = LOCKS / f"{app}.lock"
    try:
        holder = path.read_text(encoding="utf-8").strip()
    except FileNotFoundError:
        holder = ""
    if not holder or holder.lower() == "released":
        block(f"{path.as_posix()} is not held. Write your task id to it before any {app} call, "
              "and release it only after your last call.")


def main():
    app = sys.argv[1] if len(sys.argv) > 1 else ""
    if app == "revit-ops":
        return ops_main()
    if app == "acad-ops":
        return acad_ops_main()
    if app not in ("revit", "acad"):
        block(f"unknown app '{app}'")
    try:
        payload = json.load(sys.stdin)
    except Exception:
        block("could not parse hook input")
    tool = payload.get("tool_name", "")
    ti = payload.get("tool_input") or {}
    prefix = "mcp__revit-mcp__" if app == "revit" else "mcp__autocad-mcp__"
    if not tool.startswith(prefix):
        sys.exit(0)
    check_lock(app)

    if app == "revit" and tool == prefix + "send_code_to_revit":
        code = str(ti.get("code", ""))
        m = REVIT_DENY.search(code)
        if m:
            block(f"code references '{m.group(1)}'; revit-writer only edits the model in transactions. Save, sync, "
                  "relinquish, close, detach, audit and opening files belong to the revit-ops agent")
        why = revit_open_problem(code)
        if why:
            block(why)

    if app == "acad":
        short = tool[len(prefix):]
        op = str(ti.get("operation", ""))
        in_place_save = op == "save" and not (ti.get("data") or {}).get("path")
        if short == "drawing" and ACAD_DRAWING_DENY.search(op) and not in_place_save:
            block(f"drawing.{op} is not allowed; writers never save, open, close or purge the DWG")
        if short == "system" and op == "execute_lisp":
            code = str((ti.get("data") or {}).get("code", ""))
            texts = [("execute_lisp", code)]
            for f in LOAD_RE.findall(code):
                try:
                    texts.append((f, Path(f).read_text(encoding="utf-8", errors="replace")))
                except OSError:
                    block(f"cannot read '{f}' to check it; load only scripts under swarm/scripts")
            for where, text in texts:
                m = ACAD_DENY.search(text)
                if m:
                    block(f"{where} contains '{m.group(0)}'; writers never save, close or quit the DWG")
    sys.exit(0)


def ops_main():
    """revit-ops: file operations (sync, save, save-as, relinquish, close, detach, audit, open approved files,
    switch to an open model); no model edits. Holds the same revit.lock as the writer."""
    try:
        payload = json.load(sys.stdin)
    except Exception:
        block("could not parse hook input")
    tool = payload.get("tool_name", "")
    ti = payload.get("tool_input") or {}
    if not tool.startswith("mcp__revit-mcp__"):
        sys.exit(0)
    check_lock("revit")
    if tool != "mcp__revit-mcp__send_code_to_revit":
        block("revit-ops uses send_code_to_revit only (identity checks and file operations)")
    if ti.get("transactionMode") != "none":
        block('revit-ops must use transactionMode "none" (no model edits)')
    code = str(ti.get("code", ""))
    m = OPS_DENY.search(code)
    if m:
        block(f"code references '{m.group(0).strip()}'; revit-ops never edits the model (that is revit-writer's job)")
    why = revit_open_problem(code)
    if why:
        block(why)
    sys.exit(0)


# ---- acad-ops (Jeremy, 2026-11-03): AutoCAD file operations only — open approved DWGs, switch the active
# drawing, save in place. No entity edits, no save-as / close / quit / purge. Holds the same acad.lock.
ACAD_OPEN_APPROVALS = LOCKS / "acad_open_approved.txt"
ACAD_OPS_ALLOWED = {
    "system": {"status", "health", "get_backend", "runtime", "init", "execute_lisp"},
    "drawing": {"info", "get_variables", "open", "save"},
}
# execute_lisp: pure query LISP (same deny list as acad_readonly_guard), or exactly one activate-by-name call.
ACAD_ACTIVATE_RE = re.compile(
    r'^\(\s*vla-activate\s+\(\s*vla-item\s+\(\s*vla-get-documents\s+\(\s*vlax-get-acad-object\s*\)\s*\)\s+'
    r'"[^"\\]{1,255}\.dwg"\s*\)\s*\)$', re.IGNORECASE)


# Document-level calls the read-only deny list doesn't cover (close / quit / open / activate outside the template).
ACAD_OPS_LISP_DENY = re.compile(
    r"\b(vla-close|vla-quit|vla-open|vla-activate|vla-sendcommand|vla-postcommand|vla-new|vla-wblock|vla-export|"
    r"vl-cmdf|vlax-create-object|vla-import)\b", re.IGNORECASE)


def _norm(p):
    return re.sub(r"/+", "/", str(p).replace("\\", "/")).strip().lower()


def acad_open_problem(path):
    """None if Jeremy approved this DWG (exact path, or a folder entry ending in '/'), else the reason."""
    try:
        lines = ACAD_OPEN_APPROVALS.read_text(encoding="utf-8").splitlines()
    except FileNotFoundError:
        lines = []
    target = _norm(path)
    if not target.endswith(".dwg"):
        return "acad-ops opens .dwg files only"
    for ln in lines:
        ln = ln.strip()
        if not ln or ln.startswith("#"):
            continue
        a = _norm(ln)
        if (a.endswith("/") and target.startswith(a) and "/" not in target[len(a):]) or target == a:
            return None
    return ("opening this DWG needs Jeremy's approval; the orchestrator adds the file (or its folder, ending in '/') "
            f"to {ACAD_OPEN_APPROVALS.as_posix()} after he approves it in chat")


def acad_ops_main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        block("could not parse hook input")
    tool = payload.get("tool_name", "")
    ti = payload.get("tool_input") or {}
    prefix = "mcp__autocad-mcp__"
    if not tool.startswith(prefix):
        sys.exit(0)
    check_lock("acad")
    short = tool[len(prefix):]
    op = str(ti.get("operation", "")).strip()
    data = ti.get("data") or {}
    if op not in ACAD_OPS_ALLOWED.get(short, set()):
        block(f"{short}.{op} is not a file operation; acad-ops never edits the drawing (that is acad-writer's job)")
    if short == "drawing" and op == "save" and data.get("path"):
        block("acad-ops saves in place only (drawing save with no path); save-as is not allowed")
    if short == "drawing" and op == "open":
        why = acad_open_problem(data.get("path", ""))
        if why:
            block(why)
    if short == "system" and op == "execute_lisp":
        code = str(data.get("code", "")).strip()
        if not ACAD_ACTIVATE_RE.match(code):
            sys.path.insert(0, str(Path(__file__).parent))
            from acad_readonly_guard import LISP_DENY  # noqa: E402
            m = LISP_DENY.search(code + " ") or ACAD_OPS_LISP_DENY.search(code)
            if m:
                block(f"execute_lisp contains '{m.group(1)}'; acad-ops runs only query LISP or one vla-activate call")
    sys.exit(0)


if __name__ == "__main__":
    main()
