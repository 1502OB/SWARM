// transactionMode "none" (read-only). library/revit/model-to-sheet.cs, 2026-11-17 (LESSONS R82; retro/20261116-1839.md P4).
// Maps a model / view-plane point to its position on the sheet that hosts the view, the way that worked on
// WF-R1-T1.v2 / WF-R1-T2 (25-061). Copy the ToSheet lambda into a task script; this file runs it standalone as a probe.
//   sheet = vp.GetBoxCenter() + ((P - cropCentre) . view.Right, (P - cropCentre) . view.Up) / view.Scale
// Never Viewport.GetProjectionToSheetTransform on model coordinates: it returned ~(147, 209) ft on WF (twice).
// The result is asserted inside the viewport outline (+tol); a point outside it is reported, never used.
// parameters: [mode, viewportId, x, y, z]   mode "dryrun" or "run" (both read-only); x, y, z in feet (internal).
// Return: done=/skipped=/failed= first, then one line per check (R59: tail-first, well under 2.3 KB).
string mode = parameters != null && parameters.Length > 0 ? Convert.ToString(parameters[0]) : "dryrun";
if (parameters == null || parameters.Length < 5)
    return "done=0 skipped=0 failed=1\nFAILED model-to-sheet: parameters [mode, viewportId, x, y, z] required";
Viewport vp = document.GetElement(new ElementId(Convert.ToInt64(parameters[1]))) as Viewport;
if (vp == null) return "done=0 skipped=0 failed=1\nFAILED model-to-sheet: " + parameters[1] + " is not a Viewport";
View vw = document.GetElement(vp.ViewId) as View;
XYZ P = new XYZ(Convert.ToDouble(parameters[2]), Convert.ToDouble(parameters[3]), Convert.ToDouble(parameters[4]));

Func<View, Viewport, XYZ, double[]> ToSheet = (v, port, pt) =>
{
    XYZ R = v.RightDirection, U = v.UpDirection;
    double scale = Math.Max(1, v.Scale);
    BoundingBoxXYZ cb = v.CropBox;                          // crop centre in model coordinates
    XYZ cc = cb.Transform.OfPoint((cb.Min + cb.Max) * 0.5);
    XYZ d = pt - cc;
    XYZ bc = port.GetBoxCenter();                           // viewport box centre on the sheet (= crop centre)
    return new double[] { bc.X + d.DotProduct(R) / scale, bc.Y + d.DotProduct(U) / scale };
};

double[] s = ToSheet(vw, vp, P);
Outline ol = vp.GetBoxOutline();
double tol = 0.05; // ft on paper: the outline includes the crop, not annotation outside it
bool inside = s[0] >= ol.MinimumPoint.X - tol && s[0] <= ol.MaximumPoint.X + tol
           && s[1] >= ol.MinimumPoint.Y - tol && s[1] <= ol.MaximumPoint.Y + tol;
string warn = "";
if (vp.Rotation != ViewportRotation.None) warn += " viewport rotated " + vp.Rotation + " (mapping assumes none)";
if (!vw.CropBoxActive) warn += " crop inactive (box centre may not be the crop centre)";
return (inside ? "done=1 skipped=0 failed=0" : "done=0 skipped=0 failed=1")
    + "\ndoc=" + document.Title + " mode=" + mode
    + "\nsheet=" + vp.SheetId.Value + " view=" + vw.Name + " scale=" + vw.Scale
    + "\nmapping=center+delta/scale sheet_pt=(" + s[0].ToString("F4") + "," + s[1].ToString("F4") + ")"
    + "\noutline=(" + ol.MinimumPoint.X.ToString("F4") + "," + ol.MinimumPoint.Y.ToString("F4") + ")-("
    + ol.MaximumPoint.X.ToString("F4") + "," + ol.MaximumPoint.Y.ToString("F4") + ") inside=" + inside
    + (warn.Length > 0 ? "\nwarn:" + warn : "");
