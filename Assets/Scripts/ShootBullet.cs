using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {
	
	
	public float shootForce;
	
	public Transform shoot_bullet_from;
	public float myPoints = 0f;
	public bool iGetPoint;
	public float shootLength;
	//GUIText updateToPlayer;
	public Transform instantiateBulletFrom;
	public GUIStyle myStyle;

	//get the gunCam
	public Transform gunCamera;

	/******************************************************************
	******************************************************************
	******************************************************************
	******************************************************************
	******************************************************************
	******************************************************************

	***********NOTE => Create a seperate script which controls all the script turning on and off***************

	******************************************************************
	******************************************************************
	******************************************************************
	******************************************************************
	******************************************************************/

	
	void Start(){
		//updateToPlayer = GameObject.FindGameObjectWithTag ("playerUpdates").guiText;
		
	}

	Vector3 rayOriginPos;

	void OnGUI(){
		GUI.Label (new Rect (0,Screen.height - 50,100,50), myPoints.ToString() +": points", myStyle);
	}
	
	void Update () {
		//gunCamera = this.GetComponent<GunMovement> ().myCamera.transform.parent.gameObject.transform;

		if(gunCamera == null)
			return;

		rayOriginPos = gunCamera.position;
		//print ("SHOOT BULLET "+rayOriginPos);

		if(Time.timeScale!=0){
			
			if(Input.GetMouseButton(1)){
				networkView.RPC ("SmartFire", RPCMode.All);
			} else if(Input.GetMouseButtonDown(0)){
				GameObject myBullet=(GameObject) Instantiate(Resources.Load("Bullet"),instantiateBulletFrom.position, transform.rotation);
				myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);
				
				networkView.RPC ("PlayerFire", RPCMode.All);
				smartFire = false;
			} else{
				smartFire = false;
			}
			Debug.DrawRay(rayOriginPos, transform.forward * shootLength);
		}
	}
	
	public bool smartFire;
	
	[RPC]
	void SmartFire(){
		RaycastHit hit;
		//need shoot_bullet_from transform because gun position isnt same as crosshair position on screen
		//also still need to change shoot_bullet_from position little more up so it falls very close to crosshair pos
		
		if(Physics.Raycast(shoot_bullet_from.position, transform.forward, out hit, shootLength)){//make ray length larger
			if(hit.transform.gameObject.tag == "Player"){
				smartFire = true;
				
				GameObject myShootFX = (GameObject) Instantiate(Resources.Load("ShootFXLineRenderer"), Vector3.zero, Quaternion.identity);
				LineRenderer myLR = myShootFX.GetComponent<LineRenderer>();
				myLR.SetPosition(0, instantiateBulletFrom.position);
				myLR.SetPosition(1, hit.point);
				
				
				hit.transform.gameObject.GetComponentInChildren<Health>().hit = true;
				if(hit.transform.gameObject.GetComponentInChildren<Health>().health < 30){//*UPDATE : FINE FOR NOW IGNORE -> (this isnt quick enough, need another method for chekcing if we have killed another player)
					//usually the last health update we get is 20 so we kinda have to assume player has died when health goes below 30 on our side
					
					//will be using this very soon
					//updateToPlayer.text = "Killed " + hit.transform.gameObject.GetComponentInChildren<GUIText>().text;
					
					//right now on other player we will get a null refernce error beacuse we are updating it from our side but not on others even though 
					//its the same guiText we are changing so to fix this we can instantiate the GUIText object callled PlayerUpdates
					
					iGetPoint = true;
				}
			} else{
				smartFire = false;
			}

			if(iGetPoint){//not very fast either but for now its good as well
				iGetPoint = false;
				myPoints += 5f;//add 5 points for each kill kinda~~
				//updateToPlayer.text = "test"; //will be using this soon too.
			}
			
		} else{
			smartFire = false;
		}
		//Debug.DrawRay(shoot_bullet_from.position, transform.forward * shootLength);
	}
	
	
	[RPC]
	void PlayerFire(){
		RaycastHit hit;
		//need shoot_bullet_from transform because gun position isnt same as crosshair position on screen
		//also still need to change shoot_bullet_from position little more up so it falls very close tot crosshair pos

		if(Physics.Raycast(rayOriginPos, transform.forward, out hit, shootLength)){//make ray length larger
			//print("player hit status: "+hit.transform.gameObject.GetComponentInChildren<Health>().hit);
			print ("I hit " + hit.transform.gameObject.tag + "with tag "+hit.transform.gameObject.tag);
			if(hit.transform.gameObject.tag == "Player"){
				hit.transform.gameObject.GetComponentInChildren<Health>().hit = true;
				if(hit.transform.gameObject.GetComponentInChildren<Health>().health < 30){
					
					//updateToPlayer.text = "Killed " + hit.transform.gameObject.GetComponentInChildren<GUIText>().text;
					iGetPoint = true;
				}
			}
			if(iGetPoint){//not very fast either but for now its good as well
				iGetPoint = false;
				myPoints += 5f;//add 5 points for each kill kinda it usually adds 10 or more~~
			}
		}

		Debug.DrawRay(rayOriginPos, transform.forward * shootLength, Color.green);
	}
	
}