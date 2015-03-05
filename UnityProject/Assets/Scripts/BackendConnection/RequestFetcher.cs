using UnityEngine;
using System.Collections;

public class RequestFetcher : AbstractHttpFetcher {

    public string actionName = "/requestFight";

    //state management
    PlayerDataFetcher playerDataFetcher = null;
    GameController gameController = null;

    bool requestSent = false;

    public string requestId = "";   // output

	// Use this for initialization
	void Start () {
        playerDataFetcher = GetComponent<PlayerDataFetcher>();
        gameController = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerDataFetcher != null && !requestSent && playerDataFetcher.playerId != 0)
        {
            requestSent = true;
            gameController.loadingMsg = "Requesting a new fight";

            receiveCallback += UpdateRequestId;
            this.absoluteAddress = gameController.matchmakerAddress + actionName + "/" + playerDataFetcher.playerId.ToString();
            this.Fetch();
        }
	}

    void UpdateRequestId(string data)
    {
        requestId = SimpleJSON.JSON.Parse(data)["requestId"];
    }
}
