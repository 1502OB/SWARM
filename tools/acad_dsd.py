"""Write a one-sheet DSD (sheet list) so acad-writer can -PUBLISH one layout to PDF without dialogs.

    python C:/Users/Jeremy/swarm/tools/acad_dsd.py "<drawing.dwg>" "<layout>" "<out.pdf>" "<out.dsd>"

Template: the DSD that worked for 22-001 PC9 (LESSONS A6/A23): page setup of the layout, PDF target, 1200 dpi
vector. Refuses if the PDF or DSD already exists (never overwrites). Prints the DSD path.
The PDF should go to a local folder (swarm/visual/<run-id>/), never straight to L: (shared-drive permission block).
"""
import os
import sys
from pathlib import Path

TEMPLATE = """[DWF6Version]
Ver=1
[DWF6MinorVersion]
MinorVer=1
[DWF6Sheet:{sheet}]
DWG={dwg}
Layout={layout}
Setup=
OriginalSheetPath={dwg}
Has Plot Port=0
Has3DDWF=0
[Target]
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


def win(p):
    # abspath, not resolve(): resolve() turns L:\ into \EgnyteDrive\..., which AutoCAD treats as another file
    return os.path.abspath(str(p)).replace("/", "\\")


def main():
    if len(sys.argv) != 5:
        print('usage: acad_dsd.py "<drawing.dwg>" "<layout>" "<out.pdf>" "<out.dsd>"')
        sys.exit(2)
    dwg, layout, pdf, dsd = sys.argv[1:]
    if not Path(dwg).is_file():
        print(f"ERROR drawing not found: {dwg}")
        sys.exit(1)
    for p in (pdf, dsd):
        if Path(p).exists():
            print(f"ERROR already exists, not overwriting: {p}")
            sys.exit(1)
    Path(pdf).parent.mkdir(parents=True, exist_ok=True)
    Path(dsd).parent.mkdir(parents=True, exist_ok=True)
    text = TEMPLATE.format(sheet=f"{Path(dwg).stem}-{layout}", dwg=win(dwg), layout=layout, pdf=win(pdf),
                           outdir=win(Path(pdf).parent))
    Path(dsd).write_text(text, encoding="utf-8")
    print(os.path.abspath(dsd).replace("\\", "/"))


if __name__ == "__main__":
    main()
