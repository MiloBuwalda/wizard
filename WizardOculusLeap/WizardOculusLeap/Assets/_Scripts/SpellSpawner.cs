using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellSpawner : MonoBehaviour {
	public static SpellSpawner instance;
	public Dictionary<string, GameObject> spellBook;


	void Awake () {
		//singleton
		instance = this;
	}

	void Start()	{
		spellBook = new Dictionary<string, GameObject> ();
		GameObject[] spells = Resources.LoadAll<GameObject> ("Spells");
		foreach(GameObject g in spells)	{
			spellBook.Add(g.name,g);
		}
	}

	//Create a spell with elements from player when player demands it
	public SpellManager CreateSpell(elementType t, Vector3 position){
		SpellManager spell = new SpellManager();
		GameObject g;
		if (spellBook.TryGetValue (t.ToString () + "Spell", out g)) {
			spell.instance = (GameObject)Instantiate (g, position, transform.rotation);
			spell.Setup ();
		} else {
			spell = null;
		}

		return spell;
	}
}
