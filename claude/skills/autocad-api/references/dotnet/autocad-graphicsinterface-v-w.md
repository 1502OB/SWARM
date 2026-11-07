# Autodesk.AutoCAD.GraphicsInterface

NAMESPACE: Autodesk.AutoCAD.GraphicsInterface
--------------------------------------------------------------------------------

[ENUM] VariantType
Full Name: Autodesk.AutoCAD.GraphicsInterface.VariantType
Inherits: Enum

  Values:
    - Undefined = 0
    - Boolean = 1
    - Int = 2
    - Double = 3
    - Color = 4
    - String = 5
    - Table = 6

--------------------------------------------------------------------------------

[CLASS] VertexData
Full Name: Autodesk.AutoCAD.GraphicsInterface.VertexData

  CONSTRUCTORS:
    new VertexData()

  PROPERTIES:
    OrientationType OrientationFlag { get; set; }

  METHODS:
    Point3d[] get_MappingCoords(MapChannel mapChannel)
    Vector3d[] GetNormalVectors()
    EntityColor[] GetTrueColors()
    void set_MappingCoords(MapChannel mapChannel, Point3d[] coords)
    void SetNormalVectors(Vector3d[] normal)
    void SetTrueColors(EntityColor[] colors)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Viewport
Full Name: Autodesk.AutoCAD.GraphicsInterface.Viewport
Inherits: RXObject

  PROPERTIES:
    int AcadWindowId { get; }
    Point3d CameraLocation { get; }
    Point3d CameraTarget { get; }
    Vector3d CameraUpVector { get; }
    Extents2d DeviceContextViewportCorners { get; }
    Matrix3d EyeToModelTransform { get; }
    Matrix3d EyeToWorldTransform { get; }
    FrontAndBackClipping FrontAndBackClipping { get; }
    bool IsPerspective { get; }
    double LinetypeGenerationCriteria { get; }
    double LinetypeScaleMultiplier { get; }
    Matrix3d ModelToEyeTransform { get; }
    Vector3d ViewDirection { get; }
    IntPtr ViewportId { get; }
    Matrix3d WorldToEyeTransform { get; }

  METHODS:
    Point3d DoInversePerspective(Point3d value)
    Point3d DoPerspective(Point3d value)
    Point2d GetNumPixelsInUnitSquare(Point3d givenWorldPoint)
    bool LayerVisible(ObjectId idLayer)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ViewportDraw
Full Name: Autodesk.AutoCAD.GraphicsInterface.ViewportDraw
Inherits: CommonDraw

  PROPERTIES:
    ViewportGeometry Geometry { get; }
    int SequenceNumber { get; }
    Viewport Viewport { get; }
    ObjectId ViewportObjectId { get; }

  METHODS:
    bool IsValidId(IntPtr id)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ViewportGeometry
Full Name: Autodesk.AutoCAD.GraphicsInterface.ViewportGeometry
Inherits: Geometry

  METHODS:
    bool DeviceContextPolygon(Point3dCollection points)
    bool DeviceContextPolyline(Point3dCollection points)
    bool DeviceContextRasterImage(Point3d origin, Vector3d u, Vector3d v, Matrix2d pixel, ObjectId entityId, ImageOrg imageOrg, int imageWidth, int imageHeight, short imageColorDepth, bool transparency, ImageSource source, Vector3d unRotated, ImageOrg originalImage, Matrix2d unRotatedPixel, int unRotatedImageWidth, int unRotatedImageHeight)
    bool PolygonEye(Point3dCollection points)
    bool PolylineEye(Point3dCollection points)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ViewportTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.ViewportTraits
Inherits: DrawableTraits

  CONSTRUCTORS:
    new ViewportTraits()

  PROPERTIES:
    Color AmbientLightColor { get; set; }
    ObjectId Background { get; set; }
    double Brightness { get; set; }
    double Contrast { get; set; }
    bool DefaultLightingOn { get; set; }
    DefaultLightingType DefaultLightingType { get; set; }
    ObjectId RenderEnvironment { get; set; }
    ObjectId RenderSettings { get; set; }
    ToneOperatorParameters ToneOperatorParameters { get; set; }

--------------------------------------------------------------------------------

[CLASS] VisualStyle
Full Name: Autodesk.AutoCAD.GraphicsInterface.VisualStyle
Inherits: RXObject

  CONSTRUCTORS:
    new VisualStyle(VisualStyleType type)
    new VisualStyle()

  PROPERTIES:
    VisualStyleType Type { get; set; }

  METHODS:
    VariantType GetPropertyType(VisualStyleProperty prop)
    object GetTrait(VisualStyleProperty prop)
    bool GetTraitFlag(VisualStyleProperty prop, uint flagVal)
    VisualStyleOperation Operation(VisualStyleProperty prop)
    void SetTrait(VisualStyleProperty prop, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, Color color, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, double red, double green, double blue, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, double dVal, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, bool bVal, VisualStyleOperation __unnamed002)
    void SetTrait(VisualStyleProperty prop, int nVal, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, object val, VisualStyleOperation op)
    void SetTraitFlag(VisualStyleProperty prop, uint flagVal, bool bEnable)

--------------------------------------------------------------------------------

[ENUM] VisualStyleOperation
Full Name: Autodesk.AutoCAD.GraphicsInterface.VisualStyleOperation
Inherits: Enum

  Values:
    - Inherit = 0
    - Set = 1
    - Disable = 2
    - Enable = 3
    - InvalidOperation = -1

--------------------------------------------------------------------------------

[ENUM] VisualStyleProperty
Full Name: Autodesk.AutoCAD.GraphicsInterface.VisualStyleProperty
Inherits: Enum

  Values:
    - FaceLightingModel = 0
    - FaceLightingQuality = 1
    - FaceColorMode = 2
    - FaceModifier = 3
    - FaceOpacity = 4
    - FaceSpecular = 5
    - FaceMonoColor = 6
    - EdgeModel = 7
    - EdgeStyle = 8
    - EdgeIntersectionColor = 9
    - EdgeObscuredColor = 10
    - EdgeObscuredLinePattern = 11
    - EdgeIntersectionLinePattern = 12
    - EdgeCreaseAngle = 13
    - EdgeModifier = 14
    - EdgeColor = 15
    - EdgeOpacity = 16
    - EdgeWidth = 17
    - EdgeOverhang = 18
    - EdgeJitterAmount = 19
    - EdgeSilhouetteColor = 20
    - EdgeSilhouetteWidth = 21
    - EdgeHaloGap = 22
    - EdgeIsolines = 23
    - EdgeHidePrecision = 24
    - DisplayStyle = 25
    - DisplayBrightness = 26
    - DisplayShadowType = 27
    - UseDrawOrder = 28
    - ViewportTransparency = 29
    - LightingEnabled = 30
    - PosterizeEffect = 31
    - MonoEffect = 32
    - BlurEffect = 33
    - PencilEffect = 34
    - BloomEffect = 35
    - PastelEffect = 36
    - BlurAmount = 37
    - PencilAngle = 38
    - PencilScale = 39
    - PencilPattern = 40
    - PencilColor = 41
    - BloomThreshold = 42
    - BloomRadius = 43
    - TintColor = 44
    - FaceAdjustment = 45
    - PostContrast = 46
    - PostBrightness = 47
    - PostPower = 48
    - TintEffect = 49
    - BloomIntensity = 50
    - Color = 51
    - Transparency = 52
    - EdgeWiggleAmount = 53
    - EdgeTexturePath = 54
    - DepthOfField = 55
    - FocusDistance = 56
    - FocusWidth = 57
    - PropertyCount = 58
    - InvalidProperty = -1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] VisualStyleTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.VisualStyleTraits
Inherits: DrawableTraits

  PROPERTIES:
    VisualStyle AcGiVisualStyle { get; set; }

--------------------------------------------------------------------------------

[ENUM] VisualStyleType
Full Name: Autodesk.AutoCAD.GraphicsInterface.VisualStyleType
Inherits: Enum

  Values:
    - Flat = 0
    - FlatWithEdges = 1
    - Gouraud = 2
    - GouraudWithEdges = 3
    - Wireframe2D = 4
    - Wireframe3D = 5
    - Hidden = 6
    - Basic = 7
    - Realistic = 8
    - Conceptual = 9
    - Custom = 10
    - Dim = 11
    - Brighten = 12
    - Thicken = 13
    - LinePattern = 14
    - FacePattern = 15
    - ColorChange = 16
    - FaceOnly = 17
    - EdgeOnly = 18
    - DisplayOnly = 19
    - JitterOff = 20
    - OverhangOff = 21
    - EdgeColorOff = 22
    - ShadesOfGray = 23
    - Sketchy = 24
    - XRay = 25
    - ShadedWithEdges = 26
    - Shaded = 27
    - EmptyStyle = 31

--------------------------------------------------------------------------------

[ENUM] VSDisplayShadowType
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSDisplayShadowType
Inherits: Enum

  Values:
    - None = 0
    - GroundPlane = 1
    - Full = 2
    - FullAndGround = 3

--------------------------------------------------------------------------------

[ENUM] VSDisplayStyles
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSDisplayStyles
Inherits: Enum

  Values:
    - None = 0
    - BackgroundsFlag = 1
    - LightingFlag = 2
    - MaterialsFlag = 4
    - TexturesFlag = 8

--------------------------------------------------------------------------------

[ENUM] VSEdgeJitterAmount
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSEdgeJitterAmount
Inherits: Enum

  Values:
    - JitterLow = 1
    - JitterMedium = 2
    - JitterHigh = 3

--------------------------------------------------------------------------------

[ENUM] VSEdgeLinePattern
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSEdgeLinePattern
Inherits: Enum

  Values:
    - Solid = 1
    - Dashed = 2
    - Dotted = 3
    - ShortDash = 4
    - MediumDash = 5
    - LongDash = 6
    - DoubleShortDash = 7
    - DoubleMediumDash = 8
    - DoubleLongDash = 9
    - MediumLongDash = 10
    - SparseDot = 11

--------------------------------------------------------------------------------

[ENUM] VSEdgeModel
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSEdgeModel
Inherits: Enum

  Values:
    - NoEdges = 0
    - Isolines = 1
    - FacetEdges = 2

--------------------------------------------------------------------------------

[ENUM] VSEdgeModifiers
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSEdgeModifiers
Inherits: Enum

  Values:
    - None = 0
    - EdgeOverhangFlag = 1
    - EdgeJitterFlag = 2
    - EdgeWidthFlag = 4
    - EdgeColorFlag = 8
    - EdgeHaloGapFlag = 16
    - AlwaysOnTopFlag = 64
    - EdgeOpacityFlag = 128

--------------------------------------------------------------------------------

[ENUM] VSEdgeStyles
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSEdgeStyles
Inherits: Enum

  Values:
    - None = 0
    - VisibleFlag = 1
    - SilhouetteFlag = 2
    - ObscuredFlag = 4
    - IntersectionFlag = 8

--------------------------------------------------------------------------------

[ENUM] VSFaceColorMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSFaceColorMode
Inherits: Enum

  Values:
    - None = 0
    - ObjectColor = 1
    - BackgroundColor = 2
    - Mono = 3
    - Tint = 4
    - Desaturate = 5

--------------------------------------------------------------------------------

[ENUM] VSFaceLightingModel
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSFaceLightingModel
Inherits: Enum

  Values:
    - Invisible = 0
    - Constant = 1
    - Phong = 2
    - Gooch = 3

--------------------------------------------------------------------------------

[ENUM] VSFaceLightingQuality
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSFaceLightingQuality
Inherits: Enum

  Values:
    - NoLighting = 0
    - PerFaceLighting = 1
    - PerVertexLighting = 2
    - PerPixelLighting = 3

--------------------------------------------------------------------------------

[ENUM] VSFaceModifiers
Full Name: Autodesk.AutoCAD.GraphicsInterface.VSFaceModifiers
Inherits: Enum

  Values:
    - None = 0
    - FaceOpacityFlag = 1
    - SpecularFlag = 2

--------------------------------------------------------------------------------

[ENUM] WebFileType
Full Name: Autodesk.AutoCAD.GraphicsInterface.WebFileType
Inherits: Enum

  Values:
    - TypeC = 1
    - TypeB = 2
    - TypeA = 3

--------------------------------------------------------------------------------

[ABSTRACT CLASS] WebLightTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.WebLightTraits
Inherits: PointLightTraits

  CONSTRUCTORS:
    new WebLightTraits()

  PROPERTIES:
    string WebFile { get; set; }
    WebFileType WebFileType { get; set; }
    double WebFlux { get; set; }
    bool WebHorzAng90to270 { get; set; }
    Vector3d WebRotation { get; set; }
    WebSymmetry WebSymmetry { get; set; }

--------------------------------------------------------------------------------

[ENUM] WebSymmetry
Full Name: Autodesk.AutoCAD.GraphicsInterface.WebSymmetry
Inherits: Enum

  Values:
    - NoSymmetry = 0
    - SingleSymmetry = 1
    - DoubleSymmetry = 2
    - AxialSymmetry = 3

--------------------------------------------------------------------------------

[CLASS] WoodTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.WoodTexture
Inherits: ProceduralTexture

  CONSTRUCTORS:
    new WoodTexture()

  PROPERTIES:
    double AxialNoise { get; set; }
    MaterialColor Color1 { get; set; }
    MaterialColor Color2 { get; set; }
    double GrainThickness { get; set; }
    double RadialNoise { get; set; }

  METHODS:
    WoodTexture Clone()
    bool Equals(object obj)
    int GetHashCode()
    void Set(WoodTexture value)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] WorldDraw
Full Name: Autodesk.AutoCAD.GraphicsInterface.WorldDraw
Inherits: CommonDraw

  PROPERTIES:
    WorldGeometry Geometry { get; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] WorldGeometry
Full Name: Autodesk.AutoCAD.GraphicsInterface.WorldGeometry
Inherits: Geometry

  METHODS:
    void SetExtents(Extents3d extents)
    void StartAttributesSegment()

--------------------------------------------------------------------------------

