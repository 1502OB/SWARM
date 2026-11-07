"""Write references/core.md: the AutoLISP / vla names our swarm .lsp files actually call, most-used first, with
the signature from this skill and firm notes (facts that cost failed runs; source in brackets).

    python gen_core.py C:/Users/Jeremy/swarm        (scans **/*.lsp except build/ and snapshots/)
"""
import collections
import sys
from datetime import date
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
import acad_api_check as ac  # noqa: E402

NOTES = {
    "entget": "Returns the entity's DXF list (the help data's return type 'Ename' is wrong). Group codes: dxf-entities.md.",
    "entdel": "entdel on an already-deleted ename UNDELETES it: entdel only enames whose entget is non-nil (scripting-lessons).",
    "entmake": "Group 0 first (or second after -1); R13+ types (LWPOLYLINE, MTEXT, ...) need (100 . \"AcDbEntity\") + their subclass marker; cannot make VIEWPORTs; returns nil on failure: re-query, never assume (dxf-entities.md notes, acad-conventions).",
    "entmod": "Cannot change the type, handle 5, internal fields (-1/-2) or any VIEWPORT entity; referenced styles/blocks must exist (a new layer name is created); after a subentity change call (entupd main-ename) (dxf-entities.md notes).",
    "command": "OSMODE is 16383 here: pass points with \"_non\" or build with entmake/ActiveX (scripting-lessons). Use \"_.\" English command names.",
    "vl-cmdf": "Same snap rule as command.",
    "vla-getboundingbox": "Out args are quoted symbols set to variants: (vla-GetBoundingBox o 'mn 'mx) then (vlax-safearray->list mn). For CLOUDS use the VERTEX bbox (group 10): the bbox adds the arc bulge (scripting-lessons, 22-001 P3FIX).",
    "vla-explode": "Declared on BlockReference, LWPolyline, Polyline, 3DPolyline, PolygonMesh, Region only: MText has NO Explode (vla-explode fails); explode a temp copy with (command \"_.EXPLODE\" e) (scripting-lessons).",
    "vla-item": "Raises an error on a missing key: wrap in vl-catch-all-apply ('vla-item (list coll key)) and test vl-catch-all-error-p.",
    "vla-setbulge": "LWPolyline/Polyline: (vla-SetBulge pl index bulge), vertex index from 0. Sagitta = |bulge| x chord / 2 (clouds).",
    "vla-addlightweightpolyline": "Declared on Block (so ModelSpace, PaperSpace, any block def): VerticesList = variant of a double safearray x1 y1 x2 y2 ... (2D, no z).",
    "vla-addtext": "Declared on Block: (vla-AddText space str (vlax-3d-point x y 0) height). Insertion point is a variant 3D point.",
    "vla-get-handle": "Declared on Object (every entity and table record): READ-ONLY.",
    "vla-update": "Declared on Entity, Application, Group, SelectionSet: regenerates the object's display.",
    "vla-startundomark": "On Document: pair with vla-EndUndoMark in every exit path (acad-conventions: one UNDO group per RUN).",
    "vla-endundomark": "On Document; see vla-StartUndoMark.",
    "vla-copy": "Declared on Entity: returns the copy in the same space; move it with vla-Move (variant 3D points).",
    "vla-move": "Declared on Entity: (vla-Move o (vlax-3d-point from) (vlax-3d-point to)).",
    "vla-put-closed": "On LWPolyline, Polyline, 3DPolyline, Spline, LoftedSurface; value :vlax-true / :vlax-false.",
    "vla-put-layer": "Declared on Entity. The layer must exist (vla-Add on Layers first).",
    "vla-get-layouts": "On Document. Layout objects; a layout's Block is its paper space (vla-get-Block).",
    "vla-get-activedocument": "On Application. Target documents by name and make sure the target is active before ActiveX work (scripting-lessons).",
    "vlax-get-acad-object": "Application object; needs (vl-load-com) once per session.",
    "vlax-ename->vla-object": "ename -> VLA object; vlax-vla-object->ename back.",
    "vlax-make-safearray": "Then vlax-safearray-fill and vlax-make-variant to pass coordinates; or use vlax-3d-point for one point.",
    "vlax-invoke": "Undocumented but works here (returns LISP lists, no variants). Documented form: vlax-invoke-method.",
    "vlax-get": "Undocumented but works here (LISP values). Documented form: vlax-get-property.",
    "vlax-put": "Undocumented but works here. Documented form: vlax-put-property.",
    "ssget": "\"_X\" with a filter list reads the whole drawing (no pick); (410 . \"Model\") / layout name filters by space.",
    "tblsearch": "Returns the table entry DXF list or nil; LAYER 70 flags and negative 62 = off (dxf-entities.md).",
    "vl-catch-all-apply": "Second argument must be a LIST of arguments; test the result with vl-catch-all-error-p.",
    "vl-string-search": "Case-sensitive, 0-based, nil when absent.",
    "getvar": "Restore every sysvar you change (OSMODE, FILEDIA, CMDDIA, BACKGROUNDPLOT, CANNOTATIONSCALE) (acad-conventions).",
    "setvar": "See getvar: restore what you change, also in *error*.",
}
AVOID = [
    ("fboundp / identity", "do not exist (AutoLISP is not Common Lisp): use (type 'sym) / (lambda (x) x) (scripting-lessons)."),
    ("stringp", "does not exist: (= (type x) 'STR). Found in scripts/C22-TB-T1.v1.lsp:361 by this checker (2026-11-06)."),
    ("vla-put-Bulge / vla-GetTransform", "do not exist (use vla-SetBulge / vla-TransformBy)."),
    ("MText.Explode", "does not exist (see vla-Explode)."),
    ("Viewport.*", "Viewport is the tiled viewport table record; the paper-space viewport entity is PViewport (IAcadPViewport). Per-viewport frozen layers are in its ACAD xdata (1003), not DXF 341 (scripting-lessons)."),
    ("Hatch.PatternName put", "READ-ONLY: use vla-SetPattern."),
]


def main(root):
    root = Path(root)
    ck = ac.Checker()
    count, files = collections.Counter(), collections.defaultdict(set)
    lsps = [p for p in root.rglob("*.lsp") if not any(x in p.parts for x in ("build", "snapshots", "northgate"))]  # northgate = junction
    for p in lsps:
        toks = ac.tokenize(p.read_text(encoding="utf-8", errors="replace"))
        defs = {toks[i + 1][1].lower() for i, (k, v, _) in enumerate(toks)
                if k == "sym" and v.lower() == "defun" and i + 1 < len(toks)}
        for i, (k, v, _) in enumerate(toks):
            if k in ("(",) and i + 1 < len(toks) and toks[i + 1][0] == "sym":
                n = toks[i + 1][1].lower()
                if n in defs or not (n in ck.funcs or n in ck.vla or n in ac.UNDOC):
                    continue
                count[n] += 1
                files[n].add(p.name)
    api = ("vla-", "vlax-", "vl-", "ent", "ss", "tbl", "dict", "namedobjdict", "getvar", "setvar", "command",
           "vl-cmdf", "regapp", "handent", "xd", "layoutlist", "trans", "osnap", "acad_", "getpropertyvalue",
           "setpropertyvalue", "textbox", "polar", "angle", "distance", "inters", "redraw", "findfile", "open",
           "write-line", "read-line", "close", "load", "startapp")
    rows = [n for n, c in count.most_common() if (n.startswith(api) and c >= 2) or n in NOTES][:110]
    out = [f"# Core AutoLISP / ActiveX names used by our swarm scripts", "",
           f"Generated {date.today()} by scripts/gen_core.py from {len(lsps)} .lsp files under {root} (calls counted"
           " in function position; drawing / ActiveX / file API only, list and math primitives left out). Look here first; everything else: the checker, activex-INDEX.md,"
           " autolisp-functions.md, dxf-entities.md. Notes are facts that cost failed runs or were verified in the"
           " 2027 type library.", "",
           "| Name | Calls | Files | Signature (2027) | Note |", "|---|---|---|---|---|"]
    for n in rows:
        if n.startswith("vla-"):
            h = ck.vla[n][0]
            sig = ac.fmt_method(h[1][4:], h[3]) if h[2] == "method" else \
                h[1] + " on " + ", ".join(sorted({ac.short(x[0]) for x in ck.vla[n]})[:4])
            disp = h[1]
        elif n in ck.funcs:
            sig, disp = ck.funcs[n][0]["signature"], ck.al["names"].get(n, n)
        else:
            sig, disp = "(undocumented)", n
        out.append(f"| {disp} | {count[n]} | {len(files[n])} | `{sig.replace('|', '/')}` | {NOTES.get(n, '')} |")
    out += ["", "## Names that do NOT exist (memory gets these wrong)", ""]
    out += [f"- **{a}**: {b}" for a, b in AVOID]
    (ac.REFS / "core.md").write_text("\n".join(out) + "\n", encoding="utf-8")
    print(f"core.md: {len(rows)} names from {len(lsps)} files")


if __name__ == "__main__":
    main(sys.argv[1] if len(sys.argv) > 1 else "C:/Users/Jeremy/swarm")
