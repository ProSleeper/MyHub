using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AddMob : MonoBehaviour {


    private BannerView adBannerView = null;
 
    void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        if (adBannerView == null)
        {
            AdRequest.Builder builder = new AdRequest.Builder();
            //AdRequest adRequest = builder.Build();
            AdRequest adRequest = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("MY_DEVICE_ID").Build();//FOR TEST
            adBannerView = new BannerView("ca-app-pub-2081199175882186/2229510153", AdSize.SmartBanner, AdPosition.BottomLeft);
            adBannerView.LoadAd(adRequest);//해당 함수로 광고 요청 반복 가능.
        }
    }
 
    
    public void AddShow()
    {
        adBannerView.Show();
    }
        

}
