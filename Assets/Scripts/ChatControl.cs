using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for the list

public class ChatControl : MonoBehaviour {
	
	public string messageToSend;
	public List<string> chatMessages = new List<string>();
	//make string empty because we dont want to send null values over network
	public Vector2 scrollPosition;
	public string playerName;
	public bool showChatTextField;
	
	void Start(){
		//chatMessages.Add ("Welcome to Multiplayer Car-Game!");
	}
	
	
	private void windowFunc(int id){
		GUILayout.BeginHorizontal(GUILayout.Width(250));
		messageToSend = GUILayout.TextField (messageToSend, GUILayout.MinWidth(150));
		if(GUILayout.Button("Send", GUILayout.MinWidth(50)) || Event.current.isKey && Event.current.keyCode == KeyCode.Return){//add or press Enter later
			//send the message and empty chatText space
			//add messages to chatMessages list
			if(messageToSend.Length > 0){//we dont send empty messages
				networkView.RPC("sendMessage", RPCMode.AllBuffered, new object[]{messageToSend, playerName});
				messageToSend = string.Empty;
			}
		}
		
		GUILayout.EndHorizontal();
		
		//GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));//will work if Rect var is not being updated multiple times
	}
	
	public void OnGUI(){
		playerName = GameObject.FindGameObjectWithTag ("playerName").GetComponent<PlayerLabel> ().myName;
		GUILayout.Space(Screen.height - (Screen.height - 120));//add space above b/c of name change field
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(/*450*/250));
		foreach(string message in chatMessages){
			//display the messages
			GUILayout.Label(message);
		}
		GUILayout.EndScrollView ();
		/*
		GUILayout.Space(5);//add little bit space above b/c of chat messages display field
		


*/
		if(Event.current.keyCode == KeyCode.BackQuote)
			showChatTextField = true;
		
		if(Event.current.keyCode == KeyCode.Escape)
			showChatTextField = false;
		
		if(showChatTextField){
			Rect windowRect = new Rect(0,Screen.height - 50,270,70);
			windowRect = GUI.Window(1, windowRect, windowFunc, "Chat");
		}
		
		
		//messageToSend = GUI.TextField(new Rect (0,Screen.height - 50,170,21), messageToSend);
		//GUILayout.Space(120);
		//messageToSend = GUILayout.TextField (messageToSend);
		
		
		/*GUILayout.BeginHorizontal(GUILayout.Width(250));
			messageToSend = GUILayout.TextField (messageToSend);
			if(GUILayout.Button("Send") || Event.current.isKey && Event.current.keyCode == KeyCode.Return){//add or press Enter later
				//send the message and empty chatText space
				//add messages to chatMessages list
				if(messageToSend.Length > 0){//we dont send empty messages
					networkView.RPC("sendMessage", RPCMode.AllBuffered, new object[]{messageToSend, playerName});
					messageToSend = string.Empty;
				}
			}

			GUILayout.EndHorizontal();*/
		
		scrollPosition.y = 100000000000000000;//hopefully this never ends
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