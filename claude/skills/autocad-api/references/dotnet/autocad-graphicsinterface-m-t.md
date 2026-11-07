# Autodesk.AutoCAD.GraphicsInterface

NAMESPACE: Autodesk.AutoCAD.GraphicsInterface
--------------------------------------------------------------------------------

[ENUM] MapChannel
Full Name: Autodesk.AutoCAD.GraphicsInterface.MapChannel
Inherits: Enum

  Values:
    - AllChannels = 0
    - MapChannels = 1

--------------------------------------------------------------------------------

[ENUM] MapFilter
Full Name: Autodesk.AutoCAD.GraphicsInterface.MapFilter
Inherits: Enum

  Values:
    - Default = 0
    - None = 1

--------------------------------------------------------------------------------

[CLASS] Mapper
Full Name: Autodesk.AutoCAD.GraphicsInterface.Mapper
Inherits: RXObject

  CONSTRUCTORS:
    new Mapper(Projection projection, Tiling uTiling, Tiling vTiling, AutoTransform autoTransform, Matrix3d transform)
    new Mapper()

  PROPERTIES:
    AutoTransform AutoTransform { get; }
    Projection Projection { get; }
    Matrix3d Transform { get; }
    Tiling UTiling { get; set; }
    Tiling VTiling { get; set; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()

--------------------------------------------------------------------------------

[CLASS] MarbleTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.MarbleTexture
Inherits: ProceduralTexture

  CONSTRUCTORS:
    new MarbleTexture()

  PROPERTIES:
    MaterialColor StoneColor { get; set; }
    MaterialColor VeinColor { get; set; }
    double VeinSpacing { get; set; }
    double VeinWidth { get; set; }

  METHODS:
    MarbleTexture Clone()
    bool Equals(object obj)
    int GetHashCode()
    void Set(MarbleTexture value)

--------------------------------------------------------------------------------

[CLASS] MaterialColor
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialColor
Inherits: RXObject

  CONSTRUCTORS:
    new MaterialColor(Method method, double factor, EntityColor color)
    new MaterialColor()

  PROPERTIES:
    EntityColor Color { get; }
    double Factor { get; }
    Method Method { get; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()

--------------------------------------------------------------------------------

[STRUCT] MaterialDiffuseComponent
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialDiffuseComponent

  CONSTRUCTORS:
    new MaterialDiffuseComponent(MaterialColor color, MaterialMap map)

  PROPERTIES:
    MaterialColor Color { get; }
    MaterialMap Map { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] MaterialMap
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialMap
Inherits: RXObject

  CONSTRUCTORS:
    new MaterialMap(Source source, string fileName, MaterialTexture pTexture, double blendFactor, Mapper mapper)
    new MaterialMap(Source source, MaterialTexture pTexture, double blendFactor, Mapper mapper, MapFilter filter)
    new MaterialMap(Source source, MaterialTexture pTexture, double blendFactor, Mapper mapper)
    new MaterialMap()

  PROPERTIES:
    double BlendFactor { get; }
    MapFilter Filter { get; }
    Mapper Mapper { get; }
    Source Source { get; }
    string SourceFileName { get; }
    MaterialTexture Texture { get; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()

--------------------------------------------------------------------------------

[STRUCT] MaterialNormalMapComponent
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialNormalMapComponent

  CONSTRUCTORS:
    new MaterialNormalMapComponent(MaterialMap map, NormalMapMethod method, double strength)

  PROPERTIES:
    MaterialMap Map { get; }
    NormalMapMethod Method { get; }
    double Strength { get; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[STRUCT] MaterialOpacityComponent
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialOpacityComponent

  CONSTRUCTORS:
    new MaterialOpacityComponent(double percentage, MaterialMap map)

  PROPERTIES:
    MaterialMap Map { get; }
    double Percentage { get; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[STRUCT] MaterialRefractionComponent
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialRefractionComponent

  CONSTRUCTORS:
    new MaterialRefractionComponent(double index, MaterialMap map)

  PROPERTIES:
    double Index { get; }
    MaterialMap Map { get; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[STRUCT] MaterialSpecularComponent
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialSpecularComponent

  CONSTRUCTORS:
    new MaterialSpecularComponent(MaterialColor color, MaterialMap map, double gloss)

  PROPERTIES:
    MaterialColor Color { get; }
    double Gloss { get; }
    MaterialMap Map { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] MaterialTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialTexture
Inherits: RXObject

  CONSTRUCTORS:
    new MaterialTexture()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] MaterialTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.MaterialTraits
Inherits: NonEntityTraits

  PROPERTIES:
    MaterialColor Ambient { get; set; }
    MaterialMap Bump { get; set; }
    ChannelFlags ChannelFlags { get; set; }
    double ColorBleedScale { get; set; }
    MaterialDiffuseComponent Diffuse { get; set; }
    FinalGatherMode FinalGather { get; set; }
    GlobalIlluminationMode GlobalIllumination { get; set; }
    IlluminationModel IlluminationModel { get; set; }
    double IndirectBumpScale { get; set; }
    double Luminance { get; set; }
    LuminanceMode LuminanceMode { get; set; }
    Mode Mode { get; set; }
    MaterialNormalMapComponent NormalMap { get; set; }
    MaterialOpacityComponent Opacity { get; set; }
    double ReflectanceScale { get; set; }
    MaterialMap Reflection { get; set; }
    double Reflectivity { get; set; }
    MaterialRefractionComponent Refraction { get; set; }
    double SelfIllumination { get; set; }
    MaterialSpecularComponent Specular { get; set; }
    double Translucence { get; set; }
    double TransmittanceScale { get; set; }
    bool TwoSided { get; set; }

--------------------------------------------------------------------------------

[ENUM] Method
Full Name: Autodesk.AutoCAD.GraphicsInterface.Method
Inherits: Enum

  Values:
    - Inherit = 0
    - Override = 1

--------------------------------------------------------------------------------

[ENUM] Mode
Full Name: Autodesk.AutoCAD.GraphicsInterface.Mode
Inherits: Enum

  Values:
    - Realistic = 0
    - Advanced = 1

--------------------------------------------------------------------------------

[CLASS] NonEntityTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.NonEntityTraits
Inherits: DrawableTraits

  PROPERTIES:
    short Color { get; set; }
    int DrawFlags { get; set; }
    Fill Fill { get; set; }
    FillType FillType { get; set; }
    ObjectId Layer { get; set; }
    Linetype LinePattern { get; set; }
    ObjectId LineType { get; set; }
    double LineTypeScale { get; set; }
    LineWeight LineWeight { get; set; }
    Mapper Mapper { get; set; }
    ObjectId Material { get; set; }
    PlotStyleDescriptor PlotStyleDescriptor { get; set; }
    bool Sectionable { get; set; }
    SelectionFlags SelectionFlags { get; set; }
    bool SelectionOnlyGeometry { get; set; }
    ShadowFlags ShadowFlags { get; set; }
    double Thickness { get; set; }
    Transparency Transparency { get; set; }
    EntityColor TrueColor { get; set; }
    ObjectId VisualStyle { get; set; }

  METHODS:
    void AddLight(ObjectId lightId)
    object GetHighlightProperty(HighlightProperty propertyType)
    void SetHighlightProperty(HighlightProperty propertyType, object value)
    void SetSelectionMarker(IntPtr markerId)
    void SetupForEntity(Entity entity)

--------------------------------------------------------------------------------

[ENUM] NormalMapMethod
Full Name: Autodesk.AutoCAD.GraphicsInterface.NormalMapMethod
Inherits: Enum

  Values:
    - TangentSpaceNormalMap = 0

--------------------------------------------------------------------------------

[ENUM] OrientationBehavior
Full Name: Autodesk.AutoCAD.GraphicsInterface.OrientationBehavior
Inherits: Enum

  Values:
    - World = 0
    - Screen = 1
    - ZAxis = 2

--------------------------------------------------------------------------------

[ENUM] OrientationType
Full Name: Autodesk.AutoCAD.GraphicsInterface.OrientationType
Inherits: Enum

  Values:
    - NoOrientation = 0
    - Clockwise = 1
    - CounterClockwise = -1

--------------------------------------------------------------------------------

[CLASS] PhotographicExposureParameters
Full Name: Autodesk.AutoCAD.GraphicsInterface.PhotographicExposureParameters
Inherits: ToneOperatorParameters

  CONSTRUCTORS:
    new PhotographicExposureParameters()

  PROPERTIES:
    double Exposure { get; set; }
    Color WhiteColor { get; }
    double WhitePoint { get; set; }

  METHODS:
    void CopyFrom(RXObject source)
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[STRUCT] PixelBGRA32
Full Name: Autodesk.AutoCAD.GraphicsInterface.PixelBGRA32

  CONSTRUCTORS:
    new PixelBGRA32(byte blue, byte green, byte red, byte alpha)
    new PixelBGRA32(uint bgra)
    new PixelBGRA32()

  PROPERTIES:
    byte Alpha { get; set; }
    byte Blue { get; set; }
    byte Green { get; set; }
    byte Red { get; set; }

  METHODS:
    void init()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PointLightTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.PointLightTraits
Inherits: StandardLightTraits

  CONSTRUCTORS:
    new PointLightTraits()

  PROPERTIES:
    LightAttenuation Attenuation { get; set; }
    bool HasTarget { get; set; }
    ColorRGB LampColor { get; set; }
    double PhysicalIntensity { get; set; }
    Point3d Position { get; set; }
    Point3d TargetLocation { get; set; }

--------------------------------------------------------------------------------

[CLASS] Polyline
Full Name: Autodesk.AutoCAD.GraphicsInterface.Polyline
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Polyline(Point3dCollection pPoints, Vector3d normal, IntPtr pBaseSubEntMarker)
    new Polyline()

  PROPERTIES:
    IntPtr BaseSubEntMarker { get; set; }
    Vector3d Normal { get; set; }
    Point3dCollection Points { get; set; }

--------------------------------------------------------------------------------

[CLASS] PolylineCollection
Full Name: Autodesk.AutoCAD.GraphicsInterface.PolylineCollection
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new PolylineCollection()

  PROPERTIES:
    int Count { get; }
    Polyline Item { get; set; }

  METHODS:
    int Add(Polyline value)
    void Clear()
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ENUM] PositionBehavior
Full Name: Autodesk.AutoCAD.GraphicsInterface.PositionBehavior
Inherits: Enum

  Values:
    - World = 0
    - Viewport = 1
    - Screen = 2
    - ScreenLocalOrigin = 3
    - WorldWithScreenOffset = 4

--------------------------------------------------------------------------------

[CLASS] ProceduralTexture
Full Name: Autodesk.AutoCAD.GraphicsInterface.ProceduralTexture
Inherits: MaterialTexture

  CONSTRUCTORS:
    new ProceduralTexture()

--------------------------------------------------------------------------------

[ENUM] ProceduralTextureType
Full Name: Autodesk.AutoCAD.GraphicsInterface.ProceduralTextureType
Inherits: Enum

  Values:
    - Wood = 0
    - Marble = 1
    - Generic = 2

--------------------------------------------------------------------------------

[ENUM] Projection
Full Name: Autodesk.AutoCAD.GraphicsInterface.Projection
Inherits: Enum

  Values:
    - InheritProjection = 0
    - Planar = 1
    - Box = 2
    - Cylinder = 3
    - Sphere = 4

--------------------------------------------------------------------------------

[ENUM] RapidRTFilterType
Full Name: Autodesk.AutoCAD.GraphicsInterface.RapidRTFilterType
Inherits: Enum

  Values:
    - Box = 0
    - Triangle = 1
    - Gaussian = 2
    - Lanczos = 3
    - Mitchell = 4

--------------------------------------------------------------------------------

[ENUM] RapidRTLightingMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.RapidRTLightingMode
Inherits: Enum

  Values:
    - Simplified = 0
    - Basic = 1
    - Advanced = 2

--------------------------------------------------------------------------------

[ENUM] RapidRTQuitCondition
Full Name: Autodesk.AutoCAD.GraphicsInterface.RapidRTQuitCondition
Inherits: Enum

  Values:
    - ERenderLevel = 0
    - ERenderTime = 1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] RapidRTRenderSettingsTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.RapidRTRenderSettingsTraits
Inherits: NonEntityTraits

  PROPERTIES:
    float FilterHeight { get; set; }
    RapidRTFilterType FilterType { get; set; }
    float FilterWidth { get; set; }
    RapidRTLightingMode LightingMode { get; set; }
    RapidRTQuitCondition QuitCondition { get; set; }
    int RenderLevel { get; set; }
    int RenderTime { get; set; }

--------------------------------------------------------------------------------

[ENUM] RegenType
Full Name: Autodesk.AutoCAD.GraphicsInterface.RegenType
Inherits: Enum

  Values:
    - Invalid = 0
    - StandardDisplay = 2
    - HideOrShadeCommand = 3
    - RenderCommand = 4
    - RenderCommand = 4
    - ForExplode = 5
    - SaveWorldDrawForProxy = 6

--------------------------------------------------------------------------------

[ABSTRACT CLASS] RenderEnvironmentTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.RenderEnvironmentTraits
Inherits: NonEntityTraits

  PROPERTIES:
    bool Enable { get; set; }
    MaterialTexture EnvironmentMap { get; set; }
    double FarDistance { get; set; }
    double FarPercentage { get; set; }
    EntityColor FogColor { get; set; }
    bool IsBackground { get; set; }
    double NearDistance { get; set; }
    double NearPercentage { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] RenderSettingsTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.RenderSettingsTraits
Inherits: NonEntityTraits

  PROPERTIES:
    bool BackFacesEnabled { get; set; }
    bool DiagnosticBackgroundEnabled { get; set; }
    bool MaterialEnabled { get; set; }
    double ModelScaleFactor { get; set; }
    bool ShadowsEnabled { get; set; }
    bool TextureSampling { get; set; }

--------------------------------------------------------------------------------

[ENUM] ScaleBehavior
Full Name: Autodesk.AutoCAD.GraphicsInterface.ScaleBehavior
Inherits: Enum

  Values:
    - World = 0
    - Viewport = 1
    - Screen = 2
    - ViewportLocalOrigin = 3
    - ScreenLocalOrigin = 4

--------------------------------------------------------------------------------

[ENUM] SelectionFlags
Full Name: Autodesk.AutoCAD.GraphicsInterface.SelectionFlags
Inherits: Enum

  Values:
    - None = 0
    - SelectionIgnore = 1

--------------------------------------------------------------------------------

[ENUM] ShadowFlags
Full Name: Autodesk.AutoCAD.GraphicsInterface.ShadowFlags
Inherits: Enum

  Values:
    - ShadowsCastAndReceive = 0
    - ShadowsDoesNotCast = 1
    - ShadowsDoesNotReceive = 2
    - ShadowsIgnore = 3

--------------------------------------------------------------------------------

[CLASS] ShadowParameters
Full Name: Autodesk.AutoCAD.GraphicsInterface.ShadowParameters
Inherits: RXObject

  CONSTRUCTORS:
    new ShadowParameters()

  PROPERTIES:
    double ExtendedLightLength { get; set; }
    double ExtendedLightRadius { get; set; }
    ExtendedLightShape ExtendedLightShape { get; set; }
    double ExtendedLightWidth { get; set; }
    short ShadowMapSize { get; set; }
    byte ShadowMapSoftness { get; set; }
    short ShadowSamples { get; set; }
    bool ShadowsOn { get; set; }
    ShadowType ShadowType { get; set; }
    bool ShapeVisibility { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[ENUM] ShadowType
Full Name: Autodesk.AutoCAD.GraphicsInterface.ShadowType
Inherits: Enum

  Values:
    - RayTraced = 0
    - Maps = 1
    - Sampled = 2

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SkyBackgroundTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.SkyBackgroundTraits
Inherits: NonEntityTraits

  PROPERTIES:
    SkyParameters SkyParameters { get; set; }

--------------------------------------------------------------------------------

[CLASS] SkyParameters
Full Name: Autodesk.AutoCAD.GraphicsInterface.SkyParameters
Inherits: RXObject

  CONSTRUCTORS:
    new SkyParameters()

  PROPERTIES:
    bool AerialPerspective { get; set; }
    double DiskIntensity { get; set; }
    double DiskScale { get; set; }
    double GlowIntensity { get; set; }
    Color GroundColor { get; set; }
    double Haze { get; set; }
    double HorizonBlur { get; set; }
    double HorizonHeight { get; set; }
    bool Illumination { get; set; }
    double IntensityFactor { get; set; }
    Color NightColor { get; set; }
    double RedBlueShift { get; set; }
    double Saturation { get; set; }
    short SolarDiskSamples { get; set; }
    Vector3d SunDirection { get; set; }
    double VisibilityDistance { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SolidBackgroundTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.SolidBackgroundTraits
Inherits: NonEntityTraits

  PROPERTIES:
    EntityColor ColorSolid { get; set; }

--------------------------------------------------------------------------------

[ENUM] Source
Full Name: Autodesk.AutoCAD.GraphicsInterface.Source
Inherits: Enum

  Values:
    - Scene = 0
    - File = 1
    - Procedural = 2

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SpotLightTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.SpotLightTraits
Inherits: StandardLightTraits

  CONSTRUCTORS:
    new SpotLightTraits()

  PROPERTIES:
    LightAttenuation Attenuation { get; set; }
    double Falloff { get; }
    double Hotspot { get; }
    ColorRGB LampColor { get; set; }
    double PhysicalIntensity { get; set; }
    Point3d Position { get; set; }
    Point3d TargetLocation { get; set; }

  METHODS:
    bool SetHotspotAndFalloff(double hotspot, double falloff)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] StandardLightTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.StandardLightTraits
Inherits: LightTraits

  CONSTRUCTORS:
    new StandardLightTraits()

  PROPERTIES:
    double Intensity { get; set; }
    Color LightColor { get; set; }
    ShadowParameters Shadow { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SubEntityTraits
Full Name: Autodesk.AutoCAD.GraphicsInterface.SubEntityTraits
Inherits: RXObject

  PROPERTIES:
    short Color { get; set; }
    int DrawFlags { get; set; }
    Fill Fill { get; set; }
    FillType FillType { get; set; }
    ObjectId Layer { get; set; }
    ObjectId LineType { get; set; }
    double LineTypeScale { get; set; }
    LineWeight LineWeight { get; set; }
    Mapper Mapper { get; set; }
    ObjectId Material { get; set; }
    PlotStyleDescriptor PlotStyleDescriptor { get; set; }
    bool Sectionable { get; set; }
    bool SelectionOnlyGeometry { get; set; }
    ShadowFlags ShadowFlags { get; set; }
    double Thickness { get; set; }
    Transparency Transparency { get; set; }
    EntityColor TrueColor { get; set; }
    ObjectId VisualStyle { get; set; }

  METHODS:
    void SetSelectionMarker(IntPtr markerId)

--------------------------------------------------------------------------------

[CLASS] TextStyle
Full Name: Autodesk.AutoCAD.GraphicsInterface.TextStyle
Inherits: RXObject

  CONSTRUCTORS:
    new TextStyle(string fontName, string bigFontName, double textSize, double x, double obliqueAngle, double trackingPercent, bool isBackward, bool isUpsideDown, bool isVertical, bool isOverLined, bool isUnderlined, bool isStrikethrough, string styleName)
    new TextStyle()

  PROPERTIES:
    bool Backward { get; set; }
    string BigFontFileName { get; set; }
    string FileName { get; set; }
    FontDescriptor Font { get; set; }
    byte LoadStyleRec { get; }
    double ObliquingAngle { get; set; }
    bool Overlined { get; set; }
    bool PreLoaded { get; set; }
    bool Strikethrough { get; set; }
    string StyleName { get; set; }
    double TextSize { get; set; }
    double TrackingPercent { get; set; }
    bool Underlined { get; set; }
    bool UpsideDown { get; set; }
    bool Vertical { get; set; }
    double XScale { get; set; }

  METHODS:
    static TextStyle Create(IntPtr unmanagedPointer, bool autoDelete)
    Extents2d ExtentsBox(string value1, bool includeSpaces, bool raw, WorldDraw context)
    void FromTextStyleTableRecord(string AcDbStyleName)
    void FromTextStyleTableRecord(ObjectId AcDbStyleId)
    void SetTrackKerning(double trackPercent)
    ObjectId ToTextStyleTableRecord(string AcDbStyleName)
    void ToTextStyleTableRecord(ObjectId AcDbStyleId)
    ObjectId ToTextStyleTableRecord()

--------------------------------------------------------------------------------

[ENUM] Tiling
Full Name: Autodesk.AutoCAD.GraphicsInterface.Tiling
Inherits: Enum

  Values:
    - InheritTiling = 0
    - Tile = 1
    - Crop = 2
    - Clamp = 3
    - Mirror = 4

--------------------------------------------------------------------------------

[CLASS] ToneOperatorParameters
Full Name: Autodesk.AutoCAD.GraphicsInterface.ToneOperatorParameters
Inherits: RXObject

  CONSTRUCTORS:
    new ToneOperatorParameters()

  PROPERTIES:
    double Brightness { get; set; }
    bool ChromaticAdaptation { get; set; }
    bool ColorDifferentiation { get; set; }
    double Contrast { get; set; }
    ExteriorDaylightMode ExteriorDaylight { get; set; }
    bool IsActive { get; set; }
    double MidTones { get; set; }
    bool ProcessBackground { get; set; }
    Color WhiteColor { get; set; }

  METHODS:
    void CopyFrom(RXObject source)
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[ENUM] TransientDrawingMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.TransientDrawingMode
Inherits: Enum

  Values:
    - Main = 0
    - Sprite = 1
    - DirectShortTerm = 2
    - Highlight = 3
    - DirectTopmost = 4
    - Contrast = 5
    - Count = 6

--------------------------------------------------------------------------------

[CLASS] TransientManager
Full Name: Autodesk.AutoCAD.GraphicsInterface.TransientManager
Inherits: DisposableWrapper

  PROPERTIES:
    static TransientManager CurrentTransientManager { get; set; }

  METHODS:
    bool AddChildTransient(Drawable added, Drawable parent)
    bool AddTransient(Drawable added, TransientDrawingMode mode, int subDrawingMode, IntegerCollection viewportNumbers)
    bool EraseChildTransient(Drawable erased, Drawable parent)
    bool EraseTransient(Drawable erased, IntegerCollection viewportNumbers)
    bool EraseTransients(TransientDrawingMode mode, int subDrawingMode, IntegerCollection viewportNumbers)
    int GetFreeSubDrawingMode(TransientDrawingMode mode, IntegerCollection viewportNumbers, out int subDrawingMode)
    void UpdateChildTransient(Drawable updated, Drawable parent)
    void UpdateTransient(Drawable updated, IntegerCollection viewportNumbers)

--------------------------------------------------------------------------------

[ENUM] TransparencyMode
Full Name: Autodesk.AutoCAD.GraphicsInterface.TransparencyMode
Inherits: Enum

  Values:
    - TransparencyOff = 0
    - Transparency1Bit = 1
    - Transparency8Bit = 2

--------------------------------------------------------------------------------

