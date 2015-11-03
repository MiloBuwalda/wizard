using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public float _magnitude;
	public elementType element;
	public bool notInsideElement;
	public bool shootSign;
	Vector3 fieldCenter;
	Vector3 _position;
	Vector3 previousLocation;
	float startTime;
	float journeyLength;
	float speed = 1f;
	elementType otherElement;
//	bool inPosition;
	bool shot;

	void Start(){
		_position = transform.position;
	}

	void Update(){
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;

		if (shootSign) {
			shot = true;
		} 
//		if (notInsideElement && !shot && _position != fieldCenter) 
//		{
//			float distCovered = (Time.time - startTime) * speed;
//			float fracJourney = distCovered / journeyLength;
//			transform.position = Vector3.Lerp(_position, fieldCenter, fracJourney);
//		}
		if (shot) {
			_position = transform.position;
			//_position.x += Mathf.Sin(Time.time) * Time.deltaTime;
			_position.z += 3f * Time.deltaTime;
			transform.position = _position;
		}
	}

//	void OnTriggerEnter (Collider other)
//	{
//		if (other.gameObject.tag == "Field") 
//		{
//			fieldCenter = other.gameObject.transform.position;
//		}
//	}
//
//	void OnTriggerStay (Collider other)
//	{
//		if (other.gameObject.tag == "Field" && notInsideElement) 
//		{
//			if(!inPosition)
//			{
//				inPosition = true;
//				_position = transform.position;
//				startTime = Time.time;
//				journeyLength = Vector3.Distance(_position, fieldCenter);
//			}
//		}
//	}
//	void OnTriggerExit (Collider other)
//	{
//		if (other.gameObject.tag == "Field") 
//		{
//			if(inPosition)
//			{
//				inPosition = false;
//			}
//		}
//	}
}