using UnityEngine;
using System.Collections;

public class HealthOfPlayer : MonoBehaviour {

	public int health, maxhealth;
    public float left, top;
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
            health = 0;
		}
	}

	void doDead() {
        isDeath = true;
		deathDelegate();
	}

    void OnGUI()
    {        
        if(gameObject.tag == "Player")
        {
            GUI.Box(new Rect(Screen.width / 2-left, Screen.height / 2-top, 100, 20), "Health: " + health.ToString());
        }
        if(gameObject.tag == "Enemy")
        {
            GUI.Box(new Rect(Screen.width / 2 + left, Screen.height / 2 - top, 130, 20), "Health Enemy: " + health.ToString());
        }
        
    }
}
