using UnityEngine;
using System.Collections;

public class Tutorial_2B_Spawnscript : Photon.MonoBehaviour
{
   

    public Transform playerPrefab;


    void OnJoinedRoom()
    {
        Spawnplayer();
    }

    void Spawnplayer()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-3,3),0,Random.Range(-3,3));
        PhotonNetwork.Instantiate(playerPrefab.name, pos, transform.rotation, 0);
    }


    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Clean up after player " + player);
  
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.Log("Clean up a bit after server quit");
        
        /* 
        * To reset the scene we'll just reload it:
        */
        Application.LoadLevel(Application.loadedLevel);
    }

}