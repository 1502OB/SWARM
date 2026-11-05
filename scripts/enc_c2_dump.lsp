;; Read-only: bounding boxes of annotation content for revision-cloud planning (ENC-C2).
;; Writes C:/Users/Jeremy/swarm/manifests/ENC-C2.bbox.txt lines:
;;   E|handle|space(layout or Model)|type|name|layer|xmin|ymin|xmax|ymax
(vl-load-com)
(defun c:ENC-C2-DUMP ( / f ss i e ed o mn mx ty nm lay n)
  (setq f (open "C:/Users/Jeremy/swarm/manifests/ENC-C2.bbox.txt" "w") n 0)
  (setq ss (ssget "_X" '((0 . "TEXT,MTEXT,MULTILEADER,INSERT,OLE2FRAME,DIMENSION,LEADER")
                         (-4 . "<OR") (410 . "P1.00") (410 . "P1.01") (410 . "Model") (-4 . "OR>"))) i 0)
  (if ss
    (while (< i (sslength ss))
      (setq e (ssname ss i) ed (entget e) o (vlax-ename->vla-object e)
            ty (cdr (assoc 0 ed)) lay (cdr (assoc 410 ed))
            nm (if (= ty "INSERT") (cdr (assoc 2 ed)) ""))
      (if (not (vl-catch-all-error-p (vl-catch-all-apply 'vla-getboundingbox (list o 'mn 'mx))))
        (progn
          (setq mn (vlax-safearray->list mn) mx (vlax-safearray->list mx) n (1+ n))
          (write-line (strcat "E|" (cdr (assoc 5 ed)) "|" (if lay lay "Model") "|" ty "|" nm "|" (cdr (assoc 8 ed)) "|"
                              (rtos (car mn) 2 4) "|" (rtos (cadr mn) 2 4) "|" (rtos (car mx) 2 4) "|" (rtos (cadr mx) 2 4)) f)))
      (setq i (1+ i))))
  (close f)
  (strcat "entities=" (itoa n)))
