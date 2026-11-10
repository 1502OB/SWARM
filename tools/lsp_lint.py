"""Lint an AutoLISP script before it is loaded into AutoCAD (2026-11-06; extends tools/lspcheck.py).

    python ~/swarm/tools/lsp_lint.py <file.lsp> [<file.lsp> ...] [--json]

ERRORS (exit 1, the file must not be loaded):
  E1 unbalanced parens / unterminated string (reports the top-level form, e.g. the defun, where it starts)
  E2 Common Lisp / other dialect function that AutoLISP does not have (fboundp, identity, let, loop, format, ...)
  E3 the same function defined twice in the file, or a shared-library function (swl:*) redefined
  E4 XREF / PDFATTACH / IMAGE attach sent with (command ...): it cannot run inside the dispatcher; use
     (swl:queue-command "...") from swarm/library/acad/swarm_lib.lsp
WARNINGS (reported, exit 0):
  W1 setq of a variable that is neither a parameter nor declared local (after "/"): it leaks into the session and
     can clash with another script. Namespaced globals PREFIX:NAME (e.g. R7:FAIL) and *NAME* are intentional.
  W2 a string containing \\n outside princ/prompt/print/write-line/alert: if it reaches a bridge result it used to
     break the JSON (the bridge now escapes it, but keep results on one line: join with " | ")
  W3 a top-level form other than defun / defun-q / setq / princ / vl-load-com / load: it runs at load time
  W4 (command ...) in a function: allowed, but it runs in the dispatcher's context; prefer entmake / ActiveX
  W5 (A36) a c:* command whose last form is (princ) / (princ x) and does not return a string: execute_lisp then
     gets an EMPTY payload and the writer has to infer the result. End with (swl:result-out <task> <string>).
  W6 (A31) (setq v (or ...)) where v is later used as a value: AutoLISP OR returns T, not the first non-nil
     argument. Use (cond (a) (b)) to keep the value.
  W7 (A43, heuristic) an entget DATA list passed where an ENAME is expected: vlax-ename->vla-object / entget /
     entdel / entupd / entnext / ssadd / ssmemb (or a function of this file or swarm_lib whose parameter flows into
     one of them) called with a symbol whose last assignment in the defun was (entget ...) / (swl:ent ...), or an
     element of a list of those. Pass (cdr (assoc -1 ed)). entmod is not a sink: it takes the data list.
Exit 0 = no errors, 1 = errors, 2 = usage / unreadable file.
"""
import json
import os
import re
import sys
from pathlib import Path

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
from swarm_paths import SWARM  # noqa: E402

LIBRARY = SWARM / "library" / "acad" / "swarm_lib.lsp"
NOT_AUTOLISP = {
    "fboundp", "identity", "let", "let*", "loop", "format", "flet", "labels", "incf", "decf", "dolist", "dotimes",
    "when", "unless", "defvar", "defparameter", "defconstant", "funcall", "string-upcase", "string-downcase",
    "concatenate", "elt", "first", "second", "third", "rest", "push", "pop", "setf", "gethash", "make-hash-table",
    "lambda*", "return-from", "block", "case", "ecase", "typecase", "char", "string=", "equalp", "prog1", "prog2",
}
ATTACH_CMD = re.compile(r"^\s*_?\.?-?(XREF|XATTACH|PDFATTACH|IMAGE|IMAGEATTACH|DWFATTACH|DGNATTACH)\b", re.I)
PRINT_HEADS = {"princ", "prompt", "print", "prin1", "write-line", "alert", "terpri"}
TOP_OK = {"defun", "defun-q", "setq", "princ", "vl-load-com", "load", "prompt"}


class Node:
    __slots__ = ("items", "line", "end_line")

    def __init__(self, line):
        self.items, self.line, self.end_line = [], line, None


def parse(text):
    """Nested lists of atoms; atoms are (kind, value, line) with kind 'str' | 'sym'. Returns (top_forms, errors)."""
    root, stack, errors = Node(1), [], []
    stack.append(root)
    i, n, line = 0, len(text), 1
    while i < n:
        c = text[i]
        if c == "\n":
            line += 1
            i += 1
        elif c.isspace():
            i += 1
        elif c == ";":
            if text.startswith(";|", i):
                j = text.find("|;", i + 2)
                j = n if j < 0 else j + 2
                line += text.count("\n", i, j)
                i = j
            else:
                while i < n and text[i] != "\n":
                    i += 1
        elif c == "(":
            node = Node(line)
            stack[-1].items.append(node)
            stack.append(node)
            i += 1
        elif c == ")":
            if len(stack) == 1:
                errors.append(("E1", line, "extra ')' with no open form"))
            else:
                stack.pop().end_line = line
            i += 1
        elif c == '"':
            j, start = i + 1, line
            buf = []
            while j < n and text[j] != '"':
                if text[j] == "\\" and j + 1 < n:
                    buf.append(text[j:j + 2])
                    j += 2
                    continue
                if text[j] == "\n":
                    line += 1
                buf.append(text[j])
                j += 1
            if j >= n:
                errors.append(("E1", start, "unterminated string"))
                break
            stack[-1].items.append(("str", "".join(buf), start))
            i = j + 1
        elif c == "'":
            i += 1
        else:
            j = i
            while j < n and not text[j].isspace() and text[j] not in '();"\'':
                j += 1
            stack[-1].items.append(("sym", text[i:j], line))
            i = j
    for node in stack[1:]:
        head = node.items[1][1] if len(node.items) > 1 and node.items[0] == ("sym", "defun", node.line) else None
        errors.append(("E1", node.line, "unclosed '(' opened here" + (f" (in defun {head})" if head else "")))
    return root.items, errors


def head(node):
    return node.items[0][1].lower() if isinstance(node, Node) and node.items and isinstance(node.items[0], tuple) \
        and node.items[0][0] == "sym" else None


def walk(node, parent_head=None):
    """Yield (node_or_atom, enclosing call head) for every element under node."""
    if isinstance(node, Node):
        h = head(node)
        for it in node.items:
            yield it, h
            if isinstance(it, Node):
                yield from walk(it, h)


def library_names():
    if not LIBRARY.exists():
        return set()
    forms, _ = parse(LIBRARY.read_text(encoding="utf-8", errors="replace"))
    return {f.items[1][1].lower() for f in forms if head(f) in ("defun", "defun-q") and len(f.items) > 1}


# ---------------------------------------------------------------- W5 / W6 / W7 (2026-11-09, LESSONS A31 A36 A43)
STRING_HEADS = {"strcat", "swl:result", "swl:result-out", "swl:san", "swl:join", "substr", "itoa", "rtos", "vl-princ-to-string",
                "vl-prin1-to-string", "strcase", "vl-string-subst", "vl-string-trim", "vl-string-right-trim",
                "vl-string-left-trim", "vl-string-translate", "chr", "swl:f", "getvar"}
PRED_HEADS = {"=", "/=", "<", ">", "<=", ">=", "eq", "equal", "null", "not", "and", "or", "wcmatch", "member",
              "minusp", "zerop", "numberp", "boundp", "vl-catch-all-error-p", "vlax-erased-p", "listp", "atom",
              "vl-string-search", "assoc", "vl-position", "vlax-property-available-p", "vlax-method-applicable-p",
              "findfile", "tblsearch", "vl-some", "vl-every"}
BOOL_USE = {"if", "and", "or", "not", "null", "while"}
ENAME_SINKS = {"vlax-ename->vla-object": {0}, "entget": {0}, "entdel": {0}, "entupd": {0}, "entnext": {0},
               "ssadd": {0}, "ssmemb": {0}, "redraw": {0}}
DATA_HEADS = {"entget", "swl:ent"}
PICK_HEADS = {"car", "cadr", "caddr", "last", "nth"}
LIST_HEADS = {"cons", "list", "append"}


def sym(x):
    return x[1].lower() if isinstance(x, tuple) and x[0] == "sym" else None


def defun_parts(f):
    """(name, params [lower], body forms) of a (defun name (args / locals) body...) node."""
    name = f.items[1][1] if isinstance(f.items[1], tuple) else "?"
    params, args = [], f.items[2] if len(f.items) > 2 else None
    if isinstance(args, Node):
        for a in args.items:
            if sym(a) == "/":
                break
            if sym(a):
                params.append(sym(a))
    return name, params, f.items[3:]


def w5_check(f, warnings):
    name, _, body = defun_parts(f)
    if not name.lower().startswith("c:") or not body:
        return
    last = body[-1]
    while isinstance(last, Node) and head(last) == "progn" and len(last.items) > 1:
        last = last.items[-1]
    if not (isinstance(last, Node) and head(last) == "princ"):
        return
    if len(last.items) > 1:
        arg = last.items[1]
        if isinstance(arg, tuple) and arg[0] == "str":
            return
        if isinstance(arg, Node) and head(arg) in STRING_HEADS:
            return
        if sym(arg) and _set_from_string(f, sym(arg)):
            return
    form = "(princ)" if len(last.items) == 1 else "(princ ...)"
    warnings.append(("W5", last.line, f"{name} ends in {form}: the bridge gets no result; "
                                      "return the result string: end with (swl:result-out <task> ...)"))


def _set_from_string(f, v):
    for it, _ in walk(f):
        if isinstance(it, Node) and head(it) == "setq":
            for t, val in zip(it.items[1::2], it.items[2::2]):
                if sym(t) == v and ((isinstance(val, tuple) and val[0] == "str")
                                    or (isinstance(val, Node) and head(val) in STRING_HEADS)):
                    return True
    return False


def _walk_parent(node):
    """Yield (item, parent node, index in parent) for everything under node."""
    if isinstance(node, Node):
        for k, it in enumerate(node.items):
            yield it, node, k
            yield from _walk_parent(it)


def w6_check(f, warnings):
    name = defun_parts(f)[0]
    sets = []  # (var, target atom, value node)
    for it, _ in walk(f):
        if isinstance(it, Node) and head(it) == "setq":
            for t, val in zip(it.items[1::2], it.items[2::2]):
                if sym(t) and isinstance(val, Node) and head(val) == "or":
                    args = val.items[1:]
                    if args and all(isinstance(a, Node) and head(a) in PRED_HEADS for a in args):
                        continue  # (or (= a 1) (> b 2)) is a boolean on purpose
                    sets.append((sym(t), t, val))
    if not sets:
        return
    parents = list(_walk_parent(f))
    cond_clauses = {id(it) for it, p, k in parents if isinstance(it, Node) and head(p) == "cond"}
    for v, target, val in sets:
        value_use = False
        for it, p, k in parents:
            if it is target or sym(it) != v or p is f.items[2]:
                continue  # the (args / locals) list is a declaration, not a use
            h = head(p)
            if h == "setq":
                continue  # another assignment target, or copied into another variable: not decisive
            if h in BOOL_USE or (id(p) in cond_clauses and k == 0):
                continue
            value_use = True
            break
        if value_use:
            warnings.append(("W6", val.line, f"(setq {v} (or ...)) in {name}: or returns T, not the value; use cond"))


def ename_params(defs):
    """{function name: {argument index}} that must be an ENAME: the builtin sinks plus every given defun whose
    parameter is passed on to one of them (fixed point)."""
    out = {k: set(v) for k, v in ENAME_SINKS.items()}
    parts = [defun_parts(f) for f in defs]
    changed = True
    while changed:
        changed = False
        for nm, params, body in parts:
            got = out.setdefault(nm.lower(), set())
            for b in body:
                for it, _ in walk(_wrap([b])):
                    if isinstance(it, Node) and head(it) in out:
                        for k in list(out[head(it)]):
                            if k + 1 < len(it.items) and sym(it.items[k + 1]) in params:
                                idx = params.index(sym(it.items[k + 1]))
                                if idx not in got:
                                    got.add(idx)
                                    changed = True
    return {k: v for k, v in out.items() if v}


def w7_check(f, sinks, warnings):
    name, params, body = defun_parts(f)
    state = {}  # var -> "data" | "list" | None

    def kind(val):
        if isinstance(val, Node):
            h = head(val)
            if h in DATA_HEADS:
                return "data"
            if h in PICK_HEADS:
                src = val.items[2] if h == "nth" and len(val.items) > 2 else (val.items[1] if len(val.items) > 1 else None)
                return "data" if state.get(sym(src)) == "list" else None
            if h in LIST_HEADS and any(state.get(sym(a)) in ("data", "list") for a in val.items[1:]):
                return "list"
        return None

    def visit(n):
        if not isinstance(n, Node):
            return
        h = head(n)
        if h == "setq":
            for t, val in zip(n.items[1::2], n.items[2::2]):
                visit(val)
                if sym(t):
                    state[sym(t)] = kind(val)
            return
        if h == "foreach" and len(n.items) > 2 and sym(n.items[1]):
            visit(n.items[2])
            if state.get(sym(n.items[2])) == "list":
                state[sym(n.items[1])] = "data"
            for b in n.items[3:]:
                visit(b)
            return
        for it in n.items[1:]:
            visit(it)
        if h in sinks:
            for k in sinks[h]:
                if k + 1 < len(n.items):
                    a = n.items[k + 1]
                    if state.get(sym(a)) == "data" or (isinstance(a, Node) and head(a) in DATA_HEADS):
                        what = a[1] if isinstance(a, tuple) else f"({head(a)} ...)"
                        warnings.append(("W7", n.line, f"({h} {what} ...) in {name}: {what} holds entget DATA, "
                                                       "not an ename; pass (cdr (assoc -1 ...))"))

    for b in body:
        visit(b)


_LIB_DEFS = None


def library_defs():
    global _LIB_DEFS
    if _LIB_DEFS is None:
        _LIB_DEFS = []
        if LIBRARY.exists():
            forms, _ = parse(LIBRARY.read_text(encoding="utf-8", errors="replace"))
            _LIB_DEFS = [f for f in forms if head(f) in ("defun", "defun-q") and len(f.items) > 2]
    return _LIB_DEFS


def lint(path, lib=None):
    text = Path(path).read_text(encoding="utf-8", errors="replace")
    forms, errors = parse(text)
    warnings = []
    lib = library_names() if lib is None else lib
    is_lib = Path(path).resolve() == LIBRARY.resolve() if LIBRARY.exists() else False
    seen = {}
    file_defs = [f for f in forms if isinstance(f, Node) and head(f) in ("defun", "defun-q") and len(f.items) > 2]
    sinks = ename_params(([] if is_lib else library_defs()) + file_defs)
    for f in file_defs:
        w5_check(f, warnings)
        w6_check(f, warnings)
        w7_check(f, sinks, warnings)
    for f in forms:
        if not isinstance(f, Node):
            continue
        h = head(f)
        if h not in TOP_OK:
            warnings.append(("W3", f.line, f"top-level ({h or '?'} ...) runs at load time"))
        if h in ("defun", "defun-q") and len(f.items) > 2:
            name = f.items[1][1].lower() if isinstance(f.items[1], tuple) else "?"
            if name in seen:
                errors.append(("E3", f.line, f"{name} is defined twice (first at line {seen[name]})"))
            seen[name] = f.line
            if not is_lib and name in lib:
                errors.append(("E3", f.line, f"{name} redefines a shared-library function; call it, do not redefine it"))
            args = f.items[2]
            declared = set()
            if isinstance(args, Node):
                declared = {a[1].lower() for a in args.items if isinstance(a, tuple) and a[1] != "/"}
            for it, ph in walk(f):
                if isinstance(it, Node) and head(it) == "setq":
                    targets = it.items[1::2]
                    for t in targets:
                        if isinstance(t, tuple) and t[0] == "sym":
                            v = t[1].lower()
                            if v not in declared and ":" not in v and not (v.startswith("*") and v.endswith("*")):
                                warnings.append(("W1", t[2], f"setq of '{t[1]}' in {name}: not a parameter or "
                                                             "local; add it after '/' (or name it PREFIX:NAME)"))
    for it, ph in walk(Node(0) if not forms else _wrap(forms)):
        if isinstance(it, Node):
            h = head(it)
            if h in NOT_AUTOLISP:
                errors.append(("E2", it.line, f"({h} ...) does not exist in AutoLISP"))
            if h in ("command", "command-s", "vl-cmdf") and len(it.items) > 1:
                first = it.items[1]
                if isinstance(first, tuple) and first[0] == "str" and ATTACH_CMD.match(first[1]):
                    errors.append(("E4", it.line, f"({h} \"{first[1][:30]}\" ...) cannot run inside the dispatcher; "
                                                  "use (swl:queue-command \"...\")"))
                else:
                    warnings.append(("W4", it.line, f"({h} ...) runs in the dispatcher's context; prefer entmake/ActiveX"))
        elif it[0] == "str" and "\\n" in it[1] and ph not in PRINT_HEADS:
            warnings.append(("W2", it[2], "string with \\n outside princ/prompt: keep bridge results on one line"))
    # one W1 per variable per function is enough; other warnings are kept per line
    uniq, out_w = set(), []
    for w in warnings:
        key = (w[0], w[2]) if w[0] == "W1" else (w[0], w[1], w[2])
        if key not in uniq:
            uniq.add(key)
            out_w.append(w)
    return sorted(set(errors), key=lambda e: e[1]), sorted(out_w, key=lambda w: w[1])


def _wrap(forms):
    n = Node(0)
    n.items = forms
    return n


def main():
    args = [a for a in sys.argv[1:] if a != "--json"]
    as_json = "--json" in sys.argv
    if not args:
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    lib = library_names()
    report, bad = {}, False
    for p in args:
        try:
            errors, warnings = lint(p, lib)
        except OSError as e:
            print(f"ERROR cannot read {p}: {e}")
            sys.exit(2)
        report[p] = {"errors": [dict(code=c, line=l, msg=m) for c, l, m in errors],
                     "warnings": [dict(code=c, line=l, msg=m) for c, l, m in warnings]}
        bad |= bool(errors)
        if not as_json:
            print(f"{'FAIL' if errors else 'OK  '} {Path(p).name}: {len(errors)} error(s), {len(warnings)} warning(s)")
            for c, l, m in errors:
                print(f"  {c} line {l}: {m}")
            for c, l, m in warnings[:25]:
                print(f"  {c} line {l}: {m}")
            if len(warnings) > 25:
                print(f"  ... {len(warnings) - 25} more warnings")
    if as_json:
        print(json.dumps(report, indent=1))
    sys.exit(1 if bad else 0)


if __name__ == "__main__":
    main()
