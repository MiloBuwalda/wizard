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
			Debug.Log ("Create Shield");
			GameManager.instance.player.ExecuteShield();
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Element") {
			GameManager.instance.player.Combine ();
		}
	}
}
