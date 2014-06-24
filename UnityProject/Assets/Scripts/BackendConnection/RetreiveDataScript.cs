using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RetreiveDataScript : MonoBehaviour {

	string serverAddress = "http://localhost:8080/GrailsMMOArenaBackend/fight/";
	public int playerID;
		
	// Data from backend
	public string characterName;
	public int characterLevel;
	public int characterExp;

	public string items;
	
	public Rect retreiveDataButtonRect;
	public Rect storeDataButtonRect;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if( GUI.Button( retreiveDataButtonRect, "Retreive data from server") ) {
			StartCoroutine( Retreive() );			
		}

		if( GUI.Button( storeDataButtonRect, "Send data back to server") ) {
			StartCoroutine( SendFightResponse() );			
		}
	}
	
	IEnumerator Retreive() {	
		string webaddress = serverAddress + "requestPlayerData?playerID=" + playerID.ToString();

		Debug.Log("web address = " + webaddress);

		WWW www = new WWW(webaddress );
		yield return www;
		
		if( www.error != null) {
			Debug.LogError(www.error);
		}
		
		string responseData = www.text;		
		Debug.Log(responseData);

		var N = JSON.Parse(responseData);
		characterName = N["character"]["name"];
		characterLevel = int.Parse (N["character"]["level"]);
		characterExp = int.Parse( N["character"]["exp"] );
		items = N["items"][0]["name"];
	}


	IEnumerator SendFightResponse() {
		int gainedExp = 1;

		string webaddress = serverAddress + "storeResults?playerID=" + playerID.ToString() + "&gainedExp=" + gainedExp;
		
		Debug.Log("web address = " + webaddress);

		WWW www = new WWW(webaddress );
		yield return www;
		
		if( www.error != null) {
			Debug.LogError(www.error);
		} else {
			Debug.Log("Response: " + www.text);
		}
	}

}
