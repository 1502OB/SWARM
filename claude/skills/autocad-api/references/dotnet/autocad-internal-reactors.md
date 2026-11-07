# Autodesk.AutoCAD.Internal.Reactors

NAMESPACE: Autodesk.AutoCAD.Internal.Reactors
--------------------------------------------------------------------------------

[STRUCT] AcEdIPEReactorImpl
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcEdIPEReactorImpl

--------------------------------------------------------------------------------

[STRUCT] AcEdOPMObjectFilterReactorImpl
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcEdOPMObjectFilterReactorImpl

--------------------------------------------------------------------------------

[STRUCT] AcEdUcsReactorImpl
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcEdUcsReactorImpl

--------------------------------------------------------------------------------

[STRUCT] AcEdViewFinalChangeReactorImpl
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcEdViewFinalChangeReactorImpl

--------------------------------------------------------------------------------

[STRUCT] AcSunViewportMonitorReactorImpl
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcSunViewportMonitorReactorImpl

--------------------------------------------------------------------------------

[STRUCT] AcVsESWDictionaryReactor
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcVsESWDictionaryReactor

--------------------------------------------------------------------------------

[STRUCT] AcVsESWObjectReactor
Full Name: Autodesk.AutoCAD.Internal.Reactors.AcVsESWObjectReactor

--------------------------------------------------------------------------------

[CLASS] ApplicationDockLayoutChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationDockLayoutChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ApplicationDockLayoutChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] ApplicationDocumentFrameChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationDocumentFrameChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ApplicationDocumentFrameChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] ApplicationEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationEventManager

  METHODS:
    static ApplicationEventManager Instance()

  EVENTS:
    event ApplicationDocumentFrameChangedEventHandler ApplicationDocumentFrameChanged
    event ApplicationDockLayoutChangedEventHandler ApplicationDockLayoutChanged
    event ApplicationMainWindowVisibleChangedEventHandler ApplicationMainWindowVisibleChanged
    event ApplicationMainWindowSizedEventHandler ApplicationMainWindowSized
    event ApplicationMainWindowMovedEventHandler ApplicationMainWindowMoved

--------------------------------------------------------------------------------

[CLASS] ApplicationMainWindowMovedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationMainWindowMovedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ApplicationMainWindowMovedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] ApplicationMainWindowSizedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationMainWindowSizedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ApplicationMainWindowSizedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ApplicationMainWindowStateChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ApplicationMainWindowStateChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ApplicationMainWindowStateChangedEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationMainWindowStateChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    StateChangeType ChangeType { get; }
    ValueType NewSize { get; }

--------------------------------------------------------------------------------

[CLASS] ApplicationMainWindowVisibleChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ApplicationMainWindowVisibleChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ApplicationMainWindowVisibleChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] CuiEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.CuiEventManager

  METHODS:
    void FreeCustomizationSectionHandle(GCHandle handle)
    GCHandle GetCustomizationSectionHandle(string menuGroupName)
    static CuiEventManager Instance()

  EVENTS:
    event LoadRibbonEventHandler LoadRibbon
    event WorkspaceSettingsSavedEventHandler WorkspaceSettingsSaved
    event WorkspaceRibbonSaveEventHandler WorkspaceRibbonSave
    event WorkspaceRestoreEventHandler WorkspaceRestore

--------------------------------------------------------------------------------

[CLASS] CuiLoadEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.CuiLoadEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new CuiLoadEventArgs()

  PROPERTIES:
    string CurrentWorkspaceId { get; }
    string CurrentWorkspaceName { get; }

--------------------------------------------------------------------------------

[CLASS] DictionaryEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.DictionaryEventManager
Implements: IDisposable

  CONSTRUCTORS:
    new DictionaryEventManager(ValueType dictionaryId, bool bMonitorDictionaryObjects, bool bNotifyImmediately)
    new DictionaryEventManager(ValueType dictionaryId, bool bMonitorDictionaryObjects)

  PROPERTIES:
    bool Enabled { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event EventHandler DictionaryModified
    event EventHandler ObjectModified

--------------------------------------------------------------------------------

[CLASS] IPEEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.IPEEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new IPEEventArgs()

  PROPERTIES:
    Entity Entity { get; }
    IPEExitStatus ExitStatus { get; }

--------------------------------------------------------------------------------

[CLASS] IPEEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.IPEEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new IPEEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, IPEEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, IPEEventArgs e)

--------------------------------------------------------------------------------

[CLASS] IPEEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.IPEEventManager

  METHODS:
    static IPEEventManager Instance()
    void invokeSetIPEMacroMode(int iMode)
    void invokeSetIPEString(string contents)

  EVENTS:
    event IPEEventHandler IPEEnded
    event IPEEventHandler IPEWillStart

--------------------------------------------------------------------------------

[CLASS] LoadRibbonEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.LoadRibbonEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LoadRibbonEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, CuiLoadEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, CuiLoadEventArgs e)

--------------------------------------------------------------------------------

[CLASS] OPMObjectFilterEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.OPMObjectFilterEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new OPMObjectFilterEventArgs()

  PROPERTIES:
    Guid EntityCLSID { get; }
    string EntityName { get; }

--------------------------------------------------------------------------------

[CLASS] OPMObjectFilterEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.OPMObjectFilterEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OPMObjectFilterEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, OPMObjectFilterEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, OPMObjectFilterEventArgs e)

--------------------------------------------------------------------------------

[CLASS] OPMObjectFilterEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.OPMObjectFilterEventManager

  METHODS:
    static OPMObjectFilterEventManager Instance()

  EVENTS:
    event OPMObjectFilterEventHandler OPMObjectFilterChanged

--------------------------------------------------------------------------------

[CLASS] SunViewportMonitorEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.SunViewportMonitorEventManager

  METHODS:
    static SunViewportMonitorEventManager Instance()

  EVENTS:
    event EventHandler Goodbye
    event EventHandler ViewportModified
    event EventHandler ActiveSunModified

--------------------------------------------------------------------------------

[CLASS] TableSubSelectFilter
Full Name: Autodesk.AutoCAD.Internal.Reactors.TableSubSelectFilter

  METHODS:
    static TableSubSelectFilter Instance()

  EVENTS:
    event TableSubSelectFilterEventHandler CellSelected

--------------------------------------------------------------------------------

[CLASS] TableSubSelectFilterEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.TableSubSelectFilterEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new TableSubSelectFilterEventArgs()

  PROPERTIES:
    bool Reposition { get; }
    bool Selected { get; }
    ObjectId TableId { get; }

--------------------------------------------------------------------------------

[CLASS] TableSubSelectFilterEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.TableSubSelectFilterEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new TableSubSelectFilterEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, TableSubSelectFilterEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, TableSubSelectFilterEventArgs e)

--------------------------------------------------------------------------------

[CLASS] UcsEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.UcsEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new UcsEventArgs()

  PROPERTIES:
    Matrix3d Matrix3dValue { get; }

--------------------------------------------------------------------------------

[CLASS] UcsEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.UcsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new UcsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, UcsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, UcsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] UcsEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.UcsEventManager

  METHODS:
    static UcsEventManager instance()

  EVENTS:
    event UcsEventHandler DynamicOriginUpdate
    event UcsEventHandler UcsChanged
    event UcsEventHandler UcsWillChange

--------------------------------------------------------------------------------

[CLASS] ViewChangeEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.ViewChangeEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new ViewChangeEventArgs()

  PROPERTIES:
    int UsedX { get; }
    int UsedY { get; }

--------------------------------------------------------------------------------

[CLASS] ViewFinalChangeEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ViewFinalChangeEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ViewFinalChangeEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ViewChangeEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ViewChangeEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ViewFinalChangeEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.ViewFinalChangeEventManager

  METHODS:
    static ViewFinalChangeEventManager Instance()

  EVENTS:
    event ViewResizeEventHandler SendViewportsResized
    event ViewFinalChangeEventHandler SendViewportCycled
    event ViewFinalChangeEventHandler SendViewportChanged
    event ViewFinalChangeEventHandler SendViewFinalChange

--------------------------------------------------------------------------------

[CLASS] ViewResizeEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.ViewResizeEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new ViewResizeEventArgs()

  PROPERTIES:
    double UsedEX { get; }
    double UsedEY { get; }
    bool UsedSplit { get; }
    double UsedSX { get; }
    double UsedSY { get; }

--------------------------------------------------------------------------------

[CLASS] ViewResizeEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.ViewResizeEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ViewResizeEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ViewResizeEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ViewResizeEventArgs e)

--------------------------------------------------------------------------------

[CLASS] VisualStyleEventManager
Full Name: Autodesk.AutoCAD.Internal.Reactors.VisualStyleEventManager
Implements: IDisposable

  METHODS:
    void Dispose()
    static VisualStyleEventManager Instance()
    void SetDataBase(Database acadDatabase, bool bUpdateUI)

  EVENTS:
    event VsESWDictionaryEventHandler VisualStyleDictionaryEvent
    event VsESWObjectEventHandler VisualStyleObjectEvent

--------------------------------------------------------------------------------

[CLASS] VsESWDictionaryEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.VsESWDictionaryEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new VsESWDictionaryEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender)

--------------------------------------------------------------------------------

[CLASS] VsESWObjectEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.VsESWObjectEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new VsESWObjectEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender)

--------------------------------------------------------------------------------

[CLASS] WorkspaceEventArgs
Full Name: Autodesk.AutoCAD.Internal.Reactors.WorkspaceEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new WorkspaceEventArgs()

  PROPERTIES:
    CustomizationSection CustomizationSection { get; }
    string Id { get; }
    string Name { get; }
    Workspace Workspace { get; }

--------------------------------------------------------------------------------

[CLASS] WorkspaceRestoreEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.WorkspaceRestoreEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new WorkspaceRestoreEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, WorkspaceEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, WorkspaceEventArgs e)

--------------------------------------------------------------------------------

[CLASS] WorkspaceRibbonSaveEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.WorkspaceRibbonSaveEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new WorkspaceRibbonSaveEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, WorkspaceEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, WorkspaceEventArgs e)

--------------------------------------------------------------------------------

[CLASS] WorkspaceSettingsSavedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Reactors.WorkspaceSettingsSavedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new WorkspaceSettingsSavedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

