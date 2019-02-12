using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {

	[SerializeField]
	private bool resetData;
	
// Use this for initialization
	void Start () {
		if(resetData)
			ResetAllData ();
	}

	void Update(){
	}

	public void ResetAllData(){
		PlayerPrefs.DeleteAll ();
	}

	public void SetControllMode(int value){
		PlayerPrefs.SetInt ("ControllMode",value);
	}
	public int GetControllMode(){
		return PlayerPrefs.GetInt ("ControllMode",0);
	}

	public void SetReversesRotation(int value){
		PlayerPrefs.SetInt ("ReversesRotation", value);
	}

	public int GetReversesRotation(){
		return PlayerPrefs.GetInt ("ReversesRotation",0);
	}

	public void SetHighScore(float value){	
		PlayerPrefs.SetFloat ("Highscore", value);
	}
	public float GetHighScore(){	
		return PlayerPrefs.GetFloat ("Highscore", 0);
	}

	public void SetTutorial(int value){	
		PlayerPrefs.SetInt ("Tutorial", value);
	}
	public int GetTutorial(){
		return PlayerPrefs.GetInt ("Tutorial", 1);
	}

	//Reward Selected
	public void SetRewardSelected(int index){
		PlayerPrefs.SetInt ("RewardSelected", index);
	}
	public int GetRewardSelected(){
		return PlayerPrefs.GetInt ("RewardSelected", 0);
	}

	//reward
	public void SetRewardIsAvailable(string rewardName,int value){
		string key = rewardName + "IsAvailable";
		PlayerPrefs.SetInt (key, value);
	}
	public int GetRewardIsAvailable(string rewardName){

		string key = rewardName + "IsAvailable";
		return PlayerPrefs.GetInt (key,GetAdsWaitingTimes());
	}

	//ads
	public void SetAdsIsEnable(string adsName,int value){

		string key = adsName + "IsEnable";

		PlayerPrefs.SetInt (key, value);

	}
	public int GetAdsIsEnable(string adsName){

		string key = adsName + "IsEnable";

		return PlayerPrefs.GetInt (key,-13); // -18
	}

	public int GetAdsWaitingTimes(){
		return -4; // -5
	}

}
