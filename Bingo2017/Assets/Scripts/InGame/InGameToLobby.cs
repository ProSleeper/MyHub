using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameToLobby : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(RoomOut);
        //Debug.Log("now" + SceneManager.GetActiveScene().buildIndex);
    }

    void RoomOut()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        PhotonInit.Instance.LeaveRoom();
    }
}
