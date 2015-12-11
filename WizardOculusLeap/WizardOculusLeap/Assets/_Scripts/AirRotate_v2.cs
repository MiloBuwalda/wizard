using UnityEngine;
using System.Collections;

public class AirRotate_v2 : MonoBehaviour 
{
	#region Variables
	[Range(0.1f,100.0f)]
	public float rotationSpeed = 1.0f;

	public Vector3 rotationDirection;
	public bool centreOfGravity = false;
	#endregion

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (!centreOfGravity) {
			transform.localPosition = new Vector3 (
				Mathf.Sin (rotationDirection.x * rotationSpeed * Time.time) * 0.5f,
				Mathf.Sin (rotationDirection.y * rotationSpeed * Time.time) * 0.5f,
				Mathf.Sin (rotationDirection.z * rotationSpeed * Time.time) * 0.5f
			);
		} else {
			transform.Rotate(new Vector3 (
				rotationDirection.x * rotationSpeed * Time.deltaTime * Random.Range(0.1f,1.9f),
				rotationDirection.y * rotationSpeed * Time.deltaTime * Random.Range(0.1f,1.9f),
				rotationDirection.z * rotationSpeed * Time.deltaTime * Random.Range(0.1f,1.9f)),
			    rotationSpeed * Time.deltaTime);
		}

	}
}
