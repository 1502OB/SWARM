"""Offline check of revit-mcp send_code_to_revit scripts: real compile + firm lint, no Revit needed.

    python tools/revit_cs_check.py <script.cs> [<script.cs> ...] [--json <out.json>] [--quiet]
    python tools/revit_cs_check.py --selftest

What it does (2026-11-11):
1. Wraps each script exactly as the revit-mcp plugin does (namespace AIGeneratedCode, static CodeExecutor.Execute(
   Document document, object[] parameters), usings System / Linq / Autodesk.Revit.DB / Autodesk.Revit.UI /
   Collections.Generic; template read from RevitMCPCommandSet.dll) and compiles it with the plugin's own Roslyn 4.8
   against RevitAPI.dll + RevitAPIUI.dll (Revit 2025) and the .NET 8 shared framework, emitting to memory like the
   plugin. Any error the bridge would print as "Code compilation error" shows here first, mapped to script lines.
   A clean compile also proves every Revit type/member/overload exists in the 2025 API (it is stricter than
   revit_api_check.py: argument types, overloads, generics, missing usings).
2. Lints the firm's script contract (revit-conventions, scripter brief): dry-run support, transactionMode vs
   Transaction use, the done= summary line, a document guard, no UI / file ops / active-view changes, the
   writer_guard deny list, return size.
Exit 0 = PASS (no errors; warnings allowed), 1 = FAIL, 2 = tool problem (no runtime/Roslyn/Revit DLLs).

The compiler host (tools/revit_cs/RevitCsHost.cs) is built once into cache/revit_cs/ with the .NET Framework csc.exe
(no .NET SDK needed) and rebuilt when its source or the plugin's Roslyn changes.
Overrides: REVIT_DIR (default C:/Program Files/Autodesk/Revit 2025), REVIT_MCP_ROSLYN_DIR, DOTNET_ROOT.
Known gap: Revit references every assembly loaded in its AppDomain; offline we reference the whole .NET 8 shared
framework + WindowsDesktop + RevitAPI/RevitAPIUI + Newtonsoft.Json. A script using an assembly Revit has not loaded
could compile here and fail live (rare); one using a third-party add-in DLL fails here (report it, do not "fix" it).
"""
import json
import os
import re
import subprocess
import sys
import tempfile
from pathlib import Path

HERE = Path(__file__).resolve().parent
SWARM = HERE.parent
sys.path.insert(0, str(SWARM))
try:
    from swarm_paths import SWARM as _SW  # noqa: E402
    SWARM = Path(_SW)
except Exception:
    pass

CACHE = SWARM / "cache" / "revit_cs"
HOST_SRC = HERE / "revit_cs" / "RevitCsHost.cs"
HOST_DLL = CACHE / "RevitCsHost.dll"
APPDATA = Path(os.environ.get("APPDATA", str(Path.home() / "AppData/Roaming")))
REVIT_DIR = Path(os.environ.get("REVIT_DIR", r"C:\Program Files\Autodesk\Revit 2025"))
ROSLYN_DIR = Path(os.environ.get("REVIT_MCP_ROSLYN_DIR", str(
    APPDATA / "Autodesk/Revit/Addins/2025/revit_mcp_plugin/Commands/RevitMCPCommandSet/2025")))
CSC = Path(os.environ.get("WINDIR", r"C:\Windows")) / "Microsoft.NET/Framework64/v4.0.30319/csc.exe"

# The plugin's wrapper (RevitMCPCommandSet.dll, user-string heap, 2026-11-11). The user code goes right after the
# "entry point" comment; the plugin closes the method, class and namespace after it.
PREFIX = ("\r\nusing System;\r\nusing System.Linq;\r\nusing Autodesk.Revit.DB;\r\nusing Autodesk.Revit.UI;\r\n"
          "using System.Collections.Generic;\r\n\r\nnamespace AIGeneratedCode\r\n{\r\n    public static class CodeExecutor"
          "\r\n    {\r\n        public static object Execute(Document document, object[] parameters)\r\n        {\r\n"
          "            // User code entry point\r\n            ")
SUFFIX = "\r\n        }\r\n    }\r\n}\r\n"
OFFSET = PREFIX.count("\n")  # wrapped line = script line + OFFSET

# Compiler warnings worth a scripter's attention; the rest (unused variables etc.) are dropped as noise.
KEEP_WARN = {
    "CS0618": "obsolete API (may be removed in Revit 2026)", "CS0612": "obsolete API",
    "CS0162": "unreachable code", "CS0665": "assignment in a condition", "CS1717": "self-assignment",
    "CS0252": "reference comparison", "CS0253": "reference comparison", "CS0472": "comparison always true/false",
    "CS0464": "comparison with null always false", "CS1718": "comparison to itself", "CS0108": "hides member",
    "CS4014": "un-awaited call",
}


# ----------------------------------------------------------------------------------------------- environment
def _newest(base: Path, major: str):
    if not base.is_dir():
        return None
    vers = [d for d in base.iterdir() if d.is_dir() and d.name.split(".")[0] == major]
    vers.sort(key=lambda d: [int(x) if x.isdigit() else 0 for x in d.name.split(".")])
    return vers[-1] if vers else None


def env():
    roots = [os.environ.get("DOTNET_ROOT", ""), r"C:\Program Files\dotnet\x64", r"C:\Program Files\dotnet"]
    for r in filter(None, roots):
        exe = Path(r) / "dotnet.exe"
        core = _newest(Path(r) / "shared/Microsoft.NETCore.App", "8")
        if exe.is_file() and core:
            desk = _newest(Path(r) / "shared/Microsoft.WindowsDesktop.App", "8")
            break
    else:
        raise SystemExit("revit_cs_check: no dotnet.exe with a .NET 8 runtime (Revit 2025 installs one); set DOTNET_ROOT")
    for p in (ROSLYN_DIR / "Microsoft.CodeAnalysis.dll", ROSLYN_DIR / "Microsoft.CodeAnalysis.CSharp.dll",
              REVIT_DIR / "RevitAPI.dll", REVIT_DIR / "RevitAPIUI.dll"):
        if not p.is_file():
            raise SystemExit(f"revit_cs_check: missing {p} (set REVIT_DIR / REVIT_MCP_ROSLYN_DIR)")
    return exe, core, desk


def _is_managed(p: Path) -> bool:
    """True if the PE file has a CLI header (data directory 14), i.e. it is a .NET assembly."""
    try:
        with open(p, "rb") as f:
            b = f.read(1024)
        pe = int.from_bytes(b[0x3C:0x40], "little")
        if b[pe:pe + 4] != b"PE\0\0":
            return False
        opt = pe + 24
        magic = int.from_bytes(b[opt:opt + 2], "little")
        dd = opt + (112 if magic == 0x20B else 96)
        return int.from_bytes(b[dd + 14 * 8:dd + 14 * 8 + 4], "little") != 0
    except Exception:
        return False


def references(core: Path, desk):
    refs = [p for d in (core, desk) if d for p in sorted(d.glob("*.dll")) if _is_managed(p)]
    refs += [REVIT_DIR / "RevitAPI.dll", REVIT_DIR / "RevitAPIUI.dll"]
    nj = ROSLYN_DIR / "Newtonsoft.Json.dll"
    if nj.is_file():
        refs.append(nj)
    return refs


def build_host(core: Path):
    """Compile RevitCsHost.cs with the .NET Framework csc (C# 5) against .NET 8 + the plugin's Roslyn."""
    CACHE.mkdir(parents=True, exist_ok=True)
    stamp = CACHE / "host.stamp"
    roslyn = ROSLYN_DIR / "Microsoft.CodeAnalysis.CSharp.dll"
    key = f"{HOST_SRC.stat().st_mtime_ns}|{roslyn.stat().st_mtime_ns}|{core}"
    if HOST_DLL.is_file() and stamp.is_file() and stamp.read_text() == key:
        return
    if not CSC.is_file():
        raise SystemExit(f"revit_cs_check: {CSC} not found (needed once to build the compiler host)")
    refs = ["System.Private.CoreLib", "System.Runtime", "System.Collections", "System.Collections.Immutable",
            "System.Console", "System.Linq", "System.Text.Encoding.Extensions"]
    cmd = [str(CSC), "-nologo", "-noconfig", "-nostdlib+", "-target:exe", f"-out:{HOST_DLL}"]
    cmd += [f"-r:{core / (r + '.dll')}" for r in refs]
    cmd += [f"-r:{ROSLYN_DIR / 'Microsoft.CodeAnalysis.dll'}", f"-r:{roslyn}", str(HOST_SRC)]
    r = subprocess.run(cmd, capture_output=True, text=True)
    if r.returncode != 0:
        raise SystemExit("revit_cs_check: host build failed:\n" + r.stdout + r.stderr)
    # The host runs on .NET 8 and loads the plugin's Roslyn from its own folder (probed without a deps.json).
    import shutil
    for n in ("Microsoft.CodeAnalysis.dll", "Microsoft.CodeAnalysis.CSharp.dll"):
        shutil.copy2(ROSLYN_DIR / n, CACHE / n)
    (CACHE / "RevitCsHost.runtimeconfig.json").write_text(json.dumps({"runtimeOptions": {
        "tfm": "net8.0", "framework": {"name": "Microsoft.NETCore.App", "version": "8.0.0"},
        "rollForward": "LatestPatch"}}, indent=2))
    stamp.write_text(key)


# ----------------------------------------------------------------------------------------------- compile
def compile_all(paths):
    exe, core, desk = env()
    build_host(core)
    tmp = Path(tempfile.mkdtemp(prefix="revit_cs_", dir=str(CACHE)))
    try:
        reflist = tmp / "refs.txt"
        reflist.write_text("\n".join(str(p) for p in references(core, desk)), encoding="utf-8")
        wrapped = {}
        for i, p in enumerate(paths):
            code = Path(p).read_text(encoding="utf-8-sig")
            w = tmp / f"s{i}.cs"
            w.write_bytes((PREFIX + code + SUFFIX).encode("utf-8"))  # byte-exact: text mode would double \r
            wrapped[str(w)] = (p, code.count("\n") + 1)
        r = subprocess.run([str(exe), str(HOST_DLL), str(reflist)] + list(wrapped), capture_output=True,
                           text=True, encoding="utf-8", errors="replace")
        if r.returncode not in (0, 1) or "FILE\t" not in r.stdout:
            raise SystemExit("revit_cs_check: compiler host failed:\n" + r.stdout[-2000:] + r.stderr[-2000:])
        out = {p: {"errors": [], "warnings": []} for p in paths}
        cur = None
        for line in r.stdout.splitlines():
            f = line.split("\t")
            if f[0] == "FILE":
                cur = wrapped[f[1]]
            elif f[0] == "DIAG" and cur and len(f) >= 6:
                src, nlines = cur
                sev, cid, wl, col, msg = f[1], f[2], int(f[3]), int(f[4]), f[5]
                ln = wl - OFFSET
                d = {"line": ln if 1 <= ln <= nlines else 0, "live_line": wl - 1, "col": col, "id": cid, "message": msg}
                if d["line"] == 0:
                    d["message"] += " (in the plugin wrapper: e.g. a code path without return, or unbalanced braces)"
                if sev == "Error":
                    out[src]["errors"].append(d)
                elif cid in KEEP_WARN:
                    d["why"] = KEEP_WARN[cid]
                    out[src]["warnings"].append(d)
        return out
    finally:
        import shutil
        shutil.rmtree(tmp, ignore_errors=True)


# ----------------------------------------------------------------------------------------------- lint
_TOKEN = re.compile(r'/\*.*?\*/|//[^\n]*|@\$?"(?:[^"]|"")*"|\$?"(?:\\.|[^"\\\n])*"|\'(?:\\.|[^\'\\\n])\'', re.DOTALL)


def blank(code: str) -> str:
    """Comments and string contents blanked, newlines kept (so offsets and line numbers stay valid)."""
    def repl(m):
        t = m.group(0)
        if t.startswith("/"):
            return re.sub(r"[^\n]", " ", t)
        q = t[0] if t[0] in "'\"" else '"'
        return q + re.sub(r"[^\n]", " ", t[1:-1] if len(t) > 1 else "")[:max(len(t) - 2, 0)] + q if len(t) > 1 else t
    return _TOKEN.sub(repl, code)


def _deny():
    try:
        sys.path.insert(0, str(SWARM / "claude" / "hooks"))
        from writer_guard import REVIT_DENY  # the same list the live guard enforces
        return REVIT_DENY
    except Exception:
        return re.compile(r"\b(Save|SaveAs|SynchronizeWithCentral|RelinquishOwnership|Close|PostCommand|"
                          r"Process\.Start|OpenDocumentFile|DetachFromCentral\w*|Audit|SetAudit)\b")


WRITE_RE = re.compile(r"\b(new\s+(Sub)?Transaction\b|\.Delete\s*\(|Document\.Delete|"
                      r"\.Create\w*\s*\(\s*(document|doc|_doc|d)\s*[,)]|\.Set\s*\(|"
                      r"NewFamilyInstance|NewElbowFitting|NewTeeFitting|NewTransitionFitting|ConnectTo\s*\(|"
                      r"ElementTransformUtils\.|\.Move\s*\(|CopyElements?|\.SetValueString\s*\(|\.Text\s*=[^=])")
# Calls that open UI (TaskDialogResult / TaskDialogCommonButtons are plain enums and are fine).
UI_RE = re.compile(r"\b(TaskDialog\s*\.\s*Show|new\s+TaskDialog\b|MessageBox\s*\.|PromptFor\w+|PickObjects?|PickPoint|"
                   r"PickElementsByRectangle|new\s+\w*Form\s*\(|ShowDialog|new\s+Window\s*\()")
VIEW_RE = re.compile(r"\b(ActiveView\s*=[^=]|RequestViewChange|OpenAndActivateDocument|ShowElements)\b")
# Writing a result/shared-parameter file is normal (File.WriteAllText); these are not.
IO_RE = re.compile(r"\b(Environment\.Exit|Process\.|WebClient|HttpClient|Assembly\.Load\w*|Activator\.)")
IO_WARN_RE = re.compile(r"\b(File\.(Delete|Move|Replace)|Directory\.(Delete|Move)|Thread\.Sleep)\b")
MODE_RE = re.compile(r'(?:transactionMode|\bmode)\W{0,3}"?(auto|none)"?', re.I)  # 23-073 headers also say mode "auto"


def line_of(code, pos):
    return code.count("\n", 0, pos) + 1


def lint(path: str, code: str):
    errs, warns = [], []
    b = blank(code)

    def add(lst, rule, pos, msg):
        lst.append({"rule": rule, "line": line_of(code, pos) if pos is not None else 0, "message": msg})

    header = "\n".join(code.splitlines()[:25])
    m = MODE_RE.search(header)
    mode = m.group(1).lower() if m else None
    if mode is None:
        add(warns, "L1-mode", None, 'header does not state transactionMode "auto" or "none" (the writer needs it)')
    # A Transaction opened only when the document is not already modifiable (dry-run simulation) is fine in auto mode.
    starts = [x for x in re.finditer(r"\bnew\s+(Transaction|TransactionGroup)\s*\(", b)
              if "IsModifiable" not in b[max(0, b.rfind("\n", 0, x.start()) - 200):x.start()]]
    cleanup = ".cleanup." in Path(path).name.lower()
    if mode == "auto" and starts:
        add(errs, "L2-nested-transaction", starts[0].start(),
            f"transactionMode auto already runs the code inside a Transaction: new {starts[0].group(1)} here throws "
            "at runtime. Use SubTransaction, or declare transactionMode \"none\" and manage your own transactions")
    writes = list(WRITE_RE.finditer(b))
    if mode == "none" and writes and not starts:
        add(errs, "L3-no-transaction", writes[0].start(),
            'transactionMode "none" with model changes but no Transaction: Revit rejects the change')
    dry = re.search(r'"dryrun"', code, re.I)
    if not dry and re.search(r'"probe"', code, re.I):
        dry = True
        add(warns, "L4-dryrun-name", None, 'dry run is named "probe": the writer sends ["dryrun"]; use that name')
    if writes and not dry:
        add(warns if cleanup else errs, "L4-dryrun", None, 'no "dryrun" mode: every Revit script supports parameters '
            '["dryrun"] (read-only preflight that returns before any change)' + (" (cleanup script: recommended)" if cleanup else ""))
    if writes and dry:
        dpos = re.search(r"\bif\s*\(\s*!?\s*(dry\w*|isDry\w*|probe\w*)\b", b, re.I)
        first_write = writes[0].start()
        if dpos is None:
            add(warns, "L5-dryrun-branch", None, "dryrun is read but no `if (dry...)` branch was found: check the "
                "dry run returns before any change")
        elif first_write < dpos.start():
            add(warns, "L5-dryrun-order", first_write, "a model change appears before the first dry-run branch: "
                "in a dry run nothing may change (auto mode commits it)")
    if "done=" not in code:
        add(errs if writes else warns, "L6-summary", None,
            "the return does not end with a `done=<n> skipped=<n> failed=<n>` line")
    if not re.search(r"\bdocument\s*\.\s*(Title|PathName)\b", b):
        add(warns, "L7-doc-guard", None, "no document.Title/PathName check: guard against the wrong model being "
            "active (two Revit instances share one bridge)")
    for rx, rule, msg in ((_deny(), "L8-denied", "blocked by writer_guard (file operations belong to revit-ops)"),
                          (UI_RE, "L9-ui", "UI call: the bridge has no UI; a modal dialog freezes Revit"),
                          (VIEW_RE, "L10-active-view", "changes the active view/document (never allowed)"),
                          (IO_RE, "L11-io", "file/process/thread/reflection call not allowed in a bridge script")):
        for x in rx.finditer(b):
            add(errs, rule, x.start(), f"`{x.group(0).strip()}`: {msg}")
            break
    x = IO_WARN_RE.search(b)
    if x:
        add(warns, "L11-io", x.start(), f"`{x.group(0)}`: deleting/moving files or sleeping on the Revit thread")
    if re.search(r"\bConsole\s*\.\s*Write", b):
        add(warns, "L12-console", None, "Console output is lost on the bridge; put it in the return string")
    if re.search(r"\b(GetMethod|InvokeMember)\b", b):
        add(errs, "L13-reflection", None, "reflection calls are blocked by writer_guard")
    if not re.search(r"\breturn\b", b):
        add(errs, "L14-return", None, "no return: the bridge reports nothing")
    if re.search(r"\bforeach\b[^{;]*\{[^}]*\b(sb|lines?|log|out\w*)\s*\.\s*(Append|Add)\w*\s*\(", b) and \
            "Take(" not in b and "Length >" not in b and "Count >" not in b:
        add(warns, "L15-return-size", None, "the return is built in a loop with no cap: keep it under ~3 KB "
            "(the bridge truncates; put long lists in counts or a file)")
    return mode, errs, warns


# ----------------------------------------------------------------------------------------------- main
def check(paths):
    comp = compile_all(paths)
    results = []
    for p in paths:
        code = Path(p).read_text(encoding="utf-8-sig")
        mode, lerr, lwarn = lint(p, code)
        c = comp[p]
        status = "FAIL" if c["errors"] or lerr else "PASS"
        results.append({"file": str(Path(p)).replace("\\", "/"), "status": status, "transaction_mode": mode,
                        "compile": c, "lint": {"errors": lerr, "warnings": lwarn}})
    return results


def report(results, quiet=False):
    for r in results:
        c, lt = r["compile"], r["lint"]
        print(f"{r['status']}  {r['file']}  compile: {len(c['errors'])} errors, {len(c['warnings'])} warnings; "
              f"lint: {len(lt['errors'])} errors, {len(lt['warnings'])} warnings; mode={r['transaction_mode']}")
        if quiet:
            continue
        for e in c["errors"][:15]:
            print(f"  ERROR {e['id']} line {e['line']}:{e['col']} {e['message']}")
        for e in lt["errors"]:
            print(f"  ERROR {e['rule']} line {e['line']}: {e['message']}")
        for w in c["warnings"][:10]:
            print(f"  warn  {w['id']} line {w['line']}: {w['message']}")
        for w in lt["warnings"]:
            print(f"  warn  {w['rule']} line {w['line']}: {w['message']}")


SELFTEST_OK = '''// selftest: transactionMode "auto". parameters: ["dryrun"]
bool dry = parameters != null && parameters.Length > 0 && Convert.ToString(parameters[0]) == "dryrun";
if (document.Title == null) return "NO_GO";
var walls = new FilteredElementCollector(document).OfClass(typeof(Wall)).GetElementCount();
if (dry) return "DRYRUN walls=" + walls + "\\ndone=0 skipped=0 failed=0";
var p = document.ProjectInformation.LookupParameter("Project Name");
if (p != null) p.Set("x");
return "walls=" + walls + "\\ndone=1 skipped=0 failed=0";
'''
SELFTEST_BAD = '''// selftest bad: transactionMode "auto"
var t = new Transaction(document, "x"); t.Start();
Wall w = document.GetElement(new ElementId(1L)) as Wall;
double z = w.NoSuchMember;
TaskDialog.Show("a", "b");
return "x";
'''


def selftest():
    d = Path(tempfile.mkdtemp(prefix="revit_cs_selftest_"))
    ok, bad = d / "ok.cs", d / "bad.cs"
    ok.write_text(SELFTEST_OK, encoding="utf-8")
    bad.write_text(SELFTEST_BAD, encoding="utf-8")
    res = check([str(ok), str(bad)])
    report(res)
    r_ok, r_bad = res
    got = {e["id"] for e in r_bad["compile"]["errors"]} | {e["rule"] for e in r_bad["lint"]["errors"]}
    want = {"CS1061", "L2-nested-transaction", "L4-dryrun", "L6-summary", "L9-ui"}
    passed = r_ok["status"] == "PASS" and want <= got and any(e["line"] == 4 for e in r_bad["compile"]["errors"])
    print("SELFTEST", "PASS" if passed else f"FAIL (bad got {sorted(got)})")
    return 0 if passed else 1


def main(argv):
    if "--selftest" in argv:
        return selftest()
    out = None
    if "--json" in argv:
        i = argv.index("--json")
        out = argv[i + 1]
        del argv[i:i + 2]
    quiet = "--quiet" in argv
    paths = [a for a in argv if not a.startswith("--")]
    if not paths:
        print(__doc__)
        return 2
    for p in paths:
        if not Path(p).is_file():
            print(f"revit_cs_check: no such file {p}")
            return 2
    res = check(paths)
    report(res, quiet)
    if out:
        Path(out).write_text(json.dumps(res[0] if len(res) == 1 else res, indent=2), encoding="utf-8")
    return 1 if any(r["status"] == "FAIL" for r in res) else 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
