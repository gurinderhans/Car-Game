using UnityEngine;
using System.Collections;

public class GunCameraMovement : MonoBehaviour {


	public float speed;

	Transform turret;

	//Camera guncam;
	public Camera driveCam;

	public float behindPos;
	public float upPos;
	/*[HideInInspector]*/ public Rect rectShootCam;
	Rect rectStored;
	bool shootMode=false;
	
	void Start(){
		/*set the cameras*/
		//guncam = GameObject.FindGameObjectWithTag ("GunCam").camera;
		driveCam = GameObject.FindGameObjectWithTag ("MainCamera").camera;

		rectShootCam = camera.rect;
		rectStored = camera.rect;

		turret = GameObject.Find ("gun TBS 001C").transform;
	}

	void LateUpdate(){
		if(Time.timeScale!=0){
			
			transform.position = turret.position;
			transform.Translate (-Vector3.forward * behindPos);
			transform.Translate(Vector3.up * upPos);
			
			float angleX = transform.eulerAngles.x;
			float angleY = transform.eulerAngles.y;
			angleX -= Input.GetAxis ("Mouse Y") * speed;
			angleY += Input.GetAxis ("Mouse X") * speed;
			transform.rotation = Quaternion.Euler (angleX, angleY, 0);
		}
	}
	
	void Update () {
		if(Time.timeScale!=0){
			
			if(Input.GetKeyDown(KeyCode.Y)){
				if(!shootMode){
					camera.rect = new Rect(0,0,Screen.width,Screen.height);
					shootMode = true;
					//GameObject.FindGameObjectWithTag("MainCamera").camera.GetComponent<GUILayer>().enabled = false;
				}
				else{
					camera.rect = rectStored;
					camera.depth += 2;
					shootMode = false;
					print ("shoot mode is false");
					driveCam.GetComponent<GUILayer>().enabled = true;
					this.GetComponent<GUILayer>().enabled = false;
					//guncam.GetComponent<GUILayer>().enabled = false;
					//print("Shoot Camera's depth is "+ camera.depth);
				}
			}
			
			rectShootCam = camera.rect;
			
		}
	}
}
