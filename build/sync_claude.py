"""Sync the swarm's Claude Code hooks and skills between this repo and ~/.claude.

    python ~/swarm/build/sync_claude.py status    # list files that differ (default)
    python ~/swarm/build/sync_claude.py pull      # ~/.claude -> repo/claude   (capture live edits, then commit)
    python ~/swarm/build/sync_claude.py install   # repo/claude -> ~/.claude   (fresh machine / after git pull)

Synced: ~/.claude/hooks/**/*.py (incl. tests/) and the firm skills in SKILLS below. Agents are not copied: they are
generated into ~/.claude/agents by build/gen_all.py. claude.ai-managed skills (skills/synced) are never touched.
Never deletes anything on either side. A file that would be overwritten is first copied to
build/backup_claude_<timestamp>/<side>/... (gitignored). plumbing-code page renders (*.png) stay local only:
they are regenerated from the L: source PDFs by skills/plumbing-code/scripts/code_index.py.
"""
import filecmp
import shutil
import sys
import time
from pathlib import Path

REPO = Path(__file__).resolve().parent.parent / "claude"
LIVE = Path.home() / ".claude"
SKILLS = ["acad-conventions", "revit-conventions", "revit-api", "autocad-api", "plumbing-code"]
SKIP_DIRS = {"__pycache__"}


def skip(rel: Path) -> bool:
    if SKIP_DIRS & set(rel.parts) or ".bak" in rel.name or rel.suffix == ".pyc":
        return True
    return rel.parts[:2] == ("skills", "plumbing-code") and rel.suffix.lower() == ".png"


def files(root: Path):
    out = set()
    for sub in ["hooks"] + [f"skills/{s}" for s in SKILLS]:
        base = root / sub
        if not base.exists():
            continue
        pattern = "**/*.py" if sub == "hooks" else "**/*"  # hooks/tests/*.py included (2026-11-06)
        for p in base.glob(pattern):
            rel = p.relative_to(root)
            if p.is_file() and not skip(rel):
                out.add(rel)
    return out


def diff(src: Path, dst: Path):
    new, changed = [], []
    for rel in sorted(files(src)):
        d = dst / rel
        if not d.exists():
            new.append(rel)
        elif not filecmp.cmp(src / rel, d, shallow=False):
            changed.append(rel)
    return new, changed


def copy(src: Path, dst: Path, side: str):
    new, changed = diff(src, dst)
    backup = Path(__file__).resolve().parent / f"backup_claude_{time.strftime('%Y%m%d-%H%M%S')}" / side
    for rel in changed:
        b = backup / rel
        b.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(dst / rel, b)
    for rel in new + changed:
        (dst / rel).parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(src / rel, dst / rel)
    print(f"{side}: {len(new)} new, {len(changed)} updated" + (f" (old versions in {backup})" if changed else ""))
    for rel in changed:
        print(f"  updated {rel.as_posix()}")


def main():
    cmd = sys.argv[1] if len(sys.argv) > 1 else "status"
    if cmd == "pull":
        copy(LIVE, REPO, "repo")
    elif cmd == "install":
        copy(REPO, LIVE, "live")
    elif cmd == "status":
        for label, src, dst in (("only/newer in ~/.claude", LIVE, REPO), ("only/newer in repo", REPO, LIVE)):
            new, changed = diff(src, dst)
            print(f"{label}: {len(new)} missing on the other side, {len(changed)} differ")
            for rel in new[:20]:
                print(f"  missing  {rel.as_posix()}")
            for rel in changed:
                print(f"  differs  {rel.as_posix()}")
    else:
        sys.exit(__doc__)


if __name__ == "__main__":
    main()
