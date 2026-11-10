"""Assemble single-sheet Revit PDF exports into one bookmarked set at the true sheet size.

usage: python tools/assemble_sheet_set.py <export_dir> <prefix> <out.pdf> [--size 42x30]
  export_dir/<SheetNumber>/*.pdf   one PDF per sheet (e.g. A0 exports with the sheet centred)
  prefix                           e.g. F-P  -> takes every folder whose name starts with it, sorted by sheet number
Each page: the title block border (longest horizontal + vertical lines) is found, cropped, and scaled onto a new
page of the given size (inches). Bookmark = '<SheetNumber> - <sheet name from the file name>'.
Checks: the sheet number text is on the page; reports any page whose border was not found.
"""
import glob
import os
import re
import sys

import pymupdf as fitz

src, prefix, out = sys.argv[1], sys.argv[2], sys.argv[3]
size = "42x30"
if "--size" in sys.argv:
    size = sys.argv[sys.argv.index("--size") + 1]
W, H = [float(v) * 72 for v in size.split("x")]


def key(num):
    return [int(t) if t.isdigit() else t for t in re.split(r"(\d+)", num)]


def border(pg):
    hs, vs = [], []
    for d in pg.get_drawings():
        for it in d["items"]:
            if it[0] != "l":
                continue
            a, b = it[1], it[2]
            if abs(a.y - b.y) < 0.5 and abs(a.x - b.x) > pg.rect.width * 0.6:
                hs.append((a.y, min(a.x, b.x), max(a.x, b.x)))
            if abs(a.x - b.x) < 0.5 and abs(a.y - b.y) > pg.rect.height * 0.6:
                vs.append((a.x, min(a.y, b.y), max(a.y, b.y)))
        if d["rect"].width > pg.rect.width * 0.6 and d["rect"].height > pg.rect.height * 0.6 and not d["items"][0][0] == "l":
            r = d["rect"]
            hs += [(r.y0, r.x0, r.x1), (r.y1, r.x0, r.x1)]
            vs += [(r.x0, r.y0, r.y1), (r.x1, r.y0, r.y1)]
    if len(hs) < 2 or len(vs) < 2:
        return None
    return fitz.Rect(min(v[0] for v in vs), min(h[0] for h in hs), max(v[0] for v in vs), max(h[0] for h in hs))


folders = sorted([f for f in glob.glob(os.path.join(src, prefix + "*")) if os.path.isdir(f)],
                 key=lambda f: key(os.path.basename(f)))
outdoc = fitz.open()
toc, problems = [], []
for f in folders:
    num = os.path.basename(f)
    pdfs = glob.glob(os.path.join(f, "*.pdf"))
    if not pdfs:
        problems.append(num + ": no PDF")
        continue
    pdf = max(pdfs, key=os.path.getmtime)
    sd = fitz.open(pdf)
    pg = sd[0]
    if num not in pg.get_text().replace(" ", ""):
        problems.append(num + ": sheet number not found on the page (" + os.path.basename(pdf) + ")")
    clip = border(pg)
    if clip is not None and abs(clip.width / clip.height - W / H) > 0.02:
        problems.append(num + f": border found {clip} has the wrong aspect, using the A0 default")
        clip = None
    if clip is None and abs(pg.rect.width - 3370.4) < 2 and abs(pg.rect.height - 2383.94) < 2:
        clip = fitz.Rect(168.12, 108.36, 3202.2, 2275.56)  # 42x30 sheet border in a Revit A0 export (measured 11-05)
    if clip is None:
        problems.append(num + ": border not found, page scaled whole")
        clip = pg.rect
    np_ = outdoc.new_page(width=W, height=H)
    np_.show_pdf_page(np_.rect, sd, 0, clip=clip, keep_proportion=True)
    name = os.path.splitext(os.path.basename(pdf))[0]
    name = name[6:] if name.lower().startswith("sheet-") else name
    toc.append([1, f"{num} - {name}", len(outdoc)])
    sd.close()
outdoc.set_toc(toc)
outdoc.set_metadata({"title": os.path.splitext(os.path.basename(out))[0], "author": "120 Degreez", "creator": "Revit export + assemble_sheet_set.py"})
outdoc.save(out, garbage=3, deflate=True)
print(f"{out}: {len(outdoc)} pages {size} in")
for p in problems:
    print("PROBLEM", p)
