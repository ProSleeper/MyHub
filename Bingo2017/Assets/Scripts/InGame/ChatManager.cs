using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{

    private PhotonView chatServer;
    public Text chatWindow;
    public InputField chatInput;
    public Button send;

    void Awake()
    {
        chatServer = GetComponent<PhotonView>();
    }

    // Use this for initialization
    void Start()
    {
        send.onClick.AddListener(SendChatMessage);
        //Debug.Log(PhotonNetwork.isMasterClient);
    }

    void SendChatMessage()
    {
        string msg = "\n" + chatInput.text;
        if (chatInput.text != "")
        {
            chatServer.RPC("LogMsg", PhotonTargets.All, msg);
            chatInput.text = "";
        }
    }

	// public void JoinOther()
	// {
    //     SendSystemMessage("\n상대방이 들어왔습니다.");
	// }

	// public void LeaveOther()
	// {
    //     SendSystemMessage("\n상대방이 나갔습니다.");
	// }

	public void SendSystemMessage(string message)
	{
		chatServer.RPC("LogMsg", PhotonTargets.All, message);
	}

    [PunRPC]
    void LogMsg(string msg)
    {
        chatWindow.text = chatWindow.text + msg;
	}
}
