using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {

	public enum preset { none, shotgunPreset, crysisPreset }
	public preset crosshairPreset = preset.none;
	
	public bool showCrosshair = true;
	public Texture2D verticalTexture ;
	public Texture2D horizontalTexture ;
	
	//Size of boxes
	public float cLength = 10.0f;
	public float cWidth = 3.0f;
	float mycLength, mycWidth;
	
	//Spreed setup
	public float minSpread = 45.0f;
	public float maxSpread = 250.0f;
	float myminSpread, mymaxSpread;
	public float spreadPerSecond = 150.0f;
	
	//Rotation
	public float rotAngle = 0.0f;
	public float rotSpeed = 0.0f;
	
	//by Manshant Singh
	Transform target;
	GameObject shootCamera,targetTrans;
	[HideInInspector] public string whoIsIt;
	public Texture2D redTexture;
	public Texture2D greenTexture;
	Rect rectShootCamera;
	bool modeFS;
	
	[HideInInspector] public Texture2D temp;
	[HideInInspector] public float spread,myspread;
	
	void Start ()
	{
		crosshairPreset = preset.none;
		whoIsIt = null;
		//shootCamera = GameObject.Find ("ShootCamera");
		targetTrans = GameObject.Find ("gun TBS 001C");
		target = targetTrans.transform;
		
	}
	
	void Update(){
		//from the other Camera -MSK
		//cameraMovement scriptShootCamera = shootCamera.gameObject.GetComponent<cameraMovement>();
		//Rect OriCamShoot = scriptShootCamera.rectShootCam;
		Rect OriCamShoot = new Rect (0.65f, 0.55f, 0.4f, 0.3f);
		rectShootCamera = new Rect (Screen.width*OriCamShoot.x, Screen.height*OriCamShoot.y, Screen.width*OriCamShoot.width, Screen.height*OriCamShoot.height);
		
		mycLength = cLength * OriCamShoot.height;
		mycWidth = cWidth * OriCamShoot.width;
		myminSpread = minSpread * (OriCamShoot.width+OriCamShoot.height)/2;
		mymaxSpread = maxSpread * (OriCamShoot.width + OriCamShoot.height) / 2;
		
		if(Time.timeScale!=0){
			//switch between modes
			if(Input.GetKeyDown(KeyCode.Y)) modeFS=!modeFS;
			
			
			

			//by Manshant Singh
			RaycastHit hit;
			Debug.DrawRay(target.transform.position, transform.forward * 1000);
			if(Physics.Raycast (target.transform.position, target.transform.TransformDirection(Vector3.forward), out hit)){
				whoIsIt=hit.transform.gameObject.tag;
			}
			else
				whoIsIt=null;


			//Used just for test (weapon script should change spread).
			if(isMoving()){
				spread += spreadPerSecond * Time.deltaTime;
				myspread += spreadPerSecond * Time.deltaTime;
			}
			else{
				spread -= spreadPerSecond * 2 * Time.deltaTime;
				myspread -= spreadPerSecond * 2 * Time.deltaTime;
			}
			//Rotation
			rotAngle += rotSpeed * Time.deltaTime;
		}
	}
	
	bool isMoving(){
		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");
		if((x!=0f)||(y!=0f)) return true;
		else return false;
	}
	
	void OnGUI(){
		
		
		
		if(showCrosshair && verticalTexture && horizontalTexture && Time.timeScale!=0){
			GUIStyle verticalT = new GUIStyle();
			GUIStyle horizontalT = new GUIStyle();
			
			if(whoIsIt=="Green"){
				verticalT.normal.background = greenTexture;
				horizontalT.normal.background = greenTexture;
			}else if(whoIsIt=="Red"){
				verticalT.normal.background = redTexture;
				horizontalT.normal.background = redTexture;
			}
			else{
				verticalT.normal.background = verticalTexture;
				horizontalT.normal.background = horizontalTexture;
			}
			spread = Mathf.Clamp(spread, minSpread, maxSpread);
			myspread = Mathf.Clamp(myspread, myminSpread, mymaxSpread);
			Vector2 pivot = new Vector2(Screen.width/2, Screen.height/2);
			
			if(crosshairPreset == preset.crysisPreset){
				
				GUI.Box(new Rect((Screen.width - 2)/2, (Screen.height - spread)/2 - 14, 2, 14), temp, horizontalT);
				GUIUtility.RotateAroundPivot(45,pivot);
				GUI.Box(new Rect((Screen.width + spread)/2, (Screen.height - 2)/2, 14, 2), temp, verticalT);
				GUIUtility.RotateAroundPivot(0,pivot);
				GUI.Box(new Rect((Screen.width - 2)/2, (Screen.height + spread)/2, 2, 14), temp, horizontalT);
			}
			
			if(crosshairPreset == preset.shotgunPreset){
				
				GUIUtility.RotateAroundPivot(45,pivot);
				
				//Horizontal
				GUI.Box(new Rect((Screen.width - 14)/2, (Screen.height - spread)/2 - 3, 14, 3), temp, horizontalT);
				GUI.Box(new Rect((Screen.width - 14)/2, (Screen.height + spread)/2, 14, 3), temp, horizontalT);
				//Vertical
				GUI.Box(new Rect((Screen.width - spread)/2 - 3, (Screen.height - 14)/2, 3, 14), temp, verticalT);
				GUI.Box(new Rect((Screen.width + spread)/2, (Screen.height - 14)/2, 3, 14), temp, verticalT);
			}
			
			if(crosshairPreset == preset.none){
				if(!modeFS){
					GUIUtility.RotateAroundPivot(rotAngle%360,pivot);
					//Horizontal
					GUI.Box(new Rect(rectShootCamera.x + (rectShootCamera.width - mycWidth)*0.37f, rectShootCamera.y*0.734f - (rectShootCamera.height + myspread)/2 - mycLength/2, mycWidth, mycLength), temp, horizontalT);
					GUI.Box(new Rect(rectShootCamera.x + (rectShootCamera.width - mycWidth)*0.37f, rectShootCamera.y*0.734f - (rectShootCamera.height - myspread)/2 - mycLength/2, mycWidth, mycLength), temp, horizontalT);
					//Vertical
					GUI.Box(new Rect(rectShootCamera.x + (rectShootCamera.width - myspread)*0.37f - mycLength/2, rectShootCamera.y*0.73f - (rectShootCamera.height - mycWidth)/2, mycLength, mycWidth), temp, verticalT);
					GUI.Box(new Rect(rectShootCamera.x + (rectShootCamera.width + myspread)*0.37f - mycLength/2, rectShootCamera.y*0.73f - (rectShootCamera.height - mycWidth)/2, mycLength, mycWidth), temp, verticalT);
				}
				if(modeFS){
					GUIUtility.RotateAroundPivot(rotAngle%360,pivot);
					//Horizontal
					GUI.Box(new Rect((Screen.width - cWidth)/2,(Screen.height - spread)/2 - cLength/2, cWidth, cLength), temp, horizontalT);
					GUI.Box(new Rect((Screen.width - cWidth)/2,(Screen.height + spread)/2 - cLength/2, cWidth, cLength), temp, horizontalT);
					//Vertical
					GUI.Box(new Rect((Screen.width - spread)/2 - cLength/2, (Screen.height - cWidth)/2, cLength, cWidth), temp, verticalT);
					GUI.Box(new Rect((Screen.width + spread)/2 - cLength/2, (Screen.height - cWidth)/2, cLength, cWidth), temp, verticalT);
				}
			}
		}
	}

}
