# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[ENUM] PaperOrientationStates
Full Name: Autodesk.AutoCAD.DatabaseServices.PaperOrientationStates
Inherits: Enum

  Values:
    - True = 0
    - False = 1
    - NotApplicable = 2

--------------------------------------------------------------------------------

[CLASS] ParallelConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.ParallelConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new ParallelConstraint()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ParameterValueSet
Full Name: Autodesk.AutoCAD.DatabaseServices.ParameterValueSet
Inherits: DisposableWrapper

  PROPERTIES:
    DataType DataType { get; }

  METHODS:
    void CopyFrom(RXObject obj)
    ResultBuffer GetClosestLegalValue(ResultBuffer value)
    bool ValueIsLegal(ResultBuffer value)

--------------------------------------------------------------------------------

[CLASS] ParameterValueSetIncrement
Full Name: Autodesk.AutoCAD.DatabaseServices.ParameterValueSetIncrement
Inherits: ParameterValueSetMinMax

  CONSTRUCTORS:
    new ParameterValueSetIncrement(ParameterValueSetIncrement other)
    new ParameterValueSetIncrement()

  PROPERTIES:
    double Increment { get; set; }

--------------------------------------------------------------------------------

[CLASS] ParameterValueSetList
Full Name: Autodesk.AutoCAD.DatabaseServices.ParameterValueSetList
Inherits: ParameterValueSet

  CONSTRUCTORS:
    new ParameterValueSetList(ParameterValueSetList other)
    new ParameterValueSetList()

  PROPERTIES:
    ResultBuffer[] Values { get; set; }

--------------------------------------------------------------------------------

[CLASS] ParameterValueSetMinMax
Full Name: Autodesk.AutoCAD.DatabaseServices.ParameterValueSetMinMax
Inherits: ParameterValueSet

  CONSTRUCTORS:
    new ParameterValueSetMinMax(ParameterValueSetMinMax other)
    new ParameterValueSetMinMax()

  PROPERTIES:
    double Maximum { get; set; }
    double Minimum { get; set; }

--------------------------------------------------------------------------------

[ENUM] ParseOption
Full Name: Autodesk.AutoCAD.DatabaseServices.ParseOption
Inherits: Enum

  Values:
    - ParseOptionNone = 0
    - SetDefaultFormat = 1
    - PreserveMtextFormat = 2

--------------------------------------------------------------------------------

[ENUM] PasswordOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.PasswordOptions
Inherits: Enum

  Values:
    - Default = 0
    - UpperCase = 1
    - IsExternalReference = 2

--------------------------------------------------------------------------------

[ENUM] PathOption
Full Name: Autodesk.AutoCAD.DatabaseServices.PathOption
Inherits: Enum

  Values:
    - NoPath = 1
    - RelativePath = 2
    - AbsolutePath = 3
    - PathAndFile = 4

--------------------------------------------------------------------------------

[CLASS] PathRef
Full Name: Autodesk.AutoCAD.DatabaseServices.PathRef
Inherits: GeomRef

  CONSTRUCTORS:
    new PathRef(FullSubentityPath[] edgeSubentPathArr, FullSubentityPath[] faceSubentPathArr)
    new PathRef(EdgeRef[] edges)
    new PathRef()

  PROPERTIES:
    EdgeRef[] EdgeRefs { get; set; }

  METHODS:
    Entity[] GetEntityArray(bool bConcatenate)
    bool IsEqualTo(Curve3d curve)
    bool IsReferencePath()

--------------------------------------------------------------------------------

[STRUCT] PatternDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.PatternDefinition

  CONSTRUCTORS:
    new PatternDefinition(double patternAngle, Point2d basePoint, Point2d offsetVector, DoubleCollection dashOffsets)

  PROPERTIES:
    double Angle { get; }
    double BaseX { get; }
    double BaseY { get; }
    double OffsetX { get; }
    double OffsetY { get; }

  METHODS:
    DoubleCollection GetDashes()

--------------------------------------------------------------------------------

[STRUCT] PCAdsName
Full Name: Autodesk.AutoCAD.DatabaseServices.PCAdsName

--------------------------------------------------------------------------------

[CLASS] PdfDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.PdfDefinition
Inherits: UnderlayDefinition

  CONSTRUCTORS:
    new PdfDefinition()

--------------------------------------------------------------------------------

[CLASS] PdfReference
Full Name: Autodesk.AutoCAD.DatabaseServices.PdfReference
Inherits: UnderlayReference

  CONSTRUCTORS:
    new PdfReference()

--------------------------------------------------------------------------------

[CLASS] PerpendicularConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.PerpendicularConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new PerpendicularConstraint()

--------------------------------------------------------------------------------

[ENUM] PhysicalIntensityMethod
Full Name: Autodesk.AutoCAD.DatabaseServices.PhysicalIntensityMethod
Inherits: Enum

  Values:
    - PeakIntensity = 0
    - Flux = 1
    - Illuminance = 2

--------------------------------------------------------------------------------

[CLASS] PlaceHolder
Full Name: Autodesk.AutoCAD.DatabaseServices.PlaceHolder
Inherits: DBObject

  CONSTRUCTORS:
    new PlaceHolder()

--------------------------------------------------------------------------------

[ENUM] Planarity
Full Name: Autodesk.AutoCAD.DatabaseServices.Planarity
Inherits: Enum

  Values:
    - NonPlanar = 0
    - Planar = 1
    - Linear = 2

--------------------------------------------------------------------------------

[CLASS] PlaneSurface
Full Name: Autodesk.AutoCAD.DatabaseServices.PlaneSurface
Inherits: Surface

  CONSTRUCTORS:
    new PlaneSurface()

  METHODS:
    void CreateFromRegion(Region region)

--------------------------------------------------------------------------------

[ENUM] PlotPaperUnit
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotPaperUnit
Inherits: Enum

  Values:
    - Inches = 0
    - Millimeters = 1
    - Pixels = 2

--------------------------------------------------------------------------------

[ENUM] PlotRotation
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotRotation
Inherits: Enum

  Values:
    - Degrees000 = 0
    - Degrees090 = 1
    - Degrees180 = 2
    - Degrees270 = 3

--------------------------------------------------------------------------------

[CLASS] PlotSettings
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotSettings
Inherits: DBObject

  CONSTRUCTORS:
    new PlotSettings(bool modelType)

  PROPERTIES:
    string CanonicalMediaName { get; }
    string CurrentStyleSheet { get; }
    CustomScale CustomPrintScale { get; }
    bool DrawViewportsFirst { get; set; }
    bool ModelType { get; }
    bool PlotAsRaster { get; }
    bool PlotCentered { get; }
    string PlotConfigurationName { get; }
    bool PlotHidden { get; set; }
    Point2d PlotOrigin { get; }
    Extents2d PlotPaperMargins { get; }
    Point2d PlotPaperSize { get; }
    PlotPaperUnit PlotPaperUnits { get; }
    bool PlotPlotStyles { get; set; }
    PlotRotation PlotRotation { get; }
    string PlotSettingsName { get; set; }
    bool PlotTransparency { get; set; }
    PlotType PlotType { get; }
    string PlotViewName { get; }
    bool PlotViewportBorders { get; set; }
    Extents2d PlotWindowArea { get; }
    bool PlotWireframe { get; }
    bool PrintLineweights { get; set; }
    bool ScaleLineweights { get; set; }
    PlotSettingsShadePlotType ShadePlot { get; set; }
    short ShadePlotCustomDpi { get; set; }
    ObjectId ShadePlotId { get; }
    ShadePlotResLevel ShadePlotResLevel { get; set; }
    bool ShowPlotStyles { get; set; }
    double StdScale { get; }
    StdScaleType StdScaleType { get; }
    bool UseStandardScale { get; }

  METHODS:
    void AddToPlotSettingsDictionary(Database toWhichDatabase)
    void SetShadePlot(PlotSettingsShadePlotType type, ObjectId shadePlotId)

--------------------------------------------------------------------------------

[ENUM] PlotSettingsShadePlotType
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotSettingsShadePlotType
Inherits: Enum

  Values:
    - AsDisplayed = 0
    - Wireframe = 1
    - Hidden = 2
    - Rendered = 3
    - VisualStyle = 4
    - RenderPreset = 5

--------------------------------------------------------------------------------

[CLASS] PlotSettingsValidator
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotSettingsValidator
Inherits: DisposableWrapper

  PROPERTIES:
    static PlotSettingsValidator Current { get; }

  METHODS:
    StringCollection GetCanonicalMediaNameList(PlotSettings plotSet)
    string GetLocaleMediaName(PlotSettings plotSet, int index)
    string GetLocaleMediaName(PlotSettings plotSet, string canonicalName)
    StringCollection GetPlotDeviceList()
    StringCollection GetPlotStyleSheetList()
    void RefreshLists(PlotSettings plotSet)
    void SetCanonicalMediaName(PlotSettings plotSet, string mediaName)
    void SetClosestMediaName(PlotSettings plotSet, double paperWidth, double paperHeight, PlotPaperUnit units, bool matchPrintableArea)
    void SetCurrentStyleSheet(PlotSettings plotSet, string styleSheetName)
    void SetCustomPrintScale(PlotSettings plotSet, CustomScale scale)
    void SetDefaultPlotConfig(PlotSettings plotSet)
    void SetPlotCentered(PlotSettings plotSet, bool isCentered)
    void SetPlotConfigurationName(PlotSettings plotSet, string plotDeviceName, string mediaName)
    void SetPlotOrigin(PlotSettings plotSet, Point2d origin)
    void SetPlotPaperUnits(PlotSettings plotSet, PlotPaperUnit units)
    void SetPlotRotation(PlotSettings plotSet, PlotRotation rotationType)
    void SetPlotType(PlotSettings plotSet, PlotType plotAreaType)
    void SetPlotViewName(PlotSettings plotSet, string viewName)
    void SetPlotWindowArea(PlotSettings plotSet, Extents2d windowArea)
    void SetStdScale(PlotSettings plotSet, double standardScale)
    void SetStdScaleType(PlotSettings plotSet, StdScaleType scaleType)
    void SetUseStandardScale(PlotSettings plotSet, bool useStandard)
    void SetZoomToPaperOnUpdate(PlotSettings plotSet, bool doZoom)

--------------------------------------------------------------------------------

[STRUCT] PlotStyleDescriptor
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotStyleDescriptor

  CONSTRUCTORS:
    new PlotStyleDescriptor(ObjectId id, PlotStyleNameType type)

  PROPERTIES:
    ObjectId Id { get; }
    PlotStyleNameType Type { get; }

  METHODS:
    bool Equals(object unmanagedObjPtr)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[ENUM] PlotStyleNameType
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotStyleNameType
Inherits: Enum

  Values:
    - PlotStyleNameByLayer = 0
    - PlotStyleNameByBlock = 1
    - PlotStyleNameIsDictionaryDefault = 2
    - PlotStyleNameById = 3

--------------------------------------------------------------------------------

[CLASS] PlotStyleTableChangedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotStyleTableChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId Id { get; }
    string NewName { get; }

--------------------------------------------------------------------------------

[CLASS] PlotStyleTableChangedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotStyleTableChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PlotStyleTableChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PlotStyleTableChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PlotStyleTableChangedEventArgs e)

--------------------------------------------------------------------------------

[ENUM] PlotType
Full Name: Autodesk.AutoCAD.DatabaseServices.PlotType
Inherits: Enum

  Values:
    - Display = 0
    - Extents = 1
    - Limits = 2
    - View = 3
    - Window = 4
    - Layout = 5

--------------------------------------------------------------------------------

[CLASS] Point3AngularDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.Point3AngularDimension
Inherits: Dimension

  CONSTRUCTORS:
    new Point3AngularDimension(Point3d centerPoint, Point3d line1Point, Point3d line2Point, Point3d arcPoint, string dimensionText, ObjectId dimensionStyle)
    new Point3AngularDimension()

  PROPERTIES:
    Point3d ArcPoint { get; set; }
    Point3d CenterPoint { get; set; }
    Point3d XLine1Point { get; set; }
    Point3d XLine2Point { get; set; }

--------------------------------------------------------------------------------

[CLASS] PointCloud
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloud
Inherits: Entity

  CONSTRUCTORS:
    new PointCloud()

  METHODS:
    static void RegenPointClouds()

--------------------------------------------------------------------------------

[CLASS] PointCloudClassificationColorRamp
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudClassificationColorRamp
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new PointCloudClassificationColorRamp()

  PROPERTIES:
    string Name { get; set; }
    int NumColors { get; }

  METHODS:
    EntityColor Color(int c)
    void SetColor(int c, EntityColor color)
    void SetFrom(PointCloudClassificationColorRamp source)
    void SetVisibility(int c, bool bVisible)
    bool Visibility(int c)

--------------------------------------------------------------------------------

[CLASS] PointCloudColorMap
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudColorMap
Inherits: DBObject

  PROPERTIES:
    string[] ClassificationSchemeGUIDs { get; }
    string[] ColorSchemeGUIDs { get; }
    string DefaultClassificationColorScheme { get; set; }
    string DefaultElevationColorScheme { get; set; }
    string DefaultIntensityColorScheme { get; set; }

  METHODS:
    string[] ClassificationColorSchemeInUse()
    bool ClassificationScheme(string name, out PointCloudClassificationColorRamp target)
    bool ColorScheme(string name, out PointCloudColorRamp target)
    string[] ColorSchemeInUse()
    bool DeleteClassificationScheme(string name)
    bool DeleteColorScheme(string name)
    static ObjectId GetColorMapId(Database hostDatabase)
    bool HasClassificationScheme(string name)
    bool HasColorScheme(string name)
    bool SetClassificationScheme(string name, PointCloudClassificationColorRamp source)
    bool SetColorScheme(string name, PointCloudColorRamp source)

  EVENTS:
    event PointCloudColorSchemeChangedEventHandler PointCloudColorSchemeChanged

--------------------------------------------------------------------------------

[CLASS] PointCloudColorRamp
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudColorRamp
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new PointCloudColorRamp()

  PROPERTIES:
    string Name { get; set; }
    int NumColors { get; set; }

  METHODS:
    EntityColor Color(int c)
    void SetColor(int c, EntityColor color)
    void SetFrom(PointCloudColorRamp source)
    void SetVisibility(int c, bool bVisible)
    bool Visibility(int c)

--------------------------------------------------------------------------------

[CLASS] PointCloudColorSchemeChangedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudColorSchemeChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PointCloudColorSchemeChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(PointCloudColorMap __unnamed000, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(PointCloudColorMap A_0)

--------------------------------------------------------------------------------

[CLASS] PointCloudCrop
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudCrop
Implements: IDisposable

  PROPERTIES:
    Plane CropPlane { get; set; }
    PointCloudCropType CropType { get; set; }
    bool Inside { get; set; }
    bool Inverted { get; set; }
    Point3dCollection Vertices { get; set; }

  METHODS:
    void Clear()
    static PointCloudCrop Create(IntPtr unmanagedObjPtr)
    void Dispose()
    bool IsValid()

--------------------------------------------------------------------------------

[ENUM] PointCloudCropType
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudCropType
Inherits: Enum

  Values:
    - Invalid = 0
    - Rectangular = 1
    - Polygonal = 2
    - Circular = 3

--------------------------------------------------------------------------------

[CLASS] PointCloudDefEx
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudDefEx
Inherits: DBObject

  CONSTRUCTORS:
    new PointCloudDefEx()

  PROPERTIES:
    string ActiveFileName { get; set; }
    double defaultHeight { get; }
    double defaultLength { get; }
    double defaultWidth { get; }
    int EntityCount { get; }
    Extents3d extents { get; }
    string FileType { get; }
    string SourceFileName { get; set; }
    long totalPointsCount { get; }
    int totalRegionsCount { get; }
    int totalScansCount { get; }

  METHODS:
    static int classVersion()
    string[] getAllRcsFilePaths()
    string getRcsFilePath(string guid)
    bool load()
    bool unload()

--------------------------------------------------------------------------------

[ENUM] PointCloudDispOptionOutOfRange
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudDispOptionOutOfRange
Inherits: Enum

  Values:
    - UseMinMaxColors = 0
    - UseRGBScanColors = 1
    - HidePoints = 2

--------------------------------------------------------------------------------

[CLASS] PointCloudEx
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudEx
Inherits: Entity

  CONSTRUCTORS:
    new PointCloudEx()

  PROPERTIES:
    string ActiveFileName { get; }
    bool CroppingInverted { get; set; }
    string CurrentColorScheme { get; set; }
    bool ElevationApplyToFixedRange { get; set; }
    bool ElevationGradient { get; set; }
    PointCloudDispOptionOutOfRange ElevationOutOfRangeBehavior { get; set; }
    Extents3d GeomExtents { get; }
    bool IntensityGradient { get; set; }
    PointCloudDispOptionOutOfRange IntensityOutOfRangeBehavior { get; set; }
    Tuple<double, double> LimitBoxBound { get; }
    Point3d Location { get; set; }
    Tuple<double, double> MinMaxElevation { get; set; }
    Tuple<int, int> MinMaxIntensity { get; set; }
    Extents3d NativeExtents { get; }
    ObjectId PointCloudDefExId { get; set; }
    double Rotation { get; set; }
    double Scale { get; set; }
    bool ShowCropped { get; set; }
    PointCloudStylizationType Stylization { get; set; }

  METHODS:
    void addCroppingBoundary(PointCloudCrop cropping)
    static ObjectId AttachPointCloud(string filename, Point3d location, double scale, double rotation, Database db)
    bool clearCropping()
    PointCloudItem findRegionItem(int regionId)
    PointCloudItem findScanItem(string scanGuid)
    string GetColorSchemeForStylization(PointCloudStylizationType type)
    int getCroppingCount()
    PointCloudCrop getPointCloudCropping(int index)
    List<PointCloudItem> getPointCloudDataList()
    bool GetScanViewInfo(string scanGuid, out ValueType origin, out ValueType extent)
    PointCloudPropertyState HasProperty(PointCloudProperty prop)
    bool removeLastCropping()
    void setAllRegionsVisibility(bool visible, bool includeUnassigned)
    void setAllScansVisibility(bool visible)
    void SetColorSchemeForStylization(string name, PointCloudStylizationType type)
    void setRegionVisibility(int regionId, bool visible)
    void setScanVisibility(string scanGuid, bool visible)
    bool TransformBy(Matrix3d xform)

--------------------------------------------------------------------------------

[CLASS] PointCloudItem
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudItem
Inherits: Notifier

  CONSTRUCTORS:
    new PointCloudItem()

  PROPERTIES:
    PointCloudItemType CategoryId { get; set; }
    ValueType CurrentAdsName { get; set; }
    string Guid { get; set; }
    int ItemId { get; set; }
    string Name { get; set; }
    string ProjectName { get; set; }
    bool Visibility { get; set; }

  METHODS:
    PointCloudItem Clone()
    bool CompareTo(PointCloudItem other)
    static List<PointCloudItem> CreatePointCloudDataItemCollection(IntPtr unmanagedObjPtr)
    string ToCmdString()

--------------------------------------------------------------------------------

[ENUM] PointCloudItemType
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudItemType
Inherits: Enum

  Values:
    - LegacyPointCloud = 0
    - PointCloudProject = 1
    - IndividualScan = 2
    - Scan = 3
    - Region = 4
    - ScanRootGroup = 5
    - RegionRootGroup = 6
    - UnassignedPoint = 7

--------------------------------------------------------------------------------

[ENUM] PointCloudProperty
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudProperty
Inherits: Enum

  Values:
    - Color = 1
    - Intensity = 2
    - Classification = 3
    - Normal = 4
    - TerrestrialClassification = 7

--------------------------------------------------------------------------------

[ENUM] PointCloudPropertyState
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudPropertyState
Inherits: Enum

  Values:
    - Some = 0
    - All = 1
    - None = -1

--------------------------------------------------------------------------------

[ENUM] PointCloudStylizationType
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCloudStylizationType
Inherits: Enum

  Values:
    - TrueColor = 1
    - SingleColor = 2
    - NormalRamp = 3
    - HeightRamp = 4
    - IntensityRamp = 5
    - ClassificationRamp = 6

--------------------------------------------------------------------------------

[CLASS] PointCoincidenceConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCoincidenceConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new PointCoincidenceConstraint()

--------------------------------------------------------------------------------

[CLASS] PointCurveConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.PointCurveConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new PointCurveConstraint()

--------------------------------------------------------------------------------

[ENUM] Poly2dType
Full Name: Autodesk.AutoCAD.DatabaseServices.Poly2dType
Inherits: Enum

  Values:
    - SimplePoly = 0
    - FitCurvePoly = 1
    - QuadSplinePoly = 2
    - CubicSplinePoly = 3

--------------------------------------------------------------------------------

[ENUM] Poly3dType
Full Name: Autodesk.AutoCAD.DatabaseServices.Poly3dType
Inherits: Enum

  Values:
    - SimplePoly = 0
    - QuadSplinePoly = 1
    - CubicSplinePoly = 2

--------------------------------------------------------------------------------

[CLASS] PolyFaceMesh
Full Name: Autodesk.AutoCAD.DatabaseServices.PolyFaceMesh
Inherits: Entity
Implements: IEnumerable

  CONSTRUCTORS:
    new PolyFaceMesh()

  PROPERTIES:
    int NumFaces { get; }
    int NumVertices { get; }

  METHODS:
    ObjectId AppendFaceRecord(FaceRecord toAppend)
    ObjectId AppendVertex(PolyFaceMeshVertex vertexToAppend)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] PolyFaceMeshVertex
Full Name: Autodesk.AutoCAD.DatabaseServices.PolyFaceMeshVertex
Inherits: Vertex

  CONSTRUCTORS:
    new PolyFaceMeshVertex(Point3d position)
    new PolyFaceMeshVertex()

  PROPERTIES:
    Point3d Position { get; set; }

--------------------------------------------------------------------------------

[CLASS] PolygonMesh
Full Name: Autodesk.AutoCAD.DatabaseServices.PolygonMesh
Inherits: Entity
Implements: IEnumerable

  CONSTRUCTORS:
    new PolygonMesh(PolyMeshType type, int size, int sizeValue, Point3dCollection vertices, bool closedValue, bool closed)
    new PolygonMesh()

  PROPERTIES:
    bool IsMClosed { get; }
    bool IsNClosed { get; }
    short MSize { get; set; }
    short MSurfaceDensity { get; set; }
    short NSize { get; set; }
    short NSurfaceDensity { get; set; }
    PolyMeshType PolyMeshType { get; set; }

  METHODS:
    ObjectId AppendVertex(PolygonMeshVertex toAppend)
    void ConvertToPolyMeshType(PolyMeshType newVal)
    IEnumerator GetEnumerator()
    void MakeMClosed()
    void MakeMOpen()
    void MakeNClosed()
    void MakeNOpen()
    void Straighten()
    void SurfaceFit(PolyMeshType surfType, int surfU, int surfV)
    void SurfaceFit()

--------------------------------------------------------------------------------

[CLASS] PolygonMeshVertex
Full Name: Autodesk.AutoCAD.DatabaseServices.PolygonMeshVertex
Inherits: Vertex

  CONSTRUCTORS:
    new PolygonMeshVertex(Point3d point)
    new PolygonMeshVertex()

  PROPERTIES:
    Point3d Position { get; set; }
    Vertex3dType VertexType { get; }

--------------------------------------------------------------------------------

[CLASS] Polyline
Full Name: Autodesk.AutoCAD.DatabaseServices.Polyline
Inherits: Curve

  CONSTRUCTORS:
    new Polyline(int vertices)
    new Polyline()

  PROPERTIES:
    bool Closed { get; set; }
    double ConstantWidth { get; set; }
    double Elevation { get; set; }
    bool HasBulges { get; }
    bool HasWidth { get; }
    bool IsOnlyLines { get; }
    double Length { get; }
    Vector3d Normal { get; set; }
    int NumberOfVertices { get; }
    bool Plinegen { get; set; }
    double Thickness { get; set; }

  METHODS:
    void AddVertexAt(int index, Point2d pt, double bulge, double startWidth, double endWidth)
    void ConvertFrom(Entity entity, bool transferId)
    Polyline2d ConvertTo(bool transferId)
    CircularArc2d GetArcSegment2dAt(int index)
    CircularArc3d GetArcSegmentAt(int index)
    double GetBulgeAt(int index)
    double GetEndWidthAt(int index)
    LineSegment2d GetLineSegment2dAt(int index)
    LineSegment3d GetLineSegmentAt(int index)
    Point2d GetPoint2dAt(int index)
    Point3d GetPoint3dAt(int value)
    SegmentType GetSegmentType(int index)
    double GetStartWidthAt(int index)
    void MaximizeMemory()
    void MinimizeMemory()
    bool OnSegmentAt(int index, Point2d pt2d, double value)
    void RemoveVertexAt(int index)
    void Reset(bool reuse, int vertices)
    void SetBulgeAt(int index, double bulge)
    void SetEndWidthAt(int index, double endWidth)
    void SetPointAt(int index, Point2d pt)
    void SetStartWidthAt(int index, double startWidth)

--------------------------------------------------------------------------------

[CLASS] Polyline2d
Full Name: Autodesk.AutoCAD.DatabaseServices.Polyline2d
Inherits: Curve
Implements: IEnumerable

  CONSTRUCTORS:
    new Polyline2d(Poly2dType type, Point3dCollection vertices, double elevation, bool closed, double startWidth, double endWidth, DoubleCollection bulges)
    new Polyline2d()

  PROPERTIES:
    bool Closed { get; set; }
    double ConstantWidth { get; set; }
    double DefaultEndWidth { get; set; }
    double DefaultStartWidth { get; set; }
    double Elevation { get; set; }
    double Length { get; }
    bool LinetypeGenerationOn { get; set; }
    Vector3d Normal { get; set; }
    Poly2dType PolyType { get; set; }
    double Thickness { get; set; }

  METHODS:
    ObjectId AppendVertex(Vertex2d vertexToAppend)
    void ConvertToPolyType(Poly2dType newVal)
    void CurveFit()
    IEnumerator GetEnumerator()
    ObjectId InsertVertexAt(ObjectId vertexId, Vertex2d newVertex)
    void InsertVertexAt(Vertex2d indexVertex, Vertex2d newVertex)
    void NonDBAppendVertex(Vertex2d vertexToAppend)
    void SplineFit(Poly2dType value, int segments)
    void SplineFit()
    void Straighten()
    Point3d VertexPosition(Vertex2d vertex)

--------------------------------------------------------------------------------

[CLASS] Polyline3d
Full Name: Autodesk.AutoCAD.DatabaseServices.Polyline3d
Inherits: Curve
Implements: IEnumerable

  CONSTRUCTORS:
    new Polyline3d(Poly3dType type, Point3dCollection vertices, bool closed)
    new Polyline3d()

  PROPERTIES:
    bool Closed { get; set; }
    double Length { get; }
    Poly3dType PolyType { get; set; }

  METHODS:
    ObjectId AppendVertex(PolylineVertex3d vertexToAppend)
    void ConvertToPolyType(Poly3dType newVal)
    IEnumerator GetEnumerator()
    ObjectId InsertVertexAt(ObjectId indexVertexId, PolylineVertex3d newVertex)
    void InsertVertexAt(PolylineVertex3d indexVertex, PolylineVertex3d newVertex)
    void SplineFit(Poly3dType value, int segments)
    void SplineFit()
    void Straighten()

--------------------------------------------------------------------------------

[CLASS] PolylineVertex3d
Full Name: Autodesk.AutoCAD.DatabaseServices.PolylineVertex3d
Inherits: Vertex

  CONSTRUCTORS:
    new PolylineVertex3d(Point3d param0)
    new PolylineVertex3d()

  PROPERTIES:
    Point3d Position { get; set; }
    Vertex3dType VertexType { get; }

--------------------------------------------------------------------------------

[ENUM] PolyMeshType
Full Name: Autodesk.AutoCAD.DatabaseServices.PolyMeshType
Inherits: Enum

  Values:
    - SimpleMesh = 0
    - QuadSurfaceMesh = 5
    - CubicSurfaceMesh = 6
    - BezierSurfaceMesh = 8

--------------------------------------------------------------------------------

[CLASS] Profile3d
Full Name: Autodesk.AutoCAD.DatabaseServices.Profile3d
Inherits: RXObject

  CONSTRUCTORS:
    new Profile3d(Profile3d profile3d)
    new Profile3d(VertexRef vertexRef)
    new Profile3d(PathRef pathRef)
    new Profile3d(FullSubentityPath faceSubentPath)
    new Profile3d(Entity entity)
    new Profile3d()

  PROPERTIES:
    Entity Entity { get; set; }
    bool IsClosed { get; }
    bool IsEdge { get; }
    bool IsFace { get; }
    bool IsPlanar { get; }
    bool IsSubent { get; }
    bool IsValid { get; }
    PathRef ProfilePathRef { get; set; }
    VertexRef ProfileVertexRef { get; set; }

  METHODS:
    static Profile3d ConvertProfile(PathRef[] pathRefArray)
    Profile3d[] ConvertProfile(bool explodeMultiFaceRegions, bool convertSurfaceToEdges, bool nonPlanarOnly, bool outerLoopOnly)
    void CopyFrom(Profile3d source)
    static Profile3d[] MergeProfiles(Profile3d[] profiles, bool mergeEdges, bool mergeCurves)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PropertiesOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.PropertiesOverrule
Inherits: Overrule

  METHODS:
    Guid GetClassID(DBObject entity)
    void List(Entity entity)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

[CLASS] ProxyEntity
Full Name: Autodesk.AutoCAD.DatabaseServices.ProxyEntity
Inherits: Entity

  PROPERTIES:
    string ApplicationDescription { get; }
    GraphicsMetafileType GraphicsMetafileType { get; }
    string OriginalClassName { get; }
    string OriginalDxfName { get; }
    int ProxyFlags { get; }

  METHODS:
    DBObjectReferenceCollection GetReferences()

--------------------------------------------------------------------------------

[CLASS] ProxyObject
Full Name: Autodesk.AutoCAD.DatabaseServices.ProxyObject
Inherits: DBObject

  PROPERTIES:
    string ApplicationDescription { get; }
    string OriginalClassName { get; }
    string OriginalDxfName { get; }
    int ProxyFlags { get; }

  METHODS:
    DBObjectReferenceCollection GetReferences()
    static void ResurrectMeNow(ObjectId id)

--------------------------------------------------------------------------------

[CLASS] ProxyResurrectionCompletedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.ProxyResurrectionCompletedEventArgs
Inherits: EventArgs

  PROPERTIES:
    string ApplicationName { get; }
    ObjectIdCollection Ids { get; }

--------------------------------------------------------------------------------

[CLASS] ProxyResurrectionCompletedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.ProxyResurrectionCompletedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ProxyResurrectionCompletedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ProxyResurrectionCompletedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ProxyResurrectionCompletedEventArgs e)

--------------------------------------------------------------------------------

