using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PhotonInit : MonoBehaviour
{

    public GameObject roomObject;

    //싱글톤 코드
    public static PhotonInit Instance;

    void Awake()
    {
        this.InstantiateController();
    }

    private void InstantiateController()
    {
        if (Instance == null)
        {
            Instance = this;
            PhotonNetwork.ConnectUsingSettings("v0.9");
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }
    //여기까지 싱글톤 코드

    //여기부터 네트워크 상황에 맞춰서 실행하는 함수

    //로비 입장시 실행 함수
    void OnJoinedLobby()
    {
        Debug.Log("Join Lobby");
        //PhotonNetwork.CreateRoom("MyMatch");
    }

    // void OnLeftRoom ()
	// {
    //     //본인 클라이언트가 나갔을 때 실행됨
	// 	Debug.Log("left");
	// }

    //룸 입장시 실행 함순
    // void OnJoinedRoom()
    // {
    //     Debug.Log("Join Room");
    //     Debug.Log(PhotonNetwork.room.PlayerCount);

    //     if (PhotonNetwork.room.PlayerCount == 1)        //방을 생성함
    //     {
            
    //     }
    //     else if (PhotonNetwork.room.PlayerCount == 2)   //방에 입장함
    //     {
            
    //     }
    //     //Debug.Log(PhotonNetwork.player.UserId);
    //     //Debug.Log(PhotonNetwork.inRoom);
    //     //GameObject.Find("ChatManager").GetComponent<ChatManager>().SendSystemMessage("\n상대방이 들어왔습니다.");
    // }

    public void LeaveRoom()
    {
        Debug.Log("Leave Room");
        PhotonNetwork.LeaveRoom();
    }
    

    //룸 생성 래퍼 함수
    public void OnCreateRoom(string RoomName)
    {
        if (PhotonNetwork.connected)
        {
            RoomOptions roomOption = new RoomOptions();

            roomOption.IsOpen = true;
            roomOption.IsVisible = true;
            roomOption.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(RoomName, roomOption, TypedLobby.Default);
        }
    }

    //룸이 만들어지거나 없어지거나 했을때 실행되는 함수
    void OnReceivedRoomListUpdate()
    {
        Debug.Log("RoomUpdate");
        Debug.Log("RoomCount: " + PhotonNetwork.GetRoomList().Length);
        Debug.Log("PlayerCount: " + PhotonNetwork.countOfPlayers);

        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            RoomDisplay();
        }
    }

    //룸 상태 보여줌
    public void RoomDisplay()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Room"))
        {
            Destroy(item);
        }

        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(room.Name);
            GameObject Rooms = Instantiate(roomObject);
            JoinRoom roomdata = Rooms.GetComponent<JoinRoom>();
            roomdata.NameSetting(room.Name);
            Rooms.transform.SetParent(GameObject.Find("Grid").transform, false);
        }
    }


    //후에 관여.

    //룸 입장을 실패했을때 실행되는 함수
    // void OnPhotonRandomJoinFailed()
    // {
    // 	Debug.Log("No Room");
    // 	PhotonNetwork.CreateRoom("MyMatch");
    // }

    // void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    // {
    // 	Debug.Log(PhotonNetwork.room.PlayerCount);
    // }

    // void ServerConnect()
    // {
    //     if (isPaused)
    //     {
    //         Debug.Log("Disconnect");
    //         //PhotonNetwork.Disconnect();
    //     }
    //     else if ((!isPaused) && (!PhotonNetwork.connected))
    //     {
    //         Debug.Log("Connect");
    //         PhotonNetwork.ConnectUsingSettings("v0.9");
    //     }
    // }

    // bool isPaused = false;

    // //앱이 현재 실행중인지 아닌지 판단.. 현재 실행중이라면 hasFocus 가 true 실행중이지 않다면 hasFocus false
    // void OnApplicationFocus(bool hasFocus)
    // {
    //     isPaused = !hasFocus;
    //     ServerConnect();
    // }
    // void OnApplicationQuit()
    // {
    //     Debug.Log("Quit");
    // }
}
