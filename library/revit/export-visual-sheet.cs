// Export one sheet using Document.Export and its true paper size. transactionMode "none".
// parameters: ["dryrun"|"run", request ID, exact document title, stable document ID, sheet number, absolute PDF path].
string requestId = parameters != null && parameters.Length > 1 ? Convert.ToString(parameters[1]) : "";
string Failed(string message) => requestId + " done=0 skipped=0 failed=1 " + message;
if (parameters == null || parameters.Length != 6 || string.IsNullOrWhiteSpace(requestId))
    return Failed("expected phase, request id, document title and ID, sheet number, and PDF path");
string phase = Convert.ToString(parameters[0]);
bool dry = phase == "dryrun";
if (!dry && phase != "run") return Failed("invalid phase");
if (!string.Equals(document.Title, Convert.ToString(parameters[2]), StringComparison.OrdinalIgnoreCase))
    return Failed("unexpected active document " + document.Title);

string number = Convert.ToString(parameters[4]);
string path = Convert.ToString(parameters[5]);
try
{
    string documentId = document.PathName;
    if (document.IsModelInCloud)
    {
        var model = document.GetCloudModelPath();
        documentId = "cloud:" + model.GetProjectGUID() + "/" + model.GetModelGUID();
    }
    else if (document.IsWorkshared)
    {
        var central = document.GetWorksharingCentralModelPath();
        if (central != null) documentId = ModelPathUtils.ConvertModelPathToUserVisiblePath(central);
    }
    string expectedId = Convert.ToString(parameters[3]);
    if (string.IsNullOrWhiteSpace(expectedId) || !string.Equals(documentId.Replace('\\', '/'),
            expectedId.Replace('\\', '/'), StringComparison.OrdinalIgnoreCase))
        return Failed("active document ID differs from the approved export");

    if (!System.IO.Path.IsPathFullyQualified(path) ||
        !string.Equals(System.IO.Path.GetExtension(path), ".pdf", StringComparison.OrdinalIgnoreCase))
        return Failed("expected an absolute PDF path");
    string folder = System.IO.Path.GetDirectoryName(path);
    if (!System.IO.Directory.Exists(folder) || System.IO.File.Exists(path))
        return Failed("export directory missing or destination already exists");
    var sheets = new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).Cast<ViewSheet>()
        .Where(s => !s.IsPlaceholder && string.Equals(s.SheetNumber, number, StringComparison.OrdinalIgnoreCase))
        .Take(2).ToList();
    if (sheets.Count != 1) return Failed("sheet number did not resolve uniquely: " + number);
    if (dry) return requestId + " done=0 skipped=0 failed=0 DRYRUN sheet=" + number + " path=" + path;

    var options = new PDFExportOptions();
    options.Combine = true;
    options.FileName = System.IO.Path.GetFileNameWithoutExtension(path);
    options.PaperFormat = ExportPaperFormat.Default;
    options.ZoomType = ZoomType.Zoom;
    options.ZoomPercentage = 100;
    options.HideCropBoundaries = true;
    options.HideScopeBoxes = true;
    options.HideReferencePlane = true;
    options.HideUnreferencedViewTags = true;
    options.StopOnError = true;
    if (!document.Export(folder, new List<ElementId> { sheets[0].Id }, options) || !System.IO.File.Exists(path))
        return Failed("expected PDF was not exported: " + path);
    long bytes = new System.IO.FileInfo(path).Length;
    if (bytes == 0) return Failed("exported PDF is empty: " + path);
    return requestId + " done=1 skipped=0 failed=0 sheet=" + number + " bytes=" + bytes + " path=" + path;
}
catch (Exception ex)
{
    return Failed(ex.GetType().Name + ": " + ex.Message);
}
