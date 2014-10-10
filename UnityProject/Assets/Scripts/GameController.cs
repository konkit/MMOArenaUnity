using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;

    bool isGameStarted = false;

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
                    player = characterStats[0].gameObject;
                    enemy = characterStats[1].gameObject;
                } else {
                    player = characterStats[1].gameObject;
                    enemy = characterStats[0].gameObject;
                }

                isGameStarted = true;
            }
        }
	}
}
