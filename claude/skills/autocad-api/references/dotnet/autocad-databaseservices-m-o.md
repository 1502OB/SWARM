# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[ENUM] MaintenanceReleaseVersion
Full Name: Autodesk.AutoCAD.DatabaseServices.MaintenanceReleaseVersion
Inherits: Enum

  Values:
    - Release0 = 0
    - ReleaseExtendedNewest = 1
    - ReleaseExtendedNewest = 1
    - Release2 = 2
    - Release3 = 3
    - Release4 = 4
    - Release5 = 5
    - Release6 = 6
    - Release7 = 7
    - Release8 = 8
    - Release9 = 9
    - Release10 = 10
    - Release11 = 11
    - Release12 = 12
    - Release13 = 13
    - Release14 = 14
    - Release15 = 15
    - Release16 = 16
    - Release17 = 17
    - Release18 = 18
    - Release19 = 19
    - Release20 = 20
    - Release21 = 21
    - Release22 = 22
    - Release23 = 23
    - Release24 = 24
    - Release25 = 25
    - Release26 = 26
    - Release27 = 27
    - Release28 = 28
    - Release29 = 29
    - Release30 = 30
    - Release31 = 31
    - Release32 = 32
    - Release33 = 33
    - Release34 = 34
    - Release35 = 35
    - Release36 = 36
    - Release37 = 37
    - Release38 = 38
    - Release39 = 39
    - Release40 = 40
    - Release41 = 41
    - Release42 = 42
    - Release43 = 43
    - Release44 = 44
    - Release45 = 45
    - Release46 = 46
    - Release47 = 47
    - Release48 = 48
    - Release49 = 49
    - Release50 = 50
    - Release51 = 51
    - Release52 = 52
    - Release53 = 53
    - Release54 = 54
    - Release55 = 55
    - Release56 = 56
    - Release57 = 57
    - Release58 = 58
    - Release59 = 59
    - Release60 = 60
    - Release61 = 61
    - Release62 = 62
    - Release63 = 63
    - Release64 = 64
    - Release65 = 65
    - Release66 = 66
    - Release67 = 67
    - Release68 = 68
    - Release69 = 69
    - Release70 = 70
    - Release71 = 71
    - Release72 = 72
    - Release73 = 73
    - Release74 = 74
    - Release75 = 75
    - Release76 = 76
    - Release77 = 77
    - Release78 = 78
    - Release79 = 79
    - Release80 = 80
    - Release81 = 81
    - Release82 = 82
    - Release83 = 83
    - Release84 = 84
    - Release85 = 85
    - Release86 = 86
    - Release87 = 87
    - Release88 = 88
    - Release89 = 89
    - Release90 = 90
    - Release91 = 91
    - Release92 = 92
    - Release93 = 93
    - Release94 = 94
    - Release95 = 95
    - Release96 = 96
    - Release97 = 97
    - Release98 = 98
    - Release99 = 99
    - Release100 = 100
    - Release101 = 101
    - Release102 = 102
    - Release103 = 103
    - Release104 = 104
    - Release105 = 105
    - Release106 = 106
    - Release107 = 107
    - Release108 = 108
    - Release109 = 109
    - Release110 = 110
    - Release111 = 111
    - Release112 = 112
    - Release113 = 113
    - Release114 = 114
    - Release115 = 115
    - Release116 = 116
    - Release117 = 117
    - Release118 = 118
    - Release119 = 119
    - Release120 = 120
    - Release121 = 121
    - Release122 = 122
    - Release123 = 123
    - Release124 = 124
    - Release125 = 125
    - Release125 = 125
    - Release126 = 126
    - Release127 = 127
    - Release128 = 128
    - Release129 = 129
    - Release130 = 130
    - Release131 = 131
    - Release132 = 132
    - Release133 = 133
    - Release134 = 134
    - Release135 = 135
    - Release136 = 136
    - Release137 = 137
    - Release138 = 138
    - Release139 = 139
    - Release140 = 140
    - Release141 = 141
    - Release142 = 142
    - Release143 = 143
    - Release144 = 144
    - Release145 = 145
    - Release146 = 146
    - Release147 = 147
    - Release148 = 148
    - Release149 = 149
    - Release150 = 150
    - Release151 = 151
    - Release152 = 152
    - Release153 = 153
    - Release154 = 154
    - Release155 = 155
    - Release156 = 156
    - Release157 = 157
    - Release158 = 158
    - Release159 = 159
    - Release160 = 160
    - Release2010Newest = 226
    - Release2010Max = 255
    - ReleaseNewest = 427
    - ReleaseNewest = 427
    - ReleaseExtendedCurrent = 477
    - ReleaseUnknown = 2147483646
    - ReleaseMax = 2147483647

--------------------------------------------------------------------------------

[CLASS] MatchProperties
Full Name: Autodesk.AutoCAD.DatabaseServices.MatchProperties
Inherits: RXObject

  METHODS:
    void CopyProperties(Entity sourceEntity, Entity targetEntity, int flag)

--------------------------------------------------------------------------------

[CLASS] Material
Full Name: Autodesk.AutoCAD.DatabaseServices.Material
Inherits: DBObject

  CONSTRUCTORS:
    new Material()

  PROPERTIES:
    MaterialColor Ambient { get; set; }
    bool Anonymous { get; set; }
    MaterialMap Bump { get; set; }
    ChannelFlags ChannelFlags { get; set; }
    double ColorBleedScale { get; set; }
    string Description { get; set; }
    MaterialDiffuseComponent Diffuse { get; set; }
    FinalGatherMode FinalGather { get; set; }
    GlobalIlluminationMode GlobalIllumination { get; set; }
    IlluminationModel IlluminationModel { get; set; }
    double IndirectBumpScale { get; set; }
    double Luminance { get; set; }
    LuminanceMode LuminanceMode { get; set; }
    Mode Mode { get; set; }
    string Name { get; set; }
    MaterialNormalMapComponent NormalMap { get; set; }
    MaterialOpacityComponent Opacity { get; set; }
    double ReflectanceScale { get; set; }
    MaterialMap Reflection { get; set; }
    double Reflectivity { get; set; }
    MaterialRefractionComponent Refraction { get; set; }
    double SelfIllumination { get; set; }
    MaterialSpecularComponent Specular { get; set; }
    double Translucence { get; set; }
    double TransmittanceScale { get; set; }
    bool TwoSided { get; set; }

--------------------------------------------------------------------------------

[ENUM] MeasurementValue
Full Name: Autodesk.AutoCAD.DatabaseServices.MeasurementValue
Inherits: Enum

  Values:
    - English = 0
    - Metric = 1

--------------------------------------------------------------------------------

[ENUM] MergeCellStyleOption
Full Name: Autodesk.AutoCAD.DatabaseServices.MergeCellStyleOption
Inherits: Enum

  Values:
    - None = 0
    - CopyDuplicates = 1
    - OverwriteDuplicates = 2
    - ConvertDuplicatesToOverrrides = 4
    - IgnoreNewStyles = 8

--------------------------------------------------------------------------------

[STRUCT] MeshDataCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.MeshDataCollection

  CONSTRUCTORS:
    new MeshDataCollection(Point3dCollection pts, Int32Collection faces, EntityColorCollection colors, ObjectIdCollection materialIds)

  PROPERTIES:
    EntityColorCollection ColorArray { get; set; }
    Int32Collection FaceArray { get; set; }
    ObjectIdCollection MaterialIdArray { get; set; }
    Point3dCollection VertexArray { get; set; }

--------------------------------------------------------------------------------

[STRUCT] MeshFaceterData
Full Name: Autodesk.AutoCAD.DatabaseServices.MeshFaceterData

  CONSTRUCTORS:
    new MeshFaceterData(double surface, double normal, double gRatio, double eLength, UInt16 numGrid, UInt16 uGrid, UInt16 vGrid, short mType)

  PROPERTIES:
    double FaceterDevNormal { get; set; }
    double FaceterDevSurface { get; set; }
    double FaceterGridRatio { get; set; }
    double FaceterMaxEdgeLength { get; set; }
    UInt16 FaceterMaxGrid { get; set; }
    short FaceterMeshType { get; set; }
    UInt16 FaceterMinUGrid { get; set; }
    UInt16 FaceterMinVGrid { get; set; }

--------------------------------------------------------------------------------

[STRUCT] MeshPointMap
Full Name: Autodesk.AutoCAD.DatabaseServices.MeshPointMap

  CONSTRUCTORS:
    new MeshPointMap(Point2d sourcePt, Point2d destPt)

  PROPERTIES:
    Point2d DestPoint { get; set; }
    Point2d SourcePoint { get; set; }

--------------------------------------------------------------------------------

[STRUCT] MeshPointMaps
Full Name: Autodesk.AutoCAD.DatabaseServices.MeshPointMaps

  CONSTRUCTORS:
    new MeshPointMaps(Point2dCollection sourcePts, Point2dCollection destPts)

  PROPERTIES:
    Point2dCollection DestPonints { get; set; }
    Point2dCollection SourcePonints { get; set; }

--------------------------------------------------------------------------------

[CLASS] MidPointConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.MidPointConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new MidPointConstraint()

--------------------------------------------------------------------------------

[CLASS] MInsertBlock
Full Name: Autodesk.AutoCAD.DatabaseServices.MInsertBlock
Inherits: BlockReference

  CONSTRUCTORS:
    new MInsertBlock(Point3d position, ObjectId blockTableRecord, short columns, short rows, double colSpacing, double rowSpacing)
    new MInsertBlock()

  PROPERTIES:
    short Columns { get; set; }
    double ColumnSpacing { get; set; }
    short Rows { get; set; }
    double RowSpacing { get; set; }

--------------------------------------------------------------------------------

[CLASS] MLeader
Full Name: Autodesk.AutoCAD.DatabaseServices.MLeader
Inherits: Entity

  CONSTRUCTORS:
    new MLeader()

  PROPERTIES:
    double ArrowSize { get; set; }
    ObjectId ArrowSymbolId { get; set; }
    Color BlockColor { get; set; }
    BlockConnectionType BlockConnectionType { get; set; }
    ObjectId BlockContentId { get; set; }
    Point3d BlockPosition { get; set; }
    double BlockRotation { get; set; }
    Scale3d BlockScale { get; set; }
    ContentType ContentType { get; set; }
    double DoglegLength { get; set; }
    bool EnableAnnotationScale { get; set; }
    bool EnableDogleg { get; set; }
    bool EnableFrameText { get; set; }
    bool EnableLanding { get; set; }
    bool ExtendLeaderToText { get; set; }
    double LandingGap { get; set; }
    int LeaderCount { get; }
    Color LeaderLineColor { get; set; }
    int LeaderLineCount { get; }
    LeaderType LeaderLineType { get; set; }
    ObjectId LeaderLineTypeId { get; set; }
    LineWeight LeaderLineWeight { get; set; }
    ObjectId MLeaderStyle { get; set; }
    MText MText { get; set; }
    Vector3d Normal { get; }
    double Scale { get; set; }
    TextAlignmentType TextAlignmentType { get; set; }
    TextAngleType TextAngleType { get; set; }
    TextAttachmentDirection TextAttachmentDirection { get; set; }
    TextAttachmentType TextAttachmentType { get; set; }
    Color TextColor { get; set; }
    double TextHeight { get; set; }
    Point3d TextLocation { get; set; }
    ObjectId TextStyleId { get; set; }
    Point3d ToleranceLocation { get; set; }

  METHODS:
    void AddFirstVertex(int leaderLineIndex, Point3d point)
    void AddLastVertex(int leaderLineIndex, Point3d point)
    int AddLeader()
    int AddLeaderLine(Point3d point)
    int AddLeaderLine(int leaderIndex)
    Point3d ConnectionPoint(Vector3d direction, TextAttachmentDirection textAttachmentDirection)
    Point3d ConnectionPoint(Vector3d direction)
    double GetArrowSize(int leaderLineIndex)
    ObjectId GetArrowSymbolId(int leaderLineIndex)
    AttributeReference GetBlockAttribute(ObjectId attDefId)
    Extents3d GetContentGeomExtents()
    IntPtr getContextDataManager()
    Vector3d GetDogleg(int leaderIndex)
    double GetDoglegLength(int leaderIndex)
    Point3d GetFirstVertex(int leaderLineIndex)
    Point3d GetLastVertex(int leaderLineIndex)
    int GetLeaderIndex(int leaderLineIndex)
    ArrayList GetLeaderIndexes()
    Color GetLeaderLineColor(int leaderLineIndex)
    ArrayList GetLeaderLineIndexes(int leaderIndex)
    LeaderType GetLeaderLineType(int leaderLineIndex)
    ObjectId GetLeaderLineTypeId(int leaderLineIndex)
    LineWeight GetLeaderLineWeight(int leaderLineIndex)
    void getOverridedMLeaderStyle(MLeaderStyle mleaderStyle)
    Plane GetPlane()
    TextAttachmentType GetTextAttachmentType(LeaderDirectionType leaderDirection)
    Point3d GetVertex(int leaderLineIndex, int index)
    bool HasContent()
    void MoveMLeader(Vector3d vec, MoveType type)
    void PostMLeaderToDb(Database db)
    void recomputeBreakPoints()
    void RemoveFirstVertex(int leaderLineIndex)
    void RemoveLastVertex(int leaderLineIndex)
    void RemoveLeader(int index)
    void RemoveLeaderLine(int leaderLineIndex)
    void SetArrowSize(int leaderLineIndex, double arrowSize)
    void SetArrowSymbolId(int leaderLineIndex, ObjectId id)
    void SetBlockAttribute(ObjectId attDefId, AttributeReference attributeValue)
    void SetContextDataManager(IntPtr contextDataManager)
    void SetDogleg(int leaderIndex, Vector3d vector)
    void SetDoglegLength(int leaderIndex, double doglegLength)
    void SetFirstVertex(int leaderLineIndex, Point3d point)
    void SetLastVertex(int leaderLineIndex, Point3d point)
    void SetLeaderLineColor(int leaderLineIndex, Color leaderLineTypeId)
    void SetLeaderLineType(int leaderLineIndex, LeaderType leaderLineType)
    void SetLeaderLineTypeId(int leaderLineIndex, ObjectId leaderLineTypeId)
    void SetLeaderLineWeight(int leaderLineIndex, LineWeight leaderLineWeight)
    void SetPlane(Plane value)
    void SetTextAttachmentType(TextAttachmentType textAttachmentType, LeaderDirectionType leaderDirection)
    void SetVertex(int leaderLineIndex, int index, Point3d point)
    int VerticesCount(int leaderLineIndex)

--------------------------------------------------------------------------------

[CLASS] MLeaderStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.MLeaderStyle
Inherits: DBObject

  CONSTRUCTORS:
    new MLeaderStyle(MLeaderStyle mleaderStyle)
    new MLeaderStyle()

  PROPERTIES:
    double ArrowSize { get; set; }
    ObjectId ArrowSymbolId { get; set; }
    Color BlockColor { get; set; }
    BlockConnectionType BlockConnectionType { get; set; }
    ObjectId BlockId { get; set; }
    double BlockRotation { get; set; }
    Scale3d BlockScale { get; set; }
    double BreakSize { get; set; }
    ContentType ContentType { get; set; }
    MText DefaultMText { get; set; }
    double DoglegLength { get; set; }
    DrawLeaderOrderType DrawLeaderOrderType { get; set; }
    DrawMLeaderOrderType DrawMLeaderOrderType { get; set; }
    bool EnableBlockRotation { get; set; }
    bool EnableBlockScale { get; set; }
    bool EnableDogleg { get; set; }
    bool EnableFrameText { get; set; }
    bool EnableLanding { get; set; }
    bool ExtendLeaderToText { get; set; }
    AngleConstraint FirstSegmentAngleConstraint { get; set; }
    double LandingGap { get; set; }
    Color LeaderLineColor { get; set; }
    LeaderType LeaderLineType { get; set; }
    ObjectId LeaderLineTypeId { get; set; }
    LineWeight LeaderLineWeight { get; set; }
    int MaxLeaderSegmentsPoints { get; set; }
    string Name { get; set; }
    double Scale { get; set; }
    AngleConstraint SecondSegmentAngleConstraint { get; set; }
    bool TextAlignAlwaysLeft { get; set; }
    TextAlignmentType TextAlignmentType { get; set; }
    TextAngleType TextAngleType { get; set; }
    TextAttachmentDirection TextAttachmentDirection { get; set; }
    TextAttachmentType TextAttachmentType { get; set; }
    Color TextColor { get; set; }
    double TextHeight { get; set; }
    ObjectId TextStyleId { get; set; }

  METHODS:
    TextAttachmentType GetTextAttachmentType(LeaderDirectionType leaderDirection)
    bool OverwritePropChanged()
    ObjectId PostMLeaderStyleToDb(Database db, string styleName)
    void SetTextAttachmentType(TextAttachmentType textAttachmentType, LeaderDirectionType leaderDirection)

--------------------------------------------------------------------------------

[CLASS] Mline
Full Name: Autodesk.AutoCAD.DatabaseServices.Mline
Inherits: Entity

  CONSTRUCTORS:
    new Mline()

  PROPERTIES:
    bool IsClosed { get; set; }
    MlineJustification Justification { get; set; }
    Vector3d Normal { get; set; }
    int NumberOfVertices { get; }
    double Scale { get; set; }
    ObjectId Style { get; set; }
    bool SupressEndCaps { get; set; }
    bool SupressStartCaps { get; set; }

  METHODS:
    void AppendSegment(Point3d newVertex)
    int Element(Point3d pt)
    Point3d GetClosestPointTo(Point3d givenPoint, Vector3d normal, bool extend, bool excludeCaps)
    Point3d GetClosestPointTo(Point3d givenPoint, bool extend, bool excludeCaps)
    void MoveVertexAt(int index, Point3d newPosition)
    void RemoveLastSegment(Point3d lastVertex)
    Point3d VertexAt(int index)

--------------------------------------------------------------------------------

[ENUM] MlineJustification
Full Name: Autodesk.AutoCAD.DatabaseServices.MlineJustification
Inherits: Enum

  Values:
    - Top = 0
    - Zero = 1
    - Bottom = 2

--------------------------------------------------------------------------------

[CLASS] MlineStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.MlineStyle
Inherits: DBObject

  CONSTRUCTORS:
    new MlineStyle()

  PROPERTIES:
    string Description { get; set; }
    MlineStyleElementCollection Elements { get; }
    double EndAngle { get; set; }
    bool EndInnerArcs { get; set; }
    bool EndRoundCap { get; set; }
    bool EndSquareCap { get; set; }
    Color FillColor { get; set; }
    bool Filled { get; set; }
    string Name { get; set; }
    bool ShowMiters { get; set; }
    double StartAngle { get; set; }
    bool StartInnerArcs { get; set; }
    bool StartRoundCap { get; set; }
    bool StartSquareCap { get; set; }

  METHODS:
    void Reset()
    void Set(MlineStyle source, bool checkIfReferenced)

--------------------------------------------------------------------------------

[STRUCT] MlineStyleElement
Full Name: Autodesk.AutoCAD.DatabaseServices.MlineStyleElement

  CONSTRUCTORS:
    new MlineStyleElement(double offset, Color color, ObjectId linetypeId)

  PROPERTIES:
    Color Color { get; }
    ObjectId LinetypeId { get; }
    double Offset { get; }

  METHODS:
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] MlineStyleElementCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.MlineStyleElementCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    MlineStyleElement Item { get; set; }

  METHODS:
    int Add(MlineStyleElement element, bool checkIfReferenced)
    void CopyTo(MlineStyleElement[] array, int index)
    MlineStyleElementCollectionEnumerator GetEnumerator()
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] MlineStyleElementCollectionEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.MlineStyleElementCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new MlineStyleElementCollectionEnumerator(MlineStyleElementCollection col)

  PROPERTIES:
    MlineStyleElement Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[ENUM] ModelDocViewLabelAlignmentType
Full Name: Autodesk.AutoCAD.DatabaseServices.ModelDocViewLabelAlignmentType
Inherits: Enum

  Values:
    - AlignmentLeft = 0
    - AlignmentCenter = 1
    - AlignmentRight = 2

--------------------------------------------------------------------------------

[ENUM] ModelDocViewLabelAttachmentPoint
Full Name: Autodesk.AutoCAD.DatabaseServices.ModelDocViewLabelAttachmentPoint
Inherits: Enum

  Values:
    - AboveView = 0
    - BelowView = 1

--------------------------------------------------------------------------------

[ENUM] ModelerFlavor
Full Name: Autodesk.AutoCAD.DatabaseServices.ModelerFlavor
Inherits: Enum

  Values:
    - Full = 0
    - RegionsOnly = 1
    - ObjectsOnly = 2

--------------------------------------------------------------------------------

[CLASS] ModifiesOwnerAttribute
Full Name: Autodesk.AutoCAD.DatabaseServices.ModifiesOwnerAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new ModifiesOwnerAttribute()

--------------------------------------------------------------------------------

[ENUM] MoveGripPointsFlags
Full Name: Autodesk.AutoCAD.DatabaseServices.MoveGripPointsFlags
Inherits: Enum

  Values:
    - Osnapped = 1
    - Polar = 2
    - ObjectTrack = 4
    - ZDir = 8
    - Keyboard = 16

--------------------------------------------------------------------------------

[ENUM] MoveType
Full Name: Autodesk.AutoCAD.DatabaseServices.MoveType
Inherits: Enum

  Values:
    - MoveAllPoints = 0
    - MoveAllExceptArrowHeaderPoints = 1
    - MoveContentAndDoglegPoints = 2

--------------------------------------------------------------------------------

[CLASS] MText
Full Name: Autodesk.AutoCAD.DatabaseServices.MText
Inherits: Entity

  CONSTRUCTORS:
    new MText()

  PROPERTIES:
    double ActualHeight { get; }
    double ActualWidth { get; }
    static string AlignChange { get; }
    double Ascent { get; }
    AttachmentPoint Attachment { get; set; }
    bool BackgroundFill { get; set; }
    Color BackgroundFillColor { get; set; }
    double BackgroundScaleFactor { get; set; }
    Transparency BackgroundTransparency { get; set; }
    static string BlockBegin { get; }
    static string BlockEnd { get; }
    static string ColorChange { get; }
    bool ColumnAutoHeight { get; set; }
    int ColumnCount { get; set; }
    bool ColumnFlowReversed { get; set; }
    double ColumnGutterWidth { get; set; }
    ColumnType ColumnType { get; set; }
    double ColumnWidth { get; set; }
    string Contents { get; set; }
    string ContentsRTF { get; set; }
    double Descent { get; }
    Vector3d Direction { get; set; }
    FlowDirection FlowDirection { get; set; }
    static string FontChange { get; }
    double Height { get; set; }
    static string HeightChange { get; }
    static string LineBreak { get; }
    double LineSpaceDistance { get; set; }
    double LineSpacingFactor { get; set; }
    LineSpacingStyle LineSpacingStyle { get; set; }
    Point3d Location { get; set; }
    static string NonBreakSpace { get; }
    Vector3d Normal { get; set; }
    static string ObliqueChange { get; }
    static string OverlineOff { get; }
    static string OverlineOn { get; }
    static string ParagraphBreak { get; }
    double Rotation { get; set; }
    bool ShowBorders { get; set; }
    static string StackStart { get; }
    static string StrikethroughOff { get; }
    static string StrikethroughOn { get; }
    string Text { get; }
    double TextHeight { get; set; }
    ObjectId TextStyleId { get; set; }
    string TextStyleName { get; }
    static string TrackChange { get; }
    static string UnderlineOff { get; }
    static string UnderlineOn { get; }
    bool UseBackgroundColor { get; set; }
    double Width { get; set; }
    static string WidthChange { get; }

  METHODS:
    void ConvertFieldToText()
    int CorrectSpelling()
    void ExplodeFragments(MTextFragmentCallback enumerator)
    void ExplodeFragments(MTextFragmentCallback enumerator, object userData)
    void ExplodeFragments(MTextFragmentCallback enumerator, object userData, WorldDraw context)
    Point3dCollection GetBoundingPoints()
    double GetColumnHeight(int index)
    string getMTextWithFieldCodes()
    void SetAttachmentMovingLocation(AttachmentPoint value)
    void SetColumnHeight(int index, double height)
    int SetContentsRtf(string value)
    void SetDynamicColumns(double width, double gutter, bool auto_height)
    void SetStaticColumns(double width, double gutter, int count)

--------------------------------------------------------------------------------

[CLASS] MTextFragment
Full Name: Autodesk.AutoCAD.DatabaseServices.MTextFragment
Inherits: DisposableWrapper

  PROPERTIES:
    string BigFont { get; }
    bool Bold { get; }
    double CapsHeight { get; }
    EntityColor Color { get; }
    Vector3d Direction { get; }
    Point2d Extents { get; }
    bool Italic { get; }
    Point3d Location { get; }
    Vector3d Normal { get; }
    double ObliqueAngle { get; }
    bool Overlined { get; }
    string ShxFont { get; }
    bool StackBottom { get; }
    bool StackTop { get; }
    bool Strikethrough { get; }
    string Text { get; }
    double TrackingFactor { get; }
    string TrueTypeFont { get; }
    bool Underlined { get; }
    double WidthFactor { get; }

  METHODS:
    Point3d[] GetOverLinePoints()
    Point3d[] GetStrikethroughPoints()
    Point3d[] GetUnderLinePoints()

--------------------------------------------------------------------------------

[CLASS] MTextFragmentCallback
Full Name: Autodesk.AutoCAD.DatabaseServices.MTextFragmentCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new MTextFragmentCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(MTextFragment __unnamed000, object __unnamed001, AsyncCallback callback, object obj)
    MTextFragmentCallbackStatus EndInvoke(IAsyncResult result)
    MTextFragmentCallbackStatus Invoke(MTextFragment A_0, object A_1)

--------------------------------------------------------------------------------

[ENUM] MTextFragmentCallbackStatus
Full Name: Autodesk.AutoCAD.DatabaseServices.MTextFragmentCallbackStatus
Inherits: Enum

  Values:
    - Terminate = 0
    - Continue = 1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] MultiModesGripPE
Full Name: Autodesk.AutoCAD.DatabaseServices.MultiModesGripPE
Inherits: RXObject

  CONSTRUCTORS:
    new MultiModesGripPE()

  METHODS:
    GripMode CurrentMode(Entity entity, GripData gripData)
    uint CurrentModeId(Entity entity, GripData gripData)
    bool GetGripModes(Entity entity, GripData gripData, GripModeCollection modes, ref uint curMode)
    GripType GetGripType(Entity entity, GripData gripData)
    void Reset(Entity entity)
    bool SetCurrentMode(Entity entity, GripData gripData, uint curMode)

--------------------------------------------------------------------------------

[ENUM] NewLayerNotification
Full Name: Autodesk.AutoCAD.DatabaseServices.NewLayerNotification
Inherits: Enum

  Values:
    - NoNewLayerNotification = 0
    - NotifyOnPlot = 1
    - NotifyOnOpen = 2
    - NotifyOnXrefAttachAndReload = 4
    - NotifyOnLayerStateRestore = 8
    - NotifyOnSave = 16
    - NotifyOnInsert = 32

--------------------------------------------------------------------------------

[CLASS] NormalConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.NormalConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new NormalConstraint()

--------------------------------------------------------------------------------

[CLASS] Notifier
Full Name: Autodesk.AutoCAD.DatabaseServices.Notifier
Implements: INotifyPropertyChanged

  CONSTRUCTORS:
    new Notifier()

  METHODS:
    void OnPropertyChanged(string propertyName)

  EVENTS:
    event PropertyChangedEventHandler PropertyChanged

--------------------------------------------------------------------------------

[STRUCT] NurbsData
Full Name: Autodesk.AutoCAD.DatabaseServices.NurbsData

  PROPERTIES:
    bool Closed { get; }
    double ControlPointTolerance { get; }
    int Degree { get; }
    double KnotTolerance { get; }
    bool Periodic { get; }
    bool Rational { get; }

  METHODS:
    bool Equals(object obj)
    Point3dCollection GetControlPoints()
    int GetHashCode()
    DoubleCollection GetKnots()
    DoubleCollection GetWeights()
    bool IsEqualTo(NurbsData other)
    bool IsEqualTo(NurbsData other, Tolerance tolerance)

--------------------------------------------------------------------------------

[CLASS] NurbSurface
Full Name: Autodesk.AutoCAD.DatabaseServices.NurbSurface
Inherits: Surface

  CONSTRUCTORS:
    new NurbSurface(int uDegree, int vDegree, bool rational, int uNumControlPoints, int vNumControlPoints, Point3dCollection ctrlPts, DoubleCollection weights, KnotCollection uKnots, KnotCollection vKnots)
    new NurbSurface()

  PROPERTIES:
    Point3dCollection ControlPoints { get; }
    int DegreeInU { get; }
    int DegreeInV { get; }
    bool IsClosedInU { get; }
    bool IsClosedInV { get; }
    bool IsPeriodicInU { get; }
    bool IsPeriodicInV { get; }
    bool IsRational { get; }
    int NumberOfControlPointsInU { get; }
    int NumberOfControlPointsInV { get; }
    int NumberOfKnotsInU { get; }
    int NumberOfKnotsInV { get; }
    int NumberOfSpansInU { get; }
    int NumberOfSpansInV { get; }
    double PeriodInU { get; }
    double PeriodInV { get; }
    KnotCollection UKnots { get; }
    KnotCollection VKnots { get; }

  METHODS:
    void Evaluate(double u, double v, int derivDegree, ref Point3d point, ref Vector3dCollection derivatives)
    void Evaluate(double u, double v, ref Point3d pos, ref Vector3d uDeriv, ref Vector3d vDeriv, ref Vector3d uuDeriv, ref Vector3d uvDeriv, ref Vector3d vvDeriv)
    void Evaluate(double u, double v, ref Point3d pos, ref Vector3d uDeriv, ref Vector3d vDeriv)
    Point3d Evaluate(double u, double v)
    Point3d GetControlPointAt(int uIndex, int vIndex)
    Curve[] GetIsolineAtU(double u)
    Curve[] GetIsolineAtV(double v)
    Vector3d GetNormal(double u, double v)
    void GetParameterOfPoint(Point3d point, out double u, out double v)
    double GetWeight(int uIndex, int vIndex)
    void InsertControlPointsAtU(double u, Point3dCollection vCtrlPts, DoubleCollection vWeights)
    void InsertControlPointsAtV(double v, Point3dCollection uCtrlPts, DoubleCollection uWeights)
    void InsertKnotAtU(double u)
    void InsertKnotAtV(double v)
    bool IsPlanar(out Point3d ptOnSurface, out Vector3d normal)
    bool IsPointOnSurface(Point3d point)
    void ModifyPosition(double u, double v, Point3d point)
    void ModifyPositionAndTangent(double u, double v, Point3d point, Vector3d uDeriv, Vector3d vDeriv)
    void Rebuild(int uDegree, int vDegree, int numUCtrlPts, int numVCtrlPts, bool bRestore)
    void Rebuild(int uDegree, int vDegree, int numUCtrlPts, int numVCtrlPts)
    void RemoveControlPointsAtU(int uIndex)
    void RemoveControlPointsAtV(int vIndex)
    void Set(int uDegree, int vDegree, bool rational, int uNumControlPoints, int vNumControlPoints, Point3dCollection ctrlPts, DoubleCollection weights, KnotCollection uKnots, KnotCollection vKnots)
    void SetControlPointAt(int uIndex, int vIndex, Point3d point)
    void SetControlPoints(int uCount, int vCount, Point3dCollection points)
    void SetWeight(int uIndex, int vIndex, double weight)

--------------------------------------------------------------------------------

[CLASS] ObjectClosedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectClosedEventArgs
Inherits: EventArgs

  PROPERTIES:
    ObjectId Id { get; }

--------------------------------------------------------------------------------

[CLASS] ObjectClosedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectClosedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ObjectClosedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ObjectClosedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ObjectClosedEventArgs e)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ObjectContext
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectContext
Inherits: RXObject

  PROPERTIES:
    string CollectionName { get; }
    string Name { get; set; }
    IntPtr UniqueIdentifier { get; }

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ObjectContextCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectContextCollection
Inherits: RXObject
Implements: IEnumerable

  PROPERTIES:
    ObjectContext CurrentContext { get; set; }
    string Name { get; }

  METHODS:
    void AddContext(ObjectContext ctxt)
    ObjectContext GetContext(string contextName)
    ObjectContextCollectionEnumerator GetEnumerator()
    bool HasContext(string contextName)
    IEnumerator IEnumerable.GetEnumerator()
    void RemoveContext(string contextName)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ObjectContextCollectionEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectContextCollectionEnumerator
Inherits: RXObject
Implements: IEnumerator

  PROPERTIES:
    ObjectContext Current { get; }

  METHODS:
    object IEnumerator.get_Current()
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] ObjectContextManager
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectContextManager
Inherits: RXObject

  CONSTRUCTORS:
    new ObjectContextManager()

  METHODS:
    ObjectContextCollection GetContextCollection(string collectionName)
    void RegisterContextCollection(string collectionName, ObjectContextCollection collection)
    void UnregisterContextCollection(string collectionName)

--------------------------------------------------------------------------------

[CLASS] ObjectErasedEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectErasedEventArgs
Inherits: EventArgs

  PROPERTIES:
    DBObject DBObject { get; }
    bool Erased { get; }

--------------------------------------------------------------------------------

[CLASS] ObjectErasedEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectErasedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ObjectErasedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ObjectErasedEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ObjectErasedEventArgs e)

--------------------------------------------------------------------------------

[CLASS] ObjectEventArgs
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs
Inherits: EventArgs

  PROPERTIES:
    DBObject DBObject { get; }

--------------------------------------------------------------------------------

[CLASS] ObjectEventHandler
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ObjectEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, ObjectEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, ObjectEventArgs e)

--------------------------------------------------------------------------------

[STRUCT] ObjectId
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectId
Implements: IComparable<ObjectId>, IDynamicMetaObjectProvider

  CONSTRUCTORS:
    new ObjectId(IntPtr oldId)

  PROPERTIES:
    Database Database { get; }
    Handle Handle { get; }
    bool IsEffectivelyErased { get; }
    bool IsErased { get; }
    bool IsNull { get; }
    bool IsResident { get; }
    bool IsValid { get; }
    bool IsWellBehaved { get; }
    Handle NonForwardedHandle { get; }
    static ObjectId Null { get; }
    RXClass ObjectClass { get; }
    bool ObjectLeftOnDisk { get; }
    long OldId { get; }
    IntPtr OldIdPtr { get; }
    Database OriginalDatabase { get; }

  METHODS:
    bool Compare(ObjectId value1)
    int CompareTo(ObjectId other)
    ObjectId ConvertToRedirectedId()
    bool Equals(object obj)
    int GetHashCode()
    DynamicMetaObject GetMetaObject(Expression parameter)
    DBObject GetObject(OpenMode mode, bool openErased, bool forceOpenOnLockedLayer)
    DBObject GetObject(OpenMode mode, bool openErased)
    DBObject GetObject(OpenMode mode)
    DBObject Open(OpenMode mode, bool openErased, bool forceOpenOnLockedLayer)
    DBObject Open(OpenMode mode, bool openErased)
    DBObject Open(OpenMode mode)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[CLASS] ObjectIdCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection
Inherits: DisposableWrapper
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new ObjectIdCollection(ObjectId[] values)
    new ObjectIdCollection()

  PROPERTIES:
    int Count { get; }
    ObjectId Item { get; set; }

  METHODS:
    int Add(ObjectId value)
    void Clear()
    bool Contains(ObjectId value)
    void CopyTo(ObjectId[] array, int index)
    IEnumerator GetEnumerator()
    int IndexOf(ObjectId value)
    void Insert(int index, ObjectId value)
    void Remove(ObjectId value)
    void RemoveAt(int index)

--------------------------------------------------------------------------------

[CLASS] ObjectIdGraph
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectIdGraph
Inherits: Graph

  CONSTRUCTORS:
    new ObjectIdGraph()

  METHODS:
    void AddNode(ObjectIdGraphNode nodeToAdd)
    void DelNode(ObjectIdGraphNode nodeToDelete)
    ObjectIdGraphNode FindNode(ObjectId id)
    ObjectIdGraphNode IdNode(int index)

--------------------------------------------------------------------------------

[CLASS] ObjectIdGraphNode
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectIdGraphNode
Inherits: GraphNode

  CONSTRUCTORS:
    new ObjectIdGraphNode(ObjectId id)

  PROPERTIES:
    ObjectId Id { get; }

  METHODS:
    static ObjectIdGraphNode Create(IntPtr unmanagedPointer, bool autoDelete)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] ObjectOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectOverrule
Inherits: Overrule

  METHODS:
    void Cancel(DBObject dbObject)
    void Close(DBObject dbObject)
    DBObject DeepClone(DBObject dbObject, DBObject ownerObject, IdMapping idMap, bool isPrimary)
    void Erase(DBObject dbObject, bool erasing)
    void Open(DBObject dbObject, OpenMode mode)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)
    DBObject WblockClone(DBObject dbObject, RXObject ownerObject, IdMapping idMap, bool isPrimary)

--------------------------------------------------------------------------------

[CLASS] ObjectSnapContext
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectSnapContext

  PROPERTIES:
    int GraphicsSystemSelectionMark { get; }
    Point3d LastPoint { get; }
    Entity PickedObject { get; }
    Point3d PickPoint { get; }
    Matrix3d ViewTransform { get; }

--------------------------------------------------------------------------------

[CLASS] ObjectSnapInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectSnapInfo

  PROPERTIES:
    Curve3dCollection SnapCurves { get; }
    Point3dCollection SnapPoints { get; }

--------------------------------------------------------------------------------

[ENUM] ObjectSnapModes
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectSnapModes
Inherits: Enum

  Values:
    - ModeEnd = 1
    - ModeMid = 2
    - ModeCenter = 3
    - ModeNode = 4
    - ModeQuad = 5
    - ModeIns = 7
    - ModePerpendicular = 8
    - ModeTangent = 9
    - ModeNear = 10

--------------------------------------------------------------------------------

[CLASS] ObjectTypeAttribute
Full Name: Autodesk.AutoCAD.DatabaseServices.ObjectTypeAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new ObjectTypeAttribute(Type type)

  PROPERTIES:
    Type ObjectType { get; }

--------------------------------------------------------------------------------

[CLASS] Ole2Frame
Full Name: Autodesk.AutoCAD.DatabaseServices.Ole2Frame
Inherits: Entity

  CONSTRUCTORS:
    new Ole2Frame()

  PROPERTIES:
    byte AutoOutputQuality { get; set; }
    bool IsLinked { get; }
    string LinkName { get; }
    string LinkPath { get; }
    Point3d Location { get; }
    bool LockAspect { get; set; }
    object OleObject { get; }
    byte OutputQuality { get; set; }
    Rectangle Position2d { get; set; }
    Rectangle3d Position3d { get; set; }
    double Rotation { get; set; }
    double ScaleHeight { get; set; }
    double ScaleWidth { get; set; }
    ItemType Type { get; }
    string UserType { get; }
    double WcsHeight { get; set; }
    double WcsWidth { get; set; }

--------------------------------------------------------------------------------

[CLASS] OpenCloseTransaction
Full Name: Autodesk.AutoCAD.DatabaseServices.OpenCloseTransaction
Inherits: Transaction

  CONSTRUCTORS:
    new OpenCloseTransaction()

  PROPERTIES:
    TransactionManager TransactionManager { get; }

  METHODS:
    void Abort()
    void AddNewlyCreatedDBObject(DBObject obj, bool add)
    void Commit()
    DBObject GetObject(ObjectId id, OpenMode mode, bool openErased, bool forceOpenOnLockedLayer)
    DBObject GetObject(ObjectId id, OpenMode mode, bool openErased)
    DBObject GetObject(ObjectId id, OpenMode mode)

--------------------------------------------------------------------------------

[ENUM] OpenMode
Full Name: Autodesk.AutoCAD.DatabaseServices.OpenMode
Inherits: Enum

  Values:
    - ForRead = 0
    - ForWrite = 1
    - ForNotify = 2

--------------------------------------------------------------------------------

[CLASS] OpenModeAttribute
Full Name: Autodesk.AutoCAD.DatabaseServices.OpenModeAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new OpenModeAttribute(OpenMode mode)

  PROPERTIES:
    OpenMode OpenMode { get; }

--------------------------------------------------------------------------------

[CLASS] OrdinateDimension
Full Name: Autodesk.AutoCAD.DatabaseServices.OrdinateDimension
Inherits: Dimension

  CONSTRUCTORS:
    new OrdinateDimension(bool useXAxis, Point3d definingPoint, Point3d leaderEndPoint, string dimText, ObjectId dimStyle)
    new OrdinateDimension()

  PROPERTIES:
    Point3d DefiningPoint { get; set; }
    Point3d LeaderEndPoint { get; set; }
    Point3d Origin { get; set; }
    bool UsingXAxis { get; set; }
    bool UsingYAxis { get; }

--------------------------------------------------------------------------------

[ENUM] OrthographicView
Full Name: Autodesk.AutoCAD.DatabaseServices.OrthographicView
Inherits: Enum

  Values:
    - NonOrthoView = 0
    - TopView = 1
    - BottomView = 2
    - FrontView = 3
    - BackView = 4
    - LeftView = 5
    - RightView = 6

--------------------------------------------------------------------------------

[ABSTRACT CLASS] OsnapOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.OsnapOverrule
Inherits: Overrule

  METHODS:
    void GetObjectSnapPoints(Entity entity, ObjectSnapModes snapMode, IntPtr gsSelectionMark, Point3d pickPoint, Point3d lastPoint, Matrix3d viewTransform, Point3dCollection snapPoints, IntegerCollection geometryIds, Matrix3d insertionMat)
    void GetObjectSnapPoints(Entity entity, ObjectSnapModes snapMode, IntPtr gsSelectionMark, Point3d pickPoint, Point3d lastPoint, Matrix3d viewTransform, Point3dCollection snapPoints, IntegerCollection geometryIds)
    bool IsContentSnappable(Entity entity)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)

--------------------------------------------------------------------------------

