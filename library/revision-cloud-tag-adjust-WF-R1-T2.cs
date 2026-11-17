// Replace a sheet revision cloud with a new rect and re-place/move revision tags clear of annotations (viewport obstacle projection) - WF-R1-T2 (25-061)
// revision-cloud-tag-adjust: P0.02 cloud rebuilt lower + tag left of table; P1.01 tag 3242034 moved right of its cloud (layout only)
// WF-R1-T2 v1 (precheck fix 1) | run 20261116-1839 | Revit 2024 (members present in 2024 and 2025) | follow-up to WF-R1-T1 (cloud/tag code from WF-R1-T1.cs)
// Body of: public static object Execute(Document document, object[] parameters)
// send_code_to_revit transactionMode "none": own TransactionGroup + one Transaction. Never saves or syncs.
// parameters: ["dryrun"] = full edit inside the group, commit, read back, ROLL BACK the group. [] = real run (Assimilate if read-back matches).
// Return: body capped, tail (cleanup_params + done/skipped/failed) built first and never cut.
// done counts: (a) old P0.02 cloud+tag deleted =1, (b) new P0.02 cloud =1, (c) new P0.02 tag =1, (d) P1.01 tag moved =1.
// In-view obstacles (viewport 3211275) are projected like WF-R1-T1.v2 textRect: model point -> view frame (Origin/Right/Up) -> sheet,
// three candidate mappings, the one that puts most annotations inside vp.GetBoxOutline() is kept; none inside -> FAILED.
var inv = System.Globalization.CultureInfo.InvariantCulture;
bool dry = false;
if (parameters != null)
    foreach (object po in parameters)
        if (po != null && po.ToString().Trim().Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;
string TAIL0 = "\ndone=0 skipped=0 failed=0";
long REVID = 3242008L, OLDCLOUD = 3242051L, OLDTAG = 3242052L, TAG101 = 3242034L, CLOUD101 = 3242033L, SH0 = 3090590L, SH2 = 3204515L, SCHI = 3236203L, VW2 = 3205752L;
double[] NEWRECT = new double[] { 2.738, 1.633, 3.214, 1.6635 };
double[] OLDRECT = new double[] { 2.738, 1.635, 3.214, 1.671 };
double[][] CTR0 = new double[][] { new double[] { 2.700, 1.648 }, new double[] { 2.690, 1.648 } };
double[][] CTR2 = new double[][] { new double[] { 2.957, 0.235 }, new double[] { 2.957, 0.210 }, new double[] { 2.957, 0.260 } };
List<string> warn = new List<string>();
System.Text.StringBuilder sb = new System.Text.StringBuilder();
Func<string, string> fail = m => "FAILED WF-R1-T2: " + m + TAIL0;
Func<double, string> f3 = v => Math.Round(v, 3).ToString(inv);
Func<double[], string> bx = b => "(" + f3(b[0]) + "," + f3(b[1]) + ")-(" + f3(b[2]) + "," + f3(b[3]) + ")";
Func<double[], double[], double, bool> ov = (a, b, pad) => a[0] < b[2] + pad && b[0] - pad < a[2] && a[1] < b[3] + pad && b[1] - pad < a[3];
Func<double[], double[], bool> near = (a, b) => Math.Abs(a[0] - b[0]) < 0.01 && Math.Abs(a[1] - b[1]) < 0.01 && Math.Abs(a[2] - b[2]) < 0.01 && Math.Abs(a[3] - b[3]) < 0.01;

// ---------- 0. document, elements
string title = document.Title ?? "";
if (title.IndexOf("25-061_Whole-Foods-Market_AO_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return fail("wrong document open: " + title);
ViewSheet sh0 = document.GetElement(new ElementId(SH0)) as ViewSheet;
ViewSheet sh2 = document.GetElement(new ElementId(SH2)) as ViewSheet;
Revision rev = document.GetElement(new ElementId(REVID)) as Revision;
ScheduleSheetInstance ssi = document.GetElement(new ElementId(SCHI)) as ScheduleSheetInstance;
IndependentTag tag101 = document.GetElement(new ElementId(TAG101)) as IndependentTag;
RevisionCloud cloud101 = document.GetElement(new ElementId(CLOUD101)) as RevisionCloud;
RevisionCloud oldCloud = document.GetElement(new ElementId(OLDCLOUD)) as RevisionCloud;
IndependentTag oldTag = document.GetElement(new ElementId(OLDTAG)) as IndependentTag;
if (sh0 == null || sh2 == null) return fail("sheet P0.02 " + SH0 + " or P1.01 " + SH2 + " missing");
if (rev == null) return fail("revision " + REVID + " missing");
if (rev.Issued) return fail("revision " + REVID + " is issued");
if ((rev.Description ?? "").Trim() != "CDPv4 SUBMITTAL") return fail("revision " + REVID + " is '" + rev.Description + "'");
if (ssi == null) return fail("schedule instance " + SCHI + " missing");
if (tag101 == null || cloud101 == null) return fail("tag " + TAG101 + " or cloud " + CLOUD101 + " missing");
if (tag101.OwnerViewId.Value != SH2 || cloud101.OwnerViewId.Value != SH2) return fail("tag/cloud 101 not owned by sheet P1.01");
bool tagsCloud = false; foreach (ElementId x in tag101.GetTaggedLocalElementIds()) if (x.Value == CLOUD101) tagsCloud = true;
if (!tagsCloud) return fail("tag " + TAG101 + " does not tag cloud " + CLOUD101);
sb.Append(dry ? "DRYRUN WF-R1-T2 v1" : "RUN WF-R1-T2 v1").Append(" doc=").Append(title);

// ---------- 1. worksharing
if (document.IsWorkshared)
{
    List<long> cid = new List<long> { TAG101, CLOUD101, SH0, SH2, REVID };
    if (oldCloud != null) cid.Add(OLDCLOUD);
    if (oldTag != null) cid.Add(OLDTAG);
    List<string> owned = new List<string>();
    foreach (long id in cid)
        if (WorksharingUtils.GetCheckoutStatus(document, new ElementId(id)) == CheckoutStatus.OwnedByOtherUser)
        {
            string ow = "?";
            try { ow = WorksharingUtils.GetWorksharingTooltipInfo(document, new ElementId(id)).Owner; } catch (Exception) { }
            owned.Add(id + " owned by " + ow);
        }
    if (owned.Count > 0) return fail(string.Join("; ", owned));
}

// ---------- 2. helpers
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
// sheet-owned items in sheet coordinates: {x0,y0,x1,y1,kind,id}; kind 1 cloud, 2 tag, 3 schedule, 4 text, 5 generic annotation
Func<ViewSheet, HashSet<long>, List<double[]>> sheetObs = (v, excl) =>
{
    List<double[]> res = new List<double[]>();
    foreach (Element e in new FilteredElementCollector(document, v.Id).WhereElementIsNotElementType())
    {
        if (excl.Contains(e.Id.Value)) continue;
        double[] b = null; double kind = 0;
        RevisionCloud rc = e as RevisionCloud; TextNote tn = e as TextNote; IndependentTag it = e as IndependentTag; ScheduleSheetInstance si = e as ScheduleSheetInstance;
        FamilyInstance fi = e as FamilyInstance;
        if (rc != null) { b = cloudBox(rc); kind = 1; }
        else if (it != null) kind = 2;
        else if (si != null) kind = 3;
        else if (tn != null) kind = 4;
        else if (fi != null && fi.Category != null && fi.Category.BuiltInCategory == BuiltInCategory.OST_GenericAnnotation) kind = 5;
        else continue;
        if (b == null)
        {
            BoundingBoxXYZ bb = e.get_BoundingBox(v); if (bb == null) continue;
            b = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
        }
        if (tn != null)
        {   // TextNote bbox is unreliable: add the Width/Height rectangle (sheet paper feet) from the insertion point
            double w = tn.Width, h = tn.Height;
            if (h < 0.003 || h > 3.0) h = 0.05;
            XYZ o = tn.Coord; XYZ bd = tn.BaseDirection, ud = tn.UpDirection;
            double a0 = 0, a1 = w, b0 = -h, b1 = 0;
            if (tn.HorizontalAlignment == HorizontalTextAlignment.Center) { a0 = -w / 2; a1 = w / 2; } else if (tn.HorizontalAlignment == HorizontalTextAlignment.Right) { a0 = -w; a1 = 0; }
            if (tn.VerticalAlignment == VerticalTextAlignment.Middle) { b0 = -h / 2; b1 = h / 2; } else if (tn.VerticalAlignment == VerticalTextAlignment.Bottom) { b0 = 0; b1 = h; }
            double[] aa = new double[] { a0, a1, a1, a0 }, bbv = new double[] { b0, b0, b1, b1 };
            for (int i = 0; i < 4; i++) { XYZ p = o + bd * aa[i] + ud * bbv[i]; b[0] = Math.Min(b[0], p.X); b[1] = Math.Min(b[1], p.Y); b[2] = Math.Max(b[2], p.X); b[3] = Math.Max(b[3], p.Y); }
        }
        res.Add(new double[] { b[0], b[1], b[2], b[3], kind, e.Id.Value });
    }
    return res;
};
// annotations owned by the view inside a viewport, projected to sheet coordinates (WF-R1-T1.v2 mapping: view frame + 3 candidates + outline check)
List<string> mapInfo = new List<string>();
Func<View, Viewport, List<double[]>> viewObs = (vw, vp) =>
{
    Outline ol = vp.GetBoxOutline(); double tol = 0.05;
    double ox0 = ol.MinimumPoint.X - tol, oy0 = ol.MinimumPoint.Y - tol, ox1 = ol.MaximumPoint.X + tol, oy1 = ol.MaximumPoint.Y + tol;
    XYZ R = vw.RightDirection, U = vw.UpDirection, O = vw.Origin;
    Transform T = vp.GetProjectionToSheetTransform();
    XYZ bc = vp.GetBoxCenter(); double scale = Math.Max(1, vw.Scale);
    Transform ct = null; double cx = 0, cy = 0;
    try { BoundingBoxXYZ cb = vw.CropBox; ct = cb.Transform; XYZ cc = ct.OfPoint((cb.Min + cb.Max) * 0.5); cx = (cc - O).DotProduct(R); cy = (cc - O).DotProduct(U); } catch (Exception) { ct = null; }
    if (vp.Rotation != ViewportRotation.None) warn.Add("viewport rotated " + vp.Rotation.ToString());
    string[] mname = new string[] { "T(view-frame)", "T(crop-local)", "center+delta/scale" };
    Func<XYZ, int, double[]> mp = (P, m) =>
    {
        double px = (P - O).DotProduct(R), py = (P - O).DotProduct(U);
        if (m == 0) { XYZ s0 = T.OfPoint(new XYZ(px, py, 0)); return new double[] { s0.X, s0.Y }; }
        if (m == 1) { XYZ pl = ct.Inverse.OfPoint(P); XYZ s1 = T.OfPoint(new XYZ(pl.X, pl.Y, 0)); return new double[] { s1.X, s1.Y }; }
        return new double[] { bc.X + (px - cx) / scale, bc.Y + (py - cy) / scale };
    };
    List<XYZ[]> pts = new List<XYZ[]>(); List<double[]> meta = new List<double[]>();
    foreach (Element e in new FilteredElementCollector(document, vw.Id).WhereElementIsNotElementType())
    {
        double kind = 0; TextNote tn = e as TextNote; IndependentTag it = e as IndependentTag; FamilyInstance fi = e as FamilyInstance;
        if (it != null) kind = 2;
        else if (tn != null) kind = 4;
        else if (fi != null && fi.Category != null && fi.Category.BuiltInCategory == BuiltInCategory.OST_GenericAnnotation) kind = 5;
        else continue;
        List<XYZ> pl = new List<XYZ>();
        BoundingBoxXYZ bb = e.get_BoundingBox(vw);
        if (bb != null)
            foreach (double zx in new double[] { bb.Min.X, bb.Max.X }) foreach (double zy in new double[] { bb.Min.Y, bb.Max.Y }) foreach (double zz in new double[] { bb.Min.Z, bb.Max.Z })
                pl.Add(bb.Transform.OfPoint(new XYZ(zx, zy, zz)));
        if (tn != null)
        {   // text rectangle in model units (paper size x view scale) from the insertion point
            double w = tn.Width * scale, h = tn.Height * scale;
            if (tn.Height < 0.003 || tn.Height > 3.0) h = 0.05 * scale;
            double a0 = 0, a1 = w, b0 = -h, b1 = 0;
            if (tn.HorizontalAlignment == HorizontalTextAlignment.Center) { a0 = -w / 2; a1 = w / 2; } else if (tn.HorizontalAlignment == HorizontalTextAlignment.Right) { a0 = -w; a1 = 0; }
            if (tn.VerticalAlignment == VerticalTextAlignment.Middle) { b0 = -h / 2; b1 = h / 2; } else if (tn.VerticalAlignment == VerticalTextAlignment.Bottom) { b0 = 0; b1 = h; }
            double[] aa = new double[] { a0, a1, a1, a0 }, bbv = new double[] { b0, b0, b1, b1 };
            for (int i = 0; i < 4; i++) pl.Add(tn.Coord + tn.BaseDirection * aa[i] + tn.UpDirection * bbv[i]);
        }
        if (pl.Count == 0) continue;
        pts.Add(pl.ToArray()); meta.Add(new double[] { kind, e.Id.Value });
    }
    int best = -1, bestCount = -1; List<string> scores = new List<string>();
    for (int m = 0; m < 3; m++)
    {
        if (m == 1 && ct == null) continue;
        int cnt = 0;
        for (int i = 0; i < pts.Count; i++)
        {
            double sx = 0, sy = 0; foreach (XYZ p in pts[i]) { double[] q = mp(p, m); sx += q[0]; sy += q[1]; }
            sx /= pts[i].Length; sy /= pts[i].Length;
            if (sx >= ox0 && sx <= ox1 && sy >= oy0 && sy <= oy1) cnt++;
        }
        scores.Add(mname[m] + "=" + cnt);
        if (cnt > bestCount) { bestCount = cnt; best = m; }
    }
    if (pts.Count > 0 && bestCount <= 0) throw new Exception("view " + vw.Id.Value + ": " + pts.Count + " annotations map outside viewport outline " + bx(new double[] { ox0, oy0, ox1, oy1 }) + " by every method: " + string.Join(" ", scores));
    List<double[]> res = new List<double[]>();
    for (int i = 0; i < pts.Count; i++)
    {
        double x0 = double.MaxValue, y0 = double.MaxValue, x1 = double.MinValue, y1 = double.MinValue, sx = 0, sy = 0;
        foreach (XYZ p in pts[i]) { double[] q = mp(p, best); x0 = Math.Min(x0, q[0]); y0 = Math.Min(y0, q[1]); x1 = Math.Max(x1, q[0]); y1 = Math.Max(y1, q[1]); sx += q[0]; sy += q[1]; }
        sx /= pts[i].Length; sy /= pts[i].Length;
        if (sx < ox0 || sx > ox1 || sy < oy0 || sy > oy1) continue;   // not shown in the viewport
        res.Add(new double[] { x0, y0, x1, y1, meta[i][0], meta[i][1] });
    }
    mapInfo.Add("view " + vw.Id.Value + " annots=" + pts.Count + " inside=" + res.Count + " method=" + mname[best] + " scores " + string.Join(" ", scores) + " outline=" + bx(new double[] { ox0 + tol, oy0 + tol, ox1 - tol, oy1 - tol }));
    return res;
};
string[] kindName = new string[] { "?", "cloud", "tag", "schedule", "text", "annotation" };
// first obstacle hit by box tb (pad 0.01 ft, 0.004 for clouds); null if clear
Func<double[], List<double[]>, string> hit = (tb, obs) =>
{
    foreach (double[] o in obs)
        if (ov(tb, o, o[4] == 1 ? 0.004 : 0.01)) return kindName[(int)o[4]] + " " + (long)o[5] + " " + bx(o);
    return null;
};
// tag moved so its box centre is on target; returns the final box or null
Func<IndependentTag, ViewSheet, double[], double[]> centreTag = (tag, sh, ctr) =>
{
    for (int pass = 0; pass < 2; pass++)
    {
        BoundingBoxXYZ b = tag.get_BoundingBox(sh); if (b == null) return null;
        double dx = ctr[0] - 0.5 * (b.Min.X + b.Max.X), dy = ctr[1] - 0.5 * (b.Min.Y + b.Max.Y);
        if (Math.Abs(dx) < 0.0005 && Math.Abs(dy) < 0.0005) break;
        XYZ hp = tag.TagHeadPosition; tag.TagHeadPosition = new XYZ(hp.X + dx, hp.Y + dy, hp.Z);
        document.Regenerate();
    }
    BoundingBoxXYZ f = tag.get_BoundingBox(sh); if (f == null) return null;
    return new double[] { f.Min.X, f.Min.Y, f.Max.X, f.Max.Y };
};

// ---------- 3. states
BoundingBoxXYZ sbb = ssi.get_BoundingBox(sh0);
if (sbb == null) return fail("schedule instance " + SCHI + " has no bbox");
double[] schBox = new double[] { sbb.Min.X, sbb.Min.Y, sbb.Max.X, sbb.Max.Y };
double[] oldCloudBox = oldCloud != null ? cloudBox(oldCloud) : null;
RevisionCloud newExisting = null;
foreach (Element e in new FilteredElementCollector(document, sh0.Id).OfClass(typeof(RevisionCloud)))
{
    RevisionCloud rc = e as RevisionCloud; if (rc == null || rc.RevisionId.Value != REVID || (oldCloud != null && rc.Id.Value == OLDCLOUD)) continue;
    double[] b = cloudBox(rc); if (b != null && near(b, NEWRECT)) newExisting = rc;
}
string st1;
if (newExisting != null) st1 = "done";
else if (oldCloud != null && oldCloudBox != null && near(oldCloudBox, OLDRECT)) st1 = "todo";
else return fail("P0.02: neither the new cloud nor the old cloud " + OLDCLOUD + " (rect " + (oldCloudBox != null ? bx(oldCloudBox) : "gone") + ") found");
if (st1 == "todo" && oldTag == null) return fail("old tag " + OLDTAG + " missing");
BoundingBoxXYZ b101 = tag101.get_BoundingBox(sh2); if (b101 == null) return fail("tag " + TAG101 + " has no bbox");
double[] box101 = new double[] { b101.Min.X, b101.Min.Y, b101.Max.X, b101.Max.Y };
double cx101 = 0.5 * (box101[0] + box101[2]), cy101 = 0.5 * (box101[1] + box101[3]);
string st2 = "todo";
foreach (double[] c in CTR2) if (Math.Abs(cx101 - c[0]) < 0.005 && Math.Abs(cy101 - c[1]) < 0.005 && cx101 > 2.934) st2 = "done";
Viewport vp2 = null;
foreach (Element e in new FilteredElementCollector(document, sh2.Id).OfClass(typeof(Viewport))) { Viewport v = e as Viewport; if (v != null && v.ViewId.Value == VW2) vp2 = v; }
if (vp2 == null) return fail("no viewport of view " + VW2 + " on P1.01");
View view2 = document.GetElement(new ElementId(VW2)) as View;
if (view2 == null) return fail("view " + VW2 + " missing");
XYZ oldHead101 = tag101.TagHeadPosition;
sb.Append("\nstep1(P0.02)=").Append(st1).Append(" oldCloud=").Append(oldCloudBox != null ? bx(oldCloudBox) : "-").Append(" new=").Append(bx(NEWRECT)).Append(" sched=").Append(bx(schBox));
sb.Append("\nstep2(P1.01)=").Append(st2).Append(" tag ").Append(TAG101).Append(" box=").Append(bx(box101)).Append(" head=(").Append(f3(oldHead101.X)).Append(",").Append(f3(oldHead101.Y)).Append(") cloud=").Append(bx(cloudBox(cloud101)));
XYZ oldHead52 = (oldTag != null) ? oldTag.TagHeadPosition : null;

// ---------- 4. tag type
FamilySymbol tagSym = null;
if (oldTag != null) tagSym = document.GetElement(oldTag.GetTypeId()) as FamilySymbol;
if (tagSym == null)
    foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
    { FamilySymbol fs = e as FamilySymbol; if (fs != null && (fs.FamilyName == "Revision Tag" || fs.Name == "Revision Tag")) { tagSym = fs; break; } }
if (tagSym == null) return fail("no Revision Tag symbol");
sb.Append("\ntag_type=").Append(tagSym.Id.Value).Append(" ").Append(tagSym.FamilyName).Append(":").Append(tagSym.Name);

// ---------- 5. group + transaction
TransactionGroup tg = new TransactionGroup(document, "WF-R1-T2");
Transaction tx = new Transaction(document, "WF-R1-T2 edits");
List<string> fails = new List<string>(); int failTotal = 0;
EventHandler<Autodesk.Revit.DB.Events.FailuresProcessingEventArgs> fh = (s, a) =>
{
    FailuresAccessor fa = a.GetFailuresAccessor(); bool err = false;
    foreach (FailureMessageAccessor fm in fa.GetFailureMessages())
    {
        failTotal++; FailureSeverity sev = fm.GetSeverity();
        if (fails.Count < 6) fails.Add(sev.ToString() + ": " + fm.GetDescriptionText());
        if (sev == FailureSeverity.Error) err = true; else if (sev == FailureSeverity.Warning) fa.DeleteWarning(fm);
    }
    if (err) a.SetProcessingResult(FailureProcessingResult.ProceedWithRollBack);
};
string step = "start", commitState = "not reached";
int nDone = 0, nSkip = 0;
RevisionCloud newCloud = null; IndependentTag newTag = null; double[] newTagBox = null; double[] ctr0Used = null; double[] tag101Final = null; double[] ctr2Used = null;
List<string> tried = new List<string>(), tried0 = new List<string>();
document.Application.FailuresProcessing += fh;
try
{
    tg.Start(); tx.Start();
    // ---- (1) P0.02
    if (st1 == "todo")
    {
        step = "delete old tag/cloud";
        if (oldTag.Pinned) oldTag.Pinned = false;
        if (oldCloud.Pinned) oldCloud.Pinned = false;
        document.Delete(new ElementId(OLDTAG));
        document.Delete(new ElementId(OLDCLOUD));
        if (document.GetElement(new ElementId(OLDCLOUD)) != null || document.GetElement(new ElementId(OLDTAG)) != null) throw new Exception("old cloud/tag still exist after Delete");
        nDone++;
        step = "new cloud P0.02";
        double[] r = NEWRECT;
        XYZ[] pts = new XYZ[] { new XYZ(r[0], r[1], 0), new XYZ(r[0], r[3], 0), new XYZ(r[2], r[3], 0), new XYZ(r[2], r[1], 0) };   // BL, TL, TR, BR = clockwise
        List<Curve> curves = new List<Curve>();
        for (int k = 0; k < 4; k++) curves.Add(Line.CreateBound(pts[k], pts[(k + 1) % 4]));
        newCloud = RevisionCloud.Create(document, sh0, rev.Id, curves);
        nDone++;
        step = "new tag P0.02";
        newTag = IndependentTag.Create(document, tagSym.Id, sh0.Id, new Reference(newCloud), false, TagOrientation.Horizontal, new XYZ(CTR0[0][0], CTR0[0][1], 0));
        document.Regenerate();
        try { if (newTag.HasLeader) newTag.HasLeader = false; } catch (Exception) { warn.Add("leader flag not settable"); }
        HashSet<long> ex0 = new HashSet<long>(); ex0.Add(newTag.Id.Value);
        List<double[]> obs0 = sheetObs(sh0, ex0);
        foreach (double[] c in CTR0)
        {
            double[] tb = centreTag(newTag, sh0, c);
            if (tb == null) throw new Exception("new tag has no bbox");
            string bad = null;
            if (tb[0] < 2.60) bad = "x0 " + f3(tb[0]) + " < 2.60";
            else if (ov(tb, schBox, 0.01)) bad = "within 0.01 ft of schedule instance " + bx(schBox);
            else if (ov(tb, NEWRECT, 0.002)) bad = "touches new cloud";
            else { string h0 = hit(tb, obs0); if (h0 != null) bad = "intersects " + h0; }
            if (bad == null) { newTagBox = tb; ctr0Used = c; break; }
            tried0.Add("(" + f3(c[0]) + "," + f3(c[1]) + ") box " + bx(tb) + " " + bad);
        }
        if (newTagBox == null) throw new Exception("no clear position for new P0.02 tag: " + string.Join(" | ", tried0));
        nDone++;
    }
    else nSkip += 3;
    // ---- (2) P1.01 tag move
    step = "move tag 3242034";
    if (st2 == "todo")
    {
        try { if (tag101.HasLeader) tag101.HasLeader = false; } catch (Exception) { warn.Add("leader flag not settable on 3242034"); }
        HashSet<long> ex2 = new HashSet<long>(); ex2.Add(TAG101);
        List<double[]> obs2 = sheetObs(sh2, ex2);
        int nSheet = obs2.Count;
        step = "project view annotations";
        obs2.AddRange(viewObs(view2, vp2));
        step = "move tag 3242034";
        sb.Append("\nP1.01 obstacles: sheet=").Append(nSheet).Append(" view=").Append(obs2.Count - nSheet).Append(" ").Append(CapS(string.Join(" ", mapInfo), 450));
        foreach (double[] c in CTR2)
        {
            double[] tb = centreTag(tag101, sh2, c);
            if (tb == null) throw new Exception("tag 3242034 has no bbox");
            string h2 = hit(tb, obs2);
            if (h2 == null) { tag101Final = tb; ctr2Used = c; break; }
            tried.Add("(" + f3(c[0]) + "," + f3(c[1]) + ") box " + bx(tb) + " hits " + h2);
        }
        if (tag101Final == null) throw new Exception("no clear position for tag 3242034: " + string.Join(" | ", tried));
        nDone++;
    }
    else nSkip++;
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
    string headE = "ROLLED_BACK " + step + ": " + CapS(msg, 1200) + "\ncommit: " + commitState + " failureTotal=" + failTotal + (fails.Count > 0 ? " " + CapS(string.Join(" | ", fails), 400) : "");
    return headE + "\n" + CapS(sb.ToString(), 3000 - headE.Length - tailE.Length - 2) + tailE;
}
document.Application.FailuresProcessing -= fh;

// ---------- 6. read-back (inside the group)
List<string> mism = new List<string>();
List<string> cp = new List<string>();
if (st1 == "todo")
{
    if (document.GetElement(new ElementId(OLDCLOUD)) != null) mism.Add("old cloud still present");
    if (document.GetElement(new ElementId(OLDTAG)) != null) mism.Add("old tag still present");
    RevisionCloud nc = document.GetElement(newCloud.Id) as RevisionCloud; IndependentTag nt = document.GetElement(newTag.Id) as IndependentTag;
    if (nc == null || nt == null) mism.Add("new cloud/tag missing");
    else
    {
        double[] cb = cloudBox(nc);
        if (cb == null || !near(cb, NEWRECT)) mism.Add("new cloud outline " + (cb != null ? bx(cb) : "null") + " != " + bx(NEWRECT));
        if (cloudArea(nc) >= 0) mism.Add("new cloud not clockwise");
        if (nc.RevisionId.Value != REVID || nc.OwnerViewId.Value != SH0) mism.Add("new cloud revision/sheet wrong");
        bool t2 = false; foreach (ElementId x in nt.GetTaggedLocalElementIds()) if (x.Value == nc.Id.Value) t2 = true;
        if (!t2) mism.Add("new tag does not tag new cloud");
        BoundingBoxXYZ nb = nt.get_BoundingBox(sh0);
        if (nb == null) mism.Add("new tag no bbox");
        else
        {
            double[] nbx = new double[] { nb.Min.X, nb.Min.Y, nb.Max.X, nb.Max.Y };
            if (nbx[0] < 2.60) mism.Add("new tag x0 " + f3(nbx[0]));
            if (ov(nbx, schBox, 0.01)) mism.Add("new tag within 0.01 ft of schedule instance");
            if (Math.Abs(0.5 * (nbx[0] + nbx[2]) - ctr0Used[0]) > 0.005 || Math.Abs(0.5 * (nbx[1] + nbx[3]) - ctr0Used[1]) > 0.005) mism.Add("new tag centre off target: " + bx(nbx));
        }
    }
    cp.Add("cloud:" + newCloud.Id.Value); cp.Add("tag:" + newTag.Id.Value);
    cp.Add("oldhead52:" + oldHead52.X.ToString("R", inv) + "," + oldHead52.Y.ToString("R", inv) + "," + oldHead52.Z.ToString("R", inv));
}
if (st2 == "todo")
{
    IndependentTag t3 = document.GetElement(new ElementId(TAG101)) as IndependentTag;
    BoundingBoxXYZ fb = t3 != null ? t3.get_BoundingBox(sh2) : null;
    if (fb == null) mism.Add("tag 3242034 unreadable");
    else
    {
        double[] fbx = new double[] { fb.Min.X, fb.Min.Y, fb.Max.X, fb.Max.Y };
        if (Math.Abs(0.5 * (fbx[0] + fbx[2]) - ctr2Used[0]) > 0.005 || Math.Abs(0.5 * (fbx[1] + fbx[3]) - ctr2Used[1]) > 0.005) mism.Add("tag 3242034 centre off target: " + bx(fbx));
    }
    RevisionCloud c101 = document.GetElement(new ElementId(CLOUD101)) as RevisionCloud;
    double[] c101b = c101 != null ? cloudBox(c101) : null;
    if (c101b == null || Math.Abs(c101b[0] - 2.692) > 0.005 || Math.Abs(c101b[3] - 0.304) > 0.005) mism.Add("cloud 3242033 outline changed: " + (c101b != null ? bx(c101b) : "null"));
    cp.Add("oldhead34:" + oldHead101.X.ToString("R", inv) + "," + oldHead101.Y.ToString("R", inv) + "," + oldHead101.Z.ToString("R", inv));
}
// ---------- 7. report (body capped; tail first)
sb.Append("\ncommit: ").Append(commitState).Append(" failureTotal=").Append(failTotal);
if (fails.Count > 0) sb.Append(" ").Append(CapS(string.Join(" | ", fails), 300));
if (st1 == "todo") sb.Append("\nP0.02 new cloud ").Append(newCloud.Id.Value).Append(" rect=").Append(bx(NEWRECT)).Append(" new tag ").Append(newTag.Id.Value).Append(" box=").Append(bx(newTagBox)).Append(" centre=(").Append(f3(ctr0Used[0])).Append(",").Append(f3(ctr0Used[1])).Append(") x0>=2.60:").Append(newTagBox[0] >= 2.60 ? "yes" : "NO").Append(" sched_pad0.01_hit=").Append(ov(newTagBox, schBox, 0.01) ? "YES" : "no").Append(tried0.Count > 0 ? " earlier: " + CapS(string.Join(" | ", tried0), 250) : "");
if (st2 == "todo") sb.Append("\nP1.01 tag 3242034 now box=").Append(bx(tag101Final)).Append(" centre=(").Append(f3(ctr2Used[0])).Append(",").Append(f3(ctr2Used[1])).Append(")").Append(tried.Count > 0 ? " earlier: " + CapS(string.Join(" | ", tried), 300) : "");
if (warn.Count > 0) sb.Append("\nwarn: ").Append(CapS(string.Join("; ", warn), 200));
if (mism.Count > 0)
{
    try { tg.RollBack(); } catch (Exception) { }
    string tailM = "\nMISMATCH: " + CapS(string.Join(" | ", mism), 600) + "\nROLLED_BACK readback\ndone=0 skipped=" + nSkip + " failed=" + mism.Count;
    return CapS(sb.ToString(), 3000 - tailM.Length) + tailM;
}
if (dry)
{
    try { tg.RollBack(); } catch (Exception) { }
    string tailD = "\ngroup: rolled back (dryrun); nothing remains in the model\ndone=0 skipped=" + nSkip + " failed=0 (dryrun: would_do=" + nDone + ")";
    return CapS(sb.ToString(), 3000 - tailD.Length) + tailD;
}
tg.Assimilate();
string tailR = "\ncleanup_params: [\"" + string.Join("\",\"", cp) + "\"]\ndone=" + nDone + " skipped=" + nSkip + " failed=0";
return CapS(sb.ToString(), 3000 - tailR.Length) + tailR;
}
public static string CapS(string s, int n) { return s.Length <= n ? s : s.Substring(0, n) + "..."; }
public static object DummyTail() { return null;
