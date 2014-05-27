using UnityEngine;
using System.Collections;

public class DestroyObjScript : MonoBehaviour {
	//public int bulletLifetime;
	float myLR = 0.001f;

	float timer;
	
	void Update () {
		if(Time.timeScale!=0){
			timer += Time.deltaTime;

			//Linerenderer lifetime
			if(timer > myLR){
				Destroy(gameObject);
			}
		}
	}
}
