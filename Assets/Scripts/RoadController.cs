using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{

    [SerializeField] private SpawnManager spawnManager = null;
    [SerializeField] private PlayerController player = null;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position += new Vector3(0, 0, transform.GetChild(0).GetComponent<Renderer>().bounds.size.z * 4);
  
            if (player.control == 2)
            {
                spawnManager.Spawn1();
            }
            if (player.control == 4)
            {
                spawnManager.Spawn2();
                player.control = 0;
            }


        }
        
        
        
    }
}
