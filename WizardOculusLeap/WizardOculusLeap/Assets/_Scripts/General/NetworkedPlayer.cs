using UnityEngine;
using System.Collections;

public class NetworkedPlayer : Photon.MonoBehaviour
{
	public GameObject avatar;
	
	public Transform playerGlobal;
	public Transform playerLocal;

	// Required for interpoliation
	private float lastSyncTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Vector3 syncStartOrientation = Vector3.zero;

	private float m_Speed;
	private float m_LastNetworkDataReceivedTime;
	private Vector3 m_NetworkPosition;

	void Start ()
	{
		Debug.Log("i'm instantiated");
		
		if (photonView.isMine) {
			Debug.Log ("player is mine");
			
			playerGlobal = GameObject.Find ("LeapOVRPlayerController").transform;
			playerLocal = playerGlobal.Find ("LeapOVRCameraRig/CenterEyeAnchor");
			
			this.transform.SetParent (playerLocal);
			this.transform.localPosition = Vector3.zero;
			
			// avatar.SetActive(false);
		} else {
			Debug.Log("Not mine"); 
		}
	}

	void Update(){
		// Update postion and orientation from network
		if (!photonView.isMine) {
			UpdateNetworkedPosition1();
		}
	}

	void UpdateNetworkedPosition1()
	{
		syncTime += Time.deltaTime;
		this.transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime/syncDelay);
	}

	void UpdateNetworkedPosition()
	{
		//Here we try to predict where the player actually is depending on the data we received through the network
		//Check out Part 1 Lesson 2 http://youtu.be/7hWuxxm6wsA for more detailed explanations
		float pingInSeconds = (float)PhotonNetwork.GetPing() * 0.001f;
		float timeSinceLastUpdate = (float)( PhotonNetwork.time - m_LastNetworkDataReceivedTime );
		float totalTimePassed = pingInSeconds + timeSinceLastUpdate;
		
		Vector3 exterpolatedTargetPosition = m_NetworkPosition
			+ transform.forward * m_Speed * totalTimePassed;
		
		
		Vector3 newPosition = Vector3.MoveTowards( transform.position
		                                          , exterpolatedTargetPosition
		                                          , m_Speed * Time.deltaTime );
		
		if( Vector3.Distance( transform.position, exterpolatedTargetPosition ) > 2f )
		{
			newPosition = exterpolatedTargetPosition;
		}
		
		newPosition.y = Mathf.Clamp( newPosition.y, 0.5f, 50f );
		
		transform.position = newPosition;
	}


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		// Sending to network (if this player is moving)
		if (stream.isWriting)
		{
			stream.SendNext(playerGlobal.position);
			stream.SendNext(playerGlobal.rotation);
			stream.SendNext(playerLocal.localPosition);
			stream.SendNext(playerLocal.localRotation);
		}
		// Recieving from network (must interpolate to get smooth values)
		else
		{
			syncEndPosition = (Vector3) stream.ReceiveNext();
			syncStartPosition = playerLocal.position;
			syncTime = 0f;
			syncDelay = Time.time - lastSyncTime;
			lastSyncTime = Time.time;

//			this.transform.position = (Vector3)stream.ReceiveNext();
			this.transform.rotation = (Quaternion)stream.ReceiveNext();
			avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
			avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
		}
	}
}