using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingGame : MonoBehaviour
{
	public Text WaitText;
	public Text PlayText;

	float DotTime = 1.0f;
	char dot = '.';

    // Use this for initialization
    void Start()
    {
		WaitText.text = "Waiting for other\n";
        StartCoroutine(WaitRutine());
		PlayGame();
    }

    // Update is called once per frame
    IEnumerator WaitRutine()
    {
		Debug.Log("char");
		while(true)
		{
			WaitText.text += dot;
			yield return new WaitForSeconds(DotTime);
            if (WaitText.text == "Waiting for other\n......")
            {
                WaitText.text = "Waiting for other\n";
            }
        }
    }

	void PlayGame()
	{
		StopCoroutine(WaitRutine());
		WaitText.text = "Other Player\nConnected!";
		StartCoroutine(StartRutine());
	}


	IEnumerator StartRutine()
	{
		Debug.Log("char");
		int i = 0;
		while(i < 5)
		{
			PlayText.text = (5 - i).ToString();
			i++;
			yield return new WaitForSeconds(DotTime);
        }
		this.gameObject.SetActive(false);
	}

}
