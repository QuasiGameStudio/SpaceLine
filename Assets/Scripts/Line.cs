using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Line: MonoBehaviour {

	public bool isLineStarter;

	[Tooltip("Obstacle variations that want to spawn")]
	public GameObject[] obstacleVariations;

	//Amount of obstacle that will spawn at one line
	private int n_Obstacle; 

	//Default line angle
	private float lineAngle = 0;

	//line rotating speed
	private float rotationSpeed = 600;

	//to know whether an obstacle has spawned on a side of line
	private bool[] lineSideSpawned = new bool[4];

	//to know whether an obstacle has spawned on a post of a line side
	private bool[] sidePostSpawned = new bool[4];

	private float rotationAngle = 90;

	// Use this for initialization
	void Start () {
	
		if (isLineStarter) {
			n_Obstacle = Random.Range (0, 0);
		} else {
			//Random how much obstacle will spawn at one line
			n_Obstacle = Random.Range (3, 5);
		}


		for(int i = 0; i < n_Obstacle; i++){

			//Random obstacle variation
			int obsIndex = Random.Range (0, obstacleVariations.Length);

			//Random line Side
			int lineSide = Random.Range (0,4);

			//Preventing the spawn of obstacle at a Side line that has been spawn a previous obstacle
			while (lineSideSpawned [lineSide] == true) {
				lineSide = Random.Range (0,4);
			}				
			lineSideSpawned[lineSide] = true;

			//Random line side post
			int sidePost = Random.Range (0,4);
			//Preventing the spawn of obstacle at a Post that has been spawn a previous obstacle
			while (sidePostSpawned [sidePost] == true) {
				sidePost = Random.Range (0, 4);
			}
			sidePostSpawned [sidePost] = true;


			//Transform of Side Post Selected from random
			Transform sidePostSelected = transform.GetChild(lineSide).GetChild (sidePost);

			//Spawn obstacle to Side Post Selected Position	
			GameObject newObstacle = Instantiate (obstacleVariations[obsIndex], sidePostSelected.position, sidePostSelected.rotation) as GameObject;

			//This line became the parents of a new object(Obstacle)
			newObstacle.transform.parent = this.transform;		

		}
	}
	
	// Update is called once per frame
	void Update () {

		//Swiping line
		if (SwipeManager.Instance.isSwiping(SwipeDirection.Right) || TapManager.Instance.IsTaping(TapDirection.Right)) {			
			//Add angle value (new value)
			lineAngle += rotationAngle * GenerateLine.Instance.GetRotationDirection();
			GenerateLine.Instance.SetGeneralLineAngle (lineAngle);
//			Debug.Log ("Swipe Right");
		} else if (SwipeManager.Instance.isSwiping(SwipeDirection.Left) || TapManager.Instance.IsTaping(TapDirection.Left)) {			
			//Add angle value (new value)
			lineAngle -= rotationAngle * GenerateLine.Instance.GetRotationDirection();
			GenerateLine.Instance.SetGeneralLineAngle (lineAngle);
//			Debug.Log ("Swipe Left");

		}

		//Preventing player rotate line when gameover
		if (!GameController.Instance.GetIsGameOver() && !GameController.Instance.GetIsPaused()) {
			//Change new value of line angle
			Quaternion target = Quaternion.Euler(0, 0, lineAngle);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * rotationSpeed);
		}

		if (lineAngle != GenerateLine.Instance.GetGeneralLineAngle ()) {
			lineAngle = GenerateLine.Instance.GetGeneralLineAngle ();
		}
			
		if (GameController.Instance.GetIsGameOver()) {
			CancelInvoke ("DestroyLine");
		}

	}
		
	void DestroyLine(){
		//Destroy (this.gameObject);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other){

		//Destroy this line on after player passed the line
		if (other.tag == "Line Destroyer") {
			//GenerateLine.Instance.SpawnLine ();
			GenerateLine.Instance.MoveLine ();
			Invoke ("DestroyLine", 0);
		}
	
	}

}
