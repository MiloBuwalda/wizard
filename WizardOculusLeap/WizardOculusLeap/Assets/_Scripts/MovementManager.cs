using UnityEngine;
using System.Collections;
using Leap;

public class MovementManager : MonoBehaviour {
  	public GameObject leapMotionOVRController = null;
	public GameObject cameraDirection = null;
  	public HandController handController = null;
	public bool circleMotion;
	public bool insideShield;
	public bool insideShieldLeft;
	public bool insideShieldRight;
	public bool summoning;

	Vector3 directionLeft;
	Vector3 normalLeft; 
	Vector3 directionRight;
	Vector3 normalRight;

	int elementToSummon;

	float timerShield; //Delay shield summon
	float timerSummon; //Delay summons

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

		//Get all physic hand models
    	HandModel[] hands = handController.GetAllPhysicsHands();
/////////////////////////////////////////////////////////////// SUMMON WITH GRAB ///////////////////////////////////////////////////////////////
		if (hands.Length > 0) {
			if (GameManager.instance.player.elementPool.Count < 2) {
				if (GameManager.instance.player.handLeft != 2 && hands[GameManager.instance.player.handLeft].GetLeapHand().GrabStrength > 0.8f){
					if (!GameManager.instance.player.handLeftSlot && !summoning && !insideShieldLeft && timerSummon < Time.time) {
						GameManager.instance.elementSpawner.ElementToSpawn (hands [GameManager.instance.player.handLeft].GetPalmPosition (), GameManager.instance.player.handLeft);
						GameManager.instance.player.handLeftSlot = true;
						timerShield = Time.time + 10f * Time.deltaTime;
						timerSummon = Time.time + 40f * Time.deltaTime;
						summoning = true;
					} else if (!GameManager.instance.player.handLeftSlot && !summoning && insideShieldLeft && timerSummon < Time.time){
						GameManager.instance.player.AddElementToPool(GameManager.instance.player.triggerShieldElementTypeLeft, GameManager.instance.player.handLeft);
						GameManager.instance.player.EmptyShieldPool();
						GameManager.instance.player.handLeftSlot = true;
						timerShield = Time.time + 10f * Time.deltaTime;
						timerSummon = Time.time + 40f * Time.deltaTime;
					}
				}
				if (GameManager.instance.player.handRight != 2 && hands[GameManager.instance.player.handRight].GetLeapHand().GrabStrength > 0.8f){
					if (!GameManager.instance.player.handRightSlot && !summoning && !insideShieldRight && timerSummon < Time.time) {
						GameManager.instance.elementSpawner.ElementToSpawn (hands [GameManager.instance.player.handRight].GetPalmPosition (), GameManager.instance.player.handRight);
						GameManager.instance.player.handRightSlot = true;
						timerShield = Time.time + 10f * Time.deltaTime;
						timerSummon = Time.time + 40f * Time.deltaTime;
						summoning = true;
					} else if (!GameManager.instance.player.handRightSlot && !summoning && insideShieldRight && timerSummon < Time.time) {
						GameManager.instance.player.AddElementToPool(GameManager.instance.player.triggerShieldElementTypeRight, GameManager.instance.player.handRight);
						GameManager.instance.player.EmptyShieldPool();
						GameManager.instance.player.handRightSlot = true;
						timerShield = Time.time + 10f * Time.deltaTime;
						timerSummon = Time.time + 40f * Time.deltaTime;
					}
				}
/////////////////////////////////////////////////////////////// SUMMON WITH GRAB /////////////////////////////////////////////////////////////// 

/////////////////////////////////////////////////////////////// SUMMON WITH CIRCLE GESTURE ///////////////////////////////////////////////////////////////
//				// Check the list of gestures for a circle
//				Frame current = leapController.Frame ();
//
//				GestureList gesturesInFrame = current.Gestures ();
//
//				foreach (Gesture gesture in gesturesInFrame) {
//					//Circle gesture om element te summonen
//					if (gesture.Type == Gesture.GestureType.TYPECIRCLE) { 
//						if (gesture.DurationSeconds > 0.5f) { //Hoelang je een cirkel beweging minimaal moet maken
//							//Linkerhand zonder rechterhand in de scene
//							if (GameManager.instance.player.handLeft != 2 && gesture.Hands [0].Id == hands [GameManager.instance.player.handLeft].GetLeapHand ().Id) { 
//								Debug.Log ("Left circle found 1");
//								if (!GameManager.instance.player.handLeftSlot && timerSummon < Time.time) {
//									GameManager.instance.elementSpawner.ElementToSpawn (hands [GameManager.instance.player.handLeft].GetPalmPosition (), GameManager.instance.player.handLeft);
//									GameManager.instance.player.handLeftSlot = true;
//									timerShield = Time.time + 10f * Time.deltaTime;
//									timerSummon = Time.time + 40f * Time.deltaTime;
//								}
//							//Rechterhand zonder linkerhand in de scene
//							} else if (GameManager.instance.player.handRight != 2 && gesture.Hands [0].Id == hands [GameManager.instance.player.handRight].GetLeapHand ().Id) {
//								Debug.Log ("Right circle found 1");
//								if (!GameManager.instance.player.handRightSlot && timerSummon < Time.time) {
//									GameManager.instance.elementSpawner.ElementToSpawn (hands [GameManager.instance.player.handRight].GetPalmPosition (), GameManager.instance.player.handRight);
//									GameManager.instance.player.handRightSlot = true;
//									timerShield = Time.time + 10f * Time.deltaTime;
//									timerSummon = Time.time + 40f * Time.deltaTime;
//								}
//							}
//							if (hands.Length > 1) {
//								//Linkerhand met rechterhand in de scene
//								if (GameManager.instance.player.handLeft != 2 && gesture.Hands [1].Id == hands [GameManager.instance.player.handLeft].GetLeapHand ().Id) { 
//									Debug.Log ("Left circle found 2");
//									if (!GameManager.instance.player.handLeftSlot && timerSummon < Time.time) {
//										GameManager.instance.elementSpawner.ElementToSpawn (hands [GameManager.instance.player.handLeft].GetPalmPosition (), GameManager.instance.player.handLeft);
//										GameManager.instance.player.handLeftSlot = true;
//										timerShield = Time.time + 10f * Time.deltaTime;
//										timerSummon = Time.time + 40f * Time.deltaTime;
//									}
//									//Rechterhand met linkerhand in de scene
//								} else if (GameManager.instance.player.handRight != 2 && gesture.Hands [1].Id == hands [GameManager.instance.player.handRight].GetLeapHand ().Id) {
//									Debug.Log ("Right circle found 2"); 
//									if (!GameManager.instance.player.handRightSlot && timerSummon < Time.time) {
//										GameManager.instance.elementSpawner.ElementToSpawn (hands [GameManager.instance.player.handRight].GetPalmPosition (), GameManager.instance.player.handRight);
//										GameManager.instance.player.handRightSlot = true;
//										timerShield = Time.time + 10f * Time.deltaTime;
//										timerSummon = Time.time + 40f * Time.deltaTime;
//									}
//								}
//							}
//						}
//					} 
//				}
/////////////////////////////////////////////////////////////// SUMMON WITH CIRCLE GESTURE ///////////////////////////////////////////////////////////////
			}
			// Handen vooruit bewegen om spell execute te doen
			if (hands.Length > 1) {	
				Vector3 directionLeft = (hands [GameManager.instance.player.handLeft].GetPalmPosition () - handController.transform.position).normalized;
				Vector3 normalLeft = hands [GameManager.instance.player.handLeft].GetPalmNormal ().normalized;
				
				Vector3 directionRight = (hands [GameManager.instance.player.handRight].GetPalmPosition () - handController.transform.position).normalized;
				Vector3 normalRight = hands [GameManager.instance.player.handRight].GetPalmNormal ().normalized;

				if (Vector3.Dot (directionLeft, normalLeft) > directionLeft.sqrMagnitude * 0.5f && Vector3.Dot (directionRight, normalRight) > directionRight.sqrMagnitude * 0.5f) {
//					if (GameObject.Find ("palm").GetComponent<HandInteraction> ()._magnitude > 1) {
					if (hands[0].gameObject.GetComponentInChildren<HandInteraction>()._magnitude > 1 && hands[1].gameObject.GetComponentInChildren<HandInteraction>()._magnitude > 1){
						//if collision with shield
						if (insideShield){ 
							GameManager.instance.player.ExecuteSpell ();
						}
					}
				}
			}
			//Linkerhand schild wanneer hand naar voren
			if(GameManager.instance.player.handLeftSlot){
				Vector3 directionLeft = (hands [GameManager.instance.player.handLeft].GetPalmPosition () - handController.transform.position).normalized;
				Vector3 normalLeft = hands [GameManager.instance.player.handLeft].GetPalmNormal ().normalized;

				if (Vector3.Dot (directionLeft, normalLeft) > directionLeft.sqrMagnitude * 0.70f && Vector3.Dot (directionLeft, normalLeft) < directionLeft.sqrMagnitude * 1.1f && hands [GameManager.instance.player.handLeft].GetLeapHand().GrabStrength < 0.8f && timerShield < Time.time) {
					if(GameManager.instance.player.shieldPool.Count > 0){
						GameManager.instance.player.EmptyShieldPool();
						GameManager.instance.player.ExecuteShield(GameManager.instance.player.leftElement);
					} else {
						GameManager.instance.player.ExecuteShield(GameManager.instance.player.leftElement);
					}
				}
			}
			//Rechterhand schild wanneer hand naar voren 
			if(GameManager.instance.player.handRightSlot){
				Vector3 directionRight = (hands [GameManager.instance.player.handRight].GetPalmPosition () - handController.transform.position).normalized;
				Vector3 normalRight = hands [GameManager.instance.player.handRight].GetPalmNormal ().normalized;

				if (Vector3.Dot (directionRight, normalRight) > directionRight.sqrMagnitude * 0.70f && Vector3.Dot (directionRight, normalRight) < directionRight.sqrMagnitude * 1.1f && hands [GameManager.instance.player.handRight].GetLeapHand().GrabStrength < 0.8f && timerShield < Time.time) {
					if(GameManager.instance.player.shieldPool.Count > 0){
						GameManager.instance.player.EmptyShieldPool();
						GameManager.instance.player.ExecuteShield(GameManager.instance.player.rightElement);
					} else {
						GameManager.instance.player.ExecuteShield(GameManager.instance.player.rightElement);
					}
				}
			}
		}
	}
}
	