;;; ENC-C1  Revision 5 clouds + DELTA-TAG "5" on P1.00 / P1.01 (paper space)
;;; Drawing: 24-040_Encino Village_P_26-10-06.dwg
;;; Commands: (c:ENC-C1-PLAN) read-only | (c:ENC-C1-RUN) | (c:ENC-C1-CHECK) read-only | (c:ENC-C1-CLEAN)
;;; No (command) calls, no tab switching, no saving.

(vl-load-com)

(setq ENC:LAY "N_P_REV5"
      ENC:BLK "DELTA-TAG"
      ENC:BUL 0.520567
      ENC:CHORD 0.10
      ENC:PAD1 0.10
      ENC:GAP 0.60
      ENC:PAD2 0.05
      ENC:TAGOFF 0.05
      ENC:DIR "C:/Users/Jeremy/swarm/manifests/")

;; (layout paper-handles model-handles (vp-handle pc target vc k))
(setq ENC:SPEC
  (list
    (list "P1.00"
      '("393C7" "393C6"
        "35B8F" "35C7E" "390E6" "390E1" "390DC" "390D7" "35C65" "35C5B" "35C3E"
        "39092" "3908D" "35C36" "35C25" "35B7D" "35B6C" "35B59" "35B4A" "35B37" "35AF5" "35AE6"
        "390C7" "390C4" "390C1" "390BE" "390BB" "390B8" "390B5" "390B2" "390AF" "390AC" "390A9")
      '("29DB6" "327EF" "327FD" "3280B" "32819" "3902D" "39028" "3283D" "32826")
      (list "728" '(16.542 18.250) '(-3894.425 6283.252) '(10249.111 -4799.667) (/ 21.380 2052.481)))
    (list "P1.01"
      '("385E0" "3A552"
        "3858D" "38879" "35821" "35826" "3582B" "35830"
        "37F0E" "35802" "357EF" "357D8" "357C9" "357B6" "3571B" "3570C"
        "39685" "39684" "39683" "39682" "39681" "39680" "3967F" "3967E"
        "3967D" "3967C" "3967B" "3967A" "39679" "39678" "39662" "39661")
      '("328E8" "3293C" "32920" "3292E" "328F6" "379F6" "32904" "32912")
      (list "2A4F9" '(16.693 20.684) '(-3894.425 6283.252) '(10243.192 -6603.593) (/ 16.122 1547.670)))))

;; ---------------------------------------------------------------- utils
(defun enc:doc () (vla-get-ActiveDocument (vlax-get-acad-object)))
(defun enc:r3 (x) (rtos x 2 3))
(defun enc:join (lst sep / r)
  (setq r "")
  (foreach s lst (setq r (if (= r "") s (strcat r sep s))))
  r)
(defun enc:first (n lst / r)
  (while (and lst (> n 0)) (setq r (cons (car lst) r) lst (cdr lst) n (1- n)))
  (reverse r))
(defun enc:boxstr (b)
  (strcat "[" (enc:r3 (car b)) "," (enc:r3 (cadr b)) "," (enc:r3 (caddr b)) "," (enc:r3 (cadddr b)) "]"))
(defun enc:err (s) (setq ENC:ERR (cons s ENC:ERR)))
(defun enc:ent (h / e)
  (if (and (setq e (handent h)) (entget e)) e))
(defun enc:norm (p q)
  (list (min (car p) (car q)) (min (cadr p) (cadr q))
        (max (car p) (car q)) (max (cadr p) (cadr q))))
(defun enc:pad (b d)
  (list (- (car b) d) (- (cadr b) d) (+ (caddr b) d) (+ (cadddr b) d)))
(defun enc:ctr (b)
  (list (/ (+ (car b) (caddr b)) 2.0) (/ (+ (cadr b) (cadddr b)) 2.0)))
(defun enc:in (p b)
  (and (>= (car p) (car b)) (<= (car p) (caddr b))
       (>= (cadr p) (cadr b)) (<= (cadr p) (cadddr b))))
(defun enc:clip (b r)
  (list (max (car b) (car r)) (max (cadr b) (cadr r))
        (min (caddr b) (caddr r)) (min (cadddr b) (cadddr r))))
(defun enc:union (a b)
  (list (min (car a) (car b)) (min (cadr a) (cadr b))
        (max (caddr a) (caddr b)) (max (cadddr a) (cadddr b))))
(defun enc:near (a b g)
  (and (<= (- (car a) g) (caddr b)) (<= (- (car b) g) (caddr a))
       (<= (- (cadr a) g) (cadddr b)) (<= (- (cadr b) g) (cadddr a))))
(defun enc:close (a b tol) (<= (abs (- a b)) tol))

;; bounding box (xmin ymin xmax ymax) in the entity's own space
(defun enc:bbox (e / o r mn mx ed)
  (setq o (vlax-ename->vla-object e) mn nil mx nil)
  (setq r (vl-catch-all-apply 'vla-GetBoundingBox (list o 'mn 'mx)))
  (if (and (not (vl-catch-all-error-p r)) mn mx)
    (progn
      (setq mn (vlax-safearray->list mn) mx (vlax-safearray->list mx))
      (enc:norm mn mx))
    (progn  ; fallback (OLE2FRAME corners 10/11)
      (setq ed (entget e))
      (if (and (assoc 10 ed) (assoc 11 ed))
        (enc:norm (cdr (assoc 10 ed)) (cdr (assoc 11 ed)))))))

;; model -> paper : pc + (m - target - vc) * k
(defun enc:xf (p vs / pc tg vc k)
  (setq pc (cadr vs) tg (caddr vs) vc (cadddr vs) k (nth 4 vs))
  (list (+ (car pc) (* k (- (car p) (car tg) (car vc))))
        (+ (cadr pc) (* k (- (cadr p) (cadr tg) (cadr vc))))))

;; validate viewport against given params, return its paper rectangle or nil (error recorded)
(defun enc:vprect (lay vs / e ed c w h vh)
  (setq e (enc:ent (car vs)))
  (cond
    ((null e) (enc:err (strcat "vp" (car vs) ":missing")) nil)
    ((progn (setq ed (entget e)) (/= (cdr (assoc 0 ed)) "VIEWPORT"))
     (enc:err (strcat "vp" (car vs) ":notvp")) nil)
    ((/= (strcase (cdr (assoc 410 ed))) (strcase lay))
     (enc:err (strcat "vp" (car vs) ":layout")) nil)
    ((and (assoc 51 ed) (> (abs (cdr (assoc 51 ed))) 1e-6))
     (enc:err (strcat "vp" (car vs) ":twist")) nil)
    ((not (and (enc:close (car (cdr (assoc 10 ed))) (car (cadr vs)) 0.01)
               (enc:close (cadr (cdr (assoc 10 ed))) (cadr (cadr vs)) 0.01)
               (enc:close (car (cdr (assoc 17 ed))) (car (caddr vs)) 0.01)
               (enc:close (cadr (cdr (assoc 17 ed))) (cadr (caddr vs)) 0.01)
               (enc:close (car (cdr (assoc 12 ed))) (car (cadddr vs)) 0.01)
               (enc:close (cadr (cdr (assoc 12 ed))) (cadr (cadddr vs)) 0.01)
               (setq vh (cdr (assoc 45 ed)))
               (> vh 0.0)
               (< (abs (- (/ (/ (cdr (assoc 41 ed)) vh) (nth 4 vs)) 1.0)) 1e-4)))
     (enc:err (strcat "vp" (car vs) ":params")) nil)
    (T
     (setq c (cdr (assoc 10 ed)) w (cdr (assoc 40 ed)) h (cdr (assoc 41 ed)))
     (list (- (car c) (/ w 2.0)) (- (cadr c) (/ h 2.0))
           (+ (car c) (/ w 2.0)) (+ (cadr c) (/ h 2.0))))))

;; fills ENC:TG ((lay handle paperbox)...), ENC:SKIP, ENC:ERR
(defun enc:collect (/ sp lay vs rect e ed b p q)
  (setq ENC:TG nil ENC:SKIP nil ENC:ERR nil)
  (foreach sp ENC:SPEC
    (setq lay (car sp))
    (if (vl-catch-all-error-p (vl-catch-all-apply 'vla-item (list (vla-get-Layouts (enc:doc)) lay)))
      (enc:err (strcat lay ":nolayout")))
    (foreach h (cadr sp)
      (setq e (enc:ent h))
      (cond
        ((null e) (enc:err (strcat h ":missing")))
        ((progn (setq ed (entget e))
                (or (/= (strcase (cdr (assoc 410 ed))) (strcase lay)) (/= (cdr (assoc 67 ed)) 1)))
         (enc:err (strcat h ":space")))
        ((null (setq b (enc:bbox e))) (enc:err (strcat h ":bbox")))
        (T (setq ENC:TG (cons (list lay h b) ENC:TG)))))
    (setq vs (cadddr sp) rect (enc:vprect lay vs))
    (foreach h (caddr sp)
      (setq e (enc:ent h))
      (cond
        ((null e) (enc:err (strcat h ":missing")))
        ((progn (setq ed (entget e))
                (or (= (cdr (assoc 67 ed)) 1) (/= (strcase (cdr (assoc 410 ed))) "MODEL")))
         (enc:err (strcat h ":space")))
        ((null rect) nil)
        ((null (setq b (enc:bbox e))) (enc:err (strcat h ":bbox")))
        (T
         (setq p (enc:xf (list (car b) (cadr b)) vs)
               q (enc:xf (list (caddr b) (cadddr b)) vs)
               b (enc:norm p q))
         (if (enc:in (enc:ctr b) rect)
           (setq ENC:TG (cons (list lay h (enc:clip b rect)) ENC:TG))
           (setq ENC:SKIP (cons h ENC:SKIP)))))))
  (setq ENC:TG (reverse ENC:TG) ENC:SKIP (reverse ENC:SKIP) ENC:ERR (reverse ENC:ERR))
  ENC:TG)

;; merge boxes overlapping or within ENC:GAP, repeat until stable
(defun enc:cluster (boxes / changed out merged)
  (setq changed T)
  (while changed
    (setq changed nil out nil)
    (foreach b boxes
      (setq merged nil)
      (setq out (mapcar '(lambda (c)
                           (if (and (not merged) (enc:near b c ENC:GAP))
                             (progn (setq merged T changed T) (enc:union b c))
                             c))
                        out))
      (if (not merged) (setq out (append out (list b)))))
    (setq boxes out))
  boxes)

;; ((lay (cloudbox ...)) ...)  -- requires enc:collect first
(defun enc:plan (/ r lay boxes)
  (foreach sp ENC:SPEC
    (setq lay (car sp) boxes nil)
    (foreach tg ENC:TG
      (if (= (car tg) lay) (setq boxes (cons (enc:pad (caddr tg) ENC:PAD1) boxes))))
    (setq boxes (mapcar '(lambda (b) (enc:pad b ENC:PAD2)) (enc:cluster (reverse boxes))))
    (setq r (cons (list lay boxes) r)))
  (reverse r))

;; ---------------------------------------------------------------- cloud geometry
(defun enc:side (p q / L n i r f)
  (setq L (distance p q) n (max 1 (fix (+ 0.5 (/ L ENC:CHORD)))) i 0 r nil)
  (while (< i n)
    (setq f (/ (float i) n))
    (setq r (cons (list (+ (car p) (* f (- (car q) (car p))))
                        (+ (cadr p) (* f (- (cadr q) (cadr p))))) r))
    (setq i (1+ i)))
  (reverse r))

;; CCW from bottom-left; corners are vertices; equal chords per side
(defun enc:cloudpts (b / bl br tr tl)
  (setq bl (list (car b) (cadr b)) br (list (caddr b) (cadr b))
        tr (list (caddr b) (cadddr b)) tl (list (car b) (cadddr b)))
  (append (enc:side bl br) (enc:side br tr) (enc:side tr tl) (enc:side tl bl)))

(defun enc:props (o)
  (vla-put-Layer o ENC:LAY)
  (vl-catch-all-apply 'vla-put-Color (list o 256))
  (vl-catch-all-apply 'vla-put-Linetype (list o "ByLayer"))
  (vl-catch-all-apply 'vla-put-Lineweight (list o -1)))

;; returns the object, or nil on a soft failure
(defun enc:mkcloud (blk b / pts arr o i)
  (setq pts (enc:cloudpts b))
  (setq arr (vlax-make-safearray vlax-vbDouble (cons 0 (1- (* 2 (length pts))))))
  (vlax-safearray-fill arr (apply 'append pts))
  (setq o (vla-AddLightWeightPolyline blk (vlax-make-variant arr)))
  (setq ENC:NEW (cons o ENC:NEW))
  (enc:props o)
  (vla-put-ConstantWidth o 0.0)
  (setq i 0)
  (repeat (length pts) (vla-SetBulge o i ENC:BUL) (setq i (1+ i)))
  (vla-put-Closed o :vlax-true)
  (vla-Update o)
  o)

;; returns the object, or nil if attribute A not found exactly once
(defun enc:mktag (blk b / o n)
  (setq o (vla-InsertBlock blk (vlax-3d-point (list (+ (caddr b) ENC:TAGOFF) (+ (cadddr b) ENC:TAGOFF) 0.0))
                           ENC:BLK 1.0 1.0 1.0 0.0))
  (setq ENC:NEW (cons o ENC:NEW))
  (enc:props o)
  (if (or (/= (vla-get-XScaleFactor o) 1.0) (/= (vla-get-YScaleFactor o) 1.0))
    (progn (vla-put-XScaleFactor o 1.0) (vla-put-YScaleFactor o 1.0) (vla-put-ZScaleFactor o 1.0)))
  (setq n 0)
  (foreach a (vlax-invoke o 'GetAttributes)
    (if (= (strcase (vla-get-TagString a)) "A")
      (progn (vla-put-TextString a "5") (setq n (1+ n)))))
  (vla-Update o)
  (if (= n 1) o nil))

(defun enc:rev5count (lay / ss)
  (setq ss (ssget "_X" (list (cons 8 ENC:LAY) (cons 410 lay))))
  (if ss (sslength ss) 0))

;; run fn on args; returns nil on success, else a short error string
(defun enc:try (fn args label / r)
  (setq r (vl-catch-all-apply fn args))
  (cond ((vl-catch-all-error-p r) (strcat label ":" (vl-catch-all-error-message r)))
        ((null r) (strcat label ":nil"))
        (T nil)))

;; ---------------------------------------------------------------- commands
(defun c:ENC-C1-PLAN (/ pl f parts n)
  (enc:collect)
  (setq pl (enc:plan) parts nil)
  (setq f (open (strcat ENC:DIR "ENC-C1.plan.txt") "w"))
  (foreach lp pl
    (setq n 0)
    (write-line (strcat (car lp) " clusters=" (itoa (length (cadr lp)))) f)
    (foreach b (cadr lp)
      (setq n (1+ n))
      (write-line (strcat "  " (car lp) " cloud" (itoa n) " " (enc:boxstr b)) f))
    (setq parts (cons (strcat (car lp) " clusters=" (itoa (length (cadr lp))) " "
                              (enc:join (mapcar 'enc:boxstr (cadr lp)) " ")) parts)))
  (write-line (strcat "targets_used=" (itoa (length ENC:TG))
                      " skipped=" (enc:join ENC:SKIP ",") " errors=" (enc:join ENC:ERR ",")) f)
  (close f)
  (strcat (enc:join (reverse parts) " | ")
          " | skipped=" (if ENC:SKIP (enc:join ENC:SKIP ",") "none")
          (if ENC:ERR (strcat " | ERR=" (enc:join (enc:first 6 ENC:ERR) ",")) "")))

(defun c:ENC-C1-RUN (/ doc ld pl blk fail nc nt del f hs lays)
  (setq doc (enc:doc))
  (enc:collect)
  (setq ld (tblsearch "LAYER" ENC:LAY))
  (cond
    (ENC:ERR (strcat "REFUSED err=" (enc:join (enc:first 6 ENC:ERR) ",")))
    ((null ld) "REFUSED no layer N_P_REV5")
    ((= 4 (logand 4 (cdr (assoc 70 ld)))) "REFUSED layer N_P_REV5 locked")
    ((null (tblsearch "BLOCK" ENC:BLK)) "REFUSED no block DELTA-TAG")
    ((or (> (enc:rev5count "P1.00") 0) (> (enc:rev5count "P1.01") 0))
     (strcat "REFUSED N_P_REV5 already used P1.00=" (itoa (enc:rev5count "P1.00"))
             " P1.01=" (itoa (enc:rev5count "P1.01"))))
    (T
     (setq pl (enc:plan) ENC:NEW nil fail nil nc 0 nt 0 lays (vla-get-Layouts doc))
     (vla-StartUndoMark doc)
     (foreach lp pl
       (if (not fail)
         (progn
           (setq blk (vl-catch-all-apply 'vla-item (list lays (car lp))))
           (if (not (vl-catch-all-error-p blk))
             (setq blk (vl-catch-all-apply 'vla-get-Block (list blk))))
           (if (vl-catch-all-error-p blk)
             (setq fail (strcat (car lp) ":block"))
             (foreach b (cadr lp)
               (if (not fail)
                 (if (setq fail (enc:try 'enc:mkcloud (list blk b) (strcat (car lp) " cloud")))
                   nil
                   (setq nc (1+ nc))))
               (if (not fail)
                 (if (setq fail (enc:try 'enc:mktag (list blk b) (strcat (car lp) " tag")))
                   nil
                   (setq nt (1+ nt)))))))))
     (if fail
       (progn
         (setq del 0)
         (foreach o ENC:NEW
           (if (not (vl-catch-all-error-p (vl-catch-all-apply 'vla-Delete (list o))))
             (setq del (1+ del))))
         (setq ENC:NEW nil)
         (vla-EndUndoMark doc)
         (strcat "FAILED " fail " rolledback=" (itoa del)))
       (progn
         (vla-EndUndoMark doc)
         (setq hs (mapcar 'vla-get-Handle (reverse ENC:NEW)))
         (setq f (open (strcat ENC:DIR "ENC-C1.created.txt") "w"))
         (foreach h hs (write-line h f))
         (close f)
         (strcat "OK clouds=" (itoa nc) " tags=" (itoa nt)))))))

;; (ok vertex-bbox) for an LWPOLYLINE entget list
(defun enc:cloudok (ed / pts bs a i n p q ok)
  (setq pts nil bs nil)
  (foreach g ed
    (cond ((= (car g) 10) (setq pts (cons (list (cadr g) (caddr g)) pts)))
          ((= (car g) 42) (setq bs (cons (cdr g) bs)))))
  (setq pts (reverse pts) n (length pts) a 0.0 i 0)
  (while (< i n)
    (setq p (nth i pts) q (nth (rem (1+ i) n) pts)
          a (+ a (- (* (car p) (cadr q)) (* (car q) (cadr p)))) i (1+ i)))
  (setq ok (and (> n 2) (= 1 (logand 1 (cdr (assoc 70 ed)))) (> a 0.0)
                (= (length bs) n)))
  (foreach b bs (if (> (abs (- b ENC:BUL)) 1e-5) (setq ok nil)))
  (list ok
        (if pts
          (list (apply 'min (mapcar 'car pts)) (apply 'min (mapcar 'cadr pts))
                (apply 'max (mapcar 'car pts)) (apply 'max (mapcar 'cadr pts)))
          '(0.0 0.0 0.0 0.0))))

;; exactly one ATTRIB, tag A, value "5"
(defun enc:tagok (e / a ed ok n)
  (setq a (entnext e) n 0 ok nil)
  (while (and a (= (cdr (assoc 0 (setq ed (entget a)))) "ATTRIB"))
    (setq n (1+ n))
    (if (and (= (strcase (cdr (assoc 2 ed))) "A") (= (cdr (assoc 1 ed)) "5")) (setq ok T))
    (setq a (entnext a)))
  (and ok (= n 1)))

(defun c:ENC-C1-CHECK (/ parts lay ss i e ed nc nt good boxes cr ins tot nm o)
  (enc:collect)
  (setq parts nil ins 0 tot 0)
  (foreach sp ENC:SPEC
    (setq lay (car sp) nc 0 nt 0 good T boxes nil)
    (setq ss (ssget "_X" (list (cons 8 ENC:LAY) (cons 410 lay))) i 0)
    (if ss
      (while (< i (sslength ss))
        (setq e (ssname ss i) ed (entget e))
        (cond
          ((= (cdr (assoc 0 ed)) "LWPOLYLINE")
           (setq nc (1+ nc) cr (enc:cloudok ed) boxes (cons (cadr cr) boxes))
           (if (not (car cr)) (setq good nil)))
          ((= (cdr (assoc 0 ed)) "INSERT")
           (setq o (vlax-ename->vla-object e)
                 nm (vl-catch-all-apply 'vla-get-EffectiveName (list o)))
           (if (vl-catch-all-error-p nm) (setq nm (cdr (assoc 2 ed))))
           (if (= (strcase nm) ENC:BLK)
             (progn (setq nt (1+ nt)) (if (not (enc:tagok e)) (setq good nil)))
             (setq good nil)))
          (T (setq good nil)))
        (setq i (1+ i))))
    (if (or (= nc 0) (/= nc nt)) (setq good nil))
    (foreach tg ENC:TG
      (if (= (car tg) lay)
        (progn
          (setq tot (1+ tot))
          (if (vl-some '(lambda (b) (enc:in (enc:ctr (caddr tg)) b)) boxes) (setq ins (1+ ins))))))
    (setq parts (cons (strcat lay " clouds=" (itoa nc) " tags=" (itoa nt) (if good " ok" " fail")) parts)))
  (strcat (enc:join (reverse parts) " | ")
          " | targets_inside=" (itoa ins) "/" (itoa tot)
          " skipped=" (itoa (length ENC:SKIP))
          (if ENC:ERR (strcat " ERR=" (enc:join (enc:first 4 ENC:ERR) ",")) "")))

(defun c:ENC-C1-CLEAN (/ f ln hs lst e ed n skip)
  (setq f (open (strcat ENC:DIR "ENC-C1.created.txt") "r") hs nil lst nil n 0 skip 0)
  (if (null f)
    "CLEAN no created.txt"
    (progn
      (while (setq ln (read-line f))
        (if (/= (vl-string-trim " \t" ln) "") (setq hs (cons (vl-string-trim " \t" ln) hs))))
      (close f)
      ;; collect first (only our layer + our entity types)
      (foreach h hs
        (setq e (enc:ent h))
        (if (and e (setq ed (entget e))
                 (= (strcase (cdr (assoc 8 ed))) ENC:LAY)
                 (member (cdr (assoc 0 ed)) '("LWPOLYLINE" "INSERT"))
                 (not (member e lst)))
          (setq lst (cons e lst))
          (setq skip (1+ skip))))
      ;; then delete once each
      (vla-StartUndoMark (enc:doc))
      (foreach e lst (entdel e) (setq n (1+ n)))
      (vla-EndUndoMark (enc:doc))
      (strcat "CLEAN deleted=" (itoa n) " listed=" (itoa (length hs)) " notfound=" (itoa skip)))))

(princ "\nENC-C1 loaded: ENC-C1-PLAN ENC-C1-RUN ENC-C1-CHECK ENC-C1-CLEAN")
(princ)
