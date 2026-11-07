# Autodesk.AutoCAD.Geometry

NAMESPACE: Autodesk.AutoCAD.Geometry
--------------------------------------------------------------------------------

[STRUCT] Matrix2d
Full Name: Autodesk.AutoCAD.Geometry.Matrix2d
Implements: IFormattable

  CONSTRUCTORS:
    new Matrix2d(double[] data)

  PROPERTIES:
    CoordinateSystem2d CoordinateSystem2d { get; }
    double ElementAt { get; }
    Vector2d Translation { get; }

  METHODS:
    static Matrix2d AlignCoordinateSystem(Point2d fromOrigin, Vector2d fromE0, Vector2d fromE1, Point2d toOrigin, Vector2d toE0, Vector2d toE1)
    static Matrix2d Displacement(Vector2d vector)
    bool Equals(object obj)
    double GetDeterminant()
    int GetHashCode()
    double GetScale()
    Matrix2d Inverse()
    bool IsConformal(Matrix2dInfo info)
    bool IsEqualTo(Matrix2d a)
    bool IsEqualTo(Matrix2d a, Tolerance tolerance)
    bool IsScaledOrtho()
    bool IsScaledOrtho(Tolerance tolerance)
    bool IsSingular()
    bool IsSingular(Tolerance tolerance)
    bool IsUniscaledOrtho()
    bool IsUniscaledOrtho(Tolerance tolerance)
    static Matrix2d Mirroring(Line2d line)
    static Matrix2d Mirroring(Point2d point)
    Matrix2d Multiply(Matrix2d matrix)
    Matrix2d PostMultiplyBy(Matrix2d rightSide)
    Matrix2d PreMultiplyBy(Matrix2d leftSide)
    static Matrix2d Rotation(double angle, Point2d center)
    static Matrix2d Scaling(double scaleAll, Point2d center)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    Matrix2d Transpose()

--------------------------------------------------------------------------------

[CLASS] Matrix2dBuilder
Full Name: Autodesk.AutoCAD.Geometry.Matrix2dBuilder

  CONSTRUCTORS:
    new Matrix2dBuilder(Matrix2d value)
    new Matrix2dBuilder()

  PROPERTIES:
    double ElementAt { get; set; }

  METHODS:
    Matrix2d ToMatrix2d()

--------------------------------------------------------------------------------

[STRUCT] Matrix2dInfo
Full Name: Autodesk.AutoCAD.Geometry.Matrix2dInfo

  PROPERTIES:
    double Angle { get; }
    bool IsMirror { get; }
    Vector2d Reflex { get; }
    double Scale { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(Matrix2dInfo a)
    bool IsEqualTo(Matrix2dInfo a, Tolerance tolerance)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[STRUCT] Matrix3d
Full Name: Autodesk.AutoCAD.Geometry.Matrix3d
Implements: IFormattable

  CONSTRUCTORS:
    new Matrix3d(double[] data)

  PROPERTIES:
    CoordinateSystem3d CoordinateSystem3d { get; }
    double ElementAt { get; }
    static Matrix3d Identity { get; }
    double Normal { get; }
    Vector3d Translation { get; }

  METHODS:
    static Matrix3d AlignCoordinateSystem(Point3d fromOrigin, Vector3d fromXAxis, Vector3d fromYAxis, Vector3d fromZAxis, Point3d toOrigin, Vector3d toXAxis, Vector3d toYAxis, Vector3d toZAxis)
    static Matrix3d Displacement(Vector3d vector)
    bool Equals(object obj)
    double GetDeterminant()
    int GetHashCode()
    double GetScale()
    Matrix3d Inverse()
    bool IsEqualTo(Matrix3d mat)
    bool IsEqualTo(Matrix3d mat, Tolerance tolerance)
    bool IsInverse(Matrix3d inv)
    bool IsInverse(Matrix3d inv, Tolerance tolerance)
    bool IsScaledOrtho()
    bool IsScaledOrtho(Tolerance tolerance)
    bool IsSingular()
    bool IsSingular(Tolerance tolerance)
    bool IsUniscaledOrtho()
    bool IsUniscaledOrtho(Tolerance tolerance)
    static Matrix3d Mirroring(Point3d pointValue)
    static Matrix3d Mirroring(Plane plane)
    static Matrix3d Mirroring(Line3d line)
    static Matrix3d PlaneToWorld(Vector3d normal)
    static Matrix3d PlaneToWorld(Plane plane)
    Matrix3d PostMultiplyBy(Matrix3d rightSide)
    Matrix3d PreMultiplyBy(Matrix3d leftSide)
    static Matrix3d Projection(Plane projectionPlane, Vector3d projectDir)
    static Matrix3d Rotation(double angle, Vector3d axis, Point3d center)
    static Matrix3d Scaling(double scaleAll, Point3d center)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    Matrix3d Transpose()
    static Matrix3d WorldToPlane(Vector3d normal)
    static Matrix3d WorldToPlane(Plane plane)

--------------------------------------------------------------------------------

[CLASS] Matrix3dBuilder
Full Name: Autodesk.AutoCAD.Geometry.Matrix3dBuilder

  CONSTRUCTORS:
    new Matrix3dBuilder(Matrix3d value)
    new Matrix3dBuilder()

  PROPERTIES:
    double ElementAt { get; set; }

  METHODS:
    Matrix3d ToMatrix3d()

--------------------------------------------------------------------------------

[CLASS] NurbCurve2d
Full Name: Autodesk.AutoCAD.Geometry.NurbCurve2d
Inherits: SplineEntity2d

  CONSTRUCTORS:
    new NurbCurve2d(LineSegment2d linSegment)
    new NurbCurve2d(EllipticalArc2d ellipse)
    new NurbCurve2d(Point2dCollection fitPoints, Vector2dCollection fitTangents, bool periodic)
    new NurbCurve2d(Point2dCollection fitPoints, Vector2dCollection fitTangents, Tolerance fitTolerance, bool periodic)
    new NurbCurve2d(Point2dCollection fitPoints)
    new NurbCurve2d(Point2dCollection fitPoints, Tolerance fitTolerance)
    new NurbCurve2d(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, bool startTangentDefined, bool endTangentDefined, KnotParameterizationEnum knotParam)
    new NurbCurve2d(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, bool startTangentDefined, bool endTangentDefined, KnotParameterizationEnum knotParam, Tolerance fitTolerance)
    new NurbCurve2d(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, bool startTangentDefined, bool endTangentDefined)
    new NurbCurve2d(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, bool startTangentDefined, bool endTangentDefined, Tolerance fitTolerance)
    new NurbCurve2d(int degree, PolylineCurve2d fitPolyline, bool periodic)
    new NurbCurve2d(int degree, KnotCollection knots, Point2dCollection controlPoints, DoubleCollection weights, bool periodic)
    new NurbCurve2d(int degree, KnotCollection knots, Point2dCollection controlPoints, bool periodic)
    new NurbCurve2d()

  PROPERTIES:
    NurbCurve2dData DefinitionData { get; }
    bool EvalMode { get; }
    NurbCurve2dFitData FitData { get; }
    KnotParameterizationEnum FitKnotParameterization { get; set; }
    Tolerance FitTolerance { get; set; }
    int NumFitPoints { get; }
    int NumWeights { get; }

  METHODS:
    void AddControlPointAt(double newKnot, Point2d point, double weight)
    bool AddFitPointAt(int index, Point2d point)
    void AddKnot(double newKnot)
    static NurbCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    void DeleteControlPointAt(int index)
    bool DeleteFitPointAt(int index)
    void ElevateDegree(int plusDegree)
    Point2d GetFitPointAt(int index)
    Vector2d[] GetFitTangents()
    DoubleCollection GetParametersOfC1Discontinuity()
    DoubleCollection GetParametersOfC1Discontinuity(Tolerance tolerance)
    DoubleCollection GetParametersOfG1Discontinuity()
    DoubleCollection GetParametersOfG1Discontinuity(Tolerance tolerance)
    double GetWeightAt(int index)
    void HardTrimByParams(double newStartParameter, double newEndParameter)
    void InsertKnot(double newKnot)
    void JoinWith(NurbCurve2d curve)
    void MakeClosed()
    void MakeNonPeriodic()
    void MakeOpen()
    void MakePeriodic()
    void MakeRational(double weight)
    bool PurgeFitData()
    void SetEvaluateMode(bool evaluateMode)
    void SetFitData(int degree, Point2dCollection fitPoints)
    void SetFitData(int degree, Point2dCollection fitPoints, Tolerance fitTolerance)
    void SetFitData(KnotCollection fitKnots, Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, bool periodic)
    void SetFitData(KnotCollection fitKnots, Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, Tolerance fitTolerance, bool periodic)
    void SetFitData(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, KnotParameterizationEnum knotParam, Tolerance fitTolerance)
    void SetFitData(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent)
    void SetFitData(Point2dCollection fitPoints, Vector2d startTangent, Vector2d endTangent, Tolerance fitTolerance)
    bool SetFitPointAt(int index, Point2d point)
    bool SetFitTangents(Vector2d startTangent, Vector2d endTangent)
    void SetWeightAt(int index, double value)

--------------------------------------------------------------------------------

[STRUCT] NurbCurve2dData
Full Name: Autodesk.AutoCAD.Geometry.NurbCurve2dData

  PROPERTIES:
    Point2dCollection ControlPoints { get; }
    int Degree { get; }
    KnotCollection Knots { get; }
    bool Periodic { get; }
    bool Rational { get; }
    DoubleCollection Weights { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(NurbCurve2dData a)

--------------------------------------------------------------------------------

[STRUCT] NurbCurve2dFitData
Full Name: Autodesk.AutoCAD.Geometry.NurbCurve2dFitData

  PROPERTIES:
    int Degree { get; }
    Vector2d EndTangent { get; }
    Point2dCollection FitPoints { get; }
    Tolerance FitTolerance { get; }
    KnotParameterizationEnum KnotParam { get; }
    Vector2d StartTangent { get; }
    bool TangentsExist { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(NurbCurve2dFitData a)

--------------------------------------------------------------------------------

[CLASS] NurbCurve3d
Full Name: Autodesk.AutoCAD.Geometry.NurbCurve3d
Inherits: SplineEntity3d

  CONSTRUCTORS:
    new NurbCurve3d(LineSegment3d lineSegment)
    new NurbCurve3d(EllipticalArc3d ellipse)
    new NurbCurve3d(Point3dCollection fitPoints, Vector3dCollection fitTangents, bool periodic)
    new NurbCurve3d(Point3dCollection fitPoints, Vector3dCollection fitTangents, Tolerance fitTolerance, bool periodic)
    new NurbCurve3d(Point3dCollection fitPoints)
    new NurbCurve3d(Point3dCollection fitPoints, Tolerance fitTolerance)
    new NurbCurve3d(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, bool startTangentDefined, bool endTangentDefined, KnotParameterizationEnum knotParam)
    new NurbCurve3d(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, bool startTangentDefined, bool endTangentDefined, KnotParameterizationEnum knotParam, Tolerance fitTolerance)
    new NurbCurve3d(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, bool startTangentDefined, bool endTangentDefined)
    new NurbCurve3d(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, bool startTangentDefined, bool endTangentDefined, Tolerance fitTolerance)
    new NurbCurve3d(int degree, PolylineCurve3d fitPolyline, bool periodic)
    new NurbCurve3d(int degree, KnotCollection knots, Point3dCollection controlPoints, DoubleCollection weights, bool periodic)
    new NurbCurve3d(int degree, KnotCollection knots, Point3dCollection controlPoints, bool periodic)
    new NurbCurve3d()

  PROPERTIES:
    NurbCurve3dData DefinitionData { get; }
    bool EvalMode { get; }
    NurbCurve3dFitData FitData { get; }
    KnotParameterizationEnum FitKnotParameterization { get; set; }
    Tolerance FitTolerance { get; set; }
    int NumFitPoints { get; }
    int NumWeights { get; }

  METHODS:
    void AddControlPointAt(double newKnot, Point3d point, double weight)
    bool AddFitPointAt(int index, Point3d point)
    void AddKnot(double newKnot)
    static NurbCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    void DeleteControlPointAt(int index)
    bool DeleteFitPointAt(int index)
    void ElevateDegree(int plusDegree)
    Point3d GetFitPointAt(int index)
    Vector3d[] GetFitTangents()
    DoubleCollection GetParametersOfC1Discontinuity()
    DoubleCollection GetParametersOfC1Discontinuity(Tolerance tolerance)
    DoubleCollection GetParametersOfG1Discontinuity()
    DoubleCollection GetParametersOfG1Discontinuity(Tolerance tolerance)
    double GetWeightAt(int index)
    void HardTrimByParams(double newStartParameter, double newEndParameter)
    void InsertKnot(double newKnot)
    void JoinWith(NurbCurve3d curve)
    void MakeClosed()
    void MakeNonPeriodic()
    void MakeOpen()
    void MakePeriodic()
    void MakeRational(double weight)
    bool PurgeFitData()
    void SetEvaluateMode(bool evaluateMode)
    void SetFitData(int degree, Point3dCollection fitPoints)
    void SetFitData(int degree, Point3dCollection fitPoints, Tolerance fitTolerance)
    void SetFitData(KnotCollection fitKnots, Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, bool periodic)
    void SetFitData(KnotCollection fitKnots, Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, Tolerance fitTolerance, bool periodic)
    void SetFitData(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, KnotParameterizationEnum knotParam, Tolerance fitTolerance)
    void SetFitData(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent)
    void SetFitData(Point3dCollection fitPoints, Vector3d startTangent, Vector3d endTangent, Tolerance fitTolerance)
    bool SetFitPointAt(int index, Point3d point)
    bool SetFitTangents(Vector3d startTangent, Vector3d endTangent)
    void SetWeightAt(int index, double value)

--------------------------------------------------------------------------------

[STRUCT] NurbCurve3dData
Full Name: Autodesk.AutoCAD.Geometry.NurbCurve3dData

  PROPERTIES:
    Point3dCollection ControlPoints { get; }
    int Degree { get; }
    KnotCollection Knots { get; }
    bool Periodic { get; }
    bool Rational { get; }
    DoubleCollection Weights { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(NurbCurve3dData a)

--------------------------------------------------------------------------------

[STRUCT] NurbCurve3dFitData
Full Name: Autodesk.AutoCAD.Geometry.NurbCurve3dFitData

  PROPERTIES:
    int Degree { get; }
    Vector3d EndTangent { get; }
    Point3dCollection FitPoints { get; }
    Tolerance FitTolerance { get; }
    KnotParameterizationEnum KnotParam { get; }
    Vector3d StartTangent { get; }
    bool TangentsExist { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(NurbCurve3dFitData a)

--------------------------------------------------------------------------------

[CLASS] NurbSurface
Full Name: Autodesk.AutoCAD.Geometry.NurbSurface
Inherits: Surface

  CONSTRUCTORS:
    new NurbSurface(int degreeU, int degreeV, int propsInU, int propertiesInV, int numberControlPointsInU, int numberControlPointsInV, Point3dCollection controlPoints, DoubleCollection weights, KnotCollection knotsInU, KnotCollection knotsInV)
    new NurbSurface(int degreeU, int degreeV, int propsInU, int propertiesInV, int numberControlPointsInU, int numberControlPointsInV, Point3dCollection controlPoints, DoubleCollection weights, KnotCollection knotsInU, KnotCollection knotsInV, Tolerance tolerance)
    new NurbSurface()

  PROPERTIES:
    Point3dCollection ControlPoints { get; }
    int DegreeInU { get; }
    int DegreeInV { get; }
    bool IsPeriodicInU { get; }
    bool IsPeriodicInV { get; }
    bool IsRationalInU { get; }
    bool IsRationalInV { get; }
    int NumControlPointsInU { get; }
    int NumControlPointsInV { get; }
    int NumKnotsInU { get; }
    int NumKnotsInV { get; }
    double PeriodicInU { get; }
    double PeriodicInV { get; }
    int SingularityInU { get; }
    int SingularityInV { get; }
    KnotCollection UKnots { get; }
    KnotCollection VKnots { get; }
    DoubleCollection Weights { get; }

  METHODS:
    static NurbSurface Create(IntPtr unmanagedPointer, bool autoDelete)
    NurbSurfaceDefinition GetDefinition()
    void Set(int degreeU, int degreeV, int propsInU, int propsInV, int numControlPointsInU, int numControlPointsInV, Point3dCollection controlPoints, DoubleCollection weights, KnotCollection knotsInU, KnotCollection knotsInV)
    void Set(int degreeU, int degreeV, int propsInU, int propsInV, int numControlPointsInU, int numControlPointsInV, Point3dCollection controlPoints, DoubleCollection weights, KnotCollection knotsInU, KnotCollection knotsInV, Tolerance tolerance)

--------------------------------------------------------------------------------

[STRUCT] NurbSurfaceDefinition
Full Name: Autodesk.AutoCAD.Geometry.NurbSurfaceDefinition

  PROPERTIES:
    Point3dCollection ControlPoints { get; }
    int DegreeInU { get; }
    int DegreeInV { get; }
    int NumberOfControlPointsInU { get; }
    int NumberOfControlPointsInV { get; }
    int PropertiesInU { get; }
    int PropertiesInV { get; }
    KnotCollection UKnots { get; }
    KnotCollection VKnots { get; }
    DoubleCollection Weights { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(NurbSurfaceDefinition a)

--------------------------------------------------------------------------------

[CLASS] OffsetCurve2d
Full Name: Autodesk.AutoCAD.Geometry.OffsetCurve2d
Inherits: Curve2d

  CONSTRUCTORS:
    new OffsetCurve2d(Curve2d baseCurve, double offsetDistance)

  PROPERTIES:
    Curve2d Curve { get; set; }
    double OffsetDistance { get; set; }
    bool ParameterDirection { get; }
    Matrix2d Transformation { get; }

  METHODS:
    static OffsetCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)

--------------------------------------------------------------------------------

[CLASS] OffsetCurve3d
Full Name: Autodesk.AutoCAD.Geometry.OffsetCurve3d
Inherits: Curve3d

  CONSTRUCTORS:
    new OffsetCurve3d(Curve3d baseCurve, Vector3d planeNormal, double offsetDistance)

  PROPERTIES:
    Curve3d Curve { get; set; }
    Vector3d Normal { get; set; }
    double OffsetDistance { get; set; }
    bool ParameterDirection { get; }
    Matrix3d Transformation { get; }

  METHODS:
    static OffsetCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)

--------------------------------------------------------------------------------

[ENUM] OffsetCurveExtensionType
Full Name: Autodesk.AutoCAD.Geometry.OffsetCurveExtensionType
Inherits: Enum

  Values:
    - Fillet = 0
    - Chamfer = 1
    - Extend = 2

--------------------------------------------------------------------------------

[CLASS] OffsetSurface
Full Name: Autodesk.AutoCAD.Geometry.OffsetSurface
Inherits: Surface

  CONSTRUCTORS:
    new OffsetSurface(Surface baseSurface, double offsetDistance)
    new OffsetSurface()

  PROPERTIES:
    Surface ConstructionSurface { get; }
    bool IsBoundedPlane { get; }
    bool IsCone { get; }
    bool IsCylinder { get; }
    bool IsPlane { get; }
    bool IsSphere { get; }
    bool IsTorus { get; }
    double OffsetDist { get; }
    Surface OriginalSurface { get; }

  METHODS:
    static OffsetSurface Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(Surface surface, double offsetDistance)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PlanarEntity
Full Name: Autodesk.AutoCAD.Geometry.PlanarEntity
Inherits: Surface

  PROPERTIES:
    PlanarEquationCoefficients Coefficients { get; }
    Vector3d Normal { get; }
    Point3d PointOnPlane { get; }

  METHODS:
    Point3d[] ClosestPointToLinearEntity(LinearEntity3d line)
    Point3d[] ClosestPointToLinearEntity(LinearEntity3d line, Tolerance tolerance)
    Point3d[] ClosestPointToPlanarEntity(PlanarEntity otherPlane)
    Point3d[] ClosestPointToPlanarEntity(PlanarEntity otherPlane, Tolerance tolerance)
    static PlanarEntity Create(IntPtr unmanagedPointer, bool autoDelete)
    CoordinateSystem3d GetCoordinateSystem()
    Point3d[] IntersectWith(LinearEntity3d linearEntity)
    Point3d[] IntersectWith(LinearEntity3d linearEntity, Tolerance tolerance)
    bool IsCoplanarTo(PlanarEntity otherPlanarEntity)
    bool IsCoplanarTo(PlanarEntity otherPlanarEntity, Tolerance tolerance)
    bool IsParallelTo(PlanarEntity otherPlanarEntity)
    bool IsParallelTo(PlanarEntity otherPlanarEntity, Tolerance tolerance)
    bool IsParallelTo(LinearEntity3d linearEntity)
    bool IsParallelTo(LinearEntity3d linearEntity, Tolerance tolerance)
    bool IsPerpendicularTo(PlanarEntity linearEntity)
    bool IsPerpendicularTo(PlanarEntity linearEntity, Tolerance tolerance)
    bool IsPerpendicularTo(LinearEntity3d linearEntity)
    bool IsPerpendicularTo(LinearEntity3d linearEntity, Tolerance tolerance)

--------------------------------------------------------------------------------

[STRUCT] PlanarEquationCoefficients
Full Name: Autodesk.AutoCAD.Geometry.PlanarEquationCoefficients

  CONSTRUCTORS:
    new PlanarEquationCoefficients(double a, double b, double c, double d)

  PROPERTIES:
    double A { get; }
    double B { get; }
    double C { get; }
    double D { get; }

--------------------------------------------------------------------------------

[CLASS] Plane
Full Name: Autodesk.AutoCAD.Geometry.Plane
Inherits: PlanarEntity

  CONSTRUCTORS:
    new Plane(double a, double b, double c, double d)
    new Plane(Point3d origin, Vector3d u, Vector3d v)
    new Plane(Point3d u, Point3d origin, Point3d v)
    new Plane(Point3d origin, Vector3d normal)
    new Plane(Plane source)
    new Plane()

  METHODS:
    static Plane Create(IntPtr unmanagedPointer, bool autoDelete)
    double GetSignedDistanceTo(Point3d pointValue)
    LineSegment3d IntersectWith(BoundedPlane boundPlane)
    LineSegment3d IntersectWith(BoundedPlane boundPlane, Tolerance tolerance)
    Line3d IntersectWith(Plane otherPlane)
    Line3d IntersectWith(Plane otherPlane, Tolerance tolerance)
    void Set(Point3d origin, Vector3d u, Vector3d v)
    void Set(double a, double b, double c, double d)
    void Set(Point3d u, Point3d origin, Point3d v)
    void Set(Point3d pointValue, Vector3d normal)

--------------------------------------------------------------------------------

[STRUCT] Point2d
Full Name: Autodesk.AutoCAD.Geometry.Point2d
Implements: IFormattable

  CONSTRUCTORS:
    new Point2d(double[] xy)
    new Point2d(double x, double y)

  PROPERTIES:
    double Coordinate { get; }
    static Point2d Origin { get; }
    double X { get; }
    double Y { get; }

  METHODS:
    Point2d Add(Vector2d value)
    Point2d DivideBy(double value)
    bool Equals(object obj)
    Vector2d GetAsVector()
    double GetDistanceTo(Point2d point)
    int GetHashCode()
    Vector2d GetVectorTo(Point2d point)
    bool IsEqualTo(Point2d a)
    bool IsEqualTo(Point2d a, Tolerance tolerance)
    Point2d Mirror(Line2d line)
    Point2d MultiplyBy(double value)
    Point2d RotateBy(double angle, Point2d origin)
    Point2d ScaleBy(double scaleFactor, Point2d origin)
    Point2d Subtract(Vector2d value)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    Point2d TransformBy(Matrix2d leftSide)

--------------------------------------------------------------------------------

[CLASS] Point2dCollection
Full Name: Autodesk.AutoCAD.Geometry.Point2dCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Point2dCollection(Point2d[] values)
    new Point2dCollection(int capacity)
    new Point2dCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    Point2d Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(Point2d value)
    void AddRange(Point2d[] range)
    void Clear()
    bool Contains(Point2d value)
    void CopyTo(Point2d[] array, int index)
    Point2dCollectionEnumerator GetEnumerator()
    int IndexOf(Point2d value)
    void Insert(int index, Point2d value)
    void Remove(Point2d value)
    void RemoveAt(int index)
    Point2d[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] Point2dCollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.Point2dCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new Point2dCollectionEnumerator(Point2dCollection col)

  PROPERTIES:
    Point2d Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[STRUCT] Point3d
Full Name: Autodesk.AutoCAD.Geometry.Point3d
Implements: IFormattable

  CONSTRUCTORS:
    new Point3d(PlanarEntity plane, Point2d point)
    new Point3d(double[] xyz)
    new Point3d(double x, double y, double z)

  PROPERTIES:
    double Coordinate { get; }
    static Point3d Origin { get; }
    double X { get; }
    double Y { get; }
    double Z { get; }

  METHODS:
    Point3d Add(Vector3d value)
    Point2d Convert2d(PlanarEntity plane)
    double DistanceTo(Point3d point)
    Point3d DivideBy(double value)
    bool Equals(object obj)
    Vector3d GetAsVector()
    int GetHashCode()
    Vector3d GetVectorTo(Point3d point)
    bool IsEqualTo(Point3d point)
    bool IsEqualTo(Point3d point, Tolerance tolerance)
    Point3d Mirror(Plane plane)
    Point3d MultiplyBy(double value)
    Point3d OrthoProject(Plane plane)
    Point3d Project(Plane plane, Vector3d vector)
    Point3d RotateBy(double angle, Vector3d vector, Point3d centerPoint)
    Point3d ScaleBy(double scaleFactor, Point3d centerPoint)
    Point3d Subtract(Vector3d value)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()
    Point3d TransformBy(Matrix3d leftSide)

--------------------------------------------------------------------------------

[CLASS] Point3dCollection
Full Name: Autodesk.AutoCAD.Geometry.Point3dCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Point3dCollection(Point3d[] values)
    new Point3dCollection()

  PROPERTIES:
    int Count { get; }
    Point3d Item { get; set; }

  METHODS:
    int Add(Point3d value)
    void Clear()
    bool Contains(Point3d value)
    void CopyTo(Point3d[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(Point3d value)
    void Insert(int index, Point3d value)
    void Remove(Point3d value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PointEntity2d
Full Name: Autodesk.AutoCAD.Geometry.PointEntity2d
Inherits: Entity2d

  PROPERTIES:
    Point2d Point { get; }

  METHODS:
    static PointEntity2d Create(IntPtr unmanagedPointer, bool autoDelete)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PointEntity3d
Full Name: Autodesk.AutoCAD.Geometry.PointEntity3d
Inherits: Entity3d

  METHODS:
    static PointEntity3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Point3d GetPoint()

--------------------------------------------------------------------------------

[CLASS] PointOnCurve2d
Full Name: Autodesk.AutoCAD.Geometry.PointOnCurve2d
Inherits: PointEntity2d

  CONSTRUCTORS:
    new PointOnCurve2d(Curve2d curve, double parameter)
    new PointOnCurve2d(Curve2d curve)
    new PointOnCurve2d()

  PROPERTIES:
    Curve2d Curve { get; set; }
    double Parameter { get; set; }
    Point2d Point { get; }

  METHODS:
    static PointOnCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Vector2d GetDerivative(int order, Curve2d curve, double parameter)
    Vector2d GetDerivative(int order, double parameter)
    Vector2d GetDerivative(int order)
    Point2d GetPointAtParameter(double parameter)
    Point2d GetPointOnCurve(Curve2d curve, double parameter)

--------------------------------------------------------------------------------

[CLASS] PointOnCurve3d
Full Name: Autodesk.AutoCAD.Geometry.PointOnCurve3d
Inherits: PointEntity3d

  CONSTRUCTORS:
    new PointOnCurve3d(Curve3d curve, double parameter)
    new PointOnCurve3d()

  PROPERTIES:
    Curve3d Curve { get; set; }
    double Parameter { get; set; }
    Point3d Point { get; }

  METHODS:
    static PointOnCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    double GetCurvature(double parameter)
    double GetCurvature()
    static Vector3d GetDerivative(int order, Curve3d curve, double parameter)
    Vector3d GetDerivative(int order, double parameter)
    Vector3d GetDerivative(int order)
    Point3d GetPointAtParameter(double parameter)
    static Point3d GetPointOnCurve(Curve3d curve, double parameter)
    bool IsSingular()
    bool IsSingular(Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] PointOnSurface
Full Name: Autodesk.AutoCAD.Geometry.PointOnSurface
Inherits: PointEntity3d

  CONSTRUCTORS:
    new PointOnSurface(Surface surf, Point2d point)
    new PointOnSurface()

  PROPERTIES:
    Point2d Parameter { get; set; }
    Surface Surface { get; set; }

  METHODS:
    static PointOnSurface Create(IntPtr unmanagedPointer, bool autoDelete)
    static Vector2d GetInverseTangentVector(Vector3d vector, Surface surf, Point2d point)
    Vector2d GetInverseTangentVector(Vector3d vector, Point2d point)
    Vector2d GetInverseTangentVector(Vector3d vector)
    static Vector3d GetMixedPartial(Surface surf, Point2d point)
    Vector3d GetMixedPartial(Point2d point)
    Vector3d GetMixedPartial()
    static Vector3d GetNormal(Surface surf, Point2d point)
    Vector3d GetNormal(Point2d point)
    Vector3d GetNormal()
    static Point3d GetPoint(Surface surface, Point2d point)
    Point3d GetPoint(Point2d point)
    static Vector3d GetTangentVector(Vector2d vector, Surface vectorSurface, Point2d pointer)
    Vector3d GetTangentVector(Vector2d vector, Point2d point)
    Vector3d GetTangentVector(Vector2d vector)
    static Vector3d GetUDerivative(int order, Surface surface, Point2d point)
    Vector3d GetUDerivative(int order, Point2d point)
    Vector3d GetUDerivative(int order)
    static Vector3d GetVDerivative(int order, Surface surface, Point2d point)
    Vector3d GetVDerivative(int order, Point2d point)
    Vector3d GetVDerivative(int order)

--------------------------------------------------------------------------------

[CLASS] PolylineCurve2d
Full Name: Autodesk.AutoCAD.Geometry.PolylineCurve2d
Inherits: SplineEntity2d

  CONSTRUCTORS:
    new PolylineCurve2d(Curve2d curve, double epsilon)
    new PolylineCurve2d(KnotCollection knots, Point2dCollection controlPoints)
    new PolylineCurve2d(Point2dCollection points)
    new PolylineCurve2d()

  PROPERTIES:
    int NumberOfFitPoints { get; }

  METHODS:
    static PolylineCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Point2d FitPointAt(int i)
    void SetFitPointAt(int i, Point2d point)

--------------------------------------------------------------------------------

[CLASS] PolylineCurve3d
Full Name: Autodesk.AutoCAD.Geometry.PolylineCurve3d
Inherits: SplineEntity3d

  CONSTRUCTORS:
    new PolylineCurve3d(Curve3d curve, double epsilon)
    new PolylineCurve3d(KnotCollection knots, Point3dCollection controlPoints)
    new PolylineCurve3d(Point3dCollection points)
    new PolylineCurve3d()

  PROPERTIES:
    int NumberOfFitPoints { get; }

  METHODS:
    static PolylineCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Point3d FitPointAt(int i)
    void SetFitPointAt(int i, Point3d point)

--------------------------------------------------------------------------------

[CLASS] Position2d
Full Name: Autodesk.AutoCAD.Geometry.Position2d
Inherits: PointEntity2d

  CONSTRUCTORS:
    new Position2d(double x, double y)
    new Position2d(Point2d point)
    new Position2d()

  METHODS:
    static Position2d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(double x, double y)
    void Set(Point2d point)

--------------------------------------------------------------------------------

[CLASS] Position3d
Full Name: Autodesk.AutoCAD.Geometry.Position3d
Inherits: PointEntity3d

  CONSTRUCTORS:
    new Position3d(double x, double y, double z)
    new Position3d(Point3d point)
    new Position3d()

  METHODS:
    static Position3d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(double x, double y, double z)
    void Set(Point3d point)

--------------------------------------------------------------------------------

[CLASS] Ray2d
Full Name: Autodesk.AutoCAD.Geometry.Ray2d
Inherits: LinearEntity2d

  CONSTRUCTORS:
    new Ray2d(Point2d point1, Point2d point2)
    new Ray2d(Point2d point, Vector2d vector)
    new Ray2d()

  METHODS:
    static Ray2d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(Point2d point1, Point2d point2)
    void Set(Point2d point, Vector2d vector)

--------------------------------------------------------------------------------

[CLASS] Ray3d
Full Name: Autodesk.AutoCAD.Geometry.Ray3d
Inherits: LinearEntity3d

  CONSTRUCTORS:
    new Ray3d(Point3d point1, Point3d point2)
    new Ray3d(Point3d point, Vector3d vector)
    new Ray3d()

  METHODS:
    static Ray3d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(Point3d point1, Point3d point2)
    void Set(Point3d point, Vector3d vector)

--------------------------------------------------------------------------------

[STRUCT] Scale2d
Full Name: Autodesk.AutoCAD.Geometry.Scale2d
Implements: IFormattable

  CONSTRUCTORS:
    new Scale2d(double[] xy)
    new Scale2d(double x, double y)
    new Scale2d(double factor)

  PROPERTIES:
    double Coordinate { get; }
    double X { get; }
    double Y { get; }

  METHODS:
    bool Equals(object obj)
    static Scale2d ExtractScale(Matrix2d mat)
    int GetHashCode()
    Matrix2d GetMatrix()
    Scale2d Inverse()
    bool IsEqualTo(Scale2d a)
    bool IsEqualTo(Scale2d a, Tolerance tolerance)
    bool IsProportional()
    bool IsProportional(Tolerance tolerance)
    Scale2d MultiplyBy(double factor)
    Scale2d PostMultiplyBy(Scale2d rightSide)
    Scale2d PreMultiplyBy(Scale2d leftSide)
    static Scale2d RemoveScale(Matrix2d mat)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[STRUCT] Scale3d
Full Name: Autodesk.AutoCAD.Geometry.Scale3d
Implements: IFormattable

  CONSTRUCTORS:
    new Scale3d(double[] xyz)
    new Scale3d(double x, double y, double z)
    new Scale3d(double factor)

  PROPERTIES:
    double Coordinate { get; }
    double X { get; }
    double Y { get; }
    double Z { get; }

  METHODS:
    bool Equals(object obj)
    static Scale3d ExtractScale(Matrix3d mat)
    int GetHashCode()
    Matrix3d GetMatrix()
    Scale3d Inverse()
    bool IsEqualTo(Scale3d scaleVector)
    bool IsEqualTo(Scale3d scaleVector, Tolerance tolerance)
    bool IsProportional()
    bool IsProportional(Tolerance tolerance)
    Scale3d MultiplyBy(double s)
    Scale3d PostMultiplyBy(Scale3d rightSide)
    Scale3d PreMultiplyBy(Scale3d leftSide)
    static Scale3d RemoveScale(Matrix3d mat)
    double[] ToArray()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] Sphere
Full Name: Autodesk.AutoCAD.Geometry.Sphere
Inherits: Surface

  CONSTRUCTORS:
    new Sphere(double radius, Point3d center, Vector3d northAxis, Vector3d referenceAxis, double startAngleU, double endAngleU, double startAngleV, double endAngleV)
    new Sphere(double radius, Point3d center)
    new Sphere()

  PROPERTIES:
    Point3d Center { get; }
    bool IsOuterNormal { get; }
    Vector3d NorthAxis { get; }
    Point3d NorthPole { get; }
    double Radius { get; set; }
    Vector3d ReferenceAxis { get; }
    Point3d SouthPole { get; }

  METHODS:
    static Sphere Create(IntPtr unmanagedPointer, bool autoDelete)
    double[] GetAnglesInU()
    double[] GetAnglesInV()
    Point3d[] IntersectWith(LinearEntity3d linearEntity)
    Point3d[] IntersectWith(LinearEntity3d linearEntity, Tolerance tolerance)
    bool IsClosed()
    bool IsClosed(Tolerance tolerance)
    void Set(double radius, Point3d center, Vector3d northAxis, Vector3d referenceAxis, double startAngleU, double endAngleU, double startAngleV, double endAngleV)
    void Set(double radius, Point3d center)
    void SetAnglesInU(double startAngle, double endAngle)
    void SetAnglesInV(double startAngle, double endAngle)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SplineEntity2d
Full Name: Autodesk.AutoCAD.Geometry.SplineEntity2d
Inherits: Curve2d

  PROPERTIES:
    int Degree { get; }
    double EndParameter { get; }
    Point2d EndPoint { get; }
    bool HasFitData { get; }
    bool IsRational { get; }
    KnotCollection Knots { get; }
    int NumControlPoints { get; }
    int NumKnots { get; }
    int Order { get; }
    double StartParameter { get; }
    Point2d StartPoint { get; }

  METHODS:
    static SplineEntity2d Create(IntPtr unmanagedPointer, bool autoDelete)
    int GetContinuityAtKnot(int index)
    int GetContinuityAtKnot(int index, Tolerance tolerance)
    Point2d GetControlPointAt(int index)
    double GetKnotAt(int index)
    void SetControlPointAt(int index, Point2d point)
    void SetKnotAt(int index, double value)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] SplineEntity3d
Full Name: Autodesk.AutoCAD.Geometry.SplineEntity3d
Inherits: Curve3d

  PROPERTIES:
    int Degree { get; }
    double EndParameter { get; }
    Point3d EndPoint { get; }
    bool HasFitData { get; }
    bool IsRational { get; }
    KnotCollection Knots { get; }
    int NumberOfControlPoints { get; }
    int NumberOfKnots { get; }
    int Order { get; }
    double StartParameter { get; }
    Point3d StartPoint { get; }

  METHODS:
    Point3d ControlPointAt(int i)
    static SplineEntity3d Create(IntPtr unmanagedPointer, bool autoDelete)
    int GetContinuityAtKnot(int i)
    int GetContinuityAtKnot(int i, Tolerance tolerance)
    double KnotAt(int i)
    void SetControlPointAt(int i, Point3d point)
    void SetKnotAt(int i, double value)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Surface
Full Name: Autodesk.AutoCAD.Geometry.Surface
Inherits: Entity3d

  PROPERTIES:
    bool IsNormalReversed { get; }
    Surface ReverseNormal { get; }

  METHODS:
    Point3d ClosestPointTo(Point3d point)
    Point3d ClosestPointTo(Point3d point, Tolerance tolerance)
    static Surface Create(IntPtr unmanagedPointer, bool autoDelete)
    double DistanceTo(Point3d point)
    double DistanceTo(Point3d point, Tolerance tolerance)
    Point3d EvaluatePoint(Point2d parameter, int derivOrder, ref Vector3dCollection derivatives, ref Vector3d normal)
    Point3d EvaluatePoint(Point2d parameter, int derivOrder, ref Vector3dCollection derivatives)
    Point3d EvaluatePoint(Point2d parameter)
    PointOnSurface GetClosestPointTo(Point3d point)
    PointOnSurface GetClosestPointTo(Point3d point, Tolerance tolerance)
    Interval[] GetEnvelope()
    bool IsClosedInU()
    bool IsClosedInU(Tolerance tolerance)
    bool IsClosedInV()
    bool IsClosedInV(Tolerance tolerance)
    bool IsOn(Point3d point, Point2d parameterPoint)
    bool IsOn(Point3d point, Point2d parameterPoint, Tolerance tolerance)
    Point2d ParameterOf(Point3d point)
    Point2d ParameterOf(Point3d point, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] SurfaceSurfaceIntersector
Full Name: Autodesk.AutoCAD.Geometry.SurfaceSurfaceIntersector
Inherits: Entity3d

  CONSTRUCTORS:
    new SurfaceSurfaceIntersector(Surface surface2, Surface surface1)
    new SurfaceSurfaceIntersector(Surface surface2, Surface surface1, Tolerance tolerance)
    new SurfaceSurfaceIntersector()

  PROPERTIES:
    int NumResults { get; }
    Surface Surface1 { get; }
    Surface Surface2 { get; }
    Tolerance Tolerance { get; }

  METHODS:
    static SurfaceSurfaceIntersector Create(IntPtr unmanagedPointer, bool autoDelete)
    int GetDimension(int number)
    SurfaceSurfaceIntersectorConfigurations GetIntersectionConfigurations(int number)
    Point2d[] GetIntersectPointParameters(int number)
    SurfaceSurfaceIntersectorType GetType(int number)
    Curve3d IntersectCurve(int number, bool isExternal)
    Curve2d IntersectParameterCurve(int number, bool isExternal, bool isFirst)
    Point3d IntersectPoint(int number)
    void Set(Surface surface1, Surface surface2)
    void Set(Surface surface1, Surface surface2, Tolerance tolerance)

--------------------------------------------------------------------------------

[ENUM] SurfaceSurfaceIntersectorConfiguration
Full Name: Autodesk.AutoCAD.Geometry.SurfaceSurfaceIntersectorConfiguration
Inherits: Enum

  Values:
    - Unknown = 0
    - Out = 1
    - In = 2
    - Coincident = 3

--------------------------------------------------------------------------------

[STRUCT] SurfaceSurfaceIntersectorConfigurations
Full Name: Autodesk.AutoCAD.Geometry.SurfaceSurfaceIntersectorConfigurations

  PROPERTIES:
    int Dimension { get; }
    SurfaceSurfaceIntersectorType IntersectionType { get; }
    SurfaceSurfaceIntersectorConfiguration Surface1Left { get; }
    SurfaceSurfaceIntersectorConfiguration Surface1Right { get; }
    SurfaceSurfaceIntersectorConfiguration Surface2Left { get; }
    SurfaceSurfaceIntersectorConfiguration Surface2Right { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(SurfaceSurfaceIntersectorConfigurations a)

--------------------------------------------------------------------------------

[ENUM] SurfaceSurfaceIntersectorType
Full Name: Autodesk.AutoCAD.Geometry.SurfaceSurfaceIntersectorType
Inherits: Enum

  Values:
    - Transverse = 0
    - Tangent = 1
    - AntiTangent = 2

--------------------------------------------------------------------------------

[STRUCT] Tolerance
Full Name: Autodesk.AutoCAD.Geometry.Tolerance

  CONSTRUCTORS:
    new Tolerance(double equalVector, double equalPoint)

  PROPERTIES:
    double EqualPoint { get; }
    double EqualVector { get; }
    static Tolerance Global { get; set; }

--------------------------------------------------------------------------------

[CLASS] Torus
Full Name: Autodesk.AutoCAD.Geometry.Torus
Inherits: Surface

  CONSTRUCTORS:
    new Torus(double majorRadius, double minorRadius, Point3d origin, Vector3d axisOfSymmetry, Vector3d referenceAxis, double startAngleU, double endAngleU, double startAngleV, double endAngleV)
    new Torus(double majorRadius, double minorRadius, Point3d origin, Vector3d axisOfSymmetry)
    new Torus()

  PROPERTIES:
    Vector3d AxisOfSymmetry { get; }
    Point3d Center { get; }
    bool IsApple { get; }
    bool IsDegenerate { get; }
    bool IsDoughnut { get; }
    bool IsHollow { get; }
    bool IsLemon { get; }
    bool IsOuterNormal { get; }
    bool IsVortex { get; }
    double MajorRadius { get; set; }
    double MinorRadius { get; set; }
    Vector3d ReferenceAxis { get; }

  METHODS:
    static Torus Create(IntPtr unmanagedPointer, bool autoDelete)
    double[] GetAnglesInU()
    double[] GetAnglesInV()
    Point3d[] IntersectWith(LinearEntity3d linearEntity)
    Point3d[] IntersectWith(LinearEntity3d linearEntity, Tolerance tolerance)
    void Set(double majorRadius, double minorRadius, Point3d origin, Vector3d axisOfSymmetry, Vector3d refAxis, double startAngleU, double endAngleU, double startAngleV, double endAngleV)
    void Set(double majorRadius, double minorRadius, Point3d origin, Vector3d axisOfSymmetry)
    void SetAnglesInU(double startAngle, double endAngle)
    void SetAnglesInV(double startAngle, double endAngle)

--------------------------------------------------------------------------------

[CLASS] UInt32Collection
Full Name: Autodesk.AutoCAD.Geometry.UInt32Collection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new UInt32Collection(uint[] values)
    new UInt32Collection(int capacity)
    new UInt32Collection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    uint Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(uint value)
    void AddRange(uint[] range)
    void Clear()
    bool Contains(uint value)
    void CopyTo(uint[] array, int index)
    UInt32CollectionEnumerator GetEnumerator()
    int IndexOf(uint value)
    void Insert(int index, uint value)
    void Remove(uint value)
    void RemoveAt(int index)
    uint[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] UInt32CollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.UInt32CollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new UInt32CollectionEnumerator(UInt32Collection col)

  PROPERTIES:
    uint Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

