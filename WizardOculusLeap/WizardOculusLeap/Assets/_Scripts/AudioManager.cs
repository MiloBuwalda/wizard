using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
//	FMOD.Studio.EventInstance engine;
//	FMOD.Studio.ParameterInstance engineRPM;
	FMOD.Studio.EventInstance background;
	public bool play = false;

	void Start()
	{
//		engine = FMOD_StudioSystem.instance.getEvent("/Background/Orchestral Music");
//		engine.start();
//		engine.getParameter("RPM", out engineRPM);
		background = FMOD_StudioSystem.instance.GetEvent ("event:/Background/Orchestral Music");
		if(play)
			background.start ();
	}
	void Update()
	{
		if (!play) {
			background.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			background.release ();
		}
		// get a RPM value from the game's car engine
//		engineRPM.setValue(rpm);
	}
	void OnDisable()
	{
//		engine.stop();
//		engine.release();
	}
	public void OneShot(elementType element)
	{
//		FMOD_StudioSystem.instance.PlayOneShot("snapshot:/Spells/Fire", transform.position);
	}
}
