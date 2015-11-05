using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List <ElementManager> elementPool;
	public List <SpellManager> spellPool;
	public List <ShieldManager> shieldPool;
	public int handLeft;
	public int handRight;
	public bool handLeftSlot;
	public bool handRightSlot;

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

	public void AddElementToPool(elementType t, int handNumber)
	{
		ElementManager el = ElementSpawner.instance.GetElementOfType (t, handNumber);
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
	public void ExecuteSpell(){
		if (elementPool[0] != null) {
			SpellManager spell = SpellSpawner.instance.CreateSpell (elementPool);
			if (spell != null) {
				spellPool.Add (spell);
				EmptyElementPool ();
				handLeftSlot = false;
				handRightSlot = false;
			}
		}
	}

	void ExecuteShield(){
		ShieldManager shield = ShieldSpawner.instance.CreateShield (elementPool);
		if (shield != null) {
			shieldPool.Add(shield);
			EmptyElementPool();
			handLeftSlot = false;
			handRightSlot = false;
		}
	}

	void Hands(){
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		if (hands.Length == 0) {
			handLeft = 2;
			handRight = 2;
		}
		if (hands.Length > 0) {
			if (hands [0].GetLeapHand ().IsLeft) {
				handLeft = 0; 
			} else if (hands [0].GetLeapHand ().IsRight) {
				handRight = 0;
			}
			if (hands.Length > 1) {
				if (hands [1].GetLeapHand ().IsLeft) {
					handLeft = 1;
				}
				if (hands [1].GetLeapHand ().IsRight) {
					handRight = 1;
				}
			}
		}
	}

}
