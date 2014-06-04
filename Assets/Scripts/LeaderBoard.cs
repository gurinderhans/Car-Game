using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for the list

public class LeaderBoard : MonoBehaviour {

	//create the list to store all player info
	List<PlayerScore> allPlayers = new List<PlayerScore>();
	bool showLeaderBoard;

	bool playerConnected;

	public GameObject ourPlayer;

	void Update(){
		if(Input.GetKeyDown(KeyCode.L)) showLeaderBoard = !showLeaderBoard;


		print (allPlayers.Count);

		if(playerConnected){
			ourPlayer = GameObject.FindGameObjectWithTag ("Player");
			int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet>().myPoints;

			if(ourPlayer.GetComponentInChildren<ShootBullet>().pointsUpdated){


				allPlayers.Clear();
				networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});




				//int index = System.Array.IndexOf(allPlayers, this.GetComponent<NetworkManager>().myName);
				//print (index);
				//allPlayers.RemoveAt(0);
				//print (playerPoints);
				//int index = allPlayers.FindIndex (x => x == playerPoints);
				//networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});
			}
			//networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints,0,0});
			//call this when pointsUpdated from shootbullet.cs has been set to true
		}

	}
	Vector2 scrollPosition;
	void windowFunc(int id){

		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width/2.1f), GUILayout.Height(220));

		foreach(PlayerScore player in allPlayers){
			//display the messages
			GUILayout.BeginHorizontal(GUILayout.Width(250));
				GUILayout.Label(player.playerName);
				GUILayout.Label(player.points.ToString());
			GUILayout.EndHorizontal();
		}
		
		GUILayout.EndScrollView ();
		scrollPosition.y = 100000000000f;
		
	}

	void OnServerInitialized(){//add the server to leaderboard
		ourPlayer = GameObject.FindGameObjectWithTag ("Player");
		int playerPoints = (int)ourPlayer.GetComponentInChildren<ShootBullet> ().myPoints;

		networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});
		playerConnected = true;
	}

	void OnConnectedToServer(){//add clinet to leaderboard
		ourPlayer = GameObject.FindGameObjectWithTag ("Player");
		int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet> ().myPoints;

		networkView.RPC ("AddPlayer", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});
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

		//string playerString = name + " \t" + point + " points";

		allPlayers.Add (new PlayerScore (name, point));

		//leaderboardStuff += playerString;




		/*allPlayers.Add (playerString);

		int index = allPlayers.FindIndex (x => x == playerString);
		allPlayers.RemoveAt (index);
		allPlayers.Add (playerString);*/
	}

	void OnGUI () {

		if(showLeaderBoard){
			Rect windowRect = new Rect(0,0, Screen.width/2, Screen.height/1.5f);
			windowRect = GUI.Window(3, windowRect, windowFunc, "Leader Board");
		}
	}
}


public class PlayerScore{
	
	public string playerName;
	public int points;
	
	//get set stuff
	public PlayerScore(string name, int point){
		playerName = name;
		points = point;
	}
	
}