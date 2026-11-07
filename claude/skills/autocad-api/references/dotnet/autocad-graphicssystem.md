# Autodesk.AutoCAD.GraphicsSystem

NAMESPACE: Autodesk.AutoCAD.GraphicsSystem
--------------------------------------------------------------------------------

[CLASS] CertificationData
Full Name: Autodesk.AutoCAD.GraphicsSystem.CertificationData

  CONSTRUCTORS:
    new CertificationData()

  PROPERTIES:
    string CardName { get; set; }
    int CertificationStatus { get; set; }
    string DriverVersion { get; set; }
    string FailedDevices { get; set; }
    int HardwareFeatureLevel { get; set; }
    int HardwareID { get; set; }
    string ProductDriverURL { get; set; }
    int SoftwareFeatureLevel { get; set; }
    string TuningDate { get; set; }

--------------------------------------------------------------------------------

[STRUCT] ClientViewInfo
Full Name: Autodesk.AutoCAD.GraphicsSystem.ClientViewInfo

  CONSTRUCTORS:
    new ClientViewInfo()

  PROPERTIES:
    int AcadWindowId { get; set; }
    int ViewportId { get; set; }
    int ViewportObjectId { get; set; }

--------------------------------------------------------------------------------

[CLASS] Configuration
Full Name: Autodesk.AutoCAD.GraphicsSystem.Configuration
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Configuration()

  PROPERTIES:
    bool AdaptiveDegradation { get; set; }
    DriverInfo CurrentDisplayDriver { get; }
    int CurveTessellationTolerance { get; set; }
    DriverInfo DefaultHardwareAcceleratedDriver { get; }
    bool DiscardBackFaces { get; set; }
    string DriverName { get; }
    int DriverRevision { get; }
    string DriverSearchPath { get; set; }
    int DriverVersion { get; }
    bool DynamicTessellation { get; set; }
    int FrameRate { get; set; }
    bool GenerateVertexNormals { get; set; }
    Handedness Handedness { get; set; }
    DriverInfo HardwareAcceleratedDriver { get; set; }
    int MaxLODs { get; set; }
    bool RedrawOnWindowExpose { get; set; }
    int SurfaceTessellationTolerance { get; set; }
    Quality Transparency { get; set; }

  METHODS:
    bool Configure()
    static Configuration Create(IntPtr unmanagedPointer, bool autoDelete)
    int DegradationChainPosition(DegradationChannel channel)
    DegradationChannel DegradationChannelAt(int order)
    bool GetCanDegradeChannel(DegradationChannel channel)
    CertificationData GetCertificationData()
    List<EffectStatus> GetEffectList(EffectListType type)
    int GetFeatureAvailableVal(ulong A_0)
    FeatureModeType GetFeatureMode()
    int GetFeatureModeAvailable()
    int GetFeatureRecommendedVal(ulong A_0)
    int GetFeatureUsedVal(ulong A_0)
    DriverInfo[] GetHardwareAcceleratedDrivers()
    string GetVirtualDeviceName()
    bool IsFeatureAvailable(ulong A_0)
    bool IsFeatureEnabled(ulong A_0)
    bool IsFeatureRecommended(ulong A_0)
    bool IsHardwareAccelerationAvailable()
    bool IsHardwareAccelerationEnabled()
    bool IsHardwareAccelerationRecommended()
    bool IsInConfiguring()
    bool IsNoHardwareOverrideEnabled()
    void Restore()
    void SaveToRegistry()
    void SetCanDegradeChannel(DegradationChannel channel, bool degrade)
    void SetFeatureEnabled(ulong __unnamed000, bool enable)
    void SetFeatureMode(FeatureModeType type)
    void SetFeatureUsedVal(ulong __unnamed000, int value)
    void setHardwareAcceleration(bool bEnable)
    void ShiftDegradationChainPosition(DegradationChannel channel, bool shiftDown)
    bool ShowConfigDialog(string dlgType)

--------------------------------------------------------------------------------

[CLASS] ConfigWasModifiedEventHandler
Full Name: Autodesk.AutoCAD.GraphicsSystem.ConfigWasModifiedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ConfigWasModifiedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[ENUM] DefaultLightingType
Full Name: Autodesk.AutoCAD.GraphicsSystem.DefaultLightingType
Inherits: Enum

  Values:
    - OneLight = 0
    - TwoLights = 1

--------------------------------------------------------------------------------

[ENUM] DegradationChannel
Full Name: Autodesk.AutoCAD.GraphicsSystem.DegradationChannel
Inherits: Enum

  Values:
    - ViewportDraw = 0
    - LineAntialias = 1
    - Lighting = 2
    - TransparencyQuality = 3
    - ShadowsFull = 4
    - Transparency = 5
    - DiscardBackfaces = 6
    - ShadowsGround = 7
    - EdgeStyles = 8
    - FacetEdges = 9
    - FastSilhouette = 10
    - Textures = 11
    - Materials = 12
    - LightingQuality = 13
    - Backgrounds = 14
    - IntersectEdges = 15
    - Faceted = 16
    - Wireframe = 17
    - DegradationChannels = 18

--------------------------------------------------------------------------------

[CLASS] Device
Full Name: Autodesk.AutoCAD.GraphicsSystem.Device
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Device(IntPtr hwnd, DeviceType deviceType)
    new Device(IntPtr hwnd)
    new Device()

  PROPERTIES:
    Color BackgroundColor { get; set; }
    RendererType DeviceRenderType { get; set; }
    bool IsValid { get; }

  METHODS:
    bool Add(View view)
    static Device Create(IntPtr unmanagedPointer, bool autoDelete, IntPtr graphicsKernelPointer)
    static Device Create(IntPtr unmanagedPointer, bool autoDelete)
    bool Erase(View view)
    void EraseAll()
    [SKIPPED METHOD] GetSnapshot: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    void Invalidate(Rectangle rect)
    void Invalidate()
    void OnDisplayChange(int bitsPerPixel, int xRes, int yRes)
    void OnRealizeBackgroundPalette()
    void OnRealizeForegroundPalette()
    void OnSize(Size size)
    void SetLogicalPalette(Color[] palette)
    void SetPhysicalPalette(Color[] palette)
    void Update(Rectangle updatedRect)
    void Update()

--------------------------------------------------------------------------------

[ENUM] DeviceType
Full Name: Autodesk.AutoCAD.GraphicsSystem.DeviceType
Inherits: Enum

  Values:
    - ScreenDevice = 0
    - OffScreenDevice = 1
    - SelectionDevice = 2

--------------------------------------------------------------------------------

[STRUCT] DriverInfo
Full Name: Autodesk.AutoCAD.GraphicsSystem.DriverInfo

  CONSTRUCTORS:
    new DriverInfo(bool hardwareAccelerated, string path, string driver)
    new DriverInfo(string path, string driver)

  PROPERTIES:
    string Driver { get; }
    bool HardwareAccelerated { get; }
    string Path { get; }

--------------------------------------------------------------------------------

[ENUM] EffectListType
Full Name: Autodesk.AutoCAD.GraphicsSystem.EffectListType
Inherits: Enum

  Values:
    - kEL_Current = 0
    - kEL_RegistryHardware = 1
    - kEL_RegistrySoftware = 2
    - kEL_File = 3
    - kEL_HardwareBasic = 4
    - kEL_HardwareMedium = 5
    - kEL_HardwareAdvanced = 6

--------------------------------------------------------------------------------

[CLASS] EffectStatus
Full Name: Autodesk.AutoCAD.GraphicsSystem.EffectStatus

  CONSTRUCTORS:
    new EffectStatus()

  PROPERTIES:
    int Available { get; set; }
    string EffectName { get; set; }
    ulong EffectUniqueString { get; set; }
    int Enabled { get; set; }
    int FeatureLevel { get; set; }
    int Recommended { get; set; }

  EVENTS:
    event EventHandler EffectOnChanged

--------------------------------------------------------------------------------

[ENUM] ErrorStatus
Full Name: Autodesk.AutoCAD.GraphicsSystem.ErrorStatus
Inherits: Enum

  Values:
    - Success = 0
    - OutOfRange = 1
    - InvalidInput = 2

--------------------------------------------------------------------------------

[ENUM] FeatureModeType
Full Name: Autodesk.AutoCAD.GraphicsSystem.FeatureModeType
Inherits: Enum

  Values:
    - kFM_Software = 0
    - kFM_Basic = 1
    - kFM_Medium = 2
    - kFM_Advanced = 3
    - kFM_Unknown = 65535
    - kFM_Start = -3
    - kFM_Start = -3
    - kFM_ClassicDx9 = -2
    - kFM_ClassicDx11 = -1

--------------------------------------------------------------------------------

[CLASS] GetInterfaceFunction
Full Name: Autodesk.AutoCAD.GraphicsSystem.GetInterfaceFunction
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetInterfaceFunction(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(int objectId, bool bNeedsValidation, AsyncCallback callback, object obj)
    Drawable EndInvoke(IAsyncResult result)
    Drawable Invoke(int objectId, bool bNeedsValidation)

--------------------------------------------------------------------------------

[CLASS] GraphicsKernel
Full Name: Autodesk.AutoCAD.GraphicsSystem.GraphicsKernel
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new GraphicsKernel(IntPtr unmanagedPointer)
    new GraphicsKernel()

--------------------------------------------------------------------------------

[CLASS] GsToBeUnloadedEventHandler
Full Name: Autodesk.AutoCAD.GraphicsSystem.GsToBeUnloadedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GsToBeUnloadedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[ENUM] Handedness
Full Name: Autodesk.AutoCAD.GraphicsSystem.Handedness
Inherits: Enum

  Values:
    - Left = 0
    - Right = 1

--------------------------------------------------------------------------------

[ENUM] HighlightStyle
Full Name: Autodesk.AutoCAD.GraphicsSystem.HighlightStyle
Inherits: Enum

  Values:
    - HighlightDashed = 0

--------------------------------------------------------------------------------

[ENUM] InvalidationHint
Full Name: Autodesk.AutoCAD.GraphicsSystem.InvalidationHint
Inherits: Enum

  Values:
    - InvalidateNone = 0
    - InvalidateIsolines = 1
    - InvalidateViewportCache = 2
    - InvalidateAll = 3
    - InvalidateAllStatic = 4
    - InvalidateFacets = 5
    - InvalidateFills = 6
    - InvalidateLinetypes = 7
    - InvalidateMaterials = 8

--------------------------------------------------------------------------------

[CLASS] KernelDescriptor
Full Name: Autodesk.AutoCAD.GraphicsSystem.KernelDescriptor
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new KernelDescriptor()

  METHODS:
    void addRequirement(ulong A_0)
    bool supports(ulong A_0)

  CONSTANTS/STATIC FIELDS:
    const ulong Drawing2D
    const ulong Drawing3D
    const ulong Drawing3D2
    const ulong Selection3D
    const ulong RapidRTRendering3D

--------------------------------------------------------------------------------

[ENUM] LinePattern
Full Name: Autodesk.AutoCAD.GraphicsSystem.LinePattern
Inherits: Enum

  Values:
    - Solid = 0
    - Dashed = 1
    - DefaultLinePattern = 2
    - DefaultLinePattern = 2
    - DashDot = 3
    - ShortDash = 4
    - MediumDash = 5
    - LongDash = 6
    - DoubleShortDash = 7
    - DoubleMediumDash = 8
    - DoubleLongDash = 9
    - MediumLongDash = 10
    - MediumDashShortDashShortDash = 11
    - LongDashShortDash = 12
    - LongDashDotDot = 13
    - LongDashDot = 14
    - MediumDashDotShortDashDot = 15
    - SparseDot = 16

--------------------------------------------------------------------------------

[ENUM] LineWeight
Full Name: Autodesk.AutoCAD.GraphicsSystem.LineWeight
Inherits: Enum

  Values:
    - kLnWt000 = 0
    - kLnWt005 = 5
    - kLnWt009 = 9
    - kLnWt013 = 13
    - kLnWt015 = 15
    - kLnWt018 = 18
    - kLnWt020 = 20
    - kLnWt025 = 25
    - kLnWt030 = 30
    - kLnWt035 = 35
    - kLnWt040 = 40
    - kLnWt050 = 50
    - kLnWt053 = 53
    - kLnWt060 = 60
    - kLnWt070 = 70
    - kLnWt080 = 80
    - kLnWt090 = 90
    - kLnWt100 = 100
    - kLnWt106 = 106
    - kLnWt120 = 120
    - kLnWt140 = 140
    - kLnWt158 = 158
    - kLnWt200 = 200
    - kLnWt211 = 211
    - kLnWtByLwDefault = -3
    - kLnWtByBlock = -2
    - kLnWtByLayer = -1

--------------------------------------------------------------------------------

[CLASS] Manager
Full Name: Autodesk.AutoCAD.GraphicsSystem.Manager
Inherits: DisposableWrapper

  PROPERTIES:
    GraphicsKernel[] ActiveGraphicsKernels { get; }
    Model[] ActiveModels { get; }
        [SKIPPED PROPERTY] DeviceIndependentDisplaySize: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  METHODS:
    static GraphicsKernel AcquireGraphicsKernel(KernelDescriptor desc)
    Device CreateAutoCADDevice(GraphicsKernel __unnamed000, IntPtr hwnd)
    Model CreateAutoCADModel(GraphicsKernel A_0)
    Device CreateAutoCADOffScreenDevice(GraphicsKernel A_0)
    View CreateAutoCADView(GraphicsKernel __unnamed000, Drawable drawable)
    View CreateAutoCADViewport(GraphicsKernel __unnamed000, ViewportTableRecord vp)
    View CreateView(Device A_0)
    View CreateViewport(Device __unnamed000, ViewportTableRecord vp)
    void DestroyView(View view, ViewportTableRecord vp)
    Model[] GetActiveModels(Database db)
    View GetCurrent3dAcGsView(int viewportNumber)
    View GetCurrentAcGsView(int viewportNumber)
    Model GetDBModel(GraphicsKernel A_0)
    Model GetGsModel(TransientDrawingMode mode, int subDrawingMode, int viewportNumber)
    Device GetGUIDevice(GraphicsKernel A_0)
    Device GetOffScreenDevice(GraphicsKernel A_0)
    View GetOffScreenView(GraphicsKernel A_0, ClientViewInfo A_1)
    View ObtainAcGsView(int viewportNumber, KernelDescriptor createIfNone)
    static void ReleaseGraphicsKernel(GraphicsKernel kernel)
    void SetViewFromViewport(View view, int viewportNumber)
    void SetViewportFromView(int viewportNumber, View view, bool regenRequired, bool rescaleRequired, bool syncRequired)
    void SetViewportFromView(int viewportNumber, View view)

  EVENTS:
    event ConfigWasModifiedEventHandler ConfigWasModified
    event GsToBeUnloadedEventHandler GsToBeUnloaded
    event ViewWasUpdatedEventHandler ViewWasUpdated
    event ViewToBeUpdatedEventHandler ViewToBeUpdated
    event ViewToBeDestroyedEventHandler ViewToBeDestroyed
    event ViewWasCreatedEventHandler ViewWasCreated

--------------------------------------------------------------------------------

[CLASS] Model
Full Name: Autodesk.AutoCAD.GraphicsSystem.Model
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Model(RenderType renderType)

  PROPERTIES:
    ObjectId BackgroundId { get; set; }
    bool EnableSectioning { get; set; }
    bool LinetypesEnabled { get; set; }
    RenderType RenderType { get; }
    ObjectId SectioningVisualStyle { set; }
    bool Selectable { get; set; }
    double ShadowPlaneLocation { get; set; }
    Matrix3d Transform { get; set; }
    bool ViewClippingOverride { set; }
    VisualStyle VisualStyle { get; set; }
    ObjectId VisualStyleId { get; set; }

  METHODS:
    bool AddSceneGraphRoot(Drawable pRoot)
    static Model Create(IntPtr unmanagedPointer, bool autoDelete, IntPtr graphicsKernelPointer)
    static Model Create(IntPtr unmanagedPointer, bool autoDelete)
    bool EraseSceneGraphRoot(Drawable pRoot)
    void Invalidate(InvalidationHint hint)
    void OnAdded(Drawable added, int parentId)
    void OnAdded(Drawable added, Drawable parent)
    void OnErased(Drawable erased, int parentId)
    void OnErased(Drawable erased, Drawable parent)
    void OnModified(Drawable modified, int parentId)
    void OnModified(Drawable modified, Drawable parent)
    void OnPaletteModified()
    void SetExtents(Point3d point3dx, Point3d point3dy)
    bool SetSectioning(Point3dCollection pts, Vector3d upVector, double top, double bottom)
    bool SetSectioning(Point3dCollection pts, Vector3d upVector)

--------------------------------------------------------------------------------

[CLASS] Node
Full Name: Autodesk.AutoCAD.GraphicsSystem.Node
Inherits: DisposableWrapper

  PROPERTIES:
    Drawable Drawable { get; }

--------------------------------------------------------------------------------

[ENUM] Projection
Full Name: Autodesk.AutoCAD.GraphicsSystem.Projection
Inherits: Enum

  Values:
    - Parallel = 0
    - Perspective = 1

--------------------------------------------------------------------------------

[ENUM] Quality
Full Name: Autodesk.AutoCAD.GraphicsSystem.Quality
Inherits: Enum

  Values:
    - LowQuality = 0
    - MediumQuality = 1
    - HighQuality = 2

--------------------------------------------------------------------------------

[CLASS] ReleaseInterfaceFunction
Full Name: Autodesk.AutoCAD.GraphicsSystem.ReleaseInterfaceFunction
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ReleaseInterfaceFunction(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(Drawable __unnamed000, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(Drawable A_0)

--------------------------------------------------------------------------------

[ENUM] RendererType
Full Name: Autodesk.AutoCAD.GraphicsSystem.RendererType
Inherits: Enum

  Values:
    - Default = 0
    - Software = 1
    - SoftwareNewViewsOnly = 2
    - FullRender = 3
    - SelectionRender = 4

--------------------------------------------------------------------------------

[ENUM] RenderType
Full Name: Autodesk.AutoCAD.GraphicsSystem.RenderType
Inherits: Enum

  Values:
    - Main = 0
    - Sprite = 1
    - Direct = 2
    - Highlight = 3
    - HighlightSelection = 4
    - DirectTopmost = 5
    - Contrast = 6
    - Count = 7

--------------------------------------------------------------------------------

[STRUCT] StereoParameters
Full Name: Autodesk.AutoCAD.GraphicsSystem.StereoParameters

  CONSTRUCTORS:
    new StereoParameters(double magnitude, double parallax)

  PROPERTIES:
    double Magnitude { get; }
    double Parallax { get; }

--------------------------------------------------------------------------------

[CLASS] View
Full Name: Autodesk.AutoCAD.GraphicsSystem.View
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new View(ClientViewInfo clientViewInfo, bool enableLayerVisibilityPerView)
    new View()

  PROPERTIES:
    int AcadWindowId { get; }
    double BackClip { get; set; }
    ObjectId BackgroundId { get; set; }
    Device Device { get; }
    bool EnableBackClip { get; set; }
    bool EnableFrontClip { get; set; }
    bool EnableStereo { get; set; }
    bool ExceededBounds { get; }
    double FieldHeight { get; }
    double FieldWidth { get; }
    double FrontClip { get; set; }
    bool IsInteractive { get; }
    bool IsPerspective { get; }
    bool IsValid { get; }
    bool IsVisible { get; }
    Matrix3d ObjectToDeviceMatrix { get; }
    Point3d Position { get; }
    Matrix3d ProjectionMatrix { get; }
    Matrix3d ScreenMatrix { get; }
    StereoParameters StereoParameters { get; set; }
    Point3d Target { get; }
    Vector3d UpVector { get; }
    Matrix3d ViewingMatrix { get; }
    ViewportBorderProperties ViewportBorderProperties { get; set; }
    bool ViewportBorderVisibility { get; set; }
    Extents2d ViewportExtents { get; set; }
    IntPtr ViewportId { get; }
    ObjectId ViewportObjectId { get; }
    VisualStyle VisualStyle { get; set; }
    ObjectId VisualStyleId { get; set; }
    Matrix3d WorldToDeviceMatrix { get; }

  METHODS:
    bool Add(Drawable drawable, Model model)
    void BeginInteractivity(double frameRateInHZ)
    void ClearFrozenLayers()
    View Clone()
    View Clone(bool cloneViewParameters, bool cloneGeometry)
    static View Create(IntPtr unmanagedPointer, bool autoDelete, bool autoCADView, IntPtr graphicsKernelPointer)
    static View Create(IntPtr unmanagedPointer, bool autoDelete, bool autoCADView)
    void Dolly(double x, double y, double z)
    void Dolly(Vector3d vector)
    void EnableDefaultLighting(bool bEnable)
    void EnableDefaultLighting(bool bEnable, DefaultLightingType type)
    void EndInteractivity()
    bool Erase(Drawable drawable)
    void EraseAll()
    bool ExtentsInView(Point3d minPoint, Point3d maxPoint)
    void Flush()
    void FreezeLayer(IntPtr layerId)
    void FreezeLayer(long layerId)
    Model GetModel(Drawable drw)
    Model[] GetModelList()
    Point2d GetNumPixelsInUnitSquare(Point3d givenWorldpt)
    Point2d GetNumPixelsInUnitSquare(Point3d givenWorldpt, bool includePerspective)
    [SKIPPED METHOD] GetSnapshot: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    void Hide()
    void Invalidate(Rectangle rect)
    void Invalidate()
    void InvalidateCachedViewportGeometry()
    void Orbit(double x, double y)
    void Pan(double x, double y)
    bool PointInView(Point3d point)
    void RemoveViewportClipRegion()
    [SKIPPED METHOD] RenderToImage: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] RenderToImage: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] RenderToImage: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    void Roll(double angle)
    void SetView(Point3d position, Point3d target, Vector3d upVector, double fieldWidth, double fieldHeight)
    void SetView(Point3d position, Point3d target, Vector3d upVector, double fieldWidth, double fieldHeight, Projection projection)
    void SetViewportClipRegion(int contours, int[] counts, Point[] points)
    void Show()
    void ThawLayer(IntPtr layerId)
    void ThawLayer(long layerId)
    void Update()
    void Zoom(double factor)
    void ZoomExtents(Point3d minPoint, Point3d maxPoint)
    void ZoomWindow(Point2d lowerLeft, Point2d upperRight)

--------------------------------------------------------------------------------

[CLASS] ViewEventArgs
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new ViewEventArgs()

  PROPERTIES:
    View View { get; }

--------------------------------------------------------------------------------

[STRUCT] ViewportBorderProperties
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewportBorderProperties

  CONSTRUCTORS:
    new ViewportBorderProperties(Color color, int weight)

  PROPERTIES:
    Color Color { get; }
    int Weight { get; }

--------------------------------------------------------------------------------

[CLASS] ViewToBeDestroyedEventHandler
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewToBeDestroyedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ViewToBeDestroyedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ViewEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ViewEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ViewToBeUpdatedEventHandler
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewToBeUpdatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ViewToBeUpdatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ViewUpdateEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ViewUpdateEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ViewUpdateEventArgs
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewUpdateEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new ViewUpdateEventArgs()

  PROPERTIES:
    View View { get; }
    ViewUpdateFlags viewUpdateFlags { get; }

--------------------------------------------------------------------------------

[ENUM] ViewUpdateFlags
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewUpdateFlags
Inherits: Enum

  Values:
    - CameraChanged = 1

--------------------------------------------------------------------------------

[CLASS] ViewWasCreatedEventHandler
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewWasCreatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ViewWasCreatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ViewEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ViewEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ViewWasUpdatedEventHandler
Full Name: Autodesk.AutoCAD.GraphicsSystem.ViewWasUpdatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ViewWasUpdatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ViewUpdateEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ViewUpdateEventArgs e)

--------------------------------------------------------------------------------

