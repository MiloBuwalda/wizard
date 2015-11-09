using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour 
{	
	public float _magnitude;
	GameObject elementSlot = null;
	Transform transformElementSlot = null;
	int handNumber = 0;
	bool occupied;
	bool grabbed;
	Vector3 previousLocation;
	string spawnName;

	void Awake(){
		transformElementSlot = transform.FindChild ("ElementSlot");
	}

	void Update(){
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Element" && !occupied) {
			elementSlot = other.gameObject;
			occupied = true; 
		}
	}

	void OnTriggerStay(Collider other){
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (hands.Length > 0) {
			if (other.gameObject == elementSlot) {
				if (hands [0].gameObject.transform == gameObject.transform.parent) {
					handNumber = 0;
				} else if (hands [1].gameObject.transform == gameObject.transform.parent) {
					handNumber = 1;
				}
			}
			if (other.gameObject == elementSlot && hands [handNumber].GetLeapHand ().GrabStrength > 0.8 && !grabbed) {
				grabbed = true;
			} else if (other.gameObject == elementSlot && hands [handNumber].GetLeapHand ().GrabStrength < 0.8 && grabbed) {
				grabbed = false;
			} else {
				grabbed = false;
			}
		} else {
			if(grabbed){
				grabbed = false;
			}
		}

		if(grabbed)	{
			other.transform.position = transformElementSlot.transform.position;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == elementSlot && occupied) {
			occupied = false;
		}
	}
}
