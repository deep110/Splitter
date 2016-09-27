using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler {

	public static ObjectPooler current;
	public GameObject pooledObject;
	public int pooledAmount;
	public bool willGrow;

	List<GameObject> pooledObjects;

	public void Init(GameObject pooledObject, int amount){
		this.pooledAmount = amount;
		this.pooledObject = pooledObject;

		pooledObjects = new List<GameObject>(amount);
	}
	
}
