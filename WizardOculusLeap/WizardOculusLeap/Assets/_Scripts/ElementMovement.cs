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
		if (transformElementSlot != null) {
			transform.position = transformElementSlot.transform.position;
		} else {
			HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
			if (hands[handNumber].GetLeapHand().IsLeft){
				GameManager.instance.player.ExecuteShield(GameManager.instance.player.leftElement);
			} else if (hands[handNumber].GetLeapHand().IsRight){
				GameManager.instance.player.ExecuteShield(GameManager.instance.player.rightElement);
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Element") {
			GameManager.instance.player.Combine ();
		}
	}
}
