using UnityEngine;
using System.Collections;

public class SpellCollision : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.tag == "Spell") {

		}
		if (other.tag == "Shield") {

		}
	}
}
