using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for the list

public class ChatControl : MonoBehaviour {

	public string chatText;
    public List<string> chatMessages = new List<string>();
    //make string empty because we dont want to send null values over network
	public Vector2 scrollPosition;
	public string playerName;
    	
	public void OnGUI(){

		playerName = GameObject.FindGameObjectWithTag ("playerName").GetComponent<PlayerLabel> ().myName;
		GUILayout.Space(50);//add space above b/c of name change field
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(450));
		foreach(string message in chatMessages){
			//display the messages
			GUILayout.Label(message);
		}
		GUILayout.EndScrollView ();

		GUILayout.Space(5);//add little bit space above b/c of chat messages display field

		GUILayout.BeginHorizontal(GUILayout.Width(250));
		chatText = GUILayout.TextField (chatText);
		if(GUILayout.Button("Send") || Event.current.isKey && Event.current.keyCode == KeyCode.Return){//add or press Enter later
			//send the message and empty chatText space
			//add messages to chatMessages list
			if(chatText.Length > 0){//we dont send empty messages
				networkView.RPC("sendMessage", RPCMode.AllBuffered, new object[]{chatText, playerName});
				chatText = string.Empty;
			}
		}

		GUILayout.EndHorizontal();
		scrollPosition.y = 100000000000000000;
	}

	[RPC]
	public void sendMessage(string msg, string pname){
		//send the message
		chatMessages.Add("[-" + pname + "-]"+" : "+ msg);
	}

	/*
	*need to add player name e.g [playername: message sent by player]
	*show only fter the player is well connected and ready to play
	*anything else also do :)
	*this is a ROUGH DRAFT
	*/
}