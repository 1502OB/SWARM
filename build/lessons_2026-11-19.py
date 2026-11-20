"""One-off: fold the 24-071 Village sheet-setup run (20261119-VSV) lessons into LESSONS.md (2026-11-19).

Sources: 24-071_Pacific_Edge_Laguna_Beach_PDB_Collaborative/STATUS.md "Run 20261119-VSV" + 11-19 lines, its
manifests/VSV-* (write logs, dryrun log, verify, visual), scripts/VSV-*.cs, retro/20261119-VSV.md (P1-P9; it could not
see the manifests).
- Extends O23 (sync-reminder dialog), O26 (task.json for every task, PREFIX-WORD-Tn ids), R52 (get_warnings cap),
  R65 (batch_export stuck / use a Document.Export script), R76 (view titles by label outline), R77 (dry run persisted
  again: probe after every dry run), R82 (absolute mapping vs cross-document difference), R86 (rolled in), Q27 (sheet
  ids / PDFs for the visual-verifier, '&').
- Adds O39-O42 and R87-R92.
"""
p = "LESSONS.md"
s = open(p, encoding="utf-8").read()  # universal newlines; written back as LF (git stores LF)

VSV = "24-071 VSV 2026-11-19"


def row_line(rid):
    i = s.index(f"\n| {rid} |") + 1
    return s[i:s.index("\n", i)]


def extend(rid, lesson_add, evidence_add=None, targets=None):
    """Append to a row's lesson cell (and evidence/targets). Rows are | id | lesson | evidence | targets |."""
    global s
    old = row_line(rid)
    body = old[len(f"| {rid} | "):-2]  # strip leading id cell and trailing ' |'
    lesson, evidence, tgt = body.rsplit(" | ", 2)
    lesson = lesson.rstrip() + " " + lesson_add
    if evidence_add:
        evidence = evidence + "; " + evidence_add
    if targets:
        tgt = targets
    new = f"| {rid} | {lesson} | {evidence} | {tgt} |"
    assert s.count(old) == 1, rid
    s = s.replace(old, new)


def add_after(rid, rows):
    global s
    old = row_line(rid)
    assert s.count(old + "\n") == 1, rid
    s = s.replace(old + "\n", old + "\n" + "\n".join(rows) + "\n")


# ---- O table ----
extend("O23", "Revit's own 'Changes Not Synchronized with Central' reminder (a timed sync reminder) can open during a "
       "long run and block the bridge (VSV-ENL-T1 S write TIMEOUT): it is safe to Cancel (ask Jeremy), then reconcile "
       "read-only before re-running. From Bash run it as `powershell.exe -NoProfile -ExecutionPolicy Bypass -File "
       "tools/dialog_check.ps1` (pwsh is blocked there).", VSV + " ENL-T1 S",
       "→ MARKUP_SWARM_PROMPT step 5 (DIALOG CHECK), run protocol 21")
extend("O26", "The schema pattern also admits <prefix>-<WORD>-T<n> (VSV-ENL-T1, VSV-PLOT-T1): name the group, keep the "
       "prefix. The orchestrator writes task.json for EVERY task, export-only and read-only ones included (writers "
       "refuse without it; scripters cannot write task.json, writer_guard), and briefs the scripter with the final id "
       "(retro 20261119-VSV P5).", VSV, None)
O = [
    "| O39 | ONE Revit job at a time, exports included. Two visual-verifiers exporting with batch_export at once clashed (stale replies, no PDFs); batch_export then kept returning one stale reply, and while a verifier queried in parallel Revit hit a serious error and closed without saving (the 14 enlarged sheets were lost). Rules: never run an export alongside any other Revit call; visual PDFs come from a read-only Document.Export script (scripts/VSV-ENL-PDF.cs: one PDF per sheet, named by sheet number, no '&') run by a revit-writer; visual-verifiers then work from the PDFs on disk only (several in parallel is fine); the verifier runs after the export, alone. | " + VSV + " ENL-T1 visual/verify, STATUS 11-19 | → orchestrator (run protocol 21), visual-verifier, verifier, revit-writer |",
    "| O40 | Hand edits Jeremy makes in Revit after the last sync are lost if Revit later closes without saving (orchestrator's account, not in STATUS.md: he ticked 4 hand fixes 7:47-7:54 pm and a later close without saving lost them). After Jeremy reports or ticks hand fixes, remind him to Synchronize (or run a revit-ops sync with his OK) before more bridge work. | " + VSV + " | → orchestrator (run protocol 21), run-tracker |",
    "| O41 | Orchestrator tooling on this machine: from Bash pwsh is blocked: run tools/dialog_check.ps1 with `powershell.exe -NoProfile -ExecutionPolicy Bypass -File`; the WindowsApps `python` alias is blocked: use the full interpreter path (~/AppData/Local/Python/pythoncore-3.14-64/python.exe); run ids may be YYYYMMDD-HHMM or YYYYMMDD-<WORD> (snapshot_backup.py refused '20261119-VSV' for the backup copy; FIXED 2026-11-19, it accepts both); publish artifacts from the real swarm-data path (project folders in swarm/ are junctions; publishing through the junction fires an approval). | " + VSV + " retro P9 | → MARKUP_SWARM_PROMPT run protocol 21, snapshot-taker, tools/snapshot_backup.py |",
    "| O42 | The live agents/hooks/skills can lag the repo: VSV ran on ~/.claude/agents generated 11-12 (no R77 read-only dry-run rule, no script-checker), and an 11-19 `sync_claude.py pull` copied the stale 11-09 ~/.claude hooks and revit-conventions skill over the repo's 11-17 versions (backup_claude_20261119-115751). After every roll-in commit run `gen_all.py` AND `sync_claude.py install`; before any `pull` run `status` and pull only when the ~/.claude file is the one you edited. | " + VSV + ", build/backup_claude_20261119-115751 | → README, orchestrator |",
]
add_after("O38", O)

# ---- R table ----
extend("R52", "get_warnings caps at 500 (24-071 VSV snapshot: 'first 200 of 500'): at the cap an equal before/after "
       "count proves nothing; compare warning ids/descriptions (or an uncapped count from a read-only "
       "Document.GetWarnings() script).", VSV + " snapshot, retro P7", None)
extend("R65", "2026-11-19: two exports at once clashed and batch_export then stuck on one stale reply before Revit "
       "crashed (O39): for visual PDFs use a read-only Document.Export script (scripts/VSV-ENL-PDF.cs pattern: "
       "PDFExportOptions per sheet, file named by sheet number) run alone.", VSV + " ENL-T1", None)
extend("R76", "View titles copied across documents (VSV-P1-T2, ENL-T1): after the viewport is aligned (R87), set "
       "LabelLineLength = source, copy LabelOffset, then correct it by the sheet-XY difference of the "
       "GetLabelOutline() lower-left corners (up to 3 passes; fatal > 0.05 in): a plain LabelOffset copy is wrong "
       "because it is relative to the box, whose size differs between documents.", VSV + " P1-T2 (verify PASS)",
       "→ scripter, visual-verifier, revit-conventions")
extend("R77", "RECURRED 2026-11-19: VSV-P1-T1 attempt 3 (a view/sheet script with a commit-then-rollback dry run) left "
       "TB types, sheet names, key plans, a deleted TB and viewport centres live, though it returned 'group rolled "
       "back'; the live agents predated this rule (O42). Later ENL dry runs rolled back. Writers: right after EVERY "
       "dry run, one read-only probe of the things the task creates/changes (counts of new sheets/views/ids, one "
       "changed name/type/centre per kind) and stop with ROLLBACK_FAILED if anything stayed (R86).",
       VSV + " P1-T1 dryrun log", None)
extend("R82", "This is for ABSOLUTE sheet positions (R82 holds). GetProjectionToSheetTransform expects "
       "projection-space points; on raw model points its absolute result was nonsense on WF. The DIFFERENCE of one "
       "model point mapped through two viewports whose views share the model frame (plan views, unrotated, same "
       "scale, link at identity) is valid and is the cross-document alignment method (R87).", VSV + " P1-T1/ENL-T1",
       None)
extend("R86", "Rolled in 2026-11-19 with R77 (writer post-dry-run probe).", None,
       "→ scripter, revit-writer, script-checker, revit-conventions")
R = [
    "| R87 | Placing a viewport like one in another document: the box centre is not a match (box size includes annotation crop and annotations outside the crop; S-P1.01 was 43 in off with annotation crop off). Align by a model point: P = source (arch) view crop centre (world XY); delta = srcVp.GetProjectionToSheetTransform().OfPoint(P) - tgtVp.GetProjectionToSheetTransform().OfPoint(P) in sheet XY; tgtVp.SetBoxCenter(current + delta, keep Z); read back <= 0.01 in (fatal > 0.05). Condition: both views plan views, unrotated, same scale, link transform identity (check it); see R82 for absolute positions. All cross-document point comparisons are XY only: GetBoxCenter Z differs between documents (P1-T1 attempts 1-2 rolled back on a 3D DistanceTo). Verified: 0.0005 in on 15 P1 + 14 enlarged sheets; plots matched the arch. | " + VSV + " P1-T1, ENL-T1 verify I8; retro P2/P4 | → scripter, script-checker, verifier, revit-conventions |",
    "| R88 | After a send_code timeout, bridge replies can lag one call: the orchestrator's sync returned the verifier's earlier callout-parent scan output. Every call carries a marker (task id + step) in its first return token; check each reply answers THAT call, and confirm state with a cheap read (Document.IsModified, an id count) rather than trusting the reply. | " + VSV + " SYNC C | → revit-writer, verifier, revit-scout, revit-ops, orchestrator |",
    "| R89 | Heavy model-wide scans (collect all views to find callout parents) time out and jam the send_code queue for every later call (ENL-T2 dry run and SYNC C timed out while get_project_info still answered). Verifier and scout queries are scoped to the task's element/sheet ids; a check with no cheap path is null 'not checked: heavy scan', never a model-wide collector. | " + VSV + " ENL-T1 verify #2 | → verifier, revit-scout, scripter (expected.json) |",
    "| R90 | When enlarged views vary scale, a copied view template must list VIEW_SCALE, VIEW_SCALE_PULLDOWN_IMPERIAL and VIEW_SCALE_PULLDOWN_METRIC as non-controlled (SetNonControlledTemplateParameterIds), and the script re-sets View.Scale AFTER assigning the template and reads it back (B-P6.21 restroom came out 1:48 instead of 1:24; dry-run readback caught it). | " + VSV + " ENL-T1 dryrun B | → scripter, revit-writer (readback), revit-conventions |",
    "| R91 | Viewport types (and their title family) missing in the target model: copy them from the source document with ElementTransformUtils.CopyElements in preflight, after a name pre-check (an existing name raises a duplicate-types dialog that blocks the bridge), before creating viewports; never keep a different type silently ('kept' in P1-T1). | " + VSV + " P1-T1, ENL-T1 ('2 Module' + 'RDC View Title'); retro P8 | → scripter, revit-conventions |",
    "| R92 | Scripts that read a source architectural model fall back to the loaded link document (RevitLinkInstance.GetLinkDocument, after checking the link transform is identity) when the detached arch document is not open. Added to VSV-ENL-T1.cs (attempt 6 ran from the link after Revit reopened without VS_detached). | " + VSV + " ENL-T1 attempt 6 | → scripter, revit-conventions |",
]
add_after("R86", R)

# ---- Q table ----
extend("Q27", "The visual-verifier has no sheet-number -> id lookup: the brief gives sheet ids (or the PDFs, O39). "
       "The Bash guard blocks '&' in unquoted file names: export folders and files are named by sheet number only.",
       VSV + " P1-T2/P2-T1/P2-T2 visual; retro P6", None)

s = s.rstrip("\n") + """

## 2026-11-19 roll-in: 24-071 Village sheet setup, run 20261119-VSV
- Evidence: 24-071 STATUS.md 'Run 20261119-VSV' + 11-19 lines, manifests/VSV-*, scripts/VSV-*.cs, retro/20261119-VSV.md.
- New: O39 (one Revit job at a time incl. exports), O40 (sync after hand fixes), O41 (tooling: dialog_check via
  powershell.exe, full python path, run-id grammar, publish from swarm-data), O42 (live agents/hooks lag the repo),
  R87 (cross-document viewport alignment by a model point, XY only), R88 (reply lag after a timeout), R89 (no heavy
  scans), R90 (template scale non-controlled + re-set), R91 (copy viewport types), R92 (link-document fallback).
- Extended: O23, O26, R52, R65, R76, R77 (recurred), R82 (absolute vs difference), R86 (rolled in), Q27.
- tools/snapshot_backup.py accepts YYYYMMDD-<WORD> run ids. Agents: scripter, revit-writer, revit-scout, verifier
  (gen_agents.py); visual-verifier, snapshot-taker, script-debugger (gen_agents_upgrade.py); script-checker
  (gen_agents_cscheck.py). revit-conventions SKILL.md + scripting-lessons; MARKUP_SWARM_PROMPT run protocol 21.
"""
open(p, "w", encoding="utf-8", newline="").write(s)
print("lessons ok")
