using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockClick : MonoBehaviour
{

    //public Button ChangeBlock;
    public Sprite OriginalSprite;
    public Sprite ChageSprite;
    public bool IsClick;
    GameObject BManager;

    // Use this for initialization
    void Start()
    {
        IsClick = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClickBlock);
        BManager = GameObject.Find("BingoManager");
    }

    public void InitBlock()
    {
        IsClick = false;
        this.gameObject.GetComponent<Image>().sprite = OriginalSprite;
        this.transform.FindChild("Text").gameObject.SetActive(true);
    }

    public void ClickBlock()
    {
        if (!IsClick)
        {
            IsClick = true;
            this.gameObject.GetComponent<Image>().sprite = ChageSprite;
        	this.transform.FindChild("Text").gameObject.SetActive(false);
            int ClickNumber;
            int.TryParse(this.transform.FindChild("Text").GetComponent<Text>().text, out ClickNumber);
			BManager.GetComponent<BingoManager>().BingoLogic();                 //클릭시 빙고 검사 로직
            BManager.GetComponent<BingoManager>().SendNumber(ClickNumber);        //상대방에게 클릭한 번호 보냄
        }
    }

    public bool getClick()
    {
        return IsClick;
    }
}
