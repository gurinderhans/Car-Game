using UnityEngine;
using System.Collections;

public class MenuScript : Photon.MonoBehaviour
{


    void OnGUI()
    {

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 225, 0, 450, Screen.height));

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Select a scene");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();


        GUILayout.BeginVertical();
        GUILayout.Label("In this package:");
        GUILayout.Space(10);
        if (GUILayout.Button("Tutorial 1A - Connect"))
        {
            Application.LoadLevel("Tutorial_1A");
        }
        if (GUILayout.Button("Tutorial 1B - Connect"))
        {
            Application.LoadLevel("Tutorial_1B");
        }
        if (GUILayout.Button("Tutorial 1C - Connect"))
        {
            Application.LoadLevel("Tutorial_1C");
        }
        GUILayout.Space(10);

        if (GUILayout.Button("Tutorial 2A1 - Observe transform"))
        {
            Application.LoadLevel("Tutorial_2A1");
        }
        if (GUILayout.Button("Tutorial 2A2 - Observe code"))
        {
            Application.LoadLevel("Tutorial_2A2");
        }
        if (GUILayout.Button("Tutorial 2A3 - RPC"))
        {
            Application.LoadLevel("Tutorial_2A3");
        }
        if (GUILayout.Button("Tutorial 2B - Instantiating"))
        {
            Application.LoadLevel("Tutorial_2B");
        }

        GUILayout.Space(10);

        GUILayout.EndVertical();

        GUILayout.Space(30);

        GUILayout.BeginVertical();
        GUILayout.Label("In the full guide:");
        GUILayout.Space(10);

        if (GUILayout.Button("Tutorial 3 - Authoritative server"))
        {
            Application.LoadLevel("Tutorial_3");
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Tutorial 4 - Allocate PhotonViews"))
        {
            Application.LoadLevel("Tutorial_4");
        }
        if (GUILayout.Button("Example 1 - Chat"))
        {
            Application.LoadLevel("Example1_Chat");
        }
        if (GUILayout.Button("Example 2 - Masterserver"))
        {
            Application.LoadLevel("Example2_menu");
        }
        if (GUILayout.Button("Example 3 - Lobby"))
        {
            Application.LoadLevel("Example3_lobbymenu"); ;
        }
        if (GUILayout.Button("Example 4 - FPS game"))
        {
            Application.LoadLevel("Example4_Menu");
        }
        if (GUILayout.Button("Example 5 - Auto matchmaking"))
        {
            Application.LoadLevel("Example5_Game");
        }
        GUILayout.EndVertical();


        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();

    }
}