# AutoCAD 2027 ActiveX: 3DFace .. DimOrdinate

Generated from the type libraries by scripts/tlb_to_md.py. M = method, P = property.
vla- call: the object is always the first argument; 'name<out T> = pass a quoted symbol the call sets.
A member is listed only on the interface that declares it: walk `Inherits:` (IAcadLine -> IAcadEntity -> IAcadObject).

[INTERFACE] IAcad3DFace
Object: Acad3DFace
Inherits: IAcadEntity
Doc: AutoCAD 3dFace Interface
  Methods
    M GetInvisibleEdge  (vla-GetInvisibleEdge obj Index<long>) -> VARIANT_BOOL  ; Gets the visibility status for the edge.
    M SetInvisibleEdge  (vla-SetInvisibleEdge obj Index<long> State<VARIANT_BOOL>)  ; Sets the visibility of the edge.
  Properties
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the current vertex of the 3D face
    P VisibilityEdge1 : VARIANT_BOOL  [get/put]  vla-get-VisibilityEdge1 / vla-put-VisibilityEdge1  ; Determines whether 3DFace Edge 1 is visible or hidden
    P VisibilityEdge2 : VARIANT_BOOL  [get/put]  vla-get-VisibilityEdge2 / vla-put-VisibilityEdge2  ; Determines whether 3DFace Edge 2 is visible or hidden
    P VisibilityEdge3 : VARIANT_BOOL  [get/put]  vla-get-VisibilityEdge3 / vla-put-VisibilityEdge3  ; Determines whether 3DFace Edge 3 is visible or hidden
    P VisibilityEdge4 : VARIANT_BOOL  [get/put]  vla-get-VisibilityEdge4 / vla-put-VisibilityEdge4  ; Determines whether 3DFace Edge 4 is visible or hidden

[INTERFACE] IAcad3DPolyline
Object: Acad3DPolyline
Inherits: IAcadEntity
Doc: AutoCAD 3dPolyline Interface
  Methods
    M AppendVertex  (vla-AppendVertex obj vertex<VARIANT>)  ; Appends a vertex to the 3dPolyline.
    M Explode  (vla-Explode obj) -> VARIANT  ; Explodes the 3dPolyline.
  Properties
    P Closed : VARIANT_BOOL  [get/put]  vla-get-Closed / vla-put-Closed  ; Determines whether the 3D polyline is open or closed
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the current vertex of the 3D Polyline
    P Length : double  [get READ-ONLY]  vla-get-Length  ; Specifies the length of the 3D polyline
    P Type : Ac3DPolylineType  [get/put]  vla-get-Type / vla-put-Type  ; Specifies the type of line or surface curve fitting

[INTERFACE] IAcad3DSolid
Object: Acad3DSolid
Inherits: IAcadEntity
Doc: AutoCAD 3dSolid Interface
  Methods
    M Boolean  (vla-Boolean obj Operation<AcBooleanType> SolidObject<IAcad3DSolid*>)  ; Performs a boolean operation against another 3dsolid.
    M CheckInterference  (vla-CheckInterference obj Object<IAcad3DSolid*> CreateInterferenceSolid<VARIANT_BOOL> 'SolidsInterfere<out VARIANT_BOOL>) -> IAcad3DSolid  ; Check interference for the 3dsolid object.
    M SectionSolid  (vla-SectionSolid obj Point1<VARIANT> Point2<VARIANT> point3<VARIANT>) -> IAcadRegion  ; Create a section of the 3dsolid given three points that define the plane. Returns the Section as a Region object
    M SliceSolid  (vla-SliceSolid obj Point1<VARIANT> Point2<VARIANT> point3<VARIANT> Negative<VARIANT_BOOL>) -> IAcad3DSolid  ; Create a slice of the 3dsolid given three points that define the plane. Returns the resulting array of 3dSolid object.
  Properties
    P Centroid : VARIANT  [get READ-ONLY]  vla-get-Centroid  ; Gets the center of area or mass for a region or solid
    P History : VARIANT_BOOL  [get/put]  vla-get-History / vla-put-History  ; Specifies whether history is saved
    P MomentOfInertia : VARIANT  [get READ-ONLY]  vla-get-MomentOfInertia  ; Gets the moment of inertia for the solid
    P Position : VARIANT  [get/put]  vla-get-Position / vla-put-Position  ; Specifies the X, Y, Z coordinate for center of the base or center of the solid
    P PrincipalDirections : VARIANT  [get READ-ONLY]  vla-get-PrincipalDirections  ; Gets the principal directions of the solid or region
    P PrincipalMoments : VARIANT  [get READ-ONLY]  vla-get-PrincipalMoments  ; Gets the principal moments property of the solid or region
    P ProductOfInertia : VARIANT  [get READ-ONLY]  vla-get-ProductOfInertia  ; Gets the product of inertia of the solid or region
    P RadiiOfGyration : VARIANT  [get READ-ONLY]  vla-get-RadiiOfGyration  ; Gets the radius of gyration of the solid or region
    P ShowHistory : VARIANT_BOOL  [get/put]  vla-get-ShowHistory / vla-put-ShowHistory  ; Specifies whether to show history of the solid
    P SolidType : BSTR  [get READ-ONLY]  vla-get-SolidType  ; Indicates the type of solid
    P Volume : double  [get READ-ONLY]  vla-get-Volume  ; Gets the volume of the solid

[EVENTS] _DAcadApplicationEvents
Note: event (source) interface: these are reactor callbacks, NOT vla- functions you can call.
Doc: Event interface for AcadApplication
  Methods
    M AppActivate  (vla-AppActivate obj)  ; Triggered just before the main application window is activated
    M AppDeactivate  (vla-AppDeactivate obj)  ; Triggered just before the main application window is deactivated
    M ARXLoaded  (vla-ARXLoaded obj AppName<BSTR>)  ; Triggered when an ObjectARX application has been loaded
    M ARXUnloaded  (vla-ARXUnloaded obj AppName<BSTR>)  ; Triggered when an ObjectARX application has been unloaded
    M BeginCommand  (vla-BeginCommand obj CommandName<BSTR>)  ; Triggered immediately after a command is issued, but before it completes
    M BeginFileDrop  (vla-BeginFileDrop obj FileName<BSTR> 'Cancel<out VARIANT_BOOL>)  ; Triggered when a file is dropped on the main application window
    M BeginLisp  (vla-BeginLisp obj FirstLine<BSTR>)  ; Triggered immediately after AutoCAD receives a request to evaluate a LISP expression
    M BeginModal  (vla-BeginModal obj)  ; Triggered just before a modal dialog is displayed
    M BeginOpen  (vla-BeginOpen obj 'FileName<out BSTR>)  ; Triggered immediately after AutoCAD receives a request to open an existing drawing
    M BeginPlot  (vla-BeginPlot obj DrawingName<BSTR>)  ; Triggered immediately after AutoCAD receives a request to print a drawing
    M BeginQuit  (vla-BeginQuit obj 'Cancel<out VARIANT_BOOL>)  ; Triggered just before an AutoCAD session ends or a document closes
    M BeginSave  (vla-BeginSave obj FileName<BSTR>)  ; Triggered immediately after AutoCAD receives a request to save the drawing
    M EndCommand  (vla-EndCommand obj CommandName<BSTR>)  ; Triggered immediately after a command completes
    M EndLisp  (vla-EndLisp obj)  ; Triggered upon completion of evaluating a LISP expression
    M EndModal  (vla-EndModal obj)  ; Triggered just after a modal dialog is dismissed
    M EndOpen  (vla-EndOpen obj FileName<BSTR>)  ; Triggered immediately after AutoCAD finishes opening an existing drawing
    M EndPlot  (vla-EndPlot obj DrawingName<BSTR>)  ; Triggered after a document has been sent to the printer
    M EndSave  (vla-EndSave obj FileName<BSTR>)  ; Triggered when AutoCAD has finished saving the drawing
    M LispCancelled  (vla-LispCancelled obj)  ; Triggered when the evaluation of a LISP expression is cancelled
    M NewDrawing  (vla-NewDrawing obj)  ; Triggered just before a new drawing is created
    M SysVarChanged  (vla-SysVarChanged obj SysvarName<BSTR> newVal<VARIANT>)  ; Triggered when the value of a system variable is changed
    M WindowChanged  (vla-WindowChanged obj WindowState<AcWindowState>)  ; Triggered when there is a change to the application or document windows
    M WindowMovedOrResized  (vla-WindowMovedOrResized obj HWNDFrame<long> bMoved<VARIANT_BOOL>)  ; Happens when the main frame window is moved or resized

[EVENTS] _DAcadDocumentEvents
Note: event (source) interface: these are reactor callbacks, NOT vla- functions you can call.
Doc: Event interface for ThisDrawing
  Methods
    M Activate  (vla-Activate obj)  ; Triggered when a document window is activated
    M BeginClose  (vla-BeginClose obj)  ; Triggered immediately after AutoCAD receives a request to close a drawing
    M BeginCommand  (vla-BeginCommand obj CommandName<BSTR>)  ; Triggered immediately after a command is issued, but before it completes
    M BeginDocClose  (vla-BeginDocClose obj 'Cancel<out VARIANT_BOOL>)  ; Triggered before the BeginClose event. This is an opportunity to cancel the closing process
    M BeginDoubleClick  (vla-BeginDoubleClick obj PickPoint<VARIANT>)  ; Triggered after the user double-clicks on an object in the drawing
    M BeginLisp  (vla-BeginLisp obj FirstLine<BSTR>)  ; Triggered immediately after AutoCAD receives a request to evaluate a LISP expression
    M BeginPlot  (vla-BeginPlot obj DrawingName<BSTR>)  ; Triggered immediately after AutoCAD receives a request to print a drawing
    M BeginRightClick  (vla-BeginRightClick obj PickPoint<VARIANT>)  ; Triggered after the user right-clicks on the drawing window
    M BeginSave  (vla-BeginSave obj FileName<BSTR>)  ; Triggered immediately after AutoCAD receives a request to save the drawing
    M BeginShortcutMenuCommand  (vla-BeginShortcutMenuCommand obj 'ShortcutMenu<out IAcadPopupMenu> Command<BSTR>)  ; Triggered after the user right-clicks on the drawing window, and before the shortcut menu appears in command mode
    M BeginShortcutMenuDefault  (vla-BeginShortcutMenuDefault obj 'ShortcutMenu<out IAcadPopupMenu>)  ; Triggered after the user right-clicks on the drawing window, and before the shortcut menu appears in default mode
    M BeginShortcutMenuEdit  (vla-BeginShortcutMenuEdit obj 'ShortcutMenu<out IAcadPopupMenu> 'SelectionSet<out IAcadSelectionSet>)  ; Triggered after the user right-clicks on the drawing window, and before the shortcut menu appears in edit mode
    M BeginShortcutMenuGrip  (vla-BeginShortcutMenuGrip obj 'ShortcutMenu<out IAcadPopupMenu>)  ; Triggered after the user right-clicks on the drawing window, and before the shortcut menu appears in grip mode
    M BeginShortcutMenuOsnap  (vla-BeginShortcutMenuOsnap obj 'ShortcutMenu<out IAcadPopupMenu>)  ; Triggered after the user right-clicks on the drawing window, and before the shortcut menu appears in osnap mode
    M Deactivate  (vla-Deactivate obj)  ; Triggered when the drawing window is deactivated
    M EndCommand  (vla-EndCommand obj CommandName<BSTR>)  ; Triggered immediately after a command completes
    M EndLisp  (vla-EndLisp obj)  ; Triggered upon completion of evaluating a LISP expression
    M EndPlot  (vla-EndPlot obj DrawingName<BSTR>)  ; Triggered after a document has been sent to the printer
    M EndSave  (vla-EndSave obj FileName<BSTR>)  ; Triggered when AutoCAD has finished saving the drawing
    M EndShortcutMenu  (vla-EndShortcutMenu obj 'ShortcutMenu<out IAcadPopupMenu>)  ; Triggered after the shortcut menu appears
    M LayoutSwitched  (vla-LayoutSwitched obj LayoutName<BSTR>)  ; Triggered after the user switches to a different layout
    M LispCancelled  (vla-LispCancelled obj)  ; Triggered when the evaluation of a LISP expression is cancelled
    M ObjectAdded  (vla-ObjectAdded obj Object<IDispatch*>)  ; Triggered when an object has been added to the drawing
    M ObjectErased  (vla-ObjectErased obj ObjectID<LONG_PTR>)  ; Triggered when an object has been erased from the drawing
    M ObjectModified  (vla-ObjectModified obj Object<IDispatch*>)  ; Triggered when an object in the drawing has been modified
    M SelectionChanged  (vla-SelectionChanged obj)  ; Triggered when the current pickfirst selection set changes
    M WindowChanged  (vla-WindowChanged obj WindowState<AcWindowState>)  ; Triggered when there is a change to the application or document windows
    M WindowMovedOrResized  (vla-WindowMovedOrResized obj HWNDFrame<LONG_PTR> bMoved<VARIANT_BOOL>)  ; Happens when the drawing window is moved or resized.

[INTERFACE] IAcadAcCmColor
Object: AcadAcCmColor
Doc: AutoCAD AcCmColor Interface
  Methods
    M Delete  (vla-Delete obj)  ; Deletes the true color.
    M SetColorBookColor  (vla-SetColorBookColor obj BookName<BSTR> ColorName<BSTR>)  ; Sets the color to a color from a color book.
    M SetNames  (vla-SetNames obj ColorName<BSTR> BookName<BSTR>)  ; Specifies the color name and book name of the color.
    M SetRGB  (vla-SetRGB obj Red<long> Green<long> Blue<long>)  ; Specifies the RGB values of the true color.
  Properties
    P Blue : long  [get READ-ONLY]  vla-get-Blue  ; Specifies the blue component of the true color.
    P BookName : BSTR  [get READ-ONLY]  vla-get-BookName  ; Specifies the book name (if any) of the color.
    P ColorIndex : AcColor  [get/put]  vla-get-ColorIndex / vla-put-ColorIndex  ; Specifies the color index.
    P ColorMethod : AcColorMethod  [get/put]  vla-get-ColorMethod / vla-put-ColorMethod  ; Specifies the color method.
    P ColorName : BSTR  [get READ-ONLY]  vla-get-ColorName  ; Specifies the name (if any) of the color.
    P EntityColor : long  [get/put]  vla-get-EntityColor / vla-put-EntityColor  ; Specifies the AcCmEntityColor of the true color.
    P Green : long  [get READ-ONLY]  vla-get-Green  ; Specifies the green component of the true color.
    P Red : long  [get READ-ONLY]  vla-get-Red  ; Specifies the red component of the true color.

[INTERFACE] IAcadApplication
Object: AcadApplication
Doc: An instance of the AutoCAD application
  Methods
    M Eval  (vla-Eval obj Expression<BSTR>)  ; Evaluates an expression in VBA
    M GetAcadState  (vla-GetAcadState obj) -> IAcadState  ; Retrieves an AcadState object.
    M GetInterfaceObject  (vla-GetInterfaceObject obj ProgID<BSTR>) -> IDispatch  ; Accepts a program ID and attempts to load it into AutoCAD as an in-process server
    M ListArx  (vla-ListArx obj) -> VARIANT  ; Gets the currently loaded AutoCAD ARX applications
    M LoadArx  (vla-LoadArx obj Name<BSTR>)  ; Loads the specified AutoCAD ARX application
    M LoadDVB  (vla-LoadDVB obj Name<BSTR>)  ; Loads the specified AutoCAD VBA project file
    M Quit  (vla-Quit obj)  ; Closes the drawing file and exits the AutoCAD application
    M RunMacro  (vla-RunMacro obj MacroPath<BSTR>)  ; Runs a VBA macro from the Application object
    M UnloadArx  (vla-UnloadArx obj Name<BSTR>)  ; Unloads the specified AutoCAD ARX application
    M UnloadDVB  (vla-UnloadDVB obj Name<BSTR>)  ; Unloads the specified AutoCAD VBA project file
    M Update  (vla-Update obj)  ; Updates the object to the drawing screen
    M Zoom  (vla-Zoom obj Type<long> vParams<VARIANT*>)  ; Zoom
    M ZoomAll  (vla-ZoomAll obj)  ; Zooms the current viewport to display the entire drawing
    M ZoomCenter  (vla-ZoomCenter obj Center<VARIANT> Magnify<double>)  ; Zooms the current viewport to a specified center point and magnification
    M ZoomExtents  (vla-ZoomExtents obj)  ; Zooms the current viewport to the drawing extents
    M ZoomPickWindow  (vla-ZoomPickWindow obj)  ; Zooms the current viewport to a window defined by points picked on the screen
    M ZoomPrevious  (vla-ZoomPrevious obj)  ; Zooms the current viewport to its previous extents
    M ZoomScaled  (vla-ZoomScaled obj scale<double> ScaleType<AcZoomScaleType>)  ; Zooms the current viewport to given scale factor
    M ZoomWindow  (vla-ZoomWindow obj LowerLeft<VARIANT> UpperRight<VARIANT>)  ; Zooms the current viewport to the area specified by two opposite corners of a rectangle
  Properties
    P ActiveDocument : IAcadDocument  [get/put]  vla-get-ActiveDocument / vla-put-ActiveDocument  ; Specifies the active document (drawing file)
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Caption : BSTR  [get READ-ONLY]  vla-get-Caption  ; Gets the text that the user sees displayed for the application or a menu item
    P Documents : IAcadDocuments  [get READ-ONLY]  vla-get-Documents  ; Returns the documents collection.
    P FullName : BSTR  [get READ-ONLY]  vla-get-FullName  ; Gets the name of the application or document, including the path
    P Height : long  [get/put]  vla-get-Height / vla-put-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P HWND : LONG_PTR  [get READ-ONLY]  vla-get-HWND  ; Gets the window handle of the application window frame
    P LocaleId : long  [get READ-ONLY]  vla-get-LocaleId  ; Gets the locale ID of the current AutoCAD session
    P MenuBar : IAcadMenuBar  [get READ-ONLY]  vla-get-MenuBar  ; Gets the MenuBar object for the session
    P MenuGroups : IAcadMenuGroups  [get READ-ONLY]  vla-get-MenuGroups  ; Gets the MenuGroups collection for the session
    P Name : BSTR  [get READ-ONLY]  vla-get-Name  ; Specifies the name of the object
    P Path : BSTR  [get READ-ONLY]  vla-get-Path  ; Gets the path of the document, application, or external reference
    P Preferences : IAcadPreferences  [get READ-ONLY]  vla-get-Preferences  ; Gets the Preferences object
    P StatusId[VportObj<IDispatch*>] : VARIANT_BOOL  [get READ-ONLY]  vla-get-StatusId  ; Gets the current active status of the viewport
    P VBE : IDispatch  [get READ-ONLY]  vla-get-VBE  ; Gets the VBAIDE extensibility object
    P Version : BSTR  [get READ-ONLY]  vla-get-Version  ; Gets the version of the AutoCAD application you are using
    P Visible : VARIANT_BOOL  [get/put]  vla-get-Visible / vla-put-Visible  ; Specifies the visibility of an object or the application
    P Width : long  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window
    P WindowLeft : long  [get/put]  vla-get-WindowLeft / vla-put-WindowLeft  ; Specifies the left edge of the application window
    P WindowState : AcWindowState  [get/put]  vla-get-WindowState / vla-put-WindowState  ; Specifies the state of the application or document window
    P WindowTop : long  [get/put]  vla-get-WindowTop / vla-put-WindowTop  ; Specifies the top edge of the application window

[INTERFACE] IAcadArc
Object: AcadArc
Inherits: IAcadEntity
Doc: AutoCAD Arc Interface
  Methods
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Offsets the arc to the given side
  Properties
    P ArcLength : ACAD_DISTANCE  [get READ-ONLY]  vla-get-ArcLength  ; Specifies the arc length of the arc
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the arc when implicitly closed with a line
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specify the X, Y, Z coordinate of the center point of the arc or use the Pick Point button to set X, Y, Z values simultaneously
    P EndAngle : ACAD_ANGLE  [get/put]  vla-get-EndAngle / vla-put-EndAngle  ; Specifies the end angle of the arc
    P EndPoint : VARIANT  [get READ-ONLY]  vla-get-EndPoint  ; Specify the X, Y, Z coordinate of the end point of the arc
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specify the X, Y, Z coordinate of the normal direction vector
    P Radius : double  [get/put]  vla-get-Radius / vla-put-Radius  ; Specifies the radius of the arc
    P StartAngle : ACAD_ANGLE  [get/put]  vla-get-StartAngle / vla-put-StartAngle  ; Specifies the start angle of the arc
    P StartPoint : VARIANT  [get READ-ONLY]  vla-get-StartPoint  ; Specify the X, Y, Z coordinate of the start point of the arc
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the arc
    P TotalAngle : ACAD_ANGLE  [get READ-ONLY]  vla-get-TotalAngle  ; Specifies the total angle of the arc

[INTERFACE] IAcadAttribute
Object: AcadAttribute
Inherits: IAcadEntity
Doc: AutoCAD Attribute Interface
  Methods
    M UpdateMTextAttribute  (vla-UpdateMTextAttribute obj)  ; Updates multiline MText
  Properties
    P Alignment : AcAlignment  [get/put]  vla-get-Alignment / vla-put-Alignment  ; Specifies both text height and text orientation by designating the endpoints of the baseline
    P Backward : VARIANT_BOOL  [get/put]  vla-get-Backward / vla-put-Backward  ; Determines whether the text is backward or not
    P Constant : VARIANT_BOOL  [get/put]  vla-get-Constant / vla-put-Constant  ; Specifies the constant mode of the attribute
    P FieldLength : long  [get/put]  vla-get-FieldLength / vla-put-FieldLength  ; Specifies the field length of the attribute
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the attribute
    P HorizontalAlignment : AcHorizontalAlignment  [get/put]  vla-get-HorizontalAlignment / vla-put-HorizontalAlignment  ; Specifies the horizontal alignment of the attribute
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specify the X, Y, Z coordinate of the insertion point of the text
    P Invisible : VARIANT_BOOL  [get/put]  vla-get-Invisible / vla-put-Invisible  ; Specifies the invisible mode of the attribute
    P LockPosition : VARIANT_BOOL  [get/put]  vla-get-LockPosition / vla-put-LockPosition  ; Specifies whether the attribute may be moved relative to the geometry in the block
    P Mode : long  [get/put]  vla-get-Mode / vla-put-Mode  ; Specifies the mode of the attribute definition
    P MTextAttribute : VARIANT_BOOL  [get/put]  vla-get-MTextAttribute / vla-put-MTextAttribute  ; Determines whether if the attribute is multiline
    P MTextAttributeContent : BSTR  [get/put]  vla-get-MTextAttributeContent / vla-put-MTextAttributeContent  ; Gets the multiline attribute content
    P MTextBoundaryWidth : double  [get/put]  vla-get-MTextBoundaryWidth / vla-put-MTextBoundaryWidth  ; Gets the width of text boundary of the Mtext
    P MTextDrawingDirection : AcDrawingDirection  [get/put]  vla-get-MTextDrawingDirection / vla-put-MTextDrawingDirection  ; Gets the drawing direction of the Mtext
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P ObliqueAngle : ACAD_ANGLE  [get/put]  vla-get-ObliqueAngle / vla-put-ObliqueAngle  ; Specifies the oblique angle of the attribute
    P Preset : VARIANT_BOOL  [get/put]  vla-get-Preset / vla-put-Preset  ; Specifies the preset mode of the attribute
    P PromptString : BSTR  [get/put]  vla-get-PromptString / vla-put-PromptString  ; Specifies the prompt string of the attribute
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the attribute
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the scale factor of the attribute
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the text style of the attribute
    P TagString : BSTR  [get/put]  vla-get-TagString / vla-put-TagString  ; Specifies the tag string of the attribute
    P TextAlignmentPoint : VARIANT  [get/put]  vla-get-TextAlignmentPoint / vla-put-TextAlignmentPoint  ; Specify the X, Y, Z alignment point of the attribute
    P TextGenerationFlag : long  [get/put]  vla-get-TextGenerationFlag / vla-put-TextGenerationFlag  ; Specifies the attribute text generation flag
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the attribute
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the attribute
    P UpsideDown : VARIANT_BOOL  [get/put]  vla-get-UpsideDown / vla-put-UpsideDown  ; Determines whether the text is upside down or not
    P Verify : VARIANT_BOOL  [get/put]  vla-get-Verify / vla-put-Verify  ; Specifies the verify mode of the attribute
    P VerticalAlignment : AcVerticalAlignment  [get/put]  vla-get-VerticalAlignment / vla-put-VerticalAlignment  ; Specifies the vertical alignment of the attribute.

[INTERFACE] IAcadAttributeReference
Object: AcadAttributeReference
Inherits: IAcadEntity
Doc: AutoCAD Attribute Reference Interface
  Methods
    M UpdateMTextAttribute  (vla-UpdateMTextAttribute obj)  ; Updates attribute reference from the multiline mtext and vice versa
  Properties
    P Alignment : AcAlignment  [get/put]  vla-get-Alignment / vla-put-Alignment  ; Specifies the alignment of the attribute reference
    P Backward : VARIANT_BOOL  [get/put]  vla-get-Backward / vla-put-Backward  ; Determines whether the text is backward and sets the text backward
    P Constant : VARIANT_BOOL  [get READ-ONLY]  vla-get-Constant  ; Specifies the constant mode of the attribute reference
    P FieldLength : long  [get/put]  vla-get-FieldLength / vla-put-FieldLength  ; Specifies the field length of the attribute reference
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the attribute reference
    P HorizontalAlignment : AcHorizontalAlignment  [get/put]  vla-get-HorizontalAlignment / vla-put-HorizontalAlignment  ; Specifies the horizontal alignment of the attribute reference
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specifies the insertion point of the text
    P Invisible : VARIANT_BOOL  [get/put]  vla-get-Invisible / vla-put-Invisible  ; Specifies the invisible mode of the attribute reference
    P LockPosition : VARIANT_BOOL  [get READ-ONLY]  vla-get-LockPosition  ; Specifies whether the attribute may be moved relative to the geometry in the block
    P MTextAttribute : VARIANT_BOOL  [get/put]  vla-get-MTextAttribute / vla-put-MTextAttribute  ; Determines whether if the attribute reference is multiline
    P MTextAttributeContent : BSTR  [get/put]  vla-get-MTextAttributeContent / vla-put-MTextAttributeContent  ; Gets the multiline attribute reference content
    P MTextBoundaryWidth : double  [get/put]  vla-get-MTextBoundaryWidth / vla-put-MTextBoundaryWidth  ; Gets the width of text boundary of the Mtext
    P MTextDrawingDirection : AcDrawingDirection  [get/put]  vla-get-MTextDrawingDirection / vla-put-MTextDrawingDirection  ; Gets the drawing direction of the Mtext
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the normal direction vector
    P ObliqueAngle : ACAD_ANGLE  [get/put]  vla-get-ObliqueAngle / vla-put-ObliqueAngle  ; Specifies the oblique angle of the attribute reference
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the attribute reference
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the scale factor of the attribute reference
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of the attribute reference
    P TagString : BSTR  [get/put]  vla-get-TagString / vla-put-TagString  ; Specifies the tag string of the attribute reference
    P TextAlignmentPoint : VARIANT  [get/put]  vla-get-TextAlignmentPoint / vla-put-TextAlignmentPoint  ; Specifies the alignment point of the attribute reference
    P TextGenerationFlag : long  [get/put]  vla-get-TextGenerationFlag / vla-put-TextGenerationFlag  ; Specifies the attribute reference text generation flag
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the attribute reference
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the attribute reference
    P UpsideDown : VARIANT_BOOL  [get/put]  vla-get-UpsideDown / vla-put-UpsideDown  ; Returns whether the text is upside down and sets the text upside down
    P VerticalAlignment : AcVerticalAlignment  [get/put]  vla-get-VerticalAlignment / vla-put-VerticalAlignment  ; Specifies the vertical alignment of the attribute reference

[INTERFACE] IAxDbDocument
Object: AxDbDocument
Inherits: IAcadDatabase
Doc: IAxDbDocument Interface
  Methods
    M DxfIn  (vla-DxfIn obj FileName<BSTR> [LogFileName<VARIANT>])  ; method DxfIn
    M DxfOut  (vla-DxfOut obj FileName<BSTR> [precision<VARIANT>] [SaveThumbnailImage<VARIANT>])  ; method DxfOut
    M Open  (vla-Open obj FileName<BSTR> [Password<VARIANT>])  ; method Open
    M Save  (vla-Save obj)  ; method Save
    M SaveAs  (vla-SaveAs obj FileName<BSTR> [vSecurityParams<VARIANT>])  ; method SaveAs
  Properties
    P Application : IDispatch  [get READ-ONLY]  vla-get-Application  ; Returns the application object.
    P Database : IAcadDatabase  [get READ-ONLY]  vla-get-Database  ; Returns the database object.
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; property Name

[EVENTS] IAxDbDocumentEvents
Note: event (source) interface: these are reactor callbacks, NOT vla- functions you can call.
Doc: IAxDbDocumentEvents Interface

[INTERFACE] IAcadBlock
Object: AcadBlock
Inherits: IAcadObject
Doc: A block definition containing a name and a set of objects
  Methods
    M Add3DFace  (vla-Add3DFace obj Point1<VARIANT> Point2<VARIANT> point3<VARIANT> Point4<VARIANT>) -> IAcad3DFace  ; Creates a 3DFace object given four vertices
    M Add3DMesh  (vla-Add3DMesh obj M<long> N<long> PointsMatrix<VARIANT>) -> IAcadPolygonMesh  ; Creates a free-form 3D mesh, given the number of points in the M and N directions and the coordinates of the points in the M and N directions
    M Add3DPoly  (vla-Add3DPoly obj PointsArray<VARIANT>) -> IAcad3DPolyline  ; Creates a 3D polyline from the given array of coordinates
    M AddArc  (vla-AddArc obj Center<VARIANT> Radius<double> StartAngle<double> EndAngle<double>) -> IAcadArc  ; Creates an arc given the center, radius, start angle, and end angle of the arc
    M AddAttribute  (vla-AddAttribute obj Height<double> Mode<AcAttributeMode> Prompt<BSTR> InsertionPoint<VARIANT> Tag<BSTR> Value<BSTR>) -> IAcadAttribute  ; Creates an attribute definition at the given location with the specified properties
    M AddBox  (vla-AddBox obj Origin<VARIANT> Length<double> Width<double> Height<double>) -> IAcad3DSolid  ; Creates a 3D solid box with edges parallel to the axes of the WCS
    M AddCircle  (vla-AddCircle obj Center<VARIANT> Radius<double>) -> IAcadCircle  ; Creates a circle given a center point and radius
    M AddCone  (vla-AddCone obj Center<VARIANT> BaseRadius<double> Height<double>) -> IAcad3DSolid  ; Creates a 3D solid cone with the base on the XY plane of the WCS
    M AddCustomObject  (vla-AddCustomObject obj ClassName<BSTR>) -> IDispatch  ; Creates a Custom object
    M AddCylinder  (vla-AddCylinder obj Center<VARIANT> Radius<double> Height<double>) -> IAcad3DSolid  ; Creates a 3D solid cylinder whose base is on the XY plane of the WCS
    M AddDim3PointAngular  (vla-AddDim3PointAngular obj AngleVertex<VARIANT> FirstEndPoint<VARIANT> SecondEndPoint<VARIANT> TextPoint<VARIANT>) -> IAcadDim3PointAngular  ; Creates an angular dimension for an arc, two lines, or a circle
    M AddDimAligned  (vla-AddDimAligned obj ExtLine1Point<VARIANT> ExtLine2Point<VARIANT> TextPosition<VARIANT>) -> IAcadDimAligned  ; Creates an aligned dimension object
    M AddDimAngular  (vla-AddDimAngular obj AngleVertex<VARIANT> FirstEndPoint<VARIANT> SecondEndPoint<VARIANT> TextPoint<VARIANT>) -> IAcadDimAngular  ; Creates an angular dimension for an arc, two lines, or a circle
    M AddDimArc  (vla-AddDimArc obj ArcCenter<VARIANT> FirstEndPoint<VARIANT> SecondEndPoint<VARIANT> ArcPoint<VARIANT>) -> IAcadDimArcLength  ; Creates an arc length dimension for an arc
    M AddDimDiametric  (vla-AddDimDiametric obj ChordPoint<VARIANT> FarChordPoint<VARIANT> LeaderLength<double>) -> IAcadDimDiametric  ; Creates a diametric dimension for a circle or arc given the two points on the diameter and the length of the leader line
    M AddDimOrdinate  (vla-AddDimOrdinate obj DefinitionPoint<VARIANT> LeaderEndPoint<VARIANT> UseXAxis<long>) -> IAcadDimOrdinate  ; Creates an ordinate dimension given the definition point, and leader endpoint
    M AddDimRadial  (vla-AddDimRadial obj Center<VARIANT> ChordPoint<VARIANT> LeaderLength<double>) -> IAcadDimRadial  ; Creates a radial dimension for the selected object at the given location
    M AddDimRadialLarge  (vla-AddDimRadialLarge obj Center<VARIANT> ChordPoint<VARIANT> OverrideCenter<VARIANT> JogPoint<VARIANT> JogAngle<double>) -> IAcadDimRadialLarge  ; Creates a jogged radial dimension for an arc, circle, or polyline arc segment
    M AddDimRotated  (vla-AddDimRotated obj ExtLine1Point<VARIANT> ExtLine2Point<VARIANT> DimLineLocation<VARIANT> RotationAngle<double>) -> IAcadDimRotated  ; Creates a rotated linear dimension
    M AddEllipse  (vla-AddEllipse obj Center<VARIANT> MajorAxis<VARIANT> RadiusRatio<double>) -> IAcadEllipse  ; Creates an ellipse in the XY plane of the WCS given the center point, a point on the major axis, and the radius ratio
    M AddEllipticalCone  (vla-AddEllipticalCone obj Center<VARIANT> MajorRadius<double> MinorRadius<double> Height<double>) -> IAcad3DSolid  ; Creates a 3D solid elliptical cone on the XY plane of the WCS given the Center, MajorRadius, MinorRadius, and Height
    M AddEllipticalCylinder  (vla-AddEllipticalCylinder obj Center<VARIANT> MajorRadius<double> MinorRadius<double> Height<double>) -> IAcad3DSolid  ; Creates a 3D solid elliptical cylinder whose base is on the XY plane of the WCS, given the Center, MajorRadius, MinorRadius, and Height
    M AddExtrudedSolid  (vla-AddExtrudedSolid obj Profile<IAcadRegion*> Height<double> TaperAngle<double>) -> IAcad3DSolid  ; Creates an extruded solid given the Profile, Height, and TaperAngle
    M AddExtrudedSolidAlongPath  (vla-AddExtrudedSolidAlongPath obj Profile<IAcadRegion*> Path<IDispatch*>) -> IAcad3DSolid  ; Creates an extruded solid given the profile and an extrusion path
    M AddHatch  (vla-AddHatch obj PatternType<long> PatternName<BSTR> Associativity<VARIANT_BOOL> [HatchObjectType<VARIANT>]) -> IAcadHatch  ; Creates a Hatch object
    M AddLeader  (vla-AddLeader obj PointsArray<VARIANT> Annotation<IAcadEntity*> Type<AcLeaderType>) -> IAcadLeader  ; Creates a leader line, given the coordinates of the points
    M AddLightWeightPolyline  (vla-AddLightWeightPolyline obj VerticesList<VARIANT>) -> IAcadLWPolyline  ; Creates a lightweight polyline from a list of vertices
    M AddLine  (vla-AddLine obj StartPoint<VARIANT> EndPoint<VARIANT>) -> IAcadLine  ; Creates a line passing through two points
    M AddMInsertBlock  (vla-AddMInsertBlock obj InsertionPoint<VARIANT> Name<BSTR> Xscale<double> Yscale<double> Zscale<double> Rotation<double> NumRows<long> NumColumns<long> RowSpacing<long> ColumnSpacing<long> [Password<VARIANT>]) -> IAcadMInsertBlock  ; Inserts an array of blocks
    M AddMLeader  (vla-AddMLeader obj PointsArray<VARIANT> 'leaderLineIndex<out long>) -> IAcadMLeader  ; Creates a multileader
    M AddMLine  (vla-AddMLine obj VertexList<VARIANT>) -> IAcadMLine  ; Creates a polyface mesh from a list of vertices
    M AddMText  (vla-AddMText obj InsertionPoint<VARIANT> Width<double> Text<BSTR>) -> IAcadMText  ; Creates an MText entity in a rectangle defined by the insertion point and width of the bounding box
    M AddPoint  (vla-AddPoint obj Point<VARIANT>) -> IAcadPoint  ; Creates a Point object at a given location
    M AddPolyfaceMesh  (vla-AddPolyfaceMesh obj VertexList<VARIANT> FaceList<VARIANT>) -> IAcadPolyfaceMesh  ; Creates a polyface mesh from a list of vertices
    M AddPolyline  (vla-AddPolyline obj VerticesList<VARIANT>) -> IAcadPolyline  ; Creates a polyline from a list of vertices
    M AddRaster  (vla-AddRaster obj imageFileName<BSTR> InsertionPoint<VARIANT> ScaleFactor<double> RotationAngle<double>) -> IAcadRasterImage  ; Creates a new raster image based on an existing image file
    M AddRay  (vla-AddRay obj Point1<VARIANT> Point2<VARIANT>) -> IAcadRay  ; Creates a ray passing through two unique points
    M AddRegion  (vla-AddRegion obj ObjectList<VARIANT>) -> VARIANT  ; Creates a region from a set of entities. The given entities must form a closed coplanar region
    M AddRevolvedSolid  (vla-AddRevolvedSolid obj Profile<IAcadRegion*> AxisPoint<VARIANT> AxisDir<VARIANT> Angle<double>) -> IAcad3DSolid  ; Creates a revolved solid, given the region around an axis
    M AddSection  (vla-AddSection obj FromPoint<VARIANT> ToPoint<VARIANT> planeVector<VARIANT>) -> IAcadSection  ; Creates a section plane
    M AddShape  (vla-AddShape obj Name<BSTR> InsertionPoint<VARIANT> ScaleFactor<double> RotationAngle<double>) -> IAcadShape  ; Creates a Shape object based on a template identified by name, at the given insertion point, scale factor, and rotation
    M AddSolid  (vla-AddSolid obj Point1<VARIANT> Point2<VARIANT> point3<VARIANT> Point4<VARIANT>) -> IAcadSolid  ; Creates a 2D solid polygon
    M AddSphere  (vla-AddSphere obj Center<VARIANT> Radius<double>) -> IAcad3DSolid  ; Creates a sphere given the center and radius
    M AddSpline  (vla-AddSpline obj PointsArray<VARIANT> StartTangent<VARIANT> EndTangent<VARIANT>) -> IAcadSpline  ; Creates a quadratic or cubic NURBS (nonuniform rational B-spline) curve
    M AddTable  (vla-AddTable obj InsertionPoint<VARIANT> NumRows<long> NumColumns<long> RowHeight<double> ColWidth<double>) -> IAcadTable  ; Creates a table at the given insertion point, given the number of rows, number of columns, row height and column width
    M AddText  (vla-AddText obj TextString<BSTR> InsertionPoint<VARIANT> Height<double>) -> IAcadText  ; Creates a single line of text
    M AddTolerance  (vla-AddTolerance obj Text<BSTR> InsertionPoint<VARIANT> Direction<VARIANT>) -> IAcadTolerance  ; Creates a tolerance entity
    M AddTorus  (vla-AddTorus obj Center<VARIANT> TorusRadius<double> TubeRadius<double>) -> IAcad3DSolid  ; Creates a torus at the given location
    M AddTrace  (vla-AddTrace obj PointsArray<VARIANT>) -> IAcadTrace  ; Creates a Trace object from an array of points
    M AddWedge  (vla-AddWedge obj Center<VARIANT> Length<double> Width<double> Height<double>) -> IAcad3DSolid  ; Creates a wedge with edges parallel to the axes given the length, width, and height
    M AddXline  (vla-AddXline obj Point1<VARIANT> Point2<VARIANT>) -> IAcadXline  ; Creates an xline (an infinite line) passing through two specified points
    M AttachExternalReference  (vla-AttachExternalReference obj PathName<BSTR> Name<BSTR> InsertionPoint<VARIANT> Xscale<double> Yscale<double> Zscale<double> Rotation<double> bOverlay<VARIANT_BOOL> [Password<VARIANT>]) -> IAcadExternalReference  ; Attaches an external reference (xref) to the drawing
    M Bind  (vla-Bind obj bPrefixName<VARIANT_BOOL>)  ; Binds an external reference (xref) to a drawing
    M Detach  (vla-Detach obj)  ; Detachs an external reference (xref) from a drawing
    M InsertBlock  (vla-InsertBlock obj InsertionPoint<VARIANT> Name<BSTR> Xscale<double> Yscale<double> Zscale<double> Rotation<double> [Password<VARIANT>]) -> IAcadBlockReference  ; Inserts a drawing file or a named block that has been defined in the current drawing
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadEntity  ; Gets the member object at a given index in a collection, group, or selection set
    M Reload  (vla-Reload obj)  ; Reloads the external reference (xref)
    M Unload  (vla-Unload obj)  ; Unloads the menu group or external reference
  Properties
    P BlockScaling : AcBlockScaling  [get/put]  vla-get-BlockScaling / vla-put-BlockScaling  ; Specifies the allowed scaling for the block
    P Comments : BSTR  [get/put]  vla-get-Comments / vla-put-Comments  ; Specifies the comments for the block
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Explodable : VARIANT_BOOL  [get/put]  vla-get-Explodable / vla-put-Explodable  ; Specifies whether the block can be exploded
    P IsDynamicBlock : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsDynamicBlock  ; Specifies if this is a dynamic block
    P IsLayout : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsLayout  ; Determines if the given block is a layout block
    P IsXRef : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsXRef  ; Determines if the given block is an XRef block
    P Layout : IAcadLayout  [get READ-ONLY]  vla-get-Layout  ; Specifies the layout associated with the model space, paper space, or block object
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P Origin : VARIANT  [get/put]  vla-get-Origin / vla-put-Origin  ; Specifies the origin of the UCS, block, layout, or raster image in WCS coordinates
    P Path : BSTR  [get/put]  vla-get-Path / vla-put-Path  ; Specifies the path of the external reference
    P Units : AcInsertUnits  [get/put]  vla-get-Units / vla-put-Units  ; Specifies the native units of measure for the block
    P XRefDatabase : IAcadDatabase  [get READ-ONLY]  vla-get-XRefDatabase  ; Gets the Database object that defines the contents of the block

[INTERFACE] IAcadBlockReference
Object: AcadBlockReference
Inherits: IAcadEntity
Doc: AutoCAD Block Reference Interface
  Methods
    M ConvertToAnonymousBlock  (vla-ConvertToAnonymousBlock obj)  ; Converts a dynamic block to a regular anonymous block
    M ConvertToStaticBlock  (vla-ConvertToStaticBlock obj newBlockName<BSTR>)  ; Converts a dynamic block to a regular named block
    M Explode  (vla-Explode obj) -> VARIANT  ; Explodes the block and returns the sub-entities as an array of Object
    M GetAttributes  (vla-GetAttributes obj) -> VARIANT  ; Gets Attributes in the block
    M GetConstantAttributes  (vla-GetConstantAttributes obj) -> VARIANT  ; Gets constant attributes in the block
    M GetDynamicBlockProperties  (vla-GetDynamicBlockProperties obj) -> VARIANT  ; Gets the dynamic block properties
    M ResetBlock  (vla-ResetBlock obj)  ; Resets the dynamic block to the default state
  Properties
    P EffectiveName : BSTR  [get READ-ONLY]  vla-get-EffectiveName  ; Specifies the original block name
    P HasAttributes : VARIANT_BOOL  [get READ-ONLY]  vla-get-HasAttributes  ; Specifies if the block has any attributes in it
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specify the X, Y, Z coordinate for insertion point of the block or use the Pick Point button to set X, Y, Z values simultaneously
    P InsUnits : BSTR  [get READ-ONLY]  vla-get-InsUnits  ; Specifies insunits saved with the block
    P InsUnitsFactor : double  [get READ-ONLY]  vla-get-InsUnitsFactor  ; Specifies the conversion factor between block units and drawing units
    P IsDynamicBlock : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsDynamicBlock  ; Specifies if this is a dynamic block
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the block
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the block
    P XEffectiveScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-XEffectiveScaleFactor / vla-put-XEffectiveScaleFactor  ; Specifies the effective XScale factor of the block
    P XScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-XScaleFactor / vla-put-XScaleFactor  ; Specifies the XScale factor of the block
    P YEffectiveScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-YEffectiveScaleFactor / vla-put-YEffectiveScaleFactor  ; Specifies the effective Yscale factor of the block
    P YScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-YScaleFactor / vla-put-YScaleFactor  ; Specifies the Yscale factor of the block
    P ZEffectiveScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ZEffectiveScaleFactor / vla-put-ZEffectiveScaleFactor  ; Specifies the effective ZScale factor of the block
    P ZScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ZScaleFactor / vla-put-ZScaleFactor  ; Specifies the ZScale factor of the block

[INTERFACE] IAcadBlocks
Object: AcadBlocks
Inherits: IAcadObject
Doc: The collection of all blocks in the drawing
  Methods
    M Add  (vla-Add obj InsertionPoint<VARIANT> Name<BSTR>) -> IAcadBlock  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadBlock  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadCircle
Object: AcadCircle
Inherits: IAcadEntity
Doc: AutoCAD Circle Interface
  Methods
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Creates an arc by offsetting the original arc by a specified distance
  Properties
    P Area : double  [get/put]  vla-get-Area / vla-put-Area  ; Specifies the area of the circle
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specify the X, Y, Z coordinate of the center of the circle or use the Pick Point button to set X, Y, Z values simultaneously
    P Circumference : double  [get/put]  vla-get-Circumference / vla-put-Circumference  ; Specifies the circumference of the circle
    P Diameter : double  [get/put]  vla-get-Diameter / vla-put-Diameter  ; Specifies the diameter of the circle
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specify the X, Y, Z coordinate of the normal direction vector
    P Radius : double  [get/put]  vla-get-Radius / vla-put-Radius  ; Specifies the radius of the circle
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the circle

[INTERFACE] IAcadComparedReference
Object: AcadComparedReference
Inherits: IAcadExternalReference
Doc: IAcadExternalReference Interface

[INTERFACE] IAcadDatabase
Object: AcadDatabase
Doc: The contents of an XRef block
  Methods
    M CopyObjects  (vla-CopyObjects obj Objects<VARIANT> [Owner<VARIANT>] ['IdPairs<out VARIANT>]) -> VARIANT  ; Duplicates multiple objects (deep cloning)
    M HandleToObject  (vla-HandleToObject obj Handle<BSTR>) -> IDispatch  ; Gets the object that corresponds to the given handle
    M ObjectIdToObject  (vla-ObjectIdToObject obj ObjectID<LONG_PTR>) -> IDispatch  ; Gets the object that corresponds to the given object ID
  Properties
    P Blocks : IAcadBlocks  [get READ-ONLY]  vla-get-Blocks  ; Gets the Blocks collection for the drawing
    P Dictionaries : IAcadDictionaries  [get READ-ONLY]  vla-get-Dictionaries  ; Gets the Dictionaries collection for the document
    P DimStyles : IAcadDimStyles  [get READ-ONLY]  vla-get-DimStyles  ; Gets the DimStyles collection for the document
    P ElevationModelSpace : double  [get/put]  vla-get-ElevationModelSpace / vla-put-ElevationModelSpace  ; Specifies the elevation setting in the model space
    P ElevationPaperSpace : double  [get/put]  vla-get-ElevationPaperSpace / vla-put-ElevationPaperSpace  ; Specifies the elevation setting in the paper space
    P Groups : IAcadGroups  [get READ-ONLY]  vla-get-Groups  ; Gets the Groups collection for the document
    P Layers : IAcadLayers  [get READ-ONLY]  vla-get-Layers  ; Gets the Layers collection for the document
    P Layouts : IAcadLayouts  [get READ-ONLY]  vla-get-Layouts  ; Gets the Layouts collection for the document
    P Limits : VARIANT  [get/put]  vla-get-Limits / vla-put-Limits  ; Specifies the drawing limits
    P Linetypes : IAcadLineTypes  [get READ-ONLY]  vla-get-Linetypes  ; Gets the Linetypes collection for the document
    P Materials : IAcadMaterials  [get READ-ONLY]  vla-get-Materials
    P ModelSpace : IAcadModelSpace  [get READ-ONLY]  vla-get-ModelSpace  ; Gets the ModelSpace collection for the document
    P PaperSpace : IAcadPaperSpace  [get READ-ONLY]  vla-get-PaperSpace  ; Gets the PaperSpace collection for the document
    P PlotConfigurations : IAcadPlotConfigurations  [get READ-ONLY]  vla-get-PlotConfigurations  ; Gets the PlotConfigurations collection for the document
    P Preferences : IAcadDatabasePreferences  [get READ-ONLY]  vla-get-Preferences  ; Gets the Preferences object
    P RegisteredApplications : IAcadRegisteredApplications  [get READ-ONLY]  vla-get-RegisteredApplications  ; The collection of all registered applications in the drawing
    P SectionManager : IAcadSectionManager  [get READ-ONLY]  vla-get-SectionManager  ; Returns the section manager object.
    P SummaryInfo : IAcadSummaryInfo  [get READ-ONLY]  vla-get-SummaryInfo  ; Returns the summary info object.
    P TextStyles : IAcadTextStyles  [get READ-ONLY]  vla-get-TextStyles  ; Gets the TextStyles collection for the document
    P UserCoordinateSystems : IAcadUCSs  [get READ-ONLY]  vla-get-UserCoordinateSystems  ; Gets the UCSs collection for the document
    P Viewports : IAcadViewports  [get READ-ONLY]  vla-get-Viewports  ; Gets the Viewports collection for the document
    P Views : IAcadViews  [get READ-ONLY]  vla-get-Views  ; Gets the Views collection for the document

[INTERFACE] IAcadDatabasePreferences
Object: AcadDatabasePreferences
Doc: This object specifies the current AutoCAD drawing specific settings
  Properties
    P AllowLongSymbolNames : VARIANT_BOOL  [get/put]  vla-get-AllowLongSymbolNames / vla-put-AllowLongSymbolNames  ; Determines if symbol names may include extended character sets, or more than 31 characters
    P Application : IDispatch  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P ContourLinesPerSurface : long  [get/put]  vla-get-ContourLinesPerSurface / vla-put-ContourLinesPerSurface  ; Specifies the number of contour lines (isolines) per surface on objects
    P DisplaySilhouette : VARIANT_BOOL  [get/put]  vla-get-DisplaySilhouette / vla-put-DisplaySilhouette  ; Controls if silhouette curves of solid objects are displayed in Wireframe mode
    P Lineweight : AcLineWeight  [get/put]  vla-get-Lineweight / vla-put-Lineweight  ; Specifies the lineweight of an individual entity or the default lineweight for the drawing
    P LineWeightDisplay : VARIANT_BOOL  [get/put]  vla-get-LineWeightDisplay / vla-put-LineWeightDisplay  ; Specifies whether lineweights are displayed in model space for the drawing
    P MaxActiveViewports : long  [get/put]  vla-get-MaxActiveViewports / vla-put-MaxActiveViewports  ; Specifies the maximum number of active viewports
    P ObjectSortByPlotting : VARIANT_BOOL  [get/put]  vla-get-ObjectSortByPlotting / vla-put-ObjectSortByPlotting  ; Toggles sorting of drawing objects by plotting order
    P ObjectSortByPSOutput : VARIANT_BOOL  [get/put]  vla-get-ObjectSortByPSOutput / vla-put-ObjectSortByPSOutput  ; Toggles sorting of drawing objects by PostScript output order
    P ObjectSortByRedraws : VARIANT_BOOL  [get/put]  vla-get-ObjectSortByRedraws / vla-put-ObjectSortByRedraws  ; Toggles sorting of drawing objects by redraw order
    P ObjectSortByRegens : VARIANT_BOOL  [get/put]  vla-get-ObjectSortByRegens / vla-put-ObjectSortByRegens  ; Toggles sorting of drawing objects by regeneration order
    P ObjectSortBySelection : VARIANT_BOOL  [get/put]  vla-get-ObjectSortBySelection / vla-put-ObjectSortBySelection  ; Toggles sorting of drawing objects by object selection
    P ObjectSortBySnap : VARIANT_BOOL  [get/put]  vla-get-ObjectSortBySnap / vla-put-ObjectSortBySnap  ; Toggles sorting of drawing objects by object snap
    P OLELaunch : VARIANT_BOOL  [get/put]  vla-get-OLELaunch / vla-put-OLELaunch  ; Determines whether to launch the parent application when plotting OLE objects
    P RenderSmoothness : double  [get/put]  vla-get-RenderSmoothness / vla-put-RenderSmoothness  ; Specifies the smoothness of shaded, rendered, and hidden line-removed objects
    P SegmentPerPolyline : long  [get/put]  vla-get-SegmentPerPolyline / vla-put-SegmentPerPolyline  ; Specifies the number of line segments to be generated for each polyline curve
    P SolidFill : VARIANT_BOOL  [get/put]  vla-get-SolidFill / vla-put-SolidFill  ; Specifies if multilines, traces, solids, all hatches (including solid-fill) and wide polylines are filled in
    P TextFrameDisplay : VARIANT_BOOL  [get/put]  vla-get-TextFrameDisplay / vla-put-TextFrameDisplay  ; Toggles the display of frames for text objects instead of displaying the text itself
    P XRefEdit : VARIANT_BOOL  [get/put]  vla-get-XRefEdit / vla-put-XRefEdit  ; Determines if the current drawing can be edited in place when being referenced by another user
    P XRefLayerVisibility : VARIANT_BOOL  [get/put]  vla-get-XRefLayerVisibility / vla-put-XRefLayerVisibility  ; Determines the visibility of xref-dependent layers and specifies if nested xref path changes are saved

[INTERFACE] IAcadDictionaries
Object: AcadDictionaries
Inherits: IAcadObject
Doc: The collection of all dictionaries in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadDictionary  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadObject  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadDictionary
Object: AcadDictionary
Inherits: IAcadObject
Doc: A container object for storing and retrieving objects
  Methods
    M AddObject  (vla-AddObject obj Keyword<BSTR> ObjectName<BSTR>) -> IAcadObject  ; Adds an object to a named dictionary
    M AddXRecord  (vla-AddXRecord obj Keyword<BSTR>) -> IAcadXRecord  ; Creates an XRecord object in any dictionary
    M GetName  (vla-GetName obj Object<IAcadObject*>) -> BSTR  ; Gets the name (keyword) of an object in a dictionary
    M GetObject  (vla-GetObject obj Name<BSTR>) -> IAcadObject  ; Gets the object in a dictionary, given the name (keyword) of the object
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadObject  ; Gets the member object at a given index in a collection, group, or selection set
    M Remove  (vla-Remove obj Name<BSTR>) -> IAcadObject  ; Removes a named object from the dictionary
    M Rename  (vla-Rename obj OldName<BSTR> NewName<BSTR>)  ; Renames an item in the dictionary
    M Replace  (vla-Replace obj OldName<BSTR> pObj<IAcadObject*>)  ; Replaces an item in the dictionary by a given item
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object

[INTERFACE] IAcadDim3PointAngular
Object: AcadDim3PointAngular
Inherits: IAcadDimension
Doc: IAcadDim3PointAngular Interface
  Properties
    P AngleFormat : AcAngleUnits  [get/put]  vla-get-AngleFormat / vla-put-AngleFormat  ; Specifies the angle format (DIMAUNIT system variable)
    P AngleVertex : VARIANT  [get/put]  vla-get-AngleVertex / vla-put-AngleVertex  ; Specifies the angle vertex for the three point angular dimension
    P Arrowhead1Block : BSTR  [get/put]  vla-get-Arrowhead1Block / vla-put-Arrowhead1Block  ; Specifies the block to use as the custom arrowhead for the first end of the dimension line
    P Arrowhead1Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead1Type / vla-put-Arrowhead1Type  ; Specifies type of the first dimension arrowhead (DIMBLK1 system variable)
    P Arrowhead2Block : BSTR  [get/put]  vla-get-Arrowhead2Block / vla-put-Arrowhead2Block  ; Specifies the block to use as the custom arrowhead for the second end of the dimension line
    P Arrowhead2Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead2Type / vla-put-Arrowhead2Type  ; Specifies type of the second dimension arrowhead (DIMBLK2 system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P DimConstrDesc : BSTR  [get/put]  vla-get-DimConstrDesc / vla-put-DimConstrDesc  ; Specifies description for constraint
    P DimConstrExpression : BSTR  [get/put]  vla-get-DimConstrExpression / vla-put-DimConstrExpression  ; Specifies the expression or the value of the constraint
    P DimConstrForm : VARIANT_BOOL  [get/put]  vla-get-DimConstrForm / vla-put-DimConstrForm  ; Specifies the constraint type - Dynamic or Annotational
    P DimConstrName : BSTR  [get/put]  vla-get-DimConstrName / vla-put-DimConstrName  ; Specifies the name of the dimensional constraint. Names cannot have spaces
    P DimConstrReference : VARIANT_BOOL  [get/put]  vla-get-DimConstrReference / vla-put-DimConstrReference  ; Specifies whether the parameter is a reference constraint. Reference dimensions can be used in expressions but don't drive geometry - similar to an associative dimension
    P DimConstrValue : BSTR  [get/put]  vla-get-DimConstrValue / vla-put-DimConstrValue  ; Specifies the value of the constraint
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine1Suppress / vla-put-DimLine1Suppress  ; Sets suppression of first dimension line On or Off (DIMSD1 system variable)
    P DimLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine2Suppress / vla-put-DimLine2Suppress  ; Sets suppression of second dimension line On or Off (DIMSD2 system variable)
    P DimLineInside : VARIANT_BOOL  [get/put]  vla-get-DimLineInside / vla-put-DimLineInside  ; Sets drawing of dimension lines outside extension lines On or Off (DIMSOXD system variable)
    P ExtensionLineColor : ACAD_COLOR  [get/put]  vla-get-ExtensionLineColor / vla-put-ExtensionLineColor  ; Specifies color of the extension line (DIMCLRE system variable)
    P ExtensionLineExtend : double  [get/put]  vla-get-ExtensionLineExtend / vla-put-ExtensionLineExtend  ; Specifies amount to extend extension line beyond the dimension line (DIMEXE system variable)
    P ExtensionLineOffset : double  [get/put]  vla-get-ExtensionLineOffset / vla-put-ExtensionLineOffset  ; Specifies offset of extension lines from the origin points (DIMEXO system variable)
    P ExtensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-ExtensionLineWeight / vla-put-ExtensionLineWeight  ; Specifies lineweight for extension lines (DIMLWE system variable)
    P ExtLine1EndPoint : VARIANT  [get/put]  vla-get-ExtLine1EndPoint / vla-put-ExtLine1EndPoint  ; Specifies the endpoint of the first extension line
    P ExtLine1Linetype : BSTR  [get/put]  vla-get-ExtLine1Linetype / vla-put-ExtLine1Linetype  ; Specifies the linetype of the first extension line (DIMLTEX1 system variable)
    P ExtLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine1Suppress / vla-put-ExtLine1Suppress  ; Sets suppression of first extension line On or Off (DIMSE1 system variable)
    P ExtLine2EndPoint : VARIANT  [get/put]  vla-get-ExtLine2EndPoint / vla-put-ExtLine2EndPoint  ; Specifies the endpoint of the second extension line
    P ExtLine2Linetype : BSTR  [get/put]  vla-get-ExtLine2Linetype / vla-put-ExtLine2Linetype  ; Specifies the linetype of the second extension line (DIMLTEX2 system variable)
    P ExtLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine2Suppress / vla-put-ExtLine2Suppress  ; Sets suppression of second extension line On or Off (DIMSE2 system variable)
    P ExtLineFixedLen : double  [get/put]  vla-get-ExtLineFixedLen / vla-put-ExtLineFixedLen  ; Set extension line fixed length (DIMFXL system variable)
    P ExtLineFixedLenSuppress : VARIANT_BOOL  [get/put]  vla-get-ExtLineFixedLenSuppress / vla-put-ExtLineFixedLenSuppress  ; Sets suppression of extension line fixed length On or Off (DIMFXLON system variable)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P HorizontalTextPosition : AcDimHorizontalJustification  [get/put]  vla-get-HorizontalTextPosition / vla-put-HorizontalTextPosition  ; Specifies horizontal dimension text position (DIMJUST system variable)
    P Measurement : ACAD_ANGLE  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P TextPrecision : AcDimPrecision  [get/put]  vla-get-TextPrecision / vla-put-TextPrecision  ; Specifies number of precision decimal places displayed for angular dimension text (DIMADEC system variable)

[INTERFACE] IAcadDimAligned
Object: AcadDimAligned
Inherits: IAcadDimension
Doc: AutoCAD Aligned Dimension Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSubUnitsFactor : double  [get/put]  vla-get-AltSubUnitsFactor / vla-put-AltSubUnitsFactor  ; Specifies the alternate sub-units scale factor for all applicable linear dimension
    P AltSubUnitsSuffix : BSTR  [get/put]  vla-get-AltSubUnitsSuffix / vla-put-AltSubUnitsSuffix  ; Specifies the text suffix for the alternate dimension when change to alternate sub-units
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets alternate units dimensioning On or Off (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P Arrowhead1Block : BSTR  [get/put]  vla-get-Arrowhead1Block / vla-put-Arrowhead1Block  ; Specifies the block to use as the custom arrowhead for the first end of the dimension line
    P Arrowhead1Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead1Type / vla-put-Arrowhead1Type  ; Specifies the type of the first dimension arrowhead (DIMBLK1 system variable)
    P Arrowhead2Block : BSTR  [get/put]  vla-get-Arrowhead2Block / vla-put-Arrowhead2Block  ; Specifies the block to use as the custom arrowhead for the second end of the dimension line
    P Arrowhead2Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead2Type / vla-put-Arrowhead2Type  ; Specifies the type of the second dimension arrowhead (DIMBLK2 system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies the size of the dimension arrowhead (DIMASZ system variable)
    P DimConstrDesc : BSTR  [get/put]  vla-get-DimConstrDesc / vla-put-DimConstrDesc  ; Specifies description for constraint
    P DimConstrExpression : BSTR  [get/put]  vla-get-DimConstrExpression / vla-put-DimConstrExpression  ; Specifies the expression or the value of the constraint
    P DimConstrForm : VARIANT_BOOL  [get/put]  vla-get-DimConstrForm / vla-put-DimConstrForm  ; Specifies the constraint type - Dynamic or Annotational
    P DimConstrName : BSTR  [get/put]  vla-get-DimConstrName / vla-put-DimConstrName  ; Specifies the name of the dimensional constraint. Names cannot have spaces
    P DimConstrReference : VARIANT_BOOL  [get/put]  vla-get-DimConstrReference / vla-put-DimConstrReference  ; Specifies whether the parameter is a reference constraint. Reference dimensions can be used in expressions but don't drive geometry - similar to an associative dimension
    P DimConstrValue : BSTR  [get/put]  vla-get-DimConstrValue / vla-put-DimConstrValue  ; Specifies the value of the constraint
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLineExtend : double  [get/put]  vla-get-DimensionLineExtend / vla-put-DimensionLineExtend  ; Specifies amount to extend dimension lines beyond the extension line (DIMDLE system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies the lineweight for dimension lines (DIMLWD system variable)
    P DimLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine1Suppress / vla-put-DimLine1Suppress  ; Sets suppression of the first dimension line On or Off (DIMSD1 system variable)
    P DimLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine2Suppress / vla-put-DimLine2Suppress  ; Sets suppression of the second dimension line On or Off (DIMSD2 system variable)
    P DimLineInside : VARIANT_BOOL  [get/put]  vla-get-DimLineInside / vla-put-DimLineInside  ; Sets drawing of dimension lines outside extension lines On or Off (DIMSOXD system variable)
    P ExtensionLineColor : ACAD_COLOR  [get/put]  vla-get-ExtensionLineColor / vla-put-ExtensionLineColor  ; Specifies color of the extension line (DIMCLRE system variable)
    P ExtensionLineExtend : double  [get/put]  vla-get-ExtensionLineExtend / vla-put-ExtensionLineExtend  ; Specifies amount to extend the extension line beyond the dimension line (DIMEXE system variable)
    P ExtensionLineOffset : double  [get/put]  vla-get-ExtensionLineOffset / vla-put-ExtensionLineOffset  ; Specifies offset of extension lines from the origin points (DIMEXO system variable)
    P ExtensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-ExtensionLineWeight / vla-put-ExtensionLineWeight  ; Specifies lineweight for extension lines (DIMLWE system variable)
    P ExtLine1Linetype : BSTR  [get/put]  vla-get-ExtLine1Linetype / vla-put-ExtLine1Linetype  ; Specifies the linetype of the first extension line (DIMLTEX1 system variable)
    P ExtLine1Point : VARIANT  [get/put]  vla-get-ExtLine1Point / vla-put-ExtLine1Point  ; Specifies the origin of extension line 1
    P ExtLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine1Suppress / vla-put-ExtLine1Suppress  ; Sets suppression of the first extension line On or Off (DIMSE1 system variable)
    P ExtLine2Linetype : BSTR  [get/put]  vla-get-ExtLine2Linetype / vla-put-ExtLine2Linetype  ; Specifies the linetype of the second extension line (DIMLTEX2 system variable)
    P ExtLine2Point : VARIANT  [get/put]  vla-get-ExtLine2Point / vla-put-ExtLine2Point  ; Specifies the origin of extension line 1
    P ExtLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine2Suppress / vla-put-ExtLine2Suppress  ; Sets suppression of the second extension line On or Off (DIMSE2 system variable)
    P ExtLineFixedLen : double  [get/put]  vla-get-ExtLineFixedLen / vla-put-ExtLineFixedLen  ; Set extension line fixed length (DIMFXL system variable)
    P ExtLineFixedLenSuppress : VARIANT_BOOL  [get/put]  vla-get-ExtLineFixedLenSuppress / vla-put-ExtLineFixedLenSuppress  ; Sets suppression of extension line fixed length On or Off (DIMFXLON system variable)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing a dimension line between extension lines even when text is placed outside extension lines (DIMTOFL system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P HorizontalTextPosition : AcDimHorizontalJustification  [get/put]  vla-get-HorizontalTextPosition / vla-put-HorizontalTextPosition  ; Specifies horizontal dimension text position (DIMJUST system variable)
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SubUnitsFactor : double  [get/put]  vla-get-SubUnitsFactor / vla-put-SubUnitsFactor  ; Specifies the sub-units scale factor for all applicable linear dimension
    P SubUnitsSuffix : BSTR  [get/put]  vla-get-SubUnitsSuffix / vla-put-SubUnitsSuffix  ; Specifies the text suffix for all applicable linear dimension when change to sub-units
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets drawing of text between extension lines On or Off (DIMTIX system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside the extension lines On or Off (DIMTXT system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)

[INTERFACE] IAcadDimAngular
Object: AcadDimAngular
Inherits: IAcadDimension
Doc: AutoCAD Angular Dimension Interface
  Properties
    P AngleFormat : AcAngleUnits  [get/put]  vla-get-AngleFormat / vla-put-AngleFormat  ; Specifies the angle format (DIMAUNIT system variable)
    P Arrowhead1Block : BSTR  [get/put]  vla-get-Arrowhead1Block / vla-put-Arrowhead1Block  ; Specifies the block to use as the custom arrowhead for the first end of the dimension line
    P Arrowhead1Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead1Type / vla-put-Arrowhead1Type  ; Specifies type of the first dimension arrowhead (DIMBLK1 system variable)
    P Arrowhead2Block : BSTR  [get/put]  vla-get-Arrowhead2Block / vla-put-Arrowhead2Block  ; Specifies the block to use as the custom arrowhead for the second end of the dimension line
    P Arrowhead2Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead2Type / vla-put-Arrowhead2Type  ; Specifies type of the second dimension arrowhead (DIMBLK2 system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P DimConstrDesc : BSTR  [get/put]  vla-get-DimConstrDesc / vla-put-DimConstrDesc  ; Specifies description for constraint
    P DimConstrExpression : BSTR  [get/put]  vla-get-DimConstrExpression / vla-put-DimConstrExpression  ; Specifies the expression or the value of the constraint
    P DimConstrForm : VARIANT_BOOL  [get/put]  vla-get-DimConstrForm / vla-put-DimConstrForm  ; Specifies the constraint type - Dynamic or Annotational
    P DimConstrName : BSTR  [get/put]  vla-get-DimConstrName / vla-put-DimConstrName  ; Specifies the name of the dimensional constraint. Names cannot have spaces
    P DimConstrReference : VARIANT_BOOL  [get/put]  vla-get-DimConstrReference / vla-put-DimConstrReference  ; Specifies whether the parameter is a reference constraint. Reference dimensions can be used in expressions but don't drive geometry - similar to an associative dimension
    P DimConstrValue : BSTR  [get/put]  vla-get-DimConstrValue / vla-put-DimConstrValue  ; Specifies the value of the constraint
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies the color of the dimension lines (DIMCLRD system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine1Suppress / vla-put-DimLine1Suppress  ; Sets suppression of first dimension line On or Off (DIMSD1 system variable)
    P DimLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine2Suppress / vla-put-DimLine2Suppress  ; Sets suppression of second dimension line On or Off (DIMSD2 system variable)
    P DimLineInside : VARIANT_BOOL  [get/put]  vla-get-DimLineInside / vla-put-DimLineInside  ; Sets drawing of dimension lines outside extension lines On or Off (DIMSOXD system variable)
    P ExtensionLineColor : ACAD_COLOR  [get/put]  vla-get-ExtensionLineColor / vla-put-ExtensionLineColor  ; Specifies color of the extension line (DIMCLRE system variable)
    P ExtensionLineExtend : double  [get/put]  vla-get-ExtensionLineExtend / vla-put-ExtensionLineExtend  ; Specifies amount to extend extension line beyond the dimension line (DIMEXE system variable)
    P ExtensionLineOffset : double  [get/put]  vla-get-ExtensionLineOffset / vla-put-ExtensionLineOffset  ; Specifies offset of extension lines from the origin points (DIMEXO system variable)
    P ExtensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-ExtensionLineWeight / vla-put-ExtensionLineWeight  ; Specifies lineweight for extension lines (DIMLWE system variable)
    P ExtLine1EndPoint : VARIANT  [get/put]  vla-get-ExtLine1EndPoint / vla-put-ExtLine1EndPoint  ; Specifies the endpoint of the first extension line
    P ExtLine1Linetype : BSTR  [get/put]  vla-get-ExtLine1Linetype / vla-put-ExtLine1Linetype  ; Specifies the linetype of the first extension line (DIMLTEX1 system variable)
    P ExtLine1StartPoint : VARIANT  [get/put]  vla-get-ExtLine1StartPoint / vla-put-ExtLine1StartPoint  ; Specifies the start point of the first extension line
    P ExtLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine1Suppress / vla-put-ExtLine1Suppress  ; Sets suppression of first extension line On or Off (DIMSE1 system variable)
    P ExtLine2EndPoint : VARIANT  [get/put]  vla-get-ExtLine2EndPoint / vla-put-ExtLine2EndPoint  ; Specifies the endpoint of the second extension line
    P ExtLine2Linetype : BSTR  [get/put]  vla-get-ExtLine2Linetype / vla-put-ExtLine2Linetype  ; Specifies the linetype of the second extension line (DIMLTEX2 system variable)
    P ExtLine2StartPoint : VARIANT  [get/put]  vla-get-ExtLine2StartPoint / vla-put-ExtLine2StartPoint  ; Specifies the start point of the second extension line
    P ExtLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine2Suppress / vla-put-ExtLine2Suppress  ; Sets suppression of second extension line On or Off (DIMSE2 system variable)
    P ExtLineFixedLen : double  [get/put]  vla-get-ExtLineFixedLen / vla-put-ExtLineFixedLen  ; Set extension line fixed length (DIMFXL system variable)
    P ExtLineFixedLenSuppress : VARIANT_BOOL  [get/put]  vla-get-ExtLineFixedLenSuppress / vla-put-ExtLineFixedLenSuppress  ; Sets suppression of extension line fixed length On or Off (DIMFXLON system variable)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P HorizontalTextPosition : AcDimHorizontalJustification  [get/put]  vla-get-HorizontalTextPosition / vla-put-HorizontalTextPosition  ; Specifies horizontal dimension text position (DIMJUST system variable)
    P Measurement : ACAD_ANGLE  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets drawing of text between extension lines On or Off (DIMTIX system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P TextPrecision : AcDimPrecision  [get/put]  vla-get-TextPrecision / vla-put-TextPrecision  ; Specifies number of precision decimal places displayed for angular dimension text (DIMADEC system variable)

[INTERFACE] IAcadDimArcLength
Object: AcadDimArcLength
Inherits: IAcadDimension
Doc: IAcadDimArcLength Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSubUnitsFactor : double  [get/put]  vla-get-AltSubUnitsFactor / vla-put-AltSubUnitsFactor  ; Specifies the alternate sub-units scale factor for all applicable linear dimension
    P AltSubUnitsSuffix : BSTR  [get/put]  vla-get-AltSubUnitsSuffix / vla-put-AltSubUnitsSuffix  ; Specifies the text suffix for the alternate dimension when change to alternate sub-units
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets units format for alternate units dimensions On or Off, except angular (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P ArcEndParam : double  [get/put]  vla-get-ArcEndParam / vla-put-ArcEndParam  ; Specifies the end parameter of the arc
    P ArcPoint : VARIANT  [get/put]  vla-get-ArcPoint / vla-put-ArcPoint  ; Specifies a point on the arc
    P ArcStartParam : double  [get/put]  vla-get-ArcStartParam / vla-put-ArcStartParam  ; Specifies the start parameter of the arc
    P Arrowhead1Block : BSTR  [get/put]  vla-get-Arrowhead1Block / vla-put-Arrowhead1Block  ; Specifies the block to use as the custom arrowhead for the first end of the dimension line
    P Arrowhead1Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead1Type / vla-put-Arrowhead1Type  ; Specifies type of the first dimension arrowhead (DIMBLK1 system variable)
    P Arrowhead2Block : BSTR  [get/put]  vla-get-Arrowhead2Block / vla-put-Arrowhead2Block  ; Specifies the block to use as the custom arrowhead for the second end of the dimension line
    P Arrowhead2Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead2Type / vla-put-Arrowhead2Type  ; Specifies type of the second dimension arrowhead (DIMBLK2 system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P CenterPoint : VARIANT  [get/put]  vla-get-CenterPoint / vla-put-CenterPoint  ; Specifies the center of the arc
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLineExtend : double  [get/put]  vla-get-DimensionLineExtend / vla-put-DimensionLineExtend  ; Specifies amount to extend dimension lines beyond the extension line (DIMDLE system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine1Suppress / vla-put-DimLine1Suppress  ; Sets suppression of first dimension line On or Off (DIMSD1 system variable)
    P DimLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine2Suppress / vla-put-DimLine2Suppress  ; Sets suppression of second dimension line On or Off (DIMSD2 system variable)
    P DimLineInside : VARIANT_BOOL  [get/put]  vla-get-DimLineInside / vla-put-DimLineInside  ; Sets drawing of dimension lines outside extension lines On or Off (DIMSOXD system variable)
    P ExtensionLineColor : ACAD_COLOR  [get/put]  vla-get-ExtensionLineColor / vla-put-ExtensionLineColor  ; Specifies color of the extension line (DIMCLRE system variable)
    P ExtensionLineExtend : double  [get/put]  vla-get-ExtensionLineExtend / vla-put-ExtensionLineExtend  ; Specifies amount to extend extension line beyond the dimension line (DIMEXE system variable)
    P ExtensionLineOffset : double  [get/put]  vla-get-ExtensionLineOffset / vla-put-ExtensionLineOffset  ; Specifies offset of extension lines from the origin points (DIMEXO system variable)
    P ExtensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-ExtensionLineWeight / vla-put-ExtensionLineWeight  ; Specifies lineweight for extension lines (DIMLWE system variable)
    P ExtLine1Linetype : BSTR  [get/put]  vla-get-ExtLine1Linetype / vla-put-ExtLine1Linetype  ; Specifies the linetype of the first extension line (DIMLTEX1 system variable)
    P ExtLine1Point : VARIANT  [get/put]  vla-get-ExtLine1Point / vla-put-ExtLine1Point  ; Specifies the origin of extension line 1
    P ExtLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine1Suppress / vla-put-ExtLine1Suppress  ; Sets suppression of first extension line On or Off (DIMSE1 system variable)
    P ExtLine2Linetype : BSTR  [get/put]  vla-get-ExtLine2Linetype / vla-put-ExtLine2Linetype  ; Specifies the linetype of the second extension line (DIMLTEX2 system variable)
    P ExtLine2Point : VARIANT  [get/put]  vla-get-ExtLine2Point / vla-put-ExtLine2Point  ; Specifies the origin of extension line 2
    P ExtLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine2Suppress / vla-put-ExtLine2Suppress  ; Sets suppression of second extension line On or Off (DIMSE2 system variable)
    P ExtLineFixedLen : double  [get/put]  vla-get-ExtLineFixedLen / vla-put-ExtLineFixedLen  ; Set extension line fixed length (DIMFXL system variable)
    P ExtLineFixedLenSuppress : VARIANT_BOOL  [get/put]  vla-get-ExtLineFixedLenSuppress / vla-put-ExtLineFixedLenSuppress  ; Sets suppression of extension line fixed length On or Off (DIMFXLON system variable)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P HasLeader : VARIANT_BOOL  [get/put]  vla-get-HasLeader / vla-put-HasLeader  ; Specifies the whether the dimension has leader
    P HorizontalTextPosition : AcDimHorizontalJustification  [get/put]  vla-get-HorizontalTextPosition / vla-put-HorizontalTextPosition  ; Specifies horizontal dimension text position (DIMJUST system variable)
    P IsPartial : VARIANT_BOOL  [get/put]  vla-get-IsPartial / vla-put-IsPartial  ; Specifies whether the dimension is for a partial arc
    P Leader1Point : VARIANT  [get/put]  vla-get-Leader1Point / vla-put-Leader1Point  ; Specifies the origin of leader 1
    P Leader2Point : VARIANT  [get/put]  vla-get-Leader2Point / vla-put-Leader2Point  ; Specifies the origin of leader 2
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SubUnitsFactor : double  [get/put]  vla-get-SubUnitsFactor / vla-put-SubUnitsFactor  ; Specifies the sub-units scale factor for all applicable linear dimension
    P SubUnitsSuffix : BSTR  [get/put]  vla-get-SubUnitsSuffix / vla-put-SubUnitsSuffix  ; Specifies the text suffix for all applicable linear dimension when change to sub-units
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P SymbolPosition : AcDimArcLengthSymbol  [get/put]  vla-get-SymbolPosition / vla-put-SymbolPosition  ; Specifies placement of the arc length dimension symbol (DIMARCSYM system variable)
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)

[INTERFACE] IAcadDimDiametric
Object: AcadDimDiametric
Inherits: IAcadDimension
Doc: IAcadDimDiametric Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets alternate units dimensioning On or Off (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P Arrowhead1Block : BSTR  [get/put]  vla-get-Arrowhead1Block / vla-put-Arrowhead1Block  ; Specifies the block to use as the custom arrowhead for the first end of the dimension line
    P Arrowhead1Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead1Type / vla-put-Arrowhead1Type  ; Specifies type of the first dimension arrowhead (DIMBLK1 system variable)
    P Arrowhead2Block : BSTR  [get/put]  vla-get-Arrowhead2Block / vla-put-Arrowhead2Block  ; Specifies the block to use as the custom arrowhead for the second end of the dimension line
    P Arrowhead2Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead2Type / vla-put-Arrowhead2Type  ; Specifies type of the second dimension arrowhead (DIMBLK2 system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P CenterMarkSize : double  [get/put]  vla-get-CenterMarkSize / vla-put-CenterMarkSize  ; Specifies size of the center mark on the dimension (DIMCEN system variable)
    P CenterType : AcDimCenterType  [get/put]  vla-get-CenterType / vla-put-CenterType  ; Specifies type of center mark on the dimension (DIMCEN system variable)
    P DimConstrDesc : BSTR  [get/put]  vla-get-DimConstrDesc / vla-put-DimConstrDesc  ; Specifies description for constraint
    P DimConstrExpression : BSTR  [get/put]  vla-get-DimConstrExpression / vla-put-DimConstrExpression  ; Specifies the expression or the value of the constraint
    P DimConstrForm : VARIANT_BOOL  [get/put]  vla-get-DimConstrForm / vla-put-DimConstrForm  ; Specifies the constraint type - Dynamic or Annotational
    P DimConstrName : BSTR  [get/put]  vla-get-DimConstrName / vla-put-DimConstrName  ; Specifies the name of the dimensional constraint. Names cannot have spaces
    P DimConstrReference : VARIANT_BOOL  [get/put]  vla-get-DimConstrReference / vla-put-DimConstrReference  ; Specifies whether the parameter is a reference constraint. Reference dimensions can be used in expressions but don't drive geometry - similar to an associative dimension
    P DimConstrValue : BSTR  [get/put]  vla-get-DimConstrValue / vla-put-DimConstrValue  ; Specifies the value of the constraint
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine1Suppress / vla-put-DimLine1Suppress  ; Sets suppression of first dimension line On or Off (DIMSD1 system variable)
    P DimLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine2Suppress / vla-put-DimLine2Suppress  ; Sets suppression of second dimension line On or Off (DIMSD2 system variable)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P LeaderLength : double  [put]  vla-put-LeaderLength  ; Specifies the length of the leader on the diameter or radius dimension
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets drawing of text between extension lines On or Off (DIMTIX system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)

[INTERFACE] IAcadDimension
Object: AcadDimension
Inherits: IAcadEntity
Doc: IAcadDimension Interface
  Properties
    P DecimalSeparator : BSTR  [get/put]  vla-get-DecimalSeparator / vla-put-DecimalSeparator  ; Specifies the decimal separator for metric dimensions (DIMDSEP system variable)
    P DimTxtDirection : VARIANT_BOOL  [get/put]  vla-get-DimTxtDirection / vla-put-DimTxtDirection  ; Specifies the dimension text viewing direction.
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle for the object
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the overall scale factor applied to properties that specify sizes, distances, or offsets (DIMSCALE system variable)
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the current dimension style by name (for DIMSTYLE system variable use SETVAR)
    P SuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-SuppressLeadingZeros / vla-put-SuppressLeadingZeros  ; Sets suppression of leading zeros for dimensions On or Off (DIMZIN system variable)
    P SuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-SuppressTrailingZeros / vla-put-SuppressTrailingZeros  ; Sets suppression of trailing zeros for dimensions On or Off (DIMZIN system variable)
    P TextColor : ACAD_COLOR  [get/put]  vla-get-TextColor / vla-put-TextColor  ; Specifies the color of the dimension text (DIMCLRT system variable)
    P TextFill : VARIANT_BOOL  [get/put]  vla-get-TextFill / vla-put-TextFill  ; Sets fill color On or Off (DIMTFILL system variable)
    P TextFillColor : ACAD_COLOR  [get/put]  vla-get-TextFillColor / vla-put-TextFillColor  ; Sets text fill color (DIMTFILLCLR system variable)
    P TextGap : double  [get/put]  vla-get-TextGap / vla-put-TextGap  ; Specifies distance around dimension text when dimension line breaks for dimension text (DIMGAP system variable)
    P TextHeight : double  [get/put]  vla-get-TextHeight / vla-put-TextHeight  ; Specifies text height of the dimension (DIMTXT system variable)
    P TextMovement : AcDimTextMovement  [get/put]  vla-get-TextMovement / vla-put-TextMovement  ; Specifies position of text when it's moved, either manually or automatically (DIMTMOVE system variable)
    P TextOverride : BSTR  [get/put]  vla-get-TextOverride / vla-put-TextOverride  ; Specifies the text string of the dimension (overrides Measurement string)
    P TextPosition : VARIANT  [get/put]  vla-get-TextPosition / vla-put-TextPosition  ; Specifies the dimension text position or pick point
    P TextPrefix : BSTR  [get/put]  vla-get-TextPrefix / vla-put-TextPrefix  ; Specifies the text prefix for the dimension (DIMPOST system variable)
    P TextRotation : ACAD_ANGLE  [get/put]  vla-get-TextRotation / vla-put-TextRotation  ; Specifies the rotation angle of the dimension text
    P TextStyle : BSTR  [get/put]  vla-get-TextStyle / vla-put-TextStyle  ; Specifies text style of the dimension (DIMTXSTY system variable)
    P TextSuffix : BSTR  [get/put]  vla-get-TextSuffix / vla-put-TextSuffix  ; Specifies the text suffix for the dimension (DIMPOST system variable)
    P ToleranceDisplay : AcDimToleranceMethod  [get/put]  vla-get-ToleranceDisplay / vla-put-ToleranceDisplay  ; Specifies display mode of dimension tolerances to dimension text (DIMTOL system variable)
    P ToleranceHeightScale : ACAD_NOUNITS  [get/put]  vla-get-ToleranceHeightScale / vla-put-ToleranceHeightScale  ; Specifies scale factor for text height of tolerance values relative to dimension text height as set by DIMTXT (DIMTFAC system variable)
    P ToleranceJustification : AcDimToleranceJustify  [get/put]  vla-get-ToleranceJustification / vla-put-ToleranceJustification  ; Specifies vertical justification for tolerance values relative to nominal dimension text (DIMTOLJ system variable)
    P ToleranceLowerLimit : double  [get/put]  vla-get-ToleranceLowerLimit / vla-put-ToleranceLowerLimit  ; Specifies minimum (or lower) tolerance limit for dimension text when DIMTOL or DIMLIM is on (DIMTM system variable)
    P TolerancePrecision : AcDimPrecision  [get/put]  vla-get-TolerancePrecision / vla-put-TolerancePrecision  ; Specifies number of decimal places for tolerance values of a dimension (DIMTDEC system variable)
    P ToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressLeadingZeros / vla-put-ToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressTrailingZeros / vla-put-ToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceUpperLimit : double  [get/put]  vla-get-ToleranceUpperLimit / vla-put-ToleranceUpperLimit  ; Specifies the maximum (or upper) tolerance limit for dimension text when DIMTOL or DIMLIM is on (DIMTP sysem variable)
    P VerticalTextPosition : AcDimVerticalJustification  [get/put]  vla-get-VerticalTextPosition / vla-put-VerticalTextPosition  ; Specifies the vertical dimension text position relative to dimension line (DIMTAD system variable)

[INTERFACE] IAcadDimOrdinate
Object: AcadDimOrdinate
Inherits: IAcadDimension
Doc: AutoCAD Ordinate Dimension Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSubUnitsFactor : double  [get/put]  vla-get-AltSubUnitsFactor / vla-put-AltSubUnitsFactor  ; Specifies the alternate sub-units scale factor for all applicable linear dimension
    P AltSubUnitsSuffix : BSTR  [get/put]  vla-get-AltSubUnitsSuffix / vla-put-AltSubUnitsSuffix  ; Specifies the text suffix for the alternate dimension when change to alternate sub-units
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets units format for alternate units dimensions On or Off, except angular (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P ExtensionLineColor : ACAD_COLOR  [get/put]  vla-get-ExtensionLineColor / vla-put-ExtensionLineColor  ; Specifies color of the extension line (DIMCLRE system variable)
    P ExtensionLineOffset : double  [get/put]  vla-get-ExtensionLineOffset / vla-put-ExtensionLineOffset  ; Specifies offset of extension lines from the origin points (DIMEXO system variable)
    P ExtensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-ExtensionLineWeight / vla-put-ExtensionLineWeight  ; Specifies lineweight for extension lines (DIMLWE system variable)
    P ExtLineFixedLen : double  [get/put]  vla-get-ExtLineFixedLen / vla-put-ExtLineFixedLen  ; Set extension line fixed length (DIMFXL system variable)
    P ExtLineFixedLenSuppress : VARIANT_BOOL  [get/put]  vla-get-ExtLineFixedLenSuppress / vla-put-ExtLineFixedLenSuppress  ; Sets suppression of extension line fixed length On or Off (DIMFXLON system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SubUnitsFactor : double  [get/put]  vla-get-SubUnitsFactor / vla-put-SubUnitsFactor  ; Specifies the sub-units scale factor for all applicable linear dimension
    P SubUnitsSuffix : BSTR  [get/put]  vla-get-SubUnitsSuffix / vla-put-SubUnitsSuffix  ; Specifies the text suffix for all applicable linear dimension when change to sub-units
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)
