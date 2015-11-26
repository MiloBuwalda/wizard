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

	void OnTriggerStay (Collider other){
		if (other.tag == "Shield") {
			HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

			if (hands[handNumber].GetLeapHand().IsLeft && !GameManager.instance.movementManager.insideShieldLeft){
				GameManager.instance.player.triggerShieldElementTypeLeft = other.gameObject.GetComponent<ShieldElement>().elementType;
				GameManager.instance.movementManager.insideShieldLeft = true;
			} else if (hands[handNumber].GetLeapHand().IsRight && !GameManager.instance.movementManager.insideShieldRight){
				GameManager.instance.player.triggerShieldElementTypeRight = other.gameObject.GetComponent<ShieldElement>().elementType;
				GameManager.instance.movementManager.insideShieldRight = true;
			}

			GameManager.instance.player.triggerShieldPosition = other.gameObject.transform.position;
			GameManager.instance.player.triggerShieldElementTypeSpell = other.gameObject.GetComponent<ShieldElement>().elementType;
			GameManager.instance.movementManager.insideShield = true;
			GameManager.instance.movementManager.insideShieldId = other.gameObject.GetComponent<ShieldElement>().id;
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
