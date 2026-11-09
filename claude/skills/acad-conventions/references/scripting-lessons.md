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
- Plotting: -PLOT, PlotToFile and the bridge's plot_pdf fail (Northgate); on 22-022 -PUBLISH also failed ("Did Not
  Plot") and plotfns.lsp _plot1 worked - see the 22-022 check-plot rule below. -PUBLISH of a DSD sheet list WORKS
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

## 22-022 Carmel lessons (run 20261106-1325, retro/20261106-1325.md + 11-07 follow-up)
- Live values only (P1): every layer, style, handle position and coordinate constant is read from the live entity in
  PLAN and compared with a tolerance (swl:preflight); an ABORT prints the live value. A debugger changes a constant
  only after reading the live DXF itself, never from a claim in its prompt (R8 v5 set layer "PLBG" from a misread).
- Names (P2): AutoLISP symbols are case-insensitive. A setq'd variable must never share a name with a defun
  (R8:VP vs r8:vp) or with a BUILT-IN: an inline query did (setq abs (findfile ...)) and every later (abs x) in that
  drawing failed "bad function: '<path>'" (repair: (defun abs (x) (if (minusp x) (- x) x))). Prefix variables
  TASK:NAME, keep inline execute_lisp queries inside a (defun tmp ( / locals) ...) or with prefixed names.
- Live test (P3): a handle is live only when (entget (handent h)) is non-nil; handent returns erased enames.
- Stage tags (P4): every PLAN/RUN writes stage tags to <task>.stage.txt from version 1 and PLAN returns stages=.
  On an error with no location ("bad argument type: consp T"), add tags and rerun; do not guess a fix.
- Search loops (P5): over ~500 entities, select with a filtered ssget window, prefilter records to the target frame
  once, scan a coarse grid (max(0.25 plot in, range/12)), refine around the best 3, stop at the first score 0, and
  ABORT at a hard cap instead of running past the 10 s bridge wait.
- or/and return T or nil, never the operand (P6): value-or-default is (cond ((expr)) (T default)).
- No (command) under the dispatcher (P8). PDF/xref/image attaches go in a separate <task>.attach.lsp that calls
  (swl:queue-command "<LITERAL command>") - writer_guard refuses a strcat'd or variable argument. Form:
  "_.-PDFATTACH <path with \\\\>\r<page>\r<x>,<y>\r<scale>\r<rot>" with NO trailing \r (the helper adds one; an
  extra one repeats the command). Check the live slot against the literal values first and ABORT on a mismatch.
- Delete (P11): CLEAN and rollback use (vla-Delete obj) and verify vlax-erased-p; entdel returned deleted=3 while
  all 3 stayed live (C22-R1-T2). Count only verified deletions.
- Text that does not plot (P12): TEXT/MTEXT made with vla-AddText/AddMText (S1/S2) and paper-space clouds/tag made
  by entmake/CopyObjects on P012 existed (entget fine, layer on, draw order on top) but never plotted; cause
  unknown. Make new text as vla-Copy of a plotting entity of the same kind in the same space, then set string and
  position. Every task that adds text/markup gets a test plot read back (PDF text/linework search or a crop);
  PASS_API_ONLY never proves it prints.
- Delta layer (P13): N_P_REV2/N_P_REV3 differ per DWG. Never ABORT on a missing delta layer: create N_P_REV3 (copy
  of N_P_REV2 if present, else colour 1, Continuous, plottable, thawed), report layer_created, record it for CLEAN.
- MTEXT size (P14): plan from a temp copy built by the SAME function RUN uses; measure max(DXF 42/43, bbox) (DXF 43
  can be stale right after creation); RUN re-measures and moves, re-plans cloud/tag; only a clash aborts.
- Clash scans (P15) skip non-plot layers (Defpoints, plot flag off); such frames are margins, not obstacles.
- Check plots (P16): set CTAB, force paper space ((vla-put-MSpace doc :vlax-false), CVPORT 1 - P101-W plotted as
  bare model space with a floating viewport active), vla-Regen acAllViewports, make the output folder, _plot1,
  then restore CTAB and BACKGROUNDPLOT (plotfns leaves it 0). A bridge timeout on _plot1 is normal: compare the PDF
  size with a previous good plot instead of replotting blindly. Try -PUBLISH first only where it is known to work.
- CHECK finds its own notes by the handles in created.txt (joining wrapped TEXT lines), never by a text search (P17).
- Embedded Excel (P10): an OLE frame shows ONE sheet of a multi-sheet workbook. With two candidate frames, test-plot
  the layout first and edit the frame whose rendered content shows the target labels (P001: DDCE0 showed the
  Fixture Unit calc; the printed pressure calc was D7966). xl_ole.ps1 formula edits need formula_approval.by (P18).
- PDF pages for highlights: measure the row on the page AS AUTOCAD DISPLAYS IT (render the page; PyMuPDF text
  coordinates can be in a rotated content space). Check with a test plot that the highlight sits on the right row.
- PDF underlays crop: to fill an A&O sheet border with another A&O-format sheet, crop the PDF to the inner border
  (x 1.25..38.75, y 0.5..29.5 in) with a cropbox and attach at (1.25,0.5) scale 1 (P012, Fuscoe C6.0).
