# Autodesk.AutoCAD.Geometry

NAMESPACE: Autodesk.AutoCAD.Geometry
--------------------------------------------------------------------------------

[CLASS] AugmentedPolylineCurve3d
Full Name: Autodesk.AutoCAD.Geometry.AugmentedPolylineCurve3d
Inherits: PolylineCurve3d

  CONSTRUCTORS:
    new AugmentedPolylineCurve3d(Curve3d curve, double fromParameter, double toParameter, double eps)
    new AugmentedPolylineCurve3d(Point3dCollection controlPoints, Vector3dCollection vectorBundle)
    new AugmentedPolylineCurve3d(KnotCollection knots, Point3dCollection controlPoints, Vector3dCollection vectorBundle)
    new AugmentedPolylineCurve3d()

  PROPERTIES:
    double ApproximateTolerance { get; set; }
    Vector3dCollection D1Vectors { get; }
    Vector3dCollection D2Vectors { get; }
    Point3dCollection Points { get; }

  METHODS:
    static AugmentedPolylineCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Vector3d GetD2VectorAt(int index)
    Point3d GetPointAt(int index)
    Vector3d GetVectorAt(int index)
    void SetD2VectorAt(int index, Vector3d point)
    void SetPointAt(int index, Point3d point)
    void SetVectorAt(int index, Vector3d point)

--------------------------------------------------------------------------------

[CLASS] BoundBlock2d
Full Name: Autodesk.AutoCAD.Geometry.BoundBlock2d
Inherits: Entity2d

  CONSTRUCTORS:
    new BoundBlock2d(Point2d basePoint, Vector2d direction1, Vector2d direction2)
    new BoundBlock2d(Point2d point1, Point2d point2)
    new BoundBlock2d()

  PROPERTIES:
    Point2d BasePoint { get; }
    Vector2d Direction1 { get; }
    Vector2d Direction2 { get; }
    bool IsBox { get; set; }

  METHODS:
    bool Contains(Point2d point)
    static BoundBlock2d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Extend(Point2d point)
    Point2d GetMaximumPoint()
    Point2d GetMinimumPoint()
    bool IsDisjoint(BoundBlock2d block)
    void Set(Point2d basePoint, Vector2d direction1, Vector2d direction2)
    void Set(Point2d firstPoint, Point2d secondPoint)
    void Swell(double distance)

--------------------------------------------------------------------------------

[CLASS] BoundBlock3d
Full Name: Autodesk.AutoCAD.Geometry.BoundBlock3d
Inherits: Entity3d

  CONSTRUCTORS:
    new BoundBlock3d(Point3d point1, Vector3d dir1, Vector3d dir2, Vector3d dir3)
    new BoundBlock3d()

  PROPERTIES:
    Point3d BasePoint { get; }
    Vector3d Direction1 { get; }
    Vector3d Direction2 { get; }
    Vector3d Direction3 { get; }
    bool IsBox { get; set; }

  METHODS:
    bool Contains(Point3d pointToTest)
    static BoundBlock3d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Extend(Point3d pointToInclude)
    Point3d GetMaximumPoint()
    Point3d GetMinimumPoint()
    bool IsDisjoint(BoundBlock3d block)
    void Set(Point3d basePoint, Vector3d dir1, Vector3d dir2, Vector3d dir3)
    void Set(Point3d basePoint, Point3d maximumPoint)
    void Swell(double distance)

--------------------------------------------------------------------------------

[CLASS] BoundedPlane
Full Name: Autodesk.AutoCAD.Geometry.BoundedPlane
Inherits: PlanarEntity

  CONSTRUCTORS:
    new BoundedPlane(Point3d p1, Point3d origin, Point3d p2)
    new BoundedPlane(Point3d origin, Vector3d u, Vector3d v)
    new BoundedPlane()

  METHODS:
    static BoundedPlane Create(IntPtr unmanagedPointer, bool autoDelete)
    LineSegment3d IntersectWith(BoundedPlane plane)
    LineSegment3d IntersectWith(BoundedPlane plane, Tolerance tolerance)
    LineSegment3d IntersectWith(Plane plane)
    LineSegment3d IntersectWith(Plane plane, Tolerance tolerance)
    void Set(Point3d p1, Point3d origin, Point3d p2)
    void Set(Point3d origin, Vector3d u, Vector3d v)

--------------------------------------------------------------------------------

[CLASS] CircularArc2d
Full Name: Autodesk.AutoCAD.Geometry.CircularArc2d
Inherits: Curve2d

  CONSTRUCTORS:
    new CircularArc2d(Point2d startPoint, Point2d endPoint, double bulge, bool bulgeFlag)
    new CircularArc2d(Point2d startPoint, Point2d point, Point2d endPoint)
    new CircularArc2d(Point2d center, double radius, double startAngle, double endAngle, Vector2d referenceVector, bool isClockWise)
    new CircularArc2d(Point2d center, double radius)
    new CircularArc2d()

  PROPERTIES:
    Point2d Center { get; set; }
    double EndAngle { get; }
    Point2d EndPoint { get; }
    bool IsClockWise { get; }
    double Radius { get; set; }
    Vector2d ReferenceVector { get; set; }
    double StartAngle { get; }
    Point2d StartPoint { get; }

  METHODS:
    static CircularArc2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Line2d GetTangent(Point2d point)
    Line2d GetTangent(Point2d point, Tolerance tolerance)
    Point2d[] IntersectWith(CircularArc2d arc)
    Point2d[] IntersectWith(CircularArc2d arc, Tolerance tolerance)
    Point2d[] IntersectWith(LinearEntity2d line)
    Point2d[] IntersectWith(LinearEntity2d line, Tolerance tolerance)
    bool IsInside(Point2d point)
    bool IsInside(Point2d point, Tolerance tolerance)
    void Set(Curve2d curve1, Curve2d curve2, Curve2d curve3, double parameter1, double parameter2, double parameter3)
    void Set(Curve2d curve1, Curve2d curve2, double radius, double parameter1, double parameter2)
    void Set(Point2d startPoint, Point2d endPoint, double bulge, bool bulgeFlag)
    void Set(Point2d startPoint, Point2d point, Point2d endPoint)
    void Set(Point2d center, double radius, double angle1, double angle2, Vector2d referenceVector, bool isClockWise)
    void Set(Point2d center, double radius)
    void SetAngles(double startAngle, double endAngle)
    void SetToComplement()

--------------------------------------------------------------------------------

[CLASS] CircularArc3d
Full Name: Autodesk.AutoCAD.Geometry.CircularArc3d
Inherits: Curve3d

  CONSTRUCTORS:
    new CircularArc3d(Point3d startPoint, Point3d pointOnArc, Point3d endPoint)
    new CircularArc3d(Point3d center, Vector3d normal, Vector3d referenceVector, double radius, double startAngle, double endAngle)
    new CircularArc3d(Point3d center, Vector3d normal, double radius)
    new CircularArc3d()

  PROPERTIES:
    Point3d Center { get; set; }
    double EndAngle { get; }
    Point3d EndPoint { get; }
    Vector3d Normal { get; }
    double Radius { get; set; }
    Vector3d ReferenceVector { get; }
    double StartAngle { get; }
    Point3d StartPoint { get; }

  METHODS:
    Point3d[] ClosestPointToPlane(PlanarEntity plane)
    Point3d[] ClosestPointToPlane(PlanarEntity plane, Tolerance tolerance)
    static CircularArc3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Plane GetPlane()
    Line3d GetTangent(Point3d pointValue)
    Line3d GetTangent(Point3d pointValue, Tolerance tolerance)
    Point3d[] IntersectWith(PlanarEntity plane)
    Point3d[] IntersectWith(PlanarEntity plane, Tolerance tolerance)
    Point3d[] IntersectWith(CircularArc3d arc)
    Point3d[] IntersectWith(CircularArc3d arc, Tolerance tolerance)
    Point3d[] IntersectWith(LinearEntity3d line)
    Point3d[] IntersectWith(LinearEntity3d line, Tolerance tolerance)
    bool IsInside(Point3d pointValue)
    bool IsInside(Point3d pointValue, Tolerance tolerance)
    Point3d[] ProjectedIntersectWith(LinearEntity3d line, Vector3d projectionDirection)
    Point3d[] ProjectedIntersectWith(LinearEntity3d line, Vector3d projectionDirection, Tolerance tolerance)
    void Set(Curve3d curve1, Curve3d curve2, Curve3d curve3, double param1, double param2, double param3)
    void Set(Curve3d curve1, Curve3d curve2, double radius, double param1, double param2)
    void Set(Point3d startPoint, Point3d pointOnArc, Point3d endPoint)
    void Set(Point3d center, Vector3d normal, Vector3d referenceVector, double radius, double startAngle, double endAngle)
    void Set(Point3d center, Vector3d normal, double radius)
    void SetAngles(double startAngle, double endAngle)
    void SetAxes(Vector3d normal, Vector3d referenceVector)

--------------------------------------------------------------------------------

[CLASS] ClipBoundary2d
Full Name: Autodesk.AutoCAD.Geometry.ClipBoundary2d
Inherits: Entity2d

  CONSTRUCTORS:
    new ClipBoundary2d(Point2dCollection clipBoundary)
    new ClipBoundary2d(Point2d firstCorner, Point2d secondCorner)
    new ClipBoundary2d()

  METHODS:
    ClipBoundary2dData ClipPolygon(Point2dCollection rawVertices)
    ClipBoundary2dData ClipPolyline(Point2dCollection rawVertices)
    static ClipBoundary2d Create(IntPtr unmanagedPointer, bool autoDelete)
    ClipError Set(Point2dCollection clipBoundary)
    ClipError Set(Point2d firstCorner, Point2d secondCorner)

--------------------------------------------------------------------------------

[STRUCT] ClipBoundary2dData
Full Name: Autodesk.AutoCAD.Geometry.ClipBoundary2dData

  PROPERTIES:
    ClipCondition ClipCondition { get; }
    IntegerCollection ClippedSegmentSourceLabel { get; }
    Point2dCollection ClippedVertices { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(ClipBoundary2dData a)

--------------------------------------------------------------------------------

[ENUM] ClipCondition
Full Name: Autodesk.AutoCAD.Geometry.ClipCondition
Inherits: Enum

  Values:
    - Invalid = 0
    - AllSegmentsInside = 1
    - SegmentsIntersect = 2
    - AllSegmentsOutsideZeroWinds = 3
    - AllSegmentsOutsideOddWinds = 4
    - AllSegmentsOutsideEvenWinds = 5

--------------------------------------------------------------------------------

[ENUM] ClipError
Full Name: Autodesk.AutoCAD.Geometry.ClipError
Inherits: Enum

  Values:
    - OK = 0
    - InvalidClipBoundary = 1
    - NotInitialized = 2

--------------------------------------------------------------------------------

[CLASS] CompositeCurve2d
Full Name: Autodesk.AutoCAD.Geometry.CompositeCurve2d
Inherits: Curve2d

  CONSTRUCTORS:
    new CompositeCurve2d(Curve2d[] curves)

  METHODS:
    static CompositeCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Curve2d[] GetCurves()
    CompositeParameter GlobalToLocalParameter(double value)
    double LocalToGlobalParameter(CompositeParameter value)
    void SetCurves(Curve2d[] curves)

--------------------------------------------------------------------------------

[CLASS] CompositeCurve3d
Full Name: Autodesk.AutoCAD.Geometry.CompositeCurve3d
Inherits: Curve3d

  CONSTRUCTORS:
    new CompositeCurve3d(Curve3d[] curves)

  METHODS:
    static CompositeCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Curve3d[] GetCurves()
    CompositeParameter GlobalToLocalParameter(double value)
    double LocalToGlobalParameter(CompositeParameter value)

--------------------------------------------------------------------------------

[STRUCT] CompositeParameter
Full Name: Autodesk.AutoCAD.Geometry.CompositeParameter

  CONSTRUCTORS:
    new CompositeParameter(int segmentIndex, double localParameter)

  PROPERTIES:
    double LocalParameter { get; }
    int SegmentIndex { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(CompositeParameter a)
    bool IsEqualTo(CompositeParameter a, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] Cone
Full Name: Autodesk.AutoCAD.Geometry.Cone
Inherits: Surface

  CONSTRUCTORS:
    new Cone(double cosineAngle, double sineAngle, Point3d baseOrigin, double baseRadius, Vector3d axisOfSymmetry, Vector3d referenceAxis, Interval height, double startAngle, double endAngle)
    new Cone(double cosineAngle, double sineAngle, Point3d baseOrigin, double baseRadius, Vector3d axisOfSymmetry)
    new Cone()

  PROPERTIES:
    Point3d Apex { get; }
    Vector3d AxisOfSymmetry { get; }
    Point3d BaseCenter { get; }
    double BaseRadius { get; set; }
    double HalfAngle { get; }
    Interval Height { get; set; }
    bool IsOuterNormal { get; }
    Vector3d ReferenceAxis { get; }

  METHODS:
    static Cone Create(IntPtr unmanagedPointer, bool autoDelete)
    double[] GetAngles()
    double[] GetHalfAngles()
    double GetHeightAt(double parameter)
    Point3d[] IntersectWith(LinearEntity3d linearEntity)
    Point3d[] IntersectWith(LinearEntity3d linearEntity, Tolerance tolerance)
    bool IsClosed()
    bool IsClosed(Tolerance tolerance)
    void Set(double cosineAngle, double sineAngle, Point3d baseCenter, double baseRadius, Vector3d axisOfSymmetry, Vector3d refAxis, Interval height, double startAngle, double endAngle)
    void Set(double cosineAngle, double sineAngle, Point3d baseCenter, double baseRadius, Vector3d axisOfSymmetry)
    void SetAngles(double startAngle, double endAngle)

--------------------------------------------------------------------------------

[STRUCT] CoordinateSystem2d
Full Name: Autodesk.AutoCAD.Geometry.CoordinateSystem2d
Implements: IFormattable

  CONSTRUCTORS:
    new CoordinateSystem2d(Point2d o, Vector2d x, Vector2d y)

  PROPERTIES:
    Point2d Origin { get; }
    Vector2d Xaxis { get; }
    Vector2d Yaxis { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(CoordinateSystem2d a)
    bool IsEqualTo(CoordinateSystem2d a, Tolerance tolerance)
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[STRUCT] CoordinateSystem3d
Full Name: Autodesk.AutoCAD.Geometry.CoordinateSystem3d
Implements: IFormattable

  CONSTRUCTORS:
    new CoordinateSystem3d(Point3d o, Vector3d x, Vector3d y)

  PROPERTIES:
    Point3d Origin { get; }
    Vector3d Xaxis { get; }
    Vector3d Yaxis { get; }
    Vector3d Zaxis { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    bool IsEqualTo(CoordinateSystem3d a)
    bool IsEqualTo(CoordinateSystem3d a, Tolerance tolerance)
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] CubicSplineCurve2d
Full Name: Autodesk.AutoCAD.Geometry.CubicSplineCurve2d
Inherits: SplineEntity2d

  CONSTRUCTORS:
    new CubicSplineCurve2d(KnotCollection knots, Point2dCollection fitPoints, Vector2dCollection firstDerivatives, bool periodic)
    new CubicSplineCurve2d(Curve2d curve, double epsilon)
    new CubicSplineCurve2d(Point2dCollection fitPoints, Vector2d startDerivative, Vector2d endDerivative)
    new CubicSplineCurve2d(Point2dCollection fitPoints, Vector2d startDerivative, Vector2d endDerivative, Tolerance tolerance)
    new CubicSplineCurve2d(Point2dCollection fitPoints)
    new CubicSplineCurve2d(Point2dCollection fitPoints, Tolerance tolerance)
    new CubicSplineCurve2d()

  PROPERTIES:
    int NumFitPoints { get; }

  METHODS:
    static CubicSplineCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Vector2d GetFirstDerivativeAt(int index)
    Point2d GetFitPointAt(int index)
    void SetFirstDerivativeAt(int index, Vector2d derivative)
    void SetFitPointAt(int index, Point2d point)

--------------------------------------------------------------------------------

[CLASS] CubicSplineCurve3d
Full Name: Autodesk.AutoCAD.Geometry.CubicSplineCurve3d
Inherits: SplineEntity3d

  CONSTRUCTORS:
    new CubicSplineCurve3d(Curve3d curve, double epsilon)
    new CubicSplineCurve3d(KnotCollection knots, Point3dCollection fitPoints, Vector3dCollection firstDerivatives, bool periodic)
    new CubicSplineCurve3d(Point3dCollection points, Vector3d startDerivative, Vector3d endDerivative)
    new CubicSplineCurve3d(Point3dCollection points, Vector3d startDerivative, Vector3d endDerivative, Tolerance tolerance)
    new CubicSplineCurve3d(Point3dCollection points)
    new CubicSplineCurve3d(Point3dCollection points, Tolerance tolerance)
    new CubicSplineCurve3d()

  PROPERTIES:
    int NumberOfFitPoints { get; }

  METHODS:
    static CubicSplineCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Vector3d FirstDerivativeAt(int i)
    Point3d FitPointAt(int i)
    void SetFirstDerivativeAt(int i, Vector3d derivative)
    void SetFitPointAt(int i, Point3d point)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Curve2d
Full Name: Autodesk.AutoCAD.Geometry.Curve2d
Inherits: Entity2d

  PROPERTIES:
    BoundBlock2d BoundBlock { get; }
    Point2d EndPoint { get; }
    bool HasEndPoint { get; }
    bool HasStartPoint { get; }
    BoundBlock2d OrthoBoundBlock { get; }
    Point2d StartPoint { get; }

  METHODS:
    static Curve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Point2d EvaluatePoint(double parameter)
    Curve2d[] Explode(Interval interval)
    double GetArea(double startParameter, double endParameter)
    double GetArea(double startParameter, double endParameter, Tolerance tolerance)
    BoundBlock2d GetBoundBlockOf(Interval range)
    PointOnCurve2d[] GetClosestPointTo(Curve2d curve)
    PointOnCurve2d[] GetClosestPointTo(Curve2d curve, Tolerance tolerance)
    PointOnCurve2d GetClosestPointTo(Point2d point)
    PointOnCurve2d GetClosestPointTo(Point2d point, Tolerance tolerance)
    double GetDistanceTo(Curve2d curve)
    double GetDistanceTo(Curve2d curve, Tolerance tolerance)
    double GetDistanceTo(Point2d point)
    double GetDistanceTo(Point2d point, Tolerance tolerance)
    Interval GetInterval()
    double GetLength(double fromParameter, double toParameter)
    double GetLength(double fromParameter, double toParameter, Tolerance tolerance)
    PointOnCurve2d GetNormalPoint(Point2d point)
    PointOnCurve2d GetNormalPoint(Point2d point, Tolerance tolerance)
    BoundBlock2d GetOrthoBoundBlockOf(Interval range)
    double GetParameterAtLength(double datumParameter, double length, bool parameterDirection)
    double GetParameterAtLength(double datumParameter, double length, bool parameterDirection, Tolerance tolerance)
    double GetParameterOf(Point2d point)
    double GetParameterOf(Point2d point, Tolerance tolerance)
    Curve2d GetReverseParameterCurve()
    Point2d[] GetSamplePoints(int numSample)
    PointOnCurve2d[] GetSamplePoints(double fromParameter, double toParameter, double approxEps)
    Curve2d[] GetSplitCurves(double value)
    Curve2d[] GetTrimmedOffset(double distance, OffsetCurveExtensionType extensionType)
    Curve2d[] GetTrimmedOffset(double distance, OffsetCurveExtensionType extensionType, Tolerance tolerance)
    bool IsClosed()
    bool IsClosed(Tolerance tolerance)
    bool IsDegenerate(out Entity2d result)
    bool IsDegenerate(out Entity2d result, Tolerance tolerance)
    bool IsLinear(out Line2d line)
    bool IsLinear(out Line2d line, Tolerance tolerance)
    bool IsOn(double parameter)
    bool IsOn(double parameter, Tolerance tolerance)
    bool IsOn(Point2d point, out double value)
    bool IsOn(Point2d point, out double value, Tolerance tolerance)
    bool IsOn(Point2d point)
    bool IsOn(Point2d point, Tolerance tolerance)
    bool IsPeriodic(out double period)
    void SetInterval(Interval value)

--------------------------------------------------------------------------------

[CLASS] Curve2dCollection
Full Name: Autodesk.AutoCAD.Geometry.Curve2dCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Curve2dCollection()

  PROPERTIES:
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    Curve2d Item { get; set; }

  METHODS:
    int Add(Curve2d value)
    void Clear()
    bool Contains(Curve2d value)
    void CopyTo(Curve2d[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(Curve2d value)
    void Insert(int index, Curve2d value)
    void Remove(Curve2d value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Curve3d
Full Name: Autodesk.AutoCAD.Geometry.Curve3d
Inherits: Entity3d

  PROPERTIES:
    BoundBlock3d BoundBlock { get; }
    Point3d EndPoint { get; }
    bool HasEndPoint { get; }
    bool HasStartPoint { get; }
    BoundBlock3d OrthoBoundBlock { get; }
    Point3d StartPoint { get; }

  METHODS:
    static Curve3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Point3d EvaluatePoint(double value)
    Curve3d[] Explode(Interval interval)
    double GetArea(double startingParameter, double endingParameter)
    double GetArea(double startingParameter, double endingParameter, Tolerance tolerance)
    BoundBlock3d GetBoundBlockOf(Interval range)
    PointOnCurve3d[] GetClosestPointTo(Curve3d curve3d)
    PointOnCurve3d[] GetClosestPointTo(Curve3d curve3d, Tolerance tolerance)
    PointOnCurve3d GetClosestPointTo(Point3d point)
    PointOnCurve3d GetClosestPointTo(Point3d point, Tolerance tolerance)
    double GetDistanceTo(Curve3d curve)
    double GetDistanceTo(Curve3d curve, Tolerance tolerance)
    double GetDistanceTo(Point3d point)
    double GetDistanceTo(Point3d point, Tolerance tolerance)
    Interval GetInterval()
    double GetLength(double fromParameter, double toParameter, double tolerance)
    PointOnCurve3d[] GetNewSamplePoints(double fromParameter, double toParameter, double chordHeight)
    PointOnCurve3d GetNormalPoint(Point3d point)
    PointOnCurve3d GetNormalPoint(Point3d point, Tolerance tolerance)
    BoundBlock3d GetOrthoBoundBlockOf(Interval range)
    Entity3d GetOrthoProjectEntity(Plane projectionPlane)
    Entity3d GetOrthoProjectEntity(Plane projectionPlane, Tolerance tolerance)
    double GetParameterAtLength(double datumParameter, double length, bool direction, double tolerance)
    double GetParameterOf(Point3d point)
    double GetParameterOf(Point3d point, Tolerance tolerance)
    PointOnCurve3d[] GetProjectedClosestPointTo(Curve3d curve3d, Vector3d projectDirection)
    PointOnCurve3d[] GetProjectedClosestPointTo(Curve3d curve3d, Vector3d projectDirection, Tolerance tolerance)
    PointOnCurve3d GetProjectedClosestPointTo(Point3d point, Vector3d projectDirection)
    PointOnCurve3d GetProjectedClosestPointTo(Point3d point, Vector3d projectDirection, Tolerance tolerance)
    Entity3d GetProjectedEntity(Plane projectionPlane, Vector3d projectDirection)
    Entity3d GetProjectedEntity(Plane projectionPlane, Vector3d projectDirection, Tolerance tolerance)
    Curve3d GetReverseParameterCurve()
    PointOnCurve3d[] GetSamplePoints(int numSample)
    PointOnCurve3d[] GetSamplePoints(double fromParameter, double toParameter, double chordHeight)
    Curve3d[] GetSplitCurves(double value)
    Curve3d[] GetTrimmedOffset(double distance, Vector3d planeNormal, OffsetCurveExtensionType extensionType)
    Curve3d[] GetTrimmedOffset(double distance, Vector3d planeNormal, OffsetCurveExtensionType extensionType, Tolerance tolerance)
    bool IsClosed()
    bool IsClosed(Tolerance tolerance)
    bool IsCoplanarWith(Curve3d curve3d, out Plane plane)
    bool IsCoplanarWith(Curve3d curve3d, out Plane plane, Tolerance tolerance)
    bool IsDegenerate(out Entity3d result)
    bool IsDegenerate(out Entity3d result, Tolerance tolerance)
    bool IsLinear(out Line3d line)
    bool IsLinear(out Line3d line, Tolerance tolerance)
    bool IsOn(double value)
    bool IsOn(double value, Tolerance tolerance)
    bool IsOn(Point3d point, out double value)
    bool IsOn(Point3d point, out double value, Tolerance tolerance)
    bool IsOn(Point3d point)
    bool IsOn(Point3d point, Tolerance tolerance)
    bool IsPeriodic(out double period)
    bool IsPlanar(out Plane plane)
    bool IsPlanar(out Plane plane, Tolerance tolerance)
    void SetInterval(Interval value)

--------------------------------------------------------------------------------

[CLASS] Curve3dCollection
Full Name: Autodesk.AutoCAD.Geometry.Curve3dCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Curve3dCollection()

  PROPERTIES:
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    Curve3d Item { get; set; }

  METHODS:
    int Add(Curve3d value)
    void Clear()
    bool Contains(Curve3d value)
    void CopyTo(Curve3d[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(Curve3d value)
    void Insert(int index, Curve3d value)
    void Remove(Curve3d value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] CurveBoundary
Full Name: Autodesk.AutoCAD.Geometry.CurveBoundary
Inherits: DisposableWrapper
Implements: ICloneable

  CONSTRUCTORS:
    new CurveBoundary(int numberOfCurves, Curve3d[] curve3d, Curve2d[] curve2d, bool orientation3d, bool orientation2d)
    new CurveBoundary()

  PROPERTIES:
    CurveBoundaryData Contour { get; set; }
    Curve2d DegenerateCurve { get; }
    Position3d DegeneratePosition { get; }
    bool IsDegenerate { get; }
    bool IsOwnerOfCurves { get; }
    int NumElements { get; }

  METHODS:
    object Clone()
    void Set(int numElements, Entity3d[] curve3ds, Curve2d[] curve2ds, bool orientation3d, bool orientation2d)
    void SetToOwnCurves()

--------------------------------------------------------------------------------

[STRUCT] CurveBoundaryData
Full Name: Autodesk.AutoCAD.Geometry.CurveBoundaryData

  PROPERTIES:
    bool Orientation2d { get; }
    bool Orientation3d { get; }

  METHODS:
    bool Equals(object obj)
    Curve2d[] GetCurve2ds()
    Entity3d[] GetCurve3ds()
    int GetHashCode()
    bool IsEqualTo(CurveBoundaryData a)

--------------------------------------------------------------------------------

[CLASS] CurveCurveIntersector2d
Full Name: Autodesk.AutoCAD.Geometry.CurveCurveIntersector2d
Inherits: Entity2d

  CONSTRUCTORS:
    new CurveCurveIntersector2d(Curve2d curve1, Curve2d curve2, Interval range1, Interval range2)
    new CurveCurveIntersector2d(Curve2d curve1, Curve2d curve2, Interval range1, Interval range2, Tolerance tolerance)
    new CurveCurveIntersector2d(Curve2d curve1, Curve2d curve2)
    new CurveCurveIntersector2d(Curve2d curve1, Curve2d curve2, Tolerance tolerance)
    new CurveCurveIntersector2d()

  PROPERTIES:
    Curve2d Curve1 { get; }
    Curve2d Curve2 { get; }
    int NumberOfIntersectionPoints { get; }
    int OverlapCount { get; }
    bool OverlapDirection { get; }
    Tolerance Tolerance { get; }

  METHODS:
    void ChangeCurveOrder()
    static CurveCurveIntersector2d Create(IntPtr unmanagedPointer, bool autoDelete)
    double[] GetIntersectionParameters(int intersection)
    Point2d GetIntersectionPoint(int number)
    double GetIntersectionPointTolerance(int intersection)
    Interval[] GetIntersectionRanges()
    Interval[] GetOverlapRanges(int overlapNumber)
    PointOnCurve2d GetPointOnCurve1(int intersection)
    PointOnCurve2d GetPointOnCurve2(int intersection)
    bool IsTangential(int intersection)
    bool IsTransversal(int intersection)
    void OrderWithRegardsTo1()
    void OrderWithRegardsTo2()
    void Set(Curve2d curve1, Curve2d curve2, Interval range1, Interval range2)
    void Set(Curve2d curve1, Curve2d curve2, Interval range1, Interval range2, Tolerance tolerance)
    void Set(Curve2d curve1, Curve2d curve2)
    void Set(Curve2d curve1, Curve2d curve2, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] CurveCurveIntersector3d
Full Name: Autodesk.AutoCAD.Geometry.CurveCurveIntersector3d
Inherits: Entity3d

  CONSTRUCTORS:
    new CurveCurveIntersector3d(Curve3d curve1, Curve3d curve2, Interval range1, Interval range2, Vector3d planeNormal)
    new CurveCurveIntersector3d(Curve3d curve1, Curve3d curve2, Interval range1, Interval range2, Vector3d planeNormal, Tolerance tolerance)
    new CurveCurveIntersector3d(Curve3d curve1, Curve3d curve2, Vector3d planeNormal)
    new CurveCurveIntersector3d(Curve3d curve1, Curve3d curve2, Vector3d planeNormal, Tolerance tolerance)
    new CurveCurveIntersector3d()

  PROPERTIES:
    Curve3d Curve1 { get; }
    Curve3d Curve2 { get; }
    int NumberOfIntersectionPoints { get; }
    Vector3d PlaneNormal { get; }
    Tolerance Tolerance { get; }

  METHODS:
    void ChangeCurveOrder()
    static CurveCurveIntersector3d Create(IntPtr unmanagedPointer, bool autoDelete)
    double[] GetIntersectionParameters(int intersection)
    Point3d GetIntersectionPoint(int intersection)
    double GetIntersectionPointTolerance(int intersection)
    Interval[] GetIntersectionRanges()
    Interval[] GetOverlapRanges(int overlap)
    PointOnCurve3d GetPointOnCurve1(int intersection)
    PointOnCurve3d GetPointOnCurve2(int intersection)
    bool IsTangential(int intersection)
    bool IsTransversal(int intersection)
    void OrderWithRegardsTo1()
    void OrderWithRegardsTo2()
    int OverlapCount()
    bool OverlapDirection()
    void Set(Curve3d curve1, Curve3d curve2, Interval range1, Interval range2, Vector3d planeNormal)
    void Set(Curve3d curve1, Curve3d curve2, Interval range1, Interval range2, Vector3d planeNormal, Tolerance tolerance)
    void Set(Curve3d curve1, Curve3d curve2, Vector3d planeNormal)
    void Set(Curve3d curve1, Curve3d curve2, Vector3d planeNormal, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] Cylinder
Full Name: Autodesk.AutoCAD.Geometry.Cylinder
Inherits: Surface

  CONSTRUCTORS:
    new Cylinder(double radius, Point3d origin, Vector3d axisOfSymmetry, Vector3d referenceAxis, Interval height, double startAngle, double endAngle)
    new Cylinder(double radius, Point3d origin, Vector3d axisOfSymmetry)
    new Cylinder()

  PROPERTIES:
    Vector3d AxisOfSymmetry { get; }
    Interval Height { get; set; }
    bool IsOuterNormal { get; }
    Point3d Origin { get; }
    double Radius { get; set; }
    Vector3d ReferenceAxis { get; }

  METHODS:
    static Cylinder Create(IntPtr unmanagedPointer, bool autoDelete)
    double[] GetAngles()
    double GetHeightAt(double parameter)
    Point3d[] IntersectWith(LinearEntity3d linearEntity)
    Point3d[] IntersectWith(LinearEntity3d linearEntity, Tolerance tolerance)
    bool IsClosed()
    bool IsClosed(Tolerance tolerance)
    void Set(double radius, Point3d origin, Vector3d axisOfSymmetry, Vector3d referenceAxis, Interval height, double startAngle, double endAngle)
    void Set(double radius, Point3d origin, Vector3d axisOfSymmetry)
    void SetAngles(double startAngle, double endAngle)

--------------------------------------------------------------------------------

[CLASS] DoubleCollection
Full Name: Autodesk.AutoCAD.Geometry.DoubleCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new DoubleCollection(double[] values)
    new DoubleCollection(int capacity)
    new DoubleCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    double Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(double value)
    void AddRange(double[] range)
    void Clear()
    bool Contains(double value)
    void CopyTo(double[] array, int index)
    DoubleCollectionEnumerator GetEnumerator()
    int IndexOf(double value)
    void Insert(int index, double value)
    void Remove(double value)
    void RemoveAt(int index)
    double[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] DoubleCollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.DoubleCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new DoubleCollectionEnumerator(DoubleCollection col)

  PROPERTIES:
    double Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] EllipticalArc2d
Full Name: Autodesk.AutoCAD.Geometry.EllipticalArc2d
Inherits: Curve2d

  CONSTRUCTORS:
    new EllipticalArc2d(Point2d center, Vector2d majorAxis, Vector2d minorAxis, double majorRadius, double minorRadius, double startAngle, double endAngle)
    new EllipticalArc2d(Point2d center, Vector2d majorAxis, Vector2d minorAxis, double majorRadius, double minorRadius)
    new EllipticalArc2d(CircularArc2d arc)
    new EllipticalArc2d()

  PROPERTIES:
    Point2d Center { get; set; }
    double EndAngle { get; }
    Point2d EndPoint { get; }
    bool IsClockWise { get; }
    Vector2d MajorAxis { get; }
    double MajorRadius { get; set; }
    Vector2d MinorAxis { get; }
    double MinorRadius { get; set; }
    double StartAngle { get; }
    Point2d StartPoint { get; }

  METHODS:
    static EllipticalArc2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Point2d[] IntersectWith(LinearEntity2d line)
    Point2d[] IntersectWith(LinearEntity2d line, Tolerance tolerance)
    bool IsCircular()
    bool IsCircular(Tolerance tolerance)
    bool IsInside(Point2d point)
    bool IsInside(Point2d point, Tolerance tolerance)
    void Set(CircularArc2d arc)
    void Set(Point2d center, Vector2d majorAxis, Vector2d minorAxis, double majorRadius, double minorRadius, double startAngle, double endAngle)
    void Set(Point2d center, Vector2d majorAxis, Vector2d minorAxis, double majorRadius, double minorRadius)
    void SetAngles(double startAngle, double endAngle)
    void SetAxes(Vector2d majorAxis, Vector2d minorAxis)

--------------------------------------------------------------------------------

[CLASS] EllipticalArc3d
Full Name: Autodesk.AutoCAD.Geometry.EllipticalArc3d
Inherits: Curve3d

  CONSTRUCTORS:
    new EllipticalArc3d(Point3d center, Vector3d majorAxis, Vector3d minorAxis, double majorRadius, double minorRadius, double startAngle, double endAngle)
    new EllipticalArc3d(Point3d center, Vector3d majorAxis, Vector3d minorAxis, double majorRadius, double minorRadius)
    new EllipticalArc3d()

  PROPERTIES:
    Point3d Center { get; set; }
    double EndAngle { get; }
    Point3d EndPoint { get; }
    Vector3d MajorAxis { get; }
    double MajorRadius { get; set; }
    Vector3d MinorAxis { get; }
    double MinorRadius { get; set; }
    Vector3d Normal { get; }
    double StartAngle { get; }
    Point3d StartPoint { get; }

  METHODS:
    Point3d[] ClosestPointToPlane(PlanarEntity plane)
    Point3d[] ClosestPointToPlane(PlanarEntity plane, Tolerance tolerance)
    static EllipticalArc3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Plane GetPlane()
    Point3d[] IntersectWith(PlanarEntity plane)
    Point3d[] IntersectWith(PlanarEntity plane, Tolerance tolerance)
    Point3d[] IntersectWith(LinearEntity3d line)
    Point3d[] IntersectWith(LinearEntity3d line, Tolerance tolerance)
    bool IsCircular()
    bool IsCircular(Tolerance tolerance)
    bool IsInside(Point3d pointValue)
    bool IsInside(Point3d pointValue, Tolerance tolerance)
    Point3d[] ProjectedIntersectWith(LinearEntity3d line, Vector3d projectionDirection)
    Point3d[] ProjectedIntersectWith(LinearEntity3d line, Vector3d projectionDirection, Tolerance tolerance)
    void Set(Point3d center, Vector3d majorAxis, Vector3d minorAxis, double majorRadius, double minorRadius, double startAngle, double endAngle)
    void Set(Point3d center, Vector3d majorAxis, Vector3d minorAxis, double majorRadius, double minorRadius)
    void SetAngles(double startAngle, double endAngle)
    void SetAxes(Vector3d majorAxis, Vector3d minorAxis)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Entity2d
Full Name: Autodesk.AutoCAD.Geometry.Entity2d
Inherits: DisposableWrapper
Implements: ICloneable

  METHODS:
    object Clone()
    static Entity2d Create(IntPtr unmanagedPointer, bool autoDelete)
    bool IsEqualTo(Entity2d entity)
    bool IsEqualTo(Entity2d entity, Tolerance tolerance)
    bool IsOn(Point2d point)
    bool IsOn(Point2d point, Tolerance tolerance)
    void Mirror(Line2d line)
    void RotateBy(double angle, Point2d point)
    void ScaleBy(double scaleFactor, Point2d point)
    void TransformBy(Matrix2d transform)
    void TranslateBy(Vector2d translateVector)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] Entity3d
Full Name: Autodesk.AutoCAD.Geometry.Entity3d
Inherits: DisposableWrapper
Implements: ICloneable

  METHODS:
    object Clone()
    static Entity3d Create(IntPtr unmanagedPointer, bool autoDelete)
    bool IsEqualTo(Entity3d entity)
    bool IsEqualTo(Entity3d entity, Tolerance tolerance)
    bool IsOn(Point3d point)
    bool IsOn(Point3d point, Tolerance tolerance)
    void Mirror(Plane plane)
    void RotateBy(double angle, Vector3d vector, Point3d point)
    void ScaleBy(double scaleFactor, Point3d point)
    void TransformBy(Matrix3d transform)
    void TranslateBy(Vector3d translateVector)

--------------------------------------------------------------------------------

[ENUM] ErrorCondition
Full Name: Autodesk.AutoCAD.Geometry.ErrorCondition
Inherits: Enum

  Values:
    - OK = 0
    - This = 1
    - Arg1 = 2
    - Arg2 = 3
    - PerpendicularArg1Arg2 = 4
    - EqualArg1Arg2 = 5
    - EqualArg1Arg3 = 6
    - EqualArg2Arg3 = 7
    - LinearlyDependentArg1Arg2Arg3 = 8
    - Arg1TooBig = 9
    - Arg1OnThis = 10
    - Arg1InsideThis = 11

--------------------------------------------------------------------------------

[CLASS] ExternalBoundedSurface
Full Name: Autodesk.AutoCAD.Geometry.ExternalBoundedSurface
Inherits: Surface

  CONSTRUCTORS:
    new ExternalBoundedSurface(IntPtr surfaceDefinition, ExternalEntityKind surfaceKind)
    new ExternalBoundedSurface()

  PROPERTIES:
    Surface BaseSurface { get; }
    ExternalSurface ExternalBaseSurface { get; }
    IntPtr ExternalSurfaceDefinition { get; }
    ExternalEntityKind ExternalSurfaceKind { get; }
    bool IsCone { get; }
    bool IsCylinder { get; }
    bool IsDefined { get; }
    bool IsExternalSurface { get; }
    bool IsNurbs { get; }
    bool IsOwnerOfSurface { get; }
    bool IsPlane { get; }
    bool IsSphere { get; }
    bool IsTorus { get; }
    int NumContours { get; }

  METHODS:
    static ExternalBoundedSurface Create(IntPtr unmanagedPointer, bool autoDelete)
    CurveBoundary[] GetContours()
    void Set(IntPtr surfaceDef, ExternalEntityKind surfaceKind)
    void SetToOwnSurface()

--------------------------------------------------------------------------------

[CLASS] ExternalCurve2d
Full Name: Autodesk.AutoCAD.Geometry.ExternalCurve2d
Inherits: Curve2d

  CONSTRUCTORS:
    new ExternalCurve2d(IntPtr curveDef, ExternalEntityKind curveKind)
    new ExternalCurve2d()

  PROPERTIES:
    IntPtr ExternalCurve { get; }
    ExternalEntityKind ExternalCurveKind { get; }
    bool IsDefined { get; }
    bool IsNurbCurve { get; }
    bool IsOwnerOfCurve { get; }
    NurbCurve2d NurbCurve { get; }

  METHODS:
    static ExternalCurve2d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(IntPtr curveDef, ExternalEntityKind curveKind)
    void SetToOwnCurve()

--------------------------------------------------------------------------------

[CLASS] ExternalCurve3d
Full Name: Autodesk.AutoCAD.Geometry.ExternalCurve3d
Inherits: Curve3d

  PROPERTIES:
    ExternalEntityKind ExternalCurveKind { get; }
    bool IsCircularArc { get; }
    bool IsDefined { get; }
    bool IsEllipticalArc { get; }
    bool IsLine { get; }
    bool IsLineSegment { get; }
    bool IsNativeCurve { get; }
    bool IsNurbCurve { get; }
    bool IsRay { get; }
    Curve3d NativeCurve { get; }

  METHODS:
    static ExternalCurve3d Create(IntPtr unmanagedPointer, bool autoDelete)

--------------------------------------------------------------------------------

[ENUM] ExternalEntityKind
Full Name: Autodesk.AutoCAD.Geometry.ExternalEntityKind
Inherits: Enum

  Values:
    - AcisEntity = 0
    - ExternalEntityUndefined = 1

--------------------------------------------------------------------------------

[CLASS] ExternalSurface
Full Name: Autodesk.AutoCAD.Geometry.ExternalSurface
Inherits: Surface

  CONSTRUCTORS:
    new ExternalSurface(IntPtr surfaceDefinition, ExternalEntityKind surfaceKind)
    new ExternalSurface()

  PROPERTIES:
    IntPtr ExternalSurfaceDefiniton { get; }
    ExternalEntityKind ExternalSurfaceKind { get; }
    bool IsCone { get; }
    bool IsCylinder { get; }
    bool IsDefined { get; }
    bool IsNativeSurface { get; }
    bool IsNurbSurface { get; }
    bool IsOwnerOfSurface { get; }
    bool IsPlane { get; }
    bool IsSphere { get; }
    bool IsTorus { get; }
    Surface nativeSurface { get; }

  METHODS:
    static ExternalSurface Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(IntPtr surfaceDef, ExternalEntityKind surfaceKind)
    void SetToOwnSurface()

--------------------------------------------------------------------------------

[STRUCT] GeoDataLonLatAltInfo
Full Name: Autodesk.AutoCAD.Geometry.GeoDataLonLatAltInfo

  CONSTRUCTORS:
    new GeoDataLonLatAltInfo(double dbLong, double dbLat, double dbAlt)

  PROPERTIES:
    double Altitude { get; set; }
    double Latitude { get; set; }
    double Longitude { get; set; }

--------------------------------------------------------------------------------

[CLASS] Int32Collection
Full Name: Autodesk.AutoCAD.Geometry.Int32Collection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Int32Collection(int[] values)
    new Int32Collection(int capacity)
    new Int32Collection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    int Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(int value)
    void AddRange(int[] range)
    void Clear()
    bool Contains(int value)
    void CopyTo(int[] array, int index)
    Int32CollectionEnumerator GetEnumerator()
    int IndexOf(int value)
    void Insert(int index, int value)
    void Remove(int value)
    void RemoveAt(int index)
    int[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] Int32CollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.Int32CollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new Int32CollectionEnumerator(Int32Collection col)

  PROPERTIES:
    int Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] IntegerCollection
Full Name: Autodesk.AutoCAD.Geometry.IntegerCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new IntegerCollection(int[] values)
    new IntegerCollection(int capacity)
    new IntegerCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    int Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(int value)
    void AddRange(int[] range)
    void Clear()
    bool Contains(int value)
    void CopyTo(int[] array, int index)
    IntegerCollectionEnumerator GetEnumerator()
    int IndexOf(int value)
    void Insert(int index, int value)
    void Remove(int value)
    void RemoveAt(int index)
    int[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] IntegerCollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.IntegerCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new IntegerCollectionEnumerator(IntegerCollection col)

  PROPERTIES:
    int Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] Interval
Full Name: Autodesk.AutoCAD.Geometry.Interval
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new Interval(Interval other)
    new Interval(bool boundedBelow, double bound, double tolerance)
    new Interval(double lower, double upper, double tolerance)
    new Interval(double tolerance)

  PROPERTIES:
    double Element { get; }
    bool IsBounded { get; }
    bool IsBoundedAbove { get; }
    bool IsBoundedBelow { get; }
    bool IsSingleton { get; }
    bool IsUnbounded { get; }
    double Length { get; }
    double LowerBound { get; }
    double Tolerance { get; }
    static Interval Unbounded { get; }
    double UpperBound { get; }

  METHODS:
    bool Contains(double value)
    bool Contains(Interval otherInterval)
    double[] GetBounds()
    Interval GetMerge(Interval otherInterval)
    Interval IntersectWith(Interval otherInterval)
    bool IsContinuousAtUpper(Interval otherInterval)
    bool IsDisjoint(Interval otherInterval)
    bool IsEqualAtLower(double value)
    bool IsEqualAtLower(Interval otherInterval)
    bool IsEqualAtUpper(double value)
    bool IsEqualAtUpper(Interval otherInterval)
    bool IsOverlapAtUpper(Interval otherInterval, Interval overlap)
    bool IsPeriodicallyOn(double period, out double value)
    Interval[] Subtract(Interval otherInterval)

--------------------------------------------------------------------------------

[CLASS] IntPtrCollection
Full Name: Autodesk.AutoCAD.Geometry.IntPtrCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new IntPtrCollection(IntPtr[] values)
    new IntPtrCollection(int capacity)
    new IntPtrCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    IntPtr Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(IntPtr value)
    void AddRange(IntPtr[] range)
    void Clear()
    bool Contains(IntPtr value)
    void CopyTo(IntPtr[] array, int index)
    IntPtrCollectionEnumerator GetEnumerator()
    int IndexOf(IntPtr value)
    void Insert(int index, IntPtr value)
    void Remove(IntPtr value)
    void RemoveAt(int index)
    IntPtr[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] IntPtrCollectionEnumerator
Full Name: Autodesk.AutoCAD.Geometry.IntPtrCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new IntPtrCollectionEnumerator(IntPtrCollection col)

  PROPERTIES:
    IntPtr Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] KnotCollection
Full Name: Autodesk.AutoCAD.Geometry.KnotCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new KnotCollection()

  PROPERTIES:
    int Count { get; }
    double EndParameter { get; }
    double Item { get; set; }
    int NumberOfIntervals { get; }
    double StartParameter { get; }
    double Tolerance { get; set; }

  METHODS:
    int Add(double value)
    void Clear()
    bool Contains(double value)
    void CopyTo(double[] array, int index)
    DoubleCollection GetDistinctKnots()
    IEnumerator GetEnumerator()
    int GetInterval(int order, double parameter, out Interval interval)
    int GetMultiplicityAt(int i)
    void Insert(int index, double value)
    bool IsOn(double knot)
    void RemoveAt(int index)
    void Reverse()
    void SetRange(double lower, double upper)
    KnotCollection[] Split(double parameter, int multiplicityLast, int multiplicityFirst)

--------------------------------------------------------------------------------

[ENUM] KnotParameterizationEnum
Full Name: Autodesk.AutoCAD.Geometry.KnotParameterizationEnum
Inherits: Enum

  Values:
    - Chord = 0
    - SqrtChord = 1
    - Uniform = 2
    - CustomParameterization = 15
    - NotDefinedParameterization = 16

--------------------------------------------------------------------------------

[CLASS] Line2d
Full Name: Autodesk.AutoCAD.Geometry.Line2d
Inherits: LinearEntity2d

  CONSTRUCTORS:
    new Line2d(Point2d point1, Point2d point2)
    new Line2d(Point2d point, Vector2d vector)
    new Line2d()

  METHODS:
    static Line2d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(Point2d point1, Point2d point2)
    void Set(Point2d point, Vector2d vector)

--------------------------------------------------------------------------------

[CLASS] Line2dCollection
Full Name: Autodesk.AutoCAD.Geometry.Line2dCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new Line2dCollection()

  PROPERTIES:
    int Count { get; }
    Line2d Item { get; set; }

  METHODS:
    int Add(Line2d value)
    void Clear()
    bool Contains(Line2d value)
    void CopyTo(Line2d[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(Line2d value)
    void Insert(int index, Line2d value)
    void Remove(Line2d value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] Line3d
Full Name: Autodesk.AutoCAD.Geometry.Line3d
Inherits: LinearEntity3d

  CONSTRUCTORS:
    new Line3d(Point3d point1, Point3d point2)
    new Line3d(Point3d point, Vector3d vector)
    new Line3d()

  METHODS:
    static Line3d Create(IntPtr unmanagedPointer, bool autoDelete)
    void Set(Point3d point1, Point3d point2)
    void Set(Point3d point, Vector3d vector)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LinearEntity2d
Full Name: Autodesk.AutoCAD.Geometry.LinearEntity2d
Inherits: Curve2d

  PROPERTIES:
    Vector2d Direction { get; }
    Point2d PointOnLine { get; }

  METHODS:
    static LinearEntity2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Line2d GetLine()
    Line2d GetPerpendicularLine(Point2d point)
    Point2d[] IntersectWith(LinearEntity2d line)
    Point2d[] IntersectWith(LinearEntity2d line, Tolerance tolerance)
    bool IsColinearTo(LinearEntity2d line)
    bool IsColinearTo(LinearEntity2d line, Tolerance tolerance)
    bool IsParallelTo(LinearEntity2d line)
    bool IsParallelTo(LinearEntity2d line, Tolerance tolerance)
    bool IsPerpendicularTo(LinearEntity2d line)
    bool IsPerpendicularTo(LinearEntity2d line, Tolerance tolerance)
    LinearEntity2d Overlap(LinearEntity2d line)
    LinearEntity2d Overlap(LinearEntity2d line, Tolerance tolerance)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] LinearEntity3d
Full Name: Autodesk.AutoCAD.Geometry.LinearEntity3d
Inherits: Curve3d

  PROPERTIES:
    Vector3d Direction { get; }
    Point3d PointOnLine { get; }

  METHODS:
    static LinearEntity3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Line3d GetLine()
    Plane GetPerpendicularPlane(Point3d intersectionPoint)
    Point3d[] IntersectWith(PlanarEntity plane)
    Point3d[] IntersectWith(PlanarEntity plane, Tolerance tolerance)
    Point3d[] IntersectWith(LinearEntity3d line)
    Point3d[] IntersectWith(LinearEntity3d line, Tolerance tolerance)
    bool IsColinearTo(LinearEntity3d line)
    bool IsColinearTo(LinearEntity3d line, Tolerance tolerance)
    bool IsOn(Plane plane)
    bool IsOn(Plane plane, Tolerance tolerance)
    bool IsParallelTo(PlanarEntity plane)
    bool IsParallelTo(PlanarEntity plane, Tolerance tolerance)
    bool IsParallelTo(LinearEntity3d line)
    bool IsParallelTo(LinearEntity3d line, Tolerance tolerance)
    bool IsPerpendicularTo(PlanarEntity plane)
    bool IsPerpendicularTo(PlanarEntity plane, Tolerance tolerance)
    bool IsPerpendicularTo(LinearEntity3d line)
    bool IsPerpendicularTo(LinearEntity3d line, Tolerance tolerance)
    LinearEntity3d Overlap(LinearEntity3d line)
    LinearEntity3d Overlap(LinearEntity3d line, Tolerance tolerance)
    Point3d[] ProjectedIntersectWith(LinearEntity3d line, Vector3d projectionDirection)
    Point3d[] ProjectedIntersectWith(LinearEntity3d line, Vector3d projectionDirection, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] LineSegment2d
Full Name: Autodesk.AutoCAD.Geometry.LineSegment2d
Inherits: LinearEntity2d

  CONSTRUCTORS:
    new LineSegment2d(Point2d point, Vector2d vector)
    new LineSegment2d(Point2d point1, Point2d point2)
    new LineSegment2d()

  PROPERTIES:
    Point2d EndPoint { get; }
    double Length { get; }
    Point2d MidPoint { get; }
    Point2d StartPoint { get; }

  METHODS:
    Point2d BaryComb(double blendCoefficient)
    static LineSegment2d Create(IntPtr unmanagedPointer, bool autoDelete)
    Line2d GetBisector()
    double GetSegmentLength(double fromParameter, double toParameter)
    double GetSegmentLength(double fromParameter, double toParameter, Tolerance tolerance)
    void Set(Curve2d curve, Point2d point, double parameter)
    void Set(Curve2d curve1, Curve2d curve2, double parameter1, double parameter2)
    void Set(Point2d point1, Point2d point2)
    void Set(Point2d point, Vector2d vector)

--------------------------------------------------------------------------------

[CLASS] LineSegment3d
Full Name: Autodesk.AutoCAD.Geometry.LineSegment3d
Inherits: LinearEntity3d

  CONSTRUCTORS:
    new LineSegment3d(Point3d point1, Point3d point2)
    new LineSegment3d(Point3d point, Vector3d vector)
    new LineSegment3d()

  PROPERTIES:
    Point3d EndPoint { get; }
    double Length { get; }
    Point3d MidPoint { get; }
    Point3d StartPoint { get; }

  METHODS:
    Point3d BaryComb(double blendCoefficient)
    static LineSegment3d Create(IntPtr unmanagedPointer, bool autoDelete)
    Plane GetBisector()
    void Set(Curve3d curve, Point3d point, double parameter)
    void Set(Curve3d curve1, Curve3d curve2, double parameter1, double parameter2)
    void Set(Point3d point1, Point3d point2)
    void Set(Point3d point, Vector3d vector)

--------------------------------------------------------------------------------

