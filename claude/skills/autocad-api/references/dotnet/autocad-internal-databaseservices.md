# Autodesk.AutoCAD.Internal.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.Internal.DatabaseServices
--------------------------------------------------------------------------------

[CLASS] Block1PointParameter
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.Block1PointParameter
Inherits: BlockParameter

--------------------------------------------------------------------------------

[CLASS] Block2PointParameter
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.Block2PointParameter
Inherits: BlockParameter

--------------------------------------------------------------------------------

[CLASS] BlockAction
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockAction
Inherits: BlockElement

--------------------------------------------------------------------------------

[CLASS] BlockActionEntity
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockActionEntity
Inherits: BlockElementEntity

--------------------------------------------------------------------------------

[CLASS] BlockElement
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockElement
Inherits: EvalConnectable

  PROPERTIES:
    string Name { get; set; }

--------------------------------------------------------------------------------

[CLASS] BlockElementEntity
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockElementEntity
Inherits: Entity

  PROPERTIES:
    ObjectId Element { get; }

--------------------------------------------------------------------------------

[CLASS] BlockFlipParameter
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockFlipParameter
Inherits: Block2PointParameter

  PROPERTIES:
    string BaseStateLabel { get; }
    string FlippedStateLabel { get; }

--------------------------------------------------------------------------------

[CLASS] BlockLookupAction
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockLookupAction
Inherits: BlockAction

  PROPERTIES:
    int NumberOfInputColumns { get; }
    int NumberOfOutputColumns { get; }
    int NumberOfRows { get; }

  METHODS:
    static bool DuplicateCellsInLookupColumn(Array aryTable, LookupColumnDescriptorCollection descArray, int colIdx, out Array outArray)
    static bool DuplicateRowsOverInputColumns(Array aryTable, LookupColumnDescriptorCollection descArray, out Array outArray)
    void GetLookupTable(out Array pDataTable, out LookupColumnDescriptorCollection descArray)
    static bool NonSingletonRangeInInputColumns(Array aryTable, LookupColumnDescriptorCollection descArray, out Array outArray)
    static bool NullsInInputColumns(Array aryTable, LookupColumnDescriptorCollection descArray, out Array outArray)
    void SetLookupTable(Array aryTable, LookupColumnDescriptorCollection descArray)

--------------------------------------------------------------------------------

[CLASS] BlockLookupActionEntity
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockLookupActionEntity
Inherits: BlockActionEntity

--------------------------------------------------------------------------------

[CLASS] BlockLookupParameter
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockLookupParameter
Inherits: Block1PointParameter

--------------------------------------------------------------------------------

[CLASS] BlockParameter
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockParameter
Inherits: BlockElement

  PROPERTIES:
    BlockParameterPropertyDescriptorCollection PropertyDescription { get; }

  METHODS:
    string GetPropertyConnectionName(string propName)
    object GetPropertyValue(string name, ref Matrix3d xform)
    object GetPropertyValue(string name)

--------------------------------------------------------------------------------

[CLASS] BlockParameterPropertyDescriptor
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockParameterPropertyDescriptor

  PROPERTIES:
    string ConnectionName { get; }
    bool HasValueSet { get; }
    string PropertyDescription { get; }
    string PropertyName { get; }
    short PropertyType { get; }
    bool ReadOnly { get; }
    UnitsType UnitsType { get; }
    Array ValueSetValues { get; }
    bool Visible { get; }

--------------------------------------------------------------------------------

[CLASS] BlockParameterPropertyDescriptorCollection
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockParameterPropertyDescriptorCollection
Inherits: DisposableWrapper
Implements: ICollection, IEnumerable

  CONSTRUCTORS:
    new BlockParameterPropertyDescriptorCollection()

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    BlockParameterPropertyDescriptor Item { get; }
    object SyncRoot { get; }

  METHODS:
    void CopyTo(BlockParameterPropertyDescriptor[] array, int size)
    IEnumerator GetEnumerator()
    void ICollectionCopyTo(Array array, int size)

--------------------------------------------------------------------------------

[CLASS] BlockUserParameter
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.BlockUserParameter
Inherits: Block1PointParameter

  PROPERTIES:
    UserParameterType UserParamType { get; }

--------------------------------------------------------------------------------

[CLASS] EvalConnectable
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.EvalConnectable
Inherits: EvalExpr

--------------------------------------------------------------------------------

[CLASS] EvalExpr
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.EvalExpr
Inherits: DBObject

--------------------------------------------------------------------------------

[CLASS] EvalGraph
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.EvalGraph
Inherits: DBObject

  METHODS:
    int[] GetAllNodes()
    DBObject GetNode(uint nodeId, OpenMode mode, Transaction pTrans)

--------------------------------------------------------------------------------

[CLASS] LookupColumnDescriptor
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.LookupColumnDescriptor
Inherits: RXObject

  CONSTRUCTORS:
    new LookupColumnDescriptor()

  PROPERTIES:
    uint ConnectableId { get; set; }
    string ConnectionName { get; set; }
    bool IsInvertible { get; set; }
    bool IsOutputColumn { get; set; }
    short PropertyType { get; set; }
    UnitsType PropertyUnits { get; set; }
    string UnmatchedValue { get; set; }

--------------------------------------------------------------------------------

[CLASS] LookupColumnDescriptorCollection
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.LookupColumnDescriptorCollection
Inherits: DisposableWrapper
Implements: ICollection, IEnumerable

  CONSTRUCTORS:
    new LookupColumnDescriptorCollection()

  PROPERTIES:
    int Count { get; }
    bool IsSynchronized { get; }
    LookupColumnDescriptor Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(LookupColumnDescriptor value)
    void Clear()
    void CopyTo(LookupColumnDescriptor[] array, int size)
    IEnumerator GetEnumerator()
    void ICollectionCopyTo(Array array, int size)
    void Insert(int index, LookupColumnDescriptor value)
    void Remove(LookupColumnDescriptor value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ENUM] UnitsType
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.UnitsType
Inherits: Enum

  Values:
    - NoUnits = 0
    - Angular = 1
    - Distance = 2
    - Area = 3

--------------------------------------------------------------------------------

[ENUM] UserParameterType
Full Name: Autodesk.AutoCAD.Internal.DatabaseServices.UserParameterType
Inherits: Enum

  Values:
    - Distance = 0
    - Area = 1
    - Volume = 2
    - Scalar = 3
    - Angle = 4
    - String = 5

--------------------------------------------------------------------------------

