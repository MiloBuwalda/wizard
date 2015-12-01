using UnityEngine;
using System.Collections;

public class ObserverPlayer : MonoBehaviour {

	public Transform observeOverview;
	public Transform observePlayerA;
	public Transform observePlayerB;

	private bool isObserver = false;

	public Transform currentObservePoint;

	// Use this for initialization
	void Start () {
		
	}


	public bool IsObserver {
		get {
			return isObserver;
		}
	}

	public void SetObserver(bool isObserver)
	{
		this.isObserver = isObserver;
	}
}
