// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainMenu.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using UnityEngine;

public class Connect1B : MonoBehaviour
{

    private string roomName = "myRoom";
    private Vector2 scrollPos = Vector2.zero;

    private bool connectFailed = false;

    private void Awake()
    {
        // Connect to the main photon server.
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("1.0");
        }


        //Load our name from PlayerPrefs
        PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
    }

    private void OnGUI()
    {

        if (!PhotonNetwork.connected)
        {
            GUI_Disconnected();
        }
        else
        {
            if (PhotonNetwork.room != null)
            {
                GUI_Connected_Room();
            }
            else
            {
                GUI_Connected_Lobby();
            }            
        }
    }

    void GUI_Disconnected()
    {
        if (PhotonNetwork.connecting)
        {
            GUILayout.Label("Connecting...");
        }
        else
        {
            GUILayout.Label("Not connected. Check console output. (" + PhotonNetwork.connectionState + ")");
        }

        if (this.connectFailed)
        {
            GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
            GUILayout.Label(string.Format("Server: {0}", PhotonNetwork.PhotonServerSettings.ServerAddress));
            GUILayout.Label(string.Format("AppId: {0}", PhotonNetwork.PhotonServerSettings.AppID));

            if (GUILayout.Button("Try Again", GUILayout.Width(100)))
            {
                this.connectFailed = false;
                PhotonNetwork.ConnectUsingSettings("1.0");
            }
        }
    }

    void GUI_Connected_Lobby()
    {
        GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

        GUILayout.Label("Main Menu");

        // Player name
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player name:", GUILayout.Width(150));
        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
        if (GUI.changed)
        {
            PlayerPrefs.SetString("playerName" + Application.platform, PhotonNetwork.playerName);
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(15);


        // Join room by title
        GUILayout.BeginHorizontal();
        GUILayout.Label("JOIN ROOM:", GUILayout.Width(150));
        this.roomName = GUILayout.TextField(this.roomName);
        if (GUILayout.Button("GO"))
        {
            PhotonNetwork.JoinRoom(this.roomName);
        }
        GUILayout.EndHorizontal();


        // Create a room (fails if already exists!)
        GUILayout.BeginHorizontal();
        GUILayout.Label("CREATE ROOM:", GUILayout.Width(150));
        this.roomName = GUILayout.TextField(this.roomName);
        if (GUILayout.Button("GO"))
        {
            PhotonNetwork.CreateRoom(this.roomName, new RoomOptions() { maxPlayers = 10}, null);
        }

        GUILayout.EndHorizontal();


        // Join random room (there must be at least 1 room)
        GUILayout.BeginHorizontal();
        GUILayout.Label("JOIN RANDOM ROOM:", GUILayout.Width(150));
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("..no games available...");
        }
        else
        {
            if (GUILayout.Button("GO"))
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(30);

        //Show a list of all current rooms
        GUILayout.Label("ROOM LISTING:");
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("..no games available..");
        }
        else
        {
            // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
                if (GUILayout.Button("JOIN"))
                {
                    PhotonNetwork.JoinRoom(game.name);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }

        GUILayout.EndArea();
    }

    void GUI_Connected_Room()
    {
        GUILayout.Label("We are connected to room: "+PhotonNetwork.room);
        GUILayout.Label("Players: ");
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            GUILayout.Label("ID: "+player.ID+" Name: "+player.name);
        }

        if (GUILayout.Button("Leave room"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }


    // We have two options here: we either joined(by title, list or random) or created a room.
    private void OnJoinedRoom()
    {
        Debug.Log("We have joined a room.");
        StartCoroutine(MoveToGameScene());
    }

    private void OnCreatedRoom()
    {
        Debug.Log("We have created a room.");
        //When creating a room, OnJoinedRoom is also called, so we don't have to do anything here.
    }


    private void OnFailedToConnectToPhoton(object parameters)
    {
        this.connectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters);
    }

    private IEnumerator MoveToGameScene()
    {
        //Wait for the 
        while (PhotonNetwork.room == null)
        {
            yield return 0;
        }

        Debug.LogWarning("Normally we would load the game scene right now.");
        /*
            PhotonNetwork.LoadLevel( LEVEL TO LOAD);
         */

    }







    //CALLBACKS - for debug info only

    // ROOMS

    void OnLeftRoom()
    {
        Debug.Log("This client has left a game room.");
    }

    void OnPhotonCreateRoomFailed()
    {
        Debug.Log("A CreateRoom call failed, most likely the room name is already in use.");
    }

    void OnPhotonJoinRoomFailed()
    {
        Debug.Log("A JoinRoom call failed, most likely the room name does not exist or is full.");
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("A JoinRandom room call failed, most likely there are no rooms available.");
    }

    // LOBBY EVENTS

    void OnJoinedLobby()
    {
        Debug.Log("We joined the lobby.");
    }

    void OnLeftLobby()
    {
        Debug.Log("We left the lobby.");
    }

    // ROOMLIST

    void OnReceivedRoomList()
    {
        Debug.Log("We received a new room list, total rooms: " + PhotonNetwork.GetRoomList().Length);
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
    }


}