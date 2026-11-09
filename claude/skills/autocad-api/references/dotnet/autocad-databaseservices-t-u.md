# Autodesk.AutoCAD.DatabaseServices

NAMESPACE: Autodesk.AutoCAD.DatabaseServices
--------------------------------------------------------------------------------

[CLASS] Table
Full Name: Autodesk.AutoCAD.DatabaseServices.Table
Inherits: BlockReference
Implements: IEnumerable

  CONSTRUCTORS:
    new Table()

  PROPERTIES:
    bool BreakEnabled { get; set; }
    TableBreakFlowDirection BreakFlowDirection { get; set; }
    TableBreakOptions BreakOptions { get; set; }
    CellRange Cells { get; }
    ColumnsCollection Columns { get; }
    Vector3d Direction { get; set; }
    FlowDirection FlowDirection { get; set; }
    bool HasSubSelection { get; }
    double Height { get; set; }
    double HorizontalCellMargin { get; set; }
    bool IsHeaderSuppressed { get; set; }
    bool IsTitleSuppressed { get; set; }
    double MinimumTableHeight { get; }
    double MinimumTableWidth { get; }
    int NumColumns { get; set; }
    int NumRows { get; set; }
    RowsCollection Rows { get; }
    CellRange SubSelection { get; set; }
    ObjectId TableStyle { get; set; }
    string TableStyleName { get; }
    double VerticalCellMargin { get; set; }
    double Width { get; set; }

  METHODS:
    CellAlignment Alignment(int row, int col)
    CellAlignment Alignment(RowType type)
    Point3d AttachmentPoint(int row, int col)
    Color BackgroundColor(int row, int col)
    Color BackgroundColor(RowType type)
    double BlockRotation(int row, int col)
    double BlockScale(int row, int col)
    ObjectId BlockTableRecordId(int row, int col)
    bool CanDeleteColumns(int index, int nCount)
    bool CanDeleteRows(int index, int nCount)
    bool CanInsertColumn(int index)
    bool CanInsertRow(int index)
    TableStyleOverride[] CellStyleOverrides(int row, int col)
    TableCellType CellType(int row, int col)
    void ClearSubSelection()
    void ClearTableStyleOverrides(int options)
    double ColumnWidth(int col)
    Color ContentColor(int row, int col)
    Color ContentColor(RowType type)
    CellRange CopyFrom(Table source, TableCopyOptions options, CellRange sourceRange, CellRange targetRange)
    CellRange CopyFrom(LinkedTableData source, TableCopyOptions options, CellRange sourceRange, CellRange targetRange)
    void CopyFrom(LinkedTableData source, TableCopyOptions options)
    int CreateContent(int row, int column, int contentIndex)
    DataType DataType(int row, int col)
    DataType DataType(RowType type)
    void DeleteCellContent(int row, int col)
    void DeleteColumns(int col, int columns)
    void DeleteContent(CellRange A_0)
    void DeleteContent(int row, int column, int contentIndex)
    void DeleteContent(int row, int column)
    void DeleteRows(int row, int rows)
    ObjectId FieldId(int row, int col)
    void Fill(CellRange fillRange, CellRange sourceRange, TableFillOptions options)
    string Format(int row, int col)
    string Format(RowType type)
    void GenerateLayout()
    string GetBlockAttributeValue(int row, int column, int contentIndex, ObjectId attDefId)
    string GetBlockAttributeValue(int row, int col, ObjectId attributeDefinitionId)
    ObjectId GetBlockTableRecordId(int row, int column, int contentIndex)
    double GetBreakHeight(int index)
    Vector3d GetBreakOffset(int index)
    double GetBreakSpacing()
    void GetCellExtents(int row, int col, bool isOuterCell, Point3dCollection pts)
    CellStates GetCellState(int row, int column)
    string GetCellStyle(int row, int column)
    string GetColumnName(int index)
    Color GetContentColor(int row, int column, int contentIndex)
    CellContentLayout GetContentLayout(int row, int column)
    CellContentTypes GetContentTypes(int row, int column, int contentIndex)
    CellContentTypes GetContentTypes(int row, int column)
    object GetCustomData(int row, int column, string key)
    int GetCustomData(int row, int column)
    string GetDataFormat(int row, int column, int contentIndex)
    string GetDataFormat(int row, int column)
    ObjectIdCollection GetDataLink(CellRange range)
    ObjectIdCollection GetDataLink()
    ObjectId GetDataLink(int row, int column)
    CellRange GetDataLinkRange(int row, int column)
    DataTypeParameter GetDataType(int row, int column, int contentIndex)
    TableEnumerator GetEnumerator(CellRange range, TableEnumeratorOption option)
    TableEnumerator GetEnumerator()
    ObjectId GetFieldId(int row, int column, int contentIndex)
    string GetFormula(int row, int column, int contentIndex)
    Color GetGridColor(int row, int column, GridLineType gridLineType)
    double GetGridDoubleLineSpacing(int row, int column, GridLineType gridLineType)
    GridLineStyle GetGridLineStyle(int row, int column, GridLineType gridLineType)
    ObjectId GetGridLinetype(int row, int column, GridLineType gridLineType)
    LineWeight GetGridLineWeight(int row, int column, GridLineType gridLineType)
    GridPropertyParameter GetGridProperty(int row, int column, GridLineType gridLineType)
    Visibility GetGridVisibility(int row, int column, GridLineType gridLineType)
    bool GetIsAutoScale(int row, int column, int contentIndex)
    double GetMargin(int row, int column, CellMargins margin)
    bool GetMergeAllEnabled(int row, int column)
    CellRange GetMergeRange(int row, int column)
    int GetNumberOfContents(int row, int column)
    CellProperties GetOverrides(int row, int column, GridLineType gridLineType)
    CellProperties GetOverrides(int row, int column, int contentIndex)
    double GetRotation(int row, int column, int contentIndex)
    double GetScale(int row, int column, int contentIndex)
    double GetTextHeight(int row, int column, int contentIndex)
    string GetTextString(int row, int column, int contentIndex, FormatOption formatOption)
    string GetTextString(int row, int column, int contentIndex)
    ObjectId GetTextStyleId(int row, int column, int contentIndex)
    string GetToolTip(int row, int column)
    object GetValue(int row, int column, int contentIndex, FormatOption formatOption)
    object GetValue(int row, int column, int contentIndex)
    Color GridColor(int row, int col, CellEdgeMasks edge)
    Color GridColor(GridLineType gridlineType, RowType type)
    LineWeight GridLineWeight(int row, int col, CellEdgeMasks edge)
    LineWeight GridLineWeight(GridLineType gridlineType, RowType type)
    bool GridVisibility(int row, int col, CellEdgeMasks edge)
    bool GridVisibility(GridLineType gridlineType, RowType type)
    bool HasFormula(int row, int column, int contentIndex)
    TableHitTestInfo HitTest(Point3d point, Vector3d viewVector)
    IEnumerator IEnumerable.GetEnumerator()
    void InsertColumns(int col, double width, int columns)
    void InsertColumnsAndInherit(int col, int inheritFrom, int numCols)
    void InsertRows(int row, double height, int rows)
    void InsertRowsAndInherit(int index, int inheritFrom, int numRows)
    bool IsAutoScale(int row, int col)
    bool IsBackgroundColorNone(int row, int col)
    bool IsBackgroundColorNone(RowType type)
    bool IsContentEditable(int row, int column)
    bool IsEmpty(int row, int column)
    bool IsFormatEditable(int row, int column)
    bool IsLinked(int row, int column)
    bool IsMergedCell(int row, int col, out CellRange range)
    void MergeCells(CellRange range)
    double MinimumColumnWidth(int col)
    double MinimumRowHeight(int row)
    void MoveContent(int row, int column, int fromIndex, int toIndex)
    void RecomputeTableBlock(bool forceUpdate)
    void RemoveAllOverrides(int row, int column)
    void RemoveDataLink()
    void RemoveDataLink(int row, int column)
    void ReselectSubRegion(FullSubentityPath[] paths)
    void ResetValue(int row, int col)
    double RowHeight(int row)
    RowType RowType(int row)
    TableHitTestInfo Select(Point3d pickingPoint, Vector3d hitTestViewDirection, Vector3d hitTestViewOrientation, bool allowOutside, bool inPickFirst, FullSubentityPath[] paths)
    CellRange SelectSubRegion(Point3d cornerPoint1, Point3d cornerPoint2, Vector3d selectionViewDirection, Vector3d hitTestViewDirection, SelectType selectionType, bool includeCurrentSelection, bool inPickFirst, FullSubentityPath[] paths)
    void SetAlignment(int row, int col, CellAlignment align)
    void SetAlignment(CellAlignment align, int rowTypes)
    void SetAutoScale(int row, int col, bool autoFit)
    void SetBackgroundColor(int row, int col, Color color)
    void SetBackgroundColor(Color color, int rowTypes)
    void SetBackgroundColorNone(int row, int col, bool value)
    void SetBackgroundColorNone(bool value, int rowTypes)
    void SetBlockAttributeValue(int row, int column, int contentIndex, ObjectId attDefId, string value)
    void SetBlockAttributeValue(int row, int col, ObjectId attributeDefinitionId, string value)
    void SetBlockRotation(int row, int col, double rotationalAngle)
    void SetBlockScale(int row, int col, double scale)
    void SetBlockTableRecordId(int row, int column, int contentIndex, ObjectId blockId, bool autoFit)
    void SetBlockTableRecordId(int row, int col, ObjectId blockId, bool autoFit)
    void SetBreakHeight(int index, double height)
    void SetBreakOffset(int index, Vector3d offset)
    void SetBreakSpacing(double spacing)
    void SetCellState(int row, int column, CellStates cellState)
    void SetCellStyle(int row, int column, string styleName)
    void SetCellType(int row, int col, TableCellType type)
    void SetColumnName(int index, string name)
    void SetColumnWidth(double width)
    void SetColumnWidth(int col, double width)
    void SetContentColor(int row, int column, int contentIndex, Color color)
    void SetContentColor(int row, int col, Color color)
    void SetContentColor(Color color, int rowType)
    void SetContentLayout(int row, int column, CellContentLayout layout)
    void SetCustomData(int row, int column, string key, object value)
    void SetCustomData(int row, int column, int data)
    void SetDataFormat(int row, int column, int contentIndex, string format)
    void SetDataFormat(int row, int column, string format)
    void SetDataLink(CellRange range, ObjectId dataLinkId, bool bUpdate)
    void SetDataLink(int row, int column, ObjectId dataLinkId, bool bUpdate)
    void SetDataType(int row, int column, int contentIndex, DataTypeParameter dataType)
    void SetDataType(int row, int col, DataType nDataType, UnitType nUnitType)
    void SetDataType(DataType nDataType, UnitType nUnitType, int rowTypes)
    void SetFieldId(int row, int column, int contentIndex, ObjectId fieldId, CellOption option)
    void SetFieldId(int row, int col, ObjectId fieldId)
    void SetFormat(int row, int col, string pFormat)
    void SetFormat(string pFormat, int rowTypes)
    void SetFormula(int row, int column, int contentIndex, string formula)
    void SetGridColor(int row, int column, GridLineType gridLineType, Color color)
    void SetGridColor(int row, int col, short edges, Color color)
    void SetGridColor(Color color, int borders, int rows)
    void SetGridDoubleLineSpacing(int row, int column, GridLineType gridLineType, double spacing)
    void SetGridLineStyle(int row, int column, GridLineType gridLineType, GridLineStyle lineStyle)
    void SetGridLinetype(int row, int column, GridLineType gridLineType, ObjectId linetype)
    void SetGridLineWeight(int row, int column, GridLineType gridLineType, LineWeight lineWeight)
    void SetGridLineWeight(int row, int col, short edges, LineWeight value)
    void SetGridLineWeight(LineWeight lineWeight, int borders, int rows)
    void SetGridProperty(CellRange rangeIn, GridLineType gridLineTypes, GridPropertyParameter gridProp)
    void SetGridProperty(int row, int column, GridLineType gridLineType, GridPropertyParameter gridProperty)
    void SetGridVisibility(int row, int column, GridLineType gridLineType, Visibility visibility)
    void SetGridVisibility(int row, int col, short edges, bool value)
    void SetGridVisibility(bool visible, int borders, int rows)
    void SetIsAutoScale(int row, int column, int contentIndex, bool autoFit)
    void SetMargin(int row, int column, CellMargins margin, double value)
    void SetMergeAllEnabled(int row, int column, bool enable)
    void SetOverrides(int row, int column, GridLineType gridLineType, CellProperties override)
    void SetOverrides(int row, int column, int contentIndex, CellProperties override)
    void SetRotation(int row, int column, int contentIndex, double angle)
    void SetRowHeight(double height)
    void SetRowHeight(int row, double height)
    void SetScale(int row, int column, int contentIndex, double scale)
    void SetSize(int numRows, int numCols)
    void SetTextHeight(int row, int column, int contentIndex, double height)
    void SetTextHeight(int row, int col, double height)
    void SetTextHeight(double height, int rowTypes)
    void SetTextRotation(int row, int col, RotationAngle rot)
    void SetTextString(int row, int column, int contentIndex, string text)
    void SetTextString(int row, int col, string value)
    void SetTextStyle(int row, int col, ObjectId id)
    void SetTextStyle(ObjectId id, int rowTypes)
    void SetTextStyleId(int row, int column, int contentIndex, ObjectId id)
    void SetToolTip(int row, int column, string toolTip)
    void SetValue(int row, int column, int contentIndex, string value, ParseOption parseOption)
    void SetValue(int row, int column, int contentIndex, object value, ParseOption parseOption)
    void SetValue(int row, int column, int contentIndex, object value)
    void SetValue(int row, int col, string pText, ParseOption nOption)
    void SetValue(int row, int col, object pValue)
    void SuppressRegenerateTable(bool suppress)
    TableStyleOverride[] TableStyleOverrides()
    double TextHeight(int row, int col)
    double TextHeight(RowType type)
    RotationAngle TextRotation(int row, int col)
    string TextString(int row, int col, FormatOption nOption)
    string TextString(int row, int col)
    string TextStringConst(int row, int col)
    ObjectId TextStyle(int row, int col)
    ObjectId TextStyle(RowType type)
    UnitType UnitType(int row, int col)
    UnitType UnitType(RowType type)
    void UnmergeCells(CellRange range)
    void UpdateDataLink(UpdateDirection dir, UpdateOption option)
    void UpdateDataLink(int row, int column, UpdateDirection dir, UpdateOption option)
    object Value(int row, int col)

--------------------------------------------------------------------------------

[ENUM] TableBreakFlowDirection
Full Name: Autodesk.AutoCAD.DatabaseServices.TableBreakFlowDirection
Inherits: Enum

  Values:
    - Right = 1
    - DownOrUp = 2
    - Left = 4

--------------------------------------------------------------------------------

[ENUM] TableBreakOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.TableBreakOptions
Inherits: Enum

  Values:
    - None = 0
    - EnableBreaking = 1
    - RepeatTopLabels = 2
    - RepeatBottomLabels = 4
    - AllowManualPositions = 8
    - AllowManualHeights = 16

--------------------------------------------------------------------------------

[ENUM] TableCellType
Full Name: Autodesk.AutoCAD.DatabaseServices.TableCellType
Inherits: Enum

  Values:
    - UnknownCell = 0
    - TextCell = 1
    - BlockCell = 2
    - MultipleContentCell = 3

--------------------------------------------------------------------------------

[CLASS] TableContent
Full Name: Autodesk.AutoCAD.DatabaseServices.TableContent
Inherits: FormattedTableData

  CONSTRUCTORS:
    new TableContent()

  PROPERTIES:
    ObjectId TableStyleId { get; set; }

  METHODS:
    string GetCellStyle(int row, int column)
    double GetColumnWidth(int column)
    double GetRowHeight(int row)
    void SetCellStyle(int row, int column, string cellStyle)
    void SetColumnWidth(int column, double width)
    void SetRowHeight(int row, double height)

--------------------------------------------------------------------------------

[ENUM] TableCopyOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.TableCopyOptions
Inherits: Enum

  Values:
    - None = 0
    - ExpandOrContractTable = 1
    - SkipContent = 2
    - SkipValue = 4
    - SkipField = 8
    - SkipFormula = 16
    - SkipBlock = 32
    - SkipDataLink = 64
    - SkipLabelCell = 128
    - SkipDataCell = 256
    - SkipFormat = 512
    - SkipCellStyle = 1024
    - ConvertFormatToOverrides = 2048
    - SkipCellState = 4096
    - SkipContentFormat = 8192
    - SkipDissimilarContentFormat = 16384
    - SkipGeometry = 32768
    - SkipMerges = 65536
    - FillTarget = 131072
    - OverwriteReadOnlyContent = 262144
    - OverwriteReadOnlyFormat = 524288
    - OverwriteContentModifiedAfterUpdate = 1048576
    - OverwriteFormatModifiedAfterUpdate = 2097152
    - OnlyContentModifiedAfterUpdate = 4194304
    - OnlyFormatModifiedAfterUpdate = 8388608
    - TableCopyRowHeight = 16777216
    - TableCopyColumnWidth = 33554432

--------------------------------------------------------------------------------

[CLASS] TableEnumerator
Full Name: Autodesk.AutoCAD.DatabaseServices.TableEnumerator
Inherits: DisposableWrapper
Implements: IEnumerator

  PROPERTIES:
    Cell Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[ENUM] TableEnumeratorOption
Full Name: Autodesk.AutoCAD.DatabaseServices.TableEnumeratorOption
Inherits: Enum

  Values:
    - None = 0
    - IterateSelection = 1
    - IterateRows = 2
    - IterateColumns = 4
    - SkipReadOnlyContent = 16
    - SkipReadOnlyFormat = 32
    - SkipMerged = 64

--------------------------------------------------------------------------------

[ENUM] TableFillOptions
Full Name: Autodesk.AutoCAD.DatabaseServices.TableFillOptions
Inherits: Enum

  Values:
    - None = 0
    - Row = 1
    - Reverse = 2
    - GenerateSeries = 4
    - CopyContent = 8
    - CopyFormat = 16
    - OverwriteReadOnlyContent = 32
    - OverwriteReadOnlyFormat = 64

--------------------------------------------------------------------------------

[STRUCT] TableHitTestInfo
Full Name: Autodesk.AutoCAD.DatabaseServices.TableHitTestInfo

  PROPERTIES:
    int Column { get; }
    int Row { get; }
    TableHitTestType Type { get; }

  METHODS:
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[ENUM] TableHitTestType
Full Name: Autodesk.AutoCAD.DatabaseServices.TableHitTestType
Inherits: Enum

  Values:
    - None = 0
    - Cell = 1

--------------------------------------------------------------------------------

[CLASS] TableStyle
Full Name: Autodesk.AutoCAD.DatabaseServices.TableStyle
Inherits: DBObject

  CONSTRUCTORS:
    new TableStyle()

  PROPERTIES:
    int BitFlags { get; set; }
    ArrayList CellStyles { get; }
    string Description { get; set; }
    FlowDirection FlowDirection { get; set; }
    double HorizontalCellMargin { get; set; }
    bool IsHeaderSuppressed { get; set; }
    bool IsTitleSuppressed { get; set; }
    string Name { get; set; }
    ObjectId Template { get; set; }
    double VerticalCellMargin { get; set; }

  METHODS:
    CellAlignment Alignment(RowType rowType)
    Color BackgroundColor(RowType rowType)
    CellClass CellClass(string styleName)
    Color Color(RowType rowType)
    DataType DataType(RowType rowType)
    string Format(RowType rowType)
    Color GridColor(GridLineType gridLineType, RowType rowType)
    double GridDoubleLineSpacing(GridLineType gridLineType, string styleName)
    GridLineStyle GridLineStyle(GridLineType gridLineType, string styleName)
    ObjectId GridLinetype(GridLineType gridLineType, string styleName)
    LineWeight GridLineWeight(GridLineType gridLineType, RowType rowType)
    bool GridVisibility(GridLineType gridLineType, RowType rowType)
    bool IsBackgroundColorNone(RowType rowType)
    double Margin(CellMargins cellMargin, string styleName)
    ObjectId PostTableStyleToDatabase(Database databasePointer, string styleName)
    void SetAlignment(CellAlignment alignment, int rowTypes)
    void SetBackgroundColor(Color color, int rowTypes)
    void SetBackgroundColorNone(bool value, int rowTypes)
    void SetCellClass(CellClass cellClass, string styleName)
    void SetColor(Color color, int rowTypes)
    void SetDataType(DataType nDataType, UnitType nUnitType, RowType rowTypes)
    void SetFormat(string pFormat, RowType rowTypes)
    void SetGridColor(Color color, int gridLineTypes, int rowTypes)
    void SetGridDoubleLineSpacing(double spacing, GridLineType gridLineTypes, string styleName)
    void SetGridLineStyle(GridLineStyle lineStyle, GridLineType gridLineTypes, string styleName)
    void SetGridLinetype(ObjectId linetype, GridLineType gridLineTypes, string styleName)
    void SetGridLineWeight(LineWeight lineWeight, int gridLineTypes, int rowTypes)
    void SetGridVisibility(bool visible, int gridLineTypes, int rowTypes)
    void SetMargin(CellMargins cellMargin, double margin, string styleName)
    void SetTextHeight(double value, string cellStyle)
    void SetTextHeight(double height, int rowTypes)
    void SetTextStyle(ObjectId id, string styleName)
    void SetTextStyle(ObjectId id, int rowTypes)
    double TextHeight(string cellStyle)
    double TextHeight(RowType rowType)
    ObjectId TextStyle(string styleName)
    ObjectId TextStyle(RowType rowType)
    UnitType UnitType(RowType rowType)

--------------------------------------------------------------------------------

[ENUM] TableStyleFlags
Full Name: Autodesk.AutoCAD.DatabaseServices.TableStyleFlags
Inherits: Enum

  Values:
    - HorizontalInsideLineFirst = 1
    - HorizontalInsideLineSecond = 2
    - HorizontalInsideLineThird = 4
    - TableStyleModified = 8

--------------------------------------------------------------------------------

[ENUM] TableStyleOverride
Full Name: Autodesk.AutoCAD.DatabaseServices.TableStyleOverride
Inherits: Enum

  Values:
    - TitleSuppressed = 1
    - HeaderSuppressed = 2
    - FlowDirection = 3
    - HorizontalCellMargin = 4
    - VerticalCellMargin = 5
    - TitleRowColor = 6
    - HeaderRowColor = 7
    - DataRowColor = 8
    - TitleRowFillNone = 9
    - HeaderRowFillNone = 10
    - DataRowFillNone = 11
    - TitleRowFillColor = 12
    - HeaderRowFillColor = 13
    - DataRowFillColor = 14
    - TitleRowAlignment = 15
    - HeaderRowAlignment = 16
    - DataRowAlignment = 17
    - TitleRowTextStyle = 18
    - HeaderRowTextStyle = 19
    - DataRowTextStyle = 20
    - TitleRowTextHeight = 21
    - HeaderRowTextHeight = 22
    - DataRowTextHeight = 23
    - TitleRowDataType = 24
    - HeaderRowDataType = 25
    - DataRowDataType = 26
    - TitleHorizontalTopColor = 40
    - TitleHorizontalInsideColor = 41
    - TitleHorizontalBottomColor = 42
    - TitleVerticalLeftColor = 43
    - TitleVerticalInsideColor = 44
    - TitleVerticalRightColor = 45
    - HeaderHorizontalTopColor = 46
    - HeaderHorizontalInsideColor = 47
    - HeaderHorizontalBottomColor = 48
    - HeaderVerticalLeftColor = 49
    - HeaderVerticalInsideColor = 50
    - HeaderVerticalRightColor = 51
    - DataHorizontalTopColor = 52
    - DataHorizontalInsideColor = 53
    - DataHorizontalBottomColor = 54
    - DataVerticalLeftColor = 55
    - DataVerticalInsideColor = 56
    - DataVerticalRightColor = 57
    - TitleHorizontalTopLineWeight = 70
    - TitleHorizontalInsideLineWeight = 71
    - TitleHorizontalBottomLineWeight = 72
    - TitleVerticalLeftLineWeight = 73
    - TitleVerticalInsideLineWeight = 74
    - TitleVerticalRightLineWeight = 75
    - HeaderHorizontalTopLineWeight = 76
    - HeaderHorizontalInsideLineWeight = 77
    - HeaderHorizontalBottomLineWeight = 78
    - HeaderVerticalLeftLineWeight = 79
    - HeaderVerticalInsideLineWeight = 80
    - HeaderVerticalRightLineWeight = 81
    - DataHorizontalTopLineWeight = 82
    - DataHorizontalInsideLineWeight = 83
    - DataHorizontalBottomLineWeight = 84
    - DataVerticalLeftLineWeight = 85
    - DataVerticalInsideLineWeight = 86
    - DataVerticalRightLineWeight = 87
    - TitleHorizontalTopVisibility = 100
    - TitleHorizontalInsideVisibility = 101
    - TitleHorizontalBottomVisibility = 102
    - TitleVerticalLeftVisibility = 103
    - TitleVerticalInsideVisibility = 104
    - TitleVerticalRightVisibility = 105
    - HeaderHorizontalTopVisibility = 106
    - HeaderHorizontalInsideVisibility = 107
    - HeaderHorizontalBottomVisibility = 108
    - HeaderVerticalLeftVisibility = 109
    - HeaderVerticalInsideVisibility = 110
    - HeaderVerticalRightVisibility = 111
    - DataHorizontalTopVisibility = 112
    - DataHorizontalInsideVisibility = 113
    - DataHorizontalBottomVisibility = 114
    - DataVerticalLeftVisibility = 115
    - DataVerticalInsideVisibility = 116
    - DataVerticalRightVisibility = 117
    - CellAlignment = 130
    - CellBackgroundFillNone = 131
    - CellBackgroundColor = 132
    - CellContentColor = 133
    - CellTextStyle = 134
    - CellTextHeight = 135
    - CellTopGridColor = 136
    - CellRightGridColor = 137
    - CellBottomGridColor = 138
    - CellLeftGridColor = 139
    - CellTopGridLineWeight = 140
    - CellRightGridLineWeight = 141
    - CellBottomGridLineWeight = 142
    - CellLeftGridLineWeight = 143
    - CellTopVisibility = 144
    - CellRightVisibility = 145
    - CellBottomVisibility = 146
    - CellLeftVisibility = 147
    - CellDataType = 148

--------------------------------------------------------------------------------

[CLASS] TableTemplate
Full Name: Autodesk.AutoCAD.DatabaseServices.TableTemplate
Inherits: TableContent

  CONSTRUCTORS:
    new TableTemplate(Table table, TableCopyOptions copyOption)

  METHODS:
    void Capture(Table table, TableCopyOptions copyOption)
    Table CreateTable(TableCopyOptions copyOption)

--------------------------------------------------------------------------------

[CLASS] TangentConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.TangentConstraint
Inherits: GeometricalConstraint

  CONSTRUCTORS:
    new TangentConstraint()

--------------------------------------------------------------------------------

[ENUM] TextAlignment
Full Name: Autodesk.AutoCAD.DatabaseServices.TextAlignment
Inherits: Enum

  Values:
    - LeftAlignment = 0
    - CenterAlignment = 1
    - RightAlignment = 2

--------------------------------------------------------------------------------

[ENUM] TextAlignmentType
Full Name: Autodesk.AutoCAD.DatabaseServices.TextAlignmentType
Inherits: Enum

  Values:
    - LeftAlignment = 0
    - CenterAlignment = 1
    - RightAlignment = 2

--------------------------------------------------------------------------------

[ENUM] TextAngleType
Full Name: Autodesk.AutoCAD.DatabaseServices.TextAngleType
Inherits: Enum

  Values:
    - InsertAngle = 0
    - HorizontalAngle = 1
    - AlwaysRightReadingAngle = 2

--------------------------------------------------------------------------------

[ENUM] TextAttachmentDirection
Full Name: Autodesk.AutoCAD.DatabaseServices.TextAttachmentDirection
Inherits: Enum

  Values:
    - AttachmentHorizontal = 0
    - AttachmentVertical = 1

--------------------------------------------------------------------------------

[ENUM] TextAttachmentType
Full Name: Autodesk.AutoCAD.DatabaseServices.TextAttachmentType
Inherits: Enum

  Values:
    - AttachmentTopOfTop = 0
    - AttachmentMiddleOfTop = 1
    - AttachmentMiddle = 2
    - AttachmentMiddleOfBottom = 3
    - AttachmentBottomOfBottom = 4
    - AttachmentBottomLine = 5
    - AttachmentBottomOfTopLine = 6
    - AttachmentBottomOfTop = 7
    - AttachmentAllLine = 8
    - AttachmentCenter = 9
    - AttachmentLinedCenter = 10

--------------------------------------------------------------------------------

[CLASS] TextEditor
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditor
Inherits: DisposableWrapper
Implements: ITextEditorSelectable

  PROPERTIES:
    double ActualHeight { get; }
    double ActualWidth { get; }
    AttachmentPoint Attachment { get; set; }
    bool AutoCAPS { get; set; }
    bool AutoListEnabled { get; set; }
    TextEditorColumns Columns { get; }
    TextEditorCursor Cursor { get; }
    FlowAlign DefaultStackAlignment { get; set; }
    double DefaultStackScale { get; set; }
    double DefinedHeight { get; set; }
    double DefinedWidth { get; set; }
    TextEditorLocation EndOfText { get; }
    int FontCount { get; }
    bool IsAnnotativeStyle { get; }
    bool NumberingEnabled { get; set; }
    TextEditorParagraph[] Paragraphs { get; }
    TextEditorSelection Selection { get; }
    int StackCount { get; }
    TextEditorLocation StartOfText { get; }
    ObjectId Style { get; set; }
    int StyleCount { get; }
    bool TabOnlyDelimiter { get; set; }
    double TextHeight { get; set; }
    bool VerticalSHX { get; }
    bool VerticalTTF { get; }
    TextEditorWipeout Wipeout { get; }

  METHODS:
    void ClearSelection()
    void Close(ExitStatus stat)
    static TextEditor CreateTextEditor(MText mtext)
    void FindTextW(string findString, TextFindFlags findFlags, out TextEditorLocation start, out TextEditorLocation end)
    Font GetFont(int index)
    void MakeSelection(TextEditorLocation start, TextEditorLocation end)
    void Redraw()
    void SelectAll()

--------------------------------------------------------------------------------

[CLASS] TextEditorColumn
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorColumn
Inherits: DisposableWrapper
Implements: ITextEditorSelectable

  PROPERTIES:
    TextEditorLocation EndOfText { get; }
    double Height { get; set; }
    TextEditorLocation StartOfText { get; }

--------------------------------------------------------------------------------

[CLASS] TextEditorColumns
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorColumns
Inherits: DisposableWrapper

  PROPERTIES:
    bool AutoHeight { get; set; }
    int Count { get; set; }
    double Gutter { get; set; }
    bool IsFlowReversed { get; set; }
    TextEditorColumn Item { get; }
    int MaxCount { get; }
    double TotalWidth { get; set; }
    ColumnType Type { get; set; }
    double Width { get; set; }

--------------------------------------------------------------------------------

[CLASS] TextEditorCursor
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorCursor
Inherits: TextEditorSelectionbase

  PROPERTIES:
    TextEditorColumn Column { get; }
    TextEditorLocation Location { get; set; }
    TextEditorParagraph Paragraph { get; }

--------------------------------------------------------------------------------

[CLASS] TextEditorLocation
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorLocation
Inherits: DisposableWrapper

--------------------------------------------------------------------------------

[CLASS] TextEditorParagraph
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorParagraph
Inherits: DisposableWrapper
Implements: ITextEditorSelectable

  PROPERTIES:
    AlignmentType Alignment { get; set; }
    TextEditorLocation EndOfText { get; }
    double FirstIndent { get; set; }
    double LeftIndent { get; set; }
    double LineSpaceFactor { get; set; }
    LineSpacingStyle LineSpaceStyle { get; set; }
    double MaxLineSpacingFactor { get; }
    double MaxSpacingValue { get; }
    double MinLineSpacingFactor { get; }
    double MinSpacingValue { get; }
    NumberingType ParaNumberingType { get; set; }
    double RightIndent { get; set; }
    double SpaceAfter { get; set; }
    double SpaceBefore { get; set; }
    TextEditorLocation StartOfText { get; }
    int TabsCount { get; }

  METHODS:
    void AddTab(TextEditorParagraphTab tab)
    void ContinueNumbering()
    TextEditorParagraphTab GetTab(int idx)
    void RemoveTab(TextEditorParagraphTab tab)
    void RestartNumbering()

--------------------------------------------------------------------------------

[STRUCT] TextEditorParagraphTab
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorParagraphTab

  CONSTRUCTORS:
    new TextEditorParagraphTab()

  PROPERTIES:
    char DecimalChar { get; set; }
    double Offset { get; set; }
    ParagraphTabType Type { get; set; }

--------------------------------------------------------------------------------

[CLASS] TextEditorSelection
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorSelection
Inherits: TextEditorSelectionbase

  PROPERTIES:
    bool CanChangeCase { get; }
    bool CanStack { get; }
    bool CanUnStack { get; }
    Field FieldObject { get; }
    TextEditorParagraph[] Paragraphs { get; }
    string SelectionString { get; }
    bool SingleFieldSelected { get; }
    bool SingleStackSelected { get; }
    TextEditorStack StackSettings { get; set; }

  METHODS:
    bool CanSupportFont(Font font)
    bool CanSupportLanguage(int charset)
    bool ChangeToLowercase()
    bool ChangeToUppercase()
    void CombineParagraphs()
    void ConvertToPlainText()
    void RemoveAllFormatting()
    void RemoveCharacterFormatting()
    void RemoveParagraphFormatting()
    void Stack()
    void UnStack()
    void UpdateField()

--------------------------------------------------------------------------------

[CLASS] TextEditorSelectionbase
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorSelectionbase
Inherits: DisposableWrapper

  PROPERTIES:
    bool Bold { get; set; }
    Color Color { get; set; }
    FlowAlign FlowAlignment { get; set; }
    Font Font { get; set; }
    double Height { get; set; }
    bool Italic { get; set; }
    int Language { get; set; }
    static double MaxObliqueAngle { get; }
    static double MaxTrackingFactor { get; }
    static double MaxWidthScale { get; }
    static double MinObliqueAngle { get; }
    static double MinTrackingFactor { get; }
    static double MinWidthScale { get; }
    double ObliqueAngle { get; set; }
    bool Overline { get; set; }
    bool Strikethrough { get; set; }
    double TrackingFactor { get; set; }
    bool Underline { get; set; }
    double WidthScale { get; set; }

  METHODS:
    void InputSpecialChar(char ch)
    void InsertColumnBreak()
    void InsertImportedText(InsertTextType type, byte[] data)
    void InsertString(string string)
    void InsertSymbol(char ch, int langId)

--------------------------------------------------------------------------------

[STRUCT] TextEditorStack
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorStack

  CONSTRUCTORS:
    new TextEditorStack()

  PROPERTIES:
    string Bottom { get; set; }
    char DecimalChar { get; set; }
    FlowAlign FlowAlign { get; set; }
    static double MaxStackScale { get; }
    static double MinStackScale { get; }
    double Scale { get; set; }
    string Top { get; set; }
    StackType Type { get; set; }

--------------------------------------------------------------------------------

[CLASS] TextEditorWipeout
Full Name: Autodesk.AutoCAD.DatabaseServices.TextEditorWipeout
Inherits: DisposableWrapper

  PROPERTIES:
    Color BackgroundFillColor { get; set; }
    bool BackgroundFillEnabled { get; set; }
    double BackgroundScaleFactor { get; set; }
    static double MaxBackgroundScaleFactor { get; }
    static double MinBackgroundScaleFactor { get; }
    bool UseBackgroundColor { get; set; }

--------------------------------------------------------------------------------

[ENUM] TextHorizontalMode
Full Name: Autodesk.AutoCAD.DatabaseServices.TextHorizontalMode
Inherits: Enum

  Values:
    - TextLeft = 0
    - TextCenter = 1
    - TextRight = 2
    - TextAlign = 3
    - TextMid = 4
    - TextFit = 5

--------------------------------------------------------------------------------

[CLASS] TextStyleTable
Full Name: Autodesk.AutoCAD.DatabaseServices.TextStyleTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] TextStyleTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.TextStyleTableRecord
Inherits: SymbolTableRecord

  CONSTRUCTORS:
    new TextStyleTableRecord()

  PROPERTIES:
    string BigFontFileName { get; set; }
    string FileName { get; set; }
    byte FlagBits { get; set; }
    FontDescriptor Font { get; set; }
    bool IsShapeFile { get; set; }
    bool IsVertical { get; set; }
    double ObliquingAngle { get; set; }
    double PriorSize { get; set; }
    double TextSize { get; set; }
    double XScale { get; set; }

--------------------------------------------------------------------------------

[ENUM] TextVerticalMode
Full Name: Autodesk.AutoCAD.DatabaseServices.TextVerticalMode
Inherits: Enum

  Values:
    - TextBase = 0
    - TextBottom = 1
    - TextVerticalMid = 2
    - TextTop = 3

--------------------------------------------------------------------------------

[CLASS] ThreePointAngleConstraint
Full Name: Autodesk.AutoCAD.DatabaseServices.ThreePointAngleConstraint
Inherits: AngularConstraint

  CONSTRUCTORS:
    new ThreePointAngleConstraint(AngularSectorType type)
    new ThreePointAngleConstraint()

--------------------------------------------------------------------------------

[ENUM] TimeZone
Full Name: Autodesk.AutoCAD.DatabaseServices.TimeZone
Inherits: Enum

  Values:
    - UTC = 0
    - GMT = 1
    - Monrovia = 2
    - Berlin = 1000
    - Paris = 1001
    - Prague = 1002
    - Sarajevo = 1003
    - WestCentralAfrica = 1004
    - Athens = 2000
    - EasternEurope = 2001
    - Cairo = 2002
    - Harare = 2003
    - Helsinki = 2004
    - Jerusalem = 2005
    - Moscow = 3000
    - Riyadh = 3001
    - Baghdad = 3002
    - EastAfrica = 3003
    - Tehran = 3300
    - AbuDhabi = 4000
    - Caucasus = 4001
    - Kabul = 4300
    - Ekaterinburg = 5000
    - Islamabad = 5001
    - Kolkata = 5300
    - Kathmandu = 5450
    - Almaty = 6000
    - Dhaka = 6001
    - SriLanka = 6002
    - Rangoon = 6300
    - Bangkok = 7000
    - Krasnoyarsk = 7001
    - Beijing = 8000
    - Singapore = 8001
    - Taipei = 8002
    - Irkutsk = 8003
    - Perth = 8004
    - Tokyo = 9000
    - Seoul = 9001
    - Yakutsk = 9002
    - Adelaide = 9300
    - Darwin = 9301
    - Sydney = 10000
    - Guam = 10001
    - Brisbane = 10002
    - Hobart = 10003
    - Vladivostock = 10004
    - Magadan = 11000
    - Wellington = 12000
    - Fiji = 12001
    - Tonga = 13000
    - InternationalDateLine = -12000
    - MidwayIsland = -11000
    - Hawaii = -10000
    - Alaska = -9000
    - Pacific = -8000
    - Mazatlan = -7002
    - Arizona = -7001
    - Mountain = -7000
    - Saskatchewan = -6003
    - MexicoCity = -6002
    - CentralAmerica = -6001
    - Central = -6000
    - Bogota = -5002
    - Indiana = -5001
    - Eastern = -5000
    - Santiago = -4002
    - Caracas = -4001
    - AtlanticCanada = -4000
    - Newfoundland = -3300
    - Greenland = -3002
    - BuenosAires = -3001
    - Brasilia = -3000
    - MidAtlantic = -2000
    - CapeVerde = -1001
    - Azores = -1000

--------------------------------------------------------------------------------

[CLASS] Trace
Full Name: Autodesk.AutoCAD.DatabaseServices.Trace
Inherits: Entity

  CONSTRUCTORS:
    new Trace(Point3d pointer1, Point3d pointer2, Point3d pointer3, Point3d pointer4)
    new Trace()

  PROPERTIES:
    Vector3d Normal { get; set; }
    double Thickness { get; set; }

  METHODS:
    Point3d GetPointAt(short index)
    void SetPointAt(short index, Point3d pointValue)

--------------------------------------------------------------------------------

[CLASS] Transaction
Full Name: Autodesk.AutoCAD.DatabaseServices.Transaction
Inherits: RXObject

  PROPERTIES:
    int NumberOfOpenedObjects { get; }
    TransactionManager TransactionManager { get; }

  METHODS:
    void Abort()
    void AddNewlyCreatedDBObject(DBObject obj, bool add)
    void Commit()
    DBObjectCollection GetAllObjects()
    DBObject GetObject(ObjectId id, OpenMode mode, bool openErased, bool forceOpenOnLockedLayer)
    DBObject GetObject(ObjectId id, OpenMode mode, bool openErased)
    DBObject GetObject(ObjectId id, OpenMode mode)

--------------------------------------------------------------------------------

[CLASS] TransactionManager
Full Name: Autodesk.AutoCAD.DatabaseServices.TransactionManager
Inherits: RXObject

  PROPERTIES:
    int NumberOfActiveTransactions { get; }
    int NumberOfOpenedObjects { get; }
    Transaction TopTransaction { get; }

  METHODS:
    void AddNewlyCreatedDBObject(DBObject obj, bool add)
    DBObjectCollection GetAllObjects()
    DBObject GetObject(ObjectId id, OpenMode mode, bool openErased, bool forceOpenOnLockedLayer)
    DBObject GetObject(ObjectId id, OpenMode mode, bool openErased)
    DBObject GetObject(ObjectId id, OpenMode mode)
    void QueueForGraphicsFlush()
    OpenCloseTransaction StartOpenCloseTransaction()
    Transaction StartTransaction()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] TransformOverrule
Full Name: Autodesk.AutoCAD.DatabaseServices.TransformOverrule
Inherits: Overrule

  METHODS:
    bool CloneMeForDragging(Entity entity)
    void Explode(Entity entity, DBObjectCollection entitySet)
    Entity GetTransformedCopy(Entity entity, Matrix3d transform)
    bool HideMeForDragging(Entity entity)
    void SetCustomFilter()
    void SetExtensionDictionaryEntryFilter(string entryName)
    void SetIdFilter(ObjectId[] ids)
    void SetNoFilter()
    void SetXDataFilter(string registeredApplicationName)
    void TransformBy(Entity entity, Matrix3d transform)

--------------------------------------------------------------------------------

[STRUCT] TypedValue
Full Name: Autodesk.AutoCAD.DatabaseServices.TypedValue
Implements: IFormattable

  CONSTRUCTORS:
    new TypedValue(int typeCode, object value)
    new TypedValue(int typeCode)

  PROPERTIES:
    short TypeCode { get; }
    object Value { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString(string format, IFormatProvider provider)
    string ToString(IFormatProvider provider)
    string ToString()

--------------------------------------------------------------------------------

[ENUM] TypeOfCoordinates
Full Name: Autodesk.AutoCAD.DatabaseServices.TypeOfCoordinates
Inherits: Enum

  Values:
    - CoordinateTypeUnknown = 0
    - CoordinateTypeLocal = 1
    - CoordinateTypeGrid = 2
    - CoordinateTypeGeographic = 3

--------------------------------------------------------------------------------

[CLASS] UcsTable
Full Name: Autodesk.AutoCAD.DatabaseServices.UcsTable
Inherits: SymbolTable

--------------------------------------------------------------------------------

[CLASS] UcsTableRecord
Full Name: Autodesk.AutoCAD.DatabaseServices.UcsTableRecord
Inherits: SymbolTableRecord

  CONSTRUCTORS:
    new UcsTableRecord()

  PROPERTIES:
    Point3d Origin { get; set; }
    Vector3d XAxis { get; set; }
    Vector3d YAxis { get; set; }

  METHODS:
    Point3d GetUcsBaseOrigin(OrthographicView view)
    void SetUcsBaseOrigin(Point3d origin, OrthographicView view)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] UnderlayDefinition
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayDefinition
Inherits: DBObject

  PROPERTIES:
    string ActiveFileName { get; }
    string ItemName { get; set; }
    bool Loaded { get; }
    string SourceFileName { get; set; }
    UnderlayItem UnderlayItem { get; }

  METHODS:
    static string GetDictionaryKey(Type underlayDefinitionType)
    void Load(string password)
    void SetUnderlayItem(string sourceFileName, string activeFileName, UnderlayItem item)
    void Unload()

--------------------------------------------------------------------------------

[CLASS] UnderlayFile
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayFile
Inherits: DisposableWrapper

  PROPERTIES:
    UnderlayItemCollection Items { get; }

--------------------------------------------------------------------------------

[CLASS] UnderlayHost
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayHost

  PROPERTIES:
    static UnderlayHost DgnDocHost { get; }
    static UnderlayHost DgnHost { get; }
    static UnderlayHost DwfHost { get; }
    static UnderlayHost PdfHost { get; }

  METHODS:
    UnderlayFile GetFile(string path, string password)

--------------------------------------------------------------------------------

[CLASS] UnderlayItem
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayItem
Inherits: DisposableWrapper

  PROPERTIES:
    Extents2d Extents { get; }
    string Name { get; }
        [SKIPPED PROPERTY] Thumbnail: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    UnitsValue Units { get; }
    bool UsingPartialContent { get; }

--------------------------------------------------------------------------------

[CLASS] UnderlayItemCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayItemCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    UnderlayItem Item { get; }

  METHODS:
    void CopyTo(UnderlayItem[] array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[CLASS] UnderlayLayer
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayLayer
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new UnderlayLayer()

  PROPERTIES:
    string Name { get; }
    UnderlayLayerState State { get; set; }

--------------------------------------------------------------------------------

[CLASS] UnderlayLayerCollection
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayLayerCollection
Implements: ICollection, IEnumerable

  PROPERTIES:
    int Count { get; }
    UnderlayLayer Item { get; set; }

  METHODS:
    void CopyTo(UnderlayLayer[] array, int index)
    IEnumerator GetEnumerator()

--------------------------------------------------------------------------------

[ENUM] UnderlayLayerState
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayLayerState
Inherits: Enum

  Values:
    - Off = 0
    - On = 1

--------------------------------------------------------------------------------

[ABSTRACT CLASS] UnderlayReference
Full Name: Autodesk.AutoCAD.DatabaseServices.UnderlayReference
Inherits: Entity

  PROPERTIES:
    bool AdjustColorForBackground { get; set; }
    int Contrast { get; set; }
    static int ContrastLowerLimit { get; }
    static int ContrastUpperLimit { get; }
    static int DefaultContrast { get; }
    static int DefaultFade { get; }
    ObjectId DefinitionId { get; set; }
    int Fade { get; set; }
    static int FadeLowerLimit { get; }
    static int FadeUpperLimit { get; }
    double Height { get; set; }
    bool IsClipInverted { get; set; }
    bool IsClipped { get; set; }
    bool IsOn { get; set; }
    bool Monochrome { get; set; }
    string Name { get; set; }
    string NameOfSheet { get; set; }
    Vector3d Normal { get; set; }
    string Path { get; set; }
    Point3d Position { get; set; }
    double Rotation { get; set; }
    Scale3d ScaleFactors { get; set; }
    Matrix3d Transform { get; set; }
    UnderlayLayerCollection UnderlayLayerCollection { get; }
    double Width { get; set; }

  METHODS:
    void GenerateClipBoundaryFromPline(ObjectId polyId)
    Point2d[] GetClipBoundary()
    void SetClipBoundary(Point2d[] points)

--------------------------------------------------------------------------------

[ENUM] Unit
Full Name: Autodesk.AutoCAD.DatabaseServices.Unit
Inherits: Enum

  Values:
    - None = 0
    - Millimeter = 1
    - Centimeter = 2
    - Meter = 3
    - Kilometer = 4
    - Inch = 5
    - Foot = 6
    - Yard = 7
    - Mile = 8
    - Microinches = 9
    - Mils = 10
    - Angstroms = 11
    - Nanometers = 12
    - Microns = 13
    - Decimeters = 14
    - Dekameters = 15
    - Hectometers = 16
    - GigaMeters = 17
    - Astronomical = 18
    - LightYears = 19
    - Parsecs = 20

--------------------------------------------------------------------------------

[CLASS] UnitsConverter
Full Name: Autodesk.AutoCAD.DatabaseServices.UnitsConverter
Inherits: TypeConverter

  CONSTRUCTORS:
    new UnitsConverter()

  METHODS:
    bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    static double GetConversionFactor(UnitsValue from, UnitsValue to)

--------------------------------------------------------------------------------

[ENUM] UnitsValue
Full Name: Autodesk.AutoCAD.DatabaseServices.UnitsValue
Inherits: Enum

  Values:
    - Undefined = 0
    - Inches = 1
    - Feet = 2
    - Miles = 3
    - Millimeters = 4
    - Centimeters = 5
    - Meters = 6
    - Kilometers = 7
    - MicroInches = 8
    - Mils = 9
    - Yards = 10
    - Angstroms = 11
    - Nanometers = 12
    - Microns = 13
    - Decimeters = 14
    - Dekameters = 15
    - Hectometers = 16
    - Gigameters = 17
    - Astronomical = 18
    - LightYears = 19
    - Parsecs = 20
    - USSurveyFeet = 21
    - USSurveyInch = 22
    - USSurveyYard = 23
    - USSurveyMile = 24

--------------------------------------------------------------------------------

[ENUM] UnitType
Full Name: Autodesk.AutoCAD.DatabaseServices.UnitType
Inherits: Enum

  Values:
    - Unitless = 0
    - Distance = 1
    - Angle = 2
    - Area = 4
    - Volume = 8
    - Currency = 16
    - Percentage = 32
    - AngleNotTransformed = 65536

--------------------------------------------------------------------------------

[CLASS] UnitTypeAttribute
Full Name: Autodesk.AutoCAD.DatabaseServices.UnitTypeAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new UnitTypeAttribute(UnitType flags)

  PROPERTIES:
    UnitType UnitType { get; }

--------------------------------------------------------------------------------

[ENUM] UpdateAction
Full Name: Autodesk.AutoCAD.DatabaseServices.UpdateAction
Inherits: Enum

  Values:
    - None = 0
    - Ignore = 1
    - UpdateRequired = 2

--------------------------------------------------------------------------------

[ENUM] UpdateDirection
Full Name: Autodesk.AutoCAD.DatabaseServices.UpdateDirection
Inherits: Enum

  Values:
    - SourceToData = 1
    - DataToSource = 2

--------------------------------------------------------------------------------

[ENUM] UpdateOption
Full Name: Autodesk.AutoCAD.DatabaseServices.UpdateOption
Inherits: Enum

  Values:
    - None = 0
    - SkipFormat = 131072
    - UpdateRowHeight = 262144
    - UpdateColumnWidth = 524288
    - AllowSourceUpdate = 1048576
    - ForceFullSourceUpdate = 2097152
    - OverwriteContentModifiedAfterUpdate = 4194304
    - OverwriteFormatModifiedAfterUpdate = 8388608
    - ForPreview = 16777216
    - IncludeXrefs = 33554432

--------------------------------------------------------------------------------

