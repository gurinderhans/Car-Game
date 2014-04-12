using UnityEngine;
using System.Collections;

public class Tutorial_2A1 : Photon.MonoBehaviour
{
    void Update()
    {
        //This is only run on the first client; the masterclient
        if (PhotonNetwork.isMasterClient)
        {
            Vector3 moveDirection = new Vector3(-1 * Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            float speed = 5;
            transform.Translate(speed * moveDirection * Time.deltaTime);//now really move!
        }
    }
}