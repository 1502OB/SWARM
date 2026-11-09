# Autodesk.AutoCAD.Internal

NAMESPACE: Autodesk.AutoCAD.Internal
--------------------------------------------------------------------------------

[CLASS] NamedViewUtilities
Full Name: Autodesk.AutoCAD.Internal.NamedViewUtilities

  CONSTRUCTORS:
    new NamedViewUtilities()

  METHODS:
    static IntPtr GetNamedViewImage(ObjectId objectId, int imageWidth, int imageHeight, Color backgroundColor)

--------------------------------------------------------------------------------

[CLASS] NavigationUtils
Full Name: Autodesk.AutoCAD.Internal.NavigationUtils

  CONSTRUCTORS:
    new NavigationUtils()

  METHODS:
    static int GetNavStatus()
    static bool IsNavActive()
    static void NavPauseRecording()
    static void NavPlayAnimation()
    static void NavRecordAnimation()
    static void NavSaveAnimation()
    static void NavShowAnimationSetting()

--------------------------------------------------------------------------------

[CLASS] NavisworksFileInfo
Full Name: Autodesk.AutoCAD.Internal.NavisworksFileInfo

  CONSTRUCTORS:
    new NavisworksFileInfo(NavisworksFileInfo obj)
    new NavisworksFileInfo()

--------------------------------------------------------------------------------

[ENUM] NavisworksFileType
Full Name: Autodesk.AutoCAD.Internal.NavisworksFileType
Inherits: Enum

  Values:
    - Aggregate = 0
    - Model = 1
    - Error = 2

--------------------------------------------------------------------------------

[CLASS] NavisworksServiceManager
Full Name: Autodesk.AutoCAD.Internal.NavisworksServiceManager

  CONSTRUCTORS:
    new NavisworksServiceManager()

  PROPERTIES:
    static INavisworksService Host { get; set; }

--------------------------------------------------------------------------------

[CLASS] NewTabPageUtil
Full Name: Autodesk.AutoCAD.Internal.NewTabPageUtil

  CONSTRUCTORS:
    new NewTabPageUtil()

  METHODS:
    static bool SwitchToStartPage()

--------------------------------------------------------------------------------

[CLASS] NonMonitoredSysvarEnumerator
Full Name: Autodesk.AutoCAD.Internal.NonMonitoredSysvarEnumerator
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new NonMonitoredSysvarEnumerator(IntPtr p)

  METHODS:
    MonitoredSysvarWrapper Current()
    bool Done()
    void MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] ObjectContexts
Full Name: Autodesk.AutoCAD.Internal.ObjectContexts

  CONSTRUCTORS:
    new ObjectContexts()

  METHODS:
    static void AddContext(DBObject obj, ObjectContext ctxt)
    static List<ObjectContext> GetContexts(DBObject obj, string collectionName)
    static bool HasContext(DBObject obj, ObjectContext ctxt)
    static void RemoveContext(DBObject obj, ObjectContext ctxt)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] OnBoardingStateContextBase
Full Name: Autodesk.AutoCAD.Internal.OnBoardingStateContextBase
Implements: IDisposable

  CONSTRUCTORS:
    new OnBoardingStateContextBase()

  METHODS:
    void Dispose()
    static string getBlockSyncFolder()
    static string getBlockSyncFolderFromServer(int timeout)
    static int getConsentStatus(int timeout)
    static bool getOnBoardingCompleted()
    bool isStateTransitionLocked()
    void lockStateTransition()
    static MessageResultEnum outputMessage(string title, string mgs, MessageIconEnum icon, MessageButtonEnum btn)
    static void setBlockSyncFolder(string val)
    static void setConsentStatus(int status)
    static void setOnBoardingCompleted()
    void unlockStateTransition()

  CONSTANTS/STATIC FIELDS:
    const getBlockSyncFolderFromServerHandler blockSyncFolderFromServerGetter
    const getConsentStatusHandler consentStatusGetter
    const setConsentStatusHandler consentStatusSetter
    const setOnBoardingCompletedHandler onboardingCompletedSetter
    const getOnBoardingCompletedHandler onboardingCompletedGetter
    const getValueAsStringHandler blockSyncFolderGetter
    const setValueAsStringHandler blockSyncFolderSetter
    const outputMessageHandler messagePrinter

--------------------------------------------------------------------------------

[ENUM] OnBoardingStateContextEnum
Full Name: Autodesk.AutoCAD.Internal.OnBoardingStateContextEnum
Inherits: Enum

  Values:
    - kRecent = 0
    - kFavorites = 1

--------------------------------------------------------------------------------

[ENUM] OPMModeFlags
Full Name: Autodesk.AutoCAD.Internal.OPMModeFlags
Inherits: Enum

  Values:
    - Selection = 1
    - ZeroSelection = 2
    - FromCommand = 4
    - InvokeDialog = 8
    - HatchPattern = 16
    - InvokeHatchDialog = 24

--------------------------------------------------------------------------------

[ENUM] OPMStatus
Full Name: Autodesk.AutoCAD.Internal.OPMStatus
Inherits: Enum

  Values:
    - OK = 0
    - InvalidProperty = 1
    - InvalidPropertyValue = 2
    - InvalidPropertyType = 3
    - InvalidPropertyCategory = 4
    - Failed = 5

--------------------------------------------------------------------------------

[CLASS] outputMessageHandler
Full Name: Autodesk.AutoCAD.Internal.outputMessageHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new outputMessageHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(string __unnamed000, string __unnamed001, MessageIconEnum __unnamed002, MessageButtonEnum __unnamed003, AsyncCallback callback, object obj)
    MessageResultEnum EndInvoke(IAsyncResult result)
    MessageResultEnum Invoke(string A_0, string A_1, MessageIconEnum A_2, MessageButtonEnum A_3)

--------------------------------------------------------------------------------

[CLASS] PerfLog
Full Name: Autodesk.AutoCAD.Internal.PerfLog

  CONSTRUCTORS:
    new PerfLog()

  METHODS:
    static bool IsEventEnabled(int eventId)
    static bool WriteEvent(int eventId, string param1, string param2)
    static bool WriteEvent(int eventId, string param)
    static bool WriteEvent(int eventId)

--------------------------------------------------------------------------------

[ENUM] PreviewControlType
Full Name: Autodesk.AutoCAD.Internal.PreviewControlType
Inherits: Enum

  Values:
    - RibbonItemControl = 1
    - PropertyPaletteControl = 2

--------------------------------------------------------------------------------

[CLASS] ProductInfo
Full Name: Autodesk.AutoCAD.Internal.ProductInfo

  CONSTRUCTORS:
    new ProductInfo(ref AcProductInfo product)

  PROPERTIES:
    string highlightsFile { get; set; }
    string info { get; set; }
    string name { get; set; }
    string productRootKey { get; set; }
    ValueType update { get; set; }
    string version { get; set; }

--------------------------------------------------------------------------------

[ENUM] RecentBlockSerializeState
Full Name: Autodesk.AutoCAD.Internal.RecentBlockSerializeState
Inherits: Enum

  Values:
    - kInMemory = 0
    - kSerializable = 1
    - kSerializing = 2
    - kSerialized = 3
    - kDeleted = 4

--------------------------------------------------------------------------------

[CLASS] RecentDocumentAction
Full Name: Autodesk.AutoCAD.Internal.RecentDocumentAction

  CONSTRUCTORS:
    new RecentDocumentAction()

  PROPERTIES:
    static RecentDocumentActionCollection<RecentDocumentAction> Collection { get; }
    string Id { get; }
    string Text { get; }

  METHODS:
    bool CanExecute(string document)
    bool Execute(string document)

  CONSTANTS/STATIC FIELDS:
    const string IdOpen = ACAD.Open
    const string IdOpenReadOnly = ACAD.OpenReadOnly
    const string IdRemove = ACAD.Remove

--------------------------------------------------------------------------------

[CLASS] RecentDocumentActionCollection`1
Full Name: Autodesk.AutoCAD.Internal.RecentDocumentActionCollection`1
Inherits: ReadOnlyCollection`1
Implements: INotifyCollectionChanged, INotifyPropertyChanged
Type Parameters: <T>

  CONSTRUCTORS:
    new RecentDocumentActionCollection`1(bool dynamic)

  PROPERTIES:
    T Item { get; }
    T Open { get; }
    T OpenReadOnly { get; }
    T Remove { get; }

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged
    event NotifyCollectionChangedEventHandler CollectionChanged

--------------------------------------------------------------------------------

[CLASS] ScaleListHelpers
Full Name: Autodesk.AutoCAD.Internal.ScaleListHelpers

  CONSTRUCTORS:
    new ScaleListHelpers()

  METHODS:
    static string getDefaultScaleList(bool returnPredefinedScales, int measurement, List<string> scaleStr, List<double> scaleNum, List<double> scaleDenom, int[] scaleCount)
    static void GetReferencedScaleNames(Database db, ObjectIdCollection scaleIds, List<string> scaleStr, int[] scaleCount)
    static string GetUnitScaleName(Database db)
    static void loadScaleList(ObjectIdCollection referencedScaleIds, int[] referencedScaleCount, List<string> scaleStr, List<double> scaleNum, List<double> scaleDenom, int[] scaleCount)
    static void loadScaleUnitStrings(string[] unitStrings)
    static void resetScaleList(int measurement, List<string> scaleStr, List<double> scaleNum, List<double> scaleDenom, int[] scaleCount)
    static void saveScaleList(List<string> scaleStr, List<double> scaleNum, List<double> scaleDenom, int scaleCount)
    static void setDefaultScaleList(List<string> newScaleStr, List<double> scaleNum, List<double> scaleDenom, List<int> scaleType, int scaleCount)
    static void updateScaleList(List<string> oldScaleStr, List<string> newScaleStr, List<double> scaleNum, List<double> scaleDenom, int scaleCount)

--------------------------------------------------------------------------------

[CLASS] SecUtil
Full Name: Autodesk.AutoCAD.Internal.SecUtil

  CONSTRUCTORS:
    new SecUtil()

  METHODS:
    static string GetCertificateSubjectName(string filePath)
    static long GetSecureSettingsValue()
    static bool HasCADManagerSubsetCmds()
    static bool IsCADManagerAllowed(string cmd)
    static bool IsDownloadOnlyEnabled()
    static bool IsInternetConnectionAllowed()
    static int IsSignatureValid(string filePath)

--------------------------------------------------------------------------------

[CLASS] ServicePackInfo
Full Name: Autodesk.AutoCAD.Internal.ServicePackInfo

  CONSTRUCTORS:
    new ServicePackInfo()

  METHODS:
    List<ProductInfo> getProductInfo()

--------------------------------------------------------------------------------

[CLASS] setConsentStatusHandler
Full Name: Autodesk.AutoCAD.Internal.setConsentStatusHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new setConsentStatusHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(int __unnamed000, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(int A_0)

--------------------------------------------------------------------------------

[CLASS] setOnBoardingCompletedHandler
Full Name: Autodesk.AutoCAD.Internal.setOnBoardingCompletedHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new setOnBoardingCompletedHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] SetReadOnlySysvar
Full Name: Autodesk.AutoCAD.Internal.SetReadOnlySysvar

  CONSTRUCTORS:
    new SetReadOnlySysvar()

  METHODS:
    static void SetLayerManagerState(int eState)

--------------------------------------------------------------------------------

[CLASS] setValueAsStringHandler
Full Name: Autodesk.AutoCAD.Internal.setValueAsStringHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new setValueAsStringHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(string val, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(string val)

--------------------------------------------------------------------------------

[CLASS] SharedRecentFileData
Full Name: Autodesk.AutoCAD.Internal.SharedRecentFileData

  CONSTRUCTORS:
    new SharedRecentFileData(int nAccessTime, string strProvider, string strFileName, string strPath, string strID, string strPlatform, string strProviderId, string strProviderSecId)
    new SharedRecentFileData()

--------------------------------------------------------------------------------

[CLASS] SharedRecentFiles
Full Name: Autodesk.AutoCAD.Internal.SharedRecentFiles

  CONSTRUCTORS:
    new SharedRecentFiles()

  PROPERTIES:
    static GetSharedRecentFilesCallback GetRecentFilesCallback { get; set; }
    static ShareRecentFileCallback RecentFileCallback { get; set; }

--------------------------------------------------------------------------------

[CLASS] ShareRecentFileCallback
Full Name: Autodesk.AutoCAD.Internal.ShareRecentFileCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ShareRecentFileCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(string path, int nAccessTime, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(string path, int nAccessTime)

--------------------------------------------------------------------------------

[CLASS] ShareRecentListHandler
Full Name: Autodesk.AutoCAD.Internal.ShareRecentListHandler

  CONSTRUCTORS:
    new ShareRecentListHandler()

  METHODS:
    static bool prepareShareData(string filePath, ref AcShareFileData fileData)

--------------------------------------------------------------------------------

[CLASS] SMManager
Full Name: Autodesk.AutoCAD.Internal.SMManager

  CONSTRUCTORS:
    new SMManager()

  METHODS:
    static bool CIP_LogTargetShot(string strShotName)
    static void DeleteSequenceShot(bool bSequence, string strName)
    static void EndPlay()
    static bool GetPlayStatus(out string activeShotName, out string activeSeqName, out PlayMode playMode, out PlayAction playAction, out bool bPause)
    static ValueType[] GetSequenceIds()
    static ValueType[] GetShotIds(string strName)
    static bool IsShotPlaying()
    static void LoopStatusChanged(bool bLoop)
    static void MoveSequenceShot(bool bSequence, string strName, bool bLeft)
    static void PauseOrResumePlayBack(bool bPause)
    static bool RenameSequenceShot(bool bSequence, string oldName, string newName)
    static void UIToBeHidden()
    static void UIToBeShown()
    static void UpdateCachedData()
    static bool UpdateThumbnailAll()
    static bool UpdateThumbnailSequenceShot(bool bSequence, string strName)

--------------------------------------------------------------------------------

[CLASS] SmManagerUtilities
Full Name: Autodesk.AutoCAD.Internal.SmManagerUtilities

  METHODS:
    static void SetSmManagerInterface(object mgr)

--------------------------------------------------------------------------------

[CLASS] StartupToolsEventArgs
Full Name: Autodesk.AutoCAD.Internal.StartupToolsEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new StartupToolsEventArgs()

  METHODS:
    void Add(string toolName, string commandName)

--------------------------------------------------------------------------------

[CLASS] SubSetting
Full Name: Autodesk.AutoCAD.Internal.SubSetting

  CONSTRUCTORS:
    new SubSetting()

  METHODS:
    static bool IsCommandEnabled(string command)
    static bool IsSysVarEnabled(string sysvar)

--------------------------------------------------------------------------------

[CLASS] SupportProject
Full Name: Autodesk.AutoCAD.Internal.SupportProject

  PROPERTIES:
    List<string> LoadedProjectPartialCUIs { get; }

  METHODS:
    void UnloadSharedPartialCUI(string partialCUI)

--------------------------------------------------------------------------------

[CLASS] SupportProjectManager
Full Name: Autodesk.AutoCAD.Internal.SupportProjectManager

  CONSTRUCTORS:
    new SupportProjectManager()

  PROPERTIES:
    static List<string> LoadedLocalPartialCUIs { get; }
    static string ProjectName { get; }

  METHODS:
    static SupportProject GetActiveProject()
    static void UnloadLocalPartialCUI(string partialCUI)

--------------------------------------------------------------------------------

[CLASS] SyncYourBlockHelper
Full Name: Autodesk.AutoCAD.Internal.SyncYourBlockHelper

  CONSTRUCTORS:
    new SyncYourBlockHelper()

  METHODS:
    static string getColorTheme()
    static OnBoardingStateContextBase getStateContext(OnBoardingStateContextEnum name)
    static void setStateContext(OnBoardingStateContextEnum name, OnBoardingStateContextBase ctx)
    static bool showConsent()
    static bool showGuide()
    static string showStorageDialog(string title, string defaultStr)
    static bool showSyncFolderChooser()
    static bool showSyncSettingsDialog(string title)
    static bool signIn()
    static bool walkOnBoarding()

--------------------------------------------------------------------------------

[CLASS] SysvarMonitorIteratorWrapper
Full Name: Autodesk.AutoCAD.Internal.SysvarMonitorIteratorWrapper
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new SysvarMonitorIteratorWrapper(IntPtr p)

  METHODS:
    bool isEqual(SysvarMonitorIteratorWrapper sysvarMonitorIter)
    bool isnotEqual(SysvarMonitorIteratorWrapper sysvarMonitorIter)
    SysvarMonitorIteratorWrapper next()
    MonitoredSysvarWrapper sysvar()

--------------------------------------------------------------------------------

[CLASS] SysvarMonitorWrapper
Full Name: Autodesk.AutoCAD.Internal.SysvarMonitorWrapper
Inherits: DisposableWrapper

  PROPERTIES:
    bool BalloonAlertEnabled { get; set; }
    bool Enabled { get; set; }

  METHODS:
    MonitoredSysvarWrapper addMonitoredSysvar(MonitoredSysvarWrapper sysvar)
    SysvarMonitorIteratorWrapper begin()
    static SysvarMonitorWrapper createSysvarMonitorWrapper(IntPtr p)
    SysvarMonitorIteratorWrapper end()
    NonMonitoredSysvarEnumerator getNonMonitoredSysvarEnumerator()
    bool isDifferenceDetected()
    bool isSysvarMonitored(string sysvar_name)
    void removeMonitoredSysvar(MonitoredSysvarWrapper sysvar)
    void resetMonitoredSysvar(MonitoredSysvarWrapper sysvar)

--------------------------------------------------------------------------------

[ENUM] TableItem
Full Name: Autodesk.AutoCAD.Internal.TableItem
Inherits: Enum

  Values:
    - TableItemInvalid = 0
    - TableItemCell = 1
    - TableItemRow = 2
    - TableItemColumn = 3

--------------------------------------------------------------------------------

[CLASS] TableUtilities
Full Name: Autodesk.AutoCAD.Internal.TableUtilities

  CONSTRUCTORS:
    new TableUtilities()

  METHODS:
    static bool CanDelete(Table table, CellRange selRange, bool row)
    static bool CanInsert(Table table, CellRange selRange, bool row, bool rightOrBottom)
    static bool CanMergeCells(Table table, CellRange selRange)
    static bool CanUnMergeCells(Table table, CellRange selRange)
    static Color GetBackgroundColor(Table table)
    static string GetCellStyle(Table table, CellRange selRange, TableItem item, bool localized, out bool varies)
    static string[] GetCellStyles(TableStyle tableStyle, out int count, bool localized, bool sort)
    static string[] GetCellStyles(TableStyle tableStyle, out int count)
    static void GetDataType(Table table, out DataType dateType, out UnitType unitType)
    static bool GetPickFirstTable(out ObjectId tableId, out CellRange selRange, out int tableIndex)
    static void GetReadOnlyState(Table table, CellRange selRange, out bool readOnlyContent, out bool readOnlyFormat)
    static bool GetSelectionAlignment(ValueType tableId, out CellAlignment alignment)
    static bool GetSelectionLock(Table table, out CellStates cellState)
    static bool IsDataLinkCommandAllowed()
    static bool IsFieldCommandAllowed()
    static bool IsSingleCell(Table table, CellRange selRange)
    static void SetBackgroundColor(Table table, Color color)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] TaskDescriber
Full Name: Autodesk.AutoCAD.Internal.TaskDescriber

  CONSTRUCTORS:
    new TaskDescriber(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  PROPERTIES:
        [SKIPPED PROPERTY] DisplayItem: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    int ThumbnailSize { get; }
    ThumbnailGenerateState ThumbnailState { get; set; }
    bool UseLocalServer { get; set; }

  METHODS:
    [SKIPPED METHOD] onTaskCompleted: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] TextIPEUtils
Full Name: Autodesk.AutoCAD.Internal.TextIPEUtils

  PROPERTIES:
    static double MaxmumObliqueAngle { get; }
    static double MaxmumTrackingFactor { get; }
    static double MaxmumWidthScale { get; }
    static double MinimumObliqueAngle { get; }
    static double MinimumTrackingFactor { get; }
    static double MinimumWidthScale { get; }

  METHODS:
    static bool ContinueAutoNumbering()
    static bool Diagonal()
    static IPEStateWrapper GetIPEState()
    static bool Horizontal()
    static bool MgdIPESendCommand(IPECommands command, object param1, object param2)
    static string NoUnitsNumberToString(double dValue)
    static string NumberToString(double dValue)
    static bool RedoAutoNumbering()
    static bool RestartAutoNumbering()
    static bool StackProperties()
    static bool StringToNoUnitsNumber(string sText, ref double dValue)
    static bool StringToNumber(string sText, ref double dValue)
    static bool UndoAutoNumbering()
    static bool Unstack()

--------------------------------------------------------------------------------

[CLASS] ThemeChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.ThemeChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ThemeChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[ENUM] ThumbnailGenerateState
Full Name: Autodesk.AutoCAD.Internal.ThumbnailGenerateState
Inherits: Enum

  Values:
    - kRaw = 0
    - kRequired = 1
    - kGenerating = 2
    - kGenerated = 3

--------------------------------------------------------------------------------

[CLASS] ThumbnailGenerateTask
Full Name: Autodesk.AutoCAD.Internal.ThumbnailGenerateTask
Implements: IDisposable

  CONSTRUCTORS:
    new ThumbnailGenerateTask(TaskDescriber taskDesc)

  PROPERTIES:
        [SKIPPED PROPERTY] DataHost: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    bool Executed { get; set; }
    TaskDescriber TaskDesc { get; set; }
    bool Valid { get; }
    long WaitTime { get; }

  METHODS:
    void Dispose()
    void onTaskDiscarded()
    void run()

--------------------------------------------------------------------------------

[CLASS] ThumbnailGenerateTaskQueue
Full Name: Autodesk.AutoCAD.Internal.ThumbnailGenerateTaskQueue
Implements: IDisposable

  METHODS:
    void clear()
    [SKIPPED METHOD] discard: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] discard: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void Dispose()
    bool isEmpty()
    ThumbnailGenerateTask pop()
    void push(ThumbnailGenerateTask A_0)
    static ThumbnailGenerateTaskQueue queue()
    Queue<ThumbnailGenerateTask> queueData()
    void remove(ThumbnailGenerateTask task)
    ThumbnailGenerateTask top()

--------------------------------------------------------------------------------

[CLASS] ThumbnailTaskDescriber
Full Name: Autodesk.AutoCAD.Internal.ThumbnailTaskDescriber
Inherits: TaskDescriber

  CONSTRUCTORS:
    new ThumbnailTaskDescriber(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  PROPERTIES:
    int ThumbnailSize { get; }
    ThumbnailGenerateState ThumbnailState { get; set; }
    bool UseLocalServer { get; set; }

  METHODS:
    [SKIPPED METHOD] onTaskCompleted: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] TooltipTaskDescriber
Full Name: Autodesk.AutoCAD.Internal.TooltipTaskDescriber
Inherits: TaskDescriber

  CONSTRUCTORS:
    new TooltipTaskDescriber(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  PROPERTIES:
    int ThumbnailSize { get; }
    ThumbnailGenerateState ThumbnailState { get; set; }
    bool UseLocalServer { get; set; }

  METHODS:
    [SKIPPED METHOD] onTaskCompleted: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] TraceSheet
Full Name: Autodesk.AutoCAD.Internal.TraceSheet

  CONSTRUCTORS:
    new TraceSheet()

  PROPERTIES:
    string AvatarUrl { get; set; }
    string FirstName { get; set; }
    bool IsActive { get; set; }
    bool IsOn { get; set; }
    bool IsPrimary { get; set; }
    string LastName { get; set; }
    string ModifiedAt { get; set; }
    string UserId { get; set; }
    string UserName { get; set; }

--------------------------------------------------------------------------------

[CLASS] TraceUtils
Full Name: Autodesk.AutoCAD.Internal.TraceUtils

  CONSTRUCTORS:
    new TraceUtils()

  METHODS:
    static bool currentTraceIsCheckout()
    static void HighlightSheet(string userId, bool highLight)
    static bool isAccDwgSheetMarkup()
    static bool isImportedMarkupPresent()
    static bool ToggleSheetVisibility(string userId)
    static TraceSheet[] TraceSheets()

--------------------------------------------------------------------------------

