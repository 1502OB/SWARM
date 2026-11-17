// add-sheet-note: from CB6-R1-T12 (23-073), verified
// CB6-R1-T12 | operation: add-sheet-text-note (+ revision cloud + Revision Tag) | run 20261109-1412 | Revit 2025 | v1
// Library basis: add-text-note-CB6-R1-T4.cs (cloud loop, R25 tag head and raise, read-back), on a SHEET (scale 1).
// Body of: public static object Execute(Document document, object[] parameters); send_code_to_revit transactionMode "auto".
// One SubTransaction inside the bridge transaction: any failure rolls back and returns "ROLLED_BACK <item>: <error>".
// Sheet P4.90: one TextNote, type 3/32" Arial Narrow, wrap width 4.5", Left/Top at sheet (1.05, 0.60) ft, exact text
// below; one CLOCKWISE cloud on revision 'FEWD Submittal' = measured text box (TextNote.Width x Height after Regenerate,
// R30) + 1/4"; one Revision Tag, head at (cloud right - 0.17", cloud top + 0.08"), raised until its box clears the cloud
// top by 0.08". Text, cloud and tag must stay in the free area x 1.0..3.2, y 0.10..0.70 ft and clear every live sheet
// item. Skips if a note with this exact text is already on the sheet. Confirms the revision is on the sheet afterwards.
// parameters: ["dryrun"] = read-only preflight and plan; [] or ["run"] = do it. No literal inch-mark quotes (Q = (char)34).
string Q = ((char)34).ToString();
bool dry = false;
if (parameters != null)
    foreach (object po in parameters)
        if (po != null && po.ToString().Trim().Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;
string TAIL0 = "\ndone=0 skipped=0 failed=0";
string SHEET_NO = "P4.90"; long SHEET_ID_REF = 13951052L;
string REV_DESC = "FEWD Submittal"; long REV_ID_REF = 15034302L;
string TN_NARROW = "3/32" + Q + " Arial Narrow"; long TN_NARROW_REF = 14085252L;
string TAG_FAMILY = "MEP_Tag_Revision", TAG_TYPE = "Revision Tag";
string NOTE_TEXT = "UNDERCOUNTER DISHWASHER SHALL NOT CONNECT TO THE GREASE INTERCEPTOR; ROUTED TO SANITARY WASTE PER FEWD REQUIREMENTS.";
double IN = 1.0 / 12.0;
double PX = 1.05, PY = 0.60, WRAP_IN = 4.5;
double[] FREE = new double[] { 1.0, 0.10, 3.2, 0.70 };
List<string> warn = new List<string>();

// ---------------- 1. Preflight (read-only) ----------------
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
if (sheet.Id.Value != SHEET_ID_REF) warn.Add("sheet id " + sheet.Id.Value + " (task " + SHEET_ID_REF + ")");
string[] cn = new string[] { "TextNotes", "RevisionClouds", "RevisionCloudTags" };
BuiltInCategory[] cats = new BuiltInCategory[] { BuiltInCategory.OST_TextNotes, BuiltInCategory.OST_RevisionClouds, BuiltInCategory.OST_RevisionCloudTags };
for (int ci = 0; ci < cats.Length; ci++)
    if (sheet.GetCategoryHidden(new ElementId(cats[ci]))) return "NO_GO " + cn[ci] + " hidden on " + SHEET_NO + TAIL0;

Revision rev = null; int revHits = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REV_DESC) { rev = r; revHits++; }
}
if (revHits != 1) return "NO_GO " + revHits + " revisions have Description '" + REV_DESC + "'" + TAIL0;
if (rev.Issued) return "NO_GO revision '" + REV_DESC + "' is issued" + TAIL0;
if (rev.Visibility == RevisionVisibility.Hidden) return "NO_GO revision '" + REV_DESC + "' Visibility=Hidden" + TAIL0;
if (rev.Id.Value != REV_ID_REF) warn.Add("revision id " + rev.Id.Value + " (task " + REV_ID_REF + ")");

TextNoteType tnt = null;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(TextNoteType)))
    if (e.Name == TN_NARROW) { tnt = e as TextNoteType; break; }
if (tnt == null) return "NO_GO text type '" + T12Esc(TN_NARROW) + "' not found" + TAIL0;
if (tnt.Id.Value != TN_NARROW_REF) warn.Add("text type id " + tnt.Id.Value + " (task " + TN_NARROW_REF + ")");
Parameter pSz = tnt.get_Parameter(BuiltInParameter.TEXT_SIZE);
double tSize = (pSz == null || pSz.AsDouble() <= 0) ? (0.09375 / 12.0) : pSz.AsDouble();
Parameter pWf = tnt.get_Parameter(BuiltInParameter.TEXT_WIDTH_SCALE);
double tWf = (pWf == null || pWf.AsDouble() <= 0) ? 1.0 : pWf.AsDouble();
double wrap = WRAP_IN * IN;
double minW = TextElement.GetMinimumAllowedWidth(document, tnt.Id), maxW = TextElement.GetMaximumAllowedWidth(document, tnt.Id);
if (wrap < minW || wrap > maxW) return "NO_GO wrap width 4.5 in outside the type's allowed range" + TAIL0;

FamilySymbol tagSym = null; List<FamilySymbol> tagAlt = new List<FamilySymbol>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol;
    if (fs == null) continue;
    string fam = fs.Family == null ? "" : fs.Family.Name;
    if (fam == TAG_FAMILY && fs.Name == TAG_TYPE) { tagSym = fs; break; }
    if (fam == TAG_TYPE || fs.Name == TAG_TYPE) tagAlt.Add(fs);
}
if (tagSym == null && tagAlt.Count > 0) { tagAlt.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name)); tagSym = tagAlt[0]; warn.Add("using tag symbol " + tagSym.Id.Value); }
if (tagSym == null) return "NO_GO no Revision Cloud Tag '" + TAG_FAMILY + ":" + TAG_TYPE + "'" + TAIL0;

// ---------------- 2. Idempotency ----------------
List<TextNote> same = new List<TextNote>();
foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(TextNote)))
{
    TextNote tn = e as TextNote;
    if (tn != null && tn.OwnerViewId.Value == sheet.Id.Value && T12Norm(tn.Text) == T12Norm(NOTE_TEXT)) same.Add(tn);
}
if (same.Count > 1) return "NO_GO " + same.Count + " notes with this text on " + SHEET_NO + TAIL0;
if (same.Count == 1)
{
    TextNote ex = same[0];
    double[] tb0 = T12TextBox(ex);
    RevisionCloud exC = null; IndependentTag exT = null;
    foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(RevisionCloud)))
    {
        RevisionCloud rc = e as RevisionCloud;
        if (rc == null || rc.RevisionId.Value != rev.Id.Value) continue;
        double[] cb = T12CloudBox(rc);
        if (cb != null && T12Inside(tb0, cb)) { exC = rc; break; }
    }
    if (exC != null)
        foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(IndependentTag)))
        {
            IndependentTag it = e as IndependentTag;
            try { if (it != null && it.GetTaggedLocalElementIds().Contains(exC.Id)) { exT = it; break; } } catch (Exception) { }
        }
    if (exC == null || exT == null)
        return "NO_GO note " + ex.Id.Value + " exists but cloud=" + (exC == null ? "none" : exC.Id.Value.ToString()) + " tag=" + (exT == null ? "none" : exT.Id.Value.ToString()) + "; run cleanup with its ids first" + TAIL0;
    return "{\"mode\":\"" + (dry ? "dryrun" : "run") + "\",\"status\":\"skipped: already in target state\",\"note\":" + ex.Id.Value + ",\"text_box\":" + T12Box(tb0) + ",\"cloud\":" + exC.Id.Value + ",\"tag\":" + exT.Id.Value + "}"
        + "\ndone=0 skipped=3 failed=0 skipped_ids=" + ex.Id.Value + "," + exC.Id.Value + "," + exT.Id.Value;
}

// ---------------- 3. Plan ----------------
int cpl = Math.Max(10, (int)Math.Floor(wrap / (tSize * tWf * 0.80)));
int cur = 0, estLines = 1;
foreach (string wd in NOTE_TEXT.Split(' '))
{
    int need = (cur == 0) ? wd.Length : cur + 1 + wd.Length;
    if (need > cpl && cur > 0) { estLines++; cur = wd.Length; } else cur = need;
}
double estH = estLines * tSize * 1.7;
double[] tagOff0 = new double[] { -0.12 * IN, -0.12 * IN, 0.12 * IN, 0.12 * IN };
double[] planTb = new double[] { PX, PY - estH, PX + wrap, PY };
double[] planL = T12Layout(planTb);
List<double[]> obs = new List<double[]>(); List<string> obsLbl = new List<string>(); List<string> obsSkip = new List<string>();
T12Obs(document, sheet, obs, obsLbl, obsSkip);
double[] planTag = new double[] { planL[4] - 0.12 * IN, planL[5], planL[4] + 0.12 * IN, planL[5] + 0.3 * IN };
List<string> planProb = T12Check(planTb, planL, planTag, FREE, obs, obsLbl);

System.Text.StringBuilder rep = new System.Text.StringBuilder();
rep.Append("{\"mode\":\"").Append(dry ? "dryrun" : "run").Append("\",\"sheet\":").Append(sheet.Id.Value).Append(",\"rev\":").Append(rev.Id.Value)
   .Append(",\"text_type\":").Append(tnt.Id.Value).Append(",\"tag_sym\":").Append(tagSym.Id.Value);
rep.Append(",\"est_lines\":").Append(estLines).Append(",\"plan_text_box\":").Append(T12Box(planTb)).Append(",\"plan_cloud\":").Append(T12Box(new double[] { planL[0], planL[1], planL[2], planL[3] }));
rep.Append(",\"plan_tag_head\":[").Append(T12F(planL[4])).Append(",").Append(T12F(planL[5])).Append("],\"obstacles\":").Append(obs.Count);
if (obsSkip.Count > 0) rep.Append(",\"obs_skipped\":\"").Append(T12Esc(string.Join("; ", obsSkip.Take(4)))).Append("\"");
if (warn.Count > 0) rep.Append(",\"warn\":\"").Append(T12Esc(string.Join("; ", warn))).Append("\"");
if (planProb.Count > 0)
{
    rep.Append(",\"status\":\"NO_GO plan: ").Append(T12Esc(string.Join("; ", planProb.Take(8)))).Append("\"}");
    return "NO_GO " + rep.ToString() + TAIL0;
}
if (dry) { rep.Append(",\"would_create\":3,\"status\":\"GO\"}"); return rep.ToString() + "\ndone=0 skipped=0 failed=0 (dryrun, nothing changed)"; }

// ---------------- 4. Create (SubTransaction) ----------------
string item = "note";
List<string> newIds = new List<string>();
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    TextNoteOptions opts = new TextNoteOptions(tnt.Id);
    opts.HorizontalAlignment = HorizontalTextAlignment.Left;
    opts.VerticalAlignment = VerticalTextAlignment.Top;
    TextNote note = TextNote.Create(document, sheet.Id, new XYZ(PX, PY, 0), wrap, NOTE_TEXT, opts);
    document.Regenerate();
    newIds.Add(note.Id.Value.ToString());
    if (T12Norm(note.Text) != T12Norm(NOTE_TEXT)) throw new Exception("text read back differs: " + T12Esc(note.Text));
    if (Math.Abs(note.Coord.X - PX) > 0.001 || Math.Abs(note.Coord.Y - PY) > 0.001) throw new Exception("note at (" + T12F(note.Coord.X) + "," + T12F(note.Coord.Y) + ")");
    double W = note.Width, H = note.Height;
    if (!(W > 0.9 * wrap && W < 1.1 * wrap)) throw new Exception("note width " + T12F(W * 12) + " in, expected 4.5");
    if (!(H > 0.3 * estH && H < 3.0 * estH)) { warn.Add("note height " + T12F(H * 12) + " in implausible, using estimate"); H = estH; }
    double[] tb = new double[] { PX, PY - H, PX + W, PY };
    double[] L = T12Layout(tb);
    rep.Append(",\"note\":").Append(note.Id.Value).Append(",\"text_box\":").Append(T12Box(tb)).Append(",\"measured_paper_in\":[").Append(T12F(W * 12)).Append(",").Append(T12F(H * 12)).Append("]");

    item = "cloud";
    double[] rect = new double[] { L[0], L[1], L[2], L[3] };
    XYZ bl = new XYZ(rect[0], rect[1], 0), tl = new XYZ(rect[0], rect[3], 0), tr = new XYZ(rect[2], rect[3], 0), br = new XYZ(rect[2], rect[1], 0);
    List<Curve> curves = new List<Curve>();
    curves.Add(Line.CreateBound(bl, tl));   // CLOCKWISE: BL > TL > TR > BR > BL
    curves.Add(Line.CreateBound(tl, tr));
    curves.Add(Line.CreateBound(tr, br));
    curves.Add(Line.CreateBound(br, bl));
    RevisionCloud cloud = RevisionCloud.Create(document, sheet, rev.Id, curves);
    document.Regenerate();
    newIds.Add(cloud.Id.Value.ToString());
    double area = 0;
    foreach (Curve cv in cloud.GetSketchCurves()) { XYZ a0 = cv.GetEndPoint(0), a1 = cv.GetEndPoint(1); area += a0.X * a1.Y - a1.X * a0.Y; }
    area /= 2.0;
    if (area >= 0) throw new Exception("cloud signed area " + T12F(area) + " is not negative");
    double[] rb = T12CloudBox(cloud);
    if (rb == null || Math.Abs(rb[0] - rect[0]) > 0.005 || Math.Abs(rb[3] - rect[3]) > 0.005) throw new Exception("cloud read back " + (rb == null ? "null" : T12Box(rb)));
    if (!cloud.GetSheetIds().Contains(sheet.Id)) throw new Exception("cloud GetSheetIds lacks the sheet");
    rep.Append(",\"cloud\":").Append(cloud.Id.Value).Append(",\"cloud_rect\":").Append(T12Box(rect)).Append(",\"signed_area\":").Append(T12F(area * 1000)).Append("e-3");

    item = "tag";
    if (!tagSym.IsActive) tagSym.Activate();
    XYZ head = new XYZ(L[4], L[5], 0);
    IndependentTag tag = IndependentTag.Create(document, tagSym.Id, sheet.Id, new Reference(cloud), false, TagOrientation.Horizontal, head);
    tag.TagHeadPosition = head;
    document.Regenerate();
    newIds.Add(tag.Id.Value.ToString());
    double clr = 0.08 * IN, step = 0.02 * IN, extra = 0;
    double[] tbx = T12TagBox(tag, sheet, tagOff0);
    if (tbx[1] < rect[3] + clr)
    {
        extra = Math.Ceiling((rect[3] + clr - tbx[1]) / step) * step;
        tag.TagHeadPosition = new XYZ(head.X, head.Y + extra, 0);
        document.Regenerate();
        tbx = T12TagBox(tag, sheet, tagOff0);
    }
    XYZ th = tag.TagHeadPosition;
    if (tbx[1] < rect[3] + clr - 0.001) throw new Exception("tag box bottom " + T12F(tbx[1]) + " within 0.08 in of cloud top " + T12F(rect[3]));
    if (Math.Abs(th.X - (rect[2] - 0.17 * IN)) > 0.002) throw new Exception("tag head x " + T12F(th.X) + " not at cloud right - 0.17 in");
    List<string> pr = T12Check(tb, L, tbx, FREE, obs, obsLbl);
    if (pr.Count > 0) throw new Exception("measured fit: " + string.Join("; ", pr.Take(6)));
    rep.Append(",\"tag\":").Append(tag.Id.Value).Append(",\"tag_head\":[").Append(T12F(th.X)).Append(",").Append(T12F(th.Y)).Append("],\"tag_box\":").Append(T12Box(tbx)).Append(",\"tag_raise_in\":").Append(T12F(extra * 12));

    item = "revision schedule";
    IList<ElementId> shRevs = sheet.GetAllRevisionIds();
    if (!shRevs.Contains(rev.Id)) throw new Exception("sheet GetAllRevisionIds lacks " + rev.Id.Value);
    rep.Append(",\"sheet_revs\":[").Append(string.Join(",", shRevs.Select(x => x.Value.ToString()))).Append("]");
    st.Commit();
}
catch (Exception ex)
{
    try { st.RollBack(); } catch (Exception) { }
    return "ROLLED_BACK " + item + ": " + T12Esc(ex.Message) + "\ndone=0 skipped=0 failed=1 (nothing created)";
}
if (warn.Count > 0) rep.Append(",\"warn2\":\"").Append(T12Esc(string.Join("; ", warn))).Append("\"");
rep.Append(",\"status\":\"OK\"}");
rep.Append("\ndone=").Append(newIds.Count).Append(" skipped=0 failed=0 created=").Append(string.Join(",", newIds));
rep.Append("\ncleanup_params=[\"").Append(string.Join("\",\"", newIds)).Append("\"]");
return rep.ToString();
}

// ---------------- helpers (CB6-R1-T12) ----------------
static string T12F(double v) { return Math.Round(v, 4).ToString(System.Globalization.CultureInfo.InvariantCulture); }
static string T12Box(double[] b) { return "[" + T12F(b[0]) + "," + T12F(b[1]) + "," + T12F(b[2]) + "," + T12F(b[3]) + "]"; }
static string T12Esc(string s) { return (s ?? "").Replace("\\", "\\\\").Replace(((char)34).ToString(), "\\" + ((char)34).ToString()).Replace("\r", " ").Replace("\n", " "); }
static string T12Norm(string s) { return (s ?? "").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\v", " ").Trim().ToUpperInvariant(); }
static bool T12Ov(double[] a, double[] b, double g) { return a[0] - g < b[2] && b[0] < a[2] + g && a[1] - g < b[3] && b[1] < a[3] + g; }
static bool T12Inside(double[] inner, double[] outer) { return inner[0] >= outer[0] - 1e-6 && inner[1] >= outer[1] - 1e-6 && inner[2] <= outer[2] + 1e-6 && inner[3] <= outer[3] + 1e-6; }
static double[] T12Grow(double[] a, double m) { return new double[] { a[0] - m, a[1] - m, a[2] + m, a[3] + m }; }
static double[] T12U(double[] a, double x, double y) { return new double[] { Math.Min(a[0], x), Math.Min(a[1], y), Math.Max(a[2], x), Math.Max(a[3], y) }; }
static double[] T12OBox(Outline o) { XYZ a = o.MinimumPoint, b = o.MaximumPoint; return new double[] { Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Max(a.X, b.X), Math.Max(a.Y, b.Y) }; }
static double[] T12CloudBox(RevisionCloud rc)
{
    double[] r = null;
    foreach (Curve cv in rc.GetSketchCurves())
        for (int k = 0; k < 2; k++) { XYZ p = cv.GetEndPoint(k); r = (r == null) ? new double[] { p.X, p.Y, p.X, p.Y } : T12U(r, p.X, p.Y); }
    return r;
}
static double[] T12TextBox(TextNote tn)
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
        r = T12U(r, c.X + b.X * ka * w + u.X * kb * h, c.Y + b.Y * ka * w + u.Y * kb * h);
    return r;
}
static void T12Obs(Document doc, ViewSheet sh, List<double[]> boxes, List<string> lbls, List<string> skipped)
{
    foreach (Element e in new FilteredElementCollector(doc, sh.Id).WhereElementIsNotElementType())
    {
        try
        {
            if (e.Category != null && e.Category.BuiltInCategory == BuiltInCategory.OST_TitleBlocks) continue;
            double[] b = null; string l = (e.Category == null ? e.GetType().Name : e.Category.Name) + " " + e.Id.Value;
            if (e is Viewport)
            {
                Viewport v = (Viewport)e;
                b = T12OBox(v.GetBoxOutline());
                Outline lo = v.GetLabelOutline();
                if (lo != null && !lo.IsEmpty) { double[] lb = T12OBox(lo); if (lb[2] - lb[0] > 1e-4 && lb[3] - lb[1] > 1e-4) { boxes.Add(lb); lbls.Add("label of " + l + " " + T12Box(lb)); } }
            }
            else if (e is TextNote) b = T12TextBox((TextNote)e);
            else if (e is RevisionCloud) { b = T12CloudBox((RevisionCloud)e); if (b != null) b = T12Grow(b, 0.05 / 12.0); }
            else
            {
                BoundingBoxXYZ bb = e.get_BoundingBox(sh);
                if (bb == null) continue;
                b = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
            }
            if (b == null) continue;
            if (b[2] - b[0] > 2.8 && b[3] - b[1] > 2.0) { skipped.Add(l + " (sheet-size)"); continue; }
            boxes.Add(b); lbls.Add(l + " " + T12Box(b));
        }
        catch (Exception) { }
    }
}
// [0..3] cloud rect (text + 1/4 in), [4,5] tag head (right - 0.17 in, top + 0.08 in)
static double[] T12Layout(double[] tb)
{
    double IN = 1.0 / 12.0;
    double[] r = T12Grow(tb, 0.25 * IN);
    return new double[] { r[0], r[1], r[2], r[3], r[2] - 0.17 * IN, r[3] + 0.08 * IN };
}
static List<string> T12Check(double[] tb, double[] L, double[] tag, double[] free, List<double[]> obs, List<string> lbl)
{
    double bulge = 0.05 / 12.0;
    List<string> p = new List<string>();
    double[] cr = T12Grow(new double[] { L[0], L[1], L[2], L[3] }, bulge);
    if (!T12Inside(cr, free)) p.Add("cloud " + T12Box(cr) + " not inside free area " + T12Box(free));
    if (!T12Inside(tag, free)) p.Add("tag " + T12Box(tag) + " not inside free area");
    for (int i = 0; i < obs.Count; i++)
    {
        if (T12Ov(cr, obs[i], 0)) p.Add("cloud x " + lbl[i]);
        else if (T12Ov(tag, obs[i], 0)) p.Add("tag x " + lbl[i]);
    }
    return p;
}
static double[] T12TagBox(IndependentTag tag, View v, double[] tagOff)
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
