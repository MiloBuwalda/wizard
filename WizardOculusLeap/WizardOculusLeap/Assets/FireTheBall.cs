using UnityEngine;
using System.Collections;

public class FireTheBall : MonoBehaviour 
{
	public float speed = 10.0f;
	private Vector3 currentPosition;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentPosition = transform.position;
		currentPosition.z += speed * Time.deltaTime;
		transform.position = currentPosition;
	}
}
