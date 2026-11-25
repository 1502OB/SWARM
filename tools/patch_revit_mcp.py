"""Patch the revit-mcp Node server so several Revit instances can be driven at once.

Stock behaviour: every call reads %APPDATA%/Autodesk/Revit/Addins/<yr>/revit_mcp_plugin/mcp-port.txt,
which all Revit 2025 windows share, so starting a second instance's server can silently redirect every
Claude session to it.

After the patch, port resolution per call is:
  1. port pinned in THIS server process with the new `revit_instance` tool (one process per Claude session)
  2. env REVIT_MCP_PORT (8080-8089)
  3. mcp-port.txt (stock fallback), else 8080

`revit_instance` actions: list (probe 8080-8089, report each instance's open document), select (verify
and pin), status, clear. Error messages name the port they tried.

Idempotent; keeps index.js.orig next to the bundle the first time. Re-run after every plugin update.
Usage: python tools/patch_revit_mcp.py [--check] [--restore]
       python tools/patch_revit_mcp.py --health-only [--check|--restore]
The health patch adds get_project_info(health=true) with a five-second budget and a request marker.
       python tools/patch_revit_mcp.py --codefile-only [--check|--restore]
The codefile patch (2026-11-25, LESSONS R70/R72/O37):
  - send_code_to_revit takes codeFile (absolute path to a .cs under the swarm folder) instead of code; the server
    reads the file, so writers never retype a long script. Optional expectMarker (text the file must contain) and
    expectSha256 (the gate's sha256 of the file bytes); CRLF is normalised to LF as the gate does.
    The swarm guards read the same file (guardlib.revit_code) and judge it exactly like inline code.
  - With no pin and no REVIT_MCP_PORT, the port comes from the first mcp-port.txt (2027 -> 2023) whose port
    actually answers, so a stale file left by a closed Revit no longer wins over the running one.
Install it after the base patch (it needs resolveRevitPortInfo / tcpOpen from the base patch).
Running sessions keep the old code until their revit-mcp server restarts (/mcp reconnect or new session).
"""
import os
import shutil
import sys
from pathlib import Path
from tempfile import NamedTemporaryFile

MARK = "/* swarm-multi-instance-patch v2 */"
HEALTH_MARK = "/* swarm-quick-health v1 */"
BUNDLE = Path(os.environ["APPDATA"]) / "Autodesk/Revit/Addins/2025/revit_mcp_plugin/Commands/RevitMCPCommandSet/server/build/index.js"

RESOLVER = MARK + r'''
var pinnedRevitPort = null;
var pinnedRevitTitle = null;
function envRevitPort() {
  const p = parseInt(process.env.REVIT_MCP_PORT || "", 10);
  return p >= 8080 && p <= 8089 ? p : null;
}
function resolveRevitPortInfo() {
  if (pinnedRevitPort !== null) return { port: pinnedRevitPort, source: "pinned (revit_instance select)" };
  const e = envRevitPort();
  if (e !== null) return { port: e, source: "env REVIT_MCP_PORT" };
  return { port: readPortFromFile(), source: "mcp-port.txt (shared by every Revit window of that year)" };
}
function resolveRevitPort() {
  return resolveRevitPortInfo().port;
}
'''

TOOL = r'''
// swarm patch: src/tools/revit_instance
var REVIT_IDENTIFY_CODE = 'var d = document; var p = d.PathName; string c = ""; try { if (d.IsWorkshared) { var mp = d.GetWorksharingCentralModelPath(); c = mp == null ? "" : ModelPathUtils.ConvertModelPathToUserVisiblePath(mp); } } catch {} return "title=" + d.Title + "|path=" + p + "|central=" + c;';
function parseIdentify(r) {
  const s = typeof r === "string" ? r : JSON.stringify(r);
  const m = /title=([^|"]*)\|path=([^|"]*)\|central=([^"]*?)(?:"|$)/.exec(s);
  return m ? { title: m[1], path: m[2].replace(/\\\\/g, "\\"), central: m[3].replace(/\\\\/g, "\\").replace(/\\+$/, "") } : { raw: s.slice(0, 300) };
}
async function identifyRevitPort(port, timeoutMs) {
  return await attemptConnection(port, async (c) => {
    const r = await c.sendCommand("send_code_to_revit", { code: REVIT_IDENTIFY_CODE, parameters: [], transactionMode: "none" }, timeoutMs);
    return parseIdentify(r);
  }, timeoutMs);
}
function tcpOpen(port) {
  return new Promise((resolve) => {
    const s = new net.Socket();
    const done = (ok) => { s.destroy(); resolve(ok); };
    s.setTimeout(400, () => done(false));
    s.once("connect", () => done(true));
    s.once("error", () => done(false));
    s.connect(port, "127.0.0.1");
  });
}
function registerRevitInstanceTool(server2) {
  server2.tool(
    "revit_instance",
    "Choose which running Revit instance THIS session talks to, so several Revit windows can be driven at once without the shared mcp-port.txt redirecting sessions. action=list probes ports 8080-8089 and reports the open document of each live instance; action=select verifies a port answers and pins this session to it (pass expectTitle to refuse a wrong model); action=status reports the port in use and where it comes from; action=clear unpins (back to REVIT_MCP_PORT / mcp-port.txt). When more than one Revit is open, select before any other Revit call. Subagents share the pin of the session that spawned them.",
    {
      action: external_exports.enum(["list", "select", "status", "clear"]).describe("list | select | status | clear"),
      port: external_exports.number().int().min(8080).max(8089).optional().describe("select: the port to pin"),
      expectTitle: external_exports.string().optional().describe("select: refuse unless the document title or path contains this text (case-insensitive)")
    },
    async (args) => {
      try {
        if (args.action === "status") {
          const info = resolveRevitPortInfo();
          return rawToolResponse("revit_instance", { ...info, pinnedTitle: pinnedRevitTitle });
        }
        if (args.action === "clear") {
          pinnedRevitPort = null;
          pinnedRevitTitle = null;
          return rawToolResponse("revit_instance", { cleared: true, ...resolveRevitPortInfo() });
        }
        if (args.action === "list") {
          const out = [];
          for (let p = 8080; p <= 8089; p++) {
            if (!(await tcpOpen(p))) continue;
            try {
              out.push({ port: p, ...(await identifyRevitPort(p, 15e3)) });
            } catch (e) {
              out.push({ port: p, error: "listening but not answering as revit-mcp: " + errorMessage(e) });
            }
          }
          return rawToolResponse("revit_instance", { current: resolveRevitPortInfo(), instances: out });
        }
        if (args.action === "select") {
          if (args.port === void 0) return toolError("revit_instance", "select needs port (8080-8089); run action=list first");
          let id;
          try {
            id = await identifyRevitPort(args.port, 3e4);
          } catch (e) {
            return toolError("revit_instance", `Port ${args.port} did not answer: ${errorMessage(e)}. Pin unchanged (${resolveRevitPortInfo().port}).`);
          }
          if (args.expectTitle) {
            const hay = ((id.title || "") + " " + (id.path || "") + " " + (id.central || "") + " " + (id.raw || "")).toLowerCase();
            if (!hay.includes(args.expectTitle.toLowerCase())) {
              return toolError("revit_instance", `Port ${args.port} has "${id.title || id.raw}" open, not "${args.expectTitle}". Pin unchanged (${resolveRevitPortInfo().port}).`);
            }
          }
          pinnedRevitPort = args.port;
          pinnedRevitTitle = id.title || null;
          return rawToolResponse("revit_instance", { pinned: args.port, ...id });
        }
        return toolError("revit_instance", "unknown action");
      } catch (error2) {
        return toolError("revit_instance", `revit_instance failed: ${errorMessage(error2)}`);
      }
    }
  );
}
'''

EDITS = [
    # (old, new, count expected)
    ("function readPortFromFile() {\n", RESOLVER + "function readPortFromFile() {\n", 1),
    ("  await previousMutex;\n  const port = readPortFromFile();\n",
     "  await previousMutex;\n  const port = resolveRevitPort();\n", 1),
    ('      "Cannot connect to Revit after 3 attempts. Make sure Revit is open and MCP Switch is active (green indicator)."',
     '      `Cannot connect to Revit on port ${port} after 3 attempts (${resolveRevitPortInfo().source}). Make sure Revit is open and MCP Switch is active (green indicator); with several Revit windows use revit_instance list/select.`', 1),
    ("// src/index.ts\n", TOOL + "\n// src/index.ts\n", 1),
    ("  await registerTools(server);\n", "  await registerTools(server);\n  registerRevitInstanceTool(server);\n", 1),
]

HEALTH = HEALTH_MARK + r'''
var activeRevitRequests = 0;
function swarmHealthReply(reply, marker) {
  if (typeof reply === "string") return reply.startsWith(marker + "\n") ? reply : undefined;
  if (reply && typeof reply === "object") {
    for (const child of Object.values(reply)) {
      const found = swarmHealthReply(child, marker);
      if (found !== undefined) return found;
    }
  }
}
async function swarmRevitHealth() {
  if (activeRevitRequests) return { status: "busy" };
  const budgetMs = 5000;
  const deadline = performance.now() + budgetMs;
  const marker = randomUUID();
  const code = 'var d = document; string id = ""; try { if (d.IsModelInCloud) { var p = d.GetCloudModelPath(); id = "cloud:" + p.GetProjectGUID() + "/" + p.GetModelGUID(); } else if (d.IsWorkshared) { var p = d.GetWorksharingCentralModelPath(); if (p != null) id = ModelPathUtils.ConvertModelPathToUserVisiblePath(p); } } catch {} if (string.IsNullOrEmpty(id)) id = d.PathName; return "' + marker + '\\n" + d.Title + "\\n" + id;';
  const reply = await withRevitConnection(async (client) => {
    const remaining = Math.floor(deadline - performance.now());
    if (remaining < 1) throw new Error("health check connection exceeded five seconds");
    return await client.sendCommand("send_code_to_revit", { code, parameters: [], transactionMode: "none" }, remaining);
  }, budgetMs, 1);
  const body = swarmHealthReply(reply, marker);
  if (body === undefined) {
    throw new Error("health reply had no matching request marker");
  }
  const [, document, document_id] = body.split("\n");
  return { status: "alive", request_id: marker, document, document_id, elapsed_ms: Math.round(budgetMs + performance.now() - deadline) };
}
'''

HEALTH_EDITS = [
    ("async function withRevitConnection(operation, timeoutMs = 12e4) {\n",
     HEALTH + "async function withRevitConnection(operation, timeoutMs = 12e4, maxRetries = MAX_RETRIES) {\n"
     "  activeRevitRequests++;\n"),
    ("for (let attempt = 0; attempt < MAX_RETRIES; attempt++)",
     "for (let attempt = 0; attempt < maxRetries; attempt++)"),
    ("attempt >= MAX_RETRIES - 1", "attempt >= maxRetries - 1"),
    ("  } finally {\n    releaseMutex();\n  }\n}", "  } finally {\n    activeRevitRequests--;\n    releaseMutex();\n  }\n}"),
    ('      includePhases: external_exports.boolean().optional().describe("Include project phases (default: true)"),',
     '      health: external_exports.boolean().optional().describe("Short read-only bridge health and document identity check"),\n'
     '      includePhases: external_exports.boolean().optional().describe("Include project phases (default: true)"),'),
    ("      const params = {\n        includePhases: args.includePhases ?? true,",
     '''      if (args.health) {
        try {
          return rawToolResponse("get_project_info", await swarmRevitHealth());
        } catch (error2) {
          return toolError("get_project_info", errorMessage(error2));
        }
      }
      const params = {
        includePhases: args.includePhases ?? true,'''),
]


def health_text(text):
    if HEALTH_MARK in text:
        return text
    for old, new in HEALTH_EDITS:
        if text.count(old) != 1:
            raise ValueError(f"health patch anchor mismatch: {old[:80]!r}")
        text = text.replace(old, new)
    return text


def install_health(bundle, check=False, restore=False):
    backup = bundle.with_suffix(".js.pre-swarm-health")
    current = bundle.read_text(encoding="utf-8")
    patched = health_text(current)
    if restore:
        original = backup.read_text(encoding="utf-8")
        if health_text(original) != current:
            raise ValueError("installed bundle changed since the health patch; refusing to overwrite it")
        patched = original
    if current == patched:
        print("health patch already installed:", bundle)
        return 0
    if check:
        print("health patch anchors OK:", bundle)
        return 0
    if not restore:
        if backup.exists() and backup.read_text(encoding="utf-8") != current:
            raise ValueError(f"health backup already belongs to a different bundle: {backup}")
        if not backup.exists():
            shutil.copy2(bundle, backup)
    with NamedTemporaryFile("w", encoding="utf-8", newline="\n", dir=bundle.parent, delete=False) as stream:
        temporary = Path(stream.name)
        try:
            stream.write(patched)
            stream.close()
            os.replace(temporary, bundle)
        finally:
            temporary.unlink(missing_ok=True)
    print("restored health backup:" if restore else "installed health patch:", bundle)
    return 0


CODEFILE_MARK = "/* swarm-codefile-portprobe v1 */"
CODEFILE_HELPERS = CODEFILE_MARK + r"""
import { createHash as swarmCreateHash } from "crypto";
function swarmPortFileCandidates() {
  const appData = process.env.APPDATA || "";
  const out = [];
  for (const year of ["2027", "2026", "2025", "2024", "2023"]) {
    const portFile = join(appData, "Autodesk", "Revit", "Addins", year, "revit_mcp_plugin", "mcp-port.txt");
    if (!existsSync(portFile)) continue;
    try {
      const port = parseInt(readFileSync(portFile, "utf-8").trim(), 10);
      if (port >= 8080 && port <= 8089 && !out.some((c) => c.port === port)) out.push({ year, port });
    } catch {
    }
  }
  return out;
}
async function resolveRevitPortLive() {
  const info = resolveRevitPortInfo();
  if (!info.source.startsWith("mcp-port.txt")) return info.port;
  for (const c of swarmPortFileCandidates()) {
    if (await tcpOpen(c.port)) return c.port;
  }
  return info.port;
}
function swarmReadCodeFile(args) {
  const hasCode = typeof args.code === "string" && args.code.trim() !== "";
  if (!args.codeFile) {
    if (!hasCode) throw new Error("pass code or codeFile");
    return args.code;
  }
  if (hasCode) throw new Error("pass either code or codeFile, not both");
  const f = String(args.codeFile);
  if (!/^[A-Za-z]:[\\/]/.test(f) || !/\.cs$/i.test(f)) throw new Error(`codeFile must be an absolute path to a .cs file (${f})`);
  if (!existsSync(f)) throw new Error(`codeFile not found: ${f}`);
  const raw = readFileSync(f);
  if (args.expectSha256) {
    const got = swarmCreateHash("sha256").update(raw).digest("hex");
    if (got !== String(args.expectSha256).toLowerCase()) throw new Error(`codeFile ${f} sha256 ${got} is not the reviewed ${args.expectSha256} (the file changed after the gate)`);
  }
  let text = raw.toString("utf-8");
  if (text.charCodeAt(0) === 0xfeff) text = text.slice(1);
  text = text.replace(/\r\n?/g, "\n");
  if (args.expectMarker && !text.includes(args.expectMarker)) throw new Error(`codeFile ${f} does not contain the marker "${args.expectMarker}" (stale or wrong version)`);
  return text;
}
"""

CODEFILE_EDITS = [
    ("function readPortFromFile() {\n", CODEFILE_HELPERS + "function readPortFromFile() {\n"),
    ("  await previousMutex;\n  const port = resolveRevitPort();\n",
     "  await previousMutex;\n  const port = await resolveRevitPortLive();\n"),
    ("      code: external_exports.string().describe('C# code to execute.",
     "      codeFile: external_exports.string().optional().describe(\"Absolute path to a .cs file under the swarm folder. The server reads the file and runs its text as code, so a script on disk is never retyped. Use instead of code.\"),\n"
     "      expectMarker: external_exports.string().optional().describe(\"With codeFile: text the file must contain (e.g. the script's version line); the call fails if it is missing.\"),\n"
     "      expectSha256: external_exports.string().optional().describe(\"With codeFile: sha256 of the file bytes, as printed by the revit_cs_check gate; the call fails if the file changed.\"),\n"
     "      code: external_exports.string().optional().describe('C# code to execute (or pass codeFile instead)."),
    ("      const params = {\n        code: args.code,\n        parameters: args.parameters || [],",
     "      let codeText;\n      try {\n        codeText = swarmReadCodeFile(args);\n      } catch (error2) {\n        return rawToolError(\"send_code_to_revit\", `Code execution failed: ${errorMessage(error2)}`);\n      }\n"
     "      const params = {\n        code: codeText,\n        parameters: args.parameters || [],"),
    ('          return rawToolResponse("revit_instance", { ...info, pinnedTitle: pinnedRevitTitle });',
     '          return rawToolResponse("revit_instance", { ...info, pinnedTitle: pinnedRevitTitle, liveFallbackPort: await resolveRevitPortLive(), portFiles: swarmPortFileCandidates() });'),
]


def codefile_text(text):
    if CODEFILE_MARK in text:
        return text
    if MARK not in text:
        raise ValueError("install the base multi-instance patch first (python tools/patch_revit_mcp.py)")
    for old, new in CODEFILE_EDITS:
        if text.count(old) != 1:
            raise ValueError(f"codefile patch anchor mismatch ({text.count(old)} found): {old[:80]!r}")
        text = text.replace(old, new)
    return text


def install_codefile(bundle, check=False, restore=False):
    backup = bundle.with_suffix(".js.pre-swarm-codefile")
    current = bundle.read_text(encoding="utf-8")
    if restore:
        original = backup.read_text(encoding="utf-8")
        if codefile_text(original) != current:
            raise ValueError("installed bundle changed since the codefile patch; refusing to overwrite it")
        patched = original
    else:
        patched = codefile_text(current)
    if current == patched:
        print("codefile patch already installed:", bundle)
        return 0
    if check:
        print("codefile patch anchors OK:", bundle)
        return 0
    if not restore:
        if backup.exists() and backup.read_text(encoding="utf-8") != current:
            raise ValueError(f"codefile backup already belongs to a different bundle: {backup}")
        if not backup.exists():
            shutil.copy2(bundle, backup)
    with NamedTemporaryFile("w", encoding="utf-8", newline="\n", dir=bundle.parent, delete=False) as stream:
        temporary = Path(stream.name)
        try:
            stream.write(patched)
            stream.close()
            os.replace(temporary, bundle)
        finally:
            temporary.unlink(missing_ok=True)
    print("restored codefile backup:" if restore else "installed codefile patch:", bundle)
    return 0


def main():
    if "--codefile-only" in sys.argv:
        try:
            return install_codefile(BUNDLE, check="--check" in sys.argv, restore="--restore" in sys.argv)
        except (OSError, ValueError) as error:
            print(error, file=sys.stderr)
            return 1
    if "--health-only" in sys.argv:
        try:
            return install_health(BUNDLE, check="--check" in sys.argv, restore="--restore" in sys.argv)
        except (OSError, ValueError) as error:
            print(error, file=sys.stderr)
            return 1
    orig = BUNDLE.with_suffix(".js.orig")
    if "--restore" in sys.argv:
        shutil.copy2(orig, BUNDLE)
        print(f"restored {BUNDLE} from {orig}")
        return 0
    text = BUNDLE.read_text(encoding="utf-8")
    if MARK in text:
        print("already patched:", BUNDLE)
        return 0
    for old, _new, n in EDITS:
        c = text.count(old)
        if c != n:
            print(f"ANCHOR MISMATCH ({c} found, {n} expected): {old[:80]!r}")
            return 1
    if "--check" in sys.argv:
        print("anchors OK, not patched yet")
        return 0
    for old, new, _n in EDITS:
        text = text.replace(old, new)
    if not orig.exists():
        shutil.copy2(BUNDLE, orig)
    BUNDLE.write_text(text, encoding="utf-8", newline="\n")
    print(f"patched {BUNDLE} (backup {orig})")
    return 0


if __name__ == "__main__":
    sys.exit(main())
