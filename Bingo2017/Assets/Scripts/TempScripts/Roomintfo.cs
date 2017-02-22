using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roomintfo : MonoBehaviour {

	[HideInInspector]
	public Text RoomName;
	[HideInInspector]
	public string roomName;

	// Use this for initialization
	public void WriteRoomName () {
		RoomName.text = roomName;
	}
}
