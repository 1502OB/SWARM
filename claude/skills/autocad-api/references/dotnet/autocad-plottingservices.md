# Autodesk.AutoCAD.PlottingServices

NAMESPACE: Autodesk.AutoCAD.PlottingServices
--------------------------------------------------------------------------------

[ENUM] AppPlotStatus
Full Name: Autodesk.AutoCAD.PlottingServices.AppPlotStatus
Inherits: Enum

  Values:
    - PlotStart = 0
    - ViewPlotLog = 1
    - PlotSuccessful = 2
    - PlotHadErrors = 3
    - PlotHadSystemError = 4
    - DwfFilePlotted = -3
    - PlottingMessage = -2
    - NoPlotYet = -1

--------------------------------------------------------------------------------

[CLASS] BeginDocumentEventArgs
Full Name: Autodesk.AutoCAD.PlottingServices.BeginDocumentEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BeginDocumentEventArgs(PlotInfo plotInfo, string documentName, int copies, bool plotToFile, string fileName)

  PROPERTIES:
    int Copies { get; }
    string DocumentName { get; }
    string FileName { get; }
    PlotInfo PlotInfo { get; }
    bool PlotToFile { get; }

--------------------------------------------------------------------------------

[CLASS] BeginDocumentEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.BeginDocumentEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginDocumentEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginDocumentEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginDocumentEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginPageEventArgs
Full Name: Autodesk.AutoCAD.PlottingServices.BeginPageEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BeginPageEventArgs(PlotPageInfo plotPageInfo, PlotInfo plotInfo, bool lastPage)

  PROPERTIES:
    bool LastPage { get; }
    PlotInfo PlotInfo { get; }
    PlotPageInfo PlotPageInfo { get; }

--------------------------------------------------------------------------------

[CLASS] BeginPageEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.BeginPageEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginPageEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginPageEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginPageEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginPlotEventArgs
Full Name: Autodesk.AutoCAD.PlottingServices.BeginPlotEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BeginPlotEventArgs(PlotProgress progress, PlotType plotType)

  PROPERTIES:
    PlotProgress PlotProgress { get; }
    PlotType PlotType { get; }

--------------------------------------------------------------------------------

[CLASS] BeginPlotEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.BeginPlotEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginPlotEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginPlotEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginPlotEventArgs e)

--------------------------------------------------------------------------------

[ENUM] CustomSizeResults
Full Name: Autodesk.AutoCAD.PlottingServices.CustomSizeResults
Inherits: Enum

  Values:
    - Possible = 0
    - MustCreatePC3 = 1
    - RotationRequired = 2
    - PC3DirReadOnly = 4
    - PmpDirReadOnly = 8
    - PmpDirMissing = 16
    - UnknownErrPmpDir = 32
    - PC3FileReadOnly = 64
    - SizeTooBig = 128
    - Exception = 256
    - UnknownErrPC3File = 512
    - UnknownErrPmpFile = 1024
    - PmpFileReadOnly = 2048
    - WidthAndHeightMustBePositive = 4096
    - DeviceLoadFailed = 8192

--------------------------------------------------------------------------------

[ENUM] DeviceType
Full Name: Autodesk.AutoCAD.PlottingServices.DeviceType
Inherits: Enum

  Values:
    - SystemPrinter = 0
    - PC3File = 1
    - OneOffConfig = 2
    - Uninitialized = -1

--------------------------------------------------------------------------------

[CLASS] DsdData
Full Name: Autodesk.AutoCAD.PlottingServices.DsdData
Inherits: RXObject

  CONSTRUCTORS:
    new DsdData()

  PROPERTIES:
    string CategoryName { get; set; }
    string DestinationName { get; set; }
    Dwf3dOptions Dwf3dOptions { get; }
    bool InitializeLayouts { get; set; }
    bool IsHomogeneous { get; set; }
    bool IsSheetSet { get; set; }
    string LogFilePath { get; set; }
    int MajorVersion { get; set; }
    int MinorVersion { get; set; }
    int NoOfCopies { get; set; }
    string Password { get; set; }
    bool PlotStampOn { get; set; }
    string ProjectPath { get; set; }
    bool PromptForDwfName { get; set; }
    string SelectionSetName { get; set; }
    string SheetSetName { get; set; }
    SheetType SheetType { get; set; }
    StringCollection UnrecognizedDataSectionNames { get; }
    StringCollection UnrecognizedDataSections { get; }

  METHODS:
    DsdData Copy()
    DsdEntryCollection GetDsdEntryCollection()
    void ReadDsd(string fileName)
    void SetDsdEntryCollection(DsdEntryCollection entries)
    void SetUnrecognizedData(StringCollection sectionNames, StringCollection sectionData)
    void SetUnrecognizedData(string sectionName, string sectionData)
    void WriteDsd(string fileName)

--------------------------------------------------------------------------------

[CLASS] DsdEntry
Full Name: Autodesk.AutoCAD.PlottingServices.DsdEntry
Inherits: RXObject

  CONSTRUCTORS:
    new DsdEntry()

  PROPERTIES:
    string DwgName { get; set; }
    string Layout { get; set; }
    string Nps { get; set; }
    string NpsSourceDwg { get; set; }
    string OriginalSheetPath { get; }
    string Title { get; set; }
    string TraceSession { get; set; }

  METHODS:
    DsdEntry Copy()

--------------------------------------------------------------------------------

[CLASS] DsdEntryCollection
Full Name: Autodesk.AutoCAD.PlottingServices.DsdEntryCollection
Inherits: DisposableWrapper
Implements: ICollection, IEnumerable

  CONSTRUCTORS:
    new DsdEntryCollection()

  PROPERTIES:
    int Count { get; }
    DsdEntry Item { get; set; }

  METHODS:
    int Add(DsdEntry value)
    void Clear()
    void CopyTo(DsdEntry[] array, int index)
    IEnumerator GetEnumerator()
    void Insert(int index, DsdEntry value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] Dwf3dOptions
Full Name: Autodesk.AutoCAD.PlottingServices.Dwf3dOptions

  PROPERTIES:
    bool GroupByXrefHierarchy { get; set; }
    bool PublishWithMaterials { get; set; }

--------------------------------------------------------------------------------

[CLASS] EndDocumentEventArgs
Full Name: Autodesk.AutoCAD.PlottingServices.EndDocumentEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new EndDocumentEventArgs(PlotCancelStatus status)

  PROPERTIES:
    PlotCancelStatus Status { get; }

--------------------------------------------------------------------------------

[CLASS] EndDocumentEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.EndDocumentEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndDocumentEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EndDocumentEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EndDocumentEventArgs e)

--------------------------------------------------------------------------------

[CLASS] EndPageEventArgs
Full Name: Autodesk.AutoCAD.PlottingServices.EndPageEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new EndPageEventArgs(SheetCancelStatus status)

  PROPERTIES:
    SheetCancelStatus Status { get; }

--------------------------------------------------------------------------------

[CLASS] EndPageEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.EndPageEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndPageEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EndPageEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EndPageEventArgs e)

--------------------------------------------------------------------------------

[CLASS] EndPlotEventArgs
Full Name: Autodesk.AutoCAD.PlottingServices.EndPlotEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new EndPlotEventArgs(PlotCancelStatus status)

  PROPERTIES:
    PlotCancelStatus Status { get; }

--------------------------------------------------------------------------------

[CLASS] EndPlotEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.EndPlotEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndPlotEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EndPlotEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EndPlotEventArgs e)

--------------------------------------------------------------------------------

[CLASS] HostAppServices
Full Name: Autodesk.AutoCAD.PlottingServices.HostAppServices

  PROPERTIES:
    static PlotLogger PlotLogger { get; }

  METHODS:
    static void UpdatePlotJobStatus(AppPlotStatus status, string message)

--------------------------------------------------------------------------------

[ENUM] MatchingPolicy
Full Name: Autodesk.AutoCAD.PlottingServices.MatchingPolicy
Inherits: Enum

  Values:
    - MatchDisabled = 1
    - MatchEnabled = 2
    - MatchEnabledCustom = 3
    - MatchEnabledTemporaryCustom = 4

--------------------------------------------------------------------------------

[STRUCT] MediaBounds
Full Name: Autodesk.AutoCAD.PlottingServices.MediaBounds

  CONSTRUCTORS:
    new MediaBounds(Point2d pageSize, Point2d lowerLeftPrintableArea, Point2d upperRightPrintableArea)

  PROPERTIES:
    Point2d LowerLeftPrintableArea { get; set; }
    Point2d PageSize { get; set; }
    Point2d UpperRightPrintableArea { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(MediaBounds a)
    bool IsEqualTo(MediaBounds a, Tolerance tolerance)
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[ENUM] MergeStatus
Full Name: Autodesk.AutoCAD.PlottingServices.MergeStatus
Inherits: Enum

  Values:
    - NoChanges = 0
    - PlotConfigurationName = 1
    - PlotPaperMargins = 2
    - PlotPaperSize = 4
    - CanonicalMediaName = 8
    - PlotOrigin = 16
    - PlotPaperUnits = 32
    - PlotViewportBorders = 64
    - PlotPlotStyles = 128
    - ShowPlotStyles = 256
    - PlotRotation = 512
    - PlotCentered = 1024
    - PlotHidden = 2048
    - ShadePlot = 4096
    - ShadePlotResLevel = 8192
    - ShadePlotCustomDpi = 16384
    - PlotType = 32768
    - PlotWindowArea = 65536
    - PlotViewName = 131072
    - Scale = 262144
    - CurrentStyleSheet = 524288
    - ScaleLineWeights = 1048576
    - PrintLineWeights = 2097152
    - DrawViewportsFirst = 4194304
    - PlotTransparency = 8388608

--------------------------------------------------------------------------------

[CLASS] PageCancelledEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.PageCancelledEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PageCancelledEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] PlotCancelledEventHandler
Full Name: Autodesk.AutoCAD.PlottingServices.PlotCancelledEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PlotCancelledEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[ENUM] PlotCancelStatus
Full Name: Autodesk.AutoCAD.PlottingServices.PlotCancelStatus
Inherits: Enum

  Values:
    - Continue = 0
    - CanceledByCaller = 1
    - CanceledByCancelAllButton = 2

--------------------------------------------------------------------------------

[CLASS] PlotConfig
Full Name: Autodesk.AutoCAD.PlottingServices.PlotConfig
Inherits: RXObject

  PROPERTIES:
    StringCollection CanonicalMediaNames { get; }
    string Comment { get; }
    string DefaultFileExtension { get; }
    string DeviceName { get; }
    int DeviceType { get; }
    string DriverName { get; }
    string FullPath { get; }
    bool IsPlotToFile { get; set; }
    string LocationName { get; }
    int MaximumDeviceDotsPerInch { get; }
    PlotToFileCapability PlotToFileCapability { get; }
    string PortName { get; }
    string ServerName { get; }
    string TagLine { get; }

  METHODS:
    string GetLocalMediaName(string canonicalMediaName)
    MediaBounds GetMediaBounds(string canonicalMediaName)
    void RefreshMediaNameList()
    void SaveToPC3(string name)

--------------------------------------------------------------------------------

[CLASS] PlotConfigInfo
Full Name: Autodesk.AutoCAD.PlottingServices.PlotConfigInfo
Inherits: RXObject

  CONSTRUCTORS:
    new PlotConfigInfo()

  PROPERTIES:
    string DeviceName { get; set; }
    DeviceType DeviceType { get; set; }
    string FullPath { get; set; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[CLASS] PlotConfigInfoCollection
Full Name: Autodesk.AutoCAD.PlottingServices.PlotConfigInfoCollection
Inherits: DisposableWrapper
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    PlotConfigInfo Item { get; }

  METHODS:
    void CopyTo(PlotConfigInfo[] array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] PlotConfigManager
Full Name: Autodesk.AutoCAD.PlottingServices.PlotConfigManager

  PROPERTIES:
    static StringCollection ColorDependentPlotStyles { get; }
    static PlotConfig CurrentConfig { get; }
    static PlotConfigInfoCollection Devices { get; }
    static StringCollection NamedPlotStyles { get; }
    static string StdConfigNames { get; }

  METHODS:
    static void RefreshList(RefreshCode refreshCode)
    static PlotConfig SetCurrentConfig(string deviceName)

--------------------------------------------------------------------------------

[CLASS] PlotEngine
Full Name: Autodesk.AutoCAD.PlottingServices.PlotEngine
Inherits: DisposableWrapper

  PROPERTIES:
    bool IsBackgroundPackaging { get; }

  METHODS:
    void BeginDocument(PlotInfo plotInfo, string documentName, object parameters, int copies, bool plotToFile, string fileName)
    void BeginGenerateGraphics(object parameters)
    void BeginPage(PlotPageInfo pageInfo, PlotInfo plotInfo, bool lastPage, object parameters)
    void BeginPlot(PlotProgress plotProgress, object parameters)
    void Destroy()
    void EndDocument(object parameters)
    void EndGenerateGraphics(object parameters)
    void EndPage(object parameters)
    void EndPlot(object parameters)

--------------------------------------------------------------------------------

[CLASS] PlotFactory
Full Name: Autodesk.AutoCAD.PlottingServices.PlotFactory

  PROPERTIES:
    static ProcessPlotState ProcessPlotState { get; }

  METHODS:
    static PlotEngine CreatePreviewEngine(int previewFlags)
    static PlotEngine CreatePublishEngine()

--------------------------------------------------------------------------------

[CLASS] PlotInfo
Full Name: Autodesk.AutoCAD.PlottingServices.PlotInfo
Inherits: RXObject

  CONSTRUCTORS:
    new PlotInfo()

  PROPERTIES:
    PlotConfig DeviceOverride { get; set; }
    bool IsValidated { get; }
    ObjectId Layout { get; set; }
    int MergeStatus { get; }
    PlotSettings OverrideSettings { get; set; }
    PlotConfig ValidatedConfig { get; set; }
    PlotSettings ValidatedSettings { get; set; }

  METHODS:
    bool IsCompatibleDocument(PlotInfo info)

--------------------------------------------------------------------------------

[CLASS] PlotInfoValidator
Full Name: Autodesk.AutoCAD.PlottingServices.PlotInfoValidator
Inherits: RXObject

  CONSTRUCTORS:
    new PlotInfoValidator()

  PROPERTIES:
    int DimensionalWeight { get; set; }
    int MediaBoundsWeight { get; set; }
    int MediaGroupWeight { get; set; }
    MatchingPolicy MediaMatchingPolicy { get; set; }
    int MediaMatchingThreshold { get; set; }
    int PrintableBoundsWeight { get; set; }
    int SheetDimensionalWeight { get; set; }
    int SheetMediaGroupWeight { get; set; }

  METHODS:
    int IsCustomPossible(PlotInfo info)
    void Validate(PlotInfo info)

--------------------------------------------------------------------------------

[CLASS] PlotLogger
Full Name: Autodesk.AutoCAD.PlottingServices.PlotLogger
Inherits: DisposableWrapper

  PROPERTIES:
    bool ErrorHasHappenedInJob { get; }
    bool ErrorHasHappenedInSheet { get; }
    bool WarningHasHappenedInJob { get; }
    bool WarningHasHappenedInSheet { get; }

  METHODS:
    void EndJob()
    void EndSheet()
    void LogAbortRetryIgnoreError(string message)
    void LogError(string message)
    void LogInformation(string message)
    void LogMessage(string message)
    void LogSevereError(string message)
    void LogTerminalError(string message)
    void LogWarning(string message)
    void StartJob()
    void StartSheet()

--------------------------------------------------------------------------------

[ENUM] PlotMessageIndex
Full Name: Autodesk.AutoCAD.PlottingServices.PlotMessageIndex
Inherits: Enum

  Values:
    - DialogTitle = 0
    - SheetName = 1
    - SheetNameToolTip = 2
    - Status = 3
    - SheetProgressCaption = 4
    - SheetSetProgressCaption = 5
    - MessageCanceling = 6
    - MessageCancelingCurrent = 7
    - CancelSheetButtonMessage = 8
    - CancelJobButtonMessage = 9
    - MessageCount = 10

--------------------------------------------------------------------------------

[CLASS] PlotPageInfo
Full Name: Autodesk.AutoCAD.PlottingServices.PlotPageInfo
Inherits: RXObject

  CONSTRUCTORS:
    new PlotPageInfo()

  PROPERTIES:
    int EntityCount { get; }
    int GradientCount { get; }
    int OleObjectCount { get; }
    int RasterCount { get; }
    int ShadedViewportType { get; }

--------------------------------------------------------------------------------

[CLASS] PlotProgress
Full Name: Autodesk.AutoCAD.PlottingServices.PlotProgress
Inherits: DisposableWrapper

  PROPERTIES:
    bool IsPlotCancelled { get; }
    bool IsSheetCancelled { get; }
    bool IsVisible { get; set; }
    int LowerPlotProgressRange { get; set; }
    int LowerSheetProgressRange { get; set; }
    PlotCancelStatus PlotCancelStatus { get; set; }
    int PlotProgressPos { get; set; }
    SheetCancelStatus SheetCancelStatus { get; set; }
    int SheetProgressPos { get; set; }
    string StatusMsgString { get; set; }
    int UpperPlotProgressRange { get; set; }
    int UpperSheetProgressRange { get; set; }

  METHODS:
    void Heartbeat()

--------------------------------------------------------------------------------

[CLASS] PlotProgressDialog
Full Name: Autodesk.AutoCAD.PlottingServices.PlotProgressDialog
Inherits: PlotProgress

  CONSTRUCTORS:
    new PlotProgressDialog(bool isPreview, int sheetCount, bool showCancelSheetButton)

  PROPERTIES:
    bool IsSingleSheetPlot { get; }
    string PlotMsgString { get; set; }

  METHODS:
    void Destroy()
    void OnBeginPlot()
    void OnBeginSheet()
    void OnEndPlot()
    void OnEndSheet()

--------------------------------------------------------------------------------

[CLASS] PlotReactorManager
Full Name: Autodesk.AutoCAD.PlottingServices.PlotReactorManager

  CONSTRUCTORS:
    new PlotReactorManager()

  EVENTS:
    event PageCancelledEventHandler PageCancelled
    event PlotCancelledEventHandler PlotCancelled
    event EndPageEventHandler EndPage
    event EndDocumentEventHandler EndDocument
    event EndPlotEventHandler EndPlot
    event BeginPageEventHandler BeginPage
    event BeginDocumentEventHandler BeginDocument
    event BeginPlotEventHandler BeginPlot

--------------------------------------------------------------------------------

[ENUM] PlotToFileCapability
Full Name: Autodesk.AutoCAD.PlottingServices.PlotToFileCapability
Inherits: Enum

  Values:
    - NoPlotToFile = 0
    - PlotToFileAllowed = 1
    - MustPlotToFile = 2

--------------------------------------------------------------------------------

[ENUM] PlotType
Full Name: Autodesk.AutoCAD.PlottingServices.PlotType
Inherits: Enum

  Values:
    - Plot = 0
    - Preview = 1
    - BackgroundPackaging = 2
    - BackgroundPlot = 3

--------------------------------------------------------------------------------

[CLASS] PreviewEndPlotInfo
Full Name: Autodesk.AutoCAD.PlottingServices.PreviewEndPlotInfo
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new PreviewEndPlotInfo()

  PROPERTIES:
    PreviewEndPlotStatus Status { get; }

--------------------------------------------------------------------------------

[ENUM] PreviewEndPlotStatus
Full Name: Autodesk.AutoCAD.PlottingServices.PreviewEndPlotStatus
Inherits: Enum

  Values:
    - Normal = 0
    - Plot = 1
    - Cancel = 2
    - Next = 3
    - Previous = 4

--------------------------------------------------------------------------------

[ENUM] PreviewEngineFlags
Full Name: Autodesk.AutoCAD.PlottingServices.PreviewEngineFlags
Inherits: Enum

  Values:
    - Plot = 1
    - NextSheet = 2
    - PreviousSheet = 4

--------------------------------------------------------------------------------

[ENUM] ProcessPlotState
Full Name: Autodesk.AutoCAD.PlottingServices.ProcessPlotState
Inherits: Enum

  Values:
    - NotPlotting = 0
    - ForegroundPlotting = 1
    - BackgroundPlotting = 2

--------------------------------------------------------------------------------

[ENUM] RefreshCode
Full Name: Autodesk.AutoCAD.PlottingServices.RefreshCode
Inherits: Enum

  Values:
    - All = 0
    - RefreshDevicesList = 1
    - RefreshStyleList = 2
    - RefreshSystemDevicesList = 3
    - RefreshPC3DevicesList = 4

--------------------------------------------------------------------------------

[ENUM] SheetCancelStatus
Full Name: Autodesk.AutoCAD.PlottingServices.SheetCancelStatus
Inherits: Enum

  Values:
    - Continue = 0
    - CanceledByCancelButton = 1
    - CanceledByCancelAllButton = 2
    - CanceledByCaller = 3

--------------------------------------------------------------------------------

[ENUM] SheetType
Full Name: Autodesk.AutoCAD.PlottingServices.SheetType
Inherits: Enum

  Values:
    - SingleDwf = 0
    - MultiDwf = 1
    - OriginalDevice = 2
    - SingleDwfx = 3
    - MultiDwfx = 4
    - SinglePdf = 5
    - MultiPdf = 6

--------------------------------------------------------------------------------

[ENUM] StdConfiguration
Full Name: Autodesk.AutoCAD.PlottingServices.StdConfiguration
Inherits: Enum

  Values:
    - NoneDevice = 0
    - DefaultWindowsSysPrinter = 1
    - Dwf6ePlot = 2
    - DwfEplotOptForPlotting = 3
    - DwfEplotOptForViewing = 4
    - PublishToWebDwf = 5
    - PublishToWebJpg = 6
    - PublishToWebPng = 7
    - DWFxePlot = 8
    - PublishToWebDWFx = 9

--------------------------------------------------------------------------------

