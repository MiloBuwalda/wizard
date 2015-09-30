using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public int element;

	public HandController handController = null;

	int handNumber = 0;
	bool notGrabbed;

//	float verticalSpeed = 1;
//	float amplitudev = 0.001f;

//	Vector3 floatingPosition;

	void Start()
	{
//		floatingPosition = transform.localPosition;
	}

	void FixedUpdate()
	{
//		floatingPosition.y += Mathf.Sin (Time.realtimeSinceStartup + verticalSpeed) * amplitudev;
//		transform.localPosition = floatingPosition;
	}

//	void OnTriggerStay(Collider other)
//	{
//		HandModel[] hands = handController.GetAllPhysicsHands();
//		if (other.gameObject.name == "palm")
//		{
//			if(hands[0].gameObject.transform == other.gameObject.transform.parent)
//			{
//				Debug.Log("Check is 0");
//				handNumber = 0;
//			}
//			else if(hands[1].gameObject.transform == other.gameObject.transform.parent)
//			{
//				Debug.Log("Check is 1");
//				handNumber = 1;
//			}
//		}
//
//		if (other.gameObject.name == "palm" && hands[handNumber].GetLeapHand().GrabStrength > 0.8)
//		{
//			transform.position = other.transform.position;
//		}
//	}
}