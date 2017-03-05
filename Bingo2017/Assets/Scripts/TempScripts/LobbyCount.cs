using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//현재 로비에 유저가 몇명 있는지 보여주는 코드

public class LobbyCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Text>().text ="Player: " + PhotonNetwork.countOfPlayers.ToString();
	}
}
