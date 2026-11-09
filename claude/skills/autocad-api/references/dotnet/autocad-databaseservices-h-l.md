# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[STRUCT] Handle
Full Name: Autodesk.AutoCAD.DatabaseServices.Handle

  CONSTRUCTORS:
    new Handle(long value)

  PROPERTIES:
    bool IsOne { get; }
    long Value { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] Hatch
Full Name: Autodesk.AutoCAD.DatabaseServices.Hatch
Inherits: Entity

  CONSTRUCTORS:
    new Hatch()

  PROPERTIES:
    double Area { get; }
    bool Associative { get; set; }
    Color BackgroundColor { get; set; }
    double Elevation { get; set; }
    double GradientAngle { get; set; }
    string GradientName { get; }
    bool GradientOneColorMode { get; set; }
    float GradientShift { get; set; }
    GradientPatternType GradientType { get; }
    HatchObjectType HatchObjectType { get; set; }
    HatchStyle HatchStyle { get; set; }
    bool IsGradient { get; }
    bool IsHatch { get; }
    bool IsSolidFill { get; }
    Vector3d Normal { get; set; }
    int NumberOfHatchLines { get; }
    int NumberOfLoops { get; }
    int NumberOfPatternDefinitions { get; }
    Point2d Origin { get; set; }
    double PatternAngle { get; set; }
    bool PatternDouble { get; set; }
    string PatternName { get; }
    double PatternScale { get; set; }
    double PatternSpace { get; set; }
    HatchPatternType PatternType { get; }
    float ShadeTintValue { get; set; }

  METHODS:
    void AppendLoop(HatchLoopTypes loopType, Point2dCollection vertexCollection, DoubleCollection bulgeCollection)
    void AppendLoop(HatchLoopTypes loopType, Curve2dCollection edgePtrCollection, IntegerCollection edgeTypeCollection)
    void AppendLoop(HatchLoopTypes loopType, ObjectIdCollection dbObjIds)
    void AppendLoop(HatchLoop hatchLoop)
    Color EvaluateGradientColorAt(float value)
    void EvaluateHatch(bool underEstimateNumLines)
    ObjectIdCollection GetAssociatedObjectIds()
    ObjectIdCollection GetAssociatedObjectIdsAt(int loopIndex)
    GradientColor[] GetGradientColors()
    Line2d GetHatchLineDataAt(int index)
    Line2dCollection GetHatchLinesData()
    HatchLoop GetLoopAt(int loopIndex)
    PatternDefinition GetPatternDefinitionAt(int index)
    void InsertLoopAt(int loopIndex, HatchLoopTypes loopType, ObjectIdCollection dbObjIds)
    void InsertLoopAt(int loopIndex, HatchLoop hatchLoop)
    HatchLoopTypes LoopTypeAt(int loopIndex)
    void RemoveAssociatedObjectIds()
    void RemoveLoopAt(int loopIndex)
    void SetGradient(GradientPatternType gradientType, string gradientName)
    void SetGradientColors(GradientColor[] value)
    void SetHatchPattern(HatchPatternType patternType, string patternName)

--------------------------------------------------------------------------------

[ENUM] HatchEdgeType
Full Name: Autodesk.AutoCAD.DatabaseServices.HatchEdgeType
Inherits: Enum

  Values:
    - Line = 1
    - CircularArc = 2
    - EllipticalArc = 3
    - Spline = 4

--------------------------------------------------------------------------------

[CLASS] HatchLoop
Full Name: Autodesk.AutoCAD.DatabaseServices.HatchLoop

  CONSTRUCTORS:
    new HatchLoop(HatchLoopTypes loopType)

  PROPERTIES:
    Curve2dCollection Curves { get; }
    bool IsPolyline { get; }
    HatchLoopTypes LoopType { get; }
    BulgeVertexCollection Polyline { get; }

--------------------------------------------------------------------------------

[ENUM] HatchLoopTypes
Full Name: Autodesk.AutoCAD.DatabaseServices.HatchLoopTypes
Inherits: Enum

  Values:
    - Default = 0
    - External = 1
    - Polyline = 2
    - Derived = 4
    - Textbox = 8
    - Outermost = 16
    - NotClosed = 32
    - SelfIntersecting = 64
    - TextIsland = 128
    - Duplicate = 256

--------------------------------------------------------------------------------

[ENUM] HatchObjectType
Full Name: Autodesk.AutoCAD.DatabaseServices.HatchObjectType
Inherits: Enum

  Values:
    - HatchObject = 0
    - GradientObject = 1

--------------------------------------------------------------------------------

[ENUM] HatchPatternType
Full Name: Autodesk.AutoCAD.DatabaseServices.HatchPatternType
Inherits: Enum

  Values:
    - UserDefined = 0
    - PreDefined = 1
    - CustomDefined = 2

--------------------------------------------------------------------------------

[ENUM] HatchStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.HatchStyle
Inherits: Enum

  Values:
    - Normal = 0
    - Outer = 1
    - Ignore = 2

--------------------------------------------------------------------------------

[CLASS] Helix
Full Name: Autodesk.AutoCAD.DatabaseServices.Helix
Inherits: Spline

  CONSTRUCTORS:
    new Helix()

  PROPERTIES:
    Vector3d AxisVector { get; set; }
    double BaseRadius { get; set; }
    ConstrainType Constrain { get; set; }
    double Height { get; set; }
    Point3d StartPoint { get; set; }
    double TopRadius { get; set; }
    double TotalLength { get; }
    double TurnHeight { get; set; }
    double Turns { get; set; }
    double TurnSlope { get; }
    bool Twist { get; set; }

  METHODS:
    void CreateHelix()
    Point3d GetAxisPoint()
    void SetAxisPoint(Point3d axisPoint, bool moveStartPoint)

--------------------------------------------------------------------------------

[CLASS] HelpParameter
Full Name: Autodesk.AutoCAD.DatabaseServices.HelpParameter
Inherits: ConstraintGroupNode

  CONSTRUCTORS:
    new HelpParameter()

  PROPERTIES:
    GeometricalConstraint ConnectedConstraint { get; }
    EqualHelpParameterConstraint[] ConnectedEqualparamConstraints { get; }
    ConstrainedGeometry ConnectedGeometry { get; }
    double Value { get; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] HighlightOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.HighlightOverrule
Inherits: Overrule

  METHODS:
    void Highlight(Entity entity, FullSubentityPath subId, bool highlightAll)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)
    void Unhighlight(Entity entity, FullSubentityPath subId, bool highlightAll)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] HighlightStateOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.HighlightStateOverrule
Inherits: Overrule

  METHODS:
    HighlightStyle HighlightState(Entity entity, FullSubentityPath subId)
    void PopHighlight(Entity entity, FullSubentityPath subId)
    void PushHighlight(Entity entity, FullSubentityPath subId, HighlightStyle highlightStyle)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

[CLASS] HorizontalConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.HorizontalConstraint
Inherits: ParallelConstraint

  CONSTRUCTORS:
    new HorizontalConstraint()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] HostApplicationServices
Full Name: Autodesk.AutoCAD.DatabaseServices.HostApplicationServices
Inherits: RXObject

  PROPERTIES:
    string AllUsersRootFolder { get; }
    string AlternateFontName { get; }
    string ColorBookLocation { get; }
    string CompanyName { get; }
    static HostApplicationServices Current { get; set; }
    string FontMapFileName { get; }
    string LocalRootFolder { get; }
    string MachineRegistryProductRootKey { get; }
    ModelerFlavor ModelerFlavor { get; }
    string Product { get; }
    string Program { get; }
    string releaseMarketVersion { get; }
    string RoamableRootFolder { get; }
    string UserRegistryProductRootKey { get; }
    static Database WorkingDatabase { get; set; }

  METHODS:
    void FatalError(string message)
    string FindFile(string fileName, Database database, FindFileHint hint)
    string GetEnvironmentVariable(string name)
    string GetPassword(string dwgName, PasswordOptions options)
    string GetRemoteFile(Uri url, bool ignoreCache)
    Uri GetUrl(string localFile)
    bool IsUrl(string filePath)
    void LoadApplication(string appName, ApplicationLoadReasons why, bool printIt, bool asCmd)
    bool NotifyCorruptDrawingFoundOnOpen(ObjectId id, ErrorStatus es)
    void PutRemoteFile(Uri url, string localFile)
    bool UserBreak()

--------------------------------------------------------------------------------

[CLASS] HyperLink
Full Name: Autodesk.AutoCAD.DatabaseServices.HyperLink
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new HyperLink()

  PROPERTIES:
    string Description { get; set; }
    string DisplayString { get; }
    bool IsOutermostContainer { get; }
    string Name { get; set; }
    int NestedLevel { get; }
    string SubLocation { get; set; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()

--------------------------------------------------------------------------------

[CLASS] HyperLinkCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.HyperLinkCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    HyperLink Item { get; set; }

  METHODS:
    int Add(HyperLink value)
    void Clear()
    bool Contains(HyperLink value)
    void CopyTo(HyperLink[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(HyperLink value)
    void Insert(int index, HyperLink value)
    void Remove(HyperLink value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] IBLBackground
Full Name: Autodesk.AutoCAD.DatabaseServices.IBLBackground
Inherits: Background

  CONSTRUCTORS:
    new IBLBackground()

  PROPERTIES:
    bool DisplayImage { get; set; }
    bool Enable { get; set; }
    string IBLImageName { get; set; }
    double Rotation { get; set; }
    ObjectId SecondaryBackground { get; set; }

--------------------------------------------------------------------------------

[CLASS] IdMapping
Full Name: Autodesk.AutoCAD.DatabaseServices.IdMapping
Inherits: RXObject
Implements: IEnumerable

  CONSTRUCTORS:
    new IdMapping()

  PROPERTIES:
    DeepCloneType DeepCloneContext { get; }
    Database DestinationDatabase { get; set; }
    DuplicateRecordCloning DuplicateRecordCloning { get; }
    IdPair Item { get; set; }
    Database OriginalDatabase { get; }

  METHODS:
    void Add(IdPair pairToAdd)
    void Change(IdPair pairToChange)
    bool Contains(ObjectId key)
    void Delete(ObjectId key)
    IEnumerator GetEnumerator()
    IdPair Lookup(ObjectId key)

--------------------------------------------------------------------------------

[CLASS] IdMappingEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.IdMappingEventArgs
Inherits: EventArgs

  PROPERTIES:
    IdMapping IdMapping { get; }

--------------------------------------------------------------------------------

[CLASS] IdMappingEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.IdMappingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new IdMappingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, IdMappingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, IdMappingEventArgs e)

--------------------------------------------------------------------------------

[STRUCT] IdPair
Full Name: Autodesk.AutoCAD.DatabaseServices.IdPair

  CONSTRUCTORS:
    new IdPair(ObjectId key, ObjectId value, bool isCloned, bool isPrimary, bool isOwnerTranslated)

  PROPERTIES:
    bool IsCloned { get; }
    bool IsOwnerTranslated { get; }
    bool IsPrimary { get; }
    ObjectId Key { get; }
    ObjectId Value { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] Image
Full Name: Autodesk.AutoCAD.DatabaseServices.Image
Inherits: Entity

--------------------------------------------------------------------------------

[CLASS] ImageBackground
Full Name: Autodesk.AutoCAD.DatabaseServices.ImageBackground
Inherits: Background

  CONSTRUCTORS:
    new ImageBackground()

  PROPERTIES:
    bool FitToScreen { get; set; }
    string ImageFileName { get; set; }
    bool MaintainAspectRatio { get; set; }
    Vector2d Offset { get; set; }
    Scale2d Scale { get; set; }
    bool UseTiling { get; set; }

--------------------------------------------------------------------------------

[ENUM] ImageDisplayOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.ImageDisplayOptions
Inherits: Enum

  Values:
    - Show = 1
    - ShowUnaligned = 2
    - Clip = 4
    - Transparent = 8

--------------------------------------------------------------------------------

[ENUM] ImageQuality
Full Name: Autodesk.AutoCAD.DatabaseServices.ImageQuality
Inherits: Enum

  Values:
    - Draft = 0
    - High = 1
    - Invalid = -1

--------------------------------------------------------------------------------

[ENUM] ImplicitPointType
Full Name: Autodesk.AutoCAD.DatabaseServices.ImplicitPointType
Inherits: Enum

  Values:
    - StartImplicit = 0
    - EndImplicit = 1
    - MidImplicit = 2
    - CenterImplicit = 3
    - DefineImplicit = 4

--------------------------------------------------------------------------------

[ENUM] IndexCreation
Full Name: Autodesk.AutoCAD.DatabaseServices.IndexCreation
Inherits: Enum

  Values:
    - NoIndex = 0
    - IndexByLayer = 1
    - IndexSpatially = 2

--------------------------------------------------------------------------------

[CLASS] InterferenceProtocolExtension
Full Name: Autodesk.AutoCAD.DatabaseServices.InterferenceProtocolExtension
Inherits: RXObject

  METHODS:
    Entity[] CreateInterferenceObjects(Entity ent1, Entity ent2, int flags)

--------------------------------------------------------------------------------

[ENUM] Intersect
Full Name: Autodesk.AutoCAD.DatabaseServices.Intersect
Inherits: Enum

  Values:
    - OnBothOperands = 0
    - ExtendThis = 1
    - ExtendArgument = 2
    - ExtendBoth = 3

--------------------------------------------------------------------------------

[INTERFACE] IParameter
Full Name: Autodesk.AutoCAD.DatabaseServices.IParameter
Implements: IDisposable

  PROPERTIES:
    bool Angular { get; }
    DxfCode DataType { get; }
    string Description { get; set; }
    string Expression { get; set; }
    string Name { get; set; }
    DBObject ParameterObject { get; }
    bool ReadOnly { get; }
    ResultBuffer Value { get; set; }
    ParameterValueSet ValueSet { get; }

  METHODS:
    bool IsNameUnique(string name)

--------------------------------------------------------------------------------

[INTERFACE] ISpecialValueConverter
Full Name: Autodesk.AutoCAD.DatabaseServices.ISpecialValueConverter

  METHODS:
    string ConvertToGlobal(object o, string value, ref bool converted)
    string ConvertToLocal(object o, string value, ref bool converted)
    string[] GetSpecialValues()

--------------------------------------------------------------------------------

[INTERFACE] ISubObject
Full Name: Autodesk.AutoCAD.DatabaseServices.ISubObject

  PROPERTIES:
    object Parent { get; set; }

--------------------------------------------------------------------------------

[STRUCT] ItemLocator
Full Name: Autodesk.AutoCAD.DatabaseServices.ItemLocator

  CONSTRUCTORS:
    new ItemLocator(int itemIndex, int rowIndex, int levelIndex)

  PROPERTIES:
    int ItemIndex { get; set; }
    int LevelIndex { get; set; }
    int RowIndex { get; set; }

--------------------------------------------------------------------------------

[INTERFACE] ITextEditorSelectable
Full Name: Autodesk.AutoCAD.DatabaseServices.ITextEditorSelectable

  PROPERTIES:
    TextEditorLocation EndOfText { get; }
    TextEditorLocation StartOfText { get; }

--------------------------------------------------------------------------------

[ENUM] JoinStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.JoinStyle
Inherits: Enum

  Values:
    - StyleNone = 0
    - StyleRound = 1
    - StyleAngle = 2
    - StyleFlat = 3

--------------------------------------------------------------------------------

[ENUM] LampColorPreset
Full Name: Autodesk.AutoCAD.DatabaseServices.LampColorPreset
Inherits: Enum

  Values:
    - D65White = 0
    - Fluorescent = 1
    - CoolWhite = 2
    - WhiteFluorescent = 3
    - DaylightFluorescent = 4
    - Incandescent = 5
    - Xenon = 6
    - Halogen = 7
    - Quartz = 8
    - MetalHalide = 9
    - Mercury = 10
    - PhosphorMercury = 11
    - HighPressureSodium = 12
    - LowPressureSodium = 13
    - Custom = 14

--------------------------------------------------------------------------------

[ENUM] LampColorType
Full Name: Autodesk.AutoCAD.DatabaseServices.LampColorType
Inherits: Enum

  Values:
    - Kelvin = 0
    - Preset = 1

--------------------------------------------------------------------------------

[ENUM] LayerEvaluation
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerEvaluation
Inherits: Enum

  Values:
    - NoNewLayerEvaluation = 0
    - EvalNewXrefLayers = 1
    - EvalAllNewLayers = 2

--------------------------------------------------------------------------------

[CLASS] LayerStateDeletedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateDeletedEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] LayerStateDeletedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateDeletedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayerStateDeletedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayerStateDeletedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayerStateDeletedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LayerStateEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId Id { get; }
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] LayerStateEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayerStateEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayerStateEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayerStateEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LayerStateManager
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateManager
Inherits: RXObject

  CONSTRUCTORS:
    new LayerStateManager(Database database)

  PROPERTIES:
    string LastRestoredLayerState { get; }

  METHODS:
    bool CompareLayerStateToDb(string name, ObjectId idVp)
    void DeleteLayerState(string name)
    void ExportLayerState(string nameToExport, string fileName)
    string GetLayerStateDescription(string name)
    ArrayList GetLayerStateLayers(string name, bool bInvert)
    LayerStateMasks GetLayerStateMask(string name)
    ArrayList GetLayerStateNames(bool bIncludeHidden, bool bIncludeXref)
    bool HasLayerState(string name)
    void ImportLayerState(string fileName)
    void ImportLayerStateFromDb(string name, Database database)
    bool LayerStateHasViewportData(string name)
    ObjectId LayerStatesDictionaryId(bool createIfNotPresent)
    void RenameLayerState(string name, string newName)
    void RestoreLayerState(string name, ObjectId id, int undefinedLayerStatePolicy, LayerStateMasks clientMask)
    void SaveLayerState(string name, LayerStateMasks mask, ObjectId id)
    void SetLayerStateDescription(string name, string description)
    void SetLayerStateMask(string name, LayerStateMasks mask)

  EVENTS:
    event LayerStateRenameEventHandler AbortLayerStateRename
    event LayerStateRenameEventHandler LayerStateRenamed
    event LayerStateRenameEventHandler LayerStateToBeRenamed
    event LayerStateEventHandler AbortLayerStateDelete
    event LayerStateDeletedEventHandler LayerStateDeleted
    event LayerStateEventHandler LayerStateToBeDeleted
    event LayerStateEventHandler AbortLayerStateRestore
    event LayerStateEventHandler LayerStateRestored
    event LayerStateEventHandler LayerStateToBeRestored
    event LayerStateEventHandler LayerStateCompareFailed
    event LayerStateEventHandler LayerStateCreated

--------------------------------------------------------------------------------

[ENUM] LayerStateMasks
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateMasks
Inherits: Enum

  Values:
    - None = 0
    - On = 1
    - Frozen = 2
    - Locked = 4
    - Plot = 8
    - NewViewport = 16
    - Color = 32
    - LineType = 64
    - LineWeight = 128
    - PlotStyle = 256
    - CurrentViewport = 512
    - Transparency = 1024
    - LastRestored = 65536

--------------------------------------------------------------------------------

[CLASS] LayerStateRenameEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateRenameEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Name { get; }
    string NewName { get; }

--------------------------------------------------------------------------------

[CLASS] LayerStateRenameEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerStateRenameEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayerStateRenameEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayerStateRenameEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayerStateRenameEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LayerTable
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerTable
Inherits: SymbolTable

  PROPERTIES:
    bool HasUnreconciledLayers { get; }
    LayerTable IncludingHidden { get; }
    LayerTable SkippingReconciled { get; }

  METHODS:
    void GenerateUsageData()
    SymbolTableEnumerator GetEnumerator()
    ObjectIdCollection GetUnreconciledLayers()

--------------------------------------------------------------------------------

[CLASS] LayerTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerTableRecord
Inherits: SymbolTableRecord

  CONSTRUCTORS:
    new LayerTableRecord()

  PROPERTIES:
    Color Color { get; set; }
    string Description { get; set; }
    EntityColor EntityColor { get; }
    bool HasOverrides { get; }
    bool IsFrozen { get; set; }
    bool IsHidden { get; set; }
    bool IsLocked { get; set; }
    bool IsOff { get; set; }
    bool IsPlottable { get; set; }
    bool IsReconciled { get; set; }
    bool IsUsed { get; }
    ObjectId LinetypeObjectId { get; set; }
    LineWeight LineWeight { get; set; }
    ObjectId MaterialId { get; set; }
    string PlotStyleName { get; set; }
    ObjectId PlotStyleNameId { get; set; }
    Transparency Transparency { get; set; }
    bool ViewportVisibilityDefault { get; set; }

  METHODS:
    LayerViewportProperties GetViewportOverrides(ObjectId viewportId)
    bool HasViewportOverrides(ObjectId viewportId)
    void RemoveAllOverrides()

--------------------------------------------------------------------------------

[CLASS] LayerViewportProperties
Full Name: Autodesk.AutoCAD.DatabaseServices.LayerViewportProperties

  PROPERTIES:
    Color Color { get; set; }
    bool IsColorOverridden { get; set; }
    bool IsLinetypeOverridden { get; set; }
    bool IsLineWeightOverridden { get; set; }
    bool IsPlotStyleOverridden { get; set; }
    bool IsTransparencyOverridden { get; set; }
    ObjectId LinetypeObjectId { get; set; }
    LineWeight LineWeight { get; set; }
    string PlotStyleName { get; set; }
    ObjectId PlotStyleNameId { get; set; }
    Transparency Transparency { get; set; }

  METHODS:
    void RemoveOverrides()

--------------------------------------------------------------------------------

[CLASS] Layout
Full Name: Autodesk.AutoCAD.DatabaseServices.Layout
Inherits: PlotSettings

  CONSTRUCTORS:
    new Layout()

  PROPERTIES:
    bool AnnoAllVisible { get; set; }
    ObjectId BlockTableRecordId { get; set; }
    Extents3d Extents { get; }
    string LayoutName { get; set; }
    Extents2d Limits { get; }
    int TabOrder { get; set; }
    bool TabSelected { get; set; }
        [SKIPPED PROPERTY] Thumbnail: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.

  METHODS:
    void AddToLayoutDictionary(Database toWhichDatabase, ObjectId blockTableRecordId)
    ObjectIdCollection GetViewports()
    ObjectId Initialize()

--------------------------------------------------------------------------------

[CLASS] LayoutCopiedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutCopiedEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId Id { get; }
    string Name { get; }
    ObjectId NewId { get; }
    string NewName { get; }

--------------------------------------------------------------------------------

[CLASS] LayoutCopiedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutCopiedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayoutCopiedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayoutCopiedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayoutCopiedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LayoutEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId Id { get; }
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] LayoutEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayoutEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayoutEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayoutEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LayoutManager
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutManager
Inherits: RXObject

  PROPERTIES:
    static LayoutManager Current { get; }
    string CurrentLayout { get; set; }
    int LayoutCount { get; }

  METHODS:
    void CloneLayout(string copyName, string newName, int newTabOrder)
    void CopyLayout(string copyName, string newName)
    ObjectId CreateLayout(string newName)
    void DeleteLayout(string deleteName)
    ObjectId GetLayoutId(string name)
    ObjectId GetNonRectangularViewportIdFromClipId(ObjectId clipId)
    bool LayoutExists(string name)
    void RenameLayout(string oldName, string newName)
    void SetCurrentLayoutId(ObjectId layoutId)

  EVENTS:
    event EventHandler LayoutsRefresh
    event EventHandler LayoutsReordered
    event PlotStyleTableChangedEventHandler PlotStyleTableChanged
    event LayoutEventHandler LayoutSwitched
    event LayoutRenamedEventHandler AbortLayoutRename
    event LayoutRenamedEventHandler LayoutRenamed
    event LayoutRenamedEventHandler LayoutToBeRenamed
    event LayoutEventHandler AbortLayoutCopied
    event LayoutCopiedEventHandler LayoutCopied
    event LayoutEventHandler LayoutToBeCopied
    event LayoutEventHandler AbortLayoutRemoved
    event LayoutEventHandler LayoutRemoved
    event LayoutEventHandler LayoutToBeRemoved
    event LayoutEventHandler LayoutCreated

--------------------------------------------------------------------------------

[CLASS] LayoutRenamedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId Id { get; }
    string Name { get; }
    string NewName { get; }

--------------------------------------------------------------------------------

[CLASS] LayoutRenamedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayoutRenamedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayoutRenamedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayoutRenamedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] Leader
Full Name: Autodesk.AutoCAD.DatabaseServices.Leader
Inherits: Curve

  CONSTRUCTORS:
    new Leader()

  PROPERTIES:
    double AnnoHeight { get; }
    ObjectId Annotation { get; set; }
    Vector3d AnnotationOffset { get; set; }
    AnnotationType AnnoType { get; }
    double AnnoWidth { get; }
    double Dimasz { get; set; }
    Color Dimclrd { get; set; }
    ObjectId DimensionStyle { get; set; }
    string DimensionStyleName { get; set; }
    double Dimgap { get; set; }
    ObjectId Dimldrblk { get; set; }
    LineWeight Dimlwd { get; set; }
    bool Dimsah { get; set; }
    double Dimscale { get; set; }
    int Dimtad { get; set; }
    double Dimtxt { get; set; }
    Point3d FirstVertex { get; }
    bool HasArrowHead { get; set; }
    bool HasHookLine { get; }
    bool IsSplined { get; set; }
    Point3d LastVertex { get; }
    Vector3d Normal { get; }
    int NumVertices { get; }
    ObjectId TextStyleId { get; set; }

  METHODS:
    bool AppendVertex(Point3d pointToAdd)
    void EvaluateLeader()
    DimStyleTableRecord GetDimstyleData()
    void RemoveLastVertex()
    void SetDimstyleData(DimStyleTableRecord style)
    void SetPlane(Plane value)
    bool SetVertexAt(int index, Point3d pointValue)
    Point3d VertexAt(int value)

--------------------------------------------------------------------------------

[ENUM] LeaderDirectionType
Full Name: Autodesk.AutoCAD.DatabaseServices.LeaderDirectionType
Inherits: Enum

  Values:
    - UnknownLeader = 0
    - LeftLeader = 1
    - RightLeader = 2
    - TopLeader = 3
    - BottomLeader = 4

--------------------------------------------------------------------------------

[ENUM] LeaderType
Full Name: Autodesk.AutoCAD.DatabaseServices.LeaderType
Inherits: Enum

  Values:
    - InVisibleLeader = 0
    - StraightLeader = 1
    - SplineLeader = 2

--------------------------------------------------------------------------------

[CLASS] Light
Full Name: Autodesk.AutoCAD.DatabaseServices.Light
Inherits: Entity

  CONSTRUCTORS:
    new Light()

  PROPERTIES:
    LightAttenuation Attenuation { get; set; }
    AttenuationType AttenuationType { get; set; }
    Vector3d Direction { get; set; }
    double EndLimitOffset { get; set; }
    double FalloffAngle { get; }
    bool HasTarget { get; set; }
    double HotspotAngle { get; }
    double IlluminanceDistance { get; set; }
    double Intensity { get; set; }
    bool IsOn { get; set; }
    bool IsPlottable { get; set; }
    LampColorPreset LampColorPreset { get; set; }
    ColorRGB LampColorRGB { get; set; }
    double LampColorTemp { get; set; }
    LampColorType LampColorType { get; set; }
    Color LightColor { get; set; }
    DrawableType LightType { get; set; }
    int MapSize { get; set; }
    string Name { get; set; }
    double PhysicalIntensity { get; set; }
    PhysicalIntensityMethod PhysicalIntensityMethod { get; set; }
    Point3d Position { get; set; }
    ShadowParameters Shadow { get; set; }
    ShadowType ShadowType { get; set; }
    byte Softness { get; set; }
    double StartLimitOffset { get; set; }
    Point3d TargetLocation { get; set; }
    bool UseLimits { get; set; }
    string WebFile { get; set; }
    Vector3d WebRotation { get; set; }

  METHODS:
    Color ResultingColor()
    void SetHotspotAndFalloff(double hotspot, double falloff)

--------------------------------------------------------------------------------

[ENUM] LightingUnits
Full Name: Autodesk.AutoCAD.DatabaseServices.LightingUnits
Inherits: Enum

  Values:
    - LightingUnitsGeneric = 0
    - LightingUnitsAmerican = 1
    - LightingUnitsInternational = 2

--------------------------------------------------------------------------------

[CLASS] Line
Full Name: Autodesk.AutoCAD.DatabaseServices.Line
Inherits: Curve

  CONSTRUCTORS:
    new Line(Point3d pointer1, Point3d pointer2)
    new Line()

  PROPERTIES:
    double Angle { get; }
    Vector3d Delta { get; }
    Point3d EndPoint { get; set; }
    double Length { get; }
    Vector3d Normal { get; set; }
    Point3d StartPoint { get; set; }
    double Thickness { get; set; }

--------------------------------------------------------------------------------

[CLASS] LineAngularDimension2
Full Name: Autodesk.AutoCAD.DatabaseServices.LineAngularDimension2
Inherits: Dimension

  CONSTRUCTORS:
    new LineAngularDimension2(Point3d line1Start, Point3d line1End, Point3d line2Start, Point3d line2End, Point3d arcPoint, string dimensionText, ObjectId dimensionStyle)
    new LineAngularDimension2()

  PROPERTIES:
    Point3d ArcPoint { get; set; }
    Point3d XLine1End { get; set; }
    Point3d XLine1Start { get; set; }
    Point3d XLine2End { get; set; }
    Point3d XLine2Start { get; set; }

--------------------------------------------------------------------------------

[ENUM] LineSpacingStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.LineSpacingStyle
Inherits: Enum

  Values:
    - AtLeast = 1
    - Exactly = 2

--------------------------------------------------------------------------------

[CLASS] LinetypeTable
Full Name: Autodesk.AutoCAD.DatabaseServices.LinetypeTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] LinetypeTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord
Inherits: SymbolTableRecord

  CONSTRUCTORS:
    new LinetypeTableRecord()

  PROPERTIES:
    string AsciiDescription { get; set; }
    string Comments { get; set; }
    bool IsScaledToFit { get; set; }
    int NumDashes { get; set; }
    double PatternLength { get; set; }

  METHODS:
    double DashLengthAt(int index)
    void SetDashLengthAt(int index, double value)
    void SetShapeIsUcsOrientedAt(int index, bool isOriented)
    void SetShapeIsUprightAt(int index, bool isUpright)
    void SetShapeNumberAt(int index, int shapeNumber)
    void SetShapeOffsetAt(int index, Vector2d offset)
    void SetShapeRotationAt(int index, double rotation)
    void SetShapeScaleAt(int index, double scale)
    void SetShapeStyleAt(int index, ObjectId id)
    void SetTextAt(int index, string value)
    bool ShapeIsUcsOrientedAt(int index)
    bool ShapeIsUprightAt(int index)
    int ShapeNumberAt(int index)
    Vector2d ShapeOffsetAt(int index)
    double ShapeRotationAt(int index)
    double ShapeScaleAt(int index)
    ObjectId ShapeStyleAt(int index)
    string TextAt(int index)

--------------------------------------------------------------------------------

[ENUM] LineWeight
Full Name: Autodesk.AutoCAD.DatabaseServices.LineWeight
Inherits: Enum

  Values:
    - LineWeight000 = 0
    - LineWeight005 = 5
    - LineWeight009 = 9
    - LineWeight013 = 13
    - LineWeight015 = 15
    - LineWeight018 = 18
    - LineWeight020 = 20
    - LineWeight025 = 25
    - LineWeight030 = 30
    - LineWeight035 = 35
    - LineWeight040 = 40
    - LineWeight050 = 50
    - LineWeight053 = 53
    - LineWeight060 = 60
    - LineWeight070 = 70
    - LineWeight080 = 80
    - LineWeight090 = 90
    - LineWeight100 = 100
    - LineWeight106 = 106
    - LineWeight120 = 120
    - LineWeight140 = 140
    - LineWeight158 = 158
    - LineWeight200 = 200
    - LineWeight211 = 211
    - ByDIPs = -4
    - ByLineWeightDefault = -3
    - ByBlock = -2
    - ByLayer = -1

--------------------------------------------------------------------------------

[CLASS] LineWeightConverter
Full Name: Autodesk.AutoCAD.DatabaseServices.LineWeightConverter
Inherits: TypeConverter

  CONSTRUCTORS:
    new LineWeightConverter()

  METHODS:
    bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LinkedData
Full Name: Autodesk.AutoCAD.DatabaseServices.LinkedData
Inherits: DBObject

  PROPERTIES:
    bool IsEmpty { get; }
    string Name { get; set; }

  METHODS:
    void Clear()

--------------------------------------------------------------------------------

[CLASS] LinkedTableData
Full Name: Autodesk.AutoCAD.DatabaseServices.LinkedTableData
Inherits: LinkedData

  CONSTRUCTORS:
    new LinkedTableData()

  PROPERTIES:
    int NumberOfColumns { get; }
    int NumberOfRows { get; }

  METHODS:
    int AppendColumn(int columnsNumber)
    int AppendRow(int rowsNumber)
    DataType DataType(int row, int column)
    void DeleteColumn(int index, int columnsNumberToDelete)
    void DeleteContent(int row, int column)
    void DeleteRow(int index, int rowsNumberToDelete)
    string GetBlockAttributeValue(int row, int column, ObjectId attributeDefinitionId)
    ObjectId GetBlockTableRecordId(int row, int column)
    CellStates GetCellState(int row, int column)
    string GetColumnName(int index)
    CellContentTypes GetContentTypes(int row, int column)
    object GetCustomData(int row, int column, string key)
    string GetDataFormat(int row, int column)
    ObjectIdCollection GetDataLink(CellRange range)
    ObjectIdCollection GetDataLink()
    ObjectId GetDataLink(int row, int column)
    TableEnumerator GetEnumerator(TableEnumeratorOption option)
    TableEnumerator GetEnumerator()
    ObjectId GetFieldId(int row, int column)
    string GetToolTip(int row, int column)
    object GetValue(int row, int column, int content, FormatOption formatOption)
    object GetValue(int row, int column)
    int InsertColumn(int index, int columnsNumber)
    int InsertRow(int index, int rowsNumber)
    bool IsContentEditable(int row, int column)
    bool IsLinked(int row, int column)
    void SetBlockAttributeValue(int row, int column, ObjectId attributeDefinitionId, string value)
    void SetBlockTableRecordId(int row, int column, ObjectId value)
    void SetCellState(int row, int column, CellStates value)
    void SetColumnName(int index, string name)
    void SetCustomData(int row, int column, string key, object value)
    void SetDataFormat(int row, int column, string format)
    void SetDataLink(CellRange range, ObjectId dataLinkId, bool bUpdate)
    void SetDataLink(int row, int column, ObjectId dataLinkId, bool bUpdate)
    void SetDataType(int row, int column, DataType dataType, UnitType unitType)
    void SetFieldId(int row, int column, ObjectId value)
    void SetSize(int numRows, int numCols)
    void SetToolTip(int row, int column, string value)
    void SetValue(int row, int column, int content, object value, ParseOption parseOption)
    void SetValue(int row, int column, object value)
    UnitType UnitType(int row, int column)
    void UpdateDataLink(UpdateDirection dir, UpdateOption option)
    void UpdateDataLink(int row, int column, UpdateDirection dir, UpdateOption option)

--------------------------------------------------------------------------------

[CLASS] LoftedSurface
Full Name: Autodesk.AutoCAD.DatabaseServices.LoftedSurface
Inherits: Surface

  CONSTRUCTORS:
    new LoftedSurface()

  PROPERTIES:
    bool Closed { get; set; }
    LoftProfile[] CrossSectionProfiles { get; }
    Entity[] CrossSections { get; }
    int EndCrossSectionContinuity { get; set; }
    double EndCrossSectionMagnitude { get; set; }
    int EndGuideCurveContinuity { get; set; }
    double EndGuideCurveMagnitude { get; set; }
    Entity[] GuideCurves { get; }
    LoftProfile[] GuideProfiles { get; }
    LoftOptions LoftOptions { get; set; }
    int NumberOfCrossSections { get; }
    int NumberOfGuideCurves { get; }
    Entity PathEntity { get; }
    LoftProfile PathProfile { get; }
    int StartCrossSectionContinuity { get; set; }
    double StartCrossSectionMagnitude { get; set; }
    int StartGuideCurveContinuity { get; set; }
    double StartGuideCurveMagnitude { get; set; }
    LoftSurfaceType SurfaceType { get; }

  METHODS:
    void CreateLoftedSurface(Entity[] crossSections, Entity[] guideCurves, Entity pathCurve, LoftOptions loftOptions)
    LoftProfile GetCrossSectionProfile(int idx)
    LoftProfile GetGuideProfile(int idx)

--------------------------------------------------------------------------------

[CLASS] LoftOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.LoftOptions
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new LoftOptions(LoftOptions opts)
    new LoftOptions()

  PROPERTIES:
    bool AlignDirection { get; }
    bool ArcLengthParam { get; }
    bool Closed { get; }
    double DraftEnd { get; }
    double DraftEndMag { get; }
    double DraftStart { get; }
    double DraftStartMag { get; }
    LoftOptionsNormalOption NormalOption { get; }
    bool NoTwist { get; }
    bool Ruled { get; }
    bool Simplify { get; }
    bool VirtualGuide { get; }

  METHODS:
    LoftOptionsCheckCurvesOut CheckCrossSectionCurves(Entity[] crossSectionCurves, bool displayErrorMessages)
    void CheckGuideCurves(Entity[] guideCurves, bool displayErrorMessages)
    LoftOptionsCheckCurvesOut CheckLoftCurves(Entity[] crossSectionCurves, Entity[] guideCurves, Entity pPathCurve, bool displayErrorMessages)
    void CheckPathCurve(Entity pathCurve, bool displayErrorMessages)
    object Clone()

--------------------------------------------------------------------------------

[CLASS] LoftOptionsBuilder
Full Name: Autodesk.AutoCAD.DatabaseServices.LoftOptionsBuilder

  CONSTRUCTORS:
    new LoftOptionsBuilder(LoftOptions value)
    new LoftOptionsBuilder()

  PROPERTIES:
    bool AlignDirection { get; set; }
    bool ArcLengthParam { get; set; }
    bool Closed { get; set; }
    double DraftEnd { get; set; }
    double DraftEndMag { get; set; }
    double DraftStart { get; set; }
    double DraftStartMag { get; set; }
    LoftOptionsNormalOption NormalOption { get; set; }
    bool NoTwist { get; set; }
    bool Ruled { get; set; }
    bool Simplify { get; set; }
    bool VirtualGuide { get; set; }

  METHODS:
    void SetOptionsFromSysvars()
    LoftOptions ToLoftOptions()

--------------------------------------------------------------------------------

[CLASS] LoftOptionsCheckCurvesOut
Full Name: Autodesk.AutoCAD.DatabaseServices.LoftOptionsCheckCurvesOut

  CONSTRUCTORS:
    new LoftOptionsCheckCurvesOut()

  PROPERTIES:
    bool AllClosed { get; }
    bool AllOpen { get; }
    bool AllPlanar { get; }

--------------------------------------------------------------------------------

[ENUM] LoftOptionsNormalOption
Full Name: Autodesk.AutoCAD.DatabaseServices.LoftOptionsNormalOption
Inherits: Enum

  Values:
    - NoNormal = 0
    - FirstNormal = 1
    - LastNormal = 2
    - EndsNormal = 3
    - AllNormal = 4
    - UseDraftAngles = 5

--------------------------------------------------------------------------------

[CLASS] LoftProfile
Full Name: Autodesk.AutoCAD.DatabaseServices.LoftProfile
Inherits: Profile3d

  CONSTRUCTORS:
    new LoftProfile(LoftProfile LoftProfile)
    new LoftProfile(Point3d pnt)
    new LoftProfile(Entity entity)
    new LoftProfile(PathRef pathRef)
    new LoftProfile()

  PROPERTIES:
    short Continuity { get; set; }
    double Magnitude { get; set; }

  METHODS:
    void CopyFrom(Profile3d source)
    void CopyFrom(LoftProfile source)

--------------------------------------------------------------------------------

[CLASS] LongTransaction
Full Name: Autodesk.AutoCAD.DatabaseServices.LongTransaction
Inherits: DBObject

  CONSTRUCTORS:
    new LongTransaction()
    new LongTransaction(IntPtr unmanagedPointer, bool autoDelete)

  PROPERTIES:
    IdMapping ActiveIdMap { get; }
    ObjectId DestinationBlock { get; }
    bool DisallowDrawOrder { get; }
    string LongTransactionName { get; }
    ObjectId OriginBlock { get; }
    LongTransactionType Type { get; }

  METHODS:
    void AddToWorkSet(ObjectId id)
    ObjectId OriginObject(ObjectId id)
    void RegenWorkSetWithDrawOrder()
    void RemoveFromWorkSet(ObjectId id)
    void SyncWorkSet()
    bool WorkSetHas(ObjectId id, bool includeErased)

--------------------------------------------------------------------------------

[ENUM] LongTransactionType
Full Name: Autodesk.AutoCAD.DatabaseServices.LongTransactionType
Inherits: Enum

  Values:
    - SameDb = 0
    - XRefDb = 1
    - UnrelatedDb = 2

--------------------------------------------------------------------------------

