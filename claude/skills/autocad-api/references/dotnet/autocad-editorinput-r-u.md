# Autodesk.AutoCAD.EditorInput

NAMESPACE: Autodesk.AutoCAD.EditorInput
--------------------------------------------------------------------------------

[CLASS] RolloverEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.RolloverEventArgs
Inherits: EventArgs

  PROPERTIES:
    FullSubentityPath Highlighted { get; set; }
    FullSubentityPath Picked { get; }

--------------------------------------------------------------------------------

[CLASS] RolloverEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.RolloverEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new RolloverEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, RolloverEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, RolloverEventArgs e)

--------------------------------------------------------------------------------

[ENUM] SamplerStatus
Full Name: Autodesk.AutoCAD.EditorInput.SamplerStatus
Inherits: Enum

  Values:
    - OK = 0
    - NoChange = 1
    - Cancel = 2

--------------------------------------------------------------------------------

[CLASS] SelectedObject
Full Name: Autodesk.AutoCAD.EditorInput.SelectedObject

  CONSTRUCTORS:
    new SelectedObject(ObjectId id, SelectedSubObject[] subSelections)
    new SelectedObject(ObjectId id, SelectionMethod method, IntPtr gsMarker)
    new SelectedObject(ObjectId id, SelectionMethod method, long gsMarker)

  PROPERTIES:
    long GraphicsSystemMarker { get; }
    IntPtr GraphicsSystemMarkerPtr { get; }
    ObjectId ObjectId { get; }
    SelectionDetails OptionalDetails { get; set; }
    SelectionMethod SelectionMethod { get; }

  METHODS:
    SelectedSubObject[] GetSubentities()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] SelectedSubObject
Full Name: Autodesk.AutoCAD.EditorInput.SelectedSubObject

  CONSTRUCTORS:
    new SelectedSubObject(FullSubentityPath path, SelectionMethod method, IntPtr gsMarker)
    new SelectedSubObject(FullSubentityPath path, SelectionMethod method, long gsMarker)

  PROPERTIES:
    FullSubentityPath FullSubentityPath { get; }
    long GraphicsSystemMarker { get; }
    IntPtr GraphicsSystemMarkerPtr { get; }
    SelectionDetails OptionalDetails { get; }
    SelectionMethod SelectionMethod { get; }

  METHODS:
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] SelectionAddedEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.SelectionAddedEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    SelectionSet AddedObjects { get; }
    SelectionFlags Flags { get; }
    SelectionSet Selection { get; }

  METHODS:
    void Add(SelectedObject value)
    void AddSubEntity(SelectedSubObject value)
    void Dispose()
    void Highlight(int subSelectionIndex, FullSubentityPath path)
    void Remove(int index)
    void RemoveSubEntity(int entityIndex, int subEntityIndex)

--------------------------------------------------------------------------------

[CLASS] SelectionAddedEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.SelectionAddedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SelectionAddedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SelectionAddedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SelectionAddedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] SelectionDetails
Full Name: Autodesk.AutoCAD.EditorInput.SelectionDetails

  PROPERTIES:
    Matrix3d Transform { get; }

  METHODS:
    ObjectId[] GetContainers()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] SelectionFilter
Full Name: Autodesk.AutoCAD.EditorInput.SelectionFilter

  CONSTRUCTORS:
    new SelectionFilter(TypedValue[] value)

  METHODS:
    TypedValue[] GetFilter()

--------------------------------------------------------------------------------

[ENUM] SelectionFlags
Full Name: Autodesk.AutoCAD.EditorInput.SelectionFlags
Inherits: Enum

  Values:
    - Normal = 0
    - PickPoints = 1
    - Duplicates = 2
    - NestedEntities = 4
    - SubEntities = 8
    - SinglePick = 16
    - PickfirstSet = 32
    - PreviousSet = 64
    - SubSelection = 128
    - Undo = 256
    - FailedPickAuto = 512

--------------------------------------------------------------------------------

[ENUM] SelectionMethod
Full Name: Autodesk.AutoCAD.EditorInput.SelectionMethod
Inherits: Enum

  Values:
    - NonGraphical = 0
    - PickPoint = 1
    - Window = 2
    - Crossing = 3
    - Fence = 4
    - SubEntity = 5
    - Unavailable = -1

--------------------------------------------------------------------------------

[ENUM] SelectionMode
Full Name: Autodesk.AutoCAD.EditorInput.SelectionMode
Inherits: Enum

  Values:
    - Window = 1
    - Crossing = 2
    - Box = 3
    - Last = 4
    - Entity = 5
    - All = 6
    - FencePolyline = 7
    - CrossingPolygon = 8
    - WindowPolygon = 9
    - Pick = 10
    - Every = 11
    - Extents = 12
    - Group = 13
    - Previous = 14
    - Multiple = 15

--------------------------------------------------------------------------------

[CLASS] SelectionRemovedEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.SelectionRemovedEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    SelectionFlags Flags { get; }
    SelectionSet RemovedObjects { get; }
    SelectionSet Selection { get; }

  METHODS:
    void Dispose()
    void Remove(int index)
    void RemoveSubentity(int index, int subentityIndex)

--------------------------------------------------------------------------------

[CLASS] SelectionRemovedEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.SelectionRemovedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SelectionRemovedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SelectionRemovedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SelectionRemovedEventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SelectionSet
Full Name: Autodesk.AutoCAD.EditorInput.SelectionSet
Implements: ICollection, IEnumerable, IDisposable

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    SelectedObject Item { get; }
    object SyncRoot { get; }

  METHODS:
    void CopyTo(SelectedObject[] array, int index)
    void Dispose()
    static SelectionSet FromObjectIds(ObjectId[] ids)
    IEnumerator GetEnumerator()
    ObjectId[] GetObjectIds()
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] SelectionSetDelayMarshalled
Full Name: Autodesk.AutoCAD.EditorInput.SelectionSetDelayMarshalled
Inherits: SelectionSet

  PROPERTIES:
    int Count { get; }
    SelectedObject Item { get; }

  METHODS:
    IEnumerator GetEnumerator()
    ObjectId[] GetObjectIds()

--------------------------------------------------------------------------------

[CLASS] SelectionTextInputEventArgs
Full Name: Autodesk.AutoCAD.EditorInput.SelectionTextInputEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Input { get; }

  METHODS:
    void AddObjects(ObjectId[] ids)
    void SetErrorMessage(string errorMessage)

--------------------------------------------------------------------------------

[CLASS] SelectionTextInputEventHandler
Full Name: Autodesk.AutoCAD.EditorInput.SelectionTextInputEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SelectionTextInputEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SelectionTextInputEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SelectionTextInputEventArgs e)

--------------------------------------------------------------------------------

[ENUM] SubtractedKeywords
Full Name: Autodesk.AutoCAD.EditorInput.SubtractedKeywords
Inherits: Enum

  Values:
    - LastAllGroupPrevious = 1
    - PickImplied = 2
    - All = 4
    - BoxAuto = 8
    - Multiple = 16
    - CrossingCPolygon = 32
    - Fence = 64
    - Last = 128
    - Previous = 256
    - WindowWPolygon = 512
    - Group = 1024
    - AddRemove = 2048

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Transient
Full Name: Autodesk.AutoCAD.EditorInput.Transient
Inherits: Drawable

  PROPERTIES:
    static Drawable CapturedDrawable { get; set; }
    ObjectId Id { get; }
    bool IsPersistent { get; }

  EVENTS:
    event EventHandler Captured
    event EventHandler CaptureReleased
    event PointInputEventHandler PointInput
    event DeviceInputEventHandler DeviceInput

--------------------------------------------------------------------------------

[ENUM] UserInputControls
Full Name: Autodesk.AutoCAD.EditorInput.UserInputControls
Inherits: Enum

  Values:
    - GovernedByOrthoMode = 1
    - NullResponseAccepted = 2
    - DoNotEchoCancelForCtrlC = 4
    - DoNotUpdateLastPoint = 8
    - NoDwgLimitsChecking = 16
    - NoZeroResponseAccepted = 32
    - NoNegativeResponseAccepted = 64
    - Accept3dCoordinates = 128
    - AcceptMouseUpAsPoint = 256
    - AnyBlankTerminatesInput = 512
    - InitialBlankTerminatesInput = 1024
    - AcceptOtherInputString = 2048
    - GovernedByUCSDetect = 4096
    - NoZDirectionOrtho = 8192
    - UseBasePointElevation = 32768

--------------------------------------------------------------------------------

