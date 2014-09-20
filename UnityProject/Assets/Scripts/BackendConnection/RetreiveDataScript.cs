using UnityEngine;
using System.Collections;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class RetreiveDataScript : MonoBehaviour {
    // other scripts references
    GameController gameController;

	// server connection data
	public string serverAddress = "http://localhost:8080/GrailsMMOArena";
	string controllerName = "/fight";
	string actionName = "/requestFightData";
	string storeActionName = "/storeResults";

    // other objects references
	public GameObject player;
	public GameObject enemy;
	public GameObject[] prefabs;
		
	// Data received at the beginning of the fight
	public FightData fightData;

    // Data received after the fight
    public FightAwards fightAwards;
	
	// Use this for initialization
	void Awake () {
        gameController = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RetreiveFightData() {
        StartCoroutine(Retreive());
    }
	
	IEnumerator Retreive() {
        string webaddress = serverAddress + controllerName + actionName;

        Debug.Log("Request sent to : " + webaddress);

        WWW www = new WWW(webaddress );
        yield return www;
		
        
        if( www.error != null) {
            gameController.BackendConnectFailed(www.error);
            throw new UnityException("Retreive FightData error : " + www.error);
        }
		
        string responseData = www.text;

        Debug.Log("Response data : " + responseData);

        var serializer = new XmlSerializer(typeof(FightData));
        var stream = new MemoryStream( Encoding.ASCII.GetBytes(responseData));
        fightData = serializer.Deserialize(stream) as FightData;
        stream.Close();

        player.GetComponent<CharacterStats>().LoadFromData(fightData.Player);
        player.GetComponent<CharacterInventory>().LoadInventory(fightData.Player);
        player.GetComponent<CharacterSpellcasting>().LoadSpells(fightData.Player);

        enemy.GetComponent<CharacterStats>().LoadFromData(fightData.Enemy);
        enemy.GetComponent<CharacterInventory>().LoadInventory(fightData.Enemy);
        enemy.GetComponent<CharacterSpellcasting>().LoadSpells(fightData.Enemy);

        Debug.Log("Response fight data : " + fightData.FightId);

        gameController.BackendConnectSuccess();
	}

	public void StoreFightResults() {
		StartCoroutine(StoreFightResultsCoroutine() );
	}

	IEnumerator StoreFightResultsCoroutine() {
		WWWForm form = new WWWForm();
		
		form.AddField( "playerHealthRemained", player.GetComponent<CharacterStats>().health );
		form.AddField( "enemyHealthRemained", enemy.GetComponent<CharacterStats>().health );

        Debug.Log("Store result address = " + serverAddress + controllerName + storeActionName);
		WWW resultDataWWW = new WWW( serverAddress + controllerName + storeActionName, form );
		
		// Wait until the download is done
		yield return resultDataWWW;
		
		if(resultDataWWW.error != null) {
            gameController.ResultSendFailed(resultDataWWW.error);
		}

        var serializer = new XmlSerializer(typeof(FightAwards));
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(resultDataWWW.text));
        fightAwards = serializer.Deserialize(stream) as FightAwards;
        stream.Close();

        gameController.ResultSendSuccessful();
	}

}
