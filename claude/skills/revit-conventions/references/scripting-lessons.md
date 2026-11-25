# Scripting lessons (Revit)

Moved here 2026-11-05 from the scripter prompt (build/gen_agents.py) so the guardrails live in the skill (AU CS2138).
Read before writing or fixing a script. New lessons go here, plus a one-line pointer in swarm/LESSONS.md.

## Compile and API facts (Bakersfield 26032, 2026-11-02)
- Known API facts: TransformWithBoundary has GetModelToProjectionTransform() (no GetTransform()); for
  NewTransitionFitting pass the PIPE connector first (a family connector first fails "owner should be flex duct or
  pipe"); a plumb branch off a 1/4 in/ft sloped pipe fails NewTeeFitting/NewTakeoffFitting angle checks: place the
  tee family manually, BreakCurve, ConnectTo; ElementId(long) / .Value on Revit 2024.
- Never hard-code a level, view or type NAME copied from another model (Pad 10's "PLUMBING - WV - FLOOR PLAN" is
  not in Pad 11): resolve levels by elevation and types by family + type name found in THIS model, and abort in
  preflight if not found. Firm-template system types can carry odd classifications (storm types classed
  SupplyHydronic): match system types by name as well as classification.
- Safety guards that compare before/after counts must exclude the ids the task deliberately deletes.
- Keep every return under ~3 KB: the bridge truncates longer returns and the writer loses the tail. Put long lists
  as counts plus first N ids.
- FormattedText has no Replace(): edit TextNote text with GetFormattedText(), find the index, then
  SetPlainText(new TextRange(start, len), newText) and SetFormattedText (keeps the formatting).
- New elements land on the ACTIVE workset. In the creating script set ELEM_PARTITION_PARAM on every new element to
  the workset of the elements it extends (storm piping = STORM), resolved by name in THIS model. Preflight checks each
  target view's range (cut/bottom/depth vs the new elements' elevations) and the workset's visibility in that view,
  and expected.json gets one "visible in view <name>" item per target view, not just "exists" (R22).

## Pad 11 lessons (Bakersfield 26032, 2026-11-02; swarm/LESSONS.md R23-R27)
- Rotating a plan view: setting View.CropBox with a rotated transform does NOT rotate the view (RightDirection stays
  +X, and the script deleted and recreated the clouds anyway). Rotate the crop region element (category Element,
  same name as the view; verify it in preflight) with ElementTransformUtils.RotateElement about a vertical axis
  through the crop center, with CropBoxVisible temporarily on. Keep the viewport box centre on the sheet unchanged.
  Before rotating, record each plan cloud's corners in MODEL coordinates; after, map them back to sheet coordinates
  and recreate each cloud clockwise with a new delta tag. expected.json checks RightDirection.
- After a 180 deg view rotation, check every keynote diamond and tag reads upright: diamonds need HandOrientation
  -X in a 180-rotated view (a "6" read as "9"). Report and flip the +X ones in the same batch.
- Revit revision cloud + delta tag (Jeremy standard): one "Revision Tag" per cloud, head at (cloud right edge
  - 0.17 in, cloud top + 0.08 in) paper, so the tag's bottom touches the top line outside the cloud. A new cloud and
  its tag box must not touch or overlap another cloud and must not cut a text line or a tag: grow it to enclose the
  text, or merge touching clouds into ONE rectilinear outline cloud with one delta (T10 "outline"). Cloud only
  changed items; use the revision named in the task.
- Elements returned by Connector.Owner become invalid after a delete in the same transaction: keep ElementIds and
  re-fetch with doc.GetElement. The API has no spot slope (manual list). Spot elevations work:
  NewSpotElevation with Reference.ParseFromStableRepresentation(doc, pipe.UniqueId + ":9:LINEAR"), then set
  "Display Elevations" = Bottom.
- Moving a tag/keynote/note off a clash: try both axes and take the smallest move that clears everything, and cap
  the move (default 6 ft model / 1 in paper); over the cap, abort that item and report it (T10b chose a 13 ft move
  along the long axis when 1.6 ft up was clear).
- Section/elevation heads whose view is on no sheet: hide them in the plumbing sheet views (they print as blank
  heads). Never delete the section.

## Revit lessons (Garden 25-088; swarm/LESSONS.md)
- Creating tags with leaders runs at about 1 tag/s; more than ~100 tags in one call passes the 300 s bridge timeout
  (the transaction still commits). If a task would create more, say so in your reply so it is split (2-3 views each).
- Tags placed in a dependent view belong to its parent and show in every dependent: tag through one view per parent
  and dedupe by distance (<= 2 ft), never by rounded coordinates (rounding to 1 ft made 131 duplicates).
- Locate targets by their exact current text/value, check the old value before changing it, and throw (rolling back
  the whole batch) on any mismatch. Never address schedule rows or cells by index: rows shift.
- The API cannot retarget view references or match lines, edit tag label formats, or set general Project Units
  accuracy other than 1E-06. Do not attempt these; reply "manual: <what>" so they go on Jeremy's worklist.
- Before any model-wide change (project units, a system type, a shared type), the task must cite a read-only side
  effect scan (0-decimal units would have misreported fractional WSFU 0.75/1.125/2.5 in 185 schedules). Stacked or
  odd tag labels usually come from the tag type's Yes/No label switches: fix by type parameter, not a new family.
- "Computed" values (DFU/FU, flow) settle only after commit. Never put a count or value the script computed inside
  its own transaction into expected.json; write the check and let the verifier read the settled value.
- Export/print by sheet number, never by sheet name (duplicate names overwrote each other's PDFs).

## Pacific Edge 24-071 lessons (2026-11-04..10; swarm/LESSONS.md R30-R46)
Bridge and script shape
- The script is the BODY of a method: no `class` declarations (CS1513 "} expected" / "Type or namespace definition ...
  expected"). Local functions and lambdas are fine. Interfaces that need an implementing class (IFailuresPreprocessor)
  cannot be written. IFamilyLoadOptions: use the Reflection.Emit helper in scripts/TB-T1.cs; writer_guard allows only
  `typeof(IFamilyLoadOptions).GetMethod("OnFamilyFound"|"OnSharedFamilyFound")` and blocks all other reflection (R40).
- The bridge hides thrown messages ("Exception has been thrown by the target of an invocation"). When a check fails
  BEFORE any change, `return "FAILED <task>: <reason>"`; throw only to roll back after changes, and put the reason in
  the message anyway. Wrap each risky call (Delete, NewFamilyInstance, NewTeeFitting) in try/catch and return
  ex.GetType().Name + ex.Message (R31).
- Plumbing types need full names in some bridge builds: `Autodesk.Revit.DB.Plumbing.Pipe`, `.PipeType`,
  `.PipingSystemType`, `.PlumbingUtils` (a `using` line is not allowed).
- EditFamily, LoadFamily and Document.Export(PDF) need transactionMode "none" (the project must not be modifiable);
  open your own Transaction inside the family document (R41).
Matching and editing
- Text matching: TextNote.Text contains "\r" line breaks; normalise with
  `Regex.Replace(t, @"\s+", " ").Trim().ToUpperInvariant()` before comparing (R30).
- Pinned elements throw on Delete: `if (e.Pinned) e.Pinned = false;` in the same transaction, then delete (R32).
- Workshared cloud model: an element borrowed by another user (WorksharingUtils.GetCheckoutStatus ==
  OwnedByOtherUser, e.g. Project Information owned by the architect) makes Revit raise an editing-request DIALOG that
  blocks the bridge until someone answers it (300 s timeout, every later call times out). The dryrun must check the
  checkout status of every element it will modify and RETURN FAILED with the owner name (R42).
Elevations and placement
- Level.Elevation can be survey-based (Elevation Base = Survey Point): in PLMB-24220-R25, Elevation 57.66 =
  internal 169.84. Internal Z = Level.ProjectElevation in that case. Never type an elevation; read it from the Level
  and check it against an existing element on that level (R34).
- Work-plane-based families (FamilyPlacementType.WorkPlaneBased): `NewFamilyInstance(sketchPlane.GetPlaneReference(),
  ...)` fails "Failed to create family instance on face"; `NewFamilyInstance(level.GetPlaneReference(), pt,
  XYZ.BasisX, symbol)` works. Check the family's connector classification in the dryrun: the 120D combo roof drain
  connector is SupplyHydronic, so SD pipes cannot connect to it (R35, R46).
- Sloped branch into a vertical riser: NewTeeFitting fails ("angle between connectors"); a second leader ending
  0.2-0.4 ft above an elbow-trimmed riser top cannot be teed either; leave those for a manual drag-join (see R20).
Views, templates, links
- New elements and links can be invisible because their WORKSET is hidden by default and the view uses
  UseGlobalSetting (24-071: A-OCEAN NORTH held the arch link, A-STORM the new storm pipes): set
  `view.SetWorksetVisibility(wsId, WorksetVisibility.Visible)` (template views: on the template) (R33).
- View templates cannot be View.Duplicate'd (CanViewBeDuplicated = false). Make a copy with
  `viewUsingTemplate.CreateViewTemplate()`, rename it, copy `GetNonControlledTemplateParameterIds()` from the
  original, and re-point only the target views. Never edit a template shared with other buildings (R36).
- RevitLinkGraphicsSettings exposes only LinkVisibilityType, LinkedViewId, ViewRange, ObjectStyles, NestedLinks,
  ViewFilterType, ColorFill: no per-category control. "By Linked View" shows whatever the linked view shows (grids
  included). To hide linked grids keep the same LinkedViewId with LinkVisibilityType = Custom and hide OST_Grids in
  the host view (24-071 F-P2.04 test, then 18 views) (R37).
- Match a host plan view to the arch background view by level ProjectElevation (within 1 ft) and building prefix,
  not by level name (arch "SURF - LEVEL 1 (BEACH)" vs host "SURF - BEACH LEVEL"); use "BACKGROUND FLOOR PLAN", not
  the RCP backgrounds (R37).
Schedules and parameters
- ScheduleFilter Contains/BeginsWith work only on string fields; ElementId fields (Level) reject them with "The field
  and filter type cannot be used to filter this ScheduleDefinition"; filters are AND-only. Tag a string field
  (e.g. Comments) or filter Equal on an ElementId (R38).
- Schedule titles: a dark fill with white text prints black-on-dark in grayscale/black-line plots; use a white cell
  with black bold text. Set TableCellStyleOverrideOptions (BackgroundColor, FontColor, Bold = true) before
  SetCellStyle. Column headings live in Body row 0; turn group ShowHeader/ShowFooter off for a one-row-per-type
  count schedule (R39).
- Consultant shared parameters (BIMxP): ParameterUtils.DownloadParameter with "parameters.<guid>:1.0.0" threw
  ArgumentException. Recreate with the architect's exact GUID: temp shared parameter file
  (ExternalDefinitionCreationOptions.GUID), BindingMap.Insert(def, InstanceBinding(Sheets), GroupTypeId.Data), restore
  Application.SharedParametersFilename in finally. Delete a same-name NON-shared project parameter first. A
  linked-model schedule reads sheet parameters by GUID (R43).
Plotting
- MCP batch_export: paper sizes only (no colour, no sheet size), and its replies lag by one call. Find each PDF on
  disk and confirm the sheet number text is on the page. For deliverable sets use Document.Export(folder, sheetIds,
  PDFExportOptions{ColorDepth = GrayScale, PaperFormat = ExportPaperFormat.Default (sheet size), Combine = true,
  FileName, Hide* = true}) (scripts/P71-PDF-T1.cs) and add bookmarks with tools/bookmark_set.py (R44).

## Pacific Edge 24-071 P71 lessons (2026-11-10 afternoon; swarm/LESSONS.md R47-R58, Q27; retro/20261110-p71.md)
Dry runs
- A dry run must reach Commit. transactionMode "none": TransactionGroup.Start(); Transaction.Start(); ...;
  t.Commit(); group.RollBack(). Capture commit-time failures with a lambda on
  document.Application.FailuresProcessing (record GetSeverity, GetDescriptionText, GetFailingElementIds; on errors
  SetProcessingResult(ProceedWithRollBack)); remove the handler in finally. No class is needed. Return
  "commit: Committed|RolledBack failureTotal=<n>" and the failure list in BOTH dry run and real run. A pre-Commit
  dry run passed P71-SAN-T1 v2; the real Commit then failed ("Line is too short", "opposite direction") and Revit
  crashed (R47).
- Stage irreversible deletes/moves (stage A) apart from new geometry (stage B); each stage is idempotent and gets
  its own dry run (parameters ["dryrun","A"|"B"|"AB"]).
- Self-checks before Commit roll back only on hard faults (< 0.1 ft, flipped direction, no fall); "preferred"
  lengths are info. Always build the full report, then roll back (R49).
- Report every property expected.json checks per element (level, workset, symbol, host, Z) and name every
  cascade-deleted id (Document.Delete return) by category + type before the rollback (R50).
- Cosmetic calls are non-fatal: FormatOptions.IsValidAccuracy / UnitUtils.IsValidForSpec first; plain counts use
  UnitTypeId.Fixed (General with Accuracy 1.0 threw and rolled back the whole schedule build, P71-SIDX-T1) (R54).
Piping (extends R20)
- Branch to a sloped main: level stub, perpendicular tee, or a tee family + PlumbingUtils.BreakCurve + ConnectTo.
  Never NewTeeFitting / NewTakeoffFitting from a sloped or vertical pipe ("angle too small or too large"; takeoff:
  "No routing preference for takeoff set for this PipeType"); never tee a vertical into a horizontal header.
- Spacing: >= 2.0 ft between tees, >= 1.5 ft horizontal runs, >= 0.5 ft drops, no X jog shorter than 2x the fitting
  take-off, a diagonal is one straight run. Budget the fall first (1/4 in/ft from the tie up); a leg with no fall is
  reported, not drawn. Pattern: swarm/library/revit/pipe-branch-to-main.cs (from P71-SAN-T1.v6.cs, written clean:
  35 pipes, 26 fittings, 0 failures) (R48).
Elements and ids
- CopyElement keeps the SOURCE element's level/host (a WC copied on BEACH LEVEL landed on PARKING - LEVEL B2): set
  FAMILY_LEVEL_PARAM and restore Z with INSTANCE_ELEVATION_PARAM (scripts/P71-SAN-T2.cs) (R50).
- Return all ids the commit created (before/after id sets over the categories touched), incl. Revit-spawned
  fittings; never "id > N": other users edit the model at the same time (R51).
- Warnings are not counted in the script (before regeneration: 476 -> 476 in-script, 476 -> 505 real). The writer
  calls get_warnings before and after (R52).
- Fully qualify EVERY type in Autodesk.Revit.DB.Plumbing / .Mechanical / .Electrical, enums too:
  Autodesk.Revit.DB.Plumbing.PipeSystemType.Sanitary ("'PipeSystemType' is inaccessible due to its protection
  level"). revit_api_check.py does not test this (R56).
Schedules
- A schedule made in the same session reads blank from TableSectionData.GetCellText until regenerated: check cell
  text and row counts on the sheet PDF (visual-verifier), not via the API (R55).

## 23-073 Blocks 6&7, 25-076, 26036 CFA, 25-061 WF lessons (2026-11-09..16; swarm/LESSONS.md R59-R85, O34, O37)
Returns, size, dry runs
- The bridge truncates returns at ~3 KB: `done=/skipped=/failed=` and cleanup_params first, capped detail after; <= 2.3 KB
  on every path (R59). Writers retype nothing: since 2026-11-25 they send the FILE (send_code_to_revit codeFile +
  expectSha256 from the gate + expectMarker = the line-1 "// <task-id> v<n>"), so length is not a transcription risk
  (R70/R72). Still stage by concern (delete vs create, one dry run each); over ~1500 lines needs a reason. One long
  script per writer (R62).
- SAFETY (R77): a commit-then-rollback dry run PERSISTED view edits (rename, crop, HideElements, viewport
  SetBoxCenter, detail curves; CFA T16a v3); pipes rolled back fine. Check grp.Start()/grp.RollBack() statuses, re-read
  the model after RollBack, return ROLLBACK_FAILED on leftovers. View/sheet/annotation dry runs are read-only.
  Viewport crop/scale/SetBoxCenter can move LabelOffset: restore it.
- Simulate a risky step inside a rolled-back SubTransaction in the dry run; T9a dry runs said GO, real runs failed (R73).
Parameters and names
- Parameter.Set(string) on a generic-annotation 'Note Text' reads back the OLD value until document.Regenerate()
  (CB6-R1-T3 v3 rolled back a good edit). Compare after Regenerate; report Set()==false separately (R63).
- Formula-driven family values (GI 'Holding Capacity') are read-only: change the driving dimension (R68).
- Names exactly: built-in line styles '<Thin Lines>'; LookupParameter is case-sensitive (LENGTH/WIDTH/HEIGHT) (R71).
- Revision tag in IMEG-based models: family 'MEP_Tag_Revision', type 'Revision Tag' (R64).
  RevisionNumberingSequenceId cannot be set to InvalidElementId (throws): skip it (R66).
Annotation placement
- Clash-check against MEP element boxes (pipes: centre line + radius; fittings, accessories, equipment), not only
  text/tags; size clouds from boxes measured after Regenerate; cloud the text only, never the equipment it points to
  (R67). TextNote.get_BoundingBox(view) spans its leader: box leadered notes from Coord/Width/Height/alignment and
  test the leader as segments (R69).
- Model/view point -> sheet = vp.GetBoxCenter() + ((P - cropCentre) . view.Right, (P - cropCentre) . view.Up) /
  view.Scale. GetProjectionToSheetTransform on model points gave ~(147, 209) ft twice (WF-R1-T1 v1, v2). Assert the
  point is inside vp.GetBoxOutline(). Helper: swarm/library/revit/model-to-sheet.cs (R82).
- Viewport.GetLabelOutline can be far off (off-sheet Y) right after a scale/crop change, even after Regenerate (R76).
- Clouds on schedule rows: inset by the arc sagitta; tags clear of in-viewport text, tags and title-block borders (R84).
- FilledRegion.Create is not allowed on a ViewSheet: opaque-background TextNoteType or a scale-1 drafting view (R61).
Pipes, systems, families
- SetSystemType on a pipe connected to another system retypes the WHOLE network (25-076: building drain -> VENT, 0 FU).
  Disconnect, retype the isolated pipes, reconnect, verify both systems (R60). Per-pipe RBS_PIPING_SYSTEM_TYPE_PARAM
  Set returns true and reverts after Regenerate: divide the PipingSystem, retype the system element (R73).
- Deleting a pipe/fitting cascades to its centerline sub-element: class Element, OST_PipeCurvesCenterLine (-2008045) /
  OST_PipeFittingCenterLine (-2008072), not OST_CenterLines; allow exactly these in cascade guards (R75).
- Leg length between fittings = routing-preference take-offs + 0.5 ft (rolled-back probe), never planned C-C (R81).
- Wye-only pipe type: vent off a horizontal drain = 45deg wye rolled up + 1/8 bend (R79); off a sloped 3 in main no
  tee variant works: riser unconnected, tie manual; never move the main (R80).
- Work-plane-based floor drains: NewReferencePlane + GetReference (a SketchPlane reference fails "on face"; a level
  plane can't be moved off the level); P-traps placed by point lie flat: no trap families (R78).
- Hosted families ignore MoveElement in Z: use INSTANCE_FREE_HOST_OFFSET_PARAM / Elevation from Level, find the sign
  live, check connector Z after Regenerate (R74).
Scouting and bridge
- A scout "not found" lists its exact search; schedules: Header, Body and Footer sections; text: every TextNote on every
  sheet and every view on the named sheets (R83). An index from another document is not evidence (O34).
- The bridge picked a stale mcp-port.txt (2026 file, 8081) over the live Revit 2024 (8080): confirm Document.Title and
  version on the first call (O37). Since 2026-11-25 the patched server skips a port file whose port does not answer
  (revit_instance status shows portFiles / liveFallbackPort); with two live Revits use revit_instance select.

## Village 24-071 VSV lessons (2026-11-19; swarm/LESSONS.md R76/R77/R82/R86-R92, O39; run 20261119-VSV)
Dry runs
- A commit-then-rollback dry run of a view/sheet script persisted AGAIN (VSV-P1-T1 attempt 3: title block types, sheet
  names, key plans, a deleted TB and viewport centres stayed, though the return said "group rolled back"); the ENL dry
  runs rolled back. View/sheet/viewport/name edits: read-only dry run. Writers probe read-only after EVERY dry run
  (counts of created things, one changed value per kind) and stop on any leftover (R77/R86).
Cross-document sheet placement (arch sheet -> plumbing sheet)
- The viewport box centre is not a match: the box includes annotation crop and annotations outside the crop, so its
  size differs between documents (S-P1.01 was 43 in off with annotation crop off).
- Align by a model point: P = source view crop centre (world XY);
  delta = srcVp.GetProjectionToSheetTransform().OfPoint(P) - tgtVp.GetProjectionToSheetTransform().OfPoint(P), sheet XY;
  tgtVp.SetBoxCenter(current + delta) keeping Z; Regenerate; read back <= 0.01 in (fatal > 0.05). Verified 0.0005 in on
  15 + 14 sheets, plots matched (R87).
- Reconciling with R82: GetProjectionToSheetTransform takes projection-space points; an ABSOLUTE sheet position from a
  raw model point was nonsense on 25-061 WF (~147, 209 ft): use library/revit/model-to-sheet.cs for those. The
  DIFFERENCE of the same model point through two viewports is valid when both are unrotated plan views at the same
  scale and the link transform is identity (check all three; any projection-space offset cancels).
- Compare points across documents in XY only: GetBoxCenter Z differs between documents (attempts 1-2 rolled back on a
  3D DistanceTo of equal XY points).
- View titles: after alignment set LabelLineLength = source, copy LabelOffset, then correct LabelOffset by the
  sheet-XY difference of the GetLabelOutline() lower-left corners (up to 3 passes). LabelOffset is relative to the
  box, so a plain copy lands wrong (R76).
Templates, types, source model
- A copied view template used by views of several scales must list VIEW_SCALE, VIEW_SCALE_PULLDOWN_IMPERIAL and
  VIEW_SCALE_PULLDOWN_METRIC in SetNonControlledTemplateParameterIds; set View.Scale AFTER assigning the template and
  read it back (B-P6.21 restroom came out 1:48 instead of 1:24) (R90).
- Viewport types (and their title family) missing in the target: ElementTransformUtils.CopyElements from the source
  document in preflight, after a name pre-check (an existing name raises a duplicate-types dialog) (R91).
- Reading the arch model: when the detached arch document is not open, fall back to the loaded link
  (RevitLinkInstance.GetLinkDocument), after checking the link transform is identity; report which was used (R92;
  written into VSV-ENL-T1.cs attempt 6, ran from the link).
Exports, warnings, bridge
- Visual PDFs: a read-only Document.Export script (scripts/VSV-ENL-PDF.cs: PDFExportOptions per sheet, Combine, file
  named by sheet number with '&'/spaces replaced, File.Exists + size in the return), run alone. batch_export run twice
  at once wrote nothing, then stuck on a stale reply, and Revit crashed while a verifier queried in parallel (O39, R65).
- get_warnings caps at 500: equal counts at the cap prove nothing; compare ids/descriptions (R52).
- After a send_code timeout replies can lag one call: put a marker in the first return token and check it; confirm
  state with Document.IsModified or an id count (R88). Scope queries to the task's ids: a model-wide callout-parent
  scan timed out and jammed the queue for later calls (R89).
- Revit's 'Changes Not Synchronized with Central' reminder blocks the bridge mid-run; Cancel lets it continue (O23).

## Erewhon 26021 ECM lessons (2026-11-20; swarm/LESSONS.md R37/R52/R58/R66/R67/R75/R93-R99, O39; run 20261117-ECM)
Dry runs for layout and sheet changes
- Own-Transaction pattern (transactionMode none): one Transaction; the dry run performs everything, measures
  (positions, extents, overlaps, margins), then Transaction.RollBack() and checks the returned status. It rolled back
  cleanly on every ECM run and is the only real test for layout/sheet changes. Never Commit inside a TransactionGroup
  and then group.RollBack() (R86). The writer still probes after the dry run (R77). revit_cs_check's L5
  'dryrun-order' warning fires on this pattern: a known false positive (R99).
Sheet text and schedules
- Anchor notes to a NAMED ScheduleSheetInstance / element id from a scout, never a guessed table title: on P001 the
  'FIXTURE UNIT REFERENCE' table lived inside one 'Sticky-P0.5 Design Criteria' schedule. Drawing-area edges come from
  the exported PDF's border line (measure it), not the title-block bbox plus a guess (the B01 note crossed the thick
  left border). Keep >= 0.02 ft margin to every limit in layout checks. A heading needs a bold 1/8 in text type:
  create it when absent (R93).
- Sticky (text) schedules hold their text in HEADER section cells; get_schedule_data reads the body (blank). Edit with
  schedule.GetTableData().GetSectionData(SectionType.Header).SetCellText(r, c, text). Before splitting text across
  cells, check which cells print (P000: r5c1 printed, a twin schedule's r4c1 did not). A range delete removes the whole
  table body, titles AND their rows; verify on the export (R96).
- Schedule width is driven by the BODY columns: when trimming header columns, scale the body column widths to the
  kept header width. Header row heights do not auto-fit (a small SetRowHeight stays small and clips): compute from text
  length / column width (~10 chars/in at 3/32 in text, 0.17 in per line + 0.08 in) and only ever shrink (R97).
- Moving content between sheets: ScheduleSheetInstance.Create on the target sheet, then delete the old instance; text
  notes one at a time with ElementTransformUtils.CopyElements(srcView, ids, dstView, Transform.Identity, opts) + delete
  the original. Combine/renumber sheets in ONE own-Transaction script with a full dry run that rolls back (R98;
  scripts/ECM-CMB-T1.cs).
Revisions, links, matching
- New revision on a no-delta issue: copy RevisionNumberingSequenceId from an existing row (its value was
  InvalidElementId; copying it worked and blanked the '1'), never construct it (R66; scripts/ECM-D1-T2.cs).
- RevitLinkGraphicsSettings.LinkedViewId lives in link.GetLinkDocument(); host document.GetElement returns null and the
  .Name call throws a bare 'target of an invocation' (R37).
- Cleanouts: name contains "CO" or "CLEANOUT", case-insensitive (storm COs are 'FloorCleanout_Round1 | TWO-WAY').
  'West/east stubs' map by scout XY, never by network numbering (R94).
- Annotation obstacles: pipe centrelines buffered by OD/2 + 1/16 in paper, fixtures/accessories and all view
  annotations (R67).
Pipe resize (scripts/ECM-GAS-T1.v6.cs, 6 attempts)
- Explicit element list from the scout; no connector walk through FamilyInstances (v1 escaped through gas meters and
  tenant boxes into other networks).
- Connector.Radius only when Shape == ConnectorProfileType.Round && Domain == Domain.DomainPiping (v2 threw).
- Resizing pipes does not resize generic fittings: set their writable 'Nominal Radius' (Double; any spec, not only
  Length: v4 skipped it); recreate transitions; delete redundant ones and ConnectTo (v3, v6).
- Deleting a fitting cascades to its own centerline sub-element (OST_PipeFittingCenterLine): benign (R75; v5's gate
  rejected it). Compare connector snapshots as unordered sets (v5 compared in connector order) (R95).
Exports, warnings, API checks
- Never batch_export, for checks or sets: it returned the previous path three times on an idle bridge. Document.Export
  + PDFExportOptions, one sheet per call, Combine=true so FileName is honoured (scripts/ECM-D2-T1.cs). Export never
  overwrites (it appends -1): delete or move stale files first and check name + size (O39, R65).
- get_warnings returning exactly 500 is its cap: count with document.GetWarnings().Count (R52).
- revit_api_check.py does not index enum members (BuiltInParameter.*, BuiltInCategory.* report NOT FOUND): grep
  revit-api references/db-b-c.md for " NAME = "; the revit_cs_check compile decides (R58).
