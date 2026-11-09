# Autodesk.AutoCAD.Runtime

NAMESPACE: Autodesk.AutoCAD.Runtime
--------------------------------------------------------------------------------

[CLASS] Registry
Full Name: Autodesk.AutoCAD.Runtime.Registry

  PROPERTIES:
    static RegistryKey ClassesRoot { get; }
    static RegistryKey CurrentConfig { get; }
    static RegistryKey CurrentUser { get; }
    static RegistryKey LocalMachine { get; }

--------------------------------------------------------------------------------

[CLASS] RegistryKey
Full Name: Autodesk.AutoCAD.Runtime.RegistryKey
Implements: IDisposable

  CONSTRUCTORS:
    new RegistryKey(RegistryKey hKey)

  PROPERTIES:
    int SubKeyCount { get; }
    int ValueCount { get; }

  METHODS:
    void Close()
    RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, RegistrySecurity registrySecurity)
    RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistrySecurity registrySecurity)
    RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
    RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
    RegistryKey CreateSubKey(string subkey)
    void DeleteSubKey(string subkey)
    void DeleteSubKeyTree(string subkey)
    void DeleteValue(string name)
    void Dispose()
    static RegistryKey GetBaseKey(RegistryHive hKey)
    string[] GetSubKeyNames()
    object GetValue(string name, object defaultValue, RegistryValueOptions options)
    object GetValue(string name, object defaultValue)
    object GetValue(string name)
    RegistryValueKind GetValueKind(string name)
    string[] GetValueNames()
    static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
    RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
    RegistryKey OpenSubKey(string name, bool writable)
    RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
    RegistryKey OpenSubKey(string name)
    void SetValue(string name, object value, RegistryValueKind kind)
    void SetValue(string name, object value)

--------------------------------------------------------------------------------

[CLASS] RuntimeSystem
Full Name: Autodesk.AutoCAD.Runtime.RuntimeSystem

  METHODS:
    static void Initialize(HostApplicationServices hostServices, int localId)
    static int Main(IntPtr arg, int argLength)
    static void Terminate()

--------------------------------------------------------------------------------

[CLASS] RXClass
Full Name: Autodesk.AutoCAD.Runtime.RXClass
Inherits: RXObject

  PROPERTIES:
    string AppName { get; }
    Version ClassVersion { get; }
    string DxfName { get; }
    RXClass MyParent { get; }
    string Name { get; }
    int ProxyFlags { get; }

  METHODS:
    IntPtr AddX(RXClass runtimeClass, RXObject protocolExtension)
    RXObject Create()
    IntPtr DelX(RXClass runtimeClass)
    Type GetRuntimeType()
    IntPtr GetX(RXClass runtimeClass)
    bool IsDerivedFrom(RXClass runtimeClass)
    IntPtr QueryX(RXClass runtimeClass)

  EVENTS:
    event EntityAlignmentEventHandler EntityAlignment

--------------------------------------------------------------------------------

[ABSTRACT CLASS] RXObject
Full Name: Autodesk.AutoCAD.Runtime.RXObject
Inherits: DisposableWrapper
Implements: ICloneable

  METHODS:
    object Clone()
    int CompareTo(object obj)
    void CopyFrom(RXObject source)
    static RXObject Create(IntPtr unmanagedPointer, bool autoDelete)
    static RXClass GetClass(Type type)
    RXClass GetRXClass()
    IntPtr QueryX(RXClass protocolClass)
    IntPtr X(RXClass protocolClass)

--------------------------------------------------------------------------------

[CLASS] SecuredApplicationAttribute
Full Name: Autodesk.AutoCAD.Runtime.SecuredApplicationAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new SecuredApplicationAttribute(string license, string ourSignature, string clientSignature, string clientPublicKey)

  PROPERTIES:
    string ClientPublicKey { get; }
    string ClientSignature { get; }
    string License { get; }
    string OurSignature { get; }

--------------------------------------------------------------------------------

[CLASS] ShowExceptionDialogHandler
Full Name: Autodesk.AutoCAD.Runtime.ShowExceptionDialogHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ShowExceptionDialogHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(Exception e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(Exception e)

--------------------------------------------------------------------------------

[CLASS] SynchronizationContext
Full Name: Autodesk.AutoCAD.Runtime.SynchronizationContext
Inherits: SynchronizationContext

  CONSTRUCTORS:
    new SynchronizationContext()

  METHODS:
    SynchronizationContext CreateCopy()
    void OperationCompleted()
    void OperationStarted()
    void Post(SendOrPostCallback d, object state)
    void Send(SendOrPostCallback d, object state)
    int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)

--------------------------------------------------------------------------------

[CLASS] SystemObjects
Full Name: Autodesk.AutoCAD.Runtime.SystemObjects

  PROPERTIES:
    static Dictionary ClassDictionary { get; }
    static DynamicLinker DynamicLinker { get; }
    static Dictionary ServiceDictionary { get; }
    static Variables Variables { get; }

--------------------------------------------------------------------------------

[CLASS] SystemVariableEnumerator
Full Name: Autodesk.AutoCAD.Runtime.SystemVariableEnumerator
Inherits: DisposableWrapper
Implements: IEnumerator

  CONSTRUCTORS:
    new SystemVariableEnumerator()

  PROPERTIES:
    Variable Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] UnhandledExceptionFilter
Full Name: Autodesk.AutoCAD.Runtime.UnhandledExceptionFilter

  METHODS:
    static void CerOrShowExceptionDialog(Exception e)

--------------------------------------------------------------------------------

[CLASS] Utilities
Full Name: Autodesk.AutoCAD.Runtime.Utilities

  CONSTRUCTORS:
    new Utilities()

  METHODS:
    static string GetErrorStatus(ErrorStatus eStatus)
    static string GetRedirectedRegistryValue(string subPath, string propertyName)
    static IDictionary<string, string> GetRedirectedRegistryValueMap(string subPath)
    static string GetReservedString(ReservedStringEnumType reservedType, bool bGetLocalized)
    static string TranslateReservedString(string pGlobalName)

--------------------------------------------------------------------------------

[CLASS] Variable
Full Name: Autodesk.AutoCAD.Runtime.Variable
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Variable(IntPtr pImp, bool autoDelete)

  PROPERTIES:
    bool IsLocked { get; }
    bool IsReadOnly { get; set; }
    string Name { get; }
    short PrimaryType { get; }
    RangeInfo Range { get; }
    SecondaryTypeInfo SecondaryType { get; }
    StorageType Storage { get; }
    short TypeFlags { get; }
    object Value { get; set; }

  METHODS:
    void Reset()

  EVENTS:
    event VariableChangingEventHandler Changing
    event VariableChangedEventHandler Changed

--------------------------------------------------------------------------------

[CLASS] VariableChangedEventArgs
Full Name: Autodesk.AutoCAD.Runtime.VariableChangedEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    object NewValue { get; }
    object OldValue { get; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] VariableChangedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.VariableChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new VariableChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, VariableChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, VariableChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] VariableChangingEventArgs
Full Name: Autodesk.AutoCAD.Runtime.VariableChangingEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    bool IsResetting { get; }
    object NewValue { get; set; }
    object OldValue { get; }

  METHODS:
    void Dispose()
    void Veto(string reason)

--------------------------------------------------------------------------------

[CLASS] VariableChangingEventHandler
Full Name: Autodesk.AutoCAD.Runtime.VariableChangingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new VariableChangingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, VariableChangingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, VariableChangingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] Variables
Full Name: Autodesk.AutoCAD.Runtime.Variables
Inherits: DisposableWrapper
Implements: IEnumerable<Variable>, IEnumerable

  PROPERTIES:
    Variable Item { get; }

  METHODS:
    string[] GetAllNames()
    IEnumerator<Variable> GetEnumerator()
    void Reset(StorageType filter)

  EVENTS:
    event VariableChangingEventHandler Changing
    event VariableChangedEventHandler Changed

--------------------------------------------------------------------------------

[CLASS] WrapperAttribute
Full Name: Autodesk.AutoCAD.Runtime.WrapperAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new WrapperAttribute(string wrappedClass)

  PROPERTIES:
    string WrappedClass { get; }

--------------------------------------------------------------------------------

