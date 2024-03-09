using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public int timeScore;
    public int highScore;
    private int previousHighScore;
    public bool isScore;
    private Color highScoreColor;
    private Color newHighScoreColor;
    private bool scoreControl;
    private void Start()
    {
        LoadHighScore();
        scoreControl = true;
        highScore = previousHighScore;
        highScoreColor = new Color(0.78f, 0.31f, 0.31f, 1f);
        newHighScoreColor = new Color(0.6f, 0.027f,0.91f,1f);
        highScoreText.color = highScoreColor;
        isScore = true;
        timeScore = 0;
        StartCoroutine(IncreaseScore());
    }
   
    public IEnumerator IncreaseScore()
    {
        yield return new WaitForSeconds(3f);
        while (isScore)
        {
            scoreText.text = "Score: " + timeScore;
            highScoreText.text = "High Score: " + highScore;
            HighScoreTextControl();
            yield return new WaitForSeconds(0.5f);
            timeScore++;
        }
    }
    public void CheckAndUpdateHighScore()
    {
        if (timeScore > previousHighScore)
        {
            previousHighScore = timeScore;
            PlayerPrefs.SetInt("High Score", previousHighScore);
            PlayerPrefs.Save();
        }
    }
    private void LoadHighScore()
    {
        previousHighScore = PlayerPrefs.GetInt("High Score", 0);
    }
    public void ResetHighScore()
    {
        previousHighScore = 0;
        PlayerPrefs.SetInt("High Score", previousHighScore);
        PlayerPrefs.Save();
    }
    void HighScoreTextControl()
    {
        if (timeScore > previousHighScore)
        {
            highScore = timeScore+1;
        }
        if (timeScore > previousHighScore && scoreControl)
        {
            StartCoroutine(ChangeColor());
            scoreControl = false;
        }
    }
    IEnumerator ChangeColor()
    {
        highScoreText.color = newHighScoreColor;
        yield return new WaitForSeconds(5f);
        highScoreText.color = highScoreColor;
    }
}
