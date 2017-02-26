using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //async.allowSceneActivation = true;
        PhotonInit.Instance.LeaveRoom();
    }
}
