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
	
	public GameObject hostMessageGameObj;
	public GameObject main_scripts;
	
	void Start(){
		//chatMessages.Add ("Welcome to the Multiplayer Car-Game!");
		hostMessageGameObj = GameObject.FindGameObjectWithTag("HostMessage");
		
		//Setting Temporary Variables as the Default Value
		SizeSlider = hostMessageGameObj.GetComponent<GUIText>().fontSize;
		tempColor = hostMessageGameObj.GetComponent<GUIText>().color;
		Rslider = tempColor.r;
		Gslider = tempColor.g;
		Bslider = tempColor.b;
		Aslider = tempColor.a;
		FontStyle styleStart = hostMessageGameObj.GetComponent<GUIText>().fontStyle;
		if(styleStart==FontStyle.Bold) strStyle="Bold";
		else if(styleStart==FontStyle.Italic) strStyle="Italic";
		else if(styleStart==FontStyle.BoldAndItalic) strStyle="Bold and Italic";
		else strStyle="Normal";
		ApplyAllChanges ();

		main_scripts = GameObject.Find ("_SCRIPTS");
	}
	
	
	void windowFunc(int id){
		
		GUILayout.BeginHorizontal (GUILayout.Width (250));
		messageToSend = GUILayout.TextField (messageToSend, GUILayout.MinWidth (150));
		if (GUILayout.Button ("Send", GUILayout.MinWidth (50)) || Event.current.isKey && Event.current.keyCode == KeyCode.Return) {//add or press Enter later
			//send the message and empty chatText space
			//add messages to chatMessages list
			if (Network.isServer) {
				//if we are server/host then we send special messages to all clients
				ApplyAllChanges();
				if(ifCheatThenActivate(messageToSend)){}//everything done in the function
				else networkView.RPC ("SendHostMessage", RPCMode.AllBuffered, new object[]{messageToSend, mySize, myFontStyle, Rcolor, Gcolor, Bcolor, Acolor});
			}
			else {
				if (messageToSend.Length > 0) {//we dont send empty messages
					networkView.RPC ("SendClientMessage", RPCMode.AllBuffered, new object[]{messageToSend, playerName});
				}
			}
			messageToSend = string.Empty;
		}
		
		GUILayout.EndHorizontal ();
		
	}
	
	public bool playerNameCheck;
	
	void Update(){
		playerNameCheck = main_scripts.GetComponent<NetworkManager> ().playerHasName;
		//print (playerNameCheck);
		if(Input.GetKeyDown(KeyCode.BackQuote))
			showChatTextField = !showChatTextField;
		if(Input.GetKeyDown(KeyCode.Backspace)&&Network.isServer) networkView.RPC ("UnlockMap", RPCMode.AllBuffered, new object[]{serverMandeep,lockedDoorMandeep,openDoorMandeep});
	}
	
	public void OnGUI(){
		playerName = main_scripts.GetComponent<NetworkManager> ().myName;
		
		if(Time.timeScale != 0){
			
			if(playerNameCheck){
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
		
		if(Network.isServer){
			StyleModifications();
		}
	}
	
	[RPC]
	public void SendHostMessage(string hostmsg, int  fontsize, string fontstyle, float R, float G, float B, float A){
		hostMessageGameObj.GetComponent<GUIText> ().text = hostmsg;
		
		//********All My changes********
		//size
		hostMessageGameObj.GetComponent<GUIText> ().fontSize = fontsize;
		//color
		hostMessageGameObj.GetComponent<GUIText> ().color = new Color (R, G, B, A);
		//fontStyle
		if(fontstyle=="Normal") hostMessageGameObj.GetComponent<GUIText> ().fontStyle=FontStyle.Normal;
		else if(fontstyle=="Bold") hostMessageGameObj.GetComponent<GUIText> ().fontStyle=FontStyle.Bold;
		else if(fontstyle=="Italic") hostMessageGameObj.GetComponent<GUIText> ().fontStyle=FontStyle.Italic;
		else hostMessageGameObj.GetComponent<GUIText> ().fontStyle=FontStyle.BoldAndItalic;
	}
	
	[RPC]
	public void SendClientMessage(string msg, string pname){
		//send the message
		chatMessages.Add("[-" + pname + "-]"+" : "+ msg);
	}
	
	
	
	//**********All my edits start here**********
	bool showEditOptions;
	
	//Temporary Variables
	
	float SizeSlider,Rslider,Gslider,Bslider,Aslider;
	Color tempColor;
	string strStyle;
	
	//Permanent Variables
	public float Rcolor,Gcolor,Bcolor,Acolor;
	public string myFontStyle;
	public int mySize;
	
	void StyleModifications(){
		
		if(GUI.Button(new Rect(Screen.width-225,5,175,25),"Edit Player Updates")) showEditOptions=!showEditOptions;
		
		if(showEditOptions){
			//Basic Frame Boxes
			GUI.Box(new Rect((Screen.width-600)/2,(Screen.height-300)/2+50,600,160), "");
			GUI.Box(new Rect((Screen.width-100)/2,(Screen.height-300)/2+50,100,25), "Edit Menu");
			//Buttons
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+90,100,25), "Size");
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+130,100,25), "Colour");
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+170,100,25), "Style");
			
			
			//********************All the Changes********************
			
			//size slider
			SizeSlider=GUI.HorizontalSlider(new Rect((Screen.width-600)/2+200,(Screen.height-300)/2+95,240,40), SizeSlider, 25, 100);
			GUI.Box(new Rect((Screen.width-600)/2+460,(Screen.height-300)/2+90,40,25), ConvertToString(SizeSlider));
			
			//color sliders for R,G,B,Alpha
			Rslider = GUI.HorizontalSlider(new Rect((Screen.width-600)/2+200,(Screen.height-300)/2+135,60,40), Rslider, 0, 1);
			Gslider = GUI.HorizontalSlider(new Rect((Screen.width-600)/2+280,(Screen.height-300)/2+135,60,40), Gslider, 0, 1);
			Bslider = GUI.HorizontalSlider(new Rect((Screen.width-600)/2+360,(Screen.height-300)/2+135,60,40), Bslider, 0, 1);
			Aslider = GUI.HorizontalSlider(new Rect((Screen.width-600)/2+440,(Screen.height-300)/2+135,60,40), Aslider, 0, 1);
			tempColor = new Color(Rslider,Gslider,Bslider,Aslider);
			GUI.color = tempColor;
			GUI.Label(new Rect((Screen.width-600)/2+520,(Screen.height-300)/2+130,50,40), "R,G,B,A");
			GUI.color = Color.white;
			
			//style
			if(GUI.Button(new Rect((Screen.width-200)/2, (Screen.height-300)/2+170, 300, 25), strStyle)) strStyle = StyleChangePress(strStyle);
			
			//Apply Changes after pressing button
			/*if(GUI.Button(new Rect((Screen.width-100)/2,(Screen.height+125)/2,100,25), "Apply")) {
				ApplyAllChanges();
			}*/
		}
		
		
	}
	
	string ConvertToString( float no){
		string str = no.ToString ();
		if(no==100) return "100";
		else if(str.Length<=2) return str;
		else return str.Substring(0,2);
	}
	
	string StyleChangePress(string str){
		if(str=="Normal") return "Bold";
		else if(str=="Bold") return "Italic";
		else if(str=="Italic") return "Bold and Italic";
		else return "Normal";
	}
	
	void ApplyAllChanges(){
		mySize = (int) SizeSlider;
		Rcolor = Rslider;
		Gcolor = Gslider;
		Bcolor = Bslider;
		Acolor = Aslider;
		myFontStyle = strStyle;
	}
	
	//******for Public Doors Unlock******
	[HideInInspector] public bool serverMandeep,serverMustafa;
	[HideInInspector] public string lockedDoorMandeep = "ClosedDoorMandeep";
	[HideInInspector] public string lockedDoorMustafa = "ClosedDoorMustafa";
	[HideInInspector] public string openDoorMandeep = "Doors/OpenDoorMandeep";
	[HideInInspector] public string openDoorMustafa = "Doors/OpenDoorMustafa";
	
	bool ifCheatThenActivate(string txt){
		if(txt=="mandeepmap"){
			networkView.RPC ("UnlockMap", RPCMode.AllBuffered, new object[]{serverMandeep,lockedDoorMandeep,openDoorMandeep});
			return true;
		}
		else if(txt=="mustafamap"){
			networkView.RPC ("UnlockMap", RPCMode.AllBuffered, new object[]{serverMustafa,lockedDoorMustafa,openDoorMustafa});
			return true;
		}
		else return false;
	}
	
	[RPC]
	void UnlockMap(bool theBool, string lockedDoorStr, string openDoor){
		if(!theBool){
			GameObject lockedDoor=GameObject.Find(lockedDoorStr);
			Vector3 pos = lockedDoor.transform.position;
			Quaternion rot = lockedDoor.transform.rotation;
			DestroyObject (lockedDoor);
			Instantiate (Resources.Load(openDoor), pos, rot);
			theBool=true;
		}
	}
}