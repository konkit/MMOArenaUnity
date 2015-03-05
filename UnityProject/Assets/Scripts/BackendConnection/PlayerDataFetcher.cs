using UnityEngine;
using System.Collections;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class PlayerDataFetcher : AbstractHttpFetcher
{
    public string controllerName = "/fight";
    public string actionName = "/getPlayerData";

    public Character playerData = null;
    public int playerId = 0;

    GameController gameController = null;

    // Use this for initialization
    void Start()
    {
        gameController = GetComponent<GameController>();
        gameController.loadingMsg = "Fetching player data";

        receiveCallback += UpdatePlayerData;

        this.absoluteAddress = gameController.userBackendAddress + controllerName + actionName;
        this.Fetch();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdatePlayerData(string data)
    {
        var serializer = new XmlSerializer(typeof(Character));
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
        playerData = serializer.Deserialize(stream) as Character;

        Debug.Log(playerData);

        playerId = playerData.Id;

        stream.Close();
    }
}
