using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour {
	
	public float gunUp;
	public float gunBehind;
	
	public float speed;
	
	public Transform shoot_bullet_from;
	public bool showTurret;
	
	void Start(){
		transform.localPosition = Vector3.down*1.5f;
	}
	
	void Update(){
		//whole update function is just to hide and show gun and switch between cam controller scripts

		if(Input.GetKeyDown(KeyCode.Y)) showTurret=!showTurret;

		if(!showTurret){//drive mode
			Camera.main.GetComponent<GunCameraMovement>().enabled = false;
			Camera.main.GetComponent<DriveCamController>().enabled = true;//after this disable gun so it dosent show
			Camera.main.GetComponent<CrossHair>().enabled = false;

			transform.localPosition = Vector3.down *2;
		}
		if(showTurret){//shoot mode
			Camera.main.GetComponent<GunCameraMovement>().enabled = true;
			Camera.main.GetComponent<DriveCamController>().enabled = false;
			Camera.main.GetComponent<CrossHair>().enabled = true;

			transform.localPosition = Vector3.up *6;
		}
		
	}
	
	void LateUpdate () {
		if(Time.timeScale!=0){
			float cameraX = Camera.main.transform.eulerAngles.x;
			float cameraY = Camera.main.transform.eulerAngles.y;

			Vector3 vectorRotation;
			vectorRotation.x = cameraX;
			vectorRotation.y = cameraY;
			vectorRotation.z = 0;
			Quaternion targetRotation = Quaternion.Euler (vectorRotation);
			transform.rotation = targetRotation;
		}
	}
}