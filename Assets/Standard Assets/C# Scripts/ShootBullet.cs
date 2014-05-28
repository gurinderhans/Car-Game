using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {


	public float shootForce;
	GameObject turret;

	public Transform shoot_bullet_from;

	public float myPoints = 0f;

	public bool iGetPoint;

	public float shootLength;

	GUIText updateToPlayer;

	public float cannonForce;


	void Start(){
		updateToPlayer = GameObject.FindGameObjectWithTag ("playerUpdates").guiText;

		turret = GameObject.FindGameObjectWithTag ("Gun");
	}

	void OnGUI(){
		GUI.Box (new Rect (0,Screen.height - 50,100,50), myPoints.ToString() +": points");
	}

	void Update () {
		if(Time.timeScale!=0){
			transform.rotation=turret.transform.rotation;
			transform.position=turret.transform.position;
			
			if(Input.GetMouseButton(1)){
				networkView.RPC ("SmartFire", RPCMode.All);
			} else if(Input.GetMouseButtonDown(0)){
				GameObject myBullet=(GameObject) Instantiate(Resources.Load("Bullet"),transform.position, transform.rotation);
				myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);

				networkView.RPC ("PlayerFire", RPCMode.All);
				smartFire = false;
			} else{
				smartFire = false;
			}
			Debug.DrawRay(shoot_bullet_from.position, transform.forward*shootLength);
		}
	}

	public bool smartFire;

	[RPC]
	void SmartFire(){
		RaycastHit hit;
		//need shoot_bullet_from transform because gun position isnt same as crosshair position on screen
		//also still need to change shoot_bullet_from position little more up so it falls very close tot crosshair pos
		
		if(Physics.Raycast(shoot_bullet_from.position, transform.forward, out hit, shootLength)){//make ray length larger
			//hit.transform.gameObject.tag;
			//string objHit = hit.transform.gameObject.tag;
			//print (objHit);
			//updateToPlayer.text = "test";
			//print (hit.point);
			//print (hit.transform.name);
			if(hit.transform.gameObject.tag == "Player"){
				smartFire = true;
				//well do a big bullet here or somehting
				//instantiating multiple bullets wrecks the whole animation with the crosshair rotating

				//GameObject myBullet=(GameObject) Instantiate(Resources.Load("Bullet"),shoot_bullet_from.position, transform.rotation);
				//myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);

				GameObject myShootFX = (GameObject) Instantiate(Resources.Load("ShootFXLineRenderer"), Vector3.zero, Quaternion.identity);
				LineRenderer myLR = myShootFX.GetComponent<LineRenderer>();
				myLR.SetPosition(0, shoot_bullet_from.position);
				myLR.SetPosition(1, hit.point);


				/*GameObject myBullet=(GameObject) Instantiate(Resources.Load("Bullet"),shoot_bullet_from.position, transform.rotation);
				myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);*/


				//print (hit.point);

				/*GameObject myTrailRend = (GameObject) Instantiate(Resources.Load("ShootFXTrailRenderer"), shoot_bullet_from.position, Quaternion.identity);
				myTrailRend.transform.position = Vector3.Lerp(myTrailRend.transform.position, hit.point);*/


				//print(hit.transform.GetComponent<Health>().health);
				hit.transform.gameObject.GetComponent<Health>().hit = true;
				//print (hit.transform.gameObject.GetComponent<Health>().health);
				if(hit.transform.gameObject.GetComponent<Health>().health < 30){//this isnt quick enough, need another method for chekcing if we have killed another player
					//usually the last health update we get is 20 so we kinda have to assume player has died when health goes below 30 on our side
					//print ("Killed other player");


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
				//updateToPlayer.text = "test";
				print (myPoints);
			}
			
			
		} else{
			smartFire = false;
		}
		Debug.DrawRay(shoot_bullet_from.position, transform.forward * shootLength);
	}


	[RPC]
	void PlayerFire(){
		RaycastHit hit;
		//need shoot_bullet_from transform because gun position isnt same as crosshair position on screen
		//also still need to change shoot_bullet_from position little more up so it falls very close tot crosshair pos

		if(Physics.Raycast(shoot_bullet_from.position, transform.forward, out hit, shootLength)){//make ray length larger
			//hit.transform.gameObject.tag;
			//string objHit = hit.transform.gameObject.tag;
			//print (objHit);


			//print (hit.point);
			//print (hit.transform.name);
			if(hit.transform.gameObject.tag == "Player"){
				//print(hit.transform.GetComponent<Health>().health);
				hit.transform.gameObject.GetComponent<Health>().hit = true;
				//print (hit.transform.gameObject.GetComponent<Health>().health);
				if(hit.transform.gameObject.GetComponent<Health>().health < 30){//this isnt quick enough, need another method for chekcing if we have killed another player
					//usually the last health update we get is 20 so we kinda have to assume player has died when health goes below 30 on our side
					//print ("Killed other player");
					updateToPlayer.text = "Killed " + hit.transform.gameObject.GetComponentInChildren<GUIText>().text;
					iGetPoint = true;
				}
			}

			if(iGetPoint){//not very fast either but for now its good as well
				iGetPoint = false;
				myPoints += 5f;//add 5 points for each kill kinda~~
				print (myPoints);
			}


		} else{
			//print ("NOTHIGN");
		}
		Debug.DrawRay(shoot_bullet_from.position, transform.forward * shootLength);
		//print (transform.TransformDirection(Vector3.forward) * 100);
	}

}
