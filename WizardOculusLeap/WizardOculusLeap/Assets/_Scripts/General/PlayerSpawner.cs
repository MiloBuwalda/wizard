using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject[] spawnPoints;

	public GameObject playerReference;

	private Transform currentSpawnPoint;

	// Use this for initialization
	void Start () {
		Setup ();
		SetCurrentSpawnPoint ();
	}

	void Setup()
	{
		if (spawnPoints == null || spawnPoints.Length == 0) {
			spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

			// if still null no spawnpoints have been set 
			if(spawnPoints == null )
				throw new UnityException("No spawn points exist in game or no Spawn tag has been assigned");
		}
	}

	void SetCurrentSpawnPoint()
	{
		if (PhotonNetwork.playerList.Length == 1)
			currentSpawnPoint = spawnPoints[0].transform;
		else
			currentSpawnPoint = spawnPoints[1].transform;
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
