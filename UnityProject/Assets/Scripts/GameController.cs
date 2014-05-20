using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;

	// Use this for initialization
	void Start () {
		// currently is redundant, but shows a way of thinking.
		enemy.GetComponent<HealthOfPlayer>().deathDelegate += sendBackWinScore;
		player.GetComponent<HealthOfPlayer>().deathDelegate += sendBackLoseScore;
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sendBackWinScore() {
		Debug.Log("This happens when enemy dies");

		//send data to backend
	}

	public void sendBackLoseScore() {
		Debug.Log("This happens when player dies");
        
		//send data to backend
	}
}
