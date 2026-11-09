# Autodesk.AutoCAD.Publishing

NAMESPACE: Autodesk.AutoCAD.Publishing
--------------------------------------------------------------------------------

[CLASS] AboutToBeginBackgroundPublishingEventArgs
Full Name: Autodesk.AutoCAD.Publishing.AboutToBeginBackgroundPublishingEventArgs
Inherits: EventArgs

  PROPERTIES:
    DsdData DsdData { get; }
    bool JobWillPublishInBackground { get; }

  METHODS:
    IDictionaryEnumerator ReadPrivateSection(string sectionName)
    void WritePrivateSection(string sectionName, IDictionaryEnumerator data)

--------------------------------------------------------------------------------

[CLASS] AboutToBeginBackgroundPublishingEventHandler
Full Name: Autodesk.AutoCAD.Publishing.AboutToBeginBackgroundPublishingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new AboutToBeginBackgroundPublishingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, AboutToBeginBackgroundPublishingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, AboutToBeginBackgroundPublishingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] AboutToBeginPublishingEventArgs
Full Name: Autodesk.AutoCAD.Publishing.AboutToBeginPublishingEventArgs
Inherits: EventArgs

  PROPERTIES:
    DsdData DsdData { get; }
    bool JobWillPublishInBackground { get; }
    PlotLogger PlotLogger { get; }

  METHODS:
    IDictionaryEnumerator ReadPrivateSection(string sectionName)
    void WritePrivateSection(string sectionName, IDictionaryEnumerator data)

--------------------------------------------------------------------------------

[CLASS] AboutToBeginPublishingEventHandler
Full Name: Autodesk.AutoCAD.Publishing.AboutToBeginPublishingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new AboutToBeginPublishingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, AboutToBeginPublishingEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, AboutToBeginPublishingEventArgs e)

--------------------------------------------------------------------------------

[CLASS] AboutToEndPublishingEventHandler
Full Name: Autodesk.AutoCAD.Publishing.AboutToEndPublishingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new AboutToEndPublishingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishEventArgs e)

--------------------------------------------------------------------------------

[CLASS] AboutToMoveFileEventHandler
Full Name: Autodesk.AutoCAD.Publishing.AboutToMoveFileEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new AboutToMoveFileEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginAggregationEventArgs
Full Name: Autodesk.AutoCAD.Publishing.BeginAggregationEventArgs
Inherits: EventArgs

  PROPERTIES:
    string DwfFileName { get; }
    string DwfPassword { get; }
    PlotLogger PlotLogger { get; }

  METHODS:
    void AddGlobalPropertyRange(EPlotProperty[] properties)
    void AddGlobalResourceRange(EPlotResource[] resources)

--------------------------------------------------------------------------------

[CLASS] BeginAggregationEventHandler
Full Name: Autodesk.AutoCAD.Publishing.BeginAggregationEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginAggregationEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginAggregationEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginAggregationEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginEntityEventHandler
Full Name: Autodesk.AutoCAD.Publishing.BeginEntityEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginEntityEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishEntityEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishEntityEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginPublishingSheetEventArgs
Full Name: Autodesk.AutoCAD.Publishing.BeginPublishingSheetEventArgs
Inherits: EventArgs

  PROPERTIES:
    DsdEntry DsdEntry { get; }
    PlotLogger PlotLogger { get; }
    string UniqueId { get; }

--------------------------------------------------------------------------------

[CLASS] BeginPublishingSheetEventHandler
Full Name: Autodesk.AutoCAD.Publishing.BeginPublishingSheetEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginPublishingSheetEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginPublishingSheetEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginPublishingSheetEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginSheetEventHandler
Full Name: Autodesk.AutoCAD.Publishing.BeginSheetEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginSheetEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishSheetEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishSheetEventArgs e)

--------------------------------------------------------------------------------

[CLASS] CancelledOrFailedPublishingEventHandler
Full Name: Autodesk.AutoCAD.Publishing.CancelledOrFailedPublishingEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new CancelledOrFailedPublishingEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishEventArgs e)

--------------------------------------------------------------------------------

[CLASS] Dwf3dNavigationTreeNode
Full Name: Autodesk.AutoCAD.Publishing.Dwf3dNavigationTreeNode
Inherits: RXObject

  CONSTRUCTORS:
    new Dwf3dNavigationTreeNode(string displayName, bool isGroup, bool isBlock)
    new Dwf3dNavigationTreeNode()

  PROPERTIES:
    Dwf3dNavigationTreeNodeCollection Children { get; }
    string DisplayName { get; set; }
    bool IsBlock { get; set; }
    bool IsGroup { get; set; }

  METHODS:
    int[] GetKeys()
    void SetKeys(int[] keys)

--------------------------------------------------------------------------------

[CLASS] Dwf3dNavigationTreeNodeCollection
Full Name: Autodesk.AutoCAD.Publishing.Dwf3dNavigationTreeNodeCollection

  PROPERTIES:
    int Count { get; }
    Dwf3dNavigationTreeNode Item { get; }

  METHODS:
    void Add(Dwf3dNavigationTreeNode node)
    void Remove(Dwf3dNavigationTreeNode node)

--------------------------------------------------------------------------------

[CLASS] DwfNode
Full Name: Autodesk.AutoCAD.Publishing.DwfNode

  CONSTRUCTORS:
    new DwfNode(int nodeId, string nodeName)
    new DwfNode()

  PROPERTIES:
    int NodeId { get; set; }
    string NodeName { get; set; }

--------------------------------------------------------------------------------

[CLASS] EndEntityEventHandler
Full Name: Autodesk.AutoCAD.Publishing.EndEntityEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndEntityEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishEntityEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishEntityEventArgs e)

--------------------------------------------------------------------------------

[CLASS] EndPublishEventHandler
Full Name: Autodesk.AutoCAD.Publishing.EndPublishEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndPublishEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishEventArgs e)

--------------------------------------------------------------------------------

[CLASS] EndSheetEventHandler
Full Name: Autodesk.AutoCAD.Publishing.EndSheetEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new EndSheetEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishSheetEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishSheetEventArgs e)

--------------------------------------------------------------------------------

[CLASS] EPlotAttribute
Full Name: Autodesk.AutoCAD.Publishing.EPlotAttribute

  CONSTRUCTORS:
    new EPlotAttribute(string ns, string nsUrl, string name, string value)
    new EPlotAttribute()

  PROPERTIES:
    string Name { get; set; }
    string Ns { get; set; }
    string NsUrl { get; set; }
    string Value { get; set; }

--------------------------------------------------------------------------------

[CLASS] EPlotAttributeCollection
Full Name: Autodesk.AutoCAD.Publishing.EPlotAttributeCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    EPlotAttribute Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    void Add(EPlotAttribute value)
    void Clear()
    void CopyTo(Array array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] EPlotProperty
Full Name: Autodesk.AutoCAD.Publishing.EPlotProperty

  CONSTRUCTORS:
    new EPlotProperty(string name, string val)
    new EPlotProperty()

  PROPERTIES:
    EPlotAttributeCollection Attributes { get; }
    string Category { get; set; }
    string Name { get; set; }
    string Type { get; set; }
    string Units { get; set; }
    string Value { get; set; }

  METHODS:
    void AddEplotAttribute(EPlotAttribute att)
    void AddEPlotAttribute(string ns, string nsUrl, string attName, string attValue)

--------------------------------------------------------------------------------

[CLASS] EPlotPropertyBag
Full Name: Autodesk.AutoCAD.Publishing.EPlotPropertyBag

  CONSTRUCTORS:
    new EPlotPropertyBag(string id, string namespaceUrl, string namespaceLocation)
    new EPlotPropertyBag()

  PROPERTIES:
    string Id { get; set; }
    string NamespaceLocation { get; set; }
    string NamespaceUrl { get; set; }
    EPlotPropertyCollection Properties { get; }
    StringCollection References { get; set; }

--------------------------------------------------------------------------------

[CLASS] EPlotPropertyCollection
Full Name: Autodesk.AutoCAD.Publishing.EPlotPropertyCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    EPlotProperty Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    void Add(EPlotProperty value)
    void Clear()
    void CopyTo(Array array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] EPlotResource
Full Name: Autodesk.AutoCAD.Publishing.EPlotResource

  CONSTRUCTORS:
    new EPlotResource(string role, string mime, string path)
    new EPlotResource()

  PROPERTIES:
    string Mime { get; set; }
    string Path { get; set; }
    string Role { get; set; }

--------------------------------------------------------------------------------

[CLASS] InitPublishOptionsDialogEventHandler
Full Name: Autodesk.AutoCAD.Publishing.InitPublishOptionsDialogEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new InitPublishOptionsDialogEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, PublishUIEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, PublishUIEventArgs e)

--------------------------------------------------------------------------------

[STRUCT] OptionsDialogResult
Full Name: Autodesk.AutoCAD.Publishing.OptionsDialogResult

  PROPERTIES:
    DsdData DsdData { get; }
    PlotConfig PlotConfig { get; }
    Nullable<bool> Status { get; }

--------------------------------------------------------------------------------

[CLASS] PublishEntityEventArgs
Full Name: Autodesk.AutoCAD.Publishing.PublishEntityEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId EffectiveBlockLayerId { get; }
    Entity Entity { get; }
    bool IsCancelled { get; }
    PlotLogger PlotLogger { get; }
    string UniqueEntityId { get; }

  METHODS:
    bool Add3DDwfProperty(string category, string name, string value)
    bool AddNodeToMap(ObjectId entityId, ObjectIdCollection objIds, int nodeId)
    bool AddPropertiesIds(EPlotPropertyBag properties, DwfNode node)
    bool AddPropertyBag(EPlotPropertyBag properties)
    void Cancel()
    void Flush()
    DwfNode GetCurrentEntityNode(ObjectIdCollection objIds)
    ObjectIdCollection getEntityBlockRefPath()
    int GetEntityNode(ObjectId entityId, ObjectIdCollection objIds)
    int[] GetGraphicIDs()
    int GetNextAvailableNode()
    DwfNode GetNode(int nodeId)
    bool SetCurrentNode(int nodeId, ObjectIdCollection objIds)
    bool SetNodeName(int nodeId, string nodeName)

--------------------------------------------------------------------------------

[CLASS] Publisher
Full Name: Autodesk.AutoCAD.Publishing.Publisher
Inherits: MarshalByRefObject

  PROPERTIES:
    string CurrentPublishedSheetSetPath { get; }

  METHODS:
    void FireAboutToBeginBackgroundPublishing(AboutToBeginBackgroundPublishingEventArgs e)
    void FireAboutToBeginPublishing(AboutToBeginPublishingEventArgs e)
    void FireAboutToEndPublishing(PublishEventArgs e)
    void FireAboutToMoveFile(PublishEventArgs e)
    void FireBeginAggregation(BeginAggregationEventArgs e)
    void FireBeginEntity(PublishEntityEventArgs e)
    void FireBeginPublishingSheet(BeginPublishingSheetEventArgs e)
    void FireBeginSheet(PublishSheetEventArgs e)
    void FireCancelledOrFailedPublishing(PublishEventArgs e)
    void FireEndEntity(PublishEntityEventArgs e)
    void FireEndPublish(PublishEventArgs e)
    void FireEndSheet(PublishSheetEventArgs e)
    void FireInitPublishOptionsDialog(PublishUIEventArgs e)
    int PublishDsd(string dsdFile, PlotProgressDialog plotProgressDialog)
    void PublishExecute(DsdData dsdData, PlotConfig plotConfig)
    void PublishSelectedLayouts(bool bOverride)
    OptionsDialogResult ShowDwfOptionsDialog(DsdData dsdData, PlotConfig plotConfig, string optionsDialogTitle)
    void ShowPublishDialog(DsdData dsdData, PlotConfig plotConfig, StringCollection namePageSetups, StringCollection namePageSetupPaths, string optionsDialogTitle, string optionsButtonText)

  EVENTS:
    event EndSheetEventHandler EndSheet
    event EndEntityEventHandler EndEntity
    event BeginEntityEventHandler BeginEntity
    event BeginSheetEventHandler BeginSheet
    event InitPublishOptionsDialogEventHandler InitPublishOptionsDialog
    event CancelledOrFailedPublishingEventHandler CancelledOrFailedPublishing
    event EndPublishEventHandler EndPublish
    event AboutToMoveFileEventHandler AboutToMoveFile
    event AboutToEndPublishingEventHandler AboutToEndPublishing
    event BeginAggregationEventHandler BeginAggregation
    event BeginPublishingSheetEventHandler BeginPublishingSheet
    event AboutToBeginPublishingEventHandler AboutToBeginPublishing
    event AboutToBeginBackgroundPublishingEventHandler AboutToBeginBackgroundPublishing

--------------------------------------------------------------------------------

[CLASS] PublishEventArgs
Full Name: Autodesk.AutoCAD.Publishing.PublishEventArgs
Inherits: EventArgs

  PROPERTIES:
    string DwfFileName { get; }
    string DwfPassword { get; }
    bool IsMultiSheetDwf { get; }
    string TemporaryDwfFileName { get; }

--------------------------------------------------------------------------------

[CLASS] PublishSheetEventArgs
Full Name: Autodesk.AutoCAD.Publishing.PublishSheetEventArgs
Inherits: EventArgs

  PROPERTIES:
    bool AreLinesHidden { get; }
    bool ArePlottingLineWeights { get; }
    bool AreScalingLineWeights { get; }
    string CanonicalMediaName { get; }
    string Configuration { get; }
    Database Database { get; }
    double DisplayMaxX { get; }
    double DisplayMaxY { get; }
    double DisplayMinX { get; }
    double DisplayMinY { get; }
    double DrawingScale { get; }
    Dwf3dNavigationTreeNode Dwf3dNavigationTreeNode { get; set; }
    double EffectivePlotOffsetX { get; }
    int EffectivePlotOffsetXDevice { get; }
    double EffectivePlotOffsetY { get; }
    int EffectivePlotOffsetYDevice { get; }
    bool IsModelLayout { get; }
    bool IsPlotJobCancelled { get; }
    bool IsScaleSpecified { get; }
    double LayoutBoundsMaxX { get; }
    double LayoutBoundsMaxY { get; }
    double LayoutBoundsMinX { get; }
    double LayoutBoundsMinY { get; }
    double LayoutMarginMaxX { get; }
    double LayoutMarginMaxY { get; }
    double LayoutMarginMinX { get; }
    double LayoutMarginMinY { get; }
    double MaxBoundsX { get; }
    double MaxBoundsY { get; }
    double OriginX { get; }
    double OriginY { get; }
    double PaperScale { get; }
    double PlotBoundsMaxX { get; }
    double PlotBoundsMaxY { get; }
    double PlotBoundsMinX { get; }
    double PlotBoundsMinY { get; }
    ObjectId PlotLayoutId { get; }
    PlotLogger PlotLogger { get; }
    PlotPaperUnit PlotPaperUnit { get; }
    PlotRotation PlotRotation { get; }
    string PlotToFileName { get; }
    string PlotToFilePath { get; }
    PlotType PlotType { get; }
    double PlotWindowMaxX { get; }
    double PlotWindowMaxY { get; }
    double PlotWindowMinX { get; }
    double PlotWindowMinY { get; }
    double PrintableBoundsX { get; }
    double PrintableBoundsY { get; }
    bool PublishingTo3DDwf { get; }
    double StepsPerInch { get; }
    string UniqueLayoutId { get; }
    string ViewPlotted { get; }

  METHODS:
    void AddPagePropertyRange(EPlotProperty[] properties)
    void AddPageResourceRange(EPlotResource[] resources)

--------------------------------------------------------------------------------

[CLASS] PublishUIEventArgs
Full Name: Autodesk.AutoCAD.Publishing.PublishUIEventArgs
Inherits: EventArgs

  PROPERTIES:
    DsdData DsdData { get; }
    IUnknown** IUnknown { get; }
    bool JobWillPublishInBackground { get; }

  METHODS:
    IDictionaryEnumerator ReadPrivateSection(string sectionName)
    void WritePrivateSection(string sectionName, IDictionaryEnumerator data)

--------------------------------------------------------------------------------

