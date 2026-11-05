// BP-probe: READ-ONLY baseline probe of riser tag overlaps (QAQC3 plan A4).
// Body of: public static object Execute(Document document, object[] parameters)
// Runs under send_code_to_revit transactionMode "auto". Makes NO model changes.
// Writes full per-view JSON to C:/Users/Jeremy/swarm/manifests/BP-probe.result.json and returns a compact summary (<2000 chars).
// Revit 2024+ (ElementId.Value is long). No LINQ used; System types fully qualified.

var inv = System.Globalization.CultureInfo.InvariantCulture;
string resultPath = "C:/Users/Jeremy/swarm/manifests/BP-probe.result.json";
string targetFamily = "P_Pipe Tag";
string targetType = "Size & System & Comments (Left Justified)";

System.Func<string, string> esc = s =>
{
    if (s == null) return "";
    var sbE = new System.Text.StringBuilder();
    foreach (char ch in s)
    {
        switch (ch)
        {
            case '"': sbE.Append("\\\""); break;
            case '\\': sbE.Append("\\\\"); break;
            case '\n': sbE.Append("\\n"); break;
            case '\r': sbE.Append("\\r"); break;
            case '\t': sbE.Append("\\t"); break;
            default:
                if (ch < 0x20) sbE.Append("\\u" + ((int)ch).ToString("x4"));
                else sbE.Append(ch);
                break;
        }
    }
    return sbE.ToString();
};
System.Func<double, string> num = d => d.ToString("0.####", inv);

bool modifiedAtStart = document.IsModified;

// 1. Find the riser tag type(s) by name.
var typeIds = new System.Collections.Generic.HashSet<long>();
foreach (Element e in new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipeTags).WhereElementIsElementType())
{
    FamilySymbol fs = e as FamilySymbol;
    if (fs == null) continue;
    if (fs.FamilyName == targetFamily && fs.Name == targetType) typeIds.Add(fs.Id.Value);
}
if (typeIds.Count == 0) return "BP-probe ERROR: tag type '" + targetFamily + " : " + targetType + "' not found";

long pipeTagCat = (long)BuiltInCategory.OST_PipeTags;

// 2. One pass over all IndependentTags, one pass over TextNotes; group by owner view.
var riserByView = new System.Collections.Generic.Dictionary<long, System.Collections.Generic.List<IndependentTag>>();
var otherByView = new System.Collections.Generic.Dictionary<long, System.Collections.Generic.List<Element>>();
int riserTotal = 0, allTagTotal = 0, textNoteTotal = 0;
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(IndependentTag)))
{
    IndependentTag t = e as IndependentTag;
    if (t == null) continue;
    allTagTotal++;
    long ov = t.OwnerViewId.Value;
    bool isRiser = t.Category != null && t.Category.Id.Value == pipeTagCat && typeIds.Contains(t.GetTypeId().Value);
    if (isRiser)
    {
        riserTotal++;
        if (!riserByView.ContainsKey(ov)) riserByView[ov] = new System.Collections.Generic.List<IndependentTag>();
        riserByView[ov].Add(t);
    }
    else
    {
        if (!otherByView.ContainsKey(ov)) otherByView[ov] = new System.Collections.Generic.List<Element>();
        otherByView[ov].Add(e);
    }
}
foreach (Element e in new FilteredElementCollector(document).OfClass(typeof(TextNote)))
{
    textNoteTotal++;
    long ov = e.OwnerViewId.Value;
    if (!riserByView.ContainsKey(ov)) continue; // only views that own riser tags matter
    if (!otherByView.ContainsKey(ov)) otherByView[ov] = new System.Collections.Generic.List<Element>();
    otherByView[ov].Add(e);
}

// Helpers
System.Func<Element, View, double[]> box = (el, vw) =>
{
    BoundingBoxXYZ bb = null;
    try { bb = el.get_BoundingBox(vw); } catch { bb = null; }
    if (bb == null) return null;
    Transform tr = bb.Transform ?? Transform.Identity;
    double x0 = double.MaxValue, y0 = double.MaxValue, x1 = double.MinValue, y1 = double.MinValue;
    double[] xs = new double[] { bb.Min.X, bb.Max.X };
    double[] ys = new double[] { bb.Min.Y, bb.Max.Y };
    foreach (double x in xs)
    {
        foreach (double y in ys)
        {
            XYZ p = tr.OfPoint(new XYZ(x, y, bb.Min.Z));
            if (p.X < x0) x0 = p.X;
            if (p.X > x1) x1 = p.X;
            if (p.Y < y0) y0 = p.Y;
            if (p.Y > y1) y1 = p.Y;
        }
    }
    return new double[] { x0, y0, x1, y1 };
};
System.Func<View, string> sheetNo = vw =>
{
    try
    {
        Parameter p = vw.get_Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER);
        if (p == null) return "";
        string s = p.AsString();
        return s ?? "";
    }
    catch { return ""; }
};
System.Func<IndependentTag, string> tagDetail = t =>
{
    string text = "";
    try { text = t.TagText; } catch { }
    string head = "null";
    try { XYZ h = t.TagHeadPosition; head = "[" + num(h.X) + "," + num(h.Y) + "," + num(h.Z) + "]"; } catch { }
    bool hasLeader = false;
    try { hasLeader = t.HasLeader; } catch { }
    string lec = "n/a";
    if (hasLeader) { try { lec = t.LeaderEndCondition.ToString(); } catch { lec = "error"; } }
    return "{\"id\":" + t.Id.Value + ",\"text\":\"" + esc(text) + "\",\"head\":" + head +
           ",\"has_leader\":" + (hasLeader ? "true" : "false") + ",\"leader_end\":\"" + esc(lec) + "\"}";
};

// 3. Per owner view
var rows = new System.Collections.Generic.List<System.Tuple<int, string, string>>(); // (pairs, summaryLine, json)
int sumPairs = 0, sumPairsClr = 0, sumOtherHitTags = 0, sumLeader = 0, sumNullBox = 0, sumOtherPairs = 0;
foreach (var kv in riserByView)
{
    long ovId = kv.Key;
    var tags = kv.Value;
    View v = document.GetElement(new ElementId(ovId)) as View;
    string vName = v != null ? v.Name : "(no view " + ovId + ")";
    int scale = 1;
    try { if (v != null) scale = v.Scale; } catch { }
    if (scale <= 0) scale = 1;

    // dependents and sheets
    var depJson = new System.Text.StringBuilder();
    var sheetSet = new System.Collections.Generic.List<string>();
    int depCount = 0;
    bool ownerIsDependent = false;
    string ownSheet = "";
    if (v != null)
    {
        try { ownerIsDependent = v.GetPrimaryViewId() != ElementId.InvalidElementId; } catch { }
        ownSheet = sheetNo(v);
        if (ownSheet != "") sheetSet.Add(ownSheet);
        System.Collections.Generic.ICollection<ElementId> deps = null;
        try { deps = v.GetDependentViewIds(); } catch { deps = null; }
        if (deps != null)
        {
            foreach (ElementId did in deps)
            {
                View dv = document.GetElement(did) as View;
                if (dv == null) continue;
                string ds = sheetNo(dv);
                if (ds != "" && !sheetSet.Contains(ds)) sheetSet.Add(ds);
                if (depCount > 0) depJson.Append(",");
                depJson.Append("{\"id\":" + did.Value + ",\"name\":\"" + esc(dv.Name) + "\",\"sheet\":\"" + esc(ds) + "\"}");
                depCount++;
            }
        }
    }

    // riser boxes
    int n = tags.Count;
    var boxes = new double[n][];
    int leaderCount = 0, nullBox = 0;
    for (int i = 0; i < n; i++)
    {
        boxes[i] = v != null ? box(tags[i], v) : null;
        if (boxes[i] == null) nullBox++;
        try { if (tags[i].HasLeader) leaderCount++; } catch { }
    }
    var order = new System.Collections.Generic.List<int>();
    for (int i = 0; i < n; i++) if (boxes[i] != null) order.Add(i);
    order.Sort((a, b) => boxes[a][0].CompareTo(boxes[b][0]));

    // tag-tag pairs: plain and with 1/32" paper clearance.
    // Clearance rule: a pair counts if the gap between boxes is less than c = (1/32)/12 ft x scale,
    // i.e. each box inflated by c/2 per side.
    double c = (1.0 / 32.0) / 12.0 * scale;
    int pairs = 0, pairsClr = 0;
    var firstPairs = new System.Text.StringBuilder();
    int firstCount = 0;
    var tagInPair = new System.Collections.Generic.HashSet<int>();
    for (int oi = 0; oi < order.Count; oi++)
    {
        double[] A = boxes[order[oi]];
        for (int oj = oi + 1; oj < order.Count; oj++)
        {
            double[] B = boxes[order[oj]];
            if (B[0] >= A[2] + c) break; // sorted by minX; nothing further can be within clearance
            bool yClr = A[1] < B[3] + c && B[1] < A[3] + c;
            bool xClr = B[0] < A[2] + c && A[0] < B[2] + c;
            if (xClr && yClr) pairsClr++;
            bool hit = B[0] < A[2] && A[0] < B[2] && A[1] < B[3] && B[1] < A[3];
            if (hit)
            {
                pairs++;
                tagInPair.Add(order[oi]);
                tagInPair.Add(order[oj]);
                if (firstCount < 5)
                {
                    if (firstCount > 0) firstPairs.Append(",");
                    firstPairs.Append("{\"a\":" + tagDetail(tags[order[oi]]) + ",\"b\":" + tagDetail(tags[order[oj]]) + "}");
                    firstCount++;
                }
            }
        }
    }

    // tag vs other annotation (other IndependentTags any category + TextNotes owned by same view)
    int otherHitTags = 0, otherPairs = 0, otherTagPairs = 0, otherTextPairs = 0, otherCount = 0, otherNullBox = 0;
    System.Collections.Generic.List<Element> others;
    if (otherByView.TryGetValue(ovId, out others) && v != null)
    {
        otherCount = others.Count;
        var oBoxes = new System.Collections.Generic.List<double[]>();
        var oIsText = new System.Collections.Generic.List<bool>();
        foreach (Element oe in others)
        {
            double[] ob = box(oe, v);
            if (ob == null) { otherNullBox++; continue; }
            oBoxes.Add(ob);
            oIsText.Add(oe is TextNote);
        }
        foreach (int i in order)
        {
            double[] A = boxes[i];
            bool any = false;
            for (int k = 0; k < oBoxes.Count; k++)
            {
                double[] B = oBoxes[k];
                if (B[0] < A[2] && A[0] < B[2] && A[1] < B[3] && B[1] < A[3])
                {
                    any = true;
                    otherPairs++;
                    if (oIsText[k]) otherTextPairs++; else otherTagPairs++;
                }
            }
            if (any) otherHitTags++;
        }
    }

    sumPairs += pairs; sumPairsClr += pairsClr; sumOtherHitTags += otherHitTags; sumLeader += leaderCount;
    sumNullBox += nullBox; sumOtherPairs += otherPairs;

    string sheetsJ = "";
    for (int s = 0; s < sheetSet.Count; s++) sheetsJ += (s > 0 ? "," : "") + "\"" + esc(sheetSet[s]) + "\"";
    double leaderShare = n > 0 ? (double)leaderCount / n : 0.0;

    var vj = new System.Text.StringBuilder();
    vj.Append("{\"owner_view_id\":" + ovId);
    vj.Append(",\"owner_view_name\":\"" + esc(vName) + "\"");
    vj.Append(",\"view_type\":\"" + (v != null ? v.ViewType.ToString() : "") + "\"");
    vj.Append(",\"scale\":" + scale);
    vj.Append(",\"owner_is_dependent\":" + (ownerIsDependent ? "true" : "false"));
    vj.Append(",\"has_dependents\":" + (depCount > 0 ? "true" : "false"));
    vj.Append(",\"dependents\":[" + depJson.ToString() + "]");
    vj.Append(",\"owner_sheet\":\"" + esc(ownSheet) + "\"");
    vj.Append(",\"sheets\":[" + sheetsJ + "]");
    vj.Append(",\"riser_tag_count\":" + n);
    vj.Append(",\"null_bbox_tags\":" + nullBox);
    vj.Append(",\"tag_tag_overlap_pairs\":" + pairs);
    vj.Append(",\"tags_in_overlap_pairs\":" + tagInPair.Count);
    vj.Append(",\"tag_tag_pairs_within_1_32in_clearance\":" + pairsClr);
    vj.Append(",\"clearance_ft_model\":" + num(c));
    vj.Append(",\"other_annotation_count\":" + otherCount);
    vj.Append(",\"other_annotation_null_bbox\":" + otherNullBox);
    vj.Append(",\"riser_tags_overlapping_other_annotation\":" + otherHitTags);
    vj.Append(",\"riser_vs_other_pairs\":" + otherPairs);
    vj.Append(",\"riser_vs_other_tag_pairs\":" + otherTagPairs);
    vj.Append(",\"riser_vs_textnote_pairs\":" + otherTextPairs);
    vj.Append(",\"has_leader_count\":" + leaderCount);
    vj.Append(",\"has_leader_share\":" + num(leaderShare));
    vj.Append(",\"first_overlap_pairs\":[" + firstPairs.ToString() + "]}");

    string shortName = vName.Length > 40 ? vName.Substring(0, 40) : vName;
    string sheetsShort = string.Join("/", sheetSet.ToArray());
    string line = shortName + "|1:" + scale + "|" + sheetsShort + "|n" + n + "|p" + pairs + "|pc" + pairsClr + "|o" + otherHitTags + "|L" + leaderCount;
    rows.Add(System.Tuple.Create(pairs, line, vj.ToString()));
}

rows.Sort((a, b) => b.Item1.CompareTo(a.Item1));

// 4. Write full JSON
string typeIdsJ = "";
foreach (long tid in typeIds) typeIdsJ += (typeIdsJ.Length > 0 ? "," : "") + tid.ToString(inv);

var sb = new System.Text.StringBuilder();
sb.Append("{\"task_id\":\"BP-probe\"");
sb.Append(",\"document\":\"" + esc(document.Title) + "\"");
sb.Append(",\"doc_is_modified_at_start\":" + (modifiedAtStart ? "true" : "false"));
sb.Append(",\"tag_type\":\"" + esc(targetFamily + " : " + targetType) + "\"");
sb.Append(",\"tag_type_ids\":[" + typeIdsJ + "]");
sb.Append(",\"totals\":{\"riser_tags\":" + riserTotal + ",\"owner_views\":" + riserByView.Count +
          ",\"all_independent_tags\":" + allTagTotal + ",\"text_notes\":" + textNoteTotal +
          ",\"tag_tag_overlap_pairs\":" + sumPairs + ",\"tag_tag_pairs_within_1_32in_clearance\":" + sumPairsClr +
          ",\"riser_tags_overlapping_other_annotation\":" + sumOtherHitTags + ",\"riser_vs_other_pairs\":" + sumOtherPairs +
          ",\"has_leader_count\":" + sumLeader + ",\"has_leader_share\":" + num(riserTotal > 0 ? (double)sumLeader / riserTotal : 0.0) +
          ",\"null_bbox_tags\":" + sumNullBox + "}");
sb.Append(",\"notes\":[\"bbox = get_BoundingBox(view) XY in model ft; overlap = strict rectangle intersection\"," +
          "\"clearance pair = gap < (1/32)/12 ft x view.Scale (each box inflated by half that per side)\"," +
          "\"TextNote get_BoundingBox(view) is known to be undersized; textnote overlaps are a lower bound\"]");
sb.Append(",\"views\":[");
for (int r = 0; r < rows.Count; r++) { if (r > 0) sb.Append(","); sb.Append(rows[r].Item3); }
sb.Append("]}");

string writeStatus = "written";
try { System.IO.File.WriteAllText(resultPath, sb.ToString()); }
catch (System.Exception ex) { writeStatus = "WRITE FAILED: " + ex.Message; }

// 5. Compact summary
var sum = new System.Text.StringBuilder();
sum.Append("BP-probe OK (read-only). doc=" + document.Title + " modifiedAtStart=" + modifiedAtStart);
sum.Append(" | riserTags=" + riserTotal + " views=" + riserByView.Count + " allTags=" + allTagTotal + " textNotes=" + textNoteTotal +
           " pairs=" + sumPairs + " pairsClr1/32=" + sumPairsClr +
           " tagsHitOther=" + sumOtherHitTags + " leader=" + sumLeader + "/" + riserTotal + " nullBox=" + sumNullBox);
sum.Append(" | result=" + writeStatus);
sum.Append(" | top(name|scale|sheets|n|pairs|pairsClr|otherHit|leader):");
for (int r = 0; r < rows.Count && r < 10; r++) sum.Append("\n" + rows[r].Item2);
string outS = sum.ToString();
if (outS.Length > 1990) outS = outS.Substring(0, 1990);
return outS;
