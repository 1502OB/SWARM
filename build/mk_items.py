import json
src = "25139_Bakersfield_CA_Pad 10 ASMEP_10-09-26_MCG Review Comments.pdf"
base = "25139_Bakersfield_CA_Pad_10_ASMEP_10-09-26_MCG_Review_Comments-p%s.png"
import sys, os
sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
from swarm_paths import SWARM, fwd  # noqa: E402
S = fwd(SWARM) + "/"
d = json.load(open(S + 'manifests/doc_scan.json'))['files'][0]
by = {i['id'].split('-p', 1)[1]: i for i in d['items']}
S = {1: 'G101', 8: 'A101', 10: 'A141', 11: 'A200', 12: 'A-301', 22: 'S201', 27: 'S304', 32: None, 33: 'M201', 45: 'E400', 52: 'P201', 53: 'P300'}
NP = "not plumbing"
R = "revit-writer"
RO = "response-only"
M32 = NP + "; also sheet number uncertain (page label M103 duplicates sheet 31, text candidate M200)"
RT = 'Plumbing sheet; no sheet_map.json, Revit assumed'
NG = 'Grid/room not visible in crop'
I = [
 ('1-x14609', RO, 'Title sheet, building data block (area value)', NP, False, 'Architect (FVelasquez) markup'),
 ('1-x14619', RO, 'Title sheet, building data block (area value)', NP, False, 'Architect (FVelasquez) markup'),
 ('8-x14626', RO, 'Floor plan, exterior at door D2 (grid not visible)', NP, False, 'Architectural clearance dimension'),
 ('8-x14631', RO, 'Floor plan, accessible path (grid not visible)', NP, False, 'Architectural clearance dimension'),
 ('8-x14636', RO, 'Floor plan key notes F36 (grease interceptor)', NP + " (architectural key note, only cross-references plumbing drawings; confirm P-sheets consistent)", False, 'Arch key note'),
 ('8-x14642', RO, 'Floor plan key notes F37', NP, False, 'Architectural/landscape key note'),
 ('10-x14705', RO, 'Roof plan', NP, False, 'Architectural/structural'),
 ('10-x14755', RO, 'Roof plan', NP, False, 'Architectural'),
 ('10-x14761', RO, 'Roof plan', NP, False, 'Architect/structural coordination'),
 ('10-x14763', RO, 'Roof plan, PV legend', NP, False, 'Architect/PV coordination'),
 ('11-x14671', RO, 'Exterior elevations', NP, False, 'Architectural'),
 ('11-x14673', RO, 'Exterior elevations / finish schedule', NP, False, 'Architectural'),
 ('11-x14713', RO, 'Exterior elevations, wall', NP, False, 'Architectural'),
 ('12-x14977', RO, 'Wall section', NP + " (NNorouzi measurement note, not a review comment)", False, 'Architectural'),
 ('22-x14854', RO, 'Roof framing plan', NP, False, 'Structural'),
 ('27-x14652', RO, 'Structural section', NP, False, 'Structural'),
 ('32-x14860', RO, 'ASR room, EUH (grid not visible)', M32, False, 'Mechanical'),
 ('32-x14863', RO, 'ASR room door (grid not visible)', M32, False, 'Mechanical/architectural'),
 ('32-x15179', RO, 'ASR room highlight (fragment of 150 CFM)', M32, False, 'Mechanical highlight'),
 ('32-x15183', RO, 'ASR room door louver highlight', M32, False, 'Mechanical highlight'),
 ('32-x15188', RO, 'ASR room, EG highlight', M32, False, 'Mechanical highlight'),
 ('33-x15144', RO, 'Roof plan, above RTUs', NP, False, 'Mechanical'),
 ('33-x15147', RO, 'Roof plan, RTU-1/RTU-3 area', NP, False, 'Mechanical'),
 ('45-x14895', RO, 'Single line diagram, future meter section', NP, False, 'Electrical'),
 ('45-x15131', RO, 'Single line diagram (highlight)', NP, False, 'Electrical'),
 ('45-x15135', RO, 'Single line diagram (highlight)', NP, False, 'Electrical'),
 ('45-x15139', RO, 'Single line diagram', NP, False, 'Electrical'),
 ('52-x15235', R, 'Roof plan, gas line risers at roof (3"G and 2"G)', NG + '; confirm all roof gas branches ("typ.")', False, RT),
 ('53-x14622', 'calc', 'Key notes list, note 3 grease interceptor (currently 3500 GALLON)', 'Interceptor sizing calc needed (3500 to 4000 gal), then update notes/schedule/details consistently', True, 'Volume change needs verification before drawing change'),
 ('53-x14904', R, 'Building exterior at FCO-1', NG, False, RT),
 ('53-x14910', R, 'Building exterior near FCO-1 (adjacent leg)', NG, False, RT),
 ('53-x14918', R, 'Building exterior, side wall near key note 6', NG, False, RT),
 ('53-x14924', R, 'Building exterior, side wall at curved path', NG, False, RT),
 ('53-x14928', R, 'Grease waste (G) line, floor plan', NG + '; confirm which tenant branches to remove', False, RT),
 ('53-x15142', 'calc', 'Plumbing floor plan, general (text box above grid bubbles 2-5)', 'Utility Matrix dated 8/28/26 not in inputs; cannot define change until provided', True, 'Verification against matrix may change fixture/gas/water sizing'),
]
items = []
for n, (k, route, loc, rs, calc, rr) in enumerate(I, 1):
    a = by[k]
    pg = a['page']
    items.append({"id": "MCG-%03d" % n, "source_file": src, "page": pg, "sheet_number": S[pg], "location": loc,
        "comment": a['content'], "comment_source": "bluebeam_mcp", "author": a['author'],
        "crop": S + "crops/" + base % k, "route": route, "route_reason": rr,
        "needs_calc": calc, "status": "needs_review", "review_reason": rs, "markup_id": a['id'],
        "applies_to": ["Pad 10", "Pad 11", "Pad 12"]})
doc = {"project": "Riverview Marketplace, Pad 10 (Shell Building)", "project_number": "25.139.42 (file prefix 25139)",
 "address": "SWC Coffee & Rosedale, Bakersfield, CA", "client": "Coffee Road Ventures, LLC (535 Anton Blvd. Suite 850, Costa Mesa, CA 92626; contact Brett Thornton)",
 "architect": "MCG Architecture",
 "plan_check_identifier": "No plan check number found; sheets show '1ST PLAN CHECK SUBMITTAL 06.26.26' and '1ST CITY SUBMITTAL 06.26.2026'",
 "review_set": "ASMEP 10-09-26 MCG review comments, 54 pages", "applies_also_to": "Pads 11 and 12 per email from MCG",
 "reviewers": "clee (2026-09-25, 2026-10-09), NNorouzi (REVISED tags 2026-09-28), FVelasquez (Architect, 2026-07-01)",
 "sheet_map": "inputs/sheet_map.json absent; Revit assumed for plumbing sheets",
 "tool_path": "bluebeam_mcp (open_file + list_markups_in_pdf); PyMuPDF scan used for crops (184 annotations, matched)",
 "sheet_number_source": "Bluebeam page labels; page 32 label M103 is a duplicate so sheet left null; page 11 label A200 differs from text candidate A201",
 "not_itemized": "%d of 184 markups itemized (all text comments). Remaining are blank-comment graphics (clouds, lines, arrows, snapshots, ink), REVISED tags (NNorouzi), and label-only callouts (R7, R14, S, G, F36). Page 10 and 22 ink strokes not decoded as handwriting." % len(items),
 "doc_scan": S + "manifests/doc_scan.json"}
json.dump({"document": doc, "items": items}, open(S + 'manifests/work_items.json', 'w'), indent=1)
print(len(items), sum(i['status'] == 'needs_review' for i in items))
