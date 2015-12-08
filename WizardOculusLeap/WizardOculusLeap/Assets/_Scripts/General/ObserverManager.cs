using UnityEngine;
using System.Collections;


enum ObservationPoint 
{
	observeOverview,
	observePlayerA,
	observePlayerB,
}

public class ObserverManager : MonoBehaviour {

	public Transform[] observationPoints;

	public bool useObserver = false;

	public Transform currentObservationPoint;
	public Camera currentCamera;

	public int currentObservationIndex = 0;



	// Use this for initialization
	void Start () {

		if (observationPoints==null || observationPoints.Length == 0)
			throw new UnityException ("No observation points set!");

		currentObservationPoint = observationPoints [currentObservationIndex];

		if (currentCamera == null) {
			currentCamera = Camera.main;
			Debug.Log (currentCamera.isActiveAndEnabled);
//			currentCamera.GetComponent<Camera>().;
		}
	}

	void Update()
	{
		// On C go to next camera position
		switchCurrentObservationPoint ();

	}

	void switchCurrentObservationPoint()
	{
		if(Input.GetKeyDown(KeyCode.C))
	   	{
			currentObservationPoint = observationPoints[currentObservationIndex];
			setCameraLocationAndOrientation(currentObservationPoint);

			currentObservationIndex = (currentObservationIndex+1) % observationPoints.Length;
		}
	}

	void setCameraLocationAndOrientation(Transform desiredLocation)
	{
		// Instead of switching between camera's, 
		// we set the camera to the location/orientation of the parent. 
		currentCamera.transform.SetParent (desiredLocation);

		// reset local position and orientation because the location and orientation are set by parent
		currentCamera.transform.localPosition = Vector3.zero;
		currentCamera.transform.localRotation = Quaternion.identity;
	}



//	public bool IsObserver {
//		get {
//			return isObserver;
//		}
//	}
//
//	public void SetObserver(bool isObserver)
//	{
//		this.isObserver = isObserver;
//	}
}
