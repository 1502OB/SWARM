# REVISION PLAN: 24-071 Pacific Edge Hotel, Ocean North 100% DD (Surf F-, Driftwood/Villa D-)
Run 20261104-1905. Planner: revision-planner (check tag PLAN-20261104-1905). Model: PLMB-24220-R25 (title confirmed). Issue 11-05-2026.
Inputs: manifests/requirements.json (77 req: 34 clear, 25 needs_info, 1 conflict, 17 other_discipline), COMMENT_LOG.md + crops, cache/revit-index.json, plus 8 read-only Revit dumps (all transactionMode none).

## Model state found (read-only)
- Sheet set matches the reviewed PDFs: F-P0.00-0.13, F-P1.01-1.04, F-P2.01-2.04, D-P0.00-0.13, D-P1.01-1.04, D-P2.01-2.04 (44 sheets). The model titles call P1.03/P2.03 "BASEMENT (AND GROUND) LEVEL"; the comment log calls them "L3". Same sheets and views, so nothing is superseded.
- Plan views have no view templates on Surf. Scale 1/8" (96). Views: F-P1.01 18215302, F-P1.02 18215641, F-P1.03 18216028 + 18216239, F-P1.04 15476126, F-P2.01 22821417 (Detail Level Coarse; the others are Fine), F-P2.02 22821494, F-P2.03 22821504 + 22821517, F-P2.04 22821532. D-P views: 18238206, 18238991, 18240583, 18238136, 22874443, 22874794, 22875878, 22876358 (template "Plumbing Plan" on most).
- Links: ARCH ...ON.rvt (inst 22841688), ARCH ...OS.rvt (22841683), STRU Reef (22883216). All are loaded and shown By Host View, except view 18215302 (Custom on 22841688). There are no DWG/PDF imports in the plan views. F-P2.04 has the raster image 22841584 "F+P Surf Roof Drainage Diagram.pdf".
- Host sprinklers: 0, so the heads on the plans come from the arch link.
- Plumbing fixtures modeled: Surf Beach 30 (5 tagged), Alley 7, Basement 2, Ground 2 (0 tagged). D/V plan views: 0. Most guestroom/restroom fixtures are not modeled in plumbing.
- The legend on F-P0.01 is the "Sticky-P0.2 Symbol Legend" schedule (22778659): one Mark field, 2 empty rows, so it is graphic-only. It cannot be extended by a script.
- Title blocks: "RDC Title Block Arch E1_SURF" (22) and "..._DRIFTWOOD" (22). 'SHEET Sorting Number' is a NON-shared project parameter (Double; 5 on the P1/P2 sheets, 0 on the P0 sheets). 'Sheet Issue Date' reads 05/28/25 or 09/02/26.
- Revisions: only one ("Description", seq 16). The requirements name no revision, so no task adds revision clouds (DD progress issue).

## Summary table
| Req | Exact change | Sheet / view | Agent | Depends on | Order |
|---|---|---|---|---|---|
| R4 | Delete TN 20332878 + 22821542 "SLEEPY HOLLOW LANE" (R4-T1). The BFP symbol at grid 3 goes to manual R4-M1 | F-P1.04 / 15476126, F-P2.04 / 22821532 | scripter -> revit-writer -> verifier | - | B1 |
| R5 | Copy the crop of 18215302 to 18215641/22821417/22821494; move viewports 18215898/22821568/22821569 to match vp 18215890 (R5-T1) | F-P1.01, F-P1.02, F-P2.01, F-P2.02 | scripter -> revit-writer -> verifier | - | B1 |
| R7 | Hide OST_Sprinklers in 10 Surf plan views (R7-T1) | all F-P1/F-P2 plans | scripter -> revit-writer -> verifier | - | B1 |
| R9 | Delete TN 22821493 "APPROXIMATE LOCATION OF CIVIL SEWAGE PUMP STATION" (R9-T1) | F-P2.01 / 22821417 | scripter -> revit-writer -> verifier | - | B1 |
| R58 | 'Sheet Issue Date' = "11.05.26" on 44 F-P/D-P sheets (R58-T1). The "DD" label is an open question | all F-P*, D-P* | scripter -> revit-writer -> verifier | - | B1 |
| R59 | 'SHEET Sorting Number' = 8 on 44 sheets (R59-T1). Publish steps are manual R59-M1 | all F-P*, D-P* | manual R59-M1, then scripter -> revit-writer -> verifier | R59-M1 | B2 |
| R1 | Reload the 11-04 arch model (Transin #88) | all plans | manual | - | M0 (first) |
| R2 | Hide unwanted linked linework (linked elements cannot be hidden by API) | F-P2.01, F-P2.02 | manual | R1 | M1 |
| R3 | Hide "1/F-A6.04"-style arch callouts | all Surf plans | manual + open question | R1, Q-P1 | M1 |
| R6 | Two sets of roof slopes | F-P2.04 / 22821532 | open question Q-P2 (then 1 delete task) | Q-P2 | - |
| R8 | Add water meter (Water Meter-MAIN : MAIN METER 2", 22822844) symbol to the graphic legend | F-P0.01 sticky legend | manual | - | M1 |
| R12 | Fixture tags at Surf restrooms/guestrooms | F-P1.01-1.03 | open question Q-P3 + manual (fixtures not modeled) | R13, Q-P3 | - |
| R13 | Model waste/vent on the upper floors | F-P1.02, F-P1.03 | manual (design + DFU calc) | R1 | M2 |
| R14 | Coordinate W/V transfers between Beach/Alley | F-P1.01, F-P1.02 | manual | R13 | M2 |
| R16 | Show VTRs on the roof | F-P1.04 | manual | R13 | M3 |
| R18 | Surf sewer exit inverts to C3 (overdue) | F-P1.01 | manual (calc + email) | - | M0 |
| R21 | Locate/size CW/HW risers | F-P2.01-2.03 | manual (calc) | R1 | M2 |
| R22 | CW to fixtures or enlarged plans (Jeremy's choice) | F-P2.01-2.03 | manual | R21 | M3 |
| R23 | CW line to the water tap | F-P2.01 | manual | R1 | M2 |
| R24 | Hose bibb at the private deck, Guestroom 6106 | F-P2.01 | manual | R1 | M2 |
| R25 | Move the capped stub-out + note TN 22831292 to the revised pool equipment room | F-P2.01 / 22821417 | manual | R1 | M2 |
| R26 | Pool stub-out termination, sleeves, flexible-connection note | F-P2.01 | manual | R25 | M3 |
| R27 | Show all storm leaders incl. from above | F-P2.02, F-P2.03 | manual (calc) | R1 | M2 |
| R29 | Flip slope/drain at the elevator overrun, leader in chase, overflow at roof | F-P2.04 | manual | R1 | M2 |
| R41 | D/V title block text overlap / "SHEET NOTES" on one line (title block family labels) | D-P1.01 (all D-P) | manual (part of the title block swap) | R59-M1 | M1 |
| R42 | D/V restroom W/V + floor drains | D-P1.01-1.03 | manual (design + DFU) | R1 | M2 |
| R44 | D/V sewer exit location/invert with C3 | D-P1.01 | manual (calc + email) | - | M0 |
| R46 | FDs/FSs per the FS drawings (10-27 set) | D-P1.02 | manual | R1 | M2 |
| R47 | Vent stacks at the roof | D-P1.04 | manual | R42 | M3 |
| R48 | FS water + gas connections | D-P2.01, D-P2.02 | manual (gas calc) | R46 | M3 |
| R49 | Restroom fixture tags + connections (0 fixtures modeled) | D-P2.01-2.03 | manual | R42 | M3 |
| R53 | Gas at the fireplaces | D-P2.01, D-P2.02 | manual (gas calc) | - | M2 |
| R55 | Roof drain + scupper to downspout at the elevator tower | D-P2.04 | manual | R1 | M2 |
| R56 | Call out existing gutters/downspouts to remain; leaders per existing gutters | D-P2.01-2.04 | manual | R1 | M2 |

## Per requirement (executable)
### R9: R9-T1 delete-text-note
- Target: TN 22821493 in view 22821417 (F-P2.01).
- Verifier: the element no longer exists, and no text note in 22821417 contains "SEWAGE PUMP STATION".

### R4: R4-T1 delete-text-note (plus manual R4-M1)
- Targets: TN 20332878 (view 15476126, F-P1.04) and TN 22821542 (view 22821532, F-P2.04), both "SLEEPY HOLLOW LANE".
- Verifier: both are gone. TN 21610240 and 21610255, detail items 20333030 / 22821545 and raster 22841584 still exist.
- R4-M1 (manual): the dashed enclosure + BFP symbol at grid 3 on the roof plans is not a host element (0 pipe accessories in either roof view, view range bottom = roof level). After R1, find its source (link or detail item 20333030/22821545, bbox x177-190, y164-241) and hide it.

### R7: R7-T1 hide-category-in-view
- Hides OST_Sprinklers in views 18215302, 18215641, 18216028, 18216239, 15476126, 22821417, 22821494, 22821504, 22821517, 22821532.
- Verifier: GetCategoryHidden(OST_Sprinklers) = true in all 10. For 18215302, report the Custom link override of 22841688 and confirm linked sprinklers are not visible (collector on the link doc visible in view, or a screenshot of F-P1.01).
- No "reference only" note: the reviewer accepted hiding, and E28 says FP is not 120D scope.

### R5: R5-T1 align-viewports
- Reference: vp 18215890 / view 18215302 on F-P1.01 (box center 2.08, 1.29).
- Step 1: copy the crop + annotation crop to 18215641, 22821417, 22821494.
- Step 2: move vp 18215898 (F-P1.02), 22821568 (F-P2.01), 22821569 (F-P2.02).
- Verifier: the projection-to-sheet position of model point (232,184,0) is equal within 0.001 ft on all 4 viewports, the crop boxes are equal, and title blocks have not moved.
- Not included: F-P1.03/2.03 and the roof sheets. The reviewer asked only for Beach/Alley.

### R58: R58-T1 set-sheet-parameter
- Sets 'Sheet Issue Date' = "11.05.26" on the 44 F-P/D-P sheets.
- Verifier: count = 44, and every value is "11.05.26".
- Where "DD" appears, and whether the RDC title block is the DyeLot building title block, are Q-P4 and Q-P5 (title block content is Jeremy's).

### R59: R59-M1 (manual), then R59-T1
- R59-M1 (Jeremy): consultant title block from ACC (confirm the folder: BIMxP says both 'Architectural' and 'Interior Design'), fonts on the PDF printer, all 5 shared parameters from the Parameter Service, Transfer Project Information, acquire coordinates, links 'By Linked View' per BIMxP, then publish. Due EOD 11/4, or send CAD instead.
- R59-T1: 'SHEET Sorting Number' = 8 on the 44 sheets, preferring the shared parameter if one was loaded.
- Verifier: count = 44, value 8, and report shared or non-shared.

## Execution order
- **Batch 1 (Revit, one writer, sequential; may start now):** R9-T1 -> R4-T1 -> R7-T1 -> R5-T1 -> R58-T1. These are independent of the link reload: they touch host notes, view categories, crops, viewports and sheet parameters.
- **Batch 2 (Revit):** R59-T1, after Jeremy finishes R59-M1. It must run before the publish.
- **Manual worklist for Jeremy (no scripter):**
  - M0 now: R1 reload, R18 + R44 inverts to C3.
  - M1: R2, R3, R4-M1, R8, R41 (with the title block swap R59-M1).
  - M2 design modeling: R13, R14, R21, R23, R24, R25, R27, R29, R42, R46, R53, R55, R56.
  - M3: R16, R22, R26, R47, R48, R49.
- There are no AutoCAD tasks (project.json lists revit only). No Revit task runs in parallel with another Revit task.

## Open questions
Planner questions (to Jeremy):
- Q-P1 (R3): "1/F-A6.04" sits inside the arch room tag in the link, and the host has 0 room tags. Hide linked Room Tags in the Surf plans and place host room tags (name + number only) on the linked rooms? yes/no
- Q-P2 (R6): F-P2.04 has raster 22841584 "F+P Surf Roof Drainage Diagram.pdf" over the linked roof, which is the second set of slopes. Delete it (or hide it in 22821532) so only the arch slopes remain? yes/no
- Q-P3 (R12): Surf has no enlarged plans, and your rule is that fixture tags come from enlarged plans only. Tag the 36 modeled fixtures on overall F-P1.01-1.03 now? yes/no. If yes, which tag type: Plumbing MEP FIXTURE Tag "Standard" (22145275) or "STANDARD W/ FLOW" (22145277)? Guestroom fixtures are not modeled, so they belong with the R13/R22 modeling.
- Q-P4 (R58): which title block field carries the "DD" in "DD 11.05.26": a Milestone field, the sheet issue text, or the new DyeLot title block? Give the value.
- Q-P5 (R58/R59): is the current "RDC Title Block Arch E1_SURF/_DRIFTWOOD" the DyeLot building-specific title block from E3, or must it be swapped from ACC? yes/no
- Q-P6 (R59): should SHEET Sorting Number = 8 also go on the other buildings' plumbing sheets in this model (B-, C-, L-, P-, R-, S-, T-)? yes/no

needs_info / conflict (quoted from requirements.json):
- R60 (conflict, Jeremy): E1 Brian 11-04 says "DD 11.05.26", publish EOD 11/4. E27 (Fathom 11/3) says "MEP deliverables are due Friday" (11/6). Elise 10/27 said 11/4. Which date governs the DD set: 11/5? yes/no
- R10 (Jeremy, Bluebeam): what is the dashed X on F-P1.01 south of the TP-1 restroom (check after R1)? If it is arch linework, hide it? yes/no
- R11 (Jeremy, Bluebeam): which family is the "floating equip?" by Guestroom 6203 / stair on F-P2.02, and should it move into the closet? yes/no
- R15 (DyeLot/Brian): open-corridor floor drains "as discussed": spacing/quantity, and do they discharge to storm? (value)
- R17 (Jeremy, Bluebeam): is the pipe vs door header at F-P1.01 a real clash? (value: header height vs pipe invert)
- R19 (C3): Surf water service POC elevation at the Plumbing & Electrical Room? (value)
- R20 (Jeremy -> Danielle): booster pump recommended for Surf / Villa-Driftwood? yes/no (needs C3 POC elevations + the pressure-flow curves zip saved to Egnyte)
- R28 (C3): location/elevation of the existing Surf storm connection, and does it outfall to the ocean? (value)
- R30 (Jeremy, Bluebeam): show hydronic piping to the fan coil on the plumbing plans (likely Mechanical/Jeffrey)? yes/no
- R31 (DyeLot): send the Acoustic BOD Report. May the Reef "Plumbing Acoustic Notes" (BOD 5.5/5.8) be reused for Surf? yes/no
- R32 (DyeLot): where is the glass filler (bar sink) located? (room)
- R33 (DyeLot/C3): is the Surf GI shift approved? yes/no
- R34 (C3/DyeLot): is the Driftwood GI shift approved? yes/no. Also send the GI spec (250228).
- R35 (Jeffrey): do the fitness room split units drain condensate to plumbing, and where? yes/no + location
- R36 (Structural): does the below-slab drainage need a plumbing sump/subdrain connection? yes/no
- R37 (Structural Focus): may plumbing sleeve through the footings at Structural p12? yes/no
- R38 (DyeLot): is the trench drain at the slab notch plumbing scope? yes/no + discharge
- R39 (DyeLot/waterproofing): does the paver drainage channel connect to plumbing storm? yes/no
- R40 (Technology): LV riser locations/sizes in the shared shafts? (value)
- R43 (Architect): is the D/V L3 restroom fixture layout (D088) in the 11-04 backgrounds? yes/no
- R45 (Jeremy, Bluebeam): is the D-P1.01 cleanout in the deck, and should it move to BOH? yes/no
- R50 (Architect): same as R43 for the D-P2.03 restroom tags/connections? yes/no
- R51 (C3): D/V water service POC elevation? (value)
- R52 (Kellie): send the gas meter exhibit (meter in front of the transformer)? (document)
- R54 (Jeremy): confirm the combined Villa+Driftwood WH system (min. 2 units, combined elec/WH room) from the 11/3 Fathom recap before answering D079? yes/no
- R57 (DyeLot): are the "Add drain" area drains at the exterior landing (Kitchen 9206 / Women's 9203) plumbing scope? yes/no

## By other discipline (standard response)
R61, R62, R63, R64, R65, R66, R67, R68, R69, R70, R71, R72, R73, R74, R75, R76, R77
