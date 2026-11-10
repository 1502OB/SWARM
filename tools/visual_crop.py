"""Crop a changed region of an exported sheet PDF for visual-verifier (PyMuPDF).

    python ~/swarm/tools/visual_crop.py <sheet.pdf | folder> <page-1-based> <x0,y0,x1,y1> <task-id>
           [--units pt|in|frac] [--sheet W,H] [--dpi 200]
    python ~/swarm/tools/visual_crop.py <sheet.pdf | folder> <page-1-based | all> <task-id> --find "<text>"
           [--margin 72] [--dpi 200]

--find "<text>": crop around the FIRST match of the text (PyMuPDF search_for, case-insensitive) padded by --margin
  points (default 72 = 1 in) on each side. With page "all" the first page that has a match is used. Text boxes
  come in UNROTATED page coordinates, the crop uses the rotated page.rect, so the box is multiplied by
  page.rotation_matrix first (LESSONS A45: AutoCAD PDFs with /Rotate 270 gave blank crops). The match count and
  the box are printed.

<sheet.pdf | folder>: a PDF, or a folder (the newest .pdf in it is used). Export each task into its own folder
  (swarm/visual/<run-id>/<task-id>/) and pass the folder: Revit names exported PDFs after the sheet title, and a
  title with "&" cannot pass the Bash guard.
Rectangle units, measured from the sheet's top-left corner:
  pt   PDF points on the exported page (default)
  in   sheet inches; with --sheet W,H (the real sheet size in inches) they are placed on the exported page the
       way Revit batch_export does it: centred at 1:1 (a 42x30 sheet on its A0 page = 2.4 in / 1.55 in margins),
       shrunk only if the page is smaller than the sheet
  frac fractions 0-1 of the page (use when the sheet size is unknown)
  (these rectangles are already in page.rect space, i.e. what you see in a viewer: no rotation is applied)
The rectangle is padded by 0.5 in (page scale) and clipped to the page. Writes
~/swarm/crops/<task-id>.after.png at 200 DPI, and crops/<task-id>.pair.png (before | after) when
crops/<task-id>.before.png exists. Prints JSON: pdf used, page size, rectangle, scale, outputs.
"""
import json
import os
import sys
from pathlib import Path

try:
    import pymupdf as fitz  # PyMuPDF >= 1.24
except ImportError:  # older PyMuPDF
    import fitz

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
from swarm_paths import SWARM  # noqa: E402

CROPS = SWARM / "crops"


def opt(args, name, default):
    if name in args:
        i = args.index(name)
        val = args[i + 1]
        del args[i:i + 2]
        return val
    return default


def fail(msg, **kw):
    print(json.dumps({"error": msg, **kw}))
    sys.exit(1)


def text_rect(page, text):
    """Boxes of `text` on page, converted to page.rect (rotated) space: [Rect, ...]."""
    return [fitz.Rect(r) * page.rotation_matrix for r in page.search_for(text)]


def main():
    args = sys.argv[1:]
    units = opt(args, "--units", "pt")
    sheet = opt(args, "--sheet", None)
    dpi = int(opt(args, "--dpi", "200"))
    find = opt(args, "--find", None)
    margin = float(opt(args, "--margin", "72"))
    if (find is None and len(args) != 4) or (find is not None and len(args) != 3) or units not in ("pt", "in", "frac"):
        print(__doc__.strip().splitlines()[2].strip())
        print(__doc__.strip().splitlines()[4].strip())
        sys.exit(2)
    if find is not None:
        src, page_no, task = args
    else:
        src, page_no, rect_s, task = args
    src = Path(src)
    if src.is_dir():
        pdfs = sorted(src.glob("*.pdf"), key=lambda p: p.stat().st_mtime)
        if not pdfs:
            fail(f"no .pdf in folder {src.as_posix()}")
        src = pdfs[-1]
    doc = fitz.open(str(src))
    found = None
    if find is not None:
        pages = range(len(doc)) if page_no.lower() == "all" else [int(page_no) - 1]
        for k in pages:
            hits = text_rect(doc[k], find)
            if hits:
                found = (k, hits)
                break
        if not found:
            fail(f"text not found: {find!r}", pdf=src.as_posix(), page=page_no)
        page_no = str(found[0] + 1)
    page = doc[int(page_no) - 1]
    pw, ph = page.rect.width, page.rect.height
    if found:
        r0 = found[1][0]
        rect_s = f"{r0.x0},{r0.y0},{r0.x1},{r0.y1}"
        units = "pt"
    v = [float(x) for x in rect_s.split(",")]
    scale, ox, oy = 1.0, 0.0, 0.0
    if units == "frac":
        v = [v[0] * pw, v[1] * ph, v[2] * pw, v[3] * ph]
    elif units == "in":
        if sheet:
            sw, sh = (float(x) * 72 for x in sheet.split(","))
            scale = min(1.0, pw / sw, ph / sh)  # batch_export centres the sheet at 1:1 (Phase 6 test)
            ox, oy = (pw - sw * scale) / 2, (ph - sh * scale) / 2
        v = [ox + v[0] * 72 * scale, oy + v[1] * 72 * scale, ox + v[2] * 72 * scale, oy + v[3] * 72 * scale]
    pad = margin if found else 36.0 * (scale if units == "in" else 1.0)
    rect = fitz.Rect(min(v[0], v[2]) - pad, min(v[1], v[3]) - pad, max(v[0], v[2]) + pad, max(v[1], v[3]) + pad)
    rect = rect & page.rect
    if rect.is_empty:
        fail("rectangle is outside the page", page_pt=[round(pw, 1), round(ph, 1)])
    CROPS.mkdir(parents=True, exist_ok=True)
    after = CROPS / f"{task}.after.png"
    page.get_pixmap(clip=rect, dpi=dpi).save(str(after))
    result = {"pdf": src.as_posix(), "page_pt": [round(pw, 1), round(ph, 1)],
              "page_in": [round(pw / 72, 2), round(ph / 72, 2)], "rect_pt": [round(x, 1) for x in rect],
              "scale": round(scale, 4), "dpi": dpi, "after": after.as_posix()}
    if found:
        result.update(find=find, page=found[0] + 1, matches=len(found[1]), rotation=page.rotation,
                      match_rect_pt=[round(x, 1) for x in found[1][0]])
    before = CROPS / f"{task}.before.png"
    if before.exists():
        a, b = fitz.Pixmap(str(after)), fitz.Pixmap(str(before))
        out = fitz.open()
        pg = out.new_page(width=b.width + a.width + 20, height=max(a.height, b.height))
        pg.insert_image(fitz.Rect(0, 0, b.width, b.height), filename=str(before))
        pg.insert_image(fitz.Rect(b.width + 20, 0, b.width + 20 + a.width, a.height), filename=str(after))
        pair = CROPS / f"{task}.pair.png"
        pg.get_pixmap(dpi=72).save(str(pair))
        result["pair"] = pair.as_posix()
    print(json.dumps(result))


if __name__ == "__main__":
    main()
