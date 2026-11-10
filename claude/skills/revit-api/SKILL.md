---
name: revit-api
description: Exact Revit 2025 API reference (all public types and members, generated from RevitAPI.dll + RevitAPIUI.dll with Autodesk's dll2llm) plus a checker script. Use before writing or fixing any Revit C# (send_code_to_revit scripts) to confirm every type, method, property and constructor exists with that signature.
---

# Revit 2025 API reference

Generated 2026-11-05 with Autodesk's dll2llm (commit 099c2de) from `C:/Program Files/Autodesk/Revit 2025/`
(RevitAPI.dll + RevitAPIUI.dll + RevitAPI.xml): 2306 public types in 45 files under `references/`. It matches the
Revit version on this PC (2025.4); regenerate it after a Revit upgrade.

## Contract
- **Never** use an API member in a script that you have not found here. Memory of the Revit API is not evidence:
  `TransformWithBoundary.GetTransform()` "existed" in a script and cost three failed runs (LESSONS R20).
- **Irreversible:** none. This skill only reads files.
- **Failure looks like:** the checker prints `NOT FOUND`. Then change the code to a member it lists, or stop and
  report the member as `new_api: NOT IN 2025 API`. Never send code with a NOT FOUND member.
- **Done when:** every Revit type and member the script uses is listed in your reply as
  `api_checked: <n> found, 0 not found`, from a checker run or a grep of `references/`.

## How to check (in this order)
1. `references/core.md`: the ~80 types our scripts actually use, with firm notes (the facts that cost failed runs).
2. `references/INDEX.md`: type -> file (2306 types). Then read only that topic file's section for the type.
3. Checker (if you have Bash): one call for all members, short or full type names, inheritance followed:
   `python ~/.claude/skills/revit-api/scripts/revit_api_check.py View.SetCategoryHidden Viewport.Pinned ...`
   `--type Viewport` lists a type's members. Exit 1 = something is NOT FOUND (it suggests close names).
4. Without Bash, use Grep on `references/` for the member signature, e.g. `SetBoxCenter\(`, then confirm the
   enclosing type header above the hit with a second Grep such as `^\[[A-Z ]+\] Viewport\b` (headers look like
   `[CLASS] Viewport` or `[ABSTRACT CLASS] PlumbingUtils`; the files have CRLF line ends, so never end a pattern
   with `$`). A member is documented only on the type that declares it: `Viewport.Pinned` is on `Element`,
   `TextNote.Text` on `TextElement`.

## Things the reference shows that memory gets wrong
- Two `Document` types: fitting/spot creation (NewTeeFitting, NewTransitionFitting, NewTakeoffFitting,
  NewSpotElevation) is on `Autodesk.Revit.Creation.Document` (`doc.Create`), not `Autodesk.Revit.DB.Document`.
- Static utilities (`ElementTransformUtils`, `PlumbingUtils`, `WorksharingUtils`) are listed as `[ABSTRACT CLASS]`.
- C# `el.get_Parameter(BuiltInParameter.X)` is the indexed property `Parameter` in the reference.
- Inherited members appear only on the declaring base type (dll2llm limitation): walk `Inherits:` upward.
