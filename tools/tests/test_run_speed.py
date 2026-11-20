"""Regressions for queue gates, stale evidence, compiler reuse and parallel QA/QC prep."""
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
