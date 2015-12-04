using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
//	FMOD.Studio.EventInstance engine;
//	FMOD.Studio.ParameterInstance engineRPM;
	FMOD.Studio.EventInstance background;

	void Start()
	{
//		engine = FMOD_StudioSystem.instance.getEvent("/Background/Orchestral Music");
//		engine.start();
//		engine.getParameter("RPM", out engineRPM);
		background = FMOD_StudioSystem.instance.GetEvent ("event:/Background/Orchestral Music");
		background.start ();
	}
	void Update()
	{
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
