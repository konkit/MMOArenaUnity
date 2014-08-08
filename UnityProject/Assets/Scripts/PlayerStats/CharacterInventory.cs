using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventory : MonoBehaviour {

	public List<ItemPossession> itemPossession;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadInventory(Character characterData) {
		itemPossession = characterData.Items;
	}
}
