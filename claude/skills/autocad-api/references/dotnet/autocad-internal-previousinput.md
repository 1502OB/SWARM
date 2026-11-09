# Autodesk.AutoCAD.Internal.PreviousInput

NAMESPACE: Autodesk.AutoCAD.Internal.PreviousInput
--------------------------------------------------------------------------------

[STRUCT] AcPiMonitorReactorImpl
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.AcPiMonitorReactorImpl

--------------------------------------------------------------------------------

[CLASS] CommandLineMonitor
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.CommandLineMonitor

  PROPERTIES:
    string CurrentCommandName { get; }
    double CurrentDoubleOut { get; }
    int CurrentInitgetFlags { get; }
    string CurrentInitgetKeywords { get; }
    InputControlFlags CurrentInputControlFlags { get; }
    long CurrentIntOut { get; }
    string CurrentKeywordsIn { get; }
    InputModes CurrentMode { get; }
    InputModes CurrentModes { get; }
    int CurrentNestingCount { get; }
    ValueType CurrentPointIn { get; }
    bool CurrentPointInFlag { get; }
    ValueType CurrentPointOut { get; }
    ValueType CurrentPointOut2d { get; }
    bool CurrentPointOut2dFlag { get; }
    bool CurrentPointOutFlag { get; }
    string CurrentPrompt { get; }
    PromptStatus CurrentReturnStatus { get; }
    string CurrentSSControlsIn { get; }
    bool CurrentSSGetConsumedPickfirst { get; }
    int CurrentSSGetCount { get; }
    bool CurrentSSGetIsManual { get; }
    ArrayList CurrentSSGetKeywordsUsed { get; }
    string CurrentStringOut { get; }
    string LastCommandName { get; }
    int LastNotifiedSSGetCount { get; }

  METHODS:
    void EnableSsGetFilter(bool on)

  EVENTS:
    event GetInputEventHandler GetInput
    event SelectEventHandler PickfirstEnded
    event SelectEventHandler PickfirstWillStart
    event SelectOperationEventHandler SelectOperation
    event InputStringEventHandler EndSSGetCustomKeywordCallback
    event InputStringEventHandler BeginSSGetCustomKeywordCallback
    event GetAngleEventHandler GetAngleEnded
    event GetAngleEventHandler GetAngleWillStart
    event GetDistanceEventHandler GetDistanceEnded
    event GetDistanceEventHandler GetDistanceWillStart
    event GetInputEventHandler SelectEnded
    event SelectEventHandler SelectWillStart
    event InputStringEventHandler UnknownCmd
    event InputStringEventHandler CommandEnded
    event InputStringEventHandler CommandWillStart

--------------------------------------------------------------------------------

[CLASS] CommandLineMonitorServices
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.CommandLineMonitorServices

  METHODS:
    CommandFlags ActiveCommandFlags()
    int CommandActiveBits()
    int CommandNestedDepth()
    CommandLineMonitor GetCommandLineMonitor(Document doc)
    string GetLocalCommandName(string globalCmdName)
    static CommandLineMonitorServices Instance()
    bool IsCurrentCommandTransparent()

--------------------------------------------------------------------------------

[CLASS] GetAngleEventHandler
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.GetAngleEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetAngleEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, GetInputEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, GetInputEventArgs e)

--------------------------------------------------------------------------------

[CLASS] GetDistanceEventHandler
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.GetDistanceEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetDistanceEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, GetInputEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, GetInputEventArgs e)

--------------------------------------------------------------------------------

[CLASS] GetInputEventArgs
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.GetInputEventArgs
Inherits: EventArgs

  PROPERTIES:
    InputModes Mode { get; }
    PromptStatus ReturnStatus { get; }
    object Value { get; }

--------------------------------------------------------------------------------

[CLASS] GetInputEventHandler
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.GetInputEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetInputEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, GetInputEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, GetInputEventArgs e)

--------------------------------------------------------------------------------

[ENUM] InputControlFlags
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.InputControlFlags
Inherits: Enum

  Values:
    - udNONE = 0
    - udBASE = 1
    - udWNOSORT = 2
    - udWNOSORT = 2
    - udNULLOK = 4
    - udKWORD = 8
    - udDRAG = 16
    - udSILENTETX = 32
    - udCURSOR = 64
    - udMSG = 128
    - udNOLAST = 256
    - udNOLIM = 512
    - udOTHER = 1024
    - udNOZERO = 2048
    - udNONEG = 4096
    - udENTITY = 8192
    - udGETZ = 16384
    - udNORANGEMSG = 32768
    - udNOPFILT = 65536
    - udNOOSNAP = 131072
    - udFLATCUR = 262144
    - udDISPLPT = 524288
    - udMSGNUM = 1048576
    - udNOSMODE = 2097152
    - ud2DDIST = 4194304
    - udRESCAN = 8388608
    - udPOINTUP = 16777216
    - udANYBLANK = 33554432
    - udFIRSTBLANK = 67108864
    - udIGNOREETX = 134217728
    - udPOINTOK = 268435456
    - udLABELOK = 536870912
    - udBKSL = 1073741824
    - udXCTL = -2147483648

--------------------------------------------------------------------------------

[ENUM] InputModes
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.InputModes
Inherits: Enum

  Values:
    - None = 0
    - Point = 1
    - Angle = 2
    - Distance = 4
    - Orientation = 8
    - Corner = 16
    - ScaleFactor = 32
    - String = 64
    - kword = 128
    - Integer = 256
    - Color = 512
    - Real = 2048
    - EntSel = 4096
    - Nentsel = 8192
    - SSGet = 16384
    - Drag = 32768
    - Cmd = 65536
    - UnknownCmd = 131072
    - Vector = 262144
    - ScaleFactor2 = 524288
    - DynDimGetPoint = 1048576
    - RawAngle = 8388608
    - AllGetInput = 16284671

--------------------------------------------------------------------------------

[CLASS] InputStringEventArgs
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.InputStringEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Value { get; }

--------------------------------------------------------------------------------

[CLASS] InputStringEventHandler
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.InputStringEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InputStringEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, InputStringEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, InputStringEventArgs e)

--------------------------------------------------------------------------------

[CLASS] SelectEventHandler
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.SelectEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SelectEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] SelectOperationEventArgs
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.SelectOperationEventArgs
Inherits: EventArgs

  PROPERTIES:
    string GroupName { get; }
    bool IsAdding { get; }
    bool IsCancel { get; }
    bool IsCrossingBox { get; }
    bool IsSubSelection { get; }
    Point3dCollection Points { get; }
    int SelectionFlags { get; }
    SelectionMode SelectionMode { get; }

--------------------------------------------------------------------------------

[CLASS] SelectOperationEventHandler
Full Name: Autodesk.AutoCAD.Internal.PreviousInput.SelectOperationEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SelectOperationEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SelectOperationEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SelectOperationEventArgs e)

--------------------------------------------------------------------------------

