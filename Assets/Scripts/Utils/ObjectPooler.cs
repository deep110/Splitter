using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler{

	public int PoolSize {get { return pooledObjects.Count; }}
	public bool WillGrow {get; set;}

	private GameObject pooledObject;
	private List<GameObject> pooledObjects;

	public ObjectPooler(){
		WillGrow = true;
	}

	public ObjectPooler(GameObject pooledObject, int amount) : this(pooledObject,amount, true){}

	public ObjectPooler(GameObject pooledObject, int amount, bool willGrow){
		WillGrow = willGrow;
		Init(pooledObject,amount);
	}

	public void Init(GameObject pooledObject, int amount){

		if(pooledObjects != null || pooledObject == null || amount <1){
			return;
		}

		this.pooledObject = pooledObject;

		pooledObjects = new List<GameObject>(amount);

		for(int i=0; i<amount;i++){
			GameObject obj = Object.Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);

		}
	}

	public GameObject GetPooledObject(){

		if(pooledObjects==null) return null;

		for(int i=0; i< pooledObjects.Count; i++){

			if(!pooledObjects[i].activeInHierarchy){
				return pooledObjects[i];
			}
		}

		if(WillGrow){
			GameObject obj = Object.Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}

	public GameObject Spawn(Vector3 position, Quaternion rotation){
		GameObject obj = GetPooledObject();
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive(true);

		return obj;
	}
	
}
