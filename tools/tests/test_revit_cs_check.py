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
# R116 / L16: the whole body sits inside one top-level try whose catch returns FAILED @<stage>
OK_BODY = 'string step = "start";\ntry {\n' + OK_BODY + '} catch (Exception ex) { return "FAILED @" + step + ": " + ex.Message; }\n'


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


def test_lint_outer_catch_l16():
    # R116 (ATZ-R2-T1/R13-T1): a writing script with no top-level try/catch-return, or a Revit call before it, is an error
    bare = ('// t: transactionMode "none". parameters: ["dryrun"]\nbool dry = true;\n'
            'if (dry) return "DRYRUN\\ndone=0 skipped=0 failed=0";\n'
            'var t = new Transaction(document, "x"); t.Start(); document.Delete(new ElementId(5L)); t.Commit();\n'
            'return "ok\\ndone=1 skipped=0 failed=0";\n')
    e, _ = rules(bare)
    assert "L16-outer-catch" in e
    pre = OK_BODY.replace('string step = "start";', 'var el = document.GetElement(new ElementId(5L));\nstring step = "s";')
    e, _ = rules(HDR + pre)
    assert "L16-outer-catch" in e
    lam = OK_BODY.replace('string step = "start";', 'Func<long, Element> get = id => document.GetElement(new ElementId(id));'
                          '\nbool ws = document.IsWorkshared;\nstring step = "start";')
    e, w = rules(HDR + lam)
    assert "L16-outer-catch" not in e | w, (e, w)
    ro = ('// t: transactionMode "none". parameters: ["dryrun"]\n'
          'var c = new FilteredElementCollector(document).GetElementCount();\nreturn "n=" + c + "\\ndone=0 skipped=0 failed=0";\n')
    e, w = rules(ro)
    assert "L16-outer-catch" in w and "L16-outer-catch" not in e


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


# ------------------------------------------------------------------ L1 / L5 false positives (2026-11-25)
def test_lint_l5_own_transaction_rolled_back_on_dryrun_is_ok():
    # R99 own-Transaction pattern: mode none, the dry run performs everything then RollBack()s its Transaction
    code = ('// t: transactionMode "none". parameters: ["dryrun"]\nstring step = "start";\ntry {\n'
            'bool dry = parameters != null && Convert.ToString(parameters[0]) == "dryrun";\n'
            'if (document.Title == null) return "NO_GO";\n'
            'Transaction tx = new Transaction(document, "x"); tx.Start();\n'
            'var p = document.ProjectInformation.LookupParameter("Project Name"); p.Set("x");\n'
            'if (dry) { TransactionStatus s = tx.RollBack(); return "DRYRUN " + s + "\\ndone=0 skipped=0 failed=0"; }\n'
            'tx.Commit(); return "ok\\ndone=1 skipped=0 failed=0";\n'
            '} catch (Exception ex) { return "FAILED @" + step + ": " + ex.Message; }\n')
    e, w = rules(code)
    assert "L5-dryrun-order" not in w, w
    # the same change with no rollback in the dry branch still warns
    e, w = rules(code.replace("TransactionStatus s = tx.RollBack();", "string s = \"\";"))
    assert "L5-dryrun-order" in w
    # a TransactionGroup rolled back on dry is R47 (geometry ok) / R86 (view blocker): warn with that note
    grp = code.replace('Transaction tx = new Transaction(document, "x"); tx.Start();',
                       'TransactionGroup tx = new TransactionGroup(document, "x"); tx.Start();\n'
                       'Transaction t1 = new Transaction(document, "s"); t1.Start();')
    _, errs, warns = rc.lint("T.cs", grp)
    l5 = [x for x in warns if x["rule"] == "L5-dryrun-order"]
    assert l5 and "R86" in l5[0]["message"], warns


def test_lint_l5_transaction_constructor_is_not_a_change():
    # 12 overruled L5 findings fired on `new SubTransaction(document)` before the dry branch
    code = HDR + OK_BODY.replace('if (dry)', 'SubTransaction st = new SubTransaction(document);\nif (dry)')
    e, w = rules(code)
    assert "L5-dryrun-order" not in w, w


def test_lint_l1_mode_inferred_and_silent_when_read_only():
    ro = ('// t: read-only probe. parameters: ["dryrun"]\nstring step = "s";\ntry {\nif (document.Title == null) '
          'return "x";\nreturn "n=" + new FilteredElementCollector(document).GetElementCount() + "\\ndone=0 skipped=0 '
          'failed=0";\n} catch (Exception ex) { return "FAILED @" + step + ex.Message; }\n')
    info = {}
    mode, e, w = rc.lint("T.cs", ro, None, info)
    assert mode is None and "L1-mode" not in {x["rule"] for x in w} and info["transaction_mode_inferred"] is None
    sub = HDR.replace('transactionMode "auto". ', '') + OK_BODY.replace('if (dry)', 'var st = new SubTransaction(document);\nif (dry)')
    info = {}
    mode, e, w = rc.lint("T.cs", sub, None, info)
    assert info["transaction_mode_inferred"] == "auto" and any('"auto"' in x["message"] for x in w if x["rule"] == "L1-mode")
    cleanup = "// cleanup: restores the value. Auto transaction.\n" + OK_BODY
    mode, e, w = rc.lint("X.cleanup.cs", cleanup)
    assert mode == "auto" and "L1-mode" not in {x["rule"] for x in w}


# ------------------------------------------------------------------ gotcha rules G1-G4 (2026-11-25)
def gotcha(body, name="T1.cs"):
    """(error rules, warning rules, waivers) for HDR + OK_BODY with `body` inserted before the dry branch."""
    waived = []
    _, e, w = rc.lint(name, HDR + OK_BODY.replace("if (dry)", body + "\nif (dry)", 1), waived)
    return {x["rule"] for x in e}, {x["rule"] for x in w}, waived


TEE = ('Connector a = null, b2 = null, c = null;\n'
       'FamilyInstance tee = document.Create.NewTeeFitting(a, b2, c);')


def test_g1_tee_without_slope_check_fails_and_waiver_passes():
    e, w, _ = gotcha(TEE)
    assert "G1-tee-on-sloped" in e
    # the CB6-R1-T20a v1 shape: route slope reporting does not count, takeoff too
    e, _, _ = gotcha("var t = document.Create.NewTakeoffFitting(null, null);")
    assert "G1-tee-on-sloped" in e
    for guard in ("XYZ dir = (pc.Curve as Line).Direction; if (Math.Abs(dir.Z) > 0.001) return \"NO_GO sloped\";",
                  "double mSlope = (mHi.Z - mLo.Z) / 10.0;",
                  "var sl = pipe.get_Parameter(BuiltInParameter.RBS_PIPE_SLOPE).AsDouble();",
                  "if (Math.Abs(e[0].Z - e[1].Z) > 0.01) return \"NO_GO\";"):
        e, _, _ = gotcha(guard + "\n" + TEE)
        assert "G1-tee-on-sloped" not in e, guard
    e, w, waived = gotcha("// gotcha-ok: G1 tees only between pipes this script draws level\n" + TEE)
    assert "G1-tee-on-sloped" not in e | w and waived and waived[0]["rule"] == "G1-tee-on-sloped"
    assert waived[0]["reason"].startswith("tees only")


def test_gotcha_waiver_needs_a_reason():
    e, w, waived = gotcha("// gotcha-ok: G1\n" + TEE)
    assert "G1-tee-on-sloped" in e and "G0-waiver" in w and not waived
    e, w, waived = gotcha("// gotcha-ok: G1-tee-on-sloped -   \n" + TEE)
    assert "G1-tee-on-sloped" in e and not waived


def test_g2_system_type_change():
    e, _, _ = gotcha("Autodesk.Revit.DB.Plumbing.Pipe sp = null; sp.SetSystemType(new ElementId(5L));")
    assert "G2-system-type-change" in e
    e, _, _ = gotcha("Parameter tp = el.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM);\n"
                     "if (!tp.Set(new ElementId(7L))) return \"x\";")
    assert "G2-system-type-change" in e
    # reading the system type, then reusing the variable name for another parameter, is not a change
    e, w, _ = gotcha("Parameter p = el.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM);\n"
                     "string st0 = p.AsValueString();\np = el.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);\n"
                     "p.Set(1);")
    assert "G2-system-type-change" not in e | w
    e, _, waived = gotcha("// gotcha-ok: G2 stub disconnected first, neighbours re-read after commit\n"
                          "Autodesk.Revit.DB.Plumbing.Pipe sp = null; sp.SetSystemType(new ElementId(5L));")
    assert "G2-system-type-change" not in e and waived[0]["severity"] == "error"


def test_g2_connect_without_system_read_warns():
    body = "Connector a = null, b2 = null; a.ConnectTo(b2);"
    e, w, _ = gotcha(body)
    assert "G2-system-type-change" in w and "G2-system-type-change" not in e
    _, w, _ = gotcha(body + "\nvar sys0 = a.MEPSystem;")
    assert "G2-system-type-change" not in w
    _, w = rules(HDR + OK_BODY.replace("if (dry)", body + "\nif (dry)"), "T.cleanup.cs")
    assert "G2-system-type-change" not in w  # a cleanup restoring a connection


def test_g3_exact_name_lookup():
    e, _, _ = gotcha('var sym = new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>()'
                     '.FirstOrDefault(s => s.FamilyName == "ARCH E1 30x42");')  # AB-R1-T12
    assert "G3-exact-name" not in e  # a warning since the 2026-11-25 follow-up (18 working finals hit it)
    for hit in ('var sym = new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>()'
                '.FirstOrDefault(s => s.FamilyName == "ARCH E1 30x42");',
                'View v0 = null; if ("P-100" == v0.Name) return "x";',
                'View v0 = null; bool ok = string.Equals(v0.Name, "Level 1");'):
        e, w, _ = gotcha(hit)
        assert "G3-exact-name" in w and "G3-exact-name" not in e, hit
    for fine in ('FamilySymbol fs = null; bool t = fs.FamilyName == "MEP_Tag_Revision" && fs.Name == "Revision Tag";',
                 'View v0 = null; bool t = string.Equals(v0.Name.Trim(), "Level 1", StringComparison.OrdinalIgnoreCase);',
                 'View v0 = null; if (v0.Name != "P6.00") return "NO_GO sheet";',
                 'Parameter q = null; bool t = q.Definition.Name == "Comments";',
                 'GraphicsStyle sc = null; string LS = "x"; bool t = sc.Name == "<" + LS + ">";'):
        e, w, _ = gotcha(fine)
        assert "G3-exact-name" not in e | w, fine
    e, _, waived = gotcha('// gotcha-ok: G3 line style created by this script under this exact name\n'
                          'GraphicsStyle sc = null; bool t = sc.Name == "L76 Frame White";')
    assert "G3-exact-name" not in e | w and waived


def test_g4_obstacle_search_without_model_categories_warns():
    body = ('var obstacles = new FilteredElementCollector(document, v.Id).OfCategory(BuiltInCategory.OST_TextNotes)'
            '.ToElements();\nTextNote n = TextNote.Create(document, v.Id, XYZ.Zero, "x", tt);')
    e, w, _ = gotcha(body)
    assert "G4-obstacle-categories" in w and "G4-obstacle-categories" not in e
    _, w, _ = gotcha(body + "\nvar fx = new FilteredElementCollector(document, v.Id)"
                            ".OfCategory(BuiltInCategory.OST_PlumbingFixtures).ToElements();")
    assert "G4-obstacle-categories" not in w


def test_gotcha_error_fails_the_check_status():
    if not have_toolchain():
        return
    d = Path(tempfile.mkdtemp(prefix="rcs_g_"))
    f = d / "t.cs"
    f.write_text(HDR + OK_BODY.replace("if (dry)", TEE + "\nif (dry)"), encoding="utf-8")
    r = rc.check([str(f)])[0]
    assert r["status"] == "FAIL" and "G1-tee-on-sloped" in {x["rule"] for x in r["lint"]["errors"]}


# ------------------------------------------------------------------ precheck reuse kinds (2026-11-25)
def _reuse_setup(folder, script_text=b'// T-1 v1\r\nreturn "done";\r\n'):
    script, cleanup = folder / "T-1.cs", folder / "T-1.cleanup.cs"
    script.write_bytes(script_text)
    cleanup.write_bytes(b'return "clean";\r\n')
    task, expected = folder / "T-1.task.json", folder / "T-1.expected.json"
    task.write_text('{"task_id": "T-1"}', encoding="utf-8")
    expected.write_text('{"items": []}', encoding="utf-8")
    rows = [{"path": str(p), "sha256": rc.read_source(p)[0], "compile": "PASS"} for p in (script, cleanup)]
    return script, cleanup, task, expected, rows


def _reuse(script, precheck, *extra):
    import contextlib
    import io
    from unittest.mock import patch
    out = io.StringIO()
    with patch.object(rc, "toolchain_key", return_value="tc"), patch.object(rc, "check", return_value=[]):
        with contextlib.redirect_stdout(out):
            code = rc.main([str(script), "--reuse-precheck", str(precheck), *extra, "--emit-file"])
    return code, out.getvalue()


def test_bound_precheck_reused_across_runs_until_task_or_expected_changes():
    import json
    d = Path(tempfile.mkdtemp(prefix="rcs_b_"))
    script, cleanup, task, expected, rows = _reuse_setup(d)
    pre = d / "T-1.precheck.json"
    pre.write_text(json.dumps({"status": "PASS", "run_id": "old-run", "task_id": "T-1", "toolchain": "tc",
                               "bound": rc.bound_for(task, expected), "scripts": rows}), encoding="utf-8")
    code, out = _reuse(script, pre, "--run-id", "new-session", "--task-id", "T-1")
    assert code == 0 and json.loads(out)["precheck_kind"] == "bound", out
    assert _reuse(script, pre)[0] == 0  # no run/task id needed: the recorded paths are re-hashed
    expected.write_text('{"items": [1]}', encoding="utf-8")
    assert _reuse(script, pre, "--run-id", "new-session", "--task-id", "T-1")[0] == 3
    expected.write_text('{"items": []}', encoding="utf-8")
    task.write_text('{"task_id": "T-1", "x": 1}', encoding="utf-8")
    assert _reuse(script, pre)[0] == 3
    task.write_text('{"task_id": "T-1"}', encoding="utf-8")
    assert _reuse(script, pre)[0] == 0
    # another task id resolves <precheck dir>/<that id>.task.json, which does not exist: no reuse
    assert _reuse(script, pre, "--task-id", "T-2")[0] == 3
    cleanup.write_bytes(b'return "changed";\r\n')
    assert _reuse(script, pre)[0] == 3


def test_bound_precheck_without_expected_json():
    import json
    d = Path(tempfile.mkdtemp(prefix="rcs_b_"))
    script, _, task, expected, rows = _reuse_setup(d)
    expected.unlink()
    b = rc.bound_for(task, d / "T-1.expected.json")
    assert b["expected_sha256"] is None and b["task_sha256"]
    pre = d / "T-1.precheck.json"
    pre.write_text(json.dumps({"status": "PASS", "run_id": "r", "task_id": "T-1", "toolchain": "tc", "bound": b,
                               "scripts": rows}), encoding="utf-8")
    assert _reuse(script, pre)[0] == 0
    expected.write_text("{}", encoding="utf-8")  # an expected.json appearing later changes the bound
    assert _reuse(script, pre)[0] == 3


def test_library_precheck_matches_on_sha_and_toolchain_only():
    import json
    from unittest.mock import patch
    d = Path(tempfile.mkdtemp(prefix="rcs_l_"))
    lib = d / "library" / "revit"
    lib.mkdir(parents=True)
    script = lib / "export-visual-sheet.cs"
    script.write_bytes(b'// export-visual-sheet v3\r\nreturn "x";\r\n')
    pre = d / "export-visual-sheet.precheck.json"
    pre.write_text(json.dumps({"status": "PASS", "run_id": "r1", "task_id": "LIB", "toolchain": "tc", "library": True,
                               "scripts": [{"path": str(script), "sha256": rc.read_source(script)[0],
                                            "compile": "PASS"}]}), encoding="utf-8")
    with patch.object(rc, "LIBRARY", d / "library"):
        assert _reuse(script, pre, "--run-id", "any", "--task-id", "VIS-T9")[0] == 0
        assert _reuse(script, pre)[0] == 0
        script.write_bytes(b'// export-visual-sheet v4\r\nreturn "y";\r\n')
        assert _reuse(script, pre)[0] == 3
    # "library": true on a script outside swarm/library/ never matches
    other = d / "T-1.cs"
    other.write_bytes(b'return "x";\r\n')
    pre.write_text(json.dumps({"status": "PASS", "toolchain": "tc", "library": True, "run_id": "r", "task_id": "t",
                               "scripts": [{"path": str(other), "sha256": rc.read_source(other)[0],
                                            "compile": "PASS"}]}), encoding="utf-8")
    with patch.object(rc, "LIBRARY", d / "library"):
        assert _reuse(other, pre)[0] == 3
        assert rc.main([str(other), "--evidence", "--library"]) == 2


def test_legacy_precheck_keeps_run_and_task_binding():
    import json
    d = Path(tempfile.mkdtemp(prefix="rcs_g_"))
    script, _, _, _, rows = _reuse_setup(d)
    pre = d / "T-1.precheck.json"
    pre.write_text(json.dumps({"status": "PASS", "run_id": "run", "task_id": "T-1", "toolchain": "tc",
                               "scripts": rows}), encoding="utf-8")
    assert _reuse(script, pre, "--run-id", "run", "--task-id", "T-1")[0] == 0
    assert _reuse(script, pre, "--run-id", "other", "--task-id", "T-1")[0] == 3
    assert _reuse(script, pre)[0] == 3  # legacy needs both ids
    assert rc.precheck_kind({"bound": {}}) == "bound" and rc.precheck_kind({}) == "legacy"


def test_reuse_blocked_by_unwaived_gotcha_even_if_precheck_says_pass():
    import json
    d = Path(tempfile.mkdtemp(prefix="rcs_g_"))
    body = (HDR + OK_BODY.replace("if (dry)", TEE + "\nif (dry)")).encode("utf-8")
    script, _, task, expected, rows = _reuse_setup(d, body)
    pre = d / "T-1.precheck.json"
    pre.write_text(json.dumps({"status": "PASS", "run_id": "r", "task_id": "T-1", "toolchain": "tc",
                               "bound": rc.bound_for(task, expected), "scripts": rows}), encoding="utf-8")
    code, out = _reuse(script, pre)
    assert code == 3 and "G1-tee-on-sloped" in out, out


def test_legacy_precheck_not_rejudged_by_gotcha_rules():
    # 2026-11-25 follow-up: a script prechecked before the G rules existed (no "bound") and written ahead for a
    # current job is still reused; the rules apply to prechecks made under them.
    import json
    d = Path(tempfile.mkdtemp(prefix="rcs_lg_"))
    body = (HDR + OK_BODY.replace("if (dry)", TEE + "\nif (dry)")).encode("utf-8")
    script, _, _, _, rows = _reuse_setup(d, body)
    pre = d / "T-1.precheck.json"
    pre.write_text(json.dumps({"status": "PASS", "run_id": "run", "task_id": "T-1", "toolchain": "tc",
                               "scripts": rows}), encoding="utf-8")
    code, out = _reuse(script, pre, "--run-id", "run", "--task-id", "T-1")
    assert code == 0, out


def test_evidence_records_bound_and_waivers():
    import contextlib
    import io
    import json
    from unittest.mock import patch
    d = Path(tempfile.mkdtemp(prefix="rcs_e_"))
    script, _, task, expected, _ = _reuse_setup(d, ("// gotcha-ok: G1 tee between new level pipes\n" + HDR +
                                                    OK_BODY.replace("if (dry)", TEE + "\nif (dry)")).encode("utf-8"))
    fake = {str(script): {"errors": [], "warnings": []}}
    out = io.StringIO()
    with patch.object(rc, "toolchain_key", return_value="tc"), patch.object(rc, "compile_all", return_value=fake):
        with contextlib.redirect_stdout(out):
            code = rc.main([str(script), "--evidence", "--task-json", str(task), "--expected-json", str(expected)])
    row = json.loads(out.getvalue())[0]
    assert code == 0 and row["status"] == "PASS", row
    assert row["bound"]["task_sha256"] == rc.file_sha256(task) and row["bound"]["expected_sha256"] == rc.file_sha256(expected)
    assert row["lint"]["waivers"][0]["rule"] == "G1-tee-on-sloped"


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
