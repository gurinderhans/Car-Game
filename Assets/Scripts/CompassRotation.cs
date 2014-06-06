using UnityEngine;
using System.Collections;

public class CompassRotation : MonoBehaviour {
	
	Transform myCam;
	
	
	void Start(){
		GameObject.Find ("Compass-Cam").GetComponent<Camera> ().farClipPlane = 1;
		//myCam = GameObject.Find ("DriveCamFinder").transform;
		myCam = Camera.main.transform;
	}
	
	void Update(){
		float yRot = myCam.transform.rotation.eulerAngles.y;
		//print (yRot);
		transform.rotation = Quaternion.identity;
		transform.Rotate (Vector3.up * yRot);
	}
}