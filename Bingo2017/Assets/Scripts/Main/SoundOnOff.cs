using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사운드 싱글턴 코드.. 아마 읽어보면 다 알거임

public class SoundOnOff : MonoBehaviour
{

    public AudioSource PlayerBGM;
    public AudioSource PlayerClick;

	public AudioClip BGM;
    public AudioClip[] Effect;

    PlayerPrefs aa;

    //싱글턴 코드
    public static SoundOnOff Instance;
    void Awake()
    {
        this.InstantiateController();
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

    void Start()
    {
        BgmPlay();
        EffectSetting();
        // if (!PlayerData.Instance.CurrentPlayerData.SoundOn)
		// {
		// 	this.PlayerBGM.mute = true;
		// }
    }

    public void AudioOn()
    {
        this.PlayerBGM.mute = false;
		this.PlayerClick.mute = false;
    }

    public void AudioOff()
    {
        this.PlayerBGM.mute = true;
		this.PlayerClick.mute = true;
    }

    void BgmPlay()
	{
		this.PlayerBGM = this.gameObject.AddComponent<AudioSource>();
		this.PlayerBGM.clip = BGM;
		this.PlayerBGM.loop = true;
		this.PlayerBGM.volume = 0.05f;
		this.PlayerBGM.Play();
	}

    void EffectSetting()
	{
		this.PlayerClick = this.gameObject.AddComponent<AudioSource>();
		this.PlayerClick.loop = false;
	}

    public void EffectPlay(int effect, int volume)
    {
        this.PlayerClick.clip = Effect[effect];
        this.PlayerClick.volume = (float)volume / 10.0f;   //최초값이 0.75정도?
        this.PlayerClick.Play();
    }


}
