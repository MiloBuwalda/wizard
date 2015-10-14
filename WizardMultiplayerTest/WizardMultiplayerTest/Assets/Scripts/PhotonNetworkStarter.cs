using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotonNetworkStarter : MonoBehaviour {

	public Canvas canvas;

	private Text txt;

	// Use this for initialization
	void Start () {
		txt = canvas.GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
//		txt.CrossFadeAlpha (Mathf.Sin (Time.deltaTime), 1, false);
		if (PhotonNetwork.connectionState == ConnectionState.Disconnected && Input.GetKeyDown (KeyCode.P) == true) {
			NetworkController.Instance.Connect();
			canvas.gameObject.SetActive (false);
		}
	}
}
