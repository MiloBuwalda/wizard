using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementSpawner : MonoBehaviour {
	public static ElementSpawner instance;
	public Dictionary<string, GameObject> elementBook;

	void Awake(){
		//singleton
		instance = this;
	}

	void Start(){
		elementBook = new Dictionary<string, GameObject> ();
		GameObject[] elements = Resources.LoadAll<GameObject> ("Elements");
		foreach(GameObject g in elements){
			elementBook.Add(g.name,g);
		}
	}

	public ElementManager GetElementOfType(elementType t, int handNumber){
		ElementManager element = null;
		GameObject g;
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (elementBook.TryGetValue (t.ToString (), out g)) {
			element = new ElementManager ();
			element.instance = (GameObject)Instantiate (g, hands[handNumber].GetPalmPosition(), transform.rotation);
			//element.instance = (GameObject)Instantiate (g, transform.position, transform.rotation);
			element.elementType = t;
			//element.instance.transform.parent = hands[handNumber].transform;
			element.Setup ();
		} else {
			print ("Could not find element: " + t.ToString());
		}

		return element;
	}

	public void ElementToSpawn (Vector3 location, int handNumber){
		float locationSignX = Mathf.Sign (location.x);

		if (locationSignX == -1 && location.y > 58.7) {
			GameManager.instance.player.AddElementToPool (elementType.Fire, handNumber);
		}
		else if (locationSignX == 1 && location.y > 58.7) {
			GameManager.instance.player.AddElementToPool (elementType.Air, handNumber);
		}
		else if (locationSignX == 1 && location.y < 58.7) {
			GameManager.instance.player.AddElementToPool (elementType.Water, handNumber);
		}
		else if (locationSignX == -1 && location.y < 58.7) {
			GameManager.instance.player.AddElementToPool (elementType.Earth, handNumber);
		}
	}
}
