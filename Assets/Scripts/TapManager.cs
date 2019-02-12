using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TapDirection{
	None = 0,
	Left = 1,
	Right = 2,
}

public class TapManager : Singleton<TapManager> {

	public TapDirection direction{ set; get;}

	private int dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		direction = TapDirection.None;

		//If Swiping distance is meet swipe Resistence
		if (Mathf.Abs (dir) > 0) {				
			direction |= (dir < 0) ? TapDirection.Right : TapDirection.Left;
		}

		dir = 0;
	}

	public void TapRight(){
		//direction = TapDirection.Right;
		dir = 1;
	}
	public void TapLeft(){
		//direction = TapDirection.Left;
		dir = -1;
	}

	public bool IsTaping(TapDirection dir){
		return (direction & dir) == dir;
	}
}
