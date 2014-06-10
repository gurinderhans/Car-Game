using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	
	public bool gamePaused;
	string whereInPause;
	
	float menuX;
	float menuY;
	
	void Start(){
		menuX = (Screen.width - 50)/4;
		menuY = (Screen.height - 50)/4;
	}
	
	void Update () {
		
		
		//menuX /= 4;
		//menuY /= 4;
		if(Input.GetKeyDown(KeyCode.C)) Screen.showCursor = !Screen.showCursor;
		
		//if(Input.GetKeyDown(KeyCode.F)) Screen.fullScreen=!Screen.fullScreen;
		
		if (Input.GetKeyDown(KeyCode.P)){
			if(whereInPause=="quit"){
				whereInPause="main";
			}
			else if(gamePaused){
				Time.timeScale=1;
				gamePaused=false;
				//Screen.showCursor=true;
				whereInPause=null;
			}
			else {
				Time.timeScale=0;
				gamePaused=true;
				//Screen.showCursor=true;
				whereInPause="main";
			}
		}
	}
	void OnGUI(){
		if(whereInPause=="quit"){
			
			if(GUI.Button(new Rect((Screen.width-450)/2,(Screen.height-60)/2,200,60), "Yes")){
				Application.Quit();
			}
			if(GUI.Button(new Rect((Screen.width+50)/2,(Screen.height-60)/2,200,60), "No")){
				whereInPause="main";
			}
		}
		else if(gamePaused){
			
			GUI.Box (new Rect (menuX, menuY, 635, 345), "Game Paused");
			
			
			if(whereInPause=="main"){
				if(GUI.Button(new Rect ((menuX+212),(menuY+67),200,60), "Resume")){
					Time.timeScale=1;
					gamePaused=false;
					whereInPause=null;
				}
				if(GUI.Button(new Rect ((menuX+212),(menuY+137),200,60), "Menu")){
					//Application.loadLevel("Menu");
				}
				if(GUI.Button(new Rect((menuX+212),(menuY+207),200,60), "Options")){
					whereInPause="options";
				}
				if(GUI.Button(new Rect((menuX+212),(menuY+277),200,60), "Quit")){
					whereInPause="quit";
				}
			}
			if (whereInPause == "options") {
				if (GUI.Button(new Rect((menuX+212),(menuY+67),200,60), "Toggle Fullscreen")){
					Screen.fullScreen=!Screen.fullScreen;
				}
				if(GUI.Button(new Rect((menuX+212),(menuY+137),200,60), "Back")){
					whereInPause="main";
				}
			}
		}
	}
	
}