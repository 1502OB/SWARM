# Autodesk.AutoCAD.Geometry

NAMESPACE: Autodesk.AutoCAD.Geometry
--------------------------------------------------------------------------------

[STRUCT] Vector2d
Full Name: Autodesk.AutoCAD.Geometry.Vector2d
Implements: IFormattable

  CONSTRUCTORS:
    new Vector2d(double[] A_0)
    new Vector2d(double x, double y)

  PROPERTIES:
    double Angle { get; }
    double Coordinate { get; }
    double Length { get; }
    double LengthSqrd { get; }
    double X { get; }
    static Vector2d XAxis { get; }
    double Y { get; }
    static Vector2d YAxis { get; }

  METHODS:
    Vector2d Add(Vector2d v)
    Vector2d DivideBy(double value)
    double DotProduct(Vector2d v)
    bool Equals(object obj)
    double GetAngleTo(Vector2d vector)
    int GetHashCode()
    Vector2d GetNormal()
    Vector2d GetNormal(Tolerance tolerance)
    Vector2d GetPerpendicularVector()
    bool IsCodirectionalTo(Vector2d vector)
    bool IsCodirectionalTo(Vector2d vector, Tolerance tolerance)
    bool IsEqualTo(Vector2d vector)
    bool IsEqualTo(Vector2d vector, Tolerance tolerance)
    bool IsParallelTo(Vector2d vector)
    bool IsParallelTo(Vector2d vector, Tolerance tolerance)
    bool IsPerpendicularTo(Vector2d vector)
    bool IsPerpendicularTo(Vector2d vector, Tolerance tolerance)
    bool IsUnitLength()
    bool IsUnitLength(Tolerance tolerance)
    bool IsZeroLength()
    bool IsZeroLength(Tolerance tolerance)
    Vector2d Mirror(Vector2d line)
    Vector2d MultiplyBy(double value)
    Vector2d Negate()
    Vector2d RotateBy(double angle)
    Vector2d Subtract(Vector2d v)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    Vector2d TransformBy(Matrix2d leftSide)

--------------------------------------------------------------------------------

[CLASS] Vector2dCollection
Full Name: Autodesk.AutoCAD.Geometry.Vector2dCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Vector2dCollection(Vector2d[] values)
    new Vector2dCollection(int capacity)
    new Vector2dCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    Vector2d Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(Vector2d value)
    void AddRange(Vector2d[] range)
    void Clear()
    bool Contains(Vector2d value)
    void CopyTo(Vector2d[] array, int index)
    Vector2dCollectionEnumerator GetEnumerator()
    int IndexOf(Vector2d value)
    void Insert(int index, Vector2d value)
    void Remove(Vector2d value)
    void RemoveAt(int index)
    Vector2d[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] Vector2dCollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.Vector2dCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new Vector2dCollectionEnumerator(Vector2dCollection col)

  PROPERTIES:
    Vector2d Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[STRUCT] Vector3d
Full Name: Autodesk.AutoCAD.Geometry.Vector3d
Implements: IFormattable

  CONSTRUCTORS:
    new Vector3d(PlanarEntity plane, Vector2d vector2d)
    new Vector3d(double[] xyz)
    new Vector3d(double x, double y, double z)

  PROPERTIES:
    double Coordinate { get; }
    int LargestElement { get; }
    double Length { get; }
    double LengthSqrd { get; }
    double X { get; }
    static Vector3d XAxis { get; }
    double Y { get; }
    static Vector3d YAxis { get; }
    double Z { get; }
    static Vector3d ZAxis { get; }

  METHODS:
    Vector3d Add(Vector3d v)
    double AngleOnPlane(PlanarEntity plane)
    Vector2d Convert2d(PlanarEntity plane)
    Vector3d CrossProduct(Vector3d vector)
    Vector3d DivideBy(double value)
    double DotProduct(Vector3d v)
    bool Equals(object obj)
    double GetAngleTo(Vector3d vector, Vector3d referenceVector)
    double GetAngleTo(Vector3d vector)
    int GetHashCode()
    Vector3d GetNormal()
    Vector3d GetNormal(Tolerance tolerance)
    Vector3d GetPerpendicularVector()
    bool IsCodirectionalTo(Vector3d vector)
    bool IsCodirectionalTo(Vector3d vector, Tolerance tolerance)
    bool IsEqualTo(Vector3d vector)
    bool IsEqualTo(Vector3d vector, Tolerance tolerance)
    bool IsParallelTo(Vector3d vector)
    bool IsParallelTo(Vector3d vector, Tolerance tolerance)
    bool IsPerpendicularTo(Vector3d vector)
    bool IsPerpendicularTo(Vector3d vector, Tolerance tolerance)
    bool IsUnitLength()
    bool IsUnitLength(Tolerance tolerance)
    bool IsZeroLength()
    bool IsZeroLength(Tolerance tolerance)
    Vector3d Mirror(Vector3d normalToPlane)
    Vector3d MultiplyBy(double value)
    Vector3d Negate()
    Vector3d OrthoProjectTo(Vector3d planeNormal)
    Vector3d ProjectTo(Vector3d planeNormal, Vector3d projectDirection)
    Vector3d RotateBy(double angle, Vector3d axis)
    Vector3d Subtract(Vector3d v)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    Vector3d TransformBy(Matrix3d leftSide)

--------------------------------------------------------------------------------

[CLASS] Vector3dCollection
Full Name: Autodesk.AutoCAD.Geometry.Vector3dCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Vector3dCollection(Vector3d[] values)
    new Vector3dCollection(int capacity)
    new Vector3dCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    Vector3d Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(Vector3d value)
    void AddRange(Vector3d[] range)
    void Clear()
    bool Contains(Vector3d value)
    void CopyTo(Vector3d[] array, int index)
    Vector3dCollectionEnumerator GetEnumerator()
    int IndexOf(Vector3d value)
    void Insert(int index, Vector3d value)
    void Remove(Vector3d value)
    void RemoveAt(int index)
    Vector3d[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] Vector3dCollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.Vector3dCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new Vector3dCollectionEnumerator(Vector3dCollection col)

  PROPERTIES:
    Vector3d Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

