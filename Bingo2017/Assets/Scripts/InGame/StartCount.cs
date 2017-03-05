using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이버튼 누르면 실행되는 시작 카운트 코드

public class StartCount : MonoBehaviour
{

    public Text count;

	void OnEnable()
    {
        StartCoroutine(StartRutine());
    }
	
    IEnumerator StartRutine()
    {
        int i = 0;
        while (i < 5)
        {
            count.text = (5 - i).ToString();
            i++;
            //계이름으로 해봅시다!
            SoundOnOff.Instance.EffectPlay(i + 4, 1);
            yield return new WaitForSeconds(1);
        }
        this.gameObject.SetActive(false);
        StopCoroutine(StartRutine());
        GameManager.Instance.InitPlay();
        GameManager.Instance.SetTurn();
    }
}
