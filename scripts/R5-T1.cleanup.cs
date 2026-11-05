// cleanup for R5-T1: restores crop boxes/annotation offsets of views and box centers of viewports. parameters = the "OLD:" strings from the run ("V|id|..." and "P|id|x,y,z"), items may be joined by " ;; "
var inv = System.Globalization.CultureInfo.InvariantCulture;
var items = new List<string>();
foreach (var o in parameters)
    foreach (var part in o.ToString().Split(new[] { ";;" }, StringSplitOptions.RemoveEmptyEntries))
        if (part.Trim().Length > 0) items.Add(part.Trim());
int views = 0, vps = 0; var bad = new List<string>();
foreach (var it in items)
{
    var seg = it.Split('|');
    if (seg.Length < 3) { bad.Add(it); continue; }
    long id = long.Parse(seg[1], inv);
    double[] n = seg[2].Split(',').Select(s => double.Parse(s, inv)).ToArray();
    if (seg[0] == "V")
    {
        View v = document.GetElement(new ElementId(id)) as View;
        double[] off = seg[3].Split(',').Select(s => double.Parse(s, inv)).ToArray();
        if (v == null || n.Length != 18 || off.Length != 4) { bad.Add(id.ToString()); continue; }
        BoundingBoxXYZ b = new BoundingBoxXYZ();
        Transform t = Transform.Identity;
        t.Origin = new XYZ(n[6], n[7], n[8]);
        t.BasisX = new XYZ(n[9], n[10], n[11]);
        t.BasisY = new XYZ(n[12], n[13], n[14]);
        t.BasisZ = new XYZ(n[15], n[16], n[17]);
        b.Transform = t;
        b.Min = new XYZ(n[0], n[1], n[2]);
        b.Max = new XYZ(n[3], n[4], n[5]);
        v.CropBox = b;
        ViewCropRegionShapeManager m = v.GetCropRegionShapeManager();
        if (m.CanHaveAnnotationCrop)
        {
            m.LeftAnnotationCropOffset = off[0];
            m.RightAnnotationCropOffset = off[1];
            m.TopAnnotationCropOffset = off[2];
            m.BottomAnnotationCropOffset = off[3];
        }
        views++;
    }
}
document.Regenerate();
foreach (var it in items)
{
    var seg = it.Split('|');
    if (seg.Length < 3 || seg[0] != "P") continue;
    Viewport vp = document.GetElement(new ElementId(long.Parse(seg[1], inv))) as Viewport;
    double[] n = seg[2].Split(',').Select(s => double.Parse(s, inv)).ToArray();
    if (vp == null || n.Length != 3) { bad.Add(seg[1]); continue; }
    vp.SetBoxCenter(new XYZ(n[0], n[1], n[2]));
    vps++;
}
return "restoredViews=" + views + " restoredViewports=" + vps + " problems=" + string.Join(",", bad.Take(10));
