"""Regression tests for the swarm PreToolUse guards (2026-11-06). Run before committing any hook change:

    python ~/.claude/hooks/tests/test_guards.py          (exit 1 if any case fails; also runs from repo claude/hooks)

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
sys.path.insert(0, str(HOOKS))
from paths import CLAUDE, SWARM, fwd  # noqa: E402  (expected paths computed exactly as the hooks compute them)

SW, CL = fwd(SWARM), fwd(CLAUDE)
SW_BASH = "/" + SW[0].lower() + SW[2:] if SW[1:2] == ":" else SW  # Git-Bash form /c/Users/...
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
run(BG, ["scripter"], "Bash", {"command": f'python {SW}/tools/lsp_lint.py {SW}/scripts/T.lsp'},
    ALLOW, "scripter: lsp_lint")
run(BG, ["scripter"], "Bash", {"command": f'python {SW}/tools/lsp_dryrun.py {SW}/scripts/T.lsp '
                                          f'"{SW}/backups/r/GP2.9-W PLUMBING.dwg" T --run'}, ALLOW,
    "scripter: lsp_dryrun on a backup copy")
run(BG, ["scripter"], "Bash", {"command": f'python {SW}/tools/lsp_lint.py a.lsp && del a.lsp'}, BLOCK,
    "scripter: chained command")
run(BG, ["scripter"], "Bash", {"command": f'python {SW}/tools/file_deliverable.py a b c d'}, BLOCK,
    "scripter: another agent's tool")

# ---- writer_guard acad-ops ----
run(WG, ["acad-ops"], A + "system", lisp(
    '(vla-activate (vla-item (vla-get-documents (vlax-get-acad-object)) "P003 - PLUMBING (CALCS).dwg"))'), ALLOW,
    "activate a name with parentheses (fix fb765f4)")
run(WG, ["acad-ops"], A + "system", lisp('(vla-Close doc)'), BLOCK, "acad-ops close")
run(WG, ["acad-ops"], A + "entity", {"operation": "erase"}, BLOCK, "acad-ops entity erase")

# ---- 2026-11-09 (plan 2.4): checker tools for scripter / script-debugger, publish + log merge for acad-writer ----
API = f"{CL}/skills/autocad-api/scripts/acad_api_check.py"
LIB = f"{SW}/library/acad/swarm_lib.lsp"
for prof in ("scripter", "script-debugger"):
    run(BG, [prof], "Bash", {"command": f'python {API} --lsp {SW}/scripts/M18-R3-T15.v2.lsp --with {LIB}'}, ALLOW,
        f"{prof}: acad_api_check --lsp --with")
    run(BG, [prof], "Bash", {"command": f'python {SW}/tools/lsp_collide.py {SW}/scripts/M18-R3-T15.v2.lsp {LIB}'}, ALLOW,
        f"{prof}: lsp_collide")
    run(BG, [prof], "Bash", {"command": f'python "{SW_BASH}/tools/lsp_collide.py" {SW_BASH}/scripts/T.lsp'}, ALLOW,
        f"{prof}: lsp_collide, Git-Bash /c/ path")
    run(BG, [prof], "Bash", {"command": f'python {SW}/tools/acad_dsd.py a b c d'}, BLOCK, f"{prof}: acad_dsd is the writer's")
run(BG, ["scripter"], "Bash", {"command": f'python {API} --lsp {SW}/scripts/T.lsp | findstr NOT'}, BLOCK,
    "scripter: api_check piped")
run(BG, ["scripter"], "Bash", {"command": f'python {SW}/../evil/acad_api_check.py --lsp x.lsp'}, BLOCK,
    "scripter: api_check from another folder")
run(BG, ["acad-writer"], "Bash", {"command": f'python {SW}/tools/acad_dsd.py --sheets-file '
                                             f'{SW}/visual/20261109-1100/M18-PUB.sheets.txt "{SW}/visual/20261109-1100/set.pdf" '
                                             f'"{SW}/visual/20261109-1100/set.dsd"'}, ALLOW, "acad-writer: acad_dsd --sheets-file")
run(BG, ["acad-writer"], "Bash", {"command": f'python {SW}/tools/acad_dsd.py "L:/Shared/x/A & B.dwg" P1 a.pdf a.dsd'}, BLOCK,
    "acad-writer: a path with & on the command line (use --sheets-file)")
run(BG, ["acad-writer"], "Bash", {"command": f'python {SW}/tools/merge_ops.py {SW}/2018-930Mansfield/manifests M18-R3-T15'},
    ALLOW, "acad-writer: merge_ops")
run(BG, ["acad-writer"], "Bash", {"command": f'python {SW}/validate.py {SW}/schemas/write.schema.json x.json'}, ALLOW,
    "acad-writer: validate.py kept")
run(BG, ["acad-writer"], "Bash", {"command": f'python {SW}/tools/lsp_dryrun.py a.lsp b.dwg T --run'}, BLOCK,
    "acad-writer: lsp_dryrun is the scripter's")
run(BG, ["acad-ops"], "Bash", {"command": f'python {SW}/tools/merge_ops.py {SW}/2018-930Mansfield/manifests M18-R3-T15'},
    ALLOW, "acad-ops: merge_ops")
run(BG, ["acad-ops"], "Bash", {"command": f'python {SW}/tools/acad_dsd.py a b c d'}, BLOCK, "acad-ops: acad_dsd")
run(BG, ["swarm-validate"], "Bash", {"command": f'python {SW}/tools/merge_ops.py a'}, BLOCK, "swarm-validate: merge_ops")

# ---- writer_guard scripter / script-debugger Write|Edit (Jeremy D1, 2026-11-09) ----
MANS = f"{SW}/22-022 Carmel PCH Apartments (Long Beach, CA)/manifests"
run(WG, ["scripter"], "Edit", {"file_path": f"{SW}/scripts/M18-R3-T15.v2.lsp", "old_string": "a", "new_string": "b"},
    ALLOW, "scripter: Edit scripts/X.lsp")
run(WG, ["scripter"], "Edit", {"file_path": f"{SW}/scripts/M18-R3-T15.v2.lsp".replace("/", "\\")}, ALLOW,
    "scripter: Edit scripts\\X.lsp (backslashes)")
run(WG, ["scripter"], "Edit", {"file_path": f"{SW}/build/gen_agents.py"}, BLOCK, "scripter: Edit build/gen_agents.py")
run(WG, ["scripter"], "Edit", {"file_path": f"{SW}/scripts/../build/gen_agents.lsp"}, BLOCK, "scripter: Edit via ..")
run(WG, ["scripter"], "Edit", {"file_path": f"{SW}/scripts/M18-T1.cs"}, BLOCK, "scripter: Edit a .cs (Write it instead)")
run(WG, ["scripter"], "Edit", {"file_path": f"{SW}/library/acad/swarm_lib.lsp"}, BLOCK, "scripter: Edit the shared library")
run(WG, ["scripter"], "Edit", {"file_path": f"{MANS}/C22-R8-T1.expected.json"}, ALLOW, "scripter: Edit own expected.json")
run(WG, ["scripter"], "Edit", {"file_path": f"{MANS}/C22-R8-T1.write.json"}, BLOCK, "scripter: Edit the writer's write.json")
run(WG, ["scripter"], "Edit", {"file_path": f"{MANS}/C22-R8-T1.debug.json"}, BLOCK, "scripter: Edit the debugger's debug.json")
run(WG, ["scripter"], "Write", {"file_path": f"{SW}/scripts/M18-T1.cleanup.cs", "content": ""}, ALLOW, "scripter: Write a .cs")
run(WG, ["scripter"], "Write", {"file_path": f"{SW}/2018-930Mansfield/manifests/M18-R3-T15.expected.json"}, ALLOW,
    "scripter: Write expected.json")
run(WG, ["scripter"], "Write", {"file_path": f"{SW}/tools/lsp_lint.py"}, BLOCK, "scripter: Write a tool")
run(WG, ["scripter"], "Write", {"file_path": f"{CL}/hooks/writer_guard.py"}, BLOCK, "scripter: Write a hook")
run(WG, ["scripter"], "Read", {"file_path": f"{SW}/build/gen_agents.py"}, ALLOW, "scripter: Read is not judged")
run(WG, ["script-debugger"], "Edit", {"file_path": f"{SW}/scripts/C22-R8-T1.v11.lsp"}, ALLOW, "debugger: Edit scripts/X.lsp")
run(WG, ["script-debugger"], "Write", {"file_path": f"{MANS}/C22-R8-T1.debug.json"}, ALLOW, "debugger: Write debug.json")
run(WG, ["script-debugger"], "Edit", {"file_path": f"{MANS}/C22-R8-T1.verify.json"}, BLOCK, "debugger: Edit verify.json")
run(WG, ["script-debugger"], "Edit", {"file_path": f"{SW}/build/gen_agents.py"}, BLOCK, "debugger: Edit build/gen_agents.py")

# ---- writer_guard acad-ops close (Jeremy D2, 2026-11-09) ----
CLOSE = ('(if (and (eq (vla-get-Saved (setq d (vla-item (vla-get-documents (vlax-get-acad-object)) '
         '"P003 - PLUMBING (CALCS).dwg"))) :vlax-true) (not (equal d (vla-get-ActiveDocument (vlax-get-acad-object))))) '
         '(progn (vla-Close d :vlax-false) "CLOSED") "NOT CLOSED")')
run(WG, ["acad-ops"], A + "system", lisp(CLOSE), ALLOW, "acad-ops: guarded close (Saved, not active, no save)")
run(WG, ["acad-ops"], A + "system", lisp(CLOSE.replace(":vlax-false)", ":vlax-true)")), BLOCK,
    "acad-ops: close that saves")
run(WG, ["acad-ops"], A + "system", lisp(CLOSE.replace("(vla-Close d", "(vla-Close e")), BLOCK,
    "acad-ops: close of a different variable")
run(WG, ["acad-ops"], A + "system", lisp(CLOSE.replace(":vlax-true)", ":vlax-false)", 1)), BLOCK,
    "acad-ops: close of an UNSAVED drawing")
run(WG, ["acad-ops"], A + "system", lisp('(vla-Close (vla-item (vla-get-documents (vlax-get-acad-object)) "x.dwg") :vlax-false)'),
    BLOCK, "acad-ops: close without the Saved check")
run(WG, ["acad-ops"], A + "system", lisp(CLOSE + '(entdel (entlast))'), BLOCK, "acad-ops: close form plus extra code")
run(WG, ["acad-ops"], A + "system", lisp(CLOSE), BLOCK, "acad-ops: close without the lock",
    env=dict(ENV, SWARM_LOCKS_DIR=str(TMP / "nolocks")))

failed = [r for r in results if not r[0]]
for ok, guard, name, code, err in results:
    print(f"{'PASS' if ok else 'FAIL'}  {guard:30} {name}" + ("" if ok else f"  (exit {code}: {err})"))
print(f"\n{len(results) - len(failed)}/{len(results)} passed")
sys.exit(1 if failed else 0)
