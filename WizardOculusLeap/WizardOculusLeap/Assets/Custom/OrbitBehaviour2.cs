using UnityEngine;
using System.Collections;

public class OrbitBehaviour2 : MonoBehaviour {

   
    public float turnspeed = 10;


    Vector3 c;

    // Use this for initialization
    void Start () {
        //transform.position = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {


        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }
}
