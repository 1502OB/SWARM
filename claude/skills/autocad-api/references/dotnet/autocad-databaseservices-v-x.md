# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[CLASS] Vertex
Full Name: Autodesk.AutoCAD.DatabaseServices.Vertex
Inherits: Entity

--------------------------------------------------------------------------------

[CLASS] Vertex2d
Full Name: Autodesk.AutoCAD.DatabaseServices.Vertex2d
Inherits: Vertex

  CONSTRUCTORS:
    new Vertex2d(Point3d position, double bulge, double startWidth, double endWidth, double tangent)
    new Vertex2d()

  PROPERTIES:
    double Bulge { get; set; }
    double EndWidth { get; set; }
    Point3d Position { get; set; }
    double StartWidth { get; set; }
    double Tangent { get; set; }
    bool TangentUsed { get; set; }
    Vertex2dType VertexType { get; }

--------------------------------------------------------------------------------

[ENUM] Vertex2dType
Full Name: Autodesk.AutoCAD.DatabaseServices.Vertex2dType
Inherits: Enum

  Values:
    - SimpleVertex = 0
    - SplineControlVertex = 1
    - SplineFitVertex = 2
    - CurveFitVertex = 3

--------------------------------------------------------------------------------

[ENUM] Vertex3dType
Full Name: Autodesk.AutoCAD.DatabaseServices.Vertex3dType
Inherits: Enum

  Values:
    - SimpleVertex = 0
    - ControlVertex = 1
    - FitVertex = 2

--------------------------------------------------------------------------------

[CLASS] VertexRef
Full Name: Autodesk.AutoCAD.DatabaseServices.VertexRef
Inherits: SubentRef

  CONSTRUCTORS:
    new VertexRef(Point3d point)
    new VertexRef(Entity entity)
    new VertexRef(FullSubentityPath path)
    new VertexRef(CompoundObjectId compoundObjectId, SubentityId subentId, Point3d point)
    new VertexRef(CompoundObjectId compoundObjectId, SubentityId subentId)
    new VertexRef(CompoundObjectId compoundObjectId)
    new VertexRef()

  PROPERTIES:
    Point3d Point { get; }

--------------------------------------------------------------------------------

[CLASS] VerticalConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.VerticalConstraint
Inherits: ParallelConstraint

  CONSTRUCTORS:
    new VerticalConstraint()

--------------------------------------------------------------------------------

[CLASS] ViewBorder
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewBorder
Inherits: Entity

  CONSTRUCTORS:
    new ViewBorder()

  PROPERTIES:
    double Height { get; }
    Point3d InsertionPoint { get; }
    string InventorFileReference { get; }
    bool IsFirstAngleProjection { get; }
    double RotationAngle { get; }
    double Scale { get; }
    uint ShadedDPI { get; }
    ObjectId ViewportId { get; }
    double Width { get; }

  METHODS:
    SourceType GetSourceType()
    ViewStyleType GetViewStyleType()

--------------------------------------------------------------------------------

[CLASS] Viewport
Full Name: Autodesk.AutoCAD.DatabaseServices.Viewport
Inherits: Entity

  CONSTRUCTORS:
    new Viewport()

  PROPERTIES:
    Color AmbientLightColor { get; set; }
    AnnotationScale AnnotationScale { get; set; }
    double BackClipDistance { get; set; }
    bool BackClipOn { get; set; }
    ObjectId Background { get; set; }
    double Brightness { get; set; }
    Point3d CenterPoint { get; set; }
    int CircleSides { get; set; }
    double Contrast { get; set; }
    double CustomScale { get; set; }
    bool DefaultLightingOn { get; set; }
    DefaultLightingType DefaultLightingType { get; set; }
    string EffectivePlotStyleSheet { get; }
    double Elevation { get; set; }
    bool FastZoomOn { get; set; }
    bool FrontClipAtEyeOn { get; set; }
    double FrontClipDistance { get; set; }
    bool FrontClipOn { get; set; }
    bool GridAdaptive { get; set; }
    bool GridBoundToLimits { get; set; }
    bool GridFollow { get; set; }
    Vector2d GridIncrement { get; set; }
    short GridMajor { get; set; }
    bool GridOn { get; set; }
    bool GridSubdivisionRestricted { get; set; }
    double Height { get; set; }
    bool HiddenLinesRemoved { get; set; }
    double LensLength { get; set; }
    bool LinkedToSheetView { get; }
    bool Locked { get; set; }
    ObjectId NonRectClipEntityId { get; set; }
    bool NonRectClipOn { get; set; }
    int Number { get; }
    bool On { get; set; }
    bool PerspectiveOn { get; set; }
    bool PlotAsRaster { get; }
    string PlotStyleSheet { get; set; }
    bool PlotWireframe { get; }
    ShadePlotType ShadePlot { get; set; }
    ObjectId ShadePlotId { get; }
    double SnapAngle { get; set; }
    Point2d SnapBasePoint { get; set; }
    Vector2d SnapIncrement { get; set; }
    bool SnapIsometric { get; set; }
    int SnapIsoPair { get; set; }
    bool SnapOn { get; set; }
    StandardScaleType StandardScale { get; set; }
    ObjectId SunId { get; }
        [SKIPPED PROPERTY] Thumbnail: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    ToneOperatorParameters ToneOperatorParameters { get; set; }
    bool Transparent { get; set; }
    double TwistAngle { get; set; }
    CoordinateSystem3d Ucs { get; set; }
    bool UcsFollowModeOn { get; set; }
    bool UcsIconAtOrigin { get; set; }
    bool UcsIconVisible { get; set; }
    ObjectId UcsName { get; }
    OrthographicView UcsOrthographic { get; }
    bool UcsPerViewport { get; set; }
    Point2d ViewCenter { get; set; }
    Vector3d ViewDirection { get; set; }
    double ViewHeight { get; set; }
    OrthographicView ViewOrthographic { get; }
    Point3d ViewTarget { get; set; }
    ObjectId VisualStyleId { get; set; }
    double Width { get; set; }

  METHODS:
    void FreezeLayersInViewport(IEnumerator layerIds)
    ObjectIdCollection GetFrozenLayers()
    ObjectId GetPreviousBackground(DrawableType type)
    void GetUcs(ref Point3d origin, ref Vector3d x, ref Vector3d y)
    bool IsLayerFrozenInViewport(ObjectId layerId)
    void SetPreviousBackground(ObjectId value, DrawableType type)
    void SetShadePlot(ShadePlotType type, ObjectId shadePlotId)
    ObjectId SetSun(DBObject sun)
    void SetUcs(ObjectId userCoordinateSystemId)
    void SetUcs(OrthographicView view)
    void SetUcs(Point3d origin, Vector3d x, Vector3d y)
    void SetUcsToWorld()
    void SetViewDirection(OrthographicView view)
    void ThawAllLayersInViewport()
    void ThawLayersInViewport(IEnumerator layerIds)
    void UpdateDisplay()

--------------------------------------------------------------------------------

[CLASS] ViewportTable
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewportTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] ViewportTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewportTableRecord
Inherits: AbstractViewTableRecord

  CONSTRUCTORS:
    new ViewportTableRecord()

  PROPERTIES:
    short CircleSides { get; set; }
    bool FastZoomsEnabled { get; set; }
    bool GridAdaptive { get; set; }
    bool GridBoundToLimits { get; set; }
    bool GridEnabled { get; set; }
    bool GridFollow { get; set; }
    Point2d GridIncrements { get; set; }
    short GridMajor { get; set; }
    bool GridSubdivisionRestricted { get; set; }
    bool IconAtOrigin { get; set; }
    bool IconEnabled { get; set; }
    bool IsometricSnapEnabled { get; set; }
    Point2d LowerLeftCorner { get; set; }
    int Number { get; }
    double SnapAngle { get; set; }
    Point2d SnapBase { get; set; }
    bool SnapEnabled { get; set; }
    Point2d SnapIncrements { get; set; }
    short SnapPair { get; set; }
    ToneOperatorParameters ToneOperatorParameters { get; set; }
    bool UcsFollowMode { get; set; }
    bool UcsSavedWithViewport { get; set; }
    Point2d UpperRightCorner { get; set; }

  METHODS:
    ObjectId GetPreviousBackground(DrawableType type)
    void SetPreviousBackground(ObjectId value, DrawableType type)

--------------------------------------------------------------------------------

[CLASS] ViewRepBlockReference
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewRepBlockReference
Inherits: BlockReference

  CONSTRUCTORS:
    new ViewRepBlockReference()

  PROPERTIES:
    ObjectId OwnerViewportId { get; }

--------------------------------------------------------------------------------

[ENUM] ViewStyleType
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewStyleType
Inherits: Enum

  Values:
    - FromBase = 0
    - WireframeVisibleEdges = 1
    - WireframeHiddenEdges = 2
    - ShadedVisibleEdges = 3
    - ShadedHiddenEdges = 4

--------------------------------------------------------------------------------

[CLASS] ViewTable
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] ViewTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.ViewTableRecord
Inherits: AbstractViewTableRecord

  CONSTRUCTORS:
    new ViewTableRecord()

  PROPERTIES:
    AnnotationScale AnnotationScale { get; set; }
    string CategoryName { get; set; }
    bool IsPaperspaceView { get; set; }
    bool IsUcsAssociatedToView { get; }
    string LayerState { get; set; }
    ObjectId Layout { get; set; }
    ObjectId LiveSection { get; set; }
        [SKIPPED PROPERTY] Thumbnail: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    bool ViewAssociatedToViewport { get; set; }

  METHODS:
    void DisassociateUcsFromView()

--------------------------------------------------------------------------------

[ENUM] Visibility
Full Name: Autodesk.AutoCAD.DatabaseServices.Visibility
Inherits: Enum

  Values:
    - Visible = 0
    - Invisible = 1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] VisibilityOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.VisibilityOverrule
Inherits: Overrule

  METHODS:
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetVisibility(Entity entity, Visibility newVal, bool doSubents)
    void SetXDataFilter(string registeredApplicationName)
    Visibility Visibility(Entity entity)

--------------------------------------------------------------------------------

[CLASS] WblockNoticeEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.WblockNoticeEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database From { get; }
    Database To { get; }

--------------------------------------------------------------------------------

[CLASS] WblockNoticeEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.WblockNoticeEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new WblockNoticeEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, WblockNoticeEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, WblockNoticeEventArgs e)

--------------------------------------------------------------------------------

[CLASS] Wipeout
Full Name: Autodesk.AutoCAD.DatabaseServices.Wipeout
Inherits: RasterImage

  CONSTRUCTORS:
    new Wipeout()

  PROPERTIES:
    bool HasFrame { get; }

  METHODS:
    void SetFrom(Point2dCollection points, Vector3d normal)

--------------------------------------------------------------------------------

[CLASS] Xline
Full Name: Autodesk.AutoCAD.DatabaseServices.Xline
Inherits: Curve

  CONSTRUCTORS:
    new Xline()

  PROPERTIES:
    Point3d BasePoint { get; set; }
    Point3d SecondPoint { get; set; }
    Vector3d UnitDir { get; set; }

--------------------------------------------------------------------------------

[CLASS] Xrecord
Full Name: Autodesk.AutoCAD.DatabaseServices.Xrecord
Inherits: DBObject
Implements: IEnumerable<TypedValue>, IEnumerable

  CONSTRUCTORS:
    new Xrecord()

  PROPERTIES:
    ResultBuffer Data { get; set; }
    DuplicateRecordCloning MergeStyle { get; set; }
    bool XlateReferences { get; set; }

  METHODS:
    void Append(ResultBuffer data)
    XrecordEnumerator GetEnumerator()
    IEnumerator IEnumerableGetEnumerator()
    IEnumerator<TypedValue> IEnumerableTypedValueGetEnumerator()

--------------------------------------------------------------------------------

[CLASS] XrecordEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.XrecordEnumerator
Inherits: RXObject
Implements: IEnumerator<TypedValue>, IEnumerator

  PROPERTIES:
    TypedValue Current { get; set; }
    short CurrentTypeCode { get; }
    object IEnumeratorCurrent { get; }

  METHODS:
    void InsertAtCurrent(ResultBuffer data)
    bool MoveNext()
    void RemoveCurrent()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] XrefBeginOperationEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefBeginOperationEventArgs
Inherits: EventArgs

  PROPERTIES:
    string FileName { get; }
    Database From { get; }

--------------------------------------------------------------------------------

[CLASS] XrefBeginOperationEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefBeginOperationEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefBeginOperationEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefBeginOperationEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefBeginOperationEventArgs e)

--------------------------------------------------------------------------------

[CLASS] XrefComandeeredEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefComandeeredEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database From { get; }
    ObjectId Id { get; }

--------------------------------------------------------------------------------

[CLASS] XrefComandeeredEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefComandeeredEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefComandeeredEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefComandeeredEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefComandeeredEventArgs e)

--------------------------------------------------------------------------------

[CLASS] XrefGraph
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefGraph
Inherits: Graph

  PROPERTIES:
    XrefGraphNode HostDrawing { get; }

  METHODS:
    XrefGraphNode GetXrefNode(int idx)
    XrefGraphNode GetXrefNode(Database db)
    XrefGraphNode GetXrefNode(ObjectId btrId)
    XrefGraphNode GetXrefNode(string name)
    bool MarkUnresolvedTrees()

--------------------------------------------------------------------------------

[CLASS] XrefGraphNode
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefGraphNode
Inherits: GraphNode

  PROPERTIES:
    ObjectId BlockTableRecordId { get; }
    Database Database { get; }
    bool IsNested { get; }
    string Name { get; }
    XrefNotificationStatus XrefNotificationStatus { get; }
    XrefStatus XrefStatus { get; }

--------------------------------------------------------------------------------

[ENUM] XrefNotificationStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefNotificationStatus
Inherits: Enum

  Values:
    - None = 0
    - ResolvedMatch = 1
    - ResolvedElsewhere = 2
    - ResolvedWithUpdate = 3
    - ResolvedUpdateAvailable = 4

--------------------------------------------------------------------------------

[STRUCT] XrefObjectId
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefObjectId

  CONSTRUCTORS:
    new XrefObjectId(ObjectId XrefBlockTableRecordId, Handle hand)
    new XrefObjectId(ObjectId LocalId)

  PROPERTIES:
    Handle Handle { get; }
    bool IsNull { get; }
    bool IsValid { get; }
    bool IsXref { get; }
    ObjectId ObjectId { get; }

  METHODS:
    static XrefObjectId Deserialize(ResultBuffer rbStart, out ResultBuffer rbEnd)
    bool Equals(object obj)
    ObjectId ResolveObjectId()
    ResultBuffer Serialize()

--------------------------------------------------------------------------------

[ENUM] XrefOperation
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefOperation
Inherits: Enum

  Values:
    - XrefAttachOperation = 0
    - XrefBindOperation = 1
    - XrefBindOperation = 1
    - XrefDetachOperation = 2
    - XrefOverlayOperation = 3
    - XrefPathOperation = 4
    - XrefReloadOperation = 5
    - XrefResolveOperation = 6
    - XrefUnloadOperation = 7

--------------------------------------------------------------------------------

[CLASS] XrefPreXrefLockFileEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefPreXrefLockFileEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId btrId { get; }

--------------------------------------------------------------------------------

[CLASS] XrefPreXrefLockFileEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefPreXrefLockFileEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefPreXrefLockFileEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefPreXrefLockFileEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefPreXrefLockFileEventArgs e)

--------------------------------------------------------------------------------

[CLASS] XrefRedirectedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefRedirectedEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId NewId { get; }
    ObjectId OldId { get; }

--------------------------------------------------------------------------------

[CLASS] XrefRedirectedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefRedirectedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefRedirectedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefRedirectedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefRedirectedEventArgs e)

--------------------------------------------------------------------------------

[ENUM] XrefStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefStatus
Inherits: Enum

  Values:
    - NotAnXref = 0
    - Resolved = 1
    - Unloaded = 2
    - Unreferenced = 3
    - FileNotFound = 4
    - Unresolved = 5

--------------------------------------------------------------------------------

[CLASS] XrefSubCommandAbortedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefSubCommandAbortedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefSubCommandAbortedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefSubCommandEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefSubCommandEventArgs e)

--------------------------------------------------------------------------------

[CLASS] XrefSubCommandEndEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefSubCommandEndEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefSubCommandEndEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefSubCommandEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefSubCommandEventArgs e)

--------------------------------------------------------------------------------

[CLASS] XrefSubCommandEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefSubCommandEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectIdCollection btrIds { get; }
    string[] btrNames { get; }
    string[] paths { get; }
    XrefOperation xrefOp { get; }

--------------------------------------------------------------------------------

[CLASS] XrefSubCommandStartEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefSubCommandStartEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new XrefSubCommandStartEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, XrefVetoableSubCommandEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, XrefVetoableSubCommandEventArgs e)

--------------------------------------------------------------------------------

[CLASS] XrefVetoableSubCommandEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.XrefVetoableSubCommandEventArgs
Inherits: XrefSubCommandEventArgs

  PROPERTIES:
    bool abortOp { get; set; }

--------------------------------------------------------------------------------

