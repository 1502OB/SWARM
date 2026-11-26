# library/

Reusable, reviewed scripts. Task scripts live in `scripts/`; files here are fixed and called by path.

## Read-only scout scripts (Revit), 2026-11-25

Pre-compiled read-only C# for revit-scout, snapshot-taker and verifier, so no agent hand-writes a dump and no compile
error costs a round trip on the serial bridge lane. Each one does many API calls and returns ONE compact JSON page.

Call (needs the codefile bridge patch, `tools/patch_revit_mcp.py --codefile-only`):

    send_code_to_revit {codeFile: "C:/Users/j/swarm/library/revit/<name>.cs",
                        expectMarker: "// swarm-lib <name> v1", transactionMode: "none",
                        parameters: ["tag=<task-id> <step>", "title=<doc title substring>", "<key>=<value>", ...]}

- Parameters are strings `key=value` (any order, keys case-insensitive). `tag=` is echoed first; `title=` makes
  the script refuse a wrong active document. Lists inside one value: ids comma-separated, parameter names `|`.
- Return: line 1 `<tag> <MARKER> done=<n> skipped=<n> failed=<n>`, line 2 one JSON object. The exact schema is in
  each file's header comment. Every return stays under `max=` (default 2300 chars, the bridge truncates at ~3 KB);
  a list that does not fit sets `truncated: true` and `next: <offset>`: call again with `offset=<next>`.
- A script error returns `failed=1` and `{"marker","tag","error":"<stage>: <Type>: <message>"}`.
- All four pass `tools/revit_cs_check.py` (compile + lint) and `claude/hooks/revit_readonly_code_guard.py`
  (inline and as codeFile). Re-run both after any edit, and bump the `v1` in line 1 (it is the expectMarker).

| File | Marker | Use | Main parameters |
|---|---|---|---|
| `revit/scout-doc.cs` | SCOUT-DOC v1 | first Revit call of a run (MARKUP 20a): title, path, doc_id, version, workshared, modified, active view, open docs | `title=` |
| `revit/scout-sheets-revisions.cs` | SCOUT-SHEETS v1 | snapshot sheet list: every sheet with its revisions (id + number on sheet) and current revision, plus project revisions | `part=all\|sheets\|revisions`, `offset=`, `numbers=`, `placeholders=1` |
| `revit/scout-warnings.cs` | SCOUT-WARNINGS v1 | warning total (no 500 cap) and top-N groups by description, optional element-id filter with per-id counts | `top=`, `ids=`, `sample=` |
| `revit/scout-elements.cs` | SCOUT-ELEMENTS v1 | targeted per-task baseline: category, family, type, level, workset, checkout + owner, location, named parameters, connectors; missing ids | `ids=` (required), `params=A\|B`, `raw=1`, `conn=1`, `offset=` |

AutoCAD: no scout library yet. `acad_readonly_guard.py` blocks `(load ...)` and `defun` in execute_lisp, so a
read-only agent cannot load a library .lsp (see the guard; a guard change is needed first).
