using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private int control1 = 0;
    private int random;
    [SerializeField] private SpawnManager spawnManager = null;
    [SerializeField] private PlayerController player = null;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position += new Vector3(0, 0, transform.GetChild(0).GetComponent<Renderer>().bounds.size.z * 4);
         
            control1 += 1;
            Debug.Log(control1);
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
