using UnityEngine;
using System.Collections;

public class ElementSpawnVisual : MonoBehaviour {
	
	public elementType element;
	public int handNumber;

	int handID;
	float speed = 10f;
	float size = 4.1f;
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

		transform.localScale = new Vector3(size, size, size);
	}

	void Update() {
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (hands.Length > 0) {
			endPosition = hands [handNumber].GetPalmPosition ();
		}

		float distCovered = (Time.time -  startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

		transform.localScale = new Vector3(size - fracJourney * 4, size - fracJourney * 4, size - fracJourney * 4);

		if (hands.Length == 0) {
			GameManager.instance.movementManager.summoning = false;
			Destroy (gameObject);
		} else if (hands.Length == 1) {
			if (handID != hands [0].GetLeapHand ().Id) {
				GameManager.instance.movementManager.summoning = false;
				Destroy (gameObject);
			}
		} else {
			if (handID != hands [handNumber].GetLeapHand ().Id) {
				GameManager.instance.movementManager.summoning = false;
				Destroy (gameObject);
			}
		}

		if (fracJourney > 1) {
			GameManager.instance.player.AddElementToPool (element, handNumber);
			GameManager.instance.movementManager.summoning = false;
			Destroy(gameObject);
		}
	}
}