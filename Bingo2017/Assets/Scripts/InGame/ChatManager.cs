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
    public RectTransform content;

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
        SoundOnOff.Instance.EffectPlay(4, 7);
        string UserType;
        if (PhotonNetwork.playerName == "Maker")
        {
            UserType = "M: ";
        }
        else
        {
            UserType = "J: ";
        }


        string msg = "\n" + UserType + chatInput.text;
        if (chatInput.text != "")
        {
            chatServer.RPC("LogMsg", PhotonTargets.All, msg);
            chatInput.text = "";
        }
    }

	public void JoinOther()
	{
        LogMsg("\n상대방이 들어왔습니다.");
	}

	public void LeaveOther()
	{
        LogMsg("\n상대방이 나갔습니다.");
	}

    [PunRPC]
    public void LogMsg(string msg)
    {
        chatWindow.text = chatWindow.text + msg;
        content.sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y + 110.0f);
	}
}
