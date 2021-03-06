﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldSpawner : MonoBehaviour {

	public static ShieldSpawner instance;
	public Dictionary<string, GameObject> shieldBook;

	// shield id not a counter
	public int shieldId = 1000;

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
			shield.id = shieldId;
			shieldId++;
			ElementManager basis = element;
			GameObject g;
			PhotonView currentPhotonView;
			ShieldObserver shieldObserver;
			if (shieldBook.TryGetValue (basis.elementType.ToString() + "Shield", out g)){


//				CLEANUPCOMPONENTS
//				ShieldObserver[] so = g.GetComponents<ShieldObserver>();
//				for (int i = 0; i < so.Length; i++) {
//					DestroyImmediate(so[i], true);
//				}
//				
//				PhotonView[] pv = g.GetComponents<PhotonView>();
//				for (int i = 0; i < pv.Length; i++) {
//					DestroyImmediate (pv[i], true);
//				}

				currentPhotonView = g.GetComponent<PhotonView>();

				if(currentPhotonView == null){
					currentPhotonView = g.AddComponent<PhotonView>();
				}

				shieldObserver = g.GetComponent<ShieldObserver>();
				if (shieldObserver == null){
					shieldObserver = g.AddComponent<ShieldObserver>();
				}
				
//				Debug.Log("currentPhotonView Count: " + currentPhotonView.ObservedComponents.Count);
				currentPhotonView.ObservedComponents.Clear();
				if(currentPhotonView.ObservedComponents!=null && currentPhotonView.ObservedComponents.Count==0)
				{
					currentPhotonView.ObservedComponents.Add(shieldObserver);
//					currentPhotonView.ObservedComponents[0] = shieldObserver;
//					currentPhotonView.ObservedComponents.
				}

				// Instantiate on network (call current element shield from within shield folder)
				shield.instance = (GameObject) PhotonNetwork.Instantiate(
					"Shields/"+g.name, basis.instance.transform.position, transform.rotation ,0);

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
