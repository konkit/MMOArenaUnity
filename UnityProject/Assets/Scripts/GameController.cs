using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public CharacterStats player;
    public CharacterStats enemy;

    public bool backendDataLoaded = false;
    public bool isGameStarted = false;
    public bool isGameFinished = false;

    public bool isPaused = false;

    public string errorMsg = "";
    public string loadingMsg = "";

    public string userBackendAddress = "http://localhost:8080/GrailsMMOArena";
    public string matchmakerAddress = "http://localhost:5000";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isGameStarted == false)
        {
            CharacterStats[] characterStats = FindObjectsOfType(typeof(CharacterStats)) as CharacterStats[];

            if (characterStats.Length != 2)
            {
                if (backendDataLoaded == true)
                {
                    loadingMsg = "Waiting for other player to connect ...";
                }

                return;
            }
            else
            {
                if ( characterStats[0].GetComponent<PhotonView>().isMine )
                {
                    player = characterStats[0];
                    enemy = characterStats[1];
                } else {
                    player = characterStats[1];
                    enemy = characterStats[0];
                }

                Debug.Log("Loading player spellcasting data");

                PlayerDataFetcher playerDataFetcher = GetComponent<PlayerDataFetcher>();
                player.LoadFromData(playerDataFetcher.playerData);
                player.GetComponent<PhotonCharacterSpellcasting>().LoadSpells(playerDataFetcher.playerData);

                Debug.Log("Loading enemy spellcasting data");

                EnemyDataFetcher enemyDataFetcher = GetComponent<EnemyDataFetcher>();
                enemy.LoadFromData(enemyDataFetcher.enemyData);
                enemy.GetComponent<PhotonCharacterSpellcasting>().LoadSpells(enemyDataFetcher.enemyData);

                if (player.health > 0)
                {
                    isGameStarted = true;
                }
            }
        }
        else if( isGameStarted == true )
        {
            if (player.health <= 0 || enemy == null || enemy.health <= 0)
            {
                isGameFinished = true;

                PauseGame();
            }
        }

	}

    public void PauseGame()
    {
        player.GetComponent<MouseController>().Disable();
        player.GetComponent<HumanPlayerController>().enabled = false;

        isPaused = true;
    }

    public void UnpauseGame()
    {
        player.GetComponent<MouseController>().Enable();
        player.GetComponent<HumanPlayerController>().enabled = true;

        isPaused = false;
    }
}
