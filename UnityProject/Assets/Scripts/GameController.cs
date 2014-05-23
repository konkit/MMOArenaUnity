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

    void Awake()
    {
        victory = false;
        defeat = false;
    }
	// Use this for initialization
	void Start () {
		// currently is redundant, but shows a way of thinking.
		enemy.GetComponent<HealthOfPlayer>().deathDelegate += sendBackWinScore;
		player.GetComponent<HealthOfPlayer>().deathDelegate += sendBackLoseScore;
        windowRect = new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sendBackWinScore() {
		Debug.Log("This happens when enemy dies");
        victory = true;
		//send data to backend
	}

	public void sendBackLoseScore() {
		Debug.Log("This happens when player dies");
        defeat = true;
        
		//send data to backend
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
        MenuGame();
    }

    void DoMyWindowDefeat(int windowID)
    {
        GUI.Label(new Rect(90, 30, 570, 250), labels[1]);
        MenuGame();
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
}
