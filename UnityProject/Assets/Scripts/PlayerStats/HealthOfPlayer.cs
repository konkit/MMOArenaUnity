using UnityEngine;
using System.Collections;

public class HealthOfPlayer : MonoBehaviour {

	public int health, maxhealth;

	public delegate void DeathDelegate();
	public DeathDelegate deathDelegate;
    public bool isDeath;
	// Use this for initialization
	void Start () {
		health = 100;
		maxhealth = 100;
        isDeath = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void decreaseHealth(int amount) {
		health -= amount;

		if( health <= 0 ) {
			doDead();
		}
	}

	void doDead() {
        isDeath = true;
		deathDelegate();
	}
}
