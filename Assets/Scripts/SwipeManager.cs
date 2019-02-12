using UnityEngine;
using System.Collections;


public enum SwipeDirection{
	None = 0,
	Left = 1,
	Right = 2,
	Up = 4,
	Down = 8,
}

public class SwipeManager : Singleton<SwipeManager> {
	
	public SwipeDirection Direction{ set; get;}

	//First touch position
	private Vector3 touchPosition;
	//How long distance from first touch position to last touch (swiping) is required to detect this gesture is swipe
	private float swipeResistenceX = 50.0f;

	void Start(){
		instance = this;
	}

	// Update is called once per frame
	void Update () {

		Direction = SwipeDirection.None;

		if (Input.GetMouseButtonDown (0)) {
			touchPosition = Input.mousePosition;
		}
	
		if (Input.GetMouseButtonUp (0) && ControllerManager.Instance.GetControllMode() == 0) {
			//Distance from first touch position to last touch
			Vector2 deltaSwipe = touchPosition - Input.mousePosition;

			//If Swiping distance is meet swipe Resistence
			if (Mathf.Abs (deltaSwipe.x) > swipeResistenceX) {				
				Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
			}
		}

	}

	public bool isSwiping (SwipeDirection dir){
		return (Direction & dir) == dir;
	}

}
