using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

	public string name = "John Doe";
	public int level = 1;
	public int exp = 0;

	public int health = 100, maxhealth = 100;

	public delegate void DeathDelegate();
	public DeathDelegate deathDelegate;
	public bool isDeath;
	
	// Use this for initialization
	void Start () {
		isDeath = false;
		health = maxhealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadFromData(Character characterData) {
		name = characterData.Name;
		level = characterData.Level;
		exp = characterData.Exp;
		health = maxhealth = characterData.MaxHealth;
	}

    [RPC]
	public void decreaseHealth(int amount) {
        Debug.Log("Decreasing health from " + gameObject + " player, amount : " + amount);

		health -= amount;
		
		if( health <= 0 ) {
			doDead();
			health = 0;
		}

        Debug.Log("Resulting health = " + health);
	}
	
	void doDead() {
		isDeath = true;
		//deathDelegate();
	}
}
