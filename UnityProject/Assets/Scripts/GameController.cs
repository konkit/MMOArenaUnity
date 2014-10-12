using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public CharacterStats player;
    public CharacterStats enemy;

    public bool isGameStarted = false;
    public bool isGameFinished = false;

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

                isGameStarted = true;
            }
        }

	}
}
