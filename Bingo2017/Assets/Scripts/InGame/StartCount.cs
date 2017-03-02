using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            yield return new WaitForSeconds(1);
        }
        this.gameObject.SetActive(false);
        StopCoroutine(StartRutine());
        GameManager.Instance.InitPlay();
        GameManager.Instance.SetTurn();
    }
}
