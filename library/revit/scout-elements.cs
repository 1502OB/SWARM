// swarm-lib scout-elements v1 | transactionMode "none" (read-only) | library/revit/scout-elements.cs, 2026-11-25
// Purpose: the targeted scout / per-task baseline (O34, R89): for the given element ids only, the facts a scripter
//   and verifier need: category, family, type, level, workset, checkout status + owner, key location, requested
//   parameter values, optionally connectors (R123). No collectors: every element is fetched by id.
// Send: send_code_to_revit {codeFile: "C:/Users/j/swarm/library/revit/scout-elements.cs",
//   expectMarker: "// swarm-lib scout-elements v1", transactionMode: "none",
//   parameters: ["tag=<task-id> s4", "title=<substring>", "ids=123,456", "params=Size|System Type|Comments"]}
// Parameters (strings "key=value", any order, keys case-insensitive; unknown keys ignored):
//   tag=<text>      required; echoed first on line 1 (R88)
//   title=<text>    optional; Document.Title must contain it (case-insensitive) or nothing is read (failed=1)
//   ids=<a,b,..>    required; element ids (comma, space or ';' separated), reported in the given order
//   params=<A|B>    optional; parameter names separated by '|' (exact, case-sensitive, as LookupParameter). Looked up
//                   on the instance first, then on its type
//   raw=1           Double parameters report the internal value (feet, radians, ...) as "<number>" instead of the
//                   display string AsValueString (default 0)
//   conn=1          also list connectors of MEP curves / MEP family instances (default 0)
//   offset=<n>      default 0; index into the ids list of the first element to report (paging)
//   max=<chars>     default 2300; whole-return budget (bridge truncates at ~3 KB, R59); elements that do not fit are
//                   left for the next call: truncated=true, next=<offset to send>
//   strmax=<n>      default 80; longer string values are cut to n chars and end with "~"
// Return: line 1 "<tag> SCOUT-ELEMENTS v1 done=<elements listed> skipped=<found ids left for later pages>
//                 failed=<ids not found + bad tokens>"   (failed counts missing targets; a script error is failed=1 + error)
//         line 2 JSON object:
//   marker "SCOUT-ELEMENTS v1", tag string, doc string (Document.Title), is_workshared bool, user string (Username)
//   requested int (parsed ids), found int, offset int
//   elements  array, one per found id from offset:
//     {"id": long, "cat": string|null Category.Name, "cls": string .NET class (Pipe, FamilyInstance, TextNote ...),
//      "fam": string|null family name, "type": string|null type name, "lvl": string|null level name
//      (LevelId, else MEPCurve.ReferenceLevel, else the reference/schedule level parameter),
//      "ws": string|null workset name (workshared only), "co": string|null CheckoutStatus
//      (OwnedByCurrentUser | OwnedByOtherUser | NotOwned; null when not workshared), "owner": string|null,
//      "view": long|absent OwnerViewId for view-specific elements,
//      "loc": {"pt":[x,y,z]} LocationPoint | {"p0":[x,y,z],"p1":[x,y,z]} LocationCurve end points |
//             {"bb":[minx,miny,minz,maxx,maxy,maxz]} model bounding box | null; feet, internal coordinates, 4 dp,
//      "p":  {"<name>": string|null}  instance parameter values (null = parameter exists but has no value),
//      "pt": {"<name>": string|null}  values found on the TYPE (absent when none),
//      "pm": string[]                 requested names found on neither (absent when none),
//      "conn": [{"i": int Connector.Id, "dia": number|null inches (round), "dir": [x,y,z] BasisZ (2 dp),
//                "to": long[] owner ids of connected connectors, [] = free}]   (only with conn=1)}
//   missing  long[]    ids not in the document (capped at 50)
//   bad_ids  string[]  tokens that are not integers (absent when none)
//   truncated bool, next int|null
//   On a script error: line 1 "... done=0 skipped=0 failed=1", line 2 {"marker","tag","error":"<stage>: <Type>: <msg>"}
var P = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
if (parameters != null)
    foreach (var o in parameters)
    {
        string s = Convert.ToString(o) ?? "";
        int k = s.IndexOf('=');
        if (k > 0) P[s.Substring(0, k).Trim()] = s.Substring(k + 1).Trim();
    }
string Arg(string key, string def) => P.TryGetValue(key, out var v) && v.Length > 0 ? v : def;
int IntArg(string key, int def) { int v; return int.TryParse(Arg(key, ""), out v) ? v : def; }
string Q(string s)
{
    if (s == null) return "null";
    var sb = new System.Text.StringBuilder("\"");
    foreach (char c in s)
    {
        if (c == '"') sb.Append("\\\"");
        else if (c == '\\') sb.Append("\\\\");
        else if (c == '\n') sb.Append("\\n");
        else if (c == '\r') sb.Append("\\r");
        else if (c == '\t') sb.Append("\\t");
        else if (c < 0x20) sb.Append("\\u" + ((int)c).ToString("x4"));
        else sb.Append(c);
    }
    return sb.Append('"').ToString();
}
string N(double d, int dp) => Math.Round(d, dp).ToString(System.Globalization.CultureInfo.InvariantCulture);
string Pt(XYZ p) => "[" + N(p.X, 4) + "," + N(p.Y, 4) + "," + N(p.Z, 4) + "]";
const string MARK = "SCOUT-ELEMENTS v1";
string tag = Arg("tag", "untagged");
string Fail(string msg) => tag + " " + MARK + " done=0 skipped=0 failed=1\n{\"marker\":" + Q(MARK) + ",\"tag\":" + Q(tag)
    + ",\"error\":" + Q(msg) + "}";
string stage = "start";
try
{
    stage = "document guard";
    string want = Arg("title", "");
    if (want.Length > 0 && (document.Title ?? "").IndexOf(want, StringComparison.OrdinalIgnoreCase) < 0)
        return Fail("wrong document: active is " + document.Title + ", expected title containing " + want);
    int offset = Math.Max(0, IntArg("offset", 0));
    int max = Math.Max(400, IntArg("max", 2300));
    int strmax = Math.Max(8, IntArg("strmax", 80));
    bool raw = Arg("raw", "0") == "1";
    bool conn = Arg("conn", "0") == "1";
    string Cut(string s) => s == null ? null : (s.Length > strmax ? s.Substring(0, strmax) + "~" : s);
    var pnames = Arg("params", "").Split('|').Select(x => x.Trim()).Where(x => x.Length > 0).Distinct().Take(30).ToList();

    stage = "ids";
    var ids = new List<long>();
    var bad = new List<string>();
    foreach (string t in Arg("ids", "").Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries))
    {
        long n;
        if (long.TryParse(t.Trim(), out n)) { if (!ids.Contains(n)) ids.Add(n); }
        else bad.Add(t.Trim());
    }
    if (ids.Count == 0) return Fail("ids= is required (comma-separated element ids)");
    var found = new List<Element>();
    var missing = new List<long>();
    foreach (long n in ids)
    {
        Element e = document.GetElement(new ElementId(n));
        if (e == null) missing.Add(n); else found.Add(e);
    }
    bool shared = document.IsWorkshared;
    WorksetTable wst = shared ? document.GetWorksetTable() : null;

    string PVal(Parameter p)
    {
        if (p == null || !p.HasValue) return "null";
        switch (p.StorageType)
        {
            case StorageType.String: return Q(Cut(p.AsString()));
            case StorageType.Double: return raw ? Q(N(p.AsDouble(), 6)) : Q(Cut(p.AsValueString() ?? N(p.AsDouble(), 6)));
            case StorageType.Integer: return Q(Cut(p.AsValueString() ?? p.AsInteger().ToString()));
            case StorageType.ElementId:
                ElementId pid = p.AsElementId();
                string vs = p.AsValueString();
                return Q(Cut(string.IsNullOrEmpty(vs) ? pid.Value.ToString() : vs + " [" + pid.Value + "]"));
            default: return "null";
        }
    }
    string LevelName(Element e)
    {
        ElementId lid = e.LevelId;
        if ((lid == null || lid == ElementId.InvalidElementId) && e is MEPCurve mc && mc.ReferenceLevel != null) return mc.ReferenceLevel.Name;
        if (lid == null || lid == ElementId.InvalidElementId)
        {
            foreach (var bip in new[] { BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM, BuiltInParameter.FAMILY_LEVEL_PARAM,
                                        BuiltInParameter.SCHEDULE_LEVEL_PARAM, BuiltInParameter.RBS_START_LEVEL_PARAM })
            {
                Parameter lp = e.get_Parameter(bip);
                if (lp != null && lp.StorageType == StorageType.ElementId && lp.AsElementId() != ElementId.InvalidElementId)
                { lid = lp.AsElementId(); break; }
            }
        }
        if (lid == null || lid == ElementId.InvalidElementId) return null;
        return document.GetElement(lid)?.Name;
    }
    string Conns(Element e)
    {
        ConnectorManager cm = null;
        if (e is MEPCurve mc) cm = mc.ConnectorManager;
        else if (e is FamilyInstance fi && fi.MEPModel != null) cm = fi.MEPModel.ConnectorManager;
        if (cm == null) return "[]";
        var parts = new List<string>();
        foreach (Connector c in cm.Connectors)
        {
            if (parts.Count >= 8) break;
            string dia = "null";
            try { if (c.Shape == ConnectorProfileType.Round) dia = N(c.Radius * 24.0, 3); } catch (Exception) { }
            string dir = "null";
            try { XYZ z = c.CoordinateSystem.BasisZ; dir = "[" + N(z.X, 2) + "," + N(z.Y, 2) + "," + N(z.Z, 2) + "]"; } catch (Exception) { }
            var to = new List<long>();
            try
            {
                if (c.IsConnected)
                    foreach (Connector r in c.AllRefs)
                        if (r.Owner != null && r.Owner.Id != e.Id && (r.ConnectorType == ConnectorType.End
                            || r.ConnectorType == ConnectorType.Curve || r.ConnectorType == ConnectorType.Physical)
                            && !to.Contains(r.Owner.Id.Value)) to.Add(r.Owner.Id.Value);
            }
            catch (Exception) { }
            parts.Add("{\"i\":" + c.Id + ",\"dia\":" + dia + ",\"dir\":" + dir + ",\"to\":[" + string.Join(",", to.Take(6)) + "]}");
        }
        return "[" + string.Join(",", parts) + "]";
    }

    stage = "head";
    string head = "{\"marker\":" + Q(MARK) + ",\"tag\":" + Q(tag) + ",\"doc\":" + Q(document.Title)
        + ",\"is_workshared\":" + (shared ? "true" : "false") + ",\"user\":" + Q(document.Application.Username)
        + ",\"requested\":" + ids.Count + ",\"found\":" + found.Count + ",\"offset\":" + offset
        + ",\"missing\":[" + string.Join(",", missing.Take(50)) + "]"
        + (bad.Count > 0 ? ",\"bad_ids\":[" + string.Join(",", bad.Take(10).Select(Q)) + "]" : "");
    int used = tag.Length + MARK.Length + 60 + head.Length + 60;
    var items = new List<string>();
    bool trunc = false;
    int next = -1;
    for (int i = offset; i < found.Count; i++)
    {
        Element e = found[i];
        stage = "element " + e.Id.Value;
        var sb = new System.Text.StringBuilder();
        sb.Append("{\"id\":").Append(e.Id.Value).Append(",\"cat\":").Append(Q(e.Category?.Name)).Append(",\"cls\":").Append(Q(e.GetType().Name));
        ElementType et = e as ElementType ?? document.GetElement(e.GetTypeId()) as ElementType;
        sb.Append(",\"fam\":").Append(Q(Cut(et?.FamilyName))).Append(",\"type\":").Append(Q(Cut(et?.Name)));
        sb.Append(",\"lvl\":").Append(Q(LevelName(e)));
        if (shared)
        {
            string owner;
            CheckoutStatus cs = WorksharingUtils.GetCheckoutStatus(document, e.Id, out owner);
            sb.Append(",\"ws\":").Append(Q(wst.GetWorkset(e.WorksetId)?.Name)).Append(",\"co\":").Append(Q(cs.ToString()))
              .Append(",\"owner\":").Append(Q(string.IsNullOrEmpty(owner) ? null : owner));
        }
        else sb.Append(",\"ws\":null,\"co\":null,\"owner\":null");
        if (e.OwnerViewId != null && e.OwnerViewId != ElementId.InvalidElementId) sb.Append(",\"view\":").Append(e.OwnerViewId.Value);
        string loc = "null";
        if (e.Location is LocationPoint lpt) loc = "{\"pt\":" + Pt(lpt.Point) + "}";
        else if (e.Location is LocationCurve lc && lc.Curve != null && lc.Curve.IsBound)
            loc = "{\"p0\":" + Pt(lc.Curve.GetEndPoint(0)) + ",\"p1\":" + Pt(lc.Curve.GetEndPoint(1)) + "}";
        else
        {
            BoundingBoxXYZ bb = e.get_BoundingBox(null);
            if (bb != null) loc = "{\"bb\":[" + Pt(bb.Min).Trim('[', ']') + "," + Pt(bb.Max).Trim('[', ']') + "]}";
        }
        sb.Append(",\"loc\":").Append(loc);
        if (pnames.Count > 0)
        {
            var pi = new List<string>();
            var ptl = new List<string>();
            var pm = new List<string>();
            foreach (string pn in pnames)
            {
                Parameter p = e.LookupParameter(pn);
                if (p != null) { pi.Add(Q(pn) + ":" + PVal(p)); continue; }
                Parameter tp = et != null && !(e is ElementType) ? et.LookupParameter(pn) : null;
                if (tp != null) ptl.Add(Q(pn) + ":" + PVal(tp)); else pm.Add(Q(pn));
            }
            sb.Append(",\"p\":{").Append(string.Join(",", pi)).Append("}");
            if (ptl.Count > 0) sb.Append(",\"pt\":{").Append(string.Join(",", ptl)).Append("}");
            if (pm.Count > 0) sb.Append(",\"pm\":[").Append(string.Join(",", pm)).Append("]");
        }
        else sb.Append(",\"p\":{}");
        if (conn) sb.Append(",\"conn\":").Append(Conns(e));
        sb.Append("}");
        string item = sb.ToString();
        if (used + item.Length + 1 > max) { trunc = true; next = i; break; }
        items.Add(item);
        used += item.Length + 1;
    }

    stage = "json";
    int skipped = trunc ? found.Count - next : 0;
    string json = head + ",\"elements\":[" + string.Join(",", items) + "],\"truncated\":" + (trunc ? "true" : "false")
        + ",\"next\":" + (next < 0 ? "null" : next.ToString()) + "}";
    return tag + " " + MARK + " done=" + items.Count + " skipped=" + skipped + " failed=" + (missing.Count + bad.Count) + "\n" + json;
}
catch (Exception ex)
{
    return Fail(stage + ": " + ex.GetType().Name + ": " + ex.Message);
}
