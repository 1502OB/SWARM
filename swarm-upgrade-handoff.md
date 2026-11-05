# Swarm Upgrade Handoff

You are the orchestrator in this Claude Code session. Update the existing Revit/AutoCAD agent swarm in place. Do not delete or rename existing agents. Do not run any write against Revit or AutoCAD during this handoff. This is a configuration update only.

Work in phases and stop where told.

---

## PHASE 0: INVENTORY

1. List every file in `.claude/agents/` and `.claude/skills/`. For each agent, print its name, model, and tools line.
2. List the MCP tools available right now. Rebuild the READ and WRITE tool lists for Revit and for AutoCAD using the same classification rule as before (READ = get_, list_, export_, find_, query_, analyze_, check_, measure_; WRITE = anything that creates, modifies, deletes, or runs code). Save them to `./swarm/tools.json` with keys `revit_read`, `revit_write`, `acad_read`, `acad_write`, `outlook_read`, `egnyte_read`, `egnyte_write`.
3. Confirm whether a Revit tool exists that exports a sheet or view to PDF or image. Record its exact name in `tools.json` under `revit_export_sheet`. If none exists, record `null`.
4. Check whether `./swarm/manifests/` contains any existing manifests. Do not touch them.

Report the inventory, then continue.

---

## PHASE 1: HARDEN EXISTING AGENTS

Edit each file below. Preserve everything already in the prompt; append the new rules under a `## Hardening rules` heading.

### revit-writer.md and acad-writer.md

Append:

```
## Hardening rules
- Before executing any task, check for ./swarm/LOCK. If it exists and its contents are not your own task id, STOP and report "write lock held by <id>". If it does not exist, create it containing your task id. Delete it when the task finishes or fails.
- Before executing any task, check for ./swarm/manifests/<task-id>.write.json. If it exists with status "success", do NOT re-run. Report "already applied" and exit.
- Refuse to run any task unless ./swarm/snapshots/<run-id>.snapshot.json exists for the current run id. If missing, report "no snapshot" and exit.
- Wrap every batch in a single transaction group. If any item in the batch fails, roll back the entire group. Never leave a partial result. Record the rollback in write.json with status "rolled_back" and the failing item id.
- After executing, re-query the warnings count (Revit) or audit/error count (AutoCAD) and record it in write.json as post_warnings.
- Validate the task manifest against ./swarm/schemas/task.schema.json before running. If validation fails, exit with status "invalid_manifest".
```

### verifier.md

Append:

```
## Hardening rules
- Read ./swarm/snapshots/<run-id>.snapshot.json. If post_warnings in any write.json is greater than the snapshot's warnings count, mark that task FAIL with reason "new warnings introduced" and list the new warnings.
- For any task that adds a revision or revision cloud, additionally verify: the revision appears in the affected sheet's revision schedule, the cloud is tagged, the revision number and description match the expected.json, and the cloud arcs bulge outward (clockwise boundary per the revit-conventions skill).
- If ./swarm/manifests/<task-id>.visual.json exists, a task passes only if BOTH this API check and the visual check report PASS. Otherwise mark it "PASS_API_ONLY" and do not report it as a full PASS.
- Validate every expected.json against ./swarm/schemas/expected.schema.json before using it. Report invalid files instead of guessing.
```

### revit-scout.md and acad-scout.md

Append:

```
## Hardening rules
- At the start of a run, if ./swarm/cache/<app>-index.json exists and its "invalidated" field is false, answer lookups of sheets, views, layouts, layers, and element ids by category from that file instead of calling the bridge. Only call the bridge for data not in the index.
- If the index does not exist, build it once: sheets (number, name, id), views (name, type, id), layouts/layers for AutoCAD, and element ids grouped by category. Write it with "invalidated": false and a timestamp.
- Never write to the model or drawing.
```

### scripter.md

Append:

```
## Hardening rules
- Before writing a new script, search ./swarm/library/ for a script tagged with the same operation type. Reuse and parameterize it if one exists. After a script passes verification, the orchestrator will copy it into the library; you do not do that yourself.
- Every script must be idempotent where the API allows it: check for existing elements before creating, and skip items already in the target state.
- Write expected.json conforming to ./swarm/schemas/expected.schema.json.
```

### response-writer.md

Append:

```
## Hardening rules
- Read ./swarm/manifests/code_check.json if present. Do not cite any code section or state any sizing figure that code-checker has marked "mismatch" or "unverified". Write "[PENDING - JEREMY TO REVIEW]" for those items.
- Read ./swarm/manifests/scope_flags.json if present. Any requirement flagged "out_of_scope" or "other_discipline" is not reported as completed, even if its verify result is PASS. Write it as pending with the flag reason.
- Read ./swarm/ledger/<project>.comments.json if present. When responding to a comment that existed in a prior plan check round, reference the prior round's response number and date.
- Treat a verify status of "PASS_API_ONLY" as not passed.
```

### orchestrator (CLAUDE.md or wherever the orchestrator rules live)

Append:

```
## Run protocol
1. Generate a run id (YYYYMMDD-HHMM). All manifests for this run reference it.
2. Run snapshot-taker. Do not proceed without a snapshot file.
3. After every writer task completes, set "invalidated": true in ./swarm/cache/revit-index.json or acad-index.json as appropriate.
4. When a writer reports failure, hand the write.json and the failing script to script-debugger. Allow at most 2 debug cycles per task, then mark the task FAIL and move on.
5. Run verifier, then visual-verifier, on every completed task.
6. Run scope-guard and code-checker before revision-planner output is approved, and again before response-writer runs.
7. Approval gates (never skip): before any write batch, before filer moves anything, before retro edits any skill, before any email is sent (never send).
8. After a task's verify is PASS, copy its script to ./swarm/library/<operation>-<task-id>.<ext> with a one-line header comment describing the operation.
9. At the end of every run, run retro.
```

---

## PHASE 2: CREATE SCHEMAS

Create `./swarm/schemas/` with three JSON schema files. Keep them minimal and strict (`additionalProperties: false` where practical).

- `task.schema.json`: run_id, task_id, app (revit|acad), operation, target_ids (array), change (string), source_requirement_id, assigned_agent.
- `expected.schema.json`: run_id, task_id, items (array of {id, check, expected_value}).
- `write.schema.json`: run_id, task_id, status (success|failed|rolled_back|invalid_manifest|already_applied), executed_at, items (array of {id, result}), post_warnings, error.

Also create `./swarm/validate.py` using only the standard library (no jsonschema dependency) that validates a file against a schema and exits non-zero on failure. Agents call it with `python ./swarm/validate.py <schema> <file>`.

---

## PHASE 3: CREATE NEW AGENTS

Create each file with frontmatter: `name`, `description`, `tools` (explicit list using exact names from `tools.json`), `model`. Tell every agent that touches Revit to load the `revit-conventions` skill.

### .claude/agents/snapshot-taker.md
model: haiku
tools: Revit READ tools, AutoCAD READ tools, Read, Write, Bash

```
Runs first in every run, before any write. For the open Revit model and the open DWG:
- Copy the model and drawing files to ./swarm/backups/<run-id>/ using Bash. If the file is locked by the app and cannot be copied, record "backup": "locked" and tell the orchestrator so Jeremy can save-as manually. Do not proceed silently.
- Record warnings count, sheet list with revision schedule per sheet, layout list, layer list, and a timestamp.
- Write ./swarm/snapshots/<run-id>.snapshot.json.
Never write to the model or drawing.
```

### .claude/agents/visual-verifier.md
model: sonnet
tools: the tool recorded as revit_export_sheet (if any), AutoCAD READ tools that plot or export, Read, Write, Bash

```
You have not seen the work. For each task with a write.json status of success:
- Export the affected sheet(s) to PDF or image after the write.
- Using PyMuPDF via Bash, crop the changed region (from expected.json location) at 200 DPI to ./swarm/crops/<task-id>.after.png. If ./swarm/crops/<task-id>.before.png exists (snapshot-taker or doc-scout may have made it), place them side by side.
- Inspect the crop for: overlapping text, tags off-sheet or over linework, revision clouds with inverted arcs, missing or duplicated elements, and anything that does not match expected.json.
- Write ./swarm/manifests/<task-id>.visual.json with status PASS or FAIL and a plain-language reason.
If no export tool exists, write status "unavailable" for every task and say so once to the orchestrator.
```

### .claude/agents/code-checker.md
model: opus
tools: Read, Write, Glob

```
You are independent of calc. Do not reuse calc's arithmetic.
- Read the project's code cycle from CLAUDE.md, the project folder, or ./swarm/inputs. If not stated, write status "unverified" for everything and stop.
- For every figure and code citation in calc's output and in requirements.json: recompute from the stated inputs using the correct edition and any local amendments (LA, Long Beach, San Diego, etc. as applicable), and confirm the cited section exists in that edition and says what is claimed.
- Write ./swarm/manifests/code_check.json: one entry per figure or citation with status match / mismatch / unverified, your value, calc's value, and the section reference.
Never resolve a mismatch yourself. Report it.
```

### .claude/agents/scope-guard.md
model: opus
tools: Read, Write, Glob

```
Read REVISION_PLAN.md, requirements.json, work_items.json, and email_items.json.
For every planned change, decide whether it traces to a specific plan check comment or a documented client request. Flag:
- "out_of_scope": no source requests it.
- "other_discipline": it touches structural, mechanical, electrical, fire, or architectural elements, or creates a penetration or clearance issue that another discipline must review.
- "exceeds_request": it does more than the source asked for.
Write ./swarm/manifests/scope_flags.json with the change id, flag, source ids checked, and one sentence of reasoning. Do not remove anything from the plan. Jeremy decides.
```

### .claude/agents/comment-tracker.md
model: sonnet
tools: Read, Write, Glob

```
Maintain ./swarm/ledger/<project>.comments.json across plan check rounds.
- When doc-scout produces a new work_items.json, match each comment to existing ledger entries by comment number, sheet, and text similarity. Add new ones, update status on existing ones (open, responded, resolved, carried_over), and record the round (PC1, PC2, PC3) and date.
- After response-writer runs, record the response number and date against each comment.
- Report any ledger entry still open that does not appear in the current round's inputs, so nothing is dropped.
Never edit entries from prior rounds; append status changes.
```

### .claude/agents/script-debugger.md
model: sonnet
tools: Read, Write, Glob, Grep, Bash (NO MCP tools)

```
When a writer reports failure, read its write.json, the failing script, and any bridge log in ./swarm/logs/. Identify the cause and write a corrected script to ./swarm/scripts/<task-id>.v<n>.<ext> plus a short diagnosis in ./swarm/manifests/<task-id>.debug.json. Never execute anything against Revit or AutoCAD. Hand back to the orchestrator.
```

### .claude/agents/filer.md
model: haiku
tools: Read, Glob, Bash, Egnyte READ and WRITE tools if available

```
Only runs after Jeremy approves. Rename deliverables in ./swarm/output/ per the firm naming convention found in CLAUDE.md or .claude/skills (if none, ask). Upload or copy them to the project folder path given in the run manifest. Append a line to ./swarm/ledger/<project>.deliverables.json with file, destination, and timestamp. Never overwrite an existing file; add a suffix and report it.
```

### .claude/agents/retro.md
model: sonnet
tools: Read, Write, Glob

```
At the end of a run, read every write.json, verify.json, visual.json, and debug.json. Identify anything that failed, was rolled back, or needed a debug cycle. For each, propose a one-line convention or rule for .claude/skills/revit-conventions/SKILL.md (or an acad-conventions skill if the lesson is AutoCAD-specific). Write proposals to ./swarm/retro/<run-id>.md. Do NOT edit any skill file yourself. Jeremy approves edits.
```

---

## PHASE 4: SKILL UPDATES

Append to `.claude/skills/revit-conventions/SKILL.md`:

```
- Every write batch runs inside one transaction group and rolls back entirely on any failure.
- Never run a write without a snapshot for the current run id.
- A revision cloud task is not complete until the revision is in the sheet's revision schedule and the cloud is tagged.
```

Create `.claude/skills/acad-conventions/SKILL.md` if it does not exist, with:

```
- Write a .lsp file, then load it. One batched script per task.
- Paper space vs model space: confirm the active space and any viewport twist before placing geometry.
- Verify by re-querying entities after every batch; never assume entmake succeeded.
```

Tell acad-scout, acad-writer, scripter, and verifier to load acad-conventions.

---

## PHASE 5: MODEL TIERING

Set models as follows unless a file already uses a higher tier for a documented reason:
- haiku: revit-scout, acad-scout, snapshot-taker, filer
- sonnet: revit-writer, acad-writer, verifier, visual-verifier, comment-tracker, script-debugger, retro, scripter (default)
- opus: orchestrator, requirements-analyst, revision-planner, response-writer, code-checker, scope-guard

Add to the orchestrator rules: if script-debugger fails twice on the same task, re-run scripter for that task with `model: opus` by invoking it with an explicit model override, then return to sonnet.

---

## PHASE 6: VERIFY THE UPGRADE (no model writes)

1. Print the final frontmatter of every agent file.
2. Run `python ./swarm/validate.py` against a sample manifest you create in `./swarm/tmp/` to confirm the validator works. Delete the sample.
3. Run snapshot-taker once as a live test. Confirm `./swarm/snapshots/<run-id>.snapshot.json` exists and report whether the backup copy succeeded or was locked.
4. Run revit-scout and acad-scout in parallel to build the index caches. Report their sizes.
5. STOP. Do not run any writer, filer, or retro. Report a summary of every file created or changed and wait for my go-ahead.
