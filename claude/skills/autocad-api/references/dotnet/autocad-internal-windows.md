# Autodesk.AutoCAD.Internal.Windows

NAMESPACE: Autodesk.AutoCAD.Internal.Windows
--------------------------------------------------------------------------------

[CLASS] ClicFixedUi
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicFixedUi

  CONSTRUCTORS:
    new ClicFixedUi(adlsdkFixedControl* pFixedControl)

  PROPERTIES:
    string Avatar { get; }
    bool Display { get; }
    bool Enabled { get; }
    uint ItemCount { get; }
    List<ClicMenuUi> MenuItems { get; }
    ClicUiStyle MenuStyle { get; }
    ClicUiStyle Style { get; }
    string Text { get; }

--------------------------------------------------------------------------------

[CLASS] ClicMenuUi
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicMenuUi

  CONSTRUCTORS:
    new ClicMenuUi(adlsdkUiMenuItem* pMenuItem)

  PROPERTIES:
    byte[] Action { get; }
    bool Enabled { get; }
    string Text { get; }

--------------------------------------------------------------------------------

[CLASS] ClicTemporalUi
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicTemporalUi

  CONSTRUCTORS:
    new ClicTemporalUi(adlsdkTemporalControl* pTemporalControl)

  PROPERTIES:
    byte[] Action { get; }
    string Badge { get; }
    bool Display { get; }
    bool Enabled { get; }
    ClicUiStyle Style { get; }
    string Text { get; }

--------------------------------------------------------------------------------

[CLASS] ClicUi
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicUi

  METHODS:
    static ClicUi Instance()
    void SendUiAction(byte[] action, int left, int top, int right, int bottom)

  EVENTS:
    event ClicUiDisplayEventHandler ClicUiDisplay

--------------------------------------------------------------------------------

[CLASS] ClicUiDisplayEventHandler
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicUiDisplayEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ClicUiDisplayEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ClicUiEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ClicUiEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ClicUiElementColors
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicUiElementColors

  CONSTRUCTORS:
    new ClicUiElementColors(adlsdkUiElementColors* pUiElementColors)

  PROPERTIES:
    byte[] BackgroundColor { get; }
    byte[] BorderColor { get; }
    byte[] FontColor { get; }

--------------------------------------------------------------------------------

[CLASS] ClicUiEventArgs
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicUiEventArgs
Inherits: EventArgs

  PROPERTIES:
    ClicFixedUi FixedUi { get; }
    ClicTemporalUi TemporalUi { get; }

--------------------------------------------------------------------------------

[CLASS] ClicUiStyle
Full Name: Autodesk.AutoCAD.Internal.Windows.ClicUiStyle

  CONSTRUCTORS:
    new ClicUiStyle(adlsdkUiStyle* pUiStyle)

  PROPERTIES:
    bool FontBold { get; }
    string FontFamily { get; }
    bool FontItalic { get; }
    uint FontSize { get; }
    ClicUiElementColors Hover { get; }
    ClicUiElementColors Normal { get; }

--------------------------------------------------------------------------------

[CLASS] CommandThroat
Full Name: Autodesk.AutoCAD.Internal.Windows.CommandThroat

  CONSTRUCTORS:
    new CommandThroat()

  EVENTS:
    event InputCharactersQueuedEventHandler InputCharactersQueued

--------------------------------------------------------------------------------

[CLASS] CommandToolTipData
Full Name: Autodesk.AutoCAD.Internal.Windows.CommandToolTipData

  CONSTRUCTORS:
    new CommandToolTipData()

  PROPERTIES:
    string BasicContent { get; set; }
    string CLICommand { get; set; }
    bool EnableHelp { get; set; }
    string ExtendedContentKey { get; set; }
    string ExtendedContentSource { get; set; }
    string HelpSource { get; set; }
    string HelpTopic { get; set; }
    string Macro { get; set; }
    string Shortcut { get; set; }
    string Title { get; set; }
    string UserTag { get; set; }

--------------------------------------------------------------------------------

[CLASS] InputCharactersQueuedEventArgs
Full Name: Autodesk.AutoCAD.Internal.Windows.InputCharactersQueuedEventArgs
Inherits: EventArgs

  PROPERTIES:
    string Characters { get; }

--------------------------------------------------------------------------------

[CLASS] InputCharactersQueuedEventHandler
Full Name: Autodesk.AutoCAD.Internal.Windows.InputCharactersQueuedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InputCharactersQueuedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, InputCharactersQueuedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, InputCharactersQueuedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] MenuGroupData
Full Name: Autodesk.AutoCAD.Internal.Windows.MenuGroupData
Implements: IDisposable

  CONSTRUCTORS:
    new MenuGroupData(MenuGroupTypeEnum nMenuGroupType, string sName, string sFileName, AcMenuGroupData* pMenuGroupData)

  PROPERTIES:
    static string DefaultToolTipHelpSource { get; }
    string FileName { get; }
    MenuGroupTypeEnum MenuGroupType { get; }
    string Name { get; }
    List<MenuGroupData> PartialMenus { get; }

  METHODS:
    void Dispose()
    Dictionary<string, RibbonMenuMacro> GetMenuMacro()
    RibbonMenuMacro GetMenuMacro(string sId)

--------------------------------------------------------------------------------

[ENUM] MenuGroupTypeEnum
Full Name: Autodesk.AutoCAD.Internal.Windows.MenuGroupTypeEnum
Inherits: Enum

  Values:
    - Enterprise = 0
    - Main = 1
    - Partial = 2

--------------------------------------------------------------------------------

[CLASS] ProfileManager
Full Name: Autodesk.AutoCAD.Internal.Windows.ProfileManager

  CONSTRUCTORS:
    new ProfileManager()

  METHODS:
    static bool GetProfileNode(_com_ptr_t<_com_IIID<MSXML::IXMLDOMNode\,\&_GUID_2933bf80_7b36_11d2_b20e_00c04f983e60> >* pNode, Char* pszNodePath, bool bCreateIfNotExists, bool bCurrentProfile)
    static string LoadData(string sNodePath, string sWSName, bool bCurrentProfile)
    static bool LoadHideableDialogSettingsDictionary()
    static bool SaveData(string sXmlData, string sNodePath, string sWorkspaceName, bool bCurrentProfile)
    static bool SaveHideableDialogSettingsDictionary()

--------------------------------------------------------------------------------

[CLASS] RibbonData
Full Name: Autodesk.AutoCAD.Internal.Windows.RibbonData

  CONSTRUCTORS:
    new RibbonData()

  METHODS:
    static IntPtr CreateHwndControl(string sControlName, string sModule, IntPtr hwndParent)
    static IntPtr CreateInvalidHwndControl(IntPtr hWndParent)
    static Collection<Uri> GetDefaultToolTipContentSources()
    static List<MenuGroupData> GetMenuGroups()
    static string GetProfileStorageFilename()
    [SKIPPED METHOD] GetWindowSize: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] Load: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] Load: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string LoadXml(string sNodeName, string sWSName)
    static bool SaveXml(string sData, string sWSName)

--------------------------------------------------------------------------------

[CLASS] RibbonMenuMacro
Full Name: Autodesk.AutoCAD.Internal.Windows.RibbonMenuMacro

  CONSTRUCTORS:
    new RibbonMenuMacro(AcMenuMacroData* pAcMenuMacroData)

  PROPERTIES:
    string CLICommand { get; }
    string Command { get; }
    string ElementID { get; }
        [SKIPPED PROPERTY] LargeImage: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    string LargeImageStr { get; }
    string Name { get; }
    string Shortcut { get; }
        [SKIPPED PROPERTY] SmallImage: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    string SmallImageStr { get; }
    CommandToolTipData ToolTipData { get; }
    string UserData { get; }
    string UserTag { get; }

--------------------------------------------------------------------------------

