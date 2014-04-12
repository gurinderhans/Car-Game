@CustomEditor(EasyRoads3DTerrainID)
class TerrainEditorScript extends Editor
{
function OnSceneGUI()
{
if(Event.current.shift && RoadObjectScript.OQCQDODCQQ != null) Selection.activeGameObject = RoadObjectScript.OQCQDODCQQ.gameObject;
else RoadObjectScript.OQCQDODCQQ = null;
}
}
