using UnityEngine;
using System.Collections;

public class DestroyObjScript : MonoBehaviour {
	//public int bulletLifetime;
	public float myLR = 0.05f;

	public float timer;
	
	void Update () {
		if(Time.timeScale!=0){
			timer += Time.deltaTime;
			//bullet destroy
			/*if(timer > bulletLifetime){
				//networkView.enabled = false;
				//Destroy(gameObject);
				//Network.Destroy(GetComponent<NetworkView>().viewID);
				//networkView.RPC("DestroyBullet", RPCMode.All);
				Destroy(gameObject);
			}*/

			//skidmarks lifetime
			if(timer > myLR){
				Destroy(gameObject);
			}
		}
	}
}
