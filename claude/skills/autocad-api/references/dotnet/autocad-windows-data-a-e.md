# Autodesk.AutoCAD.Windows.Data

NAMESPACE: Autodesk.AutoCAD.Windows.Data
--------------------------------------------------------------------------------

[CLASS] AnimationEditor
Full Name: Autodesk.AutoCAD.Windows.Data.AnimationEditor
Implements: INotifyPropertyChanged

  CONSTRUCTORS:
    new AnimationEditor()

  PROPERTIES:
    bool IsActive { get; set; }
    NavStatus Status { get; set; }

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] AttachmentPointCommand
Full Name: Autodesk.AutoCAD.Windows.Data.AttachmentPointCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new AttachmentPointCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] AutoCompleteList
Full Name: Autodesk.AutoCAD.Windows.Data.AutoCompleteList
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new AutoCompleteList(string prefix, string parsedSearch, int cmdFlags, CommandCompletionType searchType, bool abortIfMultipleMatchesFound)
    new AutoCompleteList(string rawSearch, CommandCompletionType searchType)

  PROPERTIES:
    CommandLineItem Item { get; }
    int Length { get; }

  METHODS:
    void Sort()

--------------------------------------------------------------------------------

[CLASS] AutoCompleteToolTipService
Full Name: Autodesk.AutoCAD.Windows.Data.AutoCompleteToolTipService

  CONSTRUCTORS:
    new AutoCompleteToolTipService()

  METHODS:
    static void HideToolTip()
    static void ShowToolTip(ValueType pTooltipInfo)

--------------------------------------------------------------------------------

[CLASS] AutoCorrectList
Full Name: Autodesk.AutoCAD.Windows.Data.AutoCorrectList
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new AutoCorrectList(string rawSearch, AutoCorrectType searchType)

  PROPERTIES:
    bool HasClearWinner { get; }
    CommandLineItem Item { get; }
    int Length { get; }

  METHODS:
    void Sort()

--------------------------------------------------------------------------------

[CLASS] AutoCorrectorService
Full Name: Autodesk.AutoCAD.Windows.Data.AutoCorrectorService

  CONSTRUCTORS:
    new AutoCorrectorService()

  METHODS:
    static bool IsAutoCorrectSupported()
    static void IsInputCommand(bool isInput)
    static void UpdateErrInputCount(string errInput, string cmdGlobalName, string cmdLocalName)

--------------------------------------------------------------------------------

[ENUM] AutoCorrectType
Full Name: Autodesk.AutoCAD.Windows.Data.AutoCorrectType
Inherits: Enum

  Values:
    - CommandSysvar = 0
    - InternalCommand = 1

--------------------------------------------------------------------------------

[CLASS] BackgroundMaskCommand
Full Name: Autodesk.AutoCAD.Windows.Data.BackgroundMaskCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new BackgroundMaskCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] BlockDialogModelBase
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelBase
Implements: INotifyPropertyChanged

  PROPERTIES:
    string Name { get; }

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelInsert
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelInsert
Inherits: BlockDialogModelBase
Implements: IDisposable

  CONSTRUCTORS:
    new BlockDialogModelInsert()

  PROPERTIES:
    BlockDialogModelValue<bool> AutoPlacement { get; }
    BlockDialogModelValue<bool> Explode { get; }
    BlockDialogModelInsertionPoint InsertionPoint { get; }
    bool IsDragingInsert { get; }
        [SKIPPED PROPERTY] PaletteWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    BlockDialogModelValue<bool> Repeat { get; }
    BlockDialogModelRotation Rotation { get; }
    BlockDialogModelScales Scales { get; }
    string SelectedBlockData { set; }
    BlockDialogModelUnit Unit { get; }
    BlockDialogModelValue<bool> UseGeoData { get; }

  METHODS:
    void Dispose()
    void initBlockStreamUIData()
    void Insert(string blockKey)
    void InsertAndRedefine(string blockKey)
    void InsertSnapshot(string snapshotPath, string targetBlockName)
    void InsertSnapshotAndRedefine(string snapshotPath, string targetBlockName)
    void InsertSnapshotWithoutRedefine(string snapshotPath, string targetBlockName)
    void InsertWithoutRedefine(string blockKey)
    bool IsBlockCanBeRedefined(string blockKey)
    bool isPaletteVisible()
    void RedefineFromSnapshotOnly(string snapshotPath, string targetBlockName)
    void RedefineOnly(string blockKey)
    void saveBlockStreamUIData()

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelInsertionPoint
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelInsertionPoint

  CONSTRUCTORS:
    new BlockDialogModelInsertionPoint(BlockDialogModelBase host)

  PROPERTIES:
    BlockDialogModelValue<bool> PickOnScreen { get; }
    BlockDialogModelValue<double> X { get; }
    BlockDialogModelValue<double> Y { get; }
    BlockDialogModelValue<double> Z { get; }

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelRotation
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelRotation

  CONSTRUCTORS:
    new BlockDialogModelRotation(BlockDialogModelBase host)

  PROPERTIES:
    BlockDialogModelValue<double> Angle { get; }
    BlockDialogModelValue<bool> PickOnScreen { get; }

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelScales
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelScales

  CONSTRUCTORS:
    new BlockDialogModelScales(BlockDialogModelBase host)

  PROPERTIES:
    BlockDialogModelValue<bool> ScaleOnScreen { get; }
    BlockDialogModelValue<bool> UniformScale { get; }
    BlockDialogModelValue<double> X { get; }
    BlockDialogModelValue<bool> XLabelState { get; }
    BlockDialogModelValue<double> Y { get; }
    BlockDialogModelValue<double> Z { get; }

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelUnit
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelUnit

  CONSTRUCTORS:
    new BlockDialogModelUnit(BlockDialogModelBase host)

  PROPERTIES:
    BlockDialogModelValueString ScaleFactor { get; }
    BlockDialogModelValueString UnitValue { get; }

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelValue<bool>
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelValue<bool>

  CONSTRUCTORS:
    new BlockDialogModelValue<bool>(string dataName, BlockDialogModelBase host)

  PROPERTIES:
    bool Enabled { get; }
    bool Value { get; set; }

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelValue<double>
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelValue<double>

  CONSTRUCTORS:
    new BlockDialogModelValue<double>(string dataName, BlockDialogModelBase host)

  PROPERTIES:
    bool Enabled { get; }
    double Value { get; set; }

--------------------------------------------------------------------------------

[CLASS] BlockDialogModelValueString
Full Name: Autodesk.AutoCAD.Windows.Data.BlockDialogModelValueString

  CONSTRUCTORS:
    new BlockDialogModelValueString(string dataName, BlockDialogModelBase host)

  PROPERTIES:
    bool Enabled { get; }
    string Value { get; set; }

--------------------------------------------------------------------------------

[CLASS] BlockEditor
Full Name: Autodesk.AutoCAD.Windows.Data.BlockEditor
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new BlockEditor()

  PROPERTIES:
    string BlockName { get; }
    string CurrentVisibilityStateName { get; set; }
    bool IsAuthorPaletteVisible { get; }
    bool IsInBlockEditor { get; }
    bool IsVisibilityParameterPresent { get; }
    string Tooltip { get; }
    ObservableCollection<string> VisibilitySets { get; }

  METHODS:
    void Dispose()
    void OnDocumentActivated(object sender, DocumentCollectionEventArgs e)
    void OnSystemVariableChanged(object sender, PropertyChangedEventArgs e)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] BlockLibraryInterop
Full Name: Autodesk.AutoCAD.Windows.Data.BlockLibraryInterop
Implements: IDisposable

  CONSTRUCTORS:
    new BlockLibraryInterop()

  METHODS:
    void Dispose()
    [SKIPPED METHOD] setWPFBrowserPalette: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] CharacterSetCommand
Full Name: Autodesk.AutoCAD.Windows.Data.CharacterSetCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new CharacterSetCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] ClearLineSpaceCommand
Full Name: Autodesk.AutoCAD.Windows.Data.ClearLineSpaceCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new ClearLineSpaceCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] CloseCommand
Full Name: Autodesk.AutoCAD.Windows.Data.CloseCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new CloseCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] CMLContentSearchPreviews
Full Name: Autodesk.AutoCAD.Windows.Data.CMLContentSearchPreviews

  CONSTRUCTORS:
    new CMLContentSearchPreviews()

  METHODS:
    static void ClearDimStySampleObjects()
    [SKIPPED METHOD] ConvertImageToImageSource: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static void CreateDimStySampleObjects()
    [SKIPPED METHOD] GetBlockTRThumbnail: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool GetColorFromSysvarSettings(string sysvarName, ref Color outColor)
    [SKIPPED METHOD] GetDimStyleThumbnail: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] ColorCollection
Full Name: Autodesk.AutoCAD.Windows.Data.ColorCollection
Inherits: DataItemCollection

  PROPERTIES:
    ILookup<ICommand> Commands { get; }

  METHODS:
    AcDiDynamicObject* AddIfNotContain(Color value)
    object AddIfNotContain(object value)

--------------------------------------------------------------------------------

[CLASS] ColorCommand
Full Name: Autodesk.AutoCAD.Windows.Data.ColorCommand
Implements: IGalleryCommand, ICommand

  CONSTRUCTORS:
    new ColorCommand(string colorPropertyName, string previewPropertyName)
    new ColorCommand(string colorPropertyName)

  METHODS:
    bool CanExecute(object parameter)
    void Execute(object parameter)

  EVENTS:
    event EventHandler CanExecuteChanged

--------------------------------------------------------------------------------

[CLASS] ColumnsSettingsDialogCommand
Full Name: Autodesk.AutoCAD.Windows.Data.ColumnsSettingsDialogCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new ColumnsSettingsDialogCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] Command
Full Name: Autodesk.AutoCAD.Windows.Data.Command
Implements: ICommand, ICloneable

  PROPERTIES:
    bool Hidden { get; set; }
    bool IsFocusAble { get; set; }
    bool Synchronous { get; set; }

  METHODS:
    bool CanExecute(object parameter)
    object Clone()
    void Execute(object parameter)

  EVENTS:
    event EventHandler CanExecuteChanged

--------------------------------------------------------------------------------

[ENUM] CommandCompletionType
Full Name: Autodesk.AutoCAD.Windows.Data.CommandCompletionType
Inherits: Enum

  Values:
    - Command = 1
    - Keyword = 2
    - File = 4

--------------------------------------------------------------------------------

[CLASS] CommandEditor
Full Name: Autodesk.AutoCAD.Windows.Data.CommandEditor
Implements: INotifyPropertyChanged

  PROPERTIES:
    int ActualDisplayedLinesCount { get; set; }
    string CurrentCommand { get; }
    IntPtr CurrentCommandIcon { get; }
    ObservableCollection<CommandHistory> History { get; }
    bool IsBusy { get; set; }
    bool NeedShowClickableKeywords { get; }
    PromptAndInput PromptAndInput { get; }
    List<string> RecentCommands { get; }
    ObservableCollection<CommandHistory> TemporaryHistory { get; }
    int VisibleHistoryItemsCount { get; set; }

  METHODS:
    void ClearTempHistory()
    void CopyHistoryToClipBoard()
    void SyncHistoryViewList()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] CommandEditorManager
Full Name: Autodesk.AutoCAD.Windows.Data.CommandEditorManager
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    CommandEditor ActiveEditor { get; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] CommandHistory
Full Name: Autodesk.AutoCAD.Windows.Data.CommandHistory

  PROPERTIES:
    string Command { get; }

--------------------------------------------------------------------------------

[CLASS] CommandLineItem
Full Name: Autodesk.AutoCAD.Windows.Data.CommandLineItem

  PROPERTIES:
    string DisplayText { get; }
    int Flags { get; }
    string GlobalName { get; }
    IntPtr Image { get; }
    string ImageKey { get; }
    string LocalName { get; }
    IntPtr Tooltip { get; }
    string UnderlyingCommand { get; }
    string Value { get; }

  METHODS:
    void AddRef()
    [SKIPPED METHOD] ImageFromKey: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    void Release()

--------------------------------------------------------------------------------

[CLASS] Commands3DPrint
Full Name: Autodesk.AutoCAD.Windows.Data.Commands3DPrint

  CONSTRUCTORS:
    new Commands3DPrint()

  PROPERTIES:
    bool IsPrintStudioAvailable { get; }

--------------------------------------------------------------------------------

[CLASS] CommandStack
Full Name: Autodesk.AutoCAD.Windows.Data.CommandStack
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    string AllCommands { get; }
    string MainCommand { get; }

  METHODS:
    bool ContainsAny(params string[] commands)
    bool ContainsOnly(params string[] commands)
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] CommonProperties
Full Name: Autodesk.AutoCAD.Windows.Data.CommonProperties
Inherits: WrapICustomTypeDescriptor
Implements: INotifyPropertyChanged, ILookup<CommonProperty>, IDisposable

  PROPERTIES:
    CommonProperty Item { get; }
    Nullable<bool> RequireRegisteredTypes { get; }

  METHODS:
    bool ContainsAny(params string[] properties)
    void Dispose()
    TypeConverter GetConverterFromRegisteredType()
    EventDescriptorCollection GetEventsFromRegisteredType()
    PropertyDescriptorCollection GetPropertiesFromRegisteredType()
    void PreviewEnded(object sender, LivePreviewEventArgs e)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] CommonProperty
Full Name: Autodesk.AutoCAD.Windows.Data.CommonProperty
Inherits: PropertyDescriptor
Implements: IDisposable

  PROPERTIES:
    Type ComponentType { get; }
    bool IsReadOnly { get; }
    Type PropertyType { get; }
    bool SupportsChangeEvents { get; }
    TrueValues TrueValues { get; }

  METHODS:
    bool CanResetValue(object component)
    void Dispose()
    object GetValue()
    object GetValue(object component)
    void ResetValue(object component)
    void SetValue(object value)
    void SetValue(object component, object value)
    bool ShouldSerializeValue(object component)

--------------------------------------------------------------------------------

[CLASS] DataBindings
Full Name: Autodesk.AutoCAD.Windows.Data.DataBindings
Implements: IDisposable

  PROPERTIES:
    CommandStack ActiveCommands { get; }
    AnimationEditor AnimationEditor { get; }
    object ArrayCreation { get; }
    BlockDialogModelInsert BlockDialogModelInsert { get; }
    BlockEditor BlockEditor { get; }
    DataItemCollections Collections { get; }
        [SKIPPED PROPERTY] ColorSettings: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    CommandEditorManager CommandEditorManager { get; }
    ILookup<Command> Commands { get; }
    Commands3DPrint Commands3DPrint { get; }
    object CurrentViewport { get; }
    static object DoNothing { get; }
    EffectivePropertySource EffectivePropertySource { get; }
    EplotExport EplotExport { get; }
    GeoData GeoData { get; }
    HatchCommands HatchCommands { get; }
    InplaceTextEditor InplaceTextEditor { get; }
    IsoDraft IsoDraft { get; }
    LightEngine LightEngine { get; }
    ILookup<NativeFunction> NativeFunctions { get; }
    RenderEngine RenderEngine { get; }
    ILookup<SystemVariable> SystemVariables { get; }
    SysvarMonitorState SysvarMonitorState { get; }
    TextSize TextSize { get; }
    ThemeEngine ThemeEngine { get; }
    ILookup<ICommand> ViewportsCommands { get; }
    Views2DCommands Views2DCommands { get; }
        [SKIPPED PROPERTY] Workspaces: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

  METHODS:
    void Dispose()
    string GetLocalColorName(Color acadColor)
    void Refresh()

--------------------------------------------------------------------------------

[CLASS] DataItemCollection
Full Name: Autodesk.AutoCAD.Windows.Data.DataItemCollection
Implements: IList<ICustomTypeDescriptor>, ICollection<ICustomTypeDescriptor>, IEnumerable<ICustomTypeDescriptor>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged, INotifyCollectionItemsChanged, IDisposable, IInvalidateProperty

  PROPERTIES:
    bool Active { get; }
    int Count { get; }
    ICustomTypeDescriptor CurrentItem { get; set; }
    bool IsReadOnly { get; }
    ICustomTypeDescriptor Item { get; set; }

  METHODS:
    void Dispose()
    void InvalidateProperty(string propertyName)

  EVENTS:
    event NotifyCollectionItemsChangedEventHandler ItemsChanged
    event NotifyCollectionChangedEventHandler CollectionChanged
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] DataItemCollections
Full Name: Autodesk.AutoCAD.Windows.Data.DataItemCollections
Implements: ILookup<DataItemCollection>, IDisposable

  PROPERTIES:
    DataItemCollection Blocks { get; }
    ColorCollection Colors { get; }
    DataItemCollection DetailViewStyles { get; }
    DataItemCollection DimensionStyles { get; }
    DataItemCollection Item { get; }
    DataItemCollection LayerFilters { get; }
    DataItemCollection Layers { get; }
    DataItemCollection LayerStates { get; }
    DataItemCollection Linetypes { get; }
    EnumItemCollection Lineweights { get; }
    DataItemCollection MleaderStyles { get; }
    DataItemCollection NamedModelViews { get; }
    DataItemCollection NamedViews { get; }
    DataItemCollection PlotStyles { get; }
    DataItemCollection PointCloudClassificationSchemes { get; }
    DataItemCollection PointCloudColorSchemes { get; }
    DataItemCollection PointClouds { get; }
    DataItemCollection RecentBlocks { get; }
    DataItemCollection RenderPresets { get; }
    DataItemCollection SectionViewStyles { get; }
    Selection Selection { get; }
    DataItemCollection TableCellStyles { get; }
    DataItemCollection TableStyles { get; }
    DataItemCollection TextStyles { get; }
    DataItemCollection UcsPlanes { get; }
    IList<UIFontInfo> UIFontInfoCollection { get; }
    DataItemCollection VisualStyles { get; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] DataItemFactoryMethod
Full Name: Autodesk.AutoCAD.Windows.Data.DataItemFactoryMethod
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DataItemFactoryMethod(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(FullSubentityPath objId, object wrapper, AsyncCallback callback, object obj)
    DataItem EndInvoke(IAsyncResult result)
    DataItem Invoke(FullSubentityPath objId, object wrapper)

--------------------------------------------------------------------------------

[CLASS] DbObjectCollection
Full Name: Autodesk.AutoCAD.Windows.Data.DbObjectCollection
Inherits: DataItemCollection

--------------------------------------------------------------------------------

[CLASS] DefinedSymbolsCommand
Full Name: Autodesk.AutoCAD.Windows.Data.DefinedSymbolsCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new DefinedSymbolsCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] DisposingEventHandler
Full Name: Autodesk.AutoCAD.Windows.Data.DisposingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DisposingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[CLASS] EffectiveProperties
Full Name: Autodesk.AutoCAD.Windows.Data.EffectiveProperties
Inherits: WrapICustomTypeDescriptor
Implements: INotifyPropertyChanged, IInvalidateProperty

  PROPERTIES:
    EffectiveProperty Item { get; }
    Nullable<bool> RequireRegisteredTypes { get; }

  METHODS:
    TypeConverter GetConverterFromRegisteredType()
    EventDescriptorCollection GetEventsFromRegisteredType()
    PropertyDescriptorCollection GetPropertiesFromRegisteredType()
    void Invalidate(string propertyName)
    void InvalidateProperty(string propertyName)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] EffectiveProperty
Full Name: Autodesk.AutoCAD.Windows.Data.EffectiveProperty
Inherits: PropertyDescriptor

  PROPERTIES:
    Type ComponentType { get; }
    bool IsReadOnly { get; }
    Type PropertyType { get; }
    bool SupportsChangeEvents { get; }
    TrueValues TrueValues { get; }

  METHODS:
    bool CanResetValue(object component)
    object GetValue(object component)
    void ResetValue(object component)
    void SetValue(object component, object value)
    bool ShouldSerializeValue(object component)

--------------------------------------------------------------------------------

[CLASS] EffectivePropertySource
Full Name: Autodesk.AutoCAD.Windows.Data.EffectivePropertySource
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    EffectiveProperties EffectiveProperties { get; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] EnumItemCollection
Full Name: Autodesk.AutoCAD.Windows.Data.EnumItemCollection
Implements: IList<ICustomTypeDescriptor>, ICollection<ICustomTypeDescriptor>, IEnumerable<ICustomTypeDescriptor>, IEnumerable, INotifyPropertyChanged, IDisposable, IInvalidateProperty

  PROPERTIES:
    int Count { get; }
    ICustomTypeDescriptor CurrentItem { get; set; }
    bool IsReadOnly { get; }
    ICustomTypeDescriptor Item { get; set; }

  METHODS:
    void Dispose()
    void InvalidateProperty(string propertyName)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] EplotExport
Full Name: Autodesk.AutoCAD.Windows.Data.EplotExport
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new EplotExport()

  PROPERTIES:
    string CurrentItemExport { get; set; }
    string CurrentItemPageSetup { get; set; }
    bool EnablePageSetupBtn { get; }
    bool EnableWindowBtn { get; }
    ObservableCollection<string> ExportSet { get; }
    ObservableCollection<string> PageSetupSet { get; }

  METHODS:
    void Dispose()
    void UpdateCurrentItemExport()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] Expression
Full Name: Autodesk.AutoCAD.Windows.Data.Expression
Implements: INotifyPropertyChanged

  CONSTRUCTORS:
    new Expression(object value, string text)

  PROPERTIES:
    string Text { get; set; }
    object Value { get; set; }

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] ExtendedPropertyEventArgs
Full Name: Autodesk.AutoCAD.Windows.Data.ExtendedPropertyEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new ExtendedPropertyEventArgs(string propertyName, ICustomTypeDescriptor dataItem, string dataItemType)

  PROPERTIES:
    ICustomTypeDescriptor DataItem { get; }
    string DataItemType { get; }
    PropertyDescriptor PropertyDesc { get; set; }
    string PropertyName { get; }

--------------------------------------------------------------------------------

[CLASS] ExtendedPropertyEventHandler
Full Name: Autodesk.AutoCAD.Windows.Data.ExtendedPropertyEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ExtendedPropertyEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ExtendedPropertyEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ExtendedPropertyEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ExtendedPropertyManager
Full Name: Autodesk.AutoCAD.Windows.Data.ExtendedPropertyManager

  CONSTRUCTORS:
    new ExtendedPropertyManager()

  METHODS:
    static PropertyDescriptor TryGetExtendedProperty(string propertyName, ICustomTypeDescriptor dataItem, string dataItemType)

  EVENTS:
    event ExtendedPropertyEventHandler RegisterExtendedProperty

--------------------------------------------------------------------------------

