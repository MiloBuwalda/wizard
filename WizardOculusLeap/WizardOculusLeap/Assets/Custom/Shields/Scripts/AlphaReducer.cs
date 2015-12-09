using UnityEngine;
using System.Collections;

public class AlphaReducer : MonoBehaviour 
{
	[Range(0.1f,1.0f)]
	public float alphaScale = 0.5f;
	private Material mat;

	// Use this for initialization
	void Start () 
	{
		mat = gameObject.GetComponent<Renderer> ().material;
		//mat.SetFloat("_

		//Color col = mat.color;
		//col.a *= alphaScale;
		//mat.color = col;
		//print (mat.color);
	}
}
