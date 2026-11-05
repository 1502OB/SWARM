;; TB_delta5.lsp - run with TB.dwg (00 BIM_CAD\01_X_Arch\TB\TB.dwg) open and active:
;;   (load "C:/Users/Jeremy/swarm/scripts/TB_delta5.lsp")  then type  TBDELTA5
;; Adds delta 5 / PLAN CHECK RESPONSE / 10-16-2026 in the top revision row (y 7.86-8.19)
;; and changes LAST ISSUE DATE (handle 48D4) from 05-12-2026 to 10-16-2026. Does not save.
;; Backup of the original: C:\Users\Jeremy\swarm\archive\encino\TB_before_delta5_2026-10-16.dwg
(defun _tb-txt (s x y h j)
  (entmake (list '(0 . "TEXT") '(8 . "A-SHT-TEXT") '(7 . "Style-WORKING")
                 (cons 10 (list x y 0.0)) (cons 11 (list x y 0.0)) (cons 40 h) (cons 1 s)
                 (cons 72 j) (cons 73 (if (= j 1) 2 0)))))
;; Is anything (text, block, polyline) already placed inside the top revision row? Ignores the row's ruled lines.
(defun _tb-row-used ( / ss i ed p hit)
  (setq ss (ssget "_X" '((410 . "Model") (0 . "TEXT,MTEXT,INSERT,LWPOLYLINE"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)) p (cdr (assoc 10 ed)))
    (if (and p (> (car p) 38.65) (< (car p) 41.11) (> (cadr p) 7.87) (< (cadr p) 8.18)) (setq hit T))
    (setq i (1+ i))))
  hit)
(defun c:TBDELTA5 ( / d ed ss)
  (setq d (handent "48D4"))
  (cond
    ((not (and d (setq ed (entget d)) (= (cdr (assoc 1 ed)) "05-12-2026")))
     (princ "\nTBDELTA5: LAST ISSUE DATE text 48D4 not found or not 05-12-2026 - nothing changed."))
    ((_tb-row-used)
     (princ "\nTBDELTA5: top revision row is not empty - nothing changed."))
    (t
     (entmake (list '(0 . "LWPOLYLINE") '(100 . "AcDbEntity") '(8 . "A-SHT-TEXT") '(100 . "AcDbPolyline")
                    '(90 . 3) '(70 . 1) '(10 38.70 7.915) '(10 38.94 7.915) '(10 38.82 8.135)))
     (_tb-txt "5" 38.82 7.985 0.075 1)                  ; centered in the triangle
     (_tb-txt "PLAN CHECK RESPONSE" 39.02 7.985 0.083 0)
     (_tb-txt "10-16-2026" 41.06 7.985 0.083 2)         ; right-aligned at the row edge
     (entmod (subst (cons 1 "10-16-2026") (assoc 1 ed) ed))
     (princ "\nTBDELTA5: delta 5 row added, LAST ISSUE DATE = 10-16-2026. Review, then SAVE.")))
  (princ))
