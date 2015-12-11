using UnityEngine;
using System.Collections;

public class OrbitBehaviour34 : MonoBehaviour {

    public float speed = 1;
    public float speed2 = 1;
    public float offset = 0;
    Vector3 c;

    // Use this for initialization
    void Start () {
        //transform.position = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.parent.rot
        c = new Vector3(Mathf.Sin(Time.time * speed2 + offset), Mathf.Cos(Time.time * speed2 + offset), Mathf.Sin(Time.time * speed2 + offset));
        
        transform.Rotate(c, Time.deltaTime * speed);

    }
}
