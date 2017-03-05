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

    
    void Start()
    {
        send.onClick.AddListener(SendChatMessage);
        //Debug.Log(PhotonNetwork.isMasterClient);
    }

    //현재 방장인지 들어온 플레이어인지 판단해서 채팅구분
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

    //채팅 내용 보내는 함수
    [PunRPC]
    public void LogMsg(string msg)
    {
        chatWindow.text = chatWindow.text + msg;
        content.sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y + 110.0f);
	}
}
