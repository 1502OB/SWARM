# AutoCAD 2027 ActiveX: DimRadial .. Object

Generated from the type libraries by scripts/tlb_to_md.py. M = method, P = property.
vla- call: the object is always the first argument; 'name<out T> = pass a quoted symbol the call sets.
A member is listed only on the interface that declares it: walk `Inherits:` (IAcadLine -> IAcadEntity -> IAcadObject).

[INTERFACE] IAcadDimRadial
Object: AcadDimRadial
Inherits: IAcadDimension
Doc: AutoCAD Radial Dimension Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets units format for alternate units dimensions On or Off, except angular (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P ArrowheadBlock : BSTR  [get/put]  vla-get-ArrowheadBlock / vla-put-ArrowheadBlock  ; Specifies the block to use as the custom arrowhead for a radial dimension or leader line
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P ArrowheadType : AcDimArrowheadType  [get/put]  vla-get-ArrowheadType / vla-put-ArrowheadType  ; Specifies type of the dimension arrowhead
    P CenterMarkSize : double  [get/put]  vla-get-CenterMarkSize / vla-put-CenterMarkSize  ; Specifies size of the center mark on the dimension (DIMCEN system variable)
    P CenterType : AcDimCenterType  [get/put]  vla-get-CenterType / vla-put-CenterType  ; Specifies type of center mark on the dimension (DIMCEN system variable)
    P DimConstrDesc : BSTR  [get/put]  vla-get-DimConstrDesc / vla-put-DimConstrDesc  ; Specifies description for constraint
    P DimConstrExpression : BSTR  [get/put]  vla-get-DimConstrExpression / vla-put-DimConstrExpression  ; Specifies the expression or the value of the constraint
    P DimConstrForm : VARIANT_BOOL  [get/put]  vla-get-DimConstrForm / vla-put-DimConstrForm  ; Specifies the constraint type - Dynamic or Annotational
    P DimConstrName : BSTR  [get/put]  vla-get-DimConstrName / vla-put-DimConstrName  ; Specifies the name of the dimensional constraint. Names cannot have spaces
    P DimConstrReference : VARIANT_BOOL  [get/put]  vla-get-DimConstrReference / vla-put-DimConstrReference  ; Specifies whether the parameter is a reference constraint. Reference dimensions can be used in expressions but don't drive geometry - similar to an associative dimension
    P DimConstrValue : BSTR  [get/put]  vla-get-DimConstrValue / vla-put-DimConstrValue  ; Specifies the value of the constraint
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLineSuppress : VARIANT_BOOL  [get/put]  vla-get-DimLineSuppress / vla-put-DimLineSuppress  ; Sets the suppression of the second dimension line On or Off (DIMSD2)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P LeaderLength : double  [put]  vla-put-LeaderLength  ; Specifies the length of the leader on the diameter or radius dimension
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets drawing of text between extension lines On or Off (DIMTIX system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)

[INTERFACE] IAcadDimRadialLarge
Object: AcadDimRadialLarge
Inherits: IAcadDimension
Doc: AutoCAD Jogged Dimension Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets units format for alternate units dimensions On or Off, except angular (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P ArrowheadBlock : BSTR  [get/put]  vla-get-ArrowheadBlock / vla-put-ArrowheadBlock  ; Specifies the block to use as the custom arrowhead for a radial dimension or leader line
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P ArrowheadType : AcDimArrowheadType  [get/put]  vla-get-ArrowheadType / vla-put-ArrowheadType  ; Specifies type of the dimension arrowhead
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specifies the center of the arc
    P CenterMarkSize : double  [get/put]  vla-get-CenterMarkSize / vla-put-CenterMarkSize  ; Specifies size of the center mark on the dimension (DIMCEN system variable)
    P CenterType : AcDimCenterType  [get/put]  vla-get-CenterType / vla-put-CenterType  ; Specifies type of center mark on the dimension (DIMCEN system variable)
    P ChordPoint : VARIANT  [get/put]  vla-get-ChordPoint / vla-put-ChordPoint  ; Specifies the chord point for the arc
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLineSuppress : VARIANT_BOOL  [get/put]  vla-get-DimLineSuppress / vla-put-DimLineSuppress  ; Sets the suppression of the second dimension line On or Off (DIMSD2)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P JogAngle : ACAD_ANGLE  [get/put]  vla-get-JogAngle / vla-put-JogAngle  ; Specifies the jog angle (DIMJOGANG system variable)
    P JogLocation : VARIANT  [get/put]  vla-get-JogLocation / vla-put-JogLocation  ; Specifies the jog location or pick point
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P OverrideCenter : VARIANT  [get/put]  vla-get-OverrideCenter / vla-put-OverrideCenter  ; Specifies the override center location or pick point
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets drawing of text between extension lines On or Off (DIMTIX system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)

[INTERFACE] IAcadDimRotated
Object: AcadDimRotated
Inherits: IAcadDimension
Doc: AutoCAD Rotated Dimension Interface
  Properties
    P AltRoundDistance : double  [get/put]  vla-get-AltRoundDistance / vla-put-AltRoundDistance  ; Specifies distance rounding value for alternate units (DIMALTRND system variable)
    P AltSubUnitsFactor : double  [get/put]  vla-get-AltSubUnitsFactor / vla-put-AltSubUnitsFactor  ; Specifies the alternate sub-units scale factor for all applicable linear dimension
    P AltSubUnitsSuffix : BSTR  [get/put]  vla-get-AltSubUnitsSuffix / vla-put-AltSubUnitsSuffix  ; Specifies the text suffix for the alternate dimension when change to alternate sub-units
    P AltSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressLeadingZeros / vla-put-AltSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltSuppressTrailingZeros / vla-put-AltSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroFeet / vla-put-AltSuppressZeroFeet  ; Sets suppression of zero feet for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltSuppressZeroInches / vla-put-AltSuppressZeroInches  ; Sets suppression of zero inches for alternate units dimensions On or Off (DIMALTZ system variable)
    P AltTextPrefix : BSTR  [get/put]  vla-get-AltTextPrefix / vla-put-AltTextPrefix  ; Specifies text prefix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTextSuffix : BSTR  [get/put]  vla-get-AltTextSuffix / vla-put-AltTextSuffix  ; Specifies text sufffix to alternate dimensions except angular (DIMAPOST system variable)
    P AltTolerancePrecision : AcDimPrecision  [get/put]  vla-get-AltTolerancePrecision / vla-put-AltTolerancePrecision  ; Specifies number of decimal places for tolerance values of an alternate units dimension (DIMALTTD system variable)
    P AltToleranceSuppressLeadingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressLeadingZeros / vla-put-AltToleranceSuppressLeadingZeros  ; Sets suppression of leading zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressTrailingZeros : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressTrailingZeros / vla-put-AltToleranceSuppressTrailingZeros  ; Sets suppression of trailing zeros for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroFeet / vla-put-AltToleranceSuppressZeroFeet  ; Sets suppression of zero feet for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-AltToleranceSuppressZeroInches / vla-put-AltToleranceSuppressZeroInches  ; Sets suppression of zero inches for alternate units tolerance values On or Off (DIMALTTZ system variable)
    P AltUnits : VARIANT_BOOL  [get/put]  vla-get-AltUnits / vla-put-AltUnits  ; Sets units format for alternate units dimensions On or Off, except angular (DIMALT system variable)
    P AltUnitsFormat : AcDimUnits  [get/put]  vla-get-AltUnitsFormat / vla-put-AltUnitsFormat  ; Specifies units format for alternate units dimensions except angular (DIMALTU system variable)
    P AltUnitsPrecision : AcDimPrecision  [get/put]  vla-get-AltUnitsPrecision / vla-put-AltUnitsPrecision  ; Specifies decimal place precision for alternate units (DIMALTD system variable)
    P AltUnitsScale : ACAD_NOUNITS  [get/put]  vla-get-AltUnitsScale / vla-put-AltUnitsScale  ; Specifies scale factor for alternate units (DIMALTF system variable)
    P Arrowhead1Block : BSTR  [get/put]  vla-get-Arrowhead1Block / vla-put-Arrowhead1Block  ; Specifies the block to use as the custom arrowhead for the first end of the dimension line
    P Arrowhead1Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead1Type / vla-put-Arrowhead1Type  ; Specifies type of the first dimension arrowhead (DIMBLK1 system variable)
    P Arrowhead2Block : BSTR  [get/put]  vla-get-Arrowhead2Block / vla-put-Arrowhead2Block  ; Specifies the block to use as the custom arrowhead for the second end of the dimension line
    P Arrowhead2Type : AcDimArrowheadType  [get/put]  vla-get-Arrowhead2Type / vla-put-Arrowhead2Type  ; Specifies type of the second dimension arrowhead (DIMBLK2 system variable)
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies size of the dimension arrowhead (DIMASZ system variable)
    P DimConstrDesc : BSTR  [get/put]  vla-get-DimConstrDesc / vla-put-DimConstrDesc  ; Specifies description for constraint
    P DimConstrExpression : BSTR  [get/put]  vla-get-DimConstrExpression / vla-put-DimConstrExpression  ; Specifies the expression or the value of the constraint
    P DimConstrForm : VARIANT_BOOL  [get/put]  vla-get-DimConstrForm / vla-put-DimConstrForm  ; Specifies the constraint type - Dynamic or Annotational
    P DimConstrName : BSTR  [get/put]  vla-get-DimConstrName / vla-put-DimConstrName  ; Specifies the name of the dimensional constraint. Names cannot have spaces
    P DimConstrReference : VARIANT_BOOL  [get/put]  vla-get-DimConstrReference / vla-put-DimConstrReference  ; Specifies whether the parameter is a reference constraint. Reference dimensions can be used in expressions but don't drive geometry - similar to an associative dimension
    P DimConstrValue : BSTR  [get/put]  vla-get-DimConstrValue / vla-put-DimConstrValue  ; Specifies the value of the constraint
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies color of the dimension lines (DIMCLRD system variable)
    P DimensionLineExtend : double  [get/put]  vla-get-DimensionLineExtend / vla-put-DimensionLineExtend  ; Specifies amount to extend dimension lines beyond the extension line (DIMDLE system variable)
    P DimensionLinetype : BSTR  [get/put]  vla-get-DimensionLinetype / vla-put-DimensionLinetype  ; Specifies the linetype of the dimension line (DIMLTYPE system variable)
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies lineweight for dimension lines (DIMLWD system variable)
    P DimLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine1Suppress / vla-put-DimLine1Suppress  ; Sets suppression of first dimension line On or Off (DIMSD1 system variable)
    P DimLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-DimLine2Suppress / vla-put-DimLine2Suppress  ; Sets suppression of second dimension line On or Off (DIMSD2 system variable)
    P DimLineInside : VARIANT_BOOL  [get/put]  vla-get-DimLineInside / vla-put-DimLineInside  ; Sets drawing of dimension lines outside extension lines On or Off (DIMSOXD system variable)
    P ExtensionLineColor : ACAD_COLOR  [get/put]  vla-get-ExtensionLineColor / vla-put-ExtensionLineColor  ; Specifies color of the extension line (DIMCLRE system variable)
    P ExtensionLineExtend : double  [get/put]  vla-get-ExtensionLineExtend / vla-put-ExtensionLineExtend  ; Specifies amount to extend extension line beyond the dimension line (DIMEXE system variable)
    P ExtensionLineOffset : double  [get/put]  vla-get-ExtensionLineOffset / vla-put-ExtensionLineOffset  ; Specifies offset of extension lines from the origin points (DIMEXO system variable)
    P ExtensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-ExtensionLineWeight / vla-put-ExtensionLineWeight  ; Specifies lineweight for extension lines (DIMLWE system variable)
    P ExtLine1Linetype : BSTR  [get/put]  vla-get-ExtLine1Linetype / vla-put-ExtLine1Linetype  ; Specifies the linetype of the first extension line (DIMLTEX1 system variable)
    P ExtLine1Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine1Suppress / vla-put-ExtLine1Suppress  ; Sets suppression of first extension line On or Off (DIMSE1 system variable)
    P ExtLine2Linetype : BSTR  [get/put]  vla-get-ExtLine2Linetype / vla-put-ExtLine2Linetype  ; Specifies the linetype of the second extension line (DIMLTEX2 system variable)
    P ExtLine2Suppress : VARIANT_BOOL  [get/put]  vla-get-ExtLine2Suppress / vla-put-ExtLine2Suppress  ; Sets suppression of second extension line On or Off (DIMSE2 system variable)
    P ExtLineFixedLen : double  [get/put]  vla-get-ExtLineFixedLen / vla-put-ExtLineFixedLen  ; Set extension line fixed length (DIMFXL system variable)
    P ExtLineFixedLenSuppress : VARIANT_BOOL  [get/put]  vla-get-ExtLineFixedLenSuppress / vla-put-ExtLineFixedLenSuppress  ; Sets suppression of extension line fixed length On or Off (DIMFXLON system variable)
    P Fit : AcDimFit  [get/put]  vla-get-Fit / vla-put-Fit  ; Determines what elements are moved to fit text and arrowheads in space between extension lines (DIMATFIT system variable)
    P ForceLineInside : VARIANT_BOOL  [get/put]  vla-get-ForceLineInside / vla-put-ForceLineInside  ; Forces drawing dimension line between extension lines On or Off, even when text is placed outside extension lines (DIMTOFL system variable)
    P FractionFormat : AcDimFractionType  [get/put]  vla-get-FractionFormat / vla-put-FractionFormat  ; Sets fraction type (DIMFRAC system variable)
    P HorizontalTextPosition : AcDimHorizontalJustification  [get/put]  vla-get-HorizontalTextPosition / vla-put-HorizontalTextPosition  ; Specifies horizontal dimension text position (DIMJUST system variable)
    P LinearScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-LinearScaleFactor / vla-put-LinearScaleFactor  ; Specifies global scale factor for linear dimensions (DIMLFAC system variable)
    P Measurement : double  [get READ-ONLY]  vla-get-Measurement  ; Specifies dimension measurement value
    P PrimaryUnitsPrecision : AcDimPrecision  [get/put]  vla-get-PrimaryUnitsPrecision / vla-put-PrimaryUnitsPrecision  ; Specifies precision for primary units dimensions (DIMDEC system variable)
    P RoundDistance : double  [get/put]  vla-get-RoundDistance / vla-put-RoundDistance  ; Specifies distance rounding value (DIMRND system variable)
    P SubUnitsFactor : double  [get/put]  vla-get-SubUnitsFactor / vla-put-SubUnitsFactor  ; Specifies the sub-units scale factor for all applicable linear dimension
    P SubUnitsSuffix : BSTR  [get/put]  vla-get-SubUnitsSuffix / vla-put-SubUnitsSuffix  ; Specifies the text suffix for all applicable linear dimension when change to sub-units
    P SuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroFeet / vla-put-SuppressZeroFeet  ; Sets suppression of zero feet for dimensions On or Off (DIMZIN system variable)
    P SuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-SuppressZeroInches / vla-put-SuppressZeroInches  ; Sets suppression of zero inches for dimensions On or Off (DIMZIN system variable)
    P TextInside : VARIANT_BOOL  [get/put]  vla-get-TextInside / vla-put-TextInside  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextInsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextInsideAlign / vla-put-TextInsideAlign  ; Sets position of dimension text inside extension lines On or Off (DIMTIH system variable)
    P TextOutsideAlign : VARIANT_BOOL  [get/put]  vla-get-TextOutsideAlign / vla-put-TextOutsideAlign  ; Sets positioning of dimension text outside extension lines On or Off (DIMTOH system variable)
    P ToleranceSuppressZeroFeet : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroFeet / vla-put-ToleranceSuppressZeroFeet  ; Sets suppression of zero feet for tolerance values On or Off (DIMTZIN system variable)
    P ToleranceSuppressZeroInches : VARIANT_BOOL  [get/put]  vla-get-ToleranceSuppressZeroInches / vla-put-ToleranceSuppressZeroInches  ; Sets suppression of zero inches for tolerance values On or Off (DIMTZIN system variable)
    P UnitsFormat : AcDimLUnits  [get/put]  vla-get-UnitsFormat / vla-put-UnitsFormat  ; Specifies units format for linear dimensions (DIMLUNIT system variable)

[INTERFACE] IAcadDimStyle
Object: AcadDimStyle
Inherits: IAcadObject
Doc: A group of dimension settings that determines the appearance of a dimension
  Methods
    M CopyFrom  (vla-CopyFrom obj StyleSource<IDispatch*>)  ; Copies the dimension style data from a source object
  Properties
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object

[INTERFACE] IAcadDimStyles
Object: AcadDimStyles
Inherits: IAcadObject
Doc: The collection of all dimension styles in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadDimStyle  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadDimStyle  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadDocument
Object: AcadDocument
Inherits: IAcadDatabase
Doc: An AutoCAD drawing
  Methods
    M Activate  (vla-Activate obj)  ; Makes the specified drawing active
    M AuditInfo  (vla-AuditInfo obj FixErr<VARIANT_BOOL>)  ; Evaluates the integrity of the drawing
    M Close  (vla-Close obj [SaveChanges<VARIANT>] [FileName<VARIANT>])  ; Closes the specified drawing, or all open drawings
    M EndUndoMark  (vla-EndUndoMark obj)  ; Marks the end of a block of operations
    M Export  (vla-Export obj FileName<BSTR> Extension<BSTR> SelectionSet<IAcadSelectionSet*>)  ; Exports the AutoCAD drawing to a WMF, SAT, EPS, DXF, or BMP format
    M GetVariable  (vla-GetVariable obj Name<BSTR>) -> VARIANT  ; Gets the current setting of an AutoCAD system variable
    M Import  (vla-Import obj FileName<BSTR> InsertionPoint<VARIANT> ScaleFactor<double>) -> IDispatch  ; Imports a drawing file in SAT, EPS, DXF, or WMF format
    M LoadShapeFile  (vla-LoadShapeFile obj FullName<BSTR>)  ; Loads a shape file (SHX)
    M New  (vla-New obj TemplateFileName<BSTR>) -> IAcadDocument  ; Creates a new document in SDI mode
    M Open  (vla-Open obj FullName<BSTR> [Password<VARIANT>]) -> IAcadDocument  ; Opens an existing drawing file (DWG) and makes it the active document
    M PostCommand  (vla-PostCommand obj Command<BSTR>)  ; Posts a command string from a VB or VBA application to the document for processing
    M PurgeAll  (vla-PurgeAll obj)  ; Removes unused named references such as unused blocks or layers from the document
    M Regen  (vla-Regen obj WhichViewports<AcRegenType>)  ; Regenerates the entire drawing and recomputes the screen coordinates and view resolution for all objects
    M Save  (vla-Save obj)  ; Saves the document or menu group
    M SaveAs  (vla-SaveAs obj FullFileName<BSTR> [SaveAsType<VARIANT>] [vSecurityParams<VARIANT>])  ; Saves the document or menu group to a specified file
    M SendCommand  (vla-SendCommand obj Command<BSTR>)  ; Sends a command string from a VB or VBA application to the document for processing
    M SetVariable  (vla-SetVariable obj Name<BSTR> Value<VARIANT>)  ; Sets the value of an AutoCAD system variable
    M StartUndoMark  (vla-StartUndoMark obj)  ; Marks the beginning of a block of operations
    M Wblock  (vla-Wblock obj FileName<BSTR> SelectionSet<IAcadSelectionSet*>)  ; Writes out the given selection set as a new drawing file
  Properties
    P Active : VARIANT_BOOL  [get READ-ONLY]  vla-get-Active  ; Determines if the document is the active document for the session
    P ActiveDimStyle : IAcadDimStyle  [get/put]  vla-get-ActiveDimStyle / vla-put-ActiveDimStyle  ; Specifies the active dimension style
    P ActiveLayer : IAcadLayer  [get/put]  vla-get-ActiveLayer / vla-put-ActiveLayer  ; Specifies the active layer
    P ActiveLayout : IAcadLayout  [get/put]  vla-get-ActiveLayout / vla-put-ActiveLayout  ; Specifies the active layout
    P ActiveLinetype : IAcadLineType  [get/put]  vla-get-ActiveLinetype / vla-put-ActiveLinetype  ; Specifies the active linetype for the drawing
    P ActiveMaterial : IAcadMaterial  [get/put]  vla-get-ActiveMaterial / vla-put-ActiveMaterial
    P ActivePViewport : IAcadPViewport  [get/put]  vla-get-ActivePViewport / vla-put-ActivePViewport  ; Specifies the active paper space viewport for the drawing
    P ActiveSelectionSet : IAcadSelectionSet  [get READ-ONLY]  vla-get-ActiveSelectionSet  ; Gets the active selection set for the drawing
    P ActiveSpace : AcActiveSpace  [get/put]  vla-get-ActiveSpace / vla-put-ActiveSpace  ; Toggles the active space between paper space and model space
    P ActiveTextStyle : IAcadTextStyle  [get/put]  vla-get-ActiveTextStyle / vla-put-ActiveTextStyle  ; Specifies the active text style for the drawing
    P ActiveUCS : IAcadUCS  [get/put]  vla-get-ActiveUCS / vla-put-ActiveUCS  ; Specifies the active UCS for the drawing
    P ActiveViewport : IAcadViewport  [get/put]  vla-get-ActiveViewport / vla-put-ActiveViewport  ; Specifies the active viewport for the drawing
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Database : IAcadDatabase  [get READ-ONLY]  vla-get-Database  ; Gets the database in which the object belongs
    P FullName : BSTR  [get READ-ONLY]  vla-get-FullName  ; Gets the name of the application or document, including the path
    P Height : long  [get/put]  vla-get-Height / vla-put-Height  ; Height of the attribute, shape, text, or view toolbar or the main application window
    P HWND : LONG_PTR  [get READ-ONLY]  vla-get-HWND  ; Gets the window handle of the document window frame
    P MSpace : VARIANT_BOOL  [get/put]  vla-get-MSpace / vla-put-MSpace  ; Allows editing of the model from floating paper space viewports
    P Name : BSTR  [get READ-ONLY]  vla-get-Name  ; Specifies the name of the object
    P ObjectSnapMode : VARIANT_BOOL  [get/put]  vla-get-ObjectSnapMode / vla-put-ObjectSnapMode  ; Specifies the setting of the object snap mode
    P Path : BSTR  [get READ-ONLY]  vla-get-Path  ; Gets the path of the document, application, or external reference
    P PickfirstSelectionSet : IAcadSelectionSet  [get READ-ONLY]  vla-get-PickfirstSelectionSet  ; Gets the pickfirst selection set
    P Plot : IAcadPlot  [get READ-ONLY]  vla-get-Plot  ; Gets the Plot object for the document
    P ReadOnly : VARIANT_BOOL  [get READ-ONLY]  vla-get-ReadOnly  ; Specifies if the document is read-only or read-write
    P Saved : VARIANT_BOOL  [get READ-ONLY]  vla-get-Saved  ; Specifies if the document has any unsaved changes
    P SelectionSets : IAcadSelectionSets  [get READ-ONLY]  vla-get-SelectionSets  ; Gets the SelectionSets collection for the document
    P Utility : IAcadUtility  [get READ-ONLY]  vla-get-Utility  ; Gets the Utility object for the document
    P Width : long  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the width of the text boundary, view, image, toolbar, or main application window
    P WindowState : AcWindowState  [get/put]  vla-get-WindowState / vla-put-WindowState  ; Specifies the state of the application or document window
    P WindowTitle : BSTR  [get READ-ONLY]  vla-get-WindowTitle  ; Gets the title of the document window

[INTERFACE] IAcadDocuments
Object: AcadDocuments
Doc: The collection of all AutoCAD drawings open in the current session
  Methods
    M Add  (vla-Add obj [TemplateName<VARIANT>]) -> IAcadDocument  ; Creates a member object and adds it to the appropriate collection
    M Close  (vla-Close obj)  ; Closes the specified drawing, or all open drawings
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadDocument  ; Gets the member object at a given index in a collection, group, or selection set
    M Open  (vla-Open obj Name<BSTR> [ReadOnly<VARIANT>] [Password<VARIANT>]) -> IAcadDocument  ; Opens an existing drawing file (DWG) and makes it the active document
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadDwfUnderlay
Inherits: IAcadUnderlay
  Properties
    P DWFFormat : BSTR  [get/put]  vla-get-DWFFormat / vla-put-DWFFormat  ; Specifies the format of DWF file

[INTERFACE] IAcadDynamicBlockReferenceProperty
Object: AcadDynamicBlockReferenceProperty
Doc: AutoCAD Dynamic Block Property Interface
  Properties
    P AllowedValues : VARIANT  [get READ-ONLY]  vla-get-AllowedValues  ; Specifies the allowed values for the property.
    P Description : BSTR  [get READ-ONLY]  vla-get-Description  ; Specifies the description for the property.
    P PropertyName : BSTR  [get READ-ONLY]  vla-get-PropertyName  ; Specifies the name for the property.
    P ReadOnly : VARIANT_BOOL  [get READ-ONLY]  vla-get-ReadOnly  ; Specifies whether the property is read-only.
    P show : VARIANT_BOOL  [get READ-ONLY]  vla-get-show  ; Specifies whether the property is showin in the user interface.
    P UnitsType : AcDynamicBlockReferencePropertyUnitsType  [get READ-ONLY]  vla-get-UnitsType  ; Specifies the current display units type for the property.
    P Value : VARIANT  [get/put]  vla-get-Value / vla-put-Value  ; Specifies the current value for the property.

[INTERFACE] IAcadEllipse
Object: AcadEllipse
Inherits: IAcadEntity
Doc: AutoCAD Ellipse Interface
  Methods
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; method Offset
  Properties
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the ellipse
    P Center : VARIANT  [get/put]  vla-get-Center / vla-put-Center  ; Specify the X, Y, Z coordinate of the center point of the ellipse or use the Pick Point button to set X, Y, Z values simultaneously
    P EndAngle : ACAD_ANGLE  [get/put]  vla-get-EndAngle / vla-put-EndAngle  ; Specifies the end angle of the ellipse
    P EndParameter : double  [get/put]  vla-get-EndParameter / vla-put-EndParameter  ; Specifies the end parameter for an ellipse
    P EndPoint : VARIANT  [get READ-ONLY]  vla-get-EndPoint  ; Specify the X, Y, Z coordinate of the end point of the ellipse
    P MajorAxis : VARIANT  [get/put]  vla-get-MajorAxis / vla-put-MajorAxis  ; Specifies the major axis of the ellipse
    P MajorRadius : double  [get/put]  vla-get-MajorRadius / vla-put-MajorRadius  ; Specifies the major radius of the ellipse
    P MinorAxis : VARIANT  [get READ-ONLY]  vla-get-MinorAxis  ; Specifies the minor axis of the ellipse
    P MinorRadius : double  [get/put]  vla-get-MinorRadius / vla-put-MinorRadius  ; Specifies the minor radius of the ellipse
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P RadiusRatio : double  [get/put]  vla-get-RadiusRatio / vla-put-RadiusRatio  ; Specifies the radius ratio of the ellipse
    P StartAngle : ACAD_ANGLE  [get/put]  vla-get-StartAngle / vla-put-StartAngle  ; Specifies the start angle of the ellipse
    P StartParameter : double  [get/put]  vla-get-StartParameter / vla-put-StartParameter  ; Specifies the start parameter for an ellipse
    P StartPoint : VARIANT  [get READ-ONLY]  vla-get-StartPoint  ; Specify the X, Y, Z coordinate of the start point of the ellipse

[INTERFACE] IAcadEntity
Object: AcadEntity
Inherits: IAcadObject
Doc: AutoCAD Entity Interface
  Methods
    M ArrayPolar  (vla-ArrayPolar obj NumberOfObjects<long> AngleToFill<double> CenterPoint<VARIANT>) -> VARIANT  ; Creates an array of selected objects in a polar pattern.
    M ArrayRectangular  (vla-ArrayRectangular obj NumberOfRows<long> NumberOfColumns<long> NumberOfLevels<long> DistBetweenRows<double> DistBetweenCols<double> DistBetweenLevels<double>) -> VARIANT  ; Creates an array of selected objects in a rectangular pattern.
    M Copy  (vla-Copy obj) -> IDispatch  ; Copies the entity object.
    M GetBoundingBox  (vla-GetBoundingBox obj 'MinPoint<out VARIANT> 'MaxPoint<out VARIANT>)  ; Returns the min and max point of the bounding box of the entity object.
    M Highlight  (vla-Highlight obj HighlightFlag<VARIANT_BOOL>)  ; Highlights the entity object.
    M IntersectWith  (vla-IntersectWith obj IntersectObject<IDispatch*> option<AcExtendOption>) -> VARIANT  ; Intersects with the input entity object.
    M Mirror  (vla-Mirror obj Point1<VARIANT> Point2<VARIANT>) -> IDispatch  ; Mirrors selected objects about a line.
    M Mirror3D  (vla-Mirror3D obj Point1<VARIANT> Point2<VARIANT> point3<VARIANT>) -> IDispatch  ; Mirrors selected objects about a plane defined by three points.
    M Move  (vla-Move obj FromPoint<VARIANT> ToPoint<VARIANT>)  ; Moves the entity object from source to destination.
    M Rotate  (vla-Rotate obj BasePoint<VARIANT> RotationAngle<double>)  ; Rotates the entity object about a point.
    M Rotate3D  (vla-Rotate3D obj Point1<VARIANT> Point2<VARIANT> RotationAngle<double>)  ; Rotates the entity object about a 3D line.
    M ScaleEntity  (vla-ScaleEntity obj BasePoint<VARIANT> ScaleFactor<double>)  ; Scale the entity object with respect to the base point and the scale factor.
    M TransformBy  (vla-TransformBy obj TransformationMatrix<VARIANT>)  ; Performs the specified transformation on the entity object.
    M Update  (vla-Update obj)  ; Updates the graphics of the entity object.
  Properties
    P color : ACAD_COLOR  [get/put]  vla-get-color / vla-put-color  ; Specifies the color for objects
    P EntityName : BSTR  [get READ-ONLY]  vla-get-EntityName  ; Returns the class name of the object.
    P EntityTransparency : BSTR  [get/put]  vla-get-EntityTransparency / vla-put-EntityTransparency  ; Specifies the transparency of the object
    P EntityType : long  [get READ-ONLY]  vla-get-EntityType  ; Returns the entity type of the object as an integer.
    P Hyperlinks : IAcadHyperlinks  [get READ-ONLY]  vla-get-Hyperlinks  ; Assigns a hyperlink to an object and displays the hyperlink name or description (if one is specified)
    P Layer : BSTR  [get/put]  vla-get-Layer / vla-put-Layer  ; Specifies the current layer of the object
    P Linetype : BSTR  [get/put]  vla-get-Linetype / vla-put-Linetype  ; Specifies the current linetype of the object
    P LinetypeScale : ACAD_NOUNITS  [get/put]  vla-get-LinetypeScale / vla-put-LinetypeScale  ; Specifies the linetype scale factor of the object
    P Lineweight : ACAD_LWEIGHT  [get/put]  vla-get-Lineweight / vla-put-Lineweight  ; Specifies the lineweight for the object
    P Material : BSTR  [get/put]  vla-get-Material / vla-put-Material  ; Specifies the material
    P PlotStyleName : BSTR  [get/put]  vla-get-PlotStyleName / vla-put-PlotStyleName  ; Specifies the plotstyle name for the object
    P TrueColor : IAcadAcCmColor  [get/put]  vla-get-TrueColor / vla-put-TrueColor  ; Returns the true color of the object.
    P Visible : VARIANT_BOOL  [get/put]  vla-get-Visible / vla-put-Visible  ; Specifies the visibility of an object or the application

[INTERFACE] IAcadExternalReference
Object: AcadExternalReference
Inherits: IAcadBlockReference
Doc: IAcadExternalReference Interface
  Properties
    P LayerPropertyOverrides : VARIANT_BOOL  [get READ-ONLY]  vla-get-LayerPropertyOverrides  ; Specifies whether the object has layer property overrides.
    P Path : BSTR  [get/put]  vla-get-Path / vla-put-Path  ; Specifies the path of the external reference

[INTERFACE] IAcadExtrudedSurface
Object: AcadExtrudedSurface
Inherits: IAcadSurface
Doc: IAcadExtrudedSurface Interface
  Properties
    P Direction : VARIANT  [get READ-ONLY]  vla-get-Direction  ; Displays the extrusion direction
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the extrusion
    P TaperAngle : ACAD_ANGLE  [get/put]  vla-get-TaperAngle / vla-put-TaperAngle  ; Specifies the taper angle of the extrusion

[INTERFACE] IAcadGeomapImage
Object: AcadGeomapImage
Inherits: IAcadRasterImage
Doc: AutoCAD Geomap Image Interface
  Properties
    P GeoImageBrightness : long  [get/put]  vla-get-GeoImageBrightness / vla-put-GeoImageBrightness  ; Specifies the brightness of the Geomap image
    P GeoImageContrast : long  [get/put]  vla-get-GeoImageContrast / vla-put-GeoImageContrast  ; Specifies the contrast of the Geomap image
    P GeoImageFade : long  [get/put]  vla-get-GeoImageFade / vla-put-GeoImageFade  ; Specifies the fade of the Geomap image
    P GeoImageHeight : double  [get READ-ONLY]  vla-get-GeoImageHeight  ; Specifies the height of the Geomap image
    P GeoImagePosition : VARIANT  [get READ-ONLY]  vla-get-GeoImagePosition  ; Specifies the position of the Geomap image
    P GeoImageWidth : double  [get READ-ONLY]  vla-get-GeoImageWidth  ; Specifies the width of the Geomap image

[INTERFACE] IAcadGeoPositionMarker
Object: AcadGeoPositionMarker
Inherits: IAcadEntity
Doc: AutoCAD GeoPositionMarker Interface
  Properties
    P Altitude : double  [get/put]  vla-get-Altitude / vla-put-Altitude  ; Specifies the elevation of the marker
    P BackgroundFill : VARIANT_BOOL  [get/put]  vla-get-BackgroundFill / vla-put-BackgroundFill  ; Specifies use Background Mask of the MText
    P DrawingDirection : AcDrawingDirection  [get/put]  vla-get-DrawingDirection / vla-put-DrawingDirection  ; Specifies the drawing direction of the MText
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the Mtext
    P LandingGap : double  [get/put]  vla-get-LandingGap / vla-put-LandingGap  ; Specify the landing distance
    P Latitude : BSTR  [get/put]  vla-get-Latitude / vla-put-Latitude  ; Specifies the latitude of the marker
    P LineSpacingDistance : double  [get/put]  vla-get-LineSpacingDistance / vla-put-LineSpacingDistance  ; Specifies the line spacing distance of the Mtext
    P LineSpacingFactor : double  [get/put]  vla-get-LineSpacingFactor / vla-put-LineSpacingFactor  ; Specifies the line spacing factor of the Mtext
    P LineSpacingStyle : AcLineSpacingStyle  [get/put]  vla-get-LineSpacingStyle / vla-put-LineSpacingStyle  ; Specifies the line spacing style of the Mtext
    P Longitude : BSTR  [get/put]  vla-get-Longitude / vla-put-Longitude  ; Specifies the longitude of the marker
    P Notes : BSTR  [get/put]  vla-get-Notes / vla-put-Notes  ; Specifies the notes for the marker
    P Position : VARIANT  [get/put]  vla-get-Position / vla-put-Position  ; Specify the marker's position
    P Radius : double  [get/put]  vla-get-Radius / vla-put-Radius  ; Radius
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the Mtext
    P TextFrameDisplay : VARIANT_BOOL  [get/put]  vla-get-TextFrameDisplay / vla-put-TextFrameDisplay  ; Display/hide text frame of content
    P TextJustify : AcAttachmentPoint  [get/put]  vla-get-TextJustify / vla-put-TextJustify  ; Specifies the attachment point of the MText
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the MText
    P TextStyleName : BSTR  [get/put]  vla-get-TextStyleName / vla-put-TextStyleName  ; Specifies the style name of the MText
    P TextWidth : double  [get/put]  vla-get-TextWidth / vla-put-TextWidth  ; Specifies the width of the MText

[INTERFACE] IAcadGroup
Object: AcadGroup
Inherits: IAcadObject
Doc: A named SelectionSet object
  Methods
    M AppendItems  (vla-AppendItems obj Objects<VARIANT>)  ; Appends one or more entities to the specified group
    M Highlight  (vla-Highlight obj HighlightFlag<VARIANT_BOOL>)  ; Sets the highlight status for the given object, or for all objects in a given selection set
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadEntity  ; Gets the member object at a given index in a collection, group, or selection set
    M RemoveItems  (vla-RemoveItems obj Objects<VARIANT>)  ; Removes specified items from the group or selection set
    M Update  (vla-Update obj)  ; Updates the object to the drawing screen
  Properties
    P color : AcColor  [put]  vla-put-color  ; Specifies the color of an entity or layer
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Layer : BSTR  [put]  vla-put-Layer  ; Specifies the layer for an entity
    P Linetype : BSTR  [put]  vla-put-Linetype  ; Specifies the linetype of an entity
    P LinetypeScale : ACAD_NOUNITS  [put]  vla-put-LinetypeScale  ; Specifies the linetype scale of an entity
    P Lineweight : ACAD_LWEIGHT  [put]  vla-put-Lineweight  ; Specifies the lineweight of an individual entity or the default lineweight for the drawing
    P Material : BSTR  [put]  vla-put-Material  ; Specifies the material
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P PlotStyleName : BSTR  [put]  vla-put-PlotStyleName  ; Specifies the plot style name for an entity
    P TrueColor : IAcadAcCmColor*  [put]  vla-put-TrueColor  ; Sets the true color for entities in the group.
    P Visible : VARIANT_BOOL  [put]  vla-put-Visible  ; Specifies the visibility of an object or the application

[INTERFACE] IAcadGroups
Object: AcadGroups
Inherits: IAcadObject
Doc: The collection of all groups in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadGroup  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadGroup  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadHatch
Object: AcadHatch
Inherits: IAcadEntity
Doc: AutoCAD Hatch Interface
  Methods
    M AppendInnerLoop  (vla-AppendInnerLoop obj ObjectArray<VARIANT>)  ; Append loops to the hatch
    M AppendOuterLoop  (vla-AppendOuterLoop obj ObjectArray<VARIANT>)  ; Append loops to the hatch
    M Evaluate  (vla-Evaluate obj)  ; Evaluate the hatch
    M GetLoopAt  (vla-GetLoopAt obj Index<long> 'ObjectArray<out VARIANT>)  ; Get loops at given index of the hatch
    M InsertLoopAt  (vla-InsertLoopAt obj Index<long> LoopType<AcLoopType> ObjectArray<VARIANT>)  ; Insert loops at given index to the hatch
    M SetPattern  (vla-SetPattern obj PatternType<long> PatternName<BSTR>)  ; Set Pattern Type and Name of the hatch
  Properties
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the hatch entity
    P AssociativeHatch : VARIANT_BOOL  [get/put]  vla-get-AssociativeHatch / vla-put-AssociativeHatch  ; Determines whether the hatch is associative or not
    P BackgroundColor : IAcadAcCmColor  [get/put]  vla-get-BackgroundColor / vla-put-BackgroundColor  ; Specifies the background color of the hatch.
    P Elevation : double  [get/put]  vla-get-Elevation / vla-put-Elevation  ; Specifies the elevation of the hatch relative to the Z axis of the objects' coordinate system
    P GradientAngle : ACAD_ANGLE  [get/put]  vla-get-GradientAngle / vla-put-GradientAngle  ; Specifies the gradient angle
    P GradientCentered : VARIANT_BOOL  [get/put]  vla-get-GradientCentered / vla-put-GradientCentered  ; Determines whether the gradient is centered or not
    P GradientColor1 : IAcadAcCmColor  [get/put]  vla-get-GradientColor1 / vla-put-GradientColor1  ; Specifies the gradient start color.
    P GradientColor2 : IAcadAcCmColor  [get/put]  vla-get-GradientColor2 / vla-put-GradientColor2  ; Specifies the gradient end color.
    P GradientName : BSTR  [get/put]  vla-get-GradientName / vla-put-GradientName  ; Specifies the pattern name of the gradient.
    P HatchObjectType : AcHatchObjectType  [get/put]  vla-get-HatchObjectType / vla-put-HatchObjectType  ; Sets the type of the hatch.
    P HatchStyle : AcHatchStyle  [get/put]  vla-get-HatchStyle / vla-put-HatchStyle  ; Sets the island display style of the hatch
    P ISOPenWidth : AcISOPenWidth  [get/put]  vla-get-ISOPenWidth / vla-put-ISOPenWidth  ; Specifies the ISO pen width of an ISO hatch pattern
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P NumberOfLoops : long  [get READ-ONLY]  vla-get-NumberOfLoops  ; Gets the number of loops in the hatch boundary
    P Origin : VARIANT  [get/put]  vla-get-Origin / vla-put-Origin  ; Specifies the origin coordinates for the pattern of the hatch entity
    P PatternAngle : ACAD_ANGLE  [get/put]  vla-get-PatternAngle / vla-put-PatternAngle  ; Specifies the pattern angle of the hatch
    P PatternDouble : VARIANT_BOOL  [get/put]  vla-get-PatternDouble / vla-put-PatternDouble  ; Determines whether the hatch pattern is double or not
    P PatternName : BSTR  [get READ-ONLY]  vla-get-PatternName  ; Specifies the pattern name of the hatch
    P PatternScale : ACAD_NOUNITS  [get/put]  vla-get-PatternScale / vla-put-PatternScale  ; Specifies the pattern scale of the hatch
    P PatternSpace : double  [get/put]  vla-get-PatternSpace / vla-put-PatternSpace  ; Specifies the pattern space of the hatch
    P PatternType : AcPatternType  [get READ-ONLY]  vla-get-PatternType  ; Specifies the pattern type of the hatch

[INTERFACE] IAcadHelix
Object: AcadHelix
Inherits: IAcadEntity
Doc: IAcadSpring Interface
  Properties
    P BaseRadius : double  [get/put]  vla-get-BaseRadius / vla-put-BaseRadius  ; Specifies the base radius of the helix
    P Constrain : AcHelixConstrainType  [get/put]  vla-get-Constrain / vla-put-Constrain  ; Controls which property is constrained when editing other property values
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the height of the helix
    P Position : VARIANT  [get/put]  vla-get-Position / vla-put-Position  ; Specifies the X, Y, and Z for the center of the base of the helix
    P TopRadius : double  [get/put]  vla-get-TopRadius / vla-put-TopRadius  ; Specifies the top radius of the helix
    P TotalLength : double  [get READ-ONLY]  vla-get-TotalLength  ; Specifies the total length of the helix
    P TurnHeight : double  [get/put]  vla-get-TurnHeight / vla-put-TurnHeight  ; Specifies the height of one full turn for the helix
    P Turns : ACAD_NOUNITS  [get/put]  vla-get-Turns / vla-put-Turns  ; Specifies the number of turns for the helix
    P TurnSlope : ACAD_ANGLE  [get READ-ONLY]  vla-get-TurnSlope  ; Displays the constant incline angle for the helix path
    P Twist : AcHelixTwistType  [get/put]  vla-get-Twist / vla-put-Twist  ; Controls the twist direction of the helix

[INTERFACE] IAcadHyperlink
Object: AcadHyperlink
Doc: A URL and URL description
  Methods
    M Delete  (vla-Delete obj)  ; Deletes a specified object
  Properties
    P Application : IDispatch  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P URL : BSTR  [get/put]  vla-get-URL / vla-put-URL  ; Specifies the URL for the Hyperlink object
    P URLDescription : BSTR  [get/put]  vla-get-URLDescription / vla-put-URLDescription  ; Specifies the URL description for the Hyperlink object
    P URLNamedLocation : BSTR  [get/put]  vla-get-URLNamedLocation / vla-put-URLNamedLocation  ; Specifies the named location for the Hyperlink object

[INTERFACE] IAcadHyperlinks
Object: AcadHyperlinks
Doc: The collection of all hyperlinks for a given entity
  Methods
    M Add  (vla-Add obj Name<BSTR> [Description<VARIANT>] [NamedLocation<VARIANT>]) -> IAcadHyperlink  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<long>) -> IAcadHyperlink  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Application : IDispatch  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadIdPair
Object: AcadIdPair
Doc: A special object for use with the CopyObjects method that contains the object IDs of both the source and destination objects
  Properties
    P Application : IDispatch  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P IsCloned : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsCloned  ; Determines if the source object in a CopyObjects operation has been cloned
    P IsOwnerXlated : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsOwnerXlated  ; Determines if the owning object in a CopyObjects operation has been translated
    P IsPrimary : VARIANT_BOOL  [get READ-ONLY]  vla-get-IsPrimary  ; Determines if the source object in a CopyObjects operation was part of the primary set of objects being copied, or if it was simply owned by a member in the primary set
    P key : LONG_PTR  [get READ-ONLY]  vla-get-key  ; The object ID of the source object in the CopyObjects operation
    P Value : LONG_PTR  [get READ-ONLY]  vla-get-Value  ; The object ID of the newly created cloned object in the CopyObjects operation

[INTERFACE] IAcadLayer
Object: AcadLayer
Inherits: IAcadObject
Doc: A logical grouping of data, similar to transparent acetate overlays on a drawing
  Properties
    P color : AcColor  [get/put]  vla-get-color / vla-put-color  ; Specifies the color of an entity or layer
    P Description : BSTR  [get/put]  vla-get-Description / vla-put-Description  ; Returns and sets the description of the layer.
    P Freeze : VARIANT_BOOL  [get/put]  vla-get-Freeze / vla-put-Freeze  ; Specifies the freeze status of a layer
    P LayerOn : VARIANT_BOOL  [get/put]  vla-get-LayerOn / vla-put-LayerOn  ; Specifies the state of a layer
    P Linetype : BSTR  [get/put]  vla-get-Linetype / vla-put-Linetype  ; Specifies the linetype of an entity
    P Lineweight : ACAD_LWEIGHT  [get/put]  vla-get-Lineweight / vla-put-Lineweight  ; Specifies the lineweight for the object
    P Lock : VARIANT_BOOL  [get/put]  vla-get-Lock / vla-put-Lock  ; Locks or unlocks a layer
    P Material : BSTR  [get/put]  vla-get-Material / vla-put-Material  ; Specifies the material
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object
    P PlotStyleName : BSTR  [get/put]  vla-get-PlotStyleName / vla-put-PlotStyleName  ; Specifies the plotstyle name for the object
    P Plottable : VARIANT_BOOL  [get/put]  vla-get-Plottable / vla-put-Plottable  ; Specifies wether the layer is plottable.
    P TrueColor : IAcadAcCmColor  [get/put]  vla-get-TrueColor / vla-put-TrueColor  ; Specifies the color of an entity or layer
    P Used : VARIANT_BOOL  [get READ-ONLY]  vla-get-Used  ; returns whether the layer is in use. See GenerateUsageData.
    P ViewportDefault : VARIANT_BOOL  [get/put]  vla-get-ViewportDefault / vla-put-ViewportDefault  ; Specifies if the layer is to be frozen in new viewports

[INTERFACE] IAcadLayers
Object: AcadLayers
Inherits: IAcadObject
Doc: The collection of all layers in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadLayer  ; Creates a member object and adds it to the appropriate collection
    M GenerateUsageData  (vla-GenerateUsageData obj)  ; Generates layer usage data. See also Used property of Layer.
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadLayer  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadLayerStateManager
Object: AcadLayerStateManager
Doc: IAcadLayerStateManager Interface
  Methods
    M Delete  (vla-Delete obj bsName<BSTR>)  ; Deletes the specified layers state.
    M Export  (vla-Export obj bsName<BSTR> bsFilename<BSTR>)  ; Exports the specified layer state to the specified file.
    M Import  (vla-Import obj bsFilename<BSTR>)  ; Imports all layer states from the specified file.
    M Rename  (vla-Rename obj bsName<BSTR> bsNewName<BSTR>)  ; Renames the specified layers state.
    M Restore  (vla-Restore obj bsName<BSTR>)  ; Restores the layers to the specified state as per the layer state mask.
    M Save  (vla-Save obj bsName<BSTR> eMask<AcLayerStateMask>)  ; Saves the attribute mask and the state of all layers into the specified layer state.
    M SetDatabase  (vla-SetDatabase obj iHostDb<IAcadDatabase*>)  ; Sets the working database for the layer state manager.
  Properties
    P Mask[bsName<BSTR>] : AcLayerStateMask  [get/put]  vla-get-Mask / vla-put-Mask  ; Sets the mask used for restoring the specified layer state.

[INTERFACE] IAcadLayout
Object: AcadLayout
Inherits: IAcadPlotConfiguration
Doc: The plot settings and visual properties of a model space or paper space block
  Properties
    P Block : IAcadBlock  [get READ-ONLY]  vla-get-Block  ; Gets the block associated with the layout
    P TabOrder : long  [get/put]  vla-get-TabOrder / vla-put-TabOrder  ; Specifies the tab order of a layout

[INTERFACE] IAcadLayouts
Object: AcadLayouts
Inherits: IAcadObject
Doc: The collection of all layouts in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadLayout  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadLayout  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadLeader
Object: AcadLeader
Inherits: IAcadEntity
Doc: AutoCAD Leader Interface
  Methods
    M Evaluate  (vla-Evaluate obj)  ; Evaluate the leader
  Properties
    P Annotation : IAcadEntity  [get/put]  vla-get-Annotation / vla-put-Annotation  ; Specifies the annotation object for a leader
    P ArrowheadBlock : BSTR  [get/put]  vla-get-ArrowheadBlock / vla-put-ArrowheadBlock  ; Specifies the block to use as the custom arrowhead for a radial dimension or leader line
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies the size of the leader arrowhead
    P ArrowheadType : AcDimArrowheadType  [get/put]  vla-get-ArrowheadType / vla-put-ArrowheadType  ; Specifies the type of the leader arrowhead
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the coordinates of the leader
    P DimensionLineColor : ACAD_COLOR  [get/put]  vla-get-DimensionLineColor / vla-put-DimensionLineColor  ; Specifies the color of the leader lines
    P DimensionLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-DimensionLineWeight / vla-put-DimensionLineWeight  ; Specifies the lineweight of the leader line
    P Normal : VARIANT  [get READ-ONLY]  vla-get-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P ScaleFactor : ACAD_NOUNITS  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the overall scale factor applied to properties that specify sizes, distances, or offsets
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of the leader
    P TextGap : double  [get/put]  vla-get-TextGap / vla-put-TextGap  ; Specifies the distance around the dimension text that the dimension line is broken
    P Type : AcLeaderType  [get/put]  vla-get-Type / vla-put-Type  ; Specifies the type of the leader
    P VerticalTextPosition : AcDimVerticalJustification  [get/put]  vla-get-VerticalTextPosition / vla-put-VerticalTextPosition  ; Specifies the vertical dimension text position

[INTERFACE] IAcadLine
Object: AcadLine
Inherits: IAcadEntity
Doc: AutoCAD Line Interface
  Methods
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Creates a new line by offsetting the current line by a specified distance
  Properties
    P Angle : ACAD_ANGLE  [get READ-ONLY]  vla-get-Angle  ; Specifies the angle of the line
    P Delta : VARIANT  [get READ-ONLY]  vla-get-Delta  ; Specifies the delta of the line
    P EndPoint : VARIANT  [get/put]  vla-get-EndPoint / vla-put-EndPoint  ; Specify the X, Y, Z coordinate of the end point of the line or use the Pick Point button to set X, Y, Z values simultaneously
    P Length : double  [get READ-ONLY]  vla-get-Length  ; Specifies the length of the line
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P StartPoint : VARIANT  [get/put]  vla-get-StartPoint / vla-put-StartPoint  ; Specify the X, Y, Z coordinate of the start point of the line or use the Pick Point button to set X, Y, Z values simultaneously
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the line

[INTERFACE] IAcadLineType
Object: AcadLineType
Inherits: IAcadObject
Doc: The line characteristics consisting of combinations of dashes, dots, and spaces
  Properties
    P Description : BSTR  [get/put]  vla-get-Description / vla-put-Description  ; Specifies the linetype description
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object

[INTERFACE] IAcadLineTypes
Object: AcadLineTypes
Inherits: IAcadObject
Doc: The collection of all linetypes in the drawing
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadLineType  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadLineType  ; Gets the member object at a given index in a collection, group, or selection set
    M Load  (vla-Load obj Name<BSTR> FileName<BSTR>)  ; Loads a menu group from a menu file or the definition of a linetype from a library (LIN) file
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadLoftedSurface
Object: AcadLoftedSurface
Inherits: IAcadSurface
Doc: IAcadLoftedSurface Interface
  Properties
    P Closed : VARIANT_BOOL  [get/put]  vla-get-Closed / vla-put-Closed  ; Specfies whether loft object is open or closed
    P EndDraftAngle : ACAD_ANGLE  [get/put]  vla-get-EndDraftAngle / vla-put-EndDraftAngle  ; Specifies the draft angle of the surface for the last cross section
    P EndDraftMagnitude : double  [get/put]  vla-get-EndDraftMagnitude / vla-put-EndDraftMagnitude  ; Specifies the draft magnitude of the surface for the last cross section
    P EndSmoothContinuity : long  [get/put]  vla-get-EndSmoothContinuity / vla-put-EndSmoothContinuity  ; Specifies smooth continuity of the surface for the last cross section
    P EndSmoothMagnitude : double  [get/put]  vla-get-EndSmoothMagnitude / vla-put-EndSmoothMagnitude  ; Specifies smooth magnitude of the surface for the last cross section
    P NumCrossSections : long  [get READ-ONLY]  vla-get-NumCrossSections  ; Displays the number of cross-section curves used
    P NumGuidePaths : long  [get READ-ONLY]  vla-get-NumGuidePaths  ; Displays the number of guide paths used
    P Periodic : VARIANT_BOOL  [get/put]  vla-get-Periodic / vla-put-Periodic  ; Specfies whether loft object is periodic.
    P StartDraftAngle : ACAD_ANGLE  [get/put]  vla-get-StartDraftAngle / vla-put-StartDraftAngle  ; Specifies the draft angle of the surface for the first cross section
    P StartDraftMagnitude : double  [get/put]  vla-get-StartDraftMagnitude / vla-put-StartDraftMagnitude  ; Specifies the draft magnitude of the surface for the first cross section
    P StartSmoothContinuity : long  [get/put]  vla-get-StartSmoothContinuity / vla-put-StartSmoothContinuity  ; Specifies smooth continuity of the surface for the first cross section
    P StartSmoothMagnitude : double  [get/put]  vla-get-StartSmoothMagnitude / vla-put-StartSmoothMagnitude  ; Specifies smooth magnitude of the surface for the first cross section
    P SurfaceNormals : AcLoftedSurfaceNormalType  [get/put]  vla-get-SurfaceNormals / vla-put-SurfaceNormals  ; Specifies for which cross-section curves the surface is normal to the cross section

[INTERFACE] IAcadLWPolyline
Object: AcadLWPolyline
Inherits: IAcadEntity
Doc: AutoCAD Lightweight Polyline Interface
  Methods
    M AddVertex  (vla-AddVertex obj Index<long> vertex<VARIANT>)  ; Adds a vertex to the lightweight polyline
    M Explode  (vla-Explode obj) -> VARIANT  ; Explodes the lightweight polyline, and returns the sub-entities as an array of Object
    M GetBulge  (vla-GetBulge obj Index<long>) -> double  ; Returns the vertex bulge of the lightweight polyline
    M GetWidth  (vla-GetWidth obj Index<long> 'StartWidth<out double> 'EndWidth<out double>)  ; Returns segment width of the lightweight polyline
    M Offset  (vla-Offset obj Distance<double>) -> VARIANT  ; Creates a new entity object by offsetting the lightweight polyline by a given distance
    M SetBulge  (vla-SetBulge obj Index<long> bulge<double>)  ; Sets the vertex bulge of the lightweight polyline
    M SetWidth  (vla-SetWidth obj Index<long> StartWidth<double> EndWidth<double>)  ; Sets the segment width of the lightweight polyline
  Properties
    P Area : double  [get READ-ONLY]  vla-get-Area  ; Specifies the area of the lightweight polyline
    P Closed : VARIANT_BOOL  [get/put]  vla-get-Closed / vla-put-Closed  ; Determines whether polyline is Open or Closed. Closed draws a line segment from current position to starting point of the polyline.
    P ConstantWidth : double  [get/put]  vla-get-ConstantWidth / vla-put-ConstantWidth  ; Specifies the constant width for the polyline
    P Coordinate[Index<long>] : VARIANT  [get/put]  vla-get-Coordinate / vla-put-Coordinate  ; Specifies the coordinate of a single vertex in the object
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the current vertex of the lightweight polyline
    P Elevation : double  [get/put]  vla-get-Elevation / vla-put-Elevation  ; Specifies the elevation of the polyline relative to the Z axis of the objects' coordinate system (Z coordinate of current vertex)
    P Length : double  [get READ-ONLY]  vla-get-Length  ; Specifies the length of the lightweight polyline
    P LinetypeGeneration : VARIANT_BOOL  [get/put]  vla-get-LinetypeGeneration / vla-put-LinetypeGeneration  ; Determines whether linetype generation is Enabled or Disabled for the polyline
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Thickness : double  [get/put]  vla-get-Thickness / vla-put-Thickness  ; Specifies the thickness of the lightweight polyline

[INTERFACE] IAcadMaterial
Object: AcadMaterial
Inherits: IAcadObject
Doc: The render material characteristics
  Properties
    P Description : BSTR  [get/put]  vla-get-Description / vla-put-Description  ; Specifies the material description
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Specifies the name of the object

[INTERFACE] IAcadMaterials
Object: AcadMaterials
Inherits: IAcadObject
  Methods
    M Add  (vla-Add obj Name<BSTR>) -> IAcadMaterial  ; Creates a member object and adds it to the appropriate collection
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadMaterial  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set

[INTERFACE] IAcadMenuBar
Object: AcadMenuBar
Doc: A collection of PopupMenu objects representing the current AutoCAD menu bar
  Methods
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadPopupMenu  ; Gets the member object at a given index in a collection, group, or selection set
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Parent : IAcadApplication  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object

[INTERFACE] IAcadMenuGroup
Object: AcadMenuGroup
Doc: An AutoCAD menu group
  Methods
    M Save  (vla-Save obj MenuFileType<AcMenuFileType>)  ; Saves the document or menu group
    M SaveAs  (vla-SaveAs obj MenuFileName<BSTR> MenuFileType<AcMenuFileType>)  ; Saves the document or menu group to a specified file
    M Unload  (vla-Unload obj)  ; Unloads the menu group or external reference
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P MenuFileName : BSTR  [get READ-ONLY]  vla-get-MenuFileName  ; Gets the menu file name where the menu group is located
    P Menus : IAcadPopupMenus  [get READ-ONLY]  vla-get-Menus  ; Gets the PopupMenus collection
    P Name : BSTR  [get READ-ONLY]  vla-get-Name  ; Specifies the name of the object
    P Parent : IAcadMenuGroups  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object
    P Toolbars : IAcadToolbars  [get READ-ONLY]  vla-get-Toolbars  ; Gets the Toolbars collection
    P Type : AcMenuGroupType  [get READ-ONLY]  vla-get-Type  ; Specifies type of a Leader, MenuGroup, PopupMenuItem, ToolbarItem, Polyline, or PolygonMesh object

[INTERFACE] IAcadMenuGroups
Object: AcadMenuGroups
Doc: A collection of MenuGroup objects representing all the menu groups loaded in the current AutoCAD session
  Methods
    M Item  (vla-Item obj Index<VARIANT>) -> IAcadMenuGroup  ; Gets the member object at a given index in a collection, group, or selection set
    M Load  (vla-Load obj MenuFileName<BSTR> [BaseMenu<VARIANT>]) -> IAcadMenuGroup  ; Loads a menu group from a menu file or the definition of a linetype from a library (LIN) file
  Properties
    P Application : IAcadApplication  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Count : long  [get READ-ONLY]  vla-get-Count  ; Gets the number of items in the collection, dictionary, group, or selection set
    P Parent : IAcadApplication  [get READ-ONLY]  vla-get-Parent  ; Gets the parent of the object

[INTERFACE] IAcadMInsertBlock
Object: AcadMInsertBlock
Inherits: IAcadBlockReference
Doc: IAcadMInsertBlock Interface
  Properties
    P Columns : long  [get/put]  vla-get-Columns / vla-put-Columns  ; Specifies the number of columns in the block array
    P ColumnSpacing : double  [get/put]  vla-get-ColumnSpacing / vla-put-ColumnSpacing  ; Specifies the column spacing in the block array
    P Rows : long  [get/put]  vla-get-Rows / vla-put-Rows  ; Determines the number of columns in the block array
    P RowSpacing : double  [get/put]  vla-get-RowSpacing / vla-put-RowSpacing  ; Specifies the row spacing in the block array

[INTERFACE] IAcadMLeader
Object: AcadMLeader
Inherits: IAcadEntity
Doc: AutoCAD Multi-Leader Interface
  Methods
    M AddLeader  (vla-AddLeader obj) -> long  ; Adds a new leader cluster to this multileader object
    M AddLeaderLine  (vla-AddLeaderLine obj leaderIndex<long> pointArray<VARIANT>) -> long  ; Adds a leader line to the leader cluster with specified index
    M AddLeaderLineEx  (vla-AddLeaderLineEx obj pointArray<VARIANT>) -> long  ; Adds a new leader line to this multileader object determined by input point which will be the first point of new leader line
    M GetBlockAttributeValue  (vla-GetBlockAttributeValue obj attdefId<LONG_PTR>) -> BSTR  ; Gets attribute value in block content determined by attribute definition id
    M GetDoglegDirection  (vla-GetDoglegDirection obj leaderIndex<long>) -> VARIANT  ; Gets the dog leg direction of the specific leader
    M GetLeaderIndex  (vla-GetLeaderIndex obj leaderLineIndex<long>) -> long  ; Gets the index of leader cluster which the specified leader line is in
    M GetLeaderLineIndexes  (vla-GetLeaderLineIndexes obj leaderIndex<long>) -> VARIANT  ; Gets the indexes of leader lines of the specific leader
    M GetLeaderLineVertices  (vla-GetLeaderLineVertices obj leaderLineIndex<long>) -> VARIANT  ; Specifies the vertices of leader line with specified index
    M GetVertexCount  (vla-GetVertexCount obj leaderLineIndex<long>) -> long  ; Gets the number of vertices in the specified leader line
    M RemoveLeader  (vla-RemoveLeader obj leaderIndex<long>)  ; Removes the leader cluster with specified index
    M RemoveLeaderLine  (vla-RemoveLeaderLine obj leaderLineIndex<long>)  ; Removes the leader line with specified index
    M SetBlockAttributeValue  (vla-SetBlockAttributeValue obj attdefId<LONG_PTR> Value<BSTR>)  ; Sets attribute value in block content with attribute definition id
    M SetDoglegDirection  (vla-SetDoglegDirection obj leaderIndex<long> dirVec<VARIANT>)  ; Sets the dog leg direction of the specific leader
    M SetLeaderLineVertices  (vla-SetLeaderLineVertices obj leaderLineIndex<long> pointArray<VARIANT>)  ; Specifies the vertices of leader line with specified index
  Properties
    P ArrowheadBlock : BSTR  [get/put]  vla-get-ArrowheadBlock / vla-put-ArrowheadBlock  ; Specifies the block to use as the custom arrowhead for leader lines of multileader
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies the size of leader arrowhead
    P ArrowheadType : AcDimArrowheadType  [get/put]  vla-get-ArrowheadType / vla-put-ArrowheadType  ; Specifies the type of leader arrowhead
    P BlockConnectionType : AcBlockConnectionType  [get/put]  vla-get-BlockConnectionType / vla-put-BlockConnectionType  ; Specify how leaders connect with content block
    P BlockScale : ACAD_NOUNITS  [get/put]  vla-get-BlockScale / vla-put-BlockScale  ; Specify how leaders connect with content block
    P ContentBlockName : BSTR  [get/put]  vla-get-ContentBlockName / vla-put-ContentBlockName  ; Specify the name of multileader's content block
    P ContentBlockType : AcPredefBlockType  [get/put]  vla-get-ContentBlockType / vla-put-ContentBlockType  ; Specifies the content block of multileader
    P ContentType : AcMLeaderContentType  [get/put]  vla-get-ContentType / vla-put-ContentType  ; Specifies the content type of this multileader object
    P DogLegged : VARIANT_BOOL  [get/put]  vla-get-DogLegged / vla-put-DogLegged  ; Enable/Disable horizontal landing of multileader
    P DoglegLength : double  [get/put]  vla-get-DoglegLength / vla-put-DoglegLength  ; Specify the landing distance
    P LandingGap : double  [get/put]  vla-get-LandingGap / vla-put-LandingGap  ; Specify the text landing gap
    P LeaderCount : long  [get READ-ONLY]  vla-get-LeaderCount  ; Gets the number of leader line clusters in this multileader object
    P LeaderLineColor : IAcadAcCmColor  [get/put]  vla-get-LeaderLineColor / vla-put-LeaderLineColor  ; Specifies the color of the leader lines
    P LeaderLinetype : ACAD_LTYPE  [get/put]  vla-get-LeaderLinetype / vla-put-LeaderLinetype  ; Specifies the linetype of leader lines
    P LeaderLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-LeaderLineWeight / vla-put-LeaderLineWeight  ; Specifies the line weight of leader lines
    P LeaderType : AcMLeaderType  [get/put]  vla-get-LeaderType / vla-put-LeaderType  ; Specifies the leader type
    P ScaleFactor : double  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Specifies the overall scale factor of this multileader object
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of this multileader object
    P TextAttachmentDirection : AcTextAttachmentDirection  [get/put]  vla-get-TextAttachmentDirection / vla-put-TextAttachmentDirection  ; Specifies if leaders connect with the text horizontally or vertically
    P TextBackgroundFill : VARIANT_BOOL  [get/put]  vla-get-TextBackgroundFill / vla-put-TextBackgroundFill  ; Specifies use Background Mask
    P TextBottomAttachmentType : AcVerticalTextAttachmentType  [get/put]  vla-get-TextBottomAttachmentType / vla-put-TextBottomAttachmentType  ; Specifies how leaders on the bottom connect with the text
    P TextDirection : AcDrawingDirection  [get/put]  vla-get-TextDirection / vla-put-TextDirection  ; Specifies the drawing direction of the Mtext
    P TextFrameDisplay : VARIANT_BOOL  [get/put]  vla-get-TextFrameDisplay / vla-put-TextFrameDisplay  ; Display/hide text frame of multileader content
    P TextHeight : double  [get/put]  vla-get-TextHeight / vla-put-TextHeight  ; Specifies the height of the Mtext
    P TextJustify : AcAttachmentPoint  [get/put]  vla-get-TextJustify / vla-put-TextJustify  ; Specifies the attachment point of the Mtext
    P TextLeftAttachmentType : AcTextAttachmentType  [get/put]  vla-get-TextLeftAttachmentType / vla-put-TextLeftAttachmentType  ; Specify how leaders on the left side connect with the text
    P TextLineSpacingDistance : double  [get/put]  vla-get-TextLineSpacingDistance / vla-put-TextLineSpacingDistance  ; Specifies the line spacing distance of the Mtext
    P TextLineSpacingFactor : double  [get/put]  vla-get-TextLineSpacingFactor / vla-put-TextLineSpacingFactor  ; Specifies the line spacing factor of the Mtext
    P TextLineSpacingStyle : AcLineSpacingStyle  [get/put]  vla-get-TextLineSpacingStyle / vla-put-TextLineSpacingStyle  ; Specifies the line spacing style of the Mtext
    P TextRightAttachmentType : AcTextAttachmentType  [get/put]  vla-get-TextRightAttachmentType / vla-put-TextRightAttachmentType  ; Gets the dog leg direction of the specific leader
    P TextRotation : ACAD_ANGLE  [get/put]  vla-get-TextRotation / vla-put-TextRotation  ; Specifies the rotation angle of the Mtext
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the Mtext
    P TextStyleName : BSTR  [get/put]  vla-get-TextStyleName / vla-put-TextStyleName  ; Specifies the style name of the Mtext
    P TextTopAttachmentType : AcVerticalTextAttachmentType  [get/put]  vla-get-TextTopAttachmentType / vla-put-TextTopAttachmentType  ; Specifies how leaders on the top connect with the text
    P TextWidth : double  [get/put]  vla-get-TextWidth / vla-put-TextWidth  ; Specifies the width of the Mtext

[INTERFACE] IAcadMLeaderLeader
Object: AcadMLeaderLeader
Inherits: IAcadSubEntity
Doc: AutoCAD Interface for Multi-Leader's Leader Line
  Properties
    P ArrowheadBlock : BSTR  [get/put]  vla-get-ArrowheadBlock / vla-put-ArrowheadBlock  ; Specifies the block to use as the custom arrowhead for leader lines of multileader
    P ArrowheadSize : double  [get/put]  vla-get-ArrowheadSize / vla-put-ArrowheadSize  ; Specifies the size of leader arrowhead
    P ArrowheadType : AcDimArrowheadType  [get/put]  vla-get-ArrowheadType / vla-put-ArrowheadType  ; Specifies the type of leader arrowhead
    P LeaderLineColor : IAcadAcCmColor  [get/put]  vla-get-LeaderLineColor / vla-put-LeaderLineColor  ; Specifies the color of the leader lines
    P LeaderLinetype : ACAD_LTYPE  [get/put]  vla-get-LeaderLinetype / vla-put-LeaderLinetype  ; Specifies the linetype of leader lines
    P LeaderLineWeight : ACAD_LWEIGHT  [get/put]  vla-get-LeaderLineWeight / vla-put-LeaderLineWeight  ; Specifies the line weight of leader lines
    P LeaderType : AcMLeaderType  [get/put]  vla-get-LeaderType / vla-put-LeaderType  ; Specifies the leader type

[INTERFACE] IAcadMLeaderStyle
Object: AcadMLeaderStyle
Inherits: IAcadObject
Doc: AutoCAD MLeaderStyle Interface
  Properties
    P AlignSpace : double  [get/put]  vla-get-AlignSpace / vla-put-AlignSpace  ; Returns the alignment space value for the specified mleaderstyle.
    P Annotative : VARIANT_BOOL  [get/put]  vla-get-Annotative / vla-put-Annotative  ; Returns the annotative status for the specified mleaderstyle.
    P ArrowSize : double  [get/put]  vla-get-ArrowSize / vla-put-ArrowSize  ; Returns the arrow size for the specified mleaderstyle.
    P ArrowSymbol : BSTR  [get/put]  vla-get-ArrowSymbol / vla-put-ArrowSymbol  ; Returns the arrow symbol for the specified mleaderstyle.
    P BitFlags : long  [get/put]  vla-get-BitFlags / vla-put-BitFlags  ; Returns the operation bit set for the specified mleaderstyle.
    P Block : BSTR  [get/put]  vla-get-Block / vla-put-Block  ; Returns the block content for the specified mleaderstyle.
    P BlockColor : IAcadAcCmColor  [get/put]  vla-get-BlockColor / vla-put-BlockColor  ; Returns the block color in block content for the specified mleaderstyle.
    P BlockConnectionType : AcBlockConnectionType  [get/put]  vla-get-BlockConnectionType / vla-put-BlockConnectionType  ; Returns the block connection type for the specified mleaderstyle.
    P BlockRotation : double  [get/put]  vla-get-BlockRotation / vla-put-BlockRotation  ; Returns the rotation of the block referenced by multileader for the specified mleaderstyle.
    P BlockScale : double  [get/put]  vla-get-BlockScale / vla-put-BlockScale  ; Returns the scale of the block referenced by multileader for the specified mleaderstyle.
    P BreakSize : double  [get/put]  vla-get-BreakSize / vla-put-BreakSize  ; Returns the break size used for breaking leader lines for the specified mleaderstyle.
    P ContentType : AcMLeaderContentType  [get/put]  vla-get-ContentType / vla-put-ContentType  ; Returns the content type for the specified mleaderstyle.
    P Description : BSTR  [get/put]  vla-get-Description / vla-put-Description  ; Returns the description for the specified mleaderstyle.
    P DoglegLength : double  [get/put]  vla-get-DoglegLength / vla-put-DoglegLength  ; Returns the length of dog-leg leader line for the specified mleaderstyle.
    P DrawLeaderOrderType : AcDrawLeaderOrderType  [get/put]  vla-get-DrawLeaderOrderType / vla-put-DrawLeaderOrderType  ; Returns the order of leader line creation when creating a multileader for the specified mleaderstyle.
    P DrawMLeaderOrderType : AcDrawMLeaderOrderType  [get/put]  vla-get-DrawMLeaderOrderType / vla-put-DrawMLeaderOrderType  ; Returns the order of multileader creation for the specified mleaderstyle.
    P EnableBlockRotation : VARIANT_BOOL  [get/put]  vla-get-EnableBlockRotation / vla-put-EnableBlockRotation  ; Indicate whether the block rotation value works for the specified mleaderstyle.
    P EnableBlockScale : VARIANT_BOOL  [get/put]  vla-get-EnableBlockScale / vla-put-EnableBlockScale  ; Indicate whether the block scale value works for the specified mleaderstyle.
    P EnableDogleg : VARIANT_BOOL  [get/put]  vla-get-EnableDogleg / vla-put-EnableDogleg  ; Indicate whether dog-leg leader lines are enabled for the specified mleaderstyle.
    P EnableFrameText : VARIANT_BOOL  [get/put]  vla-get-EnableFrameText / vla-put-EnableFrameText  ; Indicate whether or not the text frame is displayed around the MText for the specified mleaderstyle.
    P EnableLanding : VARIANT_BOOL  [get/put]  vla-get-EnableLanding / vla-put-EnableLanding  ; Indicate whether landing of leader line is enabled for the specified mleaderstyle.
    P FirstSegmentAngleConstraint : AcSegmentAngleType  [get/put]  vla-get-FirstSegmentAngleConstraint / vla-put-FirstSegmentAngleConstraint  ; Returns the first segment angle constraint when creating a multileader for the specified mleaderstyle.
    P LandingGap : double  [get/put]  vla-get-LandingGap / vla-put-LandingGap  ; Returns the gap between MText and the tail of leader lines for the specified mleaderstyle.
    P LeaderLineColor : IAcadAcCmColor  [get/put]  vla-get-LeaderLineColor / vla-put-LeaderLineColor  ; Returns the color of leader lines for the specified mleaderstyle.
    P LeaderLinetype : AcMLeaderType  [get/put]  vla-get-LeaderLinetype / vla-put-LeaderLinetype  ; Returns the leader line type for the specified mleaderstyle.
    P LeaderLineTypeId : ACAD_LTYPE  [get/put]  vla-get-LeaderLineTypeId / vla-put-LeaderLineTypeId  ; Returns the linetype of leader lines for the specified mleaderstyle.
    P LeaderLineWeight : AcLineWeight  [get/put]  vla-get-LeaderLineWeight / vla-put-LeaderLineWeight  ; Returns the line weight of leader lines for the specified mleaderstyle.
    P MaxLeaderSegmentsPoints : long  [get/put]  vla-get-MaxLeaderSegmentsPoints / vla-put-MaxLeaderSegmentsPoints  ; Returns the max number of segment points in leader lines for the specified mleaderstyle.
    P Name : BSTR  [get/put]  vla-get-Name / vla-put-Name  ; Returns the name for the specified mleaderstyle.
    P OverwritePropChanged : VARIANT_BOOL  [get READ-ONLY]  vla-get-OverwritePropChanged  ; Indicate whether properties were changed for the specified mleaderstyle.
    P ScaleFactor : double  [get/put]  vla-get-ScaleFactor / vla-put-ScaleFactor  ; Returns the scale of multileader created for the specified mleaderstyle.
    P SecondSegmentAngleConstraint : AcSegmentAngleType  [get/put]  vla-get-SecondSegmentAngleConstraint / vla-put-SecondSegmentAngleConstraint  ; Returns the second segment angle constraint when creating a multileader for the specified mleaderstyle.
    P TextAlignmentType : AcTextAlignmentType  [get/put]  vla-get-TextAlignmentType / vla-put-TextAlignmentType  ; Returns the text alignment type for the specified mleaderstyle.
    P TextAngleType : AcTextAngleType  [get/put]  vla-get-TextAngleType / vla-put-TextAngleType  ; Returns the angle type of text with respect to the last leader line segment for the specified mleaderstyle.
    P TextAttachmentDirection : AcTextAttachmentDirection  [get/put]  vla-get-TextAttachmentDirection / vla-put-TextAttachmentDirection  ; Returns the type of text attachment for the specified mleaderstyle.
    P TextBottomAttachmentType : AcVerticalTextAttachmentType  [get/put]  vla-get-TextBottomAttachmentType / vla-put-TextBottomAttachmentType  ; Returns the type of text attachment for the specified mleaderstyle.
    P TextColor : IAcadAcCmColor  [get/put]  vla-get-TextColor / vla-put-TextColor  ; Returns the text color of MText for the specified mleaderstyle.
    P TextHeight : double  [get/put]  vla-get-TextHeight / vla-put-TextHeight  ; Returns the text height of MText for the specified mleaderstyle.
    P TextLeftAttachmentType : AcTextAttachmentType  [get/put]  vla-get-TextLeftAttachmentType / vla-put-TextLeftAttachmentType  ; Returns the type of text attachment for the specified mleaderstyle.
    P TextRightAttachmentType : AcTextAttachmentType  [get/put]  vla-get-TextRightAttachmentType / vla-put-TextRightAttachmentType  ; Returns the type of text attachment for the specified mleaderstyle.
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Returns the text string of the Mtext for the specified mleaderstyle.
    P TextStyle : BSTR  [get/put]  vla-get-TextStyle / vla-put-TextStyle  ; Returns the text style for the specified mleaderstyle.
    P TextTopAttachmentType : AcVerticalTextAttachmentType  [get/put]  vla-get-TextTopAttachmentType / vla-put-TextTopAttachmentType  ; Returns the type of text attachment for the specified mleaderstyle.

[INTERFACE] IAcadMLine
Object: AcadMLine
Inherits: IAcadEntity
Doc: IAcadMLine Interface
  Properties
    P Coordinates : VARIANT  [get/put]  vla-get-Coordinates / vla-put-Coordinates  ; Specifies the coordinates for each vertex in the object
    P Justification : AcMLineJustification  [get/put]  vla-get-Justification / vla-put-Justification  ; Specifies the justification of the MLine
    P MLineScale : double  [get/put]  vla-get-MLineScale / vla-put-MLineScale  ; Specifies the scale of the MLine
    P StyleName : BSTR  [get READ-ONLY]  vla-get-StyleName  ; Specifies the Mline style name

[INTERFACE] IAcadModelSpace
Object: AcadModelSpace
Inherits: IAcadBlock
Doc: A special Block object containing all model space entities

[INTERFACE] IAcadMText
Object: AcadMText
Inherits: IAcadEntity
Doc: AutoCAD MText Interface
  Methods
    M FieldCode  (vla-FieldCode obj) -> BSTR  ; Returns the text string with field codes of the Mtext
  Properties
    P AttachmentPoint : AcAttachmentPoint  [get/put]  vla-get-AttachmentPoint / vla-put-AttachmentPoint  ; Specifies both text height and text orientation by designating the endpoints of the baseline
    P BackgroundFill : VARIANT_BOOL  [get/put]  vla-get-BackgroundFill / vla-put-BackgroundFill  ; Specifies use Background mask
    P DrawingDirection : AcDrawingDirection  [get/put]  vla-get-DrawingDirection / vla-put-DrawingDirection  ; Specifies the drawing direction of the Mtext
    P Height : double  [get/put]  vla-get-Height / vla-put-Height  ; Specifies the text height of the Mtext
    P InsertionPoint : VARIANT  [get/put]  vla-get-InsertionPoint / vla-put-InsertionPoint  ; Specify the X, Y, Z coordinate for the insertion point of the Mtext or use the Pick Point button to set X, Y, Z values simultaneously
    P LineSpacingDistance : double  [get/put]  vla-get-LineSpacingDistance / vla-put-LineSpacingDistance  ; Specifies the line spacing distance of the Mtext
    P LineSpacingFactor : double  [get/put]  vla-get-LineSpacingFactor / vla-put-LineSpacingFactor  ; Specifies the line spacing factor of the Mtext
    P LineSpacingStyle : AcLineSpacingStyle  [get/put]  vla-get-LineSpacingStyle / vla-put-LineSpacingStyle  ; Specifies the line spacing style of the Mtext
    P Normal : VARIANT  [get/put]  vla-get-Normal / vla-put-Normal  ; Specifies the three-dimensional normal unit vector for the entity
    P Rotation : ACAD_ANGLE  [get/put]  vla-get-Rotation / vla-put-Rotation  ; Specifies the rotation angle of the Mtext
    P StyleName : BSTR  [get/put]  vla-get-StyleName / vla-put-StyleName  ; Specifies the style name of the Mtext
    P TextString : BSTR  [get/put]  vla-get-TextString / vla-put-TextString  ; Specifies the text string of the Mtext
    P Width : double  [get/put]  vla-get-Width / vla-put-Width  ; Specifies the defined width of the Mtext

[INTERFACE] IAcadNurbSurface
Object: AcadNurbSurface
Inherits: IAcadSurface
Doc: AutoCAD NURBS Surface Interface
  Properties
    P CvHullDisplay : VARIANT_BOOL  [get/put]  vla-get-CvHullDisplay / vla-put-CvHullDisplay  ; Specifies whether displaying the CV Hull for NURBS surface

[INTERFACE] IAcadObject
Object: AcadObject
Doc: The standard interface for a basic AutoCAD object
  Methods
    M Delete  (vla-Delete obj)  ; Deletes a specified object
    M Erase  (vla-Erase obj)  ; Erases all the objects in a selection set
    M GetExtensionDictionary  (vla-GetExtensionDictionary obj) -> IAcadDictionary  ; Gets the extension dictionary associated with an object
    M GetXData  (vla-GetXData obj AppName<BSTR> 'XDataType<out VARIANT> 'XDataValue<out VARIANT>)  ; Gets the extended data (XData) associated with an object
    M SetXData  (vla-SetXData obj XDataType<VARIANT> XDataValue<VARIANT>)  ; Sets the extended data (XData) associated with an object
  Properties
    P Application : IDispatch  [get READ-ONLY]  vla-get-Application  ; Gets the Application object
    P Database : IAcadDatabase  [get READ-ONLY]  vla-get-Database  ; Gets the database in which the object belongs
    P Document : IDispatch  [get READ-ONLY]  vla-get-Document  ; Gets the document (drawing) in which the object belongs
    P Handle : BSTR  [get READ-ONLY]  vla-get-Handle  ; Gets the handle of an object
    P HasExtensionDictionary : VARIANT_BOOL  [get READ-ONLY]  vla-get-HasExtensionDictionary  ; Determines if the object has an extension dictionary associated with it
    P ObjectID : LONG_PTR  [get READ-ONLY]  vla-get-ObjectID  ; Gets the object ID of the object
    P ObjectName : BSTR  [get READ-ONLY]  vla-get-ObjectName  ; Gets the AutoCAD class name of the object
    P OwnerID : LONG_PTR  [get READ-ONLY]  vla-get-OwnerID  ; Gets the object ID of the owner (parent) object
