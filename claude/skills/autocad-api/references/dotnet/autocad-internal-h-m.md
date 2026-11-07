# Autodesk.AutoCAD.Internal

NAMESPACE: Autodesk.AutoCAD.Internal
--------------------------------------------------------------------------------

[CLASS] HiddenChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.HiddenChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new HiddenChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    [SKIPPED METHOD] BeginInvoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    void EndInvoke(IAsyncResult result)
    [SKIPPED METHOD] Invoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] HPPrinter
Full Name: Autodesk.AutoCAD.Internal.HPPrinter

  CONSTRUCTORS:
    new HPPrinter()

  PROPERTIES:
    string HPUserId { get; set; }
    string PrinterId { get; set; }
    string PrinterName { get; set; }
    string WSUserId { get; set; }

--------------------------------------------------------------------------------

[CLASS] HPPrinterPlotOption
Full Name: Autodesk.AutoCAD.Internal.HPPrinterPlotOption

  CONSTRUCTORS:
    new HPPrinterPlotOption()

  PROPERTIES:
    int Copies { get; set; }
    HPPrinterPlotQuality Quality { get; set; }

--------------------------------------------------------------------------------

[ENUM] HPPrinterPlotQuality
Full Name: Autodesk.AutoCAD.Internal.HPPrinterPlotQuality
Inherits: Enum

  Values:
    - Normal = 0
    - Fast = 1
    - Best = 2

--------------------------------------------------------------------------------

[CLASS] HPPrinterPlotResult
Full Name: Autodesk.AutoCAD.Internal.HPPrinterPlotResult

  CONSTRUCTORS:
    new HPPrinterPlotResult()

  PROPERTIES:
    string Code { get; set; }
    string Message { get; set; }
    int Status { get; set; }

--------------------------------------------------------------------------------

[ENUM] HttpMethod
Full Name: Autodesk.AutoCAD.Internal.HttpMethod
Inherits: Enum

  Values:
    - Get = 0
    - Post = 1
    - Head = 2
    - Put = 3
    - Delete = 4

--------------------------------------------------------------------------------

[INTERFACE] ICloudPrintingService
Full Name: Autodesk.AutoCAD.Internal.ICloudPrintingService

  METHODS:
    HPPrinter[] ListHPPrinters()
    HPPrinterPlotResult PlotHPPrinter(string pdfFilePath, HPPrinter printer, HPPrinterPlotOption option)
    bool ShowHPUserLoginDialog()

--------------------------------------------------------------------------------

[INTERFACE] ILivePreviewControl
Full Name: Autodesk.AutoCAD.Internal.ILivePreviewControl
Implements: IPreviewContext

  PROPERTIES:
    PreviewControlType ControlType { get; set; }
    short PreviewDelay { get; set; }
    string Rule { get; set; }

--------------------------------------------------------------------------------

[INTERFACE] ILivePreviewRule
Full Name: Autodesk.AutoCAD.Internal.ILivePreviewRule

  PROPERTIES:
    bool IsPreviewAble { get; }

--------------------------------------------------------------------------------

[INTERFACE] INavisworksFileProtocol
Full Name: Autodesk.AutoCAD.Internal.INavisworksFileProtocol

  METHODS:
    void CloseFile(IntPtr filehandle)
    NavisworksFileType GetAggregateInfo(string filename, ref string aggregateJson)
    NavisworksFileInfo GetFileInfo(string filename)
    IntPtr OpenFile(string filename)
    bool ReadFile(IntPtr filehandle, ulong offset, ulong numbytes, byte[] bytes)

--------------------------------------------------------------------------------

[INTERFACE] INavisworksService
Full Name: Autodesk.AutoCAD.Internal.INavisworksService

  METHODS:
    void RegisterFileProtocol(string scheme, INavisworksFileProtocol fileprotocol)

--------------------------------------------------------------------------------

[ENUM] IPECallbackType
Full Name: Autodesk.AutoCAD.Internal.IPECallbackType
Inherits: Enum

  Values:
    - IPECallbackUpdateUI = 0
    - IPECallbackEnableUI = 1
    - IPECallbackCreateUI = 2
    - IPECallbackRemoveUI = 3
    - IPECallbackCreateToolbar = 4
    - IPECallbackRemoveToolbar = 5

--------------------------------------------------------------------------------

[ENUM] IPECharFlags
Full Name: Autodesk.AutoCAD.Internal.IPECharFlags
Inherits: Enum

  Values:
    - BoldFlag = 1
    - ItalicFlag = 2
    - UnderlineFlag = 4
    - OverlineFlag = 8
    - FontFlag = 16
    - HeightFlag = 32
    - ColorFlag = 64
    - LanguageFlag = 128
    - ObliqueAngleFlag = 256
    - WidthScaleFlag = 512
    - TrackingFactorFlag = 1024
    - FlowAlignFlag = 2048

--------------------------------------------------------------------------------

[ENUM] IPECharFlowType
Full Name: Autodesk.AutoCAD.Internal.IPECharFlowType
Inherits: Enum

  Values:
    - FlowBase = 0
    - FlowCenter = 1
    - FlowTop = 2

--------------------------------------------------------------------------------

[CLASS] IPECharStateWrapper
Full Name: Autodesk.AutoCAD.Internal.IPECharStateWrapper
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new IPECharStateWrapper()

  PROPERTIES:
    bool Bold { get; set; }
    Color Color { get; set; }
    IPECharFlags Flags { get; set; }
    IPECharFlowType FlowAlign { get; set; }
    string FontName { get; set; }
    double Height { get; set; }
    bool Italic { get; set; }
    int Language { get; set; }
    double ObliqueAngle { get; set; }
    bool Overline { get; set; }
    double TrackingFactor { get; set; }
    bool TrueType { get; set; }
    bool Underline { get; set; }
    double WidthScale { get; set; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[ENUM] IPEColumnCommandType
Full Name: Autodesk.AutoCAD.Internal.IPEColumnCommandType
Inherits: Enum

  Values:
    - IPENoColumnsCommand = 0
    - IPEStaticColumnsCommand = 1
    - IPEDynamicColumnsCommand = 2
    - IPEMoreStaticColumnsCommand = 3
    - IPEInsertColumnBreakCommand = 4
    - IPEColumnSettingsCommand = 5

--------------------------------------------------------------------------------

[ENUM] IPECommands
Full Name: Autodesk.AutoCAD.Internal.IPECommands
Inherits: Enum

  Values:
    - IPEUnknownCmd = 0
    - IPESuspendEditorCmd = 1
    - IPEReinstateEditorCmd = 2
    - IPEUndoCmd = 3
    - IPERedoCmd = 4
    - IPEStackCmd = 5
    - IPECloseCmd = 6
    - IPEToggleRulerCmd = 7
    - IPEContextMenuCmd = 8
    - IPESetStyleCmd = 9
    - IPESetFontCmd = 10
    - IPEToggleAnnotativeCmd = 11
    - IPESetTextHeightCmd = 12
    - IPESetColorCmd = 13
    - IPEToggleBoldCmd = 14
    - IPEToggleItalicCmd = 15
    - IPEToggleUnderlineCmd = 16
    - IPEToggleOverlineCmd = 17
    - IPESetObliqueAngleCmd = 18
    - IPESetTrackingFactorCmd = 19
    - IPESetWidthScaleCmd = 20
    - IPEColumnsMenuCmd = 21
    - IPEAttachmentMenuCmd = 22
    - IPELineSpacingMenuCmd = 23
    - IPENumberingMenuCmd = 24
    - IPESymbolMenuCmd = 25
    - IPEParagraphDialogCmd = 26
    - IPEFieldDialogCmd = 27
    - IPESetParagraphAlignmentCmd = 28
    - IPEChangeCaseCmd = 29
    - IPEReplaceContentsCmd = 30
    - IPEInsertMTextCmd = 31
    - IPEToggleDimPrimaryCmd = 32
    - IPEToggleDimAlternateCmd = 33
    - IPEToggleDimUnderlineCmd = 34
    - IPEToggleDimParenthesesCmd = 35
    - IPETweakDimensionCmd = 36
    - IPESetCustomStringCmd = 37
    - IPEInsertCustomStringCmd = 38
    - IPERemoveCustomStringCmd = 39
    - IPEHandleColumnsCmd = 40
    - IPEHandleJustificationCmd = 41
    - IPEHandleLineSpacingCmd = 42
    - IPEHandleSymbolCmd = 43
    - IPEHandleNumberingCmd = 44
    - IPEHandleOptionsCmd = 45
    - IPESetLayerCmd = 46

--------------------------------------------------------------------------------

[ENUM] IPEControlIconType
Full Name: Autodesk.AutoCAD.Internal.IPEControlIconType
Inherits: Enum

  Values:
    - IPEBoldButtonIcon = 0
    - IPEItalicButtonIcon = 1
    - IPEOverlineButtonIcon = 2
    - IPEUnderlineButtonIcon = 3
    - IPEObliqueAngleSpinnerIcon = 4

--------------------------------------------------------------------------------

[CLASS] IPEDimensionStateWrapper
Full Name: Autodesk.AutoCAD.Internal.IPEDimensionStateWrapper
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new IPEDimensionStateWrapper()

  PROPERTIES:
    bool AlternateFound { get; set; }
    Dimension Dimension { get; set; }
    bool EnclosedWithParentheses { get; set; }
    bool EverythingUnderlined { get; set; }
    bool PrimaryFound { get; set; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[ENUM] IPEExitType
Full Name: Autodesk.AutoCAD.Internal.IPEExitType
Inherits: Enum

  Values:
    - ExitQuit = 0
    - ExitSave = 1
    - ExitSaveAndGoRight = 2
    - ExitSaveAndGoLeft = 3
    - ExitSaveAndGoBelow = 4
    - ExitSaveAndGoAbove = 5

--------------------------------------------------------------------------------

[ENUM] IPELineSpacingCommandType
Full Name: Autodesk.AutoCAD.Internal.IPELineSpacingCommandType
Inherits: Enum

  Values:
    - IPEMultiLineSpaceCommand = 0
    - IPEMoreLineSpaceCommand = 1
    - IPEClearLineSpaceCommand = 2

--------------------------------------------------------------------------------

[ENUM] IPELineSpacingStyle
Full Name: Autodesk.AutoCAD.Internal.IPELineSpacingStyle
Inherits: Enum

  Values:
    - LineSpacingDefault = 0
    - LineSpacingExactly = 1
    - LineSpacingAtLeast = 2
    - LineSpacingMultiple = 3
    - NumLineSpacingStyles = 4

--------------------------------------------------------------------------------

[ENUM] IPENumberingCommandType
Full Name: Autodesk.AutoCAD.Internal.IPENumberingCommandType
Inherits: Enum

  Values:
    - IPENumberingOffCommand = 0
    - IPENumberingBulletCommand = 1
    - IPENumberingLetterLowCaseCommand = 2
    - IPENumberingLetterUpperCaseCommand = 3
    - IPENumberingNumberedCommand = 4
    - IPENumberingRestartCommand = 5
    - IPENumberingContinueCommand = 6
    - IPENumberingAutoListCommand = 7
    - IPENumberingTabOnlyDelimiterCommand = 8
    - IPENumberingEnabledCommand = 9

--------------------------------------------------------------------------------

[ENUM] IPENumberingType
Full Name: Autodesk.AutoCAD.Internal.IPENumberingType
Inherits: Enum

  Values:
    - NumberingOff = 0
    - NumberingBullet = 1
    - NumberingNumber = 2
    - NumberingLetterLower = 3
    - NumberingLetterUpper = 4
    - NumberingNumTypes = 5

--------------------------------------------------------------------------------

[ENUM] IPEOptionsCommandType
Full Name: Autodesk.AutoCAD.Internal.IPEOptionsCommandType
Inherits: Enum

  Values:
    - IPEInsertFieldCommand = 0
    - IPEEditFieldCommand = 1
    - IPEUpdateFieldCommand = 2
    - IPEConvertFieldCommand = 3
    - IPEImportTextCommand = 4
    - IPEConvertCustomCommand = 5
    - IPEStackCommand = 6
    - IPEUnstackCommand = 7
    - IPEStackPropertyCommand = 8
    - IPEOpaqueBackgroundCommand = 9
    - IPEParagraphCommand = 10
    - IPEFindReplaceCommand = 11
    - IPEUpperCaseCommand = 12
    - IPELowerCaseCommand = 13
    - IPEAutoCAPSCommand = 14
    - IPECombineParagraphsCommand = 15
    - IPERemoveCharaterFormattingCommand = 16
    - IPERemoveParagraphFormattingCommand = 17
    - IPERemoveAllFormattingCommand = 18
    - IPEBackgroundMaskCommand = 19
    - IPEWYSIWYGCommand = 20
    - IPEShowToolbarCommand = 21
    - IPEShowOptionsCommand = 22
    - IPEShowRulerCommand = 23
    - IPEShowRibbonCommand = 24
    - IPECheckSpellingCommand = 25
    - IPECheckSpellingSettingsCommand = 26
    - IPEDictionariesCommand = 27
    - IPETextHeightlightColorCommand = 28
    - IPENewFeaturesCommand = 29
    - IPEHelpCommand = 30
    - IPEParagraphAlignCommand = 31
    - IPECharaterSetCommand = 32

--------------------------------------------------------------------------------

[ENUM] IPEParagraphAlignmentType
Full Name: Autodesk.AutoCAD.Internal.IPEParagraphAlignmentType
Inherits: Enum

  Values:
    - AlignmentDefault = 0
    - AlignmentLeft = 1
    - AlignmentCenter = 2
    - AlignmentRight = 3
    - AlignmentJustify = 4
    - AlignmentDistribute = 5
    - NumAlignmentTypes = 6

--------------------------------------------------------------------------------

[ENUM] IPEParagraphFlags
Full Name: Autodesk.AutoCAD.Internal.IPEParagraphFlags
Inherits: Enum

  Values:
    - FirstIndentFlag = 1
    - LeftIndentFlag = 2
    - RightIndentFlag = 4
    - SpaceBeforeFlag = 8
    - SpaceAfterFlag = 16
    - LineSpacingFlag = 32
    - AlignmentFlag = 64
    - NumberingFlag = 128

--------------------------------------------------------------------------------

[CLASS] IPEParagraphStateWrapper
Full Name: Autodesk.AutoCAD.Internal.IPEParagraphStateWrapper
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new IPEParagraphStateWrapper()

  PROPERTIES:
    IPEParagraphAlignmentType Alignment { get; set; }
    bool AutoListEnabled { get; }
    double FirstIndent { get; set; }
    IPEParagraphFlags Flags { get; set; }
    double LeftIndent { get; set; }
    double LineSpacingFactor { get; set; }
    IPELineSpacingStyle LineSpacingStyle { get; set; }
    IPENumberingType NumberingType { get; set; }
    double RightIndent { get; set; }
    double SpaceAfter { get; set; }
    double SpaceBefore { get; set; }
    bool TabOnlyDelimitedEnabled { get; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[CLASS] IPESettingsWrapper
Full Name: Autodesk.AutoCAD.Internal.IPESettingsWrapper
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new IPESettingsWrapper()

  PROPERTIES:
    bool AllowTabs { get; set; }
    bool AuthoringEntity { get; set; }
    double DefinedHeight { get; set; }
    bool Dimension { get; set; }
    Dimension DimensionObject { get; set; }
    double DimensionScale { get; set; }
    int EditFlags { get; set; }
    double HorizontalMargin { get; set; }
    string InputString { get; set; }
    bool MultilineAttribute { get; set; }
    bool RotateForFont { get; set; }
    bool SimpleMText { get; set; }
    bool TableCell { get; set; }
    ObjectId TableId { get; set; }
    ValueType ToolbarRectangle { get; set; }
    double VerticalMargin { get; set; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[CLASS] IPEStateWrapper
Full Name: Autodesk.AutoCAD.Internal.IPEStateWrapper
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new IPEStateWrapper()

  PROPERTIES:
    bool Annotative { get; set; }
    bool AnnotativeAvailable { get; set; }
    bool AutoCapsEnabled { get; }
    bool AutoHeightColumns { get; }
    bool ChangeCaseAvailable { get; set; }
    IPECharStateWrapper CharState { get; set; }
    bool ColumnsAvailable { get; set; }
    bool CopyAvailable { get; set; }
    double DefinedWidth { get; }
    bool Dimension { get; }
    IPEDimensionStateWrapper DimensionState { get; set; }
    bool EmptySelection { get; }
    bool FieldsAvailable { get; set; }
    bool ForceOpaqueBackground { get; }
    bool HideOptions { get; }
    bool HideToolbar { get; }
    ColumnType IPEColumnType { get; }
    bool IsAuthoringEntity { get; }
    bool IsDText { get; }
    bool IsSimpleMText { get; }
    AttachmentPoint Justification { get; }
    int LanguageIndex { get; }
        [SKIPPED PROPERTY] LayerColor: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    bool MLeader { get; }
    int MTextFixed { get; }
    bool NumberingAvailable { get; set; }
    bool OpaqueBackground { get; }
    bool ParaAttsAvailable { get; set; }
    IPEParagraphStateWrapper ParagraphState { get; set; }
    bool PasteAvailable { get; set; }
    bool QuickStartLinkAvailable { get; }
    bool RedoAvailable { get; set; }
    bool RibbonOn { get; }
    bool RulerAvailable { get; set; }
    bool RulerVisible { get; set; }
    bool SingleCustom { get; }
    bool SingleField { get; }
    bool SpellingEnabled { get; }
    bool StackAvailable { get; set; }
    int StaticColumnsCount { get; }
    ObjectId StyleId { get; set; }
    string StyleName { get; set; }
    bool TableCell { get; }
    bool UndoAvailable { get; set; }
    bool UnstackAvailable { get; set; }
    bool VerticalSHX { get; set; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[ENUM] IPESymbolCommandType
Full Name: Autodesk.AutoCAD.Internal.IPESymbolCommandType
Inherits: Enum

  Values:
    - IPEStandardSymbolCommand = 0
    - IPECustomSymbolCommand = 1

--------------------------------------------------------------------------------

[ENUM] IPETableFlags
Full Name: Autodesk.AutoCAD.Internal.IPETableFlags
Inherits: Enum

  Values:
    - TableSubsequentCellFlag = 1
    - StartInOpaqueBackground = 2
    - TableGoRightFlag = 4
    - TableGoLeftFlag = 8
    - TableGoBelowFlag = 16
    - TableGoAboveFlag = 32
    - TableFlipFlag = 256
    - DisableAutoStackFlag = 512
    - SelectAllFlag = 1024
    - CursorAtEndFlag = 2048
    - kForceNoUIWhenScript = 4096

--------------------------------------------------------------------------------

[CLASS] IPETestingEventArgs
Full Name: Autodesk.AutoCAD.Internal.IPETestingEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new IPETestingEventArgs(object data)

  PROPERTIES:
    object Data { get; }

--------------------------------------------------------------------------------

[CLASS] IPETestingHandler
Full Name: Autodesk.AutoCAD.Internal.IPETestingHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new IPETestingHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, IPETestingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, IPETestingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] IPETestingWrapper
Full Name: Autodesk.AutoCAD.Internal.IPETestingWrapper

  PROPERTIES:
    object Data { set; }
    static IPETestingWrapper IPETestingManager { get; }

  METHODS:
    void FireIPETestingHandler()

  EVENTS:
    event IPETestingHandler IPETest

--------------------------------------------------------------------------------

[CLASS] IPEUpdateUIEventArgs
Full Name: Autodesk.AutoCAD.Internal.IPEUpdateUIEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new IPEUpdateUIEventArgs(bool forceUpdate)

  PROPERTIES:
    bool ForceUpdate { get; }

--------------------------------------------------------------------------------

[CLASS] IPEWrapper
Full Name: Autodesk.AutoCAD.Internal.IPEWrapper

  PROPERTIES:
    static IPEWrapper IPEEventManager { get; }

  METHODS:
    void FireUpdateUIEvent(bool forceUpdate)

  EVENTS:
    event UpdateUIEventHandler UpdateUI

--------------------------------------------------------------------------------

[INTERFACE] IPreviewContext
Full Name: Autodesk.AutoCAD.Internal.IPreviewContext

  PROPERTIES:
    string Description { get; set; }
    string Name { get; set; }

--------------------------------------------------------------------------------

[INTERFACE] IPreviewContextProvider
Full Name: Autodesk.AutoCAD.Internal.IPreviewContextProvider

  METHODS:
    ILivePreviewControl GetPreviewControl(string name)
    ILivePreviewRule GetPreviewRule(string name)

--------------------------------------------------------------------------------

[ENUM] ItemTypeEnum
Full Name: Autodesk.AutoCAD.Internal.ItemTypeEnum
Inherits: Enum

  Values:
    - kBlock = 0
    - kDrawingAsBlock = 1
    - kDrawing = 2
    - kFolder = 3

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LayerUtilities
Full Name: Autodesk.AutoCAD.Internal.LayerUtilities

  PROPERTIES:
    static bool ApplyLayerFilterToToolbar { get; set; }
    static bool InvertLayerFilter { get; set; }
    static bool IsMultiDocumentActivationEnabled { get; }
    static bool UseCurrentLayerFilter { get; set; }
    static Color VpOverrideBackgroundColor { get; set; }
    static bool WillHighlightLayerPropOverrides { get; set; }
    static Color XrefOverrideBackgroundColor { get; set; }

  METHODS:
    static bool addXRefLayerOverride(ValueType layerId, int prop)
    static int CompareName(string string1, string string2)
    static void disableXRefLayerPropertyOverrideRecording()
    static void enableXRefLayerPropertyOverrideRecording()
    static void FocusMainFrameIfPossible()
    static AcArray<AcDbObjectId\,AcArrayMemCopyReallocator<AcDbObjectId> >* getAllLayersWithXRefOveride(AcArray<AcDbObjectId\,AcArrayMemCopyReallocator<AcDbObjectId> >* , AcDbDatabase* pDb)
    [SKIPPED METHOD] getAllLayersWithXRefOveride: Value cannot be null. (Parameter 'key')
    static ObjectId GetCurViewportObjectId()
    static int GetRestoreFlags()
    static bool hasAnyXRefLayerOverrides(ValueType layerId)
    static Predefined IsPredefined(LayerFilter filter)
    static bool IsXrefLayerPropertyOverridden(ValueType layerId, int prop)
    static bool isXRefLayerPropertyOverrideRecordingEnabled()
    static int LineWeightToPixel(LineWeight lineWeight)
    static bool removeXRefLayerOverride(ValueType layerId, int prop)
    static bool removeXRefLayerOverrides(Database dbHost)
    static bool removeXRefLayerOverrides(ValueType layerId)
    static void SetAllLayersUsed(LayerTable layerTable)
    static void SetLayerUsed(LayerTableRecord layerTableRecord)
    static void SetNotDefaultCommand(bool bNotDefaultCommand)
    static void ShowLayerMergeDialog(ObjectId[] layers)
    static void ShowLayerStateManagerDialog()
    static void ShowLayerStateSaveDialog()
    static bool UnreconciledFilterVisible(Database database)
    static bool VPOverridesFilterVisible(Database database, ObjectId viewport)
    static bool XrefOverridesFilterVisible(Database dbHost)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LayoutTabCtrl
Full Name: Autodesk.AutoCAD.Internal.LayoutTabCtrl
Implements: IDisposable

  PROPERTIES:
    Method Invoking { get; }

  METHODS:
    static string ActiveLayoutName(Database database)
    void Dispose()
    void Dragging(int dragging)
    void ExecuteMenu(Menu menu)
    void Invoke(Method method, IEnumerable<ObjectId> ids, int current, int selected)
    void UpdateMenu(Dictionary<Menu, int> menu, int selected)

--------------------------------------------------------------------------------

[CLASS] LayoutThumbnailEnumerator
Full Name: Autodesk.AutoCAD.Internal.LayoutThumbnailEnumerator
Inherits: DisposableWrapper

  PROPERTIES:
    uint Count { get; }
    int Index { get; }
    bool IsLayoutEmpty { get; }
    string LayoutName { get; }
        [SKIPPED PROPERTY] Thumbnail: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.

  METHODS:
    static LayoutThumbnailEnumerator EnumerateLayoutThumbnails(Document doc, ThumbnailCaptureFlags nFlag)
    bool Locate(string layoutName)
    bool MoveNext()
    bool Reset()

--------------------------------------------------------------------------------

[CLASS] LightUtil
Full Name: Autodesk.AutoCAD.Internal.LightUtil

  CONSTRUCTORS:
    new LightUtil()

  METHODS:
    static bool GetBackgroundType(out DrawableType type)
    static bool GetSkyStatus()
    static bool RegSceneUI()
    static void SetSunAngle(Database db, ValueType sunId, DateTime dateTime)

--------------------------------------------------------------------------------

[CLASS] LivePreviewUtil
Full Name: Autodesk.AutoCAD.Internal.LivePreviewUtil

  CONSTRUCTORS:
    new LivePreviewUtil()

  PROPERTIES:
    static IPreviewContextProvider PreviewContextProvider { get; }

  METHODS:
    [SKIPPED METHOD] IsControlLivePreviewAble: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static IPreviewContextProvider SetPreviewContextProvider(IPreviewContextProvider provider)

--------------------------------------------------------------------------------

[ENUM] LocalServerUsage
Full Name: Autodesk.AutoCAD.Internal.LocalServerUsage
Inherits: Enum

  Values:
    - kDisabled = 0
    - kForActivity = 1
    - kForMetaData = 2
    - kForBothActivityAndMetadata = 3

--------------------------------------------------------------------------------

[ENUM] MessageButtonEnum
Full Name: Autodesk.AutoCAD.Internal.MessageButtonEnum
Inherits: Enum

  Values:
    - kOK = 0
    - kOKCancel = 1
    - kYesNoCancel = 3
    - kYesNo = 4

--------------------------------------------------------------------------------

[ENUM] MessageIconEnum
Full Name: Autodesk.AutoCAD.Internal.MessageIconEnum
Inherits: Enum

  Values:
    - kNone = 0
    - kError = 16
    - kWarning = 48
    - kInformation = 64

--------------------------------------------------------------------------------

[ENUM] MessageResultEnum
Full Name: Autodesk.AutoCAD.Internal.MessageResultEnum
Inherits: Enum

  Values:
    - kNone = 0
    - kOK = 1
    - kCancel = 2
    - kYes = 6
    - kNo = 7

--------------------------------------------------------------------------------

[CLASS] mgBlockPreviewInfo
Full Name: Autodesk.AutoCAD.Internal.mgBlockPreviewInfo

  CONSTRUCTORS:
    new mgBlockPreviewInfo()

  METHODS:
    string getBlockPreviewInfoJsonStr()
    bool isOutDated()

--------------------------------------------------------------------------------

[CLASS] MgdAcViewTransitions
Full Name: Autodesk.AutoCAD.Internal.MgdAcViewTransitions

  METHODS:
    static void GetSettings(out bool enable2d, out bool enable3d, out bool enableDuringScripts, out uint timeMs, out double fps)
    static void SetSettings(bool enable2d, bool enable3d, bool enableDuringScripts, uint timeMs, double fps)

--------------------------------------------------------------------------------

[CLASS] MLibDownloadUtils
Full Name: Autodesk.AutoCAD.Internal.MLibDownloadUtils

  CONSTRUCTORS:
    new MLibDownloadUtils()

  METHODS:
    static void BeginToExtract()
    static void CancelDownloadJob()
    static ulong DownloadCurrentSize()
    static void DownloadServicEndUp()
    static void DownloadServiceStartUp()
    static ulong DownloadTotalSize()
    static bool IsMLibCancelled()
    static bool IsMLibDownloadComplete()
    static bool IsMLibDownloading()
    static bool IsMLibError()
    static bool IsMLibExtracting()
    static bool IsMLibReadyToInstall()
    static void RegisterTimerCallback(AcDownloadCallback fp)
    static void ShowErrorTaskDialog()

--------------------------------------------------------------------------------

[CLASS] MlinesMgdServices
Full Name: Autodesk.AutoCAD.Internal.MlinesMgdServices

  METHODS:
    static void AdjustColorToBackground(ref Color mgForeground, Color mgBackground)
    static void CloseCachedMLStyle()
    static string GetDefaultMlnFilename()
    static void GetDisplayBGColor(Color mgColor)
    static string GetLinetypeNameForDisplay(ObjectId linetypeId)
    static bool IsStyleAlterable(string mgName)
    static bool IsValidStyleName(string mgName)
    static MLError LoadAndUpdateMlnStyle(string mlnFile, int fileOffset)
    static MLError MlnFileReadStyles(string filename, ArrayList nameList, ArrayList positionList)
    static void ProcessMlEditCommand(CommandCode commandCode)
    static string ShowOpenMlnFileDialog(string currentMlnFile)
    static MLError ShowSaveDialog(ObjectId styleId, ref string mlnFilename)

--------------------------------------------------------------------------------

[CLASS] MonitoredSysvarValueWrapper
Full Name: Autodesk.AutoCAD.Internal.MonitoredSysvarValueWrapper
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new MonitoredSysvarValueWrapper(IntPtr p, bool _auto_delete)
    new MonitoredSysvarValueWrapper(IntPtr p)

  METHODS:
    MonitoredSysvarValueWrapper clone()
    bool isEqualTo(MonitoredSysvarValueWrapper sysvar_value)
    bool setFromString(string value)
    string toString(int unit, int prec)
    string toString()
    string type()

--------------------------------------------------------------------------------

[CLASS] MonitoredSysvarWrapper
Full Name: Autodesk.AutoCAD.Internal.MonitoredSysvarWrapper
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new MonitoredSysvarWrapper(IntPtr p)

  METHODS:
    MonitoredSysvarValueWrapper currentValue()
    bool isValidValue(MonitoredSysvarValueWrapper value)
    string name()
    MonitoredSysvarValueWrapper perferedValue()
    void resetToPreferedValue()
    int setPreferedValue(string value)
    void setPreferedValue(MonitoredSysvarValueWrapper prefered_value)
    void syncCurrentValue()

--------------------------------------------------------------------------------

