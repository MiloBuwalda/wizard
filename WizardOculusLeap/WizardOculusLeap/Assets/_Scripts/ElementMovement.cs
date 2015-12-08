using UnityEngine;
using System.Collections;

public class ElementMovement : MonoBehaviour {

	public float _magnitude;
	public 	int handNumber;
	Transform transformElementSlot;
	Vector3 previousLocation;

	void Start(){
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		transformElementSlot = hands[handNumber].transform.FindChild ("palm"); 
		transformElementSlot = transformElementSlot.transform.FindChild ("ElementSlot");
	}


	void Update(){
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;

		//Wanneer er een element aan de hand zit, maak er een schild van als het element verdwijnt
		if (transformElementSlot != null) {
			transform.position = transformElementSlot.transform.position;
		} else {
			HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
			if(GameManager.instance.player.shieldPool.Count > 0){
				GameManager.instance.player.EmptyShieldPool();
				if (hands[handNumber].GetLeapHand().IsLeft){
					GameManager.instance.player.ExecuteShield(GameManager.instance.player.leftElement);
				} else if (hands[handNumber].GetLeapHand().IsRight){
					GameManager.instance.player.ExecuteShield(GameManager.instance.player.rightElement);
				}
			} else {
				if(hands.Length > 0){
					if (hands[handNumber].GetLeapHand().IsLeft){
						GameManager.instance.player.ExecuteShield(GameManager.instance.player.leftElement);
					} else if (hands[handNumber].GetLeapHand().IsRight){
						GameManager.instance.player.ExecuteShield(GameManager.instance.player.rightElement);
					}
				}
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Element") {
			GameManager.instance.player.Combine ();
		}
	}
}
