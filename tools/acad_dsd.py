"""Write a DSD (sheet list) so acad-writer can -PUBLISH layouts to ONE PDF without dialogs.

    python ~/swarm/tools/acad_dsd.py "<drawing.dwg>" "<layout>" "<out.pdf>" "<out.dsd>"
    python ~/swarm/tools/acad_dsd.py --sheets "<dwg>|<layout>;<dwg>|<layout>;..." "<out.pdf>" "<out.dsd>"
    python ~/swarm/tools/acad_dsd.py --sheets-file <sheets.txt> "<out.pdf>" "<out.dsd>"

One sheet: the DSD that worked for 22-001 PC9 (LESSONS A6/A23): page setup of the layout, PDF target, 1200 dpi vector.
Multi-sheet (LESSONS A41, publish-set tasks): one [DWF6Sheet:...] block per "dwg|layout" pair, in the given order,
all into ONE multi-sheet PDF - the layout of 2018-930Mansfield/output/M18-ISS-T1.dsd, which published 20 sheets.
--sheets-file holds one "dwg|layout" per line (blank lines and lines starting with # are ignored); use it when a
path contains "&" or other characters the command line or the Bash guard would mangle.
Refuses if the PDF or DSD already exists (never overwrites), if a drawing is missing, or if a pair is repeated.
Paths are kept as given (os.path.abspath, never resolve(): resolve() turns L:\\ into \\\\EgnyteDrive\\..., which
AutoCAD treats as another file). Prints the DSD path.
The PDF should go to a local folder (swarm/visual/<run-id>/ or the project output folder), never straight to L:
(shared-drive permission block).
"""
import os
import sys
from pathlib import Path

HEADER = """[DWF6Version]
Ver=1
[DWF6MinorVersion]
MinorVer=1
"""

SHEET = """[DWF6Sheet:{sheet}]
DWG={dwg}
Layout={layout}
Setup=
OriginalSheetPath={dwg}
Has Plot Port=0
Has3DDWF=0
"""

FOOTER = """[Target]
Type=6
DWF={pdf}
OUT={outdir}\\
PWD=
[PdfOptions]
IncludeHyperlinks=TRUE
CreateBookmarks=TRUE
CaptureFontsInDrawing=TRUE
ConvertTextToGeometry=FALSE
VectorResolution=1200
RasterResolution=400
[AutoCAD Block Data]
IncludeBlockInfo=0
BlockTmplFilePath=
[SheetSet Properties]
IsSheetSet=FALSE
IsHomogeneous=FALSE
SheetSet Name=
NoOfCopies=1
PlotStampOn=FALSE
ViewFile=FALSE
JobID=0
SelectionSetName=
AcadProfile=AutoCAD MEP (Global)
CategoryName=
LogFilePath=
IncludeLayer=TRUE
LineMerge=FALSE
CurrentPrecision=
PromptForDwfName=FALSE
PwdProtectPublishedDWF=FALSE
PromptForPwd=FALSE
RepublishingMarkups=FALSE
PublishSheetSetMetadata=FALSE
PublishSheetMetadata=FALSE
3DDWFOptions=0 0
"""

TEMPLATE = HEADER + SHEET + FOOTER  # one-sheet DSD (kept for callers that import it)
# multi-sheet footer = exactly the one of M18-ISS-T1.dsd (20 sheets published 2026-11-09): no PromptForPwd /
# RepublishingMarkups lines
FOOTER_MULTI = FOOTER.replace("PromptForPwd=FALSE\n", "").replace("RepublishingMarkups=FALSE\n", "")

USAGE = [
    'usage: acad_dsd.py "<drawing.dwg>" "<layout>" "<out.pdf>" "<out.dsd>"',
    '       acad_dsd.py --sheets "<dwg>|<layout>;<dwg>|<layout>;..." "<out.pdf>" "<out.dsd>"',
    '       acad_dsd.py --sheets-file <sheets.txt> "<out.pdf>" "<out.dsd>"',
]


def win(p):
    # abspath, not resolve(): resolve() turns L:\ into \EgnyteDrive\..., which AutoCAD treats as another file
    return os.path.abspath(str(p)).replace("/", "\\")


def parse_pairs(items):
    """["dwg|layout", ...] -> [(dwg, layout), ...]; raises ValueError on a malformed item."""
    pairs = []
    for raw in items:
        item = raw.strip()
        if not item or item.startswith("#"):
            continue
        dwg, sep, layout = item.rpartition("|")
        if not sep or not dwg.strip() or not layout.strip():
            raise ValueError(f"not '<dwg>|<layout>': {raw!r}")
        pairs.append((dwg.strip().strip('"'), layout.strip()))
    if not pairs:
        raise ValueError("no sheets given")
    return pairs


def build(pairs, pdf):
    """DSD text for [(dwg, layout), ...] into one PDF."""
    blocks, names, seen = [], set(), set()
    for dwg, layout in pairs:
        key = (win(dwg).lower(), layout.lower())
        if key in seen:
            raise ValueError(f"sheet listed twice: {dwg}|{layout}")
        seen.add(key)
        name, k = f"{Path(dwg).stem}-{layout}", 1
        while name.lower() in names:  # section names must be unique
            k += 1
            name = f"{Path(dwg).stem}-{layout}-{k}"
        names.add(name.lower())
        blocks.append(SHEET.format(sheet=name, dwg=win(dwg), layout=layout))
    footer = FOOTER if len(pairs) == 1 else FOOTER_MULTI
    return HEADER + "".join(blocks) + footer.format(pdf=win(pdf), outdir=win(Path(win(pdf)).parent))


def write_dsd(pairs, pdf, dsd):
    """Checks + write; returns (exit code, message)."""
    for dwg, _ in pairs:
        if not Path(dwg).is_file():
            return 1, f"ERROR drawing not found: {dwg}"
    for p in (pdf, dsd):
        if Path(p).exists():
            return 1, f"ERROR already exists, not overwriting: {p}"
    try:
        text = build(pairs, pdf)
    except ValueError as e:
        return 1, f"ERROR {e}"
    Path(pdf).parent.mkdir(parents=True, exist_ok=True)
    Path(dsd).parent.mkdir(parents=True, exist_ok=True)
    Path(dsd).write_text(text, encoding="utf-8")
    return 0, os.path.abspath(dsd).replace("\\", "/")


def main(argv=None):
    args = list(sys.argv[1:] if argv is None else argv)
    pairs = None
    try:
        if args and args[0] == "--sheets" and len(args) == 4:
            pairs = parse_pairs(args[1].split(";"))
            args = args[2:]
        elif args and args[0] == "--sheets-file" and len(args) == 4:
            with open(args[1], encoding="utf-8-sig") as f:
                pairs = parse_pairs(f.read().splitlines())
            args = args[2:]
        elif len(args) == 4 and not args[0].startswith("--"):
            pairs = [(args[0], args[1])]
            args = args[2:]
    except (OSError, ValueError) as e:
        print(f"ERROR {e}")
        return 1
    if pairs is None:
        print("\n".join(USAGE))
        return 2
    code, msg = write_dsd(pairs, args[0], args[1])
    print(msg)
    return code


if __name__ == "__main__":
    sys.exit(main())
