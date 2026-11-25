"""One-off: roll in the 26030 Aritzia run (20261120-ATZ), its retro, the 24-071 Village move retro (20261123-VSM),
the 2026-0022 EBR AutoCAD lessons (A54-A58) and the 25-045 Excel deleterow/valign tool change (2026-11-25).

Sources: retro/20261120-ATZ.md (P1-P10), retro/20261123-VSM.md (P1-P14 + cross-document checklist), LESSONS.md
run notes R101-R115 / O50-O56 / Q29 / V12 (ATZ) and A54-A58 (EBR), tools/xl_ole.ps1 (deleterow, rowfmt valign).
- New: R116-R134, O57, X6. Extended: O54 (writer never re-checks; VSM P14). Superseded: ATZ P10 / VSM P12 (line
  budget) by the 2026-11-25 codeFile roll-in (stage by concern, >~1500 lines needs a reason).
- Tools done in the same pass: revit_cs_check L16-outer-catch (R116), writer_guard <task>.debug.c<n>.json (R117),
  sheet_tool accepts swarm-data junction targets (O50), REVIEW_BRIEF_TEMPLATE + gen_agents_qaqc rect_quality (Q29).
Idempotent: every insertion is skipped when its marker is already present.
"""
import re
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
MARK = "2026-11-25 (ATZ"   # marker used in every brief insertion
changed = []


def rd(p):
    return (ROOT / p).read_text(encoding="utf-8")


def wr(p, s, crlf=None):
    path = ROOT / p
    if crlf is None:
        crlf = b"\r\n" in path.read_bytes()
    path.write_bytes((s.replace("\r\n", "\n").replace("\n", "\r\n") if crlf else s).encode("utf-8"))
    changed.append(p)


def insert(p, anchor, text, marker=None, before=False):
    s = rd(p).replace("\r\n", "\n")
    marker = marker or text.strip().split("\n")[0]   # each insertion has its own marker: its first line
    if marker in s:
        print(f"skip {p}: already has {marker!r}")
        return
    n = s.count(anchor)
    assert n == 1, f"{p}: anchor found {n}x: {anchor[:70]!r}"
    s = s.replace(anchor, (text + anchor) if before else (anchor + text))
    wr(p, s)


def replace(p, old, new, marker):
    s = rd(p).replace("\r\n", "\n")
    if marker in s:
        print(f"skip {p}: already has {marker!r}")
        return
    assert s.count(old) == 1, f"{p}: {old[:70]!r}"
    wr(p, s.replace(old, new))


# ======================================================================================== agents: gen_agents.py
G = "build/gen_agents.py"

insert(G, "       stale for every element you touch: ask for a re-scout instead of scripting from it.\n", """\
     2026-11-25 (ATZ + VSM) rules (LESSONS R101-R115, R116-R134, O53/O55/O56; 26030 Aritzia run 20261120-ATZ and the
     24-071 Village move 20261123-VSM; details in scripting-lessons "Aritzia 26030 ATZ" / "Village move VSM"):
     - Outer catch (R116, lint L16 = error on writing scripts): every statement, pre-checks and lookups included, sits
       inside ONE top-level try whose catch (Exception ex) returns "FAILED @<stage>: <type>: <message>" from a stage
       variable you keep updating; no document.<Method>() or collector before it (lambdas may be declared before it).
     - No-op is a failure (R122): when expected.json asks for a change, the dry run reports would_edit=<n>; a run that
       would edit 0 returns NO_OP with failed=1, never success (ATZ-R27 v2 'succeeded' with done=0 on the wrong schedule).
     - Delete + rebuild (R101): never commit a delete stage before its rebuild has a dry-run GO: both in one group, or
       dry-run the rebuild against the deleted state inside a rolled-back group first (ATZ-R20 needed discard + reopen).
     - After document.Delete or a RollBack read no property of a pre-captured wrapper (R119): capture plain values
       (ids, diameters, XYZ) first; a rollback re-read takes each id in its own try/catch, an unreadable created id is
       gone, ROLLBACK_FAILED only on a readable mismatch.
     - Connected fittings (R118/R109): setting a size/radius on a fitting connected in a network can raise the modal
       "Disconnect the family from the network?" that FailuresProcessing never sees (ATZ-R17 v1 hung). Prefer delete +
       recreate; if you resize, register UIApplication.DialogBoxShowing (OverrideResult Cancel, log DialogId; any dialog
       in the dry run = NO_GO) and unregister it in finally.
     - Topology (R123/R103): plan only from the scout's connector list (size, BasisZ, joined to pipe / fitting / free,
       stubs < 0.3 ft); a smaller vent route keeps or recreates the reducer; a fixture connector pointing down or
       skewed, or a topology the list does not match, is a manual step, not a script.
     - Fittings (R124/R106/R107): a geometry probe never returns GO for a fitting: create it inside a rolled-back
       SubTransaction to learn whether Revit accepts it. A 45-deg wye on a sloped main: NewTeeFitting is refused
       ("angle too small or too large"); the working method places the wye FamilyInstance with its 'Nominal Radius n'
       set from an explicit sizeMap, aligned to the main's flow vector and rolled for arm grade, BreakCurve after
       placement, trims to port origins, ConnectTo, checks |Nport+Nend| ({SCRIPTS}/ATZ-R13-T2.A.v4.cs). After one
       NO_GO on a tie it goes on the hand-fix list.
     - Families (R110/R112): Document.LoadFamily(<path on L:>) can return false with no exception: copy the .rfa to a
       local temp folder and LoadFamily(localPath, out Family) in the script's own Transaction (mode none) with a
       logging dialog handler. Library accessories may size by TYPE params ('Connection RADIUS') and regenerate only in
       their rated range: probe with a +0.01 driver search before placing.
     - Links (R111): ByHostView makes a link follow the host phase filter (plans on 'Show All' showed the arch
       demolition); use Custom + SetPhase/SetPhaseFilter on the link ids with no LinkedViewId.
     - Text edits (R104/R105/R120): the OLD fragment may have been changed by an earlier task this run: re-derive it
       from those tasks' scripts and give text scripts a read-only "find" mode; edit the schedule that PRINTS the
       string (from the scout); find each row's label column by scanning that row, never reuse another row's index.
     - Tags (R121/O55/R113/R114): match TagText by numeric regex on normalised text (it reads "3.000000 FU"); test the
       tag text box, not the head point; apply the resolved subset and list unresolved items (the dry run states both
       counts). Deleting a pipe piece deletes its tags: record and re-create them. Never script riser/isometric tag
       layout (5 versions did not converge): hand it to the drafter. A View3D duplicated WithDetailing and cropped
       smaller keeps orphan tags/notes: delete them in the same task.
     - Sheets (R102/R115/O56): use the parameter the title block's date label reads (ATZ: shared 'Project Start Date',
       not SHEET_ISSUE_DATE); 'Drawn By' can live in SHEET_DRAWN_BY, 'MEP_Drawn By' and a second 'Drawn By': set all by
       Definition.Name. Packing viewports: measure GetBoxOutline + label first and try every order.
     - Views (O53): a tag/size stage skips and reports views with crop off or an unlocked 3D view instead of failing.
     - Cross-document moves (R125-R134, VSM; checklist in scripting-lessons "Village move VSM"):
       * Keynoting Settings (R125): compare keynote file + numbering of source and target before the first
         CopyElements; if they differ the orchestrator gets Jeremy to align them first, and the script header says
         "dialog likely". Any script that can raise a modal writes STARTED/DONE lines and its final return to
         {MAN}/<task>.<mode>.<requestId>.result.txt before returning (R126).
       * Counts come from a live list of the source ("copy all that match, require the N the sheets use"), never a
         hard-coded N (R127: 16 Village levels, not 15).
       * Parameters (R128): match names exact-case; when two source parameters differ only by case, take the one with
         non-empty values and say which.
       * Types first (R132): every filled-region, detail-group, detail-item and text type the copied annotation uses
         is created or mapped in the LOAD stage; a name-only clash is mapped, not skipped.
       * Category.SetLinePatternId takes only a LinePatternElement id or LinePatternElement.GetSolidPatternId(): a '-'
         source is skipped, solid maps to GetSolidPatternId(), missing named patterns are created from the source (R131).
       * A view-range level the target lacks is a hand item in the dry run, never a guess (R133).
       * The dry run's FailuresProcessing records WARNINGS as well as errors; GO needs both at 0 (R134: a duplicated
         group type 'Setback (Don't Delete) 2' surfaced only after commit).
""")

insert(G, "     Before you hand an .lsp back, run these yourself and report each result", """\
     2026-11-25 (ATZ pass; EBR AutoCAD rules, LESSONS A54-A57, 2026-0022; details in acad-conventions scripting-lessons):
     - Page setup (A54/A55): PlotConfiguration.GetPaperSize returns the RAW media size and ignores PlotRotation: decide
       the rotation once from the title block (landscape title block on portrait media, e.g. DWG To PDF ARCH E1 30 x 42
       -> PlotRotation 1) and check the EFFECTIVE size (swap sides for 1/3); never loop rotations until w > h. AO E1 on
       DWG To PDF: ARCH_full_bleed_E1_(30.00_x_42.00_Inches), origin 0,0, rotation 1, grayscale.ctb (the standard
       media's margins clipped the title-block column). Page setup lives in its own -PAGE / -PAGECHECK / -PAGECLEAN
       commands so a plot-setting failure never rolls back sheet content.
     - Copied text (A56): a vla-Copy of a plotting source inherits its ROTATION (EBR tags copied from a vertical margin
       note all printed at 90 deg and CHECK passed): set Rotation 0 on the copy, prefer a horizontal source, and CHECK
       rotation. A cond clause whose test is (and ...) with no body returns T: setq the value inside the test, return it.
     - Zones (A57): long axis-aligned LINEs that run past a zone (sheet division lines) are BOUNDARIES that split it: a
       clash for placement, not content that makes the zone "not empty". Take zone bounds from the scout's split lines.
""", marker="2026-11-25 (ATZ pass; EBR", before=True)

insert(G, "  is the get_warnings cap; use an uncapped Document.GetWarnings().Count probe (R52).\n", """\
- 2026-11-25 (ATZ + VSM; LESSONS R122/R126/R118/O54): a dry run reporting would_edit=0 for a task whose expected.json
  asks for a change is NO_GO "NO_OP", never followed by a run (ATZ-R27 v2 wrote success with done=0). A script whose
  header says "dialog likely" (cross-document copies, connected-fitting resizes): tell the orchestrator before the call
  so Jeremy can watch; after a timeout, once the dialog is cleared, read {MAN}/<task>.<mode>.<requestId>.result.txt and
  rewrite write.json from it with result_source "file" (VSM-LOAD v6 stayed "inferred"). A missing or stale precheck
  (script or expected.json changed) is NOT_RUN "precheck stale": the orchestrator re-runs script-checker, never you.
""")

insert(G, "  set (O45). A re-scout after a pause reports what changed since the earlier scout (O43: Jeremy had placed the GCO).\n", """\
- 2026-11-25 (ATZ + VSM; LESSONS R102/R105/R115/R123/R125/R127/R128/R132/R133): for every fixture or fitting a task
  connects to, list each connector (size, BasisZ direction, joined to pipe / fitting / free) and every neighbouring
  pipe shorter than 0.3 ft (R123: a 0.05 ft stub and a down-pointing DW-1 connector cost six dry runs). Name the
  parameter the title block's date / drawn-by labels read (R102/R115) and the schedule that actually PRINTS a string
  (R105). Every count quotes the exact query and the live list behind it, never a digest (R127: 16 Village levels; a
  sheet-name search missed the B-/L-/S- set). Cross-document moves: list every project/shared parameter bound to
  Sheets, Views and Viewports in source and target (exact-case name, storage type, GUID, group, filled/empty counts;
  R128), the Keynoting Settings of both (file, numbering; R125), every annotation type the copied views use
  (filled-region, detail group, detail item, text; R132) and each view range's levels with whether the target has
  them (R133).
""")

insert(G, "  Radius' as well as the pipes, and compare connector sets unordered. Cleanouts match \"CO\" or \"CLEANOUT\" in any case.\n", """\
- 2026-11-25 (ATZ + EBR; LESSONS A56/R115/O55/R122/R114): new AutoCAD text: check Rotation (reads horizontally) as
  well as string and position (EBR tags copied from a vertical note printed at 90 deg and passed). 'Drawn By' and
  dates: check the parameter the title block prints (all three Drawn By params, R115). Split or rebuilt pipes: their
  size tags still exist (count before/after, O55). Duplicated 3D views: no orphan tags/notes outside the section box
  (R114). A change task that shows done=0 is a FAIL, not a pass because nothing broke (R122).
""")

insert(G, "- Keep scans to one DWG per call batch and <= 40 entities per execute_lisp (AutoCAD crashed on a long 6-xref scan, A30).\n", """\
- Zones (2026-11-25 (ATZ pass), A57, 2026-0022 EBR): when asked whether a zone is empty, report long axis-aligned LINEs
  that run past it (sheet division lines, e.g. AO E1 '104 MED 2': y 21.28/21.75 in the right strip, x 16.974 full
  height) separately as zone splits with coordinates, not as content; the orchestrator writes zone bounds from them.
""")

# ======================================================================================== gen_agents_upgrade.py
U = "build/gen_agents_upgrade.py"
insert(U, "   keyed-note tags must clear pipes, cleanouts/fixtures and other annotations.\n", """\
   Cross-document moves and copied text (LESSONS R129/A56, 2026-11-25 (ATZ pass), VSM/EBR): compare each moved sheet
   with the source sheet's plot for view graphics (grids printed, arch background weight and poche, border bar weight,
   schedule header boxes, cut lines); a difference is a FAIL naming the category. New AutoCAD text must read
   horizontally (a copy that kept a vertical source's rotation is a FAIL).
""")
insert(U, "orchestrator gave you; if none was given, ask for it (\"run_id\": \"unknown\" is not allowed).\n", """\
File names (R117, 2026-11-25 (ATZ)): cycle 1 writes {MAN}/<task-id>.debug.json, cycle n >= 2 writes
{MAN}/<task-id>.debug.c<n>.json; never overwrite an earlier cycle's file (ATZ-R17 ran 6 versions under one debug.json).
Known causes (LESSONS R116-R124, ATZ): a bare 'Object reference' / 'target of an invocation' with no stage = a Revit
call outside the top-level try (L16: move it inside, add stage tags); InvalidObjectException after Delete/RollBack =
a pre-captured wrapper read (R119: capture plain values); a timeout on a connected-fitting resize = the "Disconnect
the family from the network?" modal (R118: delete + recreate, or DialogBoxShowing Cancel); "angle too small or too
large" on NewTeeFitting = the R107 wye method once, then the hand-fix list; LoadFamily false with no exception = load
a local copy (R110); a VSM 'Duplicate Types' timeout = keynote settings differ (R125, a model setting for Jeremy).
""")
replace(U, "3. Write {MAN}/<task-id>.debug.json: {{", "3. Write {MAN}/<task-id>.debug.json (cycle >= 2: <task-id>.debug.c<n>.json): {{",
        "(cycle >= 2: <task-id>.debug.c<n>.json)")
replace(U, "*.verify.json, *.visual.json, *.debug.json and *.chatgpt.json that carries this run id.",
        "*.verify.json, *.visual.json, *.debug.json, *.debug.c<n>.json (R117) and *.chatgpt.json that carries this run id.",
        "*.debug.c<n>.json (R117)")

# ======================================================================================== gen_agents_cscheck.py
C = "build/gen_agents_cscheck.py"
insert(C, "  findings (it does not index enums, R58): the compile decides.\n", """\
- 2026-11-25 (ATZ + VSM; R116-R134): lint L16-outer-catch on a writing script (no top-level try whose catch returns
  FAILED @<stage>, or a Revit call before it) is a blocker you cannot overrule (ATZ-R2-T1/R13-T1 returned bare
  exceptions). Major: a change script whose dry run gives no would_edit count or that returns success on done=0
  (R122); a delete stage that commits without its rebuild's dry-run GO (R101); reading a wrapper captured before
  Delete/RollBack (R119); a size change on a connected fitting without a DialogBoxShowing Cancel handler (R118); a
  probe that reports GO for a fitting it never created in a rolled-back SubTransaction (R124); one row's label column
  reused for another (R120); TagText matched with Contains on a formatted number (R121); cross-document copies without
  the keynote-settings check and result file (R125/R126), with a hard-coded count (R127), case-insensitive parameter
  matching (R128), SetLinePatternId with InvalidElementId (R131), or a dry run whose failure capture ignores warnings
  (R134).
""")

# ======================================================================================== gen_agents_p67.py
P = "build/gen_agents_p67.py"
insert(P, "  receptor or tie-in): never a condensate pump (J14).\n", """\
- 2026-11-25 (ATZ + VSM; LESSONS R113/R123/R128-R133): a tie or reroute whose scouted topology does not match the plan
  (connector size or direction, stubs < 0.3 ft) goes on the manual list before scripting (R123); riser/isometric tag
  cleanup is a drafter hand item, never a task (R113). A move of sheets/views between models is planned in this order:
  (1) Jeremy's hand step Transfer Project Standards > Object Styles + Line Weights (no API for the line weight table,
  R130); (2) LOAD: project info, levels, keynote settings, every type the copied annotation uses (R132), sheet/view/
  viewport parameters in exact case (R128); (3) a VIEW GRAPHICS task (per-view link settings, category V/G not driven
  by the template, filters, view-range levels, phase filter; R129/R133) BEFORE any sheet is built; (4) sheets and
  views; (5) the hand list (tags that need their hosts, area/room tags). Checklist: revit-conventions scripting-lessons
  "Village move VSM".
""")
replace(P, """xl_ole.ps1 has a `rowfmt` mode (row height / font size, values checked) but
  excel_bash_guard.py does not allow it until Jeremy adds it; if it is blocked, report and stop.""",
        """xl_ole.ps1 `rowfmt` sets row height / font size / vertical alignment
  ("valign": top|center|bottom), checks values unchanged and restores everything on any change. `deleterow` (X6,
  2026-11-25 (ATZ pass); 25-045 duplicate LAV-2 row) deletes ONE worksheet row, cells shifting up, only after every
  `expect` cell on that row contains its text: spec {{"sheet","row","expect":{{"A7":"LAV-2"}}}}. Use it only when the
  manifest names the row and quotes Jeremy asking for the delete; report deleted_cells and the used range before/after.""",
        "`deleterow` (X6")

# ======================================================================================== revit-conventions skill
S = "claude/skills/revit-conventions/SKILL.md"
insert(S, "  R97; details in scripting-lessons \"Erewhon 26021 ECM\").\n", """\
- Every statement sits inside one top-level try whose catch returns "FAILED @<stage>: ..." (lint L16, R116). A change
  task whose dry run would edit 0 is NO_OP = failed (R122). Never commit a delete before its rebuild has a dry-run GO
  (R101). No size change on a network-connected fitting without a DialogBoxShowing Cancel handler; prefer delete +
  recreate (R118). Learn whether a fitting can be made by creating it in a rolled-back SubTransaction, not from
  geometry (R124). Details in scripting-lessons "Aritzia 26030 ATZ" (2026-11-25 (ATZ pass)).
- Moving sheets/views between models: follow the checklist in scripting-lessons "Village move VSM": Jeremy's line
  weights / object styles transfer first, LOAD of types, parameters and keynote settings, a view-graphics task before
  any sheet (R125-R134).
""")

SL = "claude/skills/revit-conventions/references/scripting-lessons.md"
vsm = rd("retro/20261123-VSM.md").replace("\r\n", "\n")
checklist = vsm[vsm.index("## Cross-document sheet move checklist"):].split("\n", 1)[1].strip("\n")
sl = rd(SL).replace("\r\n", "\n")
if "## Aritzia 26030 ATZ" not in sl:
    sl = sl.rstrip("\n") + """

## Aritzia 26030 ATZ lessons (2026-11-25 roll-in; swarm/LESSONS.md R101-R124, O53-O56; run 20261120-ATZ)
Script shape
- One top-level try around EVERYTHING, pre-checks included; its catch returns "FAILED @<stage>: <type>: <message> |
  <first frame>". R2-T1 and R13-T1 v1 threw before their try and returned a bare 'Object reference' / 'target of an
  invocation'; each cost a debug cycle (R116; revit_cs_check L16 is an error on writing scripts).
- A change task whose dry run would edit 0 is NO_OP = failed; the dry run states would_edit=<n> (R27 v2 targeted the
  wrong schedule and logged success with done=0) (R122).
- Never commit a DELETE stage before its REBUILD stage has a dry-run GO; pair them in one TransactionGroup or dry-run
  the rebuild against the deleted state in a rolled-back group (R20: the 2x1-1/2 reducer could not be recreated, the
  model was recovered by discard and reopen) (R101).
- After document.Delete or a RollBack, read nothing from a pre-captured wrapper (Pipe, Line, Connector): capture ids,
  diameters and XYZ first. A rollback re-read takes each id in its own try/catch (unreadable created id = gone);
  ROLLBACK_FAILED only on a readable mismatch (R25 v2/v3 reported ROLLBACK_FAILED on a clean model) (R119).
Pipes and fittings
- Setting size/radius on a fitting connected in a network can raise "The family is connected in a network and can no
  longer keep the connectivity. Disconnect the family from the network?" (OK/Cancel), a modal that FailuresProcessing
  never sees (R17 v1 timed out until Jeremy answered). Delete and recreate instead, or register
  UIApplication.DialogBoxShowing (OverrideResult Cancel, log DialogId, dry run NO_GO on any dialog). With that handler
  in place, resizing a connected wye branch via 'Nominal Radius 2' inside a delete+rebuild group worked (R118/R109).
- Plan connections from the scout's connector list: size, BasisZ, joined to pipe / fitting / free, neighbouring pipes
  < 0.3 ft (R17: 3488613 was a 0.05 ft stub; R15-T2: DW-1's connector points (0,0,-1); R20-T2: vent connector 2 in > the
  1-1/2 in route, which needs the reducer kept, CPC 703.2) (R123/R103).
- A geometry probe cannot say GO for a fitting: create it in a rolled-back SubTransaction and report Revit's answer
  (R13-T2 / R19 probes said GO, NewTeeFitting then refused "angle between them is too small or too large") (R124/R106).
- 45-deg wye on a sloped horizontal main (R107, R13-T2 A.v4 from the ChatGPT second opinion, manifests/ATZ-WYE.chatgpt.md):
  place the wye FamilyInstance, set 'Nominal Radius n' from an explicit sizeMap ("1,2|3" = run|branch for
  120D_Wye 45 Deg Tee Reducing), 3D-align the run to the main's flow vector and roll about it for arm grade,
  BreakCurve after placement, trim to port origins, ConnectTo, check |Nport+Nend|.
- Document.LoadFamily(path) returned false with no exception for 3 complete library .rfa files on L:. Copy to a local
  temp folder and LoadFamily(localPath, out Family) in the script's own Transaction (mode none) with a logging dialog
  handler: all 3 (Revit 2021 families) loaded first try (R108/R110).
- Library accessories may size by TYPE params (filter: 'Connection RADIUS' drives the connector), and some regenerate
  only in their rated range (dual check 1/4-3/8 fails at 1/2): probe with a +0.01 driver search before placing (R112).
Views, links, tags, sheets
- RevitLinkGraphicsSettings ByHostView makes the link follow the host's phase filter (plans on 'Show All' then show the
  arch demolition): use Custom + SetPhase/SetPhaseFilter on the link ids, no LinkedViewId (R111).
- Tag matching: TagText reads "3.000000 FU"; match by regex 3(\\.0+)?\\s*FU on normalised text; test the tag's text box
  (not its head point) against the target rect; in iso views far tags project onto the same sheet spot, so they are
  obstacles; apply the resolved subset and list unresolved items (R12-T1/T2) (R121).
- Deleting a pipe piece deletes its size tags: scripts that split pipes record and re-create them (ACC-T2 v5/v6) (O55).
- Riser/isometric tag auto-layout did not converge in 5 versions (text box shifts 0.5-1.8 in with leader side): do
  not script it; at most offer duplicate-tag deletion (R113).
- View3D.Duplicate(WithDetailing) then a smaller section box leaves the copied tags/notes of hosts outside the box
  floating: delete that orphan annotation in the same task (R114).
- Tag/size stages skip and report views with crop off or an unlocked 3D view (O53).
- Schedule/sticky text: find each row's label column by scanning that row (R9-T1: row 13's label 'B.' was in col 0,
  not row 12's column) (R120); the OLD fragment can change in an earlier task of the run (R5 -> R26): re-derive it and
  give the script a read-only "find" mode (R104); scout which schedule prints the string (R27 v2 searched the symbol
  legend; abbreviations were in 'P5.00 ABBREVIATIONS') (R105).
- Title block date: scout which parameter the label reads (ATZ: sheet shared 'Project Start Date', GUID 3d21cc7a-...,
  not SHEET_ISSUE_DATE) (R102). 'Drawn By' may live in SHEET_DRAWN_BY, shared 'MEP_Drawn By' and a second 'Drawn By':
  set all by Definition.Name and check the printed sheet (R115).
- Packing viewports: measure GetBoxOutline + label first and try all orders; find the title-strip edge from geometry
  (it fell back to tbMinX + 3.6 ft) (O56).

## Village move VSM lessons (2026-11-25 roll-in; swarm/LESSONS.md R125-R134; run 20261123-VSM, PLMB-24220-R25 -> VILLAGE)
- Every cross-document CopyElements that brings a keynote-bearing element (schedule, keynote tag family, view template,
  detail item with a keynote param) raised a modal 'Duplicate Types' (Keynoting Settings : Standard) when the two
  models point at different keynote files: 4 timeouts, 4 Jeremy clicks. Compare keynote file + numbering first; align
  them only with Jeremy's OK (a model setting); even then expect the dialog: mark the script "dialog likely" (R125).
- A script that can hit a modal writes STARTED/DONE progress and its final return to
  manifests/<task>.<mode>.<requestId>.result.txt before returning; the writer rewrites write.json from it after the
  dialog clears (result_source "file") (R126).
- Never hard-code a count from the plan: R25 has 16 Village levels (an extra 'COAST - NEW BASEMENT LEVEL'); copy all
  that match and require the N the sheets use; scouts quote the query behind every count (R127).
- Sheet parameters: VILLAGE had 'Sheet Sort / Sheet Group / Sub-Discipline' while R25 had both 'SHEET Category' (filled)
  and 'Sheet Category' (empty); a case-insensitive match took the empty ones. Match exact-case; when two differ only
  by case, choose by non-empty count and say which (R128; extends R71).
- View graphics not driven by the template (RevitLinkGraphicsSettings per view, GetCategoryHidden /
  GetCategoryOverrides, filters, detail level, phase filter, view range) and project standards (object styles, line
  patterns, text and filled-region types) need their own task BEFORE sheets: SHT-T1 failed visual (grids printed,
  faint arch, 4x border bars) and VG-T1 changed 2,530 per-view settings afterwards (R129).
- The line weight table has no API: Jeremy's Transfer Project Standards (Object Styles + Line Weights) is the first
  manual step; do not script model-category object styles one by one (R130).
- Category.SetLinePatternId accepts only a LinePatternElement id or LinePatternElement.GetSolidPatternId():
  InvalidElementId threw ArgumentException (VG-T1 B failed=3) (R131).
- Create or map every filled-region, detail-group, detail-item and text type the copied annotation uses in LOAD; a
  group type clash only warned and made 'Setback (Don't Delete) 2'; area/room tags need the scheme and rooms: list them
  as hand items in the plan (R132).
- A view range that uses a level the target lacks (PARKING - LEVEL B1) is reported in the dry run as a hand item (R133).
- The dry run's FailuresProcessing must record warnings too; GO = failureTotal 0 for errors AND warnings (R134).

Cross-document sheet move checklist (retro 20261123-VSM; what must be copied besides sheets and views):
""" + checklist + "\n"
    wr(SL, sl)

ASL = "claude/skills/acad-conventions/references/scripting-lessons.md"
asl = rd(ASL).replace("\r\n", "\n")
if "## 2026-0022 EBR" not in asl:
    asl = asl.rstrip("\n") + """

## 2026-0022 EBR 2026-11-24 (Eastern Breezeway, AO E1 title block; swarm/LESSONS.md A54-A58)
- PlotConfiguration.GetPaperSize returns the RAW media size and ignores PlotRotation: never pick a rotation by looping
  values until w > h (EBR-SHT-T1 v2 left rotation 0 and rolled back). Decide once: landscape title block on portrait
  media (DWG To PDF ARCH E1 = 30 x 42) -> PlotRotation 1; check the EFFECTIVE size (swap sides for 1/3). Keep page
  setup in its own -PAGE / -PAGECHECK / -PAGECLEAN commands so a plot-setting failure never rolls back content (A54).
- AO E1 on DWG To PDF: the standard ARCH_E1_(30.00_x_42.00_Inches) media has printable margins (plot shifted ~2 in,
  title-block column clipped). Use ARCH_full_bleed_E1_(30.00_x_42.00_Inches), origin 0,0, rotation 1, grayscale.ctb (A55).
- New text made by vla-Copy of a plotting source inherits the source ROTATION: EBR-TAG-T1 copied 1F1A, a vertical
  margin note, and every tag printed at 90 deg while the API CHECK passed. Set Rotation 0 (and direction) on the copy,
  prefer a horizontal source, and CHECK rotation. A cond clause whose test is (and ...) with no body returns T into
  strcat: setq the value inside the test and return it (EBR-SHT-T1 v4) (A56).
- 'Zone must be empty' checks treat long axis-aligned LINEs that run past the zone (sheet division lines: '104 MED 2'
  y 21.28/21.75 in the right strip, x 16.974 full height) as BOUNDARIES: a clash for placement, not content. Scouts
  report them as zone splits; the orchestrator reads them before writing zone bounds (A57).
- Bridge `drawing open` still returns 'opened' without opening in AutoCAD 2027 (A3): the orchestrator opens with
  Invoke-Item between agent runs and checks for one acad process (A58).
"""
    wr(ASL, asl)

# ======================================================================================== orchestrator prompt
M = "MARKUP_SWARM_PROMPT.md"
mp = rd(M).replace("\r\n", "\n")
if "\n23. 26030 Aritzia" not in mp:
    mp = mp.rstrip("\n") + """
23. 26030 Aritzia run 20261120-ATZ, 24-071 Village move 20261123-VSM, 2026-0022 EBR (2026-11-25 roll-in, LESSONS
    O50-O57, R116-R134, A54-A58):
    a. Tracker first (O57): publish the run tracker before the first scout, with the planned steps, and name it in the
       first scout brief (VSM published it only when Jeremy asked). Read tracker answers at EVERY bridge-idle point,
       not only at planned decision points (O52: answers sat ~40 min, the 3rd miss in one run).
    b. Failure counter (R117/R100): keep one per task in STATUS.md, counting dry-run-driven redesigns too, and apply
       the ladder by it (1 debugger c1, 2 opus fix mode, 3 debugger c2 + ChatGPT, then FAIL / manual). Debug cycles
       write <task>.debug.json, then <task>.debug.c<n>.json (ATZ: R17 ran 6 versions with one debug.json, no c2).
    c. Re-check after edits (O54): after ANY edit of a task's script or expected.json, re-run script-checker before
       launching the writer; writers never re-check (VSM-VG-T1 B went NOT_RUN).
    d. Modal dialogs (R118/R125/R126): when a script header says "dialog likely" (cross-document copies with differing
       keynote settings, connected-fitting resizes), tell Jeremy on the tracker before the call and poll
       tools/dialog_check.ps1; after a timeout the writer rewrites write.json from the result file. Aligning keynote
       settings in the target is a model setting: Jeremy's OK first.
    e. Cross-document moves (R129/R130): Jeremy's Transfer Project Standards (Object Styles + Line Weights) is the FIRST
       step, before any script; the planner's view-graphics task runs before sheets are built (checklist in
       revit-conventions scripting-lessons "Village move VSM").
    f. Discarding unsynced bad work (O51): Jeremy closes without saving and reopens from ACC; then a read-only reconcile
       scout against the last-sync checklist before any write.
    g. AutoCAD (A57/A58): read the scout's division lines before writing zone bounds; open DWGs with Invoke-Item
       between agent runs (bridge `drawing open` says 'opened' without opening) and check for one acad process.
    h. QAQC (O50/Q29): sheet_tool accepts swarm-data junction targets, so QAQC project folders may stay junctions;
       reviewers give rect_quality "approximate" and the markup-locator confirms rects with `check`.
"""
    wr(M, mp)

# ======================================================================================== LESSONS.md
L = "LESSONS.md"
ls = rd(L).replace("\r\n", "\n")
if "## 2026-11-25 roll-in: ATZ + VSM retros" not in ls:
    for a in ("A54", "A55", "A56", "A57"):
        row = re.search(rf"\n\| {a} \|[^\n]*\(pending roll-in\) \|", ls)
        assert row, a
        ls = ls.replace(row.group(0), row.group(0).replace("(pending roll-in)", "(rolled in 2026-11-25)"))
    old = "| -> acad-ops (already A3) |"
    assert ls.count(old) == 1
    ls = ls.replace(old, "| -> acad-ops (already A3), orchestrator (MARKUP 23g, rolled in 2026-11-25) |")
    ls = ls.replace("- O50 (open): sheet_tool.py", "- O50 (fixed 2026-11-25: sheet_tool accepts swarm-data junction targets): sheet_tool.py")
    ls = ls.replace("- Q29 (open): REVIEW_BRIEF_TEMPLATE", "- Q29 (fixed 2026-11-25: reviewers give \"approximate\", locators own `check`): REVIEW_BRIEF_TEMPLATE")
    x5 = re.search(r"\n\| X5 \|[^\n]*", ls).group(0)
    ls = ls.replace(x5, x5 + "\n| X6 | xl_ole.ps1 gained `deleterow` (deletes ONE worksheet row, cells shift up, only after every `expect` cell on that row contains its text; reports deleted cells and used range) and `rowfmt` \"valign\" (top/center/bottom, restored on failure); excel_bash_guard allows deleterow. Use deleterow only when the manifest names the row and quotes Jeremy's request. | 25-045 S45-R1-T1 (duplicate LAV-2 row), 2026-11-24 | → excel-writer, hooks (rolled in 2026-11-25) |", 1)
    ls = ls.rstrip("\n") + """

## 2026-11-25 roll-in: ATZ + VSM retros, EBR (A54-A58), 25-045 Excel
- From retro/20261120-ATZ.md: R116 one top-level try/catch-return around everything (P3; revit_cs_check L16, error on
  writing scripts), R117 debug cycle files <task>.debug.c<n>.json + per-task failure counter (P8; writer_guard, MARKUP
  23b), R118 no size change on a connected fitting without a DialogBoxShowing Cancel handler (P1), R119 no wrapper reads
  after Delete/RollBack (P2), R120 label column per row (P4), R121 tag match by regex / text box / apply the resolved
  subset (P5), R122 NO_OP = failed (P6), R123 connector topology baseline (P7), R124 fitting feasibility by a rolled-back
  SubTransaction (P9). P10 (line budget, precheck hash of expected.json) already covered: codeFile roll-in + O54 gate.
- From retro/20261123-VSM.md: R125 keynote settings / Duplicate Types dialog (P1), R126 result file for modal-prone
  scripts (P2), R127 live counts, scouts quote the query (P3), R128 exact-case parameter match (P4), R129 view-graphics
  task before sheets (P5), R130 line weights hand step first (P6), R131 SetLinePatternId (P7), R132 annotation types in
  LOAD (P8), R133 view-range levels (P9), R134 warnings count in the dry run (P10), O57 tracker before the first scout
  (P13), O54 extended (writer never re-checks, P14). P11 = R116; P12 superseded by codeFile. The cross-document checklist
  is in revit-conventions scripting-lessons "Village move VSM".
- ATZ run notes R101-R115, O51-O56, V12 rolled into scripter / revit-scout / revit-writer / verifier / script-checker /
  script-debugger briefs and scripting-lessons "Aritzia 26030 ATZ"; O50 and Q29 fixed in tools; EBR A54-A58 rolled into
  scripter, acad-scout, verifier, visual-verifier and acad-conventions scripting-lessons; X6 into excel-writer.
- Files: build/lessons_2026-11-25.py (briefs, skills, MARKUP 23), tools/revit_cs_check.py (L16 + test),
  claude/hooks/writer_guard.py (+3 tests), tools/sheet_tool.py, templates/REVIEW_BRIEF_TEMPLATE.md,
  build/gen_agents_qaqc.py, tools/xl_ole.ps1 + claude/hooks/excel_bash_guard.py (X6).
- Still open: R46 (roof drain family connector, Jeremy); P12 (22-022) text that does not plot; first live run with
  codeFile and with L16 in force (older scripts in scripts/ fail L16; only new versions must pass).
"""
    wr(L, ls)

print("changed:", *changed, sep="\n  ")
