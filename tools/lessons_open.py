"""Print only the LESSONS.md rows a session still has to read.

    python tools/lessons_open.py [--all-status] [--section O|R|A|...] [--grep <text>]

A row whose status column says "-> <agent>" / "→ agent|prompt" is already built into an agent definition or a swarm
prompt, so reading it again only costs context (2026-11-25 swarm review: 249 of 271 rows were folded, ~33k tokens
per session). This prints the rest (status open / reference / done / fixed / anything else) with its section, plus
the file's header notes. Run-note bullets ("- R107: ...") in the dated roll-in sections are shown only when their id
appears in no skill, generated agent source or swarm prompt (i.e. not rolled in yet). --grep searches every row and
bullet (folded ones too) when a specific lesson id or topic is needed.
"""
import argparse
import re
import sys
from pathlib import Path

LESSONS = Path(__file__).resolve().parent.parent / "LESSONS.md"
ROW = re.compile(r"^\|\s*([A-Z]{1,2}\d+[a-z]?)\s*\|")
BULLET = re.compile(r"^- \**([A-Z]{1,2}\d+[a-z]?)\b")
ROOT = LESSONS.parent
ROLLED_IN = ["claude/skills/*/SKILL.md", "claude/skills/*/references/*.md", "build/gen_agents*.py",
             "MARKUP_SWARM_PROMPT.md", "QAQC_SWARM_PROMPT.md"]
FOLDED = re.compile(r"^\s*(→|->)")


def rows(text):
    section = ""
    for line in text.splitlines():
        if line.startswith("## "):
            section = line[3:].strip()
            continue
        match = ROW.match(line)
        if match:
            cells = [c.strip() for c in line.strip().strip("|").split("|")]
            yield section, match.group(1), cells[-1] if cells else "", line
            continue
        match = BULLET.match(line)
        if match and section[:2] == "20":   # dated roll-in / run-note sections
            yield section, match.group(1), "bullet", line


def rolled_in_text():
    return "\n".join(f.read_text(encoding="utf-8", errors="replace")
                     for pattern in ROLLED_IN for f in sorted(ROOT.glob(pattern)))


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("--all-status", action="store_true", help="include folded rows")
    parser.add_argument("--section", help="only ids starting with this letter (O, R, A, Q, ...)")
    parser.add_argument("--grep", help="case-insensitive text to match anywhere in the row (implies --all-status)")
    args = parser.parse_args()
    text = LESSONS.read_text(encoding="utf-8")
    corpus = rolled_in_text()
    shown = folded = 0
    current = None
    out = []
    for section, lesson_id, status, line in rows(text):
        if args.section and not lesson_id.startswith(args.section.upper()):
            continue
        if args.grep and args.grep.casefold() not in line.casefold():
            continue
        is_folded = FOLDED.match(status) or (status == "bullet" and re.search(rf"\b{lesson_id}\b", corpus))
        if is_folded and not (args.all_status or args.grep):
            folded += 1
            continue
        if section != current:
            out.append(f"\n## {section}")
            current = section
        out.append(line)
        shown += 1
    sys.stdout.reconfigure(encoding="utf-8")
    print(f"LESSONS.md: {shown} rows shown, {folded} folded into agents/prompts not shown "
          f"(use --grep <id|text> to look one up).")
    print("\n".join(out))


if __name__ == "__main__":
    main()
