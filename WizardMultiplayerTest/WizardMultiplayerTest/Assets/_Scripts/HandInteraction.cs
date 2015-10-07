using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour 
{	
	int handNumber = 0;
	GameObject elementSlot = null;
	bool occupied;
	bool grabbed;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Element" && occupied == false) 
		{
			elementSlot = other.gameObject;
			other.gameObject.GetComponent<SphereInteraction>().notGrabbed = false;
			occupied = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		HandModel[] hands = GameObject.Find ("MovementManager").GetComponent<MovementManager> ().handController.GetAllPhysicsHands ();
		//Debug.Log (other.name);
		if (other.gameObject == elementSlot)
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
		
		if (other.gameObject == elementSlot && hands [handNumber].GetLeapHand ().GrabStrength > 0.8) 
		{
			other.transform.position = transform.position;
			grabbed = true;
		}
		else if (other.gameObject == elementSlot && hands [handNumber].GetLeapHand ().GrabStrength < 0.8 && grabbed == true) 
		{
			//other.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
			grabbed = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == elementSlot && occupied == true) 
		{
			occupied = false;
			other.gameObject.GetComponent<SphereInteraction>().notGrabbed = true;
		}
	}
}
