// operation: align-viewports | R5-T1 | copies CropBox+annotation crop offsets of view 18215302 onto 3 views, then aligns 3 viewports so model point (232,184,0) lands at the same sheet XY as on ref viewport 18215890; parameters ["dryrun"] = read-only preflight
bool dry = parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString() == "dryrun";
var inv = System.Globalization.CultureInfo.InvariantCulture;
const double TOL = 0.001;
XYZ modelPt = new XYZ(232.0, 184.0, 0.0);
long refVpId = 18215890, refViewId = 18215302;
long[] viewIds = new long[] { 18215641, 22821417, 22821494 };
long[] vpIds = new long[] { 18215898, 22821568, 22821569 };
long[] sheetIds = new long[] { 18150983, 22821166, 22821212 };
Func<double, string> f = d => d.ToString("R", inv);
Func<XYZ, string> fx = p => f(p.X) + "," + f(p.Y) + "," + f(p.Z);
Func<XYZ, string> f2 = p => Math.Round(p.X, 4).ToString(inv) + "," + Math.Round(p.Y, 4).ToString(inv);

// ---- reference
Viewport refVp = document.GetElement(new ElementId(refVpId)) as Viewport;
if (refVp == null) throw new Exception("ROLLED_BACK ref viewport " + refVpId + " missing");
if (refVp.ViewId.Value != refViewId) throw new Exception("ROLLED_BACK ref viewport view " + refVp.ViewId.Value + " != " + refViewId);
View refView = document.GetElement(new ElementId(refViewId)) as View;
if (refView == null) throw new Exception("ROLLED_BACK ref view missing");
if (refVp.Rotation != ViewportRotation.None) throw new Exception("ROLLED_BACK ref viewport rotated");
ViewCropRegionShapeManager refMgr = refView.GetCropRegionShapeManager();
if (refMgr.ShapeSet || refMgr.Split) throw new Exception("ROLLED_BACK ref view crop non-rectangular/split");
BoundingBoxXYZ rc = refView.CropBox;
double rL = refMgr.LeftAnnotationCropOffset, rR = refMgr.RightAnnotationCropOffset, rT = refMgr.TopAnnotationCropOffset, rB = refMgr.BottomAnnotationCropOffset;
XYZ refSheetPt = refVp.GetProjectionToSheetTransform().OfPoint(modelPt);
XYZ refCenter = refVp.GetBoxCenter();

// ---- preflight each target
var problems = new List<string>();
var rep = new System.Text.StringBuilder();
for (int i = 0; i < 3; i++)
{
    Viewport vp = document.GetElement(new ElementId(vpIds[i])) as Viewport;
    View v = document.GetElement(new ElementId(viewIds[i])) as View;
    if (vp == null) { problems.Add(vpIds[i] + " viewport missing"); continue; }
    if (v == null) { problems.Add(viewIds[i] + " view missing"); continue; }
    if (vp.ViewId.Value != viewIds[i]) problems.Add(vpIds[i] + " shows view " + vp.ViewId.Value);
    if (vp.SheetId.Value != sheetIds[i]) problems.Add(vpIds[i] + " on sheet " + vp.SheetId.Value);
    if (vp.Pinned) problems.Add(vpIds[i] + " PINNED");
    if (vp.Rotation != ViewportRotation.None) problems.Add(vpIds[i] + " rotated " + vp.Rotation);
    if (v.Scale != refView.Scale) problems.Add(viewIds[i] + " scale " + v.Scale + " != " + refView.Scale);
    ViewCropRegionShapeManager m = v.GetCropRegionShapeManager();
    if (m.ShapeSet || m.Split) problems.Add(viewIds[i] + " non-rectangular or split crop");
    if (!m.CanHaveAnnotationCrop && refMgr.CanHaveAnnotationCrop) problems.Add(viewIds[i] + " cannot have annotation crop");
    if (v.ViewTemplateId != ElementId.InvalidElementId)
    {
        View t = document.GetElement(v.ViewTemplateId) as View;
        bool locks = true;
        try
        {
            var nc = t.GetNonControlledTemplateParameterIds();
            if (nc.Contains(new ElementId(BuiltInParameter.VIEWER_CROP_REGION))) locks = false;
        }
        catch { }
        if (locks) problems.Add(viewIds[i] + " view template '" + (t != null ? t.Name : "?") + "' controls crop");
    }
    XYZ cur = vp.GetProjectionToSheetTransform().OfPoint(modelPt);
    rep.Append(vpIds[i] + " center=" + f2(vp.GetBoxCenter()) + " refPt=" + f2(cur) + "; ");
}
if (problems.Count > 0) throw new Exception("ROLLED_BACK preflight: " + string.Join("; ", problems));

if (dry)
    return "DRYRUN ok ref vp " + refVpId + " center=" + f2(refCenter) + " refPt=" + f2(refSheetPt) + " refCrop min=" + f2(rc.Min) + " max=" + f2(rc.Max) + " off LRTB=" + f(rL) + "," + f(rR) + "," + f(rT) + "," + f(rB) + " | " + rep;

// ---- change
var oldViews = new List<string>();
var oldVps = new List<string>();
var before = new List<string>();
var after = new List<string>();
for (int i = 0; i < 3; i++)
{
    View v = (View)document.GetElement(new ElementId(viewIds[i]));
    Viewport vp = (Viewport)document.GetElement(new ElementId(vpIds[i]));
    ViewCropRegionShapeManager m = v.GetCropRegionShapeManager();
    BoundingBoxXYZ oc = v.CropBox;
    Transform ot = oc.Transform;
    oldViews.Add("V|" + viewIds[i] + "|" + fx(oc.Min) + "," + fx(oc.Max) + "," + fx(ot.Origin) + "," + fx(ot.BasisX) + "," + fx(ot.BasisY) + "," + fx(ot.BasisZ)
        + "|" + f(m.LeftAnnotationCropOffset) + "," + f(m.RightAnnotationCropOffset) + "," + f(m.TopAnnotationCropOffset) + "," + f(m.BottomAnnotationCropOffset));
    XYZ c0 = vp.GetBoxCenter();
    oldVps.Add("P|" + vpIds[i] + "|" + fx(c0));
    before.Add(vpIds[i] + ":" + f2(c0) + " pt=" + f2(vp.GetProjectionToSheetTransform().OfPoint(modelPt)));

    BoundingBoxXYZ nb = new BoundingBoxXYZ();
    nb.Transform = rc.Transform;
    nb.Min = rc.Min;
    nb.Max = rc.Max;
    v.CropBox = nb;
    if (m.CanHaveAnnotationCrop)
    {
        m.LeftAnnotationCropOffset = rL;
        m.RightAnnotationCropOffset = rR;
        m.TopAnnotationCropOffset = rT;
        m.BottomAnnotationCropOffset = rB;
    }
}
document.Regenerate();
for (int i = 0; i < 3; i++)
{
    Viewport vp = (Viewport)document.GetElement(new ElementId(vpIds[i]));
    XYZ pt = vp.GetProjectionToSheetTransform().OfPoint(modelPt);
    XYZ d = new XYZ(refSheetPt.X - pt.X, refSheetPt.Y - pt.Y, 0);
    if (d.GetLength() > 1e-9)
    {
        XYZ c = vp.GetBoxCenter();
        vp.SetBoxCenter(new XYZ(c.X + d.X, c.Y + d.Y, c.Z));
    }
}
document.Regenerate();
var bad = new List<string>();
for (int i = 0; i < 3; i++)
{
    Viewport vp = (Viewport)document.GetElement(new ElementId(vpIds[i]));
    View v = (View)document.GetElement(new ElementId(viewIds[i]));
    XYZ pt = vp.GetProjectionToSheetTransform().OfPoint(modelPt);
    double err = Math.Max(Math.Abs(pt.X - refSheetPt.X), Math.Abs(pt.Y - refSheetPt.Y));
    BoundingBoxXYZ nc = v.CropBox;
    double cdiff = (nc.Min - rc.Min).GetLength() + (nc.Max - rc.Max).GetLength();
    if (err > TOL) bad.Add(vpIds[i] + " ptErr=" + err);
    if (cdiff > 1e-6) bad.Add(viewIds[i] + " cropDiff=" + cdiff);
    after.Add(vpIds[i] + ":" + f2(vp.GetBoxCenter()) + " pt=" + f2(pt) + " err=" + err.ToString("E1", inv));
}
XYZ refAfter = refVp.GetProjectionToSheetTransform().OfPoint(modelPt);
if ((refAfter - refSheetPt).GetLength() > 1e-9 || (refVp.GetBoxCenter() - refCenter).GetLength() > 1e-9) bad.Add("reference viewport moved");
if (bad.Count > 0) throw new Exception("ROLLED_BACK verify: " + string.Join("; ", bad));

string res = "OK refVp " + refVpId + " center=" + f2(refCenter) + " refPt=" + f2(refSheetPt) + " | BEFORE " + string.Join(" ; ", before) + " | AFTER " + string.Join(" ; ", after)
    + " | OLD: " + string.Join(" ;; ", oldViews.Concat(oldVps));
if (res.Length > 2900) res = res.Substring(0, 2900);
return res;
