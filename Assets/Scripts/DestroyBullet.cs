using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	private int bulletLifetime = 2;
	
	private float timer;
	
	void Update () {
		if(Time.timeScale!=0){
			timer += Time.deltaTime;
			//bullet destroy
			if(timer > bulletLifetime){
				//networkView.enabled = false;
				//Destroy(gameObject);
				//Network.Destroy(GetComponent<NetworkView>().viewID);
				networkView.RPC("Destroy_bullet", RPCMode.All);
			}
		}
	}
	
	[RPC]
	void Destroy_bullet(){
		Network.RemoveRPCs (networkView.viewID);
		Destroy (gameObject);
	}

}
