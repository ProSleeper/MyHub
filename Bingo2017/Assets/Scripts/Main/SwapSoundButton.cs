using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSoundButton : MonoBehaviour
{

    public Sprite swapimage;
    public Sprite oriimage;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ChangeSprite);
        oriimage = this.gameObject.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void ChangeSprite()
    {

        if (this.gameObject.GetComponent<Image>().sprite == oriimage)
        {
            this.gameObject.GetComponent<Image>().sprite = swapimage;
            return;
        }

        this.gameObject.GetComponent<Image>().sprite = oriimage;
    }
}
