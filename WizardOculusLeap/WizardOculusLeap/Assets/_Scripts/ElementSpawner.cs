using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementSpawner : MonoBehaviour {
	public static ElementSpawner instance;
	public Dictionary<string, GameObject> elementBook;
	int basisNumber;

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
			element.elementType = t;
			element.Setup ();
			element.elementMovement.handNumber = handNumber;
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

	public ElementManager CombineElements(List<ElementManager> list){
		if (list.Count > 0) {
			ElementManager element = new ElementManager();
			float speed0 = list[0].instance.GetComponent<ElementMovement>()._magnitude;
			float speed1 = list[1].instance.GetComponent<ElementMovement>()._magnitude;
			if(speed0 < speed1){
				basisNumber = 0; 
			}else{
				basisNumber = 1;
			}
			ElementManager basis = list[basisNumber];
			GameObject g;
			if (elementBook.TryGetValue (basis.elementType.ToString(), out g)){
				element.instance = (GameObject)Instantiate(g, basis.instance.transform.position, transform.rotation);
				element.Setup();
			} 
			
			return element;
		} else {
			//buzz sound
			return null;
		}
	}
}
