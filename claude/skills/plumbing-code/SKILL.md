---
name: plumbing-code
description: Extracted, page-indexed plumbing code and local amendment text (CPC editions, LAPC, LADBS bulletins) plus the firm's code-check rubric. Use when checking a code citation or recomputing a sizing figure against a specific code edition, so every citation is quoted from a source file and page.
---

# Plumbing code references

## Contract
- **Never** state a code section, table value or requirement as verified unless it is quoted from a file under
  `references/<edition>/` with its page. Memory is `unverified` (citation_basis `recalled`).
- **Never** pick an edition yourself: it comes from the project's code basis (see references/rubric.md section 1).
- **Irreversible:** none; read-only. New PDFs are added with the script below, which never overwrites a source.
- **Failure looks like:** the edition folder is missing; the section is not in any INDEX.md; the needed table is on
  an image page you could not read. Each of these is `unverified` with the reason, never a guess.
- **Done when:** every item has a status (match / mismatch / unverified) and every match or mismatch has
  `source_file` + `source_page`; counts are reported: `<n> match, <n> mismatch, <n> unverified`.

## How to use
1. Read `references/rubric.md` (what to check, statuses, tolerances, output fields).
2. Open `references/<edition>/SOURCES.md`, then that source's `INDEX.md` (section/table -> pages).
3. Read the page file `pNNN.md`; if it says IMAGE PAGE, read `pNNN.png` (tables scanned as images).
4. Quote the exact words or table row; give file + page.

## Editions indexed (2026-11-05, from L:\Shared\Documents\01_Codes & Standards\Codes and Standard)
- `2025_CPC/`: 2025 California Plumbing Code, 639 pages (67 image pages rendered as PNG).
- `2022_CPC/`: 2022 California Plumbing Code with the 2024 Supplement (searchable), 599 pages.
- `2019_CPC/`: 2019 California Plumbing Code, 555 pages (2020-cycle jobs, e.g. 22-001 Olympic).
- `2019_LAPC/`: 2019 Los Angeles Plumbing Code amendments, 104 pages, SCANNED: every page is a PNG and INDEX.md has
  no section numbers; read the page images (and say so) until it is OCR'd.
- `LADBS_IB/`: IB P/PC 2026-009 Domestic Water Pipe Sizing (LAPC 610.5, 41 pp: meters pp. 5-6, FU -> gpm p. 8,
  permissible friction p. 9, Type L copper charts p. 10 on); Corr. List 031 (2020) Sewage Ejectors and Sump Pumps;
  IB P/PC 2014-012 Graywater Systems.
- `LA_County/`: LA County Sump Pump Design requirements.
- A table appears on every page that mentions it (contents, cross references); the page where the table itself is
  printed is the one whose text starts the table block. Read the candidate pages; cite the printed table page.
- Not indexed: UPC 2021, IPC, 2016/2015 codes, other cities. Add a PDF with:
  `python ~/.claude/skills/plumbing-code/scripts/code_index.py "<code.pdf>" <edition-folder> --label "<name>"`
