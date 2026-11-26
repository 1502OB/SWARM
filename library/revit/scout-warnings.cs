// swarm-lib scout-warnings v1 | transactionMode "none" (read-only) | library/revit/scout-warnings.cs, 2026-11-25
// Purpose: the model's warning count (Document.GetWarnings: no 500 cap, unlike get_warnings, R52) and the counts
//   grouped by description, optionally only the warnings that touch given element ids (before/after a write).
// Send: send_code_to_revit {codeFile: "C:/Users/j/swarm/library/revit/scout-warnings.cs",
//   expectMarker: "// swarm-lib scout-warnings v1", transactionMode: "none",
//   parameters: ["tag=<task-id> s3", "title=<substring>", "top=15", "ids=123,456"]}
// Parameters (strings "key=value", any order, keys case-insensitive; unknown keys ignored):
//   tag=<text>     required; echoed first on line 1 (R88)
//   title=<text>   optional; Document.Title must contain it (case-insensitive) or nothing is read (failed=1)
//   top=<n>        default 15; at most n description groups, largest count first (ties: description A-Z)
//   ids=<a,b,..>   optional element-id filter (comma or space separated): only warnings whose failing OR additional
//                  elements include one of these ids are grouped; per_id then counts the warnings per given id
//   sample=<n>     default 3; failing-element ids listed per group (0 = none)
//   max=<chars>    default 2300; whole-return budget (bridge truncates at ~3 KB, R59); groups that do not fit are
//                  dropped from the end and truncated=true
//   strmax=<n>     default 140; longer descriptions are cut to n chars and end with "~"
// Return: line 1 "<tag> SCOUT-WARNINGS v1 done=<groups listed> skipped=<groups not listed> failed=<0|1>"
//         line 2 JSON object:
//   marker "SCOUT-WARNINGS v1", tag string, doc string (Document.Title)
//   total         int   every warning in the model (unfiltered)
//   matched       int   warnings counted in groups (= total without ids=)
//   groups_total  int   distinct descriptions among the matched warnings
//   groups        array {"desc": string GetDescriptionText, "n": int, "sev": string FailureSeverity,
//                        "ids": long[] first <sample> failing element ids of that group (in warning order)}
//   by_severity   object {"<FailureSeverity>": int} over the matched warnings
//   filter_ids    long[]|absent   the parsed ids= list (absent without ids=)
//   per_id        array|absent    {"id": long, "n": int warnings touching that id} for each filter id, given order
//   bad_ids       string[]|absent ids= tokens that are not integers
//   truncated     bool  true when fewer than min(top, groups_total) groups are listed because of max
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
const string MARK = "SCOUT-WARNINGS v1";
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
    int top = Math.Max(1, IntArg("top", 15));
    int sample = Math.Max(0, IntArg("sample", 3));
    int max = Math.Max(400, IntArg("max", 2300));
    int strmax = Math.Max(16, IntArg("strmax", 140));

    stage = "ids";
    var filter = new List<long>();
    var bad = new List<string>();
    foreach (string t in Arg("ids", "").Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries))
    {
        long n;
        if (long.TryParse(t.Trim(), out n)) { if (!filter.Contains(n)) filter.Add(n); }
        else bad.Add(t.Trim());
    }
    var fset = new HashSet<long>(filter);
    var perId = filter.ToDictionary(x => x, x => 0);

    stage = "warnings";
    IList<FailureMessage> ws = document.GetWarnings();
    var groups = new Dictionary<string, List<FailureMessage>>();
    var gFirst = new Dictionary<string, List<long>>();
    var bySev = new SortedDictionary<string, int>();
    int matched = 0;
    foreach (FailureMessage w in ws)
    {
        var failing = w.GetFailingElements().Select(x => x.Value).ToList();
        if (fset.Count > 0)
        {
            var touched = failing.Concat(w.GetAdditionalElements().Select(x => x.Value)).Where(fset.Contains).Distinct().ToList();
            if (touched.Count == 0) continue;
            foreach (long t in touched) perId[t]++;
        }
        matched++;
        string d = w.GetDescriptionText() ?? "";
        if (!groups.ContainsKey(d)) { groups[d] = new List<FailureMessage>(); gFirst[d] = new List<long>(); }
        groups[d].Add(w);
        foreach (long f in failing) if (gFirst[d].Count < sample) gFirst[d].Add(f);
        string sv = w.GetSeverity().ToString();
        bySev[sv] = bySev.ContainsKey(sv) ? bySev[sv] + 1 : 1;
    }

    stage = "page";
    string head = "{\"marker\":" + Q(MARK) + ",\"tag\":" + Q(tag) + ",\"doc\":" + Q(document.Title) + ",\"total\":" + ws.Count
        + ",\"matched\":" + matched + ",\"groups_total\":" + groups.Count
        + ",\"by_severity\":{" + string.Join(",", bySev.Select(kv => Q(kv.Key) + ":" + kv.Value)) + "}"
        + (fset.Count > 0 ? ",\"filter_ids\":[" + string.Join(",", filter.Take(50)) + "]"
            + ",\"per_id\":[" + string.Join(",", filter.Take(50).Select(x => "{\"id\":" + x + ",\"n\":" + perId[x] + "}")) + "]" : "")
        + (bad.Count > 0 ? ",\"bad_ids\":[" + string.Join(",", bad.Take(10).Select(Q)) + "]" : "");
    int used = tag.Length + MARK.Length + 60 + head.Length + 40;
    var outG = new List<string>();
    var ordered = groups.OrderByDescending(kv => kv.Value.Count).ThenBy(kv => kv.Key, StringComparer.Ordinal).Take(top).ToList();
    bool trunc = false;
    foreach (var kv in ordered)
    {
        string d = kv.Key.Length > strmax ? kv.Key.Substring(0, strmax) + "~" : kv.Key;
        string item = "{\"desc\":" + Q(d) + ",\"n\":" + kv.Value.Count + ",\"sev\":" + Q(kv.Value[0].GetSeverity().ToString())
            + ",\"ids\":[" + string.Join(",", gFirst[kv.Key]) + "]}";
        if (used + item.Length + 1 > max) { trunc = true; break; }
        outG.Add(item);
        used += item.Length + 1;
    }

    stage = "json";
    string json = head + ",\"groups\":[" + string.Join(",", outG) + "],\"truncated\":" + (trunc ? "true" : "false") + "}";
    return tag + " " + MARK + " done=" + outG.Count + " skipped=" + (groups.Count - outG.Count) + " failed=0\n" + json;
}
catch (Exception ex)
{
    return Fail(stage + ": " + ex.GetType().Name + ": " + ex.Message);
}
