using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Play 버튼 누르면 실행 코드

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(ClickPlay);
	}
	
	void ClickPlay()
	{
		SoundOnOff.Instance.EffectPlay(4, 7);
		GameManager.Instance.PlayGame();
	}
}
