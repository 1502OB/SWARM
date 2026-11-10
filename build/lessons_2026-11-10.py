"""One-off: fold the 24-071 Pacific Edge lessons (2026-11-10) into LESSONS.md."""
p = "LESSONS.md"
s = open(p, encoding="utf-8").read()


def add(section_hdr, rows):
    global s
    i = s.index(section_hdr)
    j = s.index("\n## ", i + 5)
    s = s[:i] + s[i:j].rstrip("\n") + "\n" + "\n".join(rows) + "\n" + s[j:]


R = [
    r"| R30 | TextNote.Text holds \r line breaks: normalise with Regex \s+ -> ' ' before matching (R9-T1 failed on a double space). | 24-071 R9-T1.write.log.md | → scripter, revit-conventions |",
    "| R31 | The bridge hides thrown messages ('target of an invocation'). Pre-change failures RETURN 'FAILED <task>: ...'; wrap risky calls and return ex.Message. | 24-071 R6-T1 attempt 1 | → scripter, revit-conventions |",
    "| R32 | Pinned elements throw on Delete: unpin in the same transaction first (the F-P2.04 roof PDF raster). | 24-071 R6-own.scout.json | → scripter, revit-conventions |",
    "| R33 | 'Not visible' = check worksets first: hidden-by-default worksets (A-OCEAN NORTH arch link, A-STORM new pipes) with views on UseGlobalSetting. SetWorksetVisibility per view or on our template. | 24-071 R29-link / R29-basis | → scripter, revit-scout, revit-conventions |",
    "| R34 | Level.Elevation can be survey-based (Elevation Base = Survey Point): internal Z = ProjectElevation (57.66 vs 169.84). Never type elevations; check against an existing element. A scout reported risers '112 ft too high' from the wrong basis. | 24-071 R29-basis.digest.md | → scripter, revit-scout, revit-conventions |",
    "| R35 | Work-plane-based families: NewFamilyInstance with a SketchPlane reference fails 'on face'; level.GetPlaneReference() works. | 24-071 R29-T2b | → scripter, revit-conventions |",
    "| R36 | View templates cannot be Duplicate'd: CreateViewTemplate() from a view using it + copy the non-controlled list; never edit a template shared with other buildings (Jeremy). | 24-071 DV-WS-T1; memory new-template-not-shared | → scripter, revision-planner, revit-conventions |",
    "| R37 | RevitLinkGraphicsSettings has no per-category control: By Linked View shows the linked view's grids. Hide linked grids with Custom + same LinkedViewId + host OST_Grids hidden. Match background views by level ProjectElevation + building prefix, not name. | 24-071 BG-T1, GRD-T1/T2 | → scripter, revit-scout, revision-planner, revit-conventions |",
    "| R38 | ScheduleFilter Contains only on string fields (Level = ElementId rejects it), AND only: tag Comments or filter Equal on the id. | 24-071 P009-T1 attempt 2 | → scripter, revit-conventions |",
    "| R39 | Schedule titles with dark fill + white text print black-on-dark in grayscale: white cell, black bold text; override flags before SetCellStyle; headings = Body row 0; group header/footer off for count schedules. | 24-071 FMT-T1/T2 | → scripter, revit-conventions |",
    "| R40 | No class declarations in send_code_to_revit (method body). IFamilyLoadOptions via Reflection.Emit worked once, but writer_guard now blocks reflection: family reload with overwrite is manual. | 24-071 TB-T1, P71-TB-T2 | → scripter, revision-planner, revit-conventions |",
    "| R41 | EditFamily / LoadFamily / Document.Export need transactionMode none; open your own Transaction in the family doc. | 24-071 TB-T1 | → scripter, revit-conventions |",
    "| R42 | Editing an element another user has borrowed (Project Information) raises an editing-request dialog that blocks the bridge. Dryrun checks GetCheckoutStatus and returns FAILED with the owner. | 24-071 P71-PI-T1 | → scripter, revit-writer, revit-scout, prompt 18d |",
    "| R43 | BIMxP consultant shared params: ParameterUtils.DownloadParameter threw; recreate by the arch GUID via a temp shared parameter file (restore SharedParametersFilename); delete the same-name non-shared param first. | 24-071 P71-SP-T1 | → revit-conventions, revision-planner |",
    "| R44 | batch_export replies lag one call and have no colour/sheet-size option: verify each PDF on disk by sheet number; deliverable sets via Document.Export PDFExportOptions (GrayScale, sheet size, Combine) + tools/bookmark_set.py. | 24-071 FINAL*, GRAY | → visual-verifier, prompt 18b/18c, revit-conventions |",
    "| R45 | Sync comment: no % (orchestrator_guard COMMENT_CHARS). | 24-071 SYNC 11-05 | → prompt 18a |",
    "| R46 | Check a placed family's connector classification in the dryrun: the 120D combo roof drain connector is SupplyHydronic, so SD leaders could not connect (library family needs fixing). | 24-071 R29-T2b.connectors.txt | → scripter, revit-conventions; family fix open (Jeremy) |",
]
add("## R. Revit bridge and API", R)
Q = [
    "| Q23 | Spot checks are not a set check: plot and review EVERY sheet of the issue (contact sheets) before saying done. 24-071: three Surf W/V sheets and all D-P sheets had no background, found only when Jeremy asked 'have you plotted'. | 24-071 FINAL, crops/final | → prompt 18b, visual-verifier |",
    "| Q24 | Look at a doubtful visual finding yourself before acting: 'circles may be sprinkler heads' were floor drains. | 24-071 R7-T1.visual.json | → prompt 18f |",
]
add("## Q. QAQC review and markups", Q)
J = [
    "| J10 | One building's views need a template change: make a NEW template for them; never edit the shared one. | 24-071 11-05 | → scripter, revision-planner (R36) |",
    "| J11 | Team/transmittal sets are grayscale, true sheet size, combined per building, bookmarked, next to the other disciplines in the Transout folder. | 24-071 11-10 | → prompt 18c |",
    "| J12 | Civil invert lists: inverts from each building's LOWEST level, gravity line, not the pumped line; estimates are fine when not modeled; cover the whole site. | 24-071 11-10 | reference (tools/c3_poc_list_24071.py) |",
]
add("## J. Jeremy's standing preferences", J)
s = s.rstrip("\n") + """

## 2026-11-10 Pacific Edge 24-071 roll-in (Jeremy: "review the lessons learned to implement into the agents")
- Lessons R30-R46, Q23-Q24, J10-J12 from the 24-071 Ocean North 100% DD run (11-04..11-10). Backup: build/backup_2026-11-10/.
- revit-conventions: Contract (borrowed elements, shared templates, no class declarations, hidden exception text) and
  references/scripting-lessons.md "Pacific Edge 24-071 lessons" (bridge shape, matching, ownership dialogs, elevation
  basis, work-plane families, worksets, templates, link display, schedule filters/styles, consultant shared params,
  grayscale export).
- Agents: scripter (Revit rules block), revit-writer (dialog on first write), revit-scout (elevation bases, not-visible
  triage, checkout status, background matching) in gen_agents.py; revision-planner (manual items, BEP compliance table)
  in gen_agents_p67.py; visual-verifier (export lag, sheet-number check, full-set mode) in gen_agents_upgrade.py.
- MARKUP_SWARM_PROMPT run protocol 18 (sync comment chars, plot before done, grayscale team sets, borrowed-element
  dialogs, BEP intake, doubtful visual findings, reflection guard, shared project.json).
- Tools added this run: tools/contact_sheets.py, tools/assemble_sheet_set.py, tools/bookmark_set.py,
  tools/c3_poc_list_24071.py.
- Not done (Jeremy's call): a writer_guard exception for an IFamilyLoadOptions helper (family reload with overwrite);
  the 120D combo roof drain family connector fix (SupplyHydronic -> storm); library copies of the P71 scripts.
"""
open(p, "w", encoding="utf-8").write(s)
print("lessons ok")
