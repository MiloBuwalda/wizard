using UnityEngine;
using System.Collections;

public class SpellMovement : MonoBehaviour {

	Vector3 _position;

	void Update () {
		_position = transform.position;
		_position.z += 3f * Time.deltaTime;
		transform.position = _position;
	}
}
