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
