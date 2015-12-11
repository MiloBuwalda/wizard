using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkController : Photon.MonoBehaviour
{
	string _versionNumber = "0.1"; 
	string _room = "room_01";
	string _levelName; //"DemoShooting";

	public Transform spawnPoint;


	public GameObject observer;
	public GameObject playerSpawner;
	public GameObject OVRplayer;
	 
//	public GameObject playerReference;


	public bool isConnected = false;

	
	public static bool isHost = false;
	public static bool QuitOnLogout = false;


	public static bool IsConnected
	{
		get
		{
			return PhotonNetwork.offlineMode == false && PhotonNetwork.connectionState == ConnectionState.Connected;
		}
	}
	
	void Start()
	{
		_levelName = Application.loadedLevelName;
		DontDestroyOnLoad (gameObject);
//
//		PhotonNetwork.ConnectUsingSettings(_versionNumber);
		Connect ();
		Debug.Log ("NetworkController Started");
	}

	public void Connect()
	{
		Debug.Log("Connect");
		if (PhotonNetwork.connectionState != ConnectionState.Disconnected) {
			return;
		}

		try
		{
			PhotonNetwork.ConnectUsingSettings(_versionNumber);
		}
		catch
		{
			Debug.LogWarning ("Couldn't connect to server");
		}
		
	}

	void OnConnectedToPhoton()
	{
		if( isHost == true )
			return;
	}

	/// <summary>
	/// When we joined the lobby after connecting to Photon, we want to immediately join the demo room, or create it if it doesn't exist
	/// </summary>
	void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions (){};
		PhotonNetwork.JoinOrCreateRoom (_room, roomOptions, TypedLobby.Default);
		Debug.Log ("Starting Server");
//		if( isHost == true )
//			return;
//		
//		if( QuitOnLogout == true )
//		{
//			Application.Quit();
//			return;
//		}
//		
//		if( Application.loadedLevelName == _levelName )
//		{
//			RoomOptions roomOptions = new RoomOptions();
//			roomOptions.maxPlayers = 20;
//			
//			PhotonNetwork.JoinOrCreateRoom( _room, roomOptions, TypedLobby.Default );
//			Debug.Log( "Joined Lobby" );
//		}
//		else
//		{
//			//If we join the lobby while not being in the MainMenu scene, something went wrong and we disconnect from Photon
//			PhotonNetwork.Disconnect();
//		}
	}



	void OnJoinedRoom()
	{
		isConnected = true;
		Debug.Log ("Network Controller/PhotonNetwork.playerList.Length: "+ PhotonNetwork.playerList.Length);

		// When a"Player" is spawned on the network use OnPhotonInstantiate inside Player 
		Debug.Log ("PhotonNetwork.playerList.Length");
		// Create Observer if 2 players are already in the game
		if (PhotonNetwork.playerList.Length > 2) {
			SetObserver();
		}
		if (playerSpawner != null) {
			if(PhotonNetwork.playerList.Length == 1)
				GameManager.instance.player.SetTeam(Team.Blue);
			else
				GameManager.instance.player.SetTeam(Team.Red);
		}
		Debug.Log("playerSpawnRed: " + GameManager.instance.playerSpawner.spawnPointRed.position);
		Debug.Log("playerSpawnBlue: " + GameManager.instance.playerSpawner.spawnPointBlue.position);
//		PlayerSpawner ps = new PlayerSpawner ();
//		ps.CreateNetworkedPlayer ();
//		PhotonNetwork.Instantiate (
//			playerReference.name, 
//			spawnPoint.position, 
//			Quaternion.identity, 
//			0);
		Debug.Log ("Joined Room");
//		PhotonNetwork.isMessageQueueRunning = false;
//		Application.LoadLevel ("Level");
	}


	void OnPhotonCreateRoomFailed()
	{
		if( isHost == true )
			return;
	}
	
	void OnPhotonJoinRoomFailed()
	{
		if( isHost == true )
			return;
	}


	/// <summary>
	/// Called when we created a Photon room.
	/// </summary>
	void OnCreatedRoom()
	{
		Debug.Log( "OnCreatedRoom" );
		
		//When we create the room we set several custom room properties that get synchronized between all players
		ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
		
//		//If we don't set the scores to 0 here, players would get errors when trying to access the score properties
//		properties.Add( RoomProperty.BlueScore, (int)0 );
//		properties.Add( RoomProperty.RedScore, (int)0 );
		
		//PhotonNetwork.time is synchronized between all players, so by using it as the start time here, all players can calculate how long the game ran
		properties.Add( RoomProperty.StartTime, PhotonNetwork.time );
		
		PhotonNetwork.room.SetCustomProperties( properties );
	}

	/// <summary>
	/// Called by Unity after Application.LoadLevel is completed
	/// </summary>
	/// <param name="level">The index of the level that was loaded</param>
	void OnLevelWasLoaded( int level )
	{
		//Resume the Photon message queue so we get all the updates.
		//All updates that were sent during the level load were cached and are dispatched now so we can handle them properly.
		PhotonNetwork.isMessageQueueRunning = true;
	}
	
	void OnDisconnectedFromPhoton()
	{
//		if( Application.loadedLevelName != _levelName )
//		{
//			Application.LoadLevel( _levelName );
//		}
	}
	
	void OnFailedToConnectToPhoton( DisconnectCause cause )
	{
		if( isHost == true )
		{
			return;
		}
		
		Debug.LogWarning( "OnFailedToConnectToPhoton: " + cause );
	}
	
	void OnConnectionFail( DisconnectCause cause )
	{
		if( isHost == true )
		{
			return;
		}
		
		Debug.LogWarning( "OnConnectionFail: " + cause );
	}
	
	void OnLeftRoom()
	{
		if( isHost == true )
		{
			return;
		}
		
		Debug.Log( "OnLeftRoom" );
	}
	
	public static NetworkController instance;
	public static NetworkController Instance
	{
		get
		{
			if( instance == null )
			{
				CreateInstance();
			}
			
			return instance;
		}
	}
	
	public static void CreateInstance()
	{
		if( instance == null )
		{
			GameObject connectorObject = GameObject.Find( "NetworkController" );
			
			if( connectorObject == null )
			{
				connectorObject = new GameObject( "NetworkController" );
				connectorObject.AddComponent<NetworkController>();
			}

			instance = connectorObject.GetComponent<NetworkController>();
		}
	}

	// only if someone doesn't have an OVR on his head
	void SetObserver()
	{

		// Turn of game functionality
		GameManager.instance.enabled = false;


//		float fadeSpeed = 1.5f;
//		Color fadeColor = new Color (0.01f, 0.01f, 0.01f);

//		bool starting = true;

//		Transform leftEye;
//		Transform rightEye;

		// If someone has the Rift on, no need to switch to a normal camera.
		if (Ovr.Hmd.Detect () > 0) {
		
//			PERHAPS TO EASE THE TRANSITION MAKE A FADER

//			leftEye = OVRplayer.transform.Find("LeftEyeAnchor");
//			rightEye = OVRplayer.transform.Find("RightEyeAnchor");
//
//			OVRScreenFade screenFaderLeft = leftEye.gameObject.AddComponent<OVRScreenFade>();
//			OVRScreenFade screenFaderRight = rightEye.gameObject.AddComponent<OVRScreenFade>();

			// However, his or her position and orientation has to be set to the observers'

//			screenFaderLeft.fadeColor = fadeColor;
//			screenFaderLeft.fadeTime = fadeSpeed;
//			screenFaderRight.fadeColor = fadeColor;
//			screenFaderRight.fadeTime = fadeSpeed;

//			screenFaderLeft.

			// Switch positions
			OVRplayer.transform.position = observer.transform.position;
			OVRplayer.transform.rotation = observer.transform.rotation;

			OVRplayer.GetComponent<CharacterController>().enabled = false;
			OVRplayer.GetComponent<OVRPlayerController>().enabled = false;

			return;
		}

		OVRplayer.SetActive (false);
		observer.SetActive (true);

	}

//	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
//	{
//		if(PhotonNetwork.playerList.Length == 3)
//		{
//
//		}
//	}






}