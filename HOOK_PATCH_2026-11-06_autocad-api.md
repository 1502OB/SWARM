# Hook patch 2026-11-06: let scripter and script-debugger run the autocad-api checker

Claude's edit to this hook was blocked by the auto-mode classifier (self-modification), so it is written down here
for Jeremy to apply. Until then, the generated prompts say "if your Bash allows it, run the checker, otherwise
Grep the references", and the orchestrator runs the checker before every writer (MARKUP_SWARM_PROMPT step 10).

File: `C:/Users/Jeremy/.claude/hooks/bash_allowlist_guard.py`. The checker
`~/.claude/skills/autocad-api/scripts/acad_api_check.py` only reads the skill's references and the .lsp files it is
given.

1. After `HOME = r"(C:|/c)/Users/Jeremy"` add:
   ```python
   ACAD_API = HOME + r"/\.claude/skills/autocad-api/scripts/acad_api_check\.py"   # 2026-11-06: read-only API checker
   ```
2. In `PROFILES`, append `script(ACAD_API)` to the `"script-debugger"` list and to the `"scripter"` list.
3. In the `allowed` help texts for `"script-debugger"` and `"scripter"`, add
   `python C:/Users/Jeremy/.claude/skills/autocad-api/scripts/acad_api_check.py --lsp <file.lsp> [--with <lib.lsp>]`.
4. Docstring line: `script-debugger: tools/lspcheck.py, validate.py, autocad-api acad_api_check.py (also scripter).`

Test (each must print the expected rc), then `python ~/.claude/hooks/tests/test_guards.py` and
`python ~/swarm/build/sync_claude.py pull`:
```bash
t(){ printf '{"tool_name":"Bash","tool_input":{"command":%s}}' "$(python -c 'import json,sys;print(json.dumps(sys.argv[1]))' "$2")" | python ~/.claude/hooks/bash_allowlist_guard.py $1 >/dev/null 2>&1; echo "$1 rc=$? :: $2"; }
t scripter 'python C:/Users/Jeremy/.claude/skills/autocad-api/scripts/acad_api_check.py --lsp C:/Users/Jeremy/swarm/scripts/C22-R7-T1.lsp --with C:/Users/Jeremy/swarm/library/acad/swarm_lib.lsp'   # 0
t script-debugger 'python /c/Users/Jeremy/.claude/skills/autocad-api/scripts/acad_api_check.py vla-AddText Line.Layer'   # 0
t scripter 'python C:/Users/Jeremy/.claude/skills/autocad-api/scripts/acad_api_check.py --lsp a.lsp; rm -rf /'          # 2
t scripter 'python C:/Users/Jeremy/.claude/skills/autocad-api/scripts/evil.py x'                                         # 2
t sheet-reviewer 'python C:/Users/Jeremy/.claude/skills/autocad-api/scripts/acad_api_check.py x'                         # 2
```
