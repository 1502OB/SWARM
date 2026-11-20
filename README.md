# swarm

Claude Code agent tree for 120 Degreez plumbing work: Revit MCP, AutoCAD MCP and Bluebeam tasks (plan check markups, QA/QC passes, revisions, sheet publishing).

## What is tracked here

- `.claude/settings.json` - Claude Code project settings and hooks
- `build/` - `gen_agents*.py` and `gen_all.py`, which generate the agent definitions
- `claude/hooks/`, `claude/skills/` - mirror of `~/.claude/hooks` and the firm skills (acad-conventions, revit-conventions, revit-api, autocad-api, plumbing-code, run-tracker), synced by `build/sync_claude.py`
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
python build\sync_claude.py install
python build\gen_all.py
```

On this machine, install the Revit bridge health patch with `python tools/patch_revit_mcp.py --health-only`.
Run it again after a Revit MCP update. Use `--health-only --check` to check compatibility and
`--health-only --restore` to restore the saved bundle. Reconnect only the Revit MCP server or start a new
Claude Code session to load the change. The added `get_project_info(health=true)` request checks the active
model with a five-second budget. AutoCAD already provides request-correlated file IPC and `system health`.

Then confirm the generated agents landed in `%USERPROFILE%\.claude\agents`. plumbing-code page renders (`*.png`) are not
in the repo; re-run `claude\skills\plumbing-code\scripts\code_index.py` on the L: source PDFs if the image pages are needed.

## Keeping hooks and skills in sync

Agents regenerate from `build/` (`gen_all.py`); hooks and skills are copied files, so they need a sync step:

- Edited a hook or skill in `~/.claude`: `python build\sync_claude.py pull`, then commit and push.
- Pulled changes from GitHub: `python build\sync_claude.py install` (backs up anything it overwrites to `build\backup_claude_*`).
- `python build\sync_claude.py status` lists anything that differs. The script never deletes files.
- Run `status` before every `pull`: `pull` copies every differing file from `~/.claude`, so a stale `~/.claude` (no
  `install` after the last commit) overwrites newer repo files (2026-11-19, LESSONS O42). After a roll-in commit run
  both `gen_all.py` and `sync_claude.py install`.

## Running the approved work queue

The markup orchestrator represents each stage as JSON, with prerequisites named by stage ID. Use the approved
task plan to include the snapshot, relevant scout and precheck, write approval, dry-run/writer, applicable plots
and reviews, and verification/save/sync. For example, these could be two stages in a larger plan.

```json
{
  "run_id": "20261120-DEMO",
  "stages": [
    {"id": "scout-P1", "task": "C22-R1-T1", "lane": "revit", "after": []},
    {"id": "script-P1", "task": "C22-R1-T1", "lane": "offline", "after": ["scout-P1"]}
  ]
}
```

From the repo root, initialize `python tools/run_queue.py "<queue.json>" init "<plan.json>"`. Keep the queue
under the project's manifests folder and resume from that file. The main session owns all queue updates.
Use `ready` to select stages, `start <stage-id>` before dispatch, and `finish <stage-id> pass|fail|uncertain`
when the stage returns. Add new stages with `add "<plan.json>"`. A failed stage can be retried after correction
and any required approval, with `retry <stage-id> --after <new-precheck-stage>` when new prerequisites apply.
An uncertain write holds the bridge lane until read-only reconciliation establishes the outcome and the
orchestrator runs `resolve <stage-id> pass|fail`.

Use lanes `revit`, `acad`, `offline`, `tracker`, and `human`. The CAD lanes each admit one active stage.
Offline stages can overlap CAD stages. Use `report` to see per-task queue wait, lane time, human and tracker
wait, and reconciliation time. With `run_id` set in the active project pointer, the timing hook also
records completed CAD MCP durations that Claude Code makes available under `logs/<run-id>.bridge/`.
The report labels these separately as `bridge_tool_s`; work outside a queue interval is counted as unassigned.

The scoped model cache uses the stable document identity and run ID. An entry covers only its named scope.
Use `python tools/model_index.py show|merge|invalidate …` from the orchestrator as documented in the
markup prompt. A fresh scout is required for missing or invalidated target evidence. The writer's live
preflight and dry run still run before the edit.

For QA/QC, run `python tools/sheet_tool.py prep "<src>" "<renders>" "<text>"` after collecting one PDF per sheet.
For a new review round, add `--reuse-from "<prior-run>"`. The batch checks the source content and render settings
before reusing complete outputs. Add `--workers 2` to lower parallel processing on a machine with less memory.
