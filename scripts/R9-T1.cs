// operation: delete-text-note | R9-T1 | run 20261104-1905
// parameters: ["dryrun"] = read-only preflight; otherwise deletes. transactionMode "auto" (no own transaction).
bool dry = parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString() == "dryrun";
if (document.Title.IndexOf("PLMB-24220-R25", StringComparison.OrdinalIgnoreCase) < 0)
    throw new Exception("wrong document open: " + document.Title);
long viewIdV = 22821417; long noteIdV = 22821493;
string expText = "APPROXIMATE LOCATION OF CIVIL SEWAGE PUMP STATION";
ElementId viewId = new ElementId(viewIdV);
View view = document.GetElement(viewId) as View;
if (view == null) throw new Exception("ROLLED_BACK R9-T1: view 22821417 not found");

var notesInView = new FilteredElementCollector(document, viewId).OfClass(typeof(TextNote)).Cast<TextNote>().ToList();
Func<TextNote, string> norm = t => System.Text.RegularExpressions.Regex.Replace(t.Text ?? "", @"\s+", " ").Trim().ToUpperInvariant();
var matches = notesInView.Where(t => norm(t).Contains("SEWAGE PUMP STATION")).ToList();

Element target = document.GetElement(new ElementId(noteIdV));
string status;
if (target == null)
{
    if (matches.Count > 0)
        throw new Exception("ROLLED_BACK R9-T1: id 22821493 gone but similar note exists: " + matches[0].Id.Value);
    return "skipped: already in target state (note 22821493 absent, no SEWAGE PUMP STATION note in view) view=" + view.Name;
}
TextNote tn = target as TextNote;
if (tn == null) throw new Exception("ROLLED_BACK R9-T1: 22821493 is not a TextNote");
if (tn.OwnerViewId != viewId) throw new Exception("ROLLED_BACK R9-T1: note not owned by view 22821417");
if (norm(tn) != expText) throw new Exception("ROLLED_BACK R9-T1: text mismatch: '" + tn.Text + "'");
if (matches.Count != 1) throw new Exception("ROLLED_BACK R9-T1: expected 1 match in view, found " + matches.Count);

int before = notesInView.Count;
if (dry)
    return "DRYRUN ok: would delete TextNote 22821493 '" + tn.Text + "' in view '" + view.Name + "'; textnotes in view=" + before;

var del = document.Delete(tn.Id);
int remain = new FilteredElementCollector(document, viewId).OfClass(typeof(TextNote)).Cast<TextNote>()
    .Count(t => norm(t).Contains("SEWAGE PUMP STATION"));
return "deleted ids=" + string.Join(",", del.Select(i => i.Value.ToString())) + "; notes in view before=" + before + "; remaining SEWAGE matches (pre-commit)=" + remain;
