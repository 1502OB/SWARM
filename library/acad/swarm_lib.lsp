;;; swarm_lib.lsp - shared AutoLISP helpers for swarm scripts (prefix swl:). Library: shared-helpers.
;;; 2026-11-06, harvested from the r8: helpers of 24-049 C22-R8-T1.v10.lsp (ran and passed verify 11-06), generalised:
;;; no script globals, every setting is a parameter. Tests: test_swarm_lib.lsp (run headless with tools/lsp_dryrun.py).
;;; 2026-11-09 (Mansfield retro, LESSONS A32-A50): swl:swarm-dir, swl:result-out, swl:mtext-box / -bottom, swl:ebox,
;;; swl:group-box, swl:clash-ex, swl:inside-vp-tol, swl:floating-vps, swl:vp-msbox, swl:on-sheet-p.
;;; Load:  (load (strcat (swl:swarm-dir) "library/acad/swarm_lib.lsp"))  before the task script; never redefine swl:*.
;;;        (swl:swarm-dir) = the SWARM_HOME environment variable if set, else %USERPROFILE%/swarm/. Before the first
;;;        load swl:swarm-dir does not exist yet, so build the same path by hand: (getenv "SWARM_HOME"), else
;;;        (strcat (getenv "USERPROFILE") "/swarm/"), + "library/acad/swarm_lib.lsp". No path in this file is hard-coded.
;;; Naming (LESSONS A43): ed* = entget data lists, e* / en* = enames, o* = vla objects; swl:ent returns DATA.
;;; Boxes are (xmin ymin xmax ymax); points (x y [z]). Functions marked [AX] need ActiveX (live AutoCAD only).

(vl-load-com)

;; ---------------------------------------------------------------- basics
(defun swl:doc () (vla-get-ActiveDocument (vlax-get-acad-object)))   ; [AX]
(defun swl:f (x) (rtos x 2 3))
(defun swl:join (lst sep / r)
  (setq r "")
  (foreach s lst (setq r (if (= r "") s (strcat r sep s))))
  r)
(defun swl:ent (h / e) (if (setq e (handent h)) (entget e)))          ; nil for erased handles (entget nil)
(defun swl:p2 (p) (list (car p) (cadr p)))
(defun swl:dxf0 (code ed / v) (if (setq v (cdr (assoc code ed))) v 0))
(defun swl:close (a b tol) (<= (abs (- a b)) tol))
(defun swl:cnt (s) (if s (sslength s) 0))
(defun swl:uniq (l / r) (foreach x l (if (not (member x r)) (setq r (cons x r)))) (reverse r))
;; swarm root folder, forward slashes, trailing "/": SWARM_HOME if set, else %USERPROFILE%/swarm/. Every path the
;; library builds starts here (no hard-coded user folder).
(defun swl:swarm-dir (/ d)
  (setq d (getenv "SWARM_HOME"))
  (if (or (null d) (= d "")) (setq d (strcat (cond ((getenv "USERPROFILE")) ("C:")) "/swarm/")))
  (setq d (vl-string-translate "\\" "/" d))
  (if (/= (substr d (strlen d)) "/") (setq d (strcat d "/")))
  d)

;; ---------------------------------------------------------------- results (one line, no newlines: bridge-safe)
;; replace newline / CR / tab with " | " / "" / " " and double quotes with single quotes
(defun swl:san (s / r i ch)
  (setq r "" i 1 s (if s (vl-princ-to-string s) ""))
  (while (<= i (strlen s))
    (setq ch (substr s i 1))
    (setq r (strcat r (cond ((= ch (chr 10)) " | ") ((= ch (chr 13)) "") ((= ch (chr 9)) " ") ((= ch "\"") "'") (T ch))))
    (setq i (1+ i)))
  r)
;; the result line every swarm script returns: "done=<n> skipped=<n> failed=<n> | ids=<a,b> | <note>"
(defun swl:result (done skipped failed ids note)
  (swl:san (strcat "done=" (itoa done) " skipped=" (itoa skipped) " failed=" (itoa failed)
                   (if ids (strcat " | ids=" (swl:join ids ",")) "")
                   (if (and note (/= note "")) (strcat " | " note) ""))))
;; LESSONS A36: a c: command that only princs gives the writer an EMPTY payload. Every c:<task>-PLAN/-RUN/-CHECK ends
;; with (swl:result-out "<task>" <line>): writes the line (sanitised to one line) to <swarm>/scripts/<task>.result.txt
;; (overwrite) AND returns it. If the file cannot be opened the line is still returned, ending " | result-file=unwritable".
;; (Named -out because swl:result above already builds the line: (swl:result-out "T" (swl:result 1 0 0 ids "ok")).)
(defun swl:result-out (task str / f r)
  (setq r (swl:san str))
  (if (setq f (open (strcat (swl:swarm-dir) "scripts/" (vl-string-translate "/\\:*?<>|\"" "_________" task) ".result.txt") "w"))
    (progn (write-line r f) (close f) r)
    (strcat r " | result-file=unwritable")))

;; ---------------------------------------------------------------- boxes
(defun swl:norm (p q)
  (list (min (car p) (car q)) (min (cadr p) (cadr q)) (max (car p) (car q)) (max (cadr p) (cadr q))))
(defun swl:pad (b d) (list (- (car b) d) (- (cadr b) d) (+ (caddr b) d) (+ (cadddr b) d)))
(defun swl:union (a b)
  (list (min (car a) (car b)) (min (cadr a) (cadr b)) (max (caddr a) (caddr b)) (max (cadddr a) (cadddr b))))
(defun swl:inbox (p b)
  (and (>= (car p) (car b)) (<= (car p) (caddr b)) (>= (cadr p) (cadr b)) (<= (cadr p) (cadddr b))))
(defun swl:ovl (a b)
  (and (< (car a) (caddr b)) (< (car b) (caddr a)) (< (cadr a) (cadddr b)) (< (cadr b) (cadddr a))))
(defun swl:boxstr (b)
  (strcat "[" (swl:f (car b)) "," (swl:f (cadr b)) "," (swl:f (caddr b)) "," (swl:f (cadddr b)) "]"))
(defun swl:vbox (o / mn mx r)                                           ; [AX] bounding box of a vla object
  (setq r (vl-catch-all-apply 'vla-GetBoundingBox (list o 'mn 'mx)))
  (if (and (not (vl-catch-all-error-p r)) mn mx)
    (progn (setq mn (vlax-safearray->list mn) mx (vlax-safearray->list mx))
           (swl:norm mn mx))))
;; vertex bbox of a polyline / any entity with group 10 points (no arc bulge: LESSONS A20)
(defun swl:vtxbox (ed / xs ys)
  (foreach g ed (if (= (car g) 10) (setq xs (cons (cadr g) xs) ys (cons (caddr g) ys))))
  (if xs (list (apply 'min xs) (apply 'min ys) (apply 'max xs) (apply 'max ys))))
;; x (ename, vla object or handle string) -> ename, or nil
(defun swl:->ename (x)
  (cond ((= (type x) 'ENAME) x)
        ((= (type x) 'STR) (handent x))
        ((= (type x) 'VLA-OBJECT) (vlax-vla-object->ename x))))
;; x -> vla object, or nil when there is none (erased, or no ActiveX: headless Core Console)
(defun swl:->obj (x / o)
  (cond ((= (type x) 'VLA-OBJECT) x)
        ((setq x (swl:->ename x))
         (setq o (vl-catch-all-apply 'vlax-ename->vla-object (list x)))
         (if (and o (not (vl-catch-all-error-p o))) o))))
;; the A34 rule on a raw box: bottom = bbox top - max(bbox height, DXF 43). Pure LISP.
(defun swl:mtext-rule (bb h43)
  (list (car bb) (- (cadddr bb) (max (- (cadddr bb) (cadr bb)) (if (numberp h43) h43 0.0))) (caddr bb) (cadddr bb)))
;; DXF-only MTEXT box (no ActiveX): insertion 10, attachment 71, width 42 (else 41), height 43; rotation ignored.
;; DXF 43 can be stale (A34): this is the fallback when ActiveX is missing (headless tests), never preferred live.
(defun swl:mtext-dxfbox (ed / p a w h x y)
  (setq p (cdr (assoc 10 ed)) a (swl:dxf0 71 ed) w (swl:dxf0 42 ed) h (swl:dxf0 43 ed))
  (if (<= w 0.0) (setq w (swl:dxf0 41 ed)))
  (if (< a 1) (setq a 1))
  (setq x (- (car p) (* w 0.5 (rem (1- a) 3)))
        y (+ (cadr p) (* h 0.5 (/ (1- a) 3))))                         ; y = top of the text
  (list x (- y h) (+ x w) y))
;; MTEXT box by the A34 rule (LESSONS A29/A34, the M18-R4-T1 m4:mbox rule): vla bbox x-range and top, bottom =
;; top - max(bbox height, DXF 43). The ONLY allowed MText measure; every task touching the same note uses it.
;; x = ename, vla object or handle. Without ActiveX falls back to swl:mtext-dxfbox. nil if x is not an MTEXT.
(defun swl:mtext-box (x / e ed o bb)
  (setq e (swl:->ename x) ed (if e (entget e)))
  (if (and ed (= (cdr (assoc 0 ed)) "MTEXT"))
    (progn
      (setq o (swl:->obj e) bb (if o (swl:vbox o)))
      (swl:mtext-rule (if bb bb (swl:mtext-dxfbox ed)) (swl:dxf0 43 ed)))))
(defun swl:mtext-bottom (x / b) (if (setq b (swl:mtext-box x)) (cadr b)))
;; box of one entity (ename, vla object or handle), the same way for PLAN and CHECK: MTEXT by swl:mtext-box; others
;; by the vla bbox when ActiveX is there, else from DXF: LINE ends, LWPOLYLINE vertices padded by the largest arc
;; sagitta (|bulge| * half chord), CIRCLE centre +- r, TEXT by textbox (rotation 0 only), anything else its group
;; 10 point(s) (an INSERT gives only its insertion point headless). nil if the entity is missing.
(defun swl:ebox (x / e ed o bb typ pts sg n i p q s r tb)
  (setq e (swl:->ename x) ed (if e (entget e)))
  (if ed
    (progn
      (setq typ (cdr (assoc 0 ed)))
      (cond
        ((= typ "MTEXT") (swl:mtext-box e))
        ((and (setq o (swl:->obj e)) (setq bb (swl:vbox o))) bb)
        ((= typ "LINE") (swl:norm (cdr (assoc 10 ed)) (cdr (assoc 11 ed))))
        ((= typ "LWPOLYLINE")
         (foreach g ed
           (cond ((= (car g) 10) (setq pts (cons (list (cadr g) (caddr g)) pts)))
                 ((= (car g) 42) (setq sg (cons (cdr g) sg)))))
         (setq pts (reverse pts) sg (reverse sg) n (length pts) i 0 s 0.0)
         (while (< i n)
           (setq p (nth i pts) q (nth (rem (1+ i) n) pts))
           (if (and (nth i sg) (or (< i (1- n)) (= 1 (logand 1 (swl:dxf0 70 ed)))))
             (setq s (max s (* (abs (nth i sg)) 0.5 (distance p q)))))
           (setq i (1+ i)))
         (if (setq bb (swl:vtxbox ed)) (swl:pad bb s)))
        ((= typ "CIRCLE")
         (setq p (cdr (assoc 10 ed)) r (cdr (assoc 40 ed)))
         (list (- (car p) r) (- (cadr p) r) (+ (car p) r) (+ (cadr p) r)))
        ((and (= typ "TEXT") (setq tb (textbox ed)))
         (setq p (cdr (assoc 10 ed)))
         (swl:norm (list (+ (car p) (car (car tb))) (+ (cadr p) (cadr (car tb))))
                   (list (+ (car p) (car (cadr tb))) (+ (cadr p) (cadr (cadr tb))))))
        (T (swl:vtxbox ed))))))
;; LESSONS A32: one box for a whole note group (note + tag + cloud ...), called by BOTH the PLAN and the CHECK of a
;; script so they test the same geometry. handles: list of handle strings (enames / vla objects also accepted).
;; Returns the union box, or nil if any member is missing or has no box (the caller ABORTs: the group changed).
(defun swl:group-box (handles / r b bad)
  (foreach h handles
    (if (setq b (swl:ebox h)) (setq r (if r (swl:union r b) b)) (setq bad T)))
  (if (and r (not bad)) r))

;; ---------------------------------------------------------------- clash checks (crossing-aware)
;; segment p-q (2D) touches box b (an endpoint inside, or it crosses an edge)
(defun swl:seghit (p q b / c1 c2 c3 c4)
  (or (swl:inbox p b) (swl:inbox q b)
      (progn
        (setq c1 (list (car b) (cadr b)) c2 (list (caddr b) (cadr b))
              c3 (list (caddr b) (cadddr b)) c4 (list (car b) (cadddr b)))
        (or (inters p q c1 c2 T) (inters p q c2 c3 T) (inters p q c3 c4 T) (inters p q c4 c1 T)))))
(defun swl:tst (op code x y) (list (cons -4 op) (list code x y 0.0)))
;; ssget filter: LINEs in space (layout name, "Model") whose x-span overlaps [bx1,bx2] and y-span overlaps [by1,by2]
;; (bbox overlap; swl:seghit then keeps only real crossings). 2026-11-09: was "x-span covers [bx1,bx2]", which missed
;; a line crossing the box top-to-bottom or cutting a corner with no endpoint inside (found by test clash-sees-frame).
(defun swl:spanflt (space bx1 by1 bx2 by2 / r)
  (setq r (list '(0 . "LINE") (cons 410 space) (cons -4 "<or")))
  (foreach xc (list (list 10 11) (list 11 10))
    (foreach yc (list (list 10 11) (list 11 10))
      (setq r (append r (list (cons -4 "<and"))
                (swl:tst "<=,*,*" (car xc) bx2 0.0) (swl:tst ">=,*,*" (cadr xc) bx1 0.0)
                (swl:tst "*,<=,*" (car yc) 0.0 by2) (swl:tst "*,>=,*" (cadr yc) 0.0 by1)
                (list (cons -4 "and>"))))))
  (append r (list (cons -4 "or>"))))
;; handles of entities in space overlapping box b: LINEs with an endpoint in b+pad, LINEs crossing b, LWPOLYLINE
;; segments, other entities by bounding box [AX for those]. skip = handles to ignore. cap = max entities examined;
;; returns (list "ABORT too-many") above cap (never scan the whole drawing: LESSONS).
(defun swl:clash (space b skip pad cap / hits ss i e ed h bb pts n j x1 y1 x2 y2 stop sA sB sC sD)
  (setq hits nil x1 (- (car b) pad) y1 (- (cadr b) pad) x2 (+ (caddr b) pad) y2 (+ (cadddr b) pad))
  (setq sA (ssget "_X" (list (cons 0 "LINE") (cons 410 space)
                             (cons -4 "<or")
                             (cons -4 "<and") (cons -4 ">=,>=,*") (list 10 x1 y1 0.0) (cons -4 "<=,<=,*") (list 10 x2 y2 0.0) (cons -4 "and>")
                             (cons -4 "<and") (cons -4 ">=,>=,*") (list 11 x1 y1 0.0) (cons -4 "<=,<=,*") (list 11 x2 y2 0.0) (cons -4 "and>")
                             (cons -4 "or>"))))
  (setq sD (ssget "_X" (swl:spanflt space (car b) (cadr b) (caddr b) (cadddr b))))
  (setq sB (ssget "_X" (list (cons 0 "LWPOLYLINE") (cons 410 space)
                             (cons -4 ">=,>=,*") (list 10 x1 y1 0.0) (cons -4 "<=,<=,*") (list 10 x2 y2 0.0))))
  (setq sC (ssget "_X" (list (cons 410 space)
                             (cons -4 "<not") (cons 0 "LINE,LWPOLYLINE,VIEWPORT") (cons -4 "not>")
                             (cons -4 ">=,>=,*") (list 10 x1 y1 0.0) (cons -4 "<=,<=,*") (list 10 x2 y2 0.0))))
  (if (> (+ (swl:cnt sA) (swl:cnt sB) (swl:cnt sC) (swl:cnt sD)) cap)
    (list "ABORT too-many")
    (progn
      (foreach ss (list sA sD sB)
        (if ss
          (progn
            (setq i 0)
            (while (< i (sslength ss))
              (setq e (ssname ss i) ed (entget e) h (cdr (assoc 5 ed)))
              (if (not (member h skip))
                (if (= (cdr (assoc 0 ed)) "LINE")
                  (if (swl:seghit (swl:p2 (cdr (assoc 10 ed))) (swl:p2 (cdr (assoc 11 ed))) b)
                    (setq hits (cons h hits)))
                  (progn
                    (setq pts nil)
                    (foreach g ed (if (= (car g) 10) (setq pts (cons (list (cadr g) (caddr g)) pts))))
                    (setq pts (reverse pts) n (length pts) j 0 stop nil)
                    (while (and (not stop) (< j (if (= 1 (logand 1 (cdr (assoc 70 ed)))) n (1- n))))
                      (if (swl:seghit (nth j pts) (nth (rem (1+ j) n) pts) b)
                        (progn (setq hits (cons h hits)) (setq stop T)))
                      (setq j (1+ j))))))
              (setq i (1+ i))))))
      (if sC
        (progn
          (setq i 0)
          (while (< i (sslength sC))
            (setq e (ssname sC i) h (cdr (assoc 5 (entget e))))
            (if (not (member h skip))
              (if (and (setq bb (swl:vbox (vlax-ename->vla-object e))) (swl:ovl bb b))
                (setq hits (cons h hits))))
            (setq i (1+ i)))))
      (swl:uniq (reverse hits)))))
;; LESSONS A35: swl:clash, but hits on any layer in exclude-layers (list of layer names, case-insensitive exact match,
;; e.g. (list "FRAME")) do not count. Same return as swl:clash (handles, or (list "ABORT too-many")). Use it ONLY for
;; cloud edges placed ON a border line; placing NEW text uses plain swl:clash (FRAME stays an obstacle).
(defun swl:clash-ex (space b skip pad cap exclude-layers / hits ex r ed)
  (setq hits (swl:clash space b skip pad cap) ex (mapcar 'strcase exclude-layers))
  (if (and hits (= (car hits) "ABORT too-many"))
    hits
    (progn
      (foreach h hits
        (setq ed (swl:ent h))
        (if (not (and ed (member (strcase (cdr (assoc 8 ed))) ex))) (setq r (cons h r))))
      (reverse r))))

;; ---------------------------------------------------------------- tag / text spot scoring
;; cands: list of candidate boxes in preference order; clashfn: (lambda (box) -> list of hits).
;; Returns (box . nhits) of the first candidate with no hits, else of the candidate with the fewest hits.
(defun swl:best-spot (cands clashfn / best n c)
  (foreach c cands
    (if (or (null best) (> (cdr best) 0))
      (progn
        (setq n (length (apply clashfn (list c))))
        (if (or (null best) (< n (cdr best))) (setq best (cons c n))))))
  best)

;; ---------------------------------------------------------------- viewport projection (model <-> paper)
;; vp record from a VIEWPORT handle on layout lo: (pc tgt vc k box) or an error string. Refuses twisted viewports.
(defun swl:vp (h lo / ed pc tg vc vh ph w)
  (setq ed (swl:ent h))
  (cond
    ((null ed) (strcat "viewport " h " missing"))
    ((/= (cdr (assoc 0 ed)) "VIEWPORT") (strcat h " is not a viewport"))
    ((/= (strcase (cdr (assoc 410 ed))) (strcase lo)) (strcat "viewport " h " not on layout " lo))
    ((and (assoc 51 ed) (> (abs (cdr (assoc 51 ed))) 1e-6)) (strcat "viewport " h " has twist"))
    (T
     (setq pc (cdr (assoc 10 ed)) vc (cdr (assoc 12 ed)) vh (cdr (assoc 45 ed))
           ph (cdr (assoc 41 ed)) w (cdr (assoc 40 ed))
           tg (if (assoc 17 ed) (cdr (assoc 17 ed)) (list 0.0 0.0 0.0)))
     (list pc tg vc (/ ph vh)
           (list (- (car pc) (/ w 2.0)) (- (cadr pc) (/ ph 2.0)) (+ (car pc) (/ w 2.0)) (+ (cadr pc) (/ ph 2.0)))))))
(defun swl:ms->ps (vp p / pc tg vc k)
  (setq pc (car vp) tg (cadr vp) vc (caddr vp) k (cadddr vp))
  (list (+ (car pc) (* k (- (car p) (car tg) (car vc)))) (+ (cadr pc) (* k (- (cadr p) (cadr tg) (cadr vc))))))
(defun swl:ps->ms (vp p / pc tg vc k)
  (setq pc (car vp) tg (cadr vp) vc (caddr vp) k (cadddr vp))
  (list (+ (car tg) (car vc) (/ (- (car p) (car pc)) k)) (+ (cadr tg) (cadr vc) (/ (- (cadr p) (cadr pc)) k))))
(defun swl:box->ms (vp b) (swl:norm (swl:ps->ms vp (list (car b) (cadr b))) (swl:ps->ms vp (list (caddr b) (cadddr b)))))
(defun swl:inside-vp (vp b / r)
  (setq r (nth 4 vp))
  (and (swl:inbox (list (car b) (cadr b)) r) (swl:inbox (list (caddr b) (cadddr b)) r)))
;; LESSONS A33: containment with a tolerance, for geometry that is NOT being moved: box b (paper units) shrunk by tol
;; on every side (nil = 0.10) must lie inside the viewport. A box smaller than 2*tol shrinks to its centre point.
(defun swl:inside-vp-tol (vp b tol / cx cy)
  (if (null tol) (setq tol 0.10))
  (setq cx (/ (+ (car b) (caddr b)) 2.0) cy (/ (+ (cadr b) (cadddr b)) 2.0))
  (swl:inside-vp vp (list (min cx (+ (car b) tol)) (min cy (+ (cadr b) tol))
                          (max cx (- (caddr b) tol)) (max cy (- (cadddr b) tol)))))
;; LESSONS A39/A50: enames of the floating viewports (DXF 69 > 1; 69 = 1 is the paper-space pseudo viewport) on
;; layout lo. Pure DXF ("_X" + group 410), so it works headless (no layoutlist / ActiveX). nil = the layout never
;; shows model space: report floating_viewports=0 and treat its model-space targets as off-sheet.
(defun swl:floating-vps (lo / ss i e r)
  (setq ss (ssget "_X" (list '(0 . "VIEWPORT") (cons 410 lo))) i 0)
  (while (and ss (< i (sslength ss)))
    (setq e (ssname ss i))
    (if (> (swl:dxf0 69 (entget e)) 1) (setq r (cons e r)))
    (setq i (1+ i)))
  (reverse r))
;; model-space window a VIEWPORT shows, from its entget data (centre = target 17 + view centre 12, height 45,
;; width 40 * 45 / 41). Twisted (51): the bounding box of the rotated window (a little more: the safe side for
;; on-sheet tests). A non-rectangular clip (340) counts as its rectangle. nil if the data is incomplete.
(defun swl:vp-msbox (ed / tg vc vh w ph hw hh tw c s ex ey)
  (setq tg (if (assoc 17 ed) (cdr (assoc 17 ed)) (list 0.0 0.0 0.0)) vc (cdr (assoc 12 ed))
        vh (cdr (assoc 45 ed)) w (cdr (assoc 40 ed)) ph (cdr (assoc 41 ed)))
  (if (and vc vh w ph (> ph 0.0))
    (progn
      (setq hh (/ vh 2.0) hw (/ (* w vh) (* 2.0 ph)) tw (swl:dxf0 51 ed) c (abs (cos tw)) s (abs (sin tw))
            ex (+ (* hw c) (* hh s)) ey (+ (* hw s) (* hh c)))
      (list (- (+ (car tg) (car vc)) ex) (- (+ (cadr tg) (cadr vc)) ey)
            (+ (car tg) (car vc) ex) (+ (cadr tg) (cadr vc) ey)))))
;; T if any floating viewport on any paper layout shows (overlaps) the model-space box msbox; nil = off-sheet: the
;; target never plots, report "off-sheet" instead of changing it (A39/A50). Works headless.
(defun swl:on-sheet-p (msbox / ss i ed vb hit)
  (setq ss (ssget "_X" '((0 . "VIEWPORT"))) i 0)
  (while (and ss (not hit) (< i (sslength ss)))
    (setq ed (entget (ssname ss i)))
    (if (and (/= (strcase (cdr (assoc 410 ed))) "MODEL") (> (swl:dxf0 69 ed) 1)
             (setq vb (swl:vp-msbox ed)) (swl:ovl vb msbox))
      (setq hit T))
    (setq i (1+ i)))
  hit)

;; ---------------------------------------------------------------- revision clouds (clockwise, vertex-sized)
(defun swl:side (p q chord / L n i r f)
  (setq L (distance p q) n (max 1 (fix (+ 0.5 (/ L chord)))) i 0 r nil)
  (while (< i n)
    (setq f (/ (float i) n))
    (setq r (cons (list (+ (car p) (* f (- (car q) (car p)))) (+ (cadr p) (* f (- (cadr q) (cadr p))))) r))
    (setq i (1+ i)))
  (reverse r))
;; cloud vertices around box b (already padded by the caller with swl:pad), clockwise BL > TL > TR > BR
(defun swl:cloudpts (b chord / bl br tr tl)
  (setq bl (list (car b) (cadr b)) br (list (caddr b) (cadr b))
        tr (list (caddr b) (cadddr b)) tl (list (car b) (cadddr b)))
  (append (swl:side bl tl chord) (swl:side tl tr chord) (swl:side tr br chord) (swl:side br bl chord)))
;; check an LWPOLYLINE cloud: closed, clockwise (signed area < 0), every bulge = bulge -> (ok vertexbox nvertices)
(defun swl:cloud-check (ed bulge / pts bs a i n p q ok)
  (foreach g ed
    (cond ((= (car g) 10) (setq pts (cons (list (cadr g) (caddr g)) pts)))
          ((= (car g) 42) (setq bs (cons (cdr g) bs)))))
  (setq pts (reverse pts) n (length pts) a 0.0 i 0)
  (while (< i n)
    (setq p (nth i pts) q (nth (rem (1+ i) n) pts)
          a (+ a (- (* (car p) (cadr q)) (* (car q) (cadr p)))) i (1+ i)))
  (setq ok (and (> n 2) (= 1 (logand 1 (cdr (assoc 70 ed)))) (< a 0.0) (= (length bs) n)))
  (foreach b bs (if (> (abs (- b bulge)) 1e-5) (setq ok nil)))
  (list ok (if pts (swl:vtxbox ed) '(0.0 0.0 0.0 0.0)) n))
;; entmake a closed cloud LWPOLYLINE on layer lay in space (layout name or "Model"), returns its ename or nil
(defun swl:entmake-cloud (b chord bulge lay space / pts dl)
  (setq pts (swl:cloudpts b chord))
  (setq dl (list '(0 . "LWPOLYLINE") '(100 . "AcDbEntity") (cons 410 space) (cons 8 lay) '(100 . "AcDbPolyline")
                 (cons 90 (length pts)) '(70 . 1)))
  (foreach p pts (setq dl (append dl (list (cons 10 p) (cons 42 bulge)))))
  (if (entmake dl) (entlast)))

;; ---------------------------------------------------------------- live-value preflight
;; checks: list of (handle dxf-code expected tolerance); tolerance nil = exact. Returns nil if all match, else the
;; first problem as a one-line string (the script must ABORT on it: the drawing changed since planning).
(defun swl:preflight (checks / ed live bad)
  (foreach c checks
    (if (null bad)
      (progn
        (setq ed (swl:ent (car c)) live (if ed (cdr (assoc (cadr c) ed))))
        (cond
          ((null ed) (setq bad (strcat (car c) " missing")))
          ((and (numberp (caddr c)) (numberp live) (cadddr c))
           (if (not (swl:close live (caddr c) (cadddr c)))
             (setq bad (strcat (car c) " group " (itoa (cadr c)) " is " (swl:f live) ", planned " (swl:f (caddr c))))))
          ((and (listp (caddr c)) (listp live) (cadddr c))
           (if (not (and (swl:close (car live) (car (caddr c)) (cadddr c)) (swl:close (cadr live) (cadr (caddr c)) (cadddr c))))
             (setq bad (strcat (car c) " group " (itoa (cadr c)) " moved"))))
          ((not (equal live (caddr c)))
           (setq bad (strcat (car c) " group " (itoa (cadr c)) " is " (swl:san live) ", planned " (swl:san (caddr c)))))))))
  bad)

;; ---------------------------------------------------------------- queued commands (attach / xref / publish)
;; (command) cannot attach PDFs or xrefs inside the dispatcher; queue them instead. Only these may be queued
;; (writer_guard enforces the same list). Returns "queued <cmd>" or "ABORT ..." (never sends a disallowed command).
(setq swl:*queue-ok* (list "XREF" "PDFATTACH" "IMAGE" "IMAGEATTACH" "PUBLISH" "REGENALL"))
(defun swl:queue-command (cmd / name)
  (setq name (strcase (vl-string-left-trim "_.-" (car (swl:words cmd)))))
  (cond
    ((not (member name swl:*queue-ok*)) (strcat "ABORT not a queueable command: " (swl:san cmd)))
    (T (vla-SendCommand (swl:doc) (strcat cmd (chr 13))) (strcat "queued " (swl:san cmd)))))
(defun swl:words (s / r w i ch)
  (setq r nil w "" i 1)
  (while (<= i (strlen s))
    (setq ch (substr s i 1))
    (if (= ch " ") (progn (if (/= w "") (setq r (cons w r))) (setq w "")) (setq w (strcat w ch)))
    (setq i (1+ i)))
  (if (/= w "") (setq r (cons w r)))
  (if r (reverse r) (list "")))

(princ)
