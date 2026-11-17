// family-dimension-tune: from CB6-R1-T6.v2 (23-073), verified
// set-family-dimension-parameter: GI LENGTH secant-tuned so read-only Holding Capacity = 200.5 CF, + cloud/delta if outline moves
// CB6-R1-T6 v1 | operation: set-family-dimension-parameter (+ revision cloud + Revision Tag) | run 20261109-1412
// parameters: ["dryrun"] = read-only; [] or ["run"] = do it. transactionMode "auto". All inside one SubTransaction (all or nothing).
// Body of: public static object Execute(Document document, object[] parameters)
System.Globalization.CultureInfo inv = System.Globalization.CultureInfo.InvariantCulture;
const long GI_ID = 14839803L;
long[] VIEW_IDS = new long[] { 12968396L, 12968502L };   // P2.10, P1.00
string[] VIEW_LBL = new string[] { "P2.10", "P1.00" };
const string REV_DESC = "FEWD Submittal";
const string TAG_FAMILY = "MEP_Tag_Revision";
const string TAG_TYPE = "Revision Tag";
double TARGET = 200.5, LO = 200.2, HI = 200.8;
double BASE_EXTRA = 0.6667;      // capacity ~ proportional to (LENGTH - 0.6667 ft), per task
string TAIL0 = "\ndone=0 skipped=0 failed=0";

bool dry = false;
if (parameters != null)
    foreach (object po in parameters)
        if (po != null && po.ToString().Trim().Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;

// ---------------- Preflight (read-only) ----------------
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return "NO_GO wrong document open: " + title + TAIL0;
FamilyInstance gi = document.GetElement(new ElementId(GI_ID)) as FamilyInstance;
if (gi == null) return "NO_GO element " + GI_ID + " is not a FamilyInstance / not found" + TAIL0;
string famName = gi.Symbol.Family.Name, typName = gi.Symbol.Name;
if (famName.IndexOf("Grease Interceptor", StringComparison.OrdinalIgnoreCase) < 0 && typName.IndexOf("Grease Interceptor", StringComparison.OrdinalIgnoreCase) < 0)
    return "NO_GO element " + GI_ID + " is " + famName + " : " + typName + TAIL0;
Parameter pL = gi.LookupParameter("LENGTH"), pW = gi.LookupParameter("WIDTH"), pH = gi.LookupParameter("HEIGHT"),
          pT = gi.LookupParameter("Wall Thickness"), pC = gi.LookupParameter("Holding Capacity");
if (pL == null || pW == null || pH == null || pT == null || pC == null)
    return "NO_GO missing parameter on GI: Length=" + (pL != null) + " Width=" + (pW != null) + " Height=" + (pH != null) + " Wall Thickness=" + (pT != null) + " Holding Capacity=" + (pC != null) + TAIL0;
if (pL.IsReadOnly) return "NO_GO parameter Length is read-only" + TAIL0;
if (pL.StorageType != StorageType.Double || pC.StorageType != StorageType.Double) return "NO_GO Length/Holding Capacity are not Double parameters" + TAIL0;
double L0 = pL.AsDouble(), C0 = pC.AsDouble(), W0 = pW.AsDouble(), H0 = pH.AsDouble(), T0 = pT.AsDouble();
string capStr0 = pC.AsValueString(), lenStr0 = pL.AsValueString();
string conn0 = T6Conns(gi);

Revision rev = null; int revHits = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REV_DESC) { rev = r; revHits++; }
}
if (revHits != 1) return "NO_GO " + revHits + " revisions have Description '" + REV_DESC + "'" + TAIL0;
if (rev.Issued) return "NO_GO revision '" + REV_DESC + "' is issued" + TAIL0;

FamilySymbol tagSym = null;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol;
    if (fs == null) continue;
    if (fs.Name == TAG_TYPE && fs.Family != null && fs.Family.Name == TAG_FAMILY) { tagSym = fs; break; }
    if (tagSym == null && (fs.Name == TAG_TYPE || (fs.Family != null && fs.Family.Name == TAG_FAMILY))) tagSym = fs;
}
if (tagSym == null) return "NO_GO no Revision Cloud Tag symbol " + TAG_FAMILY + " : " + TAG_TYPE + TAIL0;

View[] views = new View[VIEW_IDS.Length];
double[] INs = new double[VIEW_IDS.Length];
double[][] preBB = new double[VIEW_IDS.Length][];
List<double[]>[] obsCl = new List<double[]>[VIEW_IDS.Length];
List<double[]>[] obsTg = new List<double[]>[VIEW_IDS.Length];
for (int i = 0; i < VIEW_IDS.Length; i++)
{
    View v = document.GetElement(new ElementId(VIEW_IDS[i])) as View;
    if (v == null || v.IsTemplate || v.ViewType != ViewType.FloorPlan) return "NO_GO view " + VIEW_IDS[i] + " missing or not a floor plan" + TAIL0;
    if (Math.Abs(v.RightDirection.X - 1.0) > 1e-6) return "NO_GO view " + VIEW_IDS[i] + " is rotated" + TAIL0;
    foreach (BuiltInCategory bc in new BuiltInCategory[] { BuiltInCategory.OST_RevisionClouds, BuiltInCategory.OST_RevisionCloudTags })
        if (v.GetCategoryHidden(new ElementId(bc))) return "NO_GO revision cloud/tag category hidden in view " + VIEW_IDS[i] + TAIL0;
    views[i] = v; INs[i] = v.Scale / 12.0;
    BoundingBoxXYZ bb = gi.get_BoundingBox(v);
    preBB[i] = (bb == null) ? null : new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
    obsCl[i] = new List<double[]>(); obsTg[i] = new List<double[]>();
    foreach (Element e in new FilteredElementCollector(document, v.Id).WhereElementIsNotElementType())
    {
        RevisionCloud rc = e as RevisionCloud;
        if (rc != null) { double[] cbx = T6CloudBox(rc); if (cbx != null) obsCl[i].Add(cbx); continue; }
        IndependentTag it = e as IndependentTag;
        if (it != null && it.Category != null && it.Category.BuiltInCategory == BuiltInCategory.OST_RevisionCloudTags)
        {
            BoundingBoxXYZ tb = it.get_BoundingBox(v);
            if (tb != null) obsTg[i].Add(new double[] { tb.Min.X, tb.Min.Y, tb.Max.X, tb.Max.Y });
        }
    }
}
// T4 callout leader ends (P2.10)
string leaderInfo0 = T6Leaders(document, views[0], preBB[0]);

System.Text.StringBuilder rep = new System.Text.StringBuilder();
rep.Append("{\"mode\":\"").Append(dry ? "dryrun" : "run").Append("\",\"gi\":").Append(GI_ID).Append(",\"type\":\"").Append(T6Esc(famName + " : " + typName)).Append("\"");
rep.Append(",\"rev\":").Append(rev.Id.Value).Append(",\"tag_sym\":").Append(tagSym.Id.Value);
rep.Append(",\"old\":{\"L_ft\":").Append(T6F(L0)).Append(",\"L\":\"").Append(T6Esc(lenStr0)).Append("\",\"cap_cf\":").Append(T6F(C0)).Append(",\"cap\":\"").Append(T6Esc(capStr0));
rep.Append("\",\"W\":").Append(T6F(W0)).Append(",\"H\":").Append(T6F(H0)).Append(",\"wall\":").Append(T6F(T0)).Append("}");
rep.Append(",\"connectors_old\":\"").Append(T6Esc(conn0)).Append("\"");
rep.Append(",\"leaders_old\":\"").Append(T6Esc(leaderInfo0)).Append("\"");
for (int i = 0; i < views.Length; i++)
    rep.Append(",\"bb_").Append(VIEW_LBL[i]).Append("\":").Append(preBB[i] == null ? "null" : T6Box(preBB[i])).Append(",\"obs_clouds_").Append(VIEW_LBL[i]).Append("\":").Append(obsCl[i].Count);

if (C0 >= LO && C0 <= HI)
{
    rep.Append(",\"status\":\"skipped: already in target state\"}");
    return rep.ToString() + "\ndone=0 skipped=1 failed=0 skipped_ids=" + GI_ID;
}
double L1 = (L0 - BASE_EXTRA) * TARGET / C0 + BASE_EXTRA;
rep.Append(",\"planned_first_L_ft\":").Append(T6F(L1));
if (dry)
{
    // estimated cloud clearance using the current outline
    for (int i = 0; i < views.Length; i++)
    {
        if (preBB[i] == null) { rep.Append(",\"est_").Append(VIEW_LBL[i]).Append("\":\"GI not visible\""); continue; }
        double[] rc0 = T6Grow(preBB[i], 0.25 * INs[i]);
        rep.Append(",\"est_clear_").Append(VIEW_LBL[i]).Append("\":\"").Append(T6Esc(string.Join("; ", T6Clear(rc0, null, obsCl[i], obsTg[i], INs[i])))).Append("\"");
    }
    rep.Append(",\"status\":\"GO\"}");
    return rep.ToString() + "\ndone=0 skipped=0 failed=0 (dryrun, nothing changed)";
}

// ---------------- Run ----------------
string item = "length";
List<string> pairs = new List<string>();
List<string> newIds = new List<string>();
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    List<double[]> pts = new List<double[]>();
    pts.Add(new double[] { L0, C0 });
    double Ltry = L1, Cnow = C0;
    bool ok = false;
    for (int k = 0; k < 4; k++)
    {
        if (Ltry < 2.0 || Ltry > 20.0) throw new Exception("NO_GO next LENGTH " + T6F(Ltry) + " ft outside 2..20 ft; pairs: " + string.Join(" ", pairs));
        if (!pL.Set(Ltry)) throw new Exception("NO_GO Length.Set returned false at " + T6F(Ltry));
        document.Regenerate();
        Cnow = pC.AsDouble();
        pts.Add(new double[] { Ltry, Cnow });
        pairs.Add("L=" + T6F(Ltry) + "/C=" + T6F(Cnow));
        if (Cnow >= LO && Cnow <= HI) { ok = true; break; }
        double[] a = pts[pts.Count - 2], b = pts[pts.Count - 1];
        if (Math.Abs(b[1] - a[1]) < 1e-9) throw new Exception("NO_GO capacity does not change with LENGTH; pairs: " + string.Join(" ", pairs));
        Ltry = b[0] + (TARGET - b[1]) * (b[0] - a[0]) / (b[1] - a[1]);
    }
    if (!ok) throw new Exception("NO_GO range " + LO + "-" + HI + " not reached after 4 tries; pairs: L0=" + T6F(L0) + "/C0=" + T6F(C0) + " " + string.Join(" ", pairs));
    if (Math.Abs(pW.AsDouble() - W0) > 1e-9 || Math.Abs(pH.AsDouble() - H0) > 1e-9 || Math.Abs(pT.AsDouble() - T0) > 1e-9)
        throw new Exception("NO_GO Width/Height/Wall Thickness changed");
    double Lnew = pL.AsDouble(), Cnew = pC.AsDouble();
    rep.Append(",\"new\":{\"L_ft\":").Append(T6F(Lnew)).Append(",\"L\":\"").Append(T6Esc(pL.AsValueString())).Append("\",\"cap_cf\":").Append(T6F(Cnew)).Append(",\"cap\":\"").Append(T6Esc(pC.AsValueString())).Append("\"}");
    rep.Append(",\"pairs\":\"").Append(T6Esc(string.Join(" ", pairs))).Append("\"");
    rep.Append(",\"connectors_new\":\"").Append(T6Esc(T6Conns(gi))).Append("\"");
    rep.Append(",\"leaders_new\":\"").Append(T6Esc(T6Leaders(document, views[0], null))).Append("\"");

    for (int i = 0; i < views.Length; i++)
    {
        string vl = VIEW_LBL[i];
        BoundingBoxXYZ bb = gi.get_BoundingBox(views[i]);
        if (bb == null || preBB[i] == null) { rep.Append(",\"").Append(vl).Append("\":\"GI not visible, no cloud\""); continue; }
        double[] nb = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
        double tol = 0.02 * INs[i];
        double mv = Math.Max(Math.Max(Math.Abs(nb[0] - preBB[i][0]), Math.Abs(nb[1] - preBB[i][1])), Math.Max(Math.Abs(nb[2] - preBB[i][2]), Math.Abs(nb[3] - preBB[i][3])));
        rep.Append(",\"bb_new_").Append(vl).Append("\":").Append(T6Box(nb)).Append(",\"moved_").Append(vl).Append("\":").Append(T6F(mv));
        if (mv <= tol) continue;
        item = "cloud " + vl;
        double[] rect = T6Grow(nb, 0.25 * INs[i]);
        double z = bb.Min.Z;
        Level lv = document.GetElement(views[i].GenLevel == null ? ElementId.InvalidElementId : views[i].GenLevel.Id) as Level;
        XYZ bl = new XYZ(rect[0], rect[1], z), tl = new XYZ(rect[0], rect[3], z), tr = new XYZ(rect[2], rect[3], z), br = new XYZ(rect[2], rect[1], z);
        List<Curve> curves = new List<Curve>();
        curves.Add(Line.CreateBound(bl, tl)); curves.Add(Line.CreateBound(tl, tr)); curves.Add(Line.CreateBound(tr, br)); curves.Add(Line.CreateBound(br, bl));   // CLOCKWISE
        RevisionCloud cloud = RevisionCloud.Create(document, views[i], rev.Id, curves);
        document.Regenerate();
        newIds.Add(cloud.Id.Value.ToString());
        double area = 0;
        foreach (Curve cv in cloud.GetSketchCurves()) { XYZ p0 = cv.GetEndPoint(0), p1 = cv.GetEndPoint(1); area += (p0.X * p1.Y - p1.X * p0.Y); }
        area /= 2.0;
        if (area >= 0) throw new Exception("cloud signed area " + T6F(area) + " not negative");

        item = "tag " + vl;
        if (!tagSym.IsActive) tagSym.Activate();
        double IN = INs[i];
        XYZ head = new XYZ(rect[2] - 0.17 * IN, rect[3] + 0.08 * IN, z);
        IndependentTag tag = IndependentTag.Create(document, tagSym.Id, views[i].Id, new Reference(cloud), false, TagOrientation.Horizontal, head);
        tag.TagHeadPosition = head;
        document.Regenerate();
        newIds.Add(tag.Id.Value.ToString());
        double[] tbx = T6TagBox(tag, views[i], IN);
        if (tbx[1] < rect[3] - 0.01 * IN)
        {
            XYZ h1 = tag.TagHeadPosition;
            tag.TagHeadPosition = new XYZ(h1.X, h1.Y + (rect[3] - tbx[1]), h1.Z);
            document.Regenerate();
            tbx = T6TagBox(tag, views[i], IN);
        }
        List<double[]> ownObsCl = new List<double[]>();
        foreach (double[] oc in obsCl[i]) ownObsCl.Add(oc);
        List<string> cl = T6Clear(rect, tbx, ownObsCl, obsTg[i], IN);
        if (cl.Count > 0) throw new Exception("NO_GO cloud/tag clearance in " + vl + ": " + string.Join("; ", cl) + " (length change rolled back too)");
        rep.Append(",\"cloud_").Append(vl).Append("\":").Append(cloud.Id.Value).Append(",\"tag_").Append(vl).Append("\":").Append(tag.Id.Value);
        rep.Append(",\"rect_").Append(vl).Append("\":").Append(T6Box(rect)).Append(",\"area_").Append(vl).Append("\":").Append(T6F(area));
    }
    st.Commit();
}
catch (Exception ex)
{
    try { st.RollBack(); } catch (Exception) { }
    string m = T6Esc(ex.Message);
    if (m.StartsWith("NO_GO")) return m + "\ndone=0 skipped=0 failed=1 (rolled back, nothing changed)";
    return "ROLLED_BACK " + item + ": " + m + "\ndone=0 skipped=0 failed=1 (nothing changed; pairs: " + T6Esc(string.Join(" ", pairs)) + ")";
}
rep.Append(",\"status\":\"OK\",\"restore_for_cleanup\":\"restoreLength=").Append(L0.ToString("R", inv)).Append("\"}");
rep.Append("\ndone=").Append(1 + newIds.Count).Append(" skipped=0 failed=0 gi=").Append(GI_ID).Append(" created=").Append(string.Join(",", newIds));
return rep.ToString();
}

// ---------------- helpers (CB6-R1-T6) ----------------
static string T6F(double v) { return Math.Round(v, 4).ToString(System.Globalization.CultureInfo.InvariantCulture); }
static string T6Box(double[] b) { return "[" + T6F(b[0]) + "," + T6F(b[1]) + "," + T6F(b[2]) + "," + T6F(b[3]) + "]"; }
static string T6Esc(string s) { return (s ?? "").Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", " ").Replace("\n", " "); }
static double[] T6Grow(double[] a, double m) { return new double[] { a[0] - m, a[1] - m, a[2] + m, a[3] + m }; }
static bool T6Ov(double[] a, double[] b, double g) { return a[0] - g < b[2] && b[0] < a[2] + g && a[1] - g < b[3] && b[1] < a[3] + g; }
static double[] T6CloudBox(RevisionCloud rc)
{
    double[] r = null;
    foreach (Curve cv in rc.GetSketchCurves())
        for (int k = 0; k < 2; k++)
        {
            XYZ p = cv.GetEndPoint(k);
            r = (r == null) ? new double[] { p.X, p.Y, p.X, p.Y } : new double[] { Math.Min(r[0], p.X), Math.Min(r[1], p.Y), Math.Max(r[2], p.X), Math.Max(r[3], p.Y) };
        }
    return r;
}
static double[] T6TagBox(IndependentTag tag, View view, double IN)
{
    XYZ h = tag.TagHeadPosition;
    BoundingBoxXYZ bb = tag.get_BoundingBox(view);
    if (bb != null)
    {
        double w = bb.Max.X - bb.Min.X, hh = bb.Max.Y - bb.Min.Y;
        if (w > 0.05 * IN && w < 0.6 * IN && hh > 0.05 * IN && hh < 0.6 * IN) return new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
    }
    return new double[] { h.X - 0.12 * IN, h.Y - 0.12 * IN, h.X + 0.12 * IN, h.Y + 0.12 * IN };
}
// clearance: new cloud (+0.05 in bulge) must be >= 1/8 in paper from every existing cloud; new tag/cloud must not overlap other tags/clouds
static List<string> T6Clear(double[] rect, double[] tagBox, List<double[]> clouds, List<double[]> tags, double IN)
{
    List<string> r = new List<string>();
    double[] cr = T6Grow(rect, 0.05 * IN);
    for (int i = 0; i < clouds.Count; i++)
    {
        if (T6Ov(cr, clouds[i], 0.125 * IN)) r.Add("cloud within 1/8 in of existing cloud " + T6Box(clouds[i]));
        if (tagBox != null && T6Ov(tagBox, clouds[i], 0)) r.Add("tag overlaps existing cloud " + T6Box(clouds[i]));
    }
    for (int i = 0; i < tags.Count; i++)
    {
        if (T6Ov(cr, tags[i], 0)) r.Add("cloud overlaps existing tag " + T6Box(tags[i]));
        if (tagBox != null && T6Ov(tagBox, tags[i], 0)) r.Add("tag overlaps existing tag " + T6Box(tags[i]));
    }
    return r;
}
static string T6Conns(FamilyInstance fi)
{
    List<string> l = new List<string>();
    try
    {
        foreach (Connector c in fi.MEPModel.ConnectorManager.Connectors)
        {
            string role = (c.Id == 1) ? "inlet" : (c.Id == 2) ? "outlet" : (c.Id == 3) ? "vent" : "other";
            l.Add(c.Id + "(" + role + ")=(" + T6F(c.Origin.X) + "," + T6F(c.Origin.Y) + "," + T6F(c.Origin.Z) + ")");
        }
    }
    catch (Exception e) { l.Add("err " + e.Message); }
    l.Sort(string.CompareOrdinal);
    return string.Join(" ", l);
}
// T4 callout leader ends vs GI bbox in view (bb null = read live bbox is not available here, report ends only)
static string T6Leaders(Document doc, View v, double[] bb)
{
    List<string> l = new List<string>();
    foreach (Element e in new FilteredElementCollector(doc, v.Id).OfClass(typeof(TextNote)))
    {
        TextNote tn = (TextNote)e;
        if ((tn.Text ?? "").ToUpperInvariant().IndexOf("GREASE INTERCEPTOR") < 0) continue;
        foreach (Leader ld in tn.GetLeaders())
            l.Add("note " + tn.Id.Value + " end=(" + T6F(ld.End.X) + "," + T6F(ld.End.Y) + ")");
    }
    Element gi = doc.GetElement(new ElementId(14839803L));
    BoundingBoxXYZ g = (gi == null) ? null : gi.get_BoundingBox(v);
    if (g != null) l.Add("GI bbox=[" + T6F(g.Min.X) + "," + T6F(g.Min.Y) + "," + T6F(g.Max.X) + "," + T6F(g.Max.Y) + "]");
    return string.Join(" ", l);
}
public static object DummyTail() { return null;
