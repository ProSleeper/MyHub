using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	PhotonView pv;

	public GameObject OtherWait;
	public GameObject OtherConnected;
	public GameObject BackButton;
	public GameObject GameStart;
	public Text Win;
	public Text Lose;


	// Use this for initialization
	void Start () {
		pv = this.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        

        if (PhotonNetwork.room.PlayerCount == 1)        //방을 생성함
        {
            Debug.Log("RoomMake");
			OtherWait.SetActive(true);
			PhotonNetwork.playerName = "Maker";
        }
        else if (PhotonNetwork.room.PlayerCount == 2)   //방에 입장함
        {
            Debug.Log("RoomJoin");
			OtherWait.SetActive(true);
			PhotonNetwork.playerName = "Joiner";
        }
        //Debug.Log(PhotonNetwork.player.UserId);
        //Debug.Log(PhotonNetwork.inRoom);
        //GameObject.Find("ChatManager").GetComponent<ChatManager>().SendSystemMessage("\n상대방이 들어왔습니다.");

		Debug.Log(PhotonNetwork.playerName);
    }

    void OnPhotonPlayerConnected()
    {
        Debug.Log("PlayerJoin");
		OtherWait.SetActive(false);
		OtherConnected.SetActive(true);
    }

    void OnPhotonPlayerDisconnected()
    {
		Debug.Log("PlayerJoin(Lobby&Quit");
		OtherWait.SetActive(true);
    }
    
}
