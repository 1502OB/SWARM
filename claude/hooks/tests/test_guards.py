"""Regression tests for the swarm PreToolUse guards (2026-11-06). Run before committing any hook change:

    python C:/Users/Jeremy/.claude/hooks/tests/test_guards.py          (exit 1 if any case fails)

Each case runs the guard as Claude Code does: `python <guard>.py <args>` with the tool call JSON on stdin.
Exit 2 = blocked, 0 = allowed. Locks are redirected to a temp folder (SWARM_LOCKS_DIR), so nothing real is touched.
"""
import json
import os
import subprocess
import sys
import tempfile
from pathlib import Path

HOOKS = Path(__file__).resolve().parent.parent
TMP = Path(tempfile.mkdtemp(prefix="guardtest_"))
(TMP / "revit.lock").write_text("T-1 revit-writer", encoding="utf-8")
(TMP / "acad.lock").write_text("T-1 acad-writer", encoding="utf-8")
ENV = dict(os.environ, SWARM_LOCKS_DIR=str(TMP))
ALLOW, BLOCK = 0, 2
results = []


def run(guard, args, tool, tool_input, expect, name, env=ENV):
    p = subprocess.run([sys.executable, str(HOOKS / guard), *args], input=json.dumps(
        {"tool_name": tool, "tool_input": tool_input}), capture_output=True, text=True, env=env)
    ok = p.returncode == expect
    results.append((ok, guard, name, p.returncode, p.stderr.strip()[:160]))


def lisp(code):
    return {"operation": "execute_lisp", "data": {"code": code}}


def lsp_file(name, text):
    f = TMP / name
    f.write_text(text, encoding="utf-8")
    return f.as_posix()


A, R = "mcp__autocad-mcp__", "mcp__revit-mcp__"
RO = "acad_readonly_guard.py"
# ---- acad_readonly_guard (scouts, verifier, planner) ----
run(RO, [], A + "system", lisp('(vl-file-size "C:/x.json")'), ALLOW, "vl-file-size is read-only")
run(RO, [], A + "system", lisp('(vl-file-systime "C:/x.json")'), ALLOW, "vl-file-systime is read-only")
run(RO, [], A + "system", lisp('(length (layoutlist)) ; (entmake ...) only in a comment'), ALLOW, "write call in a comment")
run(RO, [], A + "system", lisp('(setvar "CTAB" "P0.03")'), ALLOW, "CTAB switch")
run(RO, [], A + "entity", {"operation": "list"}, ALLOW, "entity list")
run(RO, [], A + "system", lisp('(entmake (list (cons 0 "LINE")))'), BLOCK, "entmake")
run(RO, [], A + "system", lisp('(vl-file-delete "C:/x")'), BLOCK, "vl-file-delete")
run(RO, [], A + "system", lisp('(vla-SendCommand (vla-get-ActiveDocument (vlax-get-acad-object)) "_.ERASE ")'), BLOCK,
    "vla-SendCommand (was missing)")
run(RO, [], A + "system", lisp('(vla-Move obj p1 p2)'), BLOCK, "vla-Move (was missing)")
run(RO, [], A + "system", lisp('(vla-Explode obj)'), BLOCK, "vla-Explode (was missing)")
run(RO, [], A + "drawing", {"operation": "save"}, BLOCK, "drawing.save")
run(RO, [], A + "entity", {"operation": "erase"}, BLOCK, "entity.erase")

# ---- revit_readonly_code_guard (verifier, scouts, planner) ----
RR = "revit_readonly_code_guard.py"
SC = R + "send_code_to_revit"
run(RR, [], SC, {"transactionMode": "none", "code": 'var t = "CLOSE TO WALL"; // Save the result\nreturn t;'}, ALLOW,
    "Close/Save inside a string and a comment")
run(RR, [], SC, {"transactionMode": "none", "code": '/* Audit, Commit */ return doc.Title;'}, ALLOW, "block comment")
run(RR, [], SC, {"transactionMode": "none", "code": 'doc.Save(); return 1;'}, BLOCK, "doc.Save()")
run(RR, [], SC, {"transactionMode": "none", "code": 'using (var t = new Transaction(doc)) {}'}, BLOCK, "Transaction")
run(RR, [], SC, {"transactionMode": "none", "code": 'typeof(Document).GetMethod("Save").Invoke(doc, null);'}, BLOCK,
    "reflection to a hidden Save")
run(RR, [], SC, {"transactionMode": "auto", "code": 'return 1;'}, BLOCK, "transactionMode auto")

# ---- writer_guard revit (revit-writer) ----
WG = "writer_guard.py"
run(WG, ["revit"], SC, {"code": 'TextNote.Create(doc, v.Id, p, "CLOSE TO WALL - SEE AUDIT", tid); // Close enough'},
    ALLOW, "text note text 'CLOSE ... AUDIT' + comment")
run(WG, ["revit"], SC, {"code": 'doc.Save();'}, BLOCK, "revit-writer Save")
run(WG, ["revit"], SC, {"code": 'doc.Close(false);'}, BLOCK, "revit-writer Close")
run(WG, ["revit"], SC, {"code": 'doc.GetType().InvokeMember("Save", f, null, doc, null);'}, BLOCK, "InvokeMember")
run(WG, ["revit"], SC, {"code": 'return 1;'}, BLOCK, "lock not held",
    env=dict(ENV, SWARM_LOCKS_DIR=str(TMP / "nolocks")))
run(WG, ["revit-ops"], SC, {"transactionMode": "none", "code": '// Delete nothing here\nreturn document.Title;'}, ALLOW,
    "revit-ops: 'Delete' only in a comment")
run(WG, ["revit-ops"], SC, {"transactionMode": "none", "code": 'document.Delete(id);'}, BLOCK, "revit-ops Delete")

# ---- writer_guard acad (acad-writer) ----
run(WG, ["acad"], A + "system", lisp('(setq lbl "EXIT")(entmake (list (cons 0 "TEXT") (cons 1 lbl)))'), ALLOW,
    "label text EXIT is not a command")
run(WG, ["acad"], A + "system", lisp('(command "_.-LAYER" "S" "0" "")'), ALLOW, "ordinary command")
run(WG, ["acad"], A + "system", lisp('(swl:queue-command "_.-PDFATTACH \\"C:/a.pdf\\" 1 0,0 1 0 ")'), ALLOW,
    "queued PDFATTACH via the library")
run(WG, ["acad"], A + "system", lisp('(vla-SendCommand (vla-get-ActiveDocument (vlax-get-acad-object)) "_.-XREF A x ")'),
    ALLOW, "raw SendCommand with an allowed literal")
run(WG, ["acad"], A + "system", lisp('(command "_.QSAVE")'), BLOCK, "QSAVE")
run(WG, ["acad"], A + "system", lisp('(command "_.CLOSE" "_Y")'), BLOCK, "CLOSE")
run(WG, ["acad"], A + "system", lisp('(vla-SaveAs doc "C:/x.dwg")'), BLOCK, "vla-SaveAs")
run(WG, ["acad"], A + "system", lisp('(vla-SendCommand doc cmd)'), BLOCK, "SendCommand with a variable")
run(WG, ["acad"], A + "system", lisp('(vla-SendCommand doc "_.ERASE ALL  ")'), BLOCK, "SendCommand ERASE")
run(WG, ["acad"], A + "system", lisp('(swl:queue-command "_.ERASE ALL  ")'), BLOCK, "queue-command ERASE")
run(WG, ["acad"], A + "system", lisp('(swl:queue-command cmd)'), BLOCK, "queue-command with a variable")
f_ok = lsp_file("ok.lsp", '(defun c:T-RUN () (setq s "EXIT") ; (command "_.QSAVE") in a comment\n (princ))')
f_bad = lsp_file("bad.lsp", '(defun c:T-RUN () (command "_.SAVEAS" "" "C:/x.dwg"))')
run(WG, ["acad"], A + "system", lisp(f'(load "{f_ok}")'), ALLOW, "load a script with EXIT label + commented QSAVE")
run(WG, ["acad"], A + "system", lisp(f'(load "{f_bad}")'), BLOCK, "load a script that SAVEAS")
f_unbal = lsp_file("unbal.lsp", '(defun c:T-RUN (/ a)\n (setq a 1)\n')
f_cl = lsp_file("cl.lsp", '(defun c:T-RUN (/ a) (if (fboundp (quote a)) 1))')
run(WG, ["acad"], A + "system", lisp(f'(load "{f_unbal}")'), BLOCK, "load an unbalanced script (lint E1)")
run(WG, ["acad"], A + "system", lisp(f'(load "{f_cl}")'), BLOCK, "load a script with fboundp (lint E2)")
run(WG, ["acad"], A + "drawing", {"operation": "save", "data": {"path": "C:/x.dwg"}}, BLOCK, "save-as via drawing tool")
run(WG, ["acad"], A + "drawing", {"operation": "save"}, ALLOW, "in-place save")

# ---- bash_allowlist_guard scripter profile (2026-11-06) ----
BG = "bash_allowlist_guard.py"
run(BG, ["scripter"], "Bash", {"command": 'python C:/Users/Jeremy/swarm/tools/lsp_lint.py C:/Users/Jeremy/swarm/scripts/T.lsp'},
    ALLOW, "scripter: lsp_lint")
run(BG, ["scripter"], "Bash", {"command": 'python C:/Users/Jeremy/swarm/tools/lsp_dryrun.py C:/Users/Jeremy/swarm/scripts/T.lsp '
                                          '"C:/Users/Jeremy/swarm/backups/r/GP2.9-W PLUMBING.dwg" T --run'}, ALLOW,
    "scripter: lsp_dryrun on a backup copy")
run(BG, ["scripter"], "Bash", {"command": 'python C:/Users/Jeremy/swarm/tools/lsp_lint.py a.lsp && del a.lsp'}, BLOCK,
    "scripter: chained command")
run(BG, ["scripter"], "Bash", {"command": 'python C:/Users/Jeremy/swarm/tools/file_deliverable.py a b c d'}, BLOCK,
    "scripter: another agent's tool")

# ---- writer_guard acad-ops ----
run(WG, ["acad-ops"], A + "system", lisp(
    '(vla-activate (vla-item (vla-get-documents (vlax-get-acad-object)) "P003 - PLUMBING (CALCS).dwg"))'), ALLOW,
    "activate a name with parentheses (fix fb765f4)")
run(WG, ["acad-ops"], A + "system", lisp('(vla-Close doc)'), BLOCK, "acad-ops close")
run(WG, ["acad-ops"], A + "entity", {"operation": "erase"}, BLOCK, "acad-ops entity erase")

failed = [r for r in results if not r[0]]
for ok, guard, name, code, err in results:
    print(f"{'PASS' if ok else 'FAIL'}  {guard:30} {name}" + ("" if ok else f"  (exit {code}: {err})"))
print(f"\n{len(results) - len(failed)}/{len(results)} passed")
sys.exit(1 if failed else 0)
