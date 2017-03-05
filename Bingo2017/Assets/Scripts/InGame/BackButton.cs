using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//백버튼 누르면 팝업창 나오는 코드
public class BackButton : MonoBehaviour
{
    public GameObject Panel;

    // Use this for initialization
    void Start()
    {
        Panel.SetActive(false);
        this.gameObject.GetComponent<Button>().onClick.AddListener(PanelOnOff);
    }

    // Update is called once per frame
 	public void PanelOnOff()
    {
        SoundOnOff.Instance.EffectPlay(4, 7);
		if (Panel.activeSelf)
		{
			Panel.SetActive(false);
			return;
		}
		Panel.SetActive(true);
    }
}
