using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour
{
    [HideInInspector]
    string RoomName;

    public Text ViewName;
    //AsyncOperation async; 

    // Use this for initialization
    void Start()
    {
        //async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        //async.allowSceneActivation = false;
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(joinroom);
    }

    void joinroom()
    {
		PhotonNetwork.JoinRoom(RoomName);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //async.allowSceneActivation = true;
    }

    public void NameSetting(string setName)
    {
        RoomName = setName;
        ViewName.text = RoomName;
    }
}
