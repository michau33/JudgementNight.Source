using UnityEngine;
using System.Collections.Generic;
using System;

public class ObjectPooler : Singleton<ObjectPooler>
{
	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	public Action onPoolRegistered;

#region Methods

	void Awake()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		InitializePools();
	}

	void InitializePools() 
	{
		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			// add object pool to dictionary with a specific tag
			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject InstantiateFromPool(string _tag, Vector3 _position, Quaternion _rotation)
	{
		if (!poolDictionary.ContainsKey(_tag))
			return null;

		GameObject objToSpawn = poolDictionary[_tag].Dequeue();

		objToSpawn.SetActive(true);
		objToSpawn.transform.position = _position;
		objToSpawn.transform.rotation = _rotation;

		Component pooledObject = objToSpawn.GetComponent(typeof(IPooledObject));

		if (pooledObject != null)
		{
			(pooledObject as IPooledObject).OnObjectPulled();
		}

		poolDictionary[_tag].Enqueue(objToSpawn);

		return objToSpawn;
	}	

	public void RegisterPool(Pool poolToRegister) 
	{
		if (pools.Contains(poolToRegister))
			return;

		pools.Add(poolToRegister);
		// poolDictionary.Add(poolToRegister.tag, objectPool);

		if (onPoolRegistered != null)
			onPoolRegistered();
	}
}

#endregion

[System.Serializable]
public class Pool : UnityEngine.Object
{
	public string tag;
	public GameObject prefab;
	public int size;

	public Pool(string tag, GameObject prefab, int size)
	{
		this.tag = tag;
		this.prefab = prefab;
		this.size = size;

		ObjectPooler.instance.onPoolRegistered += OnPoolRegistered;
	}
	
	/// TODO: i don't know if it's working so reconsider deleting it later 
	public override bool Equals(System.Object obj)
	{
		if (obj == null || obj.GetType() != GetType())
			return false;

		return ((obj as Pool).tag == this.tag);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	void OnPoolRegistered()
	{
		Debug.Log("Pool with tag " + this.tag + " succesfully registered");
	}
}

