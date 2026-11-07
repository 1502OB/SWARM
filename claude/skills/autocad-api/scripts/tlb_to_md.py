"""Turn tlb_dump.ps1 TSV output into this skill's ActiveX references.

    python tlb_to_md.py acax.tsv axdb.tsv        (first file wins on duplicate type names)

Writes ../references/activex.json (used by acad_api_check.py), activex-INDEX.md, activex-<range>.md and
activex-enums.md. Regenerate after an AutoCAD upgrade:
    pwsh -File tlb_dump.ps1 -Tlb "C:/Program Files/Common Files/Autodesk Shared/acax<NN>enu.tlb" -Out acax.tsv
    pwsh -File tlb_dump.ps1 -Tlb "C:/Program Files/Common Files/Autodesk Shared/axdb<NN>enu.tlb" -Out axdb.tsv
"""
import json
import sys
from pathlib import Path

REFS = Path(__file__).resolve().parent.parent / "references"
SKIP_IFACES = {"IUnknown", "IDispatch"}


def strip_ptr(t):
    """Drop the out-pointer star; interface pointers (IAcadText*) become the bare interface name."""
    t = t[:-1] if t.endswith("*") else t
    return t.rstrip("*") if t.startswith(("IAcad", "IAxDb", "IDispatch", "IUnknown")) else t


def load(paths):
    libs, types, order = [], {}, []
    for path in paths:
        cur = None
        for line in Path(path).read_text(encoding="utf-8").splitlines():
            f = line.split("\t")
            if f[0] == "LIB":
                libs.append(f"{f[1]} ({Path(path).name})")
            elif f[0] == "TYPE":
                kind, name, doc, bases = f[1], f[2], f[3], f[4]
                alias = f[5] if len(f) > 5 else ""
                if name in types:      # first library wins
                    cur = None
                    continue
                cur = types[name] = {"kind": kind, "name": name, "doc": doc, "lib": Path(path).stem,
                                     "bases": [b for b in bases.split(",") if b], "alias": alias,
                                     "funcs": [], "consts": []}
                order.append(name)
            elif f[0] == "FUNC" and cur is not None:
                _, _t, name, inv, ret, params, doc = f[:7]
                ps = []
                for p in (params.split("|") if params else []):
                    pn, pt, pf = (p.split(":", 2) + ["", ""])[:3]
                    ps.append({"name": pn, "type": pt, "flags": [x for x in pf.split(",") if x]})
                cur["funcs"].append({"name": name, "inv": inv, "ret": ret, "params": ps, "doc": doc})
            elif f[0] == "CONST" and cur is not None:
                cur["consts"].append({"name": f[2], "value": f[3], "doc": f[4] if len(f) > 4 else ""})
    return libs, types, order


def build(types):
    """Collapse FUNC records into methods and properties per interface."""
    coclass_of = {}
    for t in types.values():
        if t["kind"] == "COCLASS":
            for b in t["bases"]:
                if b.startswith("*") and not b.startswith("*source:"):
                    coclass_of.setdefault(b[1:], []).append(t["name"])
    out = {}
    for t in types.values():
        if t["kind"] not in ("INTERFACE", "DISPATCH") or t["name"] in SKIP_IFACES:
            continue
        methods, props = {}, {}
        for fn in t["funcs"]:
            if fn["inv"] == "METHOD":
                ret = "void"
                args = []
                for p in fn["params"]:
                    if "retval" in p["flags"]:
                        ret = strip_ptr(p["type"])
                    else:
                        args.append({"name": p["name"], "type": strip_ptr(p["type"]) if "out" in p["flags"] else p["type"],
                                     "out": "out" in p["flags"], "opt": "opt" in p["flags"] or any(
                                         x.startswith("default=") for x in p["flags"]),
                                     "default": next((x[8:] for x in p["flags"] if x.startswith("default=")), None)})
                methods[fn["name"]] = {"ret": ret, "args": args, "doc": fn["doc"]}
            else:
                pr = props.setdefault(fn["name"], {"type": "", "get": False, "put": False, "putref": False,
                                                   "index": [], "doc": fn["doc"]})
                vals = [p for p in fn["params"] if "retval" in p["flags"] or fn["inv"] != "GET"]
                idx = [p for p in fn["params"] if "retval" not in p["flags"]]
                if fn["inv"] == "GET":
                    pr["get"] = True
                    if vals:
                        pr["type"] = strip_ptr(vals[-1]["type"])
                    pr["index"] = [{"name": p["name"], "type": p["type"]} for p in idx]
                else:
                    pr["put" if fn["inv"] == "PUT" else "putref"] = True
                    if not pr["type"] and fn["params"]:
                        pr["type"] = fn["params"][-1]["type"]
                if not pr["doc"]:
                    pr["doc"] = fn["doc"]
        bases = [b for b in t["bases"] if b not in SKIP_IFACES and not b.startswith("source:")]
        events = t["name"].startswith("_D") or t["name"].endswith("Events")
        out[t["name"]] = {"doc": t["doc"], "lib": t["lib"], "inherits": bases, "events": events,
                          "coclass": coclass_of.get(t["name"], []), "methods": methods, "properties": props}
    enums = {t["name"]: {"doc": t["doc"], "values": [(c["name"], c["value"]) for c in t["consts"]]}
             for t in types.values() if t["kind"] == "ENUM"}
    aliases = {t["name"]: t["alias"] for t in types.values() if t["kind"] == "ALIAS"}
    return out, enums, aliases


def method_line(name, m):
    parts = []
    for a in m["args"]:
        s = f"{a['name']}<{a['type']}>"
        if a["out"]:
            s = f"'{a['name']}<out {a['type']}>"
        if a["opt"]:
            s = f"[{s}{'=' + a['default'] if a['default'] not in (None, '') else ''}]"
        parts.append(s)
    sig = f"(vla-{name} obj{(' ' + ' '.join(parts)) if parts else ''})"
    ret = "" if m["ret"] in ("void", "HRESULT") else f" -> {m['ret']}"
    doc = f"  ; {m['doc']}" if m["doc"] else ""
    return f"    M {name}  {sig}{ret}{doc}"


def prop_line(name, p):
    acc = "get" + ("/put" if p["put"] else "") + ("/putref" if p["putref"] else "") if p["get"] else \
        ("put" if p["put"] else "putref")
    if not p["put"] and not p["putref"]:
        acc += " READ-ONLY"
    calls = []
    if p["get"]:
        calls.append(f"vla-get-{name}")
    if p["put"] or p["putref"]:
        calls.append(f"vla-put-{name}")
    idx = ""
    if p["index"]:
        idx = "[" + ", ".join(f"{i['name']}<{i['type']}>" for i in p["index"]) + "]"
    doc = f"  ; {p['doc']}" if p["doc"] else ""
    return f"    P {name}{idx} : {p['type']}  [{acc}]  {' / '.join(calls)}{doc}"


def section(name, it):
    lines = [f"[{'EVENTS' if it['events'] else 'INTERFACE'}] {name}"]
    if it["events"]:
        lines.append("Note: event (source) interface: these are reactor callbacks, NOT vla- functions you can call.")
    if it["coclass"]:
        lines.append(f"Object: {', '.join(it['coclass'])}")
    if it["inherits"]:
        lines.append(f"Inherits: {', '.join(it['inherits'])}")
    if it["doc"]:
        lines.append(f"Doc: {it['doc']}")
    if it["methods"]:
        lines.append("  Methods")
        lines += [method_line(n, m) for n, m in sorted(it["methods"].items(), key=lambda kv: kv[0].lower())]
    if it["properties"]:
        lines.append("  Properties")
        lines += [prop_line(n, p) for n, p in sorted(it["properties"].items(), key=lambda kv: kv[0].lower())]
    lines.append("")
    return lines


def chunk(names, ifaces, target=900):
    groups, cur, size = [], [], 0
    for n in names:
        s = 6 + len(ifaces[n]["methods"]) + len(ifaces[n]["properties"])
        if cur and size + s > target:
            groups.append(cur)
            cur, size = [], 0
        cur.append(n)
        size += s
    if cur:
        groups.append(cur)
    return groups


def key(n):
    return n[5:] if n.startswith("IAcad") else (n[1:] if n.startswith("I") else n)


def main(paths):
    libs, types, _ = load(paths)
    ifaces, enums, aliases = build(types)
    REFS.mkdir(parents=True, exist_ok=True)
    for old in REFS.glob("activex-*.md"):
        old.unlink()
    names = sorted(ifaces, key=lambda n: key(n).lower())
    index = {}
    for g in chunk(names, ifaces):
        a, b = key(g[0])[:1].lower(), key(g[-1])[:1].lower()
        fname = f"activex-{a}.md" if a == b else f"activex-{a}-{b}.md"
        n = 2
        while (REFS / fname).exists():
            fname = fname.replace(".md", f"-{n}.md")
            n += 1
        lines = [f"# AutoCAD 2027 ActiveX: {key(g[0])} .. {key(g[-1])}", "",
                 "Generated from the type libraries by scripts/tlb_to_md.py. M = method, P = property.",
                 "vla- call: the object is always the first argument; 'name<out T> = pass a quoted symbol the call sets.",
                 "A member is listed only on the interface that declares it: walk `Inherits:` (IAcadLine -> IAcadEntity -> IAcadObject).",
                 ""]
        for nme in g:
            lines += section(nme, ifaces[nme])
            index[nme] = fname
        (REFS / fname).write_text("\n".join(lines), encoding="utf-8")
    en = ["# AutoCAD 2027 ActiveX enums", "",
          "From the type libraries. In AutoLISP use the constant name (defined after (vl-load-com)) or its value.", ""]
    for nme in sorted(enums, key=str.lower):
        e = enums[nme]
        en.append(f"[ENUM] {nme}" + (f"  ; {e['doc']}" if e["doc"] else ""))
        en += [f"    {c} = {v}" for c, v in e["values"]]
        en.append("")
    (REFS / "activex-enums.md").write_text("\n".join(en), encoding="utf-8")
    ix = ["# AutoCAD 2027 ActiveX index", "", f"Sources: {'; '.join(libs)}.",
          f"{len(ifaces)} interfaces, {len(enums)} enums. Interface -> file (object/coclass names in brackets).", "",
          "| Interface | Object | Inherits | File |", "|---|---|---|---|"]
    for nme in names:
        it = ifaces[nme]
        ix.append(f"| {nme} | {', '.join(it['coclass'])} | {', '.join(it['inherits'])} | {index[nme]} |")
    ix += ["", "Enums: activex-enums.md"]
    (REFS / "activex-INDEX.md").write_text("\n".join(ix) + "\n", encoding="utf-8")
    (REFS / "activex.json").write_text(json.dumps({"sources": libs, "interfaces": ifaces, "enums": enums,
                                                   "aliases": aliases}, indent=0), encoding="utf-8")
    print(f"{len(ifaces)} interfaces in {len(set(index.values()))} files, {len(enums)} enums")


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print(__doc__)
        sys.exit(2)
    main(sys.argv[1:])
