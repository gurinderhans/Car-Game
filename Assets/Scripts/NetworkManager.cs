using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private string reg_game_name = "CarGame_gurinderhans";
	public float refresReqLength = 3.0f;
	public HostData[] hostData;
	private string car_choosen;
	private string color_choosen;

	/********************/
	public Transform spawnOne;
	public Transform spawnTwo;
	public Transform spawnThree;
	public Transform spawnFour;
	public Transform spawnFive;
	/*******************/
	
	/*private void  Awake(){
		MasterServer.ipAddress = "10.82.32.35";
		MasterServer.port = 23466;
	}*/

	private Vector3 spawnPosition;
	private Vector3 CalcSpawnPos(){
		int randomNum = Random.Range (0, 5);

		if(randomNum == 0){
			spawnPosition = spawnOne.position;
		} else if(randomNum == 1){
			spawnPosition = spawnTwo.position;
		} else if(randomNum == 2){
			spawnPosition = spawnThree.position;
		} else if(randomNum == 3){
			spawnPosition  = spawnFour.position;
		} else if(randomNum == 4){
			spawnPosition = spawnFive.position;
		} else{
			spawnPosition = Vector3.zero;
		}

		return spawnPosition;
	}

	private void SpawnPlayer(){

		Vector3 spawnPos = CalcSpawnPos ();
		Debug.Log ("Spawning Player");
		GameObject myCar = (GameObject) Network.Instantiate (Resources.Load(car_choosen+color_choosen), spawnPos, Quaternion.identity, 0);
		
		GameObject myCam = (GameObject) Instantiate(Resources.Load("DriveCam"), new Vector3(0f, 10f, 0f), Quaternion.identity);
		((MonoBehaviour)myCam.GetComponent ("CarCameraController")).enabled = true;//for getting .js files
		myCar.GetComponent<CarController> ().enabled = true;//for getting .cs files
		myCar.GetComponent<Health> ().enabled = true;
		//enable the gun parts of car
		myCar.GetComponentInChildren<GunMovement> ().enabled = true;
		myCar.GetComponentInChildren<CrossHair> ().enabled = true;
		myCar.GetComponentInChildren<ShootBullet> ().enabled = true;

		//keep this in case unity gives problem and gives you ability to control other players guns
		/*GameObject myGun = (GameObject) myCar.transform.FindChild ("gun TBS 001C").gameObject;
		myGun.GetComponent<GunMovement> ().enabled = true;
		myGun.GetComponent<CrossHair> ().enabled = true;
		myGun.GetComponent<ShootBullet> ().enabled = true;*/
		
		//hide the player name locally so it wont interrupt gameplay but show on others screen
		myCar.GetComponentInChildren<GUIText> ().enabled = false;

		//MSK stuff
		Instantiate (Resources.Load ("GunCam"), new Vector3 (0f, 10f, 0f), Quaternion.identity);

	}
	
	public IEnumerator RefreshHostList(){
		Debug.Log ("RefreshHostList");
		MasterServer.RequestHostList (reg_game_name);//request host list from master server
		float timeEnd = Time.time + refresReqLength;
		
		while(Time.time < timeEnd){
			hostData = MasterServer.PollHostList();
			yield return new WaitForEndOfFrame();
		}
		
		if(hostData == null || hostData.Length == 0){
			Debug.Log ("No active servers have been found");
		}
		
	}

	/**************************************************************/
	//Call Backs from the Client and Server
	/**************************************************************/

	private void StartServer(){
		Network.InitializeServer (32, 25000, false);
		MasterServer.RegisterHost (reg_game_name, "Unity3D Game", "Enjoy!");
	}

	void OnServerInitialized(){
		Debug.Log ("OnServerInitialized");
		SpawnPlayer ();
	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent){
		if(masterServerEvent == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Registration Succeeded");
		}
	}

	void OnConnectedToServer(){
		SpawnPlayer ();
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info){
		Application.LoadLevel (0);
	}

	void OnFailedToConnect(NetworkConnectionError error){
		
	}

	void OnPlayerConnected(NetworkPlayer player){
		Debug.Log ("Player connected from:" + player.ipAddress + ":" + player.port);
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("Player disconnected from:" + player.ipAddress + ":" + player.port);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info){
		
	}

	void OnNetworkInstantiate(NetworkMessageInfo info){
	}

	void OnApplicationQuit(){
		if(Network.isServer){
			Network.Disconnect(200);
			MasterServer.UnregisterHost();
		}

		if(Network.isClient){
			Network.Disconnect(200);
		}
	}

	void ChooseCar(){
		//GUI BOX (l,t,w,h)
		GUI.Box(new Rect (25f, 25f, 250f, 205f), "CHOOSE YOUR CAR:");
		
		//Create buttons for cars
		if(GUI.Button( new Rect(50f, 50f, 200f, 30f) , "CHEVROLET CAMARO")){
			car_choosen = "CHEVROLET_CAMARO/";
			isCarChosen = true;
		} /*else if(GUI.Button( new Rect(50f, 85f, 200f, 30f) , "RANDOM CAR 2")){
			//print ("camaro");
		} else if(GUI.Button( new Rect(50f, 120f, 200f, 30f) , "RANDOM CAR 3")){
			//print("camaro");
		} else if(GUI.Button( new Rect(50f, 155f, 200f, 30f) , "RANDOM CAR 4")){
			//print("camaro");
		} else if(GUI.Button( new Rect(50f, 190f, 200f, 30f) , "RANDOM CAR 5")){
			//print("camaro");
		}*/
	}

	void ChooseColor(){
		//GUI BOX (l,t,w,h)
		GUI.Box(new Rect (25f, 25f, 250f, 205f), "WHICH COLOR DO YOU LIKE:");

		if(GUI.Button( new Rect(50f, 50f, 200f, 30f) , "WHITE")){
			color_choosen = "CHEVROLET_CAMARO_WHITE";
			isColorChosen = true;
		} /*else if(GUI.Button( new Rect(50f, 85f, 200f, 30f) , "YELLOW")){
			//color_choosen = "CHEVROLET_CAMARO_YELLOW";
			//isColorChosen = true;
		}*/ /*else if(GUI.Button( new Rect(50f, 120f, 200f, 30f) , "RED")){
			color_choosen = "CHEVROLET_CAMARO_RED";
			isColorChosen = true;
		} else if(GUI.Button( new Rect(50f, 155f, 200f, 30f) , "BLUE")){
			color_choosen = "CHEVROLET_CAMARO_BLUE";
			isColorChosen = true;
		}*/// else if(GUI.Button( new Rect(50f, 85f/*CHANGE*/, 200f, 30f) , "MORE COMING SOON")){}
		/*else if(GUI.Button( new Rect(50f, 155f, 200f, 30f) , "GREY")){
			car_choosen = "CHEVROLET_CAMARO_GREY";
			isColorChosen = true;
		} else if(GUI.Button( new Rect(50f, 190f, 200f, 30f) , "YELLOW")){
			car_choosen = "CHEVROLET_CAMARO_YELLOW";
			isColorChosen = true;
		}*/
	}

	private string playerCustomName;
	void ServerMenu(){
		//(l,t,w,h)
		if(GUI.Button(new Rect(25f, 15f, 150f, 30f), "Create Server")){
			// Start server function here
			StartServer();
			isServerStarted = true;
		}

		if(GUI.Button(new Rect(25f, 55f, 150f, 30f), "Find Game")){
			// Refresh server list funciton here
			StartCoroutine("RefreshHostList");
		}

		if(hostData != null){
			for(int i = 0; i < hostData.Length; i++){
				if(GUI.Button(new Rect(Screen.width/2, 65f+(30f*i), 300f, 30f), hostData[i].gameName)){
					Network.Connect(hostData[i]);
					isServerStarted = true;
				}
			}
		}
		
	}


	//GUI STUFF
	private bool isCarChosen;
	private bool isColorChosen;
	private bool isServerStarted;

	public void OnGUI(){

		if(Network.isServer){
			GUILayout.Label("Running as a server.");
		} else if(Network.isClient){
			GUILayout.Label("Running as a client.");
		}

		if(!isCarChosen){
			ChooseCar();
		} else {
			if(!isColorChosen){
				ChooseColor();
			} else {
				if(!isServerStarted){
					ServerMenu();
				} else{
					//do nothing here
				}
			}
		}
	}
}