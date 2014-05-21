using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class PlayerLabel : MonoBehaviour {
	
	public Transform target;  // Object that this label should follow
	public Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
	public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
	public float clampBorderSize = 0.05f;  // How much viewport space to leave at the borders when a label is being clamped
	public bool useMainCamera = true;   // Use the camera tagged MainCamera
	public Camera cameraToUse;   // Only use this if useMainCamera is false
	Camera cam;
	Transform thisTransform;
	Transform camTransform;

	/*****NAME STUFF****/
	public string myName;
	
	void Start (){
		//cameraToUse = GameObject.FindGameObjectWithTag("PlayerLabelCam").camera;

		thisTransform = transform;
		if (useMainCamera){
			cam = Camera.main;
		} else{
			cam = cameraToUse;
		}

		camTransform = cam.transform;
	}
	
	
	void Update(){
		if(!networkView.isMine){
			if(Vector3.Distance (camTransform.position, target.position) > 200){
				this.GetComponent<GUIText> ().enabled = false;//disable gui Text
			} else{
				this.GetComponent<GUIText> ().enabled = true;
				if (clampToScreen){
					Vector3 relativePosition = camTransform.InverseTransformPoint(target.position);//convert world space x,y,z to local space x,y,z
					relativePosition.z =  Mathf.Max(relativePosition.z, 1.0f);
					thisTransform.position = cam.WorldToViewportPoint(camTransform.TransformPoint(relativePosition + offset));
					thisTransform.position = new Vector3(Mathf.Clamp(thisTransform.position.x, clampBorderSize, 0.977f - clampBorderSize),
					                                     Mathf.Clamp(thisTransform.position.y, clampBorderSize, 1f - clampBorderSize),
					                                     thisTransform.position.z);

				} else{
					thisTransform.position = cam.WorldToViewportPoint(target.position + offset);//lerp this for smoothness
				}
			}
		}
	}
	public bool playerHasName;

	void GivePlayerName(){
		if(networkView.isMine){
			myName = GUI.TextField(new Rect(115f, 20.5f, 150f, 22.5f), myName, 25);
			if (Event.current.isKey && Event.current.keyCode == KeyCode.Return || GUI.Button (new Rect (0f, 20.5f, 100f, 22.5f), "Update Name")){
				networkView.RPC ("changeName", RPCMode.OthersBuffered, new object[]{myName});
				playerHasName = true;
			}
		}
	}

	void OnGUI(){
		if(!playerHasName){
			GivePlayerName();
		} else{
			//Debug.Log (Vector3.Distance (camTransform.position, target.position));
		}
	}

	public Color labelColor;

	Color CalcNameColor(){

		Color red = Color.red;
		Color blue = Color.blue;
		Color green = Color.green;
		Color cyan = Color.cyan;
		Color magenta = Color.magenta;
		int randomNum = Random.Range (0, 5);

		if(randomNum == 0){
			labelColor = red;
		} else if(randomNum == 1){
			labelColor = blue;
		} else if(randomNum == 2){
			labelColor = cyan;
		} else if(randomNum == 3){
			labelColor = green;
		} else if(randomNum == 4){
			labelColor = magenta;
		} else{
			labelColor = Color.white;
		}

		return labelColor;

	}

	[RPC]
	public void changeName(string pname){
		this.GetComponent<GUIText> ().text = pname;
		//change guiText Color
		Color guiTextColor = CalcNameColor ();
		this.GetComponent<GUIText> ().color = guiTextColor;
	}
}