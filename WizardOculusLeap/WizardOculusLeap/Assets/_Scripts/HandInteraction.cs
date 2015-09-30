using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour 
{	
	int handNumber = 0;
	bool occupied;

	void OnTriggerStay(Collider other)
	{
		HandModel[] hands = GameObject.Find ("MovementManager").GetComponent<MovementManager> ().handController.GetAllPhysicsHands ();
		//Debug.Log (other.name);
		if (other.gameObject.tag == "Element")
		{
			if(hands[0].gameObject.transform == gameObject.transform.parent)
			{
				handNumber = 0;
			}
			else if(hands[1].gameObject.transform == gameObject.transform.parent)
			{
				handNumber = 1;
			}
		}
		
		if (other.gameObject.tag == "Element" && hands [handNumber].GetLeapHand ().GrabStrength > 0.8) {
			other.transform.position = transform.position;
			if (occupied == false)
				occupied = true;
		} else if (occupied == true) {
			occupied = false;
		}
	}
}
