using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Earthscript : MonoBehaviour {

    



    public Material mat;
    public Mesh[] modelLibary;

    List<GameObject> metaballs;
    List<Vector3[]> vert_norm;

    List<Color> c;

    public float spd = 1;
    public float dist = 1;
    public float magn = 1;
    

    // Use this for initialization
    void Start()
    {
        
        
        metaballs = new List<GameObject>();
        vert_norm = new List<Vector3[]>();
        c = new List<Color>();
        c.Add(Color.red);
        c.Add(Color.green);
        c.Add(Color.blue);

        List<int> randomNumbers = new List<int>();

        

        foreach (Transform child in transform)
        {
            int nr = Random.Range(0, 4);
            while (randomNumbers.Contains(nr))
            {
                nr = Random.Range(0, 24);
            }
            randomNumbers.Add(nr);

            Spawn(child.gameObject,nr);

        }

    }

    // Update is called once per frame
    void Update()
    {
        Shaderding();

        int count = metaballs.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            metaballs[i].transform.rotation = Quaternion.Euler(0, 0, 0);


            for (int j = count - 1; j >= 0; j--)
            {
                if (j != i)
                {
                    Reach(metaballs[j].transform.position, metaballs[i].transform, vert_norm[i], c[i]);
                }
            }
        }
    }

    void Shaderding()
    {
        
        mat.SetFloat("_Shininess", (Mathf.Sin(Time.time*2)+1)*0.14F+0.03F);
    }

    void Reach(Vector3 ReachPoint, Transform metab, Vector3[] vn, Color c)
    {
        float rad = magn;
        Vector3[] vert_norm = vn;

        MeshFilter mesh_filter = metab.GetComponent<MeshFilter>();
        Vector3[] vert = mesh_filter.mesh.vertices;

        for (int i = 0; i < vert.Length; i++)
        {
            //ReachPoint = ReachPoint + metab.position;
            //Vector3 v = Quaternion.AngleAxis(30, Vector3.up) * vert[i];
            Debug.DrawLine(ReachPoint, vert[i] + metab.position, c);
            float distance = Vector3.Distance(vert_norm[i], ReachPoint);
            Vector3 vert_goal = vert_norm[i];
            if (distance <= rad)
            {
                // (rad - distance) daar moet ergens een kwadraad ofzo in
                Vector3 vert_trans = (ReachPoint - metab.position);
                vert_trans = vert_trans * (rad - distance);

                vert_goal += vert_trans;


            }
            vert[i] = Vector3.Lerp(vert[i], vert_goal, Time.deltaTime * spd);

        }
        mesh_filter.mesh.vertices = vert;
    }

    void Spawn(GameObject Spawnpoint, int modelnr)
    {
        GameObject Metaball = new GameObject("Metaball", typeof(MeshFilter), typeof(MeshRenderer));
        Metaball.transform.parent = Spawnpoint.transform;
        Metaball.transform.localPosition = Vector3.up * dist;
        

        MeshFilter mesh_filter = Metaball.GetComponent<MeshFilter>();
        
        //int l = Random.ra


        mesh_filter.mesh = modelLibary[modelnr];
        MeshRenderer mesh_renderer = Metaball.GetComponent<MeshRenderer>();
        mesh_renderer.material = mat;
        //mesh_renderer.castShadows = false;

        

        metaballs.Add(Metaball);
        vert_norm.Add(mesh_filter.mesh.vertices);

    }


}

