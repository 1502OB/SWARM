;;; swarm_publish.lsp - publish one prepared DSD (tools/acad_dsd.py) to PDF without dialogs. Library: publish-sheet.
;;; Generalised from 22-001 pc9-check/cad/pc9publish.lsp (LESSONS A6/A23). Changes nothing in the drawing.
;;; acad-writer: (load "C:/Users/Jeremy/swarm/tools/swarm_publish.lsp") then, in a separate short call,
;;;   (swarm:publish "C:/.../<task>.dsd" "C:/.../<task>.pdf")
;;; Runs -PUBLISH in the foreground, restores BACKGROUNDPLOT/FILEDIA/CMDDIA, never overwrites the PDF,
;;; and returns a short status: "ran | output exists <bytes>" or "ABORT ..." / "ERROR ...".
(defun swarm:publish (dsd out / bg fd cd r)
  (setq bg (getvar "BACKGROUNDPLOT") fd (getvar "FILEDIA") cd (getvar "CMDDIA"))
  (cond
    ((not (findfile dsd)) (setq r "ABORT dsd not found"))
    ((findfile out) (setq r "ABORT output already exists - not overwriting"))
    (t (setvar "BACKGROUNDPLOT" 0) (setvar "FILEDIA" 0) (setvar "CMDDIA" 0)
       (setq r (vl-catch-all-apply
                 '(lambda () (command "_.-PUBLISH" dsd) (while (> (getvar "CMDACTIVE") 0) (command "")))))
       (setvar "BACKGROUNDPLOT" bg) (setvar "FILEDIA" fd) (setvar "CMDDIA" cd)
       (setq r (if (vl-catch-all-error-p r) (strcat "ERROR " (vl-catch-all-error-message r)) "ran"))
       (setq r (strcat r (if (findfile out)
                            (strcat " | output exists " (itoa (vl-file-size out)))
                            " | NO output file")))))
  r)
(princ)
