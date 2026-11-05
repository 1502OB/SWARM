;; read-only probe of MLEADER annotation contexts
(defun ap-r (x) (mapcar '(lambda (v) (rtos v 2 3)) x))
(defun ap-ctx (h / e dm sd r d p inll v1)
  (setq e (entget (handent h) '("*")))
  (setq r (strcat h " " (cdr (assoc 0 e)) " main10=" (vl-princ-to-string (ap-r (cdr (assoc 10 e)))) " || "))
  (setq dm (cdr (assoc 360 e)))
  (if (not dm) (strcat r "NO EXT DICT")
    (progn
      (setq sd (cdr (assoc 350 (entget (cdr (assoc 360 (entget dm)))))))
      (foreach p (entget sd)
        (if (= (car p) 350)
          (progn
            (setq d (entget (cdr p)) inll nil v1 nil)
            (foreach q d
              (if (= (car q) 304) (setq inll T))
              (if (and inll (null v1) (= (car q) 10)) (setq v1 (cdr q))))
            (setq r (strcat r "scale=" (cdr (assoc 300 (entget (cdr (assoc 340 d)))))
                            " 40=" (rtos (cdr (assoc 40 d)) 2 2)
                            " 10=" (vl-princ-to-string (ap-r (cdr (assoc 10 d))))
                            " 110=" (vl-princ-to-string (ap-r (cdr (assoc 110 d))))
                            " v0=" (vl-princ-to-string (if v1 (ap-r v1) "nil")) " || ")))))
      r)))
