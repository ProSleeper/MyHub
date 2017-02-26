using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStart : MonoBehaviour
{

    GameObject BManager;
	GameObject Panel;

    // Use this for initialization
    void Start()
    {
        BManager = GameObject.Find("BingoManager");
        this.gameObject.GetComponent<Button>().onClick.AddListener(ReStartGame);

		Panel = GameObject.Find("BackButtonPanel");
    }

    void ReStartGame()
    {
		BManager.GetComponent<BingoManager>().InitGame();
		Panel.SetActive(false);
    }
}
