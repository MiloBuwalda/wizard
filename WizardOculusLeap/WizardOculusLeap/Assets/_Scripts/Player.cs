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
		if (Input.GetKeyDown (KeyCode.Space)) { //Test for execute spell
			ExecuteSpell ();
		}
	}

	//Asign which hand number int is left and right || 2 is no hand found
	void Hands(){
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		if (hands.Length == 0) {
			handLeft = 2;
			handRight = 2;
		}
		if (hands.Length > 0) {
			if (hands [0].GetLeapHand ().IsLeft) {
				handLeft = 0; 
				hands [0].gameObject.GetComponentInChildren<HandInteraction>().handNumber = 0;
			} else if (hands [0].GetLeapHand ().IsRight) {
				handRight = 0;
				hands [0].gameObject.GetComponentInChildren<HandInteraction>().handNumber = 0;
			}
			if (hands.Length > 1) {
				if (hands [1].GetLeapHand ().IsLeft) {
					handLeft = 1;
					hands [1].gameObject.GetComponentInChildren<HandInteraction>().handNumber = 1;
				}
				if (hands [1].GetLeapHand ().IsRight) {
					handRight = 1;
					hands [1].gameObject.GetComponentInChildren<HandInteraction>().handNumber = 1;
				}
			}
		}
	}

	//Add summoned element to element pool
	public void AddElementToPool(elementType t, int handNumber)
	{
		ElementManager el = ElementSpawner.instance.GetElementOfType (t, handNumber);
		elementPool.Add (el);
	}

	//Delete alle elements and references in element pool
	void EmptyElementPool()
	{
		foreach (ElementManager element in elementPool)	{
			Destroy(element.instance);
		}
		elementPool.Clear ();
	}

	//Combine elements from pool
	public void Combine(){
		if (elementPool.Count >  1){
			//Add Elements to combine
			ElementManager element = ElementSpawner.instance.CombineElements(elementPool);
			if (element != null)
			{
				EmptyElementPool();
				elementPool.Add(element);

				HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

				if (hands[handLeft].gameObject.GetComponentInChildren<HandInteraction>()._magnitude < hands[handRight].gameObject.GetComponentInChildren<HandInteraction>()._magnitude){
					handRightSlot = false;
				}else{
					handLeftSlot = false;
				}
			}
		}
	}

	//Create a shield with elements from pool
	public void ExecuteSpell(){
		SpellManager spell = SpellSpawner.instance.CreateSpell (elementPool);
		if (spell != null) {
			spellPool.Add (spell);
			EmptyElementPool ();
			handLeftSlot = false; 
			handRightSlot = false;
		}
	}

	//Create a spell with elements from pool
	public void ExecuteShield(){
		ShieldManager shield = ShieldSpawner.instance.CreateShield (elementPool[0]);
		if (shield != null) {
			shieldPool.Add(shield);
			EmptyElementPool();
			handLeftSlot = false;
			handRightSlot = false;
		}
	}
}
