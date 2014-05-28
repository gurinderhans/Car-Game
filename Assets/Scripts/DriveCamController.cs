using UnityEngine;
using System.Collections;

public class DriveCamController : MonoBehaviour {

	// the distance we want the camera to be behind the target
	float distance;
	// the height we want the camera to be above the target
	float height;

	// How much 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	public Transform target;//target to follow
	
	public GameObject car;//get the car for other stuff like speed
	
	
	void Start(){
		target = GameObject.Find ("Follow").transform;
		
		car = GameObject.FindGameObjectWithTag("Player");
		
	}

	/*Private variables*/
	float wantedRotationAngle;
	float wantedHeight;
	float currentRotationAngle;
	float currentHeight;
	Quaternion currentRotation;

	
	void Update(){
		//get floats of distance and height of camera from car b/c each car has different values
		distance = car.GetComponent<CarController> ().carCamPosBehind;
		height = car.GetComponent<CarController> ().carCamPosUp;
		
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
		
	}

}