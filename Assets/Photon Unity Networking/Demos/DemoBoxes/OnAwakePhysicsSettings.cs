using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
public class OnAwakePhysicsSettings : Photon.MonoBehaviour {

    // this tiny script "disables" the rigidbody for remotely controlled GameObjects (owned by someone else)
    void Awake()
    {
        if (!this.photonView.isMine)
        {
            Rigidbody attachedRigidbody = this.GetComponent<Rigidbody>();
            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = true;
            }
        }
    }
}
