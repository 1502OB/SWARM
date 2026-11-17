// edit-generic-annotation-text: append suffix to USERFILLED_TAG Note Text, read back after Regenerate (R30)
// CB6-R1-T3 | operation: edit-generic-annotation-text + revision-cloud | run 20261109-1412 | Revit 2025 | v4 (fix mode)
// Body of: public static object Execute(Document document, object[] parameters)
// send_code_to_revit, transactionMode "auto" (one outer transaction; this script uses a SubTransaction so it can
// roll back its own work and still return "ROLLED_BACK ..."). Never saves or syncs.
// parameters: ["dryrun"]  = read-only preflight + planned cloud outlines, returns before any write.
//             ["settest"] = sets the 5 Note Texts inside a SubTransaction, reports Set()/read-back per label,
//                           then ALWAYS rolls back (nothing persists). Diagnostic only.
//             []          = real run.
// v4: NO inch-mark literal anywhere in the code. A label is "todo" when its current Note Text starts with "4", ends
// with "FS-1", is <= 8 chars and has no "(F-"; the new value is built as current + " (F-xx)" and the read-back is
// compared to that same constructed string. Char codes of every current/new value are reported. Set() returning
// false, Set() throwing and a read-back mismatch are reported distinctly, for every label, before rolling back.
// Clouds: unchanged from v3 (one per riser branch, clockwise, 1/4 in margin, grow to enclose cut text, merge
// touching, NO_GO on touching an existing cloud or > 15 in paper, one 'Revision Tag' at right-0.17 in / top+0.08 in).
var inv = System.Globalization.CultureInfo.InvariantCulture;
string mode0 = (parameters != null && parameters.Length > 0 && parameters[0] != null) ? parameters[0].ToString().Trim().ToLowerInvariant() : "";
bool dry = mode0 == "dryrun";
bool setTest = mode0 == "settest";
string QT = ((char)34).ToString();
string CANON_OLD = "4" + QT + " FS-1"; // used only to flag a non-canonical current value, never to select labels
string REVDESC = "FEWD Submittal";
long VIEWID = 14662318, SHEETID = 14662586, VPID = 14662592;
long[] LBL = new long[] { 14680811, 14680841, 14680866, 14698172, 14698003 };
string[] FNUM = new string[] { "45", "48", "50", "53", "55" };
string[] SUFFIX = new string[LBL.Length];
for (int i = 0; i < LBL.Length; i++) SUFFIX[i] = " (F-" + FNUM[i] + ")";
// K1.1 item numbers of the HEX KEYNOTE TAGs beside each label, with the riser ids from CB6_fewd_mapping.json (same model,
// WP-1 survey). 0 = id unknown: found in-script by value, nearest to the label.
string[][] HEXNUM = new string[][] { new string[] { "97", "98", "100" }, new string[] { "83", "84", "87", "89", "92" }, new string[] { "115" }, new string[] { "230" }, new string[] { "224", "225", "226", "227" } };
long[][] HEXID = new long[][] { new long[] { 14680817, 14680812, 14680810 }, new long[] { 14680844, 14680842, 14680843, 14680845, 14680846 }, new long[] { 14680867 }, new long[] { 14698171 }, new long[] { 14698002, 0, 14698004, 14698010 } };
int[] GRP = new int[] { 0, 0, 0, 1, 1 }; // riser branch: PREP KITCHEN labels = 0, BAR/LOUNGE labels = 1; one cloud per branch
int CAP = 40; // sanity cap on whole items a branch cloud may enclose (size itself is limited by the 15 in paper cap)
List<string> warn = new List<string>();
System.Text.StringBuilder sb = new System.Text.StringBuilder();
sb.Append(dry ? "DRYRUN CB6-R1-T3 v4" : (setTest ? "SETTEST CB6-R1-T3 v4" : "RUN CB6-R1-T3 v4"));

// ---- 0. document
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return CB6NoGo("wrong document open: " + title);
sb.Append(" modifiable=").Append(document.IsModifiable ? "yes" : "no");

// ---- 1. view, viewport, sheet
View view = document.GetElement(new ElementId(VIEWID)) as View;
if (view == null || !(view is ViewDrafting)) return CB6NoGo("view " + VIEWID + " missing or not a drafting view");
int scale = view.Scale;
if (scale != 48) warn.Add("view scale 1:" + scale + " (task says 1:48; offsets use the live scale)");
double PI_ = scale / 12.0; // model ft per paper inch
double margin = 0.25 * PI_, inClr = 0.03 * PI_, dxTag = 0.17 * PI_, dyTag = 0.08 * PI_, tagHW = 0.13 * PI_, tagH = 0.20 * PI_;
double clrIn = 0.10;
try
{
    RevisionSettings rset = RevisionSettings.GetRevisionSettings(document);
    double spIn = rset.RevisionCloudSpacing * 12.0;
    clrIn = Math.Max(0.10, Math.Min(0.25, 0.4 * spIn));
    sb.Append(" cloud_spacing_in=").Append(Math.Round(spIn, 3).ToString(inv));
}
catch (Exception) { warn.Add("RevisionCloudSpacing unreadable; arc clearance 0.10 in"); }
double clr = clrIn * PI_;
double maxDim = 15.0 * PI_; // a cloud larger than 15 in paper in either direction is a NO_GO
Viewport vp = document.GetElement(new ElementId(VPID)) as Viewport;
if (vp == null || vp.ViewId.Value != VIEWID || vp.SheetId.Value != SHEETID) return CB6NoGo("viewport " + VPID + " does not place view " + VIEWID + " on sheet " + SHEETID);
ViewSheet sheet = document.GetElement(vp.SheetId) as ViewSheet;
string sheetNo = sheet != null ? sheet.SheetNumber : "?";
if (sheetNo != "P6.01") warn.Add("sheet number is " + sheetNo + " (task says P6.01)");
if (vp.Rotation != ViewportRotation.None) warn.Add("viewport rotated " + vp.Rotation.ToString() + ": delta offsets assume an unrotated viewport");
sb.Append(" view=").Append(VIEWID).Append(" scale=1:").Append(scale).Append(" sheet=").Append(sheetNo);

// ---- 2. revision by Description
Revision rev = null; int nrev = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REVDESC) { nrev++; if (rev == null) rev = r; }
}
if (nrev == 0) return CB6NoGo("revision Description 'FEWD Submittal' not found (CB6-R1-T1 must run first)");
if (nrev > 1) return CB6NoGo(nrev + " revisions have Description 'FEWD Submittal'");
if (rev.Issued) return CB6NoGo("revision 'FEWD Submittal' (id " + rev.Id.Value + ") is issued; clouds cannot go on it");
if (rev.Visibility == RevisionVisibility.Hidden) warn.Add("revision visibility is Hidden: clouds/tags will not show");
sb.Append(" rev=").Append(rev.Id.Value).Append(" seq=").Append(rev.SequenceNumber);

// ---- 3. Revision Tag type
FamilySymbol tagSym = null; List<string> tagNames = new List<string>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol; if (fs == null) continue;
    tagNames.Add(fs.FamilyName + ":" + fs.Name);
    if ((fs.FamilyName == "Revision Tag" || fs.Name == "Revision Tag") && (tagSym == null || fs.Id.Value < tagSym.Id.Value)) tagSym = fs;
}
if (tagSym == null) return CB6NoGo("no family 'Revision Tag' in category Revision Cloud Tags; found: " + string.Join(", ", tagNames.Take(5)));
sb.Append(" tag_type=").Append(tagSym.Id.Value).Append(" '").Append(tagSym.FamilyName).Append(":").Append(tagSym.Name).Append("'");

// ---- 4. worksharing ownership
if (document.IsWorkshared)
{
    List<long> chk = new List<long>(LBL); chk.Add(VIEWID);
    List<string> owned = new List<string>();
    foreach (long id in chk)
        if (WorksharingUtils.GetCheckoutStatus(document, new ElementId(id)) == CheckoutStatus.OwnedByOtherUser) owned.Add(id.ToString());
    if (owned.Count > 0) return CB6NoGo("owned by another user: " + string.Join(",", owned));
}

// ---- 5. labels (matched by structure, new value constructed from the current value)
string[] state = new string[LBL.Length]; string[] cur = new string[LBL.Length]; string[] newv = new string[LBL.Length];
int[] nNoteText = new int[LBL.Length];
CB6Box[] oldBox = new CB6Box[LBL.Length];
double zPl = 0; bool zSet = false;
for (int i = 0; i < LBL.Length; i++)
{
    cur[i] = ""; newv[i] = "";
    FamilyInstance fi = document.GetElement(new ElementId(LBL[i])) as FamilyInstance;
    if (fi == null) { state[i] = "invalid:not found or not a family instance"; continue; }
    if (fi.OwnerViewId.Value != VIEWID) { state[i] = "invalid:owner view " + fi.OwnerViewId.Value; continue; }
    if (fi.Category == null || fi.Category.BuiltInCategory != BuiltInCategory.OST_GenericAnnotation) { state[i] = "invalid:not a generic annotation"; continue; }
    string fam = fi.Symbol != null ? fi.Symbol.FamilyName + ":" + fi.Symbol.Name : "";
    if (fam.ToUpperInvariant().IndexOf("USERFILLED_TAG") < 0) { state[i] = "invalid:family " + fam; continue; }
    IList<Parameter> allNt = fi.GetParameters("Note Text");
    nNoteText[i] = allNt != null ? allNt.Count : 0;
    if (nNoteText[i] > 1) warn.Add("label " + LBL[i] + " has " + nNoteText[i] + " parameters named 'Note Text'");
    Parameter p = fi.LookupParameter("Note Text");
    if (p == null || p.StorageType != StorageType.String) { state[i] = "invalid:no string 'Note Text'"; continue; }
    if (p.IsReadOnly) { state[i] = "invalid:'Note Text' read-only"; continue; }
    cur[i] = p.AsString() ?? "";
    BoundingBoxXYZ bb = fi.get_BoundingBox(view);
    if (bb == null) { state[i] = "invalid:no bounding box in view"; continue; }
    oldBox[i] = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, LBL[i]);
    if (!zSet) { zPl = bb.Min.Z; zSet = true; }
    string c0 = cur[i];
    if (c0.EndsWith(SUFFIX[i], StringComparison.Ordinal) && CB6IsOld(c0.Substring(0, c0.Length - SUFFIX[i].Length))) { state[i] = "already"; newv[i] = c0; }
    else if (CB6IsOld(c0))
    {
        state[i] = "todo"; newv[i] = c0 + SUFFIX[i];
        if (c0 != CANON_OLD) warn.Add("label " + LBL[i] + " current text is not ASCII 4,34,32,F,S,-,1: " + CB6Show(c0));
    }
    else state[i] = "mismatch";
}

// ---- 6. HEX tags per label
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
    if (state[i] != "todo" && state[i] != "already") continue;
    double lcx = 0.5 * (oldBox[i].x0 + oldBox[i].x1), lcy = 0.5 * (oldBox[i].y0 + oldBox[i].y1);
    for (int k = 0; k < HEXNUM[i].Length; k++)
    {
        string num = HEXNUM[i][k]; long hid = HEXID[i][k];
        FamilyInstance h = hid > 0 ? document.GetElement(new ElementId(hid)) as FamilyInstance : null;
        if (h != null && h.OwnerViewId.Value != VIEWID) { warn.Add("hex " + hid + " not in view"); h = null; }
        if (h != null) { if (!CB6HasVal(h, num)) warn.Add("hex " + hid + " value not confirmed as #" + num); }
        else
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
        usedHex.Add(h.Id.Value);
        hexIds[i].Add(h.Id.Value);
        hexBoxes[i].Add(new CB6Box(hb.Min.X, hb.Min.Y, hb.Max.X, hb.Max.Y, h.Id.Value));
    }
}

// ---- 7. existing clouds in the view
List<CB6Box> hard = new List<CB6Box>(); List<CB6Box> fewdBoxes = new List<CB6Box>(); int nCloudsBefore = 0;
foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(RevisionCloud)))
{
    RevisionCloud rc = e as RevisionCloud; if (rc == null) continue;
    CB6Box b = CB6CloudBox(rc); if (b == null) continue;
    nCloudsBefore++; hard.Add(b);
    if (rc.RevisionId.Value == rev.Id.Value) fewdBoxes.Add(b);
}
bool[] covered = new bool[LBL.Length];
for (int i = 0; i < LBL.Length; i++)
{
    if (state[i] != "todo" && state[i] != "already") continue;
    double cx = 0.5 * (oldBox[i].x0 + oldBox[i].x1), cy = 0.5 * (oldBox[i].y0 + oldBox[i].y1);
    foreach (CB6Box b in fewdBoxes) if (cx > b.x0 && cx < b.x1 && cy > b.y0 && cy < b.y1) covered[i] = true;
}
sb.Append(" clouds_in_view_before=").Append(nCloudsBefore).Append(" fewd_before=").Append(fewdBoxes.Count);

// ---- 8. obstacles: text notes, tags, generic annotations that are not members of a cloud to be drawn
HashSet<long> memberSet = new HashSet<long>();
for (int i = 0; i < LBL.Length; i++)
    if ((state[i] == "todo" || state[i] == "already") && !covered[i]) { memberSet.Add(LBL[i]); foreach (long h in hexIds[i]) memberSet.Add(h); }
List<CB6Box> obs = new List<CB6Box>(); int nBig = 0;
foreach (Element e in new FilteredElementCollector(document, view.Id).WhereElementIsNotElementType())
{
    if (memberSet.Contains(e.Id.Value) || e is RevisionCloud || e.Category == null) continue;
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

int nTodo = 0, nAlready = 0, nBad = 0;
for (int i = 0; i < LBL.Length; i++) { if (state[i] == "todo") nTodo++; else if (state[i] == "already") nAlready++; else nBad++; }

// ---- 9. DRY RUN: plan with a conservative label box (old box widened both ways by the extra text length)
if (dry)
{
    CB6Box[] lb = new CB6Box[LBL.Length];
    for (int i = 0; i < LBL.Length; i++) if (oldBox[i] != null) lb[i] = state[i] == "todo" ? CB6Grow(oldBox[i], CB6GrowF(cur[i], SUFFIX[i])) : oldBox[i];
    List<CB6Clu> cl = CB6Build(LBL, state, covered, lb, hexIds, hexBoxes, margin, GRP);
    string perr = CB6Plan(cl, obs, hard, margin, clr, inClr, dxTag, dyTag, tagHW, tagH, maxDim, CAP);
    sb.Append("\n");
    if (perr != null) sb.Append("PLAN NO_GO: ").Append(perr).Append("\n");
    for (int i = 0; i < LBL.Length; i++)
        sb.Append("label ").Append(LBL[i]).Append(" ").Append(state[i]).Append(" cur=").Append(CB6Show(cur[i]))
          .Append(" -> ").Append(newv[i].Length > 0 ? CB6Show(newv[i]) : "-").Append(" note_text_params=").Append(nNoteText[i])
          .Append(" hex=").Append(string.Join("/", hexIds[i])).Append(covered[i] ? " covered" : "").Append("\n");
    if (perr == null) foreach (CB6Clu c in cl) sb.Append(CB6Desc(c, dxTag, dyTag, inv)).Append("\n");
    if (warn.Count > 0) sb.Append("warn: ").Append(CB6Cap(string.Join("; ", warn), 700)).Append("\n");
    int sk = nAlready + nBad;
    for (int i = 0; i < LBL.Length; i++) if (covered[i]) sk++;
    sb.Append("done=0 skipped=").Append(sk).Append(" failed=").Append(perr != null ? 1 : 0)
      .Append(" (dryrun: would_edit=").Append(nTodo).Append(" would_clouds=").Append(perr == null ? cl.Count : 0).Append(")");
    return CB6Cap(sb.ToString(), 3200);
}

// ---- 10. RUN / SETTEST (all or nothing inside a SubTransaction)
string item = "start";
List<string> doneIds = new List<string>(), skipIds = new List<string>(), cleanup = new List<string>();
List<string> cloudLines = new List<string>(), setDiag = new List<string>();
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    // 10a. text edits: attempt every label, record the outcome of each, fail after the loop
    int nSetFail = 0;
    for (int i = 0; i < LBL.Length; i++)
    {
        item = "text " + LBL[i];
        if (state[i] == "already") { skipIds.Add("text:" + LBL[i] + "(already)"); continue; }
        if (state[i] != "todo") { skipIds.Add("text:" + LBL[i] + "(" + (state[i] == "mismatch" ? "mismatch " + CB6Show(cur[i]) : state[i]) + ")"); continue; }
        Parameter p = document.GetElement(new ElementId(LBL[i])).LookupParameter("Note Text");
        string res;
        bool ret = false; string thr = null;
        try { ret = p.Set(newv[i]); }
        catch (Exception sx) { thr = sx.GetType().Name + ": " + CB6Cap(sx.Message, 120); }
        string rb = p.AsString() ?? "";
        if (thr != null) res = "SET_THREW " + thr + " readback=" + CB6Show(rb);
        else if (!ret) res = "SET_RETURNED_FALSE readback=" + CB6Show(rb);
        else if (rb == newv[i]) res = "OK";
        else
        {
            document.Regenerate();
            string rb2 = document.GetElement(new ElementId(LBL[i])).LookupParameter("Note Text").AsString() ?? "";
            if (rb2 == newv[i]) res = "OK_AFTER_REGEN";
            else res = "READBACK_MISMATCH (Set returned true) want=" + CB6Show(newv[i]) + " got=" + CB6Show(rb2);
        }
        setDiag.Add(LBL[i] + " " + res);
        if (res == "OK" || res == "OK_AFTER_REGEN") { doneIds.Add("text:" + LBL[i]); cleanup.Add("text:" + LBL[i]); }
        else nSetFail++;
    }
    if (setTest)
    {
        if (st.HasStarted() && !st.HasEnded()) st.RollBack();
        sb.Append("\nset: ").Append(string.Join(" | ", setDiag)).Append("\n");
        if (warn.Count > 0) sb.Append("warn: ").Append(CB6Cap(string.Join("; ", warn), 500)).Append("\n");
        sb.Append("SETTEST rolled back, nothing persisted\n");
        sb.Append("done=0 skipped=").Append(skipIds.Count).Append(" failed=").Append(nSetFail)
          .Append(" (settest: would_set_ok=").Append(doneIds.Count).Append(")");
        return CB6Cap(sb.ToString(), 3200);
    }
    if (nSetFail > 0) throw new Exception(nSetFail + " Note Text edit(s) failed | " + string.Join(" | ", setDiag));
    item = "regenerate";
    document.Regenerate();
    CB6Box[] lb = new CB6Box[LBL.Length];
    for (int i = 0; i < LBL.Length; i++)
    {
        if (oldBox[i] == null) continue;
        if (state[i] != "todo") { lb[i] = oldBox[i]; continue; }
        BoundingBoxXYZ bb = document.GetElement(new ElementId(LBL[i])).get_BoundingBox(view);
        double ow = oldBox[i].x1 - oldBox[i].x0;
        if (bb != null && (bb.Max.X - bb.Min.X) >= 1.3 * ow) lb[i] = new CB6Box(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, LBL[i]);
        else { lb[i] = CB6Grow(oldBox[i], CB6GrowF(cur[i], SUFFIX[i])); warn.Add("label " + LBL[i] + " bbox did not grow; used widened old box"); }
    }
    for (int i = 0; i < LBL.Length; i++) if (covered[i]) skipIds.Add("cloud:" + LBL[i] + "(inside existing FEWD cloud)");
    item = "plan";
    List<CB6Clu> cl = CB6Build(LBL, state, covered, lb, hexIds, hexBoxes, margin, GRP);
    string perr = CB6Plan(cl, obs, hard, margin, clr, inClr, dxTag, dyTag, tagHW, tagH, maxDim, CAP);
    if (perr != null) throw new Exception(perr);
    List<IndependentTag> tags = new List<IndependentTag>(); List<CB6Clu> made = new List<CB6Clu>();
    foreach (CB6Clu c in cl)
    {
        item = "cloud " + string.Join("/", c.labels);
        double area = CB6Area(c.loop);
        if (area >= 0) throw new Exception("loop not clockwise (area " + area + ")");
        List<Curve> curves = new List<Curve>();
        for (int k = 0; k < c.loop.Count; k++)
        {
            double[] a = c.loop[k], b = c.loop[(k + 1) % c.loop.Count];
            curves.Add(Line.CreateBound(new XYZ(a[0], a[1], zPl), new XYZ(b[0], b[1], zPl)));
        }
        RevisionCloud rc = RevisionCloud.Create(document, view, rev.Id, curves);
        item = "tag " + string.Join("/", c.labels);
        double[] hd = CB6TagHead(c.rects, dxTag, dyTag);
        IndependentTag tg = IndependentTag.Create(document, tagSym.Id, view.Id, new Reference(rc), false, TagOrientation.Horizontal, new XYZ(hd[0], hd[1], zPl));
        tags.Add(tg); made.Add(c);
        double sk = CB6SketchArea(rc);
        cloudLines.Add("cloud " + rc.Id.Value + " tag " + tg.Id.Value + " labels=" + string.Join("/", c.labels) + " " + c.mode + " n=" + c.loop.Count
            + " area=" + Math.Round(area, 2).ToString(inv) + " sketch_area=" + Math.Round(sk, 2).ToString(inv)
            + " v0=(" + Math.Round(c.loop[0][0], 2).ToString(inv) + "," + Math.Round(c.loop[0][1], 2).ToString(inv) + ")"
            + " head=(" + Math.Round(hd[0], 2).ToString(inv) + "," + Math.Round(hd[1], 2).ToString(inv) + ")"
            + (c.added.Count > 0 ? " enclosed=" + string.Join("/", c.added) : ""));
        doneIds.Add("cloud:" + rc.Id.Value); cleanup.Add("tag:" + tg.Id.Value); cleanup.Add("cloud:" + rc.Id.Value);
    }
    item = "post-check";
    document.Regenerate();
    for (int t = 0; t < tags.Count; t++)
    {
        BoundingBoxXYZ tb = tags[t].get_BoundingBox(view);
        if (tb == null) { warn.Add("tag " + tags[t].Id.Value + " no bbox"); continue; }
        CB6Box tbx = new CB6Box(tb.Min.X, tb.Min.Y, tb.Max.X, tb.Max.Y, tags[t].Id.Value);
        foreach (CB6Box o in obs) if (CB6Ov(tbx, o)) { bool inside = false; foreach (CB6Clu c in made) if (CB6InU(o, c.rects)) inside = true; if (!inside) warn.Add("tag " + tags[t].Id.Value + " box overlaps " + o.id); }
        foreach (CB6Box hb in hard) if (CB6Ov(tbx, hb)) warn.Add("tag " + tags[t].Id.Value + " overlaps existing cloud " + hb.id);
    }
    for (int i = 0; i < LBL.Length; i++)
    {
        if (state[i] != "todo") continue;
        string rr = document.GetElement(new ElementId(LBL[i])).LookupParameter("Note Text").AsString() ?? "";
        if (rr != newv[i]) throw new Exception("re-read of " + LBL[i] + " after clouds: want=" + CB6Show(newv[i]) + " got=" + CB6Show(rr));
    }
    st.Commit();
}
catch (Exception ex)
{
    if (st.HasStarted() && !st.HasEnded()) st.RollBack();
    return "ROLLED_BACK " + item + ": " + CB6Cap(ex.Message, 1400) + "\ndone=0 skipped=" + skipIds.Count + " failed=1";
}
sb.Append("\n");
for (int i = 0; i < LBL.Length; i++)
    sb.Append("label ").Append(LBL[i]).Append(" ").Append(state[i]).Append(" old='").Append(cur[i]).Append("' new='")
      .Append(state[i] == "todo" || state[i] == "already" ? newv[i] : cur[i]).Append("' hex=").Append(string.Join("/", hexIds[i])).Append("\n");
sb.Append("set: ").Append(string.Join(" | ", setDiag)).Append("\n");
foreach (string s in cloudLines) sb.Append(CB6Cap(s, 500)).Append("\n");
if (warn.Count > 0) sb.Append("warn: ").Append(CB6Cap(string.Join("; ", warn), 600)).Append("\n");
sb.Append("cleanup_params: [\"").Append(string.Join("\",\"", cleanup)).Append("\"]\n");
sb.Append("done=").Append(doneIds.Count).Append(" skipped=").Append(skipIds.Count).Append(" failed=0\n");
sb.Append("done_ids: ").Append(string.Join(",", doneIds)).Append("\n");
sb.Append("skipped_ids: ").Append(string.Join(",", skipIds));
return sb.ToString();
}

// ===================== helpers (CB6 prefix) =====================
// old floor-sink label by structure: "4" ... "FS-1", at most 8 chars, no F-number yet (no inch-mark literal needed)
public static bool CB6IsOld(string s)
{
    if (s == null) return false;
    return s.StartsWith("4", StringComparison.Ordinal) && s.EndsWith("FS-1", StringComparison.Ordinal) && s.Length <= 8 && s.IndexOf("(F-", StringComparison.Ordinal) < 0;
}
// 'text'[char codes], so a substituted inch mark (34 vs 8221/8243) or stray space shows in the return
public static string CB6Show(string s)
{
    if (s == null) return "null";
    System.Text.StringBuilder b = new System.Text.StringBuilder();
    b.Append("'").Append(s).Append("'[");
    for (int k = 0; k < s.Length; k++) { if (k > 0) b.Append(","); b.Append(((int)s[k]).ToString()); }
    return b.Append("]").ToString();
}
public static double CB6GrowF(string cur, string suffix) { return (double)suffix.Length / Math.Max(1, cur.Length); }
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
public static CB6Box CB6Grow(CB6Box b, double f) { double d = (b.x1 - b.x0) * f; return new CB6Box(b.x0 - d, b.y0, b.x1 + d, b.y1, b.id); }
public static bool CB6Ov(CB6Box a, CB6Box b) { double e = 1e-6; return a.x0 < b.x1 - e && b.x0 < a.x1 - e && a.y0 < b.y1 - e && b.y0 < a.y1 - e; }
public static bool CB6OvU(CB6Box b, List<CB6Box> rs) { foreach (CB6Box r in rs) if (CB6Ov(b, r)) return true; return false; }
// true if box b lies entirely inside the union of rs
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
public static CB6Box CB6TagBox(double[] h, double hw, double th) { return new CB6Box(h[0] - hw, h[2], h[0] + hw, h[2] + th, -1); }
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
// union is not a single simple loop (holes, pinch points, separate parts).
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
            if (i == 0 || !cov[i - 1, j]) { if (!CB6Add(nxt, i * K + j, i * K + j + 1)) bad = true; ne++; }                       // left, up
            if (j == ny - 1 || !cov[i, j + 1]) { if (!CB6Add(nxt, i * K + j + 1, (i + 1) * K + j + 1)) bad = true; ne++; }       // top, right
            if (i == nx - 1 || !cov[i + 1, j]) { if (!CB6Add(nxt, (i + 1) * K + j + 1, (i + 1) * K + j)) bad = true; ne++; }     // right, down
            if (j == 0 || !cov[i, j - 1]) { if (!CB6Add(nxt, (i + 1) * K + j, i * K + j)) bad = true; ne++; }                   // bottom, left
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
// TextNote extent: bbox when it is plausible, else Coord + Width/Height (paper ft) x scale by alignment
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
// one cluster per riser branch (grp): hull of the branch's labels that need a cloud + their hex tags, plus margin
public static List<CB6Clu> CB6Build(long[] lbl, string[] state, bool[] covered, CB6Box[] lb, List<long>[] hexIds, List<CB6Box>[] hexBoxes, double margin, int[] grp)
{
    List<CB6Clu> cl = new List<CB6Clu>();
    List<int> gs = new List<int>(); foreach (int g in grp) if (!gs.Contains(g)) gs.Add(g);
    foreach (int g in gs)
    {
        CB6Clu c = new CB6Clu(); List<CB6Box> mb = new List<CB6Box>();
        for (int i = 0; i < lbl.Length; i++)
        {
            if (grp[i] != g || (state[i] != "todo" && state[i] != "already") || covered[i] || lb[i] == null) continue;
            c.labels.Add(lbl[i]); c.members.Add(lbl[i]); c.members.AddRange(hexIds[i]);
            mb.Add(lb[i]); mb.AddRange(hexBoxes[i]);
        }
        if (mb.Count == 0) continue;
        c.rects.Add(CB6Ex(CB6Hull(mb, c.labels[0]), margin));
        cl.Add(c);
    }
    return cl;
}
public static bool CB6Touch(CB6Clu a, CB6Clu b, double clr, double dx, double dy, double hw, double th)
{
    foreach (CB6Box ra in a.rects) if (CB6OvU(CB6Ex(ra, 2 * clr), b.rects)) return true;
    CB6Box ta = CB6TagBox(CB6TagHead(a.rects, dx, dy), hw, th), tb = CB6TagBox(CB6TagHead(b.rects, dx, dy), hw, th);
    return CB6OvU(CB6Ex(ta, clr), b.rects) || CB6OvU(CB6Ex(tb, clr), a.rects) || CB6Ov(CB6Ex(ta, clr), tb);
}
// Merge touching clusters, grow each to enclose any obstacle its outline (+arc clearance) or its tag would cut,
// trace a single clockwise outline (else fall back to the hull rectangle). Returns null when settled, else the reason.
public static string CB6Plan(List<CB6Clu> cl, List<CB6Box> obs, List<CB6Box> hard, double margin, double clr, double inClr, double dx, double dy, double hw, double th, double maxDim, int cap)
{
    for (int it = 0; it < 60; it++)
    {
        bool ch = false; bool mg = true;
        while (mg)
        {
            mg = false;
            for (int a = 0; a < cl.Count && !mg; a++)
                for (int b = a + 1; b < cl.Count && !mg; b++)
                    if (CB6Touch(cl[a], cl[b], clr, dx, dy, hw, th))
                    {
                        cl[a].labels.AddRange(cl[b].labels); cl[a].members.AddRange(cl[b].members);
                        cl[a].rects.AddRange(cl[b].rects); cl[a].added.AddRange(cl[b].added); cl[a].hulled = cl[a].hulled || cl[b].hulled;
                        cl.RemoveAt(b); mg = true; ch = true;
                    }
        }
        foreach (CB6Clu c in cl)
        {
            double[] h = CB6TagHead(c.rects, dx, dy); CB6Box tb = CB6TagBox(h, hw, th);
            foreach (CB6Box x in hard)
                if (CB6OvU(CB6Ex(x, 2 * clr), c.rects) || CB6Ov(CB6Ex(x, clr), tb)) return "cloud for labels " + string.Join("/", c.labels) + " would touch existing cloud " + x.id;
            foreach (CB6Box o in obs)
            {
                if (CB6InU(CB6Ex(o, inClr), c.rects)) continue;
                if (CB6OvU(CB6Ex(o, clr), c.rects) || CB6Ov(o, tb))
                {
                    c.rects.Add(CB6Ex(o, margin)); c.added.Add(o.id); ch = true;
                    if (c.added.Count > cap) return "cloud for labels " + string.Join("/", c.labels) + " would have to enclose more than " + cap + " other items: " + string.Join("/", c.added.Take(15));
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
public static string CB6Desc(CB6Clu c, double dx, double dy, IFormatProvider inv)
{
    System.Text.StringBuilder s = new System.Text.StringBuilder();
    double[] h = CB6TagHead(c.rects, dx, dy);
    s.Append("cloud labels=").Append(string.Join("/", c.labels)).Append(" ").Append(c.mode).Append(" area=").Append(Math.Round(CB6Area(c.loop), 2).ToString(inv)).Append(" loop=");
    foreach (double[] p in c.loop) s.Append("(").Append(Math.Round(p[0], 2).ToString(inv)).Append(",").Append(Math.Round(p[1], 2).ToString(inv)).Append(")");
    s.Append(" head=(").Append(Math.Round(h[0], 2).ToString(inv)).Append(",").Append(Math.Round(h[1], 2).ToString(inv)).Append(")");
    if (c.added.Count > 0) s.Append(" encloses_other=").Append(string.Join("/", c.added.Take(15))).Append(c.added.Count > 15 ? "(+" + (c.added.Count - 15) + ")" : "");
    return s.ToString();
}
public static object DummyTail() { return null;
