using System.IO;
using UnityEngine;
using UnityEditor;

using System.Collections;

public class FixBuildSettings : MonoBehaviour
{

    [MenuItem("PUN Guide/Reset build settings")]
    static void FixBSet()
    {
        //
        //  SET SCENES
        //

        if (!EditorUtility.DisplayDialog("Resetting build settings", "Can the current build settings be overwritten with the scenes for the PUN guide?", "OK", "No, cancel"))
            return;

        // find path of pun guide
        string[] tempPaths = Directory.GetDirectories(Application.dataPath, "PUNGuide_M2H", SearchOption.AllDirectories);
        if (tempPaths == null || tempPaths.Length != 1)
        {
            return;
        }

        // find scenes of guide
        string guidePath = tempPaths[0];
        tempPaths = Directory.GetFiles(guidePath, "*.unity", SearchOption.AllDirectories);

        if (tempPaths == null || tempPaths.Length == 0)
        {
            return;
        }

        // add found guide scenes to build settings
        int mainSceneIndex = 0;
        EditorBuildSettingsScene[] sceneAr = new EditorBuildSettingsScene[tempPaths.Length];
        for (int i = 0; i < tempPaths.Length; i++)
        {
            //Debug.Log(tempPaths[i]);
            string path = tempPaths[i].Substring(Application.dataPath.Length - "Assets".Length);
            path = path.Replace('\\', '/');
            if (mainSceneIndex == 0 && path.Contains("_Main"))
            {
                mainSceneIndex = i;
            }
            //Debug.Log(path);
            sceneAr[i] = new EditorBuildSettingsScene(path, true);
        }
        
        EditorBuildSettings.scenes = sceneAr;
        EditorApplication.OpenScene(sceneAr[mainSceneIndex].path);

        Debug.Log("PUN Guide: reset project build settings.");
    }
}