using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : Singleton<AdsManager> {

	void Start(){
		
	}

	public void RefuseBoostAds(){
		GameData.Instance.SetAdsIsEnable ("Boost", GameData.Instance.GetAdsWaitingTimes ());
	}
	public void RefuseReviveAds(){
		GameData.Instance.SetAdsIsEnable ("Revive", GameData.Instance.GetAdsWaitingTimes ());
	}
		
	public void CheckReviveAdsIsEnable(){

		//Revive option will request if score is 80% of highscore and ads waiting time is reach

		int score = Mathf.RoundToInt(GameController.Instance.GetScore ()) ;
		int highscore = Mathf.RoundToInt(GameData.Instance.GetHighScore ());
		double percent = ((double)score / (double)highscore) * 100;
		//Debug.Log (percent);
		if (GameData.Instance.GetAdsIsEnable ("Revive") > 0 && percent > 65 || GameData.Instance.GetAdsIsEnable ("Revive") > 0 && percent > 75) {
			GUIManager.Instance.GetReviveAdsOption().SetActive (UnityAds.Instance.GetAdvertisementsIsReady ());
			if (UnityAds.Instance.GetAdvertisementsIsReady ()) {
				GUIManager.Instance.GetGameOverPanel ().transform.GetChild (0).gameObject.SetActive (false); // default option
				GUIManager.Instance.GetReviveAdsOption().transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = "Your Score " + score + ", Almost Beat The HighScore " + highscore + "!";
			}
		} else {
			RefillReviveAds ();
		}
	}
		
	void RefillReviveAds(){
		GameData.Instance.SetAdsIsEnable ("Revive", GameData.Instance.GetAdsIsEnable ("Revive") + 1);
	}

	public bool CheckBoostAdsIsEnable(){
		if (GameData.Instance.GetAdsIsEnable ("Boost") > 0 && GameData.Instance.GetRewardIsAvailable("Boost") < 1 && UnityAds.Instance.GetAdvertisementsIsReady ()) {
			GUIManager.Instance.GetBoostAdsOption().SetActive (UnityAds.Instance.GetAdvertisementsIsReady ());
			return true;
		} else {
			RefillBoostAds ();
			return false;
		}
	}

	void RefillBoostAds(){
		GameData.Instance.SetAdsIsEnable ("Boost", GameData.Instance.GetAdsIsEnable("Boost") + 1);
	}

}
