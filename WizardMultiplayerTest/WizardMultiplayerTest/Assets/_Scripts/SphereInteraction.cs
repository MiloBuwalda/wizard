using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public int element;
	public HandController handController = null;
	public bool notGrabbed;
	public bool shootSign;
	bool inPosition;
	bool shot;
	Vector3 fieldCenter;
	Vector3 _position;
	float startTime;
	float journeyLength;
	float speed = 1f;

	void Start()
	{
		_position = transform.position;
	}

	void Update()
	{
		if (inPosition == true && shootSign == true) 
		{
			shot = true;
		} 
		else if (inPosition == true && notGrabbed == true && shot == false && _position != fieldCenter) 
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(_position, fieldCenter, fracJourney);
		}
		if (shot == true) 
		{
			_position = transform.position;
			//_position.x += Mathf.Sin(Time.time) * Time.deltaTime;
			_position.z += 3f * Time.deltaTime;
			transform.position = _position;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Field") 
		{
			fieldCenter = other.gameObject.transform.position;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Field" && notGrabbed == true) 
		{
			if(inPosition == false)
			{
				inPosition = true;
				_position = transform.position;
				startTime = Time.time;
				journeyLength = Vector3.Distance(_position, fieldCenter);
			}
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Field") 
		{
			if(inPosition == true)
			{
				inPosition = false;
			}
		}
	}
}