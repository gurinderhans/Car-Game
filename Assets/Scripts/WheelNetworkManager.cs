using UnityEngine;
using System.Collections;

public class WheelNetworkManager : MonoBehaviour {

	private Quaternion currentPlayerRot = Quaternion.identity;
	private Quaternion correctPlayerRot = Quaternion.identity;
	

	public float SmoothingDelay = 5;
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		
		if(stream.isWriting){//stuff in this code block will be sent over the server
			currentPlayerRot = transform.rotation;
			stream.Serialize(ref currentPlayerRot);
		} else{
			//here we recieve the data
			stream.Serialize(ref currentPlayerRot);
			correctPlayerRot = currentPlayerRot;
		}
	}
	
	public void Update(){
		if(!networkView.isMine){
			//update remote player
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
		}
	}
}