using UnityEngine;
using System.Collections;


public class SphereInteraction : MonoBehaviour {
	public int element;
	public HandController handController = null;
	public bool notGrabbed;
	public bool shot;

	void Update()
	{
		if (shot == true)
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1 * Time.deltaTime);
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