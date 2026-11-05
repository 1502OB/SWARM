# QAQC #<N> Sheet Review Brief: <Project name>, <project no.>

<!-- Orchestrator: copy this to <run>/REVIEW_BRIEF.md and fill every <...>. Delete these comments. Ask Jeremy to
     confirm the code basis, issue purpose, scope, and "already decided" list before launching reviewers.
     Template from LESSONS.md O8-O10, Q10-Q15, J1-J4 (2026-11-02). -->

**Project:** <name, address>. Job <Egnyte job number>; Egnyte `/Shared/Projects/<year>/<folder>`; swarm folder `swarm/<folder>`. Client <...>; architect <...>. Plumbing by 120 Degreez Engineering. Model/DWG "<...>".
**Set:** plotted <date time>, title block "<status text>". <n> sheets, <W>x<H> pt: <SHEET title · SHEET title · ...>.
**Issue purpose:** <internal QAQC | bid set | city plan check submittal | permit set>. Seal, stamp and signature
findings depend on this: for internal or bid plots an unsealed interim stamp is normal.
**Output:** feeds a marked-up PDF (clouds + callouts) and a change list, so every item needs a location rectangle.

## Scope
<!-- State holds and limits exactly: what is out of scope, and what is still in scope next to it.
     "Level 3 on hold" made scouts skip in-scope items (Garden NL:20). -->
- In scope: <...>
- Out of scope / on hold: <exactly what, and what is NOT affected by the hold>
- Set coverage: this set has <...>. Missing sheet numbers are NOT errors by themselves. Flag a cross-reference only
  when a callout, sheet index or note points to a sheet that is not in the set (set-wide, once).

## Already decided (do not flag)
<!-- Jeremy's decisions and firm conventions. Keep the firm lines; add project decisions. -->
- Title block content (project no., stamp, seal, permit no., issue fields) is handled by Jeremy: no title-block items
  unless the brief asks for them.
- Arch link annotations stay off on plumbing views.
- Fixture tags and DFU are read on the enlarged plans only; tag overlaps on the overall plan are not findings.
- Stack/riser numbering: unique stacks get unique numbers; duplicated (typical) stacks share a number.
- <project decision, with date>

## Inputs (read-only; never modify)
- Sheet PDFs: `<run>\src\<SHEET>.pdf` (one page each)
- Renders: `<run>\renders\<SHEET>\overview.png` + `tile_r{0,1}c{0,1,2}.png` (2x3, r0 = top, c0 = left); `tiles.json`
  gives each tile's rect in PDF points.
- Text: `<run>\text\<SHEET>.txt`. <!-- Probe first (LESSONS.md O10): compare line counts of a plan sheet and a blank
  sheet and grep for a known tag. Then write ONE of these two lines: -->
  Drawing text IS extractable: grep it for tags and cross-references.
  Drawing text is NOT extractable (probe: <what you checked>): read the images; don't conclude something is missing
  because grep can't find it.
- Zoom / locate: `python C:\Users\Jeremy\swarm\tools\sheet_tool.py zoom|grid|check <sheet pdf> x0,y0,x1,y1 <out_png>`.
  All outputs go in your own scratch folder `<run>\scratch\<agent label>\`.

## Code basis
<!-- code-reviewer stops if this section is empty. Name: edition(s) and local amendments (e.g. 2019 CPC / 2020 LAPC),
     jurisdiction (for seismic gas shutoff etc.), design values, the sizing tables the calcs use, and any code
     reference excerpts in swarm/codes/ to read. Items the city already cleared go under "already decided". -->
<!-- Name the edition and the exact tables, with the design values (rainfall rate, slope) where known. A wrong
     table cited in the brief was repeated by reviewers and the checker (Retreat P2.52-02). -->
- <2025 CPC, T24, CALGreen ...>; storm: rainfall rate <in/h, source>; sizing tables <vertical / horizontal>.
- Any sizing number in a finding names its table and basis (rate, slope). If unsure, give the issue without the number.

## Checklist
1. Completeness: every fixture/drain connected and sized; drains and overflows sized with flow arrows and discharge
   points; POCs with sizes; risers and stacks tagged; backflow/meter/shutoff shown.
2. Code: per the code basis above.
3. Coordination: callouts, detail/section references, match lines, key plan, sheet index vs actual sheets, legend
   symbols vs symbols used (and no new symbol duplicating an existing legend symbol).
4. Calcs and schedules: fixture schedule vs plans, WSFU/DFU/storm calcs, pipe sizes consistent across sheets.
5. Drafting: text overlaps, clipped text, leftover commentary, placeholders ("XX", "TBD", "?", "Project Name",
   "Approver"), template leftovers (another project's logos or names), wrong sheet titles, title-block consistency,
   and overlaps/readability of newly placed tags.
6. Anything a city plan checker would mark.

Quote what you see. Give causes only as hypotheses. If unsure, confidence "low". Report set-wide issues once, on the
first sheet of your pair, with "set_wide": true.

<!-- RE-REVIEW ROUNDS ONLY (LESSONS.md O8). Run two roles with the prior change list as input:
     PRIOR (sonnet): only re-check the prior open items for its sheets; status resolved | partially_resolved |
       unresolved | cannot_verify, with evidence. No new items.
     NEW (opus): only new items; read the prior list for its sheets first and skip anything already listed.
     Add to each item: "source": "prior|new", "prior_id", "prior_status". -->

## Location rect
For every item give `rect: [x0,y0,x1,y1]` in PDF points tightly around the issue (min ~40 pt a side), and
`rect_quality: "exact"` only if you confirmed it with `sheet_tool.py check`, else `"approximate"`.

## How to revise
For every item give `revise_how` (Revit/AutoCAD steps: view, element/type, parameter or cell, value) and `who`:
`claude-script` | `manual-revit` | `engineering-decision` (state the question) | `external` (architect/civil) |
`jeremy-titleblock`.

## Output
Write `<run>\reviews\<SHEET>.json`:
```json
{"sheet": "<SHEET>", "title": "...", "summary": "1-2 sentences on readiness",
 "items": [
  {"id": "<SHEET>-01", "location": "tile_r1c0 near ...", "rect": [0,0,0,0], "rect_quality": "exact|approximate",
   "category": "completeness|code|coordination|calcs|drafting|other",
   "priority": "must-fix|should-fix|nice-to-have",
   "issue": "...", "evidence": "quoted text / what you see", "change_needed": "...",
   "markup_text": "short callout text for the PDF markup (<= 25 words)",
   "revise_how": "...", "who": "...", "depends_on": null, "cross_ref": null, "set_wide": false,
   "confidence": "high|medium|low"}
 ]}
```
Write only your own review files. Reply as your agent definition says.
