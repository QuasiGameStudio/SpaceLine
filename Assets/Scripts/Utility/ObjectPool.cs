using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool> {

	[SerializeField]
	private GameObject pooledObject;
	private int pooledAmount = 15;
	public bool willGrow = false;

	List<GameObject> pooledObjects;

	[SerializeField]
	Transform pooledObjectParent; 

	// Use this for initialization
	void Awake () {
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject) Instantiate(pooledObject);
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetPooledObject(){
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy) {
				return pooledObjects [i];
			}
		}

		if (willGrow) {
			GameObject obj = (GameObject) Instantiate(pooledObject);
			obj.transform.parent = pooledObjectParent;
			pooledObjects.Add (obj);
			return obj;
		}

		return null;
	}

	public int GetPooledAmount(){
		return pooledAmount;
	}
}
