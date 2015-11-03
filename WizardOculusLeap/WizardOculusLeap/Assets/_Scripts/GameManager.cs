using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public MovementManager movementManager;
	public Player player;
	public ElementSpawner elementSpawner;
	public SpellSpawner spellSpawner;
	public ShieldSpawner shieldSpawner;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	void Start(){
		movementManager = GetComponent<MovementManager> ();
		player = GetComponent<Player> ();
		elementSpawner = GetComponent<ElementSpawner> ();
		spellSpawner = GetComponent<SpellSpawner> ();
		shieldSpawner = GetComponent<ShieldSpawner> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F2)) {
			OVRManager.display.RecenterPose();
		}
	}
}
