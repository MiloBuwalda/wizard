using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager {
	public List<elementType> elements;
	public GameObject instance;

	public void Setup (List<ElementManager> list) {
		elements = new List<elementType> ();
		foreach(ElementManager e in list){
			elements.Add(e.elementType);
		}
		//Choose Texture


	}
}
