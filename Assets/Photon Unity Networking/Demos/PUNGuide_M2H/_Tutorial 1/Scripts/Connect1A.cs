using UnityEngine;
using System.Collections;

public class Connect1A : Photon.MonoBehaviour
{

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
            GUILayout.Label("Connection status: Disconnected");

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
            //We're connected or connecting!
            if (PhotonNetwork.connected)
            {
                GUILayout.Label("Connection status: Connected");
                GUILayout.Label("Ping to server: " + PhotonNetwork.GetPing());

                if (GUILayout.Button("Disconnect"))
                {
                    //Completely disconnect from the Photon server
                    PhotonNetwork.Disconnect();
                }
            }
            else
            {
                //Connecting...
                GUILayout.Label("Connection status: " + PhotonNetwork.connectionState);
            }
        }
    }



    // Note:
    // NONE of the functions below is of any use in this demo, the code below is for demonstration/reference only.
    // First ensure you understand the code in the OnGUI() function above.

    //
    // You can add any of the methods below to any of your scripts:
    //      they will be called when the corresponding event happens
    //

    // MAIN CALLBACKS
    void OnConnectedToPhoton()
    {
        Debug.Log("This client has connected to a server");
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.Log("This client has disconnected from the server");
    }

    void OnFailedToConnectToPhoton(ExitGames.Client.Photon.StatusCode status)
    {
        Debug.Log("Failed to connect to Photon: " + status);
    }

    

    //ROOM EVENTS

    void OnCreatedRoom()
    {
        Debug.Log("We have created a room.");
        //When creating a room, both OnCreatedRoom AND OnJoinedRoom will be called.
    }

    void OnJoinedRoom()
    {
        Debug.Log("We have joined a room.");
    }

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





    //PLAYER EVENTS
    void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("Player connected: " + player);
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player disconnected: " + player);

    }
    void OnMasterClientSwitched(PhotonPlayer newMaster)
    {
        Debug.Log("The old masterclient left, we have a new masterclient: " + newMaster);
    }

    //This is called only when the current gameobject has been Instantiated via PhotonNetwork.Instantiate
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("New object instantiated by " + info.sender);
    }

    //This is used only when observing this script via a PhotonView, more on this later.
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Custom code here (your code!)
    }


    /* 
     The last Networking function is the RPC.
     RPCs are custom functions that you'll have to define and call yourself.
     They allow you to send/receive any kind of information to one or more targets.
     
     The only thing that is different from any other method is the requirement to add [RPC] before the method.
 
     [RPC]
     void MyRPCMethod (){
        //do something
     }
    
    */
}
