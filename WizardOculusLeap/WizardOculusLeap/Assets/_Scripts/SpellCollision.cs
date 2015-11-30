using UnityEngine;
using System.Collections;

public class SpellCollision : MonoBehaviour {

	public int id;
	
	public elementType elementType;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Spell") {
			elementType otherElement = other.GetComponent<SpellCollision>().elementType; 
		}
		if (other.tag == "Shield") {
			elementType otherElement = other.GetComponent<ShieldElement>().elementType;
		}
	}

	void CreateString (elementType Element, bool isSpell){
		string e = Element.ToString ();
		if (e.Contains ("Fire")) {
			e = e.Replace ("Fire", "F");
		}
		if (e.Contains ("Air")) {
			e = e.Replace ("Air", "A"); 
		}
		if (e.Contains ("Water")) {
			e = e.Replace ("Water", "W");
		}
		if (e.Contains ("Earth")) {
			e = e.Replace ("Earth","E");
		}
		switch (Element) {
		case elementType.Air:
			e = e.Insert(1, "0");
			break;
		case elementType.Earth:
			e = e.Insert(1, "0");
			break;
		case elementType.Fire:
			e = e.Insert(1, "0");
			break;
		case elementType.Water:
			e = e.Insert(1, "0");
			break;
		default:
			break;
		}
		if (isSpell) {
			e = e.Insert(2,"_M");
		} else {
			e = e.Insert(2,"_S");
		}
	}
}
