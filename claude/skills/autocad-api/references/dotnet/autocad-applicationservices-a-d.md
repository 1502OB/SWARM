# Autodesk.AutoCAD.ApplicationServices

NAMESPACE: Autodesk.AutoCAD.ApplicationServices
--------------------------------------------------------------------------------

[ABSTRACT CLASS] Application
Full Name: Autodesk.AutoCAD.ApplicationServices.Application
Inherits: Application

  PROPERTIES:
    static object AcadApplication { get; }
    static bool DisplayTextScreen { get; set; }
    static DocumentWindowCollection DocumentWindowCollection { get; }
    static InfoCenter InfoCenter { get; }
    static bool IsInCustomizationMode { get; set; }
    static object MenuBar { get; }
    static object MenuGroups { get; }
    static     [SKIPPED PROPERTY] NonInPlaceMainWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static object Preferences { get; }
    static StatusBar StatusBar { get; }
    static DataBindings UIBindings { get; }

  METHODS:
    static void AddDefaultContextMenuExtension(ContextMenuExtension menuExtension)
    static void AddObjectContextMenuExtension(RXClass runtimeClass, ContextMenuExtension menuExtension)
    static void CommitModalWindow(bool commitValue)
    [SKIPPED METHOD] DoDragDrop: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] DoDragDrop: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] InvokeContextHelp: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    static void InvokeContextHelp(IntPtr window, int contextId, string helpPrefix)
    static void InvokeContextHelp(IntPtr window, int contextId)
    static void InvokeHelp(string fileName, string topic)
    static bool InvokeHelpForExternal(string functionName)
    static bool IsConnectEnabled()
    static bool LoadMainMenu(string filename)
    static bool LoadPartialMenu(string filename)
    static void PostSearchQuery(string searchString)
    static void ReloadAllMenus()
    static void RemoveDefaultContextMenuExtension(ContextMenuExtension menuExtension)
    static void RemoveObjectContextMenuExtension(RXClass runtimeClass, ContextMenuExtension menuExtension)
    static bool SetCurrentWorkspace(string workspacename)
    [SKIPPED METHOD] ShowModalDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModalDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    static bool ShowModalWindow(Uri htmlPage)
    static bool ShowModalWindow(IntPtr owner, Uri htmlPage)
    static bool ShowModalWindow(IntPtr owner, Uri htmlPage, bool persistSizeAndPosition)
    [SKIPPED METHOD] ShowModelessDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] ShowModelessDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    static void ShowModelessWindow(Uri htmlPage)
    static void ShowModelessWindow(IntPtr owner, Uri htmlPage)
    static void ShowModelessWindow(IntPtr owner, Uri htmlPage, bool persistSizeAndPosition)
    [SKIPPED METHOD] ToSystemDrawingPoint: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ToSystemDrawingSize: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ToSystemWindowsPoint: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ToSystemWindowsSize: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool UnloadPartialMenu(string filename)

  EVENTS:
    event TabbedDialogEventHandler DisplayingCustomizeDialog
    event TabbedDialogEventHandler DisplayingDraftingSettingsDialog
    event TabbedDialogEventHandler DisplayingOptionDialog
    event EventHandler EndCustomizationMode
    event EventHandler BeginCustomizationMode

--------------------------------------------------------------------------------

[CLASS] BeginCloseAllEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginCloseAllEventArgs
Inherits: EventArgs

  PROPERTIES:
    bool IsVetoed { get; set; }

--------------------------------------------------------------------------------

[CLASS] BeginCloseAllEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginCloseAllEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginCloseAllEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginCloseAllEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginCloseAllEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginDoubleClickEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginDoubleClickEventArgs
Inherits: EventArgs

  PROPERTIES:
    Point3d Location { get; }

--------------------------------------------------------------------------------

[CLASS] BeginDoubleClickEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginDoubleClickEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginDoubleClickEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginDoubleClickEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginDoubleClickEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginExtractionEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginExtractionEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BeginExtractionEventArgs(BeginExtractionEventArgs* args)

  METHODS:
    void QueueForExtraction(ObjectId idObject, string viewableId)

--------------------------------------------------------------------------------

[CLASS] BeginExtractionEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginExtractionEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginExtractionEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginExtractionEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginExtractionEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginQuitEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginQuitEventArgs
Inherits: EventArgs

  PROPERTIES:
    bool IsVetoed { get; set; }

--------------------------------------------------------------------------------

[CLASS] BeginQuitEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.BeginQuitEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginQuitEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginQuitEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginQuitEventArgs e)

--------------------------------------------------------------------------------

[CLASS] CheckingOutEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.CheckingOutEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new CheckingOutEventArgs(LongTransaction longTrans, ObjectIdCollection objCollection)

  PROPERTIES:
    ObjectIdCollection CheckOutSet { get; }
    LongTransaction Transaction { get; }

  METHODS:
    void Veto()

--------------------------------------------------------------------------------

[CLASS] CheckingOutEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.CheckingOutEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new CheckingOutEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, CheckingOutEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, CheckingOutEventArgs e)

--------------------------------------------------------------------------------

[CLASS] CommandEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.CommandEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new CommandEventArgs()

  PROPERTIES:
    string GlobalCommandName { get; }

--------------------------------------------------------------------------------

[CLASS] CommandEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.CommandEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new CommandEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, CommandEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, CommandEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ConfigurationSectionNameAttribute
Full Name: Autodesk.AutoCAD.ApplicationServices.ConfigurationSectionNameAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new ConfigurationSectionNameAttribute(string name)

  PROPERTIES:
    string Name { get; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DatabaseExtension
Full Name: Autodesk.AutoCAD.ApplicationServices.DatabaseExtension

  METHODS:
    static void Audit(Database db, bool bFixErrors, bool bCmdLnEcho)

--------------------------------------------------------------------------------

[CLASS] Document
Full Name: Autodesk.AutoCAD.ApplicationServices.Document
Inherits: DisposableWrapper

  PROPERTIES:
    string CommandInProgress { get; }
    Database Database { get; }
    Editor Editor { get; }
    DocumentSaveFormat FormatForSave { get; }
    Manager GraphicsManager { get; }
    bool IsActive { get; }
    bool IsNamedDrawing { get; }
    bool IsReadOnly { get; }
    string Name { get; }
    TransactionManager TransactionManager { get; }
    Hashtable UserData { get; }
        [SKIPPED PROPERTY] Window: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  METHODS:
    static Document Create(IntPtr unmanagedPointer)
    void DowngradeDocOpen(bool bPromptForSave)
    object GetLispSymbol(string name)
    DocumentLock LockDocument()
    DocumentLock LockDocument(DocumentLockMode lockMode, string globalCommandName, string localCommandName, bool promptIfFails)
    DocumentLockMode LockMode(bool bIncludeMyLocks)
    DocumentLockMode LockMode()
    void PopDbmod()
    void PushDbmod()
    void SendStringToExecute(string command, bool activate, bool wrapUpInactiveDoc, bool echoCommand)
    void SetLispSymbol(string name, object value)
    Database TryGetDatabase()
    void UpgradeDocOpen()

  EVENTS:
    event ModelessOperationEventHandler ModelessOperationEnded
    event ModelessOperationEventHandler ModelessOperationWillStart
    event LayoutSwitchingEventHandler LayoutSwitching
    event LayoutSwitchedEventHandler LayoutSwitched
    event EventHandler ViewChanged
    event EventHandler ImpliedSelectionChanged
    event EventHandler CloseWillStart
    event EventHandler CloseAborted
    event DocumentBeginCloseEventHandler BeginDocumentClose
    event DrawingOpenEventHandler EndDwgOpen
    event DrawingOpenEventHandler BeginDwgOpen
    event EventHandler LispCancelled
    event EventHandler LispEnded
    event LispWillStartEventHandler LispWillStart
    event CommandEventHandler CommandFailed
    event CommandEventHandler CommandCancelled
    event CommandEventHandler CommandEnded
    event CommandEventHandler CommandWillStart
    event UnknownCommandEventHandler UnknownCommand

--------------------------------------------------------------------------------

[CLASS] DocumentActivationChangedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentActivationChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    bool NewValue { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentActivationChangedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentActivationChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentActivationChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentActivationChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentActivationChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentBeginCloseEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentBeginCloseEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new DocumentBeginCloseEventArgs()

  METHODS:
    void Veto()

--------------------------------------------------------------------------------

[CLASS] DocumentBeginCloseEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentBeginCloseEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentBeginCloseEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentBeginCloseEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentBeginCloseEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentClosedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentClosedEventArgs
Inherits: EventArgs

  PROPERTIES:
    RecentDocument RecentDocument { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentClosedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentClosedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentClosedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentClosedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentClosedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentCollection
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentCollection
Inherits: MarshalByRefObject
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    Document CurrentDocument { get; set; }
    DocumentSaveFormat DefaultFormatForSave { get; set; }
    bool DocumentActivationEnabled { get; set; }
    bool IsApplicationContext { get; }
    Document MdiActiveDocument { get; set; }

  METHODS:
    void AppContextNewDocument(string templateFileName)
    void AppContextOpenDocument(string fileName)
    void AppContextRecoverDocument(string fileName)
    void CopyTo(Document[] array, int index)
    void ExecuteInApplicationContext(ExecuteInApplicationContextCallback callback, object data)
    ExecutionResult ExecuteInCommandContextAsync(Func<object, Task> callback, object data)
    Document GetDocument(Database db)
    IEnumerator GetEnumerator()
    Tuple<bool, Document> GetPendingDocumentForSwitch()

  EVENTS:
    event DocumentLockModeChangedEventHandler DocumentLockModeChanged
    event DocumentLockModeChangeVetoedEventHandler DocumentLockModeChangeVetoed
    event DocumentLockModeWillChangeEventHandler DocumentLockModeWillChange
    event DocumentCollectionEventHandler DocumentActivated
    event DocumentActivationChangedEventHandler DocumentActivationChanged
    event DocumentCollectionEventHandler DocumentToBeDeactivated
    event DocumentCollectionEventHandler DocumentToBeActivated
    event DocumentCollectionEventHandler DocumentBecameCurrent
    event DocumentCollectionEventHandler DocumentCreationCanceled
    event DocumentDestroyedEventHandler DocumentDestroyed
    event DocumentCollectionEventHandler DocumentToBeDestroyed
    event DocumentCollectionEventHandler DocumentCreated
    event DocumentCollectionEventHandler DocumentCreateStarted

--------------------------------------------------------------------------------

[CLASS] DocumentCollectionEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentCollectionEventArgs
Inherits: EventArgs

  PROPERTIES:
    Document Document { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentCollectionEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentCollectionEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentCollectionEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentCollectionEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentCollectionEventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DocumentCollectionExtension
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentCollectionExtension

  METHODS:
    static Document Add(DocumentCollection docCol, string templateFileName)
    static void CloseAll(DocumentCollection docCol)
    static Document Open(DocumentCollection docCol, string fileName, bool forReadOnly, string password)
    static Document Open(DocumentCollection docCol, string fileName, bool forReadOnly)
    static Document Open(DocumentCollection docCol, string fileName)

--------------------------------------------------------------------------------

[CLASS] DocumentDestroyedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentDestroyedEventArgs
Inherits: EventArgs

  PROPERTIES:
    string FileName { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentDestroyedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentDestroyedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentDestroyedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentDestroyedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentDestroyedEventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DocumentExtension
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentExtension

  METHODS:
    [SKIPPED METHOD] CapturePreviewImage: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static void CloseAndDiscard(Document doc)
    static void CloseAndSave(Document doc, string fileName)
    static Document FromAcadDocument(object acadDocument)
    static object GetAcadDocument(Document doc)
    [SKIPPED METHOD] GetDocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static StatusBar GetStatusBar(Document doc)

--------------------------------------------------------------------------------

[CLASS] DocumentLock
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLock
Implements: IDisposable

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[ENUM] DocumentLockMode
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockMode
Inherits: Enum

  Values:
    - None = 0
    - AutoWrite = 1
    - NotLocked = 2
    - Write = 4
    - ProtectedAutoWrite = 20
    - Read = 32
    - ExclusiveWrite = 64

--------------------------------------------------------------------------------

[CLASS] DocumentLockModeChangedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockModeChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    DocumentLockMode CurrentMode { get; }
    Document Document { get; }
    string GlobalCommandName { get; }
    DocumentLockMode MyCurrentMode { get; }
    DocumentLockMode MyPreviousMode { get; }

  METHODS:
    void Veto()

--------------------------------------------------------------------------------

[CLASS] DocumentLockModeChangedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockModeChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentLockModeChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentLockModeChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentLockModeChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentLockModeChangeVetoedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockModeChangeVetoedEventArgs
Inherits: EventArgs

  PROPERTIES:
    Document Document { get; }
    string GlobalCommandName { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentLockModeChangeVetoedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockModeChangeVetoedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentLockModeChangeVetoedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentLockModeChangeVetoedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentLockModeChangeVetoedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentLockModeWillChangeEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockModeWillChangeEventArgs
Inherits: EventArgs

  PROPERTIES:
    DocumentLockMode CurrentMode { get; }
    Document Document { get; }
    string GlobalCommandName { get; }
    DocumentLockMode MyCurrentMode { get; }
    DocumentLockMode MyNewMode { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentLockModeWillChangeEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentLockModeWillChangeEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentLockModeWillChangeEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentLockModeWillChangeEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentLockModeWillChangeEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentPinStateChangedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentPinStateChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    RecentDocument RecentDocument { get; }

--------------------------------------------------------------------------------

[CLASS] DocumentPinStateChangedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentPinStateChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentPinStateChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentPinStateChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentPinStateChangedEventArgs e)

--------------------------------------------------------------------------------

[ENUM] DocumentSaveFormat
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentSaveFormat
Inherits: Enum

  Values:
    - R12Dxf = 1
    - R13Dwg = 4
    - R13Dxf = 5
    - R14Dwg = 8
    - R14Dxf = 9
    - R2000Dwg = 12
    - R2000Dxf = 13
    - R2000Template = 14
    - R2000Standard = 15
    - R2000Xml = 16
    - R2004Dwg = 24
    - R2004Dxf = 25
    - R2004Template = 26
    - R2004Standard = 27
    - R2007Dwg = 36
    - R2007Dxf = 37
    - R2007Template = 38
    - R2007Standard = 39
    - R2010Dwg = 48
    - R2010Dxf = 49
    - R2010Template = 50
    - R2010Standard = 51
    - R2013Dwg = 60
    - R2013Dxf = 61
    - R2013Template = 62
    - R2013Standard = 63
    - R2018Dwg = 64
    - R2018Dwg = 64
    - R2018Dxf = 65
    - R2018Template = 66
    - R2018Standard = 67
    - Unknown = -1

--------------------------------------------------------------------------------

[CLASS] DocumentWindowActivatedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowActivatedEventArgs
Inherits: EventArgs

  PROPERTIES:
        [SKIPPED PROPERTY] DocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] DocumentWindowActivatedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowActivatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentWindowActivatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentWindowActivatedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentWindowActivatedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentWindowCollection
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowCollection
Implements: ICollection, IEnumerable, INotifyCollectionChanged, IDisposable

  CONSTRUCTORS:
    new DocumentWindowCollection()

  PROPERTIES:
        [SKIPPED PROPERTY] ActiveDocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    int Count { get; }
    bool IsSynchronized { get; }
    object SyncRoot { get; }

  METHODS:
    [SKIPPED METHOD] AddDocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] AddDocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void ArrangeIcons()
    void Cascade()
    void CopyTo(Array array, int index)
    void Dispose()
    [SKIPPED METHOD] FindDocumentWindowByHandle: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    IEnumerator GetEnumerator()
    [SKIPPED METHOD] MoveDocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void TileHorizontally()
    void TileVertically()

  EVENTS:
    event DocumentWindowToBeDockedEventHandler DocumentWindowToBeDocked
    event DocumentWindowDockedEventHandler DocumentWindowDocked
    event DocumentWindowFloatedEventHandler DocumentWindowFloated
    event DocumentWindowActivatedEventHandler DocumentWindowActivated
    event NotifyCollectionChangedEventHandler CollectionChanged

--------------------------------------------------------------------------------

[CLASS] DocumentWindowDockedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowDockedEventArgs
Inherits: DocumentWindowFloatedOrDockedEventArgs

--------------------------------------------------------------------------------

[CLASS] DocumentWindowDockedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowDockedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentWindowDockedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentWindowDockedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentWindowDockedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentWindowFloatedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowFloatedEventArgs
Inherits: DocumentWindowFloatedOrDockedEventArgs

--------------------------------------------------------------------------------

[CLASS] DocumentWindowFloatedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowFloatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentWindowFloatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentWindowFloatedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentWindowFloatedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentWindowFloatedOrDockedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowFloatedOrDockedEventArgs
Inherits: EventArgs

  PROPERTIES:
        [SKIPPED PROPERTY] DocumentWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] DocumentWindowToBeDockedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowToBeDockedEventArgs
Inherits: DocumentWindowFloatedOrDockedEventArgs

--------------------------------------------------------------------------------

[CLASS] DocumentWindowToBeDockedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DocumentWindowToBeDockedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentWindowToBeDockedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DocumentWindowToBeDockedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DocumentWindowToBeDockedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DrawingOpenEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.DrawingOpenEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database Database { get; }
    string FileName { get; }

--------------------------------------------------------------------------------

[CLASS] DrawingOpenEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.DrawingOpenEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DrawingOpenEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DrawingOpenEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DrawingOpenEventArgs e)

--------------------------------------------------------------------------------

