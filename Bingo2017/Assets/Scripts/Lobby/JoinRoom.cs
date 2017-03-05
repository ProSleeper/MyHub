using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//룸 클릭시 룸으로 입장하게 만드는 코드

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
        SoundOnOff.Instance.EffectPlay(4, 7);
        LManager.GetComponent<LobbyManager>().SceneChange();
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NameSetting(string setName)
    {
        RoomName = setName;
        ViewName.text = RoomName;
    }
}
