// CSCHECK-T1: live parity test for tools/revit_cs_check.py (2026-11-11). READ-ONLY: no model change in any mode.
// transactionMode "none" (no transaction is opened at all). parameters: ["dryrun"] = report only (the only mode used);
// any other run returns NO_GO without touching the model. Works on whatever model is active and reports which one.
// Exercises the compile surface the bridge must agree with the offline check on: namespaces the wrapper does NOT
// import (Plumbing, Mechanical, Architecture), Autodesk.Revit.UI types (imported), LINQ, tuples, interpolation,
// a local function, and helper methods after the body (the DummyTail pattern).
bool dry = parameters != null && parameters.Length > 0 &&
           string.Equals(Convert.ToString(parameters[0]), "dryrun", StringComparison.OrdinalIgnoreCase);
if (document == null || document.Title == null) return "NO_GO no active document\ndone=0 skipped=0 failed=1";
if (!dry) return "NO_GO CSCHECK-T1 is a dry-run-only test; send [\"dryrun\"]\ndone=0 skipped=0 failed=1";

int Count<T>() where T : Element => new FilteredElementCollector(document).OfClass(typeof(T)).GetElementCount();

var pipes = new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.Plumbing.Pipe))
    .Cast<Autodesk.Revit.DB.Plumbing.Pipe>().ToList();
var bySystem = pipes
    .Select(p => (sys: p.MEPSystem?.Name ?? "(none)", len: p.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH)?.AsDouble() ?? 0.0))
    .GroupBy(x => x.sys)
    .Select(g => (name: g.Key, n: g.Count(), ft: g.Sum(x => x.len)))
    .OrderByDescending(g => g.n).Take(5).ToList();
int fittings = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipeFitting)
    .WhereElementIsNotElementType().GetElementCount();
int rooms = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Rooms)
    .WhereElementIsNotElementType().OfType<Autodesk.Revit.DB.Architecture.Room>().Count();
int mechEq = new FilteredElementCollector(document).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>()
    .Count(fi => fi.MEPModel is Autodesk.Revit.DB.Mechanical.MechanicalEquipment || MechanicalFittingLike());
int sheets = Count<ViewSheet>();
int revisions = Revision.GetAllRevisionIds(document).Count;
TaskDialogResult unused = TaskDialogResult.Ok; // Autodesk.Revit.UI is imported by the bridge wrapper (never shown)

var sb = new System.Text.StringBuilder();
sb.Append($"DRYRUN CSCHECK-T1 doc='{document.Title}' workshared={document.IsWorkshared} modifiable={document.IsModifiable}\n");
sb.Append($"pipes={pipes.Count} fittings={fittings} rooms={rooms} sheets={sheets} revisions={revisions} mechFamilies={mechEq}\n");
foreach (var g in bySystem) sb.Append($"  system '{Short(g.name)}': {g.n} pipes, {g.ft:F1} ft\n");
sb.Append($"api={document.Application.VersionNumber} build={document.Application.VersionBuild}\n");
sb.Append("would change: nothing (read-only test)\ndone=0 skipped=0 failed=0");
return sb.ToString();
}

static bool MechanicalFittingLike() => false;  // helper after the body (always false: keeps the type test compiling)

static string Short(string s) => s == null ? "" : (s.Length <= 40 ? s : s.Substring(0, 40) + "...");

public static object DummyTail() { return null;
