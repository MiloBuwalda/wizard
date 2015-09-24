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

	public Button networkInitializerButton;

	private bool showStartServerButton = true;
//	private bool showRefreshHotlistButton = true;

	private HostData[] hostList;
	

//	private bool showButton = true;

	void Start()
	{
		instantiateButtons ();
	}

	void Update()
	{
		refreshServerList ();
	}

	public void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.ipAddress = "127.0.0.1";
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		Debug.Log("isclient: " + Network.isClient);
		Debug.Log ("isserver: " + Network.isServer); 
		if (Network.isClient || Network.isServer) {
			toggleButton( ref startServerButton, ref showStartServerButton);
//			toggleButton();
		}
	}
	
	void instantiateButtons()
	{
		if (canvas != null)
		if (!Network.isClient && !Network.isServer) {
			Debug.Log ("Instantiate Buttons");
			startServerButton.onClick.AddListener(() => StartServer());
			refreshHotlistButton.onClick.AddListener(() => {RefreshHostList();});
//			joinServerButton.onClick.AddListener(() => JoinAllServers());
//			joinServerButton.onClick.AddListener(() => JoinServer (
//			startServerButton.transform.SetParent(canvas.transform, false);
		}
	} 

	void refreshServerList()
	{
		Debug.Log ("RefreshServerList");
		if (canvas != null)
		if (!Network.isClient && !Network.isServer) {
			for(int i = 0 ; i < hostList.Length; i++)
			{
				Button joinServerButtonX = Instantiate(joinServerButton) as Button;
				joinServerButtonX.GetComponentsInChildren<Text>()[0].text = hostList[i].gameName;
				joinServerButtonX.transform.SetParent(joinServerPanel.transform, false);
//				addButtonToElement(joinServerPanel, ref joinServerButtonX);
				joinServerButtonX.onClick.AddListener(() => JoinServer (hostList[1]));
			}
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
		if (msEvent == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList ();
			Debug.Log("hostList is set");
			Debug.Log("length: " +	hostList.Length);
		}
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

//	void addButton()
//	{
//		networkInitializerButton.onClick.AddListener(() => StartServer());
//		networkInitializerButton.transform.SetParent(canvas.transform, false);
//	} 
//
//	void toggleButton()
//	{
//		showButton = !showButton;
//		networkInitializerButton.gameObject.SetActive(showButton);
//	}
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