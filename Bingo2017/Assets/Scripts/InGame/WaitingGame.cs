using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingGame : MonoBehaviour
{
	Text WaitText;
	string InitText;

	float DotTime = 1.0f;
	char dot = '.';

    // Use this for initialization
    void Start()
    {
		WaitText = this.GetComponent<Text>();
        StartCoroutine(WaitRutine(WaitText));
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
