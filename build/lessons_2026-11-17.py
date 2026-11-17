"""One-off: tidy LESSONS.md and fold in the 23-073 / 25-076 / 26036 CFA / 25-061 WF lessons (2026-11-17).

- Repairs two broken tables: a blank line had split R60 off the R table, and a stray '---' had split the J table, so
  J10-J12 existed twice. The 25-076 J rows become J13-J15 (24-071's J10-J12 were rolled in first, MARKUP 18).
- Moves the free-form 2026-11-11 (R61, R62) and 26036 CFA (R59-R66) notes into the R table. CFA R60-R66 collided with
  23-073 / 25-076 rows and become R75-R81.
- Adds retro/20261116-1839.md (Whole Foods 25-061 WF-R1-T1/T2) P1-P7 as O37, O38 (and extends O34), R82-R85.
"""
p = "LESSONS.md"
s = open(p, encoding="utf-8").read()  # universal newlines; written back as LF (git stores LF)


def sub(old, new, count=1):
    global s
    n = s.count(old)
    assert n == count, (n, old[:80])
    s = s.replace(old, new)


def row_line(rid):
    i = s.index(f"\n| {rid} |") + 1
    return s[i:s.index("\n", i)]


# ---- J table: one table, 24-071 J10-J12 in place, 25-076 rows renumbered J13-J15 ----
j76 = [row_line(x) for x in ("J10", "J11", "J12")]  # first occurrences = the 25-076 rows
assert "Always run independent work" in j76[0] and "condensate" in j76[1] and "FINAL plan check" in j76[2]
j71_block = s[s.index("\n---\n| J10 | One building"):]
j71_block = j71_block[:j71_block.index("\n\n")]
j71 = j71_block.strip().split("\n")[1:]
assert len(j71) == 3 and all(r.startswith("| J1") for r in j71)
sub("\n".join(j76) + "\n", "")
sub(j71_block, "")
renum = [r.replace(f"| J1{k} |", f"| J1{k + 3} |", 1) for k, r in enumerate(j76)]
sub(row_line("J9") + "\n", row_line("J9") + "\n" + "\n".join(j71 + renum) + "\n")

# ---- R table: rejoin R60 (25-076), add the note-form lessons as rows ----
r60 = row_line("R60")
sub("\n\n" + r60 + "\n", "\n")  # re-added in order after R59 below
WF = "25-061 WF retro/20261116-1839.md"
CFA = "26036 CFA 2026-11-13"
R = [
    "| R59 | send_code returns are truncated by the bridge at ~3 KB (4 of 7 CFA batch-2 scripts failed precheck on it). Print `done=/skipped=/failed=` and `cleanup_params` FIRST, then a capped detail body; the whole return <= 2.3 KB on every path (GO, NO_GO, ROLLED_BACK). | " + CFA + " T6/T7a/T10b/T12 | → scripter, script-checker, revit-conventions |",
    "| R61 | FilledRegion.Create (masking or filled) is not allowed on a ViewSheet. Mask raster text on a sheet with an opaque-background TextNoteType or a scale-1 drafting view in a no-title viewport; first check whether the 'raster' text is live (TextNote, schedule, drafting view) and edit it in place. | 25-076 2026-11-11 sheet-text batch | → scripter, revision-planner |",
    "| R62 | Give each long send_code script (> ~300 lines) its own revit-writer: a batched writer runs out of context sending dry run + real run and leaves the queued tasks unrun (see R70/R72). | 25-076 2026-11-11 | → orchestrator, revit-writer |",
    "| R75 | Deleting a pipe/pipe fitting also deletes its centerline sub-element (class Element, OST_PipeCurvesCenterLine -2008045 / OST_PipeFittingCenterLine -2008072, a GetDependentElements member of the host; NOT OST_CenterLines). Cascade guards allow exactly these when they depend on a target. | " + CFA + " T7a (3 NO_GOs; was CFA 'R60') | → scripter, revit-writer |",
    "| R76 | Viewport.GetLabelOutline can return a far-off outline (off-sheet Y) right after a view scale/crop change, even after Regenerate: sanity-check it against the viewport box, fall back to an estimate, confirm on the plotted sheet. | " + CFA + " (was CFA 'R61') | → scripter, visual-verifier |",
    "| R77 | SAFETY: an R47 dry run (Commit inside a TransactionGroup, then RollBack) PERSISTED its changes for a script editing VIEW state (rename, crop, HideElements, viewport SetBoxCenter, detail curves; T16a v3); pipes rolled back fine. Every R47 dry run checks grp.Start()/grp.RollBack() statuses AND re-reads the model after RollBack (created ids gone, changed values back) and returns ROLLBACK_FAILED otherwise. View/sheet/annotation edits use a read-only dry run (no setters). Viewport crop/scale/SetBoxCenter can move LabelOffset: restore it. | " + CFA + " T16a (was CFA 'R62') | → scripter, script-checker, revit-writer, script-debugger, revit-conventions |",
    "| R78 | Work-plane-based floor drains (FD-1) place on a ReferencePlane (doc.Create.NewReferencePlane + GetReference), not a SketchPlane reference ('Failed to create family instance on face'); level-plane placement can't be moved off the level. P-trap families placed by point lie flat: model FDs without trap families (trap by note). | " + CFA + " (was CFA 'R63') | → scripter, revit-conventions |",
    "| R79 | Wye-only pipe type: a vertical vent off a horizontal drain needs a 45deg wye rolled up + 1/8 bend; never fall back to geometry that moves the main. | " + CFA + " (was CFA 'R64') | → scripter |",
    "| R80 | Wye-only pipe type: no NewTeeFitting variant (vertical, 45deg rolled, reducer, tee on back) makes a vent branch off a sloped 3 in main; script the riser unconnected and leave the tie manual (a revision-planner 'manual' step). | " + CFA + " (was CFA 'R65') | → scripter, revision-planner |",
    "| R81 | Size every leg between fittings as the routing-preference family take-offs + 0.5 ft (measure with a rolled-back probe), never a planned centre-to-centre distance. | " + CFA + " (was CFA 'R66') | → scripter, revit-conventions |",
    "| R82 | Model/view point -> sheet: vp.GetBoxCenter() + ((pt - cropCentre) projected on view Right/Up) / view.Scale. Never Viewport.GetProjectionToSheetTransform on model coordinates (gave ~147,209 ft, twice). Assert the result is inside vp.GetBoxOutline() and the title block before creating anything; report the mapping in the dry run. Helper: library/revit/model-to-sheet.cs. | " + WF + " P4 (WF-R1-T1 v1/v2 NO_GO, T2 draft) | → scripter, script-checker, revit-conventions, library |",
    "| R83 | A scout 'not found' counts only with the exact search listed (keywords, element classes, schedule sections) and whitespace/case-normalised matching per keyword; schedules: Header, Body and Footer; text: every TextNote on every sheet plus every view on the named sheets, before saying text 'lives in the DWG/PDF'. | " + WF + " P2 (WF-GEN said 0 of 4) | → revit-scout, revit-conventions |",
    "| R84 | A cloud on a schedule row is inset by the arc sagitta so its arcs stay inside the row band (neighbouring rows' text untouched); a delta tag is tested against in-viewport text, existing tags AND the sheet/title-block border lines, not only sheet-level elements. Check the plotted crop before the first write. | " + WF + " P5 (WF-R1-T1 visual FAIL) | → scripter, visual-verifier, revit-conventions |",
    "| R85 | expected.json counts of pre-existing elements come from the scout baseline (clouds per sheet from the sheet's own list, all revisions), and every script fallback position/value is inside the matching expected item. | " + WF + " P7 (T1 I11, T2 I3) | → scripter, revision-planner, script-checker |",
]
R.insert(1, r60)
r74 = row_line("R74")
sub(r74 + "\n", r74 + "\n" + "\n".join(R) + "\n")

# ---- O table ----
sub("| O34 | check_task.py on a partial Revit index WARNs on nearly every MEP/annotation id (UNVERIFIABLE); the per-task scout baseline is what confirms them. Have the scout baseline every target id, or index MEP categories.",
    "| O34 | check_task.py on a partial Revit index WARNs on nearly every MEP/annotation id (UNVERIFIABLE); the per-task scout baseline is what confirms them. Have the scout baseline every target id, or index MEP categories. An index from ANOTHER document (WF ran against 25-076's cache) is not evidence: check_task refuses it and the first scout of a run rebuilds it (WF retro P3).")
sub("Run folder renamed 6and7; fix the guard's path quoting in a tooling session. Crop guard also only accepts C:/Users/Jeremy paths. |",
    "Run folder renamed 6and7. FIXED 2026-11-17: '&' is allowed inside a double-quoted argument (unquoted '&' stays blocked). |")
sub("With C:/Users/j paths validate.py and visual_crop.py are refused (T3 verify ran twice). |",
    "With C:/Users/j paths validate.py and visual_crop.py are refused (T3 verify ran twice). FIXED 2026-11-17: hooks/paths.py accepts both the home folder and its junction aliases (path_re, norm). |")
O = [
    "| O37 | Before the first Revit call, confirm the bridge is on the intended document: the first scout call returns Document.Title + Application.VersionNumber and the orchestrator compares them with project.json. Mismatch or hang: check every %APPDATA%/Autodesk/Revit/Addins/<year>/revit_mcp_plugin/mcp-port.txt against the running Revit's port and rename stale ones (readPortFromFile takes the highest year first; a stale 2026 file beat the live 2024). | " + WF + " P1 | → orchestrator, revit-scout, revit-conventions; tooling: plugin port pick |",
    "| O38 | When the permission mode changes (auto mode on/off), never resume (SendMessage) a writer launched under the old mode: spawn a fresh writer with the same task, script and run id (a resumed writer kept the old classifier behaviour). Extends A19. | " + WF + " P6 | → orchestrator |",
]
sub(row_line("O36") + "\n", row_line("O36") + "\n" + "\n".join(O) + "\n")

# ---- remove the moved free-form notes ----
i = s.index("## 2026-11-11 25-076 sheet-text batch")
j = s.index("- R66 (26036 CFA): size every leg")
j = s.index("\n", j) + 1
s = s[:i] + s[j:]

s = s.rstrip("\n") + """

## 2026-11-17 roll-in: 23-073 Blocks 6&7, 25-076, 26036 CFA, 25-061 WF (Jeremy: "implement the latest lessons learned and push")
- Tables repaired: R60 rejoined the R table; the J table had been split by '---' (J10-J12 twice): 25-076 rows are now
  J13 (parallel work), J14 (no condensate pump), J15 (final letter, no 'pending').
- Free-form notes moved into the R table: 25-076 R61/R62 kept; 26036 CFA R59 kept, CFA R60-R66 -> R75-R81.
- New from retro/20261116-1839.md (WF-R1-T1/T2): O37 (bridge on the right document / stale mcp-port.txt), O38 (fresh
  writer after a permission-mode change), O34 extended (index from another document), R82 (model -> sheet mapping,
  library/revit/model-to-sheet.cs), R83 (scout 'not found'), R84 (cloud on schedule rows, tags vs borders), R85
  (expected.json from the scout baseline, fallbacks covered).
- Hooks: O32 ('&' in quoted args) and O33 (home junction alias) fixed in bash_allowlist_guard.py / paths.py.
- Agents: scripter, revit-writer, revit-scout, verifier (gen_agents.py); revision-planner, response-writer final mode
  (gen_agents_p67.py); visual-verifier, script-debugger (gen_agents_upgrade.py); script-checker (gen_agents_cscheck.py).
  revit-conventions SKILL.md + scripting-lessons; MARKUP_SWARM_PROMPT run protocol 20.
- Open tooling: revit-mcp readPortFromFile should prefer the live Revit's port file and expose revit_instance to
  scouts (O37); bridge-side load-by-path for long scripts (R70/R72); check_task.py document check (O34).
"""
open(p, "w", encoding="utf-8", newline="").write(s)
print("lessons ok")
