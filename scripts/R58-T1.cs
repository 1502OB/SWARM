// operation: set-sheet-parameter | R58-T1 | sets sheet text parameter 'Sheet Issue Date' on F-P*/D-P* sheets; parameters ["dryrun"] = read-only preflight
const string PNAME = "Sheet Issue Date";
const string NEWVAL = "DD 11.05.26";
bool dry = parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString() == "dryrun";

var sheets = new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).Cast<ViewSheet>()
    .Where(s => s.SheetNumber.StartsWith("F-P", StringComparison.Ordinal) || s.SheetNumber.StartsWith("D-P", StringComparison.Ordinal))
    .OrderBy(s => s.SheetNumber).ToList();
if (sheets.Count != 44) throw new Exception("ABORT: sheet count " + sheets.Count + " != 44");

var groups = new Dictionary<string, List<long>>();
int already = 0;
var problems = new List<string>();
foreach (var s in sheets)
{
    Parameter p = s.LookupParameter(PNAME);
    if (p == null) { problems.Add(s.SheetNumber + ": parameter missing"); continue; }
    if (p.IsReadOnly) { problems.Add(s.SheetNumber + ": read-only"); continue; }
    if (p.StorageType != StorageType.String) { problems.Add(s.SheetNumber + ": storage " + p.StorageType); continue; }
    string old = p.AsString() ?? "";
    if (old == NEWVAL) already++;
    List<long> l;
    if (!groups.TryGetValue(old, out l)) { l = new List<long>(); groups[old] = l; }
    l.Add(s.Id.Value);
}
if (problems.Count > 0) throw new Exception("ABORT preflight: " + string.Join("; ", problems.Take(10)) + " (n=" + problems.Count + ")");

// grouped old values: "<old>|id,id,..." (empty old shown as <empty>); pass these strings to cleanup
string grouped = string.Join(" ;; ", groups.Select(g => (g.Key == "" ? "<empty>" : g.Key) + "|" + string.Join(",", g.Value)));
string counts = string.Join(", ", groups.Select(g => (g.Key == "" ? "<empty>" : g.Key) + " x" + g.Value.Count));

if (dry)
    return "DRYRUN count=44 values: " + counts + "; already=" + already + "; storage=String readOnly=false on all; OLD: " + grouped;

int changed = 0;
foreach (var s in sheets)
{
    Parameter p = s.LookupParameter(PNAME);
    if ((p.AsString() ?? "") == NEWVAL) continue;
    if (!p.Set(NEWVAL)) throw new Exception("ROLLED_BACK " + s.SheetNumber + ": Set failed");
    changed++;
}
return "OK changed=" + changed + " skipped(already in target state)=" + already + " total=44; OLD: " + grouped;
