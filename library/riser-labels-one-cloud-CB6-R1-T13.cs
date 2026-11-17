// riser-labels-one-cloud: from CB6-R1-T13.v3 (23-073), verified
// CB6-R1-T13 v3 one riser cloud
// CB6-R1-T13 | operation: add-text-labels-riser (+ hex Note Number edits, riser total edit, one riser revision cloud replacing 2) | run 20261109-1412 | Revit 2025 | v3
// Body of: public static object Execute(Document document, object[] parameters)
// send_code_to_revit, transactionMode "auto" (one outer transaction; ALL work runs in ONE SubTransaction so the script
// rolls back its own changes and still returns "ROLLED_BACK ..."). Never saves or syncs.
// Grease riser drafting view 14662318 (P6.01, 1:48): one unwrapped TextNote 'F-xx' at each of 21 K1.1 HEX keynotes.
// v3 (fix mode, JEREMY DECISION 2026-11-09):
//  - the two FEWD branch clouds 15034577 (PREP KITCHEN, tag 15034578) and 15034597 (BAR/LOUNGE, tag 15034598) are
//    replaced by ONE clockwise rectangular cloud on 'FEWD Submittal' around the whole grease riser content: every hex
//    keynote (generic annotation with 'Note Number') in the view, the FS elements, FS/FD labels (text notes / tags whose
//    text starts FS or FD), everything the two old clouds enclosed, the riser total 14662429 and every F-# label and
//    leader. 1.0 ft (1/4 in paper) margin from the boxes measured after Regenerate; any text / tag / annotation the
//    line or the delta would touch is enclosed (cloud grown), never cut. One MEP_Tag_Revision : Revision Tag at R25.
//    Cloud <= 30 in paper per side. NO_GO if it would touch a cloud other than the two replaced ones, or leave the
//    view crop (crop active; with no crop the viewport growth is reported).
//  - labels are planned first with NO cloud constraint (old clouds and their tags are ignored as obstacles): beside the
//    hex (v1 rule) where clear, else 0.5..4.0 ft away (16 directions + N/S both sides) with one straight leader to the
//    hex edge; no overlap with text / tags / annotations / labels / leaders; leaders never cross or touch each other.
//  - hex renumbers 224->244 (14697991), 05->25 (hex at FS 14680894), 65->64 (hex at FS 14680966) and riser total
//    '000 DFU' -> '56 DFU' (built from the current string) are applied whether or not their label is placed.
//  - order inside the SubTransaction: edits, labels (measured), cloud plan from everything, delete old tags + clouds,
//    create the new cloud + tag, post-checks, read-back (R30); any failure rolls back everything.
//  - parameters: ["dryrun"] read-only preflight + full plan; [] real run, all or nothing (NO_GO if any label has no
//    clear spot); ["partial"] real run placing every label that fits and listing the rest on 'unplaced:' (the cloud
//    still goes around everything placed).
// Cleanup: CB6-R1-T13.v3.cleanup.cs (the deleted old clouds/tags can only come back from the SYNC A ACC version).
// No inch-mark literal: (char)34.
var inv = System.Globalization.CultureInfo.InvariantCulture;
bool dry = false, partial = false;
if (parameters != null)
    foreach (object po in parameters)
    {
        if (po == null) continue;
        foreach (string part in po.ToString().Split(','))
        {
            string a0 = part.Trim().Trim('"', '[', ']', ' ').ToLowerInvariant();
            if (a0 == "dryrun") dry = true; else if (a0 == "partial") partial = true;
        }
    }
string QT = ((char)34).ToString();
string REVDESC = "FEWD Submittal";
long VIEWID = 14662318, SHEETID = 14662586, VPID = 14662592;
long DFUID = 14662429; string DFU_OLD = "000", DFU_NEW = "56", DFU_KEY = " DFU";
string TYPENAME = "3/32" + QT + " Arial Narrow"; long TYPE_HINT = 14085252;
long[] OLDCL = new long[] { 15034577, 15034597 };
long[] OLDTG = new long[] { 15034578, 15034598 };
double GAPX = 0.15, MINCLR = 1.0, MAXSIDE_IN = 30.0;
int RETMAX = 3100;
long[] HEX = new long[] { 14680912, 14680785, 14680884, 14680903, 14680844, 14680842, 14680843, 14680845, 14680855, 14680846, 14680817, 14680812, 14680797, 14680867, 14698002, 14698004, 14698010, 14698171, 14697991, 0, 0 };
long[] FSID = new long[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14697992, 14680894, 14680966 };
string[] NOLD = new string[] { "01", "23", "23", "23", "83", "84", "87", "89", "90", "92", "97", "98", "105", "115", "224", "226", "227", "230", "224", "05", "65" };
string[] NNEW = new string[] { "01", "23", "23", "23", "83", "84", "87", "89", "90", "92", "97", "98", "105", "115", "224", "226", "227", "230", "244", "25", "64" };
int[] FN = new int[] { 1, 7, 7, 7, 14, 15, 16, 17, 18, 19, 21, 22, 25, 27, 31, 32, 33, 34, 39, 8, 10 };
string SIDES = "RLLRRLRLRRRRRRLRRRRRR";
double NaN_ = double.NaN;
double[] PX = new double[] { 36.73, -8.41, 24.41, 32.97, 12.06, 11.47, 12.66, 11.47, 15.12, 12.66, 2.03, 2.77, -3.20, 20.02, 2.16, 3.35, -1.18, -5.20, NaN_, NaN_, NaN_ };
double[] PY = new double[] { 50.63, 50.69, 50.66, 50.66, 50.99, 49.94, 49.94, 48.89, 53.16, 48.89, 53.12, 48.89, 48.89, 48.89, -15.22, -15.22, -13.44, -15.28, NaN_, NaN_, NaN_ };
int N = HEX.Length;
if (FSID.Length != N || NOLD.Length != N || NNEW.Length != N || FN.Length != N || SIDES.Length != N || PX.Length != N || PY.Length != N) return CB6NoGo("script table lengths differ");
string[] LBLT = new string[N];
for (int i = 0; i < N; i++) LBLT[i] = "F-" + FN[i].ToString(inv);
List<string> warn = new List<string>();
System.Text.StringBuilder sb = new System.Text.StringBuilder();
sb.Append(dry ? "DRYRUN CB6-R1-T13 v3" : (partial ? "RUN-PARTIAL CB6-R1-T13 v3" : "RUN CB6-R1-T13 v3"));

// ---- 0. document
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return CB6NoGo("wrong document open: " + title);
sb.Append(" modifiable=").Append(document.IsModifiable ? "yes" : "no");

// ---- 1. view, viewport, sheet, live constants
View view = document.GetElement(new ElementId(VIEWID)) as View;
if (view == null || !(view is ViewDrafting)) return CB6NoGo("view " + VIEWID + " missing or not a drafting view");
int scale = view.Scale;
if (scale != 48) warn.Add("view scale 1:" + scale + " (task says 1:48)");
double PI_ = scale / 12.0;
double margin = MINCLR + 0.05;
double dxTag = 0.17 * PI_, dyTag = 0.08 * PI_;
double LDMIN = 0.125 * PI_, LDSTEP = 0.0625 * PI_, LDMAX = 1.0 * PI_, LCLR = 0.02 * PI_;
double spIn = 0.125, clrIn = 0.10;
try
{
    RevisionSettings rset = RevisionSettings.GetRevisionSettings(document);
    spIn = rset.RevisionCloudSpacing * 12.0;
    clrIn = Math.Max(0.10, Math.Min(0.25, 0.4 * spIn));
}
catch (Exception) { warn.Add("RevisionCloudSpacing unreadable; 0.125 in assumed"); }
double clr = clrIn * PI_;
double gapReq = Math.Max(0.06, 0.5 * spIn + 0.03) * PI_;
double freeGap = margin + 2 * clr;
double maxSide = MAXSIDE_IN * PI_;
Viewport vp = document.GetElement(new ElementId(VPID)) as Viewport;
if (vp == null || vp.ViewId.Value != VIEWID || vp.SheetId.Value != SHEETID) return CB6NoGo("viewport " + VPID + " does not place view " + VIEWID + " on sheet " + SHEETID);
ViewSheet sheet = document.GetElement(vp.SheetId) as ViewSheet;
string sheetNo = sheet != null ? sheet.SheetNumber : "?";
if (sheetNo != "P6.01") warn.Add("sheet number is " + sheetNo);
if (vp.Rotation != ViewportRotation.None) warn.Add("viewport rotated " + vp.Rotation.ToString());
BuiltInCategory[] needCats = new BuiltInCategory[] { BuiltInCategory.OST_TextNotes, BuiltInCategory.OST_RevisionClouds, BuiltInCategory.OST_RevisionCloudTags };
foreach (BuiltInCategory bc in needCats)
    if (view.GetCategoryHidden(new ElementId(bc))) return CB6NoGo("category " + bc.ToString() + " is hidden in view " + VIEWID);
sb.Append(" view=").Append(VIEWID).Append(" 1:").Append(scale).Append(" sheet=").Append(sheetNo)
  .Append(" spacing_in=").Append(Math.Round(spIn, 3).ToString(inv));

// ---- 2. revision by Description
Revision rev = null; int nrev = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REVDESC) { nrev++; if (rev == null) rev = r; }
}
if (nrev != 1) return CB6NoGo(nrev + " revisions have Description 'FEWD Submittal' (need exactly 1)");
if (rev.Issued) return CB6NoGo("revision 'FEWD Submittal' (id " + rev.Id.Value + ") is issued");
if (rev.Visibility == RevisionVisibility.Hidden) return CB6NoGo("revision 'FEWD Submittal' Visibility=Hidden");
sb.Append(" rev=").Append(rev.Id.Value);
if (rev.Id.Value != 15034302) warn.Add("FEWD revision id is " + rev.Id.Value + " (task says 15034302)");

// ---- 3. Revision Tag type (R31: family MEP_Tag_Revision, type 'Revision Tag')
FamilySymbol tagSym = null; int tagRank = 9; List<string> tagNames = new List<string>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol; if (fs == null) continue;
    tagNames.Add(fs.FamilyName + ":" + fs.Name);
    int rk = (fs.FamilyName == "MEP_Tag_Revision" && fs.Name == "Revision Tag") ? 0 : ((fs.FamilyName == "Revision Tag" || fs.Name == "Revision Tag") ? 1 : 9);
    if (rk < tagRank || (rk == tagRank && rk < 9 && fs.Id.Value < tagSym.Id.Value)) { tagSym = fs; tagRank = rk; }
}
if (tagSym == null || tagRank > 0) return CB6NoGo("no MEP_Tag_Revision:'Revision Tag' in Revision Cloud Tags; found: " + string.Join(", ", tagNames.Take(5)));
sb.Append(" tag_type=").Append(tagSym.Id.Value).Append("'").Append(tagSym.FamilyName).Append(":").Append(tagSym.Name).Append("'");

// ---- 4. text type: the task's 3/32 Arial Narrow, and it must already be used by text notes in this view
Dictionary<long, int> tnUse = new Dictionary<long, int>();
List<TextNote> viewNotes = new List<TextNote>();
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(TextNote)))
{
    TextNote t = e as TextNote; if (t == null) continue;
    viewNotes.Add(t); long k = t.GetTypeId().Value;
    tnUse[k] = T13Use(tnUse, k) + 1;
}
TextNoteType tnt = null; string tntWhy = "";
List<TextNoteType> allT = new List<TextNoteType>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(TextNoteType))) { TextNoteType t = e as TextNoteType; if (t != null) allT.Add(t); }
foreach (TextNoteType t in allT)
    if (t.Name == TYPENAME && T13Use(tnUse, t.Id.Value) > 0 && (tnt == null || T13Use(tnUse, t.Id.Value) > T13Use(tnUse, tnt.Id.Value))) { tnt = t; tntWhy = "exact name"; }
if (tnt == null)
    foreach (TextNoteType t in allT)
    {
        string nm = (t.Name ?? "").ToUpperInvariant();
        if (nm.StartsWith("3/32") && nm.IndexOf("ARIAL NARROW") >= 0 && T13Use(tnUse, t.Id.Value) > 0 && (tnt == null || T13Use(tnUse, t.Id.Value) > T13Use(tnUse, tnt.Id.Value))) { tnt = t; tntWhy = "3/32 Arial Narrow variant"; }
    }
if (tnt == null)
{
    List<string> used = new List<string>();
    foreach (TextNoteType t in allT) if (T13Use(tnUse, t.Id.Value) > 0) used.Add(t.Id.Value + "'" + t.Name + "'x" + T13Use(tnUse, t.Id.Value));
    return CB6NoGo("no text type named 3/32 Arial Narrow is used by a text note in view " + VIEWID + "; used: " + CB6Cap(string.Join(", ", used), 600));
}
if (tnt.Id.Value != TYPE_HINT) warn.Add("text type id " + tnt.Id.Value + " is not the task's " + TYPE_HINT);
double tSize = 0.09375 / 12.0, tWf = 1.0;
Parameter pSz = tnt.get_Parameter(BuiltInParameter.TEXT_SIZE); if (pSz != null && pSz.AsDouble() > 0) tSize = pSz.AsDouble();
Parameter pWf = tnt.get_Parameter(BuiltInParameter.TEXT_WIDTH_SCALE); if (pWf != null && pWf.AsDouble() > 0) tWf = pWf.AsDouble();
double estH = tSize * 1.7 * scale;
sb.Append("\ntext_type=").Append(tnt.Id.Value).Append(" ").Append(CB6Show(tnt.Name)).Append(" (").Append(tntWhy).Append(", used_in_view=").Append(T13Use(tnUse, tnt.Id.Value))
  .Append(") size_in=").Append(Math.Round(tSize * 12, 4).ToString(inv)).Append(" wf=").Append(Math.Round(tWf, 2).ToString(inv));

// ---- 5. regenerate before measuring (R30)
if (document.IsModifiable)
{
    try { document.Regenerate(); }
    catch (Exception rx) { warn.Add("Regenerate failed: " + CB6Cap(rx.Message, 100)); }
}
else warn.Add("document not modifiable: measured without Regenerate");

// ---- 6. hexes: resolve, re-read Note Number, box and point
List<FamilyInstance> hexCands = new List<FamilyInstance>();
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(FamilyInstance)))
{
    FamilyInstance fi = e as FamilyInstance;
    if (fi == null || fi.Category == null || fi.Category.BuiltInCategory != BuiltInCategory.OST_GenericAnnotation) continue;
    if (T13NumParam(fi) != null) hexCands.Add(fi);
}
string[] numSt = new string[N]; long[] hid = new long[N]; CB6Box[] hb = new CB6Box[N]; double[] hxp = new double[N], hyp = new double[N];
string[] cur = new string[N]; string[] info = new string[N]; bool[] ok = new bool[N];
HashSet<long> listed = new HashSet<long>(); foreach (long h0 in HEX) if (h0 > 0) listed.Add(h0);
List<string> invalid = new List<string>(); List<string> pRep = new List<string>();
double zPl = 0; bool zSet = false;
for (int i = 0; i < N; i++)
{
    info[i] = ""; cur[i] = ""; numSt[i] = "invalid";
    FamilyInstance h = null;
    if (HEX[i] > 0)
    {
        h = document.GetElement(new ElementId(HEX[i])) as FamilyInstance;
        if (h == null) { invalid.Add("hex " + HEX[i] + " not found or not a family instance"); continue; }
    }
    else
    {
        Element fs = document.GetElement(new ElementId(FSID[i]));
        if (fs == null) { invalid.Add("FS " + FSID[i] + " not found"); continue; }
        BoundingBoxXYZ fb = fs.get_BoundingBox(view);
        if (fb == null) { invalid.Add("FS " + FSID[i] + " has no box in view"); continue; }
        if (fs.OwnerViewId.Value != VIEWID) warn.Add("FS " + FSID[i] + " owner view " + fs.OwnerViewId.Value);
        double fx = 0.5 * (fb.Min.X + fb.Max.X), fy = 0.5 * (fb.Min.Y + fb.Max.Y);
        double d1 = 1e9, d2 = 1e9; FamilyInstance best = null;
        foreach (FamilyInstance c in hexCands)
        {
            if (listed.Contains(c.Id.Value)) continue;
            string v = T13Val(T13NumParam(c));
            if (!T13Eq(v, NOLD[i]) && !T13Eq(v, NNEW[i])) continue;
            XYZ cp = T13Pt(c, view); if (cp == null) continue;
            double d = Math.Sqrt((cp.X - fx) * (cp.X - fx) + (cp.Y - fy) * (cp.Y - fy));
            if (d < d1) { d2 = d1; d1 = d; best = c; } else if (d < d2) d2 = d;
        }
        if (best == null || d1 > 10.0) { invalid.Add("no hex #" + NOLD[i] + "/#" + NNEW[i] + " within 10 ft of FS " + FSID[i] + (best != null ? " (nearest " + T13F(d1) + ")" : "")); continue; }
        h = best; listed.Add(h.Id.Value);
        info[i] = "@FS" + FSID[i] + " d=" + T13F(d1) + (d2 < 1e8 ? " next=" + T13F(d2) : "");
        if (d2 < d1 + 1.0) warn.Add("hex for FS " + FSID[i] + " ambiguous: nearest " + T13F(d1) + " next " + T13F(d2));
    }
    if (h.OwnerViewId.Value != VIEWID) { invalid.Add("hex " + h.Id.Value + " owner view " + h.OwnerViewId.Value); continue; }
    if (h.Category == null || h.Category.BuiltInCategory != BuiltInCategory.OST_GenericAnnotation) { invalid.Add("hex " + h.Id.Value + " not a generic annotation"); continue; }
    hid[i] = h.Id.Value;
    Parameter p = T13NumParam(h);
    if (p == null)
    {
        bool onType = h.Symbol != null && h.Symbol.LookupParameter("Note Number") != null;
        invalid.Add("hex " + hid[i] + " has no instance text/integer 'Note Number'" + (onType ? " (it is a TYPE parameter)" : "")); continue;
    }
    cur[i] = T13Val(p);
    bool isEdit = NOLD[i] != NNEW[i];
    if (T13Eq(cur[i], NOLD[i])) numSt[i] = isEdit ? "edit" : "ok";
    else if (isEdit && T13Eq(cur[i], NNEW[i])) numSt[i] = "already";
    else numSt[i] = "mismatch";
    if (numSt[i] == "edit" && p.IsReadOnly) { invalid.Add("hex " + hid[i] + " 'Note Number' is read-only"); numSt[i] = "invalid"; continue; }
    if (i == 0 || isEdit) pRep.Add(hid[i] + " " + CB6Show(cur[i]) + " in " + T13PRep(h, cur[i]));
    XYZ pt = T13Pt(h, view); BoundingBoxXYZ bb = h.get_BoundingBox(view);
    if (pt == null || bb == null) { invalid.Add("hex " + hid[i] + " has no point/box in view"); numSt[i] = "invalid"; continue; }
    hxp[i] = pt.X; hyp[i] = pt.Y; hb[i] = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, hid[i]);
    if (!zSet) { zPl = bb.Min.Z; zSet = true; }
    if (!double.IsNaN(PX[i]))
    {
        double dd = Math.Sqrt((pt.X - PX[i]) * (pt.X - PX[i]) + (pt.Y - PY[i]) * (pt.Y - PY[i]));
        if (dd > 0.15) warn.Add("hex " + hid[i] + " point (" + T13F(pt.X) + "," + T13F(pt.Y) + ") is " + T13F(dd) + " ft from the task point");
    }
    if (FSID[i] > 0 && HEX[i] > 0)
    {
        Element fs = document.GetElement(new ElementId(FSID[i])); BoundingBoxXYZ fb = fs != null ? fs.get_BoundingBox(view) : null;
        if (fb != null) info[i] = "@FS" + FSID[i] + " d=" + T13F(Math.Sqrt(Math.Pow(pt.X - 0.5 * (fb.Min.X + fb.Max.X), 2) + Math.Pow(pt.Y - 0.5 * (fb.Min.Y + fb.Max.Y), 2)));
        else warn.Add("FS " + FSID[i] + " not found / no box (hex " + hid[i] + " resolved by id)");
    }
    ok[i] = numSt[i] != "mismatch";
}
if (invalid.Count > 0) return CB6NoGo("hexes: " + CB6Cap(string.Join("; ", invalid), 1200));

// ---- 7. riser total text
Element dfuEl = document.GetElement(new ElementId(DFUID));
if (dfuEl == null) return CB6NoGo("riser total " + DFUID + " not found");
if (dfuEl.OwnerViewId.Value != VIEWID) return CB6NoGo("riser total " + DFUID + " owner view " + dfuEl.OwnerViewId.Value);
TextNote dtn = dfuEl as TextNote; string dfuKind, dfuCur = "", dfuParam = null; CB6Box dfuBox = null;
if (dtn != null) { dfuKind = "TextNote"; dfuCur = dtn.Text ?? ""; dfuBox = CB6TextBox(dtn, view, scale, document); }
else
{
    int nhit = 0;
    foreach (Parameter p in dfuEl.Parameters)
    {
        if (p.StorageType != StorageType.String || p.IsReadOnly) continue;
        string v = p.AsString() ?? "";
        if (T13Find(v, DFU_OLD + DFU_KEY) >= 0 || T13Find(v, DFU_NEW + DFU_KEY) >= 0) { nhit++; dfuParam = p.Definition.Name; dfuCur = v; }
    }
    if (nhit != 1) return CB6NoGo("riser total " + DFUID + " is a " + dfuEl.GetType().Name + " with " + nhit + " writable text parameters holding the DFU value");
    dfuKind = "param '" + dfuParam + "'";
    BoundingBoxXYZ db = dfuEl.get_BoundingBox(view); if (db != null) dfuBox = new CB6Box(db.Min.X, db.Min.Y, db.Max.X, db.Max.Y, DFUID);
}
if (dfuBox == null) return CB6NoGo("riser total " + DFUID + " has no box in view");
string dfuSt, dfuNewS;
if (T13Count(dfuCur, DFU_OLD + DFU_KEY) == 1) { int ix = T13Find(dfuCur, DFU_OLD + DFU_KEY); dfuSt = "edit"; dfuNewS = dfuCur.Substring(0, ix) + DFU_NEW + dfuCur.Substring(ix + DFU_OLD.Length); }
else if (T13Count(dfuCur, DFU_OLD + DFU_KEY) == 0 && T13Count(dfuCur, DFU_NEW + DFU_KEY) == 1) { dfuSt = "already"; dfuNewS = dfuCur; }
else return CB6NoGo("riser total " + DFUID + " text is " + CB6Show(dfuCur) + ": expected exactly one '000 DFU'");
if (dfuCur.IndexOf("GSAN", StringComparison.Ordinal) < 0) warn.Add("riser total text has no 'GSAN'");
sb.Append("\ndfu ").Append(DFUID).Append(" ").Append(dfuKind).Append(" ").Append(dfuSt).Append(" ").Append(CB6Show(dfuCur.TrimEnd('\r', '\n')))
  .Append(" -> '").Append(dfuNewS.TrimEnd('\r', '\n').Replace("\r", "|")).Append("'");

// ---- 8. old clouds and their tags (to be replaced)
HashSet<long> oldCl = new HashSet<long>(OLDCL);
List<long> oldClLive = new List<long>(); List<string> oldGone = new List<string>();
List<List<double[]>> oldSegs = new List<List<double[]>>();
foreach (long oc in OLDCL)
{
    Element e = document.GetElement(new ElementId(oc));
    if (e == null) { oldGone.Add("cloud:" + oc); continue; }
    RevisionCloud rc = e as RevisionCloud;
    if (rc == null || rc.OwnerViewId.Value != VIEWID || rc.RevisionId.Value != rev.Id.Value) return CB6NoGo("old cloud " + oc + " is not a FEWD revision cloud in view " + VIEWID);
    oldClLive.Add(oc); oldSegs.Add(T5SegsRc(rc));
}
HashSet<long> delTg = new HashSet<long>(); int nExtraTg = 0;
foreach (long ot in OLDTG)
{
    Element e = document.GetElement(new ElementId(ot));
    if (e == null) { oldGone.Add("tag:" + ot); continue; }
    IndependentTag tg = e as IndependentTag;
    if (tg == null || tg.Category == null || tg.Category.BuiltInCategory != BuiltInCategory.OST_RevisionCloudTags || tg.OwnerViewId.Value != VIEWID) return CB6NoGo("old tag " + ot + " is not a revision cloud tag in view " + VIEWID);
    bool onOld = false;
    foreach (ElementId x in tg.GetTaggedLocalElementIds()) if (oldCl.Contains(x.Value)) onOld = true;
    if (!onOld) return CB6NoGo("old tag " + ot + " does not tag cloud 15034577 or 15034597");
    delTg.Add(ot);
}
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(IndependentTag)))
{
    IndependentTag tg = e as IndependentTag;
    if (tg == null || delTg.Contains(tg.Id.Value) || tg.Category == null || tg.Category.BuiltInCategory != BuiltInCategory.OST_RevisionCloudTags) continue;
    foreach (ElementId x in tg.GetTaggedLocalElementIds()) if (oldCl.Contains(x.Value)) { delTg.Add(tg.Id.Value); nExtraTg++; break; }
}

// ---- 9. worksharing ownership
if (document.IsWorkshared)
{
    List<long> chk = new List<long>(); chk.Add(VIEWID); chk.Add(DFUID);
    for (int i = 0; i < N; i++) if (hid[i] > 0) chk.Add(hid[i]);
    chk.AddRange(oldClLive); chk.AddRange(delTg);
    List<string> owned = new List<string>();
    foreach (long id in chk)
        if (WorksharingUtils.GetCheckoutStatus(document, new ElementId(id)) == CheckoutStatus.OwnedByOtherUser) owned.Add(id.ToString());
    if (owned.Count > 0) return CB6NoGo("owned by another user: " + string.Join(",", owned));
}

// ---- 10. labels already placed (idempotency): beside (v1 rule) or with a leader ending at this hex
long[] exL = new long[N]; CB6Box[] lbox = new CB6Box[N]; char[] side = new char[N]; bool[] exLd = new bool[N];
HashSet<long> usedNotes = new HashSet<long>();
for (int i = 0; i < N; i++)
{
    if (!ok[i]) continue;
    foreach (TextNote t in viewNotes)
    {
        if (usedNotes.Contains(t.Id.Value) || (t.Text ?? "").Trim() != LBLT[i]) continue;
        if (t.LeaderCount > 0)
        {
            bool hitL = false;
            try { foreach (Leader lq in t.GetLeaders()) { XYZ eq = lq.End; if (eq != null && T13InB(eq.X, eq.Y, CB6Ex(hb[i], 0.2))) hitL = true; } }
            catch (Exception) { }
            if (!hitL) continue;
            string srcq;
            CB6Box bq = T13Meas(t, view, scale, T13EstW(LBLT[i], tSize, tWf, scale), estH, out srcq);
            exL[i] = t.Id.Value; lbox[i] = bq; usedNotes.Add(t.Id.Value); side[i] = 'D'; exLd[i] = true;
            break;
        }
        CB6Box b = CB6TextBox(t, view, scale, document);
        double cy = 0.5 * (b.y0 + b.y1), gx = Math.Max(0, Math.Max(b.x0 - hb[i].x1, hb[i].x0 - b.x1));
        if (Math.Abs(cy - hyp[i]) < 0.35 && gx < 0.6)
        {
            exL[i] = t.Id.Value; lbox[i] = b; usedNotes.Add(t.Id.Value);
            side[i] = 0.5 * (b.x0 + b.x1) > hxp[i] ? 'R' : 'L';
            break;
        }
    }
}

// ---- 11. other clouds, obstacles (old tags ignored), existing leaders, delta tag model
List<CB6Box> otherCl = new List<CB6Box>(); List<List<double[]>> otherSegs = new List<List<double[]>>(); List<bool> otherFewd = new List<bool>();
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(RevisionCloud)))
{
    RevisionCloud rc = e as RevisionCloud; if (rc == null || oldCl.Contains(rc.Id.Value)) continue;
    CB6Box b = CB6CloudBox(rc); if (b == null) continue;
    otherCl.Add(b); otherSegs.Add(T5SegsRc(rc)); otherFewd.Add(rc.RevisionId.Value == rev.Id.Value);
}
List<CB6Box> obs = new List<CB6Box>(); List<CB6Box> bigs = new List<CB6Box>(); HashSet<long> fsfd = new HashSet<long>();
List<double[]> exLead = new List<double[]>(); int nLdApprox = 0, nLdErr = 0;
foreach (Element e in new FilteredElementCollector(document, view.Id).WhereElementIsNotElementType())
{
    if (e is RevisionCloud || e.Category == null || delTg.Contains(e.Id.Value)) continue;
    if (e is TextNote || e is AnnotationSymbol || e is IndependentTag)
    {
        try { nLdApprox += T13Leads(document, view, e, exLead); } catch (Exception) { nLdErr++; }
    }
    CB6Box b = null;
    if (e is TextNote)
    {
        b = CB6TextBox((TextNote)e, view, scale, document);
        if (T13IsFsFd(((TextNote)e).Text)) fsfd.Add(e.Id.Value);
    }
    else if (e is IndependentTag || e.Category.BuiltInCategory == BuiltInCategory.OST_GenericAnnotation)
    {
        BoundingBoxXYZ bb = e.get_BoundingBox(view);
        if (bb != null) b = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, e.Id.Value);
        if (e is IndependentTag)
        {
            string tt = "";
            try { tt = ((IndependentTag)e).TagText; } catch (Exception) { }
            if (T13IsFsFd(tt)) fsfd.Add(e.Id.Value);
        }
    }
    if (b == null) continue;
    if (b.x1 - b.x0 > 10.0 * PI_ || b.y1 - b.y0 > 10.0 * PI_) { bigs.Add(b); continue; }
    obs.Add(b);
}
double tL = 0, tR = 0, tB = -1e9, tA = -1e9; int nTg = 0;
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(IndependentTag)))
{
    IndependentTag tg0 = e as IndependentTag;
    if (tg0 == null || tg0.Category == null || tg0.Category.BuiltInCategory != BuiltInCategory.OST_RevisionCloudTags) continue;
    XYZ hp = tg0.TagHeadPosition; BoundingBoxXYZ tbb = tg0.get_BoundingBox(view);
    if (hp == null || tbb == null) continue;
    double l = hp.X - tbb.Min.X, r = tbb.Max.X - hp.X, bl = hp.Y - tbb.Min.Y, ab = tbb.Max.Y - hp.Y;
    if (l + r > 0.01 && bl + ab > 0.01 && Math.Abs(l) < PI_ && Math.Abs(r) < PI_ && Math.Abs(bl) < PI_ && Math.Abs(ab) < PI_)
    { tL = Math.Max(tL, l); tR = Math.Max(tR, r); tB = Math.Max(tB, bl); tA = Math.Max(tA, ab); nTg++; }
}
string tagSrc = "measured(" + nTg + ")";
if (nTg == 0) { tL = 0.13 * PI_; tR = 0.13 * PI_; tB = 0.10 * PI_; tA = 0.10 * PI_; tagSrc = "default"; }
double dyEff = Math.Max(dyTag, gapReq + tB);
double[] TG = new double[] { dxTag, dyEff, tL, tR, tB, tA };
if (nLdErr > 0) warn.Add(nLdErr + " elements' leaders could not be read (not checked against new leaders)");

// ---- 12. idempotent cloud: old clouds gone and an existing FEWD cloud already holds all base content -> keep it
long alreadyCl = 0; List<double[]> alreadySegs = null;
int[] ccnt = new int[8]; HashSet<long> contentIds = new HashSet<long>();
bool[] okC = new bool[N];
if (oldClLive.Count == 0)
{
    List<CB6Box> base0 = T13Content(document, view, hexCands, FSID, DFUID, dfuBox, N, okC, lbox, exL, new List<double[]>(), exLead, obs, oldSegs, fsfd, contentIds, ccnt, 10.0 * PI_);
    for (int k = 0; k < otherCl.Count && alreadyCl == 0; k++)
    {
        if (!otherFewd[k]) continue;
        bool all = true;
        foreach (CB6Box b in base0) if (T5Clear(b, otherSegs[k]) < MINCLR - 0.02) { all = false; break; }
        if (all) { alreadyCl = otherCl[k].id; alreadySegs = otherSegs[k]; }
    }
}
List<CB6Box> hard = new List<CB6Box>(); List<double[]> allCS = new List<double[]>();
for (int k = 0; k < otherCl.Count; k++) { if (otherCl[k].id == alreadyCl) continue; hard.Add(otherCl[k]); allCS.AddRange(otherSegs[k]); }
sb.Append("\nold_clouds=").Append(string.Join("/", oldClLive)).Append(" old_tags=").Append(string.Join("/", delTg)).Append(nExtraTg > 0 ? " (extra " + nExtraTg + ")" : "")
  .Append(oldGone.Count > 0 ? " gone=" + string.Join("/", oldGone) : "")
  .Append(" other_clouds=").Append(hard.Count).Append(alreadyCl > 0 ? " riser_cloud_already=" + alreadyCl : "")
  .Append(" obs=").Append(obs.Count).Append(" big=").Append(bigs.Count).Append(" fsfd=").Append(fsfd.Count)
  .Append(" tagbox=").Append(tagSrc).Append(" head_dy=").Append(T13F(dyEff))
  .Append(" leaders=").Append(exLead.Count).Append(" approx=").Append(nLdApprox).Append(nLdErr > 0 ? " unread=" + nLdErr : "");

// ---- 13. plan labels (estimated boxes; no cloud constraint): pass 1 beside, pass 2 leader
List<CB6Box> placed = new List<CB6Box>(); List<string> noSide = new List<string>(); string[] sideWhy = new string[N];
string[] howP = new string[N]; double[][] ldSeg = new double[N][]; int[] ldDir = new int[N]; double[] ldD = new double[N];
bool[] holdP = new bool[N]; string[] holdWhy = new string[N];
List<double[]> newLead = new List<double[]>();
for (int i = 0; i < N; i++) { howP[i] = ""; holdWhy[i] = ""; sideWhy[i] = ""; }
for (int i = 0; i < N; i++) if (ok[i] && exL[i] > 0) { placed.Add(new CB6Box(lbox[i].x0, lbox[i].y0, lbox[i].x1, lbox[i].y1, hid[i])); howP[i] = exLd[i] ? "had-lead" : "had"; }
for (int i = 0; i < N; i++)
{
    if (!ok[i] || exL[i] > 0) continue;
    double ew = T13EstW(LBLT[i], tSize, tWf, scale);
    char first = SIDES[i], second = first == 'R' ? 'L' : 'R';
    char[] trySides = new char[] { first, second };
    foreach (char s in trySides)
    {
        CB6Box b = T13Cand(s, hb[i], hyp[i], ew, estH, GAPX, hid[i]);
        string why = T13Judge(b, hid[i], obs, placed, hard, freeGap);
        if (why == null) why = T13BoxLeads(b, exLead, newLead);
        if (why == null) { lbox[i] = b; side[i] = s; placed.Add(b); howP[i] = "side"; break; }
        sideWhy[i] += s + ":" + why + " ";
    }
}
for (int i = 0; i < N; i++)
{
    if (!ok[i] || exL[i] > 0 || lbox[i] != null) continue;
    double ew = T13EstW(LBLT[i], tSize, tWf, scale);
    string lwhy;
    List<T13LC> lcs = T13LeadSearch(hb[i], hid[i], hyp[i], ew, estH, SIDES[i], LDMIN, LDSTEP, LDMAX, obs, placed, exLead, newLead, allCS, hard, freeGap, LCLR, 1, out lwhy);
    if (lcs.Count > 0)
    {
        T13LC c0 = lcs[0];
        lbox[i] = c0.box; side[i] = 'D'; howP[i] = "lead"; ldSeg[i] = new double[] { c0.ax, c0.ay, c0.ex, c0.ey }; ldDir[i] = c0.dir; ldD[i] = c0.d;
        placed.Add(new CB6Box(c0.box.x0, c0.box.y0, c0.box.x1, c0.box.y1, hid[i]));
        newLead.Add(new double[] { c0.ax, c0.ay, c0.ex, c0.ey, hid[i] });
    }
    else
    {
        noSide.Add(LBLT[i] + "@" + hid[i] + " " + CB6Cap(sideWhy[i].Trim(), 90) + " lead:" + lwhy);
        holdP[i] = true; holdWhy[i] = "no clear spot (" + CB6Cap(sideWhy[i].Trim(), 80) + "; lead " + lwhy + ")";
    }
}
if (noSide.Count > 0 && !dry && !partial) return CB6NoGo("labels without a clear spot (run [\"partial\"] to place the rest): " + CB6Cap(string.Join("; ", noSide), 1400));

// ---- 14. riser cloud plan from everything (estimated label boxes)
for (int i = 0; i < N; i++) okC[i] = ok[i] && !holdP[i] && lbox[i] != null;
List<CB6Box> content = T13Content(document, view, hexCands, FSID, DFUID, dfuBox, N, okC, lbox, exL, newLead, exLead, obs, oldSegs, fsfd, contentIds, ccnt, 10.0 * PI_);
List<long> encl = new List<long>(); CB6Box R = null; string rerr = null, cropInfo = "";
double[] hd0 = null;
if (alreadyCl == 0)
{
    List<CB6Box> others = obs.Where(o => !contentIds.Contains(o.id)).ToList();
    rerr = T13Rect(content, others, hard, margin, clr, TG, maxSide, 80, encl, out R);
    if (rerr == null)
    {
        hd0 = CB6TagHead(new List<CB6Box> { R }, TG[0], TG[1]);
        CB6Box all0 = CB6Hull(new List<CB6Box> { CB6Ex(R, clr), T5TagBox(hd0, TG) }, 0);
        string ce = T13CropErr(view, vp, all0, zPl, out cropInfo);
        if (ce != null) rerr = ce;
        else if (cropInfo.StartsWith("crop inactive") || cropInfo.StartsWith("crop check failed")) warn.Add(cropInfo);
    }
}
else
{
    foreach (CB6Box b in content) if (T5Clear(b, alreadySegs) < MINCLR - 0.02) { rerr = "existing riser cloud " + alreadyCl + " does not hold item " + b.id + " with 1.0 ft"; break; }
}
int nBigCut = 0;
if (R != null) { List<double[]> rs0 = T13RectSegs(R); foreach (CB6Box b in bigs) if (T13CutErr(b, rs0, 0, 0) != null) nBigCut++; }
if (nBigCut > 0) warn.Add(nBigCut + " big boxes (> 10 in paper, not obstacles) meet the cloud line");

int nLblNew = 0, nLblEx = 0, nEdit = 0, nNumAlready = 0, nMis = 0, nLead = 0, nHeld = 0;
for (int i = 0; i < N; i++)
{
    if (numSt[i] == "mismatch") { nMis++; continue; }
    if (numSt[i] == "edit") nEdit++; else if (numSt[i] == "already") nNumAlready++;
    if (holdP[i]) { nHeld++; continue; }
    if (exL[i] > 0) nLblEx++; else { nLblNew++; if (howP[i] == "lead") nLead++; }
}
List<string> unplacedL = new List<string>();
for (int i = 0; i < N; i++)
    if (ok[i] && holdP[i]) unplacedL.Add(LBLT[i] + "@" + hid[i] + " " + CB6Cap(holdWhy[i], 150));
sb.Append("\nnote_number_param: ").Append(CB6Cap(string.Join(" | ", pRep), 260));
string planLine = alreadyCl > 0 ? "riser cloud " + alreadyCl + " already in place" + (rerr != null ? " BUT " + rerr : "")
    : (rerr != null ? "PLAN NO_GO: " + CB6Cap(rerr, 300)
       : "plan rect=(" + T13F(R.x0) + "," + T13F(R.y0) + "," + T13F(R.x1) + "," + T13F(R.y1) + ") paper_in=" + T13F((R.x1 - R.x0) / PI_) + "x" + T13F((R.y1 - R.y0) / PI_)
         + " content=hex" + ccnt[0] + "/fs" + ccnt[1] + "/fsfd" + ccnt[2] + "/oldcloud" + ccnt[3] + "/dfu" + ccnt[4] + "/lbl" + ccnt[5] + "/newlead" + ccnt[6] + "/oldlead" + ccnt[7]
         + " enclosed_other=" + encl.Count + (encl.Count > 0 ? "(" + string.Join("/", encl.Take(8)) + ")" : "")
         + " head=(" + T13F(hd0[0]) + "," + T13F(hd0[1]) + ") " + cropInfo);

// ---- 15. DRY RUN
if (dry)
{
    System.Text.StringBuilder items = new System.Text.StringBuilder();
    for (int i = 0; i < N; i++) items.Append(T13ItemLine(i, LBLT, hid, cur, NNEW, numSt, exL, side, lbox, info, howP, holdP, ldDir, ldD, ldSeg)).Append("\n");
    int skD = nLblEx + nNumAlready + nMis + (dfuSt == "already" ? 1 : 0) + (alreadyCl > 0 ? 1 : 0);
    string fullVerdict = noSide.Count > 0 ? "labels without a clear spot" : rerr;
    string dTail = (warn.Count > 0 ? "warn: " + CB6Cap(string.Join("; ", warn), 250) + "\n" : "")
        + "full_mode([]): " + (fullVerdict == null ? "GO" : "NO_GO " + CB6Cap(fullVerdict, 120)) + "\n"
        + "partial_mode([\"partial\"]): " + (rerr != null ? "NO_GO" : "GO") + " unplaced=" + unplacedL.Count
        + (unplacedL.Count > 0 ? ": " + CB6Cap(string.Join("; ", unplacedL), 800) : "") + "\n"
        + "done=0 skipped=" + skD + " failed=" + (fullVerdict != null ? 1 : 0)
        + " (dryrun partial: would_label=" + nLblNew + " of which leader=" + nLead + " would_hexnum=" + nEdit + " would_dfu=" + (dfuSt == "edit" ? 1 : 0)
        + " would_delete=" + (delTg.Count + oldClLive.Count) + " would_cloud=" + (alreadyCl == 0 && rerr == null ? 1 : 0) + " unplaced=" + unplacedL.Count + " mismatch=" + nMis + ")";
    string dPlan = CB6Cap(planLine, 600);
    string dHead = CB6Cap(sb.ToString(), 700);
    int dLeft = RETMAX - dTail.Length - dPlan.Length - dHead.Length - 3;
    string dItems = CB6Cap(items.ToString(), Math.Max(300, dLeft));
    return dHead + "\n" + dItems + "\n" + dPlan + "\n" + dTail;
}
if (rerr != null) return CB6NoGo("cloud plan: " + CB6Cap(rerr, 1200));

// ---- 16. RUN: all or nothing in one SubTransaction
string item = "start";
List<string> doneIds = new List<string>(), skipIds = new List<string>(), cleanup = new List<string>(), cloudLines = new List<string>(), deletedOld = new List<string>();
for (int i = 0; i < N; i++)
{
    if (numSt[i] == "mismatch") { skipIds.Add("hex:" + hid[i] + "(Note Number " + CB6Show(cur[i]) + " not " + NOLD[i] + ")"); continue; }
    if (numSt[i] == "already") skipIds.Add("hexnum:" + hid[i] + "(already " + cur[i] + ")");
    if (holdP[i]) continue;
    if (exL[i] > 0) skipIds.Add("label:" + exL[i] + "(already)");
}
if (dfuSt == "already") skipIds.Add("dfu:" + DFUID + "(already)");
foreach (string g in oldGone) skipIds.Add("old " + g + "(already gone)");
if (alreadyCl > 0) skipIds.Add("cloud:" + alreadyCl + "(already in target state)");
TextNote[] madeN = new TextNote[N];
bool[] isLd = new bool[N]; bool[] needLead = new bool[N];
List<double[]> newLeadR = new List<double[]>();
int nLblDone = 0;
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    // 16a. hex Note Number edits (all three, independent of the labels) + riser total text
    for (int i = 0; i < N; i++)
    {
        if (numSt[i] != "edit") continue;
        item = "hexnum " + hid[i];
        Parameter p = T13NumParam(document.GetElement(new ElementId(hid[i])));
        bool ret = T13SetNum(p, NNEW[i]);
        if (!ret) throw new Exception("Set returned false for Note Number " + NNEW[i]);
        doneIds.Add("hexnum:" + hid[i]); cleanup.Add("hexnum:" + hid[i] + ":" + cur[i] + ":" + NNEW[i]);
    }
    if (dfuSt == "edit")
    {
        item = "dfu " + DFUID;
        if (dtn != null)
        {
            FormattedText ft = dtn.GetFormattedText();
            string fp = ft.GetPlainText() ?? "";
            if (T13Count(fp, DFU_OLD + DFU_KEY) != 1) throw new Exception("formatted text " + CB6Show(fp) + " does not hold exactly one 000 DFU");
            ft.SetPlainText(new TextRange(T13Find(fp, DFU_OLD + DFU_KEY), DFU_OLD.Length), DFU_NEW);
            dtn.SetFormattedText(ft);
        }
        else
        {
            Parameter p = dfuEl.LookupParameter(dfuParam);
            if (p == null || !p.Set(dfuNewS)) throw new Exception("Set of '" + dfuParam + "' failed");
        }
        doneIds.Add("dfu:" + DFUID); cleanup.Add("dfu:" + DFUID + ":" + DFU_OLD + ":" + DFU_NEW);
    }
    item = "regenerate after edits";
    document.Regenerate();
    for (int i = 0; i < N; i++)
    {
        if (numSt[i] != "edit") continue;
        string rb = T13Val(T13NumParam(document.GetElement(new ElementId(hid[i]))));
        if (!T13Eq(rb, NNEW[i])) throw new Exception("hex " + hid[i] + " Note Number read back " + CB6Show(rb) + " want " + NNEW[i]);
    }
    if (dfuSt == "edit")
    {
        Element de = document.GetElement(new ElementId(DFUID));
        string rb = (de is TextNote) ? (((TextNote)de).Text ?? "") : (de.LookupParameter(dfuParam).AsString() ?? "");
        if (T13Norm(rb) != T13Norm(dfuNewS)) throw new Exception("riser total read back " + CB6Show(rb) + " want " + CB6Show(dfuNewS));
    }
    {
        Element de = document.GetElement(new ElementId(DFUID));
        if (de is TextNote) dfuBox = CB6TextBox((TextNote)de, view, scale, document);
        else { BoundingBoxXYZ db2 = de.get_BoundingBox(view); if (db2 != null) dfuBox = new CB6Box(db2.Min.X, db2.Min.Y, db2.Max.X, db2.Max.Y, DFUID); }
    }
    for (int i = 0; i < N; i++)
    {
        if (!ok[i]) continue;
        BoundingBoxXYZ bb = document.GetElement(new ElementId(hid[i])).get_BoundingBox(view);
        if (bb == null) throw new Exception("hex " + hid[i] + " has no box after regenerate");
        hb[i] = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, hid[i]);
    }

    // 16b. beside labels: create, measure after Regenerate, align, judge; other side once; then fall back to a leader
    List<CB6Box> placedR = new List<CB6Box>();
    for (int i = 0; i < N; i++) if (ok[i] && exL[i] > 0) placedR.Add(new CB6Box(lbox[i].x0, lbox[i].y0, lbox[i].x1, lbox[i].y1, hid[i]));
    for (int i = 0; i < N; i++)
    {
        if (!ok[i] || exL[i] > 0 || holdP[i] || howP[i] != "side") continue;
        item = "label " + LBLT[i] + " hex " + hid[i];
        double ew = T13EstW(LBLT[i], tSize, tWf, scale);
        char[] trySides = new char[] { side[i], side[i] == 'R' ? 'L' : 'R' };
        string whys = ""; bool placedOk = false;
        foreach (char s in trySides)
        {
            CB6Box mb; string src;
            TextNote t = T13Make(document, view, tnt.Id, LBLT[i], s, hb[i], hyp[i], GAPX, scale, ew, estH, zPl, out mb, out src);
            string al = T13AlignErr(mb, s, hb[i], hyp[i], GAPX);
            if (al != null) throw new Exception("alignment " + al + " (" + src + ")");
            if ((t.Text ?? "").Trim() != LBLT[i]) throw new Exception("text read back " + CB6Show(t.Text));
            if (t.LeaderCount != 0) throw new Exception("note has " + t.LeaderCount + " leaders");
            string why = T13Judge(mb, hid[i], obs, placedR, hard, freeGap);
            if (why == null) why = T13BoxLeads(mb, exLead, newLeadR);
            if (why == null)
            {
                madeN[i] = t; lbox[i] = mb; side[i] = s; placedR.Add(new CB6Box(mb.x0, mb.y0, mb.x1, mb.y1, hid[i])); placedOk = true;
                nLblDone++; cleanup.Add("note:" + t.Id.Value);
                if (s != SIDES[i]) warn.Add(LBLT[i] + "@" + hid[i] + " moved to side " + s + " (" + whys.Trim() + ")");
                break;
            }
            whys += s + ":" + why + " ";
            document.Delete(t.Id);
            document.Regenerate();
        }
        if (!placedOk) { needLead[i] = true; sideWhy[i] = whys; warn.Add(LBLT[i] + "@" + hid[i] + " measured box has no clear side, leader tried (" + CB6Cap(whys.Trim(), 80) + ")"); }
    }

    // 16c. leader labels: create once to measure, search with the measured size, add one straight leader, re-check
    for (int i = 0; i < N; i++)
    {
        if (!ok[i] || exL[i] > 0 || holdP[i] || madeN[i] != null) continue;
        if (howP[i] != "lead" && !needLead[i]) continue;
        item = "lead label " + LBLT[i] + " hex " + hid[i];
        double ew = T13EstW(LBLT[i], tSize, tWf, scale);
        CB6Box tgt0 = (howP[i] == "lead" && lbox[i] != null) ? lbox[i] : T13LBoxAny(SIDES[i] == 'L' ? 1 : 0, LDMIN, hb[i], hyp[i], ew, estH, SIDES[i], hid[i]);
        CB6Box mb; string src;
        TextNote t = T13MakeAt(document, view, tnt.Id, LBLT[i], tgt0, scale, ew, estH, zPl, out mb, out src);
        if ((t.Text ?? "").Trim() != LBLT[i]) throw new Exception("text read back " + CB6Show(t.Text));
        string lwhy;
        List<T13LC> lcs = T13LeadSearch(hb[i], hid[i], hyp[i], mb.x1 - mb.x0, mb.y1 - mb.y0, SIDES[i], LDMIN, LDSTEP, LDMAX, obs, placedR, exLead, newLeadR, allCS, hard, freeGap, LCLR, 16, out lwhy);
        string tw = ""; bool okL = false;
        foreach (T13LC lc in lcs)
        {
            CB6Box b = mb;
            T13MoveTo(document, t, view, scale, ew, estH, lc.box.x0, 0.5 * (lc.box.y0 + lc.box.y1), ref b, out src);
            mb = b;
            string jw = T13Judge(b, hid[i], obs, placedR, hard, freeGap);
            if (jw == null) jw = T13BoxLeads(b, exLead, newLeadR);
            double[] sgm = null;
            if (jw == null)
            {
                char lsq = b.x0 >= 0.5 * (hb[i].x0 + hb[i].x1) - 1e-6 ? 'L' : 'R';
                double[] en = T13LEnd(hb[i], lsq == 'L' ? b.x0 : b.x1, 0.5 * (b.y0 + b.y1));
                if (en == null) jw = "leader too short";
                else
                {
                    jw = T13AddLead(document, t, lsq, en[0], en[1], zPl, out sgm);
                    if (jw == null) jw = T13LeadOk(sgm[0], sgm[1], sgm[2], sgm[3], hid[i], hb[i], obs, placedR, exLead, newLeadR, allCS, LCLR);
                    if (jw != null && t.LeaderCount > 0) { t.RemoveLeaders(); document.Regenerate(); }
                }
            }
            if (jw == null)
            {
                madeN[i] = t; lbox[i] = b; side[i] = 'D'; isLd[i] = true; ldSeg[i] = sgm; ldDir[i] = lc.dir; ldD[i] = lc.d; howP[i] = "lead";
                placedR.Add(new CB6Box(b.x0, b.y0, b.x1, b.y1, hid[i]));
                newLeadR.Add(new double[] { sgm[0], sgm[1], sgm[2], sgm[3], hid[i] });
                nLblDone++; cleanup.Add("note:" + t.Id.Value); okL = true;
                break;
            }
            tw += T13DirName(lc.dir) + T13F(lc.d) + ":" + jw + " ";
        }
        if (!okL)
        {
            document.Delete(t.Id); document.Regenerate();
            string rsn = "measured: " + (lcs.Count == 0 ? lwhy : CB6Cap(tw.Trim(), 200));
            if (!partial) throw new Exception("NO_GO no clear spot for " + LBLT[i] + " at hex " + hid[i] + ": " + rsn);
            holdP[i] = true; holdWhy[i] = rsn; lbox[i] = null;
            unplacedL.Add(LBLT[i] + "@" + hid[i] + " " + CB6Cap(rsn, 150));
        }
    }

    // 16d. riser cloud plan from the measured boxes (after Regenerate)
    item = "cloud plan";
    document.Regenerate();
    for (int i = 0; i < N; i++) okC[i] = ok[i] && !holdP[i] && lbox[i] != null;
    content = T13Content(document, view, hexCands, FSID, DFUID, dfuBox, N, okC, lbox, exL, newLeadR, exLead, obs, oldSegs, fsfd, contentIds, ccnt, 10.0 * PI_);
    List<CB6Box> othersR = obs.Where(o => !contentIds.Contains(o.id)).ToList();
    if (alreadyCl == 0)
    {
        string re2 = T13Rect(content, othersR, hard, margin, clr, TG, maxSide, 80, encl, out R);
        if (re2 != null) throw new Exception("NO_GO " + re2);
        double[] hd1 = CB6TagHead(new List<CB6Box> { R }, TG[0], TG[1]);
        string ci1; string ce1 = T13CropErr(view, vp, CB6Hull(new List<CB6Box> { CB6Ex(R, clr), T5TagBox(hd1, TG) }, 0), zPl, out ci1);
        if (ce1 != null) throw new Exception("NO_GO " + ce1);
        cropInfo = ci1;

        // 16e. delete the old tags, then the old clouds
        foreach (long tgid in delTg)
        {
            item = "delete old tag " + tgid;
            if (document.GetElement(new ElementId(tgid)) == null) { skipIds.Add("old tag:" + tgid + "(already gone)"); continue; }
            document.Delete(new ElementId(tgid));
            if (document.GetElement(new ElementId(tgid)) != null) throw new Exception("still exists after Delete");
            doneIds.Add("del:" + tgid); deletedOld.Add("tag:" + tgid);
        }
        foreach (long oc in oldClLive)
        {
            item = "delete old cloud " + oc;
            if (document.GetElement(new ElementId(oc)) == null) { skipIds.Add("old cloud:" + oc + "(already gone)"); continue; }
            document.Delete(new ElementId(oc));
            if (document.GetElement(new ElementId(oc)) != null) throw new Exception("still exists after Delete");
            doneIds.Add("del:" + oc); deletedOld.Add("cloud:" + oc);
        }
        document.Regenerate();

        // 16f. the new cloud (clockwise rectangle BL -> TL -> TR -> BR) + its delta tag
        item = "riser cloud";
        List<double[]> loop = new List<double[]> { new double[] { R.x0, R.y0 }, new double[] { R.x0, R.y1 }, new double[] { R.x1, R.y1 }, new double[] { R.x1, R.y0 } };
        double area = CB6Area(loop);
        if (area >= 0) throw new Exception("loop not clockwise (area " + area + ")");
        List<Curve> curves = new List<Curve>();
        for (int k = 0; k < loop.Count; k++)
        {
            double[] a = loop[k], b2 = loop[(k + 1) % loop.Count];
            curves.Add(Line.CreateBound(new XYZ(a[0], a[1], zPl), new XYZ(b2[0], b2[1], zPl)));
        }
        RevisionCloud rcN = RevisionCloud.Create(document, view, rev.Id, curves);
        doneIds.Add("cloud:" + rcN.Id.Value); cleanup.Add("cloud:" + rcN.Id.Value);
        item = "riser cloud tag";
        if (!tagSym.IsActive) tagSym.Activate();
        IndependentTag tgN = IndependentTag.Create(document, tagSym.Id, view.Id, new Reference(rcN), false, TagOrientation.Horizontal, new XYZ(hd1[0], hd1[1], zPl));
        cleanup.Insert(cleanup.Count - 1, "tag:" + tgN.Id.Value);
        document.Regenerate();

        // 16g. post-checks
        item = "post-check cloud " + rcN.Id.Value;
        if (rcN.RevisionId.Value != rev.Id.Value || rcN.OwnerViewId.Value != VIEWID) throw new Exception("cloud revision/view read back wrong");
        double sk = CB6SketchArea(rcN);
        if (sk >= 0) throw new Exception("sketch area " + sk + " not clockwise");
        List<double[]> sg = T5SegsRc(rcN);
        double mn = double.MaxValue; long mnId = 0;
        foreach (CB6Box b in content)
        {
            double d = T5Clear(b, sg);
            if (d < MINCLR - 0.02) throw new Exception("content box " + b.id + " clearance " + Math.Round(d, 3) + " ft < " + MINCLR);
            if (d < mn) { mn = d; mnId = b.id; }
        }
        foreach (CB6Box o in othersR)
        {
            string ce = T13CutErr(o, sg, MINCLR - 0.02, clr - 0.02);
            if (ce != null) throw new Exception("cloud line vs " + o.id + ": " + ce);
        }
        item = "post-check tag " + tgN.Id.Value;
        bool tagOk = false;
        foreach (ElementId x in tgN.GetTaggedLocalElementIds()) if (x.Value == rcN.Id.Value) tagOk = true;
        if (!tagOk) throw new Exception("tag does not reference the new cloud");
        double top = R.y1, moved = 0;
        for (int pass = 0; pass < 2; pass++)
        {
            BoundingBoxXYZ tb = tgN.get_BoundingBox(view);
            if (tb == null) throw new Exception("tag has no bbox");
            CB6Box tbx = new CB6Box(tb.Min.X, tb.Min.Y, tb.Max.X, tb.Max.Y, tgN.Id.Value);
            double gap = T5BoxSegs(tbx, sg);
            bool inside = T5In(0.5 * (tbx.x0 + tbx.x1), 0.5 * (tbx.y0 + tbx.y1), sg);
            if (!inside && gap >= gapReq - 0.005 && tbx.y0 >= top + gapReq - 0.005) break;
            if (pass == 1) throw new Exception("tag box still within " + Math.Round(gap, 3) + " ft of cloud (need " + Math.Round(gapReq, 3) + ")");
            double up = (top + gapReq + 0.02) - tbx.y0;
            if (up <= 0) up = gapReq - gap + 0.02;
            if (up > 0.5 * PI_) throw new Exception("tag would need to move up " + Math.Round(up, 2) + " ft (> 0.5 in paper)");
            XYZ hp0 = tgN.TagHeadPosition;
            tgN.TagHeadPosition = new XYZ(hp0.X, hp0.Y + up, hp0.Z);
            moved += up;
            document.Regenerate();
        }
        BoundingBoxXYZ tbF = tgN.get_BoundingBox(view);
        CB6Box tf = new CB6Box(tbF.Min.X, tbF.Min.Y, tbF.Max.X, tbF.Max.Y, tgN.Id.Value);
        foreach (CB6Box h2 in hard) if (CB6Ov(CB6Ex(tf, clr), h2)) throw new Exception("tag touches other cloud " + h2.id);
        foreach (CB6Box o in othersR) if (CB6Ov(tf, o)) throw new Exception("tag box overlaps " + o.id);
        foreach (CB6Box lb2 in placedR) if (CB6Ov(tf, lb2)) throw new Exception("tag box overlaps new label at hex " + lb2.id);
        foreach (double[] s9 in newLeadR) if (T13SegBox(s9[0], s9[1], s9[2], s9[3], tf)) throw new Exception("tag box crosses new leader of hex " + (long)s9[4]);
        foreach (double[] s9 in exLead) if (T13SegBox(s9[0], s9[1], s9[2], s9[3], tf)) throw new Exception("tag box crosses leader of " + (long)s9[4]);
        string ci2; string ce2 = T13CropErr(view, vp, CB6Hull(new List<CB6Box> { CB6Ex(R, clr), tf }, 0), zPl, out ci2);
        if (ce2 != null) throw new Exception("NO_GO " + ce2);
        XYZ hpF = tgN.TagHeadPosition;
        cloudLines.Add("cloud " + rcN.Id.Value + " tag " + tgN.Id.Value + " rect=(" + T13F(R.x0) + "," + T13F(R.y0) + "," + T13F(R.x1) + "," + T13F(R.y1) + ")"
            + " paper_in=" + T13F((R.x1 - R.x0) / PI_) + "x" + T13F((R.y1 - R.y0) / PI_) + " area=" + Math.Round(sk, 2).ToString(inv)
            + " min_clr=" + T13F(mn) + "(" + mnId + ") head=(" + T13F(hpF.X) + "," + T13F(hpF.Y) + ") head_dx=" + T13F(R.x1 - hpF.X)
            + " tag_gap=" + T13F(tf.y0 - top) + " up=" + T13F(moved)
            + " content=hex" + ccnt[0] + "/fs" + ccnt[1] + "/fsfd" + ccnt[2] + "/oldcloud" + ccnt[3] + "/dfu" + ccnt[4] + "/lbl" + ccnt[5] + "/lead" + (ccnt[6] + ccnt[7])
            + " enclosed_other=" + encl.Count + (encl.Count > 0 ? "(" + string.Join("/", encl.Take(8)) + ")" : "") + " " + ci2);
    }
    else
    {
        item = "post-check existing riser cloud " + alreadyCl;
        foreach (CB6Box b in content) if (T5Clear(b, alreadySegs) < MINCLR - 0.02) throw new Exception("NO_GO existing riser cloud " + alreadyCl + " does not hold " + b.id + " with 1.0 ft");
    }

    // 16h. final read-back
    item = "final read-back";
    document.Regenerate();
    for (int i = 0; i < N; i++)
    {
        if (madeN[i] == null) continue;
        TextNote t = document.GetElement(madeN[i].Id) as TextNote;
        if (t == null || (t.Text ?? "").Trim() != LBLT[i] || t.OwnerViewId.Value != VIEWID || t.GetTypeId().Value != tnt.Id.Value) throw new Exception("label " + madeN[i].Id.Value + " read back wrong");
        if (t.LeaderCount != (isLd[i] ? 1 : 0)) throw new Exception("label " + madeN[i].Id.Value + " has " + t.LeaderCount + " leaders, want " + (isLd[i] ? 1 : 0));
    }
    for (int i = 0; i < N; i++)
        if ((numSt[i] == "edit" || numSt[i] == "already") && !T13Eq(T13Val(T13NumParam(document.GetElement(new ElementId(hid[i])))), NNEW[i])) throw new Exception("hex " + hid[i] + " Note Number is not " + NNEW[i]);
    foreach (long oc in OLDCL) if (document.GetElement(new ElementId(oc)) != null) throw new Exception("old cloud " + oc + " still exists");
    foreach (long tgid in delTg) if (document.GetElement(new ElementId(tgid)) != null) throw new Exception("old tag " + tgid + " still exists");
    if (sheet != null && !sheet.GetAllRevisionIds().Any(x => x.Value == rev.Id.Value)) throw new Exception("sheet " + sheetNo + " does not list FEWD Submittal after the run");
    st.Commit();
}
catch (Exception ex)
{
    if (st.HasStarted() && !st.HasEnded()) st.RollBack();
    return "ROLLED_BACK " + item + ": " + CB6Cap(ex.Message, 1400) + "\ndone=0 skipped=" + skipIds.Count + " failed=1";
}
// return: tail (cleanup ids, deleted old, unplaced, counts) is never cut; then cloud line, header, per-label lines
System.Text.StringBuilder mid = new System.Text.StringBuilder();
for (int i = 0; i < N; i++)
{
    if (!ok[i]) { mid.Append(LBLT[i]).Append("@").Append(hid[i]).Append(" SKIP mismatch ").Append(CB6Show(cur[i])).Append("\n"); continue; }
    string hn = NOLD[i] != NNEW[i] ? " #" + cur[i] + ">" + NNEW[i] : "";
    if (holdP[i]) { mid.Append(LBLT[i]).Append("@").Append(hid[i]).Append(" UNPLACED").Append(hn).Append("\n"); continue; }
    long nid = madeN[i] != null ? madeN[i].Id.Value : exL[i];
    mid.Append(LBLT[i]).Append("@").Append(hid[i]).Append(">").Append(nid).Append(madeN[i] == null ? "(had)" : "").Append(" ").Append(side[i]);
    if (isLd[i] && ldSeg[i] != null) mid.Append(":").Append(T13DirName(ldDir[i])).Append(T13F(ldD[i])).Append(" end=(").Append(T13F(ldSeg[i][2])).Append(",").Append(T13F(ldSeg[i][3])).Append(")");
    mid.Append(" [").Append(T13F(lbox[i].x0)).Append(",").Append(T13F(lbox[i].y0)).Append(",").Append(T13F(lbox[i].x1)).Append(",").Append(T13F(lbox[i].y1)).Append("]").Append(hn).Append("\n");
}
mid.Append("dfu ").Append(dfuSt);
System.Text.StringBuilder cls = new System.Text.StringBuilder();
foreach (string s in cloudLines) cls.Append(CB6Cap(s, 600)).Append("\n");
if (warn.Count > 0) cls.Append("warn: ").Append(CB6Cap(string.Join("; ", warn), 250)).Append("\n");
int nLdDone = 0; for (int i = 0; i < N; i++) if (madeN[i] != null && isLd[i]) nLdDone++;
string tail = "cleanup_params: [\"" + string.Join("\",\"", cleanup) + "\"]\n"
    + "deleted_old (restore only from the SYNC A ACC version): " + (deletedOld.Count > 0 ? string.Join(",", deletedOld) : "(none)") + "\n"
    + "unplaced: " + (unplacedL.Count > 0 ? CB6Cap(string.Join("; ", unplacedL), 700) : "(none)") + "\n"
    + "done=" + (doneIds.Count + nLblDone) + " skipped=" + skipIds.Count + " failed=0 unplaced=" + unplacedL.Count + "\n"
    + "done_ids: labels=" + nLblDone + "(leader=" + nLdDone + "; note ids in cleanup_params)" + (doneIds.Count > 0 ? "," + string.Join(",", doneIds) : "") + "\n"
    + "skipped_ids: " + (skipIds.Count > 0 ? CB6Cap(string.Join(",", skipIds), 300) : "(none)");
string clsS = CB6Cap(cls.ToString().TrimEnd('\n'), 900);
string headS = CB6Cap(sb.ToString(), 450);
int left = RETMAX - tail.Length - clsS.Length - headS.Length - 3;
string midS = CB6Cap(mid.ToString(), Math.Max(300, left));
return headS + "\n" + midS + "\n" + clsS + "\n" + tail;
}

// ===================== helpers (T13 v3; CB6 / T5 copied from CB6-R1-T5.cs / T13 v2) =====================
public static int T13Use(Dictionary<long, int> d, long k) { return d.ContainsKey(k) ? d[k] : 0; }
public static string T13F(double v) { return Math.Round(v, 2).ToString(System.Globalization.CultureInfo.InvariantCulture); }
public static string T13Norm(string s) { return (s ?? "").Replace("\r\n", "\r").Replace("\n", "\r").TrimEnd('\r', ' '); }
// instance 'Note Number' (text or integer), else null
public static Parameter T13NumParam(Element e)
{
    if (e == null) return null;
    Parameter p = e.LookupParameter("Note Number");
    if (p == null) return null;
    if (p.StorageType != StorageType.String && p.StorageType != StorageType.Integer) return null;
    return p;
}
public static string T13Val(Parameter p)
{
    if (p == null) return "";
    if (p.StorageType == StorageType.Integer) return p.AsInteger().ToString(System.Globalization.CultureInfo.InvariantCulture);
    return (p.AsString() ?? "").Trim();
}
public static bool T13Eq(string a, string b) { return (a ?? "").Trim() == (b ?? "").Trim(); }
public static bool T13SetNum(Parameter p, string v)
{
    if (p.StorageType == StorageType.Integer) return p.Set(int.Parse(v, System.Globalization.CultureInfo.InvariantCulture));
    return p.Set(v);
}
// instance and type parameters whose value equals the hex number (dry-run report)
public static string T13PRep(FamilyInstance h, string num)
{
    List<string> r = new List<string>();
    foreach (Parameter p in h.Parameters)
    {
        if (p.StorageType != StorageType.String && p.StorageType != StorageType.Integer) continue;
        if (!T13Eq(T13Val(p), num) || num.Length == 0) continue;
        r.Add("'" + p.Definition.Name + "'" + (p.StorageType == StorageType.Integer ? "(int)" : "") + (p.IsReadOnly ? "(ro)" : "") + (p.IsShared ? "(sh)" : ""));
    }
    if (h.Symbol != null)
        foreach (Parameter p in h.Symbol.Parameters)
        {
            if (p.StorageType != StorageType.String && p.StorageType != StorageType.Integer) continue;
            if (T13Eq(T13Val(p), num) && num.Length > 0) r.Add("TYPE '" + p.Definition.Name + "'");
        }
    return r.Count > 0 ? string.Join(",", r) : "(no parameter)";
}
public static XYZ T13Pt(Element e, View v)
{
    LocationPoint lp = e.Location as LocationPoint;
    if (lp != null) return lp.Point;
    BoundingBoxXYZ bb = e.get_BoundingBox(v);
    if (bb == null) return null;
    return new XYZ(0.5 * (bb.Min.X + bb.Max.X), 0.5 * (bb.Min.Y + bb.Max.Y), bb.Min.Z);
}
// index of key in s where the char before is not a digit, else -1
public static int T13Find(string s, string key)
{
    int ix = 0;
    while (s != null && (ix = s.IndexOf(key, ix, StringComparison.Ordinal)) >= 0)
    {
        if (ix == 0 || !char.IsDigit(s[ix - 1])) return ix;
        ix++;
    }
    return -1;
}
public static int T13Count(string s, string key)
{
    int n = 0, ix = 0;
    while (s != null && (ix = s.IndexOf(key, ix, StringComparison.Ordinal)) >= 0)
    {
        if (ix == 0 || !char.IsDigit(s[ix - 1])) n++;
        ix++;
    }
    return n;
}
// FS / FD label text: starts with FS or FD followed by a digit, '-', a space, a line break or nothing
public static bool T13IsFsFd(string s)
{
    s = (s ?? "").Trim().ToUpperInvariant();
    if (s.Length < 2 || !(s.StartsWith("FS") || s.StartsWith("FD"))) return false;
    if (s.Length == 2) return true;
    char c = s[2];
    return char.IsDigit(c) || c == '-' || c == ' ' || c == '\r' || c == '\n';
}
public static double T13EstW(string txt, double tSize, double tWf, int S) { return (txt.Length * 0.75 * tSize * tWf + 0.003) * S; }
public static CB6Box T13Cand(char s, CB6Box hexB, double hy, double w, double h, double gapX, long id)
{
    if (s == 'R') return new CB6Box(hexB.x1 + gapX, hy - h / 2, hexB.x1 + gapX + w, hy + h / 2, id);
    return new CB6Box(hexB.x0 - gapX - w, hy - h / 2, hexB.x0 - gapX, hy + h / 2, id);
}
// v3: null = acceptable: no annotation / hex / new label overlap and clear of every remaining (non-replaced) cloud
public static string T13Judge(CB6Box b, long selfHex, List<CB6Box> obs, List<CB6Box> placed, List<CB6Box> hard, double freeGap)
{
    CB6Box bg = CB6Ex(b, 0.02);
    foreach (CB6Box o in obs) { if (o.id == selfHex) continue; if (CB6Ov(bg, o)) return "hits " + o.id; }
    foreach (CB6Box o in placed) { if (o.id == selfHex) continue; if (CB6Ov(bg, o)) return "hits new label of hex " + o.id; }
    foreach (CB6Box h in hard) if (CB6Ov(CB6Ex(b, freeGap), h)) return "near cloud " + h.id;
    return null;
}
// creates the note (left/right aligned, top), regenerates, measures, moves it onto the target edge / centre line
public static TextNote T13Make(Document doc, View v, ElementId typeId, string txt, char s, CB6Box hexB, double hy, double gapX, int S, double estW, double estH, double z, out CB6Box box, out string src)
{
    TextNoteOptions o = new TextNoteOptions(typeId);
    o.HorizontalAlignment = s == 'R' ? HorizontalTextAlignment.Left : HorizontalTextAlignment.Right;
    o.VerticalAlignment = VerticalTextAlignment.Top;
    double x = s == 'R' ? hexB.x1 + gapX : hexB.x0 - gapX;
    TextNote t = TextNote.Create(doc, v.Id, new XYZ(x, hy + estH / 2, z), txt, o);
    doc.Regenerate();
    box = T13Meas(t, v, S, estW, estH, out src);
    double dx = s == 'R' ? (hexB.x1 + gapX) - box.x0 : (hexB.x0 - gapX) - box.x1;
    double dy = hy - 0.5 * (box.y0 + box.y1);
    if (Math.Abs(dx) > 0.003 || Math.Abs(dy) > 0.003)
    {
        ElementTransformUtils.MoveElement(doc, t.Id, new XYZ(dx, dy, 0));
        doc.Regenerate();
        box = T13Meas(t, v, S, estW, estH, out src);
    }
    return t;
}
// creates a left-aligned top note at box b, measures it and moves its box onto b's left edge / centre line (no leader)
public static TextNote T13MakeAt(Document doc, View v, ElementId typeId, string txt, CB6Box b, int S, double estW, double estH, double z, out CB6Box box, out string src)
{
    TextNoteOptions o = new TextNoteOptions(typeId);
    o.HorizontalAlignment = HorizontalTextAlignment.Left;
    o.VerticalAlignment = VerticalTextAlignment.Top;
    TextNote t = TextNote.Create(doc, v.Id, new XYZ(b.x0, b.y1, z), txt, o);
    doc.Regenerate();
    box = T13Meas(t, v, S, estW, estH, out src);
    T13MoveTo(doc, t, v, S, estW, estH, b.x0, 0.5 * (b.y0 + b.y1), ref box, out src);
    return t;
}
// moves a note (without leader) so its measured box has left edge x0 and centre cy; box is re-measured
public static void T13MoveTo(Document doc, TextNote t, View v, int S, double estW, double estH, double x0, double cy, ref CB6Box box, out string src)
{
    src = "";
    double dx = x0 - box.x0, dy = cy - 0.5 * (box.y0 + box.y1);
    if (Math.Abs(dx) > 0.003 || Math.Abs(dy) > 0.003)
    {
        ElementTransformUtils.MoveElement(doc, t.Id, new XYZ(dx, dy, 0));
        doc.Regenerate();
    }
    box = T13Meas(t, v, S, estW, estH, out src);
}
// note box from Coord + Width/Height (paper ft x scale, plausibility-checked), unioned with a plausible view bbox
public static CB6Box T13Meas(TextNote t, View v, int S, double estW, double estH, out string src)
{
    double w = t.Width * S, h = t.Height * S; src = "api";
    if (!(w > 0.3 * estW && w < 3.0 * estW)) { w = estW; src = "estW"; }
    if (!(h > 0.3 * estH && h < 3.0 * estH)) { h = estH; src += "/estH"; }
    XYZ c = t.Coord;
    double x0 = c.X, x1 = c.X + w;
    if (t.HorizontalAlignment == HorizontalTextAlignment.Right) { x0 = c.X - w; x1 = c.X; }
    else if (t.HorizontalAlignment == HorizontalTextAlignment.Center) { x0 = c.X - w / 2; x1 = c.X + w / 2; }
    double y1 = c.Y, y0 = c.Y - h;
    if (t.VerticalAlignment == VerticalTextAlignment.Middle) { y0 = c.Y - h / 2; y1 = c.Y + h / 2; }
    else if (t.VerticalAlignment == VerticalTextAlignment.Bottom) { y0 = c.Y; y1 = c.Y + h; }
    BoundingBoxXYZ bb = t.get_BoundingBox(v);
    if (bb != null && t.LeaderCount == 0)
    {
        double bw = bb.Max.X - bb.Min.X, bh = bb.Max.Y - bb.Min.Y;
        if (bw > 0.5 * w && bw < 3.0 * w && bh > 0.5 * h && bh < 3.0 * h)
        { x0 = Math.Min(x0, bb.Min.X); y0 = Math.Min(y0, bb.Min.Y); x1 = Math.Max(x1, bb.Max.X); y1 = Math.Max(y1, bb.Max.Y); src += "+bbox"; }
    }
    return new CB6Box(x0, y0, x1, y1, t.Id.Value);
}
public static string T13AlignErr(CB6Box b, char s, CB6Box hexB, double hy, double gapX)
{
    double ex = s == 'R' ? b.x0 - (hexB.x1 + gapX) : (hexB.x0 - gapX) - b.x1;
    double ey = 0.5 * (b.y0 + b.y1) - hy;
    if (Math.Abs(ex) > 0.02 || Math.Abs(ey) > 0.02) return "edge off " + T13F(ex) + " centre off " + T13F(ey);
    return null;
}
// ---------- leader placement ----------
public class T13LC { public CB6Box box; public int dir; public double d, ax, ay, ex, ey; public char ls; }
public static double[] T13Ang() { return new double[] { 22.5, 67.5, 112.5, 157.5, 202.5, 247.5, 292.5, 337.5 }; }
public static string T13DirName(int dir)
{
    if (dir >= 8 && dir <= 15) return "A" + T13Ang()[dir - 8].ToString(System.Globalization.CultureInfo.InvariantCulture);
    if (dir == 16) return "N2";
    if (dir == 17) return "S2";
    switch (dir) { case 0: return "E"; case 1: return "W"; case 2: return "N"; case 3: return "S"; case 4: return "NE"; case 5: return "SE"; case 6: return "NW"; default: return "SW"; }
}
// label box d ft from the hex box in direction dir (E,W,N,S,NE,SE,NW,SW); E/W centred on the hex point Y;
// N/S with the near edge at the hex centre X (so the leader drops straight from the text end)
public static CB6Box T13LBox(int dir, double d, CB6Box hx, double hy, double w, double h, char pref, long id)
{
    int[] DX = new int[] { 1, -1, 0, 0, 1, 1, -1, -1 }, DY = new int[] { 0, 0, 1, -1, 1, -1, 1, -1 };
    double f = (DX[dir] != 0 && DY[dir] != 0) ? 0.7071 : 1.0;
    double cx = 0.5 * (hx.x0 + hx.x1);
    double x0, y0;
    if (DX[dir] > 0) x0 = hx.x1 + d * f; else if (DX[dir] < 0) x0 = hx.x0 - d * f - w; else x0 = pref == 'L' ? cx - w : cx;
    if (DY[dir] > 0) y0 = hx.y1 + d * f; else if (DY[dir] < 0) y0 = hx.y0 - d * f - h; else y0 = hy - h / 2;
    return new CB6Box(x0, y0, x0 + w, y0 + h, id);
}
public static double T13Gap(CB6Box a, CB6Box b)
{
    double dx = Math.Max(0, Math.Max(a.x0 - b.x1, b.x0 - a.x1)), dy = Math.Max(0, Math.Max(a.y0 - b.y1, b.y0 - a.y1));
    return Math.Sqrt(dx * dx + dy * dy);
}
// label box centred on the ray at angle deg from the hex centre, pushed out until its gap from the hex box is d
public static CB6Box T13LBoxA(double deg, double d, CB6Box hx, double w, double h, long id)
{
    double a = deg * Math.PI / 180.0, ux = Math.Cos(a), uy = Math.Sin(a);
    double cx = 0.5 * (hx.x0 + hx.x1), cy = 0.5 * (hx.y0 + hx.y1);
    double lo = 0, hi = 40;
    for (int k = 0; k < 40; k++)
    {
        double t = 0.5 * (lo + hi);
        CB6Box b = new CB6Box(cx + ux * t - w / 2, cy + uy * t - h / 2, cx + ux * t + w / 2, cy + uy * t + h / 2, id);
        if (T13Gap(b, hx) < d) lo = t; else hi = t;
    }
    return new CB6Box(cx + ux * hi - w / 2, cy + uy * hi - h / 2, cx + ux * hi + w / 2, cy + uy * hi + h / 2, id);
}
// dirs 0..7 = T13LBox, 8..15 = the 22.5 deg rays, 16/17 = N/S with the label on the other side of the hex centre
public static CB6Box T13LBoxAny(int dir, double d, CB6Box hx, double hy, double w, double h, char pref, long id)
{
    if (dir < 8) return T13LBox(dir, d, hx, hy, w, h, pref, id);
    if (dir == 16) return T13LBox(2, d, hx, hy, w, h, pref == 'L' ? 'R' : 'L', id);
    if (dir == 17) return T13LBox(3, d, hx, hy, w, h, pref == 'L' ? 'R' : 'L', id);
    return T13LBoxA(T13Ang()[dir - 8], d, hx, w, h, id);
}
// leader end on the hex edge: ray from the hex box centre toward the anchor, on the inscribed ellipse x 0.95
public static double[] T13LEnd(CB6Box hx, double ax, double ay)
{
    double cx = 0.5 * (hx.x0 + hx.x1), cy = 0.5 * (hx.y0 + hx.y1);
    double rx = Math.Max(0.01, 0.5 * (hx.x1 - hx.x0)), ry = Math.Max(0.01, 0.5 * (hx.y1 - hx.y0));
    double vx = ax - cx, vy = ay - cy, L = Math.Sqrt(vx * vx + vy * vy);
    if (L < 1e-6) return null;
    double ux = vx / L, uy = vy / L;
    double r = 1.0 / Math.Sqrt(ux * ux / (rx * rx) + uy * uy / (ry * ry));
    if (L - 0.95 * r < 0.1) return null;
    return new double[] { cx + 0.95 * r * ux, cy + 0.95 * r * uy };
}
public static T13LC T13LGeo(CB6Box b, int dir, double d, CB6Box hx)
{
    double cx = 0.5 * (hx.x0 + hx.x1);
    T13LC lc = new T13LC(); lc.box = b; lc.dir = dir; lc.d = d;
    if (b.x0 >= cx - 1e-6) { lc.ls = 'L'; lc.ax = b.x0; } else { lc.ls = 'R'; lc.ax = b.x1; }
    lc.ay = 0.5 * (b.y0 + b.y1);
    double[] en = T13LEnd(hx, lc.ax, lc.ay);
    if (en == null) return null;
    lc.ex = en[0]; lc.ey = en[1];
    return lc;
}
// candidates in order: distance first (dmin..dmax), then direction (preferred side, its shallow rays, diagonals, N, S, ...)
public static List<T13LC> T13LeadSearch(CB6Box hx, long hexId, double hy, double w, double h, char pref, double dmin, double dstep, double dmax, List<CB6Box> obs, List<CB6Box> placed, List<double[]> exLead, List<double[]> newLead, List<double[]> cloudSegs, List<CB6Box> hard, double freeGap, double lclr, int maxOut, out string why)
{
    int[] ord = pref == 'L' ? new int[] { 1, 11, 12, 6, 7, 2, 3, 16, 17, 10, 13, 0, 9, 14, 4, 5, 8, 15 } : new int[] { 0, 8, 15, 4, 5, 2, 3, 16, 17, 9, 14, 1, 10, 13, 6, 7, 11, 12 };
    List<T13LC> res = new List<T13LC>();
    Dictionary<string, int> cnt = new Dictionary<string, int>(); Dictionary<string, int> hitIds = new Dictionary<string, int>();
    int tried = 0;
    for (double d = dmin; d <= dmax + 1e-6 && res.Count < maxOut; d += dstep)
        foreach (int dir in ord)
        {
            if (res.Count >= maxOut) break;
            tried++;
            CB6Box b = T13LBoxAny(dir, d, hx, hy, w, h, pref, hexId);
            string jw = T13Judge(b, hexId, obs, placed, hard, freeGap);
            if (jw == null) jw = T13BoxLeads(b, exLead, newLead);
            T13LC lc = null;
            if (jw == null)
            {
                lc = T13LGeo(b, dir, d, hx);
                if (lc == null) jw = "leader too short";
                else jw = T13LeadOk(lc.ax, lc.ay, lc.ex, lc.ey, hexId, hx, obs, placed, exLead, newLead, cloudSegs, lclr);
            }
            if (jw == null) { res.Add(lc); continue; }
            string cat = T13Cat(jw);
            cnt[cat] = (cnt.ContainsKey(cat) ? cnt[cat] : 0) + 1;
            string[] wds = jw.Split(' ');
            string last = wds[wds.Length - 1];
            long dummy;
            if (long.TryParse(last, out dummy)) hitIds[last] = (hitIds.ContainsKey(last) ? hitIds[last] : 0) + 1;
        }
    List<string> parts = new List<string>();
    foreach (KeyValuePair<string, int> kv in cnt.OrderByDescending(x => x.Value)) parts.Add(kv.Key + "=" + kv.Value);
    string top = "";
    foreach (KeyValuePair<string, int> kv in hitIds.OrderByDescending(x => x.Value).Take(3)) top += (top.Length > 0 ? "," : "") + kv.Key + "x" + kv.Value;
    why = tried + " tried " + string.Join(" ", parts) + (top.Length > 0 ? " top=" + top : "");
    return res;
}
public static string T13Cat(string jw)
{
    if (jw.StartsWith("label hits new leader")) return "lbl_on_newleader";
    if (jw.StartsWith("label hits leader")) return "lbl_on_leader";
    if (jw.StartsWith("hits new label")) return "newlabel";
    if (jw.StartsWith("hits ")) return "obs";
    if (jw.StartsWith("near cloud")) return "nearcloud";
    if (jw.StartsWith("leader hits new label")) return "ld_newlabel";
    if (jw.StartsWith("leader hits")) return "ld_obs";
    if (jw.StartsWith("leader near new leader")) return "ld_newleader";
    if (jw.StartsWith("leader near leader")) return "ld_leader";
    if (jw.StartsWith("leader crosses cloud")) return "ld_cloud";
    return jw.Split(' ')[0];
}
public static string T13BoxLeads(CB6Box b, List<double[]> exLead, List<double[]> newLead)
{
    CB6Box bg = CB6Ex(b, 0.02);
    foreach (double[] s in exLead) if (T13SegBox(s[0], s[1], s[2], s[3], bg)) return "label hits leader of " + (long)s[4];
    foreach (double[] s in newLead) if (T13SegBox(s[0], s[1], s[2], s[3], bg)) return "label hits new leader of hex " + (long)s[4];
    return null;
}
// leader (anchor -> end) must clear text/tags/annotations (not its own hex), new labels, existing and new leaders
// (a leader leaving this same hex only needs a 20 deg separation) and must not cross a remaining cloud line
public static string T13LeadOk(double ax, double ay, double ex, double ey, long hexId, CB6Box hx, List<CB6Box> obs, List<CB6Box> placed, List<double[]> exLead, List<double[]> newLead, List<double[]> cloudSegs, double lclr)
{
    foreach (CB6Box o in obs) { if (o.id == hexId) continue; if (T13SegBox(ax, ay, ex, ey, CB6Ex(o, lclr))) return "leader hits " + o.id; }
    foreach (CB6Box o in placed) { if (o.id == hexId) continue; if (T13SegBox(ax, ay, ex, ey, CB6Ex(o, lclr))) return "leader hits new label of hex " + o.id; }
    double[] me = new double[] { ax, ay, ex, ey };
    CB6Box hz = CB6Ex(hx, 0.1);
    double cx = 0.5 * (hx.x0 + hx.x1), cy = 0.5 * (hx.y0 + hx.y1);
    foreach (double[] s in exLead)
    {
        bool i0 = T13InB(s[0], s[1], hz), i1 = T13InB(s[2], s[3], hz);
        if (i0 && i1) continue;
        if (i0 || i1)
        {
            double fx = i0 ? s[2] : s[0], fy = i0 ? s[3] : s[1];
            double a1 = Math.Atan2(fy - cy, fx - cx), a2 = Math.Atan2(ay - cy, ax - cx);
            double da = Math.Abs(a1 - a2); if (da > Math.PI) da = 2 * Math.PI - da;
            if (da < 20.0 * Math.PI / 180.0) return "leader near leader of " + (long)s[4];
            continue;
        }
        if (T13SegSeg(me, s) < lclr) return "leader near leader of " + (long)s[4];
    }
    foreach (double[] s in newLead) if (T13SegSeg(me, s) < lclr) return "leader near new leader of hex " + (long)s[4];
    foreach (double[] s in cloudSegs) if (T13SegSeg(me, s) < 0.02) return "leader crosses cloud line";
    return null;
}
// adds one straight leader ending at (ex,ey), elbow put on the anchor-end line; returns null and the segment, or why
public static string T13AddLead(Document doc, TextNote t, char ls, double ex, double ey, double z, out double[] seg)
{
    seg = null;
    try
    {
        Leader l = t.AddLeader(ls == 'L' ? TextNoteLeaderTypes.TNLT_STRAIGHT_L : TextNoteLeaderTypes.TNLT_STRAIGHT_R);
        l.End = new XYZ(ex, ey, z);
        doc.Regenerate();
        IList<Leader> l2 = t.GetLeaders();
        if (l2.Count != 1) return "leader count " + l2.Count;
        XYZ a = l2[0].Anchor;
        l2[0].Elbow = new XYZ(0.5 * (a.X + ex), 0.5 * (a.Y + ey), z);
        doc.Regenerate();
        Leader l3 = t.GetLeaders()[0];
        XYZ a3 = l3.Anchor, b3 = l3.Elbow, e3 = l3.End;
        double de = Math.Sqrt((e3.X - ex) * (e3.X - ex) + (e3.Y - ey) * (e3.Y - ey));
        if (de > 0.01) return "leader end moved " + T13F(de);
        double[] s3 = new double[] { a3.X, a3.Y, e3.X, e3.Y };
        if (T13PtSeg(b3.X, b3.Y, s3) > 0.01) return "leader not straight (elbow off " + T13F(T13PtSeg(b3.X, b3.Y, s3)) + ")";
        seg = s3;
        return null;
    }
    catch (Exception lx) { return "leader api: " + CB6Cap(lx.Message, 80); }
}
// existing leaders of a text note / generic annotation / tag as segments {x0,y0,x1,y1,ownerId}; returns how many were approximated
public static int T13Leads(Document doc, View v, Element e, List<double[]> outS)
{
    int approx = 0;
    IList<Leader> ls = null; XYZ origin = null;
    if (e is TextNote) ls = ((TextNote)e).GetLeaders();
    else if (e is AnnotationSymbol)
    {
        ls = ((AnnotationSymbol)e).GetLeaders();
        LocationPoint lp = e.Location as LocationPoint; if (lp != null) origin = lp.Point;
    }
    if (ls != null)
        foreach (Leader l in ls)
        {
            XYZ a = null, b = null, c = null;
            try { a = l.Anchor; } catch (Exception) { a = origin; if (a != null) approx++; }
            try { b = l.Elbow; } catch (Exception) { }
            try { c = l.End; } catch (Exception) { }
            T13AddPath(outS, e.Id.Value, a, b, c);
        }
    IndependentTag tg = e as IndependentTag;
    if (tg != null && tg.HasLeader)
    {
        XYZ hp = tg.TagHeadPosition;
        foreach (Reference r in tg.GetTaggedReferences())
        {
            XYZ el = null, en = null;
            try { if (tg.HasLeaderElbow(r)) el = tg.GetLeaderElbow(r); } catch (Exception) { }
            try { en = tg.GetLeaderEnd(r); }
            catch (Exception)
            {
                Element te = doc.GetElement(r); BoundingBoxXYZ tb = te != null ? te.get_BoundingBox(v) : null;
                if (tb != null) { en = new XYZ(0.5 * (tb.Min.X + tb.Max.X), 0.5 * (tb.Min.Y + tb.Max.Y), tb.Min.Z); approx++; }
            }
            T13AddPath(outS, e.Id.Value, hp, el, en);
        }
    }
    return approx;
}
public static void T13AddPath(List<double[]> outS, long id, XYZ a, XYZ b, XYZ c)
{
    List<XYZ> pts = new List<XYZ>();
    if (a != null) pts.Add(a); if (b != null) pts.Add(b); if (c != null) pts.Add(c);
    for (int k = 0; k + 1 < pts.Count; k++)
    {
        XYZ p = pts[k], q = pts[k + 1];
        if (Math.Abs(p.X - q.X) + Math.Abs(p.Y - q.Y) < 1e-6) continue;
        outS.Add(new double[] { p.X, p.Y, q.X, q.Y, id });
    }
}
public static bool T13InB(double x, double y, CB6Box b) { return x >= b.x0 && x <= b.x1 && y >= b.y0 && y <= b.y1; }
// segment / box overlap (Liang-Barsky clip)
public static bool T13SegBox(double ax, double ay, double bx, double by, CB6Box r)
{
    double t0 = 0, t1 = 1, dx = bx - ax, dy = by - ay;
    double[] p = new double[] { -dx, dx, -dy, dy }, q = new double[] { ax - r.x0, r.x1 - ax, ay - r.y0, r.y1 - ay };
    for (int k = 0; k < 4; k++)
    {
        if (Math.Abs(p[k]) < 1e-12) { if (q[k] < 0) return false; continue; }
        double t = q[k] / p[k];
        if (p[k] < 0) { if (t > t1) return false; if (t > t0) t0 = t; }
        else { if (t < t0) return false; if (t < t1) t1 = t; }
    }
    return true;
}
public static double T13Cr(double ax, double ay, double bx, double by, double cx, double cy) { return (bx - ax) * (cy - ay) - (by - ay) * (cx - ax); }
public static bool T13SegX(double[] s, double[] t)
{
    double d1 = T13Cr(t[0], t[1], t[2], t[3], s[0], s[1]), d2 = T13Cr(t[0], t[1], t[2], t[3], s[2], s[3]);
    double d3 = T13Cr(s[0], s[1], s[2], s[3], t[0], t[1]), d4 = T13Cr(s[0], s[1], s[2], s[3], t[2], t[3]);
    return ((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) && ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0));
}
public static double T13PtSeg(double px, double py, double[] s)
{
    double dx = s[2] - s[0], dy = s[3] - s[1], L2 = dx * dx + dy * dy;
    double u = L2 < 1e-12 ? 0 : Math.Max(0, Math.Min(1, ((px - s[0]) * dx + (py - s[1]) * dy) / L2));
    double qx = s[0] + u * dx - px, qy = s[1] + u * dy - py;
    return Math.Sqrt(qx * qx + qy * qy);
}
public static double T13SegSeg(double[] s, double[] t)
{
    if (T13SegX(s, t)) return 0;
    return Math.Min(Math.Min(T13PtSeg(s[0], s[1], t), T13PtSeg(s[2], s[3], t)), Math.Min(T13PtSeg(t[0], t[1], s), T13PtSeg(t[2], t[3], s)));
}
// ---------- v3 riser cloud ----------
// content boxes of the riser cloud; ids gets every element id that is content (so it is not an obstacle);
// cnt = hex, fs, fs/fd label, inside an old cloud, dfu, label, new leader, hex / label leader
public static List<CB6Box> T13Content(Document doc, View v, List<FamilyInstance> hexCands, long[] fsIds, long dfuId, CB6Box dfuBox, int n, bool[] okC, CB6Box[] lbox, long[] exL, List<double[]> newLead, List<double[]> exLead, List<CB6Box> obs, List<List<double[]>> oldSegs, HashSet<long> fsfd, HashSet<long> ids, int[] cnt, double bigLim)
{
    List<CB6Box> r = new List<CB6Box>(); ids.Clear(); for (int k = 0; k < cnt.Length; k++) cnt[k] = 0;
    HashSet<long> hexSet = new HashSet<long>(), exLSet = new HashSet<long>();
    for (int i = 0; i < n; i++) if (okC[i] && exL[i] > 0) exLSet.Add(exL[i]);
    foreach (FamilyInstance h in hexCands)
    {
        BoundingBoxXYZ bb = h.get_BoundingBox(v); if (bb == null) continue;
        CB6Box b = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, h.Id.Value);
        if (b.x1 - b.x0 > bigLim || b.y1 - b.y0 > bigLim) continue;
        r.Add(b); ids.Add(h.Id.Value); hexSet.Add(h.Id.Value); cnt[0]++;
    }
    foreach (long f in fsIds)
    {
        if (f <= 0) continue;
        Element e = doc.GetElement(new ElementId(f)); BoundingBoxXYZ bb = e != null ? e.get_BoundingBox(v) : null;
        if (bb == null) continue;
        r.Add(new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, f)); ids.Add(f); cnt[1]++;
    }
    if (dfuBox != null) { r.Add(dfuBox); ids.Add(dfuId); cnt[4]++; }
    foreach (long x in exLSet) ids.Add(x);
    foreach (CB6Box o in obs)
    {
        if (ids.Contains(o.id)) continue;
        bool take = false;
        if (fsfd.Contains(o.id)) { take = true; cnt[2]++; }
        else
        {
            double cx = 0.5 * (o.x0 + o.x1), cy = 0.5 * (o.y0 + o.y1);
            foreach (List<double[]> sg in oldSegs) if (T5In(cx, cy, sg)) { take = true; break; }
            if (take) cnt[3]++;
        }
        if (take) { r.Add(o); ids.Add(o.id); }
    }
    for (int i = 0; i < n; i++)
    {
        if (!okC[i] || lbox[i] == null) continue;
        r.Add(new CB6Box(lbox[i].x0, lbox[i].y0, lbox[i].x1, lbox[i].y1, -(i + 1))); cnt[5]++;
    }
    foreach (double[] s in newLead) { r.Add(new CB6Box(s[0], s[1], s[2], s[3], -100)); cnt[6]++; }
    foreach (double[] s in exLead)
    {
        long ow = (long)s[4];
        if (hexSet.Contains(ow) || exLSet.Contains(ow)) { r.Add(new CB6Box(s[0], s[1], s[2], s[3], -200)); cnt[7]++; }
    }
    return r;
}
// one rectangle: hull(content) + margin, grown to enclose (with margin) every other item its line band or delta box
// would touch; NO_GO text if it would touch a remaining cloud, enclose more than cap items or exceed maxSide
public static string T13Rect(List<CB6Box> content, List<CB6Box> others, List<CB6Box> hard, double margin, double clr, double[] tg, double maxSide, int cap, List<long> encl, out CB6Box R)
{
    encl.Clear();
    R = CB6Ex(CB6Hull(content, 0), margin);
    HashSet<long> es = new HashSet<long>();
    for (int it = 0; it < 400; it++)
    {
        bool ch = false;
        CB6Box tb = T5TagBox(CB6TagHead(new List<CB6Box> { R }, tg[0], tg[1]), tg);
        foreach (CB6Box h in hard)
            if (CB6Ov(CB6Ex(h, 2 * clr), R) || CB6Ov(CB6Ex(h, clr), tb)) return "riser cloud would touch other cloud " + h.id;
        foreach (CB6Box o in others)
        {
            if (es.Contains(o.id)) continue;
            bool deep = o.x0 >= R.x0 + margin - 0.02 && o.x1 <= R.x1 - margin + 0.02 && o.y0 >= R.y0 + margin - 0.02 && o.y1 <= R.y1 - margin + 0.02;
            if (deep) continue;
            if (CB6Ov(CB6Ex(o, clr), R) || CB6Ov(CB6Ex(o, clr), tb))
            {
                R = CB6Hull(new List<CB6Box> { R, CB6Ex(o, margin) }, 0); es.Add(o.id); encl.Add(o.id); ch = true;
                if (encl.Count > cap) return "riser cloud would have to enclose more than " + cap + " other items";
                break;
            }
        }
        if (R.x1 - R.x0 > maxSide || R.y1 - R.y0 > maxSide) return "riser cloud " + T13F(R.x1 - R.x0) + " x " + T13F(R.y1 - R.y0) + " ft exceeds " + T13F(maxSide) + " ft (30 in paper)";
        if (!ch) return null;
    }
    return "riser cloud plan did not settle";
}
public static List<double[]> T13RectSegs(CB6Box R)
{
    List<double[]> l = new List<double[]> { new double[] { R.x0, R.y0 }, new double[] { R.x0, R.y1 }, new double[] { R.x1, R.y1 }, new double[] { R.x1, R.y0 } };
    return T5Segs(l);
}
// null = box is inside with >= minIn from the line, or outside with >= minOut gap; else why
public static string T13CutErr(CB6Box b, List<double[]> sg, double minIn, double minOut)
{
    double d = T5Clear(b, sg);
    if (d >= 0) return d >= minIn ? null : "inside, clr " + T13F(d);
    bool anyIn = T5In(b.x0, b.y0, sg) || T5In(b.x0, b.y1, sg) || T5In(b.x1, b.y0, sg) || T5In(b.x1, b.y1, sg);
    double g = T5BoxSegs(b, sg);
    if (anyIn || g < 1e-6) return "cut by cloud line";
    return g >= minOut ? null : "outside, gap " + T13F(g);
}
// crop: NO_GO text if the box leaves an active view crop; with no crop, info reports how far the viewport box grows
public static string T13CropErr(View v, Viewport vp, CB6Box b, double z, out string info)
{
    info = "";
    XYZ[] cs = new XYZ[] { new XYZ(b.x0, b.y0, z), new XYZ(b.x0, b.y1, z), new XYZ(b.x1, b.y1, z), new XYZ(b.x1, b.y0, z) };
    try
    {
        if (v.CropBoxActive)
        {
            BoundingBoxXYZ cb = v.CropBox; Transform ti = cb.Transform.Inverse;
            double ox = 0, oy = 0;
            foreach (XYZ p in cs)
            {
                XYZ q = ti.OfPoint(p);
                ox = Math.Max(ox, Math.Max(cb.Min.X - q.X, q.X - cb.Max.X)); oy = Math.Max(oy, Math.Max(cb.Min.Y - q.Y, q.Y - cb.Max.Y));
            }
            info = "crop active out_x=" + T13F(ox) + " out_y=" + T13F(oy) + " ft";
            if (ox > 0.01 || oy > 0.01) return "riser cloud / delta would leave the view crop by " + T13F(Math.Max(ox, oy)) + " ft";
            return null;
        }
        IList<TransformWithBoundary> tl = v.GetModelToProjectionTransforms();
        if (tl == null || tl.Count == 0) { info = "crop inactive, no projection transform"; return null; }
        Transform m2p = tl[0].GetModelToProjectionTransform(); Transform p2s = vp.GetProjectionToSheetTransform();
        Outline o = vp.GetBoxOutline(); XYZ mn = o.MinimumPoint, mx = o.MaximumPoint;
        double g = 0;
        foreach (XYZ p in cs)
        {
            XYZ s = p2s.OfPoint(m2p.OfPoint(p));
            g = Math.Max(g, Math.Max(Math.Max(mn.X - s.X, s.X - mx.X), Math.Max(mn.Y - s.Y, s.Y - mx.Y)));
        }
        info = "crop inactive (viewport box follows the view extents); viewport box grows by " + T13F(Math.Max(0, g) * 12.0) + " in";
        return null;
    }
    catch (Exception ex) { info = "crop check failed: " + CB6Cap(ex.Message, 80); return null; }
}
public static string T13ItemLine(int i, string[] lbl, long[] hid, string[] cur, string[] nnew, string[] numSt, long[] exL, char[] side, CB6Box[] lb, string[] info, string[] how, bool[] hold, int[] ldDir, double[] ldD, double[][] ldSeg)
{
    System.Text.StringBuilder s = new System.Text.StringBuilder();
    s.Append(lbl[i]).Append("@").Append(hid[i]).Append(" #").Append(cur[i]).Append(numSt[i] == "edit" ? ">" + nnew[i] : "").Append(numSt[i] == "ok" ? "" : " " + numSt[i]);
    if (numSt[i] == "mismatch") return s.Append(" SKIP").ToString();
    if (exL[i] > 0) s.Append(" had=").Append(exL[i]);
    if (lb[i] == null || hold[i]) return s.Append(" UNPLACED").ToString();
    s.Append(" ").Append(side[i]);
    if (how[i] == "lead" && ldSeg[i] != null) s.Append(":").Append(T13DirName(ldDir[i])).Append(T13F(ldD[i])).Append(" end=(").Append(T13F(ldSeg[i][2])).Append(",").Append(T13F(ldSeg[i][3])).Append(")");
    s.Append("[").Append(T13F(lb[i].x0)).Append(",").Append(T13F(lb[i].y0)).Append(",").Append(T13F(lb[i].x1)).Append(",").Append(T13F(lb[i].y1)).Append("]");
    if (info[i].Length > 0) s.Append(" ").Append(info[i]);
    return s.ToString();
}
public static string CB6Show(string s)
{
    if (s == null) return "null";
    System.Text.StringBuilder b = new System.Text.StringBuilder();
    b.Append("'").Append(s.Replace("\r", "|")).Append("'[");
    int n = Math.Min(s.Length, 40);
    for (int k = 0; k < n; k++) { if (k > 0) b.Append(","); b.Append(((int)s[k]).ToString()); }
    return b.Append(s.Length > 40 ? ",...]" : "]").ToString();
}
public class CB6Box
{
    public double x0, y0, x1, y1; public long id;
    public CB6Box(double a, double b, double c, double d, long i) { x0 = Math.Min(a, c); x1 = Math.Max(a, c); y0 = Math.Min(b, d); y1 = Math.Max(b, d); id = i; }
}
public static string CB6NoGo(string r) { return "NO_GO " + r + "\ndone=0 skipped=0 failed=0"; }
public static string CB6Cap(string s, int n) { return s.Length <= n ? s : s.Substring(0, Math.Max(0, n - 3)) + "..."; }
public static CB6Box CB6Ex(CB6Box b, double d) { return new CB6Box(b.x0 - d, b.y0 - d, b.x1 + d, b.y1 + d, b.id); }
public static bool CB6Ov(CB6Box a, CB6Box b) { double e = 1e-6; return a.x0 < b.x1 - e && b.x0 < a.x1 - e && a.y0 < b.y1 - e && b.y0 < a.y1 - e; }
public static CB6Box CB6Hull(List<CB6Box> rs, long id)
{
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
    foreach (CB6Box r in rs) { a = Math.Min(a, r.x0); b = Math.Min(b, r.y0); c = Math.Max(c, r.x1); d = Math.Max(d, r.y1); }
    return new CB6Box(a, b, c, d, id);
}
public static double[] CB6TagHead(List<CB6Box> rs, double dx, double dy)
{
    double top = double.MinValue; foreach (CB6Box r in rs) top = Math.Max(top, r.y1);
    CB6Box best = null;
    foreach (CB6Box r in rs) if (Math.Abs(r.y1 - top) < 0.011 && (best == null || r.x1 > best.x1)) best = r;
    double hx = Math.Max(best.x1 - dx, 0.5 * (best.x0 + best.x1));
    return new double[] { hx, top + dy, top };
}
public static CB6Box T5TagBox(double[] h, double[] tg) { return new CB6Box(h[0] - tg[2], h[1] - tg[4], h[0] + tg[3], h[1] + tg[5], -1); }
public static double CB6Area(List<double[]> p)
{
    double s = 0;
    for (int k = 0; k < p.Count; k++) { double[] a = p[k], b = p[(k + 1) % p.Count]; s += a[0] * b[1] - b[0] * a[1]; }
    return 0.5 * s;
}
public static double CB6SketchArea(RevisionCloud rc)
{
    IList<Curve> cs = rc.GetSketchCurves(); double s = 0;
    foreach (Curve c in cs) { XYZ a = c.GetEndPoint(0), b = c.GetEndPoint(1); s += a.X * b.Y - b.X * a.Y; }
    return 0.5 * s;
}
public static CB6Box CB6CloudBox(RevisionCloud rc)
{
    IList<Curve> cs = rc.GetSketchCurves(); if (cs == null || cs.Count == 0) return null;
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
    foreach (Curve cv in cs) for (int k = 0; k < 2; k++) { XYZ p = cv.GetEndPoint(k); a = Math.Min(a, p.X); b = Math.Min(b, p.Y); c = Math.Max(c, p.X); d = Math.Max(d, p.Y); }
    return new CB6Box(a, b, c, d, rc.Id.Value);
}
public static CB6Box CB6TextBox(TextNote t, View v, int scale, Document doc)
{
    BoundingBoxXYZ bb = t.get_BoundingBox(v);
    double size = 0;
    Element ty = doc.GetElement(t.GetTypeId());
    if (ty != null) { Parameter ps = ty.get_Parameter(BuiltInParameter.TEXT_SIZE); if (ps != null) size = ps.AsDouble(); }
    double minH = size > 0 ? size * scale * 0.5 : 0.1;
    if (bb != null && (bb.Max.Y - bb.Min.Y) >= minH) return new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, t.Id.Value);
    XYZ c = t.Coord; XYZ bd = t.BaseDirection;
    double w = t.Width * scale, h = t.Height * scale;
    if (w > 60.0) w = t.Width;
    if (h <= 0 && size > 0) h = size * scale * 1.7;
    if (h > 60.0) h = t.Height;
    double x0, x1, y0, y1;
    if (bd == null || Math.Abs(bd.Y) > 1e-3 || bd.X < 0)
    {
        double r = Math.Max(w, h); x0 = c.X - r; x1 = c.X + r; y0 = c.Y - r; y1 = c.Y + r;
    }
    else
    {
        x0 = c.X; x1 = c.X + w;
        if (t.HorizontalAlignment == HorizontalTextAlignment.Center) { x0 = c.X - w / 2; x1 = c.X + w / 2; }
        else if (t.HorizontalAlignment == HorizontalTextAlignment.Right) { x0 = c.X - w; x1 = c.X; }
        y0 = c.Y - h; y1 = c.Y;
        if (t.VerticalAlignment == VerticalTextAlignment.Middle) { y0 = c.Y - h / 2; y1 = c.Y + h / 2; }
        else if (t.VerticalAlignment == VerticalTextAlignment.Bottom) { y0 = c.Y; y1 = c.Y + h; }
    }
    if (bb != null) { x0 = Math.Min(x0, bb.Min.X); y0 = Math.Min(y0, bb.Min.Y); x1 = Math.Max(x1, bb.Max.X); y1 = Math.Max(y1, bb.Max.Y); }
    return new CB6Box(x0, y0, x1, y1, t.Id.Value);
}
public static List<double[]> T5Segs(List<double[]> loop)
{
    List<double[]> s = new List<double[]>();
    for (int k = 0; k < loop.Count; k++) { double[] a = loop[k], b = loop[(k + 1) % loop.Count]; s.Add(new double[] { a[0], a[1], b[0], b[1] }); }
    return s;
}
public static List<double[]> T5SegsRc(RevisionCloud rc)
{
    List<double[]> s = new List<double[]>();
    foreach (Curve c in rc.GetSketchCurves()) { XYZ a = c.GetEndPoint(0), b = c.GetEndPoint(1); s.Add(new double[] { a.X, a.Y, b.X, b.Y }); }
    return s;
}
public static bool T5In(double px, double py, List<double[]> segs)
{
    bool ins = false;
    foreach (double[] s in segs)
    {
        if ((s[1] > py) != (s[3] > py))
        {
            double xc = s[0] + (py - s[1]) * (s[2] - s[0]) / (s[3] - s[1]);
            if (px < xc) ins = !ins;
        }
    }
    return ins;
}
public static double T5BoxSegs(CB6Box b, List<double[]> segs)
{
    double mn = double.MaxValue;
    foreach (double[] s in segs)
    {
        double sx0 = Math.Min(s[0], s[2]), sx1 = Math.Max(s[0], s[2]), sy0 = Math.Min(s[1], s[3]), sy1 = Math.Max(s[1], s[3]);
        double dx = Math.Max(0, Math.Max(sx0 - b.x1, b.x0 - sx1)), dy = Math.Max(0, Math.Max(sy0 - b.y1, b.y0 - sy1));
        mn = Math.Min(mn, Math.Sqrt(dx * dx + dy * dy));
    }
    return mn;
}
public static double T5Clear(CB6Box b, List<double[]> segs)
{
    if (!T5In(b.x0, b.y0, segs) || !T5In(b.x0, b.y1, segs) || !T5In(b.x1, b.y0, segs) || !T5In(b.x1, b.y1, segs)) return -1;
    return T5BoxSegs(b, segs);
}
public static object DummyTail() { return null;
