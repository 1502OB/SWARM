# Core AutoLISP / ActiveX names used by our swarm scripts

Generated 2026-11-06 by scripts/gen_core.py from 138 .lsp files under C:\Users\Jeremy\swarm (calls counted in function position; drawing / ActiveX / file API only, list and math primitives left out). Look here first; everything else: the checker, activex-INDEX.md, autolisp-functions.md, dxf-entities.md. Notes are facts that cost failed runs or were verified in the 2027 type library.

| Name | Calls | Files | Signature (2027) | Note |
|---|---|---|---|---|
| vl-catch-all-apply | 608 | 76 | `(vl-catch-all-apply 'function<symbol> list<list>)` | Second argument must be a LIST of arguments; test the result with vl-catch-all-error-p. |
| entget | 546 | 65 | `(entget ename<ename> applist?<list>)` | Returns the entity's DXF list (the help data's return type 'Ename' is wrong). Group codes: dxf-entities.md. |
| vl-catch-all-error-p | 465 | 75 | `(vl-catch-all-error-p arg<any>)` |  |
| vlax-3D-point | 384 | 60 | `(vlax-3D-point vlax-3D-point x<integer/real> y<integer/real> z?<integer/real>)` |  |
| vl-catch-all-error-message | 284 | 52 | `(vl-catch-all-error-message error-obj<catch-all-apply-error>)` |  |
| ssget | 255 | 47 | `(ssget sel-method?<string> pt1?<list> pt2?<list> pt-list?<list> filter-list?<list>)` | "_X" with a filter list reads the whole drawing (no pick); (410 . "Model") / layout name filters by space. |
| sslength | 250 | 47 | `(sslength ss<pickset>)` |  |
| getvar | 219 | 53 | `(getvar varname<string>)` | Restore every sysvar you change (OSMODE, FILEDIA, CMDDIA, BACKGROUNDPLOT, CANNOTATIONSCALE) (acad-conventions). |
| vlax-safearray->list | 212 | 73 | `(vlax-safearray->list var<safearray>)` |  |
| vla-get-Handle | 211 | 62 | `vla-get-Handle on Object` | Declared on Object (every entity and table record): READ-ONLY. |
| vla-put-Layer | 206 | 44 | `vla-put-Layer on Entity, Group` | Declared on Entity. The layer must exist (vla-Add on Layers first). |
| handent | 198 | 47 | `(handent handle<string>)` |  |
| write-line | 192 | 61 | `(write-line str<string> file-desc?<file>)` |  |
| ssname | 188 | 43 | `(ssname ss<pickset> index<integer/real>)` |  |
| open | 185 | 61 | `(open filename<string> mode<string> encoding?<string>)` |  |
| close | 185 | 61 | `(close file-desc<file>)` |  |
| vlax-ename->vla-object | 178 | 42 | `(vlax-ename->vla-object entname<ename>)` | ename -> VLA object; vlax-vla-object->ename back. |
| vla-HandleToObject | 164 | 17 | `(vla-HandleToObject obj Handle<BSTR>) -> IDispatch` |  |
| vl-string-search | 147 | 54 | `(vl-string-search pattern<string> str<string> start-pos?<integer>)` | Case-sensitive, 0-based, nil when absent. |
| inters | 116 | 29 | `(inters pt1<list> pt2<list> pt3<list> pt4<list> onseg?<list>)` |  |
| vla-Update | 112 | 27 | `(vla-Update obj)` | Declared on Entity, Application, Group, SelectionSet: regenerates the object's display. |
| tblsearch | 110 | 33 | `(tblsearch table-name<string> symbol<string> setnext?<boolean>)` | Returns the table entry DXF list or nil; LAYER 70 flags and negative 62 = off (dxf-entities.md). |
| vla-EndUndoMark | 110 | 37 | `(vla-EndUndoMark obj)` | On Document; see vla-StartUndoMark. |
| vlax-get-acad-object | 103 | 88 | `(vlax-get-acad-object)` | Application object; needs (vl-load-com) once per session. |
| vla-Move | 96 | 39 | `(vla-Move obj FromPoint<VARIANT> ToPoint<VARIANT>)` | Declared on Entity: (vla-Move o (vlax-3d-point from) (vlax-3d-point to)). |
| vlax-for | 93 | 41 | `(vlax-for symbol<vla-object> collection<vla-object> expression?<many>...)` |  |
| vlax-variant-value | 93 | 26 | `(vlax-variant-value var<variant>)` |  |
| vlax-vla-object->ename | 89 | 39 | `(vlax-vla-object->ename obj<vla-object>)` |  |
| vla-Copy | 85 | 33 | `(vla-Copy obj) -> IDispatch` | Declared on Entity: returns the copy in the same space; move it with vla-Move (variant 3D points). |
| vla-get-ObjectName | 85 | 32 | `vla-get-ObjectName on Object, SubEntity` |  |
| vla-Delete | 81 | 12 | `(vla-Delete obj)` |  |
| vla-get-Name | 81 | 34 | `vla-get-Name on Application, Block, BlockReference, Dictionary` |  |
| vlax-make-safearray | 79 | 39 | `(vlax-make-safearray type<integer> '(l-bound<integer> . u-bound<integer>) '(l-bound?<integer> . u-bound?<integer>)...)` | Then vlax-safearray-fill and vlax-make-variant to pass coordinates; or use vlax-3d-point for one point. |
| vlax-safearray-fill | 78 | 38 | `(vlax-safearray-fill var<safearray> 'element-values<list>)` |  |
| vla-put-TextString | 78 | 35 | `vla-put-TextString on Attribute, AttributeReference, GeoPositionMarker, MLeader` |  |
| setvar | 75 | 14 | `(setvar varname<string> value<many>)` | See getvar: restore what you change, also in *error*. |
| vla-StartUndoMark | 75 | 37 | `(vla-StartUndoMark obj)` | On Document: pair with vla-EndUndoMark in every exit path (acad-conventions: one UNDO group per RUN). |
| vla-GetLeaderLineVertices | 75 | 10 | `(vla-GetLeaderLineVertices obj leaderLineIndex<long>) -> VARIANT` |  |
| vla-get-ActiveDocument | 71 | 61 | `vla-get-ActiveDocument on Application` | On Application. Target documents by name and make sure the target is active before ActiveX work (scripting-lessons). |
| vla-Item | 67 | 46 | `(vla-Item obj Index<VARIANT>) -> IAcadEntity` | Raises an error on a missing key: wrap in vl-catch-all-apply ('vla-item (list coll key)) and test vl-catch-all-error-p. |
| vla-AddLine | 66 | 7 | `(vla-AddLine obj StartPoint<VARIANT> EndPoint<VARIANT>) -> IAcadLine` |  |
| vla-get-Layouts | 65 | 51 | `vla-get-Layouts on Database` | On Document. Layout objects; a layout's Block is its paper space (vla-get-Block). |
| vla-get-TextString | 62 | 17 | `vla-get-TextString on Attribute, AttributeReference, GeoPositionMarker, MLeader` |  |
| vl-remove-if-not | 60 | 34 | `(vl-remove-if-not predicate-function<subroutine/symbol> lst<list>)` |  |
| vla-get-Layer | 60 | 27 | `vla-get-Layer on Entity, SubEntity` |  |
| vl-load-com | 57 | 50 | `(vl-load-com)` |  |
| vl-string-trim | 56 | 33 | `(vl-string-trim char-set<string> str<string>)` |  |
| vl-princ-to-string | 55 | 31 | `(vl-princ-to-string data<any>)` |  |
| distance | 55 | 41 | `(distance pt1<list> pt2<list>)` |  |
| vla-put-Closed | 54 | 29 | `vla-put-Closed on 3DPolyline, LWPolyline, LoftedSurface, Polyline` | On LWPolyline, Polyline, 3DPolyline, Spline, LoftedSurface; value :vlax-true / :vlax-false. |
| vla-get-Block | 49 | 45 | `vla-get-Block on Layout, MLeaderStyle` |  |
| entdel | 49 | 35 | `(entdel ename<ename>)` | entdel on an already-deleted ename UNDELETES it: entdel only enames whose entget is non-nil (scripting-lessons). |
| read-line | 48 | 40 | `(read-line file-desc?<file>)` |  |
| vlax-get | 48 | 6 | `(undocumented)` | Undocumented but works here (LISP values). Documented form: vlax-get-property. |
| vl-some | 41 | 16 | `(vl-some predicate-function<subroutine/symbol> lst<list> lst?<list>...)` |  |
| vla-SetLeaderLineVertices | 41 | 7 | `(vla-SetLeaderLineVertices obj leaderLineIndex<long> pointArray<VARIANT>)` |  |
| entupd | 35 | 16 | `(entupd ename<ename>)` |  |
| entmod | 34 | 19 | `(entmod elist<list>)` | Cannot change the type, handle 5, internal fields (-1/-2) or any VIEWPORT entity; referenced styles/blocks must exist (a new layer name is created); after a subentity change call (entupd main-ename) (dxf-entities.md notes). |
| vla-get-Documents | 32 | 30 | `vla-get-Documents on Application` |  |
| vla-AddLightWeightPolyline | 31 | 29 | `(vla-AddLightWeightPolyline obj VerticesList<VARIANT>) -> IAcadLWPolyline` | Declared on Block (so ModelSpace, PaperSpace, any block def): VerticesList = variant of a double safearray x1 y1 x2 y2 ... (2D, no z). |
| vla-SetBulge | 31 | 29 | `(vla-SetBulge obj Index<long> bulge<double>)` | LWPolyline/Polyline: (vla-SetBulge pl index bulge), vertex index from 0. Sagitta = |bulge| x chord / 2 (clouds). |
| entnext | 31 | 6 | `(entnext ename?<ename>)` |  |
| command | 29 | 12 | `(command arguments?<many>...)` | OSMODE is 16383 here: pass points with "_non" or build with entmake/ActiveX (scripting-lessons). Use "_." English command names. |
| findfile | 28 | 13 | `(findfile filename<string>)` |  |
| vla-get-ModelSpace | 28 | 23 | `vla-get-ModelSpace on Database` |  |
| vla-put-StyleName | 27 | 25 | `vla-put-StyleName on Attribute, AttributeReference, Dimension, Leader` |  |
| vla-put-Height | 27 | 26 | `vla-put-Height on Application, Attribute, AttributeReference, Document` |  |
| vla-get-InsertionPoint | 26 | 13 | `vla-get-InsertionPoint on Attribute, AttributeReference, BlockReference, MText` |  |
| vlax-make-variant | 25 | 24 | `(vlax-make-variant value?<any> type?<integer>)` |  |
| vla-put-ConstantWidth | 25 | 24 | `vla-put-ConstantWidth on LWPolyline, Polyline` |  |
| entmake | 24 | 12 | `(entmake elist?<list>)` | Group 0 first (or second after -1); R13+ types (LWPOLYLINE, MTEXT, ...) need (100 . "AcDbEntity") + their subclass marker; cannot make VIEWPORTs; returns nil on failure: re-query, never assume (dxf-entities.md notes, acad-conventions). |
| vla-AddText | 22 | 22 | `(vla-AddText obj TextString<BSTR> InsertionPoint<VARIANT> Height<double>) -> IAcadText` | Declared on Block: (vla-AddText space str (vlax-3d-point x y 0) height). Insertion point is a variant 3D point. |
| entlast | 22 | 14 | `(entlast)` |  |
| vla-get-Layers | 20 | 11 | `vla-get-Layers on Database` |  |
| vl-remove | 18 | 12 | `(vl-remove element-to-remove<any> lst<list>)` |  |
| vla-put-EndPoint | 18 | 5 | `vla-put-EndPoint on Line` |  |
| vla-get-Blocks | 16 | 9 | `vla-get-Blocks on Database` |  |
| vla-GetBoundingBox | 15 | 12 | `(vla-GetBoundingBox obj 'MinPoint<VARIANT> 'MaxPoint<VARIANT>)` | Out args are quoted symbols set to variants: (vla-GetBoundingBox o 'mn 'mx) then (vlax-safearray->list mn). For CLOUDS use the VERTEX bbox (group 10): the bbox adds the arc bulge (scripting-lessons, 22-001 P3FIX). |
| vla-get-EffectiveName | 15 | 9 | `vla-get-EffectiveName on BlockReference` |  |
| vl-remove-if | 13 | 11 | `(vl-remove-if predicate-function<subroutine/symbol> lst<list>)` |  |
| tblnext | 12 | 4 | `(tblnext table-name<string> rewind?<boolean>)` |  |
| vla-put-color | 11 | 7 | `vla-put-color on Entity, Group, Layer, SubEntity` |  |
| vla-put-Width | 11 | 7 | `vla-put-Width on Application, Document, MText, Ole` |  |
| vla-get-color | 10 | 6 | `vla-get-color on Entity, Layer, SubEntity` |  |
| textbox | 10 | 10 | `(textbox elist<list>)` |  |
| vla-get-Lineweight | 9 | 5 | `vla-get-Lineweight on DatabasePreferences, Entity, Layer, SubEntity` |  |
| vla-put-InsertionPoint | 9 | 6 | `vla-put-InsertionPoint on Attribute, AttributeReference, BlockReference, MText` |  |
| vlax-erased-p | 9 | 8 | `(vlax-erased-p obj<vla-object>)` |  |
| vl-string->list | 9 | 5 | `(vl-string->list str<string>)` |  |
| vla-get-Height | 9 | 7 | `vla-get-Height on Application, Attribute, AttributeReference, Document` |  |
| vla-get-Width | 9 | 6 | `vla-get-Width on Application, Document, MText, Ole` |  |
| vl-sort | 8 | 5 | `(vl-sort lst<list> comparison-function<subroutine/symbol>)` |  |
| vla-Add | 8 | 7 | `(vla-Add obj Name<BSTR> Description<VARIANT>? NamedLocation<VARIANT>?) -> IAcadHyperlink` |  |
| vl-string-subst | 8 | 4 | `(vl-string-subst new-str<string> pattern<string> str<string> start-pos?<integer>)` |  |
| vla-get-ObjectID | 8 | 5 | `vla-get-ObjectID on Object` |  |
| vla-put-StartPoint | 8 | 3 | `vla-put-StartPoint on Line` |  |
| vla-get-StyleName | 8 | 6 | `vla-get-StyleName on Attribute, AttributeReference, Dimension, Leader` |  |
| vla-get-Linetype | 7 | 5 | `vla-get-Linetype on Entity, Layer, SubEntity` |  |
| load | 7 | 7 | `(load filename<string> onfailure?<string>)` |  |
| vla-get-XScaleFactor | 7 | 6 | `vla-get-XScaleFactor on BlockReference` |  |
| vla-get-TagString | 7 | 5 | `vla-get-TagString on Attribute, AttributeReference, PopupMenu, PopupMenuItem` |  |
| vl-file-delete | 7 | 7 | `(vl-file-delete filename<string>)` |  |
| vla-get-Rotation | 7 | 7 | `vla-get-Rotation on Attribute, AttributeReference, BlockReference, Dimension` |  |
| vla-InsertBlock | 6 | 4 | `(vla-InsertBlock obj InsertionPoint<VARIANT> Name<BSTR> Xscale<double> Yscale<double> Zscale<double> Rotation<double> Password<VARIANT>?) -> IAcadBlockReference` |  |
| vlax-invoke | 6 | 3 | `(undocumented)` | Undocumented but works here (returns LISP lists, no variants). Documented form: vlax-invoke-method. |
| vl-list->string | 6 | 5 | `(vl-list->string char-codes-list<list>)` |  |
| vl-file-rename | 6 | 6 | `(vl-file-rename old-filename<string> new-filename<string>)` |  |
| vlax-property-available-p | 6 | 6 | `(vlax-property-available-p obj<vla-object> property<string/symbol> check-modify?<boolean>)` |  |
| vla-get-TextStyles | 6 | 2 | `vla-get-TextStyles on Database` |  |
| vla-get-ContentBlockName | 5 | 4 | `vla-get-ContentBlockName on MLeader` |  |

## Names that do NOT exist (memory gets these wrong)

- **fboundp / identity**: do not exist (AutoLISP is not Common Lisp): use (type 'sym) / (lambda (x) x) (scripting-lessons).
- **stringp**: does not exist: (= (type x) 'STR). Found in scripts/C22-TB-T1.v1.lsp:361 by this checker (2026-11-06).
- **vla-put-Bulge / vla-GetTransform**: do not exist (use vla-SetBulge / vla-TransformBy).
- **MText.Explode**: does not exist (see vla-Explode).
- **Viewport.***: Viewport is the tiled viewport table record; the paper-space viewport entity is PViewport (IAcadPViewport). Per-viewport frozen layers are in its ACAD xdata (1003), not DXF 341 (scripting-lessons).
- **Hatch.PatternName put**: READ-ONLY: use vla-SetPattern.
