// operation: delete-text-note | R4-T1 | run 20261104-1905
// parameters: ["dryrun"] = read-only preflight; otherwise deletes. transactionMode "auto" (no own transaction).
bool dry = parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString() == "dryrun";
if (document.Title.IndexOf("PLMB-24220-R25", StringComparison.OrdinalIgnoreCase) < 0)
    throw new Exception("wrong document open: " + document.Title);
string expText = "SLEEPY HOLLOW LANE";
long[][] pairs = new long[][] { new long[] { 20332878, 15476126 }, new long[] { 22821542, 22821532 } };
Func<TextNote, string> norm = t => System.Text.RegularExpressions.Regex.Replace(t.Text ?? "", @"\s+", " ").Trim().ToUpperInvariant();

var toDelete = new List<ElementId>();
var report = new List<string>();
// preflight both first: any mismatch throws before anything is deleted
foreach (var p in pairs)
{
    ElementId viewId = new ElementId(p[1]);
    View view = document.GetElement(viewId) as View;
    if (view == null) throw new Exception("ROLLED_BACK R4-T1: view " + p[1] + " not found");
    var matches = new FilteredElementCollector(document, viewId).OfClass(typeof(TextNote)).Cast<TextNote>()
        .Where(t => norm(t).Contains(expText)).ToList();
    Element e = document.GetElement(new ElementId(p[0]));
    if (e == null)
    {
        if (matches.Count > 0)
            throw new Exception("ROLLED_BACK R4-T1: id " + p[0] + " gone but similar note " + matches[0].Id.Value + " in view " + p[1]);
        report.Add(p[0] + ": skipped: already in target state");
        continue;
    }
    TextNote tn = e as TextNote;
    if (tn == null) throw new Exception("ROLLED_BACK R4-T1: " + p[0] + " is not a TextNote");
    if (tn.OwnerViewId != viewId) throw new Exception("ROLLED_BACK R4-T1: " + p[0] + " not owned by view " + p[1]);
    if (norm(tn) != expText) throw new Exception("ROLLED_BACK R4-T1: " + p[0] + " text mismatch: '" + tn.Text + "'");
    if (matches.Count != 1) throw new Exception("ROLLED_BACK R4-T1: view " + p[1] + " has " + matches.Count + " SLEEPY HOLLOW LANE notes");
    toDelete.Add(tn.Id);
    report.Add(p[0] + ": " + (dry ? "would delete" : "delete") + " in '" + view.Name + "'");
}
if (dry) return "DRYRUN ok: " + string.Join(" | ", report);
foreach (var id in toDelete) document.Delete(id);
return "done: " + string.Join(" | ", report);
