<#
Dump an AutoCAD ActiveX type library (.tlb) to TSV for tlb_to_md.py. Read-only: it only loads the file
(LoadTypeLibEx REGKIND_NONE, nothing is registered).

  pwsh -File tlb_dump.ps1 -Tlb "C:\Program Files\Common Files\Autodesk Shared\acax26enu.tlb" -Out acax.tsv

Records (tab separated):
  LIB   <name> <doc>
  TYPE  <kind> <name> <doc> <base1,base2>      kind = INTERFACE | DISPATCH | COCLASS | ENUM | ALIAS | RECORD
  FUNC  <type> <name> <invkind> <ret> <params> <doc>   invkind = METHOD | GET | PUT | PUTREF
        params = name:type:flags|...  flags from in,out,retval,opt,default=<v>
  CONST <type> <name> <value> <doc>
#>
param([Parameter(Mandatory)][string]$Tlb, [Parameter(Mandatory)][string]$Out)

Add-Type -TypeDefinition @'
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

public static class TlbDump
{
    [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    static extern void LoadTypeLibEx(string file, int regkind, out ITypeLib tl);

    static string Clean(string s) { return (s ?? "").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ").Trim(); }

    static string Doc(ITypeInfo ti, int memid)
    {
        string name, doc, file; int ctx;
        ti.GetDocumentation(memid, out name, out doc, out ctx, out file);
        return Clean(doc);
    }

    static string TypeName(ITypeInfo ti, TYPEDESC td)
    {
        VarEnum vt = (VarEnum)td.vt;
        switch (vt)
        {
            case VarEnum.VT_PTR:
            {
                TYPEDESC inner = (TYPEDESC)Marshal.PtrToStructure(td.lpValue, typeof(TYPEDESC));
                return TypeName(ti, inner) + "*";
            }
            case VarEnum.VT_SAFEARRAY:
            {
                TYPEDESC inner = (TYPEDESC)Marshal.PtrToStructure(td.lpValue, typeof(TYPEDESC));
                return "SAFEARRAY(" + TypeName(ti, inner) + ")";
            }
            case VarEnum.VT_USERDEFINED:
            {
                ITypeInfo rti;
                ti.GetRefTypeInfo(unchecked((int)(long)td.lpValue), out rti);
                string n, d, f; int c;
                rti.GetDocumentation(-1, out n, out d, out c, out f);
                return n;
            }
            case VarEnum.VT_BSTR: return "BSTR";
            case VarEnum.VT_BOOL: return "VARIANT_BOOL";
            case VarEnum.VT_VARIANT: return "VARIANT";
            case VarEnum.VT_I2: return "short";
            case VarEnum.VT_I4: case VarEnum.VT_INT: return "long";
            case VarEnum.VT_I8: return "int64";
            case VarEnum.VT_UI1: return "byte";
            case VarEnum.VT_UI2: return "ushort";
            case VarEnum.VT_UI4: case VarEnum.VT_UINT: return "ulong";
            case VarEnum.VT_R4: return "float";
            case VarEnum.VT_R8: return "double";
            case VarEnum.VT_DATE: return "DATE";
            case VarEnum.VT_DISPATCH: return "IDispatch*";
            case VarEnum.VT_UNKNOWN: return "IUnknown*";
            case VarEnum.VT_HRESULT: return "HRESULT";
            case VarEnum.VT_VOID: return "void";
            case VarEnum.VT_LPSTR: case VarEnum.VT_LPWSTR: return "LPSTR";
            default: return vt.ToString();
        }
    }

    static string Variant(IntPtr p)
    {
        try { object o = Marshal.GetObjectForNativeVariant(p); return o == null ? "" : Convert.ToString(o, System.Globalization.CultureInfo.InvariantCulture); }
        catch { return "?"; }
    }

    public static void Dump(string path, string outPath)
    {
        ITypeLib tl;
        LoadTypeLibEx(path, 2 /* REGKIND_NONE */, out tl);
        var sb = new StringBuilder();
        string ln, ld, lf; int lc;
        tl.GetDocumentation(-1, out ln, out ld, out lc, out lf);
        sb.Append("LIB\t" + ln + "\t" + Clean(ld) + "\n");
        int n = tl.GetTypeInfoCount();
        for (int i = 0; i < n; i++)
        {
            ITypeInfo ti; tl.GetTypeInfo(i, out ti);
            IntPtr pta; ti.GetTypeAttr(out pta);
            TYPEATTR ta = (TYPEATTR)Marshal.PtrToStructure(pta, typeof(TYPEATTR));
            string tname, tdoc, tf; int tc;
            ti.GetDocumentation(-1, out tname, out tdoc, out tc, out tf);
            string kind;
            switch (ta.typekind)
            {
                case TYPEKIND.TKIND_INTERFACE: kind = "INTERFACE"; break;
                case TYPEKIND.TKIND_DISPATCH: kind = "DISPATCH"; break;
                case TYPEKIND.TKIND_COCLASS: kind = "COCLASS"; break;
                case TYPEKIND.TKIND_ENUM: kind = "ENUM"; break;
                case TYPEKIND.TKIND_ALIAS: kind = "ALIAS"; break;
                case TYPEKIND.TKIND_RECORD: kind = "RECORD"; break;
                default: kind = ta.typekind.ToString(); break;
            }
            // Dual interfaces: walk the vtable interface so members are the declared ones only.
            ITypeInfo walk = ti; TYPEATTR wa = ta; IntPtr pwa = IntPtr.Zero;
            if (ta.typekind == TYPEKIND.TKIND_DISPATCH && (ta.wTypeFlags & TYPEFLAGS.TYPEFLAG_FDUAL) != 0)
            {
                try
                {
                    int href; ti.GetRefTypeOfImplType(-1, out href);
                    ti.GetRefTypeInfo(href, out walk);
                    walk.GetTypeAttr(out pwa);
                    wa = (TYPEATTR)Marshal.PtrToStructure(pwa, typeof(TYPEATTR));
                    kind = "INTERFACE";
                }
                catch { walk = ti; wa = ta; pwa = IntPtr.Zero; }
            }
            var bases = new List<string>();
            for (int k = 0; k < wa.cImplTypes; k++)
            {
                try
                {
                    int href; walk.GetRefTypeOfImplType(k, out href);
                    ITypeInfo bti; walk.GetRefTypeInfo(href, out bti);
                    string bn, bd, bf; int bc;
                    bti.GetDocumentation(-1, out bn, out bd, out bc, out bf);
                    IMPLTYPEFLAGS fl; walk.GetImplTypeFlags(k, out fl);
                    bases.Add(((fl & IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT) != 0 && ta.typekind == TYPEKIND.TKIND_COCLASS ? "*" : "")
                              + ((fl & IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE) != 0 ? "source:" : "") + bn);
                }
                catch { }
            }
            string alias = "";
            if (ta.typekind == TYPEKIND.TKIND_ALIAS) alias = TypeName(ti, ta.tdescAlias);
            sb.Append("TYPE\t" + kind + "\t" + tname + "\t" + Clean(tdoc) + "\t" + string.Join(",", bases) + "\t" + alias + "\n");

            for (int f = 0; f < wa.cFuncs; f++)
            {
                IntPtr pfd; walk.GetFuncDesc(f, out pfd);
                FUNCDESC fd = (FUNCDESC)Marshal.PtrToStructure(pfd, typeof(FUNCDESC));
                if ((fd.wFuncFlags & (short)FUNCFLAGS.FUNCFLAG_FRESTRICTED) != 0) { walk.ReleaseFuncDesc(pfd); continue; }
                string[] names = new string[fd.cParams + 1]; int got;
                walk.GetNames(fd.memid, names, names.Length, out got);
                string inv = fd.invkind == INVOKEKIND.INVOKE_PROPERTYGET ? "GET"
                           : fd.invkind == INVOKEKIND.INVOKE_PROPERTYPUT ? "PUT"
                           : fd.invkind == INVOKEKIND.INVOKE_PROPERTYPUTREF ? "PUTREF" : "METHOD";
                string ret = TypeName(walk, fd.elemdescFunc.tdesc);
                var ps = new List<string>();
                int sz = Marshal.SizeOf(typeof(ELEMDESC));
                for (int p = 0; p < fd.cParams; p++)
                {
                    ELEMDESC ed = (ELEMDESC)Marshal.PtrToStructure(new IntPtr(fd.lprgelemdescParam.ToInt64() + p * sz), typeof(ELEMDESC));
                    PARAMFLAG pf = ed.desc.paramdesc.wParamFlags;
                    var fl = new List<string>();
                    if ((pf & PARAMFLAG.PARAMFLAG_FIN) != 0) fl.Add("in");
                    if ((pf & PARAMFLAG.PARAMFLAG_FOUT) != 0) fl.Add("out");
                    if ((pf & PARAMFLAG.PARAMFLAG_FRETVAL) != 0) fl.Add("retval");
                    if ((pf & PARAMFLAG.PARAMFLAG_FOPT) != 0) fl.Add("opt");
                    if ((pf & PARAMFLAG.PARAMFLAG_FHASDEFAULT) != 0 && ed.desc.paramdesc.lpVarValue != IntPtr.Zero)
                        fl.Add("default=" + Clean(Variant(new IntPtr(ed.desc.paramdesc.lpVarValue.ToInt64() + IntPtr.Size))));
                    string pn = (p + 1 < got && names[p + 1] != null) ? names[p + 1] : ("arg" + p);
                    if (fd.invkind != INVOKEKIND.INVOKE_FUNC && p + 1 >= got) pn = "value";
                    ps.Add(pn + ":" + TypeName(walk, ed.tdesc) + ":" + string.Join(",", fl));
                }
                sb.Append("FUNC\t" + tname + "\t" + names[0] + "\t" + inv + "\t" + ret + "\t" + string.Join("|", ps) + "\t" + Doc(walk, fd.memid) + "\n");
                walk.ReleaseFuncDesc(pfd);
            }
            for (int v = 0; v < wa.cVars; v++)
            {
                IntPtr pvd; walk.GetVarDesc(v, out pvd);
                VARDESC vd = (VARDESC)Marshal.PtrToStructure(pvd, typeof(VARDESC));
                string[] names = new string[1]; int got;
                walk.GetNames(vd.memid, names, 1, out got);
                string val = "";
                if (vd.varkind == VARKIND.VAR_CONST) val = Variant(vd.desc.lpvarValue);
                else val = TypeName(walk, vd.elemdescVar.tdesc);
                sb.Append("CONST\t" + tname + "\t" + names[0] + "\t" + val + "\t" + Doc(walk, vd.memid) + "\n");
                walk.ReleaseVarDesc(pvd);
            }
            if (pwa != IntPtr.Zero) walk.ReleaseTypeAttr(pwa);
            ti.ReleaseTypeAttr(pta);
        }
        File.WriteAllText(outPath, sb.ToString(), new UTF8Encoding(false));
    }
}
'@

[TlbDump]::Dump((Resolve-Path $Tlb).Path, [IO.Path]::GetFullPath($Out))
Write-Output "wrote $Out"
