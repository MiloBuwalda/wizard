using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour {


	Text InfoScreenText;

	// Use this for initialization
	void Start () {
		Setup ();
		SetText ("Test");
	}

	// Update is called once per frame
	void Update () {
		DisplayUI ();
	}

	void DisplayUI()
	{
		if (Input.GetKeyDown (KeyCode.I)) {
			this.enabled = !this.enabled;
		}
	}

	void Setup()
	{
		if (InfoScreenText == null) {
			InfoScreenText = this.GetComponentInChildren<Text> ();
		}
		else if (InfoScreenText == null) {
			InfoScreenText = GameObject.Find("OutputWindow").GetComponent<Text>();
		}
	}

	void SetText(string textToSet)
	{
		InfoScreenText.text = textToSet;
	}
}

