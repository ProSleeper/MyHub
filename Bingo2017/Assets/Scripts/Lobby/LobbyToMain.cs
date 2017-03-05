using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//클릭시 메인화면으로 이동 코드

public class LobbyToMain : MonoBehaviour
{

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SceneChange);
    }

    void SceneChange()
    {
        SoundOnOff.Instance.EffectPlay(4, 7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
