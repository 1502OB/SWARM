"""Render a folder of exported sheet PDFs into review images.

usage: python tools/contact_sheets.py <FINAL dir> <out dir>
  - contact_<n>.png: 6 sheets per image (3x2), each ~1000 px wide, labelled
  - plan_<SheetNumber>.png: P1/P2 sheets at ~2400 px wide (drawing area only, title block strip cut off)
"""
import glob
import os
import sys

import pymupdf as fitz

src, out = sys.argv[1], sys.argv[2]
os.makedirs(out, exist_ok=True)
pdfs = []
for d in sorted(glob.glob(os.path.join(src, "*"))):
    if os.path.isdir(d):
        f = glob.glob(os.path.join(d, "*.pdf"))
        if f:
            pdfs.append((os.path.basename(d), max(f, key=os.path.getmtime)))
print(len(pdfs), "sheets")

TW = 1000
thumbs = []
for num, f in pdfs:
    pg = fitz.open(f)[0]
    z = TW / pg.rect.width
    pix = pg.get_pixmap(matrix=fitz.Matrix(z, z))
    thumbs.append((num, pix))
    if "-P1." in num or "-P2." in num:
        r = pg.rect
        clip = fitz.Rect(r.x0, r.y0, r.x0 + r.width * 0.78, r.y1)  # drop the right title-block strip
        z2 = 2400 / clip.width
        pg.get_pixmap(matrix=fitz.Matrix(z2, z2), clip=clip).save(os.path.join(out, f"plan_{num}.png"))

per = 6
for i in range(0, len(thumbs), per):
    grp = thumbs[i:i + per]
    h = max(p.height for _, p in grp)
    doc = fitz.open()
    page = doc.new_page(width=TW * 3, height=(h + 40) * 2)
    for k, (num, p) in enumerate(grp):
        x, y = (k % 3) * TW, (k // 3) * (h + 40)
        page.insert_image(fitz.Rect(x, y + 40, x + TW, y + 40 + p.height), pixmap=p)
        page.insert_text((x + 10, y + 30), num, fontsize=28, color=(1, 0, 0))
        page.draw_rect(fitz.Rect(x, y, x + TW, y + 40 + p.height), color=(0.6, 0.6, 0.6), width=1)
    page.get_pixmap().save(os.path.join(out, f"contact_{i // per + 1:02d}.png"))
print("done", out)
