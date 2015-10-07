﻿using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	string _room = "Wizard";
	
	void Start()
	{
		Debug.Log ("NetworkController Started");
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnJoinedLobby()
	{
		Debug.Log("joined lobby");
		
		RoomOptions roomOptions = new RoomOptions() { };
		PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
	}
	
	void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
	}
}