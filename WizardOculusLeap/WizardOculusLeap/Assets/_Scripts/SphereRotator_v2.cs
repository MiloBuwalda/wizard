using UnityEngine;
using System.Collections;

public class SphereRotator_v2 : MonoBehaviour 
{
	private float currentRotationSpeed = 2.0f;
	public float speedupRate = 15.0f;
	private Vector3 rotationDirection;
	public float maxRotationSpeed = 100.0f;
	public float minRotationSpeed = 50.0f;
	public float inaccuracy = 1.0f;
	private bool isSpeedingUp = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentRotationSpeed < minRotationSpeed) //change the rotationdirection and speed up
		{
			currentRotationSpeed = minRotationSpeed;
			isSpeedingUp = true;
			rotationDirection = Randomize(rotationDirection, inaccuracy);
		}
		else if (currentRotationSpeed > maxRotationSpeed) //start slowing down
		{
			currentRotationSpeed = maxRotationSpeed;
			isSpeedingUp = false;
		} 
		else //continue turning
		{
			if(isSpeedingUp)
			{
				currentRotationSpeed += speedupRate * Time.deltaTime;
			}
			else
			{
				currentRotationSpeed -= speedupRate * Time.deltaTime;
			}

			transform.Rotate(rotationDirection * currentRotationSpeed * Time.deltaTime);

		}

	}

	private Vector3 Randomize(Vector3 v3, float inacc)
	{
		v3 = v3 + (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f)) * inacc);
		v3.Normalize ();
		return v3;
	}

}
