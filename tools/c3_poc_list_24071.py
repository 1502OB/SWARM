"""Build the 24-071 Ocean North sewer/storm POC list for C3 from manifests/C3-inverts.scout.json.
usage: python tools/c3_poc_list_24071.py <project folder> <out.xlsx>
"""
import json
import sys

from openpyxl import Workbook
from openpyxl.styles import Alignment, Border, Font, PatternFill, Side

proj, out = sys.argv[1], sys.argv[2]
d = json.load(open(proj + "/manifests/C3-inverts.scout.json", encoding="utf-8"))
wb = Workbook()


def F(**k):
    return Font(name="Arial", **k)


thin = Side(style="thin", color="999999")
B = Border(left=thin, right=thin, top=thin, bottom=thin)
HDR = PatternFill("solid", fgColor="1F1F1F")
TBD = PatternFill("solid", fgColor="FFF2CC")


def header(ws, row, cols, widths):
    for i, (c, w) in enumerate(zip(cols, widths), 1):
        x = ws.cell(row, i, c)
        x.font = F(bold=True, color="FFFFFF", size=9)
        x.fill = HDR
        x.alignment = Alignment(wrap_text=True, vertical="center")
        x.border = B
        ws.column_dimensions[x.column_letter].width = w


def put(ws, row, vals, fill=None):
    for i, v in enumerate(vals, 1):
        x = ws.cell(row, i, v)
        x.font = F(size=9)
        x.border = B
        x.alignment = Alignment(wrap_text=True, vertical="top")
        if fill:
            x.fill = fill


INNER = {4: 4.026, 3: 3.068, 2: 2.067}  # in, nominal -> inner diameter used by the Revit pipe types (PVC DWV / sch 40)

ws = wb.active
ws.title = "POC List"
ws["A1"] = "24-071 PACIFIC EDGE HOTEL - OCEAN NORTH: SEWER & STORM POINTS OF CONNECTION FOR C3"
ws["A1"].font = F(bold=True, size=12)
ws["A2"] = ("120 Degreez (plumbing), prepared 11-10-2026 from Revit model PLMB-24220-R25 (100% DD). Elevations in feet, "
            "project level datum (Beach level = 20.30 ft): confirm datum with C3 (Questions tab). Locations = arch grid lines "
            "(ARCH ON model) + offset. Yellow rows = to confirm / TBD. C3: please fill column N.")
ws["A2"].font = F(size=9, italic=True)
ws.merge_cells("A2:N2")
ws.row_dimensions[2].height = 32
ws["A2"].alignment = Alignment(wrap_text=True, vertical="top")
cols = ["ID", "Building", "System", "Description / status", "Size (in)", "Pipe CL elev at POC (ft)", "Pipe inner dia (in)",
        "INVERT (ft)", "Slope (ft/ft)", "Grid location (arch grids)", "Dist. to ext. wall (ft)", "Revit element id",
        "Action / note", "C3 response"]
header(ws, 4, cols, [8, 11, 11, 42, 7, 11, 9, 10, 8, 24, 9, 12, 42, 26])
r = 5


def row(id_, bld, sysn, desc, size, cl, idia, slope, grid, ext, elem, note, confirm=True):
    global r
    inv = f'=IF(AND(ISNUMBER(F{r}),ISNUMBER(G{r})),ROUND(F{r}-G{r}/24,2),"TBD")'
    put(ws, r, [id_, bld, sysn, desc, size, cl, idia, inv, slope, grid, ext, elem, note, ""], TBD if confirm else None)
    ws.cell(r, 8).font = F(size=9, bold=True)
    r += 1


sx = {x[0]: x for x in d["san_exits"]["SURF"]}
vx = {x[0]: x for x in d["san_exits"]["VILLA"]}
surf = [
    (22823239, "Building sewer exit candidate, 4\" gravity at exterior wall",
     "Alternate gravity exit (higher than SS-4); reference only."),
    (22823108, "Building sewer exit candidate, 4\" gravity at exterior wall",
     "Alternate gravity exit; reference only."),
    (22822930, "Building sewer exit candidate, 4\" gravity near exterior wall (north side)", "Alternate gravity exit; reference only."),
    (22819566, "LOWEST gravity building drain, 4\", Beach level, at exterior wall (grids 3.14 / C.2)",
     "USE THIS for the Surf sewer POC invert (lowest level, gravity)."),
]
for i, (pid, desc, note) in enumerate(surf, 1):
    x = sx[pid]
    row(f"SS-{i}", "SURF", "Sanitary", desc, x[1], x[5], INNER[x[1]], x[8], x[9], x[10], pid, note)
ej = d["sewage_ejector"]["SURF_SE_1_2"]
c = ej["discharge_piping_connectors"][0]
if False: row("SS-5", "SURF", "Sanitary (pumped)",
    "Duplex sewage ejector SE-1/2 (40\"D x 48\"W basin): two 2\" discharge connections; force main not yet routed",
    2, c["Z_datum"], INNER[2], "", ej["nearest_2_arch_grids_ft"], ej["nearest_ext_wall_ft"], ej["id"],
    "Force main route and POC TBD with C3 (Questions #2). Elevation shown = discharge connection, not a POC invert.")
villa = [
    (22877529, "Site sewer lateral end, outside the building (~41 ft from ext. wall)",
     "Lowest Villa sanitary point; likely the site POC. Confirm main and invert."),
    (22879044, "4\" line start ~29 ft outside the building, 0 slope (stub)", "Confirm; may be an unfinished site run."),
    (22879107, "4\" line start ~30 ft outside the building, 0 slope (stub)", "Confirm; may be an unfinished site run."),
]
for i, (pid, desc, note) in enumerate(villa, 1):
    x = vx[pid]
    row(f"VS-{i}", "VILLA", "Sanitary", desc, x[1], x[5], INNER[x[1]], x[8], x[9], x[10], pid, note)
row("DS-1", "DRIFTWOOD", "Sanitary",
    "No building sewer exit designed yet (D/V restroom and food service waste/vent in progress)",
    "", "", "", "", "", "", "", "TBD: 120D to provide after the D/V W/V layout (comments D062, D063).")
row("SD-S", "SURF", "Storm",
    "18 roof leaders (3\") drop to Beach level, capped 2 ft below slab: collection to site storm by C3 (see Storm Leaders tab)",
    3, "", "", "", "see tab", "", "22889480-22889531",
    "Leader bottoms at 18.30 ft (tab). Collection main / POC per the existing connection (Questions #3).")
row("SD-V", "VILLA", "Storm", "No storm piping designed: existing gutters/downspouts to remain (meeting notes E8)",
    "", "", "", "", "", "", "", "TBD")
row("SD-D", "DRIFTWOOD", "Storm", "No storm piping designed: existing gutters/downspouts to remain (meeting notes E8)",
    "", "", "", "", "", "", "", "TBD")
ws.freeze_panes = "A5"
ws.cell(r + 1, 1, ("Invert (column H) = pipe centerline elevation at the POC minus half the inner diameter. Centerline "
                   "elevations and sizes read from Revit (element ids in column L). Levels (ft): Surf Beach 20.30, Alley "
                   "29.91, Basement 39.08, Ground 48.24, Roof 57.66; Driftwood/Villa Beach 20.30.")).font = F(size=8, italic=True)

w2 = wb.create_sheet("Storm Leaders")
header(w2, 1, ["Leader", "Revit id", "Size (in)", "Bottom (capped) elev (ft)", "Roof drains served",
               "Grid location / dist. to ext. wall (ft)"], [8, 10, 8, 12, 16, 52])
lg = {}
for s in d["storm_leaders"]["leader_bottom_grids_and_ext_wall"].split(" | "):
    k, _, rest = s.partition(" ")
    lg[k] = rest
for i, x in enumerate(d["storm_leaders"]["SURF"], 2):
    put(w2, i, [x[0], x[1], x[6], x[4], x[7], lg.get(x[0], "")])
w2.cell(len(d["storm_leaders"]["SURF"]) + 3, 1,
        ("All Surf leaders: 3\" PVC SD, capped 2 ft below the Beach slab: 'connect to site storm drain, see Civil'. "
         "Secondary (overflow) roof drainage per CPC 1101.12.2 to be coordinated in CD.")).font = F(size=8, italic=True)

w3 = wb.create_sheet("Other Open Ends (ref)")
header(w3, 1, ["Building", "Revit id", "Size (in)", "CL elev (ft)", "Invert (ft)", "Slope", "Grid location",
               "Ext. wall (ft)", "Note"], [10, 10, 7, 9, 9, 7, 26, 8, 44])
used = {p for p, _, _ in surf} | {p for p, _, _ in villa}
k = 2
for b in ("SURF", "VILLA", "DRIFTWOOD"):
    for x in d["san_exits"][b]:
        if x[0] in used:
            continue
        note = "interior branch end / stub: not a POC"
        if x[0] in (22878182, 22878446):
            note = "on a SURF level but located in the Villa footprint: check the level assignment"
        if b == "DRIFTWOOD":
            note = "2\" stub ~89 ft from the building: likely misplaced, ignore for C3"
        put(w3, k, [b, x[0], x[1], x[5], x[7], x[8], x[9], x[10], note])
        k += 1

w4 = wb.create_sheet("Questions for C3")
header(w4, 1, ["#", "Question", "C3 answer"], [4, 100, 40])
qs = [
    "DATUM: our model levels for all buildings share one datum (e.g. Ocean North Beach level = 20.30 ft, Parking B3 = -1.25 ft). Please confirm this matches the vertical datum of your grading/utility plans (e.g. NAVD88), or give the offset.",
    "SURF SANITARY: the Surf building drain exits near grid 3.14 / C.2 at the exterior wall; earlier plans noted an 'approximate location of civil sewage pump station'. Our Surf building sewer is GRAVITY from the lowest (Beach) level at invert ~16.88 ft (SS-4). Please confirm the site main location and invert at that POC.",
    "SURF STORM: per the 10/27 coordination minutes, storm must tie into the existing connection points of the original building and may discharge directly to the ocean. Please provide those connection locations/inverts and confirm the outfall (public storm drain vs ocean). This decides whether roof overflow can combine with the primary leaders (CPC 1101.12.2.2.2).",
    "VILLA SANITARY: our lowest Villa point is a 4\" lateral at invert ~9.27 ft about 41 ft outside the building (VS-1). Please confirm your main location and invert at that POC.",
    "DRIFTWOOD sanitary, and Villa/Driftwood storm: not designed yet. Please provide your preferred sewer and storm POC locations/inverts so we can route to them.",
    "GREASE INTERCEPTOR: location remains on hold pending your GI shift review (Surf and Driftwood). Please advise when approved.",
    "SITE: for every building on the Site Inverts tab, please give the site sewer and storm main (or existing lateral) location and invert you want each building to connect to, so we can confirm cover/slope from our gravity inverts.",
    "PARKING / below-grade levels: our lowest gravity line in the Parking garage (B3) is at ~-9.28 ft. Please confirm whether the garage sewer and storm are pumped up to a site gravity POC, and at what elevation.",
]
for i, q in enumerate(qs, 2):
    put(w4, i, [i - 1, q, ""])
# --- Site-wide inverts (Jeremy 11-10: all buildings incl. Ocean South + Village; lowest level, gravity line, estimates OK)
we = wb.create_sheet("Site Inverts", 0)
we["A1"] = "24-071 PACIFIC EDGE HOTEL - SITE SEWER & STORM INVERTS AT BUILDING FACE (GRAVITY, LOWEST LEVEL) - PRELIMINARY FOR C3"
we["A1"].font = F(bold=True, size=12)
we["A2"] = ("Rule: lowest modeled GRAVITY invert at the building's lowest piped level, when that pipe end is within 10 ft of the building "
            "(col J, from Revit PLMB-24220-R25). Otherwise ESTIMATE (col K) = start elev (E) - depth (F) - interior run (G) x slope (H). "
            "Blue = assumption, edit as needed. Pumped lines / force mains not used. Elevations in ft, project level datum "
            "(Ocean North Beach level = 20.30; same 112.18 ft internal offset on every building). Area grouping per site key plan: confirm.")
we["A2"].font = F(size=9, italic=True)
we.merge_cells("A2:M2")
we.row_dimensions[2].height = 42
we["A2"].alignment = Alignment(wrap_text=True, vertical="top")
header(we, 4, ["Area", "Building", "System", "Size (in)", "Start point (lowest level)", "Start elev (ft)", "Depth below start (ft)",
               "Interior run (ft)", "Slope (ft/ft)", "Modeled invert (ft)", "INVERT (ft)", "Basis", "Note / Revit element"],
       [12, 12, 8, 7, 26, 9, 9, 9, 8, 10, 10, 34, 48])
BLUE = Font(name="Arial", size=9, color="0000FF")
EST_S = (2.5, 25, 0.0208)  # sewer: 2.5 ft below floor, 25 ft interior run, 1/4"/ft
EST_D = (2.0, 0, 0.0104)   # storm: downspouts/leaders collected 2 ft below the floor/grade
def est(area, bld, sysn, size, lvl_name, elev, rule, note):
    d = EST_S if sysn == "Sewer" else EST_D
    return [area, bld, sysn, size, f"{lvl_name} ({elev:.2f})", elev, d[0], d[1], d[2], "", "Estimate", note]
def mod(area, bld, sysn, size, lvl_name, inv, note):
    return [area, bld, sysn, size, lvl_name, "", "", "", "", inv, "Modeled (Revit)", note]
rows = [
 ["Ocean North", "SURF", "Sewer", 4, "Beach level", "", "", "", "", "='POC List'!H8", "Modeled (Revit)", "4\" building drain at ext. wall, grids 3.14 / C.2 (22819566, SS-4). Ends at the SE-1/2 ejector basin: the alternates SS-1..3 are ~17.6."],
 ["Ocean North", "SURF", "Storm", 6, "Leader caps L01-L18 (18.30)", 18.30, 0.5, 95, 0.0104, "", "Estimate", "6\" collector under the Beach slab from the farthest leader to the SE face, 1/8\"/ft."],
 est("Ocean North", "VILLA", "Sewer", 4, "Beach level", 20.30, "", "Modeled lowest end 9.27 (22877529) is 32 ft outside the building (site run): not used."),
 est("Ocean North", "VILLA", "Storm", 4, "Beach level", 20.30, "", "Existing gutters/downspouts to remain; no storm piping designed."),
 est("Ocean North", "DRIFTWOOD", "Sewer", 4, "Beach level", 20.30, "", "Driftwood W/V not designed (only basement fixtures modeled)."),
 est("Ocean North", "DRIFTWOOD", "Storm", 4, "Beach level", 20.30, "", "Existing gutters/downspouts to remain; no storm piping designed."),
 est("Ocean South", "COTTAGE", "Sewer", 4, "Alley level", 33.42, "", "46 SAN pipes modeled, no open exit end."),
 est("Ocean South", "COTTAGE", "Storm", 4, "Alley level", 33.42, "", "SD lowest at Basement level; no exit end modeled."),
 est("Ocean South", "REEF WING", "Sewer", 4, "Beach level", 21.69, "", "Only 2 SAN pipes modeled."),
 mod("Ocean South", "REEF WING", "Storm", 4, "Beach level", 19.50, "22775960, 0.2 ft from ext. wall, grids B.2 / 7.10."),
 mod("Ocean South", "NEW REEF", "Sewer", 4, "Beach level", 17.31, "22620388, 1.0 ft from ext. wall, grids A.2 / 7."),
 mod("Ocean South", "NEW REEF", "Storm", 3, "Beach level", 18.33, "22777043, 1.8 ft from ext. wall, grids 7.8 / B (8 storm exits 18.33-18.95)."),
 mod("Ocean South", "REEF TOWER", "Sewer", 4, "Beach level", 18.84, "22079930, 0.5 ft from ext. wall, grids A.6 / 5.4."),
 mod("Ocean South", "REEF TOWER", "Storm", 3, "Beach level", 18.96, "22881724, at ext. wall, grids 5.5 / 7 (6 storm exits 18.96-19.51)."),
 mod("Village", "BAHIA", "Sewer", 4, "Alley level", 27.14, "22287057, 7.0 ft from ext. wall, grids E / D.8 (higher alternate 22073819 at 30.46)."),
 mod("Village", "BAHIA", "Storm", 4, "Alley level", 37.35, "22321050, 2.5 ft from ext. wall, grids H.10 / H.9. Runs overhead at the Alley level (~6 ft above floor): verify."),
 est("Village", "LOBBY / COAST", "Sewer", 4, "Alley level", 31.73, "", "SAN only at (E) Basement 38.82, no open exit end."),
 est("Village", "LOBBY / COAST", "Storm", 4, "Alley level", 31.73, "", "13 SD pipes, no open exit end."),
 mod("Village", "SEAS", "Sewer", 3, "Basement level", 32.06, "22088605, 0.1 ft from ext. wall, grids 6 / H.8 (4\" 22316696 at 32.17)."),
 mod("Village", "SEAS", "Storm", 6, "Basement level", 33.34, "22312868, 4.2 ft from ext. wall, grids H.5 / 8.5."),
 mod("Village", "PARKING", "Sewer", 4, "Level B3", -9.28, "22549201, 9.0 ft from ext. wall, grids 1.1 / H; next to the SE-5/6 ejector. Below-grade garage: gravity to site unlikely, confirm."),
 est("Village", "PARKING", "Storm", 4, "Level B3", -1.25, "", "SD lowest at B2 (9.75), no exit end. Below-grade: likely pumped, confirm."),
]
for i, v in enumerate(rows, 5):
    put(we, i, v[:10] + [f'=IF(ISNUMBER(J{i}),J{i},IF(ISNUMBER(F{i}),ROUND(F{i}-G{i}-H{i}*I{i},2),"TBD"))'] + v[10:])
    for c in (6, 7, 8, 9):
        if v[c - 1] != "":
            we.cell(i, c).font = BLUE
    we.cell(i, 11).font = F(size=9, bold=True)
    if v[10] == "Estimate":
        we.cell(i, 11).fill = TBD
we.freeze_panes = "C5"
n = len(rows) + 6
we.cell(n, 1, "Yellow INVERT = estimate. Lowest levels used (ft): Surf/Villa/Driftwood Beach 20.30; Cottage Alley 33.42; Reef Wing Beach 21.69; "
              "New Reef Beach 21.50; Reef Tower Beach 21.94; Bahia Alley 31.31; Lobby/Coast Alley 31.73; Seas Basement 36.27; Parking B3 -1.25.").font = F(size=8, italic=True)
we.cell(n + 1, 1, "Detail for Ocean North: tabs POC List, Storm Leaders, Other Open Ends. Ocean South / Village detail: Revit ids in column M.").font = F(size=8, italic=True)

wb.active = 0
for wsx in wb.worksheets:
    wsx.sheet_view.showGridLines = False
wb.save(out)
print(out, "POC rows", r - 5)
