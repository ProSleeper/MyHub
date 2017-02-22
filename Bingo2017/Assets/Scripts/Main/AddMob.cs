using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AddMob : MonoBehaviour
{

    private BannerView adBannerView = null;
    public static AddMob Instance;

    void Awake() 
    {
        this.InstantiateController();
    }

    void Start ()
    {
        if (adBannerView == null)
        {
            AdRequest.Builder builder = new AdRequest.Builder();
            //AdRequest adRequest = builder.Build();
            AdRequest adRequest = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("8B7263AFC1304D36").Build();//FOR TEST
            adBannerView = new BannerView("ca-app-pub-2081199175882186/2229510153", AdSize.SmartBanner, AdPosition.BottomLeft);
            adBannerView.LoadAd(adRequest);//해당 함수로 광고 요청 반복 가능.
        }
    }
 
    private void InstantiateController() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if(this != Instance) {
            Destroy(this.gameObject);
        }
    }
    
    public void AddShow()       // 버튼이라던가 이벤트로 보여주는건데.. 없어도 배너광고 잘 나옴
    {
        adBannerView.Show();
    }
        

}
