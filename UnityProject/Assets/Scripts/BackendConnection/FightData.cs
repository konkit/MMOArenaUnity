using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("fightData")]
public class FightData {
    [XmlElement("fightId")]
    public int FightId { get; set; }

	[XmlElement("player")]
	public Character Player { get; set; }

	[XmlElement("enemy")]
	public Character Enemy { get; set; }
}

public class FightResult {
	[XmlElement("playerHealthRemained")]
	public int playerHealthRemained { get; set; }

	[XmlElement("enemyHealthRemained")]
	public int enemyHealthRemained { get; set; }
}

[XmlRoot("fightAwards")]
public class FightAwards
{
    [XmlElement("error")]
    public string errorString { get; set; }

    [XmlElement("hasWon")]
    public bool hasWon { get; set; }

    [XmlElement("expEarned")]
    public int expEarned { get; set; }
}

[XmlRoot("character")]
public class Character {
    [XmlAttribute("id")]
    public int Id { get; set; }

	[XmlElement("name")]
	public string Name { get; set; }

	[XmlElement("level")]
	public int Level { get; set; }

	[XmlElement("exp")]
	public int Exp { get; set; }

	[XmlElement("maxhealth")]
	public int MaxHealth {get; set;}

	[XmlElement("coins")]
	public int Coins { get; set; }

	[XmlArray("items")]
	[XmlArrayItem("itemPossession")]
	public List<ItemPossession> Items { get; set; }

	[XmlArray("spells")]
	[XmlArrayItem("spellPossession")]
	public List<SpellPossession> Spells { get; set; }

}

public class Item {
	[XmlElement("name")]
	public string name { get; set; }
}

[System.Serializable]
public class Spell { 
	[XmlElement("name")]
	public string name {get; set;}

	[XmlElement("damage")]
	public int damage { get; set; }

	[XmlElement("cooldownTime")]
	public int cooldownTime { get; set; }

	[XmlElement("prefabType")]
	public int prefabType { get; set; }
}

public class ItemPossession {
	[XmlElement("item")]
	public Item item { get; set; }
		
	[XmlElement("amount")]
	public int amount { get; set; }

	[XmlElement("isEquiped")]
	public bool isEquiped { get; set; }
}

[System.Serializable]
public class SpellPossession {
	[XmlElement("spell")]
	public Spell spell { get; set;}

    public string ToString()
    {
        if (spell == null)
        {
            return "NULL";
        }
        else
        {
            return "Spell : " + spell.name;
        }
    }
}

