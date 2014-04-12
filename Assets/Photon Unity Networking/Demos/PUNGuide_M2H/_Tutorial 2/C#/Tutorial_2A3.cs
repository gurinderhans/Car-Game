using UnityEngine;
using System.Collections;

public class Tutorial_2A3 : Photon.MonoBehaviour
{

    /*
     *  Sending our movement via RPCs. This is bad habit though:
     *  something as constant as movement should be transferred via 
     *  OnSerializePhotonView by observing a script.
     * 
     */

    private Vector3 lastPosition;

    void Update()
    {

        if (PhotonNetwork.isMasterClient)
        {

            //LOCAL MOVEMENT			
            Vector3 moveDirection = new Vector3(-1 * Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            float speed = 5;
            transform.Translate(speed * moveDirection * Time.deltaTime);


            //SHARE POSITION VIA RPC
            if (Vector3.Distance(transform.position, lastPosition) >= 0.05f)
            {
                //Save some network bandwidth; only send a RPC when the position has moved more than 0.05f            
                lastPosition = transform.position;

                //Send the position Vector3 over to the others; in this case all clients
                photonView.RPC("SetPosition", PhotonTargets.Others, transform.position);
            }
        }

    }


    [RPC]
    void SetPosition(Vector3 newPos)
    {
        //In this case, this function is always ran on the Clients
        //The server requested all clients to run this function (line 25).

        transform.position = newPos;
    }
}

