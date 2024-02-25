using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pool;
        public GameObject objectPrefab;
        public int poolSize;
    }
    public Pool[] pools = null;
    
    //creating gameobjects queues
    private void Awake()
    {
        
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pool = new Queue<GameObject>();
            for (int i = 0; i< pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);

                pools[j].pool.Enqueue(obj);
            }
        }
    }
    //returning a gameobject from queue
    public GameObject GetObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }

        GameObject obj = pools[objectType].pool.Dequeue();
        obj.SetActive(true);
        pools[objectType].pool.Enqueue(obj);
        return obj;

    }

}
