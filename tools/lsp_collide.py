"""Find AutoLISP symbols that are both a function (defun) and a setq'd variable, ignoring case.
AutoLISP symbols are case-insensitive: (setq M4:VPINFO ...) deletes function m4:vpinfo (LESSONS A28).
Second check (LESSONS A42): inside one defun, parameters + locals (after "/") + setq targets that differ only in
case (`ne` and `nE` are ONE symbol, so the "two" variables overwrite each other).
Usage: python ~/swarm/tools/lsp_collide.py file.lsp [lib.lsp ...]
Output: "<file> collisions: [...] | none" then "<file> local-collisions: [defun: a/A, ...] | none".
2026-11-09: single-pass lexer (the first regex version stripped strings before comments, so an inch mark
inside a comment swallowed the rest of the file and the check silently reported "none")."""
import sys


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
