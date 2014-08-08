using UnityEngine;
using System.Collections;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class RetreiveDataScript : MonoBehaviour {
	// server connection data
	string serverAddress = "http://localhost:8080/GrailsMMOArenaBackend";
	string controllerName = "/fight";
	string actionName = "/requestFightData";

	public GameObject player;
	public GameObject enemy;

	public GameObject[] prefabs;
		
	// Data from backend
	public FightData fightData;

	// Debug rectangles
	public Rect retreiveDataButtonRect;
	public Rect storeDataButtonRect;
	
	// Use this for initialization
	void Start () {
		StartCoroutine( Retreive() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Retreive() {	
		string webaddress = serverAddress + controllerName + actionName;

		WWW www = new WWW(webaddress );
		yield return www;
		
		if( www.error != null) {
			Debug.LogError(www.error);
		}
		
		string responseData = www.text;		

		var serializer = new XmlSerializer(typeof(FightData));
		var stream = new MemoryStream( Encoding.ASCII.GetBytes(responseData));
		fightData = serializer.Deserialize(stream) as FightData;
		stream.Close();

		player.GetComponent<CharacterInventory>().LoadInventory(fightData.Player);
		player.GetComponent<CharacterSpellcasting>().LoadSpells(fightData.Player);

		enemy.GetComponent<CharacterInventory>().LoadInventory(fightData.Enemy);
		enemy.GetComponent<CharacterSpellcasting>().LoadSpells(fightData.Enemy);

		Debug.Log ("fightData item 1 name : " + fightData.Player.Items[0].item.name );
	}


	IEnumerator SendFightResponse() {
		int gainedExp = 1;

		string webaddress = serverAddress;
		
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
