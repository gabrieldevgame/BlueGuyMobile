using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager instance;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private string rewardedAdID;

    public int deaths;
    public int levels;

    public BannerView bannerView;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3218905455641158~3946075793";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3218905455641158~2980494395";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        rewardedAdID = "ca-app-pub-3218905455641158/8464098826";

        MobileAds.Initialize(initStatus => {});

        RequestBanner();
        RequestInterstitial();
        RequestRewardedVideo();
    }


//BANNER/////////////////////////////////////////////////////////////////////////////////////////////////////
    public void RequestBanner(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3218905455641158/6221735405";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3218905455641158/4908653730";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        //Invoke("DestroyBanner", 5f);
    }


//INTERSTITIAL/////////////////////////////////////////////////////////////////////////////////////////////////////
    private void RequestInterstitial(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3218905455641158/4293576063";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3218905455641158/2685815079";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    void HandleOnAdClosed(object sender, System.EventArgs args){
        RequestInterstitial();
    }

    public void ShowPopUp(){
        if (interstitial.IsLoaded()){
            interstitial.Show();
            RequestInterstitial();
        }
    }


//REWARDED/////////////////////////////////////////////////////////////////////////////////////////////////////
    private void RequestRewardedVideo(){
         rewardedAd = new RewardedAd(rewardedAdID);
         rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
         rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
         rewardedAd.OnAdClosed += HandleRewardedAdClosed;
         rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
         AdRequest request = new AdRequest.Builder().Build();
         rewardedAd.LoadAd(request);   
    }

    public void ShowRewardedVideo(){
        if(rewardedAd.IsLoaded()){
            rewardedAd.Show();
            RequestRewardedVideo();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args){
        RequestRewardedVideo();
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args){
        RequestRewardedVideo();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args){
        RequestRewardedVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args){
        FindObjectOfType<SaveData>().Save();
        FindObjectOfType<SaveData>().SetMessage(FindObjectOfType<SaveData>().messageText);
    }
}
