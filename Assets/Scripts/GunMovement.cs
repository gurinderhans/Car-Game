using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour {

	public float gunUp;
	public float gunBehind;

	public float speed;
	public GameObject guncamera;

	public Transform shoot_bullet_from;
	
	void Start(){
		guncamera = GameObject.Find ("GunCamFollow");//child object inside gunCamera object which the gun follows
	}

	void Update(){
		//print (this.transform.gameObject.name);

	}

	void LateUpdate () {
		if(Time.timeScale!=0){

			if(this.transform.gameObject.name == "gun TBS 001C"){
				print ("gun TBS 001C");
				gunUp = 3f;
				gunBehind = 7f;
			} else if(this.transform.gameObject.name == "Khari Cupid_26208_assignsubmission_file_turret"){
				print ("Khari Cupid_26208_assignsubmission_file_turret");
				gunUp = 3.1f;
				gunBehind = 4.5f;
			}

			float cameraX = guncamera.transform.eulerAngles.x;
			float cameraY = guncamera.transform.eulerAngles.y;
			//print ("CameraX: "+cameraX+" CameraY:"+cameraY);
			Vector3 vectorRotation;
			vectorRotation.x = cameraX;
			vectorRotation.y = cameraY;
			vectorRotation.z = 0;
			Quaternion targetRotation = Quaternion.Euler (vectorRotation);
			//transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed);
			transform.rotation = targetRotation;

		}
	}


}