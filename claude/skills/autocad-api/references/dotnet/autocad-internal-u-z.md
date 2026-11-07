# Autodesk.AutoCAD.Internal

NAMESPACE: Autodesk.AutoCAD.Internal
--------------------------------------------------------------------------------

[CLASS] UiCallback
Full Name: Autodesk.AutoCAD.Internal.UiCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new UiCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[ENUM] UndoBoundaryEnum
Full Name: Autodesk.AutoCAD.Internal.UndoBoundaryEnum
Inherits: Enum

  Values:
    - SubCommandBoundary = 4
    - CommandBoundaryStart = 8
    - CommandBoundaryEnd = 16
    - SubCommandWall = 2048

--------------------------------------------------------------------------------

[CLASS] UnmanagedResource
Full Name: Autodesk.AutoCAD.Internal.UnmanagedResource

  CONSTRUCTORS:
    new UnmanagedResource()

  PROPERTIES:
    static IntPtr AcadResourceHandle { get; }

  METHODS:
    static IntPtr GetResourceHandle(string moduleName)
    static string LoadString(IntPtr resourceHandle, int resourceId)

--------------------------------------------------------------------------------

[CLASS] UpdateTitleBarEventHandler
Full Name: Autodesk.AutoCAD.Internal.UpdateTitleBarEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new UpdateTitleBarEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] UpdateUIEventHandler
Full Name: Autodesk.AutoCAD.Internal.UpdateUIEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new UpdateUIEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, IPEUpdateUIEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, IPEUpdateUIEventArgs e)

--------------------------------------------------------------------------------

[CLASS] Utils
Full Name: Autodesk.AutoCAD.Internal.Utils

  CONSTRUCTORS:
    new Utils()

  PROPERTIES:
    static     [SKIPPED PROPERTY] ApplicationStatusBarMenu: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static     [SKIPPED PROPERTY] ApplicationToolbarsMenu: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    bool CaptureOnLayoutSwitch { set; }
    static bool ImpliedSelectionIsActive { get; }
    static bool IsCuiCommandEnabled { get; }
    static bool IsDocumentOpening { get; }
    static bool IsEditorReady { get; }
    static bool IsOsThemed { get; }
    static bool IsScriptActive { get; }
    static bool IsTrialUser { get; }
    static string SysVarInProgress { get; }

  METHODS:
    static void ActivateDocument(IntPtr hWndDoc)
    static void AddCommand(string cmdGroupName, string cmdGlobalName, string cmdLocalName, CommandFlags cmdFlags, CommandCallback func)
    static string AngleToString(double dValue)
    static string ApplyAcValueFormat(object objValue, string formatString)
    static string CallButtonEditor(string resId, string packageName)
    [SKIPPED METHOD] CallButtonEditorWithBitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static bool CancelAndRunCmds(string pStrCmd)
    static void CloseCommandLine()
    [SKIPPED METHOD] ConvertBitmapToAcGiImageBGRA32: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] ConvertBitmapToAcGiImageBGRA32Ex: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] ConvertCMenuToMenuItem: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ConvertOSnapCMenuToMenuItem: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static object CreateCommandToolTip(ValueType pTooltipInfo)
    static IntPtr CreateNativeTrayItemInstance()
    static IDisposable CUIBeginBatch()
    static void CUIBitmapAdd(string group)
    [SKIPPED METHOD] CUIBitmapGet: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static void CUIBitmapRemove(string group)
    static bool CUIIsUsingSmallIcon()
    static void CUISaveMenuAndToolbarState()
    static bool DeleteDimConstraints(Database db, bool bKeepAnnotativeDimensions)
    static bool DisableUndoRecording(Database db, bool bDisable)
    static void Do_Cmd(string menuString, bool IsSSetNeedRestore, bool bMenuMacro, bool IsHiddenCmd, bool bSynchronous)
    static void DoHelpForCommand(string cmdName)
    static void DragBackAsMDIChildFrame(IntPtr handle)
    static void DragOutDwgFrameWindow(IntPtr handle)
    static IntPtr DrawLineType(ObjectId type, int width, int height)
    static IntPtr DrawLineWeight(LineWeight weight, int width, int height)
    static IntPtr DrawLineWeightLine(LineWeight lineWeight, int left, int top, int right, int bottom)
    static void DropOpenFile(string file)
    static void EnableDockControlBars(bool bEnable)
    static void EnableDWGResize(bool flag)
    static void EnableFloatingWindows(bool bEnable)
    static void EnableSysButtons(bool bEnable)
    [SKIPPED METHOD] EnsureVisible: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static ObjectId EntFirst()
    static ObjectId EntLast()
    static ObjectId EntNext(ObjectId entId, bool skipSubEnt)
    static ObjectId EntNext(ObjectId entId)
    static bool EvaluateTopLevelNetwork(Database db, bool bRelaxEvaluate)
    static void ExecuteApplicationStatusBarMenu(int nId)
    static void FlushGraphics()
    static IntPtr GetAcadFrameHandle()
    static int GetActualIndex(int index)
    static IntPtr GetApplicationFrameHWnd()
    static IntPtr GetBlockImage(ObjectId objectId, int nImgWidth, int nImgHeight, Color backgroundColor)
    static string GetClientId()
    static string GetCommandAtLevelForDocument(int level)
    static string GetCommandPromptString()
    static int GetCommandVersion()
    static ObjectId GetCurrentEditBlock()
    static string GetCurrentFindingContent()
    static Color GetCurrentObjectColor()
    static ObjectId GetCurrentViewportVisualStyleId()
    static int GetDbmodFromDocument(Document doc)
    static string GetDieselEvalString(string text, ref bool bGrayed, ref bool bChecked)
    static string GetDieselEvalString(string text)
    static IntPtr GetDimStyleImage(ObjectId objectId, int nImgWidth, int nImgHeight, Color backgroundColor)
    [SKIPPED METHOD] GetDockClientRect: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] GetDocWinClientRect: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] GetDwgFrameIcon: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static IntPtr GetFontImage(ObjectId fontID)
    static string GetGradientDisplayText(int nHatchGradientNameEnum)
    static string GetGradientDisplayText(string gradientName)
    static string GetGradientName(string displayText)
    static int GetGradientValue(string displayText)
    static bool GetHideWarningDialogs(uint nType)
    static List<string> GetLastCommandLines(int lastLines, bool ignoreNull)
    static void GetLastInsertBlockData(ref bool specifyScaleOnScreen, ref bool specifyRotationOnScreen)
    [SKIPPED METHOD] GetLayoutThumbnail: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static IntPtr GetMLeaderStyleImage(ObjectId objectId, int nImgWidth, int nImgHeight, Color backgroundColor)
    static bool GetMoreHideWarningDialogs(uint nType)
    static IntPtr GetOpmWindow()
    static string GetPredefinedVisualStyleGlobalName(string localName)
    static string GetProductbrandingName()
    static IntPtr GetQpWindow()
    static IntPtr GetRealHatchPreviewImage(int nWidth, int nHeight)
    static ObservableCollection<string> GetRedoHistory()
    static string GetRefEditName()
    [SKIPPED METHOD] GetStatusBarOsnapMenu: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static IntPtr GetTableCellStyleImage(string cellStyleName, int nImgWidth, int nImgHeight, Color backgroundColor)
    static IntPtr GetTableStyleImage(ObjectId objectId, int nImgWidth, int nImgHeight, Color backgroundColor)
    static Point2d GetTextExtents(ObjectId styleId, string text, double dHeight)
    static bool GetToolPaletteGroups(string schemeName, out StringCollection pGroups)
    static ObservableCollection<string> GetUndoHistory()
    static double GetUnitsConversion(UnitsValue fromUnits, UnitsValue toUnits)
    static IntPtr GetUnknownPatternSwatchImage(int nWidth, int nHeight)
    static IntPtr GetUserDefinedSwatchImage(bool bDouble, Color patternColor, Color backgroundColor, int nWidth, int nHeight)
    [SKIPPED METHOD] GetVisibleAndInActiveTopFrame: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static Color GetVisualStyleEdgeColor(ValueType visualStyleId)
    [SKIPPED METHOD] GetVisualStyleImage: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static Color GetVisualStyleIntersectionEdgeColor(ValueType visualStyleId)
    static Color GetVisualStyleObscuredEdgeColor(ValueType visualStyleId)
    [SKIPPED METHOD] GetVisualStyles: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string GetWSUID(string sWSName)
    static bool HistoryStatus()
    static string IconFilePath()
    static bool InitDialog(bool bUseDialog)
    static void InitializeCommandLineFont()
    [SKIPPED METHOD] InvokeDataLinkManagerDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] InvokeDataTypeDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    static OPMStatus InvokeOpmSetPropertyValue(object prop, object val, ObjectIdCollection objIds, Guid guid, OPMModeFlags mode)
    static void InvokeOptionsDialog(string strPos, bool bInvokedAsChildDlg, uint defaultLabelIdInFileTab, bool bExpanded)
    static void InvokeStatusBarItemDeleted(IntPtr ptr)
    [SKIPPED METHOD] InvokeStatusBarItemMouseDown: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static void InvokeTableStyleDialog()
    static void InvokeTrayItemCloseBubbleWindow(IntPtr trayItemPtr)
    static void InvokeTrayItemShowBubbleWindow(string title, string description, BubbleWindowIconType iconType, string hyperlink, string hypertext, string text, IntPtr trayItemPtr)
    static bool Is3dVisualStyle(string styleName)
    static bool IsAllowedToFloatDwgFrames()
    static bool IsAssociativeArrayRibbonContextApplicable(string type, object dataItem)
    static bool IsCommandActive(string name)
    static bool IsCommandDefined(string cmdName)
    static CommandTypeFlags IsCommandNameInUse(string name)
    static bool IsCommandReEntered(string name)
    static bool IsCoreCommand(string name)
    static bool IsDiesel(string text)
    static bool IsDocumentInBlockEditor(Document doc)
    static bool IsDroppableExtension(string extension)
    static bool IsFirstRun()
    static bool IsFlagOn(uint id, bool bDefault)
    static bool IsInBlockEditor()
    static bool IsInCommandStack(CommandFlags flags)
    static bool IsInCustomizeMode()
    static bool IsInDragging()
    static bool IsInPaperSpace()
    static bool IsInputPending()
    static bool IsInQuiescentState()
    static bool IsInStartup()
    static bool IsInTilemode()
    static bool IsLinkedObjectExist(Document doc)
    static bool IsLispCommandDefined(string name)
    static bool IsMultiRedoAvaliable()
    static bool IsNewTabCommandAllowed()
    static bool IsNonDrawingDocumentDisabled()
    static bool IsOEM()
    static bool IsOverrideActive()
    static bool IsPasteClipCommandAllowed()
    static bool IsPointOverToolbar(int x, int y)
    static bool IsTextEditorActive()
    static bool IsUndoAvailable()
    static bool IsVisualStyleDefault(ValueType visualStyleId)
    static void NewDocument()
    static bool NotifyStatusBarServiceReady()
    static string NumberToUnitsString(double dValue)
    static void OpenDocument()
    static void Orbit()
    static void Pan()
    static bool PointOnScreen(Point3d pt)
    static void PostCommandPrompt()
    static bool PostRouteMessageToDwgView(IntPtr handle, int message, IntPtr wParam, IntPtr lParam)
    static bool PreRouteMessageToDwgView(IntPtr handle, int message, IntPtr wParam, IntPtr lParam)
    static string RawAngleToString(double dValue)
    static void RedrawGrips()
    static void RegenEntity(ObjectId entId)
    static void RegisterCommandLineWindow(IntPtr window)
    static bool RegisterLeaveStartupUiCallback(UiCallback func)
    static void RegisterLispCommand(string cmdName, string lispName)
    static bool RegisterStartupUiCallback(double ordering, UiCallback func, int delayInMs)
    static void ReloadMenus(string wsCurrentStr, bool bResetWorkspace, bool bIncrementalReloading)
    static void RemoveCommand(string cmdGroupName, string cmdGlobalName)
    static void RestoreApplicationStatusBar()
    static void RunFindText(string pStrCmd)
    static bool SelectObjects(ObjectId[] ids)
    static void SendMenuStringToExecute(Document doc, string menuString, bool bEchoString)
    static void SetApplicationStatusBarProgressMeter(int nPos)
    static void SetApplicationStatusBarProgressMeter(string str, int minimum, int maximum)
    static void SetCurrentViewportVisualStyle(ObjectId visualStyleId)
    static bool SetCurrentWorkspace(string wsCurrent, bool bRestoreUI, bool bAutoSaveWS)
    static void SetCursor(Cursor cursor)
    static bool SetDbmod(int nDbmod, Database db)
    static void SetFocusToDwgView()
    static void SetHelpContext(string context, string metadata)
    static void SetHideWarningDialogs(uint nType, bool bValue)
    static void SetMoreHideWarningDialogs(uint nType, bool bValue)
    static void SetPropertiesInDatabaseForTPPlayback(Guid itemId, bool bSet)
    static void SetQnewTemplateAfterInitialSetup()
    static void SetQuickViewWindowHandle(IntPtr hWnd)
    static void SetRecoverAllMode(bool bOn)
    static void SetRecoverRequestedMode(bool bOn)
    static void SetStatusBarContainerHeight(int height)
    [SKIPPED METHOD] SetStatusBarContent: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool SetUndoMark(bool begin)
    static void ShowHideTextWindow(bool bShow)
    static void ShowLineTypeDialog()
    static MeshConversionType ShowMeshConversionTaskDialog()
    static bool StartToolbarDragDrop(StringCollection menuGroups, StringCollection names, StringCollection tags, StringCollection commands, StringCollection helpStrs, StringCollection smallImages, StringCollection largeImages, StringCollection resourceFiles)
    static int StrCompareNatural(string str1, string str2)
    static bool StringToAngle(string sText, ref double dValue)
    static bool StringToRawAngle(string sText, ref double dValue)
    static void TransferToolbarBitmaps()
    static Point3d UcsToDisplay(Point3d ucsPoint, bool bPaperSpace)
    static bool UnitsStringToNumber(string sText, ref double dValue)
    static void UnregisterCommandLineWindow(IntPtr window)
    [SKIPPED METHOD] UpdateDockingTool: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool ViewHasChanged()
    static void ViewportChange(int x, int y)
    static void ViewportCycle()
    static void ViewportResize(float sx, float sy, float ex, float ey, bool split)
    static void ViewportTypeChange(int id)
    static ObjectId visualStyleId(string visualStyleName)
    static string visualStyleName(ObjectId id)
    static void VpMaxCommand()
    static void VpMinCommand()
    static bool WcMatchEx(string str, string pattern, bool ignoreCase)
    static bool WorkspaceVisible(string strWS)
    static void WriteToCommandLine(string message)
    static bool WriteUndoBoundary(Database db, UndoBoundaryEnum boundary, string name)
    static void Zoom()
    static void ZoomAuto(short type, double a1, double a2, double a3, double a4)
    static void ZoomObjects(bool use3d)

  EVENTS:
    event EventHandler<StartupToolsEventArgs> SavingStartupTools

--------------------------------------------------------------------------------

[CLASS] ViewUtil
Full Name: Autodesk.AutoCAD.Internal.ViewUtil

  CONSTRUCTORS:
    new ViewUtil()

  PROPERTIES:
    static double GsLensLength { get; set; }

  METHODS:
    static void acquireView(int nVPNum, AcGsView** pView, AcGsGraphicsKernel** pGraphicsKernel)
    static int GetCurrentViewportNumber()
    static void releaseView(AcGsView** pView, AcGsGraphicsKernel** pGraphicsKernel)

--------------------------------------------------------------------------------

[CLASS] VisualStyleSet
Full Name: Autodesk.AutoCAD.Internal.VisualStyleSet
Inherits: RXObject

  CONSTRUCTORS:
    new VisualStyleSet()

  METHODS:
    void Intialize(ObjectIdCollection objIds, VisualStyleSet pRemoveFromSet, Color pOverrideColor)
    void RemoveAll()
    void Replace(ObjectIdCollection objIds, VisualStyleSet pRemoveFromSet)

--------------------------------------------------------------------------------

[CLASS] WebServiceMultipartRequest
Full Name: Autodesk.AutoCAD.Internal.WebServiceMultipartRequest

  CONSTRUCTORS:
    new WebServiceMultipartRequest(HttpWebRequest request, string boundary)
    new WebServiceMultipartRequest(HttpWebRequest request)

  PROPERTIES:
    Dictionary<string, string> Parameters { get; }
    HttpWebRequest Request { get; }

  METHODS:
    void AddFileContentPart(Stream fileStream, string paramName, string fileName, string contentType)
    void AddFileContentPart(Stream fileStream, string name, string fileName)
    void BeginMultipartContent()
    void EndMultipartContent()

--------------------------------------------------------------------------------

[CLASS] WebServicePostRequest
Full Name: Autodesk.AutoCAD.Internal.WebServicePostRequest

  CONSTRUCTORS:
    new WebServicePostRequest(HttpWebRequest request)
    new WebServicePostRequest(HttpWebRequest request, bool skipParameterFormatting)

  PROPERTIES:
    Dictionary<string, string> Parameters { get; }
    HttpWebRequest Request { get; }

  METHODS:
    void UpdateRequestContent()

--------------------------------------------------------------------------------

[CLASS] WSUtils
Full Name: Autodesk.AutoCAD.Internal.WSUtils

  CONSTRUCTORS:
    new WSUtils()

  METHODS:
    static HttpClientHandler CreateHttpClientProxyHandler(string uri)
    static string GetLoginUserName()
    static string GetO2tk(string scopes)
    static bool GetProxyAuthentication(string url, ref string userName, ref string password, ref string server, ref string port)
    static Server GetServer()
    static bool IsLoggedIn()
    static bool IsLoginValid()
    static bool Login()

--------------------------------------------------------------------------------

[CLASS] ZipExtractor
Full Name: Autodesk.AutoCAD.Internal.ZipExtractor
Implements: IDisposable

  CONSTRUCTORS:
    new ZipExtractor(string zipFile, string password)
    new ZipExtractor(string zipFile)

  PROPERTIES:
    IEnumerable<ZipFileInfo> Files { get; }

  METHODS:
    void Dispose()
    void Extract(ZipFileInfo file, string destination)
    void ExtractAll(string destination)

--------------------------------------------------------------------------------

