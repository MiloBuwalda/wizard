using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public Transform spawnA;
	public Transform spawnB;

	public GameObject playerReference;

	private Transform currentSpawnPoint;

	// Use this for initialization
	void Start () {
		if (PhotonNetwork.playerList.Length == 1)
			currentSpawnPoint = spawnA;
		else
			currentSpawnPoint = spawnB;
	}
	
	// Create Networked Player creates a player on one of the 2 locations
	public void CreateNetworkedPlayer() 
	{
		if (PhotonNetwork.playerList.Length == 1)
		    {
				PhotonNetwork.Instantiate (
					playerReference.name, 
					spawnA.position, 
					Quaternion.identity, 
					0);
		}
	}
}
