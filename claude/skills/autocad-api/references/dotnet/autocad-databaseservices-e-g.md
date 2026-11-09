# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[CLASS] EdgeRef
Full Name: Autodesk.AutoCAD.DatabaseServices.EdgeRef
Inherits: SubentRef

  CONSTRUCTORS:
    new EdgeRef(Curve3d curve)
    new EdgeRef(Entity entity)
    new EdgeRef(FullSubentityPath path)
    new EdgeRef(CompoundObjectId compoundObjectId, SubentityId edgeSubentId, SubentityId faceSubentId, Curve3d curve)
    new EdgeRef(CompoundObjectId compoundObjectId, SubentityId edgeSubentId, SubentityId faceSubentId)
    new EdgeRef(CompoundObjectId compoundObjectId, SubentityId edgeSubentId)
    new EdgeRef(CompoundObjectId compoundObjectId)
    new EdgeRef()

  PROPERTIES:
    Curve3d Curve { get; }
    SubentityId FaceSubentity { get; set; }

--------------------------------------------------------------------------------

[CLASS] Ellipse
Full Name: Autodesk.AutoCAD.DatabaseServices.Ellipse
Inherits: Curve

  CONSTRUCTORS:
    new Ellipse(Point3d center, Vector3d unitNormal, Vector3d majorAxis, double radiusRatio, double startAngle, double endAngle)
    new Ellipse()

  PROPERTIES:
    Point3d Center { get; set; }
    double EndAngle { get; set; }
    double EndParam { get; set; }
    bool IsNull { get; }
    Vector3d MajorAxis { get; }
    double MajorRadius { get; }
    Vector3d MinorAxis { get; }
    double MinorRadius { get; }
    Vector3d Normal { get; }
    double RadiusRatio { get; set; }
    double StartAngle { get; set; }
    double StartParam { get; set; }

  METHODS:
    double GetAngleAtParameter(double value)
    double GetParameterAtAngle(double angle)
    void Set(Point3d center, Vector3d unitNormal, Vector3d majorAxis, double radiusRatio, double startAngle, double endAngle)

--------------------------------------------------------------------------------

[ENUM] EndCap
Full Name: Autodesk.AutoCAD.DatabaseServices.EndCap
Inherits: Enum

  Values:
    - None = 0
    - Round = 1
    - Angle = 2
    - Square = 3

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Entity
Full Name: Autodesk.AutoCAD.DatabaseServices.Entity
Inherits: DBObject

  PROPERTIES:
    ObjectId BlockId { get; }
    string BlockName { get; }
    bool CastShadows { get; set; }
    bool CloneMeForDragging { get; }
    CollisionType CollisionType { get; }
    Color Color { get; set; }
    int ColorIndex { get; set; }
    Matrix3d CompoundObjectTransform { get; }
    Matrix3d Ecs { get; }
    ObjectId EdgeStyleId { get; set; }
    EntityColor EntityColor { get; }
    ObjectId FaceStyleId { get; set; }
    bool ForceAnnoAllVisible { get; set; }
    Extents3d GeometricExtents { get; }
    HyperLinkCollection Hyperlinks { get; }
    bool IsPlanar { get; }
    string Layer { get; set; }
    ObjectId LayerId { get; set; }
    string Linetype { get; set; }
    ObjectId LinetypeId { get; set; }
    double LinetypeScale { get; set; }
    LineWeight LineWeight { get; set; }
    string Material { get; set; }
    ObjectId MaterialId { get; set; }
    Mapper MaterialMapper { get; set; }
    string PlotStyleName { get; set; }
    PlotStyleDescriptor PlotStyleNameId { get; set; }
    bool ReceiveShadows { get; set; }
    Transparency Transparency { get; set; }
    bool Visible { get; set; }
    ObjectId VisualStyleId { get; set; }

  METHODS:
    void AddSubentityPaths(FullSubentityPath[] subPaths)
    void BoundingBoxIntersectWith(Entity entityPointer, Intersect intersectType, Point3dCollection points, IntPtr thisGraphicSystemMarker, IntPtr otherGraphicSystemMarker)
    void BoundingBoxIntersectWith(Entity entityPointer, Intersect intersectType, Plane projectionPlane, Point3dCollection points, IntPtr thisGraphicSystemMarker, IntPtr otherGraphicSystemMarker)
    void BoundingBoxIntersectWith(Entity entityPointer, Intersect intersectType, Point3dCollection points, long thisGraphicSystemMarker, long otherGraphicSystemMarker)
    void BoundingBoxIntersectWith(Entity entityPointer, Intersect intersectType, Plane projectionPlane, Point3dCollection points, long thisGraphicSystemMarker, long otherGraphicSystemMarker)
    void DeleteSubentityPaths(FullSubentityPath[] subPaths)
    void Draw()
    void Explode(DBObjectCollection entitySet)
    IntPtrCollection GetGraphicsMarkersAtSubentityPathIntPtr(FullSubentityPath subPath)
    bool GetGripPoints(GripDataCollection grips, double curViewUnitSize, int gripSize, Vector3d curViewDir, GetGripPointsFlags bitFlags)
    void GetGripPoints(Point3dCollection gripPoints, IntegerCollection snapModes, IntegerCollection geometryIds)
    bool GetGripPointsAtSubentityPath(FullSubentityPath subPath, GripDataCollection grips, double curViewUnitSize, int gripSize, Vector3d curViewDir, GetGripPointsFlags bitFlags)
    void GetObjectSnapPoints(ObjectSnapModes snapMode, int gsSelectionMark, Point3d pickPoint, Point3d lastPoint, Matrix3d viewTransform, Point3dCollection snapPoints, IntegerCollection geometryIds, Matrix3d insertionMat)
    void GetObjectSnapPoints(ObjectSnapModes snapMode, int gsSelectionMark, Point3d pickPoint, Point3d lastPoint, Matrix3d viewTransform, Point3dCollection snapPoints, IntegerCollection geometryIds)
    Plane GetPlane()
    void GetStretchPoints(Point3dCollection stretchPoints)
    Entity GetSubentity(FullSubentityPath id)
    Extents3d GetSubentityGeometricExtents(FullSubentityPath subPath)
    FullSubentityPath[] GetSubentityPathsAtGraphicsMarker(SubentityType type, IntPtr gsMark, Point3d pickPoint, Matrix3d viewTransform, ObjectId[] entityAndInsertStack)
    FullSubentityPath[] GetSubentityPathsAtGraphicsMarker(SubentityType type, long gsMark, Point3d pickPoint, Matrix3d viewTransform, int numInserts, ObjectId[] entityAndInsertStack)
    Entity GetTransformedCopy(Matrix3d transform)
    void Highlight(FullSubentityPath subId, bool highlightAll)
    void Highlight()
    HighlightStyle HighlightState(FullSubentityPath subId)
    void IntersectWith(Entity entityPointer, Intersect intersectType, Plane projectionPlane, Point3dCollection points, IntPtr thisGraphicSystemMarker, IntPtr otherGraphicSystemMarker)
    void IntersectWith(Entity entityPointer, Intersect intersectType, Point3dCollection points, IntPtr thisGraphicSystemMarker, IntPtr otherGraphicSystemMarker)
    void IntersectWith(Entity entityPointer, Intersect intersectType, Plane projectionPlane, Point3dCollection points, long thisGraphicSystemMarker, long otherGraphicSystemMarker)
    void IntersectWith(Entity entityPointer, Intersect intersectType, Point3dCollection points, long thisGraphicSystemMarker, long otherGraphicSystemMarker)
    bool IsContentSnappable()
    IntegerCollection JoinEntities(Entity[] otherEntities)
    void JoinEntity(Entity secondaryEntity)
    void List()
    void MoveGripPointsAt(GripDataCollection grips, Vector3d offset, MoveGripPointsFlags bitFlags)
    void MoveGripPointsAt(IntegerCollection indices, Vector3d offset)
    void MoveGripPointsAtSubentityPaths(FullSubentityPath[] subPaths, IntPtr[] appData, Vector3d offset, MoveGripPointsFlags bitFlags)
    void MoveStretchPointsAt(IntegerCollection indices, Vector3d offset)
    void PopHighlight(FullSubentityPath subId)
    void PushHighlight(FullSubentityPath subId, HighlightStyle highlightStyle)
    void RecordGraphicsModified(bool setModified)
    void SaveAs(WorldDraw mode, SaveType st)
    void SetDatabaseDefaults(Database sourceDatabase)
    void SetDatabaseDefaults()
    void SetDragStatus(DragStatus status)
    void SetGripStatus(GripStatus status)
    void SetLayerId(ObjectId newValue, bool allowHidden)
    void SetPropertiesFrom(Entity entityPointer)
    void SetSubentityGripStatus(GripStatus status, FullSubentityPath subentity)
    void TransformBy(Matrix3d transform)
    void TransformSubentityPathsBy(FullSubentityPath[] subPaths, Matrix3d transform)
    void Unhighlight(FullSubentityPath subId, bool highlightAll)
    void Unhighlight()

--------------------------------------------------------------------------------

[CLASS] EntityAlignmentEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.EntityAlignmentEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new EntityAlignmentEventArgs(Entity Entity, Point3d PickPoint, Vector3d Normal, Point3d ClosestPoint, Vector3d Direction, FullSubentityPath[] Path)

  PROPERTIES:
    Point3d ClosestPoint { get; set; }
    Vector3d Direction { get; set; }
    Entity Entity { get; }
    Vector3d Normal { get; }
    Point3d PickPoint { get; }

  METHODS:
    FullSubentityPath[] GetPickedEntities()

--------------------------------------------------------------------------------

[CLASS] EntityAlignmentEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.EntityAlignmentEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EntityAlignmentEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EntityAlignmentEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EntityAlignmentEventArgs e)

--------------------------------------------------------------------------------

[ENUM] EntityVisualStyleType
Full Name: Autodesk.AutoCAD.DatabaseServices.EntityVisualStyleType
Inherits: Enum

  Values:
    - FullVisualStyle = 0
    - FaceVisualStyle = 1
    - EdgeVisualStyle = 2

--------------------------------------------------------------------------------

[CLASS] EnumConverter
Full Name: Autodesk.AutoCAD.DatabaseServices.EnumConverter
Inherits: EnumConverter

  CONSTRUCTORS:
    new EnumConverter(Type type)

  METHODS:
    bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)

--------------------------------------------------------------------------------

[CLASS] EqualCurvatureConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.EqualCurvatureConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new EqualCurvatureConstraint()

--------------------------------------------------------------------------------

[CLASS] EqualDistanceConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.EqualDistanceConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new EqualDistanceConstraint()

--------------------------------------------------------------------------------

[CLASS] EqualHelpParameterConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.EqualHelpParameterConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new EqualHelpParameterConstraint()

  METHODS:
    void GetEqualHelpParameters(ref HelpParameter helpParameter1, ref HelpParameter helpParameter2)

--------------------------------------------------------------------------------

[CLASS] EqualLengthConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.EqualLengthConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new EqualLengthConstraint()

--------------------------------------------------------------------------------

[CLASS] EqualRadiusConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.EqualRadiusConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new EqualRadiusConstraint()

--------------------------------------------------------------------------------

[ENUM] EraseFlags
Full Name: Autodesk.AutoCAD.DatabaseServices.EraseFlags
Inherits: Enum

  Values:
    - ZeroLengthCurve = 1
    - EmptyText = 2
    - AllEmptyObj = -1

--------------------------------------------------------------------------------

[CLASS] ExplicitConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.ExplicitConstraint
Inherits: GeometricalConstraint

  PROPERTIES:
    ObjectId DimDependencyId { get; set; }
    double MeasuredValue { get; }
    ObjectId ValueDependencyId { get; }

--------------------------------------------------------------------------------

[STRUCT] Extents2d
Full Name: Autodesk.AutoCAD.DatabaseServices.Extents2d
Implements: IFormattable

  CONSTRUCTORS:
    new Extents2d(Point2d minimum, Point2d maximum)
    new Extents2d(double minX, double minY, double maxX, double maxY)

  PROPERTIES:
    Point2d MaxPoint { get; }
    Point2d MinPoint { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(Extents2d a)
    bool IsEqualTo(Extents2d a, Tolerance tolerance)
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[STRUCT] Extents3d
Full Name: Autodesk.AutoCAD.DatabaseServices.Extents3d
Implements: IFormattable

  CONSTRUCTORS:
    new Extents3d(Point3d min, Point3d max)
    new Extents3d()

  PROPERTIES:
    Point3d MaxPoint { get; }
    Point3d MinPoint { get; }

  METHODS:
    void AddBlockExtents(BlockTableRecord pointerToBlockTableRecord)
    void AddExtents(Extents3d source)
    void AddPoint(Point3d pt)
    bool Equals(object obj)
    void ExpandBy(Vector3d vector)
    int GetHashCode()
    bool IsEqualTo(Extents3d a)
    bool IsEqualTo(Extents3d a, Tolerance tolerance)
    void Set(Point3d min, Point3d max)
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    void TransformBy(Matrix3d mat)

--------------------------------------------------------------------------------

[CLASS] ExtrudedSurface
Full Name: Autodesk.AutoCAD.DatabaseServices.ExtrudedSurface
Inherits: Surface

  CONSTRUCTORS:
    new ExtrudedSurface()

  PROPERTIES:
    double Height { get; set; }
    Entity SweepEntity { get; }
    SweepOptions SweepOptions { get; set; }
    Profile3d SweepProfile { get; }
    Vector3d SweepVec { get; set; }
    double TaperAngle { get; set; }

  METHODS:
    void CreateExtrudedSurface(Entity sweepEnt, Vector3d directionVec, SweepOptions sweepOptions)
    void SetExtrude(Vector3d sweepVec, SweepOptions sweepOptions)

--------------------------------------------------------------------------------

[CLASS] Face
Full Name: Autodesk.AutoCAD.DatabaseServices.Face
Inherits: Entity

  CONSTRUCTORS:
    new Face(Point3d pointer1, Point3d pointer2, Point3d pointer3, bool value1, bool value2, bool value3, bool value4)
    new Face(Point3d pointer1, Point3d pointer2, Point3d pointer3, Point3d pointer4, bool value1, bool value2, bool value3, bool value4)
    new Face()

  METHODS:
    Point3d GetVertexAt(short value)
    bool IsEdgeVisibleAt(short vertexIndex)
    void MakeEdgeInvisibleAt(short vertexIndex)
    void MakeEdgeVisibleAt(short vertexIndex)
    void SetVertexAt(short vertexIndex, Point3d vertexPosition)

--------------------------------------------------------------------------------

[CLASS] FaceRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.FaceRecord
Inherits: Vertex

  CONSTRUCTORS:
    new FaceRecord(short vertex0, short vertex1, short vertex2, short vertex3)
    new FaceRecord()

  METHODS:
    short GetVertexAt(short faceIndex)
    bool IsEdgeVisibleAt(short faceIndex)
    void MakeEdgeInvisibleAt(short faceIndex)
    void MakeEdgeVisibleAt(short faceIndex)
    void SetVertexAt(short faceIndex, short vertexIndex)

--------------------------------------------------------------------------------

[CLASS] FaceRef
Full Name: Autodesk.AutoCAD.DatabaseServices.FaceRef
Inherits: SubentRef

  CONSTRUCTORS:
    new FaceRef(CompoundObjectId compoundObjectId, SubentityId subentId)
    new FaceRef(CompoundObjectId compoundObjectId)
    new FaceRef()

--------------------------------------------------------------------------------

[CLASS] FeatureControlFrame
Full Name: Autodesk.AutoCAD.DatabaseServices.FeatureControlFrame
Inherits: Entity

  CONSTRUCTORS:
    new FeatureControlFrame(string codes, Point3d insertionPoint, Vector3d normalVector, Vector3d x)
    new FeatureControlFrame()

  PROPERTIES:
    Color Dimclrd { get; set; }
    Color Dimclrt { get; set; }
    ObjectId DimensionStyle { get; set; }
    string DimensionStyleName { get; set; }
    double Dimgap { get; set; }
    double Dimscale { get; set; }
    ObjectId Dimtxsty { get; set; }
    double Dimtxt { get; set; }
    Vector3d Direction { get; }
    Point3d Location { get; set; }
    Vector3d Normal { get; }
    string Text { get; set; }
    ObjectId TextStyleId { get; set; }
    string TextStyleName { get; set; }

  METHODS:
    Point3dCollection GetBoundingPoints()
    Point3dCollection GetBoundingPolyline()
    DimStyleTableRecord GetDimstyleData()
    void SetDimstyleData(DimStyleTableRecord style)
    void SetOrientation(Vector3d norm, Vector3d dir)

--------------------------------------------------------------------------------

[CLASS] Field
Full Name: Autodesk.AutoCAD.DatabaseServices.Field
Inherits: DBObject

  CONSTRUCTORS:
    new Field(string fieldCode, bool textField)
    new Field(string fieldCode)
    new Field()

  PROPERTIES:
    DataType DataType { get; }
    FieldEvaluationOptions EvaluationOption { get; set; }
    FieldEvaluationStatusResult EvaluationStatus { get; }
    string EvaluatorId { get; set; }
    FieldFilingOptions FilingOption { get; set; }
    string Format { get; set; }
    HyperLink HyperLink { get; set; }
    bool IsTextField { get; }
    FieldState State { get; }
    object Value { get; }

  METHODS:
    void ConvertToTextField()
    void Evaluate()
    void Evaluate(int evaluationOptions, Database database)
    static bool FindField(string text, int iSearchFrom, ref int nStartPos, ref int nEndPos)
    Field[] GetChildren()
    ObjectId[] GetChildrenIds()
    object GetData(string key)
    string GetFieldCode(FieldCodeFlags flags)
    string GetFieldCode()
    FieldCodeWithChildren GetFieldCodeWithChildren(FieldCodeFlags flags)
    FieldCodeWithChildren GetFieldCodeWithChildren()
    string GetStringValue()
    void SetData(string key, object data, bool recursive)
    void SetData(string key, object data)
    void SetFieldCode(string fieldCode)
    void SetFieldCodeWithChildren(FieldCodeFlags flag, FieldCodeWithChildren fieldCode)
    void SetFieldCodeWithChildren(FieldCodeWithChildren fieldCode)

--------------------------------------------------------------------------------

[ENUM] FieldCodeFlags
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldCodeFlags
Inherits: Enum

  Values:
    - FieldCode = 1
    - EvaluatedText = 2
    - EvaluatedChildren = 4
    - ObjectReference = 8
    - AddMarkers = 16
    - EscapeBackslash = 32
    - StripOptions = 64
    - PreserveFields = 128
    - TextField = 256

--------------------------------------------------------------------------------

[CLASS] FieldCodeWithChildren
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldCodeWithChildren
Implements: IDisposable

  PROPERTIES:
    Field[] Children { get; }
    string FieldCode { get; set; }

  METHODS:
    void Add(int i, Field field)
    void Dispose()

--------------------------------------------------------------------------------

[ENUM] FieldEvaluationContext
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldEvaluationContext
Inherits: Enum

  Values:
    - Open = 1
    - Save = 2
    - Plot = 4
    - Etransmit = 8
    - Regen = 16
    - Demand = 32
    - Preview = 64

--------------------------------------------------------------------------------

[ENUM] FieldEvaluationOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldEvaluationOptions
Inherits: Enum

  Values:
    - Disable = 0
    - OnOpen = 1
    - OnSave = 2
    - OnPlot = 4
    - OnEtransmit = 8
    - OnRegen = 16
    - OnDemand = 32
    - Automatic = 63

--------------------------------------------------------------------------------

[STRUCT] FieldEvaluationResult
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldEvaluationResult

  PROPERTIES:
    int Evaluated { get; }
    int Found { get; }

--------------------------------------------------------------------------------

[ENUM] FieldEvaluationStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldEvaluationStatus
Inherits: Enum

  Values:
    - NotYetEvaluated = 1
    - Success = 2
    - EvaluatorNotFound = 4
    - SyntaxError = 8
    - InvalidCode = 16
    - InvalidContext = 32
    - OtherError = 64

--------------------------------------------------------------------------------

[STRUCT] FieldEvaluationStatusResult
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldEvaluationStatusResult

  CONSTRUCTORS:
    new FieldEvaluationStatusResult(FieldEvaluationStatus s, int e, string m)

  PROPERTIES:
    int ErrorCode { get; }
    string ErrorMessage { get; }
    FieldEvaluationStatus Status { get; }

--------------------------------------------------------------------------------

[ENUM] FieldFilingOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldFilingOptions
Inherits: Enum

  Values:
    - SkipFilingResult = 1

--------------------------------------------------------------------------------

[ENUM] FieldState
Full Name: Autodesk.AutoCAD.DatabaseServices.FieldState
Inherits: Enum

  Values:
    - Initialized = 1
    - Compiled = 2
    - Modified = 4
    - Evaluated = 8
    - HasCache = 16
    - HasFormattedString = 32

--------------------------------------------------------------------------------

[ENUM] FileOpenMode
Full Name: Autodesk.AutoCAD.DatabaseServices.FileOpenMode
Inherits: Enum

  Values:
    - OpenForReadAndReadShare = 1
    - OpenForReadAndWriteNoShare = 2
    - OpenForReadAndAllShare = 3
    - OpenTryForReadShare = 4

--------------------------------------------------------------------------------

[ENUM] FilerType
Full Name: Autodesk.AutoCAD.DatabaseServices.FilerType
Inherits: Enum

  Values:
    - FileFiler = 0
    - CopyFiler = 1
    - UndoFiler = 2
    - BagFiler = 3
    - IdTranslateFiler = 4
    - PageFiler = 5
    - DeepCloneFiler = 6
    - IdFiler = 7
    - PurgeFiler = 8
    - WblockCloneFiler = 9

--------------------------------------------------------------------------------

[ENUM] FilletTrimMode
Full Name: Autodesk.AutoCAD.DatabaseServices.FilletTrimMode
Inherits: Enum

  Values:
    - TrimNone = 0
    - TrimStart = 1
    - TrimSecond = 2
    - TrimBoth = 3

--------------------------------------------------------------------------------

[ENUM] FindFileHint
Full Name: Autodesk.AutoCAD.DatabaseServices.FindFileHint
Inherits: Enum

  Values:
    - Default = 0
    - FontFile = 1
    - CompiledShapeFile = 2
    - TrueTypeFontFile = 3
    - EmbeddedImageFile = 4
    - XRefDrawing = 5
    - PatternFile = 6
    - ArxApplication = 7
    - FontMapFile = 8
    - UnderlayFile = 9

--------------------------------------------------------------------------------

[STRUCT] FitData
Full Name: Autodesk.AutoCAD.DatabaseServices.FitData

  CONSTRUCTORS:
    new FitData()

  PROPERTIES:
    int Degree { get; set; }
    Vector3d EndTangent { get; set; }
    double FitTolerance { get; set; }
    KnotParameterizationEnum KnotParam { get; set; }
    bool Periodic { get; set; }
    Vector3d StartTangent { get; set; }
    bool TangentsExist { get; set; }

  METHODS:
    bool Equals(object obj)
    Point3dCollection GetFitPoints()
    int GetHashCode()
    bool IsEqualTo(FitData other)
    bool IsEqualTo(FitData other, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] FixedConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.FixedConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new FixedConstraint()

--------------------------------------------------------------------------------

[ENUM] FlowDirection
Full Name: Autodesk.AutoCAD.DatabaseServices.FlowDirection
Inherits: Enum

  Values:
    - NotSet = 0
    - LeftToRight = 1
    - RightToLeft = 2
    - TopToBottom = 3
    - BottomToTop = 4
    - ByStyle = 5

--------------------------------------------------------------------------------

[STRUCT] Font
Full Name: Autodesk.AutoCAD.DatabaseServices.Font

  CONSTRUCTORS:
    new Font(string name, bool isTrueType)

  PROPERTIES:
    bool IsTrueType { get; }
    string Name { get; }

--------------------------------------------------------------------------------

[ENUM] FormatOption
Full Name: Autodesk.AutoCAD.DatabaseServices.FormatOption
Inherits: Enum

  Values:
    - FormatOptionNone = 0
    - ForEditing = 1
    - ForExpression = 2
    - UseMaximumPrecision = 4
    - IgnoreMtextFormat = 8

--------------------------------------------------------------------------------

[CLASS] FormattedTableData
Full Name: Autodesk.AutoCAD.DatabaseServices.FormattedTableData
Inherits: LinkedTableData

  CONSTRUCTORS:
    new FormattedTableData()

  METHODS:
    CellAlignment GetAlignment(int row, int column)
    Color GetBackgroundColor(int row, int column)
    Color GetContentColor(int row, int column)
    Color GetGridColor(int row, int column, GridLineType gridLinetype)
    ObjectId GetGridLinetype(int row, int column, GridLineType gridLinetype)
    LineWeight GetGridLineWeight(int row, int column, GridLineType gridLinetype)
    Visibility GetGridVisibility(int row, int column, GridLineType gridLinetype)
    double GetMargin(int row, int column, CellMargins margin)
    CellRange GetMergeRange(int row, int column)
    GridProperties GetOverride(int row, int column, GridLineType gridLinetype)
    CellProperties GetOverride(int row, int column, int content)
    double GetRotation(int row, int column)
    double GetScale(int row, int column)
    double GetTextHeight(int row, int column)
    ObjectId GetTextStyle(int row, int column)
    bool IsFormatEditable(int row, int column)
    bool IsMerged(int row, int column)
    void Merge(CellRange range)
    void RemoveAllOverrides(int row, int column)
    void SetAlignment(int row, int column, CellAlignment value)
    void SetBackgroundColor(int row, int column, Color value)
    void SetContentColor(int row, int column, Color value)
    void SetGridColor(int row, int column, GridLineType gridLinetype, Color value)
    void SetGridLinetype(int row, int column, GridLineType gridLinetype, ObjectId value)
    void SetGridLineWeight(int row, int column, GridLineType gridLinetype, LineWeight value)
    void SetGridVisibility(int row, int column, GridLineType gridLinetype, Visibility value)
    void SetMargin(int row, int column, CellMargins margin, double value)
    void SetOverride(int row, int column, GridLineType gridLinetype, GridProperties gridProperty)
    void SetOverride(int row, int column, int content, CellProperties cellProperty)
    void SetRotation(int row, int column, double value)
    void SetScale(int row, int column, double value)
    void SetTextHeight(int row, int column, double value)
    void SetTextStyle(int row, int column, ObjectId value)
    void Unmerge(CellRange range)

--------------------------------------------------------------------------------

[ENUM] FrameSetting
Full Name: Autodesk.AutoCAD.DatabaseServices.FrameSetting
Inherits: Enum

  Values:
    - ImageFrameOff = 0
    - ImageFrameAbove = 1
    - ImageFrameBelow = 2
    - ImageFrameOnNoPlot = 3
    - ImageFrameInvalid = -1

--------------------------------------------------------------------------------

[STRUCT] FullDwgVersion
Full Name: Autodesk.AutoCAD.DatabaseServices.FullDwgVersion

  CONSTRUCTORS:
    new FullDwgVersion(DwgVersion majorVersion, MaintenanceReleaseVersion minorVersion)

  PROPERTIES:
    DwgVersion MajorVersion { get; }
    MaintenanceReleaseVersion MinorVersion { get; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()
    string ToString()

--------------------------------------------------------------------------------

[STRUCT] FullSubentityPath
Full Name: Autodesk.AutoCAD.DatabaseServices.FullSubentityPath

  CONSTRUCTORS:
    new FullSubentityPath(ObjectId[] ids, SubentityId index)

  PROPERTIES:
    bool IsNull { get; }
    static FullSubentityPath Null { get; }
    SubentityId SubentId { get; }

  METHODS:
    void CopyToUnmanagedObject(IntPtr unmanagedPointer)
    bool Equals(object obj)
    int GetHashCode()
    ObjectId[] GetObjectIds()

--------------------------------------------------------------------------------

[CLASS] G2SmoothConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.G2SmoothConstraint
Inherits: CompositeConstraint

  CONSTRUCTORS:
    new G2SmoothConstraint()

--------------------------------------------------------------------------------

[CLASS] GeoCoordinateCategory
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoCoordinateCategory
Inherits: DisposableWrapper

  PROPERTIES:
    string ID { get; }

  METHODS:
    static GeoCoordinateCategory[] CreateAll()
    GeoCoordinateSystem GetCoordinateAt(int index)
    int NumOfCoordinate()

--------------------------------------------------------------------------------

[CLASS] GeoCoordinateSystem
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoCoordinateSystem
Inherits: DisposableWrapper

  PROPERTIES:
    Extents2d CartesianExtents { get; }
    GeoDatum Datum { get; }
    string Description { get; }
    GeoEllipsoid Ellipsoid { get; }
    int EPSGcode { get; }
    Extents2d GeodeticExtents { get; }
    GeoCSUnit GeoUnit { get; }
    string ID { get; }
    Vector2d Offset { get; }
    GeoCSProjectionCode ProjectionCode { get; }
    GeoCSType Type { get; }
    UnitsValue Unit { get; }
    double UnitScale { get; }
    string WktRepresentation { get; }
    string XmlRepresentation { get; }

  METHODS:
    static GeoCoordinateSystem Create(string coordSysIdOrFullDef)
    static GeoCoordinateSystem[] CreateAll(GeoCoordinateCategory coordCategory)
    static GeoCoordinateSystem[] CreateAll(ValueType geoPt)
    static GeoCoordinateSystem[] CreateAll()
    GeoProjectionParam[] GetProjectionParamList(bool includeSpecialParams)

--------------------------------------------------------------------------------

[CLASS] GeoCoordinateTransformer
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoCoordinateTransformer
Inherits: DisposableWrapper

  PROPERTIES:
    string SourceCSid { get; }
    string TargetCSid { get; }

  METHODS:
    static GeoCoordinateTransformer Create(string source, string target)
    static Point3d TransformPoint(string source, string target, Point3d sourcePt)
    Point3d TransformPoint(Point3d sourcePt)
    static Point3dCollection TransformPoints(string source, string target, Point3dCollection sourcePts)
    Point3dCollection TransformPoints(Point3dCollection sourcePts)

--------------------------------------------------------------------------------

[ENUM] GeoCSProjectionCode
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoCSProjectionCode
Inherits: Enum

  Values:
    - Unknown = 0
    - LL = 1
    - Tm = 3
    - Alber = 4
    - OblqM = 5
    - Mrcat = 6
    - Azmed = 7
    - Lmtan = 8
    - Plycn = 9
    - Modpc = 10
    - Azmea = 11
    - Edcnc = 12
    - Miller = 13
    - Mstero = 15
    - Nzealand = 16
    - Sinus = 17
    - Ortho = 18
    - Gnomonic = 19
    - Edcyl = 20
    - Vdgrntn = 21
    - Cassini = 22
    - Robinson = 23
    - Bonne = 24
    - Eckert4 = 25
    - Eckert6 = 26
    - Mollweid = 27
    - Goode = 28
    - Neacyl = 29
    - Teacyl = 30
    - Bipolar = 31
    - Swiss = 32
    - Pstro = 33
    - Ostro = 34
    - Sstro = 35
    - Lm1sp = 36
    - Lm2sp = 37
    - Lmblg = 38
    - Wccsl = 39
    - Wccst = 40
    - Mndotl = 41
    - Mndott = 42
    - Sotrm = 43
    - Utm = 44
    - Trmrs = 45
    - GaussK = 46
    - Krovak = 47
    - MrcatK = 49
    - Krvk95 = 51
    - Pstrosl = 53
    - Trmeraf = 54
    - Nerth = 55
    - Obqcyl = 56
    - Sys34 = 57
    - Ostn97 = 58
    - Azede = 59
    - Ostn02 = 60
    - Sys34_99 = 61
    - Trmrkrg = 62
    - Winkl = 63
    - Nrthsrt = 64
    - Lmbrtaf = 65
    - Sys34_01 = 66
    - EdcylE = 67
    - PlateCarree = 68
    - PvMercator = 69
    - Hom1uv = 1281
    - Hom1xy = 1282
    - Hom2uv = 1283
    - Hom2xy = 1284
    - Rskew = 1285
    - Rskewc = 1286
    - Rskewo = 1287

--------------------------------------------------------------------------------

[ENUM] GeoCSType
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoCSType
Inherits: Enum

  Values:
    - Unknown = 0
    - Arbitrary = 1
    - Geographic = 2
    - Projected = 3

--------------------------------------------------------------------------------

[ENUM] GeoCSUnit
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoCSUnit
Inherits: Enum

  Values:
    - Unknown = 0
    - Meter = 1
    - Foot = 2
    - Inch = 3
    - IFoot = 4
    - ClarkeFoot = 5
    - IInch = 6
    - Centimeter = 7
    - Kilometer = 8
    - Yard = 9
    - SearsYard = 10
    - Mile = 11
    - IYard = 12
    - IMile = 13
    - Knot = 14
    - NautM = 15
    - Lat66 = 16
    - Lat83 = 17
    - Decimeter = 18
    - Millimeter = 19
    - Dekameter = 20
    - Hectometer = 21
    - GermanMeter = 22
    - CaGrid = 23
    - ClarkeChain = 24
    - GunterChain = 25
    - BenoitChain = 26
    - SearsChain = 27
    - ClarkeLink = 28
    - GunterLink = 29
    - BenoitLink = 30
    - SearsLink = 31
    - Rod = 32
    - Perch = 33
    - Pole = 34
    - Furlong = 35
    - Rood = 36
    - CapeRood = 37
    - Brealey = 38
    - SearsFoot = 39
    - GoldCoastFoot = 40
    - MicroInch = 41
    - IndianYard = 42
    - IndianFoot = 43
    - IndianFt37 = 44
    - IndianFt62 = 45
    - IndianFt75 = 46
    - IndianYd37 = 47
    - Decameter = 48
    - InternationalChain = 49
    - InternationalLink = 50
    - Degree = 1001
    - Grad = 1002
    - Grade = 1003
    - MapInfo = 1004
    - Mil = 1005
    - Minute = 1006
    - Radian = 1007
    - Second = 1008
    - Decisec = 1009
    - Centisec = 1010
    - Millisec = 1011

--------------------------------------------------------------------------------

[CLASS] GeoLocationData
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoLocationData
Inherits: DBObject

  CONSTRUCTORS:
    new GeoLocationData()

  PROPERTIES:
    ObjectId BlockTableRecordId { get; set; }
    double CoordinateProjectionRadius { get; set; }
    string CoordinateSystem { get; set; }
    Point3d DesignPoint { get; set; }
    bool DoSeaLevelCorrection { get; set; }
    string GeoRSSTag { get; set; }
    UnitsValue HorizontalUnits { get; set; }
    double HorizontalUnitsScale { get; set; }
    double NorthDirection { get; }
    Vector2d NorthDirectionVector { get; set; }
    int NumMeshPoints { get; }
    Point3d ReferencePoint { get; set; }
    ScaleEstimationMethod ScaleEstimationMethod { get; set; }
    double ScaleFactor { get; set; }
    double SeaLevelElevation { get; set; }
    TypeOfCoordinates TypeOfCoordinates { get; set; }
    Vector3d UpDirection { get; set; }
    UnitsValue VerticalUnits { get; set; }
    double VerticalUnitsScale { get; set; }

  METHODS:
    void AddMeshPointMap(int index, Point2d sourcePt, Point2d destPt)
    void EraseFromDb()
    MeshPointMap GetMeshPointMap(int index)
    MeshPointMaps GetMeshPointMaps()
    ObjectId PostToDb()
    void ResetMeshPointMaps()
    void SetMeshPointMaps(Point2dCollection sourcePts, Point2dCollection destPts)
    Point3d TransformFromLonLatAlt(Point3d geoPt)
    GeoDataLonLatAltInfo TransformToLonLatAlt(double x, double y, double z)
    Point3d TransformToLonLatAlt(Point3d dwgPt)
    void UpdateTransformationMatrix()

--------------------------------------------------------------------------------

[CLASS] GeomapImage
Full Name: Autodesk.AutoCAD.DatabaseServices.GeomapImage
Inherits: RasterImage

  CONSTRUCTORS:
    new GeomapImage()

  PROPERTIES:
    Point3d BottomLeftPoint { get; }
    byte Brightness { get; set; }
    byte Contrast { get; set; }
    byte Fade { get; set; }
    double Height { get; }
    Point3d ImageBottomLeftPoint { get; }
    double ImageHeight { get; }
    double ImageWidth { get; }
    bool IsOutOfDate { get; }
    uint LOD { get; }
    GeomapType MapType { get; set; }
    GeomapResolution Resolution { get; set; }
    double Width { get; }

  METHODS:
    Point3dCollection GetImageVertices()
    Point3dCollection GetVertices()
    Vector2d ImageSize()
    bool UpdateMapImage(bool Reset)

--------------------------------------------------------------------------------

[CLASS] GeometricalConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.GeometricalConstraint
Inherits: ConstraintGroupNode

  PROPERTIES:
    ConstrainedGeometry[] ConnectedGeometries { get; }
    HelpParameter[] ConnectedHelpParameters { get; }
    bool IsActive { get; }
    bool IsEnabled { get; }
    bool IsImplied { get; }
    bool IsInternal { get; }
    CompositeConstraint OwningCompositeConstraint { get; }

  METHODS:
    void Deactivate()
    HelpParameter GetConnectedHelpParameterFor(ConstrainedGeometry consGeom)
    void Reactivate()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] GeometryOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.GeometryOverrule
Inherits: Overrule

  METHODS:
    Extents3d GetGeomExtents(Entity entity)
    void IntersectWith(Entity entity, Entity ent, Intersect intType, Plane projPlane, Point3dCollection points, IntPtr thisGsMarker, IntPtr otherGsMarker)
    void IntersectWith(Entity entity, Entity ent, Intersect intType, Point3dCollection points, IntPtr thisGsMarker, IntPtr otherGsMarker)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] GeomRef
Full Name: Autodesk.AutoCAD.DatabaseServices.GeomRef
Inherits: RXObject

  PROPERTIES:
    bool IsEmpty { get; }
    bool IsValid { get; }

  METHODS:
    void Reset()

--------------------------------------------------------------------------------

[CLASS] GeoPositionMarker
Full Name: Autodesk.AutoCAD.DatabaseServices.GeoPositionMarker
Inherits: Entity

  CONSTRUCTORS:
    new GeoPositionMarker(Point3d pos, double radius, double landingGap)
    new GeoPositionMarker()

  PROPERTIES:
    bool EnableFrameText { get; set; }
    Point3d GeoPosition { get; set; }
    double LandingGap { get; set; }
    MText MText { get; set; }
    bool MTextVisible { get; set; }
    Vector3d Normal { get; }
    string Notes { get; set; }
    Point3d Position { get; set; }
    double Radius { get; set; }
    string Text { get; set; }
    TextAlignment TextAlignmentType { get; set; }
    ObjectId TextStyle { get; }

--------------------------------------------------------------------------------

[ENUM] GetGripPointsFlags
Full Name: Autodesk.AutoCAD.DatabaseServices.GetGripPointsFlags
Inherits: Enum

  Values:
    - GripPointsOnly = 1
    - CyclableGripsOnly = 2
    - DynamicDimensionMode = 4

--------------------------------------------------------------------------------

[CLASS] GradientBackground
Full Name: Autodesk.AutoCAD.DatabaseServices.GradientBackground
Inherits: Background

  CONSTRUCTORS:
    new GradientBackground()

  PROPERTIES:
    EntityColor ColorBottom { get; set; }
    EntityColor ColorMiddle { get; set; }
    EntityColor ColorTop { get; set; }
    double Height { get; set; }
    double Horizon { get; set; }
    double Rotation { get; set; }

--------------------------------------------------------------------------------

[STRUCT] GradientColor
Full Name: Autodesk.AutoCAD.DatabaseServices.GradientColor

  CONSTRUCTORS:
    new GradientColor(Color color, float value)

  METHODS:
    Color get_Color()
    float get_Value()

--------------------------------------------------------------------------------

[ENUM] GradientPatternType
Full Name: Autodesk.AutoCAD.DatabaseServices.GradientPatternType
Inherits: Enum

  Values:
    - PreDefinedGradient = 0
    - UserDefinedGradient = 1

--------------------------------------------------------------------------------

[CLASS] Graph
Full Name: Autodesk.AutoCAD.DatabaseServices.Graph
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Graph(GraphNode root)

  PROPERTIES:
    bool IsEmpty { get; }
    int NumNodes { get; }
    GraphNode RootNode { get; }

  METHODS:
    void AddEdge(GraphNode from, GraphNode toPointer)
    void AddNode(GraphNode nodeToAdd)
    void BreakCycleEdge(GraphNode from, GraphNode toPointer)
    void ClearAll(byte flags)
    static Graph Create(IntPtr unmanagedPointer, bool autoDelete)
    void DelNode(GraphNode nodeToDelete)
    bool FindCycles(GraphNode start)
    GraphNodeCollection GetOutgoing()
    GraphNode Node(int index)
    void Reset()
    void SetNodeGrowthRate(int rate)

--------------------------------------------------------------------------------

[ENUM] GraphicsMetafileType
Full Name: Autodesk.AutoCAD.DatabaseServices.GraphicsMetafileType
Inherits: Enum

  Values:
    - NoMetafile = 0
    - BoundingBox = 1
    - FullGraphics = 2

--------------------------------------------------------------------------------

[CLASS] GraphNode
Full Name: Autodesk.AutoCAD.DatabaseServices.GraphNode
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new GraphNode()

  PROPERTIES:
    IntPtr Data { get; set; }
    bool IsCycleNode { get; }
    GraphNode NextCycleNode { get; }
    int NumCycleIn { get; }
    int NumCycleOut { get; }
    int NumIn { get; }
    int NumOut { get; }
    Graph Owner { get; }

  METHODS:
    void AddRefTo(GraphNode outGoingNode)
    void Clear(byte flags)
    static GraphNode Create(IntPtr unmanagedPointer, bool autoDelete)
    GraphNode CycleIn(int index)
    GraphNode CycleOut(int index)
    void DisconnectAll()
    GraphNode In(int index)
    bool IsMarkedAs(int flag)
    void MarkAs(byte flags)
    void MarkTree(byte flags, GraphNodeCollection appendedTo)
    GraphNode Out(int index)
    void RemoveRefTo(GraphNode nodeToRemoveReference)
    void SetEdgeGrowthRate(int outEdgeRate, int inEdgeRate)

--------------------------------------------------------------------------------

[CLASS] GraphNodeCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.GraphNodeCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new GraphNodeCollection()

  PROPERTIES:
    int Count { get; }
    GraphNode Item { get; set; }

  METHODS:
    int Add(GraphNode value)
    void Clear()
    bool Contains(GraphNode value)
    void CopyTo(GraphNode[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(GraphNode value)
    void Insert(int index, GraphNode value)
    void Remove(GraphNode value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ENUM] GridLineStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.GridLineStyle
Inherits: Enum

  Values:
    - Single = 1
    - Double = 2

--------------------------------------------------------------------------------

[ENUM] GridLineType
Full Name: Autodesk.AutoCAD.DatabaseServices.GridLineType
Inherits: Enum

  Values:
    - InvalidGridLine = 0
    - HorizontalTop = 1
    - HorizontalInside = 2
    - HorizontalBottom = 4
    - HorizontalGridLines = 7
    - VerticalLeft = 8
    - VerticalInside = 16
    - InnerGridLines = 18
    - VerticalRight = 32
    - OuterGridLines = 45
    - VerticalGridLines = 56
    - AllGridLines = 63

--------------------------------------------------------------------------------

[ENUM] GridProperties
Full Name: Autodesk.AutoCAD.DatabaseServices.GridProperties
Inherits: Enum

  Values:
    - Invalid = 0
    - LineStyle = 1
    - LineWeight = 2
    - Linetype = 4
    - Color = 8
    - Visibility = 16
    - DoubleLineSpacing = 32

--------------------------------------------------------------------------------

[STRUCT] GridPropertyParameter
Full Name: Autodesk.AutoCAD.DatabaseServices.GridPropertyParameter

--------------------------------------------------------------------------------

[CLASS] GripData
Full Name: Autodesk.AutoCAD.DatabaseServices.GripData
Inherits: DisposableWrapper

  PROPERTIES:
    Nullable<Point3d> AlternateBasePoint { get; set; }
    bool DrawAtDragImageGripPoint { get; set; }
    bool ForcedPickOn { get; set; }
    bool GizmosEnabled { get; set; }
    Point3d GripPoint { get; set; }
    bool HotGripInvokesRightClick { get; set; }
    bool IsPerViewport { get; set; }
    bool ModeKeywordsDisabled { get; set; }
    bool RubberBandLineDisabled { get; set; }
    bool SkipWhenShared { get; set; }
    bool TriggerGrip { get; set; }

  METHODS:
    DynamicDimensionDataCollection GetHotGripDimensionData(ObjectId id, double dimScale)
    DynamicDimensionDataCollection GetHoverDimensionData(ObjectId id, double dimScale)
    string GetTooltip()
    void OnGripStatusChanged(ObjectId entityId, Status newStatus)
    ReturnValue OnHotGrip(ObjectId entityId, Context contextFlags)
    ReturnValue OnHover(ObjectId entityId, Context contextFlags)
    IEnumerable<IMenuItem> OnRightClick(GripDataCollection hotGrips, ObjectIdCollection entities)
    bool ViewportDraw(ViewportDraw worldDraw, ObjectId entityId, DrawType type, Nullable<Point3d> imageGripPoint, int gripSizeInPixels)
    bool WorldDraw(WorldDraw worldDraw, ObjectId entityId, DrawType type, Nullable<Point3d> imageGripPoint, double dGripSize)

--------------------------------------------------------------------------------

[CLASS] GripDataCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.GripDataCollection
Inherits: DisposableWrapper
Implements: ICollection<GripData>, IEnumerable<GripData>, IEnumerable

  CONSTRUCTORS:
    new GripDataCollection()

  PROPERTIES:
    int Count { get; }
    bool IsReadOnly { get; }
    GripData Item { get; }

  METHODS:
    void Add(GripData gripData)
    void Clear()
    bool Contains(GripData gripData)
    void CopyTo(GripData[] A_0, int A_1)
    IEnumerator<GripData> GetEnumerator()
    bool Remove(GripData gripData)

--------------------------------------------------------------------------------

[CLASS] GripMode
Full Name: Autodesk.AutoCAD.DatabaseServices.GripMode
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new GripMode()

  PROPERTIES:
    ActionType Action { get; set; }
    string CLIDisplayString { get; set; }
    string CLIKeywordList { get; set; }
    string CLIPromptString { get; set; }
    string CommandString { get; set; }
    CursorType Cursor { get; set; }
    string DisplayString { get; set; }
    uint ModeId { get; set; }
    string ToolTip { get; set; }

--------------------------------------------------------------------------------

[CLASS] GripModeCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.GripModeCollection
Inherits: DisposableWrapper
Implements: ICollection<GripMode>, IEnumerable<GripMode>, IEnumerable

  CONSTRUCTORS:
    new GripModeCollection()

  PROPERTIES:
    int Count { get; }
    bool IsReadOnly { get; }
    GripMode Item { get; }

  METHODS:
    void Add(GripMode gripMode)
    void Clear()
    bool Contains(GripMode gripMode)
    void CopyTo(GripMode[] A_0, int A_1)
    IEnumerator<GripMode> GetEnumerator()
    bool Remove(GripMode gripMode)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] GripOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.GripOverrule
Inherits: Overrule

  METHODS:
    void GetGripPoints(Entity entity, GripDataCollection grips, double curViewUnitSize, int gripSize, Vector3d curViewDir, GetGripPointsFlags bitFlags)
    void GetGripPoints(Entity entity, Point3dCollection gripPoints, IntegerCollection snapModes, IntegerCollection geometryIds)
    void GetStretchPoints(Entity entity, Point3dCollection stretchPoints)
    void MoveGripPointsAt(Entity entity, GripDataCollection grips, Vector3d offset, MoveGripPointsFlags bitFlags)
    void MoveGripPointsAt(Entity entity, IntegerCollection indices, Vector3d offset)
    void MoveStretchPointsAt(Entity entity, IntegerCollection indices, Vector3d offset)
    void OnGripStatusChanged(Entity entity, GripStatus status)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

[ENUM] GripStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.GripStatus
Inherits: Enum

  Values:
    - GripsDone = 0
    - GripsToBeDeleted = 1
    - DimDataToBeDeleted = 2

--------------------------------------------------------------------------------

[CLASS] GroundPlaneBackground
Full Name: Autodesk.AutoCAD.DatabaseServices.GroundPlaneBackground
Inherits: Background

  CONSTRUCTORS:
    new GroundPlaneBackground()

  PROPERTIES:
    EntityColor ColorGroundPlaneFar { get; set; }
    EntityColor ColorGroundPlaneNear { get; set; }
    EntityColor ColorSkyHorizon { get; set; }
    EntityColor ColorSkyZenith { get; set; }
    EntityColor ColorUndergroundAzimuth { get; set; }
    EntityColor ColorUndergroundHorizon { get; set; }

--------------------------------------------------------------------------------

[CLASS] Group
Full Name: Autodesk.AutoCAD.DatabaseServices.Group
Inherits: DBObject

  CONSTRUCTORS:
    new Group(string description, bool selectable)
    new Group()

  PROPERTIES:
    string Description { get; set; }
    bool IsAnonymous { get; }
    bool IsNotAccessible { get; }
    string Name { get; set; }
    int NumEntities { get; }
    bool Selectable { get; set; }

  METHODS:
    void Append(ObjectIdCollection ids)
    void Append(ObjectId id)
    void Clear()
    ObjectId[] GetAllEntityIds()
    int GetIndex(ObjectId id)
    bool Has(Entity entity)
    void InsertAt(int index, ObjectIdCollection ids)
    void InsertAt(int index, ObjectId id)
    void Prepend(ObjectIdCollection ids)
    void Prepend(ObjectId id)
    void Remove(ObjectIdCollection ids)
    void Remove(ObjectId id)
    void RemoveAt(int index, ObjectIdCollection ids)
    void RemoveAt(int index)
    void Replace(ObjectId oldId, ObjectId newId)
    void Reverse()
    void SetAnonymous()
    void SetColor(Color color)
    void SetColorIndex(int color)
    void SetHighlight(bool value)
    void SetLayer(ObjectId value)
    void SetLayer(string value)
    void SetLinetype(ObjectId value)
    void SetLinetype(string value)
    void SetLinetypeScale(double value)
    void SetVisibility(bool value)
    void Transfer(int fromIndex, int toIndex, int numItems)

--------------------------------------------------------------------------------

[ENUM] GsMarkType
Full Name: Autodesk.AutoCAD.DatabaseServices.GsMarkType
Inherits: Enum

  Values:
    - None = 0
    - ArrowMark = 1
    - LeaderLineMark = 5001
    - DoglegMark = 10001
    - MTextMark = 15001
    - MTextUnderLineMark = 15002
    - ToleranceMark = 15003
    - BlockMark = 15004

--------------------------------------------------------------------------------

