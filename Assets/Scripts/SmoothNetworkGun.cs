using UnityEngine;
using System.Collections;

public class SmoothNetworkGun : MonoBehaviour {

	private Quaternion currentGunRot = Quaternion.identity;
	
	private Quaternion correctGunRot = Quaternion.identity;
	
	public float SmoothingDelay = 5;
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		
		if(stream.isWriting){//stuff in this code block will be sent over the server
			
			currentGunRot = transform.rotation;
			
			stream.Serialize(ref currentGunRot);
			
		} else{
			//here we recieve the data for gun rotation and etc
			stream.Serialize(ref currentGunRot);
			
			correctGunRot = currentGunRot;
			
		}
		
	}
	
	public void Update(){
		if(!networkView.isMine){
			//update remote player
			transform.rotation = Quaternion.Lerp(transform.rotation, correctGunRot, Time.deltaTime * this.SmoothingDelay);
		}
	}
}
