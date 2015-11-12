using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour {	
	public float _magnitude;
	public int handNumber;

	Vector3 previousLocation;

	void Update(){
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Shield") {
			HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

			if(hands[handNumber].GetLeapHand().IsLeft){
				GameManager.instance.movementManager.insideShieldLeft = true;
			}
			if(hands[handNumber].GetLeapHand().IsRight){
				GameManager.instance.movementManager.insideShieldRight = true;
			}
		}
	}
	void OnTriggerExit (Collider other){
		if (other.tag == "Shield") {
			HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

			if(hands[handNumber].GetLeapHand().IsLeft){
				GameManager.instance.movementManager.insideShieldLeft = false;
			}
			if(hands[handNumber].GetLeapHand().IsRight){
				GameManager.instance.movementManager.insideShieldRight = false;
			}
		}
	}
}
