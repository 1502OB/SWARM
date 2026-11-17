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
Running sessions keep the old code until their revit-mcp server restarts (/mcp reconnect or new session).
"""
import os
import shutil
import sys
from pathlib import Path

MARK = "/* swarm-multi-instance-patch v2 */"
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


def main():
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
