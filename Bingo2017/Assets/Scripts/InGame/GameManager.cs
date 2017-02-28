using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

	PhotonView GameMessage;
	GameObject BManager;

	public GameObject[] Panel;

	//위 Panel 배열 실제 오브젝트
	// public GameObject GamePanel;
	// public GameObject OtherWait;
	// public GameObject OtherConnected;
	// public GameObject BackButton;
	// public GameObject GameStart;
	// public GameObject Win;
	// public GameObject Lose;

	void Start () {
		GameMessage = this.GetComponent<PhotonView>();
		BManager = GameObject.Find("BingoManager");
	}
	
	public void PlayGame()
	{
		Debug.Log("play");
		BManager.GetComponent<BingoManager>().InitGame();
		Panel[4].SetActive(true);
		GameMessage.RPC("sendMessage", PhotonTargets.Others);
		
	}

	public void OtherPlayGame()
	{
		Debug.Log("get");
		BManager.GetComponent<BingoManager>().InitGame();
		Panel[4].SetActive(true);
	}

	public void SetPlay()
	{
		Panel[0].SetActive(false);
		Panel[1].SetActive(false);
		Panel[2].SetActive(false);
		Panel[3].SetActive(false);
		Panel[4].SetActive(false);
		Panel[5].SetActive(false);
		Panel[6].SetActive(false);
	}
	
	[PunRPC]
    void sendMessage()
    {
        OtherPlayGame();
	}


	void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        
        if (PhotonNetwork.room.PlayerCount == 1)        //방을 생성함
        {
            Debug.Log("RoomMake");
			Panel[1].SetActive(true);
			PhotonNetwork.playerName = "Maker";
        }
        else if (PhotonNetwork.room.PlayerCount == 2)   //방에 입장함
        {
            Debug.Log("RoomJoin");
			Panel[1].SetActive(true);
			PhotonNetwork.playerName = "Joiner";
        }

		Debug.Log(PhotonNetwork.playerName);
		Debug.Log(PhotonNetwork.inRoom);
    }

    void OnPhotonPlayerConnected()
    {
        Debug.Log("PlayerJoin");
		SetPlay();
		Panel[0].SetActive(true);
		Panel[2].SetActive(true);
		PhotonNetwork.room.IsVisible = false;
		PhotonNetwork.room.IsOpen = false;
		PhotonNetwork.playerName = "Maker";
    }

	void OnPhotonPlayerDisconnected()
    {
        Debug.Log("PlayerJoin(Lobby&Quit)");
		SetPlay();
		Panel[0].SetActive(true);
		Panel[1].SetActive(true);
		PhotonNetwork.room.IsVisible = true;
		PhotonNetwork.room.IsOpen = true;
		PhotonNetwork.playerName = "Maker";
    }

	public void GameWin()
	{
		SetPlay();
		Panel[0].SetActive(true);
		Panel[5].SetActive(true);
		//maker 와 joiner의 차이를 판단해서 코드 작성!
		Invoke("OnPhotonPlayerConnected", 3.0f);
	}

	public void GameLose()
	{
		SetPlay();
		Panel[0].SetActive(true);
		Panel[6].SetActive(true);
		Invoke("OnPhotonPlayerConnected", 3.0f);
	}
}
