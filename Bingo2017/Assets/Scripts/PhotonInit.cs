using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonInit : MonoBehaviour {


	void Awake()
	{
		PhotonNetwork.ConnectUsingSettings("v0.9");
	}

	// Use this for initialization
	void Start () {
		//PhotonNetwork.CreateRoom("MyMatch");
		//OnConnectedToMaster();
	}

	public void OnJoinedLobby()
	{
		Debug.Log("Entered Lobby");
    	PhotonNetwork.JoinRandomRoom();
		//PhotonNetwork.CreateRoom("MyMatch");
	}

	// public void OnConnectedToMaster()
    // {
    //     //Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
    //     PhotonNetwork.JoinRandomRoom();
    // }

	void OnPhotonRandomJoinFailed()
	{
    	Debug.Log("No Room");
    	PhotonNetwork.CreateRoom("MyMatch");
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log(PhotonNetwork.room.PlayerCount);
	}

	void OnJoinedRoom()
	{
		Debug.Log("Enter Room");
		//PhotonNetwork.JoinRandomRoom();
		//Debug.Log(PhotonNetwork.room.PlayerCount);
	}

	public void OnClickStartBtn()
	{
		Debug.Log(PhotonNetwork.room.Name);
		Debug.Log(PhotonNetwork.room.PlayerCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
