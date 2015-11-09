using UnityEngine;
using System.Collections;

public class ElementMovement : MonoBehaviour {

	public float _magnitude;
	Vector3 previousLocation;

	void Update(){
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;
	}
}
