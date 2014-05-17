using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour {


	public float speed;
	//GameObject followCar;
	public GameObject guncamera;
	
	void Start(){
		//followCar = GameObject.Find ("whereShooterFollows");
		guncamera = GameObject.Find ("GunCamFollow");
	}
	
	void Update () {
		if(Time.timeScale!=0){

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