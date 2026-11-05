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
| O17 | A LISP paren check exists (`northgate/scripts/lspcheck.py`). The scripter can't run it, so the orchestrator runs it before the acad-writer. Move it to `swarm/tools/`. | Northgate STATUS:47 | → `tools/lspcheck.py` + plan-check prompt step 5 |
| O18 | After editing a guard or hook regex, retest every mode it gates. A dropped space blocked every Excel helper mode. | Northgate STATUS:51 | → plan-check prompt |
| O20 | Swarm agents read Outlook, Teams and Egnyte (2026-11-02): email-scout gets all three, doc-scout gets Egnyte, read tools only (the connector itself also holds Mail.Send and Files.ReadWrite, so only the tool lists keep agents read-only). Egnyte files are fetched into inputs/ by the orchestrator after Jeremy confirms the match. Teams search returns bot cards ("Nexus" task register): not a source. | Jeremy 2026-11-02 | → email-scout, doc-scout, plan-check prompt step 1b |
| O21 | Each project gets `swarm/<Egnyte project folder name>` (spaces removed). Find it by job number under `/Shared/Projects/<year>/`; the architect's number is not the job number (Retreat). Old short folders (garden, northgate, retreat, archive/bakersfield, archive/encino) were renamed 2026-11-02 and left as junctions so existing paths keep working. To remove a junction use `cmd /c rmdir <old name>` (no /s), never PowerShell 5.1 `Remove-Item -Recurse`, which can follow the link and delete the real files. | Jeremy 2026-11-02 | → both prompts, brief template, project.json `project_folder`/`egnyte_folder` |
| O22 | Set manifests/project.json at project start: acad-ops refused to save because it still described Northgate. Agents append to shared logs, never overwrite (a writer wiped writer_log.md). | 22-001 pc9-check | → COMMON, acad-ops, plan-check prompt |
| O19 | Subagents are blocked from live `L:` writes. The orchestrator does those itself, with Jeremy's OK. | Northgate STATUS:60 | → both prompts |
| O23 | Hung Revit/AutoCAD calls are usually a modal dialog the bridge can't see. `tools/dialog_check.ps1` (read-only: never clicks or focuses) lists each app's dialogs with title, text and buttons (WM_GETTEXT, then UI Automation for TaskDialogs/WPF), flags a disabled main window or a not-responding app, and with -CaptureDir saves a PNG of the dialog window only (never the desktop: a full screenshot showed Outlook/Teams). Exit 0 clear / 1 blocked / 2 not running. Tested on live Revit 2025 + AutoCAD 2027 (clear) and on self-test dialogs (message box, modal WinForms), in pwsh 7 and PS 5.1. Choosing a button stays Jeremy's. | Jeremy 2026-11-04 | → MARKUP_SWARM_PROMPT step 5 (DIALOG CHECK) |
| O24 | Swarm upgrade (swarm-upgrade-handoff.md): run ids, snapshot-taker before any write (writers refuse without snapshots/<run-id>.snapshot.json), strict schemas (schemas/*.schema.json via validate.py; expected.json items are only {id, check, expected_value}; write.json is strict and details go to <task>.write.log.md), per-document index caches (cache/<app>-index.json), visual-verifier BEFORE verifier (else every task is PASS_API_ONLY), scope-guard + code-checker at the Step 4 gate and before the letter, script library (library/), retro proposals. Locks stay per app (locks/*.lock, 'released'), not ./swarm/LOCK: writers cannot delete files. New Bash profiles need HOOK_PATCH_2026-11-04.md. | Jeremy 2026-11-04 | → MARKUP_SWARM_PROMPT Run protocol, gen_agents*.py |

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

## A. AutoCAD bridge and LISP

| id | lesson | source | status |
|---|---|---|---|
| A1 | Keep execute_lisp payloads under ~600 chars (otherwise "malformed list on input"). Put logic in the .lsp. | Northgate HANDOFF:18 | → scripter, acad-scout |
| A2 | A timeout on a trivial call means AutoCAD is blocked: stop and report, never loop. Don't loop over all open documents in one call; save one document per call. | Northgate HANDOFF:19, logs/SAVEALL-1021f | → writers |
| A3 | Never open AutoCAD through computer-use (it starts a second empty instance). Use the bridge's `drawing open`. After a crash, run `system init`. With only the Start tab open, Jeremy has to open the first drawing by hand. | Northgate HANDOFF:17; mem 23 | → acad-writer |
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
| A18 | AutoCAD file-ops split (Jeremy 2026-11-03): scouts/writers/verifier can't open DWGs, so P007 stalled. New acad-ops agent (sonnet) = open DWGs listed in locks/acad_open_approved.txt (file or folder/), switch active drawing (one vla-activate), in-place save; no edits, save-as, close, quit, purge. acad-writer reports "needs acad-ops open". Also: MTEXT has no ActiveX Explode (vla-explode fails silently) - use (command "_.EXPLODE" e) on a temp copy and entdel only live enames. | 22-001 pc9-check | -> writer_guard.py (acad-ops), gen_agents.py |
| A19 | Permissions (Jeremy 2026-11-03): in auto mode the classifier denies subagent LISP that edits a DWG on L: or publishes there ("Modify Shared Resources"), even with an auto-mode soft-allow rule; one-time approvals of an exact call don't carry over to a subagent. drawing save works with a plain allow rule for mcp__autocad-mcp__drawing. Out of auto mode, subagent calls prompt Jeremy and run when approved. If a writer is denied: report, don't retry, ask Jeremy (approve prompt, or run the loaded c:COMMAND himself). | 22-001 pc9-check writer_log | → prompt, acad-writer |
| A20 | Clouds: locate, check and size by VERTEX bbox (group 10 min/max). vla-GetBoundingBox adds ~0.03-0.04 in of arc bulge (chord 0.14, bulge 0.52) and made the P3FIX position check abort. Keep arcs clear of text: sagitta = bulge x chord / 2. | 22-001 pc9-check (P3FIX) | → scripter, verifier |
| A21 | AutoLISP is not Common Lisp: no fboundp, no identity (cost two failed runs). Use (type sym) and (lambda (x) x). | 22-001 pc9fix/p001idxcloud | → scripter |
| A22 | Drawings can carry text that does not visibly plot: P0.01 had a stale model-space sheet index printing at ~0.05 pt next to the real one (an embedded Excel table with no text layer). A text-layer comparison produced a false "index wrong" finding. Check the rendered page before reporting. | 22-001 qaqc_final | → sheet-reviewer, prompt |
| A23 | Publish to a local test folder under the run first (no shared-drive permission block, QA before L:). An open drawing with unsaved edits publishes in its in-memory state with no save prompt. Splice into a new dated set; a PDF open in Revu blocks overwriting it. | 22-001 testplot | → scripter, plan-check prompt |

## X. Excel OLE calcs

| id | lesson | source | status |
|---|---|---|---|
| X1 | A new row in an embedded table takes Excel's default format. Run `xl_ole.ps1 copyfmt` from the row above. | Northgate QA_1527_schedules:18, STATUS:52 | → excel-writer |
| X2 | Workbooks can hold stale, unplotted tabs and duplicate embeds, and calc cells can be typed values. Confirm what plots before reporting a mismatch. | Northgate STATUS:32-33, LETTER_CHECK:24 | → excel-writer, response-writer |
| X3 | `close discard` does not undo embedded edits. Close Excel when done. | memory close-excel-when-done; Encino | → excel-writer (already) |
| X4 | An OLE2FRAME shows a FIXED window of the workbook: rows written past it are clipped and the frame does not grow (22-001 sheet index, +22 rows). Shrink row height/font (new `xl_ole.ps1 rowfmt`; excel_bash_guard.py must allow it - Jeremy's call, the classifier blocked Claude editing the hook) or resize, then test-plot. Jeremy re-pasted the table by hand this time. | 22-001 testplot test1/test2 | → excel-writer, scripter; guard OPEN |

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
