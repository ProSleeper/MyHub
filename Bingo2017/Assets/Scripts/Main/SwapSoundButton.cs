using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//볼륨버튼 클릭마다 이미지 바뀌게 만든 코드.. 이렇게 길 필요가 있나 싶네

public class SwapSoundButton : MonoBehaviour
{

    public Sprite swapImage;
    public Sprite oriImage;
    Image thisImage;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ChangeSprite);
        thisImage = this.gameObject.GetComponent<Image>();
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            SoundOnOff.Instance.AudioOff();
            thisImage.sprite = swapImage;
        }
        else
        {
            SoundOnOff.Instance.AudioOn();
            thisImage.sprite = oriImage;
        }
    }

    // Update is called once per frame
    void ChangeSprite()
    {
        if (thisImage.sprite == oriImage)   //현재 볼륨on이면 off로 바꿈
        {
            thisImage.sprite = swapImage;
            SoundOnOff.Instance.AudioOff();
            PlayerPrefs.SetInt("Mute", 1);
            return;
        }

        thisImage.sprite = oriImage;        //현재 볼륨off이면 on로 바꿈
        SoundOnOff.Instance.AudioOn();
        PlayerPrefs.SetInt("Mute", 0);
    }
}
