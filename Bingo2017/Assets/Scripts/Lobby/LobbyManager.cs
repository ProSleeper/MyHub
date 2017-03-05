using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//별것 없지만 일단 조금 중요한.. 다른앱 실행했다가 다시 왔을때 다시 재접속하게 만드는 코드가 들어있음

public class LobbyManager : MonoBehaviour
{
    //AsyncOperation async; 

    void Start()
    {
        //async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        //async.allowSceneActivation = false;
        if(!PhotonNetwork.connected)
        {
            PhotonNetwork.Reconnect();
        }

        PhotonInit.Instance.RoomDisplay();
    }

    // Update is called once per frame
    public void SceneChange()
    {
        //async.allowSceneActivation = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
