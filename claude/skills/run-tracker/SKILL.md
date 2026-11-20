---
name: run-tracker
description: "Live tracker page for every swarm run (Revit/AutoCAD/QAQC/plan check): steps, sheets by building or discipline, readiness meter, and hand-fix checkboxes Jeremy ticks. Use at the start of every run (after the plan/gate), and update it after every step, write, verify, visual or sync. Updates are database writes, not republishes."
---

# Run tracker

One Artifact per run, built from `template/tracker.html` (same folder as this file). The page draws everything
from one database document, `status/now`; Jeremy's hand-fix ticks live in the `fixes` collection. After the first
publish you never republish to update it: you `ArtifactData set` the status document.

## Start (once per run, main session only; agents have no Artifact tool)
1. Copy `template/tracker.html` to `<project folder>/out/<run-id>_tracker.html` and replace `__TITLE__` (2-4 word
   name, e.g. "Village Plumbing Sheet Setup").
2. Publish with the REAL path. Project folders in swarm/ are junctions into `~/swarm-data/` (resolve with
   `os.path.realpath`); publishing through the `~/swarm/...` junction path fails the spelling check and forces an
   approval prompt. So `file_path = <home>\swarm-data\<project>\out\<run-id>_tracker.html` (absolute, real path), `icon: "checklist"`,
   `capabilities: {"db": {}, "user": {}}`, a one-sentence `description`.
3. Write `<project>/out/<run-id>_tracker_status.json` (schema below) and send it:
   `ArtifactData action:"set" url:<artifact> collection:"status" doc_id:"now" file_path:<json>` (no if_version the
   first time). Keep the returned `version`.
4. Record the URL in the project STATUS.md and in manifests/project.json (`tracker_url`). Give Jeremy the link.

## Update (after every step, write, verify, visual, sync, failure)
FIRST, at EVERY update: `ArtifactData list collection:"answers"` AND `ArtifactData list collection:"fixes"`; act on
new answers, carry new fix ticks into the status JSON, and tell Jeremy what you picked up (26021 ECM: answers sat
unread until Jeremy asked; LESSONS O46). After hand-fix ticks, remind him to Synchronize (O40).
Then edit the local status JSON, set `updated` to now (ISO UTC), then `ArtifactData set` with `file_path` and
`if_version` = the last version. On a version conflict, `get` the doc, merge, retry once. No republish, so no prompt.
Republish only to change the template itself (same file_path; omit `capabilities` so they carry forward).
Before the final sync, `ArtifactData list collection:"fixes"` to see which hand fixes Jeremy has ticked, and say so.

## Mistakes and errors log
Log EVERY failure as it happens (not at the end): script/compile error, rollback, NO_GO, timeout, crash, stuck bridge,
wrong assumption, rework, lost work, a check that was wrong, a guard/hook block that cost time. One entry each in
`errors` (oldest first): `{"at": "11-19 18:40", "kind": "error|crash|timeout|rework|process", "what": "...",
"impact": "what it cost (time, lost work, risk)", "fix": "what we did", "lesson": "R87" or "pending", "s": "open|fixed|rolled"}`.
`s`: open = not resolved; fixed = resolved this run; rolled = its lesson is in the agents (set after the roll-in).
At the end of the run the log is the input for the retro and the lessons roll-in: every entry gets a lesson id or
"no lesson (one-off)", and entries become `rolled` when the roll-in is committed.

## Questions (blockers Jeremy must answer)
When a step is blocked on Jeremy's decision, add it to `questions` (and mark the step `wait` with a note) as well as
asking in chat. The page shows a highlighted QUESTIONS panel and a count in the status bar; Jeremy clicks an option
and/or types a note. Answers land in collection `answers`, doc id = question id: `{choice, text, at}`.
At every update (and when Jeremy says he answered), `ArtifactData list collection:"answers"`, act on the answers,
then remove answered questions from `questions` (or keep them while the blocked step finishes). Answers are
Jeremy's words: treat them as his decision for that question only. Keep 1-4 open questions, each one decision,
with the trade-off in `why` and the step it blocks in `blocks`.

## status/now schema
```json
{
  "updated": "2026-11-19T20:05:00Z",
  "title": "Village Plumbing Sheet Setup",
  "sub": "24-071 Pacific Edge. Bahia, Lobby + Coast and Seas, matched to the architect's VS model.",
  "run": "20261119-VSV", "model": "PLMB-24220-R25",
  "sync": "P1 + P2 SYNCED TO CENTRAL", "syncOk": true,
  "totals": [{"v": "30/44", "l": "sheets"}, {"v": "11/13", "l": "steps"},
             {"v": "2.4M", "l": "subagent tokens", "cls": "tok", "title": "29 agents reported; main session not counted"}],
  "level": {"n": 4, "done": false, "text": "LEVEL 4 · ENLARGED PLANS",
            "labels": ["Loaded", "P1", "P2", "Enlarged", "Synced"]},
  "steps": [{"t": "Match P1 sheets", "n": "plans aligned to the arch", "s": "ok"},
            {"t": "Enlarged plans", "n": "writing Seas", "s": "run",
             "est": "15 min", "left": "~12 min left",
             "sub": [{"t": "Bahia dry run + write", "s": "ok"}, {"t": "Seas dry run + write", "s": "run", "est": "8 min"}]}],
  "itemsName": "Sheets",
  "groups": [{"name": "Bahia", "rows": [{"label": "P1", "items": [
      {"id": "B-P1.01", "short": "B-P1.01", "s": "d", "f": false, "note": "rooms / check result"}]}]}],
  "fixes": [{"id": "l101", "where": "L-P1.01", "what": "Move the water meter notes off the notes band.",
             "sheets": ["L-P1.01"]}],
  "questions": [{"id": "q-enl-sheet", "q": "Which sheet should the Seas linen room go on?",
                 "why": "The arch puts it on S-A6.01, which is not in the plan.", "blocks": "VSV-ENL-T1 Seas",
                 "options": ["S-P6.01", "Skip it"]}]
}
```
- step `s`: `ok` | `run` | `wait` | `fail`. `sub` (optional) lists every task inside a step with its own `s`; the page
  shows "n of m left" and opens the list for a running or failed step. List ALL remaining tasks of each step
  (dry runs, writes, exports, checks, syncs), and tick them off as they finish.
- `est` on a step or sub-task = estimated duration (shown as "~8 min" until done); step `left` = time remaining.
  Base estimates on this run's own timings (agent duration_ms in task notifications), not guesses.
- Tokens: add a totals entry `{"v": "<sum>", "l": "subagent tokens", "cls": "tok"}`: the sum of `subagent_tokens`
  from every agent task notification this run (say in `title` how many agents and that the main session is not
  counted). Update it at each tracker update. item `s`: `d` done (verified) | `r` in progress | `p` queued |
  `x` failed; `f: true` = needs a hand fix (red underline). Mark an item `d` only after verify + visual pass.
- `short` keeps chips small (first chip of a row full number, the rest e.g. "1.02"). `note` shows on click.
- `level`: 3-6 milestones for the run type (plan check: Intake, Plan, Writes, Checks, Letter, Synced).
- `fixes[].sheets` lists the item ids the fix clears: ticking the fix removes their red underline (an item stays
  red while any fix that lists it is unticked). Put `f: true` on those items too, for viewers without the db.
- `fixes` ids are stable slugs; never reuse an id for a different fix (ticks are keyed by id).
- Plain words in every string (Jeremy reads it); counts only after counting them.
