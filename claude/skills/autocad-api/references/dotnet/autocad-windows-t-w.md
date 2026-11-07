# Autodesk.AutoCAD.Windows

NAMESPACE: Autodesk.AutoCAD.Windows
--------------------------------------------------------------------------------

[CLASS] TrayItem
Full Name: Autodesk.AutoCAD.Windows.TrayItem
Inherits: StatusBarItem

  CONSTRUCTORS:
    new TrayItem()

  METHODS:
    void CloseBubbleWindows()
    void ShowBubbleWindow(TrayItemBubbleWindow bubble)

--------------------------------------------------------------------------------

[CLASS] TrayItemBubbleWindow
Full Name: Autodesk.AutoCAD.Windows.TrayItemBubbleWindow
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new TrayItemBubbleWindow()

  PROPERTIES:
    string HyperLink { get; set; }
    string HyperText { get; set; }
    IconType IconType { get; set; }
    string Text { get; set; }
    string Text2 { get; set; }
    string Title { get; set; }

  EVENTS:
    event TrayItemBubbleWindowClosedEventHandler Closed

--------------------------------------------------------------------------------

[CLASS] TrayItemBubbleWindowClosedEventArgs
Full Name: Autodesk.AutoCAD.Windows.TrayItemBubbleWindowClosedEventArgs
Inherits: EventArgs

  PROPERTIES:
    TrayItemBubbleWindowCloseReason CloseReason { get; }

--------------------------------------------------------------------------------

[CLASS] TrayItemBubbleWindowClosedEventHandler
Full Name: Autodesk.AutoCAD.Windows.TrayItemBubbleWindowClosedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new TrayItemBubbleWindowClosedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, TrayItemBubbleWindowClosedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, TrayItemBubbleWindowClosedEventArgs e)

--------------------------------------------------------------------------------

[ENUM] TrayItemBubbleWindowCloseReason
Full Name: Autodesk.AutoCAD.Windows.TrayItemBubbleWindowCloseReason
Inherits: Enum

  Values:
    - FailedToCreate = 0
    - NoIcons = 1
    - NoNotifications = 2
    - ClosedByUser = 3
    - TimedOut = 4
    - HyperlinkClicked = 5
    - DocumentDeactivated = 7

--------------------------------------------------------------------------------

[CLASS] TrayItemCollection
Full Name: Autodesk.AutoCAD.Windows.TrayItemCollection
Implements: IList, ICollection, IEnumerable

  PROPERTIES:
    int Count { get; set; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    TrayItem Item { get; set; }

  METHODS:
    int Add(TrayItem value)
    void Clear()
    bool Contains(TrayItem value)
    void CopyTo(TrayItem[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(TrayItem value)
    void Insert(int index, TrayItem value)
    void Remove(TrayItem value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] Visuals
Full Name: Autodesk.AutoCAD.Windows.Visuals

  PROPERTIES:
    static     [SKIPPED PROPERTY] ApplicationIcon: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static     [SKIPPED PROPERTY] PickBitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static     [SKIPPED PROPERTY] PickSetBitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[ABSTRACT CLASS] WindowExtension
Full Name: Autodesk.AutoCAD.Windows.WindowExtension

  METHODS:
    [SKIPPED METHOD] GetIcon: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] GetLocation: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] GetSize: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] SetIcon: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] SetLocation: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] SetSize: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

