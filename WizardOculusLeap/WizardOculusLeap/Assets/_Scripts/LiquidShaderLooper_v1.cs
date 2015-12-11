using UnityEngine;
using System.Collections;

public class LiquidShaderLooper_v1 : MonoBehaviour 
{

	public float loopDuration = 1.0f;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float r = Mathf.Sin ((Time.time / loopDuration) * 2 * (Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin ((Time.time / loopDuration + .333333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin ((Time.time / loopDuration + .666666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;
		gameObject.GetComponent<Renderer>().material.SetVector ("_ChannelFactor", new Vector4 (r, g, b, 0));
	}
}
