using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MakeRoom : MonoBehaviour
{

    public Text MyText;
    GameObject parent;
    Transform room_01;
    string str;

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(RoomMake);
    }

    void Update()
    {
        //PhotonInit.Instance.OnCreateRoom(" ");
        //Debug.Log("InRoom: " + PhotonNetwork.inRoom);
        //Debug.Log(PhotonNetwork.connectionStateDetailed);
    }
    // void Start()
    // {
    // 	parent = GameObject.Find("Canvas");
    // 	room_01 = parent.transform.FindChild("Room1");

    // 	Debug.Log(room_01);
    // 	str = "Room_01";
    // 	PhotonInit.Instance.OnCreateRoom(str);
    // 	room_01.gameObject.SetActive(true);
    // 	room_01.transform.FindChild("Text").GetComponent<Text>().text = str;

    // }

    void RoomMake()
    {
        //룸을 만들면 만든 본인은 룸 안으로 들어가기 때문에 본인은 룸을 볼 수 가 없음
        if (!(MyText.text == ""))
        {
            PhotonInit.Instance.OnCreateRoom(MyText.text);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
