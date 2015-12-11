using UnityEngine;
using System.Collections;

public class EnvironmentCheck : MonoBehaviour {

	public Vector3 local;
	public Vector3 global;

	// Use this for initialization
	void Start () {
		local = this.transform.localPosition;
		global = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		local = this.transform.localPosition;
		global = transform.TransformPoint(local);
//		global = Vector3.zero;
	}
}
