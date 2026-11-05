"""ENC-C2: plan tight revision clouds that never enclose unchanged content.

Inputs (manifests/): ENC-C2.bbox.txt (enc_c2_dump.lsp), ENC-C2.geom.<handle>.json (xl_ole.ps1 geom),
task manifests ENC-P1/P2/G1/A3/B4/T1/T2 (revised handles / cells).
Output: manifests/ENC-C2.rects.txt  lines "layout|xmin|ymin|xmax|ymax|members"
"""
import json, os, sys
M = os.path.join(os.path.dirname(__file__), '..', 'manifests')
PAD, MAXGAP, CLOUDPAD = 0.04, 1.0, 0.05

VP = {  # layout: (pc, target, vc, k, paper rect)
    'P1.00': ((16.542, 18.250), (-3894.425, 6283.252), (10249.111, -4799.667), 21.380 / 2052.481, (29.069, 21.380)),
    'P1.01': ((16.693, 20.684), (-3894.425, 6283.252), (10243.192, -6603.593), 16.122 / 1547.670, (28.945, 16.122)),
}
# OLE frames whose content is mapped cell-by-cell; changed cells per frame
OLE_CHANGED = {
    '393C7': {'D8', 'D23', 'D29', 'D33', 'L33', 'D37'},           # Bldg A pressure calc
    '385E0': {'D20', 'D30', 'D36', 'D40', 'L40', 'D44'},          # Bldg B pressure calc
}
for tid, h in (('ENC-T1', '393C6'), ('ENC-T2', '3A552')):
    # sizing tables: the whole chart (C4:L6) was replaced by the LADBS row, so every data cell counts as revised
    # (cells whose old value happened to equal the new one included), plus the edited friction label.
    edited = {e['cell'] for e in json.load(open(os.path.join(M, tid + '.task.json')))['edits']}
    OLE_CHANGED[h] = edited | {c + str(r) for c in 'CDEFGHIJKL' for r in (4, 5, 6)}


def load(name):
    return json.load(open(os.path.join(M, name), encoding='utf-8-sig'))


revised = set()
for t in ('ENC-P1', 'ENC-P2', 'ENC-G1', 'ENC-A3', 'ENC-B4'):
    for e in load(t + '.task.json')['edits']:
        revised.add(e['handle'])

ents = []
for line in open(os.path.join(M, 'ENC-C2.bbox.txt'), encoding='latin-1'):
    p = line.rstrip('\n').split('|')
    if p[0] != 'E':
        continue
    h, space, ty, nm, layer = p[1:6]
    x0, y0, x1, y1 = map(float, p[6:10])
    ents.append(dict(h=h, space=space, ty=ty, nm=nm, layer=layer, box=(x0, y0, x1, y1)))


def to_paper(lay, box):
    pc, tg, vc, k, (pw, ph) = VP[lay]
    f = lambda x, y: (pc[0] + (x - tg[0] - vc[0]) * k, pc[1] + (y - tg[1] - vc[1]) * k)
    a, b = f(box[0], box[1]), f(box[2], box[3])
    x0, y0, x1, y1 = min(a[0], b[0]), min(a[1], b[1]), max(a[0], b[0]), max(a[1], b[1])
    vx0, vy0, vx1, vy1 = pc[0] - pw / 2, pc[1] - ph / 2, pc[0] + pw / 2, pc[1] + ph / 2
    if x1 < vx0 or x0 > vx1 or y1 < vy0 or y0 > vy1:
        return None
    return (max(x0, vx0), max(y0, vy0), min(x1, vx1), min(y1, vy1))


targets = {L: [] for L in VP}
obstacles = {L: [] for L in VP}
for e in ents:
    if e['layer'].upper().startswith('N_P_REV') or e['nm'] in ('TB',):
        continue
    if e['space'] == 'Model':
        for L in VP:
            b = to_paper(L, e['box'])
            if b:
                (targets if e['h'] in revised else obstacles)[L].append((b, e['h']))
        continue
    L = e['space']
    if L not in VP:
        continue
    b = e['box']
    if (b[2] - b[0]) > 25 or (b[3] - b[1]) > 18:      # sheet-sized things are not "content"
        continue
    if e['ty'] == 'OLE2FRAME' and e['h'] in OLE_CHANGED:
        g = load('ENC-C2.geom.%s.json' % e['h'])
        # The OLE frame shows the sheet's full width but only the rows that fit its aspect ratio (e.g. down to
        # "SIZE PER"), not necessarily the whole used range, so scale by the visible height, not used height.
        fw, fh = b[2] - b[0], b[3] - b[1]
        vis_h = min(g['height'], g['width'] * fh / fw)
        sx, sy = fw / g['width'], fh / vis_h
        block = None   # sizing tables: the C4:L6 chart block becomes ONE revised area
        for c in g['cells']:
            cb = (b[0] + c['x'] * sx, b[3] - (c['y'] + c['h']) * sy, b[0] + (c['x'] + c['w']) * sx, b[3] - c['y'] * sy)
            is_chart = e['h'] in ('393C6', '3A552') and c['cell'][0] in 'CDEFGHIJKL' and c['cell'][1:] in ('4', '5', '6')
            if is_chart:
                block = cb if block is None else (min(block[0], cb[0]), min(block[1], cb[1]), max(block[2], cb[2]), max(block[3], cb[3]))
                continue
            (targets if c['cell'] in OLE_CHANGED[e['h']] else obstacles)[L].append((cb, e['h'] + '!' + c['cell']))
        if block:
            targets[L].append((block, e['h'] + '!C4:L6'))
        continue
    (targets if e['h'] in revised else obstacles)[L].append((b, e['h']))


def pad(b, d):
    return (b[0] - d, b[1] - d, b[2] + d, b[3] + d)


def union(a, b):
    return (min(a[0], b[0]), min(a[1], b[1]), max(a[2], b[2]), max(a[3], b[3]))


def hits(a, b, eps=0.01):
    return a[0] < b[2] - eps and a[2] > b[0] + eps and a[1] < b[3] - eps and a[3] > b[1] + eps


def gap(a, b):
    dx = max(0, max(a[0], b[0]) - min(a[2], b[2]))
    dy = max(0, max(a[1], b[1]) - min(a[3], b[3]))
    return (dx * dx + dy * dy) ** 0.5


out = []
for L in VP:
    cl = [[pad(b, PAD), [h]] for b, h in targets[L]]
    obs = [b for b, _ in obstacles[L]]
    merged = True
    while merged:
        merged = False
        pairs = sorted((gap(cl[i][0], cl[j][0]), i, j) for i in range(len(cl)) for j in range(i + 1, len(cl)))
        for g, i, j in pairs:
            if g > MAXGAP:
                break
            u = union(cl[i][0], cl[j][0])
            if any(hits(u, o) for o in obs):
                continue
            cl[i] = [u, cl[i][1] + cl[j][1]]
            del cl[j]
            merged = True
            break
    for b, mem in cl:
        c = pad(b, CLOUDPAD - PAD)
        out.append('%s|%.4f|%.4f|%.4f|%.4f|%s' % (L, c[0], c[1], c[2], c[3], ' '.join(sorted(mem))))
    print(L, 'targets', len(targets[L]), 'obstacles', len(obs), 'clouds', len(cl))
open(os.path.join(M, 'ENC-C2.rects.txt'), 'w').write('\n'.join(out) + '\n')
print('total clouds', len(out))
