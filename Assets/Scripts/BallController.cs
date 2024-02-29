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
    private float distance;
    private float distance2;

    void Start()
    {
        player = GameObject.Find("Character");
        speed = 5;
        turnSpeed = 70;
        movement = new Vector3(0,0,-1);
        distance = 1000;
        
    }

    
    void Update()
    {
        distance2 = Vector3.Distance(transform.position, player.transform.position);
        if (distance2< distance)
        {
            transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
            transform.position += movement * speed;
            Debug.Log("mesafe kýsaldý");
        }
        
    }
}
