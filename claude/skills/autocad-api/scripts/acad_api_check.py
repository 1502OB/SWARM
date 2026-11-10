"""Check AutoLISP / ActiveX (vla-) / DXF names against this skill's AutoCAD 2027 references.

    python ~/.claude/skills/autocad-api/scripts/acad_api_check.py NAME [NAME ...]
    python .../acad_api_check.py --lsp script.lsp [more.lsp ...] [--with lib.lsp]   (every call in the file)
    python .../acad_api_check.py --object Line                       (all members, inherited ones too)
    python .../acad_api_check.py --dxf LWPOLYLINE                    (group codes of one entity)

NAME forms:
    vla-AddText  vla-get-Handle  vla-put-Closed   ActiveX wrapper: FOUND lists the objects that declare it
    Line.Delete  AcadLine.Layer  IAcadLine.StartPoint   one object's member, inheritance followed
    entget  vl-string-search  vlax-make-safearray      AutoLISP function
    dxf:LWPOLYLINE.42  dxf:TEXT.72                     DXF group code (COMMON codes apply to every entity)

--lsp skips functions the file defines itself (defun) or that a --with file / a (load "...") target defines, locals, quoted data lists and dotted pairs, and also
checks member names passed as 'Symbol or "String" to vlax-get / vlax-put / vlax-invoke / vlax-get-property /
vlax-put-property / vlax-invoke-method / vlax-property-available-p / vlax-method-applicable-p.

Status: FOUND, DOC-ONLY (in Autodesk's 2021 help data but not in the 2027 type library: assume it does not exist
unless a read-only vlax-property-available-p / vlax-method-applicable-p test on the live bridge says so) and
NOT FOUND (close matches suggested). Exit 0 = all FOUND, 1 = any DOC-ONLY / NOT FOUND, 2 = usage error.
"""
import difflib
import json
import re
import sys
from pathlib import Path

REFS = Path(__file__).resolve().parent.parent / "references"
VLAX_MEMBER_FUNCS = {"vlax-get": "P", "vlax-put": "P", "vlax-get-property": "P", "vlax-put-property": "P",
                     "vlax-property-available-p": "P", "vlax-invoke": "M", "vlax-invoke-method": "M",
                     "vlax-method-applicable-p": "M"}
# Special forms and words the help data does not list as functions but the interpreter accepts.
LANG_EXTRA = {"t", "nil", "pi", "lambda", "function", "quote", "defun-q", "*error*", "pause"}
AMBIG = {"viewport": "Viewport = the tiled model viewport table record (IAcadViewport); the paper-space viewport "
                     "ENTITY (DXF VIEWPORT, ObjectName AcDbViewport) is PViewport (IAcadPViewport)",
         "polyline": "Polyline = old heavy 2D POLYLINE (IAcadPolyline); the usual one is LWPolyline (AcDbPolyline)",
         "block": "Block = a block definition / ModelSpace / PaperSpace (IAcadBlock); an inserted block is BlockReference"}
CXR = re.compile(r"^c[ad]{1,4}r$", re.I)   # AutoLISP supports car/cdr combinations up to four levels deep
# Real but undocumented in the AutoLISP Reference (they work on this bridge: used by scripts that ran OK).
UNDOC = {"vlax-invoke": "undocumented legacy form of vlax-invoke-method (returns LISP values, not variants)",
         "vlax-get": "undocumented legacy form of vlax-get-property (returns LISP values, not variants)",
         "vlax-put": "undocumented legacy form of vlax-put-property (accepts LISP values)"}


def load():
    try:
        ax = json.loads((REFS / "activex.json").read_text(encoding="utf-8"))
        al = json.loads((REFS / "autolisp.json").read_text(encoding="utf-8"))
    except FileNotFoundError as e:
        print(f"ERROR reference missing: {e}")
        sys.exit(2)
    ifaces = ax["interfaces"]
    alias = {}
    for n, it in ifaces.items():
        names = {n, n[1:] if n.startswith("I") else n}
        for c in it["coclass"]:
            names.add(c)
        for x in list(names):
            if x.startswith("Acad"):
                names.add(x[4:])
        for x in names:
            alias.setdefault(x.lower(), n)
    vla = {}   # lower call name -> [(iface, display, detail)]
    for n, it in ifaces.items():
        if it.get("events"):
            continue
        for m, d in it["methods"].items():
            vla.setdefault(f"vla-{m}".lower(), []).append((n, f"vla-{m}", "method", d))
        for p, d in it["properties"].items():
            if d["get"]:
                vla.setdefault(f"vla-get-{p}".lower(), []).append((n, f"vla-get-{p}", "get", d))
            if d["put"] or d["putref"]:
                vla.setdefault(f"vla-put-{p}".lower(), []).append((n, f"vla-put-{p}", "put", d))
    dxf = load_dxf()
    return ifaces, alias, vla, al, dxf


def load_dxf():
    f = REFS / "dxf-entities.md"
    out, cur = {}, None
    if not f.exists():
        return out
    for line in f.read_text(encoding="utf-8").splitlines():
        m = re.match(r"^##\s+([A-Z0-9_]+)\b", line)
        if m:
            cur = out.setdefault(m.group(1), {})
            continue
        if cur is not None and line.startswith("|"):
            cells = [c.strip() for c in line.strip().strip("|").split("|")]
            if not cells or not re.match(r"^`?-?\d", cells[0]):
                continue
            meaning = cells[1] if len(cells) > 1 else ""
            for code in re.findall(r"-?\d+", cells[0].split("(")[0]):
                cur.setdefault(int(code), meaning)
    return out


def chain(ifaces, n):
    seen, order, todo = set(), [], [n]
    while todo:
        x = todo.pop(0)
        if x in seen or x not in ifaces:
            continue
        seen.add(x)
        order.append(x)
        todo += ifaces[x]["inherits"]
    return order


def fmt_method(name, d):
    args = " ".join((f"'{a['name']}" if a["out"] else a["name"]) + f"<{a['type']}>" +
                    ("?" if a["opt"] else "") for a in d["args"])
    ret = "" if d["ret"] in ("void", "HRESULT") else f" -> {d['ret']}"
    return f"(vla-{name} obj{' ' + args if args else ''}){ret}"


def fmt_prop(name, d):
    acc = ("get" if d["get"] else "") + ("/put" if d["put"] or d["putref"] else "")
    return f"{name} : {d['type']} [{acc.strip('/')}{'' if d['put'] or d['putref'] else ' READ-ONLY'}]"


def short(n):
    return n[5:] if n.startswith("IAcad") else n


class Checker:
    def __init__(self):
        self.ifaces, self.alias, self.vla, self.al, self.dxf = load()
        self.funcs = self.al["functions"]
        self.doc_vla = self.al.get("vla_doc", {})
        self.found = self.missing = 0
        self.libdefs = {}          # lower name -> file, from --with files and (load "...") targets

    def add_lib(self, path):
        p = Path(path)
        if not p.exists():
            print(f"WARNING --with/load target not found: {path}")
            return
        toks = tokenize(p.read_text(encoding="utf-8", errors="replace"))
        for i, (k, v, _) in enumerate(toks):
            if k == "sym" and v.lower() in ("defun", "defun-q") and i + 1 < len(toks):
                self.libdefs.setdefault(toks[i + 1][1].lower(), p.name)

    def report(self, ok, text, status=None):
        if ok:
            self.found += 1
            print(f"FOUND      {text}")
        else:
            self.missing += 1
            print(f"{status or 'NOT FOUND':<10} {text}")

    def suggest(self, name, pool):
        return difflib.get_close_matches(name.lower(), pool, n=4, cutoff=0.75)

    def check_vla(self, name, ctx=""):
        hits = self.vla.get(name.lower())
        if hits:
            objs = sorted({short(h[0]) for h in hits})
            h = hits[0]
            sig = fmt_method(h[1][4:], h[3]) if h[2] == "method" else fmt_prop(re.sub(r"^vla-(get|put)-", "", h[1]), h[3])
            more = f" (+{len(objs) - 6} more)" if len(objs) > 6 else ""
            self.report(True, f"{h[1]}{ctx}  on {', '.join(objs[:6])}{more}  ; {sig}")
            return
        if name.lower() in self.doc_vla:
            self.report(False, f"{name}{ctx}  documented in the 2021 help data, absent from the 2027 type library",
                        "DOC-ONLY")
            return
        s = self.suggest(name, self.vla.keys())
        self.report(False, f"{name}{ctx}" + (f"  -> did you mean: {', '.join(s)}" if s else ""))

    def resolve(self, obj):
        return self.alias.get(obj.lower())

    def check_member(self, obj, member, kind=None, ctx=""):
        n = self.resolve(obj)
        if not n:
            s = difflib.get_close_matches(obj.lower(), self.alias.keys(), n=4, cutoff=0.7)
            self.report(False, f"{obj}.{member}{ctx}  (no such object" + (f"; did you mean {', '.join(s)}" if s else "") + ")")
            return
        m = re.match(r"^(get|put)-(.+)$", member, re.I)
        want = None
        if m:
            want, member = m.group(1).lower(), m.group(2)
        for x in chain(self.ifaces, n):
            it = self.ifaces[x]
            meth = {k.lower(): (k, v) for k, v in it["methods"].items()}.get(member.lower())
            prop = {k.lower(): (k, v) for k, v in it["properties"].items()}.get(member.lower())
            if meth and kind != "P" and not want:
                self.report(True, f"{short(n)}.{meth[0]}{ctx}  declared on {x}  ; {fmt_method(meth[0], meth[1])}")
                return
            if prop and kind != "M":
                d = prop[1]
                if want == "put" and not (d["put"] or d["putref"]):
                    self.report(False, f"{short(n)}.put-{prop[0]}{ctx}  property is READ-ONLY (declared on {x})")
                    return
                self.report(True, f"{short(n)}.{prop[0]}{ctx}  declared on {x}  ; {fmt_prop(prop[0], d)}")
                return
        pool = []
        for x in chain(self.ifaces, n):
            pool += list(self.ifaces[x]["methods"]) + list(self.ifaces[x]["properties"])
        s = difflib.get_close_matches(member, pool, n=4, cutoff=0.6)
        others = sorted({short(h[0]) for h in self.vla.get(f"vla-{member}".lower(), []) +
                         self.vla.get(f"vla-get-{member}".lower(), [])})
        hint = (f"  -> did you mean: {', '.join(s)}" if s else "") + \
               (f"  (exists on: {', '.join(others[:6])})" if others else "")
        if obj.lower() in AMBIG:
            hint += f"  NOTE: {AMBIG[obj.lower()]}"
        self.report(False, f"{short(n)}.{member}{ctx}  not on {short(n)} or its bases{hint}")

    def check_any_member(self, member, kind, ctx=""):
        """Member named via vlax-get/vlax-invoke without a known object: exists on some interface?"""
        keys = [f"vla-{member}"] if kind == "M" else [f"vla-get-{member}", f"vla-put-{member}"]
        for k in keys:
            if self.vla.get(k.lower()):
                objs = sorted({short(h[0]) for h in self.vla[k.lower()]})
                self.report(True, f"'{member}{ctx}  {'method' if kind == 'M' else 'property'} on "
                                  f"{', '.join(objs[:6])}{' ...' if len(objs) > 6 else ''}")
                return
        pool = [k[4:] for k in self.vla if not k.startswith(("vla-get-", "vla-put-"))] if kind == "M" else \
            [k[8:] for k in self.vla if k.startswith("vla-get-")]
        s = difflib.get_close_matches(member.lower(), pool, n=4, cutoff=0.75)
        self.report(False, f"'{member}{ctx}  no {'method' if kind == 'M' else 'property'} of that name"
                    + (f"  -> did you mean: {', '.join(s)}" if s else ""))

    def check_func(self, name, ctx=""):
        lo = name.lower()
        if lo.startswith("vla-"):
            return self.check_vla(name, ctx)
        if lo in self.funcs:
            e = self.funcs[lo][0]
            self.report(True, f"{name}{ctx}  ; {e['signature']}")
            return
        if lo in LANG_EXTRA or CXR.match(lo):
            self.report(True, f"{name}{ctx}  ; language symbol")
            return
        if lo in UNDOC:
            self.report(True, f"{name}{ctx}  ; {UNDOC[lo]}")
            return
        if lo in self.libdefs:
            self.report(True, f"{name}{ctx}  ; defined in {self.libdefs[lo]}")
            return
        if lo.startswith("c:"):
            self.report(False, f"{name}{ctx}  command function not defined in this file", "UNDEFINED")
            return
        s = self.suggest(name, self.funcs.keys())
        self.report(False, f"{name}{ctx}" + (f"  -> did you mean: {', '.join(s)}" if s else ""))

    def check_dxf(self, ent, code):
        ent = ent.upper()
        if not self.dxf:
            self.report(False, f"dxf:{ent}.{code}  references/dxf-entities.md missing", "NO DATA")
            return
        if ent not in self.dxf:
            s = difflib.get_close_matches(ent, self.dxf.keys(), n=4, cutoff=0.6)
            self.report(False, f"dxf:{ent}.{code}  no section for {ent}" + (f"; did you mean {', '.join(s)}" if s else ""))
            return
        for sec in (ent, "COMMON"):
            if code in self.dxf.get(sec, {}):
                self.report(True, f"dxf:{ent}.{code}  ({sec}) {self.dxf[sec][code]}")
                return
        self.report(False, f"dxf:{ent}.{code}  codes on {ent}: {', '.join(map(str, sorted(self.dxf[ent])))}")

    def check(self, tok):
        m = re.match(r"^dxf:([A-Za-z0-9_]+)\.(-?\d+)$", tok)
        if m:
            return self.check_dxf(m.group(1), int(m.group(2)))
        m = re.match(r"^([A-Za-z_]\w*)\.([A-Za-z_][\w-]*)$", tok)
        if m and not tok.lower().startswith(("vla-", "vlax-", "vl-")):
            return self.check_member(m.group(1), m.group(2))
        return self.check_func(tok)

    def list_object(self, obj):
        n = self.resolve(obj)
        if not n:
            print(f"NOT FOUND object {obj}")
            return 1
        for x in chain(self.ifaces, n):
            it = self.ifaces[x]
            print(f"== {x}" + (f" ({', '.join(it['coclass'])})" if it["coclass"] else "") +
                  (f"  inherits {', '.join(it['inherits'])}" if it["inherits"] else ""))
            for k, v in sorted(it["methods"].items(), key=lambda kv: kv[0].lower()):
                print(f"   M {fmt_method(k, v)}")
            for k, v in sorted(it["properties"].items(), key=lambda kv: kv[0].lower()):
                print(f"   P {fmt_prop(k, v)}")
        return 0

    def list_dxf(self, ent):
        ent = ent.upper()
        if ent not in self.dxf:
            print(f"NOT FOUND dxf section {ent}; have: {', '.join(sorted(self.dxf))}")
            return 1
        for c, mtxt in sorted(self.dxf[ent].items()):
            print(f"   {c:>5}  {mtxt}")
        return 0

    # ---- .lsp scanning -------------------------------------------------------------------------------
    def check_lsp(self, path):
        src = Path(path).read_text(encoding="utf-8", errors="replace")
        toks = tokenize(src)
        for i, (k, v, _) in enumerate(toks):   # follow (load "file") when the file exists
            if k == "sym" and v.lower() == "load" and i + 1 < len(toks) and toks[i + 1][0] == "str":
                tgt = toks[i + 1][1].replace("\\\\", "/")
                for cand in (Path(tgt), Path(tgt + ".lsp"), Path(path).parent / tgt, Path(path).parent / (tgt + ".lsp")):
                    if cand.is_file():
                        self.add_lib(cand)
                        break
        defined, locals_ = set(), set()
        for i, (k, v, _) in enumerate(toks):   # defun names and their parameter / local lists
            if k == "sym" and v.lower() in ("defun", "defun-q") and i + 1 < len(toks):
                defined.add(toks[i + 1][1].lower())
                if i + 2 < len(toks) and toks[i + 2][0] == "(":
                    j = i + 3
                    while j < len(toks) and toks[j][0] != ")":
                        if toks[j][0] == "sym" and toks[j][1] != "/":
                            locals_.add(toks[j][1].lower())
                        j += 1
            if k == "sym" and v.lower() == "lambda" and i + 1 < len(toks) and toks[i + 1][0] == "(":
                j = i + 2
                while j < len(toks) and toks[j][0] != ")":
                    if toks[j][0] == "sym" and toks[j][1] != "/":
                        locals_.add(toks[j][1].lower())
                    j += 1
            if k == "sym" and v.lower() in ("setq", "set") and i + 1 < len(toks):
                j = i + 1     # (setq a 1 b 2): variable names may later hold a function
                while j < len(toks) and toks[j][0] == "sym":
                    locals_.add(toks[j][1].lower())
                    j = skip_expr(toks, j + 1)
        seen = {}
        stack = []                     # True = quoted data list
        # A file that is only data (first form starts with "((" or "'(") has no calls: e.g. manifests/*.result.lsp.
        if toks and (toks[0][0] == "'(" or (len(toks) > 1 and toks[0][0] == "(" and toks[1][0] in ("(", "'("))):
            print(f"# {path}: data file (no function calls), skipped")
            return
        # stack entries: "data" (quoted list), "code", "quote" ((quote ...) form: its children are data),
        # "cond" ((cond ...) form: each clause's first element is a test, not a call)
        prev = None
        for i, (k, v, line) in enumerate(toks):
            if k in ("(", "'("):
                parent = stack[-1] if stack else "code"
                head = toks[i + 1][1].lower() if i + 1 < len(toks) and toks[i + 1][0] == "sym" else None
                if k == "'(" or parent in ("data", "quote"):
                    state = "data"
                elif head == "quote":
                    state = "quote"
                elif head == "cond":
                    state = "cond"
                else:
                    state = "code"
                stack.append(state)
                clause = parent == "cond"
                # function position: the next token if it is a symbol
                if state != "data" and not clause and head is not None:
                    nxt = toks[i + 1][1]
                    after = toks[i + 2] if i + 2 < len(toks) else None
                    if after and after[0] == "sym" and after[1] == ".":
                        pass          # dotted pair
                    elif prev == "lambda-params" or nxt.lower() in defined or nxt.lower() in locals_ or \
                            re.match(r"^[-+]?\d", nxt) or nxt in (".", "/"):
                        pass
                    else:
                        seen.setdefault(("f", nxt), line)
                        if nxt.lower() in VLAX_MEMBER_FUNCS and i + 3 < len(toks):
                            # (vlax-get obj 'Prop): member is the 2nd argument
                            mem = arg_n(toks, i + 2, 1)
                            if mem:
                                seen.setdefault(("m", VLAX_MEMBER_FUNCS[nxt.lower()], mem), line)
            elif k == ")":
                if stack:
                    stack.pop()
            elif k == "q" and not (stack and stack[-1] in ("data", "quote")):
                if re.match(r"^(vla|vlax|vl)-", v, re.I):     # 'vla-item passed to vl-catch-all-apply / mapcar
                    seen.setdefault(("f", v), line)
            prev = None
        print(f"# {path}: {len(seen)} distinct names")
        for key, line in sorted(seen.items(), key=lambda kv: kv[1]):
            if key[0] == "f":
                self.check_func(key[1], f"  [line {line}]")
            else:
                self.check_any_member(key[2], key[1], f"  [line {line}]")


def skip_expr(toks, j):
    """Index just after the expression starting at j (atom or balanced list)."""
    if j >= len(toks):
        return j
    if toks[j][0] not in ("(", "'("):
        return j + 1 if toks[j][0] != ")" else j
    depth = 0
    while j < len(toks):
        k = toks[j][0]
        if k in ("(", "'("):
            depth += 1
        elif k == ")":
            depth -= 1
            if depth == 0:
                return j + 1
        j += 1
    return j


def arg_n(toks, start, n):
    """Return the n-th (0-based) argument after position start if it is 'Sym or "Str"; else None."""
    j, idx, depth = start, 0, 0
    while j < len(toks):
        k, v, _ = toks[j]
        if depth == 0 and idx == n:
            if k == "q":
                return v
            if k == "str" and re.match(r"^[A-Za-z]\w*$", v):
                return v
            return None
        if k in ("(", "'("):
            depth += 1
        elif k == ")":
            if depth == 0:
                return None
            depth -= 1
            if depth == 0:
                idx += 1
        elif depth == 0:
            idx += 1
        j += 1
    return None


def tokenize(src):
    """AutoLISP tokens: (, '(, ), sym, q ('sym), str. Comments (; and ;| |;) dropped."""
    out, i, line, n = [], 0, 1, len(src)
    while i < n:
        c = src[i]
        if c == "\n":
            line += 1
            i += 1
        elif c.isspace():
            i += 1
        elif c == ";":
            if src.startswith(";|", i):
                j = src.find("|;", i + 2)
                j = n if j < 0 else j + 2
            else:
                j = src.find("\n", i)
                j = n if j < 0 else j
            line += src.count("\n", i, j)
            i = j
        elif c == '"':
            j, buf = i + 1, []
            while j < n and src[j] != '"':
                if src[j] == "\\" and j + 1 < n:
                    buf.append(src[j:j + 2])
                    j += 2
                    continue
                buf.append(src[j])
                j += 1
            s = "".join(buf)
            out.append(("str", s, line))
            line += s.count("\n")
            i = j + 1
        elif c == "(":
            out.append(("(", "(", line))
            i += 1
        elif c == ")":
            out.append((")", ")", line))
            i += 1
        elif c == "'":
            j = i + 1
            while j < n and src[j].isspace():
                j += 1
            if j < n and src[j] == "(":
                out.append(("'(", "'(", line))
                i = j + 1
            else:
                m = re.match(r"[^\s()'\";]+", src[j:])
                out.append(("q", m.group(0) if m else "", line))
                i = j + (m.end() if m else 1)
        else:
            m = re.match(r"[^\s()'\";]+", src[i:])
            out.append(("sym", m.group(0), line))
            i += m.end()
    return out


def main(argv):
    if not argv or argv[0] in ("-h", "--help"):
        print(__doc__)
        return 2
    ck = Checker()
    if argv[0] == "--object" and len(argv) == 2:
        return ck.list_object(argv[1])
    if argv[0] == "--dxf" and len(argv) == 2:
        return ck.list_dxf(argv[1])
    if argv[0] == "--lsp":
        files, i = [], 1
        while i < len(argv):
            if argv[i] == "--with" and i + 1 < len(argv):
                ck.add_lib(argv[i + 1])
                i += 2
                continue
            files.append(argv[i])
            i += 1
        if not files:
            print(__doc__)
            return 2
        for p in files:
            ck.check_lsp(p)
    else:
        for t in argv:
            ck.check(t)
    print(f"api_checked: {ck.found} found, {ck.missing} not found")
    return 0 if ck.missing == 0 else 1


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
