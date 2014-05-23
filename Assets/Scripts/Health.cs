using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health = 100f;

	public bool hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(hit){
			//health -= 25f;
			//hit = false;
			networkView.RPC("PlayerHealthSync", RPCMode.AllBuffered);
		}

		if(health <= 0){
			//Network.Destroy(GetComponent<NetworkView>().viewID);
			//gameObject.SetActive(false);
			networkView.RPC("RespawnPlayer", RPCMode.AllBuffered);

		}
	}

	[RPC]
	void PlayerHealthSync(){
		health -= 10f;
		hit = false;
	}

	[RPC]
	void RespawnPlayer(){
		//gameObject.SetActive (false);
		transform.position = new Vector3 (0, 10f, 0);
		//transform.GetComponent<Health> ().enabled = true;
		health = 100f;
		//also disable gunCam later
	}
}
