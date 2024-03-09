using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startCountText;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        StartCoroutine(Countdown());
    }
    public IEnumerator Countdown()
    {
        playerController.isAlive = false;
        startCountText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--) 
        {
            startCountText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        playerController.isAlive = true;
        startCountText.gameObject.SetActive(false);
    }
}
