"""Find AutoLISP symbols that are both a function (defun) and a setq'd variable, ignoring case.
AutoLISP symbols are case-insensitive: (setq M4:VPINFO ...) deletes function m4:vpinfo (LESSONS A28).
Second check (LESSONS A42): inside one defun, parameters + locals (after "/") + setq targets that differ only in
case (`ne` and `nE` are ONE symbol, so the "two" variables overwrite each other).
Usage: python ~/swarm/tools/lsp_collide.py file.lsp [lib.lsp ...]
Third check (LESSONS A51, 2026-11-20): a parameter, local, setq target or foreach variable whose name is an AutoLISP
built-in in any case (`mAx` IS the SUBR MAX: its value shadows MAX for every callee -> "bad function: 1014.72").
Built-in names come from the autocad-api skill (references/autolisp.json), with a short fallback list.
Output: "<file> collisions: [...] | none", "<file> local-collisions: [defun: a/A, ...] | none", then
"<file> subr-shadows: [defun: mAx(MAX), ...] | none" (top-level setq targets are reported under "<top>").
2026-11-09: single-pass lexer (the first regex version stripped strings before comments, so an inch mark
inside a comment swallowed the rest of the file and the check silently reported "none")."""
import json
import os
import re
import sys

FALLBACK_SUBRS = ("abs angle append apply assoc atan atof atoi car cdr chr close cons cos distance entget entlast "
                  "entnext eq equal eval exp expt fix float getvar handent inters itoa last length list load log "
                  "max member min nth open polar print read rem reverse rtos set sin sqrt strcat strlen subst "
                  "substr trans type ver").split()


def subr_names():
    """Lower-case AutoLISP built-in function names (autocad-api skill), or the fallback list."""
    here = os.path.dirname(os.path.abspath(__file__))
    cands = [os.path.join(os.environ.get("CLAUDE_CONFIG_DIR", os.path.join(os.path.expanduser("~"), ".claude")),
                          "skills", "autocad-api", "references", "autolisp.json"),
             os.path.join(os.path.dirname(here), "claude", "skills", "autocad-api", "references", "autolisp.json")]
    for c in cands:
        try:
            names = json.load(open(c, encoding="utf-8"))["names"]
            return {k.lower() for k in names if re.fullmatch(r"[a-z][a-z0-9_\-]*", k.lower())}
        except (OSError, ValueError, KeyError):
            continue
    return set(FALLBACK_SUBRS)


def tokens(s):
    out, i, n = [], 0, len(s)
    while i < n:
        c = s[i]
        if c == ';':
            if s.startswith(';|', i):
                j = s.find('|;', i + 2)
                i = n if j < 0 else j + 2
            else:
                j = s.find('\n', i)
                i = n if j < 0 else j + 1
        elif c == '"':
            i += 1
            while i < n and s[i] != '"':
                i += 2 if s[i] == '\\' else 1
            i += 1
            out.append('""')
        elif c in '()':
            out.append(c)
            i += 1
        elif c.isspace() or c in "'`":
            i += 1
        else:
            j = i
            while j < n and not s[j].isspace() and s[j] not in '()";':
                j += 1
            out.append(s[i:j])
            i = j
    return out


def scan(path):
    t = tokens(open(path, encoding='utf-8', errors='ignore').read())
    defs, sets = {}, {}
    i = 0
    while i < len(t):
        if t[i] == '(' and i + 1 < len(t):
            head = t[i + 1].lower()
            if head == 'defun' and i + 2 < len(t):
                defs[t[i + 2].lower()] = path
            elif head == 'setq':
                j, depth, pos = i + 2, 0, 0
                while j < len(t):
                    if t[j] == '(':
                        depth += 1
                    elif t[j] == ')':
                        if depth == 0:
                            break
                        depth -= 1
                        if depth == 0:
                            pos += 1
                    elif depth == 0:
                        if pos % 2 == 0:
                            sets[t[j].lower()] = path
                        pos += 1
                    j += 1
        i += 1
    return defs, sets


def setq_targets(t, i):
    """Targets of the (setq ...) whose '(' is at t[i] (multi-pair aware). Returns (names, index after the form)."""
    names, j, depth, pos = [], i + 2, 0, 0
    while j < len(t):
        if t[j] == '(':
            depth += 1
        elif t[j] == ')':
            if depth == 0:
                break
            depth -= 1
            if depth == 0:
                pos += 1
        elif depth == 0:
            if pos % 2 == 0:
                names.append(t[j])
            pos += 1
        j += 1
    return names, j


def form_end(t, i):
    """Index of the ')' closing the '(' at t[i]."""
    depth, j = 0, i
    while j < len(t):
        if t[j] == '(':
            depth += 1
        elif t[j] == ')':
            depth -= 1
            if depth == 0:
                return j
        j += 1
    return len(t) - 1


def local_collisions(path):
    """[(defun name, [spellings]), ...] for names in one defun that differ only in case."""
    t = tokens(open(path, encoding='utf-8', errors='ignore').read())
    out, i = [], 0
    while i < len(t):
        if t[i] == '(' and i + 3 < len(t) and t[i + 1].lower() in ('defun', 'defun-q') and t[i + 3] == '(':
            name, end = t[i + 2], form_end(t, i)
            k, names = i + 4, []
            while k < len(t) and t[k] != ')':
                if t[k] != '/':
                    names.append(t[k])
                k += 1
            j = k + 1
            while j < end:
                if t[j] == '(' and j + 1 < end and t[j + 1].lower() == 'setq':
                    names += setq_targets(t, j)[0]
                j += 1
            groups = {}
            for nm in names:
                groups.setdefault(nm.lower(), [])
                if nm not in groups[nm.lower()]:
                    groups[nm.lower()].append(nm)
            dup = [g for g in groups.values() if len(g) > 1]
            if dup:
                out.append((name, dup))
            i = end + 1  # nested defuns are counted with their enclosing defun
            continue
        i += 1
    return out


def subr_shadows(path, subrs=None):
    """[(defun name or '<top>', name, BUILTIN), ...] for bound names that are AutoLISP built-ins in any case."""
    subrs = subr_names() if subrs is None else subrs
    t = tokens(open(path, encoding='utf-8', errors='ignore').read())
    out, seen, owner_end, owner = [], set(), [], []
    i = 0
    while i < len(t):
        while owner_end and i > owner_end[-1]:
            owner_end.pop(); owner.pop()
        cur = owner[-1] if owner else '<top>'
        bound = []
        if t[i] == '(' and i + 3 < len(t) and t[i + 1].lower() in ('defun', 'defun-q') and t[i + 3] == '(':
            cur = t[i + 2]
            owner.append(cur); owner_end.append(form_end(t, i))
            k = i + 4
            while k < len(t) and t[k] != ')':
                if t[k] != '/':
                    bound.append(t[k])
                k += 1
        elif t[i] == '(' and i + 2 < len(t) and t[i + 1].lower() == 'setq':
            bound = setq_targets(t, i)[0]
        elif t[i] == '(' and i + 2 < len(t) and t[i + 1].lower() in ('foreach', 'vlax-for') and t[i + 2] not in '()':
            bound = [t[i + 2]]
        for nm in bound:
            if nm.lower() in subrs and (cur, nm.lower()) not in seen:
                seen.add((cur, nm.lower()))
                out.append((cur, nm, nm.upper()))
        i += 1
    return out


if __name__ == '__main__':
    main = sys.argv[1]
    d, s = scan(main)
    for lib in sys.argv[2:]:
        ld, _ = scan(lib)
        d.update({k: v for k, v in ld.items() if k not in d})
    hits = sorted(set(d) & set(s))
    print(main, 'collisions:', hits if hits else 'none')
    loc = local_collisions(main)
    print(main, 'local-collisions:',
          '[' + ', '.join(f"{n}: {'/'.join(g)}" for n, gs in loc for g in gs) + ']' if loc else 'none')
    sh = subr_shadows(main)
    print(main, 'subr-shadows:', '[' + ', '.join(f"{o}: {n}({b})" for o, n, b in sh) + ']' if sh else 'none')
