"""Shared helpers for tools/tests (plain asserts; run with `python tools/tests/run.py` or pytest if installed)."""
import os
import sys
import tempfile
from pathlib import Path

TOOLS = Path(__file__).resolve().parent.parent
REPO = TOOLS.parent
for p in (str(TOOLS), str(REPO)):
    if p not in sys.path:
        sys.path.insert(0, p)


def write_tmp(text, suffix=".lsp", folder=None):
    folder = folder or tempfile.mkdtemp(prefix="swtest_")
    p = Path(folder) / ("snippet" + suffix)
    p.write_text(text, encoding="utf-8")
    return p


def run_module_tests(mod_globals):
    """Run every test_* function of a module; returns (passed, failed)."""
    passed = failed = 0
    for name, fn in sorted(mod_globals.items()):
        if name.startswith("test_") and callable(fn):
            try:
                fn()
                passed += 1
                print(f"  PASS {name}")
            except Exception as e:  # noqa: BLE001 - report every failure
                failed += 1
                print(f"  FAIL {name}: {type(e).__name__}: {e}")
    return passed, failed


os.environ.setdefault("PYTHONDONTWRITEBYTECODE", "1")
