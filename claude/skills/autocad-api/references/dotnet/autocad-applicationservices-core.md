# Autodesk.AutoCAD.ApplicationServices.Core

NAMESPACE: Autodesk.AutoCAD.ApplicationServices.Core
--------------------------------------------------------------------------------

[ABSTRACT CLASS] Application
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.Application

  PROPERTIES:
    static DocumentCollection DocumentManager { get; }
    static bool HasPickFirst { get; }
    static bool IsAdpDisabled { get; }
    static bool IsInBackgroundMode { get; }
    static bool IsInPlaceServer { get; }
    static bool IsQuiescent { get; }
    static LongTransactionManager LongTransactionManager { get; }
    static     [SKIPPED PROPERTY] MainWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static Publisher Publisher { get; }
    static RecentDocumentCollection RecentDocuments { get; }
    static UnmanagedResources UnmanagedResources { get; }
    static UserConfigurationManager UserConfigurationManager { get; }
    static Version Version { get; }

  METHODS:
    static string EvaluateDiesel(string dieselCmd)
    static object GetSystemVariable(string name)
    static WhoHasInfo GetWhoHasInfo(string pathname)
    static ResultBuffer Invoke(ResultBuffer args)
    static bool IsFileLocked(string pathname)
    static void LoadJSScript(Uri urlJSFile)
    static void Quit()
    static void SetSystemVariable(string name, object value)
    static void ShowAlertDialog(string message)
    [SKIPPED METHOD] ShowModalWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessWindow: Could not load file or assembly 'PresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static object TryGetSystemVariable(string name)
    static void UpdateScreen()

  EVENTS:
    event EndExtractionEventHandler EndExtraction
    event BeginExtractionEventHandler BeginExtraction
    event EventHandler LeaveModal
    event EventHandler EnterModal
    event PreTranslateMessageEventHandler PreTranslateMessage
    event EventHandler Idle
    event BeginDoubleClickEventHandler BeginDoubleClick
    event SystemVariableChangedEventHandler SystemVariableChanged
    event SystemVariableChangingEventHandler SystemVariableChanging
    event EventHandler QuitWillStart
    event EventHandler QuitAborted
    event BeginQuitEventHandler BeginQuit
    event BeginCloseAllEventHandler BeginCloseAll

--------------------------------------------------------------------------------

[CLASS] CreateContextMenuHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.CreateContextMenuHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new CreateContextMenuHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(Type type, AsyncCallback callback, object obj)
    AcEdUIContext* EndInvoke(IAsyncResult result)
    AcEdUIContext* Invoke(Type type)

--------------------------------------------------------------------------------

[CLASS] EnableWinFormHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.EnableWinFormHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EnableWinFormHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object o, bool enable, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(object o, bool enable)

--------------------------------------------------------------------------------

[CLASS] FilterMessageWinFormsHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.FilterMessageWinFormsHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new FilterMessageWinFormsHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(tagMSG* msg, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(tagMSG* msg)

--------------------------------------------------------------------------------

[CLASS] OnIdleWinFormsHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.OnIdleWinFormsHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnIdleWinFormsHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] OnWinFormsLoadedHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.OnWinFormsLoadedHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnWinFormsLoadedHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] OnWpfLoadedHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.OnWpfLoadedHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnWpfLoadedHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] PreProcessMessageWinFormsHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.Core.PreProcessMessageWinFormsHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PreProcessMessageWinFormsHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(tagMSG* msg, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(tagMSG* msg)

--------------------------------------------------------------------------------

