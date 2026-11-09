# DXF group codes for entget / entmod / entmake / ssget (AutoCAD 2027)

Compiled 2026-11-06 from Autodesk's official AutoCAD 2027 help (the newest year online):
- **DXF Reference 2027** (main source): `https://help.autodesk.com/cloudhelp/2027/ENU/AutoCAD-DXF/files/GUID-<id>.htm`
  (index: GUID-235B22E0-A567-4CF6-92D3-38A2306D73F3). Each section below names its topic GUID (first 8 hex digits; the full GUID is in the table at the end).
- **AutoLISP Developer's Guide 2027**, used only for the entget/entmod/entmake/ssget notes, because the DXF Reference does not document them: `https://help.autodesk.com/cloudhelp/2027/ENU/AutoCAD-AutoLISP/files/GUID-<id>.htm`

Conventions (DXF "About DXF Formatting Conventions"): **APP:** marks a meaning that applies to AutoLISP/ObjectARX and **DXF:** marks one that applies to the file only. In entget lists a point is a single group, for example `(10 x y z)`. The separate DXF 20/30 groups are folded into the 10 row here.
Only codes and notes from the pages above are included. If a value or default is not listed, the reference does not give it.

---

## ENTGET / ENTMOD / ENTMAKE NOTES

**Reading (entget)**, from "About Obtaining Entity Information" and "Retrieving Extended Data":
- The first item is `(-1 . <ename>)`. The ename changes every time the drawing is opened and is never saved. Handle 5 stays constant for the entity's whole life (use `handent` to convert a handle to an ename).
- Header items (color, linetype, thickness, attributes-follow flag, handle) are returned only when they differ from the default. Optional definition fields are returned whether or not they equal their defaults. Do not depend on group order: use `assoc`.
- Points are in the entity's OCS. For point, line, 3D line, 3DFACE, 3D polyline, 3D mesh and dimension, the OCS equals the WCS. Other entities derive their OCS from the 210 extrusion. Use `trans` to convert.
- Angles (codes 50-58) are radians through AutoLISP and degrees in DXF files.
- Xdata is returned only for the applications you request: `(entget e '("APP1" "APP2"))`. Wildcards are allowed and the apps must be registered with `regapp`.

**Assigned by AutoCAD (internal fields).** entmod cannot change these and entmake ignores them: -2 (entity name reference), -1 (entity name), 5 (handle). Symbol tables: handle codes 5 and 105 cannot be changed in entmod or specified in entmake. 330 is the soft pointer to the owner BLOCK_RECORD (DXF common codes). The reference does not say whether 330 can be set from LISP.

**entmod restrictions** ("Modifying an Entity without the Command Function"):
- It cannot change the entity type or handle, internal fields, or VIEWPORT entities (an attempt raises an error). Other restrictions apply to dimensions and hatch patterns.
- Every referenced style, linetype, shape and block must already exist. A layer is the exception: an unknown layer name is created with LAYER New defaults.
- It can modify subentities (vertices, attributes). It does not redraw the whole complex entity, so call `(entupd main-ename)` afterwards.
- Changing an entity inside a block definition affects every reference to it. Non-constant attributes are not updated on each reference.

**entmake requirements** ("Adding an Entity without Using the Command Function"):
- The first or second member must be the type (group 0). If the first member is not the type, it can only be -1.
- Internal fields are ignored. entmake cannot create VIEWPORT entities. New layer names are accepted and created. entmake validates layer, linetype, color, block, dimstyle, text style and shape names, and fails if the entity is not valid.
- **Subclass markers:** for entity types from R13 onward you must include `(100 . "AcDbEntity")` plus the specific subclass marker(s). These follow group 0 and come before the property codes. Minimum MTEXT:
  `(entmake '((0 . "MTEXT") (100 . "AcDbEntity") (8 . "ALAYER") (100 . "AcDbMText") (10 4.0 4.0 0.0) (1 . "Some\\Ptext")))`
- Types that do **not** need subclass markers (pre-R13): 3DFACE, ARC, ATTDEF, ATTRIB, CIRCLE, DIMENSION, INSERT, LINE, POINT, POLYLINE (legacy 2D/3D), SEQEND, SHAPE, SOLID, TEXT, VERTEX, VIEWPORT. All others (LWPOLYLINE, MTEXT, ELLIPSE, SPLINE, HATCH, LEADER, ...) do.
- Complex entities (block, polyface mesh, legacy polyline) are built with one entmake per subentity and are added only when SEQEND or ENDBLK arrives. Only one complex entity can be in progress at a time, and `(entmake)` with no arguments cancels it. Any invalid subentity rejects the whole complex entity. A subentity whose 67 does not match the header's 67, or a change of space during construction, cancels it. A legacy POLYLINE needs `(66 . 1)` followed by VERTEX entities and then SEQEND. An INSERT with `(66 . 1)` expects ATTRIBs followed by SEQEND.
- Non-graphical objects: the object type decides where the object goes. A layer goes into the LAYER table, and a graphical object goes into the current space. Symbol table entries (not the tables themselves) can be entmade, but `*ACTIVE` entries cannot. Group 70 of a table entry is ignored by entmod and entmake. To modify a table record you need its ename (from `tblobjname`, not `tblsearch`/`tblnext`). Table entries cannot be entdel'd. A LAYER entry cannot be modified except for its xdata, but it can be renamed (FAQ "Symbol Table Entries Cannot Be Renamed or Modified").
- Dictionaries: you can entget a dictionary and entmod its xdata, but not its entries. Use `dictsearch`/`dictnext`/`dictadd`/`dictrename`. An XRECORD can be made with `entmakex` (no owner) and then added with `dictadd`.
- entdel works only on main entities. Attributes and legacy vertices cannot be deleted on their own.
- Xdata is limited to 16K per entity (`xdsize`, `xdroom`). In entmake/entmod a 1071 value may be given as an integer or a real, and AutoLISP returns it as a real.

**ssget filter lists** ("Selection Set Filter Lists", "Relational Tests", "Logical Grouping", "Wild-Card Patterns", "Filtering for Extended Data"):
- A filter list is an association list of DXF codes. ssget ignores -1, 5 and xdata codes (>1000) and silently ignores invalid codes. Use -3 to test for xdata. Several conditions are ANDed.
- Matching color 62 finds only entities with an explicit color, not ByLayer or ByBlock. The result can contain entities from both model and paper space (67).
- **Relational operator** `(-4 . "op")` applies to the next group, for numeric groups only: `"*"` (anything), `"="`, `"!="` `"/="` `"<>"` (not equal), `"<"`, `"<="`, `">"`, `">="`, `"&"` (bitwise AND: true if any mask bit is set), `"&="` (masked equals: all mask bits set). `&` and `&=` work on integer groups only. For points, combine the tests per axis, for example `">,>,*"`. 210 accepts only `*`, `=` or `!=`. String groups take no relational operators; use wildcards (wcmatch syntax). Escape `*` for anonymous blocks: `(2 . "`*U2")`.
- **Grouping** `(-4 . "<AND")...(-4 . "AND>")`, `"<OR"/"OR>"` (one or more operands), `"<XOR"/"XOR>"` (exactly two), `"<NOT"/"NOT>"` (exactly one). They must balance or ssget fails. They are not case-sensitive and are not allowed inside a -3 group. Several apps in one -3 are an implied AND: `(-3 ("APP1")("APP2"))`. For other logic use separate -3 groups. Wildcards work too: `(-3 ("APP1,APP2"))`.
  Example: `(ssget "X" '((-4 . "<OR") (-4 . "<AND") (0 . "CIRCLE") (40 . 1.0) (-4 . "AND>") (-4 . "<AND") (0 . "LINE") (8 . "ABC") (-4 . "AND>") (-4 . "OR>")))`

**Value types by code range** (DXF "Group Code Value Types", GUID-2553CF98): 0-9 string; 10-37 3D point coordinates (double); 38-59 double; 60-79 16-bit int; 90-99 32-bit int; 100-102 string; 105 hex handle string; 110-149 double; 160-169 64-bit int; 170-179 16-bit int; 210-239 double; 270-289 16-bit int; 290-299 boolean; 300-309 string; 310-319 hex binary chunk; 320-329 hex handle (arbitrary, not translated); 330-369 hex object IDs (330-339 soft pointer, 340-349 hard pointer, 350-359 soft owner, 360-369 hard owner, all enames in AutoLISP); 370-389 16-bit int; 390-399 hex handle; 400-409 16-bit int; 410-419 string; 420-429 32-bit int; 430-439 string; 440-449 32-bit int; 450-459 long; 460-469 double; 470-479 string; 480-481 hex handle; 999 comment; 1000-1003 string; 1004 binary hex; 1005 string handle; 1010-1042 double; 1070 16-bit int; 1071 32-bit int.
Other numerical-order notes (GUID-3F0380A5): -5 APP persistent reactor chain; -4 APP conditional operator (ssget only); -3 APP xdata sentinel; -2 APP entity name reference; 38 elevation if nonzero; 66 entities-follow flag; 102 control string `{name` / `}`; 999 DXF comment.

---

## COMMON
Common entity codes, AcDbEntity (GUID-3610039E). The "If omitted" column of the reference is shown under Notes as "default". Values equal to the default are omitted from DXF output.

| Code | Meaning | Notes |
|---|---|---|
| -1 | APP: entity name (ename) | Changes each time the drawing opens and is never saved. Not omitted |
| 0 | Entity type (string) | Not omitted |
| 5 | Handle (string, up to 16 hex digits) | Not omitted. Assigned by AutoCAD |
| 102 | `{application_name` ... `}` application-defined group | Optional |
| 102 / 330 | `{ACAD_REACTORS` ... 330 soft pointer to owner dictionary ... `}` | Only if persistent reactors are attached |
| 102 / 360 | `{ACAD_XDICTIONARY` ... 360 hard owner to extension dictionary ... `}` | Only if an extension dictionary exists |
| 330 | Soft pointer to owner BLOCK_RECORD | Not omitted |
| 100 | Subclass marker `AcDbEntity` | Not omitted. Required in entmake for post-R12 types (see notes) |
| 67 | 0 or absent = model space; 1 = paper space | Optional, default 0 |
| 410 | APP: layout tab name | Not omitted |
| 8 | Layer name | Not omitted |
| 6 | Linetype name; `BYBLOCK` = floating linetype | Present if not BYLAYER. Default BYLAYER |
| 347 | Hard pointer to material object | Present if not BYLAYER |
| 62 | Color number: 0 = BYBLOCK, 256 = BYLAYER, negative = layer is off | Present if not BYLAYER. Default BYLAYER |
| 370 | Lineweight enum value (16-bit int) | Not omitted |
| 48 | Linetype scale (double) | Optional, default 1.0 |
| 60 | Visibility: 0 = visible, 1 = invisible | Optional, default 0 |
| 92 / 310 | Proxy graphics byte count / binary chunks | Optional |
| 420 | True color (32-bit int) 0x00RRGGBB: lowest byte blue, then green, then red, top byte 0 | No default. Example: R200 G100 B50 = 0x00C86432 = 13132850 (GUID-3F0380A5) |
| 430 | Color name (string) | No default |
| 440 | Transparency (32-bit int) | No default |
| 390 | Hard pointer to plot style object | No default |
| 284 | Shadow mode: 0 casts and receives, 1 casts, 2 receives, 3 ignores | Obsolete since 2016-based products |
| 39 | Thickness (fixed meaning; double) | Listed per entity, default 0 (GUID-3F0380A5) |
| 210 | Extrusion direction, APP: 3D vector | Listed per entity, default (0 0 1) |
| -3 | APP: xdata sentinel. In entget: `(-3 ("APPNAME" (1000 . "x") ...))` | Follows the normal data (GUID-A2A628B0) |
| 1001 | Registered application name (string, up to 31 bytes; matches an APPID entry) | Starts each app's group. Do not put a 1001 inside your data. One group per app per entity |
| 1000 | String (up to 255 bytes) | |
| 1002 | Control string `"{"` or `"}"` | Nestable lists. Braces must balance |
| 1003 | Layer name | |
| 1004 | Binary chunk (up to 127 bytes; hex in DXF) | |
| 1005 | Database handle | Translated on INSERT, XBIND and so on; AUDIT sets a bad one to 0. Stored as a handle, not converted to an ename |
| 1010 | 3 reals (point) | Never altered by AutoCAD |
| 1011 | World-space position | Moved, scaled, rotated, mirrored and stretched with the parent |
| 1012 | World-space displacement | Scaled, rotated and mirrored (not moved or stretched) |
| 1013 | World direction | Rotated and mirrored only (unit length) |
| 1040 | Real | |
| 1041 | Distance (real) | Scaled with the parent |
| 1042 | Scale factor (real) | Scaled with the parent |
| 1070 | 16-bit integer (signed or unsigned) | |
| 1071 | 32-bit signed long | AutoLISP returns it as a real |

Dimension style overrides (GUID-6A4C31C0) are xdata on DIMENSION, LEADER and TOLERANCE entities:
`(-3 ("ACAD" (1000 . "DSTYLE") (1002 . "{") (1070 . 177) (1070 . 3) (1070 . 71) (1070 . 1) (1002 . "}")))`, written as pairs of (DIMSTYLE group code, value). In this example DIMCLRE (177) = 3 and DIMTOL (71) = 1.

---

## LINE
GUID-FCEF5726. 2D/3D: points in WCS.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbLine` | |
| 39 | Thickness | Optional, default 0 |
| 10 | Start point (WCS) | APP: 3D point |
| 11 | Endpoint (WCS) | APP: 3D point |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## LWPOLYLINE
GUID-748FC305. Points in OCS. entmake needs the 100 markers.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbPolyline` | |
| 90 | Number of vertices (32-bit int) | |
| 70 | Polyline flag (bit-coded): 1 = Closed; 128 = Plinegen | Default 0 |
| 43 | Constant width | Optional, default 0. Not used if variable width (40/41) is set |
| 38 | Elevation | Optional, default 0 |
| 39 | Thickness | Optional, default 0 |
| 10 | Vertex coordinates (OCS), one per vertex | APP: 2D point. Repeats |
| 91 | Vertex identifier | |
| 40 | Starting width, one per vertex | Optional, default 0. Not used if 43 is set |
| 41 | End width, one per vertex | Optional, default 0. Not used if 43 is set |
| 42 | Bulge, one per vertex | Optional, default 0. Bulge = tan(included angle / 4); negative = clockwise; 1 = semicircle (definition from VERTEX) |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## POLYLINE
GUID-ABF6B778. Legacy 2D/3D polyline, polygon mesh and polyface. Followed by VERTEX entities and then SEQEND.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDb2dPolyline` or `AcDb3dPolyline` | |
| 66 | Obsolete; formerly the "entities follow" flag | DXF: ignore if present. The AutoLISP guide requires `(66 . 1)` in entmake |
| 10 | DXF: always 0. APP: dummy point; X = Y = 0, Z = elevation | OCS for 2D, WCS for 3D |
| 39 | Thickness | Optional, default 0 |
| 70 | Flag (bit-coded): 1 closed (or mesh closed in M); 2 curve-fit vertices added; 4 spline-fit vertices added; 8 3D polyline; 16 3D polygon mesh; 32 mesh closed in N; 64 polyface mesh; 128 linetype generated continuously around vertices | Default 0 |
| 40 | Default start width | Optional, default 0 |
| 41 | Default end width | Optional, default 0 |
| 71 / 72 | Polygon mesh M / N vertex count | Optional, default 0 |
| 73 / 74 | Smooth surface M / N density | Optional, default 0 |
| 75 | Curve and smooth surface type (not bit-coded): 0 none; 5 quadratic B-spline; 6 cubic B-spline; 8 Bezier | Optional, default 0 |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## VERTEX
GUID-0741E831.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbVertex` | |
| 100 | Subclass marker `AcDb2dVertex` or `AcDb3dPolylineVertex` | |
| 10 | Location (OCS when 2D, WCS when 3D) | APP: 3D point |
| 40 | Starting width | Optional, default 0 |
| 41 | Ending width | Optional, default 0 |
| 42 | Bulge: tan(1/4 included angle), negative if the arc runs clockwise; 0 = straight; 1 = semicircle | Optional, default 0 |
| 70 | Vertex flags: 1 extra vertex from curve-fitting; 2 curve-fit tangent defined; 4 not used; 8 spline vertex from spline-fitting; 16 spline frame control point; 32 3D polyline vertex; 64 3D polygon mesh; 128 polyface mesh vertex | |
| 50 | Curve-fit tangent direction | |
| 71-74 | Polyface mesh vertex indexes (a negative index marks an invisible edge) | Optional, present only if nonzero |
| 91 | Vertex identifier | |

## ARC
GUID-0B14D8F1. Center in OCS.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbCircle` | |
| 39 | Thickness | Optional, default 0 |
| 10 | Center point (OCS) | APP: 3D point |
| 40 | Radius | |
| 100 | Subclass marker `AcDbArc` | |
| 50 | Start angle | Radians in AutoLISP |
| 51 | End angle | Radians in AutoLISP |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## CIRCLE
GUID-8663262B.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbCircle` | |
| 39 | Thickness | Optional, default 0 |
| 10 | Center point (OCS) | APP: 3D point |
| 40 | Radius | |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## ELLIPSE
GUID-107CB04F. Points in WCS.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbEllipse` | |
| 10 | Center point (WCS) | APP: 3D point |
| 11 | Endpoint of major axis, relative to the center (WCS) | APP: 3D point |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 40 | Ratio of minor axis to major axis | |
| 41 | Start parameter | 0.0 for a full ellipse |
| 42 | End parameter | 2pi for a full ellipse |

## TEXT
GUID-62E5383D. Note the **second** `AcDbText` marker before 73.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbText` | |
| 39 | Thickness | Optional, default 0 |
| 10 | First alignment point (OCS) | APP: 3D point |
| 40 | Text height | |
| 1 | Default value (the string itself) | |
| 50 | Text rotation | Optional, default 0 |
| 41 | Relative X scale factor (width) | Optional, default 1. Adjusted for Fit text |
| 51 | Oblique angle | Optional, default 0 |
| 7 | Text style name | Optional, default STANDARD |
| 71 | Text generation flags: 2 = backward (mirrored in X); 4 = upside down (mirrored in Y) | Optional, default 0 |
| 72 | Horizontal justification (not bit-coded): 0 Left; 1 Center; 2 Right; 3 Aligned*; 4 Middle*; 5 Fit* (*only if 73 = 0) | Optional, default 0 |
| 11 | Second alignment point (OCS) | Optional. Meaningful only if 72 or 73 is nonzero |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 100 | Subclass marker `AcDbText` (second) | |
| 73 | Vertical justification (not bit-coded): 0 Baseline; 1 Bottom; 2 Middle; 3 Top | Optional, default 0 |

72/73 combinations: 73=3: TLeft/TCenter/TRight (72=0/1/2). 73=2: MLeft/MCenter/MRight. 73=1: BLeft/BCenter/BRight. 73=0: Left/Center/Right/Aligned/Middle/Fit (72=0..5).
If 72 and/or 73 is nonzero, the first alignment point is ignored and recalculated from the second alignment point (11) and the text. If both are zero or missing, 11 is meaningless.

## MTEXT
GUID-5E5DB93B. entmake needs `(100 . "AcDbEntity")` and `(100 . "AcDbMText")`.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbMText` | |
| 10 | Insertion point | APP: 3D point |
| 40 | Nominal (initial) text height | |
| 41 | Reference rectangle width | |
| 71 | Attachment point: 1 Top left; 2 Top center; 3 Top right; 4 Middle left; 5 Middle center; 6 Middle right; 7 Bottom left; 8 Bottom center; 9 Bottom right | |
| 72 | Drawing direction: 1 left to right; 3 top to bottom; 5 by style | |
| 1 | Text string. Under 250 chars: all in group 1. Longer: split into 250-char chunks in one or more group 3s, with the last (<250 chars) in group 1 | |
| 3 | Additional text (always 250-character chunks) | Optional. Comes before the final 1 |
| 7 | Text style name | Optional, default STANDARD |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 11 | X-axis direction vector (WCS) | APP: 3D vector. A 50 given as input is converted to this; if both are given, the last one wins |
| 42 | Horizontal width of the characters (<= 41) | Read-only, ignored if supplied |
| 43 | Vertical height of the mtext | Read-only, ignored if supplied |
| 50 | Rotation angle in radians | |
| 73 | Line spacing style: 1 At least; 2 Exact | Optional |
| 44 | Line spacing factor (0.25-4.00, of default 3-on-5 spacing) | Optional |
| 90 | Background fill: 0 off; 1 use background fill color; 2 use drawing window color | |
| 63 | Background fill color (when 90 = 1) | Optional |
| 420-429 | Background color (RGB) | |
| 430-439 | Background color (name) | |
| 45 | Fill box scale (border around the text) | Optional |
| 441 | Transparency of background fill | Not implemented |
| 75 / 76 | Column type / column count | |
| 78 / 79 | Column flow reversed / column autoheight | |
| 48 / 49 | Column width / column gutter | |
| 50 | Column heights: followed by the count (Int16), then the heights | The reference lists 50 twice (rotation and column heights) |

## INSERT
GUID-28FA4CFB. Insertion point in OCS. ATTRIBs follow when 66 = 1, ending with SEQEND.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbBlockReference` | |
| 66 | Attributes-follow flag: 1 = ATTRIB entities follow, ending with SEQEND | Optional, default 0 |
| 2 | Block name | Must exist for entmake |
| 10 | Insertion point (OCS) | APP: 3D point |
| 41 / 42 / 43 | X / Y / Z scale factor | Optional, default 1 |
| 50 | Rotation angle | Optional, default 0 |
| 70 / 71 | Column count / row count | Optional, default 1 |
| 44 / 45 | Column spacing / row spacing | Optional, default 0 |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## ATTRIB
GUID-7DD8B495. Note the vertical justification is **74** here (not 73).

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbText` | |
| 39 | Thickness | Optional, default 0 |
| 10 | Text start point (OCS) | APP: 3D point |
| 40 | Text height | |
| 1 | Default value (string) | |
| 100 | Subclass marker `AcDbAttribute` | |
| 280 | Version number: 0 = 2010 | |
| 2 | Attribute tag (no spaces) | |
| 70 | Attribute flags: 1 invisible; 2 constant; 4 verification required; 8 preset (no prompt) | |
| 73 | Field length | Optional, default 0. Not currently used |
| 50 | Text rotation | Optional, default 0 |
| 41 | Relative X scale (width) | Optional, default 1 |
| 51 | Oblique angle | Optional, default 0 |
| 7 | Text style name | Optional, default STANDARD |
| 71 | Text generation flags (as TEXT) | Optional, default 0 |
| 72 | Horizontal justification (as TEXT 72) | Optional, default 0 |
| 74 | Vertical justification (as TEXT 73) | Optional, default 0 |
| 11 | Alignment point (OCS) | Optional. Present only if 72 or 74 is present and nonzero |
| 210 | Extrusion direction | Optional. Present only if not parallel to WCS Z |
| 280 | Lock position flag (locks position within the block reference) | |
| 100 ... | Multiline-attribute data: `AcDbXrecord` marker; 280 duplicate-record cloning (1 = keep existing); 70 MText flag (2 = multiline attribute, 4 = constant multiline attdef); 70 isReallyLocked (0/1); 70 count of secondary attribs; 340 hard pointer(s) to them; 10 alignment point; 40 current annotation scale; 2 tag; then an embedded MTEXT (0 MTEXT, 100 AcDbEntity, 67, 8, 100 AcDbMText, 10, 40, 41, 46 defined annotation height, 71, 72, 1/3, 7, 210, 11, 42, 43, 50, 73, 44, 90, 63, 420-429, 430-439, 45, 441) | As listed in the reference for multiline attributes |

If 72 and/or 74 is nonzero, the insertion point (10) is ignored and recalculated from the alignment point (11). Otherwise the alignment point is ignored and recalculated from 10.

## ATTDEF
GUID-F0EA099B.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbText` | |
| 39 | Thickness | Optional, default 0 |
| 10 | First alignment point (OCS) | APP: 3D point |
| 40 | Text height | |
| 1 | Default value (string) | |
| 50 | Text rotation | Optional, default 0 |
| 41 | Relative X scale (width) | Optional, default 1 |
| 51 | Oblique angle | Optional, default 0 |
| 7 | Text style name | Optional, default STANDARD |
| 71 | Text generation flags (as TEXT) | Optional, default 0 |
| 72 | Horizontal justification (as TEXT) | Optional, default 0 |
| 11 | Second alignment point (OCS) | Optional. Meaningful only if 72 or 74 is nonzero |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 100 | Subclass marker `AcDbAttributeDefinition` | |
| 280 | Version number: 0 = 2010 | |
| 3 | Prompt string | |
| 2 | Tag string (no spaces) | |
| 70 | Attribute flags: 1 invisible; 2 constant; 4 verification required; 8 preset | |
| 73 | Field length | Optional, default 0. Not used |
| 74 | Vertical justification (as TEXT 73) | Optional, default 0 |
| 280 | Lock position flag | |
| 100 ... | Same multiline-attribute block as ATTRIB (AcDbXrecord ... embedded MTEXT) | |

## DIMENSION
GUID-239A1BDD / common codes GUID-EDD54EAC. Common codes come first, then the type-specific subclass.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbDimension` | |
| 280 | Version number: 0 = 2010 | |
| 2 | Name of the block holding the dimension picture | |
| 10 | Definition point (WCS) | APP: 3D point |
| 11 | Middle point of dimension text (OCS) | APP: 3D point |
| 70 | Dimension type: 0 rotated/horizontal/vertical; 1 aligned; 2 angular; 3 diameter; 4 radius; 5 angular 3-point; 6 ordinate. Bit values added on top: 32 = block (2) referenced by this dimension only (always set R13+); 64 = ordinate X-type (Y-type if clear; with 6 only); 128 = text at a user-defined location | |
| 71 | Attachment point 1-9 (as MTEXT 71) | |
| 72 | Text line-spacing style: 1 (or missing) At least; 2 Exact | Optional |
| 41 | Text line-spacing factor (0.25-4.00) | Optional |
| 42 | Actual measurement | Optional; read-only |
| 1 | User text override: null or `"<>"` = measurement; `" "` (one space) = suppressed; anything else = drawn as the text | Optional |
| 53 | Text rotation away from the default orientation | Optional |
| 51 | Horizontal direction: negative of the angle between OCS X and UCS X | Optional |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 3 | Dimension style name | |

WCS points: 10, 13, 14, 15. OCS points: 11, 12, 16. Overrides are "ACAD" xdata (see COMMON).
Type-specific subclasses:
- **Aligned** `AcDbAlignedDimension`: 12 insertion point for Baseline/Continue clones (OCS); 13 start of first extension line (WCS); 14 start of second extension line (WCS); 10 = dimension line location.
- **Linear/rotated** `AcDbAlignedDimension` (12, 13, 14 as aligned) + 50 angle of rotated/horizontal/vertical dims + 52 oblique (added to 50 gives the extension-line angle), then `AcDbRotatedDimension`.
- **Radial/diameter** `AcDbRadialDimension` / `AcDbDiametricDimension`: 15 point on the circle/arc (WCS); 40 leader length. Radius: 10 = center. Diameter: 10 = opposite point.
- **Angular** `AcDb3PointAngularDimension`: 13, 14 extension line endpoints; 15 vertex (3-point) or second line endpoint; 16 dimension arc point (OCS).
- **Ordinate** `AcDbOrdinateDimension`: 13 feature location; 14 leader endpoint; 10 = UCS origin at creation.

## HATCH
GUID-C6C71CED, boundary paths GUID-DC5215D6. Main codes (MPolygon-only meanings are omitted).

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbHatch` | |
| 10 | Elevation point (OCS): X = Y = 0, Z = elevation | APP: 3D point |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 2 | Hatch pattern name | |
| 70 | Solid fill flag: 0 pattern; 1 solid | |
| 71 | Associativity: 0 non-associative; 1 associative | |
| 91 | Number of boundary paths (loops) | |
| varies | Boundary path data, repeated 91 times (below) | |
| 75 | Hatch style: 0 odd parity (Normal); 1 outermost only (Outer); 2 entire area (Ignore) | |
| 76 | Pattern type: 0 user-defined; 1 predefined; 2 custom | |
| 52 | Pattern angle | Pattern fill only |
| 41 | Pattern scale or spacing | Pattern fill only |
| 77 | Pattern double: 0 no; 1 yes | Pattern fill only |
| 78 | Number of pattern definition lines (pattern line data follows) | |
| 47 | Pixel size used for intersection/ray casting | |
| 98 | Number of seed points | |
| 10 | Seed point (OCS) | APP: 2D point. Repeats |
| 450 | 0 solid hatch; 1 gradient | Optional. If present, 451, 452, 453, 460, 461, 462 and 470 must all be present |
| 451 | Reserved (0) | |
| 452 | 0 two-color; 1 single-color gradient | |
| 453 | Number of colors: 0 solid; 2 gradient | |
| 460 | Gradient rotation (radians) | Default 0 |
| 461 | Gradient shift (0.0 non-shifted .. 1.0 shifted) | |
| 462 | Color tint (0.0-1.0) | Default 0 |
| 470 | Gradient name string | Default LINEAR |

**Boundary path (per loop):** 92 path type flag (bit-coded: 0 default; 1 external; 2 polyline; 4 derived; 8 textbox; 16 outermost).
- If polyline (bit 2): 72 has-bulge flag; 73 is-closed flag; 93 vertex count; then for each vertex 10 (2D, OCS) and optional 42 bulge (default 0).
- Otherwise: 93 number of edges, then per edge 72 edge type (1 line; 2 circular arc; 3 elliptic arc; 4 spline) followed by its data. Line: 10 start, 11 end. Arc: 10 center, 40 radius, 50 start angle, 51 end angle, 73 is-counterclockwise. Ellipse: 10 center, 11 major-axis endpoint relative to center, 40 minor axis length (fraction of major), 50, 51, 73. Spline: 94 degree, 73 rational, 74 periodic, 95 knot count, 96 control-point count, 40 knots, 10 control points, 42 weights (default 1), 97 fit-data count, 11 fit points, 12 start tangent, 13 end tangent.
- Then: 97 number of source boundary objects, followed by 330 references to the source boundary objects (repeats).

## LEADER
GUID-396B2369. DIMSTYLE overrides are "ACAD" xdata.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbLeader` | |
| 3 | Dimension style name | |
| 71 | Arrowhead flag: 0 disabled; 1 enabled | |
| 72 | Path type: 0 straight segments; 1 spline | |
| 73 | Creation flag: 0 with text; 1 with tolerance; 2 with block reference; 3 without annotation | Default 3 |
| 74 | Hookline direction: 0 opposite to the horizontal vector; 1 same direction | |
| 75 | Hookline flag: 0 none; 1 has hookline | |
| 40 / 41 | Text annotation height / width | |
| 76 | Number of vertices | Ignored for OPEN |
| 10 | Vertex coordinates (one per vertex) | APP: 3D point. Repeats |
| 77 | Color used if the leader's DIMCLRD = BYBLOCK | |
| 340 | Hard reference to the annotation (MTEXT, TOLERANCE or INSERT) | |
| 210 | Normal vector | |
| 211 | "Horizontal" direction for the leader | |
| 212 | Offset of the last vertex from the block reference insertion point | |
| 213 | Offset of the last vertex from the annotation placement point | |

## MULTILEADER
Topic "MLEADER (DXF)", GUID-72D20B8C; common codes GUID-69B9139A; context data GUID-EC56D0DE. The reference lists the codes **without a subclass marker or the group-0 string**: check `(cdr (assoc 0 (entget e)))` on an existing multileader before filtering. Main codes (descriptions as given, often terse):

| Code | Meaning | Notes |
|---|---|---|
| 340 | Leader style ID | |
| 90 | Property override flag | |
| 170 | Leader line type | |
| 91 | Leader line color | |
| 341 | Leader linetype ID | |
| 171 | Leader line weight | |
| 290 / 291 | Enable landing / enable dogleg | |
| 41 | Dogleg length | |
| 342 / 42 | Arrowhead ID / arrowhead size | |
| 172 | Content type | |
| 343 | Text style ID | |
| 173 / 95 | Text left / right attachment type | |
| 174 / 175 | Text angle type / text alignment type | |
| 92 | Text color | |
| 292 | Enable frame text | |
| 344 / 93 | Block content ID / color | |
| 10 / 43 | Block content scale / rotation | |
| 176 | Block content connection type | |
| 293 | Enable annotation scale | |
| 94 / 345 | Arrowhead index / arrowhead ID | |
| 330 / 177 / 44 / 302 | Block attribute ID / index / width / text string | |
| 294 | Text direction negative | |
| 178 / 179 | Text align in IPE / text attachment point | |
| 271 | MText attachment direction: 0 horizontal; 1 vertical | |
| 272 | Bottom attachment: 9 center; 10 underline and center | |
| 273 | Top attachment: 9 center; 10 overline and center | |

Context data (in a context block): 40 content scale; 10 content base position; 41 text height; 140 arrowhead size; 145 landing gap; 290 hasMText; **304 default text contents**; 11 text normal; 340 text style ID; **12 text location**; 13 text direction; 42 text rotation; 43 text width; 44 text height; 45 line spacing factor; 170 line spacing style; 90 text color; 171 text attachment; 172 flow direction; 91 background color; 141 background scale; 92 background transparency; 291 / 292 background color on / fill on; 173 column type; 293 auto height; 142 / 143 column width / gutter; 294 flow reversed; 144 column height; 295 word break; 296 hasBlock; 341 block content ID; 14 block normal; 15 block position; 16 block scale; 46 block rotation; 93 block color; 47 block transformation matrix; 110 / 111 / 112 plane origin / X axis / Y axis; 297 plane normal reversed.
Leader node: 290 has set last leader line point; 291 has set dogleg vector; 10 last leader line point; 11 dogleg vector; 12 / 13 break start / end; 90 leader branch index; 40 dogleg length. Leader line: 10 vertex; 90 break point index; 11 / 12 break start / end; 91 leader line index.

## VIEWPORT
GUID-2602B0FB. **entmod cannot change a viewport and entmake cannot create one** (AutoLISP guide). Points in WCS.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbViewport` | |
| 10 | Center point (WCS) | APP: 3D point |
| 40 / 41 | Width / height in paper space units | |
| 68 | Status: -1 = on but fully off screen, or inactive because $MAXACTVP is exceeded; 0 = off; positive = on and active, giving the stacking order (1 = active viewport) | APP: viewport on/off-screen state |
| 69 | Viewport ID | APP: viewport identification number |
| 12 | View center point (DCS) | APP: 2D point |
| 13 / 14 / 15 | Snap base / snap spacing / grid spacing | 2D |
| 16 | View direction vector (WCS) | |
| 17 | View target point (WCS) | |
| 42 | Perspective lens length | |
| 43 / 44 | Front / back clip plane Z | |
| 45 | View height (model space units) | |
| 50 | Snap angle | |
| 51 | View twist angle | |
| 72 | Circle zoom percent | |
| 331 | Frozen layer ID/handle (repeats) | Optional |
| 90 | Status bits: 1 perspective; 2 front clip; 4 back clip; 8 UCS follow; 16 front clip not at eye; 32 UCS icon visible; 64 UCS icon at origin; 128 fast zoom; 256 snap; 512 grid; 1024 isometric snap; 2048 hide plot; 4096 kIsoPairTop; 8192 kIsoPairRight; 16384 zoom locking; 32768 always on; 65536 non-rectangular clipping; 131072 viewport off; 262144 grid beyond limits; 524288 adaptive grid; 1048576 grid subdivision; 2097152 grid follows workplane | |
| 340 | Hard pointer to the clipping boundary entity | Only if non-rectangular |
| 1 | Plot style sheet name | |
| 281 | Render mode: 0 2D optimized; 1 wireframe; 2 hidden line; 3 flat; 4 Gouraud; 5 flat + wire; 6 Gouraud + wire | |
| 71 | UCS per viewport: 0 no; 1 the viewport stores its own UCS | |
| 74 | Display UCS icon at origin | Currently ignored |
| 110 / 111 / 112 | UCS origin / X axis / Y axis | |
| 345 / 346 | Named UCS record / base UCS record (if orthographic) | |
| 79 | Orthographic UCS: 0 not; 1 top; 2 bottom; 3 front; 4 back; 5 left; 6 right | |
| 146 | Elevation | |
| 170 | ShadePlot: 0 as displayed; 1 wireframe; 2 hidden; 3 rendered | |
| 61 | Major grid line frequency | |
| 332 / 333 / 348 / 361 | Background / shade plot / visual style / sun ID | Optional |
| 292 / 282 | Default lighting flag / type (0 one distant, 1 two distant) | |
| 141 / 142 | Brightness / contrast | |
| 63, 421, 431 | Ambient light color | Written only if not black |
| 335, 343, 344, 91 | Soft pointer to viewport object (layer VP property override) | |

## OLE2FRAME
GUID-77747CE6. **This information is read-only**: on OPEN the values are ignored because they are part of the OLE binary object.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbOle2Frame` | |
| 70 | OLE version number | |
| 3 | Length of binary data | entget sample shows `(3 "Paintbrush Picture")` |
| 10 | Upper-left corner (WCS) | APP: 3D point |
| 11 | Lower-right corner (WCS) | APP: 3D point |
| 71 | OLE object type: 1 link; 2 embedded; 3 static | |
| 72 | Tile mode: 0 model space; 1 paper space | |
| 90 | Length of binary data | |
| 310 | Binary data (multiple lines) | Not in the entget sample |
| 1 | End of OLE data (the string "OLE") | |

entget sample: `((-1 . <Entity name>) (0 . "OLE2FRAME") (5 . "2D") (100 . "AcDbEntity") (67 . 1) (8 . "0") (100 . "AcDbOle2Frame") (70 . 2) (3 "Paintbrush Picture") (10 4.43116 5.66599 0.0) (11 6.4188 4.24494 0.0) (71 . 2) (72 . 1))`

## SPLINE
GUID-E1F884F8. Points in WCS.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbSpline` | |
| 210 | Normal vector | Omitted if the spline is nonplanar |
| 70 | Spline flag (bit-coded): 1 closed; 2 periodic; 4 rational; 8 planar; 16 linear (planar bit also set) | |
| 71 | Degree | |
| 72 / 73 / 74 | Number of knots / control points / fit points | |
| 42 / 43 / 44 | Knot / control-point / fit tolerance | Defaults 0.0000001 / 0.0000001 / 0.0000000001 |
| 12 / 13 | Start / end tangent (WCS) | May be omitted |
| 40 | Knot value (one per knot) | |
| 41 | Weight (if not 1) | Present if not all weights are 1 |
| 10 | Control points (WCS), one per point | |
| 11 | Fit points (WCS), one per point | |

## SOLID
GUID-E0C5F04E. 2D solid fill (OCS).

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbTrace` | |
| 10 / 11 / 12 | First / second / third corner | APP: 3D point |
| 13 | Fourth corner (same as the third if only three corners were entered) | APP: 3D point |
| 39 | Thickness | Optional, default 0 |
| 210 | Extrusion direction | Optional, default 0,0,1 |

## POINT
GUID-9C6AD32D.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbPoint` | |
| 10 | Point location (WCS) | APP: 3D point |
| 39 | Thickness | Optional, default 0 |
| 210 | Extrusion direction | Optional, default 0,0,1 |
| 50 | Angle of the UCS X axis when the point was drawn | Optional, default 0. Used when PDMODE is nonzero |

## WIPEOUT
GUID-2229F9C4.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbRasterImage` | |
| 90 | Class version | |
| 10 | Insertion point (WCS) | APP: 3D point |
| 11 | U-vector of a single pixel (along the visual bottom) (WCS) | |
| 12 | V-vector of a single pixel (along the visual left side) (WCS) | |
| 13 | Image size in pixels (U, V) | APP: 2D point |
| 340 | Hard reference to the imagedef object | |
| 70 | Display properties: 1 show image; 2 show when not aligned with the screen; 4 use clipping boundary; 8 transparency on | |
| 280 | Clipping state: 0 off; 1 on | |
| 281 / 282 | Brightness / contrast (0-100) | Default 50 / 50 |
| 283 | Fade (0-100) | Default 0 |
| 360 | Hard reference to the imagedef_reactor object | |
| 71 | Clip boundary type: 1 rectangular; 2 polygonal | |
| 91 | Number of clip boundary vertices | |
| 14 | Clip boundary vertex (OCS), repeats | Rectangular: 2 opposite corners (default (-0.5,-0.5), (size.x-0.5, size.y-0.5)). Polygonal: 3 or more, in sequence |

---

## TABLE ENTRY COMMON
Not a DXF entity name; these codes are shared by the table sections below (GUID-5926A569, GUID-8427DD38).

| Code | Meaning | Notes |
|---|---|---|
| -1 | APP: entity name | Changes each session |
| 0 | Entity type (table name, e.g. "LAYER") | |
| 5 | Handle (all except DIMSTYLE) | Cannot be set or changed from LISP |
| 105 | Handle (DIMSTYLE only) | Cannot be set or changed from LISP |
| 102/330, 102/360 | ACAD_REACTORS / ACAD_XDICTIONARY groups | Optional |
| 330 | Soft pointer to owner object | |
| 100 | Subclass marker `AcDbSymbolTableRecord` | Then the record-specific marker |

Group 70 of table entries is ignored by entmod and entmake (AutoLISP "Symbol Tables").

## LAYER
GUID-D94802B0.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbLayerTableRecord` | |
| 2 | Layer name | |
| 70 | Standard flags (bit-coded): 1 frozen (otherwise thawed); 2 frozen by default in new viewports; 4 locked; 16 xref-dependent; 32 xref-dependent and resolved (with 16); 64 referenced by an entity at the last edit (can be ignored) | Ignored by entmod/entmake (AutoLISP guide) |
| 62 | Color number; **negative = layer is off** | |
| 6 | Linetype name | Xref-dependent layers write CONTINUOUS in DXF |
| 290 | Plotting flag: 0 = do not plot | |
| 370 | Lineweight enum value | |
| 390 | Hard pointer to the PlotStyleName object | |
| 347 | Hard pointer to the Material object | |

The AutoLISP FAQ says LAYER entries cannot be modified except for xdata, but can be renamed.

## LTYPE
GUID-F57A316C.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbLinetypeTableRecord` | |
| 2 | Linetype name | CONTINUOUS cannot be modified or renamed |
| 70 | Standard flags: 16 / 32 / 64 (xref-dependent / resolved / referenced) | |
| 3 | Descriptive text | |
| 72 | Alignment code, always 65 ("A") | |
| 73 | Number of linetype elements | |
| 40 | Total pattern length | |
| 49 | Dash, dot or space length (one per element) | |
| 74 | Complex element type (one per element; default 0): bit 1 = 50 is an absolute rotation; 2 = embedded text; 4 = embedded shape | |
| 75 | Shape number if 74 = shape; 0 if text; omitted if 74 = 0 | |
| 340 | Pointer to the STYLE object (one per element if 74 > 0) | |
| 46 | Scale | Optional, repeats |
| 50 | Rotation in radians (relative or absolute) of the shape or text | |
| 44 / 45 | X / Y offset | Optional, repeat |
| 9 | Text string (if 74 = 2) | |

Codes 74, 75, 340, 46, 50, 44, 45 and 9 are **not returned by tblsearch/tblnext**: use `(entget (tblobjname "LTYPE" name))`.

## STYLE
GUID-EF68AF7C.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbTextStyleTableRecord` | |
| 2 | Style name | STANDARD cannot be renamed |
| 70 | Flags: 1 = entry describes a shape; 4 = vertical text; 16 / 32 / 64 as other tables | Shape-file LOAD entries have bit 1 set and only 3 is meaningful |
| 40 | Fixed text height (0 = not fixed) | |
| 41 | Width factor | |
| 50 | Oblique angle | |
| 71 | Text generation flags: 2 backward; 4 upside down | |
| 42 | Last height used | |
| 3 | Primary font file name | |
| 4 | Bigfont file name (blank if none) | |
| 1071 | (xdata) TrueType font pitch and family, character set, italic and bold flags | |

## BLOCK_RECORD
GUID-A1FD1934.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbBlockTableRecord` | |
| 2 | Block name | *MODEL_SPACE / *PAPER_SPACE cannot be renamed |
| 340 | Hard pointer to the associated LAYOUT object | |
| 70 | Block insertion units | |
| 280 | Block explodability | |
| 281 | Block scalability | |
| 310 | DXF: binary bitmap preview | Optional |
| 1001/1000/1002/1070 | Xdata "ACAD" / "DesignCenter Data" / { / DesignCenter version, then insert units / } | Optional |

Insert units (1070): 0 unitless; 1 inches; 2 feet; 3 miles; 4 mm; 5 cm; 6 m; 7 km; 8 microinches; 9 mils; 10 yards; 11 angstroms; 12 nanometers; 13 microns; 14 decimeters; 15 decameters; 16 hectometers; 17 gigameters; 18 AU; 19 light years; 20 parsecs; 21 US survey feet; 22 US survey inch; 23 US survey yard; 24 US survey mile.

## BLOCK
GUID-66D32572. Starts a block definition (entmake BLOCK ... entities ... ENDBLK).

| Code | Meaning | Notes |
|---|---|---|
| 0 | Entity type (BLOCK) | |
| 5 | Handle | |
| 102 | Application-defined group | Optional |
| 330 | Soft pointer to owner object | |
| 100 | Subclass marker `AcDbEntity` | |
| 8 | Layer name | |
| 100 | Subclass marker `AcDbBlockBegin` | |
| 2 | Block name | |
| 70 | Block-type flags (bit-coded): 0 none; 1 anonymous (hatch, assoc dim, internal, application); 2 has non-constant attdefs; 4 xref; 8 xref overlay; 16 externally dependent; 32 resolved xref or dependent (ignored on input); 64 referenced xref (ignored on input) | |
| 10 | Base point | APP: 3D point |
| 3 | Block name | |
| 1 | Xref path name | |
| 4 | Block description | Optional |

The UCS in effect when the block is defined becomes the WCS of its entities, shifted to the base point. *Model_Space, *Paper_Space, *Paper_Space0 ... always exist (first layout = *Paper_Space, second = *Paper_Space0, and so on). AutoLISP: tblnext/tblsearch "BLOCK" returns -2 = the first entity of the definition (ENDBLK if empty). Block definitions cannot be nested or self-referencing.

## ENDBLK
GUID-27F7CC8A.

| Code | Meaning | Notes |
|---|---|---|
| 0 | Entity type (ENDBLK) | |
| 5 | Handle | |
| 102 | Application-defined group | Optional |
| 330 | Soft pointer to owner object | |
| 100 | Subclass marker `AcDbEntity` | |
| 8 | Layer name | |
| 100 | Subclass marker `AcDbBlockEnd` | |

## DICTIONARY
GUID-40B92C63. Common object codes (GUID-6939D69E): 0 type, 5 handle, 102 groups, 330 owner.

| Code | Meaning | Notes |
|---|---|---|
| 0 | Object name (DICTIONARY) | |
| 5 | Handle | |
| 102/330/102 | `{ACAD_REACTORS`, soft pointer to owner dictionary, `}` | |
| 330 | Soft pointer to owner object | |
| 100 | Subclass marker `AcDbDictionary` | |
| 280 | Hard-owner flag: 1 = elements are hard-owned | |
| 281 | Duplicate record cloning: 0 n/a; 1 keep existing; 2 use clone; 3 `<xref>$0$<name>`; 4 `$0$<name>`; 5 unmangle name | |
| 3 | Entry name (one per entry) | Optional |
| 350 | Soft-owner ID of the entry object (one per entry) | Optional |

The "ACAD_" prefix is reserved for AutoCAD. LISP: entries are read-only through entmod; query with dictsearch/dictnext, rename keys with dictrename; entries may be removed by passing the entry object's ename to entdel. Entries starting with ACAD* cannot be renamed.

## XRECORD
GUID-24668FAF.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbXrecord` | Included in the AutoLISP entmakex example |
| 280 | Duplicate record cloning (0-5 as DICTIONARY 281) | |
| 1-369 (except 5 and 105) | Free application data | Not limited by size or order (unlike xdata) |

AutoLISP example: `(entmakex '((0 . "XRECORD") (100 . "AcDbXrecord") (1 . "text") (10 1.0 2.0 0.0) (40 . 3.14159) (62 . 1) (70 . 180)))`, then `(dictadd (namedobjdict) "XRECLIST" ename)`.

## LAYOUT
GUID-433D25BF.

| Code | Meaning | Notes |
|---|---|---|
| 0 | Object name (LAYOUT) | |
| 5 | Handle | |
| 102/330/102 | Reactors group (owner dictionary) | |
| 330 | Soft pointer to owner object | |
| 100 | Subclass marker `AcDbPlotSettings` | Followed by the PLOTSETTINGS codes |
| 100 | Subclass marker `AcDbLayout` | |
| 1 | Layout name | |
| 70 | Flags: 1 = PSLTSCALE value when current; 2 = LIMCHECK value when current | |
| 71 | Tab order ("Model" is always first) | |
| 10 / 11 | Min / max limits (LIMMIN / LIMMAX) | 2D |
| 12 | Insertion base (INSBASE) | 3D |
| 14 / 15 | Min / max extents (EXTMIN / EXTMAX) | 3D |
| 146 | Elevation | |
| 13 / 16 / 17 | UCS origin / X axis / Y axis | |
| 76 | Orthographic UCS: 0 not; 1 top; 2 bottom; 3 front; 4 back; 5 left; 6 right | |
| 330 | ID of this layout's paper space BLOCK_RECORD | |
| 331 | ID of the viewport last active in this layout | |
| 345 / 346 | Named UCS / base UCS record | |
| 333 | Shade plot ID | |

## DIMSTYLE
GUID-F2FAD36F. Main codes. The handle is **105** (not 5). These codes are also used in "ACAD" DSTYLE override xdata.

| Code | Meaning | Notes |
|---|---|---|
| 100 | Subclass marker `AcDbDimStyleTableRecord` | |
| 2 | Dimension style name | STANDARD cannot be renamed |
| 70 | Standard flags 16 / 32 / 64 | |
| 3 / 4 | DIMPOST / DIMAPOST | |
| 5 / 6 / 7 | DIMBLK / DIMBLK1 / DIMBLK2 | Obsolete (now object IDs 342-344) |
| 40 | DIMSCALE | |
| 41 / 42 / 43 / 44 | DIMASZ / DIMEXO / DIMDLI / DIMEXE | |
| 45 / 46 / 47 / 48 | DIMRND / DIMDLE / DIMTP / DIMTM | |
| 140 / 141 / 142 | DIMTXT / DIMCEN / DIMTSZ | |
| 143 / 144 / 145 / 146 / 147 / 148 | DIMALTF / DIMLFAC / DIMTVP / DIMTFAC / DIMGAP / DIMALTRND | |
| 71 / 72 / 73 / 74 | DIMTOL / DIMLIM / DIMTIH / DIMTOH | |
| 75 / 76 / 77 / 78 / 79 | DIMSE1 / DIMSE2 / DIMTAD / DIMZIN / DIMAZIN | |
| 170-175 | DIMALT / DIMALTD / DIMTOFL / DIMSAH / DIMTIX / DIMSOXD | |
| 176 / 177 / 178 / 179 | DIMCLRD / DIMCLRE / DIMCLRT / DIMADEC | |
| 270 | DIMUNIT | Obsolete (use DIMLUNIT, DIMFRAC) |
| 271-279 | DIMDEC / DIMTDEC / DIMALTU / DIMALTTD / DIMAUNIT / DIMFRAC / DIMLUNIT / DIMDSEP / DIMTMOVE | |
| 280-289 | DIMJUST / DIMSD1 / DIMSD2 / DIMTOLJ / DIMTZIN / DIMALTZ / DIMALTTZ / DIMFIT (obsolete) / DIMUPT / DIMATFIT | |
| 340 | DIMTXSTY (handle of the STYLE) | |
| 341 / 342 / 343 / 344 | DIMLDRBLK / DIMBLK / DIMBLK1 / DIMBLK2 (handle of the BLOCK) | |
| 371 / 372 | DIMLWD / DIMLWE (lineweight enum) | |

---

### Topic GUIDs
DXF (`.../2027/ENU/AutoCAD-DXF/files/GUID-*.htm`): value types 2553CF98-44F6-4828-82DD-FE3BC7448113; numerical order 3F0380A5-1C15-464D-BC66-2C5F094BCFB9; common entity 3610039E-27D1-4E23-B6D3-7E60B22BB5BD; extended data A2A628B0-3699-4740-A215-C560E7242F63; OCS D99F1509-E4E4-47A3-8691-92EA07DC88F5; dimstyle overrides 6A4C31C0-4988-499C-B5A4-15582E433B0F; LINE FCEF5726-53AE-4C43-B4EA-C84EB8686A66; LWPOLYLINE 748FC305-F3F2-4F74-825A-61F04D757A50; POLYLINE ABF6B778-BE20-4B49-9B58-A94E64CEFFF3; VERTEX 0741E831-599E-4CBF-91E1-8ADBCFD6556D; ARC 0B14D8F1-0EBA-44BF-9108-57D8CE614BC8; CIRCLE 8663262B-222C-414D-B133-4A8506A27C18; ELLIPSE 107CB04F-AD4D-4D2F-8EC9-AC90888063AB; TEXT 62E5383D-8A14-47B4-BFC4-35824CAE8363; MTEXT 5E5DB93B-F8D3-4433-ADF7-E92E250D2BAB; INSERT 28FA4CFB-9D5E-4880-9F11-36C97578252F; ATTRIB 7DD8B495-C3F8-48CD-A766-14F9D7D0DD9B; ATTDEF F0EA099B-6F88-4BCC-BEC7-247BA64838A4; DIMENSION 239A1BDD-7459-4BB9-8DD7-08EC79BF1EB0; common dimension EDD54EAC-A339-4EBA-AEA6-EC8066505E2B; aligned 7A123D5D-AC98-4A9A-A8CF-1A7EF5030418; linear F0004556-493C-48D5-8619-61D6ADF05C04; radial 165A992D-9017-4C1E-B8CC-E70A17191BFE; angular 09821B78-9F8E-43BA-82F2-8C931485EDC9; ordinate 72F01288-0D63-43E8-8179-8CE3BA544C40; HATCH C6C71CED-CE0F-4184-82A5-07AD6241F15B; boundary path DC5215D6-E73F-4DFF-8BE9-01CA9610FAEE; LEADER 396B2369-F89F-47D7-8223-8B7FB794F9F3; MLEADER 72D20B8C-0F5E-4993-BEB7-0FCF94F32BE0; common MLeader 69B9139A-48B4-48A5-B3CF-A3233ABFBE49; MLeader context EC56D0DE-026D-46AB-87B1-9692393B0C22; VIEWPORT 2602B0FB-02E4-4B9A-B03C-B1D904753D34; OLE2FRAME 77747CE6-82C6-4452-97ED-4CEEB38BE960; SPLINE E1F884F8-AA90-4864-A215-3182D47A9C74; SOLID E0C5F04E-D0C5-48F5-AC09-32733E8848F2; POINT 9C6AD32D-769D-4213-85A4-CA9CCB5C5317; WIPEOUT 2229F9C4-3C80-4C67-9EDA-45ED684808DC; symbol table common 8427DD38-7B1F-4B7F-BF66-21ADD1F41295; table entry common 5926A569-3E40-4ED2-AE06-6ACCE0EFC813; LAYER D94802B0-8BE8-4AC9-8054-17197688AFDB; LTYPE F57A316C-94A2-416C-8280-191E34B182AC; STYLE EF68AF7C-13EF-45A1-8175-ED6CE66C8FC9; BLOCK_RECORD A1FD1934-7EF5-4D35-A4B0-F8AE54A9A20A; BLOCK 66D32572-005A-4E23-8B8B-8726E8C14302; ENDBLK 27F7CC8A-E340-4C7F-A77F-5AF139AD502D; common object 6939D69E-04CB-4F4C-87B2-67BC540FCF58; DICTIONARY 40B92C63-26F0-485B-A9C2-B349099B26D0; XRECORD 24668FAF-AE03-41AE-AFA4-276C3692827F; LAYOUT 433D25BF-655D-4697-834E-C666EDFD956D; DIMSTYLE F2FAD36F-0CE3-4943-9DAD-A9BCD2AE81DA.
AutoLISP (`.../2027/ENU/AutoCAD-AutoLISP/files/GUID-*.htm`): adding entity (entmake) DFDAE6CD-E753-4D01-9D9B-4D1F66B1DE6E; modifying (entmod) 125DC058-BBAA-4CA9-A203-53F0A27B87D0; complex entities 7DA0E2BA-963F-4E41-AF33-C6B9D89D4688; obtaining entity info (entget) EF8C5586-BA1F-4EA6-BAD1-6BA792B601F0; graphics screen/entupd 34D3F07F-B93F-4316-B683-6D42E3436E27; deleting 7DC692D0-4FD4-4931-93C3-EFC616397E4C; symbol tables 7B91F774-B0CD-4F5B-9FFA-1130443E659B; table FAQ 726335A9-C1D4-445B-B395-51098A03F7FD; non-graphical objects 984A6964-E801-4C22-8E41-BF3D05CD122F; dictionaries 24E52678-513E-4322-8070-B23C8945DC3D; xrecords FA5F2E08-24F5-4947-A470-6CA84E404F2A; xdata codes 3481BF7B-73CB-4FD5-B421-C25BE92C6D56; retrieving xdata 1A2A0518-E9DB-462E-925E-32181D96CE4D; xdata memory 48A12BAA-E785-4E2B-B7A7-E3B976E75891; filter lists 7BE77062-C359-4D01-915B-69CF672C653B; relational tests 877CEEA5-0D88-48DF-8450-B9421031A1B7; logical grouping 5CB54129-22A1-42B9-B97C-2D2F5597F90E; wildcards 11EA0ABD-AE29-4DD7-92A5-DFF6EC1C9799; xdata filtering 84C3CDF5-DF88-4473-A10D-C7778EE037C8.
