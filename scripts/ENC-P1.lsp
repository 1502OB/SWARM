;;; ENC-P1.lsp  --  Encino Village P1.00 / P1.01 water callout revisions (40 text edits)
;;; Commands:
;;;   (c:ENC-P1-RUN)    pre-check every entity == old_text, then set new_text (one undo group)
;;;   (c:ENC-P1-CHECK)  "match=N mismatch=<handles> keep=K/2"
;;;   (c:ENC-P1-REVERT) pre-check every entity == new_text, then set old_text (one undo group)
;;; Command-free: no (command), no layout switching, no save.
;;; Edit record: (handle layout layer type old_text new_text)
;;; type "ML" = MULTILEADER (vla TextString), "TX" = TEXT (group 1)

(vl-load-com)

(setq ENC-P1-LONG-OLD "2\"DCW\\PP.C. TO PROVIDE BALL VALVE & CAP FOR FUTURE TENANT CONNECTION\\PPRV & SUBMETER TO BE PROVIDED UNDER FUTURE PERMIT (TYPICAL)")
(setq ENC-P1-LONG-NEW "2-1/2\"DCW\\PP.C. TO PROVIDE BALL VALVE & CAP FOR FUTURE TENANT CONNECTION\\PPRV & SUBMETER TO BE PROVIDED UNDER FUTURE PERMIT (TYPICAL)")
(setq ENC-P1-FT "MAX WSFU (F.T.) = 69")
(setq ENC-P1-FV "MAX WSFU (F.V.) = 69")

(setq ENC-P1-EDITS
  (list
    ;; ---- P1.00 Bldg A main / branch callouts
    (list "35C7E" "P1.00" "P-TEXT-WATER" "ML" "3\"DCW \\P440 WSFU"     "3-1/2\"DCW \\P440 WSFU")
    (list "390DC" "P1.00" "P-TEXT-WATER" "ML" "2-1/2\"DCW \\P264 WSFU" "3\"DCW \\P264 WSFU")
    (list "390D7" "P1.00" "P-TEXT-WATER" "ML" "2-1/2\"DCW \\P220 WSFU" "3\"DCW \\P220 WSFU")
    (list "35C65" "P1.00" "P-TEXT-WATER" "ML" "2-1/2\"DCW \\P176 WSFU" "3\"DCW \\P176 WSFU")
    (list "390E6" "P1.00" "P-TEXT-WATER" "ML" "2\"DCW \\P88 WSFU"      "2-1/2\"DCW \\P88 WSFU")
    (list "35C5B" "P1.00" "P-TEXT-WATER" "ML" "2\"DCW \\P88 WSFU"      "2-1/2\"DCW \\P88 WSFU")
    (list "390E1" "P1.00" "P-TEXT-WATER" "ML" "2\"DCW \\P44 WSFU"      "2-1/2\"DCW \\P44 WSFU")
    (list "35C3E" "P1.00" "P-TEXT-WATER" "ML" "2\"DCW \\P44 WSFU"      "2-1/2\"DCW \\P44 WSFU")
    ;; ---- P1.00 tenant branch callouts
    (list "39092" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "3908D" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35C36" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35C25" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35B7D" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35B6C" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35B59" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35B4A" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35B37" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35AF5" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35AE6" "P1.00" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    ;; ---- P1.01 Bldg B main / branch callouts
    (list "38879" "P1.01" "P-TEXT-WATER" "ML" "3\"DCW \\P552 WSFU"     "3-1/2\"DCW \\P552 WSFU")
    (list "35821" "P1.01" "P-TEXT-WATER" "ML" "2-1/2\"DCW \\P276 WSFU" "3\"DCW \\P276 WSFU")
    (list "35826" "P1.01" "P-TEXT-WATER" "ML" "2-1/2\"DCW \\P207 WSFU" "3\"DCW \\P207 WSFU")
    (list "3582B" "P1.01" "P-TEXT-WATER" "ML" "2\"DCW \\P138 WSFU"     "2-1/2\"DCW \\P138 WSFU")
    (list "35830" "P1.01" "P-TEXT-WATER" "ML" "2\"DCW \\P69 WSFU"      "2-1/2\"DCW \\P69 WSFU")
    ;; ---- P1.01 tenant branch callouts
    (list "37F0E" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "35802" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "357EF" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "357D8" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "357C9" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "357B6" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "3571B" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    (list "3570C" "P1.01" "P-TEXT-WATER" "ML" ENC-P1-LONG-OLD ENC-P1-LONG-NEW)
    ;; ---- P1.01 tenant TEXT labels F.T. -> F.V.
    (list "39685" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "39683" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "39681" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "3967F" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "3967D" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "3967B" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "39679" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
    (list "39662" "P1.01" "P-TEXT" "TX" ENC-P1-FT ENC-P1-FV)
  )
)

;; kept-unchanged MULTILEADERs (must NOT be touched): (handle expected_text)
(setq ENC-P1-KEEP
  (list (list "35B8F" "3\"DCW \\P484 WSFU")
        (list "3858D" "3\"DCW \\P552 WSFU")))

;; ---------------------------------------------------------------- helpers
(defun enc-p1-ename (h / e)
  (setq e (handent h))
  (if (and e (entget e)) e nil))   ; handent can return erased enames; entget is nil then

;; current text of an ename, by its actual type; nil if unreadable
(defun enc-p1-gettext (e / ed typ r)
  (setq ed (entget e) typ (cdr (assoc 0 ed)))
  (cond
    ((= typ "TEXT") (cdr (assoc 1 ed)))
    ((= typ "MULTILEADER")
     (setq r (vl-catch-all-apply 'vla-get-TextString (list (vlax-ename->vla-object e))))
     (if (vl-catch-all-error-p r) nil r))
    (T nil)))

;; set text; returns T on success
(defun enc-p1-settext (e s / ed typ r)
  (setq ed (entget e) typ (cdr (assoc 0 ed)))
  (cond
    ((= typ "TEXT")
     (if (entmod (subst (cons 1 s) (assoc 1 ed) ed)) (progn (entupd e) T) nil))
    ((= typ "MULTILEADER")
     (setq r (vl-catch-all-apply 'vla-put-TextString (list (vlax-ename->vla-object e) s)))
     (not (vl-catch-all-error-p r)))
    (T nil)))

;; pre-check one record against expected text; returns nil if ok, else "HANDLE:code"
;; codes: gone, type, lay(out), layer, txt
(defun enc-p1-precheck (rec want / h e ed typ)
  (setq h (nth 0 rec) e (enc-p1-ename h))
  (cond
    ((null e) (strcat h ":gone"))
    (T
     (setq ed (entget e) typ (cdr (assoc 0 ed)))
     (cond
       ((/= typ (if (= (nth 3 rec) "ML") "MULTILEADER" "TEXT")) (strcat h ":type"))
       ((/= (cdr (assoc 410 ed)) (nth 1 rec)) (strcat h ":lay"))
       ((/= (strcase (cdr (assoc 8 ed))) (strcase (nth 2 rec))) (strcat h ":layer"))
       ((/= (enc-p1-gettext e) want) (strcat h ":txt"))
       (T nil)))))

(defun enc-p1-join (lst / r)
  (setq r "")
  (foreach x lst (setq r (if (= r "") x (strcat r " " x))))
  r)

;; core: from-idx / to-idx index into record (4 = old, 5 = new)
(defun enc-p1-apply (from to tag / bad m doc done fail e)
  (setq bad nil)
  (foreach rec ENC-P1-EDITS
    (if (setq m (enc-p1-precheck rec (nth from rec))) (setq bad (cons m bad))))
  (if bad
    (strcat "REFUSED " tag " n=" (itoa (length bad)) " " (enc-p1-join (reverse bad)))
    (progn
      (setq doc (vla-get-ActiveDocument (vlax-get-acad-object)) done nil fail nil)
      (vla-StartUndoMark doc)
      (foreach rec ENC-P1-EDITS
        (if (null fail)
          (progn
            (setq e (enc-p1-ename (nth 0 rec)))
            (if (enc-p1-settext e (nth to rec))
              (setq done (cons rec done))
              (setq fail (nth 0 rec))))))
      (if fail
        ;; roll back what was already applied, so the drawing is left untouched
        (foreach rec done (enc-p1-settext (enc-p1-ename (nth 0 rec)) (nth from rec))))
      (vla-EndUndoMark doc)
      (if fail
        (strcat "FAILED " tag " at " fail " rolledback=" (itoa (length done)))
        (strcat "OK " tag " n=" (itoa (length done)))))))

;; ---------------------------------------------------------------- commands
(defun c:ENC-P1-RUN () (enc-p1-apply 4 5 "RUN"))

(defun c:ENC-P1-REVERT () (enc-p1-apply 5 4 "REVERT"))

(defun c:ENC-P1-CHECK (/ n bad e k)
  (setq n 0 bad nil k 0)
  (foreach rec ENC-P1-EDITS
    (setq e (enc-p1-ename (nth 0 rec)))
    (if (and e (= (enc-p1-gettext e) (nth 5 rec)))
      (setq n (1+ n))
      (setq bad (cons (nth 0 rec) bad))))
  (foreach kp ENC-P1-KEEP
    (setq e (enc-p1-ename (car kp)))
    (if (and e (= (enc-p1-gettext e) (cadr kp))) (setq k (1+ k))))
  (strcat "match=" (itoa n) " mismatch=" (if bad (enc-p1-join (reverse bad)) "none")
          " keep=" (itoa k) "/2"))

(princ "\nENC-P1 loaded: ENC-P1-RUN / ENC-P1-CHECK / ENC-P1-REVERT")
(princ)
