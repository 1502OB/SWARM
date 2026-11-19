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
Edit the local status JSON, set `updated` to now (ISO UTC), then `ArtifactData set` with `file_path` and
`if_version` = the last version. On a version conflict, `get` the doc, merge, retry once. No republish, so no prompt.
Republish only to change the template itself (same file_path; omit `capabilities` so they carry forward).
Before the final sync, `ArtifactData list collection:"fixes"` to see which hand fixes Jeremy has ticked, and say so.

## status/now schema
```json
{
  "updated": "2026-11-19T20:05:00Z",
  "title": "Village Plumbing Sheet Setup",
  "sub": "24-071 Pacific Edge. Bahia, Lobby + Coast and Seas, matched to the architect's VS model.",
  "run": "20261119-VSV", "model": "PLMB-24220-R25",
  "sync": "P1 + P2 SYNCED TO CENTRAL", "syncOk": true,
  "totals": [{"v": "30/44", "l": "sheets"}, {"v": "11/13", "l": "steps"}],
  "level": {"n": 4, "done": false, "text": "LEVEL 4 · ENLARGED PLANS",
            "labels": ["Loaded", "P1", "P2", "Enlarged", "Synced"]},
  "steps": [{"t": "Match P1 sheets", "n": "plans aligned to the arch", "s": "ok"}],
  "itemsName": "Sheets",
  "groups": [{"name": "Bahia", "rows": [{"label": "P1", "items": [
      {"id": "B-P1.01", "short": "B-P1.01", "s": "d", "f": false, "note": "rooms / check result"}]}]}],
  "fixes": [{"id": "l101", "where": "L-P1.01", "what": "Move the water meter notes off the notes band."}]
}
```
- step `s`: `ok` | `run` | `wait` | `fail`. item `s`: `d` done (verified) | `r` in progress | `p` queued |
  `x` failed; `f: true` = needs a hand fix (red underline). Mark an item `d` only after verify + visual pass.
- `short` keeps chips small (first chip of a row full number, the rest e.g. "1.02"). `note` shows on click.
- `level`: 3-6 milestones for the run type (plan check: Intake, Plan, Writes, Checks, Letter, Synced).
- `fixes` ids are stable slugs; never reuse an id for a different fix (ticks are keyed by id).
- Plain words in every string (Jeremy reads it); counts only after counting them.
