// cleanup for R58-T1: restores 'Sheet Issue Date'. parameters = strings "<oldvalue>|id,id,..." (the OLD groups from the run; "<empty>" = empty string)
const string PNAME = "Sheet Issue Date";
int restored = 0; var bad = new List<string>();
foreach (var o in parameters)
{
    string item = o.ToString();
    foreach (var part in item.Split(new[] { ";;" }, StringSplitOptions.RemoveEmptyEntries))
    {
        string t = part.Trim();
        int bar = t.LastIndexOf('|');
        if (bar < 0) { bad.Add(t); continue; }
        string val = t.Substring(0, bar); if (val == "<empty>") val = "";
        foreach (var ids in t.Substring(bar + 1).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            var s = document.GetElement(new ElementId(long.Parse(ids.Trim()))) as ViewSheet;
            if (s == null) { bad.Add(ids); continue; }
            Parameter p = s.LookupParameter(PNAME);
            if (p == null || p.IsReadOnly) { bad.Add(ids); continue; }
            if ((p.AsString() ?? "") != val) p.Set(val);
            restored++;
        }
    }
}
return "restored=" + restored + " problems=" + string.Join(",", bad.Take(10));
