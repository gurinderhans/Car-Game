using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {


	public float shootForce;
	GameObject turret;

	public Transform shoot_bullet_from;

	public float myPoints = 0f;

	public bool iGetPoint;

	//public GameObject myCar;
	
	void Start(){
		turret=GameObject.Find("gun TBS 001C");
	}
	
	void Update () {
		if(Time.timeScale!=0){
			transform.rotation=turret.transform.rotation;
			transform.position=turret.transform.position;
			
			CrossHair scriptCrossHair=this.GetComponent<CrossHair>();
			
			if(Input.GetMouseButton(1)){
				if(scriptCrossHair.whoIsIt=="Red"){
					//GameObject myBullet=(GameObject) Network.Instantiate(Resources.Load("Bullet"),shoot_bullet_from.position, transform.rotation,0);
					//myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);
				}
			} else if(Input.GetMouseButtonDown(0)){
				GameObject myBullet=(GameObject) Instantiate(Resources.Load("Bullet"),shoot_bullet_from.position, transform.rotation);
				myBullet.rigidbody.AddRelativeForce(Vector3.forward * shootForce);
				networkView.RPC ("PlayerFire", RPCMode.All);
			}
			Debug.DrawRay(shoot_bullet_from.position, transform.forward*100);
		}
	}


	[RPC]
	void PlayerFire(){
		RaycastHit hit;
		if(Physics.Raycast(shoot_bullet_from.position, transform.forward, out hit, 300)){//make ray length larger
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
					iGetPoint = true;
				}
			}

			if(iGetPoint){//not very fast either but for now its good as well
				iGetPoint = false;
				myPoints += 10f;//add 10 points for each kill
				print (myPoints);
			}


		} else{
			//print ("NOTHIGN");
		}
		Debug.DrawRay(shoot_bullet_from.position, transform.forward*300);
		//print (transform.TransformDirection(Vector3.forward) * 100);
	}

}
