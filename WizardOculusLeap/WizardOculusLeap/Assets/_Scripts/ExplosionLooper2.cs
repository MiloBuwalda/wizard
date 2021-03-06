﻿using UnityEngine;
using System.Collections;

public class ExplosionLooper2 : MonoBehaviour 
{
	[Range(0.5f,10.0f)]
	public float loopDuration = 1.0f;
	[Range(0.1f,2.0f)]
	public float correctionModifier = 0.4f;

	// Update is called once per frame
	void Update () 
	{
		float r = Mathf.Sin ((Time.time / loopDuration) * 2 * (Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin ((Time.time / loopDuration + .333333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin ((Time.time / loopDuration + .666666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float a = Mathf.Sin ((Time.time / loopDuration) * 2 * (Mathf.PI)) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b) * correctionModifier;
		r *= correction;
		g *= correction;
		b *= correction;
		a *= correctionModifier;
		//print (r + " + " + g + " + " + b + " + " + a);
		gameObject.GetComponent<Renderer>().material.SetVector ("_ChannelFactor", new Vector4 (r, g, b, a));
	}
}
