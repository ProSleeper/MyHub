using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonInit : MonoBehaviour {

	//public Text roomUser;


	//싱글톤 코드
	public static PhotonInit Instance;

	void Awake()
	{
		this.InstantiateController();
	}

	private void InstantiateController() {
        if(Instance == null)
        {
            Instance = this;
			PhotonNetwork.ConnectUsingSettings("v0.9");
            DontDestroyOnLoad(this);
        }
        else if(this != Instance) {
            Destroy(this.gameObject);
        }
    }
	//여기까지 싱글톤 코드

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
		
	}

	public void OnClickStartBtn()
	{
		Debug.Log("name: " + PhotonNetwork.room.Name);
		Debug.Log("playerCount: " + PhotonNetwork.room.PlayerCount);
		Debug.Log("roomCount: " + PhotonNetwork.countOfRooms);
		Debug.Log("roomCount: " + PhotonNetwork.GetRoomList().Length);
	}
	
	public void OnCreateRoom(string RoomName)
	{
		PhotonNetwork.CreateRoom(RoomName);
	}
}
