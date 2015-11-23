using UnityEngine;
using System.Collections;

public class ShieldManager {

	public GameObject instance;
	public elementType elementType;
	public ShieldElement shieldElement;

	public void Setup()	{
		shieldElement = instance.GetComponent<ShieldElement> ();
	}
}
