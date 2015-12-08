using UnityEngine;
using System.Collections;

public class ShieldElement : MonoBehaviour {

	public int id;

	public elementType elementType;

	public Transform player;

	void Start (){
		player = GameObject.Find ("LeapOVRCameraRig").transform;
		transform.LookAt (player.position);
	}
}
