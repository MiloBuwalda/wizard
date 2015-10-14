using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public GameObject newElement = null;
	public float _magnitude;
	public int element;
	public bool notInsideElement;
	public bool shootSign;
	Vector3 fieldCenter;
	Vector3 _position;
	Vector3 previousLocation;
	float startTime;
	float journeyLength;
	float speed = 1f;
	int otherElement;
	bool inPosition;
	bool shot;

	void Start()
	{
		_position = transform.position;
	}

	void Update()
	{
		_magnitude = ((transform.position - previousLocation).magnitude) / Time.deltaTime;
		previousLocation = transform.position;

		if (inPosition == true && shootSign == true) 
		{
			shot = true;
		} 
		if (inPosition == true && notInsideElement == true && shot == false && _position != fieldCenter) 
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
		if (other.gameObject.tag == "Element") 
		{
			otherElement = other.GetComponent<SphereInteraction>().element;

			if (_magnitude < other.GetComponent<SphereInteraction>()._magnitude)
			{
				Debug.Log ("Create new element with base: " + element);
				Instantiate(newElement, transform.position, Quaternion.identity);
				Destroy (gameObject);
				Destroy(other.gameObject);
			}
			//spawn nieuw element at lower magnitude location
		}
		if (other.gameObject.tag == "Field") 
		{
			fieldCenter = other.gameObject.transform.position;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Field" && notInsideElement == true) 
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