"""Dump a .docx (letter) to plain text so agents can Read it (the Read tool cannot open .docx).

Usage: python C:/Users/Jeremy/swarm/tools/docx_text.py <in.docx> <out.txt>
Paragraphs in order (numbered "[p<N>]"), then tables as " | "-joined rows. Highlighted runs are wrapped in {{HL: }}.
"""
import sys

import docx


def main():
    d = docx.Document(sys.argv[1])
    out = []
    for i, p in enumerate(d.paragraphs):
        if not p.text.strip():
            continue
        parts = []
        for r in p.runs:
            t = r.text
            if t and r.font.highlight_color is not None:
                t = "{{HL: " + t + "}}"
            parts.append(t)
        out.append(f"[p{i}] " + "".join(parts))
    for ti, t in enumerate(d.tables):
        out.append(f"[table {ti}]")
        for row in t.rows:
            out.append(" | ".join(c.text for c in row.cells))
    open(sys.argv[2], "w", encoding="utf-8").write("\n".join(out) + "\n")
    print(f"docx_text: {len(out)} lines -> {sys.argv[2]}")


if __name__ == "__main__":
    main()
