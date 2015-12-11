using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public MovementManager movementManager;
	public PlayerSpawner playerSpawner;
	public Player player;
	public ElementSpawner elementSpawner;
	public SpellSpawner spellSpawner;
	public ShieldSpawner shieldSpawner;
	public AudioManager audioManager;

	public static Team currentTeam;
	void Awake(){
		if(instance == null)
		{
			instance = this;
		}
	}
	void Start(){
		movementManager = 	GetComponent<MovementManager> ();
		playerSpawner = 	GetComponent<PlayerSpawner> ();
		player = 			GetComponent<Player> ();
		elementSpawner = 	GetComponent<ElementSpawner> ();
		spellSpawner = 		GetComponent<SpellSpawner> ();
		shieldSpawner = 	GetComponent<ShieldSpawner> ();
		audioManager = 		GetComponent<AudioManager> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F2)) {
			OVRManager.display.RecenterPose();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			currentTeam = GameManager.instance.player.Team;
			Application.LoadLevel (Application.loadedLevelName);
			Debug.Log("RESET");
			GameManager.instance.playerSpawner.playerReference = GameObject.FindGameObjectWithTag("Player");
			GameManager.instance.playerSpawner.SetCurrentSpawnPoint();		
		}
	}

	void OnLevelWasLoaded()
	{
		StartCoroutine (Restarter ());
	}

	IEnumerator Restarter ()
	{
		yield return new WaitForSeconds (1);
		if (instance.player.m_Team != null)
		{
			if(GameManager.instance.playerSpawner == null)
				Debug.Log("lol");

			GameManager.instance.player.SetTeam(currentTeam);

			if(!(GameManager.instance.playerSpawner.playerReference.transform.position == GameManager.instance.playerSpawner.spawnPointBlue.position )&&
			   !(GameManager.instance.playerSpawner.playerReference.transform.position == GameManager.instance.playerSpawner.spawnPointRed.position))
			{
				// Not in the right position;
				Debug.Log("position: "+ GameManager.instance.player.transform.position);
				GameManager.instance.playerSpawner.SetCurrentSpawnPoint();
			}
			Debug.Log ("Already Exist!");
		}	}
}
