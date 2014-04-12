using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
	//Wheel Colliders
	public WheelCollider wheelFL;//front left
	public WheelCollider wheelFR;//front right
	public WheelCollider wheelRL;//rear left
	public WheelCollider wheelRR;//rear right
	
	//transforms
	public Transform wheelFLTrans;//front left
	public Transform wheelFRTrans;//front right
	public Transform wheelRLTrans;//rear left
	public Transform wheelRRTrans;//rear right

	//incoming
	//public Quaternion wheelFront;
	
	//torque
	private float maxTorque;
	//deceleration by itself
	public int deceleration;
	//max speed
	public int maxSpeed;
	
	// Use this for initialization
	void Start () {
		rigidbody.centerOfMass = new Vector3 (0.0f, -0.9f, 0.0f);
		//print(rigidbody.centerOfMass);
	}
	
	void Update(){
		//make wheels spin
		wheelFLTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);
		wheelFRTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);
		wheelRLTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);
		wheelRRTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);


		//wheel rotate
		wheelFLTrans.localEulerAngles = new Vector3 (wheelFLTrans.localEulerAngles.x, wheelFL.steerAngle, wheelFLTrans.localEulerAngles.z);//changing just y and leaving alone x & z
		wheelFRTrans.localEulerAngles = new Vector3 (wheelFRTrans.localEulerAngles.x, wheelFR.steerAngle, wheelFRTrans.localEulerAngles.z);

		//print (rigidbody.velocity.magnitude);
	}

	
	//is called multiple times per frame ;)
	void FixedUpdate(){
		//wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		//wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		//add steer angle to front wheels to make them turn -> car turn

		/*wheelFL.steerAngle = 7 * Input.GetAxis ("Horizontal");
		wheelFR.steerAngle = 7 * Input.GetAxis ("Horizontal");*/
		
		//check for car moving if no key pressed start slowing down
		//this is also for brakes
		if(Input.GetButton("Brake")){//brake button defined as space in project settings
			wheelRL.brakeTorque = 100;
			wheelRR.brakeTorque = 100;
		} else if(Input.GetAxis("Vertical") == 0){
			wheelRL.brakeTorque = deceleration;
			wheelRR.brakeTorque = deceleration;
		} else{
			wheelRL.brakeTorque = 0.0f;
			wheelRR.brakeTorque = 0.0f;
		}

		//max speed
		//int carSpeed = (int) Mathf.Abs(2 * Mathf.PI * wheelRR.radius * wheelRR.rpm * 60 / 1000);
		int carSpeed = (int) rigidbody.velocity.magnitude;
		//print (carSpeed);
		if(carSpeed < maxSpeed){
			maxTorque = 25.0f;
			wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
			wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		} else{
			wheelRL.motorTorque = 0;
			wheelRR.motorTorque = 0;
		}

		rigidbody.AddForce(0,carSpeed*1000*-1,0);

		//steer Control -> more steer angle if car is slow and less if fast //default for fast and more for slow
		/*
		 * steer angle should be from 7 -> 10
		 * create function that computes value from 7 -> 10
		 * car speed goes from 0 -> 85
		 * max speed = 85
		 * (maxspeed - carspeed) + some number to give value close to 7->10
		 */

		float steerAngleforCar = (((maxSpeed - carSpeed)+1f)/10f)/2f;
		steerAngleforCar = steerAngleforCar + 8f;
		//print (steerAngleforCar);
		wheelFL.steerAngle = steerAngleforCar * Input.GetAxis ("Horizontal");
		wheelFR.steerAngle = steerAngleforCar * Input.GetAxis ("Horizontal");

		/*if(carSpeed < 50){//mess around with value 100
			wheelFL.steerAngle = 12 * Input.GetAxis ("Horizontal");
			wheelFR.steerAngle = 12 * Input.GetAxis ("Horizontal");
			print(12);
		} else{
			wheelFL.steerAngle = 10 * Input.GetAxis ("Horizontal");
			wheelFR.steerAngle = 10 * Input.GetAxis ("Horizontal");
			print (10);
		}*/


		//decelerating when oppsite direction button pressed to direction of motion
		//wheel.rpm and wheel.motortorque
		//print (wheelRR.motorTorque);

		if(wheelRR.rpm > 0 && wheelRR.motorTorque < 0  ||  wheelRR.rpm < 0 && wheelRR.motorTorque > 0){
			//if car is moving forward but has negative torque getting applied to it && other way -> slow the car down
			maxTorque = 35.0f;//make 35 later
			wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
			wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		}

		//controlling the max back speed
		if(wheelRR.rpm != 0){
			if (wheelRR.rpm < 0 && wheelRR.motorTorque < 0){
				if(carSpeed < 30){
					maxTorque = 25.0f;
					wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
					wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
				} else{
					wheelRL.motorTorque = 0;
					wheelRR.motorTorque = 0;
				}
			}
		}

		//reset
		if(Input.GetKeyDown("q")){
			transform.position = new Vector3(0, 15, 0);
			transform.rotation = Quaternion.identity;
		}
		/*
		 * When max speed reached and if you hold back key without letting go of front key car will lock at max speed
		 * 
		 */
	}
}