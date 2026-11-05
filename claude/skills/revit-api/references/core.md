# Core Revit 2025 API types used by our swarm scripts

Generated 2026-11-05 from the types that appear in swarm/**/*.cs (33 scripts), most-used first, with firm notes from swarm/LESSONS.md.
Look here first; the full API is in the other files of this folder (INDEX.md). Notes are facts that cost failed runs.

| Type | Full name(s) | File | Scripts using it | Note |
|---|---|---|---|---|
| FilteredElementCollector | Autodesk.Revit.DB.FilteredElementCollector | db-f.md | 36 |  |
| ElementId | Autodesk.Revit.DB.ElementId | db-e.md | 27 | Revit 2024+: use new ElementId(long) and .Value (long); IntegerValue still exists in 2025 but is the old int form, so prefer .Value. |
| BuiltInCategory | Autodesk.Revit.DB.BuiltInCategory | db-b-c.md | 26 |  |
| BuiltInParameter | Autodesk.Revit.DB.BuiltInParameter | db-b-c.md | 25 |  |
| FamilySymbol | Autodesk.Revit.DB.FamilySymbol | db-f.md | 22 |  |
| Element | Autodesk.Revit.DB.Element | db-e.md | 21 | Pinned, OwnerViewId and the indexed property Parameter[BuiltInParameter] (called as get_Parameter(...) in C#) are declared here (inherited by Viewport, TextNote, ...). New elements land on the ACTIVE workset: set ELEM_PARTITION_PARAM (R22). |
| Curve | Autodesk.Revit.DB.Curve | db-b-c.md | 16 |  |
| Location | Autodesk.Revit.DB.Location | db-j-l.md | 15 |  |
| ViewSheet | Autodesk.Revit.DB.ViewSheet | db-u-v.md | 15 |  |
| TextNote | Autodesk.Revit.DB.TextNote | db-t.md | 15 | No Replace on FormattedText: GetFormattedText(), find the index, SetPlainText(new TextRange(start,len), text), SetFormattedText (keeps formatting). TextNote.Text is inherited from TextElement. |
| View | Autodesk.Revit.DB.View | db-u-v.md | 14 | Rotating a plan: setting CropBox with a rotated transform does NOT rotate the view; rotate the crop region element with ElementTransformUtils.RotateElement (R23). SetCategoryHidden needs CanCategoryBeHidden; views with a V/G-controlling template ignore it. |
| Parameter | Autodesk.Revit.DB.Parameter | db-n-p.md | 14 |  |
| IndependentTag | Autodesk.Revit.DB.IndependentTag | db-g-i.md | 14 | ~1 tag/s with leaders; >100 per call passes the bridge timeout (R7). Tags in dependent views belong to the parent (R8). |
| Document | Autodesk.Revit.Creation.Document; Autodesk.Revit.DB.Document | creation.md; db-d.md | 13 | Creation.Document (doc.Create) holds NewTransitionFitting / NewTeeFitting / NewTakeoffFitting / NewSpotElevation; DB.Document holds Delete, Regenerate, GetElement. NewTransitionFitting: pass the PIPE connector first (a family connector first fails "owner should be flex duct or pipe") (R20). A plumb branch off a 1/4 in/ft sloped pipe fails NewTee/NewTakeoff angle checks: place the tee family, BreakCurve, ConnectTo (R20). |
| LocationCurve | Autodesk.Revit.DB.LocationCurve | db-j-l.md | 12 |  |
| Viewport | Autodesk.Revit.DB.Viewport | db-u-v.md | 12 | SetBoxCenter / GetBoxCenter move it; Rotation is ViewportRotation; Pinned is inherited from Element. |
| Line | Autodesk.Revit.DB.Line | db-j-l.md | 11 |  |
| Pipe | Autodesk.Revit.DB.Plumbing.Pipe | db-plumbing.md | 11 |  |
| Transaction | Autodesk.Revit.DB.Transaction | db-t.md | 10 |  |
| StorageType | Autodesk.Revit.DB.StorageType | db-s.md | 10 |  |
| ViewPlan | Autodesk.Revit.DB.ViewPlan | db-u-v.md | 10 | See View (rotation via crop region element, R23). |
| TransactionStatus | Autodesk.Revit.DB.TransactionStatus | db-t.md | 9 |  |
| FailureSeverity | Autodesk.Revit.DB.FailureSeverity | db-f.md | 9 |  |
| Category | Autodesk.Revit.DB.Category | db-b-c.md | 9 |  |
| FailureHandlingOptions | Autodesk.Revit.DB.FailureHandlingOptions | db-f.md | 9 |  |
| FailureMessageAccessor | Autodesk.Revit.DB.FailureMessageAccessor | db-f.md | 9 |  |
| FailureProcessingResult | Autodesk.Revit.DB.FailureProcessingResult | db-f.md | 9 |  |
| Transform | Autodesk.Revit.DB.Transform | db-t.md | 9 |  |
| IFailuresPreprocessor | Autodesk.Revit.DB.IFailuresPreprocessor | db-g-i.md | 9 |  |
| FamilyInstance | Autodesk.Revit.DB.FamilyInstance | db-f.md | 9 |  |
| LocationPoint | Autodesk.Revit.DB.LocationPoint | db-j-l.md | 9 |  |
| FailuresAccessor | Autodesk.Revit.DB.FailuresAccessor | db-f.md | 9 | Use in an IFailuresPreprocessor: delete warnings, ProceedWithRollBack on errors; FailureHandlingOptions.SetForcedModalHandling(false) so no dialog blocks the bridge. |
| BoundingBoxXYZ | Autodesk.Revit.DB.BoundingBoxXYZ | db-b-c.md | 9 |  |
| TagOrientation | Autodesk.Revit.DB.TagOrientation | db-t.md | 9 |  |
| Reference | Autodesk.Revit.DB.Reference | db-r.md | 9 |  |
| RevisionCloud | Autodesk.Revit.DB.RevisionCloud | db-r.md | 9 | Curves must be clockwise; one Revision Tag per cloud; see revit-conventions for the firm standard (R25). |
| Revision | Autodesk.Revit.DB.Revision | db-r.md | 8 |  |
| TransactionGroup | Autodesk.Revit.DB.TransactionGroup | db-t.md | 6 | Multi-step batches: transactionMode "none" + own TransactionGroup, one Transaction per step, roll back the group on any error. |
| RevitLinkInstance | Autodesk.Revit.DB.RevitLinkInstance | db-r.md | 5 |  |
| Leader | Autodesk.Revit.DB.Leader | db-j-l.md | 5 |  |
| ElementOwnerViewFilter | Autodesk.Revit.DB.ElementOwnerViewFilter | db-e.md | 5 |  |
| HorizontalTextAlignment | Autodesk.Revit.DB.HorizontalTextAlignment | db-g-i.md | 5 |  |
| ConnectorManager | Autodesk.Revit.DB.ConnectorManager | db-b-c.md | 4 |  |
| MEPCurve | Autodesk.Revit.DB.MEPCurve | db-m.md | 4 |  |
| MEPSystem | Autodesk.Revit.DB.MEPSystem | db-m.md | 4 |  |
| ConnectorType | Autodesk.Revit.DB.ConnectorType | db-b-c.md | 4 |  |
| Connector | Autodesk.Revit.DB.Connector | db-b-c.md | 4 | Connector.Owner elements go invalid after a delete in the same transaction: keep ElementIds and re-fetch with doc.GetElement (R26). |
| MEPModel | Autodesk.Revit.DB.MEPModel | db-m.md | 4 |  |
| ViewType | Autodesk.Revit.DB.ViewType | db-u-v.md | 4 |  |
| TransformWithBoundary | Autodesk.Revit.DB.TransformWithBoundary | db-t.md | 4 | Use GetModelToProjectionTransform(); there is NO GetTransform() (R20, Pad 11). |
| CurveLoop | Autodesk.Revit.DB.CurveLoop | db-b-c.md | 4 |  |
| RoutingPreferenceRuleGroupType | Autodesk.Revit.DB.RoutingPreferenceRuleGroupType | db-r.md | 3 |  |
| SubTransaction | Autodesk.Revit.DB.SubTransaction | db-s.md | 3 |  |
| PipingSystemType | Autodesk.Revit.DB.Plumbing.PipingSystemType | db-plumbing.md | 3 |  |
| Level | Autodesk.Revit.DB.Level | db-j-l.md | 3 |  |
| CheckoutStatus | Autodesk.Revit.DB.CheckoutStatus | db-b-c.md | 3 |  |
| RoutingPreferenceManager | Autodesk.Revit.DB.RoutingPreferenceManager | db-r.md | 3 |  |
| WorksharingUtils | Autodesk.Revit.DB.WorksharingUtils | db-w-z.md | 3 |  |
| PipeType | Autodesk.Revit.DB.Plumbing.PipeType | db-plumbing.md | 3 |  |
| StructuralType | Autodesk.Revit.DB.Structure.StructuralType | db-structure-s-z.md | 3 |  |
| VerticalTextAlignment | Autodesk.Revit.DB.VerticalTextAlignment | db-u-v.md | 3 |  |
| WorksetId | Autodesk.Revit.DB.WorksetId | db-w-z.md | 2 |  |
| PipeSegment | Autodesk.Revit.DB.Plumbing.PipeSegment | db-plumbing.md | 2 |  |
| Workset | Autodesk.Revit.DB.Workset | db-w-z.md | 2 |  |
| PlanViewPlane | Autodesk.Revit.DB.PlanViewPlane | db-n-p.md | 2 |  |
| MEPSize | Autodesk.Revit.DB.MEPSize | db-m.md | 2 |  |
| PlanViewRange | Autodesk.Revit.DB.PlanViewRange | db-n-p.md | 2 |  |
| ViewSchedule | Autodesk.Revit.DB.ViewSchedule | db-u-v.md | 2 |  |
| ViewDrafting | Autodesk.Revit.DB.ViewDrafting | db-u-v.md | 2 |  |
| ScheduleSheetInstance | Autodesk.Revit.DB.ScheduleSheetInstance | db-s.md | 2 |  |
| SectionType | Autodesk.Revit.DB.SectionType | db-s.md | 2 |  |
| CurveElement | Autodesk.Revit.DB.CurveElement | db-b-c.md | 2 |  |
| SpotDimension | Autodesk.Revit.DB.SpotDimension | db-s.md | 2 | Spot elevation on a pipe: doc.Create.NewSpotElevation with Reference.ParseFromStableRepresentation(doc, pipe.UniqueId + ":9:LINEAR"); set "Display Elevations" = Bottom. No spot-slope API (R26). |
| Grid | Autodesk.Revit.DB.Grid | db-g-i.md | 2 |  |
| MEPSystemClassification | Autodesk.Revit.DB.MEPSystemClassification | db-m.md | 2 |  |
| FormattedText | Autodesk.Revit.DB.FormattedText | db-f.md | 0 | See TextNote: edit via TextRange; no Replace(). |
| ElementTransformUtils | Autodesk.Revit.DB.ElementTransformUtils | db-e.md | 9 | Static: MoveElement / RotateElement / CopyElement. Rotate a plan view via its crop region element (R23). |
| Definition | Autodesk.Revit.DB.Definition | db-d.md | 6 | Static utility class. |
| PlumbingUtils | Autodesk.Revit.DB.Plumbing.PlumbingUtils | db-plumbing.md | 3 | Static: BreakCurve(doc, pipeId, pt) for the manual tee on a sloped pipe (R20). |
