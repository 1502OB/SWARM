# Swarm lessons learned

The running log of lessons from swarm work. Sessions start in `~/swarm`, so this file is where they find them. (The
older memory file `~/.claude/projects/C--Users-Jeremy/memory/lessons-learned.md` is only loaded by sessions started
in the home folder; its entries are folded in below, with its old number shown as `(mem N)`.)

**How to use it**
- Orchestrator: read this before each project and before delivering anything.
- Add a lesson the day it happens: next free id in its section, date, project, one line on what to do differently,
  and the evidence file.
- `→ agent` means the lesson is built into that agent's definition. `→ prompt` means it is in a swarm prompt.
  `open` means it lives only here so far; `reference` means it is for the orchestrator's
  own scripts and is read here.
- **Agents are generated.** Change `swarm/build/gen_agents*.py` and run `python swarm/build/gen_all.py`. Never edit
  `~/.claude/agents/*.md` directly: the next regeneration silently overwrites it (this happened to the 10-21
  acad-writer save rule).
- Fold-in passes: 2026-10-20 (all agents regenerated), 2026-11-02 (this file; applied), 2026-11-03 (Bakersfield pad11); see the bottom of this file.

Sources cited below: Garden 25-088 = `garden/qaqc2/plan/NARRATIVE_LOG.md` (NL), `garden/qaqc3/`; Northgate 23-082 =
`northgate/STATUS.md`, `HANDOFF.md`, `manifests/`, `logs/`; Retreat (job 26016; "25058" in its files is the
architect's number) = `retreat/qaqc1/`; Bakersfield/Encino = `archive/bakersfield/REVISION_PLAN.md`, `REVISION_PLAN.md`, `REPORT.md`.
Since 2026-11-02 project folders use their Egnyte names (O21); the short names above still work as junctions.

---

## O. Orchestrator and session setup

| id | lesson | source | status |
|---|---|---|---|
| O1 | Set the model on every subagent (Haiku scouts, Sonnet mechanical, Opus judgment). Ask before a rule change that costs more. | mem 1 | → agents (frontmatter) |
| O2 | Give every parallel agent its own scratch folder and unique output names, and say "write only your own files". | mem 2; Garden ADDENDUM:10 | → QAQC prompt |
| O3 | Tokens go to the orchestrator and to general-purpose agents. Use defined agents with 1-2 sheets each, never screenshot yourself, and keep one project per session. | mem 5a | → prompts |
| O4 | Without per-server MCP timeouts a hung call waits 30 min. Timeouts are now set in `~/.claude.json`. | mem 5b | done |
| O5 | `swarm/manifests/project.json` is shared between sessions; another project overwrote it and project_app_guard blocked agents. Rewrite or check it at the start of every model-agent run, and write it from Python/Bash (a BOM blocks the guard). | Garden NL:166,178; mem 11a | → plan-check prompt (step 0 + before each launch) |
| O6 | The sheet-reviewer cap (2 sheets) overrides an orchestrator "3 each". Plan reviewer work in pairs. | Garden NL:180 | → QAQC prompt |
| O7 | 20 concurrent subagents max. Queue the overflow in a `QUEUE.md`. | Garden NL:179 | → QAQC prompt |
| O8 | Re-review rounds: PRIOR reviewer (Sonnet) only re-checks open items; NEW reviewer (Opus) only finds new ones and reads the prior list. Both give `revise_how`, `who`, `depends_on`. | Garden qaqc3/ROLES.md | → brief template + QAQC prompt step 2 |
| O9 | Briefs need: a "decided, don't flag" block; the issue purpose (internal / bid / plan check); exact hold/scope wording ("Level 3 on hold" made scouts skip in-scope items). | Garden ADDENDUM:7, NL:20; Retreat check P0.01-07 | → brief template |
| O10 | Before telling reviewers "text is not extractable", probe it. On Retreat it was extractable and reviewers hedged for nothing. | Retreat check P0.02-02 | → QAQC prompt step 1 + brief template |
| O11 | Checker corrections were applied by hand in `overrides.json`, and one correction was itself wrong. Review each correction before applying it, and keep the overrides file. | Retreat build/overrides.json P2.52-02 | → QAQC prompt step 5 + markup_build (overrides `_why` check) |
| O12 | The markup builder was rewritten per project (`retreat/qaqc1/build/markup.py`, `garden/qaqc3/markup3.py`). Make it a shared tool in `swarm/tools/`. | Retreat build/markup.py:1 | → `tools/markup_build.py` (matches Retreat output exactly) |
| O13 | Verify counts with a command before stating them. | mem 15 | → both prompts |
| O14 | Health-check a bridge before scheduling agents on it. The AutoCAD bridge has hung for 1800 s. | Bakersfield RP:59,161 | → prompt step 0 |
| O15 | Hold uncertain auto-classified changes for Jeremy (`HELD.md`) and apply only the confident ones. | Garden NL:92, BF/HELD.md | → revision-planner |
| O16 | Back up an existing deliverable before re-plotting or rebuilding it. Check-set plots reused the Transout file name. | Northgate STATUS:57 | → plan-check prompt + markup_build (moves old out/ aside) |
| O17 | A LISP paren check exists (`northgate/scripts/lspcheck.py`). The scripter can't run it, so the orchestrator runs it before the acad-writer. Move it to `swarm/tools/`. 22-022 (retro P19): lspcheck.py missed stray ')' that `tools/lsp_lint.py` caught - use lsp_lint (scripter, debugger, orchestrator); lspcheck is legacy. | Northgate STATUS:47; retro 20261106-1325 P19 | → `tools/lsp_lint.py` + plan-check prompt step 5 |
| O18 | After editing a guard or hook regex, retest every mode it gates. A dropped space blocked every Excel helper mode. | Northgate STATUS:51 | → plan-check prompt |
| O20 | Swarm agents read Outlook, Teams and Egnyte (2026-11-02): email-scout gets all three, doc-scout gets Egnyte, read tools only (the connector itself also holds Mail.Send and Files.ReadWrite, so only the tool lists keep agents read-only). Egnyte files are fetched into inputs/ by the orchestrator after Jeremy confirms the match. Teams search returns bot cards ("Nexus" task register): not a source. | Jeremy 2026-11-02 | → email-scout, doc-scout, plan-check prompt step 1b |
| O21 | Each project gets `swarm/<Egnyte project folder name>` (spaces removed). Find it by job number under `/Shared/Projects/<year>/`; the architect's number is not the job number (Retreat). Old short folders (garden, northgate, retreat, archive/bakersfield, archive/encino) were renamed 2026-11-02 and left as junctions so existing paths keep working. To remove a junction use `cmd /c rmdir <old name>` (no /s), never PowerShell 5.1 `Remove-Item -Recurse`, which can follow the link and delete the real files. | Jeremy 2026-11-02 | → both prompts, brief template, project.json `project_folder`/`egnyte_folder` |
| O22 | Set manifests/project.json at project start: acad-ops refused to save because it still described Northgate. Agents append to shared logs, never overwrite (a writer wiped writer_log.md). | 22-001 pc9-check | → COMMON, acad-ops, plan-check prompt |
| O19 | Subagents are blocked from live `L:` writes. The orchestrator does those itself, with Jeremy's OK. | Northgate STATUS:60 | → both prompts |
| O23 | Hung Revit/AutoCAD calls are usually a modal dialog the bridge can't see. `tools/dialog_check.ps1` (read-only: never clicks or focuses) lists each app's dialogs with title, text and buttons (WM_GETTEXT, then UI Automation for TaskDialogs/WPF), flags a disabled main window or a not-responding app, and with -CaptureDir saves a PNG of the dialog window only (never the desktop: a full screenshot showed Outlook/Teams). Exit 0 clear / 1 blocked / 2 not running. Tested on live Revit 2025 + AutoCAD 2027 (clear) and on self-test dialogs (message box, modal WinForms), in pwsh 7 and PS 5.1. Choosing a button stays Jeremy's. | Jeremy 2026-11-04 | → MARKUP_SWARM_PROMPT step 5 (DIALOG CHECK) |
| O24 | Swarm upgrade (swarm-upgrade-handoff.md): run ids, snapshot-taker before any write (writers refuse without snapshots/<run-id>.snapshot.json), strict schemas (schemas/*.schema.json via validate.py; expected.json items are only {id, check, expected_value}; write.json is strict and details go to <task>.write.log.md), per-document index caches (cache/<app>-index.json), visual-verifier BEFORE verifier (else every task is PASS_API_ONLY), scope-guard + code-checker at the Step 4 gate and before the letter, script library (library/), retro proposals. Locks stay per app (locks/*.lock, 'released'), not ./swarm/LOCK: writers cannot delete files. New Bash profiles need HOOK_PATCH_2026-11-04.md. | Jeremy 2026-11-04 | → MARKUP_SWARM_PROMPT Run protocol, gen_agents*.py |
| O25 | Skills as contracts (AU CS2138 handout): every skill we own (revit-api, revit-conventions, acad-conventions, plumbing-code) has a Contract block (Never / Irreversible and how softened / Failure looks like / Done when), a third-person description under ~300 chars, name = folder. Scripts return done/skipped/failed counts (writers -> write.json COUNTS; verifier fails a mismatch). write.json status 'timeout' added. tools/check_task.py checks task ids against the index cache (ERROR with valid alternatives / WARN unverifiable). plumbing-code skill: 2025/2022/2019 CPC, 2019 LAPC (scanned PNG), LADBS bulletins, LA County indexed from L:\Shared\Documents\01_Codes & Standards\Codes and Standard (147 MB); rubric versioned separately; swarm/codes retired. Background jobs: worktree.bgIsolation none in swarm/.claude/settings.json (Jeremy). | Jeremy 2026-11-05 | → MARKUP Run protocol 10-12, code-checker, code-reviewer |
| O26 | Per-project manifests + ids (22-022 retro): task ids <task_prefix>-R<n>-T<m> (schema pattern; check_task.py errors on a foreign prefix; project.json task_prefix, e.g. C22); agents write to <project_folder>/manifests/ (<PROJECT> in COMMON); swarm/manifests keeps only project.json (hooks read it). In-flight runs copied on 11-06: 24-071 (123 files) and 22-022 (109), MIGRATED.md in each; originals left in swarm/manifests until Jeremy deletes them. Note: C22 is 22-022 Carmel PCH (not 24-049). | 22-022 retro | → all agents (COMMON), planner, MARKUP 13 |
| O27 | Checks scaled to the change: light operations (text, sheet/title-block parameters) get verifier per task + ONE batch visual pass on the final plot set (<run-id>.batch.visual.json); geometry tasks keep per-task visual checks. | 22-022 retro | → verifier, visual-verifier, MARKUP 15 |
| O28 | Hooks are Claude's to edit (Jeremy 2026-11-06), tested by ~/.claude/hooks/tests/test_guards.py (49 cases) and committed via build/sync_claude.py pull. 11-06 pass: guardlib.py strips C# literals/comments and LISP comments before deny patterns (fixed false blocks: text 'CLOSE TO WALL', comments 'Audit'/'Delete', label "EXIT", vl-file-size); closed gaps: read-only acad guard now denies vla-SendCommand/PostCommand and modifying vla methods (Move/Copy/Explode/...), writer may queue only allow-listed commands, reflection (GetMethod/InvokeMember) blocked in Revit guards. | Jeremy 2026-11-06 | → hooks |

## R. Revit bridge and API

| id | lesson | source | status |
|---|---|---|---|
| R1 | Run only ONE Revit agent at a time. Parallel scouts got crossed responses and wrong element ids. Label each check's return text. | mem 16a; Garden NL:19 | → all Revit agents (REVIT_SERIAL) |
| R2 | The generic read tools come back blank or wrong for sheet numbers, text note text, match lines and templates. Use one read-only C# dump through `send_code_to_revit` instead (1.7 s, complete). | mem 16a; Garden NL:24-26; Bakersfield RP:32 | → revit-scout, revision-planner (send_code + guard) |
| R3 | Never loop over every parameter of every element. A ~100k scan crashed Revit and lost unsynced work. | mem 16 | → REVIT_SERIAL |
| R4 | Sheet Number can read blank on the sheet while the title block instance has it. Match by the title block number, else by name. | Bakersfield RP:36 | → revit-scout |
| R5 | A long sync or write holds the bridge for about 10 min, and a Revit dialog blocks it. Replies can cross onto the next call. Don't stack calls; check state read-only. | mem 16c; Garden NL:99-100,145 | → REVIT_SERIAL, writers |
| R6 | "Code execution timed out" often still committed. Treat it as a timeout and verify before anything else. | Garden NL:153 | → writers |
| R7 | Tags with leaders take about 1 tag/s, so more than ~100 per call passes the 300 s timeout. Batch 2-3 views per call. | mem 16c; Garden NL:161 | → scripter, revision-planner |
| R8 | Dependent views keep their tags on the parent. Dedupe by distance (≤2 ft), not by rounded coordinates (that made 131 duplicates). | mem 16c; Garden NL:146-153 | → scripter, revit-scout |
| R9 | Take expected.json counts after commit, in a separate read-only call. System calcs settle after commit (3050 vs 3005). | mem 16c; Garden NL:120 | → scripter |
| R10 | A DFU/FU of 0 everywhere usually means the system type's Calculations = Performance. Test the change in a rolled-back transaction. | mem 16c; Garden NL:115 | → revision-planner |
| R11 | Stacked or odd tag labels come from the tag type's Yes/No label switches. Don't change Project Units to clean up decimals: fractional WSFU would misreport in 185 schedules. | mem; Garden NL:191,198 | → scripter |
| R12 | The API can't retarget view references or match lines, edit label formats, or set general unit accuracy other than 1E-06. These go on the manual worklist. | Garden NL:71,198 | → scripter, revision-planner |
| R13 | A user sync mid-batch can make a committed write vanish. Do a read-only CHECK after every write and before the verifier. | mem; Garden NL:203-205 | → writers (post_check) |
| R14 | Before applying a QAQC list, confirm the sheet set still matches the reviewed PDF, and mark stale items `superseded`. | mem 16b; Garden NL:34 | → revision-planner |
| R15 | Export by sheet number, not sheet name: duplicate names overwrote PDFs. | Garden NL:181 | → scripter |
| R16 | Locate edits by exact old text (rows shift), check the old value, and roll back the batch on any mismatch. | Garden NL:87,102 | → scripter |
| R17 | Batch cycle: SYNC A → one-transaction write → verifier → SYNC B → log. Jeremy authorized Claude to sync (2026-10-20). The permission classifier still blocks model-wide or shared-type writes until he OKs that batch. | Garden NL:46,63,116 | → plan-check prompt + orchestrator_guard sync template (2026-11-02) |
| R18 | Revit role split (Jeremy 2026-11-02): revit-writer = transactions + switching to an already-open model only. New revit-ops agent (sonnet) = sync, save, save-as, relinquish, close, detach, audit, open files listed in locks/revit_open_approved.txt (Jeremy approves each); no model edits. Orchestrator keeps the sync-only template for routine SYNC A/B. Sync only after verifier PASS. Read-only work on another open model needs no switch: use document.Application.Documents. | Bakersfield pad11-1102 | -> writer_guard.py (revit / revit-ops), gen_agents.py |
| R19 | Failed runs are the costliest step: every Revit script supports ["dryrun"] (read-only preflight, names resolved in THIS model, planned changes); one writer runs dryrun, checks GO conditions, then runs for real. Scripter lists "new_api" members not in a known-good script. | Bakersfield pad11-1102 T1/T8a (3 wasted round trips) | -> scripter, revit-writer |
| R20 | Revit 2024 API facts: TransformWithBoundary.GetModelToProjectionTransform() (no GetTransform); NewTransitionFitting(pipeConnector first, familyConnector); plumb branch off a sloped pipe fails NewTee/NewTakeoff angle checks, so place the tee family, BreakCurve, ConnectTo; level/type names differ between pads (resolve by elevation/name in the model); storm system types may be classed SupplyHydronic. FormattedText has no Replace(): use SetPlainText(new TextRange(start,len), text). Bridge truncates returns over ~3 KB. | Bakersfield pad11-1102 T1, T8a, T8a2 | -> scripter |
| R21 | Token use: scouts write <task>.digest.md (2 KB) beside big manifests; downstream agents read digests first. Scripter defaults to sonnet; orchestrator passes opus only for geometry-heavy scripts. A task prompt that names the expected document overrides project.json (agents kept flagging the Garden project.json). | Jeremy 2026-11-02 | -> gen_agents.py COMMON, SCOUT_RULES |
| R22 | New elements land on the ACTIVE workset: set ELEM_PARTITION_PARAM to the reference model's workset (storm = STORM) in the creating script, and have the verifier check visibility per target view (FilteredElementCollector(doc, viewId)), not just existence. Check target view ranges against new element elevations before building. | Bakersfield pad11-1102 T8a -> T9 | -> scripter, verifier (applied 2026-11-03; R20's FormattedText line too. Both were marked but missing from the generator) |
| R23 | Rotating a plan view: View.CropBox with a rotated transform does NOT rotate it (RightDirection stays +X, clouds got recreated anyway). Rotate the crop region element with RotateElement about a vertical axis through the crop centre; keep viewport centres; record cloud corners in model coords and recreate them after. Verify by RightDirection. | Bakersfield pad11 HANDOFF, T1.cs | → scripter, verifier, revit-conventions |
| R24 | In a 180-rotated view, keynote diamonds need HandOrientation -X or a "6" reads "9". Check every diamond and tag after a rotation. | pad11 HANDOFF, T9 C | → scripter, sheet-reviewer |
| R25 | Revit cloud standard: one "Revision Tag" per cloud at (right - 0.17", top + 0.08") paper, outside the cloud. Clouds and tags must not touch other clouds or cut text; merge touching clouds into one rectilinear outline cloud with one delta. Took 3 check-plot rounds (T9, T10, T10b). | pad11 HANDOFF, plots/inspect2-3 RECHECK*.md, T10.cs | → scripter, verifier, sheet-reviewer, revit-conventions |
| R26 | Connector.Owner elements go invalid after deletes (re-fetch by id). No spot-slope API; spot elevation = NewSpotElevation with ParseFromStableRepresentation(pipe.UniqueId + ":9:LINEAR"), Display Elevations = Bottom. Hide heads of sections that are on no sheet. | pad11 HANDOFF | → scripter |
| R27 | Annotation clash moves: try both axes, take the smallest clear move, cap it (6 ft) and abort the item over the cap. T10b proposed 13 ft sideways when 1.6 ft up was clear; the cap caught it. | pad11 logs/T10b.txt | → scripter |
| R28 | Failed scripts (revised 2026-11-04 by swarm-upgrade-handoff, Jeremy chose 'handoff as written'): script-debugger (sonnet, no MCP) gets at most 2 debug cycles per task (scripts/<task>.v<n>.*, <task>.debug.json); if both fail, scripter re-runs once with model override opus (fix mode), then the task is marked FAIL and the run moves on. Anything that may have landed waits for Jeremy's go. Superseded the same-day 'one opus fix then Jeremy' rule. An opus review of every script stays rejected (failures were API/bridge facts that dry run + lspcheck catch cheaply). | Jeremy 2026-11-04 | → script-debugger, scripter fix mode, MARKUP_SWARM_PROMPT step 5 + Run protocol |
| R29 | Revit API grounding (AU 2026 CS2138 'Skills That Pay the Bills', DLL2LLM): skill ~/.claude/skills/revit-api generated with Autodesk's dll2llm (C:/Users/Jeremy/tools/dll2llm, commit 099c2de) from Revit 2025 RevitAPI.dll + RevitAPIUI.dll: 2306 types, references/core.md = the ~80 types our scripts use with firm notes, scripts/revit_api_check.py (inheritance, same-name types, static [ABSTRACT CLASS] utils, typo suggestions). Scripter loads it and lists 'members:'; orchestrator runs the checker before any writer. It caught GetTransform (R20) as NOT FOUND. Regenerate after a Revit upgrade. Scripter lesson blocks moved to revit-conventions/acad-conventions references/scripting-lessons.md (single source; scripter prompt -7.9 KB). | Jeremy 2026-11-05 | → scripter, script-debugger, MARKUP Run protocol 10 |

## A. AutoCAD bridge and LISP

| id | lesson | source | status |
|---|---|---|---|
| A1 | Keep execute_lisp payloads under ~600 chars (otherwise "malformed list on input"). Put logic in the .lsp. | Northgate HANDOFF:18 | → scripter, acad-scout |
| A2 | A timeout on a trivial call means AutoCAD is blocked: stop and report, never loop. Don't loop over all open documents in one call; save one document per call. | Northgate HANDOFF:19, logs/SAVEALL-1021f | → writers |
| A3 | Never open AutoCAD through computer-use (it starts a second empty instance). The bridge's `drawing open` reports "ok: opened" but does NOT open files in AutoCAD LT or full AutoCAD 2027 (22-022, 11-06 and 11-07; 2018 Mansfield 11-09: returned "opened", doc never appeared; Invoke-Item opened it in the running instance): open each DWG with Windows `Invoke-Item` (memory acad-start-open-first-dwg), then load mcp_dispatch.lsp and run `system init` if the bridge is on ezdxf. After a crash, run `system init`. | Northgate HANDOFF:17; mem 23 | → acad-writer |
| A4 | Don't trust the active document: target the DWG by name and make it active before ActiveX work. A plot-device change on an inactive document hangs AutoCAD. | Northgate STATUS:28,58 | → scripter |
| A5 | Jeremy runs OSMODE 16383. Use entmake/ActiveX, or `_non` / OSMODE 0. Wipeouts go to the top of the draw order. | mem 22; Northgate STATUS:31,39 | → scripter |
| A6 | Plotting from LISP via -PLOT / PlotToFile / the bridge's plot_pdf fails (Northgate). UPDATE 2026-11-03 (22-001): -PUBLISH of a DSD WORKS from acad-writer: (command "_.-PUBLISH" dsd) with BACKGROUNDPLOT 0, FILEDIA 0, CMDDIA 0 (restore after), DSD PromptForDwfName=FALSE, Setup= (layout page setup), output path in [Target] DWF=. Template: 22-001 pc9-check/cad/pc9publish.lsp + PC9_replot.dsd. Refuse to overwrite an existing output. | mem 18; Northgate STATUS:59; 22-001 pc9-check | → scripter (update) |
| A7 | Annotative MLEADERs keep a position per scale. Moves and undos must fix every scale context (BATCH-2 drifted 0.35"-1.6" on the 1/4" views). | Northgate QA_1527_plans:51, STATUS:56 | → scripter, verifier |
| A8 | `VPLAYER _All` means every viewport in the DWG. Use `_Select`. Frozen lists are in viewport xdata 1003. | mem 19; Northgate STATUS:33 | → scripter |
| A9 | Title blocks aren't always at 0,0. Place relative to each layout's title block insertion point. | mem 17; Northgate STATUS:29 | → scripter |
| A10 | Revision clouds: copy an existing one, go clockwise, size from measured geometry. | Northgate HANDOFF:20, STATUS:29 | → scripter |
| A11 | Placement scripts search candidate spots and abort per item on a clash, rather than forcing an overlap. | Northgate logs/GI144 | → scripter |
| A12 | Labels with iso copies change in both places. Plan text in a 45° iso viewport plots ~30% small, so use enlarged copies on an iso-only layer. | mem 21; Northgate STATUS:38 | → scripter |
| A13 | Rerouting a branch: delete the old wye/stub at the old main. | mem 20 | → scripter |
| A14 | Long TextString replies time out; return lengths or chunks. Screenshots cost ~25k tokens, so check the plotted PDF instead. | Northgate STATUS:60; Encino RP:38 | → acad-scout |
| A15 | Read-only LISP can still raise DBMOD. Record it at the first and last call. | Encino RP:38 | → acad-scout |
| A16 | Never call the bridge while a background agent is using it. Stop the agent before reopening or activating documents. | mem 24 | → prompt (one AutoCAD agent at a time) |
| A17 | The permission classifier can deny an in-place save. Report the drawing as unsaved and hand it to Jeremy. SaveAs, close, quit, purge and QSAVE stay blocked. | Northgate STATUS:53, logs/FD3-LEG2 | → acad-writer |
| A18 | AutoCAD file-ops split (Jeremy 2026-11-03): scouts/writers/verifier can't open DWGs, so P007 stalled. New acad-ops agent (sonnet) = open DWGs listed in locks/acad_open_approved.txt (file or folder/; in LT the orchestrator opens them with Invoke-Item, see A3), switch active drawing (one vla-activate), in-place save; no edits, save-as, close, quit, purge. acad-writer reports "needs acad-ops open". Also: MTEXT has no ActiveX Explode (vla-explode fails silently) - use (command "_.EXPLODE" e) on a temp copy and entdel only live enames. | 22-001 pc9-check | -> writer_guard.py (acad-ops), gen_agents.py |
| A19 | Permissions (Jeremy 2026-11-03): in auto mode the classifier denies subagent LISP that edits a DWG on L: or publishes there ("Modify Shared Resources"), even with an auto-mode soft-allow rule; one-time approvals of an exact call don't carry over to a subagent. drawing save works with a plain allow rule for mcp__autocad-mcp__drawing. Out of auto mode, subagent calls prompt Jeremy and run when approved. If a writer is denied: report, don't retry, ask Jeremy (approve prompt, or run the loaded c:COMMAND himself). | 22-001 pc9-check writer_log | → prompt, acad-writer |
| A20 | Clouds: locate, check and size by VERTEX bbox (group 10 min/max). vla-GetBoundingBox adds ~0.03-0.04 in of arc bulge (chord 0.14, bulge 0.52) and made the P3FIX position check abort. Keep arcs clear of text: sagitta = bulge x chord / 2. | 22-001 pc9-check (P3FIX) | → scripter, verifier |
| A21 | AutoLISP is not Common Lisp: no fboundp, no identity (cost two failed runs). Use (type sym) and (lambda (x) x). | 22-001 pc9fix/p001idxcloud | → scripter |
| A22 | Drawings can carry text that does not visibly plot: P0.01 had a stale model-space sheet index printing at ~0.05 pt next to the real one (an embedded Excel table with no text layer). A text-layer comparison produced a false "index wrong" finding. Check the rendered page before reporting. | 22-001 qaqc_final | → sheet-reviewer, prompt |
| A23 | Publish to a local test folder under the run first (no shared-drive permission block, QA before L:). An open drawing with unsaved edits publishes in its in-memory state with no save prompt. Splice into a new dated set; a PDF open in Revu blocks overwriting it. | 22-001 testplot | → scripter, plan-check prompt |
| A24 | Bridge fixes (22-022 retro, 2026-11-06; autocad-mcp branch swarm-fixes): mcp_dispatch.lsp mcp-escape-string escapes newline/CR/tab (a raw newline in a result broke the JSON and looked like a timeout: two debug cycles); AUTOCAD_MCP_IPC_TIMEOUT = 120 s in ~/.claude.json (was the 10 s default: most writer calls timed out); `system status`/`health` ping the LISP side (lisp_alive, busy, ping_ms) instead of reporting file_ipc ok while the dispatcher was dead. Timeout + status take effect on the next Claude Code session; the escape after mcp_dispatch.lsp reloads. | 22-022 retro | → bridge, MARKUP Run protocol 16 |
| A25 | AutoLISP checks before load: tools/lsp_lint.py (per-form balance naming the defun, non-AutoLISP functions like fboundp, duplicate/library-clashing defuns, attach via (command), undeclared setq, 
 in results); PostToolUse hook lints every .lsp written under swarm/scripts or library and bounces errors; writer_guard refuses to load a failing script. It caught the 11-06 C22-R8-T1 extra paren (line 405) and 8 newline results. tools/lsp_dryrun.py runs the script headless in AutoCAD Core Console on a COPY of the drawing: load + -PLAN (+ -RUN) in ~3 s; no ActiveX there (vlax-get-acad-object nil, layoutlist undefined) -> 'needs_full_autocad'. Run accoreconsole from Python/PowerShell (from Git Bash it hangs); it uses the profile's SECURELOAD (Claude may not change SECURELOAD). | 22-022 retro | → scripter (Bash profile scripter), script-debugger, writer_guard |
| A26 | Shared AutoLISP library swarm/library/acad/swarm_lib.lsp (swl:, harvested from the run-proven r8: helpers, parameters instead of globals): swl:result/san (one-line results), boxes, crossing-aware swl:clash, swl:vp + ms<->ps, clockwise swl:cloudpts/entmake-cloud/cloud-check, swl:preflight (live values), swl:best-spot, swl:queue-command (XREF/PDFATTACH/IMAGE/PUBLISH/REGENALL only; writer_guard enforces the same list). test_swarm_lib.lsp: 37/37 pass headless. Writers load it before the script; scripters never redefine swl: and prefix their own helpers with the task id. | 22-022 retro | → scripter, acad-writer, acad-conventions |
| A27 | Never attach an xref through the command line (`-XREF A <path>` via swl:queue-command) when the path has spaces: every L: project path does ("2018- 930 Mansfield", "F05 - MEP"). The space ended the path, AutoCAD opened a dialog, the bridge dispatcher hung (every call timed out) until Jeremy closed it, and nothing attached. Use ActiveX: `(vla-AttachExternalReference <layout block> "<full path>" "<name>" (vlax-3d-point 0 0 0) 1. 1. 1. 0. :vlax-false)` and `(vla-Detach (vla-Item (vla-get-Blocks doc) "<xref>"))`: synchronous, no dialog, no wait for CMDACTIVE. | Mansfield M18-R3-T1, 2026-11-09 | open -> scripter, acad-conventions |
| A28 | AutoLISP symbols are case-insensitive: a function `m4:vpinfo` and a global `M4:VPINFO` are ONE symbol, so `(setq M4:VPINFO ...)` deletes the function and the next call fails ("no function definition" or "syntax error"), usually only on the 2nd call (the headless dry run calls PLAN once and misses it). Hit 3 times in one run (M18-R3-T1 m18r3:mv, M18-R1-T1 c18r1:feas, M18-R4-T1 m4:vpinfo). Check: `python tools/lsp_collide.py <script> library/acad/swarm_lib.lsp` (multi-pair setq aware). Name functions with verbs, globals with nouns. | Mansfield 2026-11-09 | open -> lsp_lint.py (add check), scripter |
| A29 | `ssget` window/crossing modes (_W/_C/_F) select only in the CURRENT space: with a layout tab active (TILEMODE 0, CVPORT 1) a model-space window returns nothing (Mansfield M18-R2-T1 "verticals=0"; M18-R5-T3 found it too). For model-space reads use `(ssget "_X" (list ... (cons 410 "Model")))` and filter coordinates in LISP. Also: MTEXT vla bounding boxes are inflated; measure notes from DXF 10/71/43 or per-line temp copies. | Mansfield 2026-11-09 | open -> scripter, acad-conventions |
| A30 | Never open a DWG (Invoke-Item) while any agent is using the AutoCAD bridge: the open steals focus and the agent's next call times out (Mansfield 11-09 save of XPL_RISERS). Open files only between agent runs. AutoCAD also crashed once (APPCRASH 0xc0000374 heap) during a long read-only scan of 6 open xrefs: keep scans to one DWG per call batch, <=40 entities per execute_lisp, save each DWG as soon as its tasks pass. | Mansfield 2026-11-09 | open -> orchestrator, acad-scout |
| A31 | AutoLISP `(or a b)` returns T, not the first non-nil value: `(setq bt (or (tblobjname ...) (tblobjname ...)))` then `(entnext bt)` fails "bad argument type: lentityp T" (Mansfield M18-R3-T9). Use `(cond (x) (y))` to pick the first non-nil. Also: Windows file-open (Invoke-Item) can spawn a NEW AutoCAD instance stuck at a "Profile MFE" dialog; check `Get-Process acad` count after each open and stop the extra instance if it holds no drawing. | Mansfield 2026-11-09 | open -> scripter, orchestrator |
| A32 | A script's PLAN must test the same geometry as its CHECK. M18-COS-T2 PLAN tested only the cloud (+arc pad) for clashes, CHECK tested the whole note group (note + tag), so P6.08 "needed no move" in PLAN and FAILED CHECK (group box vs "SCALE: NTS"). Build one `box-of-group` function and call it from both. | Mansfield 2026-11-09 | open -> scripter |
| A33 | Viewport containment tests need a tolerance for geometry that is not being moved: P1.01's existing table cloud sat 0.04 in outside the model viewport, so "no viewport holds both cloud boxes" aborted M18-COS-T1a. Shrink the tested box by ~0.10 in, or test only the edges being moved. | Mansfield 2026-11-09 | open -> scripter |
| A34 | MText bottom: `insertion y - DXF 43` can be nonsense (P2.01 MText 1011 gave y 5008.9 vs real 2232.9; DXF 43 stale/negative after an earlier string edit). Use the M18-R4-T1 rule: bbox top - max(bbox height, DXF 43) (swl:vbox). Refines A29. Every measure of the same entity across tasks must use one shared helper. | Mansfield 2026-11-09 | open -> swarm_lib (add swl:mtext-bottom), scripter |
| A35 | Sheet/table border polylines (layer FRAME, e.g. P1.03 637E5) must not count as cloud hits when a cloud edge sits on a table border (M18-R3-T11 aborted at every chord). BUT once excluded, placement of NEW text must still treat FRAME as an obstacle: the T11 added note landed straddling the table's right frame line (fixed by T11b move). Exclude FRAME only for edges placed ON a border line. | Mansfield 2026-11-09 | open -> scripter |
| A36 | `c:` commands that only `princ` give an EMPTY execute_lisp payload; writers then infer results by re-querying (T10d, T11b, T12b). Every c:<task>-RUN/-CHECK must also return its result string as the last form (or write <task>.result.txt) so the writer can read the RUN line. | Mansfield 2026-11-09 | open -> scripter, acad-writer |
| A37 | After a writer's publish (swarm:publish run directly) the bridge dispatcher dropped: AutoCAD up, no dialog, `system health` lisp_alive=false, `init` hung >120 s ("mcp_dispatch.lsp not loaded"). Only fix: Jeremy types `(load "C:/Users/Jeremy/autocad-mcp/lisp-code/mcp_dispatch.lsp")`. Run `system health` after every publish step; if dead, stop and ask. Also: swarm:publish refuses to overwrite an existing PDF; writers ran -PUBLISH inline (restore BACKGROUNDPLOT/FILEDIA/CMDDIA) - give each republish a new file name instead. | Mansfield 2026-11-09 | open -> acad-writer, orchestrator |
| A38 | Title-block swaps expose hidden text problems: P3.04 title MText read "...GAS PIPING PLAN PLAN" (doubled word, invisible under TBMP's layout). A swap task's CHECK must compare the plotted title against the P1.01 sheet index wording, not just "words kept". | Mansfield M18-R3-T12, 2026-11-09 | open -> scripter (swap-title-block-xref library entry) |
| A39 | Before changing a model-space table/note, check it is inside a layout viewport: P1.03 had two older gas tables at model x 13768 and 18800, outside viewport 61784 (x 14707-17691), so they never plot (clouds there would be pointless). Scripters should report "off-sheet" for any target outside every viewport. | Mansfield 2026-11-09 | open -> scripter, revision-planner |
| A40 | Process gaps seen this run: (1) scripters invented task ids that fail the pattern (M18-COS1-P101) - validate task_id against `^[A-Z][A-Z0-9]{1,5}-[A-Z0-9]+-T[0-9]+[a-z0-9]*$` in validate.py, not by eye; (2) scripters/writers cannot run acad_api_check.py, lsp_collide.py or acad_dsd.py (bash guard) and have no Edit tool (one missing paren = whole-file rewrite; writers could not append to M18-SAVE.ops.json) - allow these three tools in the guard for scripter/acad-writer and give scripters Edit; (3) the writer would not run a quick fix without task.json even when told - correct, always write task.json + expected.json for every fix; (4) handle labels in write.json can be wrong (M18-R2-T1 cloud was 64C43, not 64C40): read created.txt record kinds (C copy, L cloud, T tag). | Mansfield 2026-11-09 | open -> validate.py, writer_guard, gen_agents (scripter tools) |
| A41 | Issue sets: open sheets keep the OLD title-block xref in memory after the xref file is saved. Before publishing a set, reload the TB xref in every sheet (Block.Reload), then publish; read each page's SHEET NO. and revision row from the PDF. Convention (Delta 3 #18): issue the revised sheets only, one multi-sheet PDF + narrative PDF in F20 - Transout\External\#nn_<desc>_<MM-DD-YYYY>. | Mansfield M18-ISS-T1, 2026-11-09 | open -> orchestrator, acad-writer (publish-set task) |
| A42 | Case collisions also happen between LOCALS in one defun: CHECK of M18-R3-T15 declared both `ne` and `nE` (one symbol), so the note-height check compared the note with itself. lsp_collide.py only checks defun-vs-setq; extend it (or lsp_lint) to flag case-duplicate names in a parameter/local list and setq targets inside one defun. | Mansfield 2026-11-09 | open -> lsp_collide.py / lsp_lint.py, scripter |
| A43 | entget list passed where an ename is expected: M18-R3-T15 called (m15:noteh nE ...) with nE = (swl:ent h) data; vlax-ename->vla-object failed "lentityp ((-1 . <Entity...". swl:ent returns entget DATA. Naming rule: ed*/data for entget lists, e*/en* for enames; pass (cdr (assoc -1 ed)) explicitly. | Mansfield 2026-11-09 | open -> scripter, swarm_lib doc |
| A44 | State-dependent preflights: M18-COS-T4 checked TEXT 2512 against 1673 - OFF where OFF was the PLANNED move (d in the edit state), so a fresh PLAN always aborted. Scripts with edit/done states name values by meaning (planned-move vs current-offset) and the dry run must exercise the edit state on a post-prior-task copy (a pre-task backup only reaches the first abort). | Mansfield 2026-11-09 | open -> scripter, lsp_dryrun (copy of live DWG) |
| A45 | AutoCAD-published PDFs can carry /Rotate 270: pymupdf get_text word boxes are in UNROTATED coordinates while get_pixmap(clip=...) uses page.rect (rotated). Multiply text boxes by page.rotation_matrix before cropping (a blank crop is the symptom). | Mansfield P3.04 meters, 2026-11-09 | open -> tools/visual_crop.py, orchestrator |
| A46 | visual-verifier cannot produce AutoCAD PDFs (bridge plot_pdf fails; its guard is read-only), so it returned "unavailable" for P6.11. AutoCAD exports go through an acad-writer publish-only task (task.json + expected.json required), then visual-verifier crops the PDF. | Mansfield M18-M2-T1, 2026-11-09 | open -> visual-verifier brief, orchestrator |
| A47 | File renames on L: (M2): acad-ops cannot close documents; check first whether the DWG is open at all (the storm file was not; its .dwl/.dwl2 were stale from the 13:36 crash) and which folder it is in (PL - Plumbing, not W - Sanitary); Rename-Item -LiteralPath worked (Egnyte move rights OK); then open + publish to confirm the sheet number field. | Mansfield 2026-11-09 | open -> orchestrator, acad-ops brief |
| A48 | Shared JSON logs: agents have Write but not Edit, so they cannot append to manifests/M18-SAVE.ops.json (one agent rewrote it whole). Standard: every agent writes its own <log>.attemptN.json (one-element list) and the orchestrator merges (tools/merge_ops.py). | Mansfield 2026-11-09 | open -> tools, acad-ops/acad-writer briefs |
| A49 | When drawing geometry contradicts a note (XGAS_GF: 10 meter symbols in ONE row vs "20 GAS METERS IN 2 ROWS"), the scripter correctly stopped and reported options instead of inventing 10 more. Codify: geometry/note mismatch -> report counts + options, no script. | Mansfield M18-R3-T16, 2026-11-09 | open -> scripter (good behaviour) |
| A50 | A layout with no floating viewport (only the paper-space pseudo viewport, DXF 69 = 1) never shows model space: P8.01's model-space duty point was a stale, unplotted duplicate. Scouts report "floating_viewports=0" and treat model-space text there as off-sheet (A39). | Mansfield M18-R6-T2, 2026-11-09 | open -> acad-scout, scripter |

## X. Excel OLE calcs

| id | lesson | source | status |
|---|---|---|---|
| X1 | A new row in an embedded table takes Excel's default format. Run `xl_ole.ps1 copyfmt` from the row above. | Northgate QA_1527_schedules:18, STATUS:52 | → excel-writer |
| X2 | Workbooks can hold stale, unplotted tabs and duplicate embeds, and calc cells can be typed values. Confirm what plots before reporting a mismatch. | Northgate STATUS:32-33, LETTER_CHECK:24 | → excel-writer, response-writer |
| X3 | `close discard` does not undo embedded edits. Close Excel when done. | memory close-excel-when-done; Encino | → excel-writer (already) |
| X4 | An OLE2FRAME shows a FIXED window of the workbook: rows written past it are clipped and the frame does not grow (22-001 sheet index, +22 rows). Shrink row height/font (new `xl_ole.ps1 rowfmt`; excel_bash_guard.py must allow it - Jeremy's call, the classifier blocked Claude editing the hook) or resize, then test-plot. Jeremy re-pasted the table by hand this time. | 22-001 testplot test1/test2 | → excel-writer, scripter; guard OPEN |
| X5 | Formula edits in an embedded workbook only through `xl_ole.ps1 set` with `formula_approval.by` (Jeremy's approval for that batch) and `from_formula` matching the live formula (commit a3203ca). Pick the OLE frame whose PLOTTED content shows the target: an OLE frame shows one sheet of a multi-sheet workbook (22-022 P001: DDCE0 showed the FU calc, hidden pressure sheet took the edits; D7966 printed). Typed values in calc cells are reported, not edited. | 22-022 C22-R1-T1, retro P10/P18 | → excel-writer, revision-planner |

## Q. QAQC review and markups

| id | lesson | source | status |
|---|---|---|---|
| Q1 | Report set-wide issues once, not per sheet. | mem 4; Garden ADDENDUM:8 | → sheet-reviewer (already) |
| Q2 | Pattern that works: one shared brief, per-agent sheet assignments, per-sheet JSON, a separate consolidate script. | mem 3 | → QAQC prompt |
| Q3 | Fixes written without opening the model are labelled "unverified against the model". | mem 5 | → QAQC prompt step 5 |
| Q4 | Revit PDFs can have a non-zero MediaBox origin, and PyMuPDF `callout=` then puts leaders off-page. Draw leaders as Line annotations. | mem 6 | → markup_build |
| Q5 | Check every annotation's /Rect, /L, /CL against the MediaBox (`chk_geom.py`), clip clouds, and render a preview with leaders. | mem 7 | → QAQC prompt step 7 |
| Q6 | Text-search anchoring placed only 70%. Locate the rest with the grid tool (markup-locator). | mem 8 | → markup-locator |
| Q7 | Bluebeam MCP needs Revu running before the session starts. | mem 9 | → plan-check prompt step 0 |
| Q8 | Project folders on L: are read-only; work on copies. | mem 10 | → agents (sheet tool) |
| Q9 | Reviewers' root causes were wrong several times ("view filter" was Detail Level Coarse, 0 DFU was a system-type setting). Reviewers give causes only as hypotheses; the planner finds the real one. | Garden NL:51-62,115,191 | → sheet-reviewer, revision-planner |
| Q10 | Fixture tags and DFU come only from enlarged sheets. Overall-plan tag overlaps don't matter. | Jeremy 2026-10-21 (memory tags-from-enlarged-sheets) | → sheet-reviewer, qaqc-checker, revision-planner |
| Q11 | Missing sheet numbers in a partial set are not errors; only dangling references are. | Retreat REVIEW_BRIEF:15 | → sheet-reviewer, qaqc-checker |
| Q12 | Standing drafting checks: overlaps, clipped text, placeholders, template leftovers (another project's logos), and title-block consistency. | Retreat P0.01-04; Bakersfield RP:19,54 | → sheet-reviewer |
| Q13 | Code tables were mis-cited by the reviewer, the checker and the brief alike. Every sizing number names its table and basis. | Retreat P2.72-02, P2.52-02 | → sheet-reviewer, qaqc-checker |
| Q14 | "Opposite convention" claims were based only on where leaderless tags sat. Don't make that claim from tag position alone. | Retreat P2.42-01 | → sheet-reviewer |
| Q15 | `rect_quality: exact` only after `check`. On Retreat 0 of 82 rects were exact, so all went to the locator. | Retreat change_list.json | → sheet-reviewer |
| Q16 | The locator gave different items identical rects, so clouds stacked. Rects must be distinct, or marked `same_as`. | Retreat locate/*.loc.json | → markup-locator |
| Q17 | Newly script-placed tags produced top QAQC findings (3 stacked labels). Reviewers check new annotation for overlap and readability. | Garden NL:182 | → sheet-reviewer |
| Q18 | qaqc-checker output folder: the agent says `reviews/`, Retreat used `checks/`. Pick one in the prompt. | Retreat | fixed: `checks/` everywhere (agent, prompt, markup_build reads both) |
| Q19 | Check plots: review clouds and deltas (touching, cutting text, delta on another cloud), blank section heads, and upside-down keynotes on every sheet. | pad11 plots/inspect2-3 | → sheet-reviewer |
| Q21 | After a hand edit, delta clouds can sit off the changed values (22-001 P0.03: clouds one row up; -13.9/79.9/36.6 unclouded). Re-check clouds against the values that actually changed (PC8 vs current). | 22-001 P0.03 r3 | → sheet-reviewer, plan-check prompt |
| Q22 | A verifier/script clash check on TEXT bboxes passed clouds whose ARCS cut the lines above and below (22-001 P0.01 notes 25/28, row pitch ~0.15 in; padding 0.03 + sagitta 0.036). set-assembler caught it on the render. Single-line clouds in dense notes need small arcs (chord ~0.07) and a render check. | 22-001 smoke set-assembler | → scripter, verifier (open) |
| Q23 | Render-only text blocks (image-type notes/tables) can hold wrong code editions: P0.07 TMV note 1 cited the 2025 CPC; text-layer QAQC missed it, letter-auditor and xsheet-checker found it on the render. Code-basis sweeps must include rendered review of note blocks. | 22-001 smoke letter-auditor | → sheet-reviewer, code-reviewer (open) |
| Q24 | Agents' one-line summaries can miscount (presubmittal said 5 pass, JSON had 6): report counts from the JSON. File names given to agents must be the true sheet id (a P0.01 page saved as P2.01-S.pdf triggered a false 'wrong page' alarm). | 22-001 smoke | → prompts |
| Q20 | The PDF text layer can miss some fonts (Pad 10 GI values printed but did not extract). Confirm "missing text" on the rendered tile. | pad11 STATUS row 20 | → sheet-reviewer |

## P. Plan check, DFU cascade, letters

| id | lesson | source | status |
|---|---|---|---|
| P1 | Trace a DFU cascade from a fresh dump taken after the edits, never an earlier cascade JSON. | Northgate STATUS:58, cascade_v2/head.md:7 | → revision-planner |
| P2 | Cascade method: OCG pipe tree, fixtures as sources, label value read at the leader tip, labels matched by location, fixture-sum vs label cross-check. One mis-summed label carries its error downstream. | Northgate cascade_v2/head.md, GW_AUDIT:55 | → revision-planner |
| P3 | Re-run the cascade on the planned post-state. It caught two errors that cancelled out in the totals. | Northgate BATCH2_PLAN:17 | → revision-planner |
| P4 | Check that every main has a total label. The GI inlet value appeared nowhere. | Northgate QA_1527_plans:21 | → revision-planner |
| P5 | Letter check: compare every cited sheet range with the sheets that exist. Back up a letter before editing it, and edit only with authorization. | Northgate LETTER_CHECK:34, STATUS:36 | → response-writer |
| P6 | Fold edits to the same note into one task so it isn't clouded twice. | Bakersfield RP:103 | → revision-planner |
| P7 | Re-check every value the letter quotes after any drawing change: Jeremy changed the RFBP loss after the letter was written and five quoted values went stale. | 22-001 letter | → response-writer, plan-check prompt |
| P8 | Sheet index check: compare the visible index row by row with the set's PDF bookmarks/layout names, with counts, using the title-block numbering format. | 22-001 qaqc_final | → sheet-reviewer |

## W. Windows environment (orchestrator's own scripts)

| id | lesson | source | status |
|---|---|---|---|
| W1 | `python -I` hides user site-packages (PyMuPDF, openpyxl). Run your own scripts without `-I`. | mem 11 | reference |
| W2 | PowerShell 5.1 `Set-Content -Encoding utf8` writes a BOM. Write JSON from Python or Bash, then `json.load` it to check. | mem 11a | reference (+ plan-check prompt for project.json) |
| W3 | cp1252 console: set `PYTHONIOENCODING=utf-8`, or write output to a file. | mem 12 | reference |
| W4 | Don't patch code with sed regexes containing backrefs or quotes. Use Edit or a Python rewrite, and match on content, not line numbers. | mem 13 | reference |
| W5 | Run long renders and markup jobs in the background. | mem 14 | reference |
| W6 | Windows paths with backslashes break in bash heredocs and in Python re.sub replacement strings (\U, \P). Write Python to a file with the Write tool and pass replacements as a lambda. | 22-001 pc9-check | reference |

## J. Jeremy's standing preferences

| id | preference | source | status |
|---|---|---|---|
| J1 | All title block, seal and stamp work is Jeremy's. | Garden NL:59 | → revision-planner |
| J2 | Other disciplines' CAD and xrefs are reference only; plumbing is adjusted to match them. Title-block notes go in paper-space MTEXT on each layout, never in the architect's xref. | Garden NL:167; Northgate STATUS:19 | → revision-planner |
| J3 | Arch link annotations stay off on plumbing views. | Garden NL:75 | → brief template |
| J4 | Stack numbering: unique stacks get unique numbers; duplicated stacks share a number. | Garden NL:138 | → brief template |
| J5 | Items marked NEEDS JEREMY: do the prep, report, don't guess. He reverted counts that were applied pending his OK. | Northgate HANDOFF:24, STATUS:34 | → planner (open questions) |
| J6 | Reroutes and design changes: proposal, then one pilot, then his look, then batch. He may waive the proposal step for small items. | Northgate STATUS:37, CASCADE_DECISIONS:15 | → revision-planner |
| J7 | Delete a duplicate label rather than retexting it. Re-read areas he hand-edited before any batch. | Northgate CASCADE_DECISIONS:5,16 | → revision-planner |
| J8 | Close Excel windows that automation opened. | memory close-excel-when-done | → excel-writer |
| J9 | Claude syncs Revit itself around write batches (SYNC A / SYNC B), never after a FAIL. | memory revit-sync-authorized | → see R17 |

---

## 2026-11-02 fold-in pass

- Staged generator edits: `build/staged/gen_agents*.py`. Full diff of the generated agents:
  `build/staged/AGENTS_DIFF_2026-11-02.diff` (15 agents, +155 lines).
- It also restores the 10-21 acad-writer in-place-save rule in the generator (it was only in the `.md`).
- Tool changes: revit-scout and revision-planner gain `send_code_to_revit`, gated by the same
  `revit_readonly_code_guard.py` the verifier uses (transactionMode "none", no transactions/saves/file access).
- APPLIED 2026-11-02 with Jeremy's OK: staged generators copied into `build/`, `gen_all.py` run, all 15 agents match the
  tested output. Pre-change generators and agent files are backed up in `build/backup_2026-11-02/`.
- Second pass, same day: orchestrator items. New `tools/markup_build.py` (shared markup + change list builder),
  `tools/lspcheck.py`, `templates/REVIEW_BRIEF_TEMPLATE.md`; `QAQC_SWARM_PROMPT.md` rewritten,
  `MARKUP_SWARM_PROMPT.md` extended; qaqc-checker writes to `checks/`.
- R17/J9 sync path (Jeremy chose it 2026-11-02): orchestrator_guard.py now lets the main session run ONE exact
  sync template (relinquish all, comment only varies). Tested: 16 guard cases pass; subagents still cannot sync.
  Backup of the old guard: `~/.claude/hooks/orchestrator_guard.py.bak-2026-11-02`.

## 2026-11-03 fold-in pass

- Source: Bakersfield 26032 pad11-1102 (HANDOFF, STATUS, logs T8b-T21, plots/inspect*). Added R23-R27, Q19-Q20;
  applied R20 (FormattedText) and R22 (workset/visibility), which were marked "-> agent" but were missing from the generator.
- Edited `build/gen_agents.py` (scripter, verifier), `build/gen_agents_qaqc.py` (sheet-reviewer) and
  `~/.claude/skills/revit-conventions/SKILL.md` (not generated); ran `gen_all.py`. Changed agents: scripter (+30 lines),
  verifier (+6), sheet-reviewer (+6), revision-planner (+5/-2: R21 COMMON text, which the 11-02 17:21 run had not regenerated).
- Backup of generators, agents, skill and this file: `build/backup_2026-11-03/`.

## 2026-11-03 fold-in pass (b) — 22-001 Olympic PC9

- Source: 22-001 pc9-check (writer_log, P3FIX/P1IDXCLOUD runs, testplots, qaqc_final). Added A20-A23, X4, Q21, P7, P8,
  O22, W6; updated A6 (-PUBLISH works). Earlier the same day: A18 (acad-ops), A19 (permissions).
- Generators: `build/gen_agents.py` (COMMON append-only logs; scripter publish/AutoLISP/vertex-bbox/OLE window;
  acad-writer permission refusals; acad-ops project.json + Revu locks; verifier vertex bbox), `gen_agents_p67.py`
  (response-writer re-check values; excel-writer OLE fixed window + rowfmt), `gen_agents_qaqc.py` (sheet-reviewer
  invisible text, sheet index check, clouds after hand edits). `MARKUP_SWARM_PROMPT.md`: acad-ops in the agent list,
  replot/splice procedure, permissions, re-check after any drawing change. Ran `gen_all.py`: 12 agents changed.
- OPEN (Jeremy): add `rowfmt` to the mode list in ~/.claude/hooks/excel_bash_guard.py (line 12) so excel-writer can
  use the new xl_ole.ps1 mode; the classifier blocks Claude from editing hooks.
- Backup of generators, prompts, this file and all agents before the pass: `build/backup_2026-11-03c/`.
- New agent (Jeremy 2026-11-03): code-reviewer (opus) in `build/gen_agents_qaqc.py` - plumbing code compliance per
  the brief's code basis; writes code_reviews/<SHEET>.json (ids <SHEET>-C01..) with section, requirement, observed
  arithmetic and citation_basis reference|brief|recalled (recalled never "high"). Reuses the sheet-reviewer bash
  profile (no hook edit). qaqc-checker reads code_reviews/; `tools/markup_build.py` merges code_reviews into each
  sheet's items (tested). QAQC_SWARM_PROMPT steps 3/5 and REVIEW_BRIEF_TEMPLATE "Code basis" updated; verified
  excerpts go in `swarm/codes/<edition>/` (README there).
- New agents (Jeremy 2026-11-03, "build 1, 3 and then all"): `build/gen_agents_review.py` (imported by gen_all.py):
  calc-auditor (opus), set-assembler (sonnet), xsheet-checker (opus), letter-auditor (opus), comment-tracker
  (sonnet, no Bash), spec-checker (sonnet), presubmittal-checker (sonnet, doc-scout profile = ls). All reuse existing
  bash_allowlist_guard profiles (no hook edits). New orchestrator tools (tested on 22-001 files): `tools/calc_check.py`
  (safe-eval arithmetic; caught a planted error, passed the real P0.03 chain), `tools/set_assemble.py`
  map|diff|index|splice (diff found exactly the 2 cloud changes P0.03 r3->r4; splice refuses overwrite),
  `tools/docx_text.py`. markup_build merges code_/calc_/xsheet_/spec_reviews (tested, 5 items/2 sheets).
  `swarm/checklists/LADBS.md` (18 items from the 22-001 PC9 run). Both prompts updated. Smoke test of the agents
  themselves still to do (they load on the next turn).
- Smoke test 2026-11-03 (22-001 P0.03, calc-auditor + calc_check.py): agent transcribed 69 checks from the rendered
  embedded calc (FU table + pressure chains); calc_check 69/69 match; every letter/schedule/riser value traced and
  matched (BP-1 400 gpm/69.3 ft, 79.9/45.1/4.7, 36.6/3.9, 2,648.5 WSFU, P-5.00 6" CW). 5 findings (1 should-fix:
  HW chain 75 ft loss + 15 ft gain - correct only if 75 ft is the heater elevation; open question for Jeremy). Took
  ~3.4 min, ~65k tokens. Gap: it was not given the P0.10 cut sheet PDF, so meter/backflow losses were not checked -
  give calc-auditor the cut-sheet sheets (or pair it with spec-checker).
- Smoke tests 2026-11-03 (22-001): all 8 new agents ran on the real PC9 set and produced valid, useful output (summary: 22-001.../pc9-check/smoke/SMOKE_RESULTS.md). ~0.7M tokens total. Found 2 real must-fixes the earlier manual QAQC missed (P0.07 2025 CPC render-only note; P0.01 clouds cutting text). Lessons Q22-Q24 added (generator updates for Q22/Q23 still open).
- 2026-11-03 (22-001, after smoke tests): fixed P0.07 TMV note 1 (2025 CPC -> 2019 CALIFORNIA PLUMBING CODE (2020
  LAPC); cell A8 of OLE 1026936 via xl_ole set) + delta-9 cloud/tag; redrew P0.01 note 25/28 clouds with small arcs
  (chord 0.07, |bulge| 0.40) and moved their tags into the gutter (Jeremy's choice) - render-checked before verify.
  Q22 confirmed in practice: always test-plot and look before verifying clouds. A Bluebeam re-save of the set broke a
  bookmark (P4.11-S -> page 1); set_assemble.py now maps first-bookmark-wins and `splice --repair-toc` fixes a single
  duplicate/missing pair. manifests/project.json was shared with a concurrent 24-024 Revit session: added "autocad"
  additively instead of overwriting (O22 follow-up: per-session project files would avoid this).


## 2026-11-04 scripter escalation (Jeremy)
- Added R28. `build/gen_agents.py` scripter: new "Fixing a failed script" section. `MARKUP_SWARM_PROMPT.md`: step 6 model
  corrected to sonnet (was stale "opus"; R21), step 5 escalation rule. Backup before the edit: `build/backup_2026-11-04/`.
- Added O23 + `tools/dialog_check.ps1`; MARKUP_SWARM_PROMPT step 5 DIALOG CHECK (hang, sync over 2 min, TIMEOUT).
- Swarm upgrade (swarm-upgrade-handoff.md): O24, R28 revised; schemas/, validate.py, tools.json, tools/snapshot_backup.py, visual_crop.py, file_deliverable.py; build/gen_agents_upgrade.py (7 new agents); hardening in gen_agents.py, gen_agents_p67.py (planner task schema, response-writer), gen_agents_review.py (comment-tracker ledger); skills revit-conventions (+3 rules) and acad-conventions (new). Hook profiles staged: HOOK_PATCH_2026-11-04.md. Backup: build/backup_2026-11-04b/.
- Phase 6 live test (run 20261104-1837, PLMB-24220-R25 + P003): hook patch applied by Jeremy (old hook: ~/.claude/hooks/bash_allowlist_guard.py.bak-2026-11-04); snapshot OK (Revit model is on Autodesk Docs = backup "cloud", needs manual save-as; P003 copied); acad index complete (10 KB); revit index partial (24 KB: 220/102 KB dumps exceeded the bridge's ~3 KB return limit) -> scouts now slice dumps and index big MEP categories as counts only. snapshot-taker put sheets at top level -> prompt now pins the shape.
- Sheet export for visual check (2026-11-04): Revit batch_export PDF works (A0 page; the sheet is CENTRED AT 1:1, not scaled); exported file is named after the sheet title, and "&" cannot pass the Bash guard -> export per task folder, visual_crop.py takes the folder (+ --sheet W,H / --units frac). AutoCAD: orchestrator cannot call app tools (orchestrator_guard) and the read-only guard has no plot -> acad-writer publish-sheet task with tools/acad_dsd.py + tools/swarm_publish.lsp (library/). DSD paths must keep L:\ (Path.resolve() gave \EgnyteDrive\..., use abspath). Test on P003 P0.03 failed: PlotandPublishLog job 10 'Error(s) Did Not Plot' and the bridge timed out ~15 s; the same layout plotted twice on 11-03 before ~6 h of unsaved edits (DBMOD 29). Cause not found; no re-publish without Jeremy.
- Publish retry (Jeremy watching, 2026-11-04 19:02): job 11 Plotted in 13 s, PDF 42x30 at 1:1, crop legible; the bridge still timed out (publish outlasts one call). Job 10's failure was not reproduced. Rule: publish-sheet TIMEOUT is normal; confirm via PlotandPublishLog.CSV + PDF (MARKUP_SWARM_PROMPT Run protocol 5).

## 2026-11-05 AU CS2138 skills pass (Jeremy)
- Reviewed AU 2026 CS2138 (slides + 8-page handout) and a deep-research report (25/25 claims verified). Plan: ~/.claude/plans/imperative-bubbling-jellyfish.md.
- Added R29, O25. New skills revit-api, plumbing-code; contracts in revit-conventions/acad-conventions; lessons moved into skill references. Tools: tools/check_task.py; schema write.status timeout. Generators: gen_agents.py, gen_agents_upgrade.py, gen_agents_qaqc.py (7 agents changed). Backup: build/backup_2026-11-05/.

## 2026-11-06 22-022 retro pass (Jeremy)
- Retro items 1, 2, 4-8, 10-13 applied (3 already done, 9 = memory). Added A24-A26, O26-O28. Bridge: autocad-mcp branch swarm-fixes. Hooks: guardlib.py, lsp_lint_hook.py, tests/test_guards.py, scripter profile. Tools: lsp_lint.py, lsp_dryrun.py, library/acad/swarm_lib.lsp + test. Backup: build/backup_2026-11-06/.

## 2026-11-06 autocad-api skill (Jeremy asked: "an AutoCAD API skill similar" to revit-api)
- New skill ~/.claude/skills/autocad-api (mirrored to claude/skills/ by build/sync_claude.py): ActiveX from the AutoCAD 2027 type libraries (acax26enu.tlb + axdb26enu.tlb: 157 interfaces, 146 enums, vla- call forms, READ-ONLY flags), 491 AutoLISP/vl/vlax functions (Autodesk AutoLispExt help data, 2021), DXF group codes (2027 DXF Reference), .NET via dll2llm (1963 types), core.md (110 names our .lsp files call, with notes), checker scripts/acad_api_check.py (--lsp scans a whole script). Plan: ~/.claude/plans/go-partitioned-eich.md.
- Checker over all 137 swarm .lsp files (northgate junction excluded; helper libs via --with: swarm_lib, C22-R7-T1, Northgate ng-/b2- libs, 22-001 pc9fix): 7310 found, 1 real miss: `stringp` (no such AutoLISP function) in scripts/C22-TB-T1.v1.lsp:361 (superseded; current C22-TB-T1.lsp is clean). 2021 help names absent from the 2027 type library (62, e.g. *32 members, FileDependency) are reported DOC-ONLY.
- Generators: gen_agents.py (scripter loads autocad-api, reports api_checked + acad_members), gen_agents_upgrade.py (script-debugger .lsp branch); MARKUP_SWARM_PROMPT step 10 runs the checker before any AutoCAD writer. Hook allowlist change is pending Jeremy: HOOK_PATCH_2026-11-06_autocad-api.md. Backup: build/backup_2026-11-06b/.

## 2026-11-09 22-022 retro roll-in (Jeremy: "roll in the changes to agents")
- Retro 20261106-1325 P1-P19 plus the 11-07 Febco/P012 follow-up rolled in: acad-conventions references/scripting-lessons.md
  "22-022 Carmel lessons" section (live values, name/built-in clashes incl. the `abs` clobber, stage tags, search caps,
  literal swl:queue-command attaches, vla-Delete, text/markup that does not plot, delta layer, MTEXT size, Defpoints in clash
  scans, check plots in paper space, OLE frame choice, PDF row measuring, A&O-border PDF crops). Agent briefs: scripter,
  acad-writer, acad-scout, verifier (gen_agents.py); script-debugger (lsp_lint, live values, stage tags), visual-verifier
  (text must be visible, paper-space check) (gen_agents_upgrade.py); revision-planner (delta layers, OLE frame, other-discipline
  make/model + USC orientation), excel-writer (frame check, approved formula edits) (gen_agents_p67.py). LESSONS A3/A18 (bridge
  cannot open DWGs in LT), O17 (lsp_lint), X5 (formula approval / OLE frame). Not done (tooling, Jeremy's call): lsp_lint checks
  for setq/defun and built-in clashes and `(or (cdr ...` values, swl: helpers (delete-verified, copy-text, mtext-box, plot1),
  test_guards cases for swl:queue-command, the open root cause of entities that do not plot.

## 2026-11-09 Mansfield retro roll-in (Jeremy: D1-D4 yes, same branch -> main, make paths portable)
- Plan: retro/20261109-1100-agent-plan.md (A32-A50). Schemas: task_id pattern on expected files, sibling task check, write
  result_source. Tools: lsp_lint W5-W7, lsp_collide local collisions, lsp_dryrun --from-live + fail= detection, merge_ops.py,
  visual_crop --find (rotation-safe), acad_dsd --sheets/--sheets-file; tools/tests (27 pass). swarm_lib: swl:result-out,
  swl:mtext-bottom/-box, swl:ebox, swl:group-box, swl:clash-ex, swl:inside-vp-tol, swl:floating-vps, swl:vp-msbox,
  swl:on-sheet-p, swl:swarm-dir; swl:clash line filter fixed (missed lines crossing a box with no endpoint inside);
  library test 71 checks pass on 3 DWG copies. Hooks: paths.py (runtime paths), scripter/script-debugger Edit limited to
  scripts/*.lsp + own manifests (D1), acad-ops guarded close form (D2), acad-writer/acad-ops Bash profiles; 92/92 guard
  tests. Agents: scripter, script-debugger, acad-writer, acad-ops, acad-scout, verifier, visual-verifier (D4), retro.
  MARKUP_SWARM_PROMPT run protocol 17 (merge_ops, health after publish, issue protocol, open/close/rename, crops,
  --from-live). acad-conventions scripting-lessons "Mansfield 2026-11-09".
- Portability: swarm_paths.py (SWARM_HOME / CLAUDE_CONFIG_DIR / AUTOCAD_MCP_HOME, defaults from repo location + user
  home); hooks compute paths at runtime; .claude/settings.json uses $HOME; agent briefs rendered by gen_all.py; markdown
  uses ~/swarm, ~/.claude, ~/autocad-mcp. L:/Shared stays absolute. Run records (project folders, scripts/*.lsp, *.cs,
  HOOK_PATCH_*, LESSONS history) keep their original paths. Not portable: the claude.ai-synced plan-check-response-letter
  skill folder id (account-specific). Not done: live AutoCAD smoke test of the new briefs (next run is the test).
