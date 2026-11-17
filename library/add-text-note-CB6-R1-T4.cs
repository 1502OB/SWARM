// add-text-note: wrapped note + leader + clockwise cloud + R25 delta, MEP clash check, offset override
// CB6-R1-T4 v4 | operation: add-text-note (+ revision cloud + Revision Tag), replace v3 placement | run 20261109-1412
// v4 (redo approved by Jeremy 2026-11-09): v3 landed note 15034309 / cloud 15034316 / tag 15034327 but failed the
// visual check (GSAN 6 pipe through the text, leader hidden on the GI linework, cloud edge cutting the GI tank, delta
// on the cloud edge). v4, in ONE SubTransaction inside the bridge's "auto" transaction:
//  (a) deletes exactly 15034309, 15034316, 15034327 after checking class / owner view / revision / text / tagged cloud;
//  (b) creates the note with its head at GI + (-34, dy) model ft, dy = 18, else 16, 14, 12, 10 (first that keeps text,
//      cloud and delta >= 1 ft inside the crop), a long straight RIGHT-side leader ending on the GI's WEST edge
//      (westmost point of the GI's visible geometry in the view), a clockwise cloud around the TEXT ONLY
//      (+1/4 in paper = 2.667 ft) and one Revision Tag at R25 (right - 0.17 in, top + 0.08 in), nudged up in 0.02 in
//      steps until the tag box bottom is >= 0.08 in paper above the cloud's top line.
// Clash rule (NO_GO, nothing changed): text box, cloud (+0.05 in scallop bulge) and tag box must not overlap any
// element box in the view of Pipes (refined to the pipe centre line +/- radius), Pipe Fittings, Pipe Accessories,
// Plumbing Equipment, Plumbing Fixtures, the GI's own box, the GSAN 6 open end, or any text note / tag / annotation
// symbol / other cloud; the leader may cross linework but not text/tags/symbols/clouds. Link lines are ignored.
// parameters: ["dryrun"] = read-only (planned positions, clashes, clear alternatives); [] or ["run"] = do it;
//             optional "offset=dx,dy" (model ft from the GI) forces the head offset (no dy stepping, still checked).
// Body of: public static object Execute(Document document, object[] parameters); transactionMode "auto".
System.Globalization.CultureInfo inv = System.Globalization.CultureInfo.InvariantCulture;
const long VIEW_ID = 12968396L;
const long GI_ID = 14839803L;
const long SHEET_ID = 12968408L;
const long VP_ID = 12975814L;
const long OLD_NOTE = 15034309L, OLD_CLOUD = 15034316L, OLD_TAG = 15034327L;
const string VIEW_NAME = "PLUMBING LEVEL 1 WASTE & VENT FLOOR PLAN";
const string SHEET_NO = "P2.10";
const string TYPE_NAME = "120D Notes 1/8\"";
const string REV_DESC = "FEWD Submittal";
const string TAG_FAMILY = "Revision Tag";
const string KEY = "1,500 GAL GRAVITY GREASE INTERCEPTOR";
string[] LINES = new string[] {
    "1,500 GAL GRAVITY GREASE INTERCEPTOR (GGI), 56 DFU",
    "(3) MANHOLES + SAMPLE BOX, 5'-0\" MIN. CLEARANCE TO OPEN MANHOLES",
    "LOCATION PER COUNTY DEH",
    "SEE CIVIL DRAWINGS FOR GI DETAIL" };
string NOTE_TEXT = string.Join("\r", LINES);
double GI_X_REF = -16.452, GI_Y_REF = -167.873;   // approved GI point, checked against the live point (<= 0.1 ft)
double DX = -34.0;                                 // approved head offset X from the GI
double[] DYS = new double[] { 18, 16, 14, 12, 10 }; // approved dy, reduced in 2 ft steps only for the crop top
double WRAP_IN = 2.5;
double GSAN_X = -29.8, GSAN_Y = -157.4;
double CROP_SPARE = 1.0;                           // model ft inside the crop

bool dry = false; bool hasOv = false; double ovDx = 0, ovDy = 0;
if (parameters != null)
{
    foreach (object po in parameters)
    {
        if (po == null) continue;
        string ps = po.ToString().Trim();
        if (ps.Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;
        else if (ps.StartsWith("offset=", StringComparison.OrdinalIgnoreCase))
        {
            string[] pp = ps.Substring(7).Split(',');
            if (pp.Length == 2) { ovDx = double.Parse(pp[0].Trim(), inv); ovDy = double.Parse(pp[1].Trim(), inv); hasOv = true; }
        }
    }
}
string TAIL0 = "\ndone=0 skipped=0 failed=0";

// ---------------- 1. Preflight ----------------
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0)
    return "NO_GO wrong document open: " + title + TAIL0;

View view = document.GetElement(new ElementId(VIEW_ID)) as View;
if (view == null) return "NO_GO view " + VIEW_ID + " not found" + TAIL0;
if (view.Name != VIEW_NAME) return "NO_GO view " + VIEW_ID + " name is '" + view.Name + "'" + TAIL0;
if (view.IsTemplate || view.ViewType != ViewType.FloorPlan) return "NO_GO view " + VIEW_ID + " type " + view.ViewType + TAIL0;
int S = view.Scale;
if (S != 128) return "NO_GO view scale is 1:" + S + " (task assumes 1:128)" + TAIL0;
XYZ rd = view.RightDirection;
if (Math.Abs(rd.X - 1.0) > 1e-6) return "NO_GO view is rotated, RightDirection=(" + T4F(rd.X) + "," + T4F(rd.Y) + ")" + TAIL0;
double IN = S / 12.0;   // model ft per paper inch

Viewport vp = document.GetElement(new ElementId(VP_ID)) as Viewport;
if (vp == null) return "NO_GO viewport " + VP_ID + " not found" + TAIL0;
if (vp.ViewId.Value != VIEW_ID || vp.SheetId.Value != SHEET_ID) return "NO_GO viewport " + VP_ID + " shows view " + vp.ViewId.Value + " on sheet " + vp.SheetId.Value + TAIL0;
ViewSheet sheet = document.GetElement(vp.SheetId) as ViewSheet;
if (sheet == null || sheet.SheetNumber != SHEET_NO) return "NO_GO sheet " + SHEET_ID + " is not " + SHEET_NO + TAIL0;

string[] catNames = new string[] { "TextNotes", "RevisionClouds", "RevisionCloudTags" };
BuiltInCategory[] cats = new BuiltInCategory[] { BuiltInCategory.OST_TextNotes, BuiltInCategory.OST_RevisionClouds, BuiltInCategory.OST_RevisionCloudTags };
for (int ci = 0; ci < cats.Length; ci++)
    if (view.GetCategoryHidden(new ElementId(cats[ci]))) return "NO_GO category " + catNames[ci] + " is hidden in view " + VIEW_ID + TAIL0;

Element gi = document.GetElement(new ElementId(GI_ID));
if (gi == null) return "NO_GO GI " + GI_ID + " not found" + TAIL0;
LocationPoint giLp = gi.Location as LocationPoint;
if (giLp == null) return "NO_GO GI " + GI_ID + " has no LocationPoint" + TAIL0;
double giX = giLp.Point.X, giY = giLp.Point.Y;
double giOff = Math.Sqrt((giX - GI_X_REF) * (giX - GI_X_REF) + (giY - GI_Y_REF) * (giY - GI_Y_REF));
if (giOff > 0.1) return "NO_GO GI live point (" + T4F(giX) + "," + T4F(giY) + ") is " + T4F(giOff) + " ft from the approved (-16.452,-167.873)" + TAIL0;
string giDesc = (gi.Category == null ? "?" : gi.Category.Name) + " / " + gi.Name;
BoundingBoxXYZ gbb = gi.get_BoundingBox(view);
if (gbb == null) return "NO_GO GI " + GI_ID + " has no bounding box in view " + VIEW_ID + TAIL0;
double[] giBB = new double[] { gbb.Min.X, gbb.Min.Y, gbb.Max.X, gbb.Max.Y };

// GI west edge: westmost point of the GI's visible geometry in this view (falls back to the view bbox)
List<XYZ> gpts = new List<XYZ>();
string westSrc = "geometry";
try { Options gop = new Options(); gop.View = view; T4Pts(gi.get_Geometry(gop), gpts, 0); } catch (Exception) { }
double wX = double.MaxValue, wY = 0;
foreach (XYZ p in gpts) if (p.X < wX) wX = p.X;
if (gpts.Count > 0)
{
    double sy = 0; int ny = 0;
    foreach (XYZ p in gpts) if (p.X <= wX + 0.05) { sy += p.Y; ny++; }
    wY = sy / ny;
}
else { westSrc = "bbox"; wX = giBB[0]; wY = Math.Min(Math.Max(giY, giBB[1]), giBB[3]); }
if (Math.Abs(wX - giBB[0]) > 1.0)
    return "NO_GO GI west point from geometry x=" + T4F(wX) + " differs from the GI view bbox Min.X " + T4F(giBB[0]) + " by more than 1 ft (" + gpts.Count + " pts); check the family" + TAIL0;

Revision rev = null; int revHits = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REV_DESC) { rev = r; revHits++; }
}
if (revHits != 1) return "NO_GO " + revHits + " revisions have Description '" + REV_DESC + "'" + TAIL0;
if (rev.Issued) return "NO_GO revision '" + REV_DESC + "' (id " + rev.Id.Value + ") is issued" + TAIL0;
if (rev.Visibility == RevisionVisibility.Hidden) return "NO_GO revision '" + REV_DESC + "' Visibility=Hidden" + TAIL0;

TextNoteType tnt = null;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(TextNoteType)))
    if (e.Name == TYPE_NAME) { tnt = e as TextNoteType; break; }
if (tnt == null) return "NO_GO text type '" + TYPE_NAME + "' not found" + TAIL0;
Parameter pSize = tnt.get_Parameter(BuiltInParameter.TEXT_SIZE);
double tSize = (pSize == null) ? (0.125 / 12.0) : pSize.AsDouble();
Parameter pWf = tnt.get_Parameter(BuiltInParameter.TEXT_WIDTH_SCALE);
double tWf = (pWf == null || pWf.AsDouble() <= 0) ? 1.0 : pWf.AsDouble();
double wrapP = WRAP_IN / 12.0;
double minWp = TextElement.GetMinimumAllowedWidth(document, tnt.Id), maxWp = TextElement.GetMaximumAllowedWidth(document, tnt.Id);
if (wrapP < minWp) wrapP = minWp;
if (wrapP > maxWp) wrapP = maxWp;

List<FamilySymbol> tagSyms = new List<FamilySymbol>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol;
    if (fs != null && ((fs.Family != null && fs.Family.Name == TAG_FAMILY) || fs.Name == TAG_FAMILY)) tagSyms.Add(fs);
}
if (tagSyms.Count == 0) return "NO_GO no Revision Cloud Tag symbol of family or type '" + TAG_FAMILY + "'" + TAIL0;
tagSyms.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));
FamilySymbol tagSym = tagSyms[0];

BoundingBoxXYZ cb = view.CropBox;
XYZ c0 = cb.Transform.OfPoint(cb.Min), c1 = cb.Transform.OfPoint(cb.Max);
double[] cropR = new double[] { Math.Min(c0.X, c1.X), Math.Min(c0.Y, c1.Y), Math.Max(c0.X, c1.X), Math.Max(c0.Y, c1.Y) };
bool cropOn = view.CropBoxActive;
double[] cropUse = cropOn ? cropR : null;

// ---------------- 2. The three v3 elements (verify before delete) ----------------
Element oN = document.GetElement(new ElementId(OLD_NOTE));
Element oC = document.GetElement(new ElementId(OLD_CLOUD));
Element oT = document.GetElement(new ElementId(OLD_TAG));
List<string> oldBad = new List<string>();
if (oN != null)
{
    TextNote tn = oN as TextNote;
    if (tn == null) oldBad.Add(OLD_NOTE + " is " + oN.GetType().Name + " not TextNote");
    else if (tn.OwnerViewId.Value != VIEW_ID) oldBad.Add(OLD_NOTE + " owner view " + tn.OwnerViewId.Value);
    else if (!T4Norm(tn.Text).Contains(KEY)) oldBad.Add(OLD_NOTE + " text does not contain the GI key");
}
if (oC != null)
{
    RevisionCloud rc = oC as RevisionCloud;
    if (rc == null) oldBad.Add(OLD_CLOUD + " is " + oC.GetType().Name + " not RevisionCloud");
    else if (rc.OwnerViewId.Value != VIEW_ID) oldBad.Add(OLD_CLOUD + " owner view " + rc.OwnerViewId.Value);
    else if (rc.RevisionId.Value != rev.Id.Value) oldBad.Add(OLD_CLOUD + " revision " + rc.RevisionId.Value + " not " + rev.Id.Value);
}
double[] tagOff = null;   // tag box relative to its head, measured on the v3 tag
if (oT != null)
{
    IndependentTag it = oT as IndependentTag;
    if (it == null) oldBad.Add(OLD_TAG + " is " + oT.GetType().Name + " not IndependentTag");
    else if (it.OwnerViewId.Value != VIEW_ID) oldBad.Add(OLD_TAG + " owner view " + it.OwnerViewId.Value);
    else
    {
        bool tagsCloud = false;
        try { foreach (ElementId x in it.GetTaggedLocalElementIds()) if (x.Value == OLD_CLOUD) tagsCloud = true; } catch (Exception) { }
        FamilySymbol ofs = document.GetElement(it.GetTypeId()) as FamilySymbol;
        bool isRevTag = ofs != null && (ofs.Name == TAG_FAMILY || (ofs.Family != null && ofs.Family.Name == TAG_FAMILY));
        if (!tagsCloud) oldBad.Add(OLD_TAG + " does not tag cloud " + OLD_CLOUD);
        else if (!isRevTag) oldBad.Add(OLD_TAG + " is not a Revision Tag");
        else
        {
            if (ofs != null) tagSym = ofs;
            BoundingBoxXYZ tbb = it.get_BoundingBox(view);
            XYZ th0 = it.TagHeadPosition;
            if (tbb != null) tagOff = new double[] { tbb.Min.X - th0.X, tbb.Min.Y - th0.Y, tbb.Max.X - th0.X, tbb.Max.Y - th0.Y };
        }
    }
}
if (oN == null && oC != null) oldBad.Add("old cloud present but old note gone");
if (oC == null && oT != null) oldBad.Add("old tag present but old cloud gone");
if (oldBad.Count > 0) return "NO_GO old v3 elements not as expected: " + string.Join("; ", oldBad) + TAIL0;
string tagOffSrc = "v3 tag " + OLD_TAG;
if (tagOff == null || (tagOff[2] - tagOff[0]) < 0.05 * IN || (tagOff[2] - tagOff[0]) > 0.6 * IN || (tagOff[3] - tagOff[1]) < 0.05 * IN || (tagOff[3] - tagOff[1]) > 0.6 * IN)
{
    double hh = 0.12 * IN;
    tagOff = new double[] { -hh, -hh, hh, hh }; tagOffSrc = "default +/-0.12 in";
}

// other GI notes in the view (idempotency: a v4 note already placed?)
List<Element> viewEls = new List<Element>();
foreach (Element e in new FilteredElementCollector(document, view.Id).WhereElementIsNotElementType()) viewEls.Add(e);
List<TextNote> otherNotes = new List<TextNote>();
foreach (Element e in viewEls)
{
    TextNote tn = e as TextNote;
    if (tn != null && tn.Id.Value != OLD_NOTE && tn.OwnerViewId.Value == VIEW_ID && T4Norm(tn.Text).Contains(KEY)) otherNotes.Add(tn);
}
double dxUse = hasOv ? ovDx : DX;
if (otherNotes.Count > 1) return "NO_GO " + otherNotes.Count + " other text notes in the view contain the GI key" + TAIL0;
if (otherNotes.Count == 1)
{
    TextNote ex = otherNotes[0];
    if (oN != null) return "NO_GO old note " + OLD_NOTE + " and another GI note " + ex.Id.Value + " both exist; resolve manually" + TAIL0;
    if (Math.Abs(ex.Coord.X - giX - dxUse) > 0.2 || ex.LeaderCount != 1)
        return "NO_GO a GI note " + ex.Id.Value + " exists at offset (" + T4F(ex.Coord.X - giX) + "," + T4F(ex.Coord.Y - giY) + ") that is not the v4 placement; resolve manually" + TAIL0;
    double[] exTb = T4TextBox(ex, S);
    RevisionCloud exC = null; IndependentTag exT = null;
    foreach (Element e in viewEls)
    {
        RevisionCloud rc = e as RevisionCloud;
        if (rc == null || rc.RevisionId.Value != rev.Id.Value) continue;
        double[] rb = T4CloudBox(rc);
        if (rb != null && T4Inside(exTb, rb)) { exC = rc; break; }
    }
    if (exC != null)
        foreach (Element e in viewEls)
        {
            IndependentTag it = e as IndependentTag;
            if (it == null) continue;
            try { if (it.GetTaggedLocalElementIds().Contains(exC.Id)) { exT = it; break; } } catch (Exception) { }
        }
    if (exC == null || exT == null) return "NO_GO v4 note " + ex.Id.Value + " exists but its cloud/tag is missing; resolve manually" + TAIL0;
    return "{\"mode\":\"" + (dry ? "dryrun" : "run") + "\",\"status\":\"skipped: already in target state\",\"note\":" + ex.Id.Value + ",\"cloud\":" + exC.Id.Value + ",\"tag\":" + exT.Id.Value
        + ",\"head_minus_gi\":[" + T4F(ex.Coord.X - giX) + "," + T4F(ex.Coord.Y - giY) + "]}"
        + "\ndone=0 skipped=3 failed=0 skipped_ids=" + ex.Id.Value + "," + exC.Id.Value + "," + exT.Id.Value;
}

// ---------------- 3. Obstacles ----------------
HashSet<long> excl = new HashSet<long>(); excl.Add(OLD_NOTE); excl.Add(OLD_CLOUD); excl.Add(OLD_TAG);
HashSet<BuiltInCategory> mepCats = new HashSet<BuiltInCategory>();
mepCats.Add(BuiltInCategory.OST_PipeCurves); mepCats.Add(BuiltInCategory.OST_PipeFitting); mepCats.Add(BuiltInCategory.OST_PipeAccessory);
mepCats.Add(BuiltInCategory.OST_PlumbingEquipment); mepCats.Add(BuiltInCategory.OST_PlumbingFixtures);
double[] win = new double[] { giX - 250, giY - 250, giX + 250, giY + 250 };
List<double[]> obs = new List<double[]>(); List<string> obsLbl = new List<string>(); List<int> obsKind = new List<int>();
List<double[]> obsSeg = new List<double[]>(); List<double> obsHw = new List<double>();
double headHalfW = 0.6 * IN / 12.0 * 12.0 / 12.0 * 12.0 / 12.0, headHalfH = 0.25 / 12.0 * S;
headHalfW = 0.6 / 12.0 * S;
int nMep = 0, nAnno = 0;
foreach (Element e in viewEls)
{
    if (excl.Contains(e.Id.Value) || e.Id.Value == GI_ID) continue;
    try
    {
        double[] bx = null; string lbl = null; int kind = 0; double[] seg = null; double hw = 0;
        if (e is TextNote) { bx = T4TextBox((TextNote)e, S); lbl = "text " + e.Id.Value; kind = 1; }
        else if (e is RevisionCloud)
        {
            bx = T4CloudBox((RevisionCloud)e);
            if (bx != null) bx = T4Grow(bx, 0.15 * IN);
            lbl = "cloud " + e.Id.Value; kind = 2;
        }
        else if (e is IndependentTag)
        {
            IndependentTag it = (IndependentTag)e;
            BoundingBoxXYZ bb = it.get_BoundingBox(view);
            if (bb == null) continue;
            bx = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
            if (it.HasLeader)
            {
                XYZ h = it.TagHeadPosition;
                double[] hb = new double[] { h.X - headHalfW, h.Y - headHalfH, h.X + headHalfW, h.Y + headHalfH };
                double[] isx = T4Intersect(bx, hb);
                bx = (isx == null) ? hb : isx;
            }
            lbl = "tag " + e.Id.Value; kind = 1;
        }
        else if (e.ViewSpecific && e.Category != null && e.Category.CategoryType == CategoryType.Annotation && (e is SpatialElementTag || e is FamilyInstance))
        {
            BoundingBoxXYZ bb = e.get_BoundingBox(view);
            if (bb == null) continue;
            bx = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
            lbl = "anno " + e.Id.Value + " (" + e.Category.Name + ")"; kind = 1;
        }
        else if (e.Category != null && mepCats.Contains(e.Category.BuiltInCategory))
        {
            BoundingBoxXYZ bb = e.get_BoundingBox(view);
            if (bb == null) continue;
            bx = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
            lbl = e.Category.Name + " " + e.Id.Value; kind = 3;
            MEPCurve mc = e as MEPCurve;
            LocationCurve lc = e.Location as LocationCurve;
            if (mc != null && lc != null && lc.Curve is Line)
            {
                XYZ p0 = lc.Curve.GetEndPoint(0), p1 = lc.Curve.GetEndPoint(1);
                seg = new double[] { p0.X, p0.Y, p1.X, p1.Y };
                hw = 0.25;
                try { hw = Math.Max(mc.Diameter / 2.0, 0.1); } catch (Exception) { }
            }
        }
        if (bx == null || !T4Ov(bx, win, 0)) continue;
        obs.Add(bx); obsLbl.Add(lbl); obsKind.Add(kind); obsSeg.Add(seg); obsHw.Add(hw);
        if (kind == 3) nMep++; else nAnno++;
    }
    catch (Exception) { }
}
double[] keep = new double[] { GSAN_X - 1.0, GSAN_Y - 1.0, GSAN_X + 1.0, GSAN_Y + 1.0 };

// ---------------- 4. Plan: estimated size, dy for the crop, clashes ----------------
double estCharP = tSize * tWf * 0.80;
int cpl = Math.Max(8, (int)Math.Floor(wrapP / estCharP));
int estLines = 0;
foreach (string l in LINES)
{
    int cur = 0; int n = 1;
    foreach (string wd in l.Split(' '))
    {
        int need = (cur == 0) ? wd.Length : cur + 1 + wd.Length;
        if (need > cpl && cur > 0) { n++; cur = wd.Length; } else cur = need;
    }
    estLines += n;
}
double estW = wrapP * S, estH = Math.Max(estLines * tSize * 1.7, 1.958 / 12.0) * S;   // >= v3 measured 1.958 in

double[] dyList = hasOv ? new double[] { ovDy } : DYS;
double chosenDy = double.NaN; List<string> dyRej = new List<string>();
double[] Lp = null;
foreach (double dy in dyList)
{
    double[] L = T4Layout(giX + dxUse, giY + dy, estW, estH, S, tagOff);
    string cw = T4Crop(L, cropUse, CROP_SPARE);
    if (cw == null) { chosenDy = dy; Lp = L; break; }
    dyRej.Add(T4F(dy) + ":" + cw);
}

System.Text.StringBuilder rep = new System.Text.StringBuilder();
rep.Append("{\"mode\":\"").Append(dry ? "dryrun" : "run").Append("\",\"view\":").Append(VIEW_ID).Append(",\"scale\":").Append(S);
rep.Append(",\"gi\":[").Append(T4F(giX)).Append(",").Append(T4F(giY)).Append("],\"gi_desc\":\"").Append(T4Esc(giDesc)).Append("\"");
rep.Append(",\"gi_bbox\":").Append(T4Box(giBB)).Append(",\"gi_west\":[").Append(T4F(wX)).Append(",").Append(T4F(wY)).Append("],\"gi_west_src\":\"").Append(westSrc).Append("\",\"gi_geom_pts\":").Append(gpts.Count);
rep.Append(",\"rev\":").Append(rev.Id.Value).Append(",\"text_type\":").Append(tnt.Id.Value).Append(",\"tag_sym\":").Append(tagSym.Id.Value);
rep.Append(",\"tag_off\":").Append(T4Box(tagOff)).Append(",\"tag_off_src\":\"").Append(tagOffSrc).Append("\"");
rep.Append(",\"crop_on\":").Append(cropOn ? "true" : "false").Append(",\"crop\":").Append(T4Box(cropR));
rep.Append(",\"old_present\":\"").Append(oN != null ? OLD_NOTE.ToString() : "-").Append(",").Append(oC != null ? OLD_CLOUD.ToString() : "-").Append(",").Append(oT != null ? OLD_TAG.ToString() : "-").Append("\"");
rep.Append(",\"obstacles\":{\"mep\":").Append(nMep).Append(",\"anno\":").Append(nAnno).Append("}");
rep.Append(",\"override\":").Append(hasOv ? "true" : "false");
rep.Append(",\"est_WxH\":[").Append(T4F(estW)).Append(",").Append(T4F(estH)).Append("]");
rep.Append(",\"dy_rejected\":\"").Append(T4Esc(string.Join("; ", dyRej))).Append("\"");

// clear alternatives (information for Jeremy; never used automatically)
List<double[]> alts = new List<double[]>();
for (double adx = -24; adx >= -70; adx -= 2)
    for (double ady = 18; ady >= 6; ady -= 2)
    {
        double[] L = T4Layout(giX + adx, giY + ady, estW, estH, S, tagOff);
        if (T4Crop(L, cropUse, CROP_SPARE) != null) continue;
        double aax = L[2], aay = L[1] + (L[3] - L[1]) / 2.0;
        if (T4Clash(L, aax, aay, wX, wY, S, obs, obsLbl, obsKind, obsSeg, obsHw, giBB, keep).Count > 0) continue;
        double dd = Math.Sqrt((adx - DX) * (adx - DX) + (ady - 18) * (ady - 18));
        alts.Add(new double[] { adx, ady, dd });
    }
alts.Sort((a, b) => a[2].CompareTo(b[2]));
rep.Append(",\"clear_alternatives\":\"").Append(string.Join(" ", alts.Take(6).Select(a => "(" + T4F(a[0]) + "," + T4F(a[1]) + ")"))).Append("\"");

if (Lp == null)
{
    rep.Append(",\"status\":\"NO_GO no dy in [").Append(string.Join(",", dyList.Select(v => T4F(v)))).Append("] keeps text, cloud and delta 1 ft inside the crop\"}");
    rep.Append("\ndone=0 skipped=0 failed=0");
    return "NO_GO " + rep.ToString();
}
double hxP = giX + dxUse, hyP = giY + chosenDy;
double axP = Lp[2], ayP = Lp[1] + (Lp[3] - Lp[1]) / 2.0;
List<string> clP = T4Clash(Lp, axP, ayP, wX, wY, S, obs, obsLbl, obsKind, obsSeg, obsHw, giBB, keep);
rep.Append(",\"chosen_offset\":[").Append(T4F(dxUse)).Append(",").Append(T4F(chosenDy)).Append("]");
rep.Append(",\"plan_head\":[").Append(T4F(hxP)).Append(",").Append(T4F(hyP)).Append("]");
rep.Append(",\"plan_text_box\":").Append(T4Box(new double[] { Lp[0], Lp[1], Lp[2], Lp[3] }));
rep.Append(",\"plan_cloud_rect\":").Append(T4Box(new double[] { Lp[4], Lp[5], Lp[6], Lp[7] }));
rep.Append(",\"plan_tag_head\":[").Append(T4F(Lp[8])).Append(",").Append(T4F(Lp[9])).Append("],\"plan_tag_box\":").Append(T4Box(new double[] { Lp[10], Lp[11], Lp[12], Lp[13] })).Append(",\"plan_tag_nudge_ft\":").Append(T4F(Lp[14]));
rep.Append(",\"plan_leader\":[[").Append(T4F(axP)).Append(",").Append(T4F(ayP)).Append("],[").Append(T4F(wX)).Append(",").Append(T4F(wY)).Append("]]");
rep.Append(",\"clashes\":\"").Append(T4Esc(string.Join("; ", clP.Take(20)))).Append("\",\"clash_count\":").Append(clP.Count);

int nOld = (oN != null ? 1 : 0) + (oC != null ? 1 : 0) + (oT != null ? 1 : 0);
if (clP.Count > 0)
{
    rep.Append(",\"status\":\"NO_GO clashes at offset (").Append(T4F(dxUse)).Append(",").Append(T4F(chosenDy)).Append(")\"}");
    rep.Append("\ndone=0 skipped=0 failed=0");
    return "NO_GO " + rep.ToString();
}
if (dry)
{
    rep.Append(",\"would_delete\":").Append(nOld).Append(",\"would_create\":3,\"status\":\"GO\"}");
    rep.Append("\ndone=0 skipped=0 failed=0 (dryrun, nothing changed)");
    return rep.ToString();
}

// ---------------- 5. Delete old + create new (SubTransaction: all or nothing) ----------------
string item = "delete";
List<string> delIds = new List<string>(), newIds = new List<string>();
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    long[] delOrder = new long[] { OLD_TAG, OLD_CLOUD, OLD_NOTE };
    foreach (long dId in delOrder)
    {
        ElementId eid = new ElementId(dId);
        if (document.GetElement(eid) == null) continue;   // tag may go with its cloud
        document.Delete(eid);
        delIds.Add(dId.ToString());
    }
    document.Regenerate();
    foreach (long dId in delOrder)
        if (document.GetElement(new ElementId(dId)) != null) throw new Exception("element " + dId + " still exists after delete");
    if (oT != null && !delIds.Contains(OLD_TAG.ToString())) delIds.Add(OLD_TAG.ToString());

    item = "note";
    TextNoteOptions opts = new TextNoteOptions(tnt.Id);
    opts.HorizontalAlignment = HorizontalTextAlignment.Right;
    opts.VerticalAlignment = VerticalTextAlignment.Top;
    TextNote note = TextNote.Create(document, view.Id, new XYZ(hxP, hyP, 0), wrapP, NOTE_TEXT, opts);
    document.Regenerate();
    newIds.Add(note.Id.Value.ToString());
    if (T4Norm(note.Text) != T4Norm(NOTE_TEXT)) throw new Exception("text read back differs: " + T4Esc(T4Norm(note.Text)));
    if (!note.IsTextWrappingActive) throw new Exception("text wrapping is not active");
    double z = note.Coord.Z;
    note.LeaderRightAttachment = LeaderAtachement.Midpoint;
    Leader ld = note.AddLeader(TextNoteLeaderTypes.TNLT_STRAIGHT_R);
    ld.End = new XYZ(wX, wY, z);
    document.Regenerate();

    IList<Leader> lds = note.GetLeaders();
    if (lds.Count != 1) throw new Exception("note has " + lds.Count + " leaders");
    XYZ lEnd = lds[0].End, lAnc = lds[0].Anchor;
    if (Math.Abs(lEnd.X - wX) > 0.05 || Math.Abs(lEnd.Y - wY) > 0.05) throw new Exception("leader end (" + T4F(lEnd.X) + "," + T4F(lEnd.Y) + ") not at GI west point");
    XYZ hc = note.Coord;
    if (Math.Abs(hc.X - hxP) > 0.05 || Math.Abs(hc.Y - hyP) > 0.05) throw new Exception("note head moved to (" + T4F(hc.X) + "," + T4F(hc.Y) + ")");
    double W = (note.Width > 0.5 * wrapP && note.Width < 1.5 * wrapP) ? note.Width * S : wrapP * S;
    double H = T4Measure(note.Height, estH / S, S);
    double[] L = T4Layout(hc.X, hc.Y, W, H, S, tagOff);
    string cw = T4Crop(L, cropUse, CROP_SPARE);
    if (cw != null) throw new Exception("measured layout outside crop: " + cw);
    List<string> cl = T4Clash(L, lAnc.X, lAnc.Y, lEnd.X, lEnd.Y, S, obs, obsLbl, obsKind, obsSeg, obsHw, giBB, keep);
    if (cl.Count > 0) throw new Exception("measured clashes: " + string.Join("; ", cl.Take(10)));
    rep.Append(",\"note\":").Append(note.Id.Value).Append(",\"head\":[").Append(T4F(hc.X)).Append(",").Append(T4F(hc.Y)).Append("]");
    rep.Append(",\"head_minus_gi\":[").Append(T4F(hc.X - giX)).Append(",").Append(T4F(hc.Y - giY)).Append("]");
    rep.Append(",\"measured_paper_in\":[").Append(T4F(note.Width * 12)).Append(",").Append(T4F(note.Height * 12)).Append("]");
    rep.Append(",\"text_box\":").Append(T4Box(new double[] { L[0], L[1], L[2], L[3] }));
    rep.Append(",\"leader_anchor\":[").Append(T4F(lAnc.X)).Append(",").Append(T4F(lAnc.Y)).Append("],\"leader_end\":[").Append(T4F(lEnd.X)).Append(",").Append(T4F(lEnd.Y)).Append("]");

    item = "cloud";
    double[] rect = new double[] { L[4], L[5], L[6], L[7] };
    XYZ bl = new XYZ(rect[0], rect[1], z), tl = new XYZ(rect[0], rect[3], z), tr = new XYZ(rect[2], rect[3], z), br = new XYZ(rect[2], rect[1], z);
    List<Curve> curves = new List<Curve>();
    curves.Add(Line.CreateBound(bl, tl));   // CLOCKWISE: BL > TL > TR > BR > BL
    curves.Add(Line.CreateBound(tl, tr));
    curves.Add(Line.CreateBound(tr, br));
    curves.Add(Line.CreateBound(br, bl));
    RevisionCloud cloud = RevisionCloud.Create(document, view, rev.Id, curves);
    document.Regenerate();
    newIds.Add(cloud.Id.Value.ToString());
    double area = 0; List<string> order = new List<string>();
    foreach (Curve cv in cloud.GetSketchCurves())
    {
        XYZ p0 = cv.GetEndPoint(0), p1 = cv.GetEndPoint(1);
        area += (p0.X * p1.Y - p1.X * p0.Y);
        order.Add(T4Corner(p0, rect));
    }
    area /= 2.0;
    if (area >= 0) throw new Exception("cloud signed area " + T4F(area) + " is not negative");
    double[] rb = T4CloudBox(cloud);
    if (rb == null || Math.Abs(rb[0] - rect[0]) > 0.05 || Math.Abs(rb[3] - rect[3]) > 0.05) throw new Exception("cloud read back " + (rb == null ? "null" : T4Box(rb)));
    if (T4Ov(T4Grow(rect, 0.05 * IN), giBB, 0)) throw new Exception("cloud intersects GI bbox");
    bool onSheet = false; List<string> shs = new List<string>();
    foreach (ElementId sid in cloud.GetSheetIds()) { shs.Add(sid.Value.ToString()); if (sid.Value == SHEET_ID) onSheet = true; }
    if (!onSheet) throw new Exception("cloud GetSheetIds does not include sheet " + SHEET_ID);
    rep.Append(",\"cloud\":").Append(cloud.Id.Value).Append(",\"rect\":").Append(T4Box(rect)).Append(",\"signed_area\":").Append(T4F(area));
    rep.Append(",\"order\":\"").Append(string.Join(">", order)).Append("\"");

    item = "tag";
    if (!tagSym.IsActive) tagSym.Activate();
    XYZ tagHead = new XYZ(L[8], L[9], z);
    IndependentTag tag = IndependentTag.Create(document, tagSym.Id, view.Id, new Reference(cloud), false, TagOrientation.Horizontal, tagHead);
    tag.TagHeadPosition = tagHead;
    document.Regenerate();
    newIds.Add(tag.Id.Value.ToString());
    double clr = 0.08 * IN, step = 0.02 * IN, extra = 0;
    double[] tbx = T4TagBox(tag, view, tagOff, IN);
    if (tbx[1] < rect[3] + clr)
    {
        extra = Math.Ceiling((rect[3] + clr - tbx[1]) / step) * step;
        XYZ th1 = tag.TagHeadPosition;
        tag.TagHeadPosition = new XYZ(th1.X, th1.Y + extra, th1.Z);
        document.Regenerate();
        tbx = T4TagBox(tag, view, tagOff, IN);
    }
    XYZ th = tag.TagHeadPosition;
    if (tbx[1] < rect[3] + clr - 0.01) throw new Exception("tag box bottom " + T4F(tbx[1]) + " still within 0.08 in of cloud top " + T4F(rect[3]));
    if (Math.Abs(th.X - (rect[2] - 0.17 * IN)) > 0.05) throw new Exception("tag head x " + T4F(th.X) + " not at cloud right - 0.17 in");
    if (cropUse != null && !T4Inside(tbx, T4Grow(cropUse, -CROP_SPARE))) throw new Exception("tag box " + T4Box(tbx) + " not 1 ft inside crop");
    double[] Lt = (double[])L.Clone();
    Lt[10] = tbx[0]; Lt[11] = tbx[1]; Lt[12] = tbx[2]; Lt[13] = tbx[3];
    List<string> clT = T4Clash(Lt, lAnc.X, lAnc.Y, lEnd.X, lEnd.Y, S, obs, obsLbl, obsKind, obsSeg, obsHw, giBB, keep);
    if (clT.Count > 0) throw new Exception("clashes with the placed tag: " + string.Join("; ", clT.Take(10)));
    rep.Append(",\"tag\":").Append(tag.Id.Value).Append(",\"tag_head\":[").Append(T4F(th.X)).Append(",").Append(T4F(th.Y)).Append("]");
    rep.Append(",\"tag_head_minus_cloud_tr\":[").Append(T4F(th.X - rect[2])).Append(",").Append(T4F(th.Y - rect[3])).Append("]");
    rep.Append(",\"tag_box\":").Append(T4Box(tbx)).Append(",\"tag_nudge_ft\":").Append(T4F(L[14] + extra));

    st.Commit();
}
catch (Exception ex)
{
    try { st.RollBack(); } catch (Exception) { }
    return "ROLLED_BACK " + item + ": " + T4Esc(ex.Message) + "\ndone=0 skipped=0 failed=1 (nothing deleted or created)";
}
rep.Append(",\"status\":\"OK\"}");
rep.Append("\ndone=").Append(delIds.Count + newIds.Count).Append(" skipped=0 failed=0 deleted=").Append(string.Join(",", delIds)).Append(" created=").Append(string.Join(",", newIds));
return rep.ToString();
}

// ---------------- helpers (CB6-R1-T4 v4) ----------------
static string T4F(double v) { return Math.Round(v, 3).ToString(System.Globalization.CultureInfo.InvariantCulture); }
static string T4Box(double[] b) { return "[" + T4F(b[0]) + "," + T4F(b[1]) + "," + T4F(b[2]) + "," + T4F(b[3]) + "]"; }
static string T4Esc(string s) { return (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", " ").Replace("\n", " "); }
static string T4Norm(string s)
{
    string t = (s ?? "").Replace("\r\n", "\n").Replace("\r", "\n").Replace("\v", "\n").Trim();
    return t.ToUpperInvariant();
}
static bool T4Ov(double[] a, double[] b, double g)
{
    return a[0] - g < b[2] && b[0] < a[2] + g && a[1] - g < b[3] && b[1] < a[3] + g;
}
static bool T4Inside(double[] inner, double[] outer)
{
    return inner[0] >= outer[0] && inner[1] >= outer[1] && inner[2] <= outer[2] && inner[3] <= outer[3];
}
static double[] T4Grow(double[] a, double m) { return new double[] { a[0] - m, a[1] - m, a[2] + m, a[3] + m }; }
static double[] T4U(double[] a, double x, double y)
{
    return new double[] { Math.Min(a[0], x), Math.Min(a[1], y), Math.Max(a[2], x), Math.Max(a[3], y) };
}
static double[] T4Intersect(double[] a, double[] b)
{
    double[] r = new double[] { Math.Max(a[0], b[0]), Math.Max(a[1], b[1]), Math.Min(a[2], b[2]), Math.Min(a[3], b[3]) };
    if (r[0] >= r[2] || r[1] >= r[3]) return null;
    return r;
}
static double T4Measure(double apiPaper, double estPaper, int S)
{
    if (apiPaper > 0.3 * estPaper && apiPaper < 3.0 * estPaper) return apiPaper * S;
    return estPaper * S;
}
static double[] T4TextBox(TextNote tn, int S)
{
    XYZ c = tn.Coord, b = tn.BaseDirection, u = tn.UpDirection;
    double w = tn.Width * S, h = tn.Height * S;
    double a0 = 0, a1 = 1, b0 = -1, b1 = 0;
    if (tn.HorizontalAlignment == HorizontalTextAlignment.Right) { a0 = -1; a1 = 0; }
    else if (tn.HorizontalAlignment == HorizontalTextAlignment.Center) { a0 = -0.5; a1 = 0.5; }
    if (tn.VerticalAlignment == VerticalTextAlignment.Bottom) { b0 = 0; b1 = 1; }
    else if (tn.VerticalAlignment == VerticalTextAlignment.Middle) { b0 = -0.5; b1 = 0.5; }
    double[] r = new double[] { double.MaxValue, double.MaxValue, double.MinValue, double.MinValue };
    double[] aa = new double[] { a0, a1 }, bb = new double[] { b0, b1 };
    foreach (double ka in aa) foreach (double kb in bb)
    {
        double x = c.X + b.X * ka * w + u.X * kb * h, y = c.Y + b.Y * ka * w + u.Y * kb * h;
        r = T4U(r, x, y);
    }
    return r;
}
static double[] T4CloudBox(RevisionCloud rc)
{
    double[] r = null;
    foreach (Curve cv in rc.GetSketchCurves())
        for (int k = 0; k < 2; k++)
        {
            XYZ p = cv.GetEndPoint(k);
            r = (r == null) ? new double[] { p.X, p.Y, p.X, p.Y } : T4U(r, p.X, p.Y);
        }
    return r;
}
static bool T4SegBox(double x0, double y0, double x1, double y1, double[] b)
{
    double t0 = 0, t1 = 1, dx = x1 - x0, dy = y1 - y0;
    double[] p = new double[] { -dx, dx, -dy, dy };
    double[] q = new double[] { x0 - b[0], b[2] - x0, y0 - b[1], b[3] - y0 };
    for (int i = 0; i < 4; i++)
    {
        if (Math.Abs(p[i]) < 1e-12) { if (q[i] < 0) return false; }
        else
        {
            double t = q[i] / p[i];
            if (p[i] < 0) { if (t > t1) return false; if (t > t0) t0 = t; }
            else { if (t < t0) return false; if (t < t1) t1 = t; }
        }
    }
    return true;
}
static string T4Corner(XYZ p, double[] r)
{
    bool l = Math.Abs(p.X - r[0]) < 0.01, rt = Math.Abs(p.X - r[2]) < 0.01, bo = Math.Abs(p.Y - r[1]) < 0.01, tp = Math.Abs(p.Y - r[3]) < 0.01;
    if (l && bo) return "BL"; if (l && tp) return "TL"; if (rt && tp) return "TR"; if (rt && bo) return "BR";
    return "(" + T4F(p.X) + "," + T4F(p.Y) + ")";
}
// collects tessellated points of visible geometry (model coords)
static void T4Pts(GeometryElement ge, List<XYZ> pts, int depth)
{
    if (ge == null || depth > 4 || pts.Count > 200000) return;
    foreach (GeometryObject go in ge)
    {
        if (go is GeometryInstance) T4Pts(((GeometryInstance)go).GetInstanceGeometry(), pts, depth + 1);
        else if (go is Solid) { foreach (Edge ed in ((Solid)go).Edges) pts.AddRange(ed.Tessellate()); }
        else if (go is Curve) pts.AddRange(((Curve)go).Tessellate());
        else if (go is PolyLine) pts.AddRange(((PolyLine)go).GetCoordinates());
    }
}
// layout for head (hx,hy) of a right/top aligned note W x H (model ft):
// [0..3] text box, [4..7] cloud rect (text + 1/4 in), [8,9] tag head, [10..13] predicted tag box, [14] nudge ft
static double[] T4Layout(double hx, double hy, double W, double H, int S, double[] tagOff)
{
    double IN = S / 12.0;
    double[] tb = new double[] { hx - W, hy - H, hx, hy };
    double[] rect = T4Grow(tb, 0.25 * IN);
    double thx = rect[2] - 0.17 * IN, thy = rect[3] + 0.08 * IN;
    double clr = 0.08 * IN, step = 0.02 * IN, nudge = 0;
    double bot = thy + tagOff[1];
    if (bot < rect[3] + clr) nudge = Math.Ceiling((rect[3] + clr - bot) / step) * step;
    thy += nudge;
    return new double[] { tb[0], tb[1], tb[2], tb[3], rect[0], rect[1], rect[2], rect[3], thx, thy,
        thx + tagOff[0], thy + tagOff[1], thx + tagOff[2], thy + tagOff[3], nudge };
}
static string T4Crop(double[] L, double[] crop, double spare)
{
    if (crop == null) return null;
    double[] c = T4Grow(crop, -spare);
    double[] all = new double[] { Math.Min(L[4], L[10]), Math.Min(L[5], L[11]), Math.Max(L[6], L[12]), Math.Max(L[7], L[13]) };
    if (T4Inside(all, c)) return null;
    List<string> w = new List<string>();
    if (all[0] < c[0]) w.Add("left by " + T4F(c[0] - all[0]));
    if (all[1] < c[1]) w.Add("bottom by " + T4F(c[1] - all[1]));
    if (all[2] > c[2]) w.Add("right by " + T4F(all[2] - c[2]));
    if (all[3] > c[3]) w.Add("top by " + T4F(all[3] - c[3]));
    return "outside crop-1ft " + string.Join(",", w);
}
// all clashes of a layout: text box, cloud (+0.05 in bulge), tag box vs obstacles; leader vs annotation; GI box; GSAN6
static List<string> T4Clash(double[] L, double ax, double ay, double ex, double ey, int S, List<double[]> obs, List<string> lbl, List<int> kind, List<double[]> segs, List<double> hws, double[] giBB, double[] keep)
{
    double IN = S / 12.0, g = 0.05 * IN;
    double[] tb = new double[] { L[0], L[1], L[2], L[3] };
    double[] cr = T4Grow(new double[] { L[4], L[5], L[6], L[7] }, 0.05 * IN);
    double[] tg = new double[] { L[10], L[11], L[12], L[13] };
    List<string> res = new List<string>();
    double[][] zones = new double[][] { tb, cr, tg };
    string[] zn = new string[] { "text", "cloud", "tag" };
    for (int i = 0; i < obs.Count; i++)
    {
        for (int z = 0; z < 3; z++)
        {
            double gap = (kind[i] == 3) ? 0 : ((z == 0) ? g : 0);
            if (!T4Ov(zones[z], obs[i], gap)) continue;
            if (kind[i] == 3 && segs[i] != null)
            {
                double[] s = segs[i];
                if (!T4SegBox(s[0], s[1], s[2], s[3], T4Grow(zones[z], hws[i]))) continue;
            }
            res.Add(zn[z] + " x " + lbl[i]);
            break;
        }
        if (kind[i] != 3 && T4SegBox(ax, ay, ex, ey, obs[i])) res.Add("leader x " + lbl[i]);
    }
    for (int z = 0; z < 3; z++)
        if (T4Ov(zones[z], giBB, 0)) res.Add(zn[z] + " x GI bbox");
    if (T4Ov(cr, keep, 0)) res.Add("cloud x GSAN6 open end");
    if (T4SegBox(ax, ay, ex, ey, T4Grow(tb, -0.2))) res.Add("leader x own text");
    return res;
}
// tag box in the view; falls back to head + measured offsets when the API box is missing or implausible
static double[] T4TagBox(IndependentTag tag, View view, double[] tagOff, double IN)
{
    XYZ h = tag.TagHeadPosition;
    BoundingBoxXYZ bb = tag.get_BoundingBox(view);
    if (bb != null)
    {
        double w = bb.Max.X - bb.Min.X, hh = bb.Max.Y - bb.Min.Y;
        if (w > 0.05 * IN && w < 0.6 * IN && hh > 0.05 * IN && hh < 0.6 * IN) return new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
    }
    return new double[] { h.X + tagOff[0], h.Y + tagOff[1], h.X + tagOff[2], h.Y + tagOff[3] };
}
public static object DummyTail() { return null;
