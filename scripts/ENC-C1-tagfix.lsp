;; Move each DELTA-TAG on N_P_REV5 (P1.00/P1.01) so the triangle's lower-left vertex touches
;; its cloud's top-right corner from outside. DELTA-TAG base = triangle centroid; lower-left
;; vertex is at (-0.166029, -0.095857) at scale 1, so insertion = corner + (0.166029, 0.095857).
;; Each tag is paired with the cloud whose top-right corner is nearest to it (same layout).
(vl-load-com)
(defun _tf-corner (ed / xs ys)
  (foreach g ed (if (= (car g) 10) (setq xs (cons (cadr g) xs) ys (cons (caddr g) ys))))
  (list (apply 'max xs) (apply 'max ys) 0.0))
(defun c:ENC-C1-TAGFIX ( / n L ss i e ed clouds tags c best bd d ins tgt)
  (setq n 0)
  (foreach L '("P1.00" "P1.01")
    (setq clouds nil tags nil ss (ssget "_X" (list '(8 . "N_P_REV5") (cons 410 L))) i 0)
    (if ss (while (< i (sslength ss))
      (setq e (ssname ss i) ed (entget e))
      (cond ((= (cdr (assoc 0 ed)) "LWPOLYLINE") (setq clouds (cons (_tf-corner ed) clouds)))
            ((= (cdr (assoc 0 ed)) "INSERT") (setq tags (cons e tags))))
      (setq i (1+ i))))
    (foreach e tags
      (setq ins (cdr (assoc 10 (entget e))) best nil bd 1e9)
      (foreach c clouds (if (< (setq d (distance ins c)) bd) (setq bd d best c)))
      (if best
        (progn
          (setq tgt (list (+ (car best) 0.166029) (+ (cadr best) 0.095857) 0.0))
          (vla-Move (vlax-ename->vla-object e) (vlax-3d-point ins) (vlax-3d-point tgt))
          (setq n (1+ n))))))
  (strcat "moved=" (itoa n)))
;; Check: every tag's lower-left vertex coincides with some cloud's top-right corner (within 0.001)
(defun c:ENC-C1-TAGCHECK ( / ok bad L ss i e ed clouds tags ins v hit)
  (setq ok 0 bad "")
  (foreach L '("P1.00" "P1.01")
    (setq clouds nil tags nil ss (ssget "_X" (list '(8 . "N_P_REV5") (cons 410 L))) i 0)
    (if ss (while (< i (sslength ss))
      (setq e (ssname ss i) ed (entget e))
      (cond ((= (cdr (assoc 0 ed)) "LWPOLYLINE") (setq clouds (cons (_tf-corner ed) clouds)))
            ((= (cdr (assoc 0 ed)) "INSERT") (setq tags (cons e tags))))
      (setq i (1+ i))))
    (foreach e tags
      (setq ins (cdr (assoc 10 (entget e))) v (list (- (car ins) 0.166029) (- (cadr ins) 0.095857) 0.0) hit nil)
      (foreach c clouds (if (< (distance v c) 0.001) (setq hit T)))
      (if hit (setq ok (1+ ok)) (setq bad (strcat bad (cdr (assoc 5 (entget e))) " ")))))
  (strcat "touching=" (itoa ok) " bad=" (if (= bad "") "none" bad)))
