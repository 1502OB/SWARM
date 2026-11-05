"""Check Revit API members against this skill's references (dll2llm output for Revit 2025).

    python C:/Users/Jeremy/.claude/skills/revit-api/scripts/revit_api_check.py Type.Member [Type.Member ...]
    python C:/Users/Jeremy/.claude/skills/revit-api/scripts/revit_api_check.py --file <list.txt>   (one per line, "#" comments)
    python C:/Users/Jeremy/.claude/skills/revit-api/scripts/revit_api_check.py --type Viewport     (list a type's members)

Types may be short (View) or full (Autodesk.Revit.DB.View). Members are looked up on the type and then up its
"Inherits:" chain, because dll2llm documents a member only on the type that declares it (Viewport.Pinned is on
Element). Constructors: Type.Type. Prints FOUND (declaring type + signature) or NOT FOUND with close matches.
Exit 0 = all found, 1 = any missing, 2 = usage / references not found.
"""
import difflib
import re
import sys
from pathlib import Path

REFS = Path(__file__).resolve().parent.parent / "references"
HEAD = re.compile(r"^\[([A-Z ]+)\] (\S+)")  # [CLASS], [ABSTRACT CLASS] (static utils), [ENUM], ...
MEMBER = re.compile(r"^    (?:new |static |abstract |virtual |override |readonly |const |event )*"
                    r"(?:(?P<ret>[\w.<>\[\],? ]+?) )?(?P<name>~?[A-Za-z_]\w*)\s*(?P<rest>[({].*|=.*|;?)$")


def load():
    files = sorted(p for p in REFS.glob("*.md") if p.name not in ("INDEX.md", "GENERATED_SKILL.md", "core.md"))
    if not files:
        print(f"ERROR no reference files under {REFS}")
        sys.exit(2)
    types, short, inherits = {}, {}, {}   # types: full name -> {member: [sigs]}; short: name -> [full names]
    cur = name = None
    for f in files:
        for line in f.read_text(encoding="utf-8-sig").splitlines():
            m = HEAD.match(line)
            if m:
                name, cur = m.group(2), None
                continue
            if name is None:
                continue
            if line.startswith("Full Name: "):
                cur = line[11:].strip()
                types.setdefault(cur, {})
                short.setdefault(name, []).append(cur)
            elif cur and line.startswith("Inherits: "):
                inherits[cur] = line[10:].strip().split("<")[0]
            elif cur and line.startswith("    ") and not line.startswith("      "):
                mm = MEMBER.match(line)
                if mm:
                    mname = mm.group("name")
                    if line.strip().startswith("new ") or (mm.group("ret") is None and mname == name.split("<")[0]):
                        mname = ".ctor"
                    types[cur].setdefault(mname, []).append(f"{line.strip()}  [{f.name}]")
    return types, short, inherits


def resolve(types, short, t):
    """All full type names matching t (full or short); Autodesk.Revit.DB.* first."""
    if t in types:
        return [t]
    c = short.get(t.split(".")[-1], [])
    if "." in t:
        c = [x for x in c if x.endswith(t)] or c
    return sorted(c, key=lambda x: (not x.startswith("Autodesk.Revit.DB."), x))


def chain(types, short, inherits, t):
    out = []
    while t and t not in out:
        out.append(t)
        b = inherits.get(t)
        if b and b not in types:
            r = resolve(types, short, b)
            b = r[0] if r else None
        t = b
    return out


def main():
    args = sys.argv[1:]
    if not args:
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    types, short, inherits = load()
    if args[0] == "--type":
        cands = resolve(types, short, args[1])
        if not cands:
            print(f"NOT FOUND type {args[1]}; close: {difflib.get_close_matches(args[1].split('.')[-1], list(short), 5)}")
            sys.exit(1)
        for t in cands:
            print(f"{t}  (inherits: {' > '.join(chain(types, short, inherits, t)[1:]) or '-'})")
            for mname, sigs in sorted(types[t].items()):
                for sig in sigs:
                    print("  " + sig)
        sys.exit(0)
    if args[0] == "--file":
        items = [ln.split("#")[0].strip() for ln in Path(args[1]).read_text(encoding="utf-8").splitlines()]
        items = [i for i in items if i]
    else:
        items = args
    missing = 0
    for item in items:
        if "." not in item:
            print(f"SKIP {item}: use Type.Member")
            continue
        tname, member = item.rsplit(".", 1)
        if member == tname.split(".")[-1]:
            member = ".ctor"
        cands = resolve(types, short, tname)
        if not cands:
            missing += 1
            print(f"NOT FOUND {item}: no type '{tname}'; close types: "
                  f"{difflib.get_close_matches(tname.split('.')[-1], list(short), 4)}")
            continue
        hit = None
        for t in cands:
            decl = next((x for x in chain(types, short, inherits, t) if member in types.get(x, {})), None)
            if decl:
                hit = (t, decl)
                break
        if hit:
            t, decl = hit
            sigs = types[decl][member]
            via = "" if decl == t else f" (inherited from {decl})"
            amb = f" [type {t}; {len(cands)} types named {tname.split('.')[-1]}]" if len(cands) > 1 else ""
            more = f"  (+{len(sigs) - 1} overload(s))" if len(sigs) > 1 else ""
            print(f"FOUND {item}{via}{amb}: {sigs[0]}{more}")
        else:
            missing += 1
            pool = [m for t in cands for x in chain(types, short, inherits, t) for m in types.get(x, {})]
            print(f"NOT FOUND {item}: '{member}' is not on {', '.join(cands)} or base types; close: "
                  f"{difflib.get_close_matches(member, pool, 5)}")
    sys.exit(1 if missing else 0)


if __name__ == "__main__":
    main()
