using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

	[Tooltip("Car Speed Movement")]
	private float speed = 10;

	private float defaultSpeed;

	[SerializeField]
	private AudioClip hornClip;
	[SerializeField]
	private AudioClip crashClip;
	[SerializeField]
	private AudioClip boostEffectClip;
	[SerializeField]
	private AudioClip engineBoostClip;
	[SerializeField]
	private AudioClip engineStartClip;
	[SerializeField]
	private AudioClip engineCrashClip;
	[SerializeField]
	private GameObject playerExhaust;

	private ParticleSystem smokeWhite;
	private ParticleSystem smokeDark;
	private ParticleSystem spinFire;
	private ParticleSystem tallRedFire;
	private ParticleSystem sparkExplosion;

	[SerializeField]
	private GameObject[] obstacleSparkles;


	void Awake(){

		defaultSpeed = speed;

		smokeWhite = playerExhaust.transform.GetChild (0).GetComponent<ParticleSystem> ();
		spinFire = playerExhaust.transform.GetChild (1).GetComponent<ParticleSystem> ();
		tallRedFire = playerExhaust.transform.GetChild (2).GetComponent<ParticleSystem> ();
		smokeDark = playerExhaust.transform.GetChild (3).GetComponent<ParticleSystem> ();
		sparkExplosion = playerExhaust.transform.GetChild (4).GetComponent<ParticleSystem> ();
	}

	void Start(){
		
	}
			
	// Update is called once per frame
	void Update () {

		//transform.rotation = new Vector3 (-90, 180, 0);

		//Move forward player 
		if (!GameController.Instance.GetIsGameOver() && !GameController.Instance.GetIsPaused()) {
			
			PlayerMoving ();

			//speed Adding
			SpeedAdding();

		} else {
			transform.Translate (0, 0, 0);
			//if(GetComponent<AudioSource> ().isPlaying)
			//	GetComponent<AudioSource> ().Pause ();
		}


	}

	void PlayerMoving(){

		//start moving
		if(playerExhaust.GetComponent<AudioSource>().clip != engineStartClip){			
			playerExhaust.GetComponent<AudioSource>().clip = engineStartClip;
			playerExhaust.GetComponent<AudioSource> ().Play ();
		}

		transform.Translate (0, -Time.deltaTime * speed, 0);

		//exhaust particle 

		//smoke
		if(smokeWhite.maxParticles == 5)
			smokeWhite.maxParticles =50;

		//spin
		if(!spinFire.isPlaying)
			spinFire.Play (true);
	}

	void OnTriggerEnter(Collider other){

		//If Player hit the obstacle then gameover
		if (other.tag == "Obstacle" && !GameController.Instance.GetIsGameOver()) {						
			GameController.Instance.Gameover ();

			//Destroy obstacle
			Instantiate(obstacleSparkles[other.GetComponent<Obstacle>().GetIndexColor()],other.transform.position,transform.rotation);
			int index = other.GetComponent<Obstacle> ().GetIndexColor ();
			GUIManager.Instance.ChangeRestartButtonColor (index);
			other.gameObject.SetActive(false);

		}

		//Collect Krypton
		if ((other.tag == "Krypton")) {
			GameController.Instance.AddKrypton ();
			AudioSource.PlayClipAtPoint (other.gameObject.GetComponent<AudioSource> ().clip, transform.position);

			Destroy(other.gameObject);
		}

		//Collect Coint
		/* 
		if ((other.tag == "Coin")) {
			GameController.Instance.addCoin ();
			AudioSource.PlayClipAtPoint (other.gameObject.GetComponent<AudioSource> ().clip, transform.position);
			Destroy(other.gameObject);
		}
		*/
	}

	public float GetPlayerSpeed(){
		return speed;
	}

	public void Revive(){

		GameController.Instance.SetIsGameOver(false) ;
		GameController.Instance.SetIsPaused (false);

		GUIManager.Instance.CloseGameoverPanel ();

		DecreaseSpeed ();
		smokeDark.gameObject.SetActive (false);
		smokeWhite.Play (true);
		spinFire.gameObject.SetActive (true);
		sparkExplosion.gameObject.SetActive (false);
		playerExhaust.GetComponent<AudioSource>().clip = engineStartClip;

		StartCoroutine(Blink ());

		//Play Global Music
		AudioManager.Instance.PlayAudio ();

	}

	public void Crash(){
		GetComponent<Animator> ().SetTrigger ("Crash");

		//Audio
		//GetComponent<AudioSource> ().PlayOneShot (hornClip);
		GetComponent<AudioSource> ().PlayOneShot (crashClip);
		playerExhaust.GetComponent<AudioSource>().clip = engineCrashClip;
		playerExhaust.GetComponent<AudioSource> ().Play ();
		playerExhaust.GetComponent<AudioSource> ().volume = 0.3f;

		//Particle
		smokeDark.gameObject.SetActive (true);
		smokeWhite.Stop (true);
		spinFire.gameObject.SetActive (false);
		sparkExplosion.gameObject.SetActive (true);

	}

	public IEnumerator Blink(){
		float x = 0.09f;
		GameObject objects = transform.GetChild(0).gameObject;
		GetComponent<BoxCollider> ().enabled = false;



		for (int i = 0; i < 8; i++) {



			objects.SetActive(false);
			yield return new WaitForSeconds (x);


			objects.SetActive(true);
			playerExhaust.GetComponent<AudioSource> ().Stop ();
			yield return new WaitForSeconds (x);

			objects.SetActive(false);
			yield return new WaitForSeconds (x);


			objects.SetActive(true);
			playerExhaust.GetComponent<AudioSource> ().Stop ();
			yield return new WaitForSeconds (x);

		}

		playerExhaust.GetComponent<AudioSource> ().Play ();
		GetComponent<BoxCollider> ().enabled = true;	
		AudioManager.Instance.PlayAudio ();
	}
		
	public IEnumerator Boost(){

		float halfBoostingTime = 3.5f;

		//boost Start
		tallRedFire.gameObject.SetActive (true);
		smokeWhite.gameObject.SetActive (false);

		GetComponent<BoxCollider> ().enabled = false;

		speed *= 6;
		GetComponent<AudioSource> ().PlayOneShot (boostEffectClip);
		GetComponent<AudioSource> ().PlayOneShot (engineBoostClip);

		GUIManager.Instance.SetActiveBoostEffect (true);

		yield return new WaitForSeconds (halfBoostingTime);

		//add more sound and time to boost
		GetComponent<AudioSource> ().PlayOneShot (engineBoostClip);

		yield return new WaitForSeconds (halfBoostingTime);
		GetComponent<AudioSource> ().PlayOneShot (boostEffectClip);
		yield return new WaitForSeconds (1);


		//boost end

		tallRedFire.gameObject.SetActive (false);
		smokeWhite.gameObject.SetActive (true);

		GUIManager.Instance.SetActiveBoostEffect (false);

		GetComponent<BoxCollider> ().enabled = true;

		speed /= 6;

		StartCoroutine (Blink ());
	} 

	void SpeedAdding(){
		speed += 0.001f;
		Debug.Log ("Speed: " + speed);
	}

	void DecreaseSpeed(){
		float additionalSpeed = speed - defaultSpeed;
		speed = speed - (additionalSpeed / 2);
	}


}
