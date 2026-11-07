# Autodesk.AutoCAD.DatabaseServices.Internal

NAMESPACE: Autodesk.AutoCAD.DatabaseServices.Internal
--------------------------------------------------------------------------------

[ENUM] CompareReturnStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.CompareReturnStatus
Inherits: Enum

  Values:
    - UnableToCompare = 0
    - HistoryCompare = 1
    - RawBinaryCompare = 2

--------------------------------------------------------------------------------

[CLASS] DcDocsFile
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.DcDocsFile

  CONSTRUCTORS:
    new DcDocsFile()

  METHODS:
    static Permissions getPermissions(string sFilepath)
    static bool isDCDOCSFileSource(string sFilepath)

--------------------------------------------------------------------------------

[CLASS] DrawingCompare
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.DrawingCompare

  CONSTRUCTORS:
    new DrawingCompare()

  METHODS:
    CompareReturnStatus CompareDwgFiles(string file1name, string file2name)
    void OnDiffDetected(int drawingId, UInt64* handle, int changeType)

  EVENTS:
    event ObjectDiffEventHandler ObjectDiffEvent

--------------------------------------------------------------------------------

[CLASS] FieldEngine
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.FieldEngine

  CONSTRUCTORS:
    new FieldEngine()

  PROPERTIES:
    static int EvaluationOption { get; set; }

--------------------------------------------------------------------------------

[CLASS] ObjectDiffEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.ObjectDiffEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new ObjectDiffEventArgs(int drawingId, UInt64* handle, int changeType)

  PROPERTIES:
    int ChangeType { get; }
    int DrawingId { get; }
    Handle ObjHandle { get; }

--------------------------------------------------------------------------------

[CLASS] ObjectDiffEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.ObjectDiffEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ObjectDiffEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ObjectDiffEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ObjectDiffEventArgs e)

--------------------------------------------------------------------------------

[ENUM] Permissions
Full Name: Autodesk.AutoCAD.DatabaseServices.Internal.Permissions
Inherits: Enum

  Values:
    - None = 0
    - View = 1
    - Create = 2
    - Delete = 4
    - Rename = 8
    - Move = 16
    - Edit = 32
    - Publish = 64
    - Download = 128
    - Collaborate = 256
    - Control = 512
    - AdminAccess = 1024
    - All = 2047

--------------------------------------------------------------------------------

