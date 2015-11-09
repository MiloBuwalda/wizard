using UnityEngine;
using System.Collections;
using Leap;

public class MovementManager : MonoBehaviour {
  	public GameObject leapMotionOVRController = null;
	public GameObject cameraDirection = null;
  	public HandController handController = null;
	public bool circleMotion;

	int elementToSummon;

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

	void Update () {  
		if (leapMotionOVRController == null || handController == null)
     	 return;

    	HandModel[] hands = handController.GetAllPhysicsHands();

		if (hands.Length > 0) {
			if(GameManager.instance.player.elementPool.Count < 2){
				// Check the list of gestures for a circle
				Frame current = leapController.Frame();

				GestureList gesturesInFrame = current.Gestures();

				foreach(Gesture gesture in gesturesInFrame)	{
					if (gesture.Type == Gesture.GestureType.TYPECIRCLE)	{ 
						if (gesture.DurationSeconds > 0.5f){
							if(GameManager.instance.player.handLeft != 2 && gesture.Hands[0].Id == hands[GameManager.instance.player.handLeft].GetLeapHand().Id){ 
								Debug.Log("Left circle found");
								if (!GameManager.instance.player.handLeftSlot){
									GameManager.instance.elementSpawner.ElementToSpawn(hands[0].GetPalmPosition(), 0);
									GameManager.instance.player.handLeftSlot = true;
								}
							}
							else if (GameManager.instance.player.handRight != 2 && gesture.Hands[0].Id == hands[GameManager.instance.player.handRight].GetLeapHand().Id){
								Debug.Log("Right circle found");
								if (!GameManager.instance.player.handRightSlot){
									GameManager.instance.elementSpawner.ElementToSpawn(hands[0].GetPalmPosition(), 0);
									GameManager.instance.player.handRightSlot = true;
								}
							}
							if(hands.Length > 1){
								if(GameManager.instance.player.handLeft != 2 && gesture.Hands[1].Id == hands[GameManager.instance.player.handLeft].GetLeapHand().Id){ 
									Debug.Log("Left circle found");
									if (!GameManager.instance.player.handLeftSlot){
										GameManager.instance.elementSpawner.ElementToSpawn(hands[1].GetPalmPosition(), 1);
										GameManager.instance.player.handLeftSlot = true;
									}
								}
								else if(GameManager.instance.player.handRight != 2 && gesture.Hands[1].Id == hands[GameManager.instance.player.handRight].GetLeapHand().Id){
									Debug.Log("Right circle found"); 
									if (!GameManager.instance.player.handRightSlot){
										GameManager.instance.elementSpawner.ElementToSpawn(hands[1].GetPalmPosition(), 1);
										GameManager.instance.player.handRightSlot = true;
									}
								}
							}
						}
					} 
				}
			}
			if (hands.Length > 1)	{
				Vector3 direction0 = (hands[0].GetPalmPosition() - handController.transform.position).normalized;
				Vector3 normal0 = hands[0].GetPalmNormal().normalized;
				
				Vector3 direction1 = (hands[1].GetPalmPosition() - handController.transform.position).normalized;
				Vector3 normal1 = hands[1].GetPalmNormal().normalized;
				
				if (Vector3.Dot(direction0, normal0) > direction0.sqrMagnitude * 0.5f && Vector3.Dot(direction1, normal1) > direction1.sqrMagnitude * 0.5f)	{
					if (GameObject.Find("palm").GetComponent<HandInteraction>()._magnitude > 1)	{
						GameManager.instance.player.ExecuteSpell();
					}
				}
			}
		}
	}
}
	