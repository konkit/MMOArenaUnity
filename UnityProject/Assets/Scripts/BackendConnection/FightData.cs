using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("fightData")]
public class FightData {
	[XmlElement("player")]
	public Character Player { get; set; }

	[XmlArray("playerItems")]
	[XmlArrayItem("itemPossession")]
	public List<ItemPossession> PlayerItems { get; set; }

	[XmlElement("enemy")]
	public Character Enemy { get; set; }
}

public class Character {
	[XmlElement("name")]
	public string Name { get; set; }

	[XmlElement("level")]
	public int Level { get; set; }

	[XmlElement("exp")]
	public int Exp { get; set; }

	[XmlElement("coins")]
	public int Coins { get; set; }

}

public class Item {
	[XmlElement("name")]
	public string name;
}

public class Spell { 
	public int damage;
}

public class ItemPossession {
	[XmlElement("item")]
	public Item item;
		
	public int amount;
	public bool isEquiped;
}

public class SpellPossession {
	public Character owner;
	public Spell spell;
}

