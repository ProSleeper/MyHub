using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCount : MonoBehaviour {

	public GameObject StartPanel;
	public Text count;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartRutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator StartRutine()
	{
		int i = 0;
		while(i < 5)
		{
			count.text = (5 - i).ToString();
			i++;
			yield return new WaitForSeconds(1);
        }
		this.gameObject.SetActive(false);
		StartPanel.SetActive(false);
	}
	
}
