using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    AsyncOperation async; 

    void Start()
    {
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        async.allowSceneActivation = false;
        PhotonInit.Instance.RoomDisplay();
    }

    // Update is called once per frame
    public void SceneChange()
    {
        async.allowSceneActivation = true;
    }
}
