using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class windcontroll : MonoBehaviour {

    public float sped = 1;

    public Transform wind0;
    public Transform wind1;
    public Transform wind2;
    public Transform wind3;
    public Transform wind4;

    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    List<Transform> wind;
    List<Vector3> points;
    List<Vector3> Tpoints;

    // Use this for initialization
    void Start () {

        wind = new List<Transform>();
        wind.Add(wind0);
        wind.Add(wind1);
        wind.Add(wind2);
        wind.Add(wind3);
        wind.Add(wind4);

        points = new List<Vector3>();
        points.Add(point0.position);
        points.Add(point1.position);
        points.Add(point2.position);
        points.Add(point3.position);
        points.Add(point4.position);

        Tpoints = new List<Vector3>();
        Tpoints.Add(point0.position);
        Tpoints.Add(point1.position);
        Tpoints.Add(point2.position);
        Tpoints.Add(point3.position);
        Tpoints.Add(point4.position);



    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < wind.Count; i++)
        {
            wind[i].position = Vector3.Lerp(wind[i].position, Tpoints[i], Time.deltaTime * sped);

            if (Vector3.Distance(wind[i].position, Tpoints[i]) <= 0.1F)
            {

                
                        Tpoints[i] = points[Random.Range(0, 5)];
                      
                



            }

        }
    }
}
