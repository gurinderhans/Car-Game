using UnityEngine;
using System.Collections;

public class PlayerNameManager : MonoBehaviour {

	public string myName;

	public void OnGUI(){
		if(networkView.isMine){//if networkview component is mine -> the one I use to send/recieve data,
			//then do this stuff otherwise everyone on network will be doing it too.
			GUI.Label (new Rect (25f, 17.5f, 150f, 22.5f), "Enter Your Name : ");
			myName = GUI.TextField(new Rect(140f, 17.5f, 150f, 22.5f), myName, 25);
			//Event.current.iskey detects if event is a keyboard event -> keyboard press
			//we only want rpc call to get called when name is actually being changed not every gui update
			//and specifically we only update on enter key press :)
			if (Event.current.isKey && Event.current.keyCode == KeyCode.Return){
				networkView.RPC ("changeName", RPCMode.OthersBuffered, new object[]{myName});
			}
		}
	}


	[RPC]
	public void changeName(string name){
		this.GetComponent<TextMesh> ().text = name;
	}
}
