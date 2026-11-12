---
name: revit-conventions
description: Firm rules for changing a Revit model through the revit-mcp bridge (revision clouds and delta tags, grids, batched transactions, run safety). Use before writing, running or verifying any Revit script or MCP write; the exact API members are in the separate revit-api skill.
---

# Revit conventions

## Contract
- **Never:** Save, SynchronizeWithCentral, relinquish or close from a script (only the orchestrator's SYNC A/B
  template); hard-code an ElementId found in another model; delete an element without re-reading its id, owner
  view and text/value first; write without a snapshot for the current run id; put a cloud on an issued revision or
  invent a revision; edit an element another user has borrowed (check WorksharingUtils.GetCheckoutStatus in the
  dryrun: Revit raises an editing-request dialog that blocks the bridge); edit a view template shared with other
  buildings (make a copy); declare a class in a script (the code is a method body); skip the dry run, or write
  geometry/pipes/deletes after a dry run that stopped before Commit (it must commit inside a rolled-back
  TransactionGroup and report failureTotal=0: references/scripting-lessons.md "P71", R47/R54).
- **Irreversible, and how it is softened:** deletes cannot be undone by cleanup, so the restore point is the ACC
  version made by SYNC A (say so in the task's cleanup note); parameter and crop/position edits return the old
  values so a cleanup script can put them back; model-wide or shared-type changes need Jeremy's OK per batch.
- **Failure looks like:** a return starting `ROLLED_BACK`, `NO_GO` or `TIMEOUT`; done/skipped/failed counts that
  differ from expected.json; an element that exists but is not visible in its target view (wrong workset or view
  range); `skipped > 0` when nothing should already be in the target state. Report these; never call them a pass.
  A bare "Exception has been thrown by the target of an invocation" hides the real error: return FAILED strings
  before changes and catch/return ex.Message around risky calls. A trivial call timing out = a Revit dialog is open.
- **Done when:** the script returned `done=<n> skipped=<n> failed=0` with ids; an independent re-query (verifier)
  confirms every expected.json item; clouds are clockwise, tagged once, in the sheet's revision schedule; new
  elements are visible in every target view; the visual check of the exported sheet passed.

## Before writing or fixing a script
- Read `references/scripting-lessons.md` (compile and API facts, naming across models, worksets and view range,
  return size, Pad 11, Garden and Pacific Edge 24-071 lessons: elevation basis, ownership dialogs, templates, link
  display, schedule filters, shared parameters, grayscale PDF export). New Revit lessons go there.
- Check every API member in the `revit-api` skill.

## Revision clouds via the API
- Draw the boundary loop **CLOCKWISE**: bottom-left → top-left → top-right → bottom-right → back to bottom-left.
  Clockwise makes the cloud arcs bulge outward. Counterclockwise produces inverted clouds (arcs pointing in).
- Coordinates are in the view's plane. In a plan or drafting view, "clockwise" means clockwise seen from above
  (signed area of the loop in XY is negative).
- `RevisionCloud.Create(document, view, revisionId, curves)` needs a Revision. Clouds cannot be placed on an
  issued revision. For tests, create a temporary unissued revision and delete it during cleanup. For production
  work, take the target revision (sequence/description) from the task or scout manifest; never invent one.
- Sizing: a TextNote's `get_BoundingBox(view)` came back tiny (~0.08 x 0.02 ft) in testing, i.e. it does not
  reflect the printed text extent. Size cloud boundaries from `TextNote.Width` (paper feet) x `view.Scale`
  plus line count x text size x view.Scale, or from explicit coordinates given in the task. Margin = 1/4" paper
  (0.0208 ft x view.Scale) unless told otherwise.
- The signed-area check proves vertex order, not appearance. Confirm the clockwise rule visually once per
  Revit version; until then the verifier marks "bulges outward" as inferred (pass=null with a note).
- Verifying orientation: read the cloud's sketch curves (`RevisionCloud.GetSketchCurves()`) and compute the
  signed area of the loop from the curve endpoints in order. Negative (clockwise) = arcs bulge outward = pass.
  Report the vertex order you observed.
- Delta tag (Jeremy standard, Pad 11 2026-11-02): every cloud gets one tag of family "Revision Tag", head at
  (cloud right edge - 0.17", cloud top + 0.08") on the sheet, so its bottom touches the cloud's top line from
  outside. Not centred over the cloud.
- A cloud and its tag must not touch or overlap another cloud, and must not cut a text line or tag. Grow the
  cloud to enclose the text, or merge touching clouds into one rectilinear outline cloud with one delta.
  Cloud only the changed items.
- Rotated views: rotate a plan view by rotating its crop region element (setting View.CropBox with a rotated
  transform does not rotate it), and recreate plan clouds from corners recorded in model coordinates.

## Grids in plumbing models
- Grids are **copy/monitored** into the plumbing model, not displayed from the linked arch model.
- Arch link annotations stay **off** on plumbing sheet views. Do not turn on linked-model annotation categories
  (grids, tags, text) in plumbing views to "get grids back"; use the copy/monitored grids.

## Transactions
- Prefer one batched transaction over many single-element calls. Use one `send_code_to_revit` call
  (transactionMode "auto") that does the whole task, rather than a sequence of create_* tool calls.
- Look elements up by query inside the script (names, types, categories), not by hard-coded ElementIds.
- Never Save or SynchronizeWithCentral from a script. One exception: the swarm orchestrator (main session) syncs
  around write batches with the exact template in MARKUP_SWARM_PROMPT.md, which orchestrator_guard.py allows
  (Jeremy, 2026-11-02). Agents never sync.

## Swarm run safety (swarm-upgrade, Jeremy 2026-11-04)
- Every write batch runs inside one transaction group and rolls back entirely on any failure.
- Never run a write without a snapshot for the current run id.
- A revision cloud task is not complete until the revision is in the sheet's revision schedule and the cloud is tagged.
