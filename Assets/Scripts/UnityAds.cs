using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAds : Singleton<UnityAds> {



	public string placementId = "rewardedVideo";

	//---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//

	#if UNITY_IOS
	private string gameId = "1486551";
	#elif UNITY_ANDROID
	private string gameId = "8b1dd0f0-ac06-40d9-bf5f-526cbf3d6a9f";
	#endif

	//-------------------------------------------------------------------//




	void Start ()
	{    
		

		if (Advertisement.isSupported) {
			Advertisement.Initialize (gameId, true);
		}

		//---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//

		if (Advertisement.isSupported) {
			Advertisement.Initialize (gameId, true);
		}

		//-------------------------------------------------------------------//

	}

	void Update ()
	{
		//if (adsButton) adsButton.gameObject.SetActive(Advertisement.IsReady(placementId));
	}

	public void ShowAd ()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show(placementId, options);
	}

	void HandleShowResult (ShowResult result)
	{
		if(result == ShowResult.Finished) {
			//Debug.Log("Video completed - Offer a reward to the player");
			RewardManager.Instance.GiveReward();

		}else if(result == ShowResult.Skipped) {
			//Debug.LogWarning("Video was skipped - Do NOT reward the player");

		}else if(result == ShowResult.Failed) {
			//Debug.LogError("Video failed to show");
		}
	}

	public bool GetAdvertisementsIsReady(){
		return Advertisement.IsReady (placementId);
	}
}
