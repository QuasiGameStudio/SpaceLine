using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>{


	public AudioSource bgm_Game;

	public Image bgmButtonImage;

	private float MuteVolume = 0;
	private float DefaultVolume;

	//public float SFXValue;
	[Tooltip("0 for off and 1 for on sprite")]
	public Sprite[] BGMIcon;
	//[Tooltip("0 for off and 1 for on sprite")]
	//public Sprite[] SFXIcon;

	private int bgmMuteKey;
	//private int sfx;

	void Awake()
	{
		DefaultVolume = bgm_Game.volume;
		instance = this;
	}

	void Start () {
		

		bgmMuteKey = PlayerPrefs.GetInt("BGMMute");

		//sfx = PlayerPrefs.GetInt("SFXMute", 1);
		if(bgmMuteKey == 0)
		{			
			bgm_Game.volume = DefaultVolume;
			bgmButtonImage.sprite = BGMIcon[1];
		}
		else
		{			
			bgm_Game.volume = MuteVolume;
			bgmButtonImage.sprite = BGMIcon[0];
		}

		/*
		if(sfx == 1)
		{
			SetVolume("SFXvol", SFXValue);
		}
		else
		{
			SetVolume("SFXvol", MuteValue);
		}
		*/
	}

	void Update () {

	}

	public void StopAudio(){
		if (bgmMuteKey == 0) {
			bgm_Game.volume = 0;
		}
	}

	public void PlayAudio(){
		if (bgmMuteKey == 0) {
			bgm_Game.volume = DefaultVolume;
		}
	}

	public void ToggleBGM()
	{
		if(bgmMuteKey == 1)
		{
			bgmMuteKey = 0;
			bgm_Game.volume = DefaultVolume;
			bgmButtonImage.sprite = BGMIcon[1];
			PlayerPrefs.SetInt("BGMMute", bgmMuteKey);
		}
		else
		{
			bgmMuteKey = 1;
			bgm_Game.volume = MuteVolume;
			bgmButtonImage.sprite = BGMIcon[0];
			PlayerPrefs.SetInt("BGMMute", bgmMuteKey);
		}
	}

	/*
	public void ToggleSFX(Image theImage)
	{
		if(sfx == 1)
		{
			SetVolume("SFXvol", MuteValue);
			sfx = 0;
			theImage.sprite = SFXIcon[0];
			PlayerPrefs.SetInt("SFXMute", 0);
		}
		else
		{
			SetVolume("SFXvol", SFXValue);
			sfx = 1;
			theImage.sprite = SFXIcon[1];
			PlayerPrefs.SetInt("SFXMute", 1);
		}
	}
	*/

	void SetVolume(string name, float vol)
	{
		//mixer.SetFloat(name, vol);
	}
}
