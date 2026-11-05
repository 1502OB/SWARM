# Code-check rubric (version 1, 2026-11-05)

Versioned separately from the code text (AU CS2138: the rubric changes on its own schedule; the code folders only
hold extracted text). Used by code-checker (calc figures + citations) and code-reviewer (sheet compliance).

## 1. Pick the edition
- Take the code basis from the run's REVIEW_BRIEF.md "Code basis", else project.json, else the correction sheet's
  stated cycle. Record where it came from. No stated basis = every item `unverified`, then stop (never assume the
  newest edition).
- The edition folder must exist under `references/` (e.g. `2022_CPC/`, `2023_LAPC/`, `LADBS_IB/`). A local
  amendment or bulletin wins over the model code for the same section; cite both.

## 2. What to check
- Citations: the cited section/table exists in that edition and says what the sheet or letter claims.
- Figures: recompute from the stated inputs with the edition's own table (fixture units -> gpm, friction loss per
  100 ft, developed length, DFU -> pipe size, storm rate -> leader size, gas CFH -> size). Show the arithmetic.
- Do not reuse calc-auditor's or the designer's arithmetic; do your own from the table.

## 3. Status per item (exactly one)
- `match`: found in references, and your value equals theirs within the tolerance below.
- `mismatch`: found in references, and the value or wording differs. Quote both.
- `unverified`: not in references (memory only), table is an image you could not read, or no code basis.

## 4. Tolerances
- Pipe size: exact (next size up is a mismatch). Flow (gpm): +/- 1 gpm or 2 %, whichever is larger.
- Pressure loss / friction: +/- 0.5 psi or 3 %. Fixture or drainage units: exact. Interpolation allowed only where
  the table says so (e.g. LADBS IB demand-load tables "may be interpolated").

## 5. Output fields
`{"id", "figure_or_citation", "status", "your_value", "their_value", "section", "edition", "source_file",
"source_page", "arithmetic", "note"}`. `source_file` + `source_page` are required for match and mismatch.
code-reviewer keeps its `citation_basis`: `reference` (found here) | `brief` | `recalled` (never "high" confidence).
