using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {


	public float shootForce;
	GameObject turret;

	public Transform shoot_bullet_from_pos;

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
					GameObject mySphere=(GameObject) Network.Instantiate(Resources.Load("Bullet"),shoot_bullet_from_pos.position, transform.rotation,0);
					mySphere.rigidbody.AddRelativeForce(Vector3.forward * shootForce);
				}
			}
			else if(Input.GetMouseButton(0)){
				GameObject mySphere=(GameObject) Network.Instantiate(Resources.Load("Bullet"),shoot_bullet_from_pos.position, transform.rotation,0);
				mySphere.rigidbody.AddRelativeForce(Vector3.forward * shootForce);
			}
		}
	}

}
