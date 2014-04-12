import System.Collections.Generic;
import System.IO;
import EasyRoads3D;
import EasyRoads3DEditor;
@CustomEditor(RoadObjectScript)
class RoadObjectEditorScript extends Editor
{
var counter : int;
var pe : float;
var tv : boolean;
var tvDone : boolean;
var debugDone : boolean;
var res : boolean;
var col : Collider;

function OnEnable(){

target.backupLocation = EditorPrefs.GetInt("ER3DbckLocation", 0);

if(target.OQCODQCQOC == null){
ODCDDDDDDQ();
target.OCOOCQODQD(null, null, null);
}

target.ODODQOQO = target.OQCODQCQOC.ODDDCOQDCC();
target.ODODQOQOInt = target.OQCODQCQOC.OCDDCDCCQD();
if(target.splatmapLayer >= target.ODODQOQO.Length)target.splatmapLayer = 4;
if(target.customMesh != null){
if(target.customMesh.GetComponent(typeof(Collider))){
col = target.customMesh.GetComponent(typeof(Collider));
}else if(OQCDOODQQQ.terrain != null){
col = OQCDOODQQQ.terrain.GetComponent(typeof(TerrainCollider));
}
}else if(OQCDOODQQQ.terrain != null){
col = OQCDOODQQQ.terrain.GetComponent(typeof(TerrainCollider));
}

if(ODCDDDDDDQ()){
OQCDOODQQQ.OQOOCOCDDQ();
}
target.ODQDODOODQs = new GameObject[0];




}
function OnInspectorGUI(){

EasyRoadsGUIMenu(true, true, target);
}
function OnSceneGUI() {
if(target.OQCODQCQOC == null){
ODCDDDDDDQ();
target.OCOOCQODQD(null, null, null);
if(target.OCDQCCOCOC != EditorApplication.currentScene && target.OQCODQCQOC == null){
OCQCDCCDOC.terrainList.Clear();
target.OCDQCCOCOC = EditorApplication.currentScene;
}

}

OnScene();

}
function EasyRoadsGUIMenu(flag : boolean, senderIsMain : boolean,  nRoadScript : RoadObjectScript) : int {





if(target.OQDDCQCCDQ == null || target.OODQCQCDQO == null || target.ODOOCQDQCD == null || target.OQDDCQCCDQ.Length == 0 ){
target.OQDDCQCCDQ = new boolean[5];
target.OODQCQCDQO = new boolean[5];
target.ODOOCQDQCD = nRoadScript;

target.ODODQCCDOC = target.OQCODQCQOC.OQQCCOQDQO();
target.ODODQOQO = target.OQCODQCQOC.ODDDCOQDCC();
target.ODODQOQOInt = target.OQCODQCQOC.OCDDCDCCQD();
}
origAnchor = GUI.skin.box.alignment;
if(target.OODCOCOOCC == null){
target.OODCOCOOCC = Resources.Load("ER3DSkin", GUISkin);
target.OQQOCCQOOC = Resources.Load("ER3DLogo", Texture2D);
}
if(!flag) target.OODODDQDOQ();
if(target.ODDDQCOOQD == -1) target.ODQDODOODQ = null;
var origSkin : GUISkin = GUI.skin;
GUI.skin = target.OODCOCOOCC;
EditorGUILayout.Space();

EditorGUILayout.BeginHorizontal ();
GUILayout.FlexibleSpace();
target.OQDDCQCCDQ[0] = GUILayout.Toggle(target.OQDDCQCCDQ[0] ,new GUIContent("", " Add road markers. "),"AddMarkers",GUILayout.Width(40), GUILayout.Height(22));
if(target.OQDDCQCCDQ[0] == true && target.OODQCQCDQO[0] == false) {
target.OODODDQDOQ();
target.OQDDCQCCDQ[0] = true;  target.OODQCQCDQO[0] = true;
}
target.OQDDCQCCDQ[1]  = GUILayout.Toggle(target.OQDDCQCCDQ[1] ,new GUIContent("", " Insert road markers. "),"insertMarkers",GUILayout.Width(40),GUILayout.Height(22));
if(target.OQDDCQCCDQ[1] == true && target.OODQCQCDQO[1] == false) {
target.OODODDQDOQ();
target.OQDDCQCCDQ[1] = true;  target.OODQCQCDQO[1] = true;
}
target.OQDDCQCCDQ[2]  = GUILayout.Toggle(target.OQDDCQCCDQ[2] ,new GUIContent("", " Process the terrain and create road geometry. "),"terrain",GUILayout.Width(40),GUILayout.Height(22));

if(target.OQDDCQCCDQ[2] == true && (target.OODQCQCDQO[2] == false || target.doTerrain)) {

if(target.markers <= 2){
EditorUtility.DisplayDialog("Alert", "A minimum of 2 road markers is required before the terrain can be leveled!", "Close");
target.OQDDCQCCDQ[2] = false;
}else{
if(target.disableFreeAlerts)EditorUtility.DisplayDialog("Alert", "Switching back to 'Edit Mode' is not supported in the free version.\n\nClick Close to generate the road mesh and deform the terrain. This process can take some time depending on the terrains heightmap resolution and the optional vegetation removal, please be patient!\n\nYou can always restore the terrain using the EasyRoads3D terrain restore option in the main menu.\n\nNote: you can disable displaying this message in General Settings.", "Close");
if(!flag){
EditorUtility.DisplayDialog("Alert", "The Unity Terrain Object does not accept height values < 0. The river floor will be equal or higher then the water level. Position all markers higher above the terrain!", "Close");
target.OQDDCQCCDQ[2] = false;
}else{
tvDone = false;
target.OODODDQDOQ();
target.OQDDCQCCDQ[2] = true;  target.OODQCQCDQO[2] = true;
target.OOCQCCQOQQ = true;
target.doTerrain = false;
target.markerDisplayStr = "Show Markers";
if(target.objectType < 2){




#if UNITY_4_3

#else
Undo.RegisterUndo(OQCDOODQQQ.terrain.terrainData, "EasyRoads3D Terrain leveling");
#endif




if(!target.displayRoad){
target.displayRoad = true;
target.OQCODQCQOC.OQOCOCCQOC(true, target.OCCQOQDDDO);
}
OCQCDCCDOC.ODQODCODOD = false;

OOQCOOQQDO(target);
if(target.OOQDOOQQ)target.OQCCOODCDO();



}else{

target.OQCODQCQOC.OCCODDODOO(target.transform, false);
}
} 
if(target.disableFreeAlerts)EditorUtility.DisplayDialog("Finished!", "The terrain data has been updated.\n\nIf you want to keep these changes and add more road objects it is recommended to update the terrain backup data using the EasyRoads3D terrain backup options in the main menu. By doing this you will not loose the current terrain changes if later in the development process you want to restore the terrain back to the current status.\n\nYou can also duplicate the terrain object in the project panel and keep that as the terrain backup.\n\nNote: you can disable displaying this message in General Settings.", "Close");
}
}

target.OQDDCQCCDQ[3]  = GUILayout.Toggle(target.OQDDCQCCDQ[3] ,new GUIContent("", " General settings. "),"settings",GUILayout.Width(40),GUILayout.Height(22));
if(target.OQDDCQCCDQ[3] == true && target.OODQCQCDQO[3] == false) {
target.OODODDQDOQ();
target.OQDDCQCCDQ[3] = true;  target.OODQCQCDQO[3] = true;
}
target.OQDDCQCCDQ[4]  = GUILayout.Toggle(target.OQDDCQCCDQ[4] ,new GUIContent("", "Version and Purchase Info"),"info",GUILayout.Width(40),GUILayout.Height(22));
if(target.OQDDCQCCDQ[4] == true && target.OODQCQCDQO[4] == false) {
target.OODODDQDOQ();
target.OQDDCQCCDQ[4] = true;  target.OODQCQCDQO[4] = true;
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
GUI.skin = null;
GUI.skin = origSkin;
target.ODDDQCOOQD = -1;
for(var i : int  = 0; i < 5; i++){
if(target.OQDDCQCCDQ[i]){
target.ODDDQCOOQD = i;
target.OODQCOQQQQ = i;
}
}
if(target.ODDDQCOOQD == -1) target.OODODDQDOQ();
var markerMenuDisplay : int = 1;
if(target.ODDDQCOOQD == 0 || target.ODDDQCOOQD == 1) markerMenuDisplay = 0;
else if(target.ODDDQCOOQD == 2 || target.ODDDQCOOQD == 3 || target.ODDDQCOOQD == 4) markerMenuDisplay = 0;

if(target.OOCQCCQOQQ && !target.OQDDCQCCDQ[2] && Application.isPlaying){
EditorPrefs.SetBool("ERv2isPlaying", true);

}







if(target.OOCQCCQOQQ && !target.OQDDCQCCDQ[2]){ 
target.OQDDCQCCDQ[2] = true;
target.OODQCQCDQO[2] = true;
if(target.disableFreeAlerts)EditorUtility.DisplayDialog("Alert", "Switching back to 'Edit Mode' to add markers or change other settings is not supported in the free version.\n\nDrag the road mesh to the root of the hierarchy and delete the EasyRoads3D editor object once the road is ready!\n\nYou can use Undo to restore the terrain.", "Close");
}
GUI.skin.box.alignment = TextAnchor.UpperLeft;
if(target.ODDDQCOOQD >= 0 && target.ODDDQCOOQD != 4){
if(target.ODODQCCDOC == null || target.ODODQCCDOC.Length == 0){

target.ODODQCCDOC = target.OQCODQCQOC.OQQCCOQDQO();
target.ODODQOQO = target.OQCODQCQOC.ODDDCOQDCC();
target.ODODQOQOInt = target.OQCODQCQOC.OCDDCDCCQD();
}
EditorGUILayout.BeginHorizontal();
GUILayout.Box(target.ODODQCCDOC[target.ODDDQCOOQD], GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(50));
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
}
if(target.ODDDQCOOQD == -1 && target.ODQDODOODQ != null) Selection.activeGameObject =  target.ODQDODOODQ.gameObject;
GUI.skin.box.alignment = origAnchor;

if(target.erInit == "" || (OCQCDCCDOC.debugFlag && !debugDone)){
debugDone = true;

target.erInit = OCDQQCOQOD.OQOCQOQCOO(target.version); 
target.OQCODQCQOC.erInit = target.erInit;



this.Repaint();

}
if(target.erInit != "" && res){

target.OQDODQOODQ(target.geoResolution, false, false);
res = false;
}
if(target.erInit.Length == 0){
}else if(target.ODDDQCOOQD == 0 || target.ODDDQCOOQD == 1){
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Refresh Surfaces", GUILayout.Width(200))){
target.ODDCCCQCOC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
}else if(target.ODDDQCOOQD == 3){

GUI.skin.box.alignment = TextAnchor.MiddleLeft;
GUILayout.Box(" General Settings", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
if(target.objectType != 2){
GUILayout.Space(10);
var oldDisplay : boolean = target.displayRoad;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Display object", "This will activate/deactivate the road object transforms"), GUILayout.Width(125) );
target.displayRoad = EditorGUILayout.Toggle (target.displayRoad);
EditorGUILayout.EndHorizontal();
if(oldDisplay != target.displayRoad){
target.OQCODQCQOC.OQOCOCCQOC(target.displayRoad, target.OCCQOQDDDO);
}
}
if(target.materialStrings == null){target.materialStrings = new String[2]; target.materialStrings[0] = "Diffuse Shader"; target.materialStrings[1] = "Transparent Shader"; }
if(target.materialStrings.Length == 0){target.materialStrings = new String[2]; target.materialStrings[0] = "Diffuse Shader"; target.materialStrings[1] = "Transparent Shader"; }
var cm : int = target.materialType;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Surface Material", "The material type used for the road surfaces."), GUILayout.Width(125) );
target.materialType = EditorGUILayout.Popup (target.materialType, target.materialStrings,   GUILayout.Width(115));
EditorGUILayout.EndHorizontal();
if(cm != target.materialType) target.OQCODQCQOC.ODODDDCCOQ(target.materialType);
if(target.materialType == 1){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("        Surface Opacity", "This controls the transparacy level of the surface objects."), GUILayout.Width(125) );
var so : float = target.surfaceOpacity;
target.surfaceOpacity = EditorGUILayout.Slider(target.surfaceOpacity, 0, 1,  GUILayout.Width(150));
EditorGUILayout.EndHorizontal();
if(so != target.surfaceOpacity) target.OQCODQCQOC.OOCQQCCCDO(target.surfaceOpacity);
}
EditorGUILayout.Space();
if(target.objectType < 2){
var od: boolean = target.multipleTerrains;
}
GUI.enabled = true;
GUI.enabled = false;
var cl = target.backupLocation;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Backup Location", "Use outside Assets folder unless you are using the asset server."), GUILayout.Width(125) );
target.backupLocation = EditorGUILayout.Popup (target.backupLocation, target.backupStrings,   GUILayout.Width(115));
EditorGUILayout.EndHorizontal();
if(target.backupLocation != cl){
if(target.backupLocation == 1){
if(EditorUtility.DisplayDialog("Backup Location", "Changing the backup location to inside the assets folder is only recommended when you want to synchronize EasyRoads3D backup files with the assetserver.\n\nWould you like to continue?", "Yes", "No")){
EditorPrefs.SetInt("ER3DbckLocation", target.backupLocation);
OOQDDDCQDD.SwapFiles(target.backupLocation);
EditorUtility.DisplayDialog("Confirmation", "The backup location has been updated, all backup folders and files have been copied to the new location.\n\nUse CTRL+R to update the assets folder!", "Close");
}else target.backupLocation = 0;
}else{
if(EditorUtility.DisplayDialog("Backup Location", "The backup location will be changed to outside the assets folder.\n\nWould you like to continue?", "Yes", "No")){
EditorPrefs.SetInt("ER3DbckLocation", target.backupLocation);
OOQDDDCQDD.SwapFiles(target.backupLocation);
EditorUtility.DisplayDialog("Confirmation", "The backup location has been updated, all backup folders and files have been copied to the new location.\n\nUse CTRL+R to update the assets folder!", "Close");
}else target.backupLocation = 1;
}
}
GUI.enabled = true;
od = OCQCDCCDOC.debugFlag;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Enable Debugging", "This will enable debugging."), GUILayout.Width(125) );;
OCQCDCCDOC.debugFlag = EditorGUILayout.Toggle (OCQCDCCDOC.debugFlag);
EditorGUILayout.EndHorizontal();
if(od != OCQCDCCDOC.debugFlag && OCQCDCCDOC.debugFlag) debugDone = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Free version alerts", "Uncheck to disable free version alerts."), GUILayout.Width(125) );;
target.disableFreeAlerts = EditorGUILayout.Toggle (target.disableFreeAlerts);
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
GUILayout.Box(" Object Settings", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
EditorGUILayout.BeginHorizontal();
var wd : float = target.roadWidth;
if(target.objectType == 0)GUILayout.Label(new GUIContent("    Road width", "The width of the road") ,  GUILayout.Width(125));
else GUILayout.Label(new GUIContent("    River Width", "The width of the river") ,  GUILayout.Width(125));
target.roadWidth = EditorGUILayout.FloatField(target.roadWidth, GUILayout.Width(40) );
EditorGUILayout.EndHorizontal();
if(wd != target.roadWidth) target.OQDODQOODQ(target.geoResolution, false, false);
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Default Indent", "The distance from the left and right side of the road to the part of the terrain levelled at the same height as the road"),  GUILayout.Width(125));
target.indent = EditorGUILayout.FloatField(target.indent, GUILayout.Width(40));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Raise Markers", "This will raise the position of the markers (m)."), GUILayout.Width(125) );;
target.raiseMarkers = EditorGUILayout.FloatField (target.raiseMarkers, GUILayout.Width(40));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Force Y Position", "When toggled on, ne road markers will inherit the y position of the previous marker."), GUILayout.Width(125) );;
target.forceY = EditorGUILayout.Toggle (target.forceY);
EditorGUILayout.EndHorizontal();
if(target.forceY){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("        Y Change", "The marker will be raised / lowered according this amount for every 100 meters."), GUILayout.Width(125) );;
target.yChange = EditorGUILayout.FloatField (target.yChange, GUILayout.Width(40));
EditorGUILayout.EndHorizontal();
}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Surrounding", "This represents the distance over which the terrain will be gradually leveled to the original terrain height"),  GUILayout.Width(125));
target.surrounding = EditorGUILayout.FloatField(target.surrounding, GUILayout.Width(40));
EditorGUILayout.EndHorizontal();
var OldClosedTrack : boolean = target.OOQDOOQQ;
EditorGUILayout.BeginHorizontal();
if(target.objectType == 0)GUILayout.Label(new GUIContent("    Closed Track", "This will connect the 'start' and 'end' of the road"), GUILayout.Width(125) );
else if(target.objectType == 1)GUILayout.Label(new GUIContent("    Closed River", "This will connect the 'start' and 'end' of the river"), GUILayout.Width(125) );
else GUILayout.Label(new GUIContent("    Closed Object", "This will connect the 'start' and 'end' of the object"), GUILayout.Width(125) );
target.OOQDOOQQ = EditorGUILayout.Toggle (target.OOQDOOQQ);
EditorGUILayout.EndHorizontal();
if(OldClosedTrack != target.OOQDOOQQ){
target.ODDCCCQCOC();
}
EditorGUILayout.BeginHorizontal();
GUI.enabled = false;
GUILayout.Label(new GUIContent("    iOS Platform", "This will prepare the road mesh for the iOS Platform"), GUILayout.Width(125) );
target.iOS = EditorGUILayout.Toggle (target.iOS);
EditorGUILayout.EndHorizontal();
if(OldClosedTrack != target.iOS){
}
GUI.enabled = true;

EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Geometry Resolution", "The polycount of the generated surfaces. It is recommended to use a low resolution while creating the road. Use the maximum resolution when processing the final terrain."), GUILayout.Width(125) );
var gr : float = target.geoResolution;
target.geoResolution = EditorGUILayout.Slider(target.geoResolution, 0.5, 5,  GUILayout.Width(150));
EditorGUILayout.EndHorizontal();
if(gr != target.geoResolution) target.OQDODQOODQ(target.geoResolution, false, false);
EditorGUILayout.BeginHorizontal();
OldClosedTrack = target.iOS;
GUI.enabled = false;
GUILayout.Label(new GUIContent("    Tangents", "This will automatically calculate mesh tangents data required for bump mapping. Note that this will take a little bit more preocessing time."), GUILayout.Width(125) );
target.applyTangents = EditorGUILayout.Toggle (target.applyTangents);
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
EditorGUILayout.Space();
GUILayout.Box(" Render Settings", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
GUI.enabled = false;
if(OQCDOODQQQ.selectedTerrain == null)OQCDOODQQQ.OQOOCOCDDQ();
var st : int = OQCDOODQQQ.selectedTerrain;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Active Terrain", "The terrain that will be updated"), GUILayout.Width(125) );
OQCDOODQQQ.selectedTerrain = EditorGUILayout.Popup (OQCDOODQQQ.selectedTerrain, OQCDOODQQQ.terrainStrings,   GUILayout.Width(115));
EditorGUILayout.EndHorizontal();
if(st != OQCDOODQQQ.selectedTerrain)OQCDOODQQQ.OOODOQCOOQ();
GUI.enabled = true;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Update Vegetation", "When toggled on tree and detail objects near the road will be removed when rendering the terrain."), GUILayout.Width(125) );;
target.handleVegetation = EditorGUILayout.Toggle (target.handleVegetation);
EditorGUILayout.EndHorizontal();
if(target.handleVegetation){
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("      Tree Distance (m)", "The distance from the left and the right of the road up to which terrain trees should be removed."), GUILayout.Width(125) );
var tr : float = target.OCOCODCCDD;
target.OCOCODCCDD = EditorGUILayout.Slider(target.OCOCODCCDD, 0, 25,  GUILayout.Width(150));
EditorGUILayout.EndHorizontal();
if(tr != target.OCOCODCCDD) target.OQCODQCQOC.OCOCODCCDD = target.OCOCODCCDD;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("      Detail Distance (m)", "The distance from the left and the right of the road up to which terrain detail opbjects should be removed."), GUILayout.Width(125) );
tr = target.ODDQODQCOO;
target.ODDQODQCOO = EditorGUILayout.Slider(target.ODDQODQCOO, 0, 25,  GUILayout.Width(150));
EditorGUILayout.EndHorizontal();
if(tr != target.ODDQODQCOO) target.OQCODQCQOC.ODDQODQCOO = target.ODDQODQCOO;
GUI.enabled = true;
}
EditorGUILayout.Space();


}else if(target.ODDDQCOOQD == 2){

EditorGUILayout.Space();
if(target.objectType == 0)GUILayout.Box(" Road Settings:", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
else GUILayout.Box(" River Settings:", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
GUILayout.Space(10);
var oldRoad : boolean = target.renderRoad;
var oldRoadResolution : float = target.roadResolution;
var oldRoadUV : float = target.tuw;
var oldRaise : float = target.raise;
var oldSegments : int = target.OdQODQOD;
var oldOOQQQDOD : float = target.OOQQQDOD;
var oldOOQQQDODOffset : float = target.OOQQQDODOffset;
var oldOOQQQDODLength : float = target.OOQQQDODLength;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Render"," When active, terrain matching road geometry will be created."), GUILayout.Width(105) );
target.renderRoad = EditorGUILayout.Toggle (target.renderRoad);
EditorGUILayout.EndHorizontal();
if(target.renderRoad){
if(target.objectType == 0){
if(target.roadTexture == null){
mat = Resources.Load("roadMaterial", typeof(Material));
target.roadTexture = mat.mainTexture;
}
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Material"," The road texture."), GUILayout.Width(105) );
if(GUILayout.Button (target.roadTexture, GUILayout.Width(75), GUILayout.Height(75))){
}
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
}
EditorGUILayout.BeginHorizontal();
GUI.enabled = false;
GUILayout.Label(new GUIContent(" Road Segments"," The number of segments over the width of the road."), GUILayout.Width(105) );
target.OdQODQOD = EditorGUILayout.IntSlider(target.OdQODQOD, 1, 10,  GUILayout.Width(175));
GUI.enabled = true;
EditorGUILayout.EndHorizontal();
if(target.OdQODQOD > 1){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Bumpyness"," The bumypness of the surface of the road."), GUILayout.Width(95) );
target.OOQQQDOD = EditorGUILayout.Slider(target.OOQQQDOD, 0, 1,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Bumpyness Offset"," The bumypness variation of the road."), GUILayout.Width(95) );
target.OOQQQDODOffset = EditorGUILayout.Slider(target.OOQQQDODOffset, 0, 1,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Bumpyness Density"," The bumypness density on the road."), GUILayout.Width(95) );
target.OOQQQDODLength = EditorGUILayout.Slider(target.OOQQQDODLength, 0.01, 1,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
}
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Resolution"," The resolution level of the road geometry."), GUILayout.Width(95) );
target.roadResolution = EditorGUILayout.IntSlider(target.roadResolution, 1, 10,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
if(target.objectType == 0){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" UV Mapping"," Use the slider to control texture uv mapping of the road geometry."), GUILayout.Width(95) );
target.tuw = EditorGUILayout.Slider(target.tuw, 1, 30,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Raise (cm)","Optionally increase this setting when parts of the terrain stick through the road geometry. It is recommended to adjust these areas using the terrain tools!"), GUILayout.Width(95) );
target.raise = EditorGUILayout.Slider(target.raise, 0, 100, GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
}else{
}
GUILayout.Space(5);
GUI.enabled = false;
if(target.applyTangents)GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Calculate Tangents", GUILayout.Width(175))){
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
GUI.enabled = true;
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Save Geometry", GUILayout.Width(175))){
target.ODCDDDOCDO();
Debug.Log("Road object geometry saved");
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Finalize Object", GUILayout.Width(175))){
var bflag = false;
for(i=0;i<target.ODODQQOD.Length;i++){
if(target.ODODQQOD[i]){
bflag = true;
break;
}
}
if(target.autoODODDQQO || target.sosBuild == true)bflag = false;
if(EditorUtility.DisplayDialog("Important!", "This will unlink the road from the EasyRoads3D editor object and the EasyRoads3D object will be destroyed!\n\nWould you like to continue?", "Yes", "No")){
if(bflag){
if(EditorUtility.DisplayDialog("Important!", "This object includes activated side objects that have not yet been build!\n\nAre you sure you would you like to continue?", "Yes", "No")){
bflag = false;
}
}
if(!bflag){
target.OQCODQCQOC.FinalizeObject(target.gameObject);
DestroyImmediate(target.gameObject);
}
}
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
}
EditorGUILayout.Space();
if(oldRoad != target.renderRoad || oldRoadResolution != target.roadResolution || oldRoadUV != target.tuw || oldRaise != target.raise || oldSegments != target.OdQODQOD || target.OOQQQDOD != oldOOQQQDOD || target.OOQQQDODOffset != oldOOQQQDODOffset || target.OOQQQDODLength != oldOOQQQDODLength){

target.OQQOCOQQOC();

}
GUILayout.Box(" Terrain Settings:", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
GUILayout.Space(5);
var oldApplySplatmap : boolean = target.applySplatmap;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Apply Splatmap"," When active, the road will be added to the terrain splatmap. The quality highly depends on the terrain Control Texture Resolution size."), GUILayout.Width(125) );
target.applySplatmap = EditorGUILayout.Toggle (target.applySplatmap);
EditorGUILayout.EndHorizontal();
if(target.applySplatmap){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Terrain texture", "This represents the terrain texture which will be used for the road spatmap."), GUILayout.Width(125) );
target.splatmapLayer = EditorGUILayout.IntPopup (target.splatmapLayer, target.ODODQOQO, target.ODODQOQOInt,   GUILayout.Width(120));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Expand"," Use this setting to increase the size of the splatmap."), GUILayout.Width(125) );
target.expand = EditorGUILayout.IntSlider(target.expand,0, 3, GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Smooth Level"," Use this setting to blur the road splatmap for smoother results."), GUILayout.Width(125) );
target.splatmapSmoothLevel = EditorGUILayout.IntSlider (target.splatmapSmoothLevel, 0, 3,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Offset x"," Moves the road splatmap in the x direction."), GUILayout.Width(125) );
target.offsetX = EditorGUILayout.IntField (target.offsetX, GUILayout.Width(50));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Offset y"," Moves the road splatmap in the y direction."), GUILayout.Width(125) );
target.offsetY= EditorGUILayout.IntField (target.offsetY, GUILayout.Width(50));
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Opacity","Use this setting to blend the road splatmap with the terrain splatmap."), GUILayout.Width(125) );
target.opacity = EditorGUILayout.Slider (target.opacity, 0, 1,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
GUILayout.Space(5);
GUI.enabled = target.OCDCOQDCDQ;
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Apply Changes", GUILayout.Width(175))){
target.OQCCOODCDO();

if(target.OOQDOOQQ)target.OQCCOODCDO();
target.OCDCOQDCDQ = false;
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
}
GUILayout.Space(5);
if(oldApplySplatmap != target.applySplatmap){
target.OQCCOODCDO();

if(target.OOQDOOQQ)target.OQCCOODCDO();
}
GUI.enabled = true;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Terrain Smoothing:", "This will smoothen the terrain near the surface edges according the below distance."), GUILayout.Width(175) );
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Edges (m)","This represents the smoothen distance."), GUILayout.Width(125) );
target.smoothDistance = EditorGUILayout.Slider (target.smoothDistance, 0, 5,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
GUILayout.Space(5);
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Update Edges", GUILayout.Width(175))){

#if UNITY_4_3

#else
Undo.RegisterUndo(OQCDOODQQQ.terrain.terrainData, "EasyRoads3D Terrain smooth");
#endif
target.OQCODQCQOC.OQQDCDQQDC(target.smoothDistance, 0);
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Surrounding (m)","This represents the smoothen distance."), GUILayout.Width(125) );
target.smoothSurDistance = EditorGUILayout.Slider (target.smoothSurDistance, 0, 15,  GUILayout.Width(175));
EditorGUILayout.EndHorizontal();
GUILayout.Space(5);
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Update Surrounding", GUILayout.Width(175))){

#if UNITY_4_3

#else
Undo.RegisterUndo(OQCDOODQQQ.terrain.terrainData, "EasyRoads3D Terrain smooth");
#endif
target.OQCODQCQOC.OQQDCDQQDC(target.smoothSurDistance, 1);
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();

GUILayout.Box(" Cam Fly Over", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));
GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("  Position", ""), GUILayout.Width(75) );
var sp : float = target.splinePos;
target.splinePos = EditorGUILayout.Slider(target.splinePos, 0, 0.9999);
EditorGUILayout.EndHorizontal();
if(sp != target.splinePos){
}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("  Height", ""), GUILayout.Width(75) );
sp = target.camHeight;
target.camHeight = EditorGUILayout.Slider(target.camHeight, 1, 10);
EditorGUILayout.EndHorizontal();
if(sp != target.camHeight){
}
GUI.enabled = true;
EditorGUILayout.Space();
}else if(target.ODDDQCOOQD == 4){
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();

GUILayout.Label(target.OQQOCCQOOC);
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
GUILayout.Label(" EasyRoads3D v"+target.version);
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();

GUILayout.Label(" Version Type: Free Version", GUILayout.Height(22));
if(GUILayout.Button ("i", GUILayout.Width(22))){
Application.OpenURL ("http://www.unityterraintools.com");
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Help", GUILayout.Width(225))){
Application.OpenURL ("http://www.unityterraintools.com/manual.php");
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
GUI.skin = origSkin;
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
GUILayout.Box("Check out the full version if you had like to take advantage of all the features including the built-in paramatric modeling tool", GUILayout.Width(250));
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();	
if(GUILayout.Button ("Purchase Full Version from website", GUILayout.Width(225))){
Application.OpenURL ("http://www.unityterraintools.com/store.php");
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Purchase from the Assetstore", GUILayout.Width(225))){
//	AssetStore.Open("http://u3d.as/content/anda-soft/easy-roads3d-pro/1Ch");
Application.OpenURL ("https://www.assetstore.unity3d.com/#/content/469");
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
GUILayout.Label(new GUIContent("  Newsletter Sign Up:",""), GUILayout.Width(155) );
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
GUILayout.Label(new GUIContent("  Name", ""), GUILayout.Width(75) );
target.uname = GUILayout.TextField(target.uname,  GUILayout.Width(150));
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
GUILayout.Label(new GUIContent("  Email", ""), GUILayout.Width(75) );
target.email = GUILayout.TextField(target.email,  GUILayout.Width(150));
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Submit", GUILayout.Width(225))){
EditorUtility.DisplayDialog("Newsletter Signup", OCDDOQCCDD0.NewsletterSignUp(target.uname, target.email), "Ok");
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
}else{
if(target.markers != target.OCCQOQDDDO.childCount){
target.ODDCCCQCOC();
}
EditorGUILayout.Space();
GUILayout.Box(" General Info", GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(20));

if(RoadObjectScript.objectStrings == null){
RoadObjectScript.objectStrings = new String[3];
RoadObjectScript.objectStrings[0] = "Road Object"; RoadObjectScript.objectStrings[1]="River Object";RoadObjectScript.objectStrings[2]="Procedural Mesh Object";
}
if(target.distance == "-1"){
var ar : String[]  = target.OQCODQCQOC.ODQQQDDDCQ(-1);
target.distance = ar[0];
}
EditorGUILayout.Space();
GUILayout.Label(" Object Type: " + RoadObjectScript.objectStrings[target.objectType]);
if(target.objectType == 0) GUILayout.Label(" Total Road Distance: " + target.distance.ToString() + " km");
}
EditorGUILayout.Space();
if (GUI.tooltip != "") GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 200, 40), GUI.tooltip);
if (GUI.changed)
{
target.OCDCOQDCDQ = true;
}
return markerMenuDisplay;
}
function OOCDDQQOOD(fwd: Vector3, targetDir: Vector3, up: Vector3) : float {
var perp: Vector3 = Vector3.Cross(fwd, targetDir);
var dir: float = Vector3.Dot(perp, up);
if (dir > 0.0) {
return 1.0;
} else if (dir < 0.0) {
return -1.0;
} else {
return 0.0;
}
}
function OnScene(){
var cEvent : Event = Event.current;
if(target.OODQCOQQQQ != -1  && Event.current.shift) target.OQDDCQCCDQ[target.OODQCOQQQQ] = true;
if(target.OQDDCQCCDQ == null || target.OQDDCQCCDQ.Length == 0){
target.OQDDCQCCDQ = new boolean[5];
target.OODQCQCDQO = new boolean[5];
}
if((cEvent.shift  && cEvent.type == EventType.mouseDown) || target.OQDDCQCCDQ[1])
{
var hit : RaycastHit;
var mPos : Vector2 = cEvent.mousePosition;
mPos.y = Screen.height - mPos.y - 40;
var ray : Ray = Camera.current.ScreenPointToRay(mPos);

if (Physics.Raycast (Camera.current.transform.position, ray.direction, hit, 3000))

{
if(target.OQDDCQCCDQ[0]){
go = target.OOOCOOQCOD(hit.point);
}
else if(target.OQDDCQCCDQ[1] && cEvent.type == EventType.mouseDown && cEvent.shift){

target.ODDCCQDCQC(hit.point, true);
}
else if(target.OQDDCQCCDQ[1]  && cEvent.shift) target.ODDCCQDCQC(hit.point, false);
else if(target.handleInsertFlag) target.handleInsertFlag = target.OQCODQCQOC.OOOCDCQDOC();
Selection.activeGameObject = target.obj.gameObject;
}
}
if(cEvent.control && cEvent.alt && cEvent.type == EventType.mouseDown){
mPos = cEvent.mousePosition;
mPos.y = Screen.height - mPos.y - 40;
ray = Camera.current.ScreenPointToRay(mPos);
if (Physics.Raycast (Camera.current.transform.position, ray.direction, hit, 3000))
{
if(hit.collider.gameObject.GetComponent(typeof(Terrain)) != null){

var t : Terrain = hit.collider.gameObject.GetComponent(typeof(Terrain));
for(i = 0; i < OQCDOODQQQ.terrains.Length; i++){

if(t == OQCDOODQQQ.terrains[i]){
if(OQCDOODQQQ.terrains.Length > 1)OQCDOODQQQ.selectedTerrain = i + 1;
else OQCDOODQQQ.selectedTerrain = i;
OQCDOODQQQ.OOODOQCOOQ();
EditorUtility.SetDirty (target);
}
}
}
}
}
if(target.OQCQDODCQQ != target.obj || target.obj.name != target.OQQODDQQOO){
target.OQCODQCQOC.OCQOQQOCCO();
target.OQCQDODCQQ = target.obj;
target.OQQODDQQOO = target.obj.name;
}
if(target.ODQDODOODQ != null){
target.OQCODQCQOC.OOOCDCQDOC();

}
if(target.transform.position != Vector3.zero) target.transform.position = Vector3.zero;
}
static function ODCDDDDDDQ() : boolean{

var flag : boolean = false;
var terrains : Terrain[]  = MonoBehaviour.FindObjectsOfType(typeof(Terrain));
for(terrain in terrains) {
if(!terrain.gameObject.GetComponent(EasyRoads3DTerrainID)){
var terrainscript : EasyRoads3DTerrainID = terrain.gameObject.AddComponent("EasyRoads3DTerrainID");
var id : String = UnityEngine.Random.Range(100000000,999999999).ToString();
terrainscript.terrainid = id;
flag = true;

path = Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + id;
if( !Directory.Exists(path)){
try{
Directory.CreateDirectory( path);
}
catch(e:System.Exception)
{
Debug.Log("Could not create directory: " + path + " " + e);
}
}
if(Directory.Exists(path)){


}
}
}
}
static function OOQCOOQQDO(target){
EditorUtility.DisplayProgressBar("Build EasyRoads3D Object","Initializing", 0);

scripts = FindObjectsOfType(typeof(RoadObjectScript));
var rObj : List.<Transform> = new List.<Transform>();
for(script in scripts) {
if(script.transform != target.transform) rObj.Add(script.transform);
}
if(target.ODODQOQO == null){
target.ODODQOQO = target.OQCODQCQOC.ODDDCOQDCC();
target.ODODQOQOInt = target.OQCODQCQOC.OCDDCDCCQD();
}
target.OQDODQOODQ(0.5f, true, false);


if(OQCDOODQQQ.selectedTerrain == null || target.OQCODQCQOC.terrain == null)OQCDOODQQQ.OQOOCOCDDQ();
target.OQCODQCQOC.OCDCCOCOQO();

OQCDOODQQQ.OOCCDDOCCO(target.OQCODQCQOC.terrain, Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder + "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OQQODDQQOO+"_splatMap");


OOQDDDCQDD.ODQODDOOCC(target.OQCODQCQOC.terrain, Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OQQODDQQOO+"_heightmap.backup");
var hitOCOCCDOQQD : List.<tPoint> = target.OQCODQCQOC.OQCCCOQCOO(Vector3.zero, target.raise, target.obj, target.OOQDOOQQ, rObj, target.handleVegetation);
var changeArr : List.<Vector3> = new List.<Vector3>();
var stepsf : float = Mathf.Floor(hitOCOCCDOQQD.Count / 10);
var steps : int = Mathf.RoundToInt(stepsf);


for(i = 0; i < 10;i++){
changeArr = target.OQCODQCQOC.OOCDDDQQQD(hitOCOCCDOQQD, i * steps, steps, changeArr);
EditorUtility.DisplayProgressBar("Build EasyRoads3D Object","Updating Terrain", i * 10);
}

changeArr = target.OQCODQCQOC.OOCDDDQQQD(hitOCOCCDOQQD, 10 * steps, hitOCOCCDOQQD.Count - (10 * steps), changeArr);
target.OQCODQCQOC.OQCQCCCOOQ(changeArr, rObj);
if(target.OQCODQCQOC.handleVegetation){
target.OQCODQCQOC.OCCDDQCOCO();

path = Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OCDCQCDDDO.OQQODDQQOO;
OOCDCOQDQC.OQDDODQOOQ(Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder);
OOCDCOQDQC.OQDDODQOOQ(Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain));
OOQDDDCQDD.ODCDDCDCQD(target.OQCODQCQOC.OQQDCCOCCO.ToArray(), target.OQCODQCQOC.ODQDDQQOQQ, path);
}

target.OQCCOODCDO();

target.OQCODQCQOC.ODODDCCCQC(target.transform, true);
target.OQCODQCQOC.OOCCQQODQO();
EditorUtility.ClearProgressBar();

}
function OCOCCCDDDO(target){
EditorUtility.DisplayProgressBar("Restore EasyRoads3D Object","Restoring terrain data", 0f);
target.OQDODQOODQ(target.geoResolution, false, false);

if(target.OQCODQCQOC.OQQQOODDCC != null && target.OQCODQCQOC != null){
if(target.OQCODQCQOC.editRestore && File.Exists(Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OQQODDQQOO+"_heightmap.backup")){
OOQDDDCQDD.OCDDQCDODO(target.OQCODQCQOC.terrain, Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OQQODDQQOO+"_heightmap.backup");
}else if(target.OQCODQCQOC.editRestore){
Debug.LogWarning("The original terrain heightmap data file was not found. If necessary you may restore the terrain data by using Undo or, if the terrain backup is up to date, through the EasyRoads3D Menu");
}
}

if(target.OQCODQCQOC != null){
target.OQCODQCQOC.OQCODQODOQ();
if(target.OQCODQCQOC.handleVegetation && target.OQCODQCQOC.editRestore){
if(target.OQCODQCQOC.OQQDCCOCCO != null){
if(target.OQCODQCQOC.OQQDCCOCCO.Count == 0){
// get treeData from file
path = Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OQQODDQQOO;
target.OQCODQCQOC.OQQDCCOCCO = OOQDDDCQDD.ODCCCCQQCQ(path);
}
if(target.OQCODQCQOC.OQQDCCOCCO != null) target.OQCODQCQOC.OCCOCQQDOO();

if(target.OQCODQCQOC.ODQDDQQOQQ.Count == 0){
// get detailData from file

path = Directory.GetCurrentDirectory() + OOCDCOQDQC.backupFolder+ "/" + OQCDOODQQQ.OCCQOOCOCQ(target.OQCODQCQOC.terrain) + "/"+target.OQCODQCQOC.OQQODDQQOO;
target.OQCODQCQOC.ODQDDQQOQQ = OOQDDDCQDD.ODQQDOCODQ(path);

}
if(target.OQCODQCQOC.ODQDDQQOQQ != null) target.OQCODQCQOC.OQCDDCCCOD();
}
}
}
target.ODODDDOO = false;

EditorUtility.ClearProgressBar();
}
}
