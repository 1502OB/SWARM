// operation: title block family edit | TB-T1 | run 20261104-1905 | orchestrator 2026-11-05
// Jeremy 11-05: title block confirmed -> add the 100% DD issuance row (BEP item 1 checked by Jeremy).
// SURF  ('RDC Title Block Arch E1_SURF'): keep row 1 'SURF 75% DD SET / 09.17.2026'; copy both TextNotes
//        (22808805, 22808806) one row down (pitch 0.0144 ft, measured from the plotted ISSUANCE table) with text
//        'SURF 100% DD SET' / '11.05.26'.
// DRIFTWOOD ('RDC Title Block Arch E1_DRIFTWOOD'): no row today; create row 1 at the SURF row-1 coordinates with
//        the same TextNoteType name and alignment: 'D&V 100% DD SET' / '11.05.26'.
// Reload each family with overwrite (project parameter values kept). transactionMode MUST be "none".
// parameters: ["dryrun"] = open the family docs and report only. Failed checks RETURN "FAILED TB-T1: ...".
// The family docs are left open (writers may not call Close); Revit discards them at session end.
bool dry = parameters != null && parameters.Length > 0 && parameters[0] != null && parameters[0].ToString() == "dryrun";
if (document.Title.IndexOf("PLMB-24220-R25", StringComparison.OrdinalIgnoreCase) < 0) return "FAILED TB-T1: wrong document " + document.Title;
if (document.IsModifiable) return "FAILED TB-T1: project has an open transaction; run with transactionMode none";
const double PITCH = 0.0144;
Func<string, Family> fam = n => new FilteredElementCollector(document).OfClass(typeof(Family)).Cast<Family>().FirstOrDefault(f => f.Name == n);
Family fS = fam("RDC Title Block Arch E1_SURF"), fD = fam("RDC Title Block Arch E1_DRIFTWOOD");
if (fS == null || fD == null) return "FAILED TB-T1: title block family missing (SURF " + (fS != null) + ", DRIFTWOOD " + (fD != null) + ")";
Document dS = document.EditFamily(fS), dD = document.EditFamily(fD);
var n1 = dS.GetElement(new ElementId(22808805L)) as TextNote; var n2 = dS.GetElement(new ElementId(22808806L)) as TextNote;
if (n1 == null || n2 == null) return "FAILED TB-T1: SURF row texts 22808805/22808806 not found in family doc";
if (!n1.Text.Contains("75%") || !n2.Text.Contains("09.17")) return "FAILED TB-T1: SURF row texts changed: '" + n1.Text + "' / '" + n2.Text + "'";
Func<Document, string, bool> hasText = (d, s) => new FilteredElementCollector(d).OfClass(typeof(TextNote)).Cast<TextNote>().Any(t => t.Text.Contains(s));
bool sDone = hasText(dS, "100% DD"), dDone = hasText(dD, "100% DD");
string t1Name = dS.GetElement(n1.GetTypeId()).Name, t2Name = dS.GetElement(n2.GetTypeId()).Name;
var dTypes = new FilteredElementCollector(dD).OfClass(typeof(TextNoteType)).Cast<TextNoteType>().ToList();
var dt1 = dTypes.FirstOrDefault(t => t.Name == t1Name); var dt2 = dTypes.FirstOrDefault(t => t.Name == t2Name);
var hdr = dD.GetElement(new ElementId(22872069L)) as TextNote;
string info = "SURF row1 '" + n1.Text.Trim() + "' @" + n1.Coord + " w=" + n1.Width.ToString("F4") + " " + n1.HorizontalAlignment + " type '" + t1Name + "'; '" + n2.Text.Trim() + "' @" + n2.Coord + " " + n2.HorizontalAlignment + " type '" + t2Name + "'"
    + " | DRIFTWOOD types found: " + (dt1 != null) + "/" + (dt2 != null) + " header " + (hdr == null ? "missing" : hdr.Coord.ToString())
    + " | already has 100% DD: SURF " + sDone + ", DRIFTWOOD " + dDone;
if (dt1 == null || dt2 == null) return "FAILED TB-T1: DRIFTWOOD family lacks TextNoteType '" + t1Name + "' or '" + t2Name + "'. " + info;
if (dry) return "DRYRUN ok: " + info + "\ndone=0 skipped=0 failed=0";

var made = new List<string>();
if (!sDone)
{
    using (var tx = new Transaction(dS, "TB-T1 SURF 100% DD row"))
    {
        tx.Start();
        var c1 = ElementTransformUtils.CopyElement(dS, n1.Id, new XYZ(0, -PITCH, 0)).First();
        var c2 = ElementTransformUtils.CopyElement(dS, n2.Id, new XYZ(0, -PITCH, 0)).First();
        ((TextNote)dS.GetElement(c1)).Text = "SURF 100% DD SET";
        ((TextNote)dS.GetElement(c2)).Text = "11.05.26";
        tx.Commit();
        made.Add("SURF " + c1.Value + "," + c2.Value);
    }
}
if (!dDone)
{
    using (var tx = new Transaction(dD, "TB-T1 D&V 100% DD row"))
    {
        tx.Start();
        ElementId vId = hdr != null ? hdr.OwnerViewId : n1.OwnerViewId;
        var o1 = new TextNoteOptions(dt1.Id); o1.HorizontalAlignment = n1.HorizontalAlignment;
        var o2 = new TextNoteOptions(dt2.Id); o2.HorizontalAlignment = n2.HorizontalAlignment;
        var a = TextNote.Create(dD, vId, n1.Coord, n1.Width, "D&V 100% DD SET", o1);
        var b = TextNote.Create(dD, vId, n2.Coord, n2.Width, "11.05.26", o2);
        tx.Commit();
        made.Add("DRIFTWOOD " + a.Id.Value + "," + b.Id.Value);
    }
}
// attempt 1: the bridge rejects class declarations, so build IFamilyLoadOptions at runtime with Reflection.Emit:
// OnFamilyFound -> overwriteParameterValues=false, return true; OnSharedFamilyFound -> source=Family, overwrite=false, true.
var ab = System.Reflection.Emit.AssemblyBuilder.DefineDynamicAssembly(new System.Reflection.AssemblyName("TbOpts" + DateTime.Now.Ticks), System.Reflection.Emit.AssemblyBuilderAccess.Run);
var mb = ab.DefineDynamicModule("m");
var tbld = mb.DefineType("TbLoadOptions", System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Class, typeof(object), new[] { typeof(IFamilyLoadOptions) });
var ma = System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Virtual | System.Reflection.MethodAttributes.HideBySig | System.Reflection.MethodAttributes.NewSlot | System.Reflection.MethodAttributes.Final;
var m1 = tbld.DefineMethod("OnFamilyFound", ma, typeof(bool), new[] { typeof(bool), typeof(bool).MakeByRefType() });
m1.DefineParameter(2, System.Reflection.ParameterAttributes.Out, "overwriteParameterValues");
var il1 = m1.GetILGenerator();
il1.Emit(System.Reflection.Emit.OpCodes.Ldarg_2); il1.Emit(System.Reflection.Emit.OpCodes.Ldc_I4_0); il1.Emit(System.Reflection.Emit.OpCodes.Stind_I1);
il1.Emit(System.Reflection.Emit.OpCodes.Ldc_I4_1); il1.Emit(System.Reflection.Emit.OpCodes.Ret);
tbld.DefineMethodOverride(m1, typeof(IFamilyLoadOptions).GetMethod("OnFamilyFound"));
var m2 = tbld.DefineMethod("OnSharedFamilyFound", ma, typeof(bool), new[] { typeof(Family), typeof(bool), typeof(FamilySource).MakeByRefType(), typeof(bool).MakeByRefType() });
m2.DefineParameter(3, System.Reflection.ParameterAttributes.Out, "source");
m2.DefineParameter(4, System.Reflection.ParameterAttributes.Out, "overwriteParameterValues");
var il2 = m2.GetILGenerator();
il2.Emit(System.Reflection.Emit.OpCodes.Ldarg_3); il2.Emit(System.Reflection.Emit.OpCodes.Ldc_I4, (int)FamilySource.Family); il2.Emit(System.Reflection.Emit.OpCodes.Stind_I4);
il2.Emit(System.Reflection.Emit.OpCodes.Ldarg_S, (byte)4); il2.Emit(System.Reflection.Emit.OpCodes.Ldc_I4_0); il2.Emit(System.Reflection.Emit.OpCodes.Stind_I1);
il2.Emit(System.Reflection.Emit.OpCodes.Ldc_I4_1); il2.Emit(System.Reflection.Emit.OpCodes.Ret);
tbld.DefineMethodOverride(m2, typeof(IFamilyLoadOptions).GetMethod("OnSharedFamilyFound"));
var opts = (IFamilyLoadOptions)Activator.CreateInstance(tbld.CreateType());
string loaded = "";
if (!sDone) { var lf = dS.LoadFamily(document, opts); loaded += "SURF reloaded id=" + (lf == null ? "null" : lf.Id.Value.ToString()) + "; "; }
if (!dDone) { var lf = dD.LoadFamily(document, opts); loaded += "DRIFTWOOD reloaded id=" + (lf == null ? "null" : lf.Id.Value.ToString()) + "; "; }
return "TB-T1 ok: " + info + "\nnew family texts: " + string.Join("; ", made) + "\n" + loaded + "\ncleanup: reload the families from the ACC version before this run (or delete the new texts in the family and reload)\ndone=" + made.Count + " skipped=" + ((sDone ? 1 : 0) + (dDone ? 1 : 0)) + " failed=0";

