"""Regenerate every swarm agent in ~/.claude/agents:  python ~/swarm/build/gen_all.py

gen_agents (base) -> gen_agents_p67 (intake, planning, letter, excel) -> gen_agents_qaqc (QAQC review) -> gen_agents_review (completeness/delivery) -> gen_agents_upgrade (2026-11-04 upgrade agents).
Each module writes its agents on import; gen_agents runs once because Python caches the import.
Agent files hot-reload in running sessions (the next delegation uses the new definition).
"""
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
import gen_agents_p67  # noqa: E402,F401  (imports gen_agents first)
import gen_agents_qaqc  # noqa: E402,F401
import gen_agents_review  # noqa: E402,F401  (calc/set/xsheet/letter/comment/spec/presubmittal)
import gen_agents_upgrade  # noqa: E402,F401  (swarm-upgrade 2026-11-04: snapshot/visual/code/scope/debug/filer/retro)
