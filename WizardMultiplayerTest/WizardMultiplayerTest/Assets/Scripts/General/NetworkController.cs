using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	string _room = "Wizard";
	string _levelName = "Demo";

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
		DontDestroyOnLoad (gameObject);

		Debug.Log ("NetworkController Started");
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	public void Connect()
	{
		if (PhotonNetwork.connectionState != ConnectionState.Disconnected)
		    return;


		try
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
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
		if( isHost == true )
			return;
		
		if( QuitOnLogout == true )
		{
			Application.Quit();
			return;
		}
		
		if( Application.loadedLevelName == _levelName )
		{
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.maxPlayers = 20;
			
			PhotonNetwork.JoinOrCreateRoom( "Wizard", roomOptions, TypedLobby.Default );
			Debug.Log( "Joined Lobby" );
		}
		else
		{
			//If we join the lobby while not being in the MainMenu scene, something went wrong and we disconnect from Photon
			PhotonNetwork.Disconnect();
		}
	}



	void OnJoinedRoom()
	{
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
		if( Application.loadedLevelName != _levelName )
		{
			Application.LoadLevel( _levelName );
		}
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







}