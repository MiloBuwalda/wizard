using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DarkBehavior : MonoBehaviour {

    // all children of this object are targetted by this behavior
    public GameObject ShapeParent;
    // from which point should the object be deconstructed?
    public Vector3 Orgin;
    // how fast should the progress happen (note: put higher for larger objects.)
    public float Speed = 0.5F;
    // offset
    public float scatter = 0.4F;
    // note: this automatically scales up for scaled up objects.
    public float shardrange = 3.0f;
    float range = 0;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Orgin;
        range += Time.deltaTime * Speed;
        
        //check each object in scene. (note, in large scenes the scene should be split up in multiple parents, he'll check every object in whatever object he's given for the value of ShapeParent.
        foreach (Transform child in ShapeParent.transform)
        {
            if (child.GetComponent<MeshFilter>())
            {
                MeshFilter mesh_filter = child.GetComponent<MeshFilter>();
                Vector3[] vertices = mesh_filter.mesh.vertices;
                List<int> tri = new List<int>(mesh_filter.mesh.triangles);
                int count = tri.Count / 3;
                for (int i = count - 1; i >= 0; i--)
                {
                    Vector3 V1 = vertices[tri[i * 3 + 0]];
                    Vector3 V2 = vertices[tri[i * 3 + 1]];
                    Vector3 V3 = vertices[tri[i * 3 + 2]];
                    float V1D = Vector3.Distance(V1 + child.position,transform.position);
                    float V2D = Vector3.Distance(V2 + child.position, transform.position);
                    float V3D = Vector3.Distance(V3 + child.position, transform.position);
                    if (V1D < range && V2D < range && V3D < range)
                    {
                        tri.RemoveRange(i * 3, 3);
                        GameObject shard = new GameObject("shard", typeof(MeshFilter), typeof(MeshRenderer));
                        shard.transform.parent = this.transform;
                        MeshFilter shard_mesh_filter = shard.GetComponent<MeshFilter>();

                        Mesh mesh = shard_mesh_filter.mesh;
                        mesh.Clear();
                        mesh.vertices = new Vector3[] { V1,V2,V3 };
                        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
                        mesh.triangles = new int[] { 0, 1, 2 };

                        shard.GetComponent<MeshFilter>().mesh = mesh;

                        // the shards automatically copy the material of the target.
                        // note that for optimal effect the target's shader should have backface culling.
                        // there's a material with compatible shader in the package.
                        shard.GetComponent<MeshRenderer>().material = child.GetComponent<MeshRenderer>().material;

                        shard.transform.position = shard.transform.position * child.transform.localScale.x + Random.onUnitSphere * 0.4F;
                        shard.transform.localScale = child.transform.localScale;
                    }

                }
                if (tri.Count > 0)
                {
                    // if the target objects mesh is not yet empty, save the model so the renderer can show the half-eroded object.
                    mesh_filter.mesh.triangles = tri.ToArray();
                }
                else
                {
                    // clear up the empty gameobject once it's mesh is gone.
                    Destroy(child.gameObject);
                }
                

            }

        }

        foreach(Transform child in this.transform)
        {
            if(Vector3.Distance(child.transform.position,transform.position) <= child.transform.localScale.x * shardrange)
            {
                //move the loosened triangles (shards) away from the origin point.
                child.transform.localPosition *=  1.1F;


            }
            else
            {
                // clear up the triangles that move out of range
                Destroy(child.gameObject);
            }
            
        }

    }
}
