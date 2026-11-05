;; Read-only inventory of revision conventions. Writes ENC-S4.revinv.txt
(defun _ri-atts (e / r a ad)
  (setq r "" a (entnext e))
  (while (and a (= (cdr (assoc 0 (setq ad (entget a)))) "ATTRIB"))
    (setq r (strcat r (cdr (assoc 2 ad)) "=" (cdr (assoc 1 ad)) "; ") a (entnext a)))
  r)
(defun c:ENC-REVINV ( / f ss i ed nm lay tb)
  (setq f (open "C:/Users/Jeremy/swarm/manifests/ENC-S4.revinv.txt" "w"))
  ;; layers that look revision-related
  (setq tb (tblnext "LAYER" T))
  (while tb
    (if (wcmatch (strcase (cdr (assoc 2 tb))) "*REV*,*DELTA*,*CLOUD*")
      (write-line (strcat "LAYER|" (cdr (assoc 2 tb)) "|color=" (itoa (cdr (assoc 62 tb)))) f))
    (setq tb (tblnext "LAYER")))
  ;; block inserts named like delta/rev, plus every paper-space insert with attributes on P1.00/P1.01 (title block)
  (setq ss (ssget "_X" '((0 . "INSERT"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)) nm (strcase (cdr (assoc 2 ed))) lay (cdr (assoc 410 ed)))
    (if (or (wcmatch nm "*DELTA*,*REV*,*TRI*")
            (and (member lay '("P1.00" "P1.01")) (= (cdr (assoc 66 ed)) 1)))
      (write-line (strcat "INS|" (cdr (assoc 5 ed)) "|" lay "|" (cdr (assoc 2 ed)) "|" (cdr (assoc 8 ed)) "|"
                          (rtos (car (cdr (assoc 10 ed))) 2 2) "," (rtos (cadr (cdr (assoc 10 ed))) 2 2) "|sc="
                          (rtos (cdr (assoc 41 ed)) 2 3) "|" (if (= (cdr (assoc 66 ed)) 1) (_ri-atts (ssname ss i)) "")) f))
    (setq i (1+ i))))
  ;; clouds: polylines with bulges on rev/cloud layers (or any layer) in paper space
  (setq ss (ssget "_X" '((0 . "LWPOLYLINE"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)))
    (if (and (/= (cdr (assoc 410 ed)) "Model")
             (wcmatch (strcase (cdr (assoc 8 ed))) "*REV*,*CLOUD*,*DELTA*"))
      (write-line (strcat "CLOUD|" (cdr (assoc 5 ed)) "|" (cdr (assoc 410 ed)) "|" (cdr (assoc 8 ed)) "|verts="
                          (itoa (cdr (assoc 90 ed))) "|lw=" (rtos (cond ((cdr (assoc 43 ed))) (0.0)) 2 3)) f))
    (setq i (1+ i))))
  ;; text that looks like a delta number / revision row on P1.00/P1.01
  (setq ss (ssget "_X" '((0 . "TEXT,MTEXT") (-4 . "<OR") (410 . "P1.00") (410 . "P1.01") (410 . "P0.00") (-4 . "OR>"))) i 0)
  (if ss (while (< i (sslength ss))
    (setq ed (entget (ssname ss i)))
    (if (wcmatch (strcase (cdr (assoc 1 ed))) "*REV*,*PLAN CHECK*,*CORRECTION*,*DELTA*")
      (write-line (strcat "TXT|" (cdr (assoc 5 ed)) "|" (cdr (assoc 410 ed)) "|" (cdr (assoc 8 ed)) "|" (substr (cdr (assoc 1 ed)) 1 100)) f))
    (setq i (1+ i))))
  (close f) "revinv done")
