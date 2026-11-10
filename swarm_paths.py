"""Machine-independent locations for the swarm. Import this instead of hard-coding C:/Users/<name>/...

    from swarm_paths import SWARM, CLAUDE, AUTOCAD_MCP, fwd

Each location can be overridden by an environment variable, so the repo works for any user or checkout folder:
    SWARM_HOME          the swarm repo root            (default: the folder that holds this file)
    CLAUDE_CONFIG_DIR   Claude Code config folder      (default: ~/.claude)
    AUTOCAD_MCP_HOME    autocad-mcp checkout           (default: ~/autocad-mcp)
Project data on the shared L: drive (L:/Shared/...) is the same for everyone at the firm and stays absolute.
Paths use os.path.abspath, never Path.resolve(): resolve() turns L:\\ into \\\\EgnyteDrive\\... (LESSONS W-section).
"""
import os
from pathlib import Path


def _dir(env: str, default: Path) -> Path:
    v = os.environ.get(env)
    return Path(os.path.abspath(os.path.expanduser(v))) if v else Path(os.path.abspath(default))


SWARM = _dir("SWARM_HOME", Path(__file__).parent)
CLAUDE = _dir("CLAUDE_CONFIG_DIR", Path.home() / ".claude")
AUTOCAD_MCP = _dir("AUTOCAD_MCP_HOME", Path.home() / "autocad-mcp")


def fwd(p) -> str:
    """Forward-slash string (what AutoLISP, the bridge and the agent briefs expect)."""
    return str(p).replace("\\", "/")


# Placeholders used in shared text (agent briefs, prompts, skills). build/gen_all.py and build/sync_claude.py render them.
PLACEHOLDERS = {"{SWARM}": fwd(SWARM), "{CLAUDE}": fwd(CLAUDE), "{AUTOCAD_MCP}": fwd(AUTOCAD_MCP)}


def render(text: str) -> str:
    for k, v in PLACEHOLDERS.items():
        text = text.replace(k, v)
    return text


if __name__ == "__main__":
    for k, v in PLACEHOLDERS.items():
        print(k, v)
