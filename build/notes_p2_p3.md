P2 (parallel, launched same message): T1 revit-scout 27.4s, self-reported 4 MCP calls (harness tool_uses=1, unreliable post-restart);
T2 acad-scout 28.5s, self-reported 5 MCP calls (harness tool_uses=1). Both manifests clean, no errors.
P3 Revit: 2x revit-scout simultaneous, get_current_view_info x3 each: 6/6 ok, identical, 8.8s / 9.2s.
P3 AutoCAD: 2x acad-scout simultaneous, drawing get_variables x3 each: 6/6 ok, identical, 10.3s / 10.5s.
P3 limitation: identical requests cannot detect response crossover; no timing evidence of overlap vs queueing.
P3 HOOK: acad-scout entity.erase FFFFFFFF -> BLOCKED by acad_readonly_guard (frontmatter hooks work).
Baseline: Revit warnings 177; ACAD model space CIRCLE 183, TEXT 2, total 1461; CTAB P2.0.
