using UnityEngine;
using System.Collections;

public class SmoothNetworkCharacter : Photon.MonoBehaviour {
	public float SmoothingDelay = 5;
	//private Transform wheelsd;
	//public FLWheel flwheelScript;


	public void Start(){
		//wheelsd = GameObject.Find("FLWheel").transform;

		//wheelsd = GameObject.Find("_SCRIPTS").GetComponent<NetworkManager>().wheelFL;
	}

	public void Awake()
	{
		if (this.photonView == null || this.photonView.observed != this)
		{
			Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
		}
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation); 
			stream.SendNext(rigidbody.velocity);
			//stream.SendNext(this.GetComponent<CarController>().wheelFLTrans.rotation);
			//stream.SendNext(wheelsd.rotation);
			//stream.SendNext(GetComponentInChildren(FLWheel));
		}
		else
		{
			//Network player, receive data
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
			correctPlayerVeloc = (Vector3)stream.ReceiveNext();
			//wheelFL = (Quaternion)stream.ReceiveNext();
		}
	}
	
	private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private Vector3 correctPlayerVeloc;
	//private Quaternion wheelFL;

	public void Update()
	{
		if (!photonView.isMine)
		{
			//Update remote player (smooth this, this looks good, at the cost of some accuracy)
			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, correctPlayerVeloc, Time.deltaTime * this.SmoothingDelay);
			//this.GetComponent<CarController>().wheelFront.z = Mathf.Lerp(this.GetComponent<CarController>().wheelFront.z, wheelFL.z, Time.deltaTime * this.SmoothingDelay);
			//print (wheelFL);
		} else{
			//print (wheelFL);
		}
	}
}
