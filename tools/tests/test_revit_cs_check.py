"""revit_cs_check.py: offline compile of bridge scripts (line mapping, live_line) and the contract lint calibrations.

The compile tests need the .NET 8 runtime, the revit-mcp plugin's Roslyn and RevitAPI.dll; without them they pass
with a SKIP note (the lint tests always run).
"""
import sys
import tempfile
from pathlib import Path

import _util  # noqa: F401  (sys.path)
from _util import run_module_tests, TOOLS

sys.path.insert(0, str(TOOLS))
import revit_cs_check as rc  # noqa: E402

HDR = '// t: transactionMode "auto". parameters: ["dryrun"]\n'
OK_BODY = ('bool dry = parameters != null && parameters.Length > 0 && Convert.ToString(parameters[0]) == "dryrun";\n'
           'if (document.Title == null) return "NO_GO";\n'
           'if (dry) return "DRYRUN\\ndone=0 skipped=0 failed=0";\n'
           'var p = document.ProjectInformation.LookupParameter("Project Name"); if (p != null) p.Set("x");\n'
           'return "ok\\ndone=1 skipped=0 failed=0";\n')


def rules(code, name="T1.cs"):
    _, e, w = rc.lint(name, code)
    return {x["rule"] for x in e}, {x["rule"] for x in w}


def have_toolchain():
    try:
        rc.env()
        return rc.CSC.is_file()
    except SystemExit:
        print("  SKIP compile tests (no .NET 8 / plugin Roslyn / RevitAPI on this machine)")
        return False


def test_blank_keeps_lines_and_offsets():
    code = 'a /* x\ny */ b "s;\\"t" // c\nd'
    b = rc.blank(code)
    assert len(b) == len(code) and b.count("\n") == code.count("\n")
    assert "x" not in b and "c" not in b.split("\n")[1] and b.endswith("d")


def test_lint_clean_script():
    e, w = rules(HDR + OK_BODY)
    assert not e, e


def test_lint_nested_transaction_in_auto():
    e, _ = rules(HDR + OK_BODY.replace('if (dry)', 'var t = new Transaction(document, "x"); t.Start();\nif (dry)'))
    assert "L2-nested-transaction" in e


def test_lint_transaction_guarded_by_ismodifiable_is_ok():
    # 23-073 CB6-R1-T20a: dry-run simulation opens a Transaction only when the document is not modifiable
    code = HDR + OK_BODY.replace('if (dry)', 'Transaction tr = null; if (dry && !document.IsModifiable) '
                                             '{ tr = new Transaction(document, "sim"); tr.Start(); }\nif (dry)')
    e, _ = rules(code)
    assert "L2-nested-transaction" not in e


def test_lint_missing_dryrun_is_error_but_cleanup_warns():
    body = HDR.replace('parameters: ["dryrun"]', "") + OK_BODY.replace('"dryrun"', '"x"')
    e, _ = rules(body)
    assert "L4-dryrun" in e
    e, w = rules(body, "CB6-R1-T1.cleanup.cs")
    assert "L4-dryrun" not in e and "L4-dryrun" in w


def test_lint_tuple_create_is_not_a_write():
    # scripts/BP-probe.cs: System.Tuple.Create is not a model change
    code = '// probe: transactionMode "auto". Makes NO model changes.\nif (document.Title == null) return "x";\n' \
           'var t = System.Tuple.Create(1, 2);\nreturn "n=" + t.Item1 + "\\ndone=0 skipped=0 failed=0";\n'
    e, _ = rules(code)
    assert "L4-dryrun" not in e, e


def test_lint_ui_and_summary_and_mode_none():
    e, w = rules('// t: transactionMode "none"\nTaskDialog.Show("a","b");\nTextNote.Create(document, v, p, "x", t);\n'
                 'return "x";')
    assert {"L9-ui", "L6-summary", "L3-no-transaction", "L4-dryrun"} <= e, e


def test_lint_taskdialog_enum_is_not_ui_and_mode_alias():
    # CSCHECK-T1: TaskDialogResult is an enum; CB6-R1-T15.v6 header says 'mode "auto"'
    code = HDR.replace("transactionMode", "mode") + OK_BODY.replace('if (dry)', 'var r = TaskDialogResult.Ok;\nif (dry)')
    mode, e, w = rc.lint("T.cs", code)
    assert mode == "auto" and not e and "L1-mode" not in {x["rule"] for x in w}, (mode, e, w)


def test_lint_writing_a_result_file_is_allowed():
    code = HDR + OK_BODY.replace('return "ok', 'System.IO.File.WriteAllText(@"C:\\t\\r.txt", "x");\nreturn "ok')
    e, w = rules(code)
    assert "L11-io" not in e


def test_compile_maps_lines_and_live_line():
    if not have_toolchain():
        return
    d = Path(tempfile.mkdtemp(prefix="rcs_t_"))
    f = d / "t.cs"
    f.write_text(HDR + 'Wall w = null;\nvar z = w.NoSuchMember;\n' + OK_BODY, encoding="utf-8")
    out = rc.compile_all([str(f)])[str(f)]
    errs = [x for x in out["errors"] if x["id"] == "CS1061"]
    assert errs and errs[0]["line"] == 3, out
    # the bridge prints Roslyn's zero-based wrapped line: script line + 13 (45 live failures, 2026-11-11)
    assert errs[0]["live_line"] == 3 + 13, errs[0]


def test_compile_namespace_rules_match_the_bridge():
    if not have_toolchain():
        return
    d = Path(tempfile.mkdtemp(prefix="rcs_t_"))
    bad, good = d / "bad.cs", d / "good.cs"
    # live case: 'Pipe' not found (Autodesk.Revit.DB.Plumbing is not imported by the bridge)
    bad.write_text(HDR + 'Pipe p = null;\n' + OK_BODY, encoding="utf-8")
    good.write_text(HDR + 'Autodesk.Revit.DB.Plumbing.Pipe pp = null; TaskDialogResult r = TaskDialogResult.Ok;\n'
                    + OK_BODY, encoding="utf-8")
    out = rc.compile_all([str(bad), str(good)])
    assert any(x["id"] == "CS0246" for x in out[str(bad)]["errors"]), out[str(bad)]
    assert not out[str(good)]["errors"], out[str(good)]  # Autodesk.Revit.UI is imported by the wrapper


def test_selftest():
    if not have_toolchain():
        return
    assert rc.selftest() == 0


if __name__ == "__main__":
    p, f = run_module_tests(globals())
    sys.exit(1 if f else 0)
