# Autodesk.AutoCAD.DatabaseServices.Filters

NAMESPACE: Autodesk.AutoCAD.DatabaseServices.Filters
--------------------------------------------------------------------------------

[ABSTRACT CLASS] Filter
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.Filter
Inherits: DBObject

  PROPERTIES:
    RXClass IndexClass { get; }

--------------------------------------------------------------------------------

[CLASS] FilteredBlockIterator
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.FilteredBlockIterator
Inherits: DisposableWrapper

  PROPERTIES:
    bool BuffersForComposition { get; }
    double EstimatedHitFraction { get; }
    ObjectId Id { get; }
    ObjectId Next { get; }

  METHODS:
    bool Accepts(ObjectId id)
    void AddToBuffer(ObjectId id)
    void Seek(ObjectId id)
    void Start()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Index
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.Index
Inherits: DBObject

  PROPERTIES:
    bool IsUptoDate { get; }
    DateTime LastUpdatedAt { get; set; }
    DateTime LastUpdatedAtU { get; set; }
    ObjectId ObjectBeingIndexedId { get; }

  METHODS:
    FilteredBlockIterator GetIterator(Filter filter)
    void RebuildFull(IndexUpdateData indexData)

--------------------------------------------------------------------------------

[CLASS] IndexUpdateData
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.IndexUpdateData
Inherits: DisposableWrapper

  METHODS:
    void AddId(ObjectId id)
    long GetIdData(ObjectId id)
    IntPtr GetIdDataPtr(ObjectId id)
    byte GetIdFlags(ObjectId id)
    void SetIdData(ObjectId id, IntPtr data)
    void SetIdData(ObjectId id, int data)
    void SetIdFlags(ObjectId id, byte flags)

--------------------------------------------------------------------------------

[CLASS] LayerFilter
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.LayerFilter
Inherits: Filter

  CONSTRUCTORS:
    new LayerFilter()

  PROPERTIES:
    bool IsValid { get; }
    int LayerCount { get; }

  METHODS:
    void Add(string layer)
    void GetAt(int index, string name)
    void Remove(string layer)

--------------------------------------------------------------------------------

[CLASS] LayerIndex
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.LayerIndex
Inherits: Index

  CONSTRUCTORS:
    new LayerIndex()

  METHODS:
    void Compute(LayerTable pLT, BlockTableRecord record)

--------------------------------------------------------------------------------

[CLASS] SpatialFilter
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilter
Inherits: Filter

  CONSTRUCTORS:
    new SpatialFilter()

  PROPERTIES:
    Matrix3d ClipSpaceToWorldCoordinateSystemTransform { get; }
    SpatialFilterDefinition Definition { get; set; }
    bool HasPerspectiveCamera { get; }
    bool Inverted { get; set; }
    Matrix3d OriginalInverseBlockTransform { get; }

  METHODS:
    bool ClipVolumeIntersectsExtents(Extents3d ext)
    Extents3d GetQueryBounds()
    Extents3d GetQueryBounds(BlockReference blockReference)
    SpatialFilterVolume GetVolume()
    void SetPerspectiveCamera(Point3d fromPoint)

--------------------------------------------------------------------------------

[STRUCT] SpatialFilterDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterDefinition

  CONSTRUCTORS:
    new SpatialFilterDefinition(Point2dCollection pts, Vector3d normal, double elevation, double frontClip, double backClip, bool enabled)

  PROPERTIES:
    double BackClip { get; }
    double Elevation { get; }
    bool Enabled { get; }
    double FrontClip { get; }
    Vector3d Normal { get; }

  METHODS:
    Point2dCollection GetPoints()

--------------------------------------------------------------------------------

[STRUCT] SpatialFilterVolume
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterVolume

  CONSTRUCTORS:
    new SpatialFilterVolume(Point3d fromPoint, Point3d toPoint, Vector3d upDir, Vector2d viewField)

  PROPERTIES:
    Point3d FromPoint { get; }
    Point3d ToPoint { get; }
    Vector3d UpDirection { get; }
    Vector2d ViewField { get; }

--------------------------------------------------------------------------------

[CLASS] SpatialIndex
Full Name: Autodesk.AutoCAD.DatabaseServices.Filters.SpatialIndex
Inherits: Index

  CONSTRUCTORS:
    new SpatialIndex()

--------------------------------------------------------------------------------

