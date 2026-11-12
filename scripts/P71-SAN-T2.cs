// operation: fixture-level-fix + tag-move + pipe-cap (follow-up to P71-SAN-T1) | P71-SAN-T2 | 24-071 Pacific Edge, PLMB-24220-R25 | Surf BEACH
// parameters[0] = "dryrun" (default) | "write".  transactionMode "none".
// (1) WC 22917568: move its level association B2 -> SURF-BEACH (13180210) keeping world Z of location and every connector, connectors stay connected. Routes tried in order (each in a SubTransaction, verified):
//     FAMILY_LEVEL_PARAM, INSTANCE_REFERENCE_LEVEL_PARAM, INSTANCE_SCHEDULE_ONLY_LEVEL_PARAM; then Z restored by an offset param (INSTANCE_ELEVATION_PARAM / INSTANCE_FREE_HOST_OFFSET_PARAM / INSTANCE_SILL_HEIGHT_PARAM) else MoveElement.
// (2) tags in view 18215302 of the moved fixtures + new WC: head moved to 2.0 ft from the fixture (toward FacingOrientation = room interior, else +Y), leader state unchanged.
// (3) cap the open west end of pipe 22918236 with PlumbingUtils.PlaceCapOnOpenEnds (pipe type routing preference cap).
// Failure capture + TransactionGroup dryrun pattern as P71-SAN-T1.v6: commit inside the group, dryrun then rolls the group back.
const string TASK = "P71-SAN-T2";
bool dry = !(parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString().Trim().ToLowerInvariant() == "write");
Func<string, string> fail = (m) => "{\"task\":\"" + TASK + "\",\"status\":\"FAILED\",\"error\":\"" + m.Replace("\\", "/").Replace("\"", "'").Replace("\r", " ").Replace("\n", " ") + "\",\"failures\":[]}\nFAILED " + TASK + ": " + m.Replace("\r", " ").Replace("\n", " ") + "\ndone=0 skipped=0 failed=1";
if (document.Title.IndexOf("PLMB-24220-R25", StringComparison.OrdinalIgnoreCase) < 0) return fail("wrong document open: " + document.Title);
var inv = System.Globalization.CultureInfo.InvariantCulture;
Func<double, string> F = (d) => d.ToString("F2", inv);
Func<XYZ, string> P3 = (p) => p == null ? "null" : "(" + F(p.X) + "," + F(p.Y) + "," + F(p.Z) + ")";
Func<string, string> Q = (s) => "\"" + (s == null ? "" : s.Replace("\\", "/").Replace("\"", "'").Replace("\r", " ").Replace("\n", " ")) + "\"";
Func<long, Element> E = (id) => document.GetElement(new ElementId(id));
Func<IEnumerable<long>, string> LJ = (ids) => "[" + string.Join(",", ids.Select(x => x.ToString())) + "]";

const long WCID = 22917568, BEACHID = 13180210, VIEWID = 18215302, PIPEID = 22918236;
long[] movedFix = new long[] { 22810384, 22818820, 22828284, 22819280, 22829763, 22832174, 22828726 };
const double TAGOFF = 2.0, ZTOL = 0.01;
XYZ capPt = new XYZ(230.70, 171.49, 130.14);

// ---------- failure capture ----------
var capSev = new List<string>(); var capTxt = new List<string>(); var capIds = new List<List<long>>();
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
Func<string> failArr = () =>
{
    var seenF = new HashSet<string>(); var outl = new List<string>();
    for (int q = 0; q < capSev.Count; q++)
    {
        string key = capSev[q] + "|" + capTxt[q] + "|" + string.Join(",", capIds[q]);
        if (!seenF.Add(key) || outl.Count >= 10) continue;
        string tx0 = capTxt[q] == null ? "" : capTxt[q]; if (tx0.Length > 150) tx0 = tx0.Substring(0, 150);
        outl.Add("{\"sev\":" + Q(capSev[q]) + ",\"text\":" + Q(tx0) + ",\"ids\":" + LJ(capIds[q].Take(10)) + "}");
    }
    return "[" + string.Join(",", outl) + "]";
};

// ---------- connectors ----------
Func<Element, List<Connector>> conns = (el) =>
{
    var l = new List<Connector>();
    ConnectorManager cm = null;
    var fiq = el as FamilyInstance;
    if (fiq != null) { if (fiq.MEPModel != null) cm = fiq.MEPModel.ConnectorManager; }
    else { var mc = el as MEPCurve; if (mc != null) cm = mc.ConnectorManager; }
    if (cm != null) foreach (Connector c in cm.Connectors) if (c.ConnectorType != ConnectorType.Logical) l.Add(c);
    return l;
};

// ---------- lookups ----------
var wc = E(WCID) as FamilyInstance;
if (wc == null) return fail("WC " + WCID + " not found or not a family instance");
var wcLp = wc.Location as LocationPoint;
if (wcLp == null) return fail("WC " + WCID + " has no point location");
Level beach = E(BEACHID) as Level;
if (beach == null) return fail("level " + BEACHID + " not found");
if (beach.Name.ToUpperInvariant().IndexOf("BEACH", StringComparison.Ordinal) < 0) return fail("level " + BEACHID + " is '" + beach.Name + "', expected SURF - BEACH LEVEL");
var view = E(VIEWID) as View;
if (view == null) return fail("view " + VIEWID + " not found");
var pipe = E(PIPEID) as Autodesk.Revit.DB.Plumbing.Pipe;
if (pipe == null) return fail("pipe " + PIPEID + " not found");
Level wcLevelBefore = E(wc.LevelId.Value) as Level;
string wcLevelNameBefore = wcLevelBefore == null ? "null" : wcLevelBefore.Name;
var fixEls = new List<FamilyInstance>();
foreach (long fid in movedFix)
{
    var fe = E(fid) as FamilyInstance;
    if (fe == null) return fail("moved fixture " + fid + " not found");
    if (!(fe.Location is LocationPoint)) return fail("moved fixture " + fid + " has no point location");
    fixEls.Add(fe);
}
fixEls.Add(wc);

// tags in the view
var tagList = new List<IndependentTag>();
foreach (IndependentTag t in new FilteredElementCollector(document, view.Id).OfClass(typeof(IndependentTag)).Cast<IndependentTag>()) tagList.Add(t);
var fixSet = new HashSet<long>(movedFix); fixSet.Add(WCID);
var tagPlan = new List<object[]>(); // tag, fixture
var fixHasTag = new HashSet<long>();
foreach (IndependentTag t in tagList)
{
    foreach (ElementId tid in t.GetTaggedLocalElementIds())
        if (fixSet.Contains(tid.Value)) { tagPlan.Add(new object[] { t, E(tid.Value) }); fixHasTag.Add(tid.Value); }
}
var noTag = new List<long>(); foreach (long fid in fixSet) if (!fixHasTag.Contains(fid)) noTag.Add(fid);

// ---------- checkout ----------
var badOwn = new List<string>();
if (document.IsWorkshared)
{
    var chk = new HashSet<long>(fixSet); chk.Add(PIPEID); chk.Add(BEACHID);
    foreach (var tp in tagPlan) chk.Add(((IndependentTag)tp[0]).Id.Value);
    foreach (var c0 in conns(wc)) foreach (Connector r in c0.AllRefs) if (r.Owner != null && !(r.Owner is MEPSystem)) chk.Add(r.Owner.Id.Value);
    foreach (long cid in chk)
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
if (badOwn.Count > 0) return "{\"task\":\"" + TASK + "\",\"status\":\"FAILED\",\"error\":\"borrowed by other users\",\"owners\":[" + string.Join(",", badOwn.Take(20).Select(Q)) + "],\"failures\":[]}\nFAILED " + TASK + ": elements borrowed by other users: " + string.Join("; ", badOwn.Take(20)) + "\ndone=0 skipped=0 failed=" + badOwn.Count;

// ---------- baseline readbacks ----------
var cBefore = conns(wc);
var cBeforeZ = new List<double>(); var cBeforeConn = new List<bool>(); var cBeforeOrg = new List<XYZ>();
foreach (var c0 in cBefore) { cBeforeZ.Add(c0.Origin.Z); cBeforeConn.Add(c0.IsConnected); cBeforeOrg.Add(c0.Origin); }
double zBefore = wcLp.Point.Z; XYZ locBefore = wcLp.Point;
bool alreadyBeach = wc.LevelId.Value == BEACHID;
// parameter report
BuiltInParameter[] bps = new BuiltInParameter[] { BuiltInParameter.FAMILY_LEVEL_PARAM, BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM, BuiltInParameter.INSTANCE_SCHEDULE_ONLY_LEVEL_PARAM, BuiltInParameter.INSTANCE_ELEVATION_PARAM, BuiltInParameter.INSTANCE_FREE_HOST_OFFSET_PARAM, BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM };
var parRep = new List<string>();
foreach (var bp in bps)
{
    Parameter pp = wc.get_Parameter(bp);
    parRep.Add(bp.ToString() + (pp == null ? " absent" : (pp.IsReadOnly ? " readonly " : " writable ") + (pp.StorageType == StorageType.ElementId ? "id" + pp.AsElementId().Value : (pp.StorageType == StorageType.Double ? F(pp.AsDouble()) : pp.StorageType.ToString()))));
}
string hostDesc = wc.Host == null ? "none" : wc.Host.GetType().Name + wc.Host.Id.Value;
string wcSym = wc.Symbol.FamilyName + " : " + wc.Symbol.Name;
var wcConnRep = new List<string>();
for (int i = 0; i < cBefore.Count; i++) wcConnRep.Add(cBefore[i].PipeSystemType + "@" + P3(cBefore[i].Origin) + (cBefore[i].IsConnected ? " conn" : " free"));

// pipe cap pre-check
var openBefore = new List<Connector>();
foreach (var pc in conns(pipe)) if (!pc.IsConnected) openBefore.Add(pc);
bool capNeeded = openBefore.Count > 0;
string capSkip = "";
if (!capNeeded) capSkip = "pipe " + PIPEID + " has no open connector (already capped/connected)";
else if (openBefore.Count != 1 || openBefore[0].Origin.DistanceTo(capPt) > 0.5)
{
    capSkip = "pipe " + PIPEID + " open connectors: " + string.Join("; ", openBefore.Select(c => P3(c.Origin))) + " - not exactly the west end " + P3(capPt) + ": cap NOT placed (manual)";
    capNeeded = false;
}
ElementId pipeTypeId = pipe.GetTypeId();
int warnBefore = document.GetWarnings().Count;

// ---------- everything in ONE group / ONE transaction ----------
var failed = new List<string>(); var manual = new List<string>();
var routeLog = new List<string>(); var tagOut = new List<string>(); var capIdsNew = new List<long>();
string routeUsed = alreadyBeach ? "already on beach" : "";
double zAfter = zBefore; string lvlAfter = wcLevelNameBefore; var wcAfterConn = new List<string>();
int done = 0, skipped = 0;
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

    // ===== 1. level association =====
    Func<bool> zOk = () => { var l = wc.Location as LocationPoint; return l != null && Math.Abs(l.Point.Z - zBefore) <= ZTOL; };
    if (alreadyBeach) skipped++;
    else
    {
        BuiltInParameter[] routes = new BuiltInParameter[] { BuiltInParameter.FAMILY_LEVEL_PARAM, BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM, BuiltInParameter.INSTANCE_SCHEDULE_ONLY_LEVEL_PARAM };
        foreach (var rt in routes)
        {
            Parameter rp = wc.get_Parameter(rt);
            if (rp == null) { routeLog.Add(rt + ": absent"); continue; }
            if (rp.IsReadOnly) { routeLog.Add(rt + ": readonly"); continue; }
            if (rp.StorageType != StorageType.ElementId) { routeLog.Add(rt + ": not ElementId"); continue; }
            var sub = new SubTransaction(document);
            try
            {
                sub.Start();
                bool ok = rp.Set(beach.Id);
                document.Regenerate();
                if (!ok || wc.LevelId.Value != BEACHID) { routeLog.Add(rt + ": set=" + ok + " but LevelId=" + wc.LevelId.Value); sub.RollBack(); continue; }
                sub.Commit(); routeUsed = rt.ToString(); routeLog.Add(rt + ": LevelId changed");
                break;
            }
            catch (Exception exr)
            {
                try { if (sub.GetStatus() == TransactionStatus.Started) sub.RollBack(); } catch (Exception) { }
                routeLog.Add(rt + ": " + exr.GetType().Name + " " + exr.Message);
            }
        }
        if (routeUsed == "")
            throw new Exception("FAILED level change: no writable route moved LevelId to " + BEACHID + ": " + string.Join(" | ", routeLog) + " | params: " + string.Join("; ", parRep));
        // restore Z
        if (!zOk())
        {
            double dz = zBefore - ((LocationPoint)wc.Location).Point.Z;
            routeLog.Add("Z moved by level change: dz=" + F(dz));
            bool fixedZ = false;
            BuiltInParameter[] offs = new BuiltInParameter[] { BuiltInParameter.INSTANCE_ELEVATION_PARAM, BuiltInParameter.INSTANCE_FREE_HOST_OFFSET_PARAM, BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM };
            foreach (var ob in offs)
            {
                Parameter op = wc.get_Parameter(ob);
                if (op == null || op.IsReadOnly || op.StorageType != StorageType.Double) continue;
                double old = op.AsDouble();
                var sub2 = new SubTransaction(document);
                try
                {
                    sub2.Start();
                    op.Set(old + dz); document.Regenerate();
                    if (zOk()) { sub2.Commit(); fixedZ = true; routeLog.Add("Z restored with " + ob + " " + F(old) + "->" + F(old + dz)); break; }
                    sub2.RollBack();
                }
                catch (Exception exo) { try { if (sub2.GetStatus() == TransactionStatus.Started) sub2.RollBack(); } catch (Exception) { } routeLog.Add(ob + ": " + exo.GetType().Name + " " + exo.Message); }
            }
            if (!fixedZ)
            {
                var sub3 = new SubTransaction(document);
                try
                {
                    sub3.Start();
                    ElementTransformUtils.MoveElement(document, wc.Id, new XYZ(0, 0, dz)); document.Regenerate();
                    if (zOk()) { sub3.Commit(); fixedZ = true; routeLog.Add("Z restored with MoveElement dz=" + F(dz)); } else sub3.RollBack();
                }
                catch (Exception exm) { try { if (sub3.GetStatus() == TransactionStatus.Started) sub3.RollBack(); } catch (Exception) { } routeLog.Add("MoveElement: " + exm.GetType().Name + " " + exm.Message); }
            }
            if (!fixedZ) throw new Exception("FAILED Z restore: location Z " + F(((LocationPoint)wc.Location).Point.Z) + " vs " + F(zBefore) + " | " + string.Join(" | ", routeLog));
        }
        done++;
    }
    document.Regenerate();
    var lpA = wc.Location as LocationPoint; zAfter = lpA.Point.Z;
    Level lvA = E(wc.LevelId.Value) as Level; lvlAfter = lvA == null ? "null" : lvA.Name;
    if (wc.LevelId.Value != BEACHID) throw new Exception("FAILED: level after = " + lvlAfter + " (" + wc.LevelId.Value + ")");
    if (Math.Abs(zAfter - zBefore) > ZTOL) throw new Exception("FAILED: location Z " + F(zBefore) + " -> " + F(zAfter));
    var cAfter = conns(wc);
    if (cAfter.Count != cBefore.Count) throw new Exception("FAILED: connector count " + cBefore.Count + " -> " + cAfter.Count);
    for (int i = 0; i < cAfter.Count; i++)
    {
        wcAfterConn.Add(cAfter[i].PipeSystemType + "@" + P3(cAfter[i].Origin) + (cAfter[i].IsConnected ? " conn" : " free"));
        if (Math.Abs(cAfter[i].Origin.Z - cBeforeZ[i]) > ZTOL) throw new Exception("FAILED: connector " + i + " Z " + F(cBeforeZ[i]) + " -> " + F(cAfter[i].Origin.Z));
        if (cBeforeConn[i] && !cAfter[i].IsConnected) throw new Exception("FAILED: connector " + i + " at " + P3(cBeforeOrg[i]) + " disconnected");
    }

    // ===== 2. tags =====
    foreach (var tp in tagPlan)
    {
        var tag = (IndependentTag)tp[0]; var fe = tp[1] as FamilyInstance;
        try
        {
            var lp = fe.Location as LocationPoint; XYZ fl = lp.Point;
            XYZ oldHead = tag.TagHeadPosition;
            XYZ dir = fe.FacingOrientation; XYZ d2 = dir == null ? XYZ.Zero : new XYZ(dir.X, dir.Y, 0);
            string how = "facing";
            if (d2.GetLength() < 0.1) { d2 = new XYZ(0, 1, 0); how = "+Y(no facing)"; }
            d2 = d2.Normalize();
            XYZ nh = new XYZ(fl.X + d2.X * TAGOFF, fl.Y + d2.Y * TAGOFF, oldHead.Z);
            bool leader = tag.HasLeader;
            tag.TagHeadPosition = nh;
            tagOut.Add("{\"tag\":" + tag.Id.Value + ",\"tagged\":" + fe.Id.Value + ",\"text\":" + Q(tag.TagText) + ",\"old\":" + Q(P3(oldHead)) + ",\"new\":" + Q(P3(nh)) + ",\"fixture\":" + Q(P3(fl)) + ",\"dir\":" + Q(how) + ",\"leader\":" + leader.ToString().ToLowerInvariant() + "}");
            done++;
        }
        catch (Exception ext) { failed.Add("tag " + tag.Id.Value + ": " + ext.GetType().Name + " " + ext.Message); }
    }
    document.Regenerate();
    // readback distances
    var tagDist = new List<string>();
    foreach (var tp in tagPlan)
    {
        var tag = (IndependentTag)tp[0]; var fe = (FamilyInstance)tp[1];
        XYZ h = tag.TagHeadPosition; XYZ fl = ((LocationPoint)fe.Location).Point;
        double dd = Math.Sqrt((h.X - fl.X) * (h.X - fl.X) + (h.Y - fl.Y) * (h.Y - fl.Y));
        tagDist.Add(tag.Id.Value + ":" + F(dd));
        if (dd > 3.0) failed.Add("tag " + tag.Id.Value + " head is " + F(dd) + " ft from fixture " + fe.Id.Value + " after move (>3)");
    }
    foreach (long nt in noTag) manual.Add("no tag in view " + VIEWID + " for fixture " + nt + " (not created)");

    // ===== 3. cap =====
    if (capNeeded)
    {
        var sub4 = new SubTransaction(document);
        try
        {
            sub4.Start();
            Autodesk.Revit.DB.Plumbing.PlumbingUtils.PlaceCapOnOpenEnds(document, pipe.Id, pipeTypeId);
            document.Regenerate();
            foreach (var pc in conns(pipe))
                if (pc.Origin.DistanceTo(capPt) < 0.5)
                    foreach (Connector r in pc.AllRefs)
                        if (r.ConnectorType != ConnectorType.Logical && r.Owner != null && !(r.Owner is MEPSystem) && r.Owner.Id.Value != pipe.Id.Value) capIdsNew.Add(r.Owner.Id.Value);
            bool stillOpen = conns(pipe).Any(c => !c.IsConnected);
            if (stillOpen || capIdsNew.Count == 0) { sub4.RollBack(); manual.Add("cap: PlaceCapOnOpenEnds ran but pipe still open or no cap found (stillOpen=" + stillOpen + "): cap manual"); capIdsNew.Clear(); }
            else { sub4.Commit(); done++; }
        }
        catch (Exception exc)
        {
            try { if (sub4.GetStatus() == TransactionStatus.Started) sub4.RollBack(); } catch (Exception) { }
            capIdsNew.Clear();
            manual.Add("cap: PlaceCapOnOpenEnds failed: " + exc.GetType().Name + " " + exc.Message + " (pipe type " + (document.GetElement(pipeTypeId) == null ? "null" : document.GetElement(pipeTypeId).Name) + ") - cap manual");
        }
    }
    else { skipped++; if (capSkip != "" && capSkip.IndexOf("manual", StringComparison.Ordinal) >= 0) manual.Add(capSkip); }

    int warnTmp = document.GetWarnings().Count;
    string capState = conns(pipe).Any(c => !c.IsConnected) ? "open" : "closed";
    var sb = new System.Text.StringBuilder();
    sb.Append("{\"task\":" + Q(TASK) + ",\"mode\":" + Q(dry ? "DRYRUN(committed inside a TransactionGroup, then rolled back)" : "WRITE"));
    sb.Append(",\"wc\":{\"id\":" + WCID + ",\"symbol\":" + Q(wcSym) + ",\"host\":" + Q(hostDesc) + ",\"levelBefore\":" + Q(wcLevelNameBefore) + ",\"levelAfter\":" + Q(lvlAfter) + ",\"levelIdAfter\":" + wc.LevelId.Value + ",\"zBefore\":" + Q(F(zBefore)) + ",\"zAfter\":" + Q(F(zAfter)) + ",\"locBefore\":" + Q(P3(locBefore)) + ",\"route\":" + Q(routeUsed) + ",\"routeLog\":[" + string.Join(",", routeLog.Select(Q)) + "],\"params\":[" + string.Join(",", parRep.Select(Q)) + "],\"connBefore\":[" + string.Join(",", wcConnRep.Select(Q)) + "],\"connAfter\":[" + string.Join(",", wcAfterConn.Select(Q)) + "]}");
    sb.Append(",\"tags\":[" + string.Join(",", tagOut) + "],\"tagDist\":" + Q(string.Join(" ", tagDist)) + ",\"fixturesWithoutTag\":" + LJ(noTag));
    sb.Append(",\"cap\":{\"pipe\":" + PIPEID + ",\"placed\":" + LJ(capIdsNew) + ",\"state\":" + Q(capState) + ",\"skip\":" + Q(capSkip) + "}");
    sb.Append(",\"manual\":[" + string.Join(",", manual.Select(Q)) + "],\"warnings\":{\"before\":" + warnBefore + ",\"afterInGroup\":" + warnTmp + "}");
    string tail = "\ndone=" + done + " skipped=" + skipped + " failed=" + failed.Count;
    if (failed.Count > 0)
    {
        try { if (tx.GetStatus() == TransactionStatus.Started) tx.RollBack(); } catch (Exception) { }
        try { if (grp.GetStatus() == TransactionStatus.Started) grp.RollBack(); } catch (Exception) { }
        return sb.ToString() + ",\"status\":\"ROLLED_BACK\",\"failed\":[" + string.Join(",", failed.Select(Q)) + "],\"failures\":" + failArr() + "}" + tail;
    }

    TransactionStatus ts = tx.Commit();
    string farr = failArr();
    if (ts != TransactionStatus.Committed)
    {
        try { if (grp.GetStatus() == TransactionStatus.Started) grp.RollBack(); } catch (Exception) { }
        return sb.ToString() + ",\"status\":\"ROLLED_BACK\",\"commit\":" + Q(ts.ToString()) + ",\"failures\":" + farr + "}\nROLLED_BACK " + TASK + ": commit status " + ts + "\ndone=0 skipped=0 failed=1";
    }
    int warnAfter = document.GetWarnings().Count;
    if (dry)
    {
        grp.RollBack();
        return sb.ToString() + ",\"status\":\"DRYRUN_OK\",\"commit\":\"Committed\",\"warningsAfterCommit\":" + warnAfter + ",\"failures\":" + farr + "}" + tail;
    }
    TransactionStatus gs = grp.Assimilate();
    if (gs != TransactionStatus.Committed) return "{\"task\":\"" + TASK + "\",\"status\":\"ROLLED_BACK\",\"error\":\"group assimilate " + gs + "\",\"failures\":" + farr + "}\nROLLED_BACK " + TASK + ": group assimilate status " + gs + "\ndone=0 skipped=0 failed=1";
    return sb.ToString() + ",\"status\":\"OK\",\"warningsAfterCommit\":" + warnAfter + ",\"capIds\":" + LJ(capIdsNew) + ",\"failures\":" + farr + "}" + tail;
}
catch (Exception ex)
{
    try { if (tx.GetStatus() == TransactionStatus.Started) tx.RollBack(); } catch (Exception) { }
    try { if (grp.GetStatus() == TransactionStatus.Started) grp.RollBack(); } catch (Exception) { }
    return "{\"task\":\"" + TASK + "\",\"status\":\"ROLLED_BACK\",\"error\":" + Q(ex.GetType().Name + " " + ex.Message) + ",\"routeLog\":[" + string.Join(",", routeLog.Select(Q)) + "],\"params\":[" + string.Join(",", parRep.Select(Q)) + "],\"failures\":" + failArr() + "}\nROLLED_BACK " + TASK + ": " + ex.Message.Replace("\n", " ") + "\ndone=0 skipped=0 failed=1";
}
finally
{
    try { document.Application.FailuresProcessing -= fh; } catch (Exception) { }
}
