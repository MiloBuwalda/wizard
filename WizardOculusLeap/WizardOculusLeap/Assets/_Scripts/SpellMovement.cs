using UnityEngine;
using System.Collections;

public class SpellMovement : MonoBehaviour {

	Vector3 _position;

	void Update () {
		_position = transform.position;
		_position.z += 50f * Time.deltaTime;
		transform.position = _position;
	}
}
