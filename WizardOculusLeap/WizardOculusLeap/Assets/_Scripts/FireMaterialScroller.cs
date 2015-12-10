using UnityEngine;
using System.Collections;

public class FireMaterialScroller : MonoBehaviour {
	public Material mat;
	private Vector2 scrollVec = Vector2.zero;
	public Vector2 difVec;
	[Range(0.01f,2.0f)]
	public float size = 1.0f;
	public GameObject lightning;

	// Use this for initialization
	void Start () 
	{
		mat = GetComponent<Renderer> ().material;
		lightning.GetComponent<LightningboltShield>().impactRadius *= size;

	}
	
	// Update is called once per frame
	void Update () 
	{
		scrollVec += difVec *Time.deltaTime;
		mat.SetTextureOffset ("_MainTex", scrollVec);
		transform.localScale = new Vector3((Mathf.Sin (Time.time) * (size/10) + size),(Mathf.Cos (Time.time) * (size/10) + size),size/10);
	}
}
