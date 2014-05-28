using UnityEngine;
using System.Collections;

public class SmoothNetworkCharacter : MonoBehaviour {

	private Vector3 currentPlayerPos = Vector3.zero;
	private Quaternion currentPlayerRot = Quaternion.identity;

	private Vector3 correctPlayerPos = Vector3.zero;
	private Quaternion correctPlayerRot = Quaternion.identity;

	public float SmoothingDelay = 5;

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){

		if(stream.isWriting){//stuff in this code block will be sent over the server

			currentPlayerPos = transform.position;
			currentPlayerRot = transform.rotation;

			stream.Serialize(ref currentPlayerPos);
			stream.Serialize(ref currentPlayerRot);

		} else{
			//here we recieve the data
			stream.Serialize(ref currentPlayerPos);
			stream.Serialize(ref currentPlayerRot);

			correctPlayerPos = currentPlayerPos;
			correctPlayerRot = currentPlayerRot;

		}

	}

	public void Update(){
		if(!networkView.isMine){
			//update remote player
			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
		}
	}

}
