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
}
