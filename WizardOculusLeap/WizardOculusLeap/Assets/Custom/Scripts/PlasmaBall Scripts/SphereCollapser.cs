using UnityEngine;
using System.Collections;

public class SphereCollapser : MonoBehaviour 
{
	public float collapseSpeed = .1f;
	public float minScale = .7f;
	public float maxScale = 1.3f;
	private float currentScale = 1.0f;
	
	// Update is called once per frame
	void Update () 
	{
		if (currentScale < minScale) 
		{
			currentScale = maxScale;
		} 
		else if (currentScale > maxScale) 
		{
			currentScale = maxScale;
		} 
		else 
		{
			currentScale -= collapseSpeed * Time.deltaTime;
		}
		transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
	}
}
