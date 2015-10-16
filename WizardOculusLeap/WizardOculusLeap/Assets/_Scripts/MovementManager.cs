using UnityEngine;
using System.Collections;
using Leap;

public class MovementManager : MonoBehaviour {
  	public GameObject leapMotionOVRController = null;
	public GameObject cameraDirection = null;
  	public HandController handController = null;

	Controller leapController;
	Listener leapListener;

	void Start () {
		leapController = new Controller();
		leapListener = new Listener();
		
		// Log that the Leap Motion device is attached and recognized
		foreach(Device item in leapController.Devices)
		{
			Debug.Log("Leap Motion Device ID:" + item.ToString());
		}
		// Add a listener so that the controller is waiting for inputs
		leapController.AddListener(leapListener);
		// Check that the leap is fully connected and ready for input
		Debug.Log("Leap Motion connected: " + leapController.IsConnected);
		
		// Set up the gesture detection
		leapController.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
		Debug.Log("Circle Gesture Enabled: " + leapController.IsGestureEnabled(Gesture.GestureType.TYPE_CIRCLE));	
	}

	void Update()
	{
		CheckForGestures ();
	}

	void LateUpdate () 
	{
    	if (leapMotionOVRController == null || handController == null)
     	 return;

    	HandModel[] hands = handController.GetAllGraphicsHands();

		if (hands.Length <= 1)
		{
			GameObject[] elements;
			elements = GameObject.FindGameObjectsWithTag("Element");
			foreach(GameObject element in elements)
			{
				element.gameObject.GetComponent<SphereInteraction>().shootSign = false;
			}
		}


		if (hands.Length > 1)
		{
			Vector3 direction0 = (hands[0].GetPalmPosition() - handController.transform.position).normalized;
			Vector3 normal0 = hands[0].GetPalmNormal().normalized;
			
			Vector3 direction1 = (hands[1].GetPalmPosition() - handController.transform.position).normalized;
			Vector3 normal1 = hands[1].GetPalmNormal().normalized;
			
			if (Vector3.Dot(direction0, normal0) > direction0.sqrMagnitude * 0.5f && Vector3.Dot(direction1, normal1) > direction1.sqrMagnitude * 0.5f)
			{
				GameObject[] elementballs;
				elementballs = GameObject.FindGameObjectsWithTag("Element");
				foreach(GameObject element in elementballs)
				{
					element.gameObject.GetComponent<SphereInteraction>().shootSign = true;
				}
			}
			else
			{
				GameObject[] elements;
				elements = GameObject.FindGameObjectsWithTag("Element");
				foreach(GameObject element in elements)
				{
					element.gameObject.GetComponent<SphereInteraction>().shootSign = false;
				}
			}
		}
	}

	void CheckForGestures()
	{
		if(leapController.IsConnected)
		{
			// Check the list of gestures for a circle
			Frame current = leapController.Frame();
			
			GestureList gesturesInFrame = current.Gestures();
			foreach(Gesture gesture in gesturesInFrame)
			{
				Debug.Log("Captured a gesture: " + gesture.Type.ToString());
				if (gesture.Type == Gesture.GestureType.TYPECIRCLE) ;
				{
					Debug.Log("Circle motion found");
					
				}
			}
		}
	}
}
	