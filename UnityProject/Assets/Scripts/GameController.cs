using UnityEngine;
using System.Collections;

public enum GameState
{
    STARTING, 
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

	public RetreiveDataScript retreiveDataScript;

    public GameState gameState;

    public string errorMsg;

    void Awake()
    {
        gameState = GameState.STARTING;
        victory = false;
        defeat = false;
    }

	void Start () {
		// currently is redundant, but shows a way of thinking.
		enemy.GetComponent<CharacterStats>().deathDelegate += sendBackWinScore;
		player.GetComponent<CharacterStats>().deathDelegate += sendBackLoseScore;

        retreiveDataScript.RetreiveFightData();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState == GameState.STARTING)
        {
            if (retreiveDataScript.fightData != null)
            {
                gameState = GameState.ONGOING;
            }
        }
	}

    public void Pause()
    {
        MouseController controller = GameObject.FindObjectOfType<MouseController>() as MouseController;
        controller.Disable();
    }

    public void UnPause()
    {
        MouseController controller = GameObject.FindObjectOfType<MouseController>() as MouseController;
        controller.Disable();
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


    public void StartFailed(string errorMsg)
    {
        this.errorMsg = errorMsg;
        gameState = GameState.START_FAILED;
    }

    public void StartSuccessful()
    {
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
