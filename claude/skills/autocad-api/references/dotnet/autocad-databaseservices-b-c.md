# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[CLASS] Background
Full Name: Autodesk.AutoCAD.DatabaseServices.Background
Inherits: DBObject

  METHODS:
    static ObjectId GetBackgroundDictionaryId(Database database, bool createIfNotPresent)

--------------------------------------------------------------------------------

[CLASS] BeginInsertEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginInsertEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database From { get; }

--------------------------------------------------------------------------------

[CLASS] BeginInsertEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginInsertEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginInsertEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginInsertEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginInsertEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginWblockBlockEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockBlockEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId BlockId { get; }
    Database From { get; }

--------------------------------------------------------------------------------

[CLASS] BeginWblockBlockEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockBlockEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginWblockBlockEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginWblockBlockEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginWblockBlockEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginWblockEntireDatabaseEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockEntireDatabaseEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database From { get; }

--------------------------------------------------------------------------------

[CLASS] BeginWblockEntireDatabaseEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockEntireDatabaseEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginWblockEntireDatabaseEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginWblockEntireDatabaseEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginWblockEntireDatabaseEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginWblockObjectsEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockObjectsEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database From { get; }
    IdMapping IdMapping { get; }

--------------------------------------------------------------------------------

[CLASS] BeginWblockObjectsEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockObjectsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginWblockObjectsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginWblockObjectsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginWblockObjectsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BeginWblockSelectedObjectsEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockSelectedObjectsEventArgs
Inherits: EventArgs

  PROPERTIES:
    Database From { get; }
    Point3d InsertionPoint { get; }

--------------------------------------------------------------------------------

[CLASS] BeginWblockSelectedObjectsEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.BeginWblockSelectedObjectsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BeginWblockSelectedObjectsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BeginWblockSelectedObjectsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BeginWblockSelectedObjectsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BlendOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.BlendOptions
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new BlendOptions()

  PROPERTIES:
    Nullable<Vector3d> CoplanarDirection { get; }
    Nullable<Point3d> CoplanarPoint { get; }
    DriveModeType DriveMode { get; }
    uint Quality { get; }
    bool Simplify { get; }
    bool Solid { get; }

  METHODS:
    object Clone()

--------------------------------------------------------------------------------

[CLASS] BlendOptionsBuilder
Full Name: Autodesk.AutoCAD.DatabaseServices.BlendOptionsBuilder

  CONSTRUCTORS:
    new BlendOptionsBuilder(BlendOptions value)
    new BlendOptionsBuilder()

  PROPERTIES:
    Nullable<Vector3d> CoplanarDirection { get; set; }
    Nullable<Point3d> CoplanarPoint { get; set; }
    DriveModeType DriveMode { get; set; }
    uint Quality { get; set; }
    bool Simplify { get; set; }
    bool Solid { get; set; }

  METHODS:
    BlendOptions ToBlendOptions()

--------------------------------------------------------------------------------

[CLASS] BlockBegin
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockBegin
Inherits: Entity

  CONSTRUCTORS:
    new BlockBegin()

--------------------------------------------------------------------------------

[ENUM] BlockConnectionType
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockConnectionType
Inherits: Enum

  Values:
    - ConnectExtents = 0
    - ConnectBase = 1

--------------------------------------------------------------------------------

[CLASS] BlockEnd
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockEnd
Inherits: Entity

  CONSTRUCTORS:
    new BlockEnd()

--------------------------------------------------------------------------------

[CLASS] BlockInsertionPointsEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockInsertionPointsEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BlockInsertionPointsEventArgs(BlockTableRecord blockTableRecord, Point3dCollection points, Vector3dCollection alignmentVectors)

  PROPERTIES:
    Vector3dCollection AlignmentVectors { get; }
    BlockTableRecord BlockTableRecord { get; }
    Point3dCollection InsertionPoints { get; }

--------------------------------------------------------------------------------

[CLASS] BlockInsertionPointsEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockInsertionPointsEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new BlockInsertionPointsEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, BlockInsertionPointsEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, BlockInsertionPointsEventArgs e)

--------------------------------------------------------------------------------

[CLASS] BlockPropertiesTable
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockPropertiesTable
Inherits: DBObject

  CONSTRUCTORS:
    new BlockPropertiesTable(IntPtr unmanagedPointer, bool autoDelete)

  PROPERTIES:
    BlockPropertiesTableColumnCollection Columns { get; }
    bool ContainsRuntimeParametersOnly { get; set; }
    int DefaultActiveRowIndex { get; set; }
    bool IsDisabledInDrawingEditor { get; }
    bool MustMatch { get; set; }
    BlockPropertiesTableRowCollection Rows { get; }

  METHODS:
    AuditError[] Audit()

--------------------------------------------------------------------------------

[CLASS] BlockPropertiesTableColumn
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockPropertiesTableColumn

  PROPERTIES:
    bool Constant { get; set; }
    ObjectId CustomProperties { get; set; }
    ResultBuffer DefaultValue { get; set; }
    bool Editable { get; set; }
    string Format { get; set; }
    IParameter Parameter { get; }
    bool Removable { get; set; }
    BlockPropertiesTable Table { get; set; }
    ResultBuffer UnmatchedValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] BlockPropertiesTableColumnCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockPropertiesTableColumnCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    BlockPropertiesTableColumn Item { get; }
    object SyncRoot { get; }

  METHODS:
    void AddAt(ObjectId id, string parameterName, int index)
    void AddItem(ObjectId id, string parameterName)
    void Clear()
    bool ContainsColumn(ObjectId id, string parameterName)
    void CopyTo(Array array, int index)
    IEnumerator GetEnumerator()
    int GetIndex(BlockPropertiesTableColumn column)
    void Move(int index, int newIndex)
    void Move(BlockPropertiesTableColumn column, int newIndex)
    void Remove(BlockPropertiesTableColumn column)
    void Remove(int index)

--------------------------------------------------------------------------------

[CLASS] BlockPropertiesTableRow
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockPropertiesTableRow
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    ResultBuffer Item { get; set; }
    ResultBuffer Item { get; set; }
    object SyncRoot { get; }
    BlockPropertiesTable Table { get; set; }

  METHODS:
    void CopyTo(Array __unnamed000, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] BlockPropertiesTableRowCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockPropertiesTableRowCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    BlockPropertiesTableRow Item { get; }
    object SyncRoot { get; }

  METHODS:
    void AddAt(int index)
    void AddItem()
    void Clear()
    void CopyTo(Array array, int index)
    IEnumerator GetEnumerator()
    int GetIndex(BlockPropertiesTableRow row)
    void Move(int index, int newIndex)
    void Move(BlockPropertiesTableRow row, int newIndex)
    void Remove(BlockPropertiesTableRow row)
    void Remove(int index)
    void Sort(BlockPropertiesTableColumn column, bool ascending)

--------------------------------------------------------------------------------

[CLASS] BlockReference
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockReference
Inherits: Entity

  CONSTRUCTORS:
    new BlockReference(Point3d position, ObjectId blockTableRecord)

  PROPERTIES:
    ObjectId AnonymousBlockTableRecord { get; }
    AttributeCollection AttributeCollection { get; }
    ObjectId BlockTableRecord { get; set; }
    Matrix3d BlockTransform { get; set; }
    UnitsValue BlockUnit { get; set; }
    DynamicBlockReferencePropertyCollection DynamicBlockReferencePropertyCollection { get; }
    ObjectId DynamicBlockTableRecord { get; }
    bool IsDynamicBlock { get; }
    string Name { get; }
    Vector3d Normal { get; set; }
    Point3d Position { get; set; }
    double Rotation { get; set; }
    Scale3d ScaleFactors { get; set; }
    bool TreatAsBlockRefForExplode { get; }
    double UnitFactor { get; }

  METHODS:
    void ConvertToStaticBlock(string newBlockName)
    void ConvertToStaticBlock()
    void ExplodeToOwnerSpace()
    Extents3d GeometryExtentsBestFit()
    Extents3d GeometryExtentsBestFit(Matrix3d parentTransform)
    void ResetBlock()

--------------------------------------------------------------------------------

[ENUM] BlockScaling
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockScaling
Inherits: Enum

  Values:
    - Any = 0
    - Uniform = 1

--------------------------------------------------------------------------------

[CLASS] BlockTable
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] BlockTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockTableRecord
Inherits: SymbolTableRecord
Implements: IEnumerable

  CONSTRUCTORS:
    new BlockTableRecord()

  PROPERTIES:
    ObjectId BlockBeginId { get; }
    ObjectId BlockEndId { get; }
    BlockScaling BlockScaling { get; set; }
    string Comments { get; set; }
    ObjectId DrawOrderTableId { get; }
    bool Explodable { get; set; }
    bool HasAttributeDefinitions { get; }
    bool HasPreviewIcon { get; }
    HyperLinkCollection Hyperlinks { get; }
    BlockTableRecord IncludingErased { get; }
    bool IsAnonymous { get; }
    bool IsDynamicBlock { get; }
    bool IsFromExternalReference { get; }
    bool IsFromOverlayReference { get; set; }
    bool IsLayout { get; }
    bool IsUnloaded { get; set; }
    ObjectId LayoutId { get; set; }
    Point3d Origin { get; set; }
    string PathName { get; set; }
        [SKIPPED PROPERTY] PreviewIcon: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    UnitsValue Units { get; set; }
    XrefStatus XrefStatus { get; }

  METHODS:
    ObjectId AppendEntity(Entity entity)
    void AssumeOwnershipOf(ObjectIdCollection entitiesToMove)
    ObjectIdCollection GetAnonymousBlockIds()
    ObjectIdCollection GetBlockReferenceIds(bool directOnly, bool forceValidity)
    BlockTableRecordEnumerator GetEnumerator()
    ObjectIdCollection GetErasedBlockReferenceIds()
    Database GetXrefDatabase(bool incrementUnresolved)
    void UpdateAnonymousBlocks()

  EVENTS:
    event BlockInsertionPointsEventHandler BlockInsertionPoints

  CONSTANTS/STATIC FIELDS:
    const string ModelSpace
    const string PaperSpace

--------------------------------------------------------------------------------

[CLASS] BlockTableRecordEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.BlockTableRecordEnumerator
Inherits: DisposableWrapper
Implements: IEnumerator

  PROPERTIES:
    ObjectId Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] Body
Full Name: Autodesk.AutoCAD.DatabaseServices.Body
Inherits: Entity

  CONSTRUCTORS:
    new Body()

  PROPERTIES:
    bool IsNull { get; }
    int NumChanges { get; }
    bool UsesGraphicsCache { get; }

  METHODS:
    static DBObjectCollection AcisIn(string fileName)
    static void AcisOut(string fileName, DBObjectCollection entitiesOutToFile)

--------------------------------------------------------------------------------

[ENUM] BooleanOperationType
Full Name: Autodesk.AutoCAD.DatabaseServices.BooleanOperationType
Inherits: Enum

  Values:
    - BoolUnite = 0
    - BoolIntersect = 1
    - BoolSubtract = 2

--------------------------------------------------------------------------------

[CLASS] BulgeVertex
Full Name: Autodesk.AutoCAD.DatabaseServices.BulgeVertex

  CONSTRUCTORS:
    new BulgeVertex(Point2d point, double bulge)

  PROPERTIES:
    double Bulge { get; set; }
    Point2d Vertex { get; set; }

--------------------------------------------------------------------------------

[CLASS] BulgeVertexCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.BulgeVertexCollection
Inherits: CollectionBase

  CONSTRUCTORS:
    new BulgeVertexCollection()

  PROPERTIES:
    BulgeVertex Item { get; set; }

  METHODS:
    int Add(BulgeVertex value)
    bool Contains(BulgeVertex value)
    void CopyTo(BulgeVertex[] array, int index)
    int IndexOf(BulgeVertex value)
    void Insert(int index, BulgeVertex value)
    void Remove(BulgeVertex value)

--------------------------------------------------------------------------------

[CLASS] CallerMustCloseAttribute
Full Name: Autodesk.AutoCAD.DatabaseServices.CallerMustCloseAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new CallerMustCloseAttribute()

--------------------------------------------------------------------------------

[CLASS] Cell
Full Name: Autodesk.AutoCAD.DatabaseServices.Cell
Inherits: CellRange

  CONSTRUCTORS:
    new Cell(Table table, int row, int column)

  PROPERTIES:
    Point3d AttachmentPoint { get; }
    ObjectId BlockTableRecordId { get; set; }
    TableCellType CellType { get; }
    int Column { get; }
    CellContentsCollection Contents { get; }
    CellContentTypes ContentTypes { get; }
    ObjectId DataLink { get; set; }
    ObjectId FieldId { get; set; }
    int Row { get; }
    string TextString { get; set; }
    string ToolTip { get; set; }
    object Value { get; set; }

  METHODS:
    string GetBlockAttributeValue(ObjectId attDefId)
    CellRange GetDataLinkRange()
    Point3dCollection GetExtents()
    CellRange GetMergeRange()
    string GetTextString(FormatOption formatOption)
    object GetValue(FormatOption formatOption)
    void RemoveDataLink()
    void ResetValue()
    void SetBlockAttributeValue(ObjectId attDefId, string value)
    void SetValue(object value, ParseOption parseOption)
    void UpdateDataLink(UpdateDirection dir, UpdateOption option)

--------------------------------------------------------------------------------

[ENUM] CellAlignment
Full Name: Autodesk.AutoCAD.DatabaseServices.CellAlignment
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

[CLASS] CellBorder
Full Name: Autodesk.AutoCAD.DatabaseServices.CellBorder

  PROPERTIES:
    Color Color { get; set; }
    Nullable<double> DoubleLineSpacing { get; set; }
    Nullable<bool> IsVisible { get; set; }
    Nullable<GridLineStyle> LineStyle { get; set; }
    Nullable<ObjectId> Linetype { get; set; }
    Nullable<LineWeight> LineWeight { get; set; }
    Nullable<double> Margin { get; set; }
    Nullable<GridProperties> Overrides { get; set; }

--------------------------------------------------------------------------------

[CLASS] CellBorders
Full Name: Autodesk.AutoCAD.DatabaseServices.CellBorders

  PROPERTIES:
    CellBorder Bottom { get; }
    CellBorder Horizontal { get; }
    CellBorder Left { get; }
    CellBorder Right { get; }
    CellBorder Top { get; }
    CellBorder Vertical { get; }

--------------------------------------------------------------------------------

[ENUM] CellClass
Full Name: Autodesk.AutoCAD.DatabaseServices.CellClass
Inherits: Enum

  Values:
    - None = 0
    - Label = 1
    - Data = 2

--------------------------------------------------------------------------------

[CLASS] CellContent
Full Name: Autodesk.AutoCAD.DatabaseServices.CellContent

  PROPERTIES:
    ObjectId BlockTableRecordId { get; set; }
    Color ContentColor { get; set; }
    CellContentTypes ContentTypes { get; }
    string DataFormat { get; set; }
    DataTypeParameter DataType { get; set; }
    ObjectId FieldId { get; set; }
    string Formula { get; set; }
    bool HasFormula { get; }
    bool IsAutoScale { get; set; }
    CellProperties Overrides { get; set; }
    double Rotation { get; set; }
    double Scale { get; set; }
    double TextHeight { get; set; }
    string TextString { get; set; }
    ObjectId TextStyleId { get; set; }
    object Value { get; set; }

  METHODS:
    void DeleteContent()
    string GetBlockAttributeValue(ObjectId attDefId)
    string GetTextString(FormatOption formatOption)
    object GetValue(FormatOption formatOption)
    void SetBlockAttributeValue(ObjectId attDefId, string value)
    void SetValue(object value, ParseOption parseOption)

--------------------------------------------------------------------------------

[ENUM] CellContentLayout
Full Name: Autodesk.AutoCAD.DatabaseServices.CellContentLayout
Inherits: Enum

  Values:
    - Flow = 1
    - StackedHorizontal = 2
    - StackedVertical = 4

--------------------------------------------------------------------------------

[CLASS] CellContentsCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.CellContentsCollection
Implements: IEnumerable<CellContent>, IEnumerable

  PROPERTIES:
    int Count { get; }
    CellContent Item { get; }

  METHODS:
    void Add()
    void Clear()
    IEnumerator<CellContent> GetEnumerator()
    void InsertAt(int index)
    void Move(int fromIndex, int toIndex)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ENUM] CellContentTypes
Full Name: Autodesk.AutoCAD.DatabaseServices.CellContentTypes
Inherits: Enum

  Values:
    - Unknown = 0
    - Value = 1
    - Field = 2
    - Block = 4

--------------------------------------------------------------------------------

[ENUM] CellEdgeMasks
Full Name: Autodesk.AutoCAD.DatabaseServices.CellEdgeMasks
Inherits: Enum

  Values:
    - TopMask = 1
    - RightMask = 2
    - BottomMask = 4
    - LeftMask = 8

--------------------------------------------------------------------------------

[ENUM] CellMargins
Full Name: Autodesk.AutoCAD.DatabaseServices.CellMargins
Inherits: Enum

  Values:
    - Top = 1
    - Left = 2
    - Bottom = 4
    - Right = 8

--------------------------------------------------------------------------------

[ENUM] CellOption
Full Name: Autodesk.AutoCAD.DatabaseServices.CellOption
Inherits: Enum

  Values:
    - None = 0
    - InheritCellFormat = 1

--------------------------------------------------------------------------------

[ENUM] CellProperties
Full Name: Autodesk.AutoCAD.DatabaseServices.CellProperties
Inherits: Enum

  Values:
    - Invalid = 0
    - DataType = 1
    - DataFormat = 2
    - Rotation = 4
    - Scale = 8
    - Alignment = 16
    - ContentColor = 32
    - TextStyle = 64
    - TextHeight = 128
    - AutoScale = 256
    - BackgroundColor = 512
    - MarginLeft = 1024
    - MarginTop = 2048
    - MarginRight = 4096
    - MarginBottom = 8192
    - ContentLayout = 16384
    - MergeAll = 32768
    - FlowDirBtoT = 65536
    - MarginHorzSpacing = 131072
    - MarginVertSpacing = 262144

--------------------------------------------------------------------------------

[CLASS] CellRange
Full Name: Autodesk.AutoCAD.DatabaseServices.CellRange
Implements: ISubObject, IEnumerable<CellReference>, IEnumerable

  PROPERTIES:
    Nullable<CellAlignment> Alignment { get; set; }
    Color BackgroundColor { get; set; }
    CellBorders Borders { get; }
    Cell BottomRight { get; }
    Cell BottomRightPlusOne { get; }
    int BottomRow { get; }
    bool CanDeleteColumns { get; }
    bool CanDeleteRows { get; }
    bool CanInsertColumn { get; }
    bool CanInsertRow { get; }
    Color ContentColor { get; set; }
    Nullable<CellContentLayout> ContentLayout { get; set; }
    string DataFormat { get; set; }
    Nullable<DataTypeParameter> DataType { get; set; }
    Nullable<bool> IsBackgroundColorNone { get; set; }
    Nullable<bool> IsContentEditable { get; }
    Nullable<bool> IsEmpty { get; }
    Nullable<bool> IsFormatEditable { get; }
    Nullable<bool> IsLinked { get; }
    Nullable<bool> IsMergeAllEnabled { get; set; }
    Nullable<bool> IsMerged { get; }
    bool IsNull { get; }
    bool IsSingleCell { get; }
    Cell Item { get; }
    int LeftColumn { get; }
    Table ParentTable { get; }
    int RightColumn { get; }
    Nullable<CellStates> State { get; set; }
    string Style { get; set; }
    Nullable<double> TextHeight { get; set; }
    Nullable<ObjectId> TextStyleId { get; set; }
    Cell TopLeft { get; }
    int TopRow { get; }

  METHODS:
    void ClearStyleOverrides()
    static CellRange Create(Table table, int topRow, int leftColumn, int bottomRow, int rightColumn)
    void DeleteContent()
    bool Equals(object pRange)
    object GetCustomData(string key)
    ObjectIdCollection GetDataLink()
    IEnumerator<CellReference> GetEnumerator()
    int GetHashCode()
    TableStyleOverride[] GetStyleOverrides()
    IEnumerator IEnumerableGetEnumerator()
    void SetCustomData(string key, object value)
    void SetDataLink(ObjectId dataLinkId, bool bUpdate)

--------------------------------------------------------------------------------

[STRUCT] CellReference
Full Name: Autodesk.AutoCAD.DatabaseServices.CellReference

  PROPERTIES:
    int Column { get; set; }
    int Row { get; set; }

--------------------------------------------------------------------------------

[ENUM] CellStates
Full Name: Autodesk.AutoCAD.DatabaseServices.CellStates
Inherits: Enum

  Values:
    - None = 0
    - ContentLocked = 1
    - ContentReadOnly = 2
    - Linked = 4
    - ContentModifiedAfterUpdate = 8
    - FormatLocked = 16
    - FormatReadOnly = 32
    - FormatModifiedAfterUpdate = 64

--------------------------------------------------------------------------------

[ENUM] CellType
Full Name: Autodesk.AutoCAD.DatabaseServices.CellType
Inherits: Enum

  Values:
    - Unknown = 0
    - Integer = 1
    - Double = 2
    - CharPtr = 3
    - Point = 4
    - ObjectId = 5
    - HardOwnerId = 6
    - SoftOwnerId = 7
    - HardPtrId = 8
    - SoftPtrId = 9
    - Bool = 10
    - Vector = 11

--------------------------------------------------------------------------------

[CLASS] CenterPointConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.CenterPointConstraint
Inherits: ConcentricConstraint

--------------------------------------------------------------------------------

[CLASS] Circle
Full Name: Autodesk.AutoCAD.DatabaseServices.Circle
Inherits: Curve

  CONSTRUCTORS:
    new Circle(Point3d center, Vector3d normal, double radius)
    new Circle()

  PROPERTIES:
    Point3d Center { get; set; }
    double Circumference { get; set; }
    double Diameter { get; set; }
    Vector3d Normal { get; set; }
    double Radius { get; set; }
    double Thickness { get; set; }

--------------------------------------------------------------------------------

[ENUM] ClipBoundaryType
Full Name: Autodesk.AutoCAD.DatabaseServices.ClipBoundaryType
Inherits: Enum

  Values:
    - Invalid = 0
    - Rectangle = 1
    - Poly = 2

--------------------------------------------------------------------------------

[CLASS] ColinearConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.ColinearConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new ColinearConstraint()

--------------------------------------------------------------------------------

[ENUM] CollisionType
Full Name: Autodesk.AutoCAD.DatabaseServices.CollisionType
Inherits: Enum

  Values:
    - None = 0
    - Solid = 1

--------------------------------------------------------------------------------

[CLASS] Column
Full Name: Autodesk.AutoCAD.DatabaseServices.Column
Inherits: CellRange

  PROPERTIES:
    double MinimumWidth { get; }
    string Name { get; set; }
    double Width { get; set; }

--------------------------------------------------------------------------------

[CLASS] ColumnsCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.ColumnsCollection
Implements: IEnumerable<Column>, IEnumerable

  PROPERTIES:
    int Count { get; }
    Column Item { get; }

  METHODS:
    IEnumerator<Column> GetEnumerator()

--------------------------------------------------------------------------------

[ENUM] ColumnType
Full Name: Autodesk.AutoCAD.DatabaseServices.ColumnType
Inherits: Enum

  Values:
    - NoColumns = 0
    - StaticColumns = 1
    - DynamicColumns = 2

--------------------------------------------------------------------------------

[CLASS] CompositeConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.CompositeConstraint
Inherits: GeometricalConstraint

  PROPERTIES:
    GeometricalConstraint[] OwnedConstraints { get; }

--------------------------------------------------------------------------------

[CLASS] CompoundObjectId
Full Name: Autodesk.AutoCAD.DatabaseServices.CompoundObjectId
Inherits: RXObject

  CONSTRUCTORS:
    new CompoundObjectId(ObjectId id, ObjectIdCollection path, Database hostDatabase)
    new CompoundObjectId(CompoundObjectId A_0)
    new CompoundObjectId(ObjectId id, Database hostDatabase)
    new CompoundObjectId(ObjectId id)
    new CompoundObjectId()

  PROPERTIES:
    ObjectIdCollection FullPath { get; }
    bool IsEmpty { get; }
    bool IsExternal { get; }
    bool IsSimpleObjectId { get; }
    ObjectId LeafId { get; }
    ObjectIdCollection Path { get; }
    StatusType Status { get; }
    ObjectId TopId { get; }
    Matrix3d Transform { get; }

  METHODS:
    void DwgInFields(DwgFiler filer, int ownerVersion)
    void DwgOutFields(DwgFiler filer, Database hostDatabase)
    void DxfInFields(DxfFiler filer, Database hostDatabase, int ownerVersion)
    void DxfOutFields(DxfFiler filer, Database hostDatabase)
    bool IsValid(int validityCheckingLevel)
    static CompoundObjectId NullId()
    bool Remap(IdMapping idMap)
    void Set(ObjectId id, ObjectIdCollection path, Database hostDatabase)
    void Set(CompoundObjectId compoundID, Database hostDatabase)
    void Set(ObjectId id, Database hostDatabase)
    void SetEmpty()
    void SetFullPath(ObjectIdCollection fullPath, Database hostDatabase)

--------------------------------------------------------------------------------

[CLASS] ConcentricConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.ConcentricConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new ConcentricConstraint()

--------------------------------------------------------------------------------

[CLASS] Constrained2PointsConstructionLine
Full Name: Autodesk.AutoCAD.DatabaseServices.Constrained2PointsConstructionLine
Inherits: ConstrainedConstructionLine

  CONSTRUCTORS:
    new Constrained2PointsConstructionLine(Point3d point1, Point3d point2)
    new Constrained2PointsConstructionLine()

--------------------------------------------------------------------------------

[CLASS] ConstrainedArc
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedArc
Inherits: ConstrainedCircle

  CONSTRUCTORS:
    new ConstrainedArc(ObjectId geomDependencyId)
    new ConstrainedArc()

  PROPERTIES:
    Point3d EndPoint { get; }
    Point3d MidPoint { get; }
    Point3d StartPoint { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedBoundedEllipse
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedBoundedEllipse
Inherits: ConstrainedEllipse

  CONSTRUCTORS:
    new ConstrainedBoundedEllipse(ObjectId geomDependencyId)
    new ConstrainedBoundedEllipse()

  PROPERTIES:
    Point3d EndPoint { get; }
    Point3d StartPoint { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedBoundedLine
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedBoundedLine
Inherits: ConstrainedLine

  CONSTRUCTORS:
    new ConstrainedBoundedLine(ObjectId geomDependencyId, bool ray)
    new ConstrainedBoundedLine()

  PROPERTIES:
    Point3d EndPoint { get; }
    bool IsRay { get; }
    Point3d MidPoint { get; }
    Point3d StartPoint { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedCircle
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedCircle
Inherits: ConstrainedCurve

  CONSTRUCTORS:
    new ConstrainedCircle(ObjectId geomDependencyId)
    new ConstrainedCircle()

  PROPERTIES:
    Point3d CenterPoint { get; }
    double Radius { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedConstructionLine
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedConstructionLine
Inherits: ConstrainedLine

--------------------------------------------------------------------------------

[CLASS] ConstrainedCurve
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedCurve
Inherits: ConstrainedGeometry

  PROPERTIES:
    ConstrainedImplicitPoint[] ConstrainedImplicitPoints { get; }
    bool IsBounded { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedDatumLine
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedDatumLine
Inherits: ConstrainedConstructionLine

  CONSTRUCTORS:
    new ConstrainedDatumLine(Point3d pointOnLine, Vector3d direction)
    new ConstrainedDatumLine()

--------------------------------------------------------------------------------

[CLASS] ConstrainedEllipse
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedEllipse
Inherits: ConstrainedCurve

  CONSTRUCTORS:
    new ConstrainedEllipse(ObjectId geomDependencyId)
    new ConstrainedEllipse()

  PROPERTIES:
    Point3d CenterPoint { get; }
    Vector3d Direction { get; }
    double MajorRadius { get; }
    double MinorRadius { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedGeometry
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedGeometry
Inherits: ConstraintGroupNode

  PROPERTIES:
    GeometricalConstraint[] ConnectedConstraints { get; }
    ConstrainedGeometry[] ConnectedGeometries { get; }
    FullSubentityPath[] FullSubentityPaths { get; }
    ObjectId GeomDependencyId { get; }
    bool IsReadOnly { get; }
    ConstrainedRigidSet OwningRigidSet { get; }

  METHODS:
    GeometricalConstraint[] CommonConstraints(ConstrainedGeometry otherConsGeom)

--------------------------------------------------------------------------------

[CLASS] ConstrainedImplicitPoint
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedImplicitPoint
Inherits: ConstrainedPoint

  CONSTRUCTORS:
    new ConstrainedImplicitPoint(uint constrCurvId, ImplicitPointType ptype, int index)
    new ConstrainedImplicitPoint()

  PROPERTIES:
    uint ConstrainedCurveId { get; }
    Point3d Point { get; }
    int PointIndex { get; }
    ImplicitPointType PointType { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedLine
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedLine
Inherits: ConstrainedCurve

  CONSTRUCTORS:
    new ConstrainedLine(ObjectId geomDependencyId)
    new ConstrainedLine()

  PROPERTIES:
    Vector3d Direction { get; }
    Point3d PointOnLine { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedPoint
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedPoint
Inherits: ConstrainedGeometry

  CONSTRUCTORS:
    new ConstrainedPoint(ObjectId geomDependencyId)
    new ConstrainedPoint()

  PROPERTIES:
    Point3d Point { get; }

--------------------------------------------------------------------------------

[CLASS] ConstrainedRigidSet
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedRigidSet
Inherits: ConstrainedGeometry

  CONSTRUCTORS:
    new ConstrainedRigidSet(bool bScalable, Matrix3d trans)
    new ConstrainedRigidSet()

  PROPERTIES:
    int NumOfConstrainedGeoms { get; }
    Matrix3d Transform { get; }

  METHODS:
    ConstrainedGeometry GetConstrainedGeomAt(int index)

--------------------------------------------------------------------------------

[CLASS] ConstrainedSpline
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainedSpline
Inherits: ConstrainedCurve

  CONSTRUCTORS:
    new ConstrainedSpline(ObjectId geomDependencyId, NurbCurve3d spline)
    new ConstrainedSpline()

  PROPERTIES:
    int NumOfDefinePoints { get; }
    NurbCurve3d NurbSpline { get; }

  METHODS:
    Point3d DefinePointAt(int index)

--------------------------------------------------------------------------------

[CLASS] ConstraintGroupNode
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstraintGroupNode
Inherits: RXObject

  PROPERTIES:
    uint NodeId { get; }
    ObjectId OwningConstraintGroupId { get; }

--------------------------------------------------------------------------------

[ENUM] ConstrainType
Full Name: Autodesk.AutoCAD.DatabaseServices.ConstrainType
Inherits: Enum

  Values:
    - TurnHeight = 0
    - Turns = 1
    - Height = 2

--------------------------------------------------------------------------------

[ENUM] ContentType
Full Name: Autodesk.AutoCAD.DatabaseServices.ContentType
Inherits: Enum

  Values:
    - NoneContent = 0
    - BlockContent = 1
    - MTextContent = 2
    - ToleranceContent = 3

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Curve
Full Name: Autodesk.AutoCAD.DatabaseServices.Curve
Inherits: Entity

  PROPERTIES:
    double Area { get; }
    bool Closed { get; }
    double EndParam { get; }
    Point3d EndPoint { get; set; }
    bool IsPeriodic { get; }
    Spline Spline { get; }
    double StartParam { get; }
    Point3d StartPoint { get; set; }

  METHODS:
    static Curve CreateFromGeCurve(Curve3d geCurve)
    static Curve CreateFromGeCurve(Curve3d geCurve, Tolerance tolerance)
    static Curve CreateFromGeCurve(Curve3d geCurve, Vector3d __unnamed001)
    static Curve CreateFromGeCurve(Curve3d geCurve, Vector3d __unnamed001, Tolerance tolerance)
    void Extend(bool extendStart, Point3d toPoint)
    void Extend(double newParameter)
    Point3d GetClosestPointTo(Point3d givenPoint, Vector3d direction, bool extend)
    Point3d GetClosestPointTo(Point3d givenPoint, bool extend)
    double GetDistanceAtParameter(double value)
    double GetDistAtPoint(Point3d point)
    Vector3d GetFirstDerivative(Point3d point)
    Vector3d GetFirstDerivative(double value)
    Curve3d GetGeCurve()
    Curve3d GetGeCurve(Tolerance tolerance)
    DBObjectCollection GetOffsetCurves(double offsetDist)
    DBObjectCollection GetOffsetCurvesGivenPlaneNormal(Vector3d normal, double offsetDist)
    Curve GetOrthoProjectedCurve(Plane planeToProjectOn)
    double GetParameterAtDistance(double dist)
    double GetParameterAtPoint(Point3d point)
    Point3d GetPointAtDist(double value)
    Point3d GetPointAtParameter(double value)
    Curve GetProjectedCurve(Plane planeToProjectOn, Vector3d projectionDirection)
    Vector3d GetSecondDerivative(Point3d point)
    Vector3d GetSecondDerivative(double value)
    DBObjectCollection GetSplitCurves(Point3dCollection points)
    DBObjectCollection GetSplitCurves(DoubleCollection value)
    void ReverseCurve()
    void SetFromGeCurve(Curve3d geCurve)
    void SetFromGeCurve(Curve3d geCurve, Tolerance tolerance)
    void SetFromGeCurve(Curve3d geCurve, Vector3d __unnamed001)
    void SetFromGeCurve(Curve3d geCurve, Vector3d __unnamed001, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] CustomObjectSnapMode
Full Name: Autodesk.AutoCAD.DatabaseServices.CustomObjectSnapMode
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CustomObjectSnapMode(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    new CustomObjectSnapMode(string localModeString, string globalModeString, string toolTipText, Glyph glyph)

  PROPERTIES:
        [SKIPPED PROPERTY] Bitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    string DisplayString { get; }
    string GlobalModeString { get; }
    Glyph Glyph { get; }
    static int GlyphSize { get; }
    string LocalModeString { get; }
    string ToolTipText { get; }

  METHODS:
    static void Activate(string mode)
    void ApplyToEntityType(RXClass entity, AddObjectSnapInfo callback)
    static void Deactivate(string mode)
    static bool IsActive(string mode)
    void RemoveFromEntityType(RXClass entity)

--------------------------------------------------------------------------------

[STRUCT] CustomScale
Full Name: Autodesk.AutoCAD.DatabaseServices.CustomScale

  CONSTRUCTORS:
    new CustomScale(double numerator, double denominator)

  PROPERTIES:
    double Denominator { get; }
    double Numerator { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(CustomScale a)
    bool IsEqualTo(CustomScale a, Tolerance tolerance)
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

