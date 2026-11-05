"""Check a response letter for bracketed text (python-docx).

Usage: python docx_check.py <letter.docx>
Prints JSON: pending (count of "[PENDING - JEREMY TO REVIEW ...]" markers) and other_brackets (any other
[bracketed] text found in body, tables, headers and footers). Exit 1 if other_brackets is not empty.
"""
import json
import re
import sys

import docx

BRACKET = re.compile(r"\[[^\[\]]*\]")
PENDING = "[PENDING - JEREMY TO REVIEW]"


def texts(doc):
    for p in doc.paragraphs:
        yield p.text
    for t in doc.tables:
        for row in t.rows:
            for cell in row.cells:
                yield cell.text
    for s in doc.sections:
        for part in (s.header, s.footer):
            for p in part.paragraphs:
                yield p.text


def main():
    doc = docx.Document(sys.argv[1])
    pending, other = 0, []
    for text in texts(doc):
        for m in BRACKET.findall(text):
            if m == PENDING:
                pending += 1
            else:
                other.append(m)
    print(json.dumps({"file": sys.argv[1], "pending": pending, "other_brackets": other}, indent=1))
    sys.exit(1 if other else 0)


if __name__ == "__main__":
    main()
