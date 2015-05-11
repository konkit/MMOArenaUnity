using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectionPlaceholder : MonoBehaviour {

    public bool isActive = false;

    public MMOArenaPhotonConnector photonConnector;
    public GameController gameController;

    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        if (isActive && Application.platform == RuntimePlatform.WindowsEditor)
        {
            photonConnector = GetComponent<MMOArenaPhotonConnector>();
            gameController = GetComponent<GameController>();

            SetPlayerAndEnemyData();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetPlayerAndEnemyData()
    {
        photonConnector.isOffline = true;

        gameController.playerData = generateCharacterData();
        gameController.enemyData = generateCharacterData();

        gameController.StartGame("placeholder_room_name");

        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    Character generateCharacterData()
    {
        Character generatedCharacter = new Character();
        generatedCharacter.Id = 1;
        generatedCharacter.Name = "John Doe";
        generatedCharacter.Level = 1;
        generatedCharacter.Exp = 0;
        generatedCharacter.MaxHealth = 100;

        generatedCharacter.Spells = generateSpells();

        return generatedCharacter;
    }

    List<SpellPossession> generateSpells()
    {
        Spell fireball = new Spell();
        fireball.cooldownTime = 10;
        fireball.damage = 10;
        fireball.prefabType = 1;
        fireball.name = "Fireball";

        Spell frostball = new Spell();
        fireball.cooldownTime = 10;
        fireball.damage = 10;
        fireball.prefabType = 1;
        fireball.name = "Frostball";

        Spell thunderball = new Spell();
        fireball.cooldownTime = 10;
        fireball.damage = 10;
        fireball.prefabType = 1;
        fireball.name = "Thunderball";

        SpellPossession sp1 = new SpellPossession();
        sp1.spell = fireball;

        SpellPossession sp2 = new SpellPossession();
        sp1.spell = frostball;

        SpellPossession sp3 = new SpellPossession();
        sp1.spell = thunderball;

        List<SpellPossession> list = new List<SpellPossession>();
        list.Add(sp1);
        list.Add(sp2);
        list.Add(sp3);

        return list;
    }
}
