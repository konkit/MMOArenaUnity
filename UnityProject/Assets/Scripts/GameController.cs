using UnityEngine;
using System.Collections;

public enum GameState
{
    STARTING, 
    CONNECTING_VIA_TCP,
    START_FAILED, 
    ONGOING, 
    SENDING_RESULT, 
    SENDING_RESULT_FAILED, 
    FINISHED
}

public class GameController : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;
    
    public bool victory;
    public bool defeat;

    public bool isPaused = false;

	public RetreiveDataScript retreiveDataScript;
    public NodeJsSocketConnector nodeJsSocketConnector;

    public GameState gameState;

    public string errorMsg;

    void Awake()
    {
        nodeJsSocketConnector = GetComponent<NodeJsSocketConnector>();

        gameState = GameState.STARTING;
        victory = false;
        defeat = false;
    }

	void Start () {
		enemy.GetComponent<CharacterStats>().deathDelegate += sendBackWinScore;
		player.GetComponent<CharacterStats>().deathDelegate += sendBackLoseScore;

        retreiveDataScript.RetreiveFightData();

        Pause();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
	}

    public void Pause()
    {
        MouseController controller = GameObject.FindObjectOfType<MouseController>() as MouseController;
        controller.Disable();

        isPaused = true;
    }

    public void UnPause()
    {
        MouseController controller = GameObject.FindObjectOfType<MouseController>() as MouseController;
        controller.Enable();

        isPaused = false;
    }

	public void sendBackWinScore() {
        Pause();

        victory = true;
		//send data to backend
		retreiveDataScript.StoreFightResults();
	}

	public void sendBackLoseScore() {
        Pause();

        defeat = true;
        
		//send data to backend
		retreiveDataScript.StoreFightResults();
	}


    public void BackendConnectFailed(string errorMsg)
    {
        Debug.LogError("Backend connection failed");

        this.errorMsg = errorMsg;
        gameState = GameState.START_FAILED;
    }

    public void BackendConnectSuccess()
    {
        Debug.Log("Connected to backend successfully, connecting to TCP server");

        Debug.Log("Fight data : " + retreiveDataScript.fightData );

        nodeJsSocketConnector.fightId = retreiveDataScript.fightData.FightId;
        nodeJsSocketConnector.playerId = retreiveDataScript.fightData.Player.Id;

        gameState = GameState.CONNECTING_VIA_TCP;
    }

    public void SocketConnectFailed(string errorMsg)
    {
        Debug.LogError("Socket connection failed");

        this.errorMsg = errorMsg;
        gameState = GameState.START_FAILED;
    }

    public void SocketConnectSuccess()
    {
        UnPause();
        gameState = GameState.ONGOING;
    }

    public void ResultSendSuccessful()
    {
        gameState = GameState.FINISHED;
    }

    public void ResultSendFailed(string errorMsg)
    {
        this.errorMsg = errorMsg;
        gameState = GameState.SENDING_RESULT_FAILED;
    }


}
