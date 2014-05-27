using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	float destroyBullet = 10f;
	
	float timer;
	
	void Update () {
		if(Time.timeScale!=0){
			timer += Time.deltaTime;
			
			//Linerenderer lifetime
			if(timer > destroyBullet){
				Destroy(gameObject);
			}
		}
	}

}
