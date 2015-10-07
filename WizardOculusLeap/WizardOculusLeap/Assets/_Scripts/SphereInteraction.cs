using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public int element;
	public HandController handController = null;
	public bool notGrabbed;
	public bool shot;
	Vector3 _position;

	void Start()
	{
		_position = transform.position;
	}

	void Update()
	{
		if (shot == true)
		{
			_position = transform.position;
			//_position.x += Mathf.Sin(Time.time) * Time.deltaTime;
			_position.z += 1f * Time.deltaTime;
			transform.position = _position;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Field" && notGrabbed == true) 
		{
			if(shot == false)
			{
				shot = true;
				Debug.Log("Shoot");
			}
		}
	}
}