using UnityEngine;
using System.Collections;

public class SpellObserver : MonoBehaviour {

	/// <summary>
	/// This is the position where the network tells us the player is
	/// But since this is only updated 10 times a second, we store it so we can calculate the
	/// real position of a remote ship by applying it's known speed and turn angle
	/// </summary>
	Vector3 m_NetworkPosition;
	
	/// <summary>
	/// Same for the rotation. The ship should rotate smoothly, so we store the received value
	/// and interpolate towards it slowly to smoothen out any stutter
	/// </summary>
	Quaternion m_NetworkRotation;
	
	/// <summary>
	/// We need to know how old the last NetworkPosition and Rotation is so we can move the
	/// ship forward more, the older the data is
	/// </summary>
	double m_LastNetworkDataReceivedTime;
	
	/// <summary>
	/// Each shield has an owner.
	/// </summary>
	Player m_Owner;
	
	int m_ShieldId;

	Vector3 startPosition;

	float distance;

	float speed;
	float t = 0;
	float time;

	public bool isMine=false;
	PhotonView photonView;

	void Start()
	{
		photonView = gameObject.GetComponent<PhotonView> ();
		speed = gameObject.GetComponent<SpellMovement> ().speed;
		if (speed == 0)
			speed = 15f;
		m_NetworkPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
//		Debug.Log ("spellLocation: " + transform.position);  ////////////////////////////////

		if (!photonView.isMine) {
			if(time!=0)
				time = 0.01f;
			t += 1 / (Time.deltaTime * time);

 
//			transform.position = extendedLerp (startPosition, m_NetworkPosition, t); //DUNNO third argument
			transform.position = Vector3.Lerp(transform.position, m_NetworkPosition, .5f);
			transform.rotation = Quaternion.Slerp (transform.rotation, m_NetworkRotation, .5f);
		}
	}

	void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info )
	{
		// THIS IS FROM THE SKY ARENA PHOTON TUTORIAL
		//Multiple components need to synchronize values over the network.
		//The SerializeState methods are made up, but they're useful to keep
		//all the data separated into their respective components
		Debug.Log ("OnPhotonSerializeView");
		SerializeState( stream, info );
		//		
		//		ShipVisuals.SerializeState( stream, info );
		//		ShipMovement.SerializeState( stream, info );
	}
	public void SerializeState( PhotonStream stream, PhotonMessageInfo info )
	{
		Debug.Log ("Serialized State");
		//We only need to synchronize a couple of variables to be able to recreate a good
		//approximation of the ships position on each client
		//There is a lot of smoke and mirrors happening here
		//Check out Part 1 Lesson 2 http://youtu.be/7hWuxxm6wsA for more detailed explanations
		if( stream.isWriting == true )
		{
			Vector3 pos = transform.position;
			Quaternion rot = transform.rotation;
			stream.SendNext( pos );
			stream.SendNext( rot );
		}
		else
		{
			t=0;
			// achterlopende position naar nieuwe binnenkomende position lerpen
			startPosition = transform.position;
			m_NetworkPosition = (Vector3)stream.ReceiveNext();
			m_NetworkRotation = (Quaternion)stream.ReceiveNext();

			distance = Vector3.Distance(startPosition,m_NetworkPosition);
			time = distance/speed;
			m_LastNetworkDataReceivedTime = info.timestamp;
		}
	}

	public Vector3 extendedLerp(Vector3 start, Vector3 end, float t)
	{
		// Normally Lerp unity clamps, now we just continue
		return start + (end - start) * t;
	}
}
