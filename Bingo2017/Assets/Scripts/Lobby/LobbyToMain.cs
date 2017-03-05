using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
