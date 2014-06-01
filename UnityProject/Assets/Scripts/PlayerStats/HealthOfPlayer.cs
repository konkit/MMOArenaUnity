using UnityEngine;
using System.Collections;

public class HealthOfPlayer : MonoBehaviour {

	public float health, maxhealth;
    public float left, top;
	public delegate void DeathDelegate();
	public DeathDelegate deathDelegate;
    public bool isDeath;

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

        SetupTextures();

		maxhealth = 100;
		health = maxhealth;
        isDeath = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void decreaseHealth(int amount) {
		health -= amount;

		if( health <= 0 ) {
			doDead();
            health = 0;
		}
	}

	void doDead() {
        isDeath = true;
		deathDelegate();
	}

    void OnGUI()
    {
        Rect playerHealthBarPosition = new Rect(20, 20, baseTxPlayer.width, baseTxPlayer.height);
        Rect enemyHealthBarPostion = new Rect(Screen.width - baseTxEnemy.width - 20, 20, baseTxEnemy.width, baseTxEnemy.height);

        if (gameObject.tag == "Player")
        {
            GUI.DrawTexture(playerHealthBarPosition, baseTxPlayer);
            
            DrawHealthBarProgress(health, playerHealthBarPosition, greenTxPlayer, yellowTxPlayer, redTxPlayer);
        }
        if (gameObject.tag == "Enemy")
        {
            GUI.DrawTexture(enemyHealthBarPostion, healthbarbaseTexture);

            DrawHealthBarProgress(health, enemyHealthBarPostion, greenTxEnemy, yellowTxEnemy, redTxEnemy);
        }
    }
    void DrawHealthBarProgress(float current_health, Rect position, Texture greenTx, Texture yellowTx, Texture redTx)
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
