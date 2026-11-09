# Autodesk.AutoCAD.LayerManager

NAMESPACE: Autodesk.AutoCAD.LayerManager
--------------------------------------------------------------------------------

[CLASS] AndExpression
Full Name: Autodesk.AutoCAD.LayerManager.AndExpression

  METHODS:
    RelationalExpression[] GetRelationalExpressions()

--------------------------------------------------------------------------------

[CLASS] LayerCollection
Full Name: Autodesk.AutoCAD.LayerManager.LayerCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    ObjectId Item { get; }

  METHODS:
    void Add(ObjectId value)
    void Clear()
    bool Contains(ObjectId value)
    void CopyTo(ObjectId[] array, int index)
    IEnumerator GetEnumerator()
    void Remove(ObjectId value)

--------------------------------------------------------------------------------

[CLASS] LayerFilter
Full Name: Autodesk.AutoCAD.LayerManager.LayerFilter
Inherits: RXObject

  CONSTRUCTORS:
    new LayerFilter()

  PROPERTIES:
    bool AllowDelete { get; }
    bool AllowNested { get; }
    bool AllowRename { get; }
    LayerFilterDisplayImages DisplayImages { get; }
    bool DynamicallyGenerated { get; }
    string FilterExpression { get; set; }
    bool IsIdFilter { get; }
    bool IsProxy { get; }
    string Name { get; set; }
    LayerFilterCollection NestedFilters { get; }
    LayerFilter Parent { get; }

  METHODS:
    bool CompareTo(LayerFilter otherFilter)
    bool Filter(LayerTableRecord layer)
    void GenerateNested()
    AndExpression[] GetFilterExpressionTree()
    DialogResult ShowEditor()

--------------------------------------------------------------------------------

[CLASS] LayerFilterCollection
Full Name: Autodesk.AutoCAD.LayerManager.LayerFilterCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    LayerFilter Item { get; }

  METHODS:
    void Add(LayerFilter value)
    void Clear()
    bool Contains(LayerFilter value)
    void CopyTo(LayerFilter[] array, int index)
    IEnumerator GetEnumerator()
    void Remove(LayerFilter value)

--------------------------------------------------------------------------------

[STRUCT] LayerFilterDisplayImages
Full Name: Autodesk.AutoCAD.LayerManager.LayerFilterDisplayImages

  CONSTRUCTORS:
    new LayerFilterDisplayImages(IntPtr imageListHandle, int imageIndex, int selectedImageIndex)

  PROPERTIES:
    int ImageIndex { get; }
    IntPtr ImageListHandle { get; }
    int SelectedImageIndex { get; }

--------------------------------------------------------------------------------

[STRUCT] LayerFilterTree
Full Name: Autodesk.AutoCAD.LayerManager.LayerFilterTree

  CONSTRUCTORS:
    new LayerFilterTree(LayerFilter root, LayerFilter current)

  PROPERTIES:
    LayerFilter Current { get; }
    LayerFilter Root { get; }

--------------------------------------------------------------------------------

[CLASS] LayerGroup
Full Name: Autodesk.AutoCAD.LayerManager.LayerGroup
Inherits: LayerFilter

  CONSTRUCTORS:
    new LayerGroup()

  PROPERTIES:
    LayerCollection LayerIds { get; }

--------------------------------------------------------------------------------

[CLASS] RelationalExpression
Full Name: Autodesk.AutoCAD.LayerManager.RelationalExpression

  PROPERTIES:
    string Constant { get; }
    string Variable { get; }

--------------------------------------------------------------------------------

