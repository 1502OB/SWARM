# Autodesk.AutoCAD.Windows.Data.Render.RapidRT

NAMESPACE: Autodesk.AutoCAD.Windows.Data.Render.RapidRT
--------------------------------------------------------------------------------

[CLASS] RenderData
Full Name: Autodesk.AutoCAD.Windows.Data.Render.RapidRT.RenderData
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new RenderData(Document A_0)

  PROPERTIES:
    bool AbortFlag { get; set; }
    int CurrentTileIndex { get; set; }
    bool IsIndeterminate { get; }
    bool IsRendering { get; }
    bool IsRenderOK { get; set; }
    bool IsViewportRender { get; }
    Nullable<int> JobIndex { get; set; }
    float LevelProgress { get; set; }
    string OutputFileName { get; set; }
    string OutputImageFormat { get; set; }
    string OutputResolution { get; set; }
    string OutputSize { get; }
    string OutputView { get; }
    float OverallProgress { get; set; }
    Document OwningDocument { get; }
    int RenderLevel { get; set; }
    string RenderPreset { get; }
    string RenderStatistics { get; set; }
    RenderImage RenderTileImage { get; set; }
    int RenderTime { get; set; }

  METHODS:
    void Dispose()
    void ReRender()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] RenderDataSet
Full Name: Autodesk.AutoCAD.Windows.Data.Render.RapidRT.RenderDataSet
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new RenderDataSet()

  PROPERTIES:
    RenderData CurrentRenderData { get; set; }
    RenderDataReactorImp* Reactor { get; set; }
    ObservableCollection<RenderData> RenderJobs { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] RenderEngine
Full Name: Autodesk.AutoCAD.Windows.Data.Render.RapidRT.RenderEngine
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new RenderEngine()

  PROPERTIES:
    string CurrentOutputFile { get; set; }
    string CurrentOutputSize { get; set; }
    RenderData CurrentRenderData { get; }
    RenderEnvironment CurrentRenderEnvironment { get; }
    RenderExposure CurrentRenderExposure { get; }
    bool IsRendering { get; }
    bool IsViewportChanging { get; set; }
    bool OutputSaveEnabled { get; set; }
    Dictionary<Document, RenderDataSet> RenderDataMap { get; }
    int RenderDestination { get; set; }
    bool SkipGlobalUpdate { get; set; }

  METHODS:
    void Dispose()
    void MakeRenderJobSettingsCurrent(Document doc, int jobIndex)
    void NotifyRenderEnd(Document doc)
    void NotifyRenderStart(Document doc, int jobIndex)
    void RegisterDocument(Document doc)
    void UnregisterDocument(Document doc)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] RenderEnvironment
Full Name: Autodesk.AutoCAD.Windows.Data.Render.RapidRT.RenderEnvironment
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new RenderEnvironment()

  PROPERTIES:
    Nullable<double> Brightness { get; set; }
    bool DisplayImage { get; set; }
    bool Enable { get; set; }
    Nullable<double> Exposure { get; set; }
    string IBLImageName { get; set; }
    double Rotation { get; set; }
    Nullable<double> WhitePoint { get; set; }

  METHODS:
    void Dispose()
    void SuppressDatabaseUpdate(bool newValue)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] RenderExposure
Full Name: Autodesk.AutoCAD.Windows.Data.Render.RapidRT.RenderExposure
Implements: INotifyPropertyChanged, IDisposable

  CONSTRUCTORS:
    new RenderExposure()

  PROPERTIES:
    bool Active { get; set; }
    double Exposure { get; set; }
    bool IBLActive { get; set; }
    ExposureType Type { get; }
    double WhitePoint { get; set; }

  METHODS:
    void Dispose()
    void InvalidateUI()
    void ResetDefault()

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[CLASS] RenderImage
Full Name: Autodesk.AutoCAD.Windows.Data.Render.RapidRT.RenderImage
Implements: IDisposable

  CONSTRUCTORS:
    new RenderImage()

  PROPERTIES:
    int Height { get; set; }
        [SKIPPED PROPERTY] Image: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    int Left { get; set; }
    int Top { get; set; }
    int Width { get; set; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

