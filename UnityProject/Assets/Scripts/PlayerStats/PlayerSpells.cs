using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpells : MonoBehaviour {

	public List<SpellPossession> spellPossession;
	
	public Rect spellListRect;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
	
	public void LoadSpells(FightData data) {
		spellPossession = data.Player.Spells;
	}
	
	private Rect getRectWithTopOffset(Rect rect, int offset) {
		return new Rect(rect.left, rect.top + offset, rect.width, rect.height);
	}

}
