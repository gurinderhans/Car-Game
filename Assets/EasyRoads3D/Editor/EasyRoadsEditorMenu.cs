using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using EasyRoads3D;
using EasyRoads3DEditor;
public class EasyRoadsEditorMenu : ScriptableObject {







	[MenuItem( "GameObject/Create Other/EasyRoads3D/New EasyRoads3D Object" )]
public static void  CreateEasyRoads3DObject ()
{
		RoadObjectScript[] scrpts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
		if(scrpts.Length >= 1){
			EditorUtility.DisplayDialog("Alert", "The Free version supports only one road editor object in the scene!\n\nPlease finalize or delete the current road object or upgrade to the full version before creating a new road object.", "Close");
			Selection.activeGameObject = scrpts[0].gameObject;
			return;
		}

Terrain[] terrains = (Terrain[]) FindObjectsOfType(typeof(Terrain));
if(terrains.Length == 0){
EditorUtility.DisplayDialog("Alert", "No Terrain objects found! EasyRoads3D objects requires a terrain object to interact with. Please create a Terrain object first", "Close");
return;
}



if(NewEasyRoads3D.instance == null){
NewEasyRoads3D window = (NewEasyRoads3D)ScriptableObject.CreateInstance(typeof(NewEasyRoads3D));
window.ShowUtility();
}



}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Back Up/Terrain Height Data" )]
public static void  GetTerrain ()
{
if(GetEasyRoads3DObjects()){

OOQDDDCQDD.ODQDQDDDOD(Selection.activeGameObject);
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Restore/Terrain Height Data" )]
public static void  SetTerrain ()
{
if(GetEasyRoads3DObjects()){

OOQDDDCQDD.OQOCDQQDQD(Selection.activeGameObject);
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Back Up/Terrain Splatmap Data" )]
public static void  OOCCDDOCCO()
{
if(GetEasyRoads3DObjects()){

OQCDOODQQQ.OOCCDDOCCO(Selection.activeGameObject);
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Restore/Terrain Splatmap Data" )]
public static void  ODQCOCCOOO ()
{
if(GetEasyRoads3DObjects()){
string path = "";
if(EditorUtility.DisplayDialog("Road Splatmap", "Would you like to merge the terrain splatmap(s) with a road splatmap?", "Yes", "No")){
path = EditorUtility.OpenFilePanel("Select png road splatmap texture", "", "png");
}


OQCDOODQQQ.OOCCQCQCQQ(true, 100, 4, path, Selection.activeGameObject);
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Back Up/Terrain Vegetation Data" )]
public static void  OCDDOCQQCO()
{
if(GetEasyRoads3DObjects()){

OOQDDDCQDD.OCDDOCQQCO(Selection.activeGameObject, null, "");
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Back Up/All Terrain Data" )]
public static void  GetAllData()
{
if(GetEasyRoads3DObjects()){

OOQDDDCQDD.ODQDQDDDOD(Selection.activeGameObject);
OQCDOODQQQ.OOCCDDOCCO(Selection.activeGameObject);
OOQDDDCQDD.OCDDOCQQCO(Selection.activeGameObject, null,"");
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Restore/Terrain Vegetation Data" )]
public static void  OOQOQQDQOQ()
{
if(GetEasyRoads3DObjects()){

OOQDDDCQDD.OOQOQQDQOQ(Selection.activeGameObject);
}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}
}
[MenuItem( "GameObject/Create Other/EasyRoads3D/Restore/All Terrain Data" )]
public static void  RestoreAllData()
{
if(GetEasyRoads3DObjects()){

OOQDDDCQDD.OQOCDQQDQD(Selection.activeGameObject);
OQCDOODQQQ.OOCCQCQCQQ(true, 100, 4, "", Selection.activeGameObject);
OOQDDDCQDD.OOQOQQDQOQ(Selection.activeGameObject);

}else{
EditorUtility.DisplayDialog("Alert", "No EasyRoads3D objects found! Terrain functions cannot be accessed!", "Close");
}


}
public static bool GetEasyRoads3DObjects(){
RoadObjectScript[] scripts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
bool flag = false;
foreach (RoadObjectScript script in scripts) {
if(script.OQCODQCQOC == null){
script.OCOOCQODQD(null, null, null);
}
flag = true;
}
return flag;
}
static private void OOQCOOQQDO(RoadObjectScript target){
EditorUtility.DisplayProgressBar("Build EasyRoads3D Object - " + target.gameObject.name,"Initializing", 0);

RoadObjectScript[] scripts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
List<Transform> rObj = new List<Transform>();


#if UNITY_4_3

#else
Undo.RegisterUndo(OQCDOODQQQ.terrain.terrainData, "EasyRoads3D Terrain leveling");
#endif
foreach(RoadObjectScript script in scripts) {
if(script.transform != target.transform) rObj.Add(script.transform);
}
if(target.ODODQOQO == null){
target.ODODQOQO = target.OQCODQCQOC.ODDDCOQDCC();
target.ODODQOQOInt = target.OQCODQCQOC.OCDDCDCCQD();
}
target.OQDODQOODQ(0.5f, true, false);

List<tPoint> hitOCOCCDOQQD = target.OQCODQCQOC.OQCCCOQCOO(Vector3.zero, target.raise, target.obj, target.OOQDOOQQ, rObj, target.handleVegetation);
List<Vector3> changeArr = new List<Vector3>();
float stepsf = Mathf.Floor(hitOCOCCDOQQD.Count / 10);
int steps = Mathf.RoundToInt(stepsf);
for(int i = 0; i < 10;i++){
changeArr = target.OQCODQCQOC.OOCDDDQQQD(hitOCOCCDOQQD, i * steps, steps, changeArr);
EditorUtility.DisplayProgressBar("Build EasyRoads3D Object - " + target.gameObject.name,"Updating Terrain", i * 10);
}

changeArr = target.OQCODQCQOC.OOCDDDQQQD(hitOCOCCDOQQD, 10 * steps, hitOCOCCDOQQD.Count - (10 * steps), changeArr);
target.OQCODQCQOC.OQCQCCCOOQ(changeArr, rObj);

target.OQCCOODCDO();
EditorUtility.ClearProgressBar();

}
private static void SetWaterScript(RoadObjectScript target){
for(int i = 0; i < target.OCCCCCOOOQ.Length; i++){
if(target.OQCODQCQOC.road.GetComponent(target.OCCCCCOOOQ[i]) != null && i != target.selectedWaterScript)DestroyImmediate(target.OQCODQCQOC.road.GetComponent(target.OCCCCCOOOQ[i]));
}
if(target.OCCCCCOOOQ[0] != "None Available!"  && target.OQCODQCQOC.road.GetComponent(target.OCCCCCOOOQ[target.selectedWaterScript]) == null){
target.OQCODQCQOC.road.AddComponent(target.OCCCCCOOOQ[target.selectedWaterScript]);

}
}
public static Vector3 ReadFile(string file)
{
Vector3 pos = Vector3.zero;
if(File.Exists(file)){
StreamReader streamReader = File.OpenText(file);
string line = streamReader.ReadLine();
line = line.Replace(",",".");
string[] lines = line.Split("\n"[0]);
string[] arr = lines[0].Split("|"[0]);
float.TryParse(arr[0],System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out pos.x);
float.TryParse(arr[1],System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out pos.y);
float.TryParse(arr[2],System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out pos.z);
}
return pos;
}
}
