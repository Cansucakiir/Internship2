using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int health2;
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {
        health2 = 2;
        health = 3;
    }
    private void Update()
    {
        text.text = "Health: " + health;
        
    }
}
