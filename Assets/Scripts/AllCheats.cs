using UnityEngine;
using System.Collections;

public class AllCheats : MonoBehaviour {
	
	public bool developerMode;
	[HideInInspector] public bool showInstructions,nitroAllowed,jumpAllowed,checkPassword,showDeveloperMenu,isMandeepMapAvailable,isMustafaMapAvailable,zoomAllowed,airFly,godMode,noBlur;
	string passwordTry="";
	
	public string checkStatus(bool YESorNO){
		if(YESorNO){
			return " is Enabled";
		}
		else{
			return " is Disabled";
		}
	}
	
	string checkMapUnlocked(bool YESorNO){
		if(YESorNO){
			return " is Unlocked";
		}
		else{
			return " is Locked";
		}
	}
	
	void UnlockMandeepMap(){
		if(!isMandeepMapAvailable){
			GameObject lockedDoor = GameObject.Find ("ClosedDoorMandeep");
			Vector3 pos = lockedDoor.transform.position;
			Quaternion rot = lockedDoor.transform.rotation;
			DestroyObject (lockedDoor);
			Instantiate (Resources.Load ("Doors/OpenDoorMandeep"), pos, rot);
			isMandeepMapAvailable=true;
		}
	}
	
	void UnlockMustafaMap(){
		if(!isMustafaMapAvailable){
			GameObject lockedDoor = GameObject.Find ("ClosedDoorMustafa");
			Vector3 pos = lockedDoor.transform.position;
			Quaternion rot = lockedDoor.transform.rotation;
			DestroyObject (lockedDoor);
			Instantiate (Resources.Load ("Doors/OpenDoorMustafa"), pos, rot);
			isMustafaMapAvailable=true;
		}
	}
	
	void isPasswordRight(){
		if(passwordTry=="GURIorMSK") developerMode=!developerMode;//probably dont want to turn this false so just set to true later
		if(passwordTry=="kangaroo") jumpAllowed=!jumpAllowed;
		if(passwordTry=="rapid") nitroAllowed=!nitroAllowed;
		if(passwordTry=="mandeepmap") UnlockMandeepMap();
		if(passwordTry=="mustafamap") UnlockMustafaMap();
		if(passwordTry=="zoomify") zoomAllowed=!zoomAllowed;
		if(passwordTry=="pilot") airFly=!airFly;
		if(passwordTry=="sharpness") noBlur=!noBlur;
		
		passwordTry="";
		checkPassword = false;
		Time.timeScale = 1;
	}
	
	void Update(){
		if(this.GetComponent<PauseMenu>().gamePaused){
			showDeveloperMenu = false;
			checkPassword = false;
		} else if(Input.GetKeyDown(KeyCode.O)&&developerMode){
			showDeveloperMenu=!showDeveloperMenu;
			if(Time.timeScale!=0) Time.timeScale=0;
			else Time.timeScale=1;
		} else if(Input.GetKeyUp(KeyCode.KeypadMultiply)&&!showDeveloperMenu){
			checkPassword=true;
			Time.timeScale=0;
		} else if(Input.GetKeyDown(KeyCode.Escape)){
			checkPassword=false;
			showDeveloperMenu=false;
			Time.timeScale=1;
		}
		//show instructions;
		if(Input.GetKeyDown (KeyCode.I)) showInstructions=!showInstructions;
	}
	
	void OnGUI(){
		if (showDeveloperMenu) {
			GUI.Box(new Rect((Screen.width-300)/2,(Screen.height-400)/2,300,400),"Developer Options");
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+40,200,35),"Jump"+checkStatus(jumpAllowed))) jumpAllowed=!jumpAllowed;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+80,200,35),"Nitro"+checkStatus(nitroAllowed))) nitroAllowed=!nitroAllowed;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+120,200,35),"Sniper Zoom"+checkStatus (zoomAllowed))) zoomAllowed=!zoomAllowed;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+160,200,35),"Fly Mode"+checkStatus(airFly))) airFly=!airFly;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+200,200,35),"GOD Mode"+checkStatus(godMode))) godMode=!godMode;
			//if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+240,200,35),"Var"+checkStatus(var))) var=!var;
			//if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+280,200,35),"Var"+checkStatus(var))) var=!var;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+240,200,35),"Mandeep's map"+checkMapUnlocked(isMandeepMapAvailable))) UnlockMandeepMap();
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+280,200,35),"Mustafa's map"+checkMapUnlocked(isMustafaMapAvailable))) UnlockMustafaMap();
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+320,200,35),"No Blur"+checkStatus(noBlur))) noBlur=!noBlur;
		}
		else if(checkPassword){
			GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
			myStyle.alignment = TextAnchor.MiddleCenter;
			passwordTry=GUI.TextField(new Rect((Screen.width-200)/2, (Screen.height-20)/2, 200, 20), passwordTry, myStyle);
			if (Event.current.isKey && Event.current.keyCode == KeyCode.Return || GUI.Button (new Rect ((Screen.width+250)/2,(Screen.height-20)/2,50,20), "Enter")) isPasswordRight();
		}
		else if(showInstructions){
			GUIStyle Heading = new GUIStyle(GUI.skin.label);
			Heading.fontSize=30;
			Heading.fontStyle=FontStyle.BoldAndItalic;
			
			GUIStyle subHeading = new GUIStyle(GUI.skin.label);
			subHeading.fontSize=20;
			subHeading.fontStyle=FontStyle.Bold;
			
			GUIStyle Normal = new GUIStyle(GUI.skin.label);
			Normal.fontSize=15;
			Normal.fontStyle=FontStyle.Italic;
			
			//five times to make it think
			if(GUI.Button (new Rect((Screen.width-700)/2,(Screen.height-500)/2,700,500),"")) showInstructions=!showInstructions;
			GUI.Box (new Rect((Screen.width-700)/2,(Screen.height-500)/2,700,500),"");
			GUI.Box (new Rect((Screen.width-700)/2,(Screen.height-500)/2,700,500),"");
			GUI.Box (new Rect((Screen.width-700)/2,(Screen.height-500)/2,700,500),"");
			GUI.Box (new Rect((Screen.width-700)/2,(Screen.height-500)/2,700,500),"");
			GUI.Box (new Rect((Screen.width-700)/2,(Screen.height-500)/2,700,500),"");
			GUI.Label(new Rect((Screen.width-700)/2+250,(Screen.height-500)/2+6,200,200),"Instructions",Heading);
			
			//Basic
			GUI.Label(new Rect((Screen.width-700)/2+267,(Screen.height-500)/2+60,200,200),"(General Instructions)",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+100,200,200),"Movement",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+102,200,200),"WASD or ArrowKeys",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+150,200,200),"Drift",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+152,200,200),"Left Ctrl",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+200,200,200),"MiniMap",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+202,200,200),"M",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+250,200,200),"Chat",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+252,200,200),"Backquote(`)",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+300,200,200),"Pause Menu",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+302,200,200),"P",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+420,(Screen.height-500)/2+100,200,200),"To Reset",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+560,(Screen.height-500)/2+102,200,200),"Q",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+420,(Screen.height-500)/2+150,200,200),"Cheats",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+560,(Screen.height-500)/2+152,200,200),"Keypad Multily (*)",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+420,(Screen.height-500)/2+200,200,200),"Shoot Mode",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+560,(Screen.height-500)/2+202,200,200),"Y",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+420,(Screen.height-500)/2+250,200,200),"Cursor",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+560,(Screen.height-500)/2+252,200,200),"C",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+420,(Screen.height-500)/2+300,200,200),"ScoreBoard",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+560,(Screen.height-500)/2+302,200,200),"Tab",Normal);
			
			//Cheats required
			GUI.Label(new Rect((Screen.width-700)/2+257,(Screen.height-400)/2+310,200,200),"(Cheats Required for these)",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+400,200,200),"Jump",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+402,200,200),"E",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+20,(Screen.height-500)/2+450,200,200),"Sniper Zoom",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+160,(Screen.height-500)/2+452,200,200),"Z",Normal);
			
			GUI.Label(new Rect((Screen.width-700)/2+420,(Screen.height-500)/2+400,200,200),"Nitro",subHeading);
			GUI.Label(new Rect((Screen.width-700)/2+560,(Screen.height-500)/2+402,200,200),"Left Shift",Normal);
			
		}
	}	
}