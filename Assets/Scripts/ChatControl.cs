using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for the list

public class ChatControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public string chatText = string.Empty;
    	public List<string> chatMessages = new List<string>();
    	//make string empty because we dont want to send null values over network
    	
	public void OnGUI(){
	        GUILayout.Space(300);//add space above b/c of name change field
	        GUILayout.BeginHorizontal(GUILayout.width(250));
	            chatText = GUILayout.TextField(chatText);
	            if(GUILayout.Button("Send")){//add or press Enter later
	                //send the message and empty chatText space
	                //add messages to chatMessages list
	                networkView.RPC("sendMessage", RPCMode.AllBuffered, new Object[] {chatText}); 
	                chatText = string.Empty;
	            }
	        GUILayout.EndHorizontal();
	
	        //display the messages
	        foreach(string message in chatMessages){
	            //print the messages
	            GUILayout.Label(message);
	        }
	
	        [RPC]
	        public void sendMessage(string msg){
	            //send the message
	            chatMessages.Add(msg);
	        }
	        
	}
	
	/*
	*need to add player name e.g [playername: message sent by player]
	*show only fter the player is well connected and ready to plau
	*anything else also do :)
	*this is a ROUGH DRAFT
	*/
}
