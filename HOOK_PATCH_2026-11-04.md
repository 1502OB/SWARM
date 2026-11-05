# Hook patch for the swarm upgrade (2026-11-04) — Jeremy applies

Claude cannot edit `~/.claude/hooks/` (classifier; your standing rule). Until this is applied, every Bash call by
these agents is blocked ("unknown profile"), so:
- revit-writer / acad-writer cannot run validate.py → they stop every task with status `invalid_manifest`;
- verifier cannot validate expected.json → it reports "invalid expected.json" instead of verifying;
- snapshot-taker cannot back up files → it reports backup blocked (the snapshot JSON itself still gets written);
- visual-verifier cannot crop; script-debugger cannot run lspcheck; filer cannot copy.

## Apply

A complete patched copy is staged and tested at `swarm/build/staged/bash_allowlist_guard.py`.

```
copy C:\Users\Jeremy\swarm\build\staged\bash_allowlist_guard.py C:\Users\Jeremy\.claude\hooks\bash_allowlist_guard.py
```

(Back up first if you like: the current file is unchanged since the 2026-11-03 version.)

## What it adds (diff)

Five profiles, one command each (same `script()` matcher as the existing ones: no chaining, pipes, redirects or `$()`):

| Profile | Agents | Allowed |
|---|---|---|
| swarm-validate | revit-writer, acad-writer, verifier | `python C:/Users/Jeremy/swarm/validate.py <schema> <file>` |
| snapshot-taker | snapshot-taker | `python .../swarm/tools/snapshot_backup.py <run-id> <file>...` |
| visual-verifier | visual-verifier | `python .../swarm/tools/visual_crop.py <pdf> <page> <rect> <task-id> [--units in]` |
| script-debugger | script-debugger | `python .../swarm/tools/lspcheck.py <file>` or validate.py |
| filer | filer | `python .../swarm/tools/file_deliverable.py <src> <dest> <name> <project>` |

It also changes the help-text lookup from `[profile]` to `.get(profile, ...)`. Without that, a profile with no help
text raises KeyError → exit 1 → the hook does NOT block (exit 2 is the only blocking code), i.e. a missing entry
would silently allow every command for that agent.

## Tested (staged copy, 2026-11-04)

Allowed (exit 0): validate.py with two files; snapshot_backup.py with a quoted path containing a space;
visual_crop.py with --units in; lspcheck.py; file_deliverable.py; existing doc-scout `ls` unchanged.
Blocked (exit 2): validate.py `&& rm -rf x`; `rm -rf`; `cp a b` for snapshot-taker; snapshot_backup.py run by
script-debugger; unknown profile. Live hook today: new profiles exit 2 (blocked) as expected.
