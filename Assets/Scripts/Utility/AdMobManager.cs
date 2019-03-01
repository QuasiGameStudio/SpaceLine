using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// #if GOOGLE_MOBILE_ADS
using GoogleMobileAds;
using GoogleMobileAds.Api;
// #endif


public class AdMobManager : Singleton<AdMobManager> {

	#region
// #if GOOGLE_MOBILE_ADS
	
	bool testingMode = false;

	BannerView bannerView;
	InterstitialAd interstitial;
	RewardBasedVideoAd rewardBasedVideo;
	AdRequest requestBanner;
	AdRequest requestInterstitial;
	AdRequest requestRewardVideo;

	
	int idIndex = 0;
	bool lastAdsIsSuccessToLoaded = false;
// #endif

// #if GOOGLE_MOBILE_ADS
	private string appId = "ca-app-pub-3204981671781860~3979982505";	

	//original ads id
	private string[] bannerIds = new string[] {
		"ca-app-pub-3204981671781860/2343467205"
	};	
	private string[] interstitialIds = new string[] {
		"ca-app-pub-3204981671781860/8130856791"
	};
	private string[] rewardAdsIds = new string[] {
		"ca-app-pub-3940256099942544/5224354917"
	};

	//test ads id
	// private string[] bannerIds = new string[] {"ca-app-pub-3940256099942544/6300978111"};	
	// private string[] interstitialIds = new string[] {"ca-app-pub-3940256099942544/1033173712"};

	//Edit with your device id
	private string testDeviceId = "81A5D70CE479330C99C85E799E15DA1A";
	
	
// #endif
	
	int tryingToShowInterstitial;

	void Awake(){

		DontDestroyOnLoad (this);
					
// #if GOOGLE_MOBILE_ADS
		MobileAds.Initialize(appId);
// #endif

		rewardBasedVideo = RewardBasedVideoAd.Instance;

		

		Reset();	

	}

	public void Reset(){

		if(bannerView != null){
			DestroyBanner();
		}

	}

	public void DestroyBanner(){
		bannerView.Destroy();
	}

	public void DestoryInterstitial(){
		interstitial.Destroy();
	}

	public void SetIdIndex(int idIndex){
		this.idIndex = idIndex;
	}

	public void RequestRewardBasedVideo(int idIndex)
    {
              
        // Create an empty ad request.        
		if(testingMode)
			requestRewardVideo = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestRewardVideo = new AdRequest.Builder().Build();
        

		// Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;        
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        
		// Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(requestRewardVideo, rewardAdsIds[idIndex]);
    }

	

	public void RequestBanner(int idIndex){

		bannerView =  new BannerView(bannerIds[idIndex], AdSize.SmartBanner, AdPosition.Bottom);		
				
		if(testingMode)
			requestBanner = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestBanner = new AdRequest.Builder().Build();

		bannerView.LoadAd(requestBanner);					
	}

	public void RequestInterstitial(int idIndex){

		interstitial = new InterstitialAd(interstitialIds[idIndex]);

		if(testingMode)
			requestInterstitial = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestInterstitial = new AdRequest.Builder().Build();
		
		interstitial.OnAdLoaded += HandleOnAdLoaded;
		interstitial.OnAdClosed += HandleOnAdClosed;
		interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

		interstitial.LoadAd(requestInterstitial);
		
	}

	public void ShowRewardVideo(){
        rewardBasedVideo.Show();   
	}
	
	public void ShowBanner(){

        bannerView.Show();        

	}

	public void ShowInterstitial(){
     
		interstitial.Show();			
  
	}

	
	
	public bool GetLastAdsIsSuccessToLoaded(){
		return lastAdsIsSuccessToLoaded;
	}

	//Rewarded Video Event

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {        
		RequestRewardBasedVideo(idIndex);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }



	//Interstial Event

// #if GOOGLE_MOBILE_ADS
	// Called when an ad request has successfully loaded.
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		lastAdsIsSuccessToLoaded = true;
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		
        lastAdsIsSuccessToLoaded = false;
		
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
        
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
        //AdsManager.Instance.SetAdsEventResult(3);		
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		
	}
	
// #endif

	#endregion
}
