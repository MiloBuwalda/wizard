using UnityEngine;
using System.Collections;

public class Windscript : MonoBehaviour {

    public Material mat;
    public float time = 1.0F;
    public float dist = 3.0F;
    [Range(0.0F, 2.0F)]
    public float offset = 0.0F;

    [Range(0.0F, 3.0F)]
    public float start = 1.0F;
    [Range(0.0F, 3.0F)]
    public float end = 1.0F;



    // Use this for initialization
    void Start () {

        foreach (Transform child in transform)
        {
            child.GetChild(0).GetComponent<TrailRenderer>().material = mat;

        }

    }
	
	// Update is called once per frame
	void Update () {

        float o = Mathf.Sin(Time.time) * offset;

        foreach (Transform child in transform)
        {
            Transform trail = child.GetChild(0);
            trail.transform.localPosition = new Vector3(0, dist + o, 0);
            trail.GetComponent<TrailRenderer>().material = mat;
            trail.GetComponent<TrailRenderer>().time = time;
            trail.GetComponent<TrailRenderer>().startWidth = start;
            trail.GetComponent<TrailRenderer>().endWidth = end;


        }

    }
}
