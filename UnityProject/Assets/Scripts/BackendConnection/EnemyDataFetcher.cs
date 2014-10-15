using UnityEngine;
using System.Collections;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class EnemyDataFetcher : AbstractHttpFetcher
{
    public string serverAddress = "http://localhost:8080/GrailsMMOArena";
    public string controllerName = "/fight";
    public string actionName = "/getPlayerData";

    public Character enemyData = null;
    public int enemyId = 0;

    public RoomNameFetcher roomNameFetcher;

    public bool isSent = false;

    public bool dataReceived = false;

    // Use this for initialization
    void Start()
    {
        roomNameFetcher = GetComponent<RoomNameFetcher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSent == false && roomNameFetcher.dataReceived == true)
        {
            isSent = true;

            receiveCallback += UpdateEnemyData;

            this.absoluteAddress = serverAddress + controllerName + actionName + "/" + enemyId ;
            this.Fetch();
        }
    }

    void UpdateEnemyData(string data)
    {
        var serializer = new XmlSerializer(typeof(Character));
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
        enemyData = serializer.Deserialize(stream) as Character;

        enemyId = enemyData.Id;

        Debug.Log(enemyData);

        stream.Close();

        dataReceived = true;
    }
}
