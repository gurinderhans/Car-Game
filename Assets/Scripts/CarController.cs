using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
	//Wheel Colliders
	public WheelCollider wheelFL;//front left
	public WheelCollider wheelFR;//front right
	public WheelCollider wheelRL;//rear left
	public WheelCollider wheelRR;//rear right
	
	//Transforms
	public Transform wheelFLTrans;//front left
	public Transform wheelFRTrans;//front right
	public Transform wheelRLTrans;//rear left
	public Transform wheelRRTrans;//rear right
	
	//Skidding variables
	float slipSidewayFriction;
	float slipForwardFriction;
	
	//torque
	float maxTorque;
	
	//deceleration by itself
	public int deceleration;
	
	//max speed
	public int maxSpeed;
	
	//the MAGIC VALUE
	float magicValue = 0.05f;//controls the time it takes for car to recover from drift:low->longer, high->faster
	
	public Transform forceUp;
	
	public Transform carBody;
	
	//Cheats On Car by MSK
	float torqueMultiplier=1;
	GameObject allScripts;
	bool forSingleJump;
	
	/*each car individual stuff*/
	public Transform cof;
	
	/*carcamera position stuff*/
	//we'll give these values to DriveCam for distance behind and up b/c its going to be different for each car
	public float carCamPosUp;
	public float carCamPosBehind;
	
	
	/*
	 * Cool IDEAS TODO
	 * remote control missile
	 * bullet that changes other players car controls temporarily
	 */
	
	// Use this for initialization
	void Start () {
		//rigidbody.centerOfMass = new Vector3 (0.0f, -0.9f, 0.0f);
		rigidbody.centerOfMass = cof.localPosition;
		//print(rigidbody.centerOfMass);
		slipForwardFriction = 0.05f;
		slipSidewayFriction = 0.018f;
		
		//little camera stuff
		//if(this.transform.gameObject.tag == "
		
		//For Cheats
		allScripts = GameObject.Find ("_SCRIPTS");
		
	}
	
	void Update(){
		//make wheels spin
		wheelFLTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);
		wheelFRTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);
		wheelRLTrans.Rotate (0,0,wheelFL.rpm / 60 * 360 * Time.deltaTime);
		wheelRRTrans.Rotate (0, 0, wheelFL.rpm / 60 * 360 * Time.deltaTime);
		
		//wheel rotate
		wheelFLTrans.localEulerAngles = new Vector3 (wheelFLTrans.localEulerAngles.x, wheelFL.steerAngle, wheelFLTrans.localEulerAngles.z);//changing just y and leaving alone x & z
		wheelFRTrans.localEulerAngles = new Vector3 (wheelFRTrans.localEulerAngles.x, wheelFR.steerAngle, wheelFRTrans.localEulerAngles.z);
		
		//print (rigidbody.velocity.magnitude);
		
		//Cheats need to be added in Update or it causes errors.
		//Besides it does not move car, it only changes the multiple for torque which is used in fixedUpdate
		//but this needs to called every frame. So leave it here.
		
		CheatsControl ();
	}
	
	void CheatsControl(){
		//cheats in Use
		AllCheats cheats = allScripts.GetComponent<AllCheats>();
		if(Input.GetKeyDown(KeyCode.LeftShift) && cheats.nitroAllowed){
			maxSpeed*=4;
			torqueMultiplier=4;
		}
		
		if(Input.GetKeyUp(KeyCode.LeftShift) && cheats.nitroAllowed){
			maxSpeed/=4;
			torqueMultiplier=1;
		}
		
		if(Input.GetKeyDown(KeyCode.E) && cheats.jumpAllowed){
			rigidbody.AddForce(Vector3.up*1000000);
			ejump = true;
		}
		airplaneMode = cheats.airFly;
		forSingleJump = !cheats.jumpAllowed;
		//DeveloperMenuOpened = cheats.showDeveloperMenu;
	}
	
	bool airplaneMode;
	
	void WheelPosition(){
		RaycastHit hit;
		Vector3 wheelPos;
		
		//Raycast(object, direction, what it hits, length of raycast);
		if(Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, out hit,wheelFL.radius + wheelFL.suspensionDistance)){
			//hit.point is the point the raycast is hitting
			wheelPos = hit.point + wheelFL.transform.up * wheelFL.radius;
			//Debug.DrawRay(wheelFL.transform.position, new Vector3(0, -(wheelFL.radius + wheelFL.suspensionDistance), 0), Color.red);
		} else{
			wheelPos = wheelFL.transform.position - wheelFL.transform.up * wheelFL.suspensionDistance;
		}
		wheelFLTrans.position = wheelPos;
		
		//Raycast(object, direction, what it hits, length of raycast);
		if(Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, out hit,wheelFR.radius + wheelFR.suspensionDistance)){
			//hit.point is the point the raycast is hitting
			wheelPos = hit.point + wheelFR.transform.up * wheelFR.radius;
		} else{
			wheelPos = wheelFR.transform.position - wheelFR.transform.up * wheelFR.suspensionDistance;
		}
		wheelFRTrans.position = wheelPos;
		
		
		//Raycast(object, direction, what it hits, length of raycast);
		if(Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up, out hit,wheelRL.radius + wheelRL.suspensionDistance)){
			//hit.point is the point the raycast is hitting
			wheelPos = hit.point + wheelRL.transform.up * wheelRL.radius;
		} else{
			wheelPos = wheelRL.transform.position - wheelRL.transform.up * wheelRL.suspensionDistance;
		}
		wheelRLTrans.position = wheelPos;
		
		
		//Raycast(object, direction, what it hits, length of raycast);
		if(Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up, out hit,wheelRR.radius + wheelRR.suspensionDistance)){
			//hit.point is the point the raycast is hitting
			wheelPos = hit.point + wheelRR.transform.up * wheelRR.radius;
		} else{
			wheelPos = wheelRR.transform.position - wheelRR.transform.up * wheelRR.suspensionDistance;
		}
		wheelRRTrans.position = wheelPos;
		
		
	}
	
	//set slip function
	void MakeSlip (float forwardFriction , float sidewayFriction){
		/*<WheelFrictionCurve> is a struct so we cant change it directly
		 * So first we get the <forwardFriction> var type from it
		 * then <stiffness> attribute gets copied and is assigned a value
		 * We at the end 'wheelRR.forwardFriction = t2;' assign the <forwardFriction> type the attribute copy we had
		 */
		WheelFrictionCurve t1 = wheelRR.forwardFriction;
		t1.stiffness = forwardFriction;
		
		WheelFrictionCurve t2 = wheelRL.forwardFriction;
		t2.stiffness = forwardFriction;
		
		WheelFrictionCurve t3 = wheelRR.sidewaysFriction;
		t3.stiffness = sidewayFriction;
		
		WheelFrictionCurve t4 = wheelRL.sidewaysFriction;
		t4.stiffness = sidewayFriction;
		
		wheelRR.forwardFriction = t1;
		wheelRL.forwardFriction = t2;
		wheelRR.sidewaysFriction = t3;
		wheelRL.sidewaysFriction = t4;
	}
	
	//simple function for organization that determines the slip value for drfiting
	void CalcStiffness(){
		int carSpeed = (int)rigidbody.velocity.magnitude;
		if(carSpeed <= 70f && carSpeed >= 15f){
			slipSidewayFriction = 0.019f;
			MakeSlip(slipForwardFriction, slipSidewayFriction);
		} else if(carSpeed > 70f && carSpeed <= maxSpeed){
			slipSidewayFriction = 0.022f;
			MakeSlip(slipForwardFriction, slipSidewayFriction);
		} else if(carSpeed < 15f){
			MakeSlip(Mathf.Lerp(wheelRL.forwardFriction.stiffness, 1f, Time.deltaTime*magicValue), Mathf.Lerp(wheelRL.sidewaysFriction.stiffness, 1f, Time.deltaTime*magicValue));
		}
		//print ("SIDEWAYS FRIC: "+wheelRL.sidewaysFriction.stiffness+" FORWARD FRIC: "+wheelRL.forwardFriction.stiffness);
	}
	
	void Drift(){
		/*
		 * For car speed 0-70 slipSidewayFriction = 0.01;
		 * speed more than 70 - 100 slipSidewayFriction = 0.05
		 * speed more than 100 - 150 slipSidewayFriction = 0.09
		 */
		
		if(Input.GetAxis("Vertical") == 0){
			
			if(Input.GetButton("Brake")){
				wheelRL.brakeTorque = 100f;
				wheelRR.brakeTorque = 100f;
				CalcStiffness();
			} else{
				wheelRL.brakeTorque = deceleration;
				wheelRR.brakeTorque = deceleration;
				MakeSlip(Mathf.Lerp(wheelRL.forwardFriction.stiffness, 1f, Time.deltaTime*magicValue), Mathf.Lerp(wheelRL.sidewaysFriction.stiffness, 1f, Time.deltaTime*magicValue));
			}
		} else if(Input.GetAxis("Vertical") != 0f){
			if(Input.GetButton("Brake")){
				wheelRL.brakeTorque = 150f;//this value should be greater than brake torque on brake pressed when no accleratopm
				wheelRR.brakeTorque = 150f;
				CalcStiffness();
			} else{
				wheelRL.brakeTorque = 0f;
				wheelRR.brakeTorque = 0f;
				MakeSlip(Mathf.Lerp(wheelRL.forwardFriction.stiffness, 1f, Time.deltaTime*magicValue), Mathf.Lerp(wheelRL.sidewaysFriction.stiffness, 1f, Time.deltaTime*magicValue));
			}
		}
		
		//print (wheelRL.brakeTorque);
		//print ("SIDEWAYS FRIC: "+wheelRL.sidewaysFriction.stiffness+" FORWARD FRIC: "+wheelRL.forwardFriction.stiffness);
	}
	
	bool ejump, onGround;
	void CalcDownForceOnCar(){
		RaycastHit hit;
		int carSpeed = (int) rigidbody.velocity.magnitude;
		//Raycast(object, direction, what it hits, length of raycast);
		if(Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, out hit,wheelFL.radius + wheelFL.suspensionDistance)){
			//hit.point is the point the raycast is hitting
			rigidbody.AddForce(0,carSpeed*1000*-1,0);
			//print ("1000");
			
			//by me
			onGround=true;
		} else{//car is in air
			int force = 175;
			rigidbody.AddForce(0,carSpeed*force*-1,0);
			if(!ejump){
				rigidbody.AddForceAtPosition(new Vector3(0, carSpeed*6,0), forceUp.position);
			}
			//print (force);
			
			//by me
			onGround=false;
		}
	}
	
	public bool jumped;
	public float jumpTimer;
	void SingleJump(){
		if(forSingleJump){
			if(!jumped){
				jumpTimer = 0f;
				if(Input.GetKeyDown(KeyCode.E)){
					//print (canJumpTimer);
					rigidbody.AddForce(Vector3.up*1000000);
					jumped = true;
					ejump = true;
				}
			}
			
			if(jumped){
				jumpTimer += Time.deltaTime;
				//print (jumpTimer);
				if(jumpTimer > 10f){
					jumped = false;
					ejump = false;
				}
				//jumped = false;
			}
		}
	}
	
	void CarBodyMove(){
		int carSpeed = (int) rigidbody.velocity.magnitude;
		//transform.rotation = Quaternion.identity; <- look at this later on
		if(carSpeed != 0){
			if(wheelFL.steerAngle > 0){//make this for slow speeds and increase 0.25 to about 0.35 for speeds larger thatn 70-80
				float angle = wheelFL.steerAngle * -(Time.deltaTime + 0.1f);
				carBody.localRotation = Quaternion.Slerp(carBody.localRotation, Quaternion.Euler(angle, 0f, 0f), Time.deltaTime * 10f);
			} else if(wheelFL.steerAngle < 0){
				float angle = wheelFL.steerAngle * -(Time.deltaTime + 0.1f);
				carBody.localRotation = Quaternion.Slerp(carBody.localRotation, Quaternion.Euler(angle, 0f, 0f), Time.deltaTime * 10f);
			} else{
				carBody.localRotation = Quaternion.Slerp(carBody.localRotation, Quaternion.identity, Time.deltaTime * 5f);
			}
		} else{
			carBody.localRotation = Quaternion.Slerp(carBody.localRotation, Quaternion.identity, Time.deltaTime);
		}
		///print (carBody.rotation);
	}
	//is called multiple times per frame ;)
	void FixedUpdate(){
		//if(Drift()){ slow down car -> add very little opposing force to slow it down}
		//if car currently drifting and only Input.GetAxis("Vertical") is pressed then speed it to make it go forward
		//print (wheelRL.brakeTorque);
		//all functions for car
		
		Drift ();
		WheelPosition ();
		CalcDownForceOnCar ();
		SingleJump ();//for the single car jump
		CarBodyMove ();
		
		
		
		//max speed
		//int carSpeed = (int) Mathf.Abs(2 * Mathf.PI * wheelRR.radius * wheelRR.rpm * 60 / 1000);
		int carSpeed = (int) rigidbody.velocity.magnitude;
		//print (carSpeed);
		if(carSpeed < maxSpeed){
			maxTorque = 30f * torqueMultiplier;
			wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
			wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		} else{
			wheelRL.motorTorque = 0;
			wheelRR.motorTorque = 0;
		}
		
		//rigidbody.AddForce(0,carSpeed*1000*-1,0);
		
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
		
		
		
		//decelerating when oppsite direction button pressed to direction of motion
		//wheel.rpm and wheel.motortorque
		//print (wheelRR.motorTorque);
		
		if(wheelRR.rpm > 0 && wheelRR.motorTorque < 0  ||  wheelRR.rpm < 0 && wheelRR.motorTorque > 0){
			//if car is moving forward but has negative torque getting applied to it && other way -> slow the car down
			maxTorque = 35.0f  * torqueMultiplier;
			wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
			wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
		}
		
		//controlling the max back speed
		if(wheelRR.rpm != 0){
			if (wheelRR.rpm < 0 && wheelRR.motorTorque < 0){
				if(carSpeed < 30){
					maxTorque = 25.0f * torqueMultiplier;
					wheelRL.motorTorque = maxTorque * Input.GetAxis ("Vertical");
					wheelRR.motorTorque = maxTorque * Input.GetAxis ("Vertical");
				} else{
					wheelRL.motorTorque = 0;
					wheelRR.motorTorque = 0;
				}
			}
		}
		
		if(airplaneMode && !onGround){
			transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal"));
			print (transform.localEulerAngles);
			rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxisRaw ("Horizontal") * 150 * carSpeed);
			print (rigidbody.velocity.magnitude+" and "+rigidbody.angularVelocity.magnitude);
		}
		
		//reset
		if(Input.GetKey(KeyCode.Q)){
			transform.position = new Vector3(0, 15, 0);
			transform.rotation = Quaternion.identity;
			rigidbody.Sleep ();
		}
		
		/*
		 * When max speed reached and if you hold back key without letting go of front key car will lock at max speed
		 * 
		 */
	}
	
	/*bool DeveloperMenuOpened;
	
	void ChangeTag(){
		if(gameObject.tag=="Player") gameObject.tag="raycastTarget";
		else gameObject.tag="Player";
	}
	
	void OnGUI(){
		if(DeveloperMenuOpened){
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+200,200,35),"Current Tag is "+gameObject.tag)) ChangeTag();
		}
	}*/
}