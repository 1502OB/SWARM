"""Lint an AutoLISP script before it is loaded into AutoCAD (2026-11-06; extends tools/lspcheck.py).

    python C:/Users/Jeremy/swarm/tools/lsp_lint.py <file.lsp> [<file.lsp> ...] [--json]

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
Exit 0 = no errors, 1 = errors, 2 = usage / unreadable file.
"""
import json
import re
import sys
from pathlib import Path

LIBRARY = Path("C:/Users/Jeremy/swarm/library/acad/swarm_lib.lsp")
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


def lint(path, lib=None):
    text = Path(path).read_text(encoding="utf-8", errors="replace")
    forms, errors = parse(text)
    warnings = []
    lib = library_names() if lib is None else lib
    is_lib = Path(path).resolve() == LIBRARY.resolve() if LIBRARY.exists() else False
    seen = {}
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
