"""One-off: fold the 26021 Erewhon Costa Mesa Site & Shell PC1 run (20261117-ECM) lessons into LESSONS.md (2026-11-20).

Sources: 26021_rdc_erewhon costa mesa_costa mesa_ca_P2639/STATUS.md (run 20261117-ECM), its manifests/ECM-*,
scripts/ECM-*.cs, retro/20261117-ECM.md (orchestrator-written), and the orchestrator's run notes (11-17 to 11-19).
- Extends R37 (linked view id lives in the link document), R52 (500 = cap: count with GetWarnings().Count), R58
  (revit_api_check has no enum members), R65 + O39 (batch_export stale even on an idle bridge; never for checks or
  sets), R66 (copying an existing row's sequence id, incl. InvalidElementId, works), R67 (obstacle set), R75 (a
  fitting's own centerline), R86 (own-Transaction RollBack is the reliable dry run, R99).
- Adds R93-R99 and O43-O49.
- Not rolled in (project-specific): hiding TI in the arch link (26021 choice), the 26044 project number, stamp files.
Idempotent: does nothing when the 2026-11-20 section is already present.
"""
import sys

p = "LESSONS.md"
s = open(p, encoding="utf-8").read()  # universal newlines; written back as LF (git stores LF)
if "## 2026-11-20 roll-in" in s:
    print("lessons already rolled in")
    sys.exit(0)

ECM = "26021 ECM 2026-11-17"


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


for rid in ("O43", "R93"):
    assert f"\n| {rid} |" not in s, rid + " already exists"

# ---- O table ----
extend("O39", "2026-11-17 (26021 ECM): batch_export returned the PREVIOUS export path three times even with an idle "
       "bridge, and an export run while a writer was active returned a stale path. Never use the batch_export MCP for "
       "checks or sets: a Document.Export + PDFExportOptions script (scripts/ECM-D2-T1.cs), one sheet per call, "
       "Combine=true so FileName is honoured. Document.Export never overwrites (it appends -1): delete or move stale "
       "files first and check the exact name + size. One bridge client at a time: writer OR export OR scout; offline "
       "agents in parallel.", ECM + " visual exports (3 stale)",
       "→ orchestrator (run protocol 22), visual-verifier, verifier, revit-writer, scripter")
O = [
    "| O43 | Re-scout before scripting after any pause in which Jeremy may have edited the model (overnight, a question round, his hand fixes): 26021 GCO-T1 was scripted from an earlier scout while Jeremy had already placed the GCO himself. A scout older than the last pause is stale for every element the task touches. | " + ECM + " GCO-T1 | → orchestrator (run protocol 22), scripter |",
    "| O44 | List the target folder before creating one (a second submittal folder was created beside the existing one on Egnyte), and name files the way the City's checklist / correction list asks, over the neighbouring files' pattern. | " + ECM + " submittal folder | → orchestrator, filer |",
    "| O45 | Calcs read pipe sizes and inputs from the LIVE model (a scout or schedule export), never from a marked-up older set: the older set no longer matched the model. | " + ECM + " gas/roof/water calcs | → orchestrator, calc-auditor, revision-planner |",
    "| O46 | Run tracker: read `answers` AND `fixes` (ArtifactData list) at EVERY tracker update, not only before the final sync: Jeremy's answers sat unread until he asked. | " + ECM + " run tracker | → run-tracker skill, orchestrator |",
    "| O47 | An instruction Jeremy gives for one project stays project-scoped: record it in that project's STATUS.md / memory note, never as a cross-project memory or LESSONS rule (26021: Jeremy hides background/TI himself). | " + ECM + " A2 | → orchestrator |",
    "| O48 | Outlook drafts with a signature or images: never replace the body through the Graph/M365 connector (it drops the images). Edit in Outlook web through Chrome in place, then verify the saved server copy: OWA saves when you leave the compose view (Ctrl+S did not save); programmatic JS selection + typing did not register, use real double/triple-click selection. | " + ECM + " transmittal draft | → orchestrator, response-writer (drafts only) |",
    "| O49 | Letter builder (plan-check-response-letter) on Windows: run with PYTHONUTF8=1 (else an en dash becomes mojibake); no LibreOffice here, so tune --filler by exporting through Word COM and reading the signature y. Extends W3. | " + ECM + " PC response letter | → orchestrator, response-writer |",
]
add_after("O42", O)

# ---- R table ----
extend("R37", "Resolve RevitLinkGraphicsSettings.LinkedViewId in link.GetLinkDocument(), never the host document "
       "(26021 ECM-A2-T1 v1: host document.GetElement returned null and .Name threw a bare 'target of an invocation').",
       ECM + " A2-T1 v1", None)
extend("R52", "A get_warnings result of exactly 500 is the tool's cap: count with a read-only "
       "document.GetWarnings().Count script.", ECM, None)
extend("R58", "revit_api_check.py does not index enum members (BuiltInParameter.ELEM_PARTITION_PARAM, "
       "BuiltInCategory.OST_PipeCurves report NOT FOUND): grep the revit-api skill's references/db-b-c.md for "
       "' NAME = ' instead; a compile PASS (revit_cs_check.py) settles it.", ECM, None)
extend("R65", "2026-11-17: stale even on an idle bridge (O39); never use it for checks or sets.", ECM, None)
extend("R66", "2026-11-17 (26021 ECM-D1-T2): a new revision on a no-delta issue showed '1' in the delta column; "
       "COPYING RevisionNumberingSequenceId from an existing row (its value was InvalidElementId) worked and blanked "
       "the number. Copy from an existing row; never construct the id.", ECM + " D1-T1/T2",
       "→ scripter, revit-conventions")
extend("R67", "Obstacle set (26021 ECM-KN-T1): pipe centrelines buffered by OD/2 + 1/16 in paper, fixtures and "
       "accessories (cleanouts), and ALL view annotations; the first placement overlapped pipes and cleanouts.",
       ECM + " KN-T1", None)
extend("R75", "Deleting a FITTING cascades to its own centerline sub-element too (OST_PipeFittingCenterLine): "
       "benign (26021 ECM-GAS-T1 v5 gate rejected it).", ECM + " GAS-T1 v5", None)
extend("R86", "2026-11-17 (26021): the own-Transaction pattern (transactionMode none; dry run does everything, "
       "measures, then Transaction.RollBack() with its status checked) rolled back cleanly every time and is the only "
       "real test for layout/sheet changes (R99).", ECM, None)
R = [
    "| R93 | Sheet text placement: anchor to a NAMED ScheduleSheetInstance / element read from a scout, never a guessed table title (26021 'FIXTURE UNIT REFERENCE' lived inside one 'Sticky-P0.5 Design Criteria' schedule). Drawing-area edges come from the exported PDF's border line (measure it), not the title-block bbox plus a guess (the B01 note crossed the thick left border). Layout checks keep >= 0.02 ft margin to every limit. A heading needs a bold 1/8 in text type: create it if absent. | " + ECM + " B01-T1/T2 | → scripter, script-checker, visual-verifier, revit-conventions |",
    "| R94 | Element matching: cleanouts = family/type name contains 'CO' or 'CLEANOUT', case-insensitive (storm COs are 'FloorCleanout_Round1 \\| TWO-WAY'). 'West/east stub' and similar positional words map by scout XY, never by network numbering (both note-12s landed east: the west stubs were nets 1/4, not 2/3). | " + ECM + " KN-T1 | → scripter, revit-scout |",
    "| R95 | Pipe resize: an explicit element list from the scout (no connector walk through FamilyInstances: v1 escaped through gas meters/tenant boxes into other networks); read Connector.Radius only when Shape==Round && Domain==Piping; generic fittings do not follow the pipe: set their writable 'Nominal Radius' (Double, any spec); transitions are recreated; compare connector snapshots as unordered sets. 6 attempts; library candidate scripts/ECM-GAS-T1.v6.cs. | " + ECM + " GAS-T1 v1-v6 | → scripter, script-debugger, script-checker, revit-conventions |",
    "| R96 | Sticky (text) schedules: the text lives in HEADER section cells (get_schedule_data reads the body: blank); edit with TableSectionData on SectionType.Header (SetCellText). Before splitting text across cells check which cells actually print (P000 r5c1 printed, a twin schedule's r4c1 did not). A range delete removes the whole table body (titles AND their rows); verify on the export. | " + ECM + " SCH-T0, NOTES-T2 v3, RB1-T1 | → scripter, revit-scout, verifier, revit-conventions |",
    "| R97 | Schedule width is driven by the BODY columns: when trimming header columns, scale the body column widths to the kept header width. Revit does NOT auto-fit header row heights (a small SetRowHeight stays small and clips): compute heights from text length / column width (~10 chars/in at 3/32 in text, 0.17 in per line + 0.08 in) and only ever shrink. | " + ECM + " RB1/RB2, LAY-T1 | → scripter, visual-verifier, revit-conventions |",
    "| R98 | Moving sheet content: a schedule goes to another sheet with ScheduleSheetInstance.Create then deleting the old instance; text notes move one at a time with ElementTransformUtils.CopyElements(view, ids, view, Transform.Identity, opts) + delete the original. Combine/renumber sheets in ONE own-Transaction script (R99) with a full dry run that rolls back. | " + ECM + " CMB-T1 (P001+P002) | → scripter, revit-conventions |",
    "| R99 | Own-Transaction pattern (transactionMode none): the dry run performs everything, measures (positions, extents, overlaps), then Transaction.RollBack() and checks its status; the writer still probes after it (R77). It is the only real test for layout/sheet changes. script-checker / revit_cs_check L5 'dryrun-order' fires on it as a known false positive: accept it when the script owns its Transaction and rolls it back on dryrun (a commit inside a group then group.RollBack stays a blocker, R86). | " + ECM + " CMB-T1, RB1/RB2, LAY-T1 | → scripter, script-checker, revit-writer, script-debugger, tools/revit_cs_check.py |",
]
add_after("R92", R)

s = s.rstrip("\n") + """

## 2026-11-20 roll-in: 26021 Erewhon Costa Mesa Site & Shell PC1, run 20261117-ECM
- Evidence: 26021 STATUS.md (run 20261117-ECM), manifests/ECM-*, scripts/ECM-*.cs, retro/20261117-ECM.md.
- New: O43 (re-scout after a pause), O44 (list before creating a folder; City naming), O45 (calcs from the live
  model), O46 (tracker answers/fixes at every update), O47 (project-scoped instructions), O48 (Outlook drafts via OWA),
  O49 (letter builder PYTHONUTF8 / --filler), R93 (sheet text anchored to named elements, PDF border), R94 (cleanout
  match, west/east by XY), R95 (pipe resize recipe), R96 (sticky schedule header cells), R97 (schedule widths, row
  heights), R98 (moving schedules/text between sheets), R99 (own-Transaction dry run; L5 false positive).
- Extended: O39 + R65 (batch_export never; Document.Export appends -1), R37 (linked view in the link document), R52,
  R58, R66 (copy an existing sequence id), R67 (obstacles), R75 (fitting centerline), R86.
- Not rolled in (project-specific): TI hiding in the 26021 arch link, the 26044 project number, stamp files.
- Agents: scripter, revit-writer, revit-scout, verifier (gen_agents.py); visual-verifier, script-debugger
  (gen_agents_upgrade.py); script-checker (gen_agents_cscheck.py). revit-conventions SKILL.md + scripting-lessons;
  run-tracker SKILL.md (O46); tools/revit_cs_check.py L5 note; MARKUP_SWARM_PROMPT run protocol 22.
"""
open(p, "w", encoding="utf-8", newline="").write(s)
print("lessons ok")
