// add-shared-parameter: create/reuse a shared param file, bind an instance text param to categories, set values by id
// operation: add-shared-parameter. CB6-R1-T2: bind shared param "FEWD Tag" (Text, instance, Identity Data) to 5 categories, set F-1/F-10/F-12.
// parameters: ["dryrun"] = read-only preflight (no file IO, no edits; reports whether the shared param file would be created and set).
// Run path: if Application.SharedParametersFilename is empty, creates 23-073_FEWD_SharedParams.txt (only if missing, UTF-16 header) and sets it; previous value is returned.
bool dry = parameters != null && parameters.Length > 0 && string.Equals(Convert.ToString(parameters[0]), "dryrun", StringComparison.OrdinalIgnoreCase);
if (document.Title == null || document.Title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0)
    return "NO_GO wrong document open: " + document.Title;
const string PNAME = "FEWD Tag";
const string GNAME = "120D_FEWD";
const string NEWSPF = "C:/Users/j/swarm/23-073_Civita_Blocks_6and7_San_Diego_Sudberry/23-073_FEWD_SharedParams.txt";

var app = document.Application;
string prevSpf = app.SharedParametersFilename;
string spf = prevSpf;
string spfNote = "";
if (string.IsNullOrEmpty(prevSpf))
{
    if (dry)
    {
        spfNote = "would (if missing) write header file " + NEWSPF + " and set SharedParametersFilename to it (prev='')";
        spf = null;
    }
    else
    {
        if (!System.IO.File.Exists(NEWSPF))
        {
            string hdr = "# This is a Revit shared parameter file.\r\n# Do not edit manually.\r\n*META\tVERSION\tMINVERSION\r\nMETA\t2\t1\r\n*GROUP\tID\tNAME\r\n*PARAM\tGUID\tNAME\tDATATYPE\tDATACATEGORY\tGROUP\tVISIBLE\tDESCRIPTION\tUSERMODIFIABLE\tHIDEWHENNOINPUT\r\n";
            System.IO.File.WriteAllText(NEWSPF, hdr, System.Text.Encoding.Unicode);
            spfNote = "created " + NEWSPF + "; ";
        }
        else spfNote = "file existed " + NEWSPF + "; ";
        app.SharedParametersFilename = NEWSPF;
        spf = NEWSPF;
        spfNote += "SharedParametersFilename prev='' new='" + app.SharedParametersFilename + "'";
    }
}
else
{
    if (!dry && !System.IO.File.Exists(prevSpf)) return "NO_GO shared parameter file missing: " + prevSpf;
    spfNote = "using existing SharedParametersFilename (unchanged) " + prevSpf;
}
DefinitionFile df = null;
if (spf != null)
{
    df = app.OpenSharedParameterFile();
    if (df == null) return "NO_GO could not open shared parameter file: " + spf;
}

// categories
BuiltInCategory[] bics = new BuiltInCategory[] {
    BuiltInCategory.OST_PlumbingFixtures, BuiltInCategory.OST_PlumbingEquipment, BuiltInCategory.OST_PipeAccessory,
    BuiltInCategory.OST_MechanicalEquipment, BuiltInCategory.OST_GenericModel };
var cats = new List<Category>();
foreach (BuiltInCategory b in bics)
{
    Category c = Category.GetCategory(document, b);
    if (c == null) return "NO_GO category not found in model: " + b;
    cats.Add(c);
}

// existing binding?
bool bound = false;
DefinitionBindingMapIterator it = document.ParameterBindings.ForwardIterator();
it.Reset();
while (it.MoveNext())
{
    if (it.Key.Name == PNAME) { bound = true; break; }
}

// existing definition in the file
ExternalDefinition extDef = null;
string foundGroup = null;
if (df != null)
{
    foreach (DefinitionGroup g in df.Groups)
    {
        foreach (Definition d in g.Definitions)
        {
            if (d.Name == PNAME) { extDef = d as ExternalDefinition; foundGroup = g.Name; break; }
        }
        if (extDef != null) break;
    }
}

// targets
string[] expFam = new string[] { "Plumbing_Floor_Mop_Sink", "Generic Box WITH ROUGH-IN INFO1", "Eye & Face Wash" };
long[] tid = new long[] { 14226160L, 14970579L, 14719484L };
string[] tval = new string[] { "F-1", "F-10", "F-12" };
var tgt = new List<FamilyInstance>();
var tgtMsg = new List<string>();
for (int i = 0; i < 3; i++)
{
    FamilyInstance fi = document.GetElement(new ElementId(tid[i])) as FamilyInstance;
    if (fi == null) { tgt.Add(null); tgtMsg.Add(tid[i] + ":NOT_FOUND"); continue; }
    string fam = fi.Symbol != null ? fi.Symbol.FamilyName : "";
    bool ok = fam.IndexOf(expFam[i], StringComparison.OrdinalIgnoreCase) >= 0;
    string cat = fi.Category != null ? fi.Category.Name : "";
    if (!ok) { tgt.Add(null); tgtMsg.Add(tid[i] + ":FAMILY_MISMATCH live='" + fam + "' expected contains '" + expFam[i] + "'"); continue; }
    tgt.Add(fi);
    tgtMsg.Add(tid[i] + ":OK fam='" + fam + "' cat='" + cat + "'");
}

if (dry)
{
    return "DRYRUN spfPrev='" + prevSpf + "' spfAction=" + spfNote + " bindingExists=" + bound + " defInFile=" + (extDef != null ? (foundGroup + " " + extDef.GUID) : "none (would create in group " + GNAME + ")") +
           " cats=" + cats.Count + " targets=[" + string.Join("; ", tgtMsg) + "]\ndone=0 skipped=0 failed=0";
}

int done = 0, skipped = 0, failed = 0;
var log = new List<string>();
string guid = extDef != null ? extDef.GUID.ToString() : "";

if (bound)
{
    skipped++;
    log.Add("(a) skipped: binding '" + PNAME + "' already exists");
}
else
{
    if (extDef == null)
    {
        DefinitionGroup grp = df.Groups.get_Item(GNAME);
        if (grp == null) grp = df.Groups.Create(GNAME);
        Definition nd = grp.Definitions.get_Item(PNAME);
        if (nd == null)
        {
            var opt = new ExternalDefinitionCreationOptions(PNAME, SpecTypeId.String.Text);
            nd = grp.Definitions.Create(opt);
        }
        extDef = nd as ExternalDefinition;
        if (extDef == null) throw new Exception("ROLLED_BACK (a): could not create/get external definition");
        log.Add("(a) created definition in group " + GNAME);
    }
    else log.Add("(a) reused definition from group " + foundGroup);
    guid = extDef.GUID.ToString();
    CategorySet cs = app.Create.NewCategorySet();
    foreach (Category c in cats) cs.Insert(c);
    InstanceBinding ib = app.Create.NewInstanceBinding(cs);
    if (!document.ParameterBindings.Insert(extDef, ib, GroupTypeId.IdentityData))
        throw new Exception("ROLLED_BACK (a): ParameterBindings.Insert returned false");
    done++;
    log.Add("(a) bound instance to " + cats.Count + " categories");
    document.Regenerate();
}

for (int i = 0; i < 3; i++)
{
    FamilyInstance fi = tgt[i];
    if (fi == null) { failed++; log.Add("(b) " + tgtMsg[i]); continue; }
    Parameter p = fi.LookupParameter(PNAME);
    if (p == null) throw new Exception("ROLLED_BACK (b) " + tid[i] + ": parameter '" + PNAME + "' not found on element after binding");
    string old = p.AsString();
    if (old == tval[i]) { skipped++; log.Add("(b) " + tid[i] + " skipped: already '" + tval[i] + "'"); continue; }
    if (p.IsReadOnly || !p.Set(tval[i])) throw new Exception("ROLLED_BACK (b) " + tid[i] + ": Set failed");
    done++;
    log.Add("(b) " + tid[i] + " old='" + old + "' new='" + tval[i] + "'");
}
if (failed > 0) throw new Exception("ROLLED_BACK: " + failed + " target mismatch(es): " + string.Join("; ", tgtMsg));

return "spfPrev='" + prevSpf + "' spf: " + spfNote + "\nguid=" + guid + " cats=PlumbingFixtures,PlumbingEquipment,PipeAccessory,MechanicalEquipment,GenericModels\n" +
       string.Join("\n", log) + "\ndone=" + done + " skipped=" + skipped + " failed=" + failed;
