using UnityEngine;
using System.Collections;

public class Tutorial_2A2 : Photon.MonoBehaviour
{

    /// <summary>
    /// LOCAL movement only (not networked)
    /// </summary>

    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            //Only the MasterClient can move the cube!			
            Vector3 moveDirection = new Vector3(-1 * Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            float speed = 5;
            transform.Translate(speed * moveDirection * Time.deltaTime);
        }

    }

    /// <summary>
    /// The masterclient is the owner of this PhotonView, so it automatically is the WRITER (send)
    /// Other clients can only READ (receive)
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //Executed on the owner of the PhotonView; in this case the MasterClient
            //The server sends it's position over the network

            stream.SendNext(transform.position);//"Encode" it, and send it

            /*
            stream.SendNext(Input.GetButton ("Jump"));
            */

        }
        else
        {
            //Executed on the others; in this case the Clients
            //The clients receive a position and use it

            transform.position = (Vector3)stream.ReceiveNext();

            /*
            bool jumpBoolean = (bool) stream.ReceiveNext();
            if(jumpBoolean){
                Debug.Log(We are jumping");
            }
            */
        }
    }
}

