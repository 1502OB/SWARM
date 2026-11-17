// create-drafting-table: from CB6-R1-T11.v2 (23-073), verified
// CB6-R1-T11 | operation: create-drafting-table-on-sheet | run 20261109-1412 | Revit 2025 | v1
// Body of: public static object Execute(Document document, object[] parameters); send_code_to_revit transactionMode "auto".
// The bridge opens one transaction; all work runs in ONE SubTransaction so the script rolls back its own changes on any
// failure and returns "ROLLED_BACK <item>: <error>". Never saves or syncs.
// Creates drafting view "FEWD FIXTURE LIST AND GI SIZING" (scale 1, paper = model) with:
//   (A) FEWD FIXTURE LIST: title (1/8" Arial Bold), header + 56 rows F-1..F-56 (handoff Section 7 verbatim, TO GI = No for
//       F-12/F-24/F-44, basin sizes on F-1/F-9/F-18/F-21/F-25) + total row, one TextNote per cell (3/32" Arial Narrow),
//       grid of Thin Lines detail lines, row pitch 0.17";
//   (B) GI SIZING block (one wrapped note in a 4.3" box) below the table; (C) 3" x 2" FEWD APPROVAL STAMP box next to (B).
// Places it on P0.10 centred in the measured free area x 2.06..3.17, y 0.15..1.30 ft (viewport type without title if one
// exists), then one CLOCKWISE revision cloud on the sheet (revision 'FEWD Submittal') = viewport outline + 1/4", and one
// Revision Tag with head at (cloud right - 0.17", cloud top + 0.08"), raised until its box clears the cloud top by 0.08".
// NO_GO / ROLLED_BACK if outline leaves the free area or cloud/tag reach y 1.54 (Sticky-Sheet1) or x 3.241 (revision
// schedule) or overlap any live sheet item. DFU over TO GI = Yes rows is computed (data and read-back) and must be 56.
// parameters: ["dryrun"] = read-only preflight and plan; [] or ["run"] = do it.
// No literal inch-mark quotes: Q = (char)34; '~' in the data means an inch mark.
var inv = System.Globalization.CultureInfo.InvariantCulture;
string Q = ((char)34).ToString();
bool dry = false;
if (parameters != null)
    foreach (object po in parameters)
        if (po != null && po.ToString().Trim().Equals("dryrun", StringComparison.OrdinalIgnoreCase)) dry = true;
string TAIL0 = "\ndone=0 skipped=0 failed=0";
string VIEW_NAME = "FEWD FIXTURE LIST AND GI SIZING";
string SHEET_NO = "P0.10"; long SHEET_ID_REF = 12974000L;
string REV_DESC = "FEWD Submittal"; long REV_ID_REF = 15034302L;
string TN_NARROW = "3/32" + Q + " Arial Narrow"; long TN_NARROW_REF = 14085252L;
string TN_BOLD = "1/8" + Q + " Arial Bold"; long TN_BOLD_REF = 13105344L;
string LINE_STYLE = "Thin Lines";
string TAG_FAMILY = "MEP_Tag_Revision", TAG_TYPE = "Revision Tag";
double IN = 1.0 / 12.0;
double[] FREE = new double[] { 2.06, 0.15, 3.17, 1.30 };
double REV_X = 3.241, STICKY_Y = 1.54;
int GREASE_DFU = 56, GI_MAX_DFU = 172, GI_SPARE = 116;
int NOTES_EXPECTED = 517, LINES_EXPECTED = 77;
List<string> warn = new List<string>();

string[] HDR = new string[] { "F-#", "K1.1 ITEM", "QTY", "DESCRIPTION", "AREA", "WASTE CONNECTION", "DRAIN", "TO GI", "DFU" };
double[] COLW = new double[] { 0.45, 0.55, 0.35, 3.6, 1.1, 1.3, 0.6, 0.45, 0.4 };   // paper inches
string[] ROWS = new string[] {
"F-1|1|1|Mop sink cabinet unit (24x24x8)|Prep kitchen|Direct|2~|Yes|3",
"F-2|6|1|Walk-in cooler unit|Prep kitchen|Indirect to FS / FD|2~ / 2~|Yes|0",
"F-3|10|1|Coil unit, walk-in cooler|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-4|11|1|Coil drain line unit (NIC)|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-5|16|1|Coil unit, walk-in freezer|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-6|17|1|Coil drain line w/ heater (NIC)|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-7|23|4|Hand sink unit|Prep kitchen|Direct|2~|Yes|4",
"F-8|25/26|1|Food preparation sink, (2) compartments|Prep kitchen|Indirect to FS|2~|Yes|3",
"F-9|38|2|Utility sink (18x18x18)|Prep kitchen|Direct|2~|Yes|6",
"F-10|64|1|Combi oven unit|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-11|69|1|Salad top refrigerator unit|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-12|80|1|Eye wash station|Prep kitchen|Direct|2~|No|0",
"F-13|81|1|S/S beverage countertop|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-14|83|1|Drink dispenser unit (NIC)|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-15|84|1|Drink dispenser unit (NIC)|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-16|87|1|Coffee shuttle system (NIC)|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-17|89|1|Water filter unit (RO)|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-18|90|1|Utility sink w/ faucet, waste & trash chute (18x18x18)|Prep kitchen|Direct|2~|Yes|3",
"F-19|92|1|Iced tea dispenser & brewer (NIC)|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-20|95|1|S/S dishtable unit|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-21|97|1|Dump sink unit w/ 2~ drain outlet (18x18x18)|Prep kitchen|Direct|2~|Yes|3",
"F-22|98|1|Garbage disposal unit & solenoid|Prep kitchen|Indirect to FS|2~|Yes|3",
"F-23|99|1|Pre-rinse unit|Prep kitchen|Via #95 / #98|-|Yes|3",
"F-24|100|1|Undercounter dishwasher unit|Prep kitchen|Indirect to FS / FD|2~ / 3~|No|0",
"F-25|105/109|1|Pot sink, (3) compartments (24x24x14)|Prep kitchen|Indirect to FS|3~|Yes|3",
"F-26|114|1|Ice bin unit|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-27|115|1|Ice maker unit|Prep kitchen|Indirect to FS|2~|Yes|0",
"F-28|120|1|Undercounter Alto-Shaam unit|Prep kitchen|Indirect to FS|2~|Yes|1",
"F-29|133|4|Coffee shuttle & dispenser units|Breakfast / lounge|Indirect to FS|2~|Yes|4",
"F-30|217|1|Ice jockey box station|Cocktail bar|Indirect to FS|2~|Yes|1",
"F-31|224|1|Bar soda gun unit (NIC)|Cocktail bar|Indirect to FS|2~|Yes|1",
"F-32|226|1|Dump sink & drainboard station|Cocktail bar|Indirect to FS|2~|Yes|2",
"F-33|227|1|Bar hand sink unit|Cocktail bar|Direct|2~|Yes|1",
"F-34|230|1|3-compartment bar sink unit|Cocktail bar|Indirect to FS / FD|2~|Yes|2",
"F-35|232|3|Scupper rail unit (NIC)|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-36|235/236|1|2-door backbar refrigerator|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-37|237|2|2-head beer tower unit|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-38|238|1|Drip trough unit|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-39|244|1|Espresso cappuccino machine|Cocktail bar|Indirect to FS|2~|Yes|1",
"F-40|246|1|Backbar stone top w/ sub top (NIC)|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-41|247|2|Backbar cabinet unit (NIC)|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-42|256|4|Coke dispenser unit (NIC)|Cocktail bar|Indirect to FS|2~|Yes|4",
"F-43|262|1|Countertop freezer, glass door|Cocktail bar|Indirect to FS|2~|Yes|0",
"F-44|254|4|Ice maker dispenser units|Guest floors|Indirect to FS|2~|No|4",
"F-45|FS|1|Floor sink at dish area (#97, #98, #99)|Prep kitchen|Floor sink|2~|Yes|0",
"F-46|FS|1|Floor sink, cook line west (near #38 / #64)|Prep kitchen|Floor sink|2~|Yes|0",
"F-47|FS|1|Floor sink, cook line east (near #38)|Prep kitchen|Floor sink|2~|Yes|0",
"F-48|FS|1|Floor sink at beverage counter (#83, #84, #87, #89, #92)|Prep kitchen|Floor sink|2~|Yes|0",
"F-49|FS|1|Floor sink at food prep sink (#25)|Prep kitchen|Floor sink|2~|Yes|0",
"F-50|FS|1|Floor sink at ice bin / ice maker (#114, #115)|Prep kitchen|Floor sink|2~|Yes|0",
"F-51|FS|1|Floor sink at coffee station (#133)|Breakfast / lounge|Floor sink|2~|Yes|0",
"F-52|FS|1|Floor sink at buffet island|Breakfast / lounge|Floor sink|2~|Yes|0",
"F-53|FS|1|Floor sink at 3-comp bar sink (#230)|Cocktail bar|Floor sink|2~|Yes|0",
"F-54|FS|1|Floor sink at espresso (#244)|Cocktail bar|Floor sink|2~|Yes|0",
"F-55|FS|1|Floor sink at ice jockey / soda (#217, #224, #226)|Cocktail bar|Floor sink|2~|Yes|0",
"F-56|FD|1|Floor drain, cocktail bar|Cocktail bar|Floor drain|2~|Yes|0" };

// ---------------- 0. Data checks (no model access) ----------------
if (ROWS.Length != 56) return "NO_GO data has " + ROWS.Length + " rows, not 56" + TAIL0;
string[][] data = new string[56][];
int sumGrease = 0, sumAll = 0, nYes = 0;
for (int i = 0; i < 56; i++)
{
    string[] c = ROWS[i].Replace("~", Q).Split('|');
    if (c.Length != 9) return "NO_GO row " + (i + 1) + " has " + c.Length + " cells" + TAIL0;
    if (c[0] != "F-" + (i + 1)) return "NO_GO row " + (i + 1) + " is '" + c[0] + "'" + TAIL0;
    int d;
    if (!int.TryParse(c[8], System.Globalization.NumberStyles.Integer, inv, out d)) return "NO_GO " + c[0] + " DFU '" + c[8] + "' is not an integer" + TAIL0;
    sumAll += d;
    if (c[7] == "Yes") { sumGrease += d; nYes++; }
    else if (c[7] != "No") return "NO_GO " + c[0] + " TO GI '" + c[7] + "'" + TAIL0;
    data[i] = c;
}
if (data[11][7] != "No" || data[23][7] != "No" || data[43][7] != "No") return "NO_GO overrides F-12/F-24/F-44 TO GI not all No" + TAIL0;
if (sumGrease != GREASE_DFU) return "NO_GO DFU sum over TO GI = Yes is " + sumGrease + ", not " + GREASE_DFU + TAIL0;
if (GI_MAX_DFU - sumGrease != GI_SPARE) return "NO_GO spare " + (GI_MAX_DFU - sumGrease) + " != " + GI_SPARE + TAIL0;
string TOTAL_TEXT = "TOTAL DFU TO GREASE INTERCEPTOR: " + sumGrease;
string[] GI_LINES = new string[] {
    "GREASE INTERCEPTOR SIZING",
    "TOTAL GREASE WASTE DFU: " + sumGrease,
    "CODE MINIMUM SIZE: 1,250 GAL",
    "PROPOSED: 1,500 GAL GRAVITY GREASE INTERCEPTOR (GGI)",
    "MAXIMUM DFU FOR 1,500 GAL: " + GI_MAX_DFU + "; SPARE: " + (GI_MAX_DFU - sumGrease) + " DFU",
    "FEWD: (3) MANHOLES + SAMPLE BOX (4 TOTAL); 5'-0" + Q + " MIN. CLEARANCE TO OPEN MANHOLES; LOCATION PER COUNTY DEH.",
    "SEE CIVIL DRAWINGS FOR GI DETAIL." };
string GI_TEXT = string.Join("\r", GI_LINES);
string STAMP_TEXT = "FEWD APPROVAL STAMP (DSD 00131)";

// ---------------- 1. Preflight (read-only) ----------------
string title = document.Title ?? "";
if (title.IndexOf("Block 6_Plumbing", StringComparison.OrdinalIgnoreCase) < 0) return "NO_GO wrong document open: " + title + TAIL0;

List<ViewSheet> shs = new List<ViewSheet>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(ViewSheet)))
{
    ViewSheet s = e as ViewSheet;
    if (s != null && !s.IsTemplate && s.SheetNumber == SHEET_NO) shs.Add(s);
}
if (shs.Count != 1) return "NO_GO " + shs.Count + " sheets numbered " + SHEET_NO + TAIL0;
ViewSheet sheet = shs[0];
if (sheet.Id.Value != SHEET_ID_REF) warn.Add("sheet id " + sheet.Id.Value + " (task said " + SHEET_ID_REF + ")");
if (sheet.GetCategoryHidden(new ElementId(BuiltInCategory.OST_RevisionClouds))) return "NO_GO revision clouds hidden on " + SHEET_NO + TAIL0;
if (sheet.GetCategoryHidden(new ElementId(BuiltInCategory.OST_RevisionCloudTags))) return "NO_GO revision cloud tags hidden on " + SHEET_NO + TAIL0;

Revision rev = null; int revHits = 0;
foreach (ElementId rid in Revision.GetAllRevisionIds(document))
{
    Revision r = document.GetElement(rid) as Revision;
    if (r != null && (r.Description ?? "").Trim() == REV_DESC) { rev = r; revHits++; }
}
if (revHits != 1) return "NO_GO " + revHits + " revisions have Description '" + REV_DESC + "'" + TAIL0;
if (rev.Issued) return "NO_GO revision '" + REV_DESC + "' (" + rev.Id.Value + ") is issued" + TAIL0;
if (rev.Visibility == RevisionVisibility.Hidden) return "NO_GO revision '" + REV_DESC + "' Visibility=Hidden" + TAIL0;
if (rev.Id.Value != REV_ID_REF) warn.Add("revision id " + rev.Id.Value + " (task said " + REV_ID_REF + ")");

TextNoteType tNarrow = null, tBold = null;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(TextNoteType)))
{
    if (e.Name == TN_NARROW && tNarrow == null) tNarrow = e as TextNoteType;
    if (e.Name == TN_BOLD && tBold == null) tBold = e as TextNoteType;
}
if (tNarrow == null) return "NO_GO text type '" + T11Esc(TN_NARROW) + "' not found" + TAIL0;
if (tBold == null) return "NO_GO text type '" + T11Esc(TN_BOLD) + "' not found" + TAIL0;
if (tNarrow.Id.Value != TN_NARROW_REF) warn.Add("narrow type id " + tNarrow.Id.Value + " (task " + TN_NARROW_REF + ")");
if (tBold.Id.Value != TN_BOLD_REF) warn.Add("bold type id " + tBold.Id.Value + " (task " + TN_BOLD_REF + ")");
Parameter pSz = tNarrow.get_Parameter(BuiltInParameter.TEXT_SIZE);
double tSize = (pSz == null || pSz.AsDouble() <= 0) ? (0.09375 / 12.0) : pSz.AsDouble();
Parameter pWf = tNarrow.get_Parameter(BuiltInParameter.TEXT_WIDTH_SCALE);
double tWf = (pWf == null || pWf.AsDouble() <= 0) ? 1.0 : pWf.AsDouble();
double minW = TextElement.GetMinimumAllowedWidth(document, tNarrow.Id), maxW = TextElement.GetMaximumAllowedWidth(document, tNarrow.Id);
double minWb = TextElement.GetMinimumAllowedWidth(document, tBold.Id), maxWb = TextElement.GetMaximumAllowedWidth(document, tBold.Id);

Category linesCat = Category.GetCategory(document, BuiltInCategory.OST_Lines);
GraphicsStyle thin = null;
if (linesCat != null)
    foreach (Category sc in linesCat.SubCategories)
        if (sc != null && (sc.Name == LINE_STYLE || sc.Name == "<" + LINE_STYLE + ">")) { thin = sc.GetGraphicsStyle(GraphicsStyleType.Projection); break; }
if (thin == null) return "NO_GO line style '" + LINE_STYLE + "' not found under Lines" + TAIL0;

List<ViewFamilyType> vfts = new List<ViewFamilyType>();
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(ViewFamilyType)))
{
    ViewFamilyType v = e as ViewFamilyType;
    if (v != null && v.ViewFamily == ViewFamily.Drafting) vfts.Add(v);
}
if (vfts.Count == 0) return "NO_GO no drafting ViewFamilyType" + TAIL0;
vfts.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));
ViewFamilyType vft = vfts[0];
foreach (ViewFamilyType v in vfts) if (v.Name == "Drafting View") { vft = v; break; }

List<FamilySymbol> tagSyms = new List<FamilySymbol>();
FamilySymbol tagSym = null;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_RevisionCloudTags))
{
    FamilySymbol fs = e as FamilySymbol;
    if (fs == null) continue;
    string fam = fs.Family == null ? "" : fs.Family.Name;
    if (fam == TAG_FAMILY && fs.Name == TAG_TYPE) { tagSym = fs; break; }
    if (fam == TAG_TYPE || fs.Name == TAG_TYPE) tagSyms.Add(fs);
}
if (tagSym == null && tagSyms.Count > 0) { tagSyms.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name)); tagSym = tagSyms[0]; warn.Add("tag " + TAG_FAMILY + ":" + TAG_TYPE + " not found, using " + tagSym.Id.Value); }
if (tagSym == null) return "NO_GO no Revision Cloud Tag '" + TAG_FAMILY + ":" + TAG_TYPE + "'" + TAIL0;

// viewport type without title: any ElementType with VIEWPORT_ATTR_SHOW_LABEL = 0, prefer a name with "no title"
ElementType vpNoTitle = null; int vpTypeCount = 0;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(ElementType)))
{
    Parameter ps = e.get_Parameter(BuiltInParameter.VIEWPORT_ATTR_SHOW_LABEL);
    if (ps == null) continue;
    vpTypeCount++;
    if (ps.AsInteger() != 0) continue;
    bool named = e.Name.IndexOf("no title", StringComparison.OrdinalIgnoreCase) >= 0;
    if (vpNoTitle == null || (named && vpNoTitle.Name.IndexOf("no title", StringComparison.OrdinalIgnoreCase) < 0)) vpNoTitle = e as ElementType;
}
if (vpNoTitle == null) warn.Add("no viewport type without title (" + vpTypeCount + " types); default type, label checked");

// ---------------- 2. Idempotency ----------------
View exView = null; int exCount = 0;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(View)))
{
    View v = e as View;
    if (v != null && !v.IsTemplate && v.Name == VIEW_NAME) { exView = v; exCount++; }
}
if (exCount > 1) return "NO_GO " + exCount + " views named '" + VIEW_NAME + "'" + TAIL0;
if (exView != null)
{
    Viewport exVp = null;
    foreach (ElementId vid in sheet.GetAllViewports())
    {
        Viewport v = document.GetElement(vid) as Viewport;
        if (v != null && v.ViewId.Value == exView.Id.Value) exVp = v;
    }
    int exNotes = new FilteredElementCollector(document, exView.Id).OfClass(typeof(TextNote)).GetElementCount();
    RevisionCloud exC = null; IndependentTag exT = null;
    if (exVp != null)
    {
        double[] ob = T11OBox(exVp.GetBoxOutline());
        foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(RevisionCloud)))
        {
            RevisionCloud rc = e as RevisionCloud;
            if (rc == null || rc.RevisionId.Value != rev.Id.Value || rc.OwnerViewId.Value != sheet.Id.Value) continue;
            double[] cb = T11CloudBox(rc);
            if (cb != null && T11Inside(ob, cb)) { exC = rc; break; }
        }
        if (exC != null)
            foreach (Element e in new FilteredElementCollector(document, sheet.Id).OfClass(typeof(IndependentTag)))
            {
                IndependentTag it = e as IndependentTag;
                try { if (it != null && it.GetTaggedLocalElementIds().Contains(exC.Id)) { exT = it; break; } } catch (Exception) { }
            }
    }
    if (exVp != null && exC != null && exT != null && exNotes == NOTES_EXPECTED)
        return "{\"mode\":\"" + (dry ? "dryrun" : "run") + "\",\"status\":\"skipped: already in target state\",\"view\":" + exView.Id.Value + ",\"viewport\":" + exVp.Id.Value
            + ",\"outline\":" + T11Box(T11OBox(exVp.GetBoxOutline())) + ",\"cloud\":" + exC.Id.Value + ",\"tag\":" + exT.Id.Value + ",\"notes\":" + exNotes + "}"
            + "\ndone=0 skipped=4 failed=0 skipped_ids=" + exView.Id.Value + "," + exVp.Id.Value + "," + exC.Id.Value + "," + exT.Id.Value;
    return "NO_GO partial state: view " + exView.Id.Value + " exists (viewport on " + SHEET_NO + "=" + (exVp == null ? "none" : exVp.Id.Value.ToString())
        + ", cloud=" + (exC == null ? "none" : exC.Id.Value.ToString()) + ", tag=" + (exT == null ? "none" : exT.Id.Value.ToString())
        + ", notes=" + exNotes + "/" + NOTES_EXPECTED + "); run CB6-R1-T11.cleanup.cs with these ids first" + TAIL0;
}

// ---------------- 3. Layout plan (view coords, ft; scale 1 so paper = model) ----------------
int NC = 9;
double[] colX = new double[NC + 1];
colX[0] = 0;
for (int c = 0; c < NC; c++) colX[c + 1] = colX[c] + COLW[c] * IN;
double TW = colX[NC];
double ROWH = 0.17 * IN, PADX = 0.04 * IN, PADY = 0.03 * IN;
double tableTop = -0.25 * IN;
int NROWS = 58;                                   // header + 56 + total
double totalTop = tableTop - 57 * ROWH, tableBot = tableTop - NROWS * ROWH;
double giTop = tableBot - 0.30 * IN, giW = 4.3 * IN, giPad = 0.08 * IN;
double stX = 4.6 * IN, stW = 3.0 * IN, stH = 2.0 * IN;
double giWrap = T11Clamp(giW - 2 * giPad, minW, maxW);
double estChar = tSize * tWf * 0.80;
int cpl = Math.Max(10, (int)Math.Floor(giWrap / estChar));
int estLines = 0;
foreach (string l in GI_LINES) estLines += Math.Max(1, (int)Math.Ceiling(l.Length / (double)cpl));
double estGiH = estLines * tSize * 1.7;
double estGiBot = giTop - giPad - estGiH - giPad;
double estBot = Math.Min(estGiBot, giTop - stH);
double estW = TW, estH = -estBot;
double fcx = (FREE[0] + FREE[2]) / 2.0, fcy = (FREE[1] + FREE[3]) / 2.0;
double[] planOut = new double[] { fcx - estW / 2, fcy - estH / 2, fcx + estW / 2, fcy + estH / 2 };
double[] tagOff0 = new double[] { -0.12 * IN, -0.12 * IN, 0.12 * IN, 0.12 * IN };
double[] planL = T11Layout(planOut, tagOff0);

List<double[]> obs = new List<double[]>(); List<string> obsLbl = new List<string>(); List<string> obsSkip = new List<string>();
T11Obs(document, sheet, new HashSet<long>(), obs, obsLbl, obsSkip);
List<string> planProb = T11Check(planOut, planL, null, FREE, REV_X, STICKY_Y, obs, obsLbl);

System.Text.StringBuilder rep = new System.Text.StringBuilder();
rep.Append("{\"mode\":\"").Append(dry ? "dryrun" : "run").Append("\",\"sheet\":").Append(sheet.Id.Value).Append(",\"rev\":").Append(rev.Id.Value);
rep.Append(",\"types\":{\"narrow\":").Append(tNarrow.Id.Value).Append(",\"bold\":").Append(tBold.Id.Value).Append(",\"thin_lines\":").Append(thin.Id.Value)
   .Append(",\"vft\":").Append(vft.Id.Value).Append(",\"tag\":").Append(tagSym.Id.Value).Append(",\"vp_no_title\":").Append(vpNoTitle == null ? "null" : vpNoTitle.Id.Value.ToString()).Append("}");
rep.Append(",\"dfu_grease\":").Append(sumGrease).Append(",\"dfu_column\":").Append(sumAll).Append(",\"yes_rows\":").Append(nYes);
rep.Append(",\"text_size_in\":").Append(T11F(tSize * 12)).Append(",\"est_paper_in\":[").Append(T11F(estW * 12)).Append(",").Append(T11F(estH * 12)).Append("]");
rep.Append(",\"plan_outline\":").Append(T11Box(planOut)).Append(",\"plan_cloud\":").Append(T11Box(new double[] { planL[0], planL[1], planL[2], planL[3] }));
rep.Append(",\"plan_tag_head\":[").Append(T11F(planL[4])).Append(",").Append(T11F(planL[5])).Append("]");
rep.Append(",\"obstacles\":").Append(obs.Count).Append(",\"obs_skipped\":\"").Append(T11Esc(string.Join("; ", obsSkip.Take(4)))).Append("\"");
rep.Append(",\"would_create\":\"1 drafting view, ").Append(NOTES_EXPECTED).Append(" text notes, ").Append(LINES_EXPECTED).Append(" detail lines, 1 viewport, 1 cloud, 1 tag\"");
if (warn.Count > 0) rep.Append(",\"warn\":\"").Append(T11Esc(string.Join("; ", warn))).Append("\"");
if (planProb.Count > 0)
{
    rep.Append(",\"status\":\"NO_GO plan: ").Append(T11Esc(string.Join("; ", planProb.Take(8)))).Append("\"}");
    return "NO_GO " + rep.ToString() + TAIL0;
}
if (dry)
{
    rep.Append(",\"status\":\"GO\"}");
    return rep.ToString() + "\ndone=0 skipped=0 failed=0 (dryrun, nothing changed)";
}

// ---------------- 4. Create (SubTransaction: all or nothing) ----------------
string item = "view";
int nNotes = 0, nLines = 0;
long viewIdOut = 0, vpIdOut = 0, cloudIdOut = 0, tagIdOut = 0;
SubTransaction st = new SubTransaction(document);
st.Start();
try
{
    ViewDrafting view = ViewDrafting.Create(document, vft.Id);
    if (view.ViewTemplateId != ElementId.InvalidElementId) view.ViewTemplateId = ElementId.InvalidElementId;
    view.Name = VIEW_NAME;
    view.Scale = 1;
    viewIdOut = view.Id.Value;
    if (view.GetCategoryHidden(new ElementId(BuiltInCategory.OST_TextNotes))) throw new Exception("text notes hidden in the new view");
    if (view.GetCategoryHidden(new ElementId(BuiltInCategory.OST_Lines))) throw new Exception("lines hidden in the new view");

    item = "text";
    TextNoteOptions oN = new TextNoteOptions(tNarrow.Id);
    oN.HorizontalAlignment = HorizontalTextAlignment.Left; oN.VerticalAlignment = VerticalTextAlignment.Top;
    TextNoteOptions oB = new TextNoteOptions(tBold.Id);
    oB.HorizontalAlignment = HorizontalTextAlignment.Left; oB.VerticalAlignment = VerticalTextAlignment.Top;
    List<TextNote> allNotes = new List<TextNote>();
    List<string> allText = new List<string>();
    TextNote tTitle = TextNote.Create(document, view.Id, new XYZ(0, 0, 0), T11Clamp(TW, minWb, maxWb), "FEWD FIXTURE LIST", oB);
    allNotes.Add(tTitle); allText.Add("FEWD FIXTURE LIST");
    TextNote[,] cells = new TextNote[57, NC];
    for (int r = 0; r < 57; r++)
    {
        double rt = tableTop - r * ROWH;
        for (int c = 0; c < NC; c++)
        {
            string s = (r == 0) ? HDR[c] : data[r - 1][c];
            TextNote tn = TextNote.Create(document, view.Id, new XYZ(colX[c] + PADX, rt - PADY, 0), T11Clamp(COLW[c] * IN - 2 * PADX, minW, maxW), s, oN);
            cells[r, c] = tn; allNotes.Add(tn); allText.Add(s);
        }
    }
    TextNote tTotal = TextNote.Create(document, view.Id, new XYZ(PADX, totalTop - PADY, 0), T11Clamp(TW - 2 * PADX, minW, maxW), TOTAL_TEXT, oN);
    allNotes.Add(tTotal); allText.Add(TOTAL_TEXT);
    TextNote tGi = TextNote.Create(document, view.Id, new XYZ(giPad, giTop - giPad, 0), giWrap, GI_TEXT, oN);
    allNotes.Add(tGi); allText.Add(GI_TEXT);
    TextNote tSt = TextNote.Create(document, view.Id, new XYZ(stX + giPad, giTop - giPad, 0), T11Clamp(stW - 2 * giPad, minW, maxW), STAMP_TEXT, oN);
    allNotes.Add(tSt); allText.Add(STAMP_TEXT);
    document.Regenerate();

    item = "lines";
    List<double[]> segs = new List<double[]>();
    for (int k = 0; k <= NROWS; k++) segs.Add(new double[] { 0, tableTop - k * ROWH, TW, tableTop - k * ROWH });
    segs.Add(new double[] { 0, tableTop, 0, tableBot });
    segs.Add(new double[] { TW, tableTop, TW, tableBot });
    for (int c = 1; c < NC; c++) segs.Add(new double[] { colX[c], tableTop, colX[c], totalTop });
    double giH = tGi.Height;
    if (!(giH > 0.3 * estGiH && giH < 3.0 * estGiH)) { warn.Add("GI note height " + T11F(giH * 12) + " in implausible, using estimate"); giH = estGiH; }
    double giBot = giTop - giPad - giH - giPad;
    segs.Add(new double[] { 0, giTop, giW, giTop }); segs.Add(new double[] { giW, giTop, giW, giBot });
    segs.Add(new double[] { giW, giBot, 0, giBot }); segs.Add(new double[] { 0, giBot, 0, giTop });
    double stBot = giTop - stH;
    segs.Add(new double[] { stX, giTop, stX + stW, giTop }); segs.Add(new double[] { stX + stW, giTop, stX + stW, stBot });
    segs.Add(new double[] { stX + stW, stBot, stX, stBot }); segs.Add(new double[] { stX, stBot, stX, giTop });
    foreach (double[] sg in segs)
    {
        DetailCurve dc = document.Create.NewDetailCurve(view, Line.CreateBound(new XYZ(sg[0], sg[1], 0), new XYZ(sg[2], sg[3], 0)));
        dc.LineStyle = thin;
    }
    document.Regenerate();

    // R30 read-back after Regenerate
    item = "readback";
    nNotes = new FilteredElementCollector(document, view.Id).OfClass(typeof(TextNote)).GetElementCount();
    int nCurves = 0, nThin = 0;
    foreach (Element e in new FilteredElementCollector(document, view.Id).OfClass(typeof(CurveElement)))
    {
        DetailCurve dc = e as DetailCurve;
        if (dc == null) continue;
        nCurves++;
        if (dc.LineStyle != null && dc.LineStyle.Id.Value == thin.Id.Value) nThin++;
    }
    nLines = nCurves;
    if (nNotes != NOTES_EXPECTED) throw new Exception("view has " + nNotes + " text notes, expected " + NOTES_EXPECTED);
    if (nCurves != LINES_EXPECTED || nThin != LINES_EXPECTED) throw new Exception("view has " + nCurves + " detail lines (" + nThin + " Thin Lines), expected " + LINES_EXPECTED);
    for (int i = 0; i < allNotes.Count; i++)
        if (T11Norm(allNotes[i].Text) != T11Norm(allText[i])) throw new Exception("text read back differs: '" + T11Esc(T11Norm(allNotes[i].Text)) + "'");
    int rbGrease = 0, rbYes = 0;
    for (int r = 1; r < 57; r++)
    {
        string g = T11Norm(cells[r, 7].Text);
        int dv = int.Parse(T11Norm(cells[r, 8].Text), inv);
        if (g == "YES") { rbGrease += dv; rbYes++; }
    }
    if (rbGrease != GREASE_DFU) throw new Exception("read-back DFU sum " + rbGrease + " != " + GREASE_DFU);
    // single-line check: every cell must stay on one line (height <= 1.5 x the smallest cell height)
    double hMin = double.MaxValue, hMax = 0; string hMaxCell = "";
    for (int r = 0; r < 57; r++) for (int c = 0; c < NC; c++) { double h = cells[r, c].Height; if (h > 0 && h < hMin) hMin = h; }
    for (int r = 0; r < 57; r++) for (int c = 0; c < NC; c++)
    {
        double h = cells[r, c].Height;
        if (h > hMax) { hMax = h; hMaxCell = (r == 0 ? "hdr" : data[r - 1][0]) + "/" + HDR[c]; }
    }
    if (hMin < double.MaxValue && hMax > 1.5 * hMin) throw new Exception("cell " + hMaxCell + " wraps (height " + T11F(hMax * 12) + " in vs " + T11F(hMin * 12) + " in)");
    if (hMin < double.MaxValue && hMin > ROWH - PADY) warn.Add("cell text height " + T11F(hMin * 12) + " in exceeds row space");
    if (tTotal.Height > 1.5 * hMin) throw new Exception("total row wraps");
    if (tSt.Height > 1.5 * hMin) throw new Exception("stamp label wraps");

    item = "viewport";
    if (!Viewport.CanAddViewToSheet(document, sheet.Id, view.Id)) throw new Exception("CanAddViewToSheet false");
    Viewport vp = Viewport.Create(document, sheet.Id, view.Id, new XYZ(fcx, fcy, 0));
    vpIdOut = vp.Id.Value;
    if (vpNoTitle != null)
    {
        if (vp.GetValidTypes().Contains(vpNoTitle.Id)) vp.ChangeTypeId(vpNoTitle.Id);
        else warn.Add("viewport type " + vpNoTitle.Id.Value + " not valid for this viewport");
    }
    document.Regenerate();
    double[] o1 = T11OBox(vp.GetBoxOutline());
    XYZ bc = vp.GetBoxCenter();
    vp.SetBoxCenter(new XYZ(bc.X + (fcx - (o1[0] + o1[2]) / 2.0), bc.Y + (fcy - (o1[1] + o1[3]) / 2.0), bc.Z));
    document.Regenerate();
    double[] outl = T11OBox(vp.GetBoxOutline());
    double[] lab = null;
    Outline lo = vp.GetLabelOutline();
    if (lo != null && !lo.IsEmpty) { double[] lb = T11OBox(lo); if (lb[2] - lb[0] > 1e-4 && lb[3] - lb[1] > 1e-4) lab = lb; }
    rep.Append(",\"view\":").Append(viewIdOut).Append(",\"viewport\":").Append(vpIdOut).Append(",\"vp_type\":").Append(vp.GetTypeId().Value);
    rep.Append(",\"outline\":").Append(T11Box(outl)).Append(",\"outline_paper_in\":[").Append(T11F((outl[2] - outl[0]) * 12)).Append(",").Append(T11F((outl[3] - outl[1]) * 12)).Append("]");
    rep.Append(",\"label\":").Append(lab == null ? "null" : T11Box(lab));
    rep.Append(",\"notes\":").Append(nNotes).Append(",\"cells\":").Append(57 * NC).Append(",\"lines\":").Append(nLines);
    rep.Append(",\"dfu_readback\":").Append(rbGrease).Append(",\"yes_readback\":").Append(rbYes).Append(",\"cell_h_in\":[").Append(T11F(hMin * 12)).Append(",").Append(T11F(hMax * 12)).Append("]");
    rep.Append(",\"gi_box_in\":[").Append(T11F(giW * 12)).Append(",").Append(T11F((giTop - giBot) * 12)).Append("]");

    item = "fit";
    double[] L0 = T11Layout(outl, tagOff0);
    List<string> p0 = T11Check(outl, L0, lab, FREE, REV_X, STICKY_Y, obs, obsLbl);
    if (p0.Count > 0) throw new Exception("viewport fit: " + string.Join("; ", p0.Take(6)));

    item = "cloud";
    double[] rect = new double[] { L0[0], L0[1], L0[2], L0[3] };
    XYZ bl = new XYZ(rect[0], rect[1], 0), tl = new XYZ(rect[0], rect[3], 0), tr = new XYZ(rect[2], rect[3], 0), br = new XYZ(rect[2], rect[1], 0);
    List<Curve> curves = new List<Curve>();
    curves.Add(Line.CreateBound(bl, tl));   // CLOCKWISE: BL > TL > TR > BR > BL
    curves.Add(Line.CreateBound(tl, tr));
    curves.Add(Line.CreateBound(tr, br));
    curves.Add(Line.CreateBound(br, bl));
    RevisionCloud cloud = RevisionCloud.Create(document, sheet, rev.Id, curves);
    document.Regenerate();
    cloudIdOut = cloud.Id.Value;
    double area = 0;
    foreach (Curve cv in cloud.GetSketchCurves()) { XYZ a0 = cv.GetEndPoint(0), a1 = cv.GetEndPoint(1); area += a0.X * a1.Y - a1.X * a0.Y; }
    area /= 2.0;
    if (area >= 0) throw new Exception("cloud signed area " + T11F(area) + " is not negative");
    double[] rb = T11CloudBox(cloud);
    if (rb == null || Math.Abs(rb[0] - rect[0]) > 0.005 || Math.Abs(rb[3] - rect[3]) > 0.005) throw new Exception("cloud read back " + (rb == null ? "null" : T11Box(rb)));
    if (!cloud.GetSheetIds().Contains(sheet.Id)) throw new Exception("cloud GetSheetIds lacks the sheet");
    rep.Append(",\"cloud\":").Append(cloudIdOut).Append(",\"cloud_rect\":").Append(T11Box(rect)).Append(",\"signed_area\":").Append(T11F(area * 1000)).Append("e-3");

    item = "tag";
    if (!tagSym.IsActive) tagSym.Activate();
    XYZ head = new XYZ(L0[4], L0[5], 0);
    IndependentTag tag = IndependentTag.Create(document, tagSym.Id, sheet.Id, new Reference(cloud), false, TagOrientation.Horizontal, head);
    tag.TagHeadPosition = head;
    document.Regenerate();
    tagIdOut = tag.Id.Value;
    double clr = 0.08 * IN, step = 0.02 * IN, extra = 0;
    double[] tbx = T11TagBox(tag, sheet, tagOff0);
    if (tbx[1] < rect[3] + clr)
    {
        extra = Math.Ceiling((rect[3] + clr - tbx[1]) / step) * step;
        tag.TagHeadPosition = new XYZ(head.X, head.Y + extra, 0);
        document.Regenerate();
        tbx = T11TagBox(tag, sheet, tagOff0);
    }
    XYZ th = tag.TagHeadPosition;
    if (tbx[1] < rect[3] + clr - 0.001) throw new Exception("tag box bottom " + T11F(tbx[1]) + " still within 0.08 in of cloud top " + T11F(rect[3]));
    if (Math.Abs(th.X - (rect[2] - 0.17 * IN)) > 0.002) throw new Exception("tag head x " + T11F(th.X) + " not at cloud right - 0.17 in");
    List<string> p1 = T11Check(outl, L0, lab, FREE, REV_X, STICKY_Y, obs, obsLbl);
    if (tbx[3] >= STICKY_Y) p1.Add("tag top " + T11F(tbx[3]) + " >= " + STICKY_Y);
    if (tbx[2] >= REV_X) p1.Add("tag right " + T11F(tbx[2]) + " >= " + REV_X);
    for (int i = 0; i < obs.Count; i++) if (T11Ov(tbx, obs[i], 0)) p1.Add("tag x " + obsLbl[i]);
    if (p1.Count > 0) throw new Exception("tag fit: " + string.Join("; ", p1.Take(6)));
    rep.Append(",\"tag\":").Append(tagIdOut).Append(",\"tag_head\":[").Append(T11F(th.X)).Append(",").Append(T11F(th.Y)).Append("],\"tag_box\":").Append(T11Box(tbx)).Append(",\"tag_raise_in\":").Append(T11F(extra * 12));

    item = "revision schedule";
    document.Regenerate();
    IList<ElementId> shRevs = sheet.GetAllRevisionIds();
    if (!shRevs.Contains(rev.Id)) throw new Exception("sheet GetAllRevisionIds lacks " + rev.Id.Value);
    rep.Append(",\"sheet_revs\":[").Append(string.Join(",", shRevs.Select(x => x.Value.ToString()))).Append("]");

    st.Commit();
}
catch (Exception ex)
{
    try { st.RollBack(); } catch (Exception) { }
    return "ROLLED_BACK " + item + ": " + T11Esc(ex.Message) + "\ndone=0 skipped=0 failed=1 (nothing created)";
}
if (warn.Count > 0) rep.Append(",\"warn2\":\"").Append(T11Esc(string.Join("; ", warn.Take(6)))).Append("\"");
rep.Append(",\"status\":\"OK\"}");
rep.Append("\ndone=").Append(4 + nNotes + nLines).Append(" skipped=0 failed=0 created: view=").Append(viewIdOut).Append(" viewport=").Append(vpIdOut)
   .Append(" cloud=").Append(cloudIdOut).Append(" tag=").Append(tagIdOut).Append(" (+").Append(nNotes).Append(" text notes, ").Append(nLines).Append(" detail lines in the view)");
rep.Append("\ncleanup_params=[\"").Append(viewIdOut).Append("\",\"").Append(vpIdOut).Append("\",\"").Append(cloudIdOut).Append("\",\"").Append(tagIdOut).Append("\"]");
return rep.ToString();
}

// ---------------- helpers (CB6-R1-T11) ----------------
static string T11F(double v) { return Math.Round(v, 4).ToString(System.Globalization.CultureInfo.InvariantCulture); }
static string T11Box(double[] b) { return "[" + T11F(b[0]) + "," + T11F(b[1]) + "," + T11F(b[2]) + "," + T11F(b[3]) + "]"; }
static string T11Esc(string s) { return (s ?? "").Replace("\\", "\\\\").Replace(((char)34).ToString(), "\\" + ((char)34).ToString()).Replace("\r", " ").Replace("\n", " "); }
static string T11Norm(string s)
{
    string t = (s ?? "").Replace("\r\n", "\n").Replace("\r", "\n").Replace("\v", "\n").Trim();
    return t.ToUpperInvariant();
}
static double T11Clamp(double w, double lo, double hi) { if (w < lo) w = lo; if (w > hi) w = hi; return w; }
static bool T11Ov(double[] a, double[] b, double g) { return a[0] - g < b[2] && b[0] < a[2] + g && a[1] - g < b[3] && b[1] < a[3] + g; }
static bool T11Inside(double[] inner, double[] outer) { return inner[0] >= outer[0] - 1e-6 && inner[1] >= outer[1] - 1e-6 && inner[2] <= outer[2] + 1e-6 && inner[3] <= outer[3] + 1e-6; }
static double[] T11Grow(double[] a, double m) { return new double[] { a[0] - m, a[1] - m, a[2] + m, a[3] + m }; }
static double[] T11OBox(Outline o) { XYZ a = o.MinimumPoint, b = o.MaximumPoint; return new double[] { Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Max(a.X, b.X), Math.Max(a.Y, b.Y) }; }
static double[] T11U(double[] a, double x, double y) { return new double[] { Math.Min(a[0], x), Math.Min(a[1], y), Math.Max(a[2], x), Math.Max(a[3], y) }; }
static double[] T11CloudBox(RevisionCloud rc)
{
    double[] r = null;
    foreach (Curve cv in rc.GetSketchCurves())
        for (int k = 0; k < 2; k++) { XYZ p = cv.GetEndPoint(k); r = (r == null) ? new double[] { p.X, p.Y, p.X, p.Y } : T11U(r, p.X, p.Y); }
    return r;
}
// text box on a scale-1 view (sheet): from Coord, Width, Height and alignment
static double[] T11TextBox(TextNote tn)
{
    XYZ c = tn.Coord, b = tn.BaseDirection, u = tn.UpDirection;
    double w = tn.Width, h = tn.Height;
    double a0 = 0, a1 = 1, b0 = -1, b1 = 0;
    if (tn.HorizontalAlignment == HorizontalTextAlignment.Right) { a0 = -1; a1 = 0; }
    else if (tn.HorizontalAlignment == HorizontalTextAlignment.Center) { a0 = -0.5; a1 = 0.5; }
    if (tn.VerticalAlignment == VerticalTextAlignment.Bottom) { b0 = 0; b1 = 1; }
    else if (tn.VerticalAlignment == VerticalTextAlignment.Middle) { b0 = -0.5; b1 = 0.5; }
    double[] r = new double[] { double.MaxValue, double.MaxValue, double.MinValue, double.MinValue };
    foreach (double ka in new double[] { a0, a1 }) foreach (double kb in new double[] { b0, b1 })
        r = T11U(r, c.X + b.X * ka * w + u.X * kb * h, c.Y + b.Y * ka * w + u.Y * kb * h);
    return r;
}
// live sheet items (title block and sheet-size boxes skipped)
static void T11Obs(Document doc, ViewSheet sh, HashSet<long> excl, List<double[]> boxes, List<string> lbls, List<string> skipped)
{
    foreach (Element e in new FilteredElementCollector(doc, sh.Id).WhereElementIsNotElementType())
    {
        if (excl.Contains(e.Id.Value)) continue;
        try
        {
            if (e.Category != null && e.Category.BuiltInCategory == BuiltInCategory.OST_TitleBlocks) continue;
            double[] b = null; string l = (e.Category == null ? e.GetType().Name : e.Category.Name) + " " + e.Id.Value;
            if (e is Viewport)
            {
                Viewport v = (Viewport)e;
                b = T11OBox(v.GetBoxOutline());
                Outline lo = v.GetLabelOutline();
                if (lo != null && !lo.IsEmpty) { double[] lb = T11OBox(lo); if (lb[2] - lb[0] > 1e-4 && lb[3] - lb[1] > 1e-4) { boxes.Add(lb); lbls.Add("label of " + l); } }
            }
            else if (e is TextNote) b = T11TextBox((TextNote)e);
            else if (e is RevisionCloud) { b = T11CloudBox((RevisionCloud)e); if (b != null) b = T11Grow(b, 0.05 / 12.0); }
            else
            {
                BoundingBoxXYZ bb = e.get_BoundingBox(sh);
                if (bb == null) continue;
                b = new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
            }
            if (b == null) continue;
            if (b[2] - b[0] > 2.8 && b[3] - b[1] > 2.0) { skipped.Add(l + " (sheet-size)"); continue; }
            boxes.Add(b); lbls.Add(l + " " + T11Box(b));
        }
        catch (Exception) { }
    }
}
// [0..3] cloud rect (outline + 1/4 in), [4,5] tag head (right - 0.17 in, top + 0.08 in)
static double[] T11Layout(double[] o, double[] tagOff)
{
    double IN = 1.0 / 12.0;
    double[] r = T11Grow(o, 0.25 * IN);
    return new double[] { r[0], r[1], r[2], r[3], r[2] - 0.17 * IN, r[3] + 0.08 * IN };
}
static List<string> T11Check(double[] outl, double[] L, double[] lab, double[] free, double revX, double stickyY, List<double[]> obs, List<string> lbl)
{
    double IN = 1.0 / 12.0, bulge = 0.05 * IN;
    List<string> p = new List<string>();
    if (!T11Inside(outl, free)) p.Add("outline " + T11Box(outl) + " not inside free area " + T11Box(free));
    double[] cr = T11Grow(new double[] { L[0], L[1], L[2], L[3] }, bulge);
    if (cr[3] >= stickyY) p.Add("cloud top " + T11F(cr[3]) + " >= " + stickyY);
    if (cr[2] >= revX) p.Add("cloud right " + T11F(cr[2]) + " >= " + revX);
    if (cr[1] <= 0.05) p.Add("cloud bottom " + T11F(cr[1]) + " <= 0.05");
    double[] tg = new double[] { L[4] - 0.12 * IN, L[5], L[4] + 0.12 * IN, L[5] + 0.3 * IN };
    if (tg[3] >= stickyY) p.Add("tag est top " + T11F(tg[3]) + " >= " + stickyY);
    for (int i = 0; i < obs.Count; i++)
    {
        if (T11Ov(cr, obs[i], 0)) p.Add("cloud x " + lbl[i]);
        else if (T11Ov(tg, obs[i], 0)) p.Add("tag x " + lbl[i]);
        if (lab != null && T11Ov(lab, obs[i], 0)) p.Add("vp label x " + lbl[i]);
    }
    if (lab != null && lab[1] <= 0.05) p.Add("vp label bottom " + T11F(lab[1]));
    return p;
}
static double[] T11TagBox(IndependentTag tag, View v, double[] tagOff)
{
    double IN = 1.0 / 12.0;
    XYZ h = tag.TagHeadPosition;
    BoundingBoxXYZ bb = tag.get_BoundingBox(v);
    if (bb != null)
    {
        double w = bb.Max.X - bb.Min.X, hh = bb.Max.Y - bb.Min.Y;
        if (w > 0.05 * IN && w < 0.6 * IN && hh > 0.05 * IN && hh < 0.6 * IN) return new double[] { bb.Min.X, bb.Min.Y, bb.Max.X, bb.Max.Y };
    }
    return new double[] { h.X + tagOff[0], h.Y + tagOff[1], h.X + tagOff[2], h.Y + tagOff[3] };
}
public static object DummyTail() { return null;
