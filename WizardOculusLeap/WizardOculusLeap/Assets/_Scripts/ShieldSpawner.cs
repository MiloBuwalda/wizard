using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldSpawner : MonoBehaviour {
	public static ShieldSpawner instance;
	public Dictionary<string, GameObject> shieldBook;

	void Awake () {
		//singleton
		instance = this;
	}

	void Start(){
		shieldBook = new Dictionary<string, GameObject> ();
		GameObject[] shields = Resources.LoadAll<GameObject> ("Shields"); //Load all shield prefabs from resource folder into dictionary
		foreach(GameObject g in shields)
		{
			shieldBook.Add(g.name,g);
		}
	}

	/// <summary>
	/// NETWORKED: Creates the shield.
	/// </summary>
	/// <returns>The shield.</returns>
	/// <param name="element">Element.</param>

	//Create a shield with element pool from player when player demands it
	public ShieldManager CreateShield(ElementManager element){
		if (element != null) {
			ShieldManager shield = new ShieldManager();
			ElementManager basis = element;
			GameObject g;
			PhotonView currentPhotonView;
			if (shieldBook.TryGetValue (basis.elementType.ToString() + "Shield", out g)){
				// Instantiate on network (call current element shield from within shield folder)
				shield.instance = (GameObject) PhotonNetwork.Instantiate(
					"Shield/"+g.name, basis.instance.transform.position, transform.rotation,0);
				// Perhaps this needs to be shifted forward since photon instantiation prob requires the view:
				currentPhotonView = shield.instance.AddComponent<PhotonView>();
				// Add ShieldObserverComponent which contains the network synchronization stuff
				shield.instance.AddComponent<ShieldObserver>();
				// observe it in the photonview (!!!TEST!!!)
				currentPhotonView.observed = shield.instance.GetComponent<ShieldObserver>();

				shield.elementType = basis.elementType;
				shield.Setup();
				shield.shieldElement.elementType = basis.elementType;
			} 
			
			return shield;
		} else {
			//buzz sound
			return null;
		}
	}

	//Create a shield with element pool from player when player demands it
	public ShieldManager CreateShield2(ElementManager element){
		if (element != null) {
			ShieldManager shield = new ShieldManager();
			ElementManager basis = element;
			GameObject g;
			if (shieldBook.TryGetValue (basis.elementType.ToString() + "Shield", out g)){
				shield.instance = (GameObject)Instantiate(g, basis.instance.transform.position, transform.rotation);
				shield.elementType = basis.elementType;
				shield.Setup();
				shield.shieldElement.elementType = basis.elementType;
			} 

			return shield;
		} else {
			//buzz sound
			return null;
		}
	}
}
