using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for the list

public class LeaderBoard : MonoBehaviour {

	//create the list to store all player info
	List<string> allPlayers = new List<string>();
	bool showLeaderBoard;

	bool playerConnected;

	public GameObject ourPlayer;

	public string leaderboardStuff  = "test String";

	void Update(){
		if(Input.GetKeyDown(KeyCode.L)) showLeaderBoard = !showLeaderBoard;

		if(playerConnected){
			ourPlayer = GameObject.FindGameObjectWithTag ("Player");
			int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet>().myPoints;


			if(ourPlayer.GetComponentInChildren<ShootBullet>().pointsUpdated){
				//allPlayers.RemoveAt(0);
				//print (playerPoints);
				networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});
			}
			//networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints,0,0});
			//call this when pointsUpdated from shootbullet.cs has been set to true
		}

	}
	Vector2 scrollPosition;
	void windowFunc(int id){

		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width/2.05f), GUILayout.Height(220));

		//foreach(string player in allPlayers){
			//display the messages
			GUILayout.BeginHorizontal(GUILayout.Width(250));
				GUILayout.Label(leaderboardStuff);
			GUILayout.EndHorizontal();
		//}
		
		GUILayout.EndScrollView ();
		
	}

	void OnServerInitialized(){//add the server to leaderboard
		ourPlayer = GameObject.FindGameObjectWithTag ("Player");
		int playerPoints = (int)ourPlayer.GetComponentInChildren<ShootBullet> ().myPoints;
		//networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints,0,0});
		playerConnected = true;
	}

	void OnConnectedToServer(){//add clinet to leaderboard
		ourPlayer = GameObject.FindGameObjectWithTag ("Player");
		int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet> ().myPoints;
		//networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints,0,0});
		playerConnected = true;
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		//allPlayers.re // remove the entry from leaderboard
		print ("player disconnected");
	}

	[RPC]
	void AddPlayer(string name, int point){
		print ("Name "+name);
		print ("Points "+point);

		string playerString = name + " \t" + point + " points";

		leaderboardStuff += playerString;




		/*allPlayers.Add (playerString);

		int index = allPlayers.FindIndex (x => x == playerString);
		allPlayers.RemoveAt (index);
		allPlayers.Add (playerString);*/
	}

	void OnGUI () {
		/*foreach(Player name in allPlayers){
			//display the messages
			GUILayout.Label(name.playerName);
		}*/

		if(showLeaderBoard){
			Rect windowRect = new Rect(0,0, Screen.width/2, Screen.height/1.5f);
			windowRect = GUI.Window(3, windowRect, windowFunc, "Leader Board");
		}
	}
}
