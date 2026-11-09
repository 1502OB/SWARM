# Autodesk.AutoCAD.Windows

NAMESPACE: Autodesk.AutoCAD.Windows
--------------------------------------------------------------------------------

[CLASS] ColorDialog
Full Name: Autodesk.AutoCAD.Windows.ColorDialog

  CONSTRUCTORS:
    new ColorDialog()

  PROPERTIES:
    Color Color { get; set; }
    bool IncludeByBlockByLayer { get; set; }

  METHODS:
    void SetDialogTabs(ColorTabs value)
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[CLASS] ContextMenuExtension
Full Name: Autodesk.AutoCAD.Windows.ContextMenuExtension
Inherits: Menu
Implements: IDisposable

  CONSTRUCTORS:
    new ContextMenuExtension()

  PROPERTIES:
    string Title { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event EventHandler Popup

--------------------------------------------------------------------------------

[ENUM] DefaultPane
Full Name: Autodesk.AutoCAD.Windows.DefaultPane
Inherits: Enum

  Values:
    - ModeMacro = 0
    - CursorCoordinates = 1
    - Snap = 2
    - Grid = 3
    - Ortho = 4
    - Polar = 5
    - ObjectTrack = 6
    - LineWeight = 7
    - PaperModel = 8
    - Paper = 9
    - Model = 10
    - ObjectSnap = 11
    - Float = 12
    - Table = 13
    - Spacer = 14
    - ViewportMaximizePrevious = 15
    - ViewportMaximize = 16
    - ViewportMaximizeNext = 17
    - DynamicInput = 18
    - DynamicUcs = 19
    - LayoutModelIcons = 20
    - ModelIcon = 21
    - LayoutIcon = 22
    - LayoutMoreIcon = 23
    - All = 24

--------------------------------------------------------------------------------

[ENUM] DockSides
Full Name: Autodesk.AutoCAD.Windows.DockSides
Inherits: Enum

  Values:
    - None = 0
    - Left = 4096
    - Top = 8192
    - Right = 16384
    - Bottom = 32768

--------------------------------------------------------------------------------

[CLASS] DocumentWindowLoadedEventHandler
Full Name: Autodesk.AutoCAD.Windows.DocumentWindowLoadedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentWindowLoadedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] DocumentWindowUpdatedEventHandler
Full Name: Autodesk.AutoCAD.Windows.DocumentWindowUpdatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DocumentWindowUpdatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DropTarget
Full Name: Autodesk.AutoCAD.Windows.DropTarget

  METHODS:
    [SKIPPED METHOD] OnDragEnter: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    void OnDragLeave()
    [SKIPPED METHOD] OnDragOver: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    [SKIPPED METHOD] OnDrop: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[ENUM] IconType
Full Name: Autodesk.AutoCAD.Windows.IconType
Inherits: Enum

  Values:
    - None = 0
    - Information = 1
    - Critical = 2
    - Warning = 3

--------------------------------------------------------------------------------

[CLASS] InfoCenter
Full Name: Autodesk.AutoCAD.Windows.InfoCenter

  CONSTRUCTORS:
    new InfoCenter()

  PROPERTIES:
        [SKIPPED PROPERTY] Host: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    bool KeepFocus { get; set; }
    string SubAwareClientInfo { get; }
    string UPIXMLData { get; }
    bool Visible { get; set; }

  METHODS:
    void InfoToolbarSizeChanged(bool bExpand)
    void InvokeToolbarMoveEvent()
    void InvokeToolbarResizeEvent(int width)
    void LaunchSubAwareModule(short resReqid, string strCourseId, string strModuleId)

--------------------------------------------------------------------------------

[CLASS] InfoToolbarMoveDelegate
Full Name: Autodesk.AutoCAD.Windows.InfoToolbarMoveDelegate
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InfoToolbarMoveDelegate(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] InfoToolbarResizeTo
Full Name: Autodesk.AutoCAD.Windows.InfoToolbarResizeTo
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InfoToolbarResizeTo(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(int width, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(int width)

--------------------------------------------------------------------------------

[CLASS] LayerTransparencyDialog
Full Name: Autodesk.AutoCAD.Windows.LayerTransparencyDialog

  CONSTRUCTORS:
    new LayerTransparencyDialog()

  PROPERTIES:
    uint Percent { get; set; }

  METHODS:
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[CLASS] LinetypeDialog
Full Name: Autodesk.AutoCAD.Windows.LinetypeDialog

  CONSTRUCTORS:
    new LinetypeDialog()

  PROPERTIES:
    bool IncludeByBlockByLayer { get; set; }
    ObjectId Linetype { get; set; }

  METHODS:
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[CLASS] LineWeightDialog
Full Name: Autodesk.AutoCAD.Windows.LineWeightDialog

  CONSTRUCTORS:
    new LineWeightDialog()

  PROPERTIES:
    bool IncludeByBlockByLayer { get; set; }
    LineWeight LineWeight { get; set; }

  METHODS:
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Menu
Full Name: Autodesk.AutoCAD.Windows.Menu

  PROPERTIES:
    MenuItemCollection MenuItems { get; }

--------------------------------------------------------------------------------

[CLASS] MenuItemCollection
Full Name: Autodesk.AutoCAD.Windows.MenuItemCollection
Implements: IList, ICollection, IEnumerable, IEnumerable<IMenuItem>

  CONSTRUCTORS:
    new MenuItemCollection(Menu owner)

  PROPERTIES:
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
        [SKIPPED PROPERTY] Item: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.

  METHODS:
    [SKIPPED METHOD] Add: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    void Clear()
    [SKIPPED METHOD] Contains: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] CopyTo: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    IEnumerator<IMenuItem> GetEnumerator()
    [SKIPPED METHOD] IndexOf: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] Insert: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] Remove: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] OpenFileDialog
Full Name: Autodesk.AutoCAD.Windows.OpenFileDialog

  CONSTRUCTORS:
    new OpenFileDialog(string title, string defaultName, string extension, string dialogName, OpenFileDialogFlags flags)

  PROPERTIES:
    string Filename { get; }

  METHODS:
    string[] GetFilenames()
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[CLASS] OpenFileOrFolderDialog
Full Name: Autodesk.AutoCAD.Windows.OpenFileOrFolderDialog

  CONSTRUCTORS:
    new OpenFileOrFolderDialog(string title, string defaultName, string extension, string dialogName, OpenFileDialogFlags flags)

  PROPERTIES:
    string FileOrFoldername { get; }

  METHODS:
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] Palette
Full Name: Autodesk.AutoCAD.Windows.Palette

  PROPERTIES:
    string Name { get; set; }
        [SKIPPED PROPERTY] PaletteSet: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] PaletteActivatedEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteActivatedEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteActivatedEventArgs(Palette activated, Palette deactivated)

  PROPERTIES:
    Palette Activated { get; }
    Palette Deactivated { get; }

--------------------------------------------------------------------------------

[CLASS] PaletteActivatedEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteActivatedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteActivatedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteActivatedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteActivatedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteAddContextMenuEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteAddContextMenuEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteAddContextMenuEventArgs(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.

  PROPERTIES:
    int HitFlag { get; }
        [SKIPPED PROPERTY] MenuItems: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    List<int> RemoveMenuItems { get; }
    int RightClickTab { get; }

--------------------------------------------------------------------------------

[CLASS] PaletteAddContextMenuEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteAddContextMenuEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteAddContextMenuEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteAddContextMenuEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteAddContextMenuEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteEnterSizeMoveEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteEnterSizeMoveEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteEnterSizeMoveEventArgs(bool bEnterSizeMove)

  PROPERTIES:
    bool EnterSizeMove { get; }

--------------------------------------------------------------------------------

[CLASS] PaletteEnterSizeMoveEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteEnterSizeMoveEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteEnterSizeMoveEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteEnterSizeMoveEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteEnterSizeMoveEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PalettePersistEventArgs
Full Name: Autodesk.AutoCAD.Windows.PalettePersistEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PalettePersistEventArgs(IConfigurationSection configurationSection)

  PROPERTIES:
    IConfigurationSection ConfigurationSection { get; }

--------------------------------------------------------------------------------

[CLASS] PalettePersistEventHandler
Full Name: Autodesk.AutoCAD.Windows.PalettePersistEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PalettePersistEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PalettePersistEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PalettePersistEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetDestroyEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetDestroyEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetDestroyEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetDockSite
Full Name: Autodesk.AutoCAD.Windows.PaletteSetDockSite

  CONSTRUCTORS:
    new PaletteSetDockSite()

  METHODS:
    [SKIPPED METHOD] CanDock: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] Dock: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] Initialize: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void Uninitialize()

--------------------------------------------------------------------------------

[CLASS] PaletteSetFocusedEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteSetFocusedEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteSetFocusedEventArgs()

--------------------------------------------------------------------------------

[CLASS] PaletteSetFocusedEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetFocusedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetFocusedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteSetFocusedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteSetFocusedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetHelpEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteSetHelpEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteSetHelpEventArgs()

--------------------------------------------------------------------------------

[CLASS] PaletteSetHelpEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetHelpEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetHelpEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteSetHelpEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteSetHelpEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetMoveEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteSetMoveEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteSetMoveEventArgs(int x, int y)

  PROPERTIES:
    int X { get; }
    int y { get; }

--------------------------------------------------------------------------------

[CLASS] PaletteSetMoveEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetMoveEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetMoveEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteSetMoveEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteSetMoveEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetShowDockBarEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetShowDockBarEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetShowDockBarEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteShowDockBarEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteShowDockBarEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetSizeEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteSetSizeEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteSetSizeEventArgs(int cx, int cy, double dx, double dy)

  PROPERTIES:
    double DeviceIndependentHeight { get; }
    double DeviceIndependentWidth { get; }
    int Height { get; }
    int Width { get; }

--------------------------------------------------------------------------------

[CLASS] PaletteSetSizeEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetSizeEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetSizeEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteSetSizeEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteSetSizeEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteSetStateEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteSetStateEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteSetStateEventArgs(StateEventIndex state)

  PROPERTIES:
    StateEventIndex NewState { get; }

--------------------------------------------------------------------------------

[CLASS] PaletteSetStateEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetStateEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetStateEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PaletteSetStateEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PaletteSetStateEventArgs e)

--------------------------------------------------------------------------------

[ENUM] PaletteSetStyles
Full Name: Autodesk.AutoCAD.Windows.PaletteSetStyles
Inherits: Enum

  Values:
    - ShowAutoHideButton = 2
    - ShowPropertiesMenu = 4
    - ShowCloseButton = 8
    - NameEditable = 16
    - Snappable = 32
    - ShowTabForSingle = 64
    - UsePaletteNameAsTitleForSingle = 128
    - SingleRowDock = 512
    - Notify = 1024
    - SingleRowNoVertResize = 2048
    - SingleColDock = 4096
    - NoTitleBar = 32768
    - PauseAutoRollupForChildModalDialog = 65536

--------------------------------------------------------------------------------

[ENUM] PaletteSetTitleBarLocation
Full Name: Autodesk.AutoCAD.Windows.PaletteSetTitleBarLocation
Inherits: Enum

  Values:
    - Left = 0
    - Right = 1

--------------------------------------------------------------------------------

[CLASS] PaletteSetTitleBarLocationChangeEventHandler
Full Name: Autodesk.AutoCAD.Windows.PaletteSetTitleBarLocationChangeEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PaletteSetTitleBarLocationChangeEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] PaletteShowDockBarEventArgs
Full Name: Autodesk.AutoCAD.Windows.PaletteShowDockBarEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PaletteShowDockBarEventArgs(bool bShowDockBar)

  PROPERTIES:
    bool ShowDockBar { get; }

--------------------------------------------------------------------------------

[CLASS] Pane
Full Name: Autodesk.AutoCAD.Windows.Pane
Inherits: StatusBarItem

  CONSTRUCTORS:
    new Pane()

  PROPERTIES:
    int MaximumWidth { get; set; }
    int MinimumWidth { get; set; }
    PaneStyles Style { get; set; }
    string Text { get; set; }

--------------------------------------------------------------------------------

[CLASS] PaneCollection
Full Name: Autodesk.AutoCAD.Windows.PaneCollection
Implements: IList, ICollection, IEnumerable

  PROPERTIES:
    int Count { get; set; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    Pane Item { get; set; }

  METHODS:
    int Add(Pane value)
    void Clear()
    bool Contains(Pane value)
    void CopyTo(Pane[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(Pane value)
    void Insert(int index, Pane value)
    void Remove(Pane value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ENUM] PaneStyles
Full Name: Autodesk.AutoCAD.Windows.PaneStyles
Inherits: Enum

  Values:
    - NoBorders = 1
    - PopOut = 2
    - Stretch = 4
    - Normal = 8
    - Command = 16
    - PopUp = 32

--------------------------------------------------------------------------------

[CLASS] PlotStyleDialog
Full Name: Autodesk.AutoCAD.Windows.PlotStyleDialog

  CONSTRUCTORS:
    new PlotStyleDialog()

  PROPERTIES:
    bool IncludeByBlockByLayer { get; set; }
    string PlotStyle { get; set; }

  METHODS:
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[CLASS] SaveFileDialog
Full Name: Autodesk.AutoCAD.Windows.SaveFileDialog

  CONSTRUCTORS:
    new SaveFileDialog(string title, string defaultName, string extension, string dialogName, SaveFileDialogFlags flags)

  PROPERTIES:
    string Filename { get; }

  METHODS:
    [SKIPPED METHOD] ShowDialog: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    Nullable<bool> ShowModal()

--------------------------------------------------------------------------------

[ENUM] StateEventIndex
Full Name: Autodesk.AutoCAD.Windows.StateEventIndex
Inherits: Enum

  Values:
    - Hide = 0
    - Show = 1
    - ThemeChange = 4

--------------------------------------------------------------------------------

[CLASS] StatusBar
Full Name: Autodesk.AutoCAD.Windows.StatusBar

  PROPERTIES:
    PaneCollection Panes { get; }
    TrayItemCollection TrayItems { get; }
        [SKIPPED PROPERTY] Window: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  METHODS:
    void CloseBubbleWindows()
    Pane GetDefaultPane(DefaultPane pane)
    void RemoveDefaultPane(DefaultPane pane)
    void Update()

--------------------------------------------------------------------------------

[CLASS] StatusBarItem
Full Name: Autodesk.AutoCAD.Windows.StatusBarItem
Inherits: DisposableWrapper

  PROPERTIES:
    bool Enabled { get; set; }
        [SKIPPED PROPERTY] Icon: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    string ToolTipText { get; set; }
    bool Visible { get; set; }

  METHODS:
    Point PointToClient(Point p)
    Point PointToScreen(Point p)

  EVENTS:
    event EventHandler Deleted
    event StatusBarMouseDownEventHandler MouseDown

--------------------------------------------------------------------------------

[CLASS] StatusBarMouseDownEventHandler
Full Name: Autodesk.AutoCAD.Windows.StatusBarMouseDownEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new StatusBarMouseDownEventHandler(object A_0, IntPtr A_1)

  METHODS:
    [SKIPPED METHOD] BeginInvoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    void EndInvoke(IAsyncResult result)
    [SKIPPED METHOD] Invoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

