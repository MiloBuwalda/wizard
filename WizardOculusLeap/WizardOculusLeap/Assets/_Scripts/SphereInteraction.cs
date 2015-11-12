using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public float _magnitude;
	//public elementType element;
	//public bool notInsideElement;
	//public bool shootSign;
	//Vector3 fieldCenter;
	//Vector3 _position;
	Vector3 previousLocation;
	//float startTime;
	//float journeyLength;
	//float speed = 1f;
	//elementType otherElement;
	//bool shot;

	void Start(){
		//_position = transform.position;
	}

	void Update(){
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;
	}
}