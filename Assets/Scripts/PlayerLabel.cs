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
	//public string myName;

	//find _SCRIPTS GameObj
	GameObject all_scripts;

	bool sendInitNameChangeRPC = true;//we only want the RPC call to go once in the start when name is changed
	
	void Start (){
		thisTransform = transform;
		if (useMainCamera){
			cam = Camera.main;
		} else{
			cam = cameraToUse;
		}

		camTransform = cam.transform;

		all_scripts = GameObject.Find ("_SCRIPTS");
	}
	
	
	void Update(){
		if(!networkView.isMine){//to hide gui text on own player screen
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

		if(networkView.isMine){
			if(all_scripts.GetComponent<NetworkManager>().playerHasName && sendInitNameChangeRPC){
				//print ("yes name");
				//print (all_scripts.GetComponent<NetworkManager>().myName);
				sendInitNameChangeRPC = false;
				print ("Changin Name");
				networkView.RPC("changeName", RPCMode.AllBuffered, new object[]{all_scripts.GetComponent<NetworkManager>().myName});
			}
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