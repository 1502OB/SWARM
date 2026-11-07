# Autodesk.AutoCAD.Windows.Data

NAMESPACE: Autodesk.AutoCAD.Windows.Data
--------------------------------------------------------------------------------

[CLASS] FieldDialogCommand
Full Name: Autodesk.AutoCAD.Windows.Data.FieldDialogCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new FieldDialogCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] FindReplaceCommand
Full Name: Autodesk.AutoCAD.Windows.Data.FindReplaceCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new FindReplaceCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] FindTextCommand
Full Name: Autodesk.AutoCAD.Windows.Data.FindTextCommand
Inherits: IPECommandBase
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new FindTextCommand()

  METHODS:
    void Dispose()
    void Execute(object parameter)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] GeoData
Full Name: Autodesk.AutoCAD.Windows.Data.GeoData
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new GeoData()

  PROPERTIES:
    static GeoData Instance { get; }
    ValueType IsGeoCsSupported { get; }
    ValueType IsGeomapImageUpdateAllowed { get; }
    ValueType IsMyGeoDataAvailable { get; }
    ValueType IsMyLocationAvaliable { get; }
    ValueType IsSnapShotAllowed { get; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] HatchCommands
Full Name: Autodesk.AutoCAD.Windows.Data.HatchCommands
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    bool HasDefinedBoundaries { get; set; }
    bool IsHatchCreation { get; }
    ICommand LaunchDialog { get; }
    ICommand MatchProperties { get; }
    ICommand PickPoints { get; }
    ICommand RecreateBoundaries { get; }
    ICommand RemoveBoundaries { get; }
    ICommand SelectObjects { get; }
    ICommand SetOrigin { get; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] HatchPatterns
Full Name: Autodesk.AutoCAD.Windows.Data.HatchPatterns
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
        [SKIPPED PROPERTY] AllPatterns: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    ObservableCollection<string> CustomPatterns { get; }
    ObservableCollection<string> GradientPatterns { get; }
    static HatchPatterns Instance { get; }
    bool IsUnknownPatternSelected { get; set; }
    ObservableCollection<string> PredefinedANSIPatterns { get; }
    ObservableCollection<string> PredefinedISOPatterns { get; }
    ObservableCollection<string> PredefinedOtherPatterns { get; }
    ObservableCollection<string> PredefinedPatterns { get; }
    ObservableCollection<string> UnknownPatterns { get; }
    string UserDefinedPattern { get; }

  METHODS:
    bool AppendUnknownPattern(string unknownPattern)
    void Dispose()
    static IntPtr GetCustomSwatchImage(string name, Color patternColor, Color backgroundColor, int nWidth, int nHeight)
    static IntPtr GetGradientSwatchImage(string name, double angle, bool bShifted, Color startColor, Color stopColor, int nWidth, int nHeight)
    static IntPtr GetPatternSwatchImage(string name, Color patternColor, Color backgroundColor, int nWidth, int nHeight)
    static PatPatternCategory ValidPatternName(string patternName)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[INTERFACE] IDataItem
Full Name: Autodesk.AutoCAD.Windows.Data.IDataItem

  PROPERTIES:
    ObjectId ObjectId { get; }

  METHODS:
    IDataItemTransaction StartTransaction(OpenMode mode)

--------------------------------------------------------------------------------

[INTERFACE] IDataItemTransaction
Full Name: Autodesk.AutoCAD.Windows.Data.IDataItemTransaction
Implements: IDisposable

  PROPERTIES:
    object Item { get; }

  METHODS:
    void Commit()

--------------------------------------------------------------------------------

[INTERFACE] IInvalidateProperty
Full Name: Autodesk.AutoCAD.Windows.Data.IInvalidateProperty

  METHODS:
    void InvalidateProperty(string propertyName)

--------------------------------------------------------------------------------

[INTERFACE] ILookup`1
Full Name: Autodesk.AutoCAD.Windows.Data.ILookup`1
Type Parameters: <T>

  PROPERTIES:
    T Item { get; }

--------------------------------------------------------------------------------

[CLASS] ImportCommand
Full Name: Autodesk.AutoCAD.Windows.Data.ImportCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new ImportCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[INTERFACE] INamedImageProvider
Full Name: Autodesk.AutoCAD.Windows.Data.INamedImageProvider

  METHODS:
    [SKIPPED METHOD] GetNamedImage: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[INTERFACE] INamedValue
Full Name: Autodesk.AutoCAD.Windows.Data.INamedValue

  PROPERTIES:
    string Name { get; }
    object Value { get; }

--------------------------------------------------------------------------------

[INTERFACE] INotifyCollectionItemsChanged
Full Name: Autodesk.AutoCAD.Windows.Data.INotifyCollectionItemsChanged

  EVENTS:
    event NotifyCollectionItemsChangedEventHandler ItemsChanged

--------------------------------------------------------------------------------

[CLASS] InplaceTextEditor
Full Name: Autodesk.AutoCAD.Windows.Data.InplaceTextEditor
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    bool Annotative { get; set; }
    ICommand AttachmentPoint { get; }
    IPEAttachmentPoint AttachmentPointType { get; set; }
    bool AutoCAPS { get; set; }
    ICommand BackgroundMask { get; }
    bool Bold { get; set; }
    ICommand CharacterSet { get; }
    ICommand ClearLineSpace { get; }
    ICommand Close { get; }
    Color Color { get; set; }
    ICommand ColumnsSettingsDialog { get; }
    IPEColumnType ColumnType { get; set; }
    UIFontInfo CurrentFont { get; set; }
    string CurrentLayer { get; set; }
    ICommand DefinedSymbols { get; }
    IPEDynamicColumnType DynamicColumnType { get; set; }
    ICommand FieldDialog { get; }
    ICommand FindReplace { get; }
    IPEFlowAlignmentType FlowAlignment { get; }
    ObservableCollection<string> FontSizes { get; }
    bool HaveSelection { get; }
    ICommand Import { get; }
    bool InsertColumnBreak { get; set; }
    bool InsertField { get; set; }
    bool IsActive { get; }
    bool IsAnnotativeEnabled { get; }
    bool IsAutoListEnabled { get; set; }
    bool IsBoldEnabled { get; }
    bool IsChangeCaseAvailable { get; }
    bool IsColumnsEnabled { get; }
    bool IsInsertColumnBreak { get; }
    bool IsInsertFieldEnabled { get; }
    bool IsItalicEnabled { get; }
    bool IsLineSpacingFactorAvailable { get; }
    bool IsMatchPropertiesState { get; set; }
    bool IsOpaqueBackgroundEnabled { get; }
    bool IsOverlineEnabled { get; }
    bool IsParagraphAlignmentAvailable { get; }
    bool IsRedoEnabled { get; }
    bool IsRulerChecked { get; }
    bool IsRulerEnabled { get; }
    bool IsShowToolbarEnabled { get; }
    bool IsSpellerEnabled { get; }
    bool IsStackOrUnstackEnabled { get; }
    bool IsStrikeThroughEnabled { get; }
    bool IsTabOnlyDelimiterEnabled { get; }
    bool IsUnderlineEnabled { get; }
    bool IsUndoEnabled { get; }
    bool IsWysiwyg { get; }
    bool Italic { get; set; }
    int LanguageIndexType { get; }
    double LineSpacingFactor { get; set; }
    Collection<double> LineSpacingFactors { get; }
    ICommand LineSpacingMultiples { get; }
    LineSpacingTypes LineSpacingType { get; set; }
    ICommand Lowercase { get; }
    ICommand MoreLineSpace { get; }
    ICommand Numbering { get; }
    bool NumberingAvailable { get; set; }
    IPENumberingType NumberingType { get; set; }
    double ObliqueAngle { get; set; }
    bool OpaqueBackground { get; set; }
    ICommand Options { get; }
    bool Overline { get; set; }
    IPEParagraphAlignmentType ParagraphAlignment { get; set; }
    ICommand ParagraphAlignmentOpions { get; }
    ICommand ParagraphDialog { get; }
    ICommand QuickFindText { get; }
    ICommand Redo { get; }
    bool Ruler { get; set; }
    bool Scriptable { get; }
    bool ShowToolbar { get; set; }
    bool Speller { get; set; }
    bool Stacked { get; set; }
    ICommand StaticColumns { get; }
    Collection<double> StaticColumnsList { get; }
    IPEStaticColumnType StaticColumnType { get; set; }
    bool StrikeThrough { get; set; }
    bool Subscript { get; set; }
    bool Superscript { get; set; }
    object TextHeight { get; set; }
    object TextStyle { get; set; }
    double TrackingFactor { get; set; }
    bool Underline { get; set; }
    ICommand Undo { get; }
    ICommand Uppercase { get; }
    double WidthScale { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] InputBuffer
Full Name: Autodesk.AutoCAD.Windows.Data.InputBuffer
Implements: INotifyPropertyChanged

  PROPERTIES:
    bool HasSelection { get; }
    int Position { get; }
    int SelectPosition { get; }
    string Text { get; }

  METHODS:
    bool ExecuteKeyword(string keyword)
    bool IsPromptingForCommandName()
    bool OnCharQueue(uint inputChar, uint repCnt, uint flags)
    bool OnKeyDownQueue(uint inputChar, uint repCnt, uint flags)
    bool OnSetSelectedTextQueue(int start, int end)
    bool OnSysKeyDownQueue(uint inputChar, uint repCnt, uint flags)
    string ParseKeywordForExecute(string keyword)
    void PasteString(string originalString, string stringToPaste)
    void ReplaceInputQueue(string newInput)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[ENUM] InputSearchOptions
Full Name: Autodesk.AutoCAD.Windows.Data.InputSearchOptions
Inherits: Enum

  Values:
    - None = 0
    - AutoComplete = 1
    - AutoCorrect = 2
    - SysvarSearch = 4
    - ContentSearch = 8
    - MidString = 16

--------------------------------------------------------------------------------

[ENUM] IPEAttachmentPoint
Full Name: Autodesk.AutoCAD.Windows.Data.IPEAttachmentPoint
Inherits: Enum

  Values:
    - TopLeft = 1
    - TopCenter = 2
    - TopRight = 3
    - MiddleLeft = 4
    - MiddleCenter = 5
    - MiddleRight = 6
    - BottomLeft = 7
    - BottomCenter = 8
    - BottomRight = 9

--------------------------------------------------------------------------------

[ENUM] IPEColumnType
Full Name: Autodesk.AutoCAD.Windows.Data.IPEColumnType
Inherits: Enum

  Values:
    - NoColumns = 0
    - StaticColumns = 1
    - DynamicColumns = 2

--------------------------------------------------------------------------------

[CLASS] IPECommandBase
Full Name: Autodesk.AutoCAD.Windows.Data.IPECommandBase
Implements: ICommand

  METHODS:
    bool CanExecute(object parameter)
    void Execute(object parameter)
    void SetCanExecute(bool value)

  EVENTS:
    event EventHandler CanExecuteChanged

--------------------------------------------------------------------------------

[ENUM] IPEDynamicColumnType
Full Name: Autodesk.AutoCAD.Windows.Data.IPEDynamicColumnType
Inherits: Enum

  Values:
    - DynamicAutoHeight = 0
    - DynamicManualHeight = 1

--------------------------------------------------------------------------------

[ENUM] IPEEditorSettings
Full Name: Autodesk.AutoCAD.Windows.Data.IPEEditorSettings
Inherits: Enum

  Values:
    - AlwaysDisplayWysiwyg = 0
    - ShowToolbar = 1
    - ShowRuler = 2
    - OpaqueBackground = 3
    - CheckSpelling = 4

--------------------------------------------------------------------------------

[ENUM] IPEFlowAlignmentType
Full Name: Autodesk.AutoCAD.Windows.Data.IPEFlowAlignmentType
Inherits: Enum

  Values:
    - FlowBase = 0
    - FlowCenter = 1
    - FlowTop = 2

--------------------------------------------------------------------------------

[ENUM] IPENumberingType
Full Name: Autodesk.AutoCAD.Windows.Data.IPENumberingType
Inherits: Enum

  Values:
    - NumberingOff = 0
    - NumberingBullet = 1
    - NumberingNumber = 2
    - NumberingLetterLower = 3
    - NumberingLetterUpper = 4
    - NumberingLettered = 5

--------------------------------------------------------------------------------

[CLASS] IPENumberingTypeEnumConverter
Full Name: Autodesk.AutoCAD.Windows.Data.IPENumberingTypeEnumConverter
Inherits: EnumConverter

  CONSTRUCTORS:
    new IPENumberingTypeEnumConverter(Type type)

  METHODS:
    bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)

--------------------------------------------------------------------------------

[ENUM] IPEParagraphAlignmentType
Full Name: Autodesk.AutoCAD.Windows.Data.IPEParagraphAlignmentType
Inherits: Enum

  Values:
    - AlignmentDefault = 0
    - AlignmentLeft = 1
    - AlignmentCenter = 2
    - AlignmentRight = 3
    - AlignmentJustify = 4
    - AlignmentDistribute = 5
    - NumAlignmentTypes = 6

--------------------------------------------------------------------------------

[ENUM] IPEStaticColumnType
Full Name: Autodesk.AutoCAD.Windows.Data.IPEStaticColumnType
Inherits: Enum

  Values:
    - OneColumn = 1
    - TwoColumns = 2
    - ThreeColumns = 3
    - FourColumns = 4
    - FiveColumns = 5
    - SixColumns = 6

--------------------------------------------------------------------------------

[CLASS] IsoDraft
Full Name: Autodesk.AutoCAD.Windows.Data.IsoDraft
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new IsoDraft()

  PROPERTIES:
    short State { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] LayerFilterCollection
Full Name: Autodesk.AutoCAD.Windows.Data.LayerFilterCollection
Inherits: NamedObjectCollection

--------------------------------------------------------------------------------

[CLASS] LayerRecordCollection
Full Name: Autodesk.AutoCAD.Windows.Data.LayerRecordCollection
Inherits: DbObjectCollection

--------------------------------------------------------------------------------

[CLASS] LightEngine
Full Name: Autodesk.AutoCAD.Windows.Data.LightEngine
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new LightEngine()

  PROPERTIES:
    double Date { get; set; }
    DoubleCollection DateTicks { get; }
    bool IsDateTimeEnabled { get; set; }
    bool IsUsingGenericLightingUnits { get; }
    double MaxDate { get; }
    double MaxTime { get; }
    double MinDate { get; }
    double MinTime { get; }
    double Time { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] LineSpacingMultiplesCommand
Full Name: Autodesk.AutoCAD.Windows.Data.LineSpacingMultiplesCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new LineSpacingMultiplesCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[ENUM] LineSpacingTypes
Full Name: Autodesk.AutoCAD.Windows.Data.LineSpacingTypes
Inherits: Enum

  Values:
    - LineSpacingExactly = 1
    - LineSpacingAtLeast = 2
    - LineSpacingMultiple = 3

--------------------------------------------------------------------------------

[CLASS] LineSpacingTypesEnumConverter
Full Name: Autodesk.AutoCAD.Windows.Data.LineSpacingTypesEnumConverter
Inherits: EnumConverter

  CONSTRUCTORS:
    new LineSpacingTypesEnumConverter(Type type)

  METHODS:
    bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)

--------------------------------------------------------------------------------

[CLASS] LineWeightCollection
Full Name: Autodesk.AutoCAD.Windows.Data.LineWeightCollection
Inherits: EnumItemCollection

--------------------------------------------------------------------------------

[CLASS] LowercaseCommand
Full Name: Autodesk.AutoCAD.Windows.Data.LowercaseCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new LowercaseCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] MoreLineSpaceCommand
Full Name: Autodesk.AutoCAD.Windows.Data.MoreLineSpaceCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new MoreLineSpaceCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] NamedObjectCollection
Full Name: Autodesk.AutoCAD.Windows.Data.NamedObjectCollection
Inherits: DataItemCollection

--------------------------------------------------------------------------------

[CLASS] NativeFunction
Full Name: Autodesk.AutoCAD.Windows.Data.NativeFunction
Implements: ICommand, IDisposable

  METHODS:
    bool CanExecute(object parameter)
    void Dispose()
    void Execute(object parameter)

  EVENTS:
    event EventHandler CanExecuteChanged

--------------------------------------------------------------------------------

[ENUM] NavStatus
Full Name: Autodesk.AutoCAD.Windows.Data.NavStatus
Inherits: Enum

  Values:
    - None = 0
    - Paused = 1
    - Recording = 2
    - Default = 4

--------------------------------------------------------------------------------

[CLASS] NotifyCollectionItemsChangedEventArgs
Full Name: Autodesk.AutoCAD.Windows.Data.NotifyCollectionItemsChangedEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new NotifyCollectionItemsChangedEventArgs(IList items)

  PROPERTIES:
    IList Items { get; }

--------------------------------------------------------------------------------

[CLASS] NotifyCollectionItemsChangedEventHandler
Full Name: Autodesk.AutoCAD.Windows.Data.NotifyCollectionItemsChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new NotifyCollectionItemsChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, NotifyCollectionItemsChangedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, NotifyCollectionItemsChangedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] NumberingCommand
Full Name: Autodesk.AutoCAD.Windows.Data.NumberingCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new NumberingCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] OptionsCommand
Full Name: Autodesk.AutoCAD.Windows.Data.OptionsCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new OptionsCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] ParagraphAlignmentOpionsCommand
Full Name: Autodesk.AutoCAD.Windows.Data.ParagraphAlignmentOpionsCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new ParagraphAlignmentOpionsCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] ParagraphDialogCommand
Full Name: Autodesk.AutoCAD.Windows.Data.ParagraphDialogCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new ParagraphDialogCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[ENUM] PatPatternCategory
Full Name: Autodesk.AutoCAD.Windows.Data.PatPatternCategory
Inherits: Enum

  Values:
    - kPredef = 0
    - kUserdef = 1
    - kCustomdef = 2
    - kSolid_fill = 3
    - kISOdef = 4
    - kInvalid = -1

--------------------------------------------------------------------------------

[CLASS] PlotStyleCollection
Full Name: Autodesk.AutoCAD.Windows.Data.PlotStyleCollection
Inherits: DbObjectCollection

--------------------------------------------------------------------------------

[CLASS] PromptAndInput
Full Name: Autodesk.AutoCAD.Windows.Data.PromptAndInput
Implements: INotifyPropertyChanged

  PROPERTIES:
    InputBuffer InputBuffer { get; }
    int KeywordsStringLength { get; }
    int KeywordsStringStart { get; }
    int Position { get; }
    string Prompt { get; }
    string PromptDefault { get; }
    ObservableCollection<string> PromptKeywords { get; }
    int SelectPosition { get; }
    string Text { get; }

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged
    event EventHandler Canceled
    event EventHandler Accepted

--------------------------------------------------------------------------------

[CLASS] RedoCommand
Full Name: Autodesk.AutoCAD.Windows.Data.RedoCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new RedoCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

