using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for the list

public class LeaderBoard : MonoBehaviour {
	
	bool showLeaderBoard;

	bool playerConnected;

	public GameObject ourPlayer;

	List<PlayerScore> allPlayersOnServer = new List<PlayerScore>();

	//List<PlayerScore> allClientsList = new List<PlayerScore>();

	void Update(){
		if(Input.GetKeyDown(KeyCode.Tab)) showLeaderBoard = !showLeaderBoard;

		//print ("allPlayersOnServer contains " + allPlayersOnServer.Count + " elements");

		if(playerConnected){
			ourPlayer = GameObject.FindGameObjectWithTag ("Player");
			int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet>().myPoints;

			if(ourPlayer.GetComponentInChildren<ShootBullet>().pointsUpdated){
				//allPlayersOnServer.Clear();

				networkView.RPC ("AddPlayerScore", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});


				//this is local for adding the server scores
				/*allPlayersOnServer.RemoveAll(myItem => myItem.playerName == this.GetComponent<NetworkManager> ().myName);
				print ("getting player name: " + this.GetComponent<NetworkManager> ().myName);
				allPlayersOnServer.Add(new PlayerScore (this.GetComponent<NetworkManager> ().myName, playerPoints));*/



				//transfer all data in allPlayersOnServer to allClientsList
				/*foreach(PlayerScore playerData in allPlayersOnServer){
					allClientsList.Add( new PlayerScore(playerData.playerName, playerData.points));
				}*/

			}
		}

	}
	Vector2 scrollPosition;
	void windowFunc(int id){

		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width/2.1f), GUILayout.Height(220));

		foreach(PlayerScore player in allPlayersOnServer){
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

		//allPlayersOnServer.Add(new PlayerScore (this.GetComponent<NetworkManager> ().myName, playerPoints));

		networkView.RPC ("AddPlayerScore", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});

		playerConnected = true;
	}

	void OnConnectedToServer(){//add client to leaderboard
		ourPlayer = GameObject.FindGameObjectWithTag ("Player");
		int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet> ().myPoints;

		networkView.RPC ("AddPlayerScore", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});

		playerConnected = true;
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		// remove the entry from leaderboard
		ourPlayer = GameObject.FindGameObjectWithTag ("Player");
		int playerPoints = (int) ourPlayer.GetComponentInChildren<ShootBullet> ().myPoints;

		//this doesnt work right now will look at it later
		//networkView.RPC ("RemovePlayerScore", RPCMode.AllBuffered, new object[]{this.GetComponent<NetworkManager> ().myName, playerPoints});
	}

	//removes the player score from scoreboard/leaderboard
	[RPC]
	void RemovePlayerScore(string name, int point){
		allPlayersOnServer.RemoveAll(myItem => myItem.playerName == name);
		print ("getting player name: " + name);
	}


	//adds the player score to scoreboard/leaderboard
	[RPC]
	void AddPlayerScore(string name, int point){
		//print ("Name "+name);
		//print ("Points "+point);

		//string playerString = name + " \t" + point + " points";

		/*int index = allPlayersOnServer.FindIndex(myItem => myItem.playerName == this.GetComponent<NetworkManager> ().myName);
		print (name + " player's index is " + index);

		allPlayersOnServer.RemoveAt (index);*/

		allPlayersOnServer.RemoveAll(myItem => myItem.playerName == name);
		print ("getting player name: " + name);

		allPlayersOnServer.Add(new PlayerScore (name, point));

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
	
	//the constructor
	public PlayerScore(string name, int point){
		playerName = name;
		points = point;
	}
	
}