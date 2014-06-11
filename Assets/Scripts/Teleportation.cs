using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	void OnTriggerEnter(Collider car){

		GameObject carToTeleport = car.gameObject.transform.parent.gameObject;

		string thisDoorsName = this.transform.parent.gameObject.transform.name;

		if(thisDoorsName == "OpenDoorMustafa(Clone)"){
			carToTeleport.transform.position = new Vector3(-13110, 0f, 50f);
		} else if(thisDoorsName == "OpenDoorMandeep(Clone)"){
			carToTeleport.transform.position = new Vector3(14566.29F, 0f , -780f);
		} else if(thisDoorsName == "MustafaToMainArea"){
			carToTeleport.transform.position = new Vector3(-886f, 0f, -4f);
		} else if(thisDoorsName == "MandeepToMainArea"){
			carToTeleport.transform.position = new Vector3(940f, 0f, 37f);
		} else{
			carToTeleport.transform.position = Vector3.zero;
		}
	}

}
