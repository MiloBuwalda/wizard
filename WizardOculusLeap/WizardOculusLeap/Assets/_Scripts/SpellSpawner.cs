using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellSpawner : MonoBehaviour {
	public static SpellSpawner instance;
	public Dictionary<string, GameObject> spellBook;
	public Transform inFrontOfPlayer;


	
	// shield id not a counter
	public int spellId = 1000;

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
			//spell.instance = (GameObject)Instantiate (g, position, transform.rotation);
			spell.instance = (GameObject)Instantiate (g, inFrontOfPlayer.position, transform.rotation);
			spell.Setup ();
			spell.spellCollision.elementType = t;
		} else {
			spell = null;
		}

		return spell; 
	}

	//Create a spell with elements from player when player demands it
	public SpellManager CreateSpellNetworked(elementType t, Vector3 position){
		SpellManager spell = new SpellManager();
		GameObject g;

		spell.id = spellId;
		spellId++;
//		shield.id = shieldId;
//		shieldId++;
		PhotonView currentPhotonView;
		SpellObserver spellObserver;

		if (spellBook.TryGetValue (t.ToString () + "Spell", out g)) {

			currentPhotonView = g.GetComponent<PhotonView>();
			
			if(currentPhotonView == null){
				currentPhotonView = g.AddComponent<PhotonView>();
			}
			

			spellObserver = g.GetComponent<SpellObserver>();
			if (spellObserver == null){
				spellObserver = g.AddComponent<SpellObserver>();
			}
			
			//				Debug.Log("currentPhotonView Count: " + currentPhotonView.ObservedComponents.Count);
			currentPhotonView.ObservedComponents.Clear();
//			currentPhotonView.ObservedComponents.Add();

			if(currentPhotonView.ObservedComponents!=null && currentPhotonView.ObservedComponents.Count==0)
			{
				currentPhotonView.ObservedComponents.Add(spellObserver);
//				currentPhotonView.ObservedComponents.Add(this.transform);
				//					currentPhotonView.ObservedComponents[0] = shieldObserver;
				//					currentPhotonView.ObservedComponents.
			}
			
			// Instantiate on network (call current element shield from within shield folder)
			spell.instance = (GameObject) PhotonNetwork.Instantiate(
				"Spells/"+g.name, inFrontOfPlayer.position, inFrontOfPlayer.rotation,0);
			Debug.Log("spellspawner:instantiate " + g.name);

//			spell.instance.

//			spell.instance = (GameObject)Instantiate (g, inFrontOfPlayer.position, transform.rotation);
			spell.Setup ();
			spell.spellCollision.elementType = t;
		} else {
			spell = null;
		}
		
		return spell; 
	}
}
