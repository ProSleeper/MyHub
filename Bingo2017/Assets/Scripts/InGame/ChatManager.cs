using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {

	private PhotonView server;
	public Text chatWindow;
	public InputField chatInput;
	public Button Send;


	void Awake()
	{
		server = GetComponent<PhotonView>();
	}

	// Use this for initialization
	void Start () {
		Send.onClick.AddListener(SendChatMessage);
	}
	
	void SendChatMessage()
	{
		string msg = "\n" + chatInput.text;
		if (chatInput.text != "")
		{
			server.RPC("LogMsg", PhotonTargets.All, msg);
			chatInput.text = "";
		}
	}
	
	[PunRPC]
	void LogMsg(string msg)
	{
		chatWindow.text = chatWindow.text + msg;
	}
}
