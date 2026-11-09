# Autodesk.AutoCAD.Windows.Data

NAMESPACE: Autodesk.AutoCAD.Windows.Data
--------------------------------------------------------------------------------

[CLASS] Selection
Full Name: Autodesk.AutoCAD.Windows.Data.Selection
Inherits: DbObjectCollection

  PROPERTIES:
    CommonProperties CommonProperties { get; }
    bool IsContinuing { get; set; }

  METHODS:
    bool ContainsAny(params string[] types)
    bool ContainsAnyDrawingView()
    bool ContainsOnly(params string[] types)
    bool ContainsOnlyCenterTypeEntities()
    bool ContainsOnlyCoordinationModel()
    bool ContainsOnlyDrawingView()
    bool ContainsOnlyGeoMapImage()
    bool ContainsOnlyRevcloudEntities()
    bool ContainsOnlyTrace()
    bool isContainSectionViewHatch()
    bool isOnlySectionViewHatchs()
    int RemoveNonEditableObjects(NonEditableType type)

--------------------------------------------------------------------------------

[CLASS] StandardUcsPlane
Full Name: Autodesk.AutoCAD.Windows.Data.StandardUcsPlane

  PROPERTIES:
    static object Back { get; }
    static object Bottom { get; }
    static object Front { get; }
    static object Left { get; }
    static object Right { get; }
    static object Top { get; }
    static object Unnamed { get; }
    static object World { get; }

--------------------------------------------------------------------------------

[CLASS] StaticColumnsCommand
Full Name: Autodesk.AutoCAD.Windows.Data.StaticColumnsCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new StaticColumnsCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] SubEntity
Full Name: Autodesk.AutoCAD.Windows.Data.SubEntity
Implements: IDisposable

  PROPERTIES:
    Type ParentType { get; }

  METHODS:
    static SubEntity Create(FullSubentityPath path)
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] SystemVariable
Full Name: Autodesk.AutoCAD.Windows.Data.SystemVariable
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    bool IsEnabled { get; }
    double Maximum { get; }
    double Minimum { get; }
    object MutedValue { get; set; }
    string Name { get; }
    TrueValues TrueValues { get; }
    object Value { get; set; }
    Type ValueType { get; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] SysvarMonitorState
Full Name: Autodesk.AutoCAD.Windows.Data.SysvarMonitorState
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new SysvarMonitorState()

  PROPERTIES:
    short DifferenceNumber { get; }
    bool HasDifference { get; }
    bool ShowBalloon { get; }

  METHODS:
    void Dispose()
    void ShowDialog()
    void SyncState()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] TextSize
Full Name: Autodesk.AutoCAD.Windows.Data.TextSize
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    ObservableCollection<string> FontSizes { get; }
    object TextHeight { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] ThemeEngine
Full Name: Autodesk.AutoCAD.Windows.Data.ThemeEngine
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new ThemeEngine()

  PROPERTIES:
    double Brightness { get; }
    double Hue { get; }
    double Saturation { get; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[STRUCT] TransparencyItem
Full Name: Autodesk.AutoCAD.Windows.Data.TransparencyItem

  CONSTRUCTORS:
    new TransparencyItem(Transparency transparency, string ErrorText)

  PROPERTIES:
    string ErrorText { get; }
    int LayerValue { get; }
    Transparency Transparency { get; }

--------------------------------------------------------------------------------

[CLASS] TrueValues
Full Name: Autodesk.AutoCAD.Windows.Data.TrueValues
Implements: INotifyPropertyChanged

  PROPERTIES:
    Color AcadColor { get; set; }
    string HPBACKGROUNDCOLOR { get; set; }
    string HPCOLOR { get; set; }
    string StringValue { get; set; }

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] UIFontInfo
Full Name: Autodesk.AutoCAD.Windows.Data.UIFontInfo

  CONSTRUCTORS:
    new UIFontInfo(string fontName, bool isTrueType)

  PROPERTIES:
    bool IsTrueType { get; }
    string Name { get; }

  METHODS:
    static UIFontInfo GetFontInfo(string fontName)
    static Collection<UIFontInfo> GetFontInfoCollection()

--------------------------------------------------------------------------------

[CLASS] UndoCommand
Full Name: Autodesk.AutoCAD.Windows.Data.UndoCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new UndoCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] UppercaseCommand
Full Name: Autodesk.AutoCAD.Windows.Data.UppercaseCommand
Inherits: IPECommandBase

  CONSTRUCTORS:
    new UppercaseCommand()

  METHODS:
    void Execute(object parameter)

--------------------------------------------------------------------------------

[CLASS] View2DRibbonItemData
Full Name: Autodesk.AutoCAD.Windows.Data.View2DRibbonItemData
Implements: IDisposable

  CONSTRUCTORS:
    new View2DRibbonItemData(int id, Char* text, Char* iconkey, uint id2)
    new View2DRibbonItemData()

  PROPERTIES:
    string Key { get; }
    string Text { get; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] Views2DCommands
Full Name: Autodesk.AutoCAD.Windows.Data.Views2DCommands
Implements: INotifyPropertyChanged, IDisposable

  PROPERTIES:
    bool AutoUpdate { get; set; }
    bool CircularRbt { get; set; }
    bool CutInheritance_1 { get; set; }
    bool DeferUpdate { get; set; }
    double Depth { get; set; }
    object DepthType { get; set; }
    ObservableCollection<View2DRibbonItemData> DepthTypeList { get; }
    object DesignView { get; set; }
    ObservableCollection<string> DesignViewList { get; }
    object DetailLevel { get; set; }
    ObservableCollection<string> DetailLevelList { get; }
    string Identifier { get; set; }
    int IsEmptyMS { get; }
    bool IsEnabledAccept { get; }
    bool IsEnabledAutoUpdate { get; }
    bool IsEnabledBoundaryRbt { get; }
    bool IsEnabledCancel { get; }
    bool IsEnabledCutInheritance { get; }
    bool IsEnabledCutInheritance1 { get; }
    bool IsEnabledDeferUpdate { get; }
    bool IsEnabledDepth { get; }
    bool IsEnabledDepthDrag { get; }
    bool IsEnabledDepthType { get; }
    bool IsEnabledDesignView { get; }
    bool IsEnabledDetailLevel { get; }
    bool IsEnabledIdentifier { get; }
    bool IsEnabledMember { get; }
    bool IsEnabledModelEdgeRbt { get; }
    bool IsEnabledMove { get; }
    bool IsEnabledOption { get; }
    bool IsEnabledOrientation { get; }
    bool IsEnabledPosRepresentation { get; }
    bool IsEnabledPresentation { get; }
    bool IsEnabledProjectionView { get; }
    bool IsEnabledScale { get; }
    bool IsEnabledSelect { get; }
    bool IsEnabledSheetMetal { get; }
    bool IsEnabledShowHatch { get; }
    bool IsEnabledSViewLabel { get; }
    bool IsEnabledViewStyle { get; }
    bool IsEnabledVisibility { get; }
    bool IsEnabledVisibility1 { get; }
    bool IsEnabledVisibility2 { get; }
    bool IsEnabledVisibility3 { get; }
    bool IsEnabledVisibility4 { get; }
    bool IsEnabledVisibility5 { get; }
    bool IsEnabledVisibility6 { get; }
    bool IsEnabledWeldment { get; }
    bool IsEnableGradientPatern { get; }
    bool IsEnableReturn { get; }
    bool IsSynergyObjServiceRegistered { get; }
    bool JaggedRbt { get; set; }
    object Member { get; set; }
    ObservableCollection<View2DRibbonItemData> MemberList { get; }
    object Orientation { get; set; }
    ObservableCollection<View2DRibbonItemData> OrientationList { get; }
    object PosRepresentation { get; set; }
    ObservableCollection<string> PosRepresentationList { get; }
    object Presentation { get; set; }
    ObservableCollection<string> PresentationList { get; }
    object ProjectionView { get; set; }
    ObservableCollection<View2DRibbonItemData> ProjectionViewList { get; }
    bool RectangularRbt { get; set; }
    object Scale { get; set; }
    ObservableCollection<string> ScaleList { get; }
    object SheetMetal { get; set; }
    ObservableCollection<View2DRibbonItemData> SheetMetalList { get; }
    bool ShowHatch { get; set; }
    bool SmoothRbt { get; set; }
    bool SmoothWBRbt { get; set; }
    bool SmoothWCRbt { get; set; }
        [SKIPPED PROPERTY] StyleObject: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    bool SViewLabel { get; set; }
    ICommand ViewAccept { get; }
    ICommand ViewBaseINV { get; }
    ICommand ViewBaseMS { get; }
    ICommand ViewCancel { get; }
    ICommand ViewMove { get; }
    ICommand ViewOption { get; }
    ICommand ViewReturn { get; }
    ICommand ViewSelect { get; }
    ICommand ViewSelectDepth { get; }
    short ViewStyle { get; set; }
    ICommand ViewStyleFB { get; }
    object ViewStyleFBImage { get; }
    string ViewStyleFBText { get; }
    ICommand ViewStyleSH { get; }
    ICommand ViewStyleSV { get; }
    ICommand ViewStyleWH { get; }
    ICommand ViewStyleWV { get; }
    ICommand ViewUpdateAll { get; }
    bool Visibility_1 { get; set; }
    bool Visibility_2 { get; set; }
    bool Visibility_3 { get; set; }
    bool Visibility_4 { get; set; }
    bool Visibility_5 { get; set; }
    bool Visibility_6 { get; set; }
    object Weldment { get; set; }
    ObservableCollection<View2DRibbonItemData> WeldmentList { get; }

  METHODS:
    object ConvertDoubleToStr(object value, Type targetType, object parameter, CultureInfo culture)
    string convertItemData2Image(object item)
    object ConvertStrToDouble(object value, Type targetType, object parameter, CultureInfo culture)
    void Dispose()
    bool isVetoSetCurrentDetailViewStyle()
    bool isVetoSetCurrentSectionViewStyle()
    void showVetoWarning()
    bool vetoSetCurrentDetailViewStyle(object value)
    bool vetoSetCurrentSectionViewStyle(object value)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

