"""One-off: fold the 24-071 P71 afternoon-run lessons (retro/20261110-p71.md) into LESSONS.md (2026-11-12)."""
p = "LESSONS.md"
s = open(p, encoding="utf-8").read()


def add(section_hdr, rows):
    global s
    i = s.index(section_hdr)
    j = s.index("\n## ", i + 5)
    s = s[:i] + s[i:j].rstrip("\n") + "\n" + "\n".join(rows) + "\n" + s[j:]


O = [
    "| O30 | One run id and one fresh snapshot per session (and after any crash/reopen); pass the run id to EVERY agent incl. script-debugger and visual-verifier. 24-071 SAN ran on 11-10 under 20261104-1905 and its 11-05 snapshot (384 warnings vs 498 live); two debug.json said run 'unknown'. | retro/20261110-p71.md | → prompt 19a, snapshot-taker, revit-writer, script-debugger |",
    "| O31 | After a Revit crash or reopen: run a read-only reconcile scout, treat earlier digests (ownership, counts) as stale, and close any count drop before the next write (700 vs 701 fixtures was never closed). | 24-071 P71-SAN-T1.reconcile.scout.json | → prompt 19b, revit-scout |",
]
add("## O. Orchestrator and session setup", O)
R = [
    "| R47 | A Revit dry run must reach Commit: TransactionGroup { Transaction.Commit(); group.RollBack() } with an Application.FailuresProcessing lambda recording severity/text/ids (ProceedWithRollBack on errors). Write only after a dry run with commit=Committed failureTotal=0. Stage irreversible deletes (A) apart from new geometry (B), each idempotent and dry-run. | 24-071 P71-SAN-T1 v2: Commit Pending, Revit crashed | → scripter, script-debugger, revit-writer, prompt 19c, revit-conventions |",
    "| R48 | Pipe branch recipe (extends R20, broken twice): branch to a sloped main = level stub, perpendicular tee, or tee family + BreakCurve + ConnectTo; never NewTee/NewTakeoffFitting off a sloped/vertical pipe; >= 2 ft between tees, >= 1.5 ft horizontal, >= 0.5 ft drops, no jog < 2x take-off; budget the fall first, report a leg with no fall. Proven helpers: library/revit/pipe-branch-to-main.cs. | 24-071 P71-SAN-T1 v1-v6 | → scripter, script-debugger, revit-conventions, library |",
    "| R49 | Pre-commit self-checks roll back only on hard faults (length < 0.1 ft, flipped direction, no fall); 'preferred' lengths are info. The commit FailuresProcessing is the validator. | 24-071 P71-SAN-T1 v5 (15 soft violators, v6 committed clean) | → scripter, script-debugger |",
    "| R50 | The dry run reports every property expected.json checks (level, workset, symbol, host, Z) and resolves every cascade-deleted id to category + type before rollback. CopyElement keeps the SOURCE level: set FAMILY_LEVEL_PARAM then restore Z. | 24-071 P71-SAN-T1 I5 FAIL (WC on PARKING B2), fixed by P71-SAN-T2 | → scripter, revit-writer, revit-conventions |",
    "| R51 | Report created ids from the transaction (GetAddedElementIds / before-after set diff), incl. Revit-spawned elements; never identify a run's elements by 'id > N' (Jeremy edits the model concurrently). | 24-071 P71-SAN-T1.A.warnings.scout, SAN-T2 dryrun | → scripter, verifier, revit-scout |",
    "| R52 | Warnings: never counted in-script (taken before regeneration). Writer calls get_warnings before and after in separate calls (log pre_warnings in write.log.md, post_warnings real); verifier compares with that pre-run count, not the run snapshot; expected.json names the expected new-warning families (re-route: open connectors, flow direction). Extends R9. | 24-071 stage A in-script 476->476, real 476->505; verify I20 vs 384 | → scripter, revit-writer, verifier, revision-planner |",
    "| R53 | write.json is the CURRENT cumulative record: rewrite it after each stage/retry (never leave a superseded 'timeout'); per-stage detail goes in write.log.md, no <task>.A.write.json side files; result_source is required on every write.json. | 24-071 P71-SAN-T1.write.json stale 'timeout' | → revit-writer, acad-writer, verifier, schema |",
    "| R54 | Cosmetic settings (number format, widths, styles) are non-fatal: check IsValidAccuracy / IsValidForSpec, UnitTypeId.Fixed for counts, note failures and continue. The writer never skips the dry run for schedule/view tasks, even on instruction (R19 was broken). | 24-071 P71-SIDX-T1 run 1 rolled back on accuracy 1.0 | → scripter, revit-writer, prompt 19c, revit-conventions |",
    "| R55 | Schedule cell text / row counts of a schedule created in the same session read blank from GetCellText: put them on the visual-verifier (sheet PDF); the verifier marks them null, not FAIL. Body rows = data + header + title as configured. | 24-071 P71-SIDX-T1 verify I6/I7 | → verifier, visual-verifier, revision-planner, revit-conventions |",
    "| R56 | Fully qualify EVERY Autodesk.Revit.DB.Plumbing / .Mechanical / .Electrical type, incl. enums such as PipeSystemType ('inaccessible due to its protection level'); revit_api_check does not test this. | 24-071 P71-SAN-T3 v1 compile error | → scripter, script-debugger, revit-conventions |",
    "| R57 | R28 cap (2 debug cycles) was exceeded 2.5x: a dry-run-driven design iteration counts as a cycle; at cycle 2 the orchestrator asks Jeremy or changes the approach (simpler legs, manual list). Every cycle, even a one-line compile fix, writes a debug.json with the real run id. | 24-071 P71-SAN-T1 5 cycles | → script-debugger, prompt 19d |",
    "| R58 | script-debugger should run revit_api_check.py itself and report api_checked from its output only; a manual grep is 'not checked'. Until the Bash profile allows it, debug.json lists members_to_check and the orchestrator runs the checker before the next writer call. | 24-071 5 of 6 debug cycles unchecked | → script-debugger, prompt 19d; OPEN: bash_allowlist_guard edit (auto-mode classifier blocked it 11-12; Jeremy) |",
]
add("## R. Revit bridge and API", R)
Q = [
    "| Q27 | task.json names the sheet number(s) to plot (change text 'sheets: ...'); the visual-verifier exports by sheet number, never a bare view id, and applies Q10: tag/DFU overlaps on an overall plan are a note, not a FAIL. | 24-071 P71-SAN-T3.visual.json | → visual-verifier, revision-planner |",
]
add("## Q. QAQC review and markups", Q)
s = s.rstrip("\n") + """

## 2026-11-12 Pacific Edge 24-071 P71 retro roll-in (Jeremy: "revise the agents with lessons learned and push to git")
- Lessons O30-O31, R47-R58, Q27 from retro/20261110-p71.md (P71-SIDX-T1, P71-SAN-T1..T3, 11-10 afternoon, after the
  11-10 roll-in). Backup: build/backup_2026-11-12/.
- Agents: scripter (P71 rules block), revit-writer (dry run reaches Commit, no skipped dry run, pre/post warnings,
  cumulative write.json), revit-scout (reconcile after crash, ids from lists), verifier (warnings baseline, created-id
  lists, schedule cells null) in gen_agents.py; revision-planner (sheets in task.json, warning families, schedule
  checks on PDF) in gen_agents_p67.py; visual-verifier (sheet export, Q10), script-debugger (cycle cap, run id,
  revit_api_check, P71 causes), snapshot-taker (fresh per session) in gen_agents_upgrade.py.
- Not done: bash_allowlist_guard script-debugger/scripter + revit_api_check.py (R58): the auto-mode classifier blocked the
  hook edit as self-modification; needs Jeremy's OK or a permission rule.
- Schema: write.schema.json requires result_source (R53).
- revit-conventions scripting-lessons "Pacific Edge P71 lessons"; library/revit/pipe-branch-to-main.cs (from P71-SAN-T1.v6).
- MARKUP_SWARM_PROMPT run protocol 19.
"""
open(p, "w", encoding="utf-8").write(s)
print("lessons ok")
