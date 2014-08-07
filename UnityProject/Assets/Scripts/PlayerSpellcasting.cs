using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpellcasting : MonoBehaviour {
	//Spell prefabs
	public GameObject[] prefabs;

	//List with spell data from server
	public List<SpellPossession> spellPossession;

	// setting of current spellCasting
	public int currentSpellNum = 1;

	public float spellHeight = 1.4f;
	public float currentCooldown = 0.0f;

	CharacterControlInterface controlInterface;

	Rect currentSpellRect = new Rect(10, 10, 200, 30);

	Rect spellListRect = new Rect(10, 300, 200, 30);

	// Use this for initialization
	void Start () {
		controlInterface = GetComponent<CharacterControlInterface>();

		LoadSpellPrefabs();
	}
	
	// Update is called once per frame
	void Update () {
		if( spellPossession == null ) {
			return;
		}

		if( currentCooldown > 0.0f ) {
			currentCooldown -= Time.deltaTime * 1000;
		}

		if( controlInterface.isPunch ) {
			castCurrentSpell();
		} else if( controlInterface.previousSpell ) {
			PreviousSpell();
		} else if( controlInterface.nextSpell ) {
			NextSpell();
		}
	}

	void castCurrentSpell() {
		if( currentCooldown <= 0.0f ) {
			MouseController mouse = GetComponent<MouseController>();
			if( mouse ) mouse.AlignRotation();

			Spell cntSpell = spellPossession[currentSpellNum].spell;
			GameObject cntPrefab = prefabs[cntSpell.prefabType];

			Vector3 instantiatePos = (transform.position + new Vector3(0.0f, spellHeight, 0.0f)) + transform.forward;
			Instantiate(cntPrefab, instantiatePos, transform.rotation);

			currentCooldown = cntSpell.cooldownTime;
		}
	}

	void PreviousSpell ()
	{
		if( currentSpellNum <= 0 ) {
			currentSpellNum = spellPossession.Count - 1;
		} else {
			currentSpellNum--;
		}
	}
	
	void NextSpell ()
	{
		if( currentSpellNum >= spellPossession.Count - 1) {
			currentSpellNum = 0;
		} else {
			currentSpellNum++;
		}
	}

	public void LoadSpells(FightData data) {
		spellPossession = data.Player.Spells;
	}

	void OnGUI() {
		if( spellPossession == null ) {
			return;
		}
		
		for( int i=0; i< spellPossession.Count; i++) {
			SpellPossession it = spellPossession[i];
			
			string description = it.spell.name + " ( damage : " + it.spell.damage.ToString() + " )";
			
			GUI.Label( getRectWithTopOffset(spellListRect, i * 25), description);
		}
	}
	
	private Rect getRectWithTopOffset(Rect rect, int offset) {
		return new Rect(rect.left, rect.top + offset, rect.width, rect.height);
	}

	private void LoadSpellPrefabs() {
		RetreiveDataScript script = FindObjectOfType(typeof( RetreiveDataScript )) as RetreiveDataScript;
		prefabs = script.prefabs;
	}
}
