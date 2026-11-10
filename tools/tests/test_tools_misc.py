"""lsp_collide locals (A42), merge_ops (A48), acad_dsd multi-sheet (A41), visual_crop --find on /Rotate 270 (A45),
lsp_dryrun --from-live copy (A44) and fail= detection, validate.py sibling task_id (A40)."""
import json
import os
import re
import subprocess
import sys
import tempfile
import time
from pathlib import Path

import _util
from _util import write_tmp, run_module_tests, REPO, TOOLS

import lsp_collide
import merge_ops
import acad_dsd
import lsp_dryrun


# ---- lsp_collide: per-defun case duplicates (M18-R3-T15.v1 CHECK declared ne and nE)
def test_collide_locals_t15_shape():
    p = write_tmp('(defun c:M18-R3-T15-CHECK ( / t6 ne nb nE) (setq ne 1 nE 2) (list ne nE))\n'
                  '(defun ok1 (a / b) (setq b a))')
    loc = lsp_collide.local_collisions(str(p))
    assert loc == [("c:M18-R3-T15-CHECK", [["ne", "nE"]])], loc


def test_collide_param_vs_setq_and_output_lines():
    p = write_tmp('(defun f (Pt / x) (setq pt 1 x 2) x)\n(defun g ( / ) (setq G 1))')
    out = subprocess.run([sys.executable, str(TOOLS / "lsp_collide.py"), str(p)], capture_output=True, text=True).stdout
    lines = out.strip().splitlines()
    assert len(lines) == 2, out
    assert lines[0].endswith("collisions: ['g']"), lines[0]  # existing global defun-vs-setq check kept
    assert "local-collisions: [f: Pt/pt]" in lines[1], lines[1]


def test_collide_clean():
    p = write_tmp('(defun f (a / b) (setq b a) b)')
    out = subprocess.run([sys.executable, str(TOOLS / "lsp_collide.py"), str(p)], capture_output=True, text=True).stdout
    assert out.strip().splitlines()[1].endswith("local-collisions: none"), out


# ---- merge_ops
def _proj():
    d = Path(tempfile.mkdtemp(prefix="swmerge_"))
    (d / "manifests").mkdir()
    return d


def test_merge_ops_idempotent_order_and_move():
    d = _proj()
    man = d / "manifests"
    (man / "M18-SAVE.ops.json").write_text(json.dumps([{"attempt": 1}]), encoding="utf-8")
    a2 = man / "M18-SAVE.attempt2.ops.json"
    a3 = man / "M18-SAVE.attempt3.ops.json"
    a3.write_text(json.dumps({"attempt": 3}), encoding="utf-8")      # object = one entry
    a2.write_text(json.dumps([{"attempt": 2}, {"attempt": 1}]), encoding="utf-8")  # list; attempt 1 is a duplicate
    t = time.time()
    os.utime(a3, (t, t))
    os.utime(a2, (t - 100, t - 100))  # attempt2 is older -> merged first
    other = man / "OTHER.attempt1.ops.json"
    other.write_text("[]", encoding="utf-8")
    rep = merge_ops.merge(d, "M18-SAVE")
    log = json.loads((man / "M18-SAVE.ops.json").read_text(encoding="utf-8"))
    assert log == [{"attempt": 1}, {"attempt": 2}, {"attempt": 3}], log
    assert rep["bases"]["M18-SAVE"]["added"] == 2 and rep["bases"]["M18-SAVE"]["skipped_duplicates"] == 1, rep
    assert not a2.exists() and not a3.exists() and (man / "merged" / a2.name).exists()
    assert other.exists()  # other base untouched with --base
    # run again with the same attempt re-written: nothing added, file still moved (never deleted)
    a2.write_text(json.dumps([{"attempt": 2}]), encoding="utf-8")
    rep = merge_ops.merge(d, "M18-SAVE")
    assert rep["bases"]["M18-SAVE"]["added"] == 0
    assert json.loads((man / "M18-SAVE.ops.json").read_text(encoding="utf-8")) == log
    assert len(list((man / "merged").glob("*attempt2*"))) == 2


def test_merge_ops_bad_json_left_in_place():
    d = _proj()
    bad = d / "manifests" / "X.attempt1.ops.json"
    bad.write_text("{not json", encoding="utf-8")
    rep = merge_ops.merge(d)
    assert rep["errors"] and bad.exists()


# ---- acad_dsd multi-sheet
def test_dsd_rebuilds_mansfield_20_sheet_model():
    model = REPO / "2018-930Mansfield" / "output" / "M18-ISS-T1.dsd"
    if not model.exists():
        print("    (skip: model DSD not in this checkout)")
        return
    m = model.read_text(encoding="utf-8")
    pairs = re.findall(r"^DWG=(.*)\nLayout=(.*)$", m, re.M)
    pdf = re.search(r"^DWF=(.*)$", m, re.M).group(1)
    assert len(pairs) == 20 and acad_dsd.build(pairs, pdf) == m


def test_dsd_multi_sheet_cli_and_sheets_file():
    d = Path(tempfile.mkdtemp(prefix="swdsd_"))
    a, b = d / "P1.01 A & B.dwg", d / "P2.01 C.dwg"
    a.write_bytes(b"x")
    b.write_bytes(b"x")
    sheets = d / "sheets.txt"
    sheets.write_text(f"# set\n{a}|P1.01\n\n{b}|P2.01\n", encoding="utf-8")
    pdf, dsd = d / "out" / "set.pdf", d / "out" / "set.dsd"
    assert acad_dsd.main(["--sheets-file", str(sheets), str(pdf), str(dsd)]) == 0
    t = dsd.read_text(encoding="utf-8")
    assert t.count("[DWF6Sheet:") == 2 and t.count("[Target]") == 1
    assert t.index("P1.01 A & B-P1.01") < t.index("P2.01 C-P2.01")
    assert f"DWF={os.path.abspath(pdf)}" in t and "Layout=P2.01" in t
    assert acad_dsd.main(["--sheets-file", str(sheets), str(pdf), str(dsd)]) == 1  # never overwrites
    dsd2 = d / "out" / "set2.dsd"
    assert acad_dsd.main(["--sheets", f"{a}|P1.01;{b}|P2.01", str(d / "o2.pdf"), str(dsd2)]) == 0
    assert dsd2.read_text(encoding="utf-8").count("[DWF6Sheet:") == 2
    assert acad_dsd.main(["--sheets", f"{a}|P1.01;{a}|P1.01", str(d / "o3.pdf"), str(d / "o3.dsd")]) == 1  # repeat
    assert acad_dsd.main(["--sheets", f"{d / 'missing.dwg'}|X", str(d / "o4.pdf"), str(d / "o4.dsd")]) == 1


def test_dsd_single_sheet_unchanged():
    d = Path(tempfile.mkdtemp(prefix="swdsd1_"))
    a = d / "P1.dwg"
    a.write_bytes(b"x")
    assert acad_dsd.main([str(a), "P1", str(d / "p.pdf"), str(d / "p.dsd")]) == 0
    t = (d / "p.dsd").read_text(encoding="utf-8")
    assert t == acad_dsd.TEMPLATE.format(sheet="P1-P1", dwg=str(a), layout="P1", pdf=str(d / "p.pdf"), outdir=str(d))


def test_dsd_keeps_drive_letter():
    assert acad_dsd.win("L:/Shared/Projects/x.dwg") == "L:\\Shared\\Projects\\x.dwg"


# ---- visual_crop --find on a /Rotate 270 page
def test_visual_crop_find_rotated():
    import pymupdf
    d = Path(tempfile.mkdtemp(prefix="swcrop_"))
    pdf = d / "rot.pdf"
    doc = pymupdf.open()
    pg = doc.new_page(width=600, height=400)
    pg.insert_text((100, 100), "GAS METER M-1", fontsize=20)
    pg.set_rotation(270)
    doc.save(str(pdf))
    env = dict(os.environ, SWARM_HOME=str(d))
    r = subprocess.run([sys.executable, str(TOOLS / "visual_crop.py"), str(pdf), "all", "T-X1", "--find", "gas meter",
                        "--margin", "10"], capture_output=True, text=True, env=env)
    res = json.loads(r.stdout)
    assert res.get("matches") == 1 and res["rotation"] == 270, res
    pm = pymupdf.Pixmap(res["after"])
    dark = sum(1 for k in range(0, len(pm.samples), pm.n) if pm.samples[k] < 128)
    assert dark > 100, f"blank crop ({dark} dark pixels)"
    assert Path(res["after"]).parent == d / "crops"


def test_visual_crop_old_cli_still_works():
    import pymupdf
    d = Path(tempfile.mkdtemp(prefix="swcrop2_"))
    pdf = d / "a.pdf"
    doc = pymupdf.open()
    doc.new_page(width=600, height=400)
    doc.save(str(pdf))
    env = dict(os.environ, SWARM_HOME=str(d))
    r = subprocess.run([sys.executable, str(TOOLS / "visual_crop.py"), str(pdf), "1", "0.1,0.1,0.5,0.5", "T-X2",
                        "--units", "frac"], capture_output=True, text=True, env=env)
    assert "after" in json.loads(r.stdout), r.stdout


# ---- lsp_dryrun
def test_dryrun_from_live_copies_read_only_source():
    d = Path(tempfile.mkdtemp(prefix="swlive_"))
    src = d / "P1.01 SHEET.dwg"
    src.write_bytes(b"AC1032" + b"\0" * 100)
    os.chmod(src, 0o444)  # read-only like an L: file we must not touch
    before = (src.stat().st_mtime, src.read_bytes())
    old = lsp_dryrun.SWARM
    lsp_dryrun.SWARM = d / "repo"
    try:
        dst = lsp_dryrun.copy_live(src)
    finally:
        lsp_dryrun.SWARM = old
    assert dst.name == src.name and dst.read_bytes() == before[1]
    assert dst.parent.parent == d / "repo" / "scratch" / "live"
    assert (src.stat().st_mtime, src.read_bytes()) == before
    os.chmod(src, 0o666)


def test_dryrun_abort_detection_fail_equals():
    A = lsp_dryrun.ABORTED
    assert A.search("c:X-TEST_ok pass=4 fail=3")
    assert A.search("c:X-RUN_ok done=0 skipped=0 failed=1")
    assert A.search("c:X-PLAN_ok ABORT no clear spot")
    assert not A.search("c:X-TEST_ok pass=7 fail=0")
    assert not A.search("c:X-RUN_ok done=3 skipped=0 failed=0")


def test_dryrun_usage_from_live():
    r = subprocess.run([sys.executable, str(TOOLS / "lsp_dryrun.py"), "a.lsp", "b.dwg", "T", "--from-live", "c.dwg"],
                       capture_output=True, text=True)
    assert r.returncode == 2 and "--from-live" in r.stdout


# ---- validate.py: expected.json task_id pattern + sibling task.json
def _val(folder, name, data):
    p = folder / name
    p.write_text(json.dumps(data), encoding="utf-8")
    return subprocess.run([sys.executable, str(REPO / "validate.py"), str(REPO / "schemas" / "expected.schema.json"),
                           str(p)], capture_output=True, text=True)


def test_validate_sibling_task_id():
    d = Path(tempfile.mkdtemp(prefix="swval_"))
    task = {"run_id": "r", "task_id": "M18-R3-T15", "app": "acad", "operation": "edit", "target_ids": [],
            "change": "c", "source_requirement_id": "q", "assigned_agent": "acad-writer"}
    (d / "M18-R3-T15.task.json").write_text(json.dumps(task), encoding="utf-8")
    item = [{"id": "1", "check": "c", "expected_value": 1}]
    assert _val(d, "M18-R3-T15.expected.json", {"run_id": "r", "task_id": "M18-R3-T15", "items": item}).returncode == 0
    r = _val(d, "M18-R3-T15.expected.json", {"run_id": "r", "task_id": "M18-R3-T16", "items": item})
    assert r.returncode == 1 and "differs" in r.stdout, r.stdout
    r = _val(d, "M18-COS1-P101.expected.json", {"run_id": "r", "task_id": "M18-COS1-P101", "items": item})
    assert r.returncode == 1 and "does not match" in r.stdout, r.stdout  # pattern, no sibling needed


if __name__ == "__main__":
    p, f = run_module_tests(globals())
    print(f"test_tools_misc: {p} passed, {f} failed")
    sys.exit(1 if f else 0)
