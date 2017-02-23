using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (var player in PhotonNetwork.playerList)
             {
				//chatMsg.text += PhotonNetwork.GetRoomList();
				Debug.Log("ID: " + player.ID + "\n");
				Debug.Log("UserId: " + player.UserId + "\n");
             }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
