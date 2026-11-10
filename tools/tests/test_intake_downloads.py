"""intake_downloads.py: list recent downloads, move with no overwrite, refuse partial downloads, SOURCES.json ledger."""
import json
import subprocess
import sys
import tempfile
from pathlib import Path

import _util  # noqa: F401  (sys.path)
from _util import run_module_tests, TOOLS

TOOL = str(TOOLS / "intake_downloads.py")


def run(*a):
    r = subprocess.run([sys.executable, TOOL, *a], capture_output=True, text=True)
    return r.returncode, json.loads(r.stdout) if r.stdout.strip().startswith("{") else r.stdout


def setup():
    dl, dest = Path(tempfile.mkdtemp(prefix="swdl_")), Path(tempfile.mkdtemp(prefix="swin_"))
    (dl / "P-101 Markups.pdf").write_bytes(b"%PDF-1.7 a")
    (dl / "Letter.pdf").write_bytes(b"%PDF-1.7 b")
    (dl / "big.pdf.crdownload").write_bytes(b"x")
    return dl, dest


def test_list_flags_partial():
    dl, _ = setup()
    code, out = run("list", "--from", str(dl))
    assert code == 0
    st = {f["name"]: f["in_progress"] for f in out["files"]}
    assert st == {"P-101 Markups.pdf": False, "Letter.pdf": False, "big.pdf.crdownload": True}, st


def test_move_records_source_and_never_overwrites():
    dl, dest = setup()
    (dest / "Letter.pdf").write_bytes(b"old")
    code, out = run("move", str(dest), "--from", str(dl), "--source", "ACC: Project Files/PC2",
                    "--file", "P-101 Markups.pdf", "--file", "Letter.pdf")
    assert code == 0, out
    assert (dest / "Letter.pdf").read_bytes() == b"old" and (dest / "Letter_2.pdf").is_file()
    assert not (dl / "Letter.pdf").exists()
    led = json.loads((dest / "SOURCES.json").read_text(encoding="utf-8"))
    assert [r["file"] for r in led] == ["P-101 Markups.pdf", "Letter_2.pdf"]
    assert all(r["source"] == "ACC: Project Files/PC2" and len(r["sha256"]) == 64 for r in led)


def test_move_refuses_partial_and_missing():
    dl, dest = setup()
    code, out = run("move", str(dest), "--from", str(dl), "--source", "x",
                    "--file", "big.pdf.crdownload", "--file", "nope.pdf")
    assert code == 1
    assert [f["status"] for f in out["files"]] == ["in_progress", "missing"]
    assert (dl / "big.pdf.crdownload").exists()


def test_ledger_appends():
    dl, dest = setup()
    run("move", str(dest), "--from", str(dl), "--source", "a", "--file", "Letter.pdf")
    run("move", str(dest), "--from", str(dl), "--source", "b", "--file", "P-101 Markups.pdf")
    led = json.loads((dest / "SOURCES.json").read_text(encoding="utf-8"))
    assert [r["source"] for r in led] == ["a", "b"]


if __name__ == "__main__":
    p, f = run_module_tests(globals())
    print(f"{p} passed, {f} failed")
    sys.exit(1 if f else 0)
