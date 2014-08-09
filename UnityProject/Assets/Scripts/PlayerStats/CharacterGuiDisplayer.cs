using UnityEngine;
using System.Collections;

public class CharacterGuiDisplayer : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		if( playerStats == null || enemyStats == null ) {
			throw new UnityException("Character stats not set up");
		}

		SetupTextures();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		DisplayHealthBars();
		DisplayPlayerItems();
		DisplayPlayerSpells();
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
}
