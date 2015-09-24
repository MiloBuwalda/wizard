using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {
  	public GameObject leapMotionOVRController = null;
  	public HandController handController = null;

	public GameObject cameraDirection = null;
	
	public Rigidbody magicBall;

	public int element = 0;
	Color[] colors;

	float speed = 20f;
	int magicBallFase = 0;

	public int rightHandNumber = 2;

	void Awake()
	{
		Color[] colors = {Color.red,Color.white, Color.blue, Color.green};
	}
	
	void LateUpdate () 
	{
    	if (leapMotionOVRController == null || handController == null)
     	 return;

    	HandModel[] hands = handController.GetAllGraphicsHands();

		//Debug.Log (element);

		if (hands.Length > 0) 
		{
			if(hands[0].GetLeapHand().IsRight)
			{
				rightHandNumber = 0;
			}
			else if(hands.Length > 1 && hands[1].GetLeapHand().IsRight)
			{
				rightHandNumber = 1;
			}

			if (hands[rightHandNumber].GetLeapHand().IsRight)
			{
				Vector3 directionRight = (hands [rightHandNumber].GetPalmPosition () - handController.transform.position).normalized;
				Vector3 normalRight = hands [rightHandNumber].GetPalmNormal ().normalized;

				//Debug.Log (hands [rightHandNumber].GetLeapHand ().GrabStrength);
					
				if (Vector3.Dot (directionRight, normalRight) < directionRight.sqrMagnitude * 0.5f) 
				{
					magicBallFase = 1;
				}
				if (Vector3.Dot (directionRight, normalRight) > directionRight.sqrMagnitude * 0.5f && magicBallFase == 1) 
				{
					Rigidbody magicBallClone = (Rigidbody)Instantiate (magicBall, leapMotionOVRController.transform.position, cameraDirection.transform.rotation);
					magicBallClone.velocity = cameraDirection.transform.forward * speed;
					magicBallFase = 0;
					//magicBallClone.GetComponent<Renderer>().material.color = colors[element];
				}
			}
		}
		
	
//		// Move forward if both palms are facing outwards! Whoot!
//    	if (hands.Length > 1)
//    	{
////   			Vector3 directionRight = (hands[0].GetPalmPosition() - handController.transform.position).normalized;
////      		Vector3 normalRight = hands[0].GetPalmNormal().normalized;
////
////      		Vector3 direction1 = (hands[1].GetPalmPosition() - handController.transform.position).normalized;
////      		Vector3 normal1 = hands[1].GetPalmNormal().normalized;
////
////      		if (Vector3.Dot(directionRight, normalRight) > directionRight.sqrMagnitude * 0.5f && Vector3.Dot(direction1, normal1) > direction1.sqrMagnitude * 0.5f)
////      		{
////      	  		Vector3 target = (hands[0].GetPalmPosition() + hands[1].GetPalmPosition()) / 2.0f;
////       			target.y = 0f;
////      	  		leapMotionOVRController.transform.position = Vector3.Lerp(leapMotionOVRController.transform.position, target, 0.1f);
////      		}
//    	}
	}
}
	