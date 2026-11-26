// swarm-lib scout-doc v1 | transactionMode "none" (read-only) | library/revit/scout-doc.cs, 2026-11-25
// Purpose: the bridge-check-first call (MARKUP 20a, LESSONS O37): which document is the bridge on, is it the intended
//   model, and what state is it in. One cheap call, no collectors.
// Send: send_code_to_revit {codeFile: "C:/Users/j/swarm/library/revit/scout-doc.cs",
//   expectMarker: "// swarm-lib scout-doc v1", transactionMode: "none", parameters: ["tag=<task-id> s1", "title=<substring>"]}
// Parameters (strings "key=value", any order, keys case-insensitive; unknown keys ignored):
//   tag=<text>     required; echoed first on line 1 so a crossed/lagging reply is recognisable (R88)
//   title=<text>   optional; case-insensitive substring Document.Title must contain. Mismatch -> failed=1, title_ok false
//                  (the full JSON is still returned so the orchestrator sees which model is active)
// Return (2 lines, whole return well under 2.3 KB):
//   line 1: "<tag> SCOUT-DOC v1 done=<0|1> skipped=0 failed=<0|1>"   (done=1 = read OK and title matched or not given)
//   line 2: JSON object
//     marker       string  "SCOUT-DOC v1"
//     tag          string
//     title        string  Document.Title
//     path         string  Document.PathName ("" for an unsaved / cloud model without a local path)
//     doc_id       string  stable identity: "cloud:<projectGuid>/<modelGuid>" | central path (workshared) | PathName
//     is_cloud     bool    IsModelInCloud
//     is_workshared bool
//     central      string|null  user-visible central model path (workshared only)
//     is_detached  bool
//     is_modified  bool    IsModified (unsaved changes)
//     is_readonly  bool    IsReadOnly
//     is_family    bool    IsFamilyDocument
//     user         string  Application.Username (the name GetCheckoutStatus owners are compared with)
//     revit        object  {"version": VersionNumber, "name": VersionName, "build": VersionBuild, "sub": SubVersionNumber}
//     active_view  object|null {"id": long, "name": string, "type": ViewType string, "sheet": SheetNumber|null}
//     open_docs    int     number of documents open in this Revit session (links included)
//     open_titles  string[] up to 8 of their titles (non-link documents first)
//     title_ok     bool|null  null when title= was not given
//   On a script error: line 1 "<tag> SCOUT-DOC v1 done=0 skipped=0 failed=1", line 2 {"marker","tag","error":"<stage>: <Type>: <msg>"}
var P = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
if (parameters != null)
    foreach (var o in parameters)
    {
        string s = Convert.ToString(o) ?? "";
        int k = s.IndexOf('=');
        if (k > 0) P[s.Substring(0, k).Trim()] = s.Substring(k + 1).Trim();
    }
string Arg(string key, string def) => P.TryGetValue(key, out var v) && v.Length > 0 ? v : def;
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
string B(bool b) => b ? "true" : "false";
const string MARK = "SCOUT-DOC v1";
string tag = Arg("tag", "untagged");
string stage = "start";
try
{
    stage = "identity";
    string title = document.Title ?? "";
    string path = document.PathName ?? "";
    string central = null;
    string docId = path;
    if (document.IsModelInCloud)
    {
        var cm = document.GetCloudModelPath();
        docId = "cloud:" + cm.GetProjectGUID() + "/" + cm.GetModelGUID();
    }
    else if (document.IsWorkshared)
    {
        var cp = document.GetWorksharingCentralModelPath();
        if (cp != null)
        {
            central = ModelPathUtils.ConvertModelPathToUserVisiblePath(cp);
            docId = central;
        }
    }
    string want = Arg("title", "");
    bool? titleOk = want.Length == 0 ? (bool?)null : title.IndexOf(want, StringComparison.OrdinalIgnoreCase) >= 0;

    stage = "application";
    var app = document.Application;
    stage = "active view";
    string av = "null";
    View v = document.ActiveView;
    if (v != null)
    {
        var vs = v as ViewSheet;
        av = "{\"id\":" + v.Id.Value + ",\"name\":" + Q(v.Name) + ",\"type\":" + Q(v.ViewType.ToString())
           + ",\"sheet\":" + (vs != null ? Q(vs.SheetNumber) : "null") + "}";
    }
    stage = "open documents";
    var docs = new List<Document>();
    foreach (Document d in app.Documents) docs.Add(d);
    var titles = docs.OrderBy(d => d.IsLinked ? 1 : 0).Take(8).Select(d => Q(d.Title));

    stage = "json";
    string json = "{\"marker\":" + Q(MARK) + ",\"tag\":" + Q(tag) + ",\"title\":" + Q(title) + ",\"path\":" + Q(path)
        + ",\"doc_id\":" + Q(docId) + ",\"is_cloud\":" + B(document.IsModelInCloud)
        + ",\"is_workshared\":" + B(document.IsWorkshared) + ",\"central\":" + Q(central)
        + ",\"is_detached\":" + B(document.IsDetached) + ",\"is_modified\":" + B(document.IsModified)
        + ",\"is_readonly\":" + B(document.IsReadOnly) + ",\"is_family\":" + B(document.IsFamilyDocument)
        + ",\"user\":" + Q(app.Username)
        + ",\"revit\":{\"version\":" + Q(app.VersionNumber) + ",\"name\":" + Q(app.VersionName)
        + ",\"build\":" + Q(app.VersionBuild) + ",\"sub\":" + Q(app.SubVersionNumber) + "}"
        + ",\"active_view\":" + av + ",\"open_docs\":" + docs.Count
        + ",\"open_titles\":[" + string.Join(",", titles) + "]"
        + ",\"title_ok\":" + (titleOk == null ? "null" : B(titleOk.Value)) + "}";
    bool ok = titleOk != false;
    return tag + " " + MARK + " done=" + (ok ? 1 : 0) + " skipped=0 failed=" + (ok ? 0 : 1) + "\n" + json;
}
catch (Exception ex)
{
    return tag + " " + MARK + " done=0 skipped=0 failed=1\n{\"marker\":" + Q(MARK) + ",\"tag\":" + Q(tag)
        + ",\"error\":" + Q(stage + ": " + ex.GetType().Name + ": " + ex.Message) + "}";
}
