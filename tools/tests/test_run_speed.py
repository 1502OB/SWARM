"""Regressions for queue gates, stale evidence, compiler reuse and parallel QA/QC prep."""
import hashlib
import contextlib
import io
import json
import os
import subprocess
import sys
from pathlib import Path
from tempfile import TemporaryDirectory
from unittest.mock import patch

import pymupdf

from _util import TOOLS
import check_task
import model_index
import revit_cs_check
import run_queue
import visual_crop


def test_queue_runs_independent_work_and_holds_uncertain_lane():
    def stage(name, lane, *after):
        return {"id": name, "task": name.split("-")[0], "lane": lane, "after": list(after)}

    queue = run_queue.new_queue({"run_id": "test", "stages": [
        stage("T1-write", "revit"), stage("T2-scout", "revit"),
        stage("T1-verify", "revit", "T1-write"), stage("T3-check", "offline"),
        stage("T4-write", "acad"), stage("T1-approval", "human")
    ]}, 0)
    assert set(dict(run_queue.available(queue))) == {"T1-write", "T3-check", "T4-write", "T1-approval"}
    run_queue.transition(queue, "start", "T1-write", None, 1)
    run_queue.transition(queue, "finish", "T1-write", "uncertain", 4)
    assert not any(row["lane"] == "revit" for _, row in run_queue.available(queue))
    run_queue.transition(queue, "resolve", "T1-write", "pass", 8)
    assert "T2-scout" in dict(run_queue.available(queue))
    report = run_queue.report(queue, [("revit", 2, 2), ("acad", 9, 1)])
    assert report["seconds_by_task"]["T1"] == {
        "queue_wait_s": 1, "bridge_lane_s": 3, "reconciliation_s": 4, "bridge_tool_s": 2}
    assert report["unassigned_bridge_calls"] == 1


def test_queue_retries_wait_for_new_precheck():
    plan = {"run_id": "test", "stages": [
        {"id": "write", "task": "T1", "lane": "acad"}, {"id": "new-check", "task": "T1", "lane": "offline"}]}
    queue = run_queue.new_queue(plan, 0)
    run_queue.transition(queue, "start", "write", None, 1)
    run_queue.transition(queue, "finish", "write", "fail", 2)
    run_queue.transition(queue, "retry", "write", None, 3, ["new-check"])
    assert set(dict(run_queue.available(queue))) == {"new-check"}
    run_queue.transition(queue, "start", "new-check", None, 4)
    run_queue.transition(queue, "finish", "new-check", "pass", 5)
    assert set(dict(run_queue.available(queue))) == {"write"}


def test_document_scope_invalidates_ancestors_and_stale_scout():
    with TemporaryDirectory() as temp:
        root = Path(temp)
        with patch.object(model_index, "SWARM", root), patch.object(check_task, "SWARM", root):
            run = "20261120-T"
            name = "C22-R1-T1"
            model_index.save_json(root / "manifests/project.json", {"task_prefix": "C22"})
            task = root / f"{name}.task.json"
            model_index.save_json(task, {"task_id": name, "run_id": run, "app": "revit", "document_id": "C:/A.rvt",
                                         "lookup_scopes": ["sheets:P1.01"], "target_ids": [123456]})
            scout = root / f"{name}.scout.json"
            report = {"task_id": name, "run_id": run, "app": "revit", "document_id": "c:\\a.rvt",
                      "confirmed_target_ids": [123456],
                      "index_scopes": {scope: {"complete": True, "entries": [123456]} for scope in
                                       ("sheets", "sheets:P1.01", "views:123456")}}
            model_index.save_json(scout, report)
            model_index.merge_index("revit", "C:/A.rvt", run, report)
            assert not check_task.check(task)[3]
            assert not model_index.read_index("revit", "C:/B.rvt", run)["scopes"]
            assert not model_index.read_index("revit", "C:/A.rvt", "20261120-OTHER")["scopes"]
            removed = model_index.invalidate("revit", "C:/A.rvt", run, ["sheets:p1.01"])
            assert set(removed) == {"sheets", "sheets:p1.01"}
            assert list(model_index.read_index("revit", "C:/A.rvt", run)["scopes"]) == ["views:123456"]
            assert check_task.check(task, scout)[3] == ["target 123456 has no current evidence"]
            try:
                model_index.merge_index("revit", "C:/A.rvt", run, report, scout.stat().st_mtime_ns)
            except ValueError as error:
                assert "predates" in str(error)
            else:
                assert False, "old scout must not repopulate invalidated scopes"


def test_compile_reuse_binds_run_toolchain_and_exact_script_and_cleanup():
    rc = revit_cs_check
    with TemporaryDirectory() as temp:
        folder = Path(temp)
        script, cleanup = folder / "change.cs", folder / "change.cleanup.cs"
        script.write_bytes(b'return "done";\r\n')
        cleanup.write_bytes(b'return "clean";\r\n')
        precheck = folder / "precheck.json"
        report = {"status": "PASS", "run_id": "run", "task_id": "task", "toolchain": "current",
                  "scripts": [{"path": str(file), "sha256": rc.read_source(file)[0], "compile": "PASS"}
                              for file in (script, cleanup)]}
        precheck.write_text(json.dumps(report), encoding="utf-8")

        def invoke(run="run"):
            out = io.StringIO()
            args = [str(script), "--reuse-precheck", str(precheck), "--run-id", run, "--task-id", "task", "--emit-code"]
            with contextlib.redirect_stdout(out):
                status = rc.main(args)
            return status, out.getvalue()

        with patch.object(rc, "toolchain_key", return_value="current"), patch.object(rc, "check", return_value=[]) as compile:
            code, result = invoke()
            assert code == 0 and not compile.called
            assert json.loads(result)["code"] == 'return "done";\n'
            assert invoke("other")[0] == 3
            cleanup.write_bytes(b'return "changed cleanup";\n')
            assert invoke()[0] == 3
            assert compile.call_count == 2


def test_emit_file_gives_codefile_args_not_code():
    """R70/R72 (2026-11-25): --emit-file returns sha256 + absolute path + line-1 marker, never the script text."""
    rc = revit_cs_check
    with TemporaryDirectory() as temp:
        folder = Path(temp)
        script = folder / "T-1.cs"
        script.write_bytes(b'// T-1 v3\r\nreturn "done";\r\n')
        precheck = folder / "precheck.json"
        digest = rc.read_source(script)[0]
        precheck.write_text(json.dumps({"status": "PASS", "run_id": "run", "task_id": "T-1", "toolchain": "current",
                                        "scripts": [{"path": str(script), "sha256": digest, "compile": "PASS"}]}),
                            encoding="utf-8")
        out = io.StringIO()
        args = [str(script), "--reuse-precheck", str(precheck), "--run-id", "run", "--task-id", "T-1", "--emit-file"]
        with patch.object(rc, "toolchain_key", return_value="current"), patch.object(rc, "check", return_value=[]):
            with contextlib.redirect_stdout(out):
                status = rc.main(args)
        res = json.loads(out.getvalue())
        assert status == 0 and "code" not in res
        assert res["sha256"] == hashlib.sha256(script.read_bytes()).hexdigest() == digest
        assert res["expectMarker"] == "// T-1 v3" and Path(res["codeFile"]).is_absolute()
        assert Path(res["codeFile"]) == script.absolute()


def make_sheet(path, number, body="body"):
    with pymupdf.open() as doc:
        page = doc.new_page(width=600, height=400)
        page.insert_text((60, 90), body)
        page.insert_text((500, 370), number)
        doc.save(path)


def test_sheet_batch_parallel_reuses_outputs_and_refreshes_changed_input():
    with TemporaryDirectory() as temp:
        folder = Path(temp)
        src = folder / "src"
        src.mkdir()
        first = src / "P1.01.pdf"
        make_sheet(first, "P1.01")
        make_sheet(src / "P1.02.PDF", "P1.02")
        cmd = [sys.executable, str(TOOLS / "sheet_tool.py"), "prep", str(src), str(folder / "renders"),
               str(folder / "text"), "--workers", "2", "--dpi", "36", "--cols", "1", "--rows", "1"]
        env = dict(os.environ, SWARM_HOME=temp)

        def run_prep():
            data = json.loads(subprocess.check_output(cmd, env=env, text=True, timeout=45))
            return {row["sheet"]: row["status"] for row in data["sheets"]}

        assert run_prep() == {"P1.01": "prepared", "P1.02": "prepared"}
        assert run_prep() == {"P1.01": "reused", "P1.02": "reused"}
        second_round = folder / "round2"
        repeat = cmd.copy()
        repeat[4:6] = [str(second_round / "renders"), str(second_round / "text")]
        checked = json.loads(subprocess.check_output(repeat + ["--reuse-from", str(folder)],
                                                     env=env, text=True, timeout=45))
        assert all(row["status"] == "reused" for row in checked["sheets"])
        assert (second_round / "renders/P1.01/overview.png").read_bytes() == (
            folder / "renders/P1.01/overview.png").read_bytes()
        make_sheet(first, "P1.01", "new text")
        assert run_prep() == {"P1.01": "prepared", "P1.02": "reused"}
        assert "new text" in (folder / "text/P1.01.txt").read_text(encoding="utf-8")


def test_verify_sheet_ignores_other_sheet_refs_in_body():
    with TemporaryDirectory() as temp:
        pdf = Path(temp) / "export.pdf"
        make_sheet(pdf, "P1.011", "SEE P1.01")
        assert visual_crop.verify_sheet(pdf, 1, "P1.011")["status"] == "verified"
        assert visual_crop.verify_sheet(pdf, 1, "P1.01")["status"] == "needs_visual_title_block_check"


# ---- 2026-11-25: fail-cycle ladder, finish index work, project pointer safety, live_reresolve
def _rq(argv):
    """Run run_queue.main(argv); returns (exit code, parsed JSON or None, stderr)."""
    out, err = io.StringIO(), io.StringIO()
    with contextlib.redirect_stdout(out), contextlib.redirect_stderr(err):
        try:
            code = run_queue.main([str(a) for a in argv])
        except SystemExit as stop:
            code = stop.code
    text = out.getvalue().strip()
    return code, (json.loads(text) if text else None), err.getvalue()


@contextlib.contextmanager
def _swarm_root():
    with TemporaryDirectory() as temp:
        root = Path(temp) / "swarm"
        (root / "manifests").mkdir(parents=True)
        with patch.object(model_index, "SWARM", root), patch.object(run_queue, "SWARM", root), \
                patch.object(check_task, "SWARM", root):
            yield root


def _project(root, number="25-037", run="20261125-AAA", name="25-037_Test", doc="C:/A.rvt"):
    (root / name / "manifests").mkdir(parents=True, exist_ok=True)
    data = {"project_number": number, "project_folder": f"swarm/{name}", "task_prefix": "E37",
            "revit_document_id": doc, "acad_document_id": None, "run_id": run}
    model_index.save_json(root / "manifests/project.json", data)
    return data


def _plan(root, run="20261125-AAA"):
    plan = root / "plan.json"
    model_index.save_json(plan, {"run_id": run, "stages": [
        {"id": "scout", "task": "E37-R1-T1", "lane": "revit"},
        {"id": "write", "task": "E37-R1-T1", "lane": "revit", "after": ["scout"]},
        {"id": "check", "task": "E37-R1-T2", "lane": "offline"}]})
    return plan


def test_fail_cycle_ladder_counts_and_stops():
    with _swarm_root() as root:
        _project(root)
        queue = root / "q.json"
        assert _rq([queue, "init", _plan(root)])[0] == 0
        code, out, _ = _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "compile CS0103"])
        assert code == 0 and out["failures"] == 1 and out["next"] == "debugger-c1" and out["model"] == "sonnet"
        assert "needs_jeremy_go" not in out
        code, out, _ = _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "dry run redesign", "--landed", "maybe"])
        assert (code, out["next"], out["model"], out["needs_jeremy_go"]) == (0, "debugger-c2", "opus", True)
        assert "ChatGPT" in out["note"] and "E37-R1-T1.debug.c2.json" in out["note"]
        code, out, _ = _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "verify FAIL"])
        assert code == 4 and out["next"] == "stop-ask-jeremy" and out["failures"] == 3
        # Jeremy's answer to that stop upgrades failure 3 (no new count)
        code, out, _ = _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "verify FAIL", "--landed", "yes",
                            "--approved-extra", "one more try, Jeremy 11-25"])
        assert code == 0 and (out["failures"], out["next"], out["model"], out["needs_jeremy_go"]) == (
            3, "debugger-extra", "opus", True)
        history = model_index.load_json(queue)["debug"]["E37-R1-T1"]
        assert history[-1]["approved_extra"] == "one more try, Jeremy 11-25" and len(history) == 3
        # the extra cycle fails too: failure 4 stops again unless approved on that call
        assert _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "verify FAIL again"])[0] == 4
        code, out, _ = _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "x", "--approved-extra", "last one"])
        assert code == 0 and out["failures"] == 4 and "debug.c4.json" in out["note"]
        code, out, _ = _rq([queue, "fail-cycle", "E37-R1-T1", "--cause", "again", "--approved-extra", "pre-ok"])
        assert code == 0 and out["failures"] == 5 and out["next"] == "debugger-extra"
        code, out, _ = _rq([queue, "cycles"])
        assert out["cycles"]["E37-R1-T1"]["failures"] == 5 and out["cycles"]["E37-R1-T2"]["failures"] == 0
        assert _rq([queue, "fail-cycle", "E37-R9-T9", "--cause", "x"])[0] == 2
        assert run_queue.DEBUG_LADDER[0]["next"] == "debugger-c1" and "fail-cycle" in run_queue.DEBUG_LADDER_DOC


def test_finish_merges_scout_invalidates_and_records_attempt():
    with _swarm_root() as root:
        _project(root)
        queue = root / "q.json"
        out = _rq([queue, "init", _plan(root)])[1]
        assert out["project"]["revit_document_id"] == "C:/A.rvt" and "warnings" not in out
        assert (root / "25-037_Test/manifests/project.json").is_file()
        scout = root / "E37-R1-T1.scout.json"
        model_index.save_json(scout, {"run_id": "20261125-AAA", "app": "revit", "document_id": "c:\\a.rvt",
                                      "index_scopes": {"sheets:P1.01": {"complete": True, "entries": [123456]},
                                                       "views:9": {"complete": True, "entries": [9]}}})
        assert _rq([queue, "start", "scout"])[1]["pointer"] == "ok"
        code, out, _ = _rq([queue, "finish", "scout", "pass", "--scout", scout])
        assert code == 0 and out["index"]["merged"] == ["sheets:p1.01", "views:9"]
        _rq([queue, "start", "write"])
        code, out, _ = _rq([queue, "finish", "write", "pass", "--invalidate", "sheets:P1.01"])
        assert out["index"]["invalidated"] == ["sheets:p1.01"] and out["index"]["reason"] == "write"
        data = model_index.read_index("revit", "C:/A.rvt", "20261125-AAA")
        assert list(data["scopes"]) == ["views:9"] and data["invalidated_reason"]["sheets:p1.01"] == "write"
        assert "sheets:p1.01" in data["stale_scopes"] and not data["invalidated_hard_ns"]
        attempt = model_index.load_json(queue)["stages"]["write"]["attempts"][-1]
        assert attempt["index"]["invalidate"] == ["sheets:P1.01"] and attempt["outcome"] == "pass"
        # offline lanes refuse index work and nothing changes
        _rq([queue, "start", "check"])
        assert _rq([queue, "finish", "check", "pass", "--invalidate-all"])[0] == 2
        assert model_index.load_json(queue)["stages"]["check"]["status"] == "running"


def test_finish_uncertain_invalidates_whole_document():
    with _swarm_root() as root:
        _project(root)
        queue = root / "q.json"
        _rq([queue, "init", _plan(root)])
        model_index.merge_index("revit", "C:/A.rvt", "20261125-AAA", {
            "run_id": "20261125-AAA", "app": "revit", "document_id": "C:/A.rvt",
            "index_scopes": {"views:9": {"complete": True, "entries": [9]}}})
        _rq([queue, "start", "scout"])
        code, out, _ = _rq([queue, "finish", "scout", "uncertain"])
        assert code == 0 and out["index"]["invalidate"] == "document" and out["index"]["reason"] == "uncertain"
        data = model_index.read_index("revit", "C:/A.rvt", "20261125-AAA")
        assert not data["scopes"] and "*" in data["invalidated_hard_ns"] and not data["stale_scopes"]


def test_start_restores_foreign_pointer_or_refuses():
    with _swarm_root() as root:
        ours = _project(root)
        queue = root / "q.json"
        _rq([queue, "init", _plan(root)])
        foreign = {"project_number": "25-061", "project_folder": "swarm/25-061_Other", "run_id": "20261125-ZZZ"}
        model_index.save_json(root / "manifests/project.json", foreign)
        code, out, _ = _rq([queue, "start", "scout"])
        assert code == 0 and out["pointer_restored"] is True and out["status"] == "running"
        raw = (root / "manifests/project.json").read_bytes()
        assert not raw.startswith(b"\xef\xbb\xbf") and json.loads(raw)["project_number"] == ours["project_number"]
        assert json.loads((root / "manifests/project.json.25-061.bak").read_text())["run_id"] == "20261125-ZZZ"
        # no project copy: refuse with exit 2, nothing started
        (root / "25-037_Test/manifests/project.json").unlink()
        _rq([queue, "finish", "scout", "pass"])
        model_index.save_json(root / "manifests/project.json", foreign)
        code, out, err = _rq([queue, "start", "write"])
        assert code == 2 and "no project copy" in err
        assert model_index.load_json(queue)["stages"]["write"]["status"] == "pending"


def test_check_task_live_reresolve_accepts_write_staleness_only():
    with _swarm_root() as root:
        run, name = "20261125-AAA", "E37-R1-T1"
        _project(root)
        scout = root / f"{name}.scout.json"
        report = {"task_id": name, "run_id": run, "app": "revit", "document_id": "C:/A.rvt",
                  "confirmed_target_ids": [123456],
                  "index_scopes": {"sheets:p1.01": {"complete": True, "entries": [123456]}}}
        model_index.save_json(scout, report)
        model_index.merge_index("revit", "C:/A.rvt", run, report)
        task = root / f"{name}.task.json"
        spec = {"task_id": name, "run_id": run, "app": "revit", "document_id": "C:/A.rvt",
                "lookup_scopes": ["sheets:P1.01"], "target_ids": [123456]}
        model_index.save_json(task, spec)
        model_index.invalidate("revit", "C:/A.rvt", run, ["sheets:p1.01"], "write")
        assert check_task.check(task)[3] == ["target 123456 has no current evidence"]  # unchanged without the field
        model_index.save_json(task, dict(spec, live_reresolve=True))
        errors, warnings, found, needs = check_task.check(task)
        assert not errors and not needs and any("stale after in-run write; writer re-resolves" in w for w in warnings)
        with patch.object(sys, "argv", ["check_task.py", str(task)]), contextlib.redirect_stdout(io.StringIO()):
            assert check_task.main() == 0
        scout.unlink()  # the cached stale scope alone also counts
        assert not check_task.check(task)[3]
        model_index.save_json(scout, report)
        model_index.invalidate("revit", "C:/A.rvt", run, ["sheets:p1.01"], "write")
        model_index.invalidate("revit", "C:/A.rvt", run, ["sheets"], "outside")  # non-write after the evidence
        assert check_task.check(task)[3] == ["target 123456 has no current evidence"]
        with patch.object(sys, "argv", ["check_task.py", str(task)]), contextlib.redirect_stdout(io.StringIO()):
            assert check_task.main() == 3


def test_model_index_reason_defaults_and_cli():
    with _swarm_root() as root:
        run = "20261125-AAA"
        model_index.merge_index("acad", "C:/D.dwg", run, {"run_id": run, "app": "acad", "document_id": "C:/D.dwg",
                                                          "index_scopes": {"layouts:p1": {"complete": True,
                                                                                          "entries": ["a"]}}})
        model_index.invalidate("acad", "C:/D.dwg", run, ["layouts:p1"])
        data = model_index.read_index("acad", "C:/D.dwg", run)
        assert data["invalidated_reason"] == {"layouts:p1": "write"} and "layouts:p1" in data["stale_scopes"]
        model_index.invalidate("acad", "C:/D.dwg", run)
        data = model_index.read_index("acad", "C:/D.dwg", run)
        assert data["invalidated_reason"] == {"*": "uncertain"} and not data["stale_scopes"]
        try:
            model_index.invalidate("acad", "C:/D.dwg", run, (), "bogus")
        except ValueError:
            pass
        else:
            assert False, "unknown reason must be refused"
        env = dict(os.environ, SWARM_HOME=str(root))
        out = subprocess.check_output([sys.executable, str(TOOLS / "model_index.py"), "invalidate", "acad",
                                       "C:/D.dwg", run, "layouts:p2", "--reason", "outside"], env=env, text=True)
        assert json.loads(out)["invalidated"] == []
        hard = model_index.read_index("acad", "C:/D.dwg", run)["invalidated_hard_ns"]
        assert "layouts:p2" in hard
