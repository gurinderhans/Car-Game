using UnityEngine;
using System.Collections;

public class PlayerNameManager : MonoBehaviour {

	public string myName;

	public void OnGUI(){
		if(networkView.isMine){
			GUI.Label (new Rect (25f, 17.5f, 150f, 22.5f), "Enter Your Name : ");
			myName = GUI.TextField(new Rect(140f, 17.5f, 150f, 22.5f), myName, 25);
			networkView.RPC ("changeName", RPCMode.AllBuffered, new object[]{myName});
		}
	}


	[RPC]
	public void changeName(string name){
		this.GetComponent<TextMesh> ().text = name;
	}
}
