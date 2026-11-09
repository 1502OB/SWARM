# Autodesk.AutoCAD.ApplicationServices

NAMESPACE: Autodesk.AutoCAD.ApplicationServices
--------------------------------------------------------------------------------

[CLASS] EndExtractionEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.EndExtractionEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new EndExtractionEventArgs(EndExtractionEventArgs* args)

  METHODS:
    void AddProperty(ObjectId idObject, string propertyCategory, string propertyName, object propertyValue, string units, bool hidden)

--------------------------------------------------------------------------------

[CLASS] EndExtractionEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.EndExtractionEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndExtractionEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EndExtractionEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EndExtractionEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ExecuteInApplicationContextCallback
Full Name: Autodesk.AutoCAD.ApplicationServices.ExecuteInApplicationContextCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ExecuteInApplicationContextCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object userData, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object userData)

--------------------------------------------------------------------------------

[INTERFACE] IConfigurationSection
Full Name: Autodesk.AutoCAD.ApplicationServices.IConfigurationSection
Implements: IDisposable

  PROPERTIES:
    bool IsReadOnly { get; }

  METHODS:
    void Close()
    bool Contains(string name)
    bool ContainsSubsection(string name)
    IConfigurationSection CreateSubsection(string name)
    void Delete(string name)
    void DeleteSubsection(string name)
    IConfigurationSection OpenSubsection(string name)
    object ReadProperty(string name, object defaultValue)
    void WriteProperty(string name, object value)

--------------------------------------------------------------------------------

[CLASS] InplaceTextEditor
Full Name: Autodesk.AutoCAD.ApplicationServices.InplaceTextEditor
Inherits: TextEditor

  PROPERTIES:
    AlignmentType alginment { get; }
    bool Annotative { get; set; }
    bool CanCopy { get; }
    bool CanCut { get; }
    bool CanExitEditor { get; set; }
    bool CanPaste { get; }
    bool CanRedo { get; }
    bool CanUndo { get; }
    static InplaceTextEditor Current { get; }
    bool ForcedOpaqueBackground { get; }
    Color LayerColor { get; }
    bool MultiAttribute { get; }
    bool OpaqueBackground { get; set; }
    bool ParagraphSupported { get; }
    bool RulerHidden { get; set; }
    bool RulerSupported { get; }
    bool SimpleMText { get; }
    bool SpellerEnabled { get; set; }
    bool TableCell { get; }
    bool Text { get; }
    bool ToolbarHidden { get; set; }
    bool ToolbarOptionHidden { get; set; }
    bool UndoRecordingDisabled { get; set; }
    bool Wysiwyg { get; set; }

  METHODS:
    void AddUndoMarker(TextUndoType undoType)
    [SKIPPED METHOD] AttachmentMenus: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] ColumnMenus: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void ColumnsDialog(int staticColumnCount)
    [SKIPPED METHOD] ContextMenus: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void Copy()
    void Cut()
    void DrawHightlight()
    void FieldDialog(Field field)
    void FindAndReplaceDialog()
    void FindMatchItem(int flags, string findStr)
    void HelpDialog()
    void HightlightColorDialog()
    void ImportTextDialog()
    void InsertFile(string filename)
    static void Invoke(DBText text, out ObjectId[] appendedEntities)
    static void Invoke(MText mtext, InplaceTextEditorSettings settings)
    [SKIPPED METHOD] LineSpaceMenus: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void NewFeatureWorkShop()
    [SKIPPED METHOD] NumberingMenus: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void OtherSymbol()
    void ParagraphDialog()
    void Paste()
    void PasteWithoutFormats()
    void Redo()
    void RemoveHightlight()
    void ReplaceAllMatches(int flags, string findStr, string replaceStr)
    void ReplaceCurrentMatch(int flags, string findStr, string replaceStr)
    void SetStaticColumnsWithCount(int columnCount)
    void SpellDictionaryDialog()
    void SpellSettingDialog()
    void StackPropertyDialog()
    [SKIPPED METHOD] SymbolMenus: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void Undo()
    void WipeoutDialog()

--------------------------------------------------------------------------------

[CLASS] InplaceTextEditorSettings
Full Name: Autodesk.AutoCAD.ApplicationServices.InplaceTextEditorSettings
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new InplaceTextEditorSettings()

  PROPERTIES:
    double DefinedHeight { get; set; }
    EditFlags Flags { get; set; }
    bool SimpleMText { get; set; }
    bool TabSupported { get; set; }
    EntityType Type { get; set; }

--------------------------------------------------------------------------------

[CLASS] LayoutSwitchedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.LayoutSwitchedEventArgs
Inherits: EventArgs

  PROPERTIES:
    string NewLayout { get; }

--------------------------------------------------------------------------------

[CLASS] LayoutSwitchedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.LayoutSwitchedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayoutSwitchedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayoutSwitchedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayoutSwitchedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LayoutSwitchingEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.LayoutSwitchingEventArgs
Inherits: EventArgs

  PROPERTIES:
    string NewLayout { get; }
    string OldLayout { get; }

--------------------------------------------------------------------------------

[CLASS] LayoutSwitchingEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.LayoutSwitchingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LayoutSwitchingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LayoutSwitchingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LayoutSwitchingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LispWillStartEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.LispWillStartEventArgs
Inherits: EventArgs

  PROPERTIES:
    string FirstLine { get; }

--------------------------------------------------------------------------------

[CLASS] LispWillStartEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.LispWillStartEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LispWillStartEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LispWillStartEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LispWillStartEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LongTransactionEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.LongTransactionEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new LongTransactionEventArgs(LongTransaction longTrans)

  PROPERTIES:
    LongTransaction Transaction { get; }

--------------------------------------------------------------------------------

[CLASS] LongTransactionEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.LongTransactionEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new LongTransactionEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, LongTransactionEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, LongTransactionEventArgs e)

--------------------------------------------------------------------------------

[CLASS] LongTransactionManager
Full Name: Autodesk.AutoCAD.ApplicationServices.LongTransactionManager
Inherits: RXObject

  METHODS:
    void AbortLongTransaction(ObjectId transId, bool keepObjs)
    void AddClassFilter(RXClass filter)
    void CheckIn(ObjectId transId, IdMapping errorMap, bool keepObjs)
    ObjectId CheckOut(ObjectIdCollection checkoutSet, ObjectId toBlock, IdMapping errorMap, ObjectId blkRef)
    ObjectId CurrentLongTransactionFor(Document doc)
    bool IsFiltered(RXClass A_0)

  EVENTS:
    event LongTransactionEventHandler Aborted
    event LongTransactionEventHandler CheckedIn
    event LongTransactionEventHandler CheckingIn
    event LongTransactionEventHandler CheckedOut
    event CheckingOutEventHandler CheckingOut

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Marshaler
Full Name: Autodesk.AutoCAD.ApplicationServices.Marshaler

  METHODS:
    static SelectionSet AdsNameToSelectionSet(IntPtr pointerToAdsName)
    static void SelectionSetToAdsName(SelectionSet selectionSet, IntPtr pointerToAdsName)

--------------------------------------------------------------------------------

[CLASS] ModelessOperationEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.ModelessOperationEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Context { get; }

--------------------------------------------------------------------------------

[CLASS] ModelessOperationEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.ModelessOperationEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ModelessOperationEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ModelessOperationEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ModelessOperationEventArgs e)

--------------------------------------------------------------------------------

[CLASS] PreTranslateMessageEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.PreTranslateMessageEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new PreTranslateMessageEventHandler(object A_0, IntPtr A_1)

  METHODS:
    [SKIPPED METHOD] BeginInvoke: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void EndInvoke(IAsyncResult result)
    [SKIPPED METHOD] Invoke: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] ProfileEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.ProfileEventArgs
Inherits: EventArgs

  PROPERTIES:
    string ProfileName { get; }

--------------------------------------------------------------------------------

[CLASS] ProfileEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.ProfileEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ProfileEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ProfileEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ProfileEventArgs e)

--------------------------------------------------------------------------------

[CLASS] RecentDocument
Full Name: Autodesk.AutoCAD.ApplicationServices.RecentDocument

  PROPERTIES:
    string CurrentlyOpenBy { get; }
    bool IsClosed { get; }
    bool IsPinned { get; set; }
    ValueType LastOpenedTime { get; }
    string LastSavedBy { get; }
    string Path { get; }
        [SKIPPED PROPERTY] Preview: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    string Version { get; }

  METHODS:
    static RecentDocument FromPath(string path)

--------------------------------------------------------------------------------

[CLASS] RecentDocumentCollection
Full Name: Autodesk.AutoCAD.ApplicationServices.RecentDocumentCollection
Inherits: ReadOnlyObservableCollection`1
Implements: IDisposable

  PROPERTIES:
    static RecentDocumentCollection Instance { get; }
    bool UsePinnedItems { get; set; }

  METHODS:
    void Dispose()
    static void Remove(string path)

  EVENTS:
    event DocumentPinStateChangedEventHandler DocumentPinStateChanged
    event DocumentClosedEventHandler DocumentClosed

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangedEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventArgs
Inherits: EventArgs

  PROPERTIES:
    bool Changed { get; }
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangedEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SystemVariableChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SystemVariableChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SystemVariableChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangingEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.SystemVariableChangingEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Name { get; }

--------------------------------------------------------------------------------

[CLASS] SystemVariableChangingEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.SystemVariableChangingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new SystemVariableChangingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, SystemVariableChangingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, SystemVariableChangingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] TabbedDialogAction
Full Name: Autodesk.AutoCAD.ApplicationServices.TabbedDialogAction
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new TabbedDialogAction(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] TabbedDialogEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.TabbedDialogEventArgs
Inherits: EventArgs

  METHODS:
    void AddTab(string name, TabbedDialogExtension extension)

--------------------------------------------------------------------------------

[CLASS] TabbedDialogEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.TabbedDialogEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new TabbedDialogEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, TabbedDialogEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, TabbedDialogEventArgs e)

--------------------------------------------------------------------------------

[CLASS] TabbedDialogExtension
Full Name: Autodesk.AutoCAD.ApplicationServices.TabbedDialogExtension

  CONSTRUCTORS:
    new TabbedDialogExtension(object control, TabbedDialogAction onOK, TabbedDialogAction onCancel, TabbedDialogAction onHelp, TabbedDialogAction onApply)
    new TabbedDialogExtension(object control, TabbedDialogAction onOK, TabbedDialogAction onCancel, TabbedDialogAction onHelp)
    new TabbedDialogExtension(object control, TabbedDialogAction onOK, TabbedDialogAction onCancel)
    new TabbedDialogExtension(object control, TabbedDialogAction onOK)
    new TabbedDialogExtension(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    new TabbedDialogExtension(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    new TabbedDialogExtension(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    new TabbedDialogExtension(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

  PROPERTIES:
    object Control { get; }
    TabbedDialogAction OnApply { get; }
    TabbedDialogAction OnCancel { get; }
    TabbedDialogAction OnHelp { get; }
    TabbedDialogAction OnOk { get; }

  METHODS:
    static void SetDirty(object control, bool dirty)
    [SKIPPED METHOD] SetDirty: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] TransactionManager
Full Name: Autodesk.AutoCAD.ApplicationServices.TransactionManager
Inherits: TransactionManager

  PROPERTIES:
    Transaction TopTransaction { get; }

  METHODS:
    void EnableGraphicsFlush(bool doEnable)
    void FlushGraphics()
    Transaction StartTransaction()

--------------------------------------------------------------------------------

[CLASS] UnknownCommandEventArgs
Full Name: Autodesk.AutoCAD.ApplicationServices.UnknownCommandEventArgs
Inherits: EventArgs

  PROPERTIES:
    string GlobalCommandName { get; }

--------------------------------------------------------------------------------

[CLASS] UnknownCommandEventHandler
Full Name: Autodesk.AutoCAD.ApplicationServices.UnknownCommandEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new UnknownCommandEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, UnknownCommandEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, UnknownCommandEventArgs e)

--------------------------------------------------------------------------------

[CLASS] UserConfigurationManager
Full Name: Autodesk.AutoCAD.ApplicationServices.UserConfigurationManager
Inherits: MarshalByRefObject

  METHODS:
    IConfigurationSection OpenCurrentProfile()
    IConfigurationSection OpenDialogSection(object dialog)
    IConfigurationSection OpenGlobalSection()
    void SetCurrentProfile(string profileName)

  EVENTS:
    event ProfileEventHandler ProfileSaved
    event ProfileEventHandler ProfileSaving
    event ProfileEventHandler ProfileReset
    event ProfileEventHandler ProfileResetting
    event ProfileEventHandler CurrentProfileSaved
    event ProfileEventHandler CurrentProfileSaving
    event ProfileEventHandler CurrentProfileReset
    event ProfileEventHandler CurrentProfileResetting
    event ProfileEventHandler CurrentProfileChanged
    event ProfileEventHandler CurrentProfileChanging

--------------------------------------------------------------------------------

[STRUCT] WhoHasInfo
Full Name: Autodesk.AutoCAD.ApplicationServices.WhoHasInfo

  PROPERTIES:
    string ComputerName { get; }
    bool IsFileLocked { get; }
    string OpenTime { get; }
    string UserName { get; }

--------------------------------------------------------------------------------

[CLASS] XrefFileLock
Full Name: Autodesk.AutoCAD.ApplicationServices.XrefFileLock
Inherits: DisposableWrapper

  PROPERTIES:
    ObjectIdCollection OutOfSyncBlockIds { get; }
    int XloadCtlType { get; }

  METHODS:
    static ObjectIdCollection ConsistencyCheck(ObjectId selectedBlockId)
    void ConsistencyCheck()
    static ObjectIdCollection ConsistencyCheckLocal(ObjectId selectedBlockId)
    static int GetXloadCtlType(ObjectId selectedBlockId)
    static XrefFileLock LockFile(ObjectId selectedBlockId)
    void ReleaseFile()
    void ReleaseFile(bool skipSaveback, bool reload)
    static void ReloadFile(ObjectIdCollection blocks)
    static void ReloadFile(ObjectIdCollection blocks, int xldCtlType)
    void ReloadFile()
    void ReloadFile(int xldCtlType)

--------------------------------------------------------------------------------

