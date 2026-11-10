"""Machine-independent locations for the swarm guard hooks (2026-11-09). The hooks run from ~/.claude/hooks, not
from the repo, so they cannot import swarm/swarm_paths.py; this module computes the same values at runtime:

    HOME         Path.home()
    SWARM        $SWARM_HOME         else <home>/swarm
    CLAUDE       $CLAUDE_CONFIG_DIR  else <home>/.claude
    AUTOCAD_MCP  $AUTOCAD_MCP_HOME   else <home>/autocad-mcp

path_re(p) is a regex for p as an agent may type it on a command line: C:/x/y, c:\\x\\y or the Git-Bash form /c/x/y
(matched case-insensitively by the callers). L:/Shared/... is the same for everyone and stays absolute.
os.path.abspath, never Path.resolve(): resolve() turns L:\\ into \\\\EgnyteDrive\\... (LESSONS W-section).
"""
import os
import re
from pathlib import Path


def _dir(env, default):
    v = os.environ.get(env)
    return Path(os.path.abspath(os.path.expanduser(v))) if v else Path(os.path.abspath(default))


HOME = Path(os.path.abspath(Path.home()))
SWARM = _dir("SWARM_HOME", HOME / "swarm")
CLAUDE = _dir("CLAUDE_CONFIG_DIR", HOME / ".claude")
AUTOCAD_MCP = _dir("AUTOCAD_MCP_HOME", HOME / "autocad-mcp")
SEP = r"[/\\]"


def fwd(p):
    """Forward-slash string (what AutoLISP, the bridge and the agent briefs use)."""
    return str(p).replace("\\", "/")


def _parts(p):
    s = fwd(p)
    m = re.match(r"^([A-Za-z]):/?(.*)$", s)
    if m:
        return m.group(1), [x for x in m.group(2).split("/") if x]
    return None, [x for x in s.split("/") if x]


def path_re(base, rel=""):
    """Regex (no anchors, no groups that capture) for base/rel in C:/..., C:\\... or /c/... form."""
    drive, parts = _parts(base)
    parts += [x for x in fwd(rel).split("/") if x]
    head = rf"(?:{re.escape(drive)}:|/{re.escape(drive)})" if drive else ""
    return head + "".join(SEP + re.escape(x) for x in parts)


def norm(p):
    """Lower-case forward-slash absolute form for comparing file paths (Git-Bash /c/... becomes c:/...)."""
    s = fwd(p).strip()
    m = re.match(r"^/([A-Za-z])/(.*)$", s)
    if m:
        s = f"{m.group(1)}:/{m.group(2)}"
    return fwd(os.path.normpath(s)).lower()
