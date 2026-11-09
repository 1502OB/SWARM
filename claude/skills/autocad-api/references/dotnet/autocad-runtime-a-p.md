# Autodesk.AutoCAD.Runtime

NAMESPACE: Autodesk.AutoCAD.Runtime
--------------------------------------------------------------------------------

[ENUM] AngularUnitFormat
Full Name: Autodesk.AutoCAD.Runtime.AngularUnitFormat
Inherits: Enum

  Values:
    - Degrees = 0
    - DegreesMinutesSeconds = 1
    - Grads = 2
    - Radians = 3
    - Surveyor = 4
    - Current = -1

--------------------------------------------------------------------------------

[CLASS] CommandClassAttribute
Full Name: Autodesk.AutoCAD.Runtime.CommandClassAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new CommandClassAttribute(Type name)

  PROPERTIES:
    Type Type { get; }

--------------------------------------------------------------------------------

[ENUM] CommandFlags
Full Name: Autodesk.AutoCAD.Runtime.CommandFlags
Inherits: Enum

  Values:
    - Modal = 0
    - Transparent = 1
    - UsePickSet = 2
    - Redraw = 4
    - NoPerspective = 8
    - NoMultiple = 16
    - NoTileMode = 32
    - NoPaperSpace = 64
    - NoOem = 256
    - Undefined = 512
    - InProgress = 1024
    - Defun = 2048
    - NoNewStack = 65536
    - NoInternalLock = 131072
    - DocReadLock = 524288
    - DocExclusiveLock = 1048576
    - Session = 2097152
    - Interruptible = 4194304
    - NoHistory = 8388608
    - NoUndoMarker = 16777216
    - NoBlockEditor = 33554432
    - NoActionRecording = 67108864
    - ActionMacro = 134217728
    - NoInferConstraint = 1073741824
    - TempShowDynDimension = -2147483648

--------------------------------------------------------------------------------

[CLASS] CommandMethodAttribute
Full Name: Autodesk.AutoCAD.Runtime.CommandMethodAttribute
Inherits: Attribute
Implements: ICommandLineCallable

  CONSTRUCTORS:
    new CommandMethodAttribute(string globalName)
    new CommandMethodAttribute(string globalName, CommandFlags flags)
    new CommandMethodAttribute(string groupName, string globalName, CommandFlags flags)
    new CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags)
    new CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType)
    new CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, string helpTopic)
    new CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType, string helpFileName, string helpTopic)

  PROPERTIES:
    Type ContextMenuExtensionType { get; }
    CommandFlags Flags { get; }
    string GlobalName { get; }
    string GroupName { get; }
    string HelpFileName { get; }
    string HelpTopic { get; }
    string LocalizedNameId { get; }

--------------------------------------------------------------------------------

[CLASS] Converter
Full Name: Autodesk.AutoCAD.Runtime.Converter

  METHODS:
    static string AngleToString(double value, AngularUnitFormat units, int precision)
    static string AngleToString(double value)
    static string DistanceToString(double value, DistanceUnitFormat units, int precision)
    static string DistanceToString(double value)
    static string RawAngleToString(double value, AngularUnitFormat units, int precision)
    static string RawAngleToString(double value)
    static double StringToAngle(string value, AngularUnitFormat units)
    static double StringToAngle(string value)
    static double StringToDistance(string value, DistanceUnitFormat units)
    static double StringToDistance(string value)
    static double StringToRawAngle(string value, AngularUnitFormat units)
    static double StringToRawAngle(string value)

--------------------------------------------------------------------------------

[CLASS] DeferredAssemblyLoadEventArgs
Full Name: Autodesk.AutoCAD.Runtime.DeferredAssemblyLoadEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new DeferredAssemblyLoadEventArgs(Assembly a, bool mayHaveExtensionApplication, bool mayHaveCommands)

  PROPERTIES:
    Assembly LoadedAssembly { get; }
    bool MayHaveCommands { get; }
    bool MayHaveExtensionApplication { get; }

--------------------------------------------------------------------------------

[CLASS] DeferredAssemblyLoadEventHandler
Full Name: Autodesk.AutoCAD.Runtime.DeferredAssemblyLoadEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DeferredAssemblyLoadEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DeferredAssemblyLoadEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DeferredAssemblyLoadEventArgs e)

--------------------------------------------------------------------------------

[CLASS] Dictionary
Full Name: Autodesk.AutoCAD.Runtime.Dictionary
Inherits: RXObject
Implements: IDictionary, ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    bool DeletesObjects { get; }
    bool IsCaseSensitive { get; }
    bool IsSorted { get; }
    RXObject Item { get; set; }

  METHODS:
    RXObject At(int id)
    RXObject At(string key)
    void AtKeyAndIdPut(string newKey, int id, RXObject value)
    RXObject AtPut(int id, RXObject value)
    RXObject AtPut(string key, RXObject value, out int retId)
    RXObject AtPut(string key, RXObject value)
    bool Contains(int id)
    bool Contains(string key)
    void CopyTo(DictionaryEntry[] array, int index)
    IDictionaryEnumerator GetEnumerator()
    int IdAt(string key)
    string KeyAt(int id)
    RXObject Remove(int id)
    RXObject Remove(string key)
    void ResetKey(int id, string newKey)
    void ResetKey(string oldKey, string newKey)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DisposableWrapper
Full Name: Autodesk.AutoCAD.Runtime.DisposableWrapper
Inherits: MarshalByRefObject
Implements: IDisposable

  PROPERTIES:
    bool AutoDelete { get; set; }
    bool IsDisposed { get; }
    IntPtr UnmanagedObject { get; }

  METHODS:
    static DisposableWrapper Create(Type type, IntPtr unmanagedPointer, bool autoDelete)
    void Dispose()
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

[ENUM] DistanceUnitFormat
Full Name: Autodesk.AutoCAD.Runtime.DistanceUnitFormat
Inherits: Enum

  Values:
    - Scientific = 1
    - Decimal = 2
    - Engineering = 3
    - Architectural = 4
    - Fractional = 5
    - Current = -1

--------------------------------------------------------------------------------

[CLASS] DynamicLinker
Full Name: Autodesk.AutoCAD.Runtime.DynamicLinker
Inherits: RXObject

  PROPERTIES:
    int ProductLcid { get; }

  METHODS:
    StringCollection GetLoadedModules()
    bool IsAppBusy(string moduleName)
    bool IsApplicationLocked(string moduleName)
    bool IsAppMdiAware(string moduleName)
    bool IsModuleLoaded(string fileName)
    void LoadApp(string appName, bool printIt, bool asCmd)
    void LoadModule(string fileName, bool printIt, bool asCmd)
    void SetAppBusy(string moduleName, bool busyFlag)
    void UnloadApp(string appName, bool asCmd)
    void UnloadModule(string fileName, bool asCmd)

  EVENTS:
    event ModuleUnloadAbortedEventHandler ModuleUnloadAborted
    event ModuleUnloadedEventHandler ModuleUnloaded
    event ModuleUnloadingEventHandler ModuleUnloading
    event ModuleLoadAbortedEventHandler ModuleLoadAborted
    event ModuleLoadedEventHandler ModuleLoaded
    event ModuleLoadingEventHandler ModuleLoading

--------------------------------------------------------------------------------

[CLASS] DynamicLinkerEventArgs
Full Name: Autodesk.AutoCAD.Runtime.DynamicLinkerEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new DynamicLinkerEventArgs(string fileName)

  PROPERTIES:
    string FileName { get; }

--------------------------------------------------------------------------------

[CLASS] EnvVarExistsConditionAttribute
Full Name: Autodesk.AutoCAD.Runtime.EnvVarExistsConditionAttribute
Inherits: Attribute
Implements: ICondition

  CONSTRUCTORS:
    new EnvVarExistsConditionAttribute(string varName, string varValue)

  METHODS:
    bool Evaluate()

--------------------------------------------------------------------------------

[ENUM] ErrorStatus
Full Name: Autodesk.AutoCAD.Runtime.ErrorStatus
Inherits: Enum

  Values:
    - OK = 0
    - NotImplementedYet = 1
    - NotApplicable = 2
    - InvalidInput = 3
    - AmbiguousInput = 4
    - AmbiguousOutput = 5
    - OutOfMemory = 6
    - BufferTooSmall = 7
    - InvalidOpenState = 8
    - EntityInInactiveLayout = 9
    - HandleExists = 10
    - NullHandle = 11
    - BrokenHandle = 12
    - UnknownHandle = 13
    - HandleInUse = 14
    - NullObjectPointer = 15
    - NullObjectId = 16
    - NullBlockName = 17
    - ContainerNotEmpty = 18
    - NullEntityPointer = 20
    - IllegalEntityType = 21
    - KeyNotFound = 22
    - DuplicateKey = 23
    - InvalidIndex = 24
    - DuplicateIndex = 25
    - AlreadyInDB = 26
    - OutOfDisk = 27
    - DeletedEntry = 28
    - NegativeValueNotAllowed = 29
    - InvalidExtents = 30
    - InvalidAdsName = 31
    - InvalidSymbolTableName = 32
    - InvalidKey = 33
    - WrongObjectType = 34
    - WrongDatabase = 35
    - ObjectToBeDeleted = 36
    - InvalidDwgVersion = 37
    - AnonymousEntry = 38
    - IllegalReplacement = 39
    - EndOfObject = 40
    - EndOfFile = 41
    - IsReading = 42
    - IsWriting = 43
    - NotOpenForRead = 44
    - NotOpenForWrite = 45
    - NotThatKindOfClass = 46
    - InvalidBlockName = 47
    - MissingDxfField = 48
    - DuplicateDxfField = 49
    - InvalidDxfCode = 50
    - InvalidResultBuffer = 51
    - BadDxfSequence = 52
    - FilerError = 53
    - VertexAfterFace = 54
    - InvalidFaceVertexIndex = 55
    - InvalidMeshVertexIndex = 56
    - OtherObjectsBusy = 57
    - MustFirstAddBlockToDB = 58
    - CannotNestBlockDefinitions = 59
    - DwgRecoveredOK = 60
    - DwgNotRecoverable = 61
    - DxfPartiallyRead = 62
    - DxfReadAborted = 63
    - DxbPartiallyRead = 64
    - DwgCrcDoesNotMatch = 65
    - DwgSentinelDoesNotMatch = 66
    - DwgObjectImproperlyRead = 67
    - NoInputFiler = 68
    - DwgNeedsAFullSave = 69
    - DxbReadAborted = 70
    - FileLockedByAutoCAD = 71
    - FileAccessErr = 72
    - FileSystemErr = 73
    - FileInternalErr = 74
    - FileTooManyOpen = 75
    - FileNotFound = 76
    - DwkLockFileFound = 77
    - WasErased = 80
    - PermanentlyErased = 81
    - WasOpenForRead = 82
    - WasOpenForWrite = 83
    - WasOpenForUndo = 84
    - WasNotifying = 85
    - WasOpenForNotify = 86
    - OnLockedLayer = 87
    - MustOpenThruOwner = 88
    - SubentitiesStillOpen = 89
    - AtMaxReaders = 90
    - IsWriteProtected = 91
    - IsXRefObject = 92
    - NotAnEntity = 93
    - HadMultipleReaders = 94
    - DuplicateRecordName = 95
    - XRefDependent = 96
    - SelfReference = 97
    - MissingSymbolTable = 98
    - MissingSymbolTableRecord = 99
    - WasNotOpenForWrite = 100
    - CloseWasNotifying = 101
    - CloseModifyAborted = 102
    - ClosePartialFailure = 103
    - CloseFailObjectDamaged = 104
    - CannotBeErasedByCaller = 105
    - CannotBeResurrected = 106
    - WasNotErased = 107
    - InsertAfter = 110
    - FixedAllErrors = 120
    - LeftErrorsUnfixed = 122
    - UnrecoverableErrors = 123
    - NoDatabase = 124
    - ExternalDataSizeExceeded = 125
    - RegisteredApplicationIdNotFound = 126
    - RepeatEntity = 127
    - RecordNotInTable = 128
    - IteratorDone = 129
    - NullIterator = 130
    - NotInBlock = 131
    - OwnerNotInDatabase = 132
    - OwnerNotOpenForRead = 133
    - OwnerNotOpenForWrite = 134
    - ExplodeBeforeTransform = 135
    - CannotScaleNonUniformly = 136
    - NotInDatabase = 137
    - NotCurrentDatabase = 138
    - IsAnEntity = 139
    - CannotChangeActiveViewport = 140
    - NotInPaperspace = 141
    - CommandWasInProgress = 142
    - GeneralModelingFailure = 150
    - OutOfRange = 151
    - NonCoplanarGeometry = 152
    - DegenerateGeometry = 153
    - InvalidAxis = 154
    - PointNotOnEntity = 155
    - SingularPoint = 156
    - InvalidOffset = 157
    - NonPlanarEntity = 158
    - CannotExplodeEntity = 159
    - StringTooLong = 160
    - InvalidSymTableFlag = 161
    - UndefinedLineType = 162
    - InvalidTextStyle = 163
    - TooFewLineTypeElements = 164
    - TooManyLineTypeElements = 165
    - ExcessiveItemCount = 166
    - IgnoredLinetypeRedefinition = 167
    - BadUcs = 168
    - BadPaperspaceView = 169
    - SomeInputDataLeftUnread = 170
    - NoInternalSpace = 171
    - InvalidDimStyle = 172
    - InvalidLayer = 173
    - UserBreak = 180
    - DwgNeedsRecovery = 190
    - DeleteEntity = 191
    - InvalidFix = 192
    - FiniteStateMachineError = 193
    - BadLayerName = 200
    - LayerGroupCodeMissing = 201
    - BadColorIndex = 202
    - BadLinetypeName = 203
    - BadLinetypeScale = 204
    - BadVisibilityValue = 205
    - ProperClassSeparatorExpected = 206
    - BadLineWeightValue = 207
    - BadColor = 208
    - PagerError = 210
    - OutOfPagerMemory = 211
    - PagerWriteError = 212
    - WasNotForwarding = 213
    - InvalidIdMap = 220
    - InvalidOwnerObject = 221
    - OwnerNotSet = 222
    - WrongSubentityType = 230
    - TooManyVertices = 231
    - TooFewVertices = 232
    - NoActiveTransactions = 250
    - NotTopTransaction = 251
    - TransactionOpenWhileCommandEnded = 252
    - InProcessOfCommitting = 253
    - NotNewlyCreated = 254
    - LongTransReferenceError = 255
    - NoWorkSet = 256
    - AlreadyInGroup = 260
    - NotInGroup = 261
    - InvalidInterfaceId = 290
    - InvalidNormal = 291
    - InvalidStyle = 292
    - CannotRestoreFromAcisFile = 300
    - MakeMeProxy = 301
    - NlsFileNotAvailable = 302
    - NotAllowedForThisProxy = 303
    - NotSupportedInDwgApi = 310
    - PolyWidthLost = 311
    - NullExtents = 312
    - BadDwgHeader = 314
    - LockViolation = 320
    - LockConflict = 321
    - DatabaseObjectsOpen = 322
    - LockChangeInProgress = 323
    - Vetoed = 325
    - NoDocument = 330
    - NotFromThisDocument = 331
    - LispActive = 332
    - TargetDocNotQuiescent = 333
    - DocumentSwitchDisabled = 334
    - InvalidContext = 335
    - CreateFailed = 337
    - CreateInvalidName = 338
    - SetFailed = 340
    - DelDoesNotExist = 342
    - DelIsModelSpace = 343
    - DelLastLayout = 344
    - DelUnableToSetCurrent = 345
    - DelUnableToFind = 346
    - RenameDoesNotExist = 348
    - RenameIsModelSpace = 349
    - RenameInvalidLayoutName = 350
    - RenameLayoutAlreadyExists = 351
    - RenameInvalidName = 352
    - CopyDoesNotExist = 354
    - CopyIsModelSpace = 355
    - CopyFailed = 356
    - CopyInvalidName = 357
    - CopyNameExists = 358
    - ProfileDoesNotExist = 359
    - InvalidFileExtension = 360
    - InvalidProfileName = 361
    - FileExists = 362
    - ProfileIsInUse = 363
    - CantOpenFile = 364
    - NoFileName = 365
    - RegistryAccessError = 366
    - RegistryCreateError = 367
    - BadDxfFile = 368
    - UnknownDxfFileFormat = 369
    - MissingDxfSection = 370
    - InvalidDxfSectionName = 371
    - NotDxfHeaderGroupCode = 372
    - UndefinedDxfGroupCode = 373
    - NotInitializedYet = 374
    - InvalidDxf2dPoint = 375
    - InvalidDxf3dPoint = 376
    - BadlyNestedAppData = 378
    - IncompleteBlockDefinition = 379
    - IncompleteComplexObject = 380
    - BlockDefInEntitySection = 381
    - NoBlockBegin = 382
    - DuplicateLayerName = 383
    - BadPlotStyleName = 384
    - DuplicateBlockName = 385
    - BadPlotStyleType = 386
    - BadPlotStyleNameHandle = 387
    - UndefineShapeName = 388
    - DuplicateBlockDefinition = 389
    - MissingBlockName = 390
    - BinaryDataSizeExceeded = 391
    - ObjectIsReferenced = 392
    - NoThumbnailBitmap = 393
    - GuidNoAddress = 394
    - MustBe0to2 = 395
    - MustBe0to3 = 396
    - MustBe0to4 = 397
    - MustBe0to5 = 398
    - MustBe0to8 = 399
    - MustBe1to8 = 400
    - MustBe1to15 = 401
    - MustBePositive = 402
    - MustBeNonNegative = 403
    - MustBeNonZero = 404
    - MustBe1to6 = 405
    - NoPlotStyleTranslationTable = 406
    - PlotStyleInColorDependentMode = 407
    - MaxLayouts = 408
    - NoClassId = 409
    - UndoOperationNotAvailable = 410
    - UndoNoGroupBegin = 411
    - HatchTooDense = 420
    - OpenFileCancelled = 430
    - NotHandled = 431
    - MakeMeProxyAndResurrect = 432
    - FileSharingViolation = 433
    - UnsupportedFileFormat = 434
    - ObsoleteFileFormat = 435
    - FileMissingSections = 436
    - RepeatedDwgRead = 437
    - WrongCellType = 440
    - CannotChangeColumnType = 441
    - RowsMustMatchColumns = 442
    - DwgShareDemandLoad = 500
    - DwgShareReadAccess = 501
    - DwgShareWriteAccess = 502
    - LoadFailed = 503
    - DeviceNotFound = 504
    - NoCurrentConfig = 505
    - NullPtr = 506
    - NoLayout = 507
    - IncompatiblePlotSettings = 508
    - NonePlotDevice = 509
    - NoMatchingMedia = 510
    - InvalidView = 511
    - InvalidWindowArea = 512
    - InvalidPlotArea = 513
    - CustomSizeNotPossible = 514
    - PageCancelled = 515
    - PlotCancelled = 516
    - InvalidEngineState = 517
    - PlotAlreadyStarted = 518
    - NoErrorHandler = 519
    - InvalidPlotInfo = 520
    - NumberOfCopiesNotSupported = 521
    - LayoutNotCurrent = 522
    - GraphicsNotGenerated = 523
    - CannotPlotToFile = 524
    - MustPlotToFile = 525
    - NotMultiPageCapable = 526
    - BackgroundPlotInProgress = 527
    - SubSelectionSetEmpty = 550
    - NoIntersections = 551
    - EmbeddedIntersections = 552
    - InvalidObjectId = 600
    - InvalidXrefObjectId = 601
    - NoViewAssociation = 602
    - NoLabelBlock = 603
    - UnableToSetViewAssociation = 604
    - UnableToGetViewAssociation = 605
    - UnableToSetLabelBlock = 606
    - UnableToGetLabelBlock = 607
    - UnableToRemoveAssociation = 608
    - UnableToSyncModelView = 609
    - DataLinkAdapterNotFound = 650
    - DataLinkInvalidAdapterId = 651
    - DataLinkNotFound = 652
    - DataLinkBadConnectionString = 653
    - DataLinkNotUpdatedYet = 654
    - DataLinkSourceNotFound = 655
    - DataLinkConnectionFailed = 656
    - DataLinkSourceIsWriteProtected = 658
    - DataLinkOtherError = 660
    - SecInitializationFailure = 1001
    - SecErrorReadingFile = 1002
    - SecErrorWritingFile = 1003
    - SecInvalidDigitalId = 1101
    - SecErrorGeneratingTimestamp = 1102
    - SecErrorComputingSignature = 1103
    - SecErrorWritingSignature = 1104
    - SecErrorEncryptingData = 1201
    - SecErrorCipherNotSupported = 1202
    - SecErrorDecryptingData = 1203
    - InternetOK = 20000
    - InternetOK = 20000
    - InternetInCache = 20001
    - InternetFileNotFound = 20002
    - InternetBadPath = 20003
    - InternetTooManyOpenFiles = 20004
    - InternetFileAccessDenied = 20005
    - InternetInvalidFileHandle = 20006
    - InternetDirectoryFull = 20007
    - InternetHardwareError = 20008
    - InternetSharingViolation = 20009
    - InternetDiskFull = 20010
    - InternetFileGenericError = 20011
    - InternetValidUrl = 20012
    - InternetNotAnUrl = 20013
    - InternetNoWinInternet = 20014
    - InternetOldWinInternet = 20015
    - InternetNoInternetSupport = 20016
    - InternetNotImplemented = 20017
    - InternetProtocolNotSupported = 20018
    - InternetCreateInternetSessionFailed = 20019
    - InternetInternetSessionConnectFailed = 20020
    - InternetInternetSessionOpenFailed = 20021
    - InternetInvalidAccessType = 20022
    - InternetFileOpenFailed = 20023
    - InternetHttpOpenRequestFailed = 20024
    - InternetUserCancelledTransfer = 20025
    - InternetHttpBadRequest = 20026
    - InternetHttpAccessDenied = 20027
    - InternetHttpPaymentRequired = 20028
    - InternetHttpRequestForbidden = 20029
    - InternetHttpObjectNotFound = 20030
    - InternetHttpBadMethod = 20031
    - InternetHttpNoAcceptableResponse = 20032
    - InternetHttpProxyAuthorizationRequired = 20033
    - InternetHttpTimedOut = 20034
    - InternetHttpConflict = 20035
    - InternetHttpResourceGone = 20036
    - InternetHttpLengthRequired = 20037
    - InternetHttpPreconditionFailure = 20038
    - InternetHttpRequestTooLarge = 20039
    - InternetHttpUriTooLong = 20040
    - InternetHttpUnsupportedMedia = 20041
    - InternetHttpServerError = 20042
    - InternetHttpNotSupported = 20043
    - InternetHttpBadGateway = 20044
    - InternetHttpServiceUnavailable = 20045
    - InternetHttpGatewayTimeout = 20046
    - InternetHttpVersionNotSupported = 20047
    - InternetInternetError = 20048
    - InternetGenericException = 20049
    - InternetUnknownError = 20050
    - AlreadyActive = 20051
    - AlreadyInactive = 20052

--------------------------------------------------------------------------------

[CLASS] Exception
Full Name: Autodesk.AutoCAD.Runtime.Exception
Inherits: Exception

  CONSTRUCTORS:
    new Exception(ErrorStatus es, string message, Exception innerException)
    new Exception(ErrorStatus es, string message)
    new Exception(ErrorStatus es)
    new Exception()

  PROPERTIES:
    ErrorStatus ErrorStatus { get; set; }

  METHODS:
    void GetObjectData(SerializationInfo info, StreamingContext context)

--------------------------------------------------------------------------------

[CLASS] ExtensionApplicationAttribute
Full Name: Autodesk.AutoCAD.Runtime.ExtensionApplicationAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new ExtensionApplicationAttribute(Type entryPointType)

  PROPERTIES:
    Type Type { get; }

--------------------------------------------------------------------------------

[CLASS] ExtensionLoader
Full Name: Autodesk.AutoCAD.Runtime.ExtensionLoader
Inherits: MarshalByRefObject

  METHODS:
    static bool IsLoaded(string fileName)
    static Assembly Load(string fileName)

  EVENTS:
    event DeferredAssemblyLoadEventHandler DeferredAssemblyLoad

--------------------------------------------------------------------------------

[ENUM] ExtractionType
Full Name: Autodesk.AutoCAD.Runtime.ExtractionType
Inherits: Enum

  Values:
    - OutLine = 0
    - AllLine = 1

--------------------------------------------------------------------------------

[CLASS] ExtractOption
Full Name: Autodesk.AutoCAD.Runtime.ExtractOption

  CONSTRUCTORS:
    new ExtractOption()

--------------------------------------------------------------------------------

[CLASS] Factory
Full Name: Autodesk.AutoCAD.Runtime.Factory

  CONSTRUCTORS:
    new Factory()

  METHODS:
    static object Create(Type type)

--------------------------------------------------------------------------------

[CLASS] FileExistsConditionAttribute
Full Name: Autodesk.AutoCAD.Runtime.FileExistsConditionAttribute
Inherits: Attribute
Implements: ICondition

  CONSTRUCTORS:
    new FileExistsConditionAttribute(string fileName)

  METHODS:
    bool Evaluate()

--------------------------------------------------------------------------------

[CLASS] FileHasClosedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.FileHasClosedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new FileHasClosedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(FileOperationsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(FileOperationsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] FileOpenAbortedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.FileOpenAbortedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new FileOpenAbortedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(FileOperationsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(FileOperationsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] FileOperations
Full Name: Autodesk.AutoCAD.Runtime.FileOperations
Implements: IDisposable

  CONSTRUCTORS:
    new FileOperations()

  METHODS:
    void Dispose()

  EVENTS:
    event FileHasClosedEventHandler FileHasClosed
    event FileOpenAbortedEventHandler FileOpenAborted
    event FileWillOpenEventHandler FileWillOpen

--------------------------------------------------------------------------------

[CLASS] FileOperationsEventArgs
Full Name: Autodesk.AutoCAD.Runtime.FileOperationsEventArgs
Inherits: EventArgs
Implements: IDisposable

  PROPERTIES:
    string FilePath { get; }
    bool KnownToBeUnmodified { get; }
    bool ReadOnly { get; }
    bool ReferenceFile { get; }
    bool ReloadNested { get; }
    bool ReloadPrimary { get; }
    VetoStatusType VetoStatus { get; }

  METHODS:
    void Dispose()
    bool Veto(VetoStatusType status)

--------------------------------------------------------------------------------

[CLASS] FileWillOpenEventHandler
Full Name: Autodesk.AutoCAD.Runtime.FileWillOpenEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new FileWillOpenEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(FileOperationsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(FileOperationsEventArgs e)

--------------------------------------------------------------------------------

[INTERFACE] ICommandLineCallable
Full Name: Autodesk.AutoCAD.Runtime.ICommandLineCallable

  PROPERTIES:
    Type ContextMenuExtensionType { get; }
    CommandFlags Flags { get; }
    string GlobalName { get; }
    string GroupName { get; }
    string HelpFileName { get; }
    string HelpTopic { get; }
    string LocalizedNameId { get; }

--------------------------------------------------------------------------------

[INTERFACE] ICondition
Full Name: Autodesk.AutoCAD.Runtime.ICondition

  METHODS:
    bool Evaluate()

--------------------------------------------------------------------------------

[INTERFACE] IExtensionApplication
Full Name: Autodesk.AutoCAD.Runtime.IExtensionApplication

  METHODS:
    void Initialize()
    void Terminate()

--------------------------------------------------------------------------------

[INTERFACE] IJavaScriptCallable
Full Name: Autodesk.AutoCAD.Runtime.IJavaScriptCallable

  PROPERTIES:
    string EntryPoint { get; }

--------------------------------------------------------------------------------

[INTERFACE] IJavaScriptCallableInternal
Full Name: Autodesk.AutoCAD.Runtime.IJavaScriptCallableInternal

  PROPERTIES:
    string EntryPoint { get; }

--------------------------------------------------------------------------------

[INTERFACE] IMenuItem
Full Name: Autodesk.AutoCAD.Runtime.IMenuItem

  PROPERTIES:
    bool Checked { get; set; }
    bool Enabled { get; set; }
        [SKIPPED PROPERTY] Icon: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    IEnumerable<IMenuItem> Items { get; }
    string Text { get; set; }
    bool Visible { get; set; }

  METHODS:
    void OnClicked(EventArgs eventArgs)

  EVENTS:
    event EventHandler Click

--------------------------------------------------------------------------------

[CLASS] Interop
Full Name: Autodesk.AutoCAD.Runtime.Interop

  METHODS:
    static void AttachUnmanagedObject(DisposableWrapper obj, IntPtr unmanagedPointer, bool autoDelete)
    static void Check(int returnValue)
    static void CheckAds(int returnValue)
    static bool CheckAdsForCancel(int returnValue)
    static void CheckBool(bool returnValue)
    static void CheckBoolean(int returnValue)
    static void CheckNull(IntPtr returnValue)
    static void DetachUnmanagedObject(DisposableWrapper obj)
    static void SetAutoDelete(DisposableWrapper obj, bool value)
    static void ThrowExceptionForErrorStatus(int errorStatus)

--------------------------------------------------------------------------------

[INTERFACE] IPointCloudExtractionProgressCallback
Full Name: Autodesk.AutoCAD.Runtime.IPointCloudExtractionProgressCallback

  METHODS:
    void Cancel()
    bool Cancelled()
    void End()
    void UpdateCaption(string caption)
    void UpdateProgress(int progress)
    void UpdateRemainTime(double remainTime)

--------------------------------------------------------------------------------

[CLASS] JavaScriptCallableInternalAttribute
Full Name: Autodesk.AutoCAD.Runtime.JavaScriptCallableInternalAttribute
Inherits: Attribute
Implements: IJavaScriptCallableInternal

  CONSTRUCTORS:
    new JavaScriptCallableInternalAttribute(string entryPoint)

  PROPERTIES:
    string EntryPoint { get; }

--------------------------------------------------------------------------------

[CLASS] JavaScriptCallbackAttribute
Full Name: Autodesk.AutoCAD.Runtime.JavaScriptCallbackAttribute
Inherits: Attribute
Implements: IJavaScriptCallable

  CONSTRUCTORS:
    new JavaScriptCallbackAttribute(string entryPoint)

  PROPERTIES:
    string EntryPoint { get; }

--------------------------------------------------------------------------------

[CLASS] JavaScriptClassAttribute
Full Name: Autodesk.AutoCAD.Runtime.JavaScriptClassAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new JavaScriptClassAttribute(Type name)

  PROPERTIES:
    Type Type { get; }

--------------------------------------------------------------------------------

[CLASS] JSInteropApplicationAttribute
Full Name: Autodesk.AutoCAD.Runtime.JSInteropApplicationAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new JSInteropApplicationAttribute(Type entryPointType)

  PROPERTIES:
    Type Type { get; }

--------------------------------------------------------------------------------

[ENUM] LispDataType
Full Name: Autodesk.AutoCAD.Runtime.LispDataType
Inherits: Enum

  Values:
    - None = 5000
    - Double = 5001
    - Point2d = 5002
    - Int16 = 5003
    - Angle = 5004
    - Text = 5005
    - ObjectId = 5006
    - SelectionSet = 5007
    - Orientation = 5008
    - Point3d = 5009
    - Int32 = 5010
    - Void = 5014
    - ListBegin = 5016
    - ListEnd = 5017
    - DottedPair = 5018
    - Nil = 5019
    - T_atom = 5021

--------------------------------------------------------------------------------

[CLASS] LispFunctionAttribute
Full Name: Autodesk.AutoCAD.Runtime.LispFunctionAttribute
Inherits: Attribute
Implements: ICommandLineCallable

  CONSTRUCTORS:
    new LispFunctionAttribute(string globalName, string localizedNameId, string helpTopic)
    new LispFunctionAttribute(string globalName, string localizedNameId, string helpFileName, string helpTopic)
    new LispFunctionAttribute(string globalName, string localizedNameId)
    new LispFunctionAttribute(string globalName)

  PROPERTIES:
    string GlobalName { get; }
    string HelpFileName { get; }
    string HelpTopic { get; }
    string LocalizedNameId { get; }

--------------------------------------------------------------------------------

[CLASS] Marshaler
Full Name: Autodesk.AutoCAD.Runtime.Marshaler

  METHODS:
    static object AcValueToObject(IntPtr acValueObj)
    [SKIPPED METHOD] BitmapInfoToBitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    [SKIPPED METHOD] BitmapToBitmapInfo: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static FullSubentityPath CopyToManagedFullSubentityPath(IntPtr path)
    static void CopyToUnmanagedFullSubentityPath(FullSubentityPath path, IntPtr newPath)
    static Vector3d[] GetInternalArray(Vector3dCollection col)
    static Vector2d[] GetInternalArray(Vector2dCollection col)
    static Point2d[] GetInternalArray(Point2dCollection col)
    static int[] GetInternalArray(IntegerCollection col)
    static double[] GetInternalArray(DoubleCollection col)
    static IntPtr ObjectsToResbuf(params object[] objects)
    static void ObjectToAcValue(object obj, IntPtr acValueObj)
    static IntPtr ObjectToResbuf(object o)
    static void ObjectToResbuf(object o, IntPtr rb)
    static object ResbufToObject(IntPtr rb)
    static TypedValue[] ResbufToTypedValues(IntPtr resbuf)
    static void SetInternalArray(Vector3dCollection col, Vector3d[] newArray, int count)
    static void SetInternalArray(Vector2dCollection col, Vector2d[] newArray, int count)
    static void SetInternalArray(Point2dCollection col, Point2d[] newArray, int count)
    static void SetInternalArray(IntegerCollection col, int[] newArray, int count)
    static void SetInternalArray(DoubleCollection col, double[] newArray, int count)
    static IntPtr TypedValuesToResbuf(params TypedValue[] values)

--------------------------------------------------------------------------------

[CLASS] ModuleLoadAbortedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.ModuleLoadAbortedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModuleLoadAbortedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DynamicLinkerEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DynamicLinkerEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ModuleLoadedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.ModuleLoadedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModuleLoadedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DynamicLinkerEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DynamicLinkerEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ModuleLoadingEventHandler
Full Name: Autodesk.AutoCAD.Runtime.ModuleLoadingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModuleLoadingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DynamicLinkerEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DynamicLinkerEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ModuleUnloadAbortedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.ModuleUnloadAbortedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModuleUnloadAbortedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DynamicLinkerEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DynamicLinkerEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ModuleUnloadedEventHandler
Full Name: Autodesk.AutoCAD.Runtime.ModuleUnloadedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModuleUnloadedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DynamicLinkerEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DynamicLinkerEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ModuleUnloadingEventHandler
Full Name: Autodesk.AutoCAD.Runtime.ModuleUnloadingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModuleUnloadingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DynamicLinkerEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DynamicLinkerEventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Overrule
Full Name: Autodesk.AutoCAD.Runtime.Overrule
Inherits: RXObject

  PROPERTIES:
    static bool Overruling { get; set; }

  METHODS:
    static void AddOverrule(RXClass targetClass, Overrule overrule, bool bAtLast)
    static bool HasOverrule(RXObject overruledSubject, RXClass targetClass)
    bool IsApplicable(RXObject overruledSubject)
    static void RemoveOverrule(RXClass targetClass, Overrule overrule)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

[CLASS] PerDocumentClassAttribute
Full Name: Autodesk.AutoCAD.Runtime.PerDocumentClassAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new PerDocumentClassAttribute(Type name)

  PROPERTIES:
    Type Type { get; }

--------------------------------------------------------------------------------

[CLASS] PointCloudExtractor
Full Name: Autodesk.AutoCAD.Runtime.PointCloudExtractor
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new PointCloudExtractor()

  METHODS:
    static ObjectIdCollection AppendLineProfile(PointCloudExtractResult profile, ObjectId spaceId, string layer, Color color)
    static ObjectIdCollection AppendPolylineProfile(PointCloudExtractResult profile, ObjectId spaceId, string layer, Color color, double polylineWidth)
    static PointCloudExtractResult Extract(PointCloudEx pointCloud, Vector3d planZDirection, Vector3d planXDirection, Point3d pointOnPlan, ExtractOption extractOption, IPointCloudExtractionProgressCallback progress)

--------------------------------------------------------------------------------

[CLASS] PointCloudExtractResult
Full Name: Autodesk.AutoCAD.Runtime.PointCloudExtractResult

  CONSTRUCTORS:
    new PointCloudExtractResult()

--------------------------------------------------------------------------------

[CLASS] ProfileCurve2d
Full Name: Autodesk.AutoCAD.Runtime.ProfileCurve2d
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new ProfileCurve2d(CircularArc2d arc)
    new ProfileCurve2d(LineSegment2d seg)
    new ProfileCurve2d()

  PROPERTIES:
    CircularArc2d Arc { get; set; }
    bool IsArc { get; }
    bool IsSegment { get; }
    LineSegment2d LineSeg { get; set; }

--------------------------------------------------------------------------------

[ENUM] ProfileCurveType
Full Name: Autodesk.AutoCAD.Runtime.ProfileCurveType
Inherits: Enum

  Values:
    - LineSeg = 0
    - Arc = 1

--------------------------------------------------------------------------------

[CLASS] ProgressMeter
Full Name: Autodesk.AutoCAD.Runtime.ProgressMeter
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new ProgressMeter()

  METHODS:
    void MeterProgress()
    void SetLimit(int max)
    void Start()
    void Start(string displayString)
    void Stop()

--------------------------------------------------------------------------------

