using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject gameObject;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static PoolingManager Instance { get; private set; }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        //Awake()
        //{

        CreatePools();
    }

    private void CreatePools()
    {
        //Creates queues for all pool objects and adds those in a dictionary to call objects later.

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 1; i < pool.size + 1; i++)
            {
                GameObject obj = Instantiate(pool.gameObject, transform);
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject InstantiateFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        //Finds objects queue from given tag.

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist");
            return null;
        }

        //Dequeues the objects and enqueues later to reorder the queue.

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        IPoolable poolable = objectToSpawn.GetComponent<IPoolable>();

        if (poolable != null) poolable.Pooled();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public void DestroyPoolObject(GameObject destroyObject)
    {
        destroyObject.SetActive(false);
    }
}
