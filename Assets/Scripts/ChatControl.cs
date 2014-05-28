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

	public string hostMessage;

	public GameObject hostMessageGameObj;
	
	void Start(){
		//chatMessages.Add ("Welcome to the Multiplayer Car-Game!");
		hostMessageGameObj = GameObject.FindGameObjectWithTag("HostMessage");
	}
	

	private void windowFunc(int id){

		GUILayout.BeginHorizontal (GUILayout.Width (250));
		messageToSend = GUILayout.TextField (messageToSend, GUILayout.MinWidth (150));
		if (GUILayout.Button ("Send", GUILayout.MinWidth (50)) || Event.current.isKey && Event.current.keyCode == KeyCode.Return) {//add or press Enter later
			//send the message and empty chatText space
			//add messages to chatMessages list
			if (Network.isServer) {
				//if we are server/host then we send special messages to all clients
				networkView.RPC ("SendHostMessage", RPCMode.AllBuffered, new object[]{messageToSend});
				messageToSend = string.Empty;
			} else {
				if (messageToSend.Length > 0) {//we dont send empty messages
					networkView.RPC ("SendClientMessage", RPCMode.AllBuffered, new object[]{messageToSend, playerName});
					messageToSend = string.Empty;
				}
			}
		}
		
		GUILayout.EndHorizontal ();

	}
	
	public bool playerNameCheck;

	void Update(){
		playerNameCheck = GameObject.FindGameObjectWithTag ("playerName").GetComponent<PlayerLabel> ().playerHasName;
		if(Input.GetKeyDown(KeyCode.BackQuote))
			showChatTextField = !showChatTextField;



	}
	
	public void OnGUI(){
		playerName = GameObject.FindGameObjectWithTag ("playerName").GetComponent<PlayerLabel> ().myName;
		
		if(Time.timeScale != 0){
			
			if(playerNameCheck){
				//player chat will have to go before the next class build so this whole thing will have to removed :(
				
				if(showChatTextField){
					Rect windowRect = new Rect(0,Screen.height - 50,270,70);
					windowRect = GUI.Window(1, windowRect, windowFunc, "Chat");
				}
				
			}
			
		}
		
		//even for now and later on we dont want to hide display messages on game pasue b/c that will show game status and a whole lot of other thigns
		//so keep this outside
		GUILayout.Space(Screen.height - (Screen.height - 120));//add space above b/c of name change field
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(350));
		foreach(string message in chatMessages){
			//display the messages
			GUILayout.Label(message);
		}
		GUILayout.EndScrollView ();
		
		scrollPosition.y = 100000000000000000;//hopefully this never ends
	}

	[RPC]
	public void SendHostMessage(string hostmsg){
		hostMessage = "From Server : " + hostmsg;

		hostMessageGameObj.GetComponent<GUIText>().text = hostMessage;
	}

	[RPC]
	public void SendClientMessage(string msg, string pname){
		//send the message
		chatMessages.Add("[-" + pname + "-]"+" : "+ msg);
	}
}