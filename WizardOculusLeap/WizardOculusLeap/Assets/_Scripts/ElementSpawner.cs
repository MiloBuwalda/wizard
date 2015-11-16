using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementSpawner : MonoBehaviour {
	public static ElementSpawner instance;
	public Dictionary<string, GameObject> elementBook;
	int basisNumber;
	int otherNumber;

	void Awake(){
		//singleton
		instance = this;
	}

	void Start(){
		//Add elements to dictionary
		elementBook = new Dictionary<string, GameObject> ();
		GameObject[] elements = Resources.LoadAll<GameObject> ("Elements");
		foreach(GameObject g in elements){
			elementBook.Add(g.name,g);
		}
	}

	//Create an element
	public ElementManager GetElementOfType(elementType t, int handNumber){
		ElementManager element = null;
		GameObject g;
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (elementBook.TryGetValue (t.ToString (), out g)) {
			element = new ElementManager ();
			element.instance = (GameObject)Instantiate (g, hands[handNumber].GetPalmPosition(), transform.rotation);
			element.elementType = t;
			element.Setup ();
			element.elementMovement.handNumber = handNumber;
		} else {
			print ("Could not find element: " + t.ToString());
		}

		return element;
	}

	//Find out which element to spawn, based on its location
	public void ElementToSpawn (Vector3 location, int handNumber){
		float locationSignX = Mathf.Sign (location.x);

		if (locationSignX == -1 && location.y > 58.7) { //links boven
			GameManager.instance.player.AddElementToPool (elementType.Fire, handNumber);
		}
		else if (locationSignX == 1 && location.y > 58.7) { //rechts boven
			GameManager.instance.player.AddElementToPool (elementType.Air, handNumber);
		}
		else if (locationSignX == 1 && location.y < 58.7) { //rechts onder
			GameManager.instance.player.AddElementToPool (elementType.Water, handNumber);
		}
		else if (locationSignX == -1 && location.y < 58.7) { //links onder
			GameManager.instance.player.AddElementToPool (elementType.Earth, handNumber);
		}
	}

	public ElementManager CombineElements(List<ElementManager> list){
		ElementManager element = null;
		GameObject g;
		int handNumber;
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		float speed0 = list[0].instance.GetComponent<ElementMovement>()._magnitude;
		float speed1 = list[1].instance.GetComponent<ElementMovement>()._magnitude;

		if(speed0 < speed1){
			basisNumber = 0; 
			otherNumber = 1;
			handNumber = list[0].instance.GetComponent<ElementMovement>().handNumber;
		}else{
			basisNumber = 1;
			otherNumber = 0;
			handNumber = list[1].instance.GetComponent<ElementMovement>().handNumber;
		}

		ElementManager basis = list[basisNumber];
		string elementTypeCombined = basis.elementType.ToString() + list[otherNumber].elementType.ToString();
		
		if (elementBook.TryGetValue (basis.elementType.ToString(), out g)) {
			element = new ElementManager ();
			element.instance = (GameObject)Instantiate (g, hands[handNumber].GetPalmPosition(), transform.rotation);
			element.elementType = basis.elementType;
			element.Setup ();
			element.elementMovement.handNumber = handNumber;
		} else {
			print ("Could not find element: " + basis.elementType.ToString());
		}
		return element;
	}
}
