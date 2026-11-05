"""Exact arithmetic check for calc-auditor findings (orchestrator runs it; agents cannot run Python).

Usage: python C:/Users/Jeremy/swarm/tools/calc_check.py <checks.json> <out.json>

checks.json (written by calc-auditor):
  {"sheet": "P0.03", "checks": [
     {"id": "CW-inlet", "expr": "68.0 - 4.2 - 13.9", "stated": 49.9, "tol": 0.05, "where": "pressure at booster inlet"},
     {"id": "CW-maxloss", "expr": "(45.1 - 15.0) / 636.0 * 100", "stated": 4.7}, ...]}
  expr: numbers, + - * / ** (), and the functions round, min, max, abs, sqrt. Nothing else is evaluated.
  tol (optional): default = half a unit in the last decimal place of "stated" (49.9 -> 0.05; 400 -> 0.5).
Writes out.json: {"sheet", "n", "mismatches", "results": [{id, expr, computed, stated, tol, ok}]}. Exit 1 if any mismatch.
"""
import ast
import json
import math
import operator
import sys

OPS = {ast.Add: operator.add, ast.Sub: operator.sub, ast.Mult: operator.mul, ast.Div: operator.truediv,
       ast.Pow: operator.pow, ast.USub: operator.neg, ast.UAdd: operator.pos}
FUNCS = {"round": round, "min": min, "max": max, "abs": abs, "sqrt": math.sqrt}


def ev(node):
    if isinstance(node, ast.Expression):
        return ev(node.body)
    if isinstance(node, ast.Constant) and isinstance(node.value, (int, float)):
        return node.value
    if isinstance(node, ast.BinOp) and type(node.op) in OPS:
        return OPS[type(node.op)](ev(node.left), ev(node.right))
    if isinstance(node, ast.UnaryOp) and type(node.op) in OPS:
        return OPS[type(node.op)](ev(node.operand))
    if isinstance(node, ast.Call) and isinstance(node.func, ast.Name) and node.func.id in FUNCS and not node.keywords:
        return FUNCS[node.func.id](*[ev(a) for a in node.args])
    raise ValueError(f"not allowed: {ast.dump(node)[:60]}")


def default_tol(stated):
    s = repr(float(stated)) if not isinstance(stated, str) else stated
    s = str(stated)
    dec = len(s.split(".")[1]) if "." in s else 0
    return 0.5 * 10 ** (-dec)


def main():
    spec = json.load(open(sys.argv[1], encoding="utf-8-sig"))
    res, bad = [], 0
    for c in spec.get("checks", []):
        r = {"id": c.get("id"), "expr": c.get("expr"), "stated": c.get("stated"), "where": c.get("where")}
        try:
            val = ev(ast.parse(str(c["expr"]), mode="eval"))
            tol = float(c["tol"]) if c.get("tol") is not None else default_tol(c["stated"])
            r.update(computed=round(val, 6), tol=tol, ok=abs(val - float(c["stated"])) <= tol + 1e-9)
        except Exception as e:  # noqa: BLE001
            r.update(computed=None, ok=False, error=str(e))
        bad += 0 if r["ok"] else 1
        res.append(r)
    out = {"sheet": spec.get("sheet"), "n": len(res), "mismatches": bad, "results": res}
    json.dump(out, open(sys.argv[2], "w", encoding="utf-8"), indent=1)
    print(f"calc_check: {len(res)} checks, {bad} mismatch(es) -> {sys.argv[2]}")
    sys.exit(1 if bad else 0)


if __name__ == "__main__":
    main()
