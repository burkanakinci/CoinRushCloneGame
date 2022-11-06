using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : CustomBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string prefabTag;
        public GameObject pooledPrefab;
        public int size = 1;
        public Transform prefabParent;
    }
    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<IPooledObject>> poolDictionary;

    private IPooledObject spawnOnPool;
    private IPooledObject tempSpawned;

    public override void Initialize()
    {
        base.Initialize();

        poolDictionary = new Dictionary<string, Queue<IPooledObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            Queue<IPooledObject> activeOnPool = new Queue<IPooledObject>();
            Queue<IPooledObject> objectsOnPool = new Queue<IPooledObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                tempSpawned = Instantiate(pools[i].pooledPrefab, pools[i].prefabParent).GetComponent<IPooledObject>();
                tempSpawned.GetGameObject().gameObject.SetActive(false);
                tempSpawned.GetGameObject().Initialize();
                objectsOnPool.Enqueue(tempSpawned);
            }
            poolDictionary.Add(pools[i].prefabTag, objectsOnPool);

        }
    }
    public IPooledObject SpawnFromPool(string _prefabTag,
                                    Vector3 _position = new Vector3(),
                                    Quaternion _rotation = new Quaternion(),
                                    Transform _parent = null)
    {
        if (!poolDictionary.ContainsKey(_prefabTag))
        {
            return null;
        }

        if (poolDictionary[_prefabTag].Count > 0)
        {
            spawnOnPool = poolDictionary[_prefabTag].Dequeue();
        }
        else
        {
            for (int i = pools.Count - 1; i >= 0; i--)
            {
                if (pools[i].prefabTag == _prefabTag)
                {
                    spawnOnPool = Instantiate(pools[i].pooledPrefab, pools[i].prefabParent).GetComponent<IPooledObject>();
                    spawnOnPool.GetGameObject().Initialize();
                    break;
                }
            }
        }



        if (_position != null)
        {
            spawnOnPool.GetGameObject().transform.position = _position;
        }
        if (_rotation != null)
        {
            spawnOnPool.GetGameObject().transform.rotation = _rotation;
        }

        spawnOnPool.GetGameObject().transform.SetParent(_parent);

        spawnOnPool.GetGameObject().gameObject.SetActive(true);
        spawnOnPool.OnObjectSpawn();

        return spawnOnPool;
    }

    public void AddObjectPool(string _prefabTag, IPooledObject _pooledObject)
    {
        if (!poolDictionary[_prefabTag].Contains(_pooledObject))
            poolDictionary[_prefabTag].Enqueue(_pooledObject);
    }
}