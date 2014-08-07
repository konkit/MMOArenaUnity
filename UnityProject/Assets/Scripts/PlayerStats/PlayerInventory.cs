using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

	public List<ItemPossession> itemPossession;

	public Rect itemListRect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if( itemPossession == null ) {
			return;
		}

		for( int i=0; i< itemPossession.Count; i++) {
			ItemPossession it = itemPossession[i];
			GUI.Label( getRectWithTopOffset(itemListRect, i * 25), it.item.name + " x" + it.amount );
		}
	}

	public void LoadInventory(FightData data) {
		itemPossession = data.Player.Items;
	}

	private Rect getRectWithTopOffset(Rect rect, int offset) {
		return new Rect(rect.left, rect.top + offset, rect.width, rect.height);
	}
}
