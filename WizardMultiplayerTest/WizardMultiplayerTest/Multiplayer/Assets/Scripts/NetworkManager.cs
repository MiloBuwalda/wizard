using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";

	public Canvas canvas;
	public Button networkInitializerButton;

	private bool showButton = true;

	void Start()
	{
		instantiateButton ();
	}

	void Update()
	{

	}

	public void StartServer()
	{
		MasterServer.ipAddress = "127.0.0.1";
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		Debug.Log("isclient: " + Network.isClient);
		Debug.Log ("isserver: " + Network.isServer); 
		if (Network.isClient || Network.isServer) {
			toggleButton();
		}
	}
	
	void instantiateButton()
	{
		if (canvas != null)
		if (!Network.isClient && !Network.isServer) {
			Debug.Log ("here");
			networkInitializerButton.onClick.AddListener(() => StartServer());
			networkInitializerButton.transform.SetParent(canvas.transform, false);
		}
	} 

	void toggleButton()
	{
		showButton = !showButton;
		networkInitializerButton.gameObject.SetActive(showButton);
	}
	// Legacy! 

	// ongui()
//	{
//		if (!Network.isClient && !Network.isServer)
//		{
//			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
//				StartServer();
//		}
//	}

}