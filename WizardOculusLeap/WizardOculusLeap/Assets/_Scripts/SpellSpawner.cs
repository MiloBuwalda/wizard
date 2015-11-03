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

	public SpellManager CreateSpell(List<ElementManager> list){
		if (list.Count > 0) {
			SpellManager spell = new SpellManager();
			ElementManager basis = list[0];
			GameObject g;
			if (spellBook.TryGetValue (basis.elementType.ToString() + "Spell", out g))	{
				spell.instance = (GameObject)Instantiate(g, transform.position, transform.rotation);
				spell.Setup(list);
			}

			return spell;
		} else {
			//buzz sound
			return null;
		}

	}
}
