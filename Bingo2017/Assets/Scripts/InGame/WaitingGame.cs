using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//상대방 기다릴때 나오는 UI 코드

public class WaitingGame : MonoBehaviour
{
	Text WaitText;
	string InitText;

	float DotTime = 1.0f;
	char dot = '.';

    void OnEnable()
    {
        WaitText = this.GetComponent<Text>();
        WaitText.text = "Other Player\nWaiting";
        StartCoroutine(WaitRutine(WaitText));
    }

    void OnDisable()
    {
        StopCoroutine(WaitRutine(WaitText));
    }

    // Update is called once per frame
    IEnumerator WaitRutine(Text text)
    {
		InitText = text.text;
		int count = 0;
		while(true)
		{
			text.text += dot;
			count++;
			yield return new WaitForSeconds(DotTime);
            if ((count % 5) == 0)
            {
                text.text = InitText;
            }
        }
    }
}
