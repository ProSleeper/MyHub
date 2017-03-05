using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//게임 스타트 버튼 누르면 Lobby이동 코드

public class GameStart : MonoBehaviour
{

    //AsyncOperation async; 

    void Start()
    {
        //async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        //async.allowSceneActivation = false;
        
        this.gameObject.GetComponent<Button>().onClick.AddListener(SceneChange);
        //Debug.Log("now" + SceneManager.GetActiveScene().buildIndex);
    }

    void SceneChange()
    {
        SoundOnOff.Instance.EffectPlay(4, 7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //async.allowSceneActivation = true;
    }
}
