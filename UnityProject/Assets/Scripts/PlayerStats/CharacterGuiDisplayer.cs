using UnityEngine;
using System.Collections;

public class CharacterGuiDisplayer : MonoBehaviour {

    GameController gameController;
	public CharacterStats playerStats, enemyStats;

	//Health bar display
	public float playerLeft, playerTop, enemyLeft, enemyTop;

	public Texture healthbarbaseTexture;
	public Texture healthbargreenTexture;
	public Texture healthbaryellowTexture;
	public Texture healthbarredTexture;
	
	private Texture baseTxPlayer;
	private Texture greenTxPlayer;
	private Texture yellowTxPlayer;
	private Texture redTxPlayer;
	
	private Texture baseTxEnemy;
	private Texture greenTxEnemy;
	private Texture yellowTxEnemy;
	private Texture redTxEnemy;

    public Texture[] labels;

    public float width, height;
    public Rect windowRect;
    private bool victory;
    private bool defeat;

	// Use this for initialization
	void Start () {
        gameController = GetComponent<GameController>();

        windowRect = new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height);


		if( playerStats == null || enemyStats == null ) {
			throw new UnityException("Character stats not set up");
		}

		SetupTextures();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
        switch (gameController.gameState)
        {
            case GameState.STARTING:
                OnGUI_Starting();
                break;
            case GameState.START_FAILED:
                OnGUI_StartFail();
                break;
            case GameState.ONGOING:
                OnGUI_Ongoing();
                break;
            case GameState.SENDING_RESULT:
                OnGUI_SendingResult();
                break;
            case GameState.SENDING_RESULT_FAILED:
                OnGUI_SendingResultFail();
                break;
            case GameState.FINISHED:
                OnGUI_Finished();
                break;
        }
	}

    void OnGUI_Starting()
    {
        DisplayPleaseWait();
    }

    void OnGUI_StartFail()
    {
        DisplayError();
        DisplayGoBackButton();
    }

    void OnGUI_Ongoing()
    {
        DisplayHealthBars();
        DisplayPlayerItems();
        DisplayPlayerSpells();
    }

    void OnGUI_SendingResult()
    {
        DisplayPleaseWait();
    }

    void OnGUI_SendingResultFail()
    {
        DisplayError();
        DisplayGoBackButton();
    }

    void OnGUI_Finished()
    {
        if (gameController.defeat)
        {
            GUI.backgroundColor = Color.red;
            windowRect = GUI.Window(0, windowRect, DoMyWindowDefeat, "");
        }
        if (gameController.victory)
        {
            GUI.backgroundColor = Color.yellow;
            windowRect = GUI.Window(0, windowRect, DoMyWindowWin, "");
        }
    }

    void DisplayError()
    {
        GUI.Label(new Rect(75, 30, 570, 250), "An error occured : " + gameController.errorMsg);
    }

    void DisplayPleaseWait()
    {
        GUI.Label(new Rect(75, 30, 570, 250), "Please wait");
    }

    void DisplayGoBackButton()
    {
        if (GUI.Button(new Rect(50, 210, 200, 70), "Go back"))
        {
            Application.ExternalCall("redirectAfterFight");
        }
    }

	void DisplayHealthBars() {
		Rect playerHealthBarPosition = new Rect(20, 20, baseTxPlayer.width, baseTxPlayer.height);
		Rect enemyHealthBarPostion = new Rect(Screen.width - baseTxEnemy.width - 20, 20, baseTxEnemy.width, baseTxEnemy.height);

		GUI.DrawTexture(playerHealthBarPosition, baseTxPlayer);
		DrawHealthBarProgress(playerStats.health, playerStats.maxhealth, playerHealthBarPosition, greenTxPlayer, yellowTxPlayer, redTxPlayer);

		GUI.DrawTexture(enemyHealthBarPostion, healthbarbaseTexture);
		DrawHealthBarProgress(enemyStats.health, enemyStats.maxhealth, enemyHealthBarPostion, greenTxEnemy, yellowTxEnemy, redTxEnemy);
	}

	void DisplayPlayerItems() {

	}

	void DisplayPlayerSpells() {

	}




	void DrawHealthBarProgress(float current_health, float maxhealth, Rect position, Texture greenTx, Texture yellowTx, Texture redTx)
	{
		int maxBazSize = 200;
		Texture currentTexture;
		float healthPercent = current_health / maxhealth;
		float healthBarSize = healthPercent * maxBazSize;
		
		if (healthPercent > 0.65)
		{
			currentTexture = greenTx;
		}
		else if (healthPercent > 0.35)
		{
			currentTexture = yellowTx;
		}
		else
		{
			currentTexture = redTx;
		}
		
		Rect barPos = new Rect(position.xMin, position.yMin, position.width * healthPercent, position.height);
		
		GUI.DrawTexture(barPos, currentTexture);
	}

	void DrawQuad(Rect position, Color color) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);
	}

	void SetupTextures()
	{
		baseTxPlayer = healthbarbaseTexture;
		greenTxPlayer = healthbargreenTexture;
		yellowTxPlayer = healthbaryellowTexture;
		redTxPlayer = healthbarredTexture;
		
		//renderer.material.SetTextureScale("baseTxPlayer", new Vector2(1,-1));
		
		
		baseTxEnemy = healthbarbaseTexture;
		greenTxEnemy = healthbargreenTexture;
		yellowTxEnemy = healthbaryellowTexture;
		redTxEnemy = healthbarredTexture;
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
        if (gameController.gameState == GameState.FINISHED)
        {
            GUI.Label(new Rect(100, 170, 200, 20), "Received exp : " + gameController.retreiveDataScript.fightAwards.expEarned.ToString());

            DisplayGoBackButton();
        }
    }

}
