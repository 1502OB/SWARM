# Autodesk.AutoCAD.Windows.ToolPalette

NAMESPACE: Autodesk.AutoCAD.Windows.ToolPalette
--------------------------------------------------------------------------------

[STRUCT] AcTcToolReactorImpl
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.AcTcToolReactorImpl

--------------------------------------------------------------------------------

[ENUM] ContextMenuMode
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ContextMenuMode
Inherits: Enum

  Values:
    - ContextMenuPaletteTool = 0
    - ContextMenuEditorImage = 1
    - ContextMenuPaletteBackground = 2
    - ContextMenuPaletteSetTab = 3
    - ContextMenuPaletteSetCaption = 4
    - ContextMenuPaletteSetOptionButton = 5
    - ContextNone = -1

--------------------------------------------------------------------------------

[CLASS] DragHandler
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.DragHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DragHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(ValueType pTool, uint dwKeyState, TPOperationFlag tpOperationFlag, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(ValueType pTool, uint dwKeyState, TPOperationFlag tpOperationFlag)

--------------------------------------------------------------------------------

[CLASS] DropPointHandler
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.DropPointHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DropPointHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(Point3d point, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(Point3d point)

--------------------------------------------------------------------------------

[ENUM] ExecutionContext
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ExecutionContext
Inherits: Enum

  Values:
    - LeftButtonClicked = 0
    - DroppedInDrawing = 1

--------------------------------------------------------------------------------

[CLASS] FlyoutEntryAttribute
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.FlyoutEntryAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new FlyoutEntryAttribute(int dispatchId)

  PROPERTIES:
    int DispId { get; }

--------------------------------------------------------------------------------

[CLASS] PerPropertyBrowsingEntryAttribute
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.PerPropertyBrowsingEntryAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new PerPropertyBrowsingEntryAttribute(int dispatchId, string programId, string leftIconResource, int leftIconType, string ellipsisBmpResource, int ellipsisBmpType, int textColor, bool fullView, int integralHeight, int weight)

  PROPERTIES:
    int DispId { get; }
    string EllipsisBmpResource { get; }
    int EllipsisBmpType { get; }
    int IntegralHeight { get; }
    bool IsFullView { get; }
    string LefIconResource { get; }
    int LeftIconType { get; }
    string ProgId { get; }
    int TextColor { get; }
    int Weight { get; }

--------------------------------------------------------------------------------

[CLASS] PickHandler
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.PickHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PickHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(ValueType pTool, uint dwKeyState, TPOperationFlag tpOperationFlag, int nIndex, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(ValueType pTool, uint dwKeyState, TPOperationFlag tpOperationFlag, int nIndex)

--------------------------------------------------------------------------------

[ENUM] ReturnStatus
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ReturnStatus
Inherits: Enum

  Values:
    - ContextMenuShow = 0
    - ContextMenuHide = 1
    - ExecutionCanceled = 2
    - ExecutionCancelRejected = 3
    - ContextMenuUpdatePalette = 4

--------------------------------------------------------------------------------

[CLASS] ToolAttribute
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ToolAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new ToolAttribute(string toolName, string toolImage)

  PROPERTIES:
    string ToolImage { get; }
    string ToolName { get; }

--------------------------------------------------------------------------------

[ENUM] ToolEditFlags
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ToolEditFlags
Inherits: Enum

  Values:
    - EditDefault = 0
    - EditCustom = 1
    - EditNone = 2
    - EditMultiple = 4

--------------------------------------------------------------------------------

[STRUCT] ToolItemInfo
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ToolItemInfo

--------------------------------------------------------------------------------

[CLASS] ToolReactorManager
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.ToolReactorManager

  METHODS:
    static ToolReactorManager Instance()
    void invokeToolContextMenu(Guid itemId, int nIndex)
    void invokeToolDrag(Guid itemId, uint dwKeyState, Point3d point)
    void invokeToolPick(Guid itemId, uint dwKeyState)

  EVENTS:
    event DropPointHandler DropPoint
    event DragHandler Drag
    event PickHandler Pick

--------------------------------------------------------------------------------

[ENUM] TPOperationFlag
Full Name: Autodesk.AutoCAD.Windows.ToolPalette.TPOperationFlag
Inherits: Enum

  Values:
    - Pick = 1
    - Drag = 2
    - ContextMenu = 4

--------------------------------------------------------------------------------

