# Autodesk.AutoCAD.Internal

NAMESPACE: Autodesk.AutoCAD.Internal
--------------------------------------------------------------------------------

[CLASS] AcadTaskDialogs
Full Name: Autodesk.AutoCAD.Internal.AcadTaskDialogs

  CONSTRUCTORS:
    new AcadTaskDialogs()

  METHODS:
    static int ShowCurrentLayerOff()
    static int ShowDuplicateNameCopyReplaceTD(string objectname, string duplicatename, string copyname, IntPtr pParentWnd)
    static int ShowDuplicateNameErrorTD(string objectname, string duplicatename, IntPtr pParentWnd)
    static int ShowDuplicateNameReplaceCancelTD(string objectname, string duplicatename, IntPtr pParentWnd)
    static int ShowFileConfirmationWithNoToAll(string featureName, string featureType, string fullFileName, string fileName, IntPtr pParentWnd)
    static int ShowFileConfirmationWithoutNoToAll(string featureName, string featureType, string fullFileName, string fileName, IntPtr pParentWnd)
    static void ShowInvalidNameEmptyNameTD(string objectname, string fieldname, IntPtr pParentWnd)
    static void ShowInvalidNameTooLongTD(string objectname, string fieldname, IntPtr pParentWnd)
    static void ShowInvalidNameUnsupportedCharactersTD(string objectname, string fieldname, IntPtr pParentWnd)
    static void ShowLayerCannotFreeze()
    static Uri Source()

  CONSTANTS/STATIC FIELDS:
    const string AutoCADMissingOrCorruptDisplayDriver
    const string AutoCADMissingOrCorruptDisplayDriverMac
    const string AutoCADMissingPDFViewer
    const string AutoCADChangesNotSaved
    const int AutoCADChangesNotSaved_SaveAndCloseButon
    const int AutoCADChangesNotSaved_DiscardAndCloseButon
    const string Printing3DNoInternetConn
    const string AutoCADMissingDWFViewer
    const string AutoCADExceededSizeLimit
    const int AutoCADExceededSizeLimit_ShowDetailsButon
    const int AutoCADExceededSizeLimit_SaveDWGXButon
    const string AutoCADNearingSizeLimit
    const int AutoCADNearingSizeLimit_LargeObjectSupportOffButon
    const int AutoCADNearingSizeLimit_LargeObjectSupportOnButon
    const string AutoCADRelaxDragging
    const int AutoCADRelaxDragging_RelaxButon
    const int AutoCADRelaxDragging_DeleteButon
    const string AutoCADCannotRelax
    const string HatchBoundaryDefinitionErrorNRC
    const string HatchBoundaryDefinitionErrorRC
    const string BoundaryBoundaryDefinitionError
    const string AcadContainsLargeObjects
    const int AcadContainsLargeObjects_TurnOffSysvarButon
    const int AcadContainsLargeObjects_TurnOnSysvarButon
    const string Printing3DObjectsOnLockedLayer
    const string AutoCADCannotRotate
    const int AutoCADCannotRotate_CannotRotate_RelaxButon
    const int AutoCADCannotRotate_CannotRotate_MaintainButon
    const string AcSubDConvertToASMInvalidTopology
    const string AutoCADCannotGripEdit
    const int AutoCADCannotGripEdit_CannotGripEdit_continueButon
    const int AutoCADCannotGripEdit_CannotGripEdit_CancelButon
    const string AutoCADParametricExpression
    const int AutoCADParametricExpression_ParametricExpression_ContinueButon
    const int AutoCADParametricExpression_ParametricExpression_CancelButon
    const string AutoCADAssociativePathArray
    const int AutoCADAssociativePathArray_AssociativePathArray_ContinueButon
    const int AutoCADAssociativePathArray_AssociativePathArray_CancelButon
    const string AutoCADDependencyOnTrimAction
    const int AutoCADDependencyOnTrimAction_DependencyOnTrimAction_continueButon
    const int AutoCADDependencyOnTrimAction_DependencyOnTrimAction_CancelButon
    const string AutoCADSubtractSurface
    const int AutoCADSubtractSurface_SubtractSurface_ContinueButon
    const int AutoCADSubtractSurface_SubtractSurface_CancelButon
    const string AutoCADUnionSurface
    const int AutoCADUnionSurface_UnionSurface_ContinueButon
    const int AutoCADUnionSurface_UnionSurface_CancelButon
    const string AutoCADTooManyControlVertices
    const int AutoCADTooManyControlVertices_TooManyControlVertices_ContinueButon
    const int AutoCADTooManyControlVertices_TooManyControlVertices_CancelButon
    const string AutoCADHelpFileNotFound
    const int AutoCADHelpFileNotFound_UpdateHelpFileLocationButon
    const int AutoCADHelpFileNotFound_NotLaunchHelpButon
    const string AutoCADOnlineHelpNotAccessible
    const int AutoCADOnlineHelpNotAccessible_TryConnectInternetButon
    const int AutoCADOnlineHelpNotAccessible_UseOfflineHelpButon
    const string AutoCADOfflineHelpNotInstalled
    const int AutoCADOfflineHelpNotInstalled_DownloadOfflineHelpButon
    const int AutoCADOfflineHelpNotInstalled_UseOnlineHelpButon
    const int AutoCADOfflineHelpNotInstalled_CopyToClipboardButon
    const string AutoCADOfflineHelpNotInstalledMac
    const string AutoCADSaveOutOfProject
    const string AcCoreIncompatibleArxApplication
    const int AcCoreIncompatibleArxApplication_LoadAppButon
    const int AcCoreIncompatibleArxApplication_LoadAllButon
    const int AcCoreIncompatibleArxApplication_SkipThisButon
    const int AcCoreIncompatibleArxApplication_SkipAllButon
    const string AcCoreMissingLanguagePack
    const int AcCoreMissingLanguagePack_OpenDrawingButon
    const int AcCoreMissingLanguagePack_CancelAndInstallButon
    const string AcCoreRasteroutput
    const int AcCoreRasteroutput_IgnoreRasterButon
    const string AcCoreTrueConvert
    const string AcCoreCheckSpellBlock
    const string AcCoreCheckSpellXref
    const string AcCorePartialOpenLastSaved
    const int AcCorePartialOpenLastSaved_OpenButon
    const int AcCorePartialOpenLastSaved_RestoreButon
    const string AcCorePartialOpenLocked
    const string AcCorePartialOpenDrawing
    const int AcCorePartialOpenDrawing_PartialOpenButon
    const int AcCorePartialOpenDrawing_RestorePartialOpenStateButon
    const string AcCorePartialOpenEducationVersion
    const int AcCorePartialOpenEducationVersion_PartialOpenEducationContinueButon
    const int AcCorePartialOpenEducationVersion_CancelOperationButon
    const string AcBlockEducationalPlotStampDetected
    const int AcBlockEducationalPlotStampDetected_ContinueOperationButon
    const int AcBlockEducationalPlotStampDetected_CancelOperation_EMRButon
    const string AcCoreBHCreatFailure
    const string AcCorePointOutsideBoundary
    const string AcCorePointInBoundary
    const string AcCorePartialOpenFileLocked
    const string DWGRecoveryDamagedFile
    const string DWGRecoveryRecoverSummary
    const string AutoCADXrefLockFail
    const string DWGRecoveryErrorsFound
    const string SurfaceCVEditing
    const string AcCoreOpenACopy
    const string HatchDenseHatchCreation
    const string OptionOnlineTabDisableCloudDocuments
    const string AutodeskReqVersionNoOpen
    const string AutodeskReqVersionReadOnly
    const string AutodeskReqVersionOpenForWrite
    const string HatchInvalidPatternSelected
    const int HatchInvalidPatternSelected_RevealInFinderButon
    const int HatchInvalidPatternSelected_CancelButon
    const string HatchInvalidPatternSelectedSandboxed
    const int HatchInvalidPatternSelectedSandboxed_RemoveButon
    const string AutodeskSharedCloudFile
    const string SecurityWritablePathWarning
    const int SecurityWritablePathWarning_ContinueButon
    const string SecurityDocsNonTrustedPathWarning
    const string SecurityDocsPathDoesNotExist
    const string SecurityDocsCannotValidatePermissions
    const string SecurityDocsTrustedPathNotSecure
    const string SecurityStartAutoCADInAdminMode
    const string AutoCADInvalidProjectConfig
    const string AutoCADInvalidProjectConfigOnAdd
    const string AutodeskMismatchGeoCS
    const string AutodeskRemoveSectionJogs
    const string SecurityUnsignedExecutableFile
    const string SecurityInvalidCertificate
    const string SecuritySignedFileNotinTrustedFolder
    const string Printing3DPrintStudioNotInstalled
    const string AutodeskInitDisplayDriverError
    const string LicensingNetworkLicenseLost
    const string LicensingNetworkLicenseLostAcadShutDown
    const string LicensingUserLicenseSignoutAndQuit
    const string LicensingUserLicenseSignoutAndQuitAcadShutDown
    const string LicensingCHECKOUTTIMEDOUTSHUTDOWN
    const string LicensingGENRICSHUTDOWN
    const string AcCoreOpenReadOnlyDocsFile
    const int AcCoreOpenReadOnlyDocsFile_CheckoutButon
    const int AcCoreOpenReadOnlyDocsFile_ContinueReadOnlyButon
    const string AcPublishLoadSheetList
    const string AcPurgeConfirmPurge
    const string LinetypeReloadLinetype
    const string AcSMNavDrawingFileNotFound
    const string LayerManagerShowMessageClose
    const string LayerManagerShowMessageYesNo
    const string ActionRecorderPlaybackError
    const string ActionRecorderNoObjectsSelected
    const string ActionRecorderInvalidName
    const string ActionRecorderNameAlreadyExists
    const string ActionRecorderConflictWithCommandName
    const string ActionRecorderUserMessage
    const string ActionRecorderUnableToAccessDefaultFolder
    const string ActionRecorderConfirmDeletionOfActionMacro
    const string ActionRecorderConfirmDeletionOfItem
    const string ActionRecorderNoDirectorySpecified
    const string ActionRecorderError
    const string ActionRecorderConflictWithExistingMacroName
    const string ActionRecorderFileNameAlreadyExists
    const string ActionRecorderValueTypeMismatch
    const string ActionRecorderCommandNotRecordedOnDYNMODE
    const string ActionRecorderConfirmMultipleDeletion
    const string NavToolsNeedGreaterNumber
    const string NavToolsNeedGreaterEqualNumber
    const string NavToolsNeedNumInRange
    const string AcPublishNoPasswordSpecified
    const string ExportLayoutFileCreated
    const string ExportLayoutFileCreatedSDI
    const string AcGeoLocationUICoordSysAlreadyDefined
    const string AcGeoLocationUIDGLGEInstalled
    const string AcGeoLocationUIDGLGENotInstalled
    const string AcGeoLocationUIGoogleEarthNotOpen
    const string AcGeoLocationUIImportFromGoogleEarth
    const string AcGeoLocationUIInvalidKMLFile
    const string AcGeoLocationUIInvalidValue
    const string AcGeoLocationUILocationAlreadyExists
    const string AcGeoLocationUIRemove
    const string AcGeoLocationUITimeZoneChanged
    const string AcGeoLocationUIReorientMarker
    const string AcGeoLocationUIOptimizeGeomapImage
    const int AcGeoLocationUIOptimizeGeomapImage_OptimizeButon
    const int AcGeoLocationUIOptimizeGeomapImage_ReloadButon
    const string PlotPaperSizeNotFound
    const string AcSignAppDigitalIDNotAvailable
    const string AcBattmanSynchronizeBlocks
    const string AcBattManValueChange
    const string SheetSetManagerLostSetAssociation
    const string AcLayerAppsLayerStateSuccessfulImport
    const string AcLayerAppsLayerStateAddLayer
    const string LayerManagerSingleLayerNotDeleted
    const string LayerManagerMutipleLayerNotDeleted
    const string LayerManagerExcessLayerFilters
    const string LayerManagerCannotMakeCurrent
    const string LayerManagerCurrentLayerOff
    const string LayerManagerLayerRename
    const string LayerManagerLayerCannotFreeze
    const string LayerManagerCannotAdjustPlotSetting
    const string DGNInvalidDGNFile
    const string DGNUIInvalidSeedFile
    const string DGNNoDesignModelsFound
    const string DGNIncompatibleSeedFile
    const int DGNIncompatibleSeedFile_SelectAnotherSeedButon
    const int DGNIncompatibleSeedFile_SelectAnotherDGNExportButon
    const string DGN3DSeedFileRequired
    const string BatchStandardsCheckerReportFile
    const string AcDxWizardDataExtractionFilesNotFound
    const string AcSignApplyDigitalSignatureReadOnlyFile
    const string AcSignApplyDigitalSignaturePreviouslySignedFile
    const string SpellerCheckLayersLocked
    const string AcStdCheckComplete
    const string AcStdStandardsViolationFound
    const string ShowMotionQuickViewRenameError
    const string MTEXTAutoStackProperties1WithoutDoNotShow
    const string ShowMotionUpdateAll
    const string ShowMotionQuickViewDeleteCategoryViews
    const string ShowMotionDeleteAllView
    const string DGNUINumberOfElementsExceededLimit
    const string AcLayerAppsCannotRestoreLayerState
    const string AcLayerAppsLayerStateNoneSelected
    const string AcLayerAppsLayerStateDuplicates
    const string AcLayerAppsLayerStateNoneFound
    const string DGNUIConfirmMappingRemoval
    const string DGNUIInvalidPropertyName
    const string LayerManagerSingleDeleteConfirmation
    const string LayerManagerMultipleDeleteConfirmation
    const string LayerManagerUnableToModifyLayersWarning
    const string LayerToolsIncompatibleVisualStyle
    const string SheetSetManagerDrawingFileNotFound
    const string QVDrawingCloseAllOtherDrawings
    const string QVDrawingCloseReadOnly
    const string SheetSetManagerConfirmChanges
    const string SheetSetManagerConfirmChangesForGroups
    const string DwgAidsRestoreAllContexts
    const string DwgAidsRestoreCurrentContext
    const int DwgAidsRestoreCurrentContext_RestoreCurrentContextButon
    const string DwgAidsRestoreClassicColors
    const string AcAuthEnvironRedefineBlock
    const int AcAuthEnvironRedefineBlock_RedefineBlockAndUpdateButon
    const int AcAuthEnvironRedefineBlock_UnRedefineBlockButon
    const string AcBlockNoObjectsSelected
    const int AcBlockNoObjectsSelected_SelObjectsButon
    const int AcBlockNoObjectsSelected_ContinueDefineButon
    const string AcAuthEnvironProxyObjectsFound
    const string AcBlockXrefConflict
    const string AcDxFilesNotFound
    const int AcDxFilesNotFound_ContinueUpdateButon
    const int AcDxFilesNotFound_DoNotUpdateButon
    const string AcDxOutdatedTable
    const int AcDxOutdatedTable_UpdateButon
    const int AcDxOutdatedTable_UpdateAllButon
    const int AcDxOutdatedTable_SkipUpdateButon
    const int AcDxOutdatedTable_SkipAllUpdatesButon
    const string AcDxUpdateMCCells
    const int AcDxUpdateMCCells_UpdateAllCellsButon
    const int AcDxUpdateMCCells_SkipManuallyChangedCellsButon
    const string AcDxWizardUnsavedDrawing
    const int AcDxWizardUnsavedDrawing_SaveDrawingButon
    const string AcDxWizardQuitWizard
    const string AcTableInvalidTbCellValue
    const int AcTableInvalidTbCellValue_ChangeDataTypeButon
    const int AcTableInvalidTbCellValue_ChangeDataValueButon
    const string AcTableLargeNBOfCells
    const string AcTableDetachDataLink
    const string AcDataLinkExcelNotFound
    const string AcTableOverwriteStartingTable
    const int AcTableOverwriteStartingTable_OverwriteButon
    const string AcTableInvalidRange
    const string AcDataLinkExcelSheetNotFound
    const string AcTableRangeOverlap
    const string AcTableRangeMismatch
    const string AcDataLinkInvalidRange
    const string AcDataLinkInvalidExcelFile
    const string AcTableCanNotPasteIntoLockedCells
    const string AcDataLinkOverwrittenDatalinkCells
    const int AcDataLinkOverwrittenDatalinkCells_UpdateCellsButon
    const int AcDataLinkOverwrittenDatalinkCells_RetainCellsButon
    const string AcTableRemoveStartingTable
    const string AcDataLinkCannotUpdateData
    const string AcBlockCircularBlockReference
    const string AcBlockCustomPropMightLost
    const int AcBlockCustomPropMightLost_SetUniformScaleButon
    const int AcBlockCustomPropMightLost_RetainCurrentScaleButon
    const string AcBlockInvalidBlockName
    const int AcBlockInvalidBlockName_Continue2Buton
    const string AcBlockInvalidCoordianteVaule
    const int AcBlockInvalidCoordianteVaule_Continue1Buton
    const string AcBlockNotModified
    const string AcBlockAnnotivePropertyChanged
    const int AcBlockAnnotivePropertyChanged_SaveChangesButon
    const int AcBlockAnnotivePropertyChanged_NotSaveChangesButon
    const string AcBlockFileNotFound
    const string AcBlockLibraryInvalid
    const int AcBlockLibraryInvalid_RemoveRecordButon
    const int AcBlockLibraryInvalid_KeepRecordButon
    const string AcAuthEnvironChangesNotSaved
    const int AcAuthEnvironChangesNotSaved_SaveTheBlockButon
    const int AcAuthEnvironChangesNotSaved_DiscardChangesButon
    const string AcBlockCannotInsertAnnotiveBlocks
    const string AcAuthEnvironParameterChangesNotSaved
    const int AcAuthEnvironParameterChangesNotSaved_SaveTheParameterButon
    const int AcAuthEnvironParameterChangesNotSaved_DiscardTheChangesButon
    const string AcBlockRedefineBlock
    const int AcBlockRedefineBlock_RedefineBlockButon
    const string AcPurgeConfirmSinglePurge
    const int AcPurgeConfirmSinglePurge_PurgeThisItemButon
    const int AcPurgeConfirmSinglePurge_SkipThisItemButon
    const string AcTableMergeCells
    const string AcTableConfirmDeleteTableStyle
    const int AcTableConfirmDeleteTableStyle_DeleteTableStyleButon
    const string AcTableRedefineCellStyle
    const string AcTableRedefineTableStyle
    const string AcSmNavInvalidNameTooLong
    const string AcBlockBlockNameNotSpecified
    const string AcAuthEnvironLookupParameterNotDefined
    const string AcAuthEnvironNoErrorsFound
    const string AcBlockWriteBlockNoObjectsSelected
    const int AcBlockWriteBlockNoObjectsSelected_SelectObjectsButon
    const string AcBlockObjectsOnLockedLayer
    const string CUICannotCopyNestedToolbarFlyouts
    const string OpmNoObjectsFound
    const string DimMLeaderStyleRedefineStyle
    const int DimMLeaderStyleRedefineStyle_RedefineStyleButon
    const int DimMLeaderStyleRedefineStyle_DoNotRedefineStyleButon
    const string OPMAcPEXCtlCannotModifyProperty
    const string OPMAcPEXCtlPatternNameNotFound
    const string PropertiesObjectsOnLockedLayers
    const string PropertiesObjectsMoveToFrozenOrOffLayers
    const string DimensionNoDimensionsSelected
    const string BlockCircularReferenceToTable
    const string BlockCircularReference
    const string AcDimRedefineDimStyle
    const string AcDimInvalidStyleName
    const string AcLayerAppsOverwriteLayerState
    const string AcLayerAppsDleteLayerState
    const string AcAuthEnvironBTableErrorClosing
    const int AcAuthEnvironBTableErrorClosing_SaveTheBTableButon
    const int AcAuthEnvironBTableErrorClosing_ReturnToBTableButon
    const string AcAuthEnvironBTableAuditDunplicateRow
    const int AcAuthEnvironBTableAuditDunplicateRow_DeleteRowButon
    const string AcAuthEnvironBTableAuditDefinitionNotMatch
    const int AcAuthEnvironBTableAuditDefinitionNotMatch_AddRowButon
    const string AcAuthEnvironBTableAuditEmptyCell
    const string AcAuthEnvironBTableAuditCellNotMatchConstant
    const string AcAuthEnvironBTableAuditReferenceNotFound
    const string AcAuthEnvironBTableAuditNotInValueSet
    const int AcAuthEnvironBTableAuditNotInValueSet_UpdateValueButon
    const string AcAuthEnvironBTableAuditNotInValueSetAndReadOnly
    const string AcAuthEnvironBTableAuditCellEvalError
    const string AcAuthEnvironBTableNoErrorsFound
    const string AcAuthEnvironBTablePasteError
    const int AcAuthEnvironBTablePasteError_ContinuePasteButon
    const string AcAuthEnvironBTableInvalidCharacter
    const string AcAuthEnvironBTableOutofValueSet
    const string AcLayerAppsLayerstateInvalidFile
    const string AcLayerAppsLayerstateInvalidFile2
    const string AcLaytransChangesNotSaved
    const int AcLaytransChangesNotSaved_TranslateAndSaveButon
    const int AcLaytransChangesNotSaved_TranslateOnlyButon
    const string RibbonUnableToRemoveFromQAT
    const string RibbonUnableToAddSeparatorIntoQAT
    const string RibbonUnableToAddControlIntoQAT
    const string CUIImageNameInvalid
    const string CUIIconSizeTooLarge
    const string CUIReset
    const int CUIReset_CUIResetContinueButon
    const int CUIReset_CUIResetCancelButon
    const string CUIRestoreBackup
    const int CUIRestoreBackup_CUIRestoreBackupContinueButon
    const int CUIRestoreBackup_CUIRestoreBackupCancelButon
    const string AcUnderlayInvalidFile
    const string seekSaveChanges
    const int seekSaveChanges_TranslateAndSave1Buton
    const int seekSaveChanges_TranslateAndSave2Buton
    const string seekWebsiteNotAvailable
    const string seekSaveFile
    const int seekSaveFile_SaveFileButon
    const string PulishPresetInvalidResolution
    const string PulishPresetNameExist
    const string PulishPresetInvalidName
    const string PulishNoPreset
    const string PulishSaveDSD
    const string PulishPresetIllegalChar
    const string AcCmMgrDeleteDimParameter
    const int AcCmMgrDeleteDimParameter_ConvertDimensionalConstraintButon
    const int AcCmMgrDeleteDimParameter_DeleteParameterNameButon
    const string AcCmMgrDeleteUserParameter
    const int AcCmMgrDeleteUserParameter_DeleteUserParameterButon
    const string AcCmMgrDimOverConstrain
    const int AcCmMgrDimOverConstrain_CreateRefButon
    const int AcCmMgrDimOverConstrain_ReselectButon
    const string AcCmMgrAutoconNoTypeSelected
    const string AcCmMgrRefeditWithConstraint
    const string AcCmMgrReferencedInEquation
    const string AcOpmExtZeroValueParameter
    const string ExportSelectWindowSuccessful
    const string ExportSelectWindow
    const string SeekFileNameTooLong
    const string AcCmMgrAlreadyInGroup
    const string AcCmMgrEntityBlockSame
    const string AcCmMgrToleranceTooLarge
    const int AcCmMgrToleranceTooLarge_ProceedAutoConstrainButon
    const string AcCmMgrEvaluationFail
    const string AcCmMgrRelaxDragging
    const string AcCmMgrDeleteDimConstraint
    const string AcadAppNoAcVBA
    const string AcCmMgrConstrBarFilterZero
    const string AcCmMgrCovertAssocDimTooMany
    const string CUIFoldPanelContents
    const string AcLayTransInvalidTransparencyValue
    const string AssocSurfaceModificationFail
    const string AssocSurfaceCreationFail
    const string AssocNetworkEvaluationFail
    const string AssocVariableEvaluationFail
    const string AcArrayArrayClose
    const int AcArrayArrayClose_YesButon
    const int AcArrayArrayClose_NoButon
    const int AcArrayArrayClose_CancelButon
    const string AcArrayMaxArrayObject
    const string MaterialsLibraryInUse
    const string LayerManagerCurrentLayerOffMac
    const string LayerManagerLayerCannotFreezeMac
    const string AcContentBlockImportSingle
    const string AcContentBlockImportMultiple
    const string CustomizationSaveChanges
    const string TextFindBlockRef
    const int TextFindBlockRef_ReplaceAllButon
    const int TextFindBlockRef_ReplaceThisButon
    const int TextFindBlockRef_SkipButon
    const string LayerManagerDeletedBlockRefs
    const string LayerManagerLayerDeleteMac
    const int LayerManagerLayerDeleteMac_DeleteLayerAndObjectsButon
    const int LayerManagerLayerDeleteMac_DeleteLayerAndMoveObjectsButon
    const int LayerManagerLayerDeleteMac_Calcel_DeleteButon
    const string AcSynergyViewUpdateAssociativeAnnotations
    const string AcSynergyDeferUpdatesQueuedChanges
    const string AcSynergyVIEWBASEUnresolved
    const string AcSynergyLODUnresolved
    const string AcSynergyInventorReferencesOutOfDate
    const string AcTranslatorsRunTranslationInBackground
    const string AcTranslatorsTranslationProcessIsRunning
    const string AcTranslatorsRunExportTranslationInBackground
    const string AcTranslatorsExportTranslationProcessIsRunning
    const string AutoCADAutodeskMediumTexDownloadError
    const string AutoCADAutodeskMediumTexTobeInstalled
    const string LayerManagerDeleteGroupWarning
    const int LayerManagerDeleteGroupWarning_DeleteGroupWithoutLayersButon
    const int LayerManagerDeleteGroupWarning_DeleteGroupAndLayersButon
    const string LayerManagerNoMatchingLayers
    const int LayerManagerNoMatchingLayers_EditTheGroupButon
    const int LayerManagerNoMatchingLayers_CreateGroupAnywayButon
    const string LayerManagerHideSystemGroup
    const string AcSynergyLabelExcludeCharactersInvalid
    const string AcSynergyFinishSymbolSketchSaveExit
    const int AcSynergyFinishSymbolSketchSaveExit_SaveSymbolSketchButon
    const int AcSynergyFinishSymbolSketchSaveExit_DiscardSymbolSketchChangesButon
    const string AcSynergyFinishSymbolSketchInvalidSymbol
    const int AcSynergyFinishSymbolSketchInvalidSymbol_DiscardInvalidChangesButon
    const int AcSynergyFinishSymbolSketchInvalidSymbol_RepairSymbolButon
    const string AcSynergyFinishSymbolSketchDeleteSymbol
    const string AcSynergyOpenDrawingLostFunctionality
    const string AcSmNavLayoutIssueWithDrawing
    const int AcSmNavLayoutIssueWithDrawing_DuplicateButon
    const int AcSmNavLayoutIssueWithDrawing_DiscardDuplicateButon
    const string AcSmNavSheetMissing
    const int AcSmNavSheetMissing_RemoveSheetButon
    const int AcSmNavSheetMissing_RemapSheetButon
    const int AcSmNavSheetMissing_BrowseRemapSheetButon
    const string AcSmNavSheetDrawingMissing
    const int AcSmNavSheetDrawingMissing_AcSmNav_RemoveSheetButon
    const int AcSmNavSheetDrawingMissing_AcSmNav_BrowseRemapSheetButon
    const string AutoCADAcCloudConnectSyncFileNotSaved
    const string AcCloudCreateFolderExists
    const string AcCloudCreateFolderPathnameLength
    const string AcCloudCreateFolderInvalid
    const string AcSynergyViewStyleRestoreDefaults
    const string AcSmNavCalloutBlockImportSingle
    const int AcSmNavCalloutBlockImportSingle_SingleFileOneBlockButon
    const int AcSmNavCalloutBlockImportSingle_SingleFileMultipleBlockButon
    const string AcSmNavCalloutBlockImportMultiple
    const int AcSmNavCalloutBlockImportMultiple_MultipleFileOneBlockButon
    const int AcSmNavCalloutBlockImportMultiple_MultipleFileMultipleBlockButon
    const string AcLayerAppsLayerNotMerged
    const string AcLayerAppsLayerMergeVerify
    const string AcBlockMismatchGeoCS
    const string AcPointCloudCompatibility
    const string AcPointCloudGeoMismatch
    const string AcDwgFeedSaveFile
    const int AcDwgFeedSaveFile_SaveButon
    const int AcDwgFeedSaveFile_SyncToA360Buton
    const string PhotoViewerFileNotFound
    const string PhotoViewerInvalidFileFormat
    const string PhotoViewerFileOrFolderNotFound
    const string AutoCADAcCloudConnectDocCloseBlocker
    const string AcAuthEnvironNotShowBlockActionPanel
    const string RenderNoFacesToRender
    const string RenderOutOfMemory
    const string RenderSoftOutOfMemory
    const string RenderIBLWarnSetBackgroundWithDisplayImageOn
    const string AcCoreDeleteKey
    const string PdfImportNoDataImported
    const string PdfImportNoRasterDataImported
    const string PdfImportReadOnlyPDFImportImagePath
    const string AcPlotGuiUnsupportedPaperSize
    const int AcPlotGuiUnsupportedPaperSize_UseDefaultPaperSizeButon
    const int AcPlotGuiUnsupportedPaperSize_UseOriginalPaperSizeButon
    const string AcadExternalReferenceApplyPath
    const string CompareExportProcessingInProgress
    const string AcPurgeSelectObjects_LayerNotVisible
    const int AcPurgeSelectObjects_LayerNotVisible_Button_turn_on_layerButon
    const int AcPurgeSelectObjects_LayerNotVisible_Button_not_turn_on_layerButon
    const string AcPurgeSelectObjects_VPFrozen
    const int AcPurgeSelectObjects_VPFrozen_Button_turn_on_VP_layerButon
    const int AcPurgeSelectObjects_VPFrozen_Button_not_turn_on_VP_layerButon
    const string CommadNotAllowedInCompare
    const string CommadNotAllowedInXCompare
    const string DWGCompareIsResultDrawing
    const string AutoLISPSelectAutoLISPExtension
    const int AutoLISPSelectAutoLISPExtension_SelectVSCodeExtensionButon
    const int AutoLISPSelectAutoLISPExtension_SelectVisualLISPButon
    const string AutoLISPPromptRestartAcad
    const string ShareUploadInProgress
    const int ShareUploadInProgress_ContinueUploadingButon
    const int ShareUploadInProgress_CancelUploadingButon
    const string AcAuthEnvironInsertDupNameBlock
    const int AcAuthEnvironInsertDupNameBlock_RedefineBlockAndUpdateButtonButon
    const int AcAuthEnvironInsertDupNameBlock_UnRedefineBlockButtonButon
    const int AcAuthEnvironInsertDupNameBlock_RenameBlockButtonButon
    const string AcTraceLogoffWhileTraceOpen
    const string AcTraceNotEntitled
    const string AcTraceSelectFileOrProceed
    const int AcTraceSelectFileOrProceed_ImportIntoCurrentDrawingButtonButon
    const int AcTraceSelectFileOrProceed_ImportToDrawingButtonButon
    const string AcTraceSelectFileOrAdjust
    const int AcTraceSelectFileOrAdjust_SelectDrawingButtonButon
    const int AcTraceSelectFileOrAdjust_ProceedToCurrentDrawingButtonButon
    const string AcTraceImportToCurrentOrChange
    const int AcTraceImportToCurrentOrChange_ImportToCurrentDrawingButtonButon
    const int AcTraceImportToCurrentOrChange_ChangeLayoutOrDrawingButtonButon
    const string AcTraceRequiresMDIMode
    const string AcTracePdfReferenceMissing
    const string AcTraceErrorOccurred
    const string AcBlockSpotEndDetectReviewModePrompt
    const string AcTracePublishCheckout
    const int AcTracePublishCheckout_SendChangesButtonButon
    const int AcTracePublishCheckout_CloseTraceButtonButon
    const string AcTraceConfirmDeleteTrace
    const string AcTraceConfirmDeleteTraceWithUrl
    const string AcTraceConfirmDeleteTraceContribution
    const int AcTraceConfirmDeleteTraceContribution_RemoveMyContributionButtonButon
    const int AcTraceConfirmDeleteTraceContribution_OpenInAutodeskDocsButon
    const string AcTraceCheckoutNoSubsciption
    const string AcTraceMergeWithConflicts
    const string AcTraceMergeWithPossibleConflicts
    const string AcTraceMergeNoConflicts
    const string AcTraceTryMergeLockedCheckout
    const string AcTraceCancelCheckoutConfirmation
    const string AcTraceCancelLockedCheckoutConfirmation
    const string AcTraceCheckoutCanceledByOwner
    const string AcTraceCheckoutMergedByOwner
    const string AcTraceSubmitSuccess
    const string AcTraceSubmitFail
    const string AcTraceCheckoutSaveFail
    const string AcTraceCheckoutCreateFail
    const string CUIUnloadProjectAwareCUIFile
    const int CUIUnloadProjectAwareCUIFile_UnloadButtonButon
    const int CUIUnloadProjectAwareCUIFile_RemovefromMySettingsButon
    const int CUIUnloadProjectAwareCUIFile_CancelButtonButon
    const string MainFrameCommandLineHideWindow
    const string HatchOpenBoundary
    const string MTextUnsavedChanges
    const string DWGRecoveryDrawingRecovery
    const string CustomizationUnsavedCUIChanges
    const string ColorDlgMissingColorBook
    const string MTextStyleChange
    const string MTextWarningAboutBullets
    const string MTextWarningAboutPasting
    const string MTEXTAutoStackProperties1
    const string MTEXTAutoStackProperties2
    const string SceneUIViewportLightingMode
    const string SceneUISunlightAndExposure
    const string AcPlotGuiPlotScaleConfirm
    const string AcPublishSaveSheetList
    const string MaterialsSynchronizeMaps
    const string MaterialsDesynchronizeMaps
    const string ScaleListResetScaleList
    const string PlotShadePlot
    const string AcPublishDSDSettingConflict
    const string PlotAndPublishCancelEntireSheet
    const string PlotAndPublishCancelEntireJob
    const string AcPlotGuiPlotFileExists
    const string EnterpriseWorkspaceCannotSaveChanges
    const string DgnUIDGNImportUnsupportObjects
    const string PlotProcessingBackgroundJob
    const string SceneUIPhotometricDistantLights
    const string DrawingOpenForeignDWGFile
    const string DgnUIUnsupportDGNExportObjects
    const string ActionRecorderPlaybackComplete
    const string ActionRecorderInconsistency
    const string ActionRecorderValueNotRecorded
    const string AcadLowMemoryAlert
    const string LayerWalkLayerStatesChange
    const string AutoLISPLoadSettings
    const string VMToolsUIWalkFlyToPerspectiveView
    const string ConvertHatchObjectsWarning
    const string RecoverAllWarning
    const string MaterialDeleteNestedMaps
    const string AutoCADDWGAssociation
    const string AcGeoLocationUITimeZoneUpdated
    const string AcGeoLocationUIMultipleLocationsFound
    const string AcGeoLocationUIUnitMismatch
    const int AcGeoLocationUIUnitMismatch_ResetButon
    const int AcGeoLocationUIUnitMismatch_ContinueButon
    const string AcGeoLocationUIPlotLiveMapWarning
    const string AdRefManRemoveFolderPaths
    const string AdRefManAddXRefs
    const string MTEUnknownTextFonts
    const string DigitalSignaturesUnsupportedDrawingFormat
    const string VMToolsUIOverwriteAnimationFrames
    const string UnitsInsertUnits
    const string UnitsRenderEngine
    const string QPSwitchToFloatMode
    const string QPRemoveUndefObjType
    const string QPOffPanelWarning
    const string QPDockToRibbon
    const string RollOverRestoreDefault
    const int RollOverRestoreDefault_RestoreSettingsButon
    const int RollOverRestoreDefault_KeepCustomButon
    const string QPRestoreDefault
    const int QPRestoreDefault_RestorQPeSettingsButon
    const int QPRestoreDefault_KeepQPCustomButon
    const string QPSyncWithTooptip
    const string DGNUIIncompatibleSeedFileSettings
    const int DGNUIIncompatibleSeedFileSettings_ContinueExportButon
    const string AcadRenameSheetLayout
    const int AcadRenameSheetLayout_RenameLayoutAndSheetButon
    const int AcadRenameSheetLayout_RenameLayoutOnlyButon
    const int AcadRenameSheetLayout_DiscardNameChangeButon
    const string AcadRenameSheetSetLocked
    const int AcadRenameSheetSetLocked_RenameOnlyLayoutButon
    const int AcadRenameSheetSetLocked_CancelRenameButon
    const string CustomizationUndefinedObjectType
    const string CustomizationComfirmCopytoRibbonPanels
    const string AcPublish3DDWFCreated
    const string LayerManagerNewLayerFilteredWarning
    const string ScaleListLargeScaleAlert
    const string AssocFilletExplodePolyline
    const string AcDxWizardRemoveFolder
    const string AcDxWizardRemoveDrawing
    const string AcDxWizardRemoveDrawings
    const string AcSubDUnsupportedSubObject
    const string AcSubDUnsupportedObject
    const string AcSubDConvertToMesh
    const int AcSubDConvertToMesh_FilterObjectsButon
    const int AcSubDConvertToMesh_ConvertObjectsButon
    const string AcSubDMinSmoothLevel
    const string AcSubDMaxSmoothLevel
    const string AcSubDMeshSmoothInvalidSelection
    const string AcSubDMeshSmoothTooManyFaces
    const string AcSubDMeshSmoothNonPrimitiveObject
    const int AcSubDMeshSmoothNonPrimitiveObject_CreateMeshButon
    const int AcSubDMeshSmoothNonPrimitiveObject_DoNotConvertToMeshButon
    const string AcSubDTessellationDialogBoxInvalidEntry
    const string AcSubDMeshPrimitiveDialogBoxInvalidEntry
    const string AcSubDConvertToASM
    const int AcSubDConvertToASM_FilterMeshObjectsButon
    const int AcSubDConvertToASM_ConvertToSmoothASMButon
    const int AcSubDConvertToASM_ConvertToFacetedASMButon
    const string AcSubDCannotRefineAtMinLevel
    const string AcSubDOptionsDialogBoxInvalidEntry
    const string AcGuSMReuildSurfaceDialogBoxInvalidEntry
    const string AcGuSMReuildCurveDialogBoxInvalidEntry
    const string AcSMConersonToASpline
    const string AcGuSMInvalidEntry
    const string ConstraintNonAssociativeSelection
    const string AcParameterParameterFiltered
    const string ConstraintOverConstraint
    const string ConstraintNonDimConstraintSelection
    const string ConstraintWouldOverContrain
    const string Printing3DPrepareModel
    const string AcadUnresolvedReferenceFiles
    const int AcadUnresolvedReferenceFiles_UpdateLocationButon
    const int AcadUnresolvedReferenceFiles_IgnoreUnresolvedRefButon
    const string AcadUnresolvedReferenceFilesWithPathMap
    const int AcadUnresolvedReferenceFilesWithPathMap_UpdateLocation2Buton
    const int AcadUnresolvedReferenceFilesWithPathMap_XrefPathMapping2Buton
    const int AcadUnresolvedReferenceFilesWithPathMap_IgnoreUnresolvedRef2Buton
    const string AcadDigitallySignedDWFx
    const string BEDITConstraintsFound
    const string ConstraintDynamicBlockGripsWillBeHidden
    const string AcAuthEnvironParameterChangesNotSavedUnderConstrainedClosingEditor
    const int AcAuthEnvironParameterChangesNotSavedUnderConstrainedClosingEditor_SaveTheParameterButon
    const int AcAuthEnvironParameterChangesNotSavedUnderConstrainedClosingEditor_DiscardTheChangesButon
    const string AcAuthEnvironParameterChangesNotSavedUnderConstrained
    const int AcAuthEnvironParameterChangesNotSavedUnderConstrained_SaveItButon
    const string CUIDeleteUnreferencedImage
    const string CUIDeleteReferencedImage
    const string CUIImageNameAlreadyExist
    const string AcadUnresolvedFontFiles
    const int AcadUnresolvedFontFiles_UpdateFontButon
    const int AcadUnresolvedFontFiles_IgnoreUnresolvedFontButon
    const string HatchFrozenLayer
    const string HatchDrawingHasLargeHatches
    const string AutoCADAutodeskMaterialLibNotInstalled
    const string AutoCADAutodeskMaterialTexNotInstalled
    const string AutoCADAutodeskMaterialLibNotFound
    const string AutoCADAutodeskMaterialTexNotFound
    const string MaterialUIMaterialInUse
    const string MaterialUIMigrateMaterial
    const string PlotGuiPlotModelSpaceAlert
    const int PlotGuiPlotModelSpaceAlert_TaskPlotDialogButton_181Buton
    const int PlotGuiPlotModelSpaceAlert_TaskPlotDialogButton_182Buton
    const string AutoCADUnsupportedObjects
    const int AutoCADUnsupportedObjects_ShowObjectsIfAvailable_ButtonButon
    const int AutoCADUnsupportedObjects_ShowBoundingBox_ButtonButon
    const int AutoCADUnsupportedObjects_DoNotShowUnsupportedObjects_ButtonButon
    const string AutoCADCleanScreenOn
    const string AutoCADRenderScaleSettings
    const int AutoCADRenderScaleSettings_ConvertToMetersButon
    const int AutoCADRenderScaleSettings_CancelRenderButon
    const string AcArrayCreateNonAssocArrayForCustomObj
    const string AcArrayCustomObjSelected
    const int AcArrayCustomObjSelected_NonAssocButon
    const int AcArrayCustomObjSelected_RemoveCustomObjButon
    const string AcSynergyViewSTDHighResolution
    const string AcSynergyUnresolvedInventorFiles
    const int AcSynergyUnresolvedInventorFiles_SelectProjectButon
    const string AcSynergyViewUpdateAssociativeLossDetected
    const string AcSynergyPublishNotification
    const string AcSynergyUnresolvedBeingOpened
    const string AutoCADAutodeskMediumTexNotInstalled
    const string AutoCADAutodeskMediumTexNotInstalledLight
    const string AcArrayEditSource
    const string HatchDuplicatePatternSelected
    const int HatchDuplicatePatternSelected_OverwriteButon
    const int HatchDuplicatePatternSelected_SkipButon
    const string AcModelDocInventorImport
    const string AcModelDocSketchModeEnteringViewSketch
    const string DrawingSaveAsAcModelDoc
    const string PhotoViewerRemoveImage
    const string AutoCADApplicationRestart
    const string PlotBatchPlotFromPlot
    const int PlotBatchPlotFromPlot_BatchPlotFromPlot_LearnMoreButon
    const int PlotBatchPlotFromPlot_BatchPlotFromPlot_CancelButon
    const int PlotBatchPlotFromPlot_BatchPlotFromPlot_ContinueButon
    const string DimensionFrozenLayer
    const string UnitsInsertUSSurveyFeetUnits
    const string AcadUpdateXRefRelativePathOnSave
    const int AcadUpdateXRefRelativePathOnSave_UpdateXRefRelativePathButon
    const int AcadUpdateXRefRelativePathOnSave_IgnoreXRefPathUpdateButon
    const string PdfShxTextTextRecognitionFailed
    const string PdfShxTextTextRecognitionSucceeded
    const string DWGCompareUncomparableObjects
    const string AcBlockGeographicData
    const int AcBlockGeographicData_Button_using_geographic_dataButon
    const int AcBlockGeographicData_Button_prompt_for_insertion_pointButon
    const string DWGCompareReCompare
    const string DWGCompareExport
    const string CompareExportProcessingStarted
    const string CompareExportProcessingContinueorStop
    const string XrefCompareReload
    const string XrefCompareReloadInXCompare
    const string AcWindowsBlockSyncFolderUnavailable
    const string AutoCADUnsavedChanges
    const string CountAreaInvalidArea
    const string FolderSelectionInvalidTopFolder
    const string FolderSelectionInvalidSubFolder
    const string FolderSelectionMaxDepthExceeded
    const string ObjectNameNoFolderAccess
    const string StandardDuplicateNameCopyOverwrite
    const string StandardInvalidNameEmptyName
    const string StandardDuplicateNameError
    const string StandardInvalidNameTooLong
    const string StandardInvalidNameValueTooLong
    const string StandardInvalidNameUnsupportedCharacters
    const string StandardInvalidNameValueUnsupportedCharacters
    const string StandardDuplicateNameReplaceCancel
    const string StandardFileConfirmationWithoutNoToAll
    const string StandardFileConfirmationWithNoToAll
    const string StandardDeleteConfirmation
    const string StandardCloseReadOnly
    const string StandardFileNotFound
    const string StandardInValidPropertyName
    const string StandardObjectUnsupportCharacters
    const string StandardWriteProtectedFile
    const string StandardOffsetObjectIsNotPlanar
    const string StandardFileAlreadyExists
    const int StandardFileAlreadyExists_ReplaceButon
    const int StandardFileAlreadyExists_RenameButon
    const string StandardFileInUse
    const string StandardFilePathTooLong
    const string StandardInvalidPath
    const string StandardPathOrFileNameNotSpecified
    const string StandardObjectTypeCannotBeDeleted
    const string DWGCompareCompletelyIdentical
    const string DWGCompareCompletelyDifferent

--------------------------------------------------------------------------------

[CLASS] AcAeUtilities
Full Name: Autodesk.AutoCAD.Internal.AcAeUtilities

  CONSTRUCTORS:
    new AcAeUtilities()

  METHODS:
    static Color GetBeditConstraintColor(ConstraintType constraintType)
    static string GetBlockName()
    static string GetCurrentVisibilityStateName()
    static void GetVisibilitySets(List<string> visibilities)
    static bool IsAuthorPaletteVisible()
    static bool IsInBlockEditor()
    static bool IsInBVMode()
    static bool IsVisibilityParameterPresent()
    static bool PickFirstBeforeInvokeBvHide()
    static bool PickFirstBeforeInvokeBvShow()
    static void SetBeditConstraintColor(ConstraintType constraintType, Color color)
    static void ShowAuthorPalette(bool bShow)

--------------------------------------------------------------------------------

[CLASS] AcDownloadCallback
Full Name: Autodesk.AutoCAD.Internal.AcDownloadCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new AcDownloadCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[STRUCT] AcNavisworksProtocolAdapter
Full Name: Autodesk.AutoCAD.Internal.AcNavisworksProtocolAdapter

--------------------------------------------------------------------------------

[CLASS] AcNavisworksService
Full Name: Autodesk.AutoCAD.Internal.AcNavisworksService
Implements: INavisworksService, IDisposable

  CONSTRUCTORS:
    new AcNavisworksService()

  METHODS:
    void Dispose()
    void RegisterFileProtocol(string scheme, INavisworksFileProtocol fileprotocol)

--------------------------------------------------------------------------------

[CLASS] AcShareFileData
Full Name: Autodesk.AutoCAD.Internal.AcShareFileData

  CONSTRUCTORS:
    new AcShareFileData(string filePath)
    new AcShareFileData()

--------------------------------------------------------------------------------

[ABSTRACT CLASS] AcSmUtils
Full Name: Autodesk.AutoCAD.Internal.AcSmUtils

  PROPERTIES:
    static bool SheetSetCmdAllowed { get; }

  METHODS:
    static void GetDrawingsFromSheetSet(string sheetSetPath, Hashtable dwgList)
    static bool IsSheetSet(string path)
    static void OpenSheetSet(string path)

  CONSTANTS/STATIC FIELDS:
    const string SheetSetExtension = .dst

--------------------------------------------------------------------------------

[CLASS] ActiveThemeColor
Full Name: Autodesk.AutoCAD.Internal.ActiveThemeColor

  PROPERTIES:
    static ColorThemeEnum CurrentTheme { get; }
    static Color InspectorBGHighlight { get; }
    static Color InspectorItem { get; }
    static Color PaletteBackground { get; }

  METHODS:
    static ActiveThemeColor Instance()

  EVENTS:
    event ColorThemeChangedEventHandler ColorThemeChanged

--------------------------------------------------------------------------------

[CLASS] AcWorkItemUtils
Full Name: Autodesk.AutoCAD.Internal.AcWorkItemUtils

  CONSTRUCTORS:
    new AcWorkItemUtils()

  METHODS:
    static string getProductName()
    static void parseWorkItemMessage(string resultJsonStr, string requestId)

--------------------------------------------------------------------------------

[CLASS] ADPUtil
Full Name: Autodesk.AutoCAD.Internal.ADPUtil

  CONSTRUCTORS:
    new ADPUtil()

  METHODS:
    static void logData(string waypointName, Dictionary<string, string> attrs)

--------------------------------------------------------------------------------

[CLASS] AppLoaderDownloadUtils
Full Name: Autodesk.AutoCAD.Internal.AppLoaderDownloadUtils

  CONSTRUCTORS:
    new AppLoaderDownloadUtils()

  METHODS:
    static void CancelDownloadJob(uint nToken)

--------------------------------------------------------------------------------

[CLASS] AppMenuUtil
Full Name: Autodesk.AutoCAD.Internal.AppMenuUtil

  CONSTRUCTORS:
    new AppMenuUtil()

  METHODS:
    static ValueTuple<string, int> Initialize()
    static bool IsApplicationRegistered(string appName)
    static void RibbonDockedTop(bool top)
    static void SuspendAlt(bool suspend)

--------------------------------------------------------------------------------

[CLASS] AttachUtil
Full Name: Autodesk.AutoCAD.Internal.AttachUtil
Implements: IDisposable

  CONSTRUCTORS:
    new AttachUtil()

  METHODS:
    static bool CheckDwgFile(string csDwgPath)
    static bool CheckImageFile(string csImagePath)
    static void CmdLineImageAttach(string strImageFile)
    static void CmdLineNavisworksAttach(string strNavisworksFile)
    static void CmdLinePointCloudAttach(string strPointCloudFile)
    static bool CmdLineXAttach(string strDWGFile)
    void Dispose()
    static string GetImageFileExtensions()
    static string GetImageFilterString()
    static string[] GetOpenFilesResult(PromptOpenFileOptions opt, string[] multiSelArray)
    static void ImageAdjust(ObjectId[] objIds)
    static void ImageAttach(string strImageFile)
    static void ImageClip(ObjectId objId)
    static bool isDialogShow()
    static bool IsPCAttachAllowed()
    static void LoadBIMUnderlayArx()
    static void LoadBIMUnderlayCrx()
    static void LoadISM()
    static void LoadPointCloudArx()
    static void LoadPointCloudCrx()
    static void NavisworksAttach(string strNavisworksFile)
    static void PointCloudAttach(string strPointCloudFile)
    static void PointCloudClip(ObjectId objId)
    static void VPClip()
    static void XAttach(string[] strDWGFileArray)
    static void XClip()

--------------------------------------------------------------------------------

[CLASS] BlockLibraryPathChangedEventArgs
Full Name: Autodesk.AutoCAD.Internal.BlockLibraryPathChangedEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BlockLibraryPathChangedEventArgs(string strPath)

  PROPERTIES:
    string BlockLibraryPath { get; }

--------------------------------------------------------------------------------

[CLASS] BlockPaletteNeedUpdateEventArgs
Full Name: Autodesk.AutoCAD.Internal.BlockPaletteNeedUpdateEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new BlockPaletteNeedUpdateEventArgs(string palette)

  PROPERTIES:
    string PaletteName { get; }

--------------------------------------------------------------------------------

[CLASS] BlockPermissionMgr
Full Name: Autodesk.AutoCAD.Internal.BlockPermissionMgr
Implements: IDisposable

  CONSTRUCTORS:
    new BlockPermissionMgr()

  PROPERTIES:
    static BlockPermissionMgr Instance { get; }
    bool IsAuthorized { get; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] BlockSnapshotInfo
Full Name: Autodesk.AutoCAD.Internal.BlockSnapshotInfo

  CONSTRUCTORS:
    new BlockSnapshotInfo(string snapshotUrl, string targetBlockName)

  PROPERTIES:
    string snapshotUrl { get; }
    string targetBlockName { get; }

--------------------------------------------------------------------------------

[ENUM] BlockSnapshotSerializeState
Full Name: Autodesk.AutoCAD.Internal.BlockSnapshotSerializeState
Inherits: Enum

  Values:
    - kInMemory = 0
    - kSerializable = 1
    - kSerializing = 2
    - kSerialized = 3
    - kDeleted = 4

--------------------------------------------------------------------------------

[CLASS] BlockSpotTools
Full Name: Autodesk.AutoCAD.Internal.BlockSpotTools

  CONSTRUCTORS:
    new BlockSpotTools()

  METHODS:
    static bool IsValidForTextToAttribute(Document document)

--------------------------------------------------------------------------------

[CLASS] BlockStreamCIPLog
Full Name: Autodesk.AutoCAD.Internal.BlockStreamCIPLog

  CONSTRUCTORS:
    new BlockStreamCIPLog()

  METHODS:
    static void logBlockPaletteClosed()
    static void logBlockPaletteOpened()
    static void logBlockrecentfolderSysvarInfo(string valueType)
    static void logCountOfAutoPlacementChecked()
    static void logCountOfCurrentTabSwitchedTo()
    static void logCountOfCurrentTabUsed()
    static void logCountOfExplodeChecked()
    static void logCountOfExternalTabSwitchedTo()
    static void logCountOfExternalTabUsed()
    static void logCountOfFavoritesTabSwitchedTo()
    static void logCountOfFavoritesTabUsed()
    static void logCountOfInsertPointChecked()
    static void logCountOfRecentTabSwitchedTo()
    static void logCountOfRecentTabUsed()
    static void logCountOfRepeatChecked()
    static void logCountOfRotationChecked()
    static void logCountOfScaleChecked()
    static void logCountOfScaleUniformUsed()
    static void logCountOfStartOnBoardingProcessFromMessage()
    static void logCountOfStartOnBoardingProcessFromPage()
    static void logCountOfStartOnBoardingProcessFromSyncSetting()
    static void logCurrentTabUsedMetaData(bool bMetaDataUsedInCurrentDrawingTab)
    static void logDwgInsertedAsABlock()
    static void logFavoriteBlocksData(int totalBlockCount, int uniqueBlockNameCount, int duplicatingNamedBlockCount, int duplicatingUniqueBlockNameCount)
    static void logFavoriteBlockSize(double averageSize, double maxSize, double minSize, int snapshotCount)
    static void logFileSelectedAsLibrary()
    static void logFolderSelectedAsLibrary(int countOfFilesInFolder)
    static void logInsertDwgAsABlockFromFolder()
    static void logLibraryInfo(string libraryOperation, string libraryType)
    static void logLocalServerActivityInfo(string activityType, int runTime, string status)
    static void logNonDwgBlockInserted()
    static void logNumberOfBlocksInCurrentTab(int count)
    static void logNumberOfBlocksInExternalTab(int count)
    static void logNumberOfBlocksInFavoritesTab(int count)
    static void logNumberOfBlocksInRecentTab(int count)
    static void logOnboardingCompleted(string sSyncFolder)
    static void logSearchUsed()
    static void logSuppressAllSetMessage(bool isCloudSyncFolder)
    static void logSuppressSyncBlockMessage()
    static void logSyncFolderChangedFromSyncSettingDialog(AcString* sSyncFolder)
    static void logWayToInsertBlockByContextMenu()
    static void logWayToInsertBlockByContextMenuInsert()
    static void logWayToInsertBlockByContextMenuInsertAndExplode()
    static void logWayToInsertBlockByContextMenuInsertAndRedefine()
    static void logWayToInsertBlockByContextMenuRedefineOnly()
    static void logWayToInsertBlockByDoubleClick()
    static void logWayToInsertBlockByDragDrop()
    static void logWayToInsertBlockByDrawingAsBlockButton()
    static void logWayToInsertBlockByDrawingAsBlockButtonSuccess()
    static void logWayToInsertBlockBySinglelick()

--------------------------------------------------------------------------------

[CLASS] BlockStreamLocalServer
Full Name: Autodesk.AutoCAD.Internal.BlockStreamLocalServer

  CONSTRUCTORS:
    new BlockStreamLocalServer()

  METHODS:
    [SKIPPED METHOD] displayItemToBlockPreviewInfo: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string getBlockNameListResultJsonUrlName()
    static string getBlockNameListResultReadyStr()
    static string getBlockPreviewpartialResultName()
    static long getFileModifyTime(string filePath)
    [SKIPPED METHOD] getRequestDict: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] getRequstBlocksStr: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string getRequstBlocksStr(List<mgBlockPreviewInfo> infoList)
    [SKIPPED METHOD] pushBlockPreviewGenerationRequest: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] useLocalServerForBlock: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] BlockStreamLog
Full Name: Autodesk.AutoCAD.Internal.BlockStreamLog
Implements: IDisposable

  METHODS:
    static void debug(AcString* message)
    static void debug(string message)
    void Dispose()
    static void error(AcString* message)
    static void error(string message)
    static void inform(AcString* message)
    static void inform(string message)
    static void init()
    static bool isInitialized()
    static bool isLogEnabled()
    static void setLogEnableState(bool val)
    static void uninit()
    static void warning(AcString* message)
    static void warning(string message)

--------------------------------------------------------------------------------

[CLASS] BlockStreamUtils
Full Name: Autodesk.AutoCAD.Internal.BlockStreamUtils

  CONSTRUCTORS:
    new BlockStreamUtils()

  METHODS:
    static string blockIndexCachePath()
    static void cleanupSnapshotFromRecentBlockList(BlockSnapshotInfo snapshotInfo)
    static void closeInSideDbPool(string key)
    static string combineRecentBlocksPath(Dictionary<string, string> recent_item)
    static bool convertProviderToProviderDetail(string inputProvider, ref string outputTechnology, ref string outputSubscription)
    static bool convertProviderToProviderDetail(AcString* inputProvider, AcString* outputTechnology, AcString* outputSubscription)
    static string convertToSecureTranslatedPath(string translatedPath)
    [SKIPPED METHOD] doDragDrop: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool FindFile(string FullPathName)
    static string getAbsolutePath(string provider, string accountEmail, string relativePath)
    static string getBlockNavigateFile()
    static string getBlockOnBoardingFolder()
    [SKIPPED METHOD] getBlockPreviewImageType: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    static string getBlockRecentOnBoardingFolder()
    [SKIPPED METHOD] getBlocksFromExternalFile: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] getBlocksFromExternalFile: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] getBlocksFromPartialResults: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string GetClientId()
    [SKIPPED METHOD] getCurrentDrawingBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] getDwgDisplayItem: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string getFilePathType(string path)
    static string getLastAccessedLibrary()
    static string getLastAccessedLibraryNode()
    static List<string> getLibraryList()
    static int GetLocalServerUage()
    static int GetPreviewGenerateMethod()
    static string GetPreviewParameter(BlockViewMode viewMode)
    static string GetPreviewParameterAsJsonString(BlockViewMode viewMode, bool bForFileSnapshot)
    static string getRealPath(string path)
    [SKIPPED METHOD] getRecentBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] getSerializedBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static int getThumbnailSize(BlockViewMode viewMode)
    static bool isBlockSyncFolderCustomizedToLocal()
    static bool isBlockSyncFolderSetToCloudStorage()
    static bool IsCloudDriveSyncFolder(string dir)
    static bool isDragDropTempBlock(ValueType blockId)
    static bool isLocalPath(string filePath)
    static bool IsMetadataValid(string dwgFileUrl, string metadataRootDir, BlockViewMode viewMode, bool bDarakTheme, bool bForFileSnapShot, bool bMiniValidation)
    static bool isPaletteVisible()
    static bool isProductEnableDataSyncing()
    static bool isReadyToLoadSideDb()
    static bool isSystemBlock(ValueType blockId)
    static bool isSystemBlockInCurDb(string blockName)
    [SKIPPED METHOD] isUnsavedBlock: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool isValidBlockName(string blockName, bool allowExtendedNames)
    static void Log(string msg)
    static void makeTransparentImage(string srcPngPath, string targetTransparentPngPath)
    static void notifyBlockLibraryPathChanged(string strPath)
    static void notifyBlockPaletteNeedUpdate(string strPath)
    static void onBlockSuggestionResultReceived(string resultJsonStr, string requestId)
    [SKIPPED METHOD] OnBlockThumbnailGenerated: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static void openInSideDbPool(string key)
    static Tuple<string, string> parseConentKey(string contentKey)
    [SKIPPED METHOD] parseJsonBlockPreview: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static Dictionary<string, string> parseRecentBlocksPath(string path)
    static int readRegistryInt(string keyName, int defValue)
    static string readRegistryString(string keyName, string defValue)
    [SKIPPED METHOD] refreshCurrentDwgBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] refreshRecentBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] removeFromRecentBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static string safePathCombine(string folderPath, string fileName)
    [SKIPPED METHOD] saveSerializedBlocks: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static void setLastAccessedLibrary(string value)
    static void setLastAccessedLibraryNode(string value)
    static void setRecentBlockPinned(string key, bool val)
    static bool showBlockOnBoardingGuideDialog()
    [SKIPPED METHOD] UpdateDisplayItemFromPartialResults: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static bool UseLocalServerForActivities()
    static bool UseLocalServerForMetadata()
    static bool UseWhipToGeneratePreview()
    static void writeRegistryInt(string keyName, int value)
    static void writeRegistryString(string keyName, string value)

  EVENTS:
    event EventHandler<BlockPaletteNeedUpdateEventArgs> BlockPaletteNeedUpdate
    event EventHandler<BlockLibraryPathChangedEventArgs> BlockLibraryPathChanged

--------------------------------------------------------------------------------

[ENUM] BlockViewMode
Full Name: Autodesk.AutoCAD.Internal.BlockViewMode
Inherits: Enum

  Values:
    - kExtraLargeIcon = 0
    - kLargeIcon = 1
    - kMediumIcon = 2
    - kSmallIcon = 3
    - kDetails = 4
    - kList = 5

--------------------------------------------------------------------------------

[ENUM] BlockViewModeEnum
Full Name: Autodesk.AutoCAD.Internal.BlockViewModeEnum
Inherits: Enum

  Values:

--------------------------------------------------------------------------------

[CLASS] CertificationUtil
Full Name: Autodesk.AutoCAD.Internal.CertificationUtil

  CONSTRUCTORS:
    new CertificationUtil()

  METHODS:
    static int GetDiagnosisStatus()
    static List<string> GetGraphicsAdapters()

--------------------------------------------------------------------------------

[CLASS] CipUtils
Full Name: Autodesk.AutoCAD.Internal.CipUtils

  METHODS:
    string getMacroString(string macro)
    string getSessionId()
    static CipUtils Instance()
    bool IsOperational()
    void LogApplicationMenuCommandExecute(string id, string sCmd, string insightsMacroId)
    void LogGPUTextOnOff(int value)
    void LogIcLaunch(int icType, string group, string category, string title, string url)
    void LogIcQuery(string queryString, int buttonIndex)
    void LogInfoCenterCommandExecute(string id, string sCmd, int type)
    void LogModelessLayerItem(string cmdStr, bool bInRibbon)
    void LogQuickAccessToolbarCommandExecute(string id, string sCmd, string insightsMacroId)
    void LogRibbonItemCommandExecute(string sCmd, string sTabName, string sPanelName, bool bMenuMacro, int dockSide, string insightsMacroId)
    void LogStatusBarElementVisibility(string elementName, bool isVisible)
    void SetUaLaunchType(string uaLaunchType)
    void WaypointReachedWithAttributes(string waypoint, string state, Dictionary<string, string> attrs)
    void WaypointReachedWithNoState(string waypoint)
    void WaypointReachedWithStringAtt(string waypoint, string state, string att, string strAttValue)

--------------------------------------------------------------------------------

[CLASS] CloudPrintingServiceManager
Full Name: Autodesk.AutoCAD.Internal.CloudPrintingServiceManager

  CONSTRUCTORS:
    new CloudPrintingServiceManager()

  PROPERTIES:
    static ICloudPrintingService Host { get; set; }

--------------------------------------------------------------------------------

[CLASS] ColorThemeChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.ColorThemeChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new ColorThemeChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[ENUM] ColorThemeEnum
Full Name: Autodesk.AutoCAD.Internal.ColorThemeEnum
Inherits: Enum

  Values:
    - Dark = 0
    - Light = 1
    - User = 2

--------------------------------------------------------------------------------

[CLASS] CommandCallback
Full Name: Autodesk.AutoCAD.Internal.CommandCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new CommandCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke()

--------------------------------------------------------------------------------

[CLASS] CommandPiper
Full Name: Autodesk.AutoCAD.Internal.CommandPiper
Implements: IDisposable

  CONSTRUCTORS:
    new CommandPiper(int layerMgrCtrlId, bool bInRibbon)
    new CommandPiper()

  PROPERTIES:
    static int BlockingCommands { get; set; }
    static int LayerCount { get; set; }
    static int QueueCount { get; }

  METHODS:
    static void CommandsSentButNotStartedClear()
    static bool CommandWillStart(int layerMgrCtrlId)
    void Dispose()
    static int GetRegenLayers(ObjectIdCollection layerIds)
    static bool IsBlockingCommand(string cmdName)
    void LayerClose()
    bool LayerColor(Color value, string layerName)
    bool LayerColor(Color value, ArrayList layerNames)
    void LayerCurrent(string layerName)
    void LayerDelete(string layerName)
    void LayerDelete(ArrayList layerNames)
    void LayerDescription(string value, bool existingValueEmpty, string layerName)
    void LayerDescription(string value, bool existingValueEmpty, ArrayList layerNames)
    void LayerFilterDelete(ArrayList filtersNames)
    void LayerFilterEditGroup(bool add, string filterName, string layerName)
    void LayerFilterEditGroup(bool add, string filterName, ArrayList layerNames)
    void LayerFilterEditGroupReplace(string filterName, string parentName, ArrayList layerNames)
    void LayerFilterEditPropertyConvert(string filterName)
    void LayerFilterEditPropertyDef(string filterName, string filterExpression)
    void LayerFilterNewGroup(string filterName, string parentName)
    void LayerFilterNewProperty(string filterName, string filterExpression, string parentName)
    void LayerFilterRename(string value, string filterName)
    void LayerFilterSet(string filterName)
    void LayerFrozen(bool value, string layerName, ObjectId layerId)
    void LayerFrozen(bool value, ArrayList layerNames, ObjectIdCollection layerIds)
    void LayerLinetype(string value, string layerName)
    void LayerLinetype(string value, ArrayList layerNames)
    void LayerLineWeight(LineWeight value, string layerName)
    void LayerLineWeight(LineWeight value, ArrayList layerNames)
    void LayerLocked(bool value, string layerName)
    void LayerLocked(bool value, ArrayList layerNames)
    void LayerNew(string layerName)
    void LayerOffCurrent(string layerName)
    void LayerOn(bool value, string layerName)
    void LayerOn(bool value, ArrayList layerNames)
    void LayerPlot(bool value, string layerName)
    void LayerPlot(bool value, ArrayList layerNames)
    void LayerPlotStyle(string value, string layerName)
    void LayerPlotStyle(string value, ArrayList layerNames)
    void LayerReconcile(string layerName)
    void LayerReconcile(ArrayList layerNames)
    void LayerRename(string value, string layerName)
    void LayerTemplate(string protoName, string layerName, bool showUsed, bool freezeInAllVPs)
    void LayerTransparency(string value, string layerName)
    void LayerTransparency(string value, ArrayList layerNames)
    static bool OnCommandEnded()
    static void QueueClear()
    static void QueueSend()
    void Send()
    static bool SkipUpdate()
    bool VplayerColor(Color value, bool all, string layerName)
    bool VplayerColor(Color value, bool all, ArrayList layerNames)
    void VplayerFrozen(bool value, VPType flag, string layerName, ObjectId layerId)
    void VplayerFrozen(bool value, VPType flag, ArrayList layerNames, ObjectIdCollection layerIds)
    void VplayerLinetype(string value, bool all, string layerName)
    void VplayerLinetype(string value, bool all, ArrayList layerNames)
    void VplayerLineWeight(LineWeight value, bool all, string layerName)
    void VplayerLineWeight(LineWeight value, bool all, ArrayList layerNames)
    void VplayerPlotStyle(string value, bool all, string layerName)
    void VplayerPlotStyle(string value, bool all, ArrayList layerNames)
    void VplayerRemoveOverrides(string theProperty, bool all, string layerName)
    void VplayerRemoveOverrides(string theProperty, bool all, ArrayList layerNames)
    void VplayerReset(bool all, string layerName)
    void VplayerReset(bool all, ArrayList layerNames)
    void VplayerTransparency(string value, bool all, string layerName)
    void VplayerTransparency(string value, bool all, ArrayList layerNames)
    void VplayerViewportVisibilityDefault(bool bFrozen, bool all, string layerName)
    void VplayerViewportVisibilityDefault(bool bFrozen, bool all, ArrayList layerNames)

--------------------------------------------------------------------------------

[ENUM] CommandTypeFlags
Full Name: Autodesk.AutoCAD.Internal.CommandTypeFlags
Inherits: Enum

  Values:
    - NoneCmd = 0
    - CoreCmd = 1
    - ARXCmd = 2
    - LispCmd = 3
    - ActionMacroCmd = 4
    - SetvarCmd = 5

--------------------------------------------------------------------------------

[ENUM] ConsentStateEnum
Full Name: Autodesk.AutoCAD.Internal.ConsentStateEnum
Inherits: Enum

  Values:
    - kUnspecified = 0
    - kGranted = 1
    - kRevoked = 2
    - kRevokePending = 3

--------------------------------------------------------------------------------

[CLASS] CoreLayerUtilities
Full Name: Autodesk.AutoCAD.Internal.CoreLayerUtilities

  PROPERTIES:
    static int RegenPending { get; }

  METHODS:
    static int LayerNameCompareNatural(string str1, string str2)
    static void RegenLayers(ObjectId[] layers, int regenPending, int regenRequired)
    static void RegenLayers(ObjectId[] layers, int regenPending)

--------------------------------------------------------------------------------

[CLASS] CoreUtils
Full Name: Autodesk.AutoCAD.Internal.CoreUtils

  CONSTRUCTORS:
    new CoreUtils()

  METHODS:
    static bool AreFilesSame(string file1, string file2)
    static void GraphScr()
    static bool SetUndoRequiresRegen(Database db)
    static void TextScr()
    static bool WcMatch(string str, string pattern)
    static void WriteDebugConsole(string msg)

--------------------------------------------------------------------------------

[CLASS] CoreXRefUtils
Full Name: Autodesk.AutoCAD.Internal.CoreXRefUtils

  CONSTRUCTORS:
    new CoreXRefUtils()

  METHODS:
    static ObjectId[] getDelayResolveXRefRelativePaths()
    static bool isMarkedForDelayXRefRelativePathResolve(ObjectId defId)
    static void markForDelayXRefRelativePathResolve(ObjectId defId)
    static void unmarkForDelayXRefRelativePathResolve(ObjectId defId)

--------------------------------------------------------------------------------

[CLASS] CustomizationUtilities
Full Name: Autodesk.AutoCAD.Internal.CustomizationUtilities

  METHODS:
    static void SetCustomizationInterface(object customization)

--------------------------------------------------------------------------------

[CLASS] DbBlockImageCache
Full Name: Autodesk.AutoCAD.Internal.DbBlockImageCache

  CONSTRUCTORS:
    new DbBlockImageCache()

  METHODS:
    void addDbReactors()
    void addToCache(BlockImageCacheKey key, BlockImageCacheStruct cache)
    bool getCachedBlockBitmap(BlockImageCacheKey queryKey, ref BlockImageCacheStruct result)
    void OnDatabaseToBeDestroyed(object sender, EventArgs e)
    void OnDocumentCreated(object sender, DocumentCollectionEventArgs e)
    void OnObjectErased(object sender, ObjectErasedEventArgs e)
    void OnObjectModified(object sender, ObjectEventArgs e)
    void OnObjectUnAppended(object sender, ObjectEventArgs e)

--------------------------------------------------------------------------------

[CLASS] DebugUtil
Full Name: Autodesk.AutoCAD.Internal.DebugUtil

  CONSTRUCTORS:
    new DebugUtil()

  METHODS:
    static void Assert(bool condition, string message, string detailMessage)
    static void Assert(bool condition, string message)
    static void Assert(bool condition)
    static bool Verify(bool b, string t)
    static bool Verify(bool b)

--------------------------------------------------------------------------------

[CLASS] DMFileData
Full Name: Autodesk.AutoCAD.Internal.DMFileData

  CONSTRUCTORS:
    new DMFileData(string strID, string strRelPath)
    new DMFileData()

--------------------------------------------------------------------------------

[CLASS] DMFileUtil
Full Name: Autodesk.AutoCAD.Internal.DMFileUtil

  CONSTRUCTORS:
    new DMFileUtil()

  PROPERTIES:
    static GetDMFileItemCallback GetDMFileCallback { get; set; }

--------------------------------------------------------------------------------

[CLASS] DocumentWindowUtil
Full Name: Autodesk.AutoCAD.Internal.DocumentWindowUtil

  CONSTRUCTORS:
    new DocumentWindowUtil()

  METHODS:
    [SKIPPED METHOD] CanClose: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    [SKIPPED METHOD] SetNextActiveWindow: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] DxLegacyBinaryTemplate
Full Name: Autodesk.AutoCAD.Internal.DxLegacyBinaryTemplate

  CONSTRUCTORS:
    new DxLegacyBinaryTemplate()

  PROPERTIES:
    ArrayList AttributeList { get; }
    ArrayList BlockList { get; }

  METHODS:
    void LoadFromFile(string filename)

--------------------------------------------------------------------------------

[CLASS] ExtensionApplication
Full Name: Autodesk.AutoCAD.Internal.ExtensionApplication
Implements: IExtensionApplication

  CONSTRUCTORS:
    new ExtensionApplication()

  METHODS:
    void Initialize()
    void Terminate()

--------------------------------------------------------------------------------

[CLASS] ExtensionApplication_App
Full Name: Autodesk.AutoCAD.Internal.ExtensionApplication_App
Implements: IExtensionApplication

  CONSTRUCTORS:
    new ExtensionApplication_App()

  METHODS:
    void Initialize()
    void Terminate()

--------------------------------------------------------------------------------

[CLASS] ExternalBlockListResultHost
Full Name: Autodesk.AutoCAD.Internal.ExternalBlockListResultHost

  CONSTRUCTORS:
    new ExternalBlockListResultHost()

  METHODS:
    [SKIPPED METHOD] getBlockDataSource: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    string getBlockListRequestFileInfo()
    [SKIPPED METHOD] getBlocksFromExternalFile: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    bool parseJsonBlockList(string resultJsonStr, BlockViewMode viewMode)
    [SKIPPED METHOD] setBlockDataSource: Could not load file or assembly 'WindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] FocusChangedEventHandler
Full Name: Autodesk.AutoCAD.Internal.FocusChangedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new FocusChangedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    [SKIPPED METHOD] BeginInvoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    void EndInvoke(IAsyncResult result)
    [SKIPPED METHOD] Invoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] getBlockSyncFolderFromServerHandler
Full Name: Autodesk.AutoCAD.Internal.getBlockSyncFolderFromServerHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new getBlockSyncFolderFromServerHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(int __unnamed000, AsyncCallback callback, object obj)
    string EndInvoke(IAsyncResult result)
    string Invoke(int A_0)

--------------------------------------------------------------------------------

[CLASS] getConsentStatusHandler
Full Name: Autodesk.AutoCAD.Internal.getConsentStatusHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new getConsentStatusHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(int __unnamed000, AsyncCallback callback, object obj)
    int EndInvoke(IAsyncResult result)
    int Invoke(int A_0)

--------------------------------------------------------------------------------

[CLASS] GetDMFileItemCallback
Full Name: Autodesk.AutoCAD.Internal.GetDMFileItemCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetDMFileItemCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(string filePath, DMFileData fileData, AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke(string filePath, DMFileData fileData)

--------------------------------------------------------------------------------

[CLASS] getOnBoardingCompletedHandler
Full Name: Autodesk.AutoCAD.Internal.getOnBoardingCompletedHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new getOnBoardingCompletedHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    bool EndInvoke(IAsyncResult result)
    bool Invoke()

--------------------------------------------------------------------------------

[CLASS] GetSharedRecentFilesCallback
Full Name: Autodesk.AutoCAD.Internal.GetSharedRecentFilesCallback
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetSharedRecentFilesCallback(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(List<SharedRecentFileData> data, int nItems, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(List<SharedRecentFileData> data, int nItems)

--------------------------------------------------------------------------------

[CLASS] GetTheAnswerDelegate
Full Name: Autodesk.AutoCAD.Internal.GetTheAnswerDelegate
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GetTheAnswerDelegate(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AcString* pngFilePath, AcBlockPreviewInfo* previewTaskInfo, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(AcString* pngFilePath, AcBlockPreviewInfo* previewTaskInfo)

--------------------------------------------------------------------------------

[CLASS] getValueAsStringHandler
Full Name: Autodesk.AutoCAD.Internal.getValueAsStringHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new getValueAsStringHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(AsyncCallback callback, object obj)
    string EndInvoke(IAsyncResult result)
    string Invoke()

--------------------------------------------------------------------------------

[CLASS] GroupControlEventHandler
Full Name: Autodesk.AutoCAD.Internal.GroupControlEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GroupControlEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(object sender, EventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(object sender, EventArgs e)

--------------------------------------------------------------------------------

[ENUM] GroupControlEventIndex
Full Name: Autodesk.AutoCAD.Internal.GroupControlEventIndex
Inherits: Enum

  Values:
    - Show = 0
    - Hide = 1
    - ChevronClick = 2
    - TitleButtonClick = 3
    - FocusChange = 4
    - Resize = 5

--------------------------------------------------------------------------------

[CLASS] GroupControlHeaderButton
Full Name: Autodesk.AutoCAD.Internal.GroupControlHeaderButton
Implements: IDisposable

  CONSTRUCTORS:
    new GroupControlHeaderButton(    [SKIPPED CONSTRUCTOR]: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    new GroupControlHeaderButton()

  PROPERTIES:
        [SKIPPED PROPERTY] Bitmap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    bool Enabled { get; set; }
        [SKIPPED PROPERTY] Parent: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    string ToolTip { get; set; }

  METHODS:
    void Dispose()

  EVENTS:
    event EventHandler Click

--------------------------------------------------------------------------------

[ENUM] GroupControlStyles
Full Name: Autodesk.AutoCAD.Internal.GroupControlStyles
Inherits: Enum

  Values:
    - ShowChevron = 1
    - ShowButtonForce = 2
    - ShowNonActive = 4
    - ShowNoBorder = 8
    - ShowTransparentButton = 16
    - ShowButtonBorder = 32
    - ShowOpaqueImage = 128

--------------------------------------------------------------------------------

[CLASS] GroupResizedEventHandler
Full Name: Autodesk.AutoCAD.Internal.GroupResizedEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new GroupResizedEventHandler(object A_0, IntPtr A_1)

  METHODS:
    [SKIPPED METHOD] BeginInvoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.
    void EndInvoke(IAsyncResult result)
    [SKIPPED METHOD] Invoke: Could not load file or assembly 'System.Windows.Forms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'. The system cannot find the file specified.

--------------------------------------------------------------------------------

[CLASS] GsfUtil
Full Name: Autodesk.AutoCAD.Internal.GsfUtil

  CONSTRUCTORS:
    new GsfUtil()

  METHODS:
    static int GsfLevel()

--------------------------------------------------------------------------------

