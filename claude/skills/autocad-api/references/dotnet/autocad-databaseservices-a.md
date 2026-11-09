# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[ABSTRACT CLASS] AbstractViewTable
Full Name: Autodesk.AutoCAD.DatabaseServices.AbstractViewTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AbstractViewTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.AbstractViewTableRecord
Inherits: SymbolTableRecord

  PROPERTIES:
    Color AmbientLightColor { get; set; }
    double BackClipDistance { get; set; }
    bool BackClipEnabled { get; set; }
    ObjectId Background { get; set; }
    double Brightness { get; set; }
    Point2d CenterPoint { get; set; }
    double Contrast { get; set; }
    bool DefaultLightingOn { get; set; }
    DefaultLightingType DefaultLightingType { get; set; }
    double Elevation { get; set; }
    bool FrontClipAtEye { get; set; }
    double FrontClipDistance { get; set; }
    bool FrontClipEnabled { get; set; }
    double Height { get; set; }
    double LensLength { get; set; }
    bool PerspectiveEnabled { get; set; }
    ObjectId SunId { get; }
    Point3d Target { get; set; }
    ToneOperatorParameters ToneOperatorParameters { get; set; }
    CoordinateSystem3d Ucs { get; }
    ObjectId UcsName { get; }
    OrthographicView UcsOrthographic { get; }
    Vector3d ViewDirection { get; set; }
    OrthographicView ViewOrthographic { get; }
    double ViewTwist { get; set; }
    ObjectId VisualStyleId { get; set; }
    double Width { get; set; }

  METHODS:
    ObjectId SetSun(DBObject sun)
    void SetUcs(ObjectId id)
    void SetUcs(OrthographicView view)
    void SetUcs(Point3d origin, Vector3d x, Vector3d y)
    void SetUcsToWorld()
    void SetViewDirection(OrthographicView view)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ActionsToEvaluateCallback
Full Name: Autodesk.AutoCAD.DatabaseServices.ActionsToEvaluateCallback
Implements: IDisposable

  CONSTRUCTORS:
    new ActionsToEvaluateCallback()

  METHODS:
    void Dispose()
    void NeedsToEvaluate(ObjectId objectId, AssocStatus newStatus, bool ownedActionsAlso)

--------------------------------------------------------------------------------

[CLASS] AddObjectSnapInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.AddObjectSnapInfo
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new AddObjectSnapInfo(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(ObjectSnapContext context, ObjectSnapInfo result, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(ObjectSnapContext context, ObjectSnapInfo result)

--------------------------------------------------------------------------------

[CLASS] AlignedDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.AlignedDimension
Inherits: Dimension

  CONSTRUCTORS:
    new AlignedDimension(Point3d line1Point, Point3d line2Point, Point3d dimensionLinePoint, string dimensionText, ObjectId dimensionStyle)
    new AlignedDimension()

  PROPERTIES:
    Point3d DimLinePoint { get; set; }
    double Oblique { get; set; }
    Point3d XLine1Point { get; set; }
    Point3d XLine2Point { get; set; }

--------------------------------------------------------------------------------

[ENUM] AngleConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.AngleConstraint
Inherits: Enum

  Values:
    - DegreesAny = 0
    - Degrees015 = 1
    - Degrees030 = 2
    - Degrees045 = 3
    - Degrees060 = 4
    - Degrees090 = 6
    - DegreesHorz = 12

--------------------------------------------------------------------------------

[CLASS] AngularConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.AngularConstraint
Inherits: ExplicitConstraint

  CONSTRUCTORS:
    new AngularConstraint(AngularSectorType type)
    new AngularConstraint()

  PROPERTIES:
    AngularSectorType SectorType { get; }

  METHODS:
    static double AngleMultiplier()
    static void SetAngleMultiplier(double multiplier)

--------------------------------------------------------------------------------

[CLASS] AnnotationScale
Full Name: Autodesk.AutoCAD.DatabaseServices.AnnotationScale
Inherits: ObjectContext

  CONSTRUCTORS:
    new AnnotationScale()

  PROPERTIES:
    string CollectionName { get; }
    double DrawingUnits { get; set; }
    bool IsTemporaryScale { get; }
    string Name { get; set; }
    double PaperUnits { get; set; }
    double Scale { get; }
    IntPtr UniqueIdentifier { get; }

--------------------------------------------------------------------------------

[ENUM] AnnotationType
Full Name: Autodesk.AutoCAD.DatabaseServices.AnnotationType
Inherits: Enum

  Values:
    - MText = 0
    - FeatureControlFrame = 1
    - BlockRef = 2
    - NoAnnotation = 3

--------------------------------------------------------------------------------

[ENUM] AnnotativeStates
Full Name: Autodesk.AutoCAD.DatabaseServices.AnnotativeStates
Inherits: Enum

  Values:
    - True = 0
    - False = 1
    - NotApplicable = 2

--------------------------------------------------------------------------------

[ENUM] ApplicationLoadReasons
Full Name: Autodesk.AutoCAD.DatabaseServices.ApplicationLoadReasons
Inherits: Enum

  Values:
    - OnProxyDetection = 1
    - OnAutoCADStartup = 2
    - OnCommandInvocation = 4
    - OnLoadRequest = 8
    - LoadDisabled = 16
    - TransparentlyLoadable = 32

--------------------------------------------------------------------------------

[CLASS] Arc
Full Name: Autodesk.AutoCAD.DatabaseServices.Arc
Inherits: Curve

  CONSTRUCTORS:
    new Arc(Point3d center, Vector3d normal, double radius, double startAngle, double endAngle)
    new Arc(Point3d center, double radius, double startAngle, double endAngle)
    new Arc()

  PROPERTIES:
    Point3d Center { get; set; }
    double EndAngle { get; set; }
    double Length { get; }
    Vector3d Normal { get; set; }
    double Radius { get; set; }
    double StartAngle { get; set; }
    double Thickness { get; set; }
    double TotalAngle { get; }

--------------------------------------------------------------------------------

[CLASS] ArcDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.ArcDimension
Inherits: Dimension

  CONSTRUCTORS:
    new ArcDimension(Point3d centerPoint, Point3d xLine1Point, Point3d xLine2Point, Point3d arcPoint, string dimensionText, ObjectId dimensionStyle)

  PROPERTIES:
    double ArcEndParam { get; set; }
    Point3d ArcPoint { get; set; }
    double ArcStartParam { get; set; }
    int ArcSymbolType { get; set; }
    Point3d CenterPoint { get; set; }
    bool HasLeader { get; set; }
    bool IsPartial { get; set; }
    Point3d Leader1Point { get; set; }
    Point3d Leader2Point { get; set; }
    Point3d XLine1Point { get; set; }
    Point3d XLine2Point { get; set; }

--------------------------------------------------------------------------------

[CLASS] Assoc2dConstraintCallback
Full Name: Autodesk.AutoCAD.DatabaseServices.Assoc2dConstraintCallback
Implements: IDisposable

  CONSTRUCTORS:
    new Assoc2dConstraintCallback(AcDbAssoc2dConstraintCallback* impObj, bool autoDelete)
    new Assoc2dConstraintCallback()

  METHODS:
    bool CanBeRelaxed(ExplicitConstraint constraint)
    void ConstraintDeactivated(ExplicitConstraint constraint, bool deactivated)
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] Assoc2dConstraintGroup
Full Name: Autodesk.AutoCAD.DatabaseServices.Assoc2dConstraintGroup
Inherits: AssocAction

  CONSTRUCTORS:
    new Assoc2dConstraintGroup(Plane plane)

  PROPERTIES:
    ConstrainedGeometry[] ConstrainedGeometries { get; }
    GeometricalConstraint[] Constraints { get; }
    int GetDOF { get; }
    Plane WorkPlane { get; set; }

  METHODS:
    ThreePointAngleConstraint Add3PointAngularConstraint(ConstrainedPoint consPoint1, ConstrainedPoint consPoint2, ConstrainedPoint consPoint3, AngularSectorType sectorType, ObjectId valueDependencyId, ObjectId dimDependencyId)
    AngularConstraint AddAngularConstraint(ConstrainedLine consLine1, ConstrainedLine consLine2, AngularSectorType sectorType, ObjectId valueDependencyId, ObjectId dimDependencyId)
    ConstrainedGeometry AddConstrainedGeometry(FullSubentityPath subentPath)
    DistanceConstraint AddDistanceConstraint(ConstrainedGeometry consGeom1, ConstrainedGeometry consGeom2, DistanceDirectionType directionType, ObjectId valueDependencyId, ObjectId dimDependencyId, Vector3d fixedDirection, ConstrainedLine directionLine)
    GeometricalConstraint AddGeometricalConstraint(ConstraintType type, ConstrainedGeometry[] consGeoms)
    GeometricalConstraint AddGeometricalConstraint(ConstraintType type, FullSubentityPath[] paths)
    static void AddGlobalCallback(Assoc2dConstraintCallback callback)
    RadiusDiameterConstraint AddRadiusDiameterConstraint(ConstrainedGeometry consGeom, RadDiaConstrType type, ObjectId valueDependencyId, ObjectId dimDependencyId)
    void AutoConstrain(FullSubentityPath[] paths, Tolerance tol, AutoConstrainEvaluationCallback callback)
    SolutionStatusType ConstraintStatus(GeometricalConstraint constraint)
    void DeleteConstrainedGeometry(ObjectId geomDependencyId)
    void DeleteConstraint(GeometricalConstraint geomConst)
    void GeometryMirrored(AssocGeomDependency geomDependency)
    SolutionStatusType GeometryStatus(ConstrainedGeometry consGeom)
    ObjectIdCollection GetAllConnectedGeomDependencies(ObjectIdCollection srcGeomDependencyIds)
    ConstrainedGeometry GetConstrainedGeometry(AssocGeomDependency geomDependency, ImplicitPointType ptType, int defPtIndex, bool bCreateArcLineMid)
    ConstrainedGeometry GetConstrainedGeometry(AssocGeomDependency geomDependency)
    ConstrainedGeometry GetConstrainedGeometry(FullSubentityPath subentPath, bool createArcLineMid)
    ConstraintGroupNode GetGroupNode(uint nodeId)
    static Assoc2dConstraintCallback GlobalCallback()
    void RegenDimensionSystem()
    static void RemoveGlobalCallback(Assoc2dConstraintCallback callback)
    SolutionStatusType SolutionStatus(bool alsoCheckForConstraints)
    void TransformActionBy(Matrix3d transform)

--------------------------------------------------------------------------------

[CLASS] AssocAction
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocAction
Inherits: DBObject

  CONSTRUCTORS:
    new AssocAction()

  PROPERTIES:
    ObjectId ActionBody { get; set; }
    AssocEvaluationCallback CurrentEvaluationCallback { get; }
    bool IsActionBodyAProxy { get; }
    bool IsActionEvaluationInProgress { get; }
    ObjectId OwningNetwork { get; }
    AssocStatus Status { get; set; }

  METHODS:
    void AddDependency(ObjectId dependencyId, bool setThisActionAsOwningAction)
    void AddMoreObjectsToDeepClone(IdMapping idMap, ObjectIdCollection additionalObjectsToClone)
    bool AreDependenciesEqual(AssocDependency dependency1, AssocDependency dependency2)
    bool AreDependenciesOnTheSameThing(AssocDependency dependency1, AssocDependency dependency2)
    void DependentObjectCloned(AssocDependency dependency, DBObject dbObj, DBObject newObj)
    void DragStatus(DragStatus status)
    void Evaluate(AssocEvaluationCallback evaluationCallback)
    void EvaluateDependencies()
    void EvaluateDependency(AssocDependency dependency)
    AssocEvaluationPriority EvaluationPriority()
    static ObjectId GetActionBody(ObjectId actionId)
    static void GetActionsDependentOnObject(DBObject dbObj, bool readDependenciesWanted, bool writeDependenciesWanted, ObjectIdCollection actionIds)
    static ObjectIdCollection GetActionsDependentOnObject(DBObject dbObj, bool readDependenciesWanted, bool writeDependenciesWanted)
    ObjectIdCollection GetDependencies(bool readDependenciesWanted, bool writeDependenciesWanted)
    void GetDependentActionsToEvaluate(ActionsToEvaluateCallback actionsToEvaluateCallback)
    ObjectIdCollection GetDependentObjects(bool readDependenciesWanted, bool writeDependenciesWanted)
    bool HasDependencyCachedValue(AssocDependency dependency)
    bool IsEqualTo(AssocAction otherAction)
    bool IsExternalDependency(AssocDependency dependency)
    bool IsOwnedDependency(AssocDependency dependency)
    bool IsRelevantDependencyChange(AssocDependency dependency)
    ObjectId ObjectThatOwnsNetworkInstance()
    void OwnedDependencyStatusChanged(AssocDependency ownedDependency, AssocStatus previousStatus)
    void PostProcessAfterDeepClone(IdMapping idMap)
    void PostProcessAfterDeepCloneCancel(IdMapping idMap)
    static void RemoveActionsControllingObject(ObjectId objID, int readOnlyDependencyHandling, ObjectId objectToRedirectReadOnlyDependenciesTo)
    static void RemoveActionsControllingObject(ObjectId objID, int readOnlyDependencyHandling)
    static void RemoveActionsControllingObject(ObjectId objID)
    void RemoveAllDependencies(bool alsoEraseThem)
    void RemoveDependency(ObjectId dependencyId, bool alsoEraseIt)
    void SetOwningNetwork(ObjectId networkId, bool alsoSetAsDatabaseOwner)
    void SetStatus(AssocStatus newStatus, bool notifyOwningNetwork, bool setInOwnedActions)
    void TransformActionBy(Matrix3d transform)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocActionBody
Inherits: DBObject

  CONSTRUCTORS:
    new AssocActionBody()

  PROPERTIES:
    ObjectIdCollection OwnedParams { get; }
    ObjectId OwningNetwork { get; }
    int ParamCount { get; }
    ObjectId ParentAction { get; }
    AssocStatus Status { get; set; }

  METHODS:
    void AddDependency(ObjectId dependencyId, bool setThisActionAsOwningAction)
    void AddMoreObjectsToDeepCloneOverride(IdMapping idMap, ObjectIdCollection additionalObjectsToClone)
    void AddParam(ObjectId paramId, ref int paramIndex)
    bool AreDependenciesEqualOverride(AssocDependency dependency1, AssocDependency dependency2)
    bool AreDependenciesOnTheSameThingOverride(AssocDependency dependency1, AssocDependency dependency2)
    static void CreateActionAndActionBodyAndPostToDatabase(RXClass pActionBodyClass, ObjectId objectId, ObjectId createdActionId, ObjectId createdActionBodyId)
    AssocEvaluationCallback currentEvaluationCallback()
    void DependentObjectClonedOverride(AssocDependency pDependency, DBObject dbObj, DBObject dbNewObj)
    void DragStatusOverride(DragStatus status)
    ErrorStatus DwgInFields(DwgFiler filer)
    ErrorStatus DwgOutFields(DwgFiler filer)
    ErrorStatus DxfInFields(DxfFiler filer)
    ErrorStatus DxfOutFields(DxfFiler filer)
    void EvaluateDependencies()
    bool EvaluateDependencyOverride(AssocDependency dependency)
    void EvaluateOverride()
    void EvaluationPriorityOverride(ref AssocEvaluationPriority priority)
    static void GetActionBodiesOnObject(DBObject pObject, bool ignoreInternalActions, bool ignoreSuppressedActions, out ObjectId creationActionBodyId, out ObjectIdCollection modificationActionBodyIds, out ObjectIdCollection readOnlyActionBodyIds)
    ObjectIdCollection GetDependencies(bool readDependenciesWanted, bool writeDependenciesWanted)
    ObjectIdCollection GetDependenciesOverride(bool readDependenciesWanted, bool writeDependenciesWanted)
    void GetDependentActionsToEvaluateOverride(ActionsToEvaluateCallback actionsToEvaluateCallback)
    ObjectIdCollection GetDependentObjectsOverride(bool readDependenciesWanted, bool writeDependenciesWanted)
    static ObjectId GetParentAction(ObjectId actionBodyId)
    ValueParam GetValueParam(string paramName, int paramIndex)
    ValueParam GetValueParam(string paramName)
    ValueParam[] GetValueParamArray(string paramName)
    UnitType GetValueParamUnitType(string paramName)
    bool HasAnyErasedOrBrokenDependencies()
    bool HasDependencyCachedValueOverride(AssocDependency dependency)
    bool IsActionEvaluationInProgress()
    bool IsEqualToOverride(AssocAction otherAction)
    bool IsExternalDependencyOverride(AssocDependency dependency)
    bool IsOwnedDependencyOverride(AssocDependency dependency)
    bool IsRelevantDependencyChangeOverride(AssocDependency dependency)
    void OwnedDependencyStatusChangedOverride(AssocDependency ownedDependency, AssocStatus previousStatus)
    string[] OwnedValueParamNames()
    ObjectId ParamAtIndex(int paramIndex)
    ObjectId ParamAtName(string paramName, int index)
    ObjectId ParamAtName(string paramName)
    ObjectIdCollection ParamsAtName(string paramName)
    void PostProcessAfterDeepCloneCancelOverride(IdMapping idMap)
    void PostProcessAfterDeepCloneOverride(IdMapping idMap)
    void RemoveAllDependencies(bool alsoEraseThem)
    void RemoveAllDependenciesOverride(bool alsoEraseThem)
    void RemoveAllParams(bool alsoEraseThem)
    void RemoveDependency(ObjectId dependencyId, bool alsoEraseIt)
    void RemoveParam(ObjectId paramId, bool alsoEraseIt)
    void RemoveValueParam(string paramName)
    void SetStatus(AssocStatus newStatus, bool notifyOwningNetwork, bool setInOwnedActions)
    string SetValueParam(string paramName, ValueParam param, bool silentMode)
    string SetValueParam(string paramName, ValueParam param, bool silentMode, int paramIndex)
    string[] SetValueParamArray(string paramName, ValueParam[] valueParams, bool silentMode)
    void SetValueParamUnitType(string paramName, UnitType unitType)
    void TransformActionByOverride(Matrix3d A_0)
    ObjectIdCollection ValueParamInputVariables(string paramName)

--------------------------------------------------------------------------------

[CLASS] AssocActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocActionParam
Inherits: DBObject

  CONSTRUCTORS:
    new AssocActionParam()

  PROPERTIES:
    string Name { get; set; }
    ObjectId ParentAction { get; }

  METHODS:
    void DetachDependencies()
    ObjectIdCollection GetDependencies(bool readDependenciesWanted, bool writeDependenciesWanted)
    void MakeParamConstant()
    void MakeParamEmpty(bool alsoEraseOwnedObjects)
    void TransformConstantGeometry(Matrix3d transform)

--------------------------------------------------------------------------------

[CLASS] AssocArray
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocArray

  PROPERTIES:
    ObjectId EntityId { get; }
    ObjectIdCollection SourceEntities { get; }

  METHODS:
    void AddSourceEntity(ObjectId sourceEntity, Point3d basePoint)
    static AssocArray CreateArray(ObjectIdCollection sourceEntities, VertexRef basePoint, AssocArrayParameters parameters)
    void DeleteItem(ItemLocator index, bool bErase)
    static ObjectIdCollection Explode(ObjectId entity)
    static AssocArray GetAssociativeArray(ObjectId entity)
    static ItemLocator[] getItemLocators(FullSubentityPath[] subPaths)
    ItemLocator[] getItems(bool skipErased)
    Matrix3d GetItemTransform(ItemLocator index)
    AssocArrayParameters GetParameters()
    static bool IsAssociativeArray(ObjectId entity)
    bool IsErased(ItemLocator index)
    void RemoveSourceEntity(ObjectId sourceEntity)
    void ReplaceItems(ItemLocator[] indices, ObjectIdCollection substEntities, VertexRef basePoint)
    void ResetItems()
    void TransformItemBy(ItemLocator index, Matrix3d transform)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocArrayCommonParameters
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocArrayCommonParameters
Inherits: AssocArrayParameters

  PROPERTIES:
    Vector3d BaseNormal { get; set; }
    FaceRef BasePlane { get; set; }
    VertexRef BasePoint { get; set; }
    int LevelCount { get; set; }
    double LevelSpacing { get; set; }
    int RowCount { get; set; }
    double RowElevation { get; set; }
    double RowSpacing { get; set; }

  METHODS:
    int GetLevelCount(out string expression, out string evaluatorId)
    double GetLevelSpacing(out string expression, out string evaluatorId)
    int GetRowCount(out string expression, out string evaluatorId)
    double GetRowElevation(out string expression, out string evaluatorId)
    double GetRowSpacing(out string expression, out string evaluatorId)
    void SetLevelCount(int count, string expression, string evaluatorId)
    void SetLevelSpacing(double spacing, string expression, string evaluatorId)
    void SetRowCount(int count, string expression, string evaluatorId)
    void SetRowElevation(double value, string expression, string evaluatorId)
    void SetRowSpacing(double spacing, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocArrayParameters
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocArrayParameters
Inherits: RXObject

  PROPERTIES:
    AssocArray Owner { get; }

  METHODS:
    void Commit()

--------------------------------------------------------------------------------

[CLASS] AssocArrayPathParameters
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocArrayPathParameters
Inherits: AssocArrayCommonParameters

  CONSTRUCTORS:
    new AssocArrayPathParameters(double itemSpacing, double rowSpacing, double levelSpacing, int itemCount, int rowCount, int levelCount, double rowElevation)
    new AssocArrayPathParameters()

  PROPERTIES:
    bool AlignItems { get; set; }
    double EndOffset { get; set; }
    int ItemCount { get; set; }
    double ItemSpacing { get; set; }
    MethodType Method { get; set; }
    EdgeRef Path { get; set; }
    bool PathDirection { set; }
    double StartOffset { get; set; }

  METHODS:
    double GetEndOffset(out string expression, out string evaluatorId)
    int GetItemCount(out string expression, out string evaluatorId)
    double GetItemSpacing(out string expression, out string evaluatorId)
    double GetStartOffset(out string expression, out string evaluatorId)
    void SetEndOffset(double offset, string expression, string evaluatorId)
    void SetItemCount(int count, string expression, string evaluatorId)
    void SetItemSpacing(double spacing, string expression, string evaluatorId)
    void SetStartOffset(double offset, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocArrayPolarParameters
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocArrayPolarParameters
Inherits: AssocArrayCommonParameters

  CONSTRUCTORS:
    new AssocArrayPolarParameters(double angle, double rowSpacing, double levelSpacing, int itemCount, int rowCount, int levelCount, double rowElevation)
    new AssocArrayPolarParameters()

  PROPERTIES:
    double AngleBetweenItems { get; set; }
    ArcDirection Direction { get; set; }
    double FillAngle { get; set; }
    int ItemCount { get; set; }
    double Radius { get; set; }
    bool RotateItems { get; set; }
    double StartAngle { get; set; }

  METHODS:
    double GetAngleBetweenItems(out string expression, out string evaluatorId)
    double GetFillAngle(out string expression, out string evaluatorId)
    int GetItemCount(out string expression, out string evaluatorId)
    double GetRadius(out string expression, out string evaluatorId)
    double GetStartAngle(out string expression, out string evaluatorId)
    void SetAngleBetweenItems(double val, string expression, string evaluatorId)
    void SetFillAngle(double val, string expression, string evaluatorId)
    void SetItemCount(int count, string expression, string evaluatorId)
    void SetRadius(double val, string expression, string evaluatorId)
    void SetStartAngle(double val, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocArrayRectangularParameters
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocArrayRectangularParameters
Inherits: AssocArrayCommonParameters

  CONSTRUCTORS:
    new AssocArrayRectangularParameters(double columnSpacing, double rowSpacing, double levelSpacing, int columnCount, int rowCount, int levelCount, double rowElevation, double axesAngle)
    new AssocArrayRectangularParameters()

  PROPERTIES:
    double AxesAngle { get; set; }
    int ColumnCount { get; set; }
    double ColumnSpacing { get; set; }
    Vector3d XAxisDirection { get; set; }
    Vector3d YAxisDirection { get; set; }

  METHODS:
    double GetAxesAngle(out string expression, out string evaluatorId)
    int GetColumnCount(out string expression, out string evaluatorId)
    double GetColumnSpacing(out string expression, out string evaluatorId)
    void SetAxesAngle(double val, string expression, string evaluatorId)
    void SetColumnCount(int count, string expression, string evaluatorId)
    void SetColumnSpacing(double spacing, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocAsmBodyActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocAsmBodyActionParam
Inherits: AssocActionParam

  CONSTRUCTORS:
    new AssocAsmBodyActionParam()

  PROPERTIES:
    CompoundObjectId DependentOnCompoundObject { get; }

  METHODS:
    void SetBody(ObjectId bodyId, bool isReadDep, bool isWriteDep)
    void SetBody(Entity entity, bool isReadDep, bool isWriteDep)

--------------------------------------------------------------------------------

[CLASS] AssocBlendSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocBlendSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocBlendSurfaceActionBody()

  PROPERTIES:
    BlendOptions BlendOption { get; set; }
    double EndEdgeBulge { get; set; }
    short EndEdgeContinuity { get; set; }
    double StartEdgeBulge { get; set; }
    short StartEdgeContinuity { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, LoftProfile startProfile, LoftProfile endProfile, BlendOptions blendOptions, bool enable)
    void GetContinuityGripPoints(out Point3d startEdgePt, out Point3d endEdgePt)
    double GetEndEdgeBulge(out string expression, out string evaluatorId)
    short GetEndEdgeContinuity(out string expression, out string evaluatorId)
    void GetProfiles(out LoftProfile startProfile, out LoftProfile endProfile)
    double GetStartEdgeBulge(out string expression, out string evaluatorId)
    short GetStartEdgeContinuity(out string expression, out string evaluatorId)
    void SetEndEdgeBulge(double value, string expression, string evaluatorId)
    void SetEndEdgeContinuity(short value, string expression, string evaluatorId)
    void SetStartEdgeBulge(double value, string expression, string evaluatorId)
    void SetStartEdgeContinuity(short value, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocCompoundActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocCompoundActionParam
Inherits: AssocActionParam

  CONSTRUCTORS:
    new AssocCompoundActionParam()

  PROPERTIES:
    ObjectId Item { get; }
    ObjectIdCollection OwnedParams { get; }
    int ParamCount { get; }

  METHODS:
    int AddParam(ObjectId paramId)
    ObjectId ParamAtIndex(int index)
    ObjectId ParamAtName(string paramName, int index)
    ObjectId ParamAtName(string paramName)
    ObjectIdCollection ParamsAtName(string paramName)
    void RemoveAllParams(bool alsoEraseThem)
    void RemoveParam(ObjectId paramId, bool alsoEraseIt)

--------------------------------------------------------------------------------

[ENUM] AssocConstraintType
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocConstraintType
Inherits: Enum

  Values:
    - NoneAssocConstraintType = 0
    - DistanceAssocConstraintType = 1
    - HorizontalDistanceAssocConstraintType = 2
    - VerticalDistanceAssocConstraintType = 3
    - Angle0AssocConstraintType = 4
    - Angle1AssocConstraintType = 5
    - Angle2AssocConstraintType = 6
    - Angle3AssocConstraintType = 7
    - RadiusAssocConstraintType = 8
    - DiameterAssocConstraintType = 9

--------------------------------------------------------------------------------

[CLASS] AssocDependency
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocDependency
Inherits: DBObject

  CONSTRUCTORS:
    new AssocDependency()

  PROPERTIES:
    AssocEvaluationCallback CurrentEvaluationCallback { get; }
    ObjectId DependencyBody { get; set; }
    CompoundObjectId DependentOnCompoundObject { get; }
    ObjectId DependentOnObject { get; }
    ErrorStatus DependentOnObjectStatus { get; }
    bool HasCachedValue { get; }
    bool IsActionEvaluationInProgress { get; }
    bool IsAttachedToObject { get; }
    bool IsDelegatingToOwningAction { get; set; }
    bool IsDependentOnCompoundObject { get; }
    bool IsDependentOnObjectReadOnly { get; }
    bool IsObjectStateDependent { get; set; }
    bool IsReadDependency { get; set; }
    bool IsRelevantChange { get; }
    bool IsWriteDependency { get; set; }
    ObjectId NextDependencyOnObject { get; }
    int Order { get; set; }
    ObjectId OwningAction { get; set; }
    ObjectId PrevDependencyOnObject { get; }
    AssocStatus Status { get; set; }

  METHODS:
    void AttachToObject(CompoundObjectId compoundId)
    void DetachFromObject()
    void Evaluate()
    static ObjectIdCollection GetDependenciesOnObject(DBObject dbObj, bool readDependenciesWanted, bool writeDependenciesWanted)
    static ObjectId GetFirstDependencyOnObject(DBObject dbObj)
    bool IsDependentOnTheSameThingAs(AssocDependency otherDependency)
    bool IsEqualTo(AssocDependency otherDependency)
    static void NotifyDependenciesOnObject(DBObject dbObj, AssocStatus newStatus)
    void SetDependentOnObject(CompoundObjectId compoundId)
    void SetStatus(AssocStatus newStatus, bool notifyOwningNetwork)
    void UpdateDependentOnObject()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocDependencyBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocDependencyBody
Inherits: DBObject

  CONSTRUCTORS:
    new AssocDependencyBody()

  PROPERTIES:
    ObjectId DependentOnObject { get; }
    bool HasCachedValueOverride { get; }
    bool IsActionEvaluationInProgress { get; }
    bool IsAttachedToObject { get; }
    bool IsRelevantChangeOverride { get; }
    ObjectId OwningAction { get; }
    ObjectId ParentDependency { get; }
    AssocStatus Status { get; }

  METHODS:
    void ClonedOverride(DBObject dbObj, DBObject newObj)
    AssocEvaluationCallback currentEvaluationCallback()
    ErrorStatus DwgInFields(DwgFiler filer)
    ErrorStatus DwgOutFields(DwgFiler filer)
    ErrorStatus DxfInFields(DxfFiler filer)
    ErrorStatus DxfOutFields(DxfFiler filer)
    void ErasedOverride(DBObject dbObj, bool IsErasing)
    void EvaluateOverride()
    bool IsDependentOnTheSameThingAsOverride(AssocDependency otherDependency)
    bool IsEqualToOverride(AssocDependency otherDependency)
    void ModifiedOverride(DBObject dbObj)
    void SetStatus(AssocStatus newStatus, bool notifyOwningAction)
    void UpdateDependentOnObjectOverride()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocDependencyPE
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocDependencyPE
Inherits: RXObject

  CONSTRUCTORS:
    new AssocDependencyPE()

  METHODS:
    bool AllowsDependencies(DBObject dbObj, bool isWriteDependency)

--------------------------------------------------------------------------------

[CLASS] AssocDimDependencyBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocDimDependencyBody
Inherits: AssocDimDependencyBodyBase

  CONSTRUCTORS:
    new AssocDimDependencyBody()

  PROPERTIES:
    double EntityMeasurementOverride { get; }
    string EntityTextOverride { get; set; }
    bool IsEntityAttachmentChangedOverride { get; }

  METHODS:
    static void CreateAndPostToDatabase(ObjectId dimId, ref ObjectId dimDepId, ref ObjectId dimDepBodyId)
    void UpdateDependentOnObjectOverride()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocDimDependencyBodyBase
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocDimDependencyBodyBase
Inherits: AssocDependencyBody

  CONSTRUCTORS:
    new AssocDimDependencyBodyBase()

  PROPERTIES:
    ConstrainedGeometry[] ConstrainedGeoms { get; }
    double EntityMeasurementOverride { get; }
    string EntityTextOverride { get; set; }
    int GetCurrentlyUsedEntityNameFormat { get; }
    double GetMeasuredValue { get; }
    bool IsConstraintActive { get; }
    bool IsEntityAttachmentChangedOverride { get; }
    bool IsReferenceOnly { set; }
    bool IsRelevantChangeOverride { get; }
    FullSubentityPath[] SubentityConstrainedGeoms { get; }
    ObjectId Variable { get; }

  METHODS:
    string ComposeEntityText(int requiredNameFormat)
    ExplicitConstraint Constraint()
    void DeactivateConstraint()
    void DragStatus(DragStatus status)
    ErrorStatus DwgInFields(DwgFiler filer)
    ErrorStatus DwgOutFields(DwgFiler filer)
    ErrorStatus DxfInFields(DxfFiler filer)
    ErrorStatus DxfOutFields(DxfFiler filer)
    void EntityAttachmentPointMoved(SubentityGeometry[] newAttachedGeometries, double measurement)
    void ErasedOverride(DBObject dbObj, bool isErasing)
    void EvaluateOverride()
    static string FormatToCurrentPrecision(string expression, bool isAngular)
    void GetEntityNameAndExpression(ref string name, ref string expression)
    static bool GetEraseDimensionIfDependencyIsErased()
    static ObjectId GetFromEntity(ObjectId entityId)
    static void GetNameAndExpressionFromEntityText(string entityText, bool useMeasurementIfNoText, double measurement, bool isAngular, ref string name, ref string expression)
    SubentityGeometry[] GetSubentityGeoms(Vector3d distanceDirection)
    void GetVariableNameAndExpression(ref string name, ref string expression, string value)
    void ModifiedOverride(DBObject dbObj)
    void ReactivateConstraint()
    void SetEntityNameAndExpression(string name, string expression, string value)
    static bool SetEraseDimensionIfDependencyIsErased(bool yesNo)
    void SetNameAndExpression(string name, string expression)
    void SetVariableNameAndExpression(string name, string expression)
    void SetVariableValueToMeasuredValue()
    void SubErase(bool erasing)
    void UpdateDependentOnObjectOverride()
    string ValidateEntityText(string entityTextToValidate)

--------------------------------------------------------------------------------

[ENUM] AssocDraggingState
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocDraggingState
Inherits: Enum

  Values:
    - NotDraggingAssocDraggingState = 0
    - FirstSampleAssocDraggingState = 1
    - IntermediateSampleAssocDraggingState = 2
    - LastSampleAssocDraggingState = 3

--------------------------------------------------------------------------------

[CLASS] AssocEdgeActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocEdgeActionParam
Inherits: AssocActionParam

  CONSTRUCTORS:
    new AssocEdgeActionParam()

  PROPERTIES:
    CompoundObjectId DependentOnCompoundObject { get; }

  METHODS:
    EdgeRef[] GetEdgeRef()
    void SetEdgeRef(EdgeRef edgeRef, bool isReadDep, bool isWriteDep)
    void SetEdgeRef(EdgeRef edgeRef)

--------------------------------------------------------------------------------

[CLASS] AssocEdgeChamferActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocEdgeChamferActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocEdgeChamferActionBody()

  PROPERTIES:
    double BaseDistance { get; set; }
    double OtherDistance { get; set; }

  METHODS:
    static ObjectId CreateInstance(FullSubentityPath[] chamferEdges, SubentityId baseFace, double baseDistance, double otherDistance, bool enable)
    double GetBaseDistance(out string expression, out string evaluatorId)
    double GetOtherDistance(out string expression, out string evaluatorId)
    void SetBaseDistance(double distance, string expression, string evaluatorId)
    void SetOtherDistance(double distance, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocEdgeFilletActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocEdgeFilletActionBody
Inherits: AssocSurfaceActionBody

  CONSTRUCTORS:
    new AssocEdgeFilletActionBody()

  PROPERTIES:
    double Radius { get; set; }

  METHODS:
    static ObjectId CreateInstance(FullSubentityPath[] filletEdges, double radius, bool enable)
    double GetRadius(out string expression, out string evaluatorId)
    void SetRadius(double radius, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocEvaluationCallback
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocEvaluationCallback
Implements: IDisposable

  CONSTRUCTORS:
    new AssocEvaluationCallback()

  METHODS:
    void BeginActionEvaluation(AssocAction action)
    void BeginActionEvaluationUsingObject(AssocAction pAction, ObjectId objectId, bool objectIsGoingToBeUsed, bool objectIsGoingToBeModified, DBObject substituteObject)
    bool CancelActionEvaluation()
    void Dispose()
    AssocDraggingState DraggingState()
    void EndActionEvaluation(AssocAction action)
    void EndActionEvaluationUsingObject(AssocAction action, ObjectId objectId, DBObject obj)
    AssocEvaluationMode EvaluationMode()
    AssocTransformationType GetTransformationType()
    void SetActionEvaluationErrorStatus(AssocAction action, ErrorStatus errorStatus, ObjectId objectId, DBObject obj, IntPtr errorInfo)
    void SetActionEvaluationErrorStatus(AssocAction action, ErrorStatus errorStatus)

--------------------------------------------------------------------------------

[ENUM] AssocEvaluationMode
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocEvaluationMode
Inherits: Enum

  Values:
    - ModifyObjectsAssocEvaluationMode = 0
    - ModifyActionAssocEvaluationMode = 1

--------------------------------------------------------------------------------

[ENUM] AssocEvaluationPriority
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocEvaluationPriority
Inherits: Enum

  Values:
    - CannotDermineAssocEvaluationPriority = 0
    - CanBeEvaluatedAssocEvaluationPriority = 1000
    - CannotBeEvaluatedAssocEvaluationPriority = -1000

--------------------------------------------------------------------------------

[CLASS] AssocExtendSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocExtendSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocExtendSurfaceActionBody()

  PROPERTIES:
    double Distance { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, EdgeRef[] extendEdges, double distance, EdgeExtensionType extendOption, bool enabled)
    double GetDistance(out string expression, out string evaluatorId)
    void SetDistance(double distance, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocExtrudedSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocExtrudedSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocExtrudedSurfaceActionBody()

  PROPERTIES:
    double Height { get; set; }
    double TaperAngle { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, PathRef inputPath, Vector3d directionVec, SweepOptions sweepOptions, bool enable)
    double GetHeight(out string expression, out string evaluatorId)
    double GetTaperAngle(out string expression, out string evaluatorId)
    void SetHeight(double height, string expression, string evaluatorId)
    void SetTaperAngle(double taperAngle, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocFaceActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocFaceActionParam
Inherits: AssocActionParam

  CONSTRUCTORS:
    new AssocFaceActionParam()

  PROPERTIES:
    CompoundObjectId DependentOnCompoundObject { get; }

  METHODS:
    FaceRef[] GetFaceRef()
    void SetFaceRef(FaceRef faceRef, bool isReadDependency, bool isWriteDependency)
    void SetFaceRef(FaceRef faceRef)

--------------------------------------------------------------------------------

[CLASS] AssocFilletSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocFilletSurfaceActionBody
Inherits: AssocSurfaceActionBody

  CONSTRUCTORS:
    new AssocFilletSurfaceActionBody()

  PROPERTIES:
    double Radius { get; set; }
    FilletTrimMode TrimMode { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, ObjectId inputSurfaceId1, Point3d pickPt1, ObjectId inputSurfaceId2, Point3d pickPt2, double radius, FilletTrimMode trimMode, Vector3d viewDir, bool bEnable)
    void GetFilletHandleData(out Point3d pt, out Vector3d dir, out Point3d cenPt, out Vector3d normal)
    Point3d[] GetHintPoints()
    double GetRadius(out string expression, out string evaluatorId)
    void SetHintPoints(Point3d[] hintPoints, Vector3d viewDir)
    void SetRadius(double radius, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocGeomDependency
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocGeomDependency
Inherits: AssocDependency

  CONSTRUCTORS:
    new AssocGeomDependency()

  PROPERTIES:
    Curve3d[] EdgeSubentityGeometry { get; set; }
    Surface[] FaceSubentityGeometry { get; set; }
    bool IsCachingSubentityGeometry { get; set; }
    AssocPersSubentityId PersistentSubentId { get; }
    SubentityId Subentity { set; }
    uint SubentityType { get; }
    int TransientSubentCount { get; }
    SubentityId[] TransientSubentIds { get; }
    Point3d[] VertexSubentityGeometry { get; set; }

  METHODS:
    void DependentOnObjectMirrored()
    static void RedirectToAnotherSubentity(ObjectId oldObjectId, SubentityId oldSubentId, ObjectId newObjectId, SubentityId newSubentId)

--------------------------------------------------------------------------------

[CLASS] AssocGlobalUtility
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocGlobalUtility

  CONSTRUCTORS:
    new AssocGlobalUtility()

  METHODS:
    static int EvaluationRequestSeverityLevel(AssocStatus status)
    static bool IsDraggingProvidingSubstituteObjects(AssocEvaluationCallback evaluationCallback)
    static bool IsEvaluationRequest(AssocStatus status)
    static bool IsToBeSkipped(AssocStatus status)

--------------------------------------------------------------------------------

[CLASS] AssocLoftedSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocLoftedSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocLoftedSurfaceActionBody()

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, SubentRef[] crossSections, PathRef[] guideCurves, PathRef pathCurve, LoftOptions loftOptions, int[] continuityArray, double[] bulgeArray, bool bEnable)
    double GetBulge(ProfileType type, out string expression, out string evaluatorId)
    double GetBulge(ProfileType type)
    int GetContinuity(ProfileType type, out string expression, out string evaluatorId)
    int GetContinuity(ProfileType type)
    void SetBulge(ProfileType type, double value, string expression, string evaluatorId)
    void SetBulge(ProfileType type, double value)
    void SetContinuity(ProfileType type, int value, string expression, string evaluatorId)
    void SetContinuity(ProfileType type, int value)

--------------------------------------------------------------------------------

[CLASS] AssocManager
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocManager
Inherits: DBObject

  CONSTRUCTORS:
    new AssocManager()

  METHODS:
    static void AddGlobalEvaluationCallback(AssocEvaluationCallback callback, int order)
    static void AuditAssociativeData(Database database, bool traverseWholeDatabase)
    static bool EvaluateTopLevelNetwork(Database database, AssocEvaluationCallback callback, int callbackOrder)
    static void GetGlobalEvaluationCallbacks(ref ArrayList callbacks, ref ArrayList orders)
    static AssocEvaluationCallback GlobalEvaluationMultiCallback()
    static bool HasAssocNetwork(Database database)
    static void Initialize()
    static void RemoveGlobalEvaluationCallback(AssocEvaluationCallback callback)

--------------------------------------------------------------------------------

[CLASS] AssocNetwork
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocNetwork
Inherits: AssocAction

  CONSTRUCTORS:
    new AssocNetwork()

  PROPERTIES:
    ObjectIdCollection GetActions { get; }

  METHODS:
    void AddAction(ObjectId actionId, bool alsoSetAsDatabaseOwner)
    void AddActions(ObjectIdCollection actionIds, bool alsoSetAsDatabaseOwner)
    static ObjectId GetInstanceFromDatabase(Database database, bool createIfDoesNotExist, string dictionaryKey)
    static ObjectId GetInstanceFromObject(ObjectId owningObjectId, bool createIfDoesNotExist, bool addToTopLevelNetwork, string dictionaryKey)
    void OwnedActionStatusChanged(AssocAction ownedAction, AssocStatus previousStatus)
    void RemoveAction(ObjectId actionId, bool alsoEraseIt)
    void RemoveAllActions(bool alsoEraseThem)
    static void RemoveInstanceFromDatabase(Database database, bool alsoEraseIt, string dictionaryKey)
    static void RemoveInstanceFromObject(ObjectId owningObjectId, bool alsoEraseIt, string dictionaryKey)

--------------------------------------------------------------------------------

[CLASS] AssocNetworkSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocNetworkSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocNetworkSurfaceActionBody()

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, PathRef[] crossSections, PathRef[] guideCurves, int[] continuityArray, double[] bulgeArray, bool bEnable)
    double GetBulge(ProfileType type, out string expression, out string evaluatorId)
    double GetBulge(ProfileType type)
    int GetContinuity(ProfileType type, out string expression, out string evaluatorId)
    int GetContinuity(ProfileType type)
    void SetBulge(ProfileType type, double value, string expression, string evaluatorId)
    void SetBulge(ProfileType type, double value)
    void SetContinuity(ProfileType type, int value, string expression, string evaluatorId)
    void SetContinuity(ProfileType type, int value)

--------------------------------------------------------------------------------

[CLASS] AssocObjectTransaction
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocObjectTransaction
Implements: IDisposable

  CONSTRUCTORS:
    new AssocObjectTransaction(AssocDependencyBody dependencyBodyBeingEvaluated)
    new AssocObjectTransaction(AssocDependency dependencyBeingEvaluated)
    new AssocObjectTransaction(AssocActionBody actionBodyBeingEvaluated)
    new AssocObjectTransaction(AssocAction actionBeingEvaluated)

  METHODS:
    void Dispose()
    DBObject GetDBObject(ObjectId objectId, OpenMode openMode, bool openErased, bool openOnLockedLayer)
    bool IsSubstituteObject(DBObject dbObject)

--------------------------------------------------------------------------------

[CLASS] AssocOffsetSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocOffsetSurfaceActionBody
Inherits: AssocSurfaceActionBody

  CONSTRUCTORS:
    new AssocOffsetSurfaceActionBody()

  PROPERTIES:
    double Distance { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, ObjectId inputSurfaceId, double distance, bool bEnable)
    double GetDistance(out string expression, out string evaluatorId)
    void SetDistance(double distance, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocParamBasedActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocParamBasedActionBody
Inherits: AssocActionBody

--------------------------------------------------------------------------------

[CLASS] AssocPatchSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocPatchSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocPatchSurfaceActionBody()

  PROPERTIES:
    double Bulge { get; set; }
    int Continuity { get; set; }
    Point3d ContinuityGripPoint { get; }

  METHODS:
    static ObjectId CreateInstance(ObjectId patchSurfaceId, EdgeRef[] profileCurves, EdgeRef[] constraintCurves, VertexRef[] constraintPoints, bool enable)
    static ObjectId CreateInstance(ObjectId patchSurfaceId, EdgeRef[] edgeCurves, EdgeRef[] constraintCurves, VertexRef[] constraintPoints, int nContinuity, double dBulge, bool bEnabled)
    double GetBulge(out string expression, out string evaluatorId)
    int GetContinuity(out string expression, out string evaluatorId)
    void SetBulge(double value, string expression, string evaluatorId)
    void SetConstraintCurves(EdgeRef[] constraintCurves)
    void SetConstraintPoints(VertexRef[] constraintPoints)
    void SetContinuity(int value, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocPathActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocPathActionParam
Inherits: AssocCompoundActionParam

  CONSTRUCTORS:
    new AssocPathActionParam()

  METHODS:
    EdgeRef[][] GetEdgeRefArray()
    void SetEdgeRefArray(EdgeRef[] edgeRef)
    void UpdateEdgeRef(int index, EdgeRef edge)

--------------------------------------------------------------------------------

[CLASS] AssocPathBasedSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocPathBasedSurfaceActionBody
Inherits: AssocSurfaceActionBody

  CONSTRUCTORS:
    new AssocPathBasedSurfaceActionBody()

  PROPERTIES:
    ObjectIdCollection InputPathParams { get; }
    ObjectIdCollection InputVertexParams { get; }

  METHODS:
    void GetInputPaths(out EdgeRef[][][] paths)
    VertexRef[] GetInputPoints()
    void MakeInputPathsDrawOnTopOfResultingSurface()
    void RemoveAllPathParams()
    void RemoveAllVertexParams()
    void SetInputPaths(PathRef[] paths)
    void SetInputPaths(EdgeRef[][] edges)
    void SetInputPoints(VertexRef[] points)
    void UpdateInputPath(int index, PathRef pathRef)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocPersSubentityId
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocPersSubentityId
Inherits: RXObject

  CONSTRUCTORS:
    new AssocPersSubentityId()

  PROPERTIES:
    bool IsNull { get; }

  METHODS:
    void Audit(AuditInfo auditInfo)
    static void CreateObjectAndDwgInFields(Database database, DwgFiler filer, ref AssocPersSubentityId createdPersSubentId)
    static void CreateObjectAndDxfInFields(DxfFiler filer, ref AssocPersSubentityId createdPersSubentId)
    void DwgInFields(DwgFiler filer)
    void DwgOutFields(DwgFiler filer)
    void DxfInFields(DxfFiler filer)
    void DxfOutFields(DxfFiler filer)
    SubentityId[] GetTransientSubentIds(Entity entity)
    bool IsEqualTo(Entity entity, AssocPersSubentityId other)
    void Mirror(Entity mirroredEntity)
    SubentityType SubentType(Entity entity)
    int TransientSubentCount(Entity entity)

--------------------------------------------------------------------------------

[CLASS] AssocPersSubentityIdPE
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocPersSubentityIdPE
Inherits: RXObject

  CONSTRUCTORS:
    new AssocPersSubentityIdPE()

  METHODS:
    AssocPersSubentityId CreateNewPersSubent(Entity entity, CompoundObjectId compId, SubentityId subentId)
    SubentityId[] GetAllSubentities(Entity entity, SubentityType subentType)
    Curve3d GetEdgeSubentityGeometry(Entity entity, SubentityId edgeSubentId)
    void GetEdgeVertexSubentities(Entity entity, SubentityId edgeSubentId, ref SubentityId startVertexSubentId, ref SubentityId endVertexSubentId, ref SubentityId[] otherVertexSubentIds)
    Surface GetFaceSubentityGeometry(Entity entity, SubentityId faceSubentId)
    int GetRigidSetState(Entity entity)
    Matrix3d GetRigidSetTransform(Entity entity)
    void GetSplineEdgeVertexSubentities(Entity entity, SubentityId edgeSubentId, ref SubentityId startVertexSubentId, ref SubentityId endVertexSubentId, ref SubentityId[] controlPointSubentIds, ref SubentityId[] fitPointSubentIds)
    Matrix3d GetSubentGeometryXform(Entity entity, ObjectId[] fullSubentPath)
    SubentityId[] GetTransientSubentityIds(Entity entity, AssocPersSubentityId perSubentId)
    Point3d GetVertexSubentityGeometry(Entity entity, SubentityId vertexSubentId)
    void MirrorPersSubent(Entity mirroredEntity, AssocPersSubentityId persSubentIdToMirror)
    void SetEdgeSubentityGeometry(Entity entity, SubentityId edgeSubentId, Curve3d newEdgeCurve)
    void SetFaceSubentityGeometry(Entity entity, SubentityId faceSubentId, Surface newFaceSurface)
    void SetRigidSetTransform(Entity entity, Matrix3d trans)
    void SetVertexSubentityGeometry(Entity entity, SubentityId vertexSubentId, Point3d newVertexPosition)

--------------------------------------------------------------------------------

[CLASS] AssocPlaneSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocPlaneSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocPlaneSurfaceActionBody()

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, PathRef inputPath, bool enable)

--------------------------------------------------------------------------------

[CLASS] AssocRevolvedSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocRevolvedSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocRevolvedSurfaceActionBody()

  PROPERTIES:
    double RevolveAngle { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, PathRef revolvePath, PathRef axisPath, double angle, double startAngle, RevolveOptions revolveOptions, bool flipAxisDirection, bool enable)
    double GetRevolveAngle(out string expression, out string evaluatorId)
    void SetRevolveAngle(double value, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[CLASS] AssocSimplePersSubentId
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocSimplePersSubentId
Inherits: AssocPersSubentityId

  CONSTRUCTORS:
    new AssocSimplePersSubentId(SubentityId SubentityId)

  PROPERTIES:
    bool IsNull { get; }

  METHODS:
    void Audit(AuditInfo auditInfo)
    void DwgInFields(DwgFiler filer)
    void DwgOutFields(DwgFiler filer)
    void DxfInFields(DxfFiler filer)
    void DxfOutFields(DxfFiler filer)
    bool IsEqualTo(Entity entity, AssocPersSubentityId other)
    SubentityId SubentId(Entity entity)
    SubentityType SubentType(Entity entity)
    int TransientSubentCount(Entity entity)

--------------------------------------------------------------------------------

[CLASS] AssocSingleEdgePersSubentId
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocSingleEdgePersSubentId
Inherits: AssocPersSubentityId

  CONSTRUCTORS:
    new AssocSingleEdgePersSubentId()

  PROPERTIES:
    bool IsNull { get; }

  METHODS:
    bool IsEqualTo(Entity entity, AssocPersSubentityId other)
    SubentityType SubentType(Entity entity)
    int TransientSubentCount(Entity entity)

--------------------------------------------------------------------------------

[ENUM] AssocStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocStatus
Inherits: Enum

  Values:
    - IsUpToDateAssocStatus = 0
    - ChangedDirectlyAssocStatus = 1
    - ChangedTransitivelyAssocStatus = 2
    - ChangedNoDifferenceAssocStatus = 3
    - FailedToEvaluateAssocStatus = 4
    - ErasedAssocStatus = 5
    - SuppressedAssocStatus = 6

--------------------------------------------------------------------------------

[CLASS] AssocSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocSurfaceActionBody
Inherits: AssocParamBasedActionBody

  CONSTRUCTORS:
    new AssocSurfaceActionBody()

  PROPERTIES:
    bool IsSemiAssociative { get; }
    ObjectId ResultingSurface { get; set; }

  METHODS:
    void EvaluateOverride()
    static ObjectIdCollection FindActionsThatAffectedTopologicalSubentity(Entity entity, SubentityId subEntityId)
    ObjectId ResultingSurfaceDep(bool createIfDoesNotExist, bool isWriteOnlyDependency)
    ObjectId ResultingSurfaceDep(bool createIfDoesNotExist)
    void SetResultingSurface(ObjectId surfaceId, bool isWriteOnlyDependency)

--------------------------------------------------------------------------------

[CLASS] AssocSweptSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocSweptSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocSweptSurfaceActionBody()

  PROPERTIES:
    double AlignAngle { get; set; }
    double ScaleFactor { get; set; }
    double TwistAngle { get; set; }

  METHODS:
    static ObjectId CreateInstance(ObjectId resultingSurfaceId, PathRef sweptPath, PathRef path, SweepOptions sweptOptions, bool enable)
    double GetAlignAngle(out string expression, out string evaluatorId)
    double GetScaleFactor(out string expression, out string evaluatorId)
    double GetTwistAngle(out string expression, out string evaluatorId)
    void SetAlignAngle(double value, string expression, string evaluatorId)
    void SetScaleFactor(double value, string expression, string evaluatorId)
    void SetTwistAngle(double value, string expression, string evaluatorId)

--------------------------------------------------------------------------------

[ENUM] AssocTransformationType
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocTransformationType
Inherits: Enum

  Values:
    - NotSpecified = 0
    - Stretch = 1
    - Rotate = 2
    - Move = 3

--------------------------------------------------------------------------------

[CLASS] AssocTrimSurfaceActionBody
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocTrimSurfaceActionBody
Inherits: AssocPathBasedSurfaceActionBody

  CONSTRUCTORS:
    new AssocTrimSurfaceActionBody()

  PROPERTIES:
    double TrimmedArea { get; }

  METHODS:
    static ObjectIdCollection CreateInstance(ObjectId resultingSurfaceId, SurfaceTrimInfo[] trimInfo, bool autoExtend, bool enable)
    void MakeTrimPermanent()
    void SetTrimInfo(SurfaceTrimInfo[] trimInfoArray, bool autoExtend)
    void Untrim()

--------------------------------------------------------------------------------

[CLASS] AssocValueDependency
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocValueDependency
Inherits: AssocDependency

  CONSTRUCTORS:
    new AssocValueDependency()

  PROPERTIES:
    ResultBuffer DependentOnObjectValue { get; set; }
    string ValueName { get; set; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocValueProviderPE
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocValueProviderPE
Inherits: RXObject

  CONSTRUCTORS:
    new AssocValueProviderPE()

  METHODS:
    bool CanGetValue(DBObject dbObj, string valueName)
    bool CanSetValue(DBObject dbObj, string valueName)
    ResultBuffer GetValue(DBObject dbObj, string valueName)
    void SetValue(DBObject dbObj, string valueName, ResultBuffer newValue)

--------------------------------------------------------------------------------

[CLASS] AssocVariable
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocVariable
Inherits: AssocAction

  CONSTRUCTORS:
    new AssocVariable()

  PROPERTIES:
    string Description { get; set; }
    string EvaluatorId { get; set; }
    string Expression { get; }
    string Name { get; }
    ResultBuffer Value { get; set; }

  METHODS:
    static void AddGlobalCallback(AssocVariableCallback callback)
    string EvaluateExpression(ref ObjectIdCollection objectIds, ref ResultBuffer[] objectValues, ref ResultBuffer evaluatedExpressionValue)
    string EvaluateExpression(ref ResultBuffer evaluatedExpressionValue)
    ObjectId FindObjectByName(string objectName, RXClass pObjectClass)
    static AssocVariableCallback globalCallback()
    static void RemoveGlobalCallback(AssocVariableCallback callback)
    void SetExpression(string newExpression, string evaluatorId, bool checkForCyclicalDependencies, bool updateDependenciesOnReferencedSymbols, ref string errorMessage, bool silentMode)
    void SetName(string newName, bool updateReferencingExpressions)
    void ValidateNameAndExpression(string nameToValidate, string expressionToValidate, ref string errorMessage)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AssocVariableCallback
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocVariableCallback
Implements: IDisposable

  CONSTRUCTORS:
    new AssocVariableCallback()

  METHODS:
    bool CanBeErased(AssocVariable variable)
    void Dispose()
    string ValidateNameAndExpression(AssocVariable variable, string nameToValidate, string expressionToValidate)

--------------------------------------------------------------------------------

[CLASS] AssocVertexActionParam
Full Name: Autodesk.AutoCAD.DatabaseServices.AssocVertexActionParam
Inherits: AssocActionParam

  CONSTRUCTORS:
    new AssocVertexActionParam()

  PROPERTIES:
    CompoundObjectId DependentOnCompoundObject { get; }

  METHODS:
    VertexRef GetVertexRef()
    void SetVertexRef(VertexRef pointRef, bool isReadDep, bool isWriteDep)

--------------------------------------------------------------------------------

[ENUM] AttachmentPoint
Full Name: Autodesk.AutoCAD.DatabaseServices.AttachmentPoint
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
    - BaseLeft = 10
    - BaseCenter = 11
    - BaseRight = 12
    - BaseAlign = 13
    - BottomAlign = 14
    - MiddleAlign = 15
    - TopAlign = 16
    - BaseFit = 17
    - BottomFit = 18
    - MiddleFit = 19
    - TopFit = 20
    - BaseMid = 21
    - BottomMid = 22
    - MiddleMid = 23
    - TopMid = 24

--------------------------------------------------------------------------------

[CLASS] AttributeCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.AttributeCollection
Implements: ICollection, IEnumerable, ISubObject

  PROPERTIES:
    int Count { get; }
    ObjectId Item { get; }

  METHODS:
    ObjectId AppendAttribute(AttributeReference attributeToAddToBlockReference)
    void CopyTo(ObjectId[] array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] AttributeDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.AttributeDefinition
Inherits: DBText

  CONSTRUCTORS:
    new AttributeDefinition(Point3d position, string value, string tag, string prompt, ObjectId style)
    new AttributeDefinition()

  PROPERTIES:
    bool Constant { get; set; }
    int FieldLength { get; set; }
    bool Invisible { get; set; }
    bool IsMTextAttributeDefinition { get; set; }
    bool LockPositionInBlock { get; set; }
    MText MTextAttributeDefinition { get; set; }
    bool Preset { get; set; }
    string Prompt { get; set; }
    string Tag { get; set; }
    bool Verifiable { get; set; }

  METHODS:
    void UpdateMTextAttributeDefinition()

--------------------------------------------------------------------------------

[CLASS] AttributeReference
Full Name: Autodesk.AutoCAD.DatabaseServices.AttributeReference
Inherits: DBText

  CONSTRUCTORS:
    new AttributeReference(Point3d position, string value, string tag, ObjectId style)
    new AttributeReference()

  PROPERTIES:
    int FieldLength { get; set; }
    bool Invisible { get; set; }
    bool IsConstant { get; }
    bool IsMTextAttribute { get; set; }
    bool IsPreset { get; }
    bool IsVerifiable { get; }
    bool LockPositionInBlock { get; set; }
    MText MTextAttribute { get; set; }
    string Tag { get; set; }

  METHODS:
    void SetAttributeFromBlock(AttributeDefinition definition, Matrix3d blockTransform)
    void SetAttributeFromBlock(Matrix3d blockTransform)
    void UpdateMTextAttribute()

--------------------------------------------------------------------------------

[CLASS] AuditInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.AuditInfo
Inherits: DisposableWrapper

  PROPERTIES:
    AuditPass AuditPass { get; }
    bool FixErrors { get; }
    int NumEntities { get; }
    int NumErrors { get; }
    int NumFixes { get; }

  METHODS:
    void ErrorsFixed(int count)
    void ErrorsFound(int count)
    void IncNumEntities()
    void PrintError(DBObject value, string value2, string validation, string defaultValue)
    void PrintError(string name, string value, string validation, string defaultValue)
    void PrintNumEntities(string msg)
    void RequestRegen()
    void ResetNumEntities()

--------------------------------------------------------------------------------

[ENUM] AuditPass
Full Name: Autodesk.AutoCAD.DatabaseServices.AuditPass
Inherits: Enum

  Values:
    - Pass1 = 1
    - Pass2 = 2

--------------------------------------------------------------------------------

[CLASS] AutoConstrainEvaluationCallback
Full Name: Autodesk.AutoCAD.DatabaseServices.AutoConstrainEvaluationCallback
Implements: IDisposable

  CONSTRUCTORS:
    new AutoConstrainEvaluationCallback()

  PROPERTIES:
    bool IsEvaluationCancelled { get; }

  METHODS:
    void Dispose()
    ConstraintType[] GetAutoConstrainPriority()
    int GetConstraintPriorityOverride(ConstraintType type, ConstrainedGeometry[] geometries)
    int GetTotalConstraints(ref GeometricalConstraint[] constraints)

--------------------------------------------------------------------------------

