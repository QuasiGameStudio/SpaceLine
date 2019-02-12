using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : Singleton<ControllerManager> {

	//0 = swipe
	//1 = tap
	private int controllMode;
	private int reversesRotation;

	[SerializeField]
	private GameObject tapControllerGUI;

	[SerializeField]
	private GameObject swipeButton;
	[SerializeField]
	private GameObject tapButton;
	[SerializeField]
	private GameObject reversesToggle;

	// Use this for initialization
	void Start () {
		controllMode = GameData.Instance.GetControllMode ();

		reversesRotation = GameData.Instance.GetReversesRotation ();

		reversesToggle.GetComponent<Toggle> ().isOn = reversesRotation == 1;

		if (controllMode == 0) {
			tapButton.transform.localScale = new Vector3 (0.9f, 0.9f, 1);
			tapButton.GetComponent<Image> ().color = new Color32 (255, 255, 255, 135);
			tapButton.transform.GetChild (0).GetComponent<Text> ().color = new Color32 (255, 255, 255, 135);
		} else {

			tapControllerGUI.SetActive (true);

			swipeButton.transform.localScale = new Vector3 (0.9f, 0.9f, 1);
			swipeButton.GetComponent<Image> ().color = new Color32 (255, 255, 255, 135);
			swipeButton.transform.GetChild (0).GetComponent<Text> ().color = new Color32 (255, 255, 255, 135);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetControllMode(int value){
		controllMode = value;

		GameData.Instance.SetControllMode (controllMode);

		if (!GameController.Instance.GetIsGameOver()|| !GameController.Instance.GetIsPaused()) {
			if (controllMode == 1) {			

				//reset tutorial
				GameData.Instance.SetTutorial (1);

				tapControllerGUI.SetActive (true);

				tapButton.transform.localScale = new Vector3 (1, 1, 1);
				tapButton.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
				tapButton.transform.GetChild (0).GetComponent<Text> ().color = new Color32 (255, 255, 255, 255);

				swipeButton.transform.localScale = new Vector3 (0.9f, 0.9f, 1);
				swipeButton.GetComponent<Image> ().color = new Color32 (255, 255, 255, 135);
				swipeButton.transform.GetChild (0).GetComponent<Text> ().color = new Color32 (255, 255, 255, 135);

			} else {

				//reset tutorial
				GameData.Instance.SetTutorial (1);

				tapControllerGUI.SetActive (false);

				tapButton.transform.localScale = new Vector3 (0.9f, 0.9f, 1);
				tapButton.GetComponent<Image> ().color = new Color32 (255, 255, 255, 135);
				tapButton.transform.GetChild (0).GetComponent<Text> ().color = new Color32 (255, 255, 255, 135);

				swipeButton.transform.localScale = new Vector3 (1, 1, 1);
				swipeButton.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
				swipeButton.transform.GetChild (0).GetComponent<Text> ().color = new Color32 (255, 255, 255, 255);

			}
		} else {
			tapControllerGUI.SetActive (false);
		}
	}

	public void ChangeReversesRotation(){
		
		if (reversesRotation == 0) {
			reversesRotation = 1;
		}
		else{
			reversesRotation = 0;
		}

		GameData.Instance.SetReversesRotation (reversesRotation);
	}

	public int GetControllMode(){
		return controllMode;
	}
}
