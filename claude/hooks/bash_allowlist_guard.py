"""PreToolUse guard: per-agent Bash allowlist. Usage: bash_allowlist_guard.py <profile>

doc-scout:       the pdf_intake.py helper (scan|crop|find) and ls.
response-writer: the plan-check-response-letter build_letter.py and swarm/tools/docx_check.py.
sheet-reviewer:  swarm/tools/sheet_tool.py render|zoom|text.
markup-locator:  swarm/tools/sheet_tool.py grid|check|zoom.
qaqc-checker:    swarm/tools/sheet_tool.py zoom.
swarm-validate:  swarm/validate.py (writers, verifier). snapshot-taker: tools/snapshot_backup.py.
visual-verifier: tools/visual_crop.py. script-debugger: tools/lspcheck.py, validate.py. filer: tools/file_deliverable.py.
scripter + script-debugger (2026-11-09): also acad_api_check.py --lsp (autocad-api skill) and tools/lsp_collide.py.
acad-writer: validate.py, tools/acad_dsd.py (--sheets-file for several sheets), tools/merge_ops.py. acad-ops: merge_ops.py.
Paths are computed at runtime (hooks/paths.py: SWARM_HOME, CLAUDE_CONFIG_DIR, else under the user's home).
One command per call: no chaining, pipes, redirects or substitution. Exit code 2 blocks the call.
"""
import json
import re
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
from paths import CLAUDE, SWARM, path_re  # noqa: E402  (2026-11-09: no hard-coded user folder)

ARG = r'("[^"$`;&|<>\r\n]*"|[^\s"$`;&|<>]+)'
# claude.ai-synced skills live under skills/synced/<account id>_<skill id>/ (ids differ per user)
PCR = (path_re(CLAUDE, "skills/synced") + r"[/\\][0-9a-f-]+_[0-9a-f-]+[/\\]"
       + path_re("", "plan-check-response-letter/scripts/build_letter.py"))


def sw(rel):
    """Regex for a file under the swarm folder, e.g. sw("tools/lsp_lint.py")."""
    return path_re(SWARM, rel)


def script(path, sub=""):
    return re.compile(rf'^python3? "?{path}"? {sub}{ARG}( {ARG})*\s*$', re.IGNORECASE)


API_CHECK = path_re(CLAUDE, "skills/autocad-api/scripts/acad_api_check.py")
# 2026-11-09 (plan 2.4, LESSONS A40/A48): scripter + script-debugger check their own .lsp (api checker, collisions);
# acad-writer writes publish DSDs and merges attempt logs; acad-ops merges attempt logs. Multi-sheet publish takes
# --sheets-file <list.txt> so DWG paths with "&" never appear on the command line.
LSP_CHECKS = [script(API_CHECK), script(sw("tools/lsp_collide.py"))]

PROFILES = {
    "doc-scout": [
        script(sw("tools/pdf_intake.py"), r"(scan|crop|find) "),
        re.compile(rf"^ls( {ARG})*\s*$"),
    ],
    "response-writer": [
        script(PCR),
        script(sw("tools/docx_check.py")),
    ],
    "sheet-reviewer": [script(sw("tools/sheet_tool.py"), r"(render|zoom|text) ")],
    "markup-locator": [script(sw("tools/sheet_tool.py"), r"(grid|check|zoom) ")],
    "qaqc-checker": [script(sw("tools/sheet_tool.py"), r"zoom ")],
    # swarm-upgrade 2026-11-04 (HOOK_PATCH_2026-11-04.md)
    "swarm-validate": [script(sw("validate.py"))],
    "snapshot-taker": [script(sw("tools/snapshot_backup.py"))],
    "visual-verifier": [script(sw("tools/visual_crop.py"))],
    "script-debugger": [script(sw("tools/lspcheck.py")), script(sw("validate.py")),
                        script(sw("tools/lsp_lint.py")), script(sw("tools/lsp_dryrun.py"))] + LSP_CHECKS,
    "filer": [script(sw("tools/file_deliverable.py"))],
    # 2026-11-06: scripter checks its own AutoLISP before handing it back (lint + headless dry run on a drawing copy)
    "scripter": [script(sw("tools/lsp_lint.py")), script(sw("tools/lsp_dryrun.py")),
                 script(sw("validate.py"))] + LSP_CHECKS,
    # 2026-11-09: writers keep validate.py; acad-writer adds the DSD writer and the attempt-log merge
    "acad-writer": [script(sw("validate.py")), script(sw("tools/acad_dsd.py")), script(sw("tools/merge_ops.py"))],
    "acad-ops": [script(sw("tools/merge_ops.py"))],
}
SHEET = "python .../swarm/tools/sheet_tool.py"


def block(msg):
    sys.stderr.write("BLOCKED by bash_allowlist_guard: " + msg + "\n")
    sys.exit(2)


def main():
    profile = sys.argv[1] if len(sys.argv) > 1 else ""
    if profile not in PROFILES:
        block(f"unknown profile '{profile}'")
    try:
        payload = json.load(sys.stdin)
    except Exception:
        block("could not parse hook input")
    if payload.get("tool_name") != "Bash":
        sys.exit(0)
    cmd = str((payload.get("tool_input") or {}).get("command", "")).strip()
    if any(p.match(cmd) for p in PROFILES[profile]):
        sys.exit(0)
    allowed = {
        "doc-scout": "python .../swarm/tools/pdf_intake.py scan|crop|find <args>, or ls <paths>",
        "response-writer": "python .../plan-check-response-letter/scripts/build_letter.py <json> -o <docx>, "
                           "or python .../swarm/tools/docx_check.py <docx>",
        "sheet-reviewer": f"{SHEET} render|zoom|text <args>",
        "markup-locator": f"{SHEET} grid|check|zoom <args>",
        "qaqc-checker": f"{SHEET} zoom <args>",
        "swarm-validate": "python .../swarm/validate.py <schema> <file>",
        "snapshot-taker": "python .../swarm/tools/snapshot_backup.py <run-id> <file> [<file> ...]",
        "visual-verifier": "python .../swarm/tools/visual_crop.py <pdf> <page> <x0,y0,x1,y1> <task-id> [--units in]",
        "script-debugger": "python .../swarm/tools/lspcheck.py | lsp_lint.py | lsp_collide.py <file.lsp>, lsp_dryrun.py "
                           "<lsp> <dwg> <TASK>, python .../.claude/skills/autocad-api/scripts/acad_api_check.py --lsp "
                           "<file.lsp> [--with <lib.lsp>], or python .../swarm/validate.py <schema> <file>",
        "filer": "python .../swarm/tools/file_deliverable.py <source> <destination folder> <new name> <project>",
        "scripter": "python .../swarm/tools/lsp_lint.py | lsp_collide.py <file.lsp>, python .../swarm/tools/lsp_dryrun.py "
                    "<script.lsp> <drawing.dwg> <TASK> [--run], python .../.claude/skills/autocad-api/scripts/"
                    "acad_api_check.py --lsp <file.lsp> [--with <lib.lsp>], or python .../swarm/validate.py <schema> <file>",
        "acad-writer": "python .../swarm/validate.py <schema> <file>, python .../swarm/tools/acad_dsd.py <args> "
                       "(several sheets: --sheets-file <list.txt>, so no \"&\" path is on the command line), or "
                       "python .../swarm/tools/merge_ops.py <args>",
        "acad-ops": "python .../swarm/tools/merge_ops.py <args>",
    }.get(profile, "nothing (no help text for this profile)")
    block(f"{profile} may only run: {allowed}. One command per call, no chaining, pipes, redirects or $(). "
          "Read JSON with the Read tool.")


if __name__ == "__main__":
    main()
