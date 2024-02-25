using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private ObjectPool pool = null;
    [SerializeField] private Transform spawnPosition1;
    [SerializeField] private Transform spawnPosition2;
    [SerializeField] private Transform spawnPosition3;
    [SerializeField] private Transform spawnPosition4;

    private int randomNumber;
    private GameObject obj;
    private GameObject obj2;

    private void Start()
    {
        pool = GameObject.Find("Object Pool").GetComponent<ObjectPool>();
        Spawn2();
    }
    
    public void Spawn1()
    {
        randomNumber = Random.Range(0,2);
        obj = pool.GetObject(randomNumber);
        if (randomNumber == 0)
        {
          
            obj.transform.position = spawnPosition1.transform.position;
        }
        if (randomNumber == 1)
        {
         
            obj.transform.position = spawnPosition2.transform.position;
        }

    }
    public void Spawn2()
    {
        randomNumber = Random.Range(2,4);
        obj2 = pool.GetObject(randomNumber);
        if (randomNumber == 2)
        {
           
            obj2.transform.position = spawnPosition3.transform.position;
        }
        if (randomNumber == 3)
        {
            
            obj2.transform.position = spawnPosition4.transform.position;
        }
    }
   
}
