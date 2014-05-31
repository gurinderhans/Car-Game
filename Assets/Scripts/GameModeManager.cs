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

			Camera.main.GetComponent<GunCameraMovement>().enabled = false;
			Camera.main.GetComponent<DriveCamController>().enabled = true;//after this disable gun so it dosent show
			Camera.main.GetComponent<CrossHair>().enabled = false;

			/*Disabling Gun Components*/
			gun.GetComponent<GunMovement>().enabled = false;
			gun.GetComponent<ShootBullet>().enabled = false;

		}
	}
}
