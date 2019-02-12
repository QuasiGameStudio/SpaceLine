using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] vehicles;

	[SerializeField]
	private Transform spawnPosition;

	[SerializeField]
	private Transform playerParent;

	private int vehicleSelectedIndex = 0;

	private GameObject player;

	// Use this for initialization
	void Start () {
		//Instantiate (line.gameObject, newLine.transform.position, line.transform.rotation);	
		GameObject temp = vehicles[vehicleSelectedIndex];
		player = Instantiate (temp.gameObject, spawnPosition.position, temp.transform.rotation);
		player.transform.SetParent (playerParent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
