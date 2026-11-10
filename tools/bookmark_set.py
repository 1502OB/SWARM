"""Add one bookmark per page to a combined Revit sheet-set PDF.

usage: python tools/bookmark_set.py <in.pdf> <out.pdf> <names_dir> <prefix>
  names_dir/<SheetNumber>/Sheet-<name>.pdf supplies the sheet titles (e.g. visual/20261110/SET).
  Each page's sheet number is found in its text (prefix + d.dd), and checked to be in ascending order.
"""
import glob
import os
import re
import sys

import pymupdf as fitz

src, out, names_dir, prefix = sys.argv[1:5]
names = {}
for f in glob.glob(os.path.join(names_dir, prefix + "*", "*.pdf")):
    num = os.path.basename(os.path.dirname(f))
    n = os.path.splitext(os.path.basename(f))[0]
    names[num] = n[6:] if n.lower().startswith("sheet-") else n
rx = re.compile(re.escape(prefix) + r"\d\.\d\d")
d = fitz.open(src)
toc, seen, problems = [], [], []
for i, pg in enumerate(d, 1):
    hits = rx.findall(pg.get_text().replace(" ", ""))
    num = hits[-1] if hits else f"page {i}"  # the title block sheet number is printed last on the page
    if not hits:
        problems.append(f"page {i}: no {prefix} sheet number found")
    seen.append(num)
    toc.append([1, f"{num} - {names.get(num, '')}".rstrip(" -"), i])
if seen != sorted(seen):
    problems.append("pages not in sheet order: " + ", ".join(seen))
if len(set(seen)) != len(seen):
    problems.append("duplicate sheet numbers: " + ", ".join(seen))
d.set_toc(toc)
d.save(out, garbage=3, deflate=True)
r = d[0].rect
print(f"{out}: {len(d)} pages, {r.width / 72:.1f}x{r.height / 72:.1f} in")
for p in problems:
    print("PROBLEM", p)
