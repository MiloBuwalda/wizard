using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List <ElementManager> elementPool;
	public List <SpellManager> spellPool;
	public List <ShieldManager> shieldPool;
	public int handLeft;
	public int handRight;

	void Start () {
		elementPool = new List<ElementManager>();
		spellPool = new List<SpellManager>();
		shieldPool = new List<ShieldManager>();

		handLeft = 2;
		handRight = 2;
	}

	void Update () {
		Hands ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			ExecuteSpell ();
		}
	}

	public void AddElementToPool(elementType t)
	{
		ElementManager el = ElementSpawner.instance.GetElementOfType (t);
		elementPool.Add (el);
	}

	void EmptyElementPool()
	{
		foreach (ElementManager element in elementPool)	{
			Destroy(element.instance);
		}
		elementPool.Clear ();
	}

	//wanneer afschiet beweging
	void ExecuteSpell(){
		SpellManager spell = SpellSpawner.instance.CreateSpell (elementPool);
		if (spell != null) {
			spellPool.Add(spell);
			EmptyElementPool();
		}
	}

	void ExecuteShield(){
		ShieldManager shield = ShieldSpawner.instance.CreateShield (elementPool);
		if (shield != null) {
			shieldPool.Add(shield);
			EmptyElementPool();
		}
	}

	void Hands(){
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		if (hands.Length == 1) {
			if (hands [0].GetLeapHand ().IsLeft) {
				handLeft = 0; 
				handRight = 2;
			} else {
				handRight = 0;
				handLeft = 2;
			}
		} else if (hands.Length > 1) {
			if (hands [0].GetLeapHand ().IsLeft) {
				handLeft = 0; 
				handRight = 1;
			}
			if (hands [1].GetLeapHand ().IsLeft) {
				handLeft = 1; 
				handRight = 0;
			}
		} else {
			handLeft = 2;
			handRight = 2;
		}
	}

}
