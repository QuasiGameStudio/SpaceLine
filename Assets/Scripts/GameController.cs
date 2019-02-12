using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: Singleton<GameController>{

	private bool isGameover;
	private bool isPaused;

	private float score ;
	private float krypton;

	void Awake(){
		instance = this;

	}
	// Use this for initialization
	void Start () {		
		isPaused = true;

		Screen.sleepTimeout = SleepTimeout.SystemSetting;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("escape")) {
			//reset tutorial
			GameData.Instance.SetTutorial (1);
			Application.Quit ();
		}

		//game is playing
		if (!GameController.Instance.isGameover && !GameController.Instance.isPaused)
			score += (1 + (Player.Instance.GetPlayerSpeed()/3)) * Time.deltaTime ;
	}

	void CheckHighScore(){
		if (score >= GameData.Instance.GetHighScore()) {
			GameData.Instance.SetHighScore(score);
		}			
	}

	public void PlayGame(){
		if (!AdsManager.Instance.CheckBoostAdsIsEnable ()) {
			//unpause game and play
			PauseGame ();
			GUIManager.Instance.GetTutorial ().GetComponent<Tutorial> ().CheckTutorial ();

		} else {
			//open ads option
			AdsManager.Instance.CheckBoostAdsIsEnable ();
		}
	}

	public void PauseGame (){
		if (!isPaused) {
			isPaused = true;
			GUIManager.Instance.OpenPausePanel ();

		} else {
			isPaused = false;
			GUIManager.Instance.ClosePausePanel ();
		}
	}
		
	public void Gameover(){

		Player.Instance.Crash ();

		GUIManager.Instance.OpenGameoverPanel ();
		isGameover = true;
		AudioManager.Instance.StopAudio ();
		CheckHighScore ();

		AdsManager.Instance.CheckReviveAdsIsEnable ();
	}

	public void RestartGame(){		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void AddScore(){
		score += 10;
	}

	public void AddKrypton(){
		krypton += 10;
	}

//	private bool isGameover;
//	private  bool isPaused;
//
//	private  float score ;

	public bool GetIsGameOver(){
		return isGameover;
	}

	public bool GetIsPaused(){
		return isPaused;
	}

	public float GetScore(){
		return score;
	}

	public void SetIsGameOver(bool value){
		isGameover = value;
	}
	public void SetIsPaused(bool value){
		isPaused = value;
	}


}
