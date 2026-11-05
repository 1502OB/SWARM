// T3 - body of: public static object Execute(Document document, object[] parameters)
// Run via send_code_to_revit, transactionMode "auto" (transaction already open; do not open one).
// Assumes usings: System, System.Collections.Generic, System.Globalization, System.Text, Autodesk.Revit.DB
const string TAG = "SWARM TEST";
System.Globalization.CultureInfo inv = System.Globalization.CultureInfo.InvariantCulture;

// 1. Guards: abort if a view or revision named/described "SWARM TEST" already exists
foreach (Autodesk.Revit.DB.Element e in new Autodesk.Revit.DB.FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.View)))
{
    if (e.Name == TAG)
        return "{\"error\":\"ABORT: a view named 'SWARM TEST' already exists (id " + e.Id.ToString() + "). Nothing created.\"}";
}
foreach (Autodesk.Revit.DB.ElementId rid in Autodesk.Revit.DB.Revision.GetAllRevisionIds(document))
{
    Autodesk.Revit.DB.Revision r = document.GetElement(rid) as Autodesk.Revit.DB.Revision;
    if (r != null && r.Description == TAG)
        return "{\"error\":\"ABORT: a revision with Description 'SWARM TEST' already exists (id " + rid.ToString() + "). Nothing created.\"}";
}

int warningsBefore = document.GetWarnings().Count;

// 2. Drafting view
Autodesk.Revit.DB.ViewFamilyType draftType = null;
foreach (Autodesk.Revit.DB.Element e in new Autodesk.Revit.DB.FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.ViewFamilyType)))
{
    Autodesk.Revit.DB.ViewFamilyType vft = e as Autodesk.Revit.DB.ViewFamilyType;
    if (vft != null && vft.ViewFamily == Autodesk.Revit.DB.ViewFamily.Drafting) { draftType = vft; break; }
}
if (draftType == null)
    return "{\"error\":\"ABORT: no ViewFamilyType with ViewFamily.Drafting found. Nothing created.\"}";

Autodesk.Revit.DB.ViewDrafting view = Autodesk.Revit.DB.ViewDrafting.Create(document, draftType.Id);
view.Name = TAG;

// 3. Text note at origin with default text note type
Autodesk.Revit.DB.ElementId textTypeId = document.GetDefaultElementTypeId(Autodesk.Revit.DB.ElementTypeGroup.TextNoteType);
if (textTypeId == null || textTypeId == Autodesk.Revit.DB.ElementId.InvalidElementId)
    throw new Exception("No default TextNoteType in document; aborting (transaction will roll back).");
Autodesk.Revit.DB.TextNote note = Autodesk.Revit.DB.TextNote.Create(document, view.Id, new Autodesk.Revit.DB.XYZ(0, 0, 0), TAG, textTypeId);

// 4. Temporary unissued revision
Autodesk.Revit.DB.Revision rev = Autodesk.Revit.DB.Revision.Create(document);
rev.Description = TAG;
rev.Issued = false;

// 5. Rectangle around note, margin 1.0 ft
document.Regenerate();
double minX, minY, maxX, maxY;
Autodesk.Revit.DB.BoundingBoxXYZ bb = note.get_BoundingBox(view);
bool usedFallback = false;
if (bb != null)
{
    double m = 1.0;
    minX = bb.Min.X - m; minY = bb.Min.Y - m;
    maxX = bb.Max.X + m; maxY = bb.Max.Y + m;
}
else
{
    usedFallback = true;
    minX = -2.0; maxX = 2.0; minY = -1.0; maxY = 1.0;
}

// CLOCKWISE: bottom-left > top-left > top-right > bottom-right > bottom-left
Autodesk.Revit.DB.XYZ bl = new Autodesk.Revit.DB.XYZ(minX, minY, 0);
Autodesk.Revit.DB.XYZ tl = new Autodesk.Revit.DB.XYZ(minX, maxY, 0);
Autodesk.Revit.DB.XYZ tr = new Autodesk.Revit.DB.XYZ(maxX, maxY, 0);
Autodesk.Revit.DB.XYZ br = new Autodesk.Revit.DB.XYZ(maxX, minY, 0);
List<Autodesk.Revit.DB.Curve> curves = new List<Autodesk.Revit.DB.Curve>();
curves.Add(Autodesk.Revit.DB.Line.CreateBound(bl, tl));
curves.Add(Autodesk.Revit.DB.Line.CreateBound(tl, tr));
curves.Add(Autodesk.Revit.DB.Line.CreateBound(tr, br));
curves.Add(Autodesk.Revit.DB.Line.CreateBound(br, bl));

Autodesk.Revit.DB.RevisionCloud cloud = Autodesk.Revit.DB.RevisionCloud.Create(document, view, rev.Id, curves);

// 6. Compact JSON result
Autodesk.Revit.DB.XYZ[] pts = new Autodesk.Revit.DB.XYZ[] { bl, tl, tr, br, bl };
System.Text.StringBuilder sb = new System.Text.StringBuilder();
sb.Append("{\"view_id\":").Append(view.Id.ToString());
sb.Append(",\"text_note_id\":").Append(note.Id.ToString());
sb.Append(",\"revision_id\":").Append(rev.Id.ToString());
sb.Append(",\"cloud_id\":").Append(cloud.Id.ToString());
sb.Append(",\"loop\":[");
for (int i = 0; i < pts.Length; i++)
{
    if (i > 0) sb.Append(",");
    sb.Append("[").Append(Math.Round(pts[i].X, 4).ToString(inv)).Append(",").Append(Math.Round(pts[i].Y, 4).ToString(inv)).Append("]");
}
sb.Append("]");
sb.Append(",\"bbox_fallback\":").Append(usedFallback ? "true" : "false");
sb.Append(",\"warnings_before\":").Append(warningsBefore.ToString(inv));
sb.Append("}");
return sb.ToString();
