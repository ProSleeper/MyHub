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

    //
    void Start()
    {
        IsClick = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClickMyBlock);
        BManager = GameObject.Find("BingoManager");
        GManager = GameObject.Find("GameManager");
    }

    //블럭 초기화시 쓸 함수
    public void InitBlock()
    {
        IsClick = false;
        this.gameObject.GetComponent<Image>().sprite = OriginalSprite;
        this.transform.FindChild("Text").gameObject.SetActive(true);
    }

    //이건 내가 블럭을 클릭했을때 실행할 함수
    //여기서 상대방에게 ClickOtherBlock함수를 실행시키라고 보내주는것!
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

    //상대방이 블럭 클릭했을때 검사와 클릭상태로 바꿔주는 함수..
    //근데 위에 ClickMyBlock 함수와 통합해야 할 코드인데 귀찮아서 냅둠..
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

    //현재 해당 블럭 상태 읽기
    public bool getClick()
    {
        return IsClick;
    }
}
