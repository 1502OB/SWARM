// create-revision: add a new Revision by description/date; skips numbering seq when source is none (-1)
// operation: create-revision (v2). CB6-R1-T1: create revision "FEWD Submittal" 10/26/2026. parameters: ["dryrun"] = read-only preflight.
// v2: each step in its own try with a label; never reads RevisionNumber; skips numbering sequence when source is InvalidElementId;
// on any step failure deletes the new revision inside the same transaction and returns ROLLED_BACK with the inner exception text.
bool dry = parameters != null && parameters.Length > 0 && string.Equals(Convert.ToString(parameters[0]), "dryrun", StringComparison.OrdinalIgnoreCase);
if (document.Title == null || document.Title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0)
    return "NO_GO wrong document open: " + document.Title;
const string DESC = "FEWD Submittal";
const string DATE = "10/26/2026";

var ids = Revision.GetAllRevisionIds(document);
Revision last = null, existing = null;
foreach (ElementId id in ids)
{
    Revision r = document.GetElement(id) as Revision;
    if (r == null) continue;
    last = r;
    if (existing == null && r.Description == DESC) existing = r;
}
if (last == null) return "NO_GO no revisions found in model";

if (existing != null)
    return "skipped: already in target state. revId=" + existing.Id.Value + " seq=" + existing.SequenceNumber +
           " date=" + existing.RevisionDate + " issued=" + existing.Issued + "\ndone=0 skipped=1 failed=0";

ElementId lastSeqId = last.RevisionNumberingSequenceId;
string pre = "last revision id=" + last.Id.Value + " seq=" + last.SequenceNumber + " desc='" + last.Description +
             "' date=" + last.RevisionDate + " numberingSeq=" + lastSeqId.Value + " total=" + ids.Count;
if (dry)
    return "DRYRUN would create Revision '" + DESC + "' date " + DATE + " Issued=false Visibility=CloudAndTagVisible; " + pre +
           "\ndone=0 skipped=0 failed=0";

string step = "Revision.Create";
Revision nr = null;
ElementId nrId = null;
try
{
    nr = Revision.Create(document);
    nrId = nr.Id;
    step = "set Description"; nr.Description = DESC;
    step = "set RevisionDate"; nr.RevisionDate = DATE;
    step = "set Issued"; nr.Issued = false;
    step = "set Visibility"; nr.Visibility = RevisionVisibility.CloudAndTagVisible;
    step = "set RevisionNumberingSequenceId";
    if (lastSeqId != null && lastSeqId != ElementId.InvalidElementId && nr.RevisionNumberingSequenceId != lastSeqId)
        nr.RevisionNumberingSequenceId = lastSeqId;
}
catch (Exception ex)
{
    string msg = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
    string del = "";
    try { if (nrId != null && document.GetElement(nrId) != null) { document.Delete(nrId); del = " (new revision deleted)"; } }
    catch (Exception ex2) { del = " (delete of new revision FAILED: " + ex2.Message + ")"; }
    return "ROLLED_BACK failed at step '" + step + "': " + ex.GetType().Name + ": " + msg + del + "; " + pre + "\ndone=0 skipped=0 failed=1";
}

string back;
try
{
    back = "created revId=" + nr.Id.Value + " seq=" + nr.SequenceNumber + " desc='" + nr.Description +
           "' date=" + nr.RevisionDate + " issued=" + nr.Issued + " vis=" + nr.Visibility +
           " numberingSeq=" + nr.RevisionNumberingSequenceId.Value;
}
catch (Exception ex) { back = "created revId=" + nr.Id.Value + " (readback failed: " + ex.Message + ")"; }
return back + "; " + pre + "\ndone=1 skipped=0 failed=0";
