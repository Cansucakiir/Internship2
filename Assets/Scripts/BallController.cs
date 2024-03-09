using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private float speed;
    private float turnSpeed;
    private Vector3 movement;
    private GameObject player;
    private PlayerController playerController;
    private float distance;
    private float distance2;

    void Start()
    {
        player = GameObject.Find("Character");
        playerController = player.GetComponent<PlayerController>();
        speed = 4;
        turnSpeed = 70;
        movement = new Vector3(0,0,-1);
        distance = 950;
        
    }

    
    void Update()
    {
        distance2 = Vector3.Distance(transform.position, player.transform.position);
        if (distance2< distance && playerController.isAlive)
        {
            transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
            transform.position += movement * speed;
          
        }
        
    }
}
