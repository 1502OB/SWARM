"""Report paren balance of an AutoLISP file (strings and ; comments aware). Shared copy of northgate/scripts/lspcheck.py.

Usage: python ~/swarm/tools/lspcheck.py <file.lsp>   exit 1 if unbalanced (run before acad-writer loads it).
"""
import sys

s = open(sys.argv[1], encoding='utf-8').read()
d = 0; ins = False; i = 0; line = 1; top = []; extra = 0
while i < len(s):
    c = s[i]
    if c == '\n':
        line += 1
    if ins:
        if c == '\\':
            i += 2; continue
        if c == '"':
            ins = False
    elif c == ';':
        while i < len(s) and s[i] != '\n':
            i += 1
        continue
    elif c == '"':
        ins = True
    elif c == '(':
        if d == 0:
            top.append(line)
        d += 1
    elif c == ')':
        d -= 1
        if d < 0:
            print('extra ) at line', line); d = 0; extra += 1
    i += 1
print('final depth', d, 'in_string', ins, 'top-level forms start at', top)
sys.exit(1 if d or ins or extra else 0)
