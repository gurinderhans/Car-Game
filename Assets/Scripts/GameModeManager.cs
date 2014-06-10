using UnityEngine;
using System.Collections;

public class GameModeManager : MonoBehaviour {

	public bool shootMode;


	/*GUN Componenets*/
	GameObject gun;

	//GUI style
	public GUIStyle pointsDisplayStyle;

	void Start(){
		gun = GameObject.FindGameObjectWithTag ("Gun");
	}

	void OnGUI(){
		GUI.Label (new Rect (0,Screen.height - 50,100,50), gun.GetComponent<ShootBullet>().myPoints.ToString() +": points", pointsDisplayStyle);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Y)) shootMode=!shootMode;

		//print (shootMode);
		if(shootMode){

			//currently in shoot mode
			Camera.main.GetComponent<GunCameraMovement>().enabled = true;
			Camera.main.GetComponent<DriveCamController>().enabled = false;
			Camera.main.GetComponent<CrossHair>().enabled = true;

			/*Enabling Gun Components*/
			gun.GetComponent<GunMovement>().enabled = true;
			gun.GetComponent<ShootBullet>().enabled = true;


		} else{

			//to make sure the zoom mode is fixed
			if(this.GetComponent<GunCameraMovement>().showZoom){
				this.GetComponent<GunCameraMovement>().mouseRotationMultiplier=1;
				Color tempColor = this.GetComponent<GunCameraMovement>().zoomPlane.renderer.material.color;
				tempColor.a=0;
				this.GetComponent<GunCameraMovement>().zoomPlane.renderer.material.color = tempColor;
				camera.fieldOfView=60;
				this.GetComponent<GunCameraMovement>().showZoom=false;
			}

			Camera.main.GetComponent<GunCameraMovement>().enabled = false;
			Camera.main.GetComponent<DriveCamController>().enabled = true;//after this disable gun so it dosent show
			Camera.main.GetComponent<CrossHair>().enabled = false;

			/*Disabling Gun Components*/
			gun.GetComponent<GunMovement>().enabled = false;
			gun.GetComponent<ShootBullet>().enabled = false;

		}
	}
}
