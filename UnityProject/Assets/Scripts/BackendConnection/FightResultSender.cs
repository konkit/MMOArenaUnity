using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class FightResultSender : AbstractHttpFormSender {

    public string controllerName = "/fight";
    public string actionName = "/storeResults";

    public FightAwards fightAwards = null;

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
            absoluteAddress = gameController.userBackendAddress + controllerName + actionName;
            Send();
        }
	}

    void Send()
    {
        form = new WWWForm();

        int enemyHealth;
        if (gameController.enemy != null)
        {
            enemyHealth = gameController.enemy.GetComponent<CharacterStats>().health;
        } else {
            enemyHealth = 0;
        }

        form.AddField("playerHealthRemained", gameController.player.GetComponent<CharacterStats>().health);
        form.AddField("enemyHealthRemained", enemyHealth);

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
    }
}

