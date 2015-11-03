using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List <ElementManager> elementPool;
	public List <SpellManager> spellPool;
	public List <ShieldManager> shieldPool;

	void Start () {
		elementPool = new List<ElementManager>();
		spellPool = new List<SpellManager>();
		shieldPool = new List<ShieldManager>();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			AddElementToPool(elementType.Air);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			AddElementToPool(elementType.Earth);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			AddElementToPool(elementType.Fire);
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			AddElementToPool(elementType.Water);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			ExecuteSpell ();
		}
	}

	public void AddElementToPool(elementType t)
	{
		ElementManager el = ElementSpawner.instance.GetElementOfType (t);
		elementPool.Add (el);
	}

	void EmptyElementPool()
	{
		foreach (ElementManager element in elementPool)
		{
			Destroy(element.instance);
		}
		elementPool.Clear ();
	}

	//wanneer afschiet beweging
	void ExecuteSpell(){
		SpellManager spell = SpellSpawner.instance.CreateSpell (elementPool);
		if (spell != null) 
		{
			spellPool.Add(spell);
			EmptyElementPool();
		}
	}

	void ExecuteShield(){
		ShieldManager shield = ShieldSpawner.instance.CreateShield (elementPool);
		if (shield != null) 
		{
			shieldPool.Add(shield);
			EmptyElementPool();
		}
	}

}
