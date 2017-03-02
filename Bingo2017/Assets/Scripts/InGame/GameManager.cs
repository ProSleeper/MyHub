using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    PhotonView GameMessage;
    GameObject BManager;
    bool isMyTurn;
    bool isGameOver;

    public GameObject[] Panel;

    //위 Panel 배열 실제 오브젝트
    // public GameObject GamePanel;
    // public GameObject OtherWait;
    // public GameObject OtherConnected;
    // public GameObject BackButton;
    // public GameObject GameStart;
    // public GameObject Win;
    // public GameObject Lose;
    // public GameObject MyTurn;
    // public GameObject OtherTurn;
    // public GameObject RoomName;

    void Start()
    {
        GameMessage = this.GetComponent<PhotonView>();
        BManager = GameObject.Find("BingoManager");
		isGameOver = false;
		Debug.Log(GameMessage.viewID);
    }

    public void PlayGame()
    {
        Debug.Log("play");
        SetPlayGame();
        GameMessage.RPC("sendMessage", PhotonTargets.Others);
    }

    [PunRPC]
    void sendMessage()
    {
        OtherPlayGame();
    }

    public void OtherPlayGame()
    {
        Debug.Log("get");
        SetPlayGame();
    }

	public bool getIsMyTurn()
	{
		return isMyTurn;
	}

    public void SetTurn()
    {
        if (PhotonNetwork.playerName == "Maker")
        {
            MyTurn();
            return;
        }
        OtherTurn();
    }

    public void SendTurn()
    {
        
        if (!isGameOver)
        {
            Debug.Log("게임종료 아니고 턴넘김");
            GameMessage.RPC("MyTurn", PhotonTargets.Others);
		    OtherTurn();
        }
        else
        {
            Debug.Log("게임종료 일때 턴넘김");
        }
        
    }

    [PunRPC]
    void MyTurn()
    {
        if (!isGameOver)
        {
            Debug.Log("게임종료 아니고 나의턴");
            isMyTurn = true;
            InitPlay();
			Panel[0].SetActive(true);
            Panel[7].SetActive(true);
            Invoke("InitPlay", 1.0f);
        }
        else
        {
            Debug.Log("게임종료 일때 나의턴");
        }
        
    }

    void OtherTurn()
    {
       
        if (!isGameOver)
        {
            Debug.Log("게임종료 아니고 상대턴");
            isMyTurn = false;
            InitPlay();
            Panel[0].SetActive(true);
            Panel[8].SetActive(true);
        }
        else
        {
            Debug.Log("게임종료 일때 상대턴");
        }
         
    }

    void SetPlayGame()
    {
        InitPlay();
        BManager.GetComponent<BingoManager>().InitGame();
		Panel[0].SetActive(true);
        Panel[4].SetActive(true);
    }

    public void InitPlay()
    {
        Panel[0].SetActive(false);
        Panel[1].SetActive(false);
        Panel[2].SetActive(false);
        Panel[3].SetActive(false);
        Panel[4].SetActive(false);
        Panel[5].SetActive(false);
        Panel[6].SetActive(false);
        Panel[7].SetActive(false);
        Panel[8].SetActive(false);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        Panel[9].GetComponent<Text>().text = PhotonNetwork.room.Name;
		PhotonNetwork.isMessageQueueRunning = true;

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
        InitPlay();
        isGameOver = false;
        Panel[0].SetActive(true);
        Panel[2].SetActive(true);
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.playerName = "Maker";
    }

    void OnPhotonPlayerDisconnected()
    {
        Debug.Log("PlayerJoin(Lobby&Quit)");
        InitPlay();
        Panel[0].SetActive(true);
        Panel[1].SetActive(true);
        PhotonNetwork.room.IsVisible = true;
        PhotonNetwork.room.IsOpen = true;
        PhotonNetwork.playerName = "Maker";
    }

    public void GameWin()
    {
        Debug.Log("승리");
        isGameOver = true;
        InitPlay();
        Panel[0].SetActive(true);
        Panel[5].SetActive(true);
        //maker 와 joiner의 차이를 판단해서 코드 작성!
        MakerJoiner(PhotonNetwork.playerName);
    }

    public void GameLose()
    {
        Debug.Log("패배");
        isGameOver = true;
        InitPlay();
        Panel[0].SetActive(true);
        Panel[6].SetActive(true);
        MakerJoiner(PhotonNetwork.playerName);
    }

    void MakerJoiner(string div)
    {
        if (PhotonNetwork.playerName == "Maker")
        {
            Invoke("OnPhotonPlayerConnected", 6.0f);
            return;
        }
        Invoke("JoinerWait", 6.0f);
    }

    void JoinerWait()
    {
        InitPlay();
        isGameOver = false;
        Panel[0].SetActive(true);
        Panel[1].SetActive(true);
        PhotonNetwork.room.IsVisible = true;
        PhotonNetwork.room.IsOpen = true;
    }
}
