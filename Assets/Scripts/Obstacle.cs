using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	//Index color green, purple, yellow

	[SerializeField]
	private int indexColor;

	public int GetIndexColor(){
		return indexColor;
	}
}
