using UnityEngine;
using System.Collections;

public class ShieldManager {

	public GameObject instance;
	public elementType elementType;
	public ShieldElement shieldElement;

	public int id;

	public void Setup()	{
		shieldElement = instance.GetComponent<ShieldElement> ();
		shieldElement.id = id;
	}

	public void DestroyMe()
	{
		if (instance != null) {
			PhotonNetwork.Destroy(instance);
		}

		Debug.Log ("Destroy me: " + elementType);
	}
}
