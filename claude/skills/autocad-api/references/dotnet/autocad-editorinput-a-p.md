# Autodesk.AutoCAD.EditorInput

NAMESPACE: Autodesk.AutoCAD.EditorInput
--------------------------------------------------------------------------------

[ENUM] AddedKeywords
Full Name: Autodesk.AutoCAD.EditorInput.AddedKeywords
Inherits: Enum

  Values:
    - LastAllPrevious = 1
    - PickImplied = 2
    - All = 4
    - WindowCrossingBoxWPolygonCPolygon = 8
    - Multiple = 16
    - CrossingCPolygon = 32
    - Fence = 64
    - Last = 128
    - Previous = 256
    - WindowWPolygon = 512
    - Group = 1024

--------------------------------------------------------------------------------

[CLASS] Camera
Full Name: Autodesk.AutoCAD.EditorInput.Camera
Inherits: Entity

  PROPERTIES:
    double BackClipDistance { get; }
    bool BackClipEnabled { get; }
    double FieldOfView { get; }
    double FrontClipDistance { get; }
    bool FrontClipEnabled { get; }
    bool IsCameraPlottable { get; }
    double LensLength { get; }
    Point3d Position { get; }
    Point3d Target { get; }
    ObjectId ViewId { get; }
    double ViewTwist { get; }

--------------------------------------------------------------------------------

[CLASS] ConstraintUtilities
Full Name: Autodesk.AutoCAD.EditorInput.ConstraintUtilities

  CONSTRUCTORS:
    new ConstraintUtilities()

  METHODS:
    static bool ShowConstraintBar(FullSubentityPath[] subentityPaths, bool bToShow)

--------------------------------------------------------------------------------

[CLASS] CrossingOrWindowSelectedObject
Full Name: Autodesk.AutoCAD.EditorInput.CrossingOrWindowSelectedObject
Inherits: SelectedObject

  CONSTRUCTORS:
    new CrossingOrWindowSelectedObject(ObjectId id, SelectionMethod method, IntPtr gsMarker)
    new CrossingOrWindowSelectedObject(ObjectId id, SelectionMethod method, long gsMarker)

  METHODS:
    PickPointDescriptor[] GetPickPoints()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] CrossingOrWindowSelectedSubObject
Full Name: Autodesk.AutoCAD.EditorInput.CrossingOrWindowSelectedSubObject
Inherits: SelectedSubObject

  CONSTRUCTORS:
    new CrossingOrWindowSelectedSubObject(FullSubentityPath path, SelectionMethod method, IntPtr gsMarker)
    new CrossingOrWindowSelectedSubObject(FullSubentityPath path, SelectionMethod method, long gsMarker)

  METHODS:
    PickPointDescriptor[] GetPickPoints()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] CursorBadgeUtilities
Full Name: Autodesk.AutoCAD.EditorInput.CursorBadgeUtilities

  CONSTRUCTORS:
    new CursorBadgeUtilities()

  METHODS:
    bool AddSupplementalCursorImage(ImageBGRA32 cursorImage, int priority)
    void GetSupplementalCursorOffset(ref int x, ref int y)
    bool HasSupplementalCursorImage()
    bool RemoveSupplementalCursorImage(ImageBGRA32 cursorImage)
    void SetSupplementalCursorOffset(int x, int y)

--------------------------------------------------------------------------------

[ENUM] CursorType
Full Name: Autodesk.AutoCAD.EditorInput.CursorType
Inherits: Enum

  Values:
    - Crosshair = 0
    - RectangularCursor = 1
    - RubberBand = 2
    - NotRotated = 3
    - TargetBox = 4
    - RotatedCrosshair = 5
    - CrosshairNoRotate = 6
    - Invisible = 7
    - EntitySelect = 8
    - Parallelogram = 9
    - EntitySelectNoPerspective = 10
    - PickfirstOrGrips = 11
    - NoSpecialCursor = -1

--------------------------------------------------------------------------------

[CLASS] DeviceInputEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.DeviceInputEventArgs
Inherits: EventArgs

  PROPERTIES:
    IntPtr GraphicsSystemMarker { get; }
    bool Handled { get; set; }
    IntPtr LParam { get; }
    int Message { get; }
    IntPtr WParam { get; }

--------------------------------------------------------------------------------

[CLASS] DeviceInputEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.DeviceInputEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DeviceInputEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DeviceInputEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DeviceInputEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DisposableSelectionSet
Full Name: Autodesk.AutoCAD.EditorInput.DisposableSelectionSet
Implements: IDisposable

  CONSTRUCTORS:
    new DisposableSelectionSet(AdsName selectionSet)

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] DragCallback
Full Name: Autodesk.AutoCAD.EditorInput.DragCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DragCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(Point3d current, out Matrix3d xform, AsyncCallback callback, object obj)
    SamplerStatus EndInvoke(IAsyncResult result)
    SamplerStatus Invoke(Point3d current, out Matrix3d xform)

--------------------------------------------------------------------------------

[ENUM] DragCursor
Full Name: Autodesk.AutoCAD.EditorInput.DragCursor
Inherits: Enum

  Values:
    - Normal = 0
    - None = 1
    - Selection = 2

--------------------------------------------------------------------------------

[CLASS] DraggingEndedEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.DraggingEndedEventArgs
Inherits: EventArgs

  PROPERTIES:
    Vector3d Offset { get; }
    Point3d PickPoint { get; }
    PromptStatus Status { get; }

--------------------------------------------------------------------------------

[CLASS] DraggingEndedEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.DraggingEndedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DraggingEndedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DraggingEndedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DraggingEndedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DraggingEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.DraggingEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new DraggingEventArgs(string prompt)

  PROPERTIES:
    string Prompt { get; }

--------------------------------------------------------------------------------

[CLASS] DraggingEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.DraggingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DraggingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DraggingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DraggingEventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DrawJig
Full Name: Autodesk.AutoCAD.EditorInput.DrawJig
Inherits: Jig

--------------------------------------------------------------------------------

[CLASS] Editor
Full Name: Autodesk.AutoCAD.EditorInput.Editor
Inherits: MarshalByRefObject

  PROPERTIES:
    ObjectId ActiveViewportId { get; }
    Matrix3d CurrentUserCoordinateSystem { get; set; }
    ObjectId CurrentViewportObjectId { get; }
    Document Document { get; }
    bool IsDragging { get; }
    bool IsQuiescent { get; }
    bool IsQuiescentForTransparentCommand { get; }
    bool MouseHasMoved { get; }
    bool UseCommandLineInterface { get; }

  METHODS:
    void ApplyCurDwgLayerTableChanges()
    void Command(params object[] parameter)
    CommandResult CommandAsync(params object[] parameter)
    PromptResult DoPrompt(PromptOptions opt)
    PromptPointResult Drag(PromptDragOptions options)
    PromptPointResult Drag(SelectionSet selection, string message, DragCallback callback)
    PromptResult Drag(Jig jig)
    void DrawVector(Point3d from, Point3d to, int color, bool drawHighlighted)
    void DrawVectors(ResultBuffer rb, Matrix3d transform)
    PromptDoubleResult GetAngle(PromptAngleOptions options)
    PromptDoubleResult GetAngle(string message)
    int GetCommandVersion()
    PromptPointResult GetCorner(PromptCornerOptions options)
    PromptPointResult GetCorner(string message, Point3d basePoint)
    ViewTableRecord GetCurrentView()
    PromptDoubleResult GetDistance(PromptDistanceOptions options)
    PromptDoubleResult GetDistance(string message)
    PromptDoubleResult GetDouble(PromptDoubleOptions options)
    PromptDoubleResult GetDouble(string message)
    PromptEntityResult GetEntity(PromptEntityOptions options)
    PromptEntityResult GetEntity(string message)
    PromptFileNameResult GetFileNameForOpen(PromptOpenFileOptions options)
    PromptFileNameResult GetFileNameForOpen(string message)
    PromptFileNameResult GetFileNameForSave(PromptSaveFileOptions options)
    PromptFileNameResult GetFileNameForSave(string message)
    PromptIntegerResult GetInteger(PromptIntegerOptions options)
    PromptIntegerResult GetInteger(string message)
    PromptResult GetKeywords(PromptKeywordOptions options)
    PromptResult GetKeywords(string message, params string[] globalKeywords)
    PromptNestedEntityResult GetNestedEntity(PromptNestedEntityOptions options)
    PromptNestedEntityResult GetNestedEntity(string message)
    PromptPointResult GetPoint(PromptPointOptions options)
    PromptPointResult GetPoint(string message)
    PromptSelectionResult GetSelection(PromptSelectionOptions options, SelectionFilter filter)
    PromptSelectionResult GetSelection(PromptSelectionOptions options)
    PromptSelectionResult GetSelection(SelectionFilter filter)
    PromptSelectionResult GetSelection()
    PromptResult GetString(PromptStringOptions options)
    PromptResult GetString(string message)
    [SKIPPED METHOD] GetViewportNumber: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    int InitCommandVersion(int nVersion)
    [SKIPPED METHOD] PointToScreen: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] PointToWorld: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] PointToWorld: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void PostCommandPrompt()
    void Regen()
    PromptSelectionResult SelectAll(SelectionFilter filter)
    PromptSelectionResult SelectAll()
    PromptSelectionResult SelectCrossingPolygon(Point3dCollection polygon, SelectionFilter filter)
    PromptSelectionResult SelectCrossingPolygon(Point3dCollection polygon)
    PromptSelectionResult SelectCrossingWindow(Point3d pt1, Point3d pt2, SelectionFilter filter, bool forceSubEntitySelection)
    PromptSelectionResult SelectCrossingWindow(Point3d pt1, Point3d pt2, SelectionFilter filter)
    PromptSelectionResult SelectCrossingWindow(Point3d pt1, Point3d pt2)
    PromptSelectionResult SelectFence(Point3dCollection fence, SelectionFilter filter)
    PromptSelectionResult SelectFence(Point3dCollection fence)
    PromptSelectionResult SelectImplied()
    PromptSelectionResult SelectLast()
    PromptSelectionResult SelectPrevious()
    PromptSelectionResult SelectWindow(Point3d pt1, Point3d pt2, SelectionFilter filter)
    PromptSelectionResult SelectWindow(Point3d pt1, Point3d pt2)
    PromptSelectionResult SelectWindowPolygon(Point3dCollection polygon, SelectionFilter filter)
    PromptSelectionResult SelectWindowPolygon(Point3dCollection polygon)
    void SetCurrentView(ViewTableRecord value)
    void SetImpliedSelection(SelectionSet selectionSet)
    void SetImpliedSelection(ObjectId[] selectedObjects)
    Point3d Snap(string snapMode, Point3d input)
    EditorUserInteraction StartUserInteraction(IntPtr hwnd)
    [SKIPPED METHOD] StartUserInteraction: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void SwitchToModelSpace()
    void SwitchToPaperSpace()
    DBObjectCollection TraceBoundary(Point3d seedPoint, bool detectIslands)
    int TurnForcedPickOff()
    int TurnForcedPickOn()
    void TurnSubentityWindowSelectionOff()
    void TurnSubentityWindowSelectionOn()
    void UpdateScreen()
    void UpdateTiledViewportsFromDatabase()
    void UpdateTiledViewportsInDatabase()
    ObjectId ViewportIdFromNumber(int viewportNumber)
    void WriteMessage(string message, params object[] parameter)
    void WriteMessage(string message)

  EVENTS:
    event DraggingEndedEventHandler DraggingEnded
    event DraggingEventHandler Dragging
    event PromptForEntityEndingEventHandler PromptForEntityEnding
    event PromptSelectionResultEventHandler PromptedForSelection
    event PromptForSelectionEndingEventHandler PromptForSelectionEnding
    event SelectionRemovedEventHandler SelectionRemoved
    event RolloverEventHandler Rollover
    event SelectionAddedEventHandler SelectionAdded
    event PromptSelectionOptionsEventHandler PromptingForSelection
    event EventHandler LeavingQuiescentState
    event EventHandler EnteringQuiescentState
    event PromptNestedEntityResultEventHandler PromptedForNestedEntity
    event PromptNestedEntityOptionsEventHandler PromptingForNestedEntity
    event PromptEntityResultEventHandler PromptedForEntity
    event PromptEntityOptionsEventHandler PromptingForEntity
    event PromptDoubleResultEventHandler PromptedForDouble
    event PromptDoubleOptionsEventHandler PromptingForDouble
    event PromptIntegerResultEventHandler PromptedForInteger
    event PromptIntegerOptionsEventHandler PromptingForInteger
    event PromptStringResultEventHandler PromptedForKeyword
    event PromptKeywordOptionsEventHandler PromptingForKeyword
    event PromptStringResultEventHandler PromptedForString
    event PromptStringOptionsEventHandler PromptingForString
    event PromptPointResultEventHandler PromptedForCorner
    event PromptPointOptionsEventHandler PromptingForCorner
    event PromptDoubleResultEventHandler PromptedForDistance
    event PromptDistanceOptionsEventHandler PromptingForDistance
    event PromptDoubleResultEventHandler PromptedForAngle
    event PromptAngleOptionsEventHandler PromptingForAngle
    event PromptPointResultEventHandler PromptedForPoint
    event PromptPointOptionsEventHandler PromptingForPoint
    event PointMonitorEventHandler PointMonitor
    event PointFilterEventHandler PointFilter

  CONSTANTS/STATIC FIELDS:
    const string PauseToken = \

--------------------------------------------------------------------------------

[ABSTRACT CLASS] EditorExtension
Full Name: Autodesk.AutoCAD.EditorInput.EditorExtension

  METHODS:
    static int GetViewportNumber(Editor editor, Point point)
    static Point PointToScreen(Editor editor, Point3d pt, int viewportNumber)
    static Point3d PointToWorld(Editor editor, Point pt, int viewportNumber)
    static Point3d PointToWorld(Editor editor, Point pt)
    [SKIPPED METHOD] StartUserInteraction: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] EditorUserInteraction
Full Name: Autodesk.AutoCAD.EditorInput.EditorUserInteraction
Implements: IDisposable

  METHODS:
    void Dispose()
    void End()

--------------------------------------------------------------------------------

[CLASS] EditorVisualStyle
Full Name: Autodesk.AutoCAD.EditorInput.EditorVisualStyle

  CONSTRUCTORS:
    new EditorVisualStyle()

  PROPERTIES:
    static bool IsVS2dType { get; }
    static bool IsVS3dType { get; }

  METHODS:
    static void setCvpVS2d()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] EntityJig
Full Name: Autodesk.AutoCAD.EditorInput.EntityJig
Inherits: Jig

--------------------------------------------------------------------------------

[CLASS] FenceSelectedObject
Full Name: Autodesk.AutoCAD.EditorInput.FenceSelectedObject
Inherits: SelectedObject

  CONSTRUCTORS:
    new FenceSelectedObject(ObjectId id, SelectionMethod method, IntPtr gsMarker, PickPointDescriptor[] descriptors)
    new FenceSelectedObject(ObjectId id, SelectionMethod method, long gsMarker, PickPointDescriptor[] descriptors)

  METHODS:
    PickPointDescriptor[] GetIntersectionPoints()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] FenceSelectedSubObject
Full Name: Autodesk.AutoCAD.EditorInput.FenceSelectedSubObject
Inherits: SelectedSubObject

  CONSTRUCTORS:
    new FenceSelectedSubObject(FullSubentityPath path, SelectionMethod method, IntPtr gsMarker, PickPointDescriptor[] descriptors)
    new FenceSelectedSubObject(FullSubentityPath path, SelectionMethod method, long gsMarker, PickPointDescriptor[] descriptors)

  METHODS:
    PickPointDescriptor[] GetIntersectionPoints()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] InputPointContext
Full Name: Autodesk.AutoCAD.EditorInput.InputPointContext
Implements: IDisposable

  PROPERTIES:
    Point3d CartesianSnappedPoint { get; }
    Point3d ComputedPoint { get; }
    Document Document { get; }
    ViewportDraw DrawContext { get; }
    Point3d GrippedPoint { get; }
    PointHistoryBits History { get; }
    Point3d LastPoint { get; }
    ObjectSnapMasks ObjectSnapMask { get; }
    ObjectSnapMasks ObjectSnapOverrides { get; }
    Point3d ObjectSnappedPoint { get; }
    bool PointComputed { get; }
    Point3d RawPoint { get; }
    string ToolTipText { get; }

  METHODS:
    void Dispose()
    Curve3d[] GetAlignmentPaths()
    CustomObjectSnapMode[] GetCustomObjectSnapModes()
    CustomObjectSnapMode[] GetCustomObjectSnapOverrides()
    FullSubentityPath[] GetKeyPointEntities()
    FullSubentityPath[] GetPickedEntities()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Jig
Full Name: Autodesk.AutoCAD.EditorInput.Jig

--------------------------------------------------------------------------------

[CLASS] JigPromptAngleOptions
Full Name: Autodesk.AutoCAD.EditorInput.JigPromptAngleOptions
Inherits: JigPromptGeometryOptions

  CONSTRUCTORS:
    new JigPromptAngleOptions(string messageAndKeywords, string globalKeywords)
    new JigPromptAngleOptions(string message)
    new JigPromptAngleOptions()

  PROPERTIES:
    double DefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] JigPromptDistanceOptions
Full Name: Autodesk.AutoCAD.EditorInput.JigPromptDistanceOptions
Inherits: JigPromptGeometryOptions

  CONSTRUCTORS:
    new JigPromptDistanceOptions(string messageAndKeywords, string globalKeywords)
    new JigPromptDistanceOptions(string message)
    new JigPromptDistanceOptions()

  PROPERTIES:
    double DefaultValue { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] JigPromptGeometryOptions
Full Name: Autodesk.AutoCAD.EditorInput.JigPromptGeometryOptions
Inherits: JigPromptOptions

  CONSTRUCTORS:
    new JigPromptGeometryOptions(string messageAndKeywords, string globalKeywords)
    new JigPromptGeometryOptions(string message)
    new JigPromptGeometryOptions()

  PROPERTIES:
    Point3d BasePoint { get; set; }
    bool UseBasePoint { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] JigPromptOptions
Full Name: Autodesk.AutoCAD.EditorInput.JigPromptOptions
Inherits: PromptOptions

  PROPERTIES:
    CursorType Cursor { get; set; }
    UserInputControls UserInputControls { get; set; }

--------------------------------------------------------------------------------

[CLASS] JigPromptPointOptions
Full Name: Autodesk.AutoCAD.EditorInput.JigPromptPointOptions
Inherits: JigPromptGeometryOptions

  CONSTRUCTORS:
    new JigPromptPointOptions(string messageAndKeywords, string globalKeywords)
    new JigPromptPointOptions(string message)
    new JigPromptPointOptions()

  PROPERTIES:
    Point3d DefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] JigPrompts
Full Name: Autodesk.AutoCAD.EditorInput.JigPrompts

  METHODS:
    PromptDoubleResult AcquireAngle(string messageAndKeywords, string globalKeywords)
    PromptDoubleResult AcquireAngle(string message)
    PromptDoubleResult AcquireAngle(JigPromptAngleOptions options)
    PromptDoubleResult AcquireAngle()
    PromptDoubleResult AcquireDistance(string messageAndKeywords, string globalKeywords)
    PromptDoubleResult AcquireDistance(string message)
    PromptDoubleResult AcquireDistance(JigPromptDistanceOptions options)
    PromptDoubleResult AcquireDistance()
    PromptPointResult AcquirePoint(string messageAndKeywords, string globalKeywords)
    PromptPointResult AcquirePoint(string message)
    PromptPointResult AcquirePoint(JigPromptPointOptions options)
    PromptPointResult AcquirePoint()
    PromptResult AcquireString(string messageAndKeywords, string globalKeywords)
    PromptResult AcquireString(string message)
    PromptResult AcquireString(JigPromptStringOptions options)
    PromptResult AcquireString()

--------------------------------------------------------------------------------

[CLASS] JigPromptStringOptions
Full Name: Autodesk.AutoCAD.EditorInput.JigPromptStringOptions
Inherits: JigPromptOptions

  CONSTRUCTORS:
    new JigPromptStringOptions(string messageAndKeywords, string globalKeywords)
    new JigPromptStringOptions(string message)
    new JigPromptStringOptions()

  PROPERTIES:
    string DefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] Keyword
Full Name: Autodesk.AutoCAD.EditorInput.Keyword

  PROPERTIES:
    string DisplayName { get; set; }
    bool Enabled { get; set; }
    string GlobalName { get; set; }
    bool IsReadOnly { get; set; }
    string LocalName { get; set; }
    bool Visible { get; set; }

--------------------------------------------------------------------------------

[CLASS] KeywordCollection
Full Name: Autodesk.AutoCAD.EditorInput.KeywordCollection
Implements: ICollection, IEnumerable

  CONSTRUCTORS:
    new KeywordCollection()

  PROPERTIES:
    int Count { get; }
    string Default { get; set; }
    bool IsReadOnly { get; }
    Keyword Item { get; }

  METHODS:
    void Add(string globalName)
    void Add(string globalName, string localName)
    void Add(string globalName, string localName, string displayName)
    void Add(string globalName, string localName, string displayName, bool visible, bool enabled)
    void Clear()
    void CopyTo(Keyword[] array, int index)
    string GetDisplayString(bool showNoDefault)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] LivePreview
Full Name: Autodesk.AutoCAD.EditorInput.LivePreview
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new LivePreview()
    new LivePreview(Document doc)
    new LivePreview(Document doc, bool startPending)

  METHODS:
    void AbortAll()
    void EndPreview(int delayTime, bool bRegen)
    void EndPreview(bool bRegen)
    void EndPreview(int delayTime)
    void EndPreview()
    void EndRecording()
    static bool IsPreviewRecording()
    static bool IsPreviewRecording(Document doc)
    static bool IsPreviewStarted()
    static bool IsPreviewStarted(Document doc)
    void QueueAction(LivePreviewActionBase action, int delayTime)
    void QueueAction(LivePreviewActionBase action)
    void StartRecording()

  EVENTS:
    event EventHandler<LivePreviewEventArgs> Previewing
    event EventHandler<LivePreviewEventArgs> RecordingEnded
    event EventHandler<LivePreviewEventArgs> RecordingWillEnd
    event EventHandler<LivePreviewEventArgs> RecordingStarted
    event EventHandler<LivePreviewEventArgs> RecordingWillStart
    event EventHandler<LivePreviewEventArgs> PreviewEnded
    event EventHandler<LivePreviewEventArgs> PreviewWillEnd
    event EventHandler<LivePreviewEventArgs> PreviewStarted
    event EventHandler<LivePreviewEventArgs> PreviewWillStart

--------------------------------------------------------------------------------

[CLASS] LivePreviewAction
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewAction
Inherits: LivePreviewActionBase

  CONSTRUCTORS:
    new LivePreviewAction(Delegate method, params object[] args)

  METHODS:
    void Execute()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LivePreviewActionBase
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewActionBase

  METHODS:
    void Execute()
    void OnAborted()

--------------------------------------------------------------------------------

[CLASS] LivePreviewCallback
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LivePreviewCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] LivePreviewCommand
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewCommand
Inherits: LivePreviewActionBase

  CONSTRUCTORS:
    new LivePreviewCommand(string cmd)

  METHODS:
    void Execute()

--------------------------------------------------------------------------------

[CLASS] LivePreviewContextCallback
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewContextCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LivePreviewContextCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(string contextName, object contextVal, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(string contextName, object contextVal)

--------------------------------------------------------------------------------

[CLASS] LivePreviewContextParameter
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewContextParameter

  PROPERTIES:
    LivePreview LivePreview { get; }
    LivePreviewContextType Type { get; }
    object Value { get; }

--------------------------------------------------------------------------------

[CLASS] LivePreviewContextProxy
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewContextProxy
Implements: IDisposable

  CONSTRUCTORS:
    new LivePreviewContextProxy(string contexName, Delegate callback, params object[] parameters)

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[ENUM] LivePreviewContextType
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewContextType
Inherits: Enum

  Values:
    - EndPreview = 0
    - UpdatePreview = 1

--------------------------------------------------------------------------------

[CLASS] LivePreviewDelegate
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewDelegate
Inherits: LivePreviewActionBase

  CONSTRUCTORS:
    new LivePreviewDelegate(LivePreviewCallback actCallback, LivePreviewCallback abortedCallback)
    new LivePreviewDelegate(LivePreviewCallback actCallback)

  METHODS:
    void Execute()
    void OnAborted()

--------------------------------------------------------------------------------

[CLASS] LivePreviewEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new LivePreviewEventArgs(params object[] parameters)
    new LivePreviewEventArgs(Document document, params object[] parameters)
    new LivePreviewEventArgs()

  PROPERTIES:
    bool AbortPreview { get; set; }
    object CommandParameter { get; }
    Document Document { get; }
    object[] Parameters { get; }

  METHODS:
    IDisposable LockDocument()

--------------------------------------------------------------------------------

[CLASS] LivePreviewPropertySetting
Full Name: Autodesk.AutoCAD.EditorInput.LivePreviewPropertySetting
Inherits: LivePreviewActionBase

  CONSTRUCTORS:
    new LivePreviewPropertySetting(PropertyDescriptor prop, object component, object value)

  METHODS:
    void Execute()

--------------------------------------------------------------------------------

[ENUM] ObjectSnapMasks
Full Name: Autodesk.AutoCAD.EditorInput.ObjectSnapMasks
Inherits: Enum

  Values:
    - End = 1
    - Middle = 2
    - Center = 4
    - Node = 8
    - Quadrant = 16
    - Intersection = 32
    - Insertion = 64
    - Perpendicular = 128
    - Tangent = 256
    - Near = 512
    - Quick = 1024
    - ApparentIntersection = 2048
    - Immediate = 65536
    - AllowTangent = 131072
    - DisablePerpendicular = 262144
    - RelativeCartesian = 524288
    - RelativePolar = 1048576
    - NoneOverride = 2097152

--------------------------------------------------------------------------------

[STRUCT] PickPointDescriptor
Full Name: Autodesk.AutoCAD.EditorInput.PickPointDescriptor

  CONSTRUCTORS:
    new PickPointDescriptor(PickPointKind kind, Point3d pointOnLine, Vector3d direction)

  PROPERTIES:
    Vector3d Direction { get; }
    PickPointKind Kind { get; }
    Point3d PointOnLine { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(PickPointDescriptor a)
    bool IsEqualTo(PickPointDescriptor a, Tolerance tolerance)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[ENUM] PickPointKind
Full Name: Autodesk.AutoCAD.EditorInput.PickPointKind
Inherits: Enum

  Values:
    - InfiniteLine = 0
    - Ray = 1
    - LineSegment = 2

--------------------------------------------------------------------------------

[CLASS] PickPointSelectedObject
Full Name: Autodesk.AutoCAD.EditorInput.PickPointSelectedObject
Inherits: SelectedObject

  CONSTRUCTORS:
    new PickPointSelectedObject(ObjectId id, SelectionMethod method, IntPtr gsMarker, PickPointDescriptor descriptor)
    new PickPointSelectedObject(ObjectId id, SelectionMethod method, long gsMarker, PickPointDescriptor descriptor)

  PROPERTIES:
    PickPointDescriptor PickPoint { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PickPointSelectedSubObject
Full Name: Autodesk.AutoCAD.EditorInput.PickPointSelectedSubObject
Inherits: SelectedSubObject

  CONSTRUCTORS:
    new PickPointSelectedSubObject(FullSubentityPath path, SelectionMethod method, IntPtr gsMarker, PickPointDescriptor descriptor)
    new PickPointSelectedSubObject(FullSubentityPath path, SelectionMethod method, long gsMarker, PickPointDescriptor descriptor)

  PROPERTIES:
    PickPointDescriptor PickPoint { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PointFilterEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PointFilterEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    bool CallNext { get; set; }
    InputPointContext Context { get; }
    PointFilterResult Result { get; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] PointFilterEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PointFilterEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PointFilterEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PointFilterEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PointFilterEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PointFilterResult
Full Name: Autodesk.AutoCAD.EditorInput.PointFilterResult
Implements: IDisposable

  PROPERTIES:
    bool DisplayObjectSnapGlyph { get; set; }
    Point3d NewPoint { get; set; }
    bool Retry { get; set; }
    string ToolTipText { get; set; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[ENUM] PointHistoryBits
Full Name: Autodesk.AutoCAD.EditorInput.PointHistoryBits
Inherits: Enum

  Values:
    - DidNotPick = 0
    - Tablet = 1
    - NotDigitizer = 2
    - LastPoint = 4
    - Gripped = 8
    - CartSnapped = 16
    - Ortho = 32
    - CyclingPoint = 64
    - ObjectSnapped = 128
    - PolarAngle = 256
    - Aligned = 1024
    - AppFiltered = 2048
    - ForcedPick = 4096
    - UsedPickBox = 8192
    - UsedObjectSnapBox = 16384
    - PickAborted = 32768
    - PickMask = 57344
    - XPending = 65536
    - YPending = 131072
    - ZPending = 262144
    - CoordinatePending = 458752
    - FromKeyboard = 524288
    - NotInteractive = 1048576

--------------------------------------------------------------------------------

[CLASS] PointInputEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PointInputEventArgs
Inherits: EventArgs

  PROPERTIES:
    InputPointContext Context { get; }
    IntPtr GraphicsSystemMarker { get; }
    bool Handled { get; set; }
    PointFilterResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PointInputEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PointInputEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PointInputEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PointInputEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PointInputEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PointMonitorEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PointMonitorEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    InputPointContext Context { get; }

  METHODS:
    void AppendToolTipText(string value)
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] PointMonitorEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PointMonitorEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PointMonitorEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PointMonitorEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PointMonitorEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptAngleOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptAngleOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptAngleOptions(string messageAndKeywords, string globalKeywords)
    new PromptAngleOptions(string message)

  PROPERTIES:
    bool AllowArbitraryInput { get; set; }
    bool AllowNone { get; set; }
    bool AllowZero { get; set; }
    Point3d BasePoint { get; set; }
    double DefaultValue { get; set; }
    bool UseAngleBase { get; set; }
    bool UseBasePoint { get; set; }
    bool UseDashedLine { get; set; }
    bool UseDefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptAngleOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptAngleOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptAngleOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptAngleOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptAngleOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptAngleOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptAngleOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptAngleOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptCornerOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptCornerOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptCornerOptions(string messageAndKeywords, string globalKeywords, Point3d basePoint)
    new PromptCornerOptions(string message, Point3d basePoint)

  PROPERTIES:
    bool AllowArbitraryInput { get; set; }
    bool AllowNone { get; set; }
    Point3d BasePoint { get; set; }
    bool LimitsChecked { get; set; }
    bool UseDashedLine { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptDistanceOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptDistanceOptions
Inherits: PromptNumericalOptions

  CONSTRUCTORS:
    new PromptDistanceOptions(string messageAndKeywords, string globalKeywords)
    new PromptDistanceOptions(string message)

  PROPERTIES:
    Point3d BasePoint { get; set; }
    double DefaultValue { get; set; }
    bool Only2d { get; set; }
    bool UseBasePoint { get; set; }
    bool UseDashedLine { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptDistanceOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptDistanceOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptDistanceOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptDistanceOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptDistanceOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptDistanceOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptDistanceOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptDistanceOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptDoubleOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptDoubleOptions
Inherits: PromptNumericalOptions

  CONSTRUCTORS:
    new PromptDoubleOptions(string messageAndKeywords, string globalKeywords)
    new PromptDoubleOptions(string message)

  PROPERTIES:
    double DefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptDoubleOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptDoubleOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptDoubleOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptDoubleOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptDoubleOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptDoubleOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptDoubleOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptDoubleOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptDoubleResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptDoubleResult
Inherits: PromptResult

  PROPERTIES:
    double Value { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptDoubleResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptDoubleResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptDoubleResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptDoubleResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptDoubleResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptDoubleResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptDoubleResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptDoubleResultEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptDragOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptDragOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptDragOptions(SelectionSet selection, string messageAndKeywords, string globalKeywords, DragCallback callback)
    new PromptDragOptions(SelectionSet selection, string message, DragCallback callback)

  PROPERTIES:
    bool AllowArbitraryInput { get; set; }
    bool AllowNone { get; set; }
    DragCallback Callback { get; set; }
    DragCursor Cursor { get; set; }
    SelectionSet Selection { get; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PromptEditorOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptEditorOptions
Inherits: PromptOptions

--------------------------------------------------------------------------------

[CLASS] PromptEntityOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptEntityOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptEntityOptions(string messageAndKeywords, string globalKeywords)
    new PromptEntityOptions(string message)

  PROPERTIES:
    bool AllowNone { get; set; }
    bool AllowObjectOnLockedLayer { get; set; }

  METHODS:
    void AddAllowedClass(Type type, bool exactMatch)
    void RemoveAllowedClass(Type type)
    void SetRejectMessage(string message)

--------------------------------------------------------------------------------

[CLASS] PromptEntityOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptEntityOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptEntityOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptEntityOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptEntityOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptEntityOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptEntityOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptEntityOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptEntityResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptEntityResult
Inherits: PromptResult

  PROPERTIES:
    ObjectId ObjectId { get; }
    Point3d PickedPoint { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptEntityResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptEntityResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptEntityResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptEntityResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptEntityResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptEntityResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptEntityResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptEntityResultEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptFileNameResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptFileNameResult
Inherits: PromptResult

  PROPERTIES:
    bool ReadOnly { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PromptFileOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptFileOptions

  PROPERTIES:
    bool AllowUrls { get; set; }
    string DialogCaption { get; set; }
    string DialogName { get; set; }
    string Filter { get; set; }
    int FilterIndex { get; set; }
    string InitialDirectory { get; set; }
    string InitialFileName { get; set; }
    string Message { get; set; }
    bool PreferCommandLine { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptForEntityEndingEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptForEntityEndingEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    PromptEntityResult Result { get; }

  METHODS:
    void Dispose()
    void RemoveSelectedObject()
    void ReplaceSelectedObject(SelectedObject newValue)

--------------------------------------------------------------------------------

[CLASS] PromptForEntityEndingEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptForEntityEndingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptForEntityEndingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptForEntityEndingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptForEntityEndingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptForSelectionEndingEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptForSelectionEndingEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    SelectionFlags Flags { get; }
    SelectionSet Selection { get; }

  METHODS:
    void Add(SelectedObject value)
    void AddSubEntity(SelectedSubObject value)
    void Dispose()
    void Remove(int index)
    void RemoveSubEntity(int entityIndex, int subEntityIndex)

--------------------------------------------------------------------------------

[CLASS] PromptForSelectionEndingEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptForSelectionEndingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptForSelectionEndingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptForSelectionEndingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptForSelectionEndingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptIntegerOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptIntegerOptions
Inherits: PromptNumericalOptions

  CONSTRUCTORS:
    new PromptIntegerOptions(string messageAndKeywords, string globalKeywords, int lowerLimit, int upperLimit)
    new PromptIntegerOptions(string messageAndKeywords, string globalKeywords)
    new PromptIntegerOptions(string message)

  PROPERTIES:
    int DefaultValue { get; set; }
    int LowerLimit { get; set; }
    int UpperLimit { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptIntegerOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptIntegerOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptIntegerOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptIntegerOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptIntegerOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptIntegerOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptIntegerOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptIntegerOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptIntegerResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptIntegerResult
Inherits: PromptResult

  PROPERTIES:
    int Value { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptIntegerResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptIntegerResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptIntegerResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptIntegerResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptIntegerResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptIntegerResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptIntegerResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptIntegerResultEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptKeywordOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptKeywordOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptKeywordOptions(string messageAndKeywords, string globalKeywords)
    new PromptKeywordOptions(string message)

  PROPERTIES:
    bool AllowArbitraryInput { get; set; }
    bool AllowNone { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptKeywordOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptKeywordOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptKeywordOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptKeywordOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptKeywordOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptKeywordOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptKeywordOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptKeywordOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptNestedEntityOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptNestedEntityOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptNestedEntityOptions(string messageAndKeywords, string globalKeywords)
    new PromptNestedEntityOptions(string message)

  PROPERTIES:
    bool AllowNone { get; set; }
    Point3d NonInteractivePickPoint { get; set; }
    bool UseNonInteractivePickPoint { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptNestedEntityOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptNestedEntityOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptNestedEntityOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptNestedEntityOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptNestedEntityOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptNestedEntityOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptNestedEntityOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptNestedEntityOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptNestedEntityResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptNestedEntityResult
Inherits: PromptEntityResult

  PROPERTIES:
    Matrix3d Transform { get; }

  METHODS:
    ObjectId[] GetContainers()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptNestedEntityResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptNestedEntityResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptNestedEntityResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptNestedEntityResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptNestedEntityResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptNestedEntityResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptNestedEntityResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptNestedEntityResultEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptNumericalOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptNumericalOptions
Inherits: PromptEditorOptions

  PROPERTIES:
    bool AllowArbitraryInput { get; set; }
    bool AllowNegative { get; set; }
    bool AllowNone { get; set; }
    bool AllowZero { get; set; }
    bool UseDefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptOpenFileOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptOpenFileOptions
Inherits: PromptFileOptions

  CONSTRUCTORS:
    new PromptOpenFileOptions(string message)

  PROPERTIES:
    bool SearchPath { get; set; }
    bool TransferRemoteFiles { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PromptOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptOptions

  PROPERTIES:
    bool AppendKeywordsToMessage { get; set; }
    bool IsReadOnly { get; }
    KeywordCollection Keywords { get; }
    string Message { get; set; }

  METHODS:
    void SetMessageAndKeywords(string messageAndKeywords, string globalKeywords)

--------------------------------------------------------------------------------

[CLASS] PromptPointOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptPointOptions
Inherits: PromptCornerOptions

  CONSTRUCTORS:
    new PromptPointOptions(string messageAndKeywords, string globalKeywords)
    new PromptPointOptions(string message)

  PROPERTIES:
    bool UseBasePoint { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptPointOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptPointOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptPointOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptPointOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptPointOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptPointOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptPointOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptPointOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptPointResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptPointResult
Inherits: PromptResult

  PROPERTIES:
    Point3d Value { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptPointResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptPointResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptPointResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptPointResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptPointResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptPointResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptPointResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptPointResultEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptResult

  PROPERTIES:
    PromptStatus Status { get; }
    string StringResult { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptSaveFileOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptSaveFileOptions
Inherits: PromptFileOptions

  CONSTRUCTORS:
    new PromptSaveFileOptions(string message)

  PROPERTIES:
    bool DeriveInitialFilenameFromDrawingName { get; set; }
    bool DisplaySaveOptionsMenuItem { get; set; }
    bool ForceOverwriteWarningForScriptsAndLisp { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptSelectionOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptSelectionOptions

  CONSTRUCTORS:
    new PromptSelectionOptions()

  PROPERTIES:
    bool AllowDuplicates { get; set; }
    bool AllowSubSelections { get; set; }
    bool ForceSubSelections { get; set; }
    KeywordCollection Keywords { get; }
    string MessageForAdding { get; set; }
    string MessageForRemoval { get; set; }
    bool PrepareOptionalDetails { get; set; }
    bool RejectObjectsFromNonCurrentSpace { get; set; }
    bool RejectObjectsOnLockedLayers { get; set; }
    bool RejectPaperspaceViewport { get; set; }
    bool SelectEverythingInAperture { get; set; }
    bool SingleOnly { get; set; }
    bool SinglePickInSpace { get; set; }

  METHODS:
    void AddKeywordsToMinimalList(AddedKeywords kwds)
    void RemoveKeywordsFromFullList(SubtractedKeywords kwds)
    void SetKeywords(string keywords, string globalKeywords)

  EVENTS:
    event SelectionTextInputEventHandler KeywordInput
    event SelectionTextInputEventHandler UnknownInput

--------------------------------------------------------------------------------

[CLASS] PromptSelectionOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptSelectionOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    SelectionFilter Filter { get; }
    PromptSelectionOptions Options { get; }

  METHODS:
    Point3d[] GetPoints()

--------------------------------------------------------------------------------

[CLASS] PromptSelectionOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptSelectionOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptSelectionOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptSelectionOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptSelectionOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptSelectionResult
Full Name: Autodesk.AutoCAD.EditorInput.PromptSelectionResult

  PROPERTIES:
    PromptStatus Status { get; }
    SelectionSet Value { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] PromptSelectionResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptSelectionResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptSelectionResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptSelectionResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptSelectionResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptSelectionResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptSelectionResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptSelectionResultEventArgs e)

--------------------------------------------------------------------------------

[ENUM] PromptStatus
Full Name: Autodesk.AutoCAD.EditorInput.PromptStatus
Inherits: Enum

  Values:
    - None = 5000
    - Modeless = 5027
    - Other = 5028
    - OK = 5100
    - Keyword = -5005
    - Cancel = -5002
    - Error = -5001

--------------------------------------------------------------------------------

[CLASS] PromptStringOptions
Full Name: Autodesk.AutoCAD.EditorInput.PromptStringOptions
Inherits: PromptEditorOptions

  CONSTRUCTORS:
    new PromptStringOptions(string message)

  PROPERTIES:
    bool AllowSpaces { get; set; }
    string DefaultValue { get; set; }
    bool UseDefaultValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] PromptStringOptionsEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptStringOptionsEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptStringOptions Options { get; }

--------------------------------------------------------------------------------

[CLASS] PromptStringOptionsEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptStringOptionsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptStringOptionsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptStringOptionsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptStringOptionsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PromptStringResultEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.PromptStringResultEventArgs
Inherits: EventArgs

  PROPERTIES:
    PromptResult Result { get; }

--------------------------------------------------------------------------------

[CLASS] PromptStringResultEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.PromptStringResultEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PromptStringResultEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PromptStringResultEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PromptStringResultEventArgs e)

--------------------------------------------------------------------------------

