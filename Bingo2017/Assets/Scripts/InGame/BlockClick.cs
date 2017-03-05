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
    GameObject GManager;

    // Use this for initialization
    void Start()
    {
        IsClick = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClickMyBlock);
        BManager = GameObject.Find("BingoManager");
        GManager = GameObject.Find("GameManager");
    }

    public void InitBlock()
    {
        IsClick = false;
        this.gameObject.GetComponent<Image>().sprite = OriginalSprite;
        this.transform.FindChild("Text").gameObject.SetActive(true);
    }

    public void ClickMyBlock()
    {
        if (!IsClick)
        {
            SoundOnOff.Instance.EffectPlay(10, 5);
            IsClick = true;
            this.gameObject.GetComponent<Image>().sprite = ChageSprite;
        	this.transform.FindChild("Text").gameObject.SetActive(false);
            int ClickNumber = 0;
            int.TryParse(this.transform.FindChild("Text").GetComponent<Text>().text, out ClickNumber);
            
            BManager.GetComponent<BingoManager>().BingoLogic();
            BManager.GetComponent<BingoManager>().SendNumber(ClickNumber);  //상대방에게 클릭한 번호 보냄
            
            //호출 순서가 애매함 이게 맞는지...
            GManager.GetComponent<GameManager>().SendTurn();
        }
    }

    public void ClickOtherBlock()
    {
        if (!IsClick)
        {
            SoundOnOff.Instance.EffectPlay(11, 5);
            IsClick = true;
            this.gameObject.GetComponent<Image>().sprite = ChageSprite;
        	this.transform.FindChild("Text").gameObject.SetActive(false);
            int ClickNumber = 0;
            int.TryParse(this.transform.FindChild("Text").GetComponent<Text>().text, out ClickNumber);
			BManager.GetComponent<BingoManager>().BingoLogic();                 //클릭시 빙고 검사 로직
            BManager.GetComponent<BingoManager>().UpdateOther();
        }
    }

    public bool getClick()
    {
        return IsClick;
    }
}
