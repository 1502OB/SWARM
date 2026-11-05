;; T4.lsp -- SWARM-TEST layer + circle + text in MODEL space.
;; No (command), no setvar, no save. Load with:
;;   (load "C:/Users/Jeremy/swarm/scripts/T4.lsp")
;; Then: (c:T4-RUN) / (c:T4-CHECK) / (c:T4-CLEAN)

(vl-load-com)

(defun _t4-n (flt / ss) (if (setq ss (ssget "_X" flt)) (sslength ss) 0))

(defun _t4-split (s / p r)
  (while (setq p (vl-string-search " " s))
    (if (> p 0) (setq r (cons (substr s 1 p) r)))
    (setq s (substr s (+ p 2))))
  (if (> (strlen s) 0) (setq r (cons s r)))
  (reverse r))

(defun _t4-dellayer (/ r)
  (setq r (vl-catch-all-apply
            '(lambda ()
               (vla-delete
                 (vla-item (vla-get-layers (vla-get-activedocument (vlax-get-acad-object)))
                           "SWARM-TEST")))))
  (if (vl-catch-all-error-p r) (vl-catch-all-error-message r) "deleted"))

(defun c:T4-RUN (/ hc ht)
  (cond
    ((tblsearch "LAYER" "SWARM-TEST") "ABORT: layer SWARM-TEST already exists")
    ((> (_t4-n '((8 . "SWARM-TEST"))) 0) "ABORT: entities already on SWARM-TEST")
    ((not (entmake '((0 . "LAYER") (100 . "AcDbSymbolTableRecord")
                     (100 . "AcDbLayerTableRecord") (2 . "SWARM-TEST")
                     (70 . 0) (62 . 1) (6 . "Continuous"))))
     "ABORT: layer entmake failed")
    (T
     (if (entmake '((0 . "CIRCLE") (67 . 0) (410 . "Model") (8 . "SWARM-TEST")
                    (10 0.0 0.0 0.0) (40 . 12.0)))
       (setq hc (cdr (assoc 5 (entget (entlast))))))
     (if (and hc
              (entmake (list '(0 . "TEXT") '(67 . 0) '(410 . "Model") '(8 . "SWARM-TEST")
                             (cons 7 (getvar "TEXTSTYLE"))
                             '(10 0.0 0.0 0.0) '(11 0.0 0.0 0.0)
                             '(40 . 2.0) '(1 . "SWARM TEST") '(50 . 0.0)
                             '(72 . 1) '(73 . 2))))
       (setq ht (cdr (assoc 5 (entget (entlast))))))
     (if (and hc ht)
       (progn (setq *T4-HANDLES* (list hc ht))
              (strcat "OK circle=" hc " text=" ht))
       (progn ; roll back partial work
         (if hc (entdel (handent hc)))
         (setq *T4-HANDLES* nil)
         (strcat "ERROR: entmake failed circle=" (if hc hc "nil")
                 " text=nil; rolled back, layer=" (_t4-dellayer)))))))

(defun c:T4-CLEAN (/ hs lst e ed n)
  (setq hs (cond (*T4-HANDLES*)
                 ((= (type *T4-H*) 'STR) (_t4-split *T4-H*)))
        n 0)
  ;; collect live entities on SWARM-TEST only, then delete once each
  (foreach h hs
    (if (and (setq e (handent h))
             (setq ed (entget e))
             (= (strcase (cdr (assoc 8 ed))) "SWARM-TEST")
             (not (member e lst)))
      (setq lst (cons e lst))))
  (foreach e lst (entdel e) (setq n (1+ n)))
  (setq *T4-HANDLES* nil)
  (strcat "CLEAN deleted=" (itoa n) " layer="
          (if (tblsearch "LAYER" "SWARM-TEST") (_t4-dellayer) "absent")))

(defun c:T4-CHECK ()
  (strcat "layer=" (if (tblsearch "LAYER" "SWARM-TEST") "yes" "no")
          " onlayerMS=" (itoa (_t4-n '((8 . "SWARM-TEST") (410 . "Model"))))
          " onlayerAll=" (itoa (_t4-n '((8 . "SWARM-TEST"))))
          " circMS=" (itoa (_t4-n '((0 . "CIRCLE") (410 . "Model"))))
          " textMS=" (itoa (_t4-n '((0 . "TEXT") (410 . "Model"))))
          " totalMS=" (itoa (_t4-n '((410 . "Model"))))
          " ctab=" (getvar "CTAB")))
