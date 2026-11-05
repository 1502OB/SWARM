# Scripting lessons (AutoCAD)

Moved here 2026-11-05 from the scripter prompt (build/gen_agents.py) so the guardrails live in the skill (AU CS2138).
Read before writing or fixing a script. New lessons go here, plus a one-line pointer in swarm/LESSONS.md.

## AutoCAD lessons (Northgate 23-082; swarm/LESSONS.md)
- Keep every call the writer makes short (under ~600 characters): put the logic in the .lsp, expose short
  (c:...) commands. Count parens before you finish: an unbalanced file fails to load.
- Target documents by name and make sure the target is the active document before ActiveX work (a plot-device change
  on an inactive document hung AutoCAD). Never loop over all open documents in one call.
- Snaps: Jeremy runs OSMODE 16383. Build geometry with entmake/ActiveX, or pass points with "_non" (or OSMODE 0,
  restored after). Bring new wipeouts to the top of the draw order over PDF underlays.
- Plotting: -PLOT, PlotToFile and the bridge's plot_pdf fail (Northgate). -PUBLISH of a DSD sheet list WORKS
  (22-001, 2026-11-03): (command "_.-PUBLISH" dsd) with BACKGROUNDPLOT 0, FILEDIA 0, CMDDIA 0 (restore all three),
  DSD PromptForDwfName=FALSE, Setup= (layout page setup), output in [Target] DWF=/OUT=. Refuse to overwrite an
  existing output. Template: 22-001 pc9-check/cad/pc9publish.lsp + PC9_replot.dsd. Default the output to a test
  folder under the swarm run (QA it before anything goes to L:; local output also avoids the shared-drive
  permission block). An open drawing with unsaved edits publishes in its current in-memory state, no prompt.
- AutoLISP is not Common Lisp: there is no fboundp or identity (use (type sym), (lambda (x) x)). MTEXT has no
  ActiveX Explode (vla-explode fails silently): explode a temp copy with (command "_.EXPLODE" e), clear
  CMDACTIVE, and entdel only enames whose entget is non-nil (entdel on a deleted ename restores it).
- Cloud geometry: find, check and size clouds by their VERTEX bbox (min/max of group 10). vla-GetBoundingBox adds
  the arc bulge (~0.03-0.04 in at chord 0.14, bulge 0.52) and made a position check abort (22-001 P3FIX). Keep the
  arcs clear of neighbouring text: sagitta = |bulge| x chord / 2.
- Embedded Excel (OLE2FRAME) shows a FIXED window of the workbook. Rows written below it are clipped and the
  frame does not grow (22-001 sheet index). Plan row height/font changes or a frame resize, and test-plot.
- Annotative MLEADERs and text keep one position per annotation scale. A move must fix every scale context (set
  CANNOTATIONSCALE to each, then set the vertices), not only the current one; the CLEAN command must restore every
  context too, and expected.json needs an item per scale.
- VPLAYER "_All" freezes in every viewport in the DWG; use "_Select" with the viewport ename. Per-viewport frozen
  layers are in the viewport's ACAD xdata (1003 codes), not DXF 341.
- Title blocks are not always at 0,0: place title-block text relative to each layout's title block insertion point.
- Revision clouds: copy an existing cloud, delta and MTEXT and move them. If one must be drawn, go clockwise
  (BL > TL > TR > BR) and size it from measured geometry, never an estimate.
- Placing tags/text: try candidate positions and abort that item (not the batch) on a clash with text or a pipe;
  return the aborted items instead of forcing an overlap.
- A changed label that has an iso copy (separate iso text layer) must change in both places; add an expected.json
  item for each copy.
- Rerouting a branch to another system: also delete the old wye/stub where it joined the old main.
