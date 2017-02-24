using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{



    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SceneChange);
        //Debug.Log("now" + SceneManager.GetActiveScene().buildIndex);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}