# Autodesk.AutoCAD.Internal.PropertyInspector

NAMESPACE: Autodesk.AutoCAD.Internal.PropertyInspector
--------------------------------------------------------------------------------

[STRUCT] AcEdPropertyInspectorReactorImpl
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.AcEdPropertyInspectorReactorImpl

--------------------------------------------------------------------------------

[CLASS] CategoryCollectable
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.CategoryCollectable
Inherits: CollectableObject

  CONSTRUCTORS:
    new CategoryCollectable()

  PROPERTIES:
    int CategoryId { get; }
    string Description { get; }
    string Name { get; }
    CollectionVector Properties { get; }
    int Weight { get; set; }

  METHODS:
    bool IsCollapsed()
    bool IsEqual(CategoryCollectable category, CHECK_LEVEL check, ref uint flags)
    bool IsEqual(string name, int categoryId, int parentCategoryId)
    bool IsEqual(string name, int categoryId)
    bool IsEqual(string name)
    void RemoveDynamicCollectables(bool deleteProperty)
    void RemoveDynamicCollectables()
    void Sync(CategoryCollectable category, uint inequalityFlags, bool literalUnion)
    void Sync(CategoryCollectable category, uint inequalityFlags)
    void Sync(CategoryCollectable category)
    int ToggleState()

--------------------------------------------------------------------------------

[ENUM] CategoryInequalityFlags
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.CategoryInequalityFlags
Inherits: Enum

  Values:
    - Name = 1
    - Description = 2
    - CategoryCommandButtons = 4
    - CategoryId = 8
    - Weight = 16
    - Parent = 32
    - Visible = 64

--------------------------------------------------------------------------------

[CLASS] CollectableObject
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.CollectableObject
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CollectableObject()

  PROPERTIES:
    string CollectableName { get; }
    int ObjectCollectableKind { get; }
    int Weight { get; }

--------------------------------------------------------------------------------

[CLASS] CollectionVector
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.CollectionVector
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CollectionVector(int initialSize)
    new CollectionVector()

  METHODS:
    void Add(CollectionVector vector)
    void Add(CollectableObject value)
    void ApplyOemFilter(string general, bool zeroBased)
    void ApplyOemFilter(string general)
    void Cleanup(bool zeroBased)
    void Cleanup()
    int Count()
    void DestroyControls(bool zeroBased)
    void DestroyControls()
    CategoryCollectable FindCategory(string name, int categoryId, int parentCategoryId, bool zeroBased)
    CategoryCollectable FindCategory(string name, int categoryId, int parentCategoryId)
    CategoryCollectable FindCategory(string name, int categoryId)
    CategoryCollectable FindCategory(string name)
    void InsertAt(int index, CollectableObject value)
    CollectableObject Item(int index)
    void NestCategories(bool zeroBased)
    void NestCategories()
    void Print(string logFile, bool append)
    void Print(string logFile)
    void RemoveAll()
    void RemoveAt(int index)
    void RemoveDynamicCollectables()
    void SetAt(int index, CollectableObject value)
    void SetCategoryWeight(string categoryName, int weight, bool zeroBased)
    void SetCategoryWeight(string categoryName, int weight)
    void Sort(int begin, int finish)
    void SortVectorByWeights(int begin, int finish)

--------------------------------------------------------------------------------

[CLASS] ObjectPropertyManagerProperties
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.ObjectPropertyManagerProperties

  METHODS:
    static bool ClassIsVetoed(RXClass cls, bool isQPContext)
    static bool ConvertBlkrefToXref(ObjectId objId)
    static int GetCategoryId(PropertyCollectable prop)
    static string GetDisplayName(DBObject obj)
    static string GetDisplayName(ObjectId objId)
    static string GetDisplayNameGuts(IUnknown* pUnk)
    static bool GetMultiplePropertyGuids(RXClass cls, StringCollection GUIDs)
    static Dictionary<string, StringCollection> GetNonEntityObjectTypes(bool isTooltipsContext)
    static CollectionVector GetProperties(DBObject obj, bool bFilterable, bool bQPContext)
    static CollectionVector GetProperties(ObjectId objId, bool bFilterable, bool bQPContext)
    static CollectionVector GetPropertiesForSubtype(DBObject obj, bool bFilterable, string sGUID, bool bQPContext)
    static CollectionVector GetPropertiesForSubtype(ObjectId objId, bool bFilterable, string sGUID, bool bQPContext)
    static CollectionVector GetPropertiesForSubtypeGuts(IUnknown* pUnk, bool bFilterable, string sGUID, bool bQPContext)
    static CollectionVector GetPropertiesGuts(IUnknown* pUnk, bool bFilterable, bool bQPContext)
    static bool GetSubtypes(RXClass cls, StringCollection GUIDs, StringCollection displayNames)
    static ObjectId GiveClassChanceToCreate(RXClass cls, Database db, ref Entity pEntOut, string sGUID)
    static bool IsDynamicProperty(PropertyCollectable prop, StringCollection GUIDs)

--------------------------------------------------------------------------------

[CLASS] ObjectPropertyManagerPropertyUtility
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.ObjectPropertyManagerPropertyUtility

  METHODS:
    static IntPtr GetIUnknownFromObject(DBObject obj, bool bAttachToObject)
    static IntPtr GetIUnknownFromObjectId(ObjectId objId)
    static ITypeInfo GetTypeInfo(object obj, int lcid)
    static bool ObjectSupportsNonDBResidency(DBObject obj)

--------------------------------------------------------------------------------

[ENUM] PropertyCategoryId
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.PropertyCategoryId
Inherits: Enum

  Values:
    - Attributes = 2941
    - Geometry = -25
    - Table = -24
    - Tolerances = -23
    - LinesArrows = -22
    - Fit = -21
    - AltUnits = -20
    - PrimaryUnits = -19
    - ControlPoints = -18
    - ImageAdjust = -17
    - Mesh = -16
    - DataPoints = -15
    - Pattern = -14
    - Mass = -13
    - General = -12
    - Dde = -11
    - Scale = -10
    - Text = -9
    - List = -8
    - Data = -7
    - Behavior = -6
    - Appearance = -5
    - Position = -4
    - Font = -3
    - Misc = -2
    - Nil = -1

--------------------------------------------------------------------------------

[CLASS] PropertyCollectable
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.PropertyCollectable
Inherits: CollectableObject

  CONSTRUCTORS:
    new PropertyCollectable()

  PROPERTIES:
    string CollectableName { get; }
    string Description { get; }
    object DISP { get; }
    int dispid { get; }
    int MemberId { get; }
    string Name { get; }
    int Visible { get; }
    int weight { get; set; }

  METHODS:
    bool GetValue(IntPtr unknown, ref object value)
    bool IsEqual(PropertyCollectable propertyCollectable, CHECK_LEVEL check, ref uint flags)
    bool Skip()
    void Sync(PropertyCollectable propertyCollectable, uint inequalityFlags)
    void Sync(PropertyCollectable propertyCollectable)

--------------------------------------------------------------------------------

[ENUM] PropertyInequalityFlags
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.PropertyInequalityFlags
Inherits: Enum

  Values:
    - Name = 1
    - PropertyControl = 2
    - IntegralHeight = 4
    - FullView = 8
    - TextColor = 16
    - Weight = 32
    - Parent = 64
    - Enum = 128
    - Type = 256
    - Disabled = 512
    - Ellipses = 1024
    - ClsidPropPage = 2048
    - Visible = 4096

--------------------------------------------------------------------------------

[CLASS] PropertyInspectorEventArgs
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.PropertyInspectorEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new PropertyInspectorEventArgs()

  PROPERTIES:
    int OPMModeFlag { get; }
    IntPtr ParentWindow { get; }
    object Property { get; }
    object Value { get; }

--------------------------------------------------------------------------------

[CLASS] PropertyInspectorEventHandler
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.PropertyInspectorEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PropertyInspectorEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PropertyInspectorEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PropertyInspectorEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PropertyInspectorEventManager
Full Name: Autodesk.AutoCAD.Internal.PropertyInspector.PropertyInspectorEventManager

  METHODS:
    static PropertyInspectorEventManager Instance()

  EVENTS:
    event PropertyInspectorEventHandler propertyDialogDismissed
    event PropertyInspectorEventHandler propertyDialogInvoked
    event PropertyInspectorEventHandler propertyChanged
    event PropertyInspectorEventHandler propertyWillChange

--------------------------------------------------------------------------------

