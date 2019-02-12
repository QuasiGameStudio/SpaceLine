using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager> {

	void Start(){
		
	}

	public void GiveReward(){
		switch (GameData.Instance.GetRewardSelected()) {
		case 0:			
			GameData.Instance.SetAdsIsEnable ("Revive", GameData.Instance.GetAdsWaitingTimes());
			GameData.Instance.SetRewardIsAvailable ("Revive", 1);

			UseReviveReward ();
			break;
		case 1:
			GameData.Instance.SetAdsIsEnable ("Boost", GameData.Instance.GetAdsWaitingTimes ());
			GameData.Instance.SetRewardIsAvailable ("Boost", 1);
			GUIManager.Instance.GetBoostRewardedPanel ().SetActive (true);
			//GameController.Instance.PauseGame ();
			break;
		default:
			break;
		}
	}
		
	public void UseReviveReward(){
		if (GameData.Instance.GetRewardIsAvailable ("Revive") > 0) {			
			Player.Instance.Revive ();
			GameData.Instance.SetRewardIsAvailable ("Revive", 0);

		}

	}

	public void UseBoostReward(){
		if (GameData.Instance.GetRewardIsAvailable ("Boost") > 0) {					
			GameData.Instance.SetRewardIsAvailable ("Boost", 0);
			StartCoroutine(Player.Instance.Boost ());
		}
	}

}
