# swarm

Claude Code agent tree for 120 Degreez plumbing work: Revit MCP, AutoCAD MCP and Bluebeam tasks (plan check markups, QA/QC passes, revisions, sheet publishing).

## What is tracked here

- `.claude/settings.json` - Claude Code project settings and hooks
- `build/` - `gen_agents*.py` and `gen_all.py`, which generate the agent definitions
- `tools/`, `tools.json` - tool wrappers and the tool registry the agents call
- `scripts/`, `validate.py` - helper scripts and the manifest validator
- `templates/`, `schemas/` - task and manifest templates
- `checklists/`, `codes/` - plan check checklists and code references
- `library/` - shared LISP and other drafting assets
- `*_SWARM_PROMPT.md`, `REVISION_PLAN.md`, `LESSONS.md` - orchestrator prompts and running notes

## What is NOT tracked (see `.gitignore`)

- Project folders (`22-001_...`, `26016_...`, etc.), `inputs/`, `archive/`, `backups/`
- Run output: `logs/`, `manifests/`, `cache/`, `locks/`, `ledger/`, `snapshots/`, `retro/`
- Any PDF, PNG, DWG, RVT or XLSX

Project data stays on Egnyte. This repo is the tooling only.

## Setup on a new machine

```
git clone <repo-url> %USERPROFILE%\swarm
cd %USERPROFILE%\swarm
python build\gen_all.py
```

Then confirm the generated agents landed in `%USERPROFILE%\.claude\agents`.
