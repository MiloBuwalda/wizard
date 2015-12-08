using UnityEngine;
using System.Collections;

public class AudioState : MonoBehaviour {

	FMOD.Studio.EventInstance fmodSound;
	FMOD.Studio.ParameterInstance fmodState;
	public int state;
	
	void Start()
	{
//		engine = FMOD_StudioSystem.instance.getEvent("/Background/Orchestral Music");
//		engine.start();
//		engine.getParameter("RPM", out engineRPM);
		fmodSound = FMOD_StudioSystem.instance.GetEvent (gameObject.GetComponent<FMOD_StudioEventEmitter>().path);
		fmodSound.getParameter ("State", out fmodState);
	}
	void Update()
	{
		fmodState.setValue (state);
	}
	void OnDisable()
	{
//		engine.stop();
//		engine.release();
	}
}