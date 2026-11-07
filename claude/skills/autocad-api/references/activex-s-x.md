# AutoCAD 2027 ActiveX: SubEntity .. XRecord

Generated from the type libraries by scripts/tlb_to_md.py. M = method, P = property.
vla- call: the object is always the first argument; 'name<out T> = pass a quoted symbol the call sets.
A member is listed only on the interface that declares it: walk `Inherits:` (IAcadLine -> IAcadEntity -> IAcadObject).

[INTERFACE] IAcadSubEntity
Object: AcadSubEntity
Doc: AutoCAD SubEntity Interface
  Methods
    M OnModified  (vla-OnModified obj)
  Properties
    P color : IAcadAcCmColor  [get/put]  vla-get-color / vla-put-color  ; Returns the true color of the object.
    P Hyperlinks : IAcadHyperlinks  [get READ-ONLY]  vla-get-Hyperlinks  ; Assigns a hyperlink to an object and displays the hyperlink name or description (if one is specified)
    P Layer : BSTR  [get READ-ONLY]  vla-get-Layer  ; Specifies the current layer of the object
    P Linetype : BSTR  [get READ-ONLY]  vla-get-Linetype  ; Specifies the current linetype of the object
    P LinetypeScale : ACAD_NOUNITS  [get READ-ONLY]  vla-get-LinetypeScale  ; Specifies the linetype scale factor of the object
    P Lineweight : ACAD_LWEIGHT  [get READ-ONLY]  vla-get-Lineweight  ; Specifies the lineweight for the object
    P ObjectName : BSTR  [get READ-ONLY]  vla-get-ObjectName  ; Gets the AutoCAD class name of the object
    P PlotStyleName : BSTR  [get READ-ONLY]  vla-get-PlotStyleName  ; Specifies the plotstyle name for the object

[INTERFACE] IAcadSubEntSolidEdge
Object: AcadSubEntSolidEdge
Inherits: IAcadSubEntity
Doc: AutoCAD Interface for a edge on an AcDb3dSolid

[INTERFACE] IAcadSubEntSolidFace
Object: AcadSubEntSolidFace
Inherits: IAcadSubEntity
Doc: AutoCAD Interface for a face on an AcDb3dSolid
  Properties
    P Material : BSTR  [get/put]  vla-get-Material / vla-put-Material  ; Specifies the material

[INTERFACE] IAcadSubEntSolidNode
Object: AcadSubEntSolidNode
Inherits: IAcadSubEntity
Doc: AutoCAD Interface for an historical node on an AcDb3dSolid

[INTERFACE] IAcadSubEntSolidVertex
Object: AcadSubEntSolidVertex
Inherits: IAcadSubEntity
Doc: AutoCAD Interface for a vertex on an AcDb3dSolid

[INTERFACE] IAcadSummaryInfo
Object: AcadSummaryInfo
Doc: IAcadSummaryInfo Interface
  Methods
    M AddCustomInfo  (vla-AddCustomInfo obj key<BSTR> Value<BSTR>)  ; Adds a new custom field at the end of the existing list of custom fields.
    M GetCustomByIndex  (vla-GetCustomByIndex obj Index<long> 'pKey<out BSTR> 'pValue<out BSTR>)  ; Gets the key and value of the custom field specified its index.
    M GetCustomByKey  (vla-GetCustomByKey obj key<BSTR> 'pValue<out BSTR>)  ; Returns the number of custom information fields that have been set.
    M NumCustomInfo  (vla-NumCustomInfo obj) -> long  ; Returns the number of custom information fields that have been set.
    M RemoveCustomByIndex  (vla-RemoveCustomByIndex obj Index<long>)  ; Removes a custom field (key and value) indicated by the index. Note, index range is from one to the number of custom fields.
    M RemoveCustomByKey  (vla-RemoveCustomByKey obj key<BSTR>)  ; Removes a custom field (key and value) indicated by the key.
    M SetCustomByIndex  (vla-SetCustomByIndex obj Index<long> key<BSTR> Value<BSTR>)  ; Sets set the key and value of the custom field specified by the index. Note, index range is from one to the number of custom fields.
    M SetCustomByKey  (vla-SetCustomByKey obj key<BSTR> Value<BSTR>)  ; Sets set the value of the custom field specified by the key.
  Properties
    P Author : BSTR  [get/put]  vla-get-Author / vla-put-Author  ; Returns the value of the author field.
    P Comments : BSTR  [get/put]  vla-get-Comments / vla-put-Comments  ; Returns the value of the comments field.
    P HyperlinkBase : BSTR  [get/put]  vla-get-HyperlinkBase / vla-put-HyperlinkBase  ; Returns the value of the hyperlink base path field.
    P Keywords : BSTR  [get/put]  vla-get-Keywords / vla-put-Keywords  ; Returns the value of the keywords field.
    P LastSavedBy : BSTR  [get/put]  vla-get-LastSavedBy / vla-put-LastSavedBy  ; Returns the login name of the user to last save this database by using the LOGINNAME sysvar, or NULL if it has not been set.
    P RevisionNumber : BSTR  [get/put]  vla-get-RevisionNumber / vla-put-RevisionNumber  ; Returns the value of the revision number field, which is a string.
    P Subject : BSTR  [get/put]  vla-get-Subject / vla-put-Subject  ; Returns the value of the subject field.
    P Title : BSTR  [get/put]  vla-get-Title / vla-put-Title  ; Returns the value of the title field.

[INTERFACE] IAcadSurface
Object: AcadSurface
Inherits: IAcadEntity
Doc: IAcadSurface Interface
  Properties
    P EdgeExtensionDistances : VARIANT  [get/put]  vla-get-EdgeExtensionDistances / vla-put-EdgeExtensionDistances  ; Indicates the extension distances of the edges
    P MaintainAssociativity : long  [get/put]  vla-get-MaintainAssociativity / vla-put-MaintainAssociativity  ; Indicates if the surface is associated with another surface and also allows you to turn off associativity
    P ShowAssociativity : VARIANT_BOOL  [get/put]  vla-get-ShowAssociativity / vla-put-ShowAssociativity  ; Higlights dependent surfaces
    P SurfaceType : BSTR  [get READ-ONLY]  vla-get-SurfaceType  ; Indicates the type of surface
    P SurfTrimAssociativity : VARIANT  [get/put]  vla-get-SurfTrimAssociativity / vla-put-SurfTrimAssociativity  ; Specifies whether or not the Mtext is annotative
    P UIsolineDensity : long  [get/put]  vla-get-UIsolineDensity / vla-put-UIsolineDensity  ; Specifies the number of U isolines that are displayed
    P VIsolineDensity : long  [get/put]  vla-get-VIsolineDensity / vla-put-VIsolineDensity  ; Specifies the number of V isolines that are displayed
    P WireframeType : AcWireframeType  [get/put]  vla-get-WireframeType / vla-put-WireframeType  ; Specifies the wireframe type of the selected surface

[INTERFACE] IAcadSweptSurface
Object: AcadSweptSurface
Inherits: IAcadSurface
Doc: IAcadSweptSurface Interface
  Properties
    P Bank : VARIANT_BOOL  [get/put]  vla-get-Bank / vla-put-Bank  ; Specifies whether or not the profile curve twists and rotates along a 3D path
    P Length : double  [get READ-ONLY]  vla-get-Length  ; Specifies the length of the sweep path
    P ProfileRotation : ACAD_ANGLE  [get/put]  vla-get-ProfileRotation / vla-put-ProfileRotation  ; Specifies the rotation of the sweep profile
    P scale : double  [get/put]  vla-get-scale / vla-put-scale  ; Specifies the scale factor from start to the end of the sweep path
    P Twist : ACAD_ANGLE  [get/put]  vla-get-Twist / vla-put-Twist  ; Specifies the amount of rotation along the entire length of the sweep path

[INTERFACE] IAcadTable
Object: AcadTable
Inherits: IAcadEntity
Doc: IAcadTable Interface
  Methods
    M ClearSubSelection  (vla-ClearSubSelection obj)  ; ClearSubSelection.
    M ClearTableStyleOverrides  (vla-ClearTableStyleOverrides obj flag<long>)  ; Clears the tableStyleOverrides.
    M CreateContent  (vla-CreateContent obj nRow<long> nCol<long> nIndex<long>) -> long  ; Creates new content in a cell
    M DeleteCellContent  (vla-DeleteCellContent obj row<long> col<long>)  ; Deletes the cell content for the specified row and coluumn.
    M DeleteColumns  (vla-DeleteColumns obj col<long> cols<long>)  ; deletes the column(s) from the specified column index.
    M DeleteContent  (vla-DeleteContent obj nRow<long> nCol<long>)  ; Deletes a content from a cell
    M DeleteRows  (vla-DeleteRows obj row<long> Rows<long>)  ; deletes the row(s) from the specified row index.
    M EnableMergeAll  (vla-EnableMergeAll obj nRow<long> nCol<long> bEnable<VARIANT_BOOL>)  ; Enables or disables the merge all flag in row or column.
    M FormatValue  (vla-FormatValue obj row<long> col<long> nOption<AcFormatOption> pVal<BSTR*>)  ; Gets the formatted text string for the specified row and column.
    M GenerateLayout  (vla-GenerateLayout obj)  ; Generate layout.
    M GetAlignment  (vla-GetAlignment obj rowType<AcRowType>) -> AcCellAlignment  ; Returns the cell alignment for the specified row type.
    M GetAttachmentPoint  (vla-GetAttachmentPoint obj row<long> col<long>) -> VARIANT  ; Gets the attachment point for the specified row and column.
    M GetAutoScale  (vla-GetAutoScale obj row<long> col<long>) -> VARIANT_BOOL  ; Returns the auto scale flag value for the specified row and column.
    M GetAutoScale2  (vla-GetAutoScale2 obj nRow<long> nCol<long> nContent<long>) -> VARIANT_BOOL  ; Returns the auto scale flag value for the specified row and column  and nContent.
    M GetBackgroundColor  (vla-GetBackgroundColor obj rowType<AcRowType>) -> IAcadAcCmColor  ; Returns the background true color value for the specified row type.
    M GetBackgroundColorNone  (vla-GetBackgroundColorNone obj rowType<AcRowType>) -> VARIANT_BOOL  ; Returns the backgroundColorNone flag value for the specified row type.
    M GetBlockAttributeValue  (vla-GetBlockAttributeValue obj row<long> col<long> attdefId<LONG_PTR>) -> BSTR  ; Returns the attribute value from the Specified block cell for the attribute definition object contained in the block.
    M GetBlockAttributeValue2  (vla-GetBlockAttributeValue2 obj nRow<long> nCol<long> nContent<long> blkId<LONG_PTR>) -> BSTR  ; Returns the attribute value from the Specified block cell for the attribute definition object contained in the block  and nContent.
    M GetBlockRotation  (vla-GetBlockRotation obj row<long> col<long>) -> double  ; Returns the block rotation for the specified row and column.
    M GetBlockScale  (vla-GetBlockScale obj row<long> col<long>) -> double  ; Returns the block scale value for the specified row and column.
    M GetBlockTableRecordId  (vla-GetBlockTableRecordId obj row<long> col<long>) -> LONG_PTR  ; Returns the block table record id associated to the block-type cell.
    M GetBlockTableRecordId2  (vla-GetBlockTableRecordId2 obj nRow<long> nCol<long> nContent<long>) -> LONG_PTR  ; Gets the block table record id associated to the block-type cell  and nContent.
    M GetBreakHeight  (vla-GetBreakHeight obj nIndex<long>) -> double  ; Returns the break height of the specified table when table breaking is enabled.
    M GetCellAlignment  (vla-GetCellAlignment obj row<long> col<long>) -> AcCellAlignment  ; Returns the alignment for the specified row and column.
    M GetCellBackgroundColor  (vla-GetCellBackgroundColor obj row<long> col<long>) -> IAcadAcCmColor  ; Returns the background true color value for the specified row and column.
    M GetCellBackgroundColorNone  (vla-GetCellBackgroundColorNone obj row<long> col<long>) -> VARIANT_BOOL  ; Returns the backgroundColorNone flag value for the specified row and column.
    M GetCellContentColor  (vla-GetCellContentColor obj row<long> col<long>) -> IAcadAcCmColor  ; Returns the true color value for the specified row and column.
    M GetCellDataType  (vla-GetCellDataType obj row<long> col<long> 'pDataType<out AcValueDataType> 'pUnitType<out AcValueUnitType>)  ; Gets the cell data type and unit type for the specified row and column.
    M GetCellExtents  (vla-GetCellExtents obj row<long> col<long> bOuterCell<VARIANT_BOOL>) -> VARIANT  ; Gets the cell extents for the specified row and column.
    M GetCellFormat  (vla-GetCellFormat obj row<long> col<long>) -> BSTR  ; Gets the cell format for the specified row and column.
    M GetCellGridColor  (vla-GetCellGridColor obj row<long> col<long> edge<AcCellEdgeMask>) -> IAcadAcCmColor  ; Returns the gridColor value for the given edge of specified row and column.
    M GetCellGridLineWeight  (vla-GetCellGridLineWeight obj row<long> col<long> edge<AcCellEdgeMask>) -> ACAD_LWEIGHT  ; Returns the gridLineWeight value for the given edge of specified row and column.
    M GetCellGridVisibility  (vla-GetCellGridVisibility obj row<long> col<long> edge<AcCellEdgeMask>) -> VARIANT_BOOL  ; Returns the gridVisibility value for the given edge of specified row and column.
    M GetCellState  (vla-GetCellState obj nRow<long> nCol<long>) -> AcCellState  ; Gets the cell state.
    M GetCellStyle  (vla-GetCellStyle obj nRow<long> nCol<long>) -> BSTR  ; Gets the cell style of cell, row, or column.
    M GetCellStyleOverrides  (vla-GetCellStyleOverrides obj row<long> col<long>) -> VARIANT  ; Returns the cellStyleOverrides.
    M GetCellTextHeight  (vla-GetCellTextHeight obj row<long> col<long>) -> double  ; Returns the text height for the specified row and column.
    M GetCellTextStyle  (vla-GetCellTextStyle obj row<long> col<long>) -> BSTR  ; Returns the text style name for the specified row and column.
    M GetCellType  (vla-GetCellType obj row<long> col<long>) -> AcCellType  ; Gets the cell type for the specified row and column.
    M GetCellValue  (vla-GetCellValue obj row<long> col<long>) -> VARIANT  ; Gets the cell value for the specified row and column.
    M GetColumnName  (vla-GetColumnName obj nIndex<long>) -> BSTR  ; Gets the columns name.
    M GetColumnWidth  (vla-GetColumnWidth obj col<long>) -> double  ; Returns the column width for the specified column.
    M GetContentColor  (vla-GetContentColor obj rowType<AcRowType>) -> IAcadAcCmColor  ; Returns the true color value for the specified row type.
    M GetContentColor2  (vla-GetContentColor2 obj nRow<long> nCol<long> nContent<long>) -> IAcadAcCmColor  ; Returns the true color value for the specified row type  and nContent.
    M GetContentLayout  (vla-GetContentLayout obj row<long> col<long>) -> AcCellContentLayout  ; Gets the content layout of the cell.
    M GetContentType  (vla-GetContentType obj nRow<long> nCol<long>) -> AcCellContentType  ; Gets the content type of the content at the specified content index.
    M GetCustomData  (vla-GetCustomData obj nRow<long> nCol<long> szKey<BSTR> 'pData<out VARIANT>)  ; Gets the custom data value set in cell, row, or column.
    M GetDataFormat  (vla-GetDataFormat obj nRow<long> nCol<long> nContent<long>) -> BSTR  ; Gets the cell format for the specified row and column and nContent.
    M GetDataType  (vla-GetDataType obj rowType<AcRowType> 'pDataType<out AcValueDataType> 'pUnitType<out AcValueUnitType>)  ; Gets the row data type and unit type for the specified row type.
    M GetDataType2  (vla-GetDataType2 obj nRow<long> nCol<long> nContent<long> 'pDataType<out AcValueDataType> 'pUnitType<out AcValueUnitType>)  ; Gets the row data type and unit type for the specified row type  and nContent.
    M GetFieldId  (vla-GetFieldId obj row<long> col<long>) -> LONG_PTR  ; Returns the field object id associated to the specifed cell.
    M GetFieldId2  (vla-GetFieldId2 obj nRow<long> nCol<long> nContent<long>) -> LONG_PTR  ; Returns the field object id associated to the specifed cell  and nContent.
    M GetFormat  (vla-GetFormat obj rowType<AcRowType>) -> BSTR  ; Gets the format for the specified row type.
    M GetFormula  (vla-GetFormula obj nRow<long> nCol<long> nContent<long>) -> BSTR  ; Gets the formula if the content at the specified content index has a formula.
    M GetGridColor  (vla-GetGridColor obj gridLineType<AcGridLineType> rowType<AcRowType>) -> IAcadAcCmColor  ; Returns the gridColor value for the specified gridLineType and row type.
    M GetGridColor2  (vla-GetGridColor2 obj nRow<long> nCol<long> nGridLineType<AcGridLineType>) -> IAcadAcCmColor  ; Returns the gridColor value for the specified gridLineType and row type  and nContent.
    M GetGridDoubleLineSpacing  (vla-GetGridDoubleLineSpacing obj nRow<long> nCol<long> nGridLineType<AcGridLineType>) -> double  ; Gets the grid double line spacing from cell, row, or column
    M GetGridLineStyle  (vla-GetGridLineStyle obj nRow<long> nCol<long> nGridLineType<AcGridLineType>) -> AcGridLineStyle  ; Gets the grid line style of cell, row, or column.
    M GetGridLinetype  (vla-GetGridLinetype obj nRow<long> nCol<long> nGridLineType<AcGridLineType>) -> LONG_PTR  ; Gets the grid line type of cell, row, or column.
    M GetGridLineWeight  (vla-GetGridLineWeight obj gridLineType<AcGridLineType> rowType<AcRowType>) -> ACAD_LWEIGHT  ; Returns the gridLineWeight value for the specified gridLineType and row type.
    M GetGridLineWeight2  (vla-GetGridLineWeight2 obj nRow<long> nCol<long> nGridLineType<AcGridLineType>) -> ACAD_LWEIGHT  ; Gets the gridLineWeight value for the specified gridLineType(s) and row type(s)   and nContent.
    M GetGridVisibility  (vla-GetGridVisibility obj gridLineType<AcGridLineType> rowType<AcRowType>) -> VARIANT_BOOL  ; Returns the gridVisibility value for the specified gridLineType and row type.
    M GetGridVisibility2  (vla-GetGridVisibility2 obj nRow<long> nCol<long> nGridLineType<AcGridLineType>) -> VARIANT_BOOL  ; Returns the gridVisibility value for the specified gridLineType and row type.
    M GetHasFormula  (vla-GetHasFormula obj nRow<long> nCol<long> nContent<long>) -> VARIANT_BOOL  ; Returns true if the content at the specified index is a formula.
    M GetMargin  (vla-GetMargin obj nRow<long> nCol<long> nMargin<AcCellMargin>) -> double  ; Gets the margin of cell, row, or column.
    M GetMinimumColumnWidth  (vla-GetMinimumColumnWidth obj col<long>) -> double  ; Gets the minimum column width for the specified column.
    M GetMinimumRowHeight  (vla-GetMinimumRowHeight obj row<long>) -> double  ; Gets the minimum row height for the specified row.
    M GetOverride  (vla-GetOverride obj nRow<long> nCol<long> nContent<long>) -> AcCellProperty  ; Gets the override in cell, row, column, or content.
    M GetRotation  (vla-GetRotation obj nRow<long> nCol<long> nContent<long>) -> double  ; Gets the rotation angle of the content at the specified content index.
    M GetRowHeight  (vla-GetRowHeight obj row<long>) -> double  ; Returns the row height for the specified row.
    M GetRowType  (vla-GetRowType obj row<long>) -> AcRowType  ; Gets the row type for the specified row.
    M GetScale  (vla-GetScale obj nRow<long> nCol<long> nContent<long>) -> double  ; Gets the scale of the content at the specified content index.
    M GetSubSelection  (vla-GetSubSelection obj 'rowMin<out long> 'rowMax<out long> 'colMin<out long> 'colMax<out long>)  ; GetSubSelection.
    M GetText  (vla-GetText obj row<long> col<long>) -> BSTR  ; Returns the text value value for the specified row and column.
    M GetTextHeight  (vla-GetTextHeight obj rowType<AcRowType>) -> double  ; Returns the text height for the specified row type.
    M GetTextHeight2  (vla-GetTextHeight2 obj nRow<long> nCol<long> nContent<long>) -> double  ; Returns the text height for the specified row and column  and nContent.
    M GetTextRotation  (vla-GetTextRotation obj row<long> col<long>) -> AcRotationAngle  ; Returns the text rotation for the specified row and column.
    M GetTextString  (vla-GetTextString obj nRow<long> nCol<long> nContent<long>) -> BSTR  ; Gets the text value value for the specified row and column  and nContent.
    M GetTextStyle  (vla-GetTextStyle obj rowType<AcRowType>) -> BSTR  ; Returns the text style name for the specified row type.
    M GetTextStyle2  (vla-GetTextStyle2 obj nRow<long> nCol<long> nContent<long>) -> BSTR  ; Gets the text style name for the specified row and column  and nContent.
    M GetValue  (vla-GetValue obj nRow<long> nCol<long> nContent<long>) -> VARIANT  ; Gets the cell value for the specified row and column and nContent.
    M HitTest  (vla-HitTest obj wpt<VARIANT> wviewVec<VARIANT> 'resultRowIndex<out long> 'resultColumnIndex<out long>) -> VARIANT_BOOL  ; Hit test.
    M InsertColumns  (vla-InsertColumns obj col<long> Width<double> cols<long>)  ; Inserts the column(s) of specified width.
    M InsertColumnsAndInherit  (vla-InsertColumnsAndInherit obj col<long> nInheritFrom<long> nNumCols<long>)  ; Inserts one or more columns at the specified index and inherits the column properties from specified column.
    M InsertRows  (vla-InsertRows obj row<long> Height<double> Rows<long>)  ; Inserts the row(s) of specified height.
    M InsertRowsAndInherit  (vla-InsertRowsAndInherit obj nIndex<long> nInheritFrom<long> nNumRows<long>)  ; Inserts one or more rows at the specified index and inherits the row properties from specified row.
    M IsContentEditable  (vla-IsContentEditable obj nRow<long> nCol<long>) -> VARIANT_BOOL  ; Checks if the content of the specified cell can be modified.
    M IsEmpty  (vla-IsEmpty obj nRow<long> nCol<long>) -> VARIANT_BOOL  ; Checks if the content of the specified cell is empty.
    M IsFormatEditable  (vla-IsFormatEditable obj nRow<long> nCol<long>) -> VARIANT_BOOL  ; Checks if the format of the specified cell can be modified.
    M IsMergeAllEnabled  (vla-IsMergeAllEnabled obj nRow<long> nCol<long>) -> VARIANT_BOOL  ; Returns whether merge all flag is enabled or not in row or column.
    M IsMergedCell  (vla-IsMergedCell obj row<long> col<long> 'minRow<out long> 'maxRow<out long> 'minCol<out long> 'maxCol<out long>) -> VARIANT_BOOL  ; is Merged Cell.
    M MergeCells  (vla-MergeCells obj minRow<long> maxRow<long> minCol<long> maxCol<long>)  ; merge cells.
    M MoveContent  (vla-MoveContent obj nRow<long> nCol<long> nFromIndex<long> nToIndex<long>)  ; Moves a content in a cell from one position to another position within the cell
    M RecomputeTableBlock  (vla-RecomputeTableBlock obj bForceUpdate<VARIANT_BOOL>)  ; Recompute TableBlock.
    M RemoveAllOverrides  (vla-RemoveAllOverrides obj nRow<long> nCol<long>)  ; Removes all the overrides in cell, row, or column.
    M ReselectSubRegion  (vla-ReselectSubRegion obj)  ; Re select SubRegion.
    M ResetCellValue  (vla-ResetCellValue obj row<long> col<long>)  ; Resets the cell value for the specified row and column.
    M Select  (vla-Select obj wpt<VARIANT> wvwVec<VARIANT> wvwxVec<VARIANT> wxaper<double> wyaper<double> allowOutside<VARIANT_BOOL> 'resultRowIndex<out long> 'resultColumnIndex<out long>)  ; Select.
    M SelectSubRegion  (vla-SelectSubRegion obj wpt1<VARIANT> wpt2<VARIANT> wvwVec<VARIANT> wvwxVec<VARIANT> seltype<AcSelectType> bIncludeCurrentSelection<VARIANT_BOOL> 'rowMin<out long> 'rowMax<out long> 'colMin<out long> 'colMax<out long>)  ; Select SubRegion.
    M SetAlignment  (vla-SetAlignment obj rowTypes<long> cellAlignment<AcCellAlignment>)  ; Sets the cell alignment for the specified row types.
    M SetAutoScale  (vla-SetAutoScale obj row<long> col<long> bValue<VARIANT_BOOL*>)  ; Sets the auto scale flag value for the specified row and column.
    M SetAutoScale2  (vla-SetAutoScale2 obj nRow<long> nCol<long> nContent<long> bAutoFit<VARIANT_BOOL>)  ; Sets the auto scale flag value for the specified row and column  and nContent.
    M SetBackgroundColor  (vla-SetBackgroundColor obj rowTypes<long> pColor<IAcadAcCmColor*>)  ; Sets the background true color value for the specified row types.
    M SetBackgroundColorNone  (vla-SetBackgroundColorNone obj rowTypes<long> bValue<VARIANT_BOOL*>)  ; Sets the backgroundColorNone flag value for the specified row types.
    M SetBlockAttributeValue  (vla-SetBlockAttributeValue obj row<long> col<long> attdefId<LONG_PTR> bstrValue<BSTR>)  ; Sets the attribute value to the Specified block cell for the attribute definition object contained in the block.
    M SetBlockAttributeValue2  (vla-SetBlockAttributeValue2 obj nRow<long> nCol<long> nContent<long> blkId<LONG_PTR> Value<BSTR>)  ; Sets the attribute value from the Specified block cell for the attribute definition object contained in the block  and nContent.
    M SetBlockRotation  (vla-SetBlockRotation obj row<long> col<long> blkRotation<double>)  ; Sets the block rotation for the specified row and column.
    M SetBlockScale  (vla-SetBlockScale obj row<long> col<long> blkScale<double>)  ; Sets the block scale value for the specified row and column.
    M SetBlockTableRecordId  (vla-SetBlockTableRecordId obj row<long> col<long> blkId<LONG_PTR> bAutoFit<VARIANT_BOOL>)  ; Sets the block table record id associated to the block-type cell.
    M SetBlockTableRecordId2  (vla-SetBlockTableRecordId2 obj nRow<long> nCol<long> nContent<long> blkId<LONG_PTR> autoFit<VARIANT_BOOL>)  ; Sets the block table record id associated to the block-type cell  and nContent.
    M SetBreakHeight  (vla-SetBreakHeight obj nIndex<long> Height<double>)  ; Sets the break height of the specified table when table breaking is enabled.
    M SetCellAlignment  (vla-SetCellAlignment obj row<long> col<long> cellAlignment<AcCellAlignment>)  ; Sets the cell alignment for the specified row and column.
    M SetCellBackgroundColor  (vla-SetCellBackgroundColor obj row<long> col<long> pColor<IAcadAcCmColor*>)  ; Sets the background true color value for the specified row and column.
    M SetCellBackgroundColorNone  (vla-SetCellBackgroundColorNone obj row<long> col<long> bValue<VARIANT_BOOL*>)  ; Sets the backgroundColorNone flag value for the specified row and column.
    M SetCellContentColor  (vla-SetCellContentColor obj row<long> col<long> pColor<IAcadAcCmColor*>)  ; Sets the true color value for the specified row and column.
    M SetCellDataType  (vla-SetCellDataType obj row<long> col<long> dataType<AcValueDataType> unitType<AcValueUnitType>)  ; Sets the cell data type and unit type for the specified row and column.
    M SetCellFormat  (vla-SetCellFormat obj row<long> col<long> pFormat<BSTR>)  ; Sets the cell format for the specified row and column.
    M SetCellGridColor  (vla-SetCellGridColor obj row<long> col<long> edges<long> pColor<IAcadAcCmColor*>)  ; Sets the gridColor value for the given edges of specified row and column.
    M SetCellGridLineWeight  (vla-SetCellGridLineWeight obj row<long> col<long> edges<long> Lineweight<ACAD_LWEIGHT>)  ; Sets the gridLineWeight value for the given edges of specified row and column.
    M SetCellGridVisibility  (vla-SetCellGridVisibility obj row<long> col<long> edges<long> bValue<VARIANT_BOOL>)  ; Sets the gridVisibility value for the given edges of specified row and column.
    M SetCellState  (vla-SetCellState obj nRow<long> nCol<long> nLock<AcCellState>)  ; Sets the cell state.
    M SetCellStyle  (vla-SetCellStyle obj nRow<long> nCol<long> szCellStyle<BSTR>)  ; Sets the the cell style of cell, row, or column.
    M SetCellTextHeight  (vla-SetCellTextHeight obj row<long> col<long> TextHeight<double>)  ; Sets the text height for the specified row and column.
    M SetCellTextStyle  (vla-SetCellTextStyle obj row<long> col<long> bstrName<BSTR>)  ; Sets the text style name for the specified row and column.
    M SetCellType  (vla-SetCellType obj row<long> col<long> CellType<AcCellType>)  ; Sets the cell type for the specified row and column.
    M SetCellValue  (vla-SetCellValue obj row<long> col<long> val<VARIANT>)  ; Sets the cell value for the specified row and column.
    M SetCellValueFromText  (vla-SetCellValueFromText obj row<long> col<long> val<BSTR> nOption<AcParseOption>)  ; Sets the cell value by parsing the text for the specified row and column.
    M SetColumnName  (vla-SetColumnName obj nIndex<long> Name<BSTR>)  ; Sets the columns name.
    M SetColumnWidth  (vla-SetColumnWidth obj col<long> Width<double>)  ; Sets the column width for the specified column.
    M SetContentColor  (vla-SetContentColor obj rowTypes<long> pColor<IAcadAcCmColor*>)  ; Sets the true color value for the specified row types.
    M SetContentColor2  (vla-SetContentColor2 obj nRow<long> nCol<long> nContent<long> pColor<IAcadAcCmColor*>)  ; Sets the true color value for the specified row type  and nContent.
    M SetContentLayout  (vla-SetContentLayout obj row<long> col<long> nLayout<AcCellContentLayout>)  ; Sets the content layout of the cell.
    M SetCustomData  (vla-SetCustomData obj nRow<long> nCol<long> szKey<BSTR> data<VARIANT>)  ; Sets the custom data value set in cell, row, or column.
    M SetDataFormat  (vla-SetDataFormat obj nRow<long> nCol<long> nContent<long> szFormat<BSTR>)  ; Sets the cell format for the specified row and column and nContent.
    M SetDataType  (vla-SetDataType obj rowTypes<long> dataType<AcValueDataType> unitType<AcValueUnitType>)  ; Sets the row data type and unit type for the specified row type.
    M SetDataType2  (vla-SetDataType2 obj nRow<long> nCol<long> nContent<long> dataType<AcValueDataType> unitType<AcValueUnitType>)  ; Sets the row data type and unit type for the specified row type  and nContent.
    M SetFieldId  (vla-SetFieldId obj row<long> col<long> fieldId<LONG_PTR>)  ; Sets the field object id in the specifed cell.
    M SetFieldId2  (vla-SetFieldId2 obj nRow<long> nCol<long> nContent<long> acDbObjectId<LONG_PTR> nflag<AcCellOption>)  ; Sets  the field object id associated to the specifed cell  and nContent.
    M SetFormat  (vla-SetFormat obj rowTypes<long> pFormat<BSTR>)  ; Sets the format for the specified row type.
    M SetFormula  (vla-SetFormula obj nRow<long> nCol<long> nContent<long> pszFormula<BSTR>)  ; Sets the formula at the specified content index.
    M SetGridColor  (vla-SetGridColor obj gridLineTypes<long> rowTypes<long> pColor<IAcadAcCmColor*>)  ; Sets the gridColor value for the specified gridLineType(s) and row type(s).
    M SetGridColor2  (vla-SetGridColor2 obj nRow<long> nCol<long> nGridLineType<AcGridLineType> pColor<IAcadAcCmColor*>)  ; Sets the gridColor value for the specified gridLineType and row type  and nContent.
    M SetGridDoubleLineSpacing  (vla-SetGridDoubleLineSpacing obj nRow<long> nCol<long> nGridLineType<AcGridLineType> fSpacing<double>)  ; Sets the grid double line spacing in cell, row, or column.
    M SetGridLineStyle  (vla-SetGridLineStyle obj nRow<long> nCol<long> nGridLineTypes<AcGridLineType> nLineStyle<AcGridLineStyle>)  ; Sets the grid line style of cell, row, or column.
    M SetGridLinetype  (vla-SetGridLinetype obj nRow<long> nCol<long> nGridLineType<AcGridLineType> idLinetype<LONG_PTR>)  ; Sets the grid line type of cell, row, or column.
    M SetGridLineWeight  (vla-SetGridLineWeight obj gridLineTypes<long> rowTypes<long> Lineweight<ACAD_LWEIGHT>)  ; Sets the gridLineWeight value for the specified gridLineType(s) and row type(s).
    M SetGridLineWeight2  (vla-SetGridLineWeight2 obj nRow<long> nCol<long> nGridLineType<AcGridLineType> Lineweight<ACAD_LWEIGHT>)  ; Sets the gridLineWeight value for the specified gridLineType(s) and row type(s)   and nContent.
    M SetGridVisibility  (vla-SetGridVisibility obj gridLineTypes<long> rowTypes<long> bValue<VARIANT_BOOL>)  ; Sets the gridVisibility value for the specified gridLineType(s) and row type(s).
    M SetGridVisibility2  (vla-SetGridVisibility2 obj nRow<long> nCol<long> nGridLineType<AcGridLineType> bVisible<VARIANT_BOOL>)  ; Sets the gridVisibility value for the specified gridLineType and row type.
    M SetMargin  (vla-SetMargin obj nRow<long> nCol<long> nMargins<AcCellMargin> fMargin<double>)  ; Sets the margin of cell, row, or column.
    M SetOverride  (vla-SetOverride obj nRow<long> nCol<long> nContent<long> nProp<AcCellProperty>)  ; Sets the override in cell, row, column, or content.
    M SetRotation  (vla-SetRotation obj nRow<long> nCol<long> nContent<long> Value<double>)  ; Sets the rotation angle of the content at the specified content index.
    M SetRowHeight  (vla-SetRowHeight obj row<long> Height<double>)  ; Sets the row height for the specified row.
    M SetScale  (vla-SetScale obj nRow<long> nCol<long> nContent<long> scale<double>)  ; Sets the scale of the content at the specified content index.
    M SetSubSelection  (vla-SetSubSelection obj rowMin<long> rowMax<long> colMin<long> colMax<long>)  ; SetSubSelection.
    M SetText  (vla-SetText obj row<long> col<long> pStr<BSTR>)  ; Sets the text value value for the specified row and column.
    M SetTextHeight  (vla-SetTextHeight obj rowTypes<long> TextHeight<double>)  ; Sets the text height for the specified row types.
    M SetTextHeight2  (vla-SetTextHeight2 obj nRow<long> nCol<long> nContent<long> Height<double>)  ; Gets the text height for the specified row and column  and nContent.
    M SetTextRotation  (vla-SetTextRotation obj row<long> col<long> TextRotation<AcRotationAngle>)  ; Sets the text rotation for the specified row and column.
    M SetTextString  (vla-SetTextString obj nRow<long> nCol<long> nContent<long> Text<BSTR>)  ; Sets the text value value for the specified row and column.
    M SetTextStyle  (vla-SetTextStyle obj rowTypes<long> bstrName<BSTR>)  ; Sets the text style name for the specified row types.
    M SetTextStyle2  (vla-SetTextStyle2 obj nRow<long> nCol<long> nContent<long> bstrStyleName<BSTR>)  ; Sets the text style name for the specified row and column  and nContent.
    M SetToolTip  (vla-SetToolTip obj nRow<long> nCol<long> tip<BSTR>)  ; Sets the tooltip string for cell, row, or column.
    M SetValue  (vla-SetValue obj nRow<long> nCol<long> nContent<long> acValue<VARIANT>)  ; Sets the cell value by parsing the text for the specified row and column and nContent.
    M SetValueFromText  (vla-SetValueFromText obj nRow<long> nCol<long> nContent<long> szText<BSTR> nOption<AcParseOption>)  ; Sets the value of the content at the specified content index.
    M UnmergeCells  (vla-UnmergeCells obj minRow<long> maxRow<long> minCol<long> maxCol<long>)  ; Unmerge cells.
  Properties
    P AllowManualHeights : VARIANT_BOOL  [get/put]  vla-get-AllowManualHeights / vla-put-AllowManualHeights  ; Allows the heights of broken table parts to have their own breaking height. When set to No, all table parts will break at the height of the initial table part.
    P AllowManualPositions : VARIANT_BOOL  [get/put]  vla-get-AllowManualPositions / vla-put-AllowManualPositions  ; When enabled, each table part can be moved outside of the boundary. When not enabled, the table parts are contained within a rectangular boundary.
    P BreaksEnabled : VARIANT_BOOL  [get/put]  vla-get-BreaksEnabled / vla-put-BreaksEnabled  ; Determines whether table breaking is in use. When enabled, the table will break automatically at the current specified height.
    P BreakSpacing : double  [get/put]  vla-get-BreakSpacing / vla-put-BreakSpacing  ; Controls the spacing between the broken table parts. Depending on the break direction this will be a horizontal or vertical spacing.
    P Columns : long  [get/put]  vla-get-Columns / vla-put-Columns  ; Specifies the columns in the Table
    P ColumnWidth : double  [put]  vla-put-ColumnWidth  ; Sets the uniform column width for all the columns in the table.
    P Direction : VARIANT  [get/put]  vla-get-Direction / vla-put-Direction  ; Specifies the direction vector of the table
    P EnableBreak : VARIANT_BOOL  [put]  vla-put-EnableBreak  ; Enables or disables table breaking
    P FlowDirection : AcTableDirection  [get/put]  vla-get-FlowDirection / vla-put-FlowDirection  ; Specifies the Table flow direction
    P HasSubSelection : VARIANT_BOOL  [get READ-ONLY]  vla-get-HasSubSelection  ; HasSubSelection.
    P HeaderSuppressed : VARIANT_BOOL  [get/put]  vla-get-HeaderSuppressed / vla-put-HeaderSuppressed  ; Returns and sets the header suppressed flag value.
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the Table height
    P HorzCellMargin : double  [get/put]  vla-get-HorzCellMargin / vla-put-HorzCellMargin  ; Specifies the horizontal distance between text and edge of cell
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specifies the insertion point of the table
    P MinimumTableHeight : double  [get READ-ONLY]  vla-get-MinimumTableHeight  ; Gets the minimum height for the table.
    P MinimumTableWidth : double  [get READ-ONLY]  vla-get-MinimumTableWidth  ; Gets the minimum width for the table.
    P RegenerateTableSuppressed : VARIANT_BOOL  [get/put]  vla-get-RegenerateTableSuppressed / vla-put-RegenerateTableSuppressed  ; Enables or disables the regeneration of table block
    P RepeatBottomLabels : VARIANT_BOOL  [get/put]  vla-get-RepeatBottomLabels / vla-put-RepeatBottomLabels  ; Determines whether the bottom set of label rows is repeated at the bottom of each broken table part.
    P RepeatTopLabels : VARIANT_BOOL  [get/put]  vla-get-RepeatTopLabels / vla-put-RepeatTopLabels  ; Determines whether the first set of label rows will be repeated at the top of each broken table part.
    P RowHeight : double  [put]  vla-put-RowHeight  ; Sets the uniform row height for all the rows in the table.
    P Rows : long  [get/put]  vla-get-Rows / vla-put-Rows  ; Specifies the rows in the Table
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of the Table
    P TableBreakFlowDirection : AcTableFlowDirection  [get/put]  vla-get-TableBreakFlowDirection / vla-put-TableBreakFlowDirection  ; Determines the direction that parts of the table flow.
    P TableBreakHeight : double  [get/put]  vla-get-TableBreakHeight / vla-put-TableBreakHeight  ; Sets the breaking height for the initial table part and any other table parts that do not have manual heights set.
    P TableStyleOverrides : VARIANT  [get READ-ONLY]  vla-get-TableStyleOverrides  ; Returns the tableStyleOverrides.
    P TitleSuppressed : VARIANT_BOOL  [get/put]  vla-get-TitleSuppressed / vla-put-TitleSuppressed  ; Returns and sets the title suppressed flag value.
    P VertCellMargin : double  [get/put]  vla-get-VertCellMargin / vla-put-VertCellMargin  ; Specifies the vertical distance between text and edge of cell
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the Table width

[INTERFACE] IAcadTableStyle
Object: AcadTableStyle
Inherits: IAcadObject
Doc: AutoCAD IAcadTableStyle Interface
  Methods
    M CreateCellStyle  (vla-CreateCellStyle obj bstrCellStyle<BSTR>)
    M CreateCellStyleFromStyle  (vla-CreateCellStyleFromStyle obj bstrCellStyle<BSTR> bstrSourceCellStyle<BSTR>)
    M DeleteCellStyle  (vla-DeleteCellStyle obj bstrCellStyle<BSTR>)
    M EnableMergeAll  (vla-EnableMergeAll obj bstrCellStyle<BSTR> bEnable<VARIANT_BOOL>)
    M GetAlignment  (vla-GetAlignment obj rowType<AcRowType>) -> AcCellAlignment  ; Returns the cell alignment for the specified row type.
    M GetAlignment2  (vla-GetAlignment2 obj bstrCellStyle<BSTR>) -> AcCellAlignment  ; Returns the cell alignment for the specified cellStyle.
    M GetBackgroundColor  (vla-GetBackgroundColor obj rowType<AcRowType>) -> IAcadAcCmColor  ; Returns the background true color value for the specified row type.
    M GetBackgroundColor2  (vla-GetBackgroundColor2 obj bstrCellStyle<BSTR>) -> IAcadAcCmColor  ; Returns the background true color value for the specified cellStyle.
    M GetBackgroundColorNone  (vla-GetBackgroundColorNone obj rowType<AcRowType>) -> VARIANT_BOOL  ; Returns the backgroundColorNone flag value for the specified row type.
    M GetCellClass  (vla-GetCellClass obj bstrCellStyle<BSTR>) -> long
    M GetCellStyles  (vla-GetCellStyles obj cellStylesArray<VARIANT*>)
    M GetColor  (vla-GetColor obj rowType<AcRowType>) -> IAcadAcCmColor  ; Returns the true color value for the specified row type.
    M GetColor2  (vla-GetColor2 obj bstrCellStyle<BSTR>) -> IAcadAcCmColor  ; Returns the true color value for the specified cellStyle.
    M GetDataType  (vla-GetDataType obj rowType<AcRowType> 'pDataType<out AcValueDataType> 'pUnitType<out AcValueUnitType>)  ; Returns the data type and unit type for the specifed row type.
    M GetDataType2  (vla-GetDataType2 obj bstrCellStyle<BSTR> 'pDataType<out AcValueDataType> 'pUnitType<out AcValueUnitType>)  ; Returns the data type and unit type for the specifed cellStyle.
    M GetFormat  (vla-GetFormat obj rowType<AcRowType>) -> BSTR  ; Returns the format for the specifed row type.
    M GetFormat2  (vla-GetFormat2 obj bstrCellStyle<BSTR> 'pbstrFormat<out BSTR>)  ; Returns the format for the specifed cellStyle.
    M GetGridColor  (vla-GetGridColor obj gridLineType<AcGridLineType> rowType<AcRowType>) -> IAcadAcCmColor  ; Returns the gridColor value for the specifed gridLineType and row type.
    M GetGridColor2  (vla-GetGridColor2 obj bstrCellStyle<BSTR> gridLineType<AcGridLineType>) -> IAcadAcCmColor  ; Returns the gridColor value for the specified gridLineType and cellStyle.
    M GetGridLineWeight  (vla-GetGridLineWeight obj gridLineType<AcGridLineType> rowType<AcRowType>) -> ACAD_LWEIGHT  ; Returns the gridLineWeight value for the specifed gridLineType and row type.
    M GetGridLineWeight2  (vla-GetGridLineWeight2 obj bstrCellStyle<BSTR> gridLineType<AcGridLineType>) -> ACAD_LWEIGHT  ; Gets the gridLineWeight value for the specified gridLineType(s) and cellStyle.
    M GetGridVisibility  (vla-GetGridVisibility obj gridLineType<AcGridLineType> rowType<AcRowType>) -> VARIANT_BOOL  ; Returns the gridVisibility value for the specifed gridLineType and row type.
    M GetGridVisibility2  (vla-GetGridVisibility2 obj bstrCellStyle<BSTR> gridLineType<AcGridLineType>) -> VARIANT_BOOL  ; Returns the gridVisibility value for the specified gridLineType and cellStyle.
    M GetIsCellStyleInUse  (vla-GetIsCellStyleInUse obj pszCellStyle<BSTR>) -> VARIANT_BOOL
    M GetIsMergeAllEnabled  (vla-GetIsMergeAllEnabled obj bstrCellStyle<BSTR>) -> VARIANT_BOOL
    M GetRotation  (vla-GetRotation obj bstrCellStyle<BSTR>) -> double
    M GetTextHeight  (vla-GetTextHeight obj rowType<AcRowType>) -> double  ; Returns the text height for the specified row type.
    M GetTextHeight2  (vla-GetTextHeight2 obj bstrCellStyle<BSTR>) -> double  ; Returns the text height for the specified cellStyle.
    M GetTextStyle  (vla-GetTextStyle obj rowType<AcRowType>) -> BSTR  ; Returns the text style name for the specified row type.
    M GetTextStyleId  (vla-GetTextStyleId obj bstrCellStyle<BSTR>) -> LONG_PTR  ; Returns the text style name for the specified cellStyle.
    M GetUniqueCellStyleName  (vla-GetUniqueCellStyleName obj pszBaseName<BSTR>) -> BSTR
    M RenameCellStyle  (vla-RenameCellStyle obj bstrOldName<BSTR> bstrNewName<BSTR>)
    M SetAlignment  (vla-SetAlignment obj rowTypes<long> cellAlignment<AcCellAlignment>)  ; Sets the cell alignment for the specified row types.
    M SetAlignment2  (vla-SetAlignment2 obj bstrCellStyle<BSTR> cellAlignment<AcCellAlignment>)  ; Sets the cell alignment for the specified cellStyle.
    M SetBackgroundColor  (vla-SetBackgroundColor obj rowTypes<long> pColor<IAcadAcCmColor*>)  ; Sets the background true color value for the specifed row types.
    M SetBackgroundColor2  (vla-SetBackgroundColor2 obj bstrCellStyle<BSTR> color<IAcadAcCmColor*>)  ; Sets the background true color value for the specified cellStyle.
    M SetBackgroundColorNone  (vla-SetBackgroundColorNone obj rowTypes<long> bValue<VARIANT_BOOL*>)  ; Sets the backgroundColorNone flag value for the specified row types.
    M SetCellClass  (vla-SetCellClass obj bstrCellStyle<BSTR> cellClass<long>)
    M SetColor  (vla-SetColor obj rowTypes<long> pColor<IAcadAcCmColor*>)  ; Sets the true color value for the specifed row types.
    M SetColor2  (vla-SetColor2 obj bstrCellStyle<BSTR> color<IAcadAcCmColor*>)  ; Sets the true color value for the specified cellStyle.
    M SetDataType  (vla-SetDataType obj rowTypes<long> nDataType<AcValueDataType> nUnitType<AcValueUnitType>)  ; Sets the data type and unit type for the specifed row type.
    M SetDataType2  (vla-SetDataType2 obj bstrCellStyle<BSTR> nDataType<AcValueDataType> nUnitType<AcValueUnitType>)  ; Sets the data type and unit type for the specifed cellStyle.
    M SetFormat  (vla-SetFormat obj rowTypes<long> val<BSTR>)  ; Sets the format for the specifed row type.
    M SetFormat2  (vla-SetFormat2 obj bstrCellStyle<BSTR> bstrFormat<BSTR>)  ; Sets the format for the specifed cellStyle.
    M SetGridColor  (vla-SetGridColor obj gridLineTypes<long> rowTypes<long> pColor<IAcadAcCmColor*>)  ; Sets the gridColor value for the specifed gridLineTypes and row types.
    M SetGridColor2  (vla-SetGridColor2 obj bstrCellStyle<BSTR> gridLineTypes<AcGridLineType> pColor<IAcadAcCmColor*>)  ; Sets the gridColor value for the specified gridLineType and cellStyle.
    M SetGridLineWeight  (vla-SetGridLineWeight obj gridLineTypes<long> rowTypes<long> Lineweight<ACAD_LWEIGHT>)  ; Sets the gridLineWeight value for the specifed gridLineTypes and row types.
    M SetGridLineWeight2  (vla-SetGridLineWeight2 obj bstrCellStyle<BSTR> gridLineTypes<AcGridLineType> Lineweight<ACAD_LWEIGHT>)  ; Sets the gridLineWeight value for the specified gridLineType(s) and cellStyle.
    M SetGridVisibility  (vla-SetGridVisibility obj gridLineTypes<long> rowTypes<long> bValue<VARIANT_BOOL>)  ; Sets the gridVisibility value for the specifed gridLineTypes and row types.
    M SetGridVisibility2  (vla-SetGridVisibility2 obj bstrCellStyle<BSTR> gridLineTypes<AcGridLineType> bValue<VARIANT_BOOL>)  ; Sets the gridVisibility value for the specified gridLineType and cellStyle.
    M SetRotation  (vla-SetRotation obj bstrCellStyle<BSTR> Rotation<double>)
    M SetTemplateId  (vla-SetTemplateId obj val<LONG_PTR> option<AcMergeCellStyleOption>)
    M SetTextHeight  (vla-SetTextHeight obj rowTypes<long> TextHeight<double>)  ; Sets the text height for the specified row types.
    M SetTextHeight2  (vla-SetTextHeight2 obj bstrCellStyle<BSTR> Height<double>)  ; Sets the text height for the specified cellStyle.
    M SetTextStyle  (vla-SetTextStyle obj rowTypes<long> bstrName<BSTR>)  ; Sets the text style name for the specified row types.
    M SetTextStyleId  (vla-SetTextStyleId obj bstrCellStyle<BSTR> val<LONG_PTR>)  ; Sets the text style name for the specified cellStyle.
  Properties
    P BitFlags : long  [get/put]  vla-get-BitFlags / vla-put-BitFlags  ; Returns and sets the bit flag values.
    P Description : BSTR  [get/put]  vla-get-Description / vla-put-Description  ; Returns and sets the description of the tablestyle.
    P FlowDirection : AcTableDirection  [get/put]  vla-get-FlowDirection / vla-put-FlowDirection  ; Returns and sets the table flow direction value.
    P HeaderSuppressed : VARIANT_BOOL  [get/put]  vla-get-HeaderSuppressed / vla-put-HeaderSuppressed  ; Returns and sets the header suppressed flag value.
    P HorzCellMargin : double  [get/put]  vla-get-HorzCellMargin / vla-put-HorzCellMargin  ; Returns and sets the horizontal cell margin value.
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Returns and sets the name of the tablestyle.
    P NumCellStyles : long  [get READ-ONLY]  vla-get-NumCellStyles
    P TemplateId : LONG_PTR  [get/put]  vla-get-TemplateId / vla-put-TemplateId
    P TitleSuppressed : VARIANT_BOOL  [get/put]  vla-get-TitleSuppressed / vla-put-TitleSuppressed  ; Returns and sets the title suppressed flag value.
    P VertCellMargin : double  [get/put]  vla-get-VertCellMargin / vla-put-VertCellMargin  ; Returns and sets the vertical cell margin value.

[INTERFACE] IAcadText
Object: AcadText
Inherits: IAcadEntity
Doc: AutoCAD Text Interface
  Methods
    M FieldCode  (vla-FieldCode obj) -> BSTR  ; Returns the text string with field codes of the text
  Properties
    P Alignment : AcAlignment  [get/put]  vla-get-Alignment / vla-put-Alignment  ; Specifies both text height and text orientation by designating the endpoints of the baseline
    P Backward : VARIANT_BOOL  [get/put]  vla-get-Backward / vla-put-Backward  ; Determines whether the text is backward or not
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the text
    P HorizontalAlignment : AcHorizontalAlignment  [get/put]  vla-get-HorizontalAlignment / vla-put-HorizontalAlignment  ; Specifies the horizontal alignment of the text
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specify the X, Y, Z coordinate of the insertion point of the text
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P ObliqueAngle : ACAD_ANGLE  [get/put]  vla-get-ObliqueAngle / vla-put-ObliqueAngle  ; Specifies the oblique angle of the text
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the text
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the width scale factor of the text
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of the text
    P TextAlignmentPoint : VARIANT  [get/put]  vla-get-TextAlignmentPoint / vla-put-TextAlignmentPoint  ; Specify the X, Y, Z coordinate of the alignment point of the text
    P TextGenerationFlag : long  [get/put]  vla-get-TextGenerationFlag / vla-put-TextGenerationFlag  ; Specifies the attribute text generation flag
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the text
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the text
    P UpsideDown : VARIANT_BOOL  [get/put]  vla-get-UpsideDown / vla-put-UpsideDown  ; Determines whether the text is upside down or not
    P VerticalAlignment : AcVerticalAlignment  [get/put]  vla-get-VerticalAlignment / vla-put-VerticalAlignment  ; Specifies the vertical alignment of the text

[INTERFACE] IAcadTextStyle
Object: AcadTextStyle
Inherits: IAcadObject
Doc: A named, saved collection of settings that determines the appearance of text characters
  Methods
    M GetFont  (vla-GetFont obj 'TypeFace<out BSTR> 'Bold<out VARIANT_BOOL> 'Italic<out VARIANT_BOOL> 'Charset<out long> 'PitchAndFamily<out long>)  ; Gets the definition data of the font for the TextStyle
    M SetFont  (vla-SetFont obj TypeFace<BSTR> Bold<VARIANT_BOOL> Italic<VARIANT_BOOL> Charset<long> PitchAndFamily<long>)  ; Sets the definition data of the font for the TextStyle
  Properties
    P BigFontFile : BSTR  [get/put]  vla-get-BigFontFile / vla-put-BigFontFile  ; Specifies the name of the big font file associated with the text or attribute
    P fontFile : BSTR  [get/put]  vla-get-fontFile / vla-put-fontFile  ; Specifies the primary font file path and name
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P LastHeight : double  [get/put]  vla-get-LastHeight / vla-put-LastHeight  ; Specifies the last text height used
    P Name : BSTR  [get READ-ONLY]  vla-get-Name  ; Specifies the name of the object
    P ObliqueAngle : ACAD_ANGLE  [get/put]  vla-get-ObliqueAngle / vla-put-ObliqueAngle  ; Specifies the oblique angle of the object
    P TextGenerationFlag : long  [get/put]  vla-get-TextGenerationFlag / vla-put-TextGenerationFlag  ; Specifies the attribute text generation flag
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window

[INTERFACE] IAcadTextStyles
Object: AcadTextStyles
Inherits: IAcadObject
Doc: The collection of all text styles in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadTextStyle  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadTextStyle  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadTolerance
Object: AcadTolerance
Inherits: IAcadEntity
Doc: AutoCAD Tolerance Interface
  Properties
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies the color of the dimension lines
    P DirectionVector : VARIANT  [get/put]  vla-get-DirectionVector / vla-put-DirectionVector  ; Specifies the direction for the ray, tolerance, or xline through a vector
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specify the X, Y, Z coordinate for insertion point of the tolerance or use the Pick Point button to set X, Y, Z values simultaneously
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the overall scale factor applied to properties that specify sizes, distances, or offsets
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of the tolerance
    P TextColor : ACAD_COLOR  [get/put]  vla-get-TextColor / vla-put-TextColor  ; Specifies the color of the dimension text
    P TextHeight : double  [get/put]  vla-get-TextHeight / vla-put-TextHeight  ; Specifies the text height of the tolerance
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the tolerance
    P TextStyle : BSTR  [get/put]  vla-get-TextStyle / vla-put-TextStyle  ; Specifies the text style of the tolerance

[INTERFACE] IAcadToolbar
Object: AcadToolbar
Doc: An AutoCAD toolbar
  Methods
    M AddSeparator  (vla-AddSeparator obj Index<VARIANT>) -> IAcadToolbarItem  ; Adds a separator to an existing menu or toolbar
    M AddToolbarButton  (vla-AddToolbarButton obj Index<VARIANT> Name<BSTR> HelpString<BSTR> Macro<BSTR> [FlyoutButton<VARIANT>]) -> IAcadToolbarItem  ; Adds a toolbar item to a toolbar at a specified position
    M Delete  (vla-Delete obj)  ; Deletes a specified object
    M Dock  (vla-Dock obj Side<AcToolbarDockStatus>)  ; Docks the toolbar to the owning frame window
    M Float  (vla-Float obj top<long> left<long> NumberFloatRows<long>)  ; Floats the toolbar
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadToolbarItem  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P DockStatus : AcToolbarDockStatus  [get READ-ONLY]  vla-get-DockStatus  ; Specifies if the toolbar is docked or floating
    P FloatingRows : long  [get/put]  vla-get-FloatingRows / vla-put-FloatingRows  ; Specifies the number of rows for a floating toolbar
    P Height : long  [get READ-ONLY]  vla-get-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P HelpString : BSTR  [get/put]  vla-get-HelpString / vla-put-HelpString  ; Specifies the help string for the toolbar, toolbar item, or menu item
    P LargeButtons : VARIANT_BOOL  [get READ-ONLY]  vla-get-LargeButtons  ; Specifies if the toolbar button is large or small
    P left : long  [get/put]  vla-get-left / vla-put-left  ; Specifies the left edge of a toolbar
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P Parent : IDispatch  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object
    P TagString : BSTR  [get READ-ONLY]  vla-get-TagString  ; Specifies the tag string of the object
    P top : long  [get/put]  vla-get-top / vla-put-top  ; Specifies the top edge of a toolbar
    P Visible : VARIANT_BOOL  [get/put]  vla-get-Visible / vla-put-Visible  ; Specifies the visibility of an object or the application
    P Width : long  [get READ-ONLY]  vla-get-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window

[INTERFACE] IAcadToolbarItem
Object: AcadToolbarItem
Doc: A single button item on an AutoCAD toolbar
  Methods
    M AttachToolbarToFlyout  (vla-AttachToolbarToFlyout obj MenuGroupName<BSTR> ToolbarName<BSTR>)  ; Attaches a toolbar to a toolbar button defined as a flyout
    M Delete  (vla-Delete obj)  ; Deletes a specified object
    M GetBitmaps  (vla-GetBitmaps obj 'SmallIconName<out BSTR> 'LargeIconName<out BSTR>)  ; Gets the large and small bitmaps used as icons for the toolbar item
    M SetBitmaps  (vla-SetBitmaps obj SmallIconName<BSTR> LargeIconName<BSTR>)  ; Sets the large and small bitmaps used as icons for the toolbar item
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P CommandDisplayName : BSTR  [get/put]  vla-get-CommandDisplayName / vla-put-CommandDisplayName
    P Flyout : IAcadToolbar  [get READ-ONLY]  vla-get-Flyout  ; Gets the toolbar associated with a flyout toolbar item
    P HelpString : BSTR  [get/put]  vla-get-HelpString / vla-put-HelpString  ; Specifies the help string for the toolbar, toolbar item, or menu item
    P Index : long  [get READ-ONLY]  vla-get-Index  ; Specifies the index of the menu or toolbar item
    P Macro : BSTR  [get/put]  vla-get-Macro / vla-put-Macro  ; Specifies the macro for the menu or toolbar item
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P Parent : IAcadToolbar  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object
    P TagString : BSTR  [get/put]  vla-get-TagString / vla-put-TagString  ; Specifies the tag string of the object
    P Type : AcToolbarItemType  [get READ-ONLY]  vla-get-Type  ; Specifies type of a Leader, MenuGroup, PopupMenuItem, ToolbarItem, Polyline, or PolygonMesh object

[INTERFACE] IAcadToolbars
Object: AcadToolbars
Doc: A collection of Toolbar objects representing all the toolbars loaded in the current AutoCAD session
  Methods
    M Add  (vla-Add obj ToolbarName<BSTR>) -> IAcadToolbar  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadToolbar  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P LargeButtons : VARIANT_BOOL  [get/put]  vla-get-LargeButtons / vla-put-LargeButtons  ; Specifies if the toolbar button is large or small
    P Parent : IAcadMenuGroup  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object

[INTERFACE] IAcadTrace
Object: AcadTrace
Inherits: IAcadEntity
Doc: AutoCAD Trace Interface
  Properties
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the coordinates of the trace
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the trace

[INTERFACE] IAcadUCS
Object: AcadUCS
Inherits: IAcadObject
Doc: A user-defined coordinate system that determines the orientation of the X, Y, and Z axes in 3D space
  Methods
    M GetUCSMatrix  (vla-GetUCSMatrix obj) -> VARIANT  ; Gets the transformation matrix consisting of UCS coordinate system data
  Properties
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P Origin : VARIANT  [get/put]  vla-get-Origin / vla-put-Origin  ; Specifies the origin of the UCS, block, layout, or raster image in WCS coordinates
    P XVector : VARIANT  [get/put]  vla-get-XVector / vla-put-XVector  ; Specifies the X direction of the given UCS
    P YVector : VARIANT  [get/put]  vla-get-YVector / vla-put-YVector  ; Specifies the Y direction of the given UCS

[INTERFACE] IAcadUCSs
Object: AcadUCSs
Inherits: IAcadObject
Doc: The collection of all user coordinate systems (UCSs) in the drawing
  Methods
    M Add  (vla-Add obj Origin<VARIANT> XAxisPoint<VARIANT> YAxisPoint<VARIANT> Name<BSTR>) -> IAcadUCS  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadUCS  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadUnderlay
Object: AcadDwfUnderlay, AcadDgnUnderlay, AcadPdfUnderlay
Inherits: IAcadEntity
  Methods
    M ClipBoundary  (vla-ClipBoundary obj boundry<VARIANT>)  ; Get/Set clipping boundary
  Properties
    P AdjustForBackground : VARIANT_BOOL  [get/put]  vla-get-AdjustForBackground / vla-put-AdjustForBackground  ; Determines whether the underlay colors are adjusted for the current background color
    P ClippingEnabled : VARIANT_BOOL  [get/put]  vla-get-ClippingEnabled / vla-put-ClippingEnabled  ; Enables or disables the clipping boundary of the underlay
    P Contrast : long  [get/put]  vla-get-Contrast / vla-put-Contrast  ; Specifies the current contrast value of the underlay
    P Fade : long  [get/put]  vla-get-Fade / vla-put-Fade  ; Specifies the current fade value of the underlay
    P File : BSTR  [get/put]  vla-get-File / vla-put-File  ; Specifies the path to the underlay file
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the underlay
    P ItemName : BSTR  [get/put]  vla-get-ItemName / vla-put-ItemName  ; Specifies the item of the underlay file
    P Monochrome : VARIANT_BOOL  [get/put]  vla-get-Monochrome / vla-put-Monochrome  ; Determines whether underlay is monochrome or not
    P Position : VARIANT  [get/put]  vla-get-Position / vla-put-Position  ; Specifies the origin coordinates (lower left corner) of the underlay
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the underlay
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the scale factor of the underlay
    P UnderlayLayerOverrideApplied : AcUnderlayLayerOverrideType  [get/put]  vla-get-UnderlayLayerOverrideApplied / vla-put-UnderlayLayerOverrideApplied  ; Specifies layer override state for the underlay
    P UnderlayName : BSTR  [get/put]  vla-get-UnderlayName / vla-put-UnderlayName  ; Specifies the name of the underlay file
    P UnderlayVisibility : VARIANT_BOOL  [get/put]  vla-get-UnderlayVisibility / vla-put-UnderlayVisibility  ; Determines whether underlay is visible or not
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the underlay

[INTERFACE] IAcadUtility
Object: AcadUtility
Doc: A series of methods provided for utility purposes
  Methods
    M AngleFromXAxis  (vla-AngleFromXAxis obj StartPoint<VARIANT> EndPoint<VARIANT>) -> double  ; Gets the angle of a line from the X axis
    M AngleToReal  (vla-AngleToReal obj Angle<BSTR> Unit<AcAngleUnits>) -> double  ; Converts an angle as a string to a real (double) value
    M AngleToString  (vla-AngleToString obj Angle<double> Unit<AcAngleUnits> precision<long>) -> BSTR  ; Converts an angle from a real (double) value to a string
    M CreateTypedArray  (vla-CreateTypedArray obj 'varArr<out VARIANT> Type<long> inArgs<SAFEARRAY(VARIANT)>)  ; Creates a variant that contains an array of typed arguments
    M DistanceToReal  (vla-DistanceToReal obj Distance<BSTR> Unit<AcUnits>) -> double  ; Converts a distance from a string to a real (double) value
    M GetAngle  (vla-GetAngle obj [Point<VARIANT>] [Prompt<VARIANT>]) -> double  ; Gets the angle specified. Considers the setting of the ANGBASE system variable
    M GetCorner  (vla-GetCorner obj Point<VARIANT> [Prompt<VARIANT>]) -> VARIANT  ; Gets a corner of a rectangle
    M GetDistance  (vla-GetDistance obj [Point<VARIANT>] [Prompt<VARIANT>]) -> double  ; Gets the distance from the prompt line or a selected set of points on the screen
    M GetEntity  (vla-GetEntity obj 'Object<out IDispatch> 'PickedPoint<out VARIANT> [Prompt<VARIANT>])  ; Gets an object interactively
    M GetInput  (vla-GetInput obj) -> BSTR  ; Converts an input string from the user into a keyword index
    M GetInteger  (vla-GetInteger obj [Prompt<VARIANT>]) -> long  ; Gets an integer value from the user
    M GetKeyword  (vla-GetKeyword obj [Prompt<VARIANT>]) -> BSTR  ; Gets a keyword string from the user
    M GetObjectIdString  (vla-GetObjectIdString obj Object<IDispatch*> bHex<VARIANT_BOOL>) -> BSTR
    M GetOrientation  (vla-GetOrientation obj [Point<VARIANT>] [Prompt<VARIANT>]) -> double  ; Gets the angle specified. Ignores the setting of the ANGBASE system variable
    M GetPoint  (vla-GetPoint obj [Point<VARIANT>] [Prompt<VARIANT>]) -> VARIANT  ; Gets the point selected in AutoCAD
    M GetReal  (vla-GetReal obj [Prompt<VARIANT>]) -> double  ; Gets a real (double) value from the user
    M GetRemoteFile  (vla-GetRemoteFile obj URL<BSTR> 'LocalFile<out BSTR> IgnoreCache<VARIANT_BOOL>)  ; Downloads the file specified by a URL
    M GetString  (vla-GetString obj HasSpaces<long> [Prompt<VARIANT>]) -> BSTR  ; Gets a string from the user
    M GetSubEntity  (vla-GetSubEntity obj 'Object<out IDispatch> 'PickedPoint<out VARIANT> 'transMatrix<out VARIANT> 'ContextData<out VARIANT> [Prompt<VARIANT>])  ; Gets an object or subentity interactively
    M InitializeUserInput  (vla-InitializeUserInput obj Bits<long> [KeyWordList<VARIANT>])  ; Initializes the GetKeyword method
    M IsRemoteFile  (vla-IsRemoteFile obj LocalFile<BSTR> 'URL<out BSTR>) -> VARIANT_BOOL  ; Returns the URL that a remote file was downloaded from
    M IsURL  (vla-IsURL obj URL<BSTR>) -> VARIANT_BOOL  ; Validates a given URL
    M LaunchBrowserDialog  (vla-LaunchBrowserDialog obj 'SelectedURL<out BSTR> DialogTitle<BSTR> OpenButtonCaption<BSTR> StartPageURL<BSTR> RegistryRootKey<BSTR> OpenButtonAlwaysEnabled<VARIANT_BOOL>) -> VARIANT_BOOL  ; Launches the Web Browser dialog that allows the user to navigate to any URL and select a URL
    M PolarPoint  (vla-PolarPoint obj Point<VARIANT> Angle<double> Distance<double>) -> VARIANT  ; Gets the point at a specified angle and distance from a given point
    M Prompt  (vla-Prompt obj Message<BSTR>)  ; Posts a prompt to the command line
    M PutRemoteFile  (vla-PutRemoteFile obj URL<BSTR> LocalFile<BSTR>)  ; Uploads a file to a remote location specified by a URL
    M RealToString  (vla-RealToString obj Value<double> Unit<AcUnits> precision<long>) -> BSTR  ; Converts a real (double) value to a string
    M SendModelessOperationEnded  (vla-SendModelessOperationEnded obj Context<BSTR>)  ; Indicates a modeless operation has ended.
    M SendModelessOperationStart  (vla-SendModelessOperationStart obj Context<BSTR>)  ; Indicates a modeless operation will start.
    M TranslateCoordinates  (vla-TranslateCoordinates obj Point<VARIANT> FromCoordSystem<AcCoordinateSystem> ToCoordSystem<AcCoordinateSystem> Displacement<long> [OCSNormal<VARIANT>]) -> VARIANT  ; Translates a point from one coordinate system to another coordinate system

[INTERFACE] IAcadView
Object: AcadView
Inherits: IAcadObject
Doc: A graphical representation of a 2D drawing or 3D model from a specific location (viewpoint) in space
  Properties
    P CategoryName : BSTR  [get/put]  vla-get-CategoryName / vla-put-CategoryName  ; RReturns and sets the name of the category of the view.
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specifies the center of an arc, circle, ellipse, view, or viewport
    P Direction : VARIANT  [get/put]  vla-get-Direction / vla-put-Direction  ; Specifies the viewing direction for a 3D visualization of the drawing
    P HasVpAssociation : VARIANT_BOOL  [get/put]  vla-get-HasVpAssociation / vla-put-HasVpAssociation  ; Specifies whether the view is associated with a paperspace viewport.
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P LayerState : BSTR  [get/put]  vla-get-LayerState / vla-put-LayerState  ; Returns and sets the name of the layer state of the view.
    P LayoutId : LONG_PTR  [get/put]  vla-get-LayoutId / vla-put-LayoutId  ; Returns and sets the layout of the view.
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P Target : VARIANT  [get/put]  vla-get-Target / vla-put-Target  ; Specifies the target point for the view or viewport
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window

[INTERFACE] IAcadViewport
Object: AcadViewport
Inherits: IAcadObject
Doc: A bounded area that displays some portion of a drawing's model space
  Methods
    M GetGridSpacing  (vla-GetGridSpacing obj 'XSpacing<out double> 'YSpacing<out double>)  ; Gets the grid spacing for the viewport
    M GetSnapSpacing  (vla-GetSnapSpacing obj 'XSpacing<out double> 'YSpacing<out double>)  ; Gets the snap spacing for the viewport
    M SetGridSpacing  (vla-SetGridSpacing obj XSpacing<double> YSpacing<double>)  ; Sets the grid spacing for the viewport
    M SetSnapSpacing  (vla-SetSnapSpacing obj XSpacing<double> YSpacing<double>)  ; Sets the snap spacing for the viewport
    M SetView  (vla-SetView obj View<IAcadView*>)  ; Sets the view in a viewport to a saved view in the Views Collection object
    M Split  (vla-Split obj NumWins<AcViewportSplitType>)  ; Splits a viewport into the given number of views
  Properties
    P ArcSmoothness : long  [get/put]  vla-get-ArcSmoothness / vla-put-ArcSmoothness  ; Specifies the smoothness of circles, arcs, and ellipses
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specifies the center of an arc, circle, ellipse, view, or viewport
    P Direction : VARIANT  [get/put]  vla-get-Direction / vla-put-Direction  ; Specifies the viewing direction for a 3D visualization of the drawing
    P GridOn : VARIANT_BOOL  [get/put]  vla-get-GridOn / vla-put-GridOn  ; Specifies the status of the viewport grid
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P LowerLeftCorner : VARIANT  [get READ-ONLY]  vla-get-LowerLeftCorner  ; Gets the lower-left corner of the current active viewport
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P OrthoOn : VARIANT_BOOL  [get/put]  vla-get-OrthoOn / vla-put-OrthoOn  ; Specifies the status of the Ortho mode for the viewport
    P SnapBasePoint : VARIANT  [get/put]  vla-get-SnapBasePoint / vla-put-SnapBasePoint  ; Specifies the snap base point for the viewport
    P SnapOn : VARIANT_BOOL  [get/put]  vla-get-SnapOn / vla-put-SnapOn  ; Specifies the status of snap
    P SnapRotationAngle : ACAD_ANGLE  [get/put]  vla-get-SnapRotationAngle / vla-put-SnapRotationAngle  ; Specifies the snap rotation angle of the viewport relative to the current UCS
    P Target : VARIANT  [get/put]  vla-get-Target / vla-put-Target  ; Specifies the target point for the view or viewport
    P UCSIconAtOrigin : VARIANT_BOOL  [get/put]  vla-get-UCSIconAtOrigin / vla-put-UCSIconAtOrigin  ; Specifies if the UCS icon is displayed at the origin
    P UCSIconOn : VARIANT_BOOL  [get/put]  vla-get-UCSIconOn / vla-put-UCSIconOn  ; Specifies if the UCS icon is on
    P UpperRightCorner : VARIANT  [get READ-ONLY]  vla-get-UpperRightCorner  ; Gets the upper-right corner of the current active viewport
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window

[INTERFACE] IAcadViewports
Object: AcadViewports
Inherits: IAcadObject
Doc: The collection of all viewports in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadViewport  ; Creates a member object and adds it to the appropriate collection
    M DeleteConfiguration  (vla-DeleteConfiguration obj Name<BSTR>)  ; Deletes a viewport configuration
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadViewport  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadViews
Object: AcadViews
Inherits: IAcadObject
Doc: The collection of all views in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadView  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadView  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadWipeout
Object: AcadWipeout
Inherits: IAcadRasterImage
Doc: AutoCAD Interface for an Wipeout Raster Image

[INTERFACE] IAcadXline
Object: AcadXline
Inherits: IAcadEntity
Doc: AutoCAD Xline Interface
  Methods
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Creates a new entity object by offsetting the Xline by a specified distance
  Properties
    P BasePoint : VARIANT  [get/put]  vla-get-BasePoint / vla-put-BasePoint  ; Specify the X, Y, Z coordinate for the base point of the Xline or use the Pick Point button to set X, Y, Z values simultaneously
    P DirectionVector : VARIANT  [get/put]  vla-get-DirectionVector / vla-put-DirectionVector  ; Specify the X, Y, Z coordinate for the direction vector of the Xline
    P SecondPoint : VARIANT  [get/put]  vla-get-SecondPoint / vla-put-SecondPoint  ; Specify the X, Y, Z coordinate for the second point of the Xline or use the Pick Point button to set X, Y, Z values simultaneously

[INTERFACE] IAcadXRecord
Object: AcadXRecord
Inherits: IAcadObject
Doc: XRecord objects are used to store and manage arbitrary data
  Methods
    M GetXRecordData  (vla-GetXRecordData obj 'XRecordDataType<out VARIANT> 'XRecordDataValue<out VARIANT>)  ; Gets the extended record data (XRecordData) associated with a dictionary
    M SetXRecordData  (vla-SetXRecordData obj XRecordDataType<VARIANT> XRecordDataValue<VARIANT>)  ; Sets the extended record data (XRecordData) associated with a dictionary
  Properties
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P TranslateIDs : VARIANT_BOOL  [get/put]  vla-get-TranslateIDs / vla-put-TranslateIDs  ; Specifies the translation of any contained object IDs during deepClone or wblockClone operations
