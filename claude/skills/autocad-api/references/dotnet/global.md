# (Global)

NAMESPACE: (Global)
--------------------------------------------------------------------------------

[CLASS] AcAeNetServices
Full Name: AcAeNetServices

  CONSTRUCTORS:
    new AcAeNetServices()

  METHODS:
    static ObjectId CreateParameter(string name, string expression, UserParameterType userParameterType, bool showProperties)
    static string GenerateUniqueVariableName(string prefix)
    static BlockParameterType GetBlockParameterType(BlockParameter parameter)
    static InvokeBESettings GetInvokeBESettingsProc()
    static InvokeBTableDialog2 GetInvokeBTableDialog2Proc()
    static InvokeBTableDialog3 GetInvokeBTableDialog3Proc()
    static InvokeBTableDialog GetInvokeBTableDialogProc()
    static InvokeLookupDialog GetInvokeLookupDialogProc()
    static UserParameterType GetUserParameterType(BlockParameter parameter)
    static void SetInvokeBESettingsProc(InvokeBESettings pProc)
    static void SetInvokeBTableDialog2Proc(InvokeBTableDialog2 pProc)
    static void SetInvokeBTableDialog3Proc(InvokeBTableDialog3 pProc)
    static void SetInvokeBTableDialogProc(InvokeBTableDialog pProc)
    static void SetInvokeLookupDialogProc(InvokeLookupDialog pProc)

--------------------------------------------------------------------------------

[CLASS] AcPublishServices
Full Name: AcPublishServices

  CONSTRUCTORS:
    new AcPublishServices()

  METHODS:
    static InvokePublishBlockTemplateDialog GetInvokePublishBlockTemplateDialogProc()
    static void ReportBGCorePublishJobStarted(string dsdName)
    static void ReportBGCorePublishStatus(string dsdName, string msg, bool success)
    static void SetInvokePublishBlockTemplateDialogProc(InvokePublishBlockTemplateDialog pProc)

--------------------------------------------------------------------------------

[CLASS] AcWebServices
Full Name: AcWebServices

  CONSTRUCTORS:
    new AcWebServices()

  PROPERTIES:
    static AcWebServices Instance { get; }

  METHODS:
    void OnTokenRequested(object sender, AuthTokenRequiredEventArgs e)

--------------------------------------------------------------------------------

[STRUCT] AdsName
Full Name: AdsName

--------------------------------------------------------------------------------

[CLASS] CalcDialogCreator
Full Name: CalcDialogCreator

  CONSTRUCTORS:
    new CalcDialogCreator()

  METHODS:
    static ShowModalFunc GetCreatorFunc()
    static void SetCreatorFunc(ShowModalFunc showModal)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] FloatingWindowsHook
Full Name: FloatingWindowsHook
Implements: IDisposable

  CONSTRUCTORS:
    new FloatingWindowsHook(object form)

  METHODS:
    void Dispose()
    void OnFormClosed(object sender, EventArgs e)
    void OnFormLoad(object sender, EventArgs e)

--------------------------------------------------------------------------------

[STRUCT] GeoDatum
Full Name: GeoDatum

--------------------------------------------------------------------------------

[STRUCT] GeoEllipsoid
Full Name: GeoEllipsoid

--------------------------------------------------------------------------------

[ENUM] GeomapResolution
Full Name: GeomapResolution
Inherits: Enum

  Values:
    - Optimal = 0
    - Fine = 1
    - Finer = 2
    - Coarse = -1

--------------------------------------------------------------------------------

[ENUM] GeomapType
Full Name: GeomapType
Inherits: Enum

  Values:
    - NoMap = 0
    - Aerial = 1
    - Road = 2
    - Hybrid = 3
    - EsriImagery = 4
    - EsriOpenStreetMap = 5
    - EsriStreets = 6
    - EsriLightGray = 7
    - EsriDarkGray = 8

--------------------------------------------------------------------------------

[STRUCT] GeoProjectionParam
Full Name: GeoProjectionParam

--------------------------------------------------------------------------------

[CLASS] HatchUtils
Full Name: HatchUtils

  METHODS:
    static bool IsCurrentHatchAnnotative()
    static bool IsCurrentHatchAssociative()
    static void SetCurrentHatchAssociative(bool bAssociative)
    static bool SetHpLastPatternSysvar(string hpname)
    static string TryGetGradientColor2String()

--------------------------------------------------------------------------------

[CLASS] InvokeBESettings
Full Name: InvokeBESettings
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InvokeBESettings(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] InvokeBTableDialog
Full Name: InvokeBTableDialog
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InvokeBTableDialog(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(ObjectId tableId, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(ObjectId tableId)

--------------------------------------------------------------------------------

[CLASS] InvokeBTableDialog2
Full Name: InvokeBTableDialog2
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InvokeBTableDialog2(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(BlockPropertiesTable table, int activeRow, AsyncCallback callback, object obj)
    int EndInvoke(IAsyncResult result)
    int Invoke(BlockPropertiesTable table, int activeRow)

--------------------------------------------------------------------------------

[CLASS] InvokeBTableDialog3
Full Name: InvokeBTableDialog3
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InvokeBTableDialog3(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(ObjectId tableId, bool bAdd, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(ObjectId tableId, bool bAdd)

--------------------------------------------------------------------------------

[CLASS] InvokeLookupDialog
Full Name: InvokeLookupDialog
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InvokeLookupDialog(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(ObjectId lookupActionId, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(ObjectId lookupActionId)

--------------------------------------------------------------------------------

[CLASS] InvokePublishBlockTemplateDialog
Full Name: InvokePublishBlockTemplateDialog
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InvokePublishBlockTemplateDialog(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(StringCollection dwgList, string blkFile, bool bEdit, out string savedBlkFile, AsyncCallback callback, object obj)
    bool EndInvoke(ref string __unnamed000, IAsyncResult result)
    bool Invoke(StringCollection dwgList, string blkFile, bool bEdit, out string savedBlkFile)

--------------------------------------------------------------------------------

[CLASS] QPRibbonFrameManager
Full Name: QPRibbonFrameManager

  CONSTRUCTORS:
    new QPRibbonFrameManager()

  METHODS:
    [SKIPPED METHOD] ContextMenuOpening: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] RangeParser
Full Name: RangeParser

  CONSTRUCTORS:
    new RangeParser()

  METHODS:
    static bool CanonicalToSystemRange(int eUnits, string strIn, out string strOut)
    static bool SystemRangeToCanonical(int eUnits, string strIn, out string strOut)

--------------------------------------------------------------------------------

[CLASS] ShowModalFunc
Full Name: ShowModalFunc
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ShowModalFunc(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(string sStatusRegion, string sCurrentValue, bool bIsApplyable, AsyncCallback callback, object obj)
    CalcResult EndInvoke(IAsyncResult result)
    CalcResult Invoke(string sStatusRegion, string sCurrentValue, bool bIsApplyable)

--------------------------------------------------------------------------------

