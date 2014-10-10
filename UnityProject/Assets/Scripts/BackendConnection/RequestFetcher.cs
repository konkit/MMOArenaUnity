using UnityEngine;
using System.Collections;

public class RequestFetcher : AbstractHttpFetcher {

    public string serverAddress = "http://localhost:5000";
    public string actionName = "/requestFight";

    //state management
    PlayerDataFetcher playerDataFetcher = null;
    bool requestSent = false;

    public string requestId = "";   // output

	// Use this for initialization
	void Start () {
        playerDataFetcher = GetComponent<PlayerDataFetcher>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerDataFetcher != null && !requestSent && playerDataFetcher.playerId != 0)
        {
            requestSent = true;

            receiveCallback += UpdateRequestId;
            this.absoluteAddress = serverAddress + actionName + "/" + playerDataFetcher.playerId.ToString();
            this.Fetch();
        }
	}

    void UpdateRequestId(string data)
    {
        requestId = SimpleJSON.JSON.Parse(data)["requestId"];
    }
}
