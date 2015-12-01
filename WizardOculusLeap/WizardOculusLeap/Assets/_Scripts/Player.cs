using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Team
{
	Red,
	Blue,
	None,
}

public class Player : MonoBehaviour {

	public List <ElementManager> elementPool;
	public List <SpellManager> spellPool;
	public List <ShieldManager> shieldPool;
	public ElementManager leftElement;
	public ElementManager rightElement;
	public elementType triggerShieldElementTypeSpell;
	public elementType triggerShieldElementTypeLeft;
	public elementType triggerShieldElementTypeRight;
	public Vector3 triggerShieldPosition;
	public int handLeft;
	public int handRight;
	public bool handLeftSlot;
	public bool handRightSlot;


	void Start () {
		elementPool = new List<ElementManager>();
		spellPool = new List<SpellManager>();
		shieldPool = new List<ShieldManager>();
		leftElement = null;
		rightElement = null;

		handLeft = 2;
		handRight = 2;
	}

	void Update () {
		Hands ();
		Vangnet ();
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

	void Vangnet (){
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		if (hands.Length == 0) {
			if(handLeftSlot) {
				handLeftSlot = false;
			}
			if (handRightSlot){
				handRightSlot = false;
			}
			if (GameManager.instance.movementManager.summoning) {
			GameManager.instance.movementManager.summoning = false;
			}
			if (GameManager.instance.movementManager.insideShield) {
			GameManager.instance.movementManager.insideShield = false;
			}
			if (GameManager.instance.movementManager.insideShieldLeft) {
			GameManager.instance.movementManager.insideShieldLeft = false;
			}
			if (GameManager.instance.movementManager.insideShieldRight) {
			GameManager.instance.movementManager.insideShieldRight = false;
			}
		}
	}

	//Add summoned element to element pool
	public void AddElementToPool(elementType t, int handNumber)
	{
		ElementManager element = ElementSpawner.instance.GetElementOfType (t, handNumber);
		elementPool.Add (element);

		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (hands[handNumber].GetLeapHand().IsLeft) {
			leftElement = element;
		} else if (hands[handNumber].GetLeapHand().IsRight) {
			rightElement = element;
		}
	}

	//Delete alle elements and references in element pool
	void EmptyElementPool(){
		foreach (ElementManager element in elementPool)	{
			Destroy(element.instance);
		}
		elementPool.Clear ();
	}

	public void EmptyShieldPool(){
//		foreach (ShieldManager shield in shieldPool)	{
////			Destroy(shield.instance);
//		}

		for (int i = 0; i < shieldPool.Count; i++) {
			shieldPool[i].DestroyMe();
		}

		shieldPool.Clear ();
		GameManager.instance.movementManager.insideShieldLeft = false;
		GameManager.instance.movementManager.insideShieldRight = false;
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
					leftElement = element;
				}else{
					handLeftSlot = false;
					rightElement = element;
				}
			}
		}
	}

	//Create a shield with elements from pool
	public void ExecuteSpell(int shieldId){
		SpellManager spell = SpellSpawner.instance.CreateSpellNetworked (triggerShieldElementTypeSpell, triggerShieldPosition);
		if (spell != null) {
			spellPool.Add (spell);
			//EmptyShieldPool();
			RemoveShield(shieldId);
			GameManager.instance.movementManager.insideShield = false;
			handLeftSlot = false; 
			handRightSlot = false;
		}
	}

	//Create a spell with elements from pool
	public void ExecuteShield(ElementManager elementManager){

//		ExecuteSpell (0);
//		return;
//		// NO HE DIDNT : Workaround

		ShieldManager shield = ShieldSpawner.instance.CreateShield (elementManager);
		if (shield != null) {

			shieldPool.Add(shield);
			EmptyElementPool();
			handLeftSlot = false;
			handRightSlot = false;
		}
	}


	public void RemoveShield(int shieldId){

		int iLength = shieldPool.Count; 
		for (int i = 0; i < iLength; i++) {
			if(shieldPool[i].id == shieldId)
			{
				shieldPool[i].DestroyMe();
			}
		}

//		foreach (ShieldManager s in shieldPool) {
//			if( s.id == shieldId){
//				if(shieldPool.Remove(s)){
//					s.DestroyMe();
//				}
//				// check if s still exists
//
//			}
//		}

	}



	Team m_Team;

	public Team Team
	{
		get
		{
			return m_Team;
		}
	}

	public void SetTeam ( Team team)
	{
		m_Team = team;

		// Can set specific team colours here
	}

	void OnPhotonSerializeView ( PhotonStream stream, PhotonMessageInfo info)
	{
		SerializeState (stream, info);

	}

	void SerializeState (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting == true) {
			//stream.SendNext( health);
		}
	}
}
