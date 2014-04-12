using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;
using EasyRoads3D;

public class RoadObjectScript : MonoBehaviour {
static public string version = "";
public int objectType = 0;
public bool displayRoad = true;
public float roadWidth = 5.0f;
public float indent = 3.0f;
public float surrounding = 5.0f;
public float raise = 1.0f;
public float raiseMarkers = 0.5f;
public bool OOQDOOQQ = false;
public bool renderRoad = true;
public bool beveledRoad = false;
public bool applySplatmap = false;
public int splatmapLayer = 4;
public bool autoUpdate = true;
public float geoResolution = 5.0f;
public int roadResolution = 1;
public float tuw =  15.0f;
public int splatmapSmoothLevel;
public float opacity = 1.0f;
public int expand = 0;
public int offsetX = 0;
public int offsetY = 0;
private Material surfaceMaterial;
public float surfaceOpacity = 1.0f;
public float smoothDistance = 1.0f;
public float smoothSurDistance = 3.0f;
private bool handleInsertFlag;
public bool handleVegetation = true;
public float OCOCODCCDD = 2.0f;
public float ODDQODQCOO = 1f;
public int materialType = 0;
String[] materialStrings;
public string uname;
public string email;
private MarkerScript[] mSc;

private bool OODODDCDQQ;
private bool[] OQDDCQCCDQ = null;
private bool[] OODQCQCDQO = null;
public string[] ODODQCCDOC;
public string[] ODODQOQO;
public int[] ODODQOQOInt;
public int ODDDQCOOQD = -1;
public int OODQCOQQQQ = -1;
static public GUISkin OODCOCOOCC;
static public GUISkin OQQQCCOOOQ;
public bool OCDCOQDCDQ = false;
private Vector3 cPos;
private Vector3 ePos;
public bool ODQQCDQDQD;
static public Texture2D OQQOCCQOOC;
public int markers = 1;
public OCQCDCCDOC OQCODQCQOC;
private GameObject ODOQDQOO;
public bool OOCQCCQOQQ;
public bool doTerrain;
private Transform ODQDODOODQ = null;
public GameObject[] ODQDODOODQs;
private static string OCDQCCOCOC = null;
public Transform obj;
private string OQQODDQQOO;
public static string erInit = "";
static public Transform OQCQDODCQQ;
private RoadObjectScript ODOOCQDQCD;
public bool flyby;


private Vector3 pos;
private float fl;
private float oldfl;
private bool OQDDDDCCQC;
private bool OCOCQOQDQQ;
private bool OCQOOQCQQD;
public Transform OCCQOQDDDO;
public int OdQODQOD = 1;
public float OOQQQDOD = 0f;
public float OOQQQDODOffset = 0f;
public float OOQQQDODLength = 0f;
public bool ODODDDOO = false;
static public string[] ODOQDOQO;
static public string[] ODODOQQO; 
static public string[] ODODQOOQ;
public int ODQDOOQO = 0;
public string[] ODQQQQQO;  
public string[] ODODDQOO; 
public bool[] ODODQQOD; 
public int[] OOQQQOQO; 
public int ODOQOOQO = 0; 

public bool forceY = false;
public float yChange = 0f;
public float floorDepth = 2f;
public float waterLevel = 1.5f; 
public bool lockWaterLevel = true;
public float lastY = 0f;
public string distance = "0";
public string markerDisplayStr = "Hide Markers";
static public string[] objectStrings;
public string objectText = "Road";
public bool applyAnimation = false;
public float waveSize = 1.5f;
public float waveHeight = 0.15f;
public bool snapY = true;

private TextAnchor origAnchor;
public bool autoODODDQQO;
public Texture2D roadTexture;
public Texture2D roadMaterial;
public string[] OQDDOCDDOO;
public string[] OCCCCCOOOQ;
public int selectedWaterMaterial;
public int selectedWaterScript;
private bool doRestore = false;
public bool doFlyOver;
public static GameObject tracer;
public Camera goCam;
public float speed = 1f;
public float offset = 0f;
public bool camInit;
public GameObject customMesh = null;
static public bool disableFreeAlerts = true;
public bool multipleTerrains;
public bool editRestore = true;
public Material roadMaterialEdit;
static public int backupLocation = 0;
public string[] backupStrings = new string[2]{"Outside Assets folder path","Inside Assets folder path"};
public Vector3[] leftVecs = new Vector3[0];
public Vector3[] rightVecs = new Vector3[0];
public bool applyTangents = false;
public bool sosBuild = false;
public float splinePos = 0;
public float camHeight = 3;
public Vector3 splinePosV3 = Vector3.zero;
public bool iOS = false;
public void OCOOCQODQD(List<ODODDQQO> arr, String[] DOODQOQO, String[] OODDQOQO){

ODOODDDODD(transform, arr, DOODQOQO, OODDQOQO);
}
public void OQCDCQOQDO(MarkerScript markerScript){

ODQDODOODQ = markerScript.transform;



List<GameObject> tmp = new List<GameObject>();
for(int i=0;i<ODQDODOODQs.Length;i++){
if(ODQDODOODQs[i] != markerScript.gameObject)tmp.Add(ODQDODOODQs[i]);
}




tmp.Add(markerScript.gameObject);
ODQDODOODQs = tmp.ToArray();
ODQDODOODQ = markerScript.transform;

OQCODQCQOC.OQQDCQDOCD(ODQDODOODQ, ODQDODOODQs, markerScript.OQCQOQCQCC, markerScript.OQDOCODDQO, OCCQOQDDDO, out markerScript.ODQDODOODQs, out markerScript.trperc, ODQDODOODQs);

OODQCOQQQQ = -1;
}
public void ODCCCDCCDQ(MarkerScript markerScript){
if(markerScript.OQDOCODDQO != markerScript.ODOOQQOO || markerScript.OQDOCODDQO != markerScript.ODOOQQOO){
OQCODQCQOC.OQQDCQDOCD(ODQDODOODQ, ODQDODOODQs, markerScript.OQCQOQCQCC, markerScript.OQDOCODDQO, OCCQOQDDDO, out markerScript.ODQDODOODQs, out markerScript.trperc, ODQDODOODQs);
markerScript.ODQDOQOO = markerScript.OQCQOQCQCC;
markerScript.ODOOQQOO = markerScript.OQDOCODDQO;
}
if(ODOOCQDQCD.autoUpdate) OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
}
public void ResetMaterials(MarkerScript markerScript){
if(OQCODQCQOC != null)OQCODQCQOC.OQQDCQDOCD(ODQDODOODQ, ODQDODOODQs, markerScript.OQCQOQCQCC, markerScript.OQDOCODDQO, OCCQOQDDDO, out markerScript.ODQDODOODQs, out markerScript.trperc, ODQDODOODQs);
}
public void ODOODCODQC(MarkerScript markerScript){
if(markerScript.OQDOCODDQO != markerScript.ODOOQQOO){
OQCODQCQOC.OQQDCQDOCD(ODQDODOODQ, ODQDODOODQs, markerScript.OQCQOQCQCC, markerScript.OQDOCODDQO, OCCQOQDDDO, out markerScript.ODQDODOODQs, out markerScript.trperc, ODQDODOODQs);
markerScript.ODOOQQOO = markerScript.OQDOCODDQO;
}
OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
}
private void ODDQODCQDQ(string ctrl, MarkerScript markerScript){
int i = 0;
foreach(Transform tr in markerScript.ODQDODOODQs){
MarkerScript wsScript = (MarkerScript) tr.GetComponent<MarkerScript>();
if(ctrl == "rs") wsScript.LeftSurrounding(markerScript.rs - markerScript.ODOQQOOO, markerScript.trperc[i]);
else if(ctrl == "ls") wsScript.RightSurrounding(markerScript.ls - markerScript.DODOQQOO, markerScript.trperc[i]);
else if(ctrl == "ri") wsScript.LeftIndent(markerScript.ri - markerScript.OOQOQQOO, markerScript.trperc[i]);
else if(ctrl == "li") wsScript.RightIndent(markerScript.li - markerScript.ODODQQOO, markerScript.trperc[i]);
else if(ctrl == "rt") wsScript.LeftTilting(markerScript.rt - markerScript.ODDQODOO, markerScript.trperc[i]);
else if(ctrl == "lt") wsScript.RightTilting(markerScript.lt - markerScript.ODDOQOQQ, markerScript.trperc[i]);
else if(ctrl == "floorDepth") wsScript.FloorDepth(markerScript.floorDepth - markerScript.oldFloorDepth, markerScript.trperc[i]);
i++;
}
}
public void OQQOCCDODC(){
if(markers > 1) OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
}
public void ODOODDDODD(Transform tr, List<ODODDQQO> arr, String[] DOODQOQO, String[] OODDQOQO){
version = "2.5.4";
OODCOCOOCC = (GUISkin)Resources.Load("ER3DSkin", typeof(GUISkin));


OQQOCCQOOC = (Texture2D)Resources.Load("ER3DLogo", typeof(Texture2D));
if(RoadObjectScript.objectStrings == null){
RoadObjectScript.objectStrings = new string[3];
RoadObjectScript.objectStrings[0] = "Road Object"; RoadObjectScript.objectStrings[1]="River Object";RoadObjectScript.objectStrings[2]="Procedural Mesh Object";
}
obj = tr;
OQCODQCQOC = new OCQCDCCDOC();
ODOOCQDQCD = obj.GetComponent<RoadObjectScript>();
foreach(Transform child in obj){
if(child.name == "Markers") OCCQOQDDDO = child;
}
RoadObjectScript[] rscrpts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
OCQCDCCDOC.terrainList.Clear();
Terrain[] terrains = (Terrain[])FindObjectsOfType(typeof(Terrain));
foreach(Terrain terrain in terrains) {
Terrains t = new Terrains();
t.terrain = terrain;
if(!terrain.gameObject.GetComponent<EasyRoads3DTerrainID>()){
EasyRoads3DTerrainID terrainscript = (EasyRoads3DTerrainID)terrain.gameObject.AddComponent("EasyRoads3DTerrainID");
string id = UnityEngine.Random.Range(100000000,999999999).ToString();
terrainscript.terrainid = id;
t.id = id;
}else{
t.id = terrain.gameObject.GetComponent<EasyRoads3DTerrainID>().terrainid;
}
OQCODQCQOC.OQOOCOCDDQ(t);
}
OQCDOODQQQ.OQOOCOCDDQ();
if(roadMaterialEdit == null){
roadMaterialEdit = (Material)Resources.Load("materials/roadMaterialEdit", typeof(Material));
}
if(objectType == 0 && GameObject.Find(gameObject.name + "/road") == null){
GameObject road = new GameObject("road");
road.transform.parent = transform;
}

OQCODQCQOC.OQQDOCCQCD(obj, OCDQCCOCOC, ODOOCQDQCD.roadWidth, surfaceOpacity, out ODQQCDQDQD, out indent, applyAnimation, waveSize, waveHeight);
OQCODQCQOC.ODDQODQCOO = ODDQODQCOO;
OQCODQCQOC.OCOCODCCDD = OCOCODCCDD;
OQCODQCQOC.OdQODQOD = OdQODQOD + 1;
OQCODQCQOC.OOQQQDOD = OOQQQDOD;
OQCODQCQOC.OOQQQDODOffset = OOQQQDODOffset;
OQCODQCQOC.OOQQQDODLength = OOQQQDODLength;
OQCODQCQOC.objectType = objectType;
OQCODQCQOC.snapY = snapY;
OQCODQCQOC.terrainRendered = OOCQCCQOQQ;
OQCODQCQOC.handleVegetation = handleVegetation;
OQCODQCQOC.raise = raise;
OQCODQCQOC.roadResolution = roadResolution;
OQCODQCQOC.multipleTerrains = multipleTerrains;
OQCODQCQOC.editRestore = editRestore;
OQCODQCQOC.roadMaterialEdit = roadMaterialEdit;
OQCODQCQOC.renderRoad = renderRoad;
OQCODQCQOC.rscrpts = rscrpts.Length;

if(backupLocation == 0)OOCDCOQDQC.backupFolder = "/EasyRoads3D";
else OOCDCOQDQC.backupFolder =  "/Assets/EasyRoads3D/backups";

ODODQOQO = OQCODQCQOC.ODDDCOQDCC();
ODODQOQOInt = OQCODQCQOC.OCDDCDCCQD();


if(OOCQCCQOQQ){




doRestore = true;
}


ODDCCCQCOC();

if(arr != null || ODODQOOQ == null) OOCQQQQQQQ(arr, DOODQOQO, OODDQOQO);


if(doRestore) return;
}
public void UpdateBackupFolder(){
}
public void OODODDQDOQ(){
if(!ODODDDOO || objectType == 2){
if(OQDDCQCCDQ != null){
for(int i = 0; i < OQDDCQCCDQ.Length; i++){
OQDDCQCCDQ[i] = false;
OODQCQCDQO[i] = false;
}
}
}
}

public void OOOCOOQCOD(Vector3 pos){


if(!displayRoad){
displayRoad = true;
OQCODQCQOC.OQOCOCCQOC(displayRoad, OCCQOQDDDO);
}
pos.y += ODOOCQDQCD.raiseMarkers;
if(forceY && ODOQDQOO != null){
float dist = Vector3.Distance(pos, ODOQDQOO.transform.position);
pos.y = ODOQDQOO.transform.position.y + (yChange * (dist / 100f));
}else if(forceY && markers == 0) lastY = pos.y;
GameObject go = null;
if(ODOQDQOO != null) go = (GameObject)Instantiate(ODOQDQOO);
else go = (GameObject)Instantiate(Resources.Load("marker", typeof(GameObject)));
Transform newnode = go.transform;
newnode.position = pos;
newnode.parent = OCCQOQDDDO;
markers++;
string n;
if(markers < 10) n = "Marker000" + markers.ToString();
else if (markers < 100) n = "Marker00" + markers.ToString();
else n = "Marker0" + markers.ToString();
newnode.gameObject.name = n;
MarkerScript scr = newnode.GetComponent<MarkerScript>();
scr.ODQQCDQDQD = false;
scr.objectScript = obj.GetComponent<RoadObjectScript>();
if(ODOQDQOO == null){
scr.waterLevel = ODOOCQDQCD.waterLevel;
scr.floorDepth = ODOOCQDQCD.floorDepth;
scr.ri = ODOOCQDQCD.indent;
scr.li = ODOOCQDQCD.indent;
scr.rs = ODOOCQDQCD.surrounding;
scr.ls = ODOOCQDQCD.surrounding;
scr.tension = 0.5f;
if(objectType == 1){

pos.y -= waterLevel;
newnode.position = pos;
}
}
if(objectType == 2){
#if UNITY_3_5
if(scr.surface != null)scr.surface.gameObject.active = false;
#else
if(scr.surface != null)scr.surface.gameObject.SetActive(false);
#endif
}
ODOQDQOO = newnode.gameObject;
if(markers > 1){
OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
if(materialType == 0){

OQCODQCQOC.ODODDDCCOQ(materialType);

}
}
}
public void OQDODQOODQ(float geo, bool renderMode, bool camMode){
OQCODQCQOC.OQQOODDODO.Clear();
int ii = 0;
ODQDQDQCDQ k;
foreach(Transform child  in obj)
{
if(child.name == "Markers"){
foreach(Transform marker   in child) {
MarkerScript markerScript = marker.GetComponent<MarkerScript>();
markerScript.objectScript = obj.GetComponent<RoadObjectScript>();
if(!markerScript.ODQQCDQDQD) markerScript.ODQQCDQDQD = OQCODQCQOC.OOODQCODOC(marker);
k  = new ODQDQDQCDQ();
k.position = marker.position;
k.num = OQCODQCQOC.OQQOODDODO.Count;
k.object1 = marker;
k.object2 = markerScript.surface;
k.tension = markerScript.tension;
k.ri = markerScript.ri;
if(k.ri < 1)k.ri = 1f;
k.li =markerScript.li;
if(k.li < 1)k.li = 1f;
k.rt = markerScript.rt;
k.lt = markerScript.lt;
k.rs = markerScript.rs;
if(k.rs < 1)k.rs = 1f;
k.OQCCOQQQQO = markerScript.rs;
k.ls = markerScript.ls;
if(k.ls < 1)k.ls = 1f;
k.OQQOOODQCC = markerScript.ls;
k.renderFlag = markerScript.bridgeObject;
k.OCOCCCCCQQ = markerScript.distHeights;
k.newSegment = markerScript.newSegment;
k.tunnelFlag = markerScript.tunnelFlag;
k.floorDepth = markerScript.floorDepth;
k.waterLevel = waterLevel;
k.lockWaterLevel = markerScript.lockWaterLevel;
k.sharpCorner = markerScript.sharpCorner;
k.OCDQOQOCOC = OQCODQCQOC;
markerScript.markerNum = ii;
markerScript.distance = "-1";
markerScript.OCOCCDDCOD = "-1";
OQCODQCQOC.OQQOODDODO.Add(k);
ii++;
}
}
}
distance = "-1";

OQCODQCQOC.OOQCQCQOCO = ODOOCQDQCD.roadWidth;

OQCODQCQOC.OQODCDQQDC(geo, obj, ODOOCQDQCD.OOQDOOQQ, renderMode, camMode, objectType);
if(OQCODQCQOC.leftVecs.Count > 0){
leftVecs = OQCODQCQOC.leftVecs.ToArray();
rightVecs = OQCODQCQOC.rightVecs.ToArray();
}
}
public void StartCam(){

OQDODQOODQ(0.5f, false, true);

}
public void ODDCCCQCOC(){
int i = 0;
foreach(Transform child  in obj)
{
if(child.name == "Markers"){
i = 1;
string n;
foreach(Transform marker in child) {
if(i < 10) n = "Marker000" + i.ToString();
else if (i < 100) n = "Marker00" + i.ToString();
else n = "Marker0" + i.ToString();
marker.name = n;
ODOQDQOO = marker.gameObject;
i++;
}
}
}
markers = i - 1;

OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
}
public List<Transform> RebuildObjs(){
RoadObjectScript[] scripts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
List<Transform> rObj = new List<Transform>();
foreach (RoadObjectScript script in scripts) {
if(script.transform != transform) rObj.Add(script.transform);
}
return rObj;
}
public void RestoreTerrain1(){

OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
if(OQCODQCQOC != null) OQCODQCQOC.OQCODQODOQ();
ODODDDOO = false;
}
public void OQCCOODCDO(){
OQCODQCQOC.OQCCOODCDO(ODOOCQDQCD.applySplatmap, ODOOCQDQCD.splatmapSmoothLevel, ODOOCQDQCD.renderRoad, ODOOCQDQCD.tuw, ODOOCQDQCD.roadResolution, ODOOCQDQCD.raise, ODOOCQDQCD.opacity, ODOOCQDQCD.expand, ODOOCQDQCD.offsetX, ODOOCQDQCD.offsetY, ODOOCQDQCD.beveledRoad, ODOOCQDQCD.splatmapLayer, ODOOCQDQCD.OdQODQOD, OOQQQDOD, OOQQQDODOffset, OOQQQDODLength);
}
public void OQQOCOQQOC(){
OQCODQCQOC.OQQOCOQQOC(ODOOCQDQCD.renderRoad, ODOOCQDQCD.tuw, ODOOCQDQCD.roadResolution, ODOOCQDQCD.raise, ODOOCQDQCD.beveledRoad, ODOOCQDQCD.OdQODQOD, OOQQQDOD, OOQQQDODOffset, OOQQQDODLength);
}
public void ODDCCQDCQC(Vector3 pos, bool doInsert){


if(!displayRoad){
displayRoad = true;
OQCODQCQOC.OQOCOCCQOC(displayRoad, OCCQOQDDDO);
}

int first = -1;
int second = -1;
float dist1 = 10000;
float dist2 = 10000;
Vector3 newpos = pos;
ODQDQDQCDQ k;
ODQDQDQCDQ k1 = (ODQDQDQCDQ)OQCODQCQOC.OQQOODDODO[0];
ODQDQDQCDQ k2 = (ODQDQDQCDQ)OQCODQCQOC.OQQOODDODO[1];

OQCODQCQOC.OOQQDCQQCQ(pos, out first, out second, out dist1, out dist2, out k1, out k2, out newpos);
pos = newpos;
if(doInsert && first >= 0 && second >= 0){
if(ODOOCQDQCD.OOQDOOQQ && second == OQCODQCQOC.OQQOODDODO.Count - 1){
OOOCOOQCOD(pos);
}else{
k = (ODQDQDQCDQ)OQCODQCQOC.OQQOODDODO[second];
string name = k.object1.name;
string n;
int j = second + 2;
for(int i = second; i < OQCODQCQOC.OQQOODDODO.Count - 1; i++){
k = (ODQDQDQCDQ)OQCODQCQOC.OQQOODDODO[i];
if(j < 10) n = "Marker000" + j.ToString();
else if (j < 100) n = "Marker00" + j.ToString();
else n = "Marker0" + j.ToString();
k.object1.name = n;
j++;
}
k = (ODQDQDQCDQ)OQCODQCQOC.OQQOODDODO[first];
Transform newnode = (Transform)Instantiate(k.object1.transform, pos, k.object1.rotation);
newnode.gameObject.name = name;
newnode.parent = OCCQOQDDDO;
MarkerScript scr = newnode.GetComponent<MarkerScript>();
scr.ODQQCDQDQD = false;
float	totalDist = dist1 + dist2;
float perc1 = dist1 / totalDist;
float paramDif = k1.ri - k2.ri;
scr.ri = k1.ri - (paramDif * perc1);
paramDif = k1.li - k2.li;
scr.li = k1.li - (paramDif * perc1);
paramDif = k1.rt - k2.rt;
scr.rt = k1.rt - (paramDif * perc1);
paramDif = k1.lt - k2.lt;
scr.lt = k1.lt - (paramDif * perc1);
paramDif = k1.rs - k2.rs;
scr.rs = k1.rs - (paramDif * perc1);
paramDif = k1.ls - k2.ls;
scr.ls = k1.ls - (paramDif * perc1);
OQDODQOODQ(ODOOCQDQCD.geoResolution, false, false);
if(materialType == 0)OQCODQCQOC.ODODDDCCOQ(materialType);
#if UNITY_3_5
if(objectType == 2) scr.surface.gameObject.active = false;
#else
if(objectType == 2) scr.surface.gameObject.SetActive(false);
#endif
}
}
ODDCCCQCOC();
}
public void OQQDCOQODO(){

DestroyImmediate(ODOOCQDQCD.ODQDODOODQ.gameObject);
ODQDODOODQ = null;
ODDCCCQCOC();
}
public void ODCDDDOCDO(){
}

public List<SideObjectParams> ODDDDCDDOC(){
		return null;
}
public void OQDCCDDCDQ(){
}
public void OOCQQQQQQQ(List<ODODDQQO> arr, String[] DOODQOQO, String[] OODDQOQO){
}
public bool CheckWaterHeights(){
if(OQCDOODQQQ.terrain == null) return false;
bool flag = true;

float y = OQCDOODQQQ.terrain.transform.position.y;
foreach(Transform child  in obj) {
if(child.name == "Markers"){
foreach(Transform marker  in child) {
//MarkerScript markerScript = marker.GetComponent<MarkerScript>();
if(marker.position.y - y <= 0.1f) flag = false;
}
}
}
return flag;
}
}
