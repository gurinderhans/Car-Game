using UnityEngine;
using System.Collections;

public class GunCameraMovement : MonoBehaviour {

	void LateUpdate(){
		if(Time.timeScale!=0){
			
			transform.position = GameObject.FindGameObjectWithTag ("Gun").transform.position;
			transform.Translate (-Vector3.forward * 25);
			transform.Translate(Vector3.up * 6);

			float angleX = transform.eulerAngles.x;
			float angleY = transform.eulerAngles.y;
			angleX -= Input.GetAxis ("Mouse Y") * 5f;
			angleY += Input.GetAxis ("Mouse X") * 5f;
			transform.rotation = Quaternion.Euler (angleX, angleY, 0);
		}
	}
}
