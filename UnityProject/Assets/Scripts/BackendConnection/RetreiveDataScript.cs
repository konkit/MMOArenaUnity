using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RetreiveDataScript : MonoBehaviour {

	public string serverAddress;
	public int playerID;
		
	// Data from backend
	public string characterName;
	public int characterLevel;
	public int characterExp;
	
	public Rect retreiveDataButtonRect;
	
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
	}
	
	IEnumerator Retreive() {		
		WWW www = new WWW(serverAddress + "?playerID=" + playerID.ToString() );
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

	}
}
