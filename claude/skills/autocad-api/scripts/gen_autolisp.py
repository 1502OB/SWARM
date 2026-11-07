"""Build references/autolisp-functions.md + autolisp.json from Autodesk's AutoLISP extension help data.

    python -I gen_autolisp.py <path/to/webHelpAbstraction.json>

Source: github.com/Autodesk-AutoCAD/AutoLispExt extension/src/help/webHelpAbstraction.json (Autodesk's own
signature data for the AutoLISP Reference; its "year" field says which help release it was cut from).
vla-* wrappers are NOT written here: they are generated from the ActiveX type library at run time, so the 2027
type library (activex.json) is the authority for them. Their entries from this file are kept in autolisp.json
under "vla_doc" only, for the friendly object names ("validObjects").
"""
import json
import sys
from pathlib import Path

REFS = Path(__file__).resolve().parent.parent / "references"
HELP = "https://help.autodesk.com/view/OARX/2027/ENU/?guid=GUID-{}"


def entry(f):
    return {"signature": f.get("signature", ""), "returns": (f.get("returnType") or {}).get("typeNames", ""),
            "description": (f.get("description") or "").strip(), "guid": f.get("guid", ""),
            "platforms": f.get("platforms", ""),
            "args": [{"name": a.get("id"), "type": a.get("typeNames"), "enums": a.get("enums") or []}
                     for a in f.get("arguments") or []]}


def main(src):
    d = json.loads(Path(src).read_text(encoding="utf-8"))
    year = d.get("year", "?")
    funcs, vla_doc = {}, {}
    for k, f in d["functions"].items():
        name = f.get("id", k)
        (vla_doc if name.lower().startswith("vla-") else funcs)[name.lower()] = [entry(f)]
    for k, lst in d.get("ambiguousFunctions", {}).items():
        es = [entry(f) for f in lst]
        name = lst[0].get("id", k).lower()
        (vla_doc if name.startswith("vla-") else funcs)[name] = es
    # display names keep Autodesk's casing
    disp = {}
    for k, f in list(d["functions"].items()) + [(k, l[0]) for k, l in d.get("ambiguousFunctions", {}).items()]:
        disp[f.get("id", k).lower()] = f.get("id", k)

    def group(n):
        for p in ("vlax-", "vlr-", "vl-", "acet-", "layerstate-", "dictionary"):
            if n.startswith(p):
                return p.rstrip("-") if p.endswith("-") else "core"
        return "core"

    order = ["core", "vl", "vlax", "vlr", "layerstate", "acet"]
    titles = {"core": "Core AutoLISP", "vl": "vl- (Visual LISP)", "vlax": "vlax- (ActiveX helpers)",
              "vlr": "vlr- (reactors)", "layerstate": "layerstate-", "acet": "acet- (Express Tools: only if loaded)"}
    lines = [f"# AutoLISP functions (excluding vla-*)", "",
             f"From Autodesk's AutoLISP extension help data (help release {year}); AutoCAD 2027 defines 3058",
             "function symbols after (vl-load-com) (atoms-family count, 2026-11-06). vla-* wrappers: see activex-*.md.",
             "Format: `(name arg<type> [opt]...)  -> returns ; description`  help: GUID link. `?` after an argument",
             "= optional, `...` = repeats. Several lines for one name = documented overloads.", ""]
    for g in order:
        names = sorted(n for n in funcs if group(n) == g)
        if not names:
            continue
        lines += [f"## {titles[g]} ({len(names)})", ""]
        for n in names:
            for e in funcs[n]:
                plat = "" if "WIN" in e["platforms"] or not e["platforms"] else f" [{e['platforms']}]"
                enums = "; ".join(f"{a['name']}: {', '.join(a['enums'])}" for a in e["args"] if a["enums"])
                lines.append(f"- `{e['signature'] or '(' + disp.get(n, n) + ')'}` -> {e['returns'] or '?'} ; "
                             f"{e['description']}{plat}" + (f" (values: {enums})" if enums else "")
                             + (f" [help]({HELP.format(e['guid'])})" if e["guid"] else ""))
        lines.append("")
    (REFS / "autolisp-functions.md").write_text("\n".join(lines), encoding="utf-8")
    (REFS / "autolisp.json").write_text(json.dumps({"year": year, "functions": funcs, "names": disp,
                                                    "vla_doc": vla_doc}, indent=0), encoding="utf-8")
    print(f"{len(funcs)} functions (+{len(vla_doc)} vla- doc entries), help year {year}")


if __name__ == "__main__":
    if len(sys.argv) != 2:
        print(__doc__)
        sys.exit(2)
    main(sys.argv[1])
