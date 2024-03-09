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
    private PlayerController playerController;

    void Start()
    {
        player = GameObject.Find("Character");
        playerController = player.GetComponent<PlayerController>();
        speed = 120;
        distance = 980;
        
    }

    
    void Update()
    {
        distance2 = Vector3.Distance(transform.position, player.transform.position);
        if (distance2 < distance && playerController.isAlive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
            
}
