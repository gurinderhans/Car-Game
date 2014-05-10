using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string reg_game_name = "CarGame_gurinderhans";
	float refresReqLength = 3.0f;
	HostData[] hostData;
	string car_choosen;
	string color_choosen;

	//GUI.Box(new Rect (50f, 100f, 300f, 30f), "Which color do you like");

	/*private void  Awake(){
		MasterServer.ipAddress = "10.127.127.1";
		MasterServer.port = 23466;
	}*/

	private void SpawnPlayer(){
		Debug.Log ("Spawning Player");
		GameObject myCar = (GameObject) Network.Instantiate (Resources.Load(car_choosen+color_choosen), new Vector3 (0f, 10f, 0f), Quaternion.identity, 0);
		GameObject myCam = (GameObject) Instantiate(Resources.Load("CarCamera"), new Vector3(0f, 10f, 0f), Quaternion.identity);
		((MonoBehaviour)myCam.GetComponent ("CarCameraController")).enabled = true;//for getting .js files
		myCar.GetComponent<CarController> ().enabled = true;//for getting .cs files

		myCar.GetComponent<CarController> ().playerNameText.renderer.enabled = false;//hide the player name locally so it wont
																				//interrupt gameplay but show on others screen
		
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
		} else if(GUI.Button( new Rect(50f, 85f, 200f, 30f) , "DODGE CHALLENGER")){
			print ("camaro");
		} else if(GUI.Button( new Rect(50f, 120f, 200f, 30f) , "RANDOM CAR 3")){
			print("camaro");
		} else if(GUI.Button( new Rect(50f, 155f, 200f, 30f) , "RANDOM CAR 4")){
			print("camaro");
		} else if(GUI.Button( new Rect(50f, 190f, 200f, 30f) , "RANDOM CAR 5")){
			print("camaro");
		}
	}

	void ChooseColor(){
		//GUI BOX (l,t,w,h)
		GUI.Box(new Rect (25f, 25f, 250f, 205f), "CHOOSE YOUR CAR:");

		if(GUI.Button( new Rect(50f, 50f, 200f, 30f) , "RED")){
			color_choosen = "CHEVROLET_CAMARO_RED";
			isColorChosen = true;
		} else if(GUI.Button( new Rect(50f, 85f, 200f, 30f) , "BLUE")){
			color_choosen = "CHEVROLET_CAMARO_BLUE";
			isColorChosen = true;
		} else if(GUI.Button( new Rect(50f, 120f, 200f, 30f) , "WHITE")){
			color_choosen = "CHEVROLET_CAMARO_WHITE";
			isColorChosen = true;
		} else if(GUI.Button( new Rect(50f, 155f, 200f, 30f) , "MORE COMING SOON")){//really need better colors, now only white is acceptable :D

		} /*else if(GUI.Button( new Rect(50f, 155f, 200f, 30f) , "GREY")){
			car_choosen = "CHEVROLET_CAMARO_GREY";
			isColorChosen = true;
		} else if(GUI.Button( new Rect(50f, 190f, 200f, 30f) , "YELLOW")){
			car_choosen = "CHEVROLET_CAMARO_YELLOW";
			isColorChosen = true;
		}*/
	}

	void ServerMenu(){
		if(GUI.Button(new Rect(25f, 25f, 150f, 30f), "Create Server")){
			// Start server function here
			StartServer();
			isServerStarted = true;
		}
		
		if(GUI.Button(new Rect(25f, 65f, 150f, 30f), "Find Game")){
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