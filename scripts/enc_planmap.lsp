;; Read-only: map model-space DCW callouts to the layout viewports that show them.
;; Writes C:/Users/Jeremy/swarm/manifests/ENC-S3.planmap.txt
(defun c:ENC-PLANMAP ( / f ss i ed lay ctr h w tw out s txt)
  (setq f (open "C:/Users/Jeremy/swarm/manifests/ENC-S3.planmap.txt" "w"))
  ;; viewports: VP|handle|layout|viewctrX,Y|height|width|twist(rad)|frozen-layer-count
  (setq ss (ssget "_X" '((0 . "VIEWPORT"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)) lay (cdr (assoc 410 ed)))
    (if (and (/= lay "Model") (/= (cdr (assoc 69 ed)) 1))
      (write-line (strcat "VP|" (cdr (assoc 5 ed)) "|" lay "|"
        (rtos (car (cdr (assoc 12 ed))) 2 2) "," (rtos (cadr (cdr (assoc 12 ed))) 2 2) "|"
        (rtos (cdr (assoc 45 ed)) 2 2) "|"
        (rtos (* (cdr (assoc 45 ed)) (/ (cdr (assoc 40 ed)) (cdr (assoc 41 ed)))) 2 2) "|"
        (rtos (cdr (assoc 51 ed)) 2 6) "|" (rtos (car (cdr (assoc 17 ed))) 2 2) "," (rtos (cadr (cdr (assoc 17 ed))) 2 2)) f))
    (setq i (1+ i))))
  ;; model-space callouts: CO|handle|layer|x,y|text
  (setq ss (ssget "_X" '((-4 . "<OR") (0 . "TEXT") (0 . "MTEXT") (0 . "MULTILEADER") (-4 . "OR>") (410 . "Model"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)) txt (cdr (assoc 0 ed)))
    (setq s (cond ((= txt "MULTILEADER") (cdr (assoc 304 ed))) (t (cdr (assoc 1 ed)))))
    (if (and s (wcmatch (strcase s) "*DCW*,*CW*,*WSFU*"))
      (write-line (strcat "CO|" (cdr (assoc 5 ed)) "|" txt "|" (cdr (assoc 8 ed)) "|"
        (rtos (car (cdr (assoc 10 ed))) 2 1) "," (rtos (cadr (cdr (assoc 10 ed))) 2 1) "|" s) f))
    (setq i (1+ i))))
  ;; layout titles: LT|layout|text  (TEXT/MTEXT in paper space containing PLAN or BUILDING)
  (setq ss (ssget "_X" '((-4 . "<OR") (0 . "TEXT") (0 . "MTEXT") (-4 . "OR>") (-4 . "<NOT") (410 . "Model") (-4 . "NOT>"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)) s (cdr (assoc 1 ed)))
    (if (and s (wcmatch (strcase s) "*FLOOR PLAN*,*BUILDING*,*BLDG*,*ROOF PLAN*,*ENLARGED*"))
      (write-line (strcat "LT|" (cdr (assoc 410 ed)) "|" (substr s 1 120)) f))
    (setq i (1+ i))))
  (close f) (princ "ENC-PLANMAP done") (princ))
