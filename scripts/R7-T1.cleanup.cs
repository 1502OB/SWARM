// cleanup for R7-T1: parameters = string view ids that the run changed ("changed=" list)
Category cat = document.Settings.Categories.get_Item(BuiltInCategory.OST_Sprinklers);
var done = new List<string>();
foreach (object p in parameters)
{
    long id;
    if (!long.TryParse(Convert.ToString(p), out id)) continue;
    View v = document.GetElement(new ElementId(id)) as View;
    if (v == null) continue;
    if (v.GetCategoryHidden(cat.Id)) { v.SetCategoryHidden(cat.Id, false); done.Add(id.ToString()); }
}
return "unhidden Sprinklers in views: " + string.Join(",", done);
