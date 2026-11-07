---
name: acad-conventions
description: Firm-specific AutoCAD rules for this office's drawings (snaps, clouds and delta tags, publishing, embedded Excel calcs, verification). Use together with, and after, the autocad-mcp-drafting skill, which covers the bridge transport; this skill covers what the firm requires of the result.
---

# AutoCAD conventions

## Contract
- **Never:** save-as, purge, close or quit from a script; open a drawing (acad-ops does that, from the approved
  list); plot with -PLOT, PlotToFile or the bridge's plot_pdf (they fail here: publish with -PUBLISH of a DSD, see
  swarm tools/swarm_publish.lsp); publish or copy straight to L: (publish to a local folder first); leave OSMODE,
  FILEDIA, CMDDIA or BACKGROUNDPLOT changed.
- **Irreversible, and how it is softened:** every RUN command has a matching CLEAN command that removes exactly what
  it created (by handle) and is wrapped in one UNDO group; a published PDF or reissued set is a new dated file,
  never an overwrite; a drawing is saved in place only after a verifier PASS and only when the task names it.
- **Failure looks like:** a `ROLLED_BACK` / `ABORT` / `ERROR` return; "malformed list on input" (payload too long:
  keep execute_lisp calls under ~600 characters); a TIMEOUT (AutoCAD may still be working: run the dialog check,
  never re-run blindly; a publish takes ~13 s and always times out, confirm it in PlotandPublishLog.CSV); DBMOD rising
  in a read-only step; counts that differ from expected.json.
- **Done when:** the RUN command returned `done=<n> skipped=<n> failed=0` with handles; a re-query of the entities
  (not the return string) confirms every expected.json item; clouds are checked by their VERTEX bbox (group 10), not
  vla-GetBoundingBox; annotative objects are right at every annotation scale; the test plot shows the change.

## Rules
- Load and use the shared library `C:/Users/Jeremy/swarm/library/acad/swarm_lib.lsp` (swl: helpers: one-line results
  swl:result, boxes, crossing-aware clash, viewport projection, clockwise clouds, live-value preflight, tag spots,
  swl:queue-command for attach/xref/publish). Never redefine swl: functions; prefix your own with the task id.
- Every script defines -PLAN (read-only dry run), -RUN, -CLEAN, -CHECK. Before it goes to a writer: lsp_lint 0 errors,
  lsp_dryrun on the snapshot copy "clean" or "needs_full_autocad" (Core Console has no ActiveX).
- Write a .lsp file, then load it. One batched script per task.
- Paper space vs model space: confirm the active space and any viewport twist before placing geometry.
- Verify by re-querying entities after every batch; never assume entmake succeeded.
- Before writing or fixing any .lsp, read `references/scripting-lessons.md` (payload size, snaps OSMODE 16383,
  publishing, AutoLISP is not Common Lisp, cloud vertex bbox, annotative scales, OLE frame window, title blocks not
  at 0,0, VPLAYER _Select, iso copies). New AutoCAD lessons go there.
