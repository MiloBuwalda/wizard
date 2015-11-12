using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldSpawner : MonoBehaviour {
	public static ShieldSpawner instance;
	public Dictionary<string, GameObject> shieldBook;

	void Awake () {
		//singleton
		instance = this;
	}

	void Start(){
		shieldBook = new Dictionary<string, GameObject> ();
		GameObject[] shields = Resources.LoadAll<GameObject> ("Shields"); //Load all shield prefabs from resource folder into dictionary
		foreach(GameObject g in shields)
		{
			shieldBook.Add(g.name,g);
		}
	}

	//Create a shield with element pool from player when player demands it
	public ShieldManager CreateShield(ElementManager element){
		if (element != null) {
			ShieldManager shield = new ShieldManager();
			ElementManager basis = element;
			GameObject g;
			if (shieldBook.TryGetValue (basis.elementType.ToString() + "Shield", out g)){
				shield.instance = (GameObject)Instantiate(g, basis.instance.transform.position, transform.rotation);
				shield.Setup();
			} 

			return shield;
		} else {
			//buzz sound
			return null;
		}
	}
}
