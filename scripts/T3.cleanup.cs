// T3 cleanup - body of: public static object Execute(Document document, object[] parameters)
// Run via send_code_to_revit, transactionMode "auto".
// parameters = [view_id, text_note_id, cloud_id, revision_id] as strings (Revit 2024: ElementId(long) ctor).
if (parameters == null || parameters.Length < 4)
    return "{\"error\":\"expected 4 parameters: view_id, text_note_id, cloud_id, revision_id\"}";

string[] labels = new string[] { "view", "text_note", "cloud", "revision" };
Autodesk.Revit.DB.ElementId[] ids = new Autodesk.Revit.DB.ElementId[4];
for (int i = 0; i < 4; i++)
    ids[i] = new Autodesk.Revit.DB.ElementId(System.Convert.ToInt64(parameters[i].ToString().Trim()));

// Delete order: cloud(2), text_note(1), view(0), revision(3)
int[] order = new int[] { 2, 1, 0, 3 };
System.Text.StringBuilder deleted = new System.Text.StringBuilder();
System.Text.StringBuilder missing = new System.Text.StringBuilder();
System.Text.StringBuilder errors = new System.Text.StringBuilder();
foreach (int k in order)
{
    Autodesk.Revit.DB.Element el = document.GetElement(ids[k]);
    string entry = "{\"" + labels[k] + "\":" + ids[k].ToString() + "}";
    if (el == null)
    {
        if (missing.Length > 0) missing.Append(",");
        missing.Append(entry);
        continue;
    }
    try
    {
        document.Delete(ids[k]);
        if (deleted.Length > 0) deleted.Append(",");
        deleted.Append(entry);
    }
    catch (Exception ex)
    {
        if (errors.Length > 0) errors.Append(",");
        errors.Append("{\"" + labels[k] + "\":" + ids[k].ToString() + ",\"msg\":\"" + ex.Message.Replace("\\", "\\\\").Replace("\"", "'") + "\"}");
    }
}
return "{\"deleted\":[" + deleted.ToString() + "],\"missing\":[" + missing.ToString() + "],\"errors\":[" + errors.ToString() + "],\"warnings_after\":" + document.GetWarnings().Count.ToString() + "}";
