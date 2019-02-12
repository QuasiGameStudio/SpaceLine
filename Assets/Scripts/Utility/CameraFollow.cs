using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[Tooltip("Object to be followed")]
	public GameObject player;

	private Vector3 offset;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;

	}

	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}


}