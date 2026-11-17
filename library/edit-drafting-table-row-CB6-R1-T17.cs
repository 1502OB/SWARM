// edit-drafting-table-row: append a row to a TextNote/detail-line table, SetGeometryCurve(overrideJoins) to extend grid lines
// CB6-R1-T17 v2
// operation: edit-drafting-view-table | run 20261109-1412 | Revit 2025 | v2 (debug cycle 1) | library: none (patterns from CB6-R1-T11.v2.cs)
// v2 change: v1 rolled back at readback (extended line 15035356 unchanged). The vertical lines are now extended with
//   CurveElement.SetGeometryCurve(line, true) (overrideJoins; plain GeometryCurve set honours joins and can be ignored),
//   Regenerate, read back; any line still not at its target is deleted and recreated with NewDetailCurve (same line style)
//   and the old>new ids are reported (replaced=...). ext= in the output lists the FINAL ids for the cleanup script.
// Body of: public static object Execute(Document document, object[] parameters); send_code_to_revit transactionMode auto.
// The bridge opens one transaction; all work runs in ONE SubTransaction so any failure rolls back everything this script
// did and returns ROLLED_BACK <item>: <error>. Never saves or syncs.
// (a) FD-1 14542731: FEWD Tag = F-57, read back after Regenerate (R30).
// (b) Drafting view FEWD FIXTURE LIST AND GI SIZING (T11): measure the LIVE F-56 row (cell x, text type, width,
//     alignment), the row pitch (F-55 to F-56) and the grid lines; move everything below the F-56 bottom line (total row,
//     GI block, stamp box) down one pitch; add the 9 F-57 cells one pitch below the F-56 cells; add the F-57 bottom line
//     (same style as the F-56 bottom line, Thin Lines); extend the 8 inner and 2 outer vertical grid lines down one pitch.
//     DFU over TO GI = Yes is recomputed from the read-back cells and must stay 56.
// (c) Viewport on P0.10: keep the outline top-left fixed (move the box centre), outline must stay inside the free area
//     x 2.03..3.18 / y 0.10..1.52 ft and clear Sticky-Sheet1 (y 1.54) and the revision schedule (x 3.241).
// (d) T11 cloud: if it no longer encloses the outline with 1/4 in margin, delete it and its tag and recreate a CLOCKWISE
//     cloud (outline + 1/4 in) with one Revision Tag, head at (right - 0.17 in, top + 0.08 in), raised clear of the arcs (R25).
// parameters: [dryrun] = read-only plan; [] or [run] = do it. No literal inch marks in code: Q = (char)34.
var inv = System.Globalization.CultureInfo.InvariantCulture;
string Q = ((char)34).ToString();
bool dry = false;
if (parameters != null)
    foreach (object po in parameters)
        if (po != null && po.ToString().Trim().Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;
string TAIL0 = "\ndone=0 skipped=0 failed=0";
string VIEW_NAME = "FEWD FIXTURE LIST AND GI SIZING";
string SHEET_NO = "P0.10";
string REV_DESC = "FEWD Submittal"; long REV_ID_REF = 15034302L;
long FD_ID = 14542731L; string PNAME = "FEWD Tag"; string NEWTAG = "F-57";
long VIEW_ID_REF = 15034771L, VP_ID_REF = 15035372L, CLOUD_ID_REF = 15035379L, TAG_ID_REF = 15035390L;
string TAG_FAMILY = "MEP_Tag_Revision", TAG_TYPE = "Revision Tag";
string LINE_STYLE = "Thin Lines";
double IN = 1.0 / 12.0;
double[] FREE = new double[] { 2.03, 0.10, 3.18, 1.52 };
double REV_X = 3.241, STICKY_Y = 1.54;
int GREASE_DFU = 56;
double TOL = 0.0005;
string[] NEWROW = new string[] { "F-57", "FD", "1", "Floor drain, Level 1 (L1 FD-1)", "Level 1", "Floor drain", "4" + Q, "Yes", "0" };
List<string> warn = new List<string>();

// ---------------- 1. Document, sheet, revision ----------------
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return "NO_GO wrong document open: " + title + TAIL0;
List<ViewSheet> shs = new List<ViewSheet>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(ViewSheet)))
{
    ViewSheet s = e as ViewSheet;
    if (s != null && !s.IsTemplate && s.SheetNumber == SHEET_NO) shs.Add(s);
}
if (shs.Count != 1) return "NO_GO " + shs.Count + " sheets numbered " + SHEET_NO + TAIL0;
ViewSheet sheet = shs[0];
if (sheet.GetCategoryHidden(new ElementId(BuiltInCategory.OST_RevisionClouds))) return "NO_GO revision clouds hidden on " + SHEET_NO + TAIL0;
if (sheet.GetCategoryHidden(new ElementId(BuiltInCategory.OST_RevisionCloudTags))) return "NO_GO revision cloud tags hidden on " + SHEET_NO + TAIL0;
Revision rev = null; int revHits = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REV_DESC) { rev = r; revHits++; }
}
if (revHits != 1) return "NO_GO " + revHits + " revisions have Description '" + REV_DESC + "'" + TAIL0;
if (rev.Issued) return "NO_GO revision '" + REV_DESC + "' (" + rev.Id.Value + ") is issued" + TAIL0;
if (rev.Id.Value != REV_ID_REF) warn.Add("revision id " + rev.Id.Value + " (task " + REV_ID_REF + ")");

FamilySymbol tagSym = null; List<FamilySymbol> tagAlt = new List<FamilySymbol>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol;
    if (fs == null) continue;
    string fam = fs.Family == null ? "" : fs.Family.Name;
    if (fam == TAG_FAMILY && fs.Name == TAG_TYPE) { tagSym = fs; break; }
    if (fam == TAG_TYPE || fs.Name == TAG_TYPE) tagAlt.Add(fs);
}
if (tagSym == null && tagAlt.Count > 0) { tagAlt.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name)); tagSym = tagAlt[0]; warn.Add("tag " + TAG_FAMILY + ":" + TAG_TYPE + " not found, using " + tagSym.Id.Value); }
if (tagSym == null) return "NO_GO no Revision Cloud Tag '" + TAG_FAMILY + ":" + TAG_TYPE + "'" + TAIL0;

// ---------------- 2. FD-1 and its FEWD Tag ----------------
Element fd = document.GetElement(new ElementId(FD_ID));
if (fd == null) return "NO_GO element " + FD_ID + " (FD-1) not found" + TAIL0;
Parameter pTag = fd.LookupParameter(PNAME);
if (pTag == null) return "NO_GO " + FD_ID + " has no parameter '" + PNAME + "'" + TAIL0;
if (pTag.IsReadOnly) return "NO_GO '" + PNAME + "' on " + FD_ID + " is read-only" + TAIL0;
if (pTag.StorageType != StorageType.String) return "NO_GO '" + PNAME + "' storage " + pTag.StorageType + TAIL0;
string fdOld = pTag.AsString() ?? "";
bool paramDone = fdOld.Trim() == NEWTAG;
if (!paramDone && fdOld.Trim() != "") return "NO_GO FD-1 " + FD_ID + " FEWD Tag is already '" + T17Esc(fdOld) + "'" + TAIL0;
string fdCat = fd.Category == null ? "" : fd.Category.Name;
List<long> dupTag = new List<long>();
if (fd.Category != null)
    foreach (Element e in new FilteredElementCollector(document).OfCategory(fd.Category.BuiltInCategory).WhereElementIsNotElementType())
    {
        if (e.Id.Value == FD_ID) continue;
        Parameter pp = e.LookupParameter(PNAME);
        if (pp != null && pp.StorageType == StorageType.String && (pp.AsString() ?? "").Trim() == NEWTAG) dupTag.Add(e.Id.Value);
    }
if (dupTag.Count > 0) return "NO_GO FEWD Tag " + NEWTAG + " already used by " + string.Join(",", dupTag.Take(5)) + TAIL0;

// ---------------- 3. Drafting view: measure the live table ----------------
View fv = null; int fvc = 0;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(View)))
{
    View v = e as View;
    if (v != null && !v.IsTemplate && v.Name == VIEW_NAME) { fv = v; fvc++; }
}
if (fvc != 1) return "NO_GO " + fvc + " views named '" + VIEW_NAME + "'" + TAIL0;
if (fv.Id.Value != VIEW_ID_REF) warn.Add("view id " + fv.Id.Value + " (task " + VIEW_ID_REF + ")");
double vScale = fv.Scale < 1 ? 1.0 : (double)fv.Scale;
List<TextNote> notes = new List<TextNote>();
foreach (Element e in new FilteredElementCollector(document, fv.Id).OfClass(typeof(TextNote))) { TextNote t = e as TextNote; if (t != null) notes.Add(t); }
List<DetailCurve> dcs = new List<DetailCurve>();
foreach (Element e in new FilteredElementCollector(document, fv.Id).OfClass(typeof(CurveElement))) { DetailCurve d = e as DetailCurve; if (d != null) dcs.Add(d); }
int nNotes0 = notes.Count, nLines0 = dcs.Count;
List<TextNote> f56 = notes.Where(n => T17Norm(n.Text) == "F-56").ToList();
List<TextNote> f55 = notes.Where(n => T17Norm(n.Text) == "F-55").ToList();
List<TextNote> f57 = notes.Where(n => T17Norm(n.Text) == "F-57").ToList();
bool rowDone = false;
if (f57.Count > 1) return "NO_GO " + f57.Count + " notes read F-57 in the view" + TAIL0;
if (f57.Count == 1)
{
    List<TextNote> r57 = T17RowCells(notes, f57[0].Coord.Y);
    bool ok = r57.Count == 9;
    for (int c = 0; ok && c < 9; c++) ok = T17Norm(r57[c].Text) == T17Norm(NEWROW[c]);
    if (!ok) return "NO_GO partial F-57 row: " + r57.Count + " cells at y " + T17F(f57[0].Coord.Y) + " (" + T17Esc(string.Join("|", r57.Select(n => T17Norm(n.Text)))) + "); fix by hand or run cleanup first" + TAIL0;
    rowDone = true;
}
if (!rowDone && (f56.Count != 1 || f55.Count != 1)) return "NO_GO F-56 notes " + f56.Count + ", F-55 notes " + f55.Count + " (need 1 each)" + TAIL0;
double xF = rowDone ? f57[0].Coord.X : f56[0].Coord.X;
int[] dfu0 = T17Dfu(notes, xF, TOL);
if (dfu0[3] > 0) return "NO_GO " + dfu0[3] + " F-rows without 9 cells or with a non-integer DFU" + TAIL0;
int addDfu = NEWROW[7] == "Yes" ? int.Parse(NEWROW[8], inv) : 0;
int planDfu = rowDone ? dfu0[2] : dfu0[2] + addDfu;
if (planDfu != GREASE_DFU) return "NO_GO planned DFU over TO GI = Yes is " + planDfu + ", not " + GREASE_DFU + TAIL0;

double pitch = 0, y56 = 0, yTop = 0, yBot = 0, xL = 0, xR = 0;
List<TextNote> cells56 = new List<TextNote>();
DetailCurve lineRowBot = null, lineTblBot = null;
List<DetailCurve> innerV = new List<DetailCurve>(), outerV = new List<DetailCurve>(), shiftLines = new List<DetailCurve>();
List<TextNote> shiftNotes = new List<TextNote>();
TextNote totalNote = null; string totalText0 = "";
Dictionary<long, double[]> preSeg = new Dictionary<long, double[]>();
foreach (DetailCurve d in dcs) preSeg[d.Id.Value] = T17Seg(d);
string lsName = "";
if (!rowDone)
{
    y56 = f56[0].Coord.Y; pitch = f55[0].Coord.Y - y56;
    if (pitch < 0.10 * IN || pitch > 0.30 * IN) return "NO_GO measured row pitch " + T17F(pitch * 12) + " in (F-55 to F-56) is implausible" + TAIL0;
    cells56 = T17RowCells(notes, y56);
    if (cells56.Count != 9 || cells56[0].Id.Value != f56[0].Id.Value) return "NO_GO F-56 row has " + cells56.Count + " cells" + TAIL0;
    double x56 = f56[0].Coord.X;
    double up = double.MaxValue, dn = double.MinValue;
    foreach (DetailCurve d in dcs)
    {
        double[] s = preSeg[d.Id.Value];
        if (Math.Abs(s[1] - s[3]) > 1e-6) continue;
        if (Math.Min(s[0], s[2]) > x56 + 1e-6 || Math.Max(s[0], s[2]) < x56 - 1e-6) continue;
        if (s[1] > y56 + 1e-6 && s[1] < up) up = s[1];
        if (s[1] < y56 - 1e-6 && s[1] > dn) dn = s[1];
    }
    if (up == double.MaxValue || dn == double.MinValue) return "NO_GO no horizontal grid line above/below the F-56 row" + TAIL0;
    yTop = up; yBot = dn;
    if (Math.Abs((yTop - yBot) - pitch) > TOL) return "NO_GO F-56 row height " + T17F((yTop - yBot) * 12) + " in differs from pitch " + T17F(pitch * 12) + " in" + TAIL0;
    List<DetailCurve> atBot = new List<DetailCurve>(), atTbl = new List<DetailCurve>();
    foreach (DetailCurve d in dcs)
    {
        double[] s = preSeg[d.Id.Value];
        if (Math.Abs(s[1] - s[3]) > 1e-6) continue;
        if (Math.Min(s[0], s[2]) > x56 + 1e-6 || Math.Max(s[0], s[2]) < x56 - 1e-6) continue;
        if (Math.Abs(s[1] - yBot) < 1e-5) atBot.Add(d);
        else if (Math.Abs(s[1] - (yBot - pitch)) < 1e-5) atTbl.Add(d);
    }
    if (atBot.Count != 1 || atTbl.Count != 1) return "NO_GO lines at F-56 bottom " + atBot.Count + ", at table bottom " + atTbl.Count + " (need 1 each)" + TAIL0;
    lineRowBot = atBot[0]; lineTblBot = atTbl[0];
    double[] sT = preSeg[lineTblBot.Id.Value], sR = preSeg[lineRowBot.Id.Value];
    xL = Math.Min(sT[0], sT[2]); xR = Math.Max(sT[0], sT[2]);
    if (Math.Abs(Math.Min(sR[0], sR[2]) - xL) > TOL || Math.Abs(Math.Max(sR[0], sR[2]) - xR) > TOL) return "NO_GO F-56 bottom line x " + T17F(Math.Min(sR[0], sR[2])) + ".." + T17F(Math.Max(sR[0], sR[2])) + " differs from table bottom " + T17F(xL) + ".." + T17F(xR) + TAIL0;
    int vOdd = 0;
    foreach (DetailCurve d in dcs)
    {
        double[] s = preSeg[d.Id.Value];
        if (Math.Abs(s[0] - s[2]) > 1e-6) continue;
        if (s[0] < xL - TOL || s[0] > xR + TOL) continue;
        double lo = Math.Min(s[1], s[3]), hi = Math.Max(s[1], s[3]);
        if (hi <= yBot + 1e-6) continue;
        if (Math.Abs(lo - yBot) < 1e-5) innerV.Add(d);
        else if (Math.Abs(lo - (yBot - pitch)) < 1e-5) outerV.Add(d);
        else if (lo < yBot - 1e-6) vOdd++;
    }
    if (innerV.Count != 8 || outerV.Count != 2 || vOdd > 0) return "NO_GO vertical grid lines: inner " + innerV.Count + "/8, outer " + outerV.Count + "/2, odd " + vOdd + TAIL0;
    foreach (TextNote n in notes) { double[] b = T17TextBox(n); if (b[3] < yBot - 1e-4) shiftNotes.Add(n); }
    foreach (DetailCurve d in dcs) { double[] s = preSeg[d.Id.Value]; if (Math.Max(s[1], s[3]) < yBot - 1e-4) shiftLines.Add(d); }
    if (shiftNotes.Count != 3 || shiftLines.Count != 9) return "NO_GO below the F-56 row: " + shiftNotes.Count + " notes (expect 3: total, GI, stamp), " + shiftLines.Count + " lines (expect 9: table bottom, GI box 4, stamp box 4)" + TAIL0;
    foreach (TextNote n in shiftNotes)
        if (T17Norm(n.Text).StartsWith("TOTAL DFU TO GREASE INTERCEPTOR:") && n.Coord.Y < yBot && n.Coord.Y > yBot - pitch) totalNote = n;
    if (totalNote == null) return "NO_GO total row note not found directly below F-56" + TAIL0;
    totalText0 = T17Norm(totalNote.Text);
    if (!totalText0.EndsWith(": " + GREASE_DFU)) return "NO_GO total note reads '" + T17Esc(totalText0) + "'" + TAIL0;
    Element ls = lineRowBot.LineStyle;
    lsName = ls == null ? "" : ls.Name;
    if (lsName.IndexOf(LINE_STYLE, StringComparison.OrdinalIgnoreCase) < 0) return "NO_GO F-56 bottom line style is '" + T17Esc(lsName) + "', not " + LINE_STYLE + TAIL0;
}

// ---------------- 4. Viewport, cloud, tag on the sheet ----------------
Viewport vp = null;
foreach (ElementId vid in sheet.GetAllViewports())
{
    Viewport v = document.GetElement(vid) as Viewport;
    if (v != null && v.ViewId.Value == fv.Id.Value) vp = v;
}
if (vp == null) return "NO_GO view " + fv.Id.Value + " has no viewport on " + SHEET_NO + TAIL0;
if (vp.Id.Value != VP_ID_REF) warn.Add("viewport id " + vp.Id.Value + " (task " + VP_ID_REF + ")");
double[] o0 = T17OBox(vp.GetBoxOutline());
XYZ c0 = vp.GetBoxCenter();
RevisionCloud cl = null;
foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(RevisionCloud)))
{
    RevisionCloud rc = e as RevisionCloud;
    if (rc == null || rc.RevisionId.Value != rev.Id.Value || rc.OwnerViewId.Value != sheet.Id.Value) continue;
    double[] cb = T17CloudBox(rc);
    if (cb != null && T17Inside(o0, cb) && (cl == null || rc.Id.Value == CLOUD_ID_REF)) cl = rc;
}
if (cl == null) return "NO_GO no '" + REV_DESC + "' cloud on " + SHEET_NO + " encloses viewport " + vp.Id.Value + TAIL0;
if (cl.Id.Value != CLOUD_ID_REF) warn.Add("cloud id " + cl.Id.Value + " (task " + CLOUD_ID_REF + ")");
IndependentTag tg = null;
foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(IndependentTag)))
{
    IndependentTag it = e as IndependentTag;
    try { if (it != null && it.GetTaggedLocalElementIds().Contains(cl.Id)) { tg = it; break; } } catch (Exception) { }
}
if (tg == null) warn.Add("cloud " + cl.Id.Value + " has no tag; a new one is made");
else if (tg.Id.Value != TAG_ID_REF) warn.Add("tag id " + tg.Id.Value + " (task " + TAG_ID_REF + ")");
long clId0 = cl.Id.Value, tgId0 = tg == null ? 0L : tg.Id.Value;
double[] cb0 = T17CloudBox(cl);
XYZ head0 = tg == null ? null : tg.TagHeadPosition;
double sheetPitch = pitch / vScale;
double[] oP = rowDone ? (double[])o0.Clone() : new double[] { o0[0], o0[1] - sheetPitch, o0[2], o0[3] };
bool cloudKeepPlan = tg != null && T17Inside(T17Grow(oP, 0.25 * IN - TOL), cb0);
double[] LP = T17Layout(oP);
HashSet<long> excl = new HashSet<long>(); excl.Add(vp.Id.Value); excl.Add(clId0); if (tgId0 != 0) excl.Add(tgId0);
List<double[]> obs = new List<double[]>(); List<string> obsLbl = new List<string>(); List<string> obsSkip = new List<string>();
T17Obs(document, sheet, excl, obs, obsLbl, obsSkip);
List<string> planProb = new List<string>();
if (!T17Inside(oP, FREE)) planProb.Add("outline " + T17Box(oP) + " not inside free area " + T17Box(FREE));
if (!cloudKeepPlan) planProb = T17Check(oP, LP, null, FREE, REV_X, STICKY_Y, obs, obsLbl);

System.Text.StringBuilder rep = new System.Text.StringBuilder();
rep.Append("{\"mode\":\"").Append(dry ? "dryrun" : "run").Append("\",\"sheet\":").Append(sheet.Id.Value).Append(",\"rev\":").Append(rev.Id.Value)
   .Append(",\"view\":").Append(fv.Id.Value).Append(",\"vp\":").Append(vp.Id.Value).Append(",\"cloud0\":").Append(clId0).Append(",\"tag0\":").Append(tgId0);
rep.Append(",\"fd\":{\"id\":").Append(FD_ID).Append(",\"cat\":\"").Append(T17Esc(fdCat)).Append("\",\"old\":\"").Append(T17Esc(fdOld)).Append("\",\"action\":\"").Append(paramDone ? "skipped: already F-57" : "set F-57").Append("\"}");
rep.Append(",\"notes0\":").Append(nNotes0).Append(",\"lines0\":").Append(nLines0).Append(",\"dfu0\":[").Append(dfu0[0]).Append(",").Append(dfu0[1]).Append(",").Append(dfu0[2]).Append("],\"dfu_plan\":").Append(planDfu);
if (rowDone) rep.Append(",\"row\":\"skipped: F-57 row already in view\"");
else
{
    rep.Append(",\"pitch_in\":").Append(T17F(pitch * 12)).Append(",\"f56_y\":").Append(T17F(y56)).Append(",\"row_lines_y\":[").Append(T17F(yTop)).Append(",").Append(T17F(yBot)).Append("]");
    rep.Append(",\"plan_row_y\":").Append(T17F(y56 - pitch)).Append(",\"plan_line_y\":").Append(T17F(yBot - pitch)).Append(",\"table_x\":[").Append(T17F(xL)).Append(",").Append(T17F(xR)).Append("]");
    rep.Append(",\"cells_x\":[").Append(string.Join(",", cells56.Select(n => T17F(n.Coord.X)))).Append("],\"line_style\":\"").Append(T17Esc(lsName)).Append("\"");
    rep.Append(",\"shift_dy_in\":").Append(T17F(-pitch * 12)).Append(",\"shift_notes\":[").Append(string.Join(",", shiftNotes.Select(n => n.Id.Value.ToString()))).Append("]");
    rep.Append(",\"shift_lines\":[").Append(string.Join(",", shiftLines.Select(d => d.Id.Value.ToString()))).Append("]");
    rep.Append(",\"extend\":{\"inner\":").Append(innerV.Count).Append(",\"outer\":").Append(outerV.Count).Append("}");
}
rep.Append(",\"outline0\":").Append(T17Box(o0)).Append(",\"plan_outline\":").Append(T17Box(oP)).Append(",\"free\":").Append(T17Box(FREE));
rep.Append(",\"cloud_box0\":").Append(T17Box(cb0)).Append(",\"cloud_action\":\"").Append(cloudKeepPlan ? "keep (still encloses outline + 1/4 in)" : "recreate clockwise + new tag").Append("\"");
if (!cloudKeepPlan) rep.Append(",\"plan_cloud\":").Append(T17Box(new double[] { LP[0], LP[1], LP[2], LP[3] })).Append(",\"plan_tag_head\":[").Append(T17F(LP[4])).Append(",").Append(T17F(LP[5])).Append("]");
rep.Append(",\"obstacles\":").Append(obs.Count);
if (warn.Count > 0) rep.Append(",\"warn\":\"").Append(T17Esc(string.Join("; ", warn.Take(6)))).Append("\"");
if (planProb.Count > 0)
{
    rep.Append(",\"status\":\"NO_GO plan: ").Append(T17Esc(string.Join("; ", planProb.Take(6)))).Append("\"}");
    return "NO_GO " + rep.ToString() + TAIL0;
}
if (paramDone && rowDone && cloudKeepPlan)
{
    rep.Append(",\"status\":\"skipped: already in target state\"}");
    return rep.ToString() + "\ndone=0 skipped=3 failed=0 skipped_ids=" + FD_ID + "," + f57[0].Id.Value + "," + clId0;
}
if (dry)
{
    rep.Append(",\"status\":\"GO\"}");
    return rep.ToString() + "\ndone=0 skipped=0 failed=0 (dryrun, nothing changed)";
}

// ---------------- 5. Write (SubTransaction: all or nothing) ----------------
string item = "param";
int done = 0, skipped = 0;
List<long> newIds = new List<long>(), movedIds = new List<long>(), extIds = new List<long>(), replaced = new List<long>();
List<string> replacedTxt = new List<string>();
long cloudNew = 0, tagNew = 0;
double[] o2 = null, rectN = null, tbx = null;
int[] dfu1 = null;
XYZ thN = null;
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    if (!paramDone)
    {
        if (!pTag.Set(NEWTAG)) throw new Exception("Parameter.Set returned false");
        document.Regenerate();
        string rbTag = (fd.LookupParameter(PNAME).AsString() ?? "").Trim();
        if (rbTag != NEWTAG) throw new Exception("FEWD Tag read back after Regenerate is '" + rbTag + "'");
        done++;
    }
    else skipped++;

    if (!rowDone)
    {
        item = "shift";
        Dictionary<long, XYZ> preCoord = new Dictionary<long, XYZ>();
        List<ElementId> mv = new List<ElementId>();
        foreach (TextNote n in shiftNotes) { preCoord[n.Id.Value] = n.Coord; mv.Add(n.Id); movedIds.Add(n.Id.Value); }
        foreach (DetailCurve d in shiftLines) { mv.Add(d.Id); movedIds.Add(d.Id.Value); }
        ElementTransformUtils.MoveElements(document, mv, new XYZ(0, -pitch, 0));

        item = "cells";
        List<TextNote> newCells = new List<TextNote>();
        double hRef = 0;
        foreach (TextNote src in cells56) if (src.Height > hRef) hRef = src.Height;
        for (int c = 0; c < 9; c++)
        {
            TextNote src = cells56[c];
            TextNoteOptions o = new TextNoteOptions(src.GetTypeId());
            o.HorizontalAlignment = src.HorizontalAlignment;
            o.VerticalAlignment = src.VerticalAlignment;
            XYZ sc = src.Coord;
            TextNote tn = TextNote.Create(document, fv.Id, new XYZ(sc.X, sc.Y - pitch, sc.Z), src.Width, NEWROW[c], o);
            newCells.Add(tn); newIds.Add(tn.Id.Value);
        }

        item = "line";
        double zz = lineRowBot.GeometryCurve.GetEndPoint(0).Z;
        DetailCurve nl = document.Create.NewDetailCurve(fv, Line.CreateBound(new XYZ(xL, yBot - pitch, zz), new XYZ(xR, yBot - pitch, zz)));
        nl.LineStyle = lineRowBot.LineStyle;
        newIds.Add(nl.Id.Value);

        item = "extend";
        // step 1: SetGeometryCurve with overrideJoins = true, then Regenerate
        List<DetailCurve> extCur = new List<DetailCurve>();
        List<double[]> extTgt = new List<double[]>();
        List<double> extZ = new List<double>();
        foreach (DetailCurve d in innerV.Concat(outerV))
        {
            double[] s = preSeg[d.Id.Value];
            double[] t = (double[])s.Clone();
            if (s[1] < s[3]) t[1] = s[1] - pitch; else t[3] = s[3] - pitch;
            double z = d.GeometryCurve.GetEndPoint(0).Z;
            extCur.Add(d); extTgt.Add(t); extZ.Add(z);
            d.SetGeometryCurve(Line.CreateBound(new XYZ(t[0], t[1], z), new XYZ(t[2], t[3], z)), true);
        }
        document.Regenerate();
        // step 2 (R30): read back; any line still not at its target is deleted and recreated, same line style
        item = "extend-recreate";
        for (int i = 0; i < extCur.Count; i++)
        {
            DetailCurve d = extCur[i];
            double[] t = extTgt[i];
            if (T17SegEq(T17Seg(d), t, 1e-5)) continue;
            Element lsOld = d.LineStyle;
            long oldId = d.Id.Value;
            DetailCurve rc = document.Create.NewDetailCurve(fv, Line.CreateBound(new XYZ(t[0], t[1], extZ[i]), new XYZ(t[2], t[3], extZ[i])));
            if (lsOld != null) rc.LineStyle = lsOld;
            document.Delete(d.Id);
            extCur[i] = rc;
            replaced.Add(oldId);
            replacedTxt.Add(oldId + ">" + rc.Id.Value);
        }
        if (replaced.Count > 0) document.Regenerate();
        foreach (DetailCurve d in extCur) extIds.Add(d.Id.Value);

        // R30: read everything back after Regenerate
        item = "readback";
        foreach (TextNote n in shiftNotes)
        {
            XYZ p0 = preCoord[n.Id.Value], p1 = n.Coord;
            if (Math.Abs(p1.X - p0.X) > 1e-5 || Math.Abs(p1.Y - (p0.Y - pitch)) > 1e-5) throw new Exception("moved note " + n.Id.Value + " at y " + T17F(p1.Y) + ", expected " + T17F(p0.Y - pitch));
        }
        foreach (DetailCurve d in shiftLines)
        {
            double[] s0 = preSeg[d.Id.Value], s1 = T17Seg(d);
            if (!T17SegEq(s1, new double[] { s0[0], s0[1] - pitch, s0[2], s0[3] - pitch }, 1e-5)) throw new Exception("moved line " + d.Id.Value + " is " + T17Box(s1));
        }
        for (int i = 0; i < extCur.Count; i++)
        {
            double[] s1 = T17Seg(extCur[i]);
            if (!T17SegEq(s1, extTgt[i], 1e-5)) throw new Exception("extended line " + extCur[i].Id.Value + " is " + T17Box(s1) + ", expected " + T17Box(extTgt[i]) + " (after SetGeometryCurve(overrideJoins) and recreate)");
            if (extCur[i].LineStyle == null || extCur[i].LineStyle.Id.Value != lineRowBot.LineStyle.Id.Value) throw new Exception("extended line " + extCur[i].Id.Value + " line style differs");
        }
        if (!T17SegEq(T17Seg(lineRowBot), preSeg[lineRowBot.Id.Value], 1e-5)) throw new Exception("F-56 bottom line moved");
        double[] sn = T17Seg(nl);
        if (!T17SegEq(sn, new double[] { xL, yBot - pitch, xR, yBot - pitch }, 1e-5)) throw new Exception("new line is " + T17Box(sn));
        if (nl.LineStyle == null || nl.LineStyle.Id.Value != lineRowBot.LineStyle.Id.Value) throw new Exception("new line style differs");
        for (int c = 0; c < 9; c++)
        {
            TextNote tn = newCells[c], src = cells56[c];
            if (T17Norm(tn.Text) != T17Norm(NEWROW[c])) throw new Exception("cell " + c + " reads '" + T17Esc(T17Norm(tn.Text)) + "'");
            if (tn.GetTypeId().Value != src.GetTypeId().Value) throw new Exception("cell " + c + " text type differs from F-56");
            if (Math.Abs(tn.Coord.X - src.Coord.X) > 1e-5 || Math.Abs(tn.Coord.Y - (src.Coord.Y - pitch)) > 1e-5) throw new Exception("cell " + c + " at " + T17F(tn.Coord.X) + "," + T17F(tn.Coord.Y));
            if (hRef > 0 && tn.Height > 1.5 * hRef) throw new Exception("cell " + c + " wraps (height " + T17F(tn.Height * 12) + " in vs " + T17F(hRef * 12) + " in)");
        }
        List<TextNote> notes1 = new List<TextNote>();
        foreach (Element e in new FilteredElementCollector(document, fv.Id).OfClass(typeof(TextNote))) { TextNote t = e as TextNote; if (t != null) notes1.Add(t); }
        int nLines1 = 0;
        foreach (Element e in new FilteredElementCollector(document, fv.Id).OfClass(typeof(CurveElement))) if (e is DetailCurve) nLines1++;
        if (notes1.Count != nNotes0 + 9) throw new Exception("view has " + notes1.Count + " notes, expected " + (nNotes0 + 9));
        if (nLines1 != nLines0 + 1) throw new Exception("view has " + nLines1 + " detail lines, expected " + (nLines0 + 1));
        dfu1 = T17Dfu(notes1, xF, TOL);
        if (dfu1[3] > 0 || dfu1[0] != dfu0[0] + 1 || dfu1[2] != GREASE_DFU) throw new Exception("read-back DFU rows " + dfu1[0] + " yes " + dfu1[1] + " sum " + dfu1[2] + " bad " + dfu1[3]);
        if (T17Norm(totalNote.Text) != totalText0) throw new Exception("total note text changed");
        done += newIds.Count + movedIds.Count + extIds.Count;
    }
    else skipped++;

    item = "viewport";
    document.Regenerate();
    double[] o1 = T17OBox(vp.GetBoxOutline());
    double dxv = o0[0] - o1[0], dyv = o0[3] - o1[3];
    if (Math.Abs(dxv) > 1e-6 || Math.Abs(dyv) > 1e-6)
    {
        XYZ bc = vp.GetBoxCenter();
        vp.SetBoxCenter(new XYZ(bc.X + dxv, bc.Y + dyv, bc.Z));
        document.Regenerate();
    }
    o2 = T17OBox(vp.GetBoxOutline());
    if (Math.Abs(o2[3] - o0[3]) > TOL || Math.Abs(o2[0] - o0[0]) > TOL) throw new Exception("viewport outline " + T17Box(o2) + " top-left moved from " + T17Box(o0));
    if (!rowDone)
    {
        double grow = (o2[3] - o2[1]) - (o0[3] - o0[1]);
        if (Math.Abs(grow - sheetPitch) > 0.002) warn.Add("outline grew " + T17F(grow * 12) + " in, pitch " + T17F(sheetPitch * 12) + " in");
    }
    if (!T17Inside(o2, FREE)) throw new Exception("outline " + T17Box(o2) + " not inside free area " + T17Box(FREE));
    double[] lab = null;
    Outline lo = vp.GetLabelOutline();
    if (lo != null && !lo.IsEmpty) { double[] lb = T17OBox(lo); if (lb[2] - lb[0] > 1e-4 && lb[3] - lb[1] > 1e-4) lab = lb; }
    for (int i = 0; i < obs.Count; i++) if (T17Ov(o2, obs[i], 0)) throw new Exception("outline x " + obsLbl[i]);
    done++;

    item = "cloud";
    bool keep = tgId0 != 0 && T17Inside(T17Grow(o2, 0.25 * IN - TOL), cb0);
    if (keep) skipped++;
    else
    {
        List<ElementId> del = new List<ElementId>();
        if (tgId0 != 0) del.Add(new ElementId(tgId0));
        del.Add(new ElementId(clId0));
        document.Delete(del);
        document.Regenerate();
        double[] L2 = T17Layout(o2);
        List<string> p0 = T17Check(o2, L2, lab, FREE, REV_X, STICKY_Y, obs, obsLbl);
        if (p0.Count > 0) throw new Exception("cloud fit: " + string.Join("; ", p0.Take(5)));
        rectN = new double[] { L2[0], L2[1], L2[2], L2[3] };
        XYZ bl = new XYZ(rectN[0], rectN[1], 0), tl = new XYZ(rectN[0], rectN[3], 0), tr = new XYZ(rectN[2], rectN[3], 0), br = new XYZ(rectN[2], rectN[1], 0);
        List<Curve> curves = new List<Curve>();
        curves.Add(Line.CreateBound(bl, tl));   // CLOCKWISE: BL > TL > TR > BR > BL
        curves.Add(Line.CreateBound(tl, tr));
        curves.Add(Line.CreateBound(tr, br));
        curves.Add(Line.CreateBound(br, bl));
        RevisionCloud cloud = RevisionCloud.Create(document, sheet, rev.Id, curves);
        document.Regenerate();
        cloudNew = cloud.Id.Value;
        double area = 0;
        foreach (Curve cv in cloud.GetSketchCurves()) { XYZ a0 = cv.GetEndPoint(0), a1 = cv.GetEndPoint(1); area += a0.X * a1.Y - a1.X * a0.Y; }
        area /= 2.0;
        if (area >= 0) throw new Exception("cloud signed area " + T17F(area) + " is not negative");
        double[] rb = T17CloudBox(cloud);
        if (rb == null || Math.Abs(rb[0] - rectN[0]) > 0.005 || Math.Abs(rb[1] - rectN[1]) > 0.005 || Math.Abs(rb[2] - rectN[2]) > 0.005 || Math.Abs(rb[3] - rectN[3]) > 0.005) throw new Exception("cloud read back " + (rb == null ? "null" : T17Box(rb)));
        if (!cloud.GetSheetIds().Contains(sheet.Id)) throw new Exception("cloud GetSheetIds lacks the sheet");

        item = "tag";
        if (!tagSym.IsActive) tagSym.Activate();
        double[] tagOff0 = new double[] { -0.12 * IN, -0.12 * IN, 0.12 * IN, 0.12 * IN };
        XYZ head = new XYZ(L2[4], L2[5], 0);
        IndependentTag tag = IndependentTag.Create(document, tagSym.Id, sheet.Id, new Reference(cloud), false, TagOrientation.Horizontal, head);
        tag.TagHeadPosition = head;
        document.Regenerate();
        tagNew = tag.Id.Value;
        double clr = 0.08 * IN, step = 0.02 * IN;
        tbx = T17TagBox(tag, sheet, tagOff0);
        if (tbx[1] < rectN[3] + clr)
        {
            double extra = Math.Ceiling((rectN[3] + clr - tbx[1]) / step) * step;
            tag.TagHeadPosition = new XYZ(head.X, head.Y + extra, 0);
            document.Regenerate();
            tbx = T17TagBox(tag, sheet, tagOff0);
        }
        thN = tag.TagHeadPosition;
        if (tbx[1] < rectN[3] + clr - 0.001) throw new Exception("tag box bottom " + T17F(tbx[1]) + " within 0.08 in of cloud top " + T17F(rectN[3]));
        if (Math.Abs(thN.X - (rectN[2] - 0.17 * IN)) > 0.002) throw new Exception("tag head x " + T17F(thN.X) + " not at cloud right - 0.17 in");
        if (tbx[3] >= STICKY_Y) throw new Exception("tag top " + T17F(tbx[3]) + " >= " + STICKY_Y);
        if (tbx[2] >= REV_X) throw new Exception("tag right " + T17F(tbx[2]) + " >= " + REV_X);
        for (int i = 0; i < obs.Count; i++) if (T17Ov(tbx, obs[i], 0)) throw new Exception("tag x " + obsLbl[i]);
        if (!tag.GetTaggedLocalElementIds().Contains(cloud.Id)) throw new Exception("tag does not reference the new cloud");
        done += 2;
    }

    item = "revision schedule";
    document.Regenerate();
    if (!sheet.GetAllRevisionIds().Contains(rev.Id)) throw new Exception("sheet GetAllRevisionIds lacks " + rev.Id.Value);
    st.Commit();
}
catch (Exception ex)
{
    try { st.RollBack(); } catch (Exception) { }
    return "ROLLED_BACK " + item + ": " + T17Esc(ex.Message) + "\ndone=0 skipped=0 failed=1 (nothing changed)";
}

System.Text.StringBuilder res = new System.Text.StringBuilder();
res.Append("{\"mode\":\"run\",\"fd\":").Append(FD_ID).Append(",\"fewd_tag\":\"").Append(NEWTAG).Append("\",\"view\":").Append(fv.Id.Value).Append(",\"vp\":").Append(vp.Id.Value);
if (!rowDone)
    res.Append(",\"pitch_in\":").Append(T17F(pitch * 12)).Append(",\"row_y\":").Append(T17F(y56 - pitch)).Append(",\"dfu_readback\":[").Append(dfu1[0]).Append(",").Append(dfu1[1]).Append(",").Append(dfu1[2]).Append("]");
res.Append(",\"outline0\":").Append(T17Box(o0)).Append(",\"outline\":").Append(T17Box(o2));
if (cloudNew != 0) res.Append(",\"cloud\":").Append(cloudNew).Append(",\"cloud_rect\":").Append(T17Box(rectN)).Append(",\"tag\":").Append(tagNew).Append(",\"tag_head\":[").Append(T17F(thN.X)).Append(",").Append(T17F(thN.Y)).Append("],\"tag_box\":").Append(T17Box(tbx)).Append(",\"deleted\":[").Append(clId0).Append(",").Append(tgId0).Append("]");
else res.Append(",\"cloud\":\"kept ").Append(clId0).Append("\"");
if (replacedTxt.Count > 0) res.Append(",\"replaced_lines\":\"").Append(string.Join(",", replacedTxt)).Append("\"");
if (warn.Count > 0) res.Append(",\"warn\":\"").Append(T17Esc(string.Join("; ", warn.Take(6)))).Append("\"");
res.Append(",\"status\":\"OK\"}");
res.Append("\ndone=").Append(done).Append(" skipped=").Append(skipped).Append(" failed=0");
res.Append(" new=").Append(string.Join(",", newIds)).Append(" moved=").Append(string.Join(",", movedIds)).Append(" extended=").Append(string.Join(",", extIds));
res.Append(" replaced=").Append(replacedTxt.Count == 0 ? "none" : string.Join(",", replacedTxt));
res.Append(" cloud=").Append(cloudNew == 0 ? "kept" : cloudNew.ToString()).Append(" tag=").Append(tagNew == 0 ? "kept" : tagNew.ToString());
res.Append("\ncleanup_params=[\"fd=").Append(paramDone ? "none" : FD_ID.ToString()).Append("\",\"fdold=").Append(T17Esc(fdOld)).Append("\",\"view=").Append(fv.Id.Value)
   .Append("\",\"new=").Append(string.Join(",", newIds)).Append("\",\"moved=").Append(string.Join(",", movedIds)).Append("\",\"ext=").Append(string.Join(",", extIds))
   .Append("\",\"dy=").Append(pitch.ToString("R", inv)).Append("\",\"vp=").Append(vp.Id.Value)
   .Append("\",\"vpc=").Append(c0.X.ToString("R", inv)).Append(",").Append(c0.Y.ToString("R", inv)).Append(",").Append(c0.Z.ToString("R", inv))
   .Append("\",\"sheet=").Append(sheet.Id.Value).Append("\",\"rev=").Append(rev.Id.Value)
   .Append("\",\"cloud=").Append(cloudNew == 0 ? "none" : cloudNew.ToString()).Append("\",\"tag=").Append(tagNew == 0 ? "none" : tagNew.ToString())
   .Append("\",\"oldcloud=").Append(cb0[0].ToString("R", inv)).Append(",").Append(cb0[1].ToString("R", inv)).Append(",").Append(cb0[2].ToString("R", inv)).Append(",").Append(cb0[3].ToString("R", inv))
   .Append("\",\"oldhead=").Append(head0 == null ? "none" : head0.X.ToString("R", inv) + "," + head0.Y.ToString("R", inv)).Append("\"]");
return res.ToString();
}

// ---------------- helpers (CB6-R1-T17) ----------------
static string T17F(double v) { return Math.Round(v, 4).ToString(System.Globalization.CultureInfo.InvariantCulture); }
static string T17Box(double[] b) { return "[" + T17F(b[0]) + "," + T17F(b[1]) + "," + T17F(b[2]) + "," + T17F(b[3]) + "]"; }
static string T17Esc(string s) { return (s ?? "").Replace("\\", "\\\\").Replace(((char)34).ToString(), "\\" + ((char)34).ToString()).Replace("\r", " ").Replace("\n", " "); }
static string T17Norm(string s)
{
    string t = (s ?? "").Replace("\r\n", "\n").Replace("\r", "\n").Replace("\v", "\n").Trim();
    return t.ToUpperInvariant();
}
static double[] T17Seg(DetailCurve d)
{
    Curve g = d.GeometryCurve;
    XYZ a = g.GetEndPoint(0), b = g.GetEndPoint(1);
    return new double[] { a.X, a.Y, b.X, b.Y };
}
static bool T17SegEq(double[] a, double[] b, double t)
{
    bool same = Math.Abs(a[0] - b[0]) < t && Math.Abs(a[1] - b[1]) < t && Math.Abs(a[2] - b[2]) < t && Math.Abs(a[3] - b[3]) < t;
    bool flip = Math.Abs(a[0] - b[2]) < t && Math.Abs(a[1] - b[3]) < t && Math.Abs(a[2] - b[0]) < t && Math.Abs(a[3] - b[1]) < t;
    return same || flip;
}
static List<TextNote> T17RowCells(List<TextNote> notes, double y)
{
    return notes.Where(n => Math.Abs(n.Coord.Y - y) < 0.0005).OrderBy(n => n.Coord.X).ToList();
}
// [rows, yes rows, DFU sum over TO GI = Yes, bad rows] over notes reading F-<n> in the F-# column
static int[] T17Dfu(List<TextNote> notes, double x0, double tol)
{
    int rows = 0, yes = 0, sum = 0, bad = 0;
    foreach (TextNote n in notes)
    {
        string t = T17Norm(n.Text);
        if (!System.Text.RegularExpressions.Regex.IsMatch(t, "^F-[0-9]+$")) continue;
        if (Math.Abs(n.Coord.X - x0) > tol) continue;
        List<TextNote> r = T17RowCells(notes, n.Coord.Y);
        if (r.Count != 9) { bad++; continue; }
        rows++;
        int d;
        if (!int.TryParse(T17Norm(r[8].Text), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out d)) { bad++; continue; }
        if (T17Norm(r[7].Text) == "YES") { yes++; sum += d; }
    }
    return new int[] { rows, yes, sum, bad };
}
static bool T17Ov(double[] a, double[] b, double g) { return a[0] - g < b[2] && b[0] < a[2] + g && a[1] - g < b[3] && b[1] < a[3] + g; }
static bool T17Inside(double[] inner, double[] outer) { return inner[0] >= outer[0] - 1e-6 && inner[1] >= outer[1] - 1e-6 && inner[2] <= outer[2] + 1e-6 && inner[3] <= outer[3] + 1e-6; }
static double[] T17Grow(double[] a, double m) { return new double[] { a[0] - m, a[1] - m, a[2] + m, a[3] + m }; }
static double[] T17OBox(Outline o) { XYZ a = o.MinimumPoint, b = o.MaximumPoint; return new double[] { Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Max(a.X, b.X), Math.Max(a.Y, b.Y) }; }
static double[] T17U(double[] a, double x, double y) { return new double[] { Math.Min(a[0], x), Math.Min(a[1], y), Math.Max(a[2], x), Math.Max(a[3], y) }; }
static double[] T17CloudBox(RevisionCloud rc)
{
    double[] r = null;
    foreach (Curve cv in rc.GetSketchCurves())
        for (int k = 0; k < 2; k++) { XYZ p = cv.GetEndPoint(k); r = (r == null) ? new double[] { p.X, p.Y, p.X, p.Y } : T17U(r, p.X, p.Y); }
    return r;
}
static double[] T17TextBox(TextNote tn)
{
    XYZ c = tn.Coord, b = tn.BaseDirection, u = tn.UpDirection;
    double w = tn.Width, h = tn.Height;
    double a0 = 0, a1 = 1, b0 = -1, b1 = 0;
    if (tn.HorizontalAlignment == HorizontalTextAlignment.Right) { a0 = -1; a1 = 0; }
    else if (tn.HorizontalAlignment == HorizontalTextAlignment.Center) { a0 = -0.5; a1 = 0.5; }
    if (tn.VerticalAlignment == VerticalTextAlignment.Bottom) { b0 = 0; b1 = 1; }
    else if (tn.VerticalAlignment == VerticalTextAlignment.Middle) { b0 = -0.5; b1 = 0.5; }
    double[] r = new double[] { double.MaxValue, double.MaxValue, double.MinValue, double.MinValue };
    foreach (double ka in new double[] { a0, a1 }) foreach (double kb in new double[] { b0, b1 })
        r = T17U(r, c.X + b.X * ka * w + u.X * kb * h, c.Y + b.Y * ka * w + u.Y * kb * h);
    return r;
}
// live sheet items except the excluded ids (title block and sheet-size boxes skipped)
static void T17Obs(Document doc, ViewSheet sh, HashSet<long> excl, List<double[]> boxes, List<string> lbls, List<string> skipped)
{
    foreach (Element e in new FilteredElementCollector(doc, sh.Id).WhereElementIsNotElementType())
    {
        if (excl.Contains(e.Id.Value)) continue;
        try
        {
            if (e.Category != null && e.Category.BuiltInCategory == BuiltInCategory.OST_TitleBlocks) continue;
            double[] b = null; string l = (e.Category == null ? e.GetType().Name : e.Category.Name) + " " + e.Id.Value;
            if (e is Viewport)
            {
                Viewport v = (Viewport)e;
                b = T17OBox(v.GetBoxOutline());
                Outline lo = v.GetLabelOutline();
                if (lo != null && !lo.IsEmpty) { double[] lb = T17OBox(lo); if (lb[2] - lb[0] > 1e-4 && lb[3] - lb[1] > 1e-4) { boxes.Add(lb); lbls.Add("label of " + l); } }
            }
            else if (e is TextNote) b = T17TextBox((TextNote)e);
            else if (e is RevisionCloud) { b = T17CloudBox((RevisionCloud)e); if (b != null) b = T17Grow(b, 0.05 / 12.0); }
            else
            {
                BoundingBoxXYZ bb = e.get_BoundingBox(sh);
                if (bb == null) continue;
                b = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
            }
            if (b == null) continue;
            if (b[2] - b[0] > 2.8 && b[3] - b[1] > 2.0) { skipped.Add(l + " (sheet-size)"); continue; }
            boxes.Add(b); lbls.Add(l + " " + T17Box(b));
        }
        catch (Exception) { }
    }
}
// [0..3] cloud rect (outline + 1/4 in), [4,5] tag head (right - 0.17 in, top + 0.08 in)
static double[] T17Layout(double[] o)
{
    double IN = 1.0 / 12.0;
    double[] r = T17Grow(o, 0.25 * IN);
    return new double[] { r[0], r[1], r[2], r[3], r[2] - 0.17 * IN, r[3] + 0.08 * IN };
}
static List<string> T17Check(double[] outl, double[] L, double[] lab, double[] free, double revX, double stickyY, List<double[]> obs, List<string> lbl)
{
    double IN = 1.0 / 12.0, bulge = 0.05 * IN;
    List<string> p = new List<string>();
    if (!T17Inside(outl, free)) p.Add("outline " + T17Box(outl) + " not inside free area " + T17Box(free));
    double[] cr = T17Grow(new double[] { L[0], L[1], L[2], L[3] }, bulge);
    if (cr[3] >= stickyY) p.Add("cloud top " + T17F(cr[3]) + " >= " + stickyY);
    if (cr[2] >= revX) p.Add("cloud right " + T17F(cr[2]) + " >= " + revX);
    if (cr[1] <= 0.05) p.Add("cloud bottom " + T17F(cr[1]) + " <= 0.05");
    double[] tg = new double[] { L[4] - 0.12 * IN, L[5], L[4] + 0.12 * IN, L[5] + 0.3 * IN };
    if (tg[3] >= stickyY) p.Add("tag est top " + T17F(tg[3]) + " >= " + stickyY);
    for (int i = 0; i < obs.Count; i++)
    {
        if (T17Ov(cr, obs[i], 0)) p.Add("cloud x " + lbl[i]);
        else if (T17Ov(tg, obs[i], 0)) p.Add("tag x " + lbl[i]);
        if (lab != null && T17Ov(lab, obs[i], 0)) p.Add("vp label x " + lbl[i]);
    }
    if (lab != null && lab[1] <= 0.05) p.Add("vp label bottom " + T17F(lab[1]));
    return p;
}
static double[] T17TagBox(IndependentTag tag, View v, double[] tagOff)
{
    double IN = 1.0 / 12.0;
    XYZ h = tag.TagHeadPosition;
    BoundingBoxXYZ bb = tag.get_BoundingBox(v);
    if (bb != null)
    {
        double w = bb.Max.X - bb.Min.X, hh = bb.Max.Y - bb.Min.Y;
        if (w > 0.05 * IN && w < 0.6 * IN && hh > 0.05 * IN && hh < 0.6 * IN) return new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
    }
    return new double[] { h.X + tagOff[0], h.Y + tagOff[1], h.X + tagOff[2], h.Y + tagOff[3] };
}
public static object DummyTail() { return null;
