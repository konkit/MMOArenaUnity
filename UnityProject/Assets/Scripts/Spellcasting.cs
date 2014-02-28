using UnityEngine;
using System.Collections;

public class Spellcasting : MonoBehaviour {

	public bool isSpellcasting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( isSpellcasting ) {

			if( Input.GetKey(KeyCode.Mouse0) ) {
				Debug.Log("Attacking");
			}
		}	
	}
}
