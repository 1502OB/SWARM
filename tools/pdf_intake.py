"""PDF intake helper for doc-scout (PyMuPDF).

Subcommands
  scan  <inputs_dir> <crops_dir> <out_json> [--dpi 200]
        For each PDF: page sizes, sheet-number candidates (+ discipline_guess from the prefix), every markup
        annotation (id, page, type, rect, content, subject, author, date) with an auto crop at --dpi, a 12in
        context crop at 100 DPI (<id>.ctx.png, for grid bubbles / room tags), nearby words, and a match against a
        same-base-name Bluebeam CSV/XML export if present.
        Pages with no annotations get their text blocks (for comment letters / correction lists).
  crop  <pdf> <page_1based> <x0,y0,x1,y1> <out_png> [--dpi 200] [--pad 36]
        Crop an arbitrary region (PDF points, page coordinates as reported by scan/find).
  find  <pdf> <text> [--page N]
        Search for text; prints page numbers and rects (use to locate a sheet/grid/detail tag).

All rects are in UNROTATED page coordinates (PDF points), the same space as annot.rect,
page.search_for and get_text clips. Only rendering converts to the rotated space.
"""
import csv
import difflib
import json
import re
import sys
import xml.etree.ElementTree as ET
from pathlib import Path

import pymupdf

SKIP_TYPES = {"Link", "Widget", "Popup"}
SHEET_RE = re.compile(r"\b([A-Z]{1,3}[-\s]?\d{1,3}(?:\.\d{1,2}[A-Z]?)?)\b")
MIN_CROP_PT = 216  # 3 in
# Context crop: a wide, low-res view around each markup so grid bubbles and room tags are in frame.
CONTEXT_PT = 864  # 12 in
CONTEXT_DPI = 100
# Sheet-number prefix -> discipline. Longest prefix wins (FP before F, PL before P).
DISCIPLINES = {"P": "plumbing", "PL": "plumbing", "A": "architectural", "AD": "architectural", "ID": "interiors",
               "S": "structural", "M": "mechanical", "H": "mechanical", "E": "electrical", "FA": "fire alarm",
               "FP": "fire protection", "F": "fire protection", "C": "civil", "L": "landscape", "T": "general",
               "G": "general", "K": "food service", "FS": "food service"}


def _discipline(sheet):
    m = re.match(r"([A-Z]{1,3})", sheet or "")
    if not m:
        return None
    p = m.group(1)
    for n in range(len(p), 0, -1):
        if p[:n] in DISCIPLINES:
            return DISCIPLINES[p[:n]]
    return None


def _norm(s):
    return re.sub(r"\s+", " ", (s or "")).strip().lower()


def _unrot_bounds(page):
    return (page.rect * page.derotation_matrix).normalize() if page.rotation else page.rect


def _crop(page, disp_rect, out_png, dpi=200, pad=36, min_pt=MIN_CROP_PT):
    r = pymupdf.Rect(disp_rect)
    # grow to at least min_pt x min_pt (3in by default) around the centre, plus padding
    cx, cy = (r.x0 + r.x1) / 2, (r.y0 + r.y1) / 2
    w = max(r.width + 2 * pad, min_pt)
    h = max(r.height + 2 * pad, min_pt)
    r = pymupdf.Rect(cx - w / 2, cy - h / 2, cx + w / 2, cy + h / 2) & _unrot_bounds(page)
    # get_pixmap's clip is in the rotated (displayed) space
    clip = r * page.rotation_matrix if page.rotation else r
    pix = page.get_pixmap(clip=pymupdf.Rect(clip).normalize(), dpi=dpi)
    Path(out_png).parent.mkdir(parents=True, exist_ok=True)
    pix.save(out_png)
    return [round(v, 1) for v in r], pix.width, pix.height


def _sheet_candidates(page):
    """Sheet numbers usually sit in the title block: bottom-right region, largest text."""
    pr = page.rect  # displayed orientation: title block is bottom-right as the sheet is viewed
    tb = pymupdf.Rect(pr.x0 + pr.width * 0.75, pr.y0 + pr.height * 0.70, pr.x1, pr.y1)
    if page.rotation:
        tb = (tb * page.derotation_matrix).normalize()
    cands = {}
    for b in page.get_text("dict", clip=tb)["blocks"]:
        for line in b.get("lines", []):
            for sp in line["spans"]:
                for m in SHEET_RE.finditer(sp["text"]):
                    k = m.group(1).replace(" ", "")
                    cands[k] = max(cands.get(k, 0), round(sp["size"], 1))
    return [k for k, _ in sorted(cands.items(), key=lambda kv: -kv[1])][:5]


def _near_words(page, disp_rect, grow=144):
    r = pymupdf.Rect(disp_rect)
    r = pymupdf.Rect(r.x0 - grow, r.y0 - grow, r.x1 + grow, r.y1 + grow) & _unrot_bounds(page)
    words = page.get_text("words", clip=r)
    return " ".join(w[4] for w in words)[:600]


def _load_export(pdf):
    base = pdf.with_suffix("")
    rows, src = [], None
    for ext in (".csv", ".CSV"):
        p = Path(str(base) + ext)
        if p.exists():
            src = p
            with open(p, newline="", encoding="utf-8-sig", errors="replace") as f:
                for row in csv.DictReader(f):
                    rows.append({k.strip(): (v or "").strip() for k, v in row.items() if k})
            break
    if not rows:
        for ext in (".xml", ".XML"):
            p = Path(str(base) + ext)
            if p.exists():
                src = p
                root = ET.parse(p).getroot()
                for el in root.iter():
                    kids = {c.tag: (c.text or "").strip() for c in el}
                    if any(k.lower() in ("comments", "comment", "contents", "subject") for k in kids):
                        rows.append(kids)
                break
    return rows, (str(src) if src else None)


def _row_text(row):
    for k in row:
        if k.lower() in ("comments", "comment", "contents"):
            return row[k]
    return ""


def _row_page(row):
    for k in row:
        if k.lower() in ("page index", "page"):
            try:
                return int(re.sub(r"\D", "", row[k]))
            except ValueError:
                pass
    return None


def _match(rows, page_no, content, used):
    best, best_i = 0.0, None
    for i, row in enumerate(rows):
        if i in used:
            continue
        rp = _row_page(row)
        if rp is not None and rp not in (page_no, page_no - 1):  # export may be 0- or 1-based
            continue
        s = difflib.SequenceMatcher(None, _norm(_row_text(row)), _norm(content)).ratio()
        if s > best:
            best, best_i = s, i
    return best_i, round(best, 2)


def scan(inputs, crops, out_json, dpi=200):
    inputs, crops = Path(inputs), Path(crops)
    report = {"files": [], "dpi": dpi}
    for pdf in sorted({q.resolve() for q in inputs.iterdir() if q.suffix.lower() == ".pdf"}):
        doc = pymupdf.open(pdf)
        rows, export = _load_export(pdf)
        used = set()
        f = {"file": pdf.name, "pages": doc.page_count, "markup_export": export,
             "export_rows": len(rows), "items": [], "text_pages": [], "errors": []}
        for pno, page in enumerate(doc, start=1):
            sheets = _sheet_candidates(page)
            n_annots = 0
            for a in page.annots() or []:
                if a.type[1] in SKIP_TYPES:
                    continue
                n_annots += 1
                info = a.info
                content = info.get("content", "")
                aid = f"{pdf.stem}-p{pno}-x{a.xref}"
                aid = re.sub(r"[^A-Za-z0-9._-]+", "_", aid)
                drect = a.rect
                item = {"id": aid, "page": pno, "type": a.type[1], "rect": [round(v, 1) for v in drect],
                        "content": content, "subject": info.get("subject", ""), "author": info.get("title", ""),
                        "date": info.get("modDate", ""), "sheet_candidates": sheets,
                        "discipline_guess": _discipline(sheets[0] if sheets else None),
                        "near_text": _near_words(page, drect)}
                if rows:
                    i, score = _match(rows, pno, content, used)
                    if i is not None and score >= 0.6:
                        used.add(i)
                        item["export_comment"] = _row_text(rows[i])
                        item["export_row"] = rows[i]
                    item["export_match_score"] = score
                try:
                    crect, pw, ph = _crop(page, drect, str(crops / f"{aid}.png"), dpi=dpi)
                    item.update(crop=str(crops / f"{aid}.png").replace("\\", "/"), crop_rect=crect, crop_px=[pw, ph])
                    ctx = str(crops / f"{aid}.ctx.png")
                    xrect, _, _ = _crop(page, drect, ctx, dpi=CONTEXT_DPI, pad=0, min_pt=CONTEXT_PT)
                    item.update(context_crop=ctx.replace("\\", "/"), context_rect=xrect)
                except Exception as e:  # noqa: BLE001
                    item["crop_error"] = str(e)
                f["items"].append(item)
            if n_annots == 0:
                blocks = [b[4].strip() for b in page.get_text("blocks") if b[4].strip()]
                f["text_pages"].append({"page": pno, "sheet_candidates": sheets,
                                        "discipline_guess": _discipline(sheets[0] if sheets else None),
                                        "page_size_in": [round(page.rect.width / 72, 1), round(page.rect.height / 72, 1)],
                                        "rotation": page.rotation,
                                        "blocks": blocks[:200]})
        if rows:
            f["export_rows_unmatched"] = [rows[i] for i in range(len(rows)) if i not in used][:100]
        report["files"].append(f)
    Path(out_json).write_text(json.dumps(report, indent=1), encoding="utf-8")
    print(f"scanned {len(report['files'])} pdf(s); "
          f"{sum(len(f['items']) for f in report['files'])} annotation(s); out={out_json}")


def crop(pdf, page_no, rect, out_png, dpi=200, pad=36):
    doc = pymupdf.open(pdf)
    page = doc[int(page_no) - 1]
    r = [float(v) for v in rect.split(",")]
    crect, pw, ph = _crop(page, r, out_png, dpi=dpi, pad=pad)
    print(json.dumps({"crop": out_png, "crop_rect": crect, "px": [pw, ph]}))


def find(pdf, text, page=None):
    doc = pymupdf.open(pdf)
    hits = []
    pages = [int(page)] if page else range(1, doc.page_count + 1)
    for pno in pages:
        for r in doc[pno - 1].search_for(text):
            hits.append({"page": pno, "rect": [round(v, 1) for v in r]})
    print(json.dumps({"query": text, "hits": hits[:50], "total": len(hits)}))


def _opt(args, name, default):
    if name in args:
        i = args.index(name)
        v = args[i + 1]
        del args[i:i + 2]
        return v
    return default


if __name__ == "__main__":
    a = sys.argv[1:]
    dpi = int(_opt(a, "--dpi", 200))
    if dpi < 200:
        sys.exit("dpi must be >= 200")
    pad = float(_opt(a, "--pad", 36))
    pg = _opt(a, "--page", None)
    cmd = a[0] if a else ""
    if cmd == "scan":
        scan(a[1], a[2], a[3], dpi)
    elif cmd == "crop":
        crop(a[1], a[2], a[3], a[4], dpi, pad)
    elif cmd == "find":
        find(a[1], a[2], pg)
    else:
        sys.exit(__doc__)
