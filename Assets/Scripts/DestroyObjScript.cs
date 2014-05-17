using UnityEngine;
using System.Collections;

public class DestroyObjScript : MonoBehaviour {
	public int bulletLifetime;
	public int skidMarkLifetime;

	private float timer;
	
	void Update () {
		if(Time.timeScale!=0){
			timer += Time.deltaTime;
			//bullet destroy
			if(timer > bulletLifetime){
				networkView.enabled = false;
				Destroy(gameObject);
				//Network.Destroy(GetComponent<NetworkView>().viewID);
			}

			//skidmarks lifetime
			if(timer > skidMarkLifetime){
				Destroy(gameObject);
			}
		}
	}
}
