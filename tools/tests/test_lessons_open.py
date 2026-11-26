"""tools/lessons_open.py: only unfolded LESSONS rows and not-yet-rolled-in run-note bullets are shown."""
import _util  # noqa: F401  (sys.path)
import lessons_open

SAMPLE = """# Swarm lessons learned

## O. Orchestrator and session setup

| id | lesson | source | status |
|---|---|---|---|
| O1 | Set the model on every subagent. | mem 1 | → agents (frontmatter) |
| O2 | Copy deliverables yourself. | mem 2 | open |
| O3 | Old style arrow. | x | -> scripter |

## 2026-11-20 run 20261120-ATZ (Aritzia 26030)
- R101: rolled in already.
- ZZ999: a run note nobody rolled in yet.
"""


def _classify(text, corpus):
    shown = []
    for section, lesson_id, status, line in lessons_open.rows(text):
        folded = lessons_open.FOLDED.match(status) or (
            status == "bullet" and lessons_open.re.search(rf"\b{lesson_id}\b", corpus))
        if not folded:
            shown.append(lesson_id)
    return shown


def test_rows_and_bullets_filtered():
    assert _classify(SAMPLE, corpus="see R101 in scripting-lessons") == ["O2", "ZZ999"]


def test_bullets_only_in_dated_sections():
    text = "## R. Revit\n- R5: a bullet outside a dated section is not a lesson row\n"
    assert list(lessons_open.rows(text)) == []


def test_real_file_is_small():
    corpus = lessons_open.rolled_in_text()
    shown = _classify(lessons_open.LESSONS.read_text(encoding="utf-8"), corpus)
    assert 0 < len(shown) < 60, len(shown)
