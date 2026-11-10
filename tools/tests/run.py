"""Run every tools/tests/test_*.py without pytest:  python ~/swarm/tools/tests/run.py"""
import importlib
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
from _util import run_module_tests  # noqa: E402

total_p = total_f = 0
for f in sorted(Path(__file__).resolve().parent.glob("test_*.py")):
    print(f.stem)
    p, fl = run_module_tests(vars(importlib.import_module(f.stem)))
    total_p += p
    total_f += fl
print(f"TOTAL {total_p} passed, {total_f} failed")
sys.exit(1 if total_f else 0)
