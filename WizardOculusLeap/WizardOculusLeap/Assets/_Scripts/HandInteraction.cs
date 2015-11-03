using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour 
{	
	public float _magnitude;
	GameObject elementSlot = null;
	int handNumber = 0;
	bool occupied;
	bool grabbed;
	Vector3 previousLocation;
	string spawnName;

	void Update()
	{
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;
	}

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

		if (GameManager.instance.player.elementPool.Count < 2)
		{
			spawnName = other.gameObject.name;
			switch (spawnName)
			{
			case "FireSpawn": 
				GameManager.instance.player.AddElementToPool(elementType.Fire);
				break;
			case "AirSpawn":
				GameManager.instance.player.AddElementToPool(elementType.Air);
				break;
			case "WaterSpawn":
				GameManager.instance.player.AddElementToPool(elementType.Water);
				break;
			case "EarthSpawn":
				GameManager.instance.player.AddElementToPool(elementType.Earth);
				break;
			default:
				Debug.Log("Spawn does not exist");
				break;
			}
//			if (other.gameObject.name == "FireSpawn") 
//			{
//				GameManager.instance.player.AddElementToPool(elementType.Fire);
//			}
//			if (other.gameObject.name == "AirSpawn") 
//			{
//				GameManager.instance.player.AddElementToPool(elementType.Air);
//			}
//			if (other.gameObject.name == "WaterSpawn") 
//			{
//				GameManager.instance.player.AddElementToPool(elementType.Water);
//			}
//			if (other.gameObject.name == "EarthSpawn") 
//			{
//				GameManager.instance.player.AddElementToPool(elementType.Earth);
//			}
		}

	}

	void OnTriggerStay(Collider other)
	{
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (hands.Length > 0) {
			if (other.gameObject == elementSlot) {
				if (hands [0].gameObject.transform == gameObject.transform.parent) {
					handNumber = 0;
				} else if (hands [1].gameObject.transform == gameObject.transform.parent) {
					handNumber = 1;
				}
			}
			if (other.gameObject == elementSlot && hands [handNumber].GetLeapHand ().GrabStrength > 0.8 && !grabbed) {
				grabbed = true;
			} else if (other.gameObject == elementSlot && hands [handNumber].GetLeapHand ().GrabStrength < 0.8 && grabbed) {
				grabbed = false;
			} else {
				grabbed = false;
			}
		} else {
			if(grabbed){
				grabbed = false;
			}
		}

		if(grabbed)
		{
			other.transform.position = transform.position;
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
