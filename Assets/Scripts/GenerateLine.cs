using UnityEngine;
using System.Collections;

public class GenerateLine : Singleton<GenerateLine> {

	//rotation
	private float generalLineAngle = 0;

	[SerializeField]
	private float nextOriginPos;

	private int pooledAmount;

	private Vector3 originPosition;

	void Awake(){
		instance = this;

	}

	// Use this for initialization
	void Start () {

		originPosition = transform.position;

		pooledAmount = ObjectPool.Instance.GetPooledAmount();



		for (int i = 0; i < pooledAmount; i++) {
	
			SetLine(i);
		}

//		for (int i = 0; i < 4; i++) {
//			//SpawnLine ();		
//			SetLine(i);
//		}
	}
	
	// Update is called once per frames
	void Update () {
		
	}
		

	public void SetLine(int i){

		Vector3 newPos = originPosition + new Vector3 (0, 0, nextOriginPos);	

		GameObject obj = ObjectPool.Instance.GetPooledObject ();	
		if (obj == null)
			return;

		obj.transform.position = newPos;
	
		obj.SetActive (true);

		originPosition = newPos;
	}

	public void MoveLine(){

		Vector3 newPos = originPosition + new Vector3 (0, 0, nextOriginPos);
	
		GameObject obj = ObjectPool.Instance.GetPooledObject ();	
		if (obj == null)
			return;

		obj.transform.position = newPos;

		obj.SetActive (true);

		originPosition = newPos;

	}

	public void SetGeneralLineAngle(float value){
		generalLineAngle = value;
	}

	public float GetGeneralLineAngle(){
		return generalLineAngle;
	}
		
	public float GetRotationDirection(){
		if (GameData.Instance.GetReversesRotation () == 1)
			return -1;
		else
			return 1;
	}
}
