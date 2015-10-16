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
		if (other.gameObject.tag == "Element" && !occupied) 
		{
			elementSlot = other.gameObject;
			other.gameObject.GetComponent<SphereInteraction>().notInsideElement = false;
			occupied = true;
		}
		if (other.gameObject == elementSlot && !occupied) 
		{
			other.gameObject.GetComponent<SphereInteraction>().notInsideElement = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		HandModel[] hands = GameObject.Find ("MovementManager").GetComponent<MovementManager> ().handController.GetAllPhysicsHands ();

		if (hands.Length > 0)
		{
			if (other.gameObject == elementSlot) 
			{
				if (hands [0].gameObject.transform == gameObject.transform.parent) 
				{
					handNumber = 0;
				} 
				else if (hands [1].gameObject.transform == gameObject.transform.parent) 
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
				grabbed = false;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == elementSlot && occupied) 
		{
			occupied = false;
			other.gameObject.GetComponent<SphereInteraction>().notInsideElement = true;
		}
	}
}
