# AutoCAD 2027 ActiveX: ObjectEvents .. SubDMeshVertex

Generated from the type libraries by scripts/tlb_to_md.py. M = method, P = property.
vla- call: the object is always the first argument; 'name<out T> = pass a quoted symbol the call sets.
A member is listed only on the interface that declares it: walk `Inherits:` (IAcadLine -> IAcadEntity -> IAcadObject).

[EVENTS] IAcadObjectEvents
Note: event (source) interface: these are reactor callbacks, NOT vla- functions you can call.
Doc: Event interface for AutoCAD entites
  Methods
    M Modified  (vla-Modified obj pObject<IAcadObject*>)  ; Triggered when an object or collection in the drawing has been modified

[INTERFACE] IAcadOle
Object: AcadOle
Inherits: IAcadEntity
Doc: IAcadOle Interface
  Properties
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the OLE object box
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specifies the origin coordinates (upper left corner) of the OLE object
    P LockAspectRatio : VARIANT_BOOL  [get/put]  vla-get-LockAspectRatio / vla-put-LockAspectRatio  ; Ensures the width and height of the object stay in proportion
    P OleItemType : AcOleType  [get/put]  vla-get-OleItemType / vla-put-OleItemType  ; Specifies whether the OLE object is linked to the original pasted file when opening object for editing
    P OlePlotQuality : AcOlePlotQuality  [get/put]  vla-get-OlePlotQuality / vla-put-OlePlotQuality  ; Controls plot quality of OLE object based on file type selected from list
    P OleSourceApp : BSTR  [get/put]  vla-get-OleSourceApp / vla-put-OleSourceApp  ; Application for editing OLE object
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the OLE object
    P ScaleHeight : double  [get/put]  vla-get-ScaleHeight / vla-put-ScaleHeight  ; Specifies the height of the object as a percentage of original height
    P ScaleWidth : double  [get/put]  vla-get-ScaleWidth / vla-put-ScaleWidth  ; Specifies the width of the object as a percentage of original width
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the OLE object box

[INTERFACE] IAcadPaperSpace
Object: AcadPaperSpace
Inherits: IAcadBlock
Doc: A special Block object containing all the entities in the active paper space layout
  Methods
    M AddPViewport  (vla-AddPViewport obj Center<VARIANT> Width<double> Height<double>) -> IAcadPViewport  ; Adds a paper space viewport, given the center, height, and width

[INTERFACE] IAcadPlaneSurface
Object: AcadPlaneSurface
Inherits: IAcadSurface
Doc: IAcadPlaneSurface Interface

[INTERFACE] IAcadPlot
Object: AcadPlot
Doc: The set of methods and properties used for plotting layouts
  Methods
    M DisplayPlotPreview  (vla-DisplayPlotPreview obj Preview<AcPreviewMode>)  ; Displays the Plot Preview dialog box with the specified partial or full view preview
    M PlotToDevice  (vla-PlotToDevice obj [plotConfig<VARIANT>]) -> VARIANT_BOOL  ; Plots a layout to a device
    M PlotToFile  (vla-PlotToFile obj plotFile<BSTR> [plotConfig<VARIANT>]) -> VARIANT_BOOL  ; Plots a layout to the specified file
    M SetLayoutsToPlot  (vla-SetLayoutsToPlot obj layoutList<VARIANT>)  ; Specifies the layout or layouts to plot
    M StartBatchMode  (vla-StartBatchMode obj entryCount<long>)  ; Invokes batchmode printing
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P BatchPlotProgress : VARIANT_BOOL  [get/put]  vla-get-BatchPlotProgress / vla-put-BatchPlotProgress  ; Gets the current status of the batch plot, or terminates the batch plot
    P NumberOfCopies : long  [get/put]  vla-get-NumberOfCopies / vla-put-NumberOfCopies  ; Specifies the number of copies to plot
    P QuietErrorMode : VARIANT_BOOL  [get/put]  vla-get-QuietErrorMode / vla-put-QuietErrorMode  ; Toggles the quiet error mode for plot error reporting

[INTERFACE] IAcadPlotConfiguration
Object: AcadPlotConfiguration
Inherits: IAcadObject
Doc: A named collection of plot settings
  Methods
    M CopyFrom  (vla-CopyFrom obj pPlotConfig<IAcadPlotConfiguration*>)  ; Copies the settings from the given plotconfiguration
    M GetCanonicalMediaNames  (vla-GetCanonicalMediaNames obj) -> VARIANT  ; Gets all available canonical media names for the specified plot device.
    M GetCustomScale  (vla-GetCustomScale obj 'Numerator<out double> 'Denominator<out double>)  ; Gets the custom scale for a layout or plot configuration
    M GetLocaleMediaName  (vla-GetLocaleMediaName obj Name<BSTR>) -> BSTR  ; Gets the localized version of the canonical media name.
    M GetPaperMargins  (vla-GetPaperMargins obj 'LowerLeft<out VARIANT> 'UpperRight<out VARIANT>)  ; Gets the margins for the layout or plot configuration
    M GetPaperSize  (vla-GetPaperSize obj 'Width<out double> 'Height<out double>)  ; Gets the width and height of the configured paper
    M GetPlotDeviceNames  (vla-GetPlotDeviceNames obj) -> VARIANT  ; Gets all available plot device names.
    M GetPlotStyleTableNames  (vla-GetPlotStyleTableNames obj) -> VARIANT  ; Gets all available plot style table names.
    M GetWindowToPlot  (vla-GetWindowToPlot obj 'LowerLeft<out VARIANT> 'UpperRight<out VARIANT>)  ; Gets the coordinates that define the portion of the layout to plot
    M RefreshPlotDeviceInfo  (vla-RefreshPlotDeviceInfo obj)  ; Updates the plot, canonical media, and plot style table information to reflect the current system state.
    M SetCustomScale  (vla-SetCustomScale obj Numerator<double> Denominator<double>)  ; Sets the custom scale for a layout or plot configuration
    M SetWindowToPlot  (vla-SetWindowToPlot obj LowerLeft<VARIANT> UpperRight<VARIANT>)  ; Sets the coordinates that define the portion of the layout to plot
  Properties
    P CanonicalMediaName : BSTR  [get/put]  vla-get-CanonicalMediaName / vla-put-CanonicalMediaName  ; Specifies the paper size by name
    P CenterPlot : VARIANT_BOOL  [get/put]  vla-get-CenterPlot / vla-put-CenterPlot  ; Specifies the centering of the plot on the media
    P ConfigName : BSTR  [get/put]  vla-get-ConfigName / vla-put-ConfigName  ; Specifies the plotter configuration name
    P ModelType : VARIANT_BOOL  [get READ-ONLY]  vla-get-ModelType  ; Specifies if the plot configuration applies only to model space or to all layouts
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P PaperUnits : AcPlotPaperUnits  [get/put]  vla-get-PaperUnits / vla-put-PaperUnits  ; Specifies the units for the display of layout or plot configuration properties
    P PlotHidden : VARIANT_BOOL  [get/put]  vla-get-PlotHidden / vla-put-PlotHidden  ; Specifies if objects are to be hidden during a plot
    P PlotOrigin : VARIANT  [get/put]  vla-get-PlotOrigin / vla-put-PlotOrigin  ; Specifies the origin of the UCS, block, layout, or raster image in WCS coordinates
    P PlotRotation : AcPlotRotation  [get/put]  vla-get-PlotRotation / vla-put-PlotRotation  ; Specifies the rotation angle for the layout or plot configuration
    P PlotType : AcPlotType  [get/put]  vla-get-PlotType / vla-put-PlotType  ; Specifies the type of layout or plot configuration
    P PlotViewportBorders : VARIANT_BOOL  [get/put]  vla-get-PlotViewportBorders / vla-put-PlotViewportBorders  ; Specifies if the viewport borders are to be plotted
    P PlotViewportsFirst : VARIANT_BOOL  [get/put]  vla-get-PlotViewportsFirst / vla-put-PlotViewportsFirst  ; Specifies if all geometry in paper space viewports is plotted first
    P PlotWithLineweights : VARIANT_BOOL  [get/put]  vla-get-PlotWithLineweights / vla-put-PlotWithLineweights  ; Specifies if objects plot with the lineweights they're assigned in the plot file, or with the lineweights in the drawing file
    P PlotWithPlotStyles : VARIANT_BOOL  [get/put]  vla-get-PlotWithPlotStyles / vla-put-PlotWithPlotStyles  ; Specifies if objects plot with the configuration they're assigned in the plot file, or with the configuration in the drawing file
    P ScaleLineweights : VARIANT_BOOL  [get/put]  vla-get-ScaleLineweights / vla-put-ScaleLineweights  ; Specifies if the lineweight is scaled with the rest of the geometry when a layout is printed
    P ShowPlotStyles : VARIANT_BOOL  [get/put]  vla-get-ShowPlotStyles / vla-put-ShowPlotStyles  ; Specifies if plot styles are to be used in the plot
    P StandardScale : AcPlotScale  [get/put]  vla-get-StandardScale / vla-put-StandardScale  ; Specifies the standard scale for the layout, viewport, or plot configuration
    P StyleSheet : BSTR  [get/put]  vla-get-StyleSheet / vla-put-StyleSheet  ; Specifies the style sheet for the layout or plot configuration
    P UseStandardScale : VARIANT_BOOL  [get/put]  vla-get-UseStandardScale / vla-put-UseStandardScale  ; Specifies if the plot is to use a standard or custom scale
    P ViewToPlot : BSTR  [get/put]  vla-get-ViewToPlot / vla-put-ViewToPlot  ; Specifies the name of the view to plot

[INTERFACE] IAcadPlotConfigurations
Object: AcadPlotConfigurations
Inherits: IAcadObject
Doc: A collection of named plot settings
  Methods
    M Add  (vla-Add obj Name<BSTR> [ModelType<VARIANT>]) -> IAcadPlotConfiguration  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadPlotConfiguration  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadPoint
Object: AcadPoint
Inherits: IAcadEntity
Doc: AutoCAD Point Interface
  Properties
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specify the X, Y, Z coordinate for the position of the point or use the Pick Point button to set X, Y, Z values simultaneously
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the point

[INTERFACE] IAcadPointCloud
Object: AcadPointCloud
Inherits: IAcadEntity
Doc: AutoCAD Point Cloud Interface
  Properties
    P Height : ACAD_DISTANCE  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the point cloud.
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specifies the insertion point of the point cloud.
    P IntensityColorScheme : AcPointCloudIntensityStyle  [get/put]  vla-get-IntensityColorScheme / vla-put-IntensityColorScheme  ; Specifies the color scheme to use for displaying intensity values.
    P Length : ACAD_DISTANCE  [get/put]  vla-get-Length / vla-put-Length  ; Specifies the length of the point cloud.
    P Locked : VARIANT_BOOL  [get/put]  vla-get-Locked / vla-put-Locked  ; Specifies if the point cloud is locked.
    P Name : BSTR  [get READ-ONLY]  vla-get-Name  ; Specifies the name of the point cloud file.
    P Path : BSTR  [get READ-ONLY]  vla-get-Path  ; Specifies the path to the point cloud file.
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the point cloud.
    P scale : ACAD_NOUNITS  [get/put]  vla-get-scale / vla-put-scale  ; Specifies the scale value of the point cloud.
    P ShowClipped : VARIANT_BOOL  [get/put]  vla-get-ShowClipped / vla-put-ShowClipped  ; Enables or disables the clipping boundary of the point cloud.
    P ShowIntensity : VARIANT_BOOL  [get/put]  vla-get-ShowIntensity / vla-put-ShowIntensity  ; Specifies whether to display point cloud intensity using a shaded color scheme.  You can only see the intensity color mapping effect in 3D visual style and when hardware acceleration is on.
    P Stylization : AcPointCloudStylizationType  [get/put]  vla-get-Stylization / vla-put-Stylization  ; Specifies color stylization for selected point cloud.
    P Unit : BSTR  [get READ-ONLY]  vla-get-Unit  ; Specifies the unit of the point cloud file.
    P UnitFactor : ACAD_NOUNITS  [get READ-ONLY]  vla-get-UnitFactor  ; Specifies insert unit factor of the point cloud file.
    P UseEntityColor : AcPointCloudColorType  [get/put]  vla-get-UseEntityColor / vla-put-UseEntityColor  ; Specifies the point cloud color source.
    P Width : ACAD_DISTANCE  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the point cloud.

[INTERFACE] IAcadPointCloudEx
Inherits: IAcadEntity
Doc: AutoCAD Point Cloud Interface
  Properties
    P ColorScheme : BSTR  [get/put]  vla-get-ColorScheme / vla-put-ColorScheme  ; Specifies the color scheme to display point cloud.
    P Geolocate : VARIANT_BOOL  [get/put]  vla-get-Geolocate / vla-put-Geolocate  ; Specifies if the point cloud is geolocated.
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specifies the insertion point of the point cloud.
    P Locked : VARIANT_BOOL  [get/put]  vla-get-Locked / vla-put-Locked  ; Specifies if the point cloud is locked.
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the point cloud file.
    P Path : BSTR  [get READ-ONLY]  vla-get-Path  ; Specifies the path to the point cloud file.
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the point cloud.
    P scale : ACAD_NOUNITS  [get/put]  vla-get-scale / vla-put-scale  ; Specifies the scale value of the point cloud.
    P ShowCropped : VARIANT_BOOL  [get/put]  vla-get-ShowCropped / vla-put-ShowCropped  ; Specifies if the cropping is shown.
    P Stylization : AcPointCloudExStylizationType  [get/put]  vla-get-Stylization / vla-put-Stylization  ; Specifies the point cloud color source.
    P Unit : BSTR  [get READ-ONLY]  vla-get-Unit  ; Specifies the unit of the point cloud file.
    P UnitFactor : ACAD_NOUNITS  [get READ-ONLY]  vla-get-UnitFactor  ; Specifies insert unit factor of the point cloud file.

[INTERFACE] IAcadPointCloudEx2
Object: AcadPointCloudEx
Inherits: IAcadPointCloudEx
Doc: AutoCAD Point Cloud Interface
  Properties
    P Segmentation : BSTR  [get READ-ONLY]  vla-get-Segmentation  ; Specifies if the point cloud has segmentation.

[INTERFACE] IAcadPolyfaceMesh
Object: AcadPolyfaceMesh
Inherits: IAcadEntity
Doc: IAcadPolyfaceMesh Interface
  Properties
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the vertices of the mesh
    P Faces : VARIANT  [put]  vla-put-Faces
    P NumberOfFaces : long  [get READ-ONLY]  vla-get-NumberOfFaces  ; Specifies the number of faces in the mesh
    P NumberOfVertices : long  [get READ-ONLY]  vla-get-NumberOfVertices  ; Specifies the number of vertices in the mesh

[INTERFACE] IAcadPolygonMesh
Object: AcadPolygonMesh
Inherits: IAcadEntity
Doc: AutoCAD PolygonMesh Interface
  Methods
    M AppendVertex  (vla-AppendVertex obj vertex<VARIANT>)  ; Appends a vertex to the polygonmesh
    M Explode  (vla-Explode obj) -> VARIANT  ; Explodes the polygonmesh and returns the sub-entities as an array of object
  Properties
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the coordinates of the polygonmesh
    P MClose : VARIANT_BOOL  [get/put]  vla-get-MClose / vla-put-MClose  ; Specifies mesh style for M direction, Open or Closed
    P MDensity : long  [get/put]  vla-get-MDensity / vla-put-MDensity  ; Specifies M density value of the polygonmesh; valid values 3-201
    P MVertexCount : long  [get READ-ONLY]  vla-get-MVertexCount  ; Returns the M Vertex number of the polygonmesh
    P NClose : VARIANT_BOOL  [get/put]  vla-get-NClose / vla-put-NClose  ; Specifies mesh style for N direction, Open or Closed
    P NDensity : long  [get/put]  vla-get-NDensity / vla-put-NDensity  ; Specifies N density value of the polygonmesh; valid values 3-201
    P NVertexCount : long  [get READ-ONLY]  vla-get-NVertexCount  ; Specifies the N Vertex number of the polygonmesh
    P Type : AcPolymeshType  [get/put]  vla-get-Type / vla-put-Type  ; Specifies the type of the polygonmesh

[INTERFACE] IAcadPolyline
Object: AcadPolyline
Inherits: IAcadEntity
Doc: AutoCAD Polyline Interface
  Methods
    M AppendVertex  (vla-AppendVertex obj vertex<VARIANT>)  ; Appends a vertex to the polyline
    M Explode  (vla-Explode obj) -> VARIANT  ; Explodes the polyline and returns the sub-entities as an array of Object
    M GetBulge  (vla-GetBulge obj Index<long>) -> double  ; Returns the vertex bulge of the polyline
    M GetWidth  (vla-GetWidth obj Index<long> 'StartWidth<out double> EndWidth<double*>)  ; Returns segment width of the polyline
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Creates a new entity object by offsetting the polyline by a specified distance
    M SetBulge  (vla-SetBulge obj Index<long> bulge<double>)  ; Sets the vertex bulge of the polyline
    M SetWidth  (vla-SetWidth obj Index<long> StartWidth<double> EndWidth<double>)  ; Sets the segment width of the polyline
  Properties
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the polyline
    P Closed : VARIANT_BOOL  [get/put]  vla-get-Closed / vla-put-Closed  ; Determines whether polyline is Open or Closed. Closed draws a line segment from current position to starting point of the polyline.
    P ConstantWidth : double  [get/put]  vla-get-ConstantWidth / vla-put-ConstantWidth  ; Specifies the constant width for the polyline
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the current vertex of the polyline
    P Elevation : double  [get/put]  vla-get-Elevation / vla-put-Elevation  ; Specifies the elevation of the polyline relative to the Z axis of the objects' coordinate system
    P Length : double  [get READ-ONLY]  vla-get-Length  ; Specifies the length of the polyline
    P LinetypeGeneration : VARIANT_BOOL  [get/put]  vla-get-LinetypeGeneration / vla-put-LinetypeGeneration  ; Generates linetype in a continuous pattern through the vertices of the polyline. When turned off, linetype is generated starting and ending with a dash at each vertex.
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the polyline
    P Type : AcPolylineType  [get/put]  vla-get-Type / vla-put-Type  ; Applies a fit curve or spline type to a 2D polyline

[INTERFACE] IAcadPopupMenu
Object: AcadPopupMenu
Doc: An AutoCAD cascading menu
  Methods
    M AddMenuItem  (vla-AddMenuItem obj Index<VARIANT> Label<BSTR> Macro<BSTR>) -> IAcadPopupMenuItem  ; Adds a popup menu item to a popup menu
    M AddSeparator  (vla-AddSeparator obj Index<VARIANT>) -> IAcadPopupMenuItem  ; Adds a separator to an existing menu or toolbar
    M AddSubMenu  (vla-AddSubMenu obj Index<VARIANT> Label<BSTR>) -> IAcadPopupMenu  ; Adds a submenu to an existing menu
    M InsertInMenuBar  (vla-InsertInMenuBar obj Index<VARIANT>)  ; Inserts the popup menu into the AutoCAD menu bar at a specified location
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadPopupMenuItem  ; Gets the member object at a given index in a collection, group, or selection set
    M RemoveFromMenuBar  (vla-RemoveFromMenuBar obj)  ; Removes the popup menu from the AutoCAD menu bar
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P NameNoMnemonic : BSTR  [get READ-ONLY]  vla-get-NameNoMnemonic  ; Specifies the name of the object
    P OnMenuBar : VARIANT_BOOL  [get READ-ONLY]  vla-get-OnMenuBar  ; Determines if the specified popup menu is on the menu bar
    P Parent : IDispatch  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object
    P ShortcutMenu : VARIANT_BOOL  [get READ-ONLY]  vla-get-ShortcutMenu  ; Determines if the specified popup menu is the shortcut menu
    P TagString : BSTR  [get READ-ONLY]  vla-get-TagString  ; Specifies the tag string of the object

[INTERFACE] IAcadPopupMenuItem
Object: AcadPopupMenuItem
Doc: A single menu item on an AutoCAD pull-down menu
  Methods
    M Delete  (vla-Delete obj)  ; Deletes a specified object
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Caption : BSTR  [get READ-ONLY]  vla-get-Caption  ; Gets the text that the user sees displayed for the application or a menu item
    P Check : VARIANT_BOOL  [get/put]  vla-get-Check / vla-put-Check  ; Specifies the check status for the popup menu item
    P Enable : VARIANT_BOOL  [get/put]  vla-get-Enable / vla-put-Enable  ; Enables the popup menu or toolbar item
    P EndSubMenuLevel : long  [get/put]  vla-get-EndSubMenuLevel / vla-put-EndSubMenuLevel  ; Specifies the submenu level for the menu item
    P HelpString : BSTR  [get/put]  vla-get-HelpString / vla-put-HelpString  ; Specifies the help string for the toolbar, toolbar item, or menu item
    P Index : long  [get READ-ONLY]  vla-get-Index  ; Specifies the index of the menu or toolbar item
    P Label : BSTR  [get/put]  vla-get-Label / vla-put-Label  ; Specifies the content and formatting of menu items as they appear to the user
    P Macro : BSTR  [get/put]  vla-get-Macro / vla-put-Macro  ; Specifies the macro for the menu or toolbar item
    P Parent : IAcadPopupMenu  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object
    P SubMenu : IAcadPopupMenu  [get READ-ONLY]  vla-get-SubMenu  ; Gets the popup menu associated with a sub menu
    P TagString : BSTR  [get/put]  vla-get-TagString / vla-put-TagString  ; Specifies the tag string of the object
    P Type : AcMenuItemType  [get READ-ONLY]  vla-get-Type  ; Specifies type of a Leader, MenuGroup, PopupMenuItem, ToolbarItem, Polyline, or PolygonMesh object

[INTERFACE] IAcadPopupMenus
Object: AcadPopupMenus
Doc: A collection of PopupMenu objects representing all the popup menus loaded in the MenuGroup
  Methods
    M Add  (vla-Add obj MenuName<BSTR>) -> IAcadPopupMenu  ; Creates a member object and adds it to the appropriate collection
    M InsertMenuInMenuBar  (vla-InsertMenuInMenuBar obj MenuName<BSTR> Index<VARIANT>)  ; Inserts a menu into the AutoCAD menu bar
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadPopupMenu  ; Gets the member object at a given index in a collection, group, or selection set
    M RemoveMenuFromMenuBar  (vla-RemoveMenuFromMenuBar obj Index<VARIANT>)  ; Removes the popup menu, as specified from the collection, from the AutoCAD menu bar
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Parent : IAcadMenuGroup  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object

[INTERFACE] IAcadPreferences
Object: AcadPreferences
Doc: This object specifies the current AutoCAD settings
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Display : IAcadPreferencesDisplay  [get READ-ONLY]  vla-get-Display  ; Gets the PreferencesDisplay object
    P Drafting : IAcadPreferencesDrafting  [get READ-ONLY]  vla-get-Drafting  ; Gets the PreferencesDrafting object
    P Files : IAcadPreferencesFiles  [get READ-ONLY]  vla-get-Files  ; Gets the PreferencesFiles object
    P OpenSave : IAcadPreferencesOpenSave  [get READ-ONLY]  vla-get-OpenSave  ; Gets the PreferencesOpenSave object
    P Output : IAcadPreferencesOutput  [get READ-ONLY]  vla-get-Output  ; Gets the PreferencesOutput object
    P Profiles : IAcadPreferencesProfiles  [get READ-ONLY]  vla-get-Profiles  ; Gets the PreferencesProfiles object
    P Selection : IAcadPreferencesSelection  [get READ-ONLY]  vla-get-Selection  ; Gets the PreferencesSelection object
    P System : IAcadPreferencesSystem  [get READ-ONLY]  vla-get-System  ; Gets the PreferencesSystem object
    P User : IAcadPreferencesUser  [get READ-ONLY]  vla-get-User  ; Gets the PreferencesUser object

[INTERFACE] IAcadPreferencesDisplay
Object: AcadPreferencesDisplay
Doc: This object contains the options from the Display tab on the Options dialog
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P AutoTrackingVecColor : OLE_COLOR  [get/put]  vla-get-AutoTrackingVecColor / vla-put-AutoTrackingVecColor  ; Specifies the color of the auto tracking vector
    P CursorSize : long  [get/put]  vla-get-CursorSize / vla-put-CursorSize  ; Specifies the crosshairs size as a percentage of the screen size
    P DisplayLayoutTabs : VARIANT_BOOL  [get/put]  vla-get-DisplayLayoutTabs / vla-put-DisplayLayoutTabs  ; Specifies whether to display the Model and Layout tabs in the drawing editor
    P DisplayScreenMenu : VARIANT_BOOL  [get/put]  vla-get-DisplayScreenMenu / vla-put-DisplayScreenMenu  ; Specifies whether to display the screen menu on the right side of the drawing window
    P DisplayScrollBars : VARIANT_BOOL  [get/put]  vla-get-DisplayScrollBars / vla-put-DisplayScrollBars  ; Specifies whether to display scroll bars at the bottom and right sides of the drawing window
    P DockedVisibleLines : long  [get/put]  vla-get-DockedVisibleLines / vla-put-DockedVisibleLines  ; Specifies the number of lines of text to display in the command window
    P GraphicsWinLayoutBackgrndColor : OLE_COLOR  [get/put]  vla-get-GraphicsWinLayoutBackgrndColor / vla-put-GraphicsWinLayoutBackgrndColor  ; Specifies the background color for the paper space layouts
    P GraphicsWinModelBackgrndColor : OLE_COLOR  [get/put]  vla-get-GraphicsWinModelBackgrndColor / vla-put-GraphicsWinModelBackgrndColor  ; Specifies the background color for the model space window
    P HistoryLines : long  [get/put]  vla-get-HistoryLines / vla-put-HistoryLines  ; Specifies the number of lines of text in the text window to keep in memory
    P ImageFrameHighlight : VARIANT_BOOL  [get/put]  vla-get-ImageFrameHighlight / vla-put-ImageFrameHighlight  ; Controls the display of raster images during selection
    P LayoutCreateViewport : VARIANT_BOOL  [get/put]  vla-get-LayoutCreateViewport / vla-put-LayoutCreateViewport  ; Toggles the automatic creation of a viewport for new layouts
    P LayoutCrosshairColor : OLE_COLOR  [get/put]  vla-get-LayoutCrosshairColor / vla-put-LayoutCrosshairColor  ; Specifies the color of the crosshairs and text for paper space layouts
    P LayoutDisplayMargins : VARIANT_BOOL  [get/put]  vla-get-LayoutDisplayMargins / vla-put-LayoutDisplayMargins  ; Toggles the display of margins in layouts
    P LayoutDisplayPaper : VARIANT_BOOL  [get/put]  vla-get-LayoutDisplayPaper / vla-put-LayoutDisplayPaper  ; Toggles the display of the paper background in layouts
    P LayoutDisplayPaperShadow : VARIANT_BOOL  [get/put]  vla-get-LayoutDisplayPaperShadow / vla-put-LayoutDisplayPaperShadow  ; Toggles the display of the paper background shadow in layouts
    P LayoutShowPlotSetup : VARIANT_BOOL  [get/put]  vla-get-LayoutShowPlotSetup / vla-put-LayoutShowPlotSetup  ; Toggles the display of the Plot Setup dialog when a new layout is created
    P MaxAutoCADWindow : VARIANT_BOOL  [get/put]  vla-get-MaxAutoCADWindow / vla-put-MaxAutoCADWindow  ; Specifies if AutoCAD should fill the entire screen area when you start
    P ModelCrosshairColor : OLE_COLOR  [get/put]  vla-get-ModelCrosshairColor / vla-put-ModelCrosshairColor  ; Specifies the color of the crosshairs and text for model space
    P ShowRasterImage : VARIANT_BOOL  [get/put]  vla-get-ShowRasterImage / vla-put-ShowRasterImage  ; Controls the display of raster images during real time pan and zooms
    P TextFont : BSTR  [get/put]  vla-get-TextFont / vla-put-TextFont  ; Specifies the font for new text
    P TextFontSize : long  [get/put]  vla-get-TextFontSize / vla-put-TextFontSize  ; Specifies the font size for new text
    P TextFontStyle : AcTextFontStyle  [get/put]  vla-get-TextFontStyle / vla-put-TextFontStyle  ; Specifies the font style for new text
    P TextWinBackgrndColor : OLE_COLOR  [get/put]  vla-get-TextWinBackgrndColor / vla-put-TextWinBackgrndColor  ; Specifies the background color for the text window
    P TextWinTextColor : OLE_COLOR  [get/put]  vla-get-TextWinTextColor / vla-put-TextWinTextColor  ; Specifies the text color for the text window
    P TrueColorImages : VARIANT_BOOL  [get/put]  vla-get-TrueColorImages / vla-put-TrueColorImages  ; Determines if raster and render images are displayed at true color or palletized color
    P XRefFadeIntensity : long  [get/put]  vla-get-XRefFadeIntensity / vla-put-XRefFadeIntensity  ; Controls the dimming intensity for XRefs

[INTERFACE] IAcadPreferencesDrafting
Object: AcadPreferencesDrafting
Doc: This object contains the options from the Drafting tab on the Options dialog
  Properties
    P AlignmentPointAcquisition : AcAlignmentPointAcquisition  [get/put]  vla-get-AlignmentPointAcquisition / vla-put-AlignmentPointAcquisition  ; Specifies how AutoAlignment points are acquired
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P AutoSnapAperture : VARIANT_BOOL  [get/put]  vla-get-AutoSnapAperture / vla-put-AutoSnapAperture  ; Toggles the display of the AutoSnap aperture
    P AutoSnapApertureSize : long  [get/put]  vla-get-AutoSnapApertureSize / vla-put-AutoSnapApertureSize  ; Specifies the size of the AutoSnap aperture
    P AutoSnapMagnet : VARIANT_BOOL  [get/put]  vla-get-AutoSnapMagnet / vla-put-AutoSnapMagnet  ; Toggles the AutoSnap magnet
    P AutoSnapMarker : VARIANT_BOOL  [get/put]  vla-get-AutoSnapMarker / vla-put-AutoSnapMarker  ; Toggles the AutoSnap marker
    P AutoSnapMarkerColor : AcColor  [get/put]  vla-get-AutoSnapMarkerColor / vla-put-AutoSnapMarkerColor  ; Specifies the color of the AutoSnap marker
    P AutoSnapMarkerSize : long  [get/put]  vla-get-AutoSnapMarkerSize / vla-put-AutoSnapMarkerSize  ; Specifies the size of the AutoSnap marker
    P AutoSnapTooltip : VARIANT_BOOL  [get/put]  vla-get-AutoSnapTooltip / vla-put-AutoSnapTooltip  ; Toggles the AutoSnap tooltips
    P AutoTrackTooltip : VARIANT_BOOL  [get/put]  vla-get-AutoTrackTooltip / vla-put-AutoTrackTooltip  ; Toggles the display of the AutoTrack tooltips
    P FullScreenTrackingVector : VARIANT_BOOL  [get/put]  vla-get-FullScreenTrackingVector / vla-put-FullScreenTrackingVector  ; Toggles the display of full screen tracking vectors
    P PolarTrackingVector : VARIANT_BOOL  [get/put]  vla-get-PolarTrackingVector / vla-put-PolarTrackingVector  ; Toggles the display of polar tracking vectors

[INTERFACE] IAcadPreferencesFiles
Object: AcadPreferencesFiles
Doc: This object contains the options from the Files tab on the Options dialog
  Methods
    M GetProjectFilePath  (vla-GetProjectFilePath obj ProjectName<BSTR>) -> BSTR  ; Gets the directory in which AutoCAD looks for external reference files
    M SetProjectFilePath  (vla-SetProjectFilePath obj ProjectName<BSTR> ProjectFilePath<BSTR>)  ; Sets the directory in which AutoCAD looks for external reference files
  Properties
    P ActiveInvProject : BSTR  [get/put]  vla-get-ActiveInvProject / vla-put-ActiveInvProject  ; Sets the active Inventor project file
    P AltFontFile : BSTR  [get/put]  vla-get-AltFontFile / vla-put-AltFontFile  ; Specifies the location of the font file to use if AutoCAD can't locate the original font and an alternate font is not specified in the font mapping file
    P AltTabletMenuFile : BSTR  [get/put]  vla-get-AltTabletMenuFile / vla-put-AltTabletMenuFile  ; Specifies the path for an alternate menu to swap with the standard AutoCAD tablet menu
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P AutoSavePath : BSTR  [get/put]  vla-get-AutoSavePath / vla-put-AutoSavePath  ; Specifies the path for the file that is created if you enable automatic save using the AutoSaveInterval property
    P ColorBookPath : BSTR  [get/put]  vla-get-ColorBookPath / vla-put-ColorBookPath  ; Sets the Colorbook path.
    P ConfigFile : BSTR  [get READ-ONLY]  vla-get-ConfigFile  ; Gets the location of the configuration file used to store hardware device driver information
    P CustomDictionary : BSTR  [get/put]  vla-get-CustomDictionary / vla-put-CustomDictionary  ; Specifies a custom dictionary to use if you have one
    P CustomIconPath : BSTR  [get/put]  vla-get-CustomIconPath / vla-put-CustomIconPath  ; Specifies the search path for custom icons
    P DefaultInternetURL : BSTR  [get/put]  vla-get-DefaultInternetURL / vla-put-DefaultInternetURL  ; Specifies the default Internet address
    P DriversPath : BSTR  [get/put]  vla-get-DriversPath / vla-put-DriversPath  ; Specifies the directory in which AutoCAD looks for ADI device drivers for the video display, pointing devices, printers, and plotters
    P EnterpriseMenuFile : BSTR  [get/put]  vla-get-EnterpriseMenuFile / vla-put-EnterpriseMenuFile  ; Specifies the location of the enterprise menu file
    P FontFileMap : BSTR  [get/put]  vla-get-FontFileMap / vla-put-FontFileMap  ; Specifies the location of the file that defines how AutoCAD should convert fonts it can't locate
    P HelpFilePath : BSTR  [get/put]  vla-get-HelpFilePath / vla-put-HelpFilePath  ; Specifies the location of the AutoCAD Help file
    P LicenseServer : BSTR  [get READ-ONLY]  vla-get-LicenseServer  ; Provides network administrators a current list of client license servers available to the network license manager program
    P LogFilePath : BSTR  [get/put]  vla-get-LogFilePath / vla-put-LogFilePath  ; Specifies the location for the log file
    P MainDictionary : BSTR  [get/put]  vla-get-MainDictionary / vla-put-MainDictionary  ; Specifies the current dictionary to use for spell checking
    P MenuFile : BSTR  [get/put]  vla-get-MenuFile / vla-put-MenuFile  ; Specifies the location of the AutoCAD menu file for the session
    P ObjectARXPath : BSTR  [get/put]  vla-get-ObjectARXPath / vla-put-ObjectARXPath  ; Specifies the location for ObjectARX applications
    P PageSetupOverridesTemplateFile : BSTR  [get/put]  vla-get-PageSetupOverridesTemplateFile / vla-put-PageSetupOverridesTemplateFile  ; Sets the default template for Page Setup overrides.
    P PlotLogFilePath : BSTR  [get/put]  vla-get-PlotLogFilePath / vla-put-PlotLogFilePath  ; Sets the plot log file path.
    P PostScriptPrologFile : BSTR  [get/put]  vla-get-PostScriptPrologFile / vla-put-PostScriptPrologFile  ; Specifies a name for a customized prolog section in the acad.psf file
    P PrinterConfigPath : BSTR  [get/put]  vla-get-PrinterConfigPath / vla-put-PrinterConfigPath  ; Specifies the location for printer configuration files
    P PrinterDescPath : BSTR  [get/put]  vla-get-PrinterDescPath / vla-put-PrinterDescPath  ; Specifies the location for printer description files
    P PrinterStyleSheetPath : BSTR  [get/put]  vla-get-PrinterStyleSheetPath / vla-put-PrinterStyleSheetPath  ; Specifies the location for printer style sheet files
    P PrintFile : BSTR  [get/put]  vla-get-PrintFile / vla-put-PrintFile  ; Specifies an alternate name to use for the temporary plot file name
    P PrintSpoolerPath : BSTR  [get/put]  vla-get-PrintSpoolerPath / vla-put-PrintSpoolerPath  ; Specifies the directory for the print spool files. AutoCAD writes the plot to this location
    P PrintSpoolExecutable : BSTR  [get/put]  vla-get-PrintSpoolExecutable / vla-put-PrintSpoolExecutable  ; Specifies the application to use for print spooling
    P QNewTemplateFile : BSTR  [get/put]  vla-get-QNewTemplateFile / vla-put-QNewTemplateFile  ; Sets the default template for the QNEW command.
    P SupportPath : BSTR  [get/put]  vla-get-SupportPath / vla-put-SupportPath  ; Specifies the directories where AutoCAD searches for support files
    P TempFilePath : BSTR  [get/put]  vla-get-TempFilePath / vla-put-TempFilePath  ; Specifies the directory AutoCAD uses to store temporary files
    P TemplateDwgPath : BSTR  [get/put]  vla-get-TemplateDwgPath / vla-put-TemplateDwgPath  ; Specifies the path for the template files used by the start-up wizards
    P TempXrefPath : BSTR  [get/put]  vla-get-TempXrefPath / vla-put-TempXrefPath  ; Specifies the location of external reference files
    P TextEditor : BSTR  [get/put]  vla-get-TextEditor / vla-put-TextEditor  ; Specifies the name of the text editor for the MTEXT command
    P TextureMapPath : BSTR  [get/put]  vla-get-TextureMapPath / vla-put-TextureMapPath  ; Specifies the directory in which AutoCAD searches for rendering texture maps
    P ToolPalettePath : BSTR  [get/put]  vla-get-ToolPalettePath / vla-put-ToolPalettePath  ; Sets the ToolPalette path.
    P WorkspacePath : BSTR  [get/put]  vla-get-WorkspacePath / vla-put-WorkspacePath  ; Specifies the path for the database workspace file

[INTERFACE] IAcadPreferencesOpenSave
Object: AcadPreferencesOpenSave
Doc: This object contains the options from the Open and Save tab on the Options dialog
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P AutoAudit : VARIANT_BOOL  [get/put]  vla-get-AutoAudit / vla-put-AutoAudit  ; Specifies if AutoCAD should perform an audit after you render a DXFIN or DXBIN interchange command
    P AutoSaveInterval : long  [get/put]  vla-get-AutoSaveInterval / vla-put-AutoSaveInterval  ; Specifies an automatic save interval in minutes
    P CreateBackup : VARIANT_BOOL  [get/put]  vla-get-CreateBackup / vla-put-CreateBackup  ; Specifies the use of a backup file
    P DemandLoadARXApp : AcARXDemandLoad  [get/put]  vla-get-DemandLoadARXApp / vla-put-DemandLoadARXApp  ; Specifies if and when AutoCAD demand loads a third-party application if a drawing contains custom objects created in that application
    P FullCRCValidation : VARIANT_BOOL  [get/put]  vla-get-FullCRCValidation / vla-put-FullCRCValidation  ; Specifies if a cyclic redundancy check (CRC) should be performed each time an object is read into the drawing
    P IncrementalSavePercent : long  [get/put]  vla-get-IncrementalSavePercent / vla-put-IncrementalSavePercent  ; Specifies the percentage of wasted space allowed in a drawing file
    P LogFileOn : VARIANT_BOOL  [get/put]  vla-get-LogFileOn / vla-put-LogFileOn  ; Specifies if the contents of the text window are written to a log file
    P MRUNumber : long  [get READ-ONLY]  vla-get-MRUNumber  ; Specifies the number of most recently used files that appear in the File menu
    P ProxyImage : AcProxyImage  [get/put]  vla-get-ProxyImage / vla-put-ProxyImage  ; Controls the display of objects in a drawing that were created in a third-party application
    P SaveAsType : AcSaveAsType  [get/put]  vla-get-SaveAsType / vla-put-SaveAsType  ; Specifies the drawing type to save the drawing as
    P SavePreviewThumbnail : VARIANT_BOOL  [get/put]  vla-get-SavePreviewThumbnail / vla-put-SavePreviewThumbnail  ; Specifies if BMP preview images are saved with the drawing
    P ShowProxyDialogBox : VARIANT_BOOL  [get/put]  vla-get-ShowProxyDialogBox / vla-put-ShowProxyDialogBox  ; Specifies if AutoCAD displays a warning message when you open a drawing that contains custom objects
    P TempFileExtension : BSTR  [get/put]  vla-get-TempFileExtension / vla-put-TempFileExtension  ; Specifies the extension for temporary files
    P XrefDemandLoad : AcXRefDemandLoad  [get/put]  vla-get-XrefDemandLoad / vla-put-XrefDemandLoad  ; Specifies demand loading of external references

[INTERFACE] IAcadPreferencesOutput
Object: AcadPreferencesOutput
Doc: This object contains the options from the Output tab on the Options dialog
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P AutomaticPlotLog : VARIANT_BOOL  [get/put]  vla-get-AutomaticPlotLog / vla-put-AutomaticPlotLog  ; Determines whether to automatically save plot and publish log.
    P ContinuousPlotLog : VARIANT_BOOL  [get/put]  vla-get-ContinuousPlotLog / vla-put-ContinuousPlotLog  ; Determines whether to save a continuous plot log.
    P DefaultOutputDevice : BSTR  [get/put]  vla-get-DefaultOutputDevice / vla-put-DefaultOutputDevice  ; Specifies the default output device for new layouts and model space
    P DefaultPlotStyleForLayer : BSTR  [get/put]  vla-get-DefaultPlotStyleForLayer / vla-put-DefaultPlotStyleForLayer  ; Specifies the default plot style for Layer 0 for new drawings or drawings created with earlier releases of AutoCAD never been saved in AutoCAD 2000 format
    P DefaultPlotStyleForObjects : BSTR  [get/put]  vla-get-DefaultPlotStyleForObjects / vla-put-DefaultPlotStyleForObjects  ; Specifies the default plot style table to attach to new drawings
    P DefaultPlotStyleTable : BSTR  [get/put]  vla-get-DefaultPlotStyleTable / vla-put-DefaultPlotStyleTable  ; DefaultPlotStyleTable.
    P DefaultPlotToFilePath : BSTR  [get/put]  vla-get-DefaultPlotToFilePath / vla-put-DefaultPlotToFilePath  ; Sets the default location for plot to file operations.
    P OLEQuality : AcOleQuality  [get/put]  vla-get-OLEQuality / vla-put-OLEQuality  ; Specifies the plot quality of OLE objects
    P PlotLegacy : VARIANT_BOOL  [get/put]  vla-get-PlotLegacy / vla-put-PlotLegacy  ; Toggles if legacy plot scripts are allowed to run
    P PlotPolicy : AcPlotPolicy  [get/put]  vla-get-PlotPolicy / vla-put-PlotPolicy  ; Determines whether an object's color property is associated with its plot style name when creating a new drawing
    P PrinterPaperSizeAlert : VARIANT_BOOL  [get/put]  vla-get-PrinterPaperSizeAlert / vla-put-PrinterPaperSizeAlert  ; Specifies whether to alert the user when a layout is configured with a paper size that is different than the default setting for the PC3 file
    P PrinterSpoolAlert : AcPrinterSpoolAlert  [get/put]  vla-get-PrinterSpoolAlert / vla-put-PrinterSpoolAlert  ; Specifies whether to alert the user when the output to a device must be spooled through a system printer due to a conflict with the I/O port
    P UseLastPlotSettings : VARIANT_BOOL  [get/put]  vla-get-UseLastPlotSettings / vla-put-UseLastPlotSettings  ; Applies the plotting settings of the last successful plot

[INTERFACE] IAcadPreferencesProfiles
Object: AcadPreferencesProfiles
Doc: This object contains the options from the Profiles tab on the Options dialog
  Methods
    M CopyProfile  (vla-CopyProfile obj oldProfileName<BSTR> newProfileName<BSTR>)  ; Copies the specified profile
    M DeleteProfile  (vla-DeleteProfile obj ProfileName<BSTR>)  ; Deletes the specified profile
    M ExportProfile  (vla-ExportProfile obj ProfileName<BSTR> RegFile<BSTR>)  ; Exports the active profile so it can be shared with other users
    M GetAllProfileNames  (vla-GetAllProfileNames obj 'pNames<out VARIANT>)  ; Gets all available profiles for the system
    M ImportProfile  (vla-ImportProfile obj ProfileName<BSTR> RegFile<BSTR> IncludePathInfo<VARIANT_BOOL>)  ; Imports a profile created by another user
    M RenameProfile  (vla-RenameProfile obj origProfileName<BSTR> newProfileName<BSTR>)  ; Renames the specified profile
    M ResetProfile  (vla-ResetProfile obj Profile<BSTR>)  ; Resets the value in the specified profile to its default values
  Properties
    P ActiveProfile : BSTR  [get/put]  vla-get-ActiveProfile / vla-put-ActiveProfile  ; Specifies the active profile for the AutoCAD session
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object

[INTERFACE] IAcadPreferencesSelection
Object: AcadPreferencesSelection
Doc: This object contains the options from the Selection tab on the Options dialog
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P DisplayGrips : VARIANT_BOOL  [get/put]  vla-get-DisplayGrips / vla-put-DisplayGrips  ; Controls the display of selection set grips for the Stretch, Move, Rotate, Scale, and Mirror grip modes
    P DisplayGripsWithinBlocks : VARIANT_BOOL  [get/put]  vla-get-DisplayGripsWithinBlocks / vla-put-DisplayGripsWithinBlocks  ; Controls the assignment of grips within blocks
    P GripColorSelected : AcColor  [get/put]  vla-get-GripColorSelected / vla-put-GripColorSelected  ; Specifies the color of selected grips
    P GripColorUnselected : AcColor  [get/put]  vla-get-GripColorUnselected / vla-put-GripColorUnselected  ; Specifies the color of unselected grips
    P GripSize : long  [get/put]  vla-get-GripSize / vla-put-GripSize  ; Specifies the size of grips
    P PickAdd : VARIANT_BOOL  [get/put]  vla-get-PickAdd / vla-put-PickAdd  ; Determines if objects are added to the selection set using the Shift key
    P PickAuto : VARIANT_BOOL  [get/put]  vla-get-PickAuto / vla-put-PickAuto  ; Controls automatic windowing at the Select Objects prompt
    P PickBoxSize : long  [get/put]  vla-get-PickBoxSize / vla-put-PickBoxSize  ; Specifies the size of the object selection target
    P PickDrag : VARIANT_BOOL  [get/put]  vla-get-PickDrag / vla-put-PickDrag  ; Controls the method of drawing a selection window
    P PickFirst : VARIANT_BOOL  [get/put]  vla-get-PickFirst / vla-put-PickFirst  ; Determines if you select objects before (noun-verb selection) or after you issue a command
    P PickGroup : VARIANT_BOOL  [get/put]  vla-get-PickGroup / vla-put-PickGroup  ; Determines if picking a single object in a group selects the entire group

[INTERFACE] IAcadPreferencesSystem
Object: AcadPreferencesSystem
Doc: This object contains the options from the System tab on the Options dialog
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P BeepOnError : VARIANT_BOOL  [get/put]  vla-get-BeepOnError / vla-put-BeepOnError  ; Specifies if AutoCAD should sound an alarm beep when it detects an invalid entry
    P DisplayOLEScale : VARIANT_BOOL  [get/put]  vla-get-DisplayOLEScale / vla-put-DisplayOLEScale  ; Determines if the OLE scaling dialog is displayed when OLE objects are inserted into a drawing
    P EnableStartupDialog : VARIANT_BOOL  [get/put]  vla-get-EnableStartupDialog / vla-put-EnableStartupDialog  ; Specifies if the Start-up dialog box is displayed when AutoCAD is launched
    P LoadAcadLspInAllDocuments : VARIANT_BOOL  [get/put]  vla-get-LoadAcadLspInAllDocuments / vla-put-LoadAcadLspInAllDocuments  ; LoadAcadLspInAllDocuments.
    P ShowWarningMessages : VARIANT_BOOL  [get/put]  vla-get-ShowWarningMessages / vla-put-ShowWarningMessages  ; Resets all dialog boxes that have the "Don't Display This Warning Again" check box so they display again
    P SingleDocumentMode : VARIANT_BOOL  [get/put]  vla-get-SingleDocumentMode / vla-put-SingleDocumentMode  ; Determines if AutoCAD runs in single- or multiple-document mode
    P StoreSQLIndex : VARIANT_BOOL  [get/put]  vla-get-StoreSQLIndex / vla-put-StoreSQLIndex  ; Determines if the SQL index is stored in the drawing
    P TablesReadOnly : VARIANT_BOOL  [get/put]  vla-get-TablesReadOnly / vla-put-TablesReadOnly  ; Determines whether to open database tables in read-only mode

[INTERFACE] IAcadPreferencesUser
Object: AcadPreferencesUser
Doc: This object contains the options from the User tab on the Options dialog
  Properties
    P ADCInsertUnitsDefaultSource : AcInsertUnits  [get/put]  vla-get-ADCInsertUnitsDefaultSource / vla-put-ADCInsertUnitsDefaultSource  ; Determines the units to automatically use for objects in the AutoCAD DesignCenter for a source drawing that does not have assigned insert units
    P ADCInsertUnitsDefaultTarget : AcInsertUnits  [get/put]  vla-get-ADCInsertUnitsDefaultTarget / vla-put-ADCInsertUnitsDefaultTarget  ; Determines the units to automatically use for objects in the AutoCAD DesignCenter for a target drawing that does not have assigned insert units
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P HyperlinkDisplayCursor : VARIANT_BOOL  [get/put]  vla-get-HyperlinkDisplayCursor / vla-put-HyperlinkDisplayCursor  ; Toggles the display of the hyperlink cursor and shortcut menu
    P KeyboardAccelerator : AcKeyboardAccelerator  [get/put]  vla-get-KeyboardAccelerator / vla-put-KeyboardAccelerator  ; Specifies the Windows standard or AutoCAD classic keyboard
    P KeyboardPriority : AcKeyboardPriority  [get/put]  vla-get-KeyboardPriority / vla-put-KeyboardPriority  ; Controls how AutoCAD responds to the input of coordinate data
    P SCMCommandMode : AcDrawingAreaSCMCommand  [get/put]  vla-get-SCMCommandMode / vla-put-SCMCommandMode  ; Determines right-click functionality in the drawing area while in Command mode, which means that a command is currently in progress
    P SCMDefaultMode : AcDrawingAreaSCMDefault  [get/put]  vla-get-SCMDefaultMode / vla-put-SCMDefaultMode  ; Determines right-click functionality in the drawing area while in Default mode, which means that no objects are selected and no commands are in progress
    P SCMEditMode : AcDrawingAreaSCMEdit  [get/put]  vla-get-SCMEditMode / vla-put-SCMEditMode  ; Determines right-click functionality in the drawing area while in Edit mode, which means that one or more objects are selected and no commands are in progress
    P SCMTimeMode : VARIANT_BOOL  [get/put]  vla-get-SCMTimeMode / vla-put-SCMTimeMode  ; Determines whether time sensitive right-click functionality is on.
    P SCMTimeValue : long  [get/put]  vla-get-SCMTimeValue / vla-put-SCMTimeValue  ; Determines time sensitive right-click functionality longer click duration in milliseconds.
    P ShortCutMenuDisplay : VARIANT_BOOL  [get/put]  vla-get-ShortCutMenuDisplay / vla-put-ShortCutMenuDisplay  ; ShortCutMenuDisplay.

[INTERFACE] IAcadPViewport
Object: AcadPViewport
Inherits: IAcadEntity
Doc: IAcadPViewport Interface
  Methods
    M Display  (vla-Display obj bStatus<VARIANT_BOOL>)  ; Determines whether viewport is On or Off
    M GetGridSpacing  (vla-GetGridSpacing obj 'XSpacing<out double> 'YSpacing<out double>)  ; Specifies the grid spacing for the viewport
    M GetSnapSpacing  (vla-GetSnapSpacing obj 'XSpacing<out double> 'YSpacing<out double>)  ; Specifies the snap spacing for the viewport
    M SetGridSpacing  (vla-SetGridSpacing obj XSpacing<double> YSpacing<double>)  ; Sets the grid spacing for the viewport
    M SetSnapSpacing  (vla-SetSnapSpacing obj XSpacing<double> YSpacing<double>)  ; Sets the snap spacing for the viewport
    M SyncModelView  (vla-SyncModelView obj)  ; Updates the viewport parameters with the parameters in the associated model view.
  Properties
    P ArcSmoothness : long  [get/put]  vla-get-ArcSmoothness / vla-put-ArcSmoothness  ; Specifies the smoothness of circles, arcs, and ellipses
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specify the X, Y, Z coordinate for the center of the viewport or use the Pick Point button to set X, Y, Z values simultaneously
    P Clipped : VARIANT_BOOL  [get READ-ONLY]  vla-get-Clipped  ; Specifies that standard viewport border is replaced with user defined boundary
    P CustomScale : double  [get/put]  vla-get-CustomScale / vla-put-CustomScale  ; Specifies the custom scale for the viewport
    P Direction : VARIANT  [get/put]  vla-get-Direction / vla-put-Direction  ; Specifies the viewing direction for a 3D visualization of the drawing
    P DisplayLocked : VARIANT_BOOL  [get/put]  vla-get-DisplayLocked / vla-put-DisplayLocked  ; Determines whether viewport is in locked state or not
    P GridOn : VARIANT_BOOL  [get/put]  vla-get-GridOn / vla-put-GridOn  ; Specifies the status of the viewport grid
    P HasSheetView : VARIANT_BOOL  [get READ-ONLY]  vla-get-HasSheetView  ; Specifies whether the viewport is linked to a corresponding sheet view
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the viewport
    P LabelBlockId : LONG_PTR  [get/put]  vla-get-LabelBlockId / vla-put-LabelBlockId  ; Returns and sets the label block id associated with the viewport.
    P LayerPropertyOverrides : VARIANT_BOOL  [get READ-ONLY]  vla-get-LayerPropertyOverrides  ; Specifies whether the viewport has layer property overrides.
    P LensLength : double  [get/put]  vla-get-LensLength / vla-put-LensLength  ; Specifies the lens length used in perspective viewing
    P ModelView : IAcadView  [get/put]  vla-get-ModelView / vla-put-ModelView  ; Returns and sets the model view associated with the viewport.
    P RemoveHiddenLines : VARIANT_BOOL  [get/put]  vla-get-RemoveHiddenLines / vla-put-RemoveHiddenLines  ; Determines whether hidden line removal is On or Off
    P ShadePlot : long  [get/put]  vla-get-ShadePlot / vla-put-ShadePlot  ; Specifies the shade plot mode of the viewport
    P SheetView : IAcadView  [get/put]  vla-get-SheetView / vla-put-SheetView  ; Returns and sets the sheet view associated with the viewport.
    P SnapBasePoint : VARIANT  [get/put]  vla-get-SnapBasePoint / vla-put-SnapBasePoint  ; Specifies the snap base point for the viewport
    P SnapOn : VARIANT_BOOL  [get/put]  vla-get-SnapOn / vla-put-SnapOn  ; Specifies the status of snap
    P SnapRotationAngle : double  [get/put]  vla-get-SnapRotationAngle / vla-put-SnapRotationAngle  ; Specifies the snap rotation angle of the viewport relative to the current UCS
    P StandardScale : AcViewportScale  [get/put]  vla-get-StandardScale / vla-put-StandardScale  ; Specifies the standard scale for the viewport
    P StandardScale2 : long  [get/put]  vla-get-StandardScale2 / vla-put-StandardScale2  ; Specifies the standard scale for the viewport
    P StyleSheet : BSTR  [get/put]  vla-get-StyleSheet / vla-put-StyleSheet  ; Returns the style sheet to use
    P Target : VARIANT  [get/put]  vla-get-Target / vla-put-Target  ; Specifies the target point for the view or viewport
    P TwistAngle : ACAD_ANGLE  [get/put]  vla-get-TwistAngle / vla-put-TwistAngle  ; Specifies the twist angle for the viewport
    P UCSIconAtOrigin : VARIANT_BOOL  [get/put]  vla-get-UCSIconAtOrigin / vla-put-UCSIconAtOrigin  ; Specifies if the UCS icon is displayed at the origin
    P UCSIconOn : VARIANT_BOOL  [get/put]  vla-get-UCSIconOn / vla-put-UCSIconOn  ; Specifies if the UCS icon is on
    P UCSPerViewport : VARIANT_BOOL  [get/put]  vla-get-UCSPerViewport / vla-put-UCSPerViewport  ; Determines whether the UCS is saved with the viewport or not
    P ViewportOn : VARIANT_BOOL  [get/put]  vla-get-ViewportOn / vla-put-ViewportOn  ; Determines whether the viewport is On or Off
    P VisualStyle : long  [get/put]  vla-get-VisualStyle / vla-put-VisualStyle  ; Specifies the visual style of the viewport
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the viewport

[INTERFACE] IAcadRasterImage
Object: AcadRasterImage
Inherits: IAcadEntity
Doc: AutoCAD Raster Interface
  Methods
    M ClipBoundary  (vla-ClipBoundary obj boundry<VARIANT>)  ; Set clipping boundary
  Properties
    P Brightness : long  [get/put]  vla-get-Brightness / vla-put-Brightness  ; Specifies the current brightness value of the raster image
    P ClippingEnabled : VARIANT_BOOL  [get/put]  vla-get-ClippingEnabled / vla-put-ClippingEnabled  ; Enables or disables the clipping boundary of the image
    P Contrast : long  [get/put]  vla-get-Contrast / vla-put-Contrast  ; Specifies the current contrast value of the raster image
    P Fade : long  [get/put]  vla-get-Fade / vla-put-Fade  ; Specifies the current fade value of the raster image
    P Height : double  [get READ-ONLY]  vla-get-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P ImageFile : BSTR  [get/put]  vla-get-ImageFile / vla-put-ImageFile  ; Specifies the path to the image file
    P ImageHeight : double  [get/put]  vla-get-ImageHeight / vla-put-ImageHeight  ; Specifies the height of the raster image
    P ImageVisibility : VARIANT_BOOL  [get/put]  vla-get-ImageVisibility / vla-put-ImageVisibility  ; Determines whether image is visible or not
    P ImageWidth : double  [get/put]  vla-get-ImageWidth / vla-put-ImageWidth  ; Specifies the width of the raster image
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the image file
    P Origin : VARIANT  [get/put]  vla-get-Origin / vla-put-Origin  ; Specifies the origin coordinates (lower left corner) of the raster image
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the raster image
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the scale factor of the raster image
    P ShowRotation : VARIANT_BOOL  [get/put]  vla-get-ShowRotation / vla-put-ShowRotation  ; Determines if a raster image is displayed at its rotation value
    P transparency : VARIANT_BOOL  [get/put]  vla-get-transparency / vla-put-transparency  ; Determines whether transparency for a bitonal image is On or Off
    P Width : double  [get READ-ONLY]  vla-get-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window

[INTERFACE] IAcadRay
Object: AcadRay
Inherits: IAcadEntity
Doc: AutoCAD Ray Interface
  Properties
    P BasePoint : VARIANT  [get/put]  vla-get-BasePoint / vla-put-BasePoint  ; Specify the X, Y, Z coordinate of the basepoint of the ray or use the Pick Point button to set X, Y, Z values simultaneously
    P DirectionVector : VARIANT  [get/put]  vla-get-DirectionVector / vla-put-DirectionVector  ; Specify the X, Y, Z direction vectors of the ray
    P SecondPoint : VARIANT  [get/put]  vla-get-SecondPoint / vla-put-SecondPoint  ; Specify the X, Y, Z coordinate of the second point of the ray or use the Pick Point button to set X, Y, Z values simultaneously

[INTERFACE] IAcadRegion
Object: AcadRegion
Inherits: IAcadEntity
Doc: AutoCAD Region Interface
  Methods
    M Boolean  (vla-Boolean obj Operation<AcBooleanType> Object<IAcadRegion*>)  ; Perform a Boolean operation against another region.
    M Explode  (vla-Explode obj) -> VARIANT  ; Explodes the region and returns the sub-entities as an array of object.
  Properties
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the region
    P Centroid : VARIANT  [get READ-ONLY]  vla-get-Centroid  ; Gets the center of area or mass for a region or solid
    P MomentOfInertia : VARIANT  [get READ-ONLY]  vla-get-MomentOfInertia  ; Gets the moment of inertia for the solid
    P Normal : VARIANT  [get READ-ONLY]  vla-get-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Perimeter : double  [get READ-ONLY]  vla-get-Perimeter  ; Specifies the perimeter of the region
    P PrincipalDirections : VARIANT  [get READ-ONLY]  vla-get-PrincipalDirections  ; Gets the principal directions of the solid or region
    P PrincipalMoments : VARIANT  [get READ-ONLY]  vla-get-PrincipalMoments  ; Gets the principal moments property of the solid or region
    P ProductOfInertia : double  [get READ-ONLY]  vla-get-ProductOfInertia  ; Gets the product of inertia of the solid or region
    P RadiiOfGyration : VARIANT  [get READ-ONLY]  vla-get-RadiiOfGyration  ; Gets the radius of gyration of the solid or region

[INTERFACE] IAcadRegisteredApplication
Object: AcadRegisteredApplication
Inherits: IAcadObject
Doc: An external application that has been added to the drawing
  Properties
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object

[INTERFACE] IAcadRegisteredApplications
Object: AcadRegisteredApplications
Inherits: IAcadObject
Doc: The collection of all registered applications in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadRegisteredApplication  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadRegisteredApplication  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadRevolvedSurface
Object: AcadRevolvedSurface
Inherits: IAcadSurface
Doc: IAcadRevolvedSurface Interface
  Properties
    P AxisDirection : VARIANT  [get READ-ONLY]  vla-get-AxisDirection  ; Displays the direction of the axis of revolution
    P AxisPosition : VARIANT  [get/put]  vla-get-AxisPosition / vla-put-AxisPosition  ; Specifies the start point of the axis of revolution
    P RevolutionAngle : ACAD_ANGLE  [get/put]  vla-get-RevolutionAngle / vla-put-RevolutionAngle  ; Specifies the angle of revolution

[INTERFACE] IAcadSection
Inherits: IAcadEntity
Doc: IAcadSection Interface
  Methods
    M AddVertex  (vla-AddVertex obj nIndex<long> val<VARIANT>)  ; Adds a new vertex to the section line
    M CreateJog  (vla-CreateJog obj varPt<VARIANT>)  ; Creates a jog on the section plane
    M GenerateSectionGeometry  (vla-GenerateSectionGeometry obj pEntity<IAcadEntity*> 'pIntersectionBoundaryObjs<out VARIANT> 'pIntersectionFillObjs<out VARIANT> 'pBackgroudnObjs<out VARIANT> 'pForegroudObjs<out VARIANT> 'pCurveTangencyObjs<out VARIANT>)  ; Generates 2D or 3D section geometry
    M HitTest  (vla-HitTest obj varPtHit<VARIANT> 'pHit<out VARIANT_BOOL> 'pSegmentIndex<out long> 'pPtOnSegment<out VARIANT> 'pSubItem<out AcSectionSubItem>)  ; Does hit test on section plane
    M RemoveVertex  (vla-RemoveVertex obj nIndex<long>)  ; Removes a vertex in the section line
  Properties
    P BottomHeight : double  [get/put]  vla-get-BottomHeight / vla-put-BottomHeight  ; Specifies elevation of section plane bottom extents relative to the object's elevation
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the co-ordinate of the specified vertex
    P Elevation : double  [get/put]  vla-get-Elevation / vla-put-Elevation  ; Specifies elevation of section plane line
    P IndicatorFillColor : IAcadAcCmColor  [get/put]  vla-get-IndicatorFillColor / vla-put-IndicatorFillColor  ; Specifies color of section plane when shading is turned on
    P IndicatorTransparency : long  [get/put]  vla-get-IndicatorTransparency / vla-put-IndicatorTransparency  ; Specifies transparency of section plane when shading is turned on
    P LiveSectionEnabled : VARIANT_BOOL  [get/put]  vla-get-LiveSectionEnabled / vla-put-LiveSectionEnabled  ; Turns live section on or off for this section object
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies section object name
    P Normal : VARIANT  [get READ-ONLY]  vla-get-Normal  ; Specifies normal for the section plane
    P NumVertices : long  [get READ-ONLY]  vla-get-NumVertices  ; Gets the number of vertices in the section line
    P Settings : IAcadSectionSettings  [get READ-ONLY]  vla-get-Settings  ; Gets the section settings object
    P State : AcSectionState  [get/put]  vla-get-State / vla-put-State  ; Specifies section object type
    P TopHeight : double  [get/put]  vla-get-TopHeight / vla-put-TopHeight  ; Specifies elevation of section plane top extents relative to the object's elevation
    P VerticalDirection : VARIANT  [get/put]  vla-get-VerticalDirection / vla-put-VerticalDirection  ; Specifies the vertical direction for the section plane
    P Vertices : VARIANT  [get/put]  vla-get-Vertices / vla-put-Vertices  ; Gets the vertices in the section line
    P ViewingDirection : VARIANT  [get/put]  vla-get-ViewingDirection / vla-put-ViewingDirection  ; Specifies the viewing direction for the section plane

[INTERFACE] IAcadSection2
Object: AcadSection
Inherits: IAcadSection
Doc: IAcadSection Interface
  Properties
    P SectionPlaneOffset : double  [get/put]  vla-get-SectionPlaneOffset / vla-put-SectionPlaneOffset  ; Specifies off set of section plane
    P SliceDepth : double  [get/put]  vla-get-SliceDepth / vla-put-SliceDepth
    P State2 : AcSectionState2  [get/put]  vla-get-State2 / vla-put-State2  ; Specifies section object type

[INTERFACE] IAcadSectionManager
Object: AcadSectionManager
Inherits: IAcadObject
Doc: Controls which property is constrained when editing other property values
  Methods
    M GetLiveSection  (vla-GetLiveSection obj) -> IAcadSection  ; Gets the section whose live section is currently active
    M GetUniqueSectionName  (vla-GetUniqueSectionName obj pBaseName<BSTR>) -> BSTR  ; Finds an unique name for secion
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadSection  ; Returns an item in the collection.
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of sections in the database

[INTERFACE] IAcadSectionSettings
Object: AcadSectionSettings
Inherits: IAcadObject
Doc: IAcadSectionSettings Interface
  Methods
    M GetSectionTypeSettings  (vla-GetSectionTypeSettings obj secType<AcSectionType>) -> IAcadSectionTypeSettings  ; Gets the section type settings object
  Properties
    P CurrentSectionType : AcSectionType  [get/put]  vla-get-CurrentSectionType / vla-put-CurrentSectionType  ; Specifies the current section type

[INTERFACE] IAcadSectionTypeSettings
Doc: IAcadSectionTypeSettings Interface
  Properties
    P BackgroundLinesColor : IAcadAcCmColor  [get/put]  vla-get-BackgroundLinesColor / vla-put-BackgroundLinesColor  ; Specifies the color of background lines
    P BackgroundLinesHiddenLine : VARIANT_BOOL  [get/put]  vla-get-BackgroundLinesHiddenLine / vla-put-BackgroundLinesHiddenLine  ; Specifies the hidden line visibility of background lines
    P BackgroundLinesLayer : BSTR  [get/put]  vla-get-BackgroundLinesLayer / vla-put-BackgroundLinesLayer  ; Specifies the layer of background lines
    P BackgroundLinesLinetype : BSTR  [get/put]  vla-get-BackgroundLinesLinetype / vla-put-BackgroundLinesLinetype  ; Specifies the linetype of background lines
    P BackgroundLinesLinetypeScale : ACAD_NOUNITS  [get/put]  vla-get-BackgroundLinesLinetypeScale / vla-put-BackgroundLinesLinetypeScale  ; Specifies the linetype scale of background lines
    P BackgroundLinesLineweight : ACAD_LWEIGHT  [get/put]  vla-get-BackgroundLinesLineweight / vla-put-BackgroundLinesLineweight  ; Specifies the line weight of background lines
    P BackgroundLinesPlotStyleName : BSTR  [get/put]  vla-get-BackgroundLinesPlotStyleName / vla-put-BackgroundLinesPlotStyleName  ; Specifies the plot style name of background lines
    P BackgroundLinesVisible : VARIANT_BOOL  [get/put]  vla-get-BackgroundLinesVisible / vla-put-BackgroundLinesVisible  ; Specifies the visibility of background lines
    P CurveTangencyLinesColor : IAcadAcCmColor  [get/put]  vla-get-CurveTangencyLinesColor / vla-put-CurveTangencyLinesColor  ; Specifies the color of curve tangency lines
    P CurveTangencyLinesLayer : BSTR  [get/put]  vla-get-CurveTangencyLinesLayer / vla-put-CurveTangencyLinesLayer  ; Specifies the layer of curve tangency lines
    P CurveTangencyLinesLinetype : BSTR  [get/put]  vla-get-CurveTangencyLinesLinetype / vla-put-CurveTangencyLinesLinetype  ; Specifies the linetype of curve tangency lines
    P CurveTangencyLinesLinetypeScale : ACAD_NOUNITS  [get/put]  vla-get-CurveTangencyLinesLinetypeScale / vla-put-CurveTangencyLinesLinetypeScale  ; Specifies the linetype scale of curve tangency lines
    P CurveTangencyLinesLineweight : ACAD_LWEIGHT  [get/put]  vla-get-CurveTangencyLinesLineweight / vla-put-CurveTangencyLinesLineweight  ; Specifies the line weight of curve tangency lines
    P CurveTangencyLinesPlotStyleName : BSTR  [get/put]  vla-get-CurveTangencyLinesPlotStyleName / vla-put-CurveTangencyLinesPlotStyleName  ; Specifies the plot style name of curve tangency lines
    P CurveTangencyLinesVisible : VARIANT_BOOL  [get/put]  vla-get-CurveTangencyLinesVisible / vla-put-CurveTangencyLinesVisible  ; Specifies the visibility of curve tangency lines
    P DestinationBlock : VARIANT  [get/put]  vla-get-DestinationBlock / vla-put-DestinationBlock  ; Specifies the destination block for section generation
    P DestinationFile : BSTR  [get/put]  vla-get-DestinationFile / vla-put-DestinationFile  ; Specifies the destination file for section generation
    P ForegroundLinesColor : IAcadAcCmColor  [get/put]  vla-get-ForegroundLinesColor / vla-put-ForegroundLinesColor  ; Specifies the color of foreground lines
    P ForegroundLinesEdgeTransparency : long  [get/put]  vla-get-ForegroundLinesEdgeTransparency / vla-put-ForegroundLinesEdgeTransparency  ; Specifies the edge transparency of foreground lines
    P ForegroundLinesFaceTransparency : long  [get/put]  vla-get-ForegroundLinesFaceTransparency / vla-put-ForegroundLinesFaceTransparency  ; Specifies the face transparency of foreground lines
    P ForegroundLinesHiddenLine : VARIANT_BOOL  [get/put]  vla-get-ForegroundLinesHiddenLine / vla-put-ForegroundLinesHiddenLine  ; Specifies the hidden line visibility of foreground lines
    P ForegroundLinesLayer : BSTR  [get/put]  vla-get-ForegroundLinesLayer / vla-put-ForegroundLinesLayer  ; Specifies the layer of foreground lines
    P ForegroundLinesLinetype : BSTR  [get/put]  vla-get-ForegroundLinesLinetype / vla-put-ForegroundLinesLinetype  ; Specifies the linetype of foreground lines
    P ForegroundLinesLinetypeScale : ACAD_NOUNITS  [get/put]  vla-get-ForegroundLinesLinetypeScale / vla-put-ForegroundLinesLinetypeScale  ; Specifies the linetype scale of foreground lines
    P ForegroundLinesLineweight : ACAD_LWEIGHT  [get/put]  vla-get-ForegroundLinesLineweight / vla-put-ForegroundLinesLineweight  ; Specifies the line weight of foreground lines
    P ForegroundLinesPlotStyleName : BSTR  [get/put]  vla-get-ForegroundLinesPlotStyleName / vla-put-ForegroundLinesPlotStyleName  ; Specifies the plot style name of foreground lines
    P ForegroundLinesVisible : VARIANT_BOOL  [get/put]  vla-get-ForegroundLinesVisible / vla-put-ForegroundLinesVisible  ; Specifies the visibility of foreground lines
    P GenerationOptions : AcSectionGeneration  [get/put]  vla-get-GenerationOptions / vla-put-GenerationOptions  ; Specifies the section generation options
    P IntersectionBoundaryColor : IAcadAcCmColor  [get/put]  vla-get-IntersectionBoundaryColor / vla-put-IntersectionBoundaryColor  ; Specifies the color of intersection boundary
    P IntersectionBoundaryDivisionLines : VARIANT_BOOL  [get/put]  vla-get-IntersectionBoundaryDivisionLines / vla-put-IntersectionBoundaryDivisionLines  ; Specifies whether division lines are shown in intersection boundary
    P IntersectionBoundaryLayer : BSTR  [get/put]  vla-get-IntersectionBoundaryLayer / vla-put-IntersectionBoundaryLayer  ; Specifies the layer of intersection boundary
    P IntersectionBoundaryLinetype : BSTR  [get/put]  vla-get-IntersectionBoundaryLinetype / vla-put-IntersectionBoundaryLinetype  ; Specifies the linetype of intersection boundary
    P IntersectionBoundaryLinetypeScale : ACAD_NOUNITS  [get/put]  vla-get-IntersectionBoundaryLinetypeScale / vla-put-IntersectionBoundaryLinetypeScale  ; Specifies the linetype scale of intersection boundary
    P IntersectionBoundaryLineweight : ACAD_LWEIGHT  [get/put]  vla-get-IntersectionBoundaryLineweight / vla-put-IntersectionBoundaryLineweight  ; Specifies the line weight of intersection boundary
    P IntersectionBoundaryPlotStyleName : BSTR  [get/put]  vla-get-IntersectionBoundaryPlotStyleName / vla-put-IntersectionBoundaryPlotStyleName  ; Specifies the plot style name of intersection boundary
    P IntersectionFillColor : IAcadAcCmColor  [get/put]  vla-get-IntersectionFillColor / vla-put-IntersectionFillColor  ; Specifies the color of intersection fill
    P IntersectionFillFaceTransparency : long  [get/put]  vla-get-IntersectionFillFaceTransparency / vla-put-IntersectionFillFaceTransparency  ; Specifies the face transparency of intersection fill
    P IntersectionFillHatchAngle : ACAD_ANGLE  [get/put]  vla-get-IntersectionFillHatchAngle / vla-put-IntersectionFillHatchAngle  ; Specifies the hatch angle for intersection fill
    P IntersectionFillHatchPatternName : BSTR  [get/put]  vla-get-IntersectionFillHatchPatternName / vla-put-IntersectionFillHatchPatternName  ; Specifies the hatch pattern name for intersection fill
    P IntersectionFillHatchPatternType : AcPatternType  [get/put]  vla-get-IntersectionFillHatchPatternType / vla-put-IntersectionFillHatchPatternType  ; Specifies the hatch pattern type for intersection fill
    P IntersectionFillHatchScale : ACAD_NOUNITS  [get/put]  vla-get-IntersectionFillHatchScale / vla-put-IntersectionFillHatchScale  ; Specifies the hatch scale for intersection fill
    P IntersectionFillHatchSpacing : double  [get/put]  vla-get-IntersectionFillHatchSpacing / vla-put-IntersectionFillHatchSpacing  ; Specifies the hatch spacing for intersection fill
    P IntersectionFillLayer : BSTR  [get/put]  vla-get-IntersectionFillLayer / vla-put-IntersectionFillLayer  ; Specifies the layer of intersection fill
    P IntersectionFillLinetype : BSTR  [get/put]  vla-get-IntersectionFillLinetype / vla-put-IntersectionFillLinetype  ; Specifies the linetype of intersection fill
    P IntersectionFillLinetypeScale : ACAD_NOUNITS  [get/put]  vla-get-IntersectionFillLinetypeScale / vla-put-IntersectionFillLinetypeScale  ; Specifies the linetype scale of intersection fill
    P IntersectionFillLineweight : ACAD_LWEIGHT  [get/put]  vla-get-IntersectionFillLineweight / vla-put-IntersectionFillLineweight  ; Specifies the line weight of intersection fill
    P IntersectionFillPlotStyleName : BSTR  [get/put]  vla-get-IntersectionFillPlotStyleName / vla-put-IntersectionFillPlotStyleName  ; Specifies the plot style name of intersection fill
    P IntersectionFillVisible : VARIANT_BOOL  [get/put]  vla-get-IntersectionFillVisible / vla-put-IntersectionFillVisible  ; Specifies the visibility of intersection fill
    P SourceObjects : VARIANT  [get/put]  vla-get-SourceObjects / vla-put-SourceObjects  ; Specifies the source objects for section generation

[INTERFACE] IAcadSectionTypeSettings2
Object: AcadSectionTypeSettings
Inherits: IAcadSectionTypeSettings
Doc: IAcadSectionTypeSettings Interface
  Properties
    P IntersectionBoundaryVisible : VARIANT_BOOL  [get/put]  vla-get-IntersectionBoundaryVisible / vla-put-IntersectionBoundaryVisible  ; Specifies the visibility of intersection boundary

[INTERFACE] IAcadSecurityParams
Object: AcadSecurityParams
Doc: Security Parameters Interface
  Properties
    P Action : long  [get/put]  vla-get-Action / vla-put-Action  ; Specifies the security-related operations to be performed.
    P Algorithm : long  [get/put]  vla-get-Algorithm / vla-put-Algorithm  ; Specifies the encryption algorithm identifier.
    P Comment : BSTR  [get/put]  vla-get-Comment / vla-put-Comment  ; Specifies the comment to be included with the digital signature.
    P Issuer : BSTR  [get/put]  vla-get-Issuer / vla-put-Issuer  ; Specifies the issuer name of the digital certificate.
    P KeyLength : long  [get/put]  vla-get-KeyLength / vla-put-KeyLength  ; Specifies the length of the encryption key.
    P Password : BSTR  [get/put]  vla-get-Password / vla-put-Password  ; Specifies the encryption password.
    P ProviderName : BSTR  [get/put]  vla-get-ProviderName / vla-put-ProviderName  ; Specifies the encryption provider name.
    P ProviderType : long  [get/put]  vla-get-ProviderType / vla-put-ProviderType  ; Specifies the encryption provider type.
    P SerialNumber : BSTR  [get/put]  vla-get-SerialNumber / vla-put-SerialNumber  ; Specifies the serial number of the digital certificate.
    P Subject : BSTR  [get/put]  vla-get-Subject / vla-put-Subject  ; Specifies the subject name of the digital certificate
    P TimeServer : BSTR  [get/put]  vla-get-TimeServer / vla-put-TimeServer  ; Specifies the name of the time server to be used for the digital signature.

[INTERFACE] IAcadSelectionSet
Object: AcadSelectionSet
Doc: A group of one or more AutoCAD objects specified for processing as a single unit
  Methods
    M AddItems  (vla-AddItems obj pSelSet<VARIANT>)  ; Adds one or more objects to the specified selection set
    M Clear  (vla-Clear obj)  ; Clears the specified selection set of all items
    M Delete  (vla-Delete obj)  ; Deletes a specified object
    M Erase  (vla-Erase obj)  ; Erases all the objects in a selection set
    M Highlight  (vla-Highlight obj bFlag<VARIANT_BOOL>)  ; Sets the highlight status for the given object, or for all objects in a given selection set
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadEntity  ; Gets the member object at a given index in a collection, group, or selection set
    M RemoveItems  (vla-RemoveItems obj Objects<VARIANT>)  ; Removes specified items from the group or selection set
    M Select  (vla-Select obj Mode<AcSelect> [Point1<VARIANT>] [Point2<VARIANT>] [FilterType<VARIANT>] [FilterData<VARIANT>])  ; Selects objects and places them into a selection set
    M SelectAtPoint  (vla-SelectAtPoint obj Point<VARIANT> [FilterType<VARIANT>] [FilterData<VARIANT>])  ; Selects an object passing through a given point and places it into a selection set
    M SelectByPolygon  (vla-SelectByPolygon obj Mode<AcSelect> PointsList<VARIANT> [FilterType<VARIANT>] [FilterData<VARIANT>])  ; Selects entities within a fence and adds them to the selection set
    M SelectOnScreen  (vla-SelectOnScreen obj [FilterType<VARIANT>] [FilterData<VARIANT>])  ; Prompts the user to pick an object from the screen
    M Update  (vla-Update obj)  ; Updates the object to the drawing screen
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Name : BSTR  [get READ-ONLY]  vla-get-Name  ; Specifies the name of the object

[INTERFACE] IAcadSelectionSets
Object: AcadSelectionSets
Doc: The collection of all selection sets in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadSelectionSet  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadSelectionSet  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadShadowDisplay
Doc: AutoCAD Entity with Shadows interface
  Properties
    P EnableShadowDisplay : long  [get READ-ONLY]  vla-get-EnableShadowDisplay  ; Specifies whether the shadow display property is enabled for the object.
    P ShadowDisplay : AcShadowDisplayType  [get/put]  vla-get-ShadowDisplay / vla-put-ShadowDisplay  ; Specifies the shadow display property of the object.

[INTERFACE] IAcadShape
Object: AcadShape
Inherits: IAcadEntity
Doc: AutoCAD Shape Interface
  Properties
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the shape
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specify X, Y, Z coordinate for the insertion point of the shape or use the Pick Point button to set X, Y, Z values simultaneously
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the shape
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P ObliqueAngle : ACAD_ANGLE  [get/put]  vla-get-ObliqueAngle / vla-put-ObliqueAngle  ; Specifies the oblique angle of the shape
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the shape
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the width scale factor of the shape
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the shape

[INTERFACE] IAcadSolid
Object: AcadSolid
Inherits: IAcadEntity
Doc: AutoCAD Solid Interface
  Properties
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the current vertex of the solid
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the solid

[INTERFACE] IAcadSortentsTable
Object: AcadSortentsTable
Inherits: IAcadObject
Doc: AutoCAD AcadSortentsTable Interface
  Methods
    M Block  (vla-Block obj) -> IAcadBlock  ; Returns the Block this Sortents Table belongs to.
    M GetFullDrawOrder  (vla-GetFullDrawOrder obj 'Objects<out VARIANT> honorSortentsSysvar<VARIANT_BOOL>)  ; Returns all objects in the block, sorted by draw order, bottommost first.
    M GetRelativeDrawOrder  (vla-GetRelativeDrawOrder obj 'Objects<out VARIANT> honorSortentsSysvar<VARIANT_BOOL>)  ; Returns specified objects, sorted by draw order, bottommost first.
    M MoveAbove  (vla-MoveAbove obj Objects<VARIANT> Target<IAcadEntity*>)  ; Moves objects above target in draw order.
    M MoveBelow  (vla-MoveBelow obj Objects<VARIANT> Target<IAcadEntity*>)  ; Moves objects below target in draw order.
    M MoveToBottom  (vla-MoveToBottom obj Objects<VARIANT>)  ; Moves objects to bottom of draw order.
    M MoveToTop  (vla-MoveToTop obj Objects<VARIANT>)  ; Moves objects to top of draw order.
    M SetRelativeDrawOrder  (vla-SetRelativeDrawOrder obj Objects<VARIANT>)  ; Sets the relative draw order of the objects to the order specifed, bottommost first.
    M SwapOrder  (vla-SwapOrder obj Object1<IAcadEntity*> Object2<IAcadEntity*>)  ; Swaps draw order position for two objects.

[INTERFACE] IAcadSpline
Object: AcadSpline
Inherits: IAcadEntity
Doc: AutoCAD Spline Interface
  Methods
    M AddFitPoint  (vla-AddFitPoint obj Index<long> fitPoint<VARIANT>)  ; Adds the fit point to the spline at a given index
    M DeleteFitPoint  (vla-DeleteFitPoint obj Index<long>)  ; Deletes the fit point of the spline at a given index
    M ElevateOrder  (vla-ElevateOrder obj Order<long>)  ; Elevates the order of the spline
    M GetControlPoint  (vla-GetControlPoint obj Index<long>) -> VARIANT  ; Returns the control point of the spline at a given index
    M GetFitPoint  (vla-GetFitPoint obj Index<long>) -> VARIANT  ; Returns the fit point of the spline at a given index
    M GetWeight  (vla-GetWeight obj Index<long>) -> double  ; Returns the weight of the spline at a given control point index
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Creates a new entity object by offsetting the spline by a given value
    M PurgeFitData  (vla-PurgeFitData obj)  ; Purges the fit data of the spline
    M Reverse  (vla-Reverse obj)  ; Reverses the direction of the spline
    M SetControlPoint  (vla-SetControlPoint obj Index<long> controlPoint<VARIANT>)  ; Sets the indexed control point of the spline at a specified point
    M SetFitPoint  (vla-SetFitPoint obj Index<long> fitPoint<VARIANT>)  ; Sets the indexed fit point of the spline at a specified point
    M SetWeight  (vla-SetWeight obj Index<long> weight<double>)  ; Sets the weight of the spline at a given control point index
  Properties
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the spline
    P Closed : VARIANT_BOOL  [get READ-ONLY]  vla-get-Closed  ; Specifies whether the spline is open or closed
    P Closed2 : VARIANT_BOOL  [get/put]  vla-get-Closed2 / vla-put-Closed2  ; Specifies whether the spline is open or closed
    P ControlPoints : VARIANT  [get/put]  vla-get-ControlPoints / vla-put-ControlPoints  ; Specifies the current control point of the spline
    P Degree : long  [get READ-ONLY]  vla-get-Degree  ; Specifies the degree of the spline
    P Degree2 : long  [get/put]  vla-get-Degree2 / vla-put-Degree2  ; Specifies the degree of the spline
    P EndTangent : VARIANT  [get/put]  vla-get-EndTangent / vla-put-EndTangent  ; Specify the X, Y, Z coordinate for end tangent of the spline
    P FitPoints : VARIANT  [get/put]  vla-get-FitPoints / vla-put-FitPoints  ; Specifies the current fit point of the spline
    P FitTolerance : double  [get/put]  vla-get-FitTolerance / vla-put-FitTolerance  ; Specifies the fit tolerance of the spline
    P IsPeriodic : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsPeriodic  ; Determines if the given spline is periodic
    P IsPlanar : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsPlanar  ; Determines the whether the spline is planar
    P IsRational : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsRational  ; Determines if the given spline is planar
    P KnotParameterization : AcSplineKnotParameterizationType  [get/put]  vla-get-KnotParameterization / vla-put-KnotParameterization  ; Specifies knot spacing when spline was created
    P Knots : VARIANT  [get/put]  vla-get-Knots / vla-put-Knots  ; Gets the knot vector for a spline
    P NumberOfControlPoints : long  [get READ-ONLY]  vla-get-NumberOfControlPoints  ; Specifies the number of control points of the spline
    P NumberOfFitPoints : long  [get READ-ONLY]  vla-get-NumberOfFitPoints  ; Specifies the number of fit points of the spline
    P SplineFrame : AcSplineFrameType  [get/put]  vla-get-SplineFrame / vla-put-SplineFrame  ; Specifies whether displaying the CV Hull for spline
    P SplineMethod : AcSplineMethodType  [get/put]  vla-get-SplineMethod / vla-put-SplineMethod  ; Specifies whether fit points or CV's are displayed when selected
    P StartTangent : VARIANT  [get/put]  vla-get-StartTangent / vla-put-StartTangent  ; Specify the X, Y, Z coordinate for start tangent of the spline
    P Weights : VARIANT  [get/put]  vla-get-Weights / vla-put-Weights  ; Gets the weight vector for spline

[INTERFACE] IAcadState
Object: AcadState
Doc: AutoCAD State Interface
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P IsQuiescent : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsQuiescent  ; Specifies if AutoCAD is idle and accepting out of process Automation requests.

[INTERFACE] IAcadSubDMesh
Object: AcadSubDMesh
Inherits: IAcadEntity
Doc: AutoCAD SubDMesh Interface
  Properties
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Returns the coordinate of the vertex at a given index
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the current vertex on the mesh
    P FaceCount : long  [get READ-ONLY]  vla-get-FaceCount  ; Specifies the number of faces for the unsmooth mesh
    P Smoothness : long  [get/put]  vla-get-Smoothness / vla-put-Smoothness  ; Specifies the smoothing level for the mesh
    P VertexCount : long  [get READ-ONLY]  vla-get-VertexCount  ; Specifies the number of vertices for the unsmooth mesh

[INTERFACE] IAcadSubDMeshEdge
Object: AcadSubDMeshEdge
Inherits: IAcadSubEntity
Doc: AutoCAD SubDMesh Edge Interface
  Properties
    P CreaseLevel : double  [get/put]  vla-get-CreaseLevel / vla-put-CreaseLevel  ; Specifies crease level at which the crease starts losing its effect
    P CreaseType : AcMeshCreaseType  [get/put]  vla-get-CreaseType / vla-put-CreaseType  ; Specifies if a crease is applied

[INTERFACE] IAcadSubDMeshFace
Object: AcadSubDMeshFace
Inherits: IAcadSubEntity
Doc: AutoCAD SubDMesh Face Interface
  Properties
    P CreaseLevel : double  [get/put]  vla-get-CreaseLevel / vla-put-CreaseLevel  ; Specifies crease level at which the crease starts losing its effect
    P CreaseType : AcMeshCreaseType  [get/put]  vla-get-CreaseType / vla-put-CreaseType  ; Specifies if a crease is applied
    P Material : BSTR  [get/put]  vla-get-Material / vla-put-Material  ; Specifies the material style of the selected object

[INTERFACE] IAcadSubDMeshVertex
Object: AcadSubDMeshVertex
Inherits: IAcadSubEntity
Doc: AutoCAD SubDMesh Vertex Interface
  Properties
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the coordinate
    P CreaseLevel : double  [get/put]  vla-get-CreaseLevel / vla-put-CreaseLevel  ; Specifies crease level at which the crease starts losing its effect
    P CreaseType : AcMeshCreaseType  [get/put]  vla-get-CreaseType / vla-put-CreaseType  ; Specifies if a crease is applied
