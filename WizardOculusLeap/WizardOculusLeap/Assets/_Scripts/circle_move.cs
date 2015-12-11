using UnityEngine;
using System.Collections;

public class circle_move : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// move in sin sircle on x and z (around y)

		Vector3 positionMover = new Vector3 ();
		positionMover.x = Mathf.Sin (Time.deltaTime);
		positionMover.z = Mathf.Cos (Time.deltaTime);
		positionMover.y = this.transform.position.y;
		this.transform.position += positionMover;
	}
}
