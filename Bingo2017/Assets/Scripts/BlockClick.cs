using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockClick : MonoBehaviour {

	//public Button ChangeBlock;
	public Sprite ChageSprite;
	public bool Click;
	GameObject BManager;


	// Use this for initialization
	void Start () {
		Click = false;
		this.gameObject.GetComponent<Button>().onClick.AddListener(OneClick);
		BManager = GameObject.Find("BingoManager");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OneClick()
	{
		//Debug.Log("DoubleClick");
		Click = true;
		this.gameObject.GetComponent<Image>().sprite = ChageSprite;
		this.transform.FindChild("Text").gameObject.SetActive(false);
		BManager.GetComponent<BingoManager>().check();
	}

	public bool IsClick()
	{
		return Click;
	}
}
