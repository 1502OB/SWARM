"""Extract and index a code / standards PDF into this skill (Road Design Check pattern: extracted + indexed standards).

    python C:/Users/Jeremy/.claude/skills/plumbing-code/scripts/code_index.py "<code.pdf>" <edition-folder> [--label "<short source name>"]

<edition-folder> names the code cycle the PDF belongs to, e.g. 2022_CPC, 2025_CPC, 2023_LAPC, LADBS_IB.
Writes, under references/<edition-folder>/<pdf-stem>/:
  p001.md ...   verbatim page text, with the page number and the section / table numbers found on that page
  p003.png      a 110 dpi render of any page whose text layer is nearly empty (tables scanned as images)
  INDEX.md      section / table number -> pages, then a per-page list (first heading line, image-only flag)
and appends the source to references/<edition-folder>/SOURCES.md. Refuses to overwrite an existing source folder.
Prints a JSON summary. Uses PyMuPDF (pymupdf) only.
"""
import json
import re
import sys
from pathlib import Path

try:
    import pymupdf as fitz
except ImportError:
    import fitz

REFS = Path(__file__).resolve().parent.parent / "references"
SECTION = re.compile(r"\b(\d{3,4}(?:\.\d{1,3}){1,3})\b")             # 610.5, 702.1, 1101.12.2
TABLE = re.compile(r"\bTABLE\s+([A-Z]?\d{2,4}(?:\.\d{1,3}){0,3}(?:\([0-9A-Za-z]+\))?)", re.IGNORECASE)
APPENDIX = re.compile(r"\bAPPENDIX\s+([A-Z])\b", re.IGNORECASE)
MIN_TEXT = 200   # characters; below this a page is treated as image-only and rendered


def main():
    args = sys.argv[1:]
    label = None
    if "--label" in args:
        i = args.index("--label")
        label = args[i + 1]
        del args[i:i + 2]
    if len(args) != 2 or not re.fullmatch(r"[A-Za-z0-9_.-]+", args[1]):
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    pdf, edition = Path(args[0]), args[1]
    if not pdf.is_file():
        print(json.dumps({"error": f"not found: {pdf}"}))
        sys.exit(1)
    stem = re.sub(r"[^A-Za-z0-9_.-]+", "_", pdf.stem)[:60]
    out = REFS / edition / stem
    if out.exists() and any(out.iterdir()):
        print(json.dumps({"error": f"already indexed (not overwriting): {out.as_posix()}"}))
        sys.exit(1)
    out.mkdir(parents=True, exist_ok=True)
    doc = fitz.open(str(pdf))
    sections, tables, pages = {}, {}, []
    texts = [page.get_text() for page in doc]
    freq = {}
    for t in texts:   # running headers/footers repeat on most pages: never use them as a page's heading
        for ln in set(x.strip() for x in t.splitlines() if x.strip()):
            freq[ln] = freq.get(ln, 0) + 1
    common = {ln for ln, n in freq.items() if n > max(2, len(texts) // 3)}
    for i, page in enumerate(doc, 1):
        text = texts[i - 1]
        clean = "\n".join(ln.rstrip() for ln in text.splitlines() if ln.strip())
        secs = sorted(set(SECTION.findall(clean)), key=lambda s: [int(x) for x in s.split(".")])
        tabs = sorted(set(t.upper() for t in TABLE.findall(clean)))
        apps = sorted(set(a.upper() for a in APPENDIX.findall(clean)))
        image_only = len(clean) < MIN_TEXT
        png = None
        if image_only:
            png = f"p{i:03d}.png"
            page.get_pixmap(dpi=110).save(str(out / png))
        for s in secs:
            sections.setdefault(s, []).append(i)
        for t in tabs:
            tables.setdefault(t, []).append(i)
        for a in apps:
            tables.setdefault(f"APPENDIX {a}", []).append(i)
        first = next((ln.strip() for ln in clean.splitlines()
                      if len(ln.strip()) > 12 and ln.strip() not in common
                      and not re.fullmatch(r"[\d/ ]+", ln.strip())), "(no distinct heading)")[:90]
        pages.append({"page": i, "chars": len(clean), "first": first, "image": png, "sections": secs, "tables": tabs})
        head = (f"# {label or pdf.name} - page {i} of {len(doc)}\n\nSource: {pdf.as_posix()} (page {i})\n"
                f"Sections on this page: {', '.join(secs) or '-'}\nTables on this page: {', '.join(tabs) or '-'}\n")
        if png:
            head += f"IMAGE PAGE: the text layer is nearly empty; read {png} (same folder) for the content.\n"
        (out / f"p{i:03d}.md").write_text(head + "\n---\n\n" + clean + "\n", encoding="utf-8")
    idx = [f"# {label or pdf.name}", "", f"Source: {pdf.as_posix()} ({len(doc)} pages). Edition folder: {edition}.",
           "Cite as: <source>, page N (file pNNN.md). Section and table numbers are what appears in the page text;",
           "confirm the wording on the page before citing.", "", "## Tables / appendices -> pages", ""]
    idx += [f"- TABLE {t}: {', '.join(f'p{p:03d}' for p in sorted(set(ps)))}" if not t.startswith("APPENDIX")
            else f"- {t}: {', '.join(f'p{p:03d}' for p in sorted(set(ps)))}" for t, ps in sorted(tables.items())]
    idx += ["", "## Section numbers -> pages", ""]
    idx += [f"- {s}: {', '.join(f'p{p:03d}' for p in sorted(set(ps)))}"
            for s, ps in sorted(sections.items(), key=lambda kv: [int(x) for x in kv[0].split('.')])]
    idx += ["", "## Pages", ""]
    idx += [f"- p{p['page']:03d}{' [IMAGE: ' + p['image'] + ']' if p['image'] else ''}: {p['first']}" for p in pages]
    (out / "INDEX.md").write_text("\n".join(idx) + "\n", encoding="utf-8")
    src = REFS / edition / "SOURCES.md"
    line = f"- {label or pdf.name}: {stem}/INDEX.md ({len(doc)} pages, from {pdf.as_posix()})\n"
    src.write_text((src.read_text(encoding="utf-8") if src.exists() else f"# {edition} sources\n\n") + line,
                   encoding="utf-8")
    print(json.dumps({"edition": edition, "folder": out.as_posix(), "pages": len(doc),
                      "image_pages": sum(1 for p in pages if p["image"]), "sections": len(sections),
                      "tables": len(tables)}))


if __name__ == "__main__":
    main()
