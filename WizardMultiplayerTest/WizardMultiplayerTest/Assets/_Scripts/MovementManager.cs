using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {
  	public GameObject leapMotionOVRController = null;
  	public HandController handController = null;
	public GameObject cameraDirection = null;
	public Rigidbody magicBall;

	void LateUpdate () 
	{
//    	if (leapMotionOVRController == null || handController == null)
//     	 return;

//    	HandModel[] hands = handController.GetAllGraphicsHands();


//		// Move forward if both palms are facing outwards! Whoot!
//		if (hands.Length > 1)
//		{
//			Vector3 directionRight = (hands[0].GetPalmPosition() - handController.transform.position).normalized;
//			Vector3 normalRight = hands[0].GetPalmNormal().normalized;
//			
//			Vector3 direction1 = (hands[1].GetPalmPosition() - handController.transform.position).normalized;
//			Vector3 normal1 = hands[1].GetPalmNormal().normalized;
//			
//			if (Vector3.Dot(directionRight, normalRight) > directionRight.sqrMagnitude * 0.5f && Vector3.Dot(direction1, normal1) > direction1.sqrMagnitude * 0.5f)
//			{
//				Vector3 target = (hands[0].GetPalmPosition() + hands[1].GetPalmPosition()) / 2.0f;
//				target.y = 0f;
//				leapMotionOVRController.transform.position = Vector3.Lerp(leapMotionOVRController.transform.position, target, 0.1f);
//			}
//		}
	}
}
	