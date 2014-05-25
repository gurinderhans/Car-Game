using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class HostPublicMessages : MonoBehaviour {
	
	//GameObject scripts;
	
	bool showEditOptions;
	/*bool weKnowYouAreHost;
	bool weKnowYouAreJoiner;
	bool goAheadAndShow;*/
	
	
	/*****  All Temporary Variables  *****/
	string tempText;
	float Rslider,Gslider,Bslider,Aslider;
	float SizeSlider;
	string strStyle;
	Color tempColor;
	
	
	void Start(){
		
		transform.position = new Vector3 (0.5f, 0.1f, 0);
		//scripts = GameObject.Find ("_SCRIPTS");
		
		
		guiText.text = "test";
		
		
		//Setting Temporary Variables as the Default Value
		tempText = guiText.text;
		SizeSlider = guiText.fontSize;
		tempColor = guiText.color;
		Rslider = tempColor.r;
		Gslider = tempColor.g;
		Bslider = tempColor.b;
		Aslider = tempColor.a;
		FontStyle styleStart = guiText.fontStyle;
		if(styleStart==FontStyle.Bold) strStyle="Bold";
		else if(styleStart==FontStyle.Italic) strStyle="Italic";
		else if(styleStart==FontStyle.BoldAndItalic) strStyle="Bold and Italic";
		else strStyle="None";
		
	}
	
	void Update(){
		/*if(!weKnowYouAreHost && !weKnowYouAreJoiner){
			NetworkManager netScript = scripts.GetComponent<NetworkManager> ();
			if(netScript.youAreTheHost) weKnowYouAreHost=true;
			if(netScript.youAreTheJoiner) weKnowYouAreJoiner=true;
		}
		PauseMenu pauseScript = scripts.GetComponent<PauseMenu> ();
		goAheadAndShow = !pauseScript.gamePaused;*/
		
		/*if(!networkView.isMine){
			this.GetComponent<GUIText>().enabled = true;
		} else{
			this.GetComponent<GUIText>().enabled = false;
		}*/
		
	}
	
	string ConvertToString( float no){
		string str = no.ToString ();
		if(no==100) return "100";
		else if(str.Length<=2) return str;
		else return str.Substring(0,2);
	}
	
	string StyleChangePress(string str){
		if(str=="None") return "Bold";
		else if(str=="Bold") return "Italic";
		else if(str=="Italic") return "Bold and Italic";
		else return "None";
	}
	
	[RPC]
	void ApplyAllChanges(){
		guiText.text = tempText;
		guiText.fontSize = (int) SizeSlider;
		guiText.color = tempColor;
		if(strStyle=="None") guiText.fontStyle=FontStyle.Normal;
		else if(strStyle=="Bold") guiText.fontStyle=FontStyle.Bold;
		else if(strStyle=="Italic") guiText.fontStyle=FontStyle.Italic;
		else guiText.fontStyle=FontStyle.BoldAndItalic;

		print ("chanign");
	}
	
	void OnGUI(){
		
		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
		myStyle.alignment = TextAnchor.MiddleCenter;
		
		if(GUI.Button(new Rect(Screen.width-225,5,175,25),"Edit Player Updates")) showEditOptions=!showEditOptions;
		
		if(showEditOptions){
			//Basic Frame Boxes
			GUI.Box(new Rect((Screen.width-600)/2,(Screen.height-300)/2,600,250), "");
			GUI.Box(new Rect((Screen.width-100)/2,(Screen.height-300)/2,100,25), "Edit Menu");
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+50,100,25), "Text");
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+90,100,25), "Size");
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+130,100,25), "Colour");
			GUI.Box(new Rect((Screen.width-600)/2+15,(Screen.height-300)/2+170,100,25), "Style");
			
			
			//********************All the Changes********************
			//text
			tempText=GUI.TextField(new Rect((Screen.width-200)/2, (Screen.height-300)/2+50, 300, 25), tempText, myStyle);
			
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
			if(GUI.Button(new Rect((Screen.width-100)/2,(Screen.height+125)/2,100,25), "Apply")) {
				networkView.RPC("ApplyAllChanges", RPCMode.All);
			}
		}
		
		
	}
	
}