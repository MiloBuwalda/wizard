using UnityEngine;
using System.Collections;

public class SphereScaler : MonoBehaviour 
{
	public float scaleSpeed = .1f;
	public float minScale = .7f;
	public float maxScale = 1.3f;
	private float currentScale = 1.0f;
	private bool isGrowing = true;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentScale < minScale) 
		{
			isGrowing = true;
			currentScale = minScale;
		} 
		else if (currentScale > maxScale) 
		{
			isGrowing = false;
			currentScale = maxScale;
		} 
		else 
		{
			if(isGrowing)
				currentScale += scaleSpeed * Time.deltaTime;
			else
				currentScale -= scaleSpeed * Time.deltaTime;
		}

		transform.localScale = new Vector3 (currentScale, currentScale, currentScale);

	}
}
