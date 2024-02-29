using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed;
    private float distance;
    private float distance2;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Character");
        speed = 120;
        distance = 1000;
        
    }

    
    void Update()
    {
        distance2 = Vector3.Distance(transform.position, player.transform.position);
        if (distance2 < distance)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
            
}
