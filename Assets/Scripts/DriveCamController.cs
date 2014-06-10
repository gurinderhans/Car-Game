using UnityEngine;
using System.Collections;

public class DriveCamController : MonoBehaviour {
	
	// the distance we want the camera to be behind the target
	public float distance;
	// the height we want the camera to be above the target
	public float height;
	
	// How much 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	public Transform target;//target to follow
	
	public GameObject car;//get the car for other stuff like speed
	
	public GameObject zoomPlane;//the sniper feature plane


	//car speed text gui stuff
	GameObject speedTextObj;
	GUIText speedText;

	
	void Start(){
		target = GameObject.Find ("Follow").transform;
		
		car = GameObject.FindGameObjectWithTag("Player");
		
		//the sniper feature disabled when this script is activated
		Color temp = zoomPlane.renderer.material.color;
		temp.a = 0;
		zoomPlane.renderer.material.color = temp;
		camera.fieldOfView = 60f;

		speedTextObj = GameObject.Find ("Car Speed");
		speedText = speedTextObj.guiText;
	}
	
	/*Private variables*/
	float wantedRotationAngle;
	float wantedHeight;
	float currentRotationAngle;
	float currentHeight;
	Quaternion currentRotation;
	
	
	void Update(){
		//get floats of distance and height of camera from car b/c each car has different values
		if(car.GetComponent<CarController>() != null){
			distance = car.GetComponent<CarController> ().carCamPosBehind;
			height = car.GetComponent<CarController> ().carCamPosUp;
		} else{
			//nothing
		}
		
		transform.position = Vector3.Lerp (transform.position, transform.position, Time.deltaTime);
		
	}
	
	void LateUpdate(){
		// Early out if we don't have a target
		if (!target)
			return;
		
		
		
		// Calculate the current rotation angles
		wantedRotationAngle = target.eulerAngles.y;
		wantedHeight = target.position.y + height;
		
		currentRotationAngle = transform.eulerAngles.y;
		currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		currentRotation = Quaternion.Euler (currentRotation.eulerAngles.x, currentRotationAngle, currentRotation.eulerAngles.x);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
		
		// Always look at the target
		transform.LookAt (target);


		int carVelocity = (int) car.rigidbody.velocity.magnitude * 2;
		string carVelocityToString = carVelocity.ToString ();
		speedText.text = carVelocityToString + " km/h";
	}
	
}