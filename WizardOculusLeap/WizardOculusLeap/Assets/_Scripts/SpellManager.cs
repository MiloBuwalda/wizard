using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager {
	//public List<elementType> elements;
	public GameObject instance;
	public SpellMovement spellMovement;
	public SpellCollision spellCollision;

	public int id;

	public void Setup (){ //(List<ElementManager> list) {
		//elements = new List<elementType> ();
		//foreach(ElementManager e in list){
		//	elements.Add(e.elementType);
		//}
		//Choose Texture
		spellMovement = instance.GetComponent<SpellMovement> ();
		spellCollision = instance.GetComponent<SpellCollision> ();
		spellCollision.id = id;
	}

	public void DestroyMe()
	{
		if (instance != null) {
			PhotonNetwork.Destroy(instance);
		}
		
		Debug.Log ("Destroy me: ");
	}
}
