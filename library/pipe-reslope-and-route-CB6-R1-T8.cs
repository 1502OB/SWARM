// pipe-reslope-and-route: from CB6-R1-T8.v2 (23-073), verified
// CB6-R1-T8 | operation: route-pipe-connect-systems (+ re-slope GSAN 9 horizontals, revision clouds) | run 20261109-1412 | Revit 2025 | v2 PILOT
// v2 (debug cycle 1): v1 rolled back at "reslope check: node 14609075 at z 19.329, expected 19.832". v1 moved the fitting groups
// one after another with MoveElements while the horizontals were still connected; a 90 deg fitting cannot tilt a joined
// horizontal, so Revit translated the pipe and dragged the next fitting, and the root took every later move as well
// (-0.318 own + -0.23 - 0.20 - 0.073 = -0.821, the observed drop). v2 re-slopes in four separate steps:
// disconnect -> move fittings -> set pipe ends -> reconnect, and checks every node after each step.
// Body of: public static object Execute(Document document, object[] parameters)
// send_code_to_revit, transactionMode "auto" (the bridge's single transaction wraps everything; the work runs in ONE
// SubTransaction so the script rolls back its own changes and returns "ROLLED_BACK ..."). Never saves or syncs.
// Only System, System.Linq, System.Collections.Generic and Autodesk.Revit.DB are auto-imported: Plumbing types are fully qualified.
// parameters (tokens, any order):
//   "dryrun"     read-only preflight: resolves pipe type / system type / level / revision / tag type / views by name, walks the
//                GSAN 9 horizontal tree from the top of 14609072, plans the 1/4"/ft re-slope (node moves, old -> new slope per pipe),
//                plans the route (every pipe start/end/diameter/slope/inverts, every elbow), lists every element to modify
//                (deletes: none), runs the clash check (pipes + ducts within 0.5 ft, SAN 13369507 centre-to-centre), checks
//                both views (view range, workset, crop, sheet) and plans the clouds (R25 tag head). Returns before any change.
//   "noreslope"  skip Jeremy's QB2-9 re-slope (route only). Default: re-slope ON.
//   "allowclash" do not stop on a hard pipe/duct overlap (default: NO_GO / rollback on overlap).
//   (none)       real run: re-slope, extend 14609072 bottom, shorten stub 14187965 top (endpoint edit, not a delete), 2 new 4" pipes,
//                3 elbows from the routing preferences, read-back after Regenerate (R30), clash check, clouds + delta tags; any
//                failed check rolls back the whole SubTransaction.
// Re-slope method (v2): (1) record and Connector.DisconnectFrom both end connections of every GSAN 9 horizontal in the tree;
// (2) move each fitting group ONCE by (target z - CURRENT z) after Regenerate, in tree order, and check after each move that
// every placed node is at its target, every unplaced node and every horizontal is unmoved (tol 0.01 ft); (3) set each
// horizontal's LocationCurve to its old ends + the dz of its two nodes (1/4"/ft by construction); (4) ConnectTo the same
// fitting connectors again (gap <= 0.01 ft) and re-check every node, slope and neighbour set. Nothing is raised; verticals stretch.
// The open bottom of 14609072 is held at its live position (put back if the root move dragged it).
// Cleanup cannot undo the re-slope (restore point: SYNC A version); the run returns 'reslope_old_z' for reference.
var inv = System.Globalization.CultureInfo.InvariantCulture;
bool dry = false, noRes = false, allowClash = false;
if (parameters != null)
    foreach (object o in parameters)
    {
        if (o == null) continue;
        string tk = o.ToString().Trim().ToLowerInvariant();
        if (tk == "dryrun") dry = true; else if (tk == "noreslope") noRes = true; else if (tk == "allowclash") allowClash = true;
    }
const long V_ID = 14609072, ST_ID = 14187965, ST_FIT = 14197329, SAN_ID = 13369507, SYS9 = 14608926, SYS6 = 14196807, REV_ID = 15034302;
long[] FS_IDS = new long[] { 14608919, 15033327 };
long[] HZ_IDS = new long[] { 15033614, 15033598 };
long[] VIEW_IDS = new long[] { 12968576, 14243757 };
string[] SHEET_EXP = new string[] { "P2.20", "P4.90" };
string PIPE_TYPE = "Gravity Flow - Long Sweep";
string SYS_TYPE = "22 wst__GSAN - SANITARY DRAINAGE - GREASE LADEN";
string REVDESC = "FEWD Submittal";
double SL = 0.25 / 12.0, DROP1 = 0.36, DIA = 4.0 / 12.0, NTOL = 0.01;
double BX = 18.73, BY = -259.40, BZ = 18.81, TX = 21.03, TY = -233.94, TZ = 19.18;
List<string> warn = new List<string>();
System.Text.StringBuilder sb = new System.Text.StringBuilder();
sb.Append(dry ? "DRYRUN" : "RUN").Append(" CB6-R1-T8 v2").Append(noRes ? " noreslope" : "").Append(allowClash ? " allowclash" : "");

// ---- 0. document
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return T8NoGo("wrong document open: " + title);

// ---- 1. the two pipes we join
Autodesk.Revit.DB.Plumbing.Pipe pv = document.GetElement(new ElementId(V_ID)) as Autodesk.Revit.DB.Plumbing.Pipe;
Autodesk.Revit.DB.Plumbing.Pipe pst = document.GetElement(new ElementId(ST_ID)) as Autodesk.Revit.DB.Plumbing.Pipe;
if (pv == null) return T8NoGo("pipe 14609072 not found");
if (pst == null) return T8NoGo("stub pipe 14187965 not found (deleted?)");
LocationCurve lcv = pv.Location as LocationCurve, lcs = pst.Location as LocationCurve;
if (lcv == null || lcs == null) return T8NoGo("14609072 or 14187965 has no location curve");
XYZ v0 = lcv.Curve.GetEndPoint(0), v1 = lcv.Curve.GetEndPoint(1);
int vTopI = v0.Z >= v1.Z ? 0 : 1;
XYZ vTop = vTopI == 0 ? v0 : v1, vBot = vTopI == 0 ? v1 : v0;
XYZ s0 = lcs.Curve.GetEndPoint(0), s1 = lcs.Curve.GetEndPoint(1);
int sTopI = s0.Z >= s1.Z ? 0 : 1;
XYZ sTop = sTopI == 0 ? s0 : s1, sBot = sTopI == 0 ? s1 : s0;
if (T8Dxy(v0, v1) > 0.02) return T8NoGo("14609072 is not vertical: " + T8P(v0) + "-" + T8P(v1));
if (T8Dxy(s0, s1) > 0.02) return T8NoGo("stub 14187965 is not vertical: " + T8P(s0) + "-" + T8P(s1));
if (Math.Abs(pv.Diameter - DIA) > 0.002 || Math.Abs(pst.Diameter - DIA) > 0.002)
    return T8NoGo("diameters 14609072=" + T8F(pv.Diameter * 12) + "in, 14187965=" + T8F(pst.Diameter * 12) + "in (need 4)");
Connector cvb = T8ConnAt(pv, vBot), cst = T8ConnAt(pst, sTop), csb = T8ConnAt(pst, sBot);
if (cvb == null || cst == null || csb == null) return T8NoGo("end connectors of 14609072 / 14187965 not found");
bool vOpen = !cvb.IsConnected, sOpen = !cst.IsConnected;
string routeState = (vOpen && sOpen) ? "todo" : ((!vOpen && !sOpen) ? "done" : "partial");
if (routeState == "partial") return T8NoGo("partial state: 14609072 bottom " + (vOpen ? "open" : "connected") + ", stub top " + (sOpen ? "open" : "connected") + " - inspect before running");
MEPSystem sysV = T8Sys(pv), sysS = T8Sys(pst);
string sysVn = sysV != null ? sysV.Name + "(" + sysV.Id.Value + ")" : "none", sysSn = sysS != null ? sysS.Name + "(" + sysS.Id.Value + ")" : "none";
sb.Append("\nV 14609072 bot=").Append(T8P(vBot)).Append(" top=").Append(T8P(vTop)).Append(" sys=").Append(sysVn)
  .Append(" | stub 14187965 top=").Append(T8P(sTop)).Append(" bot=").Append(T8P(sBot)).Append(" sys=").Append(sysSn).Append(" route=").Append(routeState);
if (routeState == "todo")
{
    if (T8Dxy2(vBot, BX, BY) > 0.05 || Math.Abs(vBot.Z - BZ) > 0.10) return T8NoGo("14609072 bottom is " + T8P(vBot) + ", task says (18.73,-259.40,18.81)");
    if (T8Dxy2(sTop, TX, TY) > 0.05 || Math.Abs(sTop.Z - TZ) > 0.10) return T8NoGo("stub top is " + T8P(sTop) + ", task says (21.03,-233.94,19.18)");
    if (sysS == null || (sysS.Id.Value != SYS6 && (sysS.Name ?? "").Trim() != "GSAN 6")) return T8NoGo("stub 14187965 system is " + sysSn + ", not GSAN 6 (14196807)");
    if (sysV == null || (sysV.Id.Value != SYS9 && (sysV.Name ?? "").Trim() != "GSAN 9")) return T8NoGo("14609072 system is " + sysVn + ", not GSAN 9 (14608926)");
    if (!T8Nb(pst).Contains(ST_FIT)) warn.Add("stub bottom not joined to fitting 14197329 (" + string.Join("/", T8Nb(pst)) + ")");
}

// ---- 2. types, level, workset (by name in THIS model)
Autodesk.Revit.DB.Plumbing.PipeType ptype = null; List<string> names = new List<string>(); int nDup = 0;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.Plumbing.PipeType)))
{
    names.Add(e.Name);
    if (e.Name == PIPE_TYPE) { if (ptype != null) nDup++; else ptype = e as Autodesk.Revit.DB.Plumbing.PipeType; }
}
if (ptype == null) return T8NoGo("pipe type '" + PIPE_TYPE + "' not found; have: " + string.Join(", ", names.Take(8)));
if (nDup > 0) return T8NoGo("more than one pipe type named '" + PIPE_TYPE + "'");
Autodesk.Revit.DB.Plumbing.PipingSystemType stype = null; names.Clear(); nDup = 0;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.Plumbing.PipingSystemType)))
{
    names.Add(e.Name);
    if (e.Name == SYS_TYPE) { if (stype != null) nDup++; else stype = e as Autodesk.Revit.DB.Plumbing.PipingSystemType; }
}
if (stype == null) return T8NoGo("system type '" + SYS_TYPE + "' not found; have: " + string.Join(", ", names.Where(nm => nm.IndexOf("GSAN", StringComparison.OrdinalIgnoreCase) >= 0).Take(6)));
if (nDup > 0) return T8NoGo("more than one system type named '" + SYS_TYPE + "'");
Parameter pstV = pv.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM), pstS = pst.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM);
if (pstV != null && pstV.AsElementId().Value != stype.Id.Value) warn.Add("14609072 system type id " + pstV.AsElementId().Value + " != " + stype.Id.Value);
if (pstS != null && pstS.AsElementId().Value != stype.Id.Value) warn.Add("stub system type id " + pstS.AsElementId().Value + " != " + stype.Id.Value + " (systems may not merge)");
RoutingPreferenceManager rpm = ptype.RoutingPreferenceManager;
int nElb = rpm != null ? rpm.GetNumberOfRules(RoutingPreferenceRuleGroupType.Elbows) : 0;
if (nElb == 0) return T8NoGo("pipe type '" + PIPE_TYPE + "' has no elbow routing rule");
FamilySymbol elSym = document.GetElement(rpm.GetRule(RoutingPreferenceRuleGroupType.Elbows, 0).MEPPartId) as FamilySymbol;
string elName = elSym != null ? elSym.FamilyName + ":" + elSym.Name : "?";
Level lvl = pv.ReferenceLevel;
if (lvl == null) lvl = document.GetElement(pv.LevelId) as Level;
if (lvl == null) return T8NoGo("14609072 has no reference level");
int wsId = pv.WorksetId.IntegerValue;
Parameter pod = pst.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER), pidm = pst.get_Parameter(BuiltInParameter.RBS_PIPE_INNER_DIAM_PARAM);
double rOut = (pod != null && pod.AsDouble() > 0) ? pod.AsDouble() / 2 : 0.1875;
double rIn = (pidm != null && pidm.AsDouble() > 0) ? pidm.AsDouble() / 2 : DIA / 2;
sb.Append("\ntypes: pipe=").Append(ptype.Id.Value).Append(" sys=").Append(stype.Id.Value).Append(" elbow_rule1/").Append(nElb).Append("=").Append(elName)
  .Append(" level=").Append(lvl.Name).Append(" ws=").Append(wsId).Append(" rOut=").Append(T8F(rOut));

// ---- 3. revision and Revision Tag type
Revision rev = null; int nrev = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REVDESC) { nrev++; if (rev == null) rev = r; }
}
if (nrev != 1) return T8NoGo(nrev + " revisions have Description 'FEWD Submittal' (need exactly 1)");
if (rev.Issued) return T8NoGo("revision 'FEWD Submittal' (id " + rev.Id.Value + ") is issued");
if (rev.Id.Value != REV_ID) warn.Add("FEWD Submittal id is " + rev.Id.Value + " (task says 15034302)");
FamilySymbol tagSym = null; List<string> tagNames = new List<string>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol; if (fs == null) continue;
    tagNames.Add(fs.FamilyName + ":" + fs.Name);
    if (fs.FamilyName == "MEP_Tag_Revision" && fs.Name == "Revision Tag") tagSym = fs;
}
if (tagSym == null)
    foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
    {
        FamilySymbol fs = e as FamilySymbol;
        if (fs != null && (fs.FamilyName == "Revision Tag" || fs.Name == "Revision Tag")) { tagSym = fs; warn.Add("tag type is " + fs.FamilyName + ":" + fs.Name); break; }
    }
if (tagSym == null) return T8NoGo("no 'Revision Tag' type; found: " + string.Join(", ", tagNames.Take(5)));
double spIn = 0.125;
try { spIn = RevisionSettings.GetRevisionSettings(document).RevisionCloudSpacing * 12.0; }
catch (Exception) { warn.Add("RevisionCloudSpacing unreadable; 0.125 in assumed"); }
sb.Append(" rev=").Append(rev.Id.Value).Append(" tag=").Append(tagSym.Id.Value);

// ---- 4. re-slope plan (QB2-9): GSAN 9 horizontal tree from the top of 14609072
T8Tree tree = null; string resState = "off";
if (!noRes)
{
    tree = T8Build(document, pv, vTop, SL, V_ID, sysV != null ? sysV.Id.Value : -1);
    if (tree.err == null)
        foreach (long hz in HZ_IDS) if (!tree.hp.Any(x => x.id == hz)) { tree.err = "horizontal " + hz + " is not in the tree from 14609072"; break; }
    if (tree.err == null)
    {
        double vNew = vTop.Z + tree.dzRoot - vBot.Z;
        if (vNew < 0.3) tree.err = "14609072 would shrink to " + T8F(vNew) + " ft";
        for (int i = 0; i < tree.dvId.Count && tree.err == null; i++)
        {
            double nl = tree.dvLen[i] + tree.dz[tree.dvNode[i]];
            if (nl < 0.3) tree.err = "down vertical " + tree.dvId[i] + " would shrink to " + T8F(nl) + " ft";
        }
        foreach (KeyValuePair<long, double> kv in tree.dz) if (tree.err == null && Math.Abs(kv.Value) > 1.0) tree.err = "node " + kv.Key + " would move " + T8F(kv.Value) + " ft (> 1.0)";
        foreach (KeyValuePair<long, double> kv in tree.dz) if (tree.err == null && kv.Value > 0.002) tree.err = "node " + kv.Key + " would be raised " + T8F(kv.Value) + " ft";
    }
    if (tree.err != null) resState = "nogo";
    else resState = tree.dz.Values.All(dv => Math.Abs(dv) < 0.003) ? "done" : "todo";
}

// ---- 5. ownership
if (document.IsWorkshared)
{
    List<long> chk = new List<long> { V_ID, ST_ID, ST_FIT, VIEW_IDS[0], VIEW_IDS[1] };
    if (tree != null) { chk.AddRange(tree.order); chk.AddRange(tree.hp.Select(x => x.id)); chk.AddRange(tree.dvId); }
    List<string> owned = new List<string>();
    foreach (long id in chk.Distinct())
    {
        if (document.GetElement(new ElementId(id)) == null) continue;
        if (WorksharingUtils.GetCheckoutStatus(document, new ElementId(id)) == CheckoutStatus.OwnedByOtherUser) owned.Add(id.ToString());
    }
    if (owned.Count > 0) return T8NoGo("owned by another user: " + string.Join(",", owned.Take(12)));
}

// ---- 6. route plan (from the LIVE bottom of 14609072; it is held in place during the re-slope)
XYZ E1 = new XYZ(vBot.X, vBot.Y, vBot.Z - DROP1);
double L1 = Math.Abs(sTop.Y - vBot.Y), L2 = Math.Abs(sTop.X - vBot.X);
XYZ E2 = new XYZ(vBot.X, sTop.Y, E1.Z - SL * L1);
XYZ E3 = new XYZ(sTop.X, sTop.Y, E2.Z - SL * L2);
if (routeState == "todo")
{
    if (sTop.Y <= vBot.Y || sTop.X <= vBot.X) return T8NoGo("stub is not north-east of 14609072");
    if (L1 < 3.0 || L2 < 1.2) return T8NoGo("legs too short: north " + T8F(L1) + " ft, east " + T8F(L2) + " ft");
    if (L2 < 1.6) warn.Add("east leg only " + T8F(L2) + " ft between two elbows");
    if (E3.Z > sTop.Z - 0.05) return T8NoGo("arrival " + T8F(E3.Z) + " is not below stub top " + T8F(sTop.Z));
    if (E3.Z < sBot.Z + 2.0) return T8NoGo("arrival " + T8F(E3.Z) + " too close to stub bottom " + T8F(sBot.Z));
}
sb.Append("\nplan pipe1 ").Append(T8P(E1)).Append("->").Append(T8P(E2)).Append(" L=").Append(T8F(L1)).Append(" d=4in slope=0.25in/ft inv=").Append(T8F(E1.Z - rIn)).Append(">").Append(T8F(E2.Z - rIn))
  .Append("\nplan pipe2 ").Append(T8P(E2)).Append("->").Append(T8P(E3)).Append(" L=").Append(T8F(L2)).Append(" d=4in slope=0.25in/ft inv=").Append(T8F(E2.Z - rIn)).Append(">").Append(T8F(E3.Z - rIn))
  .Append("\nplan elbows (").Append(elName).Append("): e1@E1 down->N, e2@E2 N->E, e3@E3 E->down; pipe ends are trimmed by the elbows")
  .Append("\nmodify: 14609072 bottom z ").Append(T8F(vBot.Z)).Append("->").Append(T8F(E1.Z)).Append("; stub 14187965 top z ").Append(T8F(sTop.Z)).Append("->").Append(T8F(E3.Z)).Append(" (then elbow trim); delete: none");

// ---- 7. re-slope report
string resLine = "reslope=" + resState;
if (tree != null)
{
    resLine += " method=disconnect-move-set-reconnect root=" + tree.root + " nodes=" + tree.order.Count + " fixed=" + tree.fixedN.Count + " hz=" + tree.hp.Count + " joints=" + (2 * tree.hp.Count) + " dz_root=" + T8F(tree.dzRoot)
        + " V_top " + T8F(vTop.Z) + "->" + T8F(vTop.Z + tree.dzRoot);
    if (tree.err != null) resLine += " ERR " + tree.err;
    List<string> ps = new List<string>();
    foreach (T8HP h in tree.hp) ps.Add(h.id + ":" + T8F((h.zu - h.zd) / h.lenxy * 12) + ">0.25(L" + T8F(h.lenxy) + ")");
    resLine += " pipes " + T8Cap(string.Join(" ", ps), 420);
    List<string> ms = new List<string>();
    foreach (long n in tree.order) if (tree.dz.ContainsKey(n) && Math.Abs(tree.dz[n]) >= 0.002) ms.Add(n + ":" + T8F(tree.dz[n]));
    resLine += " moves " + T8Cap(string.Join(" ", ms), 300);
}
sb.Append("\n").Append(resLine);

// ---- 8. clash plan (pipes + ducts, host model)
List<double[]> segs = new List<double[]> { T8Seg(vBot, E1), T8Seg(E1, E2), T8Seg(E2, E3) };
T8CR cr = null;
if (routeState == "todo")
{
    cr = T8Clash(document, segs, rOut, new HashSet<long> { V_ID, ST_ID }, SAN_ID);
    sb.Append("\nclash plan: cand=").Append(cr.nCand).Append(" hits<0.5ft=").Append(cr.hits.Count).Append(" hard=").Append(cr.hard)
      .Append(" SAN13369507 cc=").Append(T8F(cr.sanCC)).Append(" gap=").Append(T8F(cr.sanGap)).Append(" ").Append(T8Cap(string.Join(" ", cr.hits), 400));
}

// ---- 9. views and cloud plans
List<XYZ> rpts = new List<XYZ>();
for (int k = 0; k <= 10; k++) { double tt = k / 10.0; rpts.Add(T8Lerp(E1, E2, tt)); rpts.Add(T8Lerp(E2, E3, tt)); }
rpts.Add(vBot);
List<XYZ> spts = new List<XYZ>();
if (resState == "todo")
    foreach (T8HP h in tree.hp)
    {
        LocationCurve hl = document.GetElement(new ElementId(h.id)).Location as LocationCurve;
        spts.Add(hl.Curve.GetEndPoint(0)); spts.Add(hl.Curve.GetEndPoint(1));
    }
double zMin = E3.Z - rOut, zMax = Math.Max(vBot.Z, E1.Z) + rOut;
T8VP[] vps = new T8VP[2];
int planFail = 0;
for (int i = 0; i < 2; i++)
{
    vps[i] = T8PlanView(document, VIEW_IDS[i], rpts, spts, rOut, rev, zMin, zMax, wsId, spIn);
    T8VP p = vps[i];
    if (p.err != null) { sb.Append("\nview ").Append(VIEW_IDS[i]).Append(" ERR ").Append(p.err); if (i == 0) planFail++; continue; }
    if (p.sheetNo != SHEET_EXP[i]) warn.Add("view " + VIEW_IDS[i] + " is on sheet " + p.sheetNo + " (task says " + SHEET_EXP[i] + ")");
    p.want = (i == 0) ? (p.clu != null) : (p.visible && p.clu != null);
    if (i == 0 && p.clu == null) { planFail++; p.rep += " NO_GO route outside the crop of the P2.20 view"; }
    if (p.want && p.already) { p.want = false; p.rep += " cloud already in target state (" + p.alreadyId + ")"; }
    if (p.want)
    {
        string perr = CB6Plan(new List<CB6Clu> { p.clu }, p.obs, p.hard, p.margin, p.clr, p.inClr, p.TG, p.maxDim, 40);
        if (perr != null) { p.want = false; p.planErr = perr; planFail++; p.rep += " PLAN NO_GO: " + perr; }
        else p.rep += " " + T8PlanRep(p);
    }
    else if (i == 1 && !p.visible) p.rep += " -> no cloud (route not visible: " + p.why + ")";
    sb.Append("\n").Append(T8Cap(p.rep, 520));
}

// ---- 10. DRY RUN
int nogo = planFail + (resState == "nogo" ? 1 : 0) + (cr != null && cr.hard > 0 && !allowClash ? 1 : 0) + (cr != null && cr.sanCC >= 0 && cr.sanCC < 0.6 ? 1 : 0);
int skipN = (resState == "done" ? 1 : 0) + (routeState == "done" ? 1 : 0);
for (int i = 0; i < 2; i++) if (vps[i] != null && vps[i].already) skipN++;
if (cr != null && cr.sanCC < 0) warn.Add("SAN 13369507 not found / no curve");
if (dry)
{
    if (warn.Count > 0) sb.Append("\nwarn: ").Append(T8Cap(string.Join("; ", warn), 400));
    sb.Append("\ndone=0 skipped=").Append(skipN).Append(" failed=").Append(nogo)
      .Append(" (dryrun: would_reslope=").Append(resState == "todo" ? "yes" : "no").Append(" would_route=").Append(routeState == "todo" ? "yes" : "no")
      .Append(" would_clouds=").Append((vps[0] != null && vps[0].want ? 1 : 0) + (vps[1] != null && vps[1].want ? 1 : 0)).Append(")");
    return T8Cap(sb.ToString(), 3300);
}
if (resState == "nogo") return T8NoGo("re-slope: " + tree.err + " (run with 'noreslope' to route only)");
if (planFail > 0) return T8NoGo("view/cloud plan failed - run dryrun for details");
if (cr != null && cr.hard > 0 && !allowClash) return T8NoGo("planned route overlaps: " + T8Cap(string.Join(" ", cr.hits), 600));
if (cr != null && cr.sanCC >= 0 && cr.sanCC < 0.6) return T8NoGo("SAN 13369507 centre-to-centre " + T8F(cr.sanCC) + " ft < 0.6");

// ---- 11. RUN: one SubTransaction, all or nothing
string item = "start";
List<string> doneIds = new List<string>(), skipIds = new List<string>(), cleanup = new List<string>(), restore = new List<string>(), lines = new List<string>(), oldZ = new List<string>();
if (resState == "done") skipIds.Add("reslope(already 1/4in/ft)");
if (routeState == "done") skipIds.Add("route(14609072 bottom and stub top already connected)");
for (int i = 0; i < 2; i++) if (vps[i] != null && vps[i].already) skipIds.Add("cloud:" + VIEW_IDS[i] + "(already " + vps[i].alreadyId + ")");
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    // 11a. re-slope: disconnect -> move fittings once -> set pipe ends -> reconnect (v2)
    if (resState == "todo")
    {
        item = "reslope read";
        Dictionary<long, XYZ> pre = new Dictionary<long, XYZ>();
        foreach (long n in tree.order) pre[n] = T8Loc(document.GetElement(new ElementId(n)));
        foreach (long n in tree.order) oldZ.Add(n + ":" + T8R(pre[n].Z));
        Dictionary<long, XYZ[]> hEnds = new Dictionary<long, XYZ[]>();   // pipe id -> {end at h.down, end at h.up}
        Dictionary<long, T8HP> hById = new Dictionary<long, T8HP>();
        List<long[]> joints = new List<long[]>();                         // {pipe id, 0 = down end / 1 = up end, fitting id}
        foreach (T8HP h in tree.hp)
        {
            hById[h.id] = h;
            Element he = document.GetElement(new ElementId(h.id));
            LocationCurve hl = he.Location as LocationCurve;
            XYZ ha = hl.Curve.GetEndPoint(0), hb = hl.Curve.GetEndPoint(1);
            bool aDown = T8Dxy2(ha, h.dx, h.dy) <= T8Dxy2(hb, h.dx, h.dy);
            XYZ hdn = aDown ? ha : hb, hup = aDown ? hb : ha;
            hEnds[h.id] = new XYZ[] { hdn, hup };
            int nj = 0;
            foreach (Connector c in T8Conns(he))
            {
                int endK = c.Origin.DistanceTo(hdn) <= c.Origin.DistanceTo(hup) ? 0 : 1;
                if (!c.IsConnected) throw new Exception("pipe " + h.id + " end " + endK + " is open");
                long wantOwner = endK == 0 ? h.down : h.up;
                foreach (Element ow in T8Partners(c, he))
                {
                    if (!(ow is FamilyInstance)) throw new Exception("pipe " + h.id + " end " + endK + " joins " + ow.Id.Value + " (" + ow.GetType().Name + "), not a fitting");
                    if (ow.Id.Value != wantOwner) throw new Exception("pipe " + h.id + " end " + endK + " joins " + ow.Id.Value + ", tree says " + wantOwner);
                    joints.Add(new long[] { h.id, endK, ow.Id.Value }); nj++;
                }
            }
            if (nj != 2) throw new Exception("pipe " + h.id + " has " + nj + " fitting joints (need 2)");
        }

        item = "reslope disconnect";
        foreach (long[] jt in joints)
        {
            item = "reslope disconnect " + jt[0] + "-" + jt[2];
            Element pe = document.GetElement(new ElementId(jt[0]));
            Connector pc = T8ConnAt(pe, hEnds[jt[0]][(int)jt[1]]);
            Connector fc = null;
            foreach (Connector r in pc.AllRefs)
                if (r != null && r.Owner != null && r.Owner.Id.Value == jt[2] && (r.ConnectorType & ConnectorType.Physical) != 0) { fc = r; break; }
            if (fc == null) throw new Exception("connector of " + jt[2] + " on pipe " + jt[0] + " not found");
            pc.DisconnectFrom(fc);
        }
        document.Regenerate();
        item = "reslope disconnect check";
        foreach (T8HP h in tree.hp)
            if (T8Conns(document.GetElement(new ElementId(h.id))).Any(cn => cn.IsConnected)) throw new Exception("pipe " + h.id + " still connected after DisconnectFrom");
        string bad0 = T8NodeCheck(document, tree, pre, new HashSet<long>(), hEnds, NTOL);
        if (bad0 != null) throw new Exception("after disconnect: " + bad0);

        List<long> gSeen = new List<long>(); HashSet<long> placed = new HashSet<long>(); List<string> mv = new List<string>();
        foreach (long n in tree.order)
        {
            long g = tree.grp[n];
            if (gSeen.Contains(g)) continue;
            gSeen.Add(g);
            List<long> mem = tree.order.Where(x => tree.grp[x] == g).ToList();
            item = "reslope move " + string.Join("+", mem);
            if (mem.Any(x => tree.fixedN.Contains(x)))
            {
                if (Math.Abs(tree.dz[n]) > NTOL) throw new Exception("fixed node group " + string.Join("+", mem) + " needs dz " + T8F(tree.dz[n]));
                foreach (long x in mem) placed.Add(x);
                continue;
            }
            XYZ curN = T8Loc(document.GetElement(new ElementId(n)));
            double tgtZ = pre[n].Z + tree.dz[n], dMove = tgtZ - curN.Z;
            if (Math.Abs(dMove) >= 0.0005)
                ElementTransformUtils.MoveElements(document, mem.Select(x => new ElementId(x)).ToList(), new XYZ(0, 0, dMove));
            document.Regenerate();
            foreach (long x in mem) placed.Add(x);
            mv.Add(string.Join("+", mem) + ":" + T8F(dMove));
            string badM = T8NodeCheck(document, tree, pre, placed, hEnds, NTOL);
            if (badM != null) throw new Exception("after moving " + string.Join("+", mem) + " by " + T8F(dMove) + ": " + badM);
        }

        item = "reslope 14609072 bottom";
        LocationCurve lvr = pv.Location as LocationCurve;
        XYZ vtR = T8Top(pv), vbR = T8Bottom(pv);
        if (vbR.DistanceTo(vBot) > 0.005)
        {
            lvr.Curve = vTopI == 0 ? Line.CreateBound(vtR, vBot) : Line.CreateBound(vBot, vtR);
            document.Regenerate();
            warn.Add("14609072 bottom was dragged to " + T8P(vbR) + " by the root move; put back at " + T8P(vBot));
        }
        if (!T8Partners(T8ConnAt(pv, T8Top(pv)), pv).Any(x => x.Id.Value == tree.root)) throw new Exception("14609072 top no longer joined to root " + tree.root);
        string badV = T8NodeCheck(document, tree, pre, placed, hEnds, NTOL);
        if (badV != null) throw new Exception("after 14609072 bottom hold: " + badV);

        foreach (T8HP h in tree.hp)
        {
            item = "reslope set pipe " + h.id;
            LocationCurve hl = document.GetElement(new ElementId(h.id)).Location as LocationCurve;
            XYZ ha = hl.Curve.GetEndPoint(0), hb = hl.Curve.GetEndPoint(1);
            XYZ[] oe = hEnds[h.id];
            bool aDown = ha.DistanceTo(oe[0]) <= hb.DistanceTo(oe[0]);
            XYZ nd = new XYZ(oe[0].X, oe[0].Y, oe[0].Z + tree.dz[h.down]), nu = new XYZ(oe[1].X, oe[1].Y, oe[1].Z + tree.dz[h.up]);
            hl.Curve = aDown ? Line.CreateBound(nd, nu) : Line.CreateBound(nu, nd);
        }
        document.Regenerate();

        foreach (long[] jt in joints)
        {
            item = "reslope reconnect " + jt[0] + "-" + jt[2];
            T8HP h = hById[jt[0]];
            XYZ oEnd = hEnds[jt[0]][(int)jt[1]];
            XYZ endP = new XYZ(oEnd.X, oEnd.Y, oEnd.Z + tree.dz[jt[1] == 0 ? h.down : h.up]);
            Element pe = document.GetElement(new ElementId(jt[0])), fe2 = document.GetElement(new ElementId(jt[2]));
            Connector pc = T8ConnAt(pe, endP);
            if (pc == null || pc.Origin.DistanceTo(endP) > NTOL) throw new Exception("pipe " + jt[0] + " end is " + (pc != null ? T8P(pc.Origin) : "?") + ", want " + T8P(endP));
            Connector fc = null; double bd = double.MaxValue;
            foreach (Connector c in T8Conns(fe2)) { if (c.IsConnected) continue; double d = c.Origin.DistanceTo(endP); if (d < bd) { bd = d; fc = c; } }
            if (fc == null) throw new Exception("fitting " + jt[2] + " has no free connector for pipe " + jt[0]);
            if (bd > NTOL) throw new Exception("fitting " + jt[2] + " free connector is " + T8F(bd) + " ft from pipe " + jt[0] + " end " + T8P(endP));
            pc.ConnectTo(fc);
        }
        document.Regenerate();

        item = "reslope check";
        string badF = T8NodeCheck(document, tree, pre, placed, null, NTOL);
        if (badF != null) throw new Exception("after reconnect: " + badF);
        List<string> sl = new List<string>();
        foreach (T8HP h in tree.hp)
        {
            Element he = document.GetElement(new ElementId(h.id));
            LocationCurve hl = he.Location as LocationCurve;
            XYZ ha = hl.Curve.GetEndPoint(0), hb = hl.Curve.GetEndPoint(1);
            bool aDown = T8Dxy2(ha, h.dx, h.dy) <= T8Dxy2(hb, h.dx, h.dy);
            XYZ dn = aDown ? ha : hb, up = aDown ? hb : ha;
            double lxy = T8Dxy(ha, hb), sIn = (up.Z - dn.Z) / lxy * 12.0;
            if (Math.Abs(sIn - 0.25) > 0.01) throw new Exception("pipe " + h.id + " slope " + T8F(sIn) + " in/ft after reconnect (want 0.25 falling to 14609072)");
            if (T8Conns(he).Any(cn => !cn.IsConnected)) throw new Exception("pipe " + h.id + " has an open end after reconnect");
            HashSet<long> nb = T8Nb(he);
            if (!nb.SetEquals(h.nb)) throw new Exception("pipe " + h.id + " connections changed: " + string.Join("/", nb) + " (was " + string.Join("/", h.nb) + ")");
            sl.Add(h.id + ":" + T8F((h.zu - h.zd) / h.lenxy * 12) + ">" + T8F(sIn));
        }
        XYZ vb2 = T8Bottom(pv);
        if (vb2.DistanceTo(vBot) > 0.005) warn.Add("14609072 bottom moved to " + T8P(vb2) + " in the re-slope; route recomputed from it");
        doneIds.Add("reslope[" + tree.hp.Count + " pipes, " + mv.Count + " moves, " + joints.Count + " joints]");
        lines.Add("reslope: dz_root=" + T8F(tree.dzRoot) + " slopes " + T8Cap(string.Join(" ", sl), 380) + " moves " + T8Cap(string.Join(" ", mv), 260));
    }

    // 11b. route
    if (routeState == "todo")
    {
        item = "route read";
        LocationCurve lv2 = pv.Location as LocationCurve;
        XYZ va = lv2.Curve.GetEndPoint(0), vb = lv2.Curve.GetEndPoint(1);
        XYZ vTopN = vTopI == 0 ? va : vb, vBotN = vTopI == 0 ? vb : va;
        E1 = new XYZ(vBotN.X, vBotN.Y, vBotN.Z - DROP1);
        E2 = new XYZ(vBotN.X, sTop.Y, E1.Z - SL * Math.Abs(sTop.Y - vBotN.Y));
        E3 = new XYZ(sTop.X, sTop.Y, E2.Z - SL * Math.Abs(sTop.X - vBotN.X));
        restore.Add("end:" + V_ID + ":" + (1 - vTopI) + ":" + T8R(vBot.X) + ":" + T8R(vBot.Y) + ":" + T8R(vBot.Z));
        restore.Add("end:" + ST_ID + ":" + sTopI + ":" + T8R(sTop.X) + ":" + T8R(sTop.Y) + ":" + T8R(sTop.Z));
        HashSet<long> vTopNb = new HashSet<long>(T8Partners(T8ConnAt(pv, vTopN), pv).Select(x => x.Id.Value));
        item = "extend 14609072";
        lv2.Curve = vTopI == 0 ? Line.CreateBound(vTopN, E1) : Line.CreateBound(E1, vTopN);
        item = "shorten stub 14187965";
        LocationCurve ls2 = pst.Location as LocationCurve;
        XYZ sBotN = sTopI == 0 ? ls2.Curve.GetEndPoint(1) : ls2.Curve.GetEndPoint(0);
        ls2.Curve = sTopI == 0 ? Line.CreateBound(E3, sBotN) : Line.CreateBound(sBotN, E3);
        document.Regenerate();
        HashSet<long> vTopNb2 = new HashSet<long>(T8Partners(T8ConnAt(pv, vTopN), pv).Select(x => x.Id.Value));
        if (!vTopNb2.SetEquals(vTopNb)) throw new Exception("14609072 top connection changed after the endpoint edit");
        if (!T8ConnAt(pst, sBotN).IsConnected) throw new Exception("stub bottom disconnected after the endpoint edit");

        item = "pipe1";
        Autodesk.Revit.DB.Plumbing.Pipe p1 = Autodesk.Revit.DB.Plumbing.Pipe.Create(document, stype.Id, ptype.Id, lvl.Id, E1, E2);
        T8SetDia(p1, DIA); T8SetWs(p1, wsId);
        cleanup.Add("pipe:" + p1.Id.Value);
        item = "pipe2";
        Autodesk.Revit.DB.Plumbing.Pipe p2 = Autodesk.Revit.DB.Plumbing.Pipe.Create(document, stype.Id, ptype.Id, lvl.Id, E2, E3);
        T8SetDia(p2, DIA); T8SetWs(p2, wsId);
        cleanup.Add("pipe:" + p2.Id.Value);
        document.Regenerate();
        if (Math.Abs(p1.Diameter - DIA) > 0.001 || Math.Abs(p2.Diameter - DIA) > 0.001) throw new Exception("new pipe diameter " + T8F(p1.Diameter * 12) + "/" + T8F(p2.Diameter * 12) + " in");

        item = "elbow1";
        FamilyInstance e1 = document.Create.NewElbowFitting(T8ConnAt(p1, E1), T8ConnAt(pv, E1));
        document.Regenerate();
        cleanup.Insert(0, "fit:" + e1.Id.Value);
        item = "elbow2";
        FamilyInstance e2 = document.Create.NewElbowFitting(T8ConnAt(p1, E2), T8ConnAt(p2, E2));
        document.Regenerate();
        cleanup.Insert(0, "fit:" + e2.Id.Value);
        item = "elbow3";
        FamilyInstance e3 = document.Create.NewElbowFitting(T8ConnAt(p2, E3), T8ConnAt(pst, E3));
        document.Regenerate();
        cleanup.Insert(0, "fit:" + e3.Id.Value);
        T8SetWs(e1, wsId); T8SetWs(e2, wsId); T8SetWs(e3, wsId);
        document.Regenerate();

        item = "route check";
        long p1i = p1.Id.Value, p2i = p2.Id.Value, e1i = e1.Id.Value, e2i = e2.Id.Value, e3i = e3.Id.Value;
        if (!T8Nb(pv).Contains(e1i)) throw new Exception("14609072 bottom not joined to elbow " + e1i);
        if (!T8Nb(pst).Contains(e3i)) throw new Exception("stub top not joined to elbow " + e3i);
        HashSet<long> n1 = T8Nb(p1), n2 = T8Nb(p2);
        if (!(n1.Contains(e1i) && n1.Contains(e2i) && n1.Count == 2)) throw new Exception("pipe1 joins " + string.Join("/", n1));
        if (!(n2.Contains(e2i) && n2.Contains(e3i) && n2.Count == 2)) throw new Exception("pipe2 joins " + string.Join("/", n2));
        foreach (FamilyInstance ef in new FamilyInstance[] { e1, e2, e3 })
            if (T8Conns(ef).Any(cn => !cn.IsConnected)) throw new Exception("elbow " + ef.Id.Value + " has an open connector");
        if (T8Conns(p1).Any(cn => !cn.IsConnected) || T8Conns(p2).Any(cn => !cn.IsConnected)) throw new Exception("a new pipe has an open end");
        XYZ[] q1 = T8Ends(p1, E1), q2 = T8Ends(p2, E2);
        double sl1 = (q1[0].Z - q1[1].Z) / T8Dxy(q1[0], q1[1]) * 12.0, sl2 = (q2[0].Z - q2[1].Z) / T8Dxy(q2[0], q2[1]) * 12.0;
        if (Math.Abs(sl1 - 0.25) > 0.01 || Math.Abs(sl2 - 0.25) > 0.01) throw new Exception("slopes " + T8F(sl1) + "/" + T8F(sl2) + " in/ft (want 0.25)");
        if (Math.Abs(q1[0].X - BX) > 0.06 || Math.Abs(q2[0].Y - TY) > 0.06) throw new Exception("route off line: pipe1 x " + T8F(q1[0].X) + ", pipe2 y " + T8F(q2[0].Y));
        Parameter ip1 = p1.get_Parameter(BuiltInParameter.RBS_PIPE_INNER_DIAM_PARAM);
        double ri = (ip1 != null && ip1.AsDouble() > 0) ? ip1.AsDouble() / 2 : rIn;
        XYZ sTopF = T8Top(pst), vBotF = T8Bottom(pv);
        Element fe = document.GetElement(new ElementId(FS_IDS[0]));
        MEPSystem m1 = T8Sys(p1), mS = T8Sys(pst), mF = fe != null ? T8Sys(fe) : null;
        int netN = -1;
        try { Autodesk.Revit.DB.Plumbing.PipingSystem psy = m1 as Autodesk.Revit.DB.Plumbing.PipingSystem; if (psy != null && psy.PipingNetwork != null) netN = psy.PipingNetwork.Size; } catch (Exception) { }
        Parameter po1 = p1.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER);
        double rOut1 = (po1 != null && po1.AsDouble() > 0) ? po1.AsDouble() / 2 : rOut;

        item = "clash";
        T8CR cr2 = T8Clash(document, new List<double[]> { T8Seg(vBotN, E1), T8Seg(E1, E2), T8Seg(E2, E3) }, rOut1, new HashSet<long> { V_ID, ST_ID, p1i, p2i }, SAN_ID);
        if (cr2.hard > 0 && !allowClash) throw new Exception("route overlaps: " + T8Cap(string.Join(" ", cr2.hits), 500));
        if (cr2.sanCC >= 0 && cr2.sanCC < 0.6) throw new Exception("SAN 13369507 c-c " + T8F(cr2.sanCC) + " ft < 0.6");
        string inA = "n/a", inB = "n/a";
        if (vps[0] != null && vps[0].err == null)
            inA = new FilteredElementCollector(document, new ElementId(VIEW_IDS[0])).OfCategory(BuiltInCategory.OST_PipeCurves).ToElementIds().Any(x => x.Value == p1i) ? "yes" : "no";
        if (vps[1] != null && vps[1].err == null)
            inB = new FilteredElementCollector(document, new ElementId(VIEW_IDS[1])).OfCategory(BuiltInCategory.OST_PipeCurves).ToElementIds().Any(x => x.Value == p1i) ? "yes" : "no";

        doneIds.Add("route[pipes " + p1i + "," + p2i + "; elbows " + e1i + "," + e2i + "," + e3i + "; mod " + V_ID + "," + ST_ID + "]");
        lines.Add("pipe1 " + p1i + " " + T8P(q1[0]) + "->" + T8P(q1[1]) + " slope=" + T8F(sl1) + " inv=" + T8F(q1[0].Z - ri) + ">" + T8F(q1[1].Z - ri));
        lines.Add("pipe2 " + p2i + " " + T8P(q2[0]) + "->" + T8P(q2[1]) + " slope=" + T8F(sl2) + " inv=" + T8F(q2[0].Z - ri) + ">" + T8F(q2[1].Z - ri));
        lines.Add("elbows " + e1i + "," + e2i + "," + e3i + " " + e1.Symbol.FamilyName + ":" + e1.Symbol.Name + " | E1=" + T8P(E1) + " E2=" + T8P(E2) + " E3=" + T8P(E3)
            + " | 14609072 bottom " + T8F(vBot.Z) + "->" + T8F(vBotF.Z) + " | stub top " + T8F(sTop.Z) + "->" + T8F(sTopF.Z) + " inv_at_stub=" + T8F(E3.Z - ri));
        lines.Add("clash: hits<0.5ft=" + cr2.hits.Count + " hard=" + cr2.hard + " SAN13369507 cc=" + T8F(cr2.sanCC) + " gap=" + T8F(cr2.sanGap) + " " + T8Cap(string.Join(" ", cr2.hits), 300));
        lines.Add("system (in-transaction, settles on commit): pipe1=" + (m1 != null ? m1.Name + "(" + m1.Id.Value + ")" : "none") + " stub=" + (mS != null ? mS.Name + "(" + mS.Id.Value + ")" : "none")
            + " FS14608919=" + (mF != null ? mF.Name + "(" + mF.Id.Value + ")" : "none") + " network=" + netN + " | in_view_collector P2.20=" + inA + " P4.90=" + inB);
    }

    // 11c. clouds + delta tags
    for (int i = 0; i < 2; i++)
    {
        T8VP p = vps[i];
        if (p == null || !p.want) continue;
        item = "cloud " + VIEW_IDS[i];
        long cid, tid;
        string ln = T8MakeCloud(document, p, rev, tagSym, out cid, out tid);
        cleanup.Insert(0, "cloud:" + cid); cleanup.Insert(0, "tag:" + tid);
        doneIds.Add("cloud" + (i == 0 ? "A" : "B") + "[" + cid + ",tag " + tid + "]");
        lines.Add(SHEET_EXP[i] + " " + ln);
    }
    if (vps[1] != null && !vps[1].want && !vps[1].already) lines.Add("P4.90 view 14243757: no cloud (" + (vps[1].err != null ? vps[1].err : "route not visible: " + vps[1].why) + ")");
    st.Commit();
}
catch (Exception ex)
{
    if (st.HasStarted() && !st.HasEnded()) st.RollBack();
    return "ROLLED_BACK " + item + ": " + T8Cap(ex.Message, 1400) + "\ndone=0 skipped=" + skipIds.Count + " failed=1";
}
sb.Clear();
sb.Append("RUN CB6-R1-T8 v2").Append(noRes ? " noreslope" : "").Append(" rev=").Append(rev.Id.Value).Append(" route=").Append(routeState).Append(" reslope=").Append(resState);
foreach (string s in lines) sb.Append("\n").Append(T8Cap(s, 600));
if (warn.Count > 0) sb.Append("\nwarn: ").Append(T8Cap(string.Join("; ", warn), 300));
sb.Append("\ncleanup_params: [\"").Append(string.Join("\",\"", cleanup)).Append("\"]");
sb.Append("\nrestore_params: [\"").Append(T8Cap(string.Join("\",\"", restore), 500)).Append("\"]");
if (oldZ.Count > 0) sb.Append("\nreslope_old_z (not undoable by cleanup; restore point SYNC A): ").Append(T8Cap(string.Join(" ", oldZ), 300));
sb.Append("\ndone=").Append(doneIds.Count).Append(" skipped=").Append(skipIds.Count).Append(" failed=0");
sb.Append("\ndone_ids: ").Append(doneIds.Count > 0 ? string.Join(" ", doneIds) : "(none)");
sb.Append("\nskipped_ids: ").Append(skipIds.Count > 0 ? string.Join(" ", skipIds) : "(none)");
return T8Cap(sb.ToString(), 3300);
}

// ===================== helpers (T8 = CB6-R1-T8; CB6/T5 cloud helpers reused from CB6-R1-T5.cs, which ran OK) =====================
public static string T8NoGo(string r) { return "NO_GO " + r + "\ndone=0 skipped=0 failed=1"; }
public static string T8Cap(string s, int n) { return s.Length <= n ? s : s.Substring(0, n) + "..."; }
public static string T8F(double d) { return Math.Round(d, 3).ToString(System.Globalization.CultureInfo.InvariantCulture); }
public static string T8R(double d) { return d.ToString("0.#####", System.Globalization.CultureInfo.InvariantCulture); }
public static string T8P(XYZ p) { return "(" + Math.Round(p.X, 2).ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + Math.Round(p.Y, 2).ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + T8F(p.Z) + ")"; }
public static double T8Dxy(XYZ a, XYZ b) { return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)); }
public static double T8Dxy2(XYZ a, double x, double y) { return Math.Sqrt((a.X - x) * (a.X - x) + (a.Y - y) * (a.Y - y)); }
public static XYZ T8Lerp(XYZ a, XYZ b, double t) { return new XYZ(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t); }
public static double[] T8Seg(XYZ a, XYZ b) { return new double[] { a.X, a.Y, a.Z, b.X, b.Y, b.Z }; }
public static List<Connector> T8Conns(Element e)
{
    List<Connector> l = new List<Connector>(); ConnectorManager cm = null;
    MEPCurve mc = e as MEPCurve; if (mc != null) cm = mc.ConnectorManager;
    FamilyInstance fi = e as FamilyInstance; if (fi != null && fi.MEPModel != null) cm = fi.MEPModel.ConnectorManager;
    if (cm == null) return l;
    foreach (Connector c in cm.Connectors) if ((c.ConnectorType & ConnectorType.Physical) != 0) l.Add(c);
    return l;
}
public static List<Element> T8Partners(Connector c, Element self)
{
    List<Element> l = new List<Element>();
    if (c == null || !c.IsConnected) return l;
    foreach (Connector r in c.AllRefs)
    {
        if (r == null || r.Owner == null || r.Owner.Id.Value == self.Id.Value || r.Owner is MEPSystem) continue;
        if ((r.ConnectorType & ConnectorType.Physical) == 0) continue;
        l.Add(r.Owner);
    }
    return l;
}
public static Connector T8ConnAt(Element e, XYZ p)
{
    Connector best = null; double bd = double.MaxValue;
    foreach (Connector c in T8Conns(e)) { double d = c.Origin.DistanceTo(p); if (d < bd) { bd = d; best = c; } }
    return best;
}
public static HashSet<long> T8Nb(Element e)
{
    HashSet<long> h = new HashSet<long>();
    foreach (Connector c in T8Conns(e)) foreach (Element o in T8Partners(c, e)) h.Add(o.Id.Value);
    return h;
}
public static MEPSystem T8Sys(Element e)
{
    if (e == null || !e.IsValidObject) return null;
    MEPCurve mc = e as MEPCurve;
    if (mc != null && mc.MEPSystem != null) return mc.MEPSystem;
    foreach (Connector c in T8Conns(e)) { try { if (c.MEPSystem != null) return c.MEPSystem; } catch (Exception) { } }
    return null;
}
public static bool T8Movable(Element e)
{
    if (!(e is FamilyInstance) || e.Category == null) return false;
    BuiltInCategory bc = e.Category.BuiltInCategory;
    return bc == BuiltInCategory.OST_PipeFitting || bc == BuiltInCategory.OST_PipeAccessory;
}
public static XYZ T8Loc(Element e)
{
    LocationPoint lp = e.Location as LocationPoint;
    if (lp != null) return lp.Point;
    BoundingBoxXYZ bb = e.get_BoundingBox(null);
    return bb != null ? new XYZ(0.5 * (bb.Min.X + bb.Max.X), 0.5 * (bb.Min.Y + bb.Max.Y), 0.5 * (bb.Min.Z + bb.Max.Z)) : XYZ.Zero;
}
// v2 R30 check: every node in 'placed' must be at pre.z + dz, every other node at pre.z (no XY drift); if hEnds is given,
// every horizontal must still have its recorded end points (it must not be dragged while disconnected). null = all OK.
public static string T8NodeCheck(Document doc, T8Tree t, Dictionary<long, XYZ> pre, HashSet<long> placed, Dictionary<long, XYZ[]> hEnds, double tol)
{
    List<string> bad = new List<string>();
    foreach (long n in t.order)
    {
        XYZ now = T8Loc(doc.GetElement(new ElementId(n)));
        double want = placed.Contains(n) ? pre[n].Z + t.dz[n] : pre[n].Z;
        if (Math.Abs(now.Z - want) > tol || T8Dxy(now, pre[n]) > tol)
            bad.Add("node " + n + (placed.Contains(n) ? "(placed)" : "(unplaced)") + " at " + T8P(now) + " want z " + T8F(want));
    }
    if (hEnds != null)
        foreach (KeyValuePair<long, XYZ[]> kv in hEnds)
        {
            LocationCurve lc = doc.GetElement(new ElementId(kv.Key)).Location as LocationCurve;
            XYZ a = lc.Curve.GetEndPoint(0), b = lc.Curve.GetEndPoint(1);
            double d1 = Math.Min(a.DistanceTo(kv.Value[0]), b.DistanceTo(kv.Value[0])), d2 = Math.Min(a.DistanceTo(kv.Value[1]), b.DistanceTo(kv.Value[1]));
            if (d1 > tol || d2 > tol) bad.Add("pipe " + kv.Key + " dragged to " + T8P(a) + "-" + T8P(b));
        }
    return bad.Count == 0 ? null : T8Cap(string.Join("; ", bad), 900);
}
public static XYZ T8Bottom(MEPCurve m) { LocationCurve lc = m.Location as LocationCurve; XYZ a = lc.Curve.GetEndPoint(0), b = lc.Curve.GetEndPoint(1); return a.Z <= b.Z ? a : b; }
public static XYZ T8Top(MEPCurve m) { LocationCurve lc = m.Location as LocationCurve; XYZ a = lc.Curve.GetEndPoint(0), b = lc.Curve.GetEndPoint(1); return a.Z >= b.Z ? a : b; }
// [0] = end nearer p (flow start), [1] = other end
public static XYZ[] T8Ends(MEPCurve m, XYZ p) { LocationCurve lc = m.Location as LocationCurve; XYZ a = lc.Curve.GetEndPoint(0), b = lc.Curve.GetEndPoint(1); return a.DistanceTo(p) <= b.DistanceTo(p) ? new XYZ[] { a, b } : new XYZ[] { b, a }; }
public static void T8SetDia(MEPCurve m, double d)
{
    Parameter p = m.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM);
    if (p == null || p.IsReadOnly) throw new Exception("diameter parameter not settable on " + m.Id.Value);
    p.Set(d);
}
public static void T8SetWs(Element e, int ws)
{
    if (e.WorksetId.IntegerValue == ws) return;
    Parameter p = e.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
    if (p != null && !p.IsReadOnly) p.Set(ws);
}
public class T8HP { public long id, down, up; public double lenxy, zd, zu, dx, dy; public HashSet<long> nb; }
public class T8Tree
{
    public string err = null; public long root = 0; public double dzRoot = 0;
    public List<long> order = new List<long>(); public List<long> fixedN = new List<long>();
    public Dictionary<long, double> f = new Dictionary<long, double>(); public Dictionary<long, double> dz = new Dictionary<long, double>();
    public Dictionary<long, long> grp = new Dictionary<long, long>();
    public List<T8HP> hp = new List<T8HP>();
    public List<long> dvId = new List<long>(); public List<double> dvLen = new List<double>(); public List<long> dvNode = new List<long>();
}
// Walk the horizontal pipes reachable from the fitting on top of 14609072 (verticals are not crossed; they stretch).
// f[node] = how far the node must move relative to the root so every horizontal falls s ft/ft toward the root.
// Every horizontal must be on system sysId (GSAN 9), so no other system is ever re-sloped.
public static T8Tree T8Build(Document doc, Autodesk.Revit.DB.Plumbing.Pipe v, XYZ vTop, double s, long vId, long sysId)
{
    T8Tree t = new T8Tree();
    Connector ct = T8ConnAt(v, vTop);
    if (ct == null || !ct.IsConnected) { t.err = "14609072 top is open"; return t; }
    List<Element> rp = T8Partners(ct, v);
    if (rp.Count != 1 || !T8Movable(rp[0])) { t.err = "14609072 top joins " + rp.Count + " element(s), not one pipe fitting"; return t; }
    t.root = rp[0].Id.Value; t.f[t.root] = 0; t.grp[t.root] = t.root;
    Queue<long> q = new Queue<long>(); q.Enqueue(t.root);
    HashSet<long> seenPipe = new HashSet<long> { vId };
    while (q.Count > 0)
    {
        if (t.order.Count > 150) { t.err = "tree larger than 150 nodes"; return t; }
        long n = q.Dequeue(); t.order.Add(n);
        Element ne = doc.GetElement(new ElementId(n));
        foreach (Connector c in T8Conns(ne))
        {
            foreach (Element e in T8Partners(c, ne))
            {
                long eid = e.Id.Value;
                if (eid == vId) { if (n != t.root) { t.err = "loop back to 14609072 at " + n; return t; } continue; }
                Autodesk.Revit.DB.Plumbing.Pipe p = e as Autodesk.Revit.DB.Plumbing.Pipe;
                if (p != null)
                {
                    if (seenPipe.Contains(eid)) continue;
                    seenPipe.Add(eid);
                    LocationCurve lc = p.Location as LocationCurve;
                    if (lc == null) { t.err = "pipe " + eid + " has no curve"; return t; }
                    XYZ a = lc.Curve.GetEndPoint(0), b = lc.Curve.GetEndPoint(1);
                    bool aNear = a.DistanceTo(c.Origin) <= b.DistanceTo(c.Origin);
                    XYZ en = aNear ? a : b, eo = aNear ? b : a;
                    double lxy = T8Dxy(a, b), dzz = eo.Z - en.Z;
                    if (lxy < 0.02) { if (dzz < 0) { t.dvId.Add(eid); t.dvLen.Add(-dzz); t.dvNode.Add(n); } continue; }
                    if (lxy < 0.05 || Math.Abs(dzz) > 0.05 * lxy) { t.err = "pipe " + eid + " at node " + n + " is neither horizontal nor vertical"; return t; }
                    MEPSystem ms = T8Sys(p);
                    if (sysId > 0 && (ms == null || ms.Id.Value != sysId)) { t.err = "horizontal " + eid + " is on " + (ms != null ? ms.Name : "no system") + ", not GSAN 9"; return t; }
                    Connector co = T8ConnAt(p, eo);
                    List<Element> op = T8Partners(co, p);
                    if (op.Count != 1 || !(op[0] is FamilyInstance)) { t.err = "horizontal " + eid + " far end joins " + op.Count + " family instance(s) (open end or pipe-pipe)"; return t; }
                    long m = op[0].Id.Value;
                    if (t.f.ContainsKey(m)) { t.err = "loop at " + m; return t; }
                    T8HP h = new T8HP(); h.id = eid; h.down = n; h.up = m; h.lenxy = lxy; h.zd = en.Z; h.zu = eo.Z; h.dx = en.X; h.dy = en.Y; h.nb = T8Nb(p);
                    t.hp.Add(h);
                    t.f[m] = t.f[n] + s * lxy - dzz; t.grp[m] = m;
                    if (T8Movable(op[0])) q.Enqueue(m); else t.fixedN.Add(m);
                    continue;
                }
                if (e is FamilyInstance)
                {
                    if (t.f.ContainsKey(eid)) continue;
                    if (!T8Movable(e)) { if (!t.fixedN.Contains(n)) t.fixedN.Add(n); continue; }
                    t.f[eid] = t.f[n]; t.grp[eid] = t.grp[n]; q.Enqueue(eid);
                    continue;
                }
                t.err = "element " + eid + " (" + e.GetType().Name + ") joined to " + n; return t;
            }
        }
    }
    if (t.fixedN.Count > 0)
    {
        double f0 = t.f[t.fixedN[0]];
        foreach (long k in t.fixedN) if (Math.Abs(t.f[k] - f0) > 0.01) { t.err = "fixed nodes " + t.fixedN[0] + "/" + k + " need different drops: no exact 1/4in/ft solution"; return t; }
        t.dzRoot = -f0;
    }
    else { double mx = 0; foreach (double fv in t.f.Values) mx = Math.Max(mx, fv); t.dzRoot = -mx; }
    foreach (KeyValuePair<long, double> kv in t.f) t.dz[kv.Key] = t.dzRoot + kv.Value;
    return t;
}
public static double T8Cl(double x) { return x < 0 ? 0 : (x > 1 ? 1 : x); }
// shortest distance between 3D segments a=(x0,y0,z0,x1,y1,z1) and b
public static double T8SegDist(double[] a, double[] b)
{
    double d1x = a[3] - a[0], d1y = a[4] - a[1], d1z = a[5] - a[2], d2x = b[3] - b[0], d2y = b[4] - b[1], d2z = b[5] - b[2];
    double rx = a[0] - b[0], ry = a[1] - b[1], rz = a[2] - b[2];
    double A = d1x * d1x + d1y * d1y + d1z * d1z, E = d2x * d2x + d2y * d2y + d2z * d2z, F = d2x * rx + d2y * ry + d2z * rz;
    double s, t;
    if (A <= 1e-12 && E <= 1e-12) { s = 0; t = 0; }
    else if (A <= 1e-12) { s = 0; t = T8Cl(F / E); }
    else
    {
        double C = d1x * rx + d1y * ry + d1z * rz;
        if (E <= 1e-12) { t = 0; s = T8Cl(-C / A); }
        else
        {
            double Bv = d1x * d2x + d1y * d2y + d1z * d2z, den = A * E - Bv * Bv;
            s = den > 1e-12 ? T8Cl((Bv * F - C * E) / den) : 0;
            t = (Bv * s + F) / E;
            if (t < 0) { t = 0; s = T8Cl(-C / A); } else if (t > 1) { t = 1; s = T8Cl((Bv - C) / A); }
        }
    }
    double px = a[0] + d1x * s - (b[0] + d2x * t), py = a[1] + d1y * s - (b[1] + d2y * t), pz = a[2] + d1z * s - (b[2] + d2z * t);
    return Math.Sqrt(px * px + py * py + pz * pz);
}
public static double T8Rad(Element e)
{
    MEPCurve mc = e as MEPCurve; if (mc == null) return 0;
    if (e is Autodesk.Revit.DB.Plumbing.Pipe)
    {
        Parameter od = e.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER);
        return (od != null && od.AsDouble() > 0) ? od.AsDouble() / 2 : mc.Diameter / 2;
    }
    Parameter dd = e.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM);
    if (dd != null && dd.AsDouble() > 0) return dd.AsDouble() / 2;
    try { return 0.5 * Math.Sqrt(mc.Width * mc.Width + mc.Height * mc.Height); } catch (Exception) { return 0.5; }
}
public class T8CR { public List<string> hits = new List<string>(); public int hard = 0, nCand = 0; public double sanCC = -1, sanGap = -1; }
public static T8CR T8Clash(Document doc, List<double[]> segs, double rR, HashSet<long> excl, long sanId)
{
    T8CR r = new T8CR();
    double x0 = 1e9, y0 = 1e9, z0 = 1e9, x1 = -1e9, y1 = -1e9, z1 = -1e9;
    foreach (double[] sg in segs)
        for (int k = 0; k < 2; k++) { x0 = Math.Min(x0, sg[3 * k]); y0 = Math.Min(y0, sg[3 * k + 1]); z0 = Math.Min(z0, sg[3 * k + 2]); x1 = Math.Max(x1, sg[3 * k]); y1 = Math.Max(y1, sg[3 * k + 1]); z1 = Math.Max(z1, sg[3 * k + 2]); }
    double pad = 2.5;
    Outline ol = new Outline(new XYZ(x0 - pad, y0 - pad, z0 - pad), new XYZ(x1 + pad, y1 + pad, z1 + pad));
    List<BuiltInCategory> cats = new List<BuiltInCategory> { BuiltInCategory.OST_PipeCurves, BuiltInCategory.OST_DuctCurves };
    List<Element> cand = new FilteredElementCollector(doc).WherePasses(new ElementMulticategoryFilter(cats)).WherePasses(new BoundingBoxIntersectsFilter(ol)).WhereElementIsNotElementType().ToList();
    Element san = doc.GetElement(new ElementId(sanId));
    if (san != null && !cand.Any(x => x.Id.Value == sanId)) cand.Add(san);
    foreach (Element e in cand)
    {
        if (excl.Contains(e.Id.Value)) continue;
        LocationCurve lc = e.Location as LocationCurve; if (lc == null) continue;
        XYZ a = lc.Curve.GetEndPoint(0), b = lc.Curve.GetEndPoint(1);
        double[] qd = new double[] { a.X, a.Y, a.Z, b.X, b.Y, b.Z };
        double cc = 1e9; foreach (double[] sg2 in segs) cc = Math.Min(cc, T8SegDist(sg2, qd));
        double gap = cc - rR - T8Rad(e);
        if (e.Id.Value == sanId) { r.sanCC = cc; r.sanGap = gap; }
        r.nCand++;
        if (gap < 0.5)
        {
            Parameter sn = e.get_Parameter(BuiltInParameter.RBS_SYSTEM_NAME_PARAM);
            r.hits.Add(e.Id.Value + ":" + (e.Category != null ? e.Category.Name : "?") + ":" + (sn != null ? sn.AsString() : "") + ":cc" + T8F(cc) + "/gap" + T8F(gap));
            if (gap < 0) r.hard++;
        }
    }
    return r;
}
public static double T8Plane(Document doc, PlanViewRange vr, PlanViewPlane pl)
{
    try
    {
        Level l = doc.GetElement(vr.GetLevelId(pl)) as Level;
        if (l == null) return double.NaN;
        return l.ProjectElevation + vr.GetOffset(pl);
    }
    catch (Exception) { return double.NaN; }
}
public static double[] T8UV(XYZ p, XYZ R, XYZ U) { return new double[] { p.X * R.X + p.Y * R.Y, p.X * U.X + p.Y * U.Y }; }
public static XYZ T8XYZ(double u, double v, XYZ R, XYZ U, double z) { return new XYZ(u * R.X + v * U.X, u * R.Y + v * U.Y, z); }
public static CB6Box T8BoxUV(double x0, double y0, double x1, double y1, XYZ R, XYZ U, long id)
{
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
    double[] xs = new double[] { x0, x1, x1, x0 }, ys = new double[] { y0, y0, y1, y1 };
    for (int k = 0; k < 4; k++) { double[] uv = T8UV(new XYZ(xs[k], ys[k], 0), R, U); a = Math.Min(a, uv[0]); b = Math.Min(b, uv[1]); c = Math.Max(c, uv[0]); d = Math.Max(d, uv[1]); }
    return new CB6Box(a, b, c, d, id);
}
public static List<double[]> T8SegsUV(RevisionCloud rc, XYZ R, XYZ U)
{
    List<double[]> s = new List<double[]>();
    foreach (Curve c in rc.GetSketchCurves()) { double[] a = T8UV(c.GetEndPoint(0), R, U), b = T8UV(c.GetEndPoint(1), R, U); s.Add(new double[] { a[0], a[1], b[0], b[1] }); }
    return s;
}
public static CB6Box T8HullUV(List<double[]> pts, double d, long id)
{
    double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, e = double.MinValue;
    foreach (double[] p in pts) { a = Math.Min(a, p[0]); b = Math.Min(b, p[1]); c = Math.Max(c, p[0]); e = Math.Max(e, p[1]); }
    return new CB6Box(a - d, b - d, c + d, e + d, id);
}
public static CB6Box T8Clip(CB6Box r, double[] crop, double ins)
{
    if (crop == null) return r;
    double a = Math.Max(r.x0, crop[0] + ins), b = Math.Max(r.y0, crop[1] + ins), c = Math.Min(r.x1, crop[2] - ins), d = Math.Min(r.y1, crop[3] - ins);
    if (c - a < 0.1 || d - b < 0.1) return null;
    return new CB6Box(a, b, c, d, r.id);
}
public class T8VP
{
    public long id; public View view; public string name = "", sheetNo = "-", err = null, why = "", rep = "", planErr = null;
    public long sheetId = -1, alreadyId = 0; public bool visible = false, already = false, want = false;
    public int scale = 96; public XYZ R, U; public double zPl = 0, PI = 8, margin, clr, inClr, gapReq, maxDim;
    public double[] TG, crop = null; public CB6Clu clu = null;
    public List<CB6Box> obs = new List<CB6Box>(), hard = new List<CB6Box>();
}
// view checks (range, workset, category, crop, sheet) + cloud input (rects, obstacles, other clouds) in view (u,v) coordinates
public static T8VP T8PlanView(Document doc, long vid, List<XYZ> rp, List<XYZ> sp, double rOut, Revision rev, double zMin, double zMax, int wsId, double spIn)
{
    T8VP p = new T8VP(); p.id = vid;
    View v = doc.GetElement(new ElementId(vid)) as View;
    ViewPlan vpl = v as ViewPlan;
    if (vpl == null || v.IsTemplate) { p.err = "view " + vid + " missing or not a plan view"; return p; }
    p.view = v; p.name = v.Name; p.scale = v.Scale; p.R = v.RightDirection; p.U = v.UpDirection;
    if (Math.Abs(p.R.Z) > 1e-6 || Math.Abs(p.U.Z) > 1e-6) { p.err = "view " + vid + " is not a horizontal plan"; return p; }
    p.zPl = v.GenLevel != null ? v.GenLevel.ProjectElevation : 0;
    p.PI = p.scale / 12.0; p.margin = 0.25 * p.PI + 0.05;
    double clrIn = Math.Max(0.10, Math.Min(0.25, 0.4 * spIn));
    p.clr = clrIn * p.PI; p.inClr = 0.03 * p.PI; p.gapReq = Math.Max(0.06, 0.5 * spIn + 0.03) * p.PI; p.maxDim = 15.0 * p.PI;
    double tL = 0.13 * p.PI, tR = 0.13 * p.PI, tB = 0.10 * p.PI, tA = 0.10 * p.PI;
    p.TG = new double[] { 0.17 * p.PI, Math.Max(0.08 * p.PI, p.gapReq + tB), tL, tR, tB, tA };
    foreach (Element e in new FilteredElementCollector(doc).OfClass(typeof(Viewport)))
    {
        Viewport vp = e as Viewport;
        if (vp != null && vp.ViewId.Value == vid) { p.sheetId = vp.SheetId.Value; ViewSheet sh = doc.GetElement(vp.SheetId) as ViewSheet; if (sh != null) p.sheetNo = sh.SheetNumber; break; }
    }
    PlanViewRange vr = vpl.GetViewRange();
    double zCut = T8Plane(doc, vr, PlanViewPlane.CutPlane), zDep = T8Plane(doc, vr, PlanViewPlane.ViewDepthPlane);
    List<string> why = new List<string>();
    if (!double.IsNaN(zCut) && zMin > zCut) why.Add("route above cut plane " + T8F(zCut));
    if (!double.IsNaN(zDep) && zMax < zDep) why.Add("route below view depth " + T8F(zDep));
    if (doc.IsWorkshared && !v.IsWorksetVisible(new WorksetId(wsId))) why.Add("workset " + wsId + " hidden");
    if (v.GetCategoryHidden(new ElementId(BuiltInCategory.OST_PipeCurves))) why.Add("Pipes category hidden");
    if (v.CropBoxActive)
    {
        BoundingBoxXYZ cb = v.CropBox; Transform tr = cb.Transform;
        double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
        double[] xs = new double[] { cb.Min.X, cb.Max.X, cb.Max.X, cb.Min.X }, ys = new double[] { cb.Min.Y, cb.Min.Y, cb.Max.Y, cb.Max.Y };
        for (int k = 0; k < 4; k++) { double[] uv = T8UV(tr.OfPoint(new XYZ(xs[k], ys[k], 0)), p.R, p.U); a = Math.Min(a, uv[0]); b = Math.Min(b, uv[1]); c = Math.Max(c, uv[0]); d = Math.Max(d, uv[1]); }
        p.crop = new double[] { a, b, c, d };
    }
    List<double[]> ruv = new List<double[]>(), suv = new List<double[]>();
    foreach (XYZ q in rp) { double[] uv = T8UV(q, p.R, p.U); if (p.crop == null || (uv[0] >= p.crop[0] && uv[0] <= p.crop[2] && uv[1] >= p.crop[1] && uv[1] <= p.crop[3])) ruv.Add(uv); }
    foreach (XYZ q in sp) { double[] uv = T8UV(q, p.R, p.U); if (p.crop == null || (uv[0] >= p.crop[0] && uv[0] <= p.crop[2] && uv[1] >= p.crop[1] && uv[1] <= p.crop[3])) suv.Add(uv); }
    if (ruv.Count == 0) why.Add("route outside crop");
    p.visible = why.Count == 0; p.why = string.Join("; ", why);
    p.rep = "view " + vid + " '" + p.name + "' sheet=" + p.sheetNo + " 1:" + p.scale + " cut=" + (double.IsNaN(zCut) ? "?" : T8F(zCut)) + " depth=" + (double.IsNaN(zDep) ? "?" : T8F(zDep))
        + " crop_in=" + ruv.Count + "/" + rp.Count + " visible=" + (p.visible ? "yes" : "no(" + p.why + ")");
    if (ruv.Count == 0) return p;
    CB6Clu cl = new CB6Clu(); cl.labels.Add(1);
    CB6Box rr = T8Clip(T8HullUV(ruv, rOut + p.margin, 1), p.crop, 0.1 * p.PI);
    if (rr == null) { p.rep += " route rect clipped away"; return p; }
    cl.rects.Add(rr);
    if (suv.Count > 0) { CB6Box sr = T8Clip(T8HullUV(suv, rOut + p.margin, 2), p.crop, 0.1 * p.PI); if (sr != null) cl.rects.Add(sr); }
    p.clu = cl;
    CB6Box near = CB6Ex(CB6Hull(cl.rects, 0), 15.0);
    foreach (Element e in new FilteredElementCollector(doc, v.Id).OfClass(typeof(RevisionCloud)))
    {
        RevisionCloud rc = e as RevisionCloud; if (rc == null) continue;
        List<double[]> sg = T8SegsUV(rc, p.R, p.U); if (sg.Count == 0) continue;
        double a = double.MaxValue, b = double.MaxValue, c = double.MinValue, d = double.MinValue;
        foreach (double[] s in sg) { a = Math.Min(a, Math.Min(s[0], s[2])); b = Math.Min(b, Math.Min(s[1], s[3])); c = Math.Max(c, Math.Max(s[0], s[2])); d = Math.Max(d, Math.Max(s[1], s[3])); }
        if (rc.RevisionId.Value == rev.Id.Value && !p.already)
        {
            bool all = true; foreach (double[] q in ruv) if (!T5In(q[0], q[1], sg)) { all = false; break; }
            if (all) { p.already = true; p.alreadyId = rc.Id.Value; }
        }
        p.hard.Add(new CB6Box(a, b, c, d, rc.Id.Value));
    }
    double big = 10.0 * p.PI;
    foreach (Element e in new FilteredElementCollector(doc, v.Id).OfClass(typeof(TextNote)))
    {
        CB6Box mb = CB6TextBox((TextNote)e, v, p.scale, doc);
        CB6Box ub = T8BoxUV(mb.x0, mb.y0, mb.x1, mb.y1, p.R, p.U, e.Id.Value);
        if (CB6Ov(ub, near) && ub.x1 - ub.x0 < big && ub.y1 - ub.y0 < big) p.obs.Add(ub);
    }
    List<Element> ann = new FilteredElementCollector(doc, v.Id).OfClass(typeof(IndependentTag)).ToList();
    ann.AddRange(new FilteredElementCollector(doc, v.Id).OfCategory(BuiltInCategory.OST_GenericAnnotation).WhereElementIsNotElementType().ToList());
    foreach (Element e in ann)
    {
        BoundingBoxXYZ bb = e.get_BoundingBox(v); if (bb == null) continue;
        CB6Box ub = T8BoxUV(bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y, p.R, p.U, e.Id.Value);
        if (CB6Ov(ub, near) && ub.x1 - ub.x0 < big && ub.y1 - ub.y0 < big) p.obs.Add(ub);
    }
    p.rep += " obstacles=" + p.obs.Count + " clouds_in_view=" + p.hard.Count;
    return p;
}
public static string T8PlanRep(T8VP p)
{
    CB6Clu c = p.clu; CB6Box hu = CB6Hull(c.rects, 0);
    double[] h = CB6TagHead(c.rects, p.TG[0], p.TG[1]); CB6Box tb = T5TagBox(h, p.TG);
    System.Text.StringBuilder s = new System.Text.StringBuilder();
    s.Append("cloud ").Append(c.mode).Append(" n=").Append(c.loop.Count).Append(" area=").Append(T8F(CB6Area(c.loop)))
     .Append(" hull_uv=(").Append(T8F(hu.x0)).Append(",").Append(T8F(hu.y0)).Append(",").Append(T8F(hu.x1)).Append(",").Append(T8F(hu.y1)).Append(")")
     .Append(" size_in=").Append(T8F((hu.x1 - hu.x0) / p.PI)).Append("x").Append(T8F((hu.y1 - hu.y0) / p.PI))
     .Append(" head_uv=(").Append(T8F(h[0])).Append(",").Append(T8F(h[1])).Append(") tag_gap=").Append(T8F(T5BoxSegs(tb, T5Segs(c.loop))));
    if (c.added.Count > 0) s.Append(" encloses=").Append(string.Join("/", c.added.Take(10)));
    return s.ToString();
}
// create one clockwise cloud + one Revision Tag (R25: head at right - 0.17 in, top + 0.08 in, raised so the box clears the arcs)
public static string T8MakeCloud(Document doc, T8VP p, Revision rev, FamilySymbol tagSym, out long cid, out long tid)
{
    CB6Clu c = p.clu; View v = p.view;
    if (CB6Area(c.loop) >= 0) throw new Exception("loop not clockwise");
    List<Curve> cv = new List<Curve>();
    for (int k = 0; k < c.loop.Count; k++)
    {
        double[] a = c.loop[k], b = c.loop[(k + 1) % c.loop.Count];
        cv.Add(Line.CreateBound(T8XYZ(a[0], a[1], p.R, p.U, p.zPl), T8XYZ(b[0], b[1], p.R, p.U, p.zPl)));
    }
    RevisionCloud rc = RevisionCloud.Create(doc, v, rev.Id, cv);
    cid = rc.Id.Value;
    if (!tagSym.IsActive) tagSym.Activate();
    double[] hd = CB6TagHead(c.rects, p.TG[0], p.TG[1]);
    IndependentTag tg = IndependentTag.Create(doc, tagSym.Id, v.Id, new Reference(rc), false, TagOrientation.Horizontal, T8XYZ(hd[0], hd[1], p.R, p.U, p.zPl));
    tid = tg.Id.Value;
    doc.Regenerate();
    double sk = CB6SketchArea(rc);
    if (sk >= 0) throw new Exception("sketch area " + T8F(sk) + " not clockwise");
    if (p.sheetId > 0)
    {
        bool on = false; foreach (ElementId sid in rc.GetSheetIds()) if (sid.Value == p.sheetId) on = true;
        if (!on) throw new Exception("cloud GetSheetIds does not include sheet " + p.sheetNo);
    }
    List<double[]> sg = T8SegsUV(rc, p.R, p.U);
    double top = hd[2], moved = 0;
    for (int pass = 0; pass < 2; pass++)
    {
        BoundingBoxXYZ tb = tg.get_BoundingBox(v);
        if (tb == null) throw new Exception("tag has no bbox");
        CB6Box tbx = T8BoxUV(tb.Min.X, tb.Min.Y, tb.Max.X, tb.Max.Y, p.R, p.U, tid);
        double gap = T5BoxSegs(tbx, sg);
        bool inside = T5In(0.5 * (tbx.x0 + tbx.x1), 0.5 * (tbx.y0 + tbx.y1), sg);
        if (!inside && gap >= p.gapReq - 0.005 && tbx.y0 >= top + p.gapReq - 0.005) break;
        if (pass == 1) throw new Exception("tag box still within " + T8F(gap) + " ft of the cloud (need " + T8F(p.gapReq) + ")");
        double up = (top + p.gapReq + 0.02) - tbx.y0;
        if (up <= 0) up = p.gapReq - gap + 0.02;
        if (up > 0.5 * p.PI) throw new Exception("tag would need to move up " + T8F(up) + " ft (> 0.5 in paper)");
        XYZ h0 = tg.TagHeadPosition;
        tg.TagHeadPosition = new XYZ(h0.X + p.U.X * up, h0.Y + p.U.Y * up, h0.Z);
        moved += up;
        doc.Regenerate();
    }
    BoundingBoxXYZ tbF = tg.get_BoundingBox(v);
    CB6Box tf = T8BoxUV(tbF.Min.X, tbF.Min.Y, tbF.Max.X, tbF.Max.Y, p.R, p.U, tid);
    foreach (CB6Box hb in p.hard) if (CB6Ov(CB6Ex(tf, p.clr), hb)) throw new Exception("tag overlaps existing cloud " + hb.id);
    foreach (CB6Box o in p.obs) if (CB6Ov(tf, o) && !CB6InU(o, c.rects)) throw new Exception("tag box overlaps " + o.id);
    XYZ hp = tg.TagHeadPosition; double[] huv = T8UV(hp, p.R, p.U);
    CB6Box hu = CB6Hull(c.rects, 0);
    return "cloud " + cid + " tag " + tid + " view " + p.id + " " + c.mode + " n=" + c.loop.Count + " area=" + T8F(sk)
        + " hull_uv=(" + T8F(hu.x0) + "," + T8F(hu.y0) + "," + T8F(hu.x1) + "," + T8F(hu.y1) + ") head_dx=" + T8F(huv[0] - hd[0] - p.TG[0])
        + " head_dy=" + T8F(huv[1] - top) + " tag_gap=" + T8F(tf.y0 - top) + " moved_up=" + T8F(moved)
        + (c.added.Count > 0 ? " encloses=" + string.Join("/", c.added.Take(10)) : "");
}
// ---- reused from CB6-R1-T5.cs (CB6 = T3 v4 cloud helpers, T5 = segment helpers)
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
public static double[] CB6TagHead(List<CB6Box> rs, double dx, double dy)
{
    double top = double.MinValue; foreach (CB6Box r in rs) top = Math.Max(top, r.y1);
    CB6Box best = null;
    foreach (CB6Box r in rs) if (Math.Abs(r.y1 - top) < 0.011 && (best == null || r.x1 > best.x1)) best = r;
    double hx = Math.Max(best.x1 - dx, 0.5 * (best.x0 + best.x1));
    return new double[] { hx, top + dy, top };
}
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
public static bool CB6Touch(CB6Clu a, CB6Clu b, double clr, double[] tg)
{
    foreach (CB6Box ra in a.rects) if (CB6OvU(CB6Ex(ra, 2 * clr), b.rects)) return true;
    CB6Box ta = T5TagBox(CB6TagHead(a.rects, tg[0], tg[1]), tg), tb = T5TagBox(CB6TagHead(b.rects, tg[0], tg[1]), tg);
    return CB6OvU(CB6Ex(ta, clr), b.rects) || CB6OvU(CB6Ex(tb, clr), a.rects) || CB6Ov(CB6Ex(ta, clr), tb);
}
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
                if (CB6OvU(CB6Ex(x, 2 * clr), c.rects) || CB6Ov(CB6Ex(x, clr), tb)) return "cloud would touch existing cloud " + x.id;
            foreach (CB6Box o in obs)
            {
                if (CB6InU(CB6Ex(o, inClr), c.rects)) continue;
                if (CB6OvU(CB6Ex(o, clr), c.rects) || CB6Ov(o, tb))
                {
                    c.rects.Add(CB6Ex(o, margin)); c.added.Add(o.id); ch = true;
                    if (c.added.Count > cap) return "cloud would enclose more than " + cap + " other items";
                    break;
                }
            }
            CB6Box hu = CB6Hull(c.rects, 0);
            if (hu.x1 - hu.x0 > maxDim || hu.y1 - hu.y0 > maxDim) return "cloud exceeds 15 in paper";
            List<double[]> lp = CB6Trace(c.rects);
            if (lp == null) { c.rects.Clear(); c.rects.Add(hu); c.hulled = true; ch = true; }
            else { c.loop = lp; c.mode = (c.hulled ? "hull-" : "") + (lp.Count == 4 ? "rect" : "outline"); }
        }
        if (!ch) return null;
    }
    return "cloud plan did not settle in 60 iterations";
}
public static List<double[]> T5Segs(List<double[]> loop)
{
    List<double[]> s = new List<double[]>();
    for (int k = 0; k < loop.Count; k++) { double[] a = loop[k], b = loop[(k + 1) % loop.Count]; s.Add(new double[] { a[0], a[1], b[0], b[1] }); }
    return s;
}
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
public static object DummyTail() { return null;
