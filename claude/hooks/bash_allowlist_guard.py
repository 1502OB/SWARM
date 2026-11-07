"""PreToolUse guard: per-agent Bash allowlist. Usage: bash_allowlist_guard.py <profile>

doc-scout:       the pdf_intake.py helper (scan|crop|find) and ls.
response-writer: the plan-check-response-letter build_letter.py and swarm/tools/docx_check.py.
sheet-reviewer:  swarm/tools/sheet_tool.py render|zoom|text.
markup-locator:  swarm/tools/sheet_tool.py grid|check|zoom.
qaqc-checker:    swarm/tools/sheet_tool.py zoom.
swarm-validate:  swarm/validate.py (writers, verifier). snapshot-taker: tools/snapshot_backup.py.
visual-verifier: tools/visual_crop.py. script-debugger: tools/lspcheck.py, validate.py. filer: tools/file_deliverable.py.
One command per call: no chaining, pipes, redirects or substitution. Exit code 2 blocks the call.
"""
import json
import re
import sys

ARG = r'("[^"$`;&|<>\r\n]*"|[^\s"$`;&|<>]+)'
HOME = r"(C:|/c)/Users/Jeremy"
PCR = (HOME + r"/\.claude/skills/synced/676e33ed-2bf9-4d9f-8b67-7695f8e9bc0d_"
       r"93d96abd-82f7-451b-b27f-57f50d762922/plan-check-response-letter/scripts/build_letter\.py")


def script(path, sub=""):
    return re.compile(rf'^python3? "?{path}"? {sub}{ARG}( {ARG})*\s*$', re.IGNORECASE)


PROFILES = {
    "doc-scout": [
        script(HOME + r"/swarm/tools/pdf_intake\.py", r"(scan|crop|find) "),
        re.compile(rf"^ls( {ARG})*\s*$"),
    ],
    "response-writer": [
        script(PCR),
        script(HOME + r"/swarm/tools/docx_check\.py"),
    ],
    "sheet-reviewer": [script(HOME + r"/swarm/tools/sheet_tool\.py", r"(render|zoom|text) ")],
    "markup-locator": [script(HOME + r"/swarm/tools/sheet_tool\.py", r"(grid|check|zoom) ")],
    "qaqc-checker": [script(HOME + r"/swarm/tools/sheet_tool\.py", r"zoom ")],
    # swarm-upgrade 2026-11-04 (HOOK_PATCH_2026-11-04.md)
    "swarm-validate": [script(HOME + r"/swarm/validate\.py")],
    "snapshot-taker": [script(HOME + r"/swarm/tools/snapshot_backup\.py")],
    "visual-verifier": [script(HOME + r"/swarm/tools/visual_crop\.py")],
    "script-debugger": [script(HOME + r"/swarm/tools/lspcheck\.py"), script(HOME + r"/swarm/validate\.py"),
                        script(HOME + r"/swarm/tools/lsp_lint\.py"), script(HOME + r"/swarm/tools/lsp_dryrun\.py")],
    "filer": [script(HOME + r"/swarm/tools/file_deliverable\.py")],
    # 2026-11-06: scripter checks its own AutoLISP before handing it back (lint + headless dry run on a drawing copy)
    "scripter": [script(HOME + r"/swarm/tools/lsp_lint\.py"), script(HOME + r"/swarm/tools/lsp_dryrun\.py"),
                 script(HOME + r"/swarm/validate\.py")],
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
        "script-debugger": "python .../swarm/tools/lspcheck.py | lsp_lint.py <file.lsp>, lsp_dryrun.py <lsp> <dwg> <TASK>, or python .../swarm/validate.py <schema> <file>",
        "filer": "python .../swarm/tools/file_deliverable.py <source> <destination folder> <new name> <project>",
        "scripter": "python .../swarm/tools/lsp_lint.py <file.lsp>, python .../swarm/tools/lsp_dryrun.py <script.lsp> "
                    "<drawing.dwg> <TASK> [--run], or python .../swarm/validate.py <schema> <file>",
    }.get(profile, "nothing (no help text for this profile)")
    block(f"{profile} may only run: {allowed}. One command per call, no chaining, pipes, redirects or $(). "
          "Read JSON with the Read tool.")


if __name__ == "__main__":
    main()
