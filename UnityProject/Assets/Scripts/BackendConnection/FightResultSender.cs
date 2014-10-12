using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class FightResultSender : AbstractHttpFormSender {

    public string serverAddress = "http://localhost:8080/GrailsMMOArena";
    public string controllerName = "/fight";
    public string actionName = "/storeResults";

    FightAwards fightAwards = null;

    public CharacterStats player = null;
    public CharacterStats enemy = null;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            return;
        }


	}

    void Send()
    {
        form = new WWWForm();

        form.AddField("playerHealthRemained", player.GetComponent<CharacterStats>().health);
        //form.AddField("enemyHealthRemained", enemy.GetComponent<CharacterStats>().health);

        StartCoroutine(SendCoroutine());
    }

    void OnDataReceived(string data)
    {
        var serializer = new XmlSerializer(typeof(FightAwards));
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
        fightAwards = serializer.Deserialize(stream) as FightAwards;
        stream.Close();

        //gameController.ResultSendSuccessful();
    }
}

