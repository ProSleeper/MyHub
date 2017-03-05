using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
//유니티 애즈 광고 코드 다른 게임에도 그대로 쓰면 될듯

public class UIManager : MonoBehaviour {
 
    public Button _BtnUnityAds;
    ShowOptions _ShowOpt = new ShowOptions();
    int _Gold = 0;
 
    void Awake()
    {
        Advertisement.Initialize("1331346");
        _ShowOpt.resultCallback = OnAdsShowResultCallBack;
        //UpdateButton();
    }
 
    void OnAdsShowResultCallBack(ShowResult result)
    {
        if (result == ShowResult.Finished) UpdateButton();
    }
 
    void UpdateButton()
    {
        _BtnUnityAds.interactable = Advertisement.IsReady("rewardedVideo");
        // _BtnUnityAds.GetComponentInChildren<Text>().text 
        //      = "See ads and earn gold\r\nGold = " + _Gold.ToString();
    }
 
    public void OnBtnUnityAds()
    {
        Advertisement.Show("rewardedVideo", _ShowOpt);
    }    
 
    void Update() { UpdateButton(); }
}