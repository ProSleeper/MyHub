using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    AsyncOperation async; 

    void Start()
    {
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        async.allowSceneActivation = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(SceneChange);
        //Debug.Log("now" + SceneManager.GetActiveScene().buildIndex);
    }

    void SceneChange()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        async.allowSceneActivation = true;
        PhotonInit.Instance.RoomDisplay();
    }
}
