"""PreToolUse guard for the swarm ORCHESTRATOR (the main session started in ~/swarm).

Wired in ~/swarm/.claude/settings.json, so it fires for the main session and for every subagent there. Subagent
calls carry a non-empty `agent_id` and pass straight through; their own frontmatter hooks apply. For the main
session, Revit / AutoCAD / Bluebeam calls are blocked except the Step 0 bridge and identity checks below, so the
orchestrator delegates model work (and screenshots) to scouts and writers instead of filling its own context.

One exception for Revit worksharing (Jeremy, 2026-11-02; he authorized Claude to sync around write batches on
2026-10-20): the main session may run send_code_to_revit when the code is EXACTLY the sync template below, with
transactionMode "none" and no parameters. Only the comment varies, limited to plain characters (no quotes or
backslashes), so the call can do nothing but SynchronizeWithCentral with relinquish-all. Subagents still cannot
sync (writer_guard / revit_readonly_code_guard block it). Exit code 2 blocks the call.
"""
import json
import re
import sys

MAIN_ALLOWED = {
    # AutoCAD: bridge health and reconnect (Step 0), plus the open drawing's name for project.json.
    "mcp__autocad-mcp__system": {"status", "health", "get_backend", "runtime", "init"},
    "mcp__autocad-mcp__drawing": {"info"},
    # Revit: the open model's identity (say_hello opens a modal dialog and is never allowed).
    "mcp__revit-mcp__get_project_info": None,
    "mcp__revit-mcp__get_current_view_info": None,
}
PREFIXES = ("mcp__revit-mcp__", "mcp__autocad-mcp__", "mcp__Bluebeam__")

SYNC_TOOL = "mcp__revit-mcp__send_code_to_revit"
# The only code the main session may send. <COMMENT> is the batch comment, e.g. "QAQC2 SYNC A before batch BK (Claude)".
SYNC_TEMPLATE = """var sw = System.Diagnostics.Stopwatch.StartNew();
var t = new TransactWithCentralOptions();
var s = new SynchronizeWithCentralOptions();
s.SetRelinquishOptions(new RelinquishOptions(true));
s.Comment = "<COMMENT>";
document.SynchronizeWithCentral(t, s);
return "SYNC ok s=" + sw.Elapsed.TotalSeconds.ToString("F1") + " IsModified=" + document.IsModified;"""
COMMENT_CHARS = r"[A-Za-z0-9 .,:;()/#'_+=&-]{1,200}"


def _ws(text):
    return re.sub(r"\s+", " ", text).strip()


_head, _tail = _ws(SYNC_TEMPLATE).split("<COMMENT>")
SYNC_RE = re.compile(re.escape(_head) + "(" + COMMENT_CHARS + ")" + re.escape(_tail))


def sync_problem(ti):
    """None if tool_input is the allowed sync call, else the reason it is not."""
    if ti.get("transactionMode") != "none":
        return 'transactionMode must be "none"'
    if ti.get("parameters") not in (None, [], "", {}):
        return "no parameters allowed"
    extra = sorted(set(ti) - {"code", "transactionMode", "parameters"})
    if extra:
        return f"unexpected fields {extra}"
    if not SYNC_RE.fullmatch(_ws(str(ti.get("code", "")))):
        return "code is not exactly the sync template (only the comment may change; no quotes or backslashes)"
    return None


def main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        sys.stderr.write("BLOCKED by orchestrator_guard: could not parse hook input\n")
        sys.exit(2)
    tool = payload.get("tool_name", "")
    if not tool.startswith(PREFIXES) or payload.get("agent_id"):
        sys.exit(0)
    ti = payload.get("tool_input") or {}
    ops = MAIN_ALLOWED.get(tool, set())
    op = str(ti.get("operation", ""))
    if tool in MAIN_ALLOWED and (ops is None or op in ops):
        sys.exit(0)
    if tool == SYNC_TOOL:
        why = sync_problem(ti)
        if why is None:
            sys.exit(0)
        sys.stderr.write(f"BLOCKED by orchestrator_guard: the only Revit code the orchestrator may run is the sync "
                         f"template in orchestrator_guard.py ({why}). Delegate anything else.\n")
        sys.exit(2)
    sys.stderr.write(
        f"BLOCKED by orchestrator_guard: the orchestrator does not call {tool}"
        f"{'.' + op if op else ''} itself. Delegate it: revit-scout / acad-scout for facts and screenshots "
        "(they save the image and return a path plus a one-line description), doc-scout for PDFs, writers for "
        "changes. Allowed here: AutoCAD system status/health/get_backend/runtime/init and drawing info, Revit "
        "get_project_info and get_current_view_info, and the exact Revit sync template.\n")
    sys.exit(2)


if __name__ == "__main__":
    main()
