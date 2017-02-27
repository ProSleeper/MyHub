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
    GameObject LManager;

    // Use this for initialization
    void Start()
    {
        LManager = GameObject.Find("LobbyManager");
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(joinroom);
        
    }

    void joinroom()
    {
		PhotonNetwork.JoinRoom(RoomName);
        LManager.GetComponent<LobbyManager>().SceneChange();
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NameSetting(string setName)
    {
        RoomName = setName;
        ViewName.text = RoomName;
    }
}
