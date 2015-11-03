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

	void Start()
	{
		shieldBook = new Dictionary<string, GameObject> ();
		GameObject[] shields = Resources.LoadAll<GameObject> ("Shields");
		foreach(GameObject g in shields)
		{
			shieldBook.Add(g.name,g);
		}
	}

	public ShieldManager CreateShield(List<ElementManager> list){
		if (list.Count > 0) {
			ShieldManager shield = new ShieldManager();
			ElementManager basis = list[0];
			GameObject g;
			if (shieldBook.TryGetValue (basis.elementType.ToString() + "Shield", out g))
			{
				shield.instance = (GameObject)Instantiate(g, transform.position, transform.rotation);
				shield.Setup();
			}

			return shield;
		} else {
			//buzz sound
			return null;
		}

	}
}
