# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[CLASS] RadialDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.RadialDimension
Inherits: Dimension

  CONSTRUCTORS:
    new RadialDimension(Point3d center, Point3d chordPoint, double leaderLength, string dimensionText, ObjectId dimensionStyle)
    new RadialDimension()

  PROPERTIES:
    Point3d Center { get; set; }
    Point3d ChordPoint { get; set; }
    double LeaderLength { get; set; }

--------------------------------------------------------------------------------

[CLASS] RadialDimensionLarge
Full Name: Autodesk.AutoCAD.DatabaseServices.RadialDimensionLarge
Inherits: Dimension

  CONSTRUCTORS:
    new RadialDimensionLarge(Point3d center, Point3d chordPoint, Point3d overrideCenter, Point3d jogPoint, double jogAngle, string dimensionText, ObjectId dimensionStyle)
    new RadialDimensionLarge()

  PROPERTIES:
    Point3d Center { get; set; }
    Point3d ChordPoint { get; set; }
    double JogAngle { get; set; }
    Point3d JogPoint { get; set; }
    Point3d OverrideCenter { get; set; }

--------------------------------------------------------------------------------

[CLASS] RadiusDiameterConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.RadiusDiameterConstraint
Inherits: ExplicitConstraint

  CONSTRUCTORS:
    new RadiusDiameterConstraint(RadDiaConstrType type)
    new RadiusDiameterConstraint()

  PROPERTIES:
    RadDiaConstrType ConstrType { get; }

--------------------------------------------------------------------------------

[CLASS] RapidRTRenderSettings
Full Name: Autodesk.AutoCAD.DatabaseServices.RapidRTRenderSettings
Inherits: RenderSettings

  CONSTRUCTORS:
    new RapidRTRenderSettings()

  PROPERTIES:
    float FilterHeight { get; set; }
    RapidRTFilterType FilterType { get; set; }
    float FilterWidth { get; set; }
    RapidRTLightingMode LightingModel { get; set; }
    int RenderLevel { get; set; }
    RapidRTRenderTarget RenderTarget { get; set; }
    int RenderTime { get; set; }

  METHODS:
    void CopyFrom(RapidRTRenderSettings srcObject)

--------------------------------------------------------------------------------

[ENUM] RapidRTRenderTarget
Full Name: Autodesk.AutoCAD.DatabaseServices.RapidRTRenderTarget
Inherits: Enum

  Values:
    - Level = 0
    - Time = 1
    - Infinite = 2

--------------------------------------------------------------------------------

[CLASS] RasterImage
Full Name: Autodesk.AutoCAD.DatabaseServices.RasterImage
Inherits: Image

  CONSTRUCTORS:
    new RasterImage()

  PROPERTIES:
    byte Brightness { get; set; }
    ClipBoundaryType ClipBoundaryType { get; }
    byte Contrast { get; set; }
    ImageDisplayOptions DisplayOptions { get; set; }
    byte Fade { get; set; }
    double Height { get; }
    ObjectId ImageDefId { get; set; }
    double ImageHeight { get; }
    bool ImageTransparency { get; set; }
    double ImageWidth { get; }
    bool IsClipped { get; set; }
    string Name { get; set; }
    CoordinateSystem3d Orientation { get; set; }
    string Path { get; }
    Matrix3d PixelToModelTransform { get; }
    Point3d Position { get; }
    ObjectId ReactorId { get; set; }
    double Rotation { get; set; }
    Vector2d Scale { get; }
    bool ShowImage { get; set; }
    double Width { get; }

  METHODS:
    void AssociateRasterDef(RasterImageDef definition)
    static void EnableReactors(bool enable)
    Point2dCollection GetClipBoundary()
    Point3dCollection GetVertices()
    Vector2d ImageSize(bool getCachedValue)
    void SetClipBoundary(ClipBoundaryType type, Point2dCollection clipBoundaryVertices)
    void SetClipBoundary(Vector2d size)
    void SetClipBoundaryToWholeImage()

--------------------------------------------------------------------------------

[CLASS] RasterImageDef
Full Name: Autodesk.AutoCAD.DatabaseServices.RasterImageDef
Inherits: DBObject

  CONSTRUCTORS:
    new RasterImageDef()

  PROPERTIES:
    string ActiveFileName { get; set; }
    int ColorDepth { get; }
    IntPtr FileDescCopy { get; }
    string FileType { get; }
    bool ImageModified { get; set; }
    bool IsEmbedded { get; }
    bool IsLoaded { get; }
    ImageOrg Organization { get; }
    Vector2d ResolutionMMPerPixel { get; set; }
    Unit ResolutionUnits { get; set; }
    string SearchForActivePath { get; }
    Vector2d Size { get; }
    string SourceFileName { get; set; }
    int UndoStoreSize { get; set; }

  METHODS:
    void CloseImage()
    static ObjectId CreateImageDictionary(Database database)
    void Embed()
    int GetEntityCount(out bool locked)
    static ObjectId GetImageDictionary(Database database)
    IntPtr ImageCopy(bool forceImageLoad)
    void Load()
    string LocateActivePath()
    IntPtr OpenImage()
    void SetImage(IntPtr image, IntPtr fileDescription, bool modifyDatabase)
    static string SuggestName(DBDictionary imageDictionary, string newImagePathName)
    void Unload(bool modifyDatabase)
    void UpdateEntities()

--------------------------------------------------------------------------------

[CLASS] RasterVariables
Full Name: Autodesk.AutoCAD.DatabaseServices.RasterVariables
Inherits: DBObject

  CONSTRUCTORS:
    new RasterVariables()

  PROPERTIES:
    FrameSetting ImageFrame { get; set; }
    ImageQuality ImageQuality { get; set; }
    Unit UserScale { get; set; }

--------------------------------------------------------------------------------

[CLASS] Ray
Full Name: Autodesk.AutoCAD.DatabaseServices.Ray
Inherits: Curve

  CONSTRUCTORS:
    new Ray()

  PROPERTIES:
    Point3d BasePoint { get; set; }
    Point3d SecondPoint { get; set; }
    Vector3d UnitDir { get; set; }

--------------------------------------------------------------------------------

[STRUCT] Rectangle3d
Full Name: Autodesk.AutoCAD.DatabaseServices.Rectangle3d

  CONSTRUCTORS:
    new Rectangle3d(Point3d upperLeft, Point3d upperRight, Point3d lowerLeft, Point3d lowerRight)

  PROPERTIES:
    Point3d LowerLeft { get; }
    Point3d LowerRight { get; }
    Point3d UpperLeft { get; }
    Point3d UpperRight { get; }

  METHODS:
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] RegAppTable
Full Name: Autodesk.AutoCAD.DatabaseServices.RegAppTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] RegAppTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.RegAppTableRecord
Inherits: SymbolTableRecord

  CONSTRUCTORS:
    new RegAppTableRecord()

--------------------------------------------------------------------------------

[CLASS] Region
Full Name: Autodesk.AutoCAD.DatabaseServices.Region
Inherits: Entity

  CONSTRUCTORS:
    new Region()

  PROPERTIES:
    double Area { get; }
    Extents3d GeometricExtents { get; }
    bool IsNull { get; }
    Vector3d Normal { get; }
    int NumChanges { get; }
    double Perimeter { get; }
    bool UsesGraphicsCache { get; }

  METHODS:
    RegionAreaProperties AreaProperties(ref Point3d origin, ref Vector3d xAxis, ref Vector3d yAxis)
    void BooleanOperation(BooleanOperationType operation, Region otherRegion)
    static DBObjectCollection CreateFromCurves(DBObjectCollection curveSegments)

--------------------------------------------------------------------------------

[STRUCT] RegionAreaProperties
Full Name: Autodesk.AutoCAD.DatabaseServices.RegionAreaProperties

  PROPERTIES:
    double Area { get; }
    Point2d Centroid { get; }
    Extents2d Extents { get; }
    Vector2d MomentsOfInertia { get; }
    double Perimeter { get; }
    Vector2d PrincipalAxes { get; }
    Vector2d PrincipalMoments { get; }
    double ProductOfInertia { get; }
    Vector2d RadiiOfGyration { get; }

--------------------------------------------------------------------------------

[CLASS] RenderEnvironment
Full Name: Autodesk.AutoCAD.DatabaseServices.RenderEnvironment
Inherits: DBObject

  CONSTRUCTORS:
    new RenderEnvironment()

  PROPERTIES:
    DoubleRangeParameter Distances { get; set; }
    bool EnvironmentImageEnabled { get; set; }
    string EnvironmentImageFileName { get; set; }
    bool FogBackgroundEnabled { get; set; }
    EntityColor FogColor { get; set; }
    DoubleRangeParameter FogDensity { get; set; }
    bool FogEnabled { get; set; }

--------------------------------------------------------------------------------

[CLASS] RenderGlobal
Full Name: Autodesk.AutoCAD.DatabaseServices.RenderGlobal
Inherits: DBObject

  CONSTRUCTORS:
    new RenderGlobal()

  PROPERTIES:
    DimensionsParameter Dimensions { get; set; }
    bool HighInfoLevel { get; set; }
    bool PredefinedPresetsFirst { get; set; }
    ProcedureAndDestinationParameter ProcedureAndDestination { get; set; }
    bool SaveEnabled { get; set; }
    string SaveFileName { get; set; }

--------------------------------------------------------------------------------

[CLASS] RenderSettings
Full Name: Autodesk.AutoCAD.DatabaseServices.RenderSettings
Inherits: DBObject

  CONSTRUCTORS:
    new RenderSettings()

  PROPERTIES:
    bool BackFacesEnabled { get; set; }
    string Description { get; set; }
    bool DiagnosticBackgroundEnabled { get; set; }
    int DisplayIndex { get; set; }
    bool IsPredefined { get; set; }
    bool MaterialsEnabled { get; set; }
    string Name { get; set; }
    string PreviewImageFileName { get; set; }
    bool ShadowsEnabled { get; set; }
    bool TextureSampling { get; set; }

--------------------------------------------------------------------------------

[ENUM] ReservedStringEnumType
Full Name: Autodesk.AutoCAD.DatabaseServices.ReservedStringEnumType
Inherits: Enum

  Values:
    - ByLayer = 0
    - ByBlock = 1
    - None = 2
    - Continuous = 3
    - Standard = 4
    - Normal = 5
    - DefPoints = 6
    - Default = 7
    - Missing = 8
    - ByColor = 9
    - Model = 10
    - Global = 11
    - VS2DWireframe = 12
    - VS3DWireframe = 13
    - VS3DHidden = 14
    - VSRealistic = 15
    - VSConceptual = 16
    - Title = 17
    - Header = 18
    - Data = 19
    - VSShadesOfGray = 20
    - VSSketchy = 21
    - VSXRay = 22
    - VSShadedWithEdges = 23
    - VSShaded = 24

--------------------------------------------------------------------------------

[CLASS] ResultBuffer
Full Name: Autodesk.AutoCAD.DatabaseServices.ResultBuffer
Inherits: DisposableWrapper
Implements: IEnumerable, IFormattable

  CONSTRUCTORS:
    new ResultBuffer(params TypedValue[] values)
    new ResultBuffer()

  METHODS:
    void Add(object value)
    void Add(TypedValue value)
    TypedValue[] AsArray()
    static ResultBuffer Create(IntPtr buffer, bool autoDelete)
    bool Equals(object obj)
    ResultBufferEnumerator GetEnumerator()
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)
    string ToString(string format, IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] ResultBufferEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.ResultBufferEnumerator
Implements: IEnumerator

  PROPERTIES:
    TypedValue Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] RevolvedSurface
Full Name: Autodesk.AutoCAD.DatabaseServices.RevolvedSurface
Inherits: Surface

  CONSTRUCTORS:
    new RevolvedSurface()

  PROPERTIES:
    Vector3d AxisDirection { get; set; }
    Point3d AxisPoint { get; set; }
    double RevolveAngle { get; set; }
    Entity RevolveEntity { get; }
    RevolveOptions RevolveOptions { get; set; }
    Profile3d RevolveProfile { get; }
    double StartAngle { get; }

  METHODS:
    void CreateRevolvedSurface(Entity revolveEntity, Point3d axisPoint, Vector3d axisDirection, double revolveAngle, double startAngle, RevolveOptions revolveOptions)
    void SetRevolve(Point3d axisPoint, Vector3d axisDirection, double revolveAngle, RevolveOptions revolveOptions)

--------------------------------------------------------------------------------

[CLASS] RevolveOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.RevolveOptions
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new RevolveOptions(RevolveOptions opts)
    new RevolveOptions()

  PROPERTIES:
    bool CloseToAxis { get; }
    double DraftAngle { get; }
    double TwistAngle { get; }

  METHODS:
    RevolveOptionsCheckRevolveCurveOut CheckRevolveCurve(Entity __unnamed000, Point3d axisPnt, Vector3d axisDir, bool displayErrorMessages)
    object Clone()

--------------------------------------------------------------------------------

[CLASS] RevolveOptionsBuilder
Full Name: Autodesk.AutoCAD.DatabaseServices.RevolveOptionsBuilder

  CONSTRUCTORS:
    new RevolveOptionsBuilder(RevolveOptions value)
    new RevolveOptionsBuilder()

  PROPERTIES:
    bool CloseToAxis { get; set; }
    double DraftAngle { get; set; }
    double TwistAngle { get; set; }

  METHODS:
    RevolveOptions ToRevolveOptions()

--------------------------------------------------------------------------------

[CLASS] RevolveOptionsCheckRevolveCurveOut
Full Name: Autodesk.AutoCAD.DatabaseServices.RevolveOptionsCheckRevolveCurveOut

  CONSTRUCTORS:
    new RevolveOptionsCheckRevolveCurveOut()

  PROPERTIES:
    bool Closed { get; }
    bool EndPointsOnAxis { get; }
    bool Planar { get; }

--------------------------------------------------------------------------------

[ENUM] RigidSetTypeInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.RigidSetTypeInfo
Inherits: Enum

  Values:
    - NotRigidSet = 0
    - ScalableRigidSet = 1
    - NonScalableRigidSet = 2

--------------------------------------------------------------------------------

[CLASS] RotatedDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.RotatedDimension
Inherits: Dimension

  CONSTRUCTORS:
    new RotatedDimension(double rotation, Point3d line1Point, Point3d line2Point, Point3d dimensionLinePoint, string dimensionText, ObjectId dimensionStyle)
    new RotatedDimension()

  PROPERTIES:
    Point3d DimLinePoint { get; set; }
    double Oblique { get; set; }
    double Rotation { get; set; }
    Point3d XLine1Point { get; set; }
    Point3d XLine2Point { get; set; }

--------------------------------------------------------------------------------

[ENUM] RotationAngle
Full Name: Autodesk.AutoCAD.DatabaseServices.RotationAngle
Inherits: Enum

  Values:
    - Degrees000 = 0
    - Degrees090 = 1
    - Degrees180 = 2
    - Degrees270 = 3
    - DegreesUnknown = -1

--------------------------------------------------------------------------------

[CLASS] Row
Full Name: Autodesk.AutoCAD.DatabaseServices.Row
Inherits: CellRange

  PROPERTIES:
    double Height { get; set; }
    double MinimumHeight { get; }

--------------------------------------------------------------------------------

[CLASS] RowsCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.RowsCollection
Implements: IEnumerable<Row>, IEnumerable

  PROPERTIES:
    int Count { get; }
    Row Item { get; }

  METHODS:
    IEnumerator<Row> GetEnumerator()

--------------------------------------------------------------------------------

[ENUM] RowType
Full Name: Autodesk.AutoCAD.DatabaseServices.RowType
Inherits: Enum

  Values:
    - UnknownRow = 0
    - DataRow = 1
    - TitleRow = 2
    - HeaderRow = 4

--------------------------------------------------------------------------------

[ENUM] SaveType
Full Name: Autodesk.AutoCAD.DatabaseServices.SaveType
Inherits: Enum

  Values:
    - NoSave = 0
    - Save12 = 1
    - Save13 = 2
    - Save14 = 3
    - Save2000 = 4
    - Save2004 = 5
    - Save2007 = 6
    - Save2010 = 7
    - Save2013 = 8
    - Save2018 = 9

--------------------------------------------------------------------------------

[ENUM] ScaleEstimationMethod
Full Name: Autodesk.AutoCAD.DatabaseServices.ScaleEstimationMethod
Inherits: Enum

  Values:
    - ScaleEstMethodUnity = 1
    - ScaleEstMethodUserDefined = 2
    - ScaleEstMethodReferencePoint = 3
    - ScaleEstMethodPrismoidal = 4

--------------------------------------------------------------------------------

[CLASS] Section
Full Name: Autodesk.AutoCAD.DatabaseServices.Section
Inherits: Entity

  CONSTRUCTORS:
    new Section(Point3dCollection pts, Vector3d verticalDir, Vector3d vecViewingDir)
    new Section(Point3dCollection pts, Vector3d verticalDir)
    new Section()

  PROPERTIES:
    double BottomPlane { get; set; }
    IList<Point3d> Boundary { get; }
    double Elevation { get; set; }
    bool HasJogs { get; }
    Color IndicatorFillColor { get; set; }
    int IndicatorTransparency { get; set; }
    bool IsLiveSectionEnabled { get; set; }
    bool IsSlice { get; set; }
    string Name { get; set; }
    Vector3d Normal { get; }
    int NumVertices { get; }
    double SectionPlaneOffset { get; set; }
    ObjectId Settings { get; }
    SectionState State { get; set; }
    double ThicknessDepth { get; set; }
    double TopPlane { get; set; }
    Vector3d VerticalDirection { get; set; }
    Point3dCollection Vertices { set; }
    Vector3d ViewingDirection { get; set; }

  METHODS:
    void AddVertex(int nInsertAt, Point3d pt)
    void CreateJog(Point3d ptOnSection)
    void GenerateSectionGeometry(Entity pEnt, out Array pIntFillEnts, out Array pBackgroundEnts, out Array pForegroundEnts, out Array pFurveTangencyEnts, out Array pCurveTangencyEnts)
    Point3d GetVertex(int nIndex)
    void GetVertices(Point3dCollection pts)
    double Height(SectionHeight nHeightType)
    SectionHitTestInfo HitTest(Point3d ptHit)
    void RemoveVertex(int nIndex)
    void SetHeight(SectionHeight nHeightType, double fHeight)
    void SetVertex(int nIndex, Point3d pt)

--------------------------------------------------------------------------------

[ENUM] SectionGeneration
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionGeneration
Inherits: Enum

  Values:
    - SourceAllObjects = 1
    - SourceSelectedObjects = 2
    - DestinationNewBlock = 16
    - DestinationReplaceBlock = 32
    - DestinationFile = 64

--------------------------------------------------------------------------------

[ENUM] SectionGeometry
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionGeometry
Inherits: Enum

  Values:
    - IntersectionBoundary = 1
    - IntersectionFill = 2
    - BackgroundGeometry = 4
    - ForegroundGeometry = 8
    - CurveTangencyLines = 16

--------------------------------------------------------------------------------

[ENUM] SectionHeight
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionHeight
Inherits: Enum

  Values:
    - HeightAboveSectionLine = 1
    - HeightBelowSectionLine = 2

--------------------------------------------------------------------------------

[STRUCT] SectionHitTestInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionHitTestInfo

  CONSTRUCTORS:
    new SectionHitTestInfo()

  PROPERTIES:
    int Index { get; }
    Point3d PtOnSegment { get; }
    SectionSubItem SubItem { get; }

--------------------------------------------------------------------------------

[CLASS] SectionManager
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionManager
Inherits: DBObject
Implements: IEnumerable

  CONSTRUCTORS:
    new SectionManager()

  PROPERTIES:
    ObjectId LiveSection { get; }
    int NumSections { get; }

  METHODS:
    IEnumerator GetEnumerator()
    ObjectId GetSection(string pszName)
    string GetUniqueSectionName(string pszBaseName)

--------------------------------------------------------------------------------

[CLASS] SectionSettings
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionSettings
Inherits: DBObject

  CONSTRUCTORS:
    new SectionSettings()

  PROPERTIES:
    SectionType CurrentSectionType { get; set; }

  METHODS:
    Color Color(SectionType nSecType, SectionGeometry nGeometry)
    ObjectId DestinationBlock(SectionType nSecType)
    string DestinationFile(SectionType nSecType)
    bool DivisionLines(SectionType nSecType, SectionGeometry nGeometry)
    int EdgeTransparency(SectionType nSecType, SectionGeometry nGeometry)
    int FaceTransparency(SectionType nSecType, SectionGeometry nGeometry)
    SectionGeneration GenerationOptions(SectionType nSecType)
    string GetHatchPatternName(SectionType nSecType, SectionGeometry nGeometry)
    HatchPatternType GetHatchPatternType(SectionType nSecType, SectionGeometry nGeometry)
    void GetSourceObjects(SectionType nSecType, ObjectIdCollection ids)
    double HatchAngle(SectionType nSecType, SectionGeometry nGeometry)
    double HatchScale(SectionType nSecType, SectionGeometry nGeometry)
    double HatchSpacing(SectionType nSecType, SectionGeometry nGeometry)
    bool HatchVisibility(SectionType nSecType, SectionGeometry nGeometry)
    bool HiddenLine(SectionType nSecType, SectionGeometry nGeometry)
    string Layer(SectionType nSecType, SectionGeometry nGeometry)
    string Linetype(SectionType nSecType, SectionGeometry nGeometry)
    double LinetypeScale(SectionType nSecType, SectionGeometry nGeometry)
    LineWeight LineWeight(SectionType nSecType, SectionGeometry nGeometry)
    string PlotStyleName(SectionType nSecType, SectionGeometry nGeometry)
    void Reset(SectionType nSecType)
    void Reset()
    void SetColor(SectionType nSecType, SectionGeometry nGeometry, Color color)
    void SetDestinationBlock(SectionType nSecType, ObjectId id)
    void SetDestinationFile(SectionType nSecType, string pszFileName)
    void SetDivisionLines(SectionType nSecType, SectionGeometry nGeometry, bool bShow)
    void SetEdgeTransparency(SectionType nSecType, SectionGeometry nGeometry, int nTransparency)
    void SetFaceTransparency(SectionType nSecType, SectionGeometry nGeometry, int nTransparency)
    void SetGenerationOptions(SectionType nSecType, SectionGeneration nOptions)
    void SetHatchAngle(SectionType nSecType, SectionGeometry nGeometry, double fAngle)
    void SetHatchPatternName(SectionType nSecType, SectionGeometry nGeometry, string sNewName)
    void SetHatchPatternType(SectionType nSecType, SectionGeometry nGeometry, HatchPatternType nPatternType)
    void SetHatchScale(SectionType nSecType, SectionGeometry nGeometry, double fScale)
    void SetHatchSpacing(SectionType nSecType, SectionGeometry nGeometry, double fSpacing)
    void SetHatchVisibility(SectionType nSecType, SectionGeometry nGeometry, bool bVisible)
    void SetHiddenLine(SectionType nSecType, SectionGeometry nGeometry, bool bHiddenLine)
    void SetLayer(SectionType nSecType, SectionGeometry nGeometry, string pszLayer)
    void SetLinetype(SectionType nSecType, SectionGeometry nGeometry, string pszLinetype)
    void SetLinetypeScale(SectionType nSecType, SectionGeometry nGeometry, double fScale)
    void SetLineWeight(SectionType nSecType, SectionGeometry nGeometry, LineWeight nLineWeight)
    void SetPlotStyleName(SectionType nSecType, SectionGeometry nGeometry, string pszPlotStyleName)
    void SetSourceObjects(SectionType nSecType, ObjectIdCollection ids)
    void SetVisibility(SectionType nSecType, SectionGeometry nGeometry, bool bVisible)
    bool Visibility(SectionType nSecType, SectionGeometry nGeometry)

--------------------------------------------------------------------------------

[ENUM] SectionState
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionState
Inherits: Enum

  Values:
    - Plane = 1
    - Boundary = 2
    - Volume = 4

--------------------------------------------------------------------------------

[ENUM] SectionSubItem
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionSubItem
Inherits: Enum

  Values:
    - SectionSubItemNone = 0
    - SectionLine = 1
    - SectionLineTop = 2
    - SectionLineBottom = 4
    - BackLine = 8
    - BackLineTop = 16
    - BackLineBottom = 32
    - VerticalLineTop = 64
    - VerticalLineBottom = 128

--------------------------------------------------------------------------------

[CLASS] SectionSymbol
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionSymbol
Inherits: Entity

  CONSTRUCTORS:
    new SectionSymbol()

  PROPERTIES:
    string Identifier { get; set; }
    bool IsHalfSection { get; set; }
    bool IsViewDirectionLeft { get; set; }
    double Scale { get; set; }
    int SectionPointsCount { get; }
    ObjectId SymbolStyleId { get; set; }

  METHODS:
    void AddSectionPoint(Point3d pt, double bulge)
    double GetBulgeAt(int index)
    string GetLabelNameAt(int index)
    Vector3d GetLabelOffsetAt(int index)
    Vector3dCollection GetLabelOffsets()
    Point3d GetSectionPointAt(int index)
    Point3dCollection GetSectionPoints()
    void RemoveSectionPointAt(int index)
    void SetLabelNameAt(int index, string name)
    void SetLabelOffsetAt(int index, Vector3d offset)
    void SetLabelOffsets(Vector3dCollection offsets)
    void SetSectionPointAt(int index, Point3d pt, double bulge)
    void SetSectionPoints(Point3dCollection points, DoubleCollection bulges)
    void SetSectionPoints(Point3dCollection points)

--------------------------------------------------------------------------------

[ENUM] SectionType
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionType
Inherits: Enum

  Values:
    - LiveSection = 1
    - Section2d = 2
    - Section3d = 4

--------------------------------------------------------------------------------

[ENUM] SectionViewArrowDirection
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionViewArrowDirection
Inherits: Enum

  Values:
    - TowardsCuttingPlane = 0
    - AwayFromCuttingPlane = 1

--------------------------------------------------------------------------------

[ENUM] SectionViewIdentifierPosition
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionViewIdentifierPosition
Inherits: Enum

  Values:
    - EndCuttingPlane = 0
    - AboveDirectionArrowLine = 1
    - AboveDirectionArrowSymbol = 2
    - StartDirectionArrow = 3
    - EndDirectionArrow = 4

--------------------------------------------------------------------------------

[CLASS] SectionViewStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.SectionViewStyle
Inherits: DBObject

  CONSTRUCTORS:
    new SectionViewStyle()

  PROPERTIES:
    ObjectId ArrowEndSymbolId { get; set; }
    SectionViewArrowDirection ArrowPosition { get; set; }
    ObjectId ArrowStartSymbolId { get; set; }
    Color ArrowSymbolColor { get; set; }
    double ArrowSymbolExtensionLength { get; set; }
    double ArrowSymbolSize { get; set; }
    Color BendLineColor { get; set; }
    double BendLineLength { get; set; }
    ObjectId BendLineTypeId { get; set; }
    LineWeight BendLineWeight { get; set; }
    string Description { get; set; }
    double EndLineLength { get; set; }
    double EndLineOvershoot { get; set; }
    DoubleCollection HatchAngles { get; set; }
    Color HatchBackgroundColor { get; set; }
    Color HatchColor { get; set; }
    string HatchPattern { get; set; }
    double HatchScale { get; set; }
    Transparency HatchTransparency { get; set; }
    Color IdentifierColor { get; set; }
    string IdentifierExcludeCharacters { get; set; }
    double IdentifierHeight { get; set; }
    double IdentifierOffset { get; set; }
    SectionViewIdentifierPosition IdentifierPosition { get; set; }
    ObjectId IdentifierStyleId { get; set; }
    bool IsContinuousLabeling { get; set; }
    bool IsModifiedForRecompute { get; }
    string Name { get; set; }
    Color PlaneLineColor { get; set; }
    ObjectId PlaneLineTypeId { get; set; }
    LineWeight PlaneLineWeight { get; set; }
    bool ShowAllBendIndentifiers { get; set; }
    bool ShowAllPlaneLines { get; set; }
    bool ShowArrowheads { get; set; }
    bool ShowEndAndBendLines { get; set; }
    bool ShowHatching { get; set; }
    bool ShowViewLabel { get; set; }
    ModelDocViewLabelAlignmentType ViewLabelAlignment { get; set; }
    ModelDocViewLabelAttachmentPoint ViewLabelAttachment { get; set; }
    double ViewLabelOffset { get; set; }
    string ViewLabelPattern { get; set; }
    Color ViewLabelTextColor { get; set; }
    double ViewLabelTextHeight { get; set; }
    ObjectId ViewLabelTextStyleId { get; set; }

  METHODS:
    string DefaultViewName(int index)
    string GetViewLabelPattern(Field field)
    ObjectId PostViewStyleToDb(Database dataBasePointer, string styleName)
    void SetDatabaseDefaults(Database dataBasePointer)
    void SetViewLabelPattern(string pattern, Field field)
    bool ValidateIdentifierExcludeCharacters(string characters)
    bool ValidateViewName(string name)

--------------------------------------------------------------------------------

[ENUM] SecurityActions
Full Name: Autodesk.AutoCAD.DatabaseServices.SecurityActions
Inherits: Enum

  Values:
    - EncryptData = 1
    - EncryptProperties = 2
    - SignData = 16
    - AddTimeStamp = 32

--------------------------------------------------------------------------------

[ENUM] SecurityAlgorithm
Full Name: Autodesk.AutoCAD.DatabaseServices.SecurityAlgorithm
Inherits: Enum

  Values:
    - RC4 = 26625

--------------------------------------------------------------------------------

[CLASS] SecurityParameters
Full Name: Autodesk.AutoCAD.DatabaseServices.SecurityParameters

  CONSTRUCTORS:
    new SecurityParameters(string password, string providerName, string subject, string issuer, string serialNumber, string comment, string timeServer, SecurityActions actions, SecurityAlgorithm algorithm, uint keyLength, uint providerType)
    new SecurityParameters()

  PROPERTIES:
    SecurityActions Action { get; set; }
    SecurityAlgorithm Algorithm { get; set; }
    string Comment { get; set; }
    string Issuer { get; set; }
    int KeyLength { get; set; }
    string Password { get; set; }
    string ProviderName { get; set; }
    int ProviderType { get; set; }
    string SerialNumber { get; set; }
    string Subject { get; set; }
    string TimeServer { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[ENUM] SegmentType
Full Name: Autodesk.AutoCAD.DatabaseServices.SegmentType
Inherits: Enum

  Values:
    - Line = 0
    - Arc = 1
    - Coincident = 2
    - Point = 3
    - Empty = 4

--------------------------------------------------------------------------------

[ENUM] SelectType
Full Name: Autodesk.AutoCAD.DatabaseServices.SelectType
Inherits: Enum

  Values:
    - Window = 1
    - Crossing = 2

--------------------------------------------------------------------------------

[CLASS] SequenceEnd
Full Name: Autodesk.AutoCAD.DatabaseServices.SequenceEnd
Inherits: Entity

  CONSTRUCTORS:
    new SequenceEnd()

--------------------------------------------------------------------------------

[ENUM] ShadePlotResLevel
Full Name: Autodesk.AutoCAD.DatabaseServices.ShadePlotResLevel
Inherits: Enum

  Values:
    - Draft = 0
    - Preview = 1
    - Normal = 2
    - Presentation = 3
    - Maximum = 4
    - Custom = 5

--------------------------------------------------------------------------------

[ENUM] ShadePlotType
Full Name: Autodesk.AutoCAD.DatabaseServices.ShadePlotType
Inherits: Enum

  Values:
    - AsDisplayed = 0
    - Wireframe = 1
    - Hidden = 2
    - Rendered = 3
    - VisualStyle = 4
    - RenderPreset = 5

--------------------------------------------------------------------------------

[CLASS] Shape
Full Name: Autodesk.AutoCAD.DatabaseServices.Shape
Inherits: Entity

  CONSTRUCTORS:
    new Shape(Point3d position, double size, double rotation, double widthFactor)
    new Shape()

  PROPERTIES:
    string Name { get; set; }
    Vector3d Normal { get; set; }
    double Oblique { get; set; }
    Point3d Position { get; set; }
    double Rotation { get; set; }
    ObjectId ShapeIndex { get; set; }
    short ShapeNumber { get; set; }
    double Size { get; set; }
    ObjectId StyleId { get; set; }
    double Thickness { get; set; }
    double WidthFactor { get; set; }

--------------------------------------------------------------------------------

[CLASS] SkyBackground
Full Name: Autodesk.AutoCAD.DatabaseServices.SkyBackground
Inherits: Background

  CONSTRUCTORS:
    new SkyBackground()

  PROPERTIES:
    ObjectId SunId { get; set; }

  METHODS:
    DrawableType GetDrawableType()

--------------------------------------------------------------------------------

[CLASS] Solid
Full Name: Autodesk.AutoCAD.DatabaseServices.Solid
Inherits: Entity

  CONSTRUCTORS:
    new Solid(Point3d pointer1, Point3d pointer2, Point3d pointer3)
    new Solid(Point3d pointer1, Point3d pointer2, Point3d pointer3, Point3d pointer4)
    new Solid()

  PROPERTIES:
    Vector3d Normal { get; set; }
    double Thickness { get; set; }

  METHODS:
    Point3d GetPointAt(short index)
    void SetPointAt(short index, Point3d pointValue)

--------------------------------------------------------------------------------

[CLASS] Solid3d
Full Name: Autodesk.AutoCAD.DatabaseServices.Solid3d
Inherits: Entity

  CONSTRUCTORS:
    new Solid3d()

  PROPERTIES:
    double Area { get; }
    Extents3d GeometricExtents { get; }
    bool IsNull { get; }
    Solid3dMassProperties MassProperties { get; }
    int NumChanges { get; }
    bool RecordHistory { get; set; }
    bool ShowHistory { get; set; }
    bool UsesGraphicsCache { get; }

  METHODS:
    void BooleanOperation(BooleanOperationType operation, Solid3d solid)
    void ChamferEdges(SubentityId[] subentityIds, SubentityId baseSubentityId, double baseDist, double otherDist)
    bool CheckInterference(Solid3d otherSolid)
    void CleanBody()
    void ConvertToBrepAtSubentPaths(FullSubentityPath[] paths)
    Entity CopyEdge(SubentityId subEntityId)
    Entity CopyFace(SubentityId subEntityId)
    void CreateBox(double lengthAlongX, double lengthAlongY, double lengthAlongZ)
    void CreateExtrudedSolid(Entity sweepEntity, SubentityId faceSubEntityId, double height, SweepOptions sweepOptions)
    void CreateExtrudedSolid(Entity sweepEntity, SubentityId faceSubEntityId, Vector3d directionVector, SweepOptions sweepOptions)
    void CreateExtrudedSolid(Entity sweepEntity, Vector3d directionVector, SweepOptions sweepOptions)
    void CreateFrom(Entity fromEntity)
    void CreateFrustum(double height, double radiusAlongX, double radiusAlongY, double topRadius)
    void CreateLoftedSolid(LoftProfile[] crossSections, LoftProfile[] guides, LoftProfile path, LoftOptions loftOptions)
    void CreateLoftedSolid(Entity[] crossSectionCurves, Entity[] guideCurves, Entity pathCurve, LoftOptions loftOptions)
    void CreatePyramid(double height, int sides, double radius, double topRadius)
    void CreateRevolvedSolid(Entity profileEntity, SubentityId faceSubEntityId, Point3d axisPoint, Vector3d axisDir, double angleOfRevolution, double startAngle, RevolveOptions revolveOptions)
    void CreateRevolvedSolid(Entity profileEntity, Point3d axisPoint, Vector3d axisDir, double angleOfRevolution, double startAngle, RevolveOptions revolveOptions)
    void CreateSculptedSolid(Entity[] limitingBodies, IntegerCollection limitingFlags)
    void CreateSphere(double radius)
    void CreateSweptSolid(Entity sweepEntity, SubentityId faceSubEntityId, Entity pathEntity, SweepOptions sweepOptions)
    void CreateSweptSolid(Entity sweepEntity, Entity pathEntity, SweepOptions sweepOptions)
    void CreateTorus(double majorRadius, double minorRadius)
    void CreateWedge(double lengthAlongX, double lengthAlongY, double lengthAlongZ)
    void Extrude(Region region, double height, double taperAngle)
    void ExtrudeAlongPath(Region region, Curve path, double taperAngle)
    void ExtrudeFaces(SubentityId[] subentityIds, double height, double taper)
    void ExtrudeFacesAlongPath(SubentityId[] subentityIds, Curve path)
    void FilletEdges(SubentityId[] subentityIds, DoubleCollection radius, DoubleCollection startSetback, DoubleCollection endSetback)
    Region GetSection(Plane plane)
    Color GetSubentityColor(SubentityId subEntityId)
    ObjectId GetSubentityMaterial(SubentityId subEntityId)
    Mapper GetSubentityMaterialMapper(SubentityId subEntityId)
    void ImprintEntity(Entity entity)
    void OffsetBody(double offsetDistance)
    void OffsetFaces(SubentityId[] subentityIds, double offsetDistance)
    Entity[] ProjectOnToSolid(Entity entityToProject, Vector3d projectionDirection)
    void RemoveFaces(SubentityId[] subentityIds)
    void Revolve(Region region, Point3d axisPoint, Vector3d axisDir, double angleOfRevolution)
    Solid3d[] SeparateBody()
    void SetSubentityColor(SubentityId subEntityId, Color color)
    void SetSubentityMaterial(SubentityId subEntityId, ObjectId materialId)
    void SetSubentityMaterialMapper(SubentityId subEntityId, Mapper mapper)
    void ShellBody(SubentityId[] subentityIds, double offsetDistance)
    Solid3d Slice(Surface surface, bool negativeHalfToo)
    void Slice(Surface surface)
    Solid3d Slice(Plane plane, bool negativeHalfToo)
    void Slice(Plane plane)
    void StlOut(string fileName, bool asciiFormat)
    void TaperFaces(SubentityId[] subentityIds, Point3d basePoint, Vector3d draftVector, double draftAngle)
    void TransformFaces(SubentityId[] subentityIds, Matrix3d matrix)

--------------------------------------------------------------------------------

[STRUCT] Solid3dMassProperties
Full Name: Autodesk.AutoCAD.DatabaseServices.Solid3dMassProperties

  CONSTRUCTORS:
    new Solid3dMassProperties(int dummy)

  PROPERTIES:
    Point3d Centroid { get; }
    Extents3d Extents { get; }
    Vector3d MomentsOfIntertia { get; }
    Vector3d PrincipalAxes { get; }
    Vector3d PrincipalMoments { get; }
    Vector3d ProductsOfIntertia { get; }
    Vector3d RadiiOfGyration { get; }
    double Volume { get; }

--------------------------------------------------------------------------------

[CLASS] SolidBackground
Full Name: Autodesk.AutoCAD.DatabaseServices.SolidBackground
Inherits: Background

  CONSTRUCTORS:
    new SolidBackground()

  PROPERTIES:
    EntityColor Color { get; set; }

--------------------------------------------------------------------------------

[ENUM] SourceType
Full Name: Autodesk.AutoCAD.DatabaseServices.SourceType
Inherits: Enum

  Values:
    - SourceNotDefined = 0
    - InventorSource = 1
    - FusionSource = 2
    - ModelSpaceSource = 3

--------------------------------------------------------------------------------

[CLASS] Spline
Full Name: Autodesk.AutoCAD.DatabaseServices.Spline
Inherits: Curve

  CONSTRUCTORS:
    new Spline(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, KnotParameterizationEnum knotParam, int degree, double fitTolerance)
    new Spline(Point3dCollection fitPoints, bool isPeriodic, KnotParameterizationEnum knotParam, int degree, double fitTolerance)
    new Spline(Point3dCollection fitPoints, KnotParameterizationEnum knotParam, int degree, double fitTolerance)
    new Spline(Point3d center, Vector3d unitNormal, Vector3d majorAxis, double radiusRatio, double startAngle, double endAngle)
    new Spline(int degree, bool rational, bool closed, bool periodic, Point3dCollection controlPoints, DoubleCollection knots, DoubleCollection weights, double controlPointTolerance, double knotTolerance)
    new Spline(Point3dCollection point, Vector3d startTangent, Vector3d endTangent, int order, double fitTolerance)
    new Spline(Point3dCollection point, int order, double fitTolerance)
    new Spline()

  PROPERTIES:
    int Degree { get; }
    Vector3d EndFitTangent { get; }
    FitData FitData { get; set; }
    double FitTolerance { get; set; }
    bool HasFitData { get; }
    bool IsNull { get; }
    bool IsPlanar { get; }
    bool IsRational { get; }
    int NumControlPoints { get; }
    int NumFitPoints { get; }
    NurbsData NurbsData { get; set; }
    Vector3d StartFitTangent { get; }
    SplineType Type { get; set; }

  METHODS:
    void ElevateDegree(int newDegree)
    Point3d GetControlPointAt(int index)
    Point3d GetFitPointAt(int index)
    ErrorStatus InsertControlPointAt(double knotParam, Point3d controlPoint, double weight)
    void InsertFitPointAt(int index, Point3d point)
    void InsertKnot(double value)
    ErrorStatus ModifyPositionAndTangent(double param, Point3d point, Vector3d deriv)
    void PurgeFitData()
    ErrorStatus Rebuild(int degree, int numCtrlPts)
    ErrorStatus RemoveControlPointAt(int index)
    void RemoveFitPointAt(int index)
    void SetControlPointAt(int index, Point3d point)
    void SetFitPointAt(int index, Point3d point)
    void SetWeightAt(int index, double weight)
    Curve ToPolyline(uint numOfVertices, bool bConvertAsArcs, bool bToLWPolyline)
    Curve ToPolyline(bool bConvertAsArcs, bool bToLWPolyline)
    Curve ToPolyline(uint numOfVertices)
    Curve ToPolyline()
    Curve ToPolylineWithPrecision(int precision, bool bConvertAsArcs, bool bToLWPolyline)
    Curve ToPolylineWithPrecision(int precision)
    void UpdateFitData()
    double WeightAt(int index)

--------------------------------------------------------------------------------

[ENUM] SplineType
Full Name: Autodesk.AutoCAD.DatabaseServices.SplineType
Inherits: Enum

  Values:
    - FitPoints = 0
    - ControlPoints = 1

--------------------------------------------------------------------------------

[ENUM] StandardScaleType
Full Name: Autodesk.AutoCAD.DatabaseServices.StandardScaleType
Inherits: Enum

  Values:
    - ScaleToFit = 0
    - CustomScale = 1
    - Scale1To1 = 2
    - Scale1To2 = 3
    - Scale1To4 = 4
    - Scale1To5 = 5
    - Scale1To8 = 6
    - Scale1To10 = 7
    - Scale1To16 = 8
    - Scale1To20 = 9
    - Scale1To30 = 10
    - Scale1To40 = 11
    - Scale1To50 = 12
    - Scale1To100 = 13
    - Scale2To1 = 14
    - Scale4To1 = 15
    - Scale8To1 = 16
    - Scale10To1 = 17
    - Scale100To1 = 18
    - Scale1To128inAnd1ft = 19
    - Scale1To64inchAnd1ft = 20
    - Scale1To32inchAnd1ft = 21
    - Scale1To16inchAnd1ft = 22
    - Scale3To32inchAnd1ft = 23
    - Scale1To8inchAnd1ft = 24
    - Scale3To16inchAnd1ft = 25
    - Scale1To4inchAnd1ft = 26
    - Scale3To8inchAnd1ft = 27
    - Scale1To2inchAnd1ft = 28
    - Scale3To4inchAnd1ft = 29
    - Scale1inAnd1ft = 30
    - Scale3inAnd1ft = 32
    - Scale6inAnd1ft = 33
    - Scale1ftAnd1ft = 34

--------------------------------------------------------------------------------

[ENUM] StdScaleType
Full Name: Autodesk.AutoCAD.DatabaseServices.StdScaleType
Inherits: Enum

  Values:
    - ScaleToFit = 0
    - StdScale1To128InchIs1ft = 1
    - StdScale1To64InchIs1ft = 2
    - StdScale1To32InchIs1ft = 3
    - StdScale1To16InchIs1ft = 4
    - StdScale3To32InchIs1ft = 5
    - StdScale1To8InchIs1ft = 6
    - StdScale3To16InchIs1ft = 7
    - StdScale1To4InchIs1ft = 8
    - StdScale3To8InchIs1ft = 9
    - StdScale1To2InchIs1ft = 10
    - StdScale3To4InchIs1ft = 11
    - StdScale1InchIs1ft = 12
    - StdScale3InchIs1ft = 13
    - StdScale6InchIs1ft = 14
    - StdScale1ftIs1ft = 15
    - StdScale1To1 = 16
    - StdScale1To2 = 17
    - StdScale1To4 = 18
    - StdScale1To5 = 19
    - StdScale1To8 = 20
    - StdScale1To10 = 21
    - StdScale1To16 = 22
    - StdScale1To20 = 23
    - StdScale1To30 = 24
    - StdScale1To40 = 25
    - StdScale1To50 = 26
    - StdScale1To100 = 27
    - StdScale2To1 = 28
    - StdScale4To1 = 29
    - StdScale8To1 = 30
    - StdScale10To1 = 31
    - StdScale100To1 = 32
    - StdScale1000To1 = 33

--------------------------------------------------------------------------------

[CLASS] SubDMesh
Full Name: Autodesk.AutoCAD.DatabaseServices.SubDMesh
Inherits: Entity

  CONSTRUCTORS:
    new SubDMesh()

  PROPERTIES:
    Int32Collection EdgeArray { get; }
    Int32Collection FaceArray { get; }
    Vector3dCollection NormalArray { get; }
    int NumberOfEdges { get; }
    int NumberOfFaces { get; }
    int NumberOfSubDividedFaces { get; }
    int NumberOfSubDividedVertices { get; }
    int NumberOfVertices { get; }
    int SmoothLevel { get; }
    Int32Collection SubDividedFaceArray { get; }
    Vector3dCollection SubDividedNormalArray { get; }
    Point3dCollection SubDividedVertices { get; }
    EntityColor[] VertexColorArray { get; set; }
    Vector3dCollection VertexNormalArray { get; set; }
    Point3dCollection VertexTextureArray { get; set; }
    Point3dCollection Vertices { get; }
    bool Watertight { get; }

  METHODS:
    void Cap(FullSubentityPath[] edges)
    void Collapse(SubentityId subentityId)
    double ComputeSurfaceArea()
    double ComputeVolume()
    Solid3d ConvertToSolid(bool convertAsSmooth, bool optimize)
    Surface ConvertToSurface(bool convertAsSmooth, bool optimize)
    Surface ConvertToSurface(bool convertAsSmooth, SubentityId subentId)
    void ExtrudeConnectedFaces(FullSubentityPath[] paths, Point3dCollection alongPath, double taper)
    void ExtrudeConnectedFaces(FullSubentityPath[] paths, double length, Vector3d dir, double taper)
    void ExtrudeFaces(FullSubentityPath[] paths, Point3dCollection alongPath, double taper)
    void ExtrudeFaces(FullSubentityPath[] paths, double length, Vector3d dir, double taper)
    FullSubentityPath[] GetAdjacentSubentPath(FullSubentityPath path, SubentityType type)
    DoubleCollection GetCrease(FullSubentityPath[] subentPaths)
    double GetCrease(SubentityId id)
    Plane GetFacePlane(SubentityId subentId)
    int GetNumberOfSubDividedFacesAt(FullSubentityPath[] paths)
    static MeshDataCollection GetObjectMesh(DBObject dbObj, MeshFaceterData faceterData)
    Point3d GetSubDividedVertexAt(SubentityId subentId)
    Point3d GetSubDividedVertexAt(int index)
    Color GetSubentColor(SubentityId subentId)
    ObjectId GetSubentMaterial(SubentityId subentId)
    Mapper GetSubentMaterialMapper(SubentityId subentId)
    FullSubentityPath[] GetSubentPath(int index, SubentityType type)
    Point3d GetVertexAt(SubentityId subentId)
    Point3d GetVertexAt(int index)
    void MergeFaces(FullSubentityPath[] faces)
    void Setbox(double xlen, double ylen, double zlen, int divx, int divy, int divz, int smoothLevel)
    void SetCone(double majorRadius, double minorRadius, double height, int divAxis, int divHeight, int divCap, double radiusRatio, int smoothLevel)
    void SetCrease(FullSubentityPath[] subentPaths, double creaseVal)
    void SetCrease(double creaseVal)
    void SetCylinder(double majorRadius, double minorRadius, double height, int divAxis, int divHeight, int divCap, int smoothLevel)
    void SetDragStatus(DragStatus status)
    void SetPyramid(double radius, double height, int divLength, int divHeight, int divCap, int nSides, double radiusRatio, int smoothLevel)
    void SetSphere(double radius, int divAxis, int divHeight, int smoothLevel)
    void SetSubDMesh(Point3dCollection vertexArray, Int32Collection indexArray, int smoothLevel)
    void SetSubentColor(SubentityId subentId, Color color)
    void SetSubentMaterial(SubentityId subentId, ObjectId matId)
    void SetSubentMaterialMapper(SubentityId subentId, Mapper mapper)
    void SetTorus(double majorRadius, int divSection, int divSweepPath, double sectionRadiusRatio, double sectionRotate, int smoothLevel)
    void SetVertexAt(SubentityId subentId, Point3d position)
    void SetVertexAt(int index, Point3d position)
    void SetWedge(double xLen, double yLen, double zLen, int divLength, int divWidth, int divHeight, int divSlope, int divCap, int smoothLevel)
    void Spin(SubentityId subentFaceId)
    void SplitFace(SubentityId subentFaceId, SubentityId subent0, Point3d point0, SubentityId subent1, Point3d point1)
    void SubdDivideDown()
    void SubdDivideUp()
    void SubdRefine(FullSubentityPath[] paths)
    void SubdRefine()

--------------------------------------------------------------------------------

[CLASS] SubentityGeometry
Full Name: Autodesk.AutoCAD.DatabaseServices.SubentityGeometry

  CONSTRUCTORS:
    new SubentityGeometry(Curve3d curve)
    new SubentityGeometry(Point3d pnt)
    new SubentityGeometry()

  PROPERTIES:
    Curve3d Curve { get; }
    Point3d Point { get; }
    SubentityType Type { get; }

--------------------------------------------------------------------------------

[STRUCT] SubentityId
Full Name: Autodesk.AutoCAD.DatabaseServices.SubentityId

  CONSTRUCTORS:
    new SubentityId(RXClass classType, IntPtr i)
    new SubentityId(SubentityType type, IntPtr i)
    new SubentityId(RXClass classType, long i)
    new SubentityId(SubentityType type, long i)

  PROPERTIES:
    int Index { get; }
    IntPtr IndexPtr { get; }
    static SubentityId Null { get; }
    SubentityType Type { get; }
    RXClass TypeClass { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SubentityOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.SubentityOverrule
Inherits: Overrule

  METHODS:
    void AddSubentPaths(Entity entity, FullSubentityPath[] paths)
    void DeleteSubentPaths(Entity entity, FullSubentityPath[] paths)
    Matrix3d GetCompoundObjectTransform(Entity entity)
    void GetGripPointsAtSubentPath(Entity entity, FullSubentityPath subPath, GripDataCollection grips, double curViewUnitSize, int gripSize, Vector3d curViewDir, GetGripPointsFlags bitFlags)
    IntPtr[] GetGsMarkersAtSubentPath(Entity entity, FullSubentityPath subPath)
    Guid GetSubentClassId(Entity entity, FullSubentityPath path)
    Extents3d GetSubentPathGeomExtents(Entity entity, FullSubentityPath path)
    FullSubentityPath[] GetSubentPathsAtGsMarker(Entity entity, SubentityType type, IntPtr gsMark, Point3d pickPoint, Matrix3d viewXform, ObjectId[] entAndInsertStack)
    void MoveGripPointsAtSubentPaths(Entity entity, FullSubentityPath[] path, GripDataCollection grips, Vector3d offset, MoveGripPointsFlags bitFlags)
    void OnSubentGripStatusChanged(Entity entity, GripStatus status, FullSubentityPath subentity)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)
    Entity SubentPtr(Entity entity, FullSubentityPath id)
    void TransformSubentPathsBy(Entity entity, FullSubentityPath[] paths, Matrix3d xform)

--------------------------------------------------------------------------------

[ENUM] SubentityType
Full Name: Autodesk.AutoCAD.DatabaseServices.SubentityType
Inherits: Enum

  Values:
    - Null = 0
    - Face = 1
    - Edge = 2
    - Vertex = 3
    - MlineCache = 4
    - Class = 5

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SubentRef
Full Name: Autodesk.AutoCAD.DatabaseServices.SubentRef
Inherits: GeomRef

  PROPERTIES:
    CompoundObjectId Entity { get; }
    SubentityId SubentId { get; }

  METHODS:
    void CopyFrom(SubentRef source)
    Entity CreateEntity()

--------------------------------------------------------------------------------

[CLASS] Sun
Full Name: Autodesk.AutoCAD.DatabaseServices.Sun
Inherits: DBObject

  CONSTRUCTORS:
    new Sun()

  PROPERTIES:
    double Altitude { get; set; }
    double Azimuth { get; set; }
    DateTime DateTime { get; set; }
    double Intensity { get; set; }
    bool IsDaylightSavingsOn { get; set; }
    bool IsOn { get; set; }
    ShadowParameters ShadowParameters { get; set; }
    SkyParameters SkyParameters { get; set; }
    Color SunColor { get; set; }
    Vector3d SunDirection { get; set; }

--------------------------------------------------------------------------------

[CLASS] Surface
Full Name: Autodesk.AutoCAD.DatabaseServices.Surface
Inherits: Entity

  CONSTRUCTORS:
    new Surface()

  PROPERTIES:
    ObjectId CreationActionBodyId { get; }
    Extents3d GeometricExtents { get; }
    ObjectIdCollection ModificationActionBodyIds { get; }
    double Perimeter { get; }
    short UIsoLineDensity { get; set; }
    bool UsesGraphicsCache { get; }
    short VIsoLineDensity { get; set; }
    WireframeTypeEnum WireframeType { get; set; }

  METHODS:
    Entity[] BooleanIntersect(Solid3d solid)
    Entity[] BooleanIntersect(Surface surface2)
    Surface BooleanSubtract(Solid3d solid)
    Surface BooleanSubtract(Surface surface2)
    Surface BooleanUnion(Surface surface2)
    void ChamferEdges(SubentityId[] subentityIds, SubentityId baseSubentityId, double baseDist, double otherDist, bool associativeEnabled)
    void ChamferEdges(SubentityId[] edgeSubentIds, SubentityId baseFaceSubentId, double baseDist, double otherDist)
    NurbSurface[] ConvertToNurbSurface()
    Entity[] ConvertToRegion()
    static Surface CreateBlendSurface(LoftProfile startProfile, LoftProfile endProfile, BlendOptions blendOptionsd)
    static ObjectId CreateBlendSurface(LoftProfile startProfile, LoftProfile endProfile, BlendOptions blendOptions, bool associativeEnabled)
    static ObjectId CreateExtendSurface(ObjectId sourceSurface, SubentityId[] edges, double extension, EdgeExtensionType extOption, bool associativeEnabled)
    static ExtrudedSurface CreateExtrudedSurface(Profile3d pSweep, Vector3d directionVec, SweepOptions sweepOptions)
    static ObjectId CreateExtrudedSurface(Profile3d pSweep, Vector3d directionVec, SweepOptions sweepOptions, bool associativeEnabled)
    static Surface CreateFilletSurface(ObjectId surfId1, Point3d pickPt1, ObjectId surfId2, Point3d pickPt2, double radius, FilletTrimMode trimMode, Vector3d projDir)
    static ObjectId CreateFilletSurface(ObjectId surfId1, Point3d pickPt1, ObjectId surfId2, Point3d pickPt2, double radius, FilletTrimMode trimMode, Vector3d projDir, bool associativeEnabled)
    static Surface CreateFrom(Entity fromEntity)
    Entity[] CreateInterferenceObjects(Entity ent, int flags)
    static Surface CreateLoftedSurface(LoftProfile[] crossSections, LoftProfile[] guides, LoftProfile path, LoftOptions loftOptions)
    static ObjectId CreateLoftedSurface(LoftProfile[] crossSections, LoftProfile[] guides, LoftProfile path, LoftOptions loftOptions, bool associativeEnabled)
    static Surface CreateNetworkSurface(Profile3d[] uProfiles, Profile3d[] vProfiles)
    static ObjectId CreateNetworkSurface(Profile3d[] uProfiles, Profile3d[] vProfiles, bool associativeEnabled)
    static Entity CreateOffsetSurface(Entity entity, double offsetDistance)
    static ObjectId CreateOffsetSurface(Entity entity, double offsetDistance, bool associativeEnabled)
    static Surface CreatePatchSurface(PathRef inputPathRef, ObjectIdCollection constraintIds, int continuity, double bulge)
    static ObjectId CreatePatchSurface(PathRef inputPathRef, ObjectIdCollection constraintIds, int continuity, double bulge, bool associativeEnabled)
    static RevolvedSurface CreateRevolvedSurface(Profile3d rev, Profile3d axisEnt, bool flipAxisDirection, double revAngle, double startAngle, RevolveOptions revolveOptions)
    static ObjectId CreateRevolvedSurface(Profile3d rev, Profile3d axisEnt, bool flipAxisDirection, double revAngle, double startAngle, RevolveOptions revolveOptions, bool associativeEnabled)
    static RevolvedSurface CreateRevolvedSurface(Profile3d rev, Point3d axisPnt, Vector3d axisDir, double revAngle, double startAngle, RevolveOptions revolveOptions)
    static ObjectId CreateRevolvedSurface(Profile3d rev, Point3d axisPnt, Vector3d axisDir, double revAngle, double startAngle, RevolveOptions revolveOptions, bool associativeEnabled)
    Entity[] CreateSectionObjects(Plane sectionPlane)
    static SweptSurface CreateSweptSurface(Profile3d sweep, Profile3d path, SweepOptions sweepOptions)
    static ObjectId CreateSweptSurface(Profile3d sweep, Profile3d path, SweepOptions sweepOptions, bool associativeEnabled)
    void ExtendEdges(FullSubentityPath[] edges, double extension, EdgeExtensionType extOption, bool associativeEnabled)
    void FilletEdges(SubentityId[] subentityIds, double radius, bool associativeEnabled)
    void FilletEdges(SubentityId[] subentityIds, DoubleCollection radius, DoubleCollection startSetback, DoubleCollection endSetback)
    double GetArea()
    Color GetSubentityColor(SubentityId subEntityId)
    ObjectId GetSubentityMaterial(SubentityId subEntityId)
    Mapper GetSubentityMaterialMapper(SubentityId subEntityId)
    void ImprintEntity(Entity entityToImprint)
    Entity[] ProjectOnToSurface(Entity entityToProject, Vector3d projectionDirection)
    void RayTest(Point3d rayBasePoint, Vector3d rayDir, double rayRadius, out SubentityId[] subEntIds, out DoubleCollection parameters)
    void SetSubentityColor(SubentityId subEntityId, Color color)
    void SetSubentityMaterial(SubentityId subEntityId, ObjectId materialId)
    void SetSubentityMaterialMapper(SubentityId subEntityId, Mapper mapper)
    SurfaceSliceResults SliceByPlane(Plane slicePlane)
    SurfaceSliceResults SliceBySurface(Surface slicingSurface)
    Solid3d Thicken(double thickness, bool bothSides)
    static void TrimSurface(ObjectId blankSurfaceId, ObjectIdCollection toolSurfaceIds, ObjectIdCollection toolCurveIds, Vector3dCollection projVectors, Point3d pickPoint, Vector3d viewVector, bool bAutoExtend, bool associativeEnabled)

--------------------------------------------------------------------------------

[STRUCT] SurfaceSliceResults
Full Name: Autodesk.AutoCAD.DatabaseServices.SurfaceSliceResults

  CONSTRUCTORS:
    new SurfaceSliceResults(Surface negativeHalfSurface, Surface newSurface)

  PROPERTIES:
    Surface NegativeHalfSurface { get; set; }
    Surface NewSurface { get; set; }

--------------------------------------------------------------------------------

[CLASS] SurfaceTrimInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.SurfaceTrimInfo
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new SurfaceTrimInfo()

  PROPERTIES:
    bool IsCurve { get; }
    Vector3d ProjVector { get; set; }
    TrimRelation Relation { get; set; }
    CompoundObjectId ToolBodyId { get; set; }

  METHODS:
    void SetTrimInfo(CompoundObjectId surfaceId, TrimRelation relation, SubentityId subEntityId)
    void SetTrimInfo(CompoundObjectId curveId, Vector3d projVector, TrimRelation relation)

--------------------------------------------------------------------------------

[CLASS] SweepOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.SweepOptions
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new SweepOptions(SweepOptions opts)
    new SweepOptions()

  PROPERTIES:
    SweepOptionsAlignOption Align { get; }
    double AlignAngle { get; }
    bool AlignStart { get; }
    bool Bank { get; }
    Point3d BasePoint { get; }
    bool CheckIntersections { get; }
    double DraftAngle { get; }
    double EndDraftDist { get; }
    Matrix3d PathEntityTransform { get; }
    double ScaleFactor { get; }
    double StartDraftDist { get; }
    Matrix3d SweepEntityTransform { get; }
    double TwistAngle { get; }

  METHODS:
    void CheckPathCurve(Entity pathEnt, bool displayErrorMessages)
    SweepOptionsCheckSweepCurveOut CheckSweepCurve(Entity sweepEnt, bool displayErrorMessages)
    object Clone()

--------------------------------------------------------------------------------

[ENUM] SweepOptionsAlignOption
Full Name: Autodesk.AutoCAD.DatabaseServices.SweepOptionsAlignOption
Inherits: Enum

  Values:
    - NoAlignment = 0
    - AlignSweepEntityToPath = 1
    - TranslateSweepEntityToPath = 2
    - TranslatePathToSweepEntity = 3

--------------------------------------------------------------------------------

[CLASS] SweepOptionsBuilder
Full Name: Autodesk.AutoCAD.DatabaseServices.SweepOptionsBuilder

  CONSTRUCTORS:
    new SweepOptionsBuilder(SweepOptions value)
    new SweepOptionsBuilder()

  PROPERTIES:
    SweepOptionsAlignOption Align { get; set; }
    double AlignAngle { get; set; }
    bool AlignStart { get; set; }
    bool Bank { get; set; }
    Point3d BasePoint { get; set; }
    bool CheckIntersections { get; set; }
    double DraftAngle { get; set; }
    double EndDraftDist { get; set; }
    Matrix3d PathEntityTransform { get; set; }
    double ScaleFactor { get; set; }
    double StartDraftDist { get; set; }
    Matrix3d SweepEntityTransform { get; set; }
    double TwistAngle { get; set; }

  METHODS:
    void SetPathEntityTransform(Entity pathEnt, bool displayErrorMessages)
    void SetSweepEntityTransform(Entity[] sweepEntities, bool displayErrorMessages)
    SweepOptions ToSweepOptions()

--------------------------------------------------------------------------------

[CLASS] SweepOptionsCheckSweepCurveOut
Full Name: Autodesk.AutoCAD.DatabaseServices.SweepOptionsCheckSweepCurveOut

  CONSTRUCTORS:
    new SweepOptionsCheckSweepCurveOut()

  PROPERTIES:
    double ApproximateArcLength { get; }
    bool Closed { get; }
    Planarity Planarity { get; }
    Point3d Point { get; }
    Vector3d Vector { get; }

--------------------------------------------------------------------------------

[CLASS] SweptSurface
Full Name: Autodesk.AutoCAD.DatabaseServices.SweptSurface
Inherits: Surface

  CONSTRUCTORS:
    new SweptSurface()

  PROPERTIES:
    bool Bank { get; set; }
    Entity PathEntity { get; }
    double PathLength { get; }
    Profile3d PathProfile { get; }
    double ProfileRotation { get; set; }
    double ScaleAlongPath { get; set; }
    Entity SweepEntity { get; }
    SweepOptions SweepOptions { get; set; }
    Profile3d SweepProfile { get; }
    double TwistAlongPath { get; set; }

  METHODS:
    void CreateSweptSurface(Entity sweepEnt, Entity pathEnt, SweepOptions sweepOptions)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SymbolTable
Full Name: Autodesk.AutoCAD.DatabaseServices.SymbolTable
Inherits: DBObject
Implements: IEnumerable

  PROPERTIES:
    SymbolTable IncludingErased { get; }
    ObjectId Item { get; }

  METHODS:
    ObjectId Add(SymbolTableRecord value)
    SymbolTableEnumerator GetEnumerator()
    bool Has(ObjectId id)
    bool Has(string key)

--------------------------------------------------------------------------------

[CLASS] SymbolTableEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.SymbolTableEnumerator
Inherits: DisposableWrapper
Implements: IEnumerator

  PROPERTIES:
    ObjectId Current { get; }

  METHODS:
    object IEnumerator.get_Current()
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SymbolTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.SymbolTableRecord
Inherits: DBObject

  PROPERTIES:
    bool IsDependent { get; }
    bool IsResolved { get; }
    string Name { get; set; }

--------------------------------------------------------------------------------

[CLASS] SymbolUtilityServices
Full Name: Autodesk.AutoCAD.DatabaseServices.SymbolUtilityServices

  CONSTRUCTORS:
    new SymbolUtilityServices()

  PROPERTIES:
    static string BlockModelSpaceName { get; }
    static string BlockPaperSpaceName { get; }
    static string LayerDefpointsName { get; }
    static string LayerZeroName { get; }
    static string LinetypeByBlockName { get; }
    static string LinetypeByLayerName { get; }
    static string LinetypeContinuousName { get; }
    static string RegAppAcadName { get; }
    static string TextStyleStandardName { get; }
    static string ViewportActiveName { get; }

  METHODS:
    static ObjectId GetBlockModelSpaceId(Database databasePointer)
    static string GetBlockNameFromInsertPathName(string pathName)
    static ObjectId GetBlockPaperSpaceId(Database databasePointer)
    static string GetInsertPathNameFromBlockName(string blockName)
    static ObjectId GetLayerDefpointsId(Database database)
    static ObjectId GetLayerZeroId(Database database)
    static ObjectId GetLinetypeByBlockId(Database databasePointer)
    static ObjectId GetLinetypeByLayerId(Database databasePointer)
    static ObjectId GetLinetypeContinuousId(Database databasePointer)
    static int GetMaxSymbolNameLength(bool isNewName, bool compatibilityMode)
    static string GetPathNameFromSymbolName(string symbolName, string extensions)
    static ObjectId GetRegAppAcadId(Database databasePointer)
    static string GetSymbolNameFromPathName(string pathName, string extensions)
    static ObjectId GetTextStyleStandardId(Database databasePointer)
    static bool IsBlockLayoutName(string name)
    static bool IsBlockModelSpaceName(string name)
    static bool IsBlockPaperSpaceName(string name)
    static bool IsCompatibilityMode(Database database)
    static bool IsLayerDefpointsName(string name)
    static bool IsLayerZeroName(string name)
    static bool IsLinetypeByBlockName(string name)
    static bool IsLinetypeByLayerName(string name)
    static bool IsLinetypeContinuousName(string name)
    static bool IsRegAppAcadName(string name)
    static bool IsTextStyleStandardName(string name)
    static bool IsViewportActiveName(string name)
    static string MakeDependentName(string dwgName, string symbolName)
    static string PreValidateSymbolName(string symbolName, bool preserveCase)
    static string RepairPreExtendedSymbolName(string oldName, bool allowVerticalBar)
    static string RepairSymbolName(string oldName, bool allowVerticalBar)
    static ErrorStatus ValidateCompatibleSymbolName(string name, bool isNewName, bool allowVerticalBar, bool compatibilityMode)
    static void ValidatePreExtendedSymbolName(string name, bool allowVerticalBar)
    static void ValidateSymbolName(string name, bool allowVerticalBar)

--------------------------------------------------------------------------------

[CLASS] SymmetricConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.SymmetricConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new SymmetricConstraint()

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.SystemVariableChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    bool Changed { get; }
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.SystemVariableChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SystemVariableChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SystemVariableChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SystemVariableChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangingEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.SystemVariableChangingEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangingEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.SystemVariableChangingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SystemVariableChangingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SystemVariableChangingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SystemVariableChangingEventArgs e)

--------------------------------------------------------------------------------

