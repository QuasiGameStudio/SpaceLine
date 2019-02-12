using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	[SerializeField]
	private float tutorialTime;

	// Use this for initialization
	void Start () {
		if (GameData.Instance.GetControllMode () == 0) {
			transform.GetChild (0).gameObject.SetActive (true);
		} else {
			transform.GetChild (1).gameObject.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
//		tutorialTime -= Time.deltaTime;
//		if (tutorialTime <= 0) {
//			this.gameObject.SetActive (false);
//			GameController.Instance.PlayGame ();
//		}
	}

	public void CloseTutorial(){
		this.gameObject.SetActive (false);
		GameController.Instance.PlayGame ();
	}

	public void CheckTutorial(){
		if (GameData.Instance.GetTutorial () > 0) {
			GameController.Instance.PauseGame ();
			this.gameObject.SetActive (true);
			GameData.Instance.SetTutorial (0);
		}
	}
}
