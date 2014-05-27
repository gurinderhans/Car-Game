using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	void OnTriggerEnter(Collider car){

		GameObject carToTeleport = car.gameObject.transform.parent.gameObject;

		string thisDoorsName = this.transform.parent.gameObject.transform.name;

		if(thisDoorsName == "OpenDoorMustafa(Clone)"){
			carToTeleport.transform.position = new Vector3(-13110, 10f, 50f);
		} else if(thisDoorsName == "OpenDoorMandeep(Clone)"){
			carToTeleport.transform.position = new Vector3(14550f, 170f , -780f);
		} else if(thisDoorsName == "MustafaToMainArea"){
			carToTeleport.transform.position = Vector3.zero;
		} else if(thisDoorsName == "MandeepToMainArea"){
			carToTeleport.transform.position = Vector3.zero;
		} else{
			carToTeleport.transform.position = Vector3.zero;
		}
	}

}
