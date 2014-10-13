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

    public bool isResultsSent = false;

    public GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameController.isGameFinished && isResultsSent == false)
        {
            isResultsSent = true;
            absoluteAddress = serverAddress + controllerName + actionName;
            Send();
        }
	}

    void Send()
    {
        form = new WWWForm();

        form.AddField("playerHealthRemained", gameController.player.GetComponent<CharacterStats>().health);
        form.AddField("enemyHealthRemained", gameController.enemy.GetComponent<CharacterStats>().health);

        receiveCallback += OnDataReceived;

        StartCoroutine(SendCoroutine());
    }

    void OnDataReceived(string data)
    {
        var serializer = new XmlSerializer(typeof(FightAwards));
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
        fightAwards = serializer.Deserialize(stream) as FightAwards;
        stream.Close();

        Debug.Log("Received results : " + fightAwards.hasWon);
        //gameController.ResultSendSuccessful();
    }
}

