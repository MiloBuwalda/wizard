using UnityEngine;
using System.Collections;

public class CamSwitch : MonoBehaviour {

	public Camera camMain;
	public Camera[] camAlt;
	private bool camMainOn = true;

	// Use this for initialization
	void Start () {
		camMain.enabled = camMainOn;
		camMain.gameObject.SetActive (camMainOn);

		int max = camAlt.Length;
		for (int i = 0; i < max; i++) {
			camAlt[i].enabled = !camMainOn;
			camAlt[i].gameObject.SetActive(!camMainOn);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.C)) {
			int max = camAlt.Length;
			camMain.enabled = !camMain.enabled;
			camMainOn = !camMainOn;

			for (int i = 0; i < max; i++) {
				camAlt[i].enabled = !camAlt[i].enabled;
				camAlt[i].gameObject.SetActive(!camMainOn);
			}
		}
	}
}
