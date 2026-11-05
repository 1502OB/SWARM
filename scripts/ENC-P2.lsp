;;; ENC-P2.lsp  --  Encino Village floor plans (MODEL space): tenant branch 2"DCW -> 2-1/2"DCW (17 MULTILEADER edits)
;;; Commands:
;;;   (c:ENC-P2-RUN)    pre-check every entity == old_text, then set new_text (one undo group)
;;;   (c:ENC-P2-CHECK)  "match=N mismatch=<handles> keep=K/5"
;;;   (c:ENC-P2-REVERT) pre-check every entity == new_text, then set old_text (one undo group)
;;; Command-free: no (command), no layout switching, no save.
;;; Edit record: (handle layout layer type old_text new_text)
;;; type "ML" = MULTILEADER (vla TextString), "TX" = TEXT (group 1)
;;; Model-space entities: group 410 may be absent or "Model" -> both accepted; group 67 must not be 1.
;;; All helpers prefixed _p2- so this file can be loaded alongside ENC-P1.lsp without clobbering.

(vl-load-com)

(setq ENC-P2-OLD "2\"DCW\\PP.C. TO PROVIDE BALL VALVE & CAP FOR FUTURE TENANT CONNECTION\\PPRV & SUBMETER TO BE PROVIDED UNDER FUTURE PERMIT (TYPICAL)")
(setq ENC-P2-NEW "2-1/2\"DCW\\PP.C. TO PROVIDE BALL VALVE & CAP FOR FUTURE TENANT CONNECTION\\PPRV & SUBMETER TO BE PROVIDED UNDER FUTURE PERMIT (TYPICAL)")
;; 3902D, 39028, 3283D, 32826 are right-justified: TextString carries a leading \pxqr; paragraph code (kept as-is).
(setq ENC-P2-OLD-R (strcat "\\pxqr;" ENC-P2-OLD))
(setq ENC-P2-NEW-R (strcat "\\pxqr;" ENC-P2-NEW))

(setq ENC-P2-EDITS
  (list
    ;; ---- Building A tenant branch callouts
    (list "29DB6" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "327EF" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "327FD" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "3280B" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "32819" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "3902D" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD-R ENC-P2-NEW-R)
    (list "39028" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD-R ENC-P2-NEW-R)
    (list "3283D" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD-R ENC-P2-NEW-R)
    (list "32826" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD-R ENC-P2-NEW-R)
    ;; ---- Building B tenant branch callouts
    (list "328E8" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "3293C" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "32920" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "3292E" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "328F6" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "379F6" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "32904" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
    (list "32912" "Model" "P-TEXT-WATER" "ML" ENC-P2-OLD ENC-P2-NEW)
  )
)

;; kept-unchanged callouts (must NOT be touched): (handle expected_text); type resolved at runtime
(setq ENC-P2-KEEP
  (list (list "32D45" "3\" CW")
        (list "32D4C" "1\" CW-UG\\P(BY CIVIL)")
        (list "396A4" "2\" CW")
        (list "396B5" "2\" CW")
        (list "396AB" "2\" CW-UP")))

;; ---------------------------------------------------------------- helpers
(defun _p2-ename (h / e)
  (setq e (handent h))
  (if (and e (entget e)) e nil))   ; handent can return erased enames; entget is nil then

;; current text of an ename, by its actual type; nil if unreadable
;; TEXT -> group 1; MULTILEADER / MTEXT -> vla TextString
(defun _p2-gettext (e / ed typ r)
  (setq ed (entget e) typ (cdr (assoc 0 ed)))
  (cond
    ((= typ "TEXT") (cdr (assoc 1 ed)))
    ((member typ '("MULTILEADER" "MTEXT"))
     (setq r (vl-catch-all-apply 'vla-get-TextString (list (vlax-ename->vla-object e))))
     (if (vl-catch-all-error-p r) nil r))
    (T nil)))

;; set text; returns T on success
(defun _p2-settext (e s / ed typ r)
  (setq ed (entget e) typ (cdr (assoc 0 ed)))
  (cond
    ((= typ "TEXT")
     (if (entmod (subst (cons 1 s) (assoc 1 ed) ed)) (progn (entupd e) T) nil))
    ((= typ "MULTILEADER")
     (setq r (vl-catch-all-apply 'vla-put-TextString (list (vlax-ename->vla-object e) s)))
     (not (vl-catch-all-error-p r)))
    (T nil)))

;; layout test: record layout "Model" accepts 410 absent or "Model" (case-insensitive) and 67 /= 1
(defun _p2-layok (ed want / lo)
  (setq lo (cdr (assoc 410 ed)))
  (if (= (strcase want) "MODEL")
    (and (or (null lo) (= (strcase lo) "MODEL"))
         (/= (cdr (assoc 67 ed)) 1))
    (and lo (= lo want))))

;; pre-check one record against expected text; returns nil if ok, else "HANDLE:code"
;; codes: gone, type, lay(out), layer, txt
(defun _p2-precheck (rec want / h e ed typ)
  (setq h (nth 0 rec) e (_p2-ename h))
  (cond
    ((null e) (strcat h ":gone"))
    (T
     (setq ed (entget e) typ (cdr (assoc 0 ed)))
     (cond
       ((/= typ (if (= (nth 3 rec) "ML") "MULTILEADER" "TEXT")) (strcat h ":type"))
       ((not (_p2-layok ed (nth 1 rec))) (strcat h ":lay"))
       ((/= (strcase (cdr (assoc 8 ed))) (strcase (nth 2 rec))) (strcat h ":layer"))
       ((/= (_p2-gettext e) want) (strcat h ":txt"))
       (T nil)))))

(defun _p2-join (lst / r)
  (setq r "")
  (foreach x lst (setq r (if (= r "") x (strcat r " " x))))
  r)

;; core: from-idx / to-idx index into record (4 = old, 5 = new)
(defun _p2-apply (from to tag / bad m doc done fail e)
  (setq bad nil)
  (foreach rec ENC-P2-EDITS
    (if (setq m (_p2-precheck rec (nth from rec))) (setq bad (cons m bad))))
  (if bad
    (strcat "REFUSED " tag " n=" (itoa (length bad)) " " (_p2-join (reverse bad)))
    (progn
      (setq doc (vla-get-ActiveDocument (vlax-get-acad-object)) done nil fail nil)
      (vla-StartUndoMark doc)
      (foreach rec ENC-P2-EDITS
        (if (null fail)
          (progn
            (setq e (_p2-ename (nth 0 rec)))
            (if (_p2-settext e (nth to rec))
              (setq done (cons rec done))
              (setq fail (nth 0 rec))))))
      (if fail
        ;; roll back what was already applied, so the drawing is left untouched
        (foreach rec done (_p2-settext (_p2-ename (nth 0 rec)) (nth from rec))))
      (vla-EndUndoMark doc)
      (if fail
        (strcat "FAILED " tag " at " fail " rolledback=" (itoa (length done)))
        (strcat "OK " tag " n=" (itoa (length done)))))))

;; ---------------------------------------------------------------- commands
(defun c:ENC-P2-RUN () (_p2-apply 4 5 "RUN"))

(defun c:ENC-P2-REVERT () (_p2-apply 5 4 "REVERT"))

(defun c:ENC-P2-CHECK (/ n bad e k)
  (setq n 0 bad nil k 0)
  (foreach rec ENC-P2-EDITS
    (setq e (_p2-ename (nth 0 rec)))
    (if (and e (= (_p2-gettext e) (nth 5 rec)))
      (setq n (1+ n))
      (setq bad (cons (nth 0 rec) bad))))
  (foreach kp ENC-P2-KEEP
    (setq e (_p2-ename (car kp)))
    (if (and e (= (_p2-gettext e) (cadr kp))) (setq k (1+ k))))
  (strcat "match=" (itoa n) " mismatch=" (if bad (_p2-join (reverse bad)) "none")
          " keep=" (itoa k) "/5"))

(princ "\nENC-P2 loaded: ENC-P2-RUN / ENC-P2-CHECK / ENC-P2-REVERT")
(princ)
