using UnityEngine;
using System.Collections;

public class Connect1C : Photon.MonoBehaviour
{

    /*
     * We want this script to automatically connect to Photon and to enter a room.
     * This will help speed up debugging in the next tutorials.
     * 
     * In Awake we connect to the Photon server(/cloud).
     * Via OnConnectedToPhoton(); we will either join an existing room (if any), otherwise create one. 
     */

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }


    void OnGUI()
    {
        //Check connection state..
        if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        {
            //We are currently disconnected
            GUILayout.Label("Connection status: " + PhotonNetwork.connectionStateDetailed);

            GUILayout.BeginVertical();
            if (GUILayout.Button("Connect"))
            {
                //Connect using the PUN wizard settings (Self-hosted server or Photon cloud)
                PhotonNetwork.ConnectUsingSettings("1.0");
            }
            GUILayout.EndVertical();
        }
        else
        {
            //We're connected!
            GUILayout.Label("Connection status: " + PhotonNetwork.connectionStateDetailed);
            if (PhotonNetwork.room != null)
            {
                GUILayout.Label("Room: " + PhotonNetwork.room.name);
                GUILayout.Label("Players: " + PhotonNetwork.room.playerCount + "/" + PhotonNetwork.room.maxPlayers);

            }
            else
            {
                GUILayout.Label("Not inside any room");
            }

            GUILayout.Label("Ping to server: " + PhotonNetwork.GetPing());
            
        }
    }

    private bool receivedRoomList = false;

    void OnConnectedToPhoton()
    {
        StartCoroutine(JoinOrCreateRoom());
    }

    void OnDisconnectedFromPhoton()
    {
        receivedRoomList = false;
    }

   



    
    /// <summary>
    /// Helper function to speed up our testing: 
    /// - after connecting to Photon, check for active rooms and join the first if possible
    /// - if no roomlist was found within 2 seconds: Create a room
    /// </summary>
    /// <returns></returns>
    IEnumerator JoinOrCreateRoom()
    {
        float timeOut = Time.time + 2;
        while (Time.time < timeOut && !receivedRoomList)
        {
            yield return 0;
        }
        //We still didn't join any room: create one
        if (PhotonNetwork.room == null){
            string roomName = "TestRoom"+Application.loadedLevelName;
            PhotonNetwork.CreateRoom(roomName, new RoomOptions() {maxPlayers = 4}, null);
        }
    }
    
    /// <summary>
    /// Not used in this script, just to show how list updates are handled.
    /// </summary>
    void OnReceivedRoomListUpdate()
    {
        Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);

        string wantedRoomName = "TestRoom" + Application.loadedLevelName;
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (room.name == wantedRoomName)
            {
                PhotonNetwork.JoinRoom(room.name);
                break;
            }
        }
        receivedRoomList = true;
    }
}
