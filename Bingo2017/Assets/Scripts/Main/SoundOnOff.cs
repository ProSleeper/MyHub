using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{

    // Use this for initialization
    public static SoundOnOff Instance;
    void Awake()
    {
        this.InstantiateController();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateController()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }
}
