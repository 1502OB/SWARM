// revision-cloud: replace clouds with hull clouds sized from post-Regenerate bboxes, delta clear of arcs (R25)
// CB6-R1-T5 | operation: revision-cloud (delete T3 clouds + redraw from measured boxes) | run 20261109-1412 | Revit 2025 | v1
// Body of: public static object Execute(Document document, object[] parameters)
// send_code_to_revit, transactionMode "auto" (one outer transaction; the work runs in a SubTransaction so the script
// can roll back its own changes and still return "ROLLED_BACK ..."). Never saves or syncs.
// parameters: ["dryrun"] = read-only preflight: checks the 4 T3 elements, measures label/HEX boxes after
//                          document.Regenerate() (R30), plans the clouds, reports rectangles, loops, clearances, tag boxes.
//             []         = real run: delete T3 tags 15034369/15034385 and clouds 15034368/15034384, regenerate,
//                          re-measure, create one clockwise cloud per riser branch on 'FEWD Submittal' with >= 1.0 ft
//                          (1/4 in at 1:48) clearance to every label and HEX box, one Revision Tag per cloud at
//                          (right - 0.17 in, top + 0.08 in) raised as needed so the tag box clears the cloud arcs,
//                          post-check from the created sketch curves, roll back everything on any failure.
// Reuses CB6-R1-T3.v4.cs (cloud plan / outline trace / R25 tag head). Labels are NOT edited.
var inv = System.Globalization.CultureInfo.InvariantCulture;
string mode0 = (parameters != null && parameters.Length > 0 && parameters[0] != null) ? parameters[0].ToString().Trim().ToLowerInvariant() : "";
bool dry = mode0 == "dryrun";
string REVDESC = "FEWD Submittal";
long VIEWID = 14662318, SHEETID = 14662586, VPID = 14662592;
long[] OLDCLOUD = new long[] { 15034368, 15034384 };   // T3 clouds: [0] PREP KITCHEN, [1] BAR/LOUNGE
long[] OLDTAG = new long[] { 15034369, 15034385 };     // their Revision Tags
string[] BRNAME = new string[] { "PREP KITCHEN", "BAR/LOUNGE" };
long[] LBL = new long[] { 14680811, 14680841, 14680866, 14698172, 14698003 };
string[] FNUM = new string[] { "45", "48", "50", "53", "55" };
string[][] HEXNUM = new string[][] { new string[] { "97", "98", "100" }, new string[] { "83", "84", "87", "89", "92" }, new string[] { "115" }, new string[] { "230" }, new string[] { "224", "225", "226", "227" } };
long[][] HEXID = new long[][] { new long[] { 14680817, 14680812, 14680810 }, new long[] { 14680844, 14680842, 14680843, 14680845, 14680846 }, new long[] { 14680867 }, new long[] { 14698171 }, new long[] { 14698002, 14698943, 14698004, 14698010 } };
int[] GRP = new int[] { 0, 0, 0, 1, 1 };
int CAP = 40;
List<string> warn = new List<string>();
System.Text.StringBuilder sb = new System.Text.StringBuilder();
sb.Append(dry ? "DRYRUN CB6-R1-T5 v1" : "RUN CB6-R1-T5 v1");

// ---- 0. document
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return CB6NoGo("wrong document open: " + title);
sb.Append(" modifiable=").Append(document.IsModifiable ? "yes" : "no");

// ---- 1. view, viewport, sheet, live constants
View view = document.GetElement(new ElementId(VIEWID)) as View;
if (view == null || !(view is ViewDrafting)) return CB6NoGo("view " + VIEWID + " missing or not a drafting view");
int scale = view.Scale;
if (scale != 48) warn.Add("view scale 1:" + scale + " (task says 1:48; offsets use the live scale)");
double PI_ = scale / 12.0; // model ft per paper inch
double minClr = 0.25 * PI_;          // required clearance label/HEX box -> cloud boundary (1.0 ft at 1:48)
double margin = minClr + 0.05;       // planned clearance (buffer for rounding)
double inClr = 0.03 * PI_, dxTag = 0.17 * PI_, dyTag = 0.08 * PI_;
double spIn = 0.125, clrIn = 0.10;
try
{
    RevisionSettings rset = RevisionSettings.GetRevisionSettings(document);
    spIn = rset.RevisionCloudSpacing * 12.0;
    clrIn = Math.Max(0.10, Math.Min(0.25, 0.4 * spIn));
}
catch (Exception) { warn.Add("RevisionCloudSpacing unreadable; spacing 0.125 in assumed"); }
double clr = clrIn * PI_;
double gapIn = Math.Max(0.06, 0.5 * spIn + 0.03); // tag box bottom above the cloud's top line: arc bulge (<= spacing/2) + 0.03 in
double gapReq = gapIn * PI_;
double maxDim = 15.0 * PI_;
sb.Append(" cloud_spacing_in=").Append(Math.Round(spIn, 3).ToString(inv)).Append(" gap_req_ft=").Append(Math.Round(gapReq, 3).ToString(inv));
Viewport vp = document.GetElement(new ElementId(VPID)) as Viewport;
if (vp == null || vp.ViewId.Value != VIEWID || vp.SheetId.Value != SHEETID) return CB6NoGo("viewport " + VPID + " does not place view " + VIEWID + " on sheet " + SHEETID);
ViewSheet sheet = document.GetElement(vp.SheetId) as ViewSheet;
string sheetNo = sheet != null ? sheet.SheetNumber : "?";
if (sheetNo != "P6.01") warn.Add("sheet number is " + sheetNo + " (task says P6.01)");
if (vp.Rotation != ViewportRotation.None) warn.Add("viewport rotated " + vp.Rotation.ToString());
sb.Append(" view=").Append(VIEWID).Append(" scale=1:").Append(scale).Append(" sheet=").Append(sheetNo);

// ---- 2. revision by Description
Revision rev = null; int nrev = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REVDESC) { nrev++; if (rev == null) rev = r; }
}
if (nrev != 1) return CB6NoGo(nrev + " revisions have Description 'FEWD Submittal' (need exactly 1)");
if (rev.Issued) return CB6NoGo("revision 'FEWD Submittal' (id " + rev.Id.Value + ") is issued");
if (rev.Visibility == RevisionVisibility.Hidden) warn.Add("revision visibility is Hidden");
sb.Append(" rev=").Append(rev.Id.Value).Append(" seq=").Append(rev.SequenceNumber);

// ---- 3. Revision Tag type (R31: family MEP_Tag_Revision, type 'Revision Tag'; match either)
FamilySymbol tagSym = null; List<string> tagNames = new List<string>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol; if (fs == null) continue;
    tagNames.Add(fs.FamilyName + ":" + fs.Name);
    if ((fs.FamilyName == "Revision Tag" || fs.Name == "Revision Tag") && (tagSym == null || fs.Id.Value < tagSym.Id.Value)) tagSym = fs;
}
if (tagSym == null) return CB6NoGo("no 'Revision Tag' in Revision Cloud Tags; found: " + string.Join(", ", tagNames.Take(5)));
sb.Append(" tag_type=").Append(tagSym.Id.Value).Append(" '").Append(tagSym.FamilyName).Append(":").Append(tagSym.Name).Append("'");

// ---- 4. worksharing ownership
if (document.IsWorkshared)
{
    List<long> chk = new List<long>(LBL); chk.Add(VIEWID); chk.AddRange(OLDCLOUD); chk.AddRange(OLDTAG);
    List<string> owned = new List<string>();
    foreach (long id in chk)
    {
        if (document.GetElement(new ElementId(id)) == null) continue;
        if (WorksharingUtils.GetCheckoutStatus(document, new ElementId(id)) == CheckoutStatus.OwnedByOtherUser) owned.Add(id.ToString());
    }
    if (owned.Count > 0) return CB6NoGo("owned by another user: " + string.Join(",", owned));
}

// ---- 5. regenerate before measuring (LESSONS R30: boxes/values are stale until Regenerate)
string regen = "no";
if (document.IsModifiable)
{
    try { document.Regenerate(); regen = "yes"; }
    catch (Exception rx) { warn.Add("Regenerate failed: " + CB6Cap(rx.Message, 100)); }
}
else warn.Add("document not modifiable: boxes measured without Regenerate");
sb.Append(" regenerated=").Append(regen);

// ---- 6. the 4 T3 elements: class, owner view, revision / tagged cloud; old tag geometry for the tag-box model
string[] cState = new string[2], tState = new string[2];
double tL = 0, tR = 0, tB = -1e9, tA = -1e9; int nTg = 0; List<string> tgMeas = new List<string>();
for (int b = 0; b < 2; b++)
{
    Element ce = document.GetElement(new ElementId(OLDCLOUD[b]));
    if (ce == null) cState[b] = "gone";
    else
    {
        RevisionCloud rc = ce as RevisionCloud;
        if (rc == null) return CB6NoGo("element " + OLDCLOUD[b] + " is not a RevisionCloud (" + ce.GetType().Name + ")");
        if (rc.OwnerViewId.Value != VIEWID) return CB6NoGo("cloud " + OLDCLOUD[b] + " owner view " + rc.OwnerViewId.Value);
        if (rc.RevisionId.Value != rev.Id.Value) return CB6NoGo("cloud " + OLDCLOUD[b] + " revision " + rc.RevisionId.Value + " is not FEWD Submittal " + rev.Id.Value);
        cState[b] = "present";
    }
    Element te = document.GetElement(new ElementId(OLDTAG[b]));
    if (te == null) tState[b] = "gone";
    else
    {
        IndependentTag tg0 = te as IndependentTag;
        if (tg0 == null) return CB6NoGo("element " + OLDTAG[b] + " is not an IndependentTag (" + te.GetType().Name + ")");
        if (tg0.Category == null || tg0.Category.BuiltInCategory != BuiltInCategory.OST_RevisionCloudTags) return CB6NoGo("tag " + OLDTAG[b] + " is not a Revision Cloud Tag");
        if (tg0.OwnerViewId.Value != VIEWID) return CB6NoGo("tag " + OLDTAG[b] + " owner view " + tg0.OwnerViewId.Value);
        ISet<ElementId> tids = tg0.GetTaggedLocalElementIds();
        bool okT = tids == null || tids.Count == 0;
        if (tids != null) foreach (ElementId x in tids) if (x.Value == OLDCLOUD[b]) okT = true;
        if (!okT) return CB6NoGo("tag " + OLDTAG[b] + " does not tag cloud " + OLDCLOUD[b]);
        tState[b] = "present";
        XYZ hp = tg0.TagHeadPosition; BoundingBoxXYZ tbb = tg0.get_BoundingBox(view);
        if (hp != null && tbb != null)
        {
            double l = hp.X - tbb.Min.X, r = tbb.Max.X - hp.X, bl = hp.Y - tbb.Min.Y, ab = tbb.Max.Y - hp.Y;
            tgMeas.Add(OLDTAG[b] + ":L" + Math.Round(l, 2).ToString(inv) + "/R" + Math.Round(r, 2).ToString(inv) + "/B" + Math.Round(bl, 2).ToString(inv) + "/A" + Math.Round(ab, 2).ToString(inv));
            if (l + r > 0.01 && bl + ab > 0.01 && Math.Abs(l) < PI_ && Math.Abs(r) < PI_ && Math.Abs(bl) < PI_ && Math.Abs(ab) < PI_)
            { tL = Math.Max(tL, l); tR = Math.Max(tR, r); tB = Math.Max(tB, bl); tA = Math.Max(tA, ab); nTg++; }
        }
    }
}
string tagSrc = "measured(" + string.Join(",", tgMeas) + ")";
if (nTg == 0) { tL = 0.13 * PI_; tR = 0.13 * PI_; tB = 0.10 * PI_; tA = 0.10 * PI_; tagSrc = "default(0.26x0.20 in, head centred)"; }
double dyEff = Math.Max(dyTag, gapReq + tB); // head offset above the top line so the tag bottom clears the arcs
double[] TG = new double[] { dxTag, dyEff, tL, tR, tB, tA };
sb.Append("\nold: cloud ").Append(OLDCLOUD[0]).Append("=").Append(cState[0]).Append(" tag ").Append(OLDTAG[0]).Append("=").Append(tState[0])
  .Append(" cloud ").Append(OLDCLOUD[1]).Append("=").Append(cState[1]).Append(" tag ").Append(OLDTAG[1]).Append("=").Append(tState[1]);
sb.Append("\ntag_box ").Append(tagSrc).Append(" head_dy_ft=").Append(Math.Round(dyEff, 3).ToString(inv)).Append(" (R25 0.32)");

// ---- 7. labels (validated, not edited) and HEX tags, boxes measured after Regenerate
string[] state = new string[LBL.Length];
CB6Box[] lb = new CB6Box[LBL.Length];
double zPl = 0; bool zSet = false; List<string> bad = new List<string>();
for (int i = 0; i < LBL.Length; i++)
{
    FamilyInstance fi = document.GetElement(new ElementId(LBL[i])) as FamilyInstance;
    if (fi == null) { bad.Add(LBL[i] + " not found"); continue; }
    if (fi.OwnerViewId.Value != VIEWID) { bad.Add(LBL[i] + " owner view " + fi.OwnerViewId.Value); continue; }
    if (fi.Category == null || fi.Category.BuiltInCategory != BuiltInCategory.OST_GenericAnnotation) { bad.Add(LBL[i] + " not generic annotation"); continue; }
    Parameter p = fi.LookupParameter("Note Text");
    string cur = p != null ? (p.AsString() ?? "") : "";
    if (!cur.EndsWith(" FS-1 (F-" + FNUM[i] + ")", StringComparison.Ordinal)) warn.Add("label " + LBL[i] + " Note Text is '" + cur + "' (T3 value expected)");
    BoundingBoxXYZ bb = fi.get_BoundingBox(view);
    if (bb == null) { bad.Add(LBL[i] + " no bbox"); continue; }
    lb[i] = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, LBL[i]);
    if (!zSet) { zPl = bb.Min.Z; zSet = true; }
    state[i] = "todo";
}
if (bad.Count > 0) return CB6NoGo("labels: " + string.Join("; ", bad));
List<FamilyInstance> hexCands = new List<FamilyInstance>();
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(FamilyInstance)))
{
    FamilyInstance fi = e as FamilyInstance;
    if (fi == null || fi.Category == null || fi.Category.BuiltInCategory != BuiltInCategory.OST_GenericAnnotation || fi.Symbol == null) continue;
    if ((fi.Symbol.FamilyName + ":" + fi.Symbol.Name).ToUpperInvariant().IndexOf("HEX") >= 0) hexCands.Add(fi);
}
List<long>[] hexIds = new List<long>[LBL.Length];
List<CB6Box>[] hexBoxes = new List<CB6Box>[LBL.Length];
HashSet<long> usedHex = new HashSet<long>();
for (int i = 0; i < LBL.Length; i++)
{
    hexIds[i] = new List<long>(); hexBoxes[i] = new List<CB6Box>();
    double lcx = 0.5 * (lb[i].x0 + lb[i].x1), lcy = 0.5 * (lb[i].y0 + lb[i].y1);
    for (int k = 0; k < HEXNUM[i].Length; k++)
    {
        string num = HEXNUM[i][k]; long hid = HEXID[i][k];
        FamilyInstance h = hid > 0 ? document.GetElement(new ElementId(hid)) as FamilyInstance : null;
        if (h != null && h.OwnerViewId.Value != VIEWID) { warn.Add("hex " + hid + " not in view"); h = null; }
        if (h != null && !CB6HasVal(h, num)) warn.Add("hex " + hid + " value not confirmed as #" + num);
        if (h == null)
        {
            double bestD = 25.0; FamilyInstance best = null;
            foreach (FamilyInstance c in hexCands)
            {
                if (usedHex.Contains(c.Id.Value) || !CB6HasVal(c, num)) continue;
                BoundingBoxXYZ cb = c.get_BoundingBox(view); if (cb == null) continue;
                double d = Math.Sqrt(Math.Pow(0.5 * (cb.Min.X + cb.Max.X) - lcx, 2) + Math.Pow(0.5 * (cb.Min.Y + cb.Max.Y) - lcy, 2));
                if (d < bestD) { bestD = d; best = c; }
            }
            if (best == null) { warn.Add("hex #" + num + " for " + LBL[i] + " not found within 25 ft"); continue; }
            h = best; warn.Add("hex #" + num + " for " + LBL[i] + " found by value: " + h.Id.Value);
        }
        BoundingBoxXYZ hb = h.get_BoundingBox(view);
        if (hb == null) { warn.Add("hex " + h.Id.Value + " has no bbox"); continue; }
        usedHex.Add(h.Id.Value); hexIds[i].Add(h.Id.Value);
        hexBoxes[i].Add(new CB6Box(hb.Min.X, hb.Min.Y, hb.Max.X, hb.Max.Y, h.Id.Value));
    }
}
sb.Append("\nboxes:");
for (int i = 0; i < LBL.Length; i++)
    sb.Append(" ").Append(LBL[i]).Append("=").Append(Math.Round(lb[i].x1 - lb[i].x0, 2).ToString(inv)).Append("x").Append(Math.Round(lb[i].y1 - lb[i].y0, 2).ToString(inv))
      .Append("+hex").Append(hexIds[i].Count);

// ---- 8. other clouds in the view (the T3 clouds are excluded: they are deleted); branch already done?
List<CB6Box> hard = new List<CB6Box>(); List<List<double[]>> fewdSegs = new List<List<double[]>>(); List<long> fewdIdsOther = new List<long>();
HashSet<long> oldSet = new HashSet<long>(OLDCLOUD); foreach (long t in OLDTAG) oldSet.Add(t);
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(RevisionCloud)))
{
    RevisionCloud rc = e as RevisionCloud; if (rc == null || oldSet.Contains(rc.Id.Value)) continue;
    CB6Box b = CB6CloudBox(rc); if (b == null) continue;
    hard.Add(b);
    if (rc.RevisionId.Value == rev.Id.Value) { fewdSegs.Add(T5SegsRc(rc)); fewdIdsOther.Add(rc.Id.Value); }
}
bool[] brAlready = new bool[2]; long[] brAlreadyId = new long[2];
for (int g = 0; g < 2; g++)
    for (int f = 0; f < fewdSegs.Count && !brAlready[g]; f++)
    {
        bool all = true;
        for (int i = 0; i < LBL.Length && all; i++)
        {
            if (GRP[i] != g) continue;
            if (T5Clear(lb[i], fewdSegs[f]) < minClr - 0.05) all = false;
            foreach (CB6Box hb in hexBoxes[i]) if (T5Clear(hb, fewdSegs[f]) < minClr - 0.05) all = false;
        }
        if (all) { brAlready[g] = true; brAlreadyId[g] = fewdIdsOther[f]; }
    }
bool[] covered = new bool[LBL.Length];
for (int i = 0; i < LBL.Length; i++) covered[i] = brAlready[GRP[i]];
sb.Append("\nother_clouds_in_view=").Append(hard.Count).Append(" other_fewd=").Append(fewdSegs.Count);
for (int g = 0; g < 2; g++) if (brAlready[g]) sb.Append(" ").Append(BRNAME[g]).Append(":already(cloud ").Append(brAlreadyId[g]).Append(")");

// ---- 9. obstacles: text notes, tags, generic annotations that are not labels/HEX and not the T3 tags
HashSet<long> memberSet = new HashSet<long>(LBL);
for (int i = 0; i < LBL.Length; i++) foreach (long h in hexIds[i]) memberSet.Add(h);
List<CB6Box> obs = new List<CB6Box>(); int nBig = 0;
foreach (Element e in new FilteredElementCollector(document, view.Id).WhereElementIsNotElementType())
{
    if (memberSet.Contains(e.Id.Value) || oldSet.Contains(e.Id.Value) || e is RevisionCloud || e.Category == null) continue;
    CB6Box b = null;
    if (e is TextNote) b = CB6TextBox((TextNote)e, view, scale, document);
    else if (e is IndependentTag || e.Category.BuiltInCategory == BuiltInCategory.OST_GenericAnnotation)
    {
        BoundingBoxXYZ bb = e.get_BoundingBox(view);
        if (bb != null) b = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, e.Id.Value);
    }
    if (b == null) continue;
    if (b.x1 - b.x0 > 10.0 * PI_ || b.y1 - b.y0 > 10.0 * PI_) { nBig++; continue; }
    obs.Add(b);
}
sb.Append(" obstacles=").Append(obs.Count).Append(" big_skipped=").Append(nBig);
int nOldPresent = 0; for (int b = 0; b < 2; b++) { if (cState[b] == "present") nOldPresent++; if (tState[b] == "present") nOldPresent++; }

// ---- 10. DRY RUN
if (dry)
{
    List<CB6Clu> cl = CB6Build(LBL, state, covered, lb, hexIds, hexBoxes, margin, GRP);
    string perr = CB6Plan(cl, obs, hard, margin, clr, inClr, TG, maxDim, CAP);
    sb.Append("\n");
    if (perr != null) sb.Append("PLAN NO_GO: ").Append(perr).Append("\n");
    else foreach (CB6Clu c in cl) sb.Append(T5Report(c, lb, LBL, hexIds, hexBoxes, TG, inv)).Append("\n");
    if (warn.Count > 0) sb.Append("warn: ").Append(CB6Cap(string.Join("; ", warn), 500)).Append("\n");
    int sk = (4 - nOldPresent); for (int g = 0; g < 2; g++) if (brAlready[g]) sk++;
    sb.Append("done=0 skipped=").Append(sk).Append(" failed=").Append(perr != null ? 1 : 0)
      .Append(" (dryrun: would_delete=").Append(nOldPresent).Append(" would_clouds=").Append(perr == null ? cl.Count : 0).Append(")");
    return CB6Cap(sb.ToString(), 3200);
}

// ---- 11. RUN: delete + recreate, all or nothing in one SubTransaction
string item = "start";
List<string> doneIds = new List<string>(), skipIds = new List<string>(), cleanup = new List<string>(), cloudLines = new List<string>();
for (int b = 0; b < 2; b++)
{
    if (tState[b] == "gone") skipIds.Add("del:" + OLDTAG[b] + "(already gone)");
    if (cState[b] == "gone") skipIds.Add("del:" + OLDCLOUD[b] + "(already gone)");
}
for (int g = 0; g < 2; g++) if (brAlready[g]) skipIds.Add("cloud:" + BRNAME[g] + "(already in target state: cloud " + brAlreadyId[g] + ")");
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    // 11a. delete tags first, then clouds; confirm each is gone
    List<long> delOrder = new List<long>();
    for (int b = 0; b < 2; b++) if (tState[b] == "present") delOrder.Add(OLDTAG[b]);
    for (int b = 0; b < 2; b++) if (cState[b] == "present") delOrder.Add(OLDCLOUD[b]);
    foreach (long id in delOrder)
    {
        item = "delete " + id;
        if (document.GetElement(new ElementId(id)) != null) document.Delete(new ElementId(id));
        if (document.GetElement(new ElementId(id)) != null) throw new Exception("element " + id + " still exists after Delete");
        doneIds.Add("del:" + id);
    }
    item = "regenerate";
    document.Regenerate();
    // 11b. re-measure every label / HEX box after the delete + Regenerate
    item = "remeasure";
    for (int i = 0; i < LBL.Length; i++)
    {
        BoundingBoxXYZ bb = document.GetElement(new ElementId(LBL[i])).get_BoundingBox(view);
        if (bb == null) throw new Exception("label " + LBL[i] + " has no bbox after regenerate");
        CB6Box nb = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, LBL[i]);
        if (Math.Abs(nb.x0 - lb[i].x0) + Math.Abs(nb.x1 - lb[i].x1) + Math.Abs(nb.y0 - lb[i].y0) + Math.Abs(nb.y1 - lb[i].y1) > 0.02) warn.Add("label " + LBL[i] + " box changed after delete");
        lb[i] = nb;
        for (int k = 0; k < hexIds[i].Count; k++)
        {
            BoundingBoxXYZ hb = document.GetElement(new ElementId(hexIds[i][k])).get_BoundingBox(view);
            if (hb == null) throw new Exception("hex " + hexIds[i][k] + " has no bbox after regenerate");
            hexBoxes[i][k] = new CB6Box(hb.Min.X, hb.Min.Y, hb.Max.X, hb.Max.Y, hexIds[i][k]);
        }
    }
    item = "plan";
    List<CB6Clu> cl = CB6Build(LBL, state, covered, lb, hexIds, hexBoxes, margin, GRP);
    string perr = CB6Plan(cl, obs, hard, margin, clr, inClr, TG, maxDim, CAP);
    if (perr != null) throw new Exception("NO_GO " + perr);
    List<IndependentTag> tags = new List<IndependentTag>(); List<RevisionCloud> clouds = new List<RevisionCloud>(); List<CB6Clu> made = new List<CB6Clu>();
    foreach (CB6Clu c in cl)
    {
        item = "cloud " + string.Join("/", c.labels);
        double area = CB6Area(c.loop);
        if (area >= 0) throw new Exception("loop not clockwise (area " + area + ")");
        List<Curve> curves = new List<Curve>();
        for (int k = 0; k < c.loop.Count; k++)
        {
            double[] a = c.loop[k], b2 = c.loop[(k + 1) % c.loop.Count];
            curves.Add(Line.CreateBound(new XYZ(a[0], a[1], zPl), new XYZ(b2[0], b2[1], zPl)));
        }
        RevisionCloud rc = RevisionCloud.Create(document, view, rev.Id, curves);
        item = "tag " + string.Join("/", c.labels);
        double[] hd = CB6TagHead(c.rects, TG[0], TG[1]);
        IndependentTag tg = IndependentTag.Create(document, tagSym.Id, view.Id, new Reference(rc), false, TagOrientation.Horizontal, new XYZ(hd[0], hd[1], zPl));
        tags.Add(tg); clouds.Add(rc); made.Add(c);
        doneIds.Add("cloud:" + rc.Id.Value); cleanup.Add("tag:" + tg.Id.Value); cleanup.Add("cloud:" + rc.Id.Value);
    }
    item = "post-check";
    document.Regenerate();
    List<List<double[]>> madeSegs = new List<List<double[]>>();
    foreach (RevisionCloud rc in clouds) madeSegs.Add(T5SegsRc(rc));
    for (int n = 0; n < clouds.Count; n++)
    {
        CB6Clu c = made[n]; List<double[]> sg = madeSegs[n];
        item = "post-check cloud " + clouds[n].Id.Value;
        double sk = CB6SketchArea(clouds[n]);
        if (sk >= 0) throw new Exception("sketch area " + sk + " not clockwise");
        // clearance of every label / HEX box of this cloud, measured from the created sketch curves
        double mn = double.MaxValue; long mnId = 0; List<string> clrs = new List<string>();
        for (int i = 0; i < LBL.Length; i++)
        {
            if (!c.labels.Contains(LBL[i])) continue;
            List<CB6Box> bx = new List<CB6Box>(); bx.Add(lb[i]); bx.AddRange(hexBoxes[i]);
            double li = double.MaxValue;
            foreach (CB6Box b in bx)
            {
                double d = T5Clear(b, sg);
                if (d < minClr - 0.02) throw new Exception("box " + b.id + " clearance " + Math.Round(d, 3) + " ft < " + Math.Round(minClr, 3));
                if (d < mn) { mn = d; mnId = b.id; }
                li = Math.Min(li, d);
            }
            clrs.Add(LBL[i] + ":" + Math.Round(li, 2).ToString(inv));
        }
        // delta tag: box clearly outside the cloud, bottom >= gapReq above the top line (clears the arcs)
        item = "post-check tag " + tags[n].Id.Value;
        CB6Box hu = CB6Hull(c.rects, 0);
        double[] hd = CB6TagHead(c.rects, TG[0], TG[1]); double top = hd[2];
        double moved = 0;
        for (int pass = 0; pass < 2; pass++)
        {
            BoundingBoxXYZ tb = tags[n].get_BoundingBox(view);
            if (tb == null) throw new Exception("tag has no bbox");
            CB6Box tbx = new CB6Box(tb.Min.X, tb.Min.Y, tb.Max.X, tb.Max.Y, tags[n].Id.Value);
            double gap = T5BoxSegs(tbx, sg);
            bool inside = T5In(0.5 * (tbx.x0 + tbx.x1), 0.5 * (tbx.y0 + tbx.y1), sg);
            bool okGap = !inside && gap >= gapReq - 0.005 && tbx.y0 >= top + gapReq - 0.005;
            if (okGap) break;
            if (pass == 1) throw new Exception("tag box still within " + Math.Round(gap, 3) + " ft of cloud (need " + Math.Round(gapReq, 3) + ")");
            double up = (top + gapReq + 0.02) - tbx.y0;
            if (up <= 0) up = gapReq - gap + 0.02;
            if (up > 0.5 * PI_) throw new Exception("tag would need to move up " + Math.Round(up, 2) + " ft (> 0.5 in paper)");
            XYZ hp0 = tags[n].TagHeadPosition;
            tags[n].TagHeadPosition = new XYZ(hp0.X, hp0.Y + up, hp0.Z);
            moved += up;
            document.Regenerate();
        }
        BoundingBoxXYZ tbF = tags[n].get_BoundingBox(view);
        CB6Box tf = new CB6Box(tbF.Min.X, tbF.Min.Y, tbF.Max.X, tbF.Max.Y, tags[n].Id.Value);
        foreach (CB6Box hb in hard) if (CB6Ov(CB6Ex(tf, clr), hb)) throw new Exception("tag overlaps existing cloud " + hb.id);
        for (int m = 0; m < clouds.Count; m++)
        {
            if (m == n) continue;
            if (T5BoxSegs(tf, madeSegs[m]) < clr || T5In(0.5 * (tf.x0 + tf.x1), 0.5 * (tf.y0 + tf.y1), madeSegs[m])) throw new Exception("tag touches new cloud " + clouds[m].Id.Value);
        }
        foreach (CB6Box o in obs)
        {
            if (!CB6Ov(tf, o)) continue;
            bool insideAny = false; foreach (CB6Clu c2 in made) if (CB6InU(o, c2.rects)) insideAny = true;
            if (!insideAny) throw new Exception("tag box overlaps " + o.id);
        }
        XYZ hpF = tags[n].TagHeadPosition;
        cloudLines.Add("cloud " + clouds[n].Id.Value + " tag " + tags[n].Id.Value + " labels=" + string.Join("/", c.labels) + " " + c.mode + " n=" + c.loop.Count
            + " area=" + Math.Round(sk, 2).ToString(inv)
            + " hull=(" + Math.Round(hu.x0, 2).ToString(inv) + "," + Math.Round(hu.y0, 2).ToString(inv) + "," + Math.Round(hu.x1, 2).ToString(inv) + "," + Math.Round(hu.y1, 2).ToString(inv) + ")"
            + " size_in=" + Math.Round((hu.x1 - hu.x0) / PI_, 2).ToString(inv) + "x" + Math.Round((hu.y1 - hu.y0) / PI_, 2).ToString(inv)
            + " v0=(" + Math.Round(c.loop[0][0], 2).ToString(inv) + "," + Math.Round(c.loop[0][1], 2).ToString(inv) + ")"
            + " min_clr=" + Math.Round(mn, 3).ToString(inv) + "(" + mnId + ") clr=" + string.Join(",", clrs)
            + " head=(" + Math.Round(hpF.X, 2).ToString(inv) + "," + Math.Round(hpF.Y, 2).ToString(inv) + ") head_dx=" + Math.Round(hpF.X - hd[0] - TG[0], 3).ToString(inv)
            + " head_dy=" + Math.Round(hpF.Y - top, 3).ToString(inv) + " tag_gap=" + Math.Round(tf.y0 - top, 3).ToString(inv) + " moved_up=" + Math.Round(moved, 3).ToString(inv)
            + (c.added.Count > 0 ? " enclosed=" + string.Join("/", c.added.Take(15)) : ""));
    }
    st.Commit();
}
catch (Exception ex)
{
    if (st.HasStarted() && !st.HasEnded()) st.RollBack();
    return "ROLLED_BACK " + item + ": " + CB6Cap(ex.Message, 1400) + "\ndone=0 skipped=" + skipIds.Count + " failed=1";
}
sb.Append("\n");
foreach (string s in cloudLines) sb.Append(CB6Cap(s, 700)).Append("\n");
if (warn.Count > 0) sb.Append("warn: ").Append(CB6Cap(string.Join("; ", warn), 400)).Append("\n");
sb.Append("cleanup_params: [\"").Append(string.Join("\",\"", cleanup)).Append("\"]\n");
sb.Append("done=").Append(doneIds.Count).Append(" skipped=").Append(skipIds.Count).Append(" failed=0\n");
sb.Append("done_ids: ").Append(string.Join(",", doneIds)).Append("\n");
sb.Append("skipped_ids: ").Append(skipIds.Count > 0 ? string.Join(",", skipIds) : "(none)");
return CB6Cap(sb.ToString(), 3200);
}

// ===================== helpers (CB6 prefix from T3 v4, T5 prefix new) =====================
public class CB6Box
{
    public double x0, y0, x1, y1; public long id;
    public CB6Box(double a, double b, double c, double d, long i) { x0 = Math.Min(a, c); x1 = Math.Max(a, c); y0 = Math.Min(b, d); y1 = Math.Max(b, d); id = i; }
}
public class CB6Clu
{
    public List<long> labels = new List<long>(); public List<long> members = new List<long>();
    public List<CB6Box> rects = new List<CB6Box>(); public List<long> added = new List<long>();
    public List<double[]> loop = null; public string mode = "rect"; public bool hulled = false;
}
public static string CB6NoGo(string r) { return "NO_GO " + r + "\ndone=0 skipped=0 failed=0"; }
public static string CB6Cap(string s, int n) { return s.Length <= n ? s : s.Substring(0, n) + "..."; }
public static CB6Box CB6Ex(CB6Box b, double d) { return new CB6Box(b.x0 - d, b.y0 - d, b.x1 + d, b.y1 + d, b.id); }
public static bool CB6Ov(CB6Box a, CB6Box b) { double e = 1e-6; return a.x0 < b.x1 - e && b.x0 < a.x1 - e && a.y0 < b.y1 - e && b.y0 < a.y1 - e; }
public static bool CB6OvU(CB6Box b, List<CB6Box> rs) { foreach (CB6Box r in rs) if (CB6Ov(b, r)) return true; return false; }
public static bool CB6InU(CB6Box b, List<CB6Box> rs)
{
    List<double> xs = new List<double> { b.x0, b.x1 }, ys = new List<double> { b.y0, b.y1 };
    foreach (CB6Box r in rs)
    {
        if (r.x0 > b.x0 && r.x0 < b.x1) xs.Add(r.x0); if (r.x1 > b.x0 && r.x1 < b.x1) xs.Add(r.x1);
        if (r.y0 > b.y0 && r.y0 < b.y1) ys.Add(r.y0); if (r.y1 > b.y0 && r.y1 < b.y1) ys.Add(r.y1);
    }
    xs.Sort(); ys.Sort();
    for (int i = 0; i + 1 < xs.Count; i++)
    {
        if (xs[i + 1] - xs[i] < 1e-9) continue; double cx = 0.5 * (xs[i] + xs[i + 1]);
        for (int j = 0; j + 1 < ys.Count; j++)
        {
            if (ys[j + 1] - ys[j] < 1e-9) continue; double cy = 0.5 * (ys[j] + ys[j + 1]);
            bool cov = false;
            foreach (CB6Box r in rs) if (cx > r.x0 && cx < r.x1 && cy > r.y0 && cy < r.y1) { cov = true; break; }
            if (!cov) return false;
        }
    }
    return true;
}
public static CB6Box CB6Hull(List<CB6Box> rs, long id)
{
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
    foreach (CB6Box r in rs) { a = Math.Min(a, r.x0); b = Math.Min(b, r.y0); c = Math.Max(c, r.x1); d = Math.Max(d, r.y1); }
    return new CB6Box(a, b, c, d, id);
}
// tag head: right end of the highest top edge - dx, that edge + dy; [2] = top y
public static double[] CB6TagHead(List<CB6Box> rs, double dx, double dy)
{
    double top = double.MinValue; foreach (CB6Box r in rs) top = Math.Max(top, r.y1);
    CB6Box best = null;
    foreach (CB6Box r in rs) if (Math.Abs(r.y1 - top) < 0.011 && (best == null || r.x1 > best.x1)) best = r;
    double hx = Math.Max(best.x1 - dx, 0.5 * (best.x0 + best.x1));
    return new double[] { hx, top + dy, top };
}
// tag box from the head and the measured offsets tg = {dx, dy, left, right, below, above}
public static CB6Box T5TagBox(double[] h, double[] tg) { return new CB6Box(h[0] - tg[2], h[1] - tg[4], h[0] + tg[3], h[1] + tg[5], -1); }
public static List<double> CB6Uniq(List<double> v, double tol)
{
    v.Sort(); List<double> o = new List<double>();
    foreach (double d in v) if (o.Count == 0 || d - o[o.Count - 1] > tol) o.Add(d);
    return o;
}
public static int CB6Idx(List<double> v, double d)
{
    int bi = 0; double bd = double.MaxValue;
    for (int i = 0; i < v.Count; i++) { double dd = Math.Abs(v[i] - d); if (dd < bd) { bd = dd; bi = i; } }
    return bi;
}
public static bool CB6Add(Dictionary<long, long> m, long a, long b) { if (m.ContainsKey(a)) return false; m[a] = b; return true; }
// Outline of the union of rects as ONE clockwise loop starting bottom-left with the first edge going up; null if the
// union is not a single simple loop.
public static List<double[]> CB6Trace(List<CB6Box> rs)
{
    List<double> xv = new List<double>(), yv = new List<double>();
    foreach (CB6Box r in rs) { xv.Add(r.x0); xv.Add(r.x1); yv.Add(r.y0); yv.Add(r.y1); }
    List<double> xs = CB6Uniq(xv, 0.01), ys = CB6Uniq(yv, 0.01);
    int nx = xs.Count - 1, ny = ys.Count - 1; if (nx < 1 || ny < 1) return null;
    bool[,] cov = new bool[nx, ny];
    foreach (CB6Box r in rs)
    {
        int a = CB6Idx(xs, r.x0), b = CB6Idx(xs, r.x1), c = CB6Idx(ys, r.y0), d = CB6Idx(ys, r.y1);
        for (int i = a; i < b; i++) for (int j = c; j < d; j++) cov[i, j] = true;
    }
    Dictionary<long, long> nxt = new Dictionary<long, long>(); int ne = 0; bool bad = false; long K = 100000;
    for (int i = 0; i < nx; i++)
        for (int j = 0; j < ny; j++)
        {
            if (!cov[i, j]) continue;
            if (i == 0 || !cov[i - 1, j]) { if (!CB6Add(nxt, i * K + j, i * K + j + 1)) bad = true; ne++; }
            if (j == ny - 1 || !cov[i, j + 1]) { if (!CB6Add(nxt, i * K + j + 1, (i + 1) * K + j + 1)) bad = true; ne++; }
            if (i == nx - 1 || !cov[i + 1, j]) { if (!CB6Add(nxt, (i + 1) * K + j + 1, (i + 1) * K + j)) bad = true; ne++; }
            if (j == 0 || !cov[i, j - 1]) { if (!CB6Add(nxt, (i + 1) * K + j, i * K + j)) bad = true; ne++; }
        }
    if (bad || ne == 0) return null;
    long start = long.MaxValue; foreach (long k in nxt.Keys) if (k < start) start = k;
    List<long> path = new List<long>(); long curv = start; int guard = 0;
    do
    {
        path.Add(curv); long n;
        if (!nxt.TryGetValue(curv, out n)) return null;
        curv = n; guard++; if (guard > ne) return null;
    } while (curv != start);
    if (path.Count != ne) return null;
    List<double[]> pts = new List<double[]>(); int m = path.Count;
    for (int k = 0; k < m; k++)
    {
        long p = path[(k - 1 + m) % m], c = path[k], q = path[(k + 1) % m];
        long pi = p / K, pj = p % K, ci = c / K, cj = c % K, qi = q / K, qj = q % K;
        bool col = (pi == ci && ci == qi) || (pj == cj && cj == qj);
        if (!col) pts.Add(new double[] { xs[(int)ci], ys[(int)cj] });
    }
    return pts.Count >= 4 ? pts : null;
}
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
public static bool CB6HasVal(Element e, string num)
{
    string n0 = num.TrimStart('0');
    foreach (Parameter p in e.Parameters)
    {
        if (p.StorageType != StorageType.String) continue;
        string s = (p.AsString() ?? "").Trim().TrimStart('#').Trim();
        if (s.Length == 0) continue;
        if (s == num || (s.TrimStart('0') == n0 && n0.Length > 0)) return true;
    }
    return false;
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
// one cluster per riser branch (grp): hull of the branch's labels + their HEX tags, plus margin
public static List<CB6Clu> CB6Build(long[] lbl, string[] state, bool[] covered, CB6Box[] lb, List<long>[] hexIds, List<CB6Box>[] hexBoxes, double margin, int[] grp)
{
    List<CB6Clu> cl = new List<CB6Clu>();
    List<int> gs = new List<int>(); foreach (int g in grp) if (!gs.Contains(g)) gs.Add(g);
    foreach (int g in gs)
    {
        CB6Clu c = new CB6Clu(); List<CB6Box> mb = new List<CB6Box>();
        for (int i = 0; i < lbl.Length; i++)
        {
            if (grp[i] != g || state[i] != "todo" || covered[i] || lb[i] == null) continue;
            c.labels.Add(lbl[i]); c.members.Add(lbl[i]); c.members.AddRange(hexIds[i]);
            mb.Add(lb[i]); mb.AddRange(hexBoxes[i]);
        }
        if (mb.Count == 0) continue;
        c.rects.Add(CB6Ex(CB6Hull(mb, c.labels[0]), margin));
        cl.Add(c);
    }
    return cl;
}
public static bool CB6Touch(CB6Clu a, CB6Clu b, double clr, double[] tg)
{
    foreach (CB6Box ra in a.rects) if (CB6OvU(CB6Ex(ra, 2 * clr), b.rects)) return true;
    CB6Box ta = T5TagBox(CB6TagHead(a.rects, tg[0], tg[1]), tg), tb = T5TagBox(CB6TagHead(b.rects, tg[0], tg[1]), tg);
    return CB6OvU(CB6Ex(ta, clr), b.rects) || CB6OvU(CB6Ex(tb, clr), a.rects) || CB6Ov(CB6Ex(ta, clr), tb);
}
// Merge touching clusters, grow each to enclose any obstacle its outline (+arc clearance) or its tag box would cut,
// trace a single clockwise outline (else the hull rectangle). Returns null when settled, else the NO_GO reason.
public static string CB6Plan(List<CB6Clu> cl, List<CB6Box> obs, List<CB6Box> hard, double margin, double clr, double inClr, double[] tg, double maxDim, int cap)
{
    for (int it = 0; it < 60; it++)
    {
        bool ch = false; bool mg = true;
        while (mg)
        {
            mg = false;
            for (int a = 0; a < cl.Count && !mg; a++)
                for (int b = a + 1; b < cl.Count && !mg; b++)
                    if (CB6Touch(cl[a], cl[b], clr, tg))
                    {
                        cl[a].labels.AddRange(cl[b].labels); cl[a].members.AddRange(cl[b].members);
                        cl[a].rects.AddRange(cl[b].rects); cl[a].added.AddRange(cl[b].added); cl[a].hulled = cl[a].hulled || cl[b].hulled;
                        cl.RemoveAt(b); mg = true; ch = true;
                    }
        }
        foreach (CB6Clu c in cl)
        {
            CB6Box tb = T5TagBox(CB6TagHead(c.rects, tg[0], tg[1]), tg);
            foreach (CB6Box x in hard)
                if (CB6OvU(CB6Ex(x, 2 * clr), c.rects) || CB6Ov(CB6Ex(x, clr), tb)) return "cloud for labels " + string.Join("/", c.labels) + " would touch existing cloud " + x.id;
            foreach (CB6Box o in obs)
            {
                if (CB6InU(CB6Ex(o, inClr), c.rects)) continue;
                if (CB6OvU(CB6Ex(o, clr), c.rects) || CB6Ov(o, tb))
                {
                    c.rects.Add(CB6Ex(o, margin)); c.added.Add(o.id); ch = true;
                    if (c.added.Count > cap) return "cloud for labels " + string.Join("/", c.labels) + " would enclose more than " + cap + " other items";
                    break;
                }
            }
            CB6Box hu = CB6Hull(c.rects, 0);
            if (hu.x1 - hu.x0 > maxDim || hu.y1 - hu.y0 > maxDim) return "cloud for labels " + string.Join("/", c.labels) + " exceeds 15 in paper";
            List<double[]> lp = CB6Trace(c.rects);
            if (lp == null) { c.rects.Clear(); c.rects.Add(hu); c.hulled = true; ch = true; }
            else { c.loop = lp; c.mode = (c.hulled ? "hull-" : "") + (lp.Count == 4 ? "rect" : "outline"); }
        }
        if (!ch) return null;
    }
    return "cloud plan did not settle in 60 iterations";
}
// segments {x0,y0,x1,y1} of a closed loop / of a cloud's sketch curves
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
// even-odd point in polygon over an unordered segment list
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
// distance from a box to the nearest segment (segment's own bbox; exact for rectilinear segments)
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
// clearance of a box inside a cloud polygon: -1 if any corner is outside, else distance to the boundary
public static double T5Clear(CB6Box b, List<double[]> segs)
{
    if (!T5In(b.x0, b.y0, segs) || !T5In(b.x0, b.y1, segs) || !T5In(b.x1, b.y0, segs) || !T5In(b.x1, b.y1, segs)) return -1;
    return T5BoxSegs(b, segs);
}
// dry-run line for one planned cloud: hull rectangle, loop, per-label clearance, tag box and its gap
public static string T5Report(CB6Clu c, CB6Box[] lb, long[] lbl, List<long>[] hexIds, List<CB6Box>[] hexBoxes, double[] tg, IFormatProvider inv)
{
    System.Text.StringBuilder s = new System.Text.StringBuilder();
    List<double[]> sg = T5Segs(c.loop);
    CB6Box hu = CB6Hull(c.rects, 0);
    double[] h = CB6TagHead(c.rects, tg[0], tg[1]); CB6Box tb = T5TagBox(h, tg);
    s.Append("plan labels=").Append(string.Join("/", c.labels)).Append(" ").Append(c.mode).Append(" n=").Append(c.loop.Count)
     .Append(" area=").Append(Math.Round(CB6Area(c.loop), 2).ToString(inv))
     .Append(" hull=(").Append(Math.Round(hu.x0, 2).ToString(inv)).Append(",").Append(Math.Round(hu.y0, 2).ToString(inv)).Append(",")
     .Append(Math.Round(hu.x1, 2).ToString(inv)).Append(",").Append(Math.Round(hu.y1, 2).ToString(inv)).Append(") clr=");
    double mn = double.MaxValue; List<string> parts = new List<string>();
    for (int i = 0; i < lbl.Length; i++)
    {
        if (!c.labels.Contains(lbl[i])) continue;
        double li = T5Clear(lb[i], sg);
        foreach (CB6Box hb in hexBoxes[i]) li = Math.Min(li, T5Clear(hb, sg));
        mn = Math.Min(mn, li); parts.Add(lbl[i] + ":" + Math.Round(li, 2).ToString(inv));
    }
    s.Append(string.Join(",", parts)).Append(" min_clr=").Append(Math.Round(mn, 3).ToString(inv));
    s.Append(" head=(").Append(Math.Round(h[0], 2).ToString(inv)).Append(",").Append(Math.Round(h[1], 2).ToString(inv)).Append(")")
     .Append(" tag_box=(").Append(Math.Round(tb.x0, 2).ToString(inv)).Append(",").Append(Math.Round(tb.y0, 2).ToString(inv)).Append(",")
     .Append(Math.Round(tb.x1, 2).ToString(inv)).Append(",").Append(Math.Round(tb.y1, 2).ToString(inv)).Append(")")
     .Append(" tag_gap=").Append(Math.Round(T5BoxSegs(tb, sg), 3).ToString(inv));
    if (c.added.Count > 0) s.Append(" encloses_other=").Append(string.Join("/", c.added.Take(12))).Append(c.added.Count > 12 ? "(+" + (c.added.Count - 12) + ")" : "");
    s.Append(" loop=");
    System.Text.StringBuilder lp = new System.Text.StringBuilder();
    foreach (double[] p in c.loop) lp.Append("(").Append(Math.Round(p[0], 2).ToString(inv)).Append(",").Append(Math.Round(p[1], 2).ToString(inv)).Append(")");
    s.Append(CB6Cap(lp.ToString(), 420));
    return s.ToString();
}
public static object DummyTail() { return null;
