using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthExplscript : MonoBehaviour {
	
	
	
	public Material mat;
	public Mesh[] modelLibary;
	
	List<GameObject> metaballs;
	List<Vector3[]> vert_norm;
	
	List<Color> c;
	
	public float spd = 1;
	public float dist = 1;
	public float magn = 1;
	
	
	public float r = 2;
	public int co = 0;
	
	// Use this for initialization
	void Start()
	{
		
		metaballs = new List<GameObject>();
		vert_norm = new List<Vector3[]>();
		c = new List<Color>();
		
		Color c0 = new Color ();
		float r = Random.Range(220.0f,255.0f);
		float g = Random.Range(220.0f,255.0f);
		float b = Random.Range(220.0f,255.0f);
		c0.r = r;
		c0.g = g;
		c0.b = b;
		c0.a = 1;
		
		c0.r = 1.0f/255.0f*r;
		c0.g = 1.0f/255.0f*g;
		c0.b = 1.0f/255.0f*b;
		
		c.Add(c0);
		c.Add(c0 * 0.7F);
		c.Add(c0 * 1.3F);
		
		List<int> randomNumbers = new List<int>();
		
		
		
		foreach (Transform child in transform)
		{
			int nr = Random.Range(0, 5);
//			while (randomNumbers.Contains(nr))
//			{
//				nr = Random.Range(0, 5);
//			}
			randomNumbers.Add(nr);
			
			Spawn(child.gameObject,nr);
			
		}
		
	}
	
	// Update is called once per frame
	void Update()
	{

		int count = metaballs.Count;
		if (count != 0) {
			
			for (int i = count - 1; i >= 0; i--) {
				metaballs [i].transform.rotation = Quaternion.Euler (90, 0, 0);
				Shaderding(metaballs[i].transform, c[i], i);
				
				for (int j = count - 1; j >= 0; j--) {
					if (j != i) {
						Reach (metaballs [j].transform.position, metaballs [i].transform, vert_norm [i], c [i]);
					}
				}
			}
		}
		
	}
	
	
	
	void Shaderding(Transform mb,Color col, int i)
	{
		//needs changing
		//mat.SetFloat("_Shininess", (Mathf.Sin(Time.time*2)+1)*0.14F+0.03F);
		float outline = Mathf.Sin((Time.time + (i*0.3f) * 3f))*0.6F;


		col.g = Mathf.Clamp01(col.g + outline);
		col.b = Mathf.Clamp01(col.g + outline * 0.6f);

		MeshRenderer mesh_renderer = mb.GetComponent<MeshRenderer>();
		//mesh_renderer.material.color = col * outline * 120;
		//mesh_renderer.material.SetColor ("_OutlineColor", Color.black);
		//mesh_renderer.material.SetFloat ("_Outline", outline);
		mesh_renderer.material.SetColor ("_ReflectColor", col);
		
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
			//Debug.DrawLine(ReachPoint, vert[i] + metab.position, c);
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
		Metaball.transform.localPosition = new Vector3(0,0,0);
		Metaball.transform.localScale = new Vector3(1, 1, 1);
		
		
		
		MeshFilter mesh_filter = Metaball.GetComponent<MeshFilter>();
		
		
		
		
		mesh_filter.mesh = modelLibary[modelnr];
		MeshRenderer mesh_renderer = Metaball.GetComponent<MeshRenderer>();
		mesh_renderer.material = new Material(mat);
		
		r = mesh_renderer.bounds.extents.magnitude;
		
		metaballs.Add(Metaball);
		vert_norm.Add(mesh_filter.mesh.vertices);
		
	}
	
	
	

}
