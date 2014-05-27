using UnityEngine;
using System.Collections;

public class ChatControl : MonoBehaviour {
	
	public string messageToSend;

	public bool showMessageInputField;

	public string hostMessage;//this gets displayed




	private void windowFunc(int id){
		GUILayout.BeginHorizontal(GUILayout.Width(250));
		messageToSend = GUILayout.TextField (messageToSend, GUILayout.MinWidth(150));
		if(GUILayout.Button("Send", GUILayout.MinWidth(50)) || Event.current.isKey && Event.current.keyCode == KeyCode.Return){//add or press Enter later
			//send the message and empty chatText space
			//add messages to chatMessages list
			if(messageToSend.Length > 0){//we dont send empty messages
				networkView.RPC("sendMessage", RPCMode.AllBuffered, new object[]{messageToSend});
				messageToSend = string.Empty;
			}
		}
		
		GUILayout.EndHorizontal();
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.BackQuote))
			showMessageInputField = !showMessageInputField;
	}
	
	public void OnGUI(){
		if(Time.timeScale != 0){
			
			if(Network.isServer){
				if(showMessageInputField){
					Rect windowRect = new Rect(0,Screen.height - 50,270,70);
					windowRect = GUI.Window(1, windowRect, windowFunc, "HostMessage");
				}
			}
			
		}

		//we dont want to hide display messages on game pasue b/c that will show all messages host sends live
		//so keep this outside
		GUILayout.Space(Screen.height - (Screen.height - 120));//add space above b/c of name change field

		GUI.Label (new Rect ((Screen.width/2) ,Screen.height - 50,100,50), " " + hostMessage);
	}
	
	[RPC]
	public void sendMessage(string msg){
		//send the message

		hostMessage = msg;
	}
	
}