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
            //PhotonNetwork.ConnectUsingSettings("v0.9");
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }
    //여기까지 싱글톤 코드



    void ServerConnect()
    {
		if( isPaused )
		{
			Debug.Log("Disconnect");
			//PhotonNetwork.Disconnect();
		}
		else if((!isPaused) && (!PhotonNetwork.connected))
		{
			Debug.Log("Connect");
			PhotonNetwork.ConnectUsingSettings("v0.9");
		}
    }

    bool isPaused = false;

	//앱이 현재 실행중인지 아닌지 판단.. 현재 실행중이라면 hasFocus 가 true 실행중이지 않다면 hasFocus false
    void OnApplicationFocus( bool hasFocus )
    {
        isPaused = !hasFocus;
		ServerConnect();
    }
	void OnApplicationQuit()
    {
        Debug.Log("Quit");
    }

    void OnJoinedLobby()
    {
        Debug.Log("Join Lobby");
        //PhotonNetwork.CreateRoom("MyMatch");
    }

    // void OnPhotonRandomJoinFailed()
    // {
    // 	Debug.Log("No Room");
    // 	PhotonNetwork.CreateRoom("MyMatch");
    // }

    // void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    // {
    // 	Debug.Log(PhotonNetwork.room.PlayerCount);
    // }

    void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }

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
        // else
        // {
        // 	접속이 안됐을때 나타내기
        // }
    }
    public void OnReceivedRoomListUpdate()
    {
        Debug.Log("RoomUpdate");
        Debug.Log(PhotonNetwork.GetRoomList().Length);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            RoomDisplay();
        }
    }

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
            Roomintfo roomdata = Rooms.GetComponent<Roomintfo>();
            roomdata.roomName = room.Name;
            roomdata.WriteRoomName();
            Rooms.transform.SetParent(GameObject.Find("Content").transform, false);
        }
    }

    public void JoinToRoom(string roomName)
    {
        
        PhotonNetwork.JoinRoom(roomName);
    }
}
