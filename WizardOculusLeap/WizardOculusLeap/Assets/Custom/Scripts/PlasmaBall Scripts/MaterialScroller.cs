using UnityEngine;
using System.Collections;

public class MaterialScroller : MonoBehaviour {
	public Material mat;
	private Vector2 scrollVec = Vector2.zero;
	public Vector2 difVec;
	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		scrollVec += difVec *Time.deltaTime;
		mat.SetTextureOffset ("_MainTex", scrollVec);
	}
}
