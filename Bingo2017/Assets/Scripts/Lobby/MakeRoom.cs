using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRoom : MonoBehaviour {

	//public Button myBtn;
	GameObject parent;
	Transform room_01;
	string str;


	void Start()
	{
		//PhotonInit.Instance.OnCreateRoom(" ");
	}

	void Update()
	{
		//PhotonInit.Instance.OnCreateRoom(" ");
	}
	// void Start()
	// {
	// 	parent = GameObject.Find("Canvas");
	// 	room_01 = parent.transform.FindChild("Room1");

	// 	Debug.Log(room_01);
	// 	str = "Room_01";
	// 	PhotonInit.Instance.OnCreateRoom(str);
	// 	room_01.gameObject.SetActive(true);
	// 	room_01.transform.FindChild("Text").GetComponent<Text>().text = str;
		
	// }

	public void OnClickStartBtn()
	{
		PhotonInit.Instance.OnClickStartBtn();
		
	}
}
