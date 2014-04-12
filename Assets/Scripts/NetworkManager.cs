using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public Transform spawnZero;
	public Transform spawnOne;
	public Transform spawnTwo;
	public Transform spawnThree;
	public Transform spawnFour;
	public Transform spawnFive;

	private Vector3 spawnPoint;

	public GUIText connState;

	public GameObject cam;

	public Camera disableCam;

	public GUIText hideText;

	//public Transform wheelFL;

	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	// Update is called once per frame
	void Update () {
		connState.text = PhotonNetwork.connectionStateDetailed.ToString ();
		//Debug.Log (transform.Find("FLWheel"));
	}
	
	void Connect(){
		PhotonNetwork.ConnectUsingSettings ("Multi v0.1");
	}
	
	void OnGUI(){
		//GUILayout.Box (PhotonNetwork.connectionStateDetailed.ToString ());
		//GUI.Label(new Rect(0, 0, 200, 40), ""+PhotonNetwork.connectionStateDetailed.ToString ()+"");
		print (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	
	void OnJoinedLobby(){
		//Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();//we need to create room
	}
	
	void OnPhotonRandomJoinFailed(){//if joining room failed
		//if failed create new room
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");//spawn player
		SpawnPlayer ();
	}
	
	void SpawnPlayer(){
		int randomNum = Random.Range (0, 5);
		if(randomNum == 0){
			print ("Spawn Zero");
			spawnPoint = spawnZero.position;
		} else if(randomNum == 1){
			print("Spawn One");
			spawnPoint = spawnOne.position;
		} else if(randomNum == 2){
			print("Spawn Two");
			spawnPoint = spawnTwo.position;
		} else if(randomNum == 3){
			print("Spawn Three");
			spawnPoint = spawnThree.position;
		} else if(randomNum == 4){
			print("Spawn Four");
			spawnPoint = spawnFour.position;
		} else if(randomNum == 5){
			print("Spawn Five");
			spawnPoint = spawnFive.position;
		} else{
			print("ERROR!!!");
		}
		//disable starting cam
		disableCam.enabled = false;
		//remove GUI text
		hideText.enabled = false;

		GameObject myCar = (GameObject) PhotonNetwork.Instantiate("CHEVROLET_CAMARO_BB", spawnPoint, Quaternion.identity, 0);
		//GameObject myCam = (GameObject) Camera.Instantiate("CarCamera", new Vector3(0,15,0), Quaternion.identity, 0);
		GameObject myCam = (GameObject) Instantiate(cam, new Vector3(0, 10, 0), Quaternion.identity);
		//wheelFL = GameObject.Find ("FLWheel").transform;

		//YourGameObject.GetComponent<YourScript>().enabled = false;
		myCar.GetComponent<CarController> ().enabled = true;//for getting .cs files
		((MonoBehaviour)myCam.GetComponent ("CarCameraController")).enabled = true;//for getting .js files
	}
}
