using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;
    public Texture[] labels;

    public float width, height;
    public Rect windowRect;
    private bool victory;
    private bool defeat;

	public RetreiveDataScript retreiveDataScript;

    void Awake()
    {
        victory = false;
        defeat = false;
    }
	// Use this for initialization
	void Start () {
		// currently is redundant, but shows a way of thinking.
		enemy.GetComponent<CharacterStats>().deathDelegate += sendBackWinScore;
		player.GetComponent<CharacterStats>().deathDelegate += sendBackLoseScore;
        windowRect = new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sendBackWinScore() {
		Debug.Log("This happens when enemy dies");

        MouseController controller = GameObject.FindObjectOfType<MouseController>() as MouseController;
        controller.Disable();

        victory = true;
		//send data to backend

		retreiveDataScript.StoreFightResults();
	}

	public void sendBackLoseScore() {
		Debug.Log("This happens when player dies");

        MouseController controller = GameObject.FindObjectOfType<MouseController>() as MouseController;
        controller.Disable();

        defeat = true;
        
		//send data to backend
		retreiveDataScript.StoreFightResults();
	}

    void OnGUI()
    {
        if(defeat)
        {
            GUI.backgroundColor = Color.red;
            windowRect = GUI.Window(0, windowRect, DoMyWindowDefeat, "");
        }
        if(victory)
        {
            GUI.backgroundColor = Color.yellow;
            windowRect = GUI.Window(0, windowRect, DoMyWindowWin, "");
        }
    }

    void DoMyWindowWin(int windowID)
    {
        GUI.Label(new Rect(75, 30, 570, 250), labels[0]);
        FightResultsWindow();
    }

    void DoMyWindowDefeat(int windowID)
    {
        GUI.Label(new Rect(90, 30, 570, 250), labels[1]);
        FightResultsWindow();
    }

    void MenuGame()
    {
        if (GUI.Button(new Rect(50, 180, 200, 70), "Restart") || Input.GetKeyDown(KeyCode.P))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (GUI.Button(new Rect(260, 180, 200, 70), "Exit") || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FightResultsWindow()
    {
        if (retreiveDataScript.fightAwards != null)
        {
            GUI.Label(new Rect(100, 170, 200, 20), "Received exp : " + retreiveDataScript.fightAwards.expEarned.ToString());
        }
        
        if (GUI.Button(new Rect(50, 210, 200, 70), "Go back") )
        {
            Application.ExternalCall("redirectAfterFight");
        }
    }
}
