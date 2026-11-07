"""Shared helpers for the swarm PreToolUse guards (2026-11-06).

The guards match deny patterns against code. Matching raw text blocked legitimate content: a C# text note
"CLOSE TO WALL", a comment saying "Audit", a LISP label "EXIT". These helpers remove the parts of the code that are
data or comments before a deny pattern is applied, so only real calls are judged.
"""
import re

# C#: block comments, line comments, verbatim strings @"..." ("" escapes), interpolated/regular strings, char literals.
_CS_TOKEN = re.compile(
    r'/\*.*?\*/'                      # block comment
    r'|//[^\n]*'                      # line comment
    r'|@\$?"(?:[^"]|"")*"'            # verbatim (and $@) string
    r'|\$?"(?:\\.|[^"\\\n])*"'        # regular / interpolated string
    r"|'(?:\\.|[^'\\\n])'",           # char literal
    re.DOTALL,
)


def strip_cs(code: str) -> str:
    """C# code with comments removed and string/char literals replaced by empty literals."""
    def repl(m):
        t = m.group(0)
        if t.startswith("/"):
            return " "
        return "''" if t.startswith("'") else '""'
    return _CS_TOKEN.sub(repl, code)


def strip_lisp_comments(code: str) -> str:
    """AutoLISP code with ; comments removed (strings kept: AutoCAD command names live in strings)."""
    out, i, n, ins = [], 0, len(code), False
    while i < n:
        c = code[i]
        if ins:
            out.append(c)
            if c == "\\" and i + 1 < n:
                out.append(code[i + 1])
                i += 2
                continue
            if c == '"':
                ins = False
        elif c == '"':
            ins = True
            out.append(c)
        elif c == ";":
            while i < n and code[i] != "\n":
                i += 1
            continue
        else:
            out.append(c)
        i += 1
    return "".join(out)


# AutoCAD command strings that run a command: (command "...") / (command-s "...") / (vl-cmdf "...")
CMD_CALL = re.compile(r'\(\s*(?:command|command-s|vl-cmdf)\s+"((?:\\.|[^"\\])*)"', re.IGNORECASE)
SEND_CALL = re.compile(r'\(\s*vla-(?:SendCommand|PostCommand)\b', re.IGNORECASE)


def _skip_form(s: str, i: int) -> int:
    """Index just past the form/atom starting at s[i] (balanced parens, strings respected)."""
    n = len(s)
    while i < n and s[i].isspace():
        i += 1
    if i < n and s[i] == "(":
        depth, ins = 0, False
        while i < n:
            c = s[i]
            if ins:
                if c == "\\":
                    i += 1
                elif c == '"':
                    ins = False
            elif c == '"':
                ins = True
            elif c == "(":
                depth += 1
            elif c == ")":
                depth -= 1
                if depth == 0:
                    return i + 1
            i += 1
        return n
    while i < n and not s[i].isspace() and s[i] not in '()"':
        i += 1
    return i


def lisp_command_strings(code: str):
    """Every literal command string the LISP sends to AutoCAD's command line: the first string argument of
    (command ...) / (command-s ...) / (vl-cmdf ...), and the text argument of vla-SendCommand / vla-PostCommand
    (after the document argument). A SendCommand whose text is not a literal string is reported as '<dynamic>'."""
    s = strip_lisp_comments(code)
    out = [m.group(1) for m in CMD_CALL.finditer(s)]
    for m in SEND_CALL.finditer(s):
        j = _skip_form(s, m.end())          # the document argument
        while j < len(s) and s[j].isspace():
            j += 1
        if j < len(s) and s[j] == '"':
            k, buf = j + 1, []
            while k < len(s) and s[k] != '"':
                if s[k] == "\\" and k + 1 < len(s):
                    buf.append(s[k:k + 2])
                    k += 2
                    continue
                buf.append(s[k])
                k += 1
            out.append("".join(buf))
        else:
            out.append("<dynamic>")
    return out
