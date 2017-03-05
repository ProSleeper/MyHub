using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//인게임 씬에서 사용되는 Play toLobby 등등 여러가지 UI적 의사소통 관련된 코드


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    PhotonView GameMessage;
    GameObject BManager;
    GameObject CManager;
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
        CManager = GameObject.Find("ChatManager");
		isGameOver = false;
    }

    //Play버튼 실행함수
    public void PlayGame()
    {
        //Debug.Log("play");
        SetPlayGame();
        GameMessage.RPC("sendMessage", PhotonTargets.Others);
    }

    
    [PunRPC]
    void sendMessage()
    {
        OtherPlayGame();
    }

    //
    public void OtherPlayGame()
    {
        //Debug.Log("get");
        SetPlayGame();
    }

    //현재 내 턴인지 확인
	public bool getIsMyTurn()
	{
		return isMyTurn;
	}

    //처음 시작시 턴 지정
    public void SetTurn()
    {
        if (PhotonNetwork.playerName == "Maker")
        {
            MyTurn();
            return;
        }
        OtherTurn();
    }

    //게임오버가 아닐시 턴 판단
    public void SendTurn()
    {
        
        if (!isGameOver)
        {
            //Debug.Log("게임종료 아니고 턴넘김");
            GameMessage.RPC("MyTurn", PhotonTargets.Others);
		    OtherTurn();
        }
        else
        {
            //Debug.Log("게임종료 일때 턴넘김");
        }
        
    }

    //내 턴일때 보여줄 UI
    [PunRPC]
    void MyTurn()
    {
        if (!isGameOver)
        {
            //Debug.Log("게임종료 아니고 나의턴");
            isMyTurn = true;
            InitPlay();
			Panel[0].SetActive(true);
            Panel[7].SetActive(true);
            Invoke("InitPlay", 1.0f);
        }
        else
        {
            //Debug.Log("게임종료 일때 나의턴");
        }
        
    }

    //상대턴일때 보여줄 UI
    void OtherTurn()
    {
       
        if (!isGameOver)
        {
            //Debug.Log("게임종료 아니고 상대턴");
            isMyTurn = false;
            InitPlay();
            Panel[0].SetActive(true);
            Panel[8].SetActive(true);
        }
        else
        {
            //Debug.Log("게임종료 일때 상대턴");
        }
         
    }

    //게임 시작시 빙고 초기화와 카운트 시작
    void SetPlayGame()
    {
        InitPlay();
        BManager.GetComponent<BingoManager>().InitGame();
		Panel[0].SetActive(true);
        Panel[4].SetActive(true);
    }

    //UI간 상호작용때 항상 모든것을 끄게 만듬
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
    
    //포톤 네트워크에서 방에 입장하면 자동으로 실행되는 함수(만들던 그냥 입장하던 둘다 실행)
    void OnJoinedRoom()
    {
        //Debug.Log("Join Room");
        Panel[9].GetComponent<Text>().text = PhotonNetwork.room.Name;
        CManager.GetComponent<ChatManager>().LogMsg("RoomName: " + PhotonNetwork.room.Name);

        if (PhotonNetwork.room.PlayerCount == 1)        //방을 생성함
        {
            //Debug.Log("RoomMake");
            Panel[1].SetActive(true);
            PhotonNetwork.playerName = "Maker";
        }
        else if (PhotonNetwork.room.PlayerCount == 2)   //방에 입장함
        {
            //Debug.Log("RoomJoin");
            Panel[1].SetActive(true);
            PhotonNetwork.playerName = "Joiner";
        }

        //Debug.Log(PhotonNetwork.playerName);
        //Debug.Log(PhotonNetwork.inRoom);
    }

    //상대 플레이어가 접속했을때 자동 실행 함수
    void OnPhotonPlayerConnected()
    {
        //Debug.Log("PlayerJoin");
        CManager.GetComponent<ChatManager>().JoinOther();
        InitPlay();
        isGameOver = false;
        Panel[0].SetActive(true);
        Panel[2].SetActive(true);
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.playerName = "Maker";
    }

    //상대 플레이어가 나가거나 끊겼을때 자동 실행 함수
    void OnPhotonPlayerDisconnected()
    {
        //Debug.Log("PlayerJoin(Lobby&Quit)");
        CManager.GetComponent<ChatManager>().LeaveOther();
        InitPlay();
        Panel[0].SetActive(true);
        Panel[1].SetActive(true);
        PhotonNetwork.room.IsVisible = true;
        PhotonNetwork.room.IsOpen = true;
        PhotonNetwork.playerName = "Maker";
    }

    //게임에 이겼을 때 보여줄 UI
    public void GameWin()
    {
        SoundOnOff.Instance.EffectPlay(2, 5);
        //Debug.Log("승리");
        isGameOver = true;
        InitPlay();
        Panel[0].SetActive(true);
        Panel[5].SetActive(true);
        //maker 와 joiner의 차이를 판단해서 코드 작성!
        MakerJoiner(PhotonNetwork.playerName);
    }

    //게임에 졌을 때 보여줄 UI
    public void GameLose()
    {
        //Debug.Log("패배");
        isGameOver = true;
        InitPlay();
        Panel[0].SetActive(true);
        Panel[6].SetActive(true);
        MakerJoiner(PhotonNetwork.playerName);
    }

    //게임이 끝나고 방장과 들어온 유저를 판단해서 보여줄 UI
    void MakerJoiner(string div)
    {
        if (PhotonNetwork.playerName == "Maker")
        {
            Invoke("OnPhotonPlayerConnected", 3.0f);
            return;
        }
        Invoke("JoinerWait", 2.0f);
    }

    //들어온 유저일 경우에 보여줄 UI
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
