using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour {
	
	public float speed;
	void LateUpdate () {
		if(Time.timeScale!=0){
			float cameraX = Camera.main.transform.eulerAngles.x;
			float cameraY = Camera.main.transform.eulerAngles.y;

			Vector3 vectorRotation;
			vectorRotation.x = cameraX;
			vectorRotation.y = cameraY;
			vectorRotation.z = 0;
			Quaternion targetRotation = Quaternion.Euler (vectorRotation);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
		}
	}
}