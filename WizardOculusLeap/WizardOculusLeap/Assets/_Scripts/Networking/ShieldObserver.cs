using UnityEngine;
using System.Collections;

public class ShieldObserver : Photon.MonoBehaviour {
	
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

	void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info )
	{
		// THIS IS FROM THE SKY ARENA PHOTON TUTORIAL
		//Multiple components need to synchronize values over the network.
		//The SerializeState methods are made up, but they're useful to keep
		//all the data separated into their respective components
		
		SerializeState( stream, info );
//		
//		ShipVisuals.SerializeState( stream, info );
//		ShipMovement.SerializeState( stream, info );
	}
	public void SerializeState( PhotonStream stream, PhotonMessageInfo info )
	{
		//We only need to synchronize a couple of variables to be able to recreate a good
		//approximation of the ships position on each client
		//There is a lot of smoke and mirrors happening here
		//Check out Part 1 Lesson 2 http://youtu.be/7hWuxxm6wsA for more detailed explanations
		if( stream.isWriting == true )
		{
			stream.SendNext( transform.position );
			stream.SendNext( transform.rotation );
		}
		else
		{
			m_NetworkPosition = (Vector3)stream.ReceiveNext();
			m_NetworkRotation = (Quaternion)stream.ReceiveNext();

			m_LastNetworkDataReceivedTime = info.timestamp;
		}
	}
}
