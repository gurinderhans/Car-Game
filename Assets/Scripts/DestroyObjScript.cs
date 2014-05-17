using UnityEngine;
using System.Collections;

public class DestroyObjScript : MonoBehaviour {
	public float bulletLifetime = 10;
	public float skidMarkLifetime = 10;

	private float timer;
	
	void Update () {
		if(Time.timeScale!=0){
			timer += Time.deltaTime;
			//bullet destroy
			/*if(timer > bulletLifetime){
				Destroy(gameObject);
			}*/

			//skidmarks lifetime
			if(timer > skidMarkLifetime){
				Destroy(gameObject);
			}
		}
	}
}
