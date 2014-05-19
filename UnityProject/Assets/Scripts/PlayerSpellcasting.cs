using UnityEngine;
using System.Collections;

public class PlayerSpellcasting : MonoBehaviour {
	public GameObject fireballPrefab;
	public GameObject frostballPrefab;
	public GameObject thunderballPrefab;
	GameObject cntPrefab;
	int currentSpell = 1;
	int maxSpells = 3;

	public float spellHeight = 1.4f;

	public float cooldownAmount = 1.0f, currentCooldown = 0.0f;



	Rect currentSpellRect = new Rect(10, 10, 200, 30);

	// Use this for initialization
	void Start () {
		cntPrefab = fireballPrefab;
	}

	void OnGUI() {
		GUI.Label( currentSpellRect, "Current spell : " + GetSpellName( currentSpell ) );
	}
	
	// Update is called once per frame
	void Update () {
		if( currentCooldown > 0.0f ) {
			currentCooldown -= Time.deltaTime;
		}

		if( Input.GetKey(KeyCode.Mouse0) ) {
			if( currentCooldown <= 0.0f ) {
				GetComponent<MouseController>().AlignRotation();

				Debug.Log("Attacking");



				Instantiate(cntPrefab, (transform.position + new Vector3(0.0f, spellHeight, 0.0f)) + transform.forward, transform.rotation);


				currentCooldown = cooldownAmount;
			}
		} else if( Input.GetKeyDown( KeyCode.Q ) ) {
			PreviousSpell();
		} else if( Input.GetKeyDown( KeyCode.E ) ) {
			NextSpell();
		}
	}

	void PreviousSpell ()
	{
		if( currentSpell <= 1 ) {
			currentSpell = maxSpells;
		} else {
			currentSpell--;
		}

		switch(currentSpell) {
		case 1:
			cntPrefab = fireballPrefab;
			break;
		case 2:
			cntPrefab = frostballPrefab;
			break;
		case 3:
			cntPrefab = thunderballPrefab;
			break;
		}
	}
	
	void NextSpell ()
	{
		if( currentSpell >= maxSpells ) {
			currentSpell = 1;
		} else {
			currentSpell++;
		}

		switch(currentSpell) {
		case 1:
			cntPrefab = fireballPrefab;
			break;
		case 2:
			cntPrefab = frostballPrefab;
			break;
		case 3:
			cntPrefab = thunderballPrefab;
			break;
		}
	}
	
	string GetSpellName(int spellNum) {
		switch(spellNum) {
			case 1:
				return "Fireball";
				break;
			case 2:
				return "Frostball";
				break;
			case 3:
				return "Thunderball";
				break;
		}

		return "";
	}
}
