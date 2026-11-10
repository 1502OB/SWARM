# Lint sweep 2026-11-09 (new W5/W6/W7 + per-defun local collisions)

Scope: all 224 `scripts/*.lsp` + `library/acad/swarm_lib.lsp` + `library/acad/test_swarm_lib.lsp` (226 files), with
the new `tools/lsp_lint.py` (W5 A36, W6 A31, W7 A43) and `tools/lsp_collide.py` (local-collisions A42, library =
swarm_lib.lsp). No .lsp was edited. Library files were the working copy at sweep time (another agent was editing
swarm_lib.lsp), so re-run before committing library changes:

    python tools/lsp_lint.py scripts/*.lsp library/acad/*.lsp
    for f in scripts/*.lsp library/acad/*.lsp; do python tools/lsp_collide.py "$f" library/acad/swarm_lib.lsp; done

## Totals

| Check | Hits | Files |
|---|---|---|
| W5 c: command ends in (princ), no result string (A36) | 20 | 8 |
| W6 (setq v (or ...)) used as a value (A31) | 1 | 1 |
| W7 entget data passed where an ename is expected (A43) | 1 | 1 |
| local-collisions (case duplicates inside one defun, A42) | 1 | 1 |
| global defun-vs-setq collisions (existing check, A28) | - | 10 (listed below) |
| library/acad/*.lsp | 0 new warnings | - |

The new checks find all three known failures of the Mansfield run in the version that failed, and nothing in the
fixed versions: M18-R3-T15.v1 (W7 + local-collision; T15 current is clean), M18-R3-T9.v1 (W6; v2/v3 clean).

## W5 per file (top offenders)

| File | W5 | Note |
|---|---|---|
| scripts/C22-R7-T1.lsp | 4 | PLAN/RUN/CHECK/CLEAN all `(r7-guard ...) (princ)` |
| scripts/C22-R7-T1.v1.lsp | 4 | same (old version) |
| scripts/M18-R3-T11b.lsp | 3 | named in A36 |
| scripts/M18-R3-T12b.lsp | 3 | named in A36 |
| scripts/M18-R3-T10d.lsp | 2 | named in A36 |
| scripts/M18-TB-T2.lsp | 2 | |
| scripts/TB_delta5.lsp | 1 | |
| scripts/enc_planmap.lsp | 1 | |

C22-R7-T1.v3-v8 are clean (the unversioned C22-R7-T1.lsp still has the pattern); the M18 T10d/T11b/T12b scripts are
exactly the three writers that had to infer results (A36).

## W6 / W7 / local collisions

| File | Code | Line | Finding |
|---|---|---|---|
| scripts/M18-R3-T9.v1.lsp | W6 | 161 | `(setq bt (or (tblobjname ...) (tblobjname ...)))` then `(entnext bt)` - the "lentityp T" failure |
| scripts/M18-R3-T15.v1.lsp | W7 | 340 | `(m15:noteh nE ...)`: nE = `(car list-of (swl:ent h))`, noteh -> m15:mk-note -> `vlax-ename->vla-object` (found interprocedurally) |
| scripts/M18-R3-T15.v1.lsp | local | - | `c:M18-R3-T15-CHECK` declares both `ne` and `nE` |

False-positive check: the first W6 draft flagged C22-R2-T1c `mv` and M18-R1-T3.core `fok` (only used as tests);
fixed (the `( / locals)` list is not a use, and boolean-only uses / all-predicate `or` are skipped). Those two are
clean now.

## Existing checks (unchanged codes, for reference)

W2 36, W3 35, W1 12, W4 2; one pre-existing error: scripts/C22-R8-T1.lsp E1 line 405 (extra ')'; the old lint
reports it too; later versions v2-v10 exist).

Global defun-vs-setq collisions (lsp_collide line 1): C22-R8-T1 (+v2-v7) `r8:vp`, ENC-C1 `enc:err`,
M18-R2-common.v1 `c18r2:band`/`c18r2:nbox`, M18-R3-T1.v1 `m18r3:mv`.
