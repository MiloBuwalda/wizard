using UnityEngine;
using System.Collections;

public class calltest : MonoBehaviour {

    public GameObject prefab;
    public GameObject parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            DarkSpawn(parent,new Vector3(0,0,0),0.5F,0.4F,3.0F);
        }
    }

    void DarkSpawn(GameObject p,Vector3 o,float sp,float sc,float sh)
    {
        GameObject dark = Instantiate(prefab);
        dark.name = "Dark";
        dark.GetComponent<DarkBehavior>().Orgin = o;
        dark.GetComponent<DarkBehavior>().Speed = sp;
        dark.GetComponent<DarkBehavior>().scatter = sc;
        dark.GetComponent<DarkBehavior>().shardrange = sh;
        dark.GetComponent<DarkBehavior>().ShapeParent = p;

    }
}
