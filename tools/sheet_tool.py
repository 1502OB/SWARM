"""Sheet helper for QAQC agents (PyMuPDF). Coordinates are unrotated PDF points of the page, origin top-left,
y down: the same space as annotation rects, so a located rectangle can be used for a markup directly.

  render <pdf> <out_dir> [--dpi 150] [--rows 2 --cols 3]
         overview.png (whole sheet, ~1600 px) + tile_r{r}c{c}.png with ~3% overlap + tiles.json (tile -> rect)
  zoom   <pdf> <x0,y0,x1,y1> <out_png> [--dpi 300]       a region at higher resolution (max 2400 px a side)
  grid   <pdf> <x0,y0,x1,y1> <out_png>                   a region with labeled coordinate gridlines
  check  <pdf> <x0,y0,x1,y1> <out_png>                   the rectangle drawn in red with 150 pt of context
  text   <pdf> <out_txt>                                 the page text (for grep across sheets)
Page 1 is used. Every command prints one JSON line with the output path and the page size.
Outputs must be under ~/swarm (source folders stay read-only).
"""
import os
import json
import math
import sys
from pathlib import Path

import pymupdf
sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
from swarm_paths import SWARM  # noqa: E402

OUT_ROOT = SWARM.resolve()
MAX_PX = 2400


def _out(path):
    p = Path(path).resolve()
    if OUT_ROOT not in p.parents:
        sys.exit(f"output {p} is outside {OUT_ROOT}; write renders to your scratch folder under swarm/")
    p.parent.mkdir(parents=True, exist_ok=True)
    return p


def _page(pdf):
    return pymupdf.open(pdf)[0]


def _bounds(page):
    return (page.rect * page.derotation_matrix).normalize() if page.rotation else page.rect


def _shot(page, rect, out_png, dpi):
    """Render an unrotated-space rect; cap the long side at MAX_PX."""
    r = pymupdf.Rect(rect) & _bounds(page)
    dpi = min(dpi, int(MAX_PX * 72 / max(r.width, r.height, 1)))
    clip = (r * page.rotation_matrix).normalize() if page.rotation else r
    pix = page.get_pixmap(clip=clip, dpi=dpi)
    pix.save(str(_out(out_png)))
    return r, dpi, pix.width, pix.height


def _rect(s):
    v = [float(x) for x in s.split(",")]
    if len(v) != 4:
        sys.exit("rect must be x0,y0,x1,y1")
    return v


def _report(page, **kw):
    b = _bounds(page)
    print(json.dumps({**kw, "page_size_pt": [round(b.width, 1), round(b.height, 1)], "rotation": page.rotation}))


def render(pdf, out_dir, dpi=150, rows=2, cols=3):
    page = _page(pdf)
    b = _bounds(page)
    out_dir = Path(out_dir)
    _shot(page, b, out_dir / "overview.png", dpi=int(1600 * 72 / max(b.width, b.height)))
    tiles = {}
    # Rows and columns follow the sheet as displayed (r0 = top, c0 = left), even on rotated pages;
    # each tile's rect is then stored in unrotated page points like everything else.
    d = page.rect
    w, h = d.width / cols, d.height / rows
    ov_w, ov_h = w * 0.03, h * 0.03
    for r in range(rows):
        for c in range(cols):
            disp = pymupdf.Rect(max(d.x0, d.x0 + c * w - ov_w), max(d.y0, d.y0 + r * h - ov_h),
                                min(d.x1, d.x0 + (c + 1) * w + ov_w), min(d.y1, d.y0 + (r + 1) * h + ov_h))
            rect = list((disp * page.derotation_matrix).normalize()) if page.rotation else list(disp)
            name = f"tile_r{r}c{c}"
            _, used, pw, ph = _shot(page, rect, out_dir / f"{name}.png", dpi)
            tiles[name] = {"rect": [round(v, 1) for v in rect], "dpi": used, "px": [pw, ph]}
    (_out(out_dir / "tiles.json")).write_text(json.dumps(tiles, indent=1), encoding="utf-8")
    _report(page, out_dir=str(out_dir), tiles=len(tiles))


def zoom(pdf, rect, out_png, dpi=300):
    page = _page(pdf)
    r, used, pw, ph = _shot(page, _rect(rect), out_png, dpi)
    _report(page, out=out_png, rect=[round(v, 1) for v in r], dpi=used, px=[pw, ph])


def grid(pdf, rect, out_png):
    page = _page(pdf)
    clip = pymupdf.Rect(_rect(rect)) & _bounds(page)
    span = max(clip.width, clip.height)
    step = 50 if span <= 900 else 100 if span <= 1800 else 200
    fs, lw = 10 * span / 900, (0.4 if span < 1800 else 1)
    rot = page.rotation  # labels are turned with the page so they read upright as displayed
    gx = math.floor(clip.x0 / step) * step
    while gx <= clip.x1:
        page.draw_line((gx, clip.y0), (gx, clip.y1), color=(0, 0.6, 1), width=lw)
        page.insert_text((gx + 2, clip.y0 + 12 * span / 900), f"x{int(gx)}", fontsize=fs, color=(0, 0.3, 1), rotate=rot)
        gx += step
    gy = math.floor(clip.y0 / step) * step
    while gy <= clip.y1:
        page.draw_line((clip.x0, gy), (clip.x1, gy), color=(0, 0.6, 1), width=lw)
        page.insert_text((clip.x0 + 2, gy - 2), f"y{int(gy)}", fontsize=fs, color=(0, 0.3, 1), rotate=rot)
        gy += step
    _, used, pw, ph = _shot(page, clip, out_png, 200)
    _report(page, out=out_png, step_pt=step, dpi=used, px=[pw, ph])


def check(pdf, rect, out_png):
    page = _page(pdf)
    x0, y0, x1, y1 = _rect(rect)
    page.draw_rect(pymupdf.Rect(x0, y0, x1, y1), color=(1, 0, 0), width=3)
    _, used, pw, ph = _shot(page, (x0 - 150, y0 - 150, x1 + 150, y1 + 150), out_png, 200)
    _report(page, out=out_png, rect=[x0, y0, x1, y1], dpi=used, px=[pw, ph])


def text(pdf, out_txt):
    page = _page(pdf)
    _out(out_txt).write_text(page.get_text(), encoding="utf-8")
    _report(page, out=out_txt)


def _opt(args, name, default):
    if name in args:
        i = args.index(name)
        v = args[i + 1]
        del args[i:i + 2]
        return v
    return default


if __name__ == "__main__":
    a = sys.argv[1:]
    dpi = _opt(a, "--dpi", None)
    rows, cols = int(_opt(a, "--rows", 2)), int(_opt(a, "--cols", 3))
    cmd = a[0] if a else ""
    if cmd == "render":
        render(a[1], a[2], int(dpi or 150), rows, cols)
    elif cmd == "zoom":
        zoom(a[1], a[2], a[3], int(dpi or 300))
    elif cmd == "grid":
        grid(a[1], a[2], a[3])
    elif cmd == "check":
        check(a[1], a[2], a[3])
    elif cmd == "text":
        text(a[1], a[2])
    else:
        sys.exit(__doc__)
