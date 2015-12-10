using UnityEngine;
using System.Collections;

public class SpellMovement : MonoBehaviour {

	public Vector3 startMarker;
	public Vector3 endMarker;
	public float speed = 15.0f;

	private float startTime;
	private float journeyLength;

	private float distCovered;
	private float fracJourney;

	void Start()
	{

		// If photonview is not mine turn off script
		PhotonView thisView = gameObject.GetComponent<PhotonView> ();
		if (!thisView.isMine)
			enabled = false;
		startMarker = this.transform.position;

			
		if(GameManager.instance.player.Team==Team.Blue){
			endMarker = GameManager.instance.playerSpawner.spawnPointRed.position;
		}
		else {
			endMarker = GameManager.instance.playerSpawner.spawnPointBlue.position;
		}
		Debug.Log ("endMarker" + endMarker);
		startTime = Time.time;
		journeyLength = Vector3.Distance (startMarker,endMarker);
	}

	void Update () {
		distCovered = (Time.time - startTime) * speed;
		fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp (startMarker, endMarker, fracJourney);
	}
}
