using UnityEngine;
using System.Collections;

public class SpellCollision : MonoBehaviour {
	
	public int id;
	public elementType elementType;
	float time;

	PhotonView photonView;
	
	#region enums
	public enum Consequence
	{
		BreakShield,
		DispelShield,
		DirectHit,
		BreakMissile,
		AbsorbShield,
		PushedBack,
		FullBlock,
		Stopped,
		Loss,
		None
	}
	#endregion

	void Start(){
		time = Time.time;
	}

	void OnTriggerEnter(Collider other){

		photonView = gameObject.GetComponent<PhotonView> ();

//		Debug.Log ("OnTriggerEnter");
		if (other.tag == "Spell") {
			elementType otherElement = other.GetComponent<SpellCollision>().elementType; 
			string element = CreateString(elementType, true);
			string elementOpposing = CreateString(otherElement, true);
			WeAreColliding (element, elementOpposing);
		}
		if (other.tag == "Shield") {
			elementType otherElement = other.GetComponent<ShieldElement>().elementType;
			string element = CreateString(elementType, false);
			string elementOpposing = CreateString(otherElement, false);
			WeAreColliding (element, elementOpposing);
		}
		if (other.tag == "PlayerCollision"){// && Time.time + 1f > time) {
			// score
			if (photonView.isMine) {
				GameManager.instance.player.RemoveSpell(id);
				Debug.Log("Removed MY SPELL");
			}
		}
	}
	
	string CreateString (elementType Element, bool isSpell){
		string e = Element.ToString ();
		if (e.Contains ("Fire")) {
			e = e.Replace ("Fire", "F");
		}
		if (e.Contains ("Air")) {
			e = e.Replace ("Air", "A"); 
		}
		if (e.Contains ("Water")) {
			e = e.Replace ("Water", "W");
		}
		if (e.Contains ("Earth")) {
			e = e.Replace ("Earth","E");
		}
		switch (Element) {
		case elementType.Air:
			e = e.Insert(1, "0");
			break;
		case elementType.Earth:
			e = e.Insert(1, "0");
			break;
		case elementType.Fire:
			e = e.Insert(1, "0");
			break;
		case elementType.Water:
			e = e.Insert(1, "0");
			break;
		default:
			break;
		}
		if (isSpell) {
			e = e.Insert(2,"_M");
		} else {
			e = e.Insert(2,"_S");
		}
		
		return e;
	}
	
	#region We Are Collising
	public void WeAreColliding (string spellIdentifier, string opposingSpellIdentifier)
	{
//		Debug.Log ("WeAreColldiding");
		switch (CalculateConsequence(spellIdentifier, opposingSpellIdentifier))
		{
		case Consequence.AbsorbShield:
			IAbsorbedASpell();
			break;
		case Consequence.BreakMissile:
			IBrokeAMissile();
			break;
		case Consequence.BreakShield:
			IBrokeAShield();
			break;
		case Consequence.DirectHit:
			IDirectlyHitSomeone();
			break;
		case Consequence.DispelShield:
			IDispelledAShield();
			break;
		case Consequence.FullBlock:
			IBlockedSomething();
			break;
		case Consequence.Loss:
			ILost();
			break;
		case Consequence.PushedBack:
			IAmPushedBack();
			break;
		case Consequence.Stopped:
			IAmStopped();
			break;
		case Consequence.None:
		default:
			//Do Nothing. Seriously... nothing happened... continue as planned!
			break;
		}
	}
	#endregion
	
	#region Calculate Response
	private Consequence CalculateConsequence (string spellIdentifier, string opposingSpellIdentifier)
	{
		switch (spellIdentifier)
		{
			#region Fire Missile
		case "F0_M":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
			case "F0_S":
			case "W0_M":
			case "W0_S":
				return Consequence.Loss;
			case "A0_M":
				return Consequence.PushedBack;
			case "A0_S":
				return Consequence.DirectHit;
			case "E0_M":
				return Consequence.BreakMissile;
			case "E0_S":
				return Consequence.DirectHit;
			default:
				return Consequence.None;
			}
			#endregion
			#region Fire Shield
		case "F0_S":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
				return Consequence.AbsorbShield;
			case "A0_M": 
				return Consequence.None;
			case "W0_M":
				return Consequence.Loss;
			case "E0_M":
				return Consequence.FullBlock;
			default:
				return Consequence.None;
			}
			#endregion
			#region Air Stream 
		case "A0_M":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
			case "F0_S":
			case "A0_S":
			case "W0_M":
			case "E0_S":
				return Consequence.None;
			case "A0_M":
				return Consequence.Stopped;
			case "W0_S":
				return Consequence.DispelShield;
			case "E0_M":
				return Consequence.PushedBack;
			default:
				return Consequence.None;
			}
			#endregion
			#region Air Shield
		case "A0_S":
			switch(opposingSpellIdentifier)
			{
			case "F0_M":
				return Consequence.None;
			case "A0_M":
				return Consequence.AbsorbShield;
			case "W0_M":
				return Consequence.FullBlock;
			case "E0_M":
				return Consequence.Loss;
			default:
				return Consequence.None;
			}
			#endregion
			#region Water Stream
		case "W0_M":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
				return Consequence.BreakMissile;
			case "F0_S":
				return Consequence.DispelShield;
			case "A0_M":
				return Consequence.PushedBack;
			case "A0_S":
			case "W0_S":
			case "E0_M":
			case "E0_S":
				return Consequence.None;
			case "W0_M":
				return Consequence.Stopped;
			default:
				return Consequence.None;
			}
			#endregion
			#region Water Shield
		case "W0_S":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
				return Consequence.FullBlock;
			case "A0_M":
				return Consequence.Loss;
			case "W0_M":
				return Consequence.AbsorbShield;
			case "E0_M":
			default:
				return Consequence.None;
			}
			#endregion
			#region Earth Missile
		case "E0_M":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
			case "F0_S":
			case "E0_M":
				return Consequence.Loss;
			case "A0_S":
			case "W0_S":
				return Consequence.DirectHit;
			case "W0_M":
				return Consequence.PushedBack;
			case "E0_S":
				return Consequence.DispelShield;
			case "A0_M":
			default:
				return Consequence.None;
			}
			#endregion 
			#region Earth Shield
		case "E0_S":
			switch (opposingSpellIdentifier)
			{
			case "F0_M":
			case "E0_M":
				return Consequence.Loss;
			case "A0_M":
				return Consequence.FullBlock;
			case "W0_M":
			default:
				return Consequence.None;
			}
			#endregion
		default:
			return Consequence.None; 
		}
	}
	#endregion
	
	#region Absorb Shield
	private void IAbsorbedASpell()
	{
		//Award points to the owner of this spell for absorbing a missile 
		//This object remains intact
	}
	#endregion
	#region Break Missile
	private void IBrokeAMissile()
	{
		//Award points to the owner of this spell for destroying a missile mid-flight
		//this object remains intact
	}
	#endregion
	#region Break Shield
	private void IBrokeAShield()
	{
		//Destroy this object
		//Destroy (gameObject);
		if (photonView.isMine) {
			GameManager.instance.player.RemoveSpell(id);
			Debug.Log("Removed MY SPELL");
		}
		//Award points to the owner of this spell for destroying a shield
	}
	#endregion
	#region Dispel Shield
	private void IDispelledAShield()
	{
		//Destroy this object
		//Destroy (gameObject);
		if (photonView.isMine) {
			GameManager.instance.player.RemoveSpell(id);
			Debug.Log("Removed MY SPELL");
		}
		//Award points to the owner of this spell for dispelling a shield
	}
	#endregion
	#region Direct Hit
	private void IDirectlyHitSomeone()
	{
		//Destroy this object
		//Destroy (gameObject);
		if (photonView.isMine) {
			GameManager.instance.player.RemoveSpell(id);
			Debug.Log("Removed MY SPELL");
		}
		//Award points to the owner of this spell for hitting another player
	}
	#endregion
	#region Full Block
	private void IBlockedSomething()
	{
		//Award points to the owner of this spell for blocking a missile
		//This object remains intact
	}
	#endregion
	#region Loss
	private void ILost()
	{
		//Destroy this object
		//Destroy (gameObject);
		if (photonView.isMine) {
			GameManager.instance.player.RemoveSpell(id);
			Debug.Log("Removed MY SPELL");
		}
		//No points are awarded this way; the destroying opposing spell does that
	}
	#endregion
	#region Pushed Back
	private void IAmPushedBack()
	{
		//This missile has a reversed movement direction.
		//This missile interacts with other object as if it was owned by the opposing player
		//This object remains intact
	}
	#endregion
	#region Stopped
	private void IAmStopped()
	{
		//This missile's movement is suspended for the duration of the collision
		//This object remains intact
	}
	#endregion
}
