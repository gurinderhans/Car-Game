using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string reg_game_name = "CarGame_gurinderhans";
	float refresReqLength = 3.0f;
	HostData[] hostData;
	bool whichColorYouLike;
	string whichCarChoosen;


	private void  Awake(){
		MasterServer.ipAddress = "10.127.127.1";
		MasterServer.port = 23466;
	}

	private void SpawnPlayer(string whichCar){
		Debug.Log ("Spawning Player");
		GameObject myCar = (GameObject) Network.Instantiate (Resources.Load(whichCar), new Vector3 (0f, 10f, 0f), Quaternion.identity, 0);
		GameObject myCam = (GameObject) Instantiate(Resources.Load("CarCamera"), new Vector3(0f, 10f, 0f), Quaternion.identity);
		((MonoBehaviour)myCam.GetComponent ("CarCameraController")).enabled = true;//for getting .js files
		myCar.GetComponent<CarController> ().enabled = true;//for getting .cs files
		

	}

	public IEnumerator RefreshHostList(){
		Debug.Log ("RefreshHostList");
		MasterServer.RequestHostList (reg_game_name);//request host list from master server
		//float timeStarted = Time.time;
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
		SpawnPlayer (whichCarChoosen);
	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent){
		if(masterServerEvent == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Registration Succeeded");
		}
	}

	void OnConnectedToServer(){
		SpawnPlayer (whichCarChoosen);
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




	//GUI STUFF

	public void OnGUI(){
		if(!whichColorYouLike){
			GUI.Box(new Rect (50f, 100f, 300f, 30f), "Which color do you like");
			if(GUI.Button( new Rect(25f, 150f, 50f, 30f) , "Red")){
				whichCarChoosen="CHEVROLET_RED";
				whichColorYouLike=true;
			}
			if(GUI.Button(new Rect(75f, 150f, 50f, 30f) , "Yellow")){
				whichCarChoosen="CHEVROLET_YELLOW";
				whichColorYouLike=true;
			}
		}
		if(Network.isServer){
			GUILayout.Label("Running as a server.");
		} else if(Network.isClient){
			GUILayout.Label("Running as a client.");
		}

		/*if(Network.isClient){
			if(GUI.Button(new Rect(25f, 25f, 150f, 30f), "Spawn")){
				SpawnPlayer();
				GameObject adke = (GameObject) Instantiate(Resources.Load("CarCamera"), new Vector3(0f, 10f, 0f), Quaternion.identity);
				((MonoBehaviour)adke.GetComponent ("CarCameraController")).enabled = true;//for getting .js files
			}
		}*/


		if(Network.isClient || Network.isServer){
			return;
		}
		if(whichColorYouLike){
			if(GUI.Button(new Rect(25f, 25f, 150f, 30f), "Create Server")){
				// Start server function here
				StartServer();
			}

			if(GUI.Button(new Rect(25f, 65f, 150f, 30f), "Find Game")){
				// Refresh server list funciton here
				StartCoroutine("RefreshHostList");
			}

			if(hostData != null){
				for(int i = 0; i < hostData.Length; i++){
					if(GUI.Button(new Rect(Screen.width/2, 65f+(30f*i), 300f, 30f), hostData[i].gameName)){
						Network.Connect(hostData[i]);
					}
				}
			}
		}
	}
}