"""PreToolUse guard: allow send_code_to_revit only in read-only form.

Used by the verifier subagent. Forces transactionMode "none" and blocks any
Transaction / SubTransaction / TransactionGroup usage. With no open transaction
the Revit API itself throws on any attempt to modify the document, so the
verifier can inspect geometry (e.g. revision cloud curves) without being able
to change the model. Exit code 2 blocks the call.
"""
import json
import re
import sys

DENY = re.compile(
    r"\b(Transaction|SubTransaction|TransactionGroup|TransactionStatus|"
    r"Commit|RollBack|Save|SaveAs|SynchronizeWithCentral|Close|"
    r"PostCommand|UIApplication|Process\.Start|File\.|Directory\.)\b"
)


def block(msg):
    sys.stderr.write("BLOCKED by revit_readonly_code_guard: " + msg + "\n")
    sys.exit(2)


def main():
    try:
        payload = json.load(sys.stdin)
    except Exception:
        block("could not parse hook input")
    if payload.get("tool_name") != "mcp__revit-mcp__send_code_to_revit":
        sys.exit(0)
    ti = payload.get("tool_input") or {}
    if ti.get("transactionMode") != "none":
        block('set transactionMode to "none"; the verifier may only run read-only code')
    m = DENY.search(str(ti.get("code", "")))
    if m:
        block(f"code references '{m.group(1)}'; read-only inspection code only")
    sys.exit(0)


if __name__ == "__main__":
    main()
