﻿using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject[] spawnPoints;

	public GameObject playerReference;
	public GameObject inFrontOfPlayer;

	private Transform currentSpawnPoint;

	// Use this for initialization
	void Start () {
		Setup ();
	}

	void Setup()
	{
		if (spawnPoints == null || spawnPoints.Length == 0) {
			spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

			// if still null no spawnpoints have been set 
			if(spawnPoints == null )
				throw new UnityException("No spawn points exist in game or no Spawn tag has been assigned");
		}
		if (playerReference == null)
			playerReference = GameObject.FindGameObjectWithTag ("Player");
		else if (playerReference == null)
			throw new UnityException ("No Player Gameobject exisits or has been asigned the Player tag");
	}

	public void SetCurrentSpawnPoint()
	{
		Debug.Log ("PlayerSpawner/PhotonNetwork.playerList.Length: "+ PhotonNetwork.playerList.Length);

		// if first player set player to pos 1 if player 2 then set tot pos 2
		if (PhotonNetwork.playerList.Length == 1) {
			currentSpawnPoint = spawnPoints [0].transform;
			Debug.Log ("currentspawnPoint:set 0");
		} else {
			currentSpawnPoint = spawnPoints [1].transform;
			Debug.Log ("currentspawnPoint:set 1");
		}
		setSpawnPoint (currentSpawnPoint);
		resetLocalSpace(playerReference);

		// if on position B then the InFrontOfPlayer object is pointed 
		// in the wrong direction. So this means 
		if (playerReference.transform.position.z > 0) {
			inFrontOfPlayer.transform.rotation = Quaternion.Euler (0, 0, 0);
			Debug.Log("Infront rotation set with y= 180 ");
		}
	}

	void resetLocalSpace(GameObject player)
	{
		player.transform.localPosition = Vector3.zero;
		player.transform.localRotation = Quaternion.identity;

	}

	void setSpawnPoint(Transform desiredSpawnPoint)
	{
		playerReference.transform.SetParent(desiredSpawnPoint);

	}
	// Create Networked Player creates a player on one of the 2 locations
//	public void CreateNetworkedPlayer() 
//	{
//		if (PhotonNetwork.playerList.Length == 1)
//		    {
//				PhotonNetwork.Instantiate (
//					playerReference.name, 
//					spawnPointA.position, 
//					Quaternion.identity, 
//					0);
//		}
//	}
}
