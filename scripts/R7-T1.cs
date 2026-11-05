// operation: hide-category-in-view (OST_Sprinklers), dryrun supported, idempotent
bool dry = parameters != null && parameters.Length > 0 && parameters.Any(p => p != null && p.ToString().ToLower() == "dryrun");
long[] ids = new long[] { 18215302, 18215641, 18216028, 18216239, 15476126, 22821417, 22821494, 22821504, 22821517, 22821532 };
long linkInst = 22841688;
Category cat = document.Settings.Categories.get_Item(BuiltInCategory.OST_Sprinklers);
if (cat == null) throw new Exception("Sprinklers category not found");
var sb = new System.Text.StringBuilder();
var changed = new List<string>();
var skipped = new List<string>();
var blocked = new List<string>();
foreach (long id in ids)
{
    View v = document.GetElement(new ElementId(id)) as View;
    if (v == null) throw new Exception("ROLLED_BACK " + id + ": not a view");
    if (v.ViewType != ViewType.FloorPlan) throw new Exception("ROLLED_BACK " + id + ": not floor plan, " + v.ViewType);
    ElementId tid = v.ViewTemplateId;
    bool hasTpl = tid != ElementId.InvalidElementId;
    bool tplControls = false;
    if (hasTpl)
    {
        View t = document.GetElement(tid) as View;
        // template controls category V/G unless the VG overrides parameter is excluded
        tplControls = true;
        try
        {
            var nc = t.GetNonControlledTemplateParameterIds();
            if (nc.Contains(new ElementId(BuiltInParameter.VIS_GRAPHICS_MODEL))) tplControls = false;
        }
        catch { }
    }
    bool canHide = v.CanCategoryBeHidden(cat.Id);
    bool hidden = v.GetCategoryHidden(cat.Id);
    sb.Append(id + ":" + v.Name + " tpl=" + tid.Value + " ctl=" + tplControls + " canHide=" + canHide + " hidden=" + hidden + "; ");
    if (tplControls) { blocked.Add(id + " template " + tid.Value); continue; }
    if (!canHide) { blocked.Add(id + " cannot hide"); continue; }
    if (hidden) { skipped.Add(id.ToString()); continue; }
    if (!dry)
    {
        v.SetCategoryHidden(cat.Id, true);
        changed.Add(id.ToString());
    }
    else changed.Add(id.ToString());
}
// link override report on 18215302
string linkRep = "";
try
{
    View v0 = document.GetElement(new ElementId(18215302)) as View;
    var li = document.GetElement(new ElementId(linkInst));
    if (li == null) linkRep = "link instance missing";
    else
    {
        var s = v0.GetLinkOverrides(li.Id);
        var props = new List<string>();
        foreach (var pi in s.GetType().GetProperties())
        {
            string val;
            try { val = Convert.ToString(pi.GetValue(s, null)); } catch { val = "?"; }
            props.Add(pi.Name + "=" + val);
        }
        linkRep = string.Join(",", props);
    }
}
catch (Exception ex) { linkRep = "link report failed: " + ex.Message; }
string manual = "manual: if the Custom override model categories on 22841688 in view 18215302 is not By Host View, set Sprinklers hidden there in V/G > Revit Links > Custom (API cannot set per-category link overrides); verify heads gone on F-P1.01";
if (document.GetElement(new ElementId(linkInst)) != null)
{
    try
    {
        var li2 = document.GetElement(new ElementId(linkInst));
        var s2 = (document.GetElement(new ElementId(18215302)) as View).GetLinkOverrides(li2.Id);
        if (Convert.ToString(s2.LinkVisibilityType) == "ByHostView") manual = "none";
    }
    catch { }
}
string res = (dry ? "DRYRUN " : "") + "changed=" + string.Join(",", changed) + " skipped=" + string.Join(",", skipped) + " blocked=" + string.Join(",", blocked) + " | " + sb + " | linkOverride(18215302,22841688): " + linkRep + " | " + manual;
if (res.Length > 2800) res = res.Substring(0, 2800);
return res;
