using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{

	public string playerName;
	public int points;

	//get set stuff
	//totally need this otherwise it leaderboard dosent work
	public int Points{
		get{
			return points;
		}
		set{
			points = value;
		}
	}
	
}