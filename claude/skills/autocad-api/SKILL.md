---
name: autocad-api
description: Exact AutoCAD 2027 API reference for our .lsp scripts - every ActiveX object, method, property and enum (vla- calls, from the installed type library), every AutoLISP / vl- / vlax- function with its signature, DXF group codes for entget/entmod/entmake/ssget, and the .NET API (dll2llm) - plus a checker that scans a whole .lsp. Use before writing or fixing any AutoCAD script to confirm every function, vla- member and group code exists in 2027.
---

# AutoCAD 2027 API reference

Generated 2026-11-06 for the AutoCAD on this PC (AutoCAD 2027, R26). Our scripts are AutoLISP run through the bridge's
execute_lisp; ActiveX is reached through Visual LISP (vla-/vlax-). Regenerate after an AutoCAD upgrade (bottom).
Firm rules for the result (clouds, snaps, publishing, payload size) are in `acad-conventions`; this skill only says
what exists and how it is called.

## Contract
- **Never** use a function, vla- member or DXF group code in a script that you have not found here. Memory of
  AutoLISP is not evidence: `fboundp`, `identity` and `stringp` do not exist; MText has no Explode.
- **Irreversible:** none. This skill only reads files.
- **Failure looks like:** the checker prints `NOT FOUND` or `DOC-ONLY`. Then change the code to a name it lists,
  or stop and report `new_api: NOT IN 2027 API`. Never send a script with a NOT FOUND name.
- **Done when:** your reply shows `api_checked: <n> found, 0 not found` from
  `acad_api_check.py --lsp <script>` (plus any `Object.Member` checks for vla- calls on a known object).

## How to check (in this order)
1. `references/core.md`: the 110 API names our scripts use, with call counts, the 2027 signature and firm notes,
   plus a list of names that do NOT exist.
2. Checker (if you have Bash). One call checks a whole script:
   `python C:/Users/Jeremy/.claude/skills/autocad-api/scripts/acad_api_check.py --lsp <file.lsp> [--with lib.lsp]`
   It checks every function call, every 'vla-xxx passed to vl-catch-all-apply/mapcar, and every member named in
   vlax-get/-put/-invoke(-method)/-property calls. It skips the file's own defuns, locals, quoted lists and dotted
   pairs, and follows `(load "...")`; pass helpers loaded another way with `--with` (e.g.
   `--with C:/Users/Jeremy/swarm/library/acad/swarm_lib.lsp`; a patch file needs `--with` its base script).
   Single names: `acad_api_check.py vla-AddText LWPolyline.SetBulge Hatch.put-PatternName entmod dxf:TEXT.72`.
   `--object LWPolyline` lists all members including inherited ones, `--dxf MTEXT` lists group codes.
   Exit 0 = all FOUND, 1 = any NOT FOUND / DOC-ONLY (it suggests close names).
3. A vla- name alone only proves SOME object has it. When you know the object, check `Object.Member`
   (`MText.Explode` -> NOT FOUND although `vla-Explode` exists on BlockReference). Object names: `Line`, `AcadLine`
   or `IAcadLine`; `put-Prop` checks that the property is writable.
4. Without Bash: `references/activex-INDEX.md` (interface -> file, inherits), then Grep the topic file for
   `^\[INTERFACE\] IAcadLWPolyline` and read that section; functions: Grep `references/autolisp-functions.md` for
   ``- `(entmod ``; DXF: the `## <NAME>` section of `references/dxf-entities.md`.

## Files
| File | What |
|---|---|
| `references/core.md` | names our scripts use, with notes (start here) |
| `references/activex-INDEX.md`, `activex-*.md` | 157 ActiveX interfaces from `acax26enu.tlb` + `axdb26enu.tlb`: `M` methods with the vla- call form, `P` properties with type and get/put/READ-ONLY |
| `references/activex-enums.md` | 146 Ac* enums with values (acLnWt025 = 25, acAttachmentPointTopLeft = 1 ...) |
| `references/autolisp-functions.md` | 491 AutoLISP, vl-, vlax-, vlr-, layerstate-, acet- functions: signature, return, description, help link |
| `references/dxf-entities.md` | DXF group codes per entity and table (COMMON codes apply to all) |
| `references/dotnet/` | .NET API (AcDbMgd, AcMgd, AcCoreMgd; 1963 types, dll2llm). Only for a NETLOAD plugin; our bridge runs LISP |
| `references/activex.json`, `autolisp.json` | machine-readable data the checker loads |

## Things the reference shows that memory gets wrong
- A member is declared once, on the base interface: Layer/Color/Move/Copy/Delete/Update/GetBoundingBox are on
  `Entity` or `Object`, AddText/AddLine/AddLightWeightPolyline are on `Block` (so ModelSpace, PaperSpace and block
  definitions). The checker walks `Inherits:`; when grepping, walk it yourself.
- `Viewport` is the tiled viewport table record; the paper-space viewport entity is `PViewport`. `Polyline` is the
  old heavy polyline; the usual one is `LWPolyline`. `Block` is a definition; an insert is `BlockReference`.
- Out parameters are quoted symbols the call sets to variants: `(vla-GetBoundingBox o 'mn 'mx)`,
  then `(vlax-safearray->list mn)`. Points and vertex lists go in as variants (`vlax-3d-point`, or
  `vlax-make-safearray` + `vlax-safearray-fill`).
- READ-ONLY properties have no vla-put-: e.g. Handle, ObjectName, Hatch.PatternName (use SetPattern), Line.Length.
- `vlax-get` / `vlax-put` / `vlax-invoke` are undocumented legacy forms; they work here (return LISP values).
- `DOC-ONLY` names (62) were in Autodesk's 2021 help data but are not in the 2027 type library: the *32 members of
  32-bit builds, FileDependency, Table.get-ColumnWidth ... Assume they do not exist; if you must, test read-only on
  the live bridge with `vlax-property-available-p` / `vlax-method-applicable-p` through acad-scout.

## Sources and regenerating
- ActiveX: `pwsh -File scripts/tlb_dump.ps1 -Tlb "C:/Program Files/Common Files/Autodesk Shared/acax26enu.tlb" -Out acax.tsv`
  (same for `axdb26enu.tlb`), then `python scripts/tlb_to_md.py acax.tsv axdb.tsv`. The type library is what the
  vla- wrappers are generated from at run time, so it is the authority for vla- names in this version.
- AutoLISP: `python -I scripts/gen_autolisp.py webHelpAbstraction.json` (Autodesk's AutoLISP extension help data,
  github.com/Autodesk-AutoCAD/AutoLispExt, extension/src/help; its release year is 2021: core functions are stable;
  AutoCAD 2027 defines 3058 function symbols after vl-load-com).
- DXF: Autodesk DXF Reference (source and year in the dxf-entities.md header).
- .NET: `dll2llm acdbmgd.dll acmgd.dll accoremgd.dll --output references/dotnet` run in the AutoCAD install folder.
- core.md: `python scripts/gen_core.py C:/Users/Jeremy/swarm` (counts calls; notes live in that script).
