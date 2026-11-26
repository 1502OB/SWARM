"""Offline check of revit-mcp send_code_to_revit scripts: real compile + firm lint, no Revit needed.

    python tools/revit_cs_check.py <script.cs> [<script.cs> ...] [--json <out.json>] [--quiet]
    python tools/revit_cs_check.py <script.cs> [<cleanup.cs>] --evidence --task-json <task.json> [--expected-json <e.json>]
    python tools/revit_cs_check.py <library/revit/x.cs> --evidence --library
    python tools/revit_cs_check.py <script.cs> --reuse-precheck <precheck.json> [--run-id <id>] [--task-id <id>]
        [--task-json <task.json> --expected-json <e.json>] --emit-file | --emit-code
    (--emit-file, 2026-11-25 R70/R72: prints sha256, the absolute codeFile path and the line-1 marker instead of the
     code; the writer passes them as send_code_to_revit codeFile / expectSha256 / expectMarker, so the script text
     never enters the writer's context and is never retyped. Needs the codefile bridge patch.)
    python tools/revit_cs_check.py --selftest

Precheck reuse (2026-11-25, see "precheck reuse" below): --evidence with --task-json/--expected-json adds a `bound`
object {task_path, task_sha256, expected_path, expected_sha256} to every evidence row; the checker copies it to the
precheck's top level. A bound PASS is reused whenever the toolchain, every script/cleanup sha and the task.json /
expected.json sha still match (any session, any run id). --evidence --library (scripts under swarm/library/ only)
adds "library": true; a library precheck matches on script sha + toolchain alone. Prechecks with neither field keep
the old rule (run_id + task_id + sha). Gotcha errors (G1, G2) are re-run on reuse of a bound or library precheck; an
un-waived one blocks it. Legacy prechecks (made before the G rules) are not re-judged by rules they never saw.

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
3. Gotcha rules (G1 tee on a sloped/vertical main, G2 system type change = errors; G3 exact-name lookup, G2 system
   merge and G4 obstacle categories = warnings) for live failures that used to pass both; waive one with a comment
   `// gotcha-ok: G<n> <reason>` (listed under lint.waivers).
Exit 0 = PASS (no errors; warnings allowed), 1 = FAIL, 2 = tool problem (no runtime/Roslyn/Revit DLLs).

The compiler host (tools/revit_cs/RevitCsHost.cs) is built once into cache/revit_cs/ with the .NET Framework csc.exe
(no .NET SDK needed) and rebuilt when its source or the plugin's Roslyn changes.
Overrides: REVIT_DIR (default C:/Program Files/Autodesk/Revit 2025), REVIT_MCP_ROSLYN_DIR, DOTNET_ROOT.
Known gap: Revit references every assembly loaded in its AppDomain; offline we reference the whole .NET 8 shared
framework + WindowsDesktop + RevitAPI/RevitAPIUI + Newtonsoft.Json. A script using an assembly Revit has not loaded
could compile here and fail live (rare); one using a third-party add-in DLL fails here (report it, do not "fix" it).
"""
import hashlib
import json
import os
import re
import shutil
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
    """Publish one complete compiler directory per toolchain, even on parallel cold starts."""
    CACHE.mkdir(parents=True, exist_ok=True)
    roslyn = [ROSLYN_DIR / n for n in ("Microsoft.CodeAnalysis.dll", "Microsoft.CodeAnalysis.CSharp.dll")]
    key = [str(core), *(str(p.stat().st_mtime_ns) for p in (HOST_SRC, *roslyn))]
    target = CACHE / ("host-" + hashlib.sha256("|".join(key).encode("utf-8")).hexdigest()[:24])
    host = target / HOST_DLL.name
    if host.is_file():
        return host
    if not CSC.is_file():
        raise SystemExit(f"revit_cs_check: {CSC} not found (needed once to build the compiler host)")
    refs = ["System.Private.CoreLib", "System.Runtime", "System.Collections", "System.Collections.Immutable",
            "System.Console", "System.Linq", "System.Text.Encoding.Extensions"]
    with tempfile.TemporaryDirectory(prefix="host-build-", dir=CACHE) as directory:
        staging = Path(directory)
        cmd = [str(CSC), "-nologo", "-noconfig", "-nostdlib+", "-target:exe", f"-out:{staging / host.name}"]
        cmd += [f"-r:{core / (r + '.dll')}" for r in refs]
        cmd += [f"-r:{p}" for p in roslyn] + [str(HOST_SRC)]
        r = subprocess.run(cmd, capture_output=True, text=True)
        if r.returncode != 0:
            raise SystemExit("revit_cs_check: host build failed:\n" + r.stdout + r.stderr)
        for assembly in roslyn:
            shutil.copy2(assembly, staging / assembly.name)
        (staging / host.with_suffix(".runtimeconfig.json").name).write_text(json.dumps({"runtimeOptions": {
            "tfm": "net8.0", "framework": {"name": "Microsoft.NETCore.App", "version": "8.0.0"},
            "rollForward": "LatestPatch"}}, indent=2))
        try:
            staging.rename(target)
        except OSError:
            if not host.is_file():
                raise
    return host


# ----------------------------------------------------------------------------------------------- compile
def compile_all(paths, sources=None):
    exe, core, desk = env()
    host = build_host(core)
    tmp = Path(tempfile.mkdtemp(prefix="revit_cs_", dir=str(CACHE)))
    try:
        reflist = tmp / "refs.txt"
        reflist.write_text("\n".join(str(p) for p in references(core, desk)), encoding="utf-8")
        wrapped = {}
        for i, p in enumerate(paths):
            code = sources[p][1] if sources is not None else Path(p).read_text(encoding="utf-8-sig")
            w = tmp / f"s{i}.cs"
            w.write_bytes((PREFIX + code + SUFFIX).encode("utf-8"))  # byte-exact: text mode would double \r
            wrapped[str(w)] = (p, code.count("\n") + 1)
        r = subprocess.run([str(exe), str(host), str(reflist)] + list(wrapped), capture_output=True,
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


# L16 (2026-11-25, LESSONS R116 / retro 20261120-ATZ P3): ATZ-R2-T1 and ATZ-R13-T1 v1 threw before their try and
# returned a bare "Object reference" / "target of an invocation" with no stage. The whole body, pre-checks
# included, must sit inside one top-level try whose catch (Exception) returns FAILED @<stage>.
PRE_TRY_API_RE = re.compile(r"\b(document|doc|uidoc|uiapp|app)\s*\.\s*[A-Za-z_]\w*\s*(<[^;()]*>)?\s*\(|\bnew\s+(FilteredElementCollector|ElementId)\s*\(")

def outer_catch(b):
    """Return (pre_api_match_or_None, has_try, has_catch_return) for blanked code b."""
    depth, first_try, catches, top = 0, None, [], []
    i, n = 0, len(b)
    while i < n:
        c = b[i]
        if c == "{":
            depth += 1
        elif c == "}":
            depth -= 1
        elif depth == 0 and (i == 0 or not (b[i - 1].isalnum() or b[i - 1] == "_")):
            if first_try is None and b.startswith("try", i) and not (b[i + 3:i + 4].isalnum() or b[i + 3:i + 4] == "_"):
                first_try = i
            m = re.match(r"catch\s*\(\s*(System\s*\.\s*)?Exception\b[^)]*\)\s*\{", b[i:])
            if m:
                j = k = i + m.end(); d = 1
                while k < n and d:
                    d += {"{": 1, "}": -1}.get(b[k], 0); k += 1
                catches.append(b[j:k]); i = k; continue
        if depth == 0 and first_try is None:
            top.append((i, c))
        i += 1
    pre_hit = None
    if top:
        text = "".join(ch for _, ch in top); offs = [p for p, _ in top]
        pos = 0
        for stmt in text.split(";"):
            if "=>" not in stmt:
                m = PRE_TRY_API_RE.search(stmt)
                if m:
                    pre_hit = (offs[pos + m.start()], m.group(0)); break
            pos += len(stmt) + 1
    return pre_hit, first_try is not None, any(re.search(r"\breturn\b", x) for x in catches)


def line_of(code, pos):
    return code.count("\n", 0, pos) + 1


# ----------------------------------------------------------------------------------------------- gotchas (G rules)
# 2026-11-25: recurring live failures that compiled and linted clean (76/76 historical prechecks PASS; each of these
# broke on the serial bridge in more than one project). Severity "error" makes the precheck FAIL; script-checker may
# not overrule it. The only escape is a waiver comment in the script itself, listed in the output:
#     // gotcha-ok: G<n> <reason>          (the reason is required; an empty one is ignored and warned)
# The rules are re-run on --reuse-precheck of bound/library prechecks, so an un-waived G error never reaches the bridge.
# G3 is a WARNING (2026-11-25 follow-up): it hit 18 final scripts that worked, so as an error it mostly added scripter
# round trips; script-checker reviews each hit (a lookup of a user-typed name with no fallback is major).
# Calibration 2026-11-25 over scripts/*.cs (425 files, 268 final = highest version per task): G1 10 files (3 final),
# G2 error 8 (5 final), G3 26 (18 final), G4 warn 29 (16 final); hits listed in the report of that change.
WAIVER_RE = re.compile(r"//[^\S\n]*gotcha-ok[^\S\n]*:[^\S\n]*(G\d+)(?:-[\w-]+)?[^\S\n]*(?:[:\-][^\S\n]*)?([^\n]*)", re.I)
GOTCHA_IDS = {"G1": "G1-tee-on-sloped", "G2": "G2-system-type-change", "G3": "G3-exact-name",
              "G4": "G4-obstacle-categories"}

# G1 (R20/R48/R80/R106/R107; CB6-R1-T20a v1, P71-SAN-T1 v1-v5, ATZ R13-T2): NewTeeFitting / NewTakeoffFitting refuse a
# sloped or vertical main ("angle too small or too large"). FAIL when the script tees and never measures a pipe's
# slope or direction. Accepted evidence of a check (anywhere in the script):
TEE_RE = re.compile(r"\bNew(Tee|Takeoff)Fitting\s*\(")
SLOPE_GUARD_RE = re.compile(
    r"RBS_PIPE_SLOPE|\bSlope\b|\b[Ii]s_?(Vertical|Horizontal|Sloped|Flat|Plumb|Level)\w*|"
    r"\b\w*(sloped|Sloped|vertical|Vertical|horizontal|Horizontal|plumb|Plumb)\w*\s*=[^=]|"
    r"Math\s*\.\s*Abs\s*\(\s*[\w.\[\]]+\s*\.\s*Z\s*\)|\.\s*Direction\s*\.\s*Z\b|"                  # direction component
    r"GetEndPoint\s*\(\s*[01]\s*\)\s*\.\s*Z\s*-\s*[\w.]*GetEndPoint\s*\(\s*[01]\s*\)\s*\.\s*Z|"      # endpoint dZ
    r"Math\s*\.\s*Abs\s*\(\s*(?P<ep>\w+)\s*\[\s*[01]\s*\]\s*\.\s*Z\s*-\s*(?P=ep)\s*\[\s*[01]\s*\]\s*\.\s*Z|"
    r"\b\w*(?:[Ss]lope|[Ff]all|[Gg]rade|[Pp]itch)\w*\s*=\s*[^;=]*\.\s*Z\s*-\s*[\w.\[\]]+\s*\.\s*Z")  # slope = (a.Z - b.Z)..

# G2 (R60/R73; L76-R8-T2f v5 retyped the building drain to VENT, CB6-R1-T9a v2-v4 Set reverted): SetSystemType or a Set
# of RBS_PIPING_SYSTEM_TYPE_PARAM retypes the whole connected network on commit, or reverts after Regenerate. Always
# a deliberate act: FAIL unless waived with how the network was isolated and the neighbours verified.
SYSTYPE_RE = re.compile(r"\.\s*SetSystemType\s*\(|RBS_PIPING_SYSTEM_TYPE_PARAM\s*\)\s*\??\s*\.\s*Set\s*\(")
SYSTYPE_VAR_RE = re.compile(r"\b(\w+)\s*=\s*[^;]*RBS_PIPING_SYSTEM_TYPE_PARAM\s*\)\s*;")
# ...and joining new pipe into an existing network merges systems on commit: WARN when nothing reads a system.
CONNECT_RE = re.compile(r"\.\s*ConnectTo\s*\(|\bNew(Tee|Takeoff|Elbow|Transition|Union|Cross)Fitting\s*\(")
SYSREAD_RE = re.compile(r"\bMEPSystem\b|RBS_PIPING_SYSTEM_TYPE_PARAM|RBS_SYSTEM_NAME_PARAM|RBS_SYSTEM_CLASSIFICATION_PARAM|"
                        r"\bPipingSystem(Type)?\b|\bSystemType\b|\bGetSystemType\b|SystemClassification")

# G3 (AB-R1-T12 FamilyName 'ARCH E1 30x42 ' had a trailing space; R30; CB6-R1-T6 'LENGTH'): a lookup that compares an
# element / type / family / view / sheet name to a literal with ==, one-argument Equals or 2-argument string.Equals
# (no Trim, no OrdinalIgnoreCase). Not flagged: != guards that report, Definition/Category/GetType names, literals
# that are concatenated ("<" + X + ">"), and the firm-standard revision tag names of R64 (stable in every model).
_NAME = r"(?<!Definition\.)(?<!Category\.)(?<!GetType\(\)\.)(?:Name|FamilyName|ViewName|SheetNumber)"
_OWNER = r"(?<![\w.])(?:\w+(?:\[[^\]\n]*\])?\s*\.\s*)+"
EXACT_NAME_RE = re.compile(
    r"(?<!GetType\(\)\.)" + _OWNER + _NAME + r"\b\s*==\s*(?P<q1>\")|"
    r"(?P<q2>\")\s*==\s*" + _OWNER + _NAME + r"\b(?!\s*[.(\[])|"
    + _OWNER + _NAME + r"\s*\.\s*Equals\s*\(\s*(?P<q3>\")[^\"\n]*\"\s*\)|"
    r"\b[Ss]tring\s*\.\s*Equals\s*\(\s*" + _OWNER + _NAME + r"\s*,\s*(?P<q4>\")[^\"\n]*\"\s*\)")
STANDARD_NAMES = {"Revision Tag", "MEP_Tag_Revision"}  # R64 / revit-conventions delta-tag standard


def _literal(code, q, closing=False):
    """Text of the string literal whose opening (or, closing=True, closing) quote is at offset q; None if dynamic."""
    if closing:
        j = q - 1
        while j > 0 and (code[j] != '"' or code[j - 1] == "\\"):
            j -= 1
        lo, hi = j + 1, q
        if re.search(r"\+\s*$", code[max(0, j - 8):j]):
            return None
    else:
        j = q + 1
        while j < len(code) and (code[j] != '"' or code[j - 1] == "\\"):
            j += 1
        lo, hi = q + 1, j
        if re.match(r"\s*\+", code[j + 1:j + 8]):
            return None
    return code[lo:hi]


# G4 (R67; CB6-R1-T4 v1-v3, CB6-R1-T16, WF-R1-T1, L76-R8-T1b): a clear-spot / obstacle search for a new or moved
# annotation whose obstacle set names no model element besides pipes (fixtures, equipment, accessories, generic
# models or FamilyInstance). WARN only: sheet-level placements (title block, viewports) legitimately skip model
# categories, and a view collector filtered in code (CB6-R1-T4) cannot be told apart statically.
SPOT_SEARCH_RE = re.compile(r"\b\w*(?:[Oo]bst|[Cc]lash|[Cc]ands?\b|[Cc]andidate|[Oo]verlap|[Oo]ccup|[Ff]ree_?[Ss]pot|"
                            r"[Cc]lear_?[Ss]pot)\w*")
PLACE_RE = re.compile(r"\b(TextNote|IndependentTag|RevisionCloud)\s*\.\s*Create\s*\(|TagHeadPosition\s*=[^=]|"
                      r"\.\s*(SetLeaderEnd|SetLeaderElbow)\s*\(")
MODEL_OBST_RE = re.compile(r"OST_(PlumbingFixtures|MechanicalEquipment|PlumbingEquipment|PipeAccessory|SpecialityEquipment|"
                           r"GenericModel|Sprinklers|ElectricalEquipment)\b|\btypeof\s*\(\s*FamilyInstance\s*\)")
LINK_OBST_RE = re.compile(r"RevitLinkInstance|GetLinkDocument|OST_RvtLinks|OST_Grids|typeof\s*\(\s*Grid\s*\)")


def waivers_in(code):
    """({G<n>: {rule, line, reason}}, [empty-reason waivers]) from `// gotcha-ok: G<n> <reason>` comments."""
    found, empty = {}, []
    for m in WAIVER_RE.finditer(code):
        gid, reason = m.group(1).upper(), m.group(2).strip()
        if reason:
            found.setdefault(gid, {"rule": GOTCHA_IDS.get(gid, gid), "line": line_of(code, m.start()), "reason": reason})
        else:
            empty.append({"rule": GOTCHA_IDS.get(gid, gid), "line": line_of(code, m.start())})
    return found, empty


def gotcha_hits(code, b=None, cleanup=False):
    """[(gid, 'error'|'warn', pos, message)] before waivers; b = blank(code)."""
    b = blank(code) if b is None else b
    hits = []
    tee = TEE_RE.search(b)
    if tee and not SLOPE_GUARD_RE.search(b):
        hits.append(("G1", "error", tee.start(), f"`{tee.group(0).strip()}` but the script never measures a pipe's "
                     "slope or direction: NewTee/NewTakeoffFitting off a sloped or vertical main is refused ('angle too "
                     "small or too large', R20/R48/R107). Test the main (Math.Abs(dir.Z), endpoint dZ, RBS_PIPE_SLOPE) and "
                     "branch by level stub / perpendicular tee / tee family + BreakCurve + ConnectTo "
                     "(library/revit/pipe-branch-to-main.cs); tees only between pipes this script draws: waive it"))
    st = SYSTYPE_RE.search(b)
    if st:
        hits.append(("G2", "error", st.start(), f"`{st.group(0).strip()}`: changing a pipe's system type retypes the "
                     "WHOLE connected network on commit (R60: building drain -> VENT, 0 FU) or reverts after Regenerate "
                     "(R73). Disconnect / DivideSystem first, retype only the isolated pipes (or the system element), "
                     "then verify the neighbouring pipes' system types; waive with how that is done"))
    else:
        for v in SYSTYPE_VAR_RE.finditer(b):
            rest = b[v.end():v.end() + 1500]
            again = re.search(r"\b" + re.escape(v.group(1)) + r"\s*=[^=]", rest)
            s = re.search(r"\b" + re.escape(v.group(1)) + r"\s*\??\s*\.\s*Set\s*\(", rest[:again.start()] if again else rest)
            if s:
                hits.append(("G2", "error", v.end() + s.start(), f"`{v.group(1)}.Set(` on RBS_PIPING_SYSTEM_TYPE_PARAM: a "
                             "per-pipe system type Set reverts after Regenerate or retypes the connected network (R60/"
                             "R73). Divide the system and retype the system element, verify neighbours"))
                break
    if not cleanup and not any(h[0] == "G2" for h in hits):
        cn = CONNECT_RE.search(b)
        if cn and not SYSREAD_RE.search(b):
            hits.append(("G2", "warn", cn.start(), f"`{cn.group(0).strip()}` joins pipe to a network but the script never "
                         "reads a system (MEPSystem / RBS_PIPING_SYSTEM_TYPE_PARAM): systems merge on commit; report the "
                         "joined network's system type before and after"))
    for m in EXACT_NAME_RE.finditer(b):
        q = next(m.start(g) for g in ("q1", "q2", "q3", "q4") if m.group(g))
        value = _literal(code, q, closing=bool(m.group("q2")))
        if value is None or value in STANDARD_NAMES:
            continue
        hits.append(("G3", "warn", m.start(), f"exact name lookup against \"{value[:60]}\": names in the model differ by "
                     "case and trailing spaces (AB-R1-T12 'ARCH E1 30x42 ', R30). Compare "
                     "string.Equals(x.Name.Trim(), \"...\", StringComparison.OrdinalIgnoreCase) (or a normalise helper) "
                     "and list the near-misses on no match; a name this script itself creates: waive it"))
        break  # one finding per script
    if SPOT_SEARCH_RE.search(b) and PLACE_RE.search(b) and not MODEL_OBST_RE.search(b):
        links = "" if LINK_OBST_RE.search(b) else ", grids incl. linked"
        hits.append(("G4", "warn", SPOT_SEARCH_RE.search(b).start(), "clear-spot / obstacle search for annotation with no "
                     f"model elements but pipes in the obstacle set: add fixtures, accessories, equipment{links} (or "
                     "collect everything visible in the view and exclude by category) (R67; CB6-R1-T4 v1-v3, WF-R1-T1)"))
    return hits


def gotchas(code, b=None, cleanup=False):
    """(errors, warnings, waivers) after applying `// gotcha-ok: G<n> <reason>` waivers."""
    found, empty = waivers_in(code)
    errs, warns, used = [], [], []
    for gid, sev, pos, msg in gotcha_hits(code, b, cleanup):
        row = {"rule": GOTCHA_IDS[gid], "line": line_of(code, pos), "message": msg}
        if gid in found:
            used.append(dict(found[gid], line_hit=row["line"], severity=sev))
            continue
        (errs if sev == "error" else warns).append(row)
    for w in empty:
        warns.append({"rule": "G0-waiver", "line": w["line"], "message": f"`gotcha-ok: {w['rule'].split('-')[0]}` "
                      "without a reason is ignored: write // gotcha-ok: G<n> <why this script is safe>"})
    return errs, warns, used


def gotcha_errors(code, path=""):
    """Un-waived G errors for a script (used on precheck reuse)."""
    return gotchas(code, cleanup=".cleanup." in Path(path).name.lower())[0]


# L1 / L5 helpers (2026-11-25 false-positive review of 76 prechecks: L5-dryrun-order 12x, L1-mode 6x overruled)
MODE_PHRASE_RE = re.compile(r"\b(auto|none)\s+(?:transaction(?:\s*mode)?|mode)\b", re.I)  # "Auto transaction."
NONE_ONLY_RE = re.compile(r"\.\s*(Export|EditFamily|LoadFamily)\s*\(")
DRY_IF_RE = re.compile(r"\bif\s*\(\s*!?\s*(dry\w*|isDry\w*|probe\w*)\b", re.I)
DRY_ANY_RE = re.compile(r"\bif\s*\(\s*!?\s*(dry\w*|isDry\w*|probe\w*)\b|\b(dry\w*|isDry\w*)\s*(\?|\|\||&&)", re.I)
TX_VAR_RE = re.compile(r"\b(\w+)\s*=\s*new\s+(SubTransaction|Transaction|TransactionGroup)\s*\(")


def rolled_back_on_dry(b, before):
    """'transaction' if a Transaction / SubTransaction constructed before offset `before` is RollBack()-ed within
    700 chars of a dry-run test (R99 own-Transaction pattern, R124 SubTransaction probe); 'group' if only a
    TransactionGroup is; else None. b = blanked code."""
    found = None
    dry_tests = [d.start() for d in DRY_ANY_RE.finditer(b)]
    for v in TX_VAR_RE.finditer(b):
        if v.start() > before:
            continue
        rb = re.compile(r"\b" + re.escape(v.group(1)) + r"\s*\.\s*RollBack\s*\(")
        if any(rb.search(b, d, d + 700) for d in dry_tests):
            if v.group(2) != "TransactionGroup":
                return "transaction"
            found = "group"
    return found


def lint(path: str, code: str, waivers_out=None, info=None):
    """(mode, errors, warnings). G-rule waivers that suppressed a hit are appended to waivers_out; info (a dict) gets
    transaction_mode_inferred when the header states no mode."""
    errs, warns = [], []
    b = blank(code)

    def add(lst, rule, pos, msg):
        lst.append({"rule": rule, "line": line_of(code, pos) if pos is not None else 0, "message": msg})

    header = "\n".join(code.splitlines()[:25])
    m = MODE_RE.search(header) or MODE_PHRASE_RE.search(header)
    mode = m.group(1).lower() if m else None
    # A Transaction opened only when the document is not already modifiable (dry-run simulation) is fine in auto mode.
    starts = [x for x in re.finditer(r"\bnew\s+(Transaction|TransactionGroup)\s*\(", b)
              if "IsModifiable" not in b[max(0, b.rfind("\n", 0, x.start()) - 200):x.start()]]
    cleanup = ".cleanup." in Path(path).name.lower()
    writes = list(WRITE_RE.finditer(b))
    if mode is None:
        # 2026-11-25: all 6 historical L1 findings were overruled because the code fixes the mode anyway: an own
        # Transaction / Export / EditFamily / LoadFamily only works in "none", any other change (SubTransaction,
        # Set, Create) only in "auto", and a read-only script runs in either (no warning). The inferred mode is
        # reported as transaction_mode; L2/L3 still apply only to a declared mode.
        inferred = "none" if starts or NONE_ONLY_RE.search(b) else "auto" if writes else None
        if info is not None:
            info["transaction_mode_inferred"] = inferred
        if inferred:
            add(warns, "L1-mode", None, f'header does not state transactionMode; the code only works with "{inferred}" '
                f'(reported as the inferred transaction_mode): add // transactionMode "{inferred}" in lines 1-25')
    if mode == "auto" and starts:
        add(errs, "L2-nested-transaction", starts[0].start(),
            f"transactionMode auto already runs the code inside a Transaction: new {starts[0].group(1)} here throws "
            "at runtime. Use SubTransaction, or declare transactionMode \"none\" and manage your own transactions")
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
        dpos = DRY_IF_RE.search(b)
        # Constructing a Transaction / SubTransaction is not a change (most overruled L5 findings fired on it).
        changes = [x for x in writes if not x.group(0).startswith("new")]
        if dpos is None:
            add(warns, "L5-dryrun-branch", None, "dryrun is read but no `if (dry...)` branch was found: check the "
                "dry run returns before any change")
        elif changes and changes[0].start() < dpos.start():
            first_write = changes[0].start()
            rolled = rolled_back_on_dry(b, first_write)
            if rolled != "transaction":  # R99 own Transaction / R124 SubTransaction rolled back in the dry branch: ok
                add(warns, "L5-dryrun-order", first_write, "a model change appears before the first dry-run branch: "
                    "in a dry run nothing may change (auto mode commits it)" + (
                        "; the dry run rolls back a TransactionGroup: allowed only for geometry/pipe/delete scripts "
                        "with checked statuses and a post-rollback re-read (R47/R77), a blocker for view/sheet "
                        "scripts (R86)" if rolled == "group" else ""))
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
    pre_hit, has_try, catch_ret = outer_catch(b)
    l16 = errs if writes else warns
    if not has_try or not catch_ret:
        add(l16, "L16-outer-catch", None, "no top-level try { ... } catch (Exception ex) { return \"FAILED @<stage>: \" + "
            "ex.GetType().Name + \": \" + ex.Message; }: a throw returns a bare 'target of an invocation' with no stage "
            "(R116, ATZ-R2-T1/R13-T1)")
    elif pre_hit:
        add(l16, "L16-outer-catch", pre_hit[0], f"`{pre_hit[1].strip()}` runs before the first top-level try: move every "
            "Revit call (pre-checks included) inside the try so a throw reports its stage (R116)")
    g_err, g_warn, g_waived = gotchas(code, b, cleanup)
    errs += g_err
    warns += g_warn
    if waivers_out is not None:
        waivers_out.extend(g_waived)
    return mode, errs, warns


# ----------------------------------------------------------------------------------------------- main
def read_source(path):
    data = Path(path).read_bytes()
    code = data.decode("utf-8-sig").replace("\r\n", "\n").replace("\r", "\n")
    return hashlib.sha256(data).hexdigest(), code


def toolchain_key():
    exe, core, desk = env()
    files = [exe, core, REVIT_DIR / "RevitAPI.dll", REVIT_DIR / "RevitAPIUI.dll",
             ROSLYN_DIR / "Microsoft.CodeAnalysis.dll", ROSLYN_DIR / "Microsoft.CodeAnalysis.CSharp.dll",
             ROSLYN_DIR / "Newtonsoft.Json.dll"]
    if desk:
        files.append(desk)
    files.extend((HOST_SRC, Path(__file__), SWARM / "claude" / "hooks" / "writer_guard.py"))
    identities = []
    for file in files:
        if file.exists():
            st = file.stat()
            identities.append((str(file.resolve()), st.st_size, st.st_mtime_ns))
    return hashlib.sha256(json.dumps(identities).encode("utf-8")).hexdigest()


# ----------------------------------------------------------------------------------------------- precheck reuse
# 2026-11-25: a PASS is bound to CONTENT, not to a session. Three kinds:
#   bound   - "bound": {task_path, task_sha256, expected_path, expected_sha256} (from --evidence --task-json/
#             --expected-json). Matches: PASS + toolchain + every script/cleanup sha + task.json and expected.json
#             sha now. run_id / task_id are recorded for audit only, so a byte-identical task needs no new checker.
#   library - "library": true, every script under swarm/library/ (from --evidence --library). Matches on script sha +
#             toolchain only: the compile + lint scope, approved once per toolchain for any task that runs it.
#   legacy  - neither field (prechecks written before 2026-11-25): the old rule, run_id + task_id + sha + toolchain.
LIBRARY = SWARM / "library"


def file_sha256(path):
    """sha256 of the file's bytes, or None when it does not exist (an expected.json may be absent: no_expected)."""
    p = Path(path)
    return hashlib.sha256(p.read_bytes()).hexdigest() if p.is_file() else None


def bound_for(task_json, expected_json):
    return {"task_path": Path(task_json).resolve().as_posix(), "task_sha256": file_sha256(task_json),
            "expected_path": Path(expected_json).resolve().as_posix() if expected_json else None,
            "expected_sha256": file_sha256(expected_json) if expected_json else None}


def in_library(path):
    try:
        Path(path).resolve().relative_to(LIBRARY.resolve())
        return True
    except ValueError:
        return False


def precheck_kind(precheck):
    if precheck.get("library") is True:
        return "library"
    return "bound" if isinstance(precheck.get("bound"), dict) else "legacy"


def current_bound(precheck, precheck_path=None, task_id=None, task_json=None, expected_json=None):
    """(task_sha256, expected_sha256) of the task files this reuse is for: explicit --task-json/--expected-json, else
    the recorded bound paths, else (or when --task-id names another task) <precheck dir>/<task-id>.task.json."""
    rec = precheck.get("bound") or {}
    tid = task_id or precheck.get("task_id")
    here = Path(precheck_path).resolve().parent if precheck_path else Path.cwd()
    other_task = task_id is not None and task_id != precheck.get("task_id")
    t = task_json or (None if other_task else rec.get("task_path")) or here / f"{tid}.task.json"
    e = expected_json or (None if other_task else rec.get("expected_path")) or here / f"{tid}.expected.json"
    return file_sha256(t), file_sha256(e)


def precheck_matches(precheck, run_id, task_id, path, digest, toolchain, bound_now=None):
    """True if this PASS precheck covers `path` with exactly `digest` under `toolchain` (see the kinds above).
    bound_now = (task_sha256, expected_sha256) for a bound precheck (current_bound)."""
    if precheck.get("status") != "PASS" or precheck.get("toolchain") != toolchain:
        return False
    kind = precheck_kind(precheck)
    if kind == "legacy":
        if (precheck.get("run_id"), precheck.get("task_id")) != (run_id, task_id) or run_id is None:
            return False
    elif kind == "library":
        if not all(in_library(row.get("path", "")) for row in precheck.get("scripts", [])) or not in_library(path):
            return False
    else:
        rec = precheck["bound"]
        if not rec.get("task_sha256") or bound_now is None or tuple(bound_now) != (
                rec.get("task_sha256"), rec.get("expected_sha256")):
            return False
    absolute = Path(path).resolve()
    return any(Path(row["path"]).resolve() == absolute and row.get("sha256") == digest
               and row.get("compile") == "PASS" for row in precheck.get("scripts", []) if row.get("path"))


def check(paths, sources=None, bound=None, library=False):
    if sources is None:
        sources = {p: read_source(p) for p in paths}
    toolchain = toolchain_key()
    comp = compile_all(paths, sources)
    results = []
    for p in paths:
        digest, code = sources[p]
        waived, info = [], {}
        mode, lerr, lwarn = lint(p, code, waived, info)
        c = comp[p]
        status = "FAIL" if c["errors"] or lerr else "PASS"
        row = {"file": Path(p).resolve().as_posix(), "sha256": digest, "toolchain": toolchain,
               "status": status, "transaction_mode": mode or info.get("transaction_mode_inferred"),
               "transaction_mode_declared": mode is not None,
               "compile": c, "lint": {"errors": lerr, "warnings": lwarn, "waivers": waived}}
        if bound is not None:
            row["bound"] = bound
        if library:
            row["library"] = True
        results.append(row)
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
        for w in lt.get("waivers", []):
            print(f"  waived {w['rule']} (hit line {w['line_hit']}, waiver line {w['line']}): {w['reason']}")


SELFTEST_OK = '''// selftest: transactionMode "auto". parameters: ["dryrun"]
string step = "start";
try
{
    bool dry = parameters != null && parameters.Length > 0 && Convert.ToString(parameters[0]) == "dryrun";
    if (document.Title == null) return "NO_GO";
    step = "count";
    var walls = new FilteredElementCollector(document).OfClass(typeof(Wall)).GetElementCount();
    if (dry) return "DRYRUN walls=" + walls + "\\ndone=0 skipped=0 failed=0";
    step = "set";
    var p = document.ProjectInformation.LookupParameter("Project Name");
    if (p != null) p.Set("x");
    return "walls=" + walls + "\\ndone=1 skipped=0 failed=0";
}
catch (Exception ex) { return "FAILED @" + step + ": " + ex.GetType().Name + ": " + ex.Message + "\\ndone=0 skipped=0 failed=1"; }
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
    want = {"CS1061", "L2-nested-transaction", "L4-dryrun", "L6-summary", "L9-ui", "L16-outer-catch"}
    passed = r_ok["status"] == "PASS" and want <= got and any(e["line"] == 4 for e in r_bad["compile"]["errors"])
    print("SELFTEST", "PASS" if passed else f"FAIL (bad got {sorted(got)})")
    return 0 if passed else 1


def main(argv):
    if "--selftest" in argv:
        return selftest()
    options = {}
    for flag in ("--json", "--reuse-precheck", "--run-id", "--task-id", "--task-json", "--expected-json"):
        if flag in argv:
            i = argv.index(flag)
            if i + 1 >= len(argv) or argv[i + 1].startswith("--"):
                sys.exit(f"revit_cs_check: {flag} requires a value")
            options[flag] = argv[i + 1]
            del argv[i:i + 2]
    out = options.get("--json")
    quiet = "--quiet" in argv
    evidence = "--evidence" in argv
    library = "--library" in argv
    if "--reuse-precheck" in options:
        if not ("--emit-code" in argv or "--emit-file" in argv):
            sys.exit("revit_cs_check: reuse requires --emit-code or --emit-file")
    elif "--emit-code" in argv or "--emit-file" in argv:
        sys.exit("revit_cs_check: --emit-code / --emit-file require a verified precheck")
    if "--expected-json" in options and "--task-json" not in options:
        sys.exit("revit_cs_check: --expected-json needs --task-json")
    paths = [a for a in argv if not a.startswith("--")]
    if not paths:
        print(__doc__)
        return 2
    for p in paths:
        if not Path(p).is_file():
            print(f"revit_cs_check: no such file {p}")
            return 2
    if library and not all(in_library(p) for p in paths):
        print(f"revit_cs_check: --library: every script must be under {LIBRARY.as_posix()}")
        return 2
    if "--task-json" in options and not Path(options["--task-json"]).is_file():
        print(f"revit_cs_check: no such task.json {options['--task-json']}")
        return 2
    sources = {p: read_source(p) for p in paths}
    precheck_path = options.get("--reuse-precheck")
    if precheck_path:
        why = "precheck missing, not PASS, or for other script/cleanup bytes, task.json/expected.json or toolchain"
        try:
            precheck = json.loads(Path(precheck_path).read_text(encoding="utf-8-sig"))
            toolchain = toolchain_key()
            kind = precheck_kind(precheck)
            bound_now = current_bound(precheck, precheck_path, options.get("--task-id"), options.get("--task-json"),
                                      options.get("--expected-json")) if kind == "bound" else None
            matches = all(precheck_matches(precheck, options.get("--run-id"), options.get("--task-id"),
                                           p, sources[p][0], toolchain, bound_now) for p in paths)
            # Check every reviewed file, including a cleanup not needed for this specific call.
            matches = matches and all(read_source(row["path"])[0] == row["sha256"]
                                      and row["compile"] == "PASS" for row in precheck["scripts"])
            # G errors are not overrulable: re-run them on every reviewed file (cheap, no compile). Legacy prechecks
            # predate the rules, so a script written ahead under them is not refused mid-job.
            if matches and kind != "legacy":
                g = [(row["path"], e) for row in precheck["scripts"]
                     for e in gotcha_errors(read_source(row["path"])[1], row["path"])]
                if g:
                    matches = False
                    why = "un-waived gotcha errors: " + "; ".join(f"{Path(p).name} line {e['line']} {e['rule']}"
                                                                    for p, e in g[:5])
        except (OSError, ValueError, TypeError, AttributeError, KeyError):
            matches = False
        if matches:
            digest, code = sources[paths[0]]
            if "--emit-file" in argv:
                first = code.split("\n", 1)[0].strip()
                print(json.dumps({"status": "PASS", "precheck_reused": True, "precheck_kind": kind, "sha256": digest,
                                  "codeFile": os.path.abspath(paths[0]),
                                  "expectMarker": first if first.startswith("//") else None,
                                  "lines": code.count("\n") + 1}))
            else:
                print(json.dumps({"status": "PASS", "precheck_reused": True, "precheck_kind": kind, "sha256": digest,
                                  "code": code}))
            return 0
        print(f"Precheck not reusable ({why}). Recompiling for diagnostics. A fresh script-checker PASS is required.")
    bound = bound_for(options["--task-json"], options.get("--expected-json")) if "--task-json" in options else None
    res = check(paths, sources, bound=bound, library=library)
    if evidence:
        print(json.dumps(res))
    else:
        report(res, quiet)
    if out:
        Path(out).write_text(json.dumps(res[0] if len(res) == 1 else res, indent=2), encoding="utf-8")
    if any(r["status"] == "FAIL" for r in res):
        return 1
    return 3 if precheck_path else 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
