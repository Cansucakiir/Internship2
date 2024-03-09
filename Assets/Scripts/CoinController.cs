using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Score score;
    private GameObject player;
    private float distance;
    private float distance2;
    private float turnSpeed;
    private void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
        player = GameObject.Find("Character");
        distance = 20;
        turnSpeed = 60;
    }
    private void Update()
    {
        distance2 = Vector3.Distance(transform.position, player.transform.position);
        if (distance2 < distance)
        {
            AudioManager.instance.PlayCoinSound();
            this.gameObject.SetActive(false);
            score.timeScore += 5;
            Invoke("SetActive", 10);

        }
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
    void SetActive()
    {
        this.gameObject.SetActive(true);
    }
    
}
