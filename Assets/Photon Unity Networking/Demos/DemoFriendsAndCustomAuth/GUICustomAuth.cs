using UnityEngine;
using System.Collections;

public class GUICustomAuth : MonoBehaviour
{
    public Rect GuiRect;
    
    private bool authFailed;
    private string authName = "usr";
    private string authToken = "usr";
    private string authDebugMessage = string.Empty;



    void Start()
    {
        GuiRect = new Rect(Screen.width / 4, 80, Screen.width / 2, Screen.height - 100);
    }


    public void OnJoinedLobby()
    {
        // for ease of use, this script simply deactivates itself on successful connect
        this.enabled = false;
    }


    /// <summary>
    /// This method is called when Custom Authentication is setup for your app but fails for any reasons.
    /// </summary>
    /// <remarks>
    /// Unless you setup a custom authentication service for your app (in the Dashboard), this won't be called.
    /// If authentication is successful, this method is not called but OnJoinedLobby, OnConnectedToMaster and the 
    /// others will be called.
    /// </remarks>
    /// <param name="debugMessage"></param>
    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        this.authDebugMessage = debugMessage;
        this.authFailed = true;
    }


    void OnGUI()
    {
        if (PhotonNetwork.connected)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
            return;
        }


        GUILayout.BeginArea(GuiRect);
        if (authFailed)
        {
            GUILayout.Label("Custom Authentication");
            GUILayout.Label("Failed. Debug Message (customizable in your service):");
            GUILayout.Label("'" + this.authDebugMessage + "'");
        }
        else
        {
            GUILayout.Label("Custom Authentication");
            GUILayout.Label("Photon Cloud can be setup to use an external service to verify players.");
            GUILayout.Label("By default, Photon Cloud allows anonymous connects. In the Dashboard, Custom Authentication can be made mandatory.");
            GUILayout.Label("Set PhotonNetwork.CustomAuthenticationValues before you call PhotonNetwork.ConnectUsingSetting().");
            GUILayout.Label("The demo service logs you in for: name == token.");
            GUILayout.Label("The script GUIFriendFinding will set a random username (independent from Custom Authentication).");
        }

        GUILayout.BeginHorizontal();
        this.authName = GUILayout.TextField(this.authName, GUILayout.Width(Screen.width / 4 - 5));
        GUILayout.FlexibleSpace();
        this.authToken = GUILayout.TextField(this.authToken, GUILayout.Width(Screen.width / 4 - 5));
        GUILayout.EndHorizontal();


        if (GUILayout.Button("Login with Custom Authentication"))
        {
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.SetAuthParameters(this.authName, this.authToken);
            PhotonNetwork.ConnectUsingSettings("1.0");
            this.authFailed = false;
        }
        if (GUILayout.Button("Skip Custom Authentication"))
        {
            PhotonNetwork.AuthValues = null;    // null by default but maybe set in a previous session.
            PhotonNetwork.ConnectUsingSettings("1.0");
            this.authFailed = false;
        }

        GUILayout.Space(8.0f);

        if (GUILayout.Button("Open Dashboard (for Setup)"))
        {
            Application.OpenURL("https://cloud.exitgames.com/dashboard");
        }
        if (GUILayout.Button("Open Custom Auth doc page"))
        {
            Application.OpenURL("http://doc.exitgames.com/photon-cloud/CustomAuthentication");
        }
        GUILayout.EndArea();
    }
}