using UnityEngine;
using System.Collections;

public class HealthOfPlayer : MonoBehaviour {

	public int health, maxhealth;

	public delegate void DeathDelegate();
	public DeathDelegate deathDelegate;

	// Use this for initialization
	void Start () {
		health = 100;
		maxhealth = 100;
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
		deathDelegate();
	}
}
