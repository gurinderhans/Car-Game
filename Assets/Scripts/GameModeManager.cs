using UnityEngine;
using System.Collections;

public class GameModeManager : MonoBehaviour {

	public bool shootMode;


	/*GUN Componenets*/
	GameObject gun;

	void Start(){
		gun = GameObject.FindGameObjectWithTag ("Gun");
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

			/*Bring the Gun UP*/
			//gun.transform.localPosition = Vector3.up * 6f;//this is to move gun up and down / hide || show it


		} else{

			Camera.main.GetComponent<GunCameraMovement>().enabled = false;
			Camera.main.GetComponent<DriveCamController>().enabled = true;//after this disable gun so it dosent show
			Camera.main.GetComponent<CrossHair>().enabled = false;

			/*Disabling Gun Components*/
			gun.GetComponent<GunMovement>().enabled = false;
			gun.GetComponent<ShootBullet>().enabled = false;

			/*Put the Gun Down*/
			//gun.transform.localPosition = Vector3.down * 2f;//this is to move gun up and down / hide || show it

		}
	}
}
