using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager> {

	[SerializeField]
	private GameObject gameoverPanel;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text highScoreText;
	[SerializeField]
	private GameObject boostButton;
	[SerializeField]
	private GameObject boostEffect;
	[SerializeField]
	private GameObject reviveAdsOption;
	[SerializeField]
	private GameObject boostAdsOption;
	[SerializeField]
	private GameObject boostRewardedPanel;
	[SerializeField]
	private GameObject tutorialPanel;
	[SerializeField]
	private Color32[] restartButtonColors;


	void Awake()
	{
		instance = this;
	}

	void Start () {		
		highScoreText.text = System.Math.Round (GameData.Instance.GetHighScore()) + "";
	}

	void Update () {
		
		//Update Coin Text
		scoreText.text = "" + System.Math.Round (GameController.Instance.GetScore() , 0);

		if (GameData.Instance.GetRewardIsAvailable ("Boost") > 0)
			SetActiveBoostButton (true);
		else
			SetActiveBoostButton (false);

		/*
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (LosePanel.activeInHierarchy) {
				GoHome ();
			} else if (ShopPanel.activeInHierarchy) {
				CloseShopPanel ();
			} else if (HowToImage.activeInHierarchy) {
				CloseHowToPlay ();
			} else if (isGameStarted) {
				Pause ();
			} else {
				Application.Quit ();
			}
		}
		*/
	}

	//change Color by obstacle hit
	public void ChangeRestartButtonColor(int obstacleIndex){
		gameoverPanel.transform.GetChild (0).GetChild (1).GetComponent<Image> ().color = new Color32 (
			restartButtonColors [obstacleIndex].r,
			restartButtonColors [obstacleIndex].g,
			restartButtonColors [obstacleIndex].b,
			restartButtonColors [obstacleIndex].a
		);
			
	}

	public void SetActiveBoostEffect(bool value){
		boostEffect.SetActive (value);
	}

	public void SetActiveBoostButton(bool value){
		boostButton.SetActive (value);
	}

	public GameObject GetReviveAdsOption(){
		return reviveAdsOption;
	}

	public GameObject GetBoostAdsOption(){
		return boostAdsOption;
	}

	public GameObject GetGameOverPanel(){
		return gameoverPanel;
	}

	public GameObject GetBoostRewardedPanel(){
		return boostRewardedPanel;
	}
	public GameObject GetTutorial(){
		return tutorialPanel;
	}







	public void OpenPanel(GameObject panel)
	{
		panel.SetActive(true);
	}

	public void ClosePanel(GameObject panel)
	{
		panel.SetActive(false);
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
	public void OpenPausePanel()
	{
		//PausePanel.SetActive(true);
	}

	public void ClosePausePanel()
	{
		//PausePanel.SetActive(false);
	}
		



	public void OpenGameoverPanel()
	{
		gameoverPanel.SetActive (true);
	}

	public void CloseGameoverPanel()
	{
		gameoverPanel.SetActive (false);
	}

	public void GoHome()
	{
		
	}

	public void GoShop()
	{
		
	}

	public void OpenHowToPlay()
	{
		
	}

	public void CloseHowToPlay()
	{
		
	}
		
	/*
	public void Pause()
	{
		if (isPaused)
		{
			Time.timeScale = 1.0f;
			PauseImage.sprite = PauseIcon[0];
			ClosePausePanel();
		}
		else if(!isPaused && !isGameOver)
		{
			Time.timeScale = 0f;
			PauseImage.sprite = PauseIcon[1];
			OpenPausePanel();
		}
		isPaused = !isPaused;
	}
	*/

}
