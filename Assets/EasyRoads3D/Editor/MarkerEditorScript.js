import EasyRoads3D;
@CustomEditor(MarkerScript)
@CanEditMultipleObjects
class MarkerEditorScript extends Editor
{
var oldPos : Vector3;
var pos : Vector3;
var OODCOCOOCC : GUISkin;
var OQQQCCOOOQ : GUISkin;
var showGui : int;
var OCOCQOQDQQ : boolean;
var count:int = 0;
function OnEnable(){
if(target.objectScript == null) target.SetObjectScript();
else target.GetMarkerCount();
}
function OnInspectorGUI()
{


showGui = EasyRoadsGUIMenu(false, false, target.objectScript);
if(showGui != -1 && !target.objectScript.ODODDQOO) Selection.activeGameObject =  target.transform.parent.parent.gameObject;
else if(target.objectScript.ODQDODOODQs.length <= 1  && !target.objectScript.ODODDDOO) ERMarkerGUI(target);
else  if(target.objectScript.ODQDODOODQs.length == 2 && !target.objectScript.ODODDDOO) MSMarkerGUI(target);
else if(target.objectScript.ODODDDOO)TRMarkerGUI(target);


}
function OnSceneGUI() {
if(target.objectScript.OQCODQCQOC == null || target.objectScript.erInit == "") Selection.activeGameObject =  target.transform.parent.parent.gameObject;
else MarkerOnScene(target);
}
function EasyRoadsGUIMenu(flag : boolean, senderIsMain : boolean,  nRoadScript : RoadObjectScript) : int {
if((target.objectScript.OQDDCQCCDQ == null || target.objectScript.OODQCQCDQO == null || target.objectScript.ODOOCQDQCD == null) && target.objectScript.OQCODQCQOC){
target.objectScript.OQDDCQCCDQ = new boolean[5];
target.objectScript.OODQCQCDQO = new boolean[5];
target.objectScript.ODOOCQDQCD = nRoadScript;

target.objectScript.ODODQCCDOC = target.objectScript.OQCODQCQOC.OQQCCOQDQO();
target.objectScript.ODODQOQO = target.objectScript.OQCODQCQOC.ODDDCOQDCC();
target.objectScript.ODODQOQOInt = target.objectScript.OQCODQCQOC.OCDDCDCCQD();
}else if(target.objectScript.OQCODQCQOC == null) return;

if(target.objectScript.OODCOCOOCC == null){
target.objectScript.OODCOCOOCC = Resources.Load("ER3DSkin", GUISkin);
target.objectScript.OQQOCCQOOC = Resources.Load("ER3DLogo", Texture2D);
}
if(!flag) target.objectScript.OODODDQDOQ();
GUI.skin = target.objectScript.OODCOCOOCC;
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal ();
GUILayout.FlexibleSpace();
target.objectScript.OQDDCQCCDQ[0] = GUILayout.Toggle(target.objectScript.OQDDCQCCDQ[0] ,new GUIContent("", " Add road markers. "),"AddMarkers",GUILayout.Width(40), GUILayout.Height(22));
if(target.objectScript.OQDDCQCCDQ[0] == true && target.objectScript.OODQCQCDQO[0] == false) {
target.objectScript.OODODDQDOQ();
target.objectScript.OQDDCQCCDQ[0] = true;  target.objectScript.OODQCQCDQO[0] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OQDDCQCCDQ[1]  = GUILayout.Toggle(target.objectScript.OQDDCQCCDQ[1] ,new GUIContent("", " Insert road markers. "),"insertMarkers",GUILayout.Width(40),GUILayout.Height(22));
if(target.objectScript.OQDDCQCCDQ[1] == true && target.objectScript.OODQCQCDQO[1] == false) {
target.objectScript.OODODDQDOQ();
target.objectScript.OQDDCQCCDQ[1] = true;  target.objectScript.OODQCQCDQO[1] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OQDDCQCCDQ[2]  = GUILayout.Toggle(target.objectScript.OQDDCQCCDQ[2] ,new GUIContent("", " Process the terrain and create road geometry. "),"terrain",GUILayout.Width(40),GUILayout.Height(22));

if(target.objectScript.OQDDCQCCDQ[2] == true && target.objectScript.OODQCQCDQO[2] == false) {
if(target.objectScript.markers < 2){
EditorUtility.DisplayDialog("Alert", "A minimum of 2 road markers is required before the terrain can be leveled!", "Close");
target.objectScript.OQDDCQCCDQ[2] = false;
}else{
target.objectScript.OQDDCQCCDQ[2] = false;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;





}
}
if(target.objectScript.OQDDCQCCDQ[2] == false && target.objectScript.OODQCQCDQO[2] == true){

target.objectScript.OODQCQCDQO[2] = false;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OQDDCQCCDQ[3]  = GUILayout.Toggle(target.objectScript.OQDDCQCCDQ[3] ,new GUIContent("", " General settings. "),"settings",GUILayout.Width(40),GUILayout.Height(22));
if(target.objectScript.OQDDCQCCDQ[3] == true && target.objectScript.OODQCQCDQO[3] == false) {
target.objectScript.OODODDQDOQ();
target.objectScript.OQDDCQCCDQ[3] = true;  target.objectScript.OODQCQCDQO[3] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OQDDCQCCDQ[4]  = GUILayout.Toggle(target.objectScript.OQDDCQCCDQ[4] ,new GUIContent("", "Version and Purchase Info"),"info",GUILayout.Width(40),GUILayout.Height(22));
if(target.objectScript.OQDDCQCCDQ[4] == true && target.objectScript.OODQCQCDQO[4] == false) {
target.objectScript.OODODDQDOQ();
target.objectScript.OQDDCQCCDQ[4] = true;  target.objectScript.OODQCQCDQO[4] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
GUI.skin = null;
target.objectScript.ODDDQCOOQD = -1;
for(var i : int  = 0; i < 5; i++){
if(target.objectScript.OQDDCQCCDQ[i]){
target.objectScript.ODDDQCOOQD = i;
target.objectScript.OODQCOQQQQ = i;
}
}
if(target.objectScript.ODDDQCOOQD == -1) target.objectScript.OODODDQDOQ();
var markerMenuDisplay : int = 1;
if(target.objectScript.ODDDQCOOQD == 0 || target.objectScript.ODDDQCOOQD == 1) markerMenuDisplay = 0;
else if(target.objectScript.ODDDQCOOQD == 2 || target.objectScript.ODDDQCOOQD == 3 || target.objectScript.ODDDQCOOQD == 4) markerMenuDisplay = 0;
if(target.objectScript.OOCQCCQOQQ && !target.objectScript.OQDDCQCCDQ[2] && !target.objectScript.ODODDQOO){
target.objectScript.OOCQCCQOQQ = false;
if(target.objectScript.objectType != 2)target.objectScript.OQCODQODOQ();
if(target.objectScript.objectType == 2 && target.objectScript.OOCQCCQOQQ){
Debug.Log("restore");
target.objectScript.OQCODQCQOC.OCCODDODOO(target.transform, true);
}
}
GUI.skin.box.alignment = TextAnchor.UpperLeft;
if(target.objectScript.ODDDQCOOQD >= 0 && target.objectScript.ODDDQCOOQD != 4){
if(target.objectScript.ODODQCCDOC == null || target.objectScript.ODODQCCDOC.Length == 0){

target.objectScript.ODODQCCDOC = target.objectScript.OQCODQCQOC.OQQCCOQDQO();
target.objectScript.ODODQOQO = target.objectScript.OQCODQCQOC.ODDDCOQDCC();
target.objectScript.ODODQOQOInt = target.objectScript.OQCODQCQOC.OCDDCDCCQD();
}
EditorGUILayout.BeginHorizontal();
GUILayout.Box(target.objectScript.ODODQCCDOC[target.objectScript.ODDDQCOOQD], GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(50));
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
}
return target.objectScript.ODDDQCOOQD;
}
function ERMarkerGUI( markerScript : MarkerScript){
EditorGUILayout.Space();
GUILayout.Box(" Marker: " + (target.markerNum + 1).ToString(), GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
if(target.distance == "-1" && target.objectScript.OQCODQCQOC != null){
var arr = target.objectScript.OQCODQCQOC.ODQQQDDDCQ(target.markerNum);
target.distance = arr[0];
target.ODODQDQDDC = arr[1];
target.OCOCCDDCOD = arr[2];
}
GUILayout.Label(" Total Distance to Marker: " + target.distance.ToString() + " km");
GUILayout.Label(" Segment Distance to Marker: " + target.ODODQDQDDC.ToString() + " km");
GUILayout.Label(" Marker Distance: " + target.OCOCCDDCOD.ToString() + " m");
EditorGUILayout.Space();
GUILayout.Box(" Marker Settings", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
var oldss : boolean = markerScript.OQCQOQCQCC;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Soft Selection", "When selected, the settings of other road markers within the selected distance will change according their distance to this marker."),  GUILayout.Width(105));
GUI.SetNextControlName ("OQCQOQCQCC");
markerScript.OQCQOQCQCC = EditorGUILayout.Toggle (markerScript.OQCQOQCQCC);
EditorGUILayout.EndHorizontal();
if(markerScript.OQCQOQCQCC){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("         Distance", "The soft selection distance within other markers should change too."),  GUILayout.Width(105));
markerScript.OQDOCODDQO = EditorGUILayout.Slider(markerScript.OQDOCODDQO, 0, 500);
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
}
if(oldss != markerScript.OQDOCODDQO) target.objectScript.ResetMaterials(markerScript);
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Left Indent", "The distance from the left side of the road to the part of the terrain levelled at the same height as the road") ,  GUILayout.Width(105));
GUI.SetNextControlName ("ri");
oldfl = markerScript.ri;
markerScript.ri = EditorGUILayout.Slider(markerScript.ri, target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.ri){
target.objectScript.ODDQODCQDQ("ri", markerScript);
markerScript.OOQOQQOO = markerScript.ri;
}
GUI.enabled = true;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Right Indent", "The distance from the right side of the road to the part of the terrain levelled at the same height as the road") ,  GUILayout.Width(105));
oldfl = markerScript.li;
markerScript.li =  EditorGUILayout.Slider(markerScript.li, target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.li){
target.objectScript.ODDQODCQDQ("li", markerScript);
markerScript.ODODQQOO = markerScript.li;
}
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Left Surrounding", "This represents the distance over which the terrain will be gradually leveled on the left side of the road to the original terrain height"),  GUILayout.Width(105));
oldfl = markerScript.rs;
GUI.SetNextControlName ("rs");
markerScript.rs = EditorGUILayout.Slider(markerScript.rs,  target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.rs){
target.objectScript.ODDQODCQDQ("rs", markerScript);
markerScript.ODOQQOOO = markerScript.rs;
}
GUI.enabled = true;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Right Surrounding", "This represents the distance over which the terrain will be gradually leveled on the right side of the road to the original terrain height"),  GUILayout.Width(105));
oldfl = markerScript.ls;
markerScript.ls = EditorGUILayout.Slider(markerScript.ls,  target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.ls){
target.objectScript.ODDQODCQDQ("ls", markerScript);
markerScript.DODOQQOO = markerScript.ls;
}
if(target.objectScript.objectType == 0){
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
oldfl = markerScript.rt;
GUILayout.Label(new GUIContent("    Left Tilting", "Use this setting to tilt the road on the left side (m)."),  GUILayout.Width(105));
markerScript.qt = EditorGUILayout.Slider(markerScript.qt, 0, target.objectScript.roadWidth * 0.5f);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.rt){
target.objectScript.ODDQODCQDQ("rt", markerScript);
markerScript.ODDQODOO = markerScript.rt;
}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Right Tilting", "Use this setting to tilt the road on the right side (cm)."),  GUILayout.Width(105));
oldfl = markerScript.lt;
markerScript.lt = EditorGUILayout.Slider(markerScript.lt, 0, target.objectScript.roadWidth * 0.5f);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.lt){
target.objectScript.ODDQODCQDQ("lt", markerScript);
markerScript.ODDOQOQQ = markerScript.lt;
}
GUI.enabled = true;
if(target.markerInt < 2){
GUILayout.Label(new GUIContent("    Bridge Objects are disabled for the first two markers!", ""));
}else{
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Bridge Object", "When selected this road segment will be treated as a bridge segment."),  GUILayout.Width(105));
GUI.SetNextControlName ("bridgeObject");
markerScript.bridgeObject = EditorGUILayout.Toggle (markerScript.bridgeObject);
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
if(markerScript.bridgeObject){
GUILayout.Label(new GUIContent("      Distribute Heights", "When selected the terrain, the terrain will be gradually leveled between the height of this road segment and the current terrain height of the inner bridge segment."),  GUILayout.Width(135));
GUI.SetNextControlName ("distHeights");
markerScript.distHeights = EditorGUILayout.Toggle (markerScript.distHeights);
}
EditorGUILayout.EndHorizontal();
}

GUI.enabled = true;
}else{
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Floor Depth", "Use this setting to change the floor depth for this marker."),  GUILayout.Width(105));
oldfl = markerScript.floorDepth;
markerScript.floorDepth = EditorGUILayout.Slider(markerScript.floorDepth, 0, 50);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.floorDepth){
target.objectScript.ODDQODCQDQ("floorDepth", markerScript);
markerScript.oldFloorDepth = markerScript.floorDepth;
}
}
EditorGUILayout.Space();
GUI.enabled = false;
if(target.objectScript.objectType == 0){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Start New LOD Segment", "Use this to split the road or river object in segments to use in LOD system."),  GUILayout.Width(170));
markerScript.newSegment = EditorGUILayout.Toggle (markerScript.newSegment);
EditorGUILayout.EndHorizontal();
}
GUI.enabled = true;
EditorGUILayout.Space();
if(!markerScript.autoUpdate){
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Refresh Surface", GUILayout.Width(225))){
target.objectScript.OQQOCCDODC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
}
if (GUI.changed && !target.objectScript.OQDDDDCCQC){
target.objectScript.OQDDDDCCQC = true;
}else if(target.objectScript.OQDDDDCCQC){
target.objectScript.ODCCCDCCDQ(markerScript);
target.objectScript.OQDDDDCCQC = false;
SceneView.lastActiveSceneView.Repaint();
}
oldfl = markerScript.rs;
}
function MSMarkerGUI( markerScript : MarkerScript){
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Align XYZ", "Click to distribute the x, y and z values of all markers in between the selected markers in a line between the selected markers."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.OQCODQCQOC.OOCQCDCCDD(target.objectScript.ODQDODOODQs, 0);
target.objectScript.OQQOCCDODC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Align XZ", "Click to distribute the x and z values of all markers in between the selected markers in a line between the selected markers."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.OQCODQCQOC.OOCQCDCCDD(target.objectScript.ODQDODOODQs, 1);
target.objectScript.OQQOCCDODC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Align XZ  Snap Y", "Click to distribute the x and z values of all markers in between the selected markers in a line between the selected markers and snap the y value to the terrain height at the new position."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.OQCODQCQOC.OOCQCDCCDD(target.objectScript.ODQDODOODQs, 2);
target.objectScript.OQQOCCDODC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Average Heights ", "Click to distribute the heights all markers in between the selected markers."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.OQCODQCQOC.OOCQCDCCDD(target.objectScript.ODQDODOODQs, 3);
target.objectScript.OQQOCCDODC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.Space();
}
function TRMarkerGUI(markerScript : MarkerScript){
EditorGUILayout.Space();
}
function MarkerOnScene(markerScript : MarkerScript){
var cEvent : Event = Event.current;

if(!target.objectScript.ODODDDOO || target.objectScript.objectType == 2){
if(cEvent.shift && (target.objectScript.OODQCOQQQQ == 0 || target.objectScript.OODQCOQQQQ == 1)) {
Selection.activeGameObject =  markerScript.transform.parent.parent.gameObject;
}else if(cEvent.shift && target.objectScript.ODQDODOODQ != target.transform){
target.objectScript.OQCDCQOQDO(markerScript);
Selection.objects = target.objectScript.ODQDODOODQs;
}else if(target.objectScript.ODQDODOODQ != target.transform && count == 0){
if(!target.InSelected()){
target.objectScript.ODQDODOODQs = new GameObject[0];
target.objectScript.OQCDCQOQDO(markerScript);
Selection.objects = target.objectScript.ODQDODOODQs;


count++;
}

}else{

if(cEvent.control)target.snapMarker = true;
else target.snapMarker = false;

pos = markerScript.oldPos;
if(pos  != oldPos && !markerScript.changed){
oldPos = pos;
if(!cEvent.shift){
target.objectScript.ODOODCODQC(markerScript);
}
}
}
if(cEvent.shift && markerScript.changed){
OCOCQOQDQQ = true;
}
markerScript.changed = false;
if(!cEvent.shift && OCOCQOQDQQ){
target.objectScript.ODOODCODQC(markerScript);
OCOCQOQDQQ = false;
}
}

}
}
