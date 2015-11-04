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

	public ElementManager GetElementOfType(elementType t){
		ElementManager element = null;
		GameObject g;
		Vector3 _position;

		_position = GameObject.Find (t.ToString() + "Spawn").transform.position;

		if (elementBook.TryGetValue (t.ToString (), out g)) {
			element = new ElementManager ();
			element.instance = (GameObject)Instantiate (g, _position, transform.rotation);
			element.elementType = t;
			element.Setup ();
		} else {
			print ("Could not find element: " + t.ToString());
		}

		return element;
	}
}
