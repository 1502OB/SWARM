// swarm-lib scout-sheets-revisions v1 | transactionMode "none" (read-only) | library/revit/scout-sheets-revisions.cs, 2026-11-25
// Purpose: every sheet with the revisions on it (snapshot-taker step 3, revit-scout sheet lookups) plus the project
//   revision list, in one call per page. Replaces the hand-written GetAllRevisionIds dump.
// Send: send_code_to_revit {codeFile: "C:/Users/j/swarm/library/revit/scout-sheets-revisions.cs",
//   expectMarker: "// swarm-lib scout-sheets-revisions v1", transactionMode: "none",
//   parameters: ["tag=<task-id> s2", "title=<substring>", "offset=0"]}
// Parameters (strings "key=value", any order, keys case-insensitive; unknown keys ignored):
//   tag=<text>      required; echoed first on line 1 (R88)
//   title=<text>    optional; Document.Title must contain it (case-insensitive) or nothing is read (failed=1)
//   part=all|sheets|revisions   default all. all = project revisions (only on offset 0) + sheets from offset.
//                   sheets = sheets only. revisions = project revisions only, paged by offset.
//   offset=<n>      default 0; index of the first item of the paged list (sheets, or revisions for part=revisions)
//   numbers=<a,b>   optional; only sheets whose SheetNumber is in this comma list (exact, case-insensitive)
//   placeholders=1  include placeholder sheets (default 0: skipped and counted in sheets_placeholder)
//   max=<chars>     default 2300; whole-return budget (the bridge truncates at ~3 KB, R59). Items that do not fit are
//                   left for the next page: truncated=true and next=<offset to send>.
//   strmax=<n>      default 80; longer names/descriptions are cut to n chars and end with "~"
// Return: line 1 "<tag> SCOUT-SHEETS v1 done=<items in this page> skipped=<items left for later pages> failed=<0|1>"
//         line 2 JSON object:
//   marker "SCOUT-SHEETS v1", tag string, doc string (Document.Title), part string, offset int
//   revisions        array|absent  present for part=all on offset 0, and part=revisions. Sorted by sequence:
//                    {"id": long, "seq": int SequenceNumber, "no": string RevisionNumber, "desc": string,
//                     "date": string RevisionDate, "issued": bool}
//   revisions_total  int   project revision count (always present)
//   revisions_truncated bool  part=all only: true when not every revision fit (then call part=revisions)
//   sheets           array|absent  (absent for part=revisions). Sorted by SheetNumber (ordinal, case-insensitive):
//                    {"id": long, "no": string SheetNumber, "name": string,
//                     "revs": [{"id": long revision id, "no": string GetRevisionNumberOnSheet}]  GetAllRevisionIds
//                              order (sequence); ids refer to the revisions list,
//                     "cur": long|null GetCurrentRevision}
//   sheets_total     int   sheets after the numbers= filter and placeholder rule (the paged list size for sheets)
//   sheets_placeholder int placeholder sheets skipped (0 when placeholders=1)
//   numbers_missing  string[]  numbers= entries that matched no sheet (absent without numbers=)
//   truncated        bool  next  int|null  (null = the paged list is complete through this page)
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
const string MARK = "SCOUT-SHEETS v1";
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
    string part = Arg("part", "all").ToLowerInvariant();
    if (part != "all" && part != "sheets" && part != "revisions") return Fail("part must be all, sheets or revisions");
    int offset = Math.Max(0, IntArg("offset", 0));
    int max = Math.Max(400, IntArg("max", 2300));
    int strmax = Math.Max(8, IntArg("strmax", 80));
    bool withPh = Arg("placeholders", "0") == "1";
    string Cut(string s) => s == null ? null : (s.Length > strmax ? s.Substring(0, strmax) + "~" : s);
    long Lid(ElementId id) => id == null ? -1 : id.Value;

    stage = "revisions";
    var revs = new FilteredElementCollector(document).OfClass(typeof(Revision)).Cast<Revision>()
        .OrderBy(r => r.SequenceNumber).ToList();
    var revItems = revs.Select(r => "{\"id\":" + r.Id.Value + ",\"seq\":" + r.SequenceNumber + ",\"no\":" + Q(r.RevisionNumber)
        + ",\"desc\":" + Q(Cut(r.Description)) + ",\"date\":" + Q(Cut(r.RevisionDate)) + ",\"issued\":"
        + (r.Issued ? "true" : "false") + "}").ToList();

    stage = "sheets";
    var all = new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).Cast<ViewSheet>().ToList();
    int phCount = withPh ? 0 : all.Count(s => s.IsPlaceholder);
    var sheets = all.Where(s => withPh || !s.IsPlaceholder).ToList();
    string numsArg = Arg("numbers", "");
    List<string> missing = null;
    if (numsArg.Length > 0)
    {
        var want2 = numsArg.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList();
        var set = new HashSet<string>(want2, StringComparer.OrdinalIgnoreCase);
        sheets = sheets.Where(s => set.Contains(s.SheetNumber ?? "")).ToList();
        var have = new HashSet<string>(sheets.Select(s => s.SheetNumber ?? ""), StringComparer.OrdinalIgnoreCase);
        missing = want2.Where(x => !have.Contains(x)).ToList();
    }
    sheets = sheets.OrderBy(s => s.SheetNumber ?? "", StringComparer.OrdinalIgnoreCase).ToList();

    stage = "page";
    string head = "{\"marker\":" + Q(MARK) + ",\"tag\":" + Q(tag) + ",\"doc\":" + Q(document.Title) + ",\"part\":" + Q(part)
        + ",\"offset\":" + offset + ",\"revisions_total\":" + revs.Count + ",\"sheets_total\":" + sheets.Count
        + ",\"sheets_placeholder\":" + phCount
        + (missing != null ? ",\"numbers_missing\":[" + string.Join(",", missing.Take(20).Select(Q)) + "]" : "");
    int used = tag.Length + MARK.Length + 60 + head.Length + 90;   // line 1 + head + tail reserve
    var outRevs = new List<string>();
    var outSheets = new List<string>();
    bool revTrunc = false, trunc = false;
    int next = -1, done = 0, skipped = 0;

    if (part == "revisions" || (part == "all" && offset == 0))
    {
        int start = part == "revisions" ? offset : 0;
        used += 16;
        for (int i = start; i < revItems.Count; i++)
        {
            if (used + revItems[i].Length + 1 > max)
            {
                revTrunc = true;
                if (part == "revisions") { trunc = true; next = i; skipped = revItems.Count - i; }
                break;
            }
            outRevs.Add(revItems[i]);
            used += revItems[i].Length + 1;
            if (part == "revisions") done++;
        }
    }
    if (part != "revisions")
    {
        used += 14;
        for (int i = offset; i < sheets.Count; i++)
        {
            ViewSheet s = sheets[i];
            var ids = s.GetAllRevisionIds();
            string sr = string.Join(",", ids.Select(r => "{\"id\":" + r.Value + ",\"no\":" + Q(s.GetRevisionNumberOnSheet(r)) + "}"));
            ElementId cur = s.GetCurrentRevision();
            string item = "{\"id\":" + s.Id.Value + ",\"no\":" + Q(s.SheetNumber) + ",\"name\":" + Q(Cut(s.Name))
                + ",\"revs\":[" + sr + "],\"cur\":" + (cur == null || cur == ElementId.InvalidElementId ? "null" : Lid(cur).ToString()) + "}";
            if (used + item.Length + 1 > max) { trunc = true; next = i; skipped = sheets.Count - i; break; }
            outSheets.Add(item);
            used += item.Length + 1;
            done++;
        }
    }

    stage = "json";
    string json = head
        + (part == "revisions" || (part == "all" && offset == 0) ? ",\"revisions\":[" + string.Join(",", outRevs) + "]" : "")
        + (part == "all" ? ",\"revisions_truncated\":" + (revTrunc ? "true" : "false") : "")
        + (part != "revisions" ? ",\"sheets\":[" + string.Join(",", outSheets) + "]" : "")
        + ",\"truncated\":" + (trunc ? "true" : "false") + ",\"next\":" + (next < 0 ? "null" : next.ToString()) + "}";
    return tag + " " + MARK + " done=" + done + " skipped=" + skipped + " failed=0\n" + json;
}
catch (Exception ex)
{
    return Fail(stage + ": " + ex.GetType().Name + ": " + ex.Message);
}
