using UnityEngine;
using System.Collections;

public class GunCameraMovement : MonoBehaviour {
	
	public GameObject zoomPlane;//the sniper feature plane
	GameObject allScripts;//access cheats
	
	float zoomIn=5;
	float zoomOut=60;
	[HideInInspector] public bool showZoom;
	Color colorForAlpha;
	float fieldOfView;
	float lerpSpeed=20f;
	float mouseRotationMultiplier=1;


	GameObject car;//get the car for other stuff like speed
	//car speed text gui stuff
	GameObject speedTextObj;
	GUIText speedText;
	
	void Start(){
		//current field of view
		camera.fieldOfView=zoomOut;
		fieldOfView = zoomOut;
		//the sniper feature disabled when this script is activated
		colorForAlpha = zoomPlane.renderer.material.color;
		colorForAlpha.a = 0;
		zoomPlane.renderer.material.color = colorForAlpha;
		
		//to get the cheats component
		allScripts = GameObject.Find ("_SCRIPTS");

		//car speed stuff and car find
		car = GameObject.FindGameObjectWithTag("Player");
		speedTextObj = GameObject.Find ("Car Speed");
		speedText = speedTextObj.guiText;
	}
	
	void Update(){
		if(allScripts.GetComponent<AllCheats> ().zoomAllowed){
			
			if(Input.GetKeyDown(KeyCode.Z)){
				if(showZoom){
					showZoom=false;
					mouseRotationMultiplier*=12;
				}
				else{
					showZoom=true;
					mouseRotationMultiplier/=12;
				}
			}
			
			if(showZoom){
				fieldOfView=Mathf.Lerp(fieldOfView, zoomIn, lerpSpeed*Time.deltaTime);
				if(fieldOfView<30f) colorForAlpha.a=Mathf.Lerp(colorForAlpha.a, 1, lerpSpeed*1.5f*Time.deltaTime);
			}
			if(!showZoom){
				fieldOfView=Mathf.Lerp(fieldOfView, zoomOut, lerpSpeed*Time.deltaTime);
				colorForAlpha.a=Mathf.Lerp (colorForAlpha.a, 0, lerpSpeed*1.5f*Time.deltaTime);
			}
			
			camera.fieldOfView=fieldOfView;
			zoomPlane.renderer.material.color = colorForAlpha;
		}
	}
	
	void LateUpdate(){
		if(Time.timeScale!=0){
			transform.position = GameObject.FindGameObjectWithTag ("Gun").transform.position;
			transform.Translate (-Vector3.forward * 25);
			transform.Translate(Vector3.up * 6);
			
			float angleX = transform.eulerAngles.x;
			float angleY = transform.eulerAngles.y;
			angleX -= Input.GetAxis ("Mouse Y") * 5f * mouseRotationMultiplier;
			angleY += Input.GetAxis ("Mouse X") * 5f * mouseRotationMultiplier;
			transform.rotation = Quaternion.Euler (angleX, angleY, 0);
		}

		int carVelocity = (int) car.rigidbody.velocity.magnitude * 2;
		string carVelocityToString = carVelocity.ToString ();
		speedText.text = carVelocityToString + " km/h";

	}
}