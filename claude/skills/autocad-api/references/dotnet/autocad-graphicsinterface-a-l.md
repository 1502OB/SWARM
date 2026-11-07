# Autodesk.AutoCAD.GraphicsInterface

NAMESPACE: Autodesk.AutoCAD.GraphicsInterface
--------------------------------------------------------------------------------

[ENUM] ArcType
Full Name: Autodesk.AutoCAD.GraphicsInterface.ArcType
Inherits: Enum

  Values:
    - ArcSimple = 0
    - ArcSector = 1
    - ArcChord = 2

--------------------------------------------------------------------------------

[ENUM] AttenuationType
Full Name: Autodesk.AutoCAD.GraphicsInterface.AttenuationType
Inherits: Enum

  Values:
    - None = 0
    - InverseLinear = 1
    - InverseSquare = 2

--------------------------------------------------------------------------------

[ENUM] AutoTransform
Full Name: Autodesk.AutoCAD.GraphicsInterface.AutoTransform
Inherits: Enum

  Values:
    - InheritAutoTransform = 0
    - None = 1
    - TransformObject = 2
    - Model = 4

--------------------------------------------------------------------------------

[ENUM] ChannelFlags
Full Name: Autodesk.AutoCAD.GraphicsInterface.ChannelFlags
Inherits: Enum

  Values:
    - None = 0
    - UseDiffuse = 1
    - UseSpecular = 2
    - UseReflection = 4
    - UseOpacity = 8
    - UseBump = 16
    - UseRefraction = 32
    - UseAll = 63

--------------------------------------------------------------------------------

[CLASS] ClipBoundary
Full Name: Autodesk.AutoCAD.GraphicsInterface.ClipBoundary
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new ClipBoundary()

  PROPERTIES:
    double BackClipZ { get; set; }
    bool ClippingBack { get; set; }
    bool ClippingFront { get; set; }
    bool DrawBoundary { get; set; }
    double FrontClipZ { get; set; }
    Vector3d NormalVector { get; set; }
    Point3d Point { get; set; }
    Matrix3d TransformInverseBlockRefXForm { get; set; }
    Matrix3d TransformToClipSpace { get; set; }

  METHODS:
    Point2dCollection GetAptPoints()
    void SetAptPoints(Point2dCollection point)

--------------------------------------------------------------------------------

[STRUCT] ColorRGB
Full Name: Autodesk.AutoCAD.GraphicsInterface.ColorRGB

  CONSTRUCTORS:
    new ColorRGB(double red, double green, double blue)
    new ColorRGB()

  PROPERTIES:
    double Blue { get; set; }
    double Green { get; set; }
    double Red { get; set; }

--------------------------------------------------------------------------------

[STRUCT] ColorRGBA
Full Name: Autodesk.AutoCAD.GraphicsInterface.ColorRGBA

  CONSTRUCTORS:
    new ColorRGBA(double red, double green, double blue, double alpha)
    new ColorRGBA(double red, double green, double blue)
    new ColorRGBA()

  PROPERTIES:
    double Alpha { get; set; }
    double Blue { get; set; }
    double Green { get; set; }
    double Red { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] CommonDraw
Full Name: Autodesk.AutoCAD.GraphicsInterface.CommonDraw
Inherits: RXObject

  PROPERTIES:
    Context Context { get; }
    bool IsDragging { get; }
    int NumberOfIsolines { get; }
    Geometry RawGeometry { get; }
    bool RegenAbort { get; }
    RegenType RegenType { get; }
    SubEntityTraits SubEntityTraits { get; }

  METHODS:
    double Deviation(DeviationType deviationType, Point3d pointOnCurve)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Context
Full Name: Autodesk.AutoCAD.GraphicsInterface.Context
Inherits: RXObject

  PROPERTIES:
    LineWeight ByBlockLineWeight { get; }
    ObjectId ByBlockPlotStyleNameId { get; }
    Database Database { get; }
    EntityColor EffectiveColor { get; }
    bool IsBoundaryClipping { get; }
    bool IsNesting { get; }
    bool IsPlotGeneration { get; }
    bool SupportsCurveTrait { get; }
    bool SupportsTrueTypeText { get; }

  METHODS:
    void DisableFastMoveDrag()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ContextualColors
Full Name: Autodesk.AutoCAD.GraphicsInterface.ContextualColors
Inherits: RXObject

  PROPERTIES:
    int CameraClipping { get; }
    int CameraFrustrum { get; }
    int CameraGlyphs { get; }
    int GridAxisLines { get; }
    int GridAxisLineTintXYZ { get; }
    int GridMajorLines { get; }
    int GridMajorLineTintXYZ { get; }
    int GridMinorLines { get; }
    int GridMinorLineTintXYZ { get; }
    Color LightDistanceColor { get; }
    int LightEndLimit { get; }
    int LightFalloff { get; }
    int LightGlyphs { get; }
    int LightHotspot { get; }
    Color LightShapeColor { get; }
    int LightStartLimit { get; }
    Color WebMeshColor { get; }
    Color WebMeshMissingColor { get; }

  METHODS:
    bool FlagsSet(int flag)
    void SetContextFlags(int flag, bool set)

--------------------------------------------------------------------------------

[ENUM] DefaultLightingType
Full Name: Autodesk.AutoCAD.GraphicsInterface.DefaultLightingType
Inherits: Enum

  Values:
    - OneDistantLight = 0
    - TwoDistantLights = 1

--------------------------------------------------------------------------------

[ENUM] DeviationType
Full Name: Autodesk.AutoCAD.GraphicsInterface.DeviationType
Inherits: Enum

  Values:
    - MaxDevForCircle = 0
    - MaxDevForCurve = 1
    - MaxDevForBoundary = 2
    - MaxDevForIsoline = 3
    - MaxDevForFacet = 4
    - MaxDevForHatch = 5

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DistantLightTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.DistantLightTraits
Inherits: StandardLightTraits

  CONSTRUCTORS:
    new DistantLightTraits()

  PROPERTIES:
    bool IsSunlight { get; set; }
    ColorRGB LampColor { get; set; }
    Vector3d LightDirection { get; set; }
    double PhysicalIntensity { get; set; }
    SkyParameters SkyParameters { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Drawable
Full Name: Autodesk.AutoCAD.GraphicsInterface.Drawable
Inherits: RXObject

  PROPERTIES:
    Nullable<Extents3d> Bounds { get; }
    DrawableType DrawableType { get; }
    DrawStream DrawStream { get; set; }
    ObjectId Id { get; }
    bool IsPersistent { get; }

  METHODS:
    int SetAttributes(DrawableTraits traits)
    void ViewportDraw(ViewportDraw vd)
    int ViewportDrawLogicalFlags(ViewportDraw vd)
    bool WorldDraw(WorldDraw wd)

--------------------------------------------------------------------------------

[ENUM] DrawableAttributes
Full Name: Autodesk.AutoCAD.GraphicsInterface.DrawableAttributes
Inherits: Enum

  Values:
    - None = 0
    - IsAnEntity = 1
    - UsesNesting = 2
    - IsCompoundObject = 4
    - ViewIndependentViewportDraw = 8
    - IsInvisible = 16
    - HasAttributes = 32
    - RegenTypeDependentGeometry = 64
    - IsDimension = 133
    - RegenDraw = 256
    - StandardDisplaySingleLOD = 512
    - ShadedDisplaySingleLOD = 1024
    - ViewDependentViewportDraw = 2048
    - BlockDependentViewportDraw = 4096
    - IsExternalReference = 8192
    - NotPlottable = 16384

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DrawableOverrule
Full Name: Autodesk.AutoCAD.GraphicsInterface.DrawableOverrule
Inherits: Overrule

  METHODS:
    int SetAttributes(Drawable drawable, DrawableTraits traits)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)
    void ViewportDraw(Drawable drawable, ViewportDraw vd)
    int ViewportDrawLogicalFlags(Drawable drawable, ViewportDraw vd)
    bool WorldDraw(Drawable drawable, WorldDraw wd)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DrawableTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.DrawableTraits
Inherits: SubEntityTraits

  PROPERTIES:
    Linetype LinePattern { get; set; }
    SelectionFlags SelectionFlags { get; set; }

  METHODS:
    void AddLight(ObjectId lightId)
    object GetHighlightProperty(HighlightProperty propertyType)
    void SetHighlightProperty(HighlightProperty propertyType, object value)
    void SetupForEntity(Entity entity)

--------------------------------------------------------------------------------

[ENUM] DrawableType
Full Name: Autodesk.AutoCAD.GraphicsInterface.DrawableType
Inherits: Enum

  Values:
    - Geometry = 0
    - DistantLight = 1
    - PointLight = 2
    - SpotLight = 3
    - AmbientLight = 4
    - SolidBackground = 5
    - GradientBackground = 6
    - ImageBackground = 7
    - GroundPlaneBackground = 8
    - Viewport = 9
    - WebLight = 10
    - SkyBackground = 11
    - ImageBasedLightingBackground = 12

--------------------------------------------------------------------------------

[ENUM] DrawFlags
Full Name: Autodesk.AutoCAD.GraphicsInterface.DrawFlags
Inherits: Enum

  Values:
    - None = 0
    - DrawBackfaces = 1
    - DrawNoLineWeight = 32
    - DrawFillTextBoundaryStart = 256
    - DrawFillTextBoundaryEnd = 512

--------------------------------------------------------------------------------

[CLASS] DrawStream
Full Name: Autodesk.AutoCAD.GraphicsInterface.DrawStream
Inherits: Drawable

  CONSTRUCTORS:
    new DrawStream(Drawable owner)
    new DrawStream()

  PROPERTIES:
    bool IsValid { get; }
    Drawable Owner { get; set; }

--------------------------------------------------------------------------------

[CLASS] EdgeData
Full Name: Autodesk.AutoCAD.GraphicsInterface.EdgeData

  CONSTRUCTORS:
    new EdgeData()

  METHODS:
    short[] GetColors()
    ObjectId[] GetLayers()
    ObjectId[] GetLineTypes()
    IntPtr[] GetSelectionMarkers()
    EntityColor[] GetTrueColors()
    byte[] GetVisibility()
    void SetColors(short[] colors)
    void SetLayers(ObjectId[] layers)
    void SetLineTypes(ObjectId[] lineTypes)
    void SetSelectionMarkers(IntPtr[] selectionMarkers)
    void SetTrueColors(EntityColor[] colors)
    void SetVisibility(byte[] visibility)

--------------------------------------------------------------------------------

[ENUM] ExtendedLightShape
Full Name: Autodesk.AutoCAD.GraphicsInterface.ExtendedLightShape
Inherits: Enum

  Values:
    - Linear = 0
    - Rectangle = 1
    - Disk = 2
    - Cylinder = 3
    - Sphere = 4

--------------------------------------------------------------------------------

[ENUM] ExteriorDaylightMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.ExteriorDaylightMode
Inherits: Enum

  Values:
    - DaylightOff = 0
    - DaylightOn = 1
    - DaylightAuto = 2

--------------------------------------------------------------------------------

[CLASS] FaceData
Full Name: Autodesk.AutoCAD.GraphicsInterface.FaceData

  CONSTRUCTORS:
    new FaceData()

  METHODS:
    short[] GetColors()
    ObjectId[] GetLayers()
    Mapper[] GetMappers()
    ObjectId[] GetMaterials()
    Vector3d[] GetNormalVectors()
    IntPtr[] GetSelectionMarkers()
    Transparency[] GetTransparency()
    EntityColor[] GetTrueColors()
    byte[] GetVisibility()
    void SetColors(short[] colors)
    void SetLayers(ObjectId[] layers)
    void SetMappers(Mapper[] mappers)
    void SetMaterials(ObjectId[] materials)
    void SetNormalVectors(Vector3d[] normal)
    void SetSelectionMarkers(IntPtr[] selectionMarker)
    void SetTransparency(Transparency[] transparency)
    void SetTrueColors(EntityColor[] colors)
    void SetVisibility(byte[] visibility)

--------------------------------------------------------------------------------

[CLASS] Fill
Full Name: Autodesk.AutoCAD.GraphicsInterface.Fill
Inherits: RXObject

  CONSTRUCTORS:
    new Fill()

--------------------------------------------------------------------------------

[ENUM] FillType
Full Name: Autodesk.AutoCAD.GraphicsInterface.FillType
Inherits: Enum

  Values:
    - FillAlways = 1
    - FillNever = 2

--------------------------------------------------------------------------------

[ENUM] FinalGatherMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.FinalGatherMode
Inherits: Enum

  Values:
    - FinalGatherNone = 0
    - FinalGatherCast = 1
    - FinalGatherReceive = 2
    - FinalGatherCastAndReceive = 3

--------------------------------------------------------------------------------

[STRUCT] FontDescriptor
Full Name: Autodesk.AutoCAD.GraphicsInterface.FontDescriptor

  CONSTRUCTORS:
    new FontDescriptor(string typeFace, bool bold, bool italic, int characters, int pitchAndFamily)

  PROPERTIES:
    bool Bold { get; }
    int CharacterSet { get; }
    bool Italic { get; }
    int PitchAndFamily { get; }
    string TypeFace { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[STRUCT] FrontAndBackClipping
Full Name: Autodesk.AutoCAD.GraphicsInterface.FrontAndBackClipping

  CONSTRUCTORS:
    new FrontAndBackClipping(bool clipFront, bool clipBack, double front, double back)

  PROPERTIES:
    double Back { get; }
    bool ClipBack { get; }
    bool ClipFront { get; }
    double Front { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(FrontAndBackClipping a)
    bool IsEqualTo(FrontAndBackClipping a, Tolerance tolerance)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] GdiDrawObject
Full Name: Autodesk.AutoCAD.GraphicsInterface.GdiDrawObject
Inherits: RXObject

  PROPERTIES:
    int Height { get; }
    int Width { get; }

  METHODS:
    bool Draw(IntPtr hDC, Rectangle bounds)

--------------------------------------------------------------------------------

[CLASS] GenericTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.GenericTexture
Inherits: ProceduralTexture

  CONSTRUCTORS:
    new GenericTexture()

  PROPERTIES:
    object Definition { get; set; }

  METHODS:
    GenericTexture Clone()
    void CopyFrom(GenericTexture pSrc)
    bool Equals(object obj)
    int GetHashCode()
    void Set(GenericTexture value)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Geometry
Full Name: Autodesk.AutoCAD.GraphicsInterface.Geometry
Inherits: RXObject

  PROPERTIES:
    Matrix3d ModelToWorldTransform { get; }
    Matrix3d WorldToModelTransform { get; }

  METHODS:
    bool Circle(Point3d firstPoint, Point3d secondPoint, Point3d thirdPoint)
    bool Circle(Point3d center, double radius, Vector3d normal)
    bool CircularArc(Point3d start, Point3d point, Point3d endingPoint, ArcType arcType)
    bool CircularArc(Point3d center, double radius, Vector3d normal, Vector3d startVector, double sweepAngle, ArcType arcType)
    bool Curve(Curve3d curve3d)
    bool Draw(Drawable value)
    bool Edge(Curve2dCollection e)
    bool EllipticalArc(Point3d center, Vector3d normal, double majorAxisLength, double minorAxisLength, double startDegreeInRads, double endDegreeInRads, double tiltDegreeInRads, ArcType arcType)
    bool Image(ImageBGRA32 imageSource, Point3d position, Vector3d u, Vector3d v, TransparencyMode transparencyMode)
    bool Image(ImageBGRA32 imageSource, Point3d position, Vector3d u, Vector3d v)
    bool Mesh(int rows, int columns, Point3dCollection points, EdgeData edgeData, FaceData faceData, VertexData vertexData, bool bAutoGenerateNormals)
    bool OwnerDraw(GdiDrawObject gdiDrawObject, Point3d position, Vector3d u, Vector3d v)
    bool Polygon(Point3dCollection points)
    bool Polyline(Polyline value, int fromIndex, int segments)
    bool Polyline(Polyline polylineObj)
    bool Polyline(Point3dCollection points, Vector3d normal, IntPtr subEntityMarker)
    bool Polypoint(Point3dCollection points, Vector3dCollection normals, IntPtrCollection subentityMarkers)
    bool PolyPolygon(UInt32Collection numPolygonPositions, Point3dCollection polygonPositions, UInt32Collection numPolygonPoints, Point3dCollection polygonPoints, EntityColorCollection outlineColors, LinetypeCollection outlineTypes, EntityColorCollection fillColors, TransparencyCollection fillOpacities)
    bool PolyPolyline(PolylineCollection polylineCollection)
    void PopClipBoundary()
    bool PopModelTransform()
    bool PushClipBoundary(ClipBoundary boundary)
    bool PushModelTransform(Matrix3d matrix)
    bool PushModelTransform(Vector3d normal)
    Matrix3d PushOrientationTransform(OrientationBehavior behavior)
    Matrix3d PushPositionTransform(PositionBehavior behavior, Point2d offset)
    Matrix3d PushPositionTransform(PositionBehavior behavior, Point3d offset)
    Matrix3d PushScaleTransform(ScaleBehavior behavior, Point2d extents)
    Matrix3d PushScaleTransform(ScaleBehavior behavior, Point3d extents)
    bool Ray(Point3d point1, Point3d point2)
    bool RowOfDots(int count, Point3d start, Vector3d step)
    bool Shell(Point3dCollection points, IntegerCollection faces, EdgeData edgeData, FaceData faceData, VertexData vertexData, bool bAutoGenerateNormals)
    bool Text(Point3d position, Vector3d normal, Vector3d direction, string message, bool raw, TextStyle textStyle)
    bool Text(Point3d position, Vector3d normal, Vector3d direction, double height, double width, double oblique, string message)
    bool WorldLine(Point3d startPoint, Point3d endPoint)
    bool Xline(Point3d point1, Point3d point2)

--------------------------------------------------------------------------------

[ENUM] GlobalIlluminationMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.GlobalIlluminationMode
Inherits: Enum

  Values:
    - GlobalIlluminationNone = 0
    - GlobalIlluminationCast = 1
    - GlobalIlluminationReceive = 2
    - GlobalIlluminationCastAndReceive = 3

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Glyph
Full Name: Autodesk.AutoCAD.GraphicsInterface.Glyph
Inherits: Drawable

  PROPERTIES:
    ObjectId Id { get; }
    bool IsPersistent { get; }

  METHODS:
    void SetLocation(Point3d point)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] GradientBackgroundTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.GradientBackgroundTraits
Inherits: NonEntityTraits

  PROPERTIES:
    EntityColor ColorBottom { get; set; }
    EntityColor ColorMiddle { get; set; }
    EntityColor ColorTop { get; set; }
    double Height { get; set; }
    double Horizon { get; set; }
    double Rotation { get; set; }

--------------------------------------------------------------------------------

[CLASS] GradientFill
Full Name: Autodesk.AutoCAD.GraphicsInterface.GradientFill
Inherits: Fill

  CONSTRUCTORS:
    new GradientFill(double gradientAngle, double gradientShift, bool bAdjustAspect, Color[] colors, GradientType gradientType)

  PROPERTIES:
    double GradientAngle { get; set; }
    Color[] GradientColors { get; set; }
    double GradientShift { get; set; }
    bool IsAdjustAspect { get; set; }
    GradientType Type { get; set; }

--------------------------------------------------------------------------------

[ENUM] GradientType
Full Name: Autodesk.AutoCAD.GraphicsInterface.GradientType
Inherits: Enum

  Values:
    - Linear = 0
    - Cylinder = 1
    - InvCylinder = 2
    - Spherical = 3
    - Hemispherical = 4
    - Curved = 5
    - InvSpherical = 6
    - InvHemispherical = 7
    - InvCurved = 8

--------------------------------------------------------------------------------

[ABSTRACT CLASS] GroundPlaneBackgroundTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.GroundPlaneBackgroundTraits
Inherits: NonEntityTraits

  PROPERTIES:
    EntityColor ColorGroundPlaneFar { get; set; }
    EntityColor ColorGroundPlaneNear { get; set; }
    EntityColor ColorSkyHorizon { get; set; }
    EntityColor ColorSkyZenith { get; set; }
    EntityColor ColorUndergroundAzimuth { get; set; }
    EntityColor ColorUndergroundHorizon { get; set; }

--------------------------------------------------------------------------------

[CLASS] HatchPattern
Full Name: Autodesk.AutoCAD.GraphicsInterface.HatchPattern
Inherits: Fill

  CONSTRUCTORS:
    new HatchPattern(HatchPatternDefinition[] patLines)

  PROPERTIES:
    HatchPatternDefinition[] PatternLines { get; }

--------------------------------------------------------------------------------

[CLASS] HatchPatternDefinition
Full Name: Autodesk.AutoCAD.GraphicsInterface.HatchPatternDefinition
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new HatchPatternDefinition(double angle, double baseX, double baseY, double offsetX, double offsetY, DoubleCollection dashList)

  PROPERTIES:
    double Angle { get; }
    double BaseX { get; }
    double BaseY { get; }
    DoubleCollection DashList { get; }
    double OffsetX { get; }
    double OffsetY { get; }

--------------------------------------------------------------------------------

[ENUM] HighlightStyle
Full Name: Autodesk.AutoCAD.GraphicsInterface.HighlightStyle
Inherits: Enum

  Values:
    - None = 0
    - Custom = 1
    - DashedAndThicken = 2
    - Dim = 3
    - ThickDim = 4
    - Glow = 5

--------------------------------------------------------------------------------

[ABSTRACT CLASS] IBLBackgroundTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.IBLBackgroundTraits
Inherits: NonEntityTraits

  PROPERTIES:
    bool DisplayImage { get; set; }
    bool Enable { get; set; }
    string IBLImageName { get; set; }
    double Rotation { get; set; }
    ObjectId SecondaryBackground { get; set; }

--------------------------------------------------------------------------------

[ENUM] IlluminationModel
Full Name: Autodesk.AutoCAD.GraphicsInterface.IlluminationModel
Inherits: Enum

  Values:
    - BlinnShader = 0
    - MetalShader = 1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ImageBackgroundTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.ImageBackgroundTraits
Inherits: NonEntityTraits

  PROPERTIES:
    bool FitToScreen { get; set; }
    string ImageFilename { get; set; }
    bool MaintainAspectRatio { get; set; }
    bool UseTiling { get; set; }
    double XOffset { get; set; }
    double XScale { get; set; }
    double YOffset { get; set; }
    double YScale { get; set; }

--------------------------------------------------------------------------------

[CLASS] ImageBGRA32
Full Name: Autodesk.AutoCAD.GraphicsInterface.ImageBGRA32
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new ImageBGRA32(uint width, uint height, IntPtr imageData)
    new ImageBGRA32()

  PROPERTIES:
    uint Height { get; set; }
    IntPtr Image { get; set; }
    uint Width { get; set; }

--------------------------------------------------------------------------------

[CLASS] ImageFileTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.ImageFileTexture
Inherits: ImageTexture

  CONSTRUCTORS:
    new ImageFileTexture()

  PROPERTIES:
    string SourceFileName { get; set; }

  METHODS:
    object Clone()
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[ENUM] ImageOrg
Full Name: Autodesk.AutoCAD.GraphicsInterface.ImageOrg
Inherits: Enum

  Values:
    - Bitonal = 0
    - Palette = 1
    - Gray = 2
    - RGBA = 3
    - BGRA = 4
    - ARGB = 5
    - ABGR = 6
    - BGR = 7
    - RGB = 8

--------------------------------------------------------------------------------

[ENUM] ImageSource
Full Name: Autodesk.AutoCAD.GraphicsInterface.ImageSource
Inherits: Enum

  Values:
    - FromDwg = 0
    - FromOleObject = 1
    - FromRender = 2

--------------------------------------------------------------------------------

[CLASS] ImageTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.ImageTexture
Inherits: MaterialTexture

  CONSTRUCTORS:
    new ImageTexture()

--------------------------------------------------------------------------------

[CLASS] LightAttenuation
Full Name: Autodesk.AutoCAD.GraphicsInterface.LightAttenuation
Inherits: RXObject

  CONSTRUCTORS:
    new LightAttenuation()

  PROPERTIES:
    AttenuationType AttenuationType { get; set; }
    double EndLimit { get; }
    double StartLimit { get; }
    bool UseLimits { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    void SetLimits(double startLimit, double endLimit)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LightTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.LightTraits
Inherits: NonEntityTraits

  CONSTRUCTORS:
    new LightTraits()

  PROPERTIES:
    bool On { get; set; }

--------------------------------------------------------------------------------

[ENUM] Linetype
Full Name: Autodesk.AutoCAD.GraphicsInterface.Linetype
Inherits: Enum

  Values:
    - Solid = 0
    - Dashed = 1
    - Dotted = 2
    - DashDot = 3
    - ShortDash = 4
    - MediumDash = 5
    - LongDash = 6
    - ShortDashX2 = 7
    - MediumDashX2 = 8
    - LongDashX2 = 9
    - MediumLongDash = 10
    - MediumDashShortDashShortDash = 11
    - LongDashShortDash = 12
    - LongDashDotDot = 13
    - LongDashDot = 14
    - MediumDashDotShortDashDot = 15
    - SparseDot = 16
    - ISODash = 17
    - ISODashSpace = 18
    - ISOLongDashDot = 19
    - ISOLongDashDoubleDot = 20
    - ISOLongDashTripleDot = 21
    - ISODot = 22
    - ISOLongDashShortDash = 23
    - ISOLongDashDoubleShortDash = 24
    - ISODashDot = 25
    - ISODoubleDashDot = 26
    - ISODashDoubleDot = 27
    - ISODoubleDashDoubleDot = 28
    - ISODashTripleDot = 29
    - ISODoubleDashTripleDot = 30
    - LineTypeNone = 31
    - Solid6PixelsBlank6Pixels = 32

--------------------------------------------------------------------------------

[CLASS] LinetypeCollection
Full Name: Autodesk.AutoCAD.GraphicsInterface.LinetypeCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new LinetypeCollection(Linetype[] values)
    new LinetypeCollection()

  PROPERTIES:
    int Count { get; }
    Linetype Item { get; set; }

  METHODS:
    int Add(Linetype value)
    void Clear()
    bool Contains(Linetype value)
    void CopyTo(Linetype[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(Linetype value)
    void Insert(int index, Linetype value)
    void Remove(Linetype value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ENUM] LuminanceMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.LuminanceMode
Inherits: Enum

  Values:
    - SelfIllumination = 0
    - Luminance = 1

--------------------------------------------------------------------------------

