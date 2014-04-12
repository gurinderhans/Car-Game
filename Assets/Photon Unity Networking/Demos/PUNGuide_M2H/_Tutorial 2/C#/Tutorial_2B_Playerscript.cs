using UnityEngine;
using System.Collections;

public class Tutorial_2B_Playerscript : Photon.MonoBehaviour
{
   

    void Awake()
    {
        if (!photonView.isMine)
        {
            //We aren't the photonView owner, disable this script
            //RPC's and OnPhotonSerializeView will STILL get trough but we prevent Update from running
            enabled = false;
        }
    }

    void Update()
    {

        if (photonView.isMine)
        {
            //Only the owner can move the cube!	
            //(Ok this check is a bit overkill since we did already disable the script in Awake)		
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            float speed = 5;
            transform.Translate(speed * moveDirection * Time.deltaTime);
        }

    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //Executed on the owner of this PhotonView; 
            //The server sends it's position over the network
            
            stream.SendNext(transform.position);//"Encode" it, and send it

        }
        else
        {
            //Executed on the others; 
            //receive a position and set the object to it

            transform.position = (Vector3)stream.ReceiveNext();

        }
    }
}
