using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public int element;

	public HandController handController = null;

	float horizontalSpeed = 1.5f;
	float verticalSpeed = 1;
	float amplitudeh = 0.0003f;
	float amplitudev = 0.001f;

	Vector3 floatingPosition;

	void Awake()
	{
		amplitudeh = Random.Range(0.00025f, 0.00035f);
		amplitudev = Random.Range(0.0009f, 0.0011f);
	}

	void Start()
	{
		floatingPosition = transform.localPosition;
	}

	void FixedUpdate()
	{
		floatingPosition.x += Mathf.Sin (Time.realtimeSinceStartup + horizontalSpeed) * amplitudeh;
		floatingPosition.y += Mathf.Sin (Time.realtimeSinceStartup + verticalSpeed) * amplitudev;
		transform.localPosition = floatingPosition;
	}
	
	void OnTriggerStay(Collider other)
	{
		HandModel[] hands = handController.GetAllGraphicsHands();

		//Debug.Log (hands [GameObject.Find ("MovementManager").GetComponent<MovementManager> ().rightHandNumber].GetLeapHand ().GrabStrength);

		//Debug.Log (hands [0].GetLeapHand ().GrabStrength);

		if (other.gameObject.name == "palm" && hands[GameObject.Find("MovementManager").GetComponent<MovementManager>().rightHandNumber].GetLeapHand().GrabStrength == 1)
		{
			GameObject.Find("MovementManager").GetComponent<MovementManager>().element = element;
			//Debug.Log ("Touching" + element);
		}
	}

//	void OnTriggerExit (Collider other)
//	{
//		if (other.gameObject.name == "palm") 
//		{
//			Debug.Log ("Noooo" + element);
//		}
//	}
}