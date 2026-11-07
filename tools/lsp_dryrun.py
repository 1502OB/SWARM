"""Dry-run an AutoLISP script headless, on a COPY of the drawing, without the live AutoCAD (2026-11-06).

    python C:/Users/Jeremy/swarm/tools/lsp_dryrun.py <script.lsp> <drawing.dwg> <TASK> [--run] [--timeout 120]

Uses AutoCAD 2027's Core Console (accoreconsole.exe, full AutoCAD only) in its own process:
  1. copies <drawing.dwg> to swarm/scratch/dryrun/<TASK>/<timestamp>/ (the original and the live session are never
     opened or changed; the copy is discarded with QUIT, nothing is saved),
  2. loads swarm/library/acad/swarm_lib.lsp (if present) and the script,
  3. calls (c:<TASK>-PLAN) (the dry run by our naming convention) and, with --run, (c:<TASK>-RUN) on the copy,
     reporting entity counts before/after,
  4. parses the console log.
Status in the printed JSON:
  clean              loaded and PLAN (and RUN) returned without an error
  needs_full_autocad loaded fine, but PLAN/RUN stopped on ActiveX (vla-*, vlax-*, layoutlist): the Core Console has
                     no ActiveX, so the writer's dry run in the live AutoCAD must cover that part (not a script error)
  errors             a load error (malformed list, unbalanced parens), an undefined function, or a runtime error
  plan_failed        PLAN/RUN ran but returned its own ABORT / ROLLED_BACK / failed>0 (e.g. its preflight refused)
Exit 0 = clean or needs_full_autocad, 1 = errors or plan_failed, 2 = usage / Core Console missing or timed out.
Probe 2026-11-06 (P003 copy): load, plain LISP, entmake and ssget work; vlax-get-acad-object returns nil and
layoutlist is undefined. Run accoreconsole from Python/PowerShell: started from Git Bash it hangs at the prompt.
"""
import json
import re
import shutil
import subprocess
import sys
import time
from pathlib import Path

CORE = Path("C:/Program Files/Autodesk/AutoCAD 2027/accoreconsole.exe")
SWARM = Path("C:/Users/Jeremy/swarm")
LIB = SWARM / "library" / "acad" / "swarm_lib.lsp"
ACTIVEX = re.compile(r"VLA-OBJECT nil|no function definition: (VLA-|VLAX-|LAYOUTLIST)|ActiveX|vlax-get-acad-object",
                     re.IGNORECASE)
ERROR = re.compile(r"; error: (.+)|^error: (.+)|malformed list|unbalanced", re.IGNORECASE)


def lisp_str(p: Path) -> str:
    return '"' + p.as_posix().replace('"', '\\"') + '"'


def main():
    args = sys.argv[1:]
    run_too = "--run" in args
    timeout = 120
    if "--timeout" in args:
        timeout = int(args[args.index("--timeout") + 1])
        del args[args.index("--timeout"):args.index("--timeout") + 2]
    args = [a for a in args if a != "--run"]
    if len(args) != 3:
        print(__doc__.strip().splitlines()[2].strip())
        sys.exit(2)
    script, dwg, task = Path(args[0]).resolve(), Path(args[1]), args[2]
    if not CORE.exists():
        print(json.dumps({"status": "unavailable", "error": f"{CORE} not found (Core Console needs full AutoCAD)"}))
        sys.exit(2)
    if not script.is_file() or not dwg.is_file():
        print(json.dumps({"status": "errors", "error": f"missing: {script if not script.is_file() else dwg}"}))
        sys.exit(1)
    base = SWARM / "scratch" / "dryrun" / re.sub(r"[^A-Za-z0-9_.-]", "_", task)
    stamp, k = time.strftime("%Y%m%d-%H%M%S"), 0
    work = base / stamp
    while work.exists():  # two runs in the same second must not share a folder
        k += 1
        work = base / f"{stamp}-{k}"
    work.mkdir(parents=True)
    copy = work / dwg.name  # same file name as the original: scripts check DWGNAME in their preflight
    shutil.copy2(dwg, copy)
    out = work / "result.txt"
    cmds = [f"c:{task}-PLAN"]
    if run_too:
        cmds.append(f"c:{task}-RUN")
    # wrapper: log every step to result.txt (one line each, no newlines inside), never save
    lines = [
        "(defun swdry:log (s / f) (if (setq f (open " + lisp_str(out) + ' "a")) (progn (write-line s f) (close f))))',
        '(defun swdry:n () (if (setq ss (ssget "_X")) (sslength ss) 0))',
        '(swdry:log (strcat "entities_before " (itoa (swdry:n))))',
    ]
    if LIB.exists():
        lines.append(f'(swdry:log (strcat "load_lib " (vl-princ-to-string (vl-catch-all-apply (quote load) (list {lisp_str(LIB)})))))')
    lines.append(f'(setq swdry:r (vl-catch-all-apply (quote load) (list {lisp_str(script)})))')
    lines.append('(swdry:log (if (vl-catch-all-error-p swdry:r) (strcat "load_error " (vl-catch-all-error-message swdry:r)) "load_ok"))')
    for c in cmds:
        sym = c.lower()
        lines.append(f'(if (not (vl-catch-all-error-p swdry:r)) (progn (setq swdry:c (vl-catch-all-apply (quote {sym}) nil)) '
                     f'(swdry:log (if (vl-catch-all-error-p swdry:c) (strcat "{c}_error " (vl-catch-all-error-message swdry:c)) '
                     f'(strcat "{c}_ok " (vl-princ-to-string swdry:c))))))')
    lines += ['(swdry:log (strcat "entities_after " (itoa (swdry:n))))', "_.QUIT", "_Y", ""]
    scr = work / "dryrun.scr"
    scr.write_bytes("\r\n".join(lines).encode("utf-8"))
    log = work / "console.log"
    t0 = time.time()
    try:
        with open(log, "wb") as lf:
            p = subprocess.run([str(CORE), "/i", str(copy), "/s", str(scr), "/l", "en-US"], stdin=subprocess.DEVNULL,
                               stdout=lf, stderr=subprocess.STDOUT, timeout=timeout)
        code = p.returncode
    except subprocess.TimeoutExpired:
        print(json.dumps({"status": "timeout", "work": work.as_posix(),
                          "error": f"Core Console did not finish in {timeout} s"}))
        sys.exit(2)
    text = log.read_bytes().replace(b"\x00", b"").decode("utf-8", "replace")
    steps = out.read_text(encoding="utf-8", errors="replace").splitlines() if out.exists() else []
    console_errors = [m.group(0).strip() for m in map(ERROR.search, text.splitlines()) if m]
    step_errors = [s for s in steps if "_error " in s]
    activex = [e for e in step_errors + console_errors if ACTIVEX.search(e)]
    real = [e for e in step_errors + console_errors if not ACTIVEX.search(e)]
    # the final undefined-command line of the console for our own QUIT etc. is not an error; dedupe
    real = list(dict.fromkeys(real))
    loaded = "load_ok" in steps
    # a PLAN/RUN that returned but reported its own failure (ABORT / ROLLED_BACK / failed>0) is not clean
    aborted = [s for s in steps if "_ok " in s and re.search(r"\bABORT\b|\bROLLED_BACK\b|failed=[1-9]", s)]
    if real or not loaded:
        status = "errors"
    elif activex:
        status = "needs_full_autocad"
    elif aborted:
        status = "plan_failed"
    else:
        status = "clean"
    report = {"status": status, "task": task, "loaded": loaded, "steps": steps, "errors": real, "aborted": aborted,
              "activex_limits": list(dict.fromkeys(activex)), "seconds": round(time.time() - t0, 1),
              "exit_code": code, "work": work.as_posix(), "copy_of": dwg.as_posix()}
    print(json.dumps(report, indent=1))
    sys.exit(1 if status in ("errors", "plan_failed") else 0)


if __name__ == "__main__":
    main()
