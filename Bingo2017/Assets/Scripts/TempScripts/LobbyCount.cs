using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCount : MonoBehaviour {

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Text>().text ="Lobby User Count: " + PhotonNetwork.countOfPlayers.ToString();
	}
}
