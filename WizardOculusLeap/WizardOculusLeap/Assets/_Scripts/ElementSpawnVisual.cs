using UnityEngine;
using System.Collections;

public class ElementSpawnVisual : MonoBehaviour {
	
	public elementType element;
	public int handNumber;

	int handID;
	float speed = 5.0f;
	Vector3 startPosition;
	Vector3 endPosition;
	float startTime;
	float journeyLength;

	void Start() {
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		handID = hands [handNumber].GetLeapHand ().Id;

		startTime = Time.time;
		startPosition = transform.position;
		endPosition = hands [handNumber].GetPalmPosition();
		journeyLength = Vector3.Distance(transform.position, endPosition);
	}

	void Update() {
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		endPosition = hands [handNumber].GetPalmPosition();

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

		if (handID != hands [handNumber].GetLeapHand ().Id) {
			GameManager.instance.movementManager.summoning = false;
			Destroy(gameObject);
		}

		if (fracJourney > 1) {
			GameManager.instance.player.AddElementToPool (element, handNumber);
			GameManager.instance.movementManager.summoning = false;
			Destroy(gameObject);
		}
	}
}