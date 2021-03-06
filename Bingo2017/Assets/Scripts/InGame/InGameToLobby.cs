﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//로비로 나가기

public class InGameToLobby : MonoBehaviour
{

    
    //AsyncOperation async; 
    void Start()
    {
        //async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1); 
        //async.allowSceneActivation = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(RoomOut);
        //Debug.Log("now" + SceneManager.GetActiveScene().buildIndex);
    }

    void RoomOut()
    {
        SoundOnOff.Instance.EffectPlay(4, 7);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //async.allowSceneActivation = true;
        PhotonInit.Instance.LeaveRoom();
    }
}
