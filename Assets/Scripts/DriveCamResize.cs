using UnityEngine;
using System.Collections;

public class DriveCamResize : MonoBehaviour {

	Rect smallScreen=new Rect(0.65f,0.55f,0.30f,0.40f);
	bool driveMode=true;
	void Update () {
		//cameraMovement scriptShootCam = shootCam.gameObject.GetComponent<cameraMovement> ();
		if(Input.GetKeyDown(KeyCode.Y)){
			if(driveMode){
				camera.rect=smallScreen;
				camera.depth+=2;
				print("Drive Camera's depth is "+ camera.depth);
				driveMode=false;
			}
			else{
				camera.rect = new Rect(0,0,Screen.width,Screen.height);
				driveMode=true;
			}
		}
	}

}
