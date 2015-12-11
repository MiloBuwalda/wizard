using UnityEngine;
using System.Collections;

public class ShieldRotator : MonoBehaviour 
{
	#region Variables
	[Range(0.1f,1000.0f)]
	public float rotationSpeed = 1.0f;
	#endregion

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3(0,0,1), rotationSpeed * Time.deltaTime);
	}
}
