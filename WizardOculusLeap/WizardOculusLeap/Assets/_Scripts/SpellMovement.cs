using UnityEngine;
using System.Collections;

public class SpellMovement : MonoBehaviour {

	public Vector3 startMarker;
	public Vector3 endMarker;
	public float speed = 15.0f;

	private float startTime;
	private float journeyLength;

	private Vector3 fakePosition;

	private float distCovered;
	private float fracJourney;

	private float currentHeight;

	void Start()
	{

		// If photonview is not mine turn off script
		PhotonView thisView = gameObject.GetComponent<PhotonView> ();
		if (!thisView.isMine)
			enabled = false;
		startMarker = this.transform.position;


		fakePosition = transform.position;
		currentHeight = transform.position.y;


			
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

		fakePosition = Vector3.Lerp(startMarker, endMarker, fracJourney);
		currentHeight = Mathf.Sin (distCovered / journeyLength * Mathf.PI) * journeyLength/4;

		transform.position = new Vector3 (fakePosition.x, currentHeight + fakePosition.y, fakePosition.z);

	}
}

