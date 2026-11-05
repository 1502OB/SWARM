"""Build a QAQC markup PDF and change list from a run folder (shared tool; generalized from retreat/qaqc1/build/markup.py).

Usage:
  python C:/Users/Jeremy/swarm/tools/markup_build.py <run_dir> --tag "QAQC #1" --name <file prefix>
         [--sheets P0.01,P0.02,...] [--src src] [--include-rejected]

Reads, under <run_dir>:
  <src>/<SHEET>.pdf             one-page sheet PDFs (default src/)
  reviews/<SHEET>.json          sheet-reviewer output ({"items": [...]})
  code_reviews/ calc_reviews/ xsheet_reviews/ spec_reviews/<SHEET>.json
                                code-reviewer / calc-auditor / xsheet-checker / spec-checker output (same item
                                schema); merged into that sheet's items
  locate/*.loc.json             markup-locator rects (override review rects)
  checks/*.check.json           qaqc-checker verdicts (reviews/*.check.json is read too)
  overrides.json                orchestrator edits to items, keyed by item id (build/overrides.json also read).
                                Each entry should carry "_why" (what was checked); entries without one are listed.
Writes <run_dir>/out/: <name>_MARKUPS.pdf, <name>_CHANGE_LIST.xlsx, change_list.json, markup_log.json.
An existing out/ is moved to out_prev_<timestamp>/ first, never overwritten (LESSONS.md O16).
Every annotation's /Rect, /L and /CL is checked against the MediaBox after saving (LESSONS.md Q4-Q5); exit code 1
if any is off-page. Leaders are separate Line annotations (FreeText callouts land off-page on offset MediaBoxes).
"""
import argparse
import glob
import json
import os
import re
import shutil
import sys
import textwrap
import time

import pymupdf

COL = {'MUST': (0.85, 0, 0), 'SHOULD': (0.95, 0.45, 0), 'NICE': (0, 0.35, 0.85)}
MARGIN = 14


def natural(s):
    return [int(t) if t.isdigit() else t for t in re.split(r'(\d+)', s)]


def load(run, *patterns):
    d = {}
    for pattern in patterns:
        for f in sorted(glob.glob(os.path.join(run, pattern))):
            d.update(json.load(open(f, encoding='utf-8')))
    return d


def pri(p):
    p = str(p).lower()
    return 'MUST' if p.startswith('must') else 'SHOULD' if p.startswith('should') else 'NICE'


def off_page(pdf_path):
    """Annotations whose /Rect, /L or /CL leave the MediaBox (1 pt slack)."""
    bad = []
    doc = pymupdf.open(pdf_path)
    for n, pg in enumerate(doc):
        mb = pg.mediabox
        for a in pg.annots():
            for key in ('Rect', 'L', 'CL'):
                t, v = doc.xref_get_key(a.xref, key)
                if t != 'array':
                    continue
                nums = [float(x) for x in v.strip('[]').split()]
                xs, ys = nums[0::2], nums[1::2]
                if min(xs) < mb.x0 - 1 or max(xs) > mb.x1 + 1 or min(ys) < mb.y0 - 1 or max(ys) > mb.y1 + 1:
                    bad.append((n + 1, a.info.get('content', '')[:20], key))
    return bad


EXTRA_DIRS = ('code_reviews', 'calc_reviews', 'xsheet_reviews', 'spec_reviews')


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('run')
    ap.add_argument('--tag', required=True, help='annotation author, e.g. "QAQC #1"')
    ap.add_argument('--name', required=True, help='output file prefix, e.g. 25058_QAQC1')
    ap.add_argument('--sheets', help='comma-separated order; default: every reviews/<SHEET>.json, natural order')
    ap.add_argument('--src', default='src')
    ap.add_argument('--include-rejected', action='store_true')
    a = ap.parse_args()
    run = os.path.abspath(a.run)

    review_files = [f for f in glob.glob(os.path.join(run, 'reviews', '*.json')) if not f.endswith('.check.json')]
    for xd in EXTRA_DIRS:
        review_files += glob.glob(os.path.join(run, xd, '*.json'))
    order = a.sheets.split(',') if a.sheets else sorted(
        set(os.path.basename(f)[:-5] for f in review_files), key=natural)
    LOC = load(run, 'locate/*.loc.json')
    CHK = load(run, 'checks/*.check.json', 'reviews/*.check.json')
    OVR = load(run, 'overrides.json', 'build/overrides.json')

    rows, log, seen = [], [], set()
    out = pymupdf.open()
    for sid in order:
        src = pymupdf.open(os.path.join(run, a.src, sid + '.pdf'))
        out.insert_pdf(src)
        pg = out[-1]
        W, H = pg.rect.width, pg.rect.height
        rp = os.path.join(run, 'reviews', sid + '.json')
        review = json.load(open(rp, encoding='utf-8')) if os.path.exists(rp) else {'items': []}
        for xd in EXTRA_DIRS:
            cp = os.path.join(run, xd, sid + '.json')
            if os.path.exists(cp):
                review['items'] = review['items'] + json.load(open(cp, encoding='utf-8')).get('items', [])
        boxes = []
        clouds = [pymupdf.Rect((LOC.get(i['id']) or {}).get('rect') or i.get('rect') or (0, 0, 0, 0)) + (-6, -6, 6, 6)
                  for i in review['items']]
        for i in review['items']:
            seen.add(i['id'])
            i = dict(i, **{k: v for k, v in OVR.get(i['id'], {}).items() if not k.startswith('_')})
            iid, P = i['id'], pri(i.get('priority'))
            chk = CHK.get(iid) or {}
            verdict = chk.get('verdict')
            loc = LOC.get(iid) or {}
            rect, rq = (loc.get('rect'), 'located') if loc.get('rect') else (i.get('rect'), 'approximate')
            rows.append(dict(i, sheet=sid, verdict=verdict, correction=i.get('correction') or chk.get('correction'),
                             rect=rect, rect_source=rq, overridden=iid in OVR))
            if verdict == 'rejected' and not a.include_rejected:
                log.append((sid, iid, 'skipped: checker rejected'))
                continue
            if not rect:
                log.append((sid, iid, 'skipped: no rect'))
                continue
            r = pymupdf.Rect(rect) & pymupdf.Rect(MARGIN, MARGIN, W - MARGIN, H - MARGIN)
            if r.is_empty or r.width < 20 or r.height < 20:
                c = pymupdf.Rect(rect).tl if r.is_empty else r.tl
                r = pymupdf.Rect(c.x, c.y, c.x + 40, c.y + 40) & pymupdf.Rect(MARGIN, MARGIN, W - MARGIN, H - MARGIN)
            col = COL[P]
            txt = f"{iid} [{P}] " + (i.get('markup_text') or i.get('change_needed', ''))
            if verdict == 'unsure':
                txt += ' (checker: unsure)'
            if rq == 'approximate':
                txt += ' (location approx.)'
            bw = 300
            bh = len(textwrap.wrap(txt, 56)) * 11.5 + 8
            # callout box: nearest free spot around the cloud, clear of every cloud and box, left of the title block
            field = pymupdf.Rect(MARGIN, MARGIN, W * 0.895, H - MARGIN)
            box = None
            for d in range(30, 1400, 30):
                for dx, dy in ((1, -1), (1, 0), (-1, -1), (-1, 0), (0, -1), (0, 1), (1, 1), (-1, 1)):
                    x = (r.x1 + d if dx > 0 else r.x0 - d - bw if dx < 0 else r.x0)
                    y = (r.y0 - d - bh if dy < 0 else r.y1 + d if dy > 0 else r.y0)
                    cand = pymupdf.Rect(x, y, x + bw, y + bh)
                    if field.contains(cand) and not any(cand.intersects(o) for o in boxes + clouds):
                        box = cand
                        break
                if box:
                    break
            if box is None:
                box = pymupdf.Rect(MARGIN, MARGIN, MARGIN + bw, MARGIN + bh)
            boxes.append(box)
            info = dict(title=a.tag, subject=f"{P} - {i.get('category', '')}")
            cl = pg.add_rect_annot(r)
            cl.set_colors(stroke=col)
            cl.set_border(width=2, clouds=1)
            cl.set_info(content=f"{iid}\nISSUE: {i.get('issue', '')}\nCHANGE: {i.get('change_needed', '')}\n"
                                f"HOW: {i.get('revise_how', '')}\nWHO: {i.get('who', '')}\nCONF: {i.get('confidence', '')}"
                                f"\nCHECK: {verdict or '-'}", **info)
            cl.update()
            ft = pg.add_freetext_annot(box, txt, fontsize=9, fontname='helv', text_color=col,
                                       fill_color=(1, 1, 0.88), border_width=1)
            ft.set_info(content=txt, **info)
            ft.update()
            out.xref_set_key(ft.xref, 'CL', 'null')
            right = box.x0 > r.x1
            ln = pg.add_line_annot(pymupdf.Point(box.x0 if right else box.x1, box.y0 + bh / 2),
                                   pymupdf.Point(r.x1 if right else r.x0, r.y0))
            ln.set_colors(stroke=col)
            ln.set_border(width=1.2)
            ln.set_line_ends(pymupdf.PDF_ANNOT_LE_NONE, pymupdf.PDF_ANNOT_LE_OPEN_ARROW)
            ln.set_info(content=f"{iid} leader", **info)
            ln.update()
            log.append((sid, iid, rq))

    od = os.path.join(run, 'out')
    if os.path.isdir(od) and os.listdir(od):
        prev = os.path.join(run, 'out_prev_' + time.strftime('%Y%m%d_%H%M%S'))
        shutil.move(od, prev)
        print('previous out/ moved to', prev)
    os.makedirs(od, exist_ok=True)
    pdf = os.path.join(od, f'{a.name}_MARKUPS.pdf')
    out.save(pdf, garbage=3, deflate=True)
    bad = off_page(pdf)
    unused_ovr = sorted(k for k in OVR if k not in seen)
    no_why = sorted(k for k, v in OVR.items() if k in seen and '_why' not in v)
    json.dump({'log': log, 'out_of_bounds': bad, 'overrides_unused': unused_ovr, 'overrides_without_why': no_why},
              open(os.path.join(od, 'markup_log.json'), 'w', encoding='utf-8'), indent=1)
    json.dump(rows, open(os.path.join(od, 'change_list.json'), 'w', encoding='utf-8'), indent=1)

    from openpyxl import Workbook
    from openpyxl.styles import Alignment, Font, PatternFill
    wb = Workbook()
    ws = wb.active
    ws.title = 'Change list'
    COLS = [('sheet', 8), ('id', 11), ('priority', 12), ('category', 13), ('issue', 60), ('evidence', 45),
            ('change_needed', 50), ('revise_how', 50), ('who', 18), ('confidence', 11), ('verdict', 11),
            ('correction', 45), ('overridden', 10), ('rect_source', 11), ('location', 30)]
    ws.append([c.upper().replace('_', ' ') for c, _ in COLS])
    fill = {'MUST': 'F8CBAD', 'SHOULD': 'FFE699', 'NICE': 'DDEBF7'}
    for r in sorted(rows, key=lambda r: (['MUST', 'SHOULD', 'NICE'].index(pri(r['priority'])), order.index(r['sheet']))):
        ws.append([str(r.get(c) or '') for c, _ in COLS])
        ws.cell(ws.max_row, 3).fill = PatternFill('solid', fgColor=fill[pri(r['priority'])])
    for n, (_, w) in enumerate(COLS, 1):
        ws.column_dimensions[ws.cell(1, n).column_letter].width = w
    for row in ws.iter_rows(min_row=2):
        for c in row:
            c.alignment = Alignment(wrap_text=True, vertical='top')
    for c in ws[1]:
        c.font = Font(bold=True)
    ws.freeze_panes = 'C2'
    ws.auto_filter.ref = ws.dimensions
    wb.save(os.path.join(od, f'{a.name}_CHANGE_LIST.xlsx'))

    placed = sum(1 for l in log if l[2] in ('located', 'approximate'))
    print(f"sheets {len(order)} items {len(rows)} placed {placed} located {sum(1 for l in log if l[2] == 'located')} "
          f"skipped {len(log) - placed} off_page {len(bad)} overrides {len(OVR)} "
          f"(unused {len(unused_ovr)}, without _why {len(no_why)}) -> {pdf}")
    sys.exit(1 if bad else 0)


if __name__ == '__main__':
    main()
