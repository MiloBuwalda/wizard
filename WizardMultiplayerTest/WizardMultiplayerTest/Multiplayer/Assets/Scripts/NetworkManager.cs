using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";

	public Canvas canvas;
	public GameObject joinServerPanel;

	public Button startServerButton;
	public Button refreshHotlistButton;
	public Button joinServerButton;

	private bool showStartServerButton = true;
	private bool showRefreshHotlistButton = true;
	private bool showJoinServerButton = true;

	private HostData[] hostList;
	
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
			toggleButton( ref startServerButton, ref showStartServerButton);
			toggleButton();
		}
	}
	
	void instantiateButton()
	{
		if (canvas != null)
		if (!Network.isClient && !Network.isServer) {
			Debug.Log ("here");
			startServerButton.onClick.AddListener(() => StartServer());
			refreshHotlistButton.onClick.AddListener(() => RefreshHostList());
			for(int i = 0 ; i < hostList.Length; i++)
			{
				Button joinServerButton = Instantiate
				addButtonToElement(joinServerPanel, 
				JoinServer(hostList[i]);		
			}
			joinServerButton.onClick.AddListener(() => JoinAllServers());
//			joinServerButton.onClick.AddListener(() => JoinServer (
//			startServerButton.transform.SetParent(canvas.transform, false);
		}
	} 

	private void addButtonToElement(GameObject panel, ref Button btn)
	{

		btn.transform.SetParent (panel.transform, false);
	}
	

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
//		toggleButton (refreshHotlistButton);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}

	void toggleButton(ref Button button, ref bool showButtonToggle)
	{
		showButtonToggle = !showButtonToggle;
		button.gameObject.SetActive(showButtonToggle);
	}

	void addButton()
	{
		networkInitializerButton.onClick.AddListener(() => StartServer());
		networkInitializerButton.transform.SetParent(canvas.transform, false);
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