using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Button btn = this.gameObject.GetComponent<Button>();
		btn.onClick.AddListener(joinroom);
	}
	
	void joinroom()
	{
		PhotonInit.Instance.JoinToRoom(this.transform.FindChild("Text").GetComponent<Text>().text);
	}
}
