"""lsp_lint W5 / W6 / W7 on the real failure shapes of the Mansfield run (LESSONS A31 A36 A43)."""
import _util  # noqa: F401  (sys.path)
from _util import write_tmp, run_module_tests

import lsp_lint


def codes(text, code):
    errors, warnings = lsp_lint.lint(str(write_tmp(text)))
    assert not errors, errors
    return [w for w in warnings if w[0] == code]


# ---- W5 (A36): c: command that only princ's
def test_w5_bare_princ():
    w = codes('(defun c:X1-R1-T1-RUN ( / r) (setq r (x1:run)) (princ r) (princ))', "W5")
    assert len(w) == 1 and "swl:result-out" in w[0][2], w


def test_w5_princ_symbol_unknown():
    assert len(codes('(defun c:X1-R1-T1-RUN ( / r) (setq r (x1:run)) (princ r))', "W5")) == 1


def test_w5_guard_then_princ_c22_shape():
    # scripts/C22-R7-T1.lsp: (defun c:C22-R7-T1-PLAN () (r7-guard "PLAN" 'r7-cmd-plan) (princ))
    assert len(codes("(defun c:C22-R7-T1-PLAN () (r7-guard \"PLAN\" 'r7-cmd-plan) (princ))", "W5")) == 1


def test_w5_ok_returns_string():
    assert not codes('(defun c:X1-R1-T1-RUN ( / r) (setq r (x1:run)) (swl:result-out "X1-R1-T1" r))', "W5")
    assert not codes('(defun c:X1-R1-T1-RUN ( / r) (setq r (strcat "a" "b")) (princ r))', "W5")
    assert not codes('(defun c:X1-R1-T1-RUN () (princ (strcat "RUN done=" "1")))', "W5")
    assert not codes('(defun x1:helper () (princ))', "W5")  # not a c: command


def test_w5_cos_t4_shape_clean():
    # scripts/M18-COS-T4.v1.lsp: PLAN returns the strcat of a cond
    t = ('(defun c:M18-COS-T4-PLAN ( / err) (c4:stage0 "plan") (setq err (c4:build))'
         ' (cond (err (c4:abort err)) (T (strcat (c4:planline) " stages=" C4G:STAGE))))')
    assert not codes(t, "W5")


# ---- W6 (A31): (setq v (or ...)) then v used as a value
T9_V1 = '''(defun c18r3t9:insbox (ed xname / nm bt e)
  (setq nm (cdr (assoc 2 ed)))
  (setq bt (or (tblobjname "BLOCK" (strcat xname "|" nm)) (tblobjname "BLOCK" nm)))
  (if bt (progn (setq e (entnext bt)) e)))'''

T9_V3 = '''(defun c18r3t9:insbox (ed xname / nm bt e)
  (setq nm (cdr (assoc 2 ed)))
  (setq bt (cond ((tblobjname "BLOCK" (strcat xname "|" nm))) ((tblobjname "BLOCK" nm)) (T nil)))
  (if bt (progn (setq e (entnext bt)) e)))'''


def test_w6_t9_v1_or_value():
    w = codes(T9_V1, "W6")
    assert len(w) == 1 and "bt" in w[0][2] and "cond" in w[0][2], w


def test_w6_t9_v3_cond_clean():
    assert not codes(T9_V3, "W6")


def test_w6_boolean_use_clean():
    # used only as a test: no warning (C22-R2-T1c mv, M18-R1-T3 fok shapes)
    t = '(defun f (a b / mv) (setq mv (or (> a 1) (g b))) (if (not mv) 0 1))'
    assert not codes(t, "W6")
    t = '(defun f (a b / ok) (setq ok (or (= a 1) (> b 2))) (list ok))'  # all-predicate or: boolean on purpose
    assert not codes(t, "W6")
    t = '(defun f (a / x) (setq x (or a 0)) (cond (x 1) (T 2)))'  # cond clause test
    assert not codes(t, "W6")


# ---- W7 (A43): entget DATA where an ename is expected
T15_V1 = '''(defun m15:mk-note (src x y / o)
  (setq o (vla-Copy (vlax-ename->vla-object src)))
  o)
(defun m15:noteh (src x y ax nw h / o sz)
  (cond (ax (setq o (m15:mk-note src x y)) 1.0) (T 2.0)))
(defun m15:mkplan (ax / t6 nE ed r nh)
  (foreach r t6
    (setq ed (swl:ent (car r)))
    (cond ((= (cadr r) "MTEXT") (setq nE (cons ed nE)))))
  (setq nE (car nE))
  (setq nh (m15:noteh nE (car (cdr (assoc 10 nE))) 0.0 ax 1.0 0.1)))'''


def test_w7_t15_v1_interprocedural():
    w = codes(T15_V1, "W7")
    assert len(w) == 1 and "m15:noteh" in w[0][2] and "nE" in w[0][2], w


def test_w7_direct_sinks():
    t = '(defun f (h / ed o) (setq ed (entget (handent h))) (setq o (vlax-ename->vla-object ed)) (entdel ed))'
    assert len(codes(t, "W7")) == 2
    t = '(defun f (h / ed ss) (setq ed (swl:ent h) ss (ssadd)) (ssadd ed ss))'
    assert len(codes(t, "W7")) == 1


def test_w7_clean_shapes():
    t = '(defun f (h / ed o) (setq ed (swl:ent h)) (setq o (vlax-ename->vla-object (cdr (assoc -1 ed)))) (entmod ed))'
    assert not codes(t, "W7")  # ename extracted; entmod takes the data list
    t = '(defun f (h / ed) (setq ed (swl:ent h)) (setq ed (cdr (assoc -1 ed))) (entdel ed))'
    assert not codes(t, "W7")  # reassigned to the ename before the call


def test_existing_warnings_stay_warnings():
    errors, warnings = lsp_lint.lint(str(write_tmp('(defun f () (setq leak 1) (command "_.LINE" "0,0" "1,1" ""))')))
    assert not errors and {w[0] for w in warnings} >= {"W1", "W4"}


if __name__ == "__main__":
    import sys
    p, f = run_module_tests(globals())
    print(f"test_lsp_lint: {p} passed, {f} failed")
    sys.exit(1 if f else 0)
