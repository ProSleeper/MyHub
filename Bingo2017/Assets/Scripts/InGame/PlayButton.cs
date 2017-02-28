using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(ClickPlay);
	}
	
	void ClickPlay()
	{
		GameManager.Instance.PlayGame();
	}
}
