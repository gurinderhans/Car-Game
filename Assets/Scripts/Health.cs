using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health = 100;

	public bool hit;

	string guiTextLines = "I I I I I I I I I I";

	public GUIStyle myHealthStyle;



	// Update is called once per frame
	void Update () {

		//print ("hit is " + hit);

		if(hit){
			//health -= 25f;
			//hit = false;
			CalculateHealth ();
			networkView.RPC("PlayerHealthSync", RPCMode.AllBuffered, new object[]{guiTextLines, health});
		}

		if(health <= 0){
			networkView.RPC("PlayerHealthSync", RPCMode.AllBuffered, new object[]{"I I I I I I I I I I", health});
			//Network.Destroy(GetComponent<NetworkView>().viewID);
			//gameObject.SetActive(false);
			networkView.RPC("RespawnPlayer", RPCMode.AllBuffered);

		}


	}

	void CalculateHealth(){

		health -= 10;
		hit = false;

		//Debug.Log (health);

		switch(health){
			
		case 90:
			guiTextLines = "I I I I I I I I I";
			break;
			
		case 80:
			guiTextLines = "I I I I I I I I";
			break;
			
		case 70:
			guiTextLines = "I I I I I I I";
			break;
			
		case 60:
			guiTextLines = "I I I I I I";
			break;
			
		case 50:
			guiTextLines = "I I I I I";
			break;
			
		case 40:
			guiTextLines = "I I I I";
			break;
			
		case 30:
			guiTextLines = "I I I";
			break;
			
		case 20:
			guiTextLines = "I I";
			break;
			
		case 10:
			guiTextLines = "I";
			break;
			
		default:
			guiTextLines = "I I I I I I I I I I";
			break;
		}
	}



	[RPC]
	void PlayerHealthSync(string numLines, int mnetworkHealth){
		health = mnetworkHealth;
		//Replace (" ", "")
		this.GetComponent<TextMesh> ().text = numLines.Replace(" ", "");
	}

	[RPC]
	void RespawnPlayer(){
		//gameObject.SetActive (false);
		//car.gameObject.transform.parent.gameObject;

		this.gameObject.transform.parent.gameObject.transform.position = new Vector3 (0, 10f, 0);

		//transform.GetComponent<Health> ().enabled = true;
		health = 100;

	}

	void OnGUI(){
		GUI.Label (new Rect (0,Screen.height - 50,100,50), guiTextLines, myHealthStyle);
	}

}
