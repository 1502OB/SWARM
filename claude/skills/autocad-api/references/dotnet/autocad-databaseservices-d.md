# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[ABSTRACT CLASS] DataAdapter
Full Name: Autodesk.AutoCAD.DatabaseServices.DataAdapter
Inherits: RXObject

  PROPERTIES:
    string DataAdapterId { get; }
    string Description { get; }
    string Name { get; }

  METHODS:
    LinkedData CreateDataObject(DataLink dataLink)
    ArrayList GetSourceFiles(DataLink dataLink, DataLinkGetSourceContext nContext)
    bool IsValid(DataLink dataLink)
    void RepathSourceFiles(DataLink dataLink, string sBasePath, PathOption nOption)
    void SourceFileModified(DataLink dataLink, string sModifiedFile, out UpdateAction pAction)
    void Update(DataLink dataLink, LinkedData data, UpdateDirection direction, UpdateOption option)

--------------------------------------------------------------------------------

[CLASS] DataAdapterManager
Full Name: Autodesk.AutoCAD.DatabaseServices.DataAdapterManager
Inherits: RXObject

  PROPERTIES:
    static int AdapterProviderCount { get; }

  METHODS:
    static DataAdapterProvider GetAdapterProvider(int index)
    static DataAdapter GetDataAdapter(string adapterId)
    static void RegisterAdapterProvider(DataAdapterProvider provider)
    static void UnregisterAdapterProvider(DataAdapterProvider provider)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DataAdapterProvider
Full Name: Autodesk.AutoCAD.DatabaseServices.DataAdapterProvider
Inherits: RXObject

  METHODS:
    DataAdapter GetDataAdapter(string adapterId)

--------------------------------------------------------------------------------

[CLASS] DataAdapterSourceFilesException
Full Name: Autodesk.AutoCAD.DatabaseServices.DataAdapterSourceFilesException
Inherits: Exception

  CONSTRUCTORS:
    new DataAdapterSourceFilesException(ErrorStatus es, string message, Exception innerException, ArrayList filelist)
    new DataAdapterSourceFilesException(ErrorStatus es, string message, ArrayList filelist)
    new DataAdapterSourceFilesException(ErrorStatus es, ArrayList filelist)
    new DataAdapterSourceFilesException(ArrayList filelist)

  PROPERTIES:
    ArrayList FileNameList { get; set; }

  METHODS:
    void GetObjectData(SerializationInfo info, StreamingContext context)

--------------------------------------------------------------------------------

[CLASS] Database
Full Name: Autodesk.AutoCAD.DatabaseServices.Database
Inherits: RXObject
Implements: IDynamicMetaObjectProvider

  CONSTRUCTORS:
    new Database(bool buildDefaultDrawing, bool noDocument)
    new Database()

  PROPERTIES:
    object AcadDatabase { get; }
    bool AllowExtendedNames { get; set; }
    double Angbase { get; set; }
    bool Angdir { get; set; }
    bool AnnoAllVisible { get; set; }
    bool AnnotativeDwg { get; set; }
    int ApproxNumObjects { get; }
    int Attmode { get; set; }
    int Aunits { get; set; }
    int Auprec { get; set; }
    ObjectId BlockTableId { get; }
    ObjectId ByBlockLinetype { get; }
    ObjectId ByLayerLinetype { get; }
    bool CameraDisplay { get; set; }
    double CameraHeight { get; set; }
    AnnotationScale Cannoscale { get; set; }
    Color Cecolor { get; set; }
    double Celtscale { get; set; }
    ObjectId Celtype { get; set; }
    LineWeight Celweight { get; set; }
    Transparency Cetransparency { get; set; }
    double Chamfera { get; set; }
    double Chamferb { get; set; }
    double Chamferc { get; set; }
    double Chamferd { get; set; }
    ObjectId Clayer { get; set; }
    ObjectId Cmaterial { get; set; }
    int Cmljust { get; set; }
    double Cmlscale { get; set; }
    ObjectId CmlstyleID { get; set; }
    ObjectId ColorDictionaryId { get; }
    ObjectId ContinuousLinetype { get; }
    int Cshadow { get; set; }
    ObjectId CurrentSpaceId { get; }
    ObjectId CurrentViewportTableRecordId { get; }
    ObjectId DataLinkDictionaryId { get; }
    DataLinkManager DataLinkManager { get; }
    ObjectId DetailViewStyle { get; set; }
    ObjectId DetailViewStyleDictionaryId { get; }
    int DgnFrame { get; set; }
    int Dimadec { get; set; }
    bool Dimalt { get; set; }
    int Dimaltd { get; set; }
    double Dimaltf { get; set; }
    double Dimaltrnd { get; set; }
    int Dimalttd { get; set; }
    int Dimalttz { get; set; }
    int Dimaltu { get; set; }
    int Dimaltz { get; set; }
    string Dimapost { get; set; }
    int Dimarcsym { get; set; }
    bool Dimaso { get; set; }
    int DimAssoc { get; set; }
    double Dimasz { get; set; }
    int Dimatfit { get; set; }
    int Dimaunit { get; set; }
    int Dimazin { get; set; }
    ObjectId Dimblk { get; set; }
    ObjectId Dimblk1 { get; set; }
    ObjectId Dimblk2 { get; set; }
    double Dimcen { get; set; }
    Color Dimclrd { get; set; }
    Color Dimclre { get; set; }
    Color Dimclrt { get; set; }
    int Dimdec { get; set; }
    double Dimdle { get; set; }
    double Dimdli { get; set; }
    char Dimdsep { get; set; }
    double Dimexe { get; set; }
    double Dimexo { get; set; }
    int Dimfrac { get; set; }
    double Dimfxlen { get; set; }
    bool DimfxlenOn { get; set; }
    double Dimgap { get; set; }
    double Dimjogang { get; set; }
    int Dimjust { get; set; }
    ObjectId Dimldrblk { get; set; }
    double Dimlfac { get; set; }
    bool Dimlim { get; set; }
    ObjectId Dimltex1 { get; set; }
    ObjectId Dimltex2 { get; set; }
    ObjectId Dimltype { get; set; }
    int Dimlunit { get; set; }
    LineWeight Dimlwd { get; set; }
    LineWeight Dimlwe { get; set; }
    string Dimpost { get; set; }
    double Dimrnd { get; set; }
    bool Dimsah { get; set; }
    double Dimscale { get; set; }
    bool Dimsd1 { get; set; }
    bool Dimsd2 { get; set; }
    bool Dimse1 { get; set; }
    bool Dimse2 { get; set; }
    bool Dimsho { get; set; }
    bool Dimsoxd { get; set; }
    ObjectId Dimstyle { get; set; }
    ObjectId DimStyleTableId { get; }
    int Dimtad { get; set; }
    int Dimtdec { get; set; }
    double Dimtfac { get; set; }
    int Dimtfill { get; set; }
    Color Dimtfillclr { get; set; }
    bool Dimtih { get; set; }
    bool Dimtix { get; set; }
    double Dimtm { get; set; }
    int Dimtmove { get; set; }
    bool Dimtofl { get; set; }
    bool Dimtoh { get; set; }
    bool Dimtol { get; set; }
    int Dimtolj { get; set; }
    double Dimtp { get; set; }
    double Dimtsz { get; set; }
    double Dimtvp { get; set; }
    ObjectId Dimtxsty { get; set; }
    double Dimtxt { get; set; }
    bool Dimtxtdirection { get; set; }
    int Dimtzin { get; set; }
    bool Dimupt { get; set; }
    int Dimzin { get; set; }
    bool DispSilh { get; set; }
    ObjectId dragvs { get; set; }
    byte DrawOrderCtl { get; set; }
    int DwfFrame { get; set; }
    bool DwgFileWasSavedByAutodeskSoftware { get; }
    int DxEval { get; set; }
    double Elevation { get; set; }
    EndCap EndCaps { get; set; }
    Point3d Extmax { get; set; }
    Point3d Extmin { get; set; }
    double Facetres { get; set; }
    string Filename { get; }
    double Filletrad { get; set; }
    bool Fillmode { get; set; }
    string FingerprintGuid { get; set; }
    ObjectId GeoDataObject { get; }
    ObjectId GroupDictionaryId { get; }
    int HaloGap { get; set; }
    Handle Handseed { get; set; }
    int HideText { get; set; }
    DbHomeView HomeView { get; set; }
    bool HpInherit { get; set; }
    Point2d HpOrigin { get; set; }
    string HyperlinkBase { get; set; }
    int Indexctl { get; set; }
    Point3d Insbase { get; set; }
    UnitsValue Insunits { get; set; }
    Color Interferecolor { get; set; }
    ObjectId Interfereobjvs { get; set; }
    ObjectId Interferevpvs { get; set; }
    int IntersectColor { get; set; }
    int IntersectDisplay { get; set; }
    bool IsBeingDestroyed { get; }
    bool IsEmr { get; }
    int Isolines { get; set; }
    bool IsPartiallyOpened { get; }
    JoinStyle JoinStyle { get; set; }
    MaintenanceReleaseVersion LastSavedAsMaintenanceVersion { get; }
    DwgVersion LastSavedAsVersion { get; }
    double Latitude { get; set; }
    int LayerEval { get; set; }
    LayerFilterTree LayerFilters { get; set; }
    int LayerNotify { get; set; }
    LayerStateManager LayerStateManager { get; }
    ObjectId LayerTableId { get; }
    ObjectId LayerZero { get; }
    ObjectId LayoutDictionaryId { get; }
    double LensLength { get; set; }
    int LightGlyphDisplay { get; set; }
    byte LightingUnits { get; set; }
    bool LightsInBlocks { get; set; }
    bool Limcheck { get; set; }
    Point2d Limmax { get; set; }
    Point2d Limmin { get; set; }
    ObjectId LinetypeTableId { get; }
    bool LineWeightDisplay { get; set; }
    double LoftAng1 { get; set; }
    double LoftAng2 { get; set; }
    double LoftMag1 { get; set; }
    double LoftMag2 { get; set; }
    int LoftNormals { get; set; }
    int LoftParam { get; set; }
    double Longitude { get; set; }
    double Ltscale { get; set; }
    int Lunits { get; set; }
    int Luprec { get; set; }
    int MaintenanceReleaseVersion { get; }
    ObjectId MaterialDictionaryId { get; }
    int Maxactvp { get; set; }
    MeasurementValue Measurement { get; set; }
    string Menu { get; }
    bool Mirrtext { get; set; }
    double MLeaderscale { get; set; }
    ObjectId MLeaderstyle { get; set; }
    ObjectId MLeaderStyleDictionaryId { get; }
    ObjectId MLStyleDictionaryId { get; }
    bool MsLtScale { get; set; }
    double MsOleScale { get; set; }
    ObjectId NamedObjectsDictionaryId { get; }
    bool NeedsRecovery { get; }
    double NorthDirection { get; set; }
    int NumberOfSaves { get; }
    ObjectContextManager ObjectContextManager { get; }
    int ObscuredColor { get; set; }
    int ObscuredLineType { get; set; }
    bool OleStartUp { get; set; }
    MaintenanceReleaseVersion OriginalFileMaintenanceVersion { get; }
    string OriginalFileName { get; }
    MaintenanceReleaseVersion OriginalFileSavedByMaintenanceVersion { get; }
    DwgVersion OriginalFileSavedByVersion { get; }
    DwgVersion OriginalFileVersion { get; }
    bool Orthomode { get; set; }
    ObjectId PaperSpaceVportId { get; }
    int PdfFrame { get; set; }
    int Pdmode { get; set; }
    double Pdsize { get; set; }
    double Pelevation { get; set; }
    Point3d Pextmax { get; set; }
    Point3d Pextmin { get; set; }
    Point3d Pinsbase { get; set; }
    bool Plimcheck { get; set; }
    Point2d Plimmax { get; set; }
    Point2d Plimmin { get; set; }
    bool PlineEllipse { get; set; }
    bool Plinegen { get; set; }
    double Plinewid { get; set; }
    ObjectId PlotSettingsDictionaryId { get; }
    bool PlotStyleMode { get; }
    ObjectId PlotStyleNameDictionaryId { get; }
    PlotStyleDescriptor PlotStyleNameId { get; set; }
    string ProjectName { get; set; }
    bool Psltscale { get; set; }
    double PsolHeight { get; set; }
    double PsolWidth { get; set; }
    CoordinateSystem3d Pucs { set; }
    ObjectId PucsBase { get; set; }
    ObjectId Pucsname { get; set; }
    Point3d Pucsorg { get; }
    OrthographicView PucsOrthographic { get; }
    Vector3d Pucsxdir { get; }
    Vector3d Pucsydir { get; }
    bool Qtextmode { get; set; }
    ObjectId RegAppTableId { get; }
    bool Regenmode { get; set; }
    bool RetainOriginalThumbnailBitmap { get; set; }
    int Saveproxygraphics { get; set; }
    ObjectId SectionManagerId { get; }
    ObjectId SectionViewStyle { get; set; }
    ObjectId SectionViewStyleDictionaryId { get; }
    SecurityParameters SecurityParameters { get; set; }
    int Shadedge { get; set; }
    int Shadedif { get; set; }
    double ShadowPlaneLocation { get; set; }
    int ShowHist { get; set; }
    double Sketchinc { get; set; }
    bool Skpoly { get; set; }
    int SolidHist { get; set; }
    int SortEnts { get; set; }
    bool Splframe { get; set; }
    int Splinesegs { get; set; }
    int Splinetype { get; set; }
    double StepSize { get; set; }
    double StepsPerSec { get; set; }
    string StyleSheet { get; set; }
    DatabaseSummaryInfo SummaryInfo { get; set; }
    int Surftab1 { get; set; }
    int Surftab2 { get; set; }
    int Surftype { get; set; }
    int Surfu { get; set; }
    int Surfv { get; set; }
    ObjectId Tablestyle { get; set; }
    ObjectId TableStyleDictionaryId { get; }
    DateTime Tdcreate { get; }
    TimeSpan Tdindwg { get; }
    DateTime Tducreate { get; }
    DateTime Tdupdate { get; }
    TimeSpan Tdusrtimer { get; }
    DateTime Tduupdate { get; }
    double Textsize { get; set; }
    ObjectId Textstyle { get; set; }
    ObjectId TextStyleTableId { get; }
    double Thickness { get; set; }
        [SKIPPED PROPERTY] ThumbnailBitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    bool TileMode { get; set; }
    int TileModeLightSynch { get; set; }
    TimeZone TimeZone { get; set; }
    double Tracewid { get; set; }
    TransactionManager TransactionManager { get; }
    int Treedepth { get; set; }
    int TStackAlign { get; set; }
    int TstackSize { get; set; }
    CoordinateSystem3d Ucs { set; }
    ObjectId UcsBase { get; set; }
    ObjectId Ucsname { get; set; }
    Point3d Ucsorg { get; }
    OrthographicView UcsOrthographic { get; }
    ObjectId UcsTableId { get; }
    Vector3d Ucsxdir { get; }
    Vector3d Ucsydir { get; }
    bool UndoRecording { get; }
    int Unitmode { get; set; }
    int UpdateThumbnail { get; set; }
    int Useri1 { get; set; }
    int Useri2 { get; set; }
    int Useri3 { get; set; }
    int Useri4 { get; set; }
    int Useri5 { get; set; }
    double Userr1 { get; set; }
    double Userr2 { get; set; }
    double Userr3 { get; set; }
    double Userr4 { get; set; }
    double Userr5 { get; set; }
    bool Usrtimer { get; set; }
    string VersionGuid { get; set; }
    double ViewportScaleDefault { get; set; }
    ObjectId ViewportTableId { get; }
    ObjectId ViewTableId { get; }
    bool Visretain { get; set; }
    ObjectId VisualStyleDictionaryId { get; }
    bool Worldview { get; set; }
    int XclipFrame { get; set; }
    ObjectId XrefBlockId { get; }
    bool XrefEditEnabled { get; set; }

  METHODS:
    void AbortDeepClone(IdMapping idMap)
    ObjectId AddDBObject(DBObject appendIt)
    void ApplyPartialOpenFilters(SpatialFilter spatialFilter, LayerFilter layerFilter)
    ObjectId AttachXref(string fileName, string blockName)
    void AuditXData(AuditInfo info)
    void BindXrefs(ObjectIdCollection xrefIds, bool insertBind)
    string ClassDxfName(RXClass getMyDxfName)
    void CloseInput(bool closeFile)
    int CountEmptyObjects(int flags)
    void CountHardReferences(ObjectIdCollection ids, int[] count)
    static Database DbFromId(long id)
    void DeepCloneObjects(ObjectIdCollection identifiers, ObjectId id, IdMapping mapping, bool deferTranslation)
    void DetachXref(ObjectId xrefId)
    void DisablePartialOpen()
    void DisableUndoRecording(bool disable)
    void DxfIn(string fileName, string logFilename)
    void DxfOut(string fileName, int precision, DwgVersion version)
    void DxfOut(string fileName, int precision, bool saveThumbnailImage)
    void DxfOutEx(string fileName, int precision, DwgVersion version, bool saveThumbnailImage, int nCodePage)
    static bool Equals(Database db1, Database db2)
    int EraseEmptyObjects(int flags)
    FieldEvaluationResult EvaluateFields(FieldEvaluationContext context, string prop)
    FieldEvaluationResult EvaluateFields(FieldEvaluationContext context)
    FieldEvaluationResult EvaluateFields()
    void ForceWblockDatabaseCopy()
    static Database FromAcadDatabase(object acadDatabase)
    static List<Database> GetAllDatabases()
    DimStyleTableRecord GetDimensionStyleChildData(RXClass classDescriptor)
    ObjectId GetDimensionStyleChildId(RXClass classDescriptor, ObjectId parentStyle)
    ObjectId GetDimensionStyleParentId(ObjectId childStyle)
    ObjectIdCollection GetDimRecentStyleList()
    DimStyleTableRecord GetDimstyleData()
    int GetHashCode()
    XrefGraph GetHostDwgXrefGraph(bool includeGhosts)
    DynamicMetaObject GetMetaObject(Expression parameter)
    LineWeight GetNearestLineWeight(int weight)
    ObjectId GetObjectId(bool createIfNotFound, Handle objHandle, int identifier)
    DwgVersion[] GetSupportedDxfOutVersions()
    DwgVersion[] GetSupportedSaveVersions()
    ObjectIdCollection GetViewports(bool bGetPaperspaceVports)
    StringCollection GetVisualStyleList()
    static long IdFromDb(Database dataBase)
    ObjectId Insert(string sourceBlockName, string destinationBlockName, Database dataBase, bool preserveSourceDatabase)
    void Insert(Matrix3d transform, Database dataBase, bool preserveSourceDatabase)
    ObjectId Insert(string blockName, Database dataBase, bool preserveSourceDatabase)
    static bool IsObjectNonPersistent(ObjectId id)
    bool IsValidLineWeight(int weight)
    void LoadLineTypeFile(string lineTypeName, string filename)
    void LoadMlineStyleFile(string mlineStyleName, string fileName)
    static void MarkObjectNonPersistent(ObjectId id, bool value)
    ObjectId OverlayXref(string fileName, string blockName)
    void Purge(ObjectIdGraph idGraph)
    void Purge(ObjectIdCollection ids)
    void ReadDwgFile(IntPtr drawingFile, bool allowCPConversion, string password)
    void ReadDwgFile(string fileName, FileOpenMode mode, bool allowCPConversion, string password)
    void ReadDwgFile(string fileName, FileShare fileSharing, bool allowCPConversion, string password)
    void ReclaimMemoryFromErasedObjects(ObjectIdCollection ids)
    void ReloadXrefs(ObjectIdCollection xrefIds)
    void ResetTimes()
    void ResolveXrefs(bool useThreadEngine, bool doNewOnly)
    void RestoreForwardingXrefSymbols()
    void RestoreOriginalXrefSymbols()
    long RuntimeId()
    void Save()
    void SaveAs(string fileName, DwgVersion version)
    void SaveAs(string fileName, bool bBakAndRename, DwgVersion version, SecurityParameters security)
    void SaveAs(string fileName, SecurityParameters security)
    void SetDimstyleData(DimStyleTableRecord style)
    TimeZone SetTimeZoneAsUtcOffset(double offset)
    void SetWorldPaperspaceUcsBaseOrigin(Point3d origin, OrthographicView orthoView)
    void SetWorldUcsBaseOrigin(Point3d origin, OrthographicView orthoView)
    string TimeZoneDescription(TimeZone tz)
    double TimeZoneOffset(TimeZone tz)
    bool TryGetObjectId(Handle objHandle, out ObjectId id)
    void UnloadXrefs(ObjectIdCollection xrefIds)
    void UpdateExt(bool doBestFit)
    Database Wblock()
    Database Wblock(ObjectId blockId)
    void Wblock(Database outputDataBase, ObjectIdCollection outObjIds, Point3d basePoint, DuplicateRecordCloning cloning)
    Database Wblock(ObjectIdCollection outObjIds, Point3d basePoint)
    void WblockCloneObjects(ObjectIdCollection identifiers, ObjectId id, IdMapping mapping, DuplicateRecordCloning cloning, bool deferTranslation)
    Point3d WorldPaperspaceUcsBaseOrigin(OrthographicView orthoView)
    Point3d WorldUcsBaseOrigin(OrthographicView orthoView)
    void XBindXrefs(ObjectIdCollection xrefSymbolIds, bool insertBind)

  EVENTS:
    event XrefSubCommandEndEventHandler XrefSubCommandEnd
    event XrefSubCommandAbortedEventHandler XrefSubCommandAborted
    event XrefPreXrefLockFileEventHandler XrefPreXrefLockFile
    event XrefSubCommandStartEventHandler XrefSubCommandStart
    event EventHandler Disposed
    event ProxyResurrectionCompletedEventHandler ProxyResurrectionCompleted
    event SystemVariableChangedEventHandler SystemVariableChanged
    event SystemVariableChangingEventHandler SystemVariableWillChange
    event ObjectErasedEventHandler ObjectErased
    event ObjectEventHandler ObjectModified
    event ObjectEventHandler ObjectOpenedForModify
    event ObjectEventHandler ObjectReappended
    event ObjectEventHandler ObjectUnappended
    event ObjectEventHandler ObjectAppended
    event EventHandler XrefRestoreEnded
    event EventHandler XrefRestoreAborted
    event XrefBeginOperationEventHandler XrefBeginRestore
    event XrefComandeeredEventHandler XrefComandeered
    event XrefRedirectedEventHandler XrefRedirected
    event EventHandler XrefAttachEnded
    event EventHandler XrefAttachAborted
    event XrefBeginOperationEventHandler XrefBeginOtherAttached
    event XrefBeginOperationEventHandler XrefBeginAttached
    event EventHandler PartialOpenNotice
    event EventHandler DeepCloneEnded
    event EventHandler DeepCloneAborted
    event IdMappingEventHandler BeginDeepCloneTranslation
    event IdMappingEventHandler BeginDeepCloning
    event IdMappingEventHandler BeginDeepClone
    event BeginWblockObjectsEventHandler BeginWblockObjects
    event EventHandler WblockEnded
    event EventHandler WblockAborted
    event IdMappingEventHandler WblockMappingAvailable
    event BeginWblockEntireDatabaseEventHandler BeginWblockEntireDatabase
    event BeginWblockBlockEventHandler BeginWblockBlock
    event BeginWblockSelectedObjectsEventHandler BeginWblockSelectedObjects
    event WblockNoticeEventHandler WblockNotice
    event EventHandler InsertEnded
    event EventHandler InsertAborted
    event IdMappingEventHandler InsertMappingAvailable
    event BeginInsertEventHandler BeginInsert
    event EventHandler DxfOutComplete
    event EventHandler AbortDxfOut
    event EventHandler BeginDxfOut
    event EventHandler DxfInComplete
    event EventHandler AbortDxfIn
    event EventHandler BeginDxfIn
    event EventHandler AbortSave
    event DatabaseIOEventHandler SaveComplete
    event DatabaseIOEventHandler BeginSave
    event EventHandler DatabaseToBeDestroyed
    event EventHandler DatabaseConstructed
    event EventHandler InitialDwgFileOpenComplete
    event DatabaseIOEventHandler DwgFileOpened

--------------------------------------------------------------------------------

[CLASS] DatabaseIOEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.DatabaseIOEventArgs
Inherits: EventArgs

  PROPERTIES:
    string FileName { get; }

--------------------------------------------------------------------------------

[CLASS] DatabaseIOEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.DatabaseIOEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new DatabaseIOEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, DatabaseIOEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, DatabaseIOEventArgs e)

--------------------------------------------------------------------------------

[STRUCT] DatabaseSummaryInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.DatabaseSummaryInfo

  PROPERTIES:
    string Author { get; }
    string Comments { get; }
    IDictionaryEnumerator CustomProperties { get; }
    string HyperlinkBase { get; }
    string Keywords { get; }
    string LastSavedBy { get; }
    string RevisionNumber { get; }
    string Subject { get; }
    string Title { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] DatabaseSummaryInfoBuilder
Full Name: Autodesk.AutoCAD.DatabaseServices.DatabaseSummaryInfoBuilder

  CONSTRUCTORS:
    new DatabaseSummaryInfoBuilder(DatabaseSummaryInfo value)
    new DatabaseSummaryInfoBuilder()

  PROPERTIES:
    string Author { get; set; }
    string Comments { get; set; }
    StringDictionary CustomProperties { get; }
    IDictionary CustomPropertyTable { get; }
    string HyperlinkBase { get; set; }
    string Keywords { get; set; }
    string LastSavedBy { get; set; }
    string RevisionNumber { get; set; }
    string Subject { get; set; }
    string Title { get; set; }

  METHODS:
    DatabaseSummaryInfo ToDatabaseSummaryInfo()

--------------------------------------------------------------------------------

[CLASS] DataCell
Full Name: Autodesk.AutoCAD.DatabaseServices.DataCell
Inherits: RXObject

  CONSTRUCTORS:
    new DataCell()

  PROPERTIES:
    CellType CellType { get; }
    object Value { get; }

  METHODS:
    void Init()
    void SetBool(bool value)
    void SetDouble(double value)
    void SetHardOwnershipId(ObjectId value)
    void SetHardPointerId(ObjectId value)
    void SetInteger(int value)
    void SetObjectId(ObjectId value)
    void SetPoint(Point3d value)
    void SetSoftOwnershipId(ObjectId value)
    void SetSoftPointerId(ObjectId value)
    void SetString(string value)
    void SetVector(Vector3d value)

--------------------------------------------------------------------------------

[CLASS] DataCellCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.DataCellCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new DataCellCollection()

  PROPERTIES:
    int Count { get; }
    DataCell Item { get; set; }

  METHODS:
    int Add(DataCell value)
    void Clear()
    bool Contains(DataCell value)
    void CopyTo(DataCell[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(DataCell value)
    void Insert(int index, DataCell value)
    void Remove(DataCell value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] DataColumn
Full Name: Autodesk.AutoCAD.DatabaseServices.DataColumn
Inherits: RXObject

  CONSTRUCTORS:
    new DataColumn(CellType type, string columnName)
    new DataColumn(DataColumn column)
    new DataColumn()

  PROPERTIES:
    string ColumnName { get; set; }
    CellType ColumnType { get; set; }
    int GrowLength { get; set; }
    int NumCells { get; }
    int PhysicalLength { get; set; }

  METHODS:
    void AppendCell(DataCell cell)
    void Assign(DataColumn col)
    DataCell GetCellAt(int index)
    int GetIndexAtCell(DataCell cell)
    void InsertCellAt(int index, DataCell cell)
    void RemoveCellAt(int index)
    void SetCellAt(int index, DataCell cell)

--------------------------------------------------------------------------------

[CLASS] DataLink
Full Name: Autodesk.AutoCAD.DatabaseServices.DataLink
Inherits: DBObject

  CONSTRUCTORS:
    new DataLink()

  PROPERTIES:
    string ConnectionString { get; set; }
    string DataAdapterId { get; set; }
    DataLinkOption DataLinkOption { get; set; }
    string Description { get; set; }
    bool IsValid { get; }
    string Name { get; set; }
    string ToolTip { get; set; }
    int UpdateOption { get; set; }

  METHODS:
    ArrayList GetSourceFiles(DataLinkGetSourceContext nContext)
    ObjectIdCollection GetTargets()
    ErrorStatus GetUpdateStatus(out UpdateDirection pDir, out DateTime pTime, out string errMessage)
    void RepathSourceFiles(string sBasePath, PathOption nOption)
    void Update(UpdateDirection dir, UpdateOption option)

--------------------------------------------------------------------------------

[ENUM] DataLinkGetSourceContext
Full Name: Autodesk.AutoCAD.DatabaseServices.DataLinkGetSourceContext
Inherits: Enum

  Values:
    - Unknown = 0
    - Etransmit = 1
    - XrefManager = 2
    - FileWatcher = 3
    - Other = 4
    - OrignalPath = 256

--------------------------------------------------------------------------------

[CLASS] DataLinkManager
Full Name: Autodesk.AutoCAD.DatabaseServices.DataLinkManager
Inherits: RXObject

  PROPERTIES:
    int DataLinkCount { get; }

  METHODS:
    ObjectId AddDataLink(DataLink dataLink)
    ObjectIdCollection GetDataLink()
    ObjectId GetDataLink(string name)
    void RemoveDataLink(ObjectId idDataLink)
    ObjectId RemoveDataLink(string name)
    void Update(UpdateDirection direction, UpdateOption option)
    void Update(ObjectIdCollection dataIds, UpdateDirection direction, UpdateOption option)

--------------------------------------------------------------------------------

[ENUM] DataLinkOption
Full Name: Autodesk.AutoCAD.DatabaseServices.DataLinkOption
Inherits: Enum

  Values:
    - None = 0
    - Anonymous = 1
    - PersistCache = 2

--------------------------------------------------------------------------------

[CLASS] DataTable
Full Name: Autodesk.AutoCAD.DatabaseServices.DataTable
Inherits: DBObject

  CONSTRUCTORS:
    new DataTable()

  PROPERTIES:
    int NumColsGrowSize { get; set; }
    int NumColsPhysicalSize { get; set; }
    int NumColumns { get; }
    int NumRows { get; }
    int NumRowsGrowSize { get; set; }
    int NumRowsPhysicalSize { get; set; }
    string TableName { get; set; }

  METHODS:
    void AppendColumn(CellType type, string columnName)
    void AppendRow(DataCellCollection row, bool validate)
    void Assign(DataTable other)
    DataCell GetCellAt(int row, int col)
    DataColumn GetColumnAt(int index)
    int GetColumnIndexAtName(string name)
    string GetColumnNameAt(int index)
    CellType GetColumnTypeAt(int index)
    DataCellCollection GetRowAt(int index)
    void InsertColumnAt(int index, CellType type, string columnName)
    void InsertRowAt(int index, DataCellCollection row, bool validate)
    void RemoveColumnAt(int index)
    void RemoveRowAt(int index)
    void SetCellAt(int row, int col, DataCell cell)
    void SetRowAt(int index, DataCellCollection row, bool validate)

--------------------------------------------------------------------------------

[ENUM] DataType
Full Name: Autodesk.AutoCAD.DatabaseServices.DataType
Inherits: Enum

  Values:
    - Unknown = 0
    - Long = 1
    - Double = 2
    - String = 4
    - Date = 8
    - Point = 16
    - Point3d = 32
    - ObjectId = 64
    - Buffer = 128
    - Resbuf = 256
    - General = 512

--------------------------------------------------------------------------------

[STRUCT] DataTypeParameter
Full Name: Autodesk.AutoCAD.DatabaseServices.DataTypeParameter

  CONSTRUCTORS:
    new DataTypeParameter(DataType dataType, UnitType unitType)

  PROPERTIES:
    DataType DataType { get; set; }
    UnitType UnitType { get; set; }

  METHODS:
    bool Equals(object other)
    int GetHashCode()

--------------------------------------------------------------------------------

[CLASS] DBDictionary
Full Name: Autodesk.AutoCAD.DatabaseServices.DBDictionary
Inherits: DBObject
Implements: IDictionary, ICollection, IEnumerable

  CONSTRUCTORS:
    new DBDictionary()

  PROPERTIES:
    int Count { get; }
    DBDictionary IncludingErased { get; }
    object Item { get; set; }
    DuplicateRecordCloning MergeStyle { get; set; }
    bool TreatElementsAsHard { get; set; }

  METHODS:
    bool Contains(ObjectId objId)
    bool Contains(string entryName)
    void CopyTo(DBDictionaryEntry[] array, int index)
    ObjectId GetAt(string entryName)
    DbDictionaryEnumerator GetEnumerator()
    string NameAt(ObjectId objId)
    void Remove(ObjectId objId)
    ObjectId Remove(string key)
    ObjectId SetAt(string searchKey, DBObject newValue)
    void SetName(string oldName, string newName)

--------------------------------------------------------------------------------

[STRUCT] DBDictionaryEntry
Full Name: Autodesk.AutoCAD.DatabaseServices.DBDictionaryEntry

  CONSTRUCTORS:
    new DBDictionaryEntry(string key, ObjectId value)

  PROPERTIES:
    string Key { get; set; }
    ObjectId Value { get; set; }

  METHODS:
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] DbDictionaryEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.DbDictionaryEnumerator
Inherits: RXObject
Implements: IDictionaryEnumerator, IEnumerator

  PROPERTIES:
    DBDictionaryEntry Current { get; }
    DBDictionaryEntry Entry { get; }
    string Key { get; }
    ObjectId Value { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] DbHomeView
Full Name: Autodesk.AutoCAD.DatabaseServices.DbHomeView
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new DbHomeView()
    new DbHomeView(IntPtr unmanagedPointer, bool autoDelete)

  PROPERTIES:
    Point3d Center { get; set; }
    Point3d Eye { get; set; }
    double Height { get; set; }
    bool Perspective { get; set; }
    Vector3d Up { get; set; }
    double Width { get; set; }

  METHODS:
    object Clone()
    bool Equals(object obj)
    void ToggleDefaultSettings()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DBObject
Full Name: Autodesk.AutoCAD.DatabaseServices.DBObject
Inherits: Drawable

  PROPERTIES:
    object AcadObject { get; }
    AnnotativeStates Annotative { get; set; }
    Guid ClassID { get; }
    Database Database { get; }
    Drawable Drawable { get; }
    ObjectId ExtensionDictionary { get; }
    Handle Handle { get; }
    bool HasFields { get; }
    bool HasSaveVersionOverride { get; set; }
    ObjectId Id { get; }
    bool IsAProxy { get; }
    bool IsCancelling { get; }
    bool IsErased { get; }
    bool IsEraseStatusToggled { get; }
    bool IsModified { get; }
    bool IsModifiedGraphics { get; }
    bool IsModifiedXData { get; }
    bool IsNewObject { get; }
    bool IsNotifyEnabled { get; }
    bool IsNotifying { get; }
    bool IsObjectIdsInFlux { get; }
    bool IsPersistent { get; }
    bool IsReadEnabled { get; }
    bool IsReallyClosing { get; }
    bool IsTransactionResident { get; }
    bool IsUndoing { get; }
    bool IsWriteEnabled { get; }
    DuplicateRecordCloning MergeStyle { get; set; }
    FullDwgVersion ObjectBirthVersion { get; }
    ObjectId ObjectId { get; }
    ObjectId OwnerId { get; set; }
    PaperOrientationStates PaperOrientation { get; }
    DwgFiler UndoFiler { get; }
    ResultBuffer XData { get; set; }

  METHODS:
    void AddContext(ObjectContext context)
    void ApplyPaperOrientationTransform(Viewport viewport)
    void ApplyPartialUndo(DwgFiler undoFiler, RXClass classObj)
    void Audit(AuditInfo auditInfo)
    void Cancel()
    void Close()
    void CloseAndPage(bool onlyWhenClean)
    void CreateExtensionDictionary()
    DecomposeForSaveReplacementRecord DecomposeForSave(DwgVersion version)
    DBObject DeepClone(DBObject ownerPointer, IdMapping idMap, bool isPrimary)
    void DisableUndoRecording(bool disable)
    void DowngradeOpen()
    void DowngradeToNotify(bool wasWritable)
    void DwgIn(DwgFiler filer)
    void DwgOut(DwgFiler filer)
    void DxfIn(DxfFiler filer)
    void DxfOut(DxfFiler filer)
    void Erase()
    void Erase(bool erasing)
    static ObjectId FromAcadObject(object acadObj)
    byte[] GetBinaryDataForKey(string key)
    DBObjectEventExtender GetEventExtender(bool create)
    ObjectId GetField(string propertyName)
    ObjectId GetField()
    FullDwgVersion GetObjectSaveVersion(DxfFiler filer)
    FullDwgVersion GetObjectSaveVersion(DwgFiler filer)
    IParameter GetParameterInterface(string name, bool runtimeInterface)
    ObjectIdCollection GetPersistentReactorIds()
    List<RXObject> GetReactors()
    List<RXObject> GetTransientReactors()
    ResultBuffer GetXDataForApplication(string applicationName)
    void HandOverTo(DBObject newPointer, bool keepXData, bool keepExtensionDictionary)
    bool HasContext(ObjectContext context)
    bool HasPersistentReactor(ObjectId objId)
    static bool IsCustomObject(ObjectId id)
    void ReleaseExtensionDictionary()
    void RemoveContext(ObjectContext context)
    void RemoveField(ObjectId id)
    ObjectId RemoveField(string propertyName)
    ObjectId RemoveField()
    void ResetScaleDependentProperties()
    void SetBinaryDataForKey(string key, byte[] chunk)
    ObjectId SetField(string propertyName, Field field)
    ObjectId SetField(Field field)
    bool SetFromStyle()
    void SetObjectIdsInFlux()
    void SetPaperOrientation(bool bPaperOrientation)
    bool SupportsCollection(string collectionName)
    void SwapIdWith(ObjectId otherId, bool swapExtendedData, bool swapExtensionDictionary)
    void SwapReferences(IdMapping idMap)
    bool UpgradeFromNotify()
    void UpgradeOpen()
    DBObject WblockClone(RXObject ownerPointer, IdMapping idMap, bool isPrimary)
    void XDataTransformBy(Matrix3d transform)

  EVENTS:
    event ObjectClosedEventHandler ObjectClosed
    event EventHandler Reappended
    event EventHandler Unappended
    event EventHandler ModifiedXData
    event EventHandler ModifyUndone
    event ObjectEventHandler SubObjectModified
    event EventHandler Modified
    event EventHandler OpenedForModify
    event EventHandler Goodbye
    event ObjectErasedEventHandler Erased
    event ObjectEventHandler Copied
    event EventHandler Cancelled

--------------------------------------------------------------------------------

[CLASS] DBObjectCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.DBObjectCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new DBObjectCollection()

  PROPERTIES:
    int Count { get; }
    DBObject Item { get; set; }

  METHODS:
    int Add(DBObject value)
    void Clear()
    bool Contains(DBObject value)
    void CopyTo(DBObject[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(DBObject value)
    void Insert(int index, DBObject value)
    void Remove(DBObject value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[STRUCT] DBObjectReference
Full Name: Autodesk.AutoCAD.DatabaseServices.DBObjectReference

  CONSTRUCTORS:
    new DBObjectReference(ObjectId id, int kind)

  PROPERTIES:
    int Kind { get; }
    ObjectId ObjectId { get; }

--------------------------------------------------------------------------------

[CLASS] DBObjectReferenceCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.DBObjectReferenceCollection
Inherits: CollectionBase

  PROPERTIES:
    DBObjectReference Item { get; set; }

  METHODS:
    int Add(DBObjectReference value)
    bool Contains(DBObjectReference value)
    void CopyTo(DBObjectReference[] array, int index)
    int IndexOf(DBObjectReference value)
    void Insert(int index, DBObjectReference value)
    void Remove(DBObjectReference value)

--------------------------------------------------------------------------------

[CLASS] DBPoint
Full Name: Autodesk.AutoCAD.DatabaseServices.DBPoint
Inherits: Entity

  CONSTRUCTORS:
    new DBPoint(Point3d position)
    new DBPoint()

  PROPERTIES:
    double EcsRotation { get; set; }
    Vector3d Normal { get; set; }
    Point3d Position { get; set; }
    double Thickness { get; set; }

--------------------------------------------------------------------------------

[CLASS] DBText
Full Name: Autodesk.AutoCAD.DatabaseServices.DBText
Inherits: Entity

  CONSTRUCTORS:
    new DBText()

  PROPERTIES:
    Point3d AlignmentPoint { get; set; }
    double Height { get; set; }
    TextHorizontalMode HorizontalMode { get; set; }
    bool IsDefaultAlignment { get; }
    bool IsMirroredInX { get; set; }
    bool IsMirroredInY { get; set; }
    AttachmentPoint Justify { get; set; }
    Vector3d Normal { get; set; }
    double Oblique { get; set; }
    Point3d Position { get; set; }
    double Rotation { get; set; }
    string TextString { get; set; }
    ObjectId TextStyleId { get; set; }
    string TextStyleName { get; }
    double Thickness { get; set; }
    TextVerticalMode VerticalMode { get; set; }
    double WidthFactor { get; set; }

  METHODS:
    void AdjustAlignment(Database alternateDatabaseToUse)
    void ConvertFieldToText()
    int CorrectSpelling()
    string getTextWithFieldCodes()

--------------------------------------------------------------------------------

[CLASS] DBVisualStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.DBVisualStyle
Inherits: DBObject

  CONSTRUCTORS:
    new DBVisualStyle()

  PROPERTIES:
    string Description { get; set; }
    bool InternalUseOnly { get; set; }
    string Name { get; }
    VisualStyleType Type { get; set; }

  METHODS:
    void CopyFrom(VisualStyle pSrc)
    void CopyTo(VisualStyle pDest)
    object GetTrait(VisualStyleProperty prop)
    bool GetTraitFlag(VisualStyleProperty prop, uint flagVal)
    void SetTrait(VisualStyleProperty prop, Color color, VisualStyleOperation __unnamed002)
    void SetTrait(VisualStyleProperty prop, double red, double green, double blue, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, double dVal, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, bool bVal, VisualStyleOperation __unnamed002)
    void SetTrait(VisualStyleProperty prop, int nVal, VisualStyleOperation op)
    void SetTrait(VisualStyleProperty prop, object val, VisualStyleOperation op)
    void SetTraitFlag(VisualStyleProperty prop, uint flagVal, bool bEnable)

--------------------------------------------------------------------------------

[STRUCT] DecomposeForSaveReplacementRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.DecomposeForSaveReplacementRecord

  CONSTRUCTORS:
    new DecomposeForSaveReplacementRecord(ObjectId replacement, bool exchangeXData)
    new DecomposeForSaveReplacementRecord(DBObject replacement, bool exchangeXData)

  PROPERTIES:
    bool ExchangeXData { get; }
    ObjectId ReplacementId { get; }
    DBObject ReplacementObject { get; }

--------------------------------------------------------------------------------

[ENUM] DeepCloneType
Full Name: Autodesk.AutoCAD.DatabaseServices.DeepCloneType
Inherits: Enum

  Values:
    - Copy = 0
    - Explode = 1
    - Block = 2
    - XrefBind = 3
    - SymbolTableMerge = 4
    - Insert = 6
    - Wblock = 7
    - Objects = 8
    - XrefInsert = 9
    - InsertCopy = 10
    - WblockObjects = 11

--------------------------------------------------------------------------------

[ENUM] DefaultLightingType
Full Name: Autodesk.AutoCAD.DatabaseServices.DefaultLightingType
Inherits: Enum

  Values:
    - OneDistantLight = 0
    - TwoDistantLights = 1

--------------------------------------------------------------------------------

[CLASS] DetailSymbol
Full Name: Autodesk.AutoCAD.DatabaseServices.DetailSymbol
Inherits: Entity

  CONSTRUCTORS:
    new DetailSymbol()

  PROPERTIES:
    Vector2d BoundarySize { get; }
    DetailSymbolBoundaryType BoundaryType { get; }
    double DetailViewScale { get; }
    Vector3d Direction { get; }
    bool DisplayIdentifier { get; }
    string Identifier { get; set; }
    Point3d IdentifierPosition { get; }
    Vector3d ModelEdgeDirection { get; }
    Point3d ModelEdgeOrigin { get; }
    DetailViewModelEdge ModelEdgeType { get; }
    Point3d Origin { get; }
    double OwningViewScale { get; }
    double Scale { get; set; }
    ObjectId SymbolStyleId { get; set; }

  METHODS:
    bool IsOverriddenProperty(DetailSymbolOverriddenProperty propertyToCheck)

--------------------------------------------------------------------------------

[ENUM] DetailSymbolBoundaryType
Full Name: Autodesk.AutoCAD.DatabaseServices.DetailSymbolBoundaryType
Inherits: Enum

  Values:
    - CircularBoundary = 0
    - RectangularBoundary = 1
    - CustomBoundary = 2

--------------------------------------------------------------------------------

[ENUM] DetailSymbolOverriddenProperty
Full Name: Autodesk.AutoCAD.DatabaseServices.DetailSymbolOverriddenProperty
Inherits: Enum

  Values:
    - ModelEdge = 1
    - IdentifierPosition = 2

--------------------------------------------------------------------------------

[ENUM] DetailViewIdentifierPlacement
Full Name: Autodesk.AutoCAD.DatabaseServices.DetailViewIdentifierPlacement
Inherits: Enum

  Values:
    - OutsideBoundary = 0
    - OutsideBoundaryWithLeader = 1
    - OnBoundary = 2
    - OnBoundaryWithLeader = 3

--------------------------------------------------------------------------------

[ENUM] DetailViewModelEdge
Full Name: Autodesk.AutoCAD.DatabaseServices.DetailViewModelEdge
Inherits: Enum

  Values:
    - Smooth = 0
    - SmoothWithBorder = 1
    - SmoothWithConnectionLine = 2
    - Jagged = 3

--------------------------------------------------------------------------------

[CLASS] DetailViewStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.DetailViewStyle
Inherits: DBObject

  CONSTRUCTORS:
    new DetailViewStyle()

  PROPERTIES:
    Color ArrowSymbolColor { get; set; }
    ObjectId ArrowSymbolId { get; set; }
    double ArrowSymbolSize { get; set; }
    Color BorderLineColor { get; set; }
    ObjectId BorderLineTypeId { get; set; }
    LineWeight BorderLineWeight { get; set; }
    Color BoundaryLineColor { get; set; }
    ObjectId BoundaryLineTypeId { get; set; }
    LineWeight BoundaryLineWeight { get; set; }
    Color ConnectionLineColor { get; set; }
    ObjectId ConnectionLineTypeId { get; set; }
    LineWeight ConnectionLineWeight { get; set; }
    string Description { get; set; }
    Color IdentifierColor { get; set; }
    double IdentifierHeight { get; set; }
    double IdentifierOffset { get; set; }
    DetailViewIdentifierPlacement IdentifierPlacement { get; set; }
    ObjectId IdentifierStyleId { get; set; }
    bool IsModifiedForRecompute { get; }
    DetailViewModelEdge ModelEdge { get; set; }
    string Name { get; set; }
    bool ShowArrowheads { get; set; }
    bool ShowViewLabel { get; set; }
    ModelDocViewLabelAlignmentType ViewLabelAlignment { get; set; }
    ModelDocViewLabelAttachmentPoint ViewLabelAttachment { get; set; }
    double ViewLabelOffset { get; set; }
    string ViewLabelPattern { get; set; }
    Color ViewLabelTextColor { get; set; }
    double ViewLabelTextHeight { get; set; }
    ObjectId ViewLabelTextStyleId { get; set; }

  METHODS:
    string DefaultViewName(int index)
    string GetViewLabelPattern(Field field)
    ObjectId PostViewStyleToDb(Database dataBasePointer, string styleName)
    void SetDatabaseDefaults(Database dataBasePointer)
    void SetViewLabelPattern(string pattern, Field field)
    bool ValidateViewName(string name)

--------------------------------------------------------------------------------

[CLASS] DgnDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.DgnDefinition
Inherits: UnderlayDefinition

  CONSTRUCTORS:
    new DgnDefinition()

  PROPERTIES:
    bool SetShowRasterRef { set; }
    bool ShowRasterRef { get; }
    bool UseMasterUnits { get; set; }
    int XrefDepth { get; set; }

--------------------------------------------------------------------------------

[CLASS] DgnReference
Full Name: Autodesk.AutoCAD.DatabaseServices.DgnReference
Inherits: UnderlayReference

  CONSTRUCTORS:
    new DgnReference()

--------------------------------------------------------------------------------

[CLASS] DgnUnderlayItem
Full Name: Autodesk.AutoCAD.DatabaseServices.DgnUnderlayItem
Inherits: UnderlayItem

  PROPERTIES:
    bool SetShowRasterRef { set; }
    bool ShowRasterRef { get; }
    bool UseMasterUnits { get; set; }

--------------------------------------------------------------------------------

[CLASS] DiametricDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.DiametricDimension
Inherits: Dimension

  CONSTRUCTORS:
    new DiametricDimension(Point3d chordPoint, Point3d farChordPoint, double leaderLength, string dimensionText, ObjectId dimensionStyle)
    new DiametricDimension()

  PROPERTIES:
    Point3d ChordPoint { get; set; }
    Point3d FarChordPoint { get; set; }
    double LeaderLength { get; set; }

--------------------------------------------------------------------------------

[CLASS] DictionaryWithDefaultDictionary
Full Name: Autodesk.AutoCAD.DatabaseServices.DictionaryWithDefaultDictionary
Inherits: DBDictionary

  CONSTRUCTORS:
    new DictionaryWithDefaultDictionary()

  PROPERTIES:
    ObjectId DefaultId { get; set; }
    FullDwgVersion ObjectBirthVersion { get; }

--------------------------------------------------------------------------------

[ENUM] DimArrowFlag
Full Name: Autodesk.AutoCAD.DatabaseServices.DimArrowFlag
Inherits: Enum

  Values:
    - FirstArrow = 0
    - SecondArrow = 1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Dimension
Full Name: Autodesk.AutoCAD.DatabaseServices.Dimension
Inherits: Entity

  PROPERTIES:
    string AlternatePrefix { get; set; }
    string AlternateSuffix { get; set; }
    bool AltSuppressLeadingZeros { get; set; }
    bool AltSuppressTrailingZeros { get; set; }
    bool AltSuppressZeroFeet { get; set; }
    bool AltSuppressZeroInches { get; set; }
    bool AltToleranceSuppressLeadingZeros { get; set; }
    bool AltToleranceSuppressTrailingZeros { get; set; }
    bool AltToleranceSuppressZeroFeet { get; set; }
    bool AltToleranceSuppressZeroInches { get; set; }
    double CenterMarkSize { get; }
    DimensionCenterMarkType CenterMarkType { get; }
    int Dimadec { get; set; }
    bool Dimalt { get; set; }
    int Dimaltd { get; set; }
    double Dimaltf { get; set; }
    double Dimaltmzf { get; set; }
    string Dimaltmzs { get; set; }
    double Dimaltrnd { get; set; }
    int Dimalttd { get; set; }
    int Dimalttz { get; set; }
    int Dimaltu { get; set; }
    int Dimaltz { get; set; }
    string Dimapost { get; set; }
    int Dimarcsym { get; set; }
    double Dimasz { get; set; }
    int Dimatfit { get; set; }
    int Dimaunit { get; set; }
    int Dimazin { get; set; }
    ObjectId Dimblk { get; set; }
    ObjectId Dimblk1 { get; set; }
    ObjectId Dimblk2 { get; set; }
    ObjectId DimBlockId { get; set; }
    Point3d DimBlockPosition { get; }
    double Dimcen { get; set; }
    Color Dimclrd { get; set; }
    Color Dimclre { get; set; }
    Color Dimclrt { get; set; }
    int Dimdec { get; set; }
    double Dimdle { get; set; }
    double Dimdli { get; set; }
    char Dimdsep { get; set; }
    ObjectId DimensionStyle { get; set; }
    string DimensionStyleName { get; set; }
    string DimensionText { get; set; }
    double Dimexe { get; set; }
    double Dimexo { get; set; }
    int Dimfrac { get; set; }
    double Dimfxlen { get; set; }
    bool DimfxlenOn { get; set; }
    double Dimgap { get; set; }
    double Dimjogang { get; set; }
    int Dimjust { get; set; }
    ObjectId Dimldrblk { get; set; }
    double Dimlfac { get; set; }
    bool Dimlim { get; set; }
    ObjectId Dimltex1 { get; set; }
    ObjectId Dimltex2 { get; set; }
    ObjectId Dimltype { get; set; }
    int Dimlunit { get; set; }
    LineWeight Dimlwd { get; set; }
    LineWeight Dimlwe { get; set; }
    double Dimmzf { get; set; }
    string Dimmzs { get; set; }
    string Dimpost { get; set; }
    double Dimrnd { get; set; }
    bool Dimsah { get; set; }
    double Dimscale { get; set; }
    bool Dimsd1 { get; set; }
    bool Dimsd2 { get; set; }
    bool Dimse1 { get; set; }
    bool Dimse2 { get; set; }
    bool Dimsoxd { get; set; }
    int Dimtad { get; set; }
    int Dimtdec { get; set; }
    double Dimtfac { get; set; }
    int Dimtfill { get; set; }
    Color Dimtfillclr { get; set; }
    bool Dimtih { get; set; }
    bool Dimtix { get; set; }
    double Dimtm { get; set; }
    int Dimtmove { get; set; }
    bool Dimtofl { get; set; }
    bool Dimtoh { get; set; }
    bool Dimtol { get; set; }
    int Dimtolj { get; set; }
    double Dimtp { get; set; }
    double Dimtsz { get; set; }
    double Dimtvp { get; set; }
    double Dimtxt { get; set; }
    bool Dimtxtdirection { get; set; }
    int Dimtzin { get; set; }
    bool Dimupt { get; set; }
    int Dimzin { get; set; }
    bool DynamicDimension { get; set; }
    double Elevation { get; set; }
    double HorizontalRotation { get; set; }
    double Measurement { get; }
    Vector3d Normal { get; set; }
    string Prefix { get; set; }
    string Suffix { get; set; }
    bool SuppressAngularLeadingZeros { get; set; }
    bool SuppressAngularTrailingZeros { get; set; }
    bool SuppressLeadingZeros { get; set; }
    bool SuppressTrailingZeros { get; set; }
    bool SuppressZeroFeet { get; set; }
    bool SuppressZeroInches { get; set; }
    AttachmentPoint TextAttachment { get; set; }
    Vector2d TextDefinedSize { get; set; }
    double TextLineSpacingFactor { get; set; }
    LineSpacingStyle TextLineSpacingStyle { get; set; }
    Point3d TextPosition { get; set; }
    double TextRotation { get; set; }
    ObjectId TextStyleId { get; set; }
    bool ToleranceSuppressLeadingZeros { get; set; }
    bool ToleranceSuppressTrailingZeros { get; set; }
    bool ToleranceSuppressZeroFeet { get; set; }
    bool ToleranceSuppressZeroInches { get; set; }
    bool UsingDefaultTextPosition { get; set; }

  METHODS:
    void FieldFromMText(MText dimMText)
    void FieldToMText(MText dimMText)
    string FormatMeasurement(double measurement, string dimensionText)
    void GenerateLayout()
    DimStyleTableRecord GetDimstyleData()
    void RecomputeDimensionBlock(bool forceUpdate)
    void RemoveTextField()
    void ResetTextDefinedSize()
    void SetDimstyleData(DimStyleTableRecord style)

--------------------------------------------------------------------------------

[ENUM] DimensionCenterMarkType
Full Name: Autodesk.AutoCAD.DatabaseServices.DimensionCenterMarkType
Inherits: Enum

  Values:
    - Mark = 0
    - Line = 1
    - None = 2

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DimensionStyleOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.DimensionStyleOverrule
Inherits: Overrule

  METHODS:
    ObjectId DimensionStyle(Dimension dimension)
    void GetDimstyleData(Dimension dimension, DimStyleTableRecord style)
    void SetCustomFilter()
    void SetDimensionStyle(Dimension dimension, ObjectId id)
    void SetDimstyleData(Dimension dimension, ObjectId dimstyleId)
    void SetDimstyleData(Dimension dimension, DimStyleTableRecord style)
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

[CLASS] DimStyleTable
Full Name: Autodesk.AutoCAD.DatabaseServices.DimStyleTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] DimStyleTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.DimStyleTableRecord
Inherits: SymbolTableRecord

  CONSTRUCTORS:
    new DimStyleTableRecord()

  PROPERTIES:
    int Dimadec { get; set; }
    bool Dimalt { get; set; }
    int Dimaltd { get; set; }
    double Dimaltf { get; set; }
    double Dimaltrnd { get; set; }
    int Dimalttd { get; set; }
    int Dimalttz { get; set; }
    int Dimaltu { get; set; }
    int Dimaltz { get; set; }
    string Dimapost { get; set; }
    int Dimarcsym { get; set; }
    double Dimasz { get; set; }
    int Dimatfit { get; set; }
    int Dimaunit { get; set; }
    int Dimazin { get; set; }
    ObjectId Dimblk { get; set; }
    ObjectId Dimblk1 { get; set; }
    ObjectId Dimblk2 { get; set; }
    double Dimcen { get; set; }
    Color Dimclrd { get; set; }
    Color Dimclre { get; set; }
    Color Dimclrt { get; set; }
    int Dimdec { get; set; }
    double Dimdle { get; set; }
    double Dimdli { get; set; }
    char Dimdsep { get; set; }
    double Dimexe { get; set; }
    double Dimexo { get; set; }
    int Dimfrac { get; set; }
    double Dimfxlen { get; set; }
    bool DimfxlenOn { get; set; }
    double Dimgap { get; set; }
    double Dimjogang { get; set; }
    int Dimjust { get; set; }
    ObjectId Dimldrblk { get; set; }
    double Dimlfac { get; set; }
    bool Dimlim { get; set; }
    ObjectId Dimltex1 { get; set; }
    ObjectId Dimltex2 { get; set; }
    ObjectId Dimltype { get; set; }
    int Dimlunit { get; set; }
    LineWeight Dimlwd { get; set; }
    LineWeight Dimlwe { get; set; }
    string Dimpost { get; set; }
    double Dimrnd { get; set; }
    bool Dimsah { get; set; }
    double Dimscale { get; set; }
    bool Dimsd1 { get; set; }
    bool Dimsd2 { get; set; }
    bool Dimse1 { get; set; }
    bool Dimse2 { get; set; }
    bool Dimsoxd { get; set; }
    int Dimtad { get; set; }
    int Dimtdec { get; set; }
    double Dimtfac { get; set; }
    int Dimtfill { get; set; }
    Color Dimtfillclr { get; set; }
    bool Dimtih { get; set; }
    bool Dimtix { get; set; }
    double Dimtm { get; set; }
    int Dimtmove { get; set; }
    bool Dimtofl { get; set; }
    bool Dimtoh { get; set; }
    bool Dimtol { get; set; }
    int Dimtolj { get; set; }
    double Dimtp { get; set; }
    double Dimtsz { get; set; }
    double Dimtvp { get; set; }
    ObjectId Dimtxsty { get; set; }
    double Dimtxt { get; set; }
    bool Dimtxtdirection { get; set; }
    int Dimtzin { get; set; }
    bool Dimupt { get; set; }
    int Dimzin { get; set; }
    bool IsModifiedForRecompute { get; }

  METHODS:
    ObjectId GetArrowId(DimArrowFlag whichArrow)

--------------------------------------------------------------------------------

[CLASS] DistanceConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.DistanceConstraint
Inherits: ExplicitConstraint

  CONSTRUCTORS:
    new DistanceConstraint(uint consLineId, DistanceDirectionType type)
    new DistanceConstraint(Vector3d direction)
    new DistanceConstraint()

  PROPERTIES:
    uint ConstrainedLineId { get; }
    Vector3d Direction { get; }
    DistanceDirectionType DirectionType { get; }

--------------------------------------------------------------------------------

[ENUM] DragStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.DragStatus
Inherits: Enum

  Values:
    - DragStart = 0
    - DragEnd = 1
    - DragAbort = 2

--------------------------------------------------------------------------------

[ENUM] DrawLeaderOrderType
Full Name: Autodesk.AutoCAD.DatabaseServices.DrawLeaderOrderType
Inherits: Enum

  Values:
    - DrawLeaderHeadFirst = 0
    - DrawLeaderTailFirst = 1

--------------------------------------------------------------------------------

[ENUM] DrawMLeaderOrderType
Full Name: Autodesk.AutoCAD.DatabaseServices.DrawMLeaderOrderType
Inherits: Enum

  Values:
    - DrawContentFirst = 0
    - DrawLeaderFirst = 1

--------------------------------------------------------------------------------

[CLASS] DrawOrderTable
Full Name: Autodesk.AutoCAD.DatabaseServices.DrawOrderTable
Inherits: DBObject

  PROPERTIES:
    ObjectId BlockId { get; set; }

  METHODS:
    bool FirstEntityIsDrawnBeforeSecond(ObjectId first, ObjectId second)
    ObjectIdCollection GetFullDrawOrder(byte honorSortEntitiesMask)
    ObjectIdCollection GetRelativeDrawOrder(byte honorSortEntitiesMask)
    Handle GetSortHandle(ObjectId id)
    void MoveAbove(ObjectIdCollection collection, ObjectId target)
    void MoveBelow(ObjectIdCollection collection, ObjectId target)
    void MoveToBottom(ObjectIdCollection collection)
    void MoveToTop(ObjectIdCollection collection)
    void SetRelativeDrawOrder(ObjectIdCollection collection)
    void SwapOrder(ObjectId id1, ObjectId id2)

--------------------------------------------------------------------------------

[ENUM] DuplicateRecordCloning
Full Name: Autodesk.AutoCAD.DatabaseServices.DuplicateRecordCloning
Inherits: Enum

  Values:
    - NotApplicable = 0
    - Ignore = 1
    - Replace = 2
    - RefMangleName = 3
    - MangleName = 4
    - UnmangleName = 5

--------------------------------------------------------------------------------

[CLASS] DwfDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.DwfDefinition
Inherits: UnderlayDefinition

  CONSTRUCTORS:
    new DwfDefinition()

  PROPERTIES:
    bool isDWFx { get; }

--------------------------------------------------------------------------------

[CLASS] DwfReference
Full Name: Autodesk.AutoCAD.DatabaseServices.DwfReference
Inherits: UnderlayReference

  CONSTRUCTORS:
    new DwfReference()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DwgFiler
Full Name: Autodesk.AutoCAD.DatabaseServices.DwgFiler
Inherits: RXObject

  PROPERTIES:
    FullDwgVersion DwgVersion { get; }
    MaintenanceReleaseVersion ExtendedMinorVersion { get; }
    ErrorStatus FilerStatus { get; set; }
    FilerType FilerType { get; }
    long Position { get; }

  METHODS:
    IntPtr ReadAddress()
    byte[] ReadBinaryChunk()
    bool ReadBoolean()
    byte ReadByte()
    void ReadBytes(byte[] value)
    double ReadDouble()
    Handle ReadHandle()
    ObjectId ReadHardOwnershipId()
    ObjectId ReadHardPointerId()
    short ReadInt16()
    int ReadInt32()
    long ReadInt64()
    Point2d ReadPoint2d()
    Point3d ReadPoint3d()
    Scale3d ReadScale3d()
    ObjectId ReadSoftOwnershipId()
    ObjectId ReadSoftPointerId()
    string ReadString()
    UInt16 ReadUInt16()
    uint ReadUInt32()
    ulong ReadUInt64()
    Vector2d ReadVector2d()
    Vector3d ReadVector3d()
    void ResetFilerStatus()
    void Seek(long offset, int method)
    void WriteAddress(IntPtr value)
    void WriteBinaryChunk(byte[] chunk)
    void WriteBoolean(bool value)
    void WriteByte(byte value)
    void WriteBytes(byte[] value)
    void WriteDouble(double value)
    void WriteHandle(Handle handle)
    void WriteHardOwnershipId(ObjectId value)
    void WriteHardPointerId(ObjectId value)
    void WriteInt16(short value)
    void WriteInt32(int value)
    void WriteInt64(long value)
    void WritePoint2d(Point2d value)
    void WritePoint3d(Point3d value)
    void WriteScale3d(Scale3d value)
    void WriteSoftOwnershipId(ObjectId value)
    void WriteSoftPointerId(ObjectId value)
    void WriteString(string value)
    void WriteUInt16(UInt16 value)
    void WriteUInt32(uint value)
    void WriteUInt64(ulong value)
    void WriteVector2d(Vector2d value)
    void WriteVector3d(Vector3d value)

--------------------------------------------------------------------------------

[ENUM] DwgVersion
Full Name: Autodesk.AutoCAD.DatabaseServices.DwgVersion
Inherits: Enum

  Values:
    - MC0To0 = 0
    - AC1To2 = 1
    - AC1To40 = 2
    - AC1To50 = 3
    - AC2To20 = 4
    - AC2To10 = 5
    - AC2To21 = 6
    - AC2To22 = 7
    - AC1001 = 8
    - AC1002 = 9
    - AC1003 = 10
    - AC1004 = 11
    - AC1005 = 12
    - AC1006 = 13
    - AC1007 = 14
    - AC1008 = 15
    - AC1009 = 16
    - AC1010 = 17
    - AC1011 = 18
    - AC1012 = 19
    - AC1013 = 20
    - AC1014 = 21
    - AC1500 = 22
    - AC1015 = 23
    - AC1800a = 24
    - AC1800 = 25
    - AC2100a = 26
    - AC1021 = 27
    - AC2400a = 28
    - AC1024 = 29
    - AC2700a = 30
    - AC1027 = 31
    - AC3200a = 32
    - AC1032 = 33
    - AC1032 = 33
    - AC1032 = 33
    - Unknown = 254
    - Max = 255

--------------------------------------------------------------------------------

[ENUM] DxfCode
Full Name: Autodesk.AutoCAD.DatabaseServices.DxfCode
Inherits: Enum

  Values:
    - Start = 0
    - Text = 1
    - Text = 1
    - MlineStyleName = 2
    - MlineStyleName = 2
    - MlineStyleName = 2
    - MlineStyleName = 2
    - MlineStyleName = 2
    - MlineStyleName = 2
    - LinetypeProse = 3
    - LinetypeProse = 3
    - LinetypeProse = 3
    - LinetypeProse = 3
    - LinetypeProse = 3
    - LinetypeProse = 3
    - SymbolTableRecordComments = 4
    - SymbolTableRecordComments = 4
    - SymbolTableRecordComments = 4
    - SymbolTableRecordComments = 4
    - Handle = 5
    - Handle = 5
    - DimBlk1 = 6
    - DimBlk1 = 6
    - DimBlk2 = 7
    - DimBlk2 = 7
    - LayerName = 8
    - CLShapeText = 9
    - XCoordinate = 10
    - YCoordinate = 20
    - ZCoordinate = 30
    - Elevation = 38
    - Thickness = 39
    - ViewportHeight = 40
    - ViewportHeight = 40
    - ViewportHeight = 40
    - ViewWidth = 41
    - ViewWidth = 41
    - ViewWidth = 41
    - TxtStylePSize = 42
    - TxtStylePSize = 42
    - ViewFrontClip = 43
    - ShapeXOffset = 44
    - ShapeXOffset = 44
    - ShapeYOffset = 45
    - ShapeYOffset = 45
    - ShapeScale = 46
    - PixelScale = 47
    - LinetypeScale = 48
    - LinetypeElement = 49
    - LinetypeElement = 49
    - LinetypeElement = 49
    - Angle = 50
    - Angle = 50
    - ViewportTwist = 51
    - Visibility = 60
    - LayerLinetype = 61
    - Color = 62
    - HasSubentities = 66
    - ViewportVisibility = 67
    - ViewportActive = 68
    - ViewportNumber = 69
    - Int16 = 70
    - ViewMode = 71
    - ViewMode = 71
    - ViewMode = 71
    - CircleSides = 72
    - CircleSides = 72
    - ViewportZoom = 73
    - ViewportZoom = 73
    - ViewportIcon = 74
    - ViewportSnap = 75
    - ViewportGrid = 76
    - ViewportSnapStyle = 77
    - ViewportSnapPair = 78
    - Int32 = 90
    - Subclass = 100
    - EmbeddedObjectStart = 101
    - ControlString = 102
    - DimVarHandle = 105
    - UcsOrg = 110
    - UcsOrientationX = 111
    - UcsOrientationY = 112
    - XReal = 140
    - ViewBrightness = 141
    - ViewContrast = 142
    - Int64 = 160
    - XInt16 = 170
    - NormalX = 210
    - NormalY = 220
    - NormalZ = 230
    - XXInt16 = 270
    - Int8 = 280
    - RenderMode = 281
    - Bool = 290
    - XTextString = 300
    - BinaryChunk = 310
    - ArbitraryHandle = 320
    - SoftPointerId = 330
    - HardPointerId = 340
    - SoftOwnershipId = 350
    - HardOwnershipId = 360
    - LineWeight = 370
    - PlotStyleNameType = 380
    - PlotStyleNameId = 390
    - ExtendedInt16 = 400
    - LayoutName = 410
    - ColorRgb = 420
    - ColorName = 430
    - Alpha = 440
    - GradientObjType = 450
    - GradientPatType = 451
    - GradientTintType = 452
    - GradientColCount = 453
    - GradientAngle = 460
    - GradientShift = 461
    - GradientTintVal = 462
    - GradientColVal = 463
    - GradientName = 470
    - Comment = 999
    - ExtendedDataAsciiString = 1000
    - ExtendedDataRegAppName = 1001
    - ExtendedDataControlString = 1002
    - ExtendedDataLayerName = 1003
    - ExtendedDataBinaryChunk = 1004
    - ExtendedDataHandle = 1005
    - ExtendedDataXCoordinate = 1010
    - ExtendedDataWorldXCoordinate = 1011
    - ExtendedDataWorldXDisp = 1012
    - ExtendedDataWorldXDir = 1013
    - ExtendedDataYCoordinate = 1020
    - ExtendedDataWorldYCoordinate = 1021
    - ExtendedDataWorldYDisp = 1022
    - ExtendedDataWorldYDir = 1023
    - ExtendedDataZCoordinate = 1030
    - ExtendedDataWorldZCoordinate = 1031
    - ExtendedDataWorldZDisp = 1032
    - ExtendedDataWorldZDir = 1033
    - ExtendedDataReal = 1040
    - ExtendedDataDist = 1041
    - ExtendedDataScale = 1042
    - ExtendedDataInteger16 = 1070
    - ExtendedDataInteger32 = 1071
    - Invalid = -9999
    - XDictionary = -6
    - PReactors = -5
    - Operator = -4
    - XDataStart = -3
    - FirstEntityId = -2
    - FirstEntityId = -2
    - End = -1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] DxfFiler
Full Name: Autodesk.AutoCAD.DatabaseServices.DxfFiler
Inherits: RXObject

  PROPERTIES:
    bool AtEmbeddedObjectStart { get; }
    bool AtEndOfFile { get; }
    bool AtEndOfObject { get; }
    bool AtExtendedData { get; }
    Database Database { get; }
    FullDwgVersion DwgVersion { get; }
    double Elevation { get; }
    string ErrorMessage { get; }
    MaintenanceReleaseVersion ExtendedMinorVersion { get; }
    FilerType FilerType { get; }
    bool IncludesDefaultValues { get; }
    bool IsModifyingExistingObject { get; }
    int Precision { get; set; }
    double Thickness { get; }

  METHODS:
    bool AtSubclassData(string value)
    void FilerStatus()
    void HaltAtClassBoundaries(bool value)
    void PushBackItem()
    ResultBuffer ReadResultBuffer()
    void ResetFilerStatus()
    int RewindFiler()
    void SetError(string format, params string[] values)
    void SetError(ErrorStatus value, string format, params string[] values)
    void WriteBool(DxfCode opCode, bool value)
    void WriteBoolean(DxfCode opCode, bool value)
    void WriteByte(DxfCode opCode, byte value)
    void WriteBytes(DxfCode opCode, byte[] chunk)
    void WriteDouble(DxfCode opCode, double value, int precision)
    void WriteEmbeddedObjectStart()
    void WriteHandle(DxfCode opCode, Handle value)
    void WriteInt16(DxfCode opCode, short value)
    void WriteInt32(DxfCode opCode, int value)
    void WriteInt64(DxfCode opCode, long value)
    void WriteObjectId(DxfCode opCode, ObjectId value)
    void WritePoint2d(DxfCode opCode, Point2d value, int precision)
    void WritePoint3d(DxfCode opCode, Point3d value, int precision)
    void WriteResultBuffer(ResultBuffer buffer)
    void WriteScale3d(DxfCode opCode, Scale3d value, int precision)
    void WriteString(DxfCode opCode, string value)
    void WriteUInt16(DxfCode opCode, UInt16 value)
    void WriteUInt32(DxfCode opCode, uint value)
    void WriteUInt64(DxfCode opCode, ulong value)
    void WriteVector2d(DxfCode opCode, Vector2d value, int precision)
    void WriteVector3d(DxfCode opCode, Vector3d value, int precision)
    void WriteXDataStart()

--------------------------------------------------------------------------------

[CLASS] DynamicBlockReferenceProperty
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicBlockReferenceProperty

  PROPERTIES:
    ObjectId BlockId { get; }
    string Description { get; }
    string PropertyName { get; }
    short PropertyTypeCode { get; }
    bool ReadOnly { get; }
    bool Show { get; }
    DynamicBlockReferencePropertyUnitsType UnitsType { get; }
    object Value { get; set; }
    bool VisibleInCurrentVisibilityState { get; }

  METHODS:
    object[] GetAllowedValues()

--------------------------------------------------------------------------------

[CLASS] DynamicBlockReferencePropertyCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicBlockReferencePropertyCollection
Inherits: DisposableWrapper
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    DynamicBlockReferenceProperty Item { get; }

  METHODS:
    void CopyTo(DynamicBlockReferenceProperty[] array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] DynamicBlockReferencePropertyCollectionEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicBlockReferencePropertyCollectionEnumerator
Implements: IEnumerator

  PROPERTIES:
    object Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[ENUM] DynamicBlockReferencePropertyUnitsType
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicBlockReferencePropertyUnitsType
Inherits: Enum

  Values:
    - NoUnits = 0
    - Angular = 1
    - Distance = 2
    - Area = 3

--------------------------------------------------------------------------------

[CLASS] DynamicDimensionChangedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicDimensionChangedEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new DynamicDimensionChangedEventArgs(int index, double value)

  PROPERTIES:
    int Index { get; }
    double Value { get; }

--------------------------------------------------------------------------------

[CLASS] DynamicDimensionData
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicDimensionData
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new DynamicDimensionData()
    new DynamicDimensionData(Dimension dimension)
    new DynamicDimensionData(Dimension dimension, bool editable)
    new DynamicDimensionData(Dimension dimension, bool editable, bool hideIfValueIsZero)

  PROPERTIES:
    object ApplicationData { get; set; }
    Dimension Dimension { get; set; }
    bool Editable { get; set; }
    bool Focal { get; set; }
    bool HideIfValueIsZero { get; set; }
    bool Visible { get; set; }

  METHODS:
    static DynamicDimensionData Create(IntPtr unmanagedPointer, bool autoDelete)

--------------------------------------------------------------------------------

[CLASS] DynamicDimensionDataCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.DynamicDimensionDataCollection
Inherits: DisposableWrapper
Implements: ICollection, IEnumerable

  CONSTRUCTORS:
    new DynamicDimensionDataCollection()

  PROPERTIES:
    int Count { get; }
    DynamicDimensionData Item { get; set; }

  METHODS:
    int Add(DynamicDimensionData data)
    void Clear()
    void CopyTo(DynamicDimensionData[] array, int index)
    IEnumerator GetEnumerator()
    void RemoveAt(int i)

--------------------------------------------------------------------------------

