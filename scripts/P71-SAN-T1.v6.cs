// operation: sanitary-refit v6 (STAGE B ONLY by default; failure capture; dryrun commits inside a TransactionGroup that is then rolled back) | P71-SAN-T1 | 24-071 Pacific Edge, PLMB-24220-R25 | Surf BEACH
// Run with parameters ["dryrun","B"] (then ["write","B"]). Stage A is already in the model (written with v3): fixtures at targets, new WC present (found by position), dead branches deleted.
// v6 vs v5 (v5 dryrun B: all 7 drawn legs tee'd OK, H6134 tie OK, no insufficient fall, warnings 505 -> 505; rolled back only by the self-check, 15 length "violators" all positive and none reversed):
//   (1) self-check: rolls back BEFORE commit only when a pipe is < 0.1 ft long or its direction flipped; shorter-than-preferred pipes (vertical < 0.5, horizontal < 1.5) are reported as info (selfCheck.info),
//       the Commit (FailuresProcessing handler; group rollback in dryrun) is the real test.
//   (2) LAV6133: if no main pipe is clear at its tee X (needs 0.7 ft from pipe ends), the tee is shifted along the 4in main (west first, then east, 1.1..4.0 ft, so the dogleg diagonal is >= 1.5 ft)
//       to the nearest clear spot that is >= 2.0 ft from the neighbouring tees (WC6133 tee x216.48 east of it) and from the freed ties; leg uses the same N-S / 45 deg / N-S dogleg as the H6134 legs.
//   (3) FD verticals between 0.1 and 0.5 ft are left as they are.
// Everything else as v5: only perpendicular horizontal tees (no vertical into header); leg = drop -> elbow -> N-S run -> [45 deg dogleg] -> perpendicular tee; H6134 header straight into tie 22830110 at the tie Y;
// H6133 has no header (3 legs straight into the 4in main, old tie 22829081 left as is = manual cap); fall budget from the tee upward at 1/4 in/ft (insufficient fall = not drawn, listed).
// parameters[0] = "dryrun" (default) | "write".   parameters[1] = "B" (default) | "A" | "AB".   transactionMode "none".
const string TASK = "P71-SAN-T1";
bool dry = !(parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString().Trim().ToLowerInvariant() == "write");
string stageS = (parameters != null && parameters.Length > 1 && parameters[1] != null) ? parameters[1].ToString().Trim().ToUpperInvariant() : "B";
Func<string, string> fail = (m) => "FAILED " + TASK + ": " + m + "\ndone=0 skipped=0 failed=1";
if (stageS != "A" && stageS != "B" && stageS != "AB") return fail("parameters[1] must be A, B or AB, got '" + stageS + "'");
bool stA = stageS.Contains("A"), stB = stageS.Contains("B");
if (document.Title.IndexOf("PLMB-24220-R25", StringComparison.OrdinalIgnoreCase) < 0) return fail("wrong document open: " + document.Title);
var inv = System.Globalization.CultureInfo.InvariantCulture;
Func<double, string> F = (d) => d.ToString("F2", inv);
Func<XYZ, string> P3 = (p) => p == null ? "null" : "(" + F(p.X) + "," + F(p.Y) + "," + F(p.Z) + ")";
Func<string, string> Q = (s) => "\"" + (s == null ? "" : s.Replace("\\", "/").Replace("\"", "'").Replace("\r", " ").Replace("\n", " ")) + "\"";
Func<long, Element> E = (id) => document.GetElement(new ElementId(id));
Func<IEnumerable<long>, string> LJ = (ids) => "[" + string.Join(",", ids.Select(x => x.ToString())) + "]";
long catPipe = (long)BuiltInCategory.OST_PipeCurves, catFit = (long)BuiltInCategory.OST_PipeFitting, catFix = (long)BuiltInCategory.OST_PlumbingFixtures;
const double MAINY = 169.6, SLOPE = 1.0 / 4.0 / 12.0, DROPMIN = 0.5, HDIA = 0.25, HY = 172.3;
const double L3 = 1.5, TEEGAP = 2.0, RUNMIN = 1.5, VMIN = 0.5, HMIN = 1.5, DOGMIN = 1.06, SQ2 = 1.41421356, HARDMIN = 0.1;

// ---------- failure capture (no class: a lambda on Application.FailuresProcessing) ----------
var capSev = new List<string>(); var capTxt = new List<string>(); var capIds = new List<List<long>>();
var scopes = new List<object[]>();
Action<string, IEnumerable<long>> addScope = (nm, ids) => { scopes.Add(new object[] { nm, new HashSet<long>(ids) }); };
EventHandler<Autodesk.Revit.DB.Events.FailuresProcessingEventArgs> fh = (snd, ea) =>
{
    try
    {
        FailuresAccessor fac = ea.GetFailuresAccessor();
        bool hasErr = false;
        var warnList = new List<FailureMessageAccessor>();
        foreach (FailureMessageAccessor fm in fac.GetFailureMessages())
        {
            FailureSeverity sv = fm.GetSeverity();
            var fids = new List<long>();
            foreach (ElementId fid in fm.GetFailingElementIds()) fids.Add(fid.Value);
            capSev.Add(sv.ToString()); capTxt.Add(fm.GetDescriptionText()); capIds.Add(fids);
            if (sv == FailureSeverity.Warning) warnList.Add(fm);
            else if (sv != FailureSeverity.None) hasErr = true;
        }
        foreach (FailureMessageAccessor wm in warnList) fac.DeleteWarning(wm);
        if (hasErr)
        {
            FailureHandlingOptions fo2 = fac.GetFailureHandlingOptions();
            fo2.SetClearAfterRollback(true);
            fac.SetFailureHandlingOptions(fo2);
            ea.SetProcessingResult(FailureProcessingResult.ProceedWithRollBack);
        }
    }
    catch (Exception exh)
    {
        capSev.Add("HandlerException"); capTxt.Add(exh.GetType().Name + " " + exh.Message); capIds.Add(new List<long>());
        try { ea.SetProcessingResult(FailureProcessingResult.ProceedWithRollBack); } catch (Exception) { }
    }
};
int[] failTotal = new int[] { 0 };
var mainIdSet = new HashSet<long>(); var fixIdLab = new Dictionary<long, string>();
Func<string> failArr = () =>
{
    var seenF = new HashSet<string>(); var outl = new List<string>(); failTotal[0] = 0;
    for (int q = 0; q < capSev.Count; q++)
    {
        string key = capSev[q] + "|" + capTxt[q] + "|" + string.Join(",", capIds[q]);
        if (!seenF.Add(key)) continue;
        failTotal[0]++;
        if (outl.Count >= 10) continue;
        var sn = new List<string>();
        foreach (var sc2 in scopes) if (sn.Count < 4 && capIds[q].Any(x => ((HashSet<long>)sc2[1]).Contains(x))) sn.Add((string)sc2[0]);
        if (capIds[q].Any(x => mainIdSet.Contains(x))) sn.Add("MAIN");
        foreach (long x2 in capIds[q]) { string fl; if (fixIdLab.TryGetValue(x2, out fl)) sn.Add("FIX:" + fl); }
        string tx0 = capTxt[q] == null ? "" : capTxt[q]; if (tx0.Length > 150) tx0 = tx0.Substring(0, 150);
        outl.Add("{\"sev\":" + Q(capSev[q]) + ",\"text\":" + Q(tx0) + ",\"ids\":" + LJ(capIds[q].Take(10)) + ",\"scopes\":[" + string.Join(",", sn.Select(Q)) + "]}");
    }
    return "[" + string.Join(",", outl) + "]";
};

// ---------- level, types, main ----------
Level beach = null;
foreach (Level lv in new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>())
{
    string lvn = lv.Name.ToUpperInvariant();
    if (lvn.Contains("SURF") && lvn.Contains("BEACH") && Math.Abs(lv.ProjectElevation - 132.48) < 0.02) beach = lv;
}
if (beach == null) return fail("level SURF - BEACH (ProjectElevation 132.48) not found");
double BEACHZ = beach.ProjectElevation;
Autodesk.Revit.DB.Plumbing.PipeType pipeT = null;
foreach (var ptq in new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.Plumbing.PipeType)).Cast<Autodesk.Revit.DB.Plumbing.PipeType>())
    if (ptq.Name == "Gravity Flow - Wye Combo 8th Bend") pipeT = ptq;
if (pipeT == null) return fail("pipe type 'Gravity Flow - Wye Combo 8th Bend' not found");
long[] mainKnown = new long[] { 22819714, 22820775, 22828886, 22820365, 22820407 };
foreach (long mk0 in mainKnown)
    if ((E(mk0) as Autodesk.Revit.DB.Plumbing.Pipe) == null) return fail("main pipe " + mk0 + " not found");
var mainRef = E(22820775) as Autodesk.Revit.DB.Plumbing.Pipe;
Parameter spm = mainRef.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM);
if (spm == null) return fail("main pipe has no system type parameter");
ElementId sysTypeId = spm.AsElementId();
Element sysTypeEl = document.GetElement(sysTypeId);
if (sysTypeEl == null || sysTypeEl.Name.ToUpperInvariant().IndexOf("SAN", StringComparison.Ordinal) < 0) return fail("main system type is '" + (sysTypeEl == null ? "null" : sysTypeEl.Name) + "', expected a SAN system");
int mainWs = mainRef.WorksetId.IntegerValue;

Func<List<Autodesk.Revit.DB.Plumbing.Pipe>> mainPipes = () =>
{
    var res = new List<Autodesk.Revit.DB.Plumbing.Pipe>();
    foreach (var p in new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.Plumbing.Pipe)).Cast<Autodesk.Revit.DB.Plumbing.Pipe>())
    {
        var lc = p.Location as LocationCurve; if (lc == null) continue;
        var ln = lc.Curve as Line; if (ln == null) continue;
        XYZ a = ln.GetEndPoint(0), b = ln.GetEndPoint(1);
        if (Math.Abs(a.Y - MAINY) > 0.25 || Math.Abs(b.Y - MAINY) > 0.25) continue;
        if (Math.Min(a.X, b.X) < 190 || Math.Max(a.X, b.X) > 265) continue;
        if (Math.Abs(a.X - b.X) < 0.3) continue;
        if (p.Diameter < 0.30 || p.Diameter > 0.37) continue;
        Parameter sp = p.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM);
        if (sp == null || sp.AsElementId().Value != sysTypeId.Value) continue;
        res.Add(p);
    }
    return res;
};
var mainAll = mainPipes();
if (mainAll.Count < 5) return fail("only " + mainAll.Count + " main pipes found along y=169.6");
foreach (var mpx in mainAll) mainIdSet.Add(mpx.Id.Value);

// ---------- connector helpers ----------
Func<Element, List<Connector>> conns = (el) =>
{
    var l = new List<Connector>();
    ConnectorManager cm = null;
    var fiq = el as FamilyInstance;
    if (fiq != null) { if (fiq.MEPModel != null) cm = fiq.MEPModel.ConnectorManager; }
    else { var mc = el as MEPCurve; if (mc != null) cm = mc.ConnectorManager; }
    if (cm != null) foreach (Connector c in cm.Connectors) l.Add(c);
    return l;
};
Func<Element, List<Connector[]>> pairsOf = (el) =>
{
    var l = new List<Connector[]>();
    foreach (Connector c in conns(el))
    {
        if (c.ConnectorType == ConnectorType.Logical) continue;
        foreach (Connector r in c.AllRefs)
        {
            if (r.ConnectorType == ConnectorType.Logical) continue;
            Element ow = r.Owner; if (ow == null || ow is MEPSystem || ow.Id.Value == el.Id.Value) continue;
            l.Add(new Connector[] { c, r });
        }
    }
    return l;
};
Func<long, XYZ, Connector> nearC = (id, pt) =>
{
    Element el = E(id); if (el == null) return null;
    Connector best = null; double bd = 1e9;
    foreach (Connector c in conns(el))
    {
        if (c.ConnectorType == ConnectorType.Logical) continue;
        double d = c.Origin.DistanceTo(pt); if (d < bd) { bd = d; best = c; }
    }
    return bd < 0.08 ? best : null;
};

// ---------- fixtures ----------
string[] fLab = new string[] { "WC6133", "LAV6133", "FD6133", "LAV6134", "FD6134", "FD_MECH", "FS_MECH" };
long[] fId = new long[] { 22810384, 22818820, 22828284, 22819280, 22829763, 22832174, 22828726 };
double[] fX = new double[] { 216.48, 213.86, 219.18, 237.59, 233.8, 236.4, 238.7 };
double[] fY = new double[] { 173.78, 178.46, 184.00, 179.36, 176.3, 182.0, 181.3 };
double[] fDia = new double[] { 0.25, 2.0 / 12.0, 0.25, 2.0 / 12.0, 0.25, 0.25, 0.25 };
long[] fArch = new long[] { 14106300, 34502374, 0, 34500383, 0, 0, 0 };
double[] fExpZ = new double[] { 132.67, 133.63, 131.30, 131.30, 131.30, 131.30, 131.30 };
int NF = fId.Length;
for (int i = 0; i < NF; i++) fixIdLab[fId[i]] = fLab[i];
// new wall WC for 6134 (copy of host 22812032) - found by position in stage B
const long HOSTWC = 22812032;
double nwX = 235.23, nwY = 173.41; long nwArch = 29581436;
if (!(fX[4] >= 231.47 + 1.0 && fX[4] <= 238.96 - 1.0 && fY[4] >= 173.12 + 1.0 && fY[4] <= 179.36 - 1.0)) return fail("FD6134 point outside room 6134 with 1 ft margin");
if (!(fX[5] >= 234.98 && fX[5] <= 240.07 && fY[5] >= 179.93 && fY[5] <= 184.13 && fX[6] >= 234.98 && fX[6] <= 240.07 && fY[6] >= 179.93 && fY[6] <= 184.13)) return fail("MECH points outside room 6139");
var fFi = new FamilyInstance[NF];
var info = new List<string>();
for (int i = 0; i < NF; i++)
{
    fFi[i] = E(fId[i]) as FamilyInstance;
    if (fFi[i] == null) return fail("fixture " + fId[i] + " (" + fLab[i] + ") not found or not a family instance");
    var lp0 = fFi[i].Location as LocationPoint; if (lp0 == null) return fail("fixture " + fId[i] + " has no point location");
    if (Math.Abs(lp0.Point.Z - fExpZ[i]) > 0.06) return fail("fixture " + fId[i] + " Z=" + F(lp0.Point.Z) + " expected " + F(fExpZ[i]) + " (scout stale?)");
    if (!stA && (Math.Abs(lp0.Point.X - fX[i]) > 0.1 || Math.Abs(lp0.Point.Y - fY[i]) > 0.1)) return fail("stage B: fixture " + fLab[i] + " is at " + P3(lp0.Point) + ", not at its stage-A target (" + F(fX[i]) + "," + F(fY[i]) + ") - run stage A first");
}
var hostFi = E(HOSTWC) as FamilyInstance;
if (hostFi == null) return fail("host wall WC " + HOSTWC + " not found");
var hostLp = hostFi.Location as LocationPoint; if (hostLp == null) return fail("host WC has no point location");
if (Math.Abs(hostLp.Point.Z - BEACHZ - 9.60) > 0.06) return fail("host WC Z=" + F(hostLp.Point.Z) + ", expected Beach+9.60=" + F(BEACHZ + 9.60));
Level hostLevel = document.GetElement(hostFi.LevelId) as Level;
if (hostLevel == null || Math.Abs(hostLevel.ProjectElevation - BEACHZ) > 0.02) return fail("host WC level is not Beach: " + (hostLevel == null ? "null" : hostLevel.Name));
string hostNameFor = hostFi.Symbol.FamilyName + " : " + hostFi.Symbol.Name;
int hostWs = hostFi.WorksetId.IntegerValue;
FamilyInstance nwcExist = null;
foreach (FamilyInstance wq in new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PlumbingFixtures).WhereElementIsNotElementType().OfType<FamilyInstance>())
{
    if (wq.Id.Value == HOSTWC || wq.Symbol == null || wq.Symbol.FamilyName != hostFi.Symbol.FamilyName) continue;
    var wlp = wq.Location as LocationPoint; if (wlp == null) continue;
    if (Math.Abs(wlp.Point.X - nwX) <= 0.2 && Math.Abs(wlp.Point.Y - nwY) <= 0.2 && Math.Abs(wlp.Point.Z - BEACHZ) <= 0.5) { nwcExist = wq; break; }
}
if (!stA && nwcExist == null) return fail("stage B: new WC (" + hostNameFor + ") not found within 0.2 ft of (" + F(nwX) + "," + F(nwY) + ") - run stage A first");

// ---------- arch link: facing of target fixtures ----------
var linkInsts = new FilteredElementCollector(document).OfClass(typeof(RevitLinkInstance)).Cast<RevitLinkInstance>().ToList();
var archFacing = new Dictionary<long, XYZ>();
double[] archChkX = new double[] { 216.48, 213.86, 235.23, 237.59 };
double[] archChkY = new double[] { 173.78, 178.46, 173.41, 179.36 };
long[] archIds = new long[] { 14106300, 34502374, 29581436, 34500383 };
for (int k = 0; k < archIds.Length; k++)
{
    bool found = false;
    foreach (var li in linkInsts)
    {
        Document ld = li.GetLinkDocument(); if (ld == null) continue;
        var af = ld.GetElement(new ElementId(archIds[k])) as FamilyInstance; if (af == null) continue;
        var alp = af.Location as LocationPoint; if (alp == null) continue;
        Transform tr = li.GetTotalTransform();
        XYZ ap = tr.OfPoint(alp.Point);
        if (Math.Abs(ap.X - archChkX[k]) > 0.15 || Math.Abs(ap.Y - archChkY[k]) > 0.15) continue;
        archFacing[archIds[k]] = tr.OfVector(af.FacingOrientation);
        info.Add("arch " + archIds[k] + " at " + P3(ap) + " facing " + P3(archFacing[archIds[k]]));
        found = true; break;
    }
    if (!found) return fail("arch element " + archIds[k] + " not found in any link at (" + F(archChkX[k]) + "," + F(archChkY[k]) + ")");
}

// ---------- delete lists (stage A only) ----------
long[] delFix = new long[] { 20158870 };
long[] delBranch = new long[] { 22810385, 22810864, 22810728, 22818899, 22819708, 22819711, 22818902, 22819404, 22820360, 22819407,
    22828927, 22829179, 22828933, 22829175, 22828889, 22829061, 22829077, 22829063, 22830189, 22830206, 22830194, 22830210, 22830100 };
long[] delVent = new long[] { 22810382, 22812985, 22812974, 22813013, 22813226, 22813253, 22813217 };
var Dids = new List<long>(); if (stA) { Dids.AddRange(delFix); Dids.AddRange(delBranch); Dids.AddRange(delVent); }
var movedSet = new HashSet<long>(fId); movedSet.Add(HOSTWC);
var absent = new List<long>(); var keepD = new List<string>(); var Dact = new List<long>();
var DallSet = new HashSet<long>(Dids);
foreach (long did in Dids)
{
    Element de = E(did);
    if (de == null) { absent.Add(did); continue; }
    long dcat = de.Category == null ? 0 : de.Category.Id.Value;
    if (dcat != catPipe && dcat != catFit && dcat != catFix) { keepD.Add(did + " category"); continue; }
    if (dcat == catFit && conns(de).Count(cq => cq.ConnectorType != ConnectorType.Logical && cq.IsConnected) >= 3) { keepD.Add(did + " shared tie"); continue; }
    bool other = false;
    foreach (var pr in pairsOf(de))
    {
        Element ow = pr[1].Owner; long oc = ow.Category == null ? 0 : ow.Category.Id.Value;
        if (oc == catFix && !DallSet.Contains(ow.Id.Value) && !movedSet.Contains(ow.Id.Value)) other = true;
    }
    if (other) { keepD.Add(did + " serves another fixture"); continue; }
    Dact.Add(did);
}
var DactSet = new HashSet<long>(Dact);
var boundary = new List<object[]>();
var checkIds = new HashSet<long>();
foreach (long did in Dact)
{
    checkIds.Add(did);
    foreach (var pr in pairsOf(E(did)))
    {
        long oid = pr[1].Owner.Id.Value;
        checkIds.Add(oid);
        if (!DactSet.Contains(oid)) boundary.Add(new object[] { oid, pr[1].Origin });
    }
}
for (int i = 0; i < NF; i++)
{
    checkIds.Add(fId[i]);
    foreach (var pr in pairsOf(fFi[i])) checkIds.Add(pr[1].Owner.Id.Value);
}
foreach (var mpa in mainAll) checkIds.Add(mpa.Id.Value);
if (nwcExist != null) checkIds.Add(nwcExist.Id.Value);
foreach (long tk1 in new long[] { 22829081, 22830110 }) if (E(tk1) != null) checkIds.Add(tk1);

// ---------- checkout ----------
var badOwn = new List<string>();
if (document.IsWorkshared)
{
    foreach (long cid in checkIds)
    {
        try
        {
            string who;
            CheckoutStatus st = WorksharingUtils.GetCheckoutStatus(document, new ElementId(cid), out who);
            if (st == CheckoutStatus.OwnedByOtherUser) badOwn.Add(cid + " owner " + who);
        }
        catch (Exception) { }
    }
}
if (badOwn.Count > 0) return "FAILED " + TASK + ": elements borrowed by other users (editing would raise a blocking dialog): " + string.Join("; ", badOwn.Take(20)) + "\ndone=0 skipped=0 failed=" + badOwn.Count + "\n\"failures\":[]";

// ---------- connector report (read-only) ----------
Func<FamilyInstance, Connector> pickSan = (fq) =>
{
    Connector best = null;
    foreach (Connector c in conns(fq))
    {
        if (c.ConnectorType == ConnectorType.Logical) continue;
        if (c.PipeSystemType == Autodesk.Revit.DB.Plumbing.PipeSystemType.Sanitary) { if (best == null || c.Origin.Z < best.Origin.Z) best = c; }
    }
    if (best != null) return best;
    foreach (Connector c in conns(fq))
    {
        if (c.ConnectorType == ConnectorType.Logical) continue;
        var sys = c.PipeSystemType;
        if (sys == Autodesk.Revit.DB.Plumbing.PipeSystemType.Global || sys == Autodesk.Revit.DB.Plumbing.PipeSystemType.UndefinedSystemType || sys == Autodesk.Revit.DB.Plumbing.PipeSystemType.OtherPipe)
            if (best == null || c.Origin.Z < best.Origin.Z) best = c;
    }
    return best;
};
Func<FamilyInstance, string> connReport = (fq) =>
{
    var parts = new List<string>();
    foreach (Connector c in conns(fq))
    {
        if (c.ConnectorType == ConnectorType.Logical) continue;
        parts.Add(c.PipeSystemType + "@" + P3(c.Origin) + " dir" + P3(c.CoordinateSystem.BasisZ) + " r" + F(c.Radius) + (c.IsConnected ? " conn" : " free"));
    }
    return string.Join("; ", parts);
};
var connRep = new List<string>();
for (int i = 0; i < NF; i++) connRep.Add(fLab[i] + " " + fId[i] + ": " + connReport(fFi[i]));
connRep.Add("host " + HOSTWC + " " + hostNameFor + " host=" + (hostFi.Host == null ? "none" : hostFi.Host.GetType().Name + hostFi.Host.Id.Value) + ": " + connReport(hostFi));
if (nwcExist != null) connRep.Add("newWC " + nwcExist.Id.Value + ": " + connReport(nwcExist));
int warnBefore = document.GetWarnings().Count;

// ---------- everything below runs inside ONE TransactionGroup / ONE Transaction ----------
var createdPipes = new List<long>(); var createdFits = new List<long>();
var manual = new List<string>(); var deleted = new List<long>(); var cascade = new List<long>(); var delFailed = new List<string>();
var movedOut = new List<string>(); var discOut = new List<string>(); var branchOut = new List<string>();
var stubsOut = new List<string>(); long newWcId = 0;
var reconnect = new List<string>(); var notes = new List<string>(); var hdrOut = new List<string>();
var knownOpen = new HashSet<long>(); var okOpen = new List<XYZ>();
var fixInstances = new List<FamilyInstance>(); var fixLabels = new List<string>(); var fixDia = new List<double>();
var drawnDir = new Dictionary<long, XYZ>(); long[] lastStub = new long[] { 0 };
FamilyInstance nwc = nwcExist; string nwHow = nwcExist != null ? "existing" : "";
TransactionGroup grp = new TransactionGroup(document, TASK);
Transaction tx = new Transaction(document, TASK);
try
{
    document.Application.FailuresProcessing += fh;
    grp.Start();
    tx.Start();
    FailureHandlingOptions fho = tx.GetFailureHandlingOptions();
    fho.SetForcedModalHandling(false); fho.SetClearAfterRollback(true);
    tx.SetFailureHandlingOptions(fho);

    // A. delete dead branches first (so moves cannot drag them) - stage A only
    foreach (long did in Dact)
    {
        Element e = E(did); if (e == null) continue;
        try
        {
            if (e.Pinned) e.Pinned = false;
            ICollection<ElementId> r = document.Delete(e.Id);
            deleted.Add(did);
            foreach (ElementId x in r) if (x.Value != did && !DallSet.Contains(x.Value)) cascade.Add(x.Value);
        }
        catch (Exception ex) { delFailed.Add(did + ":" + ex.GetType().Name + " " + ex.Message); }
    }
    document.Regenerate();

    // B. open stubs left on boundary elements (stage A) or freed ties found directly (stage B alone)
    var stubs = new List<object[]>(); var seen = new HashSet<string>();
    if (stA)
    {
        foreach (var b in boundary)
        {
            long oid = (long)b[0]; XYZ org = (XYZ)b[1];
            Element o = E(oid); if (o == null) continue;
            foreach (Connector c in conns(o))
            {
                if (c.ConnectorType == ConnectorType.Logical) continue;
                if (c.Origin.DistanceTo(org) < 0.15 && !c.IsConnected)
                {
                    string key = oid + "|" + F(c.Origin.X) + "|" + F(c.Origin.Y) + "|" + F(c.Origin.Z);
                    bool isFit = (o is FamilyInstance) && o.Category != null && o.Category.Id.Value == catFit;
                    if (seen.Add(key)) stubs.Add(new object[] { oid, c.Origin, c.Radius * 2.0, isFit, c.PipeSystemType.ToString(), false, c.CoordinateSystem.BasisZ });
                }
            }
        }
    }
    else
    {
        long[] tieKnown = new long[] { 22829081, 22830110 };
        List<FamilyInstance> scanFits;
        if (tieKnown.All(k0 => E(k0) as FamilyInstance != null)) scanFits = tieKnown.Select(k0 => E(k0) as FamilyInstance).ToList();
        else scanFits = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipeFitting).WhereElementIsNotElementType().OfType<FamilyInstance>().ToList();
        foreach (FamilyInstance ft in scanFits)
        {
            foreach (Connector c in conns(ft))
            {
                if (c.ConnectorType == ConnectorType.Logical || c.IsConnected) continue;
                XYZ fo = c.Origin;
                if (fo.X < 205 || fo.X > 250 || fo.Y < MAINY + 0.3 || fo.Y > HY + 2.5) continue;
                if (Math.Abs(c.Radius * 2.0 - HDIA) > 0.02) continue;
                string key = ft.Id.Value + "|" + F(fo.X) + "|" + F(fo.Y) + "|" + F(fo.Z);
                if (seen.Add(key)) stubs.Add(new object[] { ft.Id.Value, fo, c.Radius * 2.0, true, c.PipeSystemType.ToString(), false, c.CoordinateSystem.BasisZ });
            }
        }
    }
    foreach (var st0 in stubs) stubsOut.Add(((long)st0[0]) + (((bool)st0[3]) ? " fitting " : " pipe ") + P3((XYZ)st0[1]) + " d" + F((double)st0[2]) + " " + st0[4] + " dir" + P3((XYZ)st0[6]));

    // C. rotate/move fixtures; new wall WC for 6134 (stage A only)
    Func<FamilyInstance, long, double, double, string> placeFixture = (fq, archId, tx2, ty2) =>
    {
        var lp = fq.Location as LocationPoint; XYZ p0 = lp.Point;
        double rot = 0; XYZ af2;
        XYZ hf = fq.FacingOrientation;
        if (archId != 0 && archFacing.TryGetValue(archId, out af2) && hf != null && hf.GetLength() > 0.1 && af2.GetLength() > 0.1)
            rot = Math.Atan2(hf.X * af2.Y - hf.Y * af2.X, hf.X * af2.X + hf.Y * af2.Y);
        if (Math.Abs(rot) > 0.0087) ElementTransformUtils.RotateElement(document, fq.Id, Line.CreateBound(p0, p0 + XYZ.BasisZ), rot);
        lp = fq.Location as LocationPoint; p0 = lp.Point;
        ElementTransformUtils.MoveElement(document, fq.Id, new XYZ(tx2 - p0.X, ty2 - p0.Y, 0));
        return "rot" + F(rot * 180.0 / Math.PI) + "deg";
    };
    if (stA)
    {
        for (int i = 0; i < NF; i++)
        {
            foreach (var pr in pairsOf(fFi[i]))
            {
                string nm = pr[1].Owner.Id.Value + " " + pr[0].PipeSystemType + "@" + P3(pr[0].Origin);
                try { pr[0].DisconnectFrom(pr[1]); discOut.Add(fLab[i] + " " + nm); reconnect.Add(fLab[i] + " " + nm); }
                catch (Exception ex) { discOut.Add(fLab[i] + " " + nm + " DISCONNECT FAILED " + ex.Message); manual.Add("disconnect failed " + fLab[i] + " " + nm + ": " + ex.Message); }
            }
        }
        document.Regenerate();
        for (int i = 0; i < NF; i++)
        {
            string rr = placeFixture(fFi[i], fArch[i], fX[i], fY[i]);
            var lpn = fFi[i].Location as LocationPoint;
            movedOut.Add(fLab[i] + " " + fId[i] + " " + P3(lpn.Point) + " " + rr);
        }
        notes.Add("FD6133 22828284 free +Z sanitary connector = trap primer/top connector: ignored");
        if (nwc == null)
        {
            try
            {
                ICollection<ElementId> cp = ElementTransformUtils.CopyElement(document, hostFi.Id, new XYZ(nwX - hostLp.Point.X, nwY - hostLp.Point.Y, -9.60));
                foreach (ElementId cid in cp) { var c1 = document.GetElement(cid) as FamilyInstance; if (c1 != null) { nwc = c1; break; } }
                nwHow = "CopyElement";
            }
            catch (Exception ex) { nwHow = "CopyElement failed: " + ex.Message; }
        }
        if (nwc == null)
        {
            try
            {
                nwc = document.Create.NewFamilyInstance(new XYZ(nwX, nwY, BEACHZ), hostFi.Symbol, beach, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                nwHow += " -> NewFamilyInstance";
            }
            catch (Exception ex) { throw new Exception("new wall WC could not be created: " + nwHow + " / " + ex.GetType().Name + " " + ex.Message); }
        }
        try
        {
            Parameter nwWsP = nwc.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
            if (nwWsP != null && !nwWsP.IsReadOnly && nwWsP.AsInteger() != hostWs) nwWsP.Set(hostWs);
        }
        catch (Exception ex) { manual.Add("new WC workset not set: " + ex.GetType().Name + " " + ex.Message); }
        document.Regenerate();
        if (nwc.WorksetId.IntegerValue != hostWs) manual.Add("new WC " + nwc.Id.Value + " workset " + nwc.WorksetId.IntegerValue + " != source " + hostWs);
        newWcId = nwc.Id.Value;
        string nrr = placeFixture(nwc, nwArch, nwX, nwY);
        var nlp = nwc.Location as LocationPoint;
        if (Math.Abs(nlp.Point.Z - BEACHZ) > 0.06) { ElementTransformUtils.MoveElement(document, nwc.Id, new XYZ(0, 0, BEACHZ - nlp.Point.Z)); nlp = nwc.Location as LocationPoint; }
        movedOut.Add("WC6134_NEW " + newWcId + " " + P3(nlp.Point) + " " + nrr + " via " + nwHow + " type " + hostNameFor + " workset=" + nwc.WorksetId.IntegerValue + " hostWorkset=" + hostWs);
        document.Regenerate();
    }
    else
    {
        newWcId = nwc.Id.Value;
        notes.Add("stage B only: fixtures and new WC " + newWcId + " taken as found");
    }
    for (int i = 0; i < NF; i++) { fixInstances.Add(fFi[i]); fixLabels.Add(fLab[i]); fixDia.Add(fDia[i]); }
    fixInstances.Add(nwc); fixLabels.Add("WC6134"); fixDia.Add(0.25);
    fixIdLab[nwc.Id.Value] = "NEWWC";

    // D. headers + legs
    Action<Element> setWs = (el) =>
    {
        try
        {
            if (el == null) return;
            Parameter wq = el.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
            if (wq != null && !wq.IsReadOnly && wq.AsInteger() != mainWs) wq.Set(mainWs);
        }
        catch (Exception) { }
    };
    Action<FamilyInstance> regFit = (ff) => { createdFits.Add(ff.Id.Value); setWs(ff); };
    Func<IEnumerable<Autodesk.Revit.DB.Plumbing.Pipe>, double, double[]> hitOn = (pipes, xq) =>
    {
        foreach (var mp in pipes)
        {
            var mlc = mp.Location as LocationCurve; if (mlc == null) continue;
            var ml = mlc.Curve as Line; if (ml == null) continue;
            XYZ a = ml.GetEndPoint(0), b = ml.GetEndPoint(1);
            if (Math.Abs(a.X - b.X) < 0.3) continue;
            double x0 = Math.Min(a.X, b.X), x1 = Math.Max(a.X, b.X);
            if (xq >= x0 + 0.7 && xq <= x1 - 0.7)
            {
                double t = (xq - a.X) / (b.X - a.X);
                return new double[] { mp.Id.Value, xq, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t };
            }
        }
        return null;
    };
    Func<double, double[]> mainAt = (xq) => hitOn(mainPipes(), xq);
    double hdrY = 0;
    Func<double, double[]> hdrAt = (xq) =>
    {
        var hl = new List<Autodesk.Revit.DB.Plumbing.Pipe>();
        foreach (long hid in createdPipes)
        {
            var hp = E(hid) as Autodesk.Revit.DB.Plumbing.Pipe; if (hp == null) continue;
            var hlc = hp.Location as LocationCurve; if (hlc == null) continue;
            var hln = hlc.Curve as Line; if (hln == null) continue;
            XYZ a = hln.GetEndPoint(0), b = hln.GetEndPoint(1);
            if (Math.Abs(a.Y - hdrY) > 0.05 || Math.Abs(b.Y - hdrY) > 0.05) continue;
            if (Math.Abs(hp.Diameter - HDIA) > 0.02) continue;
            hl.Add(hp);
        }
        return hitOn(hl, xq);
    };
    Func<XYZ, XYZ, double, Autodesk.Revit.DB.Plumbing.Pipe> mkPipe = (pa, pb, pdia) =>
    {
        var p = Autodesk.Revit.DB.Plumbing.Pipe.Create(document, sysTypeId, pipeT.Id, beach.Id, pa, pb);
        if (p == null) throw new Exception("Pipe.Create returned null");
        createdPipes.Add(p.Id.Value);
        drawnDir[p.Id.Value] = (pb - pa).Normalize();
        Parameter dp = p.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM);
        if (dp == null || dp.IsReadOnly || !dp.Set(pdia)) throw new Exception("cannot set diameter on " + p.Id.Value);
        setWs(p);
        return p;
    };
    Func<List<XYZ>, List<XYZ>> dedupeW = (w0) =>
    {
        var r0 = new List<XYZ>();
        foreach (XYZ q0 in w0) { if (r0.Count == 0 || r0[r0.Count - 1].DistanceTo(q0) >= 0.05) r0.Add(q0); }
        return r0;
    };
    Func<List<XYZ>, double, List<Autodesk.Revit.DB.Plumbing.Pipe>> mkPath = (w1, pdia) =>
    {
        var l1 = new List<Autodesk.Revit.DB.Plumbing.Pipe>();
        for (int k = 0; k + 1 < w1.Count; k++) l1.Add(mkPipe(w1[k], w1[k + 1], pdia));
        return l1;
    };
    // join two pipes at V: collinear (<8 deg) -> ConnectTo, else elbow. returns "" or the error text
    Func<Autodesk.Revit.DB.Plumbing.Pipe, Autodesk.Revit.DB.Plumbing.Pipe, XYZ, string> joinAt = (pa, pb, vv) =>
    {
        try
        {
            Connector ca = nearC(pa.Id.Value, vv), cb = nearC(pb.Id.Value, vv);
            if (ca == null || cb == null) return "no pipe end at " + P3(vv);
            Line la = (Line)((LocationCurve)pa.Location).Curve; Line lb = (Line)((LocationCurve)pb.Location).Curve;
            XYZ fa = la.GetEndPoint(0).DistanceTo(vv) > la.GetEndPoint(1).DistanceTo(vv) ? la.GetEndPoint(0) : la.GetEndPoint(1);
            XYZ fb = lb.GetEndPoint(0).DistanceTo(vv) > lb.GetEndPoint(1).DistanceTo(vv) ? lb.GetEndPoint(0) : lb.GetEndPoint(1);
            double angj = (vv - fa).Normalize().AngleTo((fb - vv).Normalize()) * 180.0 / Math.PI;
            if (angj < 8.0) { ca.ConnectTo(cb); return ""; }
            FamilyInstance elj = document.Create.NewElbowFitting(ca, cb);
            if (elj == null) return "null elbow";
            regFit(elj);
            return "";
        }
        catch (Exception exj) { return exj.GetType().Name + " " + exj.Message; }
    };
    Action<string, Autodesk.Revit.DB.Plumbing.Pipe, Autodesk.Revit.DB.Plumbing.Pipe, XYZ> joint = (labj, pa, pb, vv) =>
    {
        var subj = new SubTransaction(document); int cfj = createdFits.Count;
        try
        {
            subj.Start();
            string rj = joinAt(pa, pb, vv); if (rj != "") throw new Exception(rj);
            subj.Commit();
            addScope(labj + "/joint" + P3(vv), new long[] { pa.Id.Value, pb.Id.Value }.Concat(createdFits.Skip(cfj)));
        }
        catch (Exception exj)
        {
            try { if (subj.GetStatus() == TransactionStatus.Started) subj.RollBack(); } catch (Exception) { }
            if (createdFits.Count > cfj) createdFits.RemoveRange(cfj, createdFits.Count - cfj);
            manual.Add(labj + ": joint not made at " + P3(vv) + " between pipes " + pa.Id.Value + "/" + pb.Id.Value + ": " + exj.Message);
        }
    };
    // PERPENDICULAR tee (the only proven mode): the final horizontal run U -> T (T on the header/main at tgx) is created here, joined to `up` at U (when up != null),
    // the target is broken at T and a tee is made. lastStub[0] = id of the run that was kept. returns "" or the error text
    Func<string, Autodesk.Revit.DB.Plumbing.Pipe, XYZ, double, Func<double, double[]>, double, string> tieIn = (labt, up, uu, pdia, findAt, tgx) =>
    {
        double[] h0 = findAt(tgx);
        if (h0 == null) return "no target pipe at x" + F(tgx);
        long upId = up == null ? 0 : up.Id.Value;
        string errs = "";
        var subt = new SubTransaction(document); int cp0 = createdPipes.Count, cf0 = createdFits.Count;
        try
        {
            subt.Start();
            XYZ tm = new XYZ(h0[1], h0[2], h0[3]);
            var stub = mkPipe(uu, tm, pdia);
            if (up != null) { string je = joinAt(up, stub, uu); if (je != "") throw new Exception("join at U: " + je); }
            document.Regenerate();
            ElementId nid = Autodesk.Revit.DB.Plumbing.PlumbingUtils.BreakCurve(document, new ElementId((long)h0[0]), tm);
            if (nid == null || nid.Value == ElementId.InvalidElementId.Value) throw new Exception("BreakCurve returned no id");
            document.Regenerate();
            Connector m1 = nearC((long)h0[0], tm), m2 = nearC(nid.Value, tm), m3 = nearC(stub.Id.Value, tm);
            if (m1 == null || m2 == null || m3 == null) throw new Exception("no connector at tee point");
            FamilyInstance tee = document.Create.NewTeeFitting(m1, m2, m3);
            if (tee == null) throw new Exception("null tee");
            createdPipes.Add(nid.Value); setWs(E(nid.Value)); regFit(tee);
            subt.Commit();
            lastStub[0] = stub.Id.Value;
            addScope(labt + "/tee", new long[] { stub.Id.Value, upId, (long)h0[0] }.Concat(createdFits.Skip(cf0)).Concat(createdPipes.Skip(cp0)));
            notes.Add(labt + ": perp tee at " + P3(tm));
            return "";
        }
        catch (Exception ext)
        {
            try { if (subt.GetStatus() == TransactionStatus.Started) subt.RollBack(); } catch (Exception) { }
            if (createdPipes.Count > cp0) createdPipes.RemoveRange(cp0, createdPipes.Count - cp0);
            if (createdFits.Count > cf0) createdFits.RemoveRange(cf0, createdFits.Count - cf0);
            errs = "perp tee: " + ext.GetType().Name + " " + ext.Message + "; ";
        }
        // tee failed: keep the run open so the manual fix is one connect
        try
        {
            var subk = new SubTransaction(document); int cp1 = createdPipes.Count, cf1 = createdFits.Count;
            try
            {
                subk.Start();
                XYZ tk0 = new XYZ(h0[1], h0[2], h0[3]);
                var stub0 = mkPipe(uu, tk0, pdia);
                if (up != null) { string jk = joinAt(up, stub0, uu); if (jk != "") throw new Exception(jk); }
                subk.Commit();
                lastStub[0] = stub0.Id.Value;
                knownOpen.Add(stub0.Id.Value);
                return errs + "run " + stub0.Id.Value + " kept open at " + P3(tk0) + " (tee manual)";
            }
            catch (Exception exk)
            {
                try { if (subk.GetStatus() == TransactionStatus.Started) subk.RollBack(); } catch (Exception) { }
                if (createdPipes.Count > cp1) createdPipes.RemoveRange(cp1, createdPipes.Count - cp1);
                if (createdFits.Count > cf1) createdFits.RemoveRange(cf1, createdFits.Count - cf1);
                return errs + "run not kept: " + exk.Message;
            }
        }
        catch (Exception) { return errs; }
    };
    // header pipe already ends AT the tie connector (drawn along -tieDir): just connect
    Func<Autodesk.Revit.DB.Plumbing.Pipe, XYZ, object[], string> tieReuse = (lastp, tt, cd) =>
    {
        var subr = new SubTransaction(document);
        try
        {
            subr.Start();
            Connector ce = nearC(lastp.Id.Value, tt), ct = nearC((long)cd[0], tt);
            if (ce == null || ct == null) throw new Exception("connector not found at tie");
            if (ct.IsConnected) throw new Exception("tie connector already used");
            ce.ConnectTo(ct);
            subr.Commit();
            addScope("tieReuse" + (long)cd[0], new long[] { lastp.Id.Value, (long)cd[0] });
            cd[5] = true;
            return "";
        }
        catch (Exception exr)
        {
            try { if (subr.GetStatus() == TransactionStatus.Started) subr.RollBack(); } catch (Exception) { }
            return exr.GetType().Name + " " + exr.Message;
        }
    };

    // tee X planner (DP): n fixtures sorted by X; each keeps its X (cost 0) or shifts by a dogleg d in 1.1..4.0 ft (needs lh >= 3.0 + |d|); neighbours >= TEEGAP; last tee <= limit
    Func<List<double>, List<double>, double, double[]> planTees = (sxl, lhl, limit) =>
    {
        int n = sxl.Count;
        var cs = new double[n][]; var dp = new double[n][]; var bk = new int[n][];
        for (int i = 0; i < n; i++)
        {
            var cl = new List<double>();
            cl.Add(sxl[i]);
            for (double dd = 1.1; dd <= 4.001; dd += 0.1) if (lhl[i] >= 3.0 + dd) { cl.Add(sxl[i] + dd); cl.Add(sxl[i] - dd); }
            cs[i] = cl.ToArray(); dp[i] = new double[cl.Count]; bk[i] = new int[cl.Count];
        }
        for (int j = 0; j < cs[0].Length; j++) { dp[0][j] = cs[0][j] > limit ? 1e9 : Math.Abs(cs[0][j] - sxl[0]); bk[0][j] = -1; }
        for (int i = 1; i < n; i++)
            for (int j = 0; j < cs[i].Length; j++)
            {
                double best = 1e9; int bi = -1;
                for (int k = 0; k < cs[i - 1].Length; k++)
                    if (dp[i - 1][k] < 1e8 && cs[i][j] - cs[i - 1][k] >= TEEGAP - 1e-6 && dp[i - 1][k] < best) { best = dp[i - 1][k]; bi = k; }
                dp[i][j] = (bi < 0 || cs[i][j] > limit) ? 1e9 : best + Math.Abs(cs[i][j] - sxl[i]);
                bk[i][j] = bi;
            }
        double fb = 1e9; int fj = -1;
        for (int j = 0; j < cs[n - 1].Length; j++) if (dp[n - 1][j] < fb) { fb = dp[n - 1][j]; fj = j; }
        if (fj < 0 || fb >= 1e8) return null;
        var res = new double[n]; int cur = fj;
        for (int i = n - 1; i >= 0; i--) { res[i] = cs[i][cur]; cur = bk[i][cur]; }
        return res;
    };

    // one leg. S fixture connector (points down). T = tee point on the header/main at tgx. All runs horizontal and N-S (fixture is north of the line); dogleg = N-S, 45 deg diagonal, N-S final run L3.
    Action<string, FamilyInstance, double, double, Func<double, double[]>> legF = (lab, fx, pdia, tgx, findAt) =>
    {
        int madeStart = createdPipes.Count, fitStart = createdFits.Count;
        string rep = lab;
        try
        {
            Connector fc = pickSan(fx);
            if (fc == null) { manual.Add(lab + ": no sanitary connector on fixture " + fx.Id.Value + " - branch not drawn"); return; }
            XYZ S = fc.Origin; XYZ dirOut = fc.CoordinateSystem.BasisZ;
            if (Math.Abs(dirOut.Z) <= 0.9) { manual.Add(lab + ": connector not vertical at " + P3(S) + " (dir" + P3(dirOut) + ") - branch not drawn"); return; }
            if (dirOut.Z > 0) { manual.Add(lab + ": connector points up at " + P3(S) + " - branch not drawn"); return; }
            double[] h0 = findAt(tgx);
            if (h0 == null) { manual.Add(lab + ": no header/main pipe at tee point x=" + F(tgx) + " (needs 0.7 ft clear of pipe ends) - branch not drawn"); return; }
            XYZ T = new XYZ(tgx, h0[2], h0[3]);
            double lh = S.Y - T.Y;
            double d = tgx - S.X; bool dog = Math.Abs(d) > 0.05;
            if (lh < RUNMIN) { manual.Add(lab + ": fixture only " + F(lh) + " ft (plan) north of the line at y" + F(T.Y) + " (<" + F(RUNMIN) + ") - branch not drawn"); return; }
            double yB = T.Y + L3 + Math.Abs(d);
            if (dog)
            {
                if (Math.Abs(d) < DOGMIN) { manual.Add(lab + ": tee shift " + F(d) + " ft too small for a 45 deg diagonal >= " + F(HMIN) + " ft - branch not drawn"); return; }
                if (S.Y - yB < RUNMIN) { manual.Add(lab + ": fixture " + F(lh) + " ft from the line, a dogleg of " + F(d) + " ft needs " + F(3.0 + Math.Abs(d)) + " - branch not drawn"); return; }
            }
            double Lplan = dog ? (S.Y - yB) + Math.Abs(d) * SQ2 + L3 : lh;
            double zA = T.Z + SLOPE * Lplan;
            bool hasDrop = (S.Z - zA) >= DROPMIN;
            double g, zStart;
            if (hasDrop) { g = SLOPE; zStart = zA; }
            else
            {
                if (S.Z - T.Z < 0.9 * SLOPE * Lplan)
                {
                    manual.Add(lab + ": INSUFFICIENT FALL - connector Z " + F(S.Z) + " vs tee Z " + F(T.Z) + " over " + F(Lplan) + " ft plan run (needs " + F(SLOPE * Lplan) + " at 1/4 in/ft) - branch not drawn");
                    return;
                }
                g = (S.Z - T.Z) / Lplan; zStart = S.Z;
            }
            Func<double, double> zAt = (dist) => T.Z + g * dist;
            XYZ run0 = new XYZ(S.X, S.Y, zStart);
            var W = new List<XYZ>();
            XYZ uu;
            if (hasDrop) W.Add(S);
            W.Add(run0);
            if (dog)
            {
                XYZ Bp = new XYZ(S.X, yB, zAt(Math.Abs(d) * SQ2 + L3));
                XYZ Cp = new XYZ(tgx, T.Y + L3, zAt(L3));
                W.Add(Bp); W.Add(Cp);
            }
            W = dedupeW(W);
            uu = W[W.Count - 1];
            string lens = "";
            for (int k = 0; k + 1 < W.Count; k++) lens += F(W[k].DistanceTo(W[k + 1])) + " ";
            lens += F(uu.DistanceTo(T)) + "(tee run)";
            rep += " S" + P3(S) + " T" + P3(T) + " dX" + F(d) + " plan" + F(Lplan) + " zStart" + F(zStart) + " zTee" + F(T.Z) + " drop" + (hasDrop ? F(S.Z - zStart) : "none(run starts at connector)") + " slope" + F(g * 12.0) + "in/ft lengths[" + lens.Trim() + "]";
            Autodesk.Revit.DB.Plumbing.Pipe upP = null;
            if (W.Count >= 2)
            {
                var segPipes = mkPath(W, pdia);
                document.Regenerate();
                try
                {
                    Connector c0 = nearC(segPipes[0].Id.Value, S);
                    if (c0 == null) throw new Exception("no pipe end at fixture connector");
                    c0.ConnectTo(fc);
                }
                catch (Exception ex) { manual.Add(lab + ": fixture connector not joined to pipe " + segPipes[0].Id.Value + " at " + P3(S) + ": " + ex.Message); }
                for (int k = 1; k + 1 < W.Count; k++) joint(lab, segPipes[k - 1], segPipes[k], W[k]);
                document.Regenerate();
                upP = segPipes[segPipes.Count - 1];
            }
            lastStub[0] = 0;
            string te = tieIn(lab, upP, uu, pdia, findAt, tgx);
            if (W.Count < 2 && lastStub[0] != 0)
            {
                try
                {
                    document.Regenerate();
                    Connector cv = nearC(lastStub[0], S);
                    if (cv == null) throw new Exception("no pipe end at fixture connector");
                    cv.ConnectTo(fc);
                }
                catch (Exception ex) { manual.Add(lab + ": fixture connector not joined to run " + lastStub[0] + " at " + P3(S) + ": " + ex.Message); }
            }
            rep += " tee=" + (te == "" ? "OK perpendicular" : "FAILED");
            if (te != "") manual.Add(lab + ": tee not made at x" + F(tgx) + ": " + te);
        }
        catch (Exception ex) { manual.Add(lab + ": branch failed: " + ex.GetType().Name + " " + ex.Message); rep += " EXCEPTION " + ex.Message; }
        finally { branchOut.Add(rep + " pipes" + LJ(createdPipes.Skip(madeStart))); }
        addScope(lab + "/leg", createdPipes.Skip(madeStart).Concat(createdFits.Skip(fitStart)).Concat(new long[] { fx.Id.Value }));
    };

    // H6134: one straight header, y = Y of the reused tie, west -> east, ending exactly at the tie connector
    Action<string, int[]> headerTie = (hname, mem) =>
    {
        int madeStart = createdPipes.Count, fitStart = createdFits.Count;
        try
        {
            var order = mem.Where(m => pickSan(fixInstances[m]) != null).OrderBy(m => pickSan(fixInstances[m]).Origin.X).ToList();
            if (order.Count == 0) { manual.Add(hname + ": no fixture connectors - header not drawn"); return; }
            int n = order.Count;
            double maxSx = order.Max(m => pickSan(fixInstances[m]).Origin.X);
            object[] cand = null; double bestX = 1e9;
            foreach (var sc in stubs)
            {
                if (!(bool)sc[3] || (bool)sc[5]) continue;
                if (sc[4].ToString() == "Vent") continue;
                if (Math.Abs((double)sc[2] - HDIA) > 0.02) continue;
                XYZ so = (XYZ)sc[1]; XYZ bz = (XYZ)sc[6];
                XYZ bh = new XYZ(bz.X, bz.Y, 0); if (bh.GetLength() < 0.5) continue;
                bh = bh.Normalize();
                if (bh.X > -0.9) continue;                       // header runs +X: the tie connector must point -X
                if (so.Y < MAINY + 0.3 || so.Y > HY + 2.5) continue;
                if (so.X < maxSx + TEEGAP) continue;
                if (so.X < bestX) { bestX = so.X; cand = sc; }
            }
            if (cand == null) { manual.Add(hname + ": no freed tie facing -X east of x=" + F(maxSx) + " - header and legs not drawn"); return; }
            XYZ tt = (XYZ)cand[1];
            hdrY = tt.Y;
            var sxl = new List<double>(); var lhl = new List<double>();
            foreach (int m in order) { XYZ so0 = pickSan(fixInstances[m]).Origin; sxl.Add(so0.X); lhl.Add(so0.Y - hdrY); }
            double[] tee = planTees(sxl, lhl, tt.X - TEEGAP);
            if (tee == null) { manual.Add(hname + ": no tee X assignment keeps tees >= " + F(TEEGAP) + " ft apart and >= " + F(TEEGAP) + " ft from the tie at x" + F(tt.X) + " - header not drawn"); return; }
            double xw = tee[0] - TEEGAP;
            double zw = tt.Z + SLOPE * (tt.X - xw);
            var WH = new List<XYZ>(); WH.Add(new XYZ(xw, tt.Y, zw)); WH.Add(tt);
            var hp = mkPath(WH, HDIA);
            okOpen.Add(WH[0]);
            document.Regenerate();
            string tieNote = "reuse tie " + (long)cand[0] + " at " + P3(tt) + " dir" + P3((XYZ)cand[6]);
            string tr2 = tieReuse(hp[0], tt, cand);
            if (tr2 == "")
            {
                double angd = ((XYZ)cand[6]).AngleTo((tt - WH[0]).Normalize()) * 180.0 / Math.PI;
                notes.Add(hname + ": header pipe " + F(WH[0].DistanceTo(tt)) + " ft along the tie axis (" + F(angd) + " deg off BasisZ)");
            }
            else manual.Add(hname + ": header tie not made (" + tieNote + "): " + tr2);
            hdrOut.Add(hname + " y" + F(tt.Y) + " x" + F(xw) + ".." + F(tt.X) + " zW" + F(zw) + " zTie" + F(tt.Z) + " " + tieNote + " teeX " + string.Join("/", tee.Select(F)) + " pipes" + LJ(createdPipes.Skip(madeStart)));
            addScope(hname + "/hdr", createdPipes.Skip(madeStart).Concat(createdFits.Skip(fitStart)).Concat(new long[] { (long)cand[0] }));
            notes.Add(hname + ": west end open at " + P3(WH[0]) + " (cap/cleanout by hand)");
            for (int i = 0; i < n; i++) legF(fixLabels[order[i]], fixInstances[order[i]], fixDia[order[i]], tee[i], hdrAt);
        }
        catch (Exception ex) { manual.Add(hname + ": header failed: " + ex.GetType().Name + " " + ex.Message); }
    };
    // H6133: no header; each fixture runs N-S straight into its own perpendicular tee in the 4in main.
    // A tee X with no clear main pipe (0.7 ft from the pipe ends) is shifted (west first, then east, 1.1..4.0 ft) to the nearest clear spot >= TEEGAP from the neighbouring tees and from freed ties.
    Action<string, int[]> legsToMain = (hname, mem) =>
    {
        try
        {
            var order = mem.Where(m => pickSan(fixInstances[m]) != null).OrderBy(m => pickSan(fixInstances[m]).Origin.X).ToList();
            if (order.Count == 0) { manual.Add(hname + ": no fixture connectors - legs not drawn"); return; }
            int n = order.Count;
            var sxl = new List<double>(); var lhl = new List<double>();
            foreach (int m in order) { XYZ so0 = pickSan(fixInstances[m]).Origin; sxl.Add(so0.X); lhl.Add(so0.Y - MAINY); }
            double[] tee = planTees(sxl, lhl, 1e9);
            if (tee == null) { manual.Add(hname + ": no tee X assignment on the main - legs not drawn"); return; }
            var tieXs = new List<double>();
            foreach (var st1 in stubs) { XYZ so1 = (XYZ)st1[1]; if (so1.Y < MAINY + 0.3 || so1.Y > HY + 2.5) continue; tieXs.Add(so1.X); }
            for (int i = 0; i < n; i++)
            {
                if (mainAt(tee[i]) != null) continue;
                double lo = i > 0 ? tee[i - 1] + TEEGAP : -1e9, hi = i + 1 < n ? tee[i + 1] - TEEGAP : 1e9;
                double chosen = double.NaN;
                for (double dd = 1.1; dd <= 4.001 && double.IsNaN(chosen); dd += 0.1)
                {
                    if (lhl[i] < 3.0 + dd) break;
                    foreach (double sgn in new double[] { -1.0, 1.0 })
                    {
                        double xc = sxl[i] + sgn * dd;
                        if (xc < lo - 1e-6 || xc > hi + 1e-6) continue;
                        if (tieXs.Any(tx3 => Math.Abs(tx3 - xc) < TEEGAP)) continue;
                        if (mainAt(xc) == null) continue;
                        chosen = xc; break;
                    }
                }
                if (double.IsNaN(chosen)) { notes.Add(hname + ": " + fixLabels[order[i]] + ": no clear main spot within 4.0 ft of x" + F(sxl[i]) + " (>= " + F(TEEGAP) + " ft from neighbouring tees/ties, 0.7 ft from pipe ends)"); continue; }
                notes.Add(hname + ": " + fixLabels[order[i]] + " tee X moved " + F(tee[i]) + " -> " + F(chosen) + " (no clear main pipe at the first choice)");
                tee[i] = chosen;
            }
            hdrOut.Add(hname + " no header: legs tee directly into the 4in main at x " + string.Join("/", tee.Select(F)) + " (old tie 22829081 left as is - manual cap)");
            for (int i = 0; i < n; i++) legF(fixLabels[order[i]], fixInstances[order[i]], fixDia[order[i]], tee[i], mainAt);
        }
        catch (Exception ex) { manual.Add(hname + ": legs failed: " + ex.GetType().Name + " " + ex.Message); }
    };
    if (stB)
    {
        legsToMain("H6133", new int[] { 0, 1, 2 });
        headerTie("H6134", new int[] { 7, 4, 3, 5, 6 });
        document.Regenerate();
    }

    // E0. pre-commit self-check: HARD violators (roll back before commit) = pipe < HARDMIN ft or direction flipped; shorter than preferred (vertical < VMIN, horizontal < HMIN) = info only
    var viol = new List<string>(); var shortInfo = new List<string>(); double minLen = 1e9; int chk = 0;
    foreach (long pid in createdPipes)
    {
        var pp = E(pid) as Autodesk.Revit.DB.Plumbing.Pipe; if (pp == null) continue;
        Line pl = (pp.Location as LocationCurve) == null ? null : ((LocationCurve)pp.Location).Curve as Line;
        if (pl == null) { viol.Add(pid + " no line curve"); continue; }
        chk++;
        double ln0 = pl.Length; if (ln0 < minLen) minLen = ln0;
        XYZ dn = (pl.GetEndPoint(1) - pl.GetEndPoint(0)).Normalize();
        bool vert = Math.Abs(dn.Z) > 0.9;
        string seg = pid + (vert ? " vertical " : " horizontal ") + "length " + F(ln0) + " at " + P3(pl.GetEndPoint(0)) + "->" + P3(pl.GetEndPoint(1));
        if (ln0 < HARDMIN) viol.Add(seg + " < " + F(HARDMIN));
        else if (ln0 < (vert ? VMIN : HMIN) - 0.005) shortInfo.Add(seg + " (< preferred " + F(vert ? VMIN : HMIN) + ")");
        XYZ dw;
        if (drawnDir.TryGetValue(pid, out dw) && dn.DotProduct(dw) < 0.9) viol.Add(pid + " direction changed from " + P3(dw) + " to " + P3(dn) + " at " + P3(pl.GetEndPoint(0)));
    }
    notes.Add("selfCheck: " + chk + " new pipes, min length " + (chk == 0 ? "n/a" : F(minLen)) + " ft, hard violators " + viol.Count + ", shorter than preferred (info) " + shortInfo.Count);

    // E. readback: moved positions, open ends on new pipes, worksets
    var bad = new List<string>();
    for (int i = 0; i < NF; i++)
    {
        var lq = fFi[i].Location as LocationPoint;
        if (Math.Abs(lq.Point.X - fX[i]) > 0.1 || Math.Abs(lq.Point.Y - fY[i]) > 0.1) bad.Add(fLab[i] + " at " + P3(lq.Point));
    }
    if (bad.Count > 0) throw new Exception("readback of moved fixtures failed: " + string.Join(",", bad));
    var openEnds = new List<string>(); var wsMis = new List<string>();
    foreach (long pid in createdPipes)
    {
        Element pe = E(pid); if (pe == null) continue;
        if (pe.WorksetId.IntegerValue != mainWs) wsMis.Add(pid + ":ws" + pe.WorksetId.IntegerValue);
        if (knownOpen.Contains(pid)) continue;
        foreach (Connector c in conns(pe))
            if (c.ConnectorType != ConnectorType.Logical && !c.IsConnected && !okOpen.Any(ok0 => ok0.DistanceTo(c.Origin) < 0.1)) openEnds.Add(pid + "@" + P3(c.Origin));
    }
    foreach (long fid in createdFits) { Element fe = E(fid); if (fe != null && fe.WorksetId.IntegerValue != mainWs) wsMis.Add(fid + ":ws" + fe.WorksetId.IntegerValue); }
    foreach (string oe in openEnds) manual.Add("open end on new pipe " + oe);
    if (wsMis.Count > 0) manual.Add("workset != main(" + mainWs + ") on " + wsMis.Count + " new elements: " + string.Join(",", wsMis.Take(8)));
    int warnAfter = document.GetWarnings().Count;
    var mainAfter = mainPipes();
    var mainIdsAfter = new List<long>(); foreach (long mk1 in mainKnown) mainIdsAfter.Add(E(mk1) != null ? mk1 : -mk1);
    var sb = new System.Text.StringBuilder();
    sb.Append("{\"task\":" + Q(TASK) + ",\"stage\":" + Q(stageS) + ",\"mode\":" + Q(dry ? "DRYRUN(committed inside a TransactionGroup, then group rolled back: ids below are not live)" : "WRITE") + ",\"beach\":" + Q(F(BEACHZ)) + ",\"pipeType\":" + Q(pipeT.Name) + ",\"sysType\":" + Q(sysTypeEl.Name) + ",\"mainWs\":" + mainWs + ",\"hostWs\":" + hostWs);
    sb.Append(",\"selfCheck\":{\"pipes\":" + chk + ",\"minLen\":" + Q(chk == 0 ? "n/a" : F(minLen)) + ",\"hardViolators\":[" + string.Join(",", viol.Take(30).Select(Q)) + "],\"hardViolatorCount\":" + viol.Count + ",\"info\":[" + string.Join(",", shortInfo.Take(40).Select(Q)) + "],\"infoCount\":" + shortInfo.Count + "}");
    sb.Append(",\"moved\":[" + string.Join(",", movedOut.Select(Q)) + "]");
    sb.Append(",\"newWC\":" + newWcId);
    sb.Append(",\"newPipes\":" + LJ(createdPipes) + ",\"newFittings\":" + LJ(createdFits));
    sb.Append(",\"deleted\":" + LJ(deleted) + ",\"absentIds\":" + LJ(absent) + ",\"keptNotDeleted\":[" + string.Join(",", keepD.Select(Q)) + "],\"deleteFailed\":[" + string.Join(",", delFailed.Take(10).Select(Q)) + "],\"cascadeDeleted\":" + LJ(cascade.Take(30)));
    sb.Append(",\"headers\":[" + string.Join(",", hdrOut.Select(Q)) + "]");
    sb.Append(",\"legs\":[" + string.Join(",", branchOut.Select(Q)) + "]");
    sb.Append(",\"joinNotes\":[" + string.Join(",", notes.Take(50).Select(Q)) + "]");
    sb.Append(",\"freedStubs\":[" + string.Join(",", stubsOut.Take(25).Select(Q)) + "]");
    sb.Append(",\"disconnected\":[" + string.Join(",", discOut.Take(15).Select(Q)) + "]");
    sb.Append(",\"reconnectManual\":[" + string.Join(",", reconnect.Select(Q)) + "],\"reconnectCount\":" + reconnect.Count);
    sb.Append(",\"manual\":[" + string.Join(",", manual.Take(40).Select(Q)) + "],\"manualCount\":" + manual.Count);
    sb.Append(",\"mainPipesStillPresent\":" + LJ(mainIdsAfter) + ",\"mainPipeCount\":" + mainAfter.Count);
    sb.Append(",\"warnings\":{\"before\":" + warnBefore + ",\"after\":" + warnAfter + "}");
    sb.Append(",\"connectorReport\":[" + string.Join(",", connRep.Select(Q)) + "]");
    sb.Append(",\"arch\":[" + string.Join(",", info.Select(Q)) + "]}");
    int done = movedOut.Count + deleted.Count + createdPipes.Count + createdFits.Count;
    string tail = "\ndone=" + done + " skipped=" + (absent.Count + keepD.Count) + " failed=" + (manual.Count + delFailed.Count + viol.Count);

    if (viol.Count > 0)
    {
        try { if (tx.GetStatus() == TransactionStatus.Started) tx.RollBack(); } catch (Exception) { }
        try { if (grp.GetStatus() == TransactionStatus.Started) grp.RollBack(); } catch (Exception) { }
        return "ROLLED_BACK " + TASK + ": hard selfCheck violators=" + viol.Count + " (length < " + F(HARDMIN) + " or direction flipped; no commit attempted)\n" + sb.ToString() + tail;
    }

    // commit INSIDE the group in both modes, so failures are processed (and captured) now
    TransactionStatus ts = tx.Commit();
    string farr = failArr();
    if (ts != TransactionStatus.Committed)
    {
        try { if (grp.GetStatus() == TransactionStatus.Started) grp.RollBack(); } catch (Exception) { }
        sb.Insert(1, "\"commit\":" + Q(ts.ToString()) + ",\"failures\":" + farr + ",\"failureTotal\":" + failTotal[0] + ",");
        return "ROLLED_BACK " + TASK + " stage " + stageS + ": commit status " + ts + " failureTotal=" + failTotal[0] + "\n" + sb.ToString() + tail;
    }
    sb.Insert(1, "\"commit\":" + Q(ts.ToString()) + ",\"failures\":" + farr + ",\"failureTotal\":" + failTotal[0] + ",");
    if (dry) { grp.RollBack(); return sb.ToString() + tail; }
    TransactionStatus gs = grp.Assimilate();
    if (gs != TransactionStatus.Committed) return "ROLLED_BACK " + TASK + ": group assimilate status " + gs + " failureTotal=" + failTotal[0] + " failures=" + farr + "\ndone=0 skipped=0 failed=1";
    return sb.ToString() + tail;
}
catch (Exception ex)
{
    try { if (tx.GetStatus() == TransactionStatus.Started) tx.RollBack(); } catch (Exception) { }
    try { if (grp.GetStatus() == TransactionStatus.Started) grp.RollBack(); } catch (Exception) { }
    return "ROLLED_BACK " + TASK + ": " + ex.GetType().Name + " " + ex.Message + " failureTotal=" + failTotal[0] + " failures=" + failArr() + " manual=" + string.Join(" ; ", manual.Take(10).Select(s => s.Replace("\n", " "))) + " legs=" + string.Join(" ; ", branchOut.Take(10)) + "\ndone=0 skipped=0 failed=1";
}
finally
{
    try { document.Application.FailuresProcessing -= fh; } catch (Exception) { }
}
