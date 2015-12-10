using UnityEngine;
using System.Collections;

public class AirShieldWobbler : MonoBehaviour 
{
	#region Variables
	[Range(0.1f,0.9f)]
	public float wobbleAmount = 0.5f;
	[Range(0.1f,2.0f)]
	public float wobbleSpeed = 1.0f;
	private float currentWobble;
	private bool wobbleIn = true;

	public bool insideTrail = true;
	public bool oppositeTrail = true;
	#endregion



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (wobbleIn)
			transform.localPosition -= new Vector3(transform.localPosition.x,wobbleAmount * wobbleSpeed * Time.deltaTime,transform.localPosition.z);
		else
			transform.localPosition += new Vector3(transform.localPosition.x,wobbleAmount * wobbleSpeed * Time.deltaTime,transform.localPosition.z);

		if (insideTrail) 
		{
			if (oppositeTrail) 
			{
				if (transform.localPosition.y < -wobbleAmount && wobbleIn)
					wobbleIn = false;
				else if (transform.localPosition.y > 0 && !wobbleIn)
					wobbleIn = true;
			}
			else 
			{
				if (transform.localPosition.y < 0 && wobbleIn)
					wobbleIn = false;
				else if (transform.localPosition.y > -wobbleAmount && !wobbleIn)
					wobbleIn = true;
			}
		} 
		else 
		{
			if (oppositeTrail)
			{
				//if (transform.localPosition.y >
			}
			else
			{

			}

		}
	}
}
