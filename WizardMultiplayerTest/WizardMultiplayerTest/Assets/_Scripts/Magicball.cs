using UnityEngine;
using System.Collections;

public class Magicball : MonoBehaviour 
{
	public int timeExisting = 5;

	private float timer;
	
	void Awake ()
	{
		timer = Time.time + timeExisting;
	}
	
	
	public void Die()
	{
		Destroy(gameObject);	
	}
	
	void Update () 
	{		
		if (timer < Time.time) 
		{
			Die();
		}
	}
}