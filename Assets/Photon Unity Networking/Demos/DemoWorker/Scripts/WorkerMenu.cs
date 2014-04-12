// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerMenu.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorkerMenu : MonoBehaviour
{
    private string roomName = "myRoom";

    private Vector2 scrollPos = Vector2.zero;

    private bool connectFailed = false;

    public static readonly string SceneNameMenu = "DemoWorker-Scene";

    public static readonly string SceneNameGame = "DemoWorkerGame-Scene";

    public void Awake()
    {
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;

        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
        {
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings("1.0");
        }

        // generate a name for this player, if none is assigned yet
        if (String.IsNullOrEmpty(PhotonNetwork.playerName))
        {
            PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
        }

        // if you wanted more debug out, turn this on:
        // PhotonNetwork.logLevel = NetworkLogLevel.Full;
    }

    public void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connecting)
            {
                GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress);
            }
            else
            {
                GUILayout.Label("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
            }
            
            if (this.connectFailed)
            {
                GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
                GUILayout.Label(String.Format("Server: {0}", new object[] {PhotonNetwork.ServerAddress}));
                GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID);
                
                if (GUILayout.Button("Try Again", GUILayout.Width(100)))
                {
                    this.connectFailed = false;
                    PhotonNetwork.ConnectUsingSettings("1.0");
                }
            }

            return;
        }


        GUI.skin.box.fontStyle = FontStyle.Bold;
        GUI.Box(new Rect((Screen.width - 400) / 2, (Screen.height - 350) / 2, 400, 300), "Join or Create a Room");
        GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 350) / 2, 400, 300));

        GUILayout.Space(25);

        // Player name
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player name:", GUILayout.Width(100));
        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
        GUILayout.Space(105);
        if (GUI.changed)
        {
            // Save name
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        // Join room by title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Roomname:", GUILayout.Width(100));
        this.roomName = GUILayout.TextField(this.roomName);
        
        if (GUILayout.Button("Create Room", GUILayout.Width(100)))
        {
            PhotonNetwork.CreateRoom(this.roomName, new RoomOptions() { maxPlayers = 10 }, null);
        }

        GUILayout.EndHorizontal();

        // Create a room (fails if exist!)
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //this.roomName = GUILayout.TextField(this.roomName);
        if (GUILayout.Button("Join Room", GUILayout.Width(100)))
        {
            PhotonNetwork.JoinRoom(this.roomName);
        }

        GUILayout.EndHorizontal();


        GUILayout.Space(15);

        // Join random room
        GUILayout.BeginHorizontal();

        GUILayout.Label(PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms.");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Join Random", GUILayout.Width(100)))
        {
            PhotonNetwork.JoinRandomRoom();
        }
        

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("Currently no games are available.");
            GUILayout.Label("Rooms will be listed here, when they become available.");
        }
        else
        {
            GUILayout.Label(PhotonNetwork.GetRoomList() + " currently available. Join either:");

            // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
                if (GUILayout.Button("Join"))
                {
                    PhotonNetwork.JoinRoom(roomInfo.name);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }

        GUILayout.EndArea();
    }

    // We have two options here: we either joined(by title, list or random) or created a room.
    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.LoadLevel(SceneNameGame);
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    public void OnFailedToConnectToPhoton(object parameters)
    {
        this.connectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
    }
}
