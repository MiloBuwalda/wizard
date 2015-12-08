using UnityEngine;
using System.Collections;

public class ShieldElement : MonoBehaviour {

	public int id;

	public elementType elementType;

	public Transform player;

	void Awake (){
		player = GameObject.Find ("LeapOVRCameraRig").transform;
	}

	void Start (){
		transform.LookAt (player.position);
	}
}
