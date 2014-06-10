using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {
	
	
	public float shootForce;
	
	public Transform shoot_ray_from;
	public float myPoints = 0f;
	public bool iGetPoint;
	public float shootLength;
	//GUIText updateToPlayer;
	public Transform shoot_bullet_from;

	public bool playerKill;
	public int kills;

	/*Few Checks*/
	public bool pointsUpdated;

	
	void Start(){
		//updateToPlayer = GameObject.FindGameObjectWithTag ("playerUpdates").guiText;
	}
	
	void Update () {
		if(Time.timeScale!=0){
			//print (playerKill);

			if(playerKill){
				kills += Mathf.Clamp(1, 1,1);
			}

			if(Input.GetMouseButton(1)){
				networkView.RPC ("SmartFire", RPCMode.All);
			} else if(Input.GetMouseButtonDown(0)){
				GameObject myBullet=(GameObject) Instantiate(Resources.Load("Bullet"), shoot_bullet_from.position, transform.rotation);
				myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);

				//then call the RPC
				networkView.RPC ("PlayerFire", RPCMode.All);
				smartFire = false;
			} else{
				pointsUpdated = false;
				smartFire = false;
				playerKill = false;
			}
			Debug.DrawRay(shoot_ray_from.position, shoot_ray_from.transform.forward * shootLength, Color.red);
		}
	}


	[RPC]
	void PlayerFire(){
		RaycastHit hit;

		if(Physics.Raycast(shoot_ray_from.position, shoot_ray_from.transform.forward, out hit, 2000)){//make ray length larger
			if(hit.transform.gameObject.tag == "Player"){

				hit.transform.gameObject.GetComponentInChildren<Health>().hit = true;
				print ("From NetworkPlayer's perspective : " +hit.transform.gameObject.GetComponentInChildren<Health>().health);
				if(hit.transform.gameObject.GetComponentInChildren<Health>().health < 30){
					
					//updateToPlayer.text = "Killed " + hit.transform.gameObject.GetComponentInChildren<GUIText>().text;
					iGetPoint = true;
					playerKill = true;
				} else{
					playerKill = false;
				}
			}
			if(iGetPoint){//not very fast either but for now its good as well
				iGetPoint = false;
				myPoints += 5f;//add 5 points for each kill kinda it usually adds 10 or more~~
				pointsUpdated = true;
			} else pointsUpdated = false;
		}
		
		Debug.DrawRay(shoot_ray_from.position, shoot_ray_from.transform.forward * shootLength, Color.blue);
	}

	public bool smartFire;
	
	[RPC]
	void SmartFire(){
		RaycastHit hit;

		if(Physics.Raycast(shoot_ray_from.position, shoot_ray_from.transform.forward, out hit, 2000)){//make ray length larger
			if(hit.transform.gameObject.tag == "Player"){
				smartFire = true;

				//line renderer stuff
				GameObject myShootFX = (GameObject) Instantiate(Resources.Load("ShootFXLineRenderer"), Vector3.zero, Quaternion.identity);
				LineRenderer myLR = myShootFX.GetComponent<LineRenderer>();
				myLR.SetPosition(0, shoot_bullet_from.position);
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
				pointsUpdated = true;
				//updateToPlayer.text = "test"; //will be using this soon too.
			}
		} else{
			smartFire = false;
		}
	}
	
}