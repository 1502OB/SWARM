"""Regenerate every swarm agent in <CLAUDE_CONFIG_DIR>/agents (default ~/.claude/agents):  python ~/swarm/build/gen_all.py

gen_agents (base) -> gen_agents_p67 (intake, planning, letter, excel) -> gen_agents_qaqc (QAQC review) -> gen_agents_review (completeness/delivery) -> gen_agents_upgrade (2026-11-04 upgrade agents).
Each module writes its agents on import; gen_agents runs once because Python caches the import.
Agent files hot-reload in running sessions (the next delegation uses the new definition).
Paths in the briefs are rendered from swarm_paths.py (SWARM_HOME, CLAUDE_CONFIG_DIR, AUTOCAD_MCP_HOME env overrides),
so the generated files carry the absolute paths of the machine that runs this. Test into a scratch folder with
    CLAUDE_CONFIG_DIR=<scratch> python build/gen_all.py
"""
import os
import sys

BUILD = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, BUILD)
sys.path.insert(0, os.path.dirname(BUILD))  # repo root: swarm_paths
from swarm_paths import CLAUDE  # noqa: E402

import gen_agents_p67  # noqa: E402,F401  (imports gen_agents first)
import gen_agents_qaqc  # noqa: E402,F401
import gen_agents_review  # noqa: E402,F401  (calc/set/xsheet/letter/comment/spec/presubmittal)
import gen_agents_upgrade  # noqa: E402,F401  (swarm-upgrade 2026-11-04: snapshot/visual/code/scope/debug/filer/retro)

print("agents written to", CLAUDE / "agents")
