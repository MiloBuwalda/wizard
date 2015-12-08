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
	}
}
