// Remove FUTURE from generator gas notes + schedule header cells, create revision, cloud+tag on sheets (text-note view->sheet projection) - WF-R1-T1 (25-061)
// text-schedule-edit-with-revision-cloud: 2 TextNotes + 1 schedule header row edit, new revision, 3 sheet clouds + delta tags
// WF-R1-T1 v2 | run 20261116-1839 | Revit 2024 (members present in 2024 and 2025) | library: pattern from CB6-R1-T5 (cloud/tag), CB6-R1-T1 (revision)
// v2 change: textRect maps the note through the view frame (Origin/RightDirection/UpDirection) before the viewport transform,
//            tries 3 mappings and keeps the first whose point lies inside vp.GetBoxOutline(); cloud rects are asserted inside the title block.
// Body of: public static object Execute(Document document, object[] parameters)
// send_code_to_revit transactionMode "none": own TransactionGroup + one Transaction. Never saves or syncs.
// parameters: ["dryrun"] = does everything inside the group, commits the Transaction, reads back, then ROLLS BACK the group
//                          (P71 R47: the dryrun reaches commit). Nothing stays in the model.
//             []         = same, then Assimilate the group when every read-back item matches (else roll back, FAILED).
// Real-run return carries cleanup params ("tag:<id>", "cloud:<id>", "rev:<id>" only if created, "old1:<b64>", "old2:<b64>").
// Return: body is capped, the tail (cleanup_params + done/skipped/failed) is built first and never cut.
var inv = System.Globalization.CultureInfo.InvariantCulture;
string Q = ((char)34).ToString();
bool dry = false;
if (parameters != null)
    foreach (object po in parameters)
        if (po != null && po.ToString().Trim().Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;
string TAIL0 = "\ndone=0 skipped=0 failed=0";
long TN1 = 3211028L, TN2 = 3233584L, SCHV = 3236189L, SCHI = 3236203L, SH0 = 3090590L, SH1 = 3055912L, SH2 = 3204515L, VW1 = 3015701L, VW2 = 3205752L;
string REVDESC = "CDPv4 SUBMITTAL", REVDATE = "11-16-2026";
string NEW1 = "CONTINUATION TO NATURAL GAS GENERATOR PAD";
string NEW2 = "2" + Q + "MPG-UG (2 PSI)\rTO NATURAL GAS GENERATOR. PROVIDE SHUT-OFF VALVE AND SEDIMENT TRAP ABOVE GRADE AT GENERATOR CONNECTION PER CPC AND GENERATOR MANUFACTURER'S REQUIREMENTS.\rGENERATOR LOAD = 2807 MBH @ 350' TDL";
string CELL_OLD = "Future TI Nat. Gas Generator", CELL_NEW = "Nat. Gas Generator", EST_OLD = "Estimated Future load";
double MARGIN = 0.25 / 12.0;      // 1/4 in paper (sheet coordinates are paper feet)
double ROWMARGIN_Y = 0.003;       // schedule row band: cloud sits on the row lines
List<string> warn = new List<string>();
System.Text.StringBuilder sb = new System.Text.StringBuilder();
Func<string, string> norm = s => System.Text.RegularExpressions.Regex.Replace(s ?? "", @"\s+", " ").Trim().ToUpperInvariant();
Func<string, string> fail = m => "FAILED WF-R1-T1: " + m + TAIL0;
Func<double, string> f3 = v => Math.Round(v, 3).ToString(inv);
Func<double[], string> bx = b => "(" + f3(b[0]) + "," + f3(b[1]) + ")-(" + f3(b[2]) + "," + f3(b[3]) + ")";
Func<double[], double[], bool> ov = (a, b) => a[0] < b[2] - 1e-6 && b[0] < a[2] - 1e-6 && a[1] < b[3] - 1e-6 && b[1] < a[3] - 1e-6;

// ---------- 0. document, elements
string title = document.Title ?? "";
if (title.IndexOf("25-061_Whole-Foods-Market_AO_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return fail("wrong document open: " + title);
TextNote tn1 = document.GetElement(new ElementId(TN1)) as TextNote;
TextNote tn2 = document.GetElement(new ElementId(TN2)) as TextNote;
ViewSchedule vs = document.GetElement(new ElementId(SCHV)) as ViewSchedule;
ScheduleSheetInstance ssi = document.GetElement(new ElementId(SCHI)) as ScheduleSheetInstance;
ViewSheet sh0 = document.GetElement(new ElementId(SH0)) as ViewSheet;
ViewSheet sh1 = document.GetElement(new ElementId(SH1)) as ViewSheet;
ViewSheet sh2 = document.GetElement(new ElementId(SH2)) as ViewSheet;
if (tn1 == null) return fail("TextNote " + TN1 + " not found");
if (tn2 == null) return fail("TextNote " + TN2 + " not found");
if (vs == null) return fail("ViewSchedule " + SCHV + " not found");
if (ssi == null) return fail("ScheduleSheetInstance " + SCHI + " not found");
if (sh0 == null || sh1 == null || sh2 == null) return fail("a target sheet is missing (P0.02 " + SH0 + ", P1.00 " + SH1 + ", P1.01 " + SH2 + ")");
if (tn1.OwnerViewId.Value != VW1) return fail("TextNote " + TN1 + " owner view " + tn1.OwnerViewId.Value + " (expected " + VW1 + ")");
if (tn2.OwnerViewId.Value != VW2) return fail("TextNote " + TN2 + " owner view " + tn2.OwnerViewId.Value + " (expected " + VW2 + ")");
if (ssi.ScheduleId.Value != SCHV) return fail("instance " + SCHI + " shows schedule " + ssi.ScheduleId.Value);
if (ssi.OwnerViewId.Value != SH0) return fail("instance " + SCHI + " is on sheet " + ssi.OwnerViewId.Value);
if (sh0.SheetNumber != "P0.02" || sh1.SheetNumber != "P1.00" || sh2.SheetNumber != "P1.01") warn.Add("sheet numbers " + sh0.SheetNumber + "/" + sh1.SheetNumber + "/" + sh2.SheetNumber);
sb.Append(dry ? "DRYRUN WF-R1-T1 v2" : "RUN WF-R1-T1 v2").Append(" doc=").Append(title);

// ---------- 1. worksharing (R42)
List<string> chk = new List<string>();
if (document.IsWorkshared)
{
    long[] cid = new long[] { TN1, TN2, SCHV, SCHI, SH0, SH1, SH2 };
    List<string> owned = new List<string>();
    foreach (long id in cid)
    {
        CheckoutStatus cs = WorksharingUtils.GetCheckoutStatus(document, new ElementId(id));
        chk.Add(id + ":" + cs.ToString());
        if (cs == CheckoutStatus.OwnedByOtherUser)
        {
            string ow = "?";
            try { ow = WorksharingUtils.GetWorksharingTooltipInfo(document, new ElementId(id)).Owner; } catch (Exception) { }
            owned.Add(id + " owned by " + ow);
        }
    }
    if (owned.Count > 0) return fail(string.Join("; ", owned));
}
sb.Append("\ncheckout: ").Append(chk.Count > 0 ? string.Join(" ", chk) : "not workshared");

// ---------- 2. text notes: current state
string cur1 = tn1.Text ?? "", cur2 = tn2.Text ?? "";
string n1 = norm(cur1), n2 = norm(cur2);
string st1 = n1 == norm(NEW1) ? "done" : (n1.Contains("FUTURE NATURAL GAS GENERATOR UNDER SEPERATE PERMIT") ? "todo" : "bad");
string st2 = n2 == norm(NEW2) ? "done" : (n2.Contains("TO FUTURE GENERATOR PAD") ? "todo" : "bad");
if (st1 == "bad") return fail("TextNote " + TN1 + " text is not the expected old text: [" + cur1.Replace("\r", " / ") + "]");
if (st2 == "bad") return fail("TextNote " + TN2 + " text is not the expected old text: [" + cur2.Replace("\r", " / ") + "]");
int leaders1 = 0, leaders2 = 0;
try { leaders1 = tn1.GetLeaders().Count; leaders2 = tn2.GetLeaders().Count; } catch (Exception) { }
sb.Append("\nTN1 ").Append(TN1).Append(" ").Append(st1).Append(" leaders=").Append(leaders1).Append(" old=[").Append(CapS(cur1.Replace("\r", " / "), 110)).Append("]");
sb.Append("\nTN2 ").Append(TN2).Append(" ").Append(st2).Append(" leaders=").Append(leaders2).Append(" old=[").Append(CapS(cur2.Replace("\r", " / "), 110)).Append("]");

// ---------- 3. schedule header: find the row by reading cells (never by index)
TableSectionData hd = vs.GetTableData().GetSectionData(SectionType.Header);
int r0 = hd.FirstRowNumber, r1 = hd.LastRowNumber, c0 = hd.FirstColumnNumber, c1 = hd.LastColumnNumber;
Dictionary<string, string> cellsBefore = new Dictionary<string, string>();
int genRow = -1, genCol = -1, estCol = -1, nGen = 0;
string genState = "";
System.Text.StringBuilder hdr = new System.Text.StringBuilder();
for (int r = r0; r <= r1; r++)
{
    List<string> rowTx = new List<string>();
    for (int c = c0; c <= c1; c++)
    {
        string raw = hd.GetCellText(r, c) ?? "";
        cellsBefore[r + "," + c] = raw;
        rowTx.Add(raw);
        string nt = norm(raw);
        if (nt == norm(CELL_OLD)) { nGen++; genRow = r; genCol = c; genState = "todo"; }
        else if (nt == norm(CELL_NEW)) { nGen++; genRow = r; genCol = c; genState = "done"; }
        if (nt == norm(EST_OLD) && estCol < 0) estCol = c;
    }
    hdr.Append("r").Append(r).Append(" ").Append(string.Join("|", rowTx)).Append("; ");
}
if (nGen != 1) return fail("expected exactly one header cell 'Future TI Nat. Gas Generator' / 'Nat. Gas Generator', found " + nGen + ". Header: " + hdr.ToString());
if (estCol < 0) return fail("no header cell 'Estimated Future load' found to locate the column. Header: " + hdr.ToString());
string estRaw = cellsBefore[genRow + "," + estCol];
string estState = norm(estRaw) == norm(EST_OLD) ? "todo" : (estRaw.Trim().Length == 0 ? "done" : "bad");
if (estState == "bad") return fail("cell (" + genRow + "," + estCol + ") in the generator row reads [" + estRaw + "], not 'Estimated Future load'");
sb.Append("\nSCH ").Append(SCHV).Append(" header rows ").Append(r0).Append("..").Append(r1).Append(" cols ").Append(c0).Append("..").Append(c1)
  .Append(" genRow=").Append(genRow).Append(" genCol=").Append(genCol).Append(" estCol=").Append(estCol).Append(" gen=").Append(genState).Append(" est=").Append(estState);
sb.Append("\nhdr: ").Append(CapS(hdr.ToString(), 300));

// ---------- 4. viewports and transforms
Viewport vp1 = null, vp2 = null;
foreach (Element e in new FilteredElementCollector(document, sh1.Id).OfClass(typeof(Viewport))) { Viewport v = e as Viewport; if (v != null && v.ViewId.Value == VW1) vp1 = v; }
foreach (Element e in new FilteredElementCollector(document, sh2.Id).OfClass(typeof(Viewport))) { Viewport v = e as Viewport; if (v != null && v.ViewId.Value == VW2) vp2 = v; }
if (vp1 == null) return fail("no viewport of view " + VW1 + " on P1.00");
if (vp2 == null) return fail("no viewport of view " + VW2 + " on P1.01");
sb.Append("\nVP1 ").Append(vp1.Id.Value).Append(" rot=").Append(vp1.Rotation.ToString()).Append(" VP2 ").Append(vp2.Id.Value).Append(" rot=").Append(vp2.Rotation.ToString());

// ---------- 5. revision lookup
Revision rev = null, lastRev = null; int nRevHit = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision rr = document.GetElement(rid) as Revision; if (rr == null) continue;
    if ((rr.Description ?? "").Trim() == REVDESC) { nRevHit++; if (rev == null) rev = rr; }
    if (lastRev == null || rr.SequenceNumber > lastRev.SequenceNumber) lastRev = rr;
}
if (nRevHit > 1) return fail(nRevHit + " revisions already have Description '" + REVDESC + "'");
if (rev != null && rev.Issued) return fail("revision '" + REVDESC + "' (id " + rev.Id.Value + ") is issued");
if (rev != null && document.IsWorkshared && WorksharingUtils.GetCheckoutStatus(document, rev.Id) == CheckoutStatus.OwnedByOtherUser)
{
    string rwo = "?";
    try { rwo = WorksharingUtils.GetWorksharingTooltipInfo(document, rev.Id).Owner; } catch (Exception) { }
    return fail("revision " + rev.Id.Value + " owned by " + rwo);
}
if (rev == null && lastRev == null) return fail("no revisions in the model");
sb.Append("\nrevision: ").Append(rev != null ? "exists id " + rev.Id.Value + " seq " + rev.SequenceNumber : "to create after seq " + lastRev.SequenceNumber + " '" + lastRev.Description + "'");
if (rev == null && (lastRev.Description ?? "").IndexOf("2nd P.C SUBMITTAL SET", StringComparison.OrdinalIgnoreCase) < 0) warn.Add("last revision is '" + lastRev.Description + "', not '2nd P.C SUBMITTAL SET'");

// ---------- 6. revision tag type
FamilySymbol tagSym = null; List<string> tagNames = new List<string>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol; if (fs == null) continue;
    tagNames.Add(fs.FamilyName + ":" + fs.Name);
    if ((fs.FamilyName == "Revision Tag" || fs.Name == "Revision Tag") && (tagSym == null || fs.Id.Value < tagSym.Id.Value)) tagSym = fs;
}
if (tagSym == null) return fail("no 'Revision Tag' symbol in Revision Cloud Tags; found: " + string.Join(", ", tagNames.Take(6)));
foreach (ViewSheet shx in new ViewSheet[] { sh0, sh1, sh2 })
{
    if (shx.GetCategoryHidden(new ElementId(BuiltInCategory.OST_RevisionClouds))) return fail("revision clouds hidden on " + shx.SheetNumber);
    if (shx.GetCategoryHidden(new ElementId(BuiltInCategory.OST_RevisionCloudTags))) return fail("revision cloud tags hidden on " + shx.SheetNumber);
}
double spIn = 0.125;
try { spIn = RevisionSettings.GetRevisionSettings(document).RevisionCloudSpacing * 12.0; } catch (Exception) { warn.Add("RevisionCloudSpacing unreadable; 0.125 in assumed"); }
double gapReq = Math.Max(0.06, 0.5 * spIn + 0.03) / 12.0;   // tag bottom above cloud top line: arc bulge + 0.03 in
sb.Append("\ntag_type=").Append(tagSym.Id.Value).Append(" '").Append(tagSym.FamilyName).Append(":").Append(tagSym.Name).Append("' tag_gap_ft=").Append(f3(gapReq));

// ---------- 7. helpers
// text rectangle on the sheet from insertion point, Width/Height (paper ft), alignment; {x0,y0,x1,y1,w,h}
// v2: model point -> view frame (Origin/Right/Up, valid for plan and section) -> sheet, checked against the viewport box outline.
List<string> mapInfo = new List<string>();
Func<TextNote, Viewport, double[]> textRect = (tn, vp) =>
{
    View vw = document.GetElement(vp.ViewId) as View;
    if (vw == null) throw new Exception("viewport view not found");
    Outline ol = vp.GetBoxOutline();
    double tol = 0.05;
    double ox0 = ol.MinimumPoint.X - tol, oy0 = ol.MinimumPoint.Y - tol, ox1 = ol.MaximumPoint.X + tol, oy1 = ol.MaximumPoint.Y + tol;
    XYZ R = vw.RightDirection, U = vw.UpDirection, O = vw.Origin;
    XYZ P = tn.Coord;
    double px = (P - O).DotProduct(R), py = (P - O).DotProduct(U);
    XYZ bdm = tn.BaseDirection, udm = tn.UpDirection;
    double[] bl = new double[] { bdm.DotProduct(R), bdm.DotProduct(U) }, ul = new double[] { udm.DotProduct(R), udm.DotProduct(U) };
    Transform T = vp.GetProjectionToSheetTransform();
    BoundingBoxXYZ cb = vw.CropBox;
    Transform ct = cb.Transform;
    XYZ cc = ct.OfPoint((cb.Min + cb.Max) * 0.5);
    double cx = (cc - O).DotProduct(R), cy = (cc - O).DotProduct(U);
    XYZ pl = ct.Inverse.OfPoint(P);
    XYZ bc = vp.GetBoxCenter();
    double scale = Math.Max(1, vw.Scale);
    Func<double, double, double[], double[]> dirOf = (ax, ay, dummy) => { double L = Math.Sqrt(ax * ax + ay * ay); if (L < 1e-9) L = 1; return new double[] { ax / L, ay / L }; };
    // candidates: {sheetX, sheetY, bdx, bdy, udx, udy}
    List<string> names = new List<string>(); List<double[]> cands = new List<double[]>();
    {
        XYZ s = T.OfPoint(new XYZ(px, py, 0));
        XYZ b2 = T.OfVector(new XYZ(bl[0], bl[1], 0)), u2 = T.OfVector(new XYZ(ul[0], ul[1], 0));
        double[] bn = dirOf(b2.X, b2.Y, null), un = dirOf(u2.X, u2.Y, null);
        names.Add("T(view-frame)"); cands.Add(new double[] { s.X, s.Y, bn[0], bn[1], un[0], un[1] });
    }
    {
        XYZ s = T.OfPoint(new XYZ(pl.X, pl.Y, 0));
        XYZ b2 = T.OfVector(new XYZ(bl[0], bl[1], 0)), u2 = T.OfVector(new XYZ(ul[0], ul[1], 0));
        double[] bn = dirOf(b2.X, b2.Y, null), un = dirOf(u2.X, u2.Y, null);
        names.Add("T(crop-local)"); cands.Add(new double[] { s.X, s.Y, bn[0], bn[1], un[0], un[1] });
    }
    {
        double[] bn = dirOf(bl[0], bl[1], null), un = dirOf(ul[0], ul[1], null);
        names.Add("center+delta/scale"); cands.Add(new double[] { bc.X + (px - cx) / scale, bc.Y + (py - cy) / scale, bn[0], bn[1], un[0], un[1] });
    }
    int pick = -1; List<string> tried = new List<string>();
    for (int i = 0; i < cands.Count; i++)
    {
        double[] c = cands[i]; tried.Add(names[i] + "=(" + f3(c[0]) + "," + f3(c[1]) + ")");
        if (c[0] >= ox0 && c[0] <= ox1 && c[1] >= oy0 && c[1] <= oy1) { pick = i; break; }
    }
    if (pick < 0) throw new Exception("note " + tn.Id.Value + " maps outside viewport outline " + bx(new double[] { ox0, oy0, ox1, oy1 }) + " by every method: " + string.Join(" ", tried));
    mapInfo.Add("note " + tn.Id.Value + " " + names[pick] + " outline=" + bx(new double[] { ox0 + tol, oy0 + tol, ox1 - tol, oy1 - tol }) + " tried " + string.Join(" ", tried));
    double[] cs = cands[pick];
    double w = tn.Width, h = tn.Height;
    if (h < 0.003 || h > 3.0)
    {
        double size = 0.0; Element ty = document.GetElement(tn.GetTypeId());
        if (ty != null) { Parameter ps = ty.get_Parameter(BuiltInParameter.TEXT_SIZE); if (ps != null) size = ps.AsDouble(); }
        int lines = Math.Max(1, (tn.Text ?? "").Split('\r').Length);
        h = Math.Max(size, 0.01) * 1.3 * lines; warn.Add("TextNote " + tn.Id.Value + " Height unreadable; estimated " + f3(h));
    }
    double a0 = 0, a1 = w, b0 = -h, b1 = 0;
    if (tn.HorizontalAlignment == HorizontalTextAlignment.Center) { a0 = -w / 2; a1 = w / 2; }
    else if (tn.HorizontalAlignment == HorizontalTextAlignment.Right) { a0 = -w; a1 = 0; }
    if (tn.VerticalAlignment == VerticalTextAlignment.Middle) { b0 = -h / 2; b1 = h / 2; }
    else if (tn.VerticalAlignment == VerticalTextAlignment.Bottom) { b0 = 0; b1 = h; }
    double[] xs = new double[4], ys = new double[4]; double[] aa = new double[] { a0, a1, a1, a0 }, bb = new double[] { b0, b0, b1, b1 };
    for (int i = 0; i < 4; i++) { xs[i] = cs[0] + cs[2] * aa[i] + cs[4] * bb[i]; ys[i] = cs[1] + cs[3] * aa[i] + cs[5] * bb[i]; }
    return new double[] { xs.Min() - MARGIN, ys.Min() - MARGIN, xs.Max() + MARGIN, ys.Max() + MARGIN, w, h };
};
// title block extents on a sheet (union of title block instance bboxes); null when none found
Func<ViewSheet, double[]> tbExt = sh =>
{
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue; bool any = false;
    foreach (Element e in new FilteredElementCollector(document, sh.Id).OfCategory(BuiltInCategory.OST_TitleBlocks).WhereElementIsNotElementType())
    {
        BoundingBoxXYZ bb = e.get_BoundingBox(sh); if (bb == null) continue;
        a = Math.Min(a, bb.Min.X); b = Math.Min(b, bb.Min.Y); c = Math.Max(c, bb.Max.X); d = Math.Max(d, bb.Max.Y); any = true;
    }
    return any ? new double[] { a, b, c, d } : null;
};
Func<RevisionCloud, double[]> cloudBox = rc =>
{
    IList<Curve> cs = rc.GetSketchCurves(); if (cs == null || cs.Count == 0) return null;
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
    foreach (Curve cv in cs) for (int k = 0; k < 2; k++) { XYZ p = cv.GetEndPoint(k); a = Math.Min(a, p.X); b = Math.Min(b, p.Y); c = Math.Max(c, p.X); d = Math.Max(d, p.Y); }
    return new double[] { a, b, c, d };
};
Func<RevisionCloud, double> cloudArea = rc =>
{
    double s = 0; foreach (Curve c in rc.GetSketchCurves()) { XYZ a = c.GetEndPoint(0), b = c.GetEndPoint(1); s += a.X * b.Y - b.X * a.Y; }
    return 0.5 * s;
};
// other things on a sheet: {x0,y0,x1,y1,kind} kind 1 cloud, 2 revision tag, 3 schedule instance
Func<ViewSheet, HashSet<long>, List<double[]>> obstacles = (sh, excl) =>
{
    List<double[]> res = new List<double[]>();
    foreach (Element e in new FilteredElementCollector(document, sh.Id).WhereElementIsNotElementType())
    {
        if (excl.Contains(e.Id.Value)) continue;
        RevisionCloud rc = e as RevisionCloud;
        if (rc != null) { double[] b = cloudBox(rc); if (b != null) res.Add(new double[] { b[0], b[1], b[2], b[3], 1 }); continue; }
        IndependentTag it = e as IndependentTag;
        if (it != null && it.Category != null && it.Category.BuiltInCategory == BuiltInCategory.OST_RevisionCloudTags)
        { BoundingBoxXYZ bb = it.get_BoundingBox(sh); if (bb != null) res.Add(new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, 2 }); continue; }
        ScheduleSheetInstance si = e as ScheduleSheetInstance;
        if (si != null) { BoundingBoxXYZ bb = si.get_BoundingBox(sh); if (bb != null) res.Add(new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, 3 }); }
    }
    return res;
};

// ---------- 8. planning rectangle that does not depend on the edit (schedule row band)
double[] schBox = null; string schNote = "";
BoundingBoxXYZ sbb = ssi.get_BoundingBox(sh0);
double[] rowH = new double[r1 - r0 + 1]; double sumH = 0;
for (int r = r0; r <= r1; r++) { rowH[r - r0] = hd.GetRowHeight(r); sumH += rowH[r - r0]; }
double sumW = 0; for (int c = c0; c <= c1; c++) sumW += hd.GetColumnWidth(c);
double tx0, ty1;
if (sbb != null) { tx0 = sbb.Min.X; ty1 = sbb.Max.Y; } else { tx0 = ssi.Point.X; ty1 = ssi.Point.Y; warn.Add("schedule instance bbox null; Point used as top-left"); }
double tbbH = sbb != null ? sbb.Max.Y - sbb.Min.Y : sumH, tbbW = sbb != null ? sbb.Max.X - sbb.Min.X : sumW;
bool rowOk = Math.Abs(sumH - tbbH) <= 0.03 && Math.Abs(sumW - tbbW) <= 0.03;
if (rowOk)
{
    double above = 0; for (int r = r0; r < genRow; r++) above += rowH[r - r0];
    double ytop = ty1 - above, ybot = ytop - rowH[genRow - r0];
    schBox = new double[] { tx0 - MARGIN, ybot - ROWMARGIN_Y, tx0 + sumW + MARGIN, ytop + ROWMARGIN_Y };
    schNote = "row band (row " + genRow + " height " + f3(rowH[genRow - r0]) + ")";
}
else
{
    schBox = new double[] { tx0 - MARGIN, ty1 - tbbH - MARGIN, tx0 + tbbW + MARGIN, ty1 + MARGIN };
    schNote = "WHOLE TABLE (row heights sum " + f3(sumH) + "x" + f3(sumW) + " != bbox " + f3(tbbH) + "x" + f3(tbbW) + ")";
    warn.Add("row bounds not reliable: whole table clouded");
}
sb.Append("\nSCH instance bbox=").Append(sbb != null ? bx(new double[] { sbb.Min.X, sbb.Min.Y, sbb.Max.X, sbb.Max.Y }) : "null").Append(" rows_sum=").Append(f3(sumH)).Append(" plan=").Append(schNote).Append(" rect=").Append(bx(schBox));

// ---------- 9. id snapshot before (R51)
Func<HashSet<long>> snap = () =>
{
    HashSet<long> s = new HashSet<long>();
    foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(RevisionCloud))) s.Add(e.Id.Value);
    foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(Revision))) s.Add(e.Id.Value);
    foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(IndependentTag)).OfCategory(BuiltInCategory.OST_RevisionCloudTags)) s.Add(e.Id.Value);
    return s;
};
HashSet<long> idsBefore = snap();

// ---------- 10. group, transaction, failure capture
TransactionGroup tg = new TransactionGroup(document, "WF-R1-T1");
Transaction tx = new Transaction(document, "WF-R1-T1 edits");
List<string> fails = new List<string>(); int failTotal = 0;
EventHandler<Autodesk.Revit.DB.Events.FailuresProcessingEventArgs> fh = (s, a) =>
{
    FailuresAccessor fa = a.GetFailuresAccessor(); bool err = false;
    foreach (FailureMessageAccessor fm in fa.GetFailureMessages())
    {
        failTotal++; FailureSeverity sev = fm.GetSeverity();
        if (fails.Count < 8) fails.Add(sev.ToString() + ": " + fm.GetDescriptionText() + " ids=" + string.Join(",", fm.GetFailingElementIds().Take(4).Select(x => x.Value)));
        if (sev == FailureSeverity.Error) err = true; else if (sev == FailureSeverity.Warning) fa.DeleteWarning(fm);
    }
    if (err) a.SetProcessingResult(FailureProcessingResult.ProceedWithRollBack);
};
string step = "start"; string commitState = "not reached";
bool revCreated = false;
List<string> itemLines = new List<string>();
List<RevisionCloud> clouds = new List<RevisionCloud>(); List<IndependentTag> tags = new List<IndependentTag>();
string[] cloudLabel = new string[] { "P1.00 note " + TN1, "P1.01 note " + TN2, "P0.02 schedule row" };
ViewSheet[] cloudSheet = new ViewSheet[] { sh1, sh2, sh0 };
double[][] rects = new double[3][];
string[][] tagModes = new string[][] { new string[] { "top" }, new string[] { "top" }, new string[] { "right", "top" } };
List<string> createdCleanup = new List<string>();
int nDone = 0, nSkip = 0;
document.Application.FailuresProcessing += fh;
try
{
    tg.Start(); tx.Start();
    // ---- revision
    step = "revision";
    if (rev == null)
    {
        rev = Revision.Create(document);
        rev.Description = REVDESC; rev.RevisionDate = REVDATE; rev.Issued = false; rev.Visibility = RevisionVisibility.CloudAndTagVisible;
        if (lastRev.RevisionNumberingSequenceId != null && lastRev.RevisionNumberingSequenceId != ElementId.InvalidElementId && rev.RevisionNumberingSequenceId != lastRev.RevisionNumberingSequenceId)
            rev.RevisionNumberingSequenceId = lastRev.RevisionNumberingSequenceId;
        revCreated = true; nDone++;
    }
    else
    {
        if (rev.RevisionDate != REVDATE) warn.Add("existing revision date is " + rev.RevisionDate);
        if (rev.Visibility != RevisionVisibility.CloudAndTagVisible) { rev.Visibility = RevisionVisibility.CloudAndTagVisible; warn.Add("existing revision visibility set to CloudAndTagVisible"); }
        nSkip++;
    }
    // ---- text notes
    Action<TextNote, string> setText = (tn, nt) =>
    {
        bool okFmt = false;
        try
        {
            FormattedText ft = tn.GetFormattedText();
            string plain = ft.GetPlainText();
            ft.SetPlainText(new TextRange(0, plain.Length), nt);
            tn.SetFormattedText(ft); okFmt = true;
        }
        catch (Exception) { }
        if (!okFmt || norm(tn.Text) != norm(nt)) { tn.Text = nt; warn.Add("TextNote " + tn.Id.Value + " set via Text (format runs reset)"); }
    };
    step = "text 1"; if (st1 == "todo") { setText(tn1, NEW1); nDone++; } else nSkip++;
    step = "text 2"; if (st2 == "todo") { setText(tn2, NEW2); nDone++; } else nSkip++;
    // ---- schedule cells
    step = "schedule cells";
    if (genState == "todo") { hd.SetCellText(genRow, genCol, CELL_NEW); nDone++; } else nSkip++;
    if (estState == "todo") { hd.SetCellText(genRow, estCol, ""); nDone++; } else nSkip++;
    step = "regenerate"; document.Regenerate();
    // ---- rectangles
    step = "measure";
    rects[0] = textRect(tn1, vp1); rects[1] = textRect(tn2, vp2);
    rects[2] = new double[] { schBox[0], schBox[1], schBox[2], schBox[3] };
    // ---- clouds + tags
    for (int i = 0; i < 3; i++)
    {
        ViewSheet sh = cloudSheet[i]; double[] r = rects[i];
        step = "cloud " + cloudLabel[i];
        if (r[2] - r[0] > 3.0 || r[3] - r[1] > 2.0) throw new Exception("rectangle too large " + bx(r));
        double[] tbe = tbExt(sh);
        if (tbe == null) warn.Add(sh.SheetNumber + ": no title block found for the inside-sheet check");
        else if (r[0] < tbe[0] - 0.01 || r[1] < tbe[1] - 0.01 || r[2] > tbe[2] + 0.01 || r[3] > tbe[3] + 0.01)
            throw new Exception("cloud rect " + bx(r) + " is outside the title block " + bx(tbe) + " on " + sh.SheetNumber);
        // already there? (idempotent)
        RevisionCloud have = null;
        foreach (Element e in new FilteredElementCollector(document, sh.Id).OfClass(typeof(RevisionCloud)))
        {
            RevisionCloud rc0 = e as RevisionCloud; if (rc0 == null || rc0.RevisionId.Value != rev.Id.Value) continue;
            double[] b0 = cloudBox(rc0);
            if (b0 != null && b0[0] <= r[0] + 0.01 && b0[1] <= r[1] + 0.01 && b0[2] >= r[2] - 0.01 && b0[3] >= r[3] - 0.01) have = rc0;
        }
        if (have != null) { nSkip++; itemLines.Add(cloudLabel[i] + ": already clouded (" + have.Id.Value + ")"); continue; }
        HashSet<long> exclC = new HashSet<long>(); if (sh.Id.Value == SH0) exclC.Add(SCHI);
        foreach (double[] o in obstacles(sh, exclC)) if (ov(new double[] { r[0], r[1], r[2], r[3] }, o)) throw new Exception("cloud rect " + bx(r) + " overlaps existing " + (o[4] == 1 ? "cloud" : o[4] == 2 ? "revision tag" : "schedule") + " " + bx(o));
        List<Curve> curves = new List<Curve>();
        XYZ[] pts = new XYZ[] { new XYZ(r[0], r[1], 0), new XYZ(r[0], r[3], 0), new XYZ(r[2], r[3], 0), new XYZ(r[2], r[1], 0) };   // BL, TL, TR, BR = clockwise
        for (int k = 0; k < 4; k++) curves.Add(Line.CreateBound(pts[k], pts[(k + 1) % 4]));
        RevisionCloud cloud = RevisionCloud.Create(document, sh, rev.Id, curves);
        step = "tag " + cloudLabel[i];
        IndependentTag tag = IndependentTag.Create(document, tagSym.Id, sh.Id, new Reference(cloud), false, TagOrientation.Horizontal, new XYZ(r[2] - 0.17 / 12.0, r[3] + 0.08 / 12.0, 0));
        document.Regenerate();
        HashSet<long> exclT = new HashSet<long>(); exclT.Add(cloud.Id.Value); exclT.Add(tag.Id.Value);
        List<double[]> obsT = obstacles(sh, exclT);
        string placed = null; double[] tbox = null; List<string> tried = new List<string>();
        foreach (string mode in tagModes[i])
        {
            BoundingBoxXYZ b = tag.get_BoundingBox(sh); if (b == null) throw new Exception("tag has no bbox");
            XYZ hp = tag.TagHeadPosition;
            double dx = 0, dy = 0;
            if (mode == "top")
            {
                // standard: head (right - 0.17 in, top + 0.08 in); raise until the box bottom clears the arcs
                dx = (r[2] - 0.17 / 12.0) - hp.X; dy = (r[3] + 0.08 / 12.0) - hp.Y;
                double ybot = b.Min.Y + dy;
                double up = (r[3] + gapReq) - ybot; if (up > 0) dy += up;
                if (dy > 0.08 / 12.0 + 0.5 / 12.0) { tried.Add("top: needs raise " + f3(dy)); continue; }
            }
            else
            {
                dx = (r[2] + gapReq) - b.Min.X; dy = 0.5 * (r[1] + r[3]) - 0.5 * (b.Min.Y + b.Max.Y);
            }
            tag.TagHeadPosition = new XYZ(hp.X + dx, hp.Y + dy, hp.Z);
            document.Regenerate();
            BoundingBoxXYZ b2 = tag.get_BoundingBox(sh); if (b2 == null) throw new Exception("tag has no bbox after move");
            double[] tb = new double[] { b2.Min.X, b2.Min.Y, b2.Max.X, b2.Max.Y };
            string bad = null;
            if (ov(tb, new double[] { r[0], r[1], r[2], r[3] })) bad = "overlaps own cloud";
            foreach (double[] o in obsT) if (bad == null && ov(tb, o)) bad = "overlaps " + (o[4] == 1 ? "cloud" : o[4] == 2 ? "revision tag" : "schedule") + " " + bx(o);
            if (bad == null && (tb[2] > 3.45 || tb[0] < 0.0)) bad = "outside sheet";
            if (bad == null) { placed = mode; tbox = tb; break; }
            tried.Add(mode + ": " + bad + " tag=" + bx(tb));
        }
        if (placed == null) throw new Exception("no clear tag position for " + cloudLabel[i] + " [" + string.Join(" | ", tried) + "]");
        double area = cloudArea(cloud);
        if (area >= 0) throw new Exception("cloud " + cloud.Id.Value + " not clockwise (area " + area + ")");
        clouds.Add(cloud); tags.Add(tag); nDone += 2;
        double[] cb = cloudBox(cloud);
        itemLines.Add(cloudLabel[i] + ": cloud " + cloud.Id.Value + " rect=" + bx(r) + " sketch=" + (cb != null ? bx(cb) : "?") + " area=" + f3(area) + " tag " + tag.Id.Value + " mode=" + placed + " box=" + bx(tbox));
    }
    step = "commit";
    TransactionStatus ts = tx.Commit();
    commitState = ts == TransactionStatus.Committed ? "Committed" : ts.ToString();
    if (ts != TransactionStatus.Committed) throw new Exception("Commit returned " + ts.ToString() + " failures=" + string.Join(" | ", fails));
}
catch (Exception ex)
{
    string msg = ex.GetType().Name + ": " + ex.Message;
    try { if (tx.HasStarted() && !tx.HasEnded()) tx.RollBack(); } catch (Exception) { }
    try { if (tg.HasStarted() && !tg.HasEnded()) tg.RollBack(); } catch (Exception) { }
    document.Application.FailuresProcessing -= fh;
    string tailE = "\ndone=0 skipped=0 failed=1";
    string headE = "ROLLED_BACK " + step + ": " + CapS(msg, 1200) + "\ncommit: " + commitState + " failureTotal=" + failTotal + (fails.Count > 0 ? " " + CapS(string.Join(" | ", fails), 500) : "") + (mapInfo.Count > 0 ? "\nmap: " + CapS(string.Join(" ;; ", mapInfo), 700) : "");
    return headE + "\n" + CapS(sb.ToString(), 3000 - headE.Length - tailE.Length - 2) + tailE;
}
document.Application.FailuresProcessing -= fh;

// ---------- 11. read-back after commit, inside the group
List<string> mism = new List<string>();
string note1 = ((TextNote)document.GetElement(new ElementId(TN1))).Text, note2 = ((TextNote)document.GetElement(new ElementId(TN2))).Text;
if (norm(note1) != norm(NEW1)) mism.Add("TN1 reads [" + note1 + "]");
if (norm(note2) != norm(NEW2)) mism.Add("TN2 reads [" + note2 + "]");
TableSectionData hd2 = ((ViewSchedule)document.GetElement(new ElementId(SCHV))).GetTableData().GetSectionData(SectionType.Header);
for (int r = r0; r <= r1; r++)
    for (int c = c0; c <= c1; c++)
    {
        string was = cellsBefore[r + "," + c], now = hd2.GetCellText(r, c) ?? "";
        string want = was;
        if (r == genRow && c == genCol) want = CELL_NEW;
        if (r == genRow && c == estCol) want = "";
        if (norm(now) != norm(want)) mism.Add("cell (" + r + "," + c + ") reads [" + now + "] want [" + want + "] (was [" + was + "])");
    }
Revision revRb = document.GetElement(rev.Id) as Revision;
if (revRb == null) mism.Add("revision missing");
else
{
    if ((revRb.Description ?? "").Trim() != REVDESC) mism.Add("rev description " + revRb.Description);
    if (revRb.RevisionDate != REVDATE) mism.Add("rev date " + revRb.RevisionDate);
    if (revRb.Issued) mism.Add("rev issued");
    if (revRb.Visibility != RevisionVisibility.CloudAndTagVisible) mism.Add("rev visibility " + revRb.Visibility.ToString());
}
List<string> shRev = new List<string>();
for (int i = 0; i < clouds.Count; i++)
{
    RevisionCloud rcb = document.GetElement(clouds[i].Id) as RevisionCloud; IndependentTag tgb = document.GetElement(tags[i].Id) as IndependentTag;
    if (rcb == null || tgb == null) { mism.Add("cloud/tag " + i + " missing"); continue; }
    if (rcb.RevisionId.Value != rev.Id.Value) mism.Add("cloud " + rcb.Id.Value + " wrong revision");
    if (cloudArea(rcb) >= 0) mism.Add("cloud " + rcb.Id.Value + " not clockwise");
    bool tagsIt = false; foreach (ElementId x in tgb.GetTaggedLocalElementIds()) if (x.Value == rcb.Id.Value) tagsIt = true;
    if (!tagsIt) mism.Add("tag " + tgb.Id.Value + " does not tag cloud " + rcb.Id.Value);
}
foreach (ViewSheet shx in new ViewSheet[] { sh1, sh2, sh0 })
{
    bool inSched = false; foreach (ElementId x in shx.GetAllRevisionIds()) if (x.Value == rev.Id.Value) inSched = true;
    shRev.Add(shx.SheetNumber + ":" + (inSched ? "rev listed" : "rev NOT listed"));
    if (!inSched) mism.Add(shx.SheetNumber + " revision schedule does not list the new revision");
}
HashSet<long> idsAfter = snap();
foreach (long id in idsAfter)
{
    if (idsBefore.Contains(id)) continue;
    Element e = document.GetElement(new ElementId(id));
    if (e is Revision) createdCleanup.Add("rev:" + id);
    else if (e is RevisionCloud) createdCleanup.Add("cloud:" + id);
    else createdCleanup.Add("tag:" + id);
}
string b64a = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(cur1)), b64b = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(cur2));
createdCleanup.Add("old1:" + b64a); createdCleanup.Add("old2:" + b64b);

// ---------- 12. report (body capped; tail built first and never cut)
sb.Append("\ncommit: ").Append(commitState).Append(" failureTotal=").Append(failTotal);
if (fails.Count > 0) sb.Append(" ").Append(CapS(string.Join(" | ", fails), 300));
sb.Append("\nrevision_id=").Append(rev.Id.Value).Append(revCreated ? " (created)" : " (reused)").Append(" sheets: ").Append(string.Join(" ", shRev));
foreach (string mi in mapInfo) sb.Append("\nmap: ").Append(CapS(mi, 260));
sb.Append("\nTN1 new=[").Append(CapS(note1.Replace("\r", " / "), 60)).Append("] rect=").Append(rects[0] != null ? bx(rects[0]) : "-").Append(" w=").Append(rects[0] != null ? f3(rects[0][4]) : "-").Append(" h=").Append(rects[0] != null ? f3(rects[0][5]) : "-");
sb.Append("\nTN2 new=[").Append(CapS(note2.Replace("\r", " / "), 60)).Append("] rect=").Append(rects[1] != null ? bx(rects[1]) : "-").Append(" w=").Append(rects[1] != null ? f3(rects[1][4]) : "-").Append(" h=").Append(rects[1] != null ? f3(rects[1][5]) : "-");
foreach (string l in itemLines) sb.Append("\n").Append(l);
if (warn.Count > 0) sb.Append("\nwarn: ").Append(CapS(string.Join("; ", warn), 250));
if (mism.Count > 0)
{
    try { tg.RollBack(); } catch (Exception) { }
    string tailM = "\nMISMATCH: " + CapS(string.Join(" | ", mism), 500) + "\nROLLED_BACK readback\ndone=0 skipped=" + nSkip + " failed=" + mism.Count;
    return CapS(sb.ToString(), 3000 - tailM.Length) + tailM;
}
if (dry)
{
    try { tg.RollBack(); } catch (Exception) { }
    string tailD = "\ngroup: rolled back (dryrun); nothing remains in the model\ndone=0 skipped=" + nSkip + " failed=0 (dryrun: would_do=" + nDone + ")";
    return CapS(sb.ToString(), 3000 - tailD.Length) + tailD;
}
tg.Assimilate();
string tailR = "\ncleanup_params: [\"" + string.Join("\",\"", createdCleanup) + "\"]\ndone=" + nDone + " skipped=" + nSkip + " failed=0";
return CapS(sb.ToString(), 3000 - tailR.Length) + tailR;
}
public static string CapS(string s, int n) { return s.Length <= n ? s : s.Substring(0, n) + "..."; }
public static object DummyTail() { return null;
