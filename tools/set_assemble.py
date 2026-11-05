"""Set assembly helper (orchestrator runs it; set-assembler reviews its outputs). PyMuPDF.

Usage:
  python set_assemble.py map    <set.pdf> <out.json>
        page -> sheet id from the level-2 bookmarks (e.g. "P003 - PLUMBING CALCULATIONS-P0.03" -> "P0.03").
  python set_assemble.py diff   <old.pdf> <old_sheet_or_page> <new.pdf> <new_sheet_or_page> <out_dir> [--dpi 60]
        renders both pages, writes <out_dir>/diff_<id>.png (changes in red over a grey copy) and
        diff_<id>.json with clustered change boxes in paper inches (x0,y0,x1,y1; y up from the bottom edge) and
        in unrotated PDF points. Raster cut sheets re-sample with tiny shifts: boxes of only scattered pixels are
        marked "noise_suspect".
  python set_assemble.py index  <set.pdf> <index.txt> <out.json>
        index.txt: one sheet id per line (e.g. "P4.01-S"; ranges "P4.01-S TO P4.22-S" expand). Compares with the set.
  python set_assemble.py splice <set.pdf> <out.pdf> <SHEET>=<file.pdf>[:<page>] [...]
        replaces the pages of the named sheets, keeps bookmarks, writes a NEW file. Refuses if out.pdf exists or is
        the input. Prints the page map of replaced pages.
"""
import json
import os
import re
import sys

import pymupdf

SID = re.compile(r"-((?:\(N\))?P?-?\d[\w.]*(?:-[SWG])?(?:-1)?)$")


def sheet_ids(doc):
    toc = doc.get_toc()
    lvl = max((t[0] for t in toc), default=1)
    ids = {}
    for t in toc:
        if t[0] != lvl:
            continue
        m = SID.search(t[1])
        s = (m.group(1).replace("(N)", "") if m else t[1])
        s = s if s.startswith("P") else "P" + s
        ids.setdefault(t[2] - 1, s)   # first bookmark for a page wins (a re-saved set had P4.11-S pointing at page 1)
    return [ids.get(i) for i in range(len(doc))]


def repair_toc(toc, npages):
    """If exactly one page has two bookmarks and exactly one page has none, move the LATER duplicate to the empty
    page (22-001: a Bluebeam re-save pointed P4.11-S at page 1; page 90 had none). Returns (toc, note or None)."""
    lvl = max((t[0] for t in toc), default=1)
    pages = [t[2] for t in toc if t[0] == lvl]
    dup = [p for p in set(pages) if pages.count(p) > 1]
    missing = sorted(set(range(1, npages + 1)) - set(pages))
    if len(dup) != 1 or len(missing) != 1:
        return toc, (None if not dup and not missing else f"not repaired: duplicates {dup}, missing {missing}")
    idx = [i for i, t in enumerate(toc) if t[0] == lvl and t[2] == dup[0]][-1]
    toc = [list(t) for t in toc]
    note = f"bookmark '{toc[idx][1]}' moved from page {dup[0]} to page {missing[0]}"
    toc[idx][2] = missing[0]
    return toc, note


def page_of(doc, key):
    if re.fullmatch(r"\d+", key):
        return int(key) - 1
    ids = sheet_ids(doc)
    if key not in ids:
        sys.exit(f"sheet {key} not in bookmarks")
    return ids.index(key)


def cmd_map(a):
    d = pymupdf.open(a[0])
    ids = sheet_ids(d)
    json.dump({"pages": len(d), "map": [{"page": i + 1, "sheet": s} for i, s in enumerate(ids)]},
              open(a[1], "w"), indent=1)
    print(f"map: {len(d)} pages, {sum(1 for s in ids if s)} with sheet ids -> {a[1]}")


def cmd_diff(a):
    dpi = 60
    if "--dpi" in a:
        dpi = int(a[a.index("--dpi") + 1]); a = a[:a.index("--dpi")]
    od, nd = pymupdf.open(a[0]), pymupdf.open(a[2])
    op, np_ = od[page_of(od, a[1])], nd[page_of(nd, a[3])]
    out = a[4]; os.makedirs(out, exist_ok=True)
    pa, pb = np_.get_pixmap(dpi=dpi), op.get_pixmap(dpi=dpi)
    if (pa.width, pa.height) != (pb.width, pb.height):
        sys.exit("page sizes differ")
    w, h, n = pa.width, pa.height, pa.n
    A, B = pa.samples, pb.samples
    mask = bytearray(w * h)
    img = bytearray(w * h * 3)
    for y in range(h):
        for x in range(w):
            i = (y * w + x) * n
            if abs(A[i] - B[i]) > 60 or abs(A[i + 1] - B[i + 1]) > 60:
                mask[y * w + x] = 1; img[(y * w + x) * 3:(y * w + x) * 3 + 3] = b"\xff\x00\x00"
            else:
                g = A[i]; v = 255 if g > 250 else 200 + g // 5
                img[(y * w + x) * 3:(y * w + x) * 3 + 3] = bytes((v, v, v))
    # cluster on a coarse grid (cell ~0.5 in)
    cell = max(4, int(dpi * 0.5))
    gw, gh = (w + cell - 1) // cell, (h + cell - 1) // cell
    cnt = [[0] * gw for _ in range(gh)]
    for y in range(h):
        for x in range(w):
            if mask[y * w + x]:
                cnt[y // cell][x // cell] += 1
    on = {(r, c) for r in range(gh) for c in range(gw) if cnt[r][c] >= 3}
    boxes, seen = [], set()
    for start in sorted(on):
        if start in seen:
            continue
        stack, comp = [start], []
        seen.add(start)
        while stack:
            r, c = stack.pop(); comp.append((r, c))
            for dr in (-1, 0, 1):
                for dc in (-1, 0, 1):
                    q = (r + dr, c + dc)
                    if q in on and q not in seen:
                        seen.add(q); stack.append(q)
        rs = [p[0] for p in comp]; cs = [p[1] for p in comp]
        px = (min(cs) * cell, min(rs) * cell, min(w, (max(cs) + 1) * cell), min(h, (max(rs) + 1) * cell))
        npx = sum(cnt[r][c] for r, c in comp)
        dens = npx / max(1, (px[2] - px[0]) * (px[3] - px[1]))
        Hin = h / dpi
        box_in = [round(px[0] / dpi, 2), round(Hin - px[3] / dpi, 2), round(px[2] / dpi, 2), round(Hin - px[1] / dpi, 2)]
        r = pymupdf.Rect(px[0] * 72 / dpi, px[1] * 72 / dpi, px[2] * 72 / dpi, px[3] * 72 / dpi) * np_.derotation_matrix
        r.normalize()
        boxes.append({"paper_in": box_in, "pdf_pt_unrotated": [round(v, 1) for v in (r.x0, r.y0, r.x1, r.y1)],
                      "changed_px": npx, "density": round(dens, 4), "noise_suspect": dens < 0.01})
    tag = re.sub(r"[^\w.-]", "_", a[3])
    pymupdf.Pixmap(pymupdf.csRGB, w, h, bytes(img), False).save(os.path.join(out, f"diff_{tag}.png"))
    json.dump({"old": [a[0], a[1]], "new": [a[2], a[3]], "dpi": dpi, "boxes": boxes},
              open(os.path.join(out, f"diff_{tag}.json"), "w"), indent=1)
    print(f"diff: {len(boxes)} change box(es), {sum(1 for b in boxes if b['noise_suspect'])} noise-suspect -> {out}")


def cmd_index(a):
    d = pymupdf.open(a[0])
    have = [s for s in sheet_ids(d) if s]
    want = []
    for ln in open(a[1], encoding="utf-8").read().splitlines():
        s = ln.strip().split("\t")[0].split("|")[0].strip()
        if not s:
            continue
        m = re.match(r"^(P\d\.)(\d\d)(-\w+) TO P\d\.(\d\d)-\w+$", s)
        if m:
            want += [f"{m.group(1)}{k:02d}{m.group(3)}" for k in range(int(m.group(2)), int(m.group(4)) + 1)]
        else:
            want.append(s)
    res = {"set_sheets": len(have), "index_sheets": len(want),
           "missing_from_index": [s for s in have if s not in want],
           "not_in_set": [s for s in want if s not in have],
           "duplicates_in_index": sorted({s for s in want if want.count(s) > 1})}
    json.dump(res, open(a[2], "w"), indent=1)
    print(f"index: set {len(have)}, index {len(want)}, missing {len(res['missing_from_index'])}, "
          f"extra {len(res['not_in_set'])} -> {a[2]}")


def cmd_splice(a):
    src, out = a[0], a[1]
    if os.path.abspath(src) == os.path.abspath(out) or os.path.exists(out):
        sys.exit("refusing: output exists or equals the input (write a new dated file)")
    s = pymupdf.open(src)
    ids = sheet_ids(s)
    rep = {}
    for spec in a[2:]:
        sh, f = spec.split("=", 1)
        pg = 0
        if re.search(r":\d+$", f):
            f, pg = f.rsplit(":", 1); pg = int(pg) - 1
        if sh not in ids:
            sys.exit(f"sheet {sh} not in set bookmarks")
        rep[ids.index(sh)] = (f, pg, sh)
    d = pymupdf.open()
    for i in range(len(s)):
        if i in rep:
            f, pg, sh = rep[i]
            d.insert_pdf(pymupdf.open(f), from_page=pg, to_page=pg)
        else:
            d.insert_pdf(s, from_page=i, to_page=i)
    toc = s.get_toc()
    note = None
    if toc and REPAIR:
        toc, note = repair_toc(toc, len(s))
    if toc:
        d.set_toc(toc)
    d.save(out, garbage=3, deflate=True)
    c = pymupdf.open(out)
    print(json.dumps({"out": out, "pages": len(c), "input_pages": len(s), "toc_repair": note,
                      "replaced": [{"page": i + 1, "sheet": v[2], "from": v[0]} for i, v in sorted(rep.items())]}))
    if len(c) != len(s):
        sys.exit("page count changed!")


REPAIR = "--repair-toc" in sys.argv
if __name__ == "__main__":
    cmd, args = sys.argv[1], [x for x in sys.argv[2:] if x != "--repair-toc"]
    {"map": cmd_map, "diff": cmd_diff, "index": cmd_index, "splice": cmd_splice}[cmd](args)
