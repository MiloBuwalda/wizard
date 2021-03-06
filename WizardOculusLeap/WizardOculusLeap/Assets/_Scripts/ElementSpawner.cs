﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementSpawner : MonoBehaviour {
	public static ElementSpawner instance;
	public Dictionary<string, GameObject> elementBook;
	public GameObject fireVisual;
	public GameObject airVisual;
	public GameObject waterVisual;
	public GameObject earthVisual;
	public Transform fireSpawn;
	public Transform airSpawn;
	public Transform waterSpawn;
	public Transform earthSpawn;
 	public Transform playerHeight;
	public Transform forwardDirection;
	int basisNumber;
	int otherNumber;
	
	void Awake(){
		//singleton
		instance = this;
	}

	void Start(){
		//Add elements to dictionary
		elementBook = new Dictionary<string, GameObject> ();
		GameObject[] elements = Resources.LoadAll<GameObject> ("Elements");
		foreach(GameObject g in elements){
			elementBook.Add(g.name,g);
		}
	}

	//Create an element
	public ElementManager GetElementOfType(elementType t, int handNumber){
		ElementManager element = null;
		GameObject g;
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();

		if (elementBook.TryGetValue (t.ToString (), out g)) {
			element = new ElementManager ();
			element.instance = (GameObject)Instantiate (g, hands[handNumber].GetPalmPosition(), transform.rotation);
			element.elementType = t;
			element.Setup ();
			element.elementMovement.handNumber = handNumber;
		} else {
			print ("Could not find element: " + t.ToString());
		}

		return element;
	}

	//Find out which element to spawn, based on its location
	public void ElementToSpawn (Vector3 location, int handNumber){
		float locationSignX = Mathf.Sign (location.x);
		if (GameManager.instance.player.Team == Team.Red) {
			locationSignX *= -1;
		}

		if (locationSignX == -1 && location.y > playerHeight.position.y) { //links boven
			ElementSpawnStart(elementType.Fire, handNumber);
		}
		else if (locationSignX == 1 && location.y > playerHeight.position.y) { //rechts boven
			ElementSpawnStart(elementType.Air, handNumber);
		}
		else if (locationSignX == 1 && location.y < playerHeight.position.y) { //rechts onder
			ElementSpawnStart(elementType.Water, handNumber);
		}
		else if (locationSignX == -1 && location.y < playerHeight.position.y) { //links onder
			ElementSpawnStart(elementType.Earth, handNumber);
		}
	}

	//Move an element (visual only) from spawnpoint to hand
	void ElementSpawnStart (elementType element, int handNumber){
		switch (element) { 
		case elementType.Fire:
			GameObject clone1 = (GameObject)Instantiate (fireVisual, fireSpawn.position, Quaternion.identity);
			clone1.GetComponent<ElementSpawnVisual>().element = element; 
			clone1.GetComponent<ElementSpawnVisual>().handNumber = handNumber;
			GameManager.instance.audioManager.OneShot(elementType.Fire);
			break;
		case elementType.Air:
			GameObject clone2 = (GameObject)Instantiate (airVisual, airSpawn.position, Quaternion.identity);
			clone2.GetComponent<ElementSpawnVisual>().element = element; 
			clone2.GetComponent<ElementSpawnVisual>().handNumber = handNumber; 
			break;
		case elementType.Water:
			GameObject clone3 = (GameObject)Instantiate (waterVisual, waterSpawn.position, Quaternion.identity);
			clone3.GetComponent<ElementSpawnVisual>().element = element; 
			clone3.GetComponent<ElementSpawnVisual>().handNumber = handNumber; 
			break;
		case elementType.Earth:
			GameObject clone4 = (GameObject)Instantiate (earthVisual, earthSpawn.position, Quaternion.identity);
			clone4.GetComponent<ElementSpawnVisual>().element = element; 
			clone4.GetComponent<ElementSpawnVisual>().handNumber = handNumber; 
			break;
		}


	}

	public ElementManager CombineElements(List<ElementManager> list){
		ElementManager element = null;
		GameObject g;
		int handNumber;
		HandModel[] hands = GameManager.instance.movementManager.handController.GetAllPhysicsHands();
		float speed0 = list[0].instance.GetComponent<ElementMovement>()._magnitude;
		float speed1 = list[1].instance.GetComponent<ElementMovement>()._magnitude;


		if(speed0 < speed1){
			basisNumber = 0; 
			otherNumber = 1;
			handNumber = list[0].instance.GetComponent<ElementMovement>().handNumber;
		}else{
			basisNumber = 1;
			otherNumber = 0;
			handNumber = list[1].instance.GetComponent<ElementMovement>().handNumber;
		}

		elementType basis = list[basisNumber].elementType;
		elementType other = list[otherNumber].elementType;

		string elementTypeCombined = basis.ToString() + other.ToString();
		
		if (elementBook.TryGetValue (elementTypeCombined, out g)) {
			element = new ElementManager ();
			element.instance = (GameObject)Instantiate (g, hands[handNumber].GetPalmPosition(), transform.rotation);
			switch (basis){
			case elementType.Fire:
				switch (other){
				case elementType.Air:
					basis = elementType.FireAir;
					break;
				case elementType.Earth:
					basis = elementType.FireEarth;
					break;
				}
				break;
			case elementType.Air:
				switch (other){
				case elementType.Water:
					basis = elementType.AirWater;
					break;
				case elementType.Fire:
					basis = elementType.FireAir;
					break;
				}
				break;
			case elementType.Water:
				switch (other){
				case elementType.Air:
					basis = elementType.WaterAir;
					break;
				case elementType.Earth:
					basis = elementType.WaterEarth;
					break;
				}
				break;
			case elementType.Earth:
				switch (other){
				case elementType.Fire:
					basis = elementType.EarthFire;
					break;
				case elementType.Water:
					basis = elementType.EarthWater;
					break;
				}
				break;
			}
			element.elementType = basis;
			element.Setup ();
			element.elementMovement.handNumber = handNumber;
		} else {
			print ("Could not find element");
		}
		return element;
	}
}
